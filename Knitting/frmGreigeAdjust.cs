using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Knitting
{
    public partial class frmGreigeAdjust : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();    // DataTable key     0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();    // Piece No          1
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();    // Grade             2 
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();    // Adjusted Weight   3 
        
        object[] ColumnHeadings;

        string[][] MandatoryFields;
        bool[] MandatorySelected;

        bool[] rowTicked;

        int MLNU;
        string MCode;
 
        Util core;
        bool formloaded;

        public frmGreigeAdjust()
        {
            InitializeComponent();

            MandatoryFields = new string[][]
            {   new string[] {"dtpAdjustDate", "Please select a transaction date", "0"},
                new string[] {"cmbKnitOrder", "Please select a knit order", "1"},
                new string[] {"txtApprovedBy", "Please enter the persons name who authoised this", "2"},
                new string[] {"cmboOperator", "Please select the operators name", "3"}
            };

            ColumnHeadings = new Object[6];
            oTxtA.Visible = false;
            oTxtC.ReadOnly = true;
            ColumnHeadings[0] = oTxtA;
            ColumnHeadings[1] = oTxtB;
            ColumnHeadings[2] = oTxtC;
            ColumnHeadings[3] = oTxtD;
           
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            SetUp(true);
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    }
                   
                }
            }
        }

        void SetUp(bool Updt)
        {
            formloaded = false;
            core = new Util();

            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(2);

                if (LNU != null)
                {
                    txtGreigeAdjNo.Text = "GA" + LNU.col8.ToString().PadLeft(6).Replace(' ', '0'); ;
                }

                cmbKnitOrder.DataSource = context.TLKNI_Order.Where(x => !x.KnitO_Closed).ToList();
                cmbKnitOrder.ValueMember = "KnitO_Pk";
                cmbKnitOrder.DisplayMember = "KnitO_OrderNumber";
                if (Updt)
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Dept != null)
                    {
                        cmboOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                        cmboOperator.ValueMember = "MachOP_Pk";
                        cmboOperator.DisplayMember = "MachOP_Description";

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
                        h2.ValueType = typeof(string);
                        h2.ReadOnly = true;
                        dataGridView1.Columns.Add(h2);

                        h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
                        h2.HeaderText = " Adjusted Weight";
                        h2.Width = 170;
                        h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        h2.ValueType = typeof(decimal);
                        dataGridView1.Columns.Add(h2);
                    }
                }
            }

            formloaded = true;
        }

        private void txt(object sender, EventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;
            if (oTxtBx != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxtBx.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                if (oTxtBx.TextLength > 0)
                    MandatorySelected[nbr] = true;
                else
                {
                    MandatorySelected[nbr] = false;
                }
            }
        }

        private void cmbKnitOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            DataGridView oDgv = dataGridView1;

            if (oCmbo != null && formloaded)
            {
                var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                if (KO != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();
                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandatorySelected[nbr] = true;
                    }
                    using (var context = new TTI2Entities())
                    {
                        var Machine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                        if (Machine != null)
                        {
                            MCode = Machine.MD_MachineCode.Remove(0, 1).Trim();
                            MLNU = Machine.MD_LastNumberUsed;
                        }
                        var Existing = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk).ToList();
                        foreach (var row in Existing)
                        {
                            var index = oDgv.Rows.Add();
                            oDgv.Rows[index].Cells[0].Value = row.GreigeP_Pk;
                            oDgv.Rows[index].Cells[1].Value = row.GreigeP_PieceNo;
                            oDgv.Rows[index].Cells[2].Value = row.GreigeP_Grade;
                            oDgv.Rows[index].Cells[3].Value = Math.Round(row.GreigeP_weight, 1);
                        }
                        
                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[3];
                            this.dataGridView1.BeginEdit(true);
                        }

                        rowTicked = core.PopulateArray(dataGridView1.RowCount, false);
                    }
                }
            }
        }

        private void cmboOperator_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLADM_TranactionType TranType = null;
            int GProdKey = 0;
            TLADM_Departments Dept = null;

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, true, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;

                }

                var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                if (KO != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        // We may have added some records 
                        //--------------------------------------------
                        var Machine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                        if (Machine != null)
                        {
                            if (MLNU > Machine.MD_LastNumberUsed)
                            {
                                Machine.MD_LastNumberUsed = MLNU + 1;
                            }
                        }

                        var LNU = context.TLADM_LastNumberUsed.Find(2);
                        if (LNU != null)
                        {
                            LNU.col8 += 1;
                        }

                        Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                        if (Dept != null)
                        {
                            if (KO.KnitO_YarnO_FK == null)
                            {
                                if (KO.KnitO_CommisionCust)
                                {
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2002).FirstOrDefault();

                                }
                                else
                                {
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2003).FirstOrDefault();
                                }
                            }
                            else
                            {
                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2001).FirstOrDefault();
                            }
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value == null)
                                continue;

                            var Pk = (int)row.Cells[0].Value;
                            if (Pk != 0)
                            {
                                var GreigeProd = context.TLKNI_GreigeProduction.Find(Pk);
                                if (GreigeProd != null)
                                {
                                    var NewValue = (Decimal)row.Cells[3].Value;
                                    if (NewValue != GreigeProd.GreigeP_weight)
                                    {
                                        GreigeProd.GreigeP_weight += NewValue;


                                        TLKNI_GreigeTransactions GreigeT = new TLKNI_GreigeTransactions();
                                        GreigeT.GreigeT_AdjustedWeight = NewValue;

                                        if (row.Cells[2].Value != null)
                                            GreigeT.GreigeT_Grade = row.Cells[2].Value.ToString();
                                        else
                                            GreigeT.GreigeT_Grade = string.Empty;

                                        GreigeT.GreigeT_KOrder_FK = KO.KnitO_Pk;

                                        if ((int)row.Cells[0].Value == 0)
                                            GreigeT.GreigeT_Piece_FK = GProdKey;
                                        else
                                            GreigeT.GreigeT_Piece_FK = (int)row.Cells[0].Value;

                                        GreigeT.GreigeT_TransactionDate = dtpAdjustDate.Value;
                                        GreigeT.GreigeT_TransactionNumber = LNU.col8 - 1;
                                        GreigeT.GreigeT_TransactionType_FK = TranType.TrxT_Pk;
                                        GreigeT.GreigeT_ApprovedBy = txtApprovedBy.Text;
                                        context.TLKNI_GreigeTransactions.Add(GreigeT);
                                    }
                                }
                            }
                        }


                        string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                                .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                                .ToString();


                        TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                        DailyLog.TLDL_IPAddress = Mach_IP;
                        DailyLog.TLDL_Dept_Fk = Dept.Dep_Id;
                        DailyLog.TLDL_Date = DateTime.Now;
                        DailyLog.TLDL_TransDetail = "Greige Adjustment";
                        DailyLog.TLDL_AuthorisedBy = txtApprovedBy.Text;
                        DailyLog.TLDL_Comments =  txtGreigeAdjNo.Text;

                        try
                        {
                            context.TLADM_DailyLog.Add(DailyLog);

                            context.SaveChanges();

                            MessageBox.Show("Data saved successfully to database");
                            SetUp(false);
                            frmKnitViewRep vRep = new frmKnitViewRep(14, LNU.col8 - 1);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();

                            }

                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException en)
                        {
                            foreach (var eve in en.EntityValidationErrors)
                            {
                                MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void dtpAdjustDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp1 = sender as DateTimePicker;
            if (oDtp1 != null)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp1.Name
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
            TLADM_TranactionType TranType = null;
            TLKNI_GreigeProduction GreigeP = null; 

            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
            else if (oDgv != null && e.KeyCode == Keys.Insert)
            {
                DialogResult res = MessageBox.Show("You are about to insert a new record into this Knit order ", "Please confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                    if (KO == null)
                    {
                        MessageBox.Show("Please select a Knit order");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        var Machine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                        if (Machine != null)
                        {
                            MCode = Machine.MD_MachineCode.Remove(0, 1).Trim();
                            MLNU = Machine.MD_LastNumberUsed;

                            var MachDet = Machine.MD_MachineCode.Remove(0);

                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                            if (Dept != null)
                            {
                                if (KO.KnitO_YarnO_FK == null)
                                {
                                    if (KO.KnitO_CommisionCust)
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2002).FirstOrDefault();

                                    }
                                    else
                                    {
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2003).FirstOrDefault();
                                    }
                                }
                                else
                                {
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2001).FirstOrDefault();
                                }

                                GreigeP = new TLKNI_GreigeProduction();
                                GreigeP.GreigeP_Store_FK = TranType.TrxT_ToWhse_FK;
                                GreigeP.GreigeP_KnitO_Fk = KO.KnitO_Pk;
                                GreigeP.GreigeP_Greige_Fk = KO.KnitO_Product_FK;
                                GreigeP.GreigeP_PieceNo = MCode + MLNU.ToString().PadLeft(4,'0');
                                GreigeP.GreigeP_weight = 0.00M;
                                GreigeP.GreigeP_weightAvail = 0.00M;
                                GreigeP.GreigeP_Meas1 = 0;
                                GreigeP.GreigeP_Meas2 = 0;
                                GreigeP.GreigeP_Meas3 = 0;
                                GreigeP.GreigeP_Meas4 = 0;
                                GreigeP.GreigeP_Meas5 = 0;
                                GreigeP.GreigeP_Meas6 = 0;
                                GreigeP.GreigeP_Meas7 = 0;
                                GreigeP.GreigeP_Meas8 = 0;
                                GreigeP.GreigeP_Grade = "A";
                                GreigeP.GreigeP_Captured = true;
                                GreigeP.GreigeP_Shift_FK = 1;
                                GreigeP.GreigeP_Captured = true;

                                Machine.MD_LastNumberUsed += 1;

                                context.TLKNI_GreigeProduction.Add(GreigeP);
                            }
                     
                            try
                            {
                                context.SaveChanges();
                                var Index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[Index].Cells[0].Value = GreigeP.GreigeP_Pk;
                                dataGridView1.Rows[Index].Cells[1].Value = GreigeP.GreigeP_PieceNo;
                                dataGridView1.Rows[Index].Cells[2].Value = "A";
                                dataGridView1.Rows[Index].Cells[3].Value = 0.00M;

                                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[Index].Cells[3];
                                this.dataGridView1.BeginEdit(true);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                 
                }
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
