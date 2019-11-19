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
    public partial class frmDyeReprint : Form
    {
        public frmDyeReprint()
        {
            InitializeComponent();
        }

        private void frmDyeReprint_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                cmboTransferTickets.DataSource = context.TLDYE_DyeBatch.Where(x=>x.DYEB_Transfered && !x.DYEB_Closed).OrderBy(x=>x.DYEB_BatchNo) .ToList();
                cmboTransferTickets.DisplayMember = "DYEB_BatchNo";
                cmboTransferTickets.ValueMember = "DYEB_Pk";
                cmboTransferTickets.SelectedValue = 0;
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if (oBtn != null)
            {
                var selected = (TLDYE_DyeBatch)cmboTransferTickets.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a batch number from the drop down box");
                    return;
                }

                frmDyeViewReport vRep = new frmDyeViewReport(5, selected.DYEB_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
