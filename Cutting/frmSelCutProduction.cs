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

namespace Cutting
{
    public partial class frmSelCutProduction : Form
    {
        Util core;
        int RepSortOption;
        bool formloaded;

        public frmSelCutProduction()
        {
            InitializeComponent();
        }

        private void frmSelCutProduction_Load(object sender, EventArgs e)
        {
            formloaded = false;
            core = new Util();
            RepSortOption = 1;

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Machine Code"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Style"));
            reportOptions.Add(new KeyValuePair<int, string>(4, "Quality"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            formloaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    CutReportOptions repOptions = new CutReportOptions();
                    if (chkQAReport.Checked)
                        repOptions.QAReport = true;

                    repOptions.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    repOptions.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    repOptions.toDate = repOptions.toDate.AddHours(23);
                    repOptions.C4SortOption = RepSortOption;

                    frmCutViewRep vRep = new frmCutViewRep(5, repOptions);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    repOptions = new CutReportOptions();
                }
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                RepSortOption = Convert.ToInt32(oCmbo.SelectedValue);
            }
        }
    }
}
