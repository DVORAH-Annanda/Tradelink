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
    public partial class frmDyeingProcessLoss : Form
    {
        DyeReportOptions repOps = null;
        bool formloaded = false;
        Util core;
        public frmDyeingProcessLoss()
        {
            InitializeComponent();
            core = new Util();
            txtPercentage.KeyDown += core.txtWin_KeyDownJI;
            txtPercentage.KeyPress += core.txtWin_KeyPress;
            txtPercentage.Text = "0";

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


                if (chkException.Checked)
                {
                    repOps.ExceptionOnly = true;
                    repOps.Percentage_Exception = Convert.ToInt32(txtPercentage.Text);
                }

                repOps.fromDate = DateTime.Parse(dtpFromDate.Value.ToShortDateString());
                repOps.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                frmDyeViewReport vRep = new frmDyeViewReport(24, repOps);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                repOps = new DyeReportOptions();
                
                chkException.Checked = false;

            }
        }

        private void frmDyeingProcessLoss_Load(object sender, EventArgs e)
        {
            repOps = new DyeReportOptions();
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboBatchNo.DataSource = context.TLDYE_DyeBatch.OrderBy(x => x.DYEB_BatchNo).ToList();
                cmboBatchNo.DisplayMember = "DYEB_BatchNo";
                cmboBatchNo.ValueMember = "DYEB_Pk";
                cmboBatchNo.SelectedValue = -1;

                cmboShade.DataSource = context.TLADM_FabricProduct.OrderBy(x => x.FP_Description).ToList();
                cmboShade.DisplayMember = "FP_Description";
                cmboShade.ValueMember = "FP_Id";
                cmboShade.SelectedValue = -1;

                cmboColour.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColour.ValueMember = "Col_Id";
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.SelectedValue = -1;

                var Department = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if(Department != null)
                {
                    cmboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Department.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    cmboMachines.ValueMember = "MD_Pk";
                    cmboMachines.DisplayMember = "MD_Description";
                    cmboMachines.SelectedValue = -1;
                }

                var reportOptions = new BindingList<KeyValuePair<string, string>>();
                reportOptions.Add(new KeyValuePair<string, string>("0", "Machine , Shade, Dye Machine "));
                reportOptions.Add(new KeyValuePair<string, string>("1", "Dye Machines, Shade, Colour"));
                reportOptions.Add(new KeyValuePair<string, string>("2", "Dye Machines, Quality , Colour"));
                reportOptions.Add(new KeyValuePair<string, string>("3", "Quality, Colour, Shade"));
                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;

          

            }
            formloaded = true;
        }

        private void cmboBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded)
            {
                if (oCmbo.Name == "cmboBatchNo")
                {
                    repOps.DPLBatchSel = true;
                    repOps.DPLBatchIndex = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboShade")
                {
                    repOps.DPLSByShade = true;
                    repOps.DPLShadeIndex = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboColour")
                {
                    repOps.DPLSByColour = true;
                    repOps.DPLColourIndex = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboMachines")
                {
                    repOps.DPLMachineSel = true;
                    repOps.DPLMachineIndex = (int)oCmbo.SelectedValue;
                }
            }
        }

        private void chkException_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null)
            {
                txtPercentage.Text = "5";
            }
            else
            {
                txtPercentage.Text = "0";
            }
        }
    }
}
