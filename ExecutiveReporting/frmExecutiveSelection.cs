using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecutiveReporting
{
    public partial class frmExecutiveSelection : Form
    {
        public frmExecutiveSelection()
        {
            InitializeComponent();
        }

        private void chkProduction_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked)
            {
                frmExecViewRep vRep = new frmExecViewRep(1);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                oChk.Checked = false;
            }
        }
    }
}
