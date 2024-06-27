using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
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
                SetupDataGrids();
                this.FormClosed += FrmDyeRFD_FormClosed;
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

                    // Unsubscribe before subscribing to prevent multiple subscriptions
                    dgvSizesQuantities.CellEndEdit -= dgvSizesQuantities_CellEndEdit;
                    dgvSizesQuantities.EditingControlShowing -= dgvSizesQuantities_EditingControlShowing;
                    dgvSizesQuantities.CellEnter -= dgvSizesQuantities_CellEnter;
                    dgvSizesQuantities.CurrentCellDirtyStateChanged -= dgvSizesQuantities_CurrentCellDirtyStateChanged;
                    dgvSizesQuantities.CellValueChanged -= dgvSizesQuantities_CellValueChanged;

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
                        //dgvSizesQuantities.BeginEdit(true);
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

                // Unsubscribe before subscribing to prevent multiple subscriptions
                dgvGarmentsAvailable.CellEndEdit -= dgvGarmentsAvailable_CellEndEdit;
                dgvGarmentsAvailable.CellValueChanged -= dgvGarmentsAvailable_CellValueChanged;
                dgvGarmentsAvailable.CurrentCellDirtyStateChanged -= dgvGarmentsAvailable_CurrentCellDirtyStateChanged;

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

            private void frmDyeRFD_Load(object sender, EventArgs e)
            {
                Cursor.Current = Cursors.WaitCursor;
                formLoaded = false;

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

                    foreach (DataGridViewRow row in dgvGarmentsAvailable.Rows)
                    {
                        if (!Convert.ToBoolean(row.Cells["Select"].Value))
                        {
                            continue;
                        }

                        var Pk = Convert.ToInt32(row.Cells["Key"].Value);
                        var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);

                        try
                        {
                            if (StockOnHand != null)
                            {
                                StockOnHand.TLSOH_RFD_NotYetDyed = true;

                                var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
                                if (History == null)
                                {
                                    History = new TLDYE_RFDHistory();
                                    History.DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK;
                                    History.DyeRFD_BeginDyeDate = dtpDateDyed.Value.Date;
                                    History.DyeRFD_DyeToColour = ColourSelected.Col_Id;
                                    History.DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk;
                                    History.DyeRFD_Transaction_No = lastBatchNo.col14;

                                    _context.TLDYE_RFDHistory.Add(History);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.Message);
                        }
                    }

                    lastBatchNo.col14 += 1;

                    try
                    {
                        _context.SaveChanges();
                    //frmDyeOrders_Load(this, null);
                    MessageBox.Show("Data successfully saved to database");
                        this.Close();


                    this.Cursor = Cursors.WaitCursor;

                    frmDyeViewReport vRep = new frmDyeViewReport(65);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
          
                    this.Cursor = Cursors.Default;
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);

                    //var exceptionMessages = new StringBuilder();
                    //do
                    //{
                    //    exceptionMessages.Append(ex.Message);
                    //    ex = ex.InnerException;
                    //}
                    //while (ex != null);
                    //MessageBox.Show(exceptionMessages.ToString());
                }
            }
            }

            private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
            {
                ComboBox oCmbo = sender as ComboBox;
            }

            private void LoadGarmentsAvailable(TLADM_Styles selectedStyle, TLADM_Sizes selectedSize)
            {
                var StockAvail = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == selectedStyle.Sty_Id && x.TLSOH_Size_FK == selectedSize.SI_id && !x.TLSOH_RFD_NotYetDyed).OrderBy(x => x.TLSOH_BoxNumber).ToList();

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

            private void FrmDyeRFD_FormClosed(object sender, FormClosedEventArgs e)
            {
                dtQuantitiesSizes.Dispose();
                dtGarmentsAvailable.Dispose();
                _context.Dispose();
            }

            private void frmDyeRFD_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (!e.Cancel)
                {
                    FrmDyeRFD_FormClosed(sender, null);
                }
            }
        }
}
