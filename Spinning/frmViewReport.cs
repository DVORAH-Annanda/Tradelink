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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;

namespace Spinning
{
    public partial class frmViewReport : Form
    {
      
        //-------------------------------------------------------------------------
                   
        int _RepNo;
        int _LotNo;
        DateTime _Dateselected;
        int _FromPallet;
        int _ToPallet;
        DataGridView _oDgv;
        Utilities.ReportOptions _RepOpt;
        QAYarnReportOptions _YarnRepOpt;
        StockStatusReportOptions _StockStatus;
        NSISelection _nsiSelection;
        YarnProductionSel _ypSelection;
        YarnStockOHSel _yarnSOHSel;
        WasteSelection _wasteSel;
        YarnTOptions _YarnTOpt;
        SpinningQueryParameters _QueryParms;
        SpinningRepository _Repo;
        SliverProductionSelection _sliverProductionSelection;


        public frmViewReport(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmViewReport(int RepNo, int LotNo)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _LotNo = LotNo;
        }

        public frmViewReport(int RepNo, DateTime Dateselected)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _Dateselected = Dateselected;
        }

        public frmViewReport(int RepNo, SpinningQueryParameters QParms, QAYarnReportOptions YarnOpts)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _QueryParms = QParms;
            _YarnRepOpt = YarnOpts;

        }

        public frmViewReport(int RepNo, SpinningQueryParameters QParms)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _QueryParms = QParms;

        }

        public frmViewReport(int RepNo, Utilities.ReportOptions repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _RepOpt = repOpt;
            
        }

        public frmViewReport(int RepNo, SliverProductionSelection repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _sliverProductionSelection = repOpt;
        }

        public frmViewReport(int RepNo, WasteSelection repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _wasteSel = repOpt;

        }

        public frmViewReport(int RepNo,  YarnTOptions repOpt)
        {
            InitializeComponent();

            _RepNo    = RepNo;
            _YarnTOpt = repOpt;

        }

        public frmViewReport(int RepNo, YarnStockOHSel repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _yarnSOHSel = repOpt;

        }

        public frmViewReport(int RepNo, YarnProductionSel repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _ypSelection = repOpt;

        }

        public frmViewReport(int RepNo, QAYarnReportOptions repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _YarnRepOpt = repOpt;

        }

        public frmViewReport(int RepNo,   NSISelection repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _nsiSelection = repOpt;

        }

        public frmViewReport(int RepNo, StockStatusReportOptions repOpt)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _StockStatus = repOpt;
        }

        public frmViewReport(int RepNo, int LotNo, int FromPallet, int ToPallet)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _FromPallet = FromPallet;
            _ToPallet = ToPallet;
            _LotNo = LotNo;

        }

        public frmViewReport(int RepNo, int LotNo, DataGridView oDgv)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _LotNo = LotNo;
            _oDgv = oDgv;

        }



        private void frmReportView_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1) // Blow Room Laydown 
            {
                DataSet ds = new DataSet();
                BlowRoom rep1;
                DataSet1.TLSPN_CottonReceivedBalesDataTable CottonReceived;

                CottonReceived = new DataSet1.TLSPN_CottonReceivedBalesDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_CottonReceivedBales.Where(x => x.CoBales_BlowRoomPosition != 0).OrderBy(x => x.CoBales_BlowRoomPosition).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet1.TLSPN_CottonReceivedBalesRow nRow = CottonReceived.NewTLSPN_CottonReceivedBalesRow();
                        nRow.CoBales_BlowRoomPosition = row.CoBales_BlowRoomPosition;
                        nRow.CoBales_CottonReturned = row.CoBales_CottonReturned;
                        nRow.CoBales_CottonSequence = row.CoBales_CottonSequence;
                        nRow.CoBales_CottonSold = row.CoBales_CottonSold;
                        nRow.CoBales_IssuedToProd = row.CoBales_IssuedToProd;
                        nRow.CotBales_BaleNo = row.CotBales_BaleNo;
                        nRow.CotBales_LotNo = row.CotBales_LotNo;
                        nRow.CotBales_Mic = row.CotBales_Mic;
                        nRow.CotBales_Pk = row.CotBales_Pk;
                        nRow.CotBales_Staple = row.CotBales_Staple;
                        nRow.CotBales_Weight_Nett = row.CotBales_Weight_Nett;
                        try
                        {
                            CottonReceived.AddTLSPN_CottonReceivedBalesRow(nRow);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    rep1 = new BlowRoom();
                    ds.Tables.Add(CottonReceived);
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                    crystalReportViewer1.Refresh();
                }
            }
            else if (_RepNo == 2) // Cotton Received 
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable datatable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable datatable2 = new DataSet13.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Return_No == _LotNo).FirstOrDefault();
                    if (Existing != null)
                    {
                        DataSet13.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.key = 1;
                        nr.GrossWeight = Existing.cotrx_GrossWeight;
                        nr.LotNo = Existing.cotrx_LotNo;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.NettWeight = Existing.cotrx_NetWeight;
                        nr.NoOfBales = (int)Existing.cotrx_NoBales;
                        nr.WBEmpty = Existing.cotrx_WeighBridgeEmpty;
                        nr.WBFull = Existing.cotrx_WeighBridgeFull;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.AveBaleWeight = Existing.cottrx_NettAveBaleWeight;
                        var conDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == Existing.cotrx_ContractNo_Fk).FirstOrDefault();
                        if (conDetails != null)
                            nr.ContractNo = conDetails.CottonCon_No;

                        datatable1.AddDataTable1Row(nr);

                        DataSet13.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.key = 1;
                        hnr.Date = Existing.cotrx_TransDate;
                        hnr.GrnNo = Existing.cotrx_Return_No;
                        var Supplier = context.TLADM_Cotton.Where(X => X.Cotton_Pk == Existing.cotrx_Supplier_FK).FirstOrDefault();
                        if (Supplier != null)
                            hnr.Supplier = Supplier.Cotton_Description;

                        var Haulier = context.TLADM_CottonHauliers.Where(x => x.Haul_Pk == Existing.cotrx_Haulier_FK).FirstOrDefault();
                        if (Haulier != null)
                            hnr.Transporter = Haulier.Haul_Description;

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("SPIN")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 100).FirstOrDefault();
                            if (TranType != null)
                            {
                                var whse = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK);
                                if (whse != null)
                                    hnr.StoreDetails = whse.WhStore_Description;
                            }
                        }
                        hnr.VehReg = Existing.cotrx_VehReg;

                        datatable2.AddDataTable2Row(hnr);
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);

                CottonReceived rep2 = new CottonReceived();
                rep2.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep2;

            }
            else if (_RepNo == 3) // Cotton Returns
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable datatable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable datatable2 = new DataSet13.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Return_No == _LotNo).FirstOrDefault();
                    if (Existing != null)
                    {
                        DataSet13.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.key = 1;
                        nr.GrossWeight = Existing.cotrx_GrossWeight;
                        nr.LotNo = Existing.cotrx_LotNo;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.NettWeight = Existing.cotrx_NetWeight;
                        nr.NoOfBales = (int)Existing.cotrx_NoBales;
                        nr.WBEmpty = Existing.cotrx_WeighBridgeEmpty;
                        nr.WBFull = Existing.cotrx_WeighBridgeFull;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.AveBaleWeight = Existing.cottrx_NettAveBaleWeight;
                        var conDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == Existing.cotrx_ContractNo_Fk).FirstOrDefault();
                        if (conDetails != null)
                            nr.ContractNo = conDetails.CottonCon_No;

                        datatable1.AddDataTable1Row(nr);

                        DataSet13.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.key = 1;
                        hnr.Date = Existing.cotrx_TransDate;
                        hnr.GrnNo = Existing.cotrx_Return_No;
                        var Supplier = context.TLADM_Cotton.Where(X => X.Cotton_Pk == Existing.cotrx_Supplier_FK).FirstOrDefault();
                        if (Supplier != null)
                            hnr.Supplier = Supplier.Cotton_Description;

                        var Haulier = context.TLADM_CottonHauliers.Where(x => x.Haul_Pk == Existing.cotrx_Haulier_FK).FirstOrDefault();
                        if (Haulier != null)
                            hnr.Transporter = Haulier.Haul_Description;

                        hnr.VehReg = Existing.cotrx_VehReg;

                        datatable2.AddDataTable2Row(hnr);
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);

                CottonReturned rep3 = new CottonReturned();
                rep3.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep3;
            }
            else if (_RepNo == 4) // Cotton Adjustments
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable datatable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable datatable2 = new DataSet13.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Return_No == _LotNo).FirstOrDefault();
                    if (Existing != null)
                    {
                        DataSet13.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.key = 1;
                        nr.GrossWeight = Existing.cotrx_GrossWeight;
                        nr.LotNo = Existing.cotrx_LotNo;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.NettWeight = Existing.cotrx_NetWeight;
                        nr.NoOfBales = (int)Existing.cotrx_NoBales;
                        nr.WBEmpty = Existing.cotrx_WeighBridgeEmpty;
                        nr.WBFull = Existing.cotrx_WeighBridgeFull;
                        nr.NettPerWB = Existing.cotrx_NettPerWB;
                        nr.AveBaleWeight = Existing.cottrx_NettAveBaleWeight;
                        var conDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == Existing.cotrx_ContractNo_Fk).FirstOrDefault();
                        if (conDetails != null)
                            nr.ContractNo = conDetails.CottonCon_No;

                        nr.WriteOff = Existing.cotrx_WriteOff;

                        datatable1.AddDataTable1Row(nr);

                        DataSet13.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.key = 1;
                        hnr.Date = Existing.cotrx_TransDate;
                        hnr.GrnNo = Existing.cotrx_Return_No;
                        var Supplier = context.TLADM_Cotton.Where(X => X.Cotton_Pk == Existing.cotrx_Supplier_FK).FirstOrDefault();
                        if (Supplier != null)
                            hnr.Supplier = Supplier.Cotton_Description;

                        var Haulier = context.TLADM_CottonHauliers.Where(x => x.Haul_Pk == Existing.cotrx_Haulier_FK).FirstOrDefault();
                        if (Haulier != null)
                            hnr.Transporter = Haulier.Haul_Description;

                        hnr.VehReg = Existing.cotrx_VehReg;

                        datatable2.AddDataTable2Row(hnr);
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                CottonAjustments rep4 = new CottonAjustments();
                rep4.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep4;
            }
            else if (_RepNo == 5) // Cotton Sales Report 
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable datatable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable datatable2 = new DataSet13.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Return_No == _LotNo).FirstOrDefault();
                    if (Existing != null)
                    {
                        var BaleStore = context.TLSPN_CottonReceivedBales.Where(x => x.CoBales_CottonSold && x.CoBales_CottonSequence == _LotNo).ToList();
                        foreach (var Bale in BaleStore)
                        {
                            DataSet13.DataTable1Row nr = datatable1.NewDataTable1Row();
                            nr.key = 1;
                            nr.GrossWeight = Bale.CotBales_Weight_Gross;
                            nr.LotNo = Existing.cotrx_LotNo;
                            nr.NettPerWB = Existing.cotrx_NettPerWB;
                            nr.NettWeight = Bale.CotBales_Weight_Nett;
                            nr.NoOfBales = 1;
                            nr.WBEmpty = Existing.cotrx_WeighBridgeEmpty;
                            nr.WBFull = Existing.cotrx_WeighBridgeFull;
                            nr.NettPerWB = Existing.cotrx_NettPerWB;
                            nr.AveBaleWeight = Existing.cottrx_NettAveBaleWeight;
                            nr.BaleNo = Bale.CotBales_BaleNo;

                            var conDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == Existing.cotrx_ContractNo_Fk).FirstOrDefault();
                            if (conDetails != null)
                                nr.ContractNo = conDetails.CottonCon_No;

                            datatable1.AddDataTable1Row(nr);
                        }

                        DataSet13.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.key = 1;
                        hnr.Date = Existing.cotrx_TransDate;
                        hnr.GrnNo = Existing.cotrx_Return_No;
                        var Customer = context.TLADM_CustomerFile.Where(X => X.Cust_Pk == Existing.cotrx_Customer_FK).FirstOrDefault();
                        if (Customer != null)
                        {
                            hnr.ReportTitle = Existing.cotrx_Notes;
                            hnr.Supplier = Customer.Cust_Description;
                            hnr.AddressDetail = Customer.Cust_Address1;
                        }
                        var Haulier = context.TLADM_CottonHauliers.Where(x => x.Haul_Pk == Existing.cotrx_Haulier_FK).FirstOrDefault();
                        if (Haulier != null)
                            hnr.Transporter = Haulier.Haul_Description;

                        hnr.VehReg = Existing.cotrx_VehReg;

                        datatable2.AddDataTable2Row(hnr);
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);

                CottonGoodsSold rep5 = new CottonGoodsSold();
                rep5.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep5;
            }
            else if (_RepNo == 6)
            {

                DataSet1 ds1 = new DataSet1();
                DataSet2.DataTable1DataTable SummaryTable;
                TLADM_TranactionType trantypes = new TLADM_TranactionType();


                SummaryTable = new DataSet2.DataTable1DataTable();
                IList<TLADM_Cotton> Cotton = new List<TLADM_Cotton>();
                IList<TLADM_CottonContracts> CottonContract = new List<TLADM_CottonContracts>();
                IList<TLSPN_CottonTransactions> CottonTransctions = new List<TLSPN_CottonTransactions>();

                using (var context = new TTI2Entities())
                {
                    //We need the department number 
                    //-------------------------------------------
                    var DeptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (_RepOpt.SelectionOption == 1)
                    {
                        //this return a particular supplier
                        Cotton = context.TLADM_Cotton.Where(x => x.Cotton_Pk == _RepOpt.Supplier_FK).ToList();
                    }
                    else if (_RepOpt.SelectionOption == 2)
                    {
                        //this returns all the suppliers
                        Cotton = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                    }
                    else if (_RepOpt.SelectionOption == 3)
                    {
                        //this returns contracts starting after a particular day
                        //We need to start withh all all the suppliers
                        Cotton = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();

                    }
                    else if (_RepOpt.SelectionOption == 4)
                    {
                        //this returns contracts for which there is still stock held in the bale store
                        //We need to start withh all all the suppliers
                        Cotton = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();

                    }
                    foreach (var Row in Cotton)
                    {
                        //now get all the contracts for each supplier 
                        //-------------------------------------------------------
                        if (_RepOpt.SelectionOption != 3)
                        {
                            CottonContract = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == Row.Cotton_Pk).ToList();
                        }
                        else
                        {
                            CottonContract = context.TLADM_CottonContracts.Where(x => x.CottonCon_StartDate == _RepOpt.DateSelected && x.CottonCon_ConSupplier_FK == Row.Cotton_Pk).ToList();
                        }
                        //------------------------------------------------------------------
                        foreach (var ccRow in CottonContract)
                        {
                            DataSet2.DataTable1Row nr = SummaryTable.NewDataTable1Row();
                            nr.ContractNo = ccRow.CottonCon_No;
                            nr.Supplier = Row.Cotton_Description;
                            nr.ContractedKG = ccRow.CottonCon_Mass;

                            //received to date 
                            //-----------------------------------------------
                            CottonTransctions = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Supplier_FK == Row.Cotton_Pk
                                                                                  && x.cotrx_ContractNo_Fk == ccRow.CottonCon_Pk).ToList();

                            if (CottonTransctions != null)
                            {
                                foreach (var trxRow in CottonTransctions)
                                {
                                    if (_RepOpt.SelectionOption == 4)
                                    {
                                        var cnt = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == trxRow.cotrx_LotNo &&
                                                                                              !x.CoBales_IssuedToProd &&
                                                                                              !x.CoBales_CottonReturned &&
                                                                                              !x.CoBales_CottonSold).Count();
                                        //if (cnt == 0)
                                        //  continue;
                                    }

                                    var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Pk == trxRow.cotrx_TranType && x.TrxT_Department_FK == DeptDetail.Dep_Id).FirstOrDefault();
                                    if (TranType.TrxT_Number == 100)
                                    {
                                        try
                                        {
                                            nr.ReceivedToDate += trxRow.cotrx_NetWeight;
                                            nr.BalesInStore += (int)trxRow.cotrx_NoBales;
                                            nr.BalInStore += trxRow.cotrx_NetWeight;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                    else if (TranType.TrxT_Number == 200)
                                    {
                                        nr.BalesInStore -= (int)trxRow.cotrx_NoBales;
                                        nr.BalInStore -= trxRow.cotrx_NetWeight;
                                        nr.ReceivedToDate -= trxRow.cotrx_NetWeight;
                                    }
                                    else if (TranType.TrxT_Number == 300)
                                    {
                                        nr.BalesInStore -= (int)trxRow.cotrx_NoBales;
                                        nr.BalInStore -= trxRow.cotrx_NetWeight;
                                    }
                                    else if (TranType.TrxT_Number == 400)
                                    {
                                        if (trxRow.cotrx_WriteOff)
                                        {
                                            nr.BalesInStore -= (int)trxRow.cotrx_NoBales;
                                            nr.BalInStore -= trxRow.cotrx_NetWeight;
                                        }
                                        else
                                        {
                                            nr.BalesInStore += (int)trxRow.cotrx_NoBales;
                                            nr.BalInStore += trxRow.cotrx_NetWeight;
                                        }
                                    }
                                    else if (TranType.TrxT_Number == 500)
                                    {
                                        nr.BalesInStore -= (int)trxRow.cotrx_NoBales;
                                        nr.BalInStore -= trxRow.cotrx_NetWeight;
                                        nr.LayDowns2Date += (int)trxRow.cotrx_NetWeight;
                                    }
                                }
                            }
                            else
                            {
                                nr.ReceivedToDate = 0M;
                            }

                            if (_RepOpt.SelectionOption == 1)
                            {
                                nr.ReportTitle = "Cotton Contract Summary Detail for " + _RepOpt.SupplierDetail;
                            }
                            else if (_RepOpt.SelectionOption == 2)
                            {
                                nr.ReportTitle = "Cotton Contract Summary Details for all Suppliers";
                            }
                            else if (_RepOpt.SelectionOption == 3)
                            {
                                nr.ReportTitle = "Cotton Contract Summary Details for all contracts that commenced on " + _RepOpt.DateSelected.ToString();
                            }
                            else if (_RepOpt.SelectionOption == 4)
                            {
                                nr.ReportTitle = "Contracts for which stock still held in bale store";
                            }
                            SummaryTable.AddDataTable1Row(nr);

                        }
                    }
                }


                ds1.Tables.Add(SummaryTable);
                ConttonContractSummary rep1 = new ConttonContractSummary();
                rep1.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = rep1;

            }
            else if (_RepNo == 7)
            {
                //---------------------------------------------
                // Raw Cotton Stock
                //----------------------------------------------------

                DataSet1 ds1 = new DataSet1();
                DataSet3.DataTable1DataTable SummaryTable = new DataSet3.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    //this returns all the suppliers
                    try
                    {
                        var Cotton = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                        foreach (var Row in Cotton)
                        {
                            //now get all the contracts for each supplier 
                            //-------------------------------------------------
                            var cottonContract = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == Row.Cotton_Pk && !x.CottonCon_Closed);
                            foreach (var ccRow in cottonContract)
                            {
                                DataSet3.DataTable1Row nr = SummaryTable.NewDataTable1Row();
                                nr.ContractNo = ccRow.CottonCon_No;
                                nr.Supplier = Row.Cotton_Description;
                                nr.StartDate = ccRow.CottonCon_StartDate;
                                nr.EndDate = ccRow.CottonCon_EndDate;
                                nr.ContractedNoOfBales = ccRow.CottonCon_NoOfBales;
                                nr.ContractedGrossWeight = 0M;
                                nr.ContractedNetWeight = ccRow.CottonCon_Mass;

                                var DeptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                                //received to date 
                                var Trx = context.TLSPN_CottonTransactions.Where(x => x.cotrx_Supplier_FK == Row.Cotton_Pk
                                                                                  && x.cotrx_ContractNo_Fk == ccRow.CottonCon_Pk).ToList();
                                if (Trx != null)
                                {
                                    foreach (var trxRow in Trx)
                                    {
                                        var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Pk == trxRow.cotrx_TranType && x.TrxT_Department_FK == DeptDetail.Dep_Id).FirstOrDefault();
                                        if (TranType.TrxT_Number == 100)
                                        {
                                            try
                                            {
                                                nr.DeliveredNettWeight += trxRow.cotrx_NetWeight;
                                                nr.DeliveredGrossWeight += trxRow.cotrx_GrossWeight;
                                                nr.DeliveredNoOfBales += (int)trxRow.cotrx_NoBales;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                        else if (TranType.TrxT_Number == 200)
                                        {
                                            nr.DeliveredNoOfBales -= (int)trxRow.cotrx_NoBales;
                                            nr.DeliveredGrossWeight -= trxRow.cotrx_GrossWeight;
                                            nr.DeliveredNettWeight -= trxRow.cotrx_NetWeight;
                                            ;
                                        }

                                    }
                                }
                                SummaryTable.AddDataTable1Row(nr);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);

                        MessageBox.Show(exceptionMessages.ToString());

                    }
                }


                ds1.Tables.Add(SummaryTable);
                RawCottonStock rep1 = new RawCottonStock();
                rep1.SetDataSource(ds1);

                crystalReportViewer1.ReportSource = rep1;

            }
            else if (_RepNo == 8)
            {
                //---------------------------------------
                // Raw Cotton Movement report
                //-----------------------------------------
                int OpenBales = 0;

                decimal OpenKG = 0M;
                decimal OpenAvgBaleweight = 0M;
                int MonthEndDay;

                int CloseBales = 0;
                decimal CloseKG = 0M;
                decimal CloseAvgBaleweight = 0M;

                Util core = new Util();

                IList<TLSPN_CottonTransactions> OpeningBalances = null;

                DataSet1 ds1 = new DataSet1();
                DataSet4.DataTable1DataTable openBalTable = new DataSet4.DataTable1DataTable();
                DataSet4.DataTable2DataTable receiptsTable = new DataSet4.DataTable2DataTable();
                DataSet4.DataTable3DataTable closebalTable = new DataSet4.DataTable3DataTable();
                DataSet4.DataTable4DataTable transTable = new DataSet4.DataTable4DataTable();
                DataSet4.DataTable5DataTable layDownTable = new DataSet4.DataTable5DataTable();
                using (var context = new TTI2Entities())
                {
                    //need to calculate the opening balance;
                    // 1st the Month based on the selected dates
                    //-------------------------------------------------------
                    var firstDayOfMonth = new DateTime(_Dateselected.Year, _Dateselected.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    MonthEndDay = core.LastDayOfMonth(_Dateselected.Month);

                    if (MonthEndDay > 0)
                    {
                        //------------------------------------------------------------
                        // Need to determine the opening balances prior to the day selected
                        // by selecting all transactions in the file 
                        //--------------------------------------------------
                        OpeningBalances = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TransDate < firstDayOfMonth).ToList(); ;

                        OpenBales = OpeningBalances.Where(x => x.cotrx_TranType == 1).Sum(x => (int?)x.cotrx_NoBales) ?? 0;
                        OpenKG = OpeningBalances.Where(x => x.cotrx_TranType == 1).Sum(x => (decimal?)x.cotrx_NetWeight) ?? 0.00M;
                        if (OpeningBalances.Count > 0)
                            OpenAvgBaleweight = Math.Round(OpenKG / OpenBales, 1);

                        //------------------------------------------------------------
                        // Now filter out transactions that have diminshed stock
                        // by selecting all transactions in the file 
                        //--------------------------------------------------
                        OpenBales -= OpeningBalances.Where(x => x.cotrx_TranType != 1).Sum(x => (int?)x.cotrx_NoBales) ?? 0;
                        OpenKG -= OpeningBalances.Where(x => x.cotrx_TranType != 1).Sum(x => (decimal?)x.cotrx_NetWeight) ?? 0.00M;

                        CloseBales = OpenBales;
                        CloseKG = OpenKG;
                        CloseAvgBaleweight = OpenAvgBaleweight;

                        //---------------------------------------------------------------
                        DataSet4.DataTable1Row nr = openBalTable.NewDataTable1Row();
                        nr.Bales = OpenBales;
                        nr.KG = OpenKG;
                        nr.Average = OpenAvgBaleweight;
                        nr.MonthPeriod = _Dateselected.Month;
                        nr.Description = "Stock on Hand as at the : " + firstDayOfMonth.AddDays(-1).ToString("dd/MM/yyy"); ;
                        nr.Period = firstDayOfMonth.ToString("dd/MM/yyyy") + " to " + lastDayOfMonth.ToString("dd/MM/yyyy");
                        try
                        {
                            openBalTable.AddDataTable1Row(nr);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        //--------------------------------------------------------------
                        // this months transactions starting with receipts
                        //------------------------------------------------------------------
                        var ThisMonth = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TransDate >= firstDayOfMonth
                               && x.cotrx_TransDate <= lastDayOfMonth && x.cotrx_TranType == 1).OrderBy(x => x.cotrx_LotNo).GroupBy(x => x.cotrx_Supplier_FK).ToList();
                        //-----------------------------------------------------------------------------------------
                        DataSet4.DataTable2Row ntr = receiptsTable.NewDataTable2Row();

                        foreach (var grp in ThisMonth)
                        {
                            foreach (var line in grp)
                            {
                                ntr = receiptsTable.NewDataTable2Row();
                                ntr.MonthPeriod = _Dateselected.Month;
                                ntr.GRN = line.cotrx_Return_No.ToString();
                                ntr.KG = (decimal)line.cotrx_NetWeight;
                                ntr.LotNo = line.cotrx_LotNo;
                                ntr.Bales = (int)line.cotrx_NoBales;
                                if (line.cotrx_WriteOff)
                                {
                                    ntr.KG = -1 * ntr.KG;
                                    ntr.Bales = -1 * ntr.Bales;

                                }
                                ntr.AVG = line.cottrx_NettAveBaleWeight;

                                CloseKG += ntr.KG;
                                CloseBales += ntr.Bales;
                                CloseAvgBaleweight = ntr.AVG;

                                var TranDetails = context.TLADM_TranactionType.Where(x => x.TrxT_Pk == line.cotrx_TranType).FirstOrDefault();
                                if (TranDetails != null)
                                {
                                    ntr.TransDescription = TranDetails.TrxT_Description;
                                }

                                var ContractDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == line.cotrx_ContractNo_Fk).FirstOrDefault();
                                if (ContractDetails != null)
                                {
                                    ntr.ContractNo = ContractDetails.CottonCon_No;
                                }

                                var supDetails = context.TLADM_Cotton.Where(x => x.Cotton_Pk == line.cotrx_Supplier_FK).FirstOrDefault();
                                if (supDetails != null)
                                {
                                    ntr.Supplier = supDetails.Cotton_Description;
                                }
                                try
                                {
                                    receiptsTable.AddDataTable2Row(ntr);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                        }

                        DataSet4.DataTable4Row yntr = transTable.NewDataTable4Row();
                        yntr.MonthPeriod = _Dateselected.Month;
                        transTable.AddDataTable4Row(yntr);

                        DataSet4.DataTable5Row zhr = layDownTable.NewDataTable5Row();
                        zhr.MonthPeriod = _Dateselected.Month;
                        layDownTable.AddDataTable5Row(zhr);

                        //--------------------------------------------------------------
                        // end of this months receipts
                        //-------------------------------------------------------------------
                        //--------------------------------------------------------------
                        // this months transactions
                        //------------------------------------------------------------------

                        ThisMonth = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TransDate >= firstDayOfMonth
                               && x.cotrx_TransDate <= lastDayOfMonth && x.cotrx_TranType != 1 && x.cotrx_TranType != 5).GroupBy(x => x.cotrx_Supplier_FK).ToList();

                        foreach (var grp in ThisMonth)
                        {
                            foreach (var line in grp)
                            {

                                DataSet4.DataTable4Row xyntr = transTable.NewDataTable4Row();
                                yntr.MonthPeriod = _Dateselected.Month;
                                yntr.GRN = line.cotrx_Return_No.ToString();
                                yntr.KG = (decimal)line.cotrx_NetWeight;
                                yntr.LotNo = line.cotrx_LotNo;
                                yntr.Bales = (int)line.cotrx_NoBales;
                                if (line.cotrx_WriteOff)
                                {
                                    yntr.KG = -1 * yntr.KG;
                                    yntr.Bales = -1 * yntr.Bales;

                                }
                                yntr.AVG = line.cottrx_NettAveBaleWeight;

                                CloseKG += yntr.KG;
                                CloseBales += yntr.Bales;
                                CloseAvgBaleweight = yntr.AVG;

                                var TranDetails = context.TLADM_TranactionType.Where(x => x.TrxT_Pk == line.cotrx_TranType).FirstOrDefault();
                                if (TranDetails != null)
                                {
                                    yntr.TransDescription = TranDetails.TrxT_Description;
                                }

                                var ContractDetails = context.TLADM_CottonContracts.Where(x => x.CottonCon_Pk == line.cotrx_ContractNo_Fk).FirstOrDefault();
                                if (ContractDetails != null)
                                {
                                    yntr.ContractNo = ContractDetails.CottonCon_No;
                                }

                                var supDetails = context.TLADM_Cotton.Where(x => x.Cotton_Pk == line.cotrx_Supplier_FK).FirstOrDefault();
                                if (supDetails != null)
                                {
                                    yntr.Supplier = supDetails.Cotton_Description;
                                }
                                try
                                {
                                    transTable.AddDataTable4Row(xyntr);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                            CloseAvgBaleweight = grp.Average(x => x.cotrx_NettPerWB);
                        }


                        //--------------------------------------------------------------
                        // end of this months transaactions
                        //-------------------------------------------------------------------

                        //--------------------------------------------------------------
                        // this months LayDowns
                        //------------------------------------------------------------------
                        var ThisMonthLayDowns = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TransDate >= firstDayOfMonth
                              && x.cotrx_TransDate <= lastDayOfMonth && x.cotrx_TranType == 5);

                        if (ThisMonthLayDowns.Count() == 0)
                        {
                            DataSet4.DataTable5Row xzhr = layDownTable.NewDataTable5Row();
                            layDownTable.AddDataTable5Row(xzhr);

                        }
                        else
                        {
                            DataSet4.DataTable5Row yzhr = layDownTable.NewDataTable5Row();
                            zhr.MonthPeriod = _Dateselected.Month;
                            zhr.KG = ThisMonthLayDowns.Sum(x => x.cotrx_NetWeight);
                            zhr.Bales = (int)ThisMonthLayDowns.Sum(x => x.cotrx_NoBales);
                            zhr.AVG = ThisMonthLayDowns.Average(x => x.cottrx_NettAveBaleWeight);


                            layDownTable.AddDataTable5Row(yzhr);
                            CloseKG -= ThisMonthLayDowns.Sum(x => x.cotrx_NetWeight);
                            CloseBales -= (int)ThisMonthLayDowns.Sum(x => x.cotrx_NoBales);
                            CloseAvgBaleweight = ThisMonthLayDowns.Average(x => x.cottrx_NettAveBaleWeight);
                        }


                        //--------------------------------------------------------------
                        // closing balalnce
                        //-------------------------------------------------------------------
                        DataSet4.DataTable3Row xnr = closebalTable.NewDataTable3Row();
                        xnr.MonthPeriod = _Dateselected.Month;
                        xnr.Bales = CloseBales;
                        xnr.KG = CloseKG;
                        xnr.Average = CloseAvgBaleweight;
                        xnr.MonthPeriod = _Dateselected.Month;
                        xnr.Description = "Closing Balances";
                        closebalTable.AddDataTable3Row(xnr);
                    }
                }

                ds1.Tables.Add(openBalTable);
                ds1.Tables.Add(receiptsTable);
                ds1.Tables.Add(closebalTable);
                ds1.Tables.Add(transTable);
                ds1.Tables.Add(layDownTable);
                RawCottonMovement CottonMove = new RawCottonMovement();
                CottonMove.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = CottonMove;

            }
            else if (_RepNo == 9)
            {
                DataSet1 ds1 = new DataSet1();
                DataSet5.DataTable1DataTable datatable = new DataSet5.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {

                    var yarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_OrderNumber == _LotNo).FirstOrDefault();
                    if (yarnOrder != null)
                    {
                        DataSet5.DataTable1Row nr = datatable.NewDataTable1Row();
                        nr.OrderNo = yarnOrder.YarnO_OrderNumber;
                        nr.DelDate = yarnOrder.YarnO_DelDate;
                        nr.OrderWeight = yarnOrder.Yarno_OrderWeight;
                        nr.OrderDate = yarnOrder.YarnO_Date;

                        var machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == yarnOrder.Yarno_MachNo_FK).FirstOrDefault();
                        if (machines != null)
                            nr.Machine = machines.MD_MachineCode;

                        var pallet = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yarnOrder.YarnO_Pk).FirstOrDefault();
                        if (pallet != null)
                        {
                            nr.PalletNo = pallet.YarnOP_PalletNo;
                            nr.NoOfCones = pallet.YarnOP_NoOfCones;
                        }

                        var Yarn = context.TLADM_Yarn.Where(x => x.YA_Id == yarnOrder.Yarno_YarnType_FK).FirstOrDefault();
                        if (Yarn != null)
                        {
                            nr.YarnType = Yarn.YA_YarnType;
                            nr.YarnCount = Yarn.YA_TexCount;
                            nr.Identification = Yarn.YA_ConeColour;
                            nr.Twist = Yarn.YA_Twist;

                            var Origin = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Pk == Yarn.YA_CottonOrigin_FK).FirstOrDefault();
                            if (Origin != null)
                            {
                                nr.Type = Origin.CottonOrigin_Description;
                            }
                        }

                        datatable.AddDataTable1Row(nr); ;
                    }

                }
                ds1.Tables.Add(datatable);
                YarnOrder yo = new YarnOrder();

                yo.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = yo;
            }

            else if (_RepNo == 10)
            {
                DataSet1 ds1 = new DataSet1();
                DataSet5.DataTable1DataTable datatable = new DataSet5.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {


                    var yarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == _LotNo).FirstOrDefault();
                    if (yarnOrder != null)
                    {
                        var machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == yarnOrder.Yarno_MachNo_FK).FirstOrDefault();
                        var Yarn = context.TLADM_Yarn.Where(x => x.YA_Id == yarnOrder.Yarno_YarnType_FK).FirstOrDefault();

                        var pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == yarnOrder.YarnO_Pk).ToList();
                        foreach (var row in pallets)
                        {
                            if (row.YarnOP_PalletNo < _FromPallet || row.YarnOP_PalletNo > _ToPallet)
                                continue;

                            DataSet5.DataTable1Row nr = datatable.NewDataTable1Row();
                            nr.OrderNo = yarnOrder.YarnO_OrderNumber;
                            nr.PalletNo = row.YarnOP_PalletNo;
                            nr.NoOfCones = row.YarnOP_NoOfCones;

                            if (machines != null)
                                nr.Machine = machines.MD_MachineCode;

                            if (Yarn != null)
                            {
                                nr.YarnType = Yarn.YA_YarnType;
                                nr.YarnCount = Yarn.YA_TexCount;
                                nr.Identification = Yarn.YA_ConeColour;
                                nr.Twist = Yarn.YA_Twist;

                                var Origin = context.TLADM_CottonOrigin.Where(x => x.CottonOrigin_Pk == Yarn.YA_CottonOrigin_FK).FirstOrDefault();
                                if (Origin != null)
                                {
                                    nr.Type = Origin.CottonOrigin_Description;
                                }
                            }

                            datatable.AddDataTable1Row(nr); ;
                        }

                    }

                }
                ds1.Tables.Add(datatable);
                YarnLabels yl = new YarnLabels();

                yl.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = yl;
            }
            else if (_RepNo == 11)  // Pick Lists 
            {
                DataSet1 ds1 = new DataSet1();
                DataSet6.DataTable1DataTable datatable = new DataSet6.DataTable1DataTable();

                foreach (DataGridViewRow row in _oDgv.Rows)
                {
                    if (row.Cells[1].Value == null)
                        continue;
                    /*
                    dataGridView1.Columns.Add(oTxtBoxA);    // 0 
                    dataGridView1.Columns.Add(oChk);        // 1 Selected
                    dataGridView1.Columns.Add(oTxtBoxB);    // 2 Bales Numeric 
                    dataGridView1.Columns.Add(oTxtBoxC);    // 3 MIC Decimal
                    dataGridView1.Columns.Add(oTxtBoxD);    // 4 kgs (NETT) Decimal 
                    dataGridView1.Columns.Add(oTxtBoxE);    // 5 Staple Decimal
                    dataGridView1.Columns.Add(oTxtBoxF);    // 6 Kgs (GROSS) Decimal
                     */
                    DataSet6.DataTable1Row nr = datatable.NewDataTable1Row();
                    try
                    {

                        nr.BaleNo = (int)row.Cells[2].Value;
                        nr.Micro = (decimal)row.Cells[3].Value;
                        nr.NettWeight = (decimal)row.Cells[4].Value;
                        nr.Staple = (decimal)row.Cells[5].Value;
                        nr.LotNo = _LotNo;

                        datatable.AddDataTable1Row(nr);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                ds1.Tables.Add(datatable);
                CottonPickList pl = new CottonPickList();
                pl.SetDataSource(ds1);
                crystalReportViewer1.ReportSource = pl;

            }
            else if (_RepNo == 12)  // Yarn Stock Status  
            {
                DataSet1 ds1 = new DataSet1();
                DataSet8.DataTable1DataTable datatable1 = new DataSet8.DataTable1DataTable();
                DataSet8.DataTable2DataTable datatable2 = new DataSet8.DataTable2DataTable();
                YYSByYO rep1 = new YYSByYO();
                YYSByTex rep2 = new YYSByTex();
                YYSByMachine rep3 = new YYSByMachine();
                YYSForecast rep4 = new YYSForecast();

                IList<TLSPN_YarnOrderPallets> pallets = new List<TLSPN_YarnOrderPallets>();
                bool _First = true;

                //--------------------------------------------------------------------
                //_StockStatus
                //--------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_YarnOrder.Where(X => X.YarnO_Date >= _StockStatus.DateFrom && X.YarnO_Date <= _StockStatus.DateTo && !X.Yarno_Closed).ToList();
                    foreach (var row in Existing)
                    {
                        if (_StockStatus.YSSOption == 1 || _StockStatus.YSSOption == 2 || _StockStatus.YSSOption == 3)
                        {
                            var PalletStore = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == row.YarnO_Pk && x.YarnOP_Complete &&
                                                                                  !x.YarnOP_Issued && !x.YarnOP_Scrapped && !x.YarnOP_Sold).ToList();
                            foreach (var PalletRow in PalletStore)
                            {
                                DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();
                                nr.PalletNo = PalletRow.YarnOP_PalletNo;
                                nr.YarnOrder = row.YarnO_OrderNumber;

                                var YarnDetail = context.TLADM_Yarn.Where(x => x.YA_Id == row.Yarno_YarnType_FK).FirstOrDefault();
                                if (YarnDetail != null)
                                {
                                    nr.Tex = (decimal)YarnDetail.YA_TexCount;
                                    nr.Product = YarnDetail.YA_Description;
                                }

                                var MachineCode = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == row.Yarno_MachNo_FK).FirstOrDefault();
                                if (MachineCode != null)
                                {
                                    nr.Machine = MachineCode.MD_MachineCode;
                                }

                                nr.DateProduced = (DateTime)PalletRow.YarnOP_DatePacked;
                                nr.WIP = PalletRow.YarnOP_Complete;
                                nr.GrossWeight = PalletRow.YarnOP_GrossWeight;
                                nr.NettWeight = PalletRow.YarnOP_NettWeight;
                                nr.Cones = PalletRow.YarnOP_NoOfCones;
                                nr.NoOfConesSpun = PalletRow.YarnOP_NoOfConesSpun;
                                nr.RecKey = 1;
                                datatable1.AddDataTable1Row(nr);

                            }

                            if (_First)
                            {
                                _First = false;

                                DataSet8.DataTable2Row xhr = datatable2.NewDataTable2Row();
                                xhr.YarnOrder = row.YarnO_OrderNumber;
                                xhr.DateFrom = _StockStatus.DateFrom;
                                xhr.DateTo = _StockStatus.DateTo;
                                xhr.RecKey = 1;

                                if (_StockStatus.YSSOption == 1)
                                    xhr.ReportTitle = "Yarn Stock Status By Yarn Order";
                                else if (_StockStatus.YSSOption == 2)
                                    xhr.ReportTitle = "Yarn Stock Status By Tex";
                                else if (_StockStatus.YSSOption == 3)
                                    xhr.ReportTitle = "Yarn Stock Status By Machine";

                                xhr.StoreDetail = "Yarn Store Spinning";


                                datatable2.AddDataTable2Row(xhr);
                            }

                        }
                        else if (_StockStatus.OrderStatus == 1)
                        {
                            decimal ConeWeight = 0.00M;
                            // Yarn Order Weight
                            var OrderWeight = row.Yarno_OrderWeight;
                            // List of pallets as yet not produced 
                            var PalletStore = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == row.YarnO_Pk && !x.YarnOP_Complete).ToList();
                            // Total Number of Pallets to be produced 
                            var PalletCount = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == row.YarnO_Pk).Count();
                            if (PalletCount > 0)
                                ConeWeight = OrderWeight / PalletCount;

                            foreach (var PalletRow in PalletStore)
                            {
                                DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();
                                nr.PalletNo = PalletRow.YarnOP_PalletNo;
                                nr.YarnOrder = row.YarnO_OrderNumber;

                                var YarnDetail = context.TLADM_Yarn.Where(x => x.YA_Id == row.Yarno_YarnType_FK).FirstOrDefault();
                                if (YarnDetail != null)
                                {
                                    nr.Tex = (decimal)YarnDetail.YA_TexCount;
                                    nr.Product = YarnDetail.YA_Description;
                                }

                                var MachineCode = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == row.Yarno_MachNo_FK).FirstOrDefault();
                                if (MachineCode != null)
                                {
                                    nr.Machine = MachineCode.MD_MachineCode;
                                }

                                //  nr.DateProduced = (DateTime)PalletRow.YarnOP_DatePacked;
                                nr.WIP = PalletRow.YarnOP_Complete;
                                nr.GrossWeight = PalletRow.YarnOP_GrossWeight;
                                nr.NettWeight = ConeWeight;
                                nr.Cones = PalletRow.YarnOP_NoOfCones;
                                nr.NoOfConesSpun = PalletRow.YarnOP_NoOfConesSpun;
                                nr.RecKey = 1;
                                datatable1.AddDataTable1Row(nr);
                            }

                            if (_First)
                            {
                                _First = false;
                                DataSet8.DataTable2Row xhr = datatable2.NewDataTable2Row();
                                xhr.YarnOrder = row.YarnO_OrderNumber;
                                xhr.DateFrom = _StockStatus.DateFrom;
                                xhr.DateTo = _StockStatus.DateTo;
                                xhr.RecKey = 1;

                                if (_StockStatus.OrderStatus == 1)
                                    xhr.ReportTitle = "Yarn Stock Status (WIP) By Yarn Order - Forecasted";
                                xhr.StoreDetail = "Yarn Store WIP";

                                datatable2.AddDataTable2Row(xhr);
                            }
                        }
                    }
                }
                if (datatable1.Rows.Count == 0)
                {
                    DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();
                    nr.RecKey = 1;
                    nr.ErrorLog = "No records found matching selection made";
                    datatable1.AddDataTable1Row(nr);

                }
                ds1.Tables.Add(datatable1);
                ds1.Tables.Add(datatable2);

                if (_StockStatus.YSSOption == 1 || _StockStatus.OrderStatus == 1)
                {
                    if (_StockStatus.YSSOption == 1)
                    {
                        rep1.SetDataSource(ds1);
                        crystalReportViewer1.ReportSource = rep1;
                    }
                    else
                    {
                        rep4.SetDataSource(ds1);
                        crystalReportViewer1.ReportSource = rep4;
                    }
                }
                if (_StockStatus.YSSOption == 2)
                {
                    rep2.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep2;
                }
                if (_StockStatus.YSSOption == 3)
                {
                    rep3.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep3;
                }
            }
            else if (_RepNo == 13)  // Yarn Transactions processing 
            {
                DataSet1 ds1 = new DataSet1();
                DataSet7.DataTable1DataTable datatable = new DataSet7.DataTable1DataTable();
                DataSet7.DataTable2DataTable datatable2 = new DataSet7.DataTable2DataTable();

                IList<TLSPN_YarnTransactions> yarnT = new List<TLSPN_YarnTransactions>();
                TLADM_TranactionType tranType = new TLADM_TranactionType();
                string Reason = null;
                using (var context = new TTI2Entities())
                {
                    if (_YarnTOpt.TransNumber != 800)
                    {
                        yarnT = context.TLSPN_YarnTransactions.Where(x => x.YarnTrx_Date >= _YarnTOpt.FromDate && x.YarnTrx_Date <= _YarnTOpt.ToDate).ToList();
                        var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();

                        foreach (var row in yarnT)
                        {

                            if (dept != null)
                            {
                                tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == dept.Dep_Id && x.TrxT_Pk == row.YarnTrx_TranType_FK).FirstOrDefault();
                                // 1st decision is it a yarn available transaction if so loop not needed now
                                //------------------------------------------------------------------------------------
                                if (tranType.TrxT_Number == 700)
                                    continue;

                                if (_YarnTOpt.TransNumber != 0)
                                {
                                    //the user has selected a particular report therefore exclude all other transactions
                                    //------------------------------------------------------------------------------
                                    if (_YarnTOpt.TransNumber != tranType.TrxT_Number)
                                        continue;
                                }
                            }

                            //---------------------------------------------------------------------------------------
                            //now continue to process the transaction has required
                            //------------------------------------------------------------------------

                            DataSet7.DataTable1Row nr = datatable.NewDataTable1Row();

                            var PalletDet = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_Pk == row.YarnTrx_PalletNo_Fk).FirstOrDefault();

                            if (PalletDet != null)
                                nr.PalletNo = PalletDet.YarnOP_PalletNo;

                            var YarnOrdDet = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == row.YarnTrx_YarnOrder_FK).FirstOrDefault();
                            if (YarnOrdDet != null)
                                nr.YarnOrderNo = YarnOrdDet.YarnO_OrderNumber;

                            //---------------------------------------------
                            // Now get the yarn detail
                            //------------------------------------------------

                            var yarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == row.YarnTrx_YarnOrder_FK).FirstOrDefault();
                            if (yarnOrder != null)
                            {
                                var yarnDetails = context.TLADM_Yarn.Where(x => x.YA_Id == yarnOrder.Yarno_YarnType_FK).FirstOrDefault();
                                if (yarnDetails != null)
                                {
                                    nr.YarnTYpe = yarnDetails.YA_Description;
                                    nr.TextCount = yarnDetails.YA_TexCount;
                                    nr.Twist = yarnDetails.YA_Twist;
                                    nr.Identification = yarnDetails.YA_ConeColour;

                                }
                            }


                            nr.NoOfCones = row.YarnTrx_Cones;
                            nr.NetWeight = (decimal?)row.YarnTrx_NettWeight ?? 0.0M;
                            if (row.YarnTrx_WriteOff)
                            {
                                nr.NoOfCones = -1 * row.YarnTrx_Cones;
                                nr.NetWeight = -1 * (decimal)row.YarnTrx_NettWeight;
                            }

                            Reason = row.YarnTrx_Reasons;

                            datatable.AddDataTable1Row(nr);

                        }
                    }
                    else
                    {
                        var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                        if (dept != null)
                        {
                            tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == dept.Dep_Id && x.TrxT_Number == 800).FirstOrDefault();

                            var YarnSPNPallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_Issued && x.YarnOP_DateDispatched >= _YarnTOpt.FromDate && x.YarnOP_DateDispatched <= _YarnTOpt.ToDate).OrderBy(x => x.YarnOP_YarnOrder_FK).ThenBy(x => x.YarnOP_PalletNo).ToList();
                            foreach (var Pallet in YarnSPNPallets)
                            {
                                //---------------------------------------------------------------------------------------
                                //now continue to process the transaction has required
                                //------------------------------------------------------------------------
                                DataSet7.DataTable1Row nr = datatable.NewDataTable1Row();

                                nr.PalletNo = Pallet.YarnOP_PalletNo;

                                var YarnOrdDet = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == Pallet.YarnOP_YarnOrder_FK).FirstOrDefault();
                                if (YarnOrdDet != null)
                                {
                                    nr.YarnOrderNo = YarnOrdDet.YarnO_OrderNumber;
                                    var yarnDetails = context.TLADM_Yarn.Where(x => x.YA_Id == YarnOrdDet.Yarno_YarnType_FK).FirstOrDefault();
                                    if (yarnDetails != null)
                                    {
                                        nr.YarnTYpe = yarnDetails.YA_Description;
                                        nr.TextCount = yarnDetails.YA_TexCount;
                                        nr.Twist = yarnDetails.YA_Twist;
                                        nr.Identification = yarnDetails.YA_ConeColour;


                                    }


                                    nr.NoOfCones = Pallet.YarnOP_NoOfCones;
                                    nr.NetWeight = Pallet.YarnOP_NettWeight;
                                    Reason = string.Empty;

                                    datatable.AddDataTable1Row(nr);

                                }
                            }
                        }
                    }

                    DataSet7.DataTable2Row hr = datatable2.NewDataTable2Row();
                    hr.TransNumber = _YarnTOpt.TransNumber;

                    if (_YarnTOpt.TransNumber == 800)
                        hr.ReportTitle = "Yarn Issue To Knitting";
                    else if (_YarnTOpt.TransNumber == 900)
                        hr.ReportTitle = "Yarn Sold Delivery Note";
                    else if (_YarnTOpt.TransNumber == 1000)
                        hr.ReportTitle = "Yarn Scrapped Report";
                    else
                        hr.ReportTitle = "Yarn Adjusted Report";

                    hr.FromDate = _YarnTOpt.FromDate;
                    hr.ToDate = _YarnTOpt.ToDate;
                    hr.Reason = Reason;

                    var FromWhse = context.TLADM_WhseStore.Where(x => x.WhStore_Id == tranType.TrxT_FromWhse_FK).FirstOrDefault();
                    if (FromWhse != null)
                        hr.FromWhse = FromWhse.WhStore_Description;
                    var ToWhse = context.TLADM_WhseStore.Where(x => x.WhStore_Id == tranType.TrxT_ToWhse_FK).FirstOrDefault();
                    if (ToWhse != null)
                        if (FromWhse.WhStore_Description != ToWhse.WhStore_Description)
                            hr.ToWhse = ToWhse.WhStore_Description;

                    datatable2.AddDataTable2Row(hr);

                    ds1.Tables.Add(datatable);
                    ds1.Tables.Add(datatable2);
                    YarnTransaction rep1 = new YarnTransaction();
                    rep1.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep1;



                }
            }
            else if (_RepNo == 14)  // Quality Results by Spinning Machine _YarnRepOpt;
            {                       //==================================================
                DataSet1 ds1 = new DataSet1();
                DataSet9.DataTable1DataTable datatable1 = new DataSet9.DataTable1DataTable();
                DataSet10.DataTable1DataTable datatable2 = new DataSet10.DataTable1DataTable();
                DataSet10.DataTable2DataTable datatable3 = new DataSet10.DataTable2DataTable();
                int _YarnOrderNo = 0;
                string[] machdet = new string[5];
                IList<QAData> QAD = new List<QAData>();
                IList<TLSPN_QAMeasurements> Transactions = null;
                String MachineDesc;

                SpinningRepository repo = new SpinningRepository();

                using (var context = new TTI2Entities())
                {
                    if (_YarnRepOpt.MeasurementOption == 1 || _YarnRepOpt.MeasurementOption == 3)
                    {
                        _QueryParms.MeasurementOpt = _YarnRepOpt.MeasurementOption;

                        Transactions = repo.QAMeasurementQuery(_QueryParms).ToList();

                        foreach (var rowDate in Transactions)
                        {
                            MachineDesc = context.TLADM_MachineDefinitions.Find(rowDate.YarnQA_MachineNo_FK).MD_MachineCode;
                            if (_YarnRepOpt.MeasurementOption == 1)
                            {
                                QAData qD = new QAData(rowDate.YarnQA_Date, MachineDesc, rowDate.YarnQA_08H00, rowDate.YarnQA_10H00);

                                QAD.Add(qD);
                            }
                            else
                            {
                                QAData qD = new QAData(rowDate.YarnQA_Date, MachineDesc, rowDate.YarnQA_10H00, rowDate.YarnQA_12H00);
                                QAD.Add(qD);
                            }

                        }

                        foreach (var Record in QAD)
                        {
                            DataSet10.DataTable1Row nr = datatable2.NewDataTable1Row();
                            nr.Date = Record._DateTime;
                            nr.Key = 1;
                            nr.MachDesc = Record._MachineDesc;
                            nr.Mach3 = Record._Avg;
                            nr.Mach4 = Record._CV;
                            datatable2.AddDataTable1Row(nr);
                        }

                        DataSet10.DataTable2Row hnr = datatable3.NewDataTable2Row();
                        hnr.Mach1 = "Date";
                        hnr.Mach2 = "Machine Description";
                        if (_YarnRepOpt.MeasurementOption == 1)
                        {

                            hnr.Mach3 = "Avg";
                            hnr.Mach4 = "CV %";
                        }
                        else
                        {
                            hnr.LowerTolerance = _QueryParms.LowerTolerance;
                            hnr.UpperTolerance = _QueryParms.UpperTolerance;

                            hnr.Mach3 = "Canister";
                            hnr.Mach4 = "Measurement";
                        }

                        datatable3.AddDataTable2Row(hnr);

                    }
                    else if (_YarnRepOpt.MeasurementOption == 2)
                    {
                        _QueryParms.MeasurementOpt = _YarnRepOpt.MeasurementOption;

                        var Existing = repo.QAMeasurementQuery(_QueryParms).ToList();
                        foreach (var rowDate in Existing)
                        {
                            DataSet9.DataTable1Row nr = datatable1.NewDataTable1Row();
                            var yofk = rowDate.YarnQA_YarnOrder_FK;
                            var YarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == yofk).FirstOrDefault();
                            if (YarnOrder != null)
                            {
                                nr.YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                                _YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                                var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == YarnOrder.Yarno_YarnType_FK).FirstOrDefault();
                                if (YarnType != null)
                                {
                                    nr.Tex = YarnType.YA_TexCount;
                                }
                            }

                            nr.Count = (decimal)rowDate.YarnQA_08H00;
                            nr.CV = (decimal)rowDate.YarnQA_10H00;
                            nr.Thin = (int)rowDate.YarnQA_12H00;
                            nr.Thick = (int)rowDate.YarnQA_14H00;
                            nr.Nep = (int)rowDate.YarnQA_16H00;
                            nr.IPI = (int)rowDate.YarnQA_18H00;
                            nr.Date = rowDate.YarnQA_Date;
                            var MachineDetail = context.TLADM_MachineDefinitions.Find(rowDate.YarnQA_MachineNo_FK);
                            if (MachineDetail != null)
                            {
                                nr.MachNo = MachineDetail.MD_MachineCode;
                                nr.Description = MachineDetail.MD_Description;
                            }
                            datatable1.AddDataTable1Row(nr);
                        }
                    }
                }
                if (_YarnRepOpt.MeasurementOption == 1 || _YarnRepOpt.MeasurementOption == 3)
                {
                    ds1.Tables.Add(datatable2);
                    ds1.Tables.Add(datatable3);
                }
                else
                {
                    ds1.Tables.Add(datatable1);
                }

                if (_YarnRepOpt.MeasurementOption == 1)
                {
                    QCheckResultsByM rep1 = new QCheckResultsByM();
                    if (_YarnRepOpt.QASummary)
                    {
                        rep1.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                    }

                    rep1.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep1;
                }
                else if (_YarnRepOpt.MeasurementOption == 2)
                {
                    QCheckResults rep1 = new QCheckResults();
                    if (_YarnRepOpt.QASummary)
                    {
                        rep1.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                    }

                    rep1.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep1;
                }
                else if (_YarnRepOpt.MeasurementOption == 3)
                {
                    QARSBMeasurements rep1 = new QARSBMeasurements();
                    if (_YarnRepOpt.QASummary)
                    {
                        rep1.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                    }

                    rep1.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep1;
                }

            }
            else if (_RepNo == 15)  // NSI Items Consumption; _nsiSelection
            {
                DataSet ds1 = new DataSet();
                DataSet11.DataTable1DataTable datatable1 = new DataSet11.DataTable1DataTable();
                DataSet11.DataTable2DataTable datatable2 = new DataSet11.DataTable2DataTable();
                List<PalletProd> PalletProd = new List<PalletProd>();
                List<CorrellateData> StoreData = new List<CorrellateData>();
                IList<string> measureProp = new List<string>();
                decimal[] MeasurementspKg = null; // new decimal[8];
                string[] machdet = new string[8];

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_DatePacked >= _nsiSelection.fromDate && x.YarnOP_DatePacked <= _nsiSelection.toDate && x.YarnOP_Complete).GroupBy(x => x.YarnOP_YarnOrder_FK).ToList();
                    //------------------------------------------
                    // 1st Thing is that we need the production for each machine
                    //------------------------------------------------------------------
                    foreach (var row in Existing)
                    {
                        //------------------------------------------------------
                        // 1st get the original order from the file 
                        //-----------------------------------------------
                        var OrderKey = row.FirstOrDefault().YarnOP_YarnOrder_FK;
                        var orderDet = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == OrderKey).FirstOrDefault();
                        if (orderDet != null)
                        {
                            //---------------------------------------------------------------
                            //if not all machines 
                            //-----------------------------------------------------------
                            if (!_nsiSelection.allmachines)
                            {
                                if (_nsiSelection.machineKey != orderDet.Yarno_MachNo_FK)
                                    continue;
                            }
                            //-------------------------------------------------------------
                            // need just to production numbers
                            //-------------------------------------------------------------
                            foreach (var pallet in row)
                            {
                                var p = PalletProd.Find(x => x._MachineKey == orderDet.Yarno_MachNo_FK);
                                if (p == null)
                                {
                                    PalletProd np = new PalletProd(orderDet.Yarno_MachNo_FK, pallet.YarnOP_NettWeight, orderDet.Yarno_YarnType_FK);
                                    PalletProd.Add(np);
                                }
                                else
                                    p._NettWeight += pallet.YarnOP_NettWeight;
                            }

                        }
                    }
                    // End of Production collection
                    //--------------------------------------------------------------
                    if (_nsiSelection.NSI)    // This handles the Non Stock Items
                    {
                        //-----------------------------------------------
                        // Get a list of the properties that generally get measured
                        //-----------------------------------------------------------------
                        var Properties = typeof(TLADM_MachineDefinitions).GetProperties();
                        foreach (var prop in Properties)
                        {
                            if (prop.Name.Contains("Measure_FK"))
                                measureProp.Add(prop.Name);
                        }

                        var MachineGrouped = PalletProd.GroupBy(x => x._MachineKey);
                        foreach (var MachineGroup in MachineGrouped)
                        {
                            decimal[] Measurements = new decimal[9];
                            MeasurementspKg = new decimal[8];

                            var MachineKey = MachineGroup.FirstOrDefault()._MachineKey;
                            var MachineInfo = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == MachineKey).FirstOrDefault();
                            if (MachineInfo != null)
                            {
                                foreach (var prop in measureProp)
                                {
                                    var NettWeight = MachineGroup.Sum(x => (decimal?)x._NettWeight) ?? 0.00M;

                                    if (prop.Contains("First"))
                                    {
                                        if (MachineInfo.MD_FirstMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[0] = NettWeight;
                                        Measurements[1] = (decimal)MachineInfo.MD_FirstMeasure_Qty * NettWeight;
                                    }
                                    else if (prop.Contains("Sec"))
                                    {
                                        if (MachineInfo.MD_SecMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[2] = (decimal)MachineInfo.MD_SecMeasure_Qty * NettWeight;
                                    }
                                    else if (prop.Contains("Third"))
                                    {
                                        if (MachineInfo.MD_ThirdMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[3] = (decimal)MachineInfo.MD_ThirdMeasure_Qty * NettWeight;
                                    }
                                    else if (prop.Contains("Fourth"))
                                    {
                                        if (MachineInfo.MD_FourthMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[4] = (decimal)MachineInfo.MD_FourthMeasure_Qty * NettWeight;

                                    }
                                    else if (prop.Contains("Fifth"))
                                    {
                                        if (MachineInfo.MD_FifthMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[5] = (decimal)MachineInfo.MD_FifthMeasure_Qty * NettWeight;

                                    }
                                    else if (prop.Contains("Six"))
                                    {
                                        if (MachineInfo.MD_SixMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[6] = (decimal)MachineInfo.MD_SixMeasure_Qty * NettWeight;

                                    }
                                    else if (prop.Contains("Seven"))
                                    {
                                        if (MachineInfo.MD_SevenMeasure_FK == null)
                                            continue;

                                        //--------------------------------------------
                                        //now we know it has to be measured
                                        //------------------------------------
                                        Measurements[7] = (decimal)MachineInfo.MD_SecMeasure_Qty * NettWeight;

                                    }



                                    //----------------------------------------------------
                                    // End of process of all machines write out the report
                                    // start with the header 
                                    //--------------------------------------------------


                                }

                                //---------------------------------------------------
                                // Now we have to work out the per kg factor for each machine
                                //--------------------------------------------------------
                                MeasurementspKg[0] = Measurements[1] / Measurements[0];
                                MeasurementspKg[1] = Measurements[2] / Measurements[0];
                                MeasurementspKg[2] = Measurements[3] / Measurements[0];
                                MeasurementspKg[3] = Measurements[4] / Measurements[0];
                                MeasurementspKg[4] = Measurements[5] / Measurements[0];
                                MeasurementspKg[5] = Measurements[6] / Measurements[0];
                                MeasurementspKg[6] = Measurements[7] / Measurements[0];
                                MeasurementspKg[7] = Measurements[8] / Measurements[0];

                                DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();

                                nr.Measurement = MachineInfo.MD_MachineCode;
                                nr.Key = 1;
                                nr.Mach1 = Measurements[0];     // Production
                                nr.Mach2 = Measurements[1];     // Electricity KVA 
                                nr.Mach3 = Measurements[2];     // Electricity KWH
                                nr.Mach4 = Measurements[3];     // Labour
                                nr.Mach5 = MeasurementspKg[0];  // Electricity KVA / Kg
                                nr.Mach6 = MeasurementspKg[1];  // Electricity KWH / Kg
                                nr.Mach7 = MeasurementspKg[2];  // Labour
                                nr.Mach8 = MeasurementspKg[3];

                                datatable1.AddDataTable1Row(nr);
                            }

                        }

                        // End of process of all machines write out the report
                        // start with the header 
                        //--------------------------------------------------
                        DataSet11.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.Key = 1;
                        hnr.Head1 = string.Empty;
                        hnr.Head2 = string.Empty;
                        hnr.Head3 = string.Empty;
                        hnr.Head4 = string.Empty;
                        hnr.Head5 = string.Empty;
                        hnr.Head6 = string.Empty;
                        hnr.Head7 = string.Empty;
                        hnr.Head8 = string.Empty;
                        hnr.FromDate = _nsiSelection.fromDate;
                        hnr.ToDate = _nsiSelection.toDate;

                        datatable2.AddDataTable2Row(hnr);

                        DataView DataV = datatable1.DefaultView;
                        DataV.Sort = "Measurement";
                        ds1.Tables.Add(DataV.ToTable());
                        ds1.Tables.Add(datatable2);

                        YarnNSItems rep1 = new YarnNSItems();
                        rep1.SetDataSource(ds1);
                        crystalReportViewer1.ReportSource = rep1;

                    }
                    else  // This handles the capacity utilisation
                    {
                        //------------------------------------------------------------------
                        // The follow loop gets all the measurement details
                        //---------------------------------------------------------
                        string[] crosstabTitle = new string[6];
                        crosstabTitle[0] = "Production (Kg)";
                        crosstabTitle[1] = "Production Capacity 100%";
                        crosstabTitle[2] = "Production Capacity";
                        crosstabTitle[3] = "% utilisation @ 100%";
                        crosstabTitle[4] = "% utilisation";
                        crosstabTitle[5] = string.Empty;
                        //============================================================
                        //---------Define the datatable for the data element 
                        //=================================================================
                        System.Data.DataTable dt = new System.Data.DataTable();
                        DataColumn column;

                        //------------------------------------------------------
                        // Create column 0. // This is the Machine Description
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(String);
                        column.ColumnName = "Col0";
                        dt.Columns.Add(column);


                        //-----------------------------------------------------------
                        // Create column 1. // This is the Yarn Type
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(string);
                        column.ColumnName = "Col1";
                        dt.Columns.Add(column);


                        //-----------------------------------------------------------
                        // Create column 2. // This is the total production for the period 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(Decimal);
                        column.ColumnName = "Col2";
                        dt.Columns.Add(column);

                        //-----------------------------------------------------------
                        // Create column 3. // This is the total production capacity @ 100 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(Decimal);
                        column.ColumnName = "Col3";
                        dt.Columns.Add(column);

                        //-----------------------------------------------------------
                        // Create column 4. // This is the total production capacity @ 85
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(Decimal);
                        column.ColumnName = "Col4";
                        dt.Columns.Add(column);

                        //-----------------------------------------------------------
                        // Create column 5. // This is % utilisation @ 100 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(Decimal);
                        column.ColumnName = "Col5";
                        dt.Columns.Add(column);

                        //-----------------------------------------------------------
                        // Create column 6. // This is % utilsation @ 85 
                        //----------------------------------------------
                        column = new DataColumn();
                        column.DataType = typeof(Decimal);
                        column.ColumnName = "Col6";
                        dt.Columns.Add(column);


                        var NoOfDays = _nsiSelection.toDate - _nsiSelection.fromDate;
                        var NoOfHours = Math.Round(NoOfDays.TotalHours, 0);           // This gives the number of hours worked 

                        foreach (var item in PalletProd)
                        {
                            var MachineInfo = context.TLADM_MachineDefinitions.Find(item._MachineKey);
                            if (MachineInfo != null)
                            {
                                //==================================
                                //Need to create a record
                                //==========================================
                                DataRow Row = dt.NewRow();

                                //----------------------------------------------------------------
                                // Initialise the control arrays
                                //-----------------------------------------------------------------
                                decimal[] Measurements = new decimal[5];
                                Measurements[0] = item._NettWeight;
                                Measurements[1] = (decimal)NoOfHours * MachineInfo.MD_MaxCapacity;
                                Measurements[2] = Measurements[1] * (MachineInfo.MD_Realistic / 100);
                                //----------------------------------------------------------------------
                                // Calculate the Capacity Utilisation 
                                //------------------------------------------------------------------------
                                Measurements[3] = Measurements[0] / Measurements[1] * 100;
                                Measurements[4] = Measurements[0] / Measurements[2] * 100;

                                Row[0] = MachineInfo.MD_MachineCode;

                                //--------------------------------------------------------------------
                                // find the tex type 1st the Yarn 
                                //---------------------------------------------------------------------
                                var Yarn = context.TLADM_Yarn.Where(x => x.YA_Id == item._YarnType_FK).FirstOrDefault();
                                if (Yarn != null)
                                {
                                    Row[1] = Yarn.YA_TexCount.ToString();
                                }

                                Row[2] = Measurements[0];
                                Row[3] = Measurements[1];
                                Row[4] = Measurements[2];
                                Row[5] = Measurements[3];
                                Row[6] = Measurements[4];

                                dt.Rows.Add(Row);

                            }
                        }
                        //----------------------------------------------------
                        // End of process of all machines write out the report
                        // start with the header 
                        //--------------------------------------------------
                        DataSet11.DataTable2Row hnr = datatable2.NewDataTable2Row();
                        hnr.Key = 1;
                        hnr.Head1 = crosstabTitle[0];
                        hnr.Head2 = crosstabTitle[1];
                        hnr.Head3 = crosstabTitle[2];
                        hnr.Head4 = crosstabTitle[3];
                        hnr.Head5 = crosstabTitle[4];
                        hnr.Head6 = crosstabTitle[5];

                        hnr.FromDate = _nsiSelection.fromDate;
                        hnr.ToDate = _nsiSelection.toDate;

                        datatable2.AddDataTable2Row(hnr);
                        //---------------------------------------------------
                        // Now the stored data 
                        //---------------------------------------------
                        foreach (DataRow Row in dt.Rows)
                        {
                            DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();
                            nr.Measurement = Row.Field<String>(0) + " " + Row.Field<String>(1);
                            nr.Key = 1;
                            nr.Mach1 = Row.Field<Decimal>(2);
                            nr.Mach2 = Row.Field<Decimal>(3);
                            nr.Mach3 = Row.Field<Decimal>(4);
                            nr.Mach4 = Row.Field<Decimal>(5);
                            nr.Mach5 = Row.Field<Decimal>(6);

                            datatable1.AddDataTable1Row(nr);
                        }
                        try
                        {
                            ds1.Tables.Add(datatable1);
                            ds1.Tables.Add(datatable2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }

                        YarnCapacityUtil rep1 = new YarnCapacityUtil();
                        rep1.SetDataSource(ds1);
                        crystalReportViewer1.ReportSource = rep1;
                    }
                }
            }
            else if (_RepNo == 16)  // Yarn Production Report _ypSelection
            {
                DataSet1 ds1 = new DataSet1();
                DataSet12.DataTable1DataTable datatable1 = new DataSet12.DataTable1DataTable();
                DataSet12.DataTable2DataTable datatable2 = new DataSet12.DataTable2DataTable();
                IList<TLSPN_YarnOrderPallets> Pallets = new List<TLSPN_YarnOrderPallets>();

                using (var context = new TTI2Entities())
                {
                    if (_ypSelection.SpecificOrder)
                    {
                        var record = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == _ypSelection.orderKey).FirstOrDefault();
                        if (record != null)
                        {
                            Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == record.YarnO_Pk && x.YarnOP_Complete && x.YarnOP_DatePacked >= _ypSelection.fromDate && x.YarnOP_DatePacked <= _ypSelection.toDate).ToList();
                            foreach (var row in Pallets)
                            {
                                DataSet12.DataTable1Row nr = datatable1.NewDataTable1Row();
                                nr.Date = (DateTime)row.YarnOP_DatePacked;
                                nr.YarnOrder = record.YarnO_OrderNumber;
                                nr.PalletNo = row.YarnOP_PalletNo.ToString();
                                var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == record.Yarno_YarnType_FK).FirstOrDefault();
                                if (YarnType != null)
                                {
                                    nr.TexCount = YarnType.YA_TexCount;
                                    nr.Twist = YarnType.YA_Twist;
                                }

                                nr.NoOfCones = row.YarnOP_NoOfCones;
                                nr.Gross = row.YarnOP_GrossWeight;
                                nr.Tare = row.YarnOP_TareWeight;
                                nr.Nett = nr.Gross - nr.Tare; // row.YarnOP_NettWeight;
                                nr.Key = 1;

                                datatable1.AddDataTable1Row(nr);
                            }
                        }
                    }
                    else
                    {
                        if (_ypSelection.IncludeClosed)
                        {
                            var Orders = context.TLSPN_YarnOrder.Where(x => x.YarnO_Date >= _ypSelection.fromDate && x.YarnO_Date <= _ypSelection.toDate).ToList();
                            foreach (var order in Orders)
                            {
                                Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == order.YarnO_Pk && x.YarnOP_Complete).ToList();
                                foreach (var row in Pallets)
                                {

                                    DataSet12.DataTable1Row nr = datatable1.NewDataTable1Row();
                                    nr.Date = (DateTime)row.YarnOP_DatePacked;
                                    nr.YarnOrder = order.YarnO_OrderNumber;
                                    nr.PalletNo = row.YarnOP_PalletNo.ToString();
                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == order.Yarno_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        nr.TexCount = YarnType.YA_TexCount;
                                        nr.Twist = YarnType.YA_Twist;
                                    }
                                    nr.NoOfCones = row.YarnOP_NoOfCones;
                                    nr.Gross = row.YarnOP_GrossWeight;
                                    nr.Tare = row.YarnOP_TareWeight;
                                    nr.Nett = nr.Gross - nr.Tare; // row.YarnOP_NettWeight;
                                    nr.Key = 1;

                                    datatable1.AddDataTable1Row(nr);
                                }
                            }
                        }
                        else
                        {
                            //var Orders = context.TLSPN_YarnOrder.Where(x => x.YarnO_Date >= _ypSelection.fromDate && x.YarnO_Date <= _ypSelection.toDate && !x.Yarno_Closed).ToList();
                            //foreach (var order in Orders)
                            // {
                            Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_DatePacked >= _ypSelection.fromDate && x.YarnOP_DatePacked <= _ypSelection.toDate && x.YarnOP_Complete).ToList();
                            foreach (var row in Pallets)
                            {

                                DataSet12.DataTable1Row nr = datatable1.NewDataTable1Row();
                                nr.Date = (DateTime)row.YarnOP_DatePacked;
                                var Order = context.TLSPN_YarnOrder.Find(row.YarnOP_YarnOrder_FK);
                                if (Order != null)
                                {
                                    nr.YarnOrder = Order.YarnO_OrderNumber;
                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Order.Yarno_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        nr.TexCount = YarnType.YA_TexCount;
                                        nr.Twist = YarnType.YA_Twist;
                                    }
                                }

                                nr.PalletNo = row.YarnOP_PalletNo.ToString();
                                nr.NoOfCones = row.YarnOP_NoOfCones;
                                nr.Gross = row.YarnOP_GrossWeight;
                                nr.Tare = row.YarnOP_TareWeight;
                                nr.Nett = nr.Gross - nr.Tare; // row.YarnOP_NettWeight;
                                nr.Key = 1;
                                datatable1.AddDataTable1Row(nr);
                            }
                            //}
                        }
                    }
                    //-----------------------------------------------------------------------
                    // Do the report header
                    //-----------------------------------------------------------------------
                    DataSet12.DataTable2Row hnr = datatable2.NewDataTable2Row();
                    hnr.FromDate = _ypSelection.fromDate;
                    hnr.ToDate = _ypSelection.toDate;
                    hnr.ReportTitle = "Yarn Production Report";
                    hnr.Key = 1;
                    datatable2.AddDataTable2Row(hnr);

                    ds1.Tables.Add(datatable1);
                    ds1.Tables.Add(datatable2);

                    YarnProduction rep1 = new YarnProduction();
                    if (_ypSelection.QASummary)
                        rep1.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;

                    rep1.SetDataSource(ds1);
                    crystalReportViewer1.ReportSource = rep1;

                }
            }
            else if (_RepNo == 17)  // Yarn WIP Movement Report
            {
                DataSet ds = new DataSet();
                DataSet14.DataTable1DataTable datatable1 = new DataSet14.DataTable1DataTable();

                var firstDayOfMonth = new DateTime(_Dateselected.Year, _Dateselected.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var lastDayOfLastMonth = firstDayOfMonth.AddDays(-1);

                decimal closingbal = 0.00M;
                decimal consumedinProd = 0.00M;
                decimal actualProd = 0.00M;

                using (var context = new TTI2Entities())
                {
                    var ClosingBal = context.TLADM_DepartmentsAreaTransaction.Where(x => x.TLDEPA_Date.Month == lastDayOfLastMonth.Month && x.TLDEPA_Date.Year == lastDayOfLastMonth.Year).ToList();
                    DataSet14.DataTable1Row nr = datatable1.NewDataTable1Row();

                    nr.LayDownNo = "Stock On Hand as at " + lastDayOfLastMonth.ToString("dd/MM/yyy");
                    if (ClosingBal.Count() != 0)
                    {
                        nr.Kg = ClosingBal.Sum(x => x.TRDEPA_Value);
                        closingbal = nr.Kg;
                    }
                    else
                    {
                        nr.Kg = 0;

                    }

                    datatable1.AddDataTable1Row(nr);
                    decimal TotalWeight = 0.00M;
                    var Existing = context.TLSPN_YarnOrderLayDown.Where(x => x.YarnLD_Date >= firstDayOfMonth && x.YarnLD_Date <= lastDayOfMonth).ToList();
                    foreach (var row in Existing)
                    {
                        nr = datatable1.NewDataTable1Row();
                        nr.Date = row.YarnLD_Date;
                        nr.Kg = row.YarnLD_WeightKg;
                        TotalWeight += row.YarnLD_WeightKg;

                        nr.LayDownNo = row.YarnLD_LayDownNo.ToString();
                        nr.LotNo = row.YarnLD_LotNo;
                        nr.NoOfBales = row.YarnLD_NoOfBales;
                        nr.AvgKgs = row.YarnLD_BaleAvgWeight;
                        datatable1.AddDataTable1Row(nr);

                    }

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Total for Period " + firstDayOfMonth.ToString("dd/MM/yyyy") + " : " + lastDayOfMonth.ToString("dd/MM/yyyy");
                    nr.Kg = TotalWeight;
                    datatable1.AddDataTable1Row(nr);

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Total available WIP " + String.Format("{0:MMMM}", lastDayOfMonth) + " : " + lastDayOfLastMonth.Year.ToString();
                    nr.Kg = closingbal + TotalWeight;
                    datatable1.AddDataTable1Row(nr);

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Closing WIP stock " + lastDayOfLastMonth.ToString("dd/MM/yyyy");
                    datatable1.AddDataTable1Row(nr);

                    decimal closingWIP = 0.00M;
                    var DeptArea = context.TLADM_DepartmentsAreaTransaction.Where(x => x.TLDEPA_Date >= firstDayOfMonth && x.TLDEPA_Date <= lastDayOfMonth).OrderBy(x => x.TRDEPA_DeptA_FK).ToList();
                    foreach (var row in DeptArea)
                    {
                        nr = datatable1.NewDataTable1Row();
                        try
                        {
                            nr.LayDownNo = context.TLADM_DepartmentsArea.Find(row.TRDEPA_DeptA_FK).DeptA_Description;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                        nr.Kg = row.TRDEPA_Value;
                        closingWIP += row.TRDEPA_Value;

                        datatable1.AddDataTable1Row(nr);
                    }

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Total closing WIP Stock " + lastDayOfLastMonth.ToString("dd/MM/yyy");
                    nr.Kg = closingWIP;
                    datatable1.AddDataTable1Row(nr);

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Consumed in Production";
                    consumedinProd = (closingbal + TotalWeight) - closingWIP;
                    nr.Kg = (closingbal + TotalWeight) - closingWIP;

                    datatable1.AddDataTable1Row(nr);

                    var yarnProd = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_DatePacked >= firstDayOfMonth && x.YarnOP_DatePacked <= lastDayOfMonth && x.YarnOP_Complete).ToList();
                    if (yarnProd.Count != 0)
                    {
                        actualProd = yarnProd.Sum(x => x.YarnOP_GrossWeight - x.YarnOP_TareWeight);
                    }

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Actual Yarn Production";
                    nr.Kg = actualProd;

                    datatable1.AddDataTable1Row(nr);

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Waste Kg";
                    nr.Kg = actualProd - consumedinProd;
                    datatable1.AddDataTable1Row(nr);

                    nr = datatable1.NewDataTable1Row();
                    nr.LayDownNo = "Waste %";
                    if (consumedinProd != 0)
                        nr.Kg = -100 + ((actualProd / consumedinProd) * 100);
                    else
                        nr.Kg = 0;

                    datatable1.AddDataTable1Row(nr);


                }


                ds.Tables.Add(datatable1);
                YarnMovement rep1 = new YarnMovement();
                rep1.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep1;


            }
            else if (_RepNo == 18)  //  Yarn Stock On Hand _yarnSOHSel
            {
                DataSet ds = new DataSet();
                DataSet15.DataTable1DataTable datatable = new DataSet15.DataTable1DataTable();
                DataSet15.DataTable2DataTable datatable2 = new DataSet15.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var PalletStore = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_Complete && !x.YarnOP_Issued && !x.YarnOP_Sold && !x.YarnOP_RTS && !x.YarnOP_Scrapped).GroupBy(x => x.YarnOP_YarnOrder_FK).ToList();
                    foreach (var PalletGroup in PalletStore)
                    {
                        //Find the order 
                        //-----------------------
                        var YarnOrderKey = PalletGroup.FirstOrDefault().YarnOP_YarnOrder_FK;
                        var OrderDetails = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == YarnOrderKey).FirstOrDefault();

                        foreach (var Pallet in PalletGroup)
                        {
                            try
                            {
                                DataSet15.DataTable1Row nr = datatable.NewDataTable1Row();
                                nr.key = 1;
                                nr.NoOfCones = Pallet.YarnOP_NoOfCones;
                                nr.NettWeight = Pallet.YarnOP_NettWeight;
                                nr.DatePacked = (DateTime)Pallet.YarnOP_DatePacked;
                                nr.PalletNo = Pallet.YarnOP_PalletNo;
                                if (OrderDetails != null)
                                {
                                    nr.YarnOrder = OrderDetails.YarnO_OrderNumber;
                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == OrderDetails.Yarno_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        nr.TexCount = YarnType.YA_TexCount;
                                        nr.TwistFactor = YarnType.YA_Twist;

                                    }

                                    var LayDown = context.TLSPN_CottonTransactions.Where(x => x.cotrx_pk == OrderDetails.Yarno_LayDown_FK).FirstOrDefault();
                                    if (LayDown != null)
                                    {
                                        nr.LayDown = LayDown.cotrx_Return_No;
                                    }

                                    var MachineDetails = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == OrderDetails.Yarno_MachNo_FK).FirstOrDefault();
                                    if (MachineDetails != null)
                                        nr.MachineCode = MachineDetails.MD_MachineCode;

                                    nr.StoreDetails = context.TLADM_WhseStore.Find(Pallet.YarnOP_Store_FK).WhStore_Description;


                                }

                                datatable.AddDataTable1Row(nr);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                    }
                }


                DataSet15.DataTable2Row hnr = datatable2.NewDataTable2Row();
                hnr.key = 1;

                if (_yarnSOHSel.ByStore)
                {
                    hnr.ReportTitle = "Yarn Stock on hand by store";
                }
                else if (_yarnSOHSel.ByTexCount)
                {
                    hnr.ReportTitle = "Yarn Stock on hand by tex";
                }
                else if (_yarnSOHSel.ByTwistFactor)
                {
                    hnr.ReportTitle = "Yarn Stock on hand by twist";
                }
                else if (_yarnSOHSel.ByYarnOrder)
                {
                    hnr.ReportTitle = "Yarn Stock on hand by Order number";
                }
                datatable2.AddDataTable2Row(hnr);

                if (datatable.Rows.Count == 0)
                {
                    DataSet15.DataTable1Row nr = datatable.NewDataTable1Row();
                    nr.key = 1;
                    nr.NoOfCones = 0;
                    nr.NettWeight = 0.00M;
                    nr.MachineCode = "No Yarn in Stock";
                    datatable.AddDataTable1Row(nr);

                }
                ds.Tables.Add(datatable);
                ds.Tables.Add(datatable2);

                if (_yarnSOHSel.ByStore)
                {
                    YarnSOHByStore rep4 = new YarnSOHByStore();
                    rep4.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep4;
                }
                else if (_yarnSOHSel.ByTexCount)
                {
                    YarnSOHByTex rep4 = new YarnSOHByTex();
                    rep4.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep4;

                }
                else if (_yarnSOHSel.ByTwistFactor)
                {
                    YarnSOHByTwist rep4 = new YarnSOHByTwist();
                    rep4.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep4;

                }
                else if (_yarnSOHSel.ByYarnOrder)
                {
                    YarnSOHByYO rep4 = new YarnSOHByYO();
                    rep4.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep4;

                }
            }
            else if (_RepNo == 19)  // WasteSelection _wasteSel;
            {
                DataSet ds = new DataSet();
                DataSet16.DataTable1DataTable datatable = new DataSet16.DataTable1DataTable();
                DataSet16.DataTable2DataTable datatable2 = new DataSet16.DataTable2DataTable();
                YarnWaste rep4 = new YarnWaste();

                IList<TLSPN_YarnWaste> Existing = new List<TLSPN_YarnWaste>();

                using (var context = new TTI2Entities())
                {
                    if (_wasteSel._IncludeDispose)
                        Existing = context.TLSPN_YarnWaste.Where(x => x.TLYW_Date >= _wasteSel._From && x.TLYW_Date <= _wasteSel._To && x.TLYW_Disposed).ToList();
                    else
                        Existing = context.TLSPN_YarnWaste.Where(x => x.TLYW_Date >= _wasteSel._From && x.TLYW_Date <= _wasteSel._To && !x.TLYW_Disposed).ToList();

                    foreach (var record in Existing)
                    {
                        DataSet16.DataTable1Row nr = datatable.NewDataTable1Row();
                        nr.Key = 1;
                        nr.BaleGrossWeight = record.TLYW_BaleGrossWeight;
                        nr.BaleNettWeight = record.TLYW_BaleNettWeight;
                        nr.BaleNo = record.TLYW_BaleNo;
                        if (_wasteSel._IncludeDispose)
                            nr.TDate = (DateTime)record.TLYW_DateDisposed;
                        else
                            nr.TDate = (DateTime)record.TLYW_Date;

                        datatable.AddDataTable1Row(nr);
                    }

                    if (Existing.Count == 0)
                    {
                        DataSet16.DataTable1Row nr = datatable.NewDataTable1Row();
                        nr.Key = 1;
                        nr.BaleNo = "No records found";
                        datatable.AddDataTable1Row(nr);
                    }

                }

                DataSet16.DataTable2Row hnr = datatable2.NewDataTable2Row();
                hnr.FromDate = _wasteSel._From;
                hnr.ToDate = _wasteSel._To;
                hnr.Key = 1;

                if (_wasteSel._IncludeDispose)
                    hnr.ReportTitle = "Yarn Waste Disposed";
                else
                    hnr.ReportTitle = "Yarn Waste Stock on Hand";

                datatable2.AddDataTable2Row(hnr);

                ds.Tables.Add(datatable);
                ds.Tables.Add(datatable2);

                rep4.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep4;

            }
            else if (_RepNo == 20)  // Yarn Orders Pending Production;
            {
                DataSet ds = new DataSet();
                DataSet17.TLADM_YarnDataTable yarnTable = new DataSet17.TLADM_YarnDataTable();
                DataSet17.TLSPN_YarnOrderDataTable orderTable = new DataSet17.TLSPN_YarnOrderDataTable();

                using (var context = new TTI2Entities())
                {
                    var Orders = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).ToList();
                    foreach (var row in Orders)
                    {
                        var Details = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == row.YarnO_Pk).ToList();

                        if (Details.Count == 0)
                            continue;

                        DataSet17.TLSPN_YarnOrderRow yor = orderTable.NewTLSPN_YarnOrderRow();
                        yor.YarnO_Date = row.YarnO_Date;
                        yor.YarnO_DelDate = row.YarnO_DelDate;
                        yor.YarnO_OrderNumber = row.YarnO_OrderNumber;
                        var Completed = Details.Where(x => x.YarnOP_Complete).Sum(x => (int?)x.YarnOP_NettWeight) ?? 0;
                        yor.Yarno_OrderWeight = row.Yarno_OrderWeight - Completed;
                        yor.YarnO_Pk = row.YarnO_Pk;
                        yor.Yarno_YarnType_FK = row.Yarno_YarnType_FK;

                        orderTable.AddTLSPN_YarnOrderRow(yor);
                    }

                    var YarnType = context.TLADM_Yarn.ToList();
                    foreach (var row in YarnType)
                    {
                        DataSet17.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                        yr.YA_ConeColour = row.YA_ConeColour;
                        yr.YA_Description = row.YA_Description;
                        yr.YA_Id = row.YA_Id;
                        yr.YA_TexCount = row.YA_TexCount;
                        yr.YA_Twist = row.YA_Twist;

                        yarnTable.AddTLADM_YarnRow(yr);
                    }
                }

                if (orderTable.Rows.Count == 0)
                {

                }
                ds.Tables.Add(yarnTable);
                ds.Tables.Add(orderTable);

                YOPendingProd pprod = new YOPendingProd();
                pprod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = pprod;
            }
            else if (_RepNo == 21)  // Yarn / cotton Wast Sales;
            {
                DataSet ds = new DataSet();
                DataSet18.TLADM_CustomerFileDataTable cust = new DataSet18.TLADM_CustomerFileDataTable();
                DataSet18.TLSPN_YarnWasteDataTable waste = new DataSet18.TLSPN_YarnWasteDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_YarnWaste.Where(x => x.TLYW_SalesTransactionNO == _LotNo).ToList();
                    foreach (var record in Existing)
                    {
                        DataSet18.TLSPN_YarnWasteRow wr = waste.NewTLSPN_YarnWasteRow();
                        wr.TLYW_BaleGrossWeight = record.TLYW_BaleGrossWeight;
                        wr.TLYW_BaleNettWeight = record.TLYW_BaleNettWeight;
                        wr.TLYW_BaleNo = record.TLYW_BaleNo;
                        wr.TLYW_Customer_FK = (int)record.TLYW_Customer_FK;
                        wr.TLYW_Date = record.TLYW_Date;
                        wr.TLYW_DateDisposed = (DateTime)record.TLYW_DateDisposed;
                        wr.TLYW_Disposed = record.TLYW_Disposed;
                        wr.TLYW_Pk = record.TLYW_Pk;
                        wr.TLYW_SalesTransactionNO = record.TLYW_SalesTransactionNO;

                        waste.AddTLSPN_YarnWasteRow(wr);
                    }

                    var Cust = context.TLADM_CustomerFile.ToList();
                    foreach (var record in Cust)
                    {
                        DataSet18.TLADM_CustomerFileRow cr = cust.NewTLADM_CustomerFileRow();
                        cr.Cust_Address1 = record.Cust_Address1;
                        cr.Cust_Code = record.Cust_Code;
                        cr.Cust_Description = record.Cust_Description;
                        cr.Cust_Pk = record.Cust_Pk;

                        cust.AddTLADM_CustomerFileRow(cr);
                    }
                }


                ds.Tables.Add(cust);
                ds.Tables.Add(waste);

                CottonWastSales pprod = new CottonWastSales();
                pprod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = pprod;

            }
            else if (_RepNo == 22)  // Cotton Bales on Hand in store by Bale Number;
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                _Repo = new SpinningRepository();
                using (var context = new TTI2Entities())
                {
                    var Existing = _Repo.CottonReceivedLots(_QueryParms);
                    foreach (var row in Existing)
                    {
                        DataSet19.DataTable1Row nRow = dataTable1.NewDataTable1Row();
                        nRow.BaleNo = row.CotBales_BaleNo;
                        nRow.LotNumber = row.CotBales_LotNo;
                        nRow.Mic = row.CotBales_Mic;
                        nRow.Staple = row.CotBales_Staple;
                        nRow.Weight_Gross = row.CotBales_Weight_Gross;
                        nRow.Weight_Nett = row.CotBales_Weight_Nett;

                        dataTable1.AddDataTable1Row(nRow);
                    }

                    ds.Tables.Add(dataTable1);
                    CottonBalesInStock pprod = new CottonBalesInStock();
                    if (_QueryParms.CottonRecSummarised)
                    {
                        pprod.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                    }
                    pprod.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = pprod;
                }
            }
            else if (_RepNo == 23)  // Cotton Waste Stock on hand report;
            {
                DataSet ds = new DataSet();
                DataSet20.DataTable1DataTable dataTable1 = new DataSet20.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_YarnWaste.Where(x => !x.TLYW_Disposed).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet20.DataTable1Row nRow = dataTable1.NewDataTable1Row();
                        nRow.Date = row.TLYW_Date;
                        nRow.BaleNo = Convert.ToInt32(row.TLYW_BaleNo);
                        nRow.BaleGrossWeight = row.TLYW_BaleGrossWeight;
                        nRow.BaleNettWeight = row.TLYW_BaleNettWeight;
                        dataTable1.AddDataTable1Row(nRow);
                    }

                    ds.Tables.Add(dataTable1);
                    CottonWasteSOH pprod = new CottonWasteSOH();
                    pprod.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = pprod;
                }
            }
            else if (_RepNo == 24)  // Yarn Order Audit Trail report;
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable1 = new DataSet21.DataTable1DataTable();
                DataSet21.DataTable2DataTable dataTable2 = new DataSet21.DataTable2DataTable();
                DataSet21.DataTable3DataTable dataTable3 = new DataSet21.DataTable3DataTable();

                _Repo = new SpinningRepository();
                using (var context = new TTI2Entities())
                {
                    var yarnOrders = _Repo.YarnOrderQuery(_QueryParms);

                    foreach (var yOrder in yarnOrders)
                    {
                        if (_YarnRepOpt.YarnOrderAuditTrail)
                        {
                            DataSet21.DataTable1Row nRow = dataTable1.NewDataTable1Row();
                            nRow.PrimaryKey = yOrder.YarnO_Pk;
                            nRow.DateSpun = yOrder.YarnO_Date;
                            nRow.OrderQuantity = yOrder.Yarno_OrderWeight;
                            nRow.YarnType = context.TLADM_Yarn.Find(yOrder.Yarno_YarnType_FK).YA_Description;
                            nRow.SpunToDate = (from t1 in context.TLSPN_YarnOrder
                                               join t2 in context.TLSPN_YarnOrderPallets on t1.YarnO_Pk equals t2.YarnOP_YarnOrder_FK
                                               where t2.YarnOP_YarnAvailable && t1.YarnO_Pk == yOrder.YarnO_Pk
                                               select t2).Sum(x => (decimal?)x.YarnOP_NettWeight) ?? 0.00M;

                            nRow.YarnOrderNo = yOrder.YarnO_OrderNumber.ToString();
                            dataTable1.AddDataTable1Row(nRow);

                            var KnitOrders = context.TLKNI_Order.Where(x => x.KnitO_YarnO_FK == yOrder.YarnO_Pk).ToList();
                            foreach (var Order in KnitOrders)
                            {
                                DataSet21.DataTable2Row xRow = dataTable2.NewDataTable2Row();
                                xRow.PrimaryKey = yOrder.YarnO_Pk;
                                xRow.KnitOrder = Order.KnitO_OrderNumber.ToString();
                                xRow.KnitOrderDate = (System.DateTime)Order.KnitO_OrderDate;
                                xRow.Quality = context.TLADM_Griege.Find(Order.KnitO_Product_FK).TLGreige_Description;
                                xRow.KnitMachine = context.TLADM_MachineDefinitions.Find(yOrder.Yarno_MachNo_FK).MD_AssetRegNo;
                                xRow.OrderWeight = Order.KnitO_Weight;
                                xRow.KnittedToDate = (from T1 in context.TLKNI_Order
                                                      join T2 in context.TLKNI_GreigeProduction on T1.KnitO_Pk equals T2.GreigeP_KnitO_Fk
                                                      where T1.KnitO_Pk == Order.KnitO_Pk
                                                      select T2).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;

                                dataTable2.AddDataTable2Row(xRow);

                            }
                        }
                        else
                        {

                            var KnitOrders = context.TLKNI_Order.Where(x => x.KnitO_YarnO_FK == yOrder.YarnO_Pk).ToList();
                            foreach (var Order in KnitOrders)
                            {
                                var Productions = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == Order.KnitO_Pk).ToList();
                                foreach (var Production in Productions)
                                {
                                    DataSet21.DataTable3Row xRow = dataTable3.NewDataTable3Row();
                                    xRow.YarnOrder_Pk = (int)Order.KnitO_YarnO_FK;
                                    xRow.KnitOrder = Order.KnitO_OrderNumber.ToString();
                                    xRow.PieceNumber = Production.GreigeP_PieceNo;
                                    xRow.PieceWeight = Production.GreigeP_weight;
                                    xRow.InStock = Production.GreigeP_Dye;

                                    if (Production.GreigeP_Dye)
                                    {
                                        xRow.InStock = false;
                                        xRow.DyeBatchNo = context.TLDYE_DyeBatch.Where(x => x.DYEB_Pk == Production.GreigeP_DyeBatch_FK).FirstOrDefault().DYEB_BatchNo;
                                    }
                                    else
                                        xRow.InStock = true;

                                    xRow.Quality = context.TLADM_Griege.Find(Production.GreigeP_Greige_Fk).TLGreige_Description;
                                    xRow.KnitOrderWeight = Order.KnitO_Weight;
                                    xRow.YarnOrderNo = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == xRow.YarnOrder_Pk).FirstOrDefault().YarnO_OrderNumber.ToString();

                                    dataTable3.AddDataTable3Row(xRow);

                                }
                            }
                        }
                    }
                }

                if (_YarnRepOpt.YarnOrderAuditTrail)
                {
                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);
                    YarnOrderAuditTrail yarnOrder = new YarnOrderAuditTrail();
                    yarnOrder.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = yarnOrder;
                }
                else
                {
                    ds.Tables.Add(dataTable3);
                    KnitProductionAudit yarnOrder = new KnitProductionAudit();
                    yarnOrder.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = yarnOrder;
                }
            }
            else if (_RepNo == 25)  // SLIVA Production;
            {
                DataSetSliverProduction.SliverProductionHeaderDataTable dtH = new DataSetSliverProduction.SliverProductionHeaderDataTable();
                DataSetSliverProduction.SliverProductionHeaderRow spHRow = dtH.NewSliverProductionHeaderRow();

                spHRow.Key = (int)1;
                spHRow.ReportTitle = "Card and Draw Frame Production";
                spHRow.FromDate = _sliverProductionSelection.DateFrom.ToShortDateString();
                spHRow.ToDate = _sliverProductionSelection.DateTo.ToShortDateString();

                if (_sliverProductionSelection.Summary)
                    CreateSliverProductionSummaryReport(dtH, spHRow);
                else
                    CreateSliverProductionDetailReport(dtH, spHRow);
            } 
            crystalReportViewer1.Refresh();
        }
    

    private void CreateSliverProductionSummaryReport(
                DataSetSliverProduction.SliverProductionHeaderDataTable dtH,
                DataSetSliverProduction.SliverProductionHeaderRow spHRow)
    {
        DataSet ds = new DataSet();

        DataSetSliverProduction.SliverProductionDataTable dt = new DataSetSliverProduction.SliverProductionDataTable();
        DataSetSliverProduction.SliverProductionRow spRow = dt.NewSliverProductionRow();
        spRow.Key = (int)1;
        using (var context = new TTI2Entities())
        {
            spRow.Card1Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 92)).AsEnumerable().Sum(yp => yp.YarnProduction);
            spRow.Card2Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 93)).AsEnumerable().Sum(yp => yp.YarnProduction);
            spRow.Card3Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 94)).AsEnumerable().Sum(yp => yp.YarnProduction);
            spRow.Card4Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 95)).AsEnumerable().Sum(yp => yp.YarnProduction);
            spRow.RSB1Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 96)).AsEnumerable().Sum(yp => yp.YarnProduction);
            spRow.RSB2Production = (double)context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo && (x.MachineNo_FK == 97)).AsEnumerable().Sum(yp => yp.YarnProduction);
            dt.AddSliverProductionRow(spRow);

            ds.Tables.Add(dt);
        }

        dtH.AddSliverProductionHeaderRow(spHRow);
        ds.Tables.Add(dtH);

        SliverProductionSummary spReport = new SliverProductionSummary();
        spReport.SetDataSource(ds);
        crystalReportViewer1.ReportSource = spReport;
    }

    private void CreateSliverProductionDetailReport(
            DataSetSliverProduction.SliverProductionHeaderDataTable dtH,
            DataSetSliverProduction.SliverProductionHeaderRow spHRow)
    {
        DataSet ds = new DataSet();

        DataSetSliverProduction.SliverProductionDataTable dt = new DataSetSliverProduction.SliverProductionDataTable();


        using (var context = new TTI2Entities())
        {
            var sliverProdcution = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= _sliverProductionSelection.DateFrom && x.YarnProductionDate.Value <= _sliverProductionSelection.DateTo).GroupBy(x => x.YarnProductionDate.Value).ToList();
            foreach (var row in sliverProdcution)
            {
                DataSetSliverProduction.SliverProductionRow spRow = dt.NewSliverProductionRow();
                spRow.Key = (int)1;
                spRow.Date = row.Key.Date.ToShortDateString();
                foreach (var key in row)
                {

                    int machineNo = key.MachineNo_FK;

                    switch (machineNo)
                    {
                        case 92:
                            spRow.Card1Production = (double)key.YarnProduction;
                            break;
                        case 93:
                            spRow.Card2Production = (double)key.YarnProduction;
                            break;
                        case 94:
                            spRow.Card3Production = (double)key.YarnProduction;
                            break;
                        case 95:
                            spRow.Card4Production = (double)key.YarnProduction;
                            break;
                        case 96:
                            spRow.RSB1Production = (double)key.YarnProduction;
                            break;
                        case 97:
                            spRow.RSB2Production = (double)key.YarnProduction;
                            break;
                        default:
                            break;
                    }
                }
                dt.AddSliverProductionRow(spRow);
            }
            ds.Tables.Add(dt);
        }

        dtH.AddSliverProductionHeaderRow(spHRow);
        ds.Tables.Add(dtH);

        SliverProductionDetail spReport = new SliverProductionDetail();
        spReport.SetDataSource(ds);
        crystalReportViewer1.ReportSource = spReport;
    }
}

public class CorrellateData
    {
        public string Description { get; set; }
        public decimal _Col1 { get; set; }
        public decimal _Col2 { get; set; }
        public decimal _Col3 { get; set; }
        public decimal _Col4 { get; set; }
        public decimal _Col5 { get; set; }
        public decimal _Col6 { get; set; }
        public decimal _Col7 { get; set; }
        public decimal _Col8 { get; set; }
        public decimal _Col9 { get; set; }
        public decimal _Col10 { get; set; }
    }

    public class QAData
    {
        public DateTime _DateTime;
        public string _MachineDesc;
        public Decimal _Avg;
        public Decimal _CV;

        public QAData(DateTime dt, string Desc, decimal Avg, decimal cv)
        {
            this._DateTime = dt;
            this._MachineDesc = Desc;
            this._Avg = Avg;
            this._CV = cv;
        }
    }

    public class PalletProd
    {
        public int _MachineKey;
        public decimal _NettWeight;
        public int _YarnType_FK;

        public PalletProd(int key, decimal nettWeight, int YT_FK)
        {
            this._MachineKey = key;
            this._NettWeight = nettWeight;
            this._YarnType_FK = YT_FK;
        }
    }

    public class MachinePalletProd
    {
        public int _MachineKey;                  // 
        public int _MeasurementKey;              // 
        public decimal _MeasurementValue;        //
        public decimal _MeasurementRecorded;     // 
       
        public MachinePalletProd(int key, int MeasurementKey, decimal MeasurementValue, decimal MeasurementRecorded)
        {
           this. _MachineKey = key;
           this._MeasurementKey = MeasurementKey;
           this._MeasurementValue = MeasurementValue;
           this._MeasurementRecorded = MeasurementRecorded;
        }
    }

}
