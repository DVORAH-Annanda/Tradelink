using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTCompletedWorkRepository
    {
        private readonly string connectionString;

        public CMTCompletedWorkRepository()
        {
            connectionString =
                ConfigurationManager
                    .ConnectionStrings["TTISqlConnection"]
                    .ConnectionString;
        }

        public DateTime? GetLatestTransactionDate()
        {
            const string sql = @"
SELECT MAX(TransactionDate)
FROM dbo.vw_CMTCompletedWorkAnalysis;";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToDateTime(result);
            }
        }

        public List<CMTBGradeByStyle> GetBGradeByStyle(
    DateTime fromDate,
    DateTime toDate)
        {
            const string sql = @"
SELECT
    Styles,
    ISNULL(SUM(TotalUnitsOnCutSheet), 0) AS TotalUnits,
    ISNULL(SUM(AGrade), 0) AS AGrade,
    ISNULL(SUM(BGrade), 0) AS BGrade
FROM dbo.vw_CMTCompletedWorkAnalysis
WHERE TransactionDate >= @FromDate
  AND TransactionDate < @ToDateExclusive
  AND Styles IS NOT NULL
  AND LTRIM(RTRIM(Styles)) <> ''
GROUP BY Styles
ORDER BY
    CASE
        WHEN SUM(AGrade) + SUM(BGrade) = 0 THEN 0
        ELSE
            CAST(SUM(BGrade) AS decimal(18,6))
            /
            CAST(SUM(AGrade) + SUM(BGrade) AS decimal(18,6))
    END DESC,
    Styles;";

            var results = new List<CMTBGradeByStyle>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@FromDate",
                    SqlDbType.DateTime).Value = fromDate.Date;

                command.Parameters.Add(
                    "@ToDateExclusive",
                    SqlDbType.DateTime).Value = toDate.Date.AddDays(1);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new CMTBGradeByStyle
                        {
                            Style = Convert.ToString(reader["Styles"]),
                            TotalUnits = Convert.ToInt32(reader["TotalUnits"]),
                            AGrade = Convert.ToInt32(reader["AGrade"]),
                            BGrade = Convert.ToInt32(reader["BGrade"])
                        });
                    }
                }
            }

            return results;
        }

        public CMTCompletedWorkSummary GetSummary(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    ISNULL(SUM(TotalUnitsOnCutSheet), 0) AS TotalUnits,
    ISNULL(SUM(AGrade), 0) AS AGrade,
    ISNULL(SUM(BGrade), 0) AS BGrade
FROM dbo.vw_CMTCompletedWorkAnalysis
WHERE TransactionDate >= @FromDate
  AND TransactionDate < @ToDateExclusive;";

            var result = new CMTCompletedWorkSummary();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@FromDate",
                    SqlDbType.DateTime).Value = fromDate.Date;

                command.Parameters.Add(
                    "@ToDateExclusive",
                    SqlDbType.DateTime).Value = toDate.Date.AddDays(1);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.TotalUnits =
                            Convert.ToInt32(reader["TotalUnits"]);

                        result.AGrade =
                            Convert.ToInt32(reader["AGrade"]);

                        result.BGrade =
                            Convert.ToInt32(reader["BGrade"]);
                    }
                }
            }

            return result;
        }

        public List<CMTMnffOspecByStyle> GetMnffAndOspecByStyle(
    DateTime fromDate,
    DateTime toDate)
        {
            const string sql = @"
SELECT
    Styles,
    ISNULL(SUM(TotalUnitsOnCutSheet), 0) AS TotalUnits,
    ISNULL(SUM(MNFF), 0) AS MNFF,
    ISNULL(SUM(Ospec), 0) AS Ospec
FROM dbo.vw_CMTCompletedWorkAnalysis
WHERE TransactionDate >= @FromDate
  AND TransactionDate < @ToDateExclusive
  AND Styles IS NOT NULL
  AND LTRIM(RTRIM(Styles)) <> ''
GROUP BY Styles
ORDER BY Styles;";

            var results =
                new List<CMTMnffOspecByStyle>();

            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@FromDate",
                    SqlDbType.DateTime)
                    .Value = fromDate.Date;

                command.Parameters.Add(
                    "@ToDateExclusive",
                    SqlDbType.DateTime)
                    .Value = toDate.Date.AddDays(1);

                connection.Open();

                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new CMTMnffOspecByStyle
                            {
                                Style =
                                    Convert.ToString(
                                        reader["Styles"]),

                                TotalUnits =
                                    Convert.ToInt32(
                                        reader["TotalUnits"]),

                                MNFF =
                                    Convert.ToInt32(
                                        reader["MNFF"]),

                                Ospec =
                                    Convert.ToInt32(
                                        reader["Ospec"])
                            });
                    }
                }
            }

            return results;
        }

        public List<CMTBGradeHolesByMachine> GetBGradeHolesByMachine(
    DateTime fromDate,
    DateTime toDate)
        {
            const string sql = @"
SELECT
    KnittingMachine,

    ISNULL(
        SUM(TotalUnitsOnCutSheet),
        0
    ) AS TotalUnits,

    ISNULL(
        SUM(Holes),
        0
    ) AS Holes

FROM dbo.vw_CMTCompletedWorkAnalysis

WHERE TransactionDate >= @FromDate
  AND TransactionDate < @ToDateExclusive

  AND KnittingMachine IS NOT NULL
  AND LTRIM(RTRIM(KnittingMachine)) <> ''

GROUP BY KnittingMachine

ORDER BY
    CASE
        WHEN SUM(TotalUnitsOnCutSheet) = 0
            THEN 0

        ELSE
            CAST(SUM(Holes) AS decimal(18,6))
            /
            CAST(
                SUM(TotalUnitsOnCutSheet)
                AS decimal(18,6)
            )
    END DESC,

    KnittingMachine;";

            var results =
                new List<CMTBGradeHolesByMachine>();


            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@FromDate",
                    SqlDbType.DateTime)
                    .Value = fromDate.Date;


                command.Parameters.Add(
                    "@ToDateExclusive",
                    SqlDbType.DateTime)
                    .Value = toDate.Date.AddDays(1);


                connection.Open();


                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new CMTBGradeHolesByMachine
                            {
                                KnittingMachine =
                                    Convert.ToString(
                                        reader["KnittingMachine"]),

                                TotalUnits =
                                    Convert.ToInt32(
                                        reader["TotalUnits"]),

                                Holes =
                                    Convert.ToInt32(
                                        reader["Holes"])
                            });
                    }
                }
            }


            return results;
        }

        public List<CMTSpinningByYarnType> GetSpinningByYarnType(
    DateTime fromDate,
    DateTime toDate)
        {
            const string sql = @"
SELECT
    YarnType,

    ISNULL(SUM(TotalUnitsOnCutSheet), 0) AS TotalUnits,
    ISNULL(SUM(BarreLines), 0) AS BarreLines,
    ISNULL(SUM(Fflaw), 0) AS Fflaw,
    ISNULL(SUM(Contam), 0) AS Contam

FROM dbo.vw_CMTCompletedWorkAnalysis

WHERE TransactionDate >= @FromDate
  AND TransactionDate < @ToDateExclusive
  AND YarnType IS NOT NULL
  AND LTRIM(RTRIM(YarnType)) <> ''

GROUP BY YarnType

ORDER BY YarnType;";

            var results =
                new List<CMTSpinningByYarnType>();

            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@FromDate",
                    SqlDbType.DateTime)
                    .Value = fromDate.Date;

                command.Parameters.Add(
                    "@ToDateExclusive",
                    SqlDbType.DateTime)
                    .Value = toDate.Date.AddDays(1);

                connection.Open();

                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new CMTSpinningByYarnType
                            {
                                YarnType =
                                    Convert.ToString(
                                        reader["YarnType"]),

                                TotalUnits =
                                    Convert.ToInt32(
                                        reader["TotalUnits"]),

                                BarreLines =
                                    Convert.ToInt32(
                                        reader["BarreLines"]),

                                Fflaw =
                                    Convert.ToInt32(
                                        reader["Fflaw"]),

                                Contam =
                                    Convert.ToInt32(
                                        reader["Contam"])
                            });
                    }
                }
            }

            return results;
        }
    }
}
