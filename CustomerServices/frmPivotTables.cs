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
    public partial class frmPivotTables : Form
    {
        bool formloaded;
        
        CustomerServicesParameters QueryParms;
        Repository repo;
        UserDetails UserD;
        public frmPivotTables(UserDetails ud)
        {
            InitializeComponent();
            repo = new Repository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboWareHouses.CheckStateChanged += new System.EventHandler(this.cmboWhses_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);

            UserD = ud;

            DGVOutput.AllowUserToOrderColumns = false;
            DGVOutput.AllowUserToAddRows = false;
            DGVOutput.AllowUserToDeleteRows = false;
        }

        private void frmPivotTables_Load(object sender, EventArgs e)
        {
            IList<TLADM_WhseStore> WareHouses = null;
            IList<TLADM_CustomerFile> Customers = null;

             DGVOutput.Visible = false; 
             using (var context = new TTI2Entities())
             {
                 if (!UserD._External)
                 {
                     cmboWareHouses.Items.Clear();
                     WareHouses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                     foreach (var WareHouse in WareHouses)
                     {
                         cmboWareHouses.Items.Add(new CustomerServices.CheckComboBoxItem(WareHouse.WhStore_Id, WareHouse.WhStore_Description, false));
                     }

                    cmboCustomers.Items.Clear();
                    Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                     foreach (var Customer in Customers)
                     {
                         cmboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                     }
                 }
                 else
                 {
                     cmboWareHouses.Items.Clear();
                     WareHouses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && !x.WhStore_RePack && x.WhStore_GradeA).OrderBy(x => x.WhStore_Description).ToList();
                     foreach (var WareHouse in WareHouses)
                     {
                         cmboWareHouses.Items.Add(new CustomerServices.CheckComboBoxItem(WareHouse.WhStore_Id, WareHouse.WhStore_Description, false));
                     }

                     cmboCustomers.Items.Clear();
                     var AccessPermitted = context.TLADM_CustomerAccess.Where(x => x.CustAcc_User_Fk == UserD._UserPk).ToList();
                     foreach (var Access in AccessPermitted)
                     {
                         var CustDesc = context.TLADM_CustomerFile.Find(Access.CustAcc_Customer_Fk).Cust_Description;
                         cmboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Access.CustAcc_Customer_Fk, CustDesc, false));

                     }
              
                 }
                
                 cmboStyles.Items.Clear();   
                 var Styles = context.TLADM_Styles.OrderBy(x=>x.Sty_Description).ToList();
                 foreach (var Style in Styles)
                 {
                        cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                 }

                 cmboSizes.Items.Clear();
                 var Sizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                 foreach (var Size in Sizes)
                 {
                     cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Display, false));
                 }

                 cmboColours.Items.Clear();
                 var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                 foreach (var Colour in Colours)
                 {
                     cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                 }
            }
            
            QueryParms = new CustomerServicesParameters();

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
        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomers(item._Pk));

                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            DataTable dt = new DataTable();
            IList<TLCSV_PuchaseOrderDetail> PODetail = new List<TLCSV_PuchaseOrderDetail>();
            IList<TLCSV_StockOnHand> SOHDetail = new List<TLCSV_StockOnHand>();
            List<DATA> SOHGrouped = new List<DATA>();

            BindingSource bindingSource1 = new BindingSource();

            if (oBtn != null && formloaded)
            {
                if (UserD._External)
                {
                    if (QueryParms.Customers.Count == 0)
                    {
                        MessageBox.Show("Please select a customer from the drop down list");
                        return;
                    }

                    if (QueryParms.Whses.Count == 0)
                    {
                        MessageBox.Show("Please select a warehouse fro the drop down list");
                        return;
                    }
                }

                using (var context = new TTI2Entities())
                {
                    DGVOutput.DataSource = null;
                    DGVOutput.Rows.Clear();
                 
                    if (rbAvailable.Checked || rbGrossStock.Checked)
                    {
                        //============================================================
                        // Can be either Gross Stock or Available stock data needed
                        // Start with Gross Stock always
                        //-------------------------------------------------------
                        SOHDetail = repo.GrossSOHQuery(QueryParms).ToList();
                        if (rbAvailable.Checked)
                            // Available Stock only
                            //----------------------------------------------------------
                            SOHDetail = SOHDetail.Where(x => !x.TLSOH_Picked).ToList();

                       
                        if (SOHDetail.Count == 0)
                        {
                            DGVOutput.Visible = false;
                            MessageBox.Show("No records found for selection made");
                            return;
                        }

                        var GroupedData = SOHDetail.GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK, x.TLSOH_Size_FK}).ToList();
                        foreach (var Grouped in GroupedData)
                        {
                            var Sty = Grouped.FirstOrDefault().TLSOH_Style_FK;
                            var Clr = Grouped.FirstOrDefault().TLSOH_Colour_FK;
                            var xSize = Grouped.FirstOrDefault().TLSOH_Size_FK;
                            var BoxQty = Grouped.Sum(x => (int?)x.TLSOH_BoxedQty ?? 0);
                            var Record = SOHGrouped.Find(x => x._StyleFK == Sty && x._ColourFK == Clr && x._SizeFK == xSize);
                            
                            var index = SOHGrouped.IndexOf(Record);

                            if (index < 0)
                            {
                                DATA dd = new DATA();
                                dd._BoxedQty = BoxQty;
                                dd._ColourFK = Clr;
                                dd._SizeFK = xSize;
                                dd._StyleFK = Sty;
                                dd._Total = BoxQty;

                                SOHGrouped.Add(dd);
                            }
                            else
                            {
                                Record._BoxedQty += BoxQty;
                                Record._Total += BoxQty;

                            }
                        }

                        //now ready to display 
                        //-------------------------------------------------
                        dt = new DataTable();
                        DataColumn column;
                        
                        // Create column 1 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Style";
                        dt.Columns.Add(column);

                        // Create column 2 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Colour";
                        dt.Columns.Add(column);
                        // Add the size names to the top of the grid box 
                        //-------------------------------------------------------
                        foreach (var soh in SOHGrouped)
                        {
                            var xSize = context.TLADM_Sizes.Find(soh._SizeFK).SI_Description;
                            if (!dt.Columns.Contains(xSize))
                            {
                                dt.Columns.Add(xSize, typeof(Int32));
                            }
                            else
                                continue;
                        }

                        dt.Columns.Add("Total", typeof(Int32));
                        dt.Columns[dt.Columns.IndexOf("Total")].DefaultValue = 0;

                        foreach (var soh in SOHGrouped)
                        {
                            var xStyle = context.TLADM_Styles.Find(soh._StyleFK).Sty_Description;
                            var xColour = context.TLADM_Colours.Find(soh._ColourFK).Col_Display;
                            var Sty = context.TLADM_Styles.Find(soh._StyleFK);
                            var xSize = string.Empty;

                            if (Sty != null && !Sty.Sty_WorkWear)
                            {
                                xSize = context.TLADM_Sizes.Find(soh._SizeFK).SI_Description;
                            }
                            else
                            {
                                xSize = context.TLADM_Sizes.Find(soh._SizeFK).SI_ContiSize.ToString();
                            }
                            var index = dt.Columns.IndexOf(xSize);

                            var SingleRow = (from Rows in dt.Rows.Cast<DataRow>()
                                        where Rows.Field<String>(0) == xStyle && Rows.Field<String>(1) == xColour 
                                        select Rows).FirstOrDefault();

                            if (SingleRow != null)
                            {
                                SingleRow[index] = soh._BoxedQty;
                                var CurrentTotal = Convert.ToInt32(SingleRow[dt.Columns.IndexOf("Total")]);
                                var ColIndex = dt.Columns.IndexOf("Total");
                                SingleRow[ColIndex] = CurrentTotal + soh._BoxedQty;
                            }
                            else
                            {
                                DataRow TheNewRow = dt.NewRow();
                                TheNewRow[0] = xStyle;
                                TheNewRow[1] = xColour;
                                TheNewRow[index] = soh._BoxedQty;
                                var ColIndex = dt.Columns.IndexOf("Total");
                                TheNewRow[ColIndex] = soh._BoxedQty;
                                dt.Rows.Add(TheNewRow);
                            }
                        }
                        
                        DataView DataV = dt.DefaultView;
                        DataV.Sort = dt.Columns[0].ColumnName + "," + dt.Columns[1].ColumnName;

                        DGVOutput.DataSource = DataV.ToTable();
                        DGVOutput.Visible = true;
                    }
                    else
                    {
                        //=======================================================================
                        //  this section of the code does the outstanding purchase orders
                        //=====================================================================
                        var Data = repo.POQuery(QueryParms);
                        if (Data.Count() == 0)
                        {
                            DGVOutput.Visible = false;
                            MessageBox.Show("No records found for selection made");
                            return;
                        }

                        var GroupedData = Data.GroupBy(x => new { x.TLCUSTO_Style_FK, x.TLCUSTO_Colour_FK , x.TLCUSTO_Size_FK}).ToList();
                        foreach (var Grouped in GroupedData)
                        {
                            var Sty = Grouped.FirstOrDefault().TLCUSTO_Style_FK;
                            var Clr = Grouped.FirstOrDefault().TLCUSTO_Colour_FK;
                            var xSize = Grouped.FirstOrDefault().TLCUSTO_Size_FK;
                            var BoxQty = Grouped.Sum(x => (int?)x.TLCUSTO_Qty ?? 0);
                            var Record = SOHGrouped.Find(x => x._StyleFK == Sty && x._ColourFK == Clr && x._SizeFK == xSize);
                            var index = SOHGrouped.IndexOf(Record);

                            if (index < 0)
                            {
                                DATA dd = new DATA();
                                dd._BoxedQty = BoxQty;
                                dd._ColourFK = Clr;
                                dd._SizeFK = (int)xSize;
                                dd._StyleFK = (int)Sty;
                                dd._Total = BoxQty;

                                SOHGrouped.Add(dd);
                            }
                            else
                            {
                                Record._BoxedQty += BoxQty;
                                Record._Total += BoxQty;
                            }
                        }
                        //now ready to display 
                        //-------------------------------------------------
                        dt = new DataTable();
                        DataColumn column;

                        // Create column 1 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Style";
                        dt.Columns.Add(column);

                        // Create column 2 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "Colour";
                        dt.Columns.Add(column);
                        // Add the size names to the top of the grid box 
                        //-------------------------------------------------------
                        foreach (var soh in SOHGrouped)
                        {
                            var xSize = context.TLADM_Sizes.Find(soh._SizeFK).SI_Description;
                            if (!dt.Columns.Contains(xSize))
                            {
                                dt.Columns.Add(xSize, typeof(Int32));
                            }
                            else
                                continue;
                        }

                        dt.Columns.Add("Total", typeof(Int32));
                        dt.Columns[dt.Columns.IndexOf("Total")].DefaultValue = 0;

                        foreach (var soh in SOHGrouped)
                        {
                            var xStyle = context.TLADM_Styles.Find(soh._StyleFK).Sty_Description;
                            var xColour = context.TLADM_Colours.Find(soh._ColourFK).Col_Display;
                            var xSize = context.TLADM_Sizes.Find(soh._SizeFK).SI_Description;
                            var index = dt.Columns.IndexOf(xSize);
                            
                            var SingleRow = (from Rows in dt.Rows.Cast<DataRow>()
                                             where Rows.Field<String>(0) == xStyle && Rows.Field<String>(1) == xColour
                                             select Rows).FirstOrDefault();

                            if (SingleRow != null)
                            {
                                SingleRow[index] = soh._BoxedQty;
                                var CurrentTotal = Convert.ToInt32(SingleRow[dt.Columns.IndexOf("Total")]);
                                var ColIndex = dt.Columns.IndexOf("Total");
                                SingleRow[ColIndex] = CurrentTotal + soh._BoxedQty;
                                
                            }
                            else
                            {
                                var TheNewRow = dt.NewRow();
                                TheNewRow[0] = xStyle;
                                TheNewRow[1] = xColour;
                                TheNewRow[index] = soh._BoxedQty;
                                var ColIndex = dt.Columns.IndexOf("Total");
                                TheNewRow[ColIndex] = soh._BoxedQty;
                                dt.Rows.Add(TheNewRow);
                            }
                            
                        }

                        DataView DataV = dt.DefaultView;
                        DataV.Sort = dt.Columns[0].ColumnName + "," + dt.Columns[1].ColumnName;
                        DGVOutput.DataSource = DataV.ToTable();
                        DGVOutput.Visible = true;
                    }
                }
                
               
            }
        }

        private void DGVOutput_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                DataGridView oDgv = sender as DataGridView;
                int Style_FK = 0;
                int Colour_FK = 0;
                int Size_FK = 0;
                BindingSource bindingSource1 = new BindingSource();
                if (oDgv != null && formloaded && e.Button.ToString() == "Right")
                {
                    oDgv.Visible = false;
                    this.Text = "Pivot summary tables";
                }
                else
                {
                    if (!rbOutStanding.Checked)
                    {
                        var CurrentRow = oDgv.CurrentRow;
                        // this ia a bit 'ropey' but will have to do...
                     
                        using (var context = new TTI2Entities())
                        {
                            // 1st get the style 
                            //----------------------------------------------
                            string StyleName = CurrentRow.Cells[0].Value.ToString();
                            if (!String.IsNullOrEmpty(StyleName))
                            {
                                Style_FK = context.TLADM_Styles.Where(x => x.Sty_Description.Contains(StyleName)).FirstOrDefault().Sty_Id;
                            }
                            // 2nd  get the colour
                            //--------------------------------------
                            string ColourName = CurrentRow.Cells[1].Value.ToString();
                            if (!String.IsNullOrEmpty(ColourName))
                            {
                                Colour_FK = context.TLADM_Colours.Where(x => x.Col_Display.Contains(ColourName)).FirstOrDefault().Col_Id;
                            }

                            DataTable dt = new DataTable();
                            dt.Columns.Add("WareHouse", typeof(string));
                            dt.Columns.Add("Box Number", typeof(string));
                            dt.Columns.Add("Boxed Qty", typeof(int));
                            dt.Columns.Add("Boxed Weight", typeof(decimal));

                            for (int i = 2; i < DGVOutput.Columns.Count; i++)
                            {
                                // 3rd get the size 
                                //-------------------------------------------------------
                                String SizeName = DGVOutput.Columns[i].Name;
                                if (!String.IsNullOrEmpty(SizeName))
                                {
                                    Size_FK = context.TLADM_Sizes.Where(x => x.SI_Description == SizeName).FirstOrDefault().SI_id;
                                }

                                if (Style_FK != 0 && Colour_FK != 0 && Size_FK != 0)
                                {
                                    var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == Style_FK && x.TLSOH_Colour_FK == Colour_FK && x.TLSOH_Size_FK == Size_FK).ToList();
                                    foreach (var Record in Existing)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr[0] = context.TLADM_WhseStore.Find(Record.TLSOH_WareHouse_FK).WhStore_Description;
                                        dr[1] = Record.TLSOH_BoxNumber;
                                        dr[2] = Record.TLSOH_BoxedQty;
                                        dr[3] = Math.Round(Record.TLSOH_Weight,1);

                                        dt.Rows.Add(dr);
                                    }
                                  
                                }
                            }

                            DGVOutput.DataSource = null;
                            bindingSource1.DataSource = dt;
                        
                            DGVOutput.DataSource = bindingSource1;
                            DGVOutput.Columns[0].Width = 125;
                        }
                    }
                }
        }

        private void cmboWareHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
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

        private void rbGrossStock_CheckedChanged(object sender, EventArgs e)
        {
            frmPivotTables_Load(this, null);
        }

        private void rbAvailable_CheckedChanged(object sender, EventArgs e)
        {
            frmPivotTables_Load(this, null);
        }

        private void rbOutStanding_CheckedChanged(object sender, EventArgs e)
        {
            frmPivotTables_Load(this, null);
        }
    }

    public struct DATA
    {
        public int _StyleFK;            // Style FK
        public int _ColourFK;           // Colour FK
        public int _SizeFK;             // Size FK     
        public int _BoxedQty;           // dataGridView Trim Record Key
        public int _Total;
        public DATA(int StyleKey, int ColourKey, int SizeKey, int BoxedQty, int Total)
        {
            this._StyleFK = StyleKey;
            this._ColourFK = ColourKey;
            this._SizeFK = SizeKey;
            this._BoxedQty = BoxedQty;
            this._Total = Total;
        }
    }
}
