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

namespace Knitting
{
    public partial class frmKnitOrderAdjust : Form
    {
        Boolean FormLoaded;
        int _KnitOrderPk;
        TLKNI_Order KnitO;

        public frmKnitOrderAdjust(int KnitOrderPk)
        {
            InitializeComponent();
            _KnitOrderPk = KnitOrderPk;
        }

        private void frmKnitOrderAdjust_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                comboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                comboGreige.ValueMember = "TLGreige_Id";
                comboGreige.DisplayMember = "TLGreige_Description";
                comboGreige.SelectedValue = -1;
                
                var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Department != null)
                {
                    comboMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Department.Dep_Id).OrderBy(x => x.MD_AlternateDesc).ToList();
                    comboMachine.DisplayMember = "MD_AlternateDesc";
                    comboMachine.ValueMember = "MD_Pk";
                    comboMachine.SelectedValue = -1;

                    KnitO = context.TLKNI_Order.Find(_KnitOrderPk);

                    if (KnitO != null)
                    {
                        cbResetYarnAllocated.Checked = false;

                        if (KnitO.KnitO_Closed)
                           rbClosed.Checked = true;
                        else
                           rbActive.Checked = true;

                        label5.Text = KnitO.KnitO_OrderNumber.ToString();
                        comboGreige.SelectedValue = KnitO.KnitO_Product_FK;
                        label4.Text = KnitO.KnitO_NoOfPieces.ToString();

                        comboMachine.SelectedValue = KnitO.KnitO_Machine_FK;

                    }
                }

            }

            FormLoaded = true;
        }

        private void comboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                var Selected = (TLADM_Griege)oCmbo.SelectedItem;
                if (Selected != null)
                {
                    using ( var context = new TTI2Entities())
                    {
                        comboMachine.DataSource = null;
                        comboMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_GreigeType_FK == Selected.TLGreige_Id).ToList();
                        comboMachine.DisplayMember = "MD_AlternateDesc";
                        comboMachine.ValueMember = "MD_Pk";
                        comboMachine.SelectedValue = -1;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                var GreigeSelected = (TLADM_Griege)comboGreige.SelectedItem;
                if (GreigeSelected == null)
                {
                    MessageBox.Show("Please select a Quality item from the drop down box");
                    return;
                }

                var MachineSelected = (TLADM_MachineDefinitions)comboMachine.SelectedItem;
                if (MachineSelected == null)
                {
                    MessageBox.Show("Please select a Machine from the drop down box provided");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var KO = context.TLKNI_Order.Find(KnitO.KnitO_Pk);
                    if (KO != null)
                    {
                        KO.KnitO_Machine_FK = MachineSelected.MD_Pk;
                        KO.KnitO_Product_FK = GreigeSelected.TLGreige_Id;

                        if (rbActive.Checked)
                        {
                            KO.KnitO_Closed = false;
                            KO.KnitO_ClosedDate = null;
                        }
                        else
                        {
                            KO.KnitO_Closed = true;
                            if (KnitO.KnitO_ClosedDate == null)
                                KnitO.KnitO_ClosedDate = DateTime.Now;
                        }

                        var GreigeProduction = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk).ToList();
                        foreach (var Item in GreigeProduction)
                        {
                            Item.GreigeP_Greige_Fk = GreigeSelected.TLGreige_Id;
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully stored to the database");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }
        }

        private void cbResetYarnAllocated_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if (oChk != null && FormLoaded)
            {
                if (oChk.Checked)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confimation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        using (var context = new TTI2Entities())
                        {
                            if (KnitO == null)
                                KnitO = context.TLKNI_Order.Find(_KnitOrderPk);

                            if (KnitO != null && KnitO.KnitO_YarnAssigned)
                            {
                                KnitO.KnitO_YarnAssigned = false;

                                var YarnAllocs = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KnitO.KnitO_Pk).ToList();
                                foreach (var YarnAlloc in YarnAllocs)
                                {
                                    var YarnPallet = context.TLKNI_YarnOrderPallets.Find(YarnAlloc.TLKYT_YOP_FK);
                                    if(YarnPallet != null)
                                    {
                                        YarnPallet.TLKNIOP_ReservedBy = 0;
                                        YarnPallet.TLKNIOP_NettWeightReserved = 0;
                                        YarnPallet.TLKNIOP_ConesReserved = 0;
                                        YarnPallet.TLKNIOP_ReservedDate = null;
                                        YarnPallet.TLKNIOP_AdditionalYarn = 0;
                                        YarnPallet.TLKNIOP_NettWeightReturned = 0;
                                    }
                                }

                                context.TLKNI_YarnAllocTransctions.RemoveRange(context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KnitO.KnitO_Pk));

                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Data successfully saved to database");
                                    return;
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
                            else
                            {
                                MessageBox.Show("Unable to action request. No yarn currently assigned");
                            }
                        }
                    }
                }
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                frmKnitViewRep vRep = new frmKnitViewRep(7, KnitO.KnitO_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
