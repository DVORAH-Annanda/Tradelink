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


namespace CustomerServices
{
    public partial class frmOutStandingPL : Form
    {
        bool formloaded;
        Repository repo;
        CustomerServicesParameters QueryParms;

        public frmOutStandingPL()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboWareHouses.CheckStateChanged += new EventHandler(this.cmboWareHouses_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            
            repo = new Repository();
        }

        private void frmOutStandingPL_Load(object sender, EventArgs e)
        {
               formloaded = false;

            QueryParms = new CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {

                var WareHouses = context.TLADM_WhseStore.Where(x=>x.WhStore_GradeA).OrderBy(x=>x.WhStore_Description).ToList() ;
                foreach (var WareHouse in WareHouses)
                {
                    cmboWareHouses.Items.Add(new CustomerServices.CheckComboBoxItem(WareHouse.WhStore_Id, WareHouse.WhStore_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => (bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));

                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Display, false));
                }
            }
            formloaded = true;

        
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWareHouses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Whses.Add(repo.LoadWhse(item._Pk));

                }
                else
                {
                    var value = QueryParms.Whses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        QueryParms.Whses.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        //private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        //{

        //    if (sender is CustomerServices.CheckComboBoxItem && formloaded)
        //    {
        //        CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
        //        if (item.CheckState)
        //        {
        //            QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

        //        }
        //        else
        //        {
        //            var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
        //            if (value != null)
        //                QueryParms.Styles.Remove(value);

        //        }
        //    }
        //}
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem item && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    if (item.CheckState)
                    {
                        if (QueryParms.Styles.Count == 0)
                        {
                            // Clear the color combo if this is the first style selected
                            cmboColours.Items.Clear();
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
                            if (clr != null && !cmboColours.Items.Cast<CustomerServices.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
                            cmboColours.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !x.Col_Discontinued ?? false)
                                .OrderBy(x => x.Col_Display)
                                .ToList();

                            foreach (var colour in allColours)
                            {
                                cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                            }
                        }
                        else
                        {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            cmboColours.Items.Clear();

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
                                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                                }
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
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
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
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
            if (oBtn != null)
            {
                CSVServices svcs = new CSVServices();
                if (cbSummarised.Checked)
                   svcs.PLSummarised = true;

                if (cbPLStockOrders.Checked)
                    svcs.PLStockOrder = true;
                
                frmCSViewRep VRep = new frmCSViewRep(9, QueryParms, svcs);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                VRep.ClientSize = new Size(w, h);
                VRep.ShowDialog(this);

                cmboWareHouses.Items.Clear();
                cmboStyles.Items.Clear();
                cmboSizes.Items.Clear();
                cmboColours.Items.Clear();

                frmOutStandingPL_Load(this, null);
            }
        }

        private void cmboWareHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
