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

namespace Cutting
{
    public partial class frmSelWipCutting : Form
    {
        Util core;
        int _RepSortOption;
        bool formloaded;
        CuttingQueryParameters parms;
        CuttingRepository repo;

        public frmSelWipCutting()
        {
            InitializeComponent();
            repo = new CuttingRepository();
            this.cmboColour.CheckStateChanged  += new System.EventHandler(this.cmboColour_CheckStateChanged);
            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboQuality_CheckStateChanged);
            this.cmboDepartments.CheckStateChanged += new System.EventHandler(this.cmboDepartments_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckedChange);

        }

        private void frmSelWipCutting_Load(object sender, EventArgs e)
        {
            formloaded = false;
            core = new Util();
            parms = new CuttingQueryParameters();
            
            _RepSortOption = 1;
                    
            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Row in Existing)
                {
                    cmboQuality.Items.Add(new Cutting.CheckComboBoxItem(Row.TLGreige_Id, Row.TLGreige_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach(var Style in Styles)
                {
                    cmboStyles.Items.Add(new Cutting.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new Cutting.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Depts = context.TLADM_Departments.Where(x => x.Dep_IsCut).OrderBy(x => x.Dep_Description).ToList();
                foreach (var Dep in Depts)
                {
                    cmboDepartments.Items.Add(new Cutting.CheckComboBoxItem(Dep.Dep_Id, Dep.Dep_Description, false));
                }

            }


            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Cut Sheet Number"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Style"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboQuality_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
               Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Qualities.Add(repo.LoadQuality(item._Pk));
                }
                else
                {
                    var value = parms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        parms.Qualities.Remove(value);
                }
            }
        }

        private void cmboStyles_CheckedChange(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
                {
                    using (var context = new TTI2Entities())
                    {
                    Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                    if (item.CheckState)
                        {
                            if (parms.Styles.Count == 0)
                            {
                            // Clear the color combo if this is the first style selected
                            cmboColour.Items.Clear();
                            }

                        // Add selected style to QueryParms
                        parms.Styles.Add(repo.LoadStyle(item._Pk));

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
                            // Remove the deselected style from QueryParms
                            var value = parms.Styles.Find(it => it.Sty_Id == item._Pk);
                            if (value != null)
                            parms.Styles.Remove(value);

                            // If no styles are selected, reset the combo box to show all available colors
                            if (parms.Styles.Count == 0)
                            {
                            cmboColour.Items.Clear();
                                var allColours = context.TLADM_Colours
                                    .Where(x => !x.Col_Discontinued ?? false)
                                    .OrderBy(x => x.Col_Display)
                                    .ToList();

                                foreach (var colour in allColours)
                                {
                                cmboColour.Items.Add(new Cutting.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                                }
                            }
                            else
                            {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            cmboColour.Items.Clear();

                                var selectedStyleIds = parms.Styles.Select(s => s.Sty_Id).ToList();
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
                                    cmboColour.Items.Add(new Cutting.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
        private void cmboDepartments_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Departments.Add(repo.LoadDepartments(item._Pk));
                }
                else
                {
                    var value = parms.Departments.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        parms.Departments.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColour_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Colours.Add(repo.LoadColour(item._Pk));
                }
                else
                {
                    var value = parms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        parms.Colours.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
               
                parms.AllWIP = true;

                parms.RepSortOption = _RepSortOption;
                parms.FromDate = DateTime.Now;
                parms.ToDate = DateTime.Now;

                frmCutViewRep vRep = new frmCutViewRep(4, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                
                if (parms.Styles.Count != 0)
                {
                    cmboStyles.Items.Clear();
                }
                if (parms.Qualities.Count != 0)
                {
                    cmboQuality.Items.Clear();
                }
                if (parms.Colours.Count != 0)
                {
                    cmboColour.Items.Clear();
                }

                frmSelWipCutting_Load(this, null);
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                _RepSortOption = Convert.ToInt32(oCmbo.SelectedValue);
            }
        }

        private void cmboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboColour_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
