using Analytics.Common;
using Analytics.Spinning.QaMeasurements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Analytics.Spinning.QaMeasurements
{
    public sealed class SpinningQaDashboardBuilder
    {
        public string Build(int measureNo, List<SpinningQaMeasurementRow> measurements, DateTime fromDate, DateTime toDate)
        {
            if (measureNo != 1 && measureNo != 3)
                throw new ArgumentOutOfRangeException("measureNo");

            var data = new
            {
                kind = measureNo == 1 ? "card" : "rsb",
                fromDate = fromDate.ToString("dd MMM yyyy"),
                toDate = toDate.ToString("dd MMM yyyy"),
                // The original Spinning QA selection screen initializes these
                // values, but a QA manager must confirm the applicable limits.
                initialLowerTolerance = 4410,
                initialUpperTolerance = 4590,
                rows = measurements.Select(x => new
                {
                    id = x.Id,
                    date = x.Date.ToString("yyyy-MM-dd"),
                    machine = x.Machine,
                    machineDescription = x.MachineDescription,
                    testNo = x.TestNumber,
                    dailyAverage = x.DailyAverage,
                    cv = x.CvPercentage,
                    canister = x.Canister,
                    measurement = x.Measurement,
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

            string html = EmbeddedResourceLoader.ReadText("spinning-qa-measurements.html")
                .Replace("{{ANALYTICS_CSS}}", EmbeddedResourceLoader.ReadText("analytics.css"))
                .Replace("{{CHART_JS}}", EmbeddedResourceLoader.ReadText("chart.umd.min.js"))
                .Replace("{{DASHBOARD_DATA}}", json)
                .Replace("{{PAGE_JS}}", EmbeddedResourceLoader.ReadText("spinning-qa-measurements.js"));

            // WebView2 NavigateToString has a ~2 MB HTML limit.
            if (Encoding.UTF8.GetByteCount(html) > 1800000)
                throw new InvalidOperationException("Too many quality measurements to display at once. Choose a shorter date range.");
            return html;
        }
    }
}
