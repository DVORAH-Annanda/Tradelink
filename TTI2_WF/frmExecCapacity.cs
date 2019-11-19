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
    public partial class frmExecCapacity : Form
    {
        public frmExecCapacity()
        {
            InitializeComponent();
        }

        private void frmExecCapacity_Load(object sender, EventArgs e)
        {
            rbCapKnitting.TabStop = false;
            rbCapSpinning.TabStop = false;
           
        }

        private void rbCapSpinning_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    Spinning.frmNSISelection nsiSel = new Spinning.frmNSISelection(false);
                    nsiSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void rbCapKnitting_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked)
             {
                 try
                 {
                     Knitting.frmK09ReportSel effSel = new Knitting.frmK09ReportSel();
                     effSel.ShowDialog(this);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
        }
    }
}
