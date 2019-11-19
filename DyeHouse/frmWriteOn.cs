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
namespace DyeHouse
{
    public partial class frmWriteOn : Form
    {
        bool formloaded;
        bool CheckFired;
        //------------------------------------------------------
        // datagridview1 
        //---------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn  oTxtA = new DataGridViewTextBoxColumn();     // Record Index
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn  oTxtB = new DataGridViewTextBoxColumn();     // Piece No
        DataGridViewTextBoxColumn  oTxtC = new DataGridViewTextBoxColumn();     // Current Weight
        DataGridViewTextBoxColumn  oTxtD = new DataGridViewTextBoxColumn();     // Weight to reduce by
        
        //------------------------------------------------------
        // datagridview2 
        //---------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA1 = new DataGridViewTextBoxColumn();     // Record Index
        DataGridViewTextBoxColumn oTxtB1 = new DataGridViewTextBoxColumn();     // Piece No

     


        string[][] MandatoryFields;
        bool[] MandSelected;

        Util core;


        public frmWriteOn()
        {
            InitializeComponent();

            MandatoryFields = new string[][]
                {   new string[] {"dateTimePicker1", "Please select an transaction date", "0", "10", "D"},
                    new string[] {"txtApprovedBy", "Please enter the person who approved this", "1", "", ""}, 
                    new string[] {"txtReasons", "Please enter the reasons for this write off", "2", "", ""},
                    new string[] {"cmboDyeBatches", "Please select a batch first", "3", "", ""} 
                };

            core = new Util();

            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.HeaderText = "Pk";

            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);

            oTxtB.Visible = true;
            oTxtB.ValueType = typeof(string);
            oTxtB.HeaderText = "Piece No";
            oTxtB.Width = 150;
            oTxtB.ReadOnly = true;

            oTxtC.Visible = true;
            oTxtC.ValueType = typeof(decimal);
            oTxtC.HeaderText = "Piece Weight";
            oTxtC.Width = 150;
            oTxtC.ReadOnly = true;

            oTxtD.Visible = true;
            oTxtD.ValueType = typeof(decimal);
            oTxtD.Width = 150;
            oTxtD.HeaderText = "Adjusted Weight";


           dataGridView1.Columns.Add(oTxtA);
           dataGridView1.Columns.Add(oChkA);
           dataGridView1.Columns.Add(oTxtB);
           dataGridView1.Columns.Add(oTxtC);

           dataGridView1.AllowUserToOrderColumns = false;
           dataGridView1.AllowUserToAddRows = false;
 


           SetUp(true);
        }

        void SetUp(bool xSel)
        {
            IList<TLKNI_GreigeProduction> Existing = new List<TLKNI_GreigeProduction>();
            formloaded = false;
            CheckFired = false;

            dataGridView2.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtNumber.Text = "DA" + LNU.col6.ToString().PadLeft(6, '0');
                }

                if (xSel)
                {
                    var Query = from T1 in context.TLDYE_DyeBatch
                                join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                where !T1.DYEB_Closed && !T1.DYEB_CommissinCust && T1.DYEB_Allocated && T1.DYEB_Transfered && !T2.DYEBO_Rejected && !T2.DYEBO_CutSheet && !T2.DYEBO_WriteOff
                                select new { T1.DYEB_Pk, T1.DYEB_BatchNo, T1.DYEB_DyeOrder_FK, T1.DYEB_Colour_FK, T1.DYEB_Allocated , T1.DYEB_Transfered};

                    var QueryGroup = Query.OrderBy(x => x.DYEB_BatchNo).GroupBy(x => x.DYEB_BatchNo);


                    foreach (var BatchGroup in QueryGroup)
                    {
                        TLDYE_DyeBatch DB = new TLDYE_DyeBatch();
                        DB.DYEB_Pk = BatchGroup.FirstOrDefault().DYEB_Pk;
                        DB.DYEB_DyeOrder_FK = BatchGroup.FirstOrDefault().DYEB_DyeOrder_FK;
                        DB.DYEB_BatchNo = BatchGroup.FirstOrDefault().DYEB_BatchNo;
                        DB.DYEB_Colour_FK = BatchGroup.FirstOrDefault().DYEB_Colour_FK;
                        DB.DYEB_Allocated = BatchGroup.FirstOrDefault().DYEB_Allocated;
                        DB.DYEB_Transfered = BatchGroup.FirstOrDefault().DYEB_Transfered; 

                        cmboDyeBatches.Items.Add(DB);


                        cmboDyeBatches.ValueMember = "DYEB_Pk";
                        cmboDyeBatches.DisplayMember = "DYEB_BatchNo";
                    }
                    
                    cmboDyeBatches.SelectedValue = -1;
                    
                }
            }

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
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
                    MandSelected[nbr] = true;
                }
            }
        }

        private void txtReasons_TextChanged(object sender, EventArgs e)
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
                    MandSelected[nbr] = true;
                }

            }
        }

        private void cmboDyeBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();

                var selected = (TLDYE_DyeBatch)oCmbo.SelectedItem;
                if (selected != null)
                {
                    if (!selected.DYEB_Transfered)
                    {
                        MessageBox.Show("The batch selected is still in pendeing status." + Environment.NewLine + "Please select an alternative");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk && !x.DYEBO_Sold && !x.DYEBO_WriteOff && !x.DYEBO_QAApproved).ToList();
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            var GP = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if(GP == null)
                                continue;
                            dataGridView1.Rows[index].Cells[2].Value = GP.GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[3].Value = row.DYEBD_GreigeProduction_Weight;
                        }
                        
                        
                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            CheckFired = false;
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[3];
                            this.dataGridView1.BeginEdit(true);

                            var result = (from u in MandatoryFields
                                          where u[0] == oCmbo.Name
                                          select u).FirstOrDefault();

                            if (result != null)
                            {
                                int nbr = Convert.ToInt32(result[2].ToString());
                                MandSelected[nbr] = true;
                            }
                            
                            var GP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Greige_Fk == selected.DYEB_Greige_FK).ToList();
                            foreach (var row in GP)
                            {
                                var index = dataGridView2.Rows.Add();
                                dataGridView2.Rows[index].Cells[0].Value = row.GreigeP_Pk;
                                dataGridView2.Rows[index].Cells[1].Value = false;
                                dataGridView2.Rows[index].Cells[2].Value = row.GreigeP_PieceNo;
                            }
                        }

                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else if (combo != null)
                {
                   // combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                   // combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            decimal Weight = 0.00M;
            int CurrentStore_FK = 0;
            TLADM_TranactionType TT = null;
            TLADM_Departments Depts = null;
            TLDYE_DyeTransactions dt = null;

            if (oBtn != null && formloaded)
            {
               
                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var selected = (TLDYE_DyeBatch)cmboDyeBatches.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    var LNU = context.TLADM_LastNumberUsed.Find(3);
                    if (LNU != null)
                    {
                        LNU.col6 += 1;
                    }


                    var TotalWeight = 0.00M;

                    //--------- first attend to current records 
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;

                        Weight = (decimal)row.Cells[3].Value;

                        TotalWeight += Weight;

                        var Pk = (int)row.Cells[0].Value;
                        if (Pk != 0)
                        {
                            var Record = context.TLDYE_DyeBatchDetails.Find(Pk);
                            if (Record != null)
                            {
                                Record.DYEBO_AdjustedWeight = Weight;
                                Record.DYEBO_TransactionNo = txtNumber.Text;
                                Record.DYEBO_WriteOff = true;
                                Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                                if (Depts != null)
                                {
                                    TT = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Depts.Dep_Id && x.TrxT_Number == 1000).FirstOrDefault();
                                    if (TT != null)
                                    {
                                        Record.DYEBO_CurrentStore_FK = (int)TT.TrxT_ToWhse_FK;
                                        CurrentStore_FK = (int)TT.TrxT_ToWhse_FK;
                                    }

                                }
                            }
                        }
                    }
                    
                    
                    dt = new TLDYE_DyeTransactions();
                    dt.TLDYET_BatchNo = selected.DYEB_BatchNo;
                    dt.TLDYET_Date = DateTime.Now;
                    dt.TLDYET_SequenceNo = selected.DYEB_SequenceNo;
                    dt.TLDYET_CurrentStore_FK = CurrentStore_FK;
                    dt.TLDYET_TransactionWeight = TotalWeight;
                    dt.TLDYET_AuthorisedBy = txtApprovedBy.Text;
                    dt.TLDYET_Adjustment_Reasons = txtReasons.Text;
                    dt.TLDYET_TransactionType = TT.TrxT_Pk;
                    dt.TLDYET_BatchWeight = selected.DYEB_BatchKG;
                    dt.TLDYET_Batch_FK = selected.DYEB_Pk;
                    dt.TLDYET_TransactionNumber = txtNumber.Text;

                    context.TLDYE_DyeTransactions.Add(dt);

                    //---------------------------------------------------------------------
                    // This is the daily log file that needs to be updated 
                    //-------------------------------------------------------

                    string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                               .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                               .ToString();

                    TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                    DailyLog.TLDL_IPAddress = Mach_IP;
                    DailyLog.TLDL_Dept_Fk = Depts.Dep_Id;
                    DailyLog.TLDL_Date = DateTime.Now;
                    DailyLog.TLDL_TransDetail = "Dye House Adjustment";
                    DailyLog.TLDL_AuthorisedBy = txtApprovedBy.Text;
                    DailyLog.TLDL_Comments = txtNumber.Text;

                    context.TLADM_DailyLog.Add(DailyLog);

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully stored to database");
                        frmDyeViewReport vRep = new frmDyeViewReport(15, dt.TLDYET_Pk);
                        vRep.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
              
                }
            }
        }

        private void txtApprovedBy_TextChanged_1(object sender, EventArgs e)
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
                    MandSelected[nbr] = true;
                }

            }
        }

        private void chkAdditional_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            var selected = (TLDYE_DyeBatch)cmboDyeBatches.SelectedItem;
            if (!CheckFired)
            {
                if (selected != null)
                {
                    if (oChk.Checked)
                        dataGridView2.Visible = true;
                    else
                        dataGridView2.Visible = false;

                    CheckFired = false;
                }
                else
                {
                    CheckFired = true;
                    MessageBox.Show("Please select a batch first");
                    oChk.Checked = !oChk.Checked;
                    
                }
            }

        }

       
    }
}
