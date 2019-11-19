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
    public partial class frmGreigeReceived3PStockAdjustment : Form
    {
        bool formloaded;

        Util core;
        string[][] MandatoryFields;
        bool[] MandatorySelected;

        bool[] rowTicked;

        DataGridViewTextBoxColumn  oTxtA = new DataGridViewTextBoxColumn();   //   Record primarry Key Number   0
        DataGridViewTextBoxColumn  oTxtB = new DataGridViewTextBoxColumn();   //  Piece No     1
        DataGridViewTextBoxColumn  oTxtC = new DataGridViewTextBoxColumn();   //  TTS Number   2
        DataGridViewTextBoxColumn  oTxtD = new DataGridViewTextBoxColumn();   //  Grade        3
        DataGridViewTextBoxColumn  oTxtE = new DataGridViewTextBoxColumn();   //  Nett Weight  4
     //   DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();  // tick box  5
     //   DataGridViewCheckBoxColumn oChkB = new DataGridViewCheckBoxColumn();  // tick box  6
        public frmGreigeReceived3PStockAdjustment()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            MandatoryFields = new string[][]
            {   new string[] {"cmbTrans", "Please select a transaction", "0"},
                new string[] {"dtpTransactionDate", "Please select a transaction date", "1"},
                new string[] {"cmbStore", "Please select a store", "2"},
                new string[] {"txtApprovedBy", "Please enter the approved by", "3"},
                new string[] {"txtReason", "Please supply a reason for adjustment", "4"}
            };
            core = new Util();
            SetUp(true);
        }

        void SetUp(bool Ind)
        {
            formloaded = false;
            
            using (var context = new TTI2Entities())
            {
                TLADM_LastNumberUsed LNU = context.TLADM_LastNumberUsed.Find(2);
                if (LNU != null)
                {
                    txtAdjustmentNumber.Text = "GA" + LNU.col10.ToString().PadLeft(4, '0');

                }

                cmbTrans.DataSource = context.TLKNI_GreigeCommissionTransctions.OrderBy(x => x.GreigeCom_GrnNo).GroupBy(x => x.GreigeCom_GrnNo).Select(grp => grp.FirstOrDefault()).ToList();
                cmbTrans.DisplayMember = "GreigeCom_GrnNo";
                cmbTrans.ValueMember = "GreigeCom_Pk";

                var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (dept != null)
                {
                    cmbStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == dept.Dep_Id).ToList();
                    cmbStore.DisplayMember = "WhStore_Description";
                    cmbStore.ValueMember = "WhStore_Id";
                    cmbStore.Enabled = true;
                }

                if (Ind)
                {
                    oTxtA = new DataGridViewTextBoxColumn();
                    oTxtA.ValueType = typeof(int);
                    oTxtA.HeaderText = "Key";
                    oTxtA.Visible = false;

                    oTxtB = new DataGridViewTextBoxColumn();
                    oTxtB.ValueType = typeof(string);
                    oTxtB.HeaderText = "Piece No";
                    oTxtB.Visible = true;
                    oTxtB.ReadOnly = true;

                    oTxtC = new DataGridViewTextBoxColumn();
                    oTxtC.ValueType = typeof(string);
                    oTxtC.HeaderText = "Product";
                    oTxtC.ReadOnly = true;
                    oTxtC.Visible = true;

                    oTxtD = new DataGridViewTextBoxColumn();
                    oTxtD.ValueType = typeof(decimal);
                    oTxtD.HeaderText = "Grade";
                    oTxtD.ReadOnly = true;
                    oTxtD.Visible = true;

                    oTxtE = new DataGridViewTextBoxColumn();
                    oTxtE.ValueType = typeof(decimal);
                    oTxtE.HeaderText = "Adjusted Weight";
                    oTxtE.Visible = true;
                    
                    dataGridView1.Columns.Add(oTxtA);
                    dataGridView1.Columns.Add(oTxtB);
                    dataGridView1.Columns.Add(oTxtC);
                    dataGridView1.Columns.Add(oTxtD);
                    dataGridView1.Columns.Add(oTxtE);
                  
                    dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);
                    dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToOrderColumns = false;
                    dataGridView1.AutoGenerateColumns = false;

                }

                MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);

            }
            formloaded = true;
        }

        private void Check_Changed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown  -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown  += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int _LastNumber = 0;
            TLKNI_GreigeProduction GreigeP = null;

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                var GrnNumber = (TLKNI_GreigeCommissionTransctions)cmbTrans.SelectedItem;

                using (var context = new TTI2Entities())
                {
                     TLADM_LastNumberUsed LNU = context.TLADM_LastNumberUsed.Find(2);
                     if (LNU != null)
                     {
                         _LastNumber = LNU.col10;
                         LNU.col10 += 1;
                     }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var Pk = (int)row.Cells[0].Value;
                        if (Pk != 0)
                        {
                            var Existing = context.TLKNI_GreigeCommissionTransctions.Find(Pk);
                            if (Existing != null)
                            {
                                var NewValue = (decimal)row.Cells[4].Value;
                                if (NewValue != Existing.GreigeCom_NettWeight)
                                {
                                    Existing.GreigeCom_NettWeight += NewValue;

                                    //Notes --  we must also now adjust the value in the GreigeProduction File 
                                    //====================================================================

                                    GreigeP = context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_KnitO_Fk == Pk).FirstOrDefault();
                                    if (GreigeP != null)
                                    {
                                        GreigeP.GreigeP_weight += NewValue;
                                        GreigeP.GreigeP_weightAvail += NewValue;

                                        TLKNI_GreigeCommisionAdjustment adj = new TLKNI_GreigeCommisionAdjustment();
                                        adj.GreigeComAJ_AjustmentNo = _LastNumber;
                                        adj.GreigeComAJ_AmtAdjusted = NewValue;
                                        adj.GreigeComAJ_AprovedBy = txtApprovedBy.Text;
                                        adj.GreigeComAJ_PieceNo_FK = (int)row.Cells[0].Value;
                                        adj.GreigeComAJ_Reasons = txtReason.Text;
                                        adj.GreigeComAJ_Strore_FK = (int)cmbStore.SelectedValue;
                                        adj.GreigeComAJ_TransDate = (DateTime)dtpTransactionDate.Value;
                                        adj.GreigeComAJ_GreigeProduction_FK = GreigeP.GreigeP_Pk;

                                        if (GrnNumber != null)
                                            adj.GreigeComAJ_GrnNumber = GrnNumber.GreigeCom_GrnNo;

                                        context.TLKNI_GreigeCommisionAdjustment.Add(adj);
                            
                                    }
                                }
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        dataGridView1.Rows.Clear();
                        SetUp(false);

                        frmKnitViewRep vRep = new frmKnitViewRep(19, _LastNumber);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmbTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var selected = (TLKNI_GreigeCommissionTransctions)cmbTrans.SelectedItem;
                    if (selected != null)
                    {
                        var Existing = context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_GrnNo == selected.GreigeCom_GrnNo).ToList();
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.GreigeCom_PK;
                            dataGridView1.Rows[index].Cells[1].Value = row.GreigeCom_PieceNo;
                            var prod = context.TLADM_Griege.Find(row.GreigeCom_ProductType_FK);
                            if (prod != null)
                            {
                                dataGridView1.Rows[index].Cells[2].Value = prod.TLGreige_Description;
                            }
                            dataGridView1.Rows[index].Cells[3].Value = row.GreigeCom_Grade;
                            dataGridView1.Rows[index].Cells[4].Value = row.GreigeCom_NettWeight;
                            // dataGridView1.Rows[index].Cells[5].Value = false;
                            // dataGridView1.Rows[index].Cells[6].Value = false;
                        }

                        rowTicked = core.PopulateArray(dataGridView1.RowCount, false);

                        var result = (from u in MandatoryFields
                                      where u[0] == oCmbo.Name
                                      select u).FirstOrDefault();
                        if (result != null)
                        {
                            int nbr = Convert.ToInt32(result[2].ToString());
                            MandatorySelected[nbr] = true;
                        }

                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[4];
                            this.dataGridView1.BeginEdit(true);
                        }
                    }
                }
            }
        }

        private void dtpTransactionDate_ValueChanged(object sender, EventArgs e)
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

        private void cmbStore_SelectedIndexChanged(object sender, EventArgs e)
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

        private void OnLeave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandatorySelected[nbr] = true;
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
    }
}
