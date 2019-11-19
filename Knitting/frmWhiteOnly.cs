using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knitting
{
   
    public partial class frmWhiteOnly : Form
    {
        public bool Is_White;

        public frmWhiteOnly()
        {
            InitializeComponent();
            Is_White = false;
        }

        private void rbWhiteYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;

            if (oRad != null && oRad.Checked)
            {
                Is_White = true;
            }
        }

        private void btnClosre_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
                this.Close();

        }
    }
}
