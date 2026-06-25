using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using Utilities;

namespace Administration
{
    public partial class frmProductCodeMapping : Form
    {

        private TTI2Entities _context = new TTI2Entities();
        string connectionString = ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;

        public frmProductCodeMapping()
        {
            InitializeComponent();

            ConfigureMappingGrid();

            LoadComboBoxData();
            LoadProductCodes();
        }

        private void ConfigureMappingGrid()
        {
            dgvMapping.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMapping.MultiSelect = true;
            dgvMapping.AllowUserToDeleteRows = false;
            dgvMapping.EditMode = DataGridViewEditMode.EditOnEnter;

            dgvMapping.SelectionChanged += dgvMapping_SelectionChanged;
            dgvMapping.CellValidating += dgvMapping_CellValidating;
        }

        private void dgvMapping_SelectionChanged(object sender, EventArgs e)
        {
            btnDeleteProductCode.Enabled = dgvMapping.SelectedRows
                .Cast<DataGridViewRow>()
                .Any(row => !row.IsNewRow);
        }

        private void dgvMapping_CellValidating(
    object sender,
    DataGridViewCellValidatingEventArgs e)
        {
            if (dgvMapping.Columns[e.ColumnIndex].Name != "cProductCode")
                return;

            string productCode = NormaliseProductCode(e.FormattedValue);

            // Allow blank while the user is editing.
            // Save will enforce that it is required.
            if (string.IsNullOrWhiteSpace(productCode))
                return;

            DataGridViewRow duplicateRow = dgvMapping.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(row =>
                    !row.IsNewRow &&
                    row.Index != e.RowIndex &&
                    string.Equals(
                        NormaliseProductCode(row.Cells["cProductCode"].Value),
                        productCode,
                        StringComparison.OrdinalIgnoreCase));

            if (duplicateRow != null)
            {
                e.Cancel = true;

                string message =
                    $"Product Code '{productCode}' already exists in row {duplicateRow.Index + 1}.";

                dgvMapping.Rows[e.RowIndex].ErrorText = message;

                MessageBox.Show(
                    message,
                    "Duplicate Product Code",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                dgvMapping.Rows[e.RowIndex].ErrorText = string.Empty;
            }
        }

        private void LoadProductCodes()
        {
            dgvMapping.Rows.Clear(); // Clear previous data

            string query = "SELECT ProductCode, StyleId, ColourId, SizeId FROM TLADM_ProductCodes ORDER BY StyleId, ColourId, SizeId, ProductCode";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rowIndex = dgvMapping.Rows.Add();
                        DataGridViewRow row = dgvMapping.Rows[rowIndex];

                        string originalProductCode = NormaliseProductCode(reader["ProductCode"]);
                        row.Tag = originalProductCode; // Keeps the original database key
                        row.Cells["cProductCode"].Value = originalProductCode;
                        row.Cells["cStyle"].Value = reader["StyleId"];
                        row.Cells["cColour"].Value = reader["ColourId"];
                        row.Cells["cSize"].Value = reader["SizeId"];
                    }
                }
            }
        }

        private static string NormaliseProductCode(object value)
        {
            return (value?.ToString() ?? string.Empty)
                .Trim()
                .ToUpperInvariant();
        }

        private void LoadComboBoxData()
        {
            List<TLADM_Styles> styles = _context.TLADM_Styles
                .Where(s => s.Sty_Discontinued == false) // Instead of !s.Sty_Discontinued
                .ToList();

            List<TLADM_Colours> colours = _context.TLADM_Colours
                .Where(c => c.Col_Discontinued == false)
                .ToList();

            List<TLADM_Sizes> sizes = _context.TLADM_Sizes
                .Where(s => s.SI_Discontinued == false)
                .ToList();

            // Find the ComboBox columns
            var styleColumn = dgvMapping.Columns["cStyle"] as DataGridViewComboBoxColumn;
            var colourColumn = dgvMapping.Columns["cColour"] as DataGridViewComboBoxColumn;
            var sizeColumn = dgvMapping.Columns["cSize"] as DataGridViewComboBoxColumn;

            if (styleColumn != null)
            {
                styleColumn.DataSource = styles;
                styleColumn.DisplayMember = "Sty_Description"; // Adjust based on actual column name
                styleColumn.ValueMember = "Sty_Id";
            }

            if (colourColumn != null)
            {
                colourColumn.DataSource = colours;
                colourColumn.DisplayMember = "Col_Display"; // Adjust based on actual column name
                colourColumn.ValueMember = "Col_Id";
            }

            if (sizeColumn != null)
            {
                sizeColumn.DataSource = sizes;
                sizeColumn.DisplayMember = "SI_Description"; // Adjust based on actual column name
                sizeColumn.ValueMember = "SI_id";
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dgvMapping.EndEdit();

                List<ProductCodeMappingSaveItem> saveItems;

                if (!TryBuildSaveItems(out saveItems))
                    return;

                const string insertSql = @"
            INSERT INTO TLADM_ProductCodes
            (
                ProductCode,
                StyleId,
                ColourId,
                SizeId
            )
            VALUES
            (
                @ProductCode,
                @StyleId,
                @ColourId,
                @SizeId
            );";

                const string updateSql = @"
            UPDATE TLADM_ProductCodes
            SET
                ProductCode = @ProductCode,
                StyleId = @StyleId,
                ColourId = @ColourId,
                SizeId = @SizeId
            WHERE ProductCode = @OriginalProductCode;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (ProductCodeMappingSaveItem item in saveItems)
                            {
                                bool isNewRow =
                                    string.IsNullOrWhiteSpace(item.OriginalProductCode);

                                using (SqlCommand cmd = new SqlCommand(
                                    isNewRow ? insertSql : updateSql,
                                    conn,
                                    transaction))
                                {
                                    cmd.Parameters.AddWithValue(
                                        "@ProductCode",
                                        item.ProductCode);

                                    cmd.Parameters.AddWithValue(
                                        "@StyleId",
                                        item.StyleId);

                                    cmd.Parameters.AddWithValue(
                                        "@ColourId",
                                        item.ColourId);

                                    cmd.Parameters.AddWithValue(
                                        "@SizeId",
                                        item.SizeId);

                                    if (!isNewRow)
                                    {
                                        cmd.Parameters.AddWithValue(
                                            "@OriginalProductCode",
                                            item.OriginalProductCode);
                                    }

                                    int affectedRows = cmd.ExecuteNonQuery();

                                    if (!isNewRow && affectedRows == 0)
                                    {
                                        throw new InvalidOperationException(
                                            $"Product Code '{item.OriginalProductCode}' no longer exists. Please reload the screen and try again.");
                                    }
                                }
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                LoadProductCodes();

                MessageBox.Show(
                    "Data saved successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    MessageBox.Show(
                        "A Product Code with that value already exists.",
                        "Duplicate Product Code",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    $"Could not save Product Code Mapping.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not save Product Code Mapping.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnPrintProductCodes_Click(object sender, EventArgs e)
        {
            try
            {
                string html = BuildProductCodesHtmlReport();

                string reportFileName = $"ProductCodeMapping_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                string reportPath = Path.Combine(Path.GetTempPath(), reportFileName);

                File.WriteAllText(reportPath, html, Encoding.UTF8);

                Process.Start(new ProcessStartInfo
                {
                    FileName = reportPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not create the Product Code Mapping report.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Print Product Codes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string BuildProductCodesHtmlReport()
        {
            StringBuilder rows = new StringBuilder();

            string query = @"
        SELECT
            PC.ProductCode,
            S.Sty_Description AS StyleDescription,
            C.Col_Display AS ColourDescription,
            SZ.SI_Description AS SizeDescription
        FROM TLADM_ProductCodes PC
        LEFT JOIN TLADM_Styles S
            ON S.Sty_Id = PC.StyleId
        LEFT JOIN TLADM_Colours C
            ON C.Col_Id = PC.ColourId
        LEFT JOIN TLADM_Sizes SZ
            ON SZ.SI_id = PC.SizeId
        ORDER BY
            S.Sty_Description,
            C.Col_Display,
            SZ.SI_Description,
            PC.ProductCode;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productCode = HtmlEncode(reader["ProductCode"]?.ToString());
                        string style = HtmlEncode(reader["StyleDescription"]?.ToString());
                        string colour = HtmlEncode(reader["ColourDescription"]?.ToString());
                        string size = HtmlEncode(reader["SizeDescription"]?.ToString());

                        rows.AppendLine($@"
                    <tr>
                        <td>{productCode}</td>
                        <td>{style}</td>
                        <td>{colour}</td>
                        <td>{size}</td>
                    </tr>");
                    }
                }
            }

            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>Product Code Mapping</title>

    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 30px;
            color: #222;
        }}

        h1 {{
            margin-bottom: 5px;
            color: #185785;
        }}

        .report-info {{
            margin-bottom: 20px;
            color: #666;
            font-size: 13px;
        }}

        .toolbar {{
            margin-bottom: 20px;
        }}

        button {{
            background: #185785;
            color: white;
            border: none;
            padding: 9px 16px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
        }}

        button:hover {{
            opacity: 0.9;
        }}

        table {{
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }}

        th {{
            background: #185785;
            color: white;
            padding: 10px;
            text-align: left;
            cursor: pointer;
            user-select: none;
        }}

        th:hover {{
            background: #123f61;
        }}

        td {{
            border: 1px solid #d6d6d6;
            padding: 8px 10px;
        }}

        tr:nth-child(even) {{
            background: #f6f8fa;
        }}

        tr:hover {{
            background: #eaf3f8;
        }}

        .sort-note {{
            margin-top: 10px;
            font-size: 12px;
            color: #777;
        }}

        @media print {{
            body {{
                margin: 12mm;
            }}

            .toolbar,
            .sort-note {{
                display: none;
            }}

            th {{
                background: #e5e5e5 !important;
                color: black !important;
            }}

            tr:nth-child(even) {{
                background: #f8f8f8 !important;
            }}
        }}
    </style>
</head>

<body>
    <h1>Product Code Mapping</h1>

    <div class='report-info'>
        Generated: {DateTime.Now:dd MMMM yyyy HH:mm}<br />
        Total Product Codes: {dgvMapping.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow)}
    </div>

    <div class='toolbar'>
        <button onclick='window.print()'>Print / Save as PDF</button>
    </div>

    <table id='productCodeTable'>
        <thead>
            <tr>
                <th onclick='sortTable(0)'>Product Code ↕</th>
                <th onclick='sortTable(1)'>Style ↕</th>
                <th onclick='sortTable(2)'>Colour ↕</th>
                <th onclick='sortTable(3)'>Size ↕</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>

    <div class='sort-note'>
        Click any column heading to sort the table.
    </div>

    <script>
        let currentSortColumn = -1;
        let ascending = true;

        function sortTable(columnIndex) {{
            const table = document.getElementById('productCodeTable');
            const tbody = table.tBodies[0];
            const rows = Array.from(tbody.rows);

            if (currentSortColumn === columnIndex) {{
                ascending = !ascending;
            }} else {{
                currentSortColumn = columnIndex;
                ascending = true;
            }}

            rows.sort(function (rowA, rowB) {{
                const valueA = rowA.cells[columnIndex].innerText.trim();
                const valueB = rowB.cells[columnIndex].innerText.trim();

                const comparison = valueA.localeCompare(
                    valueB,
                    undefined,
                    {{
                        numeric: true,
                        sensitivity: 'base'
                    }}
                );

                return ascending ? comparison : -comparison;
            }});

            rows.forEach(function (row) {{
                tbody.appendChild(row);
            }});
        }}
    </script>
</body>
</html>";
        }

        private string HtmlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");
        }

        private void btnExportXL_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Title = "Export Product Code Mapping";
                    saveDialog.Filter = "CSV File (*.csv)|*.csv";
                    saveDialog.FileName = $"ProductCodeMapping_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string csv = BuildProductCodesCsv();

                    // UTF-8 with BOM helps Excel display special characters correctly.
                    File.WriteAllText(
                        saveDialog.FileName,
                        csv,
                        new UTF8Encoding(true)
                    );

                    MessageBox.Show(
                        "Product Code Mapping exported successfully.",
                        "Export Complete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = saveDialog.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not export the Product Code Mapping.{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string BuildProductCodesCsv()
        {
            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Product Code,Style,Colour,Size");

            string query = @"
        SELECT
            PC.ProductCode,
            S.Sty_Description AS StyleDescription,
            C.Col_Display AS ColourDescription,
            SZ.SI_Description AS SizeDescription
        FROM TLADM_ProductCodes PC
        LEFT JOIN TLADM_Styles S
            ON S.Sty_Id = PC.StyleId
        LEFT JOIN TLADM_Colours C
            ON C.Col_Id = PC.ColourId
        LEFT JOIN TLADM_Sizes SZ
            ON SZ.SI_id = PC.SizeId
        ORDER BY
            S.Sty_Description,
            C.Col_Display,
            SZ.SI_Description,
            PC.ProductCode;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        csv.AppendLine(string.Join(",",
                            EscapeCsvValue(reader["ProductCode"]?.ToString()),
                            EscapeCsvValue(reader["StyleDescription"]?.ToString()),
                            EscapeCsvValue(reader["ColourDescription"]?.ToString()),
                            EscapeCsvValue(reader["SizeDescription"]?.ToString(), forceText: true)
                        ));
                    }
                }
            }

            return csv.ToString();
        }

        private string EscapeCsvValue(string value, bool forceText = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "\"\"";

            // Makes Excel keep values such as 3-4, 1-2 and 10-12 as text.
            if (forceText)
            {
                value = "'" + value;
            }
            else if (value.StartsWith("=") ||
                     value.StartsWith("+") ||
                     value.StartsWith("-") ||
                     value.StartsWith("@"))
            {
                // Prevent Excel formula interpretation for normal columns.
                value = "'" + value;
            }

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void btnDeleteProductCode_Click(object sender, EventArgs e)
        {
            dgvMapping.EndEdit();

            List<DataGridViewRow> selectedRows = dgvMapping.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow)
                .ToList();

            if (!selectedRows.Any())
                return;

            int savedRowCount = selectedRows.Count(row =>
                !string.IsNullOrWhiteSpace(row.Tag?.ToString()));

            string message = savedRowCount > 0
                ? $"Delete {selectedRows.Count} selected Product Code mapping(s)?{Environment.NewLine}{Environment.NewLine}This cannot be undone."
                : $"Remove {selectedRows.Count} unsaved row(s)?";

            if (MessageBox.Show(
                message,
                "Delete Product Code Mapping",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                List<DataGridViewRow> savedRows = selectedRows
                    .Where(row => !string.IsNullOrWhiteSpace(row.Tag?.ToString()))
                    .ToList();

                if (savedRows.Any())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                foreach (DataGridViewRow row in savedRows)
                                {
                                    using (SqlCommand cmd = new SqlCommand(
                                        @"DELETE FROM TLADM_ProductCodes
                                  WHERE ProductCode = @ProductCode;",
                                        conn,
                                        transaction))
                                    {
                                        cmd.Parameters.AddWithValue(
                                            "@ProductCode",
                                            row.Tag.ToString());

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }

                foreach (DataGridViewRow row in selectedRows
                    .OrderByDescending(row => row.Index))
                {
                    dgvMapping.Rows.Remove(row);
                }

                MessageBox.Show(
                    "Selected Product Code mapping(s) deleted.",
                    "Deleted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not delete the selected Product Code mapping(s).{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool TryGetIntValue(
    DataGridViewRow row,
    string columnName,
    out int value)
        {
            value = 0;

            object cellValue = row.Cells[columnName].Value;

            return cellValue != null &&
                   cellValue != DBNull.Value &&
                   int.TryParse(cellValue.ToString(), out value);
        }

        private bool ShowGridValidationError(
            DataGridViewRow row,
            string columnName,
            string message)
        {
            row.ErrorText = message;

            dgvMapping.ClearSelection();
            row.Selected = true;
            dgvMapping.CurrentCell = row.Cells[columnName];

            MessageBox.Show(
                message,
                "Product Code Mapping",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private bool TryBuildSaveItems(
            out List<ProductCodeMappingSaveItem> saveItems)
        {
            saveItems = new List<ProductCodeMappingSaveItem>();

            HashSet<string> usedProductCodes =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataGridViewRow row in dgvMapping.Rows)
            {
                if (row.IsNewRow)
                    continue;

                row.ErrorText = string.Empty;

                string productCode =
                    NormaliseProductCode(row.Cells["cProductCode"].Value);

                if (string.IsNullOrWhiteSpace(productCode))
                {
                    return ShowGridValidationError(
                        row,
                        "cProductCode",
                        $"Row {row.Index + 1}: Product Code is required.");
                }

                if (!usedProductCodes.Add(productCode))
                {
                    return ShowGridValidationError(
                        row,
                        "cProductCode",
                        $"Row {row.Index + 1}: Product Code '{productCode}' appears more than once.");
                }

                int styleId;
                int colourId;
                int sizeId;

                if (!TryGetIntValue(row, "cStyle", out styleId))
                {
                    return ShowGridValidationError(
                        row,
                        "cStyle",
                        $"Row {row.Index + 1}: Please select a Style.");
                }

                if (!TryGetIntValue(row, "cColour", out colourId))
                {
                    return ShowGridValidationError(
                        row,
                        "cColour",
                        $"Row {row.Index + 1}: Please select a Colour.");
                }

                if (!TryGetIntValue(row, "cSize", out sizeId))
                {
                    return ShowGridValidationError(
                        row,
                        "cSize",
                        $"Row {row.Index + 1}: Please select a Size.");
                }

                // Ensure the grid also displays the normalised value.
                row.Cells["cProductCode"].Value = productCode;

                saveItems.Add(new ProductCodeMappingSaveItem
                {
                    GridRow = row,
                    OriginalProductCode = row.Tag?.ToString(),
                    ProductCode = productCode,
                    StyleId = styleId,
                    ColourId = colourId,
                    SizeId = sizeId
                });
            }

            return true;
        }

        private sealed class ProductCodeMappingSaveItem
        {
            public DataGridViewRow GridRow { get; set; }
            public string OriginalProductCode { get; set; }
            public string ProductCode { get; set; }
            public int StyleId { get; set; }
            public int ColourId { get; set; }
            public int SizeId { get; set; }
        }
    }
}


