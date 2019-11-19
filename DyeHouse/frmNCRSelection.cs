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
    public partial class frmNCRSelection : Form
    {
        DyeReportOptions repOps = null;
        bool formloaded; 
        public frmNCRSelection()
        {
            InitializeComponent();
        }

        private void frmNCRSelection_Load(object sender, EventArgs e)
        {
            repOps = new DyeReportOptions();

            formloaded = false;

            using (var context = new TTI2Entities()) 
            {
                cmboNCRNumber.DataSource = context.TLDYE_NonCompliance.ToList();
                cmboNCRNumber.ValueMember = "TLDYE_NcrPk";
                cmboNCRNumber.DisplayMember = "TLDYE_NcrNumber";
                cmboNCRNumber.SelectedValue = -1;

                cmboCause.DataSource = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Code).ToList();
                cmboCause.DisplayMember = "QDF_Description";
                cmboCause.ValueMember = "QDF_Pk";
                cmboCause.SelectedValue = -1;

                cmboRemedy.DataSource = context.TLADM_DyeRemendyCodes.OrderBy(x => x.QRC_Code).ToList();
                cmboRemedy.DisplayMember = "QRC_Description";
                cmboRemedy.ValueMember = "QRC_Pk";
                cmboRemedy.SelectedValue = -1;

                cmboColour.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColour.ValueMember = "Col_Id";
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.SelectedValue = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboDyeMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    cmboDyeMachine.ValueMember = "MD_Pk";
                    cmboDyeMachine.DisplayMember = "MD_Description";
                    cmboDyeMachine.SelectedValue = -1;

                    cmboDyeOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).OrderBy(x=>x.MachOp_Description).ToList();
                    cmboDyeOperator.ValueMember = "MachOp_Pk";
                    cmboDyeOperator.DisplayMember = "MachOp_Description";
                    cmboDyeOperator.SelectedValue = -1;
                }

                cmboProduct.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboProduct.ValueMember = "TLGreige_Id";
                cmboProduct.DisplayMember = "TLGreige_Description";
                cmboProduct.SelectedValue = -1;

                formloaded = true;

            }
            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("0", "NCR Number"));
            reportOptions.Add(new KeyValuePair<string, string>("1", "Batch Number"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Product"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Colour"));
            reportOptions.Add(new KeyValuePair<string, string>("4", "Dye Machine"));
            reportOptions.Add(new KeyValuePair<string, string>("5", "Operator"));
            reportOptions.Add(new KeyValuePair<string, string>("6", "Cause when not complying"));
            reportOptions.Add(new KeyValuePair<string, string>("7", "Remedy when not compliant"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;
        }

        void ResetCombo()
        {
            formloaded = false;

            cmboNCRNumber.SelectedValue = -1;
            cmboProduct.SelectedValue = -1;
            cmboDyeOperator.SelectedValue = -1;
            cmboDyeMachine.SelectedValue = -1;
            cmboColour.SelectedValue = -1;
            cmboRemedy.SelectedValue = -1;
            cmboCause.SelectedValue = -1;
            cmboReportOptions.SelectedValue = -1;

            formloaded = true;
        }



        private void cmboNCRNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if (oCmbo.Name == "cmboNCRNumber")
                {
                    repOps.ops3_ComboSelected = 1;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboProduct")
                {
                    repOps.ops3_ComboSelected = 2;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboColour")
                {
                    repOps.ops3_ComboSelected = 3;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboDyeMachine")
                {
                    repOps.ops3_ComboSelected = 4;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboDyeOperator")
                {
                    repOps.ops3_ComboSelected = 5;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboCause")
                {
                    repOps.ops3_ComboSelected = 6;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboRemedy")
                {
                    repOps.ops3_ComboSelected = 7;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if (cmboReportOptions.SelectedValue != null)
                {
                    repOps.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }

                repOps.fromDate = DateTime.Parse(dtpFromDate.Value.ToShortDateString());
                repOps.toDate = DateTime.Parse(dtpToDate.Value.ToShortDateString());
                repOps.toDate = repOps.toDate.AddHours(23);




                frmDyeViewReport vRep = new frmDyeViewReport(29, repOps);
                vRep.ShowDialog(this);
                repOps = new DyeReportOptions();
                ResetCombo();

            }
        }
    }
}
