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
    public partial class frmReconKnitOrder : Form
    {
        bool formloaded;
        public frmReconKnitOrder()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;

            using (var context = new TTI2Entities())
            {
                cmbKnitOrders.DataSource = context.TLKNI_Order.Where(x => !x.KnitO_Confirmed && x.KnitO_Closed).ToList();
                cmbKnitOrders.ValueMember = "KnitO_Pk";
                cmbKnitOrders.DisplayMember = "KnitO_OrderNumber";
                cmbKnitOrders.SelectedValue = 0;
            }
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    var _Begin = dtpDate.Value.ToShortDateString();
                    DateTime ped = Convert.ToDateTime(_Begin);
                    DateTime time = dtpTime.Value;

                    var KO = (TLKNI_Order)cmbKnitOrders.SelectedItem;
                    if (KO != null)
                    {
                        KO = context.TLKNI_Order.Find(KO.KnitO_Pk);
                        KO.KnitO_ProductionEndDate = ped.AddHours(time.Hour);
                        KO.KnitO_Confirmed = true;

                        if (chkProduction.Checked)
                            KO.KnitO_ProductionCaptured = true;

                        if (chkYarnReturned.Checked)
                            KO.KnitO_YarnReturned = true;

                        if (chkReOpenKnitOrder.Checked)
                            KO.KnitO_ReOpen = true;

                        if (chkKnitOrderClosed.Checked && !KO.KnitO_Closed)
                        {
                            KO.KnitO_Closed = true;
                            KO.KnitO_ClosedDate = dtpDate.Value;
                        }
                           
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                       
                        frmKnitViewRep vRep = new frmKnitViewRep(12, KO.KnitO_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                       
                    }
                }
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var KO = (TLKNI_Order)cmbKnitOrders.SelectedItem;
                if(KO != null)
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(10);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }
            }
        }

        private void cmbKnitOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
             
              using (var context = new TTI2Entities())
              {
                  var KnitO = (TLKNI_Order)oCmbo.SelectedItem;
                  if (KnitO != null)
                  {
                      KnitO = context.TLKNI_Order.Find(KnitO.KnitO_Pk);
                      if (KnitO != null)
                      {
                          if (KnitO.KnitO_Closed == true)
                              chkKnitOrderClosed.Checked = true;
                                                    
                          KnitO.KnitO_ProductionCaptured = false;

                          try
                          {
                              context.SaveChanges();
                              chkReOpenKnitOrder.Checked = false; 
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

        private void chkReOpenKnitOrder_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if (oChk != null && oChk.Checked && formloaded)
            {
                 DialogResult res = MessageBox.Show("Please confirm this transaction", "conformation required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                 if (res == DialogResult.OK)
                 {
                     using (var context = new TTI2Entities())
                     {
                         cmbKnitOrders.DataSource = context.TLKNI_Order.Where(x => x.KnitO_OrderConfirmed && x.KnitO_Closed).ToList();
                         cmbKnitOrders.ValueMember = "KnitO_Pk";
                         cmbKnitOrders.DisplayMember = "KnitO_OrderNumber";
                     }
                 }
            }
        }
    }
}
