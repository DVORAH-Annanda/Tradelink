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
    public partial class frmQAQualityException : Form
    {
        public frmQAQualityException()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                DyeHouse.DyeRepository repo = new DyeRepository();
                DyeHouse.DyeQueryParameters QueryParms = new DyeQueryParameters();

                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23);

                frmDyeViewReport vRep = new frmDyeViewReport(44, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            }
        }
    }
}
