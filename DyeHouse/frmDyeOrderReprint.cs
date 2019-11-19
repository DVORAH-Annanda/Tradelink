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
    public partial class frmDyeOrderReprint : Form
    {
        bool DyeOrderReprint;
        public frmDyeOrderReprint(bool DOReprint)
        {
            InitializeComponent();
            DyeOrderReprint = DOReprint;

            if (DOReprint)
            {
                this.Text = "Dye Order Reprint";
            }
            else
            {
                this.Text = "Dye Batch Reprint";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;

            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    if (DyeOrderReprint)
                    {
                         var DyeOrder = context.TLDYE_DyeOrder.Where(x => x.TLDYO_DyeOrderNum == txtInput.Text).FirstOrDefault();
                         if (DyeOrder == null)
                         {
                             MessageBox.Show("Dye Order as entered not found");
                             txtInput.Text = string.Empty;
                             
                             return;
                         }

                         frmDyeViewReport vRep = new frmDyeViewReport(3, DyeOrder.TLDYO_Pk);
                         int h = Screen.PrimaryScreen.WorkingArea.Height;
                         int w = Screen.PrimaryScreen.WorkingArea.Width;
                         vRep.ClientSize = new Size(w, h);
                         vRep.ShowDialog(this);
                    }
                    else
                    {
                        var DyeBatch = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchNo == txtInput.Text).FirstOrDefault();
                        if (DyeBatch == null)
                        {
                            MessageBox.Show("Dye Batch as entered not found");
                            txtInput.Text = string.Empty;

                            return;
                        }

                      

                        frmDyeViewReport vRep = new frmDyeViewReport(4, DyeBatch.DYEB_Pk );
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                        DialogResult Result = MessageBox.Show("Would you like to print the transfer tickets", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (Result == DialogResult.Yes)
                        {
                            vRep = new frmDyeViewReport(5, DyeBatch.DYEB_Pk);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                        }
                    }
                }
            }
        }


    }
}
