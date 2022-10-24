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
namespace CMT
{
    public partial class frmBFASel : Form
    {
        bool formloaded;
        protected readonly TTI2Entities _context;

        public frmBFASel()
        {
            InitializeComponent();
            _context = new TTI2Entities();

        }

        private void frmBFASel_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            txtCutSheet.Select();
                        

            formloaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var CsSelected = _context.TLCUT_CutSheet.FirstOrDefault(x => x.TLCutSH_No == txtCutSheet.Text);
                if(CsSelected == null)
                {
                    MessageBox.Show("CutSheet Not Found");
                    return;

                }
                frmCMTViewRep vRep = new frmCMTViewRep(5, CsSelected.TLCutSH_Pk);
                //frmCMTViewRep vRep = new frmCMTViewRep(5, 73677);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                txtCutSheet.Text = string.Empty;

            }
        }
    }
}
