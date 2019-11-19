using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spinning;

namespace TTI2_WF
{
    public partial class frmQASpinningSel : Form
    {
        bool FormLoaded;
        public frmQASpinningSel()
        {
            InitializeComponent();
        }

        private void frmQASpinningSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            rbCapacityUtil.TabStop = false;
            rbConsumption.TabStop = false;
            rbInspection.TabStop = false;
            rbYarnProduction.TabStop = false;

            FormLoaded = true;


        }

        private void rbInspection_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                 try
                 {
                        frmQAReportSelection repsel = new frmQAReportSelection();
                        repsel.ShowDialog(this);
                 }
                 catch (Exception ex)
                 {
                        MessageBox.Show(ex.Message);
                 }
     
            }
        }

        private void rbCapacityUtil_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmNSISelection nsiSel = new frmNSISelection(false);
                    nsiSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbConsumption_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                try
                {
                    frmNSISelection nsiSel = new frmNSISelection(true);
                    nsiSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbYarnProduction_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked && FormLoaded)
             {
                 try
                 {
                     frmYarnProdSel ysel = new frmYarnProdSel();
                     ysel.ShowDialog(this);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
        }
    }
}
