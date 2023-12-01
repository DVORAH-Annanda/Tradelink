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
    

    public partial class frmDyeProductionSel : Form
    {
        Util core;
        DyeReportOptions repOps;
        bool formloaded;
        int _RepNumber;
        bool QASummary;

        public frmDyeProductionSel(int RepNumber)
        {
            InitializeComponent();
            _RepNumber = RepNumber;
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null)
            {
                if (groupBox2.Visible)
                {
                    repOps.FabricToQ = true;

                    if (radFirstTime.Checked)
                        repOps.FirstTime = true;
                    else
                        repOps.FirstTime = false;

                }
                else
                {
                    repOps.FirstTime = false;
                    repOps.FabricToStore = true;

                    if (_RepNumber == 3)
                    {
                        repOps.FabricToStore = false;
                        repOps.FabricNotFinished = true;
                    }
                }

                if(chkMonthlyProd.Checked)
                {
                    repOps.MonthlyProduction = true;
                }
                
                if (chkQASummary.Checked)
                {
                    repOps.QASummary = true;
                }

                repOps.fromDate = Convert.ToDateTime(dtFromDate.Value.ToShortDateString());
                repOps.toDate = Convert.ToDateTime(dtToDate.Value.ToShortDateString());
                repOps.toDate = repOps.toDate.AddHours(23.00);

                frmDyeViewReport vRep = new frmDyeViewReport(31, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                frmDyeProductionSel_Load(this, null);
                if(vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                repOps = new DyeReportOptions();


            }
        }

        private void frmDyeProductionSel_Load(object sender, EventArgs e)
        {
            core = new Util();
            repOps = new DyeReportOptions();
       

            using (var context = new TTI2Entities())
            {
                formloaded = false;
                
                chkMonthlyProd.Checked = false;

                if (_RepNumber == 1)
                    this.Text = "Fabric Production to Quarantine Store";
                else if (_RepNumber == 2)
                {
                    this.Text = "Fabric Production to Fabric Store";
                    groupBox2.Visible = false;
                }
                else
                {
                    this.Text = "Fabric Dyed Not Finished";
                    groupBox2.Visible = false;
                }

                radFirstTime.Checked = true;

                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;
               
                var reportOptions = new BindingList<KeyValuePair<int, string>>();
                reportOptions.Add(new KeyValuePair<int, string>(0, "Machine , Shade, Dye Machine "));
                reportOptions.Add(new KeyValuePair<int, string>(1, "Dye Machines, Shade, Colour"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Dye Machines, Quality , Colour"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Quality, Colour, Shade"));
                
                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;
                formloaded = true;
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if ((int)oCmbo.SelectedValue == 0)
                    repOps.DYEPSortOption = 0;
                else if ((int)oCmbo.SelectedValue == 1)
                    repOps.DYEPSortOption = 1;
                else if ((int)oCmbo.SelectedValue == 2)
                    repOps.DYEPSortOption = 2;
                else if ((int)oCmbo.SelectedValue == 3)
                    repOps.DYEPSortOption = 3;
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                repOps.DYEPCustNoSelected = true;
                repOps.DYEPCustNo = (int)oCmbo.SelectedValue;
            }
        }
    }
}
