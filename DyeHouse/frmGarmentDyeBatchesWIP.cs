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
    public partial class frmGarmentDyeBatchesWIP : Form
    {

        bool formLoaded;

        DyeHouse.DyeQueryParameters queryParms;
        DyeHouse.DyeRepository repo;

        public frmGarmentDyeBatchesWIP()
        {
            InitializeComponent();
            repo = new DyeRepository();
            queryParms = new DyeQueryParameters();

            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyle_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColour_CheckStateChanged);
            this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSize_CheckStateChanged);

        }

        private void frmGarmentDyeBatchesWIP_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                queryParms = new DyeQueryParameters();
                formLoaded = true;

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new DyeHouse.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }


            }
        }

        private void cmboSize_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    queryParms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = queryParms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        queryParms.Sizes.Remove(value);

                }
            }


        }

        private void cmboStyle_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                    if (item.CheckState)
                    {
                        if (queryParms.Styles.Count == 0)
                        {
                            // Clear the color combo if this is the first style selected
                            cmboColour.Items.Clear();
                        }

                        // Add selected style to queryParms
                        queryParms.Styles.Add(repo.LoadStyle(item._Pk));

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
                            if (clr != null && !cmboColour.Items.Cast<Cutting.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                cmboColour.Items.Add(new Cutting.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                            }
                        }
                    }
                    else
                    {
                        // Remove the deselected style from queryParms
                        var value = queryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                        if (value != null)
                            queryParms.Styles.Remove(value);

                        // If no styles are selected, reset the combo box to show all available colors
                        if (queryParms.Styles.Count == 0)
                        {
                            cmboColour.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !x.Col_Discontinued ?? false)
                                .OrderBy(x => x.Col_Display)
                                .ToList();

                            foreach (var colour in allColours)
                            {
                                cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                            }
                        }
                        else
                        {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            cmboColour.Items.Clear();

                            var selectedStyleIds = queryParms.Styles.Select(s => s.Sty_Id).ToList();
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
                                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmboColour_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    queryParms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = queryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        queryParms.Colours.Remove(value);

                }
            }

        }


    }
}
