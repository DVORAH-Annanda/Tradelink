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
using LinqKit;
using EntityFramework.Extensions;
using System.IO;
using System.Threading;

namespace ProductionPlanning
{
    public partial class frmSelFinishedGoods : Form
    {
        PPSRepository repo;
        bool formloaded;
        ProdQueryParameters QueryParms;
        Util core;
        bool[] Selected;
        UserDetails _UserD;

        List<TLPPS_Replenishment> PPS;
        BackgroundWorker BGW;


        public frmSelFinishedGoods(UserDetails ud)
        {
            InitializeComponent();
            _UserD = ud;
            if(_UserD._External)
            {
                rbFinishedGoods.Enabled = false;
                rbFabric.Enabled = false;
            }
        }

        private void frmSelFinishedGoods_Load(object sender, EventArgs e)
        {
            formloaded = false;
            repo = new PPSRepository();
            QueryParms = new ProdQueryParameters();

            PBar1.Visible = false;

            core = new Util();
                      
            Selected = core.PopulateArray(7, false);

            using (var context = new TTI2Entities())
            {
                var ExistingStyles = context.TLADM_Styles.OrderBy(x=>x.Sty_Description).ToList();
                foreach (var record in ExistingStyles)
                {
                    cmboStyles.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Sty_Id, record.Sty_Description, false));
                }

                var ExistingColours = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach(var record in ExistingColours)
                {
                    cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
                }
                
                var ExistingSizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).ToList();
                foreach( var record in ExistingSizes)
                {
                    cmboSizes.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.SI_id, record.SI_Description, false));
                }
                if (!_UserD._External)
                {
                    var ExistingCustomers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                    foreach (var record in ExistingCustomers)
                    {
                        cmboCustomer.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Cust_Pk, record.Cust_Description, false));
                    }
                }
                else
                {
                    var AccessPermitted = context.TLADM_CustomerAccess.Where(x => x.CustAcc_User_Fk == _UserD._UserPk).ToList();
                    foreach (var Access in AccessPermitted)
                    {
                        var CustDesc = context.TLADM_CustomerFile.Find(Access.CustAcc_Customer_Fk).Cust_Description;
                        cmboCustomer.Items.Add(new ProductionPlanning.CheckComboBoxItem(Access.CustAcc_Customer_Fk, CustDesc, false));
                    }
                }

            }

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboStyles.CheckStateChanged  += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged   += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            this.cmboCustomer.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);
            formloaded = true;
        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomer(item._Pk));

                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        //private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        //{

        //    if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
        //    {
        //        ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
        //        if (item.CheckState)
        //        {
        //             QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

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
            if (sender is ProductionPlanning.CheckComboBoxItem item && formloaded)
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
                            if (clr != null && !cmboColours.Items.Cast<ProductionPlanning.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
                                cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
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
                                    cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
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
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
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
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
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
            IList<TLCSV_PuchaseOrderDetail> PODetail = new List<TLCSV_PuchaseOrderDetail>();

        
             if (oBtn != null && formloaded)
            {
                // Display the ProgressBar control.
                PBar1.Visible = true;
                // Set Minimum to 1 
                PBar1.Minimum = 1;
                // Set the initial value of ProgessBar
                PBar1.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                PBar1.Step = 1;

                List<PPOrdersDATA> OutOrders = new List<PPOrdersDATA>();
                List<PPStockDATA> SOHStock = new List<PPStockDATA>();

                IList<TLADM_Griege> _Qualities = new List<TLADM_Griege>();
                IList<TLADM_Styles> _Styles = new List<TLADM_Styles>();
                IList<TLADM_Colours> _Colours = new List<TLADM_Colours>();
                IList<TLADM_Sizes> _Sizes = new List<TLADM_Sizes>();

                //NB
                PPSRepository repo = new PPSRepository();
                core = new Util();

                //============================================================
                //---------Define the datatable (Dataframe - pandas)
                //=================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn[] keys = new DataColumn[3];
                DataColumn column;

                //------------------------------------------------------
                // Create column 1. // This is the Style
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Col0";
                dt.Columns.Add(column);
                keys[0] = column;

                //-----------------------------------------------------------
                // Create column 2. // This is the Colour
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Col1";
                dt.Columns.Add(column);
                keys[1] = column;

                //-----------------------------------------------------------
                // Create column 3. // This is the Size 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Col2";
                dt.Columns.Add(column);
                keys[2] = column;

                //--------------------------------------------------------------
                dt.PrimaryKey = keys;
                //-------------------------------------------------------------------------
                if (rbFinishedGoods.Checked)
                {
                    PPS = repo.PPSQuery(QueryParms).ToList();
                    if (PPS.Count != 0)
                    {
                        PBar1.Maximum = PPS.Count;
                    }
                    else
                    {
                        MessageBox.Show("No valid PPS Master Records found for selection made");
                        return;
                    }
                }

                DataColumnCollection columns = dt.Columns;
                //-----------------------------------------------------------------------------
                // Set Up Customer Columns
                //-------------------------------------------------------------
                var Customers = core.CurrentCustomers(rbFinishedGoods.Checked).ToList();
                foreach (var Customer in Customers)
                {
                    if (QueryParms.Customers.Count != 0)
                    {
                        var IsNeed = QueryParms.Customers.Where(x => x.Cust_Pk == Customer.Key).FirstOrDefault();
                        if (IsNeed == null)
                            continue;
                    }

                    if (!columns.Contains(Customer.Value))
                    {
                        try
                        {
                            dt.Columns.Add(Customer.Value, typeof(int));
                            dt.Columns[Customer.Value].DefaultValue = 0;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                }

                //------------------------------------------
                // Have to add a Total column 
                //-------------------------------------------------------
                dt.Columns.Add("Order Total", typeof(int));
                dt.Columns["Order Total"].DefaultValue = 0;
                columns = dt.Columns;

                if (rbFinishedGoods.Checked)
                {
                    var Whses = core.CurrentWareHouses().ToList();
                    foreach (var Whse in Whses)
                    {

                        if (!columns.Contains(Whse))
                        {
                            try
                            {
                                dt.Columns.Add(Whse, typeof(int));
                                dt.Columns[Whse].DefaultValue = 0;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
                }
                //------------------------------------------
                // Have to add a Total column
                //-------------------------------------------------------
                dt.Columns.Add("Stock Total", typeof(int));
                dt.Columns["Stock Total"].DefaultValue = 0;
                //------------------------------------------

                //------------------------------------------
                // Difference
                //-------------------------------------------------------
                dt.Columns.Add("Difference", typeof(int));
                dt.Columns["Difference"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                if (rbFinishedGoods.Checked)
                {
                    dt.Columns.Add("Expected Units DO", typeof(int));
                    dt.Columns["Expected Units DO"].DefaultValue = 0;
                    //-------------------------------------------------------------------------------------------------------------
                    //08/01/2018 Heath Wolmaraans / Dave Ledgett changes to Algorithm
                    // 3 Possible new scenarios 
                    // A : Dyebatch created pieces selected but not transfered to the dyehouse
                    // B : DyeBatch created pieces selected transferred to dyehouse but not allocated
                    // Scenerio A + B will be known as preparation 
                    dt.Columns.Add("Expected Units - Dyeing Prep", typeof(int));
                    dt.Columns["Expected Units - Dyeing Prep"].DefaultValue = 0;
                    //  C : Dyebatch created pieces selected transfered to dyehouse and allocated to a machine
                    //      but has not exited the Quarrantine store 
                    // Scenario C : Will be known as WIP (Work in Process)
                    dt.Columns.Add("Expected Units - WIP Dyeing", typeof(int));
                    dt.Columns["Expected Units - WIP Dyeing"].DefaultValue = 0;
                    //-------------------------------------------------------------------------------------------------------------
                    dt.Columns.Add("Expected Units - Fabric Quarantine Store", typeof(int));
                    dt.Columns["Expected Units - Fabric Quarantine Store"].DefaultValue = 0;
                    dt.Columns.Add("Expected Units - Fabric Store", typeof(int));
                    dt.Columns["Expected Units - Fabric Store"].DefaultValue = 0;
                    //-------------------------------------------------------------------------------------------------------------
                }
                else
                {
                    dt.Columns.Add("Expected kgs DO", typeof(int));
                    dt.Columns["Expected kgs DO"].DefaultValue = 0;
                    dt.Columns.Add("Expected Kgs - Dyeing Prep", typeof(int));
                    dt.Columns["Expected Kgs - Dyeing Prep"].DefaultValue = 0;
                    dt.Columns.Add("Expected Kgs - WIP Dyeing", typeof(int));
                    dt.Columns["Expected Kgs - WIP Dyeing"].DefaultValue = 0;
                    //-------------------------------------------------------------------------------------------------------------
                    dt.Columns.Add("Expected Kgs - Fabric Quarantine Store", typeof(int));
                    dt.Columns["Expected Kgs - Fabric Quarantine Store"].DefaultValue = 0;
                    dt.Columns.Add("Expected Kgs - Fabric Store", typeof(int));
                    dt.Columns["Expected Kgs - Fabric Store"].DefaultValue = 0;
                }
                dt.Columns.Add("Expected Units - WIP Cutting", typeof(int));
                dt.Columns["Expected Units - WIP Cutting"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Expected Units - CUT Panel Store", typeof(int));
                dt.Columns["Expected Units - CUT Panel Store"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Expected Units - CMT Store (Receipt Cage)", typeof(int));
                dt.Columns["Expected Units - CMT Store (Receipt Cage)"].DefaultValue = 0;
                dt.Columns.Add("Expected Units - CMT WIP", typeof(int));
                dt.Columns["Expected Units - CMT WIP"].DefaultValue = 0;
                dt.Columns.Add("Expected Units - CMT Store (Despatch Cage)", typeof(int));
                dt.Columns["Expected Units - CMT Store (Despatch Cage)"].DefaultValue = 0;
                //------------------------------------------
                // Have to add a Total column 
                //-------------------------------------------------------
                dt.Columns.Add("WIP Total", typeof(int));
                dt.Columns["WIP Total"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Avg Week Sales", typeof(int));
                dt.Columns["Avg Week Sales"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Re Order Level Sales", typeof(int));
                dt.Columns["Re Order Level Sales"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Re Order Qty Total", typeof(int));
                dt.Columns["Re Order Qty Total"].DefaultValue = 0;
                //-------------------------------------------------------------------------------------------------------------
                dt.Columns.Add("Expected Dye Qty", typeof(int));
                dt.Columns["Expected Dye Qty"].DefaultValue = 0;
                //-----------------------------------------------------------------------------------
                // Need to add columns to accomodate the customer outstanding orders by month
                //------------------------------------------------------------------
                string MthDesig;
                int Month = 1;
                do
                {
                    MthDesig = "C" + Month.ToString().PadLeft(2, '0');

                    dt.Columns.Add(MthDesig, typeof(int));
                    dt.Columns[MthDesig].DefaultValue = 0;
                   
                } while (++Month < 13);
            
                //-----------------------------------------------------------------------------------
                // Need to add columns to accomodate the customer sales on a 12 month rolling basis
                //------------------------------------------------------------------
                Month = 1;
                do
                {
                    MthDesig = "S" + Month.ToString().PadLeft(2, '0');

                    dt.Columns.Add(MthDesig, typeof(int));
                    dt.Columns[MthDesig].DefaultValue = 0;

                } while (++Month < 13);
                //-----------------------------------------------------------------------------------
                var OTIndex = dt.Columns.IndexOf("Order Total");
                var STIndex = dt.Columns.IndexOf("Stock Total");
                var DiffIndex = dt.Columns.IndexOf("Difference");
                var WIPIndex = dt.Columns.IndexOf("WIP Total");
                var DyeIndex = dt.Columns.IndexOf("Expected Dye Qty");
                //--------------------------------------------------------------------------------------------
                int ColIndex;
               
                //-------------------------------------------------------------------------------------------------------------
                // Now to get down to work ..... 1st Task is to calculate the Total outstanding orders for a particular style, colour, size combination
                //----------------------------------------------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    _Styles = context.TLADM_Styles.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    _Sizes = context.TLADM_Sizes.ToList();
                    _Qualities = context.TLADM_Griege.ToList();
                    //---------------------------------------------------------------
                    // This variable is used in the 1st Task 
                    //--------------------------------------------------------------
                    if (!_UserD._External)
                    {
                        PODetail = (from T1 in context.TLCSV_PurchaseOrder
                                    join T2 in context.TLCSV_PuchaseOrderDetail on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                    where !T1.TLCSVPO_Closeed && !T2.TLCUSTO_Closed
                                    select T2).ToList();
                    }
                    else
                    {
                        var AccessPermitted = context.TLADM_CustomerAccess.Where(x => x.CustAcc_User_Fk == _UserD._UserPk).ToList();
                        if (AccessPermitted != null)
                        {
                            var Customer_Pk = AccessPermitted.FirstOrDefault().CustAcc_Customer_Fk;
                            //first retrieve open orders for a specific customer and send to list
                            PODetail = (from T1 in context.TLCSV_PurchaseOrder
                                        join T2 in context.TLCSV_PuchaseOrderDetail on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                        where !T1.TLCSVPO_Closeed && !T2.TLCUSTO_Closed && T1.TLCSVPO_Pk == Customer_Pk
                                        select T2).ToList();
                        }
                    }

                    if (rbFinishedGoods.Checked)
                    {
                        //---------------------------------------------------------------
                        // This variable is used in the 2nd Task 
                        // Getting stock on hand that is not picked not write off not returned not split, but is A
                        //--------------------------------------------------------------
                        var SOH = context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked
                                                                    && x.TLSOH_Is_A
                                                                    && !x.TLSOH_Write_Off
                                                                    && !x.TLSOH_Returned
                                                                    && !x.TLSOH_Split).ToList();

                        foreach (var Item in PPS)
                        {
                            //==================================
                            //Need to create a record
                            //==========================================
                            DataRow Row = dt.NewRow();

                            Row[0] = Item.TLREP_Style_FK;
                            Row[1] = Item.TLREP_Colour_FK;
                            Row[2] = Item.TLREP_Size_FK;

                            PBar1.PerformStep();

                            //---------------------------------------------------------------
                            // This variable is used in the 9th (a) and 9(b)  tasks-- CMT 
                            //--------------------------------------------------------------
                            var CMTPanelStore = from LI in context.TLCMT_LineIssue
                                                join CS in context.TLCUT_CutSheet on LI.TLCMTLI_CutSheet_FK equals CS.TLCutSH_Pk 
                                                join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                                                join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                                                where LI.TLCMTLI_IssuedToLine == false && LI.TLCMTLI_WorkCompleted == false
                                                && CR.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && CR.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && CRD.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK
                                                select new { CR.TLCUTSHR_Style_FK, CR.TLCUTSHR_Colour_FK, CRD.TLCUTSHRD_Size_FK, CRD.TLCUTSHRD_BundleQty, CRD.TLCUTSHRD_RejectQty };

                            var CMTWIP = from LI in context.TLCMT_LineIssue
                                         join CS in context.TLCUT_CutSheet on LI.TLCMTLI_CutSheet_FK equals CS.TLCutSH_Pk
                                         join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                                         join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                                         where LI.TLCMTLI_IssuedToLine == true && LI.TLCMTLI_WorkCompleted == false
                                         && CR.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && CR.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && CRD.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK
                                         select new { CR.TLCUTSHR_Style_FK, CR.TLCUTSHR_Colour_FK, CRD.TLCUTSHRD_Size_FK, CRD.TLCUTSHRD_BundleQty, CRD.TLCUTSHRD_RejectQty };
                            
                            //===============================================================
                            //1st step is to get all the outstandings orders at this point in time
                            //    for this style, colour, size combination and the order line must not be closed as well as the order itself
                            //============================================================================
                            var Orders = PODetail.Where(x => x.TLCUSTO_Style_FK == Item.TLREP_Style_FK && x.TLCUSTO_Colour_FK == Item.TLREP_Colour_FK && x.TLCUSTO_Size_FK == Item.TLREP_Size_FK);
                            if (Orders != null)
                            {
                                var GrpByCustomer = Orders.GroupBy(x => x.TLCUSTO_Customer_FK);
                                foreach (var Grouped in GrpByCustomer)
                                {

                                    TLCSV_PuchaseOrderDetail Order = Grouped.FirstOrDefault(); // Grouped.FirstOrDefault();
                                    var CustDetail = context.TLADM_CustomerFile.Find(Order.TLCUSTO_Customer_FK);
                                    ColIndex = dt.Columns.IndexOf(CustDetail.Cust_Code);

                                    var QtyOrdered = Grouped.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0; //  LineOrder.TLCUSTO_Qty;
                                    var AllReadySold = Grouped.Sum(x => (int?)x.TLCUSTO_QtyDelivered_ToDate) ?? 0; // LineOrder.TLCUSTO_QtyDelivered_ToDate;  //  context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == LineOrder.TLCUSTO_Pk && x.TLSOH_Sold).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    var AllReadyPicked = Grouped.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0; //  LineOrder.TLCUSTO_QtyPicked_ToDate;     //  context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == LineOrder.TLCUSTO_Pk && !x.TLSOH_Sold && x.TLSOH_Picked).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    var Nett = QtyOrdered - AllReadyPicked;
                                    if (Nett > 0 && ColIndex >= 0)
                                    {
                                        Row[ColIndex] = Row.Field<int>(ColIndex) + Nett;
                                        Row[OTIndex] = Row.Field<int>(OTIndex) + Nett;  //Row.Field<int>(ColIndex);
                                    }

                                }
                            }
                            else
                            {
                                ColIndex = dt.Columns.IndexOf("Dummy Order");
                                if (ColIndex != 0)
                                {
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + 0;
                                    Row[OTIndex] = Row.Field<int>(OTIndex) + 0;  //Row.Field<int>(ColIndex);
                                }
                            }

                            //-------------------------------------------------------------------------------------------------------------
                            // 2nd Task is to calculate the Total Stock On Hand for a particular style, colour, size combination
                            //----------------------------------------------------------------------------------------------------
                            var ItemSOH = SOH.Where(x => x.TLSOH_Style_FK == Item.TLREP_Style_FK && x.TLSOH_Colour_FK == Item.TLREP_Colour_FK && x.TLSOH_Size_FK == Item.TLREP_Size_FK).GroupBy(x => x.TLSOH_WareHouse_FK);
                            foreach (var Grouped in ItemSOH)
                            {
                                TLCSV_StockOnHand Stock = new TLCSV_StockOnHand();
                                Stock = Grouped.FirstOrDefault();

                                //============================================================
                                //Now check whether the WareHouse "exists" on the datatable
                                //==========================================================================
                                var WhseDetail = context.TLADM_WhseStore.Find(Stock.TLSOH_WareHouse_FK);

                                ColIndex = dt.Columns.IndexOf(WhseDetail.WhStore_Code);
                                if (ColIndex != -1)
                                {
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Grouped.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    Row[STIndex] = Row.Field<int>(STIndex) + Row.Field<int>(ColIndex);
                                }
                            }

                            //--------------------------------------------------------------------
                            // We need to find the difference Stock - Orders
                            //------------------------------------------------------------------------
                            Row[DiffIndex] = Row.Field<int>(STIndex) - Row.Field<int>(OTIndex);

                            //---------------------------------------------------------------
                            // 3rd Task -- Dye Orders 
                            //--------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units DO");
                            var DyeOrders = context.TLDYE_DyeOrder.Where(x => x.TLDYO_Style_FK == Item.TLREP_Style_FK && x.TLDYO_Colour_FK == Item.TLREP_Colour_FK && !x.TLDYO_Closed).ToList();
                            foreach (var DyeOrder in DyeOrders)
                            {
                                //------------------------------------------------------
                                // Because of the concept of multi markers we have to calculate a ratio based on the the number of sizes
                                // originally entered 
                                //------------------------------------------------------------------
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;

                                var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                if (DyeOrderDetail != null)
                                {
                                    Ratios = core.ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);

                                    var Entry = Ratios.FirstOrDefault(x => x.Key == Item.TLREP_Size_FK);
                                    if (Entry.Key == 0)
                                    {
                                        //this item Size is not relevant to this dye order
                                        //====================================================
                                        continue;
                                    }

                                    decimal FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                    decimal FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                    decimal TotalWeight = (decimal)DyeOrderDetail.TLDYOD_Kgs;


                                    var DyeBatches = from T1 in context.TLDYE_DyeBatch
                                                     join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                                     where !T1.DYEB_CommissinCust && T1.DYEB_DyeOrder_FK == DyeOrder.TLDYO_Pk && T2.DYEBD_BodyTrim
                                                     select T2;

                                    int ExpectedUnits = 0;
                                    if (DyeBatches.Count() != 0)
                                    {
                                        try
                                        {
                                            TotalWeight -= DyeBatches.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                            if (TotalWeight > 0)
                                            {
                                                ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                                var TotalR = Ratios.Sum(x => x.Value);
                                                ExpectedUnits = Convert.ToInt32((Entry.Value / TotalR) * ExpectedUnits);
                                            }
                                            else
                                                ExpectedUnits = 0;
                                        }
                                        catch (Exception ex)
                                        {
                                            ExpectedUnits = 0;
                                            //MessageBox.Show(ex.ToString());

                                        }
                                    }
                                    else
                                    {
                                        ExpectedUnits = DyeOrderDetail.TLDYOD_Units;
                                        var TotalR = Ratios.Sum(x => x.Value);
                                        ExpectedUnits = Convert.ToInt32((Entry.Value / TotalR) * ExpectedUnits);
                                    }

                                    //----------------------------------------------
                                    //We have to allow for a loss factor of 5%
                                    //---------------------------------------------
                                    ExpectedUnits = Convert.ToInt32(ExpectedUnits * 0.95);
                                    ExpectedUnits = Convert.ToInt32(ExpectedUnits * 0.95);
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + ExpectedUnits;
                                }
                            }

                            // 4th Task -- DyeBatching (Preparation)
                            //--------------------------------------------------------------------
                            //----------------------------------------------------------------
                            // Because Dye Batches have no specified Style, Colour and size Key 
                            // We have to get it from the respective Dye Orders 
                            //---------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - Dyeing Prep");
                            var DOrders = from T1 in context.TLDYE_DyeOrder
                                          join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                                          join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                          where !T2.DYEB_CommissinCust && T1.TLDYO_Style_FK == Item.TLREP_Style_FK && T1.TLDYO_Colour_FK == Item.TLREP_Colour_FK
                                          && !T2.DYEB_Closed && !T2.DYEB_Allocated && T3.DYEBD_BodyTrim
                                          select new { T1.TLDYO_Pk, T2.DYEB_Pk, T2.DYEB_Greige_FK, T3.DYEBD_GreigeProduction_Weight };

                            var DOrdersx = DOrders.GroupBy(x => x.TLDYO_Pk);

                            foreach (var DOOrder in DOrdersx)
                            {
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;

                                var Order = DOOrder.FirstOrDefault();
                                var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == Order.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                
                                if (DyeOrderDetail != null)
                                {
                                    Ratios = core.ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                                    var Entry = Ratios.FirstOrDefault(x => x.Key == Item.TLREP_Size_FK);
                                    if (Entry.Key == 0)
                                    {
                                        continue;
                                    }

                                    var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                    var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                    var ExpectedUnits = 0;

                                    try
                                    {
                                        var TotalWeight = DOOrder.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                        ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                    }
                                    catch (Exception ex)
                                    {
                                        ExpectedUnits = 0;
                                        //MessageBox.Show(ex.ToString());
                                    }

                                    var Total = Ratios.Sum(x => x.Value);
                                    var Answer = Convert.ToInt32((Entry.Value / Total) * ExpectedUnits);
                                    // We dont need to allow for any losses
                                    //------------------------------------------------------------
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Answer;
                                }
                            }
                            //---------------------------------
                            // Bring the Prep Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                            //-------------------------------------------------------------------
                            // 5th Task  -- DyeBatching (WIP) 
                            //----------------------------------------------------------------
                            // Because Dye Batches have no specified Style, Colour and size Key 
                            // We have to get it from the respective Dye Orders 
                            //---------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - WIP Dyeing");
                            DOrders = from T1 in context.TLDYE_DyeOrder
                                      join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                                      join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                      where !T2.DYEB_CommissinCust && T1.TLDYO_Style_FK == Item.TLREP_Style_FK && T1.TLDYO_Colour_FK == Item.TLREP_Colour_FK
                                      && !T2.DYEB_Closed && T2.DYEB_Allocated && !T2.DYEB_OutProcess && T3.DYEBD_BodyTrim
                                      select new { T1.TLDYO_Pk, T2.DYEB_Pk, T2.DYEB_Greige_FK, T3.DYEBD_GreigeProduction_Weight };

                            DOrdersx = DOrders.GroupBy(x => x.TLDYO_Pk);

                            foreach (var DOOrder in DOrdersx)
                            {
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;

                                var Order = DOOrder.FirstOrDefault();

                                //MessageBox.Show(Order.TLDYO_Pk.ToString(), "DyeBatching");
                                
                                var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == Order.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                if (DyeOrderDetail != null)
                                {
                                    Ratios = core.ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                                    var Entry = Ratios.FirstOrDefault(x => x.Key == Item.TLREP_Size_FK);
                                    if (Entry.Key == 0)
                                    {
                                        continue;
                                    }

                                    var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                    var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                    var ExpectedUnits = 0;

                                    try
                                    {
                                        var TotalWeight = DOOrder.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                        ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                    }
                                    catch (Exception ex)
                                    {
                                        ExpectedUnits = 0;
                                        //MessageBox.Show(ex.ToString());
                                    }

                                    var Total = Ratios.Sum(x => x.Value);
                                    var Answer = Convert.ToInt32((Entry.Value / Total) * ExpectedUnits);

                                    //----------------------------------------------
                                    //We have to allow for a loss factor of 5%
                                    //---------------------------------------------
                                    Answer = Convert.ToInt32(Answer * 0.95);
                                    Answer = Convert.ToInt32(Answer * 0.95);
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Answer;
                                }
                            }

                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //******************************************************
                            // 6th Task  -- DyeBatching (Quarantine) 
                            //******************************************************

                            ColIndex = dt.Columns.IndexOf("Expected Units - Fabric Quarantine Store");
                            var DBOrders = from T1 in context.TLDYE_DyeOrder
                                           join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                                           join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                           join T4 in context.TLADM_WhseStore on T3.DYEBO_CurrentStore_FK equals T4.WhStore_Id
                                           where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && !T2.DYEB_FabicSales  && T3.DYEBD_BodyTrim && T4.WhStore_Quarantine
                                           && !T3.DYEBO_CutSheet && !T3.DYEBO_Rejected && !T3.DYEBO_Sold && !T3.DYEBO_WriteOff
                                           && T1.TLDYO_Style_FK == Item.TLREP_Style_FK && T1.TLDYO_Colour_FK == Item.TLREP_Colour_FK
                                           select new { T1.TLDYO_Pk, T2.DYEB_Pk, T3.DYEBO_Nett };

                            var DBOrdersx = DBOrders.GroupBy(x => x.TLDYO_Pk);
                            foreach (var Order in DBOrdersx)
                            {
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;
                                var FirstOrder = Order.FirstOrDefault();

                                var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == FirstOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                if (DyeOrderDetail != null)
                                {
                                    Ratios = core.ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                                    var Entry = Ratios.FirstOrDefault(x => x.Key == Item.TLREP_Size_FK);
                                    if (Entry.Key != 0)
                                    {
                                        var ExpectedUnits = 0;
                                        var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                        var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                        var TotalWeight = 0.00M;

                                        try
                                        {
                                            TotalWeight = Order.Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                                            ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExpectedUnits = 0;
                                            //MessageBox.Show(ex.ToString());
                                        }

                                        int CurrentValue = 0;

                                        if (!string.IsNullOrEmpty(Row[ColIndex].ToString()))
                                            CurrentValue = Convert.ToInt32(Row[ColIndex].ToString());

                                        var Total = Ratios.Sum(x => x.Value);
                                        var Answer = Convert.ToInt32((Entry.Value / Total) * ExpectedUnits);
                                        Answer = Convert.ToInt32(Answer * 0.95);
                                        Row[ColIndex] = CurrentValue + Answer;
                                    }
                                }
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                            //-----------------------------------------------------------------
                            // 7th Task  -- DyeBatching (FABRIC Store) 
                            //--------------------------------------------------------------
                            // Because Dye Batches have no specified Style, Colour and size Key 
                            // We have to get it from the respective Dye Orders 
                            //---------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - Fabric Store");
                            DBOrders = from T1 in context.TLDYE_DyeOrder
                                       join T2 in context.TLDYE_DyeBatch
                                       on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                                       join T3 in context.TLDYE_DyeBatchDetails 
                                       on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                       where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && !T2.DYEB_FabicSales
                                       && !T3.DYEBO_Sold && T3.DYEBD_BodyTrim && T3.DYEBO_QAApproved && !T3.DYEBO_Rejected && !T3.DYEBO_WriteOff 
                                       && !T3.DYEBO_CutSheet
                                       && T1.TLDYO_Style_FK == Item.TLREP_Style_FK && T1.TLDYO_Colour_FK == Item.TLREP_Colour_FK
                                       select new { T1.TLDYO_Pk, T2.DYEB_Pk, T3.DYEBO_Nett };

                            DBOrdersx = DBOrders.GroupBy(x => x.TLDYO_Pk);
                            foreach (var Order in DBOrdersx)
                            {
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;
                                var FirstOrder = Order.FirstOrDefault();

                                var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == FirstOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                if (DyeOrderDetail != null)
                                {
                                    Ratios = core.ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                                    var Entry = Ratios.FirstOrDefault(x => x.Key == Item.TLREP_Size_FK);
                                    if (Entry.Key != 0)
                                    {
                                        var ExpectedUnits = 0;
                                        var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                        var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                        var TotalWeight = 0.00M;

                                        try
                                        {
                                            TotalWeight = Order.Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                                            ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExpectedUnits = 0;
                                            //MessageBox.Show(ex.ToString());
                                        }

                                        int CurrentValue = 0;

                                        if (!string.IsNullOrEmpty(Row[ColIndex].ToString()))
                                            CurrentValue = Convert.ToInt32(Row[ColIndex].ToString());

                                        var Total = Ratios.Sum(x => x.Value);
                                        var Answer = Convert.ToInt32((Entry.Value / Total) * ExpectedUnits);
                                        Answer = Convert.ToInt32(Answer * 0.95);
                                        Row[ColIndex] = CurrentValue + Answer;
                                    }
                                }
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                            //-------------------------------------------------------------------------------------------------------------
                            // 8th Task Expected Units in Cutting WIP 
                            //----------------------------------------------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - WIP Cutting");
                            var CutSheets = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted && x.TLCutSH_Styles_FK == Item.TLREP_Style_FK && x.TLCutSH_Colour_FK == Item.TLREP_Colour_FK && !x.TLCutSH_Closed).ToList();
                            foreach (var CutSheet in CutSheets)
                            {
                                var CutSheetDetails = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                                if (CutSheetDetails.Count() != 0)
                                {
                                    var ExpectedUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk && x.TLCUTE_Size_FK == Item.TLREP_Size_FK).ToList();
                                    if (ExpectedUnits.Count != 0)
                                    {
                                        int BoxedUnits = ExpectedUnits.Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0;
                                        Row[ColIndex] = Row.Field<int>(ColIndex) + BoxedUnits;
                                    }
                                }
                            }

                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //-------------------------------------------------------------------------------------------------------------
                            // 9th Task Expected Units in Panel 
                            //----------------------------------------------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - CUT Panel Store");
                            var CutSheetR = from T1 in context.TLCUT_CutSheetReceipt
                                            join T2 in context.TLCUT_CutSheetReceiptDetail on T1.TLCUTSHR_Pk equals T2.TLCUTSHRD_CutSheet_FK
                                            where ((!T1.TLCUTSHR_Issued && T1.TLCUTSHR_InPanelStore) || (T1.TLCUTSHR_Issued && !T1.TLCUTSHR_InReceiptCage))
                                            && T1.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && T1.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && T2.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK
                                            select new { T1.TLCUTSHR_Style_FK, T1.TLCUTSHR_Colour_FK, T2.TLCUTSHRD_Size_FK, T2.TLCUTSHRD_BoxUnits };

                            if (CutSheetR.Count() != 0)
                            {
                                Row[ColIndex] = Row.Field<int>(ColIndex) + CutSheetR.Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0; ;
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //-------------------------------------------------------------------------------------------------------------
                            // 9th Task (a) Expected Units at CMT Store in Panel Receipt Cage
                            //----------------------------------------------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - CMT Store (Receipt Cage)");
                            var CMTPS = CMTPanelStore.Where(x => x.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && x.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && x.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK).ToList();
                            if (CMTPS.Count != 0)
                            {
                                var Answer = CMTPS.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                Answer = Convert.ToInt32(Answer * 0.95);
                                Row[ColIndex] = Row.Field<int>(ColIndex) + Answer;
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //-------------------------------------------------------------------------------------------------------------
                            // 10th Task (b) Expected Units at CMT Store (WIP)
                            //----------------------------------------------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - CMT WIP");
                            if (CMTWIP.Count() != 0)
                            {
                                var WIP = CMTWIP.Where(x => x.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && x.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && x.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK).ToList();
                                if (WIP.Count != 0)
                                {
                                    var Answer = WIP.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Answer;
                                }
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //-------------------------------------------------------------------------------------------------------------
                            // 11th Task (c) Expected Units at CMT Store in Finished Goods Despatch Cage
                            //----------------------------------------------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Units - CMT Store (Despatch Cage)");
                            var QueryC = from T1 in context.TLCMT_CompletedWork
                                         where (!T1.TLCMTWC_Despatched || T1.TLCMTWC_Despatched && !T1.TLCMTWC_BoxReceiptedWhse) && T1.TLCMTWC_Style_FK == Item.TLREP_Style_FK && T1.TLCMTWC_Colour_FK == Item.TLREP_Colour_FK && T1.TLCMTWC_Size_FK == Item.TLREP_Size_FK
                                         select new { T1.TLCMTWC_Style_FK, T1.TLCMTWC_Colour_FK, T1.TLCMTWC_Size_FK, T1.TLCMTWC_Qty };
                            if (QueryC.Count() != 0)
                            {
                                Row[ColIndex] = Row.Field<int>(ColIndex) + QueryC.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                            }

                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            // 12th and Final Task is to update Avg Week Sales, ReOrder Levels, Reorder Qty Sales
                            //===================================================================================
                            var colIndex = dt.Columns.IndexOf("Avg Week Sales");
                            Row[colIndex] = Row.Field<int>(colIndex) + Item.TLREP_ExpectedSales;
                            colIndex = dt.Columns.IndexOf("Re Order Level Sales");
                            Row[colIndex] = Row.Field<int>(colIndex) + Item.TLREP_ReOrderLevel;
                            colIndex = dt.Columns.IndexOf("Re Order Qty Total");
                            Row[colIndex] = Row.Field<int>(colIndex) + Item.TLREP_ReOrderQty;

                            //==================================================
                            //Dont forget to calculate the "To Dye" Column
                            //==================================================
                            var diffValue = Row.Field<int>(DiffIndex);
                            var wipValue = Row.Field<int>(WIPIndex);

                            if (chkIgnoreReorderLevels.Checked)
                            {
                                if (diffValue + wipValue > 0)
                                {
                                    Row[DyeIndex] = 0;
                                }
                                else
                                {
                                    Row[DyeIndex] = (-1 * diffValue) - wipValue;
                                }
                            }
                            else
                            {
                                if ((diffValue + wipValue) > Item.TLREP_ReOrderLevel)
                                    Row[DyeIndex] = 0;
                                else
                                {
                                    //---------------------------------------------------------------
                                    // 30/7/2019 After discussions with Thys Greef who has relooked at the algorithm
                                    // and noticed that previous Dye Orders placed where not being taken into in the final analysis
                                    // which could over inflate the dye orders as they were not considered WIP
                                    //--------------------------------------------------------------
                                    var DOIndex = dt.Columns.IndexOf("Expected Units DO");
                                    var Total = (Item.TLREP_ReOrderLevel + Item.TLREP_ReOrderQty) - diffValue - wipValue - Row.Field<int>(DOIndex);
                                    if (Total < 0)
                                        Row[DyeIndex] = 0;
                                    else
                                        Row[DyeIndex] = Total;
                                }
                            }

                            //==================================================
                            //Now we need to handle outstanding orders -- end of PPS
                            //==================================================
                           
                            // Group Orders into months getting rid of any null Date required
                            // That may be lurking
                            //=======================================================

                            Orders = Orders.Where(x => x.TLCUSTO_DateRequired != null);
                            var GrpByMonth = Orders.GroupBy(x => x.TLCUSTO_DateRequired.Value.Month.ToString().PadLeft(2, '0'));
                            foreach (var Mnth in GrpByMonth)
                            {
                                var QtyOrdered = Mnth.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0;
                                var AllReadyPicked = Mnth.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0;
                                var Nett = QtyOrdered - AllReadyPicked;
                                var MthIndex = dt.Columns.IndexOf("C" + Mnth.Key);
                                if (Nett > 0 && MthIndex >= 0)
                                {
                                    Row[MthIndex] = Row.Field<int>(MthIndex) + Nett;
                                }
                            }

                            //*****************************************
                            // Add to to the data table 
                            //**************************************************
                            try
                            {
                                    dt.Rows.Add(Row);
                            }
                            catch (Exception ex)
                            {
                                   
                            }
                        }
                    }
                    else
                    //*************
                    // This is where the fabric 
                    // begins 
                    //************************************************************
                    {
                        IList<TLDYE_DyeOrderFabric> FABPODetail = context.TLDYE_DyeOrderFabric.Where(x=>!x.TLDYEF_Closed && (x.TLDYEF_Demand - x.TLDYEF_BatchedToDate > 0) ).ToList();
                        if (FABPODetail.Count == 0)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("There are no customer orders to process");
                                return;
                            }
                        }

                        PBar1.Maximum = FABPODetail.Count;
                        var GrpPoDetail = FABPODetail.GroupBy(x => new { x.TLDYEF_Greige_FK , x.TLDYEF_Colours_FK}).ToList();
                        Decimal FabYield = 0.00M;

                        foreach (var item in GrpPoDetail)
                        {
                            DataRow Row = dt.NewRow();

                            var QualPk = item.FirstOrDefault().TLDYEF_Greige_FK;
                            if (QualPk == 0)
                            {
                                continue;
                            }
                            
                            PBar1.PerformStep();

                            decimal TotalWeight = 0.00M;

                            var ColorPk = item.FirstOrDefault().TLDYEF_Colours_FK;


                            Row[0] = QualPk;
                            Row[1] = ColorPk;
                            Row[2] = 0;

                            PBar1.PerformStep();

                            //===============================================================
                            //1st step is to get all the outstandings orders at this point in time
                            //============================================================================

                            var GrpByCustomer = PODetail.Where(x => x.TLCUSTO_Quality_FK != null && (int)x.TLCUSTO_Quality_FK == QualPk && (int)x.TLCUSTO_Colour_FK == ColorPk).GroupBy(x => x.TLCUSTO_Customer_FK);
                            foreach (var Grouped in GrpByCustomer)
                            {
                                TLCSV_PuchaseOrderDetail Order = Grouped.FirstOrDefault(); // Grouped.FirstOrDefault();
                                var CustDetail = context.TLADM_CustomerFile.Find(Order.TLCUSTO_Customer_FK);
                                ColIndex = dt.Columns.IndexOf(CustDetail.Cust_Code);

                                var QtyOrdered = Grouped.Sum(x => (decimal?)x.TLCUSTO_QtyMeters) ?? 0.0M; //  LineOrder.TLCUSTO_Qty;
                                var AllReadyPicked = Grouped.Sum(x => (decimal?)x.TLCUSTO_QtyMeters_Delivered) ?? 0.0M; //  LineOrder.TLCUSTO_QtyPicked_ToDate;     //  context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == LineOrder.TLCUSTO_Pk && !x.TLSOH_Sold && x.TLSOH_Picked).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                var Nett = QtyOrdered - AllReadyPicked;
                                if (Nett > 0 && ColIndex >= 0)
                                {
                                    var _GriegeQual = _Qualities.FirstOrDefault(s => s.TLGreige_Id == QualPk);
                                    if (_GriegeQual != null)
                                    {
                                        var FWidth = context.TLADM_FabWidth.Find(_GriegeQual.TLGreige_FabricWidth_FK);
                                        var FWeight = context.TLADM_FabricWeight.Find(_GriegeQual.TLGreige_FabricWeight_FK);
                                        if (FWidth != null && FWeight != null)
                                        {
                                            FabYield = core.FabricYield(FWidth.FW_Calculation_Value, FWeight.FWW_Calculation_Value);
                                            Nett = Nett / FabYield;
                                        }
                                    }

                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Nett;
                                    Row[OTIndex] = Row.Field<int>(OTIndex) + Nett;

                                    var GrpByMonth = Grouped.GroupBy(x => x.TLCUSTO_DateRequired.Value.Month.ToString().PadLeft(2, '0'));
                                    foreach (var Mnth in GrpByMonth)
                                    {
                                        QtyOrdered = Mnth.Sum(x => (decimal?)x.TLCUSTO_QtyMeters ?? 0.00M) / FabYield;
                                        AllReadyPicked = Mnth.Sum(x => (decimal?)x.TLCUSTO_QtyMeters_Delivered ?? 0.00M) / FabYield;
                                        Nett = QtyOrdered - AllReadyPicked;
                                        var MthIndex = dt.Columns.IndexOf("C" + Mnth.Key);
                                        if (Nett > 0 && MthIndex >= 0)
                                        {
                                            Row[MthIndex] = Row.Field<int>(MthIndex) + Nett;
                                        }
                                    }
                                }
                            }

                            //--------------------------------------------------------------------
                            // We need to find the difference Stock - Orders
                            //------------------------------------------------------------------------
                            Row[DiffIndex] = Row.Field<int>(STIndex) - Row.Field<int>(OTIndex);
                            //---------------------------------------------------------------
                            // 3rd Task -- Dye Orders 
                            //--------------------------------------------------------------

                            ColIndex = dt.Columns.IndexOf("Expected Kgs DO");

                            var DyeOrders = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_Greige_FK == QualPk && x.TLDYEF_Colours_FK == ColorPk).ToList();

                            if (DyeOrders != null)
                            {
                                TotalWeight = DyeOrders.Sum(x => x.TLDYEF_Demand - x.TLDYEF_BatchedToDate);

                                Row[ColIndex] = Row.Field<int>(ColIndex) + TotalWeight;
                            }

                            //---------------------------------------------------
                            // 4th Task -- DyeBatching (Preparation)
                            //--------------------------------------------------------------------
                            //----------------------------------------------------------------
                            //  Expected Kgs 
                            //---------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Kgs - Dyeing Prep");

                            TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                           join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                           join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                           where !T2.DYEB_CommissinCust && T1.TLDYEF_Greige_FK == QualPk && T1.TLDYEF_Colours_FK == ColorPk
                                           && !T2.DYEB_Closed && !T2.DYEB_Allocated && T2.DYEB_Transfered
                                           select T3).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;

                            Row[ColIndex] = Row.Field<int>(ColIndex) + TotalWeight;

                            //---------------------------------
                            // Bring the Prep Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //-------------------------------------------------------------------
                            // 5th Task  -- DyeBatching (WIP) 
                            //----------------------------------------------------------------
                            // Expected Kgs WIP Dyeing  
                            //---------------------------------------------------------------
                            ColIndex = dt.Columns.IndexOf("Expected Kgs - WIP Dyeing");

                            TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                           join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                           join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                           where !T2.DYEB_CommissinCust && T1.TLDYEF_Greige_FK == QualPk && T1.TLDYEF_Colours_FK == ColorPk
                                           && !T2.DYEB_Closed && T2.DYEB_Allocated && T2.DYEB_Transfered
                                           select T3).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;

                            Row[ColIndex] = Row.Field<int>(ColIndex) + TotalWeight;


                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                            //******************************************************
                            // 6th Task  -- DyeBatching (Quarantine) 
                            //******************************************************
                            var DOF = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_Greige_FK == QualPk && x.TLDYEF_Colours_FK == ColorPk).FirstOrDefault();

                            if (DOF == null)
                            {
                                continue;
                            }

                            if (DOF.TLDYEF_Body)
                            {
                                ColIndex = dt.Columns.IndexOf("Expected Kgs - Fabric Quarantine Store");
                                TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                               join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                               join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                               join T4 in context.TLADM_WhseStore on T3.DYEBO_CurrentStore_FK equals T4.WhStore_Id
                                               where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && T4.WhStore_Quarantine && !T3.DYEBO_CutSheet
                                               && T3.DYEBD_BodyTrim && T3.DYEBD_QualityKey == QualPk && T2.DYEB_Colour_FK == ColorPk
                                               select T3).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;


                                Row[ColIndex] = (int)Row[ColIndex] + TotalWeight;

                                //---------------------------------
                                // Bring the WIP Total Up to date
                                //-------------------------------------------------------
                                Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                                //-----------------------------------------------------------------
                                // 7th Task  -- DyeBatching (FABRIC Store) 
                                //--------------------------------------------------------------
                                // Because Dye Batches have no specified Style, Colour and size Key 
                                // We have to get it from the respective Dye Orders 
                                //---------------------------------------------------------------
                                ColIndex = dt.Columns.IndexOf("Expected Kgs - Fabric Store");
                                TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                               join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                               join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                               where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && !T3.DYEBO_Sold &&
                                               T3.DYEBD_BodyTrim && T3.DYEBO_QAApproved && !T3.DYEBO_Rejected && !T3.DYEBO_WriteOff
                                               && !T3.DYEBO_CutSheet && T3.DYEBD_QualityKey == QualPk && T2.DYEB_Colour_FK == ColorPk
                                               select T3).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                                Row[ColIndex] = (int)Row[ColIndex] + TotalWeight;

                            }
                            else
                            {
                                ColIndex = dt.Columns.IndexOf("Expected Kgs - Fabric Quarantine Store");
                                TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                               join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                               join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                               join T4 in context.TLADM_WhseStore on T3.DYEBO_CurrentStore_FK equals T4.WhStore_Id
                                               where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && T4.WhStore_Quarantine && !T3.DYEBO_CutSheet
                                               && !T3.DYEBD_BodyTrim && T3.DYEBD_QualityKey == QualPk && T2.DYEB_Colour_FK == ColorPk
                                               select T3).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;


                                Row[ColIndex] = (int)Row[ColIndex] + TotalWeight;

                                //---------------------------------
                                // Bring the WIP Total Up to date
                                //-------------------------------------------------------
                                Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                                //-----------------------------------------------------------------
                                // 7th Task  -- DyeBatching (FABRIC Store) 
                                //--------------------------------------------------------------
                                // Because Dye Batches have no specified Style, Colour and size Key 
                                // We have to get it from the respective Dye Orders 
                                //---------------------------------------------------------------
                                ColIndex = dt.Columns.IndexOf("Expected Kgs - Fabric Store");
                                TotalWeight = (from T1 in context.TLDYE_DyeOrderFabric
                                               join T2 in context.TLDYE_DyeBatch on T1.TLDYEF_Pk equals T2.DYEB_DyeOrder_FK
                                               join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                               where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && !T3.DYEBO_Sold &&
                                               !T3.DYEBD_BodyTrim && T3.DYEBO_QAApproved && !T3.DYEBO_Rejected && !T3.DYEBO_WriteOff
                                               && !T3.DYEBO_CutSheet && T3.DYEBD_QualityKey == QualPk && T2.DYEB_Colour_FK == ColorPk
                                               select T3).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                                Row[ColIndex] = (int)Row[ColIndex] + TotalWeight;
                            }
                            //---------------------------------
                            // Bring the WIP Total Up to date
                            //-------------------------------------------------------
                            Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);
                            //-------------------------------------------------------------------------------------------------------------
                            try
                            {
                                dt.Rows.Add(Row);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                   /*Set up work book, work sheets, and excel application*/
                   //==========================================================
                    Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();
                    try
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        object misValue = System.Reflection.Missing.Value;
                        Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                        Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();

                        osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                        int colIndex = 0;
                        int rowIndex = 1;

                        foreach (DataColumn dc in dt.Columns)
                        {
                            colIndex++;
                            if (colIndex == 1)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                if (rbFinishedGoods.Checked)
                                {
                                    osheet.Cells[1, colIndex] = "Styles";
                                }
                                else
                                {
                                    osheet.Cells[1, colIndex] = "Qualities";
                                }
                            }
                            else if (colIndex == 2)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "Colour";
                            }
                            else if (colIndex == 3)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                if (rbFinishedGoods.Checked)
                                {
                                    osheet.Cells[1, colIndex] = "Size";
                                }
                                else
                                {
                                    osheet.Cells[1, colIndex] = string.Empty;
                                }
                            }
                            else
                            {
                                if(dc.ColumnName == "C01")
                                    osheet.Cells[1, colIndex] = "JAN";
                                else if (dc.ColumnName == "C02")
                                    osheet.Cells[1, colIndex] = "FEB";
                                else if (dc.ColumnName == "C03")
                                    osheet.Cells[1, colIndex] = "MAR";
                                else if (dc.ColumnName == "C04")
                                    osheet.Cells[1, colIndex] = "APR";
                                else if (dc.ColumnName == "C05")
                                    osheet.Cells[1, colIndex] = "MAY";
                                else if (dc.ColumnName == "C06")
                                    osheet.Cells[1, colIndex] = "JUN";
                                else if (dc.ColumnName == "C07")
                                    osheet.Cells[1, colIndex] = "JUL";
                                else if (dc.ColumnName == "C08")
                                    osheet.Cells[1, colIndex] = "AUG";
                                else if (dc.ColumnName == "C09")
                                    osheet.Cells[1, colIndex] = "SEP";
                                else if (dc.ColumnName == "C10")
                                    osheet.Cells[1, colIndex] = "OCT";
                                else if (dc.ColumnName == "C11")
                                    osheet.Cells[1, colIndex] = "NOV";
                                else if (dc.ColumnName == "C12")
                                    osheet.Cells[1, colIndex] = "DEC";
                                else if (dc.ColumnName == "S01")
                                    osheet.Cells[1, colIndex] = "Sales JAN";
                                else if (dc.ColumnName == "S02")
                                    osheet.Cells[1, colIndex] = "Sales FEB";
                                else if (dc.ColumnName == "S03")
                                    osheet.Cells[1, colIndex] = "Sales MAR";
                                else if (dc.ColumnName == "S04")
                                    osheet.Cells[1, colIndex] = "Sales APR";
                                else if (dc.ColumnName == "S05")
                                    osheet.Cells[1, colIndex] = "Sales MAY";
                                else if (dc.ColumnName == "S06")
                                    osheet.Cells[1, colIndex] = "Sales JUN";
                                else if (dc.ColumnName == "S07")
                                    osheet.Cells[1, colIndex] = "Sales JUL";
                                else if (dc.ColumnName == "S08")
                                    osheet.Cells[1, colIndex] = "Sales AUG";
                                else if (dc.ColumnName == "S09")
                                    osheet.Cells[1, colIndex] = "Sales SEP";
                                else if (dc.ColumnName == "S10")
                                    osheet.Cells[1, colIndex] = "Sales OCT";
                                else if (dc.ColumnName == "S11")
                                    osheet.Cells[1, colIndex] = "Sales NOV";
                                else if (dc.ColumnName == "S12")
                                    osheet.Cells[1, colIndex] = "Sales DEC";
                                else
                                    osheet.Cells[1, colIndex] = dc.ColumnName;
                            }
                        }

                        PBar1.Value = 1;

                        foreach (DataRow dr in dt.Rows)
                        {
                            rowIndex++;
                            colIndex = 0;

                            PBar1.PerformStep();

                            foreach (DataColumn dc in dt.Columns)
                            {
                                colIndex++;

                                if (colIndex == 1)
                                {
                                    if (rbFinishedGoods.Checked)
                                    {
                                        osheet.Cells[rowIndex, colIndex] = _Styles.FirstOrDefault(s => s.Sty_Id == (int)dr[dc.ColumnName]).Sty_Description;
                                    }
                                    else
                                    {
                                        osheet.Cells[rowIndex, colIndex] = _Qualities.FirstOrDefault(s => s.TLGreige_Id == (int)dr[dc.ColumnName]).TLGreige_Description;
                                    }
                                }
                                else if (colIndex == 2)
                                {
                                    osheet.Cells[rowIndex, colIndex] = _Colours.FirstOrDefault(s => s.Col_Id == (int)dr[dc.ColumnName]).Col_Display;
                                }
                                else if (colIndex == 3)
                                {
                                    if (rbFinishedGoods.Checked)
                                    {
                                        osheet.Cells[rowIndex, colIndex] = _Sizes.FirstOrDefault(s => s.SI_id == (int)dr[dc.ColumnName]).SI_Description;
                                    }
                                }
                                else
                                    osheet.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                                
                            }
                        }

                       

                        if (!chkManagementSummary.Checked)
                        {
                            osheet.Columns.AutoFit();

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = "C:\\Temp";
                            sfd.Filter = "Excel |*.xlsx";

                            DialogResult xdr = sfd.ShowDialog();
                            if (xdr == DialogResult.OK)
                            {
                                try
                                {
                                    obook.SaveAs(sfd.FileName);
                                    obook.Saved = true;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }

                            //=====================================================
                            //Release and terminate excel
                            //====================================================================
                            obook.Close();
                            oexcel.Quit();

                            releaseObject(osheet);
                            releaseObject(obook);
                            releaseObject(oexcel);
                            GC.Collect();

                            PBar1.Visible = false;

                            MessageBox.Show("Export finished successfully");
                        }
                        else
                        {
                            PBar1.Visible = false;

                            ProductionPlanning.frmPPSViewRep vRep = new frmPPSViewRep(9, dt);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        oexcel.Quit();
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            }

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        } 

        private struct PPOrdersDATA
        {
            public int StylePK;
            public int ColourPk;
            public int SizePk;
            public int CustomerPk;
            public int BoxedQty;
            

            public PPOrdersDATA(int _StylePk, int _ColourPk, int _SizePk, int _CustomerPk, int _BoxedQty)
            {
                this.StylePK = _StylePk;
                this.ColourPk = _ColourPk;
                this.SizePk = _SizePk;
                this.CustomerPk = _CustomerPk;
                this.BoxedQty = _BoxedQty;
            }
        }

        private struct PPStockDATA
        {
            public int StylePK;
            public int ColourPk;
            public int SizePk;
            public int WareHouse;
            public int BoxedQty;


            public PPStockDATA(int _StylePk, int _ColourPk, int _SizePk, int _WareHousePk, int _BoxedQty)
            {
                this.StylePK = _StylePk;
                this.ColourPk = _ColourPk;
                this.SizePk = _SizePk;
                this.WareHouse = _WareHousePk;
                this.BoxedQty = _BoxedQty;
            }
        }

       
        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
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

