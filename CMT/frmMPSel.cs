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
    public partial class frmMPSel : Form
    {
        bool formloaded;
        
        CMTQueryParameters QueryParms;
        CMTRepository repo;

        public frmMPSel()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboDepartment.CheckStateChanged += new System.EventHandler(this.cmboDepartment_CheckStateChanged);
            this.cmboStyle.CheckStateChanged += new EventHandler(this.cmboStyle_CheckStateChanged);
            this.cmboMeasurement.CheckStateChanged += new System.EventHandler(this.cmboMeasurement_CheckStateChanged);
            this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSize_CheckStateChanged);
            repo = new CMTRepository();
        }

        private void frmMPSel_Load(object sender, EventArgs e)
        {
            formloaded = false;

            QueryParms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Departments =  context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Department in Departments)
                {
                    cmboDepartment.Items.Add(new CMT.CheckComboBoxItem(Department.Dep_Id, Department.Dep_Description, false));
                }
                cmboDepartment.SelectedValue = -1;

                var MeasurementPoints = context.TLADM_CMTMeasurementPoints.OrderBy(x=>x.CMTMP_Description).ToList();
                foreach (var MeasurementPoint in MeasurementPoints)
                {
                    cmboMeasurement.Items.Add(new CMT.CheckComboBoxItem(MeasurementPoint.CMTMP_Pk, MeasurementPoint.CMTMP_Description, false));
                }
                cmboMeasurement.SelectedValue = -1;
                
                var Styles = context.TLADM_Styles.OrderBy(x=>x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new CMT.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }
                cmboStyle.SelectedIndex = -1;

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSize.Items.Add(new CMT.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
                cmboStyle.SelectedIndex = -1;

            }
            formloaded = true;

        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDepartment_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
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

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyle_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboMeasurement_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.MeasurementPoints.Add(repo.LoadMeasurePoints(item._Pk));

                }
                else
                {
                    var value = QueryParms.MeasurementPoints.Find(it => it.CMTMP_Pk == item._Pk);
                    if (value != null)
                        QueryParms.MeasurementPoints.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSize_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = QueryParms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        QueryParms.Sizes.Remove(value);

                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                CMTReportOptions repOpts = new CMTReportOptions();

                try
                {
                    repOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    repOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    repOpts.toDate = repOpts.toDate.AddHours(23);

                    frmCMTViewRep vRep = new frmCMTViewRep(16, QueryParms, repOpts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                finally
                {
                    cmboMeasurement.Items.Clear();
                    cmboDepartment.Items.Clear();
                    cmboSize.Items.Clear();
                    cmboStyle.Items.Clear();
                    frmMPSel_Load(this, null);

                }
            
                formloaded = true;
            }
        }
      
    }
}
