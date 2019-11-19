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
    public partial class frmYarnOrder : Form
    {
        Util core;
        bool formloaded;
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool _EditMode;

        IList<Int32> laydownList = null;

        public frmYarnOrder()
        {
            InitializeComponent();
            core = new Util();

            txtOrderWeight.KeyPress += core.txtWin_KeyPress;
            txtOrderWeight.KeyDown += core.txtWin_KeyDown;

            MandatoryFields = new string[][]
                {   new string[] {"cmbMerge", "Please select a cotton Merge", "0"},
                    new string[] {"cmbYarnType", "Please a yarn type", "1"},
                    new string[] {"dtpYarnOrderDate", "Please enter a yarn order date", "2"},
                    new string[] {"dtpYarnDeliveryDate", "Please enter a yarn delivery date", "3"},
                    new string[] {"cmbMachineNo", "Please select a machine no", "4"}, 
                    new string[] {"txtOrderWeight", "Please enter an order weight", "5"}, 
                    new string[] {"rtbPacking", "Please enter the packing details", "6"} 
                };

           SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();

                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumberUsed != null)
                    txtYarnOrder.Text = LastNumberUsed.col6.ToString();

                txtOrderWeight.Text = "0";
                txtTexCount.Text    = "0";
                txtTexFactor.Text   = "0";

                MandSelected = core.PopulateArray(MandatoryFields.Length, false);

                /*
                cotreceived = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TranType == 5 && !x.cotrx_Selected4Yarn).ToList();
                cmbCottonReceived.DataSource = cotreceived;
                cmbCottonReceived.ValueMember = "cotrx_Pk";
                cmbCottonReceived.DisplayMember = "cotrx_Return_No";
                */
                cmbYarnType.DataSource = context.TLADM_Yarn.OrderBy(x=>x.YA_Description).ToList();
                cmbYarnType.DisplayMember = "YA_Description";
                cmbYarnType.ValueMember = "YA_Id";
                cmbYarnType.SelectedValue = 0;

                var deptdetails = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                if (deptdetails != null)
                {
                    cmbMachineNo.DataSource = context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == deptdetails.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    cmbMachineNo.DisplayMember = "MD_AlternateDesc";
                    cmbMachineNo.ValueMember = "MD_Pk";
                    cmbMachineNo.SelectedValue = 0;
                }
                
                cmbMerge.DataSource = context.TLSPN_CottonMerge.ToList();
                cmbMerge.DisplayMember = "TLCTM_Description";
                cmbMerge.ValueMember = "TLCTM_Pk";
                cmbMerge.SelectedValue = -1;
                

                txtConeColour.Text = string.Empty;
                txtCottonOrigin.Text = string.Empty;
                cmbYarnType.SelectedValue = 0;
                txtOrderWeight.Text = string.Empty;
                txtTexCount.Text = string.Empty;
                txtTexFactor.Text = string.Empty;

                cmbPrevious.Visible = false;

                rtbPacking.Text = string.Empty;

                label1.Text = "Yarn Order Number";
                txtYarnOrder.Visible = true;
               
                _EditMode = false;

                formloaded = true;

            }
        }

        private void cmbYarnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var selected = (TLADM_Yarn)cmbYarnType.SelectedItem;
                if (selected != null)
                {
                    txtTexCount.Text = selected.YA_TexCount.ToString();
                    txtTexFactor.Text = selected.YA_Twist.ToString();
                    txtConeColour.Text = selected.YA_ConeColour;

                    var result = (from u in MandatoryFields
                                  where u[0] == oCmb.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                
                var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);

                if (!string.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                                
                //--------------------------------------------------------------------------------
                // Now update the order details
                //--------------------------------------------------------------------------
                TLSPN_YarnOrder yarnorder = new TLSPN_YarnOrder();
                using ( var context = new TTI2Entities())
                {
                    if (_EditMode)
                    {
                        var selected = (TLSPN_YarnOrder)cmbPrevious.SelectedItem;
                        if (selected != null)
                            yarnorder = context.TLSPN_YarnOrder.Find(selected.YarnO_Pk);
                    }

                    var Merge = (TLSPN_CottonMerge)cmbMerge.SelectedItem;
                    if (Merge != null)
                    {
                        yarnorder.YarnO_MergeContract_FK = (int)cmbMerge.SelectedValue;
                    }
 
                    var CottonOrigin = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Description == txtCottonOrigin.Text).FirstOrDefault();
                    if (CottonOrigin != null)
                        yarnorder.Yarno_CottonOrigin_FK = CottonOrigin.CottonOrigin_Pk;

                    yarnorder.YarnO_Date = dtpYarnOrderDate.Value;
                    yarnorder.YarnO_DelDate = dtpYarnDeliveryDate.Value;

                    var MachineNo = (TLADM_MachineDefinitions)cmbMachineNo.SelectedItem;
                    if (MachineNo != null)
                    {
                        yarnorder.Yarno_MachNo_FK = MachineNo.MD_Pk;
                    }

                    if(!_EditMode)
                          yarnorder.YarnO_OrderNumber = Convert.ToInt32(txtYarnOrder.Text);
                    
                    yarnorder.Yarno_OrderWeight = Convert.ToInt32(txtOrderWeight.Text);
                    yarnorder.Yarno_Packing     = rtbPacking.Text;
                                
                    var YarnType = (TLADM_Yarn)cmbYarnType.SelectedItem;
                    if(YarnType != null)
                    {
                        yarnorder.Yarno_YarnType_FK = YarnType.YA_Id;
                    }
                    
                    var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (deptDetail != null)
                    {
                        var tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 600).FirstOrDefault();
                        if (tranType != null)
                            yarnorder.YarnO_TransactionType_FK = tranType.TrxT_Pk;
                    }

                     if (!_EditMode)
                     {
                        //Dont forget to update the last number used
                        //-----------------------------------------------------------------------------------
                        var LastNumberUsed = context.TLADM_LastNumberUsed.Find(1);
                        if (LastNumberUsed != null)
                            LastNumberUsed.col6 += 1;

                         context.TLSPN_YarnOrder.Add(yarnorder);
                      }
                      try
                      {
                         context.SaveChanges();
                                          

                         SetUp();
                         MessageBox.Show("Records successfully updated to database");
                         if (_EditMode)
                         {
                             DialogResult res = MessageBox.Show("Would you like to reprint the report?", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                             if (res == DialogResult.Cancel)
                             {
                                 return;
                             }
                         }
                         frmViewReport vRep = new frmViewReport(9, yarnorder.YarnO_OrderNumber);
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

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxtB = sender as TextBox;

            if (oTxtB != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxtB.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if (oTxtB.TextLength > 0)
                        MandSelected[nbr] = true;
                    else
                        MandSelected[nbr] = false;
                }
           }
        }

        private void cmbMachineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmbCottonReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

                var selected = (TLSPN_CottonTransactions)oCmb.SelectedItem;
                if (selected != null)
                {
                    using ( var context = new TTI2Entities())
                    {
                        var CottonS = context.TLADM_Cotton.Where(x => x.Cotton_Pk == selected.cotrx_Supplier_FK).FirstOrDefault();
                        if (CottonS != null)
                        {
                            var CottonO = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Pk == CottonS.Cotton_Origin_FK).FirstOrDefault();
                            if (CottonO != null)
                            {
                                txtCottonOrigin.Text = CottonO.CottonOrigin_Description;
                            }
                        }
                      
                    }
                }
            }
        }

        private void rtbPacking_TextChanged(object sender, EventArgs e)
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

        private void frmYarnOrder_Load(object sender, EventArgs e)
        {

        }

        private void txtTexFactor_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCottonOrigin_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cmbYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
               
            }
        }

        private void btnRecall_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DisplayFields();
            }
        }

        void DisplayFields()
        {
            _EditMode = !_EditMode;
            if (_EditMode)
            {
                label1.Text = "Previous Yarn Orders";
                btnRecall.Text = "Update";
                cmbPrevious.Visible = true;
                cmbPrevious.Location = new Point(315, 35);
                txtYarnOrder.Visible = false;

                using (var context = new TTI2Entities())
                {
                    formloaded = false;
                    cmbPrevious.DataSource = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).OrderBy(x=>x.YarnO_OrderNumber).ToList();
                    cmbPrevious.ValueMember = "YarnO_Pk";
                    cmbPrevious.DisplayMember = "YarnO_OrderNumber";
                    formloaded = true;
                }

            }
            else
            {
                label1.Text = "Yarn Order Number";
                cmbPrevious.Visible = false;
                btnRecall.Text = "Edit";
                txtYarnOrder.Visible = true;
                SetUp();
            }
        }

        private void cmbPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrder)cmbPrevious.SelectedItem;
                if (selected != null)
                {
                    dtpYarnDeliveryDate.Value = selected.YarnO_DelDate;
                    dtpYarnOrderDate.Value = selected.YarnO_Date;
                    //cmbCottonReceived.SelectedValue = selected.Yarno_LayDown_FK;
                    cmbYarnType.SelectedValue = selected.Yarno_YarnType_FK;
                    cmbMachineNo.SelectedValue = selected.Yarno_MachNo_FK;
                    txtOrderWeight.Text = selected.Yarno_OrderWeight.ToString();
                    rtbPacking.Text = selected.Yarno_Packing;

                    using (var context = new TTI2Entities())
                    {
                        var TranType = context.TLSPN_CottonTransactions.Find(selected.Yarno_LayDown_FK);
                        if (TranType != null)
                        {
                            var Supplier = context.TLADM_Cotton.Find(TranType.cotrx_Supplier_FK);
                            if (Supplier != null)
                            {
                                var Origin = context.TLADM_CottonOrigin.Find(Supplier.Cotton_Origin_FK);
                                if (Origin != null)
                                {
                                    txtCottonOrigin.Text = Origin.CottonOrigin_Description;
                                }
                            }
                        }

                        var YarnDet = context.TLADM_Yarn.Find(selected.Yarno_YarnType_FK);
                        if (YarnDet != null)
                        {
                            txtTexCount.Text = YarnDet.YA_TexCount.ToString();
                            txtTexFactor.Text = YarnDet.YA_Twist.ToString();
                            txtConeColour.Text = YarnDet.YA_ConeColour;

                        }
                    }
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                }
            }
        }

        private void dtpYarnDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                    if (oDtp.Value <= dtpYarnOrderDate.Value)
                    {
                        MessageBox.Show("Delivery date cannot be on or before order date");
                        MandSelected[nbr] = false;
                    }
                }

            }
        }

        private void dtpYarnOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
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

        private void cmbMerge_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null)
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

       
    }
}
