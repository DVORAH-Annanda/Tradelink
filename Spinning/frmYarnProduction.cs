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
    public partial class frmYarnProduction : Form
    {
        bool formloaded;
        Util core;
        string[][] MandatoryFields;
        bool[] MandatorySelected;
        public frmYarnProduction()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
                {   new string[] {"cmbYarnOrder", "Please select a Yarn Order", "0","F"},
                    new string[] {"cmbPalettNo", "Please select a pallett number", "1","F"},
                    new string[] {"dtpDateProduced", "Please select a date produced", "2","F"},
                    new string[] {"txtGrossWeight", "Please enter the Gross weight", "3","T"},
                    new string[] {"txtTareWeight", "Please enter the Tare Weight", "4","T"},
                    new string[] {"txtNoOfConesSpun", "Please enter the number of cones spun", "5","F"},
                    new string[] {"cmbOperator", "Please select an operator", "6", "F"},
                    new string[] {"txtYarnGrade", "Please enter a yarn grade", "7", "F"}
                };

            txtNoOfCones.KeyPress += core.txtWin_KeyPress;
            txtNoOfCones.KeyDown += core.txtWin_KeyDown;

            txtTareWeight.KeyPress += core.txtWin_KeyPress;
            txtTareWeight.KeyDown += core.txtWin_KeyDownOEM;

            txtGrossWeight.KeyPress += core.txtWin_KeyPress;
            txtGrossWeight.KeyDown += core.txtWin_KeyDownOEM;

            txtNoOfConesSpun.KeyPress += core.txtWin_KeyPress;
            txtNoOfConesSpun.KeyDown += core.txtWin_KeyDown;

            SetUp(true);
        }

        void SetUp(bool start)
        {
            formloaded = false;
            if (start)
            {
                using (var context = new TTI2Entities())
                {
                    cmbYarnOrder.DataSource = context.TLSPN_YarnOrder.Where(x=>!x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                    cmbYarnOrder.DisplayMember = "YarnO_OrderNumber";
                    cmbYarnOrder.ValueMember = "YarnO_Pk";
                    cmbYarnOrder.SelectedValue = 0;

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("SPIN")).FirstOrDefault();
                    if (Dept != null)
                    {
                        cmbOperator.DataSource = context.TLADM_MachineOperators.Where(x=>x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).OrderBy(x => x.MachOp_Description).ToList();
                        cmbOperator.DisplayMember = "MachOp_Description";
                        cmbOperator.ValueMember = "MachOp_Pk";
                        cmbOperator.SelectedValue = 0;
                    }

                }

            }

            txtCottonType.Text = string.Empty;
            txtTareWeight.Text = string.Empty;
            txtGrossWeight.Text = string.Empty;
            txtNoOfConesSpun.Text = "0";
            txtNoOfCones.Text = "0";
            txtIdentification.Text = string.Empty;
            txtMachineNo.Text = string.Empty;
            txtYarnGrade.Text = string.Empty;
            cmbOperator.SelectedValue = 0;

            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;
        }

        private void cmbYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var yo = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                if (yo != null)
                {
                    
                    using (var context = new TTI2Entities())
                    {
                        var MachInfo = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == yo.Yarno_MachNo_FK).FirstOrDefault();
                        if (MachInfo != null)
                            txtMachineNo.Text = MachInfo.MD_MachineCode;

                        var YarnInfo = context.TLADM_Yarn.Where(x => x.YA_Id == yo.Yarno_YarnType_FK).FirstOrDefault();
                        if (YarnInfo != null)
                        {
                            txtYarnType.Text = YarnInfo.YA_Description;
                            txtTexCount.Text = Math.Round(YarnInfo.YA_TexCount, 2).ToString();
                            txtTwistFactor.Text = Math.Round(YarnInfo.YA_Twist, 2).ToString();
                            txtIdentification.Text = YarnInfo.YA_ConeColour; 
                        }

                        var Origin = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Pk == yo.Yarno_CottonOrigin_FK).FirstOrDefault();
                        if (Origin != null)
                            txtCottonType.Text = Origin.CottonOrigin_Description;

                        //-------------------------------------------------------------------
                        var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk && !x.YarnOP_Complete).ToList();
                        formloaded = false;
                        cmbPalettNo.DataSource = Existing;
                        cmbPalettNo.ValueMember = "YarnOP_Pk";
                        cmbPalettNo.DisplayMember = "YarnOP_PalletNo";
                        formloaded = true;

                        //-------------------------------------------------------------------
                    }
                    //-----end of context

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
                    if (result[3].Contains("F"))
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        if (oTxtB.TextLength > 0)
                            MandatorySelected[nbr] = true;
                        else
                            MandatorySelected[nbr] = false;
                    }
                    else
                    {
                        if (Convert.ToDecimal(oTxtB.Text) > 0)
                        {
                            int nbr = Convert.ToInt32(result[2].ToString());
                            MandatorySelected[nbr] = true;
                        }
                    }
                }
            }
        }

        private void cmbPalettNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrderPallets)cmbPalettNo.SelectedItem;
                if (selected != null)
                {
                    txtGrossWeight.Text   = selected.YarnOP_GrossWeight.ToString();
                    txtTareWeight.Text    = selected.YarnOP_TareWeight.ToString();
                    txtNoOfCones.Text     = selected.YarnOP_NoOfCones.ToString();
                    txtNoOfConesSpun.Text = selected.YarnOP_NoOfConesSpun.ToString();
                    txtYarnGrade.Text     = selected.YarnOP_Grade;
                    cmbOperator.SelectedValue = selected.YarnOP_Operator_FK;

                    using (var context = new TTI2Entities())
                    {
                        var YO = context.TLSPN_YarnOrder.Find(selected.YarnOP_YarnOrder_FK);
                        if (YO != null)
                        {
                        
                            var YT = context.TLADM_Yarn.Find(YO.Yarno_YarnType_FK);
                            if (YT != null)
                            {
                                txtIdentification.Text = YT.YA_ConeColour;

                                var Origin = context.TLADM_CottonOrigin.Find(YO.Yarno_CottonOrigin_FK);
                                if (Origin != null)
                                    txtCottonType.Text = Origin.CottonOrigin_Description;

                            }

                            var MT = context.TLADM_MachineDefinitions.Find(YO.Yarno_MachNo_FK);
                            if (MT != null)
                            {
                                txtMachineNo.Text = MT.MD_MachineCode;
                            }
                        }
                    }
                    if(selected.YarnOP_DatePacked != null)
                        dtpDateProduced.Value = Convert.ToDateTime(selected.YarnOP_DatePacked);

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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, true, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                var selected = (TLSPN_YarnOrderPallets)cmbPalettNo.SelectedItem;
                if (selected != null)
                {
                    if (cmbOperator.SelectedValue == null)
                    {
                        MessageBox.Show("Please select an operator from the drop down box");
                        return;
                    }

                    int NoOfCones = int.Parse(txtNoOfCones.Text);
                    if (NoOfCones <= 0)
                    {
                        MessageBox.Show("Please enter the number of cones spun");
                        return;

                    }

                    using (var context = new TTI2Entities())
                    {
                        var yo = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                        if (yo != null)
                        {
                             var YarnInfo = context.TLADM_Yarn.Where(x => x.YA_Id == yo.Yarno_YarnType_FK).FirstOrDefault();
                             if (YarnInfo != null)
                             {
                                 selected.YarnOP_YarnType_FK = YarnInfo.YA_Id;
                             }
                        }

                        selected = context.TLSPN_YarnOrderPallets.Find(selected.YarnOP_Pk);
                        selected.YarnOP_DatePacked = dtpDateProduced.Value;
                        selected.YarnOP_GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);
                        selected.YarnOP_TareWeight = Convert.ToDecimal(txtTareWeight.Text);
                        selected.YarnOP_NettWeight = Convert.ToDecimal(txtGrossWeight.Text) - Convert.ToDecimal(txtTareWeight.Text);
                        selected.YarnOP_NoOfCones = NoOfCones;
                        selected.YarnOP_NoOfConesSpun = Convert.ToInt32(txtNoOfConesSpun.Text);
                        selected.YarnOP_Grade = txtYarnGrade.Text;
                        if(cmbOperator.SelectedValue != null)
                            selected.YarnOP_Operator_FK = (int)cmbOperator.SelectedValue;

                        if (selected.YarnOP_NoOfConesSpun == selected.YarnOP_NoOfCones)
                        {
                            selected.YarnOP_Complete = true;
                        }
                        else
                            selected.YarnOP_Complete = false;

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("SPIN")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var Trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 500).FirstOrDefault();
                            if (Trantype != null)
                                selected.YarnOP_Store_FK = (int)Trantype.TrxT_ToWhse_FK;
                        }
                        
                        TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();

                        var Trx = context.TLSPN_YarnTransactions.Where(x => x.YarnTrx_YarnOrder_FK == selected.YarnOP_YarnOrder_FK &&
                                                                            x.YarnTrx_PalletNo_Fk == selected.YarnOP_Pk).FirstOrDefault();
                        if (Trx != null)
                        {
                                YarnT = context.TLSPN_YarnTransactions.Find(Trx.YarnTrx_Pk);
                                YarnT.YarnTrx_Date = dtpDateProduced.Value;
                                YarnT.YarnTrx_NettWeight = selected.YarnOP_NettWeight;
                                YarnT.YarnTrx_Cones = selected.YarnOP_NoOfCones;
                        }
                        else
                        {
                              var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                              if (deptDetail != null)
                              {
                                    var tranDetail = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 700).FirstOrDefault();
                                    if (tranDetail != null)
                                        YarnT.YarnTrx_TranType_FK = tranDetail.TrxT_Pk;
                               }

                               YarnT.YarnTrx_PalletNo_Fk = selected.YarnOP_Pk;
                               YarnT.YarnTrx_YarnOrder_FK = (int)selected.YarnOP_YarnOrder_FK;
                               YarnT.YarnTrx_SequenceNo = 0;
                               YarnT.YarnTrx_Cones = selected.YarnOP_NoOfCones;
                               YarnT.YarnTrx_WriteOff = false;
                               YarnT.YarnTrx_NettWeight = selected.YarnOP_NettWeight;
                               YarnT.YarnTrx_Date = dtpDateProduced.Value;

                                context.TLSPN_YarnTransactions.Add(YarnT);
                         }

                       
                        try
                        {
                            context.SaveChanges();
                            formloaded = false;
                            yo = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                            if (yo != null)
                            {
                                var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk && !x.YarnOP_Complete).ToList();
                                formloaded = false;
                                cmbPalettNo.DataSource = Existing;
                                formloaded = true;
                            }
                            MessageBox.Show("Records successfully updated to DataBase");
                            SetUp(false);
                            MandatorySelected[0] = true;
                            MandatorySelected[2] = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
            }
        }

        private void btnStockStatus_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded) 
            {
                frmStockStatusSelection stockStatus = new frmStockStatusSelection();
                stockStatus.ShowDialog(this);
            }
        }

        private void cmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {

            }
        }

        

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var yo = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                if (yo != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        //-------------------------------------------------------------------
                        var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk && x.YarnOP_Complete).ToList();
                        formloaded = false;
                        cmbPalettNo.DataSource = Existing;
                        cmbPalettNo.ValueMember = "YarnOP_Pk";
                        cmbPalettNo.DisplayMember = "YarnOP_PalletNo";
                        formloaded = true;

                        MandatorySelected = core.PopulateArray(MandatoryFields.Length, true);
                    }
                    //-------------------------------------------------------------------
                }
            }
        }

        private void cmbOperator_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void dtpDateProduced_ValueChanged(object sender, EventArgs e)
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

                    var YarnOrder = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                    if (YarnOrder != null)
                    {
                        if (oDtp.Value < YarnOrder.YarnO_Date)
                        {
                            MessageBox.Show("Yarn production date cannot be less than yarn order date", "Yarn order date " + YarnOrder.YarnO_Date.ToShortDateString());
                            MandatorySelected[nbr] = false;
                        }
                    }
                }
            }
        }
    }
}
