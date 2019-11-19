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
    public partial class frmYarnLabels : Form
    {
        bool formloaded;
        Util core;
        string[][] MandatoryFields;
        bool[] MandatorySelected;
        public frmYarnLabels()
        {
            InitializeComponent();
            core = new Util();
            txtNoOfCones.KeyDown += core.txtWin_KeyDown;
            txtNoOfCones.KeyPress += core.txtWin_KeyPress;
            MandatoryFields = new string[][]
                {   
                    new string[] {"cmbYarnOrder", "Please select a yarn order", "0"}
                };
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmbYarnOrder.DataSource = context.TLSPN_YarnOrder.Where(x=>!x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                cmbYarnOrder.DisplayMember = "YarnO_OrderNumber";
                cmbYarnOrder.ValueMember = "YarnO_Pk";
                
            }

            formloaded = true;
            txtNoOfCones.Text = "160";
            txtNoOfPallets.Text = "0";
            btnDeletePallet.Visible = false;

            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);

            cmbFromPalletNo.Enter +=cmbFromPalletNo_Enter;
        }

        private void cmbFromPalletNo_Enter(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && !String.IsNullOrEmpty(txtNoOfCones.Text))
            {
                var selected = (TLSPN_YarnOrderPallets)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Pallet = context.TLSPN_YarnOrderPallets.Find(selected.YarnOP_Pk);
                        var NoOfPallets = Convert.ToInt32(txtNoOfCones.Text);
                        
                        if (Pallet.YarnOP_NoOfCones != NoOfPallets)
                        {
                            Pallet.YarnOP_NoOfCones = NoOfPallets;
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
                            txtMachineNo.Text = MachInfo.MD_Description;
                        
                        var YarnInfo = context.TLADM_Yarn.Where(x => x.YA_Id == yo.Yarno_YarnType_FK).FirstOrDefault();
                        if (YarnInfo != null)
                        {
                            txtYarnType.Text = YarnInfo.YA_YarnType;
                            txtTexCount.Text = Math.Round(YarnInfo.YA_TexCount, 2).ToString();
                            txtTwistFactor.Text = Math.Round(YarnInfo.YA_Twist,2).ToString();
                            txtYarnDescription.Text = YarnInfo.YA_Description;
                        }

                        var Origin = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Pk == yo.Yarno_CottonOrigin_FK).FirstOrDefault();
                        if (Origin != null)
                            txtCottonType.Text = Origin.CottonOrigin_Description; 

                        //-------------------------------------------------------------------
                        var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk).OrderBy(x=>x.YarnOP_PalletNo).ToList();

                        cmbFromPalletNo.DataSource = Existing;
                        cmbFromPalletNo.ValueMember = "YarnOP_Pk";
                        cmbFromPalletNo.DisplayMember = "YarnOP_PalletNo";

                        cmbFromPalletNo.BindingContext = new System.Windows.Forms.BindingContext();
                        cmbToPalletNo.DataSource = Existing;
                        cmbToPalletNo.ValueMember = "YarnOP_Pk";
                        cmbToPalletNo.DisplayMember = "YarnOP_PalletNo";

                        txtNoOfPallets.Text = Existing.Count().ToString();

                        //-------------------------------------------------------------------

                        var result = (from u in MandatoryFields
                                      where u[0] == oCmbo.Name
                                      select u).FirstOrDefault();

                        if (result != null)
                        {
                            int nbr = Convert.ToInt32(result[2].ToString());
                            MandatorySelected[nbr] = true;
                        }

                    }
                    //-----end of context
                }
            }

        }

        private void btnLabels_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int _fromP = 1;
            int _toP = 1;

            if (oBtn != null && formloaded)
            {
                
                if (Convert.ToInt32(txtNoOfCones.Text) == 0)
                {
                    MessageBox.Show("please enter the number of cones for this pallet");
                    return;
                }

                var selected = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                if (selected != null)
                {
                    var fromP = (TLSPN_YarnOrderPallets)cmbFromPalletNo.SelectedItem;
                    if (fromP != null)
                    {
                        _fromP = fromP.YarnOP_PalletNo;
                    }

                    var toP = (TLSPN_YarnOrderPallets)cmbToPalletNo.SelectedItem;
                    if (toP != null)
                    {
                        _toP = toP.YarnOP_PalletNo;
                    }
                    
                    frmViewReport vRep = new frmViewReport(10, selected.YarnO_Pk, _fromP, _toP);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    
                }
       
            }
        }

        private void btnAddPallet_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, true, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                }
                else
                {
                    using (var context = new TTI2Entities())
                    {
                        var yo = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                        if (yo != null)
                        {
                            TLSPN_YarnOrderPallets pallets = new TLSPN_YarnOrderPallets();
                            pallets.YarnOP_YarnOrder_FK = yo.YarnO_Pk;
                         
                            var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk).OrderBy(x => x.YarnOP_PalletNo).ToList();

                            pallets.YarnOP_PalletNo = 1 + Existing.Count;
                            pallets.YarnOP_NoOfCones = 160;

                            txtNoOfPallets.Text = (1 + Existing.Count).ToString();
                            txtNoOfCones.Text = "160";
                            try
                            {
                                context.TLSPN_YarnOrderPallets.Add(pallets);
                                context.SaveChanges();
                                var ExistingP = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yo.YarnO_Pk).OrderBy(x => x.YarnOP_PalletNo).ToList();
                                // cmbPalettNo.DataSource = ExistingP;

                                //-------------------------------------------------------------------
                                cmbFromPalletNo.DataSource = ExistingP;
                                cmbFromPalletNo.ValueMember = "YarnOP_Pk";
                                cmbFromPalletNo.DisplayMember = "YarnOP_PalletNo";

                                cmbToPalletNo.DataSource = ExistingP;
                                cmbToPalletNo.ValueMember = "YarnOP_Pk";
                                cmbToPalletNo.DisplayMember = "YarnOP_PalletNo";

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

       
        private void btnDeletePallet_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DialogResult res = MessageBox.Show("Please confirm this transaction", "This transaction cannot be reversed", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    
                }
            }
        }

        private void cmbFromPalletNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
            }
        }

        private void cmbToPalletNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrderPallets)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var pallet = context.TLSPN_YarnOrderPallets.Find(selected.YarnOP_Pk);
                        if (pallet != null)
                            txtNoOfCones.Text = pallet.YarnOP_NoOfCones.ToString();

                    }

                }
            }
        }

        private void txtNoOfCones_CellLeave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var selected = (TLSPN_YarnOrderPallets)cmbToPalletNo.SelectedItem;
                if (selected != null)
                {
                    if (selected.YarnOP_NoOfCones != Convert.ToInt32(txtNoOfCones.Text))
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Pallets = context.TLSPN_YarnOrderPallets.Find(selected.YarnOP_Pk);
                            if (Pallets != null)
                            {
                                Pallets.YarnOP_NoOfCones = Convert.ToInt32(txtNoOfCones.Text);

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
            }
        }
    }
}
