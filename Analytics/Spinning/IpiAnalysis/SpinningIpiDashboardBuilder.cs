using Analytics.Common;
using Analytics.Spinning.IpiAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Analytics.Spinning.IpiAnalysis
{
    public sealed class SpinningIpiDashboardBuilder
    {
        public string Build(List<SpinningIpiRow> measurements, DateTime fromDate, DateTime toDate)
        {
            string html = EmbeddedResourceLoader.ReadText("spinning-ipi-analysis.html");
            string css = EmbeddedResourceLoader.ReadText("analytics.css");
            string chartJs = EmbeddedResourceLoader.ReadText("chart.umd.min.js");
            string pageJs = EmbeddedResourceLoader.ReadText("spinning-ipi-analysis.js");

            var data = new
            {
                fromDate = fromDate.ToString("dd MMM yyyy"),
                toDate = toDate.ToString("dd MMM yyyy"),
                rows = measurements.Select(x => new
                {
                    id = x.MeasurementId,
                    date = x.TestDate.ToString("yyyy-MM-dd"),
                    merge = x.Merge,
                    yarnOrder = x.YarnOrderNumber,
                    machine = x.Machine,
                    machineDescription = x.MachineDescription,
                    nominalTex = x.NominalTex,
                    tex = x.MeasuredTex,
                    cv = x.CvPercentage,
                    thin = x.Thin,
                    thick = x.Thick,
                    nep = x.Nep,
                    ipi = x.Ipi,
                    hasYarnOrder = x.HasYarnOrder,
                    hasYarnType = x.HasYarnType,
                    hasMachine = x.HasMachine
                }).ToList()
            };

            var serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            string json = serializer.Serialize(data)
                .Replace("&", "\\u0026")
                .Replace("<", "\\u003c")
                .Replace(">", "\\u003e")
                .Replace("\u2028", "\\u2028")
                .Replace("\u2029", "\\u2029");

            string result = html.Replace("{{ANALYTICS_CSS}}", css)
                .Replace("{{CHART_JS}}", chartJs)
                .Replace("{{DASHBOARD_DATA}}", json)
                .Replace("{{PAGE_JS}}", pageJs);

            // WebView2 NavigateToString has a maximum HTML size of about 2 MB.
            // Rather than silently clipping data, ask for a shorter date range.
            if (Encoding.UTF8.GetByteCount(result) > 1800000)
                throw new InvalidOperationException("Too many IPI measurements to display at once. Please choose a shorter date range.");
            return result;
        }
    }
}
