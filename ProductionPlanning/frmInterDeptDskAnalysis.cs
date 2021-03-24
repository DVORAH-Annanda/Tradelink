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

namespace ProductionPlanning
{
    public partial class frmInterDeptDskAnalysis : Form
    {
        bool FormLoad;
        ProdQueryParameters QueryParms;
        PPSRepository repo;
        public frmInterDeptDskAnalysis()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoad)
            {
                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                frmPPSViewRep vRep = new frmPPSViewRep(8, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);


               
            }

        }

        private void frmInterDeptDskAnalysis_Load(object sender, EventArgs e)
        {
            FormLoad = false;
            QueryParms = new ProdQueryParameters();
            FormLoad = true;
        }
    }
}
