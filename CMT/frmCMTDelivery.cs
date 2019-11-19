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
namespace CMT
{
    public partial class frmCMTDelivery : Form
    {
        bool formloaded;
        CMTQueryParameters QueryParms;
        CMTRepository Repo; 
        
        public frmCMTDelivery()
        {
            InitializeComponent();
            Repo = new CMTRepository();

            this.cmboCurrentPIMultiple.CheckStateChanged += new System.EventHandler(this.cmboPanelIssue_CheckStateChanged);
            ;
        }

        private void frmCMTDelivery_Load(object sender, EventArgs e)
        {
            formloaded = false;
            label2.Text = "0";
            label6.Text = string.Empty;
            label7.Text = string.Empty;
          
            QueryParms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                try
                {
                   formloaded = false;
                    /*
                   var Existing = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Closed).OrderBy(x => x.CMTPI_Number).ToList();
                   foreach (var Row in Existing)
                   {
                        cmboCurrentPIMultiple.Items.Add(new CMT.CheckComboBoxItem(Row.CMTPI_Pk, Row.CMTPI_Display.ToString(), false));
                   }
                   */

                   cmboCMT.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                    cmboCMT.DisplayMember = "Dep_Description";
                    cmboCMT.ValueMember = "Dep_Id";
                    cmboCMT.SelectedValue = -1;

                   formloaded = true;
                }
                catch (Exception ex)
                {
                        MessageBox.Show(ex.Message);
                        return;
                }
            }

            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboPanelIssue_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    if (label6.Text.Length == 0)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var selected = context.TLCMT_PanelIssue.Find(item._Pk);
                            if (selected != null)
                            {
                                var TranType = context.TLADM_TranactionType.Find(selected.CMTPI_TranType_FK);
                                if (TranType != null)
                                {
                                    if (selected.CMTPI_FromWhse_FK != 0)
                                        label6.Text = context.TLADM_WhseStore.Find(TranType.TrxT_FromWhse_FK).WhStore_Description;
                                    else
                                        label6.Text = String.Empty;

                                    label7.Text = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK).WhStore_Description;
                                }

                                var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == selected.CMTPI_Department_FK).FirstOrDefault();
                                if (LNU != null)
                                {
                                    label2.Text = LNU.col2.ToString();
                                }
                            }
                        }

                    }
                
                    QueryParms.PanelIssue.Add(Repo.LoadPanelIssue(item._Pk));

                }
                else
                {
                    var value = QueryParms.PanelIssue.Find(it => it.CMTPI_Pk  == item._Pk);
                    if (value != null)
                        QueryParms.PanelIssue.Remove(value);

                }
            }
        }

        private void cmboBIFIssue_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.BIFInTransit.Add(Repo.LoadBIFTransit(item._Pk));

                }
                else
                {
                    var value = QueryParms.BIFInTransit.Find(it => it.BIFT_Pk == item._Pk);
                    if (value != null)
                        QueryParms.BIFInTransit.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            frmCMTViewRep vRep = null;
            if (oBtn != null && formloaded)
            {
                bool Reprint = false;

                 if (QueryParms.PanelIssue.Count == 0)
                 {
                        MessageBox.Show("Please select a Delivery from the drop down list");
                        return;
                 }

                 int Dept_Fk = 0;
                 int DeliveryNumber = Convert.ToInt32(label2.Text);

                 if (chkPrevious.Checked)
                     Reprint = true;

                 vRep = new frmCMTViewRep(2, QueryParms, Reprint);
                 int h = Screen.PrimaryScreen.WorkingArea.Height;
                 int w = Screen.PrimaryScreen.WorkingArea.Width;
                 vRep.ClientSize = new Size(w, h);
                 vRep.ShowDialog(this);

                 if (!chkPrevious.Checked)
                 {
                     using (var context = new TTI2Entities())
                     {
                         foreach (var Query in QueryParms.PanelIssue)
                         {
                             var Existing = context.TLCMT_PanelIssue.Find(Query.CMTPI_Pk);
                             if (Existing != null)
                             {
                                 Existing.CMTPI_Closed = true;
                                 Existing.CMTPI_DeliveryNumber = DeliveryNumber;
                                 Existing.CMTPI_Display = "DN -" + DeliveryNumber.ToString() + " - TL " + Existing.CMTPI_Number.ToString();

                                 var Dept = context.TLADM_Departments.Find(Query.CMTPI_Department_FK);
                                 if (Dept != null)
                                 {
                                     Dept_Fk = Dept.Dep_Id;

                                     var TransType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 200 && x.TrxT_Department_FK == Dept.Dep_Id).FirstOrDefault();
                                     if (TransType != null)
                                         Existing.CMTPI_TranType_FK = TransType.TrxT_Pk;
                                 }
                             }
                         }

                         var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept_Fk).FirstOrDefault();
                         if (LNU != null)
                         {
                                 LNU.col2 += 1; ;
                         }

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

        private void cmboCurrentPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_Departments)cmboCMT.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        cmboCurrentPIMultiple.Items.Clear();

                        var Existing = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Closed && x.CMTPI_Department_FK == selected.Dep_Id).OrderBy(x => x.CMTPI_Number).ToList();
                        foreach (var Row in Existing)
                        {
                            cmboCurrentPIMultiple.Items.Add(new CMT.CheckComboBoxItem(Row.CMTPI_Pk, Row.CMTPI_Display.ToString(), false));
                        }
                    }
                }
                /*
                var selected = (TLCMT_PanelIssue)cmboCMT.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var TranType = context.TLADM_TranactionType.Find(selected.CMTPI_TranType_FK);
                        if (TranType != null)
                        {
                            label6.Text = context.TLADM_WhseStore.Find(TranType.TrxT_FromWhse_FK).WhStore_Description;
                            label7.Text = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK).WhStore_Description;
                        }

                        var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == selected.CMTPI_Department_FK).FirstOrDefault();
                        if (LNU != null)
                        {
                            label2.Text = LNU.col2.ToString();
                        }
                    }
               }
               */

            }
        }

       

        private void cmboCurrentPIMultiple_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                if (!oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }

        }

        private void chkPrevious_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            IList<TLCMT_PanelIssue> PanelIssue = new List<TLCMT_PanelIssue>();

            if (oChk != null && oChk.Checked && formloaded)
            {
                var selected = (TLADM_Departments)cmboCMT.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Dt = dtpTransDate.Value;
                        var ExistingGrouped = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Closed && x.CMTPI_Date >= Dt && x.CMTPI_Department_FK == selected.Dep_Id  ).GroupBy(x => x.CMTPI_DeliveryNumber).ToList();
                        foreach (var Grp in ExistingGrouped)
                        {

                            PanelIssue.Add(Grp.FirstOrDefault());
                        }

                        formloaded = false;
                        cmboPrevious.DataSource = PanelIssue;
                        cmboPrevious.ValueMember = "CMTPI_Pk";
                        cmboPrevious.DisplayMember = "CMTPI_DeliveryNumber";
                        cmboPrevious.SelectedValue = -1;
                        formloaded = true;
                    }
                }
            }
            else
            {
                label2.Text = string.Empty;
                label6.Text = string.Empty;
                label7.Text = string.Empty;

                cmboPrevious.DataSource = null;
                cmboPrevious.Items.Clear();

            }
        }

        private void cmboPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                var SelectedItem = (TLCMT_PanelIssue)oCmbo.SelectedItem;
                if (SelectedItem != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var PI = context.TLCMT_PanelIssue.Find(SelectedItem.CMTPI_Pk);
                        if (PI != null)
                        {
                            label2.Text = SelectedItem.CMTPI_DeliveryNumber.ToString().PadLeft(5, '0');
                            label6.Text = context.TLADM_WhseStore.Find(PI.CMTPI_FromWhse_FK).WhStore_Description;
                            label7.Text = context.TLADM_Departments.Find(PI.CMTPI_Department_FK).Dep_Description;

                            var Items = context.TLCMT_PanelIssue.Where(x => x.CMTPI_DeliveryNumber == SelectedItem.CMTPI_DeliveryNumber).ToList();
                            if (Items.Count != 0)
                            {
                                QueryParms = new CMTQueryParameters();

                                foreach (var Item in Items)
                                {
                                    QueryParms.PanelIssue.Add(Repo.LoadPanelIssue(Item.CMTPI_Pk));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
