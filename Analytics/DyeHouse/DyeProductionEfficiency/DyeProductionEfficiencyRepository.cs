using Analytics.DyeHouse.DyeProductionEfficiency.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.DyeHouse.DyeProductionEfficiency
{
    public class DyeProductionEfficiencyRepository
    {
        private readonly string connectionString;

        public DyeProductionEfficiencyRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;
        }

        public DateTime? GetLatestReportingDate()
        {
            const string sql = "SELECT MAX(ReportingDate) FROM dbo.vw_DyeProductionEfficiency;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                object value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
        }

        public List<DyeProcessLossRow> GetProcessLossByColour(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    CASE WHEN Colour IS NULL OR LTRIM(RTRIM(Colour)) = '' THEN 'Unknown' ELSE Colour END AS Colour,
    SUM(CAST(ISNULL(GrossWeight, 0) AS decimal(18,4))) AS GrossWeight,
    SUM(CAST(ISNULL(NettWeight, 0) AS decimal(18,4))) AS NettWeight
FROM dbo.vw_DyeProductionEfficiency
WHERE ReportingDate >= @FromDate AND ReportingDate < @ToDateExclusive
GROUP BY CASE WHEN Colour IS NULL OR LTRIM(RTRIM(Colour)) = '' THEN 'Unknown' ELSE Colour END
ORDER BY Colour;";
            var results = new List<DyeProcessLossRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        results.Add(new DyeProcessLossRow
                        {
                            Colour = Convert.ToString(reader["Colour"]),
                            GrossWeight = ToDecimal(reader["GrossWeight"]),
                            NettWeight = ToDecimal(reader["NettWeight"])
                        });
            }
            return results;
        }

        public List<DyeDiskVarianceRow> GetDiskVarianceByGreigeQuality(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    CASE WHEN GreigeQuality IS NULL OR LTRIM(RTRIM(GreigeQuality)) = '' THEN 'Unknown' ELSE GreigeQuality END AS GreigeQuality,
    COUNT(*) AS PieceCount,
    AVG(CAST(NULLIF(StandardDisk, 0) AS decimal(18,4))) AS AverageStandardDisk,
    AVG(CAST(NULLIF(ActualDisk, 0) AS decimal(18,4))) AS AverageActualDisk
FROM dbo.vw_DyeProductionEfficiency
WHERE ReportingDate >= @FromDate AND ReportingDate < @ToDateExclusive
GROUP BY CASE WHEN GreigeQuality IS NULL OR LTRIM(RTRIM(GreigeQuality)) = '' THEN 'Unknown' ELSE GreigeQuality END
ORDER BY GreigeQuality;";
            var results = new List<DyeDiskVarianceRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                AddDateParameters(command, fromDate, toDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        results.Add(new DyeDiskVarianceRow
                        {
                            GreigeQuality = Convert.ToString(reader["GreigeQuality"]),
                            PieceCount = Convert.ToInt32(reader["PieceCount"]),
                            AverageStandardDisk = ToDecimal(reader["AverageStandardDisk"]),
                            AverageActualDisk = ToDecimal(reader["AverageActualDisk"])
                        });
            }
            return results;
        }

        private static void AddDateParameters(SqlCommand command, DateTime fromDate, DateTime toDate)
        {
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate.Date;
            command.Parameters.Add("@ToDateExclusive", SqlDbType.Date).Value = toDate.Date.AddDays(1);
        }

        private static decimal ToDecimal(object value)
        {
            return value == null || value == DBNull.Value ? 0m : Convert.ToDecimal(value);
        }
    }
}
