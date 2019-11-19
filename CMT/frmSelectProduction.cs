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
    public partial class frmSelectProduction : Form
    {
        bool FormLoaded;
        CMTRepository repo; 
        public frmSelectProduction()
        {
            InitializeComponent();
            repo = new CMTRepository();
            rbUnits.Checked = true;
            rbGradeA.Checked = true; 
        }

        private void frmSelectProduction_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            FormLoaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                CMTReportOptions repOpts = new CMTReportOptions();
                var FDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var TDate = Convert.ToDateTime(DtpToDate.Value.ToShortDateString());
                TDate = TDate.AddHours(23);
                
                repOpts.fromDate = FDate;
                repOpts.toDate = TDate;

                if (rbGradeA.Checked)
                    repOpts.GradeAOnly = true;
                else if (rbGradeB.Checked)
                    repOpts.GradeBOnly = true;
                else
                    repOpts.GradeBoth = true;

                if (rbUnits.Checked)
                    repOpts.Units = true;
                else
                    repOpts.Boxes = true;

                CMTQueryParameters QueryParms = new CMTQueryParameters();
                
                frmCMTViewRep vRep = new frmCMTViewRep(26, QueryParms, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
