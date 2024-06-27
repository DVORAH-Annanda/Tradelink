using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Utilities;

using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
namespace DyeHouse
{
    public partial class frmDyeRFD : Form
    {
        protected readonly TTI2Entities _context = new TTI2Entities();
        DataTable dtQuantitiesSizes;
        DataTable dtGarmentsAvailable;
        TLADM_LastNumberUsed lastBatchNo;
        bool formLoaded;

        private int totalSelectedQuantity = 0;

        public frmDyeRFD()
        {
            InitializeComponent();
            SetupDataTables();
            //SetupDataGrids();
        }

        private void SetupDataTables()
        {
            SetupQuantitiesSizesDataTable();
            SetupGarmentsAvailableDataTable();
        }

        private void SetupDataGrids()
        {
            SetupQuantitiesSizesDataGrid();
            SetupGarmentsAvailableDataGrid();
        }

        private void SetupQuantitiesSizesDataTable()
        {
            dtQuantitiesSizes = new DataTable();

            dtQuantitiesSizes.Columns.Add(new DataColumn("RFD_Pk", typeof(int)) { DefaultValue = 0 });
            dtQuantitiesSizes.Columns.Add(new DataColumn("SizeId", typeof(int)) { DefaultValue = 0 });
            dtQuantitiesSizes.Columns.Add(new DataColumn("Quantity", typeof(int)) { DefaultValue = 0 });
            dtQuantitiesSizes.Columns.Add(new DataColumn("Outstanding", typeof(int)) { DefaultValue = 0 });
        }

        private void SetupQuantitiesSizesDataGrid()
        {
            dgvSizesQuantities.Columns.Clear();

            using (var context = new TTI2Entities())
            {
                dgvSizesQuantities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pk", DataPropertyName = "RFD_Pk", Visible = false });
                dgvSizesQuantities.Columns.Add(new DataGridViewComboBoxColumn
                {
                    DataSource = context.TLADM_Sizes.Where(x => !x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList(),
                    HeaderText = "Size",
                    ValueMember = "SI_id",
                    DisplayMember = "SI_Display",
                    DataPropertyName = "SizeId",
                    Name = "Size"
                });
                dgvSizesQuantities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", DataPropertyName = "Quantity" });
                dgvSizesQuantities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Outstanding", DataPropertyName = "Outstanding", ReadOnly = true });

                dgvSizesQuantities.CellEndEdit += dgvSizesQuantities_CellEndEdit;
                dgvSizesQuantities.EditingControlShowing += dgvSizesQuantities_EditingControlShowing;
                dgvSizesQuantities.CellEnter += dgvSizesQuantities_CellEnter;
                dgvSizesQuantities.CurrentCellDirtyStateChanged += dgvSizesQuantities_CurrentCellDirtyStateChanged;
                dgvSizesQuantities.CellValueChanged += dgvSizesQuantities_CellValueChanged;
            }
        }

        private void dgvSizesQuantities_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                dgvSizesQuantities.BeginEdit(true);
                if (dgvSizesQuantities.EditingControl is ComboBox comboBox)
                {
                    comboBox.DroppedDown = true;
                }
            }
        }

        private void dgvSizesQuantities_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvSizesQuantities.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvSizesQuantities.CurrentCell.ColumnIndex == 1)
            {
                var comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    var selectedSizeId = comboBox.SelectedValue;
                    dgvSizesQuantities.CurrentCell.Value = selectedSizeId;
                    dgvSizesQuantities.EndEdit();
                    dgvSizesQuantities.CurrentCell = dgvSizesQuantities.Rows[dgvSizesQuantities.CurrentCell.RowIndex].Cells[2]; // Move to Quantity cell
                   // dgvSizesQuantities.BeginEdit(true);
                }
            }
        }

        private void dgvSizesQuantities_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSizesQuantities.IsCurrentCellDirty)
            {
                dgvSizesQuantities.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvSizesQuantities_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0) // Quantity column
            {
                int quantity = Convert.ToInt32(dgvSizesQuantities.Rows[e.RowIndex].Cells[2].Value);
                dgvSizesQuantities.Rows[e.RowIndex].Cells[3].Value = quantity; // Set Outstanding to Quantity value
                UpdateOutstanding();
            }
        }

        private void dgvSizesQuantities_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0) // Size column
            {
                TLADM_Styles selectedStyle = GetSelectedStyle();
                TLADM_Sizes selectedSize = GetSelectedSize(e.RowIndex);

                if (selectedStyle != null && selectedSize != null)
                {
                    LoadGarmentsAvailable(selectedStyle, selectedSize);
                }
            }

            if (e.ColumnIndex == 2 && e.RowIndex >= 0) // Quantity column
            {
                UpdateOutstanding();
            }
        }

        //private void dgvSizesQuantities_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.ColumnIndex == 2)
        //    {
        //        dgvSizesQuantities.BeginEdit(true);
        //    }
        //}

        private void UpdateOutstanding()
        {
            totalSelectedQuantity = 0;

            foreach (DataGridViewRow row in dgvGarmentsAvailable.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    int selectedQuantity = Convert.ToInt32(row.Cells["ToBatchQty"].Value);
                    totalSelectedQuantity += selectedQuantity;
                }
            }

            foreach (DataGridViewRow row in dgvSizesQuantities.Rows)
            {
                if (row.Cells["Outstanding"].Value != null)
                {
                    int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    row.Cells["Outstanding"].Value = currentQuantity - totalSelectedQuantity;
                }
            }
        }

        private TLADM_Styles GetSelectedStyle()
        {
            if (cmboStyles.SelectedItem != null)
            {
                return (TLADM_Styles)cmboStyles.SelectedItem;
            }
            else
            {
                // Handle the case when no style is selected, such as showing an error message or returning a default style
                MessageBox.Show("Please select a style.");
                return null;
            }
        }

        private TLADM_Sizes GetSelectedSize(int rowIndex)
        {
            // Check if the row index is valid
            if (rowIndex >= 0 && rowIndex < dgvSizesQuantities.Rows.Count && dgvSizesQuantities.Rows[rowIndex].Cells[1].Value != null)
            {
                // Get the value of the "Size" column cell at the specified row index
                int selectedSize = (int)dgvSizesQuantities.Rows[rowIndex].Cells[1].Value;
                return _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == selectedSize);
                
            }

            return null; // Return null if the row index is invalid or size is not found
        }

        private void SetupGarmentsAvailableDataTable()
        {
            dtGarmentsAvailable = new DataTable();

            dtGarmentsAvailable.Columns.Add(new DataColumn("GD_pk", typeof(int)) { DefaultValue = 0 });
            dtGarmentsAvailable.Columns.Add(new DataColumn("GD_Select", typeof(bool)) { DefaultValue = false });
            dtGarmentsAvailable.Columns.Add(new DataColumn("GD_BoxNo", typeof(string)) { DefaultValue = string.Empty });
            dtGarmentsAvailable.Columns.Add(new DataColumn("GD_Size", typeof(string)) { DefaultValue = string.Empty });
            dtGarmentsAvailable.Columns.Add(new DataColumn("GD_Quantity", typeof(int)) { DefaultValue = 0 });
            dtGarmentsAvailable.Columns.Add(new DataColumn("SplitBox", typeof(bool)) { DefaultValue = false });
            dtGarmentsAvailable.Columns.Add(new DataColumn("QtyToBatch", typeof(int)) { DefaultValue = 0 });
            dtGarmentsAvailable.Columns.Add(new DataColumn("QtyToStock", typeof(int)) { DefaultValue = 0 });
        }

        private void SetupGarmentsAvailableDataGrid()
        {
            dgvGarmentsAvailable.AutoGenerateColumns = false;
            dgvGarmentsAvailable.AllowUserToAddRows = false;

            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "Key", DataPropertyName = "GD_pk", Visible = false });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Select", DataPropertyName = "GD_Select" });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNumber", DataPropertyName = "GD_BoxNo", ReadOnly = true });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", DataPropertyName = "GD_Size", ReadOnly = true });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxedQty", DataPropertyName = "GD_Quantity", ReadOnly = true });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewCheckBoxColumn { Name = "SplitBox", DataPropertyName = "SplitBox" });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "ToBatchQty", DataPropertyName = "QtyToBatch" });
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockQty", DataPropertyName = "QtyToStock", ReadOnly = true });

            BindingSource bSrc = new BindingSource { DataSource = dtGarmentsAvailable };
            dgvGarmentsAvailable.DataSource = bSrc;

            dgvGarmentsAvailable.CellEndEdit += dgvGarmentsAvailable_CellEndEdit;
            dgvGarmentsAvailable.CellValueChanged += dgvGarmentsAvailable_CellValueChanged;
            dgvGarmentsAvailable.CurrentCellDirtyStateChanged += dgvGarmentsAvailable_CurrentCellDirtyStateChanged;
        }

        private void dgvGarmentsAvailable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvGarmentsAvailable.IsCurrentCellDirty)
            {
                dgvGarmentsAvailable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvGarmentsAvailable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1) // Select column
                {
                    UpdateOutstanding();
                }

                if (e.ColumnIndex == 6) // Quantity column
                {
                    int qtyToBatch = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[6].Value);
                    int selectedQuantity = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[4].Value);
                    dgvGarmentsAvailable.Rows[e.RowIndex].Cells[5].Value = true;
                    // Update QtyToStock column
                    dgvGarmentsAvailable.Rows[e.RowIndex].Cells[7].Value = selectedQuantity - qtyToBatch;
                    
                    UpdateOutstanding();
                }
            }
        }

        private void frmDyeRFD_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            formLoaded = false;
            //bool RepackTransaction = false;

            LoadFormData();

            formLoaded = true;
            Cursor.Current = Cursors.Default;
        }

        private void LoadFormData()
        {      
            lastBatchNo = _context.TLADM_LastNumberUsed.Find(3);
            if (lastBatchNo != null)
            {
                if (lastBatchNo.col14 == 0)
                {
                    lastBatchNo.col14 = 1;
                }

                txtBatchNumber.Text = "GD" + lastBatchNo.col14.ToString().PadLeft(5, '0');
            }

            cmbColours.DataSource = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
            cmbColours.DisplayMember = "Col_Display";
            cmbColours.ValueMember = "Col_Id";
            cmbColours.SelectedValue = -1;

            cmboStyles.DataSource = _context.TLADM_Styles.Where(x => x.Sty_PFD).OrderBy(x => x.Sty_Description).ToList();
            cmboStyles.DisplayMember = "Sty_Description";
            cmboStyles.ValueMember = "Sty_Id";
            cmboStyles.SelectedValue = -1;

            SetupDataGrids();

      
        }

        private void dgvGarmentsAvailable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Select column
            {
                UpdateOutstanding();
            }

        }

        private void dgvGarmentsAvailable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6) // QtyToBatch column
            {
                int qtyToBatch = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[6].Value);
                int selectedQuantity = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[4].Value);

                // Update QtyToStock column
                dgvGarmentsAvailable.Rows[e.RowIndex].Cells[7].Value = selectedQuantity - qtyToBatch;

 
                UpdateOutstanding();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!formLoaded)
                return;

            // Validate Colour selection
            TLADM_Colours ColourSelected = (TLADM_Colours)cmbColours.SelectedItem;
            if (ColourSelected == null)
            {
                MessageBox.Show("Please select a colour");
                return;
            }

            // Validate Style selection
            TLADM_Styles StyleSelected = (TLADM_Styles)cmboStyles.SelectedItem;
            if (StyleSelected == null)
            {
                MessageBox.Show("Please select a style");
                return;
            }

            // Validate selection in dgvSizesQuantities
            bool sizeAndQuantitySelected = false;
            int requiredQuantity = 0;

            foreach (DataGridViewRow sizeRow in dgvSizesQuantities.Rows)
            {
                if (sizeRow.Cells["Size"].Value != null &&
                    sizeRow.Cells["Quantity"].Value != null &&
                    Convert.ToInt32(sizeRow.Cells["Quantity"].Value) > 0)
                {
                    requiredQuantity = Convert.ToInt32(sizeRow.Cells["Quantity"].Value);
                    sizeAndQuantitySelected = true;
                    break;
                }
            }

            if (!sizeAndQuantitySelected)
            {
                MessageBox.Show("Please select a size and enter a quantity.");
                return;
            }

            // Validate the selected rows in dgvGarmentsAvailable
            var selectedRows = dgvGarmentsAvailable.Rows.Cast<DataGridViewRow>()
                                           .Where(r => Convert.ToBoolean(r.Cells["Select"].Value))
                                           .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one box in the 'Select Boxes' grid.");
                return;
            }

            // Save operation
            int accumulatedQuantity = 0;

            foreach (var row in selectedRows)
            {
                var Pk = Convert.ToInt32(row.Cells["Key"].Value);
                var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);
                int boxedQty = Convert.ToInt32(row.Cells["BoxedQty"].Value);
                int toBatchQty = Convert.ToInt32(row.Cells["ToBatchQty"].Value);

                if (StockOnHand != null)
                {
                    // Check if SplitBox is checked and ToBatchQty is valid
                    if (Convert.ToBoolean(row.Cells["SplitBox"].Value))
                    {
                        if (toBatchQty >= boxedQty)
                        {
                            MessageBox.Show("To Batch Quantity must be less than Boxed Quantity when Split Box is checked.");
                            return;
                        }

                        // Split the box
                        row.Cells["BoxedQty"].Value = boxedQty - toBatchQty;
                        CreateNewBoxAndSplit(StockOnHand, toBatchQty, boxedQty, row);
                        accumulatedQuantity += toBatchQty;
                    }
                    else
                    {
                        if (accumulatedQuantity + boxedQty <= requiredQuantity)
                        {
                            row.Cells["ToBatchQty"].Value = boxedQty;
                            row.Cells["StockQty"].Value = 0;
                            accumulatedQuantity += boxedQty;
                        }
                        else
                        {
                            int remainingQty = requiredQuantity - accumulatedQuantity;
                            row.Cells["ToBatchQty"].Value = remainingQty;
                            accumulatedQuantity += remainingQty;
                        }
                    }

                    StockOnHand.TLSOH_RFD_NotYetDyed = true;

                    var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
                    if (History == null)
                    {
                        History = new TLDYE_RFDHistory
                        {
                            DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK,
                            DyeRFD_BeginDyeDate = dateTimePicker1.Value.Date,
                            DyeRFD_DyeToColour = ColourSelected.Col_Id,
                            DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk,
                            DyeRFD_Transaction_No = lastBatchNo.col14
                        };

                        _context.TLDYE_RFDHistory.Add(History);
                    }

                    if (accumulatedQuantity >= requiredQuantity)
                    {
                        break;
                    }
                }
            }

            //lastBatchNo.col14 += 1;

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Data successfully saved to database");

                // Generate Crystal Report
                //GenerateReport(selectedRows);

                this.Cursor = Cursors.WaitCursor;

                frmDyeViewReport vRep = new frmDyeViewReport(65, lastBatchNo.col14);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                

                this.Cursor = Cursors.Default;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

            lastBatchNo.col14 += 1;
        }

        private void GenerateReport(List<DataGridViewRow> selectedRows)
        {
            // Create a new DataSet
            DataSet reportDataSet = new DataSet("GarmentDataSet");
            DataTable reportTable = reportDataSet.Tables.Add("Garments");

            // Add columns to DataTable
            reportTable.Columns.Add("Key", typeof(int));
            reportTable.Columns.Add("BoxNo", typeof(string));
            reportTable.Columns.Add("Size", typeof(string));
            reportTable.Columns.Add("BoxedQty", typeof(int));
            reportTable.Columns.Add("ToBatchQty", typeof(int));
            reportTable.Columns.Add("Colour", typeof(string));
            reportTable.Columns.Add("Style", typeof(string));

            // Fill DataTable with selected rows
            foreach (var row in selectedRows)
            {
                var Pk = Convert.ToInt32(row.Cells["Key"].Value);
                var boxNo = row.Cells["BoxNo"].Value.ToString();
                var size = row.Cells["Size"].Value.ToString();
                var boxedQty = Convert.ToInt32(row.Cells["BoxedQty"].Value);
                var toBatchQty = Convert.ToInt32(row.Cells["ToBatchQty"].Value);
                var colour = cmbColours.Text;
                var style = cmboStyles.Text;

                reportTable.Rows.Add(Pk, boxNo, size, boxedQty, toBatchQty, colour, style);
            }

            // Create and set up the Crystal Report
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("PathToYourCrystalReport.rpt"); // Set the path to your Crystal Report file
            reportDocument.SetDataSource(reportDataSet);

            // Show the report in a Crystal Report Viewer
            CrystalReportViewer viewer = new CrystalReportViewer();
            viewer.ReportSource = reportDocument;
            viewer.Refresh();

            // You may also want to show the report in a new form
            Form reportForm = new Form();
            reportForm.Controls.Add(viewer);
            viewer.Dock = DockStyle.Fill;
            reportForm.ShowDialog();
        }


        private void CreateNewBoxAndSplit(TLCSV_StockOnHand originalBox, int toBatchQty, int boxedQty, DataGridViewRow row)
        {
            // Duplicate the original row with a new Box Number
            var newBox = new TLCSV_StockOnHand
            {
                TLSOH_BoxNumber = originalBox.TLSOH_BoxNumber.EndsWith("B") ? originalBox.TLSOH_BoxNumber : originalBox.TLSOH_BoxNumber + "B",
                TLSOH_Style_FK = originalBox.TLSOH_Style_FK,
                TLSOH_Size_FK = originalBox.TLSOH_Size_FK,
                TLSOH_Colour_FK = originalBox.TLSOH_Colour_FK,
                TLSOH_BoxedQty = boxedQty - toBatchQty,
                TLSOH_RFD_NotYetDyed = false
            };

            _context.TLCSV_StockOnHand.Add(newBox);

            // Update the original row
            originalBox.TLSOH_BoxNumber = originalBox.TLSOH_BoxNumber.EndsWith("A") ? originalBox.TLSOH_BoxNumber : originalBox.TLSOH_BoxNumber + "A";
            originalBox.TLSOH_BoxedQty = toBatchQty;
            originalBox.TLSOH_RFD_NotYetDyed = true;

            // Update the DataGridView
            row.Cells["BoxedQty"].Value = toBatchQty;
            row.Cells["ToBatchQty"].Value = toBatchQty;
            row.Cells["StockQty"].Value = 0;

            // Add the new row to the DataGridView
            DataRow newRow = dtGarmentsAvailable.NewRow();
            newRow["GD_pk"] = newBox.TLSOH_Pk;
            newRow["GD_BoxNo"] = newBox.TLSOH_BoxNumber;
            newRow["GD_Size"] = row.Cells["Size"].Value; // Ensure this matches your actual column name
            newRow["GD_Quantity"] = newBox.TLSOH_BoxedQty;
            newRow["SplitBox"] = false;
            newRow["QtyToBatch"] = newBox.TLSOH_BoxedQty;
            newRow["QtyToStock"] = 0;
            dtGarmentsAvailable.Rows.Add(newRow);
        }


        //private void CreateNewBox(TLCSV_StockOnHand originalBox, int newQty, ref char suffix)
        //{
        //    var newBox = new TLCSV_StockOnHand
        //    {
        //        TLSOH_BoxNumber = originalBox.TLSOH_BoxNumber + suffix,
        //        TLSOH_Style_FK = originalBox.TLSOH_Style_FK,
        //        TLSOH_Size_FK = originalBox.TLSOH_Size_FK,
        //        TLSOH_Colour_FK = originalBox.TLSOH_Colour_FK,
        //        TLSOH_BoxedQty = newQty,
        //        TLSOH_RFD_NotYetDyed = true
        //    };

        //    _context.TLCSV_StockOnHand.Add(newBox);
        //    suffix++;
        //}


        //{
        //    Button oBtn = sender as Button;
        //    if (oBtn != null && formLoaded)
        //    {
        //        TLADM_Colours ColourSelected = (TLADM_Colours)cmbColours.SelectedItem;
        //        if (ColourSelected == null)
        //        {
        //            using (DialogCenteringService centeringService = new DialogCenteringService(this))
        //            {
        //                MessageBox.Show("Please select a colour");
        //            }
        //            return;
        //        }

        //        // Validate selection in dgvSizesQuantities
        //        bool sizeAndQuantitySelected = false;
        //        string selectedSize = null;
        //        int requiredQuantity = 0;

        //        foreach (DataGridViewRow sizeRow in dgvSizesQuantities.Rows)
        //        {
        //            if (sizeRow.Cells["Size"].Value != null &&
        //                sizeRow.Cells["Quantity"].Value != null &&
        //                Convert.ToInt32(sizeRow.Cells["Quantity"].Value) > 0)
        //            {
        //                selectedSize = sizeRow.Cells["Size"].Value.ToString();
        //                requiredQuantity = Convert.ToInt32(sizeRow.Cells["Quantity"].Value);
        //                sizeAndQuantitySelected = true;
        //                break;
        //            }
        //        }

        //        if (!sizeAndQuantitySelected)
        //        {
        //            MessageBox.Show("Please select a size and enter a quantity.");
        //            return;
        //        }

        //        // Validate the number of selected rows
        //        var selectedRows = dgvGarmentsAvailable.Rows.Cast<DataGridViewRow>()
        //                               .Where(r => Convert.ToBoolean(r.Cells["Select"].Value))
        //                               .ToList();
        //        if (selectedRows.Count > 11)
        //        {
        //            MessageBox.Show("You cannot select more than 11 rows.");
        //            return;
        //        }

        //        int accumulatedQuantity = 0;
        //        char suffix = 'A';

        //        foreach (var row in selectedRows)
        //        {
        //            var Pk = Convert.ToInt32(row.Cells["Key"].Value);
        //            var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);
        //            int boxedQty = Convert.ToInt32(row.Cells["BoxedQty"].Value);

        //            if (StockOnHand != null)
        //            {
        //                if (accumulatedQuantity + boxedQty <= requiredQuantity)
        //                {
        //                    row.Cells["ToBatchQty"].Value = boxedQty;
        //                    row.Cells["StockQty"].Value = 0;
        //                    accumulatedQuantity += boxedQty;
        //                }
        //                else
        //                {
        //                    int remainingQty = requiredQuantity - accumulatedQuantity;
        //                    row.Cells["ToBatchQty"].Value = remainingQty;

        //                    if (Convert.ToBoolean(row.Cells["SplitBox"].Value))
        //                    {
        //                        row.Cells["BoxedQty"].Value = boxedQty - remainingQty;
        //                        CreateNewBox(StockOnHand, remainingQty, ref suffix);
        //                    }
        //                    accumulatedQuantity += remainingQty;
        //                }

        //                // Update the box number with a suffix
        //                StockOnHand.TLSOH_BoxNumber += suffix;
        //                suffix++;

        //                StockOnHand.TLSOH_RFD_NotYetDyed = true;

        //                var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
        //                if (History == null)
        //                {
        //                    History = new TLDYE_RFDHistory
        //                    {
        //                        DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK,
        //                        DyeRFD_BeginDyeDate = dtpDateDyed.Value.Date,
        //                        DyeRFD_DyeToColour = ColourSelected.Col_Id,
        //                        DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk,
        //                        DyeRFD_Transaction_No = lastBatchNo.col14
        //                    };

        //                    _context.TLDYE_RFDHistory.Add(History);
        //                }
        //            }

        //            if (accumulatedQuantity >= requiredQuantity)
        //            {
        //                break;
        //            }
        //        }

        //        lastBatchNo.col14 += 1;

        //        try
        //        {
        //            _context.SaveChanges();
        //            MessageBox.Show("Data successfully saved to database");
        //            this.Close();

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.InnerException.Message);
        //        }
        //    }
        //}

        //private void CreateNewBox(TLCSV_StockOnHand originalBox, int newQty, ref char suffix)
        //{
        //    var newBox = new TLCSV_StockOnHand
        //    {
        //        TLSOH_BoxNumber = originalBox.TLSOH_BoxNumber + suffix,
        //        TLSOH_Style_FK = originalBox.TLSOH_Style_FK,
        //        TLSOH_Size_FK = originalBox.TLSOH_Size_FK,
        //        TLSOH_Colour_FK = originalBox.TLSOH_Colour_FK,
        //        TLSOH_BoxedQty = newQty,
        //        TLSOH_RFD_NotYetDyed = true
        //    };

        //    _context.TLCSV_StockOnHand.Add(newBox);
        //    suffix++;
        //}

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            //if (oCmbo != null && formLoaded)
            //{
            //    TLADM_Styles StyleSelected = (TLADM_Styles)oCmbo.SelectedItem;

            //    if (StyleSelected != null)
            //    {
            //        LoadGarmentsAvailable(StyleSelected);
            //    }               
            //}
        }

        private void LoadGarmentsAvailable(TLADM_Styles selectedStyle, TLADM_Sizes selectedSize)
        {
            //  var StockAvail = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == selectedStyle.Sty_Id && x.TLSOH_Size_FK == selectedSize.SI_id && !x.TLSOH_RFD_NotYetDyed).OrderBy(x => x.TLSOH_BoxNumber).ToList();

            var StockAvail = _context.TLCSV_StockOnHand
    .Where(x => x.TLSOH_Style_FK == selectedStyle.Sty_Id
             && x.TLSOH_Size_FK == selectedSize.SI_id
             && !x.TLSOH_RFD_NotYetDyed)
    .OrderBy(x => x.TLSOH_BoxNumber)
    .ToList();


            dtGarmentsAvailable.Rows.Clear();

            foreach (var SAvail in StockAvail)
            {
                DataRow Row = dtGarmentsAvailable.NewRow();
                Row[0] = SAvail.TLSOH_Pk;
                Row[1] = false;
                Row[2] = SAvail.TLSOH_BoxNumber;
                var size = _context.TLADM_Sizes.Find(SAvail.TLSOH_Size_FK);
                Row[3] = size != null ? size.SI_Description : string.Empty;
                Row[4] = SAvail.TLSOH_BoxedQty;
                Row[5] = false;
                Row[6] = SAvail.TLSOH_BoxedQty;
                Row[7] = 0;

                dtGarmentsAvailable.Rows.Add(Row);
            }
        }

  
        private void frmDyeRFD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
