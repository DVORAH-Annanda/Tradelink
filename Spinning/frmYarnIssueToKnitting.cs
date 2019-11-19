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
using System.Threading;

namespace Spinning
{
    public partial class frmYarnIssueToKnitting : Form
    {
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool formloaded;
        Util core;
        bool Transactions;
        decimal AvgConweight;
        public frmYarnIssueToKnitting()
        {
            InitializeComponent();
            MandatoryFields = new string[][]
                {   new string[] {"dateTimePicker1", "Please select a Transaction Date", "0"},
                    new string[] {"comboYarnNo", "Please select a yarn order number", "1"},
                    new string[] {"cmboPalletNumbers", "Please select a pallet number", "2"},
                    new string[] {"txtNumberOfCones", "Please select the number of cones", "3"}
                };
            core = new Util();
            Transactions = false;
            SetUp(true);
        }
        
        void SetUp(bool mand)
        {
            formloaded = false;
            AvgConweight = 0.00M;

            using (var context = new TTI2Entities())
            {
                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumberUsed != null)
                {
                    txtDeliveryNoteNumber.Text = LastNumberUsed.col8.ToString();
                }

                comboYarnNo.DataSource = context.TLSPN_YarnOrder.Where(X => !X.Yarno_Closed).OrderBy(X => X.YarnO_OrderNumber).ToList();
                comboYarnNo.DisplayMember = "YarnO_OrderNumber";
                comboYarnNo.ValueMember = "YarnO_Pk";

                /*
                cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_Complete && !x.YarnOP_Issued && x.YarnOP_NoOfCones != 0).OrderBy(x => x.YarnOP_PalletNo).ToList();
                cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                cmboPalletNumbers.SelectedValue = 0;
                 * */

            }
            if(mand)
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //dont forget last number used 
            Button oBtn = sender as Button;
            TLADM_TranactionType tranType = null;
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
                    var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumberUsed != null)
                        LastNumberUsed.col8 += 1;

                    //---------------------------------------------------------
                    TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();

                    var YO = (TLSPN_YarnOrder)comboYarnNo.SelectedItem;
                    if (YO != null)
                        YarnT.YarnTrx_YarnOrder_FK = YO.YarnO_Pk;

                    YarnT.YarnTrx_SequenceNo = Convert.ToInt32(txtDeliveryNoteNumber.Text);
                    YarnT.YarnTrx_Date = dateTimePicker1.Value;

                    var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (deptDetail != null)
                    {
                        //tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 800).FirstOrDefault();
                        tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 700).FirstOrDefault();
                        if (tranType != null)
                        {
                            YarnT.YarnTrx_TranType_FK = tranType.TrxT_Pk;
                            YarnT.YarnTrx_FromDep_FK = tranType.TrxT_FromWhse_FK;
                            YarnT.YarnTrx_ToDep_FK = tranType.TrxT_ToWhse_FK;
                        }
                    }

                    var PalletNo = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                    if (PalletNo != null)
                        YarnT.YarnTrx_PalletNo_Fk = PalletNo.YarnOP_Pk;

                    var NoCones = Convert.ToInt32(txtNumberOfCones.Text);
                    YarnT.YarnTrx_Cones = NoCones;

                    var NettWeight = Convert.ToDecimal(txtNettWeight.Text);
                    YarnT.YarnTrx_NettWeight = Convert.ToDecimal(txtNettWeight.Text);
                    //----------------------------------------------------------------
                    // This Transaction updates the spinning rocords
                    var PalletStore = context.TLSPN_YarnOrderPallets.Find(PalletNo.YarnOP_Pk);
                    if (PalletStore != null)
                    {
                        PalletStore.YarnOP_NoOfConesSpun  -= NoCones;
                        PalletStore.YarnOP_YarnAvailable = true;
                        PalletStore.YarnOP_Issued = false;
                        PalletStore.YarnOP_Store_FK = (int)tranType.TrxT_ToWhse_FK;
                        PalletStore.YarnOP_YarnType_FK = YO.Yarno_YarnType_FK;
                        PalletStore.YarnOP_NettWeight = NettWeight;
                   
                        
                    }
                    //---------------------------------------------------------
                    try
                    {
                        context.TLSPN_YarnTransactions.Add(YarnT);
                        context.SaveChanges();
                        SetUp(false);
                        Transactions = true;
                        MessageBox.Show("Data saved to database successfully");
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
            if (oCmbo != null && formloaded)
            {
                txtIdentification.Text = string.Empty;
                txtNettWeight.Text = string.Empty;
                txtNumberOfCones.Text = string.Empty;
                txtTexCount.Text = string.Empty;
                txtTwistFactor.Text = string.Empty;
              
                txtYarnType.Text = string.Empty;

                var selected = (TLSPN_YarnOrderPallets)cmboPalletNumbers.SelectedItem;
                if (selected != null)
                {
                    txtNumberOfCones.Text = selected.YarnOP_NoOfConesSpun.ToString();
                    txtNettWeight.Text = selected.YarnOP_NettWeight.ToString();

                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == selected.YarnOP_YarnOrder_FK).FirstOrDefault();
                        if (Existing != null)
                        {
                            var result = (from u in MandatoryFields
                                          where u[0] == oCmbo.Name
                                          select u).FirstOrDefault();
                            if (result != null)
                            {
                                int nbr = Convert.ToInt32(result[2].ToString());
                                MandSelected[nbr] = true;
                                txtNettWeight.Text = selected.YarnOP_NettWeight.ToString();
                                AvgConweight = selected.YarnOP_NettWeight / selected.YarnOP_NoOfConesSpun;
                            }
                            

                            var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Existing.Yarno_YarnType_FK).FirstOrDefault();
                            if (YarnType != null)
                            {
                                txtYarnType.Text = YarnType.YA_YarnType;
                                txtTwistFactor.Text = Math.Round(YarnType.YA_Twist, 2).ToString();
                                txtTexCount.Text = Math.Round(YarnType.YA_TexCount, 2).ToString();
                                txtIdentification.Text = YarnType.YA_Description;
                            }

                           
                        }
                    }
                }
            }
        }

        private void comboYarnNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {

                var selected = (TLSPN_YarnOrder)comboYarnNo.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();
                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }

                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmboPalletNumbers.DataSource = null;
                        cmboPalletNumbers.Items.Clear();
                        cmboPalletNumbers.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Complete && x.YarnOP_NoOfConesSpun > 0).OrderBy(x => x.YarnOP_PalletNo).ToList();
                        cmboPalletNumbers.DisplayMember = "YarnOP_PalletNo";
                        cmboPalletNumbers.ValueMember = "YarnOP_Pk";
                        formloaded = true;
                    }
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

        private void cmb_SelectedIndexChange(object sender, EventArgs e)
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

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null && oTxt.TextLength > 0 && Convert.ToInt32(oTxt.Text) > 0)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
                else 
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = false;
                }

                if (oTxt.Name == "txtNumberOfCones" && oTxt.Text.Length != 0)
                {
                    int NoSold = Convert.ToInt32(oTxt.Text);
                    txtNettWeight.Text = Math.Round(Math.Round(NoSold * AvgConweight), 0).ToString();
                }
            }
        }

        private void Issue_Closing(object sender, FormClosingEventArgs e)
        {
            if (Transactions)
            {
                if (MessageBox.Show("Do you wish to print the transaction listing report?", "Print Transactions",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Cancel the Closing event
                    //================================================
                    e.Cancel = true;

                    YarnTOptions repOptions = new YarnTOptions();
                    repOptions.FromDate     = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ToDate       = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ReportSelected = 0;
                    repOptions.TransNumber = 800;

                    frmViewReport vRep = new frmViewReport(13, repOptions);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    Transactions = false;
                }
            }
       }

        private static void StartCounting()
        {
            var thread = new Thread(() =>
            {
                for (var x = 0; x < 10; x++)
                {
                    MessageBox.Show(x.ToString());
                    Thread.Sleep(10);
                }
            });

            thread.Start();
        }
    }
}
