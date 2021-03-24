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
    public partial class frmLineFeederInput : Form
    {
        bool FormLoaded;

        public frmLineFeederInput()
        {
            InitializeComponent();
        }

        private void frmLineFeederInput_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Existing = (from li in context.TLCMT_LineIssue
                                join cs in context.TLCUT_CutSheet
                                on li.TLCMTLI_CutSheet_FK equals cs.TLCutSH_Pk
                                where !li.TLCMTLI_IssuedToLine && !li.TLCMTLI_WorkCompleted
                                orderby cs.TLCutSH_No
                                select cs).ToList();

                cmboCutSheet.DataSource = Existing;
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;
            }
            FormLoaded = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var RepOpts = new CMTReportOptions();
            var CS = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
            if (CS != null)
            {
                var oBtn = (Button)sender;
                if (oBtn != null && FormLoaded)
                {
                    RepOpts.ReportTitle = CS.TLCutSH_No;
                    RepOpts.SLFCutSheetFK = CS.TLCutSH_Pk;
                    frmCMTViewRep vRep = new frmCMTViewRep(23, RepOpts);
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
            else
                MessageBox.Show("Please select a custsheet from the drop down box");
        }
    }
}
