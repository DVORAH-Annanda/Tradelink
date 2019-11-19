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
    public partial class frmReportOpts4 : Form
    {
        DyeReportOptions repOps = null;
        bool formloaded = false; 

        public frmReportOpts4()
        {
            InitializeComponent();
        }

        private void frmReportOpts4_Load(object sender, EventArgs e)
        {
            repOps = new DyeReportOptions();
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                try
                {
                    cmboBatches.DataSource = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_SequenceNo != 0).ToList() ;
                    cmboBatches.DisplayMember = "TLDYET_BatchNo";
                    cmboBatches.ValueMember = "TLDYET_Pk";
                    cmboBatches.SelectedValue = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if(Dept != null)
                {
                    cmboDyeMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboDyeMachine.ValueMember = "MD_Pk";
                    cmboDyeMachine.DisplayMember = "MD_Description";
                    cmboDyeMachine.SelectedValue = -1;

                }

                cmboFaultCode.DataSource = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Description).ToList();
                cmboFaultCode.ValueMember = "QDF_Pk";
                cmboFaultCode.DisplayMember = "QDF_Description";
                cmboFaultCode.SelectedValue = -1;

                cmboRemedy.DataSource = context.TLADM_DyeRemendyCodes.OrderBy(x => x.QRC_Description).ToList();
                cmboRemedy.ValueMember = "QRC_Pk";
                cmboRemedy.DisplayMember = "QRC_Description";
                cmboRemedy.SelectedValue = -1;

                cmboQuality.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboQuality.ValueMember = "TLGreige_Id";
                cmboQuality.DisplayMember = "TLGreige_Description";
                cmboQuality.SelectedValue = -1;

                cmboColour.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.ValueMember = "Col_Id";
                cmboColour.SelectedValue = -1;
                
                var reportOptions = new BindingList<KeyValuePair<string, string>>();
                reportOptions.Add(new KeyValuePair<string, string>("0", "Batch Number"));
                reportOptions.Add(new KeyValuePair<string, string>("1", "Dye Machines"));
                reportOptions.Add(new KeyValuePair<string, string>("2", "Fault Code"));
                reportOptions.Add(new KeyValuePair<string, string>("3", "Remedy Code"));
                reportOptions.Add(new KeyValuePair<string, string>("4", "Quality"));
                reportOptions.Add(new KeyValuePair<string, string>("5", "Colour"));
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
            if (oBtn != null)
            {
                if (cmboReportOptions.SelectedValue != null)
                {
                    repOps.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }
                else
                    repOps.DO_OptionSelected = 0;



                frmDyeViewReport vRep = new frmDyeViewReport(21, repOps);
                vRep.ShowDialog(this);

            }
        }

        private void cmboBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if (oCmbo.Name == "cmboBatches")
                {
                    repOps.ops3_ComboSelected = 0;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboDyeMachine")
                {
                    repOps.ops3_ComboSelected = 1;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboOperators")
                {
                    repOps.ops3_ComboSelected = 2;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboFaultCode")
                {
                    repOps.ops3_ComboSelected = 3;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboRemedy")
                {
                    repOps.ops3_ComboSelected = 4;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboQuality")
                {
                    repOps.ops3_ComboSelected = 5;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboColour")
                {
                    repOps.ops3_ComboSelected = 6;
                    repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                }
           
                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }
    }
}
