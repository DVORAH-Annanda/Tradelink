using Analytics.Spinning.QaMeasurements.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.Spinning.QaMeasurements
{
    public sealed class SpinningQaRepository
    {
        private readonly string connectionString;

        public SpinningQaRepository()
        {
            var config = ConfigurationManager.ConnectionStrings["TTISqlConnection"];
            if (config == null || string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new ConfigurationErrorsException("Connection string 'TTISqlConnection' is not configured.");
            connectionString = config.ConnectionString;
        }

        public List<SpinningQaMeasurementRow> GetMeasurements(int measureNo, DateTime fromDate, DateTime toDate)
        {
            if (measureNo != 1 && measureNo != 3)
                throw new ArgumentOutOfRangeException("measureNo", "Only Card (1) and RSB (3) measurements are supported here.");

            // Match the original Spinning.frmViewReport RepNo 14 field mapping:
            // Card: YarnQA_08H00=Daily Avg, YarnQA_10H00=CV%.
            // RSB:  YarnQA_TestNo=shift (1 day / 2 night),
            //       YarnQA_10H00=Canister, YarnQA_12H00=Measurement.
            // Do not apply tolerance filtering in SQL: the dashboard's user-
            // adjustable limits must be applied consistently to all rows.
            const string sql = @"
SELECT q.YarnQA_Pk AS MeasurementId,
       q.YarnQA_Date AS TestDate,
       q.YarnQA_TestNo AS TestNumber,
       q.YarnQA_08H00 AS CardDailyAverage,
       q.YarnQA_10H00 AS CardCvOrRsbCanister,
       q.YarnQA_12H00 AS RsbMeasurement,
       md.MD_MachineCode AS Machine,
       md.MD_Description AS MachineDescription,
       CASE WHEN md.MD_Pk IS NULL THEN 0 ELSE 1 END AS HasMachine
FROM dbo.TLSPN_QAMeasurements AS q
LEFT JOIN dbo.TLADM_MachineDefinitions AS md
  ON md.MD_Pk = q.YarnQA_MachineNo_FK
WHERE q.YarnQA_MeasureNo = @MeasureNo
  AND q.YarnQA_Date >= @FromDate
  AND q.YarnQA_Date < @ToExclusive
ORDER BY q.YarnQA_Date DESC, q.YarnQA_Pk DESC;";

            var result = new List<SpinningQaMeasurementRow>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 90;
                command.Parameters.Add("@MeasureNo", SqlDbType.Int).Value = measureNo;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date;
                command.Parameters.Add("@ToExclusive", SqlDbType.DateTime).Value = toDate.Date.AddDays(1);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SpinningQaMeasurementRow
                        {
                            Id = Convert.ToInt32(reader["MeasurementId"]),
                            Date = Convert.ToDateTime(reader["TestDate"]),
                            Machine = ReadString(reader, "Machine"),
                            MachineDescription = ReadString(reader, "MachineDescription"),
                            TestNumber = ReadInt(reader, "TestNumber"),
                            DailyAverage = measureNo == 1 ? ReadDecimal(reader, "CardDailyAverage") : null,
                            CvPercentage = measureNo == 1 ? ReadDecimal(reader, "CardCvOrRsbCanister") : null,
                            Canister = measureNo == 3 ? ReadDecimal(reader, "CardCvOrRsbCanister") : null,
                            Measurement = measureNo == 3 ? ReadDecimal(reader, "RsbMeasurement") : null,
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
