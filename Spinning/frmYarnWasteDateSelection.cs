using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spinning
{
    public partial class frmYarnWasteDateSelection : Form
    {
        public frmYarnWasteDateSelection()
        {
            InitializeComponent();
            rbDisposedItems.Checked = false;
            rbSOH.Checked = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                DateTime From = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime To   = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());

                WasteSelection wasteSelect = new WasteSelection();
                wasteSelect._From = From;
                wasteSelect._To = To;

                if (rbSOH.Checked)
                    wasteSelect._IncludeSOH = true;
                else
                    wasteSelect._IncludeDispose = true;

                frmViewReport vRep = new frmViewReport(19, wasteSelect);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            }

        }
    }

    public class WasteSelection
    {
        public DateTime _From { get; set; }
        public DateTime _To { get; set; }
        public bool _IncludeDispose { get; set; }
        public bool _IncludeSOH { get; set; } 
    }
}
