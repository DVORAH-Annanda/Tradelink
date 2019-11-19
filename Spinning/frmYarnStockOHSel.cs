using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spinning
{
    public partial class frmYarnStockOHSel : Form
    {
        public frmYarnStockOHSel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                YarnStockOHSel reptSel = new YarnStockOHSel();
                if (rbByStore.Checked)
                    reptSel.ByStore = true;
                else if (rbTextCount.Checked)
                    reptSel.ByTexCount = true;
                else if (rbTwistFactor.Checked)
                    reptSel.ByTwistFactor = true;
                else
                    reptSel.ByYarnOrder = true;

                frmViewReport vRep = new frmViewReport(18, reptSel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void rbByStore_CheckedChanged(object sender, EventArgs e)
        {

        }

    }

    public class YarnStockOHSel
    {
        public bool ByTexCount { get; set; }
        public bool ByTwistFactor { get; set; }
        public bool ByYarnOrder { get; set; }
        public bool ByStore { get; set; } 
    }

}
