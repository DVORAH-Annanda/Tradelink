using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace ProductionPlanning
{
    public class OutOfStockItemsReport
    {
        private readonly string connectionString =
ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;

        public OutOfStockItemsReport()
        {

        }

        public DataTable GetOutOfStockItems()
        {
            const string sql = @"
                WITH StockTotals AS
                (
                    SELECT
                        TLSOH_Style_FK,
                        TLSOH_Colour_FK,
                        TLSOH_Size_FK,
                        SUM(ISNULL(TLSOH_BoxedQty, 0)) AS StockOnHand
                    FROM TLCSV_StockOnHand
                    WHERE TLSOH_Picked = 0
                      AND TLSOH_Is_A = 1
                      AND TLSOH_Write_Off = 0
                      AND TLSOH_Returned = 0
                      AND TLSOH_Split = 0
                    GROUP BY
                        TLSOH_Style_FK,
                        TLSOH_Colour_FK,
                        TLSOH_Size_FK
                )
                SELECT
                    P.ProductCode              AS [Product Item],
                    ST.Sty_Description         AS [Quality / Style],
                    CL.Col_Display             AS [Colour],
                    SZ.SI_Description          AS [Size],
                    ISNULL(SOH.StockOnHand, 0) AS [Stock On Hand]
                FROM TLADM_ProductCodes P

                INNER JOIN TLADM_Styles ST
                    ON ST.Sty_Id = P.StyleId

                INNER JOIN TLADM_Colours CL
                    ON CL.Col_Id = P.ColourId

                INNER JOIN TLADM_Sizes SZ
                    ON SZ.SI_Id = P.SizeId

                LEFT JOIN StockTotals SOH
                    ON SOH.TLSOH_Style_FK = P.StyleId
                   AND SOH.TLSOH_Colour_FK = P.ColourId
                   AND SOH.TLSOH_Size_FK = P.SizeId

                WHERE ISNULL(SOH.StockOnHand, 0) = 0

                ORDER BY
                    P.ProductCode,
                    ST.Sty_Description,
                    CL.Col_Display,
                    SZ.SI_DisplayOrder,
                    SZ.SI_Description;";

            using (var connection =
                   new SqlConnection(connectionString))
            using (var command =
                   new SqlCommand(sql, connection))
            using (var adapter =
                   new SqlDataAdapter(command))
            {
                command.CommandTimeout = 180;

                var reportData = new DataTable();
                adapter.Fill(reportData);

                return reportData;
            }
        }

        public string BuildOutOfStockHtml(
            DataTable reportData)
        {
            var html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='utf-8'>");
            html.AppendLine(
                "<title>Out of Stock Product Items</title>");

            html.AppendLine(@"
                <style>
                    @page {
                        size: A4 landscape;
                        margin: 10mm;
                    }

                    body {
                        font-family: Arial, Helvetica, sans-serif;
                        margin: 24px;
                        color: #263238;
                        background: white;
                    }

                    h1 {
                        margin-bottom: 5px;
                        color: #325289;
                        font-size: 24px;
                    }

                    .details {
                        margin-bottom: 18px;
                        color: #555;
                        line-height: 1.6;
                    }

                    .print-button {
                        background: #325289;
                        color: white;
                        border: none;
                        border-radius: 4px;
                        padding: 10px 20px;
                        margin-bottom: 18px;
                        cursor: pointer;
                        font-size: 14px;
                    }

                    table {
                        width: 100%;
                        border-collapse: collapse;
                        font-size: 13px;
                    }

                    thead {
                        display: table-header-group;
                    }

                    th {
                        padding: 9px;
                        color: white;
                        background: #325289;
                        border: 1px solid #d0d7dc;
                        text-align: left;
                    }

                    td {
                        padding: 8px;
                        border: 1px solid #d0d7dc;
                    }

                    tbody tr:nth-child(even) {
                        background: #f4f7f9;
                    }

                    tbody tr:hover {
                        background: #e7f0f7;
                    }

                    .number {
                        text-align: right;
                    }

                    @media print {
                        body {
                            margin: 0;
                        }

                        .no-print {
                            display: none;
                        }

                        tr {
                            page-break-inside: avoid;
                        }
                    }
                </style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");

            html.AppendLine(
                "<button class='print-button no-print' " +
                "onclick='window.print()'>" +
                "Print / Save as PDF</button>");

            html.AppendLine(
                "<h1>Out of Stock Product Items (SOH = 0)</h1>");

            html.AppendLine("<div class='details'>");

            html.AppendLine(
                "<strong>Calculation:</strong> " +
                "Combined SOH across all warehouses<br>");

            html.AppendLine(
                "<strong>Stock type:</strong> Grade A stock<br>");

            html.AppendLine(
                "<strong>Report date:</strong> " +
                DateTime.Now.ToString("dd MMMM yyyy HH:mm") +
                "<br>");

            html.AppendLine(
                "<strong>Number of items:</strong> " +
                reportData.Rows.Count.ToString("N0"));

            html.AppendLine("</div>");

            html.AppendLine("<table>");

            html.AppendLine(@"
                <thead>
                    <tr>
                        <th>Product Item</th>
                        <th>Quality / Style</th>
                        <th>Colour</th>
                        <th>Size</th>
                        <th class='number'>Stock On Hand</th>
                    </tr>
                </thead>");

            html.AppendLine("<tbody>");

            foreach (DataRow row in reportData.Rows)
            {
                decimal soh =
                    Convert.ToDecimal(row["Stock On Hand"]);

                html.AppendLine("<tr>");

                html.AppendLine(
                    "<td>" +
                    HtmlEncode(row["Product Item"]) +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    HtmlEncode(row["Quality / Style"]) +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    HtmlEncode(row["Colour"]) +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    HtmlEncode(row["Size"]) +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    soh.ToString("N0") +
                    "</td>");

                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        private static string HtmlEncode(object value)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            return WebUtility.HtmlEncode(value.ToString());
        }
    }
}
