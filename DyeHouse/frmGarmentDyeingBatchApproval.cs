using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using static log4net.Appender.ColoredConsoleAppender;

namespace DyeHouse
{
    public partial class frmGarmentDyeingBatchApproval : Form
    {
        bool FormLoaded;
        int gPreviousRowIndex = -1;
        protected readonly TTI2Entities _context;

        private bool _loadingExistingRows = false;
        private bool _gridDirty = false;
        private bool _hasExistingRows = false;

        public frmGarmentDyeingBatchApproval()
        {
            InitializeComponent();

            _context = new TTI2Entities();

            cboBatchNo.SelectedIndexChanged += new EventHandler(cboBatchNo_SelectedIndexChanged);

            // Add event handlers for DataGridView
            dgvDyeBatch.RowValidating += dgvDyeBatch_RowValidating;
            dgvDyeBatch.CellValueChanged += dgvDyeBatch_CellValueChanged;
            dgvDyeBatch.CurrentCellDirtyStateChanged += dgvDyeBatch_CurrentCellDirtyStateChanged;
            dgvDyeBatch.UserDeletedRow += dgvDyeBatch_UserDeletedRow;
            dgvDyeBatch.RowsAdded += dgvDyeBatch_RowsAdded;
        }


        private void frmGarmentDyeingBatchApproval_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            ResetForm();

            LoadBatchNumbers();
            LoadMachineDefinitions();
            LoadShifts();

            InitializeCloseDyeBatchComboBox();

            FormLoaded = true;
        }

        private void ResetForm()
        {
            cboBatchNo.DataSource = null;
            txtStyle.Text = null;
            txtColour.Text = null;
            cboShift.DataSource = null;
            cboMachine.DataSource = null;

            dtpDateDyed.Value = DateTimePicker.MinimumDateTime;

            dgvDyeBatch.AutoGenerateColumns = false;
            dgvDyeBatch.AllowUserToAddRows = false;

            _gridDirty = false;
            _hasExistingRows = false;
            btnSave.Enabled = false;
        }

        private void LoadBatchNumbers()
        {
            var batchNumbers = _context.TLDYE_RFDHistory
                .Where(x => !x.DyeRFD_Completed)
                .Select(x => "GD000" + x.DyeRFD_Transaction_No)
                .Distinct()
                .ToList();

            cboBatchNo.DataSource = batchNumbers;
            cboBatchNo.SelectedIndex = -1;
        }

        private void InitializeCloseDyeBatchComboBox()
        {
            cboCloseDyeBatch.Items.Clear();
            cboCloseDyeBatch.Items.Add("Yes");
            cboCloseDyeBatch.Items.Add("No");
            cboCloseDyeBatch.SelectedIndex = 1; // Default to "No"
        }

        private void cboBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormLoaded && cboBatchNo.SelectedIndex != -1)
            {
                PopulateDataGridForSelectedBatch();
            }
        }

        private void PopulateDataGridForSelectedBatch()
        {
            var selectedTransactionNo = int.Parse(cboBatchNo.SelectedItem.ToString().Replace("GD000", ""));

            var transactionEntries = _context.TLDYE_RFDHistory
                .Where(x => x.DyeRFD_Transaction_No == selectedTransactionNo)
                .ToList();

            dgvDyeBatch.Columns.Clear();
            dgvDyeBatch.Rows.Clear();

            if (!transactionEntries.Any())
            {
                btnSave.Enabled = false;
                return;
            }

            var transactionEntry = transactionEntries.First();

            dtpDateDyed.Value = transactionEntry.DyeRFD_BeginDyeDate ?? DateTimePicker.MinimumDateTime;

            var styles = transactionEntries
                .Select(x => _context.TLADM_Styles
                    .Where(s => s.Sty_Id == x.DyeRFD_CurrentStyle)
                    .Select(s => s.Sty_Description)
                    .FirstOrDefault())
                .Distinct()
                .ToList();

            var colours = transactionEntries
                .Select(x => _context.TLADM_Colours
                    .Where(c => c.Col_Id == x.DyeRFD_DyeToColour)
                    .Select(c => c.Col_Display)
                    .FirstOrDefault())
                .Distinct()
                .ToList();

            txtStyle.Text = styles.FirstOrDefault();
            txtColour.Text = colours.FirstOrDefault();

            var sizes = _context.TLADM_Sizes
                .Where(x => !x.SI_Discontinued)
                .OrderBy(x => x.SI_DisplayOrder)
                .Select(x => x.SI_Description)
                .ToList();

            AddSizeComboBoxColumn("Size", "Size", sizes);
            AddGridColumn("BoxNumber", "Box Number", true);
            AddComboBoxColumn("Grade", "Grade", new List<string> { "A", "B", "C", "D", "E" });
            AddGridColumn("BoxQuantity", "Box Quantity", true);

            var existingProductionRows = _context.TLDYE_GarmentDyeingProduction
                .Where(x => x.GarmentDyeingTransactionNo == selectedTransactionNo)
                .OrderBy(x => x.BoxNo)
                .ToList();

            _loadingExistingRows = true;
            try
            {
                if (existingProductionRows.Any())
                {
                    _hasExistingRows = true;
                    _gridDirty = false;
                    btnSave.Enabled = false;

                    foreach (var prodRow in existingProductionRows)
                    {
                        var sizeDesc = _context.TLADM_Sizes
                            .Where(s => s.SI_id == prodRow.Size)
                            .Select(s => s.SI_Description)
                            .FirstOrDefault();

                        int rowIndex = dgvDyeBatch.Rows.Add();
                        dgvDyeBatch.Rows[rowIndex].Cells["Size"].Value = sizeDesc;
                        dgvDyeBatch.Rows[rowIndex].Cells["BoxNumber"].Value = ExtractBoxSuffix(prodRow.BoxNo);
                        dgvDyeBatch.Rows[rowIndex].Cells["Grade"].Value = prodRow.Grade;
                        dgvDyeBatch.Rows[rowIndex].Cells["BoxQuantity"].Value = prodRow.BoxQuantity;
                    }

                    dgvDyeBatch.AllowUserToAddRows = true;
                }
                else
                {
                    _hasExistingRows = false;
                    _gridDirty = false;
                    btnSave.Enabled = false;

                    AddNewRow(1);
                }
            }
            finally
            {
                _loadingExistingRows = false;
            }
        }

        //private void PopulateDataGridForSelectedBatch()
        //{
        //    //if (cboBatchNo.SelectedIndex == -1 || cboBatchNo.SelectedIndex == 0) return;

        //    var selectedTransactionNo = cboBatchNo.SelectedItem.ToString().Replace("GD000", "");
        //    var transactionEntries = _context.TLDYE_RFDHistory
        //        .Where(x => x.DyeRFD_Transaction_No.ToString() == selectedTransactionNo)
        //        .ToList();

        //    if (transactionEntries.Any())
        //    {
        //        var transactionEntry = transactionEntries.First();

        //        dtpDateDyed.Value = transactionEntry.DyeRFD_BeginDyeDate ?? DateTimePicker.MinimumDateTime; // Use DateTimePicker.MinimumDateTime if the date is null

        //        var styles = transactionEntries
        //            .Select(x => _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == x.DyeRFD_CurrentStyle)?.Sty_Description)
        //            .Distinct()
        //            .ToList();

        //        var colours = transactionEntries
        //            .Select(x => _context.TLADM_Colours.FirstOrDefault(c => c.Col_Id == x.DyeRFD_DyeToColour)?.Col_Display)
        //            .Distinct()
        //            .ToList();

        //        txtStyle.Text = styles.FirstOrDefault();
        //        txtColour.Text = colours.FirstOrDefault();

        //        var sizes = _context.TLADM_Sizes
        //            .Where(x => !x.SI_Discontinued)
        //            .OrderBy(x => x.SI_DisplayOrder)
        //            .Select(x => x.SI_Description)
        //            .ToList();

        //        // Clear existing columns and add new columns
        //        dgvDyeBatch.Columns.Clear();
        //        dgvDyeBatch.Rows.Clear();

        //        AddSizeComboBoxColumn("Size", "Size", sizes);
        //        AddGridColumn("BoxNumber", "Box Number", true);
        //        AddComboBoxColumn("Grade", "Grade", new List<string> { "A", "B", "C", "D", "E" });
        //        AddGridColumn("BoxQuantity", "Box Quantity", true);

        //        //dgvDyeBatch.AllowUserToAddRows = true;
        //        AddNewRow(1); // Start with Box Number 1

        //    }
        //    else
        //    {
        //        // Clear existing columns if no entries found
        //        dgvDyeBatch.Columns.Clear();
        //        dgvDyeBatch.Rows.Clear();
        //    }
        //}

        private string ExtractBoxSuffix(string fullBoxNo)
        {
            if (string.IsNullOrWhiteSpace(fullBoxNo))
                return string.Empty;

            int lastDash = fullBoxNo.LastIndexOf('-');
            if (lastDash >= 0 && lastDash < fullBoxNo.Length - 1)
                return fullBoxNo.Substring(lastDash + 1);

            return fullBoxNo;
        }

        private void dgvDyeBatch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loadingExistingRows) return;

            if (e.RowIndex >= 0 && !dgvDyeBatch.Rows[e.RowIndex].IsNewRow)
            {
                MarkGridDirty();
                ValidateAndAddRow(e.RowIndex);
            }
        }

        private void dgvDyeBatch_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && !dgvDyeBatch.Rows[e.RowIndex].IsNewRow)
            {
                ValidateAndAddRow(e.RowIndex);
            }
        }

        private void dgvDyeBatch_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDyeBatch.IsCurrentCellDirty)
            {
                dgvDyeBatch.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvDyeBatch_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MarkGridDirty();
        }

        private void dgvDyeBatch_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!_loadingExistingRows && FormLoaded)
            {
                MarkGridDirty();
            }
        }

        private void MarkGridDirty()
        {
            if (_loadingExistingRows) return;

            _gridDirty = true;
            btnSave.Enabled = true;
        }

        private void ValidateAndAddRow(int rowIndex)
        {
            if (_loadingExistingRows) return;

            var row = dgvDyeBatch.Rows[rowIndex];
            bool isRowComplete = true;

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    isRowComplete = false;
                    break;
                }
            }

            if (isRowComplete && gPreviousRowIndex != rowIndex)
            {
                int nextBoxNumber = rowIndex + 2;
                gPreviousRowIndex = rowIndex;

                this.BeginInvoke((MethodInvoker)delegate
                {
                    AddNewRow(nextBoxNumber);
                });
            }
        }

        //private void ValidateAndAddRow(int rowIndex)
        //{
        //    var row = dgvDyeBatch.Rows[rowIndex];
        //    bool isRowComplete = true;

        //    foreach (DataGridViewCell cell in row.Cells)
        //    {
        //        if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
        //        {
        //            isRowComplete = false;
        //            break;
        //        }
        //    }

        //    if (isRowComplete && gPreviousRowIndex != rowIndex)
        //    {                
        //        int nextBoxNumber = rowIndex + 2; // Since rowIndex is 0-based, next box number is rowIndex + 2
        //        gPreviousRowIndex = rowIndex;
        //        this.BeginInvoke((MethodInvoker)delegate {
        //            AddNewRow(nextBoxNumber);
        //            //dgvDyeBatch.AllowUserToAddRows = true;
        //        });
        //    }
        //}

        private void AddComboBoxColumn(string dataPropertyName, string headerText, List<string> items)
        {
            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = dataPropertyName,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                DataSource = items,
                AutoComplete = true
            };
            dgvDyeBatch.Columns.Add(comboBoxColumn);
        }

        private void AddGridColumn(string dataPropertyName, string headerText, bool isEditable)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = dataPropertyName,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                ReadOnly = !isEditable
            };
            dgvDyeBatch.Columns.Add(column);
        }

        private void AddSizeComboBoxColumn(string dataPropertyName, string headerText, List<string> sizes)
        {
            AddComboBoxColumn(dataPropertyName, headerText, sizes);
        }

        private void AddNewRow(int boxNumber)
        {
            Console.WriteLine("Adding new row with box number: " + boxNumber);
            var newRow = dgvDyeBatch.Rows[dgvDyeBatch.Rows.Add()];           
            // Set the BoxNumber to default incrementing value
            var selectedBatchNo = cboBatchNo.SelectedItem.ToString();

            //int boxNumber = dgvDyeBatch.Rows.Count;
            newRow.Cells["BoxNumber"].Value = boxNumber;
            //newRow.Cells["BoxLabel"].Value = $"{selectedBatchNo}-{boxNumber.ToString().PadLeft(2, '0')}";

            //dgvDyeBatch.AllowUserToAddRows = true;
        }

        private void LoadMachineDefinitions()
        {
            var machines = _context.TLADM_MachineDefinitions
                .OrderBy(x => x.MD_Description)
                .Select(x => x.MD_Description)
                .Distinct()
                .ToList();

            cboMachine.DataSource = machines;
            cboMachine.SelectedIndex = -1;
        }

        private void LoadShifts()
        {
            //var shifts = _context.TLADM_Shifts
            //    .Where(x => x.Shft_Dept_FK == Department.Dep_Id)
            //    .Select(x => x.Shft_Description)
            //    .ToList();

            //cboShift.DataSource = shifts;
            cboShift.SelectedIndex = -1; // Ensure the ComboBox is empty
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            dgvDyeBatch.AllowUserToAddRows = false;
            if (cboBatchNo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a batch number.");
                return;
            }

            int rowsSaved = 0;
            var selectedBatchNo = cboBatchNo.SelectedItem.ToString();
            var selectedTransactionNo = selectedBatchNo.Replace("GD000", "");
            bool closeDyeBatch = cboCloseDyeBatch.SelectedItem.ToString() == "Yes";
            int transactionNo = int.Parse(selectedTransactionNo);

            if (_hasExistingRows && _gridDirty)
            {
                var existingRows = _context.TLDYE_GarmentDyeingProduction
                    .Where(x => x.GarmentDyeingTransactionNo == transactionNo)
                    .ToList();

                foreach (var existing in existingRows)
                {
                    _context.TLDYE_GarmentDyeingProduction.Remove(existing);
                }
            }
            else if (_hasExistingRows && !_gridDirty)
            {
                MessageBox.Show("No changes were made.");
                return;
            }

            foreach (DataGridViewRow row in dgvDyeBatch.Rows)
            {
                string sizeDescription = row.Cells["Size"].Value?.ToString();
                string grade = row.Cells["Grade"].Value?.ToString();
                string boxNumber = row.Cells["BoxNumber"].Value?.ToString();
                int boxQuantity = int.Parse(row.Cells["BoxQuantity"].Value?.ToString() ?? "0");

                if (row.IsNewRow || (sizeDescription == null && grade == null && boxQuantity == 0)) continue;

                try
                {
                    SaveDataRow(row, selectedTransactionNo, selectedBatchNo, closeDyeBatch,
                        sizeDescription, grade, boxNumber, boxQuantity);
                    rowsSaved++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Saving Row: {ex.Message}");
                }


            }

            try
            {
                if (rowsSaved > 0)
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void SaveDataRow(DataGridViewRow row, string selectedTransactionNo, string selectedBatchNo, bool closeDyeBatch,
    string sizeDescription, string grade, string boxNumber, int boxQuantity)
        {
            var size = _context.TLADM_Sizes.FirstOrDefault(s => s.SI_Description == sizeDescription);
            if (size == null)
            {
                MessageBox.Show($"Size not found: {sizeDescription}");
                return;
            }

            if (string.IsNullOrEmpty(grade) || string.IsNullOrEmpty(boxNumber) || boxQuantity == 0)
            {
                MessageBox.Show("Please fill in all fields for each row.");
                return;
            }

            int transactionNo = int.Parse(selectedTransactionNo);
            int sizeId = size.SI_id;
            string newGarmentBoxNo = $"{selectedBatchNo}-{boxNumber.PadLeft(2, '0')}";

            var garmentDyeingProduction = new TLDYE_GarmentDyeingProduction
            {
                GarmentDyeingTransactionNo = transactionNo,
                Size = sizeId,
                Grade = grade,
                BoxNo = newGarmentBoxNo,
                BoxQuantity = boxQuantity,
                Closed = true  //**TODO-AS: should validate if box quantities is same as original batch quantities -> DyeRFD_NoumberOfAGrades (also rename this field! to DyePFD_BoxQuantity) )
            };

            _context.TLDYE_GarmentDyeingProduction.Add(garmentDyeingProduction);

            var history = _context.TLDYE_RFDHistory
                .FirstOrDefault(x => x.DyeRFD_Transaction_No == transactionNo);

            if (history == null)
            {
                throw new Exception($"No PFD history found for transaction {selectedTransactionNo}");
            }

            UpdateMatchingStockOnHandRows(
                transactionNo,
                history.DyeRFD_CurrentStyle,
                sizeId,
                newGarmentBoxNo);
        }

        private void UpdateMatchingStockOnHandRows(int transactionNo,
                                                   int styleFk,
                                                   int sizeFk,
                                                   string newGarmentBoxNo)
        {
            var matchingStocks =
                (from h in _context.TLDYE_RFDHistory
                 join s in _context.TLCSV_StockOnHand
                    on h.DyeRFD_StockOnHand_Fk equals s.TLSOH_Pk
                 where h.DyeRFD_Transaction_No == transactionNo
                       && h.DyeRFD_CurrentStyle == styleFk
                       && s.TLSOH_Size_FK == sizeFk
                       && s.TLSOH_WareHouse_FK == 93
                       && s.TLSOH_PFD_BoxNumber != null
                       && s.TLSOH_PFD_BoxNumber != ""
                 select s).Distinct().ToList();

            if (matchingStocks.Count == 0)
            {
                throw new Exception($"No matching stock row found for transaction {transactionNo}, style {styleFk}, size {sizeFk}.");
            }

            foreach (var stock in matchingStocks)
            {
                stock.TLSOH_PFD_BoxNumber = AppendGarmentBoxNo(
                    stock.TLSOH_PFD_BoxNumber,
                    newGarmentBoxNo,
                    stock.TLSOH_BoxNumber
                );

                stock.TLSOH_WareHouse_FK = 101;
            }
        }

        private string AppendGarmentBoxNo(string existingValue, string newGarmentBoxNo, string currentBoxNo)
        {
            // If field is empty, start with original box number
            if (string.IsNullOrWhiteSpace(existingValue))
            {
                return $"{currentBoxNo}|{newGarmentBoxNo}";
            }

            string originalBox = existingValue;
            string existingGarmentBoxes = string.Empty;

            if (existingValue.Contains("|"))
            {
                var parts = existingValue.Split('|');
                originalBox = parts[0];

                if (parts.Length > 1)
                    existingGarmentBoxes = parts[1];
            }

            var garmentBoxList = new List<string>();

            if (!string.IsNullOrWhiteSpace(existingGarmentBoxes))
            {
                garmentBoxList = existingGarmentBoxes
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();
            }

            if (!garmentBoxList.Contains(newGarmentBoxNo))
            {
                garmentBoxList.Add(newGarmentBoxNo);
            }

            return $"{originalBox}|{string.Join(",", garmentBoxList)}";
        }

        private void frmGarmentDyeingBatchApproval_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel && _context != null)
            {
                _context.Dispose();
            }
        }
    }
}
