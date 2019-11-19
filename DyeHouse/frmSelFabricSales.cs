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
    public partial class frmSelFabricSales : Form
    {
        bool formloaded;

        public frmSelFabricSales()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DyeReportOptions repOpts = new DyeReportOptions();
                repOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOpts.toDate = repOpts.toDate.AddHours(23);
                frmDyeViewReport vRep = new frmDyeViewReport(36, repOpts);
                vRep.ShowDialog(this);
            }
        }

        private void frmSelFabricSales_Load(object sender, EventArgs e)
        {
            formloaded = false;
            formloaded = true;
        }
    }
}
