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
    public partial class frmExecProduction : Form
    {
        public frmExecProduction()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmExecProduction_Load(object sender, EventArgs e)
        {
            rbCmtProduction.TabStop = false;
            rbCutProduction.TabStop = false;
            rbKnitingGreige.TabStop = false;
            rbFabricDyed.TabStop = false;
            rbProdSpinning.TabStop = false;
        }

        private void rbProdSpinning_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Spinning.frmYarnProdSel ysel = new Spinning.frmYarnProdSel();
                    ysel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbKnitingGreige_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Knitting.frmK07ReportSel greigeP = new Knitting.frmK07ReportSel();
                    greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbFabricDyed_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    DyeHouse.frmDyeProductionSel prodSel = new DyeHouse.frmDyeProductionSel(3);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbCutProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Cutting.frmSelCutProduction wipCutting = new Cutting.frmSelCutProduction();
                    wipCutting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbCmtProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
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
        }
    }
}
