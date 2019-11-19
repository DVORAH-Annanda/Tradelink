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

namespace Spinning
{
    public partial class frmSliverProductionSelection : Form
    {
        bool QASummary;

        public frmSliverProductionSelection()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            rbtnSummary.Checked = true;
            rbtnDetail.Checked = false; ;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                SliverProductionSelection sliverProductionSelection = new SliverProductionSelection();
                sliverProductionSelection.DateFrom = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                sliverProductionSelection.DateTo = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());

                if (rbtnSummary.Checked)
                    sliverProductionSelection.Summary = true;
                else
                    sliverProductionSelection.Detail = true;

                frmViewReport vRep = new frmViewReport(25, sliverProductionSelection);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }

    public class SliverProductionSelection
    {
        public SliverProductionSelection()
        {
        }

        public DateTime DateFrom { get; set;}
        public DateTime DateTo { get; set;}
        public bool Detail { get; set;}
        public bool Summary { get; set; } 
    }
}
