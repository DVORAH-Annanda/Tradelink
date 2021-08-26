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
    public partial class frmGreigeAnalysis : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;
        DyeHouse.DyeQueryParameters QueryParms;
        DyeHouse.DyeRepository repo;
        public frmGreigeAnalysis()
        {
            InitializeComponent();
            _context = new TTI2Entities();
        }

        private void frmGreigeAnalysis_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new DyeHouse.DyeQueryParameters();
            FormLoaded = true;
        }

        private void frmGreigeAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                QueryParms.FromDate = dtpFromDate.Value;
                QueryParms.ToDate = dtpToDate.Value;
                
                frmDyeViewReport vRep = new frmDyeViewReport(50, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Dispose();
                }

                frmGreigeAnalysis_Load(this, null);
            }
        }
    }
}
