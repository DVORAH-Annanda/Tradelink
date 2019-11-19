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
    
    public partial class frmExecStockOnHand : Form
    {
         
        public frmExecStockOnHand()
        {
            InitializeComponent();
        }

        private void frmExecStockOnHand_Load(object sender, EventArgs e)
        {
            rbSOHCMT.TabStop = false;
            rbSOHCuttingWIP.TabStop = false;
            rbSOHDyeBatches.TabStop = false;
            rbSOHDyeBatchPending.TabStop = false;
            rbSOHKnittingGreigeStock.TabStop = false;
            rbSOHPanelStore.TabStop = false;
            rbSOHRejectStock.TabStop = false;
            rbSOHSpinningCotton.TabStop = false;
            rbSOHSpinningYarnStock.TabStop = false;
            rbSOHWareHouse.TabStop = false;
            rbNegativeStock.TabStop = false;

        }

        private void rbSOHSpinningCotton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    /*
                    frmViewReport vRep = new frmViewReport(6);
                    vRep.ShowDialog(this);
                    */
                    Spinning.frmDateSelectselection datesel = new Spinning.frmDateSelectselection(2);
                    datesel.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHSpinningYarnStock_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Spinning.frmYarnStockOHSel YOH = new Spinning.frmYarnStockOHSel();
                    YOH.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHKnittingGreigeStock_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    try
                    {
                        Knitting.frmK10ReportSel repSel = new Knitting.frmK10ReportSel(true);
                        repSel.ShowDialog(this);
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

        private void rbSOHDyeBatchPending_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                try
                {
                    DyeHouse.frmReportOpt2 Opt2 = new DyeHouse.frmReportOpt2();
                    Opt2.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHDyeBatches_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                try
                {
                    DyeHouse.frmReportOpt2 Opt2 = new DyeHouse.frmReportOpt2();
                    Opt2.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHRejectStock_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                try
                {
                    DyeHouse.frmFabricSOHSel Opt2 = new DyeHouse.frmFabricSOHSel();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHCuttingWIP_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Cutting.frmSelWipCutting wipCutting = new Cutting.frmSelWipCutting();
                    wipCutting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHPanelStore_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Cutting.frmSelPanelStock selPanelStock = new Cutting.frmSelPanelStock();
                    selPanelStock.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHCMT_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    CMT.frmCMTPanelStock vRep = new CMT.frmCMTPanelStock();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbSOHWareHouse_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    CustomerServices.frmStockOnHand SOH = new CustomerServices.frmStockOnHand(1);
                    SOH.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbNegativeStock_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    CustomerServices.frmNegativeStock NegativeStock = new CustomerServices.frmNegativeStock();
                    NegativeStock.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
