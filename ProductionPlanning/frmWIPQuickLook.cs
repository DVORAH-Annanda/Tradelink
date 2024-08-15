using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;


namespace ProductionPlanning
{
    public partial class frmWIPQuickLook : Form
    {
        bool formloaded;
        PPSRepository repo;        
        ProdQueryParameters QueryParms;
        Util core;
        public frmWIPQuickLook()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.chkcboCMTSelection.CheckStateChanged += new System.EventHandler(this.chkcboCMTSelection_CheckStateChanged);
            this.chkcboStylesSelection.CheckStateChanged += new System.EventHandler(this.chkcboStylesSelection_CheckStateChanged);
            this.chkcboColoursSelection.CheckStateChanged += new System.EventHandler(this.chkcboColoursSelection_CheckStateChanged);
            this.chkcboSizesSelection.CheckStateChanged += new System.EventHandler(this.chkcboSizesSelection_CheckStateChanged);
        }

        private void frmWIPQuickLook_Load(object sender, EventArgs e)
        {
            formloaded = false;
            core = new Util();
            repo = new PPSRepository();
            QueryParms = new ProdQueryParameters();
            

            using (var context = new TTI2Entities())
            {
                var Departments = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Record in Departments)
                {
                    chkcboCMTSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Dep_Id, Record.Dep_Description, false));
                }

                var Styles = context.TLADM_Styles.ToList();
                foreach (var Record in Styles)
                {
                    chkcboStylesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Sty_Id, Record.Sty_Description, false));
                }
                chkcboStylesSelection.CheckStateChanged += chkcboStylesSelection_CheckStateChanged;
                
                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var record in Colours)
                {
                    chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var record in Sizes)
                {
                    chkcboSizesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.SI_id, record.SI_Description, false));
                }
                

            }
            formloaded = true;

        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void chkcboCMTSelection_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Departments.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Departments.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Departments.Remove(value);

                }
            }
        }
        private void chkcboStylesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formloaded) return;
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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
                
            var selectedStyles = new List<int>();
            foreach (var item in chkcboStylesSelection.Items)
            {
                var checkItem = item as ProductionPlanning.CheckComboBoxItem;
                if (checkItem != null && checkItem.CheckState)
                {
                    selectedStyles.Add(checkItem._Pk);
                }
            }

            // Filter colours based on selected styles
            using (var context = new TTI2Entities())
            {
                var colours = context.TLCUT_CutSheet
                    .Where(x => selectedStyles.Contains(x.TLCutSH_Styles_FK))
                    .Select(x => x.TLCutSH_Colour_FK)
                    .Distinct()
                    .ToList();

                var filteredColours = context.TLADM_Colours
                    .Where(x => colours.Contains(x.Col_Id))
                    .OrderBy(x => x.Col_Display)
                    .ToList();

                // Update chkcboColoursSelection
                chkcboColoursSelection.Items.Clear();
                foreach (var record in filteredColours)
                {
                    chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //--------------------------------------------------------------------------------------
        private void chkcboColoursSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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

        //---------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //---------------------------------------------------------------------------------------
        private void chkcboSizesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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
                // Display the ProgressBar control.
                pbarExpedite.Visible = true;
                // Set Minimum to 1 
                pbarExpedite.Minimum = 1;
                // Set the initial value of ProgessBar
                pbarExpedite.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                pbarExpedite.Step = 1;
                frmPPSViewRep vRep = new frmPPSViewRep(11, QueryParms, pbarExpedite);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                chkcboCMTSelection.Items.Clear();
                chkcboStylesSelection.Items.Clear();
                chkcboColoursSelection.Items.Clear();
                chkcboSizesSelection.Items.Clear();

                frmWIPQuickLook_Load(this, null);
            }
        }
    }
}
