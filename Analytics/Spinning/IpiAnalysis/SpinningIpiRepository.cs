using Analytics.Spinning.IpiAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.Spinning.IpiAnalysis
{
    public sealed class SpinningIpiRepository
    {
        private readonly string connectionString;

        public SpinningIpiRepository()
        {
            var config = ConfigurationManager.ConnectionStrings["TTISqlConnection"];
            if (config == null || string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new ConfigurationErrorsException("Connection string 'TTISqlConnection' is not configured.");
            connectionString = config.ConnectionString;
        }

        // Uses the same legacy fields as Spinning.frmViewReport, RepNo 14,
        // MeasurementOption 2.  Do not interpret YarnQA_08H00 etc as times:
        // they are reused as quality-measurement fields for this measurement type.
        public DateTime? GetLatestMeasurementDate()
        {
            const string sql = @"SELECT MAX(YarnQA_Date)
                                  FROM dbo.TLSPN_QAMeasurements
                                  WHERE YarnQA_MeasureNo = 2";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                object value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
        }

        public List<SpinningIpiRow> GetMeasurements(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    q.YarnQA_Pk AS MeasurementId,
    q.YarnQA_Date AS TestDate,
    cm.TLCTM_Description AS MergeName,
    yo.YarnO_OrderNumber AS YarnOrderNumber,
    md.MD_MachineCode AS Machine,
    md.MD_Description AS MachineDescription,
    yt.YA_TexCount AS NominalTex,
    q.YarnQA_08H00 AS MeasuredTex,
    q.YarnQA_10H00 AS CvPercentage,
    q.YarnQA_12H00 AS Thin,
    q.YarnQA_14H00 AS Thick,
    q.YarnQA_16H00 AS Nep,
    q.YarnQA_18H00 AS Ipi,
    CASE WHEN yo.YarnO_Pk IS NULL THEN 0 ELSE 1 END AS HasYarnOrder,
    CASE WHEN yt.YA_Id IS NULL THEN 0 ELSE 1 END AS HasYarnType,
    CASE WHEN md.MD_Pk IS NULL THEN 0 ELSE 1 END AS HasMachine
FROM dbo.TLSPN_QAMeasurements AS q
LEFT JOIN dbo.TLSPN_YarnOrder AS yo
    ON yo.YarnO_Pk = q.YarnQA_YarnOrder_FK
LEFT JOIN dbo.TLSPN_CottonMerge AS cm
    ON cm.TLCTM_Pk = yo.YarnO_MergeContract_FK
LEFT JOIN dbo.TLADM_Yarn AS yt
    ON yt.YA_Id = yo.Yarno_YarnType_FK
LEFT JOIN dbo.TLADM_MachineDefinitions AS md
    ON md.MD_Pk = q.YarnQA_MachineNo_FK
WHERE q.YarnQA_MeasureNo = 2
  AND q.YarnQA_Date >= @FromDate
  AND q.YarnQA_Date < @ToDateExclusive
ORDER BY yt.YA_TexCount, q.YarnQA_Date DESC, q.YarnQA_Pk DESC;";

            var result = new List<SpinningIpiRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 90;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date;
                command.Parameters.Add("@ToDateExclusive", SqlDbType.DateTime).Value = toDate.Date.AddDays(1);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SpinningIpiRow
                        {
                            MeasurementId = Convert.ToInt32(reader["MeasurementId"]),
                            TestDate = Convert.ToDateTime(reader["TestDate"]),
                            Merge = ReadString(reader, "MergeName"),
                            YarnOrderNumber = ReadInt(reader, "YarnOrderNumber"),
                            Machine = ReadString(reader, "Machine"),
                            MachineDescription = ReadString(reader, "MachineDescription"),
                            NominalTex = ReadDecimal(reader, "NominalTex"),
                            MeasuredTex = ReadDecimal(reader, "MeasuredTex"),
                            CvPercentage = ReadDecimal(reader, "CvPercentage"),
                            Thin = ReadDecimal(reader, "Thin"),
                            Thick = ReadDecimal(reader, "Thick"),
                            Nep = ReadDecimal(reader, "Nep"),
                            Ipi = ReadDecimal(reader, "Ipi"),
                            HasYarnOrder = Convert.ToInt32(reader["HasYarnOrder"]) == 1,
                            HasYarnType = Convert.ToInt32(reader["HasYarnType"]) == 1,
                            HasMachine = Convert.ToInt32(reader["HasMachine"]) == 1
                        });
                    }
                }
            }
            return result;
        }

        private static string ReadString(IDataRecord row, string column)
        {
            int index = row.GetOrdinal(column);
            return row.IsDBNull(index) ? "" : Convert.ToString(row.GetValue(index));
        }
        private static int? ReadInt(IDataRecord row, string column)
        {
            int index = row.GetOrdinal(column);
            return row.IsDBNull(index) ? (int?)null : Convert.ToInt32(row.GetValue(index));
        }
        private static decimal? ReadDecimal(IDataRecord row, string column)
        {
            int index = row.GetOrdinal(column);
            return row.IsDBNull(index) ? (decimal?)null : Convert.ToDecimal(row.GetValue(index));
        }
    }
}
