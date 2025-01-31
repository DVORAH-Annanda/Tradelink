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
using System.Globalization;
using System.Collections;


namespace CMT
{
    public partial class frmCMTViewRep : Form
    {
        int _RepNo;
        int _RecKey;
        Util core;
        CMTReportOptions _repOpts;
        CMTQueryParameters _QueryParms;
        CMTRepository _repos;
        bool _Export;
        bool _Reprint;


        IList<TLADM_Styles> _Styles;
        IList<TLADM_Colours> _Colours;
        IList<TLADM_Sizes> _Sizes;
        IList<TLADM_CustomerFile> _Customers;
        IList<TLADM_Griege> _Qualities;
        IList<TLADM_MachineDefinitions> _Machines;

        public frmCMTViewRep(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmCMTViewRep(int RepNo, bool Export)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Export = Export;
        }

        public frmCMTViewRep(int RepNo, CMTReportOptions repOpts)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _repOpts = repOpts;
        }

        public frmCMTViewRep(int RepNo, int RecKey)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _RecKey = RecKey;
        }

        public frmCMTViewRep(int RepNo, int RecKey, bool Reprint)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _RecKey = RecKey;
            _Reprint = Reprint;
        }

        public frmCMTViewRep(int RepNo, CMTQueryParameters QueryParms, bool Reprint)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _QueryParms = QueryParms;
            _Reprint = Reprint;
        }

        public frmCMTViewRep(int RepNo, CMTQueryParameters QueryParms, CMTReportOptions repOpts)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _QueryParms = QueryParms;
            _repOpts = repOpts;

        }
        public frmCMTViewRep()
        {
            InitializeComponent();
        }

        private void frmCMTViewRep_Load(object sender, EventArgs e)
        {
          
            using (var context = new TTI2Entities())
            {
                _Styles = context.TLADM_Styles.ToList();
                _Colours = context.TLADM_Colours.ToList();
                _Sizes = context.TLADM_Sizes.ToList();
                _Customers = context.TLADM_CustomerFile.ToList();
                _Qualities = context.TLADM_Griege.ToList();
                _Machines = context.TLADM_MachineDefinitions.ToList();
            }

            if (_RepNo == 1)  // Truck loading Instructions 
            {
                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable dataTable2 = new DataSet1.DataTable2DataTable();

                IList<TLCMT_PanelIssueDetail> PanelIssueDetail = new List<TLCMT_PanelIssueDetail>();

                using (var context = new TTI2Entities())
                {
                    var PI = context.TLCMT_PanelIssue.Find(_RecKey);
                    if (PI != null)
                    {
                        DataSet1.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                        dtr.Pk = _RecKey;
                        dtr.LoadDate = PI.CMTPI_Date;
                        dtr.LoadNo = PI.CMTPI_Number;
                        dtr.CMTName = context.TLADM_Departments.Find(PI.CMTPI_Department_FK).Dep_Description;
                        dataTable1.AddDataTable1Row(dtr);

                        PanelIssueDetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == PI.CMTPI_Pk).ToList();

                        foreach (var pi in PanelIssueDetail)
                        {
                            DataSet1.DataTable2Row dtr2 = dataTable2.NewDataTable2Row();
                            dtr2.Pk = _RecKey;

                            var CutSheetR = context.TLCUT_CutSheetReceipt.Find(pi.CMTPID_CutSheet_FK);
                            if (CutSheetR != null)
                            {
                                dtr2.CutSheet = context.TLCUT_CutSheet.Find(CutSheetR.TLCUTSHR_CutSheet_FK).TLCutSH_No.Remove(0, 2);
                                dtr2.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheetR.TLCUTSHR_Style_FK).Sty_Description;
                                dtr2.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheetR.TLCUTSHR_Colour_FK).Col_Description;
                            }

                            try
                            {
                                dtr2.NoOfPanels = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetR.TLCUTSHR_Pk && !x.TLCUTSHRD_PanelRejected && !x.TLCUTSHRD_ToCMT).Sum(x => x.TLCUTSHRD_BoxUnits);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == pi.CMTPID_CutSheet_FK).FirstOrDefault();
                            if (Boxes != null)
                            {
                                dtr2.AdultBoxes = Boxes.TLCUTSHB_AdultBoxes;
                                dtr2.KidsBoxes = Boxes.TLCUTSHB_KidBoxes;
                                dtr2.TotalBoxes = dtr2.AdultBoxes + dtr2.KidsBoxes;
                            }

                            dataTable2.AddDataTable2Row(dtr2);
                        }
                    }
                }

                if (dataTable2.Rows.Count == 0)
                {
                    DataSet1.DataTable2Row dtr2 = dataTable2.NewDataTable2Row();
                    dtr2.Pk = _RecKey;
                    dtr2.ErrorLog = "There are no records pertaining to the selection made";
                    dataTable2.AddDataTable2Row(dtr2);
                }

                ds.Tables.Add(dataTable1);
                DataView DataV = dataTable2.DefaultView;
                DataV.Sort = "CutSheet";
                ds.Tables.Add(DataV.ToTable());

                TruckLoadInstructions tli = new TruckLoadInstructions();
                tli.SetDataSource(ds);
                crystalReportViewer1.ReportSource = tli;
            }
            else if (_RepNo == 2)  // CMT Deleivery Note 
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable dataTable1 = new DataSet2.DataTable1DataTable();
                DataSet2.DataTable2DataTable dataTable2 = new DataSet2.DataTable2DataTable();
                TLCMT_PanelIssue PanelIssue = new TLCMT_PanelIssue();
                _repos = new CMTRepository();

                using (var context = new TTI2Entities())
                {
                    var PIssues = _repos.CMTPanelIssue(_QueryParms);
                    foreach (var PIssue in PIssues)
                    {
                        PanelIssue = context.TLCMT_PanelIssue.Find(PIssue.CMTPI_Pk);
                        if (PanelIssue != null)
                        {
                            DataSet2.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = PIssue.CMTPI_Pk;

                            var TranType = context.TLADM_TranactionType.Find(PanelIssue.CMTPI_TranType_FK);
                            if (TranType != null)
                            {
                                if (PanelIssue.CMTPI_FromWhse_FK != 0)
                                    nr.From = context.TLADM_WhseStore.Find(PanelIssue.CMTPI_FromWhse_FK).WhStore_Description;

                                nr.To = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK).WhStore_Description;
                            }

                            if (!_Reprint)
                            {
                                var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == PanelIssue.CMTPI_Department_FK).FirstOrDefault();
                                if (LNU != null)
                                {
                                    nr.DeliveryNumber = "C" + LNU.col2.ToString().PadLeft(5, '0');
                                }

                                nr.Date = DateTime.Now;
                            }
                            else
                            {
                                nr.DeliveryNumber = "C" + PanelIssue.CMTPI_DeliveryNumber.ToString().PadLeft(5, '0');
                                nr.Date = PanelIssue.CMTPI_Date;
                            }

                            nr.Title = "CMT Delivery Note";
                            nr.LoadNumber = PanelIssue.CMTPI_Number;

                            dataTable1.AddDataTable1Row(nr);

                            var PanelIssueDetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == PanelIssue.CMTPI_Pk).ToList();

                            foreach (var pi in PanelIssueDetail)
                            {
                                DataSet2.DataTable2Row dt2 = dataTable2.NewDataTable2Row();
                                dt2.Pk = PIssue.CMTPI_Pk;

                                var CutSheetR = context.TLCUT_CutSheetReceipt.Find(pi.CMTPID_CutSheet_FK);
                                if (CutSheetR != null)
                                {
                                    dt2.Bundles = CutSheetR.TLCUTSHR_NoOfBundles;
                                    var CS = context.TLCUT_CutSheet.Find(CutSheetR.TLCUTSHR_CutSheet_FK);
                                    if (CS != null)
                                    {
                                        dt2.CutSheet = CS.TLCutSH_No;
                                        var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                                        if (DB != null)
                                        {
                                            dt2.DyeBatch = DB.DYEB_BatchNo;
                                        }

                                        var DyeOrder = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                        if (DyeOrder != null)
                                        {

                                            dt2.Quality = context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK).Sty_Description;
                                        }

                                        dt2.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;
                                        dt2.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == DB.DYEB_Customer_FK).Cust_Description;

                                        var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetR.TLCUTSHR_Pk).FirstOrDefault();
                                        if (Boxes != null)
                                        {
                                            dt2.AdultBoxes = Boxes.TLCUTSHB_AdultBoxes;
                                            dt2.KidsBoxes = Boxes.TLCUTSHB_KidBoxes;
                                            dt2.TotalBoxes = Boxes.TLCUTSHB_AdultBoxes + Boxes.TLCUTSHB_KidBoxes;
                                            dt2.Binding = Boxes.TLCUTSHB_Binding;
                                            dt2.Rib = Boxes.TLCUTSHB_Ribbing;
                                        }

                                        var Qty = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetR.TLCUTSHR_Pk && !x.TLCUTSHRD_PanelRejected);
                                        if (Qty.Count() != 0)
                                        {
                                            dt2.Qty = Qty.Sum(x => x.TLCUTSHRD_BoxUnits);
                                        }

                                        StringBuilder xSizes = new StringBuilder();
                                        var GroupedBySize = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetR.TLCUTSHR_Pk).GroupBy(x => x.TLCUTSHRD_Size_FK);
                                        foreach (var Group in GroupedBySize)
                                        {
                                            var Index = Group.FirstOrDefault().TLCUTSHRD_Size_FK;
                                            String sze = _Sizes.FirstOrDefault(s => s.SI_id == Index).SI_Description;
                                            xSizes.Append(sze + ",");
                                        }

                                        dt2.xSizes = xSizes.ToString().Remove(-1 + xSizes.Length, 1);
                                    }
                                    dataTable2.AddDataTable2Row(dt2);
                                }

                            }
                        }
                    }

                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CMTDelivery CmtDel = new CMTDelivery();
                CmtDel.SetDataSource(ds);
                crystalReportViewer1.ReportSource = CmtDel;
            }
            else if (_RepNo == 3)  // CMT ReceiptDeleivery Note 
            {
                DataSet ds = new DataSet();
                DataSet3.DataTable1DataTable datatable1 = new DataSet3.DataTable1DataTable();

                core = new Util();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_PanelIssue_FK == _RecKey && !x.TLCMTLI_IssuedToLine).ToList();
                    foreach (var Row in Existing)
                    {
                        DataSet3.DataTable1Row nr = datatable1.NewDataTable1Row();

                        nr.CMTIssue = context.TLCMT_PanelIssue.Find(Row.TLCMTLI_PanelIssue_FK).CMTPI_DeliveryNumber.ToString();
                        nr.CutSheet = context.TLCUT_CutSheet.Find(Row.TLCMTLI_CutSheet_FK).TLCutSH_No;
                        nr.CMTLine = context.TLADM_WhseStore.Find(Row.TLCMTLI_WhStore_FK).WhStore_Description;

                        nr.DateIssued = Row.TLCMTLI_Date;

                        var CS = context.TLCUT_CutSheet.Find(Row.TLCMTLI_CutSheet_FK);
                        if (CS != null)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    var dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CMTReqWeek);
                                    nr.DateRequired = dt.AddDays(5);
                                }
                            }
                        }
                        datatable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(datatable1);
                PanelReceipt panReceipt = new PanelReceipt();
                panReceipt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = panReceipt;

            }
            else if (_RepNo == 4)  // CMT Factory cofiguration 
            {
                DataSet ds = new DataSet();
                DataSet4.DataTable1DataTable dataTable1 = new DataSet4.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_FactConfig.ToList();
                    foreach (var Record in Existing)
                    {
                        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Factory_Pk = Record.TLCMTCFG_Department_FK;
                        nr.Factory_Description = context.TLADM_Departments.Find(Record.TLCMTCFG_Department_FK).Dep_Description;
                        nr.Line_Details = Record.TLCMTCFG_Description;
                        nr.Line_Supervisor = Record.TLCMTCFG_Operator;
                        nr.Operators = Record.TLCMTCFG_NoOfOperators;
                        nr.Std_Output = Record.TLCMTCFG_StdOutput;
                        var StdProduct = context.TLADM_StandardProduct.Find(Record.TLCMTCFG_Quality_FK);
                        if (StdProduct != null)
                            nr.StandardProduct = StdProduct.TLADMSP_Description;

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                FactoryConfig fcon = new FactoryConfig();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

            }
            else if (_RepNo == 5)  // BULK Final Audit 
            {
                DataSet ds = new DataSet();
                DataSet5.DataTable1DataTable dataTable1 = new DataSet5.DataTable1DataTable();
                DataSet5.DataTable2DataTable dataTable2 = new DataSet5.DataTable2DataTable();
                int RecKey = _RecKey;
                var Colour = String.Empty;
                var Customer = String.Empty;

                using (var context = new TTI2Entities())
                {
                    var CS = context.TLCUT_CutSheet.Find(_RecKey);
                    if (CS != null)
                    {
                        var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            Colour = _Colours.FirstOrDefault(s => s.Col_Id == DB.DYEB_Colour_FK).Col_Display;
                            Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == CS.TLCutSH_Customer_FK).Cust_Description;
                        }
                        var Sizes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                        foreach (var Size in Sizes)
                        {
                            DataSet5.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = RecKey;
                            nr.CutSheetNo = CS.TLCutSH_No;
                            nr.Colour = Colour;
                            nr.Customer = Customer;
                            nr.Supplier = nr.Customer;

                            nr.StyleDescription = context.TLADM_Styles.Find(CS.TLCutSH_Styles_FK).Sty_Description;
                            nr.UnitOrdered = Size.TLCUTE_NoofGarments;
                            nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Size.TLCUTE_Size_FK).SI_Description;

                            var LineIssue = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                            if (LineIssue != null)
                            {
                                nr.CMT = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;
                                nr.CountryOfOrigin = string.Empty;
                                if (LineIssue.TLCMTLI_LineNo_FK != 0)
                                    nr.LineNumber = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                            }

                            dataTable1.AddDataTable1Row(nr);

                            //=============================================================================
                            // We need to sort this into a particular sequence based on the short code 
                            //============================================================================
                            var Existing = (from ADM_AuditM in context.TLADM_CMTMeasurementPoints
                                            join CMT_AuditM in context.TLCMT_AuditMeasurements on ADM_AuditM.CMTMP_Pk equals CMT_AuditM.CMTBFA_MeasureP_FK
                                            where CMT_AuditM.CMTBFA_Customer_FK == CS.TLCutSH_Customer_FK && CMT_AuditM.CMTBFA_Style_FK == CS.TLCutSH_Styles_FK && CMT_AuditM.CMTBFA_Size_FK == Size.TLCUTE_Size_FK
                                            orderby ADM_AuditM.CMTMP_DisplayOrder
                                            select CMT_AuditM).ToList();

                            foreach (var row in Existing)
                            {
                                DataSet5.DataTable2Row nrx = dataTable2.NewDataTable2Row();
                                nrx.Pk = RecKey;
                                nrx.MeasurementPoint = context.TLADM_CMTMeasurementPoints.Find(row.CMTBFA_MeasureP_FK).CMTMP_Description;
                                nrx.MeasurementValue = row.CMTBFA_Measurement;

                                dataTable2.AddDataTable2Row(nrx);
                            }

                            RecKey += 1;
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                BFAudit BFAudit = new BFAudit();
                BFAudit.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BFAudit;
            }
            else if (_RepNo == 6)  // C6 Panel Stock in George Panel store  
            {
                DataSet ds = new DataSet();
                DataSet6.DataTable1DataTable dataTable1 = new DataSet6.DataTable1DataTable();
                string[][] ColumnNames = null;
                _repos = new CMTRepository();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {


                    ColumnNames = new string[][]
                    {   new string[] {"Text10", string.Empty},
                        new string[] {"Text23", string.Empty},
                        new string[] {"Text24", string.Empty},
                        new string[] {"Text25", string.Empty},
                        new string[] {"Text26", string.Empty},
                        new string[] {"Text27", string.Empty},
                        new string[] {"Text28", string.Empty},
                        new string[] {"Text29", string.Empty},
                        new string[] {"Text30", string.Empty},
                        new string[] {"Text31", string.Empty},
                        new string[] {"Text32", string.Empty}

                    };

                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }
                    /*
                    var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).GroupBy(x => x.SI_ColNumber).ToList();
                    foreach (var xSize in Sizes)
                    {
                        xSize.OrderBy(x => x.SI_DisplayOrder);

                        StringBuilder Sb = new StringBuilder();
                        foreach (var Item in xSize)
                        {
                            Sb.Append(Item.SI_Description + Environment.NewLine);
                        }


                        foreach (var ColumnName in ColumnNames)
                        {
                            if (string.IsNullOrEmpty(ColumnName[1]))
                            {
                                ColumnName[1] = Sb.ToString();
                                break;
                            }
                        }
                    }
                    */

                    var CSR = _repos.SelCutSheetByWhse(_QueryParms);

                    foreach (var row in CSR)
                    {
                        DataSet6.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        var CS = context.TLCUT_CutSheet.Find(row.TLCUTSHR_CutSheet_FK);
                        if (CS != null)
                        {
                            if (CS.TLCutSH_Closed)
                                continue;

                            nr.CustSheet = CS.TLCutSH_No;
                        }

                        nr.Customer = context.TLADM_WhseStore.Find(row.TLCUTSHR_WhsePanStore_FK).WhStore_Description;
                        try
                        {
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCUTSHR_Colour_FK).Col_Display;
                        }
                        catch (Exception ex)
                        {
                            nr.Colour = "Unknown";
                        }

                        if (CS != null)
                        {
                            nr.Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == CS.TLCutSH_Quality_FK).TLGreige_Description;
                        }

                        var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == row.TLCUTSHR_Pk).FirstOrDefault();
                        if (Boxes != null)
                        {
                            nr.AdultBoxes = Boxes.TLCUTSHB_AdultBoxes;
                            nr.KidsBoxes = Boxes.TLCUTSHB_KidBoxes;
                        }

                        var CutSheetDet = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == row.TLCUTSHR_Pk);
                        if (CutSheetDet.Count() != 0)
                        {
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
                            nr.Total = 0;
                            nr.Bundles = CutSheetDet.Count();
                            var ExpectedUnitsGrouped = CutSheetDet.GroupBy(x => x.TLCUTSHRD_Size_FK);
                            foreach (var Group in ExpectedUnitsGrouped)
                            {
                                var SizePk = Group.FirstOrDefault().TLCUTSHRD_Size_FK;
                                var Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk);
                                if (Size != null)
                                {
                                    var EUnits = Group.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                    if (Size.SI_ColNumber == 1)
                                        nr.Col1 += EUnits;
                                    else if (Size.SI_ColNumber == 2)
                                        nr.Col2 += EUnits;
                                    else if (Size.SI_ColNumber == 3)
                                        nr.Col3 += EUnits;
                                    else if (Size.SI_ColNumber == 4)
                                        nr.Col4 += EUnits;
                                    else if (Size.SI_ColNumber == 5)
                                        nr.Col5 += EUnits;
                                    else if (Size.SI_ColNumber == 6)
                                        nr.Col6 += EUnits;
                                    else if (Size.SI_ColNumber == 7)
                                        nr.Col7 += EUnits;
                                    else if (Size.SI_ColNumber == 8)
                                        nr.Col8 += EUnits;
                                    else if (Size.SI_ColNumber == 9)
                                        nr.Col9 += EUnits;
                                    else if (Size.SI_ColNumber == 10)
                                        nr.Col10 += EUnits;
                                    else
                                        nr.Col11 += EUnits;
                                }
                            }

                            nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                        }
                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet6.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);
                }

                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CustSheet";
                ds.Tables.Add(DataV.ToTable());

                // ds.Tables.Add(dataTable1);

                PanelSOH fcon = new PanelSOH();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

                IEnumerator ie = fcon.Section2.ReportObjects.GetEnumerator();
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
            else if (_RepNo == 7)  // Panel Stock at CMT in Panel Receipt cage  
                                   // as well as units WIP as well as units in Despatch cage 

            {
                DataSet ds = new DataSet();
                DataSet7.DataTable1DataTable dataTable1 = new DataSet7.DataTable1DataTable();
                string[][] ColumnNames = null;
                Util core = new Util();


                _repos = new CMTRepository();
                var LineIssues = _repos.CMTLineIssue(_QueryParms);
                var fnd = LineIssues.FirstOrDefault(s => s.TLCMTLI_CutSheet_FK == 8726);

                var LineIssuesCw = _repos.CMTLineIssueCW(_QueryParms);

                LineIssues = LineIssues.Concat(LineIssuesCw);

                ColumnNames = new string[][]
                    {   new string[] {"Text10", string.Empty},
                        new string[] {"Text23", string.Empty},
                        new string[] {"Text24", string.Empty},
                        new string[] {"Text25", string.Empty},
                        new string[] {"Text26", string.Empty},
                        new string[] {"Text27", string.Empty},
                        new string[] {"Text28", string.Empty},
                        new string[] {"Text29", string.Empty},
                        new string[] {"Text30", string.Empty},
                        new string[] {"Text31", string.Empty},
                        new string[] {"Text32", string.Empty}
                    };

                using (var context = new TTI2Entities())
                {
                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }


                    foreach (var LineIssue in LineIssues)
                    {
                        int Units_Per_Bag = 0;

                        if (_QueryParms.ExcludeOnHold && LineIssue.TLCMTLI_OnHold)
                        {
                            continue;
                        }

                        var CSReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == LineIssue.TLCMTLI_CutSheet_FK).FirstOrDefault();
                        if (CSReceipt != null)
                        {
                            if (_QueryParms.Styles.Count != 0)
                            {
                                var Styles = _QueryParms.Styles.FirstOrDefault(s => s.Sty_Id == CSReceipt.TLCUTSHR_Style_FK);
                                if (Styles == null)
                                    continue;
                            }

                            if (_QueryParms.Colours.Count != 0)
                            {
                                var Colours = _QueryParms.Colours.FirstOrDefault(s => s.Col_Id == CSReceipt.TLCUTSHR_Colour_FK);
                                if (Colours == null)
                                    continue;
                            }

                            //==================================================
                            DataSet7.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            var CS = context.TLCUT_CutSheet.Find(CSReceipt.TLCUTSHR_CutSheet_FK);
                            if (CS != null)
                            {
                                nr.CustSheet = CS.TLCutSH_No;
                                nr.Customer = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CSReceipt.TLCUTSHR_Colour_FK).Col_Display;

                                var Styles = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK);
                                if (Styles != null)
                                {
                                    nr.Quality = Styles.Sty_Description;
                                    Units_Per_Bag = Styles.Sty_Bags;
                                }
                            }

                            var CutSheetDet = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSReceipt.TLCUTSHR_Pk);
                            if (CutSheetDet.Count() != 0)
                            {
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
                                nr.Total = 0;
                                nr.UnitsOnHold = 0;

                                var ExpectedUnitsGrouped = CutSheetDet.GroupBy(x => x.TLCUTSHRD_Size_FK);
                                foreach (var Group in ExpectedUnitsGrouped)
                                {
                                    var SizePk = Group.FirstOrDefault().TLCUTSHRD_Size_FK;
                                    var Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk);
                                    if (Size != null)
                                    {
                                        var EUnits = Group.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                        if (Size.SI_ColNumber == 1)
                                            nr.Col1 += EUnits;
                                        else if (Size.SI_ColNumber == 2)
                                            nr.Col2 += EUnits;
                                        else if (Size.SI_ColNumber == 3)
                                            nr.Col3 += EUnits;
                                        else if (Size.SI_ColNumber == 4)
                                            nr.Col4 += EUnits;
                                        else if (Size.SI_ColNumber == 5)
                                            nr.Col5 += EUnits;
                                        else if (Size.SI_ColNumber == 6)
                                            nr.Col6 += EUnits;
                                        else if (Size.SI_ColNumber == 7)
                                            nr.Col7 += EUnits;
                                        else if (Size.SI_ColNumber == 8)
                                            nr.Col8 += EUnits;
                                        else if (Size.SI_ColNumber == 9)
                                            nr.Col9 += EUnits;
                                        else if (Size.SI_ColNumber == 10)
                                            nr.Col10 += EUnits;
                                        else
                                            nr.Col11 += EUnits;
                                    }
                                }
                                nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                            }

                            nr.Date = LineIssue.TLCMTLI_Date;
                            nr.Hold = LineIssue.TLCMTLI_OnHold;
                            nr.Priority = LineIssue.TLCMTLI_Priority;

                            nr.Wip = 0;
                            nr.DespatchCage = 0;
                            nr.ReceiptCage = 0;
                            nr.Bags = Units_Per_Bag;

                            if (!LineIssue.TLCMTLI_OnHold)
                            {
                                if (!LineIssue.TLCMTLI_WorkCompleted)
                                {
                                    if (!LineIssue.TLCMTLI_IssuedToLine)
                                        nr.ReceiptCage = nr.Total;
                                    else
                                        nr.Wip = nr.Total;
                                }
                                else
                                {
                                    var CompletedW = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_LineIssue_FK == LineIssue.TLCMTLI_Pk).ToList();
                                    if (CompletedW.Count != 0)
                                    {
                                        if (!CompletedW.FirstOrDefault().TLCMTWC_Despatched)
                                            nr.DespatchCage = CompletedW.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                                    }
                                }
                            }
                            else
                            {
                                nr.UnitsOnHold = nr.Total;
                            }

                            if (nr.Wip + nr.DespatchCage + nr.ReceiptCage == 0 && !LineIssue.TLCMTLI_OnHold)
                                continue;

                            if (Units_Per_Bag != 0)
                            {
                                var Res = (Decimal)nr.ReceiptCage / Units_Per_Bag;
                                nr.Bags = (int)Math.Round(Res, 0);
                            }
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                }

                DataView DataV = dataTable1.DefaultView;
                if (_repOpts.PanelStockSortOrder == 1)
                    DataV.Sort = "CustSheet";
                else if (_repOpts.PanelStockSortOrder == 2)
                    DataV.Sort = "Quality";
                else if (_repOpts.PanelStockSortOrder == 3)
                    DataV.Sort = "Colour";
                else if (_repOpts.PanelStockSortOrder == 4)
                    DataV.Sort = "Quality, Colour";
                else
                    DataV.Sort = "CustSheet, Quality, Colour";


                ds.Tables.Add(DataV.ToTable());

                _repos.Dispose();

                PanelsAtCMT panCTM = new PanelsAtCMT();
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
            else if (_RepNo == 8)  // CMT Panel Issue Transfer  
            {
                DataSet ds = new DataSet();
                DataSet18.DataTable1DataTable dataTable1 = new DataSet18.DataTable1DataTable();
                DataSet18.DataTable2DataTable dataTable2 = new DataSet18.DataTable2DataTable();
                core = new Util();
                int _Lable = 0;
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_LineIssue.Find(_RecKey);
                    if (Existing != null)
                    {
                        DataSet18.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        //---------------------------------------------------------
                        var Whse = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Existing.TLCMTLI_CmtFacility_FK).FirstOrDefault();
                        if (Whse != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(Whse.WhStore_Description + Environment.NewLine);
                            sb.Append(Whse.WhStore_Address1 + Environment.NewLine);
                            sb.Append(Whse.WhStore_Address3 + Environment.NewLine);
                            sb.Append(Whse.WhStore_Address4 + Environment.NewLine);
                            sb.Append(Whse.WhStore_Address5 + Environment.NewLine);
                            nr.IssuedTo = sb.ToString();
                        }

                        var FactConfig = context.TLCMT_FactConfig.Find(Existing.TLCMTLI_LineNo_FK);
                        if (FactConfig != null)
                        {
                            nr.Line = FactConfig.TLCMTCFG_LineNo.ToString();
                            nr.LineDescription = FactConfig.TLCMTCFG_Description;
                        }

                        var CutSheet = context.TLCUT_CutSheet.Find(Existing.TLCMTLI_CutSheet_FK);
                        if (CutSheet != null)
                        {
                            String CS = CutSheet.TLCutSH_No.Remove(0, 2);
                            nr.IssueNumber = "SI" + CS.PadLeft(5, '0');
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;
                            nr.CutSheet = CutSheet.TLCutSH_No;
                            var DyeBatch = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                            if (DyeBatch != null)
                            {
                                nr.DyeBatch = DyeBatch.DYEB_BatchNo;
                                var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                if (DyeOrder != null)
                                {
                                    nr.BodyQuality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrder.TLDYO_Greige_FK).TLGreige_Description;
                                    nr.Order = DyeOrder.TLDYO_DyeOrderNum;
                                    var DtReq = core.FirstDateOfWeek(DyeOrder.TLDYO_OrderDate.Year, DyeOrder.TLDYO_CMTReqWeek);
                                    nr.DateRequired = DtReq.AddDays(5);
                                    _Lable = DyeOrder.TLDYO_Label_FK;

                                    var DyeOrderDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder.TLDYO_Pk && !x.TLDYOD_BodyOrTrim).ToList();
                                    var Count = 0;
                                    foreach (var DyeOrderDetail in DyeOrderDetails)
                                    {
                                        if (Count == 0)
                                            nr.TrimQuality1 = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrderDetail.TLDYOD_Greige_FK).TLGreige_Description;
                                        else
                                        {
                                            nr.TrimQuality2 = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrderDetail.TLDYOD_Greige_FK).TLGreige_Description;
                                            break;
                                        }

                                        Count += 1;
                                    }
                                }
                            }

                            var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                            if (CutSheetReceipt != null)
                            {
                                nr.Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_AdultBoxes;
                                nr.Ribbing = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Ribbing;
                                nr.Binding = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Binding;

                                var CutSheetDetails = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).ToList();

                                foreach (var CutSheetDetail in CutSheetDetails)
                                {
                                    DataSet18.DataTable2Row dt2 = dataTable2.NewDataTable2Row();

                                    dt2.Bundle = CutSheetDetail.TLCUTSHRD_Description;
                                    dt2.Size = _Sizes.FirstOrDefault(s => s.SI_id == CutSheetDetail.TLCUTSHRD_Size_FK).SI_Description;
                                    dt2.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                                    dt2.Lable = context.TLADM_Labels.Find(_Lable).Lbl_Description;
                                    dt2.Bodies = CutSheetDetail.TLCUTSHRD_BoxUnits;
                                    dt2.Ribs = CutSheetDetail.TLCUTSHRD_BoxUnits;

                                    dt2.Sleeves = CutSheetDetail.TLCUTSHRD_BoxUnits.ToString() + " x 2";
                                    dataTable2.AddDataTable2Row(dt2);
                                }
                            }
                        }

                        nr.Date = (DateTime)Existing.TLCMTLI_TransferDate;


                        dataTable1.AddDataTable1Row(nr);
                    }


                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);


                CMTPanelIssue fcon = new CMTPanelIssue();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

            }
            else if (_RepNo == 9)
            {
                DataSet ds = new DataSet();
                DataSet9.DataTable1DataTable dataTable1 = new DataSet9.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_DeflectFlaw.ToList();
                    foreach (var row in Existing)
                    {
                        DataSet9.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.ShortCode = row.TLCMTDF_ShortCode;
                        nr.Description = row.TLCMTDF_Description;

                        dataTable1.AddDataTable1Row(nr);
                    }

                }

                ds.Tables.Add(dataTable1);
                DefectFlaws fcon = new DefectFlaws();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;
            }
            else if (_RepNo == 10)
            {
                DataSet ds = new DataSet();
                DataSet10.DataTable1DataTable dataTable1 = new DataSet10.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_AuditMeasurements.ToList();
                    foreach (var row in Existing)
                    {
                        DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == row.CMTBFA_Customer_FK).Cust_Description;
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == row.CMTBFA_Style_FK).Sty_Description;
                        nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == row.CMTBFA_Size_FK).SI_Description;
                        nr.MeasurementPoint = context.TLADM_CMTMeasurementPoints.Find(row.CMTBFA_MeasureP_FK).CMTMP_Description;
                        nr.MeasurementValue = row.CMTBFA_Measurement;

                        dataTable1.AddDataTable1Row(nr);
                    }
                }
                ds.Tables.Add(dataTable1);

                CurrentMeasurementValues CurValues = new CurrentMeasurementValues();
                CurValues.SetDataSource(ds);
                crystalReportViewer1.ReportSource = CurValues;

            }
            else if (_RepNo == 11)  // Report on stock on Hand CMT Despatch Cage after production
            {                       //----------------------------------------------------------
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable dataTable1 = new DataSet11.DataTable1DataTable();
                IList<TLCMT_CompletedWork> Completed = new List<TLCMT_CompletedWork>();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Customer", typeof(string));         // 0
                dt.Columns.Add("CutSheet", typeof(string));         // 1
                dt.Columns.Add("LineNo", typeof(string));           // 2
                dt.Columns.Add("TransDate", typeof(DateTime));      // 3
                dt.Columns.Add("Style", typeof(String));            // 4 
                dt.Columns.Add("Size", typeof(string));             // 5
                dt.Columns.Add("Colour", typeof(string));           // 6
                dt.Columns.Add("BoxNumber", typeof(string));        // 7
                dt.Columns.Add("Grade", typeof(string));            // 8
                dt.Columns.Add("Qty", typeof(int));                 // 9
                dt.Columns.Add("Earmarked", typeof(string));        // 10
                dt.Columns.Add("Facility", typeof(string));         // 11
                using (var context = new TTI2Entities())
                {
                    Completed = context.TLCMT_CompletedWork.Where(x => !x.TLCMTWC_Despatched).ToList();
                    foreach (var row in Completed)
                    {
                        DataRow dr = dt.NewRow();
                        var CS = context.TLCUT_CutSheet.Find(row.TLCMTWC_CutSheet_FK);
                        if (CS != null)
                        {

                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                dr[0] = _Customers.FirstOrDefault(s => s.Cust_Pk == DB.DYEB_Customer_FK).Cust_Description;
                                dr[6] = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;
                            }
                        }

                        dr[1] = context.TLCUT_CutSheet.Find(row.TLCMTWC_CutSheet_FK).TLCutSH_No;
                        var LI = context.TLCMT_LineIssue.Find(row.TLCMTWC_LineIssue_FK);
                        if (LI != null)
                        {
                            if (_repOpts.CMT != 0 && _repOpts.CMT != LI.TLCMTLI_CmtFacility_FK)
                                continue;

                            dr[2] = context.TLCMT_FactConfig.Find(LI.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                            dr[11] = context.TLADM_Departments.Find(LI.TLCMTLI_CmtFacility_FK).Dep_Description;
                        }
                        dr[3] = row.TLCMTWC_TransactionDate;
                        dr[4] = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                        dr[5] = _Sizes.FirstOrDefault(s => s.SI_id == row.TLCMTWC_Size_FK).SI_Description;
                        dr[7] = row.TLCMTWC_BoxNumber;
                        dr[8] = row.TLCMTWC_Grade;
                        dr[9] = row.TLCMTWC_Qty;
                        dr[10] = "";
                        dr[11] = "";
                        dt.Rows.Add(dr);
                    }
                }
                try
                {
                    if (_repOpts.SortSequence != 0 && dt.Rows.Count != 0)
                    {
                        if (_repOpts.SortSequence == 1)
                            dt = dt.Select(null, dt.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repOpts.SortSequence == 2)
                            dt = dt.Select(null, dt.Columns[7].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repOpts.SortSequence == 3)
                            dt = dt.Select(null, dt.Columns[0].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repOpts.SortSequence == 4)
                            dt = dt.Select(null, dt.Columns[4].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repOpts.SortSequence == 5)
                            dt = dt.Select(null, dt.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repOpts.SortSequence == 6)
                            dt = dt.Select(null, dt.Columns[6].ColumnName, DataViewRowState.Added).CopyToDataTable();
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        DataSet11.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Customer = row[0].ToString();
                        nr.CutSheet = row[1].ToString();
                        nr.LineNo = row[2].ToString();
                        nr.TransDate = Convert.ToDateTime(row[3].ToString());
                        nr.Style = row[4].ToString();
                        nr.Size = row[5].ToString();
                        nr.Colour = row[6].ToString();
                        nr.BoxNumber = row[7].ToString();
                        nr.Grade = row[8].ToString();
                        nr.Qty = Convert.ToInt32(row[9].ToString());
                        nr.EarMarked = row[10].ToString();
                        nr.Facility = row[11].ToString();
                        dataTable1.AddDataTable1Row(nr);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet11.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(dataTable1);

                CMTStockOnHand soh = new CMTStockOnHand();
                soh.SetDataSource(ds);
                crystalReportViewer1.ReportSource = soh;
            }
            else if (_RepNo == 12)  // Report on CMT Production  (By Boxes)
            {
                DataSet ds = new DataSet();
                DataSet12.DataTable1DataTable dataTable1 = new DataSet12.DataTable1DataTable();
                DataSet12.DataTable2DataTable dataTable2 = new DataSet12.DataTable2DataTable();
                IList<TLCMT_CompletedWork> Completed = new List<TLCMT_CompletedWork>();

                _repos = new CMTRepository();

                DataSet12.DataTable1Row nr = dataTable1.NewDataTable1Row();
                nr.Pk = 1;
                nr.FromDate = _repOpts.fromDate;
                nr.ToDate = _repOpts.toDate;

                using (var context = new TTI2Entities())
                {
                    if (_repOpts.WhseNoSelected != 0)
                    {
                        nr.Factory = context.TLADM_Departments.Find(_repOpts.WhseNoSelected).Dep_Description;
                    }
                    else
                    {
                        nr.Factory = "ALL";
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("CMT Production for the period ");
                    if (_repOpts.NoOfGarments)
                        sb.Append(" by number of garments");
                    else
                        sb.Append(" by number of boxes");

                    nr.ReportTitle = sb.ToString();

                    dataTable1.AddDataTable1Row(nr);

                    Completed = _repos.CMTCompletedWork(_QueryParms).ToList();
                    var CompletedGrouped = Completed.Where(x => x.TLCMTWC_TransactionDate >= _repOpts.fromDate && x.TLCMTWC_TransactionDate <= _repOpts.toDate).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();

                    foreach (var Grp in CompletedGrouped)
                    {
                        DataSet12.DataTable2Row dtr = dataTable2.NewDataTable2Row();

                        dtr.Pk = 1;
                        int CS_FK = Grp.FirstOrDefault().TLCMTWC_CutSheet_FK;

                        var LI = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CS_FK).FirstOrDefault();
                        if (LI != null)
                        {
                            if (_repOpts.WhseNoSelected != 0 && _repOpts.WhseNoSelected != LI.TLCMTLI_CmtFacility_FK)
                                continue;

                            dtr.Factory = context.TLADM_Departments.Find(LI.TLCMTLI_CmtFacility_FK).Dep_Description;

                            if (_repOpts.LineNoSelected != 0 && _repOpts.LineNoSelected != LI.TLCMTLI_LineNo_FK)
                                continue;
                            dtr.LineNo = context.TLCMT_FactConfig.Find(LI.TLCMTLI_LineNo_FK).TLCMTCFG_Description;

                        }

                        var CS = context.TLCUT_CutSheet.Find(CS_FK);

                        if (CS != null)
                        {
                            dtr.CutSheet = CS.TLCutSH_No;

                            if (_repOpts.GreigeNoSelected != 0 && _repOpts.GreigeNoSelected != CS.TLCutSH_Quality_FK)
                                continue;
                            dtr.Product = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;

                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                if (_repOpts.ColourSelected != 0 && _repOpts.ColourSelected != DB.DYEB_Colour_FK)
                                    continue;

                                dtr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DB.DYEB_Colour_FK).Col_Display;
                            }
                        }

                        if (_repOpts.NoOfBoxes)
                        {
                            dtr.AGrades = Grp.Where(x => x.TLCMTWC_Grade == "A").Count();
                            dtr.BGrades = Grp.Where(x => x.TLCMTWC_Grade != "A").Count();
                            dtr.Total = dtr.AGrades + dtr.BGrades;
                            if (dtr.BGrades > 0)
                            {
                                decimal result = (decimal)dtr.BGrades / (decimal)dtr.Total;

                                dtr.BGradePercent = result * 100;
                            }
                            else
                                dtr.BGradePercent = 0.0M;
                            dtr.BoxedQty = Grp.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                        }
                        else
                        {
                            dtr.AGrades = Grp.Where(x => x.TLCMTWC_Grade == "A").Sum(x => x.TLCMTWC_Qty);
                            dtr.BGrades = Grp.Where(x => x.TLCMTWC_Grade != "A").Sum(x => x.TLCMTWC_Qty);
                            dtr.Total = dtr.AGrades + dtr.BGrades;
                            if (dtr.BGrades > 0)
                            {
                                decimal result = (decimal)dtr.BGrades / (decimal)dtr.Total;

                                dtr.BGradePercent = result * 100;
                            }
                        }

                        dataTable2.AddDataTable2Row(dtr);

                    }

                }
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet12.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                    dtr.Pk = 1;
                    dtr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable2.AddDataTable2Row(dtr);
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CMTProduction prod = new CMTProduction();
                if (_repOpts.SortSequence == 1)
                {
                    prod.DataDefinition.Groups[1].ConditionField = prod.Database.Tables[1].Fields[2];
                }
                else if (_repOpts.SortSequence == 2)
                {
                    prod.DataDefinition.Groups[1].ConditionField = prod.Database.Tables[1].Fields[3];
                }

                prod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = prod;

                IEnumerator ie = prod.Section2.ReportObjects.GetEnumerator();

                while (ie.MoveNext())
                {
                    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                        if (to.Name == "Text16")
                        {
                            to.Text = "B Grade %";
                        }


                    }
                }
            }
            else if (_RepNo == 13)  // Report on CMT Production  (By Units)
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable dataTable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable dataTable2 = new DataSet13.DataTable2DataTable();
                StringBuilder sb = new StringBuilder();
                IList<TLCMT_CompletedWork> Completed = new List<TLCMT_CompletedWork>();

                _repos = new CMTRepository();

                DataSet13.DataTable1Row nr = dataTable1.NewDataTable1Row();
                nr.Pk = 1;
                nr.FromDate = _repOpts.fromDate;
                nr.ToDate = _repOpts.toDate;

                using (var context = new TTI2Entities())
                {
                    sb.Append("Stock On Hand (Units) in Despatch Cage");
                    if (_repOpts.SortSequence == 4)
                    {
                        sb.Append(" Grouped by CMT");
                    }

                    nr.ReportTitle = sb.ToString();

                    dataTable1.AddDataTable1Row(nr);

                    Completed = _repos.CMTCompletedWorkNotDespatched(_QueryParms).ToList();
                    var CompletedGrouped = Completed.Where(x => x.TLCMTWC_TransactionDate >= _repOpts.fromDate && x.TLCMTWC_TransactionDate <= _repOpts.toDate).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();
                    foreach (var Grp in CompletedGrouped)
                    {
                        DataSet13.DataTable2Row dtr = dataTable2.NewDataTable2Row();

                        dtr.Pk = 1;
                        int CS_FK = Grp.FirstOrDefault().TLCMTWC_CutSheet_FK;
                        dtr.TransDate = Grp.FirstOrDefault().TLCMTWC_TransactionDate;
                        var LI = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CS_FK).FirstOrDefault();
                        if (LI != null)
                        {
                            if (_repOpts.WhseNoSelected != 0 && _repOpts.WhseNoSelected != LI.TLCMTLI_CmtFacility_FK)
                                continue;

                            if (_repOpts.LineNoSelected != 0 && _repOpts.LineNoSelected != LI.TLCMTLI_LineNo_FK)
                                continue;
                        }

                        var CS = context.TLCUT_CutSheet.Find(CS_FK);
                        if (CS != null)
                        {
                            dtr.Size = _Sizes.FirstOrDefault(s => s.SI_id == CS.TLCutSH_Size_FK).SI_Description;
                            dtr.CutSheet = CS.TLCutSH_No;
                            if (_repOpts.GreigeNoSelected != 0 && _repOpts.GreigeNoSelected != CS.TLCutSH_Quality_FK)
                                continue;

                            dtr.Quality = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                if (_repOpts.ColourSelected != 0 && _repOpts.ColourSelected != DB.DYEB_Colour_FK)
                                    continue;

                                dtr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DB.DYEB_Colour_FK).Col_Display;
                            }
                        }

                        dtr.AGrade = Grp.Where(x => x.TLCMTWC_Grade == "A").Sum(x => x.TLCMTWC_Qty);
                        dtr.BGrade = Grp.Where(x => x.TLCMTWC_Grade == "B").Sum(x => x.TLCMTWC_Qty);
                        dtr.Total = dtr.AGrade + dtr.BGrade;
                        dtr.Boxes = Grp.Count();
                        dtr.CMT = context.TLADM_Departments.Find(Grp.FirstOrDefault().TLCMTWC_CMTFacility_FK).Dep_Description;

                        dataTable2.AddDataTable2Row(dtr);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet13.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                    dtr.Pk = 1;
                    dtr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable2.AddDataTable2Row(dtr);
                }

                DespacthCageSOH prod = new DespacthCageSOH();
                prod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = prod;

                IEnumerator ie = prod.Section2.ReportObjects.GetEnumerator();

                while (ie.MoveNext())
                {
                    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                        if (to.Name == "Text16")
                        {
                            to.Text = "B Grade %";
                        }


                    }
                }
            }
            else if (_RepNo == 14)  // Panel Issue transaction --- OutStanding deliveries
            {
                DataSet ds = new DataSet();
                DataSet14.DataTable1DataTable dataTable1 = new DataSet14.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Receipted && !x.CMTPI_Closed);
                    foreach (var row in Existing)
                    {
                        var ExistingDetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == row.CMTPI_Pk).ToList();
                        foreach (var rowDetail in ExistingDetail)
                        {
                            DataSet14.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            var CSR = context.TLCUT_CutSheetReceipt.Find(rowDetail.CMTPID_CutSheet_FK);
                            if (CSR != null)
                            {
                                nr.CutSheet = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK).TLCutSH_No;
                            }

                            nr.DeliveryNumber = row.CMTPI_Number;
                            nr.TransDate = row.CMTPI_Date;

                            nr.OriginatingDept = context.TLADM_Departments.Find(row.CMTPI_Department_FK).Dep_Description;
                            var TranType = context.TLADM_TranactionType.Find(row.CMTPI_TranType_FK);
                            if (TranType != null)
                            {
                                nr.FromWhse = context.TLADM_WhseStore.Find(TranType.TrxT_FromWhse_FK).WhStore_Description;
                                nr.ToWhse = context.TLADM_WhseStore.Find(TranType.TrxT_ToWhse_FK).WhStore_Description;
                            }

                            var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (Boxes != null)
                            {
                                nr.AdultBoxes = Boxes.TLCUTSHB_AdultBoxes;
                                nr.KidsBoxes = Boxes.TLCUTSHB_KidBoxes;
                            }

                            dataTable1.AddDataTable1Row(nr);

                        }
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet14.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "There are no records currently available";
                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);
                CMTPanelsInTransit panTransit = new CMTPanelsInTransit();
                panTransit.SetDataSource(ds);
                crystalReportViewer1.ReportSource = panTransit;

            }
            else if (_RepNo == 15)  // CMT Work in Progresss
            {
                DataSet ds = new DataSet();
                DataSet15.DataTable1DataTable dataTable1 = new DataSet15.DataTable1DataTable();
                _repos = new CMTRepository();

                using (var context = new TTI2Entities())
                {
                    var Existing = _repos.CMTWIP(_QueryParms);
                    Existing = Existing.Where(x => x.TLCMTLI_TransferDate >= _repOpts.fromDate && x.TLCMTLI_TransferDate <= _repOpts.toDate);

                    foreach (var row in Existing)
                    {
                        DataSet15.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.CMTDetails = context.TLADM_Departments.Find(row.TLCMTLI_CmtFacility_FK).Dep_Description;
                        nr.TransDate = (DateTime)row.TLCMTLI_TransferDate;
                        nr.Line_Number = context.TLCMT_FactConfig.Find(row.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                        nr.CutSheet = context.TLCUT_CutSheet.Find(row.TLCMTLI_CutSheet_FK).TLCutSH_No;
                        nr.BundleQty = row.TLCMTLI_IssueQty;

                        var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == row.TLCMTLI_CutSheet_FK).FirstOrDefault();
                        if (CSR != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            var Pk = CSR.TLCUTSHR_Style_FK;
                            sb.Append(_Styles.FirstOrDefault(s => s.Sty_Id == Pk).Sty_Description);
                            Pk = CSR.TLCUTSHR_Colour_FK;
                            sb.Append(_Colours.FirstOrDefault(s => s.Col_Id == Pk).Col_Display);
                            IList<TLCUT_CutSheetReceiptDetail> CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                            StringBuilder SizeConcat = new StringBuilder();
                            foreach (var Record in CSRD)
                            {
                                var Size_FK = Record.TLCUTSHRD_Size_FK;
                                var SizeDesc = _Sizes.FirstOrDefault(s => s.SI_id == Size_FK).SI_Description;
                                if (SizeConcat.Length == 0)
                                    SizeConcat.Append(SizeDesc);
                                else
                                    if (!SizeConcat.ToString().Contains(SizeDesc))
                                    SizeConcat.Append(", " + SizeDesc);
                            }

                            sb.Append(" " + SizeConcat.ToString());

                            nr.Description = sb.ToString();
                            nr.Panels = CSRD.Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;

                        }
                        dataTable1.AddDataTable1Row(nr);
                    }
                }
                if (dataTable1.Rows.Count == 0)
                {
                    DataSet15.DataTable1Row nr = dataTable1.NewDataTable1Row();

                    nr.ErrorLog = "There are no records pertaining to selection made";

                    dataTable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(dataTable1);
                CMTWIP wip = new CMTWIP();
                wip.SetDataSource(ds);
                crystalReportViewer1.ReportSource = wip;
            }
            else if (_RepNo == 16)  // BFA Audit Numbers
            {
                DataSet ds = new DataSet();
                DataSet16.DataTable1DataTable dataTable1 = new DataSet16.DataTable1DataTable();
                DataSet16.DataTable2DataTable dataTable2 = new DataSet16.DataTable2DataTable();

                IList<TLCMT_AuditMeasureRecorded> AMRecorded = new List<TLCMT_AuditMeasureRecorded>();
                _repos = new CMTRepository();

                AMRecorded = _repos.SelectAMrecorded(_QueryParms).ToList();

                if (_repOpts.fromDate.Day != _repOpts.toDate.Day)
                    AMRecorded = AMRecorded.Where(x => x.TLDFAR_Date >= _repOpts.fromDate && x.TLDFAR_Date <= _repOpts.toDate).ToList();

                using (var context = new TTI2Entities())
                {
                    DataSet16.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                    hnr.Pk = 1;
                    hnr.FromDate = _repOpts.fromDate;
                    hnr.ToDate = _repOpts.toDate;
                    if (_repOpts.fromDate.Day != _repOpts.toDate.Day)
                        hnr.Title = "Bulk Final Audit Measurements recorded for the period from " + Convert.ToDateTime(_repOpts.fromDate.ToShortDateString()).ToString("dd/MM/yyyy")
                                       + " To " + Convert.ToDateTime(_repOpts.toDate.ToShortDateString()).ToString("dd/MM/yyyy");
                    else
                        hnr.Title = "Bulk Final Audit Measurements recorded";

                    dataTable2.AddDataTable2Row(hnr);

                    foreach (var row in AMRecorded)
                    {
                        DataSet16.DataTable1Row nr = dataTable1.NewDataTable1Row();

                        TLCUT_CutSheet cs = context.TLCUT_CutSheet.Find(row.TLBFAR_CutSheet_FK);
                        if (cs != null)
                        {
                            if (_QueryParms.Styles.Count != 0)
                            {
                                var sel = _QueryParms.Styles.Find(s => s.Sty_Id == cs.TLCutSH_Styles_FK);
                                if (sel == null)
                                    continue;
                            }

                            if (_QueryParms.Sizes.Count != 0)
                            {
                                var sel = _QueryParms.Sizes.Find(s => s.SI_id == cs.TLCutSH_Size_FK);
                                if (sel == null)
                                    continue;
                            }

                            nr.CutSheet = cs.TLCutSH_No;

                            nr.MProd1 = 0.0M;
                            var AuditM = context.TLCMT_AuditMeasurements.Find(row.TLBFAR_AuditMeasure_FK);
                            if (AuditM != null)
                            {
                                nr.MProd1 = AuditM.CMTBFA_Measurement;
                                var MeasureStandard = context.TLADM_CMTMeasurementPoints.Find(AuditM.CMTBFA_MeasureP_FK);
                                if (MeasureStandard != null)
                                {
                                    nr.MeasurementPoint = MeasureStandard.CMTMP_Description;
                                }
                            }
                        }

                        nr.CMTFacility = context.TLADM_Departments.Find(row.TLBFAR_Department_FK).Dep_Description;
                        nr.Pk = 1;
                        nr.Date = row.TLDFAR_Date;
                        nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == row.TLDFAR_Size_FK).SI_Description;
                        nr.Prod1 = row.TLDFAR_Prod1;
                        nr.Prod2 = row.TLDFAR_Prod2;
                        nr.Prod3 = row.TLDFAR_Prod3;
                        nr.Prod4 = row.TLDFAR_Prod4;
                        nr.Prod5 = row.TLDFAR_Prod5;
                        nr.Prod6 = row.TLDFAR_Prod6;
                        nr.Prod7 = row.TLDFAR_Prod7;
                        nr.Prod8 = row.TLDFAR_Prod8;
                        nr.Prod9 = row.TLDFAR_Prod9;
                        nr.Prod10 = row.TLDFAR_Prod10;

                        dataTable1.AddDataTable1Row(nr);
                    }


                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet16.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        if (_repOpts.CMT != 0)
                        {
                            nr.CMTFacility = context.TLADM_Departments.Find(_repOpts.CMT).Dep_Description;
                        }

                        nr.ErrorLog = "There are no records pertaining to selection made";
                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                CMTBFAResults results = new CMTBFAResults();
                results.SetDataSource(ds);
                crystalReportViewer1.ReportSource = results;
            }
            else if (_RepNo == 17)  // Line Feeder Quality Checklist
            {
                DataSet ds = new DataSet();
                DataSet17.DataTable1DataTable dataTable1 = new DataSet17.DataTable1DataTable();
                DataSet17.DataTable2DataTable dataTable2 = new DataSet17.DataTable2DataTable();

                DataSet17.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                hnr.Pk = 1;
                hnr.FromDate = _repOpts.fromDate;
                hnr.ToDate = _repOpts.toDate;
                hnr.ReportTitle = _repOpts.ReportTitle;
                dataTable2.AddDataTable2Row(hnr);

                using (var context = new TTI2Entities())
                {
                    var existing = context.TLCMT_LineFeederBundleCheck.Where(x => x.TLCMTFL_TransDate >= _repOpts.fromDate && x.TLCMTFL_TransDate <= _repOpts.toDate).ToList();
                    foreach (var row in existing)
                    {
                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.BundleDescription = row.TLCMTLF_Bundle_No;
                        nr.BodyQty = row.TLCMTLF_Body_Qty;
                        nr.SleeveQty = row.TLCMTLF_Sleeve_Qty;
                        nr.LabelsQty = row.TLCMTLF_Labels_Qty;
                        nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == row.TLCMTLF_Size_FK).SI_Description;
                        nr.Comments = row.TLCMTFL_Comments;
                        if (_repOpts.SLFCmt != 0 && row.TLCMTLF_Facility_FK != _repOpts.SLFCmt)
                            continue;
                        nr.Facility = context.TLADM_Departments.Find(row.TLCMTLF_Facility_FK).Dep_Description;
                        nr.Supervisor = context.TLADM_MachineOperators.Find(row.TLCMTLF_Operator_FK).MachOp_Description;
                        if (_repOpts.SLFCmtSupervisor != 0 && row.TLCMTLF_Operator_FK != _repOpts.SLFCmtSupervisor)
                            continue;
                        if (_repOpts.SLFCmtLine != 0 && row.TLCMTLF_LineNo_FK != _repOpts.SLFCmtLine)
                            continue;
                        nr.Line = context.TLCMT_FactConfig.Find(row.TLCMTLF_LineNo_FK).TLCMTCFG_Description;
                        var CS = context.TLCUT_CutSheet.Find(row.TLCMTLF_CutSheet_FK);
                        if (CS != null)
                        {
                            if (_repOpts.SLFCmtStyle != 0 && CS.TLCutSH_Styles_FK != _repOpts.SLFCmtStyle)
                                continue;
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCMTLF_Colour_FK).Col_Display;

                        }
                        bool first = true;

                        var ExistingRP = context.TLCMT_BodyMeasureRP.Where(x => x.TLBMPRP_BundleNo == row.TLCMTLF_Bundle_No).ToList();
                        foreach (var rowMP in ExistingRP)
                        {
                            if (!first)
                            {
                                nr = dataTable1.NewDataTable1Row();
                                nr.Pk = 1;
                            }
                            nr.Facility = context.TLADM_Departments.Find(row.TLCMTLF_Facility_FK).Dep_Description;
                            nr.Line = context.TLCMT_FactConfig.Find(row.TLCMTLF_LineNo_FK).TLCMTCFG_Description;
                            nr.BodyMeasure = context.TLADM_CMTMeasurementPoints.Find(rowMP.TLBMPRP_Measurement_FK).CMTMP_Description;
                            nr.RequiredMeasure = rowMP.TLBMPRP_RequiredMeasure;
                            nr.Supervisor = context.TLADM_MachineOperators.Find(row.TLCMTLF_Operator_FK).MachOp_Description;
                            if (CS != null)
                            {
                                nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCMTLF_Colour_FK).Col_Display;
                            }
                            nr.RPTop = rowMP.TLBMPRP_Top;
                            nr.RPMiddle = rowMP.TLBMPRP_Middle;
                            nr.RPBottom = rowMP.TLBMPRP_Bottom;
                            nr.Comments = row.TLCMTFL_Comments;

                            dataTable1.AddDataTable1Row(nr);

                            first = false;
                        }
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.ErrorMessage = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                LineFeederQuality lfq = new LineFeederQuality();

                if (_repOpts.SLFReportOption == 1)
                {
                    try
                    {
                        lfq.DataDefinition.Groups[0].ConditionField = lfq.Database.Tables[0].Fields[12];
                        lfq.DataDefinition.Groups[1].ConditionField = lfq.Database.Tables[0].Fields[13];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_repOpts.SLFReportOption == 2)
                {
                    try
                    {
                        lfq.DataDefinition.Groups[0].ConditionField = lfq.Database.Tables[0].Fields[12];
                        lfq.DataDefinition.Groups[1].ConditionField = lfq.Database.Tables[0].Fields[14];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_repOpts.SLFReportOption == 3)
                {
                    try
                    {
                        lfq.DataDefinition.Groups[0].ConditionField = lfq.Database.Tables[0].Fields[12];
                        lfq.DataDefinition.Groups[1].ConditionField = lfq.Database.Tables[0].Fields[16];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_repOpts.SLFReportOption == 4)
                {
                    try
                    {
                        lfq.DataDefinition.Groups[0].ConditionField = lfq.Database.Tables[0].Fields[12];
                        lfq.DataDefinition.Groups[1].ConditionField = lfq.Database.Tables[0].Fields[15];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                lfq.SetDataSource(ds);
                crystalReportViewer1.ReportSource = lfq;

            }
            else if (_RepNo == 18)  // CMT Costing Analysis
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                int Style_FK = 0;
                int Colour_FK = 0;
                int Size_FK = 0;
                int LineIssue_FK = 0;
                Util core = new Util();

                String FileName = string.Empty;

                using (var context = new TTI2Entities())
                {
                    var ExistingGroups = (from ComW in context.TLCMT_CompletedWork
                                          join li in context.TLCMT_LineIssue on ComW.TLCMTWC_LineIssue_FK equals li.TLCMTLI_Pk
                                          where li.TLCMTLI_CmtFacility_FK == _repOpts.SLFCmt && !ComW.TLCMTWC_CMTBilled && ComW.TLCMTWC_TransactionDate <= _repOpts.toDate
                                          select ComW).GroupBy(x => x.TLCMTWC_LineIssue_FK).ToList();

                    foreach (var Group in ExistingGroups)
                    {

                        var SizeList = (from i in Group
                                        join o in context.TLADM_Sizes
                                        on i.TLCMTWC_Size_FK equals o.SI_id
                                        orderby o.SI_DisplayOrder
                                        select i);

                        StringBuilder SizeConcat = new StringBuilder();


                        foreach (var Record in SizeList)
                        {
                            Style_FK = Record.TLCMTWC_Style_FK;
                            Colour_FK = Record.TLCMTWC_Colour_FK;
                            Size_FK = Record.TLCMTWC_Size_FK;
                            LineIssue_FK = Record.TLCMTWC_LineIssue_FK;

                            var SizeDesc = _Sizes.FirstOrDefault(s => s.SI_id == Size_FK).SI_Description;
                            if (SizeConcat.Length == 0)
                                SizeConcat.Append(SizeDesc);
                            else
                                if (!SizeConcat.ToString().Contains(SizeDesc))
                                SizeConcat.Append(", " + SizeDesc);

                        }
                        //=====================================================
                        // Find The Line Issue Record
                        //=========================================
                        var LineIssue = context.TLCMT_LineIssue.Find(LineIssue_FK);
                        if (LineIssue != null)
                        {
                            DataSet19.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                            NewRow.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Style_FK).Sty_Description;
                            NewRow.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Colour_FK).Col_Display;
                            NewRow.Size = SizeConcat.ToString();
                            NewRow.CMTDetails = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;
                            NewRow.LineIssuedNo = LineIssue.TLCMTLI_CutSheetDetails;
                            NewRow.LineDescription = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                            NewRow.Notes = string.Empty;

                            // Find the Standard Costs;
                            //==========================================
                            var StdCosts = context.TLCMT_ProductionCosts.Where(x => x.CMTP_CMTFacility_FK == LineIssue.TLCMTLI_CmtFacility_FK && x.CMTP_Style_FK == Style_FK &&
                                                                                         x.CMTP_Colour_FK == Colour_FK &&
                                                                                         x.CMTP_Size_FK == Size_FK).FirstOrDefault();
                            if (StdCosts != null)
                            {
                                NewRow.ManuFaultCost = StdCosts.CMTP_Production_Damage;
                                NewRow.ManuShortCost = StdCosts.CMTP_Production_Loss;
                                NewRow.ManuCost = StdCosts.CMTP_Production_Cost;
                            }
                            else
                            {
                                NewRow.ManuFaultCost = 0.00M;
                                NewRow.ManuShortCost = 0.00M;
                                NewRow.ManuCost = 0.00M;
                                NewRow.Notes = "Note : A";
                            }

                            //================================================================
                            // Find the Statistics;
                            //==========================================
                            NewRow.ManuShortQty = 0;
                            NewRow.ManuQty = 0;
                            NewRow.ManuFaultQty = 0;

                            var Stats = context.TLCMT_Statistics.Where(x => x.CMTS_PanelIssue_FK == LineIssue_FK).FirstOrDefault();
                            if (Stats != null)
                            {
                                NewRow.ManuQty = Stats.CMTS_Total_A_Grades + Stats.CMTS_Total_B_Grades;

                                if (Stats.CMTS_Total_Difference > 0)
                                    NewRow.ManuShortQty = Stats.CMTS_Total_Difference;
                                else
                                    NewRow.ManuShortQty = 0;


                            }
                            else
                            {
                                if (String.IsNullOrEmpty(NewRow.Notes))
                                    NewRow.Notes += "B";
                                else
                                    NewRow.Notes += " / B";
                            }


                            //===========================================================
                            //Find the faults
                            //===================================================
                            var PF = from PFaults in context.TLCMT_ProductionFaults
                                     join DFlaw in context.TLCMT_DeflectFlaw on PFaults.TLCMTPF_Fault_FK equals DFlaw.TLCMTDF_Pk
                                     where PFaults.TLCMTPF_PanelIssue_FK == LineIssue_FK && DFlaw.TLCMTDF_Manufacturing
                                     select PFaults;

                            NewRow.ManuFaultQty = PF.Sum(x => (int?)x.TLCMTPF_Qty) ?? 0;

                            NewRow.ManuTotal = NewRow.ManuQty * NewRow.ManuCost;
                            NewRow.ManuFaultTotal = NewRow.ManuFaultQty * NewRow.ManuFaultCost;
                            NewRow.ManuShortTotal = NewRow.ManuShortQty * NewRow.ManuShortCost;

                            NewRow.ReportHeader = _repOpts.toDate;
                            dataTable1.AddDataTable1Row(NewRow);
                        }
                    }

                    DataView DataV = dataTable1.DefaultView;
                    DataV.Sort = "LineIssuedNo";

                    ds.Tables.Add(DataV.ToTable());
                    CMTCostAnalysis CostAnalysis = new CMTCostAnalysis();
                    CostAnalysis.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CostAnalysis;
                }
            }
            else if (_RepNo == 19)  // CMT Costing Analysis
            {
                DataSet ds = new DataSet();
                DataSet20.DataTable1DataTable dataTable = new DataSet20.DataTable1DataTable();
                _repos = new CMTRepository();

                var Existing = _repos.CMTProductionsCosts(_QueryParms);

                using (var context = new TTI2Entities())
                {
                    foreach (var Record in Existing)
                    {
                        DataSet20.DataTable1Row nr = dataTable.NewDataTable1Row();

                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Record.CMTP_Style_FK).Sty_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Record.CMTP_Colour_FK).Col_Description;
                        nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Record.CMTP_Size_FK).SI_Description;
                        nr.ProdCost = Record.CMTP_Production_Cost;
                        nr.ProdLoss = Record.CMTP_Production_Loss;
                        nr.ProdShort = Record.CMTP_Production_Damage;
                        nr.Dept = context.TLADM_Departments.Find(Record.CMTP_CMTFacility_FK).Dep_Description;
                        dataTable.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable);
                CMTCostValues CostValues = new CMTCostValues();
                CostValues.SetDataSource(ds);
                crystalReportViewer1.ReportSource = CostValues;
            }
            else if (_RepNo == 20)  // CMT Completed Work Analysis
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable1 = new DataSet21.DataTable1DataTable();
                DataSet21.DataTable2DataTable dataTable2 = new DataSet21.DataTable2DataTable();
                string[][] ColumnNames = null;

                IList<TLCMT_DeflectFlaw> Reasons;

                core = new Util();
                _repos = new CMTRepository();

                // DJL
                using (var context = new TTI2Entities())
                {
                    Reasons = context.TLCMT_DeflectFlaw.ToList();

                    DataSet21.DataTable2Row t2 = dataTable2.NewDataTable2Row();
                    t2.Title = "CMT Completed Work Analysis";
                    t2.FromDate = _repOpts.fromDate;
                    t2.ToDate = _repOpts.toDate;
                    t2.Pk = 1;
                    dataTable2.AddDataTable2Row(t2);

                    var CompletedWork = _repos.CMTCompletedWork(_QueryParms).ToList();
                    var ExistingGroups = CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= _repOpts.fromDate && x.TLCMTWC_TransactionDate <= _repOpts.toDate).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();


                    foreach (var Group in ExistingGroups)
                    {
                        StringBuilder SizeConcat = new StringBuilder();

                        var xSize = Group.GroupBy(x => x.TLCMTWC_Size_FK);
                        foreach (var Record in xSize)
                        {
                            var Pk = Record.FirstOrDefault().TLCMTWC_Size_FK;
                            var SizeDesc = _Sizes.FirstOrDefault(s => s.SI_id == Pk).SI_Description;
                            SizeConcat.Append(SizeDesc + ",");
                        }

                        DataSet21.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        String sz = SizeConcat.ToString();
                        nr.Size = sz.Remove(-1 + sz.Length, 1);
                        nr.TotalCS = 0;

                        TLCMT_CompletedWork comWork = Group.FirstOrDefault();

                        var CS = context.TLCUT_CutSheet.Find(comWork.TLCMTWC_CutSheet_FK);
                        if (CS != null)
                        {
                            nr.CSNO = CS.TLCutSH_No.Remove(0, 2);

                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                nr.BATCHNO = DB.DYEB_BatchNo.Remove(0, 2);
                                // Additional requirement to add Yarn Order to report
                                //======================================================
                                var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                if (DBD != null)
                                {
                                    var GP = context.TLKNI_GreigeProduction.Find(DBD.DYEBD_GreigeProduction_FK);
                                    if (GP != null)
                                    {
                                        if (!GP.GreigeP_CommisionCust && !(bool)GP.GreigeP_BoughtIn)
                                        {
                                            var KO = context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk);
                                            if (KO != null)
                                            {
                                                var YT = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KO.KnitO_Pk).FirstOrDefault();
                                                if (YT != null)
                                                {
                                                    var YOP = context.TLKNI_YarnOrderPallets.Find(YT.TLKYT_YOP_FK);
                                                    if (YOP != null)
                                                    {
                                                        var YO = context.TLSPN_YarnOrder.Find(YOP.TLKNIOP_YarnOrder_FK);
                                                        if (YO != null)
                                                        {
                                                            nr.YarnOrder = YO.YarnO_OrderNumber.ToString();
                                                        }
                                                        else
                                                            nr.YarnOrder = YOP.TLKNIOP_TLPalletNo;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                            if (CutSheetReceipt != null)
                            {
                                nr.TotalCS = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;
                            }
                        }


                        nr.NR = 0;
                        nr.Pk = 1;
                        nr.Week = core.GetIso8601WeekOfYear(comWork.TLCMTWC_TransactionDate); ;
                        nr.CMT = context.TLADM_Departments.Find(comWork.TLCMTWC_CMTFacility_FK).Dep_Description;

                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == comWork.TLCMTWC_Style_FK).Sty_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == comWork.TLCMTWC_Colour_FK).Col_Display;

                        var Existing = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_Pk == comWork.TLCMTWC_LineIssue_FK).FirstOrDefault();
                        if (Existing != null)
                        {
                            nr.LineNumber = context.TLCMT_FactConfig.Find(Existing.TLCMTLI_LineNo_FK).TLCMTCFG_LineNo.ToString();
                        }
                        //================================================================
                        // Find the Statistics;
                        //==========================================
                        nr.Panels = 0;
                        nr.Short = 0;
                        nr.PAGrade = 0.0M;
                        nr.PBGrade = 0.0M;

                        nr.Col1 = nr.Col2 = nr.Col3 = nr.Col4 = nr.Col5 = nr.Col6 = 0;
                        nr.Col7 = nr.Col8 = nr.Col9 = nr.Col10 = nr.Col11 = nr.Col12 = 0;

                        var Stats = context.TLCMT_Statistics.Where(x => x.CMTS_PanelIssue_FK == comWork.TLCMTWC_LineIssue_FK).FirstOrDefault();
                        if (Stats != null)
                        {
                            nr.Panels = Stats.CMTS_Panels;
                            nr.TotalIssued = Stats.CMTS_Total_A_Grades + Stats.CMTS_Total_B_Grades;
                            nr.Short = (Stats.CMTS_Total_A_Grades + Stats.CMTS_Total_B_Grades + Stats.CMTS_Panels) - nr.TotalCS;
                            nr.AGrade = Stats.CMTS_Total_A_Grades;
                            nr.BGrade = Stats.CMTS_Total_B_Grades;

                            if(nr.TotalIssued != 0 && nr.AGrade != 0)
                            {
                                nr.PAGrade = ((decimal)nr.AGrade / (decimal)nr.TotalIssued) * 100;
                            }
                            if (nr.TotalIssued != 0 && nr.BGrade != 0)
                            {
                                nr.PBGrade = ((decimal)nr.BGrade / (decimal)nr.TotalIssued) * 100;
                            }
                        }

                        //===========================================================
                        //Find the faults
                        //===================================================
                        var Cnt = 0;
                        var ProdFaults = context.TLCMT_ProductionFaults.Where(x => x.TLCMTPF_PanelIssue_FK == comWork.TLCMTWC_LineIssue_FK).ToList();
                        foreach (var Fault in ProdFaults)
                        {
                            Cnt = Fault.TLCMTPF_Fault_FK;

                            var ProdQuality = context.TLCMT_DeflectFlaw.Find(Fault.TLCMTPF_Fault_FK);
                            if (ProdQuality != null)
                            {
                                if (Fault.TLCMTPF_Fault_FK == 1)
                                    nr.Col1 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 2)
                                    nr.Col2 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 3)
                                    nr.Col3 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 4)
                                    nr.Col4 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 5)
                                    nr.Col5 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 6)
                                    nr.Col6 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 7)
                                    nr.Col7 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 8)
                                    nr.Col8 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 9)
                                    nr.Col9 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 10)
                                    nr.Col10 = Fault.TLCMTPF_Qty;
                                else if (Fault.TLCMTPF_Fault_FK == 11)
                                    nr.Col11 = Fault.TLCMTPF_Qty;
                                else
                                    nr.Col12 = Fault.TLCMTPF_Qty;
                            }
                        }

                       

                        if (nr.TotalCS > 0 && nr.BGrade > 0)
                        {
                            var Percentage = (decimal)nr.BGrade / (nr.AGrade + nr.BGrade) * 100;
                            if (_repOpts.Exception)
                            {
                                if (Percentage < _repOpts.Percentage_Exception)
                                    continue;
                            }
                            nr.Col13 = Percentage;
                        }

                        var Total_Faults = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7;
                        Total_Faults += nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11 + nr.Col12;

                        if(Total_Faults != 0)
                        {
                            if(nr.Col1 != 0)
                            {
                                nr.PCol1 = ((decimal)nr.Col1 / (decimal) Total_Faults) * 100;
                            }
                            if(nr.Col2 != 0)
                            {
                                nr.PCol2 = ((decimal)nr.Col2 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col3 != 0)
                            {
                                nr.PCol3 = ((decimal)nr.Col3 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col4 != 0)
                            {
                                nr.PCol4 = ((decimal)nr.Col4 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col5 != 0)
                            {
                                nr.PCol5 = ((decimal)nr.Col5 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col6 != 0)
                            {
                                nr.PCol6 = ((decimal)nr.Col6 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col7 != 0)
                            {
                                nr.PCol7 = ((decimal)nr.Col7 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col8 != 0 )
                            {
                                nr.PCol8 = ((decimal)nr.Col8 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col9 != 0)
                            {
                                nr.PCol9 = ((decimal)nr.Col9 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col10 != 0)
                            {
                                nr.PCol10 = ((decimal)nr.Col10 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col11 != 0)
                            {
                                nr.PCol11 = ((decimal)nr.Col11 / (decimal)Total_Faults) * 100;
                            }
                            if(nr.Col12 != 0)
                            {
                                nr.PCol2 = ((decimal)nr.Col12 / (decimal)Total_Faults) * 100;
                            }
                        }
                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ColumnNames = new string[][]
                {   new string[] {"Text20", "1", ""},
                        new string[] {"Text21", "2", ""},
                        new string[] {"Text22", "3", ""},
                        new string[] {"Text23", "4", ""},
                        new string[] {"Text24", "5", ""},
                        new string[] {"Text25", "6", ""},
                        new string[] {"Text26", "7", ""},
                        new string[] {"Text27", "8", ""} ,
                        new string[] {"Text28", "9", ""},
                        new string[] {"Text29", "10",""},
                        new string[] {"Text30", "11", ""},
                        new string[] {"Text31", "12", ""}
                };

                foreach (var Reason in Reasons)
                {
                    var result = (from u in ColumnNames
                                  where u[1] == Reason.TLCMTDF_Pk.ToString()
                                  select u).FirstOrDefault();

                    if (result != null)
                        result[2] = Reason.TLCMTDF_ShortCode;

                }


                if (_repOpts.WorkAnalysis == 2 || _repOpts.WorkAnalysis == 3)
                {
                    CMTFinishedWAnalysis CostValues = new CMTFinishedWAnalysis();
                    CostValues.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CostValues;

                    if (_repOpts.QAReport)
                        CostValues.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;

                    if (_repOpts.WorkAnalysis == 2)
                    {
                        CostValues.DataDefinition.Groups[1].ConditionField = CostValues.Database.Tables[0].Fields[6];
                    }
                    else
                    {
                        CostValues.DataDefinition.Groups[1].ConditionField = CostValues.Database.Tables[0].Fields[27];
                    }


                    IEnumerator ie = CostValues.Section3.ReportObjects.GetEnumerator();
                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.FieldObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.FieldObject fo = (CrystalDecisions.CrystalReports.Engine.FieldObject)ie.Current;
                                if (fo.Name == "LineNumber1")
                                {
                                    if (_repOpts.WorkAnalysis == 3)
                                    {
                                        fo.ObjectFormat.EnableSuppress = true;

                                    }
                                }

                                if (fo.Name == "Style1")
                                {
                                    if (_repOpts.WorkAnalysis == 2)
                                    {
                                        fo.ObjectFormat.EnableSuppress = true;

                                    }
                                }

                            }
                        }
                    }
                    catch (System.ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    ie = CostValues.Section2.ReportObjects.GetEnumerator();

                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;
                                var result = (from u in ColumnNames
                                              where u[0] == to.Name
                                              select u).FirstOrDefault();

                                if (to.Text.Contains("Line"))
                                {
                                    if (_repOpts.WorkAnalysis == 2)
                                    {
                                        to.Text = "LineNumber";
                                    }
                                    else
                                        to.Text = "Style";
                                }

                                if (result != null)
                                {
                                    to.Text = result[2];

                                }
                            }
                        }
                    }
                    catch (System.ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_repOpts.WorkAnalysis == 1)
                {
                    CMTFinWAnalysis CostValues = new CMTFinWAnalysis();
                    CostValues.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CostValues;

                    if (_repOpts.QAReport)
                        CostValues.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;

                    var ie = CostValues.Section2.ReportObjects.GetEnumerator();
                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;
                                var result = (from u in ColumnNames
                                              where u[0] == to.Name
                                              select u).FirstOrDefault();



                                if (result != null)
                                {
                                    to.Text = result[2];

                                }
                            }
                        }
                    }
                    catch (System.ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (_repOpts.WorkAnalysis == 4 || _repOpts.WorkAnalysis == 5)
                {
                    CMTFinWAnalBy CostValues = new CMTFinWAnalBy();
                    CostValues.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = CostValues;
                    if (_repOpts.QAReport)
                        CostValues.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;

                    if (_repOpts.WorkAnalysis == 4)
                    {
                        CostValues.DataDefinition.Groups[2].ConditionField = CostValues.Database.Tables[0].Fields[7];
                    }
                    else
                    {
                        CostValues.DataDefinition.Groups[2].ConditionField = CostValues.Database.Tables[0].Fields[8];
                    }


                    IEnumerator ie = CostValues.GroupHeaderSection3.ReportObjects.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                        {
                            CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;
                            if (_repOpts.WorkAnalysis == 4)
                            {
                                to.Text = "Colour";

                            }
                            else
                            {
                                to.Text = "Size";
                            }
                        }
                    }

                    ie = CostValues.Section3.ReportObjects.GetEnumerator();
                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.FieldObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.FieldObject fo = (CrystalDecisions.CrystalReports.Engine.FieldObject)ie.Current;
                                if (fo.Name == "Size1")
                                {
                                    if (_repOpts.WorkAnalysis == 5)
                                    {
                                        fo.ObjectFormat.EnableSuppress = true;

                                    }
                                }

                                if (fo.Name == "Colour1")
                                {
                                    if (_repOpts.WorkAnalysis == 4)
                                    {
                                        fo.ObjectFormat.EnableSuppress = true;

                                    }
                                }

                            }

                        }

                    }
                    catch (System.ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    ie = CostValues.Section2.ReportObjects.GetEnumerator();

                    ie.Reset();
                    try
                    {
                        while (ie.MoveNext())
                        {
                            if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                            {
                                CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;
                                var result = (from u in ColumnNames
                                              where u[0] == to.Name
                                              select u).FirstOrDefault();

                                if (to.Text.Contains("Size"))
                                {
                                    if (_repOpts.WorkAnalysis == 4)
                                    {
                                        to.Text = "Size";
                                    }
                                    else
                                        to.Text = "Colour";
                                }

                                if (result != null)
                                {
                                    to.Text = result[2];

                                }

                                if (result != null)
                                {
                                    to.Text = result[2];

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
            else if (_RepNo == 21)  // this is a temporary measure until the we can get the CMT onLine 
            {
                DataSet ds = new DataSet();
                DataSet18.DataTable1DataTable dataTable1 = new DataSet18.DataTable1DataTable();
                DataSet18.DataTable2DataTable dataTable2 = new DataSet18.DataTable2DataTable();

                core = new Util();
                int _Lable = 0;
                using (var context = new TTI2Entities())
                {
                    DataSet18.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    //---------------------------------------------------------
                    var Whse = context.TLADM_WhseStore.Where(x => x.WhStore_Code.Contains("PSBLW")).FirstOrDefault();
                    if (Whse != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(Whse.WhStore_Description + Environment.NewLine);
                        sb.Append(Whse.WhStore_Address1 + Environment.NewLine);
                        sb.Append(Whse.WhStore_Address3 + Environment.NewLine);
                        sb.Append(Whse.WhStore_Address4 + Environment.NewLine);
                        sb.Append(Whse.WhStore_Address5 + Environment.NewLine);
                        nr.IssuedTo = sb.ToString();
                    }

                    nr.Line = string.Empty; //  FactConfig.TLCMTCFG_LineNo.ToString();
                    nr.LineDescription = string.Empty; // FactConfig.TLCMTCFG_Description;


                    var CutSheet = context.TLCUT_CutSheet.Find(_RecKey);
                    if (CutSheet != null)
                    {
                        String CS = CutSheet.TLCutSH_No.Remove(0, 2);
                        nr.IssueNumber = "SI" + CS.PadLeft(5, '0');

                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;
                        nr.CutSheet = CutSheet.TLCutSH_No;
                        var DyeBatch = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                        if (DyeBatch != null)
                        {
                            nr.DyeBatch = DyeBatch.DYEB_BatchNo;

                            var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                            if (DyeOrder != null)
                            {
                                nr.BodyQuality = _Qualities.FirstOrDefault(S => S.TLGreige_Id == DyeOrder.TLDYO_Greige_FK).TLGreige_Description;

                                nr.Order = DyeOrder.TLDYO_DyeOrderNum;
                                var DtReq = core.FirstDateOfWeek(DyeOrder.TLDYO_OrderDate.Year, DyeOrder.TLDYO_CMTReqWeek);
                                nr.DateRequired = DtReq.AddDays(5);
                                _Lable = DyeOrder.TLDYO_Label_FK;

                                var DyeOrderDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder.TLDYO_Pk && !x.TLDYOD_BodyOrTrim).ToList();
                                var Count = 0;
                                foreach (var DyeOrderDetail in DyeOrderDetails)
                                {
                                    if (Count == 0)
                                        nr.TrimQuality1 = _Qualities.FirstOrDefault(S => S.TLGreige_Id == DyeOrderDetail.TLDYOD_Greige_FK).TLGreige_Description;
                                    else
                                    {
                                        nr.TrimQuality2 = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrderDetail.TLDYOD_Greige_FK).TLGreige_Description;
                                        break;
                                    }

                                    Count += 1;
                                }
                            }
                        }

                        var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                        if (CutSheetReceipt != null)
                        {
                            nr.Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_AdultBoxes;
                            nr.Ribbing = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Ribbing;
                            nr.Binding = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Binding;

                            var CutSheetDetails = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).ToList();

                            foreach (var CutSheetDetail in CutSheetDetails)
                            {
                                DataSet18.DataTable2Row dt2 = dataTable2.NewDataTable2Row();

                                dt2.Bundle = CutSheetDetail.TLCUTSHRD_Description;
                                dt2.Size = _Sizes.FirstOrDefault(s => s.SI_id == CutSheetDetail.TLCUTSHRD_Size_FK).SI_Description;
                                dt2.Style = _Qualities.FirstOrDefault(s => s.TLGreige_Id == CutSheet.TLCutSH_Quality_FK).TLGreige_Description;
                                dt2.Lable = context.TLADM_Labels.Find(_Lable).Lbl_Description;
                                dt2.Bodies = CutSheetDetail.TLCUTSHRD_BoxUnits;
                                dt2.Ribs = CutSheetDetail.TLCUTSHRD_BoxUnits;

                                dt2.Sleeves = CutSheetDetail.TLCUTSHRD_BoxUnits.ToString() + " x 2";
                                dataTable2.AddDataTable2Row(dt2);
                            }
                        }
                    }

                    nr.Date = DateTime.Now;

                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);


                CMTPanelIssue fcon = new CMTPanelIssue();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

            }
            else if (_RepNo == 22)  // this is a temporary measure until the we can get the CMT onLine 
            {
                DataSet ds = new DataSet();
                DataSet22.DataTable1DataTable dataTable1 = new DataSet22.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var CMTPanelStore = (from LI in context.TLCMT_LineIssue
                                         join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                                         join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                                         where LI.TLCMTLI_IssuedToLine == false
                                         select new { CRD, LI }).GroupBy(x => x.CRD.TLCUTSHRD_CutSheet_FK);

                    foreach (var Group in CMTPanelStore)
                    {
                        DataSet22.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.DespatchCage = 0;
                        nr.ReceiptCage = 0;
                        nr.WIP = 0;
                        var Pk = Group.FirstOrDefault().CRD.TLCUTSHRD_CutSheet_FK;
                        var CutSheet = context.TLCUT_CutSheet.Find(Pk);
                        if (CutSheet != null)
                        {
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            var Sizes = Group.GroupBy(x => x.CRD.TLCUTSHRD_Size_FK).ToList();
                            StringBuilder sb = new StringBuilder();
                            foreach (var Si in Sizes)
                            {
                                string desc = _Sizes.FirstOrDefault(s => s.SI_id == Si.Key).SI_Description;
                                sb.Append(desc + ",");

                            }

                            nr.Sizes = sb.ToString();
                            nr.Sizes.Remove(-1 + nr.Sizes.Length, 1);

                            nr.OnHold = Group.FirstOrDefault().LI.TLCMTLI_OnHold;
                            nr.CutSheet = CutSheet.TLCutSH_No;
                            nr.ReceiptCage = Group.Sum(x => (int?)(x.CRD.TLCUTSHRD_BundleQty - x.CRD.TLCUTSHRD_RejectQty)) ?? 0;
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }

                    CMTPanelStore = (from LI in context.TLCMT_LineIssue
                                     join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                                     join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                                     where LI.TLCMTLI_IssuedToLine && !LI.TLCMTLI_WorkCompleted
                                     select new { CRD, LI }).GroupBy(x => x.CRD.TLCUTSHRD_CutSheet_FK);

                    foreach (var Group in CMTPanelStore)
                    {
                        DataSet22.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.DespatchCage = 0;
                        nr.ReceiptCage = 0;
                        nr.WIP = 0;
                        var Pk = Group.FirstOrDefault().CRD.TLCUTSHRD_CutSheet_FK;
                        var CutSheet = context.TLCUT_CutSheet.Find(Pk);
                        if (CutSheet != null)
                        {
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            var Sizes = Group.GroupBy(x => x.CRD.TLCUTSHRD_Size_FK).ToList();
                            StringBuilder sb = new StringBuilder();
                            foreach (var Si in Sizes)
                            {
                                string desc = _Sizes.FirstOrDefault(s => s.SI_id == Si.Key).SI_Description;
                                sb.Append(desc + ",");

                            }

                            nr.Sizes = sb.ToString();

                            nr.OnHold = Group.FirstOrDefault().LI.TLCMTLI_OnHold;
                            nr.CutSheet = CutSheet.TLCutSH_No;
                            nr.WIP = Group.Sum(x => (int?)(x.CRD.TLCUTSHRD_BundleQty - x.CRD.TLCUTSHRD_RejectQty)) ?? 0;
                        }

                        dataTable1.AddDataTable1Row(nr);

                    }


                    //-------------------------------------------------------------------------------------------------------------
                    // 8th Task (c) Expected Units at CMT Store in Finished Goods Despatch Cage
                    //----------------------------------------------------------------------------------------------------
                    var QueryC = (from T1 in context.TLCMT_LineIssue
                                  join T2 in context.TLCMT_CompletedWork on T1.TLCMTLI_Pk equals T2.TLCMTWC_LineIssue_FK
                                  where !T2.TLCMTWC_Despatched
                                  select new { T1, T2 }).GroupBy(x => x.T1.TLCMTLI_CutSheet_FK);

                    foreach (var Q in QueryC)
                    {
                        DataSet22.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.DespatchCage = 0;
                        nr.ReceiptCage = 0;
                        nr.WIP = 0;
                        var Pk = Q.FirstOrDefault().T1.TLCMTLI_CutSheet_FK;
                        var CutSheet = context.TLCUT_CutSheet.Find(Pk);
                        if (CutSheet != null)
                        {
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            var Sizes = Q.GroupBy(x => x.T2.TLCMTWC_Size_FK).ToList();
                            StringBuilder sb = new StringBuilder();
                            foreach (var Si in Sizes)
                            {
                                string desc = _Sizes.FirstOrDefault(s => s.SI_id == Si.Key).SI_Description;
                                sb.Append(desc + ",");

                            }

                            nr.Sizes = sb.ToString();

                            nr.OnHold = Q.FirstOrDefault().T1.TLCMTLI_OnHold;
                            nr.CutSheet = CutSheet.TLCutSH_No;
                            nr.DespatchCage = Q.Sum(x => (int?)(x.T2.TLCMTWC_Qty)) ?? 0;
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }

                }

                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CutSheet";

                ds.Tables.Add(DataV.ToTable());

                CMTWIPAnalysis fcon = new CMTWIPAnalysis();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;
            }
            else if (_RepNo == 23)
            {
                DataSet ds = new DataSet();
                DataSet23.DataTable1DataTable datatable = new DataSet23.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var MeasurePoints = context.TLADM_CMTMeasurementPoints.Where(x => x.CMTMP_B2MRawPanels).ToList();

                    var CutSheet = context.TLCUT_CutSheet.Find(_repOpts.SLFCutSheetFK);
                    if (CutSheet != null)
                    {
                        var MeasureValues = context.TLCMT_AuditMeasurements.Where(x => x.CMTBFA_Customer_FK == CutSheet.TLCutSH_Customer_FK &&
                                                                              x.CMTBFA_Size_FK == CutSheet.TLCutSH_Size_FK &&
                                                                              x.CMTBFA_Style_FK == CutSheet.TLCutSH_Styles_FK).ToList();
                        var cnt = 0;
                        do
                        {
                            var MeasCount = 1;
                            bool First = true;
                            foreach (var Measure in MeasureValues)
                            {
                                if (MeasCount > 2)
                                    break;

                                DataSet23.DataTable1Row nr = datatable.NewDataTable1Row();
                                if (First)
                                {
                                    nr.BundleNumber = cnt + 1;
                                    First = !First;
                                }
                                nr.MeasureDescrip = MeasurePoints.Where(x => x.CMTMP_Pk == Measure.CMTBFA_MeasureP_FK).FirstOrDefault().CMTMP_Description;
                                nr.Required = Measure.CMTBFA_Measurement;
                                nr.Title = _repOpts.ReportTitle;

                                MeasCount += 1;
                                datatable.AddDataTable1Row(nr);
                            }
                        }
                        while (++cnt < 23);
                    }
                }

                ds.Tables.Add(datatable);
                LineFeederInput LFInput = new LineFeederInput();
                LFInput.SetDataSource(ds);
                crystalReportViewer1.ReportSource = LFInput;
            }
            else if (_RepNo == 24)
            {
                DataSet ds = new DataSet();
                DataSet24.DataTable1DataTable dataTable1 = new DataSet24.DataTable1DataTable();
                string[][] ColumnNames = null;
                List<TLCMT_LineIssue> LineIssues;
                Util core = new Util();

                _repos = new CMTRepository();
                LineIssues = _repos.CutsOnHold(_QueryParms).ToList();

                ColumnNames = new string[][]
                    {   new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text15", string.Empty},
                        new string[] {"Text16", string.Empty},
                        new string[] {"Text17", string.Empty},
                        new string[] {"Text18", string.Empty},
                        new string[] {"Text19", string.Empty}

                    };

                using (var context = new TTI2Entities())
                {
                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }
                    /*
                    var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).GroupBy(x => x.SI_ColNumber).ToList();
                    foreach (var xSize in Sizes)
                    {
                        xSize.OrderBy(x => x.SI_DisplayOrder);

                        StringBuilder Sb = new StringBuilder();
                        foreach (var Item in xSize)
                        {
                            Sb.Append(Item.SI_Description + Environment.NewLine);
                        }


                        foreach (var ColumnName in ColumnNames)
                        {
                            if (string.IsNullOrEmpty(ColumnName[1]))
                            {
                                ColumnName[1] = Sb.ToString();
                                break;
                            }
                        }
                    }
                    */

                    foreach (var LineIssue in LineIssues)
                    {
                        var CSReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == LineIssue.TLCMTLI_CutSheet_FK).FirstOrDefault();
                        if (CSReceipt != null)
                        {
                            if (_QueryParms.Styles.Count != 0)
                            {
                                var Styles = _QueryParms.Styles.FirstOrDefault(s => s.Sty_Id == CSReceipt.TLCUTSHR_Style_FK);
                                if (Styles == null)
                                    continue;
                            }

                            if (_QueryParms.Colours.Count != 0)
                            {
                                var Colours = _QueryParms.Colours.FirstOrDefault(s => s.Col_Id == CSReceipt.TLCUTSHR_Colour_FK);
                                if (Colours == null)
                                    continue;
                            }

                            //==================================================
                            DataSet24.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            var CS = context.TLCUT_CutSheet.Find(CSReceipt.TLCUTSHR_CutSheet_FK);
                            if (CS != null)
                            {
                                nr.CustSheet = CS.TLCutSH_No;
                                nr.Customer = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;

                            }

                            try
                            {
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CSReceipt.TLCUTSHR_Colour_FK).Col_Display;
                            }
                            catch (Exception ex)
                            {
                                nr.Colour = "Unknown";
                            }

                            if (CS != null)
                            {
                                nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            }

                            nr.CmtFacility = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;

                            var CutSheetDet = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSReceipt.TLCUTSHR_Pk);
                            if (CutSheetDet.Count() != 0)
                            {
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
                                nr.OriginalQty = CutSheetDet.Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;

                                var ExpectedUnitsGrouped = CutSheetDet.GroupBy(x => x.TLCUTSHRD_Size_FK);
                                foreach (var Group in ExpectedUnitsGrouped)
                                {
                                    var SizePk = Group.FirstOrDefault().TLCUTSHRD_Size_FK;
                                    var Size = context.TLADM_Sizes.Find(SizePk);
                                    if (Size != null)
                                    {
                                        var EUnits = Group.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                                        if (Size.SI_ColNumber == 1)
                                            nr.Col1 += EUnits;
                                        else if (Size.SI_ColNumber == 2)
                                            nr.Col2 += EUnits;
                                        else if (Size.SI_ColNumber == 3)
                                            nr.Col3 += EUnits;
                                        else if (Size.SI_ColNumber == 4)
                                            nr.Col4 += EUnits;
                                        else if (Size.SI_ColNumber == 5)
                                            nr.Col5 += EUnits;
                                        else if (Size.SI_ColNumber == 6)
                                            nr.Col6 += EUnits;
                                        else if (Size.SI_ColNumber == 7)
                                            nr.Col7 += EUnits;
                                        else if (Size.SI_ColNumber == 8)
                                            nr.Col8 += EUnits;
                                        else if (Size.SI_ColNumber == 9)
                                            nr.Col9 += EUnits;
                                        else if (Size.SI_ColNumber == 10)
                                            nr.Col10 += EUnits;
                                        else
                                            nr.Col11 += EUnits;
                                    }
                                }
                                try
                                {
                                    nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, nr.CustSheet);
                                }
                            }

                            nr.Reason = LineIssue.TLCMTLI_Reason;
                            if (LineIssue.TLCMTLI_TransferDate != null)
                            {
                                nr.TransferDate = (DateTime)LineIssue.TLCMTLI_TransferDate;
                            }

                            try
                            {
                                dataTable1.AddDataTable1Row(nr);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message, nr.CustSheet);
                            }
                        }
                    }
                }

                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CustSheet";
                ds.Tables.Add(DataV.ToTable());

                CutsOnHold ConHold = new CutsOnHold();
                ConHold.SetDataSource(ds);

                IEnumerator ie = ConHold.Section2.ReportObjects.GetEnumerator();
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

                crystalReportViewer1.ReportSource = ConHold;
            }
            else if (_RepNo == 25)
            {
                DataSet ds = new DataSet();
                DataSet25.DataTable1DataTable dataTable1 = new DataSet25.DataTable1DataTable();
                DataSet25.DataTable2DataTable dataTable2 = new DataSet25.DataTable2DataTable();
                CMTRepository repo = new CMTRepository();

                using (var context = new TTI2Entities())
                {
                    //Set up the header file 
                    DataSet25.DataTable2Row nrx = dataTable2.NewDataTable2Row();
                    nrx.pk = 1;
                    nrx.Title = "Equivalent Costing Comparison";
                    nrx.DateFrom = _QueryParms.FromDate;
                    nrx.DateTo = _QueryParms.ToDate;
                    dataTable2.AddDataTable2Row(nrx);

                    //1st calculate the base cost from the cheapest selected;
                    //--------------------------------------------------------------
                    var Equival = context.TLADM_Styles.Where(x => x.Sty_Equiv).FirstOrDefault();
                    if (Equival != null)
                    {
                        foreach (var Dept in _QueryParms.Depts)
                        {
                            var LeastCost = context.TLCMT_ProductionCosts.Where(x => x.CMTP_Style_FK == Equival.Sty_Id && x.CMTP_CMTFacility_FK == Dept.Dep_Id).OrderBy(x => x.CMTP_Production_Cost).FirstOrDefault();
                            var ComWork = repo.CMTCompletedWork(_QueryParms).Where(x => x.TLCMTWC_TransactionDate >= _QueryParms.FromDate && x.TLCMTWC_TransactionDate <= _QueryParms.ToDate && x.TLCMTWC_CMTFacility_FK == Dept.Dep_Id).GroupBy(x => new { x.TLCMTWC_Style_FK, x.TLCMTWC_Colour_FK, x.TLCMTWC_Size_FK });
                            foreach (var CwStyle in ComWork)
                            {
                                var Style_Pk = CwStyle.FirstOrDefault().TLCMTWC_Style_FK;
                                var Colour_Pk = CwStyle.FirstOrDefault().TLCMTWC_Colour_FK;
                                var Size_Pk = CwStyle.FirstOrDefault().TLCMTWC_Size_FK;

                                var CurrentCost = context.TLCMT_ProductionCosts.Where(x => x.CMTP_Style_FK == Style_Pk && x.CMTP_CMTFacility_FK == Dept.Dep_Id).FirstOrDefault();
                                if (CurrentCost != null)
                                {
                                    var Ratio = Math.Round(CurrentCost.CMTP_Production_Cost / LeastCost.CMTP_Production_Cost, 2);

                                    DataSet25.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                    nr.pk = 1;
                                    nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Style_Pk).Sty_Description;
                                    nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Colour_Pk).Col_Display;
                                    nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Size_Pk).SI_Description;
                                    nr.Cost = CurrentCost.CMTP_Production_Cost;
                                    nr.ActualUnits = CwStyle.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                                    nr.Conversion = Ratio;
                                    nr.EquivUnits = (Decimal)Ratio * nr.ActualUnits;
                                    nr.Dept = context.TLADM_Departments.Find(Dept.Dep_Id).Dep_Description;
                                    dataTable1.AddDataTable1Row(nr);
                                }
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable1.Count == 0)
                {
                    DataSet25.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.pk = 1;
                    dataTable1.AddDataTable1Row(nr);

                    dataTable2.FirstOrDefault().errorLog = "No data found pertaining to selection made";

                }
                ds.Tables.Add(dataTable2);
                EquivCosting LFInput = new EquivCosting();
                LFInput.SetDataSource(ds);
                crystalReportViewer1.ReportSource = LFInput;
            }
            else if (_RepNo == 26) // CMT Production By Month 
            {
                DataSet ds = new DataSet();
                DataSet26.DataTable1DataTable dataTable1 = new DataSet26.DataTable1DataTable();
                DataSet26.DataTable2DataTable dataTable2 = new DataSet26.DataTable2DataTable();
                Util core = new Util();
                IList<TLCMT_CompletedWork> PGroupOrderDetails = null;

                CMTRepository repo = new CMTRepository();

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

                    PGroupOrderDetails = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= _repOpts.fromDate && x.TLCMTWC_TransactionDate <= _repOpts.toDate).ToList();

                    if (_repOpts.GradeAOnly)
                        PGroupOrderDetails = PGroupOrderDetails.Where(x => x.TLCMTWC_Grade.Contains("A")).ToList();
                    else if (_repOpts.GradeBOnly)
                        PGroupOrderDetails = PGroupOrderDetails.Where(x => x.TLCMTWC_Grade.Contains("B")).ToList();

                    var GroupedData = PGroupOrderDetails.GroupBy(x => new { x.TLCMTWC_Style_FK });

                    foreach (var Group in GroupedData)
                    {

                        var StylePk = Group.FirstOrDefault().TLCMTWC_Style_FK;
                        //=====================================================
                        // Add a new Record to the data table;
                        //=================================================
                        DataRow Row = dt.NewRow();
                        Row[0] = StylePk;
                        // Row[1] = LinePk;
                        var RecordGroup = Group.GroupBy(x => x.TLCMTWC_TransactionDate.Month);

                        foreach (var Record in RecordGroup)
                        {
                            var LinePk = Group.FirstOrDefault().TLCMTWC_LineIssue_FK;
                            var MthKey = Record.FirstOrDefault().TLCMTWC_TransactionDate.Month.ToString().PadLeft(2, '0');

                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                if (_repOpts.Units)
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Record.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                                else
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Record.Count();
                            }

                        }
                        dt.Rows.Add(Row);

                    }

                    foreach (DataRow Row in dt.Rows)
                    {
                        DataSet26.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Row.Field<int>(0)).Sty_Description;
                        nr.Pk = 1;
                        /*************************************************/
                        /*var LineIssue = context.TLCMT_LineIssue.Find(Row.Field<int>(1));
                        if(LineIssue != null)
                           nr.Line = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                         */

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
                        DataSet26.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.NoDataFound = "No data found matching selection criteria";
                        dataTable1.AddDataTable1Row(nr);
                    }
                    ds.Tables.Add(dataTable1);

                    DataSet26.DataTable2Row nrx = dataTable2.NewDataTable2Row();
                    nrx.Pk = 1;
                    nrx.FromDate = _repOpts.fromDate;
                    nrx.ToDate = _repOpts.toDate;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("CMT Production Analysis by Style ");
                    if (_repOpts.GradeAOnly)
                        sb.Append("(A Grade only). ");
                    else if (_repOpts.GradeBOnly)
                        sb.Append("(B Grade only). ");
                    else
                        sb.Append("(All Grades). ");

                    if (_repOpts.Units)
                        sb.Append("Values expressed in Units");
                    else
                        sb.Append("Values expressed in Boxes");

                    nrx.Title = sb.ToString();
                    dataTable2.AddDataTable2Row(nrx);

                    ds.Tables.Add(dataTable2);
                    CMTProdByMonth OutStanding = new CMTProdByMonth();
                    OutStanding.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = OutStanding;
                }
            }
            else if (_RepNo == 27) // NCR Reports (Individual)
            {
                DataSet ds = new DataSet();
                DataSet27.DataTable1DataTable dataTable1 = new DataSet27.DataTable1DataTable();
                DataSet27.DataTable2DataTable dataTable2 = new DataSet27.DataTable2DataTable();


                using (var context = new TTI2Entities())
                {
                    var Entries = context.TLCMT_NonCompliance.Where(x => x.CMTNCD_CutSheet_Fk == _RecKey).ToList();
                    if (Entries.Count != 0)
                    {
                        var CutSheet = context.TLCUT_CutSheet.Find(_RecKey);
                        if (CutSheet != null)
                        {
                            DataSet27.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = 1;
                            nr.Ncr_CutSheet = CutSheet.TLCutSH_No;
                            nr.Ncr_Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;
                            nr.Ncr_Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            nr.Ncr_Date = DateTime.Now;

                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("BLW")).FirstOrDefault();
                            if (Dept != null)
                            {
                                var LastNumber = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                                if (LastNumber != null)
                                {
                                    nr.Ncr_Number = LastNumber.col9;
                                    LastNumber.col9 += 1;

                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                }
                            }

                            var LineIssue = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                            if (LineIssue != null)
                            {
                                var LineDetails = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK);
                                if (LineDetails != null)
                                {
                                    nr.Ncr_Line = LineDetails.TLCMTCFG_Description;
                                }
                            }

                            dataTable1.AddDataTable1Row(nr);

                            foreach (var Entry in Entries)
                            {
                                DataSet27.DataTable2Row xRow = dataTable2.NewDataTable2Row();
                                xRow.Pk = 1;
                                xRow.Ncr_Description = context.TLADM_CMTNonCompliance.Find(Entry.CMTNCD_NonCompliance_Fk).CMTNC_Description;
                                dataTable2.AddDataTable2Row(xRow);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                NCRDetails OutStanding = new NCRDetails();
                OutStanding.SetDataSource(ds);
                crystalReportViewer1.ReportSource = OutStanding;
            }
            else if (_RepNo == 28)      // NCR By Month 
            {
                DataSet ds = new DataSet();
                DataSet29.DataTable1DataTable dataTable1 = new DataSet29.DataTable1DataTable();
                DataSet29.DataTable2DataTable dataTable2 = new DataSet29.DataTable2DataTable();
                Util core = new Util();
                IList<TLCMT_NonCompliance> NonCompliances = null;

                _repos = new CMTRepository();

                //================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Fault", typeof(int));     // 0
                //===============================================================================
                dt.Columns.Add("01", typeof(int));       // 1  
                dt.Columns[1].DefaultValue = 0;
                dt.Columns.Add("02", typeof(int));       // 2    
                dt.Columns[2].DefaultValue = 0;
                dt.Columns.Add("03", typeof(int));       // 3
                dt.Columns[3].DefaultValue = 0;
                dt.Columns.Add("04", typeof(int));       // 4
                dt.Columns[4].DefaultValue = 0;
                dt.Columns.Add("05", typeof(int));       // 5
                dt.Columns[5].DefaultValue = 0;
                dt.Columns.Add("06", typeof(int));       // 6
                dt.Columns[6].DefaultValue = 0;
                dt.Columns.Add("07", typeof(int));       // 7
                dt.Columns[7].DefaultValue = 0;
                dt.Columns.Add("08", typeof(int));       // 8
                dt.Columns[8].DefaultValue = 0;
                dt.Columns.Add("09", typeof(int));       // 9
                dt.Columns[9].DefaultValue = 0;
                dt.Columns.Add("10", typeof(int));       // 10
                dt.Columns[10].DefaultValue = 0;
                dt.Columns.Add("11", typeof(int));       // 11
                dt.Columns[11].DefaultValue = 0;
                dt.Columns.Add("12", typeof(int));       // 12   
                dt.Columns[12].DefaultValue = 0;
                dt.Columns.Add("Style", typeof(int));    // 13   
                dt.Columns[13].DefaultValue = 0;

                using (var context = new TTI2Entities())
                {
                    var LstDayMnth = core.LastDayOfMonth(DateTime.Now.Month);
                    var CutOffDate = DateTime.Now.AddDays(LstDayMnth - DateTime.Now.Day + 1).AddYears(-1);

                    NonCompliances = _repos.CMTNonCompliance(_QueryParms).ToList();
                    var NCGroupedByStyle = NonCompliances.GroupBy(x => x.CMTNCD_Style_FK);
                    foreach (var StyleGroup in NCGroupedByStyle)
                    {
                        var NCGroupedByCom = StyleGroup.GroupBy(x => x.CMTNCD_NonCompliance_Fk);
                        foreach (var NonCompliance in NCGroupedByCom)
                        {
                            //=====================================================
                            // Add a new Record to the data table;
                            //=================================================
                            DataRow Row = dt.NewRow();
                            Row[0] = NonCompliance.FirstOrDefault().CMTNCD_NonCompliance_Fk;
                            Row[13] = NonCompliance.FirstOrDefault().CMTNCD_Style_FK;
                            var RecordGroup = NonCompliance.GroupBy(x => x.CMTNCD_TransDate);
                            foreach (var Record in RecordGroup)
                            {
                                var SoldDate = Convert.ToDateTime(Record.FirstOrDefault().CMTNCD_TransDate);
                                var MthKey = SoldDate.Month.ToString().PadLeft(2, '0');

                                var ColIndex = dt.Columns.IndexOf(MthKey);
                                if (ColIndex != 0)
                                {
                                    Row[ColIndex] = Row.Field<int>(ColIndex) + Record.Count();
                                }
                            }
                            dt.Rows.Add(Row);
                        }
                    }


                    foreach (DataRow Row in dt.Rows)
                    {
                        DataSet29.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.FaultDescription = context.TLADM_CMTNonCompliance.Find(Row.Field<int>(0)).CMTNC_Description;
                        nr.Pk = 1;
                        nr.JAN = Row.Field<int>(1);
                        nr.FEB = Row.Field<int>(2);
                        nr.MAR = Row.Field<int>(3);
                        nr.APR = Row.Field<int>(4);
                        nr.MAY = Row.Field<int>(5);
                        nr.JUN = Row.Field<int>(6);
                        nr.JUL = Row.Field<int>(7);
                        nr.AUG = Row.Field<int>(8);
                        nr.SEP = Row.Field<int>(9);
                        nr.OCT = Row.Field<int>(10);
                        nr.NOV = Row.Field<int>(11);
                        nr.DEC = Row.Field<int>(12);
                        nr.Total = nr.JAN + nr.FEB + nr.MAR + nr.APR + nr.MAY + nr.JUN + nr.JUL + nr.AUG + nr.SEP + nr.OCT + nr.NOV + nr.DEC;
                        nr.Style = context.TLADM_Styles.Find(Row.Field<int>(13)).Sty_Description;
                        dataTable2.AddDataTable2Row(nr);
                    }

                    if (dataTable2.Rows.Count == 0)
                    {
                        DataSet29.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Pk = 1;
                        nr.ErrorLog = "No data found matching selection criteria";
                        dataTable2.AddDataTable2Row(nr);
                    }
                    ds.Tables.Add(dataTable1);

                    DataSet29.DataTable1Row nrx = dataTable1.NewDataTable1Row();
                    nrx.Pk = 1;
                    nrx.From_Date = _QueryParms.FromDate;
                    nrx.To_Date = _QueryParms.ToDate;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Non Compliance By Month");
                    nrx.ReportHeader = sb.ToString();
                    dataTable1.AddDataTable1Row(nrx);

                    ds.Tables.Add(dataTable2);
                    CMTNonCompliance SalesBy = new CMTNonCompliance();
                    SalesBy.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = SalesBy;
                }
            }
            else if (_RepNo == 29)      // Priority Analysis  
            {
                DataSet ds = new DataSet();
                DataSet30.DataTable1DataTable dataTable1 = new DataSet30.DataTable1DataTable();
                DataSet30.DataTable2DataTable dataTable2 = new DataSet30.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var LineIssues = (from LineIssue in context.TLCMT_LineIssue
                                      where LineIssue.TLCMTLI_WorkCompleted && LineIssue.TLCMTLI_Priority && LineIssue.TLCMTLI_Priority_Date >= _repOpts.fromDate && LineIssue.TLCMTLI_Priority_Date <= _repOpts.toDate
                                      select LineIssue).ToList();

                    foreach (var LineIssue in LineIssues)
                    {
                        DataSet30.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.CutSheetNo = LineIssue.TLCMTLI_CutSheetDetails;
                        nr.Location = context.TLADM_Departments.Find(LineIssue.TLCMTLI_CmtFacility_FK).Dep_Description;
                        nr.LineDetails = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                        nr.PriorityDate = (DateTime)LineIssue.TLCMTLI_Priority_Date;
                        nr.DateCompleted = (DateTime)LineIssue.TLCMTLI_WorkCompletedDate;
                        TimeSpan Duration = nr.DateCompleted.Subtract(nr.PriorityDate);
                        nr.Days = Duration.Days;
                        nr.Pk = 1;

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet30.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "No data found matching selection criteria";
                    dataTable1.AddDataTable1Row(nr);
                }

                DataSet30.DataTable2Row hrw = dataTable2.NewDataTable2Row();
                hrw.FromDate = _repOpts.fromDate;
                hrw.ToDate = _repOpts.toDate;
                hrw.ReportTitle = "CutSheet Priority Analysis";
                hrw.Pk = 1;
                dataTable2.AddDataTable2Row(hrw);



                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CompletedWorkPriority Priority = new CompletedWorkPriority();
                Priority.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Priority;

            }
            else if (_RepNo == 30)      // Date Required Analysis  
            {
                DataSet ds = new DataSet();
                DataSet31.DataTable1DataTable dataTable1 = new DataSet31.DataTable1DataTable();
                DataSet31.DataTable2DataTable dataTable2 = new DataSet31.DataTable2DataTable();
                core = new Util();
                IList<TLCMT_LineIssue> LineIssues = null;

                using (var context = new TTI2Entities())
                {
                    if (_QueryParms.ProductionResults)
                    {
                        LineIssues = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_WorkCompleted && x.TLCMTLI_Required_Date >= _repOpts.fromDate && x.TLCMTLI_Required_Date <= _repOpts.toDate).ToList();

                        /*LineIssues = (from LI in context.TLCMT_LineIssue
                                     join CW in context.TLCMT_CompletedWork on LI.TLCMTLI_Pk equals CW.TLCMTWC_LineIssue_FK 
                                     where LI.TLCMTLI_Required_Date >= _repOpts.fromDate && LI.TLCMTLI_Required_Date <= LI.TLCMTLI_Required_Date && LI.TLCMTLI_WorkCompleted && !CW.TLCMTWC_Picked
                                     select LI).ToList();*/
                    }
                    else
                        LineIssues = context.TLCMT_LineIssue.Where(x => !x.TLCMTLI_WorkCompleted && x.TLCMTLI_Required_Date >= _repOpts.fromDate && x.TLCMTLI_Required_Date <= _repOpts.toDate).ToList();

                    foreach (var LineIssue in LineIssues)
                    {
                        DataSet31.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                        hnr.Pk = 1;
                        var CS = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK);
                        if (CS != null)
                        {
                            hnr.CutSheetNo = CS.TLCutSH_No;
                            if (LineIssue.TLCMTLI_Priority)
                                hnr.CutSheetNo += "**";

                            hnr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            hnr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;

                            if (LineIssue.TLCMTLI_Required_Date != null)
                                hnr.RequiredDate = (DateTime)LineIssue.TLCMTLI_Required_Date;

                            if (_QueryParms.ProductionResults)
                            {
                                if (LineIssue.TLCMTLI_IssuedToLine && LineIssue.TLCMTLI_WorkCompleted && LineIssue.TLCMTLI_WorkCompletedDate != null)
                                {
                                    hnr.CompletedDate = (DateTime)LineIssue.TLCMTLI_WorkCompletedDate;
                                    hnr.Days = core.GetWorkingDays((DateTime)LineIssue.TLCMTLI_Required_Date, (DateTime)LineIssue.TLCMTLI_WorkCompletedDate);
                                }
                            }
                        }
                        dataTable2.AddDataTable2Row(hnr);
                    }
                }

                DataSet31.DataTable1Row NewRw = dataTable1.NewDataTable1Row();
                NewRw.Pk = 1;
                NewRw.FromDate = _repOpts.fromDate;
                NewRw.ToDate = _repOpts.toDate;

                if (_QueryParms.ProductionResults)
                {
                    NewRw.ProductionResults = true;
                    NewRw.Title = "Production Date Results as from ";
                }
                else
                {
                    NewRw.ProductionResults = false;
                    NewRw.Title = "Production Results required from ";
                }

                if (dataTable2.Rows.Count == 0)
                {
                    NewRw.ErrorLog = "No data found for the dates as selected";
                    DataSet31.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                    hnr.Pk = 1;
                    dataTable2.AddDataTable2Row(hnr);
                }
                dataTable1.AddDataTable1Row(NewRw);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                DatesRequired DatesReq = new DatesRequired();
                DatesReq.SetDataSource(ds);
                crystalReportViewer1.ReportSource = DatesReq;


            }
            else if (_RepNo == 31)
            {
                DataSet ds = new DataSet();
                DataSet32.DataTable1DataTable dataTable1 = new DataSet32.DataTable1DataTable();
                DataSet32.DataTable2DataTable dataTable2 = new DataSet32.DataTable2DataTable();
                core = new Util();
                _repos = new CMTRepository();

                using (var context = new TTI2Entities())
                {
                    var WorkCompleted = _repos.CMTPlanedVsActual(_QueryParms).GroupBy(x => x.TLCMTWC_CutSheet_FK);

                    foreach (var Group in WorkCompleted)
                    {
                        int CSTotalActual = 0;
                        CSTotalActual += Group.Sum(x => x.TLCMTWC_Qty);
                        var CutSheet = context.TLCUT_CutSheet.Find(Group.First().TLCMTWC_CutSheet_FK);
                        if (CutSheet != null)
                        {
                            var Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            var Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;

                            var EUTotal = (from CS in context.TLCUT_CutSheet
                                           join EU in context.TLCUT_ExpectedUnits on CS.TLCutSH_Pk equals EU.TLCUTE_CutSheet_FK
                                           select EU).Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0M;

                            DataSet32.DataTable2Row xNewRw = dataTable2.NewDataTable2Row();
                            xNewRw.Pk = 1;
                            xNewRw.Style = Style;
                            xNewRw.Colour = Colour;
                            xNewRw.Size = string.Empty;
                            xNewRw.CutSheetNumber = CutSheet.TLCutSH_No;
                            xNewRw.Planned = 0;
                            xNewRw.Actual = 0;
                            xNewRw.CutSheetTotalPlaned = (int)EUTotal;
                            xNewRw.CutSheetTotalActual = CSTotalActual;

                            dataTable2.AddDataTable2Row(xNewRw);
                        }

                    }

                    DataSet32.DataTable1Row NewRw = dataTable1.NewDataTable1Row();
                    NewRw.Pk = 1;
                    NewRw.FromDate = _repOpts.fromDate;
                    NewRw.ToDate = _repOpts.toDate;
                    NewRw.Title = "Planned Vs Actual Expected Units at CMT";

                    dataTable1.AddDataTable1Row(NewRw);
                    if (dataTable2.Rows.Count == 0)
                    {
                        DataSet32.DataTable2Row xNewRw = dataTable2.NewDataTable2Row();
                        xNewRw.Pk = 1;
                        xNewRw.ErrorLog = "No Data Found matching dates selected";
                        dataTable2.AddDataTable2Row(xNewRw);
                    }
                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);


                }

            }
            if (_RepNo == 32)
            {
                DataSet ds = new DataSet();
                DataSet33.DataTable1DataTable dataTable1 = new DataSet33.DataTable1DataTable();
                DataSet33.DataTable2DataTable dataTable2 = new DataSet33.DataTable2DataTable();

                DataSet33.DataTable1Row NewRw = dataTable1.NewDataTable1Row();
                NewRw.Pk = 1;
                NewRw.Title = "CMT Measurement Points";
                dataTable1.AddDataTable1Row(NewRw);

                using (var context = new TTI2Entities())
                {
                    var MeasurePoints = context.TLADM_CMTMeasurementPoints.OrderBy(x => x.CMTMP_ShortCode).ToList();
                    var StylesGrouped = context.TLCMT_AuditMeasurements.GroupBy(x => new { x.CMTBFA_Style_FK, x.CMTBFA_Size_FK, x.CMTBFA_MeasureP_FK });

                    foreach (var Style in StylesGrouped)
                    {
                        var StylePk = Style.FirstOrDefault().CMTBFA_Style_FK;
                        var StyleDescription = _Styles.FirstOrDefault(s => s.Sty_Id == StylePk).Sty_Description;
                        var Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == Style.FirstOrDefault().CMTBFA_Customer_FK).Cust_Description;

                        foreach (var Siz in Style)
                        {
                            DataSet33.DataTable2Row Nr = dataTable2.NewDataTable2Row();
                            Nr.Pk = 1;
                            Nr.Style = StyleDescription;
                            Nr.Customer = Customer;
                            Nr.Size = context.TLADM_Sizes.Find(Siz.CMTBFA_Size_FK).SI_Description;
                            Nr.Measurement_Value = Siz.CMTBFA_Measurement;

                            var MP = context.TLADM_CMTMeasurementPoints.Find(Siz.CMTBFA_MeasureP_FK);
                            if (MP != null)
                            {
                                Nr.ShortCode = MP.CMTMP_ShortCode;
                                Nr.Description = MP.CMTMP_Description;
                            }

                            dataTable2.AddDataTable2Row(Nr);
                        }
                    }

                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);

                    CMTMeasureP MPoints = new CMTMeasureP();
                    MPoints.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = MPoints;

                }
            }
            else if (_RepNo == 33)
            {
                DataSet ds = new DataSet();
                DataSet34.DataTable1DataTable dataTable1 = new DataSet34.DataTable1DataTable();
                DataSet34.DataTable2DataTable dataTable2 = new DataSet34.DataTable2DataTable();
                string[][] ColumnNames = null;
                _repos = new CMTRepository();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {


                    ColumnNames = new string[][]
                    {   new string[] {"Text10", string.Empty},
                        new string[] {"Text23", string.Empty},
                        new string[] {"Text24", string.Empty},
                        new string[] {"Text25", string.Empty},
                        new string[] {"Text26", string.Empty},
                        new string[] {"Text27", string.Empty},
                        new string[] {"Text28", string.Empty},
                        new string[] {"Text29", string.Empty},
                        new string[] {"Text30", string.Empty},
                        new string[] {"Text31", string.Empty},
                        new string[] {"Text32", string.Empty}

                    };

                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }

                    var CSR = _repos.CMTPlanedVsActual(_QueryParms).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();

                    foreach (var row in CSR)
                    {
                        DataSet34.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        var CutSht_Pk = row.FirstOrDefault().TLCMTWC_CutSheet_FK;
                        var CS = context.TLCUT_CutSheet.Find(CutSht_Pk);
                        if (CS != null)
                        {
                            nr.CustSheet = CS.TLCutSH_No;
                        }

                        var CMT_Cut_Pk = row.FirstOrDefault().TLCMTWC_CMTFacility_FK;
                        // nr.Customer = context.TLADM_WhseStore.Find(CMT_Cut_Pk).WhStore_Description;
                        try
                        {
                            var Colours_Pk = row.FirstOrDefault().TLCMTWC_Colour_FK;
                            nr.Colour = _Colours.Where(x => x.Col_Id == Colours_Pk).FirstOrDefault().Col_Display;
                        }
                        catch (Exception ex)
                        {
                            nr.Colour = "Unknown";
                        }

                        if (CS != null)
                        {
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                        }


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
                        nr.Total = 0;
                        var ExpectedUnitsGrouped = row.GroupBy(x => x.TLCMTWC_Size_FK);
                        foreach (var Group in ExpectedUnitsGrouped)
                        {
                            var SizePk = Group.FirstOrDefault().TLCMTWC_Size_FK;
                            var Size = _Sizes.FirstOrDefault(s => s.SI_id == SizePk);
                            if (Size != null)
                            {
                                var EUnits = Group.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                                if (Size.SI_ColNumber == 1)
                                    nr.Col1 += EUnits;
                                else if (Size.SI_ColNumber == 2)
                                    nr.Col2 += EUnits;
                                else if (Size.SI_ColNumber == 3)
                                    nr.Col3 += EUnits;
                                else if (Size.SI_ColNumber == 4)
                                    nr.Col4 += EUnits;
                                else if (Size.SI_ColNumber == 5)
                                    nr.Col5 += EUnits;
                                else if (Size.SI_ColNumber == 6)
                                    nr.Col6 += EUnits;
                                else if (Size.SI_ColNumber == 7)
                                    nr.Col7 += EUnits;
                                else if (Size.SI_ColNumber == 8)
                                    nr.Col8 += EUnits;
                                else if (Size.SI_ColNumber == 9)
                                    nr.Col9 += EUnits;
                                else if (Size.SI_ColNumber == 10)
                                    nr.Col10 += EUnits;
                                else
                                    nr.Col11 += EUnits;
                            }
                        }

                        nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet34.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);
                }

                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CustSheet";
                ds.Tables.Add(DataV.ToTable());

                DataSet34.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                hnr.Pk = 1;
                hnr.FromDate = _QueryParms.FromDate;
                hnr.ToDate = _QueryParms.ToDate;
                dataTable2.AddDataTable2Row(hnr);

                ds.Tables.Add(dataTable2);

                CMTProdAnalysis fcon = new CMTProdAnalysis();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

                IEnumerator ie = fcon.Section2.ReportObjects.GetEnumerator();
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
            else if (_RepNo == 34)
            {
                DataSet ds = new DataSet();
                DataSet35.DataTable1DataTable dataTable1 = new DataSet35.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var CutSheet = context.TLCUT_CutSheet.Find(_RecKey);
                    if (CutSheet != null)
                    {
                        var xStyle = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                        var xColour = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;

                        var Items = context.TLCMT_HistoryBoxedQty.Where(x => x.BoxHist_CutSheet_FK == _RecKey);
                        foreach (var Item in Items)
                        {
                            DataSet35.DataTable1Row nr = dataTable1.NewDataTable1Row();

                            nr.CustSheet = CutSheet.TLCutSH_No;
                            nr.Style = xStyle;
                            nr.Colour = xColour;

                            nr.Size = context.TLADM_Sizes.Find(Item.BoxHist_Size_FK).SI_Description;
                            nr.Prev_Qty = Item.BoxHist_Orignal_Qty;
                            nr.Current_Qty = Item.BoxHist_New_Qty;
                            nr.Box_Number = Item.BoxHist_No;

                            dataTable1.AddDataTable1Row(nr);


                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                BoxQtyHistory BoxH = new BoxQtyHistory();
                BoxH.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BoxH;

            }
            else if (_RepNo == 35)
            {
                DataSet ds = new DataSet();
                DataSet36.DataTable1DataTable dataTable1 = new DataSet36.DataTable1DataTable();
                _repos = new CMTRepository();

                var NCRs = _repos.CMTNonCompliance(_QueryParms);

                using (var context = new TTI2Entities())
                {
                    foreach (var NCR in NCRs)
                    {
                        var CutSheet = context.TLCUT_CutSheet.Find(NCR.CMTNCD_CutSheet_Fk);
                        if (CutSheet != null)
                        {
                            DataSet36.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.CutSheetNo = CutSheet.TLCutSH_No;
                            nr.Style = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            nr.Colour = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;

                            var NcomDesc = context.TLADM_CMTNonCompliance.Find(NCR.CMTNCD_NonCompliance_Fk);
                            if (NcomDesc != null)
                            {
                                nr.NonCompliance = NcomDesc.CMTNC_Description;

                                var FactConfic = context.TLCMT_FactConfig.Find(NCR.CMTNCD_Line_FK);
                                if (FactConfic != null)
                                {
                                    nr.Line_Details = FactConfic.TLCMTCFG_Description;
                                }
                            }

                            nr.Date = NCR.CMTNCD_TransDate;
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                NonComplianceByCutSheet BoxH = new NonComplianceByCutSheet();
                BoxH.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BoxH;

            }
            else if (_RepNo == 36)  // CMT Completed Work Analysis showing values by Sizes 
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable1 = new DataSet21.DataTable1DataTable();
                DataSet21.DataTable2DataTable dataTable2 = new DataSet21.DataTable2DataTable();

                core = new Util();
                _repos = new CMTRepository();
                using (var context = new TTI2Entities())
                {
                    DataSet21.DataTable2Row t2 = dataTable2.NewDataTable2Row();
                    t2.Title = "CMT Completed Work Analysis - Values By Size";
                    t2.FromDate = _repOpts.fromDate;
                    t2.ToDate = _repOpts.toDate;
                    t2.Pk = 1;
                    dataTable2.AddDataTable2Row(t2);

                    var CompletedWork = _repos.CMTCompletedWork(_QueryParms).ToList();
                    var ExistingGroups = CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= _repOpts.fromDate && x.TLCMTWC_TransactionDate <= _repOpts.toDate).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();


                    foreach (var Group in ExistingGroups)
                    {
                        StringBuilder SizeConcat = new StringBuilder();

                        var xSize = Group.GroupBy(x => x.TLCMTWC_Size_FK);
                        if (xSize.Count() == 1)
                            continue;

                        foreach (var Record in xSize)
                        {
                            var Pk = Record.FirstOrDefault().TLCMTWC_Size_FK;
                            var SizeDesc = _Sizes.FirstOrDefault(s => s.SI_id == Pk).SI_Description;
                          
                            DataSet21.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.TotalCS = 0;
                            nr.Pk = 1;
                            nr.Size = SizeDesc;
                            nr.AGrade = Group.Where(x => x.TLCMTWC_Grade.Contains("A") && x.TLCMTWC_Size_FK == Pk).Sum(x => x.TLCMTWC_Qty);
                            nr.BGrade = Group.Where(x => !x.TLCMTWC_Grade.Contains("A") && x.TLCMTWC_Size_FK == Pk).Sum(x => x.TLCMTWC_Qty);
                                                       
                           /* nr.Panels = Stats.CMTS_Panels;
                            nr.TotalIssued = Stats.CMTS_Total_A_Grades + Stats.CMTS_Total_B_Grades;
                            nr.Short = (Stats.CMTS_Total_A_Grades + Stats.CMTS_Total_B_Grades + Stats.CMTS_Panels) - nr.TotalCS;
                            nr.AGrade = Stats.CMTS_Total_A_Grades;
                            nr.BGrade = Stats.CMTS_Total_B_Grades;*/

                            var StylePk = Group.FirstOrDefault().TLCMTWC_Style_FK;
                            var ColorPk = Group.FirstOrDefault().TLCMTWC_Colour_FK;
                            var FacilityPk = Group.FirstOrDefault().TLCMTWC_CMTFacility_FK;

                            nr.CMT = context.TLADM_Departments.Find(FacilityPk).Dep_Description;
                            nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == StylePk).Sty_Description;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == ColorPk).Col_Display;
                            var CS = context.TLCUT_CutSheet.Find(Group.FirstOrDefault().TLCMTWC_CutSheet_FK);
                            if (CS != null)
                            {
                                nr.CSNO = CS.TLCutSH_No.Remove(0, 2);

                                var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                                if (DB != null)
                                {
                                    nr.BATCHNO = DB.DYEB_BatchNo.Remove(0, 2);

                                }

                                var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                                if (CutSheetReceipt != null)
                                {
                                    nr.TotalCS = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk && x.TLCUTSHRD_Size_FK == Pk).Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;
                                }

                                var LinePk = Group.FirstOrDefault().TLCMTWC_LineIssue_FK;

                                var Existing = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_Pk == LinePk).FirstOrDefault();
                                if (Existing != null)
                                {
                                   nr.LineNumber = context.TLCMT_FactConfig.Find(Existing.TLCMTLI_LineNo_FK).TLCMTCFG_LineNo.ToString();
                                }
                            }
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                   
                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);
                    CMTFinishedWorkBySize BoxH = new CMTFinishedWorkBySize();
                    BoxH.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = BoxH;
                }
            }

            crystalReportViewer1.Refresh();
            
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

        private struct DATA
        {
            public string GroupSort;
            public int PrimaryKey;
            public string Sizes;
            public int ForeignKey;

            public DATA(string _GS, int _PrimaryKey, string _Sizes, int _ForeignKey)
            {
                this.GroupSort = _GS;
                this.PrimaryKey = _PrimaryKey;
                this.Sizes = _Sizes;
                this.ForeignKey = _ForeignKey;
            }
        }
    }
}
