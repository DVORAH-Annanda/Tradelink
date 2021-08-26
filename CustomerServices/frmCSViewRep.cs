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
using System.Collections;

namespace CustomerServices
{
    public partial class frmCSViewRep : Form
    {
        int _RepNo;
        int _Pk;
        CustomerServicesParameters _QueryParms;
        CSVServices _Svces;

        IList<TLADM_Styles> _Styles;
        IList<TLADM_Colours> _Colours;
        IList<TLADM_Sizes> _Sizes;
        IList<TLADM_CustomerFile> _Customers;
        IList<TLADM_Griege> _Qualities; 

        public frmCSViewRep()
        {
            InitializeComponent();
        }

        public frmCSViewRep(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmCSViewRep(int RepNo, int Pk)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Pk = Pk;
        }

        public frmCSViewRep(int RepNo, CSVServices Svcs)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Svces = Svcs;
        }

        public frmCSViewRep(int RepNo, CSVServices Svcs, int Pk)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Pk = Pk;
            _Svces = Svcs;
        }

        public frmCSViewRep(int RepNo, CustomerServicesParameters QParms, CSVServices Svcs)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _QueryParms = QParms;
            _Svces = Svcs;
        }

        public frmCSViewRep(int RepNo, CustomerServicesParameters QParms)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _QueryParms = QParms;
        }

        private void frmCSViewRep_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                _Styles = context.TLADM_Styles.ToList();
                _Colours = context.TLADM_Colours.ToList();
                _Sizes = context.TLADM_Sizes.ToList();
                _Customers = context.TLADM_CustomerFile.ToList();
                _Qualities = context.TLADM_Griege.ToList();

            }

            if (_RepNo == 1)
            {
                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable dataTable2 = new DataSet1.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Box = context.TLCSV_BoxSelected.Find(_Pk);
                    if (Box != null)
                    {
                        DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = _Pk;
                        try
                        {
                            nr.Date = (DateTime)Box.TLCSV_TransDate;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        nr.FromFacility = context.TLADM_Departments.Find(Box.TLCSV_From_FK).Dep_Description;
                        nr.ToFacility = context.TLADM_WhseStore.Find(Box.TLCSV_To_FK).WhStore_Description;
                        nr.TransNumber = "CP" + Box.TLCSV_TransNumber.ToString().PadLeft(5, '0');

                        dataTable1.AddDataTable1Row(nr);

                        var CompletedWork = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_PickList_FK == Box.TLCSV_Pk).ToList();
                        foreach (var Record in CompletedWork)
                        {
                            DataSet1.DataTable2Row row = dataTable2.NewDataTable2Row();
                            row.Pk = _Pk;
                            row.Grade = Record.TLCMTWC_Grade;
                            row.BoxNumber = Record.TLCMTWC_BoxNumber;
                            row.BoxQty = Record.TLCMTWC_Qty;
                            row.Weight = Record.TLCMTWC_Weight;
                            row.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLCMTWC_Style_FK).Sty_Description;
                            row.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.TLCMTWC_Colour_FK).Col_Display;
                            row.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLCMTWC_Size_FK).SI_Description;
                            row.BoxQty = Record.TLCMTWC_Qty;
                            

                            dataTable2.AddDataTable2Row(row);

                            var Updt = context.TLCMT_CompletedWork.Find(Record.TLCMTWC_Pk);
                            if (Updt != null)
                            {
                                Updt.TLCMTWC_Picked = true;
                            }
                        }

                        Box.TLCSV_PickingList = true;
                        Box.TLCSV_PLTransDate = DateTime.Now;

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CMTPickList PickList = new CMTPickList();
                PickList.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PickList;


            }
            else if (_RepNo == 2)
            {
                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable dataTable2 = new DataSet1.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Box = context.TLCSV_BoxSelected.Find(_Pk);
                    if (Box != null)
                    {
                        DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = _Pk;

                        try
                        {
                            nr.Date = (DateTime)Box.TLCSV_TransDate;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        nr.FromFacility = context.TLADM_Departments.Find(Box.TLCSV_From_FK).Dep_Description;
                        nr.ToFacility = context.TLADM_WhseStore.Find(Box.TLCSV_To_FK).WhStore_Description;
                        nr.TransNumber = "CD" + Box.TLCSV_TransNumber.ToString().PadLeft(5, '0');

                        dataTable1.AddDataTable1Row(nr);

                        var CompletedWork = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_DepatchedList_FK == Box.TLCSV_Pk).ToList();
                        foreach (var Record in CompletedWork)
                        {
                            DataSet1.DataTable2Row row = dataTable2.NewDataTable2Row();
                            row.Pk = _Pk;
                            row.Grade = Record.TLCMTWC_Grade;
                            row.BoxNumber = Record.TLCMTWC_BoxNumber;
                            row.BoxQty = Record.TLCMTWC_Qty;
                            row.Weight = Record.TLCMTWC_Weight;
                            row.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLCMTWC_Style_FK).Sty_Description;
                            row.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.TLCMTWC_Colour_FK).Col_Display;
                            row.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLCMTWC_Size_FK).SI_Description;
                            row.BoxQty = Record.TLCMTWC_Qty;
                            row.PLDetails = context.TLCSV_BoxSelected.Find(Record.TLCMTWC_PickList_FK).TLCSV_PLDetails;
                            dataTable2.AddDataTable2Row(row);

                        }
                    }
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                CMTDeliveryNote DNote = new CMTDeliveryNote();
                DNote.SetDataSource(ds);
                crystalReportViewer1.ReportSource = DNote;
            }
            else if (_RepNo == 3)
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable dataTable1 = new DataSet2.DataTable1DataTable();
                DataSet2.DataTable2DataTable dataTable2 = new DataSet2.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Box = context.TLCSV_BoxSelected.Find(_Pk);
                    if (Box != null)
                    {
                        DataSet2.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = _Pk;
                        nr.Title = "Boxes receipted at ";
                        nr.FromCMT = context.TLADM_Departments.Find(Box.TLCSV_From_FK).Dep_Description;
                        nr.ToWhse = context.TLADM_WhseStore.Find(Box.TLCSV_To_FK).WhStore_Description;
                        nr.Date = (DateTime)Box.TLCSV_DateReceipted;

                        dataTable1.AddDataTable1Row(nr);

                        var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxSelected_FK == _Pk).ToList();
                        foreach (var row in Existing)
                        {
                            var Cmt = context.TLCMT_CompletedWork.Find(row.TLSOH_CMT_FK);
                            if (Cmt != null)
                            {
                                DataSet2.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                                dtr.Pk = _Pk;
                                dtr.BoxNumber = Cmt.TLCMTWC_BoxNumber;
                                dtr.Grade = Cmt.TLCMTWC_Grade;
                                dtr.Notes = row.TLSOH_Notes;
                                dtr.Weight = (decimal)Cmt.TLCMTWC_Weight;
                                dtr.BoxType = context.TLADM_BoxTypes.Find(Cmt.TLCMTWC_BoxType_FK).TLADMBT_Description;

                                dataTable2.AddDataTable2Row(dtr);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                WhseReceipts whseReceipts = new WhseReceipts();
                whseReceipts.SetDataSource(ds);
                crystalReportViewer1.ReportSource = whseReceipts;
            }
            else if (_RepNo == 4)    // Picking List 
            {
                DataSet ds = new DataSet();
                DataSet3.DataTable1DataTable dataTable1 = new DataSet3.DataTable1DataTable();
                List<WhseDATA> WhsePickingSlipNumbers = new List<WhseDATA>();
                IList<TLCSV_StockOnHand> StockOnHand = null;

                using (var context = new TTI2Entities())
                {
                    var PLNumbers = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                    foreach (var PLNumber in PLNumbers)
                    {
                        var PLDetail = new WhseDATA();
                        PLDetail.Whse_FK = PLNumber.WhStore_Id;
                        PLDetail.PickingListNo = PLNumber.WhStore_PickingList;
                        PLDetail.Selected = false;
                        WhsePickingSlipNumbers.Add(PLDetail);
                    }

                    StockOnHand = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == _Pk).ToList();

                    StockOnHand = (from soh in StockOnHand
                                   join style in context.TLADM_Styles on soh.TLSOH_Style_FK equals style.Sty_Id
                                   orderby style.Sty_DisplayOrder
                                   select soh).ToList();

                    foreach (var Item in StockOnHand)
                    {
                        DataSet3.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.WareHouse = context.TLADM_WhseStore.Find(Item.TLSOH_WareHouse_FK).WhStore_Description;
                        //============================================================
                        if (!_Svces.PLReprint)
                        {
                            var record = WhsePickingSlipNumbers.Find(x => x.Whse_FK == Item.TLSOH_WareHouse_FK);
                            var index = WhsePickingSlipNumbers.IndexOf(record);
                            record.Selected = true;
                            WhsePickingSlipNumbers[index] = record;
                            nr.PickingSlipNumber = record.PickingListNo;
                            //==============================================
                            var SOH = context.TLCSV_StockOnHand.Find(Item.TLSOH_Pk);
                            if (SOH != null)
                                SOH.TLSOH_WareHousePickList = record.PickingListNo;
                            //=====================================================================

                            nr.Title = "Picking Slip";
                        }
                        else
                        {
                            nr.PickingSlipNumber = Item.TLSOH_WareHousePickList;
                            nr.Title = "Picking Slip Reprint";
                        }

                        var Merge = context.TLCSV_MergePODetail.Where(x => x.TLMerge_StockOnHand_Fk == Item.TLSOH_Pk && x.TLMerge_PoDetail_FK == Item.TLSOH_POOrderDetail_FK).FirstOrDefault();
                        if (Merge != null)
                        {
                            var PODetail = context.TLCSV_PuchaseOrderDetail.Find(Merge.TLMerge_PoDetail_FK);
                            if (PODetail != null)
                            {
                                var CustomerFile = _Customers.FirstOrDefault(s => s.Cust_Pk == _Svces.POCustomer_PK);
                                if (CustomerFile != null)
                                {
                                    nr.CustomerName = CustomerFile.Cust_Description;
                                    var POOrder = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK);
                                    if (POOrder != null)
                                    {
                                        nr.OrderNumber = POOrder.TLCSVPO_PurchaseOrder;
                                        if (CustomerFile.Cust_RePack)
                                        {
                                            var RePack = context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == POOrder.TLCSVPO_Pk && x.PORConfig_Size_FK == Item.TLSOH_Size_FK).FirstOrDefault();
                                            if (RePack != null)
                                            {
                                                //Item.TLSOH_RePackConfig_Fk = RePack.PORConfig_Pk;
                                                nr.Comments = context.TLCSV_RePackConfig.Find(RePack.PORConfig_Pk).PORConfig_BoxNumber;
                                            }
                                        }
                                    }
                                }

                                nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Item.TLSOH_Style_FK).Sty_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Item.TLSOH_Colour_FK).Col_Description;
                                if (!_Styles.FirstOrDefault(s => s.Sty_Id == Item.TLSOH_Style_FK).Sty_WorkWear)
                                {
                                    nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Item.TLSOH_Size_FK).SI_Description;
                                }
                                else
                                {
                                    nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Item.TLSOH_Size_FK).SI_ContiSize.ToString();
                                }
                                nr.BoxNumber = Item.TLSOH_BoxNumber;
                                nr.BoxQty = Item.TLSOH_BoxedQty;
                                nr.Weight = Item.TLSOH_Weight;
                                nr.PickSlipNumber = "PL" + _Pk.ToString().PadLeft(5, '0');
                                nr.Date = DateTime.Now;
                                nr.PastelNumber = Item.TLSOH_PastelNumber;
                            }
                        }
                        dataTable1.AddDataTable1Row(nr);
                    }

                    if (!_Svces.PLReprint)
                    {
                        foreach (var Whse in WhsePickingSlipNumbers)
                        {
                            if (Whse.Selected)
                            {
                                var WS = context.TLADM_WhseStore.Find(Whse.Whse_FK);
                                if (WS != null)
                                    WS.WhStore_PickingList += 1;
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                PickingList PickList = new PickingList();
                PickList.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PickList;

            }
            else if (_RepNo == 5)
            {
                DataSet ds = new DataSet();
                DataSet4.DataTable1DataTable dataTable1 = new DataSet4.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCSV_PuchaseOrderDetail.Where(x => !x.TLCUSTO_Picked).ToList();
                    foreach (var Row in Existing)
                    {
                        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        var PO = context.TLCSV_PurchaseOrder.Find(Row.TLCUSTO_PurchaseOrder_FK);
                        if (PO != null)
                        {
                            nr.Customer = context.TLADM_CustomerFile.Find(PO.TLCSVPO_Customer_FK).Cust_Description;
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.TLCUSTO_Style_FK).Sty_Description;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Row.TLCUSTO_Colour_FK).Col_Description;
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLCUSTO_Size_FK).SI_Description;
                            nr.OrderDate = PO.TLCSVPO_TransDate;
                            nr.PONumber = PO.TLCSVPO_PurchaseOrder;
                            nr.Qty = Row.TLCUSTO_Qty;
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                OutStandingOrders OutStand = new OutStandingOrders();
                OutStand.SetDataSource(ds);
                crystalReportViewer1.ReportSource = OutStand;
            }
            else if (_RepNo == 6)  //Stock on Hand ie Might have picked but not sold 
            {
                DataSet ds = new DataSet();
                IList<TLCSV_StockOnHand> soh = null;

                // Have Allowed for 9 Warehouses;
                //================================================================
                DataTable dt = new DataTable();
                dt.Columns.Add("Quality", typeof(int));   //0
                dt.Columns.Add("Colour", typeof(int));    //1
                dt.Columns.Add("Size", typeof(int));      //2
                dt.Columns.Add("Column1", typeof(int));   //3   1st WareHouse 
                dt.Columns.Add("Column2", typeof(int));   //4   2nd WareHouse
                dt.Columns.Add("Column3", typeof(int));   //5   3rd WareHouse 
                dt.Columns.Add("Column4", typeof(int));   //6   4th WareHouse
                dt.Columns.Add("Column5", typeof(int));   //7   5th WareHouse 
                dt.Columns.Add("Column6", typeof(int));   //8   6th WareHouse 
                dt.Columns.Add("Column7", typeof(int));   //9   7th WareHouse 
                dt.Columns.Add("Column8", typeof(int));   //10  8th WareHouse 
                dt.Columns.Add("Column9", typeof(int));   //11  9th WareHouse 
                dt.Columns.Add("Column10", typeof(int));  //12  Total for the Quality / Colour / Size
                Repository repo = new Repository();

                List<TLADM_WhseStore> AllWhses = new List<TLADM_WhseStore>();
                List<SOHDetails> sohStockDetails = new List<SOHDetails>();
                List<WhseDetail> whseDetails = new List<WhseDetail>();


                //--------------------------------------------------------------
                // Get a list of all current operational warehouses 
                //-----------------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    AllWhses = repo.SelWhse(_QueryParms).ToList();
                    int LoopNo = 0;

                    foreach (var Whse in AllWhses)
                    {
                        whseDetails.Add(new WhseDetail
                        {
                            WareHousePk = Whse.WhStore_Id,
                            WareHouseDescrip = Whse.WhStore_Description,
                            WareHouseColNo = ++LoopNo
                        });
                    }
                }

                //--------------------------------------------------
                // Get the data from the datbase based on the parameters entered by the user 
                //----------------------------------------------------------------------------------
                if (!_Svces.SplitBoxOnly)
                    soh = repo.SOHOnHand(_QueryParms).ToList();

                else
                    soh = repo.SOHBoxesSplit(_QueryParms).ToList();

                //------------------------------------------------------
                // Filter out any 'future' dates based on the date selected by the user 
                //---------------------------------------------------------------------------
                var Stocks = soh.Where(x => x.TLSOH_DateIntoStock <= _Svces.DateIntoStock).OrderBy(x => x.TLSOH_Style_FK).ThenBy(x => x.TLSOH_Colour_FK).ThenBy(x => x.TLSOH_Size_FK).ToList();

                //--------------------------------------------------------
                // if "Available" stock --- filter out records that have been picked
                //---------------------------------------------------------------------
                if (!_Svces.SOHClassification)
                {
                    Stocks = Stocks.Where(x => !x.TLSOH_Picked).ToList();
                }

                //-----------------------------------------------------
                // Sort out the grades issue
                //---------------------------------------------------
                if (_QueryParms.GradeA)
                {
                    Stocks = Stocks.Where(x => x.TLSOH_Grade.Contains("A")).ToList();
                }
                else
                {
                    if (!_QueryParms.Discontinued)
                    {
                        Stocks = Stocks.Where(x => !x.TLSOH_Grade.Contains("A")).ToList();
                    }
                }


                //--------------------------------------------------
                // Add the data to a list 
                //----------------------------------------------------------------------------------
                foreach (var Stock in Stocks)
                {
                    sohStockDetails.Add(new SOHDetails
                    {
                        Whse = Stock.TLSOH_WareHouse_FK,
                        BoxedQty = Stock.TLSOH_BoxedQty,
                        Colour = Stock.TLSOH_Colour_FK,
                        Size = Stock.TLSOH_Size_FK,
                        Style = Stock.TLSOH_Style_FK
                    });

                }
                //-------------------------------------------------------------
                //Now to deal will all active Warehouses 
                //----------------------------------------------
                foreach (var Whse in AllWhses)
                {
                    var Result = sohStockDetails.GroupBy(g => new { g.Style, g.Colour, g.Size });
                    foreach (var data in Result)
                    {
                        DataRow drow = dt.AsEnumerable().Where(p => p.Field<Int32>(0) == data.FirstOrDefault().Style
                                                               && p.Field<Int32>(1) == data.FirstOrDefault().Colour
                                                               && p.Field<Int32>(2) == data.FirstOrDefault().Size).FirstOrDefault();

                        if (drow == null)
                        {
                            drow = dt.NewRow();
                            drow[0] = data.FirstOrDefault().Style;
                            drow[1] = data.FirstOrDefault().Colour;
                            drow[2] = data.FirstOrDefault().Size;
                            drow[3] = 0;  // 1st WareHouse
                            drow[4] = 0;  // 2nd WareHouse
                            drow[5] = 0;  // 3rd WareHouse
                            drow[6] = 0;  // 4th WareHouse
                            drow[7] = 0;  // 5th WareHouse
                            drow[8] = 0;  // 6th WareHouse
                            drow[9] = 0;  // 7th WareHouse
                            drow[10] = 0;  // 8th WareHouse
                            drow[11] = 0;  // 9th WareHouse
                            drow[12] = 0;  // Total

                            var BoxedQty = data.Where(c => c.Whse == Whse.WhStore_Id).Sum(c => c.BoxedQty);

                            var record = whseDetails.Find(x => x.WareHousePk == Whse.WhStore_Id);
                            if (record != null)
                            {
                                var index = record.WareHouseColNo;
                                if (index == 10)
                                {
                                    continue;
                                }
                                drow[index + 2] = BoxedQty;
                                drow[12] = BoxedQty;
                            }

                            dt.Rows.Add(drow);
                        }
                        else
                        {
                            var BoxedQty = data.Where(c => c.Whse == Whse.WhStore_Id).Sum(c => c.BoxedQty);


                            var record = whseDetails.Find(x => x.WareHousePk == Whse.WhStore_Id);
                            if (record != null)
                            {
                                var index = record.WareHouseColNo;
                                if (index + 3 > 10)
                                    continue;

                                drow[index + 2] = Convert.ToInt32(drow[index + 3].ToString()) + BoxedQty;
                                drow[12] = Convert.ToInt32(drow[12].ToString()) + BoxedQty;
                            }
                        }

                    }
                }
                //------------------------------------------------------
                // Now to start with the report proper
                //----------------------------------------------------------
                DataSet5.DataTable1DataTable datatable1 = new DataSet5.DataTable1DataTable();
                DataSet5.DataTable2DataTable datatable2 = new DataSet5.DataTable2DataTable();

                DataSet5.DataTable2Row nrx = datatable2.NewDataTable2Row();
                if (_Svces.SOHClassification)
                    nrx.Classification = "Warehouse stock on hand(Boxed Qty) as at the ";
                else
                    nrx.Classification = "Warehouse available Stock (Boxed Qty) on hand as at the ";

                nrx.Date = _Svces.DateIntoStock;
                datatable2.AddDataTable2Row(nrx);

                using (var context = new TTI2Entities())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataSet5.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == (int)row[0]).Sty_Description;
                        
                        var Sty =  _Styles.FirstOrDefault(s => s.Sty_Id == (int)row[0]);
                        if (Sty != null && !Sty.Sty_WorkWear)
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == (int)row[2]).SI_Description;
                        }
                        else
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == (int)row[2]).SI_ContiSize.ToString();
                        }

                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == (int)row[1]).Col_Display;
                        // nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == (int)row[2]).SI_Description;
                        nr.Col1 = (int)row[3];
                        nr.Col2 = (int)row[4];
                        nr.Col3 = (int)row[5];
                        nr.Col4 = (int)row[6];
                        nr.Col5 = (int)row[7];
                        nr.Col6 = (int)row[8];
                        nr.Col7 = (int)row[9];
                        nr.Col8 = (int)row[10];
                        nr.Col9 = (int)row[11];
                        nr.Col10 = (int)row[12];

                        datatable1.AddDataTable1Row(nr);
                    }
                }

                if (datatable1.Count == 0)
                {
                    DataSet5.DataTable1Row nr = datatable1.NewDataTable1Row();
                    nr.ErrorLog = "No records found for selection made";
                    datatable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);

                StockQtysOH OnHand = new StockQtysOH();
                IEnumerator ie = OnHand.Section2.ReportObjects.GetEnumerator();

                foreach (var Whse in AllWhses)
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

                            to.Text = Whse.WhStore_Description;
                            break;


                        }
                    }
                }

                OnHand.SetDataSource(ds);
                crystalReportViewer1.ReportSource = OnHand;
            }
            else if (_RepNo == 7)
            {
                DataSet ds = new DataSet();
                Repository repo = new Repository();
                DataSet6.DataTable1DataTable dataTable1 = new DataSet6.DataTable1DataTable();
                DataSet6.DataTable2DataTable dataTable2 = new DataSet6.DataTable2DataTable();
                IQueryable<TLCSV_StockOnHand> soh;

                //--------------------------------------------------
                // Get the data from the datbase based on the parameters entered by the user 
                //----------------------------------------------------------------------------------
                if (!_Svces.SOHBoxReturned && !_Svces.SplitBoxOnly)
                    soh = repo.SOHOnHand(_QueryParms);
                else if (_Svces.SOHBoxReturned)
                    soh = repo.SOHBoxesReturned(_QueryParms);
                else
                    soh = repo.SOHBoxesSplit(_QueryParms);


                //------------------------------------------------------
                // Filter out any 'future' dates based on the date selected by the user 
                //---------------------------------------------------------------------------
                var Stocks = soh.Where(x => x.TLSOH_DateIntoStock <= _Svces.DateIntoStock).ToList();

                if (!_Svces.SOHBoxReturned)
                {
                    //--------------------------------------------------------
                    // if "Available" stock --- filter out records that have been picked
                    //---------------------------------------------------------------------
                    if (!_Svces.SOHClassification && !_Svces.SplitBoxOnly)
                    {
                        Stocks = Stocks.Where(x => !x.TLSOH_Picked).ToList();
                    }

                    //-----------------------------------------------------
                    // Sort out the grades issue
                    //---------------------------------------------------
                    if (_QueryParms.GradeA)
                    {
                        Stocks = Stocks.Where(x => x.TLSOH_Grade.Contains("A")).ToList();
                    }
                    else
                    {
                        if (!_QueryParms.Discontinued)
                            Stocks = Stocks.Where(x => !x.TLSOH_Grade.Contains("A")).ToList();
                    }
                }


                DataSet6.DataTable1Row nr = dataTable1.NewDataTable1Row();
                nr.Pk = 1;
                if (!_Svces.SOHBoxReturned)
                {
                    if (_Svces.SOHClassification)
                        nr.Title = "Warehouse stock on hand(Boxed Qty) as at the ";
                    else if (_Svces.SOHBoxReturned)
                        nr.Title = "Warehouse available Stock (Boxed Qty) on hand as at the ";
                    else
                        nr.Title = "Warehouse stock on hand (Split Boxes)  as at the ";
                }
                else
                    nr.Title = "Warehouse Boxes returned as at the ";

                nr.Date = _Svces.DateIntoStock;
                dataTable1.AddDataTable1Row(nr);

                using (var context = new TTI2Entities())
                {
                    foreach (var SItem in Stocks)
                    {

                        DataSet6.DataTable2Row n2r = dataTable2.NewDataTable2Row();
                        n2r.Pk = 1;
                        n2r.WareHouse = context.TLADM_WhseStore.Find(SItem.TLSOH_WareHouse_FK).WhStore_Description;
                        n2r.Product = _Styles.FirstOrDefault(s => s.Sty_Id == SItem.TLSOH_Style_FK).Sty_Description;
                        n2r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == SItem.TLSOH_Colour_FK).Col_Display;

                        var Prd = _Styles.FirstOrDefault(s => s.Sty_Id == SItem.TLSOH_Style_FK);
                        if (Prd != null && !Prd.Sty_WorkWear)
                        {
                            n2r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SItem.TLSOH_Size_FK).SI_Description;
                        }
                        else
                        {
                            n2r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SItem.TLSOH_Size_FK).SI_ContiSize.ToString();
                        }
                      
                        n2r.BoxNumber = SItem.TLSOH_BoxNumber;
                        n2r.SizeDisplayOrder = _Sizes.FirstOrDefault(s => s.SI_id == SItem.TLSOH_Size_FK).SI_DisplayOrder;
                        n2r.Status = string.Empty;

                        if (!_Svces.SOHBoxReturned)
                        {
                            n2r.BoxQty = SItem.TLSOH_BoxedQty;
                            if (SItem.TLSOH_Picked)
                                n2r.Status = "Picked";
                            else
                                n2r.Status = "Available";
                        }
                        else
                            n2r.BoxQty = SItem.TLSOH_ReturnedBoxQty;

                        n2r.Grade = SItem.TLSOH_Grade;

                        dataTable2.AddDataTable2Row(n2r);
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Count == 0)
                {
                    DataSet6.DataTable2Row n2r = dataTable2.NewDataTable2Row();
                    n2r.Pk = 1;
                    n2r.ErrorLog = "No records found pertaining to selection";
                    dataTable2.AddDataTable2Row(n2r);
                }
                ds.Tables.Add(dataTable2);

                BoxesInStock BoxInSt = new BoxesInStock();
                BoxInSt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BoxInSt;

            }
            else if (_RepNo == 8)  //Outstanding Orders  
            {
                DataSet ds = new DataSet();
                Repository repo = new Repository();
                DataSet8.DataTable1DataTable datatable1 = new DataSet8.DataTable1DataTable();
                IList<TLCSV_PuchaseOrderDetail> PODetails = null;
                Util core = new Util();

                IList<TLADM_Styles> _Styles = null;
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_Sizes> _Sizes = null;

                //--------------------------------------------------
                // Get the data from the database based on the parameters entered by the user 
                //----------------------------------------------------------------------------------
                var POOrders = repo.PurchaseOrder(_QueryParms).OrderBy(x => x.TLCSVPO_PurchaseOrder);
                using (var context = new TTI2Entities())
                {
                    _Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                    _Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                    _Sizes = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                    _Qualities = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                    foreach (var POrder in POOrders)
                    {
                        PODetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == POrder.TLCSVPO_Pk && !x.TLCUSTO_Closed).ToList();


                        PODetails = (from PODet in PODetails
                                     join Sze in _Sizes on PODet.TLCUSTO_Size_FK equals Sze.SI_id
                                     orderby Sze.SI_DisplayOrder
                                     select PODet).ToList();

                        foreach (var PODetail in PODetails)
                        {
                            if (_QueryParms.Styles.Count() > 0)
                            {
                                var Find = _QueryParms.Styles.Where(x => x.Sty_Id == PODetail.TLCUSTO_Style_FK).FirstOrDefault();
                                if (Find == null)
                                    continue;
                            }

                            if (_QueryParms.Colours.Count() > 0)
                            {
                                var Find = _QueryParms.Colours.Where(x => x.Col_Id == PODetail.TLCUSTO_Colour_FK).FirstOrDefault();
                                if (Find == null)
                                    continue;
                            }


                            if (_QueryParms.Sizes.Count() > 0)
                            {
                                var Find = _QueryParms.Sizes.Where(x => x.SI_id == PODetail.TLCUSTO_Size_FK).FirstOrDefault();
                                if (Find == null)
                                    continue;
                            }
                            var Cust = _Customers.FirstOrDefault(s => s.Cust_Pk == POrder.TLCSVPO_Customer_FK);
                             
                            DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();
                            nr.OrderNo = POrder.TLCSVPO_PurchaseOrder;
                            nr.Customer = Cust.Cust_Description;
                            nr.OrderDate = (DateTime)POrder.TLCSVPO_TransDate;

                            if (PODetail.TLCUSTO_DateRequired != null)
                            {
                                nr.DueDate = (DateTime)PODetail.TLCUSTO_DateRequired;
                                nr.CenturyDayNo = core.CenturyDayNumber(nr.OrderDate);
                                nr.WeekNo = core.GetWeekNumber(nr.OrderDate);
                                nr.Year = nr.OrderDate.Year;
                            }
                            else
                            {
                                nr.DueDate = (DateTime)POrder.TLCSVPO_RequiredDate;
                                nr.CenturyDayNo = core.CenturyDayNumber(nr.OrderDate);
                                nr.WeekNo = core.GetWeekNumber(nr.OrderDate);
                                nr.Year = nr.OrderDate.Year;
                            }

                            StringBuilder sb = new StringBuilder();
                            if (!Cust.Cust_FabricCustomer)
                            {
                                sb.Append(_Styles.FirstOrDefault(s => s.Sty_Id == PODetail.TLCUSTO_Style_FK).Sty_Description + ' ');
                                sb.Append(_Colours.FirstOrDefault(s => s.Col_Id == PODetail.TLCUSTO_Colour_FK).Col_Display + ' ');

                                var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == PODetail.TLCUSTO_Style_FK);
                                if (Sty != null && !Sty.Sty_WorkWear)
                                {
                                    sb.Append(_Sizes.FirstOrDefault(s => s.SI_id == PODetail.TLCUSTO_Size_FK).SI_Description);
                                }
                                else
                                {
                                    sb.Append(_Sizes.FirstOrDefault(s => s.SI_id == PODetail.TLCUSTO_Size_FK).SI_ContiSize.ToString());
                                }

                                nr.DisplayOrder = _Sizes.FirstOrDefault(s => s.SI_id == PODetail.TLCUSTO_Size_FK).SI_DisplayOrder;
                                nr.StyleDescription = sb.ToString();
                                nr.OrderQty = PODetail.TLCUSTO_Qty;

                                //--------------------------------------------
                                // Delivered to date 
                                //--------------------------------
                                nr.LineNo = PODetail.TLCUSTO_LineNumber;
                                nr.DeliveredToDate = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == PODetail.TLCUSTO_Pk && x.TLSOH_Sold).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                nr.PickingLists = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == PODetail.TLCUSTO_Pk && x.TLSOH_Picked && !x.TLSOH_Sold).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                nr.Nett = nr.OrderQty - (nr.PickingLists + nr.DeliveredToDate);

                                if (_QueryParms.SummarisedPurchaseOrders)
                                {
                                    if (nr.Nett < 0)
                                        continue;
                                }

                                if (POrder.TLCSVPO_Closeed)
                                    nr.Status = "Closed";
                                else
                                    nr.Status = "Active";
                            }
                            else
                            {
                                sb.Append(_Qualities.FirstOrDefault(s => s.TLGreige_Id == PODetail.TLCUSTO_Quality_FK).TLGreige_Description + ' ');
                                sb.Append(_Colours.FirstOrDefault(s => s.Col_Id == PODetail.TLCUSTO_Colour_FK).Col_Display + ' ');
                                                                
                                nr.StyleDescription = sb.ToString();
                                nr.OrderQty = (int)PODetail.TLCUSTO_QtyMeters;

                                //--------------------------------------------
                                // Delivered to date 
                                //--------------------------------
                                nr.LineNo = PODetail.TLCUSTO_LineNumber;
                                nr.DeliveredToDate = (int)PODetail.TLCUSTO_QtyMeters_Delivered;
                                nr.PickingLists = 0;
                                nr.Nett = nr.OrderQty - nr.DeliveredToDate;

                                if (_QueryParms.SummarisedPurchaseOrders)
                                {
                                    if (nr.Nett < 0)
                                        continue;
                                }

                                if (POrder.TLCSVPO_Closeed)
                                    nr.Status = "Closed";
                                else
                                    nr.Status = "Active";

                            }
                            datatable1.AddDataTable1Row(nr);
                        }
                    }
                }

                if (datatable1.Rows.Count == 0)
                {
                    DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();
                    nr.ErrorLog = "No records pertaining to selection made";
                    datatable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(datatable1);
                if (!_QueryParms.GroupByWeek)
                {
                    if (!_QueryParms.SummarisedPurchaseOrders)
                    {
                        OutStandingOrders outOrders = new OutStandingOrders();
                        outOrders.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = outOrders;
                    }
                    else
                    {
                        OutStandingOrdersSummarised outOrders = new OutStandingOrdersSummarised();
                        outOrders.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = outOrders;
                    }
                }
                else
                {
                    OutStandingOrdersByWeek outOrders = new OutStandingOrdersByWeek();
                    outOrders.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = outOrders;
                }

            }
            else if (_RepNo == 9)   // Orders Picked Awaiting Delivery
            {
                DataSet ds = new DataSet();
                DataSet9.DataTable1DataTable datatable1 = new DataSet9.DataTable1DataTable();
                Repository repo = new Repository();

                var SOHDetail = repo.PendingPS(_QueryParms);

                using (var context = new TTI2Entities())
                {
                    foreach (var Item in SOHDetail)
                    {
                        DataSet9.DataTable1Row nr = datatable1.NewDataTable1Row();

                        nr.PickSlip = "PL" + Item.TLSOH_PickListNo.ToString().PadLeft(5, '0');
                        var OrderAllocated = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_TransNumber == Item.TLSOH_PickListNo).FirstOrDefault();
                        if (OrderAllocated != null && OrderAllocated.TLORDA_PLStockOrder)
                        {
                            if (_Svces.PLStockOrder && OrderAllocated.TLORDA_PLStockOrder)
                                continue;

                            nr.PickSlip += "****";
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append(_Styles.FirstOrDefault(s => s.Sty_Id == Item.TLSOH_Style_FK).Sty_Description + " ");
                        sb.Append(_Colours.FirstOrDefault(s => s.Col_Id == Item.TLSOH_Colour_FK).Col_Display + " ");
                        sb.Append(_Sizes.FirstOrDefault(s => s.SI_id == Item.TLSOH_Size_FK).SI_Description);

                        nr.Style = sb.ToString();
                        nr.BoxedValue = Item.TLSOH_BoxedQty;
                        nr.DueDate = (DateTime)Item.TLSOH_PickListDate;

                        var POOrder = context.TLCSV_PurchaseOrder.Find(Item.TLSOH_POOrder_FK);
                        if (POOrder != null)
                        {
                            nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == POOrder.TLCSVPO_Customer_FK).Cust_Description;
                        }

                        nr.BoxNumber = Item.TLSOH_BoxNumber;
                        nr.TransNumber = Item.TLSOH_WareHousePickList;

                        datatable1.AddDataTable1Row(nr);
                    }
                }

                if (datatable1.Count == 0)
                {
                    DataSet9.DataTable1Row nr = datatable1.NewDataTable1Row();
                    nr.ErrorLog = "No records found for selection made";
                    datatable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(datatable1);
                if (!_Svces.PLSummarised)
                {
                    PLWaiting PLWait = new PLWaiting();
                    PLWait.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = PLWait;
                }
                else
                {
                    PLWaitingSummarised PLWait = new PLWaitingSummarised();
                    PLWait.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = PLWait;
                }

            }
            else if (_RepNo == 10)   // Orders Picked --- Delivery Note  
            {
                DataSet ds = new DataSet();
                Repository repo = new Repository();
                DataSet10.DataTable1DataTable dataTable1 = new DataSet10.DataTable1DataTable();
                DataSet10.DataTable2DataTable dataTable2 = new DataSet10.DataTable2DataTable();
                List<WhseDATA> WhseDeliveryNote = new List<WhseDATA>();
                bool RePackCustomer = false;

                TLCSV_RePackTransactions Transaction = null;
                TLCSV_RePackConfig MasterGroup = null; ;

                using (var context = new TTI2Entities())
                {
                    if (!_Svces.DNReprint)
                    {
                        var Index = _QueryParms.Customers.FirstOrDefault().Cust_Pk;
                        var Customer = context.TLADM_CustomerFile.Find(Index);

                        if (Customer != null)
                        {
                            RePackCustomer = Customer.Cust_RePack;

                            DataSet10.DataTable2Row dtTable2 = dataTable2.NewDataTable2Row();
                            dtTable2.Pk = 1;
                            dtTable2.Date = _Svces.DateIntoStock;
                            dtTable2.DeliveryNote = "F" + _Svces.TransNumber.ToString().PadLeft(6, '0');

                            dtTable2.Customer = Customer.Cust_Description;
                            dtTable2.Attention = Customer.Cust_ContactPerson;
                            dtTable2.PhoneNumber = Customer.Cust_Telephone;
                            dtTable2.Date = _Svces.DateIntoStock;

                            dataTable2.AddDataTable2Row(dtTable2);
                        }

                        var PLNumbers = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                        foreach (var PLNumber in PLNumbers)
                        {
                            var PLDetail = new WhseDATA();
                            PLDetail.Whse_FK = PLNumber.WhStore_Id;
                            PLDetail.PickingListNo = PLNumber.WhStore_DeliveryNote;
                            PLDetail.Selected = false;
                            WhseDeliveryNote.Add(PLDetail);
                        }

                        foreach (var ShirtParm in _QueryParms.OrdersAllocated)
                        {
                            var OrderAlloc = context.TLCSV_OrderAllocated.Find(ShirtParm.TLORDA_Pk);
                            if (OrderAlloc != null)
                            {
                                var POrder = context.TLCSV_PurchaseOrder.Find(OrderAlloc.TLORDA_POOrder_FK);
                                var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == OrderAlloc.TLORDA_TransNumber).ToList();

                                Existing = (from Exist in Existing
                                            join Style in context.TLADM_Styles on Exist.TLSOH_Style_FK equals Style.Sty_Id
                                            orderby Style.Sty_DisplayOrder
                                            select Exist).ToList();
                                if (!POrder.TLCSVPO_RepackTransaction)
                                {
                                    foreach (var Row in Existing)
                                    {
                                        DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                        nr.WareHouse = context.TLADM_WhseStore.Find(Row.TLSOH_WareHouse_FK).WhStore_Description;
                                        //============================================================
                                        nr.Pk = 1;
                                        nr.ProductCode = Row.TLSOH_PastelNumber;
                                        var PODetail = context.TLCSV_PuchaseOrderDetail.Find(Row.TLSOH_POOrderDetail_FK);
                                        if (PODetail != null)
                                            nr.OrderNumber = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK).TLCSVPO_PurchaseOrder;

                                        nr.BoxNumber = Row.TLSOH_BoxNumber;

                                        //-------------------------------------------------------------
                                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.TLSOH_Style_FK).Sty_Description;
                                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Row.TLSOH_Colour_FK).Col_Display;
                                        if (!_Styles.FirstOrDefault(s => s.Sty_Id == Row.TLSOH_Style_FK).Sty_WorkWear)
                                        {
                                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLSOH_Size_FK).SI_Description;
                                        }
                                        else
                                        {
                                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLSOH_Size_FK).SI_ContiSize.ToString();
                                        }
                                        nr.BoxedQty = Row.TLSOH_BoxedQty;
                                        nr.Weight = Row.TLSOH_Weight;

                                        //=============================================================
                                        // this is were we find the next delivery Note Number
                                        //========================================================
                                        var record = WhseDeliveryNote.Find(x => x.Whse_FK == Row.TLSOH_WareHouse_FK);
                                        var index = WhseDeliveryNote.IndexOf(record);
                                        record.Selected = true;
                                        WhseDeliveryNote[index] = record;

                                        nr.WareHouseDeliveryNo = record.PickingListNo;

                                        var SOH = context.TLCSV_StockOnHand.Find(Row.TLSOH_Pk);
                                        if (SOH != null)
                                            SOH.TLSOH_WareHouseDeliveryNo = record.PickingListNo;
                                        //====================================================================

                                        dataTable1.AddDataTable1Row(nr);
                                    }
                                }
                                else
                                {
                                    var TGroupBoxes = context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == OrderAlloc.TLORDA_POOrder_FK).GroupBy(x => x.PORConfig_BoxNumber);
                                    foreach (var BoxGroup in TGroupBoxes)
                                    {
                                        foreach (var Box in BoxGroup)
                                        {
                                            MasterGroup = Box;
                                            Transaction = context.TLCSV_RePackTransactions.Where(x => x.REPACT_RePackConfig_FK == MasterGroup.PORConfig_Pk).FirstOrDefault();
                                            if (Transaction == null)
                                                continue;
                                            break;
                                        }
                                        var SOH = context.TLCSV_StockOnHand.Find(Transaction.REPACT_StockOnHand_FK);
                                        if (SOH != null)
                                        {
                                            String PastelNumber = String.Empty; // SOH.TLSOH_PastelNumber;
                                            String DeliverNumber = SOH.TLSOH_DNListNo.ToString();
                                            string PODetail = context.TLCSV_PurchaseOrder.Find(OrderAlloc.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                                            String Style = _Styles.FirstOrDefault(s => s.Sty_Id == MasterGroup.PORConfig_Style_FK).Sty_Description;
                                            String Colour = _Colours.FirstOrDefault(s => s.Col_Id == MasterGroup.PORConfig_Colour_FK).Col_Display;
                                            string Size = "ALL";

                                            var TotalBoxed = BoxGroup.Sum(x => (int?)x.PORConfig_SizeBoxQty) ?? 0;

                                            for (var Cnt = 0; Cnt < MasterGroup.PORConfig_TotalBoxes; Cnt += 1)
                                            {
                                                DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                                nr.Pk = 1;
                                                nr.WareHouse = context.TLADM_WhseStore.Find(SOH.TLSOH_WareHouse_FK).WhStore_Description;
                                                nr.Style = Style;
                                                nr.Colour = Colour;
                                                nr.Size = Size;
                                                nr.BoxedQty = TotalBoxed;
                                                nr.Weight = 0.0M;
                                                nr.BoxNumber = MasterGroup.PORConfig_BoxNumber + " - " + (Cnt + 1).ToString().PadLeft(3, '0');

                                                dataTable1.AddDataTable1Row(nr);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else     //Reprint Option 
                    {
                        var OrderAlloc = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_DelTransNumber == _Pk).FirstOrDefault();
                        if (OrderAlloc != null)
                        {
                            var POrder = context.TLCSV_PurchaseOrder.Find(OrderAlloc.TLORDA_POOrder_FK);

                            TLADM_CustomerFile CustDetail = null;
                            CustDetail = _Customers.FirstOrDefault(s => s.Cust_Pk == OrderAlloc.TLORDA_Customer_FK);
                            if (CustDetail != null)
                            {
                                DataSet10.DataTable2Row dtTable2 = dataTable2.NewDataTable2Row();
                                dtTable2.Pk = 1;
                                dtTable2.DeliveryNote = "F" + _Pk.ToString().PadLeft(6, '0');
                                dtTable2.Customer = CustDetail.Cust_Description;
                                dtTable2.Attention = CustDetail.Cust_ContactPerson;
                                dtTable2.PhoneNumber = CustDetail.Cust_Telephone;
                                dtTable2.Date = (DateTime)OrderAlloc.TLORDA_DeliveredDate;

                                dataTable2.AddDataTable2Row(dtTable2);
                                RePackCustomer = CustDetail.Cust_RePack;
                                if (!POrder.TLCSVPO_RepackTransaction)
                                {
                                    // Non Repack Center 
                                    //=============================
                                    var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_DNListNo == _Pk).ToList();
                                    foreach (var Row in Existing)
                                    {
                                        DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                        nr.WareHouse = context.TLADM_WhseStore.Find(Row.TLSOH_WareHouse_FK).WhStore_Description;
                                        nr.Pk = 1;
                                        nr.ProductCode = Row.TLSOH_PastelNumber;
                                        var PODetail = context.TLCSV_PuchaseOrderDetail.Find(Row.TLSOH_POOrderDetail_FK);
                                        if (PODetail != null)
                                            nr.OrderNumber = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK).TLCSVPO_PurchaseOrder;
                                        nr.BoxNumber = Row.TLSOH_BoxNumber;
                                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.TLSOH_Style_FK).Sty_Description;
                                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Row.TLSOH_Colour_FK).Col_Display;

                                        var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == Row.TLSOH_Style_FK);
                                        if (Sty != null && !Sty.Sty_WorkWear)
                                        {
                                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLSOH_Size_FK).SI_Description;
                                        }
                                        else
                                        {
                                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLSOH_Size_FK).SI_ContiSize.ToString();
                                        }
                                        nr.BoxedQty = Row.TLSOH_BoxedQty;
                                        nr.Weight = Row.TLSOH_Weight;
                                        nr.WareHouseDeliveryNo = Row.TLSOH_WareHouseDeliveryNo;

                                        dataTable1.AddDataTable1Row(nr);
                                    }
                                }
                                else
                                {
                                    // Repack Center  
                                    //=============================
                                    var TGroupBoxes = context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == OrderAlloc.TLORDA_POOrder_FK).GroupBy(x => x.PORConfig_BoxNumber);
                                    foreach (var BoxGroup in TGroupBoxes)
                                    {
                                        foreach (var Box in BoxGroup)
                                        {
                                            MasterGroup = Box;
                                            Transaction = context.TLCSV_RePackTransactions.Where(x => x.REPACT_RePackConfig_FK == MasterGroup.PORConfig_Pk).FirstOrDefault();
                                            if (Transaction == null)
                                                continue;
                                            break;
                                        }
                                        var SOH = context.TLCSV_StockOnHand.Find(Transaction.REPACT_StockOnHand_FK);
                                        if (SOH != null)
                                        {
                                            String PastelNumber = String.Empty; // SOH.TLSOH_PastelNumber;
                                            String DeliverNumber = SOH.TLSOH_DNListNo.ToString();
                                            string PODetail = context.TLCSV_PurchaseOrder.Find(OrderAlloc.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                                            String Style = _Styles.FirstOrDefault(s => s.Sty_Id == MasterGroup.PORConfig_Style_FK).Sty_Description;
                                            String Colour = _Colours.FirstOrDefault(s => s.Col_Id == MasterGroup.PORConfig_Colour_FK).Col_Display;
                                            string Size = "ALL";

                                            var TotalBoxed = BoxGroup.Sum(x => (int?)x.PORConfig_SizeBoxQty) ?? 0;

                                            for (var Cnt = 0; Cnt < MasterGroup.PORConfig_TotalBoxes; Cnt += 1)
                                            {
                                                DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                                nr.Pk = 1;
                                                nr.WareHouse = context.TLADM_WhseStore.Find(SOH.TLSOH_WareHouse_FK).WhStore_Description;
                                                nr.Style = Style;
                                                nr.Colour = Colour;
                                                nr.Size = Size;
                                                nr.BoxedQty = TotalBoxed;
                                                nr.Weight = 0.0M;
                                                nr.BoxNumber = MasterGroup.PORConfig_BoxNumber + " - " + (Cnt + 1).ToString().PadLeft(3, '0');

                                                dataTable1.AddDataTable1Row(nr);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!_Svces.DNReprint)
                    {
                        foreach (var Whse in WhseDeliveryNote)
                        {
                            if (Whse.Selected)
                            {
                                var WS = context.TLADM_WhseStore.Find(Whse.Whse_FK);
                                if (WS != null)
                                    WS.WhStore_DeliveryNote += 1;
                            }
                        }


                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                if (!RePackCustomer)
                {
                    DeliveryNote DNote = new DeliveryNote();
                    DNote.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = DNote;
                }
                else
                {
                    RePackDeliveryNote DNote = new RePackDeliveryNote();
                    DNote.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = DNote;

                }

            }
            else if (_RepNo == 11)   // Customer Returns 
            {
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable dataTable1 = new DataSet11.DataTable1DataTable();
                DataSet11.DataTable2DataTable dataTable2 = new DataSet11.DataTable2DataTable();
                Repository repo = new Repository();
                using (var context = new TTI2Entities())
                {
                    foreach (var ShirtParm in _QueryParms.OrdersAllocated)
                    {
                        // there should only be one allocated record for this transaction
                        //---------------------------------------------------------------------
                        var OrderAlloc = context.TLCSV_OrderAllocated.Find(ShirtParm.TLORDA_Pk);
                        if (OrderAlloc != null)
                        {
                            DataSet11.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = 1;
                            nr.Customer = context.TLADM_CustomerFile.Find(OrderAlloc.TLORDA_Customer_FK).Cust_Description;
                            nr.Date = _Svces.ReturnDate;
                            nr.ReturnNumber = "GA" + OrderAlloc.TLORDA_ReturnNumber.ToString().PadLeft(5, '0');
                            nr.ApprovedBy = _Svces.ApprovedBy;
                            nr.CustomerRef = _Svces.CustomerRef;
                            nr.Reasons = _Svces.Reasons;
                            nr.WareHouse = context.TLADM_WhseStore.Find(_Svces.WareHouse).WhStore_Description;

                            dataTable1.AddDataTable1Row(nr);

                            var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_ReturnNumber == OrderAlloc.TLORDA_ReturnNumber).ToList();
                            foreach (var Exist in Existing)
                            {
                                DataSet11.DataTable2Row t2Rw = dataTable2.NewDataTable2Row();

                                t2Rw.Pk = 1;
                                t2Rw.BoxNumber = Exist.TLSOH_BoxNumber;
                                StringBuilder sb = new StringBuilder();
                                sb.Append(_Styles.FirstOrDefault(s => s.Sty_Id == Exist.TLSOH_Style_FK).Sty_Description + " ");
                                sb.Append(_Colours.FirstOrDefault(s => s.Col_Id == Exist.TLSOH_Colour_FK).Col_Description + " ");
                                sb.Append(_Sizes.FirstOrDefault(s => s.SI_id == Exist.TLSOH_Size_FK).SI_Description);
                                t2Rw.Style = sb.ToString();
                                t2Rw.PurchaseOrder = context.TLCSV_PurchaseOrder.Find(Exist.TLSOH_POOrder_FK).TLCSVPO_PurchaseOrder;
                                t2Rw.BoxedQty = Exist.TLSOH_ReturnedBoxQty;

                                dataTable2.AddDataTable2Row(t2Rw);
                            }
                        }
                    }
                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);

                    CustReturns CustRets = new CustReturns();
                    CustRets.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CustRets;
                }
            }
            else if (_RepNo == 12)   // SOH Qty Adjustments  
            {
                DataSet ds = new DataSet();
                DataSet12.DataTable1DataTable dataTable1 = new DataSet12.DataTable1DataTable();
                DataSet12.DataTable2DataTable dataTable2 = new DataSet12.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    var Move = context.TLCSV_Movement.Find(_Pk);
                    if (Move != null)
                    {
                        DataSet12.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.Reasons = Move.TLMV_Reasons;
                        nr.AuthorisedBy = Move.TLMV_AuthorisedBy;
                        nr.Date = Move.TLMV_TransDate;
                        nr.OriginalNumber = Move.TLMV_OriginalNumber;
                        nr.Warehouse = context.TLADM_WhseStore.Find(Move.TLMV_ToWhse_FK).WhStore_Description;
                        dataTable1.AddDataTable1Row(nr);


                        var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_MoveAdjust_FK == Move.TLMV_Pk).ToList();
                        foreach (var Record in Existing)
                        {
                            DataSet12.DataTable2Row tr = dataTable2.NewDataTable2Row();
                            tr.Pk = 1;
                            tr.BoxNumber = Record.TLSOH_BoxNumber;
                            tr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLSOH_Style_FK).Sty_Description;
                            tr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.TLSOH_Colour_FK).Col_Description;
                            tr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLSOH_Size_FK).SI_Description;
                            if (Record.TLSOH_QtyAdjusted < 0)
                            {
                                tr.OrigBoxQty = Record.TLSOH_BoxedQty + Record.TLSOH_QtyAdjusted;
                            }
                            else
                            {
                                tr.OrigBoxQty = Record.TLSOH_BoxedQty - Record.TLSOH_QtyAdjusted;
                            }
                            tr.AdjustQty = Record.TLSOH_QtyAdjusted;

                            tr.Total = tr.OrigBoxQty + tr.AdjustQty;

                            dataTable2.AddDataTable2Row(tr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                StockAdjusted stkAdj = new StockAdjusted();
                stkAdj.SetDataSource(ds);
                crystalReportViewer1.ReportSource = stkAdj;


            }
            else if (_RepNo == 13)   // Customer Sales / Returns s / Ranking by Style Colour
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable dataTable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable dataTable2 = new DataSet13.DataTable2DataTable();
                DataSet13.DataTable3DataTable dataTable3 = new DataSet13.DataTable3DataTable();

                Repository repo = new Repository();

                //--------------------------------------------
                // Standard Parameters (If applicable)
                //--------------------------------------------
                var SOH = repo.SoldQuery(_QueryParms);

                //-----------------------------
                // Filter out dates 
                //-------------------------------------------------
                SOH = SOH.Where(x => x.TLSOH_SoldDate >= _Svces.fromDate && x.TLSOH_SoldDate <= _Svces.toDate);

                using (var context = new TTI2Entities())
                {
                    if (!_QueryParms.RankedByStyleColour &&
                        !_QueryParms.RankedByStyleColourSize &&
                        !_QueryParms.RankedByStyleSize)
                    {
                        foreach (var Item in SOH)
                        {
                            DataSet13.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                            t2r.Pk = 1;

                            t2r.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Item.TLSOH_Style_FK).Sty_Description;
                            t2r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Item.TLSOH_Colour_FK).Col_Display;
                            t2r.Size = _Sizes.FirstOrDefault(s => s.SI_id == Item.TLSOH_Size_FK).SI_Description;
                            t2r.SizeDisplayOrder = _Sizes.FirstOrDefault(s => s.SI_id == Item.TLSOH_Size_FK).SI_DisplayOrder;
                            t2r.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == Item.TLSOH_Customer_Fk).Cust_Description;
                            t2r.BoxedQty = Item.TLSOH_BoxedQty;
                            t2r.BoxNumber = Item.TLSOH_BoxNumber;
                            var POD = context.TLCSV_PuchaseOrderDetail.Find(Item.TLSOH_POOrderDetail_FK);
                            if (POD != null)
                            {
                                var PO = context.TLCSV_PurchaseOrder.Find(POD.TLCUSTO_PurchaseOrder_FK);
                                if (PO != null)
                                {
                                    t2r.PONumber = PO.TLCSVPO_PurchaseOrder;
                                }
                            }
                            t2r.TransDate = (DateTime)Item.TLSOH_SoldDate;
                            t2r.PickList = (int)Item.TLSOH_PickListNo;
                            t2r.DeliveryNote = (int)Item.TLSOH_DNListNo;

                            dataTable2.AddDataTable2Row(t2r);

                        }
                    }
                    else
                    {
                        if (_QueryParms.RankedByStyleColour)
                        {
                            var SOHGrouped = SOH.GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK });
                            foreach (var Item in SOHGrouped)
                            {
                                DataSet13.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                                t2r.Pk = 1;
                                var StylePk = Item.FirstOrDefault().TLSOH_Style_FK;
                                t2r.Style = context.TLADM_Styles.Find(StylePk).Sty_Description;
                                var ColourPk = Item.FirstOrDefault().TLSOH_Colour_FK;
                                t2r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == ColourPk).Col_Display;
                                //t2r.Size = " "; // context.TLADM_Sizes.Find(Item.TLSOH_Size_FK).SI_Description;
                                //t2r.Customer = " "; // context.TLADM_CustomerFile.Find(Item.TLSOH_Customer_Fk).Cust_Description;
                                t2r.BoxedQty = Item.Sum(x => x.TLSOH_BoxedQty);
                                /*t2r.BoxNumber = " "; // Item.TLSOH_BoxNumber;
                                var POD = context.TLCSV_PuchaseOrderDetail.Find(Item.TLSOH_POOrderDetail_FK);
                                if (POD != null)
                                    t2r.PONumber = context.TLCSV_PurchaseOrder.Find(POD.TLCUSTO_PurchaseOrder_FK).TLCSVPO_PurchaseOrder;

                                t2r.TransDate = (DateTime)Item.TLSOH_SoldDate;
                                t2r.PickList = (int)Item.TLSOH_PickListNo;
                                t2r.DeliveryNote = (int)Item.TLSOH_DNListNo; */


                                dataTable2.AddDataTable2Row(t2r);
                            }
                        }
                        else if (_QueryParms.RankedByStyleColourSize)
                        {
                            var SOHGrouped = SOH.GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK });
                            foreach (var Item in SOHGrouped)
                            {
                                var SizeGrouped = Item.GroupBy(x => x.TLSOH_Size_FK);
                                foreach (var Size in SizeGrouped)
                                {
                                    var StylePk = Item.FirstOrDefault().TLSOH_Style_FK;
                                    var ColourPk = Item.FirstOrDefault().TLSOH_Colour_FK;
                                    var SizePk = Size.FirstOrDefault().TLSOH_Size_FK;

                                    DataSet13.DataTable3Row t3r = dataTable3.NewDataTable3Row();
                                    t3r.Pk = 1;

                                    t3r.Style = context.TLADM_Styles.Find(StylePk).Sty_Description;
                                    t3r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == ColourPk).Col_Display;
                                    var Sty = context.TLADM_Styles.Find(StylePk);
                                    if (Sty != null && !Sty.Sty_WorkWear)
                                    {
                                        t3r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_Description;
                                    }
                                    else
                                    {
                                        t3r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_ContiSize.ToString();
                                    }
                                    t3r.BoxedQty = Size.Sum(x => x.TLSOH_BoxedQty);
                                    t3r.Total_BoxedQty = Item.Sum(x => x.TLSOH_BoxedQty);

                                    dataTable3.AddDataTable3Row(t3r);
                                }
                            }

                        }

                        else if (_QueryParms.RankedByStyleSize)
                        {
                            var SOHGrouped = SOH.GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Size_FK });
                            foreach (var Item in SOHGrouped)
                            {
                                var StylePk = Item.FirstOrDefault().TLSOH_Style_FK;
                                var ColourPk = Item.FirstOrDefault().TLSOH_Colour_FK;
                                var SizePk = Item.FirstOrDefault().TLSOH_Size_FK;

                                DataSet13.DataTable3Row t3r = dataTable3.NewDataTable3Row();
                                t3r.Pk = 1;

                                t3r.Style = context.TLADM_Styles.Find(StylePk).Sty_Description;
                                t3r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == ColourPk).Col_Display;

                                var Sty = context.TLADM_Styles.Find(StylePk);
                                if (Sty != null && !Sty.Sty_WorkWear)
                                {
                                    t3r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_Description;
                                }
                                else
                                {

                                    t3r.Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_ContiSize.ToString();
                                }
                                t3r.DisplayOrder = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_DisplayOrder;
                                t3r.BoxedQty = Item.Sum(x => x.TLSOH_BoxedQty);
                                t3r.Total_BoxedQty = Item.Sum(x => x.TLSOH_BoxedQty);

                                dataTable3.AddDataTable3Row(t3r);

                            }

                        }
                    }
                }

                DataSet13.DataTable1Row nr = dataTable1.NewDataTable1Row();
                nr.Pk = 1;
                nr.fromDate = _Svces.fromDate;
                nr.toDate = _Svces.toDate;
                nr.title = _Svces.Title;

                dataTable1.AddDataTable1Row(nr);

                if (dataTable2.Count == 0)
                {
                    DataSet13.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                    t2r.Pk = 1;
                    t2r.ErrorLog = "No records found for selection made";
                    dataTable2.AddDataTable2Row(t2r);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                ds.Tables.Add(dataTable3);

                if (!_QueryParms.SummarisedSalesByCompany &&
                    !_QueryParms.SummarisedSalesByCustomer &&
                    !_QueryParms.RankedByStyleColour &&
                    !_QueryParms.RankedByStyleColourSize &&
                    !_QueryParms.RankedByStyleSize)
                {
                    CustomerSales CustSales = new CustomerSales();
                    CustSales.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CustSales;
                }
                else if (_QueryParms.SummarisedSalesByCustomer)
                {
                    CustomerSalesSummarised CustSales = new CustomerSalesSummarised();
                    CustSales.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CustSales;
                }
                else if (_QueryParms.SummarisedSalesByCompany)
                {
                    CustomerSalesForCompany CustSales = new CustomerSalesForCompany();
                    CustSales.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CustSales;
                }
                else if (_QueryParms.RankedByStyleColour)
                {
                    SalesRankedByStyle SalesByStyle = new SalesRankedByStyle();
                    SalesByStyle.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesByStyle;
                }
                else if (_QueryParms.RankedByStyleColourSize)
                {
                    SalesRankedByStyleColourSize SalesByStyle = new SalesRankedByStyleColourSize();
                    SalesByStyle.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesByStyle;
                }
                else if (_QueryParms.RankedByStyleSize)
                {
                    SalesRankedBySize SalesByStyle = new SalesRankedBySize();
                    SalesByStyle.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesByStyle;
                }
            }

            else if (_RepNo == 14)   // WareHouse Stock take Lists 
            {
                DataSet ds = new DataSet();
                DataSet14.DataTable1DataTable dataTable1 = new DataSet14.DataTable1DataTable();

                Repository repo = new Repository();

                //--------------------------------------------
                // Standard Parameters (If applicable)
                //--------------------------------------------
                var SOH = repo.SOHStockTakeQuery(_QueryParms).OrderBy(x => x.TLSOH_PastelNumber).ToList();

                using (var context = new TTI2Entities())
                {
                    foreach (var Record in SOH)
                    {
                        DataSet14.DataTable1Row t2r = dataTable1.NewDataTable1Row();

                        t2r.STList_BoxedQty = Record.TLSOH_BoxedQty;
                        t2r.StList_Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLSOH_Style_FK).Sty_Description;
                        t2r.STList_Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.TLSOH_Colour_FK).Col_Display;
                        var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLSOH_Style_FK);
                        if (Sty != null && !Sty.Sty_WorkWear)
                        {
                            t2r.STList_Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLSOH_Size_FK).SI_Description;
                        }
                        else
                        {
                            t2r.STList_Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLSOH_Size_FK).SI_ContiSize.ToString();
                        }
                        t2r.STList_BoxNumber = Record.TLSOH_BoxNumber;
                        t2r.STList_BoxedQty = Record.TLSOH_BoxedQty;
                        t2r.STList_WareHouse = context.TLADM_WhseStore.Find(Record.TLSOH_WareHouse_FK).WhStore_Description;
                        dataTable1.AddDataTable1Row(t2r);
                    }
                }

                if (dataTable1.Count == 0)
                {
                    DataSet14.DataTable1Row xr = dataTable1.NewDataTable1Row();
                    xr.STList_ErrorList = "There are no entries for selection made";
                    dataTable1.AddDataTable1Row(xr);
                }

                ds.Tables.Add(dataTable1);
                StockTakeList StList = new StockTakeList();
                StList.SetDataSource(ds);
                crystalReportViewer1.ReportSource = StList;
            }
            else if (_RepNo == 15)   // Delivery Note Register 
            {
                DataSet ds = new DataSet();
                DataSet15.DataTable1DataTable dataTable1 = new DataSet15.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    if (_Svces.DeliveryNote)
                    {
                        var OrdersAllocated = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_Delivered && x.TLORDA_DeliveredDate >= _Svces.fromDate && x.TLORDA_DeliveredDate <= _Svces.toDate).GroupBy(x => x.TLORDA_DelTransNumber).ToList();
                        foreach (var Order in OrdersAllocated)
                        {
                            foreach (var Group in Order)
                            {
                                DataSet15.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.FromDate = _Svces.fromDate;
                                nr.ToDate = _Svces.toDate;

                                var DeliveryNo = Group.TLORDA_DelTransNumber;

                                nr.Title = "Delivery Note Register";
                                nr.DeliveryNote = "F" + DeliveryNo.ToString().PadLeft(5, '0');
                                nr.DeliveryDate = (DateTime)Group.TLORDA_DeliveredDate;
                                nr.Transporter = context.TLADM_Transporters.Find(Group.TLORDA_Transporter_FK).TLTRNS_Description;
                                var Pk = Group.TLORDA_POOrder_FK;

                                var PO = context.TLCSV_PurchaseOrder.Find(Pk);
                                if (PO != null)
                                {
                                    nr.PurchaseOrder = PO.TLCSVPO_PurchaseOrder;
                                    nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == PO.TLCSVPO_Customer_FK).Cust_Description;
                                }
                                else
                                {
                                    nr.Customer = "Delivery Cancelled";
                                }

                                dataTable1.AddDataTable1Row(nr);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var OrdersAllocated = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_TransDate >= _Svces.fromDate && x.TLORDA_TransDate <= _Svces.toDate).GroupBy(x => x.TLORDA_TransNumber).ToList();
                        foreach (var Order in OrdersAllocated)
                        {
                            foreach (var Group in Order)
                            {
                                DataSet15.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.FromDate = _Svces.fromDate;
                                nr.ToDate = _Svces.toDate;

                                var DeliveryNo = Group.TLORDA_TransNumber;

                                nr.Title = "Picking List Register";
                                nr.DeliveryNote = "P" + DeliveryNo.ToString().PadLeft(5, '0');
                                if (Group.TLORDA_PLStockOrder)
                                    nr.DeliveryNote += "****";

                                nr.DeliveryDate = (DateTime)Group.TLORDA_TransDate;
                                nr.Transporter = string.Empty;
                                var Pk = Group.TLORDA_POOrder_FK;

                                var PO = context.TLCSV_PurchaseOrder.Find(Pk);
                                if (PO != null)
                                {
                                    nr.PurchaseOrder = PO.TLCSVPO_PurchaseOrder;
                                    nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == PO.TLCSVPO_Customer_FK).Cust_Description;
                                }

                                dataTable1.AddDataTable1Row(nr);
                                break;
                            }
                        }
                    }

                    ds.Tables.Add(dataTable1);
                    CDNoteRegister StList = new CDNoteRegister();
                    StList.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = StList;
                }
            }
            else if (_RepNo == 16)
            {
                DataSet ds = new DataSet();
                DataSet16.DataTable1DataTable dataTable1 = new DataSet16.DataTable1DataTable();
                Repository repo = new Repository();

                var Records = repo.SelectNotReceipted(_QueryParms);
                using (var context = new TTI2Entities())
                {
                    foreach (var Record in Records)
                    {
                        DataSet16.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.BoxNumber = Record.TLCMTWC_BoxNumber;
                        nr.BoxQty = Record.TLCMTWC_Qty;
                        nr.BoxWeight = Record.TLCMTWC_Weight;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.TLCMTWC_Colour_FK).Col_Display;
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLCMTWC_Style_FK).Sty_Description;
                        var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == Record.TLCMTWC_Style_FK);
                        if(Sty != null && !Sty.Sty_WorkWear)
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLCMTWC_Size_FK).SI_Description;
                        }
                        else
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.TLCMTWC_Size_FK).SI_ContiSize.ToString();
                        }
                        
                        nr.ToWhse = context.TLADM_WhseStore.Find(Record.TLCMTWC_ToWhse_FK).WhStore_Description;
                        if (Record.TLCMTWC_PickList_FK != null)
                        {
                            var BoxAllocated = context.TLCSV_BoxSelected.Find(Record.TLCMTWC_PickList_FK);
                            if (BoxAllocated != null)
                            {
                                nr.PickNumber = BoxAllocated.TLCSV_PLDetails;
                                nr.DeliveryNumber = BoxAllocated.TLCSV_DNDeails;
                                nr.TransDate = (DateTime)BoxAllocated.TLCSV_DespatchedDate;
                            }
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }

                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet16.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.NoDataFound = "No data found matching selection criteria";
                        dataTable1.AddDataTable1Row(nr);
                    }
                    ds.Tables.Add(dataTable1);
                    DespatchedNotReceipted Despatch = new DespatchedNotReceipted();
                    Despatch.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = Despatch;
                }
            }
            else if (_RepNo == 17) // outstanding orders by style / colour / size and month
            {
                DataSet ds = new DataSet();
                DataSet17.DataTable1DataTable dataTable1 = new DataSet17.DataTable1DataTable();
                IList<TLCSV_PurchaseOrder> _POOrder = null;
                IList<TLCSV_StockOnHand> _SOH = null;

                Repository repo = new Repository();

                //================================================================
                DataTable dt = new DataTable();
                dt.Columns.Add("Style", typeof(int));     // 0
                dt.Columns.Add("Colour", typeof(int));    // 1
                dt.Columns.Add("Size", typeof(int));      // 2
                //===============================================================================
                dt.Columns.Add("01", typeof(int));        // 3  
                dt.Columns[3].DefaultValue = 0;
                dt.Columns.Add("02", typeof(int));        // 4    
                dt.Columns[4].DefaultValue = 0;
                dt.Columns.Add("03", typeof(int));        // 5
                dt.Columns[5].DefaultValue = 0;
                dt.Columns.Add("04", typeof(int));        // 6
                dt.Columns[6].DefaultValue = 0;
                dt.Columns.Add("05", typeof(int));        // 7
                dt.Columns[7].DefaultValue = 0;
                dt.Columns.Add("06", typeof(int));        // 8
                dt.Columns[8].DefaultValue = 0;
                dt.Columns.Add("07", typeof(int));        // 9
                dt.Columns[9].DefaultValue = 0;
                dt.Columns.Add("08", typeof(int));        // 10
                dt.Columns[10].DefaultValue = 0;
                dt.Columns.Add("09", typeof(int));        // 11
                dt.Columns[11].DefaultValue = 0;
                dt.Columns.Add("10", typeof(int));       // 12
                dt.Columns[12].DefaultValue = 0;
                dt.Columns.Add("11", typeof(int));       // 13
                dt.Columns[13].DefaultValue = 0;
                dt.Columns.Add("12", typeof(int));       // 14   
                dt.Columns[14].DefaultValue = 0;

                var PGroupOrderDetails = repo.OutStandingOrders(_QueryParms).GroupBy(x => new { x.TLCUSTO_Style_FK, x.TLCUSTO_Colour_FK, x.TLCUSTO_Size_FK });

                using (var context = new TTI2Entities())
                {
                    foreach (var Group in PGroupOrderDetails)
                    {
                        var StylePk = Group.FirstOrDefault().TLCUSTO_Style_FK;
                        var ColourPk = Group.FirstOrDefault().TLCUSTO_Colour_FK;
                        var SizePk = Group.FirstOrDefault().TLCUSTO_Size_FK;

                        //=====================================================
                        // Add a new Record to the data table;
                        //=================================================
                        DataRow Row = dt.NewRow();
                        Row[0] = StylePk;
                        Row[1] = ColourPk;
                        Row[2] = SizePk;

                        //==========================================================================
                        // Group into Months 
                        //=======================================================
                        var GroupedByMonth = Group.GroupBy(x => x.TLCUSTO_DateRequired.Value.Month);
                        foreach (var Mnth in GroupedByMonth)
                        {
                            string Mth = Mnth.FirstOrDefault().TLCUSTO_DateRequired.Value.Month.ToString().PadLeft(2, '0');
                            if (_QueryParms.Months.Count != 0)
                            {
                                int MthKey = Mnth.FirstOrDefault().TLCUSTO_DateRequired.Value.Month;
                                var SelectedMths = _QueryParms.Months.Where(x => x.Mth_Pk == MthKey).FirstOrDefault();
                                if(SelectedMths == null)
                                {
                                    continue;
                                }
                            }
                            var Picked = Mnth.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0;
                            var Ordered = Mnth.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0;

                            var ColIndex = dt.Columns.IndexOf(Mth);
                            if (ColIndex != 0 && (Ordered - Picked > 0))
                            {
                                Row[ColIndex] = Row.Field<int>(ColIndex) + (Ordered - Picked);
                            }

                        }

                        dt.Rows.Add(Row);
                    }

                    foreach (DataRow Row in dt.Rows)
                    {
                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.Field<int>(0)).Sty_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Row.Field<int>(1)).Col_Display;
                        var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == Row.Field<int>(0));
                        if (Sty != null && !Sty.Sty_WorkWear)
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.Field<int>(2)).SI_Description;
                        }
                        else
                        {
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.Field<int>(2)).SI_ContiSize.ToString();
                        }
                        var StyK = Row.Field<int>(0);
                        var ColK = Row.Field<int>(1);
                        var SzeK = Row.Field<int>(2);

                        nr.StockAvail = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == StyK && x.TLSOH_Colour_FK == ColK && x.TLSOH_Size_FK == SzeK && !x.TLSOH_Picked).Sum(x =>(int ?)x.TLSOH_BoxedQty) ?? 0;
                        nr.Jan = Row.Field<int>(3);
                        nr.Feb = Row.Field<int>(4);
                        nr.Mar = Row.Field<int>(5);
                        nr.Apr = Row.Field<int>(6);
                        nr.May = Row.Field<int>(7);
                        nr.Jun = Row.Field<int>(8);
                        nr.Jul = Row.Field<int>(9);
                        nr.Aug = Row.Field<int>(10);
                        nr.Sep = Row.Field<int>(11);
                        nr.Oct = Row.Field<int>(12);
                        nr.Nov = Row.Field<int>(13);
                        nr.Dec = Row.Field<int>(14);
                        if(nr.Jan + nr.Feb + nr.Mar + nr.Apr + nr.May + nr.Jun + nr.Jul + nr.Aug + nr.Sep + nr.Aug + nr.Sep + nr.Oct + nr.Nov + nr.Dec == 0)
                        {
                            continue;
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }

                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.NoDataFound = "No data found matching selection criteria";
                        dataTable1.AddDataTable1Row(nr);
                    }
                    ds.Tables.Add(dataTable1);

                    OutStandingOrdersByMonth OutStanding = new OutStandingOrdersByMonth();
                    OutStanding.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = OutStanding;
                }
            }

            else if (_RepNo == 18 || _RepNo == 19 || _RepNo == 20)      // WareHouse Transfer  Picking list 
            {
                DataSet ds = new DataSet();
                DataSet18.DataTable1DataTable dataTable1 = new DataSet18.DataTable1DataTable();
                DataSet18.DataTable2DataTable dataTable2 = new DataSet18.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    var WhseTransfer = context.TLCSV_WhseTransfer.Find(_Pk);
                    if (WhseTransfer != null)
                    {
                        DataSet18.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        if (_RepNo == 18)
                            nr.ReportTitle = "Inter WareHouse Transfer PickList";
                        else if (_RepNo == 19)
                            nr.ReportTitle = "Inter WareHouse Delivery Note";
                        else
                            nr.ReportTitle = "Inter WareHouse Receipt";

                        nr.TransDate = (DateTime)WhseTransfer.TLCSVWHT_PickListDate;
                        nr.From_WareHouse = context.TLADM_WhseStore.Find(WhseTransfer.TLCSVWHT_FromWhse_Fk).WhStore_Description;
                        nr.To_WareHouse = context.TLADM_WhseStore.Find(WhseTransfer.TLCSVWHT_ToWhse_Fk).WhStore_Description;
                        nr.TransNumber = WhseTransfer.TLCSVWHT_PickListNo;

                        dataTable1.AddDataTable1Row(nr);

                        var WhseDetails = context.TLCSV_WhseTransferDetail.Where(x => x.TLCSVWHTD_WhseTranfer_FK == _Pk).ToList();
                        foreach (var Detail in WhseDetails)
                        {
                            DataSet18.DataTable2Row nrd = dataTable2.NewDataTable2Row();
                            nrd.Pk = 1;
                            var SOH = context.TLCSV_StockOnHand.Find(Detail.TLCSVWHTD_TLSOH_Fk);
                            if (SOH != null)
                            {
                                nrd.BoxNumber = SOH.TLSOH_BoxNumber;
                                nrd.Style = _Styles.FirstOrDefault(s => s.Sty_Id == SOH.TLSOH_Style_FK).Sty_Description;
                                nrd.Colour = _Colours.FirstOrDefault(s => s.Col_Id == SOH.TLSOH_Colour_FK).Col_Display;

                                var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == SOH.TLSOH_Style_FK);
                                if (Sty != null && !Sty.Sty_WorkWear)
                                {
                                    nrd.Size = _Sizes.FirstOrDefault(s => s.SI_id == SOH.TLSOH_Size_FK).SI_Description;
                                }
                                else
                                {
                                    nrd.Size = _Sizes.FirstOrDefault(s => s.SI_id == SOH.TLSOH_Size_FK).SI_ContiSize.ToString();
                                }
                                nrd.Boxed_Qty = SOH.TLSOH_BoxedQty;
                                nrd.Weight = SOH.TLSOH_Weight;

                                dataTable2.AddDataTable2Row(nrd);

                            }
                        }

                    }

                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                InterTransferPickList Intertrans = new InterTransferPickList();
                Intertrans.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Intertrans;
            }
            else if (_RepNo == 21)      // Sales By Style By Period 
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                DataSet19.DataTable2DataTable dataTable2 = new DataSet19.DataTable2DataTable();
                Util core = new Util();
                IList<TLCSV_StockOnHand> PGroupOrderDetails = null;

                Repository repo = new Repository();
                //================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Line", typeof(int));     // 0
                dt.Columns.Add("Style", typeof(int));    // 1
                //===============================================================================
                dt.Columns.Add("01", typeof(int));       // 2  
                dt.Columns[2].DefaultValue = 0;
                dt.Columns.Add("02", typeof(int));       // 3    
                dt.Columns[3].DefaultValue = 0;
                dt.Columns.Add("03", typeof(int));       // 4
                dt.Columns[4].DefaultValue = 0;
                dt.Columns.Add("04", typeof(int));       // 5
                dt.Columns[5].DefaultValue = 0;
                dt.Columns.Add("05", typeof(int));       // 6
                dt.Columns[6].DefaultValue = 0;
                dt.Columns.Add("06", typeof(int));       // 7
                dt.Columns[7].DefaultValue = 0;
                dt.Columns.Add("07", typeof(int));       // 8
                dt.Columns[8].DefaultValue = 0;
                dt.Columns.Add("08", typeof(int));       // 9
                dt.Columns[9].DefaultValue = 0;
                dt.Columns.Add("09", typeof(int));       // 10
                dt.Columns[10].DefaultValue = 0;
                dt.Columns.Add("10", typeof(int));       // 11
                dt.Columns[11].DefaultValue = 0;
                dt.Columns.Add("11", typeof(int));       // 12
                dt.Columns[12].DefaultValue = 0;
                dt.Columns.Add("12", typeof(int));       // 13   
                dt.Columns[13].DefaultValue = 0;
                using (var context = new TTI2Entities())
                {
                    var LstDayMnth = core.LastDayOfMonth(DateTime.Now.Month);
                    var CutOffDate = DateTime.Now.AddDays(LstDayMnth - DateTime.Now.Day + 1).AddYears(-1);

                    PGroupOrderDetails = repo.SOHSales(_QueryParms).ToList();

                    var GroupedData = PGroupOrderDetails.GroupBy(x => new { x.TLSOH_Style_FK });

                    foreach (var Group in GroupedData)
                    {
                        var StylePk = Group.FirstOrDefault().TLSOH_Style_FK;
                        //=====================================================
                        // Add a new Record to the data table;
                        //=================================================
                        DataRow Row = dt.NewRow();
                        Row[0] = StylePk;
                        var RecordGroup = Group.GroupBy(x => x.TLSOH_SoldDate);
                        foreach (var Record in RecordGroup)
                        {
                            var SoldDate = Convert.ToDateTime(Record.FirstOrDefault().TLSOH_SoldDate);
                            var MthKey = SoldDate.Month.ToString().PadLeft(2, '0');

                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<int>(ColIndex) + Record.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                            }
                        }
                        dt.Rows.Add(Row);
                    }

                    foreach (DataRow Row in dt.Rows)
                    {
                        DataSet19.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.Field<int>(0)).Sty_Description;
                        nr.Pk = 1;
                        nr.Jan = Row.Field<int>(2);
                        nr.Feb = Row.Field<int>(3);
                        nr.Mar = Row.Field<int>(4);
                        nr.Apr = Row.Field<int>(5);
                        nr.May = Row.Field<int>(6);
                        nr.Jun = Row.Field<int>(7);
                        nr.Jul = Row.Field<int>(8);
                        nr.Aug = Row.Field<int>(9);
                        nr.Sep = Row.Field<int>(10);
                        nr.Oct = Row.Field<int>(11);
                        nr.Nov = Row.Field<int>(12);
                        nr.Dec = Row.Field<int>(13);

                        dataTable1.AddDataTable1Row(nr);
                    }

                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet19.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.NoDataFound = "No data found matching selection criteria";
                        dataTable1.AddDataTable1Row(nr);
                    }
                    ds.Tables.Add(dataTable1);

                    DataSet19.DataTable2Row nrx = dataTable2.NewDataTable2Row();
                    nrx.Pk = 1;
                    nrx.FromDate = _QueryParms.FromDate;
                    nrx.ToDate = _QueryParms.ToDate;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Sales by Style by Month expressed in Units ");
                    if (_QueryParms.Customers.Count > 0)
                    {
                        String Name = string.Empty;

                        foreach (var Customer in _QueryParms.Customers)
                        {
                            Name += context.TLADM_CustomerFile.Find(Customer.Cust_Pk).Cust_Description + " ";
                        }

                        sb.Append(Environment.NewLine + "Customers Selected : " + Name.ToString());
                    }
                    nrx.Title = sb.ToString();
                    dataTable2.AddDataTable2Row(nrx);

                    ds.Tables.Add(dataTable2);
                    SalesByStyle SalesBy = new SalesByStyle();
                    SalesBy.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesBy;
                }
            }
            else if (_RepNo == 22)      // Sales By Style By Customer 
            {
                DataSet ds = new DataSet();
                DataSet20.DataTable1DataTable dataTable1 = new DataSet20.DataTable1DataTable();
                DataSet20.DataTable2DataTable dataTable2 = new DataSet20.DataTable2DataTable();
                Util core = new Util();
                IList<TLCSV_StockOnHand> PGroupOrderDetails = null;
                string[][] ColumnNames = null;


                Repository repo = new Repository();

                ColumnNames = new string[][]
                 {   new string[] {"Text6", null},
                        new string[] {"Text7", null},
                        new string[] {"Text8", null},
                        new string[] {"Text9", null},
                        new string[] {"Text10", null},
                        new string[] {"Text11", null},
                        new string[] {"Text12", null},
                        new string[] {"Text13", null}
                 };

                //================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Line", typeof(int));     // 0
                dt.Columns.Add("Style", typeof(int));    // 1

                DataColumnCollection columns = dt.Columns;
                //-----------------------------------------------------------------------------
                // Set Up Customer Columns
                //-------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    var Customers = core.CurrentCustomers(true).ToList();
                    foreach (var Customer in Customers)
                    {
                        if (!columns.Contains(Customer.Value))
                        {
                            try
                            {
                                dt.Columns.Add(Customer.Value, typeof(int));
                                dt.Columns[Customer.Value].DefaultValue = 0;
                                foreach (var ColumnName in ColumnNames)
                                {
                                    if (ColumnName[1] == null)
                                    {
                                        ColumnName[1] = _Customers.FirstOrDefault(s => s.Cust_Pk == Customer.Key).Cust_Description;
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }

                    var LstDayMnth = core.LastDayOfMonth(DateTime.Now.Month);
                    var CutOffDate = DateTime.Now.AddDays(LstDayMnth - DateTime.Now.Day + 1).AddYears(-1);

                    PGroupOrderDetails = repo.SOHSales(_QueryParms).ToList();
                    var GroupedData = PGroupOrderDetails.GroupBy(x => new { x.TLSOH_Style_FK });
                    foreach (var Group in GroupedData)
                    {
                        var StylePk = Group.FirstOrDefault().TLSOH_Style_FK;
                        //=====================================================
                        // Add a new Record to the data table;
                        //=================================================
                        DataRow Row = dt.NewRow();
                        Row[0] = StylePk;
                        var RecordGroup = Group.GroupBy(x => x.TLSOH_Customer_Fk);
                        foreach (var Record in RecordGroup)
                        {
                            var CustKey = Record.FirstOrDefault().TLSOH_Customer_Fk;
                            if (CustKey == null)
                                continue;

                            var MthKey = _Customers.FirstOrDefault(s => s.Cust_Pk == CustKey).Cust_Code;
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                                Row[ColIndex] = Row.Field<int>(ColIndex) + Record.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;

                        }
                        dt.Rows.Add(Row);
                    }

                    foreach (DataRow Row in dt.Rows)
                    {
                        DataSet20.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.Field<int>(0)).Sty_Description;
                        nr.Pk = 1;
                        nr.Col1 = Row.Field<int>(2);
                        nr.Col2 = Row.Field<int>(3);
                        nr.Col3 = Row.Field<int>(4);
                        nr.Col4 = Row.Field<int>(5);
                        nr.Col5 = Row.Field<int>(6);
                        // nr.Col6 = Row.Field<int>(7);
                        // nr.Col7 = Row.Field<int>(8);
                        // nr.Col8 = Row.Field<int>(9);

                        dataTable2.AddDataTable2Row(nr);
                    }

                    DataView DataV = dataTable2.DefaultView;
                    DataV.Sort = "Style";

                    DataSet20.DataTable1Row xnr = dataTable1.NewDataTable1Row();
                    xnr.Pk = 1;
                    xnr.FromDate = _QueryParms.FromDate;
                    xnr.ToDate = _QueryParms.ToDate;

                    if (dataTable2.Count == 0)
                        xnr.ErrorLog = "No data found matching selection criteria";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Sales by Style by Customer expressed in Units");
                    xnr.Title = sb.ToString();
                    dataTable1.AddDataTable1Row(xnr);

                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(DataV.ToTable());
                    SalesByCustomer SalesBy = new SalesByCustomer();

                    SalesBy.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesBy;
                    IEnumerator ie = SalesBy.Section2.ReportObjects.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                        {
                            CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                            var result = (from u in ColumnNames
                                          where u[0] == to.Name
                                          select u).FirstOrDefault();

                            if (result != null && result[1] != null)
                                to.Text = result[1];
                        }
                    }
                }
            }
            else if (_RepNo == 23)      // Repack Centre Sales By Style By Customer 
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable1 = new DataSet21.DataTable1DataTable();
                DataSet21.DataTable2DataTable dataTable2 = new DataSet21.DataTable2DataTable();
                string[][] ColumnNames = null;
                TLCSV_RePackTransactions POTrans = null;
                Util core = new Util();

                ColumnNames = new string[][]
                    {   new string[] {"Text3", string.Empty},
                        new string[] {"Text4", string.Empty},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty}

                    };

                var CNames = core.CreateColumnNames();
                int i = 0;

                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                using (var context = new TTI2Entities())
                {
                    foreach (var RePackConfig in _QueryParms.RePackConfigs)
                    {
                        if (POTrans == null)
                            POTrans = context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrder_FK == RePackConfig.PORConfig_PONumber_Fk).FirstOrDefault();

                        var GroupedDetails = context.TLCSV_RePackConfig.Where(x => x.PORConfig_BoxNumber_Key == RePackConfig.PORConfig_BoxNumber_Key).GroupBy(x => x.PORConfig_BoxNumber);

                        foreach (var Details in GroupedDetails)
                        {
                            bool First = true;
                            DataSet21.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = 1;
                            nr.Col1 = 0;
                            nr.Col2 = 0;
                            nr.Col3 = 0;
                            nr.Col4 = 0;
                            nr.Col5 = 0;
                            nr.Col6 = 0;
                            nr.Col7 = 0;
                            nr.Col8 = 0;
                            nr.Col9 = 0;
                            nr.Col10 = 0;
                            nr.Col11 = 0;
                            nr.Col12 = 0;

                            foreach (var Detail in Details)
                            {
                                if (First)
                                {
                                    First = !First;
                                    nr.Repack_Description = "Ordered";
                                    nr.Repack_BoxNumber = Detail.PORConfig_BoxNumber;
                                }

                                var SizePk = Detail.PORConfig_Size_FK;
                                var Size = context.TLADM_Sizes.Find(SizePk);
                                if (Size != null)
                                {
                                    if (Size.SI_ColNumber == 1)
                                        nr.Col1 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 2)
                                        nr.Col2 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 3)
                                        nr.Col3 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 4)
                                        nr.Col4 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 5)
                                        nr.Col5 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 6)
                                        nr.Col6 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 7)
                                        nr.Col7 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 8)
                                        nr.Col8 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 9)
                                        nr.Col9 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else if (Size.SI_ColNumber == 10)
                                        nr.Col10 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                    else
                                        nr.Col11 += Detail.PORConfig_SizeBoxQty * Detail.PORConfig_TotalBoxes;
                                }

                                nr.Col12 = Detail.PORConfig_TotalBoxes;

                            }
                            dataTable1.AddDataTable1Row(nr);

                            //-----------------------------------------------------------------
                            // Now we have to take care of the delivery sides of the equation
                            //--------------------------------------------------------------
                            First = true;

                            nr = dataTable1.NewDataTable1Row();
                            nr.Pk = RePackConfig.PORConfig_PONumber_Fk;
                            nr.Col1 = 0;
                            nr.Col2 = 0;
                            nr.Col3 = 0;
                            nr.Col4 = 0;
                            nr.Col5 = 0;
                            nr.Col6 = 0;
                            nr.Col7 = 0;
                            nr.Col8 = 0;
                            nr.Col9 = 0;
                            nr.Col10 = 0;
                            nr.Col11 = 0;
                            nr.Col12 = 0;

                            foreach (var Detail in Details)
                            {
                                if (First)
                                {
                                    First = !First;
                                    nr.Repack_Description = "Delivered";
                                    nr.Repack_BoxNumber = Detail.PORConfig_BoxNumber;
                                }

                                var SizePk = Detail.PORConfig_Size_FK;
                                var Size = context.TLADM_Sizes.Find(SizePk);
                                if (Size != null)
                                {
                                    if (Size.SI_ColNumber == 1)
                                        nr.Col1 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 2)
                                        nr.Col2 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 3)
                                        nr.Col3 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 4)
                                        nr.Col4 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 5)
                                        nr.Col5 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 6)
                                        nr.Col6 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 7)
                                        nr.Col7 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 8)
                                        nr.Col8 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 9)
                                        nr.Col9 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else if (Size.SI_ColNumber == 10)
                                        nr.Col10 += Detail.PORConfig_SizeBoxQty_Delivered;
                                    else
                                        nr.Col11 += Detail.PORConfig_SizeBoxQty_Delivered;
                                }
                            }
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }

                    if (POTrans != null)
                    {
                        var RePackTrans = context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrder_FK == POTrans.REPACT_PurchaseOrder_FK).ToList();

                        foreach (var RePackTran in RePackTrans)
                        {
                            DataSet21.DataTable2Row TRow = dataTable2.NewDataTable2Row();
                            TRow.Pk = RePackTran.REPACT_PurchaseOrder_FK;
                            var SOH = context.TLCSV_StockOnHand.Find(RePackTran.REPACT_StockOnHand_FK);
                            if (SOH != null)
                            {
                                TRow.BoxStyle = _Styles.FirstOrDefault(s => s.Sty_Id == SOH.TLSOH_Style_FK).Sty_Description;
                                TRow.BoxNumber = SOH.TLSOH_BoxNumber;
                                TRow.BoxColor = _Colours.FirstOrDefault(s => s.Col_Id == SOH.TLSOH_Colour_FK).Col_Display;
                                TRow.BoxSize = _Sizes.FirstOrDefault(s => s.SI_id == SOH.TLSOH_Size_FK).SI_Description;
                                TRow.BoxedQty = RePackTran.REPACT_BoxedQty;
                                TRow.BoxLineConfig = context.TLCSV_RePackConfig.Find(RePackTran.REPACT_RePackConfig_FK).PORConfig_BoxNumber;
                            }

                            dataTable2.AddDataTable2Row(TRow);
                        }
                    }
                }


                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                RePackStatus panCTM = new RePackStatus();
                panCTM.SetDataSource(ds);

                IEnumerator ie = panCTM.Section2.ReportObjects.GetEnumerator();
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

                crystalReportViewer1.ReportSource = panCTM;



            }
            else if (_RepNo == 24)      // Cross Border Documentation Report Ex CMT to Ficksburg
            {
                DataSet ds = new DataSet();
                DataSet22.DataTable1DataTable dataTable1 = new DataSet22.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    foreach (var OrderAlloc in _QueryParms.OrdersAllocated)
                    {
                        var OrderA = context.TLCSV_OrderAllocated.Find(OrderAlloc.TLORDA_Pk);
                        if (OrderA != null)
                        {
                            var Entries = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == OrderA.TLORDA_TransNumber).ToList();
                            foreach (var Entry in Entries)
                            {
                                DataSet22.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.BoxNumber = Entry.TLSOH_BoxNumber;
                                nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Entry.TLSOH_Style_FK).Sty_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Entry.TLSOH_Colour_FK).Col_Display;
                                nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Entry.TLSOH_Size_FK).SI_Description;
                                nr.Weight = Entry.TLSOH_Weight;
                                nr.Units = Entry.TLSOH_BoxedQty;
                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                CrossBorder CrossBorder = new CrossBorder();
                CrossBorder.SetDataSource(ds);

                crystalReportViewer1.ReportSource = CrossBorder;
            }
            else if (_RepNo == 25)      // Customer Transactional History
            {
                DataSet ds = new DataSet();
                DataSet23.DataTable1DataTable dataTable1 = new DataSet23.DataTable1DataTable();
                DataSet23.DataTable2DataTable dataTable2 = new DataSet23.DataTable2DataTable();
                var repo = new Repository();
                IList<TLCSV_PuchaseOrderDetail> Dets = null;

                using (var context = new TTI2Entities())
                {
                    if (_QueryParms.TransactHistory)
                    {
                        var Boxes = repo.CustomerAudit(_QueryParms, _QueryParms.Customers.FirstOrDefault().Cust_Pk).ToList();

                        foreach (var Box in Boxes)
                        {
                            DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = 1;
                            var POOrder = context.TLCSV_PurchaseOrder.Find(Box.TLSOH_POOrder_FK);
                            if (POOrder != null)
                            {
                                nr.TPurchaseNo = POOrder.TLCSVPO_PurchaseOrder;
                                nr.TCustName = _Customers.FirstOrDefault(s => s.Cust_Pk == POOrder.TLCSVPO_Customer_FK).Cust_Description;
                                nr.TRequiredDate = POOrder.TLCSVPO_RequiredDate;
                                nr.TOrderDate = POOrder.TLCSVPO_TransDate;
                                nr.TStatus = POOrder.TLCSVPO_Closeed;
                                nr.TOrderQty = 0;

                                /*nr.TOrderQty = (from T1 in context.TLCSV_PurchaseOrder
                                                  join T2 in context.TLCSV_PuchaseOrderDetail
                                                  on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                                  where T2.TLCUSTO_Style_FK == Box.TLSOH_Style_FK &&
                                                        T2.TLCUSTO_Colour_FK == Box.TLSOH_Colour_FK &&
                                                        T2.TLCUSTO_Size_FK == Box.TLSOH_Size_FK
                                                        select T2).Sum(x => x.TLCUSTO_Qty);*/


                                nr.TStyle = _Styles.FirstOrDefault(s => s.Sty_Id == Box.TLSOH_Style_FK).Sty_Description;
                                nr.TColour = _Colours.FirstOrDefault(s => s.Col_Id == Box.TLSOH_Colour_FK).Col_Display;
                                var xSize = _Sizes.FirstOrDefault(s => s.SI_id == Box.TLSOH_Size_FK);
                                if (xSize != null)
                                {
                                   var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == Box.TLSOH_Style_FK);
                                    if (Sty != null && !Sty.Sty_WorkWear)
                                    {
                                        nr.TSize = xSize.SI_Description;
                                    }
                                    else
                                    {
                                        nr.TSize = xSize.SI_ContiSize.ToString();
                                    }
                                    nr.TDisplayOrder = xSize.SI_DisplayOrder;
                                }

                                nr.TBoxQty = Box.TLSOH_BoxedQty;
                                nr.TBoxNumber = Box.TLSOH_BoxNumber;

                                if (Box.TLSOH_Picked)
                                {
                                    if (Box.TLSOH_PickListDate != null)
                                        nr.TPLDate = (DateTime)Box.TLSOH_PickListDate;

                                    nr.TPLNumber = "PL" + Box.TLSOH_PickListNo.ToString().PadLeft(5, '0');
                                }

                                if (Box.TLSOH_Sold)
                                {
                                    if (Box.TLSOH_DNListDate != null)
                                        nr.TDLDate = (DateTime)Box.TLSOH_DNListDate;

                                    nr.TDLNumber = "DN" + Box.TLSOH_DNListNo.ToString().PadLeft(5, '0');
                                }
                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                    }
                    else
                    {
                        foreach (var PurchaseOrder in _QueryParms.PurchaseOrders)
                        {
                            Dets = (from PODetail in context.TLCSV_PuchaseOrderDetail
                                    join xSizes in context.TLADM_Sizes on PODetail.TLCUSTO_Size_FK equals xSizes.SI_id
                                    where PODetail.TLCUSTO_PurchaseOrder_FK == PurchaseOrder.TLCSVPO_Pk
                                    orderby PODetail.TLCUSTO_Style_FK, PODetail.TLCUSTO_Colour_FK, xSizes.SI_DisplayOrder
                                    select PODetail).ToList();

                            foreach (var PODetail in Dets)
                            {

                                DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Pk = 1;
                                var POOrder = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK);
                                if (POOrder != null)
                                {
                                    nr.TPurchaseNo = POOrder.TLCSVPO_PurchaseOrder;
                                    nr.TCustName = _Customers.FirstOrDefault(s => s.Cust_Pk == PurchaseOrder.TLCSVPO_Customer_FK).Cust_Description;
                                    nr.TRequiredDate = POOrder.TLCSVPO_RequiredDate;
                                    nr.TOrderDate = POOrder.TLCSVPO_TransDate;
                                    nr.TStatus = POOrder.TLCSVPO_Closeed;
                                }

                                if (!_Customers.FirstOrDefault(s => s.Cust_Pk == PurchaseOrder.TLCSVPO_Customer_FK).Cust_FabricCustomer) 
                                {
                                    nr.TStyle = _Styles.FirstOrDefault(s => s.Sty_Id == PODetail.TLCUSTO_Style_FK).Sty_Description;
                                    nr.TColour = _Colours.FirstOrDefault(s => s.Col_Id == PODetail.TLCUSTO_Colour_FK).Col_Display;
                                    var xSize = _Sizes.FirstOrDefault(s => s.SI_id == PODetail.TLCUSTO_Size_FK);
                                    if (xSize != null)
                                    {
                                        var Sty = _Styles.FirstOrDefault(s => s.Sty_Id == PODetail.TLCUSTO_Style_FK);
                                        if (Sty != null && !Sty.Sty_WorkWear)
                                        {
                                            nr.TSize = xSize.SI_Description;
                                        }
                                        else
                                        {
                                            nr.TSize = xSize.SI_ContiSize.ToString();
                                        }
                                        nr.TDisplayOrder = xSize.SI_DisplayOrder;
                                    }
                                    nr.TOrderQty = PODetail.TLCUSTO_Qty;
                                }
                                else
                                {
                                    nr.TStyle = _Qualities.FirstOrDefault(s => s.TLGreige_Id == PODetail.TLCUSTO_Quality_FK).TLGreige_Description;
                                    nr.TColour = _Colours.FirstOrDefault(s => s.Col_Id == PODetail.TLCUSTO_Colour_FK).Col_Display;
                                    nr.TSize = string.Empty;
                                    nr.TOrderQty = (int)PODetail.TLCUSTO_QtyMeters;
                                }
                            
                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                    }
                }

                DataSet23.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                hnr.Pk = 1;

                if (_QueryParms.TransactHistory)
                {
                    hnr.FromDate = _QueryParms.FromDate;
                    hnr.ToDate = _QueryParms.ToDate;

                    hnr.Title = "Customer Transactional History From";
                }
                else
                {
                    hnr.Title = "Customer Audit History";
                }

                dataTable2.AddDataTable2Row(hnr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CustHistoryTrans CustHistory = new CustHistoryTrans();
                CustHistory.SetDataSource(ds);

                crystalReportViewer1.ReportSource = CustHistory;
            }
            else if (_RepNo == 26)      // Customer Transactional History
            {
                DataSet ds = new DataSet();
                Util core = new Util();
                DataSet24.DataTable1DataTable dataTable1 = new DataSet24.DataTable1DataTable();

                IList<TLADM_Styles> _xStyles = null;
                IList<TLADM_Colours> _xColours = null;

                DateTime ToDay = DateTime.Now;

                DateTime FirstOfMonth = ToDay.AddDays(-1 * ToDay.Day + 1);

                DateTime StartDate = FirstOfMonth.AddMonths(-1 * 11);
                DateTime EndDate = DateTime.Now;

                int WorkingDays = core.GetWorkingDays(StartDate, EndDate);
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

                using (var context = new TTI2Entities())
                {
                    _xStyles = context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).ToList();
                    _xColours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).ToList();

                    foreach (var Style in _QueryParms.Styles)
                    {
                        foreach (var xColour in _QueryParms.Colours)
                        {
                            DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Style.Sty_Id).Sty_Description;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == xColour.Col_Id).Col_Display;

                            nr.Col1 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = nr.Col7 = nr.Col8 = nr.Col9 = nr.Col10 = nr.Col11 = 0;

                            var SalesBySize = context.TLCSV_StockOnHand.Where(x => x.TLSOH_SoldDate >= StartDate && x.TLSOH_SoldDate <= EndDate && x.TLSOH_Sold && x.TLSOH_Style_FK == Style.Sty_Id && x.TLSOH_Colour_FK == xColour.Col_Id).GroupBy(x => x.TLSOH_Size_FK);
                            foreach (var SizeGrp in SalesBySize)
                            {
                                var SizePk = SizeGrp.FirstOrDefault().TLSOH_Size_FK;
                                var Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk);
                                if (Size != null)
                                {
                                    var TotalSales = SizeGrp.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    decimal DaysSale = TotalSales / WorkingDays;

                                    int StockOnHand = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == Style.Sty_Id && x.TLSOH_Colour_FK == xColour.Col_Id && x.TLSOH_Size_FK == SizePk && !x.TLSOH_Sold && !x.TLSOH_Picked && !x.TLSOH_Write_Off && !x.TLSOH_Split && x.TLSOH_Grade == "A").Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    decimal AvailDaysStock = 0.0M;

                                    if (DaysSale != 0)
                                    {
                                        AvailDaysStock = Math.Round(StockOnHand / DaysSale, 1);
                                    }

                                    if (Size.SI_ColNumber == 1)
                                        nr.Col1 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 2)
                                        nr.Col2 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 3)
                                        nr.Col3 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 4)
                                        nr.Col4 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 5)
                                        nr.Col5 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 6)
                                        nr.Col6 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 7)
                                        nr.Col7 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 8)
                                        nr.Col8 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 9)
                                        nr.Col9 += AvailDaysStock;
                                    else if (Size.SI_ColNumber == 10)
                                        nr.Col10 += AvailDaysStock;
                                    else
                                        nr.Col11 += AvailDaysStock;
                                }
                            }
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                AvailableDaysStock DaysStock = new AvailableDaysStock();
                DaysStock.SetDataSource(ds);

                IEnumerator ie = DaysStock.Section2.ReportObjects.GetEnumerator();
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

                crystalReportViewer1.ReportSource = DaysStock;


            }
            else if (_RepNo == 27)      // Customer Negative Stock Report
            {
                DataSet ds = new DataSet();
                DataSet25.DataTable1DataTable dataTable1 = new DataSet25.DataTable1DataTable();
                Util core = new Util();
                Repository repo = new Repository();
                IList<TLCSV_StockOnHand> soh = null;

                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                 {   new string[] {"Text4", string.Empty},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty}

                 };

                var CNames = core.CreateColumnNames();

                int i = 0;
                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }
                //================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Customer", typeof(int));     // 0
                dt.Columns["Customer"].DefaultValue = 0;
                dt.Columns.Add("Style", typeof(int));        // 1
                dt.Columns["Style"].DefaultValue = 0;
                dt.Columns.Add("Colour", typeof(int));       // 2
                dt.Columns["Colour"].DefaultValue = 0;

                dt.Columns.Add("01", typeof(int));        // 3
                dt.Columns["01"].DefaultValue = 0;
                dt.Columns.Add("02", typeof(int));        // 4
                dt.Columns["02"].DefaultValue = 0;
                dt.Columns.Add("03", typeof(int));        // 5
                dt.Columns["03"].DefaultValue = 0;
                dt.Columns.Add("04", typeof(int));        // 6
                dt.Columns["04"].DefaultValue = 0;
                dt.Columns.Add("05", typeof(int));        // 7
                dt.Columns["05"].DefaultValue = 0;
                dt.Columns.Add("06", typeof(int));        // 8
                dt.Columns["06"].DefaultValue = 0;
                dt.Columns.Add("07", typeof(int));        // 9
                dt.Columns["07"].DefaultValue = 0;
                dt.Columns.Add("08", typeof(int));        // 10
                dt.Columns["08"].DefaultValue = 0;
                dt.Columns.Add("09", typeof(int));        // 11
                dt.Columns["09"].DefaultValue = 0;
                dt.Columns.Add("10", typeof(int));        // 12
                dt.Columns["10"].DefaultValue = 0;
                dt.Columns.Add("11", typeof(int));        // 13
                dt.Columns["11"].DefaultValue = 0;

                using (var context = new TTI2Entities())
                {
                    soh = repo.SOHOnHand(_QueryParms).ToList();
                    soh = soh.Where(x => x.TLSOH_Grade == "A").ToList();
                    var GroupedOutSOrders = repo.WholeSaleOutStandingOrders(_QueryParms).GroupBy(x => new { x.TLCUSTO_Customer_FK, x.TLCUSTO_Style_FK, x.TLCUSTO_Colour_FK });

                    foreach (var CustoGroup in GroupedOutSOrders)
                    {
                        var CustoFK = CustoGroup.FirstOrDefault().TLCUSTO_Customer_FK;
                        var StyleFK = CustoGroup.FirstOrDefault().TLCUSTO_Style_FK;
                        var ColourFK = CustoGroup.FirstOrDefault().TLCUSTO_Colour_FK;

                        DataRow dr = dt.NewRow();
                        var AddRow = false;

                        dr[0] = CustoFK;
                        dr[1] = StyleFK;
                        dr[2] = ColourFK;

                        if (CustoFK == 23 && StyleFK == 34 && ColourFK == 10)
                        {
                            int a = 0;
                        }
                        var SizeGroup = CustoGroup.GroupBy(x => x.TLCUSTO_Size_FK);
                        foreach (var SG in SizeGroup)
                        {
                            var SizeFK = SG.FirstOrDefault().TLCUSTO_Size_FK;
                            var SOH = soh.Where(x => x.TLSOH_Style_FK == StyleFK && x.TLSOH_Colour_FK == ColourFK && x.TLSOH_Size_FK == SizeFK).Count();

                            var SizeDetail = context.TLADM_Sizes.Find(SizeFK);
                            if (SizeDetail != null)
                            {
                                var ColPos = SizeDetail.SI_ColNumber.ToString().PadLeft(2, '0');

                                var Ind = dt.Columns.IndexOf(ColPos);
                                if (Ind > 0)
                                {
                                    var QtyPicked = SG.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate - x.TLCUSTO_QtyDelivered_ToDate) ?? 0;
                                    var QtyDlivered = SG.Sum(x => (int?)x.TLCUSTO_QtyDelivered_ToDate) ?? 0;
                                    var QtyOrdered = SG.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0;
                                    var QtyOutStanding = QtyOrdered - QtyPicked - QtyDlivered;

                                    var BoxedQty = soh.Where(x => x.TLSOH_Style_FK == StyleFK && x.TLSOH_Colour_FK == ColourFK & x.TLSOH_Size_FK == SizeFK).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    if (QtyOutStanding > 0 && BoxedQty < QtyOutStanding)
                                    {
                                        dr[Ind] = QtyOutStanding;
                                        AddRow = true;
                                    }
                                }
                            }
                        }


                        if (AddRow)
                            dt.Rows.Add(dr);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataSet25.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.CustomerName = _Customers.FirstOrDefault(s => s.Cust_Pk == (int)dr[0]).Cust_Description;
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == (int)dr[1]).Sty_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == (int)dr[2]).Col_Display;
                        nr.Col1 = (int)dr[3];
                        nr.Col2 = (int)dr[4];
                        nr.Col3 = (int)dr[5];
                        nr.Col4 = (int)dr[6];
                        nr.Col5 = (int)dr[7];
                        nr.Col6 = (int)dr[8];
                        nr.Col7 = (int)dr[9];
                        nr.Col8 = (int)dr[10];
                        nr.Col9 = (int)dr[11];
                        nr.Col10 = (int)dr[12];
                        nr.Col11 = (int)dr[13];

                        dataTable1.AddDataTable1Row(nr);

                    }


                    ds.Tables.Add(dataTable1);

                    NegativeOrder OnHand = new NegativeOrder();
                    IEnumerator ie = OnHand.Section2.ReportObjects.GetEnumerator();
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

                    OnHand.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = OnHand;
                }
            }
            else if (_RepNo == 28)      // Pastel Reconciliation Report 
            {
                DataSet ds = new DataSet();
                DataSet26.DataTable1DataTable dataTable1 = new DataSet26.DataTable1DataTable();
                DataSet26.DataTable2DataTable dataTable2 = new DataSet26.DataTable2DataTable();
                Util core = new Util();
                Repository repo = new Repository();
                // IList<TLCSV_StockOnHand> sohs = null;

                var sohs = repo.PastelRecon(_QueryParms).GroupBy(x => x.TLSOH_Style_FK).ToList();
                using (var context = new TTI2Entities())
                {
                    foreach (var soh in sohs)
                    {
                        DataSet26.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Key = 1;
                        var Style = soh.FirstOrDefault().TLSOH_Style_FK;
                        nr.Description = context.TLADM_Styles.Where(x => x.Sty_Id == Style).FirstOrDefault().Sty_Description;
                        nr.BoxedQty = soh.Sum(x => x.TLSOH_BoxedQty);


                        dataTable2.AddDataTable2Row(nr);

                    }
                }

                DataSet26.DataTable1Row hnr = dataTable1.NewDataTable1Row();
                hnr.Key = 1;
                hnr.FromDate = _QueryParms.FromDate;
                hnr.ToDate = _QueryParms.ToDate;
                dataTable1.AddDataTable1Row(hnr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                PastelRecon PRecon = new PastelRecon();
                PRecon.SetDataSource(ds);

                crystalReportViewer1.ReportSource = PRecon;

            }
            else if (_RepNo == 29)      // Bought in Goods Receipt Report 
            {
                DataSet ds = new DataSet();
                DataSet27.DataTable1DataTable dataTable1 = new DataSet27.DataTable1DataTable();
                DataSet27.DataTable2DataTable dataTable2 = new DataSet27.DataTable2DataTable();
                Util core = new Util();
                Repository repo = new Repository();
                using (var context = new TTI2Entities())
                {
                    DataSet27.DataTable1Row hnr = dataTable1.NewDataTable1Row();
                    hnr.Pk = 1;

                    var BoughtInGoods = context.TLCSV_BoughtInGoods.Find(_Pk);
                    if (BoughtInGoods != null)
                    {
                        hnr.TransDate = BoughtInGoods.TLBIG_TransDate;
                        hnr.ReceivingWareHouse = context.TLADM_WhseStore.Find(BoughtInGoods.TLBIG_WareHouse_FK).WhStore_Description;
                        hnr.Supplier = context.TLADM_Suppliers.Find(BoughtInGoods.TLBIG_Supplier_FK).Sup_Description;
                        hnr.TransNumber = BoughtInGoods.TLBIG_TransNumber;
                        hnr.TTIOrderNumber = BoughtInGoods.TLBIG_TTIOrderNo;
                        hnr.SupplierDelNumber = BoughtInGoods.TLBIG_SupplierDelNo;
                        
                        dataTable1.AddDataTable1Row(hnr);

                        var BoughtInDetails = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoughtInGoods_Fk == _Pk).GroupBy(x => new { x.TLSOH_Style_FK, x.TLSOH_Colour_FK, x.TLSOH_Size_FK });
                        foreach (var Sty in BoughtInDetails)
                        {
                            DataSet27.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.Pk = 1;
                            var FirstSty = Sty.FirstOrDefault().TLSOH_Style_FK;
                            nr.Style = context.TLADM_Styles.Find(FirstSty).Sty_Description;
                            var FirstClr = Sty.FirstOrDefault().TLSOH_Colour_FK;
                            nr.Colour = context.TLADM_Colours.Find(FirstClr).Col_Display;
                            var FirstSiz = Sty.FirstOrDefault().TLSOH_Size_FK;
                            nr.Size = context.TLADM_Sizes.Find(FirstSiz).SI_ContiSize.ToString();
                            nr.ReceivedQty = Sty.Count();

                            dataTable2.AddDataTable2Row(nr);
                        }
                    }


                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);

                    CustomerServices.ReceivedBoughtInGoods PRecon = new CustomerServices.ReceivedBoughtInGoods();
                    PRecon.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = PRecon;
                }
               
            }
            
            crystalReportViewer1.Refresh();
        }

        public struct WhseDATA
        {
            public int Whse_FK;            // Warehouse Foreign Key
            public int PickingListNo;      // LastPickingList
            public bool Selected;
            public WhseDATA(int WhseKey, int PLNumber, bool Sel)
            {
                this.Whse_FK = WhseKey;
                this.PickingListNo = PLNumber;
                this.Selected = Sel;
            }
        }

        public class SOHDetails
        {
            public int Whse { get; set; }
            public int Style { get; set; }
            public int Colour { get; set; }
            public int Size { get; set; }

            public int BoxedQty { get; set; }

        }

        public class WhseDetail
        {
            public string WareHouseDescrip { get; set; }
            public int WareHousePk { get; set; }
            public int WareHouseColNo { get; set; }
        }
    }
}
