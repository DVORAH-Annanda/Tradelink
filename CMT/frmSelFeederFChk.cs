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
    public partial class frmSelFeederFChk : Form
    {
        bool formloaded;
        int LineFSort;
        CMTReportOptions RepOpts;

        public frmSelFeederFChk()
        {
            InitializeComponent();
        }

        private void frmSelFeederFChk_Load(object sender, EventArgs e)
        {
            formloaded = false;
            LineFSort = 0;

            RepOpts = new CMTReportOptions();

            using (var context = new TTI2Entities())
            {
               

                cmboFacility.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboFacility.ValueMember = "Dep_Id";
                cmboFacility.DisplayMember = "Dep_Description";
                cmboFacility.SelectedValue = -1;

                cmboLines.DataSource = context.TLCMT_FactConfig.ToList();
                cmboLines.ValueMember = "TLCMTCFG_Pk";
                cmboLines.DisplayMember = "TLCMTCFG_Description";
                cmboLines.SelectedValue = -1;

                cmboSupervisor.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Inspector && !x.MachOp_Discontinued).ToList();
                cmboSupervisor.ValueMember = "MachOp_Pk";
                cmboSupervisor.DisplayMember = "MachOp_Description";
                cmboSupervisor.SelectedValue = -1;

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

                cmboStyles.DataSource = context.TLADM_Styles.ToList();
                cmboStyles.ValueMember = "Sty_Id";
                cmboStyles.DisplayMember = "Sty_Description";
                cmboStyles.SelectedValue = -1;

                var reportOptions = new BindingList<KeyValuePair<int, string>>();
                reportOptions.Add(new KeyValuePair<int, string>(1, "CMT, Line"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "CMT, Supervisor"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "CMT, Colour"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "CMT, Style"));
       
                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;       

            }

            formloaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                RepOpts.fromDate = Convert.ToDateTime(dtpfromDate.Value.ToShortDateString());
                RepOpts.toDate = Convert.ToDateTime(dtptoDate.Value.ToShortDateString());
                RepOpts.toDate = RepOpts.toDate.AddHours(23);
                RepOpts.ReportTitle = "Line Feeder Quality CheckList"; 
                frmCMTViewRep vRep = new frmCMTViewRep(17, RepOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                
                RepOpts = new CMTReportOptions();
                formloaded = false;
                cmboReportOptions.SelectedIndex = -1;
                cmboCutSheet.SelectedValue = -1;
                cmboSupervisor.SelectedValue = -1;
                cmboLines.SelectedValue = -1;
                cmboFacility.SelectedValue = -1;
                cmboStyles.SelectedIndex = -1;

                formloaded = true;
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                RepOpts.SLFReportOption = (int)oCmbo.SelectedValue;
            }
        }

        private void Selected_IndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
               

                if (oCmbo.Name == "cmboFacility")
                {
                    RepOpts.SLFCmt = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboLines")
                {
                    RepOpts.SLFCmtLine = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboSupervisor")
                {
                    RepOpts.SLFCmtSupervisor = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboCutSheet")
                {
                    RepOpts.SLFCmtCutSheet = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboStyles")
                {
                    RepOpts.SLFCmtStyle = (int)oCmbo.SelectedValue;
                }
            }
        }
    }
}
