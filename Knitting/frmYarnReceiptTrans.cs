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

namespace Knitting
{
    public partial class frmYarnReceiptTrans : Form
    {
        bool FormLoaded;
        Knitting.KnitRepository repo;
        
        public frmYarnReceiptTrans()
        {
            InitializeComponent();
        }

        private void frmYarnReceiptTrans_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            repo = new KnitRepository();
            FormLoaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                KnitReportOptions RepOpts = new KnitReportOptions();

                DateTime From = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime To = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                To = To.AddHours(23);

                RepOpts.ThirdParty = chkThirdPTrans.Checked;
                RepOpts.FromDate = From;
                RepOpts.ToDate = To;

                frmKnitViewRep vRep = new frmKnitViewRep(32, RepOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);


            }

        }
    }
}
