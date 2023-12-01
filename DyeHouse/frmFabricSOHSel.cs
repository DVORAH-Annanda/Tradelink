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

namespace DyeHouse
{
    public partial class frmFabricSOHSel : Form
    {

        DyeReportOptions repOps = null;
        bool FormLoad;
        public frmFabricSOHSel()
        {
            InitializeComponent();
        }

        private void frmFabricSOHSel_Load(object sender, EventArgs e)
        {
            FormLoad = false;
            repOps = new DyeReportOptions();

            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("0", "Batch Number"));
            /*reportOptions.Add(new KeyValuePair<string, string>("1", "Store, Customer, Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Store, Colour, Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Quality, Colour, Customer"));*/
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.FirstOrDefault(x => x.Dep_ShortCode == "DYE");
                if (!Object.Equals(Depts, null))
                {
                    cmboWhseStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Depts.Dep_Id && !x.WhStore_DyeKitchen).ToList();
                    cmboWhseStore.DisplayMember = "WhStore_Description";
                    cmboWhseStore.ValueMember = "WhStore_Id";
                    cmboWhseStore.SelectedValue = -1;

                }
            }
            rbStandard.Checked = true;
            rbPenNotSoldNotDeliv.Checked = false;
            rbPenSoldNotDeliv.Checked = false;
            FormLoad = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoad)
            {

                var SelectedItem = (TLADM_WhseStore)cmboWhseStore.SelectedItem;

                if (SelectedItem == null)
                {
                    MessageBox.Show("Please select a store from the drop down list provided");
                    return;
                }

                if (SelectedItem.WhStore_Code.Contains("FS"))
                {
                    repOps.FabricStore = true;
                }
                else if (SelectedItem.WhStore_Code.Contains("FQS"))
                {
                    repOps.FabricQSStore = true;
                }
                else if (SelectedItem.WhStore_Code.Contains("FRJS"))
                {
                    repOps.FabricRejectStore = true;
                }
                else
                {
                    MessageBox.Show("Please select either Fabric store, Fabric Quarantine Store or Reject Store");
                    return;
                }

                if (rbStandard.Checked)
                    repOps.FabricType = true;


                repOps.FabricStoreSelected = (int)cmboWhseStore.SelectedValue;

                if (cmboReportOptions.SelectedValue != null)
                {
                    repOps.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }
                else
                    repOps.DO_OptionSelected = 0;

          

                repOps.ShowIndvidualNo = false;

                if (rbStandard.Checked)
                    repOps.FabricType = (bool)rbStandard.Checked;
                else
                    repOps.FabricType = (bool)rbBoughtIn.Checked;

                repOps.FabPendingNotSoldNotDelivered = rbPenNotSoldNotDeliv.Checked;
                repOps.FabPendingSoldNotDelivered = rbPenSoldNotDeliv.Checked;
                
                frmDyeViewReport vRep = new frmDyeViewReport(22, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                rbPenNotSoldNotDeliv.Checked = false;
                rbPenSoldNotDeliv.Checked = false;
                
                repOps = new DyeReportOptions();
               

            }
        }
    }
}
