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
    public partial class frmProductionDays : Form
    {
        public frmProductionDays()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null)
            {
                DateTime FDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime TDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                TDate = TDate.AddHours(23);

                 DyeReportOptions repOps = new DyeReportOptions();
                 repOps.fromDate = FDate;
                 repOps.toDate   = TDate;

                 if (chkProdResults.Checked)
                     repOps.ProductionResults = true;

                frmDyeViewReport vRep = new frmDyeViewReport(43, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
