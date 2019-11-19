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
    public partial class frmSelDyeMachPerformance : Form
    {
        bool formloaded;

        public frmSelDyeMachPerformance()
        {
            InitializeComponent();
        }

        private void frmSelDyeMachPerformance_Load(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Depts != null)
                {
                    cmboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == Depts.Dep_Id).ToList();
                    cmboMachines.ValueMember = "MD_Pk";
                    cmboMachines.DisplayMember = "MD_MachineCode";
                    cmboMachines.SelectedIndex = -1;

                }

                var reportOptions = new BindingList<KeyValuePair<string, string>>();
                reportOptions.Add(new KeyValuePair<string, string>("0", "Machine"));
                reportOptions.Add(new KeyValuePair<string, string>("1", "Batch Number"));
                reportOptions.Add(new KeyValuePair<string, string>("2", "Quality"));
                reportOptions.Add(new KeyValuePair<string, string>("3", "Colour"));
                reportOptions.Add(new KeyValuePair<string, string>("4", "Result"));
                reportOptions.Add(new KeyValuePair<string, string>("5", "Operator"));

                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;
            }
 
            formloaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DyeReportOptions repOptions = new DyeReportOptions();
                repOptions.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOptions.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOptions.toDate = repOptions.toDate.AddHours(23);

                var selected = (TLADM_MachineDefinitions)cmboMachines.SelectedItem;
                if (selected != null)
                   repOptions.DYEMPMachineSelected = selected.MD_Pk;
               
                if(rbFirstTime.Checked)
                   repOptions.DYEMPProdType = 1;
                else if (rbOwnReprocessing.Checked)
                   repOptions.DYEMPProdType = 2;
                else
                   repOptions.DYEMPProdType = 3;
                

                if (rbModeSummary.Checked)
                    repOptions.DYEMPReportType = 1;
                else
                    repOptions.DYEMPReportType = 2;

                if (cmboReportOptions.SelectedValue != null)
                   repOptions.DYEMPSortOptions = int.Parse(cmboReportOptions.SelectedValue.ToString());
                else
                   repOptions.DYEMPSortOptions = 1; ;

                frmDyeViewReport vRep = new frmDyeViewReport(34, repOptions);
                vRep.ShowDialog(this);

                cmboMachines.SelectedIndex = -1;
                cmboReportOptions.SelectedIndex = -1;

            }
        }
    }
}
