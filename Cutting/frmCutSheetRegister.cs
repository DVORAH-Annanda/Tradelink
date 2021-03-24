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

namespace Cutting
{
    public partial class frmCutSheetRegister : Form
    {
        CuttingRepository repo;
        CuttingQueryParameters parms;
        bool formloaded;
        public frmCutSheetRegister()
        {
            InitializeComponent();
            repo = new CuttingRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboDepartments.CheckStateChanged += new System.EventHandler(this.cmboDepartments_CheckStateChanged);
            
        }

        private void frmCutSheetRegister_Load(object sender, EventArgs e)
        {
            formloaded = false;
            parms = new CuttingQueryParameters();
            using (var context = new TTI2Entities())
            {
                var Departments = context.TLADM_Departments.Where(x => x.Dep_IsCut).OrderBy(x => x.Dep_Description).ToList();
                foreach (var Department in Departments)
                {
                    cmboDepartments.Items.Add(new Cutting.CheckComboBoxItem(Department.Dep_Id, Department.Dep_Description, false));
                }
            }
            formloaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDepartments_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Departments.Add(repo.LoadDepartments(item._Pk));

                }
                else
                {
                    var value = parms.Departments.Find(it => it.Dep_Id  == item._Pk);
                    if (value != null)
                        parms.Departments.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && formloaded)
            {
                CutReportOptions repOptions = new CutReportOptions();
                parms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                parms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                parms.ToDate = parms.ToDate.AddHours(23);
                
                frmCutViewRep vRep = new frmCutViewRep(15, repOptions, parms);
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
    }
}
