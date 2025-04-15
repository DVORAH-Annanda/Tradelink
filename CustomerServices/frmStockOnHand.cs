using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using System.IO;
using ClosedXML.Excel;
using System.Data.SqlClient;


namespace CustomerServices
{
    public partial class frmStockOnHand : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TTISqlConnection"].ConnectionString;
        bool formloaded;
        int _OptionNo;

        Repository repo;
        CustomerServicesParameters QueryParms;
        UserDetails _Ud; 

        public frmStockOnHand( int OptionNo, UserDetails Ud)
        {
            InitializeComponent();
            _OptionNo = OptionNo;
            _Ud = Ud;
        }

        public frmStockOnHand(int OptionNo)
        {
            InitializeComponent();
            _OptionNo = OptionNo;
            _Ud = null;
        }

        private void frmStockOnHand_Load(object sender, EventArgs e)
        {
            IList<TLADM_WhseStore> Whses = null;

            formloaded = false;

            repo = new Repository();
            QueryParms = new CustomerServicesParameters();

            if (_OptionNo == 1)
            {
                this.Text = "Stock quantities on hand";
            }
            else
            {
                this.Text = "Boxes in stock";
                rbCostingPastel.Visible = false;
            }
            rbGradeA.Checked = true;

            using (var context = new TTI2Entities())
            {
                rbCostingPastel.Checked = false;
                
                if (_Ud == null || !_Ud._External)
                {
                    Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA).ToList();
                }
                else
                {
                    Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA && !x.WhStore_RePack).ToList();
                }

                foreach (var Whse in Whses)
                {
                    comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    comboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    comboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Display, false));
                }
                //--------------------------------------------------------
                // wire up the check state changed event
                //--------------------------------------------------------------------------------------------------------
                this.comboWhses.CheckStateChanged += new System.EventHandler(this.cmboWhses_CheckStateChanged);
                this.comboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
                this.comboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
                this.comboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            }
            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWhses_CheckStateChanged(object sender, EventArgs e)
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
        //            using (var context = new TTI2Entities())
        //            {

        //                var Colours = context.TLPPS_Replenishment.Where(x=>x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued).GroupBy(x=>x.TLREP_Colour_FK).ToList();
        //                foreach (var Col in Colours)
        //                {
        //                    var Clr_Pk = Col.FirstOrDefault().TLREP_Colour_FK;
        //                    var Clr = context.TLADM_Colours.Find(Clr_Pk);
        //                    if (Clr != null)
        //                    {
        //                        comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Clr.Col_Id, Clr.Col_Display, false));
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
        //            if (value != null)
        //                QueryParms.Styles.Remove(value);

        //            if (QueryParms.Styles.Count == 0)
        //            {
        //                comboColours.Items.Clear();
        //                using (var context = new TTI2Entities())
        //                {
        //                    var Colours = context.TLADM_Colours.Where(x=>!(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
        //                    foreach (var Colour in Colours)
        //                    {
        //                        comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
        //                    }
        //                }
        //            }

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
                            comboColours.Items.Clear();
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
                            if (clr != null && !comboColours.Items.Cast<CustomerServices.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
                            comboColours.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !x.Col_Discontinued ?? false)
                                .OrderBy(x => x.Col_Display)
                                .ToList();

                            foreach (var colour in allColours)
                            {
                                comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                            }
                        }
                        else
                        {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            comboColours.Items.Clear();

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
                                    comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
     
            int repNo = 0;
            if (_OptionNo == 1)
                repNo = 6;
            else
                repNo = 7;

            if (oBtn != null)
            {
                CSVServices csvService = new CSVServices();
                if (RBStockOH.Checked)
                    csvService.SOHClassification = true;
                else if (RBStockAvail.Checked)
                    csvService.SOHClassification = false;
                else if (rbBoxesReturned.Checked)
                    csvService.SOHBoxReturned = true;
                else
                    csvService.SplitBoxOnly = true;

                if(rbCostingPastel.Checked)
                {
                    QueryParms.Colours.Clear();
                    QueryParms.CostColoursChecked = rbCostingPastel.Checked;
                    
                    using ( var context = new TTI2Entities())
                    {
                        var Clrs = context.TLADM_Colours.Where(x=>x.Col_ColCosting).ToList();
                        foreach(var Clr in Clrs)
                        {
                            QueryParms.Colours.Add(repo.LoadColour(Clr.Col_Id));
                        }
                    }
                }
                csvService.DateIntoStock = dtpDateinStock.Value;

                QueryParms.GradeA = false;

                if (rbGradeA.Checked)
                {
                    QueryParms.GradeA = true;
                }

                if (rbDiscontinued.Checked)
                {
                    QueryParms.Discontinued = true;
                }
                frmCSViewRep vRep = new frmCSViewRep(repNo, QueryParms, csvService);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if(vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                comboColours.Items.Clear();
                comboSizes.Items.Clear();
                comboStyles.Items.Clear();
                comboWhses.Items.Clear();

                frmStockOnHand_Load(this, null);
                
            }
        }

        private void rbOtherGrades_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && !x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }

        private void rbGradeA_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }

        private void comboWhses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown) 
                oCmbo.DroppedDown = true;
        }

        private void comboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void rbDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && !x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Set waiting cursor

            using (var context = new TTI2Entities())
            {
                // Apply user-selected filters
                var stockDataQuery = context.TLCSV_StockOnHand.Where(s => !s.TLSOH_Sold);

                if (QueryParms.Styles.Any())
                {
                    var selectedStyleIds = QueryParms.Styles.Select(st => st.Sty_Id).ToList();
                    stockDataQuery = stockDataQuery.Where(s => selectedStyleIds.Contains(s.TLSOH_Style_FK));
                }

                if (QueryParms.Colours.Any())
                {
                    var selectedColourIds = QueryParms.Colours.Select(c => c.Col_Id).ToList();
                    stockDataQuery = stockDataQuery.Where(s => selectedColourIds.Contains(s.TLSOH_Colour_FK));
                }

                if (QueryParms.Sizes.Any())
                {
                    var selectedSizeIds = QueryParms.Sizes.Select(sz => sz.SI_id).ToList();
                    stockDataQuery = stockDataQuery.Where(s => selectedSizeIds.Contains(s.TLSOH_Size_FK));
                }

                if (QueryParms.Whses.Any())
                {
                    var selectedWhseIds = QueryParms.Whses.Select(w => w.WhStore_Id).ToList();
                    stockDataQuery = stockDataQuery.Where(s => selectedWhseIds.Contains(s.TLSOH_WareHouse_FK));
                }

                var warehouseLookup = context.TLADM_WhseStore
                    .ToDictionary(w => w.WhStore_Id, w => w.WhStore_Description);

                var stockData = stockDataQuery
                    .GroupBy(s => new { s.TLSOH_Style_FK, s.TLSOH_Colour_FK, s.TLSOH_Size_FK, s.TLSOH_Grade, s.TLSOH_WareHouse_FK, s.TLSOH_BoxNumber })
                    .Select(g => new
                    {
                        StyleId = g.Key.TLSOH_Style_FK,
                        ColourId = g.Key.TLSOH_Colour_FK,
                        SizeId = g.Key.TLSOH_Size_FK,
                        Grade = g.Key.TLSOH_Grade,
                        WarehouseId = g.Key.TLSOH_WareHouse_FK,
                        BoxNumber = g.Key.TLSOH_BoxNumber,
                        BoxedQty = g.Sum(x => x.TLSOH_BoxedQty),
                        Weight = g.Sum(x => x.TLSOH_Weight)
                    })
                    .ToList() // Switch to in-memory to apply description
                    .Select(x => new
                    {
                        x.StyleId,
                        x.ColourId,
                        x.SizeId,
                        x.Grade,
                        x.BoxNumber,
                        x.BoxedQty,
                        x.Weight,
                        WarehouseDesc = warehouseLookup.ContainsKey(x.WarehouseId) ? warehouseLookup[x.WarehouseId] : "UNKNOWN"
                    })
                    .OrderBy(x => x.WarehouseDesc)
                    .ThenBy(x => x.StyleId)
                    .ToList();


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("StockOnHand");

                    // Add headers
                    worksheet.Cell(1, 1).Value = "Product Code";
                    worksheet.Cell(1, 2).Value = "Grade";
                    worksheet.Cell(1, 3).Value = "Box Number";
                    worksheet.Cell(1, 4).Value = "Boxed Qty";
                    worksheet.Cell(1, 5).Value = "Weight";
                    worksheet.Cell(1, 6).Value = "Warehouse Desc";

                    int row = 2;
                    string firstProductCode = "UNKNOWN";
                    string warehouse = "";

                    foreach (var item in stockData)
                    {
                        string productCode = "UNKNOWN";
                        
                        using (var sqlConnection = new SqlConnection(connectionString))
                        {
                            sqlConnection.Open();

                            string sql = @"SELECT TOP 1 ProductCode 
                                           FROM TLADM_ProductCodes 
                                           WHERE StyleId = @StyleId AND ColourId = @ColourId AND SizeId = @SizeId";

                            using (var command = new SqlCommand(sql, sqlConnection))
                            {
                                command.Parameters.AddWithValue("@StyleId", item.StyleId);
                                command.Parameters.AddWithValue("@ColourId", item.ColourId);
                                command.Parameters.AddWithValue("@SizeId", item.SizeId);

                                var result = command.ExecuteScalar();
                                if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                                {
                                    productCode = result.ToString().ToUpper();
                                }
                                else
                                {
                                    continue; // Skip if Product Code is missing
                                }
                            }
                        }

                        warehouse = item.WarehouseDesc;
                        if (firstProductCode == "UNKNOWN")
                            firstProductCode = productCode;

                        // Write to Excel
                        worksheet.Cell(row, 1).Value = productCode;
                        worksheet.Cell(row, 2).Value = item.Grade;
                        worksheet.Cell(row, 3).Value = item.BoxNumber;
                        worksheet.Cell(row, 4).Value = item.BoxedQty;
                        worksheet.Cell(row, 5).Value = item.Weight;
                        worksheet.Cell(row, 6).Value = item.WarehouseDesc;
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Save file with timestamp and first product code
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string fileName = $"SOH_{warehouse}_{timestamp}.xlsx";
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    workbook.SaveAs(filePath);

                    Cursor.Current = Cursors.Default; // Reset cursor
                    MessageBox.Show($"Excel export completed!\nFile saved at:\n{filePath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
