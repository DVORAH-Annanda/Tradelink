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
    public partial class frmYarnSales : Form
    {
        bool formloaded;
        Util core;
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool Transactions;
        decimal AvgConeWeight;

        public frmYarnSales()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
                {   new string[] {"dateTimePicker1", "Please select a TransAction Date", "0"},
                    new string[] {"cmboCustomerNo", "Please select a customer number", "1"}, 
                    new string[] {"txtCustomerOrder", "Please enter a customer order no", "2"}, 
                    new string[] {"cmboYarnOrder", "Please select a yarn order number", "3"},
                    new string[] {"cmbPalletNo", "Please select a pallet number", "4"},
                    new string[] {"txtConesSold", "Please enter the number of cones", "5"},
                    new string[] {"txtPalletWeightSold", "Please enter the pallet weight sold", "6"}
                 };

            txtConesSold.KeyPress += core.txtWin_KeyPress;
            txtConesSold.KeyDown += core.txtWin_KeyDown;

            txtPalletWeightSold.KeyPress += core.txtWin_KeyPress;
            txtPalletWeightSold.KeyDown += core.txtWin_KeyDownOEM;

            txtConesAvailable.TextAlign = HorizontalAlignment.Right;
            txtConesAvailable.ReadOnly = true;

            txtPalletWeightAvailable.TextAlign = HorizontalAlignment.Right;
            txtPalletWeightAvailable.ReadOnly = true;

            txtPalletWeightSold.Text = "0.00";
            txtPalletWeightSold.TextAlign = HorizontalAlignment.Right;

            txtConesSold.Text = "0";
            txtConesSold.TextAlign = HorizontalAlignment.Right;

            Transactions = false;
            SetUp();
        }

        void SetUp()
        {
           
            formloaded = false;
            AvgConeWeight = 0.00M;

            using (var context = new TTI2Entities())
            {
                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumberUsed != null)
                {
                    txtDeliveryNoteNumber.Text = LastNumberUsed.col9.ToString();
                }

                cmboYarnOrder.DataSource = context.TLSPN_YarnOrder.OrderBy(x => x.YarnO_OrderNumber).ToList();
                cmboYarnOrder.DisplayMember = "YarnO_OrderNumber";
                cmboYarnOrder.ValueMember = "YarnO_Pk";
                cmboYarnOrder.SelectedValue = 0;

                cmboCustomerNo.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomerNo.DisplayMember = "Cust_Description";
                cmboCustomerNo.ValueMember = "Cust_Pk";
                cmboCustomerNo.SelectedValue = 0;

                cmbPalletNo.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_Complete && x.YarnOP_NoOfCones != 0).OrderBy(x => x.YarnOP_PalletNo).ToList();
                cmbPalletNo.DisplayMember = "YarnOP_PalletNo";
                cmbPalletNo.ValueMember = "YarnOP_Pk";
                cmbPalletNo.SelectedValue = 0;

                txtCustomerOrder.Text = string.Empty;
            }

            rtbAddress.Text = string.Empty;
            
            txtConesSold.Text = "0";
            txtConesAvailable.Text = "0";

            txtPalletWeightAvailable.Text = "0.00";
            txtPalletWeightSold.Text = "0.00";


            txtPalletWeightSold.KeyPress += core.txtWin_KeyPress;
            txtPalletWeightSold.KeyDown += core.txtWin_KeyDownOEM;

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;
           

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // dont for get about last number userd!!!!!
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
                     var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                     if (LastNumberUsed != null)
                         LastNumberUsed.col9   += 1;
                     
                     //---------------------------------------------------------
                     TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();
                     //-------------------------------------------------------------
                     // Review Document for a list 
                     //-------------------------------------------------------------

                     var YO = (TLSPN_YarnOrder)cmboYarnOrder.SelectedItem;
                     if (YO != null)
                         YarnT.YarnTrx_YarnOrder_FK = YO.YarnO_Pk;

                     YarnT.YarnTrx_SequenceNo = Convert.ToInt32(txtDeliveryNoteNumber.Text);
                     YarnT.YarnTrx_Date = dateTimePicker1.Value;
                     YarnT.YarnTrx_OrderNo = txtCustomerOrder.Text;
                     
                     var PalletNo = (TLSPN_YarnOrderPallets)cmbPalletNo.SelectedItem;
                     if (PalletNo != null)
                         YarnT.YarnTrx_PalletNo_Fk = PalletNo.YarnOP_Pk;

                     var Customer = (TLADM_CustomerFile)cmboCustomerNo.SelectedItem;
                     if (Customer != null)
                         YarnT.YarnTrx_Customer_FK = Customer.Cust_Pk;

                  
                     var NoCones = Convert.ToInt32(txtConesSold.Text);
                     YarnT.YarnTrx_Cones = NoCones;

                     var WeightSold = Convert.ToDecimal(txtPalletWeightSold.Text);
                     YarnT.YarnTrx_NettWeight = WeightSold;

                     var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                     if (deptDetail != null)
                     {
                         var tranDetail = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 900).FirstOrDefault();
                         if (tranDetail != null)
                         {
                             YarnT.YarnTrx_TranType_FK = tranDetail.TrxT_Pk;
                             YarnT.YarnTrx_FromDep_FK = tranDetail.TrxT_FromWhse_FK;
                             YarnT.YarnTrx_ToDep_FK = tranDetail.TrxT_ToWhse_FK;

                         }
                     }

                     var PalletStore = context.TLSPN_YarnOrderPallets.Find(PalletNo.YarnOP_Pk);
                     if (PalletStore != null)
                     {
                         PalletStore.YarnOP_NoOfConesSpun -= NoCones;
                         PalletStore.YarnOP_NettWeight -= WeightSold;
                         PalletStore.YarnOP_Sold = true;
                         PalletStore.YarnOP_DateDispatched = dateTimePicker1.Value;
                     }
                     //--------------------------------------------------------------------------------------

                     try
                     {
                         context.TLSPN_YarnTransactions.Add(YarnT);
                         context.SaveChanges();
                         SetUp();
                         Transactions = true;
                         MessageBox.Show("Data successfully saved to database");
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }
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

        private void cmboCustomerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_CustomerFile)cmboCustomerNo.SelectedItem;
                if (selected != null)
                {
                    rtbAddress.Text = selected.Cust_Address1;

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

        private void cmbPalletNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrderPallets)cmbPalletNo.SelectedItem;
                if (selected != null)
                {
                    txtConesAvailable.Text = selected.YarnOP_NoOfConesSpun.ToString();
                    txtPalletWeightAvailable.Text = selected.YarnOP_NettWeight.ToString();
                    txtPalletWeightSold.Text = "0.00";
                    txtConesSold.Text = "0";

                    if(selected.YarnOP_NoOfConesSpun != 0 && selected.YarnOP_NettWeight != 0)
                          AvgConeWeight = selected.YarnOP_NettWeight / selected.YarnOP_NoOfCones;

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

        private void cmboYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrder)cmboYarnOrder.SelectedItem;
                if (selected != null)
                {
                    using ( var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmbPalletNo.DataSource = context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Complete && !x.YarnOP_Issued && !x.YarnOP_Scrapped && !x.YarnOP_Sold).OrderBy(x => x.YarnOP_PalletNo).ToList();
                        cmbPalletNo.DisplayMember = "YarnOP_PalletNo";
                        cmbPalletNo.ValueMember = "YarnOP_Pk";
                        cmbPalletNo.SelectedValue = 0;
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
                        if (oTxt.TextLength > 0)
                        {
                            MandSelected[nbr] = true;
                            if (oTxt.Name == "txtConesSold" && oTxt.Text.Length != 0)
                            {
                                int noSold = Convert.ToInt32(oTxt.Text);
                                txtPalletWeightSold.Text = Math.Round(noSold * AvgConeWeight, 1).ToString();
                            }
                        }
                        else
                            MandSelected[nbr] = false;


                        
                    
                 }
            }
        }

        private void Sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Transactions)
            {
                if (MessageBox.Show("Do you wish to print the transaction listing report?", "Print Transactions",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    YarnTOptions repOptions = new YarnTOptions();
                    repOptions.FromDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ToDate = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    repOptions.ReportSelected = 0;
                    repOptions.TransNumber = 900;
                 
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
