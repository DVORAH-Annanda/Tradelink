using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cutting
{
    public partial class frmIncompleteWasteCS : Form
    {
        public frmIncompleteWasteCS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var oBtn = sender as Button;
            if(oBtn != null)
            {
                CuttingQueryParameters parms = new CuttingQueryParameters();
                parms.FromDate = Convert.ToDateTime(dtpFromDate.Value);
                
                Cutting.frmCutViewRep vRepx = new Cutting.frmCutViewRep(25, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRepx.ClientSize = new Size(w, h);
                vRepx.ShowDialog();


            }
        }
    }
}
