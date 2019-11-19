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
    public partial class frmBFASel : Form
    {
        bool formloaded;

        public frmBFASel()
        {
            InitializeComponent();
        }

        private void frmBFASel_Load(object sender, EventArgs e)
        {
            formloaded = false;
            IList<TLCUT_CutSheet> CutSheet = new List<TLCUT_CutSheet>();

            using (var context = new TTI2Entities())
            {
                 var Existing =  (from li in context.TLCMT_LineIssue
                                       join cs in context.TLCUT_CutSheet
                                       on li.TLCMTLI_CutSheet_FK equals cs.TLCutSH_Pk
                                       where li.TLCMTLI_IssuedToLine && !li.TLCMTLI_WorkCompleted
                                       orderby cs.TLCutSH_No
                                       select cs).ToList();

                cmboCutSheet.DataSource = Existing;
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;
            }

            formloaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a cut sheet from the drop down list");
                    return;
                }

                frmCMTViewRep vRep = new frmCMTViewRep(5, selected.TLCutSH_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
             }
        }
    }
}
