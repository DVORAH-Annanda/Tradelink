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
    public partial class frmReprintCS : Form
    {
        public frmReprintCS()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var selected = (TLCUT_CutSheet)cmboReprint.SelectedItem;
                if (selected != null)
                {
                    frmCutViewRep vRep = new frmCutViewRep(1, selected.TLCutSH_Pk);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    vRep = new frmCutViewRep(2, selected.TLCutSH_Pk);
                    h = Screen.PrimaryScreen.WorkingArea.Height;
                    w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Please select a Cut sheet from the drop down box");

                }
            }

        }

        private void frmReprintCS_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                cmboReprint.DataSource = context.TLCUT_CutSheet.Where(x => x.TLCutSH_Accepted && !x.TLCutSH_Closed).ToList();
                cmboReprint.ValueMember = "TLCutSH_Pk";
                cmboReprint.DisplayMember = "TLCutSH_No";
                cmboReprint.SelectedValue = -1;

            }
        }
    }
}
