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

namespace CustomerServices
{
    public partial class frmSelWhseToWhse : Form
    {
        public frmSelWhseToWhse()
        {
            InitializeComponent();
         
        }

        private void rbCreateAPickingList_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
             
                using (frmTransferWarehouseToWarehouse Transfer = new frmTransferWarehouseToWarehouse())
                {
                    DialogResult dr = Transfer.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

                oRad.Checked = false;
            }
        }

        private void rbCreateADeliveryNote_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                using (frmWhseToWhseDelivery delivery = new frmWhseToWhseDelivery(true))
                {
                    DialogResult dr = delivery.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

                oRad.Checked = false;
            }
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
