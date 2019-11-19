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

namespace CMT
{
    public partial class frmCutSheetPAnalysis : Form
    {
        public frmCutSheetPAnalysis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                CMTReportOptions CMTRepOpts = new CMTReportOptions();

                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
               //  DateTime dt = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                CMTRepOpts.fromDate = FromDate;
                CMTRepOpts.toDate = ToDate.AddHours(23);

                frmCMTViewRep vRep = new frmCMTViewRep(29, CMTRepOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
