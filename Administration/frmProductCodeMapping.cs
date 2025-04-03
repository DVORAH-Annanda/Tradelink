using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
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
            LoadComboBoxData();
            LoadProductCodes();
        }

        private void LoadProductCodes()
        {
            dgvMapping.Rows.Clear(); // Clear previous data

            string query = "SELECT ProductCode, StyleId, ColourId, SizeId FROM TLADM_ProductCodes";

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

                        row.Cells["cProductCode"].Value = reader["ProductCode"].ToString();
                        row.Cells["cStyle"].Value = reader["StyleId"];
                        row.Cells["cColour"].Value = reader["ColourId"];
                        row.Cells["cSize"].Value = reader["SizeId"];
                    }
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dgvMapping.Rows)
                {
                    if (row.IsNewRow) continue; // Skip empty new row

                    string productCode = row.Cells["cProductCode"].Value?.ToString().ToUpper();
                    int styleId = Convert.ToInt32(row.Cells["cStyle"].Value);
                    int colourId = Convert.ToInt32(row.Cells["cColour"].Value);
                    int sizeId = Convert.ToInt32(row.Cells["cSize"].Value);

                    // Check if the product code exists
                    string checkQuery = "SELECT COUNT(*) FROM TLADM_ProductCodes WHERE ProductCode = @ProductCode";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ProductCode", productCode);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Update existing record
                            string updateQuery = "UPDATE TLADM_ProductCodes SET StyleId = @StyleId, ColourId = @ColourId, SizeId = @SizeId WHERE ProductCode = @ProductCode";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@ProductCode", productCode);
                                updateCmd.Parameters.AddWithValue("@StyleId", styleId);
                                updateCmd.Parameters.AddWithValue("@ColourId", colourId);
                                updateCmd.Parameters.AddWithValue("@SizeId", sizeId);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Insert new record
                            string insertQuery = "INSERT INTO TLADM_ProductCodes (ProductCode, StyleId, ColourId, SizeId) VALUES (@ProductCode, @StyleId, @ColourId, @SizeId)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@ProductCode", productCode);
                                insertCmd.Parameters.AddWithValue("@StyleId", styleId);
                                insertCmd.Parameters.AddWithValue("@ColourId", colourId);
                                insertCmd.Parameters.AddWithValue("@SizeId", sizeId);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


