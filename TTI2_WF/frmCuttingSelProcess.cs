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
    public partial class frmCuttingSelProcess : Form
    {
        public frmCuttingSelProcess()
        {
            InitializeComponent();
            rbCutProduction.TabStop = false;
            rbRejectPanels.TabStop = false;
        }

        private void rbRejectPanels_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Cutting.frmSelRejectedPanel rejPanel = new Cutting.frmSelRejectedPanel(1);
                    rejPanel.ShowDialog(this);
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
    }
}
