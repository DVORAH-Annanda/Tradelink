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
            reportOptions.Add(new KeyValuePair<string, string>("0", "Customer , Quality , Batch Number"));
            reportOptions.Add(new KeyValuePair<string, string>("1", "Store, Customer, Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Store, Colour, Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Quality, Colour, Customer"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            rbFabFS.Checked = true; 
            rbStandard.Checked = true;

            FormLoad = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoad)
            {
                if (cmboReportOptions.SelectedValue != null)
                {
                    repOps.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }
                else
                    repOps.DO_OptionSelected = 0;

                if (rbFabFS.Checked)
                    repOps.FabricStore = true;
                if (rbFabQS.Checked)
                    repOps.FabricQSStore = true;
                if (rbFabRS.Checked)
                    repOps.FabricRejectStore = true;
                if (rbStandard.Checked)
                    repOps.FabricType = true;

                repOps.ShowIndvidualNo = false;

                frmDyeViewReport vRep = new frmDyeViewReport(22, repOps);
                vRep.ShowDialog(this);

                repOps = new DyeReportOptions();
               

            }
        }
    }
}
