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
    public partial class frmShadeAfterCompacting : Form
    {
        DyeReportOptions repOps = null;
        bool formloaded; 

        public frmShadeAfterCompacting()
        {
            InitializeComponent();
        }

        private void frmShadeAfterCompacting_Load(object sender, EventArgs e)
        {
            repOps = new DyeReportOptions();

            formloaded = false;
            using (var context = new TTI2Entities())
            {
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

                    cmboDyeOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id).ToList();
                    cmboDyeOperator.ValueMember = "MachOp_Pk";
                    cmboDyeOperator.DisplayMember = "MachOp_Description";
                    cmboDyeOperator.SelectedValue = -1;
                }

                cmboQuality.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboQuality.ValueMember = "TLGreige_Id";
                cmboQuality.DisplayMember = "TLGreige_Description";
                cmboQuality.SelectedValue = -1;
            }

            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("0", "Batch Number"));
            reportOptions.Add(new KeyValuePair<string, string>("1", "Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Colour"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Knit Machine"));
            reportOptions.Add(new KeyValuePair<string, string>("4", "Yarn Order"));
            reportOptions.Add(new KeyValuePair<string, string>("5", "Dye Machine"));
            reportOptions.Add(new KeyValuePair<string, string>("6", "Dye Operator"));
            reportOptions.Add(new KeyValuePair<string, string>("7", "Measured fabric weight(mass)"));
            reportOptions.Add(new KeyValuePair<string, string>("8", "Measured length shrinkage"));
            reportOptions.Add(new KeyValuePair<string, string>("9", "Measured width shrinkage"));
            reportOptions.Add(new KeyValuePair<string, string>("10", "Result Type"));
            reportOptions.Add(new KeyValuePair<string, string>("11", "Cause when not complying"));
            reportOptions.Add(new KeyValuePair<string, string>("12", "remedy when not compliant"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            formloaded = true;
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
                else
                    repOps.DO_OptionSelected = 0;


                frmDyeViewReport vRep = new frmDyeViewReport(29, repOps);
                vRep.ShowDialog(this);

            }
        }

        private void cmboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if (oCmbo.Name == "cmboQuality")
                {
                    repOps.ops3_ComboSelected = 0;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboColour")
                {
                    repOps.ops3_ComboSelected = 1;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboDyeMachine")
                {
                    repOps.ops3_ComboSelected = 2;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboDyeOperator")
                {
                    repOps.ops3_ComboSelected = 3;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboCause")
                {
                    repOps.ops3_ComboSelected = 4;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboRemedy")
                {
                    repOps.ops3_ComboSelected = 5;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }

            }
        }
    }
}
