using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Text;
using Utilities;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Spinning
{
    public static class rptCottonDeliveryVariance
    {
        // Helper for safe percentage
        private static decimal SafePct(decimal diff, decimal baseVal)
            => baseVal == 0m ? 0m : (diff / baseVal) * 100m;

        public static string BuildSingle(int grnNo)
        {
            using (var ctx = new TTI2Entities())
            {
                var hit = (
                    from t in ctx.TLSPN_CottonTransactions
                    join s in ctx.TLADM_Cotton on t.cotrx_Supplier_FK equals s.Cotton_Pk
                    where t.cotrx_Return_No == grnNo
                    select new
                    {
                        Tx = t,
                        SupplierName = s.Cotton_Description
                    }
                ).FirstOrDefault();
                if (hit == null) throw new Exception($"GRN {grnNo} not found.");

                var cts = hit.Tx;
                var supplierName = hit.SupplierName;
                var lot = cts.cotrx_LotNo;
                var date = cts.cotrx_TransDate;

                // Pull only SAMPLE rows saved for this lot (we treat presence of sample fields as the marker)
                var sampleRows = ctx.TLSPN_CottonReceivedBales
                    .Where(b => b.CotBales_LotNo == lot &&
                                (b.CotBales_Sample_BaleNo != null || b.CotBales_Sample_Weight_Gross != null))
                    .Select(b => new Row
                    {
                        BaleNo = b.CotBales_Sample_BaleNo ?? b.CotBales_BaleNo.ToString(),
                        SupplierGrossKg = b.CotBales_Weight_Gross,          // plain decimal
                        TtsGrossKg = b.CotBales_Sample_Weight_Gross ?? 0m,        // plain decimal
                        Comments = b.OverrideReason
                    })
                    .ToList();

                sampleRows = sampleRows
    .OrderBy(r => SortKey(r.BaleNo))   // numeric first when possible
    .ThenBy(r => r.BaleNo)             // tie-breaker
    .ToList();

                var supplierSum = sampleRows.Sum(r => r.SupplierGrossKg);
                var ttsSum = sampleRows.Sum(r => r.TtsGrossKg);
                var diffSum = ttsSum - supplierSum;
                var pctSum = SafePct(diffSum, supplierSum);

                var sb = new StringBuilder();
                sb.Append(HtmlHead("COTTON DELIVERY WEIGHT VARIANCE REPORT"));

                sb.Append(SectionHeader(cts.cotrx_Return_No, lot, date, supplierName,
                                        cts.cotrx_NoBales ?? 0, cts.cotrx_GrossWeight));

                sb.Append(TableHeader());

                foreach (var r in sampleRows)
                {
                    var diff = r.TtsGrossKg - r.SupplierGrossKg;
                    var pct = SafePct(diff, r.SupplierGrossKg);
                    sb.Append(TableRow(
                        r.BaleNo,
                        r.SupplierGrossKg,
                        r.TtsGrossKg,
                        diff,
                        pct,
                        r.Comments));
                }

                sb.Append(TotalsRow(supplierSum, ttsSum, diffSum, pctSum));

                sb.Append(HtmlFoot());
                return sb.ToString();
            }
        }

        private static int SortKey(string s)
        {
            return int.TryParse(s, out var n) ? n : int.MaxValue;
        }

        public static string BuildRange(DateTime from, DateTime to, bool groupBySupplier)
        {
            using (var ctx = new TTI2Entities())
            {

                var start = from.Date;
                var end = to.Date.AddDays(1).AddTicks(-1);
                // Get deliveries in range
                var deliveries = (
                    from t in ctx.TLSPN_CottonTransactions
                    join s in ctx.TLADM_Cotton on t.cotrx_Supplier_FK equals s.Cotton_Pk
                    where t.cotrx_TransDate >= start && t.cotrx_TransDate <= end
                    select new
                    {
                        t.cotrx_Return_No,
                        t.cotrx_LotNo,
                        t.cotrx_TransDate,
                        t.cotrx_NoBales,
                        t.cotrx_GrossWeight,
                        Supplier = s.Cotton_Description
                    }
                ).ToList();

                var sb = new StringBuilder();
                sb.Append(HtmlHead("COTTON DELIVERY WEIGHT VARIANCE REPORT"));

                if (groupBySupplier)
                {
                    foreach (var grp in deliveries.GroupBy(d => d.Supplier).OrderBy(g => g.Key))
                    {
                        decimal gSupplierSum = 0, gTtsSum = 0;

                        sb.Append($@"<h2>Supplier: {Escape(grp.Key)}</h2>");

                        foreach (var d in grp.OrderBy(x => x.cotrx_TransDate))
                        {
                            var rows = LoadSampleRows(ctx, d.cotrx_LotNo);
                            var supplierSum = rows.Sum(r => r.SupplierGrossKg);
                            var ttsSum = rows.Sum(r => r.TtsGrossKg);
                            var diffSum = ttsSum - supplierSum;
                            var pctSum = SafePct(diffSum, supplierSum);

                            gSupplierSum += supplierSum;
                            gTtsSum += ttsSum;

                            sb.Append(SectionHeader(d.cotrx_Return_No, d.cotrx_LotNo, d.cotrx_TransDate, d.Supplier,
                                                    d.cotrx_NoBales ?? 0, d.cotrx_GrossWeight));
                            sb.Append(TableHeader());
                            foreach (var r in rows)
                            {
                                var diff = r.TtsGrossKg - r.SupplierGrossKg;
                                var pct = SafePct(diff, r.SupplierGrossKg);
                                sb.Append(TableRow(r.BaleNo, r.SupplierGrossKg, r.TtsGrossKg, diff, pct, r.Comments));
                            }
                            sb.Append(TotalsRow(supplierSum, ttsSum, diffSum, pctSum));
                        }

                        // Supplier subtotal
                        var gDiff = gTtsSum - gSupplierSum;
                        var gPct = SafePct(gDiff, gSupplierSum);
                        sb.Append($@"
                            <div class=""supplier-total"">
                                <table class=""totals"">
                                    <tr>
                                        <td class=""lbl"">Supplier subtotal:</td>
                                        <td class=""num"">{gSupplierSum:N1}</td>
                                        <td class=""num"">{gTtsSum:N1}</td>
                                        <td class=""num"">{gDiff:N1}</td>
                                        <td class=""num"">{gPct:N2}%</td>
                                    </tr>
                                </table>
                            </div>
                        ");
                    }
                }
                else
                {
                    foreach (var d in deliveries.OrderBy(x => x.cotrx_TransDate).ThenBy(x => x.Supplier))
                    {
                        var rows = LoadSampleRows(ctx, d.cotrx_LotNo);
                        var supplierSum = rows.Sum(r => r.SupplierGrossKg);
                        var ttsSum = rows.Sum(r => r.TtsGrossKg);
                        var diffSum = ttsSum - supplierSum;
                        var pctSum = SafePct(diffSum, supplierSum);

                        sb.Append(SectionHeader(d.cotrx_Return_No, d.cotrx_LotNo, d.cotrx_TransDate, d.Supplier,
                                                d.cotrx_NoBales ?? 0, d.cotrx_GrossWeight));
                        sb.Append(TableHeader());
                        foreach (var r in rows)
                        {
                            var diff = r.TtsGrossKg - r.SupplierGrossKg;
                            var pct = SafePct(diff, r.SupplierGrossKg);
                            sb.Append(TableRow(r.BaleNo, r.SupplierGrossKg, r.TtsGrossKg, diff, pct, r.Comments));
                        }
                        sb.Append(TotalsRow(supplierSum, ttsSum, diffSum, pctSum));
                    }
                }

                sb.Append(HtmlFoot());
                return sb.ToString();
            }
        }

        // --- helpers ---------------------------------------------------------

        private static List<Row> LoadSampleRows(TTI2Entities ctx, int lotNo)
        {




            return ctx.TLSPN_CottonReceivedBales
    .Where(b => b.CotBales_LotNo == lotNo &&
                (b.CotBales_Sample_BaleNo != null || b.CotBales_Sample_Weight_Gross != null))
    .Select(b => new Row
    {
        BaleNo = b.CotBales_Sample_BaleNo ?? b.CotBales_BaleNo.ToString(),
        SupplierGrossKg = b.CotBales_Weight_Gross,          // plain decimal
        TtsGrossKg = b.CotBales_Sample_Weight_Gross ?? 0m,        // plain decimal
        Comments = b.OverrideReason
    })
    // nice numeric order when possible  .OrderBy(r => r.TryBaleNoNumeric)
    .ToList();
        }

        private static string HtmlHead(string title) => $@"
<!DOCTYPE html>
<html>
<head>
<meta charset=""utf-8"">
<title>{Escape(title)}</title>
<style>
    body {{ font-family: Segoe UI, Arial, sans-serif; margin: 20px; }}
    h1 {{ margin: 0 0 10px 0; }}
    h2 {{ margin: 24px 0 6px 0; border-bottom: 1px solid #ccc; padding-bottom: 4px; }}
    .meta {{ margin: 6px 0 10px 0; color:#333; }}
    table {{ border-collapse: collapse; width: 100%; }}
    th, td {{ border: 1px solid #ddd; padding: 6px 8px; }}
    th {{ background: #f5f5f5; text-align: left; }}
    td.num {{ text-align: right; white-space: nowrap; }}
    .totals td {{ font-weight: 600; background: #fafafa; }}
    .supplier-total .totals {{ margin-top: 6px; }}
    @media print {{
        body {{ margin: 0.5in; }}
        a[href]:after {{ content: """"; }}
    }}
</style>
</head>
<body>
<h1>{Escape(title)}</h1>
";

        private static string HtmlFoot() => "</body></html>";

        private static string SectionHeader(int grn, int lot, DateTime? received, string supplier, int totalBales, decimal supplierGross)
        {
            string date = received?.ToString("dd/MM/yy") ?? "";
            return $@"
<div class=""meta"">
    <strong>GRN No:</strong> {grn} &nbsp;&nbsp;
    <strong>Lot no.:</strong> {lot} &nbsp;&nbsp;
    <strong>Date received:</strong> {Escape(date)} &nbsp;&nbsp;
    <strong>Supplier:</strong> {Escape(supplier)}<br/>
    <strong>Total bales received:</strong> {totalBales} &nbsp;&nbsp;
    <strong>Total supplier gross weight (kg):</strong> {supplierGross:N1}
</div>
";
        }

        private static string TableHeader() => @"
<table>
    <thead>
        <tr>
            <th style=""width:110px"">Bale no.</th>
            <th class=""num"">Supplier (kg)</th>
            <th class=""num"">TTS (kg)</th>
            <th class=""num"">Difference (kg)</th>
            <th class=""num"">Difference (%)</th>
            <th>Comments</th>
        </tr>
    </thead>
    <tbody>
";

        private static string TableRow(string bale, decimal supp, decimal tts, decimal diff, decimal pct, string comments)
        {
            return $@"
        <tr>
            <td>{Escape(bale)}</td>
            <td class=""num"">{supp:N1}</td>
            <td class=""num"">{tts:N1}</td>
            <td class=""num"">{diff:N1}</td>
            <td class=""num"">{pct:N2}%</td>
            <td>{Escape(comments)}</td>
        </tr>";
        }

        private static string TotalsRow(decimal suppSum, decimal ttsSum, decimal diffSum, decimal pctSum)
        {
            return $@"
    </tbody>
    <tfoot>
        <tr class=""totals"">
            <td style=""text-align:right""><strong>Totals:</strong></td>
            <td class=""num"">{suppSum:N1}</td>
            <td class=""num"">{ttsSum:N1}</td>
            <td class=""num"">{diffSum:N1}</td>
            <td class=""num"">{pctSum:N2}%</td>
            <td></td>
        </tr>
    </tfoot>
</table>
";
        }

        private static string Escape(string s) =>
            string.IsNullOrEmpty(s) ? "" : System.Net.WebUtility.HtmlEncode(s);

        private sealed class Row
        {
            public string BaleNo { get; set; }
            public decimal SupplierGrossKg { get; set; }
            public decimal TtsGrossKg { get; set; }
            public string Comments { get; set; }

            // helps stable numeric ordering when bale no is a number
            public int TryBaleNoNumeric
            {
                get
                {
                    if (int.TryParse(BaleNo, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n))
                        return n;
                    return int.MaxValue;
                }
            }
        }
    }
}
