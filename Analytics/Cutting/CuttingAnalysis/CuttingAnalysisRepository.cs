using Analytics.Cutting.CuttingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.Cutting.CuttingAnalysis
{
    public class CuttingAnalysisRepository
    {
        private readonly string connectionString;

        public CuttingAnalysisRepository()
        {
            connectionString =
                ConfigurationManager
                    .ConnectionStrings["TTISqlConnection"]
                    .ConnectionString;
        }

        public DateTime? GetLatestProductionDate()
        {
            const string sql = @"
SELECT MAX(ProductionDate)
FROM dbo.vw_CuttingProductionAnalysis;";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();

                object value = command.ExecuteScalar();

                if (value == null || value == DBNull.Value)
                    return null;

                return Convert.ToDateTime(value);
            }
        }

        // Expected quantity belongs to the cut sheet while ActualQty belongs
        // to individual receipts. Reduce to one row per cut sheet first so
        // ExpectedQty is not duplicated when a cut sheet has multiple receipts.
        public List<CuttingProductionQualityRow> GetProductionByQuality(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
WITH CutSheetProduction AS
(
    SELECT
        CutSheetPk,
        CASE
            WHEN Quality IS NULL
              OR LTRIM(RTRIM(Quality)) = ''
                THEN 'Unknown'
            ELSE Quality
        END AS Quality,
        MAX(CAST(ISNULL(ExpectedQty, 0) AS decimal(18,4))) AS ExpectedQty,
        SUM(CAST(ISNULL(ActualQty, 0) AS decimal(18,4))) AS ActualQty
    FROM dbo.vw_CuttingProductionAnalysis
    WHERE ProductionDate >= @FromDate
      AND ProductionDate < @ToDateExclusive
    GROUP BY
        CutSheetPk,
        CASE
            WHEN Quality IS NULL
              OR LTRIM(RTRIM(Quality)) = ''
                THEN 'Unknown'
            ELSE Quality
        END
)
SELECT
    Quality,
    SUM(ExpectedQty) AS ExpectedQty,
    SUM(ActualQty) AS ActualQty
FROM CutSheetProduction
GROUP BY Quality
ORDER BY Quality;";

            var results = new List<CuttingProductionQualityRow>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new CuttingProductionQualityRow
                        {
                            Quality = Convert.ToString(reader["Quality"]),
                            ExpectedQty = ToDecimal(reader["ExpectedQty"]),
                            ActualQty = ToDecimal(reader["ActualQty"])
                        });
                    }
                }
            }

            return results;
        }

        public List<CuttingProductionDailyRow> GetProductionByDay(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    CAST(ProductionDate AS date) AS ProductionDate,
    SUM(CAST(ISNULL(ActualQty, 0) AS decimal(18,4))) AS ActualQty
FROM dbo.vw_CuttingProductionAnalysis
WHERE ProductionDate >= @FromDate
  AND ProductionDate < @ToDateExclusive
GROUP BY CAST(ProductionDate AS date)
ORDER BY CAST(ProductionDate AS date);";

            var results = new List<CuttingProductionDailyRow>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new CuttingProductionDailyRow
                        {
                            ProductionDate = Convert.ToDateTime(reader["ProductionDate"]),
                            ActualQty = ToDecimal(reader["ActualQty"])
                        });
                    }
                }
            }

            return results;
        }

        public List<CuttingProductionMachineRow> GetProductionByMachine(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    CASE
        WHEN Machine IS NULL
          OR LTRIM(RTRIM(Machine)) = ''
            THEN 'Unknown'
        ELSE Machine
    END AS Machine,
    SUM(CAST(ISNULL(ActualQty, 0) AS decimal(18,4))) AS ActualQty
FROM dbo.vw_CuttingProductionAnalysis
WHERE ProductionDate >= @FromDate
  AND ProductionDate < @ToDateExclusive
GROUP BY
    CASE
        WHEN Machine IS NULL
          OR LTRIM(RTRIM(Machine)) = ''
            THEN 'Unknown'
        ELSE Machine
    END
ORDER BY ActualQty DESC;";

            var results = new List<CuttingProductionMachineRow>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new CuttingProductionMachineRow
                        {
                            Machine = Convert.ToString(reader["Machine"]),
                            ActualQty = ToDecimal(reader["ActualQty"])
                        });
                    }
                }
            }

            return results;
        }

        public List<CuttingWasteQualityRow> GetWasteByQuality(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    CASE
        WHEN Quality IS NULL
          OR LTRIM(RTRIM(Quality)) = ''
            THEN 'Unknown'
        ELSE Quality
    END AS Quality,
    SUM(CAST(ISNULL(FabNetWeight, 0) AS decimal(18,4))) AS FabricWeight,
    SUM(CAST(ISNULL(RecordedCuttingWaste, 0) AS decimal(18,4))) AS RecordedCuttingWaste,
    SUM(CAST(ISNULL(RecordedPanelWaste, 0) AS decimal(18,4))) AS RecordedPanelWaste,
    SUM(CAST(ISNULL(TotalWaste, 0) AS decimal(18,4))) AS TotalWaste
FROM dbo.vw_CuttingWasteAnalysis
WHERE CutDate >= @FromDate
  AND CutDate < @ToDateExclusive
GROUP BY
    CASE
        WHEN Quality IS NULL
          OR LTRIM(RTRIM(Quality)) = ''
            THEN 'Unknown'
        ELSE Quality
    END
ORDER BY Quality;";

            var results = new List<CuttingWasteQualityRow>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new CuttingWasteQualityRow
                        {
                            Quality = Convert.ToString(reader["Quality"]),
                            FabricWeight = ToDecimal(reader["FabricWeight"]),
                            RecordedCuttingWaste = ToDecimal(reader["RecordedCuttingWaste"]),
                            RecordedPanelWaste = ToDecimal(reader["RecordedPanelWaste"]),
                            TotalWaste = ToDecimal(reader["TotalWaste"])
                        });
                    }
                }
            }

            return results;
        }

        private static void AddDateParameters(
            SqlCommand command,
            DateTime fromDate,
            DateTime toDate)
        {
            command.Parameters.Add("@FromDate", SqlDbType.DateTime)
                .Value = fromDate.Date;

            command.Parameters.Add("@ToDateExclusive", SqlDbType.DateTime)
                .Value = toDate.Date.AddDays(1);
        }

        private static decimal ToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            return Convert.ToDecimal(value);
        }
    }
}
