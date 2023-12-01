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

namespace ProductionPlanning
{
    public partial class frmPlanningKnitStock : Form
    {
        ProdQueryParameters QueryParms;
        PPSRepository repo;
        Util core;

        bool FormLoaded;

        public frmPlanningKnitStock()
        {
            InitializeComponent();
            
            repo = new PPSRepository();
            core = new Util();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboGreigeQuality.CheckStateChanged += new System.EventHandler(this.cmboGreigeQuality_CheckStateChanged);
            this.cmboFabWeight.CheckStateChanged += new System.EventHandler(this.cmboFabWeight_CheckStateChanged);
            this.cmboFabWidth.CheckStateChanged += new System.EventHandler(this.cmboFabWidth_CheckStateChanged);
            this.cmboStore.CheckStateChanged += new System.EventHandler(this.cmboStore_CheckStateChanged); 
        }

        private void PlanningKnitStock_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            QueryParms = new ProdQueryParameters();

         

            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    var ExistingGreige = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                    foreach (var record in ExistingGreige)
                    {
                        cmboGreige.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.TLGreige_Id, record.TLGreige_Description, false));
                    }
                
                    var ExistQuality = context.TLADM_GreigeQuality.ToList();
                    foreach (var record in ExistQuality)
                    {
                        cmboGreigeQuality.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.GQ_Pk, record.GQ_Description, false));
                    }

                 
                    var ExistWeight = context.TLADM_FabricWeight.ToList();
                    foreach (var Record in ExistWeight)
                    {
                        cmboFabWeight.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.FWW_Id, Record.FWW_Description, false));
                    }

                    var ExistWidth = context.TLADM_FabWidth.ToList();
                    foreach (var record in ExistWidth)
                    {
                        cmboFabWidth.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.FW_Id, record.FW_Description, false));
                    }

                    var ExistWhse = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                    foreach (var record in ExistWhse)
                    {
                        cmboStore.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.WhStore_Id, record.WhStore_Description, false));
                    }
                                   
                }

                chkGradeA.Checked = true;
                chkGradeB.Checked = false;
                chkGradeC.Checked = false;
                chkExcludeDiscontinued.Checked = false;

                FormLoaded = true;
            }
        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Greiges.Add(repo.LoadGreige(item._Pk));

                }
                else
                {
                    var value = QueryParms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Greiges.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreigeQuality_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.GreigeQualities.Add(repo.LoadGreigeQual(item._Pk));

                }
                else
                {
                    var value = QueryParms.GreigeQualities.Find(it => it.GQ_Pk == item._Pk);
                    if (value != null)
                        QueryParms.GreigeQualities.Remove(value);

                }
            }
            
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboFabWeight_CheckStateChanged(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboFabWidth_CheckStateChanged(object sender, EventArgs e)
        {

           
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStore_CheckStateChanged(object sender, EventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBth = sender as Button;
            if (oBth != null && FormLoaded)
            {
                bool[] BoxChecked = new bool[3];

                BoxChecked[0] = chkGradeA.Checked;
                BoxChecked[1] = chkGradeB.Checked;
                BoxChecked[2] = chkGradeC.Checked;

                QueryParms.ExcludeDiscontinued = (bool)chkExcludeDiscontinued.Checked;

                QueryParms.GradeType = core.CalculateSelection(BoxChecked);

                QueryParms.IncludeGradeAWithwarnings = (bool)cbIncludeWithWarnings.Checked;
                
                frmPPSViewRep vRep = new frmPPSViewRep(2, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();

                cmboGreige.Items.Clear();
                cmboGreigeQuality.Items.Clear();

                PlanningKnitStock_Load(this, null);
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboGreigeQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboFabWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboFabWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

       
    }
}
