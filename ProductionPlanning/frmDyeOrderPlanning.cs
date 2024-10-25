﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace ProductionPlanning
{
    public partial class frmDyeOrderPlanning : Form
    {
        bool formloaded;
        bool[] Selected;

        PPSRepository repo;
        ProdQueryParameters QueryParms;
        Util core;

        public frmDyeOrderPlanning()
        {
            InitializeComponent();

            this.chkcboStylesSelection.CheckStateChanged += new System.EventHandler(this.chkcboStylesSelection_CheckStateChanged);
            this.chkcboColoursSelection.CheckStateChanged += new System.EventHandler(this.chkcboColoursSelection_CheckStateChanged);

            this.chkcboStylesSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboStylesSelection_SelectedIndexChanged);
            this.chkcboColoursSelection.SelectedIndexChanged += new System.EventHandler(this.chkcboColoursSelection_SelectedIndexChanged);

        }

        private void frmDyeOrderPlanning_Load(object sender, EventArgs e)
        {
            formloaded = false;
            core = new Util();
            repo = new PPSRepository();
            QueryParms = new ProdQueryParameters();

            dtpFromDate.Value = DateTime.Today;

            // Initialize the date parameter in QueryParms with today's date
            QueryParms.FromDate = dtpFromDate.Value;

            // Set the event handler for DateTimePicker value change
            dtpFromDate.ValueChanged += new EventHandler(dtpFromDate_ValueChanged);


            using (var context = new TTI2Entities())
            {
                //var Departments = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                //foreach (var Record in
                //    Departments)
                //{
                //    chkcboCMTSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Dep_Id, Record.Dep_Description, false));
                //}

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Record in Styles)
                {
                    chkcboStylesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Sty_Id, Record.Sty_Description, false));
                }
                //chkcboStylesSelection.CheckStateChanged += chkcboStylesSelection_CheckStateChanged;

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var record in Colours)
                {
                    chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
                }

                //var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                //foreach (var record in Sizes)
                //{
                //    chkcboSizesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.SI_id, record.SI_Description, false));
                //}
                // Wire up the check state changed events
                //this.chkcboCMTSelection.CheckStateChanged += new System.EventHandler(this.chkcboCMTSelection_CheckStateChanged);

                //this.chkcboSizesSelection.CheckStateChanged += new System.EventHandler(this.chkcboSizesSelection_CheckStateChanged);
            }

            formloaded = true;
        }
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (formloaded)
            {
                // Update QueryParms.FromDate whenever the DateTimePicker value changes
                QueryParms.FromDate = dtpFromDate.Value;

                // Optionally, you could log the change or take other actions
                Console.WriteLine("FromDate updated to: " + QueryParms.FromDate.ToShortDateString());
            }
        }
        //private void chkcboCMTSelection_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (sender is ProductionPlanning.CheckComboBoxItem item && formloaded)
        //    {
        //        if (item.CheckState)
        //        {
        //            if (!QueryParms.Departments.Any(d => d.Dep_Id == item._Pk))
        //            {
        //                QueryParms.Departments.Add(repo.LoadDepartment(item._Pk));
        //            }
        //        }
        //        else
        //        {
        //            var value = QueryParms.Departments.Find(it => it.Dep_Id == item._Pk);
        //            if (value != null)
        //                QueryParms.Departments.Remove(value);
        //        }
        //    }
        //}
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

        private void chkcboStylesSelection_SelectedIndexChanged(object sender, EventArgs e)
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


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                // Construct the string representation of the QueryParms
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Query Parameters:");

                // Assign FromDate directly from the DateTimePicker
                QueryParms.FromDate = dtpFromDate.Value;

                // Check if FromDate is set and append to the string builder
                // Display the FromDate
                sb.AppendLine($"From Date: {QueryParms.FromDate.ToShortDateString()}");


                // Check Departments
                if (QueryParms.Departments != null && QueryParms.Departments.Count > 0)
                {
                    sb.AppendLine("Departments:");
                    foreach (var department in QueryParms.Departments)
                    {
                        sb.AppendLine($"- {department.Dep_Description} (ID: {department.Dep_Id})");
                    }
                }

                // Check Styles
                if (QueryParms.Styles != null && QueryParms.Styles.Count > 0)
                {
                    sb.AppendLine("Styles:");
                    foreach (var style in QueryParms.Styles)
                    {
                        sb.AppendLine($"- {style.Sty_Description} (ID: {style.Sty_Id})");
                    }
                }

                // Check Colours
                if (QueryParms.Colours != null && QueryParms.Colours.Count > 0)
                {
                    sb.AppendLine("Colours:");
                    foreach (var colour in QueryParms.Colours)
                    {
                        sb.AppendLine($"- {colour.Col_Description} (ID: {colour.Col_Id})");
                    }
                }

                // Check Sizes
                if (QueryParms.Sizes != null && QueryParms.Sizes.Count > 0)
                {
                    sb.AppendLine("Sizes:");
                    foreach (var size in QueryParms.Sizes)
                    {
                        sb.AppendLine($"- {size.SI_Description} (ID: {size.SI_id})");
                    }
                }

                // Show the parameters in a message box
               // MessageBox.Show(sb.ToString(), "Query Parameters");

                // Proceed with creating and displaying the report
                frmPPSViewRep vRep = new frmPPSViewRep(12, QueryParms);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                // Clear selections
                chkcboCMTSelection.Items.Clear();
                chkcboStylesSelection.Items.Clear();
                chkcboColoursSelection.Items.Clear();
                chkcboSizesSelection.Items.Clear();
  
            }
        }     

    }
}

