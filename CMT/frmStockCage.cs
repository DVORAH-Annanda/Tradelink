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
    public partial class frmStockCage : Form
    {
        bool formloaded;
        int ReportSeq;
        
        public frmStockCage()
        {
            InitializeComponent();
            this.Text = "CMT Stock In Despatch Cage";
        
        }

        private void frmStockCage_Load(object sender, EventArgs e)
        {
            formloaded = false;
            ReportSeq = 0;

            using (var context = new TTI2Entities())
            {
                 cmboDepartments.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                 cmboDepartments.ValueMember = "Dep_Id";
                 cmboDepartments.DisplayMember = "Dep_Description";
                 cmboDepartments.SelectedValue = -1;
            }

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "By cut sheet"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "By box number"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "By customer"));
            reportOptions.Add(new KeyValuePair<int, string>(4, "By style"));
            reportOptions.Add(new KeyValuePair<int, string>(5, "By size"));
            reportOptions.Add(new KeyValuePair<int, string>(6, "By colour"));

            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;       
            
            formloaded = true;

        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                ReportSeq = (int)oCmbo.SelectedValue;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                CMTReportOptions repOps = new CMTReportOptions();
                var CMT = (TLADM_Departments)cmboDepartments.SelectedItem;
                if (CMT != null)
                    repOps.CMT = CMT.Dep_Id;

                repOps.SortSequence = ReportSeq;
                frmCMTViewRep vRep = new frmCMTViewRep(11, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
