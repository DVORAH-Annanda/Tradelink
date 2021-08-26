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
    public partial class frmPieceQAHistory : Form
    {
        bool FormLoaded;
        Cutting.CuttingQueryParameters QueryParms;
        Cutting.CuttingRepository repo;
        protected readonly TTI2Entities _context;
        Util core;

        public frmPieceQAHistory()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            repo = new CuttingRepository();
        }

        private void frmPieceQAHistory_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CuttingQueryParameters();


            FormLoaded = true;
        }

        private void frmPieceQAHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                QueryParms.FromDate = dtpFromDate.Value;
                QueryParms.ToDate = dtpToDate.Value;

                frmCutViewRep vRep = new frmCutViewRep(23, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }
    }
}
