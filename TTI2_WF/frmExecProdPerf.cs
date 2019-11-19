using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTI2_WF
{
    public partial class frmExecProdPerf : Form
    {
        public frmExecProdPerf()
        {
            InitializeComponent();
        }

        private void frmProdPerformance_Load(object sender, EventArgs e)
        {
            rbPPCutting.TabStop = false;
            rbPPCMT.TabStop = false;
            rbPPDyeing.TabStop = false;
            rbPPKnitting.TabStop = false;
        }

        private void rbPPKnitting_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Knitting.frmK05ReportSel ProcessLoss = new Knitting.frmK05ReportSel();
                    ProcessLoss.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbPPDyeing_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    DyeHouse.frmDyeingProcessLoss Opt2 = new DyeHouse.frmDyeingProcessLoss();
                    Opt2.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbPPCutting_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    frmCuttingSelProcess SelProcess = new frmCuttingSelProcess();
                    SelProcess.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbPPCMT_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    try
                    {
                        CMT.frmProdByPeriodSel prodByPer = new CMT.frmProdByPeriodSel(true);
                        prodByPer.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
