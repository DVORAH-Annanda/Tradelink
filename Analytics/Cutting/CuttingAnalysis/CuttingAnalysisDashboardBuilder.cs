using Analytics.Common;
using Analytics.Cutting.CuttingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Analytics.Cutting.CuttingAnalysis
{
    public class CuttingAnalysisDashboardBuilder
    {
        public string Build(
            List<CuttingProductionQualityRow> productionByQuality,
            List<CuttingProductionDailyRow> productionByDay,
            List<CuttingProductionMachineRow> productionByMachine,
            List<CuttingWasteQualityRow> wasteByQuality,
            DateTime fromDate,
            DateTime toDate)
        {
            string template =
                EmbeddedResourceLoader.ReadText("cutting-analysis.html");

            string css =
                EmbeddedResourceLoader.ReadText("analytics.css");

            string chartJs =
                EmbeddedResourceLoader.ReadText("chart.umd.min.js");

            string pageJs =
                EmbeddedResourceLoader.ReadText("cutting-analysis.js");

            var payload = new
            {
                fromDate = fromDate.ToString("dd MMM yyyy"),
                toDate = toDate.ToString("dd MMM yyyy"),

                productionByQuality =
                    productionByQuality.Select(x => new
                    {
                        quality = x.Quality,
                        expectedQty = Math.Round(x.ExpectedQty, 2),
                        actualQty = Math.Round(x.ActualQty, 2),
                        varianceQty = Math.Round(x.VarianceQty, 2),
                        expectedVsActualPercentage = Math.Round(x.ExpectedVsActualPercentage, 2),
                        variancePercentage = Math.Round(x.VariancePercentage, 2)
                    }).ToList(),

                productionByDay =
                    productionByDay.Select(x => new
                    {
                        productionDate = x.ProductionDate.ToString("dd MMM yyyy"),
                        actualQty = Math.Round(x.ActualQty, 2)
                    }).ToList(),

                productionByMachine =
                    productionByMachine.Select(x => new
                    {
                        machine = x.Machine,
                        actualQty = Math.Round(x.ActualQty, 2)
                    }).ToList(),

                wasteByQuality =
                    wasteByQuality.Select(x => new
                    {
                        quality = x.Quality,
                        fabricWeight = Math.Round(x.FabricWeight, 2),
                        recordedCuttingWaste = Math.Round(x.RecordedCuttingWaste, 2),
                        cuttingWastePercentage = Math.Round(x.CuttingWastePercentage, 2),
                        recordedPanelWaste = Math.Round(x.RecordedPanelWaste, 2),
                        panelWastePercentage = Math.Round(x.PanelWastePercentage, 2),
                        totalWaste = Math.Round(x.TotalWaste, 2),
                        totalWastePercentage = Math.Round(x.TotalWastePercentage, 2)
                    }).ToList()
            };

            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(payload);

            json = json
                .Replace("&", "\\u0026")
                .Replace("<", "\\u003c")
                .Replace(">", "\\u003e");

            return template
                .Replace("{{ANALYTICS_CSS}}", css)
                .Replace("{{CHART_JS}}", chartJs)
                .Replace("{{DASHBOARD_DATA}}", json)
                .Replace("{{PAGE_JS}}", pageJs);
        }
    }
}
