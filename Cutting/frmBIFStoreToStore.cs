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

namespace Cutting
{
    public partial class frmBIFStoreToStore : Form
    {
        public frmBIFStoreToStore()
        {
            InitializeComponent();
        }

        private void rbCreateAPickingList_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                try
                {
                    using (FrmSelectBIF SelectBIF = new FrmSelectBIF())
                    {
                        DialogResult dr = SelectBIF.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {
                          
                        }
                    }
                    rbCreateAPickingList.Checked = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void rbCreateADeliveryNote_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                using (frmBIFTruckLoad TruckLoad = new frmBIFTruckLoad())
                {
                    DialogResult dr = TruckLoad.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                       
                    }
                }
                rbCreateADeliveryNote.Checked = false; 
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {

                
            }
        }
    }
}
