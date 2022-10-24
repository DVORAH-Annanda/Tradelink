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


namespace DyeHouse
{
    public partial class FrmFabricSaleInstr : Form
    {
        public bool rbPConfirmed;
        public bool rbPCanceled;
        public bool rbFDespatched;
        public bool SelectionMade;

        public FrmFabricSaleInstr()
        {
           InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null)
            {
                if(rbCancelPending.Checked)
                {
                    rbPCanceled = true;
                    SelectionMade = true;
    }
                else if(rbPaymentConfirm.Checked )
                {
                    rbPConfirmed = true;
                    SelectionMade = true;
                }
                else
                {
                    rbFDespatched = true;
                    SelectionMade = true;
                }

                this.Close();
            }
        }

        private void FrmFabricSaleInstr_Load(object sender, EventArgs e)
        {
              rbPConfirmed  = false;
              rbPCanceled   = false;
              rbFDespatched = false;
              SelectionMade = false;
        }
    }
}
