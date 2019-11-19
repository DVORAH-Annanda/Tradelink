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
    public partial class frmDyeProdSel : Form
    {
        Util core;
        DyeReportOptions repOpts;
        bool formloaded;

        public frmDyeProdSel()
        {
            InitializeComponent();
        }


        private void frmDyeProdSel_Load(object sender, EventArgs e)
        {
            formloaded = false;
            repOpts = new DyeReportOptions();
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.SelectedValue = -1;
            }

            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("0", "Machines , Shade, Quality "));
            reportOptions.Add(new KeyValuePair<string, string>("1", "Machines, Shade, Colour"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Machines, Quality , Colour"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Quality, Colour, Shade"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;
            formloaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if (rbAll.Checked)
                    repOpts.DYEPFirstT = true;
                else if (rbReprocess.Checked)
                    repOpts.DYEPReprocessed = true;
                else if (rbAll.Checked)
                    repOpts.DYEPAll = true;
             
                repOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOpts.toDate = repOpts.toDate.AddHours(23.00);

                frmDyeViewReport viewRep = new frmDyeViewReport(31, repOpts);
                viewRep.ShowDialog(this);
            }
        }

       

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                repOpts.DYEPSort = Convert.ToInt32(oCmbo.SelectedValue.ToString());
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                repOpts.DYEPCustnoSelected = (int)oCmbo.SelectedValue; 
            }
        }
    }
}
