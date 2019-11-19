using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Knitting
{
    public partial class frmGreigeRecording : Form
    {
      

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // DataTable key     0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Piece No          1
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Piece Grade       2
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Piece Remarks     3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // No 1              4
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // No 2              5
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // No 3              6
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // No 4              7
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // No 5              8
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // No 6              9
        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn();   // No 7             10
        DataGridViewTextBoxColumn oTxtM = new DataGridViewTextBoxColumn();   // No 8             11
        DataGridViewTextBoxColumn oTxtN = new DataGridViewTextBoxColumn();   // No 9             12
        DataGridViewCheckBoxColumn oTxtO = new DataGridViewCheckBoxColumn(); // No 10            13
        DataGridViewCheckBoxColumn oTxtP = new DataGridViewCheckBoxColumn(); // No 11            14
        DataGridViewCheckBoxColumn oTxtQ = new DataGridViewCheckBoxColumn(); // No 12            15
        object[] ColumnHeadings;

        Util core;
        bool formloaded;

        int[] ColumnKeys;
        bool[] ColumnGrades;
        bool[] ProductionSplit;
        bool InspectionBegin;

        string[][] MandatoryFields;
        bool[] MandatorySelected;

        IList<TLADM_Griege> _Grieg = null;
        IList<TLADM_QualityDefinition> __QualityDefinitions = null;

        public frmGreigeRecording()
        {
            InitializeComponent();
            ColumnHeadings = new Object[16];
            oTxtA.Visible = false;

            ColumnHeadings[0]  = oTxtA; 
            ColumnHeadings[1]  = oTxtB; 
            ColumnHeadings[2]  = oTxtC; 
            ColumnHeadings[3]  = oTxtD; 
            ColumnHeadings[4]  = oTxtE; 
            ColumnHeadings[5]  = oTxtF; 
            ColumnHeadings[6]  = oTxtG; 
            ColumnHeadings[7]  = oTxtH; 
            ColumnHeadings[8]  = oTxtJ; 
            ColumnHeadings[9]  = oTxtK; 
            ColumnHeadings[10] = oTxtL;
            ColumnHeadings[11] = oTxtM;
            ColumnHeadings[12] = oTxtN;
            ColumnHeadings[13] = oTxtO;
            ColumnHeadings[14] = oTxtP;
            ColumnHeadings[15] = oTxtQ;
        
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            MandatoryFields = new string[][]
                {   new string[] {"cmbKnitOrder", "Please select an knit order", "0"},
                    new string[] {"cmbInspector", "Please select an inspector", "1"},
                    new string[] {"dateTimePicker1", "Please select a date", "2"}
                };

            SetUp();
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (formloaded && oDgv.Focused)
            {
                if (oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex > 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------
        void SetUp()
        {
            formloaded = false;
            core = new Util();

            txtGreigeQual.Text = string.Empty;

            InspectionBegin = false;

            using ( var context = new TTI2Entities())
            {
                _Grieg = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).ToList();
                
                cmbKnitOrder.DataSource = context.TLKNI_Order.Where(x =>!x.KnitO_Closed).OrderBy(x=>x.KnitO_OrderNumber).ToList();
                cmbKnitOrder.ValueMember = "KnitO_Pk";
                cmbKnitOrder.DisplayMember = "KnitO_OrderNumber";
                cmbKnitOrder.SelectedValue = 0;

                var Depts = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if(Depts != null)
                {
                    cmbInspector.DataSource = context.TLADM_MachineOperators.Where(X => X.MachOp_Department_FK == Depts.Dep_Id && X.MachOp_Inspector && !X.MachOp_Discontinued).ToList();
                    cmbInspector.ValueMember = "MachOp_Pk";
                    cmbInspector.DisplayMember = "MachOp_Description";
                    cmbInspector.SelectedValue = 0;
                  
                    var h2 = (DataGridViewTextBoxColumn)ColumnHeadings[0];
                    h2.HeaderText = "Key";
                    h2.Visible = false;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[1];
                    h2.HeaderText = "Piece No";
                    h2.ValueType = typeof(string);
                    h2.ReadOnly = true;
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[2];
                    h2.HeaderText = "Grade";
                    h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
                    h2.HeaderText = "Remarks";
                    h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);
                    
                    __QualityDefinitions = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x=>x.QD_ShortCode).ToList();
                    ColumnKeys = core.PopulateNumArray(__QualityDefinitions.Count, 0);
                    ColumnGrades = core.PopulateArray(__QualityDefinitions.Count, false);
                    ProductionSplit = core.PopulateArray(__QualityDefinitions.Count, false);

                    foreach (var Definition in __QualityDefinitions)
                    {
                        foreach (var elementH in ColumnHeadings)
                        {
                            var ch = (DataGridViewTextBoxColumn)elementH;
                            if(String.IsNullOrEmpty(ch.HeaderText))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(Definition.QD_ShortCode);
                                sb.Append(Environment.NewLine);
                                sb.Append(Definition.QD_Description);
                                ch.HeaderText = sb.ToString();
                                ch.ValueType = typeof(int);
                                ch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1.Columns.Add(ch);
                                ColumnKeys[-4 + ch.Index] = Definition.QD_Pk;
                                ColumnGrades[-4 + ch.Index] = Definition.QD_GradeCol;
                                ProductionSplit[-4 + ch.Index] = Definition.QD_SplitCol;

                                break;
                            }
                        }
                    }
                    
                    DataGridViewCheckBoxColumn h3 = (DataGridViewCheckBoxColumn)ColumnHeadings[- 1 + ColumnHeadings.Length];
                    h3.HeaderText = "Inspected";
                    h3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    h3.ValueType = typeof(Boolean);
                    dataGridView1.Columns.Add(h3);

                    h3 = (DataGridViewCheckBoxColumn)ColumnHeadings[-2 + ColumnHeadings.Length];
                    h3.HeaderText = "K / R";
                    h3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    h3.ValueType = typeof(Boolean);
                    dataGridView1.Columns.Add(h3);

                    h3 = (DataGridViewCheckBoxColumn)ColumnHeadings[-3 + ColumnHeadings.Length];
                    h3.HeaderText = "White Only";
                    h3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    h3.ValueType = typeof(Boolean);
                    h3.Visible = false;
                    dataGridView1.Columns.Add(h3);
                }
            }

            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;

        }

        //---------------------------------------------------------------------------

        private void cmbKnitOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            DataGridView oDgv = dataGridView1;

            if (oCmbo != null && formloaded)
            {
                var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                if (KO != null)
                {
                    oDgv.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        txtGreigeQual.Text = context.TLADM_Griege.Find(KO.KnitO_Product_FK).TLGreige_Description;

                        formloaded = false;
                        
                        var Existing = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk && x.GreigeP_Captured).OrderBy(x=>x.GreigeP_PieceNo).ToList();
                        foreach (var row in Existing)
                        {
                            var index = oDgv.Rows.Add();
                            oDgv.Rows[index].Cells[0].Value = row.GreigeP_Pk;
                            oDgv.Rows[index].Cells[1].Value = row.GreigeP_PieceNo;
                            oDgv.Rows[index].Cells[2].Value = row.GreigeP_Grade;
                            oDgv.Rows[index].Cells[3].Value = row.GreigeP_Remarks;
                            oDgv.Rows[index].Cells[4].Value = row.GreigeP_Meas1;
                            oDgv.Rows[index].Cells[5].Value = row.GreigeP_Meas2;
                            oDgv.Rows[index].Cells[6].Value = row.GreigeP_Meas3;
                            oDgv.Rows[index].Cells[7].Value = row.GreigeP_Meas4;
                            oDgv.Rows[index].Cells[8].Value = row.GreigeP_Meas5;
                            oDgv.Rows[index].Cells[9].Value = row.GreigeP_Meas6;
                            oDgv.Rows[index].Cells[10].Value = row.GreigeP_Meas7;
                            oDgv.Rows[index].Cells[11].Value = row.GreigeP_Meas8;
                            oDgv.Rows[index].Cells[12].Value = row.GreigeP_Inspected;
                            oDgv.Rows[index].Cells[13].Value = false;
                            oDgv.Rows[index].Cells[14].Value = row.GreigeP_WarningMessage; 
                           
                        }

                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[2];
                            this.dataGridView1.BeginEdit(true);
                        }
                        formloaded = true;

                    }

                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandatorySelected[nbr] = true;
                     }

                    cmbInspector.Focus();

                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           // Nolonger required;
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           // This property has been replaced with a click event
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool success = true;
            TLADM_TranactionType TranType = null;
            TLADM_MachineOperators Operators = null;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }
              
                var Inspector = (TLADM_MachineOperators)cmbInspector.SelectedItem;
                var KO  = (TLKNI_Order)cmbKnitOrder.SelectedItem;
   
                using ( var context = new TTI2Entities())
                {
                    Operators = (TLADM_MachineOperators)cmbInspector.SelectedItem;

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        TLKNI_GreigeProduction record = new TLKNI_GreigeProduction();
                        record = context.TLKNI_GreigeProduction.Find(row.Cells[0].Value);

                        if (row.Cells[2].Value != null)
                            record.GreigeP_Grade = row.Cells[2].Value.ToString();
                        else
                            record.GreigeP_Grade = "A";
                        
                        if(row.Cells[3].Value != null)
                            record.GreigeP_Remarks = row.Cells[3].Value.ToString();
                        else
                            record.GreigeP_Remarks = string.Empty;

                        record.GreigeP_Meas1 = (int)row.Cells[4].Value;
                        record.GreigeP_Meas2 = (int)row.Cells[5].Value;
                        record.GreigeP_Meas3 = (int)row.Cells[6].Value;
                        record.GreigeP_Meas4 = (int)row.Cells[7].Value;
                        record.GreigeP_Meas5 = (int)row.Cells[8].Value;
                        record.GreigeP_Meas6 = (int)row.Cells[9].Value;
                        record.GreigeP_Meas7 = (int)row.Cells[10].Value;
                        record.GreigeP_Meas8 = (int)row.Cells[11].Value;

                        record.GreigeP_Inspected = (bool)row.Cells[12].Value;
                        
                        record.GreigeP_InspDate = dateTimePicker1.Value;
                        record.GreigeP_Inspector_FK = Inspector.MachOp_Pk;

                        record.GreigeP_WarningMessage = (bool)row.Cells[14].Value;
                        if(row.Cells[2].Value != null)
                        {
                            if (row.Cells[2].Value.ToString().Contains("C"))
                            {
                                // We need to record a transaction 
                                //-----------------------------------------------

                                TLKNI_GreigeTransactions GreigeT = new TLKNI_GreigeTransactions();

                                GreigeT.GreigeT_AdjustedWeight = record.GreigeP_weight;
                                GreigeT.GreigeT_TransactionDate = dateTimePicker1.Value;
                                GreigeT.GreigeT_TransactionNumber = 9999;
                                GreigeT.GreigeT_Grade = row.Cells[2].Value.ToString();
                                GreigeT.GreigeT_KOrder_FK = KO.KnitO_Pk;
                                GreigeT.GreigeT_Piece_FK = record.GreigeP_Pk;

                                if (KO.KnitO_YarnO_FK != null)
                                {
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1500).FirstOrDefault();
                                    if (TranType != null)
                                    {
                                        record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                    }
                                }
                                else
                                {
                                    if (KO.KnitO_CommisionCust)
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1700).FirstOrDefault();
                                        if (TranType != null)
                                        {
                                            record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                        }
                                    }
                                    else
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1600).FirstOrDefault();
                                        if (TranType != null)
                                        {
                                            record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                        }
                                    }
                                }

                                GreigeT.GreigeT_TransactionType_FK = TranType.TrxT_Pk;
                                context.TLKNI_GreigeTransactions.Add(GreigeT);
                            }
                            else
                            {
                                if (KO.KnitO_YarnO_FK != null)
                                {
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                                    if (TranType != null)
                                    {
                                        record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                    }
                                }
                                else
                                {
                                    if (KO.KnitO_CommisionCust)
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1300).FirstOrDefault();
                                        if (TranType != null)
                                        {
                                            record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                        }
                                    }
                                    else
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1200).FirstOrDefault();
                                        if (TranType != null)
                                        {
                                            record.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                        }
                                    }
                                }
  
                            }
                        }
                    } 

                    try
                    {
                        context.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                        success = false;
                    }
                }

                if(success)
                {
                    MessageBox.Show("Records stored successfully to the database");
                    dataGridView1.Rows.Clear();
                }
                 
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                if (KO != null)
                {
                    YarnReportOptions repOpts = new YarnReportOptions();

                    repOpts.ReportKey = KO.KnitO_Pk;

                    DialogResult res = MessageBox.Show("Print Report with Current Results", "Print Options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        repOpts.PrintWithCurrent = true;
                    }
                    else
                    {
                        repOpts.PrintWithCurrent = false;
                    }

                    frmKnitViewRep vRep = new frmKnitViewRep(9, repOpts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
            }
        }

        private void cmbInspector_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandatorySelected[nbr] = true;
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandatorySelected[nbr] = true;
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null & formloaded)
            {
                var Cell = oDgv.CurrentCell;
                if (Cell != null)
                {
                    /*
                    if(ProductionSplit[-4 + Cell.ColumnIndex])
                    {
                        DialogResult res = MessageBox.Show("Do you want split this production", "Greige Production Split", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == res)
                        {
                            var CurrentRow = oDgv.CurrentRow;

                            int PrimaryKey = (int)CurrentRow.Cells[0].Value;
                            if (PrimaryKey != 0)
                            {
                                frmProductionSplit ProdSplit = new frmProductionSplit(PrimaryKey);
                                ProdSplit.ShowDialog(this);
                            }
                            
                        }
 
                    }
                     * */
                   //  MessageBox.Show(Cell.ColumnIndex.ToString(), Cell.EditedFormattedValue.ToString());
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              DataGridView oDgv = sender as DataGridView;

              if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell && e.ColumnIndex == 13)
              {
                  if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                  {
                      var CurrentRow = oDgv.CurrentRow;

                      int PrimaryKey = (int)CurrentRow.Cells[0].Value;
                      if (PrimaryKey != 0)
                      {
                              frmProductionSplit ProdSplit = new frmProductionSplit(PrimaryKey);
                              ProdSplit.ShowDialog(this);

                              dataGridView1.Rows[CurrentRow.Index].Cells[13].Value = false;
                      }
                  }
              }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                var CurrentCell = oDgv.CurrentCell;
                if (CurrentCell.ReadOnly && dataGridView1.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1].Visible)
                    dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
            else if (oDgv != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (dataGridView1.CurrentCell.ReadOnly)
                        dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 2)
            {
                if (!InspectionBegin)
                    InspectionBegin = true;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            int OtherF = 0;
            int MeasuredF = 0;
            int FaultsAllowed = 0;

            if (oDgv.Focused && e.ColumnIndex == 12)
            {
                DataGridViewCheckBoxCell cell = oDgv.CurrentRow.Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                if (!(bool)cell.EditedFormattedValue)
                {
                    DataGridViewRow CurrentRow = oDgv.CurrentRow;
                    foreach (DataGridViewCell Cell in CurrentRow.Cells)
                    {
                        if (Cell.ColumnIndex == 0)
                            continue;

                        if (Cell.ValueType == typeof(Int32))
                        {
                            int Pk = ColumnKeys[-4 + Cell.ColumnIndex];

                            var QD = __QualityDefinitions.FirstOrDefault(s => s.QD_Pk == Pk);
                            if (QD != null)
                            {
                                if (!QD.QD_Measurable)
                                {
                                    int TotalFaults = 0;
                                    int.TryParse(Cell.EditedFormattedValue.ToString(), out TotalFaults);
                                    OtherF += TotalFaults;
                                }
                                else
                                {
                                    int TotalFaults = 0;
                                    int.TryParse(Cell.EditedFormattedValue.ToString(), out TotalFaults);
                                    MeasuredF += TotalFaults;
                                }
                            }
                        }
                    }

                    var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                    if (KO != null)
                    {
                        var FabQual = _Grieg.FirstOrDefault(x => x.TLGreige_Id == KO.KnitO_Product_FK);
                        if (FabQual != null)
                        {
                            FaultsAllowed = FabQual.TLGreige_FaultsAllowed;
                        }
                    }
                    
                    if (MeasuredF <= FaultsAllowed)
                    {
                            CurrentRow.Cells[2].Value = "A";
                            CurrentRow.Cells[14].Value = false;
                            if (CurrentRow.Cells[3].Value != null)
                            {
                                CurrentRow.Cells[14].Value = true;
                            }
                    }
                    else if (MeasuredF > FaultsAllowed && MeasuredF <= 20)
                            CurrentRow.Cells[2].Value = "B";
                    else
                            CurrentRow.Cells[2].Value = "C";
                }
                else
                {
                    var CurrentRow = oDgv.CurrentRow;

                    CurrentRow.Cells[2].Value = string.Empty;
                }
            }
            
        }
    
    }
}
