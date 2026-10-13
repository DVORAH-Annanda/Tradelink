using Analytics.Common;
using Analytics.Sales.SalesAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Analytics.Sales.SalesAnalysis
{
    public class SalesAnalysisDashboardBuilder
    {
        public string Build(SalesSummary summary,
                            List<SalesMonthlyComparisonRow> monthly,
                            List<SalesPerformanceRow> customers,
                            List<SalesPerformanceRow> styles,
                            List<SalesPerformanceRow> colours,
                            SalesDataQualitySummary quality,
                            DateTime fromDate,
                            DateTime toDate)
        {
            string template = EmbeddedResourceLoader.ReadText("sales-analysis.html");
            string css = EmbeddedResourceLoader.ReadText("analytics.css");
            string chartJs = EmbeddedResourceLoader.ReadText("chart.umd.min.js");
            string colourPaletteJs = EmbeddedResourceLoader.ReadText("colour-palette.js");
            string pageJs = EmbeddedResourceLoader.ReadText("sales-analysis.js");

            var payload = new
            {
                fromDate = fromDate.ToString("dd MMM yyyy"),
                toDate = toDate.ToString("dd MMM yyyy"),
                previousFromDate = fromDate.AddYears(-1).ToString("dd MMM yyyy"),
                previousToDate = toDate.AddYears(-1).ToString("dd MMM yyyy"),
                currentYear = fromDate.Year,
                previousYear = fromDate.Year - 1,
                summary = new
                {
                    grossSales = summary.CurrentGrossSales,
                    returns = summary.CurrentReturns,
                    netSales = summary.CurrentNetSales,
                    returnRate = Math.Round(summary.CurrentReturnRate, 2),
                    previousGrossSales = summary.PreviousGrossSales,
                    previousReturns = summary.PreviousReturns,
                    previousNetSales = summary.PreviousNetSales,
                    previousReturnRate = Math.Round(summary.PreviousReturnRate, 2),
                    netGrowthPercentage = Math.Round(summary.NetGrowthPercentage, 2)
                },
                monthly = monthly.Select(x => new
                {
                    month = x.Month,
                    currentGrossSales = x.CurrentGrossSales,
                    currentReturns = x.CurrentReturns,
                    currentNetSales = x.CurrentNetSales,
                    previousGrossSales = x.PreviousGrossSales,
                    previousReturns = x.PreviousReturns,
                    previousNetSales = x.PreviousNetSales,
                    netGrowthPercentage = Math.Round(x.NetGrowthPercentage, 2)
                }).ToList(),
                customers = ToPayload(customers),
                styles = ToPayload(styles),
                colours = ToPayload(colours),
                quality = new
                {
                    soldRows = quality.SoldRows,
                    zeroOrNegativeQtyRows = quality.ZeroOrNegativeQtyRows,
                    missingMasterDataRows = quality.MissingMasterDataRows,
                    missingSoldDateRows = quality.MissingSoldDateRows,
                    latestSoldDate = quality.LatestSoldDate.HasValue ? quality.LatestSoldDate.Value.ToString("dd MMM yyyy") : "No sales recorded",
                    latestReturnDate = quality.LatestReturnDate.HasValue ? quality.LatestReturnDate.Value.ToString("dd MMM yyyy") : "No returns recorded"
                }
            };

            string json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(payload);
            return template.Replace("{{ANALYTICS_CSS}}", css)
                           .Replace("{{CHART_JS}}", chartJs)
                           .Replace("{{COLOUR_PALETTE_JS}}", colourPaletteJs)
                           .Replace("{{PAGE_JS}}", pageJs)
                           .Replace("{{DASHBOARD_DATA}}", json);
        }

        private static object ToPayload(List<SalesPerformanceRow> rows)
        {
            return rows.Select(x => new
            {
                name = x.Name,
                currentGrossSales = x.CurrentGrossSales,
                currentReturns = x.CurrentReturns,
                currentNetSales = x.CurrentNetSales,
                previousGrossSales = x.PreviousGrossSales,
                previousReturns = x.PreviousReturns,
                previousNetSales = x.PreviousNetSales,
                growthPercentage = Math.Round(x.GrowthPercentage, 2)
            }).ToList();
        }
    }
}
