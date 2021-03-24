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

namespace Knitting
{
    public partial class frmK09ReportSel : Form
    {
        public frmK09ReportSel()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            YarnReportOptions YarnOpts = new YarnReportOptions();

            YarnOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
            YarnOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
            YarnOpts.toDate = YarnOpts.toDate.AddHours(23);
            if (rbShifts.Checked)
                YarnOpts.SplitByShift = true;

            frmKnitViewRep vRep = new frmKnitViewRep(25, YarnOpts);
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            vRep.ClientSize = new Size(w, h);
            vRep.ShowDialog(this);
            if (vRep != null)
            {
                vRep.Close();
                vRep.Dispose();

            }
        }
    }
}
