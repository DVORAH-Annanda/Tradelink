using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administration
{
    public partial class frmSelectProductRating : Form
    {
        public frmSelectProductRating()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null)
            {
                int RBtnSelected = 1;

                if (rbClosedRatingsOnly.Checked)
                    RBtnSelected = 2;
                else if (rbAllProductRatings.Checked)
                    RBtnSelected = 3;

                
                frmAdminViewRep vRep = new frmAdminViewRep(12, RBtnSelected);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                
            }
        }
    }
}
