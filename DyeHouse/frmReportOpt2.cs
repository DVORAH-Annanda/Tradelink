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
    public partial class frmReportOpt2 : Form
    {
        public frmReportOpt2()
        {
            InitializeComponent();
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                DyeReportOptions repOps = new DyeReportOptions();
                if (!rbAllCustomers.Checked)
                {
                    var selected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                    if (selected == null)
                    {
                        MessageBox.Show("Please select a customer from the drop down box");
                        return;
                    }
                    else
                    {
                        repOps.CustomerSelected = true;
                        repOps.CustomerNumberSelected = ((TLADM_CustomerFile)cmboCustomers.SelectedItem).Cust_Pk;
                    }
                }
                else
                {
                    repOps.AllCustomersSelected = true;
                }

               //--------------------------------------------------------- 
                if(rbWIPDBPending.Checked)  
                    repOps.DBPending = true;
                if(rbWIPDyeing.Checked)
                    repOps.DBWIP = true;
                if (rbAll.Checked)
                    repOps.DBAll = true;
                if (rbReprocess.Checked)
                    repOps.DBReprocess = true;
                //------------------------------------------------------------

                if (cmboReportOptions.SelectedValue != null)
                {
                    repOps.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());

                }
                else
                    repOps.DO_OptionSelected = 0;


                frmDyeViewReport vRep = new frmDyeViewReport(19, repOps);
                vRep.ShowDialog(this);
                rbAllCustomers.Checked = false;
                cmboCustomers.SelectedIndex = -1;

            }
        }

        private void frmReportOpt2_Load_1(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
               
                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

                var reportOptions = new BindingList<KeyValuePair<string, string>>();
                reportOptions.Add(new KeyValuePair<string, string>("0", "Dye Order Number"));
                reportOptions.Add(new KeyValuePair<string, string>("1", "Quality"));
                reportOptions.Add(new KeyValuePair<string, string>("2", "Colour"));
                reportOptions.Add(new KeyValuePair<string, string>("3", "Customer"));
                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null)
                rbAllCustomers.Checked = false;
        }
    }
}
