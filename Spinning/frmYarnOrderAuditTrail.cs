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

    public partial class frmYarnOrderAuditTrail : Form
    {
        bool formLoaded;
        Spinning.SpinningRepository repo;
        Spinning.SpinningQueryParameters QueryParms;

        Spinning.QAYarnReportOptions YarnRepOpts;

    
        public frmYarnOrderAuditTrail()
        {
            InitializeComponent();
            repo = new SpinningRepository();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmYarnOrderAuditTrail_Load(object sender, EventArgs e)
        {
            formLoaded = false;
            using (var context = new TTI2Entities())
            {
                var yarnTypes = context.TLADM_Yarn.Where(x => !(bool)(x.YA_Discontinued)).ToList();
                foreach(var yarnType in yarnTypes)
                {
                    this.cmboYarnType.Items.Add(new Spinning.CheckComboBoxItem(yarnType.YA_Id, yarnType.YA_Description, false));
                }
            }

            using (var context = new TTI2Entities())
            {
                var yarnOrders = context.TLSPN_YarnOrder.Where(x => !(bool)(x.Yarno_Closed)).ToList();
                foreach (var yarnOrder in yarnOrders)
                {
                    this.cmboYarnOrder.Items.Add(new Spinning.CheckComboBoxItem(yarnOrder.YarnO_Pk, yarnOrder.YarnO_OrderNumber.ToString(), false));
                }
            }

            QueryParms = new SpinningQueryParameters();
    

            YarnRepOpts = new Spinning.QAYarnReportOptions();
            rbYarnOrder.Checked = true;
        
            this.cmboYarnType.CheckStateChanged += new System.EventHandler(this.cmboYarnType_CheckStateChanged);
            this.cmboYarnOrder.CheckStateChanged += new System.EventHandler(this.cmboYarnOrder_CheckStateChanged);
            formLoaded = true;
            
        }

        private void cmboYarnType_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Spinning.CheckComboBoxItem && formLoaded)
            {
                Spinning.CheckComboBoxItem item = (Spinning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.YarnTypes.Add(repo.LoadYarnTypes(item._Pk));

                }
                else
                {
                    var value = QueryParms.YarnTypes.Find(it => it.YA_Id == item._Pk);
                    if (value != null)
                        QueryParms.YarnTypes.Remove(value);

                }
            }
        }

        private void cmboYarnOrder_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Spinning.CheckComboBoxItem && formLoaded)
            {
                Spinning.CheckComboBoxItem item = (Spinning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.YarnOrders.Add(repo.LoadYarnOrders(item._Pk));

                }
                else
                {
                    var value = QueryParms.YarnOrders.Find(it => it.YarnO_Pk == item._Pk);
                    if (value != null)
                        QueryParms.YarnOrders.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oButton = (Button)sender;
            if (oButton != null && formLoaded)
            {
                if (rbYarnOrder.Checked)
                    YarnRepOpts.YarnOrderAuditTrail =  true;
                else
                    YarnRepOpts.KnitOrderAuditTrail = true;

                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23);

                frmViewReport vRep = new frmViewReport(24, QueryParms, YarnRepOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboYarnOrder.Items.Clear();
                cmboYarnType.Items.Clear();

                this.frmYarnOrderAuditTrail_Load(this, null);
 
            }
        }
    }
}
