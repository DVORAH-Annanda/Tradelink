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
    public partial class frmReportOpts : Form
    {
        public frmReportOpts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if (!rbAllCustomers.Checked)
                {
                    var selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                    if (selected == null)
                    {
                        MessageBox.Show("Please select a customer record");
                        return;
                    }
                }

                            

                DyeReportOptions opts = new DyeReportOptions();
                opts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                opts.toDate =  Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                opts.toDate = opts.toDate.AddHours(23.00);

                if(rbAllCustomers.Checked)
                    opts.AllCustomersSelected = true;
                else
                {
                    var select = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                    opts.CustomerNumberSelected = select.Cust_Pk;
                    opts.CustomerSelected = true;
                }

                if (rbDyeBatchPending.Checked)
                    opts.DBPending = true;
                else
                    opts.DBWIP = true;

                if(rbBatchNumber.Checked)
                    opts.SortByBatchNumber = true;
                if(rbByQuality.Checked)
                    opts.SortByQuality = true;
                if(rbSortByDate.Checked)
                    opts.SortByDate = true;

                frmDyeViewReport vRep = new frmDyeViewReport(12, opts);
                vRep.ShowDialog(this);

                rbAllCustomers.Checked = false;

            }
        }

        private void frmReportOpts_Load(object sender, EventArgs e)
        {
            rbSortByDate.Checked = true;
            rbDyeBatchPending.Checked = true;

            using (var context = new TTI2Entities()) 
            {
                var Depart = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Depart != null)
                {
                    cmboCustomer.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                    cmboCustomer.ValueMember = "Cust_Pk";
                    cmboCustomer.DisplayMember = "Cust_Description";
                    cmboCustomer.SelectedValue = -1;
                }
            }
        }

        private void rbAllCustomers_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null)
            {
                if(rbAllCustomers.Checked)
                    rbAllCustomers.Checked = false;
            }
        }
    }
}
