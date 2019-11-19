using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Knitting;
using Utilities;

namespace TTI2_WF
{
    public partial class frmQAKnittingSel : Form
    {
        bool FormLoaded;
        public frmQAKnittingSel()
        {
            InitializeComponent();
        }

        private void frmQAKnittingSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            rbCGrade.TabStop = false;
            rbGreigeEffic.TabStop = false;
            rbGriegeKnittedQa.TabStop = false;
            rbKnitOrdersProcess.TabStop = false;
            rbGreigeProduction.TabStop = false;

            FormLoaded = true;

        }

        private void rbKnitOrdersProcess_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmK05ReportSel ProcessLoss = new frmK05ReportSel();
                    ProcessLoss.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbGriegeKnittedQa_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmK08ReportSel QAResults = new frmK08ReportSel();
                    QAResults.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbGreigeEffic_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmK09ReportSel effSel = new frmK09ReportSel();
                    effSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbCGrade_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmK12ReportSel repSel = new frmK12ReportSel();
                    repSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbGreigeProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmK07ReportSel greigeP = new frmK07ReportSel();
                    greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
