using Analytics.Common;
using Analytics.Knitting.KnittingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Analytics.Knitting.KnittingAnalysis
{
    public class KnittingAnalysisDashboardBuilder
    {
        public string Build(
            List<KnittingDiskVarianceRow> diskVariance,
            List<KnittingQualityRow> knittingQuality,
            List<KnittingProcessLossOrderRow> processLossOrders,
            List<KnittingProcessLossMachineRow> processLossByMachine,
            DateTime fromDate,
            DateTime toDate)
        {
            string template =
                EmbeddedResourceLoader.ReadText(
                    "knitting-analysis.html");


            string css =
                EmbeddedResourceLoader.ReadText(
                    "analytics.css");


            string chartJs =
                EmbeddedResourceLoader.ReadText(
                    "chart.umd.min.js");


            string pageJs =
                EmbeddedResourceLoader.ReadText(
                    "knitting-analysis.js");


            var payload = new
            {
                fromDate =
                    fromDate.ToString("dd MMM yyyy"),

                toDate =
                    toDate.ToString("dd MMM yyyy"),


                // ==============================================
                // DISK VARIANCE
                // ==============================================
                diskVariance =
                    diskVariance
                        .Select(x => new
                        {
                            greigeQuality =
                                x.GreigeQuality,

                            pieceCount =
                                x.PieceCount,

                            averageStandardDisk =
                                Math.Round(
                                    x.AverageStandardDisk,
                                    2),

                            averageActualDisk =
                                Math.Round(
                                    x.AverageActualDisk,
                                    2),

                            diskVariancePercentage =
                                Math.Round(
                                    x.DiskVariancePercentage,
                                    2)
                        })
                        .ToList(),


                // ==============================================
                // KNITTING QUALITY
                // ==============================================
                knittingQuality =
                    knittingQuality
                        .Select(x => new
                        {
                            greigeProduct =
                                x.GreigeProduct,

                            machine =
                                x.Machine,

                            totalPieces =
                                x.TotalPieces,

                            aGrade =
                                x.AGrade,

                            bGrade =
                                x.BGrade,

                            cGrade =
                                x.CGrade,

                            brokenNeedle =
                                x.BrokenNeedle,

                            droppedStitches =
                                x.DroppedStitches,

                            holes =
                                x.Holes,

                            oilMarks =
                                x.OilMarks,

                            pressOff =
                                x.PressOff,

                            slubMarks =
                                x.SlubMarks,

                            thick =
                                x.Thick,

                            thin =
                                x.Thin,

                            totalFaults =
                                x.TotalFaults,

                            averageFaultsPerPiece =
                                Math.Round(
                                    x.AverageFaultsPerPiece,
                                    2),

                            averageHolesPerPiece =
                                Math.Round(
                                    x.AverageHolesPerPiece,
                                    2),

                            averageSlubsPerPiece =
                                Math.Round(
                                    x.AverageSlubsPerPiece,
                                    2),

                            averageFaultsExcludingSlubs =
                                Math.Round(
                                    x.AverageFaultsExcludingSlubs,
                                    2)
                        })
                        .ToList(),


                // ==============================================
                // PROCESS LOSS - ORDER DETAIL
                // ==============================================
                processLossOrders =
                    processLossOrders
                        .Select(x => new
                        {
                            primaryKey =
                                x.PrimaryKey,

                            orderNumber =
                                x.OrderNumber,

                            dateClosed =
                                x.DateClosed.HasValue
                                    ? x.DateClosed.Value
                                        .ToString("dd MMM yyyy")
                                    : "",

                            productionDate =
                                x.ProductionDate.HasValue
                                    ? x.ProductionDate.Value
                                        .ToString("dd MMM yyyy")
                                    : "",

                            machine =
                                x.Machine,

                            product =
                                x.Product,

                            orderQty =
                                Math.Round(
                                    x.OrderQty,
                                    2),

                            totalKnitted =
                                Math.Round(
                                    x.TotalKnitted,
                                    2),

                            yarnConsumed =
                                Math.Round(
                                    x.YarnConsumed,
                                    2),

                            processLoss =
                                Math.Round(
                                    x.ProcessLoss,
                                    2),

                            yarnType =
                                x.YarnType,

                            yarnTex =
                                Math.Round(
                                    x.YarnTex,
                                    2),

                            yarnTwist =
                                Math.Round(
                                    x.YarnTwist,
                                    2)
                        })
                        .ToList(),


                // ==============================================
                // PROCESS LOSS - MACHINE / PRODUCT SUMMARY
                // ==============================================
                processLossByMachine =
                    processLossByMachine
                        .Select(x => new
                        {
                            machine =
                                x.Machine,

                            product =
                                x.Product,

                            orderQty =
                                Math.Round(
                                    x.OrderQty,
                                    2),

                            totalKnitted =
                                Math.Round(
                                    x.TotalKnitted,
                                    2),

                            yarnConsumed =
                                Math.Round(
                                    x.YarnConsumed,
                                    2),

                            processLoss =
                                Math.Round(
                                    x.ProcessLoss,
                                    2)
                        })
                        .ToList()
            };


            var serializer =
                new JavaScriptSerializer();


            string json =
                serializer.Serialize(payload);


            // Protect JSON when embedded inside a
            // <script> block.
            json = json
                .Replace("&", "\\u0026")
                .Replace("<", "\\u003c")
                .Replace(">", "\\u003e");


            return template
                .Replace(
                    "{{ANALYTICS_CSS}}",
                    css)

                .Replace(
                    "{{CHART_JS}}",
                    chartJs)

                .Replace(
                    "{{DASHBOARD_DATA}}",
                    json)

                .Replace(
                    "{{PAGE_JS}}",
                    pageJs);
        }
    }
}
