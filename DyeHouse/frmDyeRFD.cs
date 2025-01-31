using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utilities;

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

        //private void UpdateOutstanding()
        //{
        //    totalSelectedQuantity = 0;

        //    foreach (DataGridViewRow row in dgvGarmentsAvailable.Rows)
        //    {
        //        if (Convert.ToBoolean(row.Cells["Select"].Value))
        //        {
        //            int selectedQuantity = Convert.ToInt32(row.Cells["ToBatchQty"].Value);
        //            totalSelectedQuantity += selectedQuantity;
        //        }
        //    }

        //    foreach (DataGridViewRow row in dgvSizesQuantities.Rows)
        //    {
        //        if (row.Cells["Outstanding"].Value != null)
        //        {
        //            int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
        //            row.Cells["Outstanding"].Value = currentQuantity - totalSelectedQuantity;
        //        }
        //    }
        //}

        private void UpdateOutstanding()
        {
            // Get the active row in dgvSizesQuantities
            if (dgvSizesQuantities.CurrentRow != null)
            {
                DataGridViewRow activeRow = dgvSizesQuantities.CurrentRow;
                int activeRowSizeId = Convert.ToInt32(activeRow.Cells["Size"].Value);

                // Calculate total selected quantity for matching size in dgvGarmentsAvailable
                totalSelectedQuantity = 0;

                foreach (DataGridViewRow row in dgvGarmentsAvailable.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Select"].Value) && Convert.ToInt32(row.Cells["SizeId"].Value) == activeRowSizeId)
                    {
                        int selectedQuantity = Convert.ToInt32(row.Cells["ToBatchQty"].Value);
                        totalSelectedQuantity += selectedQuantity;
                    }
                }

                // Update the 'Outstanding' value for the active row in dgvSizesQuantities
                if (activeRow.Cells["Outstanding"].Value != null)
                {
                    int currentQuantity = Convert.ToInt32(activeRow.Cells["Quantity"].Value);
                    activeRow.Cells["Outstanding"].Value = currentQuantity - totalSelectedQuantity;
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
            dtGarmentsAvailable.Columns.Add(new DataColumn ("GD_SizeId", typeof(int)) { DefaultValue = 0 });
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
            dgvGarmentsAvailable.Columns.Add(new DataGridViewTextBoxColumn { Name = "SizeId", DataPropertyName = "GD_SizeId", Visible = false });
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

            //DataGridView dgv = sender as DataGridView;
            //if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
            //    if (column.Name == "Select")
            //    {
            //        UpdateOutstanding();

            //        int selectedQuantity = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[4].Value);
            //        if (dgvSizesQuantities.CurrentRow != null)
            //        {
            //            int rowIndex = dgvSizesQuantities.CurrentRow.Index;

            //            // Get the current quantity in dgvSizesQuantities
            //            if (dgvSizesQuantities.Rows[rowIndex].Cells[3].Value != null)
            //            {
            //                int currentQuantity = Convert.ToInt32(dgvSizesQuantities.Rows[rowIndex].Cells[2].Value);
            //                // Subtract the selected quantity from the current quantity in dgvSizesQuantities
            //                dgvSizesQuantities.Rows[rowIndex].Cells[3].Value = currentQuantity - selectedQuantity;
            //            }
            //        }
            //    }
            //}
        }

        private void dgvGarmentsAvailable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6) // QtyToBatch column
            {
                int qtyToBatch = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[6].Value);
                int selectedQuantity = Convert.ToInt32(dgvGarmentsAvailable.Rows[e.RowIndex].Cells[4].Value);

                // Update QtyToStock column
                dgvGarmentsAvailable.Rows[e.RowIndex].Cells[7].Value = selectedQuantity - qtyToBatch;

                // Update Outstanding in dgvSizesQuantities
                //int rowIndex = dgvSizesQuantities.CurrentCell.RowIndex;
                //int qtyRequired = Convert.ToInt32(dgvSizesQuantities.Rows[rowIndex].Cells[2].Value);
                //dgvSizesQuantities.Rows[rowIndex].Cells[3].Value = qtyRequired - totalSelectedQuantity;

                UpdateOutstanding();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formLoaded)
            {
                TLADM_Colours ColourSelected = (TLADM_Colours)cmbColours.SelectedItem;
                if (ColourSelected == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a colour");
                    }
                    return;
                }

                // Validate selection in dgvSizesQuantities
                bool sizeAndQuantitySelected = false;
                string selectedSize = null;
                int requiredQuantity = 0;

                foreach (DataGridViewRow sizeRow in dgvSizesQuantities.Rows)
                {
                    if (sizeRow.Cells["Size"].Value != null &&
                        sizeRow.Cells["Quantity"].Value != null &&
                        Convert.ToInt32(sizeRow.Cells["Quantity"].Value) > 0)
                    {
                        selectedSize = sizeRow.Cells["Size"].Value.ToString();
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

                // Validate the number of selected rows
                var selectedRows = dgvGarmentsAvailable.Rows.Cast<DataGridViewRow>()
                                       .Where(r => Convert.ToBoolean(r.Cells["Select"].Value))
                                       .ToList();

                int accumulatedQuantity = 0;                

                foreach (var row in selectedRows)
                {
                    var Pk = Convert.ToInt32(row.Cells["Key"].Value);
                    var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);
                    int toBatchQty = Convert.ToInt32(row.Cells["ToBatchQty"].Value);

                    if (StockOnHand != null)
                    {
                        accumulatedQuantity += toBatchQty;
                         
                        StockOnHand.TLSOH_Colour_FK = ColourSelected.Col_Id;
                        StockOnHand.TLSOH_RFD_NotYetDyed =true;

                        _context.Entry(StockOnHand).State = EntityState.Modified;

                        if (Convert.ToBoolean(row.Cells["SplitBox"].Value))
                        {
                            char suffix = 'A';
                            int newStockQty = Convert.ToInt32(row.Cells["StockQty"].Value);                            

                            CreateNewBox(StockOnHand, newStockQty, ColourSelected.Col_Id, suffix);
                            StockOnHand.TLSOH_BoxedQty = toBatchQty;
                            StockOnHand.TLSOH_BoxNumber += suffix;
                        }                        

                        var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
                        if (History == null)
                        {
                            History = new TLDYE_RFDHistory
                            {
                                DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK,
                                DyeRFD_BeginDyeDate = dtpDateDyed.Value.Date,
                                DyeRFD_FinishDyeDate = dtpDateDyeCompletion.Value.Date,
                                DyeRFD_DyeToColour = ColourSelected.Col_Id,
                                DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk,
                                DyeRFD_Transaction_No = lastBatchNo.col14
                            };

                            _context.TLDYE_RFDHistory.Add(History);
                        }
                    }
                }

                int batchNo = lastBatchNo.col14;
                lastBatchNo.col14 += 1;

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data successfully saved to database");
                    this.Close();

                    this.Cursor = Cursors.WaitCursor;

                    frmDyeViewReport vRep = new frmDyeViewReport(65, batchNo);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    this.Cursor = Cursors.Default;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void CreateNewBox(TLCSV_StockOnHand originalBox,  int newQty, int colorId, char suffix)
        {
            suffix++;
            var newBox = new TLCSV_StockOnHand
            {
                TLSOH_BoxNumber = originalBox.TLSOH_BoxNumber + suffix,
                TLSOH_DateIntoStock = originalBox.TLSOH_DateIntoStock,
                TLSOH_Split = true,
                TLSOH_Style_FK = originalBox.TLSOH_Style_FK,
                TLSOH_Size_FK = originalBox.TLSOH_Size_FK,
                TLSOH_Colour_FK = colorId,
                TLSOH_BoxedQty = newQty,
                TLSOH_RFD_NotYetDyed = false
            };

            _context.TLCSV_StockOnHand.Add(newBox);
            

        }

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
             && !x.TLSOH_RFD_NotYetDyed) //should be TLSOH_RFD_Dyed
    .OrderBy(x => x.TLSOH_BoxNumber)
    .ToList();

            foreach (var SAvail in StockAvail)
            {
                DataRow Row = dtGarmentsAvailable.NewRow();
                Row[0] = SAvail.TLSOH_Pk;
                Row[1] = false;
                Row[2] = SAvail.TLSOH_BoxNumber;
                var size = _context.TLADM_Sizes.Find(SAvail.TLSOH_Size_FK);
                Row[3] = size != null ? size.SI_id : 0;
                Row[4] = size != null ? size.SI_Description : string.Empty;
                Row[5] = SAvail.TLSOH_BoxedQty;
                Row[6] = false;
                Row[7] = SAvail.TLSOH_BoxedQty;
                Row[8] = 0;

                dtGarmentsAvailable.Rows.Add(Row);
            }
        }

        //private void LoadGarmentsAvailable(TLADM_Styles selectedStyle, TLADM_Sizes selectedSize)
        //{
        //    var StockAvail = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == selectedStyle.Sty_Id && x.TLSOH_Size_FK == selectedSize.SI_id && !x.TLSOH_RFD_NotYetDyed).OrderBy(x => x.TLSOH_BoxNumber).ToList();

        //    // Check if this is the first selection or if there are already garments displayed
        //    if (dtGarmentsAvailable.Rows.Count == 0)
        //    {
        //        // If no garments are displayed, directly add the new ones
        //        foreach (var SAvail in StockAvail)
        //        {
        //            DataRow Row = dtGarmentsAvailable.NewRow();
        //            Row[0] = SAvail.TLSOH_Pk;
        //            Row[1] = false;
        //            Row[2] = SAvail.TLSOH_BoxNumber;
        //            Row[3] = SAvail.TLSOH_Size_FK;
        //            Row[4] = SAvail.TLSOH_BoxedQty;
        //            Row[5] = false;
        //            Row[6] = SAvail.TLSOH_BoxedQty;
        //            Row[7] = 0;

        //            dtGarmentsAvailable.Rows.Add(Row);
        //        }
        //    }
        //    else
        //    {
        //        // If garments are already displayed, append the new ones for the selected size
        //        foreach (var SAvail in StockAvail)
        //        {
        //            // Check if this garment is already displayed in the grid
        //            bool garmentExists = false;
        //            foreach (DataRow row in dtGarmentsAvailable.Rows)
        //            {
        //                if ((int)row[0] == SAvail.TLSOH_Pk)
        //                {
        //                    garmentExists = true;
        //                    break;
        //                }
        //            }

        //            // If the garment doesn't exist, add it to the grid
        //            if (!garmentExists)
        //            {
        //                DataRow newRow = dtGarmentsAvailable.NewRow();
        //                newRow[0] = SAvail.TLSOH_Pk;
        //                newRow[1] = false;
        //                newRow[2] = SAvail.TLSOH_BoxNumber;
        //                newRow[3] = SAvail.TLSOH_Size_FK;
        //                newRow[4] = SAvail.TLSOH_BoxedQty;
        //                newRow[5] = false;
        //                newRow[6] = SAvail.TLSOH_BoxedQty;
        //                newRow[7] = 0;

        //                dtGarmentsAvailable.Rows.Add(newRow);
        //            }
        //        }
        //    }
        //}

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
