using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utilities; 

namespace DyeHouse
{
    public partial class frmGarmentDyeProduction : Form
    {
        private bool formloaded;
        private Util core;
        private DyeRepository repo;

        private DyeQueryParameters QueryParms = new DyeQueryParameters();

        public frmGarmentDyeProduction()
        {
            InitializeComponent();

            chkcboCMTSelection.CheckStateChanged += chkcboCMTSelection_CheckStateChanged;
            chkcboStylesSelection.CheckStateChanged += chkcboStylesSelection_CheckStateChanged;
            chkcboColoursSelection.CheckStateChanged += chkcboColoursSelection_CheckStateChanged;
            chkcboSizesSelection.CheckStateChanged += chkcboSizesSelection_CheckStateChanged;

            chkcboCMTSelection.SelectedIndexChanged += chkcboCMTSelection_SelectedIndexChanged;
            chkcboStylesSelection.SelectedIndexChanged += chkcboStylesSelection_SelectedIndexChanged;
            chkcboColoursSelection.SelectedIndexChanged += chkcboColoursSelection_SelectedIndexChanged;
            chkcboSizesSelection.SelectedIndexChanged += chkcboSizesSelection_SelectedIndexChanged;
        }

        private void frmGarmentDyeingProd_Load(object sender, EventArgs e)
        {
            formloaded = false;

            core = new Util();
            repo = new DyeRepository();   // Make sure this exists in DyeHouse


            using (var context = new TTI2Entities())
            {
          
                var departments = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var record in departments)
                {
                    chkcboCMTSelection.Items.Add(new CheckComboBoxItem(record.Dep_Id, record.Dep_Description, false));
                }

               
                var styles = context.TLADM_Styles
                    .OrderBy(x => x.Sty_Description)
                    .ToList();
                foreach (var record in styles)
                {
                    chkcboStylesSelection.Items.Add(
                        new CheckComboBoxItem(record.Sty_Id, record.Sty_Description, false)
                    );
                }

            
                var colours = context.TLADM_Colours
                    .OrderBy(x => x.Col_Display)
                    .ToList();
                foreach (var record in colours)
                {
                    chkcboColoursSelection.Items.Add(
                        new CheckComboBoxItem(record.Col_Id, record.Col_Display, false)
                    );
                }
                var sizes = context.TLADM_Sizes
                    .Where(x => !x.SI_Discontinued)
                    .OrderBy(x => x.SI_DisplayOrder)
                    .ToList();
                foreach (var record in sizes)
                {
                    chkcboSizesSelection.Items.Add(
                        new CheckComboBoxItem(record.SI_id, record.SI_Description, false)
                    );
                }
            }

            formloaded = true;
        }

        private void chkcboCMTSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formloaded) return;
            if (sender is CheckComboBoxItem item)
            {
                //if (item.CheckState)
                //{
                //    // Add department to the QueryParms
                //    var dept = repo.LoadDepartments(item._Pk); // Corrected line
                //    if (dept != null) // Added null check
                //    {
                //        QueryParms.Departments.Add(dept);
                //    }
                //    else
                //    {
                //        MessageBox.Show($"Department with ID {item._Pk} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
                //else
                //{
                //    // Remove department if it exists
                //    var value = QueryParms.Departments.Find(it => it.Dep_Id == item._Pk);
                //    if (value != null)
                //    {
                //        QueryParms.Departments.Remove(value);
                //    }
                //}
            }
        }

        private void chkcboStylesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formloaded) return;
            if (sender is CheckComboBoxItem item)
            {
                using (var context = new TTI2Entities())
                {
                    if (item.CheckState)
                    {
                     
                        if (QueryParms.Styles.Count == 0)
                        {
                            chkcboColoursSelection.Items.Clear();
                        }
                        QueryParms.Styles.Add(repo.LoadStyle(item._Pk));
                        
                        var coloursForStyle = context.TLPPS_Replenishment
                            .Where(x => x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued)
                            .GroupBy(x => x.TLREP_Colour_FK)
                            .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                            .ToList();

                        foreach (var colourPk in coloursForStyle)
                        {
                            var clr = context.TLADM_Colours.Find(colourPk);
                            if (clr != null &&
                                !chkcboColoursSelection.Items.Cast<CheckComboBoxItem>().Any(c => c._Pk == clr.Col_Id))
                            {
                                chkcboColoursSelection.Items.Add(
                                    new CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false)
                                );
                            }
                        }
                    }
                    else
                    {
                        var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                        if (value != null)
                            QueryParms.Styles.Remove(value);

                        if (QueryParms.Styles.Count == 0)
                        {
                            chkcboColoursSelection.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !(x.Col_Discontinued ?? false))
                                .OrderBy(x => x.Col_Display)
                                .ToList();
                            foreach (var clr in allColours)
                            {
                                chkcboColoursSelection.Items.Add(
                                    new CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false)
                                );
                            }
                        }
                        else
                        {
                            chkcboColoursSelection.Items.Clear();
                            var styleIds = QueryParms.Styles.Select(s => s.Sty_Id).ToList();

                            var colours = context.TLPPS_Replenishment
                                .Where(x => styleIds.Contains(x.TLREP_Style_FK) && !x.TLREP_Discontinued)
                                .GroupBy(x => x.TLREP_Colour_FK)
                                .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                                .Distinct()
                                .ToList();

                            foreach (var colourPk in colours)
                            {
                                var clr = context.TLADM_Colours.Find(colourPk);
                                if (clr != null)
                                {
                                    chkcboColoursSelection.Items.Add(
                                        new CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false)
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }

        private void chkcboColoursSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formloaded) return;
            if (sender is CheckComboBoxItem item)
            {
                if (item.CheckState)
                {
                    var clr = repo.LoadColour(item._Pk);
                    QueryParms.Colours.Add(clr);
                }
                else
                {
                    var value = QueryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        QueryParms.Colours.Remove(value);
                }
            }
        }

        private void chkcboSizesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formloaded) return;
            if (sender is CheckComboBoxItem item)
            {
                if (item.CheckState)
                {
                    var sz = repo.LoadSize(item._Pk);
                    QueryParms.Sizes.Add(sz);
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
            if (sender is ComboBox oCmbo && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }
        private void chkcboStylesSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox oCmbo && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }
        private void chkcboColoursSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox oCmbo && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }
        private void chkcboSizesSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox oCmbo && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!formloaded) return;

            // Set the date range in our QueryParms
            QueryParms.FromDate = dtpFrom.Value.Date;
            QueryParms.ToDate = dtpTo.Value.Date;

            // example: a few dummy flags
            bool[] dummyFlags = new bool[] { true, false, true };

            // Make sure 'frmDyeViewReport' has a matching constructor: (int, DyeQueryParameters, bool[])
            frmDyeViewReport vRep = new frmDyeViewReport(66, QueryParms);

            // Added MessageBox to display QueryParms content
            string queryParmsSummary = $"FromDate: {QueryParms.FromDate}\n" +
                                                   $"ToDate: {QueryParms.ToDate}\n" +
                                                   $"Styles: {string.Join(", ", QueryParms.Styles.Select(s => s.Sty_Description))}\n" +
                                                   $"Colours: {string.Join(", ", QueryParms.Colours.Select(c => c.Col_Display))}\n" +
                                                   $"Sizes: {string.Join(", ", QueryParms.Sizes.Select(sz => sz.SI_Description))}\n";
            //$"Departments: {string.Join(", ", QueryParms.Departments.Select(d => d.Dep_Description))}"

            MessageBox.Show(queryParmsSummary, "QueryParms Content", MessageBoxButtons.OK, MessageBoxIcon.Information);

            vRep.ClientSize = new Size(
                Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height
            );
            vRep.ShowDialog(this);

            // Clear for the next run
            chkcboCMTSelection.Items.Clear();
            chkcboStylesSelection.Items.Clear();
            chkcboColoursSelection.Items.Clear();
            chkcboSizesSelection.Items.Clear();

            // Reload form to original state
            frmGarmentDyeingProd_Load(this, null);
        }
    }
}    
