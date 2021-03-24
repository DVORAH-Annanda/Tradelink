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
    public partial class frmK05ReportSel : Form
    {
        public frmK05ReportSel()
        {
            InitializeComponent();
            rbKnitOrder.Checked = true;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                YarnReportOptions repOts = new YarnReportOptions();
                repOts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOts.toDate = repOts.toDate.AddHours(23.59);
                if (rbKnitOrder.Checked)
                    repOts.KnitOrder = true;
                else if (rbKnitMachime.Checked)
                    repOts.KnitMachines = true;
                else if (rbGreigeProduct.Checked)
                    repOts.GreigeProduct = true;
                else if (rbProcesdLoss.Checked)
                    repOts.ProcessLoss = true;
                else if (rbYarnTex.Checked)
                    repOts.YarnTex = true;
                else if (rbYarnType.Checked)
                    repOts.YarnType = true;


                frmKnitViewRep vRep = new frmKnitViewRep(18, repOts);
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
