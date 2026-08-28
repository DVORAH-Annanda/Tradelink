using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Analytics.Common;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public class CMTCompletedWorkDashboardBuilder
    {
        public string Build(
            CMTCompletedWorkSummary summary,
            List<CMTBGradeByStyle> bGradeByStyle,
            List<CMTMnffOspecByStyle> mnffAndOspec,
            List<CMTBGradeHolesByMachine> holesByMachine,
            List<CMTSpinningByYarnType> spinningByYarnType,
            DateTime fromDate,
            DateTime toDate)
        {
            string template =
                EmbeddedResourceLoader.ReadText(
                    "cmt-completed-work-analysis.html");


            string css =
                EmbeddedResourceLoader.ReadText(
                    "analytics.css");


            string chartJs =
                EmbeddedResourceLoader.ReadText(
                    "chart.umd.min.js");


            string pageJs =
                EmbeddedResourceLoader.ReadText(
                    "cmt-completed-work-analysis.js");


            var payload = new
            {
                fromDate =
                    fromDate.ToString(
                        "dd MMM yyyy"),

                toDate =
                    toDate.ToString(
                        "dd MMM yyyy"),

                summary = new
                {
                    totalUnits =
                        summary.TotalUnits,

                    aGrade =
                        summary.AGrade,

                    bGrade =
                        summary.BGrade,

                    bGradePercentage =
                        Math.Round(
                            summary.BGradePercentage,
                            2)
                },

                bGradeByStyle =
                    bGradeByStyle
                        .Select(x => new
                        {
                            style =
                                x.Style,

                            totalUnits =
                                x.TotalUnits,

                            aGrade =
                                x.AGrade,

                            bGrade =
                                x.BGrade,

                            bGradePercentage =
                                Math.Round(
                                    x.BGradePercentage,
                                    2)
                        })
                        .ToList(),

                mnffAndOspec =
    mnffAndOspec
        .Select(x => new
        {
            style = x.Style,

            totalUnits =
                x.TotalUnits,

            mnff =
                x.MNFF,

            ospec =
                x.Ospec
        })
        .ToList(),

                holesByMachine =
    holesByMachine
        .Select(x => new
        {
            machine =
                x.KnittingMachine,

            totalUnits =
                x.TotalUnits,

            holes =
                x.Holes,

            holesPercentage =
                Math.Round(
                    x.HolesPercentage,
                    2)
        })
        .ToList(),

                spinningByYarnType =
    spinningByYarnType
        .Select(x => new
        {
            yarnType =
                x.YarnType,

            totalUnits =
                x.TotalUnits,

            barreLines =
                x.BarreLines,

            fflaw =
                x.Fflaw,

            contam =
                x.Contam,

            spinningBGradePercentage =
                Math.Round(
                    x.SpinningBGradePercentage,
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