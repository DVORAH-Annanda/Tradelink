using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMT;
using Utilities;

namespace TTI2_WF
{
    public partial class frmQACMTSel : Form
    {
        bool FormLoaded;

        public frmQACMTSel()
        {
            InitializeComponent();
        }

        private void frmQACMTSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            rbCompltedeWork.TabStop = false;
            rbCutSheetsOnHold.TabStop = false;
            rbNCRDetails.TabStop = false;
            rbNonCompliance.TabStop = false;
            rbProdByPeriod.TabStop = false;

            FormLoaded = true;
        }

        private void rbCutSheetsOnHold_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                CMTReportOptions repOpts = new CMTReportOptions();
                CMTQueryParameters QueryParms = new CMTQueryParameters();
                frmCMTViewRep vRep = new frmCMTViewRep(24, QueryParms, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void rbNonCompliance_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show("Work in Progress");
                }

                 
            }
        }

        private void rbNCRDetails_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                // NCR Details By Month
                try
                {
                    frmNCRByMonth NcrByMnth = new frmNCRByMonth();
                    NcrByMnth.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbProdByPeriod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    CMT.frmSelectProduction SelectProduction = new CMT.frmSelectProduction();
                    SelectProduction.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbCompltedeWork_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {

                try
                {
                    using (frmCMTFinishedWAnalysis CMTView = new frmCMTFinishedWAnalysis())
                    {
                        DialogResult dr = CMTView.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        

       
    }
}
