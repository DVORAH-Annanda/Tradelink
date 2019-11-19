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

namespace Spinning
{
    public partial class frmCottonAdjustments : Form
    {

        DataGridViewCheckBoxColumn oChk;
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;

        bool[] RowsSelected;
        string[][] MandatoryFields;
        bool[] MandSelected;
        Util core;
        bool formloaded;

        int LastBaleNo;
        decimal AvgMic;
        decimal AvgNettMass;
        decimal AvgGrossMass;
        decimal AvgStaple;

        public frmCottonAdjustments()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            MandatoryFields = new string[][]
                {   new string[] {"cmbContractNo", "Please select a contract number", "0"},
                    new string[] {"cmbLotNo", "Please select a Lot No", "1"} 
                };
            core = new Util();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumber != null)
                {
                    txtAdjustmentNumber.Text = LastNumber.col3.ToString();
                }
                
                cmbContractNo.DataSource = context.TLADM_CottonContracts.OrderBy(x => x.CottonCon_Description).ToList();
                cmbContractNo.ValueMember = "CottonCon_Pk";
                cmbContractNo.DisplayMember = "CottonCon_No";
                cmbContractNo.SelectedValue = 0;

            }

            oChk = new DataGridViewCheckBoxColumn();
            oChk.HeaderText = "Select";
            oChk.ValueType = typeof(Boolean);

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Bale No";
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Mic";
            oTxtBoxC.ValueType = typeof(Decimal);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // oTxtBoxC.ReadOnly = true;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Kgs (NETT)";
            oTxtBoxD.ValueType = typeof(Decimal);
            oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Staple";
            oTxtBoxE.ValueType = typeof(Decimal);
            oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // oTxtBoxE.ReadOnly = true;


            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.HeaderText = "Kgs (GROSS)";
            oTxtBoxF.ValueType = typeof(Decimal);
            oTxtBoxF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxF.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtBoxA);    // 0 
            dataGridView1.Columns.Add(oChk);        // 1 Selected
            dataGridView1.Columns.Add(oTxtBoxB);    // 2 Bales Numeric 
            dataGridView1.Columns.Add(oTxtBoxC);    // 3 MIC Decimal
            dataGridView1.Columns.Add(oTxtBoxD);    // 4 kgs (NETT) Decimal 
            dataGridView1.Columns.Add(oTxtBoxE);    // 5 Staple Decimal
            dataGridView1.Columns.Add(oTxtBoxF);    // 6 kgs (GROSS) Decimal

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            rbWriteOff.Checked = true;

            rtbNotes.Text = string.Empty;
            txtActualNettWA.Text = "0.00";
            txtActualGrossWA.Text = "0.00";

            txtPre_DeterminedNettWA.KeyPress += core.txtWin_KeyPress;
            txtPre_DeterminedNettWA.KeyDown += core.txtWin_KeyDownOEM;
            
            dataGridView1.Rows.Clear();
            formloaded = true;
        }

        private void cmbContractNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                cmbLotNo.SelectedValue = 0;
                using (var context = new TTI2Entities())
                {
                    var selectedRecord = (TLADM_CottonContracts)oCmbo.SelectedItem;
                    if (selectedRecord != null)
                    {
                        IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();

                        dataGridView1.Rows.Clear();
                        var existing = context.SelectCottonRecords1(selectedRecord.CottonCon_Pk, selectedRecord.CottonCon_ConSupplier_FK).ToList();
                        foreach (var row in existing)
                        {
                            TLSPN_CottonTransactions cr = new TLSPN_CottonTransactions();
                            cr.cotrx_ContractNo_Fk = row.cotrx_ContractNo_Fk;
                            cr.cotrx_Customer_FK = row.cotrx_Customer_FK;
                            cr.cotrx_GrossAveBaleWeight = row.cotrx_GrossAveBaleWeight;
                            cr.cotrx_GrossWeight = row.cotrx_GrossWeight;
                            cr.cotrx_LotNo = row.cotrx_LotNo;
                            cr.cotrx_NettPerWB = row.cotrx_NettPerWB;
                            cr.cotrx_NetWeight = row.cotrx_NetWeight;
                            cr.cotrx_NoBales = row.cotrx_NoBales;
                            cr.cotrx_Notes = row.cotrx_Notes;
                            cr.cotrx_pk = row.cotrx_pk;
                            cr.cotrx_Return_No = row.cotrx_Return_No;
                            cr.cotrx_Supplier_FK = row.cotrx_Supplier_FK;
                            cr.cotrx_TransDate = row.cotrx_TransDate;
                            cr.cotrx_TranType = row.cotrx_TranType;
                          //  cr.cotrx_VehReg_FK = row.cotrx_VehReg_FK;
                            cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                            cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                            cr.cotrx_WriteOff = row.cotrx_WriteOff;
                            cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                            cotreceived.Add(cr);
                        }

                        cmbLotNo.DataSource = cotreceived.ToList();
                        //cmbLotNo.DataSource = context.TLSPN_CottonReceived.Where(x => x.CotRec_Contract_FK == selectedRecord.CottonCon_Pk).ToList();
                        cmbLotNo.ValueMember = "cotrx_LotNo";
                        cmbLotNo.DisplayMember = "cotrx_LotNo";

                        var result = (from u in MandatoryFields
                                      where u[0] == oCmbo.Name
                                      select u).FirstOrDefault();
                        if (result != null)
                        {
                            int nbr = Convert.ToInt32(result[2].ToString());
                            MandSelected[nbr] = true;
                        }
                    }
                }
            }
        }

        private void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null & formloaded)
            {
                var selectedRecord = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (selectedRecord != null)
                {
                    dataGridView1.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo && !x.CoBales_CottonSold && !x.CoBales_IssuedToProd && !x.CoBales_CottonReturned).ToList();
                        foreach (var row in ExistingData)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                            dataGridView1.Rows[index].Cells[2].Value = row.CotBales_BaleNo;
                            LastBaleNo = row.CotBales_BaleNo;
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.CotBales_Mic, 2);
                            dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Staple, 2);
                            dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                        }

                        if (ExistingData.Count() > 0)
                        {
                            AvgMic = ExistingData.Average(x => x.CotBales_Mic);
                            AvgNettMass = ExistingData.Average(x => x.CotBales_Weight_Nett);
                            AvgGrossMass = ExistingData.Average(x => x.CotBales_Weight_Gross);
                            AvgStaple = ExistingData.Average(x => x.CotBales_Staple);
                            
                            RowsSelected = core.PopulateArray(dataGridView1.Rows.Count, false);
                            btnSave.Enabled = true;

                            var result = (from u in MandatoryFields
                                          where u[0] == oCmbo.Name
                                          select u).FirstOrDefault();
                            if (result != null)
                            {
                                int nbr = Convert.ToInt32(result[2].ToString());
                                MandSelected[nbr] = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TLSPN_CottonReceivedBales bales;

            Button oBtn = sender as Button;
            if (oBtn != null & formloaded)
            {
              
                if (RowsSelected != null && rbWriteOff.Checked)
                {
                    var cnt = RowsSelected.Where(x => x == false).Count();
                    if (cnt == RowsSelected.Count())
                    {
                        MessageBox.Show("Please select at least one row from the grid as shown");
                        return;
                    }
                }

                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var LotDetails = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                //dont forget last number used 
                //-------------------------------------------

                using (var context = new TTI2Entities())
                {
                    

                    var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumber != null)
                    {
                        LastNumber.col3 += 1;
                    }

                    // 0 
                    // 1 Selected
                    // 2 Bales Numeric 
                    // 3 MIC Decimal
                    // 4 kgs (NETT) Decimal 
                    // 5 Staple Decimal
                    // 6 kgs (GROSS) decimal 
                    int NoBales = 0;
                    decimal NettMass = 0M;
                    decimal GrossMass = 0M;
                    var ContractDetails = (TLADM_CottonContracts)cmbContractNo.SelectedItem;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null || (bool)row.Cells[1].Value == false)
                            continue;
                        
                        NoBales += 1;
                        NettMass += (decimal)row.Cells[4].Value;
                        GrossMass += (decimal)row.Cells[6].Value;

                        if (rbWriteOn.Checked)
                        {
                            TLSPN_CottonReceivedBales Bales = new TLSPN_CottonReceivedBales();
                            Bales.CotBales_BaleNo = (int)row.Cells[2].Value;
                            Bales.CotBales_Mic = (decimal)row.Cells[3].Value;
                            Bales.CotBales_Staple = (decimal)row.Cells[5].Value;
                            Bales.CotBales_Weight_Nett = (decimal)row.Cells[4].Value;
                            Bales.CotBales_LotNo = LotDetails.cotrx_LotNo;
                            Bales.CotBales_Weight_Gross = (decimal)row.Cells[6].Value;
                            Bales.CoBales_CottonSequence = Convert.ToInt32(txtAdjustmentNumber.Text);
                            context.TLSPN_CottonReceivedBales.Add(Bales);
                        }
                        else
                        {
                            bales = context.TLSPN_CottonReceivedBales.Find((int)row.Cells[0].Value);
                            if (bales != null)
                            {
                                if ((bool)row.Cells[1].Value == true)
                                {
                                    bales.CoBales_CottonReturned = true;
                                    bales.CoBales_CottonSequence = Convert.ToInt32(txtAdjustmentNumber.Text);
                                 
                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                    }

                    //----------------------------------------------------------------------------
                    //
                    //------------------------------------------------------------------------------

                    TLSPN_CottonTransactions cotTrans = new TLSPN_CottonTransactions();

                    cotTrans.cotrx_TransDate = dateTimePicker1.Value;
                    cotTrans.cotrx_ContractNo_Fk = ContractDetails.CottonCon_Pk;
                    cotTrans.cotrx_LotNo = Convert.ToInt32(cmbLotNo.SelectedValue);
                    cotTrans.cotrx_NetWeight = NettMass;
                    cotTrans.cotrx_GrossWeight = GrossMass;
                    cotTrans.cotrx_NoBales = NoBales;
                    //cotTrans.cotrx_VehReg_FK = null;
                    cotTrans.cotrx_WeighBridgeFull = 0;
                    cotTrans.cotrx_WeighBridgeEmpty = 0;
                    cotTrans.cotrx_NettPerWB = 0;
                    cotTrans.cotrx_Return_No = Convert.ToInt32(txtAdjustmentNumber.Text);
                    cotTrans.cotrx_Supplier_FK = ContractDetails.CottonCon_ConSupplier_FK;
                    cotTrans.cotrx_Notes = rtbNotes.Text;
                    //-------------------------------------------------------------------------
                    // Consult Table TLADM_TranTypes
                    //----------------------------------------------------------------------------
                    var DepDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (DepDetail != null)
                    {
                        var Trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 400 && x.TrxT_Department_FK == DepDetail.Dep_Id).FirstOrDefault();
                        if (Trantype != null)
                            cotTrans.cotrx_TranType = Trantype.TrxT_Pk;
                    }
                   if (rbWriteOff.Checked)
                        cotTrans.cotrx_WriteOff = true;
                    else
                        cotTrans.cotrx_WriteOff = false;
                    //------------------------------------------------------------------------

                    context.TLSPN_CottonTransactions.Add(cotTrans);

                    //-----------------------------------------------------------
                    //
                    //------------------------------------------------------------

                    string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                            .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                            .ToString();

                    
                    TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                    DailyLog.TLDL_IPAddress = Mach_IP;
                    DailyLog.TLDL_Dept_Fk   = DepDetail.Dep_Id;
                    DailyLog.TLDL_Date = DateTime.Now;
                    DailyLog.TLDL_TransDetail = "Cotton Adjustment";
                    DailyLog.TLDL_AuthorisedBy = txtAdjustmentNumber.Text;
                    DailyLog.TLDL_Comments = txtAdjustmentNumber.Text;

                    context.TLADM_DailyLog.Add(DailyLog);

                    try
                    {
                        context.SaveChanges();
                        var Retno = Convert.ToInt32(txtAdjustmentNumber.Text);
                        frmViewReport vRep = new frmViewReport(4, Retno);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        SetUp();
                        MessageBox.Show("Data saved to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
               }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var Cell = oDgv.CurrentCell;
                if (rbWriteOff.Checked)
                {
                    if (Cell.EditedFormattedValue.ToString() == bool.TrueString && !RowsSelected[Cell.RowIndex])
                    {
                        var Total = Convert.ToDecimal(txtActualGrossWA.Text);
                        txtActualGrossWA.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                        Total = Convert.ToDecimal(txtActualNettWA.Text);
                        txtActualNettWA.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                        RowsSelected[Cell.RowIndex] = true;
                    }
                    else if (Cell.EditedFormattedValue.ToString() == bool.FalseString && RowsSelected[Cell.RowIndex])
                    {
                        var Total = Convert.ToDecimal(txtActualGrossWA.Text);
                        txtActualGrossWA.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                        Total = Convert.ToDecimal(txtActualNettWA.Text);
                        txtActualNettWA.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                        RowsSelected[Cell.RowIndex] = false;
                    }
                }
                else if (rbWriteOn.Checked)
                {
                    if (Cell.EditedFormattedValue.ToString() == bool.TrueString)
                    {
                        var Total = Convert.ToDecimal(txtActualGrossWA.Text);
                        txtActualGrossWA.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                        Total = Convert.ToDecimal(txtActualNettWA.Text);
                        txtActualNettWA.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();

                    }
                    else if (Cell.EditedFormattedValue.ToString() == bool.FalseString)
                    {
                        var Total = Convert.ToDecimal(txtActualGrossWA.Text);
                        txtActualGrossWA.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                        Total = Convert.ToDecimal(txtActualNettWA.Text);
                        txtActualNettWA.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                    }
                }

            }
        }

        private void rbWriteOn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRadB = sender as RadioButton;
            if (oRadB != null && formloaded)
            {
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.Rows.Clear();
                txtActualGrossWA.Text = "0.00";
                txtActualNettWA.Text = "0.00";
                 
            }
        }

        private void datagridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
             var oDgv = sender as DataGridView;
             if (oDgv != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 if (rbWriteOn.Checked)
                 {
                     var Cell = oDgv.CurrentCell;
                     oDgv.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 1].Value = ++LastBaleNo;
                     oDgv.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 2].Value = Math.Round(AvgMic,1);
                     oDgv.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 3].Value = Math.Round(AvgNettMass, 1);
                     oDgv.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 4].Value = Math.Round(AvgStaple, 1);
                     oDgv.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 5].Value = Math.Round(AvgGrossMass, 1);
                  
                 }
             }
        }

        private void rbWriteOff_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRadB = sender as RadioButton;
            if (oRadB != null && formloaded)
            {
                 var selectedRecord = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                 if (selectedRecord != null)
                 {
                     dataGridView1.Rows.Clear();
                     dataGridView1.AllowUserToAddRows = false;
                     txtActualGrossWA.Text = "0.00";
                     txtActualNettWA.Text = "0.00";

                     using (var context = new TTI2Entities())
                     {
                         var ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo && !x.CoBales_CottonSold && !x.CoBales_IssuedToProd && !x.CoBales_CottonReturned).ToList();
                         foreach (var row in ExistingData)
                         {
                             var index = dataGridView1.Rows.Add();
                             dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                             dataGridView1.Rows[index].Cells[2].Value = row.CotBales_BaleNo;
                             LastBaleNo = row.CotBales_BaleNo;
                             dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.CotBales_Mic, 2);
                             dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                             dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Staple, 2);
                             dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                         }

                         if (ExistingData.Count() > 0)
                         {
                             AvgMic = ExistingData.Average(x => x.CotBales_Mic);
                             AvgNettMass = ExistingData.Average(x => x.CotBales_Weight_Nett);
                             AvgGrossMass = ExistingData.Average(x => x.CotBales_Weight_Gross);
                             AvgStaple = ExistingData.Average(x => x.CotBales_Staple);

                             RowsSelected = core.PopulateArray(dataGridView1.Rows.Count, false);
                             btnSave.Enabled = true;

                            
                         }
                     }
                 }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 3 ||
                        Cell.ColumnIndex == 5)
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
            }
        }
    }
}
