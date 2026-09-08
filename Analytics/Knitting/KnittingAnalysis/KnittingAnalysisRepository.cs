using Analytics.Knitting.KnittingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Analytics.Knitting.KnittingAnalysis
{
    public class KnittingAnalysisRepository
    {
        private readonly string connectionString;


        public KnittingAnalysisRepository()
        {
            connectionString =
                ConfigurationManager
                    .ConnectionStrings["TTISqlConnection"]
                    .ConnectionString;
        }


        // ============================================================
        // DISK VARIANCE
        // ============================================================

        public List<KnittingDiskVarianceRow> GetDiskVariance(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
WITH DiskData AS
(
    SELECT
        GreigeQuality,

        CASE
            WHEN StandardDisk > 0
             AND StandardDisk < 10
                THEN StandardDisk * 100
            ELSE StandardDisk
        END AS CorrectedStandardDisk,

        CASE
            WHEN ActualDisk > 0
             AND ActualDisk < 10
                THEN ActualDisk * 100
            ELSE ActualDisk
        END AS CorrectedActualDisk

    FROM dbo.vw_KnittingDiskVariance

    WHERE ProductionDate >= @FromDate
      AND ProductionDate < @ToDateExclusive
)

SELECT
    GreigeQuality,

    COUNT(*) AS PieceCount,

    AVG(
        CAST(
            CorrectedStandardDisk
            AS decimal(18,4)
        )
    ) AS AverageStandardDisk,

    AVG(
        CAST(
            CorrectedActualDisk
            AS decimal(18,4)
        )
    ) AS AverageActualDisk

FROM DiskData

WHERE GreigeQuality IS NOT NULL
  AND LTRIM(RTRIM(GreigeQuality)) <> ''

GROUP BY GreigeQuality

ORDER BY GreigeQuality;";


            var results =
                new List<KnittingDiskVarianceRow>();


            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                AddDateParameters(
                    command,
                    fromDate,
                    toDate);


                connection.Open();


                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new KnittingDiskVarianceRow
                            {
                                GreigeQuality =
                                    Convert.ToString(
                                        reader["GreigeQuality"]),

                                PieceCount =
                                    Convert.ToInt32(
                                        reader["PieceCount"]),

                                AverageStandardDisk =
                                    ToDecimal(
                                        reader["AverageStandardDisk"]),

                                AverageActualDisk =
                                    ToDecimal(
                                        reader["AverageActualDisk"])
                            });
                    }
                }
            }


            return results;
        }



        // ============================================================
        // KNITTING QUALITY
        // ============================================================

        public List<KnittingQualityRow> GetKnittingQuality(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    GreigeProduct,
    Machine,

    COUNT(*) AS TotalPieces,

    SUM(
        CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(Grade, '')))) = 'A'
                THEN 1
            ELSE 0
        END
    ) AS AGrade,

    SUM(
        CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(Grade, '')))) = 'B'
                THEN 1
            ELSE 0
        END
    ) AS BGrade,

    SUM(
        CASE
            WHEN UPPER(LTRIM(RTRIM(ISNULL(Grade, '')))) = 'C'
                THEN 1
            ELSE 0
        END
    ) AS CGrade,


    SUM(
        CAST(
            ISNULL(GreigeP_Meas1, 0)
            AS decimal(18,2)
        )
    ) AS BrokenNeedle,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas2, 0)
            AS decimal(18,2)
        )
    ) AS DroppedStitches,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas3, 0)
            AS decimal(18,2)
        )
    ) AS Holes,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas4, 0)
            AS decimal(18,2)
        )
    ) AS OilMarks,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas5, 0)
            AS decimal(18,2)
        )
    ) AS PressOff,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas6, 0)
            AS decimal(18,2)
        )
    ) AS SlubMarks,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas7, 0)
            AS decimal(18,2)
        )
    ) AS Thick,

    SUM(
        CAST(
            ISNULL(GreigeP_Meas8, 0)
            AS decimal(18,2)
        )
    ) AS Thin


FROM dbo.vw_KnittingQuality

WHERE GreigeP_InspDate >= @FromDate
  AND GreigeP_InspDate < @ToDateExclusive

  AND GreigeProduct IS NOT NULL
  AND LTRIM(RTRIM(GreigeProduct)) <> ''

GROUP BY
    GreigeProduct,
    Machine

ORDER BY
    GreigeProduct,
    Machine;";


            var results =
                new List<KnittingQualityRow>();


            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                AddDateParameters(
                    command,
                    fromDate,
                    toDate);


                connection.Open();


                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new KnittingQualityRow
                            {
                                GreigeProduct =
                                    Convert.ToString(
                                        reader["GreigeProduct"]),

                                Machine =
                                    Convert.ToString(
                                        reader["Machine"]),

                                TotalPieces =
                                    Convert.ToInt32(
                                        reader["TotalPieces"]),

                                AGrade =
                                    Convert.ToInt32(
                                        reader["AGrade"]),

                                BGrade =
                                    Convert.ToInt32(
                                        reader["BGrade"]),

                                CGrade =
                                    Convert.ToInt32(
                                        reader["CGrade"]),

                                BrokenNeedle =
                                    ToDecimal(
                                        reader["BrokenNeedle"]),

                                DroppedStitches =
                                    ToDecimal(
                                        reader["DroppedStitches"]),

                                Holes =
                                    ToDecimal(
                                        reader["Holes"]),

                                OilMarks =
                                    ToDecimal(
                                        reader["OilMarks"]),

                                PressOff =
                                    ToDecimal(
                                        reader["PressOff"]),

                                SlubMarks =
                                    ToDecimal(
                                        reader["SlubMarks"]),

                                Thick =
                                    ToDecimal(
                                        reader["Thick"]),

                                Thin =
                                    ToDecimal(
                                        reader["Thin"])
                            });
                    }
                }
            }


            return results;
        }



        // ============================================================
        // PROCESS LOSS - KNIT ORDER DETAIL
        // ============================================================

        public List<KnittingProcessLossOrderRow> GetProcessLossOrders(
            DateTime fromDate,
            DateTime toDate)
        {
            const string sql = @"
SELECT
    PrimaryKey,
    OrderNumber,
    DateClosed,
    ProductionDate,
    Machine,
    Product,
    OrderQty,
    TotalKnitted,
    YarnConsumed,
    ProcessLoss,
    YarnType,
    YarnTex,
    YarnTwist

FROM dbo.vw_KnittingProcessLoss

WHERE ProductionDate >= @FromDate
  AND ProductionDate < @ToDateExclusive

ORDER BY
    ProductionDate,
    Machine,
    OrderNumber;";


            var results =
                new List<KnittingProcessLossOrderRow>();


            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                AddDateParameters(
                    command,
                    fromDate,
                    toDate);


                connection.Open();


                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new KnittingProcessLossOrderRow
                            {
                                PrimaryKey =
                                    Convert.ToInt32(
                                        reader["PrimaryKey"]),

                                OrderNumber =
                                    Convert.ToString(
                                        reader["OrderNumber"]),

                                DateClosed =
                                    ToNullableDateTime(
                                        reader["DateClosed"]),

                                ProductionDate =
                                    ToNullableDateTime(
                                        reader["ProductionDate"]),

                                Machine =
                                    Convert.ToString(
                                        reader["Machine"]),

                                Product =
                                    Convert.ToString(
                                        reader["Product"]),

                                OrderQty =
                                    ToDecimal(
                                        reader["OrderQty"]),

                                TotalKnitted =
                                    ToDecimal(
                                        reader["TotalKnitted"]),

                                YarnConsumed =
                                    ToDecimal(
                                        reader["YarnConsumed"]),

                                ProcessLoss =
                                    ToDecimal(
                                        reader["ProcessLoss"]),

                                YarnType =
                                    Convert.ToString(
                                        reader["YarnType"]),

                                YarnTex =
                                    ToDecimal(
                                        reader["YarnTex"]),

                                YarnTwist =
                                    ToDecimal(
                                        reader["YarnTwist"])
                            });
                    }
                }
            }


            return results;
        }



        // ============================================================
        // PROCESS LOSS - MACHINE / PRODUCT SUMMARY
        // ============================================================

        public List<KnittingProcessLossMachineRow>
            GetProcessLossByMachine(
                DateTime fromDate,
                DateTime toDate)
        {
            const string sql = @"
SELECT
    Machine,
    Product,

    ISNULL(
        SUM(OrderQty),
        0
    ) AS OrderQty,

    ISNULL(
        SUM(TotalKnitted),
        0
    ) AS TotalKnitted,

    ISNULL(
        SUM(YarnConsumed),
        0
    ) AS YarnConsumed,

    CASE
        WHEN ISNULL(SUM(YarnConsumed), 0) <> 0
        THEN
            (
                100.0 *
                (
                    SUM(TotalKnitted)
                    /
                    SUM(YarnConsumed)
                )
            ) - 100.0

        ELSE 0.0
    END AS ProcessLoss

FROM dbo.vw_KnittingProcessLoss

WHERE ProductionDate >= @FromDate
  AND ProductionDate < @ToDateExclusive

  AND Machine IS NOT NULL
  AND LTRIM(RTRIM(Machine)) <> ''

GROUP BY
    Machine,
    Product

ORDER BY
    Machine,
    Product;";


            var results =
                new List<KnittingProcessLossMachineRow>();


            using (var connection =
                   new SqlConnection(connectionString))

            using (var command =
                   new SqlCommand(sql, connection))
            {
                AddDateParameters(
                    command,
                    fromDate,
                    toDate);


                connection.Open();


                using (var reader =
                       command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(
                            new KnittingProcessLossMachineRow
                            {
                                Machine =
                                    Convert.ToString(
                                        reader["Machine"]),

                                Product =
                                    Convert.ToString(
                                        reader["Product"]),

                                OrderQty =
                                    ToDecimal(
                                        reader["OrderQty"]),

                                TotalKnitted =
                                    ToDecimal(
                                        reader["TotalKnitted"]),

                                YarnConsumed =
                                    ToDecimal(
                                        reader["YarnConsumed"]),

                                ProcessLoss =
                                    ToDecimal(
                                        reader["ProcessLoss"])
                            });
                    }
                }
            }


            return results;
        }



        // ============================================================
        // HELPERS
        // ============================================================

        private static void AddDateParameters(
            SqlCommand command,
            DateTime fromDate,
            DateTime toDate)
        {
            command.Parameters.Add(
                "@FromDate",
                SqlDbType.DateTime)
                .Value = fromDate.Date;


            command.Parameters.Add(
                "@ToDateExclusive",
                SqlDbType.DateTime)
                .Value = toDate.Date.AddDays(1);
        }


        private static decimal ToDecimal(
            object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(value);
        }


        private static DateTime? ToNullableDateTime(
            object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(value);
        }
    }
}
