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
using DyeHouse;

namespace TTI2_WF
{
    public partial class frmQADyeSel : Form
    {
        bool FormLoaded;

        public frmQADyeSel()
        {
            InitializeComponent();
        }

        private void frmQADyeSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            rbDyeMachinePerformance.TabStop = false;
            rbDyeProcessLoss.TabStop = false;
            rbDyeResourceEfficiency.TabStop = false;
            rbNCRResults.TabStop = false;
            rbRejectedDyeBatches.TabStop = false;
            rbReprocessedDyeBatches.TabStop = false;
            rbShadeResultsAfterDrying.TabStop = false;
            rbShadeResultsAterDyeing.TabStop = false;
            rbShadeResultsAfterCompacting.TabStop = false;
            rbDyedNotFinished.TabStop = false;
            rbDyedToQuarantine.TabStop = false;
            rbQualityException.TabStop = false;
            rbFabricStockOnHAnd.TabStop = false;
            rbFabricToQuar.TabStop = false;
            rbFabricToStore.TabStop = false;

            FormLoaded = true;

        }

        private void rbRejectedDyeBatches_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmReportOpts4 Opt2 = new frmReportOpts4();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbReprocessedDyeBatches_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmReportOpts4 Opt2 = new frmReportOpts4();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbDyeMachinePerformance_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmSelDyeMachPerformance DyeMachPerformance = new frmSelDyeMachPerformance();
                    DyeMachPerformance.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbDyeResourceEfficiency_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmSelResourceEffic resourceEff = new frmSelResourceEffic();
                    resourceEff.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbDyeProcessLoss_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmDyeingProcessLoss Opt2 = new frmDyeingProcessLoss();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbShadeResultsAterDyeing_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(3);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbShadeResultsAfterDrying_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(4);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShadeResultsAfterCompacting_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(5);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbNCRResults_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmNCRSelection NCR = new frmNCRSelection();
                    NCR.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbDyedNotFinished_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked && FormLoaded)
             {
                try
                {
                    frmDyeProductionSel prodSel = new frmDyeProductionSel(3);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                
                }
             }
        }

        private void rbDyedToQuarantine_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked && FormLoaded)
             {
                 try
                 {
                     frmDyeProductionSel prodSel = new frmDyeProductionSel(1);
                     prodSel.ShowDialog(this);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
        }

        private void rbQualityException_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmQAQualityException QualException = new frmQAQualityException();
                    QualException.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbFabricStockOnHAnd_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmFabricSOHSel Opt2 = new frmFabricSOHSel();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbFabricToQuar_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmDyeProductionSel prodSel = new frmDyeProductionSel(1);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbFabricToStore_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked && FormLoaded)
             {
                 try
                 {
                     frmDyeProductionSel prodSel = new frmDyeProductionSel(2);
                     prodSel.ShowDialog(this);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
        }
    }
}
