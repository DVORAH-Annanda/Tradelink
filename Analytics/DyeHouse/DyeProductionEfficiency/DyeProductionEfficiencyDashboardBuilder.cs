using Analytics.Common;
using Analytics.DyeHouse.DyeProductionEfficiency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Analytics.DyeHouse.DyeProductionEfficiency
{
    public class DyeProductionEfficiencyDashboardBuilder
    {
        public string Build(List<DyeProcessLossRow> processLoss, List<DyeDiskVarianceRow> diskVariance, DateTime fromDate, DateTime toDate)
        {
            string template = EmbeddedResourceLoader.ReadText("dye-production-efficiency.html");
            string css = EmbeddedResourceLoader.ReadText("analytics.css");
            string chartJs = EmbeddedResourceLoader.ReadText("chart.umd.min.js");
            string colourPaletteJs = EmbeddedResourceLoader.ReadText("colour-palette.js");
            string pageJs = EmbeddedResourceLoader.ReadText("dye-production-efficiency.js");

            var payload = new
            {
                fromDate = fromDate.ToString("dd MMM yyyy"),
                toDate = toDate.ToString("dd MMM yyyy"),
                processLoss = processLoss.Select(x => new
                {
                    colour = x.Colour,
                    grossWeight = Math.Round(x.GrossWeight, 2),
                    nettWeight = Math.Round(x.NettWeight, 2),
                    processLossPercentage = Math.Round(x.ProcessLossPercentage, 2)
                }).ToList(),
                diskVariance = diskVariance.Select(x => new
                {
                    greigeQuality = x.GreigeQuality,
                    pieceCount = x.PieceCount,
                    averageStandardDisk = Math.Round(x.AverageStandardDisk, 2),
                    averageActualDisk = Math.Round(x.AverageActualDisk, 2),
                    diskVariancePercentage = Math.Round(x.DiskVariancePercentage, 2)
                }).ToList()
            };

            string json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(payload);
            return template.Replace("{{ANALYTICS_CSS}}", css)
                           .Replace("{{CHART_JS}}", chartJs)
                           .Replace("{{COLOUR_PALETTE_JS}}", colourPaletteJs)
                           .Replace("{{PAGE_JS}}", pageJs)
                           .Replace("{{DASHBOARD_DATA}}", json);
        }
    }
}
