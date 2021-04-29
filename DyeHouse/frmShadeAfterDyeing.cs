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
    public partial class frmShadeAfterDyeing : Form
    {
        DyeReportOptions repOps = null;
        bool formloaded;
        int _Stage;

        DyeRepository repo;
        DyeQueryParameters QueryParms;

        //Stage 3 Colour Matching after Dyeing
        //Stage 4 Stability Check after Drying;
        //Stage 5 Results Check after Compacting;
 
        public frmShadeAfterDyeing(int Stage)
        {
            InitializeComponent();
            _Stage = Stage;
            if (Stage == 3)
                this.Text = "Colour matching after dyeing";
            else if (Stage == 4)
            {
                this.Text = "Stability check after drying";
                this.rbAll.Visible = false;
                this.rbRejected.Visible = false;
                
                label7.Visible = false;
                cmboCause.Visible = false;
                label8.Visible = false;
                cmboRemedy.Visible = false;
                cmboReportOptions.Visible = false;
            }
            else if (Stage == 5)
            {
                this.Text = "QA check after compacting";
                this.rbRejected.Visible = false;
                this.rbAll.Visible = false;

                label7.Visible = false;
                cmboCause.Visible = false;
                label8.Visible = false;
                cmboRemedy.Visible = false;
                cmboReportOptions.Visible = false;
            }

            repo = new DyeRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboCause.CheckStateChanged += new System.EventHandler(this.cmboCauses_CheckStateChanged);
            this.cmboRemedy.CheckStateChanged += new System.EventHandler(this.cmboRemedys_CheckStateChanged);
            this.cmboDyeMachine.CheckStateChanged += new System.EventHandler(this.cmboDyeMachines_CheckStateChanged);
            this.cmboRemedy.CheckStateChanged += new System.EventHandler(this.cmboRemedys_CheckStateChanged);
        }

        private void frmShadeAfterDyeing_Load(object sender, EventArgs e)
        {
            repOps = new DyeReportOptions();
            QueryParms = new DyeQueryParameters();

            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var DyeQDCodes = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Code).ToList();
                foreach (var DyeCode in DyeQDCodes)
                {
                    cmboCause.Items.Add(new DyeHouse.CheckComboBoxItem(DyeCode.QDF_Pk, DyeCode.QDF_Description, false));
                }

                var Remedies = context.TLADM_DyeRemendyCodes.OrderBy(x => x.QRC_Code).ToList();
                foreach (var Remedy in Remedies)
                {
                    cmboRemedy.Items.Add(new DyeHouse.CheckComboBoxItem(Remedy.QRC_Pk, Remedy.QRC_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    var MachineDefinitions = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    foreach (var MachineDefinition in MachineDefinitions)
                    {
                        cmboDyeMachine.Items.Add(new DyeHouse.CheckComboBoxItem(MachineDefinition.MD_Pk, MachineDefinition.MD_AlternateDesc, false));
                    }


                    var Operators = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    foreach (var Operator in Operators)
                    {
                        cmboDyeOperator.Items.Add(new DyeHouse.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                    }
                  
                }

                var Qualities = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach(var Quality in Qualities)
                {
                    cmboQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }

              
            }

            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("1", "Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Dye Machine"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Dye Operator"));
            reportOptions.Add(new KeyValuePair<string, string>("4", "Colour"));
            if (_Stage == 3)
            {
                reportOptions.Add(new KeyValuePair<string, string>("5", "Remedy"));
                reportOptions.Add(new KeyValuePair<string, string>("6", "Fault"));
                reportOptions.Add(new KeyValuePair<string, string>("7", "Result"));
            }
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
                    QueryParms.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }
                else
                    QueryParms.DO_OptionSelected = 1;
              
                QueryParms.FromDate = DateTime.Parse(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = DateTime.Parse(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23);

                if (rbAll.Checked)
                    QueryParms.RejectedBatches = false;
                else
                    QueryParms.RejectedBatches = true;


                if (_Stage == 3)             //Stage 3 Colour Matching after Dyeing
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(28, QueryParms);
                    vRep.ShowDialog(this);
                    if(vRep != null)
                    {
                        vRep.Dispose();
                    }
                }
                else if (_Stage == 4)       //Stage 4 Stability Check after Drying;
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(32, QueryParms);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Dispose();
                    }
                }
                else                        //Stage 5 Results Check after Compacting;
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(33, QueryParms);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Dispose();
                    }

                }

                cmboCause.Items.Clear();
                cmboColour.Items.Clear();
                cmboDyeMachine.Items.Clear();
                cmboDyeOperator.Items.Clear();
                cmboQuality.Items.Clear();
                cmboRemedy.Items.Clear();
                this.frmShadeAfterDyeing_Load(this, null);

            }
        }

       

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Qualities.Add(repo.LoadQuality(item._Pk));

                }
                else
                {
                    var value = QueryParms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Qualities.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = QueryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        QueryParms.Colours.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCauses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.DyeQDCodes.Add(repo.LoadQDCodes(item._Pk));

                }
                else
                {
                    var value = QueryParms.DyeQDCodes.Find(it => it.QDF_Pk == item._Pk);
                    if (value != null)
                        QueryParms.DyeQDCodes.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboRemedys_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.RemedyCodes.Add(repo.LoadRemedyCodes(item._Pk));

                }
                else
                {
                    var value = QueryParms.RemedyCodes.Find(it => it.QRC_Pk == item._Pk);
                    if (value != null)
                        QueryParms.RemedyCodes.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDyeMachines_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Machines.Add(repo.LoadMachines(item._Pk));

                }
                else
                {
                    var value = QueryParms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Machines.Remove(value);

                }
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
