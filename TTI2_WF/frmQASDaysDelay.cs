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
using ProductionPlanning;
 
namespace TTI2_WF
{
    public partial class frmQASDaysDelay : Form
    {
        bool FormLoaded;
        bool[] ReportDepts = null;
        public frmQASDaysDelay()
        {
            InitializeComponent();
        }

        private void frmQASDaysDelay_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            ReportDepts = new bool[4]{false, false, false, false} ;
            //=================================================================
            // 1st Element = DyeHouse Prep 
            // 2nd Element = Cutting 
            // 3rd Element = CMT 
            // 4TH Element = WareHouse
            //====================================================
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;

            chkCMT.Checked = false;
            chkCutting.Checked = false;
            chkDyePrep.Checked = false;
            chkWhse.Checked = false;

            FormLoaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                if (!chkCMT.Checked && !chkCutting.Checked && !chkDyePrep.Checked && !chkWhse.Checked)
                {
                    MessageBox.Show("Please select at least one Department to report on");
                    return;
                }

                if (chkDyePrep.Checked )
                    ReportDepts[0] = true;

                if (chkCutting.Checked)
                    ReportDepts[1] = true;

                if (chkCMT.Checked)
                    ReportDepts[2] = true;

                if (chkWhse.Checked)
                    ReportDepts[3] = true;

               
                ProductionPlanning.ProdQueryParameters QueryParms = new ProductionPlanning.ProdQueryParameters();
                QueryParms.QAReportingDepts = ReportDepts;
                
                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);
                
                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;


                frmPPSViewRep vRep = new frmPPSViewRep(6, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();

                frmQASDaysDelay_Load(this, null);

            }
        }
    }
}
