using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        protected readonly TTI2Entities _context;
        DataTable DataT;
        BindingSource BindS;

        public frmGarmentDyeingBatchApproval()
        {
            InitializeComponent();

            _context = new TTI2Entities();
            
            cboBatchNo.SelectedIndexChanged += new EventHandler(cboBatchNo_SelectedIndexChanged);

            //DataT = new DataTable();
            //BindS = new BindingSource();
            //DataColumn Column;

            //dgvDyeBatch.AutoGenerateColumns = false;
            //dgvDyeBatch.AllowUserToAddRows = false;

            ////==========================================================================================
            //// 1st task is to create the data table
            //// Col 0
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(int);
            //Column.ColumnName = "RFD_Pk";
            //Column.Caption = "RFD Key";
            //Column.DefaultValue = 0;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 1
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(bool);
            //Column.ColumnName = "RFD_Select";
            //Column.Caption = "Select";
            //Column.DefaultValue = false;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 2
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(string);
            //Column.ColumnName = "RFD_TransNo";
            //Column.Caption = "TransNumber";
            //Column.DefaultValue = string.Empty;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 3
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(string);
            //Column.ColumnName = "New_Colour";
            //Column.Caption = "New Colour";
            //Column.DefaultValue = string.Empty;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 4
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(int);
            //Column.ColumnName = "No_OfGradeA";
            //Column.Caption = "No of Grade A";
            //Column.DefaultValue = 0;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 5
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(int);
            //Column.ColumnName = "No_OfGradeB";
            //Column.Caption = "No of Grade B";
            //Column.DefaultValue = 0;
            //DataT.Columns.Add(Column);

            ////==========================================================================================
            //// Col 6
            ////=====================================================================
            //Column = new DataColumn();
            //Column.DataType = typeof(int);
            //Column.ColumnName = "No_OfGradeC";
            //Column.Caption = "No of Other Grades";
            //Column.DefaultValue = 0;
            //DataT.Columns.Add(Column);
            
            ////======================================================
            //// DataGridView 
            ////==========================================================
            //DataGridViewTextBoxColumn selecta = new DataGridViewTextBoxColumn();
            //selecta.Name = "PrRatingIndex";
            //selecta.ValueType = typeof(Int32);
            //selecta.DataPropertyName = DataT.Columns[0].ColumnName;
            //selecta.HeaderText = "Rating Index";
            //selecta.Visible = false;
            //selecta.ReadOnly = true;
            //dgvDyeBatch.Columns.Add(selecta);
            //dgvDyeBatch.Columns[0].DisplayIndex = 0;

            //DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
            //oChkA.Name = "Select";
            //oChkA.ValueType = typeof(bool);
            //oChkA.HeaderText = "Select";
            //oChkA.DataPropertyName = DataT.Columns[1].ColumnName;
            //oChkA.Visible = true;
            //dgvDyeBatch.Columns.Add(oChkA);
            //dgvDyeBatch.Columns[1].DisplayIndex = 1;

            //DataGridViewTextBoxColumn selectb = new DataGridViewTextBoxColumn();
            //selectb.Name = "TransNumber";
            //selectb.ValueType = typeof(string);
            //selectb.DataPropertyName = DataT.Columns[2].ColumnName;
            //selectb.HeaderText = "Trans Number";
            //selectb.ReadOnly = true;
            //selectb.Visible = true;
            //dgvDyeBatch.Columns.Add(selectb);
            //dgvDyeBatch.Columns[2].DisplayIndex = 2;

            //DataGridViewTextBoxColumn selectd = new DataGridViewTextBoxColumn();
            //selectd.Name = "ToColour";
            //selectd.ValueType = typeof(string);
            //selectd.DataPropertyName = DataT.Columns[3].ColumnName;
            //selectd.HeaderText = "To Colour";
            //selectd.ReadOnly = true;
            //selectd.Visible = true;
            //dgvDyeBatch.Columns.Add(selectd);
            //dgvDyeBatch.Columns[3].DisplayIndex = 3;

            //DataGridViewTextBoxColumn selecte = new DataGridViewTextBoxColumn();
            //selecte.Name = "GradeA";
            //selecte.ValueType = typeof(int);
            //selecte.DataPropertyName = DataT.Columns[4].ColumnName;
            //selecte.HeaderText = "Grade A";
            //selecte.Visible = true;
            //dgvDyeBatch.Columns.Add(selecte);
            //dgvDyeBatch.Columns[4].DisplayIndex = 4;

            //DataGridViewTextBoxColumn selectf = new DataGridViewTextBoxColumn();
            //selectf.Name = "GradeA";
            //selectf.ValueType = typeof(int);
            //selectf.DataPropertyName = DataT.Columns[5].ColumnName;
            //selectf.HeaderText = "Grade B";
            //selectf.Visible = true;
            //dgvDyeBatch.Columns.Add(selectf);
            //dgvDyeBatch.Columns[5].DisplayIndex = 5;

            //DataGridViewTextBoxColumn selectg = new DataGridViewTextBoxColumn();
            //selectg.Name = "GradeOther";
            //selectg.ValueType = typeof(int);
            //selectg.DataPropertyName = DataT.Columns[6].ColumnName;
            //selectg.HeaderText = "Grade Other";
            //selectg.Visible = true;
            //dgvDyeBatch.Columns.Add(selectg);
            //dgvDyeBatch.Columns[6].DisplayIndex = 6;

            //BindS.DataSource = DataT;
            //dgvDyeBatch.DataSource = BindS;
        }

        private void frmGarmentDyeingBatchApproval_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            cboBatchNo.DataSource = null;
            cboMachine.DataSource = null;

            LoadBatchNumbers();
            LoadMachineDefinitions();
            LoadShifts();

            FormLoaded = true;
        }

        private void LoadBatchNumbers()
        {
            var batchNumbers = _context.TLDYE_RFDHistory
                .Where(x => !x.DyeRFD_Completed)
                .GroupBy(x => x.DyeRFD_Transaction_No)
                .Select(g => "GD000" + g.FirstOrDefault().DyeRFD_Transaction_No.ToString())
                .ToList();

            cboBatchNo.DataSource = batchNumbers;
            cboBatchNo.SelectedIndex = -1;

            dgvDyeBatch.AutoGenerateColumns = false;
            dgvDyeBatch.ReadOnly = false;
            dgvDyeBatch.Columns.Clear();
        }

        // Handle ComboBox selection change for Batch No
        private void cboBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBatchNo.SelectedIndex == -1) return;

            var selectedTransactionNo = cboBatchNo.SelectedItem.ToString().Replace("GD000", "");
            var transactionEntry = _context.TLDYE_RFDHistory
                .Where(x => x.DyeRFD_Transaction_No.ToString() == selectedTransactionNo)
                .FirstOrDefault();

            if (transactionEntry != null)
            {
                dtpDateDyed.Value = transactionEntry.DyeRFD_BeginDyeDate ?? DateTimePicker.MinimumDateTime; // Use DateTimePicker.MinimumDateTime if the date is null
            }
            var transactionEntries = _context.TLDYE_RFDHistory
                .Where(x => x.DyeRFD_Transaction_No.ToString() == selectedTransactionNo)
                .ToList();
          
            AddSelectColumn();
            AddGridColumn("DyeRFD_Transaction_No", "Transaction No", false);
            AddGridColumn("DyeRFD_CurrentStyle", "Style", false);
            AddGridColumn("DyeRFD_DyeToColour", "Colour", false);
            AddSizeComboBoxColumn("Size", "Size");
            AddGridColumn("BoxNumber", "Box Number", true); 
            AddGridColumn("BoxQuantity", "Box Quantity", true);
            AddGridColumn("Grade", "Grade", true);

            var _Styles = _context.TLADM_Styles.ToList(); // Assuming this contains the styles
            var _Colours = _context.TLADM_Colours.ToList();
            var _Sizes = _context.TLADM_Sizes.ToList();
            var displayEntries = transactionEntries.Select(entry => new
                {
                    entry.DyeRFD_Transaction_No,
                DyeRFD_CurrentStyle = _Styles.FirstOrDefault(s => s.Sty_Id == entry.DyeRFD_CurrentStyle)?.Sty_Description,
                DyeRFD_DyeToColour = _Colours.FirstOrDefault(s => s.Col_Id == entry.DyeRFD_DyeToColour)?.Col_Display,
                Size = "", // Placeholder for ComboBox selection
                entry.DyeRFD_NoumberOfAGrades,
                entry.DyeRFD_NumberOfBGrades,

            }).ToList();
            dgvDyeBatch.DataSource = displayEntries;
        }

        private void AddSizeComboBoxColumn(string dataPropertyName, string headerText)
        {
            var _Sizes = _context.TLADM_Sizes
                            .Where(x => !x.SI_Discontinued)
                            .OrderBy(x => x.SI_DisplayOrder)
                            .ToList();

            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = headerText,
                Name = dataPropertyName,
                DataSource = _Sizes,
                DisplayMember = "SI_Description", // Adjust DisplayMember based on your TLADM_Sizes properties
                ValueMember = "SI_id", // Adjust ValueMember based on your TLADM_Sizes properties
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };

            dgvDyeBatch.Columns.Add(comboBoxColumn);
        }


        private void AddGridColumn(string dataPropertyName, string headerText, bool isEditable = false)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName,
                ReadOnly = !isEditable
            };
            dgvDyeBatch.Columns.Add(column);
        }

        private void AddSelectColumn()
        {
            var selectColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Select",
                Name = "SelectColumn"
            };
            dgvDyeBatch.Columns.Add(selectColumn);
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

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if(oBtn != null && FormLoaded)
            {
                foreach(DataRow Row in DataT.Rows)
                {
                    if(!Row.Field<bool>(1))
                    {
                        continue;
                    }

                    var TrnsNum = Row.Field<int>(0);

                    var Entries = _context.TLDYE_RFDHistory.Where(x => x.DyeRFD_Transaction_No == TrnsNum && !x.DyeRFD_Completed).ToList();
                    foreach(var Entry in Entries)
                    {
                        Entry.DyeRFD_Completed = true;
                        Entry.DyeRFD_NoumberOfAGrades = Row.Field<int>(4);
                        Entry.DyeRFD_NumberOfBGrades = Row.Field<int>(5);
                        Entry.DyeRFD_NumberOfOtherGrades = Row.Field<int>(6);

                        var StockOnHand = _context.TLCSV_StockOnHand.Find(Entry.DyeRFD_StockOnHand_Fk);
                        if(StockOnHand != null)
                        {
                            StockOnHand.TLSOH_Style_FK = (int)(from T1 in _context.TLADM_Styles
                                      join T2 in _context.TLADM_StyAssoc
                                      on T1.Sty_Id equals T2.StyAssoc_StyPk
                                      select T2).FirstOrDefault().StyAssoc_StyOther;

                            StockOnHand.TLSOH_Colour_FK = Entry.DyeRFD_DyeToColour;
                            if (Entry.DyeRFD_NoumberOfAGrades != 0)
                            {
                                StockOnHand.TLSOH_BoxedQty = Entry.DyeRFD_NoumberOfAGrades;
                            }
                        }
                    }
                }

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data successfully saved to database");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void frmGarmentDyeingBatchApproval_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
