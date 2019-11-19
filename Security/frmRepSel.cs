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
namespace Security
{
    public partial class frmRepSel : Form
    {
        public frmRepSel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                ReportSecOptions RepSecOpts = new ReportSecOptions();
                if (rbActiveRecords.Checked)
                    RepSecOpts.ActiveRecords = true;
                else if (rbAllRecords.Checked)
                    RepSecOpts.AllRecords = true;
                else
                    RepSecOpts.DiscontinuedRecords = true;

                frmViewSecurity VRep = new frmViewSecurity(1, RepSecOpts);
                VRep.ShowDialog();
            }

        }
    }
}
