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

namespace Spinning
{
    public partial class frmQAReportSelection : Form
    {
      
        bool formloaded;
        SpinningQueryParameters QueryParms;
        SpinningRepository repo;
        Util core;
                

        public frmQAReportSelection()
        {
            InitializeComponent();
            this.repo = new SpinningRepository();
            this.cmboRSBMachines.CheckStateChanged += new System.EventHandler(this.cmboRSBMachines_CheckStateChanged);

        }

        private void frmQAReportSelection_Load(object sender, EventArgs e)
        {
            formloaded = false;
      
          
            QueryParms = new SpinningQueryParameters();

            txtBottomTolerance.Text = "4410";
            txtTopTolerance.Text = "4590";

            core = new Util();

            txtBottomTolerance.KeyPress += core.txtWin_KeyPress;
            txtBottomTolerance.KeyDown += core.txtWin_KeyDownJI;

            txtTopTolerance.KeyPress += core.txtWin_KeyPress;
            txtTopTolerance.KeyDown += core.txtWin_KeyDownJI;
           

            rbOutsideTNo.Checked = true;
 
           

            using (var context = new TTI2Entities())
            {
                var deptdetails = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                if (deptdetails != null)
                {
                    var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == deptdetails.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    foreach (var Machine in Machines)
                    {
                        cmboRSBMachines.Items.Add(new Spinning.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_MachineCode, false)); 
                    }
                   
                    cmboRSBMachines.SelectedValue = 0;
                }
            }

            formloaded = true;
        }

        

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboRSBMachines_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Spinning.CheckComboBoxItem && formloaded)
            {
                Spinning.CheckComboBoxItem item = (Spinning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Machines.Add(repo.LoadMachine(item._Pk));
                }
                else
                {
                    var value = QueryParms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Machines.Remove(value);
                }
            }
        }

        private void rbPartTest_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
               
            }

        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
        }

        private void dtpM2FromDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded);
        }
        private void dtpM_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            
        }

        private void rb2SelectAMachine_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                QAYarnReportOptions repsel = new QAYarnReportOptions();
                
                repsel.from = Convert.ToDateTime(dtpRSBFromDate.Value.ToShortDateString());
                repsel.to = Convert.ToDateTime(dtpRSBToDate.Value.ToShortDateString());

                if (rbCardMeasurements.Checked)
                {
                    repsel.MeasurementOption = 1;
                }
                else if (rbSpinning.Checked)
                {
                    repsel.MeasurementOption = 2;
                }
                else if (rbRSBMeasurements.Checked)
                {
                    repsel.MeasurementOption = 3;
             
                    if (rbOutsideTYes.Checked)
                    {
                        QueryParms.OutSideTolerance = true;
                    }
                    
                    QueryParms.LowerTolerance = Convert.ToInt32(txtBottomTolerance.Text);
                    QueryParms.UpperTolerance = Convert.ToInt32(txtTopTolerance.Text);
                }
               
                //-------------------------------------------------------------------------------------
                // Now To Print the Report 
                //----------------------------------------------------------------------------------------
                if (chkRSBQaSummary.Checked)
                    repsel.QASummary = true;

                QueryParms.FromDate = repsel.from;
                QueryParms.ToDate = repsel.to;

                frmViewReport vRep = new frmViewReport(14, QueryParms, repsel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboRSBMachines.Items.Clear();

                this.frmQAReportSelection_Load(this, null);
                
                //-------------------------------------------------------------------------------------------------
            }
        }

        private void cmbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;

        }

        private void cmbMachines2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;

        }

        private void cmboRSBMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

     

       
    }

    public class QAYarnReportOptions
    {
        public QAYarnReportOptions()
        {
        }

        public int MeasurementOption { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public bool selectAllMachines { get; set; }
        public bool selectAMachine { get; set; }
        public int MachineSelectedPk { get; set; }
        public int TestSelectedPk { get; set; }
        
        public bool QASummary { get; set; }
    

        public bool OutSideTol { get; set; }
        public int LowestTol { get; set; }
        public int HighestTol { get; set; }

        public bool YarnOrderAuditTrail { get; set; }
        public bool KnitOrderAuditTrail { get; set; } 

 
    }
}
