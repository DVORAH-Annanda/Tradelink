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

            FormLoad = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoad)
            {
                if((int)cmboWhseStore.SelectedValue <= 0)
                {
                    MessageBox.Show("Please select a store from the drop down list provided");
                    return;
                }

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

                frmDyeViewReport vRep = new frmDyeViewReport(22, repOps);
                vRep.ShowDialog(this);

                repOps = new DyeReportOptions();
               

            }
        }
    }
}
