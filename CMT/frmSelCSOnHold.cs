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
    public partial class frmSelCSOnHold : Form
    {
        bool Formloaded;
        CMT.CMTRepository repo;
        CMT.CMTQueryParameters QueryParms;

        public frmSelCSOnHold()
        {
            InitializeComponent();
            repo = new CMTRepository();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboDepartments.CheckStateChanged += new EventHandler(this.cmboDepartments_CheckStateChanged);
        }

        private void frmSelCSOnHold_Load(object sender, EventArgs e)
        {
            Formloaded = false;
            QueryParms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Departments = context.TLADM_Departments.Where(x=>x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                foreach (var Dep in Departments)
                {
                    cmboDepartments.Items.Add(new CMT.CheckComboBoxItem(Dep.Dep_Id, Dep.Dep_Description, false));
                }
            }
            Formloaded = true;

        }
        private void cmboDepartments_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && Formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Depts.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Depts.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Depts.Remove(value);

                }
            }
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && Formloaded)
            {
                CMTReportOptions repOpts = new CMTReportOptions();
                
                frmCMTViewRep vRep = new frmCMTViewRep(24, QueryParms, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                cmboDepartments.Items.Clear();

                frmSelCSOnHold_Load(this, null);
            }
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && Formloaded)
            {
                if(!oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }
    }
}
