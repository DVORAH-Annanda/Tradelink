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

            this.chkcboCMTSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboCMTSelection_SelectedIndexChanged);
            this.chkcboStylesSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboStylesSelection_SelectedIndexChanged);
            this.chkcboSizesSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboSizesSelection_SelectedIndexChanged);
            this.chkcboColoursSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboColoursSelection_SelectedIndexChanged);
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

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
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
            if (sender is ProductionPlanning.CheckComboBoxItem item && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    if (item.CheckState)
                    {
                        if (QueryParms.Styles.Count == 0)
                        {
                            // Clear the color combo if this is the first style selected
                            chkcboColoursSelection.Items.Clear();
                        }

                        // Add selected style to QueryParms
                        QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                        // Get colors associated with the selected style
                        var coloursForSelectedStyle = context.TLPPS_Replenishment
                            .Where(x => x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued)
                            .GroupBy(x => x.TLREP_Colour_FK)
                            .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                            .ToList();

                        // Loop through the colours for the selected style
                        foreach (var colourPk in coloursForSelectedStyle)
                        {
                            var clr = context.TLADM_Colours.Find(colourPk);
                            if (clr != null && !chkcboColoursSelection.Items.Cast<ProductionPlanning.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                            }
                        }
                    }
                    else
                    {
                        // Remove the deselected style from QueryParms
                        var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                        if (value != null)
                            QueryParms.Styles.Remove(value);

                        // If no styles are selected, reset the combo box to show all available colors
                        if (QueryParms.Styles.Count == 0)
                        {
                            chkcboColoursSelection.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !x.Col_Discontinued ?? false)
                                .OrderBy(x => x.Col_Display)
                                .ToList();

                            foreach (var colour in allColours)
                            {
                                chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                            }
                        }
                        else
                        {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            chkcboColoursSelection.Items.Clear();

                            var selectedStyleIds = QueryParms.Styles.Select(s => s.Sty_Id).ToList();
                            var allSelectedColours = context.TLPPS_Replenishment
                                .Where(x => selectedStyleIds.Contains(x.TLREP_Style_FK) && !x.TLREP_Discontinued)
                                .GroupBy(x => x.TLREP_Colour_FK)
                                .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                                .Distinct()
                                .ToList();

                            foreach (var colourPk in allSelectedColours)
                            {
                                var clr = context.TLADM_Colours.Find(colourPk);
                                if (clr != null)
                                {
                                    chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                                }
                            }
                        }
                    }
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

        private void chkcboCMTSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void chkcboColoursSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void chkcboStylesSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void chkcboSizesSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
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
