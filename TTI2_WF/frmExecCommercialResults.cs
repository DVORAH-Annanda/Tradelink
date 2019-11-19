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
    public partial class frmExecCommercialResults : Form
    {
        public frmExecCommercialResults()
        {
            InitializeComponent();
        }

        private void frmExecCommercialResults_Load(object sender, EventArgs e)
        {
            rbOrderAgeing.TabStop = false;
            rbPickedNotDelivered.TabStop = false;
            rbSalesByCustomer.TabStop = false;
            rbSalesByStyle.TabStop = false;
        }

        private void rbSalesByStyle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                using (CustomerServices.frmSelSalesByPeriod SalesByPeriod = new CustomerServices.frmSelSalesByPeriod(true))
                {
                    DialogResult dr = SalesByPeriod.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void rbSalesByCustomer_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked)
             {
                 try
                 {
                     using (CustomerServices.frmSelSalesByPeriod SalesByPeriod = new CustomerServices.frmSelSalesByPeriod(false))
                     {
                         DialogResult dr = SalesByPeriod.ShowDialog(this);
                         if (dr == DialogResult.OK)
                         {

                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
        }

        private void rbPickedNotDelivered_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    CustomerServices.frmOutStandingPL outPl = new CustomerServices.frmOutStandingPL();
                    outPl.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbOrderAgeing_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                using (CustomerServices.frmSelOutStandingOrders OrderOutStanding = new CustomerServices.frmSelOutStandingOrders())
                {
                    DialogResult dr = OrderOutStanding.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
        }
    }
}
