using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DyeHouse
{
    public partial class frmSelProductionDays : Form
    {
        DyeHouse.DyeQueryParameters QueryParms;
      

        public frmSelProductionDays()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null)
            {
                QueryParms = new DyeQueryParameters();

                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23);

                if (chkProductionResults.Checked)
                    QueryParms.CalculateProdResults = true;

                frmDyeViewReport vRep = new frmDyeViewReport(43, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
