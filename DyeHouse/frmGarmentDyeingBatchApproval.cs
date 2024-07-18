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

        public frmGarmentDyeingBatchApproval()
        {
            InitializeComponent();

            _context = new TTI2Entities();

            cboBatchNo.SelectedIndexChanged += new EventHandler(cboBatchNo_SelectedIndexChanged);

            // Add event handlers for DataGridView
            //dgvDyeBatch.CellValueChanged += new DataGridViewCellEventHandler(dgvDyeBatch_CellValueChanged);
            //dgvDyeBatch.RowValidating += new DataGridViewCellCancelEventHandler(dgvDyeBatch_RowValidating);
            dgvDyeBatch.RowValidating += dgvDyeBatch_RowValidating;
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
            //if (cboBatchNo.SelectedIndex == -1 || cboBatchNo.SelectedIndex == 0) return;

            var selectedTransactionNo = cboBatchNo.SelectedItem.ToString().Replace("GD000", "");
            var transactionEntries = _context.TLDYE_RFDHistory
                .Where(x => x.DyeRFD_Transaction_No.ToString() == selectedTransactionNo)
                .ToList();

            if (transactionEntries.Any())
            {
                var transactionEntry = transactionEntries.First();

                dtpDateDyed.Value = transactionEntry.DyeRFD_BeginDyeDate ?? DateTimePicker.MinimumDateTime; // Use DateTimePicker.MinimumDateTime if the date is null

                var styles = transactionEntries
                    .Select(x => _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == x.DyeRFD_CurrentStyle)?.Sty_Description)
                    .Distinct()
                    .ToList();

                var colours = transactionEntries
                    .Select(x => _context.TLADM_Colours.FirstOrDefault(c => c.Col_Id == x.DyeRFD_DyeToColour)?.Col_Display)
                    .Distinct()
                    .ToList();

                txtStyle.Text = styles.FirstOrDefault();
                txtColour.Text = colours.FirstOrDefault();

                var sizes = _context.TLADM_Sizes
                    .Where(x => !x.SI_Discontinued)
                    .OrderBy(x => x.SI_DisplayOrder)
                    .Select(x => x.SI_Description)
                    .ToList();

                // Clear existing columns and add new columns
                dgvDyeBatch.Columns.Clear();
                dgvDyeBatch.Rows.Clear();

                AddSizeComboBoxColumn("Size", "Size", sizes);
                AddGridColumn("BoxNumber", "Box Number", true);
                AddComboBoxColumn("Grade", "Grade", new List<string> { "A", "B", "C", "D", "E" });
                AddGridColumn("BoxQuantity", "Box Quantity", true);

                //dgvDyeBatch.AllowUserToAddRows = true;
                AddNewRow(1); // Start with Box Number 1
                
            }
            else
            {
                // Clear existing columns if no entries found
                dgvDyeBatch.Columns.Clear();
                dgvDyeBatch.Rows.Clear();
            }
        }

        private void dgvDyeBatch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dgvDyeBatch.Rows[e.RowIndex].IsNewRow)
            {
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

        private void ValidateAndAddRow(int rowIndex)
        {
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
                int nextBoxNumber = rowIndex + 2; // Since rowIndex is 0-based, next box number is rowIndex + 2
                gPreviousRowIndex = rowIndex;
                this.BeginInvoke((MethodInvoker)delegate {
                    AddNewRow(nextBoxNumber);
                    //dgvDyeBatch.AllowUserToAddRows = true;
                });
            }
        }

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
                    MessageBox.Show($"Error saving row: {ex.Message}");
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
            //var sizeDescription = row.Cells["Size"].Value?.ToString();
            var size = _context.TLADM_Sizes.FirstOrDefault(s => s.SI_Description == sizeDescription);
            if (size == null)
            {
                MessageBox.Show($"Size not found: {sizeDescription}");
                return;
            }

            var sizeId = size.SI_id;
            //var grade = row.Cells["Grade"].Value?.ToString();
            //var boxNumber = row.Cells["BoxNumber"].Value?.ToString();
            //var boxQuantity = int.Parse(row.Cells["BoxQuantity"].Value?.ToString() ?? "0");

            if (string.IsNullOrEmpty(grade) || string.IsNullOrEmpty(boxNumber) || boxQuantity == 0)
            {
                MessageBox.Show("Please fill in all fields for each row.");
                return;
            }

            var garmentDyeingProduction = new TLDYE_GarmentDyeingProduction
            {
                GarmentDyeingTransactionNo = int.Parse(selectedTransactionNo),
                Size = sizeId,
                Grade = grade,
                BoxNo = $"{selectedBatchNo}-{boxNumber.PadLeft(2, '0')}",
                BoxQuantity = boxQuantity,
                Closed = closeDyeBatch
            };

            _context.TLDYE_GarmentDyeingProduction.Add(garmentDyeingProduction);
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
