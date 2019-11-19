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

namespace Spinning
{
    public partial class frmYarnScrapping : Form
    {
        bool formloaded;
        Util core;
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool Transactions;
        decimal AvgConeWeight;

        public frmYarnScrapping()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
                {   new string[] {"dateTimePicker1", "Please select a TransAction Date", "0"},
                    new string[] {"cmbYarnOrder", "Please select a yarn order number", "1"},
                    new string[] {"cmboPalletNumbers", "Please select a pallet number", "2"},
                    new string[] {"txtApprovedBy", "Please complete the approved by info", "3"},
                    new string[] {"txtNettWeight", "Please select the Nett Weight", "4"},
                    new string[] {"txtNumberOfCones", "Please enter the number of cones", "5"},
                    new string[] {"richTextBox1", "Please supply the reasons for this scrapping", "6"}
                };

            txtNettWeight.KeyPress += core.txtWin_KeyPress;
            txtNettWeight.KeyDown += core.txtWin_KeyDownOEM;

            Transactions = false;
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
             using (var context = new TTI2Entities())
            {
                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumberUsed != null)
                {
                    txtDeliveryNoteNumber.Text = LastNumberUsed.col10.ToString();
                }

                cmbYarnOrder.DataSource = context.TLSPN_YarnOrder.Where(x =>!x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                cmbYarnOrder.DisplayMember = "YarnO_OrderNumber";
                cmbYarnOrder.ValueMember = "YarnO_Pk";
                cmbYarnOrder.SelectedValue = 0; 
/*
                cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_Complete && !x.YarnOP_Issued && !x.YarnOP_Sold && !x.YarnOP_Scrapped).OrderBy(x => x.YarnOP_PalletNo).ToList();
                cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                cmboPalletNumbers.SelectedValue = 0;
*/

            }
            
            richTextBox1.Text = string.Empty;
            txtApprovedBy.Text = string.Empty;
            txtIdentification.Text = string.Empty;
            txtTexCount.Text = string.Empty;
            txtTwistFactor.Text = string.Empty;
            txtYarnOrderNo.Text = string.Empty;
            txtYarnType.Text = string.Empty;

            txtNettWeight.Text = "0.00";
            txtNumberOfCones.Text = "0";

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
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

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null &&oTxt.TextLength != 0)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                    if (oTxt.Name == "txtNumberOfCones" && oTxt.Text.Length != 0)
                    {
                        int NoSold = Convert.ToInt32(oTxt.Text);
                        txtNettWeight.Text = Math.Round(NoSold * AvgConeWeight,0).ToString() ;
                    }
                }
                else
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = false;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Dont forget the last number used...
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumber != null)
                        LastNumber.col10 += 1;
                    
                    //---------------------------------------------------------
                    TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();
                    //-------------------------------------------------------------
                    // Review Document for a list 
                    //-------------------------------------------------------------
                    var YO = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                    if (YO != null)
                        YarnT.YarnTrx_YarnOrder_FK = YO.YarnO_Pk;

                    var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (deptDetail != null)
                    {
                        var tranDetail = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 1000).FirstOrDefault();
                        if (tranDetail != null)
                        {
                            YarnT.YarnTrx_TranType_FK = tranDetail.TrxT_Pk;
                            YarnT.YarnTrx_ToDep_FK = tranDetail.TrxT_ToWhse_FK;
                            YarnT.YarnTrx_FromDep_FK = tranDetail.TrxT_FromWhse_FK;

                        }
                    }

                    YarnT.YarnTrx_SequenceNo = Convert.ToInt32(txtDeliveryNoteNumber.Text);
                    YarnT.YarnTrx_Date = dateTimePicker1.Value;
                    
                    var PalletNo = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                    if (PalletNo != null)
                    {
                        YarnT.YarnTrx_PalletNo_Fk = PalletNo.YarnOP_Pk;
                        
                    }

                    YarnT.YarnTrx_ApprovedBy = txtApprovedBy.Text;
                    var NoCones = Convert.ToInt32(txtNumberOfCones.Text);
                    var NettWeight = Convert.ToDecimal(txtNettWeight.Text);
                    
                    YarnT.YarnTrx_Cones = NoCones;
                    YarnT.YarnTrx_NettWeight = NettWeight;

                    YarnT.YarnTrx_Reasons = richTextBox1.Text;

                    var PalletStore = context.TLSPN_YarnOrderPallets.Find(PalletNo.YarnOP_Pk);
                    if (PalletStore != null)
                    {
                        PalletStore.YarnOP_NoOfConesSpun  -= NoCones;
                        PalletStore.YarnOP_NettWeight -= NettWeight;
                        PalletStore.YarnOP_Scrapped = true;
                        PalletStore.YarnOP_DateDispatched = dateTimePicker1.Value;
                    }
                    //--------------------------------------------------------------------------------------

                    try
                    {
                        context.TLSPN_YarnTransactions.Add(YarnT);
                        context.SaveChanges();
                        SetUp();
                        Transactions = true;
                        MessageBox.Show("Records stored to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

             }
        }

        private void cmboPalletNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded) 
            {
                txtIdentification.Text = string.Empty;
                txtNettWeight.Text = string.Empty;
                txtNumberOfCones.Text = string.Empty;
                txtTexCount.Text = string.Empty;
                txtTwistFactor.Text = string.Empty;
                txtYarnOrderNo.Text = string.Empty;
                txtYarnType.Text = string.Empty;

                var selected = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                if (selected != null)
                {
                    txtNumberOfCones.Text = selected.YarnOP_NoOfConesSpun.ToString();
                    txtNettWeight.Text = selected.YarnOP_NettWeight.ToString();

                    if (selected.YarnOP_NettWeight != 0 && selected.YarnOP_NettWeight != 0)
                    {
                        AvgConeWeight = selected.YarnOP_NettWeight / selected.YarnOP_NoOfCones;
                    }
                    using (var context = new TTI2Entities())
                    {
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

                            /*
                            var CottonContract = context.TLADM_Cotton.Where(x => x.Cotton_Pk == Existing.Yarno_CottonContract_FK).FirstOrDefault();
                            if (CottonContract != null)
                                txtIdentification.Text = CottonContract.Cotton_Description;
                            */

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

        private void cmbYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Complete  && x.YarnOP_NoOfCones != 0).OrderBy(x => x.YarnOP_PalletNo).ToList();
                        cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                        cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                        formloaded = true;

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

        private void Scrapping_Closing(object sender, FormClosingEventArgs e)
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
                    repOptions.TransNumber = 1000;

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
