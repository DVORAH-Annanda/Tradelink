using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using System.Reflection; 

namespace Knitting
{
    public partial class frmKnitViewRep : Form
    {
        int _RepNo;
        int _KnitO;
        YarnReportOptions _opts;
        KnitReportOptions _RepOps;
        KnitRepository _Repo;
        KnitQueryParameters _QueryParms;

        public frmKnitViewRep(int repN)
        {
            InitializeComponent();
            _RepNo = repN;
        }

        
        public frmKnitViewRep(int repN, YarnReportOptions opt)
        {
            InitializeComponent();
            _RepNo = repN;
            _opts = opt;
        }

        public frmKnitViewRep(int repN, KnitReportOptions opt)
        {
            InitializeComponent();
            _RepNo = repN;
            _RepOps = opt;
        }

        public frmKnitViewRep(int repN, int KnitO)
        {
            InitializeComponent();
            _RepNo = repN;
            _KnitO = KnitO;
        }

        public frmKnitViewRep(int repN, KnitQueryParameters QueryParms, YarnReportOptions opt)
        {
            InitializeComponent();
            _RepNo = repN;
            _QueryParms = QueryParms;
            _opts = opt;
        }

        public frmKnitViewRep(int repN, KnitQueryParameters QueryParms)
        {
            InitializeComponent();
            _RepNo = repN;
            _QueryParms = QueryParms;
        }


        public frmKnitViewRep(int repN, int KnitO, KnitReportOptions repOps, KnitRepository repo)
        {
            InitializeComponent();
            _RepNo = repN;
            _KnitO = KnitO;
            _RepOps = repOps;
            _Repo = repo;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1)  //frmComKnitting _Knit) variable contains the GRNNumber
            {
                DataSet ds = new DataSet();
                DataSet1.TLADM_CustomerFileDataTable custTable = new DataSet1.TLADM_CustomerFileDataTable();
                DataSet1.TLADM_YarnDataTable yarnTable = new DataSet1.TLADM_YarnDataTable();
                DataSet1.TLKNI_YarnTransactionDataTable yarntransTable = new DataSet1.TLKNI_YarnTransactionDataTable();
                DataSet1.TLKNI_YarnTransactionDetailsDataTable yarntransDetailTable = new DataSet1.TLKNI_YarnTransactionDetailsDataTable();
                DataSet1.TLADM_TranactionTypeDataTable transTypeTable = new DataSet1.TLADM_TranactionTypeDataTable();
                DataSet1.TLADM_WhseStoreDataTable whseTable = new DataSet1.TLADM_WhseStoreDataTable();

                int yarntype_FK = 0;
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_YarnTransaction.Where(x => x.KnitY_Pk == _KnitO).FirstOrDefault();
                    if (Existing != null)
                    {
                        // Get the header record
                        DataSet1.TLKNI_YarnTransactionRow nr = yarntransTable.NewTLKNI_YarnTransactionRow();
                        nr.KnitY_Customer_FK = Existing.KnitY_Customer_FK;
                        nr.KnitY_GRNNumber = Existing.KnitY_GRNNumber;
                        nr.KnitY_Notes = Existing.KnitY_Notes;
                        nr.KnitY_Pk = Existing.KnitY_Pk;
                        nr.KnitY_ThirdParty = Existing.KnitY_ThirdParty;
                        nr.KnitY_TransactionDate = Existing.KnitY_TransactionDate;
                        nr.KnitY_TransactionDoc = Existing.KnitY_TransactionDoc;

                        yarntransTable.AddTLKNI_YarnTransactionRow(nr);
                        // get the transaction details 
                        var ExistingDetail = context.TLKNI_YarnTransactionDetails.ToList();
                        foreach (var row in ExistingDetail)
                        {
                            DataSet1.TLKNI_YarnTransactionDetailsRow ytr = yarntransDetailTable.NewTLKNI_YarnTransactionDetailsRow();
                            ytr.KnitYD_GrossWeight = row.KnitYD_GrossWeight;
                            ytr.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                            ytr.KnitYD_NettWeight = row.KnitYD_NettWeight;
                            ytr.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                            ytr.KnitYD_Notes = row.KnitYD_Notes;
                            var PalletDet = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK);
                            if (PalletDet != null)
                                ytr.KnitYD_PalletNo = PalletDet.TLKNIOP_PalletNo.ToString();
                            else
                                ytr.KnitYD_PalletNo = "0";

                            ytr.KnitYD_Pk = row.KnitYD_Pk;
                            ytr.KnitYD_RTS = false;

                            ytr.KnitYD_TransactionDate = (DateTime)row.KnitYD_TransactionDate;
                            ytr.KnitYD_TransactionNumber = (int)row.KnitYD_TransactionNumber;
                            ytr.KnitYD_TransactionType = row.KnitYD_TransactionType;
                            ytr.KnitYD_WriteOff = row.KnitYD_WriteOff;
                            ytr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;
                            yarntype_FK = row.KnitYD_YarnType_FK;

                            yarntransDetailTable.AddTLKNI_YarnTransactionDetailsRow(ytr);

                        }

                        // Get the yarn Type..... 
                        //-----------------------------------------------

                        var YarnType = context.TLADM_Yarn.ToList();
                        if (YarnType != null)
                        {
                            foreach (var row in YarnType)
                            {
                                DataSet1.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                                yr.YA_Blocked = row.YA_Blocked;
                                yr.YA_ConeColour = row.YA_ConeColour;
                                yr.YA_CottonOrigin_FK = row.YA_CottonOrigin_FK;
                                yr.YA_Description = row.YA_Description;
                                yr.YA_Discontinued = (bool)row.YA_Discontinued;
                                if (row.YA_Discontnued_Date != null)
                                    yr.YA_Discontnued_Date = (DateTime)row.YA_Discontnued_Date;
                                yr.YA_Id = row.YA_Id;
                                yr.YA_PowerN = row.YA_PowerN;
                                yr.YA_ProductGroup_FK = row.YA_ProductGroup_FK;
                                yr.YA_ProductType_FK = row.YA_ProductType_FK;
                                yr.YA_Qty_Show = row.YA_Qty_Show;
                                yr.YA_ROL = row.YA_ROL;
                                yr.YA_ROQ = row.YA_ROQ;
                                yr.YA_StdCost_Actual = row.YA_StdCost_Actual;
                                yr.YA_StdCost_Show = row.YA_StdCost_Show;
                                yr.YA_Supplier_FK = row.YA_Supplier_FK;
                                yr.YA_TexCount = row.YA_TexCount;
                                yr.YA_Twist = row.YA_Twist;
                                yr.YA_UOM_Fk = row.YA_UOM_Fk;
                                yr.YA_YarnType = row.YA_YarnType;

                                yarnTable.AddTLADM_YarnRow(yr);
                            }
                        }

                        var Cust = context.TLADM_CustomerFile.Where(x => x.Cust_Pk == Existing.KnitY_Customer_FK).FirstOrDefault();
                        if (Cust != null)
                        {
                            DataSet1.TLADM_CustomerFileRow cr = custTable.NewTLADM_CustomerFileRow();
                            cr.Cust_Address1 = Cust.Cust_Address1;
                            cr.Cust_Blocked = Cust.Cust_Blocked;
                            cr.Cust_Code = Cust.Cust_Code;
                            cr.Cust_CommissionCust = Cust.Cust_CommissionCust;
                            cr.Cust_ContactPerson = Cust.Cust_ContactPerson;
                            cr.Cust_ContactPersonEmail = Cust.Cust_ContactPersonEmail;
                            cr.Cust_CustomerCat_FK = Cust.Cust_CustomerCat_FK;
                            cr.Cust_Description = Cust.Cust_Description;
                            cr.Cust_EmailContact = Cust.Cust_EmailContact;
                            cr.Cust_Fax = Cust.Cust_Fax;
                            cr.Cust_Notes = Cust.Cust_Notes;
                            cr.Cust_Pk = Cust.Cust_Pk;
                            cr.Cust_PostalAddress = Cust.Cust_PostalAddress;
                            cr.Cust_SendEmail = Cust.Cust_SendEmail;
                            cr.Cust_Telephone = Cust.Cust_Telephone;
                            cr.Cust_Telephone = Cust.Cust_Telephone;
                            cr.Cust_VatReferenced = Cust.Cust_VatReferenced;

                            custTable.AddTLADM_CustomerFileRow(cr);

                        }

                        var TT = context.TLADM_TranactionType.ToList();
                        foreach (var row in TT)
                        {
                            DataSet1.TLADM_TranactionTypeRow tr = transTypeTable.NewTLADM_TranactionTypeRow();
                            tr.TrxT_Pk = row.TrxT_Pk;
                            tr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;
                            transTypeTable.AddTLADM_TranactionTypeRow(tr);
                        }

                        var WH = context.TLADM_WhseStore.ToList();
                        foreach (var row in WH)
                        {
                            DataSet1.TLADM_WhseStoreRow wr = whseTable.NewTLADM_WhseStoreRow();
                            wr.WhStore_Code = row.WhStore_Code;
                            wr.WhStore_Description = row.WhStore_Description;
                            wr.WhStore_Id = row.WhStore_Id;
                            whseTable.AddTLADM_WhseStoreRow(wr);
                        }

                        ds.Tables.Add(custTable);
                        ds.Tables.Add(yarnTable);
                        ds.Tables.Add(yarntransTable);
                        ds.Tables.Add(yarntransDetailTable);
                        ds.Tables.Add(transTypeTable);
                        ds.Tables.Add(whseTable);
                        ComKnitting comKnit = new ComKnitting();
                        comKnit.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = comKnit;
                    }
                }
            }
            else if (_RepNo == 2)   // Greige Production Reporting 
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable datatable = new DataSet2.DataTable1DataTable();
                DataSet2.DataTable2DataTable datatable2 = new DataSet2.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    var KO = context.TLKNI_Order.Where(x => x.KnitO_Pk == _KnitO).FirstOrDefault();

                    DataSet2.DataTable2Row nhr = datatable2.NewDataTable2Row();
                    nhr.Key = 1;
                    nhr.DataColumn1 = KO.KnitO_OrderDate;
                    nhr.DataColumn2 = KO.KnitO_OrderNumber.ToString();

                    datatable2.AddDataTable2Row(nhr);

                    var Machine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                    if (Machine != null)
                    {
                        int I = 0;
                        string Code = string.Empty;
                        
                        int LastNumber = Machine.MD_LastNumberUsed;

                        int NoPieces = KO.KnitO_NoOfPieces;
                        do
                        {
                            Code = Machine.MD_MachineCode.Remove(0,1).Trim();
                            DataSet2.DataTable1Row nr = datatable.NewDataTable1Row();

                            nr.Key = 1;
                            nr.DataColumn1 = Code + LastNumber.ToString();
                            nr.DataColumn2 = 0.00M;

                            datatable.AddDataTable1Row(nr);

                            LastNumber += 1;
                        } while (++I < NoPieces);

                        
                    }

                    ds.Tables.Add(datatable);
                    ds.Tables.Add(datatable2);

                    GreigeProduction rep1 = new GreigeProduction();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                }
            }
            else if (_RepNo == 3) // 3rd Party  
            {
                DataSet ds = new DataSet();
                DataSet3.TLADM_SuppliersDataTable custTable = new DataSet3.TLADM_SuppliersDataTable();
                DataSet3.TLADM_YarnDataTable yarnTable = new DataSet3.TLADM_YarnDataTable();
                DataSet3.TLKNI_YarnTransactionDataTable yarntransTable = new DataSet3.TLKNI_YarnTransactionDataTable();
                DataSet3.TLKNI_YarnTransactionDetailsDataTable yarntransDetailTable = new DataSet3.TLKNI_YarnTransactionDetailsDataTable();
                DataSet3.TLADM_TranactionTypeDataTable trantypeTable = new DataSet3.TLADM_TranactionTypeDataTable();
                DataSet3.TLADM_WhseStoreDataTable storeTable = new DataSet3.TLADM_WhseStoreDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_YarnTransaction.Where(x => x.KnitY_Pk == _KnitO).FirstOrDefault();
                    if (Existing != null)
                    {
                        // Get the header record
                        DataSet3.TLKNI_YarnTransactionRow nr = yarntransTable.NewTLKNI_YarnTransactionRow();
                        nr.KnitY_Customer_FK = Existing.KnitY_Customer_FK;
                        nr.KnitY_GRNNumber = Existing.KnitY_GRNNumber;
                        nr.KnitY_Notes = Existing.KnitY_Notes;
                        nr.KnitY_Pk = Existing.KnitY_Pk;
                        nr.KnitY_ThirdParty = Existing.KnitY_ThirdParty;
                        nr.KnitY_TransactionDate = Existing.KnitY_TransactionDate;
                        nr.KnitY_TransactionDoc = Existing.KnitY_TransactionDoc;

                        yarntransTable.AddTLKNI_YarnTransactionRow(nr);
                        // get the transaction details 
                        var ExistingDetail = context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_KnitY_FK == Existing.KnitY_Pk).ToList();
                        foreach (var row in ExistingDetail)
                        {
                            DataSet3.TLKNI_YarnTransactionDetailsRow ytr = yarntransDetailTable.NewTLKNI_YarnTransactionDetailsRow();
                            ytr.KnitYD_GrossWeight = row.KnitYD_GrossWeight;
                            ytr.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                            ytr.KnitYD_NettWeight = row.KnitYD_NettWeight;
                            ytr.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                            var PalletDet = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK);
                            if(PalletDet != null)
                               ytr.KnitYD_PalletNo = PalletDet.TLKNIOP_PalletNo.ToString();
                            ytr.KnitYD_Pk = row.KnitYD_Pk;
                            ytr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;
                            ytr.KnitYD_TransactionType = row.KnitYD_TransactionType;

                            //yarntype_FK = row.KnitYD_YarnType_FK;

                            yarntransDetailTable.AddTLKNI_YarnTransactionDetailsRow(ytr);

                        }

                        // Get the yarn Type 
                        //-----------------------------------------------

                        var YarnType = context.TLADM_Yarn.ToList();
                        if (YarnType != null)
                        {
                            foreach (var row in YarnType)
                            {
                                DataSet3.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                                yr.YA_Id = row.YA_Id;
                                yr.YA_TexCount = row.YA_TexCount;
                                yr.YA_Twist = row.YA_Twist;
                                yr.YA_Description = row.YA_Description;

                                yarnTable.AddTLADM_YarnRow(yr);
                            }
                        }

                        var Cust = context.TLADM_Suppliers.Where(x => x.Sup_Pk == Existing.KnitY_Customer_FK).FirstOrDefault();
                        if (Cust != null)
                        {
                            DataSet3.TLADM_SuppliersRow cr = custTable.NewTLADM_SuppliersRow();
                            cr.Sup_Pk = Cust.Sup_Pk;
                            cr.Sup_Description = Cust.Sup_Description;


                            custTable.AddTLADM_SuppliersRow(cr);

                        }

                        var Trant = context.TLADM_TranactionType.ToList();
                        foreach (var row in Trant)
                        {
                            DataSet3.TLADM_TranactionTypeRow tr = trantypeTable.NewTLADM_TranactionTypeRow();
                            tr.TrxT_Pk = row.TrxT_Pk;
                            tr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;

                            trantypeTable.AddTLADM_TranactionTypeRow(tr);
                        }

                        var whs = context.TLADM_WhseStore.ToList();
                        foreach (var row in whs)
                        {
                            DataSet3.TLADM_WhseStoreRow wr = storeTable.NewTLADM_WhseStoreRow();
                            wr.WhStore_Code = row.WhStore_Code;
                            wr.WhStore_Description = row.WhStore_Description;
                            wr.WhStore_Id = row.WhStore_Id;

                            storeTable.AddTLADM_WhseStoreRow(wr);
                        }

                        ds.Tables.Add(custTable);
                        ds.Tables.Add(yarnTable);
                        ds.Tables.Add(yarntransTable);
                        ds.Tables.Add(yarntransDetailTable);
                        ds.Tables.Add(trantypeTable);
                        ds.Tables.Add(storeTable);


                        YarnReceipt3rdParty comKnit = new YarnReceipt3rdParty();
                        comKnit.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = comKnit;
                    }
                }
            }
            else if (_RepNo == 4) // Yarn bought from a third party returned
            {
                DataSet ds = new DataSet();
                DataSet5.TLKNI_YarnTransactionDataTable yarntransTable = new DataSet5.TLKNI_YarnTransactionDataTable();
                DataSet5.TLADM_SuppliersDataTable custTable = new DataSet5.TLADM_SuppliersDataTable();
                DataSet5.TLKNI_YarnTransactionDetailsDataTable yarntransDetailTable = new DataSet5.TLKNI_YarnTransactionDetailsDataTable();
                DataSet5.TLADM_YarnDataTable yarnTable = new DataSet5.TLADM_YarnDataTable();
                DataSet5.TLADM_TranactionTypeDataTable tranTypeTable = new DataSet5.TLADM_TranactionTypeDataTable();
                DataSet5.TLADM_WhseStoreDataTable whseTable = new DataSet5.TLADM_WhseStoreDataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_YarnTransaction.ToList();
                    foreach (var row in Existing)
                    {
                        DataSet5.TLKNI_YarnTransactionRow nr = yarntransTable.NewTLKNI_YarnTransactionRow();
                        nr.KnitY_Customer_FK = row.KnitY_Customer_FK;
                        nr.KnitY_Pk = row.KnitY_Pk;
                        nr.KnitY_TransactionDoc = row.KnitY_TransactionDoc;
                        yarntransTable.AddTLKNI_YarnTransactionRow(nr);
                    }
                    
                    var wareh = context.TLADM_WhseStore.ToList();
                    foreach (var row in wareh)
                    {
                        DataSet5.TLADM_WhseStoreRow whr = whseTable.NewTLADM_WhseStoreRow();
                        whr.WhStore_Code = row.WhStore_Code;
                        whr.WhStore_Description = row.WhStore_Description;
                        whr.WhStore_Id = row.WhStore_Id;

                        whseTable.AddTLADM_WhseStoreRow(whr);
                    }

                    var tt = context.TLADM_TranactionType.ToList();
                    foreach (var row in tt)
                    {
                        DataSet5.TLADM_TranactionTypeRow ttr = tranTypeTable.NewTLADM_TranactionTypeRow();
                        ttr.TrxT_Pk = row.TrxT_Pk;
                        ttr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;
                        tranTypeTable.AddTLADM_TranactionTypeRow(ttr);
                    }

                    var ExistingDetail = context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_TransactionNumber == _KnitO).ToList();
                    foreach (var row in ExistingDetail)
                    {
                        DataSet5.TLKNI_YarnTransactionDetailsRow ytr = yarntransDetailTable.NewTLKNI_YarnTransactionDetailsRow();
                        ytr.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                        ytr.KnitYD_NettWeight = row.KnitYD_NettWeight;
                        ytr.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                        ytr.KnitYD_PalletNo = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK).TLKNIOP_PalletNo;
                        ytr.KnitYD_Pk = row.KnitYD_Pk;
                        ytr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;
                        ytr.KnitYD_TransactionNumber = (int)row.KnitYD_TransactionNumber;
                        ytr.KnitYD_TransactionDate = (DateTime)row.KnitYD_TransactionDate;
                        ytr.KnitYD_TransactionType = row.KnitYD_TransactionType;

                        yarntransDetailTable.AddTLKNI_YarnTransactionDetailsRow(ytr);

                    }
                   // Get the yarn Type......Should all be the same 
                   //-----------------------------------------------

                   var YarnType = context.TLADM_Yarn.ToList();
                   if (YarnType != null)
                   {
                       foreach (var row in YarnType)
                       {
                           DataSet5.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                           yr.YA_Id = row.YA_Id;
                           yr.YA_TexCount = row.YA_TexCount;
                           yr.YA_Twist = row.YA_Twist;
                           yr.YA_Description = row.YA_Description;
                           yr.YA_ConeColour = row.YA_ConeColour;

                           yarnTable.AddTLADM_YarnRow(yr);
                       }
                   }

                   var Cust = context.TLADM_Suppliers.ToList();
                   if (Cust != null)
                   {
                       foreach (var row in Cust)
                       {
                           DataSet5.TLADM_SuppliersRow cr = custTable.NewTLADM_SuppliersRow();
                           cr.Sup_Pk = row.Sup_Pk;
                           cr.Sup_Code = row.Sup_Code;
                           cr.Sup_Description = row.Sup_Description;
                           cr.Suip_ShippingAddress1 = row.Suip_ShippingAddress1;

                           custTable.AddTLADM_SuppliersRow(cr);
                       }
                   }


                   ds.Tables.Add(custTable);
                   ds.Tables.Add(yarnTable);
                   ds.Tables.Add(yarntransTable);
                   ds.Tables.Add(yarntransDetailTable);
                   ds.Tables.Add(tranTypeTable);
                   ds.Tables.Add(whseTable);

                   YarnReturnedToSupplier comKnit = new YarnReturnedToSupplier();
                   comKnit.SetDataSource(ds);
                   crystalReportViewer1.ReportSource = comKnit;
                   
                }

            }
            else if (_RepNo == 5) // Commission Yarn Returned to the customer now redundant 
            {
                DataSet ds = new DataSet();
                DataSet6.TLKNI_YarnTransactionDataTable yarntransTable = new DataSet6.TLKNI_YarnTransactionDataTable();
                DataSet6.TLADM_CustomerFileDataTable custTable = new DataSet6.TLADM_CustomerFileDataTable();
                DataSet6.TLKNI_YarnTransactionDetailsDataTable yarntransDetailTable = new DataSet6.TLKNI_YarnTransactionDetailsDataTable();
                DataSet6.TLADM_YarnDataTable yarnTable = new DataSet6.TLADM_YarnDataTable();
                DataSet6.TLADM_TranactionTypeDataTable tranTypeTable = new DataSet6.TLADM_TranactionTypeDataTable();
                DataSet6.TLADM_WhseStoreDataTable whseTable = new DataSet6.TLADM_WhseStoreDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_YarnTransaction.ToList();
                    foreach(var row in Existing)
                    {
                        DataSet6.TLKNI_YarnTransactionRow nr = yarntransTable.NewTLKNI_YarnTransactionRow();
                        nr.KnitY_Customer_FK = row.KnitY_Customer_FK;
                        nr.KnitY_Pk = row.KnitY_Pk;
                        nr.KnitY_TransactionDoc = row.KnitY_TransactionDoc;
                        yarntransTable.AddTLKNI_YarnTransactionRow(nr);
                    }

                    var tt = context.TLADM_TranactionType.ToList();
                    foreach (var row in tt)
                    {
                        DataSet6.TLADM_TranactionTypeRow ttr = tranTypeTable.NewTLADM_TranactionTypeRow();
                        ttr.TrxT_Pk = row.TrxT_Pk;
                        ttr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;
                        tranTypeTable.AddTLADM_TranactionTypeRow(ttr);
                    }

                    var wareh = context.TLADM_WhseStore.ToList();
                    foreach (var row in wareh)
                    {
                        DataSet6.TLADM_WhseStoreRow whr = whseTable.NewTLADM_WhseStoreRow();
                        whr.WhStore_Code = row.WhStore_Code;
                        whr.WhStore_Description = row.WhStore_Description;
                        whr.WhStore_Id = row.WhStore_Id;

                        whseTable.AddTLADM_WhseStoreRow(whr);
                    }

                    var ExistingDetail = context.TLKNI_YarnTransactionDetails.Where(x=>x.KnitYD_TransactionNumber == _KnitO).ToList();
                    foreach (var row in ExistingDetail)
                    {
                          DataSet6.TLKNI_YarnTransactionDetailsRow ytr = yarntransDetailTable.NewTLKNI_YarnTransactionDetailsRow();
                          ytr.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                          ytr.KnitYD_NettWeight = row.KnitYD_NettWeight;
                          ytr.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                          ytr.KnitYD_PalletNo = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK).TLKNIOP_PalletNo .ToString()       ;
                          ytr.KnitYD_Pk = row.KnitYD_Pk;
                          ytr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;
                          ytr.KnitYD_GrossWeight = row.KnitYD_GrossWeight;
                          ytr.KnitYD_Notes = row.KnitYD_Notes;
                          ytr.KnitYD_Pk = row.KnitYD_Pk;
                          ytr.KnitYD_RTS = (bool)row.KnitYD_RTS;
                          ytr.KnitYD_TransactionDate = (DateTime)row.KnitYD_TransactionDate;
                          ytr.KnitYD_TransactionNumber = (int)row.KnitYD_TransactionNumber;
                          ytr.KnitYD_TransactionType = row.KnitYD_TransactionType;
                          ytr.KnitYD_WriteOff = row.KnitYD_WriteOff;
                          ytr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;

                          yarntransDetailTable.AddTLKNI_YarnTransactionDetailsRow(ytr);

                    }
                    // Get the yarn Type......Should all be the same 
                    //-----------------------------------------------

                    var YarnType = context.TLADM_Yarn.ToList();
                    if (YarnType != null)
                    {
                      foreach (var row in YarnType)
                      {
                          DataSet6.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                          yr.YA_Id = row.YA_Id;
                          yr.YA_TexCount = row.YA_TexCount;
                          yr.YA_Twist = row.YA_Twist;
                          yr.YA_Description = row.YA_Description;
                          yr.YA_ConeColour = row.YA_ConeColour;

                          yarnTable.AddTLADM_YarnRow(yr);
                      }
                   }

                   var Cust = context.TLADM_CustomerFile.ToList();
                   if (Cust != null)
                   {
                      foreach (var row in Cust)
                      {
                          DataSet6.TLADM_CustomerFileRow cr = custTable.NewTLADM_CustomerFileRow();
                          cr.Cust_Pk = row.Cust_Pk;
                          cr.Cust_Description = row.Cust_Description;
                          cr.Cust_Address1 = row.Cust_Address1;

                          custTable.AddTLADM_CustomerFileRow(cr);
                      }

                   }


                   ds.Tables.Add(custTable);
                   ds.Tables.Add(yarnTable);
                   ds.Tables.Add(yarntransTable);
                   ds.Tables.Add(yarntransDetailTable);
                   ds.Tables.Add(whseTable);
                   ds.Tables.Add(tranTypeTable);

                  ComYarnReturnedToSupplier comKnit = new ComYarnReturnedToSupplier();
                  comKnit.SetDataSource(ds);
                  crystalReportViewer1.ReportSource = comKnit;
                   
                }

            }
            else if (_RepNo == 6) // Yarn Stock Adjustments _KnitO contains the transnumber 
            {
                DataSet ds = new DataSet();
                DataSet7.TLADM_YarnDataTable yarnTable = new DataSet7.TLADM_YarnDataTable();
                DataSet7.TLKNI_YarnTransactionDetailsDataTable transTable = new DataSet7.TLKNI_YarnTransactionDetailsDataTable();
                DataSet7.TLADM_TranactionTypeDataTable trantypeTable = new DataSet7.TLADM_TranactionTypeDataTable();
                DataSet7.TLADM_WhseStoreDataTable deptTable = new DataSet7.TLADM_WhseStoreDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_TransactionNumber == _KnitO).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet7.TLKNI_YarnTransactionDetailsRow nr = transTable.NewTLKNI_YarnTransactionDetailsRow();
                        nr.KnitYD_ApprovedBy = row.KnitYD_ApprovedBy;
                        nr.KnitYD_GrossWeight = row.KnitYD_GrossWeight;
                        nr.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                        nr.KnitYD_NettWeight = row.KnitYD_NettWeight;
                        nr.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                        nr.KnitYD_Notes = row.KnitYD_Notes;
                        nr.KnitYD_OriginalOrderNo = (int)row.KnitYD_OriginalOrderNo;
                        nr.KnitYD_PalletNo = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK).TLKNIOP_PalletNo.ToString();
                        nr.KnitYD_Pk = row.KnitYD_Pk;
                        nr.KnitYD_RTS = (bool)row.KnitYD_RTS;
                        nr.KnitYD_TransactionDate = (DateTime)row.KnitYD_TransactionDate;
                        nr.KnitYD_TransactionNumber = (int)row.KnitYD_TransactionNumber;
                        nr.KnitYD_TransactionType = row.KnitYD_TransactionType;
                        nr.KnitYD_WriteOff = row.KnitYD_WriteOff;
                        nr.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;

                        transTable.AddTLKNI_YarnTransactionDetailsRow(nr);
                    }

                    var YT = context.TLADM_Yarn.ToList();
                    foreach (var row in YT)
                    {
                        DataSet7.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                        yr.YA_ConeColour = row.YA_ConeColour;
                        yr.YA_Description = row.YA_Description;
                        yr.YA_Id = row.YA_Id;
                        yr.YA_TexCount = row.YA_TexCount;
                        yr.YA_Twist = row.YA_Twist;
                        yr.YA_YarnType = row.YA_YarnType;

                        yarnTable.AddTLADM_YarnRow(yr);
                    }

                    var dept = context.TLADM_WhseStore.ToList();
                    foreach (var row in dept)
                    {
                        DataSet7.TLADM_WhseStoreRow wr = deptTable.NewTLADM_WhseStoreRow();
                        wr.WhStore_Code = row.WhStore_Code;
                        wr.WhStore_DepartmentFK = row.WhStore_DepartmentFK;
                        wr.WhStore_Description = row.WhStore_Description;
                        wr.WhStore_Id = row.WhStore_Id;

                        deptTable.AddTLADM_WhseStoreRow(wr);
                    }

                    var TranType = context.TLADM_TranactionType.ToList();
                    foreach (var row in TranType)
                    {
                        DataSet7.TLADM_TranactionTypeRow tr = trantypeTable.NewTLADM_TranactionTypeRow();
                        tr.TrxT_Department_FK = row.TrxT_Department_FK;
                        tr.TrxT_Description = row.TrxT_Description;
                        tr.TrxT_FinishedGoods_FK = row.TrxT_FinishedGoods_FK;
                        tr.TrxT_FromWhse_FK = (int)row.TrxT_FromWhse_FK;
                        tr.TrxT_Number = row.TrxT_Number;
                        tr.TrxT_Pk = row.TrxT_Pk;
                        tr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;

                        trantypeTable.AddTLADM_TranactionTypeRow(tr);
                    }

                    ds.Tables.Add(yarnTable);
                    ds.Tables.Add(transTable);
                    ds.Tables.Add(deptTable);
                    ds.Tables.Add(trantypeTable);

                    YarnAjustment rep1 = new YarnAjustment();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;

                }
            }
            else if (_RepNo == 7) // Knit Order Printing _KnitO;
            {
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable datatable1 = new DataSet11.DataTable1DataTable();
                DataSet11.DataTable2DataTable datatable2 = new DataSet11.DataTable2DataTable();
             
                Util Core = new Util();

                using (var context = new TTI2Entities())
                {
                    IList<TLADM_Griege> _Quality = context.TLADM_Griege.ToList();
                    IList<TLADM_CustomerFile> _Customers = context.TLADM_CustomerFile.ToList();
                    IList<TLADM_MachineDefinitions> _Machines = context.TLADM_MachineDefinitions.ToList();
                    IList<TLADM_Yarn> _Yarns = context.TLADM_Yarn.ToList();
                    IList<TLSPN_YarnOrder> _YarnOrder = context.TLSPN_YarnOrder.ToList();

                    var KO = context.TLKNI_Order.Find(_KnitO);
                    if (KO != null)
                    {
                        DataSet11.DataTable1Row ord = datatable1.NewDataTable1Row();

                        ord.CustomerNo = _Customers.FirstOrDefault(s=>s.Cust_Pk == KO.KnitO_Customer_FK).Cust_Code;
                        ord.Machine = _Machines.FirstOrDefault(s=>s.MD_Pk == KO.KnitO_Machine_FK).MD_AlternateDesc;
                        ord.OrderNo = "KO" + KO.KnitO_OrderNumber.ToString().PadLeft(5, '0');
                        ord.Pk = KO.KnitO_Pk;
                        ord.Colour = "Not applicable";
                        ord.Size = "Not applicable";
                        ord.Product = string.Empty;

                        var GreigeType = _Quality.FirstOrDefault(s => s.TLGreige_Id == KO.KnitO_Product_FK);
                        if (GreigeType != null)
                        {
                            ord.Product = GreigeType.TLGreige_Description;
                            ord.DskWeight = GreigeType.TLGreige_CubicWeight;

                            if (GreigeType.TLGreige_IsBoughtIn && KO.KnitO_Colour_Fk != null && KO.KnitO_Size_Fk != null)
                            {
                                ord.Colour = context.TLADM_Colours.Find(KO.KnitO_Colour_Fk).Col_Display;
                                ord.Size = context.TLADM_Sizes.Find(KO.KnitO_Size_Fk).SI_Description;
                                
                            }
                        }

                        ord.DskWeight = GreigeType.TLGreige_CubicWeight;
                        ord.DeliveryDate = KO.KnitO_DeliveryDate;
                        ord.OrderDate_ = KO.KnitO_OrderDate;
                        ord.Weight = KO.KnitO_Weight;
                        ord.YLTSetting = KO.KnitO_YLTSetting;
                        ord.Pieces = KO.KnitO_NoOfPieces;

                        datatable1.AddDataTable1Row(ord);


                        var Transactions = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KO.KnitO_Pk && x.TLKYT_TranType == 1).ToList();
                        foreach (var Transaction in Transactions)
                        {
                            var Pallet = context.TLKNI_YarnOrderPallets.Find(Transaction.TLKYT_YOP_FK);
                            if (Pallet != null)
                            {
                                DataSet11.DataTable2Row pr = datatable2.NewDataTable2Row();
                                pr.Pk = KO.KnitO_Pk;
                                pr.StoreDescription = context.TLADM_WhseStore.Find(Pallet.TLKNIOP_Store_FK).WhStore_Description;
                                pr.Weight = Transaction.TLKYT_NettWeight;
                                var YOrder = _YarnOrder.FirstOrDefault(s=>s.YarnO_Pk  == Pallet.TLKNIOP_YarnOrder_FK);
                                if (YOrder != null)
                                {
                                    pr.YarnOrder = "YO" + YOrder.YarnO_OrderNumber.ToString().PadLeft(5, '0');
                                    pr.PalletNo = YOrder.YarnO_OrderNumber + "-" + Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                    TLADM_Yarn YType = _Yarns.FirstOrDefault(s=>s.YA_Id == Pallet.TLKNIOP_YarnType_FK);
                                    if (YType != null)
                                    {
                                        pr.YarnDescription = YType.YA_Description;
                                        pr.TexCount = YType.YA_TexCount;
                                        pr.Twist = YType.YA_Twist;
                                    }
                                }
                                else
                                {
                                    if (KO.KnitO_CommisionCust)
                                    {
                                        pr.YarnOrder = "Comm Customer";
                                        pr.PalletNo = "N/A";
                                    }
                                }

                                datatable2.AddDataTable2Row(pr);
                            }
                        }
                    }
                    if (datatable2.Rows.Count == 0)
                    {
                        DataSet11.DataTable2Row pr = datatable2.NewDataTable2Row();
                        pr.Pk = KO.KnitO_Pk;
                        datatable2.AddDataTable2Row(pr);
                    }
                }

                ds.Tables.Add(datatable1);
                
                ds.Tables.Add(datatable2);
               
                KnitOrder knitO = new KnitOrder();
                knitO.SetDataSource(ds);
                crystalReportViewer1.ReportSource = knitO;
            }
            else if (_RepNo == 8)
            {
                DataSet ds = new DataSet();
                DataSet8.DataTable1DataTable dataTable1 = new DataSet8.DataTable1DataTable();
                DataSet8.DataTable2DataTable dataTable2 = new DataSet8.DataTable2DataTable();
        

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_ReservedBy == _KnitO).OrderBy(x => x.YarnOP_PalletNo).ToList();
                    if (Existing != null)
                    {
                        var Pk = Existing.FirstOrDefault().YarnOP_YarnOrder_FK;
                        var YarnOrder = context.TLSPN_YarnOrder.Find(Pk);
                        var YarnType = context.TLADM_Yarn.Find(YarnOrder.Yarno_YarnType_FK);
                        DataSet8.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.YarnOrder = YarnOrder.YarnO_OrderNumber;
                        nr.DateOfTrans = (DateTime)Existing.FirstOrDefault().YarnOP_DateDispatched;
                        nr.YarnType = YarnType.YA_Description;
                        nr.DeliveryNote = (int)Existing.FirstOrDefault().YarnOP_ReservedBy;
                       
                        dataTable1.AddDataTable1Row(nr);

                        foreach (var Row in Existing)
                        {
                            DataSet8.DataTable2Row t2Row = dataTable2.NewDataTable2Row();
                            t2Row.Pk = 1;
                            t2Row.PalletNo = Row.YarnOP_PalletNo;
                            t2Row.TexCount = YarnType.YA_TexCount;
                            t2Row.Twist = YarnType.YA_Twist;
                            t2Row.NoOfCones = Row.YarnOP_NoOfCones;
                            t2Row.NettWeight = Row.YarnOP_NettWeight;
                            t2Row.DatePacked = (DateTime)Row.YarnOP_DatePacked;

                            dataTable2.Rows.Add(t2Row);

                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                YarnKnitDelivery knitO = new YarnKnitDelivery();
                knitO.SetDataSource(ds);
                crystalReportViewer1.ReportSource = knitO;
            }
            else if(_RepNo == 9)  // Greige Production Faults
            {
                DataSet ds = new DataSet();
                DataSet9.DataTable1DataTable datatable = new DataSet9.DataTable1DataTable();
                DataSet9.DataTable2DataTable datatable2 = new DataSet9.DataTable2DataTable();
                StringBuilder sb;
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == _opts.ReportKey).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet9.DataTable1Row nr = datatable.NewDataTable1Row();

                        nr.Key = 1;
                      
                        sb = new StringBuilder();
                        sb.Append(row.GreigeP_PieceNo);
                        if (_opts.PrintWithCurrent)
                        {
                            if (!string.IsNullOrEmpty(row.GreigeP_Grade))
                                sb.Append("   Gr   " + row.GreigeP_Grade);
                        }
                        nr.DataColumn1 = sb.ToString();
                        
                        if (_opts.PrintWithCurrent)
                        {
                            nr.DataMeas1 = row.GreigeP_Meas1;
                            nr.DataMeas2 = row.GreigeP_Meas2;
                            nr.DataMeas3 = row.GreigeP_Meas3;
                            nr.DataMeas4 = row.GreigeP_Meas4;
                            nr.DataMeas5 = row.GreigeP_Meas5;
                            nr.DataMeas6 = row.GreigeP_Meas6;
                            nr.DataMeas7 = row.GreigeP_Meas7;
                            nr.DataMeas8 = row.GreigeP_Meas8;
                            if(row.GreigeP_Operator_FK != null)
                                nr.DataOperator = context.TLADM_MachineOperators.Find(row.GreigeP_Operator_FK).MachOp_Description;
                        }
                        datatable.AddDataTable1Row(nr);
                    }

                    var KO = context.TLKNI_Order.Where(x => x.KnitO_Pk == _opts.ReportKey).FirstOrDefault();

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "KNIT").FirstOrDefault();
                    if (Dept != null)
                    {
                        var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Dept.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                        string[] header = new string[Reasons.Count + 1];

                        DataSet9.DataTable2Row xhr = datatable2.NewDataTable2Row();

                        foreach (var Reason in Reasons)
                        {
                            sb = new StringBuilder();
                            sb.Append(Reason.QD_ShortCode);
                            sb.Append(Environment.NewLine);
                            sb.Append(Reason.QD_Description);

                            foreach (var ele in header)
                            {
                                if (String.IsNullOrEmpty(ele))
                                {
                                    var index = Array.IndexOf(header, ele);
                                    header[index] = sb.ToString();
                                    break;
                                }

                            }
                        }

                        xhr.Key = 1;
                        xhr.DataColumn2 = header[0];
                        xhr.DataColumn3 = header[1];
                        xhr.DataColumn4 = header[2];
                        xhr.DataColumn5 = header[3];
                        xhr.DataColumn6 = header[4];
                        xhr.DataColumn7 = header[5];
                        xhr.DataColumn8 = header[6];
                        xhr.DataColumn9 = header[7];
                        if (KO != null)
                            xhr.DataColumn10 = KO.KnitO_OrderNumber.ToString();
                       
                        datatable2.AddDataTable2Row(xhr);

                    }

                    ds.Tables.Add(datatable);
                    ds.Tables.Add(datatable2);

                    GreigeProductionFaults rep1 = new GreigeProductionFaults();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                }
            }
            else if (_RepNo == 10)  // Greige Production Faults
            {
                DataSet ds = new DataSet();
                DataSet10.DataTable1DataTable datatable = new DataSet10.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_Order.Where(x =>!x.KnitO_ProductionCaptured && !x.KnitO_Closed).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet10.DataTable1Row nr = datatable.NewDataTable1Row();

                        nr.NoOfPieces = row.KnitO_NoOfPieces;
                        var noOfPieces = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_Captured).Count();
                        
                        nr.NoOfPiecesProd = noOfPieces;
                        nr.NoToGo = row.KnitO_NoOfPieces - noOfPieces;
                        if (nr.NoToGo <= 0)
                            continue;

                        nr.KnitOrder = row.KnitO_OrderNumber.ToString();

                        var machine = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == row.KnitO_Machine_FK).FirstOrDefault();
                        if (machine != null)
                            nr.MachineDetails = machine.MD_AlternateDesc;

                        datatable.AddDataTable1Row(nr);
                    }
                }
                ds.Tables.Add(datatable);
                ProductionRecon prodRecon = new ProductionRecon();
                prodRecon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = prodRecon;

            }
            else if (_RepNo == 11)   // Knitting Yarn Returned 
            {
                DataSet ds = new DataSet();
                DataSet12.DataTable1DataTable dataTable1 = new DataSet12.DataTable1DataTable();
           
                using (var context = new TTI2Entities())
                {
                    var trns = context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_TransactionNumber == _KnitO && x.KnitYD_YarnReturned).ToList();
                    foreach (var row in trns)
                    {
                        DataSet12.DataTable1Row tr =  dataTable1.NewDataTable1Row();
                        var Pallet = context.TLKNI_YarnOrderPallets.Find(row.KnitYD_PalletNo_FK);
                        if (Pallet != null)
                        {
                            tr.Pallet_No = Pallet.TLKNIOP_PalletNo.ToString();
                            tr.Weight = row.KnitYD_NettWeight;

                            var YarnAllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_YOP_FK == row.KnitYD_PalletNo_FK).FirstOrDefault();
                            if (YarnAllocTrans != null)
                            {
                                var KnitOrder = context.TLKNI_Order.Find(YarnAllocTrans.TLKYT_KnitOrder_FK);
                                if (KnitOrder != null)
                                {
                                    tr.KnitOrderNo = KnitOrder.KnitO_OrderNumber.ToString();
                                }
                            }
                        }

                        var Yarn = context.TLADM_Yarn.Find(row.KnitYD_YarnType_FK);
                        if (Yarn != null)
                        {
                            tr.Yarn_Description = Yarn.YA_Description;
                            tr.Tex_Count = Yarn.YA_TexCount;
                            tr.Twist = Yarn.YA_Twist;
                            tr.Identification = Yarn.YA_ConeColour;
                        }

                        var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                        if (Department != null)
                        {
                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Department.Dep_Id && x.TrxT_Number == row.KnitYD_TransactionType).FirstOrDefault(); ;
                            if (TranType != null)
                            {
                                tr.ToStoreDescription = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK).WhStore_Description;
                            }
                        }


                        tr.No_Of_Cones = row.KnitYD_NoOfCones;
                        tr.YarnReturnNo = row.KnitYD_TransactionNumber.ToString();
                        tr.Transaction_Date = (DateTime)row.KnitYD_TransactionDate;
 

                        dataTable1.AddDataTable1Row(tr);
                    }
                    
                }

                ds.Tables.Add(dataTable1);
              
                YarnReturnedToStore prodRecon = new YarnReturnedToStore();
                prodRecon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = prodRecon;

            }
            else if (_RepNo == 12)   // Knitting Order Recponcilliation Report 
            {
                DataSet ds = new DataSet();
                DataSet13.TLKNI_OrderDataTable ordTable = new DataSet13.TLKNI_OrderDataTable();
                DataSet13.TLADM_CustomerFileDataTable custTable = new DataSet13.TLADM_CustomerFileDataTable();
                DataSet13.TLADM_GriegeDataTable productTable = new DataSet13.TLADM_GriegeDataTable();
                DataSet13.TLADM_MachineDefinitionsDataTable machineTable = new DataSet13.TLADM_MachineDefinitionsDataTable();
                DataSet13.TLKNI_YarnTransactionDetailsDataTable transTable = new DataSet13.TLKNI_YarnTransactionDetailsDataTable();
                DataSet13.TLSPN_YarnOrderPalletsDataTable palletsTable = new DataSet13.TLSPN_YarnOrderPalletsDataTable();
                DataSet13.TLADM_YarnDataTable yarnTable = new DataSet13.TLADM_YarnDataTable();
                DataSet13.DataTable1DataTable GreigeProducedTable = new DataSet13.DataTable1DataTable();

                Decimal YarnUsed = 0.00M;

                using (var context = new TTI2Entities())
                {
                    //we need to calculate the 
                    //--------------------------------------------------------------------
                    var ord = context.TLKNI_Order.Where(x => x.KnitO_Pk == _KnitO).FirstOrDefault();
                    if (ord != null)
                    {
                        DataSet13.TLKNI_OrderRow ordr = ordTable.NewTLKNI_OrderRow();

                       //-----------------------------------------------------------------
                       // 1st Hours of Production 
                       //---------------------------------------------------------------------------
                       if (ord.KnitO_ProductionStartDate != null && ord.KnitO_ProductionEndDate != null)
                       {
                           DateTime pStart = (DateTime)ord.KnitO_ProductionStartDate;
                           DateTime pEnd = (DateTime)ord.KnitO_ProductionEndDate;
                           
                           TimeSpan ts = pEnd.Subtract(pStart);
                           
                           ordr.KnitO_MachineDays = ts.Days;
                           var GreigeProduced = context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_KnitO_Fk == ord.KnitO_Pk) .Sum(x => x.GreigeP_weight);
                           var AllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == ord.KnitO_Pk).ToList();
                           foreach (var AllocTran in AllocTrans)
                           {
                               if (AllocTran.TLKYT_TranType == 1 || AllocTran.TLKYT_TranType == 3)
                                  YarnUsed += AllocTran.TLKYT_NettWeight;
                               else if (AllocTran.TLKYT_TranType == 2)
                                  YarnUsed -= AllocTran.TLKYT_NettWeight;
                           }
                           ordr.KnitO_YarnUsed = YarnUsed;
                           ordr.KnitO_LossYarnUsed = YarnUsed - GreigeProduced;
                           ordr.KnitO_GreigeProduced = GreigeProduced;
                           ordr.KnitO_LossPercentage = -100 + ((GreigeProduced / YarnUsed) * 100);
                           var mach = context.TLADM_MachineDefinitions.Find(ord.KnitO_Machine_FK);
                           if (mach != null)
                           {
                               ordr.KnitO_StdPoduction = mach.MD_MaxCapacity * ts.Hours;
                               ordr.KnitO_Efficiency = (GreigeProduced / ordr.KnitO_StdPoduction * 100);
                           }
 
                       }
                                             
                       ordr.KnitO_Pk = ord.KnitO_Pk;
                       ordr.KnitO_Closed = ord.KnitO_Closed;

                       if (ord.KnitO_ClosedDate != null)
                           ordr.KnitO_ClosedDate = (DateTime)ord.KnitO_ClosedDate;

                       ordr.KnitO_CommisionCust = ord.KnitO_CommisionCust;
                       ordr.KnitO_Confirmed = ord.KnitO_Confirmed;
                       ordr.KnitO_Customer_FK = ord.KnitO_Customer_FK;

                       ordr.KnitO_DeliveryDate = (DateTime)ord.KnitO_DeliveryDate;
                       if(ord.KnitO_KnitO_FK != null)
                           ordr.KnitO_KnitO_FK = (int)ord.KnitO_KnitO_FK;

                       ordr.KnitO_Machine_FK = (int)ord.KnitO_Machine_FK;
                       ordr.KnitO_NoOfPieces = ord.KnitO_NoOfPieces;
                       ordr.KnitO_Notes = ord.KnitO_Notes;
                       ordr.KnitO_OrderConfirmed = ord.KnitO_OrderConfirmed;
                       
                       if (ord.KnitO_OrderConfirmedDate != null)
                           ordr.KnitO_OrderConfirmedDate = (DateTime)ord.KnitO_OrderConfirmedDate;

                       ordr.KnitO_OrderDate = (DateTime)ord.KnitO_OrderDate;
                       ordr.KnitO_OrderNumber = (int)ord.KnitO_OrderNumber;
                       ordr.KnitO_Pk = ord.KnitO_Pk;
                       ordr.KnitO_Product_FK = ord.KnitO_Product_FK;
                       ordr.KnitO_ProductionCaptured = ord.KnitO_ProductionCaptured;

                       if(ord.KnitO_ProductionEndDate != null)
                           ordr.KnitO_ProductionEndDate = (DateTime)ord.KnitO_ProductionEndDate;

                       if(ord.KnitO_ProductionStartDate != null)
                           ordr.KnitO_ProductionStartDate = (DateTime)ord.KnitO_ProductionStartDate;

                       ordr.KnitO_ReOpen = ord.KnitO_ReOpen;
                       ordr.KnitO_Weight = ord.KnitO_Weight;
                       
                       if (ord.KnitO_YarnO_FK != null)
                           ordr.KnitO_YarnO_FK = (int)ord.KnitO_YarnO_FK;
                       
                       ordr.KnitO_YarnReturned = ord.KnitO_YarnReturned;
                       ordr.KnitO_YLTSetting = ord.KnitO_YLTSetting;
                     
                       ordTable.AddTLKNI_OrderRow(ordr);
                    }

                    var yt = context.TLADM_Yarn.ToList();
                    foreach (var row in yt)
                    {
                        DataSet13.TLADM_YarnRow yr = yarnTable.NewTLADM_YarnRow();
                        yr.YA_ConeColour = row.YA_ConeColour;
                        yr.YA_Description = row.YA_Description;
                        yr.YA_Id = row.YA_Id;
                        yr.YA_TexCount = row.YA_TexCount;
                        yr.YA_Twist = row.YA_Twist;

                        yarnTable.AddTLADM_YarnRow(yr);
                    }

                    var cst = context.TLADM_CustomerFile.ToList();
                    foreach (var row in cst)
                    {
                        DataSet13.TLADM_CustomerFileRow cr = custTable.NewTLADM_CustomerFileRow();
                        cr.Cust_Description = row.Cust_Description;
                        cr.Cust_Pk = row.Cust_Pk;

                        custTable.AddTLADM_CustomerFileRow(cr);
                    }

                    var mch = context.TLADM_MachineDefinitions.ToList();
                    foreach (var row in mch)
                    {
                        DataSet13.TLADM_MachineDefinitionsRow mr = machineTable.NewTLADM_MachineDefinitionsRow();
                        mr.MD_MachineCode = row.MD_MachineCode;
                        mr.MD_Pk = row.MD_Pk;

                        machineTable.AddTLADM_MachineDefinitionsRow(mr);
                    }

                    var ttable = context.TLKNI_YarnTransactionDetails.ToList();
                    foreach (var row in ttable)
                    {
                        DataSet13.TLKNI_YarnTransactionDetailsRow trnsRow = transTable.NewTLKNI_YarnTransactionDetailsRow();
                        trnsRow.KnitYD_ApprovedBy = row.KnitYD_ApprovedBy;
                        trnsRow.KnitYD_GrossWeight = row.KnitYD_GrossWeight;
                        trnsRow.KnitYD_KnitY_FK = row.KnitYD_KnitY_FK;
                        trnsRow.KnitYD_NettWeight = row.KnitYD_NettWeight;
                        trnsRow.KnitYD_NoOfCones = row.KnitYD_NoOfCones;
                        trnsRow.KnitYD_Notes = row.KnitYD_Notes;
                        if(row.KnitYD_OriginalOrderNo != null)
                            trnsRow.KnitYD_OriginalOrderNo = (int)row.KnitYD_OriginalOrderNo;
                        trnsRow.KnitYD_PalletNo_FK = row.KnitYD_PalletNo_FK;
                        trnsRow.KnitYD_Pk = row.KnitYD_Pk;
                        trnsRow.KnitYD_RTS = (bool)row.KnitYD_RTS;
                        trnsRow.KnitYD_TransactionDate = (DateTime)row.KnitYD_TransactionDate;
                        trnsRow.KnitYD_TransactionNumber = (int)row.KnitYD_TransactionNumber;
                        trnsRow.KnitYD_TransactionType = row.KnitYD_TransactionType;
                        trnsRow.KnitYD_WriteOff = (bool)row.KnitYD_WriteOff;
                        if (!row.KnitYD_YarnReturned)
                            trnsRow.KnitYD_YarnReturned = "Yarn Allocated";
                        else
                            trnsRow.KnitYD_YarnReturned = "Yarn Returned";

                        trnsRow.KnitYD_YarnType_FK = row.KnitYD_YarnType_FK;

                        transTable.AddTLKNI_YarnTransactionDetailsRow(trnsRow);
                    }

                    var Pallets = context.TLKNI_YarnOrderPallets.ToList();
                    foreach (var row in Pallets)
                    {
                        DataSet13.TLSPN_YarnOrderPalletsRow yop = palletsTable.NewTLSPN_YarnOrderPalletsRow();
                        yop.YarnOP_PalletNo = row.TLKNIOP_PalletNo;
                        yop.YarnOP_Pk = row.TLKNIOP_Pk;
                        palletsTable.AddTLSPN_YarnOrderPalletsRow(yop);
                    }

                    var prd = context.TLADM_Griege.ToList();
                    foreach (var row in prd)
                    {
                        DataSet13.TLADM_GriegeRow gri = productTable.NewTLADM_GriegeRow();
                        gri.TLGreige_Description = row.TLGreige_Description;
                        gri.TLGreige_Id = row.TLGreige_Id;

                        productTable.AddTLADM_GriegeRow(gri);
                    }
                   
                    var GP = context.GreigeProduced(_KnitO);
                    if (GP != null)
                    {
                        bool _first = true;
                        DataSet13.DataTable1Row dtr = GreigeProducedTable.NewDataTable1Row();

                        foreach (var row in GP)
                        {
                            if (_first)
                            {
                                _first = !_first;
                                var GT = context.TLADM_Griege.Find(row.KnitO_Product_FK);
                                dtr.Product = GT.TLGreige_Description;
                                dtr.KnitO_Fk = _KnitO;
                            }

                            if (String.IsNullOrEmpty(row.GreigeP_Grade))
                            {
                                dtr.Unknown = row.Cnt.ToString();
                            }
                            else if (row.GreigeP_Grade == "A")
                            {
                                dtr.AGrade = (int)row.Cnt;
                            }
                            else if (row.GreigeP_Grade == "B")
                            {
                                dtr.BGrade = (int)row.Cnt;
                            }
                            else if (row.GreigeP_Grade == "C")
                            {
                                dtr.CGrade = (int)row.Cnt;
                            }
                        }

                        GreigeProducedTable.AddDataTable1Row(dtr);
                    }
                  

                    ds.Tables.Add(productTable);
                    ds.Tables.Add(machineTable);
                    ds.Tables.Add(ordTable);
                    ds.Tables.Add(custTable);
                    ds.Tables.Add(transTable);
                    ds.Tables.Add(palletsTable);
                    ds.Tables.Add(yarnTable);
                    ds.Tables.Add(GreigeProducedTable);

                    KnitOrderReconciliation rep1 = new KnitOrderReconciliation();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                }

            }
            else if (_RepNo == 13)   // Received from Commission Dyeing Customer 
            {
                DataSet ds = new DataSet();
                DataSet14.TLADM_CustomerFileDataTable custTable = new DataSet14.TLADM_CustomerFileDataTable();
                DataSet14.TLADM_GriegeDataTable productTable = new DataSet14.TLADM_GriegeDataTable();
                DataSet14.TLKNI_GreigeCommissionTransctionsDataTable transTable = new DataSet14.TLKNI_GreigeCommissionTransctionsDataTable();
                DataSet14.TLADM_WhseStoreDataTable storeTable = new DataSet14.TLADM_WhseStoreDataTable();
               
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_GrnNo == _KnitO).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet14.TLKNI_GreigeCommissionTransctionsRow tr = transTable.NewTLKNI_GreigeCommissionTransctionsRow();
                        tr.GreigeCom_ApprovedBy = row.GreigeCom_ApprovedBy;
                        tr.GreigeCom_Comments = row.GreigeCom_Comments;
                        tr.GreigeCom_Custdoc = row.GreigeCom_Custdoc;
                        tr.GreigeCom_Customer_FK = row.GreigeCom_Customer_FK;
                        tr.GreigeCom_CustOrderNo = row.GreigeCom_CustOrderNo;
                        tr.GreigeCom_Grade = row.GreigeCom_Grade;
                        tr.GreigeCom_GrnNo = row.GreigeCom_GrnNo;
                        tr.GreigeCom_NettWeight = row.GreigeCom_NettWeight;
                        tr.GreigeCom_PieceNo = row.GreigeCom_PieceNo;
                        tr.GreigeCom_PK = row.GreigeCom_PK;
                        tr.GreigeCom_ProductType_FK = row.GreigeCom_ProductType_FK;
                        tr.GreigeCom_Reason = row.GreigeCom_Reason;
                        tr.GreigeCom_Store_FK = row.GreigeCom_Store_FK;
                        tr.GreigeCom_Transdate = row.GreigeCom_Transdate;
                        tr.GreigeCom_TTSNo = row.GreigeCom_TTSNo;

                        transTable.AddTLKNI_GreigeCommissionTransctionsRow(tr);
                    }
                    
                    var Cust = context.TLADM_CustomerFile.ToList();
                    foreach (var row in Cust)
                    {
                        DataSet14.TLADM_CustomerFileRow cr = custTable.NewTLADM_CustomerFileRow();
                        cr.Cust_Pk = row.Cust_Pk;
                        cr.Cust_Description = row.Cust_Description;

                        custTable.AddTLADM_CustomerFileRow(cr);
                    }

                   
                    var Whse = context.TLADM_WhseStore.ToList();
                    foreach (var row in Whse)
                    {
                        DataSet14.TLADM_WhseStoreRow wr = storeTable.NewTLADM_WhseStoreRow();
                        wr.WhStore_Code = row.WhStore_Code;
                        wr.WhStore_Description = row.WhStore_Description;
                        wr.WhStore_Id = row.WhStore_Id;

                        storeTable.AddTLADM_WhseStoreRow(wr);
                    }

                    var Products = context.TLADM_Griege.ToList();
                    foreach (var row in Products)
                    {
                        DataSet14.TLADM_GriegeRow gr = productTable.NewTLADM_GriegeRow();
                        gr.TLGreige_Description = row.TLGreige_Description;
                        gr.TLGreige_Id = row.TLGreige_Id;

                        productTable.AddTLADM_GriegeRow(gr);
                    }
                }

              

                ds.Tables.Add(transTable);
                ds.Tables.Add(productTable);
                ds.Tables.Add(custTable);
                ds.Tables.Add(storeTable);

                CommDyeingReceipts comRec = new CommDyeingReceipts();
                comRec.SetDataSource(ds);
                crystalReportViewer1.ReportSource = comRec;
            }
            else if (_RepNo == 14)  // Greige Stock adjustment 
            {
                DataSet ds = new DataSet();
                DataSet15.TLADM_GriegeDataTable productTable = new DataSet15.TLADM_GriegeDataTable();
                DataSet15.TLADM_TranactionTypeDataTable tranTypeTable = new DataSet15.TLADM_TranactionTypeDataTable();
                DataSet15.TLADM_WhseStoreDataTable whseTable = new DataSet15.TLADM_WhseStoreDataTable();
                DataSet15.TLKNI_GreigeProductionDataTable productionTable = new DataSet15.TLKNI_GreigeProductionDataTable();
                DataSet15.TLKNI_GreigeTransactionsDataTable transTable = new DataSet15.TLKNI_GreigeTransactionsDataTable();
                DataSet15.TLKNI_OrderDataTable orderTable = new DataSet15.TLKNI_OrderDataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_GreigeTransactions.Where(x => x.GreigeT_TransactionNumber == _KnitO).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet15.TLKNI_GreigeTransactionsRow tr = transTable.NewTLKNI_GreigeTransactionsRow();

                        tr.GreigeT_AdjustedWeight = row.GreigeT_AdjustedWeight;
                        tr.GreigeT_ApprovedBy = row.GreigeT_ApprovedBy;
                        tr.GreigeT_Grade = row.GreigeT_Grade;
                        tr.GreigeT_KOrder_FK = row.GreigeT_KOrder_FK;
                        tr.GreigeT_Piece_FK = row.GreigeT_Piece_FK;
                        tr.GreigeT_Pk = row.GreigeT_Pk;
                        tr.GreigeT_TransactionDate = row.GreigeT_TransactionDate;
                        tr.GreigeT_TransactionNumber = row.GreigeT_TransactionNumber;
                        tr.GreigeT_TransactionType_FK = row.GreigeT_TransactionType_FK;

                        transTable.AddTLKNI_GreigeTransactionsRow(tr);
                    }

                    var trantype = context.TLADM_TranactionType.ToList();
                    foreach (var row in trantype)
                    {
                        DataSet15.TLADM_TranactionTypeRow ttr = tranTypeTable.NewTLADM_TranactionTypeRow();
                        ttr.TrxT_Pk = row.TrxT_Pk;
                        ttr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;

                        tranTypeTable.AddTLADM_TranactionTypeRow(ttr);
                    }

                    var pTable = context.TLKNI_GreigeProduction.ToList();
                    foreach (var row in pTable)
                    {
                        DataSet15.TLKNI_GreigeProductionRow prd = productionTable.NewTLKNI_GreigeProductionRow();
                        prd.GreigeP_PieceNo = row.GreigeP_PieceNo;
                        prd.GreigeP_Pk = row.GreigeP_Pk;

                        productionTable.AddTLKNI_GreigeProductionRow(prd);

                    }

                    var whse = context.TLADM_WhseStore.ToList();
                    foreach (var row in whse)
                    {
                        DataSet15.TLADM_WhseStoreRow whr = whseTable.NewTLADM_WhseStoreRow();
                        whr.WhStore_Code = row.WhStore_Code;
                        whr.WhStore_Description = row.WhStore_Description;
                        whr.WhStore_Id = row.WhStore_Id;

                        whseTable.AddTLADM_WhseStoreRow(whr);

                    }

                    var prdTable = context.TLADM_Griege.ToList();
                    foreach (var row in prdTable)
                    {
                        DataSet15.TLADM_GriegeRow grieg = productTable.NewTLADM_GriegeRow();
                        grieg.TLGreige_Description = row.TLGreige_Description;
                        grieg.TLGreige_Id = row.TLGreige_Id;

                        productTable.AddTLADM_GriegeRow(grieg);
                    }


                    var Order = context.TLKNI_Order.ToList();
                    foreach (var row in Order)
                    {
                        DataSet15.TLKNI_OrderRow ordr = orderTable.NewTLKNI_OrderRow();
                        ordr.KnitO_Pk = row.KnitO_Pk;
                        ordr.KnitO_Product_FK = row.KnitO_Product_FK;

                        orderTable.AddTLKNI_OrderRow(ordr);
                    }
                }

                ds.Tables.Add(productTable);
                ds.Tables.Add(tranTypeTable);
                ds.Tables.Add(whseTable);
                ds.Tables.Add(productionTable);
                ds.Tables.Add(transTable);
                ds.Tables.Add(orderTable);

                GreigeStockAdjustment rep1 = new GreigeStockAdjustment();
                rep1.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep1;


            }
            else if (_RepNo == 15)  // Tradelink Greige Items 
            {
                DataSet ds = new DataSet();
                DataSet16.TLADM_FabricWeightDataTable fabWeightTable = new DataSet16.TLADM_FabricWeightDataTable();
                DataSet16.TLADM_FabWidthDataTable fabWidthTable = new DataSet16.TLADM_FabWidthDataTable();
                DataSet16.TLADM_GreigeQualityDataTable greigeQualTable = new DataSet16.TLADM_GreigeQualityDataTable();
                DataSet16.TLADM_GriegeDataTable greigeDataTable = new DataSet16.TLADM_GriegeDataTable();
                DataSet16.TLADM_MachineDefinitionsDataTable machTable = new DataSet16.TLADM_MachineDefinitionsDataTable();
                DataSet16.TLADM_StockTakeFreqDataTable stockTakeTable = new DataSet16.TLADM_StockTakeFreqDataTable();

                using (var context = new TTI2Entities())
                {
                    var fabWt = context.TLADM_FabricWeight.ToList();
                    foreach (var row in fabWt)
                    {
                        DataSet16.TLADM_FabricWeightRow fr = fabWeightTable.NewTLADM_FabricWeightRow();
                        fr.FWW_Description = row.FWW_Description;
                        fr.FWW_Id = row.FWW_Id;

                        fabWeightTable.AddTLADM_FabricWeightRow(fr);
                    }

                    var fabWidth = context.TLADM_FabWidth.ToList();
                    foreach (var row in fabWidth)
                    {
                        DataSet16.TLADM_FabWidthRow fwr = fabWidthTable.NewTLADM_FabWidthRow();
                        fwr.FW_Description = row.FW_Description;
                        fwr.FW_Id = row.FW_Id;

                        fabWidthTable.AddTLADM_FabWidthRow(fwr);
                    }

                    var GQ = context.TLADM_GreigeQuality.ToList();
                    foreach (var row in GQ)
                    {
                        DataSet16.TLADM_GreigeQualityRow gqr = greigeQualTable.NewTLADM_GreigeQualityRow();
                        gqr.GQ_Description = row.GQ_Description;
                        gqr.GQ_Pk = row.GQ_Pk;

                        greigeQualTable.AddTLADM_GreigeQualityRow(gqr);
                    }

                    var GD = context.TLADM_Griege.ToList();
                    foreach (var row in GD)
                    {
                        DataSet16.TLADM_GriegeRow gdr = greigeDataTable.NewTLADM_GriegeRow();
                        gdr.TLGreige_Description = row.TLGreige_Description;
                        gdr.TLGreige_FabricWeight_FK = row.TLGreige_FabricWeight_FK;
                        gdr.TLGreige_FabricWidth_FK = row.TLGreige_FabricWidth_FK;
                        gdr.TLGreige_Id = row.TLGreige_Id;
                        gdr.TLGreige_KgPerPiece = row.TLGreige_KgPerPiece;
                        gdr.TLGreige_Machine_FK = row.TLGreige_Machine_FK;
                        gdr.TLGreige_Quality_FK = row.TLGreige_Quality_FK;
                        gdr.TLGreige_ROL = row.TLGreige_ROL;
                        gdr.TLGreige_ROQ = row.TLGreige_ROQ;
                        gdr.TLGreige_StockTakeFreq_FK = row.TLGreige_StockTakeFreq_FK;
                        gdr.TLGreige_DskWeight = row.TLGreige_CubicWeight;

                        greigeDataTable.AddTLADM_GriegeRow(gdr);
                    }

                    var mach = context.TLADM_MachineDefinitions.ToList();
                    foreach (var row in mach)
                    {
                        DataSet16.TLADM_MachineDefinitionsRow mdr = machTable.NewTLADM_MachineDefinitionsRow();
                        mdr.MD_Description = row.MD_Description;
                        mdr.MD_MachineCode = row.MD_MachineCode;
                        mdr.MD_Pk = row.MD_Pk;

                        machTable.AddTLADM_MachineDefinitionsRow(mdr);
                    }

                    var stt = context.TLADM_StockTakeFreq.ToList();
                    foreach (var row in stt)
                    {
                        DataSet16.TLADM_StockTakeFreqRow stf = stockTakeTable.NewTLADM_StockTakeFreqRow();
                        stf.STF_Description = row.STF_Description;
                        stf.STF_Period_Weeks = row.STF_Period_Weeks;
                        stf.STF_Pk = row.STF_Pk;
                        stf.STF_ShortCode = row.STF_ShortCode;

                        stockTakeTable.AddTLADM_StockTakeFreqRow(stf);
                    }

                }

                ds.Tables.Add(fabWeightTable);
                ds.Tables.Add(fabWidthTable);
                ds.Tables.Add(greigeQualTable);
                ds.Tables.Add(greigeDataTable);
                ds.Tables.Add(machTable);
                ds.Tables.Add(stockTakeTable);

                GreigeItems GI = new GreigeItems();
                GI.SetDataSource(ds);
                crystalReportViewer1.ReportSource = GI;
            
            }
            else if (_RepNo == 16)  // Tradelink Knitting Yarn Stock on Hand Commission / 3rd Party
            {
                DataSet ds = new DataSet();
                DataSet17.DataTable1DataTable dataTable1 = new DataSet17.DataTable1DataTable();
                List<TLKNI_YarnOrderPallets> Pallets = new List<TLKNI_YarnOrderPallets>();
              
                using (var context = new TTI2Entities())
                {
                    if (_opts.reportChoice == 1)
                    {
                        Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_CommisionCust && !x.TLKNIOP_PalletAllocated).ToList();
                    }
                    else
                    {
                        Pallets = context.TLKNI_YarnOrderPallets.Where(x => !x.TLKNIOP_CommisionCust && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                    }

                    foreach (var Pallet in Pallets)
                    {
                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        var Whse = context.TLADM_WhseStore.Find(Pallet.TLKNIOP_Store_FK);
                        if (Whse != null)
                        {
                            nr.Store_Code = Whse.WhStore_Code;
                            nr.Store_Description = Whse.WhStore_Description; 
                        }

                        var Yarn = context.TLADM_Yarn.Find(Pallet.TLKNIOP_YarnType_FK);
                        if (Yarn != null)
                        {
                            nr.Text_Count = Yarn.YA_TexCount;
                            nr.Yarn_Description = Yarn.YA_Description;
                            nr.Yarn_Twist = Yarn.YA_Twist;
                            nr.Yarn_Identification = Yarn.YA_ConeColour;

                        }

                        nr.Pallet_No = Pallet.TLKNIOP_PalletNo;
                        nr.Cones = Pallet.TLKNIOP_Cones + Pallet.TLKNIOP_ConesReturned - Pallet.TLKNIOP_ConesReserved;
                        nr.Weight = Pallet.TLKNIOP_NettWeight - Pallet.TLKNIOP_NettWeightConsummed - Pallet.TLKNIOP_NettWeightReserved;
                        if(Pallet.TLKNIOP_CommisionCust)
                            nr.Customer = context.TLADM_CustomerFile.Find(Pallet.TLKNIOP_CommissionCustomer_FK).Cust_Description;
                        nr.Date_Received = (DateTime)Pallet.TLKNIOP_DatePacked;

                        dataTable1.AddDataTable1Row(nr);
                    }
                   
                }
                   

                ds.Tables.Add(dataTable1);

                YarnSOHByStore soh = new YarnSOHByStore();
                soh.SetDataSource(ds);
                crystalReportViewer1.ReportSource = soh;
               

            }
            else if (_RepNo == 17)  // Knit Orders in progress 
            {
                DataSet ds = new DataSet();
                DataSet18.DataTable1DataTable dataTable1 = new DataSet18.DataTable1DataTable();
                DataSet18.TLADM_GriegeDataTable greigeTable = new DataSet18.TLADM_GriegeDataTable();
                DataSet18.TLADM_MachineDefinitionsDataTable machTable = new DataSet18.TLADM_MachineDefinitionsDataTable();
                DataSet18.TLKNI_OrderDataTable orderTable = new DataSet18.TLKNI_OrderDataTable();

                using (var context = new TTI2Entities())
                {
                    var ord = context.TLKNI_Order.Where(x=>!x.KnitO_Closed).ToList();
                    foreach (var row in ord)
                    {
                        DataSet18.TLKNI_OrderRow or = orderTable.NewTLKNI_OrderRow();
                        or.KnitO_Machine_FK = row.KnitO_Machine_FK;
                        or.KnitO_NoOfPieces = row.KnitO_NoOfPieces;
                        or.KnitO_Weight = row.KnitO_Weight;
                        or.KnitO_OrderDate = row.KnitO_OrderDate;
                        or.KnitO_Product_FK = row.KnitO_Product_FK;
                        or.KnitO_OrderNumber = row.KnitO_OrderNumber;
                        or.KnitO_Pk = row.KnitO_Pk;

                        orderTable.AddTLKNI_OrderRow(or);

                        DataSet18.DataTable1Row dr = dataTable1.NewDataTable1Row();
                        dr.PrimaryKey = row.KnitO_Pk;
                        if(context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_weight > 0).Count() != 0)
                           dr.KnittedToDate = context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_weight > 0).Sum(x=>x.GreigeP_weight);
                        else
                            dr.KnittedToDate = 0;

                        dr.BalanceToBeKnitted = row.KnitO_Weight - dr.KnittedToDate;
                        dr.YarnAllocated = 0.00M;

                        var AllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == row.KnitO_Pk && x.TLKYT_TranType == 1).FirstOrDefault();
                        if (AllocTrans != null)
                        {
                            var YOP = context.TLKNI_YarnOrderPallets.Find(AllocTrans.TLKYT_YOP_FK);
                            if (YOP != null)
                            {
                                var YarnType = context.TLADM_Yarn.Find(YOP.TLKNIOP_YarnType_FK);
                                if (YarnType != null)
                                {
                                    dr.YarnType = YarnType.YA_Description;
                                    dr.YarnText = YarnType.YA_TexCount;
                                }
                            }
                            dr.YarnAllocated = AllocTrans.TLKYT_NettWeight;
                        }
                       
                        decimal WeightProduced = 0.00M;
                                                   
                        if(context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_weight > 0).Count() != 0)
                             WeightProduced = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_weight > 0).Sum(x => (int?) x.GreigeP_weight) ?? 0;
                        
                        dr.YarnInProgress = dr.YarnAllocated - WeightProduced;

                        dataTable1.AddDataTable1Row(dr);

                    }

                    var gr = context.TLADM_Griege.ToList();
                    foreach (var row in gr)
                    {
                        DataSet18.TLADM_GriegeRow grr = greigeTable.NewTLADM_GriegeRow();
                        grr.TLGreige_Description = row.TLGreige_Description;
                        grr.TLGreige_Id = row.TLGreige_Id;
                        grr.TLGreige_DskWeight = row.TLGreige_CubicWeight;
                        greigeTable.AddTLADM_GriegeRow(grr);
                    }

                    var mach = context.TLADM_MachineDefinitions.ToList();
                    foreach (var row in mach)
                    {
                        DataSet18.TLADM_MachineDefinitionsRow macr = machTable.NewTLADM_MachineDefinitionsRow();
                        macr.MD_Description = row.MD_Description;
                        macr.MD_MachineCode = row.MD_MachineCode;
                        macr.MD_Pk = row.MD_Pk;

                        machTable.AddTLADM_MachineDefinitionsRow(macr);
                    }

                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(greigeTable);
                ds.Tables.Add(machTable);
                ds.Tables.Add(orderTable);

                KnittingWip kwip = new KnittingWip();
                kwip.SetDataSource(ds);
                crystalReportViewer1.ReportSource = kwip;

            }
            else if (_RepNo == 18)  // Process Loss 
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                DataSet19.TLKNI_OrderDataTable orderTable = new DataSet19.TLKNI_OrderDataTable();
             
                DataTable dt = new DataTable();
                dt.Columns.Add("PrimaryKey", typeof(Int32));      // 0
                dt.Columns.Add("OrderNumber", typeof(Int32));     // 1
                dt.Columns.Add("DateClosed", typeof(DateTime));   // 2
                dt.Columns.Add("Machine", typeof(String));        // 3
                dt.Columns.Add("product", typeof(String));        // 4 
                dt.Columns.Add("OrderQty", typeof(decimal));      // 5
                dt.Columns.Add("TotalKnitted", typeof(decimal));  // 6
                dt.Columns.Add("yarnConsummed", typeof(decimal)); // 7
                dt.Columns.Add("ProcessLoss", typeof(decimal));   // 8
                dt.Columns.Add("YarnType", typeof(string));       // 9
                dt.Columns.Add("YarnTex", typeof(decimal));       //10
                dt.Columns.Add("YarnTwist", typeof(decimal));     //11


                KOProcessLoss PLoss = null;
                
                using (var context = new TTI2Entities())
                {
                    var data = context.TLKNI_Order.Where(x => x.KnitO_Closed && x.KnitO_ClosedDate >= _opts.fromDate && x.KnitO_ClosedDate <= _opts.toDate && x.KnitO_YarnAssigned).ToList();
                    foreach (var row in data)
                    {
                        DataRow dr =  dt.NewRow();
                        dr[0] = row.KnitO_Pk; 
                        dr[1]  = row.KnitO_OrderNumber;
                        dr[2]  = row.KnitO_ClosedDate;

                        var mach = context.TLADM_MachineDefinitions.Find(row.KnitO_Machine_FK);
                        if(mach != null)
                           dr[3] = mach.MD_MachineCode;

                        var product = context.TLADM_Griege.Find(row.KnitO_Product_FK);
                        if (product != null)
                            dr[4] = product.TLGreige_Description;

                        dr[5] = row.KnitO_Weight;
                        dr[6] = dr[7] = dr[8] = 0.00M;
                        
                        var GreigeProduced = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == row.KnitO_Pk && x.GreigeP_weight > 0).ToList().Sum(x => (decimal?)x.GreigeP_weight ?? 0.00M);
                        var AllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == row.KnitO_Pk).ToList();
                        if (AllocTrans.Count > 0)
                        {
                            //====================================================
                            // There are 3 Transaction Types to consider
                            //=============================================================
                            // Tran Type 1    ---- Yarn that has been allocated to an Order 
                            // Tran Type 2    ---- Yarn that has been measured that will be returned to the appropriate store. While Positive in the file needs to negative for the calculation
                            // Tran Type 3    ---- Negative Yarn That has been given to another Yarn Order
                            //                ---- Positive if This order has received additional Yarn from another order         
                            //=================================================================================================
                            var YarnIssued = AllocTrans.Where(x=>x.TLKYT_TranType == 1).Sum(x => (decimal?)x.TLKYT_NettWeight) ?? 0.00M;
                            YarnIssued += AllocTrans.Where(x => x.TLKYT_TranType == 2).Sum(x => (decimal ?)x.TLKYT_NettWeight * -1) ?? 0.00M;
                            YarnIssued += AllocTrans.Where(x => x.TLKYT_TranType == 3).Sum(x => (decimal ?)x.TLKYT_NettWeight) ?? 0.00M;
                             
                            if (GreigeProduced != 0 && YarnIssued != 0)
                            {
                                dr[6] = GreigeProduced;
                                dr[7] = YarnIssued;
                                dr[8] = 100 * (GreigeProduced / YarnIssued) - 100;
                            }

                            var YOP = context.TLKNI_YarnOrderPallets.Find(AllocTrans.FirstOrDefault().TLKYT_YOP_FK);
                            if (YOP != null)
                            {
                                var YarnType = context.TLADM_Yarn.Find(YOP.TLKNIOP_YarnType_FK);
                                if (YarnType != null)
                                {
                                    dr[9] = YarnType.YA_YarnType;
                                    dr[10] = YarnType.YA_TexCount;
                                    dr[11] = YarnType.YA_Twist;
                                }
                            }
                            else
                            {
                                dr[9] = "Unknown";
                                dr[10] = 0.00M;
                                dr[11] = 0.00M;
                            }
                        }
                                                
                        dt.Rows.Add(dr);
                    }
                    try
                    {
                        if (dt.Rows.Count != 0)
                        {
                            if (_opts.KnitOrder)
                                dt = dt.Select(null, dt.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.KnitMachines)
                                dt = dt.Select(null, dt.Columns[3].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.GreigeProduct)
                                dt = dt.Select(null, dt.Columns[4].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.ProcessLoss)
                                dt = dt.Select(null, dt.Columns[8].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.YarnType)
                                dt = dt.Select(null, dt.Columns[9].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.YarnTex)
                                dt = dt.Select(null, dt.Columns[10].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    foreach (DataRow rw in dt.Rows)
                    {
                        DataSet19.TLKNI_OrderRow or = orderTable.NewTLKNI_OrderRow();
                        or.KnitO_Pk = Convert.ToInt32(rw[0].ToString());
                        or.KnitO_ClosedDate = Convert.ToDateTime(rw[2].ToString());
                        or.KnitO_Machine = rw[3].ToString();
                        or.KnitO_Product = rw[4].ToString();
                        or.KnitO_Weight =   Convert.ToDecimal(rw[5].ToString());
                        or.KnitO_OrderNumber = Convert.ToInt32(rw[1].ToString());
                        orderTable.AddTLKNI_OrderRow(or);

                        DataSet19.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                        dtr.PrimaryKey = Convert.ToInt32(rw[0].ToString()); 
                        dtr.TotalKnitted = Convert.ToDecimal(rw[6].ToString());
                        dtr.YarnConsumed = Convert.ToDecimal(rw[7].ToString());
                        dtr.ProcessLoss = Convert.ToDecimal(rw[8].ToString());

                        if (rw[9] != null)
                        {
                            dtr.YarnType = rw[9].ToString();
                            if (rw[10] != null && !String.IsNullOrEmpty(rw[10].ToString()))
                                dtr.YarnText = Convert.ToDecimal(rw[10].ToString());
                            else
                                dtr.YarnText = 0.00M;

                            if (rw[11] != null  && !String.IsNullOrEmpty(rw[11].ToString()))
                                dtr.YarnTwist = Convert.ToDecimal(rw[11].ToString());
                            else
                                dtr.YarnTwist = 0.00M;
                        }
                        else
                        {
                            dtr.YarnType = string.Empty;
                            dtr.YarnText = 0.00M;
                            dtr.YarnTwist = 0.00M;
                        }

                        if (_opts.KnitOrder)
                            dtr.SortOrder = "Sorted by Knit Order";
                        else if (_opts.KnitMachines)
                            dtr.SortOrder = "Sorted by Machine";
                        else if (_opts.GreigeProduct)
                            dtr.SortOrder = "Sorted by Product";
                        else if (_opts.ProcessLoss)
                            dtr.SortOrder = "Sorted by Process Loss";
                        else if (_opts.YarnType)
                            dtr.SortOrder = "Sorted by Yarn Type";
                        else if (_opts.YarnTex)
                            dtr.SortOrder = "Sorted by Yarn Tex";

                        dtr.FromDate = _opts.fromDate;
                        dtr.ToDate = _opts.toDate;

                        dataTable1.AddDataTable1Row(dtr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet19.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                    dtr.PrimaryKey = 1;
                    dtr.FromDate = _opts.fromDate;
                    dtr.ToDate = _opts.toDate;
                    dtr.ErrorLog = "No data available";


                    dataTable1.AddDataTable1Row(dtr);

                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(orderTable);

                PLoss = new KOProcessLoss();
                PLoss.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PLoss;

            }
            else if (_RepNo == 19)  // Greige Comm Adjustment 
            {
                DataSet ds = new DataSet();
                DataSet20.TLKNI_GreigeCommisionAdjustmentDataTable adjustTable = new DataSet20.TLKNI_GreigeCommisionAdjustmentDataTable();
                DataSet20.TLKNI_GreigeProductionDataTable gProduction = new DataSet20.TLKNI_GreigeProductionDataTable();
                using (var context = new TTI2Entities())
                {
                    var tt = context.TLKNI_GreigeCommisionAdjustment.Where(x => x.GreigeComAJ_AjustmentNo == _KnitO).ToList();
                    foreach (var row in tt)
                    {
                        DataSet20.TLKNI_GreigeCommisionAdjustmentRow ar = adjustTable.NewTLKNI_GreigeCommisionAdjustmentRow();
                        ar.GreigeComAJ_AjustmentNo = row.GreigeComAJ_AjustmentNo;
                        ar.GreigeComAJ_AmtAdjusted = row.GreigeComAJ_AmtAdjusted;
                        ar.GreigeComAJ_AprovedBy = row.GreigeComAJ_AprovedBy;
                        ar.GreigeComAJ_PieceNo_FK = row.GreigeComAJ_PieceNo_FK;
                        ar.GreigeComAJ_Pk = row.GreigeComAJ_Pk;
                        ar.GreigeComAJ_Reasons = row.GreigeComAJ_Reasons;
                        ar.GreigeComAJ_Strore_FK = row.GreigeComAJ_Strore_FK;
                        ar.GreigeComAJ_TransDate = row.GreigeComAJ_TransDate;
                        ar.GreigeComAJ_GrnNumber = row.GreigeComAJ_GrnNumber;
                        ar.GreigeComAJ_GreigeProduction_FK = row.GreigeComAJ_GreigeProduction_FK;

                        adjustTable.AddTLKNI_GreigeCommisionAdjustmentRow(ar);

                        var GreigeP = context.TLKNI_GreigeProduction.Find(row.GreigeComAJ_GreigeProduction_FK);
                        if (GreigeP != null)
                        {
                            DataSet20.TLKNI_GreigeProductionRow gpr = gProduction.NewTLKNI_GreigeProductionRow();
                            gpr.GreigeP_Greige_Fk = (int)GreigeP.GreigeP_Greige_Fk;
                            gpr.GreigeP_PieceNo = GreigeP.GreigeP_PieceNo;
                            gpr.GreigeP_Pk = GreigeP.GreigeP_Pk;
                            gpr.GreigeP_weightAvail = GreigeP.GreigeP_weightAvail;
                            gpr.GreigeP_NettWeight = GreigeP.GreigeP_weight;
                            gpr.GreigeP_ProductDescription = context.TLADM_Griege.Find(GreigeP.GreigeP_Greige_Fk).TLGreige_Description;

                            gProduction.AddTLKNI_GreigeProductionRow(gpr);
                        }
                    }
                }

                ds.Tables.Add(adjustTable);
                ds.Tables.Add(gProduction);
            
                GreigeComAdjust PLoss = new  GreigeComAdjust();
                PLoss.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PLoss;
            }
            else if (_RepNo == 20)  // Yarn Stock on hand own Yarn 
            {
                DataSet ds = new DataSet();
                DataSet21.TLADM_WhseStoreDataTable whTable = new DataSet21.TLADM_WhseStoreDataTable();
                DataSet21.TLADM_YarnDataTable yarnTable = new DataSet21.TLADM_YarnDataTable();
                DataSet21.TLSPN_YarnOrderPalletsDataTable palletsTable = new DataSet21.TLSPN_YarnOrderPalletsDataTable();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {
                    var Pallets = context.TLKNI_YarnOrderPallets.Where(x=>x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).OrderBy(x=>new {x.TLKNIOP_YarnOrder_FK, x.TLKNIOP_PalletNo}).ToList();
                    foreach (var Pallet in Pallets)
                    {
                        
                        DataSet21.TLSPN_YarnOrderPalletsRow pr = palletsTable.NewTLSPN_YarnOrderPalletsRow();
                        pr.YarnOP_Grade = Pallet.TLKNIOP_Grade;
                        pr.YarnOP_Pk = Pallet.TLKNIOP_Pk;
                        pr.YarnOP_NoOfConesSpun = 0; //row.TLKNIOP_Cones + row.TLKNIOP_ConesReturned - row.TLKNIOP_ConesReserved;
                        pr.YarnOP_NettWeight = core.CalculatePalletNett(Pallet);
                        pr.YarnOP_PalletNo = Pallet.TLKNIOP_PalletNo;
                        pr.YarnOP_Store_FK = Pallet.TLKNIOP_Store_FK;
                        pr.YarnOP_YarnType_FK = Pallet.TLKNIOP_YarnType_FK;
                        if (Pallet.TLKNIOP_YarnOrder_FK > 0)
                        {
                           pr.YarnOP_YarnOrder_FK = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK).YarnO_OrderNumber.ToString();
                        }

                        try
                        {
                           palletsTable.AddTLSPN_YarnOrderPalletsRow(pr);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                      
                    }

                    var whs = context.TLADM_WhseStore.ToList();
                    foreach (var row in whs)
                    {
                        DataSet21.TLADM_WhseStoreRow wr = whTable.NewTLADM_WhseStoreRow();
                        wr.WhStore_Code = row.WhStore_Code;
                        wr.WhStore_Description = row.WhStore_Description;
                        wr.WhStore_Id = row.WhStore_Id;

                        whTable.AddTLADM_WhseStoreRow(wr);
                    }

                    var yt = context.TLADM_Yarn.ToList();
                    foreach (var row in yt)
                    {
                        DataSet21.TLADM_YarnRow yyr = yarnTable.NewTLADM_YarnRow();
                        yyr.YA_ConeColour = row.YA_ConeColour;
                        yyr.YA_Description = row.YA_Description;
                        yyr.YA_Id = row.YA_Id;
                        yyr.YA_TexCount = row.YA_TexCount;
                        yyr.YA_Twist = row.YA_Twist;
                        yyr.YA_YarnType = row.YA_YarnType;

                        yarnTable.AddTLADM_YarnRow(yyr);
                    }

                }

                ds.Tables.Add(whTable);
                ds.Tables.Add(yarnTable);
                ds.Tables.Add(palletsTable);

                YarnSOHOwnYarn PLoss = new YarnSOHOwnYarn();
                PLoss.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PLoss;
            }
            else if (_RepNo == 21)  // C Grade report for period 1st September Yarn Stock on hand own Yarn 
            {
                DataSet ds = new DataSet();
                DataSet22.DataTable1DataTable dataTable1 = new DataSet22.DataTable1DataTable();
                DataRow dr;

                DataTable dt = new DataTable();
                dt.Columns.Add("PrimaryKey", typeof(Int32));       // 0
                dt.Columns.Add("Machine", typeof(string));         // 1
                dt.Columns.Add("PieceNo", typeof(string));         // 2
                dt.Columns.Add("Weight", typeof(Decimal));         // 3
                dt.Columns.Add("Operator", typeof(String));        // 4 
                dt.Columns.Add("YarnOrder", typeof(int));          // 5
                dt.Columns.Add("SpinMachine", typeof(String));     // 6
                dt.Columns.Add("yarnContract", typeof(decimal));   // 7
                dt.Columns.Add("FromDate", typeof(DateTime));      // 8
                dt.Columns.Add("ToDate", typeof(DateTime));        // 9
                dt.Columns.Add("SortOrder", typeof(string));       // 10

                using (var context = new TTI2Entities())
                {
                    var data = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PDate >= _opts.fromDate && x.GreigeP_PDate <= _opts.toDate && x.GreigeP_Grade.Contains("C") && x.GreigeP_Captured).ToList();
                    foreach (var row in data)
                    {
                        dr = dt.NewRow();

                        dr[0] = row.GreigeP_Pk;

                        var KO = context.TLKNI_Order.Find(row.GreigeP_KnitO_Fk);
                        if (KO != null)
                        {
                            dr[5] = KO.KnitO_OrderNumber;

                            if (KO.KnitO_YarnO_FK != null)
                            {
                                var YO = context.TLSPN_YarnOrder.Find(KO.KnitO_YarnO_FK);
                                if (YO != null)
                                {
                                    var SpinMachine = context.TLADM_MachineDefinitions.Find(YO.Yarno_MachNo_FK);
                                    if (SpinMachine != null)
                                        dr[6] = SpinMachine.MD_MachineCode;
                                }
                            }

                            dr[1] = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK).MD_Description;
                        }

                        dr[2] = row.GreigeP_PieceNo;
                        dr[3] = row.GreigeP_weight;

                        if (row.GreigeP_Operator_FK != null)
                        {
                            dr[4] = context.TLADM_MachineOperators.Find(row.GreigeP_Operator_FK).MachOp_Description;
                        }

                        dr[7] = "0";
                        dr[8] = _opts.fromDate;
                        dr[9] = _opts.toDate;

                        if (_opts.K12KnitMachine)
                            dr[10] = "Sort By Knit Machine";
                        else if (_opts.K12Operator)
                            dr[10] = "Sort By Operator";
                        else if (_opts.K12PieceNo)
                            dr[10] = "Sort By Piece No";
                        else if (_opts.K12Spinning)
                            dr[10] = "Sort By Spinning";
                        else if (_opts.K12YarnOrder)
                            dr[10] = "Sort By Yarn Order";
                        dt.Rows.Add(dr);
                    }

                    try
                    {
                        if (dt.Rows.Count != 0)
                        {
                            if (_opts.K12KnitMachine)
                                dt = dt.Select(null, dt.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.K12Operator)
                                dt = dt.Select(null, dt.Columns[4].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.K12PieceNo)
                                dt = dt.Select(null, dt.Columns[2].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.K12Spinning)
                                dt = dt.Select(null, dt.Columns[6].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_opts.K12YarnOrder)
                                dt = dt.Select(null, dt.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                foreach (DataRow rw in dt.Rows)
                {
                    DataSet22.DataTable1Row nr = dataTable1.NewDataTable1Row();

                    nr.Machine = rw[1].ToString();
                    nr.PieceNo = rw[2].ToString();
                    nr.Weight = Convert.ToDecimal(rw[3].ToString());
                    nr.Operator = rw[4].ToString();
                    nr.YarnOrder = Convert.ToInt32(rw[5].ToString());
                    nr.SpinMachine = rw[6].ToString();
                    nr.YarnContract = rw[7].ToString();
                    nr.FromDate = Convert.ToDateTime(rw[8].ToString());
                    nr.ToDate = Convert.ToDateTime(rw[9].ToString());
                    nr.SortOrder = rw[10].ToString();

                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);

                if (_opts.K12KnitMachine)
                {
                    CGradeByMachine CGrade = new CGradeByMachine();
                    CGrade.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CGrade;
                }
                else if (_opts.K12Operator)
                {
                    CGradeByOperator CGrade = new CGradeByOperator();
                    CGrade.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CGrade;
                }
                else if (_opts.K12YarnOrder)
                {
                    CGradeByYarnOrder CGrade = new CGradeByYarnOrder();
                    CGrade.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CGrade;
                }
                else if (_opts.K12Spinning)
                {
                    CGradeBySpinMachine CGrade = new CGradeBySpinMachine();
                    CGrade.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CGrade;
                }

            }
            else if (_RepNo == 22)  // QA Reports  
            {
                DataSet ds = new DataSet();
                DataSet23.DataTable1DataTable dataTable1 = new DataSet23.DataTable1DataTable();
                DataSet23.DataTable2DataTable dataTable2 = new DataSet23.DataTable2DataTable();

                _Repo = new KnitRepository();

                IList<TLADM_QualityDefinition> Reasons = null;
                string[][] ColumnNames = null;
              
                if (_opts.K8rbQA1)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text2", "Greige Product"},
                        new string[] {"Text3", "Machine"},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
                    };
                }
                else if (_opts.K8rbQA2)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text2", "Greige Product"},
                        new string[] {"Text3", "."},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
                    };
                }
                else if (_opts.K8rbQA3)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text2", "Knitting Machine"},
                        new string[] {"Text3", "."},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
                    };
                }
                else if (_opts.K8rbQA4)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text2", "Operator"},
                        new string[] {"Text3", "."},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
                    };
                }
                else if (_opts.K8rbQA7)
                {
                    ColumnNames = new string[][]
                   {   new string[] {"Text2", "Merge Detail"},
                        new string[] {"Text3", "."},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
                   };
                }
                else if(_opts.K8rbQA8)
                {
                    ColumnNames = new string[][]
               {   new string[] {"Text2", "Merge Detail"},
                        new string[] {"Text3", "."},
                        new string[] {"Text4", "Total Pieces Inspected"},
                        new string[] {"Text5", "A Grade Pieces"},
                        new string[] {"Text18", "A Pieces With Warning"},
                        new string[] {"Text6", "B Grade Pieces"},
                        new string[] {"Text7", "C Grade Pieces"},
                        new string[] {"Text16", "Not Inspected"}
               };
                }
                using (var context = new TTI2Entities())
                {
                    var Data = _Repo.QAGreigeProduction(_QueryParms);

                    if (_opts.ExcludeCommission)
                    {
                        Data = Data.Where(x => !x.GreigeP_CommisionCust);
                    }

                    var Depts = context.TLADM_Departments.Where(x=>x.Dep_ShortCode == "KNIT").FirstOrDefault();
                    if (Depts != null)
                    {
                        Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();
                    }

                    if (_opts.K8rbQA1)
                    {
                        var Grps = Data.GroupBy(x => new { x.GreigeP_Greige_Fk, x.GreigeP_Machine_FK }).ToList();
                        foreach (var prodGrp in Grps)
                        {
                           DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                           int Pk = (int)prodGrp.FirstOrDefault().GreigeP_Greige_Fk;
                           var Greige = context.TLADM_Griege.Find(Pk);
                            if(Greige != null) 
                                nr.DataColumn1 = Greige.TLGreige_Description;
                           Pk = (int)prodGrp.FirstOrDefault().GreigeP_Machine_FK;
                           var Machine = context.TLADM_MachineDefinitions.Find(Pk);
                           if(Machine != null)
                                nr.DataColumn2 = Machine.MD_MachineCode;
                           nr.DataColumn3 = prodGrp.Count();
                           nr.DataColumn4 = prodGrp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                           nr.DataColumn17 = prodGrp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                           nr.DataColumn5 = prodGrp.Where(x => x.GreigeP_Grade == "B").Count();
                           nr.DataColumn6 = prodGrp.Where(x => x.GreigeP_Grade == "C").Count();
                           nr.DataColumn7 = prodGrp.Sum(x => x.GreigeP_Meas1);
                           nr.DataColumn8 = prodGrp.Sum(x => x.GreigeP_Meas2);
                           nr.DataColumn9 = prodGrp.Sum(x => x.GreigeP_Meas3);
                           nr.DataColumn10 = prodGrp.Sum(x => x.GreigeP_Meas4);
                           nr.DataColumn11 = prodGrp.Sum(x => x.GreigeP_Meas5);
                           nr.DataColumn12 = prodGrp.Sum(x => x.GreigeP_Meas6);
                           nr.DataColumn13 = prodGrp.Sum(x => x.GreigeP_Meas7);
                           nr.DataColumn14 = prodGrp.Sum(x => x.GreigeP_Meas8);
                           nr.DataColumn15 = prodGrp.Where(x => !x.GreigeP_Inspected).Count();
                           nr.PrimaryKey = 1;
                           dataTable1.AddDataTable1Row(nr);
                                   
                       }
                    }
                    else if(_opts.K8rbQA2)
                    {
                            var Grps = Data.GroupBy(x => new { x.GreigeP_Greige_Fk}).ToList();
                            foreach (var prodGrp in Grps)
                            {
                                DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();

                                var Pk = prodGrp.FirstOrDefault().GreigeP_Greige_Fk;
                                nr.DataColumn1 = context.TLADM_Griege.Find(Pk).TLGreige_Description;
                          

                                nr.DataColumn3 = prodGrp.Count();
                                nr.DataColumn4 = prodGrp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                                nr.DataColumn17 = prodGrp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                                nr.DataColumn5 = prodGrp.Where(x => x.GreigeP_Grade == "B").Count();
                                nr.DataColumn6 = prodGrp.Where(x => x.GreigeP_Grade == "C").Count();
                                nr.DataColumn7 = prodGrp.Sum(x => x.GreigeP_Meas1);
                                nr.DataColumn8 = prodGrp.Sum(x => x.GreigeP_Meas2);
                                nr.DataColumn9 = prodGrp.Sum(x => x.GreigeP_Meas3);
                                nr.DataColumn10 = prodGrp.Sum(x => x.GreigeP_Meas4);
                                nr.DataColumn11 = prodGrp.Sum(x => x.GreigeP_Meas5);
                                nr.DataColumn12 = prodGrp.Sum(x => x.GreigeP_Meas6);
                                nr.DataColumn13 = prodGrp.Sum(x => x.GreigeP_Meas7);
                                nr.DataColumn14 = prodGrp.Sum(x => x.GreigeP_Meas8);
                                nr.DataColumn15 = prodGrp.Where(x=>!x.GreigeP_Inspected).Count();
                                nr.PrimaryKey = 1;
                                dataTable1.AddDataTable1Row(nr);
                 
                            }
                     }
                     else if (_opts.K8rbQA3)
                     {
                         var Grps = Data.GroupBy(x => new { x.GreigeP_Machine_FK}).ToList();
                         foreach (var prodGrp in Grps)
                         {
                             DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();

                             var Pk = prodGrp.FirstOrDefault().GreigeP_Machine_FK;
                             var Machine = context.TLADM_MachineDefinitions.Find(Pk);
                             if(Machine != null)
                                  nr.DataColumn1 = Machine.MD_MachineCode;
                             nr.DataColumn3 = prodGrp.Count();
                             nr.DataColumn4 = prodGrp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                             nr.DataColumn17 = prodGrp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                             nr.DataColumn5 = prodGrp.Where(x => x.GreigeP_Grade == "B").Count();
                             nr.DataColumn6 = prodGrp.Where(x => x.GreigeP_Grade == "C").Count();
                             nr.DataColumn7 = prodGrp.Sum(x => x.GreigeP_Meas1);
                             nr.DataColumn8 = prodGrp.Sum(x => x.GreigeP_Meas2);
                             nr.DataColumn9 = prodGrp.Sum(x => x.GreigeP_Meas3);
                             nr.DataColumn10 = prodGrp.Sum(x => x.GreigeP_Meas4);
                             nr.DataColumn11 = prodGrp.Sum(x => x.GreigeP_Meas5);
                             nr.DataColumn12 = prodGrp.Sum(x => x.GreigeP_Meas6);
                             nr.DataColumn13 = prodGrp.Sum(x => x.GreigeP_Meas7);
                             nr.DataColumn14 = prodGrp.Sum(x => x.GreigeP_Meas8);
                             nr.DataColumn15 = prodGrp.Where(x=>!x.GreigeP_Inspected).Count() ;
                             nr.PrimaryKey = 1;
                             dataTable1.AddDataTable1Row(nr);

                         }
                      }
                      else if (_opts.K8rbQA4)
                      {
                            var Grps = Data.GroupBy(x => x.GreigeP_Operator_FK).ToList();
                            foreach (var Grp in Grps)
                            {
                                 DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            
                                 var OpKey = Grp.FirstOrDefault().GreigeP_Operator_FK;
                                 nr.DataColumn1 = context.TLADM_MachineOperators.Find(OpKey).MachOp_Description;
                                 
                                 nr.DataColumn3 = Grp.Count();
                                 nr.DataColumn4 = Grp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                                 nr.DataColumn17 = Grp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                                 nr.DataColumn5 = Grp.Where(x => x.GreigeP_Grade == "B").Count();
                                 nr.DataColumn6 = Grp.Where(x => x.GreigeP_Grade == "C").Count();
                                 nr.DataColumn7 = Grp.Sum(x => x.GreigeP_Meas1);
                                 nr.DataColumn8 = Grp.Sum(x => x.GreigeP_Meas2);
                                 nr.DataColumn9 = Grp.Sum(x => x.GreigeP_Meas3);
                                 nr.DataColumn10 = Grp.Sum(x => x.GreigeP_Meas4);
                                 nr.DataColumn11 = Grp.Sum(x => x.GreigeP_Meas5);
                                 nr.DataColumn12 = Grp.Sum(x => x.GreigeP_Meas6);
                                 nr.DataColumn13 = Grp.Sum(x => x.GreigeP_Meas7);
                                 nr.DataColumn14 = Grp.Sum(x => x.GreigeP_Meas8);
                                 nr.DataColumn15 = Grp.Where(x=>!x.GreigeP_Inspected).Count();
                                 nr.PrimaryKey = 1;
                                 dataTable1.AddDataTable1Row(nr);
                            }
                      }
                      else if (_opts.K8rbQA7)
                      {
                        var Grps = Data.GroupBy(x => x.GreigeP_MergeDetail).ToList();
                        foreach (var Grp in Grps)
                        {
                            DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();

                            nr.DataColumn1 = Grp.FirstOrDefault().GreigeP_MergeDetail;

                            nr.DataColumn3 = Grp.Count();
                            nr.DataColumn4 = Grp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                            nr.DataColumn17 = Grp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                            nr.DataColumn5 = Grp.Where(x => x.GreigeP_Grade == "B").Count();
                            nr.DataColumn6 = Grp.Where(x => x.GreigeP_Grade == "C").Count();
                            nr.DataColumn7 = Grp.Sum(x => x.GreigeP_Meas1);
                            nr.DataColumn8 = Grp.Sum(x => x.GreigeP_Meas2);
                            nr.DataColumn9 = Grp.Sum(x => x.GreigeP_Meas3);
                            nr.DataColumn10 = Grp.Sum(x => x.GreigeP_Meas4);
                            nr.DataColumn11 = Grp.Sum(x => x.GreigeP_Meas5);
                            nr.DataColumn12 = Grp.Sum(x => x.GreigeP_Meas6);
                            nr.DataColumn13 = Grp.Sum(x => x.GreigeP_Meas7);
                            nr.DataColumn14 = Grp.Sum(x => x.GreigeP_Meas8);
                            nr.DataColumn15 = Grp.Where(x => !x.GreigeP_Inspected).Count();
                            nr.PrimaryKey = 1;
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                    else if (_opts.K8rbQA8)
                    {
                        var Grps = Data.GroupBy(x => x.GreigeP_MergeDetail).ToList();
                        foreach (var Grp in Grps)
                        {
                            foreach (var Item in Grp)
                            {
                                DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                
                                nr.DataColumn1 = Grp.FirstOrDefault().GreigeP_MergeDetail;
                                nr.DataColumn2 = Item.GreigeP_PieceNo;
                                if (Item.GreigeP_Inspected)
                                    nr.DataColumn3 = 1;
                                else
                                    nr.DataColumn3 = 0;

                                if (Item.GreigeP_Grade == "A" && !Item.GreigeP_WarningMessage)
                                    nr.DataColumn4 = 1; // Grp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();
                                else
                                    nr.DataColumn4 = 0;

                                if (Item.GreigeP_Grade == "A" && Item.GreigeP_WarningMessage)
                                    nr.DataColumn17 = 1; 
                                else
                                    nr.DataColumn17 =0; 
                                if (Item.GreigeP_Grade == "B")
                                    nr.DataColumn5 = 1; 
                                else
                                    nr.DataColumn5 = 0;
                                if (Item.GreigeP_Grade == "C")
                                    nr.DataColumn6 = 1; 
                                else 
                                    nr.DataColumn6 = 0;

                                nr.DataColumn7 = Item.GreigeP_Meas1;
                                nr.DataColumn8 = Item.GreigeP_Meas2;
                                nr.DataColumn9 = Item.GreigeP_Meas3;
                                nr.DataColumn10 = Item.GreigeP_Meas4;
                                nr.DataColumn11 = Item.GreigeP_Meas5;
                                nr.DataColumn12 = Item.GreigeP_Meas6;
                                nr.DataColumn13 = Item.GreigeP_Meas7;
                                nr.DataColumn14 = Item.GreigeP_Meas8;
                                if (!Item.GreigeP_Inspected)
                                    nr.DataColumn15 = 1;
                                else
                                    nr.DataColumn15 = 0;
                                nr.PrimaryKey = 1;
                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                    }
                }

                DataSet23.DataTable2Row hr = dataTable2.NewDataTable2Row();
                
                hr.PrimaryKey = 1;
                hr.fromDate = _QueryParms.FromDate;
                hr.toDate = _QueryParms.ToDate;

                if (_opts.K8rbQA1)
                    hr.Title = "QA details for each greige product by knitting machine";
                else if (_opts.K8rbQA2)
                    hr.Title = "QA details for each greige product (No machines)";
                else if (_opts.K8rbQA3)
                    hr.Title = "QA details for each knitting machine";
                else if (_opts.K8rbQA4)
                    hr.Title = "QA details for each operator";
                else if (_opts.K8rbQA7)
                    hr.Title = "QA details for each Merge Detail (Summarised)";
                else if (_opts.K8rbQA8)
                    hr.Title = "QA details for each Merge Detail (Detail By Piece Number)";
                dataTable2.AddDataTable2Row(hr);
                if (dataTable1.Rows.Count == 0)
                {
                    DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.PrimaryKey = 1;

                    nr.ErrorLog = "No data available";
                    nr.DataColumn7 = 0;
                    nr.DataColumn8 = 0;
                    nr.DataColumn9 = 0;
                    nr.DataColumn10 = 0;
                    nr.DataColumn11 = 0;
                    nr.DataColumn12 = 0;
                    nr.DataColumn13 = 0;
                    nr.DataColumn14 = 0;
                    nr.DataColumn17 = 0;

                    dataTable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                repTest xtst = new repTest();
                if(_opts.QASummary)
                    xtst.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;
    
                IEnumerator ie = xtst.Section2.ReportObjects.GetEnumerator();
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

                foreach (var Reason in Reasons)
                {
                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;
                                if (String.IsNullOrEmpty(to.Text))
                                {
                                    to.Text = Reason.QD_Description;
                                    break;
                                }
                            }
                        }
                    }
                    catch (System.ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else if (_RepNo == 23)  // Greige Produced for TradeLink  
            {
                DataSet ds = new DataSet();
                DataSet24.DataTable1DataTable dataTable1 = new DataSet24.DataTable1DataTable();
                DataSet24.DataTable2DataTable dataTable2 = new DataSet24.DataTable2DataTable();
                string[][] ColumnNames = null;

                IList<TLKNI_GreigeProduction> ProdGroups = null;

                _Repo = new KnitRepository();

                if (_opts.K7rbQA1)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text3", "Greige Product"},
                        new string[] {"Text4", "Machine"},
                        new string[] {"Text5", "Total Pieces Knitted"},
                        new string[] {"Text6", "Total Pieces Knitted  (Kg)"},
                        new string[] {"Text7", "A Grade Pieces(kg) Knitted"},
                        new string[] {"Text13", "A Grade Pieces(kg) with Warning"},
                        new string[] {"Text8", "B Grade Pieces(kg) Knitted"},
                        new string[] {"Text9", "C Grade Pieces(kg) Knitted"},
                        new string[] {"Text14", "Dsk Variance %"}
                    };
                }
                else if (_opts.K7rbQA2)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text3", "Greige Product"},
                        new string[] {"Text4", "."},
                        new string[] {"Text5", "Total Pieces Knitted"},
                        new string[] {"Text6", "Total Pieces Knitted (Kg)"},
                        new string[] {"Text7", "A Grade Pieces(kg) Knitted"},
                        new string[] {"Text13", "A Grade Pieces(kg) with Warning"},
                        new string[] {"Text8", "B Grade Pieces(kg) Knitted"},
                        new string[] {"Text9", "C Grade Pieces(kg) Knitted"},
                        new string[] {"Text14", "Dsk Variance %"}
                    };
                }
                else if (_opts.K7rbQA3)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text3", "Operator"},
                        new string[] {"Text4", "."},
                        new string[] {"Text5", "Total Pieces Knitted"},
                        new string[] {"Text6", "Total Pieces Knitted (kg)"},
                        new string[] {"Text7", "A Grade Pieces(kg) Knitted"},
                        new string[] {"Text13", "A Grade Pieces(kg) with Warning"},
                        new string[] {"Text8", "B Grade Pieces(kg) Knitted"},
                        new string[] {"Text9", "C Grade Pieces(kg) Knitted"},
                        new string[] {"Text14", "Dsk Variance %"}
                    };
                }

                using (var context = new TTI2Entities())
                {
                    var Data = _Repo.GreigeProduction(_QueryParms); // context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PDate >= _opts.fromDate && x.GreigeP_PDate <= _opts.toDate).ToList();

                    if (Data.Count() == 0)
                    {
                        DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.DataColumn1 = "No Data found for selection";
                        dataTable1.AddDataTable1Row(nr);
                    }
                    else
                    {
                        if (_opts.K7rbQA1)
                        {
                            ProdGroups = Data.Where(x => !x.GreigeP_CommisionCust).ToList();
                            foreach (var Greige in context.TLADM_Griege)
                            {
                                var GProduced = ProdGroups.Where(x => x.GreigeP_Greige_Fk == Greige.TLGreige_Id).ToList();
                                // By Quality, Machine
                                //-----------------------------------------------------------
                            
                                if (GProduced.Count == 0)
                                {
                                    DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                    nr.DataColumn1 = Greige.TLGreige_Description;
                                    nr.DataColumn2 = "Not Applicable";
                                    nr.DataColumn3 = 0;
                                    nr.DataColumn4 = 0.00M;
                                    nr.DataColumn5 = 0.0M;
                                    nr.DataColumn6 = 0.0M;
                                    nr.DataColumn7 = 0.0M;
                                    nr.DataColumn8 = 0.0M;
                                    nr.DataColumn9 = 0.00M;
                                    nr.PrimaryKey = 1;

                                    if (nr.DataColumn3 == 0)
                                        continue;

                                    dataTable1.AddDataTable1Row(nr);
                                }
                                else
                                {
                                    var MachineGroup = GProduced.GroupBy(x => x.GreigeP_Machine_FK);
                                    foreach(var mGroup in MachineGroup)
                                    {
                                        DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                        nr.DataColumn1 = Greige.TLGreige_Description;
                                        var Pk = mGroup.FirstOrDefault().GreigeP_Machine_FK;
                                        var MachDet = context.TLADM_MachineDefinitions.Find(Pk);
                                        if (MachDet != null)
                                            nr.DataColumn2 = MachDet.MD_MachineCode;
                                        else
                                            nr.DataColumn2 = "Unknown Mach";

                                        nr.DataColumn3 = mGroup.Count();
                                        nr.DataColumn4 = mGroup.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                                        nr.DataColumn5 = mGroup.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                        nr.DataColumn8 = mGroup.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                        nr.DataColumn6 = mGroup.Where(x => x.GreigeP_Grade == "B").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                        nr.DataColumn7 = mGroup.Where(x => x.GreigeP_Grade == "C").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                        nr.DataColumn9 = mGroup.Average(x => x.GreigeP_VarianceDiskWeight);
                                        nr.PrimaryKey = 1;
                                        if (nr.DataColumn3 == 0)
                                            continue;

                                        dataTable1.AddDataTable1Row(nr);
                                    }
                                }
                             

                                
                            }
                        }
                        else if (_opts.K7rbQA2)
                        {
                           
                            var MachGroups = Data.Where(x=>!x.GreigeP_CommisionCust).GroupBy(x => x.GreigeP_Machine_FK).ToList();
                            foreach (var Group in MachGroups)
                            {
                                DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                var Pk = Group.FirstOrDefault().GreigeP_Machine_FK;
                                var MachDef = context.TLADM_MachineDefinitions.Find(Pk);
                                if(MachDef != null)
                                    nr.DataColumn1 = MachDef.MD_MachineCode;
                                
                                Pk = Group.FirstOrDefault().GreigeP_Greige_Fk;
                                
                                nr.DataColumn2 = context.TLADM_Griege.Find(Pk).TLGreige_Description;
                                nr.DataColumn3 = Group.Count();
                                nr.DataColumn4 = Group.Sum(x => x.GreigeP_weight);
                                nr.DataColumn5 = Group.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Sum(x => (decimal ?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn8 = Group.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn6 = Group.Where(x => x.GreigeP_Grade == "B").Sum(x => (decimal ?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn7 = Group.Where(x => x.GreigeP_Grade == "C").Sum(x => (decimal ?) x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn9 = Group.Average(x => x.GreigeP_VarianceDiskWeight);
                                nr.PrimaryKey = 1;
                                if (nr.DataColumn3 == 0)
                                    continue;
  
                                dataTable1.AddDataTable1Row(nr);
                            }
                          
                        }
                        else if (_opts.K7rbQA3)
                        {
                            
                            var OperGroups = Data.Where(x=>!x.GreigeP_CommisionCust).GroupBy(x => new { x.GreigeP_Operator_FK }).ToList();
                            foreach (var Group in OperGroups)
                            {
                                DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                var Pk = Group.FirstOrDefault().GreigeP_Operator_FK;
                                nr.DataColumn1 = context.TLADM_MachineOperators.Find(Pk).MachOp_Description;
                                nr.DataColumn3 = Group.Count();
                                nr.DataColumn4 = Group.Sum(x => x.GreigeP_weight);
                                nr.DataColumn5 = Group.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Sum(x => (decimal ?) x.GreigeP_weight) ?? 0.00M;
                                nr.DataColumn8 = Group.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn6 = Group.Where(x => x.GreigeP_Grade == "B").Sum(x => (decimal ?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn7 = Group.Where(x => x.GreigeP_Grade == "C").Sum(x => (decimal ?)x.GreigeP_weight) ?? 0.0M;
                                nr.DataColumn9 = Group.Average(x => x.GreigeP_VarianceDiskWeight);
                                nr.PrimaryKey = 1;

                                if (nr.DataColumn3 == 0)
                                    continue;

                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                        else if (_opts.K7ByMachineByDay)
                        {
                            /*
                            Data = _Repo.GreigeProduction(_QueryParms);
                            var MachineGroups = Data.GroupBy(x => x.GreigeP_Machine_FK);
                            foreach(var Mach in MachineGroups)
                            {
                                if (Mach.FirstOrDefault().GreigeP_Machine_FK == null)
                                {
                                    continue;
                                }

                                var DailyData = Mach.GroupBy(x => x.GreigeP_PDate);
                                DataSet24.DataTable3Row nr = dataTable3.NewDataTable3Row();
                                nr.PrimaryKey = 1;
                                
                                nr.Day1 = 0;
                                nr.Day2 = 0;
                                nr.Day3 = 0;
                                nr.Day4 = 0;
                                nr.Day5 = 0;
                                nr.Day6 = 0;
                                nr.Day7 = 0;
                                nr.Day8 = 0;
                                nr.Day9 = 0;
                                nr.Day10 = 0;
                                nr.Day11 = 0;
                                nr.Day12 = 0;
                                nr.Day13 = 0;
                                nr.Day14 = 0;
                                nr.Day15 = 0;
                                nr.Day16 = 0;
                                nr.Day17 = 0;
                                nr.Day18 = 0;
                                nr.Day19 = 0;
                                nr.Day20 = 0;
                                nr.Day21 = 0;
                                nr.Day22 = 0;
                                nr.Day23 = 0;
                                nr.Day24 = 0;
                                nr.Day25 = 0;
                                nr.Day26 = 0;
                                nr.Day27 = 0;
                                nr.Day28 = 0;
                                nr.Day29 = 0;
                                nr.Day30 = 0;
                                nr.Day31 = 0;

                                nr.Machine = context.TLADM_MachineDefinitions.Find(Mach.FirstOrDefault().GreigeP_Machine_FK).MD_MachineCode;
                                

                                foreach (var Day in DailyData)
                                {
                                    var ProdDate = (DateTime)Day.FirstOrDefault().GreigeP_PDate;
                                    var DayOfMth = ProdDate.Day;
                                    var Ind = dataTable3.Columns.IndexOf("Day" + DayOfMth.ToString());
                                    nr[Ind] = Day.Sum(x=>x.GreigeP_weightAvail);

                                 
                                }

                                dataTable3.AddDataTable3Row(nr);
                            }
                            */


                        }
                            

                    }
                  
                }

                DataSet24.DataTable2Row hr = dataTable2.NewDataTable2Row();
                hr.PrimaryKey = 1;
                hr.DataColumn1 = _QueryParms.FromDate;
                hr.DataColumn2 = _QueryParms.ToDate;
                dataTable2.AddDataTable2Row(hr);

                
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
              
                GreigeProdByTradeL rep = new GreigeProdByTradeL();
                if(_opts.QASummary)
                    rep.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;

                IEnumerator ie = rep.Section2.ReportObjects.GetEnumerator();
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
            }
            else if (_RepNo == 24)  // Greige Stock on Hand   
            {
                DataSet ds = new DataSet();
                DataSet25.DataTable1DataTable datatable1 = new DataSet25.DataTable1DataTable();
                DataSet25.TLADM_StockTakeFreqDataTable freqTable = new DataSet25.TLADM_StockTakeFreqDataTable();
                DataSet25.TLADM_WhseStoreDataTable storeTable = new DataSet25.TLADM_WhseStoreDataTable();
                IList<TLADM_QualityDefinition> QualityDefinitions = null;
                _Repo = new KnitRepository();

                Type fieldsType = typeof(TLKNI_GreigeProduction);

                using (var context = new TTI2Entities())
                {
                    QualityDefinitions = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == 11).OrderBy(x => x.QD_ColumnIndex).ToList();

                    var GProduction = _Repo.SOHGreigeProduction(_QueryParms);
                    foreach (var row in GProduction)
                    {   //-----------------------------------------------------------------
                        //
                        //--------------------------------------------------------------
                        DataSet25.DataTable1Row nr = datatable1.NewDataTable1Row();
                        var xGreige = context.TLADM_Griege.Find(row.GreigeP_Greige_Fk);
                        if (xGreige != null)
                        {
                            nr.DataColumn1 = xGreige.TLGreige_Description;
                            nr.DataColumn6 = xGreige.TLGreige_StockTakeFreq_FK;
                        }

                        nr.DataColumn2 = row.GreigeP_PieceNo;
                        nr.DataColumn3 = row.GreigeP_Grade;
                        nr.DataColumn4 = row.GreigeP_weightAvail;
                        nr.DataColumn5 = (int)row.GreigeP_Store_FK;
                        nr.Remarks = row.GreigeP_Remarks;
                        nr.MergeDetail = row.GreigeP_MergeDetail;

                        // nr.DskWeight = (decimal)row.GreigeP_DskWeight;

                        if (row.GreigeP_BoughtIn)
                        {
                            nr.DataColumn7 = context.TLADM_Colours.Find(row.GreigeP_BIFColour_FK).Col_Display;
                        }
                        else
                        {
                            if (_opts.K10IncludeFaults)
                            {
                                /*TLKNI_GreigeProduction Cols = new TLKNI_GreigeProduction();

                                Object tst = Cols.Col;
                                

                                foreach (PropertyInfo prop in tst.GetType().GetProperties())
                                {
                                    if (prop.Name == "Key")
                                    {
                                     
                                    }
                                }*/


                                StringBuilder sb = new StringBuilder();

                                foreach(var QualityD in QualityDefinitions)
                                {
                                    if (QualityD.QD_ColumnIndex == 1)
                                    {
                                        if (row.GreigeP_Meas1 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas1.ToString() + " ");
                                        }

                                    }
                                     else if (QualityD.QD_ColumnIndex == 2)
                                    {
                                        if (row.GreigeP_Meas2 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas2.ToString() + " ");
                                        }
                                    }
                                    else if (QualityD.QD_ColumnIndex == 3)
                                    {
                                        if (row.GreigeP_Meas3 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas3.ToString() + " ");
                                        }

                                    }
                                    else if (QualityD.QD_ColumnIndex == 4)
                                    {
                                        if (row.GreigeP_Meas4 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas4.ToString() + " ");
                                        }

                                    }
                                    else if (QualityD.QD_ColumnIndex == 5)
                                    {
                                        if (row.GreigeP_Meas5 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas5.ToString() + " ");
                                        }
                                    }
                                    else if (QualityD.QD_ColumnIndex == 6)
                                    {
                                        if (row.GreigeP_Meas6 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas6.ToString() + " ");
                                        }
                                    }
                                    else if (QualityD.QD_ColumnIndex == 7)
                                    {
                                        if (row.GreigeP_Meas7 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas7.ToString() + " ");
                                        }
                                    }
                                    else
                                    {
                                        if (row.GreigeP_Meas8 != 0)
                                        {
                                            sb.Append(QualityD.QD_ShortCode + " " + row.GreigeP_Meas8.ToString() + " " );
                                        }
                                    }
                                }
                                if (sb.Length != 0)
                                {
                                    nr.Faults = "Faults : " + sb.ToString();
                                }
                            }
                            nr.DataColumn7 = row.GreigeP_Remarks;

                            var KnitOrder = context.TLKNI_Order.Find(row.GreigeP_KnitO_Fk);
                            if (KnitOrder != null)
                            {
                                    var YarnOrder = context.TLSPN_YarnOrder.Find(KnitOrder.KnitO_YarnO_FK);
                                    if (YarnOrder != null)
                                    {
                                        nr.YarnOrderNo = YarnOrder.YarnO_OrderNumber.ToString();

                                        if (YarnOrder.YarnO_MergeContract_FK > 0)
                                        {
                                            var MergeDetail = context.TLSPN_CottonMerge.Find(YarnOrder.YarnO_MergeContract_FK);
                                            if (MergeDetail != null)
                                            {
                                                nr.MergeDetail = MergeDetail.TLCTM_Description;
                                            }
                                        }
                                    }
                            }
                            
                        }
                        datatable1.AddDataTable1Row(nr);
                    }
                    // Type FieldsType = typeof(TLKNI_GreigeProduction);
                    // PropertyInfo[] props = FieldsType.GetProperties(BindingFlags.Public| BindingFlags.Instance);
                                   

                    var STF = context.TLADM_StockTakeFreq.ToList();
                    foreach (var row in STF)
                    {
                        DataSet25.TLADM_StockTakeFreqRow str = freqTable.NewTLADM_StockTakeFreqRow();
                        str.STF_Description = row.STF_Description;
                        str.STF_Period_Weeks = row.STF_Period_Weeks;
                        str.STF_Pk = row.STF_Pk;
                        str.STF_Description = row.STF_Description;
                        str.STF_ShortCode = row.STF_ShortCode;

                        freqTable.AddTLADM_StockTakeFreqRow(str);
                    }

                    var whse = context.TLADM_WhseStore.ToList();
                    foreach (var row in whse)
                    {
                        DataSet25.TLADM_WhseStoreRow whr = storeTable.NewTLADM_WhseStoreRow();
                        whr.WhStore_Code = row.WhStore_Code;
                        whr.WhStore_Description = row.WhStore_Description;
                        whr.WhStore_Id = row.WhStore_Id;

                        storeTable.AddTLADM_WhseStoreRow(whr);
                    }

                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(freqTable);
                ds.Tables.Add(storeTable);

                if (!_opts.K10STF)
                {
                    GreigeSOH rep1 = new GreigeSOH();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                }
                else
                {
                    GreigeSOHByPN rep1 = new GreigeSOHByPN();
                    rep1.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rep1;
                }

            }
            else if (_RepNo == 25)  // K9 - Efficiency / utilisation reports  
            {
                DataSet ds = new DataSet();
                DataSet26.DataTable1DataTable dataTable1 = new DataSet26.DataTable1DataTable();
                DataSet26.DataTable2DataTable dataTable2 = new DataSet26.DataTable2DataTable();
                IList<TLKNI_GreigeProduction> GreigeP = null;
                TimeSpan variable = _opts.toDate - _opts.fromDate;

                using (var context = new TTI2Entities())
                {
                    GreigeP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Captured && x.GreigeP_PDate >= _opts.fromDate && x.GreigeP_PDate <= _opts.toDate && !x.GreigeP_BoughtIn && !x.GreigeP_CommisionCust && x.GreigeP_Machine_FK != null).ToList();
                    if (!_opts.SplitByShift)
                    {
                        var GreigePGrouped = GreigeP.GroupBy(x => x.GreigeP_KnitO_Fk);
                        foreach (var KO in GreigePGrouped)
                        {
                            decimal AGradeKnitted = 0.00M;
                            decimal AGradeKnittedWithWarning =  0.00M;
                            decimal BGradeKnitted = 0.00M;
                            decimal CGradeKnitted = 0.00M;
                            decimal KRGradeKnitted = 0.00M;

                            var KnitOrder_FK = KO.FirstOrDefault().GreigeP_Pk;

                            var TotalPiecesKnitted = KO.Count();
                            var TotalKgKnitted = KO.Sum(x => x.GreigeP_weight);

                            AGradeKnitted = KO.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weight);
                            AGradeKnittedWithWarning = KO.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weight);
                            BGradeKnitted = KO.Where(x => x.GreigeP_Grade == "B").Sum(x => x.GreigeP_weight);
                            CGradeKnitted = KO.Where(x => x.GreigeP_Grade == "C").Sum(x => x.GreigeP_weight);
                            KRGradeKnitted = context.TLKNI_ProductionSplit.Where (x=>(int)x.TLKNISP_KnitOrder_FK  == KnitOrder_FK).Sum(x=>(decimal ?)x.TLKNISP_Mass) ?? 0.00M;
                             
                            var AGradePercent = (AGradeKnitted / TotalKgKnitted) * 100;
                            var AGradePercentWithWarning = (AGradeKnittedWithWarning / TotalKgKnitted) * 100;
                            var BGradePercent = (BGradeKnitted / TotalKgKnitted) * 100;
                            var CGradePercent = (CGradeKnitted / TotalKgKnitted) * 100;
                            var KRGradePercent     = (KRGradeKnitted / TotalKgKnitted) * 100;

                            decimal MaxCapacity = 0;
                            var MachDesc = string.Empty;

                            var KnitOrder = context.TLKNI_Order.Find(KO.FirstOrDefault().GreigeP_KnitO_Fk);
                            if (KnitOrder != null)
                            {
                                var machine = context.TLADM_MachineDefinitions.Find(KnitOrder.KnitO_Machine_FK);
                                if (machine != null)
                                {
                                    var Days = (int)variable.Days + 1;
                                    MaxCapacity = machine.MD_MaxCapacity * (Days * 24);
                                    MachDesc = machine.MD_AlternateDesc;
                                }
                            }

                            Decimal CapacityUtil = 0.00M;
                            
                            if(MaxCapacity > 0)
                              CapacityUtil = (TotalKgKnitted / MaxCapacity) * 100;

                            DataSet26.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Key = 1;
                            nr.BGradeKniited = BGradeKnitted;
                            nr.BGradePercent = BGradePercent;
                            nr.CGradeKnitted = CGradeKnitted;
                            nr.CGradePercent = CGradePercent;
                            nr.Machine = MachDesc;
                            nr.MaxCapacity = MaxCapacity;
                            nr.TotalKgs = TotalKgKnitted;
                            nr.TotalPieces = TotalPiecesKnitted;
                            nr.AGradeKnitted = AGradeKnitted;
                            nr.AGradePercent = AGradePercent;
                            nr.AGradeKnittedWithWarning = AGradeKnittedWithWarning;
                            nr.AGradePercentWithWarning = AGradePercentWithWarning;
                            nr.CapUtil = CapacityUtil;
                            nr.KRGradeKnitted = KRGradeKnitted;
                            nr.KRGradePercent = KRGradePercent;
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                    else
                    {
                        var GreigePGrouped = GreigeP.GroupBy(x => x.GreigeP_Shift_FK);
                        foreach (var KO in GreigePGrouped)
                        {
                            decimal AGradeKnitted = 0.00M;
                            decimal AGradeKnittedWithWarning = 0.00M;
                            decimal BGradeKnitted = 0.00M;
                            decimal CGradeKnitted = 0.00M;
                            decimal KRGradeKnitted = 0.00M;

                            var TotalPiecesKnitted = KO.Count();
                            var TotalKgKnitted = KO.Sum(x => x.GreigeP_weight);

                            AGradeKnitted = KO.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weight);
                            AGradeKnittedWithWarning = KO.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weight);
                            BGradeKnitted = KO.Where(x => x.GreigeP_Grade == "B").Sum(x => x.GreigeP_weight);
                            CGradeKnitted = KO.Where(x => x.GreigeP_Grade == "C").Sum(x => x.GreigeP_weight);
                            foreach (var K in KO)
                            {
                                KRGradeKnitted += context.TLKNI_ProductionSplit.Where (x=>(int)x.TLKNISP_KnitOrder_FK  == K.GreigeP_Pk).Sum(x=>(decimal ?)x.TLKNISP_Mass) ?? 0.00M;
                            }
                            
                            var AGradePercent = (AGradeKnitted / TotalKgKnitted) * 100;
                            var AGradePercentWithWarning = (AGradeKnittedWithWarning / TotalKgKnitted) * 100;
                            var BGradePercent = (BGradeKnitted / TotalKgKnitted) * 100;
                            var CGradePercent = (CGradeKnitted / TotalKgKnitted) * 100;
                            var KRGradePercent = (KRGradeKnitted / TotalKgKnitted) * 100;
                                              
                            decimal MaxCapacity = 0;
                            var MachDesc = string.Empty;
                                                      
                            var KnitOrder = context.TLKNI_Order.Find(KO.FirstOrDefault().GreigeP_KnitO_Fk);
                            if (KnitOrder != null)
                            {
                                var machine = context.TLADM_MachineDefinitions.Find(KnitOrder.KnitO_Machine_FK);
                                if (machine != null)
                                {
                                    var Days = (int)variable.Days + 1;
                                    MaxCapacity = machine.MD_MaxCapacity * (Days * 8);
                                    MachDesc = context.TLADM_Shifts.Find(KO.FirstOrDefault().GreigeP_Shift_FK).Shft_Description;
                                    
                                }
                            }

                            Decimal CapacityUtil = 0.00M;

                            if (MaxCapacity > 0)
                                CapacityUtil = (TotalKgKnitted / MaxCapacity) * 100;

                            DataSet26.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Key = 1;
                            nr.BGradeKniited = BGradeKnitted;
                            nr.BGradePercent = BGradePercent;
                            nr.CGradeKnitted = CGradeKnitted;
                            nr.CGradePercent = CGradePercent;
                            nr.Machine = MachDesc;
                            nr.MaxCapacity = MaxCapacity;
                            nr.TotalKgs = TotalKgKnitted;
                            nr.TotalPieces = TotalPiecesKnitted;
                            nr.AGradeKnitted = AGradeKnitted;
                            nr.AGradePercent = AGradePercent;
                            nr.AGradeKnittedWithWarning = AGradeKnittedWithWarning;
                            nr.AGradePercentWithWarning = AGradePercentWithWarning;
                            nr.CapUtil = CapacityUtil;
                            nr.KRGradeKnitted = KRGradeKnitted;
                            nr.KRGradePercent = KRGradePercent;

                            dataTable1.AddDataTable1Row(nr);


                        }
                    }
                }

                DataSet26.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                hnr.Key = 1;
                hnr.DaeTo = _opts.toDate;
                hnr.DateFrom = _opts.fromDate;
                dataTable2.AddDataTable2Row(hnr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                utilisation rep1 = new utilisation();
                rep1.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep1;

            }
            else if (_RepNo == 26)  // Data Capture   
            {
                DataSet ds = new DataSet();
                DataSet27.DataTable1DataTable dataTable1 = new DataSet27.DataTable1DataTable();
                DataSet27.DataTable2DataTable dataTable2 = new DataSet27.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var GreigeP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Captured && x.GreigeP_KnitO_Fk == _KnitO).ToList();
                    foreach (var row in GreigeP)
                    {
                        DataSet27.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Key = 1;
                        nr.PieceNumber = row.GreigeP_PieceNo;
                        nr.PieceOperator = context.TLADM_MachineOperators.Find(row.GreigeP_Operator_FK).MachOp_Description;
                        nr.PieceShift = context.TLADM_Shifts.Find(row.GreigeP_Shift_FK).Shft_Description;
                        nr.PieceDate = (DateTime)row.GreigeP_PDate;
                        nr.PieceWeight = row.GreigeP_weight;

                        dataTable1.AddDataTable1Row(nr);
                    }

                    var KO = context.TLKNI_Order.Find(_KnitO);

                    DataSet27.DataTable2Row hr = dataTable2.NewDataTable2Row();
                    hr.Key = 1;
                    hr.KnitOrder = KO.KnitO_OrderNumber.ToString();
                    dataTable2.AddDataTable2Row(hr);

                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                InputResults rep1 = new InputResults();
                rep1.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep1;
                
            }
            else if (_RepNo == 27)
            {
                DataSet ds = new DataSet();
                DataSet28.DataTable1DataTable datatable1 = new DataSet28.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_Order.Where(x => !x.KnitO_YarnAssigned && x.KnitO_OrderConfirmed && !x.KnitO_Closed).OrderBy(x => x.KnitO_OrderNumber).ToList();
                    foreach (var Row in Existing)
                    {
                        DataSet28.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.KnitMachine = context.TLADM_MachineDefinitions.Find(Row.KnitO_Machine_FK).MD_MachineCode;
                        nr.KnitQuality = context.TLADM_Griege.Find(Row.KnitO_Product_FK).TLGreige_Description;
                        nr.KnitWeight = Row.KnitO_Weight;
                        nr.OrderDate = Row.KnitO_OrderDate;
                        nr.RequiredDate = Row.KnitO_DeliveryDate;
                        if(Row.KnitO_YarnO_FK != null)
                            nr.YarnType = context.TLADM_Yarn.Find(Row.KnitO_YarnO_FK).YA_Description;
                        nr.KnitOrderNo = Row.KnitO_OrderNumber.ToString();

                        datatable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(datatable1);
                YarnAwaitingAssignment YarnAssignment = new YarnAwaitingAssignment();
                YarnAssignment.SetDataSource(ds);
                crystalReportViewer1.ReportSource = YarnAssignment;

            }
            else if (_RepNo == 28)
            {
                DataSet ds = new DataSet();
                DataSet29.DataTable1DataTable dataTable = new DataSet29.DataTable1DataTable();
                using ( var context = new TTI2Entities())
                {
                    var Records = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Captured && !x.GreigeP_Inspected).ToList();
                    foreach (var Record in Records)
                    {
                        DataSet29.DataTable1Row nr = dataTable.NewDataTable1Row();
                        nr.PieceNo = Record.GreigeP_PieceNo;
                        nr.ProductionDate = (DateTime)Record.GreigeP_PDate;
                        nr.ProductionWeight = Record.GreigeP_weight;
                        nr.KnitOrder = context.TLKNI_Order.Find(Record.GreigeP_KnitO_Fk).KnitO_OrderNumber.ToString();
                        var GreigeQual = context.TLADM_Griege.Find(Record.GreigeP_Greige_Fk);
                        if(GreigeQual != null)
                            nr.Quality = GreigeQual.TLGreige_Description;
                        dataTable.Rows.Add(nr);
                    }
                }
               
            

                ds.Tables.Add(dataTable);
                ProdWaitingInspection Wait = new ProdWaitingInspection();
                Wait.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Wait;
                
            }
            else if (_RepNo == 29)
            {
                DataSet ds = new DataSet();
                DataSet30.DataTable1DataTable dataTable1 = new DataSet30.DataTable1DataTable();
                DataSet30.DataTable2DataTable dataTable2 = new DataSet30.DataTable2DataTable();
                _Repo = new KnitRepository();

                var Pallets = _Repo.YarnOrderPallets(_QueryParms);

                using (var context = new TTI2Entities())
                {
                    foreach (var Pallet in Pallets)
                    {
                        DataSet30.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.Date = (DateTime)Pallet.TLKNIOP_ReservedDate;
                        nr.YarnOrder = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK).YarnO_OrderNumber.ToString();
                        var KnitOrder = context.TLKNI_Order.Find(Pallet.TLKNIOP_ReservedBy);
                        
                        if(KnitOrder != null)
                        {
                            nr.KnitOrder = KnitOrder.KnitO_OrderNumber;
                            nr.Quality = context.TLADM_Griege.Find(KnitOrder.KnitO_Product_FK).TLGreige_Description;
                        }
                        nr.YarnType = context.TLADM_Yarn.Find(Pallet.TLKNIOP_YarnType_FK).YA_Description;
                        nr.Weight = Pallet.TLKNIOP_NettWeightReserved;
                        nr.PalletNo = Pallet.TLKNIOP_PalletNo.ToString();

                        dataTable1.AddDataTable1Row(nr);
                    }

                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet30.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.ErrorLog = "No Records found pertaining to selection made";

                        dataTable1.AddDataTable1Row(nr);
                    }

                    DataSet30.DataTable2Row xr = dataTable2.NewDataTable2Row();
                    xr.Pk = 1;
                    xr.FromDate = _QueryParms.FromDate;
                    xr.ToDate = _QueryParms.ToDate;
                    xr.ReportHeading = "Listing of Yarn Order Pallets issued to Knit Order Pallets";
                    dataTable2.AddDataTable2Row(xr);

                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2); 

                ListingOfYarnOrders ListingOfYO = new ListingOfYarnOrders();
                ListingOfYO.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ListingOfYO;
              

            }
            else if (_RepNo == 30) // Greige Stock Summary Report
            {
                DataSet ds = new DataSet();
                DataSet31.DataTable1DataTable dataTable1 = new DataSet31.DataTable1DataTable();
                _Repo = new KnitRepository();
                Util core = new Util();
                IList<TLADM_QualityDefinition> QualityDefinitions = null;
                         
                string[][] ColumnNames = null;

                if (!_QueryParms.NonStandardGrades)
                {
                    ColumnNames = new string[][]
                    {   
                      new string[] {"Text12", "Qualified"}
                    };
                }
                else
                {
                    ColumnNames = new string[][]
                   {
                      new string[] {"Text12", "Non Standard"}
                   };
                }
                var GreigeSOHGroup = _Repo.SOHGreigeProduction(_QueryParms).GroupBy(x => x.GreigeP_Greige_Fk).ToList();

                using (var context = new TTI2Entities())
                {
                    QualityDefinitions = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == 11).OrderBy(x => x.QD_ShortCode).ToList();
                    foreach (var Group in GreigeSOHGroup)
                    {
                        var Pk = Group.FirstOrDefault().GreigeP_Greige_Fk;

                        DataSet31.DataTable1Row nr = dataTable1.NewDataTable1Row();

                        var Greige = context.TLADM_Griege.Find(Pk);
                        if (Greige != null)
                        {
                            nr.Description = Greige.TLGreige_Description;
                            nr.ReOrder_Level = Greige.TLGreige_ROL;
                            nr.ReOrder_Qty = Greige.TLGreige_ROQ;

                            var GreigeQ = context.TLADM_GreigeQuality.Find(Greige.TLGreige_Quality_FK);
                            if (GreigeQ != null)
                                nr.GroupDescription = GreigeQ.GQ_Description;

                        }
                        
                        nr.OutStanding_KO = context.TLKNI_Order.Where(x => x.KnitO_Product_FK == Pk && !x.KnitO_Closed).Sum(x => (decimal?)x.KnitO_Weight) ?? 0.00M;
                        nr.Pending_Ins = Group.Where(x => !x.GreigeP_Inspected).Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;

                        nr.Available_ToBatch = nr.Grade_A = nr.Grade_B = nr.Grade_C = nr.Qualified = 0.0M;

                        if (!_QueryParms.NonStandardGrades)
                        {
                            nr.Grade_A = Group.Where(x => x.GreigeP_Grade.Trim() == "A" && !x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weightAvail);
                            nr.Grade_B = Group.Where(x => x.GreigeP_Grade.Trim() == "B").Sum(x => x.GreigeP_weightAvail);
                            nr.Grade_C = Group.Where(x => x.GreigeP_Grade.Trim() == "C").Sum(x => x.GreigeP_weightAvail);
                            nr.Qualified = Group.Where(x => x.GreigeP_Grade.Trim() == "A" && x.GreigeP_WarningMessage).Sum(x => x.GreigeP_weightAvail);
                        }
                        else
                        {
                            nr.Qualified = Group.Sum(x => x.GreigeP_weightAvail);
                        }

                        nr.Available_ToBatch = nr.Grade_A + nr.Grade_B + nr.Grade_C + nr.Qualified;
                        
                        nr.DO_LT8 = 0.00M;
                        nr.DO_GT8 = 0.00M;

                        int ThisWeek = core.GetIso8601WeekOfYear(DateTime.Now);

                        var LessThan = from DO in context.TLDYE_DyeOrder
                                       join DOD in context.TLDYE_DyeOrderDetails on DO.TLDYO_Pk equals DOD.TLDYOD_DyeOrder_Fk
                                       where DOD.TLDYOD_BodyOrTrim && !DO.TLDYO_Closed && 
                                       DO.TLDYO_DyeReqWeek <= (ThisWeek + 8) && DO.TLDYO_Greige_FK == Pk
                                       select new { DO, DOD};
                        
                        if (LessThan.Count() != 0)
                        {
                            nr.DO_LT8 = (decimal)LessThan.Sum(x => (decimal?)x.DOD.TLDYOD_Kgs ?? 0.00M);

                            var AlreadyBatched = from LTH in LessThan
                                     join DB in context.TLDYE_DyeBatch on LTH.DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                     where DB.DYEB_Allocated 
                                     select DB;

                            if (AlreadyBatched.Count() != 0)
                            {
                                nr.DO_LT8 -= (decimal)AlreadyBatched.Sum(x => (decimal?)x.DYEB_BatchKG ?? 0.00M);
                            }
                        }
                        

                        var GreaterThan = (from DO in context.TLDYE_DyeOrder
                                           join DOD in context.TLDYE_DyeOrderDetails on DO.TLDYO_Pk equals DOD.TLDYOD_DyeOrder_Fk
                                           where DOD.TLDYOD_BodyOrTrim && !DO.TLDYO_Closed && DO.TLDYO_DyeReqWeek > (ThisWeek + 8) && DO.TLDYO_Greige_FK == Pk
                                           select new { DO, DOD });
                        
                        if(GreaterThan.Count() > 0)
                        {
                            nr.DO_GT8 = (decimal)GreaterThan.Sum(x => (decimal?)x.DOD.TLDYOD_Kgs ?? 0.00M);
                            var AlreadyBatched = from GT in GreaterThan
                                                 join DB in context.TLDYE_DyeBatch on GT.DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                                 where DB.DYEB_Allocated
                                                 select DB;

                            if (AlreadyBatched.Count() != 0)
                            {
                                nr.DO_GT8 -= (decimal)AlreadyBatched.Sum(x => (decimal?)x.DYEB_BatchKG ?? 0.00M);
                            }
                        }

                     


                        dataTable1.AddDataTable1Row(nr);
                    }

                }
                ds.Tables.Add(dataTable1);

                GreigeSOHSummary SOHSummary = new GreigeSOHSummary();
                SOHSummary.SetDataSource(ds);
                IEnumerator ie = SOHSummary.Section2.ReportObjects.GetEnumerator();
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
                crystalReportViewer1.ReportSource = SOHSummary;
            }
            else if(_RepNo == 31)
            {
                DataSet ds = new DataSet();
                DataSet32.DataTable1DataTable BoughtIn = new DataSet32.DataTable1DataTable();
                
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLKNI_BoughtInFabric.Where(x => x.TLBIN_TransNumber == _KnitO).ToList();
                    foreach (var Row in Existing)
                    {
                        DataSet32.DataTable1Row nr = BoughtIn.NewDataTable1Row();
                        nr.CountryOfOrigin  = context.TLADM_CottonOrigin.Find( Row.TLBIN_COfOrigin_FK).CottonOrigin_Description ;
                        nr.CurrentStore  = context.TLADM_WhseStore.Find(Row.TLBIN_CurrentStore_FK).WhStore_Description;
                        nr.DskWeight  = Row.TLBIN_Dsk_Weight;
                        nr.DskWidth = Row.TLBIN_Dsk_Width;
                        nr.Quality = context.TLADM_Griege.Find(Row.TLBIN_Greige_FK).TLGreige_Description ;
                        nr.Machine  = context.TLADM_MachineDefinitions.Find(Row.TLBIN_Machine_FK).MD_AlternateDesc;
                        nr.NettWeight = Row.TLBIN_Nett_Weight;
                        nr.TheirPartNo = Row.TLBIN_Their_PN;
                        nr.TransDate = Row.TLBIN_TransDate;
                        nr.TransNO = Row.TLBIN_TransNumber;
                        nr.OurPartNo = Row.TLBIN_TTS_PN;

                        BoughtIn.AddDataTable1Row(nr);
                    }
              }
                
              ds.Tables.Add(BoughtIn);
             
              Knitting.RepBoughtInFabric Boughtx = new Knitting.RepBoughtInFabric();
              Boughtx.SetDataSource(ds);
              crystalReportViewer1.ReportSource = Boughtx;
                 
            }

            else if (_RepNo == 32)   // Knit Yarn Transactions Report
            {
                DataSet ds = new DataSet();
                DataSet33.DataTable1DataTable dataTable1 = new DataSet33.DataTable1DataTable();
                DataSet33.DataTable2DataTable dataTable2 = new DataSet33.DataTable2DataTable();


                DataSet33.DataTable1Row nr = dataTable1.NewDataTable1Row();
                String Title = String.Empty;
                
                Title += "Yarn Transactions for the Period ";
                if(_RepOps.ThirdParty)
                    Title += "Yarn Transactions for the Period 3rd Party Only ";
                else
                     Title += "Yarn Transactions for the Period excluding 3rd Party ";

                nr.Pk = 1;
                nr.Title_ = Title;
                nr.FromDate = _RepOps.FromDate;
                nr.ToDate = _RepOps.ToDate;

                dataTable1.AddDataTable1Row(nr);
                


                using (var context = new TTI2Entities())
                {
                    var entries = context.TLKNI_YarnTransaction.Where(x => x.KnitY_TransactionDate >= _RepOps.FromDate && x.KnitY_TransactionDate <= _RepOps.ToDate && x.KnitY_ThirdParty == _RepOps.ThirdParty).ToList();
                    foreach (var entry in entries)
                    {
                        DataSet33.DataTable2Row xr = dataTable2.NewDataTable2Row();
                        xr.Pk = 1;
                        if(_RepOps.ThirdParty)
                           xr.Customer = context.TLADM_Suppliers.Find(entry.KnitY_Customer_FK).Sup_Description;
                        else
                            xr.Customer = context.TLADM_CustomerFile.Find(entry.KnitY_Customer_FK).Cust_Description;
                        xr.TransDate = entry.KnitY_TransactionDate;
                        xr.TransDoc = entry.KnitY_TransactionDoc;
                        xr.GrnNumber = entry.KnitY_GRNNumber;
                        xr.Notes = entry.KnitY_Notes;
                        xr._3rdParty = entry.KnitY_ThirdParty;
                        xr.GrossWeight = context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_KnitY_FK == entry.KnitY_Pk).Sum(x => (decimal?)x.KnitYD_GrossWeight) ?? 0.00M;
                        xr.NettWeight = context.TLKNI_YarnTransactionDetails.Where(x=>x.KnitYD_KnitY_FK == entry.KnitY_Pk).Sum(x=>(decimal ?)x.KnitYD_NettWeight) ?? 0.00M;
                        
                        dataTable2.AddDataTable2Row(xr);
                    }

                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                Knitting.YarnTransactions Boughtx = new Knitting.YarnTransactions();
                Boughtx.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Boughtx;

            }
            else if (_RepNo == 33)
            {
                DataSet ds = new DataSet();
                DataSet34.DataTable1DataTable dataTable1 = new DataSet34.DataTable1DataTable();
                DataSet34.DataTable2DataTable dataTable2 = new DataSet34.DataTable2DataTable();


                DataSet34.DataTable1Row HeadRow = dataTable1.NewDataTable1Row();
                HeadRow.Title = "Knitting - Key Measurement values";
                HeadRow.Pk = 1;
                dataTable1.AddDataTable1Row(HeadRow);

                using (var context = new TTI2Entities())
                {
                    var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "KNIT").FirstOrDefault();
                    if (Depts != null)
                    {
                        var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                        foreach (var Reason in Reasons)
                        {
                            DataSet34.DataTable2Row NewRow = dataTable2.NewDataTable2Row();
                            NewRow.Pk = 1;
                            NewRow.ShortCode = Reason.QD_ShortCode;
                            NewRow.Description = Reason.QD_Description;

                            dataTable2.AddDataTable2Row(NewRow);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                Knitting.QualityDescription QualityDesc = new Knitting.QualityDescription();
                QualityDesc.SetDataSource(ds);
                crystalReportViewer1.ReportSource = QualityDesc;

            }
            else if(_RepNo == 34)
            {
                DataSet ds = new DataSet();
                DataSet35.DataTable1DataTable dataTable1 = new DataSet35.DataTable1DataTable();
                Util core = new Util();
                _Repo = new KnitRepository();

                // var Data = _Repo.GriegeDskVariance(_QueryParms);
                var Data = _Repo.GreigeProduction(_QueryParms);
                using (var context = new TTI2Entities())
                {
                    foreach (var Record in Data)
                    {
                        DataSet35.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                        NewRow.KnitOrder = context.TLKNI_Order.Find(Record.GreigeP_KnitO_Fk).KnitO_OrderNumber;
                        NewRow.PieceNo = Record.GreigeP_PieceNo;
                        NewRow.Grade = Record.GreigeP_Grade;
                        NewRow.Date = (DateTime)Record.GreigeP_PDate;
                        NewRow.ProductionWeight = Record.GreigeP_weight;
                        var Greige = context.TLADM_Griege.Find(Record.GreigeP_Greige_Fk);
                        if (Greige != null)
                        {
                            NewRow.Quality = Greige.TLGreige_Description;
                            NewRow.StdDsk = Greige.TLGreige_CubicWeight;
                            NewRow.KnittedDsk = Record.GreigeP_DskWeight;
                            NewRow.Variance = Record.GreigeP_VarianceDiskWeight; //  core.CalculateDskVariance(Greige.TLGreige_CubicWeight, Record.GreigeP_DskWeight);
                        }

                        dataTable1.AddDataTable1Row(NewRow);
                    }
                }
                
                if(Data.Count() == 0)
                {
                    DataSet35.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                    NewRow.ErrorLog = "No data found for parameters entered";
                    dataTable1.AddDataTable1Row(NewRow);
                }
                ds.Tables.Add(dataTable1);
                
                

                Knitting.KnittingDskVariance KnittingVariance = new Knitting.KnittingDskVariance();
                KnittingVariance.SetDataSource(ds);
                crystalReportViewer1.ReportSource = KnittingVariance;


            }
            else if (_RepNo == 35)
            {
                DataSet ds = new DataSet();
                DataSet36.DataTable1DataTable dataTable1 = new DataSet36.DataTable1DataTable();
                DataSet36.DataTable2DataTable dataTable2 = new DataSet36.DataTable2DataTable();
                Util core = new Util();
                IList<TLKNI_GreigeProduction> GreigeP = new List<TLKNI_GreigeProduction>();
                bool AddLogMess = false;

                _Repo = new KnitRepository();

                DataSet36.DataTable1Row HeadRow = dataTable1.NewDataTable1Row();
                HeadRow.Title = "Greige Produced on a Daily Basis by Machine";
                HeadRow.PrimarkKey = 1;
                HeadRow.FromDate = _QueryParms.FromDate;
                HeadRow.ToDate = _QueryParms.ToDate;
                dataTable1.AddDataTable1Row(HeadRow);

                using (var context = new TTI2Entities())
                {
                    var MachineGroups = (from T1 in context.TLADM_MachineDefinitions
                                         join T2 in context.TLKNI_GreigeProduction
                                         on T1.MD_Pk equals T2.GreigeP_Machine_FK
                                         where T1.MD_Department_FK == 11 && T2.GreigeP_PDate >= _QueryParms.FromDate && T2.GreigeP_PDate <= _QueryParms.ToDate
                                         select new { T1.MD_MachineCode, T2.GreigeP_PDate, T2.GreigeP_weightAvail, T2.GreigeP_Machine_FK }).GroupBy(x=>x.MD_MachineCode).ToList();
                    

                    foreach(var MachG in MachineGroups)
                    {
                        DataSet36.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.PrimaryKey = 1;

                        nr.Day01 = nr.Day02 = nr.Day03 = nr.Day04 = nr.Day05 = nr.Day06 = nr.Day07 = 0;
                        nr.Day08 = nr.Day09 = nr.Day10 = nr.Day11 = nr.Day12 = nr.Day13 = nr.Day14 = 0;
                        nr.Day15 = nr.Day16 = nr.Day17 = nr.Day18 = nr.Day19 = nr.Day20 = nr.Day21 = 0;
                        nr.Day22 = nr.Day23 = nr.Day24 = nr.Day25 = nr.Day26 = nr.Day27 = nr.Day28 = 0;
                        nr.Day29 = nr.Day30 = nr.Day31 = 0;

                        nr.Machine = context.TLADM_MachineDefinitions.Find(MachG.FirstOrDefault().GreigeP_Machine_FK).MD_MachineCode;
                        nr.Quality = string.Empty; // context.TLADM_Griege.Find(MachG.FirstOrDefault().GreigeP_Greige_Fk).TLGreige_Description;

                        foreach (var Day in MachG)
                        {
                            var ProdDate = (DateTime)Day.GreigeP_PDate;
                            var DayOfMth = ProdDate.Day;
                            var Ind = dataTable2.Columns.IndexOf("Day" + DayOfMth.ToString().PadLeft(2, '0'));
                            var CurrentVal = Convert.ToDecimal(nr[Ind].ToString());
                            nr[Ind] = CurrentVal + Day.GreigeP_weightAvail;  
                        }

                        dataTable2.AddDataTable2Row(nr);
                    }

                    if(dataTable2.Count == 0)
                    {
                        AddLogMess = true;
                    }
                          
                }

                
                if (AddLogMess)
                {
                    DataSet36.DataTable2Row NewRow = dataTable2.NewDataTable2Row();
                    NewRow.PrimaryKey = 1;
                    NewRow.ErrorLog = "No data found for parameters entered";
                    dataTable2.AddDataTable2Row(NewRow);
                }
               

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                Knitting.GreigeProdByDay GProdByDay = new Knitting.GreigeProdByDay();
                int I = 0;
                var IndPos = 5;
                DateTime StartDate = new DateTime(_QueryParms.FromDate.Year, _QueryParms.FromDate.Month, 1);
                DateTime LastDate = StartDate.AddMonths(1).AddDays(-1);
                StartDate = StartDate.AddDays(-1);
                do
                {
                    if (++IndPos < 37)
                    {
                        TextObject text = (TextObject)GProdByDay.ReportDefinition.Sections["Section2"].ReportObjects["Text" + IndPos.ToString()];
                        if (text != null)
                        {
                            StartDate = StartDate.AddDays(1);
                            text.Text = StartDate.ToString("dd/MM/yy"); ;
                        }
                    }
                } while (++I < LastDate.Day);

                GProdByDay.SetDataSource(ds);
                crystalReportViewer1.ReportSource = GProdByDay;
            }
            crystalReportViewer1.Refresh();

        }

        private struct DATA
        {
            public string GreigeDescription;
            public string PieceNo;
            public string Grade;
            public decimal Weight;
            public int StorePk;
            public int STF;

            public DATA(string _Description, string _PieceNo, string _Grade, decimal _Weight, int _StoreKey, int _STF )
            {
                this.GreigeDescription = _Description;
                this.PieceNo = _PieceNo;
                this.Grade = _Grade;
                this.Weight = _Weight;
                this.StorePk = _StoreKey;
                this.STF = _STF;
            }
        }
    }
}
