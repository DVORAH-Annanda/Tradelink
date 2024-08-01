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
using System.Net.Mime;
using CrystalDecisions.CrystalReports.Engine;
using static log4net.Appender.ColoredConsoleAppender;
using System.Runtime.Remoting.Contexts;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ProductionPlanning
{
    public class CustomerMapping
    {
        public int Customer_FK { get; set; }
        public string CustomerCode { get; set; }
    }

    public partial class frmPPSViewRep : Form
    {
        int _RepNo;
        int _Pk;
        ProdQueryParameters _ProdQParms;
        Util core;
        System.Data.DataTable _dt; 

        bool[] _Selected;



        public frmPPSViewRep()
        {
            InitializeComponent();
        }

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

        public frmPPSViewRep(int RepNo, System.Data.DataTable dt)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _dt = dt;

        }
        private void frmPPSViewRep_Load(object sender, EventArgs e)
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
                    nr.ErrorLog = "No record found pertaining to selection made";
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
                        Row[3] = 0;

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
                        else if (_ProdQParms.GradeType == 4)
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

                        if(_ProdQParms.ExcludeDiscontinued)
                        {
                            Title += " Discontinued Items excluded";
                        }
                        else
                        {
                            Title += " Discontinued Items Included";
                        }

                        var GreigeProduced = (from T1 in context.TLKNI_Order
                                          join T2 in context.TLKNI_GreigeProduction
                                          on T1.KnitO_Pk equals T2.GreigeP_KnitO_Fk
                                          where T1.KnitO_Product_FK == GreigeItem.TLGreige_Id && !T1.KnitO_Closed && T2.GreigeP_Captured && T2.GreigeP_Inspected
                                          select T2).Sum(x =>(decimal ?) x.GreigeP_weightAvail) ?? 0.00M; 

                        Row[1] = GreigeP.Where(x => x.GreigeP_Captured && x.GreigeP_Inspected).Sum(x => (int?)x.GreigeP_weight) ?? 0;
                        Row[2] = GreigeP.Where(x => !x.GreigeP_Inspected).Sum(x => (int?)x.GreigeP_weight) ?? 0;
                        Row[3] = context.TLKNI_Order.Where(x => x.KnitO_Product_FK == GreigeItem.TLGreige_Id && !x.KnitO_Closed).Sum(x => (int?)x.KnitO_Weight) ?? 0;
                        if(Convert.ToDecimal(Row[3]) - GreigeProduced > 0)
                        {
                            Row[3] = Convert.ToDecimal(Row[3]) - GreigeProduced;
                        }
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
                                   .Select(g => new { g.Key.TLSOH_Style_FK, g.Key.TLSOH_Colour_FK, TotalBoxedQty = g.Sum(x => x.TLSOH_BoxedQty) }).OrderByDescending(x => x.TotalBoxedQty);

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
                            nr.Transaction_Date = (DateTime)DyeBatch.DYEB_TransferDate;
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
                                    var DyeTrans = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                                    if (DyeTrans != null)
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

                                    if (PIDet != null)
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
                                bnr.Batch_No = context.TLCUT_CutSheet.Find(CMTLineIssue.TLCMTLI_CutSheet_FK).TLCutSH_No;
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
            else if (_RepNo == 7) //  
            {
                DataSet ds = new DataSet();
                DataSet6.DataTable1DataTable dataTable1 = new DataSet6.DataTable1DataTable();
                DataSet6.DataTable2DataTable dataTable2 = new DataSet6.DataTable2DataTable();
                core = new Util();

                //===================================================== 
                // 0 = Piece Number
                // 1 = Knit Order 
                // 2 = Dye Batch 
                // 3 = Cut Sheet 
                //============================

                DataSet6.DataTable1Row hrw = dataTable1.NewDataTable1Row();
                hrw.Pk = 1;
                hrw.Title = "Inter Departmental Faults Analysis";
                hrw.FromDate = _ProdQParms.FromDate;
                hrw.ToDate = _ProdQParms.ToDate;
                dataTable1.AddDataTable1Row(hrw);

                using (var context = new TTI2Entities())
                {
                    if (_ProdQParms.SelectedOptions[3])
                    {
                        var SelectedOpt = context.TLPPS_InterDept.Find(_ProdQParms.InterDeptOption);

                        var RecKey = _ProdQParms.RecordKeys[3];

                        var LineIssue = context.TLCMT_LineIssue.Find(RecKey);
                        if (LineIssue != null)
                        {
                            DataSet6.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.Pk = 1;
                            nr.CutSheet = LineIssue.TLCMTLI_CutSheetDetails;

                            var ProdF = context.TLCMT_ProductionFaults.Where(x => x.TLCMTPF_PanelIssue_FK == RecKey && x.TLCMTPF_Fault_FK == SelectedOpt.TLInter_CMT_Fk).FirstOrDefault();
                            if (ProdF != null)
                            {
                                nr.Measurement1 = 0;
                                nr.Measurement2 = 0;
                                nr.Measurement3 = ProdF.TLCMTPF_Qty;
                                nr.MeasurementDescrip = SelectedOpt.TLInter_Description;
                                nr.PieceNo = "Total For CMT";
                                dataTable2.AddDataTable2Row(nr);

                                var CSheet = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK);
                                if (CSheet != null)
                                {
                                    var DbDetails = (from DB in context.TLDYE_DyeBatch
                                                     join DBD in context.TLDYE_DyeBatchDetails on DB.DYEB_Pk equals DBD.DYEBD_DyeBatch_FK
                                                     where CSheet.TLCutSH_DyeBatch_FK == DB.DYEB_Pk
                                                     select DBD).ToList();

                                    foreach (var DbDetail in DbDetails)
                                    {
                                        nr = dataTable2.NewDataTable2Row();
                                        nr.Pk = 1;
                                        nr.MeasurementDescrip = SelectedOpt.TLInter_Description;
                                        nr.Measurement1 = 0;
                                        nr.Measurement2 = 0;
                                        nr.Measurement3 = 0;

                                        var GriegeProd = context.TLKNI_GreigeProduction.Find(DbDetail.DYEBD_GreigeProduction_FK);
                                        if (GriegeProd != null)
                                        {
                                            nr.PieceNo = GriegeProd.GreigeP_PieceNo;

                                            var tst = context.TLADM_QualityDefinition.Find(SelectedOpt.TLInter_Knitting_Fk);
                                            if (tst != null)
                                            {
                                                if (tst.QD_ColumnIndex == 1)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas1;
                                                }
                                                else if (tst.QD_ColumnIndex == 2)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas2;
                                                }
                                                else if (tst.QD_ColumnIndex == 3)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas3;
                                                }
                                                else if (tst.QD_ColumnIndex == 4)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas4;
                                                }
                                                else if (tst.QD_ColumnIndex == 5)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas5;
                                                }
                                                else if (tst.QD_ColumnIndex == 6)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas6;
                                                }
                                                else if (tst.QD_ColumnIndex == 7)
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas7;
                                                }
                                                else
                                                {
                                                    nr.Measurement1 = GriegeProd.GreigeP_Meas8;
                                                }

                                            }
                                        }

                                        var QualExp = context.TLDye_QualityException.Where(x => x.TLDyeIns_GriegeProduct_Fk == DbDetail.DYEBD_GreigeProduction_FK && x.TLDyeIns_QADyeProcessField_Fk == SelectedOpt.TLInter_Dying_Fk).FirstOrDefault();
                                        if (QualExp != null)
                                        {
                                            nr.Measurement2 = QualExp.TLDyeIns_Quantity;
                                        }

                                        dataTable2.AddDataTable2Row(nr);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_ProdQParms.SelectedOptions[2])
                        {
                            var SelectedOpt = context.TLPPS_InterDept.Find(_ProdQParms.InterDeptOption);
                            var RecKey = _ProdQParms.RecordKeys[2];
                            var DbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == RecKey).ToList();

                            foreach (var DbDetail in DbDetails)
                            {
                                DataSet6.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                nr.Pk = 1;
                                nr.MeasurementDescrip = SelectedOpt.TLInter_Description;
                                nr.Measurement1 = 0;
                                nr.Measurement2 = 0;
                                nr.Measurement3 = 0;
                                //==============================================
                                // substitute dye batch no for cut sheet
                                //============================================
                                nr.CutSheet = context.TLDYE_DyeBatch.Find(RecKey).DYEB_BatchNo;

                                var GriegeProd = context.TLKNI_GreigeProduction.Find(DbDetail.DYEBD_GreigeProduction_FK);
                                if (GriegeProd != null)
                                {
                                    nr.PieceNo = GriegeProd.GreigeP_PieceNo;

                                    var tst = context.TLADM_QualityDefinition.Find(SelectedOpt.TLInter_Knitting_Fk);
                                    if (tst != null)
                                    {
                                        if (tst.QD_ColumnIndex == 1)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas1;
                                        }
                                        else if (tst.QD_ColumnIndex == 2)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas2;
                                        }
                                        else if (tst.QD_ColumnIndex == 3)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas3;
                                        }
                                        else if (tst.QD_ColumnIndex == 4)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas4;
                                        }
                                        else if (tst.QD_ColumnIndex == 5)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas5;
                                        }
                                        else if (tst.QD_ColumnIndex == 6)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas6;
                                        }
                                        else if (tst.QD_ColumnIndex == 7)
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas7;
                                        }
                                        else
                                        {
                                            nr.Measurement1 = GriegeProd.GreigeP_Meas8;
                                        }

                                    }
                                }

                                var QualExp = context.TLDye_QualityException.Where(x => x.TLDyeIns_GriegeProduct_Fk == DbDetail.DYEBD_GreigeProduction_FK && x.TLDyeIns_QADyeProcessField_Fk == SelectedOpt.TLInter_Dying_Fk).FirstOrDefault();
                                if (QualExp != null)
                                {
                                    nr.Measurement2 = QualExp.TLDyeIns_Quantity;
                                }
                                dataTable2.AddDataTable2Row(nr);
                            }
                        }
                        else if (_ProdQParms.SelectedOptions[1])
                        {
                            var DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _ProdQParms.FromDate && x.DYEB_OutProcessDate <= _ProdQParms.ToDate).ToList();
                            foreach (var DyeBatch in DyeBatches)
                            {
                                var SelectedOpt = context.TLPPS_InterDept.Find(_ProdQParms.InterDeptOption);
                                var RecKey = _ProdQParms.RecordKeys[2];
                                var DbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).ToList();
                                foreach (var DbDetail in DbDetails)
                                {
                                    DataSet6.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                    nr.Pk = 1;
                                    nr.MeasurementDescrip = SelectedOpt.TLInter_Description;
                                    nr.Measurement1 = 0;
                                    nr.Measurement2 = 0;
                                    nr.Measurement3 = 0;

                                    nr.CutSheet = DyeBatch.DYEB_BatchNo;

                                    var GriegeProd = context.TLKNI_GreigeProduction.Find(DbDetail.DYEBD_GreigeProduction_FK);
                                    if (GriegeProd != null)
                                    {
                                        nr.PieceNo = GriegeProd.GreigeP_PieceNo;
                                        nr.Quality = context.TLADM_Griege.Find(GriegeProd.GreigeP_Greige_Fk).TLGreige_Description;
                                        var tst = context.TLADM_QualityDefinition.Find(SelectedOpt.TLInter_Knitting_Fk);
                                        if (tst != null)
                                        {
                                            if (tst.QD_ColumnIndex == 1)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas1;
                                            }
                                            else if (tst.QD_ColumnIndex == 2)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas2;
                                            }
                                            else if (tst.QD_ColumnIndex == 3)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas3;
                                            }
                                            else if (tst.QD_ColumnIndex == 4)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas4;
                                            }
                                            else if (tst.QD_ColumnIndex == 5)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas5;
                                            }
                                            else if (tst.QD_ColumnIndex == 6)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas6;
                                            }
                                            else if (tst.QD_ColumnIndex == 7)
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas7;
                                            }
                                            else
                                            {
                                                nr.Measurement1 = GriegeProd.GreigeP_Meas8;
                                            }

                                            var QualExp = context.TLDye_QualityException.Where(x => x.TLDyeIns_GriegeProduct_Fk == DbDetail.DYEBD_GreigeProduction_FK && x.TLDyeIns_QADyeProcessField_Fk == SelectedOpt.TLInter_Dying_Fk).FirstOrDefault();
                                            if (QualExp != null)
                                            {
                                                nr.Measurement2 = QualExp.TLDyeIns_Quantity;
                                            }
                                        }
                                        dataTable2.AddDataTable2Row(nr);
                                    }
                                }
                            }

                        }
                    }
                }


                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet6.DataTable2Row bnr = dataTable2.NewDataTable2Row();
                    bnr.Pk = 1;
                    bnr.ErrorLog = " No Records Found ";
                    dataTable2.AddDataTable2Row(bnr);
                }
                ds.Tables.Add(dataTable2);

                if (_ProdQParms.SelectedOptions[2] || _ProdQParms.SelectedOptions[3])
                {
                    InterDeptComparison SItem = new InterDeptComparison();
                    TextObject text = (TextObject)SItem.ReportDefinition.Sections["Section2"].ReportObjects["Text10"];
                    text.Text = "CutSheet No ";
                    if (_ProdQParms.SelectedOptions[3])
                    {
                        text.Text = "Dye Batch No ";
                    }
                    SItem.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SItem;
                }
                else
                {
                    InterDeptComByDyeBatches SItem = new InterDeptComByDyeBatches();
                    SItem.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SItem;
                }
            }
            else if (_RepNo == 8) //  
            {
                DataSet ds = new DataSet();
                DataSet7.DataTable1DataTable dataTable1 = new DataSet7.DataTable1DataTable();
                DataSet7.DataTable2DataTable dataTable2 = new DataSet7.DataTable2DataTable();
                core = new Util();
                var repo = new PPSRepository();

                DataSet7.DataTable2Row Tab2 = dataTable2.NewDataTable2Row();
                Tab2.FromDate = _ProdQParms.FromDate;
                Tab2.ToDate = _ProdQParms.ToDate;
                Tab2.Pk = 1;
                dataTable2.Rows.Add(Tab2);


                using (var context = new TTI2Entities())
                {
                    var DyeBatchDetail = repo.SelectDskInfo(_ProdQParms);
                    foreach (var Item in DyeBatchDetail)
                    {
                        DataSet7.DataTable1Row Tab1 = dataTable1.NewDataTable1Row();
                        Tab1.Pk = 1;
                        Tab1.DyeDsk = Item.DYEBO_DiskWeight / 100;
                        Tab1.DyeMeters = Item.DYEBO_Meters;
                        Tab1.KnittingMeters = 0.00M;
                        Tab1.TransDate = (DateTime)Item.DYEBO_TransDate;
                        Tab1.DevDyeing = 0.00M;
                        Tab1.DevKnitting = 0.00M;
                        Tab1.StdDsk = 0.00M;
                        var Quality = context.TLADM_Griege.Find(Item.DYEBD_QualityKey);
                        if (Quality != null && Quality.TLGreige_CubicWeight != 0 && Item.DYEBO_DiskWeight != 0)
                        {
                            var FabWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                            if (FabWeight != null)
                            {
                                Tab1.StdFinDsk = (Decimal)FabWeight.FWW_Calculation_Value / 100;
                                Tab1.DevDyeing = core.CalculateDskVariance(Tab1.StdFinDsk, Item.DYEBO_DiskWeight / 100);
                            }

                            Tab1.Quality = Quality.TLGreige_Description;
                            Tab1.StdDsk = Quality.TLGreige_CubicWeight;
                            var DyeBat = context.TLDYE_DyeBatch.Find(Item.DYEBD_DyeBatch_FK);
                            if (DyeBat != null)
                            {
                                Tab1.Colour = context.TLADM_Colours.Find(DyeBat.DYEB_Colour_FK).Col_Display;
                            }

                            var GP = context.TLKNI_GreigeProduction.Find(Item.DYEBD_GreigeProduction_FK);
                            if (GP != null && GP.GreigeP_DskWeight != 0)
                            {
                                Tab1.PieceNo = GP.GreigeP_PieceNo;
                                Tab1.KnittingDsk = GP.GreigeP_DskWeight;
                                Tab1.KnittingMeters = GP.GreigeP_Meters;
                                Tab1.DevKnitting = core.CalculateDskVariance(Quality.TLGreige_CubicWeight, GP.GreigeP_DskWeight);

                                dataTable1.Rows.Add(Tab1);
                            }
                        }
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet7.DataTable1Row Tab1 = dataTable1.NewDataTable1Row();
                    Tab1.Pk = 1;
                    Tab1.ErrorMessage = "No Data Found for Selection Period";
                    dataTable1.Rows.Add(Tab1);
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                DskDeviation SItem = new DskDeviation();
                SItem.SetDataSource(ds);
                crystalReportViewer1.ReportSource = SItem;

            }
            if (_RepNo == 9) // Printing of management summary Finished Goods 
            {
                PPSRepository repo = new PPSRepository();

                DataSet ds = new DataSet();
                DataSet8.DataTable1DataTable dataTable1 = new DataSet8.DataTable1DataTable();

                IList<DATA> MonthsInYear = new List<DATA>();
                
                MonthsInYear.Add(new DATA(1, "C01", "Jan"));
                MonthsInYear.Add(new DATA(2, "C02", "Feb"));
                MonthsInYear.Add(new DATA(3, "C03", "Mar"));
                MonthsInYear.Add(new DATA(4, "C04", "Apr"));
                MonthsInYear.Add(new DATA(5, "C05", "May"));
                MonthsInYear.Add(new DATA(6, "C06", "Jun"));
                MonthsInYear.Add(new DATA(7, "C07", "Jul"));
                MonthsInYear.Add(new DATA(8, "C08", "Aug"));
                MonthsInYear.Add(new DATA(9, "C09", "Sep"));
                MonthsInYear.Add(new DATA(10, "C10", "Oct"));
                MonthsInYear.Add(new DATA(11, "C11", "Nov"));
                MonthsInYear.Add(new DATA(12, "C12", "Dec"));

                var CMth = DateTime.Now.Month;
                if(CMth + 1 < 13)
                {
                    CMth += 1;
                }
                else
                {
                    CMth = 1;
                }

                string[][] ColumnNames = null;
                ColumnNames = new string[][]
                {   new string[] {"Text12", string.Empty, String.Empty},
                    new string[] {"Text13", string.Empty, String.Empty},
                    new string[] {"Text14", string.Empty, String.Empty},
                    new string[] {"Text15", string.Empty, String.Empty},
                    new string[] {"Text16", string.Empty, String.Empty},
                    new string[] {"Text17", string.Empty, String.Empty}
                }; 
                foreach(var Box in ColumnNames)
                {
                    var Record = MonthsInYear.FirstOrDefault(x => x.MonthIndex == CMth);
                    var Index = MonthsInYear.IndexOf(Record);
                    if (Index >= 0)
                    {
                        Box[1] = MonthsInYear[Index].cellvalue;
                        Box[2] = MonthsInYear[Index].MonthName;
                        if (CMth - 1 > 0 )
                        {
                            CMth -= 1;
                        }
                        else
                        {
                            CMth = 12;
                        }
                    }
                    

                   
                }
                using (var context = new TTI2Entities())
                {
                    var OrderT = _dt.Columns.IndexOf("Order Total");
                    var StockT = _dt.Columns.IndexOf("Stock Total");
                    var Diff = _dt.Columns.IndexOf("Difference");
                    //--------------------------------------------------------
                    var EU_DO = _dt.Columns.IndexOf("Expected Units DO");
                    var EU_WIP = _dt.Columns.IndexOf("Expected Units - WIP Dyeing");
                    var EU_WIPDyePrep = _dt.Columns.IndexOf("Expected Units - Dyeing Prep");
                    var EU_FQS = _dt.Columns.IndexOf("Expected Units - Fabric Quarantine Store");
                    var EU_FS =  _dt.Columns.IndexOf("Expected Units - Fabric Store");
                    //-----------------------------------------------------------------------------------------------------
                    var EU_WipCut = _dt.Columns.IndexOf("Expected Units - WIP Cutting");
                    var EU_CutPS = _dt.Columns.IndexOf("Expected Units - CUT Panel Store");
                    var EU_CmtRec = _dt.Columns.IndexOf("Expected Units - CMT Store (Receipt Cage)");
                    var EU_WipCMT = _dt.Columns.IndexOf("Expected Units - CMT WIP");
                    var EU_WipCMTDespatch = _dt.Columns.IndexOf("Expected Units - CMT Store (Despatch Cage)");
                    //---------------------------------------------------------------   
                    var WIP_Total = _dt.Columns.IndexOf("WIP Total");
                    var FirstM = _dt.Columns.IndexOf(ColumnNames[0][2]);
                    var SecondM = _dt.Columns.IndexOf(ColumnNames[1][2]);
                    var ThirdM = _dt.Columns.IndexOf(ColumnNames[2][2]);
                    var FourthM = _dt.Columns.IndexOf(ColumnNames[3][2]);
                    var FiveM = _dt.Columns.IndexOf(ColumnNames[4][2]);
                    var SixM = _dt.Columns.IndexOf(ColumnNames[5][2]);
                    
                    foreach (DataRow row in _dt.Rows)
                    {
                        DataSet8.DataTable1Row Tab1 = dataTable1.NewDataTable1Row();

                        Tab1.Style = context.TLADM_Styles.Find(row.Field<int>(0)).Sty_Description;
                        Tab1.Colour = context.TLADM_Colours.Find(row.Field<int>(1)).Col_Display;
                        Tab1.Size = context.TLADM_Sizes.Find(row.Field<int>(2)).SI_Description;
                        //--------------------------------------------------------
                        Tab1.OrderTotal = row.Field<int>(OrderT);
                        Tab1.StockTotal = row.Field<int>(StockT);
                        Tab1.Difference = row.Field<int>(Diff);
                        //------------------------------------------------
                        Tab1.EU_DyeOrder = row.Field<int>(EU_DO);
                        Tab1.EU_WipDyeingPrep = row.Field<int>(EU_WIPDyePrep);
                        Tab1.EU_WipDyeing = row.Field<int>(EU_WIP);
                        Tab1.EU_DyeFQS = row.Field<int>(EU_FQS);
                        Tab1.EU_DyeFS = row.Field<int>(EU_FS);
                        //----------------------------------------------
                        Tab1.EU_WipCutting = row.Field<int>(EU_WipCut);
                        Tab1.EU_CuttingPS = row.Field<int>(EU_CutPS);
                        //---------------------------------------------------
                        Tab1.EU_CMT_RC = row.Field<int>(EU_CmtRec);
                        Tab1.EU_WipCMT = row.Field<int>(EU_WipCMT);
                        Tab1.EU_CMTDespatch = row.Field<int>(EU_WipCMTDespatch);
                        //--------------------------------------------------
                        Tab1.WipTotal = row.Field<int>(WIP_Total);
                        //---------------------------------------------------
                        Tab1.ExpectedUnits = 0;
                        Tab1.FirstMonth = row.Field<int>(FirstM);
                        Tab1.SecondMonth = row.Field<int>(SecondM);
                        Tab1.ThirdMonth = row.Field<int>(ThirdM);
                        Tab1.FourthMonth = row.Field<int>(FourthM);
                        Tab1.FifthMonth = row.Field<int>(FiveM);
                        Tab1.SixMonth = row.Field<int>(SixM);
                        dataTable1.Rows.Add(Tab1);
                    }
                }

                ds.Tables.Add(dataTable1);
                ProductionPlanning.PPSFinishedGoodsMS SItem = new ProductionPlanning.PPSFinishedGoodsMS();
                IEnumerator ie = SItem.Section2.ReportObjects.GetEnumerator();

                foreach (var Col in ColumnNames)
                {
                    while (ie.MoveNext())
                    {
                        if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                        {
                            CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                            if (!String.IsNullOrEmpty(to.Text))
                            {
                                continue;
                            }

                            to.Text = Col[1];
                            break;


                        }
                    }
                }
                SItem.SetDataSource(ds);
                crystalReportViewer1.ReportSource = SItem;
            }
            if (_RepNo == 10) // Printing of Process Loss across Production 
            {
                PPSRepository repo = new PPSRepository();
                DataSet ds = new DataSet();
                DataSet9.DataTable1DataTable dataTable1 = new DataSet9.DataTable1DataTable();
                DataSet9.DataTable2DataTable dataTable2 = new DataSet9.DataTable2DataTable();
                
                DataSet9.DataTable1Row Tab1 = dataTable1.NewDataTable1Row();
                Tab1.Pk = 1;
                Tab1.FromDate = _ProdQParms.FromDate;
                Tab1.ToDate = _ProdQParms.ToDate;
                Tab1.Title = "Production Loss Per CutSheet Production";
                dataTable1.Rows.Add(Tab1);

                using (var context = new TTI2Entities())
                {
                    var CompletedCutSheets = repo.CSProcessLossAcrossProd(_ProdQParms);
                                
                    foreach (var CutSheet in CompletedCutSheets)
                    {
                            DataSet9.DataTable2Row Tab2 = dataTable2.NewDataTable2Row();
                            Tab2.Pk = 1;
                            Tab2.Style = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            Tab2.Colour = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                            Tab2.Size = context.TLADM_Sizes.Find(CutSheet.TLCutSH_Size_FK).SI_Description;
                            Tab2.CutSheetNo = CutSheet.TLCutSH_No;

                            var CS_Pk       = CutSheet.TLCutSH_Pk;
                            var YieldFactor = 0.00M;
                            var Width       = 0.00M;
                            var Weight      = 0.00M;

                            var BatchDetails = (from T1 in context.TLCUT_CutSheetDetail
                                            join T2 in context.TLDYE_DyeBatchDetails
                                            on T1.TLCutSHD_DyeBatchDet_FK equals T2.DYEBD_Pk
                                            where T1.TLCutSHD_CutSheet_FK == CutSheet.TLCutSH_Pk && T2.DYEBD_BodyTrim
                                            select T2).ToList();

                            if (BatchDetails.Count == 0)
                                continue;
                            
                            var ProductRating_FK = BatchDetails.FirstOrDefault().DYEBO_ProductRating_FK;
                            var Quality = context.TLADM_Griege.Find(BatchDetails.FirstOrDefault().DYEBD_QualityKey);
                            if(Quality != null)
                            {
                                var WidthDetails = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                if(WidthDetails != null)
                                {
                                    Width = WidthDetails.FW_Calculation_Value;
                                }

                                var WeightDetails  =  context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                if (WeightDetails != null)
                                {
                                    Weight = WeightDetails.FWW_Calculation_Value;
                                }

                                if (Width > 0 && Weight > 0)
                                {
                                    YieldFactor = 50000.00M / Width / Weight;
                                }

                            }

                            var ActualRating = context.TLADM_ProductRating.Find(ProductRating_FK).Pr_numeric_Rating;
                            //==================================================
                            // 
                            //================================================================
                            var GrossWeight = BatchDetails.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                            var NettWeight = BatchDetails.Sum(x=>(decimal ?)x.DYEBO_Nett) ?? 0.00M;
                            
                            if (YieldFactor > 0 && ActualRating > 0 && NettWeight > 0)
                            {
                                 //Dye Loss 
                                 var GWeight = (GrossWeight * (decimal)0.95) * (decimal)0.95;
                                 Tab2.ManufactStd = (int)Math.Round((YieldFactor / ActualRating) * GWeight, 0);
                            }
                            else
                            {
                                Tab2.ManufactStd = 0;
                            }
                            
                            Tab2.Greige = GrossWeight;
                            Tab2.Fabric = NettWeight;
                        
                            Tab2.ProcessLoss_Kg = NettWeight - GrossWeight;
                            Tab2.ProcessLoss_Perc = Tab2.ProcessLoss_Kg / GrossWeight * 100;

                            //===========================================================================
                            //
                            //=======================================================

                            Tab2.ExpectedCutting = context.TLCUT_ExpectedUnits.Where(x=>x.TLCUTE_CutSheet_FK== CS_Pk).Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0;
                            Tab2.ActualCutting = (from T1 in context.TLCUT_CutSheetReceipt
                                                      join T2 in context.TLCUT_CutSheetReceiptDetail
                                                      on T1.TLCUTSHR_Pk equals T2.TLCUTSHRD_CutSheet_FK
                                                      where T1.TLCUTSHR_CutSheet_FK == CS_Pk
                                                      select T2).Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;
                            
                            Tab2.CuttingProcessLoss = Tab2.ActualCutting - Tab2.ExpectedCutting;
                            Tab2.CuttingProcessLoss_Perc = (decimal)Tab2.CuttingProcessLoss / (decimal)Tab2.ExpectedCutting * 100;

                            Tab2.CuttingWaste_Kg = -1 * context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CS_Pk).FirstOrDefault().TLCUTSHR_WasteCutSheet;
                            Tab2.CuttingWaste_Perc = (decimal)Tab2.CuttingWaste_Kg / (decimal)Tab2.Fabric * 100;

                            Tab2.CMTPanelQty = Tab2.ActualCutting;
                            Tab2.CMTGarmentsQty = (from T1 in context.TLCMT_LineIssue
                                                       join T2 in context.TLCMT_CompletedWork
                                                       on T1.TLCMTLI_CutSheet_FK equals T2.TLCMTWC_CutSheet_FK
                                                       where T1.TLCMTLI_CutSheet_FK == CS_Pk
                                                       && T1.TLCMTLI_WorkCompleted && T2.TLCMTWC_Grade.Contains("A")
                                                       select T2).Sum(x =>(int ?) x.TLCMTWC_Qty) ?? 0 ;

                            Tab2.CMTLoss = Tab2.CMTGarmentsQty - Tab2.CMTPanelQty;
                            Tab2.CMTLoss_Perc = (decimal)Tab2.CMTLoss / (decimal)Tab2.CMTPanelQty * 100;

                            dataTable2.Rows.Add(Tab2);
                        }
                }
                
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ProductionPlanning.ProcessLossOverProd PLossOverProd = new ProductionPlanning.ProcessLossOverProd();
                PLossOverProd.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PLossOverProd;
            }

            if (_RepNo == 11) // WIP Quick Look for Expedite
            {
                PPSRepository repo = new PPSRepository();

                DataSet ds = new DataSet();
                DataSet10.DataTable1DataTable dataTable1 = new DataSet10.DataTable1DataTable();

                
                IList<TLCSV_PuchaseOrderDetail> PODetail = new List<TLCSV_PuchaseOrderDetail>();
                IList<TLADM_Griege> _Qualities = new List<TLADM_Griege>();
                List<PPOrdersDATA> OutOrders = new List<PPOrdersDATA>();
                List<PPStockDATA> SOHStock = new List<PPStockDATA>();

                List<TLPPS_Replenishment> PPS = repo.PPSQuery(_ProdQParms).ToList();
                using (var context = new TTI2Entities())
                {

                    IList<TLADM_Styles> Styles = context.TLADM_Styles.ToList();
                    IList<TLADM_Colours> Colours = context.TLADM_Colours.ToList();
                    IList<TLADM_Sizes> Sizes = context.TLADM_Sizes.ToList();

                    if (_ProdQParms.Styles.Count != 0)
                    {
                        Styles = _ProdQParms.Styles.ToList();
                    }

                    if (_ProdQParms.Colours.Count != 0)
                    {
                        Colours = _ProdQParms.Colours.ToList();
                    }

                    if (_ProdQParms.Sizes.Count != 0)
                    {
                        Sizes = _ProdQParms.Sizes.ToList();
                    }

                    //***
                    PODetail = (from T1 in context.TLCSV_PurchaseOrder
                                join T2 in context.TLCSV_PuchaseOrderDetail on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                where !T1.TLCSVPO_Closeed && !T2.TLCUSTO_Closed
                                select T2).ToList();

                    var SOH = context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked
                                            && x.TLSOH_Is_A
                                            && !x.TLSOH_Write_Off
                                            && !x.TLSOH_Returned
                                            && !x.TLSOH_Split).ToList();

                    foreach (var Item in PPS)
                    {

                        DataSet10.DataTable1Row row = dataTable1.NewDataTable1Row();

                        row.Style = Item.TLREP_Style_FK.ToString();
                        row.Colour = Item.TLREP_Colour_FK.ToString();
                        row.Size = Item.TLREP_Size_FK.ToString();

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
                        //Get all the outstandings orders at this point in time
                        //    for this style, colour, size combination and the order line must not be closed as well as the order itself
                        //============================================================================
                        //var Orders = PODetail.Where(x => x.TLCUSTO_Style_FK == Item.TLREP_Style_FK && x.TLCUSTO_Colour_FK == Item.TLREP_Colour_FK && x.TLCUSTO_Size_FK == Item.TLREP_Size_FK);
                        //if (Orders != null)
                        //{
                        //    var GrpByCustomer = Orders.GroupBy(x => x.TLCUSTO_Customer_FK);
                        //    foreach (var Grouped in GrpByCustomer)
                        //    {

                        //        TLCSV_PuchaseOrderDetail Order = Grouped.FirstOrDefault(); // Grouped.FirstOrDefault();
                        //        var CustDetail = context.TLADM_CustomerFile.Find(Order.TLCUSTO_Customer_FK);
                        //        ColIndex = dt.Columns.IndexOf(CustDetail.Cust_Code);

                        //        var QtyOrdered = Grouped.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0; //  LineOrder.TLCUSTO_Qty;
                        //        var AllReadySold = Grouped.Sum(x => (int?)x.TLCUSTO_QtyDelivered_ToDate) ?? 0; // LineOrder.TLCUSTO_QtyDelivered_ToDate;  //  context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == LineOrder.TLCUSTO_Pk && x.TLSOH_Sold).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                        //        var AllReadyPicked = Grouped.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0; //  LineOrder.TLCUSTO_QtyPicked_ToDate;     //  context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == LineOrder.TLCUSTO_Pk && !x.TLSOH_Sold && x.TLSOH_Picked).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                        //        var Nett = QtyOrdered - AllReadyPicked;
                        //        if (Nett > 0 && ColIndex >= 0)
                        //        {
                        //            Row[ColIndex] = Row.Field<int>(ColIndex) + Nett;
                        //            Row[OTIndex] = Row.Field<int>(OTIndex) + Nett;  //Row.Field<int>(ColIndex);
                        //        }

                        //    }
                        //}

                        // Filter orders based on style, color, and size
                        var Orders = PODetail.Where(x => x.TLCUSTO_Style_FK == Item.TLREP_Style_FK &&
                                                          x.TLCUSTO_Colour_FK == Item.TLREP_Colour_FK &&
                                                          x.TLCUSTO_Size_FK == Item.TLREP_Size_FK);

                        // Check if there are any matching orders
                        if (Orders != null && Orders.Any())
                        {
                            // Sum up quantities directly
                            var QtyOrdered = Orders.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0;
                            var AllReadySold = Orders.Sum(x => (int?)x.TLCUSTO_QtyDelivered_ToDate) ?? 0;
                            var AllReadyPicked = Orders.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0;
                            var Nett = QtyOrdered - AllReadyPicked;

                            row.TotalOrders = Nett.ToString();
                        }




                        //-------------------------------------------------------------------------------------------------------------
                        // 2nd Task is to calculate the Total Stock On Hand for a particular style, colour, size combination
                        //----------------------------------------------------------------------------------------------------
                        //var ItemSOH = SOH.Where(x => x.TLSOH_Style_FK == Item.TLREP_Style_FK && x.TLSOH_Colour_FK == Item.TLREP_Colour_FK && x.TLSOH_Size_FK == Item.TLREP_Size_FK).GroupBy(x => x.TLSOH_WareHouse_FK);
                        //foreach (var Grouped in ItemSOH)
                        //{
                        //    TLCSV_StockOnHand Stock = new TLCSV_StockOnHand();
                        //    Stock = Grouped.FirstOrDefault();

                        //    //============================================================
                        //    //Now check whether the WareHouse "exists" on the datatable
                        //    //==========================================================================
                        //    var WhseDetail = context.TLADM_WhseStore.Find(Stock.TLSOH_WareHouse_FK);

                        //    ColIndex = dt.Columns.IndexOf(WhseDetail.WhStore_Code);
                        //    if (ColIndex != -1)
                        //    {
                        //        Row[ColIndex] = Row.Field<int>(ColIndex) + Grouped.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                        //        Row[STIndex] = Row.Field<int>(STIndex) + Row.Field<int>(ColIndex);
                        //    }
                        //}

                        // Filter stock based on style, color, and size
                        var ItemSOH = SOH.Where(x => x.TLSOH_Style_FK == Item.TLREP_Style_FK &&
                                                     x.TLSOH_Colour_FK == Item.TLREP_Colour_FK &&
                                                     x.TLSOH_Size_FK == Item.TLREP_Size_FK);

                        // Check if there are any matching stock items
                        if (ItemSOH != null && ItemSOH.Any())
                        {
                            // Sum up boxed quantities directly
                            var TotalBoxedQty = ItemSOH.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;

                            // Iterate through unique warehouses in the filtered stock
                            foreach (var warehouseId in ItemSOH.Select(x => x.TLSOH_WareHouse_FK).Distinct())
                            {
                                // Find the warehouse details
                                var WhseDetail = context.TLADM_WhseStore.Find(warehouseId);

                                row.AvailableStock = TotalBoxedQty.ToString();


                            }
                        }


                        //--------------------------------------------------------------------
                        // We need to find the difference Stock - Orders
                        //------------------------------------------------------------------------
                        //Row[DiffIndex] = Row.Field<int>(STIndex) - Row.Field<int>(OTIndex);

                        //-------------------------------------------------------------------------------------------------------------
                        // 9th Task (a) Expected Units at CMT Store in Panel Receipt Cage
                        //----------------------------------------------------------------------------------------------------
                        //("Expected Units - CMT Store (Receipt Cage)");
                        var CMTPS = CMTPanelStore.Where(x => x.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && x.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && x.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK).ToList();
                        if (CMTPS.Count != 0)
                        {
                            var Answer = CMTPS.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                            Answer = Convert.ToInt32(Answer * 0.95);
                            row.CMTPanels = Answer.ToString();
                        }
                        //---------------------------------
                        // Bring the WIP Total Up to date
                        //-------------------------------------------------------
                        //Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                        //-------------------------------------------------------------------------------------------------------------
                        // 10th Task (b) Expected Units at CMT Store (WIP)
                        //----------------------------------------------------------------------------------------------------
                        //("Expected Units - CMT WIP");
                        if (CMTWIP.Count() != 0)
                        {
                            var WIP = CMTWIP.Where(x => x.TLCUTSHR_Style_FK == Item.TLREP_Style_FK && x.TLCUTSHR_Colour_FK == Item.TLREP_Colour_FK && x.TLCUTSHRD_Size_FK == Item.TLREP_Size_FK).ToList();
                            if (WIP.Count != 0)
                            {
                                var Answer = WIP.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                row.CMTWIP = Answer.ToString();
                            }
                        }
                        //---------------------------------
                        // Bring the WIP Total Up to date
                        //-------------------------------------------------------
                        //Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);

                        //-------------------------------------------------------------------------------------------------------------
                        // 11th Task (c) Expected Units at CMT Store in Finished Goods Despatch Cage
                        //----------------------------------------------------------------------------------------------------
                        //("Expected Units - CMT Store (Despatch Cage)");
                        var QueryC = from T1 in context.TLCMT_CompletedWork
                                     where (!T1.TLCMTWC_Despatched || T1.TLCMTWC_Despatched && !T1.TLCMTWC_BoxReceiptedWhse) && T1.TLCMTWC_Style_FK == Item.TLREP_Style_FK && T1.TLCMTWC_Colour_FK == Item.TLREP_Colour_FK && T1.TLCMTWC_Size_FK == Item.TLREP_Size_FK
                                     select new { T1.TLCMTWC_Style_FK, T1.TLCMTWC_Colour_FK, T1.TLCMTWC_Size_FK, T1.TLCMTWC_Qty };
                        if (QueryC.Count() != 0)
                        {
                            row.CMTDespatch = QueryC.Sum(x => (int?)x.TLCMTWC_Qty).ToString() ?? "0";
                        }

                        //---------------------------------
                        // Bring the WIP Total Up to date
                        //-------------------------------------------------------
                        //Row[WIPIndex] = Row.Field<int>(WIPIndex) + Row.Field<int>(ColIndex);


                        //ReOrder Level
                        row.ROL = Item.TLREP_ReOrderLevel.ToString();


                    }

                    //***

                    //Get CMT Values
                    //var cmtPanels = repo.CMTPanels(_ProdQParms);
                    //var cmtWIP = repo.CMTWIPLineIssues(_ProdQParms);
                    //var cmtDespatchLineIssues = repo.CMTDespatchLineIssues(_ProdQParms);
                    
                    //foreach (var lineIssue in cmtDespatchLineIssues)
                    //{
                    //    int Units_Per_Bag = 0;

                    //    var CSReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == lineIssue.TLCMTLI_CutSheet_FK).FirstOrDefault();
                    //    if (CSReceipt != null)
                    //    {

                    //        var CS = context.TLCUT_CutSheet.Find(CSReceipt.TLCUTSHR_CutSheet_FK);
                    //        if (CS != null)
                    //        {
                    //            var cutSheet = CS.TLCutSH_No;
                    //            var cmt = context.TLADM_Departments.Find(lineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;
                    //            var colour = Colours.FirstOrDefault(s => s.Col_Id == CSReceipt.TLCUTSHR_Colour_FK).Col_Display;

                    //            var style = Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK);
                    //            if (style != null)
                    //            {
                    //                var quality = style.Sty_Description;
                    //                Units_Per_Bag = style.Sty_Bags;
                    //            }
                    //        }

                    //        //var CutSheetDet = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSReceipt.TLCUTSHR_Pk);
                    //        //if (CutSheetDet.Count() != 0)
                    //        //{


                    //        //    var ExpectedUnitsGrouped = CutSheetDet.GroupBy(x => x.TLCUTSHRD_Size_FK);
                    //        //    foreach (var Group in ExpectedUnitsGrouped)
                    //        //    {
                    //        //        var SizePk = Group.FirstOrDefault().TLCUTSHRD_Size_FK;
                    //        //        var Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk);
                    //        //        if (Size != null)
                    //        //        {
                    //        //            var EUnits = Group.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                    //        //            if (Size.SI_ColNumber == 1)
                    //        //                nr.Col1 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 2)
                    //        //                nr.Col2 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 3)
                    //        //                nr.Col3 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 4)
                    //        //                nr.Col4 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 5)
                    //        //                nr.Col5 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 6)
                    //        //                nr.Col6 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 7)
                    //        //                nr.Col7 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 8)
                    //        //                nr.Col8 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 9)
                    //        //                nr.Col9 += EUnits;
                    //        //            else if (Size.SI_ColNumber == 10)
                    //        //                nr.Col10 += EUnits;
                    //        //            else
                    //        //                nr.Col11 += EUnits;
                    //        //        }
                    //        //    }
                    //        //    nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                    //        //}

                    //        //nr.Date = LineIssue.TLCMTLI_Date;
                    //        //nr.Hold = LineIssue.TLCMTLI_OnHold;
                    //        //nr.Priority = LineIssue.TLCMTLI_Priority;

                    //        //nr.Wip = 0;
                    //        //nr.DespatchCage = 0;
                    //        //nr.ReceiptCage = 0;

                    //        //if (!LineIssue.TLCMTLI_OnHold)
                    //        //{
                    //        //    if (!LineIssue.TLCMTLI_WorkCompleted)
                    //        //    {
                    //        //        if (!LineIssue.TLCMTLI_IssuedToLine)
                    //        //            nr.ReceiptCage = nr.Total;
                    //        //        else
                    //        //            nr.Wip = nr.Total;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        var CompletedW = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_LineIssue_FK == LineIssue.TLCMTLI_Pk).ToList();
                    //        //        if (CompletedW.Count != 0)
                    //        //        {
                    //        //            if (!CompletedW.FirstOrDefault().TLCMTWC_Despatched)
                    //        //                nr.DespatchCage = CompletedW.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                    //        //        }
                    //        //    }
                    //        //}
                    //        ///####
                    //        //else
                    //        //{
                    //        //    nr.UnitsOnHold = nr.Total;
                    //        //}

                    //        //if (nr.Wip + nr.DespatchCage + nr.ReceiptCage == 0 && !LineIssue.TLCMTLI_OnHold)
                    //        //    continue;

                    //        //if (Units_Per_Bag != 0)
                    //        //{
                    //        //    var Res = (Decimal)nr.ReceiptCage / Units_Per_Bag;
                    //        //    nr.Bags = (int)Math.Round(Res, 0);
                    //        //}
                    //        //dataTable1.AddDataTable1Row(nr);
                    //    }
                    //}





////*****
                    // Mapping customer IDs to customer codes
                    //var customerMapping = new List<CustomerMapping>
                    //{
                    //    new CustomerMapping { Customer_FK = 15, CustomerCode = "GPV001" },
                    //    new CustomerMapping { Customer_FK = 16, CustomerCode = "KZNVIC" },
                    //    new CustomerMapping { Customer_FK = 20, CustomerCode = "TCT001" },
                    //    new CustomerMapping { Customer_FK = 23, CustomerCode = "TLWEC" }
                    //};

                    //// Fetch raw data including total garments per order
                    //var rawData = (from oa in context.TLCSV_OrderAllocated
                    //               join po in context.TLCSV_PurchaseOrder on oa.TLORDA_POOrder_FK equals po.TLCSVPO_Pk
                    //               join pod in context.TLCSV_PuchaseOrderDetail on po.TLCSVPO_Pk equals pod.TLCUSTO_Pk
                    //               join s in context.TLADM_Styles on pod.TLCUSTO_Style_FK equals s.Sty_Id
                    //               join c in context.TLADM_Colours on pod.TLCUSTO_Colour_FK equals c.Col_Id
                    //               join si in context.TLADM_Sizes on pod.TLCUSTO_Size_FK equals si.SI_id
                    //               join soh in context.TLCSV_StockOnHand on pod.TLCUSTO_Pk equals soh.TLSOH_POOrderDetail_FK
                    //               join ws in context.TLADM_WhseStore on soh.TLSOH_WareHouse_FK equals ws.WhStore_Id
                    //               join repl in context.TLPPS_Replenishment on new { s.Sty_Id, c.Col_Id, si.SI_id } equals new { Sty_Id = repl.TLREP_Style_FK, Col_Id = repl.TLREP_Colour_FK, SI_id = repl.TLREP_Size_FK }
                                   
                    //               //join d in context.TLADM_Departments on po.DepartmentId equals d.Dep_Id // Adjust the field name accordingly
                    //               where oa.TLORDA_Delivered == false
                    //                     // Commenting out the department condition
                    //                     //&& _ProdQParms.Departments.Any(dep => dep.Dep_Id == d.Dep_Id) // Join on department
                    //                     && _ProdQParms.Styles.Any(st => st.Sty_Id == s.Sty_Id)
                    //                     && _ProdQParms.Colours.Any(cl => cl.Col_Id == c.Col_Id)
                    //                     && _ProdQParms.Sizes.Any(sz => sz.SI_id == si.SI_id)
                    //               select new
                    //               {
                    //                   oa.TLORDA_Customer_FK,
                    //                   s.Sty_Description,
                    //                   c.Col_Description,
                    //                   si.SI_Description,
                    //                   soh.TLSOH_BoxedQty,
                    //                   ROL = repl.TLREP_ReOrderLevel, // Correct ROL value
                    //                                                  // Calculation for CMT_Panels
                    //                   CMT_Panels = context.TLCMT_LineIssue
                    //                                  .Where(li => li.TLCMTLI_IssuedToLine == false && li.TLCMTLI_WorkCompleted == false)
                    //                                  .Join(context.TLCUT_CutSheetReceiptDetail,
                    //                                        li => li.TLCMTLI_CutSheet_FK,
                    //                                        csrd => csrd.TLCUTSHRD_CutSheet_FK,
                    //                                        (li, csrd) => csrd.TLCUTSHRD_BundleQty - csrd.TLCUTSHRD_RejectQty).Sum(),
                    //                   // Calculation for CMT_WIP
                    //                   CMT_WIP = context.TLCMT_LineIssue
                    //                                .Where(li => li.TLCMTLI_IssuedToLine == true && li.TLCMTLI_WorkCompleted == false)
                    //                                .Join(context.TLCUT_CutSheetReceiptDetail,
                    //                                      li => li.TLCMTLI_CutSheet_FK,
                    //                                      csrd => csrd.TLCUTSHRD_CutSheet_FK,
                    //                                      (li, csrd) => csrd.TLCUTSHRD_BundleQty - csrd.TLCUTSHRD_RejectQty).Sum(),
                    //                   // Calculation for CMT_Despatch
                    //                   CMT_Despatch = context.TLCMT_LineIssue
                    //                                        .Where(li => li.TLCMTLI_WorkCompleted == true)
                    //                                        .Join(context.TLCMT_CompletedWork,
                    //                                              li => li.TLCMTLI_Pk,
                    //                                              cw => cw.TLCMTWC_LineIssue_FK,
                    //                                              (li, cw) => new { li, cw })
                    //                                        .Where(x => x.cw.TLCMTWC_Despatched == false)
                    //                                        .Sum(x => (int?)x.cw.TLCMTWC_Qty) ?? 0,
                    //                   // Total garments for each order
                    //                   TotalGarments = soh.TLSOH_BoxedQty
                    //               }).ToList();

                    //// Group data by style, color, and size, and aggregate the required values
                    //var result = from data in rawData
                    //             join cm in customerMapping on data.TLORDA_Customer_FK equals cm.Customer_FK
                    //             group new { data, cm } by new { data.Sty_Description, data.Col_Description, data.SI_Description } into g
                    //             select new
                    //             {
                    //                 Style = g.Key.Sty_Description,
                    //                 Colour = g.Key.Col_Description,
                    //                 Size = g.Key.SI_Description,
                    //                 // Summing total garments for each customer code
                    //                 GPV001 = g.Where(x => x.cm.CustomerCode == "GPV001").Sum(x => x.data.TotalGarments),
                    //                 KZNVIC = g.Where(x => x.cm.CustomerCode == "KZNVIC").Sum(x => x.data.TotalGarments),
                    //                 TCT001 = g.Where(x => x.cm.CustomerCode == "TCT001").Sum(x => x.data.TotalGarments),
                    //                 TLWEC = g.Where(x => x.cm.CustomerCode == "TLWEC").Sum(x => x.data.TotalGarments),
                    //                 // Total garments for all orders
                    //                 Total_Orders = g.Sum(x => x.data.TotalGarments),
                    //                 Available_Stock = g.Sum(x => x.data.TLSOH_BoxedQty),
                    //                 // Calculating the difference between available stock and total orders
                    //                 Minus_Orders = g.Sum(x => x.data.TLSOH_BoxedQty) - g.Sum(x => x.data.TotalGarments),
                    //                 CMT_Panels = g.Sum(x => x.data.CMT_Panels),
                    //                 CMT_WIP = g.Sum(x => x.data.CMT_WIP),
                    //                 CMT_Despatch = g.Sum(x => x.data.CMT_Despatch),
                    //                 ROL = g.Max(x => x.data.ROL), // Including the ROL column
                    //                 Expedite = g.Max(x => x.data.ROL) - (g.Sum(x => x.data.TLSOH_BoxedQty) - g.Sum(x => x.data.TotalGarments) + g.Sum(x => x.data.CMT_Panels) + g.Sum(x => x.data.CMT_WIP) + g.Sum(x => x.data.CMT_Despatch)) // Calculating Expedite
                    //             };

                    //// Sorting the result
                    //var sortedResult = result.OrderBy(r => r.Style)
                    //                         .ThenBy(r => r.Colour)
                    //                         .ThenBy(r => r.Size)
                    //                         .ToList();

                    //// Adding sorted result to the DataTable
                    //foreach (var r in sortedResult)
                    //{
                    //    DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    //    nr.Colour = r.Colour;
                    //    nr.Style = r.Style;
                    //    nr.Size = r.Size;
                    //    nr.AvailableStock = r.Available_Stock.ToString();
                    //    nr.TotalOrders = r.Total_Orders.ToString();
                    //    nr.KZNVIC = r.KZNVIC.ToString();
                    //    nr.GPV001 = r.GPV001.ToString();
                    //    nr.TCT001 = r.TCT001.ToString();
                    //    nr.TLWEC = r.TLWEC.ToString();
                    //    nr.DifferenceStock = r.Minus_Orders.ToString();
                    //    nr.CMTWIP = r.CMT_WIP.ToString();
                    //    nr.CMTPanels = r.CMT_Panels.ToString();
                    //    nr.CMTDespatch = r.CMT_Despatch.ToString();
                    //    nr.ROL = r.ROL.ToString(); // Adding ROL value to the DataTable row
                    //    nr.Expedite = r.Expedite.ToString(); // Adding Expedite value to the DataTable row

                    //    dataTable1.AddDataTable1Row(nr);
                    //}
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    //nr.ErrorLog = "No record found pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);

                }
                ds.Tables.Add(dataTable1);
                WIPQuickLook wipQuickLook = new WIPQuickLook();
                wipQuickLook.SetDataSource(ds);
                crystalReportViewer1.ReportSource = wipQuickLook;


            }

            crystalReportViewer1.Refresh();
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

        public struct DATA
        {
            public int MonthIndex;
            // Name of the button selected 
            public string MonthName;
            // Value of the button X / O
            public string cellvalue;

            public DATA(int MonthInd, string MName, string value)
            {
                this.MonthIndex = MonthInd;
                this.MonthName = MName;
                this.cellvalue = value;
            }
        }
        public class TopSellers
        {
            public TopSellers(int Styles, int Clrs, int Szs, int TBoxQty, int BoxQty)
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
