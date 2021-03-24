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
    public partial class frmSuggestedKO : Form
    {
        ProdQueryParameters QueryParms;
        PPSRepository repo;

        bool formloaded;
        public frmSuggestedKO()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboMachine.CheckStateChanged += new System.EventHandler(this.cmboComboMachine_CheckStateChanged);
            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboGreigeQuality.CheckStateChanged += new System.EventHandler(this.cmboGreigeQuality_CheckStateChanged);
        }

        private void frmSuggestedKO_Load(object sender, EventArgs e)
        {
            formloaded = false;

            QueryParms = new ProdQueryParameters();
            repo = new PPSRepository();

            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    var ExistingMachines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x=>x.MD_MachineCode).ToList();
                    foreach (var record in ExistingMachines)
                    {
                        cmboMachine.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.MD_Pk, record.MD_Description, false));
                    }
                    
                    var ExistingGreige = context.TLADM_Griege.ToList();
                    foreach (var record in ExistingGreige)
                    {
                        cmboGreige.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.TLGreige_Id, record.TLGreige_Description, false));
                    }

                    var ExistingQuality = context.TLADM_GreigeQuality.ToList();
                    foreach (var record in ExistingQuality)
                    {
                        cmboGreigeQuality.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.GQ_Pk, record.GQ_Description, false));
                    }
                }

                formloaded = true;    
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboComboMachine_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.KnitMachines.Add(repo.LoadMachine(item._Pk));
                }
                else
                {
                    var value = QueryParms.KnitMachines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.KnitMachines.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
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

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                frmPPSViewRep vRep = new frmPPSViewRep(3, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
               
                cmboGreige.Items.Clear();
                cmboGreigeQuality.Items.Clear();
                cmboMachine.Items.Clear();

                frmSuggestedKO_Load(this, null);
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboMachine_SelectedIndexChanged(object sender, EventArgs e)
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

     

       
    }
}
