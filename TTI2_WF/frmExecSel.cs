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
    public partial class frmExecSel : Form
    {
        public frmExecSel()
        {
            InitializeComponent();
        }

        private void frmExecSel_Load(object sender, EventArgs e)
        {
            rbCapUtil.TabStop = false;
            rbCommercial.TabStop = false;
            rbProdPerf.TabStop = false;
            rbProduction.TabStop = false;
            rbStockOnHand.TabStop = false;
        }

        private void rbProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    frmExecProduction ep = new frmExecProduction();
                    ep.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbCapUtil_CheckedChanged(object sender, EventArgs e)
        {
               RadioButton oRad = (RadioButton)sender;
               if (oRad != null && oRad.Checked)
               {
                   try
                   {
                       frmExecCapacity ep = new frmExecCapacity();
                       ep.ShowDialog();
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
               }
        }

        private void rbProdPerf_CheckedChanged(object sender, EventArgs e)
        {
               RadioButton oRad = (RadioButton)sender;
               if (oRad != null && oRad.Checked)
               {
                   try
                   {
                       
                       frmExecProdPerf pf = new frmExecProdPerf();
                       pf.ShowDialog();
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
               }

        }

        private void rbStockOnHand_CheckedChanged(object sender, EventArgs e)
        {
               RadioButton oRad = (RadioButton)sender;
               if (oRad != null && oRad.Checked)
               {
                   try
                   {
                       frmExecStockOnHand sh = new frmExecStockOnHand();
                       sh.ShowDialog();
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
               }

        }

        private void rbCommercial_CheckedChanged(object sender, EventArgs e)
        {
               RadioButton oRad = (RadioButton)sender;
               if (oRad != null && oRad.Checked)
               {

                   try
                   {
                       frmExecCommercialResults cr = new frmExecCommercialResults();
                       cr.ShowDialog();
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
               }
        }
    }
}
