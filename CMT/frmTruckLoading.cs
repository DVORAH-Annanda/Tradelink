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

namespace CMT
{
    public partial class frmTruckLoading : Form
    {
        bool formloaded;
        public frmTruckLoading()
        {
            InitializeComponent();
        }

      

        private void frmTruckLoading_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cbReprint.Checked = false;

                cmboCurrentPI.Text = string.Empty;
                cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x=>!x.CMTPI_Closed).ToList();
                cmboCurrentPI.ValueMember = "CMTPI_Pk";
                cmboCurrentPI.DisplayMember = "CMTPI_Number";
                cmboCurrentPI.SelectedValue = -1;
            }
            formloaded = true;
        }

     

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLCMT_PanelIssue)cmboCurrentPI.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("please select a panel issue from the drop down");
                    return;
                }

                frmCMTViewRep vRep = new frmCMTViewRep(1, selected.CMTPI_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();

                DialogResult Res = MessageBox.Show("Do you wish to reprint the Cut Sheet Summary Report", "Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Res == DialogResult.Yes)
                {
                    Cutting.frmCutViewRep vRepx = new Cutting.frmCutViewRep(12, selected.CMTPI_Pk);
                    h = Screen.PrimaryScreen.WorkingArea.Height;
                    w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRepx.ClientSize = new Size(w, h);
                    vRepx.ShowDialog(); 
                }

                /*
                Res = MessageBox.Show("Do you wish to reprint the CMT Panel Issue", "Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Res == DialogResult.Yes)
                {
                    Cutting.frmCutViewRep vRepx = new Cutting.frmCutViewRep(13, selected.CMTPI_Pk);
                    h = Screen.PrimaryScreen.WorkingArea.Height;
                    w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRepx.ClientSize = new Size(w, h);
                    vRepx.ShowDialog();
                }*/ 
                

                frmTruckLoading_Load(this, null);
            }
        }

        private void cbReprint_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ocbBox = (CheckBox)sender;
            if (ocbBox != null && ocbBox.Checked)
            {
                formloaded = false;
                using (var context = new TTI2Entities())
                {
                    cmboCurrentPI.DataSource = null;
                    cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Closed).ToList();
                    cmboCurrentPI.ValueMember = "CMTPI_Pk";
                    cmboCurrentPI.DisplayMember = "CMTPI_Number";
                    cmboCurrentPI.SelectedValue = -1;
                }
                formloaded = true;
            }
            else if(formloaded)
            {
                formloaded = false;
                using (var context = new TTI2Entities())
                {
                    cmboCurrentPI.DataSource = null;
                    cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Closed).ToList();
                    cmboCurrentPI.ValueMember = "CMTPI_Pk";
                    cmboCurrentPI.DisplayMember = "CMTPI_Number";
                    cmboCurrentPI.SelectedValue = -1;
                }
                formloaded = true;
            }
        }


    }
}
