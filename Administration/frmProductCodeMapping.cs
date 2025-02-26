using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Administration
{
    public partial class frmProductCodeMapping : Form
    {

        private TTI2Entities _context = new TTI2Entities();
        public frmProductCodeMapping()
        {
            InitializeComponent();
            LoadComboBoxData();
            LoadProductCodes();
        }

        private void LoadProductCodes()
        {
            using (var context = new TTI2Entities())
            {
                dgvMapping.Rows.Clear(); // Clear previous data

                // Load product codes
                var productCodes = context.TLADM_ProductCodes.ToList();

                foreach (var product in productCodes)
                {
                    int rowIndex = dgvMapping.Rows.Add();
                    DataGridViewRow row = dgvMapping.Rows[rowIndex];

                    row.Cells["cProductCode"].Value = product.ProductCode;
                    row.Cells["cStyle"].Value = product.StyleId;   // Must match ValueMember of ComboBox
                    row.Cells["cColour"].Value = product.ColourId; // Must match ValueMember of ComboBox
                    row.Cells["cSize"].Value = product.SizeId;     // Must match ValueMember of ComboBox
                }

            }
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
            TLADM_ProductCodes productCodes = new TLADM_ProductCodes();
            using (var context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in dgvMapping.Rows)
                {
                    if (row.IsNewRow) continue; // Skip the empty new row

                    // Get values from DataGridView
                    string productCode = row.Cells["cProductCode"].Value?.ToString();
                    int styleId = Convert.ToInt32(row.Cells["cStyle"].Value);
                    int colourId = Convert.ToInt32(row.Cells["cColour"].Value);
                    int sizeId = Convert.ToInt32(row.Cells["cSize"].Value);

                    // Check if the product code already exists
                    var existingRecord = context.TLADM_ProductCodes.FirstOrDefault(p => p.ProductCode == productCode);

                    if (existingRecord != null)
                    {
                        // Update existing record
                        existingRecord.StyleId = styleId;
                        existingRecord.ColourId = colourId;
                        existingRecord.SizeId = sizeId;
                    }
                    else
                    {
                        // Add new record
                        var newRecord = new TLADM_ProductCodes
                        {
                            ProductCode = productCode,
                            StyleId = styleId,
                            ColourId = colourId,
                            SizeId = sizeId
                        };
                        context.TLADM_ProductCodes.Add(newRecord);
                    }
                }

                // Save changes to the database
                context.SaveChanges();
            }

            MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}


