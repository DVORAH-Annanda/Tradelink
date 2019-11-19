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
    public partial class frmYarnAdjustment : Form
    {
        bool formloaded;
        Util core;
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool Transactions;
        decimal AvgConweight;

        bool _Mode;
        public frmYarnAdjustment(bool Mode)
        {
            // if Mode true then this routine has been called from Spinning
            // else Mode false then this has been called from Knitting

            InitializeComponent();

            _Mode = Mode;

            if (_Mode)
                this.Text = "Yarn Stock Adjustment Spinning Department";
            else
                this.Text = "Yarn Stock Adjustment Knitting Department";

            core = new Util();
            MandatoryFields = new string[][]
                {   new string[] {"dateTimePicker1", "Please select a Transaction Date", "0","F"},
                    new string[] {"cmboYarnNo", "Please select a yarn order number", "1", "F"},
                    new string[] {"cmboPalletNumbers", "Please select a pallet number", "2","F"},
                    new string[] {"txtApprovedBy", "Please complete the approved by info", "3", "F"},
                    new string[] {"txtNettWeight", "Please select the Nett Weight", "4","D"},
                    new string[] {"txtNumberOfCones", "Please enter the number of cones", "5","I"},
                    new string[] {"rtbReasons", "Please supply the reasons for this adjustment", "6","F"}
                };
            SetUp();
            Transactions = false;
        }

        void SetUp()
        {
            AvgConweight = 0.00M;

            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumberUsed != null)
                {
                    txtDeliveryNoteNumber.Text = LastNumberUsed.col11.ToString();
                }

             

                cmboYarnNo.DataSource = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                cmboYarnNo.DisplayMember = "YarnO_OrderNumber";
                cmboYarnNo.ValueMember = "YarnO_Pk";
                cmboYarnNo.SelectedValue = 0;

                /*
                cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_Complete && !x.YarnOP_Sold && !x.YarnOP_Issued && !x.YarnOP_Sold).OrderBy(x => x.YarnOP_PalletNo).ToList();
                cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                cmboPalletNumbers.SelectedValue = 0;
                */

            }

            rbWriteOn.Checked = true;
            
            txtIdentification.Text = string.Empty;
            txtNettWeight.Text = string.Empty;
            txtNumberOfCones.Text = string.Empty;
            txtTexCount.Text = string.Empty;
            txtTwistFactor.Text = string.Empty;
            txtYarnOrderNo.Text = string.Empty;
            txtYarnType.Text = string.Empty;
            txtApprovedBy.Text = string.Empty;
            rtbReasons.Text = string.Empty;

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;

        }

        private void cmboPalletNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                txtIdentification.Text = string.Empty;
                txtTexCount.Text = string.Empty;
                txtTwistFactor.Text = string.Empty;
                txtYarnOrderNo.Text = string.Empty;
                txtYarnType.Text = string.Empty;

                var selected = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();
                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                        if (_Mode)
                        {
                            AvgConweight = selected.YarnOP_NettWeight / selected.YarnOP_NoOfCones;
                            txtNettWeight.Text = selected.YarnOP_NettWeight.ToString();
                            txtNumberOfCones.Text = selected.YarnOP_NoOfConesSpun.ToString();
                        }

                        
                        using (var context = new TTI2Entities())
                        {
                            if (!_Mode)
                            {
                                var Pallet = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_PalletNo == selected.YarnOP_PalletNo).FirstOrDefault();
                                if (Pallet != null)
                                {
                                    AvgConweight = Pallet.TLKNIOP_NettWeight / Pallet.TLKNIOP_Cones;
                                    txtNettWeight.Text = Pallet.TLKNIOP_NettWeight.ToString();
                                    txtNumberOfCones.Text = Pallet.TLKNIOP_Cones.ToString();
                                }
                            }
                            var Existing = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == selected.YarnOP_YarnOrder_FK).FirstOrDefault();
                            if (Existing != null)
                            {

                                txtYarnOrderNo.Text = Existing.YarnO_OrderNumber.ToString();

                                var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Existing.Yarno_YarnType_FK).FirstOrDefault();
                                if (YarnType != null)
                                {
                                    txtYarnType.Text = YarnType.YA_Description;
                                    txtTwistFactor.Text = Math.Round(YarnType.YA_Twist, 2).ToString();
                                    txtTexCount.Text = Math.Round(YarnType.YA_TexCount, 2).ToString();
                                    txtIdentification.Text = YarnType.YA_ConeColour;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // dont forget last number used~!!!!!
            TLADM_Departments DeptDetail;

            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if(!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;

                }

                using (var context = new TTI2Entities())
                {
                    var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumber != null)
                        LastNumber.col11 += 1;

                    //---------------------------------------------------------
                    TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();
                    //-------------------------------------------------------------
                    // Review Document for a list of transaction numbers
                    //-------------------------------------------------------------

                    //-------------------------------------------------------------
                    var YO = (TLSPN_YarnOrder)cmboYarnNo.SelectedItem;
                    if (YO != null)
                        YarnT.YarnTrx_YarnOrder_FK = YO.YarnO_Pk;
                    //----------------------------------------------------------------------------------------------------
                    DeptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (DeptDetail != null)
                    {
                        var tranDetail = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == DeptDetail.Dep_Id && x.TrxT_Number == 1100).FirstOrDefault();
                        if (tranDetail != null)
                        {
                            YarnT.YarnTrx_TranType_FK = tranDetail.TrxT_Pk;
                            YarnT.YarnTrx_ToDep_FK = tranDetail.TrxT_ToWhse_FK;
                            YarnT.YarnTrx_FromDep_FK = tranDetail.TrxT_FromWhse_FK;
                        }
                    }
                    //-------------------------------------------------------------------------------
                    YarnT.YarnTrx_SequenceNo = Convert.ToInt32(txtDeliveryNoteNumber.Text);
                    YarnT.YarnTrx_Date = dateTimePicker1.Value;
                  
                    var PalletNo = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                    if (PalletNo != null)
                    {
                        if (_Mode)
                        {
                            YarnT.YarnTrx_PalletNo_Fk = PalletNo.YarnOP_Pk;
                        }
                        else
                        {
                            var PLT = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_PalletNo == PalletNo.YarnOP_PalletNo).FirstOrDefault();
                            if(PLT != null)
                                YarnT.YarnTrx_PalletNo_Fk = PLT.TLKNIOP_Pk;
                        }
                    }
                    YarnT.YarnTrx_ApprovedBy = txtApprovedBy.Text;
                    YarnT.YarnTrx_Reasons = rtbReasons.Text;
                                     
                    var NoCones = Convert.ToInt32(txtNumberOfCones.Text);
                    YarnT.YarnTrx_Cones = NoCones;
                    var NettWeight = Convert.ToDecimal(txtNettWeight.Text);
                    YarnT.YarnTrx_NettWeight = NettWeight;

                    if (_Mode)
                    {
                        var PalletStore = context.TLSPN_YarnOrderPallets.Find(PalletNo.YarnOP_Pk);
                        if (PalletStore != null)
                        {
                            if (rbWriteOff.Checked)
                            {
                                PalletStore.YarnOP_NoOfConesSpun -= NoCones;
                                PalletStore.YarnOP_NettWeight -= NettWeight;
                                YarnT.YarnTrx_WriteOff = true;
                            }
                            else
                            {
                                PalletStore.YarnOP_NoOfConesSpun += NoCones;
                                PalletStore.YarnOP_NettWeight += NettWeight;
                                YarnT.YarnTrx_WriteOff = false;
                            }
                        }
                    }
                    else
                    {
                        var PalletStore = context.TLKNI_YarnOrderPallets.Find(YarnT.YarnTrx_PalletNo_Fk);
                        if (PalletStore != null)
                        {
                            if (rbWriteOff.Checked)
                            {
                                PalletStore.TLKNIOP_Cones -= NoCones;
                                PalletStore.TLKNIOP_NettWeight -= NettWeight;
                                YarnT.YarnTrx_WriteOff = true;
                            }
                            else
                            {
                                PalletStore.TLKNIOP_Cones += NoCones;
                                PalletStore.TLKNIOP_NettWeight += NettWeight;
                                YarnT.YarnTrx_WriteOff = false;
                            }
                        }
                    }

                    String Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                           .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                          .ToString();


                    TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                    DailyLog.TLDL_IPAddress = Mach_IP;
                    DailyLog.TLDL_Dept_Fk = DeptDetail.Dep_Id;
                    DailyLog.TLDL_Date = DateTime.Now;
                    DailyLog.TLDL_TransDetail = "Yarn Adjustment";
                    DailyLog.TLDL_AuthorisedBy = txtApprovedBy.Text;
                    DailyLog.TLDL_Comments = string.Empty;
                    context.TLADM_DailyLog.Add(DailyLog);
                    
                    try
                    {
                        context.TLSPN_YarnTransactions.Add(YarnT);
                        context.SaveChanges();
                        Transactions = true;
                        SetUp();
                        MessageBox.Show("Records stored successfully to database");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txt_ValueChanged(object sender, EventArgs e)
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
                    if(oTxt.Name == "txtNumberOfCones" && oTxt.Text.Length != 0)
                    {
                        int NoSold = Convert.ToInt32(oTxt.Text);
                        txtNettWeight.Text = Math.Round(Math.Round(NoSold * AvgConweight), 0).ToString();
                    }
                }
                else
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = false;
                }

         
            }
        }

        private void rtb_ValueChanged(object sender, EventArgs e)
        {
            RichTextBox oRtb = sender as RichTextBox;
            if (oRtb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oRtb.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }

        }

        private void cmboYarnNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrder)cmboYarnNo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                      
                        if (_Mode)
                        {
                            formloaded = false;
                            cmboPalletNumbers.DataSource = null;
                            cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Complete && x.YarnOP_NoOfConesSpun != 0).OrderBy(x => x.YarnOP_PalletNo).ToList();
                            cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                            cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                            formloaded = true;
                        }
                        else
                        {
                            formloaded = false;
                            cmboPalletNumbers.DataSource = null;
                            cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Issued).OrderBy(x => x.YarnOP_PalletNo).ToList();
                            cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                            cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                            formloaded = true;
                        }
                    }
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

        private void cmboFromStore_SelectedIndexChanged(object sender, EventArgs e)
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
                    MandSelected[nbr] = true;
                }
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
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

        private void Adjustment_Closing(object sender, FormClosingEventArgs e)
        {
            if (Transactions)
            {
                if (MessageBox.Show("Do you wish to print the transaction listing report?", "Print Transactions",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    YarnTOptions repOptions = new YarnTOptions();
                    repOptions.FromDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ToDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ReportSelected = 0;
                    repOptions.TransNumber = 1100;

                    frmViewReport vRep = new frmViewReport(13, repOptions);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    
                }
            }
        }
    }
}
