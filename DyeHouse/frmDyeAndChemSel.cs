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
    public partial class frmDyeAndChemSel : Form
    {
        public frmDyeAndChemSel()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                DyeReportOptions DOpts = new DyeReportOptions();
                DOpts.NonReceipeOnly = chkNonReceipeOnly.Checked;
                DOpts.fromDate = DateTime.Parse(dtpFromDate.Value.ToShortDateString());
                DOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                frmDyeViewReport vRep = new frmDyeViewReport(23, DOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                chkNonReceipeOnly.Checked = false;
            }
        }
    }
}
