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
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections;

namespace ProductionPlanning
{
    public partial class frmPPSViewRep : Form
    {
        int _RepNo;
        int _Pk;
        ProdQueryParameters _ProdQParms;
        Util core;
        bool[] _Selected;
        public frmPPSViewRep(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmPPSViewRep(int RepNo, int Pk)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Pk = Pk;
        }

        public frmPPSViewRep(int RepNo, ProdQueryParameters PQP)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _ProdQParms = PQP;
        }

        public frmPPSViewRep(int RepNo, ProdQueryParameters PQP, bool[] Select)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _ProdQParms = PQP;
            _Selected = Select;

        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1) // Printing of Replenishment Levels 
            {
                PPSRepository repo = new PPSRepository();

                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();

                var Existing = repo.PPSQuery(_ProdQParms);
                using (var context = new TTI2Entities())
                {
                    foreach (var Record in Existing)
                    {
                        DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Colour = context.TLADM_Colours.Find(Record.TLREP_Colour_FK).Col_Display;
                        // nr.Customer = context.TLADM_CustomerFile.Find(Record.TLREP_Customer_FK).Cust_Description;
                        nr.Style = context.TLADM_Styles.Find(Record.TLREP_Style_FK).Sty_Description;
                        nr.Size = context.TLADM_Sizes.Find(Record.TLREP_Size_FK).SI_Description;
                        nr.ExpectedSales = Record.TLREP_ExpectedSales;
                        nr.ReOrderLevel = Record.TLREP_ReOrderLevel;
                        nr.ReOrderQty = Record.TLREP_ReOrderQty;
                        nr.ReOrderLevelWeeks = Record.TLREP_ReOrderLevelWeeks;
                        nr.ReOrderQtyWeeks = Record.TLREP_ReorderQtyWeeks;


                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "No record found peratining to selection made";
                    dataTable1.AddDataTable1Row(nr);

                }
                ds.Tables.Add(dataTable1);
                ReOrderDetails Reorder = new ReOrderDetails();
                Reorder.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Reorder;


            }
            else if (_RepNo == 2) // Planning Knit stock levels and Knit Orders
            {
                DataSet ds = new DataSet();
                PPSRepository repo = new PPSRepository();
                DataSet3.DataTable1DataTable dataTable1 = new DataSet3.DataTable1DataTable();
                DataSet3.DataTable2DataTable dataTable2 = new DataSet3.DataTable2DataTable();
                Util core = new Util();
                string Title = string.Empty;

                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn[] keys = new DataColumn[1];
                DataColumn column;

                //------------------------------------------------------
                // Create column 1. // This is the primary Key Greige Item
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Col1";

                //---------------------------------------------------------------
                // Add the column to the DataTable.Columns collection.
                //--------------------------------------------------------------
                dt.Columns.Add(column);
                // Add the column to the array.
                //-----------------------------------------------------------------
                keys[0] = column;

                //------------------------------------------------------------------
                // Set the PrimaryKeys property to the array.
                //------------------------------------------------------------------
                dt.PrimaryKey = keys;

                //--------------------------------------------------------------------
                // Add the balance of the columns 
                //---------------------------------------------------------------------
                dt.Columns.Add("Col2", typeof(int));       //1 Available to batch
                dt.Columns["Col2"].DefaultValue = 0;
                dt.Columns.Add("Col3", typeof(int));       //2 Pending Inspection
                dt.Columns["Col3"].DefaultValue = 0;
                dt.Columns.Add("Col4", typeof(int));       //3 Knit Orders
                dt.Columns["Col4"].DefaultValue = 0;
                dt.Columns.Add("Col5", typeof(int));       //4 Dye Orders up to 8 weeks
                dt.Columns["Col5"].DefaultValue = 0;
                dt.Columns.Add("Col6", typeof(int));       //5 Dye Orders plus 8 weeks
                dt.Columns["Col6"].DefaultValue = 0;
                dt.Columns.Add("Col7", typeof(int));       //6 Nett Strock
                dt.Columns["Col7"].DefaultValue = 0;
                dt.Columns.Add("Col8", typeof(int));       //7 Reorder Level 
                dt.Columns["Col8"].DefaultValue = 0;
                dt.Columns.Add("Col9", typeof(int));       //8 Surplus Short
                dt.Columns["Col9"].DefaultValue = 0;
                dt.Columns.Add("Col10", typeof(int));      //9 Reorder Qty
                dt.Columns["Col10"].DefaultValue = 0;
                dt.Columns.Add("Col11", typeof(int));      //10 Suggested Knit Order
                dt.Columns["Col11"].DefaultValue = 0;

                // 1st Course of action is to get all the Greige Items 
                //----------------------------------------------------------

                var EightWeeks = DateTime.Now.AddDays(56);

                var GreigeItems = repo.GreigeQuery(_ProdQParms).OrderBy(x => x.TLGreige_Description);

                using (var context = new TTI2Entities())
                {
                    var ThisWeekNo = core.GetIso8601WeekOfYear(DateTime.Now);

                    foreach (var GreigeItem in GreigeItems)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = GreigeItem.TLGreige_Description;

                        var GreigeP = core.CalculateAvailableToBatch(_ProdQParms.GradeType, GreigeItem.TLGreige_Id, _ProdQParms.IncludeGradeAWithwarnings);
                        
                        if (_ProdQParms.GradeType == 1)
                        {
                            Title = "Grade A Selected";
                        }
                        else if (_ProdQParms.GradeType == 2)
                        {
                            Title = "Grade B Selected";
                        }
                        else if (_ProdQParms.GradeType == 3)
                        {
                            Title = "Grade A, B Selected";
                        }
                        else if(_ProdQParms.GradeType == 4)
                        {
                            Title = "Grade C Selected";
                        }
                        else if (_ProdQParms.GradeType == 5)
                        {
                             Title = "Grade A, C Selected";
                        }
                        else if (_ProdQParms.GradeType == 6)
                        {
                             Title = "Grade B, C Selected";
                        }
                        else if (_ProdQParms.GradeType == 7)
                        {
                             Title = "Grade A, B and C Selected";
                        }

                        Row[1] = GreigeP.Where(x => x.GreigeP_Captured && x.GreigeP_Inspected).Sum(x => (int?)x.GreigeP_weight) ?? 0;
                        Row[2] = GreigeP.Where(x => !x.GreigeP_Inspected).Sum(x => (int?)x.GreigeP_weight) ?? 0;
                        Row[3] = context.TLKNI_Order.Where(x => x.KnitO_Product_FK == GreigeItem.TLGreige_Id && !x.KnitO_Closed).Sum(x => (int?)x.KnitO_Weight) ?? 0;
                        
                        Row[4] = core.DyeOrdersLT8Weeks(GreigeItem.TLGreige_Id);
                        Row[5] = core.DyeOrdersGT8Weeks(GreigeItem.TLGreige_Id);
                        Row[6] = Convert.ToInt32(Row[1].ToString()) + Convert.ToInt32(Row[2].ToString()) - Convert.ToInt32(Row[4].ToString()) - Convert.ToInt32(Row[5].ToString());
                        Row[7] = GreigeItem.TLGreige_ROL;
                        Row[8] = Convert.ToInt32(Row[7].ToString()) - Convert.ToInt32(Row[6].ToString());
                        Row[9] = GreigeItem.TLGreige_ROQ;

                        var SuggestKO = Convert.ToInt32(Row[8].ToString()) + Convert.ToInt32(Row[9].ToString()) - Convert.ToInt32(Row[3].ToString());
                        if (SuggestKO > 0)
                            Row[10] = SuggestKO;
                        else
                            Row[10] = 0;

                        var Greige = context.TLADM_Griege.Find(GreigeItem.TLGreige_Id);
                        if (Greige != null)
                            Greige.TLGreige_LatestSuggest = Convert.ToDecimal(Row[10].ToString());

                        dt.Rows.Add(Row);
                    }

                    context.SaveChanges();
                }

                foreach (DataRow row in dt.Rows)
                {
                    DataSet3.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.Item = row[0].ToString();
                    nr.AvailToBatch = row.Field<int>(1); //  Convert.ToInt32(row[1].ToString());
                    nr.Pending = Convert.ToInt32(row[2].ToString());
                    nr.KnitOrders = Convert.ToInt32(row[3].ToString());
                    nr.DyeOrdersT8 = Convert.ToInt32(row[4].ToString());
                    nr.DyeOrders = Convert.ToInt32(row[5].ToString());
                    nr.NettStock = Convert.ToInt32(row[6].ToString());
                    nr.ReorderLevel = Convert.ToInt32(row[7].ToString());
                    nr.Surplus = Convert.ToInt32(row[8].ToString());
                    nr.ReOrderQty = Convert.ToInt32(row[9].ToString());
                    nr.Suggested = Convert.ToInt32(row[10].ToString());
                    dataTable1.AddDataTable1Row(nr);
                }

                DataSet3.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                xnr.Pk = 1;
                xnr.Title = Title;
                dataTable2.AddDataTable2Row(xnr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                PKnitStock PKnitStock = new PKnitStock();
                PKnitStock.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PKnitStock;
            }
            else if (_RepNo == 3) // Knitting Machine Capacity vs Knit Orders
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable dataTable1 = new DataSet2.DataTable1DataTable();

                Util core = new Util();

                PPSRepository repo = new PPSRepository();

                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn[] keys = new DataColumn[2];
                DataColumn column;

                //------------------------------------------------------
                // Create column 1. // This is the primary Key Greige Item
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Col0";

                //---------------------------------------------------------------
                // Add the column to the DataTable.Columns collection.
                //--------------------------------------------------------------
                dt.Columns.Add(column);  // Greige Item
                // Add the column to the array.
                //-----------------------------------------------------------------
                keys[0] = column;

                //-----------------------------------------------------------
                // Create column 2. // This is the Secondary Key  Machine 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Col1";
                // Add the column to the DataTable.Columns collection.
                //--------------------------------------------------------------
                dt.Columns.Add(column);    // Machine Details
                // Add the column to the array.
                //-----------------------------------------------------------------
                keys[1] = column;

                //------------------------------------------------------------------
                // Set the PrimaryKeys property to the array.
                //------------------------------------------------------------------
                dt.PrimaryKey = keys;
                //--------------------------------------------------------------------
                // Add the balance of the columns 
                //---------------------------------------------------------------------
                dt.Columns.Add("Col2", typeof(int));       //2 Max Daily Capacity
                dt.Columns.Add("Col3", typeof(decimal));   //3 Balance Knit Orders
                dt.Columns.Add("Col4", typeof(decimal));   //4 Days to Knit
                dt.Columns.Add("Col5", typeof(decimal));   //5 Suggested Knit Orders
                dt.Columns.Add("Col6", typeof(int));       //6 Days to Knit Suggested Knit Orders
                dt.Columns.Add("Col7", typeof(decimal));   //7 Yarn Required including  1% loss
                dt.Columns.Add("Col8", typeof(decimal));   //8 Yarn available  

                //-------------------------------------------------
                // Process the Parameters 
                //-----------------------------------------------------------
                var xGreige = repo.GreigeQuery(_ProdQParms);

                using (var context = new TTI2Entities())
                {
                    foreach (var GreigeItem in xGreige)
                    {
                        var GroupOrders = (from KO in context.TLKNI_Order
                                           join GP in context.TLKNI_GreigeProduction on KO.KnitO_Pk equals GP.GreigeP_KnitO_Fk
                                           where KO.KnitO_Product_FK == GreigeItem.TLGreige_Id && !KO.KnitO_Closed
                                           select new { KO.KnitO_Pk, KO.KnitO_YarnO_FK, KO.KnitO_Machine_FK, GP }).GroupBy(x => x.KnitO_Pk);

                        foreach (var GroupOrder in GroupOrders)
                        {
                            //-------------------------------------------------
                            // 1st Get Machine Details 
                            //--------------------------------------------------
                            var Machine_Pk = GroupOrder.FirstOrDefault().KnitO_Machine_FK;
                            TLADM_MachineDefinitions MachDef = context.TLADM_MachineDefinitions.Find(Machine_Pk);

                            var MaxCapacity = MachDef.MD_MaxCapacity;
                            var Realistic = MachDef.MD_Realistic;
                            var Capacity = 24 * MaxCapacity * Realistic / 100;

                            //-------------------------------------------------
                            // 2nd Get Knit Order 
                            //--------------------------------------------------
                            var Order_Pk = GroupOrder.FirstOrDefault().KnitO_Pk;
                            TLKNI_Order KnitOrder = context.TLKNI_Order.Find(Order_Pk);

                            object[] keyVals = new object[] { GreigeItem.TLGreige_Description, MachDef.MD_MachineCode };

                            DataRow foundRow = dt.Rows.Find(keyVals);
                            if (foundRow == null)
                            {
                                foundRow = dt.NewRow();
                                foundRow[0] = GreigeItem.TLGreige_Description;
                                foundRow[1] = MachDef.MD_MachineCode;
                                var DailyCapacity = 24 * MaxCapacity * (Realistic / 100);
                                foundRow[2] = DailyCapacity;
                                var Balance = KnitOrder.KnitO_Weight - GroupOrder.Sum(x => (decimal?)x.GP.GreigeP_weight) ?? 0.00M;
                                if (Balance > 0)
                                    foundRow[3] = Balance;
                                else
                                    foundRow[3] = 0.00M;

                                foundRow[4] = Balance / DailyCapacity;

                                var Suggested = context.TLADM_Griege.Find(KnitOrder.KnitO_Product_FK).TLGreige_LatestSuggest;

                                foundRow[5] = Suggested;
                                foundRow[6] = Suggested / DailyCapacity;
                                foundRow[7] = Suggested * 1.01M;

                                var YarnPN = core.ExtrapNumber(GreigeItem.TLGreige_YarnPowerN, context.TLADM_Yarn.Count());

                                decimal AvailYarn = 0.00M;
                                foreach (var PN in YarnPN)
                                {
                                    var Yarn = context.TLADM_Yarn.Where(x => x.YA_PowerN == PN).FirstOrDefault();
                                    if (Yarn != null)
                                    {
                                        AvailYarn += context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == Yarn.YA_Id && !x.TLKNIOP_PalletAllocated && !x.TLKNIOP_CommisionCust).Sum(x => (Decimal?)x.TLKNIOP_NettWeight - x.TLKNIOP_NettWeightReserved) ?? 0.00M;
                                    }
                                }

                                foundRow[8] = AvailYarn;

                                dt.Rows.Add(foundRow);
                            }
                        }
                    }
                }

                foreach (DataRow Row in dt.Rows)
                {
                    DataSet2.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Col1 = Row[0].ToString();  // Greige  Item 
                    nr.Col2 = Row[1].ToString();  // Knitting Machine  
                    nr.Col3 = Convert.ToDecimal(Row[2].ToString());
                    nr.Col4 = Convert.ToDecimal(Row[3].ToString());
                    nr.Col5 = Convert.ToDecimal(Row[4].ToString());
                    nr.Col6 = Convert.ToDecimal(Row[5].ToString());
                    nr.Col7 = Convert.ToDecimal(Row[6].ToString());
                    if (!String.IsNullOrEmpty(Row[7].ToString()))
                        nr.Col8 = Convert.ToDecimal(Row[7].ToString());
                    else
                        nr.Col8 = 0;

                    if (!String.IsNullOrEmpty(Row[8].ToString()))
                        nr.Col9 = Convert.ToDecimal(Row[8].ToString());
                    else
                        nr.Col9 = 0;
                    dataTable1.AddDataTable1Row(nr);
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet2.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "No records matching selection made";
                    dataTable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(dataTable1);

                SuggestOrders SOrders = new SuggestOrders();
                SOrders.SetDataSource(ds);
                crystalReportViewer1.ReportSource = SOrders;

            }
            else if (_RepNo == 4) // Item Status Report 
            {
                DataSet ds = new DataSet();
                DataSet4.DataTable1DataTable dataTable1 = new DataSet4.DataTable1DataTable();
                IList<TLCSV_PuchaseOrderDetail> PODetail = null;

                core = new Util();

                PPSRepository repo = new PPSRepository();

                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                    {   new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text15", string.Empty},
                        new string[] {"Text16", string.Empty}
                      
                    };


                var GroupedItemStatus = repo.PPSQItemStatus(_ProdQParms).GroupBy(x => new { x.TLREP_Style_FK, x.TLREP_Colour_FK });

                var CNames = core.CreateColumnNames();
                int i = 0;

                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }
                
                using (var context = new TTI2Entities())
                {
                    foreach (var Style in _ProdQParms.Styles)
                    {
                        //=====================================================
                        // Add a new Record to the data table;
                        //=================================================
                        var StylePk = Style.Sty_Id;
                        foreach (var Colour in _ProdQParms.Colours)
                        {
                            int Count = 0;
                            do
                            {
                                DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Col1 = nr.Col10 = nr.Col11 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = nr.Col7 = nr.Col8 = nr.Col9 = 0;

                                nr.Style = Style.Sty_Description;
                                nr.Colour = Colour.Col_Display;
                                nr.ColourNumber_FK = Colour.Col_Id;
                                nr.StyleNumber_FK = Style.Sty_Id;

                                if (Count == 0)
                                {
                                    nr.Description = "Customer Orders";
                                    nr.RowIndex = 1;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        var Amt = core.CalculateCustomerOrder_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 1)
                                {
                                    nr.Description = "Available Stock";
                                    nr.RowIndex = 2;
                                    int Amt = 0;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    //IList<TLCSV_StockOnHand> StockOnHand = context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked && x.TLSOH_Grade.Contains("A") && !x.TLSOH_Write_Off).ToList();
                                    //IList<TLCSV_StockOnHand> StockOnHand = context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Write_Off && !x.TLSOH_Sold && !x.TLSOH_Picked && !x.TLSOH_Split && x.TLSOH_Grade.Contains("A")).ToList();
                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateSOH_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 2)
                                {
                                    nr.Description = "Nett Available";
                                    nr.RowIndex = 3;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    int Amt = 0;
                                    int[] Line1 = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                    int[] Line2 = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                                    foreach (DataRow dr in dataTable1.Rows)
                                    {
                                        if (dr.Field<int>(3) == 1 && dr.Field<int>(17) == Colour.Col_Id && dr.Field<int>(18) == Style.Sty_Id)
                                        {
                                            Line1[0] = (int)dr.Field<int>(6);
                                            Line1[1] = (int)dr.Field<int>(7);
                                            Line1[2] = (int)dr.Field<int>(8);
                                            Line1[3] = (int)dr.Field<int>(9);
                                            Line1[4] = (int)dr.Field<int>(10);
                                            Line1[5] = (int)dr.Field<int>(11);
                                            Line1[6] = (int)dr.Field<int>(12);
                                            Line1[7] = (int)dr.Field<int>(13);
                                            Line1[8] = (int)dr.Field<int>(14);
                                            Line1[9] = (int)dr.Field<int>(15);
                                            Line1[10] = (int)dr.Field<int>(16);
                                        }
                                        else if (dr.Field<int>(3) == 2 && dr.Field<int>(18) == Style.Sty_Id && dr.Field<int>(17) == Colour.Col_Id)
                                        {
                                            Line2[0] = (int)dr.Field<int>(6);
                                            Line2[1] = (int)dr.Field<int>(7);
                                            Line2[2] = (int)dr.Field<int>(8);
                                            Line2[3] = (int)dr.Field<int>(9);
                                            Line2[4] = (int)dr.Field<int>(10);
                                            Line2[5] = (int)dr.Field<int>(11);
                                            Line2[6] = (int)dr.Field<int>(12);
                                            Line2[7] = (int)dr.Field<int>(13);
                                            Line2[8] = (int)dr.Field<int>(14);
                                            Line2[9] = (int)dr.Field<int>(15);
                                            Line2[10] = (int)dr.Field<int>(16);
                                        }
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Line2[0] - Line1[0];
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Line2[1] - Line1[1]; ;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Line2[2] - Line1[2]; ;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Line2[3] - Line1[3]; ;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Line2[4] - Line1[4]; ;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Line2[5] - Line1[5]; ;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Line2[6] - Line1[6];
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Line2[7] - Line1[7]; ;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Line2[8] - Line1[8];
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Line2[9] - Line1[9]; ;
                                        }
                                        else
                                        {
                                            nr.Col11 += Line2[10] - Line1[10]; ;
                                        }
                                    }
                                }
                                else if (Count == 3)
                                {
                                    nr.Description = "Dye Orders";
                                    nr.RowIndex = 4;
                                    int Amt = 0;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateDO_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 4)
                                {
                                    nr.Description = "Dye Batches Pending";
                                    int Amt = 0;
                                    nr.RowIndex = 5;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateDBPrep_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);


                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 5)
                                {
                                    nr.Description = "Dye WIP";
                                    int Amt = 0;
                                    nr.RowIndex = 6;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateDBWIP_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 6)
                                {
                                    nr.Description = "Fabric";
                                    nr.RowIndex = 7;
                                    int Amt = 0;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateDBFabric_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 7)
                                {
                                    nr.Description = "Cutting WIP";
                                    int Amt = 0;
                                    nr.RowIndex = 8;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateCuttingWIP_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 8)
                                {
                                    nr.Description = "Panels George";
                                    int Amt = 0;
                                    nr.RowIndex = 9;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateCuttingPanelStore_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 9)
                                {
                                    nr.Description = "Panels BLW";
                                    int Amt = 0;
                                    nr.RowIndex = 10;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateCMTReceiptCage_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 10)
                                {
                                    nr.Description = "BLW WIP";
                                    int Amt = 0;
                                    nr.RowIndex = 11;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateCMTWIP_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);
                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 11)
                                {
                                    nr.Description = "BLW Stock";
                                    nr.RowIndex = 12;
                                    int Amt = 0;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        Amt = core.CalculateCMTDespatchCage_Units(Style.Sty_Id, Colour.Col_Id, Size.SI_id);

                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Amt;
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Amt;
                                        }
                                        else
                                        {
                                            nr.Col11 += Amt;
                                        }
                                    }
                                }
                                else if (Count == 12)
                                {
                                    nr.Description = "Total WIP";
                                    nr.RowIndex = 13;
                                    var LeadTime = context.TLPPS_ProductionLeadTime.Where(x => x.TLPDL_LineNo == nr.RowIndex).FirstOrDefault();
                                    if (LeadTime != null)
                                    {
                                        nr.Description = LeadTime.TLPDL_Description;
                                        nr.LeadTime_Days = LeadTime.TLPDL_LeadTimeDays;
                                    }
                                    int[] Line1 = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; foreach (DataRow dr in dataTable1.Rows)
                                    {
                                        if (dr.Field<int>(3) > 3 && dr.Field<int>(3) < 13 && dr.Field<int>(18) == Style.Sty_Id && dr.Field<int>(17) == Colour.Col_Id)
                                        {
                                            Line1[0] += (int)dr.Field<int>(6);
                                            Line1[1] += (int)dr.Field<int>(7);
                                            Line1[2] += (int)dr.Field<int>(8);
                                            Line1[3] += (int)dr.Field<int>(9);
                                            Line1[4] += (int)dr.Field<int>(10);
                                            Line1[5] += (int)dr.Field<int>(11);
                                            Line1[6] += (int)dr.Field<int>(12);
                                            Line1[7] += (int)dr.Field<int>(13);
                                            Line1[8] += (int)dr.Field<int>(14);
                                            Line1[9] += (int)dr.Field<int>(15);
                                            Line1[10] += (int)dr.Field<int>(16);
                                        }
                                    }

                                    foreach (var Size in _ProdQParms.Sizes)
                                    {
                                        if (Size.SI_ColNumber == 1)
                                        {
                                            nr.Col1 += Line1[0];
                                        }
                                        else if (Size.SI_ColNumber == 2)
                                        {
                                            nr.Col2 += Line1[1];
                                        }
                                        else if (Size.SI_ColNumber == 3)
                                        {
                                            nr.Col3 += Line1[2];
                                        }
                                        else if (Size.SI_ColNumber == 4)
                                        {
                                            nr.Col4 += Line1[3];
                                        }
                                        else if (Size.SI_ColNumber == 5)
                                        {
                                            nr.Col5 += Line1[4];
                                        }
                                        else if (Size.SI_ColNumber == 6)
                                        {
                                            nr.Col6 += Line1[5];
                                        }
                                        else if (Size.SI_ColNumber == 7)
                                        {
                                            nr.Col7 += Line1[6];
                                        }
                                        else if (Size.SI_ColNumber == 8)
                                        {
                                            nr.Col8 += Line1[7];
                                        }
                                        else if (Size.SI_ColNumber == 9)
                                        {
                                            nr.Col9 += Line1[8];
                                        }
                                        else if (Size.SI_ColNumber == 10)
                                        {
                                            nr.Col10 += Line1[9];
                                        }
                                        else
                                        {
                                            nr.Col11 += Line1[10];
                                        }
                                    }
                                }

                                var Tot = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                                if (Tot == 0)
                                    continue;

                                dataTable1.AddDataTable1Row(nr);
                            } while (++Count < 13);
                        }

                        if (dataTable1.Rows.Count == 0)
                        {
                            DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Col1 = nr.Col10 = nr.Col11 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = nr.Col7 = nr.Col8 = nr.Col9 = 0;

                            nr.Style = Style.Sty_Description;
                            nr.Colour = "No Data Available";

                        }
                    }

                    ds.Tables.Add(dataTable1);
                    ItemStatus SItem = new ItemStatus();

                    IEnumerator ie = SItem.Section2.ReportObjects.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                        {
                            CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                            var result = (from u in ColumnNames
                                          where u[0] == to.Name
                                          select u).FirstOrDefault();

                            if (result != null)
                                to.Text = result[1];

                        }
                    }
                    SItem.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SItem;
                }

            }
            else if (_RepNo == 5)  // Total Days Sales By Style, Colour and Size 
            {
                DataSet ds = new DataSet();
                DataSet4.DataTable1DataTable dataTable1 = new DataSet4.DataTable1DataTable();
                DataSet4.DataTable2DataTable dataTable2 = new DataSet4.DataTable2DataTable();
                core = new Util();
                List<TopSellers> TSellers = new List<TopSellers>();

                PPSRepository repo = new PPSRepository();

                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                {   new string[] {"Text6", string.Empty},
                    new string[] {"Text7", string.Empty},
                    new string[] {"Text8", string.Empty},
                    new string[] {"Text9", string.Empty},
                    new string[] {"Text10", string.Empty},
                    new string[] {"Text11", string.Empty},
                    new string[] {"Text12", string.Empty},
                    new string[] {"Text13", string.Empty},
                    new string[] {"Text14", string.Empty},
                    new string[] {"Text15", string.Empty},
                    new string[] {"Text16", string.Empty}
                      
                };

                var CNames = core.CreateColumnNames();
                int i = 0;

                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                var WorkingDays = core.GetWorkingDays(_ProdQParms.FromDate, _ProdQParms.ToDate);

                using (var context = new TTI2Entities())
                {
                    var SalesBySize = repo.PPSSales(_ProdQParms).ToList();

                    var PPSSales = repo.PPSSales(_ProdQParms).AsEnumerable()
                                   .Select(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK, x.TLSOH_BoxedQty })
                                   .GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK })
                                   .Select(g => new { g.Key.TLSOH_Style_FK, g.Key.TLSOH_Colour_FK, TotalBoxedQty = g.Sum(x => x.TLSOH_BoxedQty) }).OrderByDescending(x=>x.TotalBoxedQty);

                    var GrandTotal = PPSSales.Sum(x => x.TotalBoxedQty);

                    foreach (var PPSSale in PPSSales)
                    {
                        //if(++TSCnt > _ProdQParms.TopSellers)
                           // continue;

                        //Create a new table row  
                        //==========================================================
                        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Col1 = nr.Col10 = nr.Col11 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = nr.Col7 = nr.Col8 = nr.Col9 = 0;
                        nr.Pk = 1;
                        nr.Style = context.TLADM_Styles.Find(PPSSale.TLSOH_Style_FK).Sty_Description;
                        nr.Colour = context.TLADM_Colours.Find(PPSSale.TLSOH_Colour_FK).Col_Display;
                        
                        var SBySize = SalesBySize.AsEnumerable()
                                        .Where(x => x.TLSOH_Style_FK == PPSSale.TLSOH_Style_FK && x.TLSOH_Colour_FK == PPSSale.TLSOH_Colour_FK)
                                        .Select(x => new { x.TLSOH_Size_FK, x.TLSOH_BoxedQty }).GroupBy(x => new { x.TLSOH_Size_FK }).Select(g => new { g.Key.TLSOH_Size_FK, TotalSizeSales = g.Sum(x => x.TLSOH_BoxedQty) }).Distinct();           

                        foreach (var Si in SBySize)
                        {
                            var BxedQty = Si.TotalSizeSales;

                            var xSi = context.TLADM_Sizes.Find(Si.TLSOH_Size_FK);
                            if (xSi.SI_ColNumber == 1)
                            {
                                nr.Col1 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 2)
                            {
                                nr.Col2 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 3)
                            {
                                nr.Col3 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 4)
                            {
                                nr.Col4 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 5)
                            {
                                nr.Col5 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 6)
                            {
                                nr.Col6 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 7)
                            {
                                nr.Col7 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 8)
                            {
                                nr.Col8 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 9)
                            {
                                nr.Col9 += BxedQty;
                            }
                            else if (xSi.SI_ColNumber == 10)
                            {
                                nr.Col10 += BxedQty;
                            }
                            else
                            {
                                nr.Col11 += BxedQty;
                            }
                        }
                        //==========================================
                        // Add to the table 
                        //========================================
                        var Tot = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                        if (Tot == 0)
                                continue;

                        nr.Total = Tot;
                        nr.Percentage = (decimal)Tot / (decimal)GrandTotal * 100;

                        dataTable1.AddDataTable1Row(nr);
                        
                    }

                    DataSet4.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                    hnr.Pk = 1;
                    hnr.Title = "Sales Analysis Ranked By Style and Colour From ";
                    hnr.FromDate = _ProdQParms.FromDate;
                    hnr.ToDate = _ProdQParms.ToDate;

                    if (dataTable1.Rows.Count == 0)
                    {
                        hnr.Errorlog = "To Data Found Matching Selection Made";
                        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Col1 = nr.Col10 = nr.Col11 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = nr.Col7 = nr.Col8 = nr.Col9 = 0;
                        dataTable1.AddDataTable1Row(nr);

                    }
                    dataTable2.AddDataTable2Row(hnr);
                    
                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);
                    DaysOfSales SItem = new DaysOfSales();

                    IEnumerator ie = SItem.Section2.ReportObjects.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                        {
                            CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                            var result = (from u in ColumnNames
                                          where u[0] == to.Name
                                          select u).FirstOrDefault();

                            if (result != null)
                                to.Text = result[1];

                        }
                    }
                    SItem.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SItem;
                }
            }
            else if (_RepNo == 6)  // Production Days  
            {
                // ReportDepts = new bool[3] { false, false, false };
                // 1st Element = DyeHouse Prep 
                // 2nd Element = Cutting 
                // 3rd Element = CMT 

                DataSet ds = new DataSet();
                DataSet5.DataTable1DataTable dataTable1 = new DataSet5.DataTable1DataTable();
                DataSet5.DataTable2DataTable dataTable2 = new DataSet5.DataTable2DataTable();
                core = new Util();

                DataSet5.DataTable1Row hrw = dataTable1.NewDataTable1Row();
                hrw.Pk = 1;
                hrw.Title = "Production Analysis By Department";
                hrw.FromDate = _ProdQParms.FromDate;
                hrw.ToDate = _ProdQParms.ToDate;
                dataTable1.AddDataTable1Row(hrw);

                using (var context = new TTI2Entities())
                {
                    if (_ProdQParms.QAReportingDepts[0] == true)
                    {
                        // Dye House  
                        //=======================================================
                        var DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_Transfered && x.DYEB_BatchDate >= _ProdQParms.FromDate && x.DYEB_BatchDate <= _ProdQParms.ToDate).ToList();

                        foreach (var DyeBatch in DyeBatches)
                        {
                            DateTime TAllocatedDate = new DateTime();

                            // 1st Task is to get the transfer info
                            //===================================================
                            DataSet5.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            
                            nr.Pk = 1;
                            nr.Creation_Date = (DateTime)DyeBatch.DYEB_BatchDate;
                            nr.Transaction_Date = (DateTime)DyeBatch.DYEB_TransferDate ;
                            nr.Batch_No = DyeBatch.DYEB_BatchNo;
                            nr.Description = "DyeHouse Prep";
                            nr.Department = "Dyeing Department";
                            nr.Sector = 1;
                            nr.WorkSector = 1;
                            nr.Days = core.GetWorkingDays(nr.Creation_Date, nr.Transaction_Date);

                            dataTable2.AddDataTable2Row(nr);

                            // 2nd Task is to get the Allocated Information info
                            //===================================================
                            if (DyeBatch.DYEB_Allocated)
                            {
                                var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                                if (Allocated != null)
                                {
                                    nr = dataTable2.NewDataTable2Row();
                                    nr.Pk = 1;
                                    nr.Creation_Date = (DateTime)DyeBatch.DYEB_TransferDate;
                                    nr.Transaction_Date = (DateTime)Allocated.TLDYEA_AllocateDate;
                                    nr.Batch_No = DyeBatch.DYEB_BatchNo;
                                    nr.Sector = 1;
                                    nr.WorkSector = 2;
                                    nr.Description = "Dye Batch Allocation";
                                    nr.Department = "Dyeing Department";
                                    nr.Days = core.GetWorkingDays(nr.Creation_Date, nr.Transaction_Date);

                                    dataTable2.AddDataTable2Row(nr);

                                    TAllocatedDate = Allocated.TLDYEA_AllocateDate;

                                }

                            }
                              // 3rd Task is to get the Completed Information info
                            //===================================================
                            if (DyeBatch.DYEB_OutProcess)
                            {
                                nr = dataTable2.NewDataTable2Row();
                                nr.Pk = 1;
                                nr = dataTable2.NewDataTable2Row();
                                nr.Creation_Date = TAllocatedDate;
                                nr.Transaction_Date = (DateTime)DyeBatch.DYEB_OutProcessDate;
                                nr.Batch_No = DyeBatch.DYEB_BatchNo;
                                nr.Sector = 1;
                                nr.WorkSector = 3;
                                nr.Description = "Dyeing Complete";
                                nr.Department = "Dyeing Department";
                                nr.Days = core.GetWorkingDays(nr.Creation_Date, nr.Transaction_Date);

                                dataTable2.AddDataTable2Row(nr);

                                TAllocatedDate = (DateTime)DyeBatch.DYEB_OutProcessDate;
                            }

                            // QA Final Approval 
                            //=============================================
                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                            if (Dept != null)
                            {
                                var TransType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 700).FirstOrDefault();
                                if (TransType != null)
                                {
                                    var DyeTrans = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DyeBatch.DYEB_Pk ).FirstOrDefault();
                                    if(DyeTrans != null)
                                    {
                                       nr = dataTable2.NewDataTable2Row();
                                       nr.Pk = 1;
                                       nr.Creation_Date = TAllocatedDate;
                                       nr.Transaction_Date = (DateTime)DyeTrans.TLDYET_Date;
                                       nr.Batch_No = DyeBatch.DYEB_BatchNo;
                                       nr.Sector = 1;
                                       nr.WorkSector = 4;
                                       nr.Description = "DyeHouse QA Final Approval";
                                       nr.Department = "Dyeing Department";
                                       nr.Days = core.GetWorkingDays(nr.Creation_Date, nr.Transaction_Date);
                                       dataTable2.AddDataTable2Row(nr);
                                    }
                                }
                            }
                        }
                    }
                   
                    if (_ProdQParms.QAReportingDepts[1] == true)
                    {
                        var CutSheets = context.TLCUT_CutSheet.Where(x => x.TLCutSH_Date >= _ProdQParms.FromDate && x.TLCutSH_Date <= _ProdQParms.ToDate && x.TLCutSH_Accepted).ToList();
                        foreach (var Cutsheet in CutSheets)
                        {
                            DateTime TAllocatedDate = new DateTime();

                            var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == Cutsheet.TLCutSH_Pk).FirstOrDefault();
                            if (CutSheetReceipt != null)
                            {
                                if (CutSheetReceipt.TLCUTSHR_DateIntoBunStore != null)
                                {
                                    DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                                    bnr.Pk = 1;
                                    bnr.Creation_Date = (DateTime)Cutsheet.TLCutSH_Date;
                                    bnr.Transaction_Date = (DateTime)CutSheetReceipt.TLCUTSHR_DateIntoBunStore;
                                    bnr.Batch_No = Cutsheet.TLCutSH_No;
                                    bnr.Description = "To Bundle Store";
                                    bnr.Sector = 2;
                                    bnr.WorkSector = 1;
                                    bnr.Department = "Cutting Department";
                                    bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                                    dataTable2.AddDataTable2Row(bnr);

                                    TAllocatedDate = bnr.Transaction_Date;
                                }

                                if (CutSheetReceipt.TLCUTSHR_DateIntoPanelStore != null)
                                {
                                    DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                                    bnr.Pk = 1;
                                    bnr.Transaction_Date = (DateTime)CutSheetReceipt.TLCUTSHR_DateIntoPanelStore;

                                    bnr.Creation_Date = TAllocatedDate;
                                    bnr.Batch_No = Cutsheet.TLCutSH_No;
                                    bnr.Sector = 2;
                                    bnr.WorkSector = 2;
                                    bnr.Description = "To Panel Store";
                                    bnr.Department = "Cutting Department";
                                    bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                                    dataTable2.AddDataTable2Row(bnr);

                                    TAllocatedDate = bnr.Transaction_Date;

                                }

                                if (CutSheetReceipt.TLCUTSHR_Issued)
                                {
                                    var PIDet = (from PanIssue in context.TLCMT_PanelIssue
                                                join PanIssueDet in context.TLCMT_PanelIssueDetail on PanIssue.CMTPI_Pk equals PanIssueDet.CMTPID_PI_FK
                                                where PanIssueDet.CMTPID_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk
                                                select PanIssue).FirstOrDefault();
 
                                    if(PIDet != null)
                                    {
                                        DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                                        bnr.Pk = 1;
                                        bnr.Transaction_Date = (DateTime)PIDet.CMTPI_Date;
                                        bnr.Creation_Date = TAllocatedDate;
                                        bnr.Batch_No = Cutsheet.TLCutSH_No;
                                        bnr.Sector = 2;
                                        bnr.WorkSector = 3;
                                        bnr.Description = "Issued To CMT";
                                        bnr.Department = "Cutting Department";
                                        bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                                        dataTable2.AddDataTable2Row(bnr);
                                    }
                                }
                            }
                        }
                    }
                    
                    if (_ProdQParms.QAReportingDepts[2] == true)
                    {
                        //CMT Trans
                        var CMTLineIssues = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_Date >= _ProdQParms.FromDate && x.TLCMTLI_Date <= _ProdQParms.ToDate).ToList();
                        foreach (var CMTLineIssue in CMTLineIssues)
                        {
                            DateTime TAllocatedDate = new DateTime();

                            if (CMTLineIssue.TLCMTLI_IssuedToLine)
                            {
                                DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                                bnr.Pk = 1;
                                bnr.Creation_Date = (DateTime)CMTLineIssue.TLCMTLI_Date;
                                bnr.Transaction_Date = (DateTime)CMTLineIssue.TLCMTLI_TransferDate;
                                bnr.Batch_No = context.TLCUT_CutSheet.Find(CMTLineIssue.TLCMTLI_CutSheet_FK).TLCutSH_No ;
                                bnr.Sector = 3;
                                bnr.WorkSector = 1;
                                bnr.Description = "Issued To Line";
                                bnr.Department = "CMT Department";
                                bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                                dataTable2.AddDataTable2Row(bnr);

                                TAllocatedDate = bnr.Transaction_Date;

                            }

                            if (CMTLineIssue.TLCMTLI_WorkCompleted)
                            {
                                DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                                bnr.Pk = 1;
                                bnr.Creation_Date = TAllocatedDate;
                                bnr.Transaction_Date = (DateTime)CMTLineIssue.TLCMTLI_WorkCompletedDate;
                                bnr.Batch_No = context.TLCUT_CutSheet.Find(CMTLineIssue.TLCMTLI_CutSheet_FK).TLCutSH_No;
                                bnr.Sector = 3;
                                bnr.WorkSector = 2;
                                bnr.Description = "Work Complete";
                                bnr.Department = "CMT Department";
                                bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                                dataTable2.AddDataTable2Row(bnr);

                                TAllocatedDate = bnr.Transaction_Date;
                            }
                        }
                    }
                    if (_ProdQParms.QAReportingDepts[3] == true)
                    {
                        var Sales = context.TLCSV_StockOnHand.Where(x => x.TLSOH_SoldDate >= _ProdQParms.FromDate && x.TLSOH_SoldDate <= _ProdQParms.ToDate && x.TLSOH_Sold).ToList();
                        foreach (var Sale in Sales)
                        {
                            DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                            bnr.Pk = 1;
                            bnr.Creation_Date = (DateTime)Sale.TLSOH_DateIntoStock;
                            bnr.Transaction_Date = (DateTime)Sale.TLSOH_SoldDate;
                            bnr.Batch_No = Sale.TLSOH_BoxNumber;
                            bnr.Sector = 4;
                            bnr.WorkSector = 1;
                            bnr.Description = "Days in stock";
                            bnr.Department = "Customer Service WareHouse ";
                            bnr.Days = core.GetWorkingDays(bnr.Creation_Date, bnr.Transaction_Date);

                            dataTable2.AddDataTable2Row(bnr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet5.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                    bnr.Pk = 1;
                    bnr.ErrorLog = " No Records Found matching dates selection ";
                    bnr.Sector = 1;
                    bnr.WorkSector = 1;
                    dataTable2.AddDataTable2Row(bnr);
                }
                ds.Tables.Add(dataTable2);

                ProductionDays SItem = new ProductionDays();
                SItem.SetDataSource(ds);
                crystalReportViewer1.ReportSource = SItem;
            }

            crystalReportViewer1.Refresh();
        }

        public class TopSellers
        {
            public TopSellers(int Styles, int Clrs, int Szs, int TBoxQty , int BoxQty)
            {
                _Styles = Styles;
                _Colours = Clrs;
                _Sizes = Szs;
                _TotBoxedQtys = TBoxQty;
                _BoxedQtys = BoxQty;
            }

            public int _Styles { get; set; }
            public int _Colours { get; set; }
            public int _Sizes { get; set; }
            public int _TotBoxedQtys { get; set; }
            public int _BoxedQtys { get; set; }
        }
    }
}