using Cutting;
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
        int options;

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

                var Sizes = context.TLADM_Sizes.ToList();
                foreach (var Size in Sizes)
                {
                    cmboSize.Items.Add(new DyeHouse.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }

                options = 1;

                var reportOptions = new BindingList<KeyValuePair<int, string>>();
                reportOptions.Add(new KeyValuePair<int, string>(1, "Batch No"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Style"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Colour"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "Size"));
                cmboReportOptions.DataSource = reportOptions;
                cmboReportOptions.ValueMember = "Key";
                cmboReportOptions.DisplayMember = "Value";
                cmboReportOptions.SelectedIndex = -1;
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded)
            {
                if (oCmbo.SelectedValue is int selectedOption)
                {
                    options = selectedOption;
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

                        LoadColoursBasedOnSelectedStyles();
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

                            LoadColoursBasedOnSelectedStyles();
                        }
                    }
                }
            }
        }

        private void LoadColoursBasedOnSelectedStyles()
        {
            using (var context = new TTI2Entities())
            {
                // Get the selected style IDs
                var selectedStyleIds = queryParms.Styles.Select(style => style.Sty_Id).ToList();

                // Query the bridge table to get associated color IDs for the selected styles
                var colorIds = context.TLADM_StyleColour
                    .Where(sc => selectedStyleIds.Contains(sc.STYCOL_Style_FK))
                    .Select(sc => sc.STYCOL_Colour_FK)
                    .ToList();

                // Get the colors based on the retrieved color IDs
                var colors = context.TLADM_Colours
                    .Where(c => !(bool)c.Col_Discontinued)
                    .Where(c => colorIds.Contains(c.Col_Id))
                    .OrderBy(c => c.Col_Display)
                    .ToList();

                // Clear the ComboBox
                cmboColour.Items.Clear();

                // Populate the ComboBox with the filtered colors
                foreach (var color in colors)
                {
                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(color.Col_Id, color.Col_Display, false));
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {

                queryParms.ReportSortOption = options;
 
                queryParms.DateWIP = DateTime.Now;

                //frmDyeViewReport vRep = new frmDyeViewReport(52, queryParms);
                //int h = Screen.PrimaryScreen.WorkingArea.Height;
                //int w = Screen.PrimaryScreen.WorkingArea.Width;
                //vRep.ClientSize = new Size(w, h);
                //vRep.ShowDialog(this);

                //frmDyeViewReport vRep = new frmDyeViewReport(52, queryParms);
                frmGarmentDyeingWIPReportViewer vRep = new frmGarmentDyeingWIPReportViewer(queryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                if (queryParms.Styles.Count != 0)
                {
                    cmboStyle.Items.Clear();                }

                if (queryParms.Colours.Count != 0)
                {
                    cmboColour.Items.Clear();
                }

                if (queryParms.Sizes.Count != 0)
                {
                    cmboSize.Items.Clear();
                }

                frmGarmentDyeBatchesWIP_Load(this, null);
            }
        }
    }
}
