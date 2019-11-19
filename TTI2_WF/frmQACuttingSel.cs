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
  

    public partial class frmQACuttingSel : Form
    {
        bool FormLoaded;

        public frmQACuttingSel()
        {
            InitializeComponent();
        }

        private void frmQACuttingSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
         
            rbBerrieBe.TabStop = false;
            rbCutProduction.TabStop  = false;
            rbCutWIP.TabStop = false; ;
            rbQAResults.TabStop = false;
            FormLoaded = true;
        }

        private void rbCutProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
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

        private void rbCutWIP_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
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

        private void rbQAResults_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    try
                    {
                        Cutting.frmQaReportSelection wipCutting = new Cutting.frmQaReportSelection();
                        wipCutting.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void rbBerrieBe_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    try
                    {
                        Cutting.FrmBierriebiSel  wipCutting = new Cutting.FrmBierriebiSel();
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
}
