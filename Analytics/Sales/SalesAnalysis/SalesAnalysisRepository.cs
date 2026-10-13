using Analytics.Sales.SalesAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Analytics.Sales.SalesAnalysis
{
    public class SalesAnalysisRepository
    {
        private readonly string connectionString;

        public SalesAnalysisRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;
        }

        public DateTime? GetLatestSoldDate()
        {
            const string sql = "SELECT MAX(SoldDate) FROM dbo.vw_SalesAnalysis WHERE Sold = 1;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                object value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
        }

        public SalesSummary GetSummary(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    SUM(CASE WHEN SoldDate >= @FromDate AND SoldDate < @ToDateExclusive THEN GrossSalesQty ELSE 0 END) AS CurrentGrossSales,
    SUM(CASE WHEN ReturnedDate >= @FromDate AND ReturnedDate < @ToDateExclusive THEN ReturnedQty ELSE 0 END) AS CurrentReturns,
    SUM(CASE WHEN SoldDate >= @PreviousFromDate AND SoldDate < @PreviousToDateExclusive THEN GrossSalesQty ELSE 0 END) AS PreviousGrossSales,
    SUM(CASE WHEN ReturnedDate >= @PreviousFromDate AND ReturnedDate < @PreviousToDateExclusive THEN ReturnedQty ELSE 0 END) AS PreviousReturns
FROM dbo.vw_SalesAnalysis;";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddComparisonDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return new SalesSummary();
                    return new SalesSummary
                    {
                        CurrentGrossSales = ToInt(reader["CurrentGrossSales"]),
                        CurrentReturns = ToInt(reader["CurrentReturns"]),
                        PreviousGrossSales = ToInt(reader["PreviousGrossSales"]),
                        PreviousReturns = ToInt(reader["PreviousReturns"])
                    };
                }
            }
        }

        public List<SalesMonthlyComparisonRow> GetMonthlyComparison(DateTime fromDate, DateTime toDate)
        {
            var current = GetMonthlyPeriod(fromDate, toDate);
            var previous = GetMonthlyPeriod(fromDate.AddYears(-1), toDate.AddYears(-1));
            var results = new List<SalesMonthlyComparisonRow>();

            DateTime month = new DateTime(fromDate.Year, fromDate.Month, 1);
            DateTime lastMonth = new DateTime(toDate.Year, toDate.Month, 1);
            while (month <= lastMonth)
            {
                string currentKey = month.ToString("yyyy-MM");
                string previousKey = month.AddYears(-1).ToString("yyyy-MM");
                PeriodTotals currentTotals;
                PeriodTotals previousTotals;
                current.TryGetValue(currentKey, out currentTotals);
                previous.TryGetValue(previousKey, out previousTotals);

                results.Add(new SalesMonthlyComparisonRow
                {
                    Month = month.ToString("MMM"),
                    CurrentGrossSales = currentTotals == null ? 0 : currentTotals.GrossSales,
                    CurrentReturns = currentTotals == null ? 0 : currentTotals.Returns,
                    PreviousGrossSales = previousTotals == null ? 0 : previousTotals.GrossSales,
                    PreviousReturns = previousTotals == null ? 0 : previousTotals.Returns
                });
                month = month.AddMonths(1);
            }
            return results;
        }

        public List<SalesPerformanceRow> GetCustomerPerformance(DateTime fromDate, DateTime toDate)
        {
            return GetPerformance("Customer", fromDate, toDate);
        }

        public List<SalesPerformanceRow> GetStylePerformance(DateTime fromDate, DateTime toDate)
        {
            return GetPerformance("Style", fromDate, toDate);
        }

        public List<SalesPerformanceRow> GetColourPerformance(DateTime fromDate, DateTime toDate)
        {
            return GetPerformance("Colour", fromDate, toDate);
        }

        public SalesDataQualitySummary GetDataQuality(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    SUM(CASE WHEN Sold = 1 AND SoldDate >= @FromDate AND SoldDate < @ToDateExclusive THEN 1 ELSE 0 END) AS SoldRows,
    SUM(CASE WHEN Sold = 1 AND SoldDate >= @FromDate AND SoldDate < @ToDateExclusive AND GrossSalesQty <= 0 THEN 1 ELSE 0 END) AS ZeroOrNegativeQtyRows,
    SUM(CASE WHEN Sold = 1 AND SoldDate >= @FromDate AND SoldDate < @ToDateExclusive
             AND (Style IS NULL OR Colour IS NULL OR Size IS NULL OR Customer IS NULL OR Warehouse IS NULL) THEN 1 ELSE 0 END) AS MissingMasterDataRows,
    SUM(CASE WHEN Sold = 1 AND SoldDate IS NULL THEN 1 ELSE 0 END) AS MissingSoldDateRows,
    MAX(SoldDate) AS LatestSoldDate,
    MAX(ReturnedDate) AS LatestReturnDate
FROM dbo.vw_SalesAnalysis;";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return new SalesDataQualitySummary();
                    return new SalesDataQualitySummary
                    {
                        SoldRows = ToInt(reader["SoldRows"]),
                        ZeroOrNegativeQtyRows = ToInt(reader["ZeroOrNegativeQtyRows"]),
                        MissingMasterDataRows = ToInt(reader["MissingMasterDataRows"]),
                        MissingSoldDateRows = ToInt(reader["MissingSoldDateRows"]),
                        LatestSoldDate = ToNullableDate(reader["LatestSoldDate"]),
                        LatestReturnDate = ToNullableDate(reader["LatestReturnDate"])
                    };
                }
            }
        }

        private List<SalesPerformanceRow> GetPerformance(string columnName, DateTime fromDate, DateTime toDate)
        {
            if (columnName != "Customer" && columnName != "Style" && columnName != "Colour")
                throw new ArgumentException("Unsupported sales grouping column.", "columnName");

            string sql = @"
SELECT
    CASE WHEN " + columnName + @" IS NULL OR LTRIM(RTRIM(" + columnName + @")) = '' THEN 'Unknown' ELSE " + columnName + @" END AS Name,
    SUM(CASE WHEN SoldDate >= @FromDate AND SoldDate < @ToDateExclusive THEN GrossSalesQty ELSE 0 END) AS CurrentGrossSales,
    SUM(CASE WHEN ReturnedDate >= @FromDate AND ReturnedDate < @ToDateExclusive THEN ReturnedQty ELSE 0 END) AS CurrentReturns,
    SUM(CASE WHEN SoldDate >= @PreviousFromDate AND SoldDate < @PreviousToDateExclusive THEN GrossSalesQty ELSE 0 END) AS PreviousGrossSales,
    SUM(CASE WHEN ReturnedDate >= @PreviousFromDate AND ReturnedDate < @PreviousToDateExclusive THEN ReturnedQty ELSE 0 END) AS PreviousReturns
FROM dbo.vw_SalesAnalysis
WHERE (SoldDate >= @PreviousFromDate AND SoldDate < @ToDateExclusive)
   OR (ReturnedDate >= @PreviousFromDate AND ReturnedDate < @ToDateExclusive)
GROUP BY CASE WHEN " + columnName + @" IS NULL OR LTRIM(RTRIM(" + columnName + @")) = '' THEN 'Unknown' ELSE " + columnName + @" END;";

            var results = new List<SalesPerformanceRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddComparisonDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new SalesPerformanceRow
                        {
                            Name = Convert.ToString(reader["Name"]),
                            CurrentGrossSales = ToInt(reader["CurrentGrossSales"]),
                            CurrentReturns = ToInt(reader["CurrentReturns"]),
                            PreviousGrossSales = ToInt(reader["PreviousGrossSales"]),
                            PreviousReturns = ToInt(reader["PreviousReturns"])
                        });
                    }
                }
            }
            return results.OrderByDescending(x => x.CurrentNetSales).ToList();
        }

        private Dictionary<string, PeriodTotals> GetMonthlyPeriod(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT MonthKey, SUM(GrossSales) AS GrossSales, SUM(Returns) AS Returns
FROM
(
    SELECT CONVERT(char(7), SoldDate, 120) AS MonthKey, SUM(GrossSalesQty) AS GrossSales, 0 AS Returns
    FROM dbo.vw_SalesAnalysis
    WHERE SoldDate >= @FromDate AND SoldDate < @ToDateExclusive
    GROUP BY CONVERT(char(7), SoldDate, 120)

    UNION ALL

    SELECT CONVERT(char(7), ReturnedDate, 120) AS MonthKey, 0 AS GrossSales, SUM(ReturnedQty) AS Returns
    FROM dbo.vw_SalesAnalysis
    WHERE ReturnedDate >= @FromDate AND ReturnedDate < @ToDateExclusive
    GROUP BY CONVERT(char(7), ReturnedDate, 120)
) X
GROUP BY MonthKey
ORDER BY MonthKey;";

            var results = new Dictionary<string, PeriodTotals>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results[Convert.ToString(reader["MonthKey"])] = new PeriodTotals
                        {
                            GrossSales = ToInt(reader["GrossSales"]),
                            Returns = ToInt(reader["Returns"])
                        };
                    }
                }
            }
            return results;
        }

        private static void AddDateParameters(SqlCommand command, DateTime fromDate, DateTime toDate)
        {
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate.Date;
            command.Parameters.Add("@ToDateExclusive", SqlDbType.Date).Value = toDate.Date.AddDays(1);
        }

        private static void AddComparisonDateParameters(SqlCommand command, DateTime fromDate, DateTime toDate)
        {
            AddDateParameters(command, fromDate, toDate);
            command.Parameters.Add("@PreviousFromDate", SqlDbType.Date).Value = fromDate.Date.AddYears(-1);
            command.Parameters.Add("@PreviousToDateExclusive", SqlDbType.Date).Value = toDate.Date.AddYears(-1).AddDays(1);
        }

        private static int ToInt(object value)
        {
            return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        private static DateTime? ToNullableDate(object value)
        {
            return value == null || value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
        }

        private class PeriodTotals
        {
            public int GrossSales { get; set; }
            public int Returns { get; set; }
        }
    }
}
