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
    public partial class frmK12ReportSel : Form
    {
        public frmK12ReportSel()
        {
            InitializeComponent();
            rbKnitMachine.Checked = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                Util core = new Util();

                YarnReportOptions repOps = new YarnReportOptions();

                repOps.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOps.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOps.toDate = repOps.toDate.AddHours(23);
                if (rbKnitMachine.Checked)
                    repOps.K12KnitMachine = true;
                else if (rbOperator.Checked)
                    repOps.K12Operator = true;
                else if (rbSpinning.Checked)
                    repOps.K12Spinning = true;
                else
                    repOps.K12YarnOrder = true;

                frmKnitViewRep vRep = new frmKnitViewRep(21, repOps);
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
}
