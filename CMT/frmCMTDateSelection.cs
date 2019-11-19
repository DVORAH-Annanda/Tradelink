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
    public partial class frmCMTDateSelection : Form
    {
        bool _Export;
        bool FormLoaded;
        public frmCMTDateSelection(bool Export)
        {
            InitializeComponent();
            _Export = Export;

            if (_Export)
            {
                label2.Visible = false;
                cmboDepartments.Visible = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;

            if (!_Export)
            {
                var Depts = (TLADM_Departments)cmboDepartments.SelectedItem;
                if (Depts == null)
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a CMT to report on");
                        return;
                    }

                }
                CMTReportOptions repOps = new CMTReportOptions();
                repOps.SLFCmt = Depts.Dep_Id;

                DateTime dt = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOps.toDate = dt.AddHours(23);
                // repOps.toDate = (DateTimedtpToDate.Value;

                frmCMTViewRep vRep = new frmCMTViewRep(18, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void frmCMTDateSelection_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                cmboDepartments.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboDepartments.DisplayMember = "Dep_Description";
                cmboDepartments.ValueMember = "Dep_Id";
                cmboDepartments.SelectedValue = -1;

            }
            FormLoaded = true;
        }
    }
}
