using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ProductionPlanning
{
    public partial class frmStockBelowReorderLevel : Form
    {

        private readonly string connectionString =
    ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;

        private bool formloaded;

        private readonly HashSet<int> selectedWarehouseIds = new HashSet<int>();
        private readonly HashSet<int> selectedStyleIds = new HashSet<int>();
        private readonly HashSet<int> selectedColourIds = new HashSet<int>();
        private readonly HashSet<int> selectedSizeIds = new HashSet<int>();

        private readonly List<int> availableWarehouseIds = new List<int>();

        public frmStockBelowReorderLevel()
        {
            InitializeComponent();

            Load += frmStockBelowReorderLevel_Load;

            chkcboWarehousesSelection.CheckStateChanged +=
                chkcboWarehousesSelection_CheckStateChanged;

            chkcboStylesSelection.CheckStateChanged +=
                chkcboStylesSelection_CheckStateChanged;

            chkcboColoursSelection.CheckStateChanged +=
                chkcboColoursSelection_CheckStateChanged;

            chkcboSizesSelection.CheckStateChanged +=
                chkcboSizesSelection_CheckStateChanged;

            chkcboWarehousesSelection.SelectedIndexChanged += OpenCheckCombo;
            chkcboStylesSelection.SelectedIndexChanged += OpenCheckCombo;
            chkcboColoursSelection.SelectedIndexChanged += OpenCheckCombo;
            chkcboSizesSelection.SelectedIndexChanged += OpenCheckCombo;
        }

        private void chkcboWarehousesSelection_CheckStateChanged(
    object sender, EventArgs e)
        {
            if (!formloaded || !(sender is CheckComboBoxItem item))
                return;

            UpdateSelection(selectedWarehouseIds, item);
            UpdateWarehouseFilterText();
        }

        private void chkcboColoursSelection_CheckStateChanged(
            object sender, EventArgs e)
        {
            if (!formloaded || !(sender is CheckComboBoxItem item))
                return;

            UpdateSelection(selectedColourIds, item);
        }

        private void chkcboSizesSelection_CheckStateChanged(
            object sender, EventArgs e)
        {
            if (!formloaded || !(sender is CheckComboBoxItem item))
                return;

            UpdateSelection(selectedSizeIds, item);
        }

        private void UpdateSelection(
            HashSet<int> selectedIds,
            CheckComboBoxItem item)
        {
            if (item.CheckState)
                selectedIds.Add(item._Pk);
            else
                selectedIds.Remove(item._Pk);
        }

        private void chkcboStylesSelection_CheckStateChanged(
    object sender, EventArgs e)
        {
            if (!formloaded || !(sender is CheckComboBoxItem item))
                return;

            UpdateSelection(selectedStyleIds, item);
            ReloadApplicableColours();
        }

        private void OpenCheckCombo(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null && !combo.DroppedDown)
                combo.DroppedDown = true;
        }

        private void ReloadApplicableColours()
        {
            formloaded = false;

            selectedColourIds.Clear();
            chkcboColoursSelection.Items.Clear();

            using (var context = new TTI2Entities())
            {
                if (selectedStyleIds.Count == 0)
                {
                    LoadAllColours(context);
                }
                else
                {
                    var styleIds = selectedStyleIds.ToList();

                    var colourIds = context.TLPPS_Replenishment
                        .Where(x =>
                            styleIds.Contains(x.TLREP_Style_FK) &&
                            !x.TLREP_Discontinued)
                        .Select(x => x.TLREP_Colour_FK)
                        .Distinct()
                        .ToList();

                    var colours = context.TLADM_Colours
                        .Where(x => colourIds.Contains(x.Col_Id))
                        .OrderBy(x => x.Col_Display)
                        .ToList();

                    foreach (var colour in colours)
                    {
                        chkcboColoursSelection.Items.Add(
                            new CheckComboBoxItem(
                                colour.Col_Id,
                                colour.Col_Display,
                                false));
                    }
                }
            }

            formloaded = true;
        }

        private void frmStockBelowReorderLevel_Load(object sender, EventArgs e)
        {
            formloaded = false;

            selectedWarehouseIds.Clear();
            selectedStyleIds.Clear();
            selectedColourIds.Clear();
            selectedSizeIds.Clear();
            availableWarehouseIds.Clear();

            chkcboWarehousesSelection.Items.Clear();
            chkcboStylesSelection.Items.Clear();
            chkcboColoursSelection.Items.Clear();
            chkcboSizesSelection.Items.Clear();

            using (var context = new TTI2Entities())
            {
                var warehouses = context.TLADM_WhseStore
                    .Where(x => x.WhStore_WhseOrStore &&
                                x.WhStore_GradeA)
                    .OrderBy(x => x.WhStore_Description)
                    .ToList();

                foreach (var warehouse in warehouses)
                {
                    availableWarehouseIds.Add(warehouse.WhStore_Id);

                    chkcboWarehousesSelection.Items.Add(
                        new CheckComboBoxItem(
                            warehouse.WhStore_Id,
                            warehouse.WhStore_Description,
                            false));
                }

                var styles = context.TLADM_Styles
                    .OrderBy(x => x.Sty_Description)
                    .ToList();

                foreach (var style in styles)
                {
                    chkcboStylesSelection.Items.Add(
                        new CheckComboBoxItem(
                            style.Sty_Id,
                            style.Sty_Description,
                            false));
                }

                LoadAllColours(context);

                var sizes = context.TLADM_Sizes
                    .Where(x => (bool)!x.SI_Discontinued)
                    .OrderBy(x => x.SI_DisplayOrder)
                    .ToList();

                foreach (var size in sizes)
                {
                    chkcboSizesSelection.Items.Add(
                        new CheckComboBoxItem(
                            size.SI_id,
                            size.SI_Description,
                            false));
                }
            }

            formloaded = true;
            UpdateWarehouseFilterText();
        }

        private void LoadAllColours(TTI2Entities context)
        {
            var colours = context.TLADM_Colours
                .OrderBy(x => x.Col_Display)
                .ToList();

            foreach (var colour in colours)
            {
                chkcboColoursSelection.Items.Add(
                    new CheckComboBoxItem(
                        colour.Col_Id,
                        colour.Col_Display,
                        false));
            }
        }

        private void UpdateWarehouseFilterText()
        {
            if (selectedWarehouseIds.Count == 0)
            {
                lblWareHouseFilterText.Text =
                    "Combined SOH across all Grade A warehouses";
            }
            else if (selectedWarehouseIds.Count == 1)
            {
                lblWareHouseFilterText.Text =
                    "SOH calculated for the selected warehouse";
            }
            else
            {
                lblWareHouseFilterText.Text =
                    "SOH calculated separately for each selected warehouse";
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (!formloaded)
                return;

            bool calculatePerWarehouse =
    selectedWarehouseIds.Count > 0;

            bool showWarehouseSections =
                selectedWarehouseIds.Count > 1;

            var warehouseIds = calculatePerWarehouse
                ? selectedWarehouseIds.ToList()
                : availableWarehouseIds.ToList();

            if (warehouseIds.Count == 0)
            {
                MessageBox.Show(
                    "No Grade A warehouses are available.",
                    "Stock Below Re-order Level",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerateReport.Enabled = false;

                var reportData = GetStockBelowRolData(
                    warehouseIds,
                    selectedStyleIds.ToList(),
                    selectedColourIds.ToList(),
                    selectedSizeIds.ToList(),
                    calculatePerWarehouse);

                if (reportData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No product items were found where Stock On Hand is below the Re-order Level.",
                        "Stock Below Re-order Level",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                string warehouseDescription;
                string calculationDescription;

                if (!calculatePerWarehouse)
                {
                    warehouseDescription = "All";
                    calculationDescription =
                        "Combined SOH across all Grade A warehouses";
                }
                else
                {
                    warehouseDescription =
                        GetWarehouseDescription(warehouseIds);

                    calculationDescription =
                        selectedWarehouseIds.Count == 1
                            ? "SOH calculated for the selected warehouse"
                            : "SOH calculated separately for each selected warehouse";
                }

                string html = BuildHtmlReport(
                    reportData,
                    warehouseDescription,
                    calculationDescription,
                    showWarehouseSections);

                string reportPath = Path.Combine(
                    Path.GetTempPath(),
                    "StockBelowROL_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

                File.WriteAllText(
                    reportPath,
                    html,
                    new UTF8Encoding(true));

                Process.Start(new ProcessStartInfo
                {
                    FileName = reportPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The report could not be generated." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Stock Below Re-order Level",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnGenerateReport.Enabled = true;
            }
        }

        private DataTable GetStockBelowRolData(
            List<int> warehouseIds,
            List<int> styleIds,
            List<int> colourIds,
            List<int> sizeIds,
            bool calculatePerWarehouse)
        {
            string sql;

            if (!calculatePerWarehouse)
            {
                // No warehouse selected:
                // Combine SOH across all Grade A warehouses.
                sql = @"
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
                  AND TLSOH_WareHouse_FK IN ({WAREHOUSES})
                GROUP BY
                    TLSOH_Style_FK,
                    TLSOH_Colour_FK,
                    TLSOH_Size_FK
            )
            SELECT
                0                                           AS [Warehouse Id],
                'All'                                       AS [Warehouse],
                ISNULL(PC.ProductCode, 'NO PRODUCT CODE')   AS [Product Item],
                ST.Sty_Description                          AS [Quality / Style],
                CL.Col_Display                              AS [Colour],
                SZ.SI_Description                           AS [Size],
                R.TLREP_ReOrderLevel                        AS [Re-order Level],
                ISNULL(SOH.StockOnHand, 0)                  AS [Stock On Hand],
                R.TLREP_ReOrderLevel -
                    ISNULL(SOH.StockOnHand, 0)               AS [Shortfall]
            FROM TLPPS_Replenishment R
            INNER JOIN TLADM_Styles ST
                ON ST.Sty_Id = R.TLREP_Style_FK
            INNER JOIN TLADM_Colours CL
                ON CL.Col_Id = R.TLREP_Colour_FK
            INNER JOIN TLADM_Sizes SZ
                ON SZ.SI_id = R.TLREP_Size_FK
            LEFT JOIN StockTotals SOH
                ON SOH.TLSOH_Style_FK = R.TLREP_Style_FK
               AND SOH.TLSOH_Colour_FK = R.TLREP_Colour_FK
               AND SOH.TLSOH_Size_FK = R.TLREP_Size_FK
            OUTER APPLY
            (
                SELECT TOP 1
                    P.ProductCode
                FROM TLADM_ProductCodes P
                WHERE P.StyleId = R.TLREP_Style_FK
                  AND P.ColourId = R.TLREP_Colour_FK
                  AND P.SizeId = R.TLREP_Size_FK
                ORDER BY P.ProductCode
            ) PC
            WHERE R.TLREP_Discontinued = 0
              AND ISNULL(SOH.StockOnHand, 0) <
                  R.TLREP_ReOrderLevel";
            }
            else
            {
                // One or more warehouses selected:
                // Calculate SOH separately for every selected warehouse.
                sql = @"
            WITH SelectedWarehouses AS
            (
                SELECT
                    WhStore_Id,
                    WhStore_Description
                FROM TLADM_WhseStore
                WHERE WhStore_Id IN ({WAREHOUSES})
            ),
            StockTotals AS
            (
                SELECT
                    TLSOH_WareHouse_FK,
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
                  AND TLSOH_WareHouse_FK IN ({WAREHOUSES})
                GROUP BY
                    TLSOH_WareHouse_FK,
                    TLSOH_Style_FK,
                    TLSOH_Colour_FK,
                    TLSOH_Size_FK
            )
            SELECT
                W.WhStore_Id                               AS [Warehouse Id],
                W.WhStore_Description                      AS [Warehouse],
                ISNULL(PC.ProductCode, 'NO PRODUCT CODE') AS [Product Item],
                ST.Sty_Description                         AS [Quality / Style],
                CL.Col_Display                             AS [Colour],
                SZ.SI_Description                          AS [Size],
                R.TLREP_ReOrderLevel                       AS [Re-order Level],
                ISNULL(SOH.StockOnHand, 0)                 AS [Stock On Hand],
                R.TLREP_ReOrderLevel -
                    ISNULL(SOH.StockOnHand, 0)              AS [Shortfall]
            FROM TLPPS_Replenishment R

            CROSS JOIN SelectedWarehouses W

            INNER JOIN TLADM_Styles ST
                ON ST.Sty_Id = R.TLREP_Style_FK
            INNER JOIN TLADM_Colours CL
                ON CL.Col_Id = R.TLREP_Colour_FK
            INNER JOIN TLADM_Sizes SZ
                ON SZ.SI_id = R.TLREP_Size_FK
            LEFT JOIN StockTotals SOH
                ON SOH.TLSOH_WareHouse_FK = W.WhStore_Id
               AND SOH.TLSOH_Style_FK = R.TLREP_Style_FK
               AND SOH.TLSOH_Colour_FK = R.TLREP_Colour_FK
               AND SOH.TLSOH_Size_FK = R.TLREP_Size_FK
            OUTER APPLY
            (
                SELECT TOP 1
                    P.ProductCode
                FROM TLADM_ProductCodes P
                WHERE P.StyleId = R.TLREP_Style_FK
                  AND P.ColourId = R.TLREP_Colour_FK
                  AND P.SizeId = R.TLREP_Size_FK
                ORDER BY P.ProductCode
            ) PC
            WHERE R.TLREP_Discontinued = 0
              AND ISNULL(SOH.StockOnHand, 0) <
                  R.TLREP_ReOrderLevel";
            }

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandTimeout = 180;

                string warehouseParameters = AddIntParameters(
                    command,
                    "@Warehouse",
                    warehouseIds);

                // In the per-warehouse query the placeholder occurs twice.
                // Replace handles both occurrences with the same parameters.
                sql = sql.Replace(
                    "{WAREHOUSES}",
                    warehouseParameters);

                if (styleIds.Count > 0)
                {
                    sql += " AND R.TLREP_Style_FK IN (" +
                           AddIntParameters(
                               command,
                               "@Style",
                               styleIds) +
                           ")";
                }

                if (colourIds.Count > 0)
                {
                    sql += " AND R.TLREP_Colour_FK IN (" +
                           AddIntParameters(
                               command,
                               "@Colour",
                               colourIds) +
                           ")";
                }

                if (sizeIds.Count > 0)
                {
                    sql += " AND R.TLREP_Size_FK IN (" +
                           AddIntParameters(
                               command,
                               "@Size",
                               sizeIds) +
                           ")";
                }

                if (calculatePerWarehouse)
                {
                    sql += @"
                ORDER BY
                    W.WhStore_Description,
                    ISNULL(PC.ProductCode, 'NO PRODUCT CODE'),
                    ST.Sty_Description,
                    CL.Col_Display,
                    SZ.SI_DisplayOrder,
                    SZ.SI_Description;";
                }
                else
                {
                    sql += @"
                ORDER BY
                    ISNULL(PC.ProductCode, 'NO PRODUCT CODE'),
                    ST.Sty_Description,
                    CL.Col_Display,
                    SZ.SI_DisplayOrder,
                    SZ.SI_Description;";
                }

                command.CommandText = sql;

                var reportData = new DataTable();

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(reportData);
                }

                return reportData;
            }
        }

        private static string AddIntParameters(
    SqlCommand command,
    string parameterPrefix,
    IEnumerable<int> values)
        {
            var parameterNames = new List<string>();
            int parameterNumber = 0;

            foreach (int value in values.Distinct())
            {
                string parameterName =
                    parameterPrefix + parameterNumber;

                command.Parameters.Add(
                    parameterName,
                    SqlDbType.Int).Value = value;

                parameterNames.Add(parameterName);
                parameterNumber++;
            }

            return string.Join(", ", parameterNames);
        }

        private string GetWarehouseDescription(List<int> warehouseIds)
        {
            using (var context = new TTI2Entities())
            {
                var warehouseNames = context.TLADM_WhseStore
                    .Where(x => warehouseIds.Contains(x.WhStore_Id))
                    .OrderBy(x => x.WhStore_Description)
                    .Select(x => x.WhStore_Description)
                    .ToList();

                if (selectedWarehouseIds.Count == 0)
                    return "All Grade A warehouses";

                return string.Join(", ", warehouseNames);
            }
        }

        //VicBay blue: #325289;
        private string BuildHtmlReport(
            DataTable reportData,
            string warehouseDescription,
            string calculationDescription,
            bool showWarehouseSections)
        {
            var html = new StringBuilder();

            decimal totalRol = 0;
            decimal totalSoh = 0;
            decimal totalShortfall = 0;

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='utf-8'>");
            html.AppendLine("<title>Stock Below Re-order Level</title>");

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
                background: #ffffff;
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

.warehouse-section {
    margin-top: 24px;
    margin-bottom: 8px;
    padding: 9px 12px;
    color: white;
    background: #527d96;
    font-size: 17px;
    font-weight: bold;
    page-break-after: avoid;
}

.warehouse-total td {
    background: #dbe7ef;
    font-weight: bold;
}

.grand-total td {
    background: #325289;
    color: white;
    font-weight: bold;
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
                background: #325289;
                color: white;
                padding: 9px;
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

            tbody tr.zero-stock {
                background: #ffe6e6;
            }

            .number {
                text-align: right;
            }

            tfoot td {
                background: #dbe7ef;
                font-weight: bold;
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
                "<button class='print-button no-print' onclick='window.print()'>" +
                "Print Report</button>");

            html.AppendLine(
                "<h1>Product Items where SOH is Below ROL</h1>");

            html.AppendLine("<div class='details'>");

            html.AppendLine(
                "<strong>Warehouses:</strong> " +
                WebUtility.HtmlEncode(warehouseDescription) +
                "<br>");

            html.AppendLine(
                "<strong>Calculation:</strong> " +
                WebUtility.HtmlEncode(calculationDescription) +
                "<br>");

            html.AppendLine(
                "<strong>Report date:</strong> " +
                DateTime.Now.ToString("dd MMMM yyyy HH:mm") +
                "<br>");

            html.AppendLine(
                "<strong>Number of items:</strong> " +
                reportData.Rows.Count.ToString("N0"));

            html.AppendLine("</div>");

            var warehouseGroups = reportData.AsEnumerable()
    .GroupBy(row => new
    {
        Id = Convert.ToInt32(row["Warehouse Id"]),
        Name = Convert.ToString(row["Warehouse"])
    })
    .OrderBy(group => group.Key.Name)
    .ToList();

            foreach (var warehouseGroup in warehouseGroups)
            {
                decimal warehouseTotalRol = 0;
                decimal warehouseTotalSoh = 0;
                decimal warehouseTotalShortfall = 0;

                // This is the part missing from the current method.
                if (showWarehouseSections)
                {
                    html.AppendLine(
                        "<div class='warehouse-section'>" +
                        WebUtility.HtmlEncode(warehouseGroup.Key.Name) +
                        "</div>");
                }

                html.AppendLine("<table>");

                html.AppendLine(@"
        <thead>
            <tr>
                <th>Product Item</th>
                <th>Quality / Style</th>
                <th>Colour</th>
                <th>Size</th>
                <th class='number'>Re-order Level</th>
                <th class='number'>Stock On Hand</th>
                <th class='number'>Shortfall</th>
            </tr>
        </thead>");

                html.AppendLine("<tbody>");

                var sortedRows = warehouseGroup
                    .OrderBy(row => Convert.ToString(row["Product Item"]))
                    .ThenBy(row => Convert.ToString(row["Quality / Style"]))
                    .ThenBy(row => Convert.ToString(row["Colour"]))
                    .ThenBy(row => Convert.ToString(row["Size"]));

                foreach (DataRow row in sortedRows)
                {
                    decimal rol =
                        Convert.ToDecimal(row["Re-order Level"]);

                    decimal soh =
                        Convert.ToDecimal(row["Stock On Hand"]);

                    decimal shortfall =
                        Convert.ToDecimal(row["Shortfall"]);

                    warehouseTotalRol += rol;
                    warehouseTotalSoh += soh;
                    warehouseTotalShortfall += shortfall;

                    totalRol += rol;
                    totalSoh += soh;
                    totalShortfall += shortfall;

                    string rowClass = soh == 0
                        ? " class='zero-stock'"
                        : string.Empty;

                    html.AppendLine("<tr" + rowClass + ">");

                    html.AppendLine(
                        "<td>" + HtmlEncode(row["Product Item"]) + "</td>");

                    html.AppendLine(
                        "<td>" + HtmlEncode(row["Quality / Style"]) + "</td>");

                    html.AppendLine(
                        "<td>" + HtmlEncode(row["Colour"]) + "</td>");

                    html.AppendLine(
                        "<td>" + HtmlEncode(row["Size"]) + "</td>");

                    html.AppendLine(
                        "<td class='number'>" +
                        rol.ToString("N0") +
                        "</td>");

                    html.AppendLine(
                        "<td class='number'>" +
                        soh.ToString("N0") +
                        "</td>");

                    html.AppendLine(
                        "<td class='number'>" +
                        shortfall.ToString("N0") +
                        "</td>");

                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("<tfoot>");
                html.AppendLine("<tr class='warehouse-total'>");

                string totalDescription = showWarehouseSections
                    ? warehouseGroup.Key.Name + " Total"
                    : "Totals";

                html.AppendLine(
                    "<td colspan='4'>" +
                    WebUtility.HtmlEncode(totalDescription) +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    warehouseTotalRol.ToString("N0") +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    warehouseTotalSoh.ToString("N0") +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    warehouseTotalShortfall.ToString("N0") +
                    "</td>");

                html.AppendLine("</tr>");
                html.AppendLine("</tfoot>");
                html.AppendLine("</table>");
            }

            // Show a combined total after multiple warehouse sections.
            if (showWarehouseSections && warehouseGroups.Count > 1)
            {
                html.AppendLine("<table>");
                html.AppendLine("<tfoot>");
                html.AppendLine("<tr class='grand-total'>");
                html.AppendLine("<td colspan='4'>Grand Total</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    totalRol.ToString("N0") +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    totalSoh.ToString("N0") +
                    "</td>");

                html.AppendLine(
                    "<td class='number'>" +
                    totalShortfall.ToString("N0") +
                    "</td>");

                html.AppendLine("</tr>");
                html.AppendLine("</tfoot>");
                html.AppendLine("</table>");
            }
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
