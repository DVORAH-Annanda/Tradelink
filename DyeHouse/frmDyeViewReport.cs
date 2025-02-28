using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Utilities;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static log4net.Appender.ColoredConsoleAppender;
using System.Diagnostics;

namespace DyeHouse
{
    public partial class frmDyeViewReport : Form
    {

        int _RepNo;
        int _Pk;
        bool _Commission;
        Util core;
        DyeReportOptions _repOps;
        string _TransNumber;
        StringBuilder MergeDetails;

        DyeRepository _repo;
        DyeQueryParameters _parms;

        IList<DyeProductionDetails> _ProdDetails;

        public frmDyeViewReport()
        {
            InitializeComponent();
        }

        public frmDyeViewReport(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmDyeViewReport(int RepNo, int Pk)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Pk = Pk;
        }

        public frmDyeViewReport(int RepNo, string Transnumber)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _TransNumber = Transnumber;
        }

        public frmDyeViewReport(int RepNo, DyeReportOptions repOps)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _repOps = repOps;

        }

        public frmDyeViewReport(int RepNo, DyeQueryParameters QueryParms)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _parms = QueryParms;

        }

        public frmDyeViewReport(int RepNo, DyeQueryParameters QueryParms, string TransNumber)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _parms = QueryParms;
            _TransNumber = TransNumber;

        }
        public frmDyeViewReport(int RepNo, int Pk, bool Commission)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _Pk = Pk;
            _Commission = Commission;

        }

        public frmDyeViewReport(int RepNo, IList<DyeProductionDetails> DyeProd)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _ProdDetails = DyeProd;
        }


        private void frmDyeViewReport_Load(object sender, EventArgs e)
        {
            core = new Util();
            MergeDetails = new StringBuilder();

            if (_RepNo == 1)
            {
                DataSet ds = new DataSet();
                DataSet36.DataTable1DataTable dataTable1 = new DataSet36.DataTable1DataTable();
                DataSet36.DataTable2DataTable dataTable2 = new DataSet36.DataTable2DataTable();
                _repo = new DyeRepository();

                IList<TLADM_Colours> _Colours = null;
                IList<TLDYE_DefinitionDetails> _DefinitionDetails = null;
                IList<TLADM_ConsumablesDC> _Consumables = null;
                IList<TLDYE_ReceipeGreigeQual> _ReceipeGreigeQual = null;

                var Receipes = _repo.SOHQuery(_parms);
                using (var context = new TTI2Entities())
                {
                    _Colours = context.TLADM_Colours.ToList();
                    _DefinitionDetails = context.TLDYE_DefinitionDetails.ToList();
                    _Consumables = context.TLADM_ConsumablesDC.ToList();
                    _ReceipeGreigeQual = context.TLDYE_ReceipeGreigeQual.ToList();

                    foreach (var Receipe in Receipes)
                    {
                        var Qualities = context.TLDYE_ReceipeGreigeQual.Where(x => x.TLGQ_ReceipeDef_FK == Receipe.TLDYE_DefinePk).ToList();

                        DataSet36.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = Receipe.TLDYE_DefinePk;
                        nr.ProgCode = Receipe.TLDYE_DefineCode;
                        nr.ProgDescription = Receipe.TLDYE_DefineDescription;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Receipe.TLDYE_ColorChart_FK).Col_Display;
                        dataTable1.AddDataTable1Row(nr);

                        StringBuilder sb = new StringBuilder(Qualities.Count);

                        foreach (var Qual in Qualities)
                        {
                            if (Qual.TLGQ_GreigeQuality_FK != 0)
                            {
                                var QualDesc = context.TLADM_Griege.Find(Qual.TLGQ_GreigeQuality_FK).TLGreige_Description;
                                sb.Append("* " + QualDesc);
                            }

                        }
                        var ReceipeDetails = _DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == Receipe.TLDYE_DefinePk).OrderBy(x => x.TLDYED_Cosumables_FK).ToList();
                        foreach (var ReceipeDetail in ReceipeDetails)
                        {
                            DataSet36.DataTable2Row tr = dataTable2.NewDataTable2Row();
                            tr.Pk = Receipe.TLDYE_DefinePk;

                            tr.ConCode = _Consumables.FirstOrDefault(s => s.ConsDC_Pk == ReceipeDetail.TLDYED_Cosumables_FK).ConsDC_Code;
                            tr.ConDescription = _Consumables.FirstOrDefault(s => s.ConsDC_Pk == ReceipeDetail.TLDYED_Cosumables_FK).ConsDC_Description;

                            tr.MLC = ReceipeDetail.TLDYED_MELFC;
                            tr.LiqRatio = ReceipeDetail.TLDYED_LiqRatio;
                            tr.Quality = sb.ToString();
                            dataTable2.AddDataTable2Row(tr);
                        }

                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CRCurrentReceipes rep = new CRCurrentReceipes();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;

            }
            else if (_RepNo == 2)
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable dataTable = new DataSet2.DataTable1DataTable();
                DataSet2.DataTable2DataTable dataTable2 = new DataSet2.DataTable2DataTable();
                IList<TLADM_Griege> _Greiges = null;
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_CustomerFile> _Customers = null;

                using (var context = new TTI2Entities())
                {
                    _Greiges = context.TLADM_Griege.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    _Customers = context.TLADM_CustomerFile.ToList();

                    var Existing = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_DyeOrderNumeric == _Pk).ToList();
                    if (Existing != null)
                    {
                        DataSet2.DataTable2Row nr = dataTable2.NewDataTable2Row();

                        nr.Pk = 1;
                        nr.DyeOrder = Existing.FirstOrDefault().TLDYEF_DyeOrderNo;
                        nr.OrderDate = Existing.FirstOrDefault().TLDYEF_OrderDate;
                        nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == Existing.FirstOrDefault().TLDYEF_Customer_FK).Cust_Description;
                        nr.WeekNo = Existing.FirstOrDefault().TLDYEF_DyeWeek;

                        dataTable2.AddDataTable2Row(nr);

                        foreach (var Item in Existing)
                        {
                            DataSet2.DataTable1Row hnr = dataTable.NewDataTable1Row();
                            hnr.Pk = 1;
                            var Greige = _Greiges.FirstOrDefault(s => s.TLGreige_Id == Item.TLDYEF_Greige_FK);
                            if (Greige != null)
                            {
                                hnr.Quality = Greige.TLGreige_Description;

                                var fabwidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                if (fabwidth != null)
                                    hnr.Wth = Convert.ToInt32(fabwidth.FW_Description);

                                var fabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                if (fabWeight != null)
                                    hnr.Disk = fabWeight.FWW_Calculation_Value;

                                if (fabwidth != null && fabWeight != null)
                                {
                                    var Rating = (decimal)50000 / (decimal)fabwidth.FW_Calculation_Value / (decimal)fabWeight.FWW_Calculation_Value;
                                    hnr.Rating = Math.Round(Rating, 4);
                                }
                            }

                            hnr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Item.TLDYEF_Colours_FK).Col_Display;
                            hnr.Weight = Item.TLDYEF_Quantity;
                            hnr.Demand = Item.TLDYEF_Demand;


                            DateTime dt = core.FirstDateOfWeek(Item.TLDYEF_OrderDate.Year, Item.TLDYEF_DyeWeek);
                            nr.WeekEndingDate = dt.AddDays(5);

                            dataTable.AddDataTable1Row(hnr);
                        }
                    }
                }
                ds.Tables.Add(dataTable);
                ds.Tables.Add(dataTable2);
                FDyeOrder fDyeO = new FDyeOrder();
                fDyeO.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fDyeO;
            }
            else if (_RepNo == 3)
            {
                DataSet ds = new DataSet();
                DataSet3.DataTable1DataTable datatable1 = new DataSet3.DataTable1DataTable();
                DataSet3.DataTable2DataTable datatable2 = new DataSet3.DataTable2DataTable();
                DataSet3.DataTable3DataTable datatable3 = new DataSet3.DataTable3DataTable();

                bool[] CheckRowAdded = core.PopulateArray(4, false);
                IList<TLADM_Griege> _Quality = null;
                IList<TLADM_Styles> _Styles = null;
                IList<TLADM_CustomerFile> _Customers = null;
                IList<TLADM_Colours> _Colours = null;

                using (var context = new TTI2Entities())
                {
                    _Quality = context.TLADM_Griege.ToList();
                    _Styles = context.TLADM_Styles.ToList();
                    _Customers = context.TLADM_CustomerFile.ToList();
                    _Colours = context.TLADM_Colours.ToList();

                    var Existing = context.TLDYE_DyeOrder.Find(_Pk);
                    if (Existing != null)
                    {
                        DataSet3.DataTable2Row nr = datatable2.NewDataTable2Row();

                        nr.Key = _Pk;

                        nr.DyeOrder = Existing.TLDYO_DyeOrderNum;
                        nr.OrderDate = Existing.TLDYO_OrderDate;
                        nr.DyeWeekNo = Existing.TLDYO_DyeReqWeek;
                        nr.CutWeekNo = Existing.TLDYO_CutReqWeek;
                        nr.CMTWeekNo = Existing.TLDYO_CMTReqWeek;
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Existing.TLDYO_Style_FK).Sty_Description;
                        nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == Existing.TLDYO_Customer_FK).Cust_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Existing.TLDYO_Colour_FK).Col_Display;

                        DateTime dt = core.FirstDateOfWeek(Existing.TLDYO_OrderDate.Year, Existing.TLDYO_DyeReqWeek);
                        nr.DyeWeekEndingDate = dt.AddDays(5);

                        dt = core.FirstDateOfWeek(Existing.TLDYO_OrderDate.Year, Existing.TLDYO_CutReqWeek);
                        nr.CutWeekEndingDate = dt.AddDays(5);


                        dt = core.FirstDateOfWeek(Existing.TLDYO_OrderDate.Year, Existing.TLDYO_CMTReqWeek);
                        nr.CMTWeekEndingDate = dt.AddDays(5);

                        datatable2.AddDataTable2Row(nr);
                        var ExistDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == _Pk).ToList();
                        foreach (var row in ExistDetail)
                        {
                            DataSet3.DataTable1Row hnr = datatable1.NewDataTable1Row();

                            if (row.TLDYOD_BodyOrTrim) // body
                            {
                                hnr.Description = "Body :";
                                hnr.Key = _Pk;
                                var Greige = _Quality.FirstOrDefault(s => s.TLGreige_Id == row.TLDYOD_Greige_FK);
                                if (Greige != null)
                                {
                                    hnr.Quality = Greige.TLGreige_Description;
                                    var fabwidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                    if (fabwidth != null)
                                        hnr.Wth = Convert.ToInt32(fabwidth.FW_Description);

                                    var fabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                    if (fabWeight != null)
                                        hnr.Disk = fabWeight.FWW_Calculation_Value;
                                }

                                hnr.Weight = (decimal)row.TLDYOD_Kgs;
                                hnr.Rating = (decimal)row.TLDYOD_Rating;
                                hnr.Units = (decimal)row.TLDYOD_Units;
                                datatable1.AddDataTable1Row(hnr);

                                var MarkerRating = context.TLADM_ProductRating.Find(row.TLDYOD_MarkerRating_FK);
                                if (MarkerRating != null)
                                {
                                    //-------------------------------------------------------------------------------------
                                    // There are two scenarios 
                                    //--------------------------------------------------------------------------------------
                                    var sizes = core.ExtrapNumber(MarkerRating.Pr_PowerN, context.TLADM_Sizes.Count());
                                    sizes.Sort();

                                    if (sizes.Count > 1)
                                    {
                                        StringBuilder sb = new StringBuilder();

                                        //---------------------------------
                                        // Multiple sizes 
                                        //in that case we have to calculate what size is what ratio
                                        //-------------------------------------------------
                                        var MarkerRatingDetail = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == row.TLDYOD_MarkerRating_FK);
                                        if (MarkerRatingDetail != null)
                                        {
                                            var TotalRatio = MarkerRatingDetail.Sum(x => x.Prd_MarkerRatio);

                                            foreach (var md in MarkerRatingDetail)
                                            {
                                                if (md.Prd_MarkerRatio == 0)
                                                    continue;

                                                DataSet3.DataTable3Row t3Row = datatable3.NewDataTable3Row();
                                                t3Row.Size = context.TLADM_Sizes.Find(md.Prd_SizePN).SI_Description;
                                                t3Row.Ratio = Math.Round(md.Prd_MarkerRatio, 2);

                                                decimal Percentage = md.Prd_MarkerRatio / TotalRatio;
                                                var temp = Percentage * row.TLDYOD_Units;
                                                decimal xdec = temp - Math.Truncate(temp);

                                                t3Row.Units = (int)Math.Round(Percentage * row.TLDYOD_Units, 0);

                                                datatable3.AddDataTable3Row(t3Row);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //-----------------------------------------------------
                                        // Just one size
                                        //-------------------------------------------------------
                                        var Size = context.TLADM_Sizes.Where(x => x.SI_PowerN == MarkerRating.Pr_PowerN).FirstOrDefault();
                                        if (Size != null)
                                        {
                                            DataSet3.DataTable3Row zhnr = datatable3.NewDataTable3Row();
                                            zhnr.Size = Size.SI_Description;
                                            zhnr.Ratio = 1.00M;
                                            zhnr.Units = row.TLDYOD_Units;
                                            zhnr.Key = _Pk;

                                            datatable3.AddDataTable3Row(zhnr);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //--------------------------------------------
                                //The following section handles all the trims
                                //--------------------------------------------------------------
                                DataSet3.DataTable1Row xhnr = datatable1.NewDataTable1Row();
                                TLADM_Trims Trim = null;
                                xhnr.Key = _Pk;

                                if (row.TLDYOD_Trims_FK != null)
                                {
                                    Trim = context.TLADM_Trims.Find(row.TLDYOD_Trims_FK);

                                    if (Trim != null)
                                    {
                                        xhnr.Description = Trim.TR_Description;
                                    }
                                }


                                var Greige = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK);
                                if (Greige != null)
                                {
                                    xhnr.Quality = Greige.TLGreige_Description;

                                    var fabwidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                    if (fabwidth != null)
                                        xhnr.Wth = Convert.ToInt32(fabwidth.FW_Description);

                                    var fabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                    if (fabWeight != null)
                                        xhnr.Disk = fabWeight.FWW_Calculation_Value;
                                }

                                xhnr.Weight = (decimal)row.TLDYOD_Kgs;
                                xhnr.Rating = (decimal)row.TLDYOD_Rating;
                                xhnr.Units = (decimal)row.TLDYOD_Units;
                                datatable1.AddDataTable1Row(xhnr);


                                //-------------------------------------------------------------------
                                // end of trims 
                                //---------------------------------------------------------------
                            }

                        }
                        //---------------------------------------------------------------------
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                ds.Tables.Add(datatable3);
                // ds.Tables.Add(datatable4);
                //  ds.Tables.Add(datatable5);
                //  ds.Tables.Add(datatable6);
                //  ds.Tables.Add(datatable7);

                GDyeOrder gDyeO = new GDyeOrder();
                gDyeO.SetDataSource(ds);
                crystalReportViewer1.ReportSource = gDyeO;

            }
            else if (_RepNo == 4)  // DyeBatch Report 
            {
                DataSet ds = new DataSet();
                DataSet5.DataTable1DataTable datatable1 = new DataSet5.DataTable1DataTable();
                DataSet5.DataTable2DataTable datatable2 = new DataSet5.DataTable2DataTable();
                DataSet5.DataTable3DataTable datatable3 = new DataSet5.DataTable3DataTable();
                string[][] ColumnNames = null;
                core = new Util();

                StringBuilder sb = null;
                IList<TLADM_QualityDefinition> Reasons = null;
                IList<TLADM_Styles> _Styles = null;
                IList<TLADM_CustomerFile> _Customers = null;
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_Griege> _Quality = null;

                using (var context = new TTI2Entities())
                {
                    _Styles = context.TLADM_Styles.ToList();
                    _Customers = context.TLADM_CustomerFile.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    _Quality = context.TLADM_Griege.ToList();

                    var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Depts != null)
                    {
                        Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id && x.QD_Measurable).OrderBy(X => X.QD_ShortCode).ToList();
                    }

                    var DyeBatch = context.TLDYE_DyeBatch.Find(_Pk);
                    if (DyeBatch != null)
                    {
                        if (!DyeBatch.DYEB_FabricMode)
                        {
                            var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);

                            DataSet5.DataTable2Row xnr = datatable2.NewDataTable2Row();
                            xnr.Key = _Pk;
                            xnr.DyeBatch = DyeBatch.DYEB_BatchNo;
                            if (DyeOrder != null)
                            {
                                xnr.DyeOrder = DyeOrder.TLDYO_DyeOrderNum;

                                var Customer = context.TLADM_CustomerFile.Find(DyeOrder.TLDYO_Customer_FK);
                                if (Customer != null)
                                {
                                    StringBuilder Custsb = new StringBuilder();
                                    Custsb.Append(Customer.Cust_Description);
                                    Custsb.Append(" " + context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK).Sty_Description);

                                    xnr.Customer = Custsb.ToString();
                                }
                            }
                            else
                            {
                                var Customer = context.TLADM_CustomerFile.Find(DyeBatch.DYEB_Customer_FK);
                                if (Customer != null)
                                {
                                    xnr.Customer = Customer.Cust_Description;
                                }
                            }

                            if (DyeOrder != null)
                            {
                                xnr.DateOrdered = DyeOrder.TLDYO_OrderDate;
                                xnr.DOWeekNo = core.GetIso8601WeekOfYear(DyeOrder.TLDYO_OrderDate);

                                DateTime dt = core.FirstDateOfWeek(DyeOrder.TLDYO_OrderDate.Year, DyeOrder.TLDYO_CMTReqWeek);
                                dt = dt.AddDays(5);
                                xnr.DateWeekRequired = dt;
                                xnr.WRWeekNo = DyeOrder.TLDYO_DyeReqWeek;
                            }
                            else
                            {
                                xnr.DateOrdered = (DateTime)DyeBatch.DYEB_BatchDate;
                                xnr.DOWeekNo = core.GetIso8601WeekOfYear((DateTime)DyeBatch.DYEB_BatchDate);

                                xnr.DateWeekRequired = (DateTime)DyeBatch.DYEB_RequiredDate;
                                xnr.WRWeekNo = core.GetIso8601WeekOfYear((DateTime)DyeBatch.DYEB_RequiredDate);

                            }


                            sb = new StringBuilder();
                            sb.Append(DyeBatch.DYEB_Notes);

                            if (DyeBatch.DYEB_Lab)
                                sb.Append("Lab report required. ");
                            if (DyeBatch.DYEB_Wrap)
                                sb.Append("Fabric needs to be wrapped");

                            xnr.Notes = sb.ToString();

                            if (!DyeBatch.DYEB_CommissinCust)
                            {
                                var Colour = context.TLADM_Colours.Find(DyeOrder.TLDYO_Colour_FK);
                                if (Colour != null)
                                {
                                    xnr.Colour = Colour.Col_Display;
                                }
                            }
                            else
                            {
                                xnr.Colour = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;
                            }

                            xnr.TotalKg = 0.00M;
                            xnr.TotalPieces = 0;

                            var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).OrderBy(x => x.DYEBO_GVRowNumber).ToList();
                            if (DyeBatchDetails.Count != 0)
                            {
                                xnr.TotalKg = DyeBatchDetails.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;

                                xnr.TotalPieces = DyeBatchDetails.Count();
                            }
                            datatable2.AddDataTable2Row(xnr);

                            DataSet5.DataTable1Row newr = datatable1.NewDataTable1Row();
                            newr.Key = _Pk;

                            var GroupDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).GroupBy(x => x.DYEBO_GVRowNumber).ToList();
                            var Cnt = 0;
                            foreach (var Grp in GroupDetail)
                            {
                                if (Grp.FirstOrDefault().DYEBD_BodyTrim)
                                {
                                    int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                    var Qual = context.TLADM_Griege.Find(Greige_FK);
                                    if (Qual != null)
                                    {
                                        newr.BodyQualtity = Qual.TLGreige_Description;
                                        newr.BodyKg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                        newr.BodyPieces = Grp.Count();
                                    }
                                }
                                else
                                {
                                    if (++Cnt == 1)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim1Quality = Qual.TLGreige_Description;
                                            newr.Trim1Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim1Pieces = Grp.Count();
                                        }
                                    }
                                    else if (Cnt == 2)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim2Quality = Qual.TLGreige_Description;
                                            newr.Trim2Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim2Pieces = Grp.Count();
                                        }
                                    }
                                    else if (Cnt == 3)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim3Quality = Qual.TLGreige_Description;
                                            newr.Trim3Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim3Pieces = Grp.Count();
                                        }
                                    }
                                }
                            }

                            datatable1.AddDataTable1Row(newr);

                            foreach (var record in DyeBatchDetails)
                            {
                                DataSet5.DataTable3Row tab3 = datatable3.NewDataTable3Row();
                                var GP = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK);
                                if (GP != null)
                                {
                                    tab3.Key = _Pk;
                                    tab3.Piece = GP.GreigeP_PieceNo;
                                    tab3.Gross = GP.GreigeP_weight;
                                    tab3.Nett = string.Empty;

                                    var Greige = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk);
                                    if (Greige != null)
                                        tab3.Quality = Greige.TLGreige_Description;

                                    tab3.Tex = GP.GreigeP_YarnTex;
                                    if (GP.GreigeP_PalletNo != null)
                                        tab3.Pallet = GP.GreigeP_PalletNo.Trim();

                                    var KnitOrder = context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk);
                                    if (KnitOrder != null)
                                    {
                                        tab3.KnitOrder = KnitOrder.KnitO_OrderNumber;
                                    }

                                    var cnt = 0;

                                    tab3.MergeDetail = GP.GreigeP_MergeDetail;

                                    // tab3.Measure1 = GP.GreigeP_Meas1;
                                    // tab3.Measure2 = GP.GreigeP_Meas2;
                                    // tab3.Measure3 = GP.GreigeP_Meas3;
                                    // tab3.Measure4 = GP.GreigeP_Meas4;
                                    // tab3.Measure5 = GP.GreigeP_Meas5;
                                    // tab3.Measure6 = GP.GreigeP_Meas6;
                                    // tab3.Measure7 = GP.GreigeP_Meas7;
                                    // tab3.Measure8 = GP.GreigeP_Meas8;

                                    foreach (var Reason in Reasons)
                                    {
                                        cnt += 1;

                                        if (cnt == 1)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 2)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 3)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 4)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas8;
                                            }
                                        }
                                    }
                                    tab3.Colour = "Greige";
                                    tab3.Remarks = GP.GreigeP_Remarks;
                                    tab3.GVRowNumber = record.DYEBO_GVRowNumber;
                                }

                                datatable3.AddDataTable3Row(tab3);
                            }

                        }
                        else
                        {
                            var DyeOrder = context.TLDYE_DyeOrderFabric.Find(DyeBatch.DYEB_DyeOrder_FK);

                            DataSet5.DataTable2Row xnr = datatable2.NewDataTable2Row();
                            xnr.Key = _Pk;
                            xnr.DyeBatch = DyeBatch.DYEB_BatchNo;
                            xnr.DyeOrder = DyeOrder.TLDYEF_DyeOrderNo;

                            var Customer = context.TLADM_CustomerFile.Find(DyeOrder.TLDYEF_Customer_FK);
                            if (Customer != null)
                            {
                                xnr.Customer = Customer.Cust_Description;
                            }

                            xnr.DateOrdered = DyeOrder.TLDYEF_OrderDate;
                            xnr.DOWeekNo = core.GetIso8601WeekOfYear(DyeOrder.TLDYEF_OrderDate);

                            DateTime dt = core.FirstDateOfWeek(DyeOrder.TLDYEF_OrderDate.Year, DyeOrder.TLDYEF_DyeWeek);
                            dt = dt.AddDays(5);
                            xnr.DateWeekRequired = dt;
                            xnr.WRWeekNo = DyeOrder.TLDYEF_DyeWeek;

                            xnr.Notes = DyeBatch.DYEB_Notes;

                            var Colour = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK);
                            if (Colour != null)
                            {
                                xnr.Colour = Colour.Col_Display;
                            }

                            var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).OrderBy(x => x.DYEBO_GVRowNumber).ToList();
                            xnr.TotalPieces = DyeBatchDetails.Count();
                            xnr.TotalKg = DyeBatchDetails.Sum(x => x.DYEBD_GreigeProduction_Weight);
                            datatable2.AddDataTable2Row(xnr);

                            DataSet5.DataTable1Row newr = datatable1.NewDataTable1Row();
                            newr.Key = _Pk;

                            var GroupDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).GroupBy(x => x.DYEBO_GVRowNumber).ToList();
                            var Cnt = 0;
                            foreach (var Grp in GroupDetail)
                            {
                                if (Grp.FirstOrDefault().DYEBD_BodyTrim)
                                {
                                    int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                    var Qual = context.TLADM_Griege.Find(Greige_FK);
                                    if (Qual != null)
                                    {
                                        newr.BodyQualtity = Qual.TLGreige_Description;
                                        newr.BodyKg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                        newr.BodyPieces = Grp.Count();
                                    }
                                }
                                else
                                {
                                    if (++Cnt == 1)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim1Quality = Qual.TLGreige_Description;
                                            newr.Trim1Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim1Pieces = Grp.Count();
                                        }
                                    }
                                    else if (Cnt == 2)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim2Quality = Qual.TLGreige_Description;
                                            newr.Trim2Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim2Pieces = Grp.Count();
                                        }
                                    }
                                    else if (Cnt == 3)
                                    {
                                        int Greige_FK = Grp.FirstOrDefault().DYEBD_QualityKey;
                                        var Qual = context.TLADM_Griege.Find(Greige_FK);
                                        if (Qual != null)
                                        {
                                            newr.Trim3Quality = Qual.TLGreige_Description;
                                            newr.Trim3Kg = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                            newr.Trim3Pieces = Grp.Count();
                                        }
                                    }

                                }

                            }

                            datatable1.AddDataTable1Row(newr);

                            foreach (var record in DyeBatchDetails)
                            {
                                DataSet5.DataTable3Row tab3 = datatable3.NewDataTable3Row();
                                var GP = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK);
                                if (GP != null)
                                {
                                    tab3.Key = _Pk;
                                    tab3.Piece = GP.GreigeP_PieceNo;
                                    tab3.Gross = GP.GreigeP_weight;
                                    tab3.Nett = string.Empty;
                                    tab3.Tex = GP.GreigeP_YarnTex;
                                    tab3.Pallet = GP.GreigeP_PalletNo;

                                    var Greige = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk);
                                    if (Greige != null)
                                        tab3.Quality = Greige.TLGreige_Description;

                                    var KnitOrder = context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk);
                                    if (KnitOrder != null)
                                    {
                                        tab3.KnitOrder = KnitOrder.KnitO_OrderNumber;
                                        tab3.Colour = "Greige";
                                    }


                                    var cnt = 0;




                                    foreach (var Reason in Reasons)
                                    {
                                        cnt += 1;

                                        if (cnt == 1)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure1 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 2)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure2 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 3)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure3 = GP.GreigeP_Meas8;
                                            }
                                        }
                                        else if (cnt == 4)
                                        {
                                            if (Reason.QD_ColumnIndex == 1)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas1;
                                            }
                                            else if (Reason.QD_ColumnIndex == 2)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas2;
                                            }
                                            else if (Reason.QD_ColumnIndex == 3)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas3;
                                            }
                                            else if (Reason.QD_ColumnIndex == 4)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas4;
                                            }
                                            else if (Reason.QD_ColumnIndex == 5)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas5;
                                            }
                                            else if (Reason.QD_ColumnIndex == 6)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas6;
                                            }
                                            else if (Reason.QD_ColumnIndex == 7)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas7;
                                            }
                                            else if (Reason.QD_ColumnIndex == 8)
                                            {
                                                tab3.Measure4 = GP.GreigeP_Meas8;
                                            }
                                        }
                                    }

                                    tab3.MergeDetail = GP.GreigeP_MergeDetail;
                                    tab3.Remarks = GP.GreigeP_Remarks;
                                    tab3.GVRowNumber = record.DYEBO_GVRowNumber;
                                }
                                datatable3.AddDataTable3Row(tab3);
                            }
                        }
                    }
                }

                ColumnNames = new string[][]
                    {   new string[] {"Text67", "Piece"},
                        new string[] {"Text68", "Gross"},
                        new string[] {"Text69", "Nett"},
                        new string[] {"Text70", "Quality"},
                        new string[] {"Text71", "Tex"},
                        new string[] {"Text72", "Pallet"},
                        new string[] {"Text73", "K/Order"},
                        new string[] {"Text74", "Colour"} ,
                        new string[] {"Text75", "NI"},
                        new string[] {"Text76", "BIN"},
                        new string[] {"Text29", "Remarks"},
                        new string[] {"Text78", "g/m3"},
                        new string[] {"Text30", "DS"},
                        new string[] {"Text81", "H"},
                        new string[] {"Text82", "PO"}
                    };

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                ds.Tables.Add(datatable3);

                GDyeBatch xtst = new GDyeBatch();
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;

                /*
                foreach(var Reason in Reasons)
                {
                    var result = (from u in ColumnNames
                                  where u[1].Length == 0
                                  select u).FirstOrDefault();

                    if (result != null)
                        result[1] = Reason.QD_ShortCode;
                }
                */


                IEnumerator ie = xtst.Section2.ReportObjects.GetEnumerator();

                // IEnumerator ie = xtst.Subreports[0].ReportDefinition.ReportObjects.GetEnumerator();

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

                /*
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
                                    to.Text = Reason.QD_ShortCode;
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
                */


            }
            else if (_RepNo == 5)  // This report produces the 3 Across x Down of "Lable" Slips 
            {
                DataSet ds = new DataSet();
                DataSet6.DataTable1DataTable datatable = new DataSet6.DataTable1DataTable();
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_Griege> _Qualities = null;
                IList<TLADM_Styles> _Styles = null;
                IList<TLADM_CustomerFile> _Customers = null;
                using (var context = new TTI2Entities())
                {
                    _Colours = context.TLADM_Colours.ToList();
                    _Qualities = context.TLADM_Griege.ToList();
                    _Styles = context.TLADM_Styles.ToList();
                    _Customers = context.TLADM_CustomerFile.ToList();

                    var DyeBatch = context.TLDYE_DyeBatch.Find(_Pk);
                    if (DyeBatch != null)
                    {
                        if (!DyeBatch.DYEB_FabricMode)
                        {
                            var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                            if (DyeOrder != null)
                            {
                                //===================================================
                                StringBuilder Customer = new StringBuilder();
                                Customer.Append(_Customers.FirstOrDefault(s => s.Cust_Pk == DyeOrder.TLDYO_Customer_FK).Cust_Description);
                                Customer.Append(" " + _Styles.FirstOrDefault(s => s.Sty_Id == DyeOrder.TLDYO_Style_FK).Sty_Description);
                                //==============================================================
                                var Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeOrder.TLDYO_Colour_FK);
                                var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).OrderBy(x => x.DYEBO_GVRowNumber).ToList();

                                int count = 0;

                                foreach (var record in DyeBatchDetails)
                                {
                                    DataSet6.DataTable1Row nr = datatable.NewDataTable1Row();

                                    nr.BatchNo = DyeBatch.DYEB_BatchNo;
                                    nr.Customer = Customer.ToString();
                                    nr.Colour = Colour.Col_Display;

                                    nr.Order = DyeOrder.TLDYO_DyeOrderNum;

                                    nr.Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == record.DYEBD_QualityKey).TLGreige_Description;
                                    var GP = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK);
                                    if (GP != null)
                                    {
                                        nr.Piece = GP.GreigeP_PieceNo;
                                        nr.Gross = GP.GreigeP_weight;
                                    }

                                    nr.Nett = string.Empty;
                                    nr.Width = string.Empty;

                                    nr.Remarks = "*No claims accepted after cutting";
                                    nr.SubRemarks = "*Do not mix dye lots";

                                    nr.SequenceNo = ++count;

                                    datatable.AddDataTable1Row(nr);

                                }
                            }
                            else
                            {
                                StringBuilder Customer = new StringBuilder();
                                Customer.Append(_Customers.FirstOrDefault(s => s.Cust_Pk == DyeBatch.DYEB_Customer_FK).Cust_Description);
                                var Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeBatch.DYEB_Colour_FK);

                                var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).OrderBy(x => x.DYEBO_GVRowNumber).ToList();

                                int count = 0;

                                foreach (var record in DyeBatchDetails)
                                {
                                    DataSet6.DataTable1Row nr = datatable.NewDataTable1Row();

                                    nr.BatchNo = DyeBatch.DYEB_BatchNo;
                                    nr.Customer = Customer.ToString();
                                    nr.Colour = Colour.Col_Display;
                                    nr.Order = "Not Applicable";
                                    nr.Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == record.DYEBD_QualityKey).TLGreige_Description;
                                    var GP = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK);
                                    if (GP != null)
                                    {
                                        nr.Piece = GP.GreigeP_PieceNo;
                                        nr.Gross = GP.GreigeP_weight;
                                    }

                                    nr.Nett = string.Empty;
                                    nr.Width = string.Empty;

                                    nr.Remarks = "*No claims accepted after cutting";
                                    nr.SubRemarks = "*Do not mix dye lots";

                                    nr.SequenceNo = ++count;

                                    datatable.AddDataTable1Row(nr);

                                }
                            }
                        }
                        else
                        {
                            var DyeOrder = context.TLDYE_DyeOrderFabric.Find(DyeBatch.DYEB_DyeOrder_FK);
                            if (DyeOrder != null)
                            {
                                StringBuilder Customer = new StringBuilder();
                                Customer.Append(_Customers.FirstOrDefault(s => s.Cust_Pk == DyeOrder.TLDYEF_Customer_FK).Cust_Description);
                                var Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeOrder.TLDYEF_Colours_FK);

                                var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).OrderBy(x => x.DYEBO_GVRowNumber).ToList();

                                int count = 0;

                                foreach (var record in DyeBatchDetails)
                                {
                                    DataSet6.DataTable1Row nr = datatable.NewDataTable1Row();
                                    nr.BatchNo = DyeBatch.DYEB_BatchNo;
                                    nr.Customer = Customer.ToString();
                                    nr.Colour = Colour.Col_Display;

                                    nr.Order = DyeOrder.TLDYEF_DyeOrderNo;

                                    nr.Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == record.DYEBD_QualityKey).TLGreige_Description;

                                    var GP = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK);
                                    if (GP != null)
                                    {
                                        nr.Piece = GP.GreigeP_PieceNo;
                                        nr.Gross = GP.GreigeP_weight;
                                    }

                                    nr.Nett = string.Empty;
                                    nr.Width = string.Empty;

                                    nr.Remarks = "*No claims accepted after cutting";
                                    nr.SubRemarks = "*Do not mix dye lots";

                                    nr.SequenceNo = ++count;

                                    datatable.AddDataTable1Row(nr);

                                }
                            }
                        }
                    }
                    ds.Tables.Add(datatable);

                    rep1 xtst = new rep1();
                    xtst.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = xtst;
                }
            }
            else if (_RepNo == 6)
            {
                DataSet ds = new DataSet();
                DataSet7.TLADM_GriegeDataTable greige = new DataSet7.TLADM_GriegeDataTable();
                DataSet7.TLDYE_DyeBatchDataTable batch = new DataSet7.TLDYE_DyeBatchDataTable();
                DataSet7.TLDYE_DyeBatchDetailsDataTable batchDetails = new DataSet7.TLDYE_DyeBatchDetailsDataTable();
                // DataSet7.TLKNI_GreigeProductionDataTable prodTable = new DataSet7.TLKNI_GreigeProductionDataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLDYE_DyeBatch.Find(_Pk);
                    if (Existing != null)
                    {
                        DataSet7.TLDYE_DyeBatchRow br = batch.NewTLDYE_DyeBatchRow();
                        br.DYEB_BatchKG = Existing.DYEB_BatchKG;
                        br.DYEB_BatchNo = Existing.DYEB_BatchNo;
                        br.DYEB_BatchDate = (DateTime)Existing.DYEB_BatchDate;
                        br.DYEB_Pk = Existing.DYEB_Pk;

                        batch.AddTLDYE_DyeBatchRow(br);

                        var Gr = context.TLADM_Griege.ToList();
                        foreach (var row in greige)
                        {
                            DataSet7.TLADM_GriegeRow grow = greige.NewTLADM_GriegeRow();
                            grow.TLGreige_Description = row.TLGreige_Description;
                            grow.TLGreige_Id = row.TLGreige_Id;

                            greige.AddTLADM_GriegeRow(grow);
                        }


                        var bd = context.TLDYE_DyeBatchDetails.ToList();
                        foreach (var row in bd)
                        {
                            DataSet7.TLDYE_DyeBatchDetailsRow dbr = batchDetails.NewTLDYE_DyeBatchDetailsRow();
                            dbr.DYEBD_DyeBatch_FK = row.DYEBD_DyeBatch_FK;
                            dbr.DYEBD_GreigeProduction_FK = row.DYEBD_GreigeProduction_FK;
                            dbr.DYEBD_GreigeProduction_Weight = row.DYEBD_GreigeProduction_Weight;
                            dbr.DYEBD_Pk = row.DYEBD_Pk;
                            dbr.DYEBD_QualityKey = row.DYEBD_QualityKey;

                            batchDetails.AddTLDYE_DyeBatchDetailsRow(dbr);
                        }

                        /*
                        var pt = context.TLKNI_GreigeProduction.ToList();
                        foreach (var row in pt)
                        {
                            DataSet7.TLKNI_GreigeProductionRow gpr = prodTable.NewTLKNI_GreigeProductionRow();
                            gpr.GreigeP_PieceNo = row.GreigeP_PieceNo;
                            gpr.GreigeP_Pk = row.GreigeP_Pk;
                            gpr.GreigeP_weight = row.GreigeP_weight;

                            prodTable.AddTLKNI_GreigeProductionRow(gpr);
                        }
                         */

                    }
                }
                ds.Tables.Add(greige);
                ds.Tables.Add(batch);
                ds.Tables.Add(batchDetails);
                //   ds.Tables.Add(prodTable);

                DyeBatchTransfer xtst = new DyeBatchTransfer();
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;

            }
            else if (_RepNo == 7)   //NCR Report
            {
                DataSet ds = new DataSet();
                DataSet8.DataTable1DataTable datatable1 = new DataSet8.DataTable1DataTable();
                DataSet8.DataTable2DataTable datatable2 = new DataSet8.DataTable2DataTable();
                DataSet8.DataTable3DataTable datatable3 = new DataSet8.DataTable3DataTable();
                using (var context = new TTI2Entities())
                {
                    var nonc = context.TLDYE_NonCompliance.Where(x => x.TLDYE_NcrNumber == _Pk).FirstOrDefault();
                    if (nonc != null)
                    {
                        DataSet8.DataTable1Row nr = datatable1.NewDataTable1Row();

                        nr.pk = 1;
                        nr.Date = nonc.TLDYE_ComplainceDate;

                        var DB = context.TLDYE_DyeBatch.Find(nonc.TLDYE_NcrBatchNo_FK);
                        if (DB != null)
                        {
                            nr.BatchNumber = DB.DYEB_BatchNo;
                            nr.BatchKg = DB.DYEB_BatchKG;
                            nr.NCRNumber = nonc.TLDYE_NcrNumber.ToString();

                            var Dept = context.TLADM_Departments.Find(nonc.TLDYE_Department_FK);
                            if (Dept != null)
                                nr.Department = Dept.Dep_Description;


                            var Oper = context.TLADM_MachineOperators.Find(nonc.TLDYE_Operator_FK);
                            if (Oper != null)
                            {
                                nr.Operator = Oper.MachOp_Description;
                            }

                            var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                var DA = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                if (DA != null)
                                {
                                    var Mach = context.TLADM_MachineDefinitions.Find(DA.TLDYEA_MachCode_FK);
                                    if (Mach != null)
                                    {
                                        nr.Machine = Mach.MD_AlternateDesc;
                                    }
                                }

                                var Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK);
                                if (Colour != null)
                                {
                                    nr.Colour = Colour.Col_Display;
                                }

                                var Qual = context.TLADM_Griege.Find(DO.TLDYO_Greige_FK);
                                if (Qual != null)
                                {
                                    nr.Quality = Qual.TLGreige_Description;
                                }

                            }
                            else
                            {

                                var DA = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                if (DA != null)
                                {
                                    var Mach = context.TLADM_MachineDefinitions.Find(DA.TLDYEA_MachCode_FK);
                                    if (Mach != null)
                                    {
                                        nr.Machine = Mach.MD_AlternateDesc;
                                    }
                                }

                                var Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK);
                                if (Colour != null)
                                {
                                    nr.Colour = Colour.Col_Display;
                                }

                            }

                            var TransDesc = context.TLADM_QADyeProcess.Where(x => x.QADYEP_Pk == nonc.TLDYE_NCStage).FirstOrDefault();
                            if (TransDesc != null)
                                nr.NCRDescription = TransDesc.QADYEP_Description;

                            datatable1.AddDataTable1Row(nr);

                            var Existing = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == _Pk).OrderBy(x => x.DYENCRD_Code_FK).ToList();
                            foreach (var Record in Existing)
                            {
                                DataSet8.DataTable2Row xnr = datatable2.NewDataTable2Row();
                                xnr.pk = 1;
                                if (Record.DYENCRD_FR)
                                {
                                    var Fault = context.TLADM_DyeQDCodes.Find(Record.DYENCRD_Code_FK);
                                    if (Fault != null)
                                    {
                                        xnr.Code = Fault.QDF_Code;
                                        xnr.CodeDescription = Fault.QDF_Description;
                                        xnr.DefaultorRemedy = true;

                                    }
                                }
                                else
                                {
                                    var Remedy = context.TLDYE_RecipeDefinition.Find(Record.DYENCRD_Code_FK);
                                    if (Remedy != null)
                                    {
                                        xnr.Code = Remedy.TLDYE_DefineCode;
                                        xnr.CodeDescription = Remedy.TLDYE_DefineDescription;
                                        xnr.DefaultorRemedy = false;

                                    }

                                    /*
                                    var Remedy = context.TLADM_DyeRemendyCodes.Find(Record.DYENCRD_Code_FK);
                                    if (Remedy != null)
                                    {
                                        xnr.Code = Remedy.QRC_Code;
                                        xnr.CodeDescription = Remedy.QRC_Description;
                                        xnr.DefaultorRemedy = false;

                                    }
                                     */

                                }
                                datatable2.AddDataTable2Row(xnr);
                            }


                            var ConsExisting = context.TLDYE_NonComplianceConsDetail.Where(x => x.DYENCCON_ConNumber == _Pk).OrderBy(x => x.DYENCCON_Code_FK).ToList();
                            if (ConsExisting.Count != 0)
                            {
                                foreach (var Record in ConsExisting)
                                {
                                    DataSet8.DataTable3Row xnr = datatable3.NewDataTable3Row();
                                    if (Record.DYENCCON_ConOrNon)
                                    {
                                        xnr.ConOrNon = true;
                                        var record = context.TLADM_ConsumablesDC.Find(Record.DYENCCON_Code_FK);
                                        if (record != null)
                                        {
                                            xnr.Code = record.ConsDC_Code;
                                            xnr.CodeDescription = record.ConsDC_Description;
                                        }
                                    }
                                    else
                                    {
                                        xnr.ConOrNon = false;
                                        var record = context.TLADM_NonStockItems.Find(Record.DYENCCON_Code_FK);
                                        if (record != null)
                                        {
                                            xnr.Code = record.NSI_Code;
                                            xnr.CodeDescription = record.NSI_Description;
                                        }
                                    }

                                    xnr.Quantities = Record.DYENCCON_Qunatities;
                                    datatable3.AddDataTable3Row(xnr);
                                }
                            }
                            else
                            {
                                DataSet8.DataTable3Row xnr = datatable3.NewDataTable3Row();
                                xnr.pk = 1;
                                datatable3.AddDataTable3Row(xnr);
                            }

                        }


                    }

                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                ds.Tables.Add(datatable3);
                NCRReport ncRep = new NCRReport();
                ncRep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ncRep;
            }
            else if (_RepNo == 8)       // Transfer Report ex frmTransferToDyeHouse
            {
                DataSet ds = new DataSet();
                DataSet9.TLADM_TranactionTypeDataTable TranTable = new DataSet9.TLADM_TranactionTypeDataTable();
                DataSet9.TLADM_WhseStore1DataTable ToStore = new DataSet9.TLADM_WhseStore1DataTable();
                DataSet9.TLADM_WhseStoreDataTable FromStore = new DataSet9.TLADM_WhseStoreDataTable();
                DataSet9.TLDYE_DyeBatchDataTable BatchTable = new DataSet9.TLDYE_DyeBatchDataTable();
                DataSet9.TLDYE_DyeBatchDetailsDataTable BatchTableDetails = new DataSet9.TLDYE_DyeBatchDetailsDataTable();

                using (var context = new TTI2Entities())
                {
                    var BT = context.TLDYE_DyeBatch.Where(x => x.DYEB_Pk == _Pk).FirstOrDefault();
                    if (BT != null)
                    {
                        DataSet9.TLDYE_DyeBatchRow dbr = BatchTable.NewTLDYE_DyeBatchRow();
                        dbr.DYEB_BatchKG = BT.DYEB_BatchKG;
                        dbr.DYEB_BatchNo = BT.DYEB_BatchNo;
                        dbr.DYEB_TransferDate = (DateTime)BT.DYEB_TransferDate;
                        dbr.DYEB_TransactionType_FK = BT.DYEB_TransactionType_FK;
                        dbr.DYEB_Pk = BT.DYEB_Pk;

                        BatchTable.AddTLDYE_DyeBatchRow(dbr);

                        var BTDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == dbr.DYEB_Pk).ToList();
                        foreach (var row in BTDetails)
                        {
                            DataSet9.TLDYE_DyeBatchDetailsRow dbdr = BatchTableDetails.NewTLDYE_DyeBatchDetailsRow();
                            dbdr.DYEBD_DyeBatch_FK = row.DYEBD_DyeBatch_FK;
                            dbdr.DYEBD_GreigeProduction_FK = row.DYEBD_GreigeProduction_FK;
                            dbdr.DYEBD_GreigeProduction_Weight = row.DYEBD_GreigeProduction_Weight;
                            dbdr.DYEBD_QualityKey = row.DYEBD_QualityKey;
                            dbdr.DYEBD_Pk = row.DYEBD_Pk;
                            var Qual = context.TLADM_Griege.Find(row.DYEBD_QualityKey);
                            if (Qual != null)
                                dbdr.Quality = Qual.TLGreige_Description;

                            var PN = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if (PN != null)
                                dbdr.Piece_Number = PN.GreigeP_PieceNo;


                            BatchTableDetails.AddTLDYE_DyeBatchDetailsRow(dbdr);
                        }


                        var FS = context.TLADM_WhseStore.ToList();
                        foreach (var row in FS)
                        {
                            DataSet9.TLADM_WhseStoreRow wr = FromStore.NewTLADM_WhseStoreRow();
                            wr.WhStore_Code = row.WhStore_Code;
                            wr.WhStore_Description = row.WhStore_Description;
                            wr.WhStore_Id = row.WhStore_Id;

                            FromStore.AddTLADM_WhseStoreRow(wr);
                        }

                        var TS = context.TLADM_WhseStore.ToList();
                        foreach (var row in TS)
                        {
                            DataSet9.TLADM_WhseStore1Row wr1 = ToStore.NewTLADM_WhseStore1Row();
                            wr1.WhStore_Code = row.WhStore_Code;
                            wr1.WhStore_Description = row.WhStore_Description;
                            wr1.WhStore_Id = row.WhStore_Id;

                            ToStore.AddTLADM_WhseStore1Row(wr1);
                        }

                        var TT = context.TLADM_TranactionType.ToList();
                        foreach (var row in TT)
                        {
                            DataSet9.TLADM_TranactionTypeRow ttr = TranTable.NewTLADM_TranactionTypeRow();
                            ttr.TrxT_Department_FK = row.TrxT_Department_FK;
                            ttr.TrxT_Description = row.TrxT_Description;
                            ttr.TrxT_FromWhse_FK = (int)row.TrxT_FromWhse_FK;
                            ttr.TrxT_Pk = row.TrxT_Pk;
                            ttr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;

                            TranTable.AddTLADM_TranactionTypeRow(ttr);
                        }

                    }
                }

                ds.Tables.Add(TranTable);
                ds.Tables.Add(ToStore);
                ds.Tables.Add(FromStore);
                ds.Tables.Add(BatchTable);
                ds.Tables.Add(BatchTableDetails);

                TransferReport ncRep = new TransferReport();
                ncRep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ncRep;

            }

            else if (_RepNo == 9)       // Transfer Report ex frmCommissionDyeing
            {
                DataSet ds = new DataSet();
                DataSet10.TLADM_TranactionTypeDataTable TranTypeTable = new DataSet10.TLADM_TranactionTypeDataTable();
                DataSet10.TLADM_WhseStore1DataTable ToTable = new DataSet10.TLADM_WhseStore1DataTable();
                DataSet10.TLADM_WhseStoreDataTable FromTable = new DataSet10.TLADM_WhseStoreDataTable();
                DataSet10.TLDYE_ComDyeBatchDataTable DyeBatchTable = new DataSet10.TLDYE_ComDyeBatchDataTable();
                DataSet10.TLKNI_GreigeCommissionTransctionsDataTable CommissionTransTable = new DataSet10.TLKNI_GreigeCommissionTransctionsDataTable();
                DataSet10.TLADM_GriegeDataTable GriegeTable = new DataSet10.TLADM_GriegeDataTable();
                using (var context = new TTI2Entities())
                {
                    var DB = context.TLDYE_ComDyeBatch.Find(_Pk);
                    if (DB != null)
                    {
                        DataSet10.TLDYE_ComDyeBatchRow dbr = DyeBatchTable.NewTLDYE_ComDyeBatchRow();

                        dbr.DYEBC_BatchDate = DB.DYEBC_BatchDate;
                        dbr.DYEBC_BatchNo = DB.DYEBC_BatchNo;
                        dbr.DYEBC_DateRequired = DB.DYEBC_DateRequired;
                        dbr.DYEBC_Greige_FK = DB.DYEBC_Greige_FK;
                        dbr.DYEBC_Pk = DB.DYEBC_Pk;
                        dbr.DYEBC_TransactionType = DB.DYEBC_TransactionType;
                        dbr.DYEBC_Trim_Fk = DB.DYEBC_Trim_Fk;
                        DyeBatchTable.AddTLDYE_ComDyeBatchRow(dbr);

                        var Existing = context.TLKNI_GreigeCommissionTransctions.ToList();
                        foreach (var row in Existing)
                        {
                            DataSet10.TLKNI_GreigeCommissionTransctionsRow ctr = CommissionTransTable.NewTLKNI_GreigeCommissionTransctionsRow();
                            if (row.GreigeCom_DyeBatch_FK == null)
                                continue;

                            ctr.GreigeCom_DyeBatch_FK = (int)row.GreigeCom_DyeBatch_FK;
                            ctr.GreigeCom_NettWeight = row.GreigeCom_NettWeight;
                            ctr.GreigeCom_PieceNo = row.GreigeCom_PieceNo;
                            ctr.GreigeCom_PK = row.GreigeCom_PK;
                            ctr.GreigeCom_ProductType_FK = row.GreigeCom_ProductType_FK;

                            CommissionTransTable.AddTLKNI_GreigeCommissionTransctionsRow(ctr);
                        }

                        var TT = context.TLADM_TranactionType.ToList();
                        foreach (var row in TT)
                        {
                            DataSet10.TLADM_TranactionTypeRow ttr = TranTypeTable.NewTLADM_TranactionTypeRow();
                            ttr.TrxT_FromWhse_FK = (int)row.TrxT_FromWhse_FK;
                            ttr.TrxT_Pk = row.TrxT_Pk;
                            ttr.TrxT_ToWhse_FK = (int)row.TrxT_ToWhse_FK;
                            TranTypeTable.AddTLADM_TranactionTypeRow(ttr);
                        }

                        var FromTab = context.TLADM_WhseStore.ToList();
                        foreach (var row in FromTab)
                        {
                            DataSet10.TLADM_WhseStoreRow ws = FromTable.NewTLADM_WhseStoreRow();
                            ws.WhStore_Code = row.WhStore_Code;
                            ws.WhStore_Description = row.WhStore_Description;
                            ws.WhStore_Id = row.WhStore_Id;

                            FromTable.AddTLADM_WhseStoreRow(ws);
                        }

                        foreach (var row in FromTab)
                        {
                            DataSet10.TLADM_WhseStore1Row ws = ToTable.NewTLADM_WhseStore1Row();
                            ws.WhStore_Code = row.WhStore_Code;
                            ws.WhStore_Description = row.WhStore_Description;
                            ws.WhStore_Id = row.WhStore_Id;

                            ToTable.AddTLADM_WhseStore1Row(ws);
                        }

                        var GR = context.TLADM_Griege.ToList();
                        foreach (var row in GR)
                        {
                            DataSet10.TLADM_GriegeRow grr = GriegeTable.NewTLADM_GriegeRow();
                            grr.TLGreige_Description = row.TLGreige_Description;
                            grr.TLGreige_Id = row.TLGreige_Id;

                            GriegeTable.AddTLADM_GriegeRow(grr);
                        }
                    }
                }

                ds.Tables.Add(TranTypeTable);
                ds.Tables.Add(ToTable);
                ds.Tables.Add(FromTable);
                ds.Tables.Add(DyeBatchTable);
                ds.Tables.Add(CommissionTransTable);
                ds.Tables.Add(GriegeTable);

                DyeCommission ncRep = new DyeCommission();
                ncRep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ncRep;
            }
            else if (_RepNo == 10)       // Outstanding Dye Order Orders Report
            {
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable datatable1 = new DataSet11.DataTable1DataTable();
                var _repo = new DyeHouse.DyeRepository();

                var Existing = _repo.SelectActiveDyeOrders(_parms).ToList();

                using (var context = new TTI2Entities())
                {
                    foreach (var row in Existing)
                    {
                        var ExistDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == row.TLDYO_Pk && x.TLDYOD_BodyOrTrim).ToList();

                        foreach (var Detailrow in ExistDetails)
                        {
                            DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();
                            nr.OrderNo = row.TLDYO_DyeOrderNum;

                            var Cust = context.TLADM_CustomerFile.Find(row.TLDYO_Customer_FK);
                            if (Cust != null)
                                nr.Customer = Cust.Cust_Description;

                            var Qual = context.TLADM_Griege.Find(Detailrow.TLDYOD_Greige_FK);
                            if (Qual != null)
                                nr.Quality = Qual.TLGreige_Description;

                            var Colour = context.TLADM_Colours.Find(row.TLDYO_Colour_FK);
                            if (Colour != null)
                                nr.Colour = Colour.Col_Display;

                            nr.Style = context.TLADM_Styles.Find(row.TLDYO_Style_FK).Sty_Description;

                            var ProdRating = context.TLADM_ProductRating.Find(Detailrow.TLDYOD_MarkerRating_FK);
                            if (ProdRating != null)
                                nr.Sizes = ProdRating.Pr_Display;

                            nr.Units = Detailrow.TLDYOD_OriginalUnit;
                            nr.OrderKg = ExistDetails.Sum(x => (decimal?)x.TLDYOD_Kgs) ?? 0.00M;

                            //--------------------------------
                            //
                            //-------------------------------------
                            nr.BatchKg = (from T1 in context.TLDYE_DyeBatch
                                          join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                          where T1.DYEB_DyeOrder_FK == row.TLDYO_Pk
                                          select T2).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;

                            nr.OuStandingKg = nr.OrderKg - nr.BatchKg;
                            datatable1.AddDataTable1Row(nr);
                        }
                    }

                }

                ds.Tables.Add(datatable1);
                OutstandingDyeOrders ncRep = new OutstandingDyeOrders();
                ncRep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ncRep;
            }
            else if (_RepNo == 11)       // Fabric Transfer Document
            {
                DataSet ds = new DataSet();
                DataSet12.DataTable1DataTable datatable1 = new DataSet12.DataTable1DataTable();
                DataSet12.DataTable2DataTable datatable2 = new DataSet12.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var DyeT = context.TLDYE_DyeTransactions.Find(_Pk);
                    if (DyeT != null)
                    {
                        DataSet12.DataTable1Row tab1 = datatable1.NewDataTable1Row();
                        tab1.Key = 1;
                        tab1.AuthorisedBy = DyeT.TLDYET_AuthorisedBy;
                        tab1.BatchNumber = DyeT.TLDYET_BatchNo;
                        tab1.FabricKg = DyeT.TLDYET_BatchWeight;
                        tab1.Date = DyeT.TLDYET_Date;
                        tab1.TransferNumber = DyeT.TLDYET_TransactionNumber;

                        // I have used the Commission variable to store whether 
                        // this is a standard transfer document to the reject store or a reversal from the reject store
                        //============================================================================================
                        if (!_Commission)
                            tab1.Title = "Fabric Transfer Document";
                        else
                            tab1.Title = "Fabric Reversal Transfer Document";

                        var DyeBatch = context.TLDYE_DyeBatch.Find(DyeT.TLDYET_Batch_FK);
                        if (DyeBatch != null)
                        {
                            tab1.FabricKg = DyeBatch.DYEB_BatchKG;

                            var Colour = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK);
                            if (Colour != null)
                            {
                                tab1.Colour = Colour.Col_Description;
                            }
                            var DyeAllocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                            if (DyeAllocated != null)
                            {
                                var FabricType = context.TLADM_FabricProduct.Find(DyeAllocated.TLDYEA_FabricType_FK);
                                if (FabricType != null)
                                {
                                    tab1.Quality = FabricType.FP_Description;
                                }
                            }

                            var AOp = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                            if (AOp != null)
                            {
                                var Operator = context.TLADM_MachineOperators.Find(AOp.DYEOP_Operator_FK);
                                if (Operator != null)
                                    tab1.Operator = Operator.MachOp_Description;
                            }
                        }

                        datatable1.AddDataTable1Row(tab1);

                        //Now get all the records pertaining to this batch 
                        //------------------------------------------------------------
                        var BatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk && x.DYEBO_WasRejected).ToList();
                        foreach (var row in BatchDetails)
                        {
                            DataSet12.DataTable2Row tab2 = datatable2.NewDataTable2Row();
                            tab2.Key = 1;
                            tab2.GreigeGross = row.DYEBD_GreigeProduction_Weight;
                            tab2.FabicNett = row.DYEBO_Nett;

                            var Qual = context.TLADM_Griege.Find(row.DYEBD_QualityKey);
                            if (Qual != null)
                                tab2.Quality = Qual.TLGreige_Description;

                            var GP = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if (GP != null)
                                tab2.PieceNo = GP.GreigeP_PieceNo;

                            row.DYEBO_TransferPrinted = true;

                            datatable2.AddDataTable2Row(tab2);
                        }

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

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                rejectFabric rf = new rejectFabric();

                rf.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rf;

            }
            else if (_RepNo == 12)       // Dye Transaction Report
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable datatable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable datatable2 = new DataSet13.DataTable2DataTable();

                IList<TLDYE_DyeBatch> Existing = new List<TLDYE_DyeBatch>();

                using (var context = new TTI2Entities())
                {
                    if (_repOps.DBPending)
                    {
                        if (_repOps.AllCustomersSelected)
                        {
                            Existing = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= _repOps.fromDate && x.DYEB_BatchDate <= _repOps.toDate && x.DYEB_TransactionType_FK < 39).ToList();
                        }
                        else
                        {
                            Existing = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= _repOps.fromDate && x.DYEB_BatchDate <= _repOps.toDate && x.DYEB_TransactionType_FK < 39 && x.DYEB_Customer_FK == _repOps.CustomerNumberSelected).ToList();
                        }
                    }
                    else
                    {
                        if (_repOps.AllCustomersSelected)
                        {
                            Existing = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= _repOps.fromDate && x.DYEB_BatchDate <= _repOps.toDate && x.DYEB_TransactionType_FK == 39).ToList();
                        }
                        else
                        {
                            Existing = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= _repOps.fromDate && x.DYEB_BatchDate <= _repOps.toDate && x.DYEB_TransactionType_FK == 39 && x.DYEB_Customer_FK == _repOps.CustomerNumberSelected).ToList();
                        }
                    }

                    foreach (var row in Existing)
                    {
                        DataSet13.DataTable1Row dtr = datatable1.NewDataTable1Row();
                        dtr.Key = 1;
                        dtr.BatchDate = (DateTime)row.DYEB_BatchDate;
                        dtr.BatchNumber = row.DYEB_BatchNo;
                        try
                        {
                            dtr.BodyKg = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk && x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                            if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk && !x.DYEBD_BodyTrim).Count() != 0)
                                dtr.TrimsKg = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk && !x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                            else
                                dtr.TrimsKg = 0;
                            dtr.TotalKg = decimal.Parse(dtr.BodyKg.ToString()) + decimal.Parse(dtr.TrimsKg.ToString());
                            dtr.NoOfPieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).FirstOrDefault();
                        if (DBD != null)
                            dtr.Quality = context.TLADM_Griege.Find(DBD.DYEBD_QualityKey).TLGreige_Description;

                        datatable1.AddDataTable1Row(dtr);
                    }

                    DataSet13.DataTable2Row t2 = datatable2.NewDataTable2Row();
                    t2.Key = 1;
                    t2.FromDate = _repOps.fromDate;
                    t2.ToDate = _repOps.toDate;
                    if (_repOps.DBPending)
                    {
                        if (_repOps.SortByBatchNumber)
                            t2.ReportTitle = "Dye Batch Pending Report by Batch";
                        else if (_repOps.SortByDate)
                            t2.ReportTitle = "Dye Batch Pending Report By Date";
                        else if (_repOps.SortByQuality)
                            t2.ReportTitle = "Dye Batch Pending Report By Quality";
                    }
                    else
                    {
                        if (_repOps.SortByBatchNumber)
                            t2.ReportTitle = "Dye Batch WIP Report by Batch";
                        else if (_repOps.SortByDate)
                            t2.ReportTitle = "Dye Batch WIP Report By Date";
                        else if (_repOps.SortByQuality)
                            t2.ReportTitle = "Dye Batch WIP Report By Quality";
                    }
                    datatable2.AddDataTable2Row(t2);
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                if (_repOps.SortByBatchNumber)
                {
                    DyeBatchReport rf = new DyeBatchReport();
                    rf.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rf;
                }
                else if (_repOps.SortByDate)
                {
                    DyeBatchReportByDate rf = new DyeBatchReportByDate();
                    rf.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rf;
                }
                else if (_repOps.SortByQuality)
                {
                    DyeBatchReportByQual rf = new DyeBatchReportByQual();
                    rf.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rf;
                }

            }
            else if (_RepNo == 13)
            {
                DataSet ds = new DataSet();
                DataSet43.DataTable1DataTable dataTable1 = new DataSet43.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var GroupDCSoh = context.TLADM_ConsumablesDC.ToList();
                    foreach (var item in GroupDCSoh)
                    {
                        DataSet43.DataTable1Row nr = dataTable1.NewDataTable1Row();

                        nr.Code = item.ConsDC_Code;
                        nr.Description = item.ConsDC_Description;
                        nr.StdCost = item.ConsDC_StandardCost;
                        nr.ReOrderLevel = item.ConsDC_ReOrderLevel;
                        nr.SOHQuarantine = 0; // item.DYCSH_SOHQuar;
                        nr.SOHStore = 0;      //  item.DYCSH_StockOnHand;
                        nr.SOHDyeKitchen = 0; //  item.DYCSH_SOHKitchen;
                        nr.StockTakeFreq = context.TLADM_StockTakeFreq.Find(item.ConsDC_StockTake_FK).STF_Description;
                        nr.PreferredSupplier = context.TLADM_Suppliers.Find(item.ConsDC_PreferedSupplier_FK).Sup_Description;

                        var ConsPk = item.ConsDC_Pk;

                        var Consumable = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == ConsPk).ToList();
                        if (Consumable.Count > 0)
                        {
                            foreach (var Cons in Consumable)
                            {
                                nr.SOHQuarantine += Cons.DYCSH_SOHQuar;
                                nr.SOHStore += Cons.DYCSH_StockOnHand;
                                nr.SOHDyeKitchen += Cons.DYCSH_K_Opening + Cons.DYCSH_SOHKitchen - Cons.DYCSH_K_Used + Cons.DCSH_K_Adjusted;
                            }

                        }

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                DCSOH soh = new DCSOH();
                soh.SetDataSource(ds);
                crystalReportViewer1.ReportSource = soh;

            }
            else if (_RepNo == 14)  // DyeOrder 
            {
                DataSet ds = new DataSet();
                DataSet15.DataTable1DataTable dataTable1 = new DataSet15.DataTable1DataTable();

                IList<TLDYE_DyeOrder> Garments = new List<TLDYE_DyeOrder>();
                IList<TLDYE_DyeOrderFabric> Fabric = new List<TLDYE_DyeOrderFabric>();

                DataTable dt = new DataTable();
                dt.Columns.Add("OrderNumber", typeof(string));       // 0
                dt.Columns.Add("Date", typeof(DateTime));            // 1
                dt.Columns.Add("Quality", typeof(String));           // 2
                dt.Columns.Add("Colour", typeof(String));            // 3 
                dt.Columns.Add("OrderKg", typeof(decimal));          // 4
                dt.Columns.Add("UnitsQty", typeof(int));             // 5
                dt.Columns.Add("Batched", typeof(decimal));          // 6
                dt.Columns.Add("Outstanding", typeof(decimal));      // 7
                dt.Columns.Add("DueDate", typeof(string));           // 8
                dt.Columns.Add("Customer", typeof(string));          // 9
                dt.Columns.Add("CustomerOrder", typeof(string));     //10
                dt.Columns.Add("BatchedUnitQtys", typeof(int));      //11

                using (var context = new TTI2Entities())
                {
                    //-------------------------------------------------------------------
                    //Let start with the 
                    //-------------------------------------------------------------
                    if (_repOps.SelGarments && _repOps.DO_Both)
                    {
                        Garments = context.TLDYE_DyeOrder.ToList();
                    }
                    else if (_repOps.SelGarments && _repOps.DO_Open)
                    {
                        Garments = context.TLDYE_DyeOrder.Where(x => !x.TLDYO_Closed).ToList();
                    }
                    else if (_repOps.SelGarments && _repOps.DO_Closed)
                    {
                        Garments = context.TLDYE_DyeOrder.Where(x => x.TLDYO_Closed).ToList();
                    }
                    else if (_repOps.SelFabric)
                    {
                        if (!_repOps.DO_Closed)
                        {
                            Fabric = context.TLDYE_DyeOrderFabric.ToList();
                        }
                        else
                        {
                            Fabric = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_Closed).ToList();
                        }
                    }
                    else if (_repOps.SelBoth)
                    {
                        if (_repOps.DO_Closed)
                        {
                            Garments = context.TLDYE_DyeOrder.Where(x => x.TLDYO_Closed).ToList();
                            Fabric = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_Closed).ToList();
                        }
                        else
                        {
                            Garments = context.TLDYE_DyeOrder.ToList();
                            Fabric = context.TLDYE_DyeOrderFabric.ToList();
                        }
                    }

                    if (Garments.Count > 0)
                    {
                        foreach (var row in Garments)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = row.TLDYO_DyeOrderNum;
                            dr[1] = row.TLDYO_OrderDate;
                            dr[2] = context.TLADM_Griege.Find(row.TLDYO_Greige_FK).TLGreige_Description;
                            dr[3] = context.TLADM_Colours.Find(row.TLDYO_Colour_FK).Col_Display;
                            dr[7] = dr[4] = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == row.TLDYO_Pk && x.TLDYOD_BodyOrTrim).ToList().Sum(X => (decimal?)X.TLDYOD_Kgs) ?? 0.00M;
                            dr[5] = 0;
                            dr[6] = 0;
                            dr[11] = 0;

                            if (context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == row.TLDYO_Pk).Count() != 0)
                            {
                                var Units = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == row.TLDYO_Pk && x.TLDYOD_BodyOrTrim).Sum(x => x.TLDYOD_OriginalUnit);
                                dr[5] = Units;
                            }

                            var DBatchDetail = from DyeBatch in context.TLDYE_DyeBatch
                                               join DyeBatchDetail in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetail.DYEBD_DyeBatch_FK
                                               where DyeBatchDetail.DYEBD_BodyTrim && DyeBatch.DYEB_DyeOrder_FK == row.TLDYO_Pk
                                               select DyeBatchDetail;


                            if (DBatchDetail.Count() != 0)
                            {
                                var Greige = context.TLADM_Griege.Find(row.TLDYO_Greige_FK);
                                if (Greige != null)
                                {
                                    var FabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                    var FabWidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                    var FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                                    var Rating = DBatchDetail.FirstOrDefault().DYEBO_ProductRating_FK;
                                    var BatchWeight = DBatchDetail.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                    if (Rating != 0)
                                    {
                                        var ExpectedUnits = Math.Round((FabricYield / Rating) * BatchWeight, 0);
                                        dr[11] = (int.Parse(dr[11].ToString()) + ExpectedUnits).ToString();
                                    }
                                    else
                                    {
                                        dr[11] = 0;
                                    }
                                    dr[6] = (decimal.Parse(dr[6].ToString()) + BatchWeight).ToString();
                                    dr[7] = (decimal.Parse(dr[4].ToString()) - decimal.Parse(dr[6].ToString())).ToString();
                                }
                            }
                            //------------------------------------------------------------
                            var date = core.FirstDateOfWeek(row.TLDYO_OrderDate.Year, row.TLDYO_DyeReqWeek);
                            dr[8] = date.AddDays(5);
                            dr[9] = context.TLADM_CustomerFile.Find(row.TLDYO_Customer_FK).Cust_Description;
                            dr[10] = row.TLDYO_OrderNum;

                            dt.Rows.Add(dr);
                        }
                    }
                    if (Fabric.Count != 0)
                    {
                        foreach (var row in Fabric)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = row.TLDYEF_DyeOrderNo;
                            dr[1] = row.TLDYEF_OrderDate;
                            dr[2] = context.TLADM_Griege.Find(row.TLDYEF_Greige_FK).TLGreige_Description;
                            dr[3] = context.TLADM_Colours.Find(row.TLDYEF_Colours_FK).Col_Display;
                            dr[4] = row.TLDYEF_Demand;
                            dr[5] = 0;
                            var Batch = context.TLDYE_DyeBatch.Where(x => x.DYEB_DyeOrder_FK == row.TLDYEF_Pk).FirstOrDefault();
                            if (Batch != null)
                                dr[6] = Batch.DYEB_BatchKG;
                            else
                                dr[6] = 0.00M;

                            dr[7] = decimal.Parse(dr[4].ToString()) - decimal.Parse(dr[6].ToString());
                            var date = core.FirstDateOfWeek(row.TLDYEF_OrderDate.Year, row.TLDYEF_DyeWeek);
                            dr[8] = date.AddDays(5);
                            dr[9] = context.TLADM_CustomerFile.Find(row.TLDYEF_Customer_FK).Cust_Description;
                            dr[10] = row.TLDYEF_CustomerOrder;
                            dt.Rows.Add(dr);
                        }
                    }

                    foreach (DataRow rw in dt.Rows)
                    {
                        DataSet15.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                        dtr.DyeOrderNumber = rw[0].ToString();
                        dtr.Date = DateTime.Parse(rw[1].ToString());
                        dtr.Quality = rw[2].ToString();
                        dtr.Colour = rw[3].ToString();

                        dtr.Kgs = Decimal.Parse(rw[4].ToString());
                        dtr.UnitsQty = Int32.Parse(rw[5].ToString());
                        dtr.BatchedKgs = Decimal.Parse(rw[6].ToString());
                        dtr.OutstandingKgs = Decimal.Parse(rw[7].ToString());
                        dtr.DateDue = DateTime.Parse(rw[8].ToString());
                        dtr.Customer = rw[9].ToString();
                        dtr.OrderNumber = rw[10].ToString();
                        dtr.BatchedUnitsQty = Int32.Parse(rw[11].ToString());
                        dataTable1.AddDataTable1Row(dtr);

                    }
                }
                ds.Tables.Add(dataTable1);

                if (_repOps.DO_ByOrderNo)
                {

                    DODetails dod = new DODetails();
                    dod.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = dod;
                }
                else
                {
                    DODetailsOther dodother = new DODetailsOther();
                    if (_repOps.DO_OptionSelected == 1)
                    {
                        dodother.DataDefinition.Groups[0].ConditionField = dodother.Database.Tables[0].Fields[1];
                    }
                    else if (_repOps.DO_OptionSelected == 2)
                    {
                        dodother.DataDefinition.Groups[0].ConditionField = dodother.Database.Tables[0].Fields[2];
                    }
                    else if (_repOps.DO_OptionSelected == 3)
                    {
                        dodother.DataDefinition.Groups[0].ConditionField = dodother.Database.Tables[0].Fields[3];
                    }
                    else if (_repOps.DO_OptionSelected == 4)
                    {
                        dodother.DataDefinition.Groups[0].ConditionField = dodother.Database.Tables[0].Fields[9];
                    }
                    else if (_repOps.DO_OptionSelected == 5)
                    {
                        dodother.DataDefinition.Groups[0].ConditionField = dodother.Database.Tables[0].Fields[8];
                    }

                    dodother.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = dodother;
                }
            }
            else if (_RepNo == 15)  // Fabric Stock Adjustment 
            {
                DataSet ds = new DataSet();
                DataSet16.DataTable1DataTable dataTable1 = new DataSet16.DataTable1DataTable();
                DataSet16.DataTable2DataTable dataTable2 = new DataSet16.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Record = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Pk == _Pk).FirstOrDefault();
                    if (Record != null)
                    {
                        DataSet16.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                        dtr.Pk = 1;
                        dtr.Date = Record.TLDYET_Date;
                        dtr.AdjustmentNo = Record.TLDYET_TransactionNumber;
                        dtr.ApprovedBy = Record.TLDYET_AuthorisedBy;
                        dtr.Store = context.TLADM_WhseStore.Find(Record.TLDYET_CurrentStore_FK).WhStore_Description;

                        dataTable1.AddDataTable1Row(dtr);


                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == Record.TLDYET_Batch_FK && x.DYEBO_AdjustedWeight != 0).ToList();
                        foreach (var row in Existing)
                        {
                            if (row.DYEBO_TransactionNo != Record.TLDYET_TransactionNumber)
                                continue;

                            DataSet16.DataTable2Row dr = dataTable2.NewDataTable2Row();
                            dr.Pk = 1;
                            dr.PieceNo = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                            dr.Quality = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                            var DBD = context.TLDYE_DyeBatch.Find(row.DYEBD_DyeBatch_FK);
                            if (DBD != null)
                            {
                                dr.Colour = context.TLADM_Colours.Find(DBD.DYEB_Colour_FK).Col_Display;
                            }

                            dr.BatchNo = Record.TLDYET_BatchNo;
                            if (row.DYEBO_AdjustedWeight < 0)
                                dr.Adjustment = "Decrease";
                            else
                                dr.Adjustment = "Increase";

                            dr.FabricWeight = row.DYEBO_AdjustedWeight;

                            dataTable2.AddDataTable2Row(dr);

                        }
                    }
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                FStockAdjustment dod = new FStockAdjustment();
                dod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dod;

            }
            else if (_RepNo == 16)  // Commission Dyeing as well as Fabric Sales Delivery Note Summary 
            {
                DataSet ds = new DataSet();
                DataSet17.DataTable1DataTable datatable1 = new DataSet17.DataTable1DataTable();
                DataSet17.DataTable2DataTable datatable2 = new DataSet17.DataTable2DataTable();
                bool First = true;

                TLKNI_GreigeCommissionTransctions CommissionCust = null;

                using (var context = new TTI2Entities())
                {
                    if (!_parms.FabricSales)
                    {
                        foreach (var DB in _parms.DyeBatches)
                        {

                            if (First)
                            {
                                First = !First;
                                var Customer = context.TLADM_CustomerFile.Find(DB.DYEB_Customer_FK);
                                if (Customer != null)
                                {
                                    DataSet17.DataTable1Row tr = datatable1.NewDataTable1Row();
                                    tr.Pk = 1;
                                    tr.CustomerAddress = Customer.Cust_Address1;
                                    tr.CustomerName = Customer.Cust_Description;
                                    tr.CustomerPhone = Customer.Cust_Telephone;
                                    // tr.Date = (DateTime)Grp.First().DYEBO_DateSold;
                                    tr.DeliveryNote = _TransNumber;
                                    tr.Notes = _parms.Notes.ToString();
                                    tr.TransDate = DateTime.Now;
                                    tr.PostalCode = string.Empty;

                                    datatable1.AddDataTable1Row(tr);
                                }

                            }
                            if (_parms.CommDyeing)
                            {
                                var ExistingGrps = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).GroupBy(x => x.DYEBD_DyeBatch_FK);
                                foreach (var Grp in ExistingGrps)
                                {
                                    CommissionCust = context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                    if (CommissionCust != null)
                                    {
                                        DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();

                                        tr2.Pk = 1;
                                        tr2.YourOrderRef = CommissionCust.GreigeCom_CustOrderNo;
                                        tr2.YourRef = CommissionCust.GreigeCom_Custdoc;
                                        tr2.TotalPieces = Grp.Count();
                                        tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_FinishedCode;
                                        tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                        var QKey = Grp.First().DYEBD_QualityKey;
                                        tr2.Quality = context.TLADM_Griege.Find(QKey).TLGreige_Description;
                                        tr2.Gross = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                        tr2.Nett = Grp.Sum(x => x.DYEBO_Nett);
                                        tr2.Meters = Grp.Sum(x => x.DYEBO_Meters);
                                        tr2.OurBatch = context.TLDYE_DyeBatch.Find(CommissionCust.GreigeCom_DyeBatch_FK).DYEB_BatchNo;
                                        tr2.Date = (DateTime)Grp.FirstOrDefault().DYEBO_DateSold;

                                        datatable2.AddDataTable2Row(tr2);
                                    }
                                    else
                                    {

                                        DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                        tr2.Pk = 1;
                                        tr2.YourOrderRef = ""; // CommissionCust.GreigeCom_CustOrderNo;
                                        tr2.YourRef = ""; // CommissionCust.GreigeCom_Custdoc;
                                        tr2.TotalPieces = Grp.Count();
                                        tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_FinishedCode;
                                        tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                        tr2.Quality = context.TLADM_Griege.Find(Grp.First().DYEBD_QualityKey).TLGreige_Description;
                                        tr2.Gross = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                        tr2.Nett = Grp.Sum(x => x.DYEBO_Nett);
                                        tr2.Meters = Grp.Sum(x => x.DYEBO_Meters);
                                        tr2.OurBatch = DB.DYEB_BatchNo;
                                        tr2.Date = (DateTime)Grp.FirstOrDefault().DYEBO_DateSold;

                                        datatable2.AddDataTable2Row(tr2);


                                    }
                                }
                            }
                            else
                            {
                                var ExistingGrps = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).GroupBy(x => new { x.DYEBD_QualityKey });
                                /*var ExistingGrps = (from T1 in context.TLDYE_DyeBatch
                                           join T2 in context.TLDYE_DyeBatchDetails
                                           on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                           where T2.DYEBD_DyeBatch_FK == DB.DYEB_Pk
                                           select new { T2, T1 }).GroupBy(x => new { x.T2.DYEBD_QualityKey, x.T1.DYEB_Colour_FK }).ToList();*/


                                foreach (var Grp in ExistingGrps)
                                {
                                    DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                    tr2.Pk = 1;
                                    tr2.YourOrderRef = ""; // CommissionCust.GreigeCom_CustOrderNo;
                                    tr2.YourRef = ""; // CommissionCust.GreigeCom_Custdoc;
                                    tr2.TotalPieces = Grp.Count();
                                    tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_FinishedCode;
                                    tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                    tr2.Quality = context.TLADM_Griege.Find(Grp.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                    tr2.Gross = Grp.Sum(x => x.DYEBD_GreigeProduction_Weight);
                                    tr2.Nett = Grp.Sum(x => x.DYEBO_Nett);
                                    tr2.Meters = Grp.Sum(x => x.DYEBO_Meters);
                                    tr2.OurBatch = DB.DYEB_BatchNo;
                                    tr2.Date = (DateTime)Grp.FirstOrDefault().DYEBO_DateSold;

                                    datatable2.AddDataTable2Row(tr2);

                                }
                            }
                        }
                    }
                    else   // Fabric Sales remember the DB.DYEB_Pk has been substituted with the DyeTran Primary Key
                           // this was done for simplicity sake duhhhhhhhh fool!!!!!!!!;
                    {
                        foreach (var Tran in _parms.DyeTransactions)
                        {
                            First = true;

                            var DyeTran = context.TLDYE_DyeTransactions.Find(Tran.TLDYET_Pk);
                            if (DyeTran != null)
                            {
                                if (First)
                                {
                                    First = !First;
                                    var Customer = context.TLADM_CustomerFile.Find(DyeTran.TLDYET_Customer_FK);
                                    if (Customer != null)
                                    {
                                        DataSet17.DataTable1Row tr = datatable1.NewDataTable1Row();
                                        tr.Pk = 1;
                                        tr.CustomerAddress = Customer.Cust_Address1;
                                        tr.CustomerName = Customer.Cust_Description;
                                        tr.CustomerPhone = Customer.Cust_Telephone;
                                        tr.DeliveryNote = DyeTran.TLDYET_TransactionNumber;
                                        tr.Notes = _parms.Notes.ToString();
                                        tr.TransDate = DateTime.Now;
                                        tr.PostalCode = Customer.Cust_Code;
                                        datatable1.AddDataTable1Row(tr);

                                        var ExistingGrps = context.TLDYE_DyeBatchDetails
                                                           .Where(x => x.DYEBO_TransactionNo == DyeTran.TLDYET_TransactionNumber)
                                                           .GroupBy(x => new { x.DYEBD_DyeBatch_FK, x.DYEBD_QualityKey });



                                        foreach (var Grp in ExistingGrps)
                                        {
                                            DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                            tr2.Pk = 1;
                                            tr2.YourOrderRef = "";
                                            tr2.YourRef = "";
                                            tr2.TotalPieces = Grp.Count(); // Grp.T2..FirstOrDefault().T2.Count();

                                            var xDB = context.TLDYE_DyeBatch.Find(Grp.FirstOrDefault().DYEBD_DyeBatch_FK);
                                            if (xDB != null)
                                            {
                                                tr2.ColourCode = context.TLADM_Colours.Find(xDB.DYEB_Colour_FK).Col_FinishedCode;
                                                tr2.ColourDescription = context.TLADM_Colours.Find(xDB.DYEB_Colour_FK).Col_Description;
                                                tr2.OurBatch = xDB.DYEB_BatchNo;
                                            }

                                            tr2.Quality = context.TLADM_Griege.Find(Grp.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                            tr2.Gross = Grp.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                            tr2.Nett = Grp.Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.0M;
                                            tr2.Meters = Grp.Sum(x => (decimal?)x.DYEBO_Meters) ?? 0M;
                                            if (!String.IsNullOrEmpty(Grp.FirstOrDefault().DYEBO_DateSold.ToString()))
                                            {
                                                tr2.Date = (DateTime)Grp.FirstOrDefault().DYEBO_DateSold;
                                            }
                                            else
                                            {
                                                tr2.Date = DateTime.Now;
                                            }
                                            datatable2.AddDataTable2Row(tr2);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                SumComDelivery sumComDelivery = new SumComDelivery();
                sumComDelivery.SetDataSource(ds);
                crystalReportViewer1.ReportSource = sumComDelivery;
            }
            else if (_RepNo == 17)  // Commission Dyeing Delivery Note Details as well as Fabric Sales
            {
                DataSet ds = new DataSet();
                DataSet17.DataTable1DataTable datatable1 = new DataSet17.DataTable1DataTable();
                DataSet17.DataTable2DataTable datatable2 = new DataSet17.DataTable2DataTable();
                bool first = true;

                IList<TLKNI_GreigeCommissionTransctions> CommissionCust = null;
                IList<TLDYE_DyeBatchDetails> DyeBatchDetails = null;
                using (var context = new TTI2Entities())
                {
                    if (!_parms.FabricSales)
                    {
                        foreach (var DB in _parms.DyeBatches)
                        {

                            //1st Find the batch 
                            //-----------------------------------------------------------------------
                            var Customer = context.TLADM_CustomerFile.Find(DB.DYEB_Customer_FK);
                            if (Customer != null && first)
                            {
                                first = !first;
                                DataSet17.DataTable1Row tr = datatable1.NewDataTable1Row();
                                tr.Pk = 1;
                                tr.CustomerAddress = Customer.Cust_Address1;
                                tr.CustomerName = Customer.Cust_Description;
                                tr.CustomerPhone = Customer.Cust_Telephone;

                                tr.DeliveryNote = _TransNumber;
                                tr.Notes = Customer.Cust_ContactPerson;
                                tr.TransDate = DateTime.Now;
                                datatable1.AddDataTable1Row(tr);
                            }

                            if (_parms.CommDyeing || _parms.CommDyeingReprint)
                            {
                                CommissionCust = context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_DyeBatch_FK == DB.DYEB_Pk).ToList();
                                if (CommissionCust.Count != 0)
                                {
                                    foreach (var Commission in CommissionCust)
                                    {
                                        DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                        tr2.Pk = 1;
                                        tr2.YourOrderRef = Commission.GreigeCom_CustOrderNo;
                                        tr2.YourRef = Commission.GreigeCom_Custdoc;
                                        tr2.YourDeliveryNo = Commission.GreigeCom_DeliveryNo;
                                        tr2.TotalPieces = CommissionCust.Count();
                                        tr2.OurBatch = DB.DYEB_BatchNo;
                                        tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                        tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                        tr2.Quality = context.TLADM_Griege.Find(DB.DYEB_Greige_FK).TLGreige_Description;

                                        tr2.YourPieceNo = Commission.GreigeCom_PieceNo;
                                        tr2.OurPieceNo = Commission.GreigeCom_TTSNo;

                                        var GP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == Commission.GreigeCom_PK && x.GreigeP_PieceNo == Commission.GreigeCom_PieceNo).FirstOrDefault();
                                        if (GP != null)
                                        {
                                            var BatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == GP.GreigeP_Pk).FirstOrDefault();
                                            if (BatchDetail != null)
                                            {
                                                if (BatchDetail.DYEBO_DateSold != null)
                                                    tr2.Date = (DateTime)BatchDetail.DYEBO_DateSold;

                                                tr2.Gross = BatchDetail.DYEBD_GreigeProduction_Weight;
                                                tr2.Disk = (int)BatchDetail.DYEBO_DiskWeight;
                                                tr2.Wth = (int)BatchDetail.DYEBO_Width;
                                                tr2.Nett = BatchDetail.DYEBO_Nett;
                                                tr2.Meters = BatchDetail.DYEBO_Meters;
                                            }
                                        }

                                        datatable2.AddDataTable2Row(tr2);
                                    }
                                }
                                else
                                {
                                    DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList();
                                    foreach (var DyeDetail in DyeBatchDetails)
                                    {
                                        DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                        tr2.Pk = 1;
                                        tr2.YourOrderRef = ""; // Commission.GreigeCom_CustOrderNo;
                                        tr2.YourRef = "";  //Commission.GreigeCom_Custdoc;
                                        tr2.YourDeliveryNo = 0; // Commission.GreigeCom_DeliveryNo;
                                        tr2.TotalPieces = DyeBatchDetails.Count;

                                        tr2.OurBatch = DB.DYEB_BatchNo;
                                        tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                        tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                        tr2.Quality = context.TLADM_Griege.Find(DyeDetail.DYEBD_QualityKey).TLGreige_Description;

                                        tr2.Date = (DateTime)DyeDetail.DYEBO_DateSold;

                                        tr2.Gross = DyeDetail.DYEBD_GreigeProduction_Weight;
                                        tr2.Disk = (int)DyeDetail.DYEBO_DiskWeight;
                                        tr2.Wth = (int)DyeDetail.DYEBO_Width;
                                        tr2.Nett = DyeDetail.DYEBO_Nett;
                                        tr2.Meters = DyeDetail.DYEBO_Meters;

                                        var GP = context.TLKNI_GreigeProduction.Find(DyeDetail.DYEBD_GreigeProduction_FK);
                                        if (GP != null)
                                        {
                                            tr2.YourPieceNo = GP.GreigeP_PieceNo;
                                            tr2.OurPieceNo = GP.GreigeP_PieceNo;

                                        }

                                        datatable2.AddDataTable2Row(tr2);
                                    }
                                }
                            }
                            else
                            {

                                DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList();
                                foreach (var DyeDetail in DyeBatchDetails)
                                {
                                    DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                    tr2.Pk = 1;
                                    tr2.YourOrderRef = ""; // Commission.GreigeCom_CustOrderNo;
                                    tr2.YourRef = "";  //Commission.GreigeCom_Custdoc;
                                    tr2.YourDeliveryNo = 0; // Commission.GreigeCom_DeliveryNo;
                                    tr2.TotalPieces = DyeBatchDetails.Count;

                                    tr2.OurBatch = DB.DYEB_BatchNo;
                                    tr2.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                    tr2.ColourDescription = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                                    tr2.Quality = context.TLADM_Griege.Find(DB.DYEB_Greige_FK).TLGreige_Description;

                                    tr2.Date = (DateTime)DyeDetail.DYEBO_DateSold;

                                    tr2.Gross = DyeDetail.DYEBD_GreigeProduction_Weight;
                                    tr2.Disk = (int)DyeDetail.DYEBO_DiskWeight;
                                    tr2.Wth = (int)DyeDetail.DYEBO_Width;
                                    tr2.Nett = DyeDetail.DYEBO_Nett;
                                    tr2.Meters = DyeDetail.DYEBO_Meters;

                                    var GP = context.TLKNI_GreigeProduction.Find(DyeDetail.DYEBD_GreigeProduction_FK);
                                    if (GP != null)
                                    {
                                        tr2.YourPieceNo = GP.GreigeP_PieceNo;
                                        tr2.OurPieceNo = GP.GreigeP_PieceNo;

                                    }

                                    datatable2.AddDataTable2Row(tr2);
                                }

                            }
                        }
                    }
                    else    // This is for Fabric Sales 
                    {
                        var PrimeKey = 0;

                        foreach (var DBTran in _parms.DyeTransactions)
                        {
                            // 1st Find the batch in the Dye Tranaction 
                            // remember this is fabrics sales
                            //-----------------------------------------------------------------------

                            var DyeTran = context.TLDYE_DyeTransactions.Find(DBTran.TLDYET_Pk);
                            if (DyeTran != null)
                            {
                                first = true;

                                var Customer = context.TLADM_CustomerFile.Find(DyeTran.TLDYET_Customer_FK);
                                if (Customer != null && first)
                                {
                                    first = !first;
                                    DataSet17.DataTable1Row tr = datatable1.NewDataTable1Row();
                                    tr.Pk = ++PrimeKey;
                                    tr.CustomerAddress = Customer.Cust_Address1;
                                    tr.CustomerName = Customer.Cust_Description;
                                    tr.CustomerPhone = Customer.Cust_Telephone;

                                    tr.DeliveryNote = DyeTran.TLDYET_TransactionNumber;
                                    tr.Notes = Customer.Cust_ContactPerson.ToString();
                                    datatable1.AddDataTable1Row(tr);

                                    var DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_TransactionNo == DyeTran.TLDYET_TransactionNumber).ToList();

                                    foreach (var DBDetail in DBDetails)
                                    {
                                        var xDB = context.TLDYE_DyeBatch.Find(DBDetail.DYEBD_DyeBatch_FK);

                                        DataSet17.DataTable2Row tr2 = datatable2.NewDataTable2Row();
                                        tr2.Pk = PrimeKey;
                                        tr2.YourOrderRef = DyeTran.TLDYET_CustomerOrderNo;
                                        tr2.YourRef = "";
                                        tr2.YourDeliveryNo = 0;
                                        tr2.TotalPieces = DBDetails.Count();
                                        tr2.OurBatch = xDB.DYEB_BatchNo;
                                        tr2.ColourCode = context.TLADM_Colours.Find(xDB.DYEB_Colour_FK).Col_Display;
                                        tr2.ColourDescription = context.TLADM_Colours.Find(xDB.DYEB_Colour_FK).Col_Description;
                                        tr2.Quality = context.TLADM_Griege.Find(DBDetail.DYEBD_QualityKey).TLGreige_Description;

                                        var GP = context.TLKNI_GreigeProduction.Find(DBDetail.DYEBD_GreigeProduction_FK);
                                        if (GP != null)
                                        {
                                            tr2.OurPieceNo = GP.GreigeP_PieceNo;
                                            tr2.YourPieceNo = GP.GreigeP_PieceNo;
                                        }
                                        if (!_parms.FabricSalesPickingList)
                                        {
                                            if (!String.IsNullOrEmpty(DBDetail.DYEBO_DateSold.ToString()))
                                            {
                                                tr2.Date = (DateTime)DBDetail.DYEBO_DateSold;
                                            }
                                            else
                                            {
                                                tr2.Date = DateTime.Now;
                                            }
                                        }

                                        tr2.Gross = DBDetail.DYEBD_GreigeProduction_Weight;
                                        tr2.Disk = (int)DBDetail.DYEBO_DiskWeight;
                                        tr2.Wth = (int)DBDetail.DYEBO_Width;
                                        tr2.Nett = DBDetail.DYEBO_Nett;
                                        tr2.Meters = DBDetail.DYEBO_Meters;


                                        datatable2.AddDataTable2Row(tr2);

                                    }
                                }
                            }
                        }
                    }
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(datatable2);
                ComDelivery comDelivery = new ComDelivery();
                if (_parms.FabricSalesPickingList)
                {
                    TextObject text = (TextObject)comDelivery.ReportDefinition.Sections["Section1"].ReportObjects["Text2"];
                    text.Text = "Picking List";
                }
                comDelivery.SetDataSource(ds);
                crystalReportViewer1.ReportSource = comDelivery;


            }
            else if (_RepNo == 18)  // Fabric Sales  
            {
                DataSet ds = new DataSet();
                DataSet18.DataTable1DataTable dataTable1 = new DataSet18.DataTable1DataTable();
                DataSet18.DataTable2DataTable dataTable2 = new DataSet18.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var Record = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionNumber == _TransNumber).FirstOrDefault();
                    if (Record != null)
                    {
                        DataSet18.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.CustomerName = context.TLADM_CustomerFile.Find(Record.TLDYET_Customer_FK).Cust_Description;
                        nr.CustomerAddress = context.TLADM_CustomerFile.Find(Record.TLDYET_Customer_FK).Cust_Address1;
                        nr.DeliveryDate = Record.TLDYET_Date;
                        nr.DeliveryNote = Record.TLDYET_TransactionNumber;
                        nr.AttnOf = context.TLADM_CustomerFile.Find(Record.TLDYET_Customer_FK).Cust_ContactPerson;
                        nr.CustomerPh = context.TLADM_CustomerFile.Find(Record.TLDYET_Customer_FK).Cust_Telephone;

                        dataTable1.AddDataTable1Row(nr);

                        var BDGrp = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_TransactionNo == Record.TLDYET_TransactionNumber && x.DYEBO_PendingDelivery && x.DYEBO_SaleConfirmed).OrderBy(x => x.DYEBD_DyeBatch_FK).GroupBy(x => x.DYEBD_DyeBatch_FK);
                        foreach (var grp in BDGrp)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(grp.First().DYEBD_DyeBatch_FK);
                            var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_Pk);
                            foreach (var record in grp)
                            {
                                DataSet18.DataTable2Row t2 = dataTable2.NewDataTable2Row();
                                t2.Pk = 1;
                                t2.PieceNo = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                                if (DO != null)
                                    t2.CustomerOrderNo = DO.TLDYO_DyeOrderNum;
                                t2.TTSBatchNo = DB.DYEB_BatchNo;
                                t2.Quality = context.TLADM_Griege.Find(record.DYEBD_QualityKey).TLGreige_Description;
                                t2.Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                t2.Meters = record.DYEBO_Meters;
                                t2.Disk = record.DYEBO_DiskWeight;
                                t2.Width = record.DYEBO_Width;
                                t2.GrossWeight = record.DYEBD_GreigeProduction_Weight;
                                t2.NettWeight = record.DYEBO_Nett;

                                dataTable2.AddDataTable2Row(t2);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                FabricSales dod = new FabricSales();
                dod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dod;

            }
            else if (_RepNo == 19)  //Listing of outstanding pending dye orders   
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                DataSet19.DataTable2DataTable dataTable2 = new DataSet19.DataTable2DataTable();
                IList<TLDYE_DyeBatch> DyeBatches = null;


                using (var context = new TTI2Entities())
                {
                    DataSet19.DataTable1Row tr = dataTable1.NewDataTable1Row();
                    tr.Pk = 1;
                    tr.FromDate = _repOps.fromDate;
                    tr.ToDate = _repOps.toDate;
                    if (_repOps.DBPending)
                    {
                        tr.ReportTitle = "List of outstanding pending dye batches";
                    }
                    else if (_repOps.DBWIP)
                    {
                        tr.ReportTitle = "List of outstanding WIP batches";
                    }
                    else
                    {
                        tr.ReportTitle = "List of both outstanding  and pending dye batches";
                    }

                    dataTable1.AddDataTable1Row(tr);

                    if (_repOps.AllCustomersSelected)
                    {
                        if (_repOps.DBPending)
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK < 39 && !x.DYEB_Closed).ToList();
                        }
                        else if (_repOps.DBWIP)
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK == 39 && !x.DYEB_Closed).ToList();
                        }
                        else
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK < 40 && !x.DYEB_Closed).ToList();
                        }
                    }
                    else
                    {
                        if (_repOps.DBPending)
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK < 39 && x.DYEB_Customer_FK == _repOps.CustomerNumberSelected && !x.DYEB_Closed).ToList();
                        }
                        else if (_repOps.DBWIP)
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK == 39 && x.DYEB_Customer_FK == _repOps.CustomerNumberSelected && !x.DYEB_Closed).ToList();
                        }
                        else
                        {
                            DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_TransactionType_FK < 40 && x.DYEB_Customer_FK == _repOps.CustomerNumberSelected && !x.DYEB_Closed).ToList();
                        }
                    }

                    DyeBatches = DyeBatches.Where(x => !x.DYEB_OutProcess).ToList();

                    foreach (var DyeBatch in DyeBatches)
                    {
                        DataSet19.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                        dtr.Pk = 1;
                        dtr.BatchNumber = DyeBatch.DYEB_BatchNo.Remove(0, 2);
                        dtr.TransDate = (DateTime)DyeBatch.DYEB_BatchDate;
                        var ExpectedUnits = 0;
                        var TotalWeight = 0.00M;

                        var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk && x.DYEBD_BodyTrim).ToList();
                        if (DyeBatchDetail.Count != 0)
                        {
                            int QualityKey = DyeBatch.DYEB_Greige_FK;
                            int ProductRating = (int)DyeBatchDetail.FirstOrDefault().DYEBO_ProductRating_FK;

                            var Greige = context.TLADM_Griege.Find(QualityKey);
                            if (Greige != null)
                            {
                                var FabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                var FabWidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                var FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);
                                try
                                {
                                    if (!DyeBatch.DYEB_FabicSales)
                                    {
                                        var FabricRating = context.TLADM_ProductRating.Find(ProductRating).Pr_numeric_Rating;
                                        TotalWeight = DyeBatchDetail.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                        ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                    }
                                    else
                                    {
                                        ExpectedUnits = 0;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExpectedUnits = 0;
                                }

                            }
                        }
                        if (!DyeBatch.DYEB_CommissinCust)
                        {
                            if (!DyeBatch.DYEB_FabicSales)
                            {
                                var DO = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    dtr.DyeOrder = DO.TLDYO_OrderNum;
                                    dtr.Quality = context.TLADM_Griege.Find(DO.TLDYO_Greige_FK).TLGreige_Description;
                                    dtr.CustomerOrder = DO.TLDYO_OrderNum;
                                    dtr.Customer = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                    dtr.Units = ExpectedUnits;
                                    dtr.Style = context.TLADM_Styles.Find(DO.TLDYO_Style_FK).Sty_Description;
                                    dtr.Colour = context.TLADM_Colours.Find(DO.TLDYO_Colour_FK).Col_Display;
                                }

                            }
                            else
                            {
                                var DO = context.TLDYE_DyeOrderFabric.Find(DyeBatch.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    dtr.DyeOrder = "DO" + DO.TLDYEF_DyeOrderNumeric.ToString();
                                    dtr.Quality = context.TLADM_Griege.Find(DO.TLDYEF_Greige_FK).TLGreige_Description;
                                    dtr.CustomerOrder = DO.TLDYEF_CustomerOrder;
                                    dtr.Customer = context.TLADM_CustomerFile.Find(DO.TLDYEF_Customer_FK).Cust_Description;
                                    dtr.Units = 0;
                                    dtr.Style = string.Empty;
                                    dtr.Colour = context.TLADM_Colours.Find(DO.TLDYEF_Colours_FK).Col_Display;
                                }
                                else
                                {
                                    var DOF = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                    if (DOF != null)
                                    {
                                        dtr.DyeOrder = DOF.TLDYO_OrderNum;
                                        dtr.Quality = context.TLADM_Griege.Find(DOF.TLDYO_Greige_FK).TLGreige_Description;
                                        dtr.CustomerOrder = DOF.TLDYO_OrderNum;
                                        dtr.Customer = context.TLADM_CustomerFile.Find(DOF.TLDYO_Customer_FK).Cust_Description;
                                        dtr.Units = 0;
                                        dtr.Style = String.Empty;
                                        dtr.Colour = context.TLADM_Colours.Find(DOF.TLDYO_Colour_FK).Col_Display;
                                    }
                                }
                            }
                        }
                        else
                        {
                            dtr.Customer = context.TLADM_CustomerFile.Find(DyeBatch.DYEB_Customer_FK).Cust_Description;
                            dtr.Units = 0;
                            dtr.Style = string.Empty;
                        }

                        if (DyeBatch.DYEB_RequiredDate != null)
                            dtr.DueDate = (DateTime)DyeBatch.DYEB_RequiredDate;

                        dtr.Kg = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                        dtr.BodyKg = TotalWeight;
                        dtr.Pieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).Count();
                        dtr.Reprocess = false;
                        dtr.Trims = dtr.Kg - TotalWeight;

                        dataTable2.AddDataTable2Row(dtr);
                    }


                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                if (_repOps.DO_OptionSelected == 0)
                {
                    OutStandingDyeBatch outB = new OutStandingDyeBatch();
                    outB.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = outB;
                }
                else
                {
                    AlternateDyeBatch outB = new AlternateDyeBatch();
                    if (_repOps.DO_OptionSelected == 1)
                    {
                        outB.DataDefinition.Groups[0].ConditionField = outB.Database.Tables[1].Fields[4];
                    }
                    else if (_repOps.DO_OptionSelected == 2)
                    {
                        outB.DataDefinition.Groups[0].ConditionField = outB.Database.Tables[1].Fields[5];
                    }
                    else if (_repOps.DO_OptionSelected == 3)
                    {
                        outB.DataDefinition.Groups[0].ConditionField = outB.Database.Tables[1].Fields[11];
                    }


                    outB.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = outB;
                }
            }
            else if (_RepNo == 20)  //Listing of rejected dye batches   
            {
                DataSet ds = new DataSet();
                DataSet20.DataTable1DataTable dataTable1 = new DataSet20.DataTable1DataTable();
                DataSet20.DataTable2DataTable dataTable2 = new DataSet20.DataTable2DataTable();
                IList<TLDYE_DyeTransactions> trans = new List<TLDYE_DyeTransactions>();
                IList<TLDYE_DyeTransactions> temp = new List<TLDYE_DyeTransactions>();
                DateTime _fromDate = new DateTime();
                DateTime _toDate = new DateTime();

                using (var context = new TTI2Entities())
                {


                    if (_repOps.ops3_ComboSelected == 0)
                    {
                        int index = _repOps.ops3_ComboSelectedValue;
                        trans = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Pk == index).ToList();
                        if (trans.Count != 0)
                        {
                            _fromDate = (DateTime)trans.First().TLDYET_RejectDate;
                            _toDate = (DateTime)trans.First().TLDYET_RejectDate;
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 1)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var Mach = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == row.TLDYET_Batch_FK).FirstOrDefault();
                            if (Mach != null)
                            {
                                if (Mach.TLDYEA_MachCode_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_RejectDate;

                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_RejectDate;
                                }
                            }

                        }

                    }
                    else if (_repOps.ops3_ComboSelected == 2)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var Mach = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == row.TLDYET_Batch_FK).FirstOrDefault();
                            if (Mach != null)
                            {
                                if (Mach.DYEOP_Operator_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_RejectDate;
                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_RejectDate;


                                }
                            }

                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 3)
                    {
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 4)
                    {
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 5)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                            if (DB != null)
                            {
                                var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk && x.DYEBD_BodyTrim).FirstOrDefault();
                                if (DBD != null)
                                {
                                    if (DBD.DYEBD_QualityKey == _repOps.ops3_ComboSelectedValue)
                                    {
                                        if (_first)
                                        {
                                            _first = false;
                                        }
                                        trans.Add(row);
                                        _toDate = (DateTime)row.TLDYET_RejectDate;
                                    }
                                }
                            }
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 6)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                            if (DB != null)
                            {
                                if (DB.DYEB_Colour_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_RejectDate;
                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_RejectDate;
                                }
                            }
                        }
                    }

                    foreach (var row in trans)
                    {
                        DataSet20.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                        dtr.pk = 1;
                        dtr.FistDyed = row.TLDYET_Date;
                        dtr.DateRejected = (DateTime)row.TLDYET_RejectDate;

                        var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                        if (DB != null)
                        {
                            dtr.ColourCode = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                            dtr.BatchNumber = DB.DYEB_BatchNo;

                            var DBA = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                            if (DBA != null)
                                dtr.DyeMachine = context.TLADM_MachineDefinitions.Find(DBA.TLDYEA_MachCode_FK).MD_Description;


                            dtr.TotalKg = DB.DYEB_BatchKG;
                            dtr.DueDate = (DateTime)DB.DYEB_RequiredDate;
                            dtr.TotalPieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).Count();

                            var OPR = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DB.DYEB_Pk).FirstOrDefault();
                            if (OPR != null)
                                dtr.Operator = context.TLADM_MachineOperators.Find(OPR.DYEOP_Operator_FK).MachOp_Description;

                            if (DB.DYEB_DyeOrder_FK > 0)
                            {
                                var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    dtr.Customer = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                }

                                dtr.CustomerOrder = DO.TLDYO_OrderNum;
                                dtr.Quality = context.TLADM_Griege.Find(DO.TLDYO_Greige_FK).TLGreige_Description;
                            }

                            var NC = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_BatchNo_Fk == DB.DYEB_Pk && x.DYENCRD_FR).FirstOrDefault();
                            if (NC != null)
                            {
                                dtr.FaultCode = context.TLADM_DyeQDCodes.Find(NC.DYENCRD_Code_FK).QDF_Description;
                            }

                            var NCR = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_BatchNo_Fk == DB.DYEB_Pk && !x.DYENCRD_FR).FirstOrDefault();
                            if (NCR != null)
                            {
                                dtr.RemedyCode = context.TLADM_DyeRemendyCodes.Find(NCR.DYENCRD_Code_FK).QRC_Description;
                            }


                        }

                        dataTable2.AddDataTable2Row(dtr);
                    }

                    DataSet20.DataTable1Row tr = dataTable1.NewDataTable1Row();
                    tr.pk = 1;
                    tr.FromDate_ = _fromDate;
                    tr.ToDate = _toDate;
                    tr.ReportTitle = "List of rejected dye batches";
                    dataTable1.AddDataTable1Row(tr);

                }


                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                RejectedDyBatch rdb = new RejectedDyBatch();
                rdb.SetDataSource(ds);

                if (_repOps.DO_OptionSelected == 0)
                {
                    //"Batch Number , Quality , Colour"
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[1];
                    rdb.DataDefinition.Groups[1].ConditionField = rdb.Database.Tables[1].Fields[4];
                    rdb.DataDefinition.Groups[2].ConditionField = rdb.Database.Tables[1].Fields[5];
                }
                else if (_repOps.DO_OptionSelected == 1)
                {
                    // Dye Machine, Operator, FaultCode
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[9];
                    rdb.DataDefinition.Groups[1].ConditionField = rdb.Database.Tables[1].Fields[10];
                    rdb.DataDefinition.Groups[2].ConditionField = rdb.Database.Tables[1].Fields[13];
                }
                else if (_repOps.DO_OptionSelected == 2)
                {
                    // Dye Machine, Operator, Cause
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[9];
                    rdb.DataDefinition.Groups[1].ConditionField = rdb.Database.Tables[1].Fields[10];
                    rdb.DataDefinition.Groups[2].ConditionField = rdb.Database.Tables[1].Fields[14];
                }
                else if (_repOps.DO_OptionSelected == 3)
                {
                    //"Colour, Quality, Dye Machine"
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[5];
                    rdb.DataDefinition.Groups[1].ConditionField = rdb.Database.Tables[1].Fields[4];
                    rdb.DataDefinition.Groups[2].ConditionField = rdb.Database.Tables[1].Fields[9];
                }

                crystalReportViewer1.ReportSource = rdb;

            }
            else if (_RepNo == 21)  //Listing of reprocessed batches    
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable1 = new DataSet21.DataTable1DataTable();
                DataSet21.DataTable2DataTable dataTable2 = new DataSet21.DataTable2DataTable();
                IList<TLDYE_DyeTransactions> trans = new List<TLDYE_DyeTransactions>();
                TLDYE_DyeTransactions transBatch = new TLDYE_DyeTransactions();
                IList<TLDYE_DyeTransactions> temp = new List<TLDYE_DyeTransactions>();
                DateTime _fromDate = new DateTime();
                DateTime _toDate = new DateTime();
                using (var context = new TTI2Entities())
                {
                    if (_repOps.ops3_ComboSelected == 0)
                    {
                        int index = _repOps.ops3_ComboSelectedValue;
                        transBatch = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Pk == index).FirstOrDefault();
                        if (transBatch != null)
                        {
                            _fromDate = (DateTime)transBatch.TLDYET_Date;
                            _toDate = (DateTime)transBatch.TLDYET_Date;
                            trans.Add(transBatch);
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 1)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var Mach = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == row.TLDYET_Batch_FK).FirstOrDefault();
                            if (Mach != null)
                            {
                                if (Mach.TLDYEA_MachCode_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_Date;

                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_Date;
                                }
                            }

                        }

                    }
                    else if (_repOps.ops3_ComboSelected == 2)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var Mach = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == row.TLDYET_Batch_FK).FirstOrDefault();
                            if (Mach != null)
                            {
                                if (Mach.DYEOP_Operator_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_Date;
                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_Date;
                                }
                            }
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 3)
                    {
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 4)
                    {
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 5)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                            if (DB != null)
                            {
                                var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk && x.DYEBD_BodyTrim).FirstOrDefault();
                                if (DBD != null)
                                {
                                    if (DBD.DYEBD_QualityKey == _repOps.ops3_ComboSelectedValue)
                                    {
                                        if (_first)
                                        {
                                            _first = false;
                                        }
                                        trans.Add(row);
                                        _toDate = (DateTime)row.TLDYET_Date;
                                    }
                                }
                            }
                        }
                    }
                    else if (_repOps.ops3_ComboSelected == 6)
                    {
                        bool _first = true;
                        temp = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 41).ToList();
                        foreach (var row in temp)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                            if (DB != null)
                            {
                                if (DB.DYEB_Colour_FK == _repOps.ops3_ComboSelectedValue)
                                {
                                    if (_first)
                                    {
                                        _first = false;
                                        _fromDate = (DateTime)row.TLDYET_Date;
                                    }
                                    trans.Add(row);
                                    _toDate = (DateTime)row.TLDYET_Date;
                                }
                            }
                        }
                    }

                    foreach (var row in trans)
                    {
                        DataSet21.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                        dtr.Pk = 1;
                        dtr.Date_FirstDyed = row.TLDYET_Date;
                        // dtr.Date_Rejected = (DateTime)row.TLDYET_RejectDate;

                        var DB = context.TLDYE_DyeBatch.Find(row.TLDYET_Batch_FK);
                        if (DB != null)
                        {
                            dtr.Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                            dtr.BatchNumber = DB.DYEB_BatchNo;

                            var DBA = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                            if (DBA != null)
                                dtr.Machine = context.TLADM_MachineDefinitions.Find(DBA.TLDYEA_MachCode_FK).MD_Description;


                            dtr.TotalKg = DB.DYEB_BatchKG;
                            dtr.Due_Date = (DateTime)DB.DYEB_RequiredDate;
                            dtr.Pieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).Count();

                            if (DB.DYEB_DyeOrder_FK > 0)
                            {
                                var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    dtr.Quality = context.TLADM_Griege.Find(DO.TLDYO_Greige_FK).TLGreige_Description;
                                }
                            }


                            var NC = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_BatchNo_Fk == DB.DYEB_OriginalBatch_FK && x.DYENCRD_FR).FirstOrDefault();
                            if (NC != null)
                            {
                                dtr.Fault_Code = context.TLADM_DyeQDCodes.Find(NC.DYENCRD_Code_FK).QDF_Description;
                            }

                            var NCR = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_BatchNo_Fk == DB.DYEB_OriginalBatch_FK && !x.DYENCRD_FR).FirstOrDefault();
                            if (NCR != null)
                            {
                                dtr.Remedy_Code = context.TLADM_DyeRemendyCodes.Find(NCR.DYENCRD_Code_FK).QRC_Description;
                            }
                        }

                        dataTable2.AddDataTable2Row(dtr);
                    }

                    DataSet21.DataTable1Row tr = dataTable1.NewDataTable1Row();
                    tr.Pk = 1;
                    tr.FromDate = _fromDate;
                    tr.ToDate = _toDate;
                    tr.ReportTitle = "List of reprocessed Dye batches";
                    dataTable1.AddDataTable1Row(tr);

                }


                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                ReprocessedDyeBatch rdb = new ReprocessedDyeBatch();
                rdb.SetDataSource(ds);

                if (_repOps.DO_OptionSelected == 0)
                {
                    try
                    {
                        //"Batch Number
                        rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[1];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (_repOps.DO_OptionSelected == 1)
                {
                    // Dye Machine
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[3];

                }
                else if (_repOps.DO_OptionSelected == 2)
                {
                    // Fault Code
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[5];

                }
                else if (_repOps.DO_OptionSelected == 3)
                {
                    //Remedy Code
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[6];

                }
                else if (_repOps.DO_OptionSelected == 4)
                {
                    // Quality
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[8];


                }
                else if (_repOps.DO_OptionSelected == 5)
                {
                    //"Colour
                    rdb.DataDefinition.Groups[0].ConditionField = rdb.Database.Tables[1].Fields[10];

                }

                crystalReportViewer1.ReportSource = rdb;

            }
            else if (_RepNo == 22)  //Fabric Stock on Hand.  
            {
                DataSet ds = new DataSet();
                DataSet23.DataTable1DataTable dataTable1 = new DataSet23.DataTable1DataTable();
                DataSet23.DataTable2DataTable dataTable2 = new DataSet23.DataTable2DataTable();
                List<TLDYE_DyeBatchDetails> DBBatches = new List<TLDYE_DyeBatchDetails>();
                IList<TLADM_CustomerFile> Customers;
                IList<TLADM_Griege> Qualitys;
                IList<TLADM_Styles> Styles;
                IList<TLADM_Colours> Colours;
                IList<TLADM_WhseStore> WhseStores;

                using (var context = new TTI2Entities())
                {
                    Customers = context.TLADM_CustomerFile.ToList();
                    Styles = context.TLADM_Styles.ToList();
                    Qualitys = context.TLADM_Griege.ToList();
                    Colours = context.TLADM_Colours.ToList();
                    WhseStores = context.TLADM_WhseStore.ToList();

                    DBBatches = (from T2 in context.TLDYE_DyeBatch
                                 join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                 where T2.DYEB_OutProcess && !T3.DYEBO_WriteOff && !T3.DYEBO_CutSheet && !T3.DYEBO_Sold
                                 && T3.DYEBO_CurrentStore_FK == _repOps.FabricStoreSelected
                                 select T3).ToList();

                    if (_repOps.FabPendingNotSoldNotDelivered)
                    {
                        DBBatches = (from T1 in context.TLDYE_DyeBatch
                                     join T2 in context.TLDYE_DyeBatchDetails
                                     on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                     where T1.DYEB_FabicSales && T2.DYEBO_PendingDelivery && !T2.DYEBO_SaleConfirmed && !T2.DYEBO_Sold
                                      && T2.DYEBO_CurrentStore_FK == _repOps.FabricStoreSelected
                                     select T2).ToList();
                    }
                    else if (_repOps.FabPendingSoldNotDelivered)
                    {
                        DBBatches = (from T1 in context.TLDYE_DyeBatch
                                     join T2 in context.TLDYE_DyeBatchDetails
                                     on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                     where T1.DYEB_FabicSales && T2.DYEBO_SaleConfirmed && !T2.DYEBO_Sold
                                     && T2.DYEBO_CurrentStore_FK == _repOps.FabricStoreSelected
                                     select T2).ToList();
                    }
                    //---------------------------------------------------------------------
                    // We have the concept of bought in Fabric 
                    // This just filters out Bought in or not Bought in based on User Selection
                    //---------------------------------------------------------------------------
                    if (_repOps.FabricType) // ie Standard Stuff
                    {
                        DBBatches = (from T1 in DBBatches
                                     join T2 in context.TLKNI_GreigeProduction
                                     on T1.DYEBD_GreigeProduction_FK equals T2.GreigeP_Pk
                                     where !T2.GreigeP_BoughtIn
                                     select T1).ToList();
                    }
                    else
                    {
                        DBBatches = (from T1 in DBBatches
                                     join T2 in context.TLKNI_GreigeProduction
                                     on T1.DYEBD_GreigeProduction_FK equals T2.GreigeP_Pk
                                     where T2.GreigeP_BoughtIn
                                     select T1).ToList();
                    }

                    //=================================================================
                    // First Group into Store
                    // This is due to the likelyhood that one particlar batch may have pieces that are rejected as an example
                    //==========================================
                    var GroupedByStore = DBBatches.GroupBy(x => _repOps.FabricStoreSelected).ToList();
                    foreach (var StoreGroup in GroupedByStore)
                    {
                        // With in Store group into DyeBatch 
                        //=============================================================
                        var DBatchesGrouped = StoreGroup.GroupBy(x => x.DYEBD_DyeBatch_FK).Select(x => x.FirstOrDefault());
                        foreach (var DBatchGroup in DBatchesGrouped)
                        {
                            var DBBatch = context.TLDYE_DyeBatch.Where(x => x.DYEB_Pk == DBatchGroup.DYEBD_DyeBatch_FK).FirstOrDefault();
                            if (DBBatch != null)
                            {
                                DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Pk = 1;
                                var DyeBatch_Pk = DBatchGroup.DYEBD_DyeBatch_FK;

                                nr.BatchNumber = DBBatch.DYEB_BatchNo;
                                if (_repOps.FabricRejectStore)
                                    if (DBatchGroup.DYEBO_RejectedDate != null)
                                        nr.DateInToStore = (DateTime)DBatchGroup.DYEBO_RejectedDate;
                                    else
                                        nr.DateInToStore = (DateTime)DBBatch.DYEB_OutProcessDate;
                                else
                                {
                                    if (_repOps.FabricQSStore && DBBatch.DYEB_OutProcessDate != null)
                                        nr.DateInToStore = (DateTime)DBBatch.DYEB_OutProcessDate;
                                    else
                                        if (DBatchGroup.DYEBO_QAApproved && DBatchGroup.DYEBO_ApprovalDate != null)
                                        nr.DateInToStore = (DateTime)DBatchGroup.DYEBO_ApprovalDate;
                                }

                                if (!DBBatch.DYEB_CommissinCust)
                                {
                                    if (!DBBatch.DYEB_FabicSales)
                                    {
                                        var DO = context.TLDYE_DyeOrder.Where(x => x.TLDYO_Pk == DBBatch.DYEB_DyeOrder_FK).FirstOrDefault();
                                        if (DO != null)
                                        {
                                            nr.Customer = Customers.Where(x => x.Cust_Pk == DO.TLDYO_Customer_FK).FirstOrDefault().Cust_Description;
                                            nr.Quality = Qualitys.FirstOrDefault(x => x.TLGreige_Id == DO.TLDYO_Greige_FK).TLGreige_Description;
                                            nr.Style = Styles.FirstOrDefault(x => x.Sty_Id == DO.TLDYO_Style_FK).Sty_Description;
                                            nr.Colour = Colours.FirstOrDefault(x => x.Col_Id == DBBatch.DYEB_Colour_FK).Col_Display;
                                        }
                                    }
                                    else
                                    {
                                        var DO = context.TLDYE_DyeOrderFabric.Find(DBBatch.DYEB_DyeOrder_FK);
                                        if (DO != null)
                                        {
                                            nr.Customer = Customers.Where(x => x.Cust_Pk == DO.TLDYEF_Customer_FK).FirstOrDefault().Cust_Description;
                                            nr.Quality = Qualitys.FirstOrDefault(x => x.TLGreige_Id == DO.TLDYEF_Greige_FK).TLGreige_Description;
                                            nr.Colour = Colours.FirstOrDefault(x => x.Col_Id == DO.TLDYEF_Colours_FK).Col_Description;
                                            nr.Style = string.Empty;
                                        }
                                    }
                                }
                                else
                                {
                                    nr.Customer = Customers.Where(x => x.Cust_Pk == DBBatch.DYEB_Customer_FK).FirstOrDefault().Cust_Description;
                                    nr.Quality = Qualitys.FirstOrDefault(x => x.TLGreige_Id == DBBatch.DYEB_Greige_FK).TLGreige_Description;
                                    nr.Style = string.Empty;
                                }

                                if (!_repOps.ShowIndvidualNo)
                                {
                                    nr.BodyKg = DBBatches.Where(x => x.DYEBD_DyeBatch_FK == DBBatch.DYEB_Pk && x.DYEBD_BodyTrim && x.DYEBO_CurrentStore_FK == DBatchGroup.DYEBO_CurrentStore_FK).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                                    nr.TrimKg = DBBatches.Where(x => x.DYEBD_DyeBatch_FK == DBBatch.DYEB_Pk && !x.DYEBD_BodyTrim && x.DYEBO_CurrentStore_FK == DBatchGroup.DYEBO_CurrentStore_FK).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                                    nr.Pieces = DBBatches.Where(x => x.DYEBD_DyeBatch_FK == DBBatch.DYEB_Pk && x.DYEBO_CurrentStore_FK == DBatchGroup.DYEBO_CurrentStore_FK).Count();
                                    var WStore = WhseStores.FirstOrDefault(x => x.WhStore_Id == DBatchGroup.DYEBO_CurrentStore_FK);
                                    if (WStore != null)
                                        nr.CurrentStore = WStore.WhStore_Description;

                                    dataTable1.AddDataTable1Row(nr);
                                }
                                else
                                {

                                }
                            }
                        }
                    }


                    DataSet23.DataTable2Row rw = dataTable2.NewDataTable2Row();
                    rw.Pk = 1;
                    if (_repOps.FabricStore)
                        rw.Title = "Fabric Store";
                    else if (_repOps.FabricRejectStore)
                        rw.Title = "Reject Fabric store";
                    else if (_repOps.FabricQSStore)
                        rw.Title = "Quarantine Store";


                    dataTable2.AddDataTable2Row(rw);
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet23.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "No records matching selection made";
                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                if (!_repOps.ShowIndvidualNo)
                {
                    FabricSOH fsoh = new FabricSOH();
                    fsoh.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = fsoh;
                }
                else
                {
                    IndFabricSOH fsoh = new IndFabricSOH();
                    fsoh.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = fsoh;
                }
            }

            else if (_RepNo == 23)  //Dye and Chemical Consumption    
            {
                DataSet ds = new DataSet();
                DataSet24.DataTable1DataTable dataTable1 = new DataSet24.DataTable1DataTable();
                DataSet24.DataTable2DataTable dataTable2 = new DataSet24.DataTable2DataTable();
                IList<TLDYE_DyeBatch> DyeBatches;
                IList<TLADM_ConsumablesDC> _Consumables = null;
                IList<TLADM_Colours> _Colours = null;


                DataTable dt = new DataTable();
                dt.Columns.Add("Index", typeof(int));                // 0
                dt.Columns.Add("Receipe", typeof(string));           // 1
                dt.Columns.Add("Ingrediant", typeof(string));        // 2 
                dt.Columns.Add("Colour", typeof(string));            // 3 
                dt.Columns.Add("BatchWeight", typeof(decimal));      // 4 
                dt.Columns.Add("StdQty", typeof(decimal));           // 5   
                dt.Columns.Add("Reprocess", typeof(decimal));        // 6
                dt.Columns.Add("Total", typeof(decimal));            // 7 
                dt.Columns.Add("StdCost", typeof(decimal));          // 8
                dt.Columns.Add("TotalCost", typeof(decimal));        // 9

                using (var context = new TTI2Entities())
                {
                    _Consumables = context.TLADM_ConsumablesDC.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate).OrderBy(x => x.DYEB_BatchNo).ToList();
                    foreach (var DyeBatch in DyeBatches)
                    {
                        var Greige = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK);
                        if (Greige != null)
                        {
                            var GQualities = context.TLADM_GreigeQuality.Find(Greige.TLGreige_Quality_FK);
                            if (GQualities != null)
                            {
                                var RQualities = context.TLDYE_ReceipeGreigeQual.Where(x => x.TLGQ_GreigeQuality_FK == GQualities.GQ_Pk).ToList();
                                if (RQualities.Count != 0)
                                {
                                    var FirstTime = (from T1 in context.TLDYE_DyeBatch
                                                     join T2 in context.TLDYE_DyeBatchDetails
                                                     on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                                     where !T1.DYEB_Reprocess && T2.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk
                                                     select T2).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;

                                    var ReProcess = (from T1 in context.TLDYE_DyeBatch
                                                     join T2 in context.TLDYE_DyeBatchDetails
                                                     on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                                     where T1.DYEB_Reprocess && T2.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk
                                                     select T2).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                    if (!_repOps.NonReceipeOnly)
                                    {
                                        foreach (var Quality in RQualities)
                                        {
                                            //1st thing find the colour in any of the Dye Batches 
                                            //==========================================================
                                            var Definition = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_DefinePk == Quality.TLGQ_ReceipeDef_FK && x.TLDYE_ColorChart_FK == DyeBatch.DYEB_Colour_FK).FirstOrDefault();

                                            if (Definition != null)
                                            {
                                                var DefinitionDetails = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == Definition.TLDYE_DefinePk).ToList();
                                                foreach (var DefDetail in DefinitionDetails)
                                                {
                                                    var Consumable = _Consumables.FirstOrDefault(s => s.ConsDC_Pk == DefDetail.TLDYED_Cosumables_FK);

                                                    DataSet24.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                                    nr.Pk = 1;
                                                    nr.Item = Definition.TLDYE_DefineDescription;
                                                    nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeBatch.DYEB_Colour_FK).Col_Display;
                                                    nr.BatchWeight = FirstTime + ReProcess;
                                                    nr.BatchNo = DyeBatch.DYEB_BatchNo;
                                                    nr.Quality = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                                                    nr.ReceipeDescription = Definition.TLDYE_DefineDescription;
                                                    nr.ItemDescription = Consumable.ConsDC_Description;

                                                    if (!DefDetail.TLDYED_LiqCalc)
                                                    {
                                                        var Kgs = (DefDetail.TLDYED_MELFC / 100) * FirstTime;
                                                        nr.StdQtyKG = Kgs;

                                                        Kgs = (DefDetail.TLDYED_MELFC / 100) * ReProcess;
                                                        nr.ReprocessKG = Kgs;
                                                    }
                                                    else
                                                    {
                                                        var Kgs = (DefDetail.TLDYED_MELFC / 1000) * FirstTime * DefDetail.TLDYED_LiqRatio;
                                                        nr.StdQtyKG = Kgs;

                                                        Kgs = (DefDetail.TLDYED_MELFC / 1000) * ReProcess * DefDetail.TLDYED_LiqRatio;
                                                        nr.ReprocessKG = Kgs;
                                                    }

                                                    nr.TotalKG = nr.StdQtyKG + nr.ReprocessKG;
                                                    nr.StandardCost = Consumable.ConsDC_StandardCost;
                                                    nr.TotalCost = nr.TotalKG * Consumable.ConsDC_StandardCost;

                                                    dataTable2.AddDataTable2Row(nr);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        foreach (var Quality in RQualities)
                                        {
                                            //1st thing find the colour in any of the Dye Batches 
                                            //==========================================================
                                            var Definition = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_DefinePk == Quality.TLGQ_ReceipeDef_FK && x.TLDYE_ColorChart_FK == DyeBatch.DYEB_Colour_FK).FirstOrDefault();

                                            if (Definition == null)
                                            {
                                                DataSet24.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                                nr.Pk = 1;
                                                nr.Item = "Receipe Not Found";
                                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeBatch.DYEB_Colour_FK).Col_Display;
                                                nr.BatchWeight = FirstTime + ReProcess;
                                                nr.BatchNo = DyeBatch.DYEB_BatchNo;
                                                nr.Quality = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                                                nr.ReceipeDescription = "Receipe Not Found";
                                                nr.ItemDescription = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;

                                                dataTable2.AddDataTable2Row(nr);

                                                break;
                                            }
                                        }

                                    }
                                }
                            }

                        }
                    }
                    DataSet24.DataTable1Row xrow = dataTable1.NewDataTable1Row();
                    xrow.Pk = 1;
                    xrow.FromDate = _repOps.fromDate;
                    xrow.ToDate = _repOps.toDate;
                    dataTable1.AddDataTable1Row(xrow);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                DyeAndChemicalCons fsoh = new DyeAndChemicalCons();
                fsoh.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fsoh;
            }
            else if (_RepNo == 24)  //Process Loss    
            {
                DataSet ds = new DataSet();
                DataSet25.DataTable1DataTable dataTable1 = new DataSet25.DataTable1DataTable();
                DataSet25.DataTable2DataTable dataTable2 = new DataSet25.DataTable2DataTable();
                IList<TLADM_Griege> _Quality = null;
                IList<TLADM_FabricProduct> _FabricProduct = null;
                IList<TLADM_MachineDefinitions> _Machines = null;
                IList<TLADM_Colours> _Colours = null;
                IList<TLDYE_DyeBatchAllocated> _Allocations = null;

                using (var context = new TTI2Entities())
                {
                    _Quality = context.TLADM_Griege.ToList();
                    _FabricProduct = context.TLADM_FabricProduct.ToList();
                    _Machines = context.TLADM_MachineDefinitions.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    _Allocations = context.TLDYE_DyeBatchAllocated.ToList();

                    var Batch = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate).ToList();
                    foreach (var row in Batch)
                    {
                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).ToList();
                        if (Existing != null)
                        {
                            DataSet25.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.Pk = 1;
                            nr.BatchNumber = row.DYEB_BatchNo;
                            nr.Date = (DateTime)row.DYEB_OutProcessDate;
                            var Quality = _Quality.FirstOrDefault(s => s.TLGreige_Id == row.DYEB_Greige_FK);
                            if (Quality != null)
                            {
                                if (Quality.TLGreige_IsBoughtIn)
                                {
                                    continue;
                                }
                                nr.Quality = Quality.TLGreige_Description;
                            }

                            nr.GrossKg = Existing.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                            nr.Quality = _Quality.FirstOrDefault(s => s.TLGreige_Id == row.DYEB_Greige_FK && !s.TLGreige_IsBoughtIn).TLGreige_Description;
                            nr.GrossKg = Existing.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                            nr.NettKg = Existing.Sum(x => (decimal?)x.DYEBO_Nett ?? 0.00M);
                            nr.ColourCode = _Colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK).Col_Display;
                            var MachineAllocated = _Allocations.FirstOrDefault(s => s.TLDYEA_DyeBatch_FK == row.DYEB_Pk);
                            if (MachineAllocated != null)
                            {
                                var Machine = _Machines.FirstOrDefault(s => s.MD_Pk == MachineAllocated.TLDYEA_MachCode_FK);
                                if (Machine != null)
                                {
                                    nr.Shade = _FabricProduct.FirstOrDefault(s => s.FP_Id == Machine.MD_FabricType_FK).FP_Description;
                                    nr.Machine = Machine.MD_AlternateDesc;
                                }
                            }
                            if (nr.GrossKg != 0 && nr.NettKg != 0)
                            {
                                var ProcessLoss = nr.GrossKg - nr.NettKg;
                                var Loss = 100 * (ProcessLoss / nr.GrossKg);
                                if (Loss == 0.0M)
                                {
                                    continue;
                                }
                                if (_repOps.ExceptionOnly)
                                {
                                    if (Loss <= _repOps.Percentage_Exception)
                                    {
                                        continue;
                                    }
                                }

                                nr.ProcessLoss = ProcessLoss;
                                nr.Loss = Loss;
                            }
                            else
                            {
                                nr.ProcessLoss = 0;
                                nr.Loss = 0;
                            }


                            dataTable2.AddDataTable2Row(nr);
                        }
                    }

                    DataSet25.DataTable1Row xrow = dataTable1.NewDataTable1Row();
                    xrow.Pk = 1;
                    xrow.Title = "Dyeing process loss for the period";
                    xrow.FromDate = _repOps.fromDate;
                    xrow.ToDate = _repOps.toDate;
                    dataTable1.AddDataTable1Row(xrow);

                    ds.Tables.Add(dataTable1);
                    ds.Tables.Add(dataTable2);
                    DyeingProcessLoss fsoh = new DyeingProcessLoss();
                    fsoh.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = fsoh;
                }

            }
            else if (_RepNo == 25)  //Stock Take Sheet     
            {
                DataSet ds = new DataSet();
                DataSet26.DataTable1DataTable dataTable1 = new DataSet26.DataTable1DataTable();
                IList<TLADM_ConsumablesDC> consTable = new List<TLADM_ConsumablesDC>();

                using (var context = new TTI2Entities())
                {
                    if (_repOps.stkWhseSelected)
                    {
                        consTable = context.TLADM_ConsumablesDC.Where(x => x.ConsDC_StoreCode_FK == _repOps.stkWhseIndex).OrderBy(x => x.ConsDC_Code).ToList();

                    }
                    else if (_repOps.stkTypeSelected)
                    {
                        consTable = context.TLADM_ConsumablesDC.Where(x => x.ConsDC_StockType_FK == _repOps.stkStockTypeIndex).OrderBy(x => x.ConsDC_Code).ToList();
                    }
                    else
                    {
                        consTable = context.TLADM_ConsumablesDC.Where(x => x.ConsDC_StockTake_FK == _repOps.stkTakeCategory).OrderBy(x => x.ConsDC_Code).ToList();
                    }

                    var WhseStore = context.TLADM_WhseStore.Where(x => x.WhStore_DyeKitchen || x.WhStore_ChemicalStore || x.WhStore_Quarantine).ToList();
                    foreach (var Whse in WhseStore)
                    {
                        foreach (var row in consTable)
                        {
                            DataSet26.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.ItemCode = row.ConsDC_Code;
                            nr.ItemDescription = row.ConsDC_Description;
                            nr.Store = Whse.WhStore_Description;

                            dataTable1.AddDataTable1Row(nr);
                        }
                    }
                }
                ds.Tables.Add(dataTable1);
                StockTakeSheet stk = new StockTakeSheet();
                stk.SetDataSource(ds);
                crystalReportViewer1.ReportSource = stk;
            }
            else if (_RepNo == 26)  //QADC Codes     
            {
                DataSet ds = new DataSet();
                DataSet27.TLADM_DyeQDCodesDataTable dataTable = new DataSet27.TLADM_DyeQDCodesDataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Code).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet27.TLADM_DyeQDCodesRow nr = dataTable.NewTLADM_DyeQDCodesRow();
                        nr.QDF_Code = row.QDF_Code;
                        nr.QDF_Description = row.QDF_Description;
                        nr.QDF_NCRRequired = row.QDF_NCRRequired;
                        nr.QDF_Pk = row.QDF_Pk;

                        dataTable.AddTLADM_DyeQDCodesRow(nr);
                    }
                }

                ds.Tables.Add(dataTable);
                QDCCodes dcCodes = new QDCCodes();
                dcCodes.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dcCodes;

            }
            else if (_RepNo == 27)  //QA Remedy Codes     
            {
                DataSet ds = new DataSet();
                DataSet28.TLADM_DyeRemendyCodesDataTable dataTable = new DataSet28.TLADM_DyeRemendyCodesDataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLADM_DyeRemendyCodes.OrderBy(x => x.QRC_Code).ToList();
                    foreach (var row in Existing)
                    {
                        DataSet28.TLADM_DyeRemendyCodesRow nr = dataTable.NewTLADM_DyeRemendyCodesRow();
                        nr.QRC_Code = row.QRC_Code;
                        nr.QRC_Description = row.QRC_Description;
                        nr.QRC_AdditionalResources = row.QRC_AdditionalResources;
                        nr.QRC_Pk = row.QRC_Pk;

                        dataTable.AddTLADM_DyeRemendyCodesRow(nr);
                    }
                }

                ds.Tables.Add(dataTable);
                QDCRemedyCodes dcCodes = new QDCRemedyCodes();
                dcCodes.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dcCodes;

            }
            else if (_RepNo == 28)  //Shade Results straight after dyeing     
            {
                DataSet ds = new DataSet();
                DataSet29.DataTable1DataTable dataTable1 = new DataSet29.DataTable1DataTable();
                DataSet29.DataTable2DataTable dataTable2 = new DataSet29.DataTable2DataTable();

                IList<TLDYE_DyeTransactions> DyeTrans = new List<TLDYE_DyeTransactions>();
                TLDYE_DyeBatch DyeBatches = new TLDYE_DyeBatch();

                TLDYE_NonCompliance nonC = new TLDYE_NonCompliance();
                TLDYE_NonComplianceDetail nonCD = new TLDYE_NonComplianceDetail();

                using (var context = new TTI2Entities())
                {
                    DataSet29.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.NCRPk = 1;
                    nr.NCRFromDate = _parms.FromDate;
                    nr.NCRToDate = _parms.ToDate;
                    nr.NCReportTitle = "Report on Colour results after dyeing for batches dyed from ";
                    if (_parms.RejectedBatches)
                        nr.NCRSelection = "All rejected batches";
                    else
                        nr.NCRSelection = "All batches";

                    if (_parms.DO_OptionSelected == 0)
                        nr.NCRSorted = "Grouped By Dye Batch";
                    else if (_parms.DO_OptionSelected == 1)
                        nr.NCRSorted = "Grouped By Quality";
                    else if (_parms.DO_OptionSelected == 2)
                        nr.NCRSorted = "Grouped By Machine";
                    else if (_parms.DO_OptionSelected == 3)
                        nr.NCRSorted = "Grouped By Operator";//"Operator
                    else if (_parms.DO_OptionSelected == 4)
                        nr.NCRSorted = "Grouped By Fault";// Fault
                    else if (_parms.DO_OptionSelected == 5)
                        nr.NCRSorted = "Grouped By Remedy";// Remedy
                    else if (_parms.DO_OptionSelected == 6)
                        nr.NCRSorted = "Grouped By Colour";// Colour
                    else if (_parms.DO_OptionSelected == 7)
                        nr.NCRSorted = "Grouped By Result";// result

                    dataTable1.AddDataTable1Row(nr);
                    DyeTrans = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Date >= _parms.FromDate && x.TLDYET_Date <= _parms.ToDate && x.TLDYET_Stage == 3).ToList();
                    foreach (var Record in DyeTrans)
                    {
                        if (_parms.RejectedBatches)
                        {
                            //Only reject batches selected therefore OK batches Not Required
                            //---------------------------------------------------------------
                            if (!Record.TLDYET_Rejected)
                                continue;
                        }

                        DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_Pk == Record.TLDYET_Batch_FK).FirstOrDefault();
                        if (DyeBatches != null)
                        {
                            if (_parms.Qualities.Count != 0)
                            {
                                var value = _parms.Qualities.Find(x => x.TLGreige_Id == DyeBatches.DYEB_Greige_FK);
                                if (value == null)
                                    continue;
                            }

                            if (_parms.Colours.Count != 0)
                            {
                                var value = _parms.Colours.Find(x => x.Col_Id == DyeBatches.DYEB_Colour_FK);
                                if (value == null)
                                    continue;
                            }

                            if (_parms.Machines.Count != 0)
                            {
                                var DBA = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatches.DYEB_Pk).FirstOrDefault();
                                if (DBA != null)
                                {
                                    var value = _parms.Machines.Find(x => x.MD_Pk == DBA.TLDYEA_MachCode_FK);
                                    if (value != null)
                                        continue;
                                }
                            }

                            if (_parms.Operators.Count != 0)
                            {
                                var Operator = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DyeBatches.DYEB_Pk).FirstOrDefault();
                                if (Operator != null)
                                {
                                    var value = _parms.Operators.Find(x => x.MachOp_Pk == Operator.DYEOP_Pk);
                                    if (value != null)
                                        continue;
                                }
                            }

                            DataSet29.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                            xnr.NCRPk = 1;

                            xnr.NCRBatchNo = DyeBatches.DYEB_BatchNo;
                            xnr.NCRKg = DyeBatches.DYEB_BatchKG;
                            xnr.NCRNoOfPieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatches.DYEB_Pk).Count();
                            xnr.NCRQuality = context.TLADM_Griege.Find(DyeBatches.DYEB_Greige_FK).TLGreige_Description;
                            xnr.NCRColour = context.TLADM_Colours.Find(DyeBatches.DYEB_Colour_FK).Col_Display;
                            var OperatorAlloc = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DyeBatches.DYEB_Pk).FirstOrDefault();
                            if (OperatorAlloc != null)
                            {
                                var Op = context.TLADM_MachineOperators.Find(OperatorAlloc.DYEOP_Operator_FK);
                                if (Op != null)
                                {
                                    xnr.NCROperator = Op.MachOp_Description;
                                }
                            }
                            var MachineAlloc = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatches.DYEB_Pk).FirstOrDefault();
                            if (MachineAlloc != null)
                            {
                                xnr.NCRMachine = context.TLADM_MachineDefinitions.Find(MachineAlloc.TLDYEA_MachCode_FK).MD_Description;
                            }
                            if (!Record.TLDYET_Rejected)
                            {
                                xnr.NCRResult = "Not Rejected";
                            }
                            else
                            {
                                xnr.NCRResult = "Rejected";
                            }

                            nonC = context.TLDYE_NonCompliance.Where(x => x.TLDYE_NcrBatchNo_FK == DyeBatches.DYEB_Pk && x.TLDYE_NCStage == 1).FirstOrDefault();
                            if (nonC != null)
                            {
                                nonCD = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == nonC.TLDYE_NcrNumber && x.DYENCRD_FR).FirstOrDefault();
                                if (nonCD != null)
                                {
                                    if (_parms.ops3_ComboSelected == 5)
                                    {
                                        if (_parms.ops3_ComboSelectedValue != nonCD.DYENCRD_Code_FK)
                                            continue;
                                    }
                                    xnr.NCRNumber = nonCD.DYENCRD_ComNumber;
                                    xnr.NCRFault = context.TLADM_DyeQDCodes.Find(nonCD.DYENCRD_Code_FK).QDF_Description;

                                }
                                nonCD = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == nonC.TLDYE_NcrNumber && !x.DYENCRD_FR).FirstOrDefault();
                                if (nonCD != null)
                                {
                                    xnr.NCRRemedy = context.TLADM_DyeRemendyCodes.Find(nonCD.DYENCRD_Code_FK).QRC_Description;
                                }
                            }
                            dataTable2.AddDataTable2Row(xnr);
                        }
                    }
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ShadeAfterDyeing sad = new ShadeAfterDyeing();
                sad.SetDataSource(ds);
                crystalReportViewer1.ReportSource = sad;

                if (_parms.DO_OptionSelected == 1)
                {
                    try
                    {
                        //"Quality
                        sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[3];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (_parms.DO_OptionSelected == 2)
                {
                    // Machine
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[4];

                }
                else if (_parms.DO_OptionSelected == 3)
                {
                    //"Operator
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[5];

                }
                else if (_parms.DO_OptionSelected == 4)
                {
                    // Fault
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[6];

                }
                else if (_parms.DO_OptionSelected == 5)
                {
                    // Remedy
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[7];

                }
                else if (_parms.DO_OptionSelected == 6)
                {
                    // Colour
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[10];

                }
                else if (_parms.DO_OptionSelected == 7)
                {
                    // result
                    sad.DataDefinition.Groups[0].ConditionField = sad.Database.Tables[1].Fields[11];

                }

            }
            else if (_RepNo == 29)  //NCR Results details     
            {
                DataSet ds = new DataSet();
                DataSet30.DataTable1DataTable dataTable1 = new DataSet30.DataTable1DataTable();

                IList<TLDYE_NonCompliance> nonC = new List<TLDYE_NonCompliance>();
                IList<TLDYE_NonComplianceDetail> nonCD = new List<TLDYE_NonComplianceDetail>();

                using (var context = new TTI2Entities())
                {
                    if (_repOps.ops3_ComboSelected == 1)
                    {
                        nonC = context.TLDYE_NonCompliance.Where(x => x.TLDYE_NcrPk == _repOps.ops3_ComboSelectedValue).ToList();
                    }
                    else
                    {
                        nonC = context.TLDYE_NonCompliance.Where(x => x.TLDYE_ComplainceDate >= _repOps.fromDate && x.TLDYE_ComplainceDate <= _repOps.toDate).ToList();
                    }
                    foreach (var row in nonC)
                    {
                        /*
                        if (oCmbo.Name == "cmboNCRNumber")
                        {
                            repOps.ops3_ComboSelected = 1;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboProduct")
                        {
                            repOps.ops3_ComboSelected = 2;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboColour")
                        {
                            repOps.ops3_ComboSelected = 3;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboDyeMachine")
                        {
                            repOps.ops3_ComboSelected = 4;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboDyeOperator")
                        {
                            repOps.ops3_ComboSelected = 5;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboCause")
                        {
                            repOps.ops3_ComboSelected = 6;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                        else if (oCmbo.Name == "cmboRemedy")
                        {
                            repOps.ops3_ComboSelected = 7;
                            repOps.ops3_ComboSelectedValue = (int)oCmbo.SelectedValue;
                        }
                         
                        
                        if (_repOps.ops3_ComboSelected == 4)
                        {
                            if (_repOps.ops3_ComboSelectedValue != row.TLDYE_Machine_FK)
                                continue;
                        }
                        
                        if (_repOps.ops3_ComboSelected == 5)
                        {
                            if (_repOps.ops3_ComboSelectedValue != row.TLDYE_Operator_FK)
                                continue;
                        }
                        */

                        nonCD = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == row.TLDYE_NcrNumber).ToList();
                        foreach (var nonRecord in nonCD)
                        {
                            if (_repOps.ops3_ComboSelected == 6)
                                if (_repOps.ops3_ComboSelectedValue != nonRecord.DYENCRD_Code_FK)
                                    continue;

                            DataSet30.DataTable1Row nr = dataTable1.NewDataTable1Row();

                            nr.NCRBatchNo = context.TLDYE_DyeBatch.Find(row.TLDYE_NcrBatchNo_FK).DYEB_BatchNo;
                            nr.NCRDate = row.TLDYE_ComplainceDate;
                            nr.NCROperator = context.TLADM_MachineOperators.Find(row.TLDYE_Operator_FK).MachOp_Description;
                            nr.NCRNumber = row.TLDYE_NcrNumber;
                            nr.NCRMachine = context.TLADM_MachineDefinitions.Find(row.TLDYE_Machine_FK).MD_AlternateDesc;
                            nr.NCRStage = context.TLADM_QADyeProcess.Find(row.TLDYE_NCStage).QADYEP_Description;
                            nr.NCRFromDate = _repOps.fromDate;
                            nr.NCRToDate = _repOps.toDate;
                            if (nonRecord.DYENCRD_FR)
                            {
                                nr.NCRCode = context.TLADM_DyeQDCodes.Find(nonRecord.DYENCRD_Code_FK).QDF_Code;
                                nr.NCRCodeDescription = context.TLADM_DyeQDCodes.Find(nonRecord.DYENCRD_Code_FK).QDF_Description;
                            }
                            else
                            {
                                var NonRemedyCode = context.TLADM_DyeRemendyCodes.Find(nonRecord.DYENCRD_Code_FK);
                                if (NonRemedyCode != null)
                                {
                                    nr.NCRCode = NonRemedyCode.QRC_Code;
                                    nr.NCRCodeDescription = NonRemedyCode.QRC_Description;
                                }
                            }
                            dataTable1.AddDataTable1Row(nr);
                        }
                    }

                }
                ds.Tables.Add(dataTable1);
                NCRDetails vDetails = new NCRDetails();
                vDetails.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vDetails;
            }
            else if (_RepNo == 30)  //Document to confirm chemicals withdrawn     _PK'
            {
                DataSet ds = new DataSet();
                DataSet31.DataTable1DataTable dataTable1 = new DataSet31.DataTable1DataTable();
                DataSet31.DataTable2DataTable dataTable2 = new DataSet31.DataTable2DataTable();
                IList<TLADM_WhseStore> _WhseStore = null;
                IList<TLADM_ConsumablesDC> _Consumables = null;
                using (var context = new TTI2Entities())
                {
                    _WhseStore = context.TLADM_WhseStore.ToList();
                    _Consumables = context.TLADM_ConsumablesDC.ToList();

                    var soh = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_TransNumber == _repOps.LNU).ToList();
                    if (soh.Count != 0)
                    {
                        DataSet31.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.FromDepartment = _WhseStore.FirstOrDefault(s => s.WhStore_Id == _repOps.fromStore).WhStore_Description;
                        nr.ToDepartment = _WhseStore.FirstOrDefault(s => s.WhStore_Id == _repOps.toStore).WhStore_Description;
                        nr.TransDate = _repOps.fromDate;
                        nr.Document_Number = "DK" + _repOps.LNU.ToString().PadLeft(5, '0');

                        dataTable1.AddDataTable1Row(nr);

                        foreach (var row in soh)
                        {
                            DataSet31.DataTable2Row r2 = dataTable2.NewDataTable2Row();
                            r2.Pk = 1;
                            r2.ChemicalDescription = _Consumables.FirstOrDefault(s => s.ConsDC_Pk == row.DYCSH_Consumable_FK).ConsDC_Description;
                            r2.Quantity = row.DYCSH_StockOnHand;
                            r2.Code = _Consumables.FirstOrDefault(s => s.ConsDC_Pk == row.DYCSH_Consumable_FK).ConsDC_Code;

                            dataTable2.AddDataTable2Row(r2);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                ConsumablesWithDrawn conWithDrawl = new ConsumablesWithDrawn();
                conWithDrawl.SetDataSource(ds);
                crystalReportViewer1.ReportSource = conWithDrawl;

            }
            else if (_RepNo == 31)  //Dye Production Report _repOps
            {

                DataSet ds = new DataSet();
                DataSet32.DataTable1DataTable dataTable1 = new DataSet32.DataTable1DataTable();
                DataSet32.DataTable2DataTable dataTable2 = new DataSet32.DataTable2DataTable();
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_Griege> _Greige = null;
                IList<TLADM_MachineOperators> _Operators = null;
                IList<TLADM_MachineDefinitions> _Machines = null;
                IList<TLDYE_DyeBatch> DyeBatch = new List<TLDYE_DyeBatch>();
                string CustomerDet = string.Empty;

                using (var context = new TTI2Entities())
                {
                    _Colours = context.TLADM_Colours.ToList();
                    _Greige = context.TLADM_Griege.ToList();
                    _Operators = context.TLADM_MachineOperators.ToList();
                    _Machines = context.TLADM_MachineDefinitions.ToList();

                    //================================================================
                    // for normalised production relating to colours 
                    // We need the colour that has been designated as the bench mark
                    //======================================================================
                    var ColourBenchMark = _Colours.Where(x => x.Col_Benchmark).FirstOrDefault();

                    DyeBatch = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate && x.DYEB_OutProcess).ToList();

                    if (_repOps.FabricToQ)
                    {
                        if (_repOps.FirstTime)
                            DyeBatch = DyeBatch.Where(x => x.DYEB_SequenceNo == 0).ToList();
                        else
                            DyeBatch = DyeBatch.Where(x => x.DYEB_SequenceNo != 0).ToList();

                        if (_repOps.DYEPCustNoSelected)
                        {
                            DyeBatch = DyeBatch.Where(x => x.DYEB_Customer_FK == _repOps.DYEPCustNo).ToList();
                            CustomerDet = context.TLADM_CustomerFile.Find(_repOps.DYEPCustNo).Cust_Description;
                        }
                    }
                    else
                    {
                        if (!_repOps.FabricNotFinished)
                        {
                            // Join with query expression.
                            //===========================================
                            var result = from t in context.TLDYE_DyeBatch
                                         join x in context.TLDYE_DyeBatchDetails
                                         on t.DYEB_Pk equals x.DYEBD_DyeBatch_FK
                                         where x.DYEBO_QAApproved && x.DYEBO_ApprovalDate >= _repOps.fromDate
                                         && x.DYEBO_ApprovalDate <= _repOps.toDate && t.DYEB_OutProcess
                                         select t;

                            DyeBatch = result.Distinct().ToList();

                            if (_repOps.DYEPCustNoSelected)
                            {
                                DyeBatch = DyeBatch.Where(x => x.DYEB_Customer_FK == _repOps.DYEPCustNo).ToList();
                                CustomerDet = context.TLADM_CustomerFile.Find(_repOps.DYEPCustNo).Cust_Description;
                            }
                        }
                        else
                        {
                            // join with a query expression 
                            //===========================================
                            if (_repOps.FabricNotFinished && !_repOps.MonthlyProduction)
                            {
                                var result = (from t in context.TLDYE_DyeBatch
                                              join x in context.TLDYE_DyeTransactions
                                              on t.DYEB_Pk equals x.TLDYET_Batch_FK
                                              where x.TLDYET_Date >= _repOps.fromDate
                                              && x.TLDYET_Date <= _repOps.toDate
                                              && t.DYEB_Stage1 && !t.DYEB_OutProcess && x.TLDYET_Stage == 3
                                              select new { t, x }).ToList();

                                if (_repOps.DYEPCustNoSelected)
                                {
                                    DyeBatch = DyeBatch.Where(x => x.DYEB_Customer_FK == _repOps.DYEPCustNo).ToList();
                                    CustomerDet = context.TLADM_CustomerFile.Find(_repOps.DYEPCustNo).Cust_Description;
                                }

                                foreach (var row in result)
                                {
                                    DataSet32.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                    nr.pk = 1;
                                    nr.BatchNumber = row.t.DYEB_BatchNo;
                                    nr.Date = (DateTime)row.x.TLDYET_Date;

                                    var ColourDyed = _Colours.FirstOrDefault(s => s.Col_Id == row.t.DYEB_Colour_FK);
                                    if (ColourDyed != null)
                                    {
                                        nr.Colour = ColourDyed.Col_Display;
                                    }

                                    var Qual = _Greige.FirstOrDefault(s => s.TLGreige_Id == row.t.DYEB_Greige_FK);
                                    if (Qual != null)
                                    {
                                        if (Qual.TLGreige_IsBoughtIn)
                                        {
                                            continue;
                                        }
                                        nr.Quality = Qual.TLGreige_Description;
                                    }
                                    var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == row.t.DYEB_Pk).FirstOrDefault();
                                    if (Allocated != null)
                                    {
                                        nr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == Allocated.TLDYEA_MachCode_FK).MD_Description;
                                        nr.Shade = context.TLADM_FabricProduct.Find(Allocated.TLDYEA_FabricType_FK).FP_Description;
                                    }

                                    var OperatorAlloc = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == row.t.DYEB_Pk).FirstOrDefault();
                                    if (OperatorAlloc != null)
                                    {
                                        nr.Operator = _Operators.FirstOrDefault(s => s.MachOp_Pk == OperatorAlloc.DYEOP_Operator_FK).MachOp_Description;

                                    }

                                    if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.t.DYEB_Pk).Count() != 0)
                                    {
                                        nr.Gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.t.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                                        if (ColourDyed.Col_StandardTime > 0)
                                        {
                                            var Ratio = ColourDyed.Col_StandardTime / ColourBenchMark.Col_StandardTime;
                                            nr.Normalised = nr.Gross * Ratio;
                                        }
                                        else
                                            nr.Normalised = 0.00M;
                                    }
                                    else
                                    {
                                        nr.Gross = 0.00M;
                                        nr.Nett = 0.00M;
                                        nr.Normalised = 0.00M;
                                    }
                                    dataTable2.AddDataTable2Row(nr);
                                }
                            }
                            else
                            {
                                var result = context.TLDYE_DyeBatch.Where(x => x.DYEB_Stage1 && x.DYEB_DateStage1 >= _repOps.fromDate && x.DYEB_DateStage1 <= _repOps.toDate).ToList();

                                foreach (var row in result)
                                {
                                    DataSet32.DataTable2Row nr = dataTable2.NewDataTable2Row();
                                    nr.pk = 1;
                                    nr.BatchNumber = row.DYEB_BatchNo;
                                    nr.Date = (DateTime)row.DYEB_DateStage1;

                                    var ColourDyed = _Colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK);
                                    if (ColourDyed != null)
                                    {
                                        nr.Colour = ColourDyed.Col_Display;
                                    }

                                    var Qual = _Greige.FirstOrDefault(s => s.TLGreige_Id == row.DYEB_Greige_FK);
                                    if (Qual != null)
                                    {
                                        if (Qual.TLGreige_IsBoughtIn)
                                        {
                                            continue;
                                        }
                                        nr.Quality = Qual.TLGreige_Description;
                                    }
                                    var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == row.DYEB_Pk).FirstOrDefault();
                                    if (Allocated != null)
                                    {
                                        nr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == Allocated.TLDYEA_MachCode_FK).MD_Description;
                                        nr.Shade = context.TLADM_FabricProduct.Find(Allocated.TLDYEA_FabricType_FK).FP_Description;
                                    }

                                    var OperatorAlloc = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == row.DYEB_Pk).FirstOrDefault();
                                    if (OperatorAlloc != null)
                                    {
                                        nr.Operator = _Operators.FirstOrDefault(s => s.MachOp_Pk == OperatorAlloc.DYEOP_Operator_FK).MachOp_Description;

                                    }

                                    if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count() != 0)
                                    {
                                        nr.Gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                                        if (ColourDyed.Col_StandardTime > 0)
                                        {
                                            var Ratio = ColourDyed.Col_StandardTime / ColourBenchMark.Col_StandardTime;
                                            nr.Normalised = nr.Gross * Ratio;
                                        }
                                        else
                                            nr.Normalised = 0.00M;
                                    }
                                    else
                                    {
                                        nr.Gross = 0.00M;
                                        nr.Nett = 0.00M;
                                        nr.Normalised = 0.00M;
                                    }
                                    dataTable2.AddDataTable2Row(nr);
                                }

                            }
                        }
                    }

                    if (!_repOps.FabricNotFinished && !_repOps.MonthlyProduction)
                    {
                        foreach (var row in DyeBatch)
                        {
                            DataSet32.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.pk = 1;
                            nr.BatchNumber = row.DYEB_BatchNo;
                            if (!_repOps.FabricNotFinished)
                                nr.Date = (DateTime)row.DYEB_OutProcessDate;
                            var ColourDyed = _Colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK);
                            nr.Colour = ColourDyed.Col_Display;
                            nr.Quality = context.TLADM_Griege.Find(row.DYEB_Greige_FK).TLGreige_Description;
                            var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == row.DYEB_Pk).FirstOrDefault();
                            if (Allocated != null)
                            {
                                nr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == Allocated.TLDYEA_MachCode_FK).MD_Description;
                                nr.Shade = context.TLADM_FabricProduct.Find(Allocated.TLDYEA_FabricType_FK).FP_Description;
                            }

                            var OperatorAlloc = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == row.DYEB_Pk).FirstOrDefault();
                            if (OperatorAlloc != null)
                            {
                                nr.Operator = _Operators.FirstOrDefault(s => s.MachOp_Pk == OperatorAlloc.DYEOP_Operator_FK).MachOp_Description;
                            }

                            if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count() != 0)
                            {
                                nr.Gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                                if (_repOps.FabricToQ || _repOps.FabricToStore && ColourDyed.Col_StandardTime > 0)
                                {
                                    nr.Nett = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBO_Nett ?? 0.00M);
                                    var Ratio = ColourDyed.Col_StandardTime / ColourBenchMark.Col_StandardTime;
                                    nr.Normalised = nr.Nett * Ratio;
                                }
                                else
                                {
                                    nr.Nett = 0.00M;
                                    nr.Normalised = 0.00M;
                                }
                            }
                            else
                            {
                                nr.Gross = 0.00M;
                                nr.Nett = 0.00M;
                                nr.Normalised = 0.00M;
                            }
                            dataTable2.AddDataTable2Row(nr);
                        }
                    }
                }

                DataSet32.DataTable1Row tr = dataTable1.NewDataTable1Row();
                tr.Pk = 1;
                tr.FromDate = _repOps.fromDate;
                tr.ToDate = _repOps.toDate;
                if (CustomerDet.Length == 0)
                {
                    tr.CustomerDetail = "All";
                }
                else
                {
                    tr.CustomerDetail = CustomerDet;
                }

                if (_repOps.FabricToQ)
                {
                    if (_repOps.FirstTime)
                        tr.Title = "First Time Fabric Production";
                    else
                        tr.Title = "Reprocessed Fabric Production";
                }
                else
                {
                    if (!_repOps.FabricNotFinished)
                        tr.Title = "Fabric Processed to Fabric Store";
                    else
                        tr.Title = "Fabric Dyed (Production Stage 1)";
                }

                dataTable1.AddDataTable1Row(tr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                DyeProduction conWithDrawl = new DyeProduction();
                if (_repOps.QASummary)
                {
                    conWithDrawl.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                }
                conWithDrawl.SetDataSource(ds);
                crystalReportViewer1.ReportSource = conWithDrawl;

            }
            else if (_RepNo == 32)  // Results straight after drying
                                    // Results straight after Hydro
            {
                DataSet ds = new DataSet();
                DataSet49.DataTable1DataTable dataTable1 = new DataSet49.DataTable1DataTable();
                DataSet49.DataTable2DataTable dataTable2 = new DataSet49.DataTable2DataTable();
                IList<TLADM_QADyeProcessFields> QAProcessItems = null;

                DataSet49.DataTable1Row nr = dataTable1.NewDataTable1Row();
                nr.Pk = 1;
                nr.FromDate = _parms.FromDate;
                nr.ToDate = _parms.ToDate;
                if (_parms.DyeStage == 4)
                {
                    nr.Title = "Measurements Results after Drying";
                }
                else
                {
                    nr.Title = "Measurements Results after Hydro";
                }

                dataTable1.AddDataTable1Row(nr);
                string[][] ColumnNames = null;
                if (_parms.DyeStage == 4)
                {
                    ColumnNames = new string[][]
                    {   new string[] {"Text5", "Batch No"},
                        new string[] {"Text6", "Piece No"},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                    };
                }
                else
                {
                    ColumnNames = new string[][]
                    {  new string[] {"Text5", "Batch No"},
                        new string[] {"Text6", "Piece No"},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text9", string.Empty},
                    };
                }
                using (var context = new TTI2Entities())
                {
                    if (_parms.DyeStage == 4)
                    {
                        QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 5 && x.TLQADPF_Drier).ToList();
                    }
                    else
                    {
                        QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 5 && x.TLQADPF_Hydro && !x.TLQAPF_Operator_Ins).ToList();
                    }

                    foreach (var QAProcessItem in QAProcessItems)
                    {
                        foreach (var Column in ColumnNames)
                        {
                            if (String.IsNullOrEmpty(Column[1]))
                            {
                                Column[1] = QAProcessItem.TLQADPF_Description;
                                break;
                            }
                        }
                    }
                    if (_parms.DyeStage == 4)
                    {
                        var DBGroups = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_Date >= _parms.FromDate && x.TLDYEDC_Date <= _parms.ToDate && x.TLDYEDC_NCStage == 4 & x.TLDYEDC_Pass && (x.TLDYEDC_Code_FK == 51 || x.TLDYEDC_Code_FK == 52 || x.TLDYEDC_Code_FK == 53)).OrderBy(x => x.TLDYEDC_BatchNo).ThenBy(x => x.TLDYEDC_Code_FK).ToList();
                        var DyeBatchGroups = DBGroups.GroupBy(x => x.TLDYEDC_BatchNo);

                        foreach (var DyeBatchGroup in DyeBatchGroups)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(DyeBatchGroup.FirstOrDefault().TLDYEDC_BatchNo);
                            if (DB != null)
                            {
                                DataSet49.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                                xnr.Pk = 1;
                                xnr.DyeBatch = DB.DYEB_BatchNo;
                                var index = DyeBatchGroup.FirstOrDefault().TLDYEDC_PieceNo_FK;
                                xnr.PieceNumber = string.Empty;
                                var GreigeProd = context.TLKNI_GreigeProduction.Find(index);
                                if (GreigeProd != null)
                                {
                                    xnr.PieceNumber = GreigeProd.GreigeP_PieceNo;
                                }

                                xnr.Machine = string.Empty;
                                var DBAlloc = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                if (DBAlloc != null)
                                {
                                    xnr.Machine = (from T1 in context.TLDYE_DyeBatchAllocated
                                                   join T2 in context.TLADM_MachineDefinitions
                                                   on T1.TLDYEA_MachCode_FK equals T2.MD_Pk
                                                   where T1.TLDYEA_DyeBatch_FK == DB.DYEB_Pk
                                                   select T2).FirstOrDefault().MD_Description;
                                }

                                xnr.Quality = context.TLADM_Griege.Find(DB.DYEB_Greige_FK).TLGreige_Description;
                                xnr.Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;

                                foreach (var DyePiece in DyeBatchGroup)
                                {
                                    if (DyePiece.TLDYEDC_Code_FK == 51)
                                    {
                                        xnr.WidthBefore = DyePiece.TLDYEDC_Value;
                                        var DyeStan = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DB.DYEB_Greige_FK && x.DyeStan_QAProccessField_FK == DyePiece.TLDYEDC_Code_FK).FirstOrDefault();
                                        if (DyeStan != null)
                                        {
                                            xnr.StandWidthB4 = DyeStan.DyeStan_Value;
                                        }
                                    }
                                    else if (DyePiece.TLDYEDC_Code_FK == 52)
                                    {
                                        xnr.Speed = DyePiece.TLDYEDC_Value;
                                        var DyeStan = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DB.DYEB_Greige_FK && x.DyeStan_QAProccessField_FK == DyePiece.TLDYEDC_Code_FK).FirstOrDefault();
                                        if (DyeStan != null)
                                        {
                                            xnr.StandSpeed = DyeStan.DyeStan_Value;
                                        }
                                    }
                                    else if (DyePiece.TLDYEDC_Code_FK == 53)
                                    {
                                        xnr.WidthAfter = DyePiece.TLDYEDC_Value;
                                        var DyeStan = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DB.DYEB_Greige_FK && x.DyeStan_QAProccessField_FK == DyePiece.TLDYEDC_Code_FK).FirstOrDefault();
                                        if (DyeStan != null)
                                        {
                                            xnr.StandWidthAft = DyeStan.DyeStan_Value;
                                        }
                                    }
                                }

                                dataTable2.AddDataTable2Row(xnr);
                            }
                        }

                    }
                    else
                    {
                        var DyeBatchGroups = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_Date >= _parms.FromDate && x.TLDYEDC_Date <= _parms.ToDate && x.TLDYEDC_NCStage == 6 & x.TLDYEDC_Pass).GroupBy(x => new { x.TLDYEDC_BatchNo, x.TLDYEDC_PieceNo_FK }).ToList();

                        foreach (var DyeBatchGroup in DyeBatchGroups)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(DyeBatchGroup.FirstOrDefault().TLDYEDC_BatchNo);
                            if (DB != null)
                            {
                                DataSet49.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                                xnr.Pk = 1;
                                xnr.DyeBatch = DB.DYEB_BatchNo;
                                var index = DyeBatchGroup.FirstOrDefault().TLDYEDC_PieceNo_FK;
                                xnr.PieceNumber = string.Empty;
                                var GreigeProd = context.TLKNI_GreigeProduction.Find(index);
                                if (GreigeProd != null)
                                {
                                    xnr.PieceNumber = GreigeProd.GreigeP_PieceNo;
                                }

                                xnr.Machine = string.Empty;
                                var DBAlloc = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                                if (DBAlloc != null)
                                {
                                    xnr.Machine = (from T1 in context.TLDYE_DyeBatchAllocated
                                                   join T2 in context.TLADM_MachineDefinitions
                                                   on T1.TLDYEA_MachCode_FK equals T2.MD_Pk
                                                   where T1.TLDYEA_DyeBatch_FK == DB.DYEB_Pk
                                                   select T2).FirstOrDefault().MD_Description;
                                }
                                xnr.Quality = context.TLADM_Griege.Find(DB.DYEB_Greige_FK).TLGreige_Description;

                                xnr.Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                foreach (var DyePiece in DyeBatchGroup)
                                {
                                    if (DyePiece.TLDYEDC_Code_FK == 46)
                                    {
                                        xnr.WidthBefore = DyePiece.TLDYEDC_Value;
                                        var DyeStan = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DB.DYEB_Greige_FK && x.DyeStan_QAProccessField_FK == DyePiece.TLDYEDC_Code_FK).FirstOrDefault();
                                        if (DyeStan != null)
                                        {
                                            xnr.StandWidthB4 = DyeStan.DyeStan_Value;
                                        }
                                    }
                                    else if (DyePiece.TLDYEDC_Code_FK == 50)
                                    {
                                        xnr.WidthAfter = DyePiece.TLDYEDC_Value;
                                        var DyeStan = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DB.DYEB_Greige_FK && x.DyeStan_QAProccessField_FK == DyePiece.TLDYEDC_Code_FK).FirstOrDefault();
                                        if (DyeStan != null)
                                        {
                                            xnr.StandWidthAft = DyeStan.DyeStan_Value;
                                        }
                                    }

                                }

                                dataTable2.AddDataTable2Row(xnr);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet49.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                    xnr.Pk = 1;
                    xnr.ErrorLog = "No records found for selection made";
                    dataTable2.AddDataTable2Row(xnr);
                }
                ds.Tables.Add(dataTable2);

                DyeQAException QAExcept = new DyeQAException();
                QAExcept.SetDataSource(ds);
                crystalReportViewer1.ReportSource = QAExcept;
                IEnumerator ie = QAExcept.Section2.ReportObjects.GetEnumerator();
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

                if (_parms.DO_OptionSelected == 1)
                {
                    try
                    {
                        //"Quality
                        QAExcept.DataDefinition.Groups[0].ConditionField = QAExcept.Database.Tables[1].Fields[11];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (_parms.DO_OptionSelected == 2)
                {
                    // Machine
                    QAExcept.DataDefinition.Groups[0].ConditionField = QAExcept.Database.Tables[1].Fields[9];

                }
                else if (_parms.DO_OptionSelected == 3)
                {
                    //Colour
                    QAExcept.DataDefinition.Groups[0].ConditionField = QAExcept.Database.Tables[1].Fields[10];

                }

            }
            else if (_RepNo == 33)  //Stability check results after compacting   
            {

                DataSet ds = new DataSet();
                DataSet42.DataTable1DataTable dataTable1 = new DataSet42.DataTable1DataTable();
                DataSet42.DataTable2DataTable dataTable2 = new DataSet42.DataTable2DataTable();

                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                    {   new string[] {"Text8", "Batch No"},
                        new string[] {"Text3", "Piece No"},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty}

                    };

                IList<TLDYE_DyeTransactions> DyeTrans = new List<TLDYE_DyeTransactions>();
                TLDYE_DyeBatch DyeBatches = new TLDYE_DyeBatch();

                TLDYE_NonCompliance nonC = new TLDYE_NonCompliance();
                TLDYE_NonComplianceDetail nonCD = new TLDYE_NonComplianceDetail();

                using (var context = new TTI2Entities())
                {
                    var QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 3 || x.TLQAPF_Compactor).ToList();
                    foreach (var QAProcessItem in QAProcessItems)
                    {
                        foreach (var Column in ColumnNames)
                        {
                            if (String.IsNullOrEmpty(Column[1]))
                            {
                                Column[1] = QAProcessItem.TLQADPF_Description;
                                break;
                            }
                        }
                    }

                    DataSet42.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.NCRPk = 1;
                    nr.NCRFromDate = _parms.FromDate;
                    nr.NCRToDate = _parms.ToDate;
                    nr.NCReportTitle = "Measurement Results after compacting from ";
                    nr.NCRSelection = string.Empty;
                    nr.NCRSorted = string.Empty;
                    dataTable1.AddDataTable1Row(nr);

                    var DyeBatchGroups = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_Date >= _parms.FromDate && x.TLDYEDC_Date <= _parms.ToDate && x.TLDYEDC_NCStage == 5 & x.TLDYEDC_Pass && x.TLDYEDC_Code_FK >= 54 && x.TLDYEDC_Code_FK <= 58).GroupBy(x => new { x.TLDYEDC_BatchNo, x.TLDYEDC_PieceNo_FK }).ToList();
                    foreach (var DyeBatchGroup in DyeBatchGroups)
                    {
                        var DB = context.TLDYE_DyeBatch.Find(DyeBatchGroup.FirstOrDefault().TLDYEDC_BatchNo);
                        if (DB != null)
                        {
                            DataSet42.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                            xnr.NCRPk = 1;
                            xnr.NCRBatchNo = DB.DYEB_BatchNo;
                            var index = DyeBatchGroup.FirstOrDefault().TLDYEDC_PieceNo_FK;
                            var GreigeProd = context.TLKNI_GreigeProduction.Find(index);
                            if (GreigeProd != null)
                            {
                                xnr.NCRPieceNo = GreigeProd.GreigeP_PieceNo;
                            }
                            else
                                xnr.NCRPieceNo = string.Empty;

                            foreach (var DyePiece in DyeBatchGroup)
                            {
                                if (DyePiece.TLDYEDC_Code_FK == 12)
                                    xnr.NCRLengthShrinkage = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 13)
                                    xnr.NCRFabricWidth = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 14)
                                    xnr.NCRSpiral = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 15)
                                    xnr.NCRCourse = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 16)
                                    xnr.NCRWales = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 54)
                                    xnr.NCRWidthB4 = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 55)
                                    xnr.NCRCompaction = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 57)
                                    xnr.NCRWidth_AFT = DyePiece.TLDYEDC_Value;
                                else if (DyePiece.TLDYEDC_Code_FK == 58)
                                    xnr.NCRWeight_AFT = DyePiece.TLDYEDC_Value;
                            }

                            dataTable2.AddDataTable2Row(xnr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ShadeAfterDrying sad = new ShadeAfterDrying();
                sad.SetDataSource(ds);
                crystalReportViewer1.ReportSource = sad;

                IEnumerator ie = sad.Section2.ReportObjects.GetEnumerator();
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
            else if (_RepNo == 34)  //Dye Machine Performance    
            {
                DataSet ds = new DataSet();
                DataSet34.DataTable1DataTable datatable1 = new DataSet34.DataTable1DataTable();
                DataSet34.DataTable2DataTable dataTable2 = new DataSet34.DataTable2DataTable();
                IList<TLDYE_DyeBatch> DBatches = null;

                using (var context = new TTI2Entities())
                {
                    IList<TLADM_Griege> _Quality = context.TLADM_Griege.ToList();
                    IList<TLADM_Colours> _Colours = context.TLADM_Colours.ToList();
                    IList<TLDYE_AllocatedOperator> _allocatedOperator = context.TLDYE_AllocatedOperator.ToList();
                    IList<TLADM_MachineOperators> _Operators = context.TLADM_MachineOperators.ToList();
                    IList<TLADM_MachineDefinitions> _Machines = context.TLADM_MachineDefinitions.ToList();

                    if (_repOps.DYEMPProdType == 1)
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate && !(bool)x.DYEB_Reprocess).ToList();
                    else if (_repOps.DYEMPProdType == 2)
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate && !x.DYEB_CommissinCust && (bool)x.DYEB_Reprocess).ToList();
                    else
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= _repOps.fromDate && x.DYEB_OutProcessDate <= _repOps.toDate && x.DYEB_CommissinCust).ToList();

                    foreach (var DBatch in DBatches)
                    {
                        var DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DBatch.DYEB_Pk);
                        if (DBDetails.Count() > 0)
                        {
                            DataSet34.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.Pk = 1;
                            nr.BatchNumber = DBatch.DYEB_BatchNo;
                            nr.DateDyed = (DateTime)DBatch.DYEB_OutProcessDate;
                            nr.Greige = _Quality.FirstOrDefault(s => s.TLGreige_Id == DBatch.DYEB_Greige_FK).TLGreige_Description;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DBatch.DYEB_Colour_FK).Col_Display;
                            nr.BodyKg = DBDetails.Where(x => x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0;
                            nr.TrimKg = DBDetails.Where(x => !x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0;
                            nr.Pieces = DBDetails.Count();
                            var OrigWeight = DBatch.DYEB_BatchKG;
                            nr.ProcessLoss = 100 - ((nr.BodyKg + nr.TrimKg) / OrigWeight * 100);
                            if (DBatch.DYEB_Reprocess)
                                nr.Reprocessed = (bool)DBatch.DYEB_Reprocess;
                            else
                                nr.Reprocessed = false;

                            var Operator = _allocatedOperator.FirstOrDefault(s => s.DYEOP_BatchNo_FK == DBatch.DYEB_Pk);
                            if (Operator != null)
                            {
                                nr.Operator = _Operators.FirstOrDefault(s => s.MachOp_Pk == Operator.DYEOP_Operator_FK).MachOp_Description;
                            }

                            var Machine = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DBatch.DYEB_Pk).FirstOrDefault();
                            if (Machine != null)
                            {
                                if (_repOps.DYEMPMachineSelected != 0 && _repOps.DYEMPMachineSelected != Machine.TLDYEA_MachCode_FK)
                                    continue;

                                nr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == Machine.TLDYEA_MachCode_FK).MD_MachineCode;
                            }

                            nr.Result = "RFT";

                            if (DBatch.DYEB_Reprocess != null)
                            {
                                if ((bool)DBatch.DYEB_Reprocess)
                                    nr.Result = "BTBS";
                            }

                            dataTable2.AddDataTable2Row(nr);
                        }
                    }
                }
                DataSet34.DataTable1Row t1 = datatable1.NewDataTable1Row();
                t1.Pk = 1;
                t1.FromDate = _repOps.fromDate;
                t1.ToDate = _repOps.toDate;
                t1.Title = "Dye Machine Performance";
                if (_repOps.DYEMPSortOptions == 0)
                {
                    t1.Title += " Grouped by machine";

                }
                else if (_repOps.DYEMPSortOptions == 1)
                {
                    t1.Title += " Grouped by Batch Number";

                }
                else if (_repOps.DYEMPSortOptions == 2)
                {
                    //Quality
                    t1.Title += " Grouped by Quality";
                }
                else if (_repOps.DYEMPSortOptions == 3)
                {
                    // Colour
                    t1.Title += " Grouped by colour";
                }
                else if (_repOps.DYEMPSortOptions == 4)
                {
                    // result
                    t1.Title += " Grouped by result";
                }
                else if (_repOps.DYEMPSortOptions == 5)
                {
                    // Operator
                    t1.Title += " Grouped by Operator";
                }
                datatable1.AddDataTable1Row(t1);

                if (dataTable2.Count == 0)
                {
                    DataSet34.DataTable2Row nr = dataTable2.NewDataTable2Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "There are no records matching selection made";
                    dataTable2.AddDataTable2Row(nr);
                }

                ds.Tables.Add(datatable1);
                ds.Tables.Add(dataTable2);
                DyeMachinePerformance dmp = new DyeMachinePerformance();
                dmp.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dmp;

                // ("0", "Machine"));
                // ("1", "Batch Number"));
                // ("2", "Quality"));
                // ("3", "Colour"));
                // ("4", "Result"));
                // ("4", "Operator"));
                if (_repOps.DYEMPSortOptions == 0)
                {
                    try
                    {
                        //Machine
                        dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[13];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (_repOps.DYEMPSortOptions == 1)
                {
                    // Batch Number
                    dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[1];

                }
                else if (_repOps.DYEMPSortOptions == 2)
                {
                    //Quality
                    dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[3];
                }
                else if (_repOps.DYEMPSortOptions == 3)
                {
                    // Colour
                    dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[4];
                }
                else if (_repOps.DYEMPSortOptions == 4)
                {
                    // result
                    dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[11];
                }
                else if (_repOps.DYEMPSortOptions == 5)
                {
                    // Operator
                    dmp.DataDefinition.Groups[0].ConditionField = dmp.Database.Tables[1].Fields[10];
                }
            }
            else if (_RepNo == 35)  //Resource Efficiency    
            {
                DataSet ds = new DataSet();
                DataSet35.DataTable1DataTable dataTable1 = new DataSet35.DataTable1DataTable();
                DataSet35.DataTable2DataTable dataTable2 = new DataSet35.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    IList<TLADM_MachineDefinitions> _Machines = context.TLADM_MachineDefinitions.ToList();

                    var DBAllocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_AllocateDate >= _repOps.fromDate && x.TLDYEA_AllocateDate <= _repOps.toDate).OrderBy(x => x.TLDYEA_MachCode_FK).ThenBy(x => x.TLDYEA_FabricType_FK).GroupBy(x => new { x.TLDYEA_MachCode_FK, x.TLDYEA_FabricType_FK });
                    foreach (var DBGroup in DBAllocated)
                    {
                        if (_repOps.ResEficMachine != 0)
                        {
                            if (DBGroup.FirstOrDefault().TLDYEA_MachCode_FK != _repOps.ResEficMachine)
                                continue;
                        }

                        Decimal _BodyKg = 0.00M;
                        Decimal _TrimKg = 0.00M;
                        int _Pieces = 0;
                        Decimal _OrigKg = 0.00M;

                        DataSet35.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Pk = 1;

                        var MachineDetails = _Machines.FirstOrDefault(s => s.MD_Pk == DBGroup.FirstOrDefault().TLDYEA_MachCode_FK);

                        if (MachineDetails != null)
                        {
                            nr.Machine = MachineDetails.MD_MachineCode;
                        }

                        nr.FabricType = context.TLADM_FabricProduct.Find(DBGroup.FirstOrDefault().TLDYEA_FabricType_FK).FP_Description;
                        nr.NoOfBatches = DBGroup.Count();

                        foreach (var Group in DBGroup)
                        {
                            var DBBatch = context.TLDYE_DyeBatch.Find(Group.TLDYEA_DyeBatch_FK);
                            if (DBBatch != null)
                                _OrigKg += DBBatch.DYEB_BatchKG;

                            var DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == Group.TLDYEA_DyeBatch_FK).ToList();
                            _Pieces += DBDetails.Count();
                            _BodyKg += DBDetails.Where(x => x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                            _TrimKg += DBDetails.Where(x => !x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                        }

                        nr.BodyKg = (int)_BodyKg;
                        nr.TrimKg = (int)_TrimKg;
                        nr.Pieces = _Pieces;
                        nr.ProcessLoss = 100M * (nr.BodyKg + nr.TrimKg) / _OrigKg - 100M;
                        nr.Electricity = (int)((_BodyKg + _TrimKg) * (decimal)MachineDetails.MD_FirstMeasure_Qty);
                        nr.KVA = (int)((_BodyKg + _TrimKg) * (decimal)MachineDetails.MD_FifthMeasure_Qty);
                        nr.WaterLitre = (int)((_BodyKg + _TrimKg) * (decimal)MachineDetails.MD_SecMeasure_Qty);
                        nr.Labour = (int)((_BodyKg + _TrimKg) * (decimal)MachineDetails.MD_ThirdMeasure_Qty);
                        nr.Steam = (int)((_BodyKg + _TrimKg) * (decimal)MachineDetails.MD_FourthMeasure_Qty);
                        nr.Effluent = 0;

                        dataTable2.AddDataTable2Row(nr);

                    }

                }
                DataSet35.DataTable1Row tr = dataTable1.NewDataTable1Row();
                tr.Pk = 1;
                tr.FromDate = _repOps.fromDate;
                tr.ToDate = _repOps.toDate;
                tr.Title = "Resource Efficiency";
                tr.AltTitle = "Drying Resources standard comsumption for the period ";

                dataTable1.AddDataTable1Row(tr);

                ds.Tables.Add(dataTable1);

                if (dataTable2.Rows.Count == 0)
                {
                    DataSet35.DataTable2Row nr = dataTable2.NewDataTable2Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable2.AddDataTable2Row(nr);
                }
                ds.Tables.Add(dataTable2);

                ResourceEfficiency dmp = new ResourceEfficiency();
                dmp.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dmp;
            }
            else if (_RepNo == 36)  // Fabric Sales Delivery Report    
            {
                DataSet ds = new DataSet();
                DataSet37.DataTable1DataTable dataTable1 = new DataSet37.DataTable1DataTable();
                DataSet37.DataTable2DataTable dataTable2 = new DataSet37.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    IList<TLADM_Griege> _Quality = context.TLADM_Griege.ToList();
                    IList<TLADM_Colours> _Colours = context.TLADM_Colours.ToList();
                    IList<TLADM_CustomerFile> _Customers = context.TLADM_CustomerFile.ToList();
                    _repo = new DyeRepository();

                    string[][] ColumnNames = null;

                    ColumnNames = new string[][]
                        {   new string[] {"Text13", "By Delivery No"},
                            new string[] {"Text13", "By Contract No"}};
                    var FabricSales = _repo.SelectFabricSales(_parms);

                    foreach (var FSale in FabricSales)
                    {
                        var ExistingGroups = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_TransactionNo == FSale.TLDYET_TransactionNumber).GroupBy(x => x.DYEBO_TransactionNo).ToList();

                        foreach (var Group in ExistingGroups)
                        {
                            var TransactionNo = Group.FirstOrDefault().DYEBO_TransactionNo;

                            foreach (var record in Group)
                            {
                                DataSet37.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                                t2r.Pk = 1;

                                if (string.IsNullOrEmpty(TransactionNo))
                                {
                                    continue;
                                }

                                var DB = context.TLDYE_DyeBatch.Find(record.DYEBD_DyeBatch_FK);
                                if (DB != null)
                                {
                                    t2r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DB.DYEB_Colour_FK).Col_Display;

                                    var TransactionDetails = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionNumber == TransactionNo).FirstOrDefault();
                                    if (TransactionDetails != null)
                                    {
                                        t2r.OrderNo = TransactionDetails.TLDYET_CustomerOrderNo;

                                        if (record.DYEBO_PurchaseOrderDetail_FK != 0)
                                        {
                                            var tst = (from T1 in context.TLCSV_PurchaseOrder
                                                       join T2 in context.TLCSV_PuchaseOrderDetail
                                                       on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                                       where T2.TLCUSTO_Pk == record.DYEBO_PurchaseOrderDetail_FK
                                                       select T1.TLCSVPO_PurchaseOrder).FirstOrDefault();

                                            if (tst != null)
                                            {
                                                t2r.OrderNo = tst;
                                            }
                                        }

                                        t2r.CustNo = _Customers.FirstOrDefault(s => s.Cust_Pk == TransactionDetails.TLDYET_Customer_FK).Cust_Code;
                                        t2r.CustName = _Customers.FirstOrDefault(s => s.Cust_Pk == TransactionDetails.TLDYET_Customer_FK).Cust_Description;

                                        t2r.Quality = _Quality.FirstOrDefault(s => s.TLGreige_Id == record.DYEBD_QualityKey).TLGreige_Description;
                                        t2r.Meters = record.DYEBO_Meters;
                                        t2r.Nett = record.DYEBO_Nett;
                                        t2r.PieceNo = context.TLKNI_GreigeProduction.Find(record.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                                        t2r.Wth = (int)record.DYEBO_Width;
                                        t2r.Disk = (int)record.DYEBO_DiskWeight;
                                        if (record.DYEBO_DateSold != null)
                                        {
                                            t2r.TransDate = (DateTime)record.DYEBO_DateSold;
                                        }
                                        else
                                        {
                                            t2r.TransDate = DateTime.Now;
                                        }

                                        t2r.TransNo = TransactionNo;

                                        dataTable2.AddDataTable2Row(t2r);
                                    }
                                }
                            }

                        }
                    }
                }

                DataSet37.DataTable1Row t1 = dataTable1.NewDataTable1Row();
                t1.Pk = 1;
                t1.FromDate = _parms.FromDate;
                t1.ToDate = _parms.ToDate;
                t1.Title = "Fabric Sales for the period ";

                dataTable1.AddDataTable1Row(t1);
                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet37.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                    t2r.Pk = 1;
                    t2r.ErrorLog = "No records found pertaining to selection made";
                    dataTable2.AddDataTable2Row(t2r);
                }
                ds.Tables.Add(dataTable2);

                FabricSalesByDate dmp = new FabricSalesByDate();
                TextObject text = (TextObject)dmp.ReportDefinition.Sections["Section2"].ReportObjects["Text13"];
                text.Text = "Grouped By Delivery No";

                if (_parms.FabricSalesReportOption == 1)
                {
                    //This is the contract number
                    text.Text = "Grouped By Contract";
                    dmp.DataDefinition.Groups[1].ConditionField = dmp.Database.Tables[1].Fields[13];
                }

                dmp.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dmp;
            }
            else if (_RepNo == 37)  // This is the Dye Chemical reporting (Costing Format)     
            {
                DataSet ds = new DataSet();
                DataSet38.DataTable1DataTable dataTable1 = new DataSet38.DataTable1DataTable();
                TLADM_ConsumablesDC ConsDC = null;
                using (var context = new TTI2Entities())
                {
                    //----------------------------------------------------------------
                    // Now get all the records in the batch details
                    //----------------------------------------------------------------
                    foreach (var Record in _ProdDetails)
                    {
                        var ReceipeDefinition = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_ColorChart_FK == Record.ColorPk).FirstOrDefault();
                        if (ReceipeDefinition != null)
                        {
                            var ReceipeDetails = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == ReceipeDefinition.TLDYE_DefinePk).ToList();
                            foreach (var ReceipeRecord in ReceipeDetails)
                            {
                                ConsDC = context.TLADM_ConsumablesDC.Find(ReceipeRecord.TLDYED_Cosumables_FK);

                                if (ConsDC == null || (bool)ConsDC.ConsDC_Discontinued)
                                    continue;

                                DataSet38.DataTable1Row tr = dataTable1.NewDataTable1Row();

                                tr.Quality = context.TLADM_Griege.Find(Record.GreigePk).TLGreige_Description;
                                tr.Color = context.TLADM_Colours.Find(Record.ColorPk).Col_Display;
                                tr.ExpectedProduction = Record.PlannedProd;
                                tr.ChemicalDescription = ConsDC.ConsDC_Description;

                                Decimal ExpectedConsumption = 0.00M;

                                if (!ReceipeRecord.TLDYED_LiqCalc)
                                {
                                    //--------------------------------------------------------
                                    // The MELFC is a percentage of the receipe Program Load 
                                    //-------------------------------------------------------------------------------------
                                    ExpectedConsumption = (ReceipeRecord.TLDYED_MELFC * Record.PlannedProd) / 100;
                                }
                                else
                                {
                                    //----------------------------------------------------------
                                    // Now use Liquidity factor 
                                    //-------------------------------------------------------------
                                    ExpectedConsumption = (ReceipeRecord.TLDYED_MELFC * Record.PlannedProd * ReceipeRecord.TLDYED_LiqRatio) / 1000;
                                }

                                //-----------------------------------
                                //Find this Record in the SOH
                                //----------------------------------------------
                                var SOH = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == ReceipeRecord.TLDYED_Cosumables_FK && x.DYCSH_DyeKitchen).FirstOrDefault();
                                if (SOH == null)
                                {
                                    tr.CurrentStockHand = 0;
                                    tr.ChemicalDescription += "*";
                                    tr.ExpectedConsumption = ExpectedConsumption;
                                    tr.Variance = tr.CurrentStockHand - ExpectedConsumption;
                                    if (ConsDC != null)
                                    {
                                        tr.ReorderLevel = ConsDC.ConsDC_ReOrderLevel;
                                        tr.ReOrderQty = ConsDC.ConsDC_MinReorderQty;
                                        tr.CurrentCost = ConsDC.ConsDC_StandardCost;
                                        tr.ProjectedCost = ConsDC.ConsDC_MinReorderQty * ConsDC.ConsDC_StandardCost;
                                    }
                                }
                                else
                                {
                                    tr.CurrentStockHand = SOH.DYCSH_StockOnHand;
                                    tr.ExpectedConsumption = ExpectedConsumption;
                                    tr.Variance = tr.CurrentStockHand - ExpectedConsumption;
                                    if (ConsDC != null)
                                    {
                                        tr.ReorderLevel = 0;
                                        tr.ReOrderQty = 0;
                                        tr.CurrentCost = 0;
                                        tr.ProjectedCost = 0;

                                        if (ExpectedConsumption > 0)
                                        {
                                            tr.ReorderLevel = ConsDC.ConsDC_ReOrderLevel;
                                            tr.ReOrderQty = ConsDC.ConsDC_MinReorderQty;
                                            tr.CurrentCost = ConsDC.ConsDC_StandardCost;
                                            tr.ProjectedCost = ConsDC.ConsDC_MinReorderQty * ConsDC.ConsDC_StandardCost;
                                        }
                                    }
                                }
                                dataTable1.AddDataTable1Row(tr);
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                DCProductionPlan dmp = new DCProductionPlan();
                dmp.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dmp;
            }
            else if (_RepNo == 38)  // This is discontinued Dyes and Chemicals that may still have stock on Hand     
            {
                DataSet ds = new DataSet();
                DataSet39.DataTable1DataTable dataTable1 = new DataSet39.DataTable1DataTable();
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLADM_ConsumablesDC.Where(x => x.ConsDC_Discontinued).ToList();
                    foreach (var Row in Existing)
                    {

                        var CurrentSOH = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == Row.ConsDC_Pk).ToList();

                        foreach (var SOH in CurrentSOH)
                        {
                            if (SOH.DYCSH_StockOnHand <= 0.00M)
                                continue;

                            DataSet39.DataTable1Row nr = dataTable1.NewDataTable1Row();

                            nr.DyeChemCode = Row.ConsDC_Code;
                            nr.DyeChemDescription = Row.ConsDC_Description;
                            nr.DyeChemDate = (DateTime)Row.ConsDC_DiscontinuedDate;
                            nr.DyeChemSOH = SOH.DYCSH_StockOnHand;
                            nr.DyeChemWhse = context.TLADM_WhseStore.Find(SOH.DYCSH_WhseStore_FK).WhStore_Description;

                            dataTable1.AddDataTable1Row(nr);
                        }

                    }

                    if (dataTable1.Rows.Count == 0)
                    {
                        DataSet39.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.ErrorLog = "There no records currently available";
                        dataTable1.AddDataTable1Row(nr);
                    }

                    ds.Tables.Add(dataTable1);
                    DiscontinuedSOH dmp = new DiscontinuedSOH();
                    dmp.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = dmp;

                }
            }
            else if (_RepNo == 39) // DyeBatch Status Report
            {

                DataSet ds = new DataSet();
                DataSet40.DataTable1DataTable datatable1 = new DataSet40.DataTable1DataTable();
                _repo = new DyeRepository();
                string[][] ColumnNames = null;

                core = new Util();

                var DyeBatches = _repo.SelectViewDyeBatches(_parms);

                ColumnNames = new string[][]
                    {   new string[] {"Text15", string.Empty},
                        new string[] {"Text16", string.Empty},
                        new string[] {"Text18", string.Empty},
                        new string[] {"Text19", string.Empty},
                        new string[] {"Text20", string.Empty},
                        new string[] {"Text21", string.Empty},
                        new string[] {"Text22", string.Empty},
                        new string[] {"Text23", string.Empty},
                        new string[] {"Text24", string.Empty},
                        new string[] {"Text25", string.Empty},
                        new string[] {"Text26", string.Empty}

                    };

                var CNames = core.CreateColumnNames();

                int i = 0;
                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                using (var context = new TTI2Entities())
                {
                    foreach (var DyeBatch in DyeBatches)
                    {
                        DataSet40.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.DyeBatchNo = DyeBatch.DYEB_BatchNo;
                        nr.DyeBatchDate = (DateTime)DyeBatch.DYEB_BatchDate;
                        nr.DyeBatchReqDate = (DateTime)DyeBatch.DYEB_RequiredDate;
                        nr.DyeBatchWeight = Math.Round(DyeBatch.DYEB_BatchKG, 2);
                        nr.DyeBatchClosed = false;
                        nr.DyeBatchOutofProcess = false;
                        nr.DyeBatchAllocated = false;
                        nr.DyeBatchStage1 = false;
                        nr.DyeBatchStage2 = false;
                        nr.DyeBatchStage3 = false;
                        nr.DyeBatchTransfer = false;
                        nr.DyeFabStore = false;
                        nr.DyeCut = false;
                        nr.DyeCMT = false;

                        /*  nr.Col1 = 0;
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
                          nr.Col12 = 0; */

                        var DO = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                        if (DO != null)
                        {
                            nr.DyeBatchStyles = context.TLADM_Styles.Find(DO.TLDYO_Style_FK).Sty_Description;
                            var DOD = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                            if (DOD != null)
                            {
                                BindingList<KeyValuePair<int, decimal>> Ratios = null;
                                Ratios = core.ReturnRatios(DOD.TLDYOD_MarkerRating_FK);

                                var TotalWeight = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk && x.DYEBD_BodyTrim).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                nr.DyeBatchWeight = TotalWeight;
                                var EUnits = 0;

                                var SumRatio = Ratios.Sum(x => x.Value);

                                foreach (var Ratio in Ratios)
                                {
                                    var FabricYield = DOD.TLDYOD_Yield;
                                    var FabricRating = Ratio.Value;
                                    var Percentage = Math.Round(Ratio.Value / SumRatio, 2);

                                    if (TotalWeight > 0.00M)
                                        EUnits = Convert.ToInt32(FabricYield / FabricRating * (TotalWeight * Percentage));
                                    else
                                        EUnits = 0;

                                    /* var Size = context.TLADM_Sizes.Find(Ratio.Key);
                                      if (Size != null)
                                      {
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
                                    */

                                }
                            }
                        }

                        if (DyeBatch.DYEB_Closed)
                        {
                            nr.DyeBatchClosed = true;
                        }

                        if (DyeBatch.DYEB_Transfered)
                        {
                            nr.DyeBatchTransfer = true;
                            TLDYE_DyeTransactions dt = null;
                            dt = context.TLDYE_DyeTransactions.FirstOrDefault(x => x.TLDYET_Batch_FK == DyeBatch.DYEB_Pk && x.TLDYET_Stage == 2);
                            if (dt != null)
                            {
                                nr.DateTransfered = dt.TLDYET_Date;
                            }
                        }
                        if (DyeBatch.DYEB_Allocated)
                        {
                            nr.DyeBatchAllocated = true;

                            TLDYE_DyeBatchAllocated Allocated = context.TLDYE_DyeBatchAllocated.FirstOrDefault(x => x.TLDYEA_DyeBatch_FK == DyeBatch.DYEB_Pk);
                            if (Allocated != null)
                            {
                                nr.DEateAllocated = Allocated.TLDYEA_AllocateDate;
                            }
                        }
                        if (DyeBatch.DYEB_Stage1)
                        {
                            nr.DyeBatchStage1 = true;
                            if (DyeBatch.DYEB_DateStage1 != null)
                            {
                                nr.DateDyeing = (DateTime)DyeBatch.DYEB_DateStage1;
                            }
                            else
                            {
                                TLDYE_DyeTransactions trns = context.TLDYE_DyeTransactions.FirstOrDefault(x => x.TLDYET_Batch_FK == DyeBatch.DYEB_Pk && x.TLDYET_Stage == 3);
                                if (trns != null)
                                {
                                    nr.DateDyeing = (DateTime)trns.TLDYET_Date;
                                }
                            }
                        }
                        if (DyeBatch.DYEB_Stage2)
                        {
                            nr.DyeBatchStage2 = true;
                            if (DyeBatch.DYEB_DateStage2 != null)
                            {
                                nr.DateDrying = (DateTime)DyeBatch.DYEB_DateStage2;
                            }
                            else
                            {
                                TLDYE_NonComplianceAnalysis nca = new TLDYE_NonComplianceAnalysis();
                                nca = context.TLDYE_NonComplianceAnalysis.FirstOrDefault(x => x.TLDYEDC_BatchNo == DyeBatch.DYEB_Pk && x.TLDYEDC_NCStage == 4);
                                if (nca != null)
                                {
                                    nr.DateDrying = (DateTime)nca.TLDYEDC_Date;
                                }
                            }
                        }
                        if (DyeBatch.DYEB_Stage3)
                        {
                            nr.DyeBatchStage3 = true;
                            if (DyeBatch.DYEB_DateStage3 != null)
                            {
                                nr.DateDrying = (DateTime)DyeBatch.DYEB_DateStage3;
                            }
                            else
                            {
                                TLDYE_NonComplianceAnalysis nca = new TLDYE_NonComplianceAnalysis();
                                nca = context.TLDYE_NonComplianceAnalysis.FirstOrDefault(x => x.TLDYEDC_BatchNo == DyeBatch.DYEB_Pk && x.TLDYEDC_NCStage == 5);
                                if (nca != null)
                                {
                                    nr.DateCompacing = (DateTime)nca.TLDYEDC_Date;
                                }
                            }
                        }
                        if (DyeBatch.DYEB_OutProcess)
                        {
                            nr.DyeBatchOutofProcess = true;
                            nr.DateQStore = (DateTime)DyeBatch.DYEB_OutProcessDate;
                        }

                        var DBDetail = (from T1 in context.TLDYE_DyeBatch
                                        join T3 in context.TLDYE_DyeBatchDetails
                                        on T1.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                                        where T1.DYEB_Pk == DyeBatch.DYEB_Pk
                                        select T3).ToList();

                        if (DBDetail != null)
                        {
                            var QAApproved = DBDetail.FirstOrDefault(x => x.DYEBO_QAApproved);
                            if (QAApproved != null)
                            {
                                nr.DyeFabStore = true;
                                nr.DateFStore = (DateTime)QAApproved.DYEBO_ApprovalDate;
                            }

                            var Cutting = DBDetail.FirstOrDefault(x => x.DYEBO_CutSheet);
                            if (Cutting != null)
                            {
                                nr.DyeCut = true;

                                var CuttingDetails = context.TLCUT_CutSheet.FirstOrDefault(x => x.TLCutSH_DyeBatch_FK == DyeBatch.DYEB_Pk);
                                if (CuttingDetails != null)
                                {
                                    var CutReceipt = context.TLCUT_CutSheetReceipt.FirstOrDefault(x => x.TLCUTSHR_CutSheet_FK == CuttingDetails.TLCutSH_Pk);
                                    if (CutReceipt != null)
                                    {
                                        nr.DateCut = (DateTime)CutReceipt.TLCUTSHR_DateIntoPanelStore;
                                    }

                                    var CMTDetails = context.TLCMT_LineIssue.FirstOrDefault(x => x.TLCMTLI_CutSheet_FK == CuttingDetails.TLCutSH_Pk);
                                    if (CMTDetails != null)
                                    {
                                        nr.DyeCMT = true;
                                        nr.DateCMT = (DateTime)CMTDetails.TLCMTLI_Date;
                                    }
                                }
                            }
                        }


                        nr.DyeBatchColour = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;
                        nr.DyeBatchQuality = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;


                        datatable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(datatable1);
                DyeBatchStatus dbs = new DyeBatchStatus();
                TextObject text = (TextObject)dbs.ReportDefinition.Sections["Section1"].ReportObjects["Text15"];
                text.Text = "Dye Batch Status Report  From " + _parms.FromDate.ToString("dd/MM/yyyy") + " To " + _parms.ToDate.ToString("dd/MM/yyyy");

                dbs.SetDataSource(ds);

                //================================================================
                crystalReportViewer1.ReportSource = dbs;
            }
            else if (_RepNo == 40) // Dyeorder Status Report
            {
                DataSet ds = new DataSet();
                DataSet41.TLDYE_DyeOrderDataTable DyeOrder = new DataSet41.TLDYE_DyeOrderDataTable();
                DataSet41.TLDYE_DyeBatchDataTable DyeBatch = new DataSet41.TLDYE_DyeBatchDataTable();
                DataSet41.TLDYE_DyeBatchDetailsDataTable DyeBatchDetails = new DataSet41.TLDYE_DyeBatchDetailsDataTable();
                _repo = new DyeRepository();

                using (var context = new TTI2Entities())
                {
                    var DO = context.TLDYE_DyeOrder.Find(_Pk);
                    if (DO != null)
                    {
                        DataSet41.TLDYE_DyeOrderRow dor = DyeOrder.NewTLDYE_DyeOrderRow();
                        dor.TLDYO_Closed = DO.TLDYO_Closed;
                        dor.TLDYO_CMTPLoss = DO.TLDYO_CMTPLoss;
                        dor.TLDYO_CMTReqWeek = DO.TLDYO_CMTReqWeek;
                        dor.TLDYO_Colour_FK = DO.TLDYO_Colour_FK;
                        dor.TLDYO_Customer_FK = DO.TLDYO_Customer_FK;
                        dor.TLDYO_CutPLoss = DO.TLDYO_CutPLoss;
                        dor.TLDYO_CutReqWeek = DO.TLDYO_CutReqWeek;
                        dor.TLDYO_DyeOrderNum = DO.TLDYO_DyeOrderNum;
                        dor.TLDYO_DyePLoss = DO.TLDYO_DyePLoss;
                        dor.TLDYO_DyeReqWeek = DO.TLDYO_DyeReqWeek;
                        dor.TLDYO_GarmOrFab = DO.TLDYO_GarmOrFab;
                        dor.TLDYO_Greige_FK = DO.TLDYO_Greige_FK;
                        dor.TLDYO_Label_FK = DO.TLDYO_Label_FK;
                        dor.TLDYO_Notes = DO.TLDYO_Notes;
                        dor.TLDYO_OrderDate = DO.TLDYO_OrderDate;
                        dor.TLDYO_OrderNum = DO.TLDYO_OrderNum;
                        dor.TLDYO_Pk = DO.TLDYO_Pk;
                        dor.TLDYO_Style_FK = DO.TLDYO_Style_FK;

                        DyeOrder.AddTLDYE_DyeOrderRow(dor);

                        var DB = context.TLDYE_DyeBatch.Where(x => x.DYEB_DyeOrder_FK == DO.TLDYO_Pk).ToList();

                        foreach (var Record in DB)
                        {
                            DataSet41.TLDYE_DyeBatchRow dbr = DyeBatch.NewTLDYE_DyeBatchRow();
                            dbr.DYEB_Allocated = Record.DYEB_Allocated;
                            dbr.DYEB_BatchDate = (DateTime)Record.DYEB_BatchDate;
                            dbr.DYEB_BatchKG = Record.DYEB_BatchKG;
                            dbr.DYEB_BatchNo = Record.DYEB_BatchNo;
                            dbr.DYEB_Closed = Record.DYEB_Closed;
                            dbr.DYEB_Colour_FK = Record.DYEB_Colour_FK;
                            dbr.DYEB_CommissinCust = Record.DYEB_CommissinCust;
                            dbr.DYEB_Customer_FK = Record.DYEB_Customer_FK;
                            dbr.DYEB_DyeOrder_FK = Record.DYEB_DyeOrder_FK;
                            dbr.DYEB_FabricMode = Record.DYEB_FabricMode;
                            dbr.DYEB_Greige_FK = Record.DYEB_Greige_FK;
                            dbr.DYEB_Lab = Record.DYEB_Lab;
                            dbr.DYEB_Notes = Record.DYEB_Notes;
                            dbr.DYEB_OriginalBatch_FK = Record.DYEB_OriginalBatch_FK;
                            dbr.DYEB_OutProcess = Record.DYEB_OutProcess;
                            if (Record.DYEB_OutProcessDate != null)
                                dbr.DYEB_OutProcessDate = (DateTime)Record.DYEB_OutProcessDate;
                            dbr.DYEB_Pk = Record.DYEB_Pk;
                            dbr.DYEB_Reprocess = Record.DYEB_Reprocess;
                            if (Record.DYEB_RequiredDate != null)
                                dbr.DYEB_RequiredDate = (DateTime)Record.DYEB_RequiredDate;
                            dbr.DYEB_SequenceNo = Record.DYEB_SequenceNo;
                            dbr.DYEB_Stage1 = Record.DYEB_Stage1;
                            dbr.DYEB_Stage2 = Record.DYEB_Stage2;
                            dbr.DYEB_Stage3 = Record.DYEB_Stage3;
                            dbr.DYEB_TransactionType_FK = Record.DYEB_TransactionType_FK;
                            if (Record.DYEB_TransferDate != null)
                                dbr.DYEB_TransferDate = (DateTime)Record.DYEB_TransferDate;
                            dbr.DYEB_Transfered = Record.DYEB_Transfered;
                            dbr.DYEB_Wrap = Record.DYEB_Wrap;

                            DyeBatch.AddTLDYE_DyeBatchRow(dbr);

                            var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == Record.DYEB_Pk).ToList();
                            foreach (var Detail in DBD)
                            {
                                DataSet41.TLDYE_DyeBatchDetailsRow dbd = DyeBatchDetails.NewTLDYE_DyeBatchDetailsRow();
                                dbd.DYEBD_BodyTrim = Detail.DYEBD_BodyTrim;
                                dbd.DYEBD_DyeBatch_FK = Detail.DYEBD_DyeBatch_FK;
                                dbd.DYEBD_DyeOrderDet_FK = Detail.DYEBD_DyeOrderDet_FK;
                                dbd.DYEBD_GreigeProduction_FK = Detail.DYEBD_GreigeProduction_FK;
                                dbd.DYEBD_GreigeProduction_Weight = Detail.DYEBD_GreigeProduction_Weight;
                                dbd.DYEBD_Pk = Detail.DYEBD_Pk;
                                dbd.DYEBD_QualityKey = Detail.DYEBD_QualityKey;
                                dbd.DYEBO_AdjustedWeight = Detail.DYEBO_AdjustedWeight;
                                if (Detail.DYEBO_ApprovalDate != null)
                                    dbd.DYEBO_ApprovalDate = (DateTime)Detail.DYEBO_ApprovalDate;
                                dbd.DYEBO_CurrentStore_FK = Detail.DYEBO_CurrentStore_FK;
                                dbd.DYEBO_CutSheet = Detail.DYEBO_CutSheet;
                                if (Detail.DYEBO_DateSold != null)
                                    dbd.DYEBO_DateSold = (DateTime)Detail.DYEBO_DateSold;
                                dbd.DYEBO_DiskWeight = Detail.DYEBO_DiskWeight;
                                if (Detail.DYEBO_DyeDate != null)
                                    dbd.DYEBO_DyeDate = (DateTime)Detail.DYEBO_DyeDate;
                                dbd.DYEBO_GVRowNumber = Detail.DYEBO_GVRowNumber;
                                dbd.DYEBO_Meters = Detail.DYEBO_Meters;
                                dbd.DYEBO_Nett = Detail.DYEBO_Nett;
                                dbd.DYEBO_ProductRating = Detail.DYEBO_ProductRating_FK;
                                dbd.DYEBO_QAApproved = Detail.DYEBO_QAApproved;
                                dbd.DYEBO_Rejected = Detail.DYEBO_Rejected;
                                if (Detail.DYEBO_RejectedDate != null)
                                    dbd.DYEBO_RejectedDate = (DateTime)Detail.DYEBO_RejectedDate;
                                dbd.DYEBO_Sold = Detail.DYEBO_Sold;
                                dbd.DYEBO_TransactionNo = Detail.DYEBO_TransactionNo;
                                if (Detail.DYEBO_TransDate != null)
                                    dbd.DYEBO_TransDate = (DateTime)Detail.DYEBO_TransDate;
                                if (Detail.DYEBO_TransferPrinted != null)
                                    dbd.DYEBO_TransferPrinted = (bool)Detail.DYEBO_TransferPrinted;
                                dbd.DYEBO_TrimKey = (int)Detail.DYEBO_TrimKey;
                                dbd.DYEBO_Width = Detail.DYEBO_Width;
                                dbd.DYEBO_WriteOff = Detail.DYEBO_WriteOff;

                                DyeBatchDetails.AddTLDYE_DyeBatchDetailsRow(dbd);
                            }
                        }
                    }
                }

                ds.Tables.Add(DyeOrder);
                ds.Tables.Add(DyeBatch);
                ds.Tables.Add(DyeBatchDetails);

                DyeOrderView dbs = new DyeOrderView();
                dbs.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbs;
            }
            else if (_RepNo == 41)
            {
                DataSet ds = new DataSet();
                DataSet44.DataTable1DataTable dataTable1 = new DataSet44.DataTable1DataTable();
                DataSet44.DataTable2DataTable dataTable2 = new DataSet44.DataTable2DataTable();
                _repo = new DyeRepository();

                IList<TLDYE_DefinitionDetails> _DefinitionDetails = null;
                IList<TLDYE_RecipeDefinition> _Definition = null;

                using (var context = new TTI2Entities())
                {
                    _Definition = context.TLDYE_RecipeDefinition.ToList();
                    _DefinitionDetails = context.TLDYE_DefinitionDetails.ToList();

                    var Consummables = _repo.SelectConsumables(_parms);
                    foreach (var Consummable in Consummables)
                    {
                        //1st Decision --- Find it on the definition Details Tables 
                        //==============================================================
                        var DefinitionDetails = _DefinitionDetails.Where(x => x.TLDYED_Cosumables_FK == Consummable.ConsDC_Pk).ToList();
                        foreach (var DefinitionDetail in DefinitionDetails)
                        {
                            //2nd Decision --- Find it on the definition Tables 
                            //===========================================================
                            var Receipes = _Definition.FirstOrDefault(s => s.TLDYE_DefinePk == DefinitionDetail.TLDYED_Receipe_FK);
                            if (Receipes != null)
                            {
                                DataSet44.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Pk = 1;
                                nr.Code = Consummable.ConsDC_Description;
                                nr.ReceipeNo = Receipes.TLDYE_DefineDescription;
                                nr.LIQCALC = DefinitionDetail.TLDYED_LiqCalc;
                                nr.LIQRATIO = DefinitionDetail.TLDYED_LiqRatio;
                                nr.MELFC = DefinitionDetail.TLDYED_MELFC;

                                dataTable1.AddDataTable1Row(nr);

                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                //=============================================================
                DataSet44.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                xnr.Pk = 1;
                xnr.FromDate = _parms.FromDate;
                xnr.ToDate = _parms.ToDate;


                if (dataTable1.Rows.Count == 0)
                {
                    xnr.ErrorLog = "No Data found pertaining to selection made";
                }
                dataTable2.AddDataTable2Row(xnr);
                //============================================================================
                ds.Tables.Add(dataTable2);
                ReceipeUsage dbs = new ReceipeUsage();
                dbs.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbs;
            }
            else if (_RepNo == 42)
            {
                DataSet ds = new DataSet();
                DataSet45.DataTable1DataTable dataTable1 = new DataSet45.DataTable1DataTable();
                DataSet45.DataTable2DataTable dataTable2 = new DataSet45.DataTable2DataTable();
                _repo = new DyeRepository();


                IList<TLDYE_DyeBatch> DBatches = null;

                IList<TLADM_Colours> _Colours = null; ;
                IList<TLADM_MachineDefinitions> _Machines;

                //=============================================================
                DataSet45.DataTable1Row xnr = dataTable1.NewDataTable1Row();
                xnr.Pk = 1;
                xnr.FromDate = _parms.FromDate;
                xnr.ToDate = _parms.ToDate;

                if (_parms.ProdWIP)
                    xnr.Title = "Dye House Production (WIP) for the period from";
                else
                    xnr.Title = "Dye House Production (Completed) for the period from";

                dataTable1.AddDataTable1Row(xnr);

                using (var context = new TTI2Entities())
                {
                    _Colours = context.TLADM_Colours.ToList();
                    _Machines = context.TLADM_MachineDefinitions.ToList();

                    if (_parms.ProdWIP)
                    {
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_Allocated && x.DYEB_BatchDate >= _parms.FromDate && x.DYEB_BatchDate <= _parms.ToDate && !x.DYEB_Stage1).ToList();
                    }
                    else
                    {
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_Allocated && x.DYEB_BatchDate >= _parms.FromDate && x.DYEB_BatchDate <= _parms.ToDate && x.DYEB_Stage1).ToList();
                    }

                    foreach (var DBatch in DBatches)
                    {
                        DataSet45.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Pk = 1;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == DBatch.DYEB_Colour_FK).Col_Display;
                        var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DBatch.DYEB_Pk).FirstOrDefault();
                        if (Allocated != null)
                        {
                            nr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == Allocated.TLDYEA_MachCode_FK).MD_MachineCode;
                        }
                        nr.BatchWeight = DBatch.DYEB_BatchKG;

                        dataTable2.AddDataTable2Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    dataTable1.FirstOrDefault().ErrorLog = "No Data found pertaining to selection made";
                }

                ds.Tables.Add(dataTable2);
                DyeProd dbs = new DyeProd();

                dbs.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbs;

            }
            else if (_RepNo == 43)
            {
                DataSet ds = new DataSet();
                DataSet46.DataTable1DataTable dataTable1 = new DataSet46.DataTable1DataTable();
                DataSet46.DataTable2DataTable dataTable2 = new DataSet46.DataTable2DataTable();

                core = new Util();
                IList<TLADM_Styles> _Styles = null;
                IList<TLADM_Colours> _Colours = null;

                IList<TLDYE_DyeBatch> DBatches = null;

                using (var context = new TTI2Entities())
                {
                    _Styles = context.TLADM_Styles.ToList();
                    _Colours = context.TLADM_Colours.ToList();

                    if (_parms.CalculateProdResults)
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_RequiredDate >= _parms.FromDate && x.DYEB_RequiredDate <= _parms.ToDate && x.DYEB_Allocated && x.DYEB_OutProcess && !x.DYEB_CommissinCust).ToList();
                    else
                        DBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_RequiredDate >= _parms.FromDate && x.DYEB_RequiredDate <= _parms.ToDate && !x.DYEB_OutProcess && !x.DYEB_CommissinCust).ToList();

                    foreach (var DBatch in DBatches)
                    {
                        DataSet46.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.DB_Pk = 1;
                        nr.DB_BatchNo = DBatch.DYEB_BatchNo;
                        nr.DB_Required_Date = (DateTime)DBatch.DYEB_RequiredDate;
                        nr.DB_Kgs = DBatch.DYEB_BatchKG;

                        var DyeOrder = context.TLDYE_DyeOrder.Find(DBatch.DYEB_DyeOrder_FK);
                        if (DyeOrder != null)
                        {
                            nr.DB_Style = _Styles.FirstOrDefault(s => s.Sty_Id == DyeOrder.TLDYO_Style_FK).Sty_Description;
                            nr.DB_Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeOrder.TLDYO_Colour_FK).Col_Display;

                        }
                        if (_parms.CalculateProdResults)
                        {
                            if (DBatch.DYEB_OutProcess)
                            {
                                var DBDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DBatch.DYEB_Pk).FirstOrDefault();
                                if (DBDetail != null && DBDetail.DYEBO_QAApproved)
                                {
                                    TimeSpan TS = (TimeSpan)DBDetail.DYEBO_ApprovalDate.Value.Subtract((DateTime)DBatch.DYEB_RequiredDate);
                                    nr.DB_Completed_Date = (DateTime)DBDetail.DYEBO_ApprovalDate;
                                    nr.DB_Days = core.GetWorkingDays((DateTime)DBatch.DYEB_RequiredDate, (DateTime)DBDetail.DYEBO_ApprovalDate);

                                }
                            }
                        }

                        dataTable2.AddDataTable2Row(nr);
                    }
                }

                if (dataTable2.Rows.Count == 0)
                {
                    DataSet46.DataTable2Row nr = dataTable2.NewDataTable2Row();
                    nr.DB_Pk = 1;
                    nr.DB_ErrorLog = "No data found matching selection made";
                    dataTable2.AddDataTable2Row(nr);

                }
                DataSet46.DataTable1Row T1Row = dataTable1.NewDataTable1Row();
                T1Row.DB_Pk = 1;
                T1Row.DBFrom_Date = _parms.FromDate;
                T1Row.DBTO_Date = _parms.ToDate;

                if (_parms.CalculateProdResults)
                {
                    T1Row.DBTitle = "Dye Batch Production Results in days from ";
                }
                else
                {
                    T1Row.DBTitle = "Dye Batch Production Targets from ";
                }
                dataTable1.AddDataTable1Row(T1Row);


                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                DBProductionResults dbs = new DBProductionResults();
                dbs.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbs;
            }
            else if (_RepNo == 44)
            {
                DataSet ds = new DataSet();
                DataSet47.DataTable1DataTable dataTable1 = new DataSet47.DataTable1DataTable();
                DataSet47.DataTable2DataTable dataTable2 = new DataSet47.DataTable2DataTable();

                IList<TLADM_Griege> _Quality = null;
                IList<TLADM_Colours> _Colours = null;
                IList<TLADM_MachineDefinitions> _Machines = null;
                IList<TLADM_QADyeProcessFields> _ProcessFields = null;

                int Dye_Pk = 0;
                string Quality = string.Empty;
                string Colour = string.Empty;
                string DyeMachine = string.Empty;
                String KnittingMachine = string.Empty; ;
                String Cause = string.Empty;
                TLDYE_DyeBatch DyeBatch = null;

                using (var context = new TTI2Entities())
                {
                    _Quality = context.TLADM_Griege.ToList();
                    _Colours = context.TLADM_Colours.ToList();
                    _Machines = context.TLADM_MachineDefinitions.ToList();
                    _ProcessFields = context.TLADM_QADyeProcessFields.ToList();

                    var Entries = context.TLDye_QualityException.Where(x => x.TLDyeIns_TransactionDate >= _parms.FromDate && x.TLDyeIns_TransactionDate <= _parms.ToDate).ToList();
                    foreach (var Entry in Entries)
                    {
                        DataSet47.DataTable2Row xr = dataTable2.NewDataTable2Row();
                        xr.Pk = 1;
                        xr.Transaction_Date = Entry.TLDyeIns_TransactionDate;

                        // /*if (Dye_Pk != Entry.TLDyeIns_DyeBatch_Fk)
                        // {
                        DyeBatch = context.TLDYE_DyeBatch.Find(Entry.TLDyeIns_DyeBatch_Fk);
                        if (DyeBatch != null)
                        {
                            Dye_Pk = Entry.TLDyeIns_DyeBatch_Fk;
                            Cause = DyeBatch.DYEB_QExceptionCause;

                            Quality = _Quality.FirstOrDefault(s => s.TLGreige_Id == DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                            Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeBatch.DYEB_Colour_FK).Col_Display;
                            var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                            if (Allocated != null)
                            {
                                DyeMachine = _Machines.FirstOrDefault(s => s.MD_Pk == Allocated.TLDYEA_MachCode_FK).MD_Description;
                            }
                            else
                                DyeMachine = string.Empty;

                            var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                            if (DyeBatchDetail != null)
                            {
                                var GriegeProd = context.TLKNI_GreigeProduction.Find(DyeBatchDetail.DYEBD_GreigeProduction_FK);
                                if (GriegeProd != null && GriegeProd.GreigeP_Machine_FK != null)
                                {
                                    KnittingMachine = context.TLADM_MachineDefinitions.Find(GriegeProd.GreigeP_Machine_FK).MD_Description;
                                }
                                else
                                    KnittingMachine = string.Empty;
                            }
                        }
                        // }


                        xr.DyeBatch = DyeBatch.DYEB_BatchNo;//
                        xr.Quality = Quality;
                        xr.Colour = Colour;
                        xr.DyeMachine = DyeMachine;
                        xr.KnittingMachine = KnittingMachine;
                        xr.ConcernQty = Entry.TLDyeIns_Quantity;
                        xr.ConcernFault = _ProcessFields.FirstOrDefault(s => s.TLQADPF_Pk == Entry.TLDyeIns_QADyeProcessField_Fk).TLQADPF_Description;
                        xr.Cause = Cause;


                        dataTable2.AddDataTable2Row(xr);
                    }
                }

                DataSet47.DataTable1Row hr = dataTable1.NewDataTable1Row();
                hr.Title = "Dyeing QA Quality Exception Report";
                hr.FromDate = _parms.FromDate;
                hr.ToDate = _parms.ToDate;
                hr.Pk = 1;
                dataTable1.AddDataTable1Row(hr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                DyeQAException dbs = new DyeQAException();
                dbs.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbs;

            }
            else if (_RepNo == 45)
            {
                DataSet ds = new DataSet();
                DataSet48.DataTable1DataTable dataTable1 = new DataSet48.DataTable1DataTable();
                core = new Util();

                using (var context = new TTI2Entities())
                {
                    var BatchesOnHold = context.TLDYE_DyeBatch.Where(x => x.DYEB_OnHold).OrderBy(x => x.DYEB_BatchNo).ToList();

                    foreach (var Batch in BatchesOnHold)
                    {
                        DataSet48.DataTable1Row nr = dataTable1.NewDataTable1Row();

                        nr.DyeBatchDate = (DateTime)Batch.DYEB_OnHold_Date;
                        nr.DyeBatchNo = Batch.DYEB_BatchNo;
                        nr.DyeBatchWeight = Batch.DYEB_BatchKG;
                        nr.Reason = Batch.DYEB_OnHold_Reason;
                        nr.DaysOnHold = core.GetWorkingDays((DateTime)Batch.DYEB_OnHold_Date, DateTime.Now);
                        nr.Quality = context.TLADM_Griege.Find(Batch.DYEB_Greige_FK).TLGreige_Description;

                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet48.DataTable1Row nr = dataTable1.NewDataTable1Row();

                    nr.ErrorLog = "There are no Dye Batches on hold";
                    dataTable1.AddDataTable1Row(nr);
                }
                ds.Tables.Add(dataTable1);

                DbOnHold dbOnHold = new DbOnHold();
                dbOnHold.SetDataSource(ds);
                crystalReportViewer1.ReportSource = dbOnHold;


            }
            else if (_RepNo == 46)
            {
                DataSet ds = new DataSet();
                DataSet50.DataTable1DataTable dataTable1 = new DataSet50.DataTable1DataTable();
                DataSet50.DataTable2DataTable dataTable2 = new DataSet50.DataTable2DataTable();
                core = new Util();
                using (var context = new TTI2Entities())
                {
                    DataSet50.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                    var DyeBatch = context.TLDYE_DyeBatch.Find(_Pk);
                    if (DyeBatch != null)
                    {
                        NewRow.Pk = 1;
                        var Colour = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK);
                        if ((bool)Colour.Col_Padding)
                        {
                            NewRow.Colour = Colour.Col_Display + " Padding : Yes";

                        }
                        else
                        {
                            NewRow.Colour = Colour.Col_Display + " Padding : No";

                        }
                        NewRow.Quality = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                        NewRow.DyeBatch = DyeBatch.DYEB_BatchNo;
                    }
                    dataTable1.Rows.Add(NewRow);

                    DataSet50.DataTable2Row NRow = dataTable2.NewDataTable2Row();

                    NRow = dataTable2.NewDataTable2Row();
                    NRow.Pk = 1;
                    NRow.Description = "No of Pieces";
                    NRow.Section = 1;
                    NRow.SectionDescription = "Hydro";
                    dataTable2.Rows.Add(NRow);

                    var HydroMeasurements = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Hydro && !(bool)x.TLQAPF_Padding).ToList();
                    foreach (var Meas in HydroMeasurements)
                    {
                        NRow = dataTable2.NewDataTable2Row();
                        NRow.Pk = 1;
                        NRow.Description = Meas.TLQADPF_Description;
                        NRow.Standard = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_QAProccessField_FK == Meas.TLQADPF_Pk && x.DyeStan_Quality_FK == DyeBatch.DYEB_Greige_FK).FirstOrDefault().DyeStan_Value.ToString();
                        if (Meas.TLQAPF_Operator_Ins)
                        {
                            NRow.Description += " ****";
                        }
                        NRow.Section = 1;
                        NRow.SectionDescription = "Hydro";
                        dataTable2.Rows.Add(NRow);
                    }
                    var DryerMeasurements = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Drier).ToList();
                    foreach (var Meas in DryerMeasurements)
                    {
                        NRow = dataTable2.NewDataTable2Row();
                        NRow.Pk = 1;
                        NRow.Description = Meas.TLQADPF_Description;
                        if (Meas.TLQAPF_Operator_Ins)
                        {
                            NRow.Description += " ****";
                        }
                        NRow.Standard = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_QAProccessField_FK == Meas.TLQADPF_Pk && x.DyeStan_Quality_FK == DyeBatch.DYEB_Greige_FK).FirstOrDefault().DyeStan_Value.ToString();
                        NRow.Section = 2;
                        NRow.SectionDescription = "Drier";
                        dataTable2.Rows.Add(NRow);
                    }

                    DryerMeasurements = context.TLADM_QADyeProcessFields.Where(x => x.TLQAPF_Compactor || x.TLQADPF_Process_FK == 3).ToList();
                    foreach (var Meas in DryerMeasurements)
                    {
                        NRow = dataTable2.NewDataTable2Row();
                        NRow.Pk = 1;
                        NRow.Description = Meas.TLQADPF_Description;
                        if (Meas.TLQAPF_Operator_Ins)
                        {
                            NRow.Description += " ****";
                        }

                        var Standard = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_QAProccessField_FK == Meas.TLQADPF_Pk && x.DyeStan_Quality_FK == DyeBatch.DYEB_Greige_FK).FirstOrDefault();
                        if (Standard != null)
                        {
                            NRow.Standard = Standard.DyeStan_Value.ToString();
                        }
                        NRow.Section = 3;
                        NRow.SectionDescription = "Compacter";
                        dataTable2.Rows.Add(NRow);
                    }

                    NRow = dataTable2.NewDataTable2Row();
                    NRow.Pk = 1;
                    NRow.Description = "Piece";
                    NRow.Standard = "Width";
                    NRow.QCColour = "QC Colour";
                    NRow.QCWidth = "QC Width";
                    NRow.Cutting = "Cutting";
                    NRow.Section = 4;
                    dataTable2.Rows.Add(NRow);

                    var Pieces = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).ToList();
                    foreach (var Piece in Pieces)
                    {
                        NRow = dataTable2.NewDataTable2Row();
                        NRow.Pk = 1;
                        NRow.Description = context.TLKNI_GreigeProduction.Find(Piece.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                        NRow.Section = 4;

                        dataTable2.Rows.Add(NRow);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                QualityAssuranceCheck QualAssurance = new QualityAssuranceCheck();
                QualAssurance.SetDataSource(ds);
                crystalReportViewer1.ReportSource = QualAssurance;
            }
            else if (_RepNo == 47)
            {
                DataSet ds = new DataSet();
                DataSet51.DataTable1DataTable dataTable1 = new DataSet51.DataTable1DataTable();
                DataSet51.DataTable2DataTable dataTable2 = new DataSet51.DataTable2DataTable();
                core = new Util();
                var TransNumber = int.Parse(_TransNumber);

                using (var context = new TTI2Entities())
                {
                    DataSet51.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                    NewRow.Pk = 1;
                    NewRow.WareHouse = context.TLADM_WhseStore.Find(_parms.Consumable_Whse_FK).WhStore_Description;
                    NewRow.Transdate = DateTime.Now;
                    NewRow.TransNumber = TransNumber.ToString();
                    dataTable1.Rows.Add(NewRow);

                    var Consumables = context.TLDYE_ConSummableReceived.Where(x => x.DYECON_TransNumber == TransNumber).ToList();

                    foreach (var Consumable in Consumables)
                    {
                        DataSet51.DataTable2Row NRow = dataTable2.NewDataTable2Row();
                        NRow.Pk = 1;
                        NRow.Consumable = context.TLADM_ConsumablesDC.Find(Consumable.DYECON_Consumable_FK).ConsDC_Description;
                        NRow.OrderNo = Consumable.DYECON_OrderNo;
                        NRow.Supplier = context.TLADM_Suppliers.Find(Consumable.DYECON_Supplier_FK).Sup_Description;
                        NRow.ContainetId = Consumable.DYECON_ContainerId;
                        NRow.UOM = context.TLADM_UOM.Find(Consumable.DYECON_UOM_FK).UOM_Description;
                        NRow.Volume = Consumable.DYECON_Amount;
                        dataTable2.Rows.Add(NRow);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                DyeHouse.ConsumableGRN QualAssurance = new DyeHouse.ConsumableGRN();
                QualAssurance.SetDataSource(ds);
                crystalReportViewer1.ReportSource = QualAssurance;

            }
            else if (_RepNo == 48)
            {
                DataSet ds = new DataSet();
                DataSet52.DataTable1DataTable dataTable1 = new DataSet52.DataTable1DataTable();
                DataSet52.DataTable2DataTable dataTable2 = new DataSet52.DataTable2DataTable();
                core = new Util();
                _repo = new DyeHouse.DyeRepository();

                DataSet52.DataTable2Row NewRow = dataTable2.NewDataTable2Row();
                NewRow.Pk = 1;
                NewRow.FromDate = _parms.FromDate;
                NewRow.ToDate = _parms.ToDate;
                NewRow.Title = "Fabric width movement through finishing processes";
                dataTable2.Rows.Add(NewRow);

                using (var context = new TTI2Entities())
                {
                    var DyeBatches = _repo.SelectDyedBasicQuality(_parms);

                    foreach (var DBatch in DyeBatches)
                    {
                        var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DBatch.DYEB_Pk && x.DYEBD_BodyTrim).ToList();

                        var AfterHydro = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == DBatch.DYEB_Pk && x.TLDYEDC_NCStage == 6).FirstOrDefault();
                        var AfterDrying = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == DBatch.DYEB_Pk && x.TLDYEDC_NCStage == 4 && x.TLDYEDC_Code_FK == 1).FirstOrDefault();
                        var AfterCompacting = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == DBatch.DYEB_Pk && x.TLDYEDC_NCStage == 5 && x.TLDYEDC_Code_FK == 1).FirstOrDefault();

                        foreach (var Detail in DyeBatchDetails)
                        {
                            DataSet52.DataTable1Row NRow = dataTable1.NewDataTable1Row();
                            NRow.Pk = 1;
                            var GreigeProd = context.TLKNI_GreigeProduction.Find(Detail.DYEBD_GreigeProduction_FK);
                            if (GreigeProd == null)
                            {
                                continue;
                            }

                            NRow.DyeBatch = DBatch.DYEB_BatchNo;
                            NRow.PieceNo = GreigeProd.GreigeP_PieceNo;
                            var Quality = context.TLADM_Griege.Find(DBatch.DYEB_Greige_FK);
                            if (Quality != null)
                            {
                                NRow.Quality = Quality.TLGreige_Description;
                                var FabWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                if (FabWidth != null)
                                {
                                    NRow.GreigeWidth = FabWidth.FW_Calculation_Value;
                                }
                            }

                            NRow.Quality = context.TLADM_Griege.Find(DBatch.DYEB_Greige_FK).TLGreige_Description;
                            NRow.Colour = context.TLADM_Colours.Find(DBatch.DYEB_Colour_FK).Col_Display;
                            NRow.GrossKg = Detail.DYEBD_GreigeProduction_Weight;
                            NRow.NetKg = Detail.DYEBO_Nett;

                            if (NRow.GrossKg != 0 & NRow.NetKg != 0)
                            {
                                NRow.ProccesLoss = core.CalculateVariance(NRow.GrossKg, NRow.NetKg);
                            }
                            else
                            {
                                NRow.ProccesLoss = 0;
                            }

                            if (_parms.ProcessLoss != 0)
                            {
                                if (NRow.ProccesLoss > (_parms.ProcessLoss * -1) && NRow.ProccesLoss < _parms.ProcessLoss)
                                {
                                    continue;
                                }
                            }

                            if (AfterHydro != null && AfterHydro.TLDYEDC_Value != 0)
                            {
                                NRow.GreigeWidth_AfterHydro = AfterHydro.TLDYEDC_Value;
                                NRow.WidthMove_AfterHydro = core.CalculateVariance(NRow.GreigeWidth, AfterHydro.TLDYEDC_Value);
                            }
                            else
                            {
                                NRow.GreigeWidth_AfterHydro = 0;
                                NRow.WidthMove_AfterHydro = 0;
                            }

                            if (AfterDrying != null && AfterDrying.TLDYEDC_Value != 0 && NRow.GreigeWidth_AfterHydro != 0)
                            {
                                NRow.GreigeWidth_AfterDrying = AfterDrying.TLDYEDC_Value;
                                NRow.WidthMove_AfterDrying = core.CalculateVariance(NRow.GreigeWidth_AfterHydro, AfterHydro.TLDYEDC_Value);
                            }
                            else
                            {
                                NRow.GreigeWidth_AfterDrying = 0;
                                NRow.WidthMove_AfterDrying = 0;
                            }

                            if (AfterCompacting != null && NRow.GreigeWidth_AfterDrying != 0 && AfterCompacting.TLDYEDC_Value != 0)
                            {
                                NRow.GreigeWidth_AfterCompact = AfterCompacting.TLDYEDC_Value;
                                NRow.WidthMove_AfterCompact = core.CalculateVariance(NRow.GreigeWidth_AfterHydro, AfterCompacting.TLDYEDC_Value);
                                NRow.WidthMove_AfterGreige = core.CalculateVariance(NRow.GrossKg, AfterCompacting.TLDYEDC_Value);
                            }
                            else
                            {
                                NRow.GreigeWidth_AfterCompact = Detail.DYEBO_Width;
                                NRow.WidthMove_AfterCompact = 0;
                                NRow.WidthMove_AfterGreige = core.CalculateVariance(NRow.GreigeWidth, Detail.DYEBO_Width); ;
                            }
                            if (_parms.WidthMagnitude != 0)
                            {
                                if (NRow.WidthMove_AfterGreige > (_parms.WidthMagnitude * -1) && NRow.WidthMove_AfterGreige < _parms.WidthMagnitude)
                                {
                                    continue;
                                }
                            }
                            var AllocatedOp = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DBatch.DYEB_Pk).FirstOrDefault();
                            if (AllocatedOp != null)
                            {
                                NRow.DyeOperator = context.TLADM_MachineOperators.Find(AllocatedOp.DYEOP_Operator_FK).MachOp_Description;
                            }

                            var AllocatedMachine = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DBatch.DYEB_Pk).FirstOrDefault();
                            if (AllocatedMachine != null)
                            {
                                NRow.DyeMachine = context.TLADM_MachineDefinitions.Find(AllocatedMachine.TLDYEA_MachCode_FK).MD_Description;
                            }


                            dataTable1.Rows.Add(NRow);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                DyeHouse.FabricWidthThroughProcess ThroughProcess = new DyeHouse.FabricWidthThroughProcess();
                ThroughProcess.SetDataSource(ds);

                /*reportOptions = new BindingList<KeyValuePair<int, string>>();
                reportOptions.Add(new KeyValuePair<int, string>(0, "Batch Number "));
                reportOptions.Add(new KeyValuePair<int, string>(1, "Dye Machines"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Dye Operator"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Process loss magnitude"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "Width difference to fabric final")); */

                if (_parms.DO_OptionSelected == 0)
                {
                    //Dye Batch Number
                    ThroughProcess.DataDefinition.Groups[0].ConditionField = ThroughProcess.Database.Tables[0].Fields[1];
                }
                else if (_parms.DO_OptionSelected == 1)
                {
                    // Dye Machine
                    ThroughProcess.DataDefinition.Groups[0].ConditionField = ThroughProcess.Database.Tables[0].Fields[17];
                }
                else if (_parms.DO_OptionSelected == 2)
                {
                    //Operator
                    ThroughProcess.DataDefinition.Groups[0].ConditionField = ThroughProcess.Database.Tables[0].Fields[16];
                }
                else if (_parms.DO_OptionSelected == 3)
                {
                    // Process Loss Magnitude
                    ThroughProcess.DataDefinition.Groups[0].ConditionField = ThroughProcess.Database.Tables[0].Fields[7];
                }
                else
                {
                    // Process Width Loss Magnitude
                    ThroughProcess.DataDefinition.Groups[0].ConditionField = ThroughProcess.Database.Tables[0].Fields[8];
                }


                crystalReportViewer1.ReportSource = ThroughProcess;
            }
            else if (_RepNo == 49)
            {
                //DataSet ds = new DataSet();
                //DataSet53.DataTable1DataTable dataTable1 = new DataSet53.DataTable1DataTable();
                //DataSet53.DataTable2DataTable dataTable2 = new DataSet53.DataTable2DataTable();
                //core = new Util();
                //_repo = new DyeHouse.DyeRepository();

                //DataSet53.DataTable2Row NewRow = dataTable2.NewDataTable2Row();
                //NewRow.Pk = 1;
                //NewRow.FromDate = _parms.FromDate;
                //NewRow.ToDate = _parms.ToDate;
                //NewRow.Title = "Fabric Weight movement through finishing processes";
                //dataTable2.Rows.Add(NewRow);

                //using (var context = new TTI2Entities())
                //{
                //    var DyeBatches = _repo.SelectDyedBasicQuality(_parms);

                //    foreach (var DBatch in DyeBatches)
                //    {
                //        var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DBatch.DYEB_Pk && x.DYEBD_BodyTrim).ToList();

                //        foreach (var Detail in DyeBatchDetails)
                //        {
                //            DataSet53.DataTable1Row NRow = dataTable1.NewDataTable1Row();
                //            NRow.Pk = 1;

                //            if (!Detail.DYEBD_BodyTrim)
                //                continue;

                //            var GreigeProd = context.TLKNI_GreigeProduction.Find(Detail.DYEBD_GreigeProduction_FK);
                //            if (GreigeProd == null)
                //            {
                //                continue;
                //            }

                //            NRow.DyeBatch = DBatch.DYEB_BatchNo;
                //            NRow.PieceNo = GreigeProd.GreigeP_PieceNo;
                //            var Quality = context.TLADM_Griege.Find(DBatch.DYEB_Greige_FK);
                //            NRow.StandardDskWeight = 0.0M;

                //            if (Quality != null)
                //            {
                //                NRow.Quality = Quality.TLGreige_Description;
                //                //==============================================================
                //                //Dsk Weight
                //                //========================================
                //                var FWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                //                if (FWeight != null)
                //                {
                //                    NRow.StandardDskWeight = FWeight.FWW_Calculation_Value;
                //                }
                //                var FWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                //                if (FWidth != null)
                //                {
                //                    NRow.StandardWidth = FWidth.FW_Calculation_Value;
                //                }
                //            }

                //            NRow.Colour = context.TLADM_Colours.Find(DBatch.DYEB_Colour_FK).Col_Display;
                //            NRow.GrossKg = Detail.DYEBD_GreigeProduction_Weight;
                //            NRow.NetKg = Detail.DYEBO_Nett;

                //            if (NRow.GrossKg != 0 & NRow.NetKg != 0)
                //            {
                //                NRow.ProccesLoss = Math.Round(core.CalculateVariance(NRow.GrossKg, NRow.NetKg), 4);
                //            }
                //            else
                //            {
                //                NRow.ProccesLoss = 0;
                //            }

                //            if (_parms.ProcessLoss != 0)
                //            {
                //                if (NRow.ProccesLoss > (_parms.ProcessLoss * -1) && NRow.ProccesLoss < _parms.ProcessLoss)
                //                {
                //                    continue;
                //                }
                //            }
                //            //********************************************
                //            // Weight Variance
                //            //*************************************************
                //            NRow.DiskWeight = Detail.DYEBO_DiskWeight;
                //            if (NRow.StandardDskWeight != 0 && NRow.DiskWeight != 0)
                //            {
                //                NRow.WeightVariance = core.CalculateVariance(NRow.StandardDskWeight, NRow.DiskWeight);
                //            }
                //            else
                //            {
                //                NRow.WeightVariance = 0;
                //            }

                //            NRow.AfterProcess = Math.Round(Detail.DYEBO_Width, 1);
                //            //********************************************
                //            // Width Variance
                //            //*************************************************
                //            if (NRow.StandardWidth != 0 && Detail.DYEBO_Width != 0)
                //            {
                //                NRow.WidthVariance = core.CalculateVariance(NRow.StandardWidth, Detail.DYEBO_Width);
                //            }
                //            else
                //            {
                //                NRow.WidthVariance = 0;
                //            }

                //            if (_parms.WidthMagnitude != 0)
                //            {
                //                if (NRow.WidthVariance > (_parms.WidthMagnitude * -1) && NRow.WidthVariance < _parms.WidthMagnitude)
                //                {
                //                    continue;
                //                }
                //            }
                //            var Rating = context.TLADM_ProductRating.Find(Detail.DYEBO_ProductRating_FK).Pr_numeric_Rating;

                //            if (Rating != 0 && Detail.DYEBO_Nett != 0)
                //            {
                //                if (NRow.StandardDskWeight != 0 && NRow.StandardWidth != 0)
                //                {
                //                    var Yield = core.FabricYield(NRow.StandardDskWeight, NRow.StandardWidth);
                //                    NRow.StdExpectQty = Convert.ToInt32((Yield / Rating) * GreigeProd.GreigeP_weightAvail);

                //                    Yield = core.FabricYield(Detail.DYEBO_DiskWeight, Detail.DYEBO_Width);
                //                    NRow.Actual = Convert.ToInt32((Yield / Rating) * Detail.DYEBO_Nett);

                //                    NRow.Difference = NRow.Actual - NRow.StdExpectQty;
                //                    NRow.VariancePercent = core.CalculateVariance(NRow.StdExpectQty, NRow.Actual);
                //                }
                //                else
                //                {
                //                    NRow.StdExpectQty = 0;
                //                    NRow.Actual = 0;
                //                    NRow.Difference = 0;
                //                    NRow.VariancePercent = 0;
                //                }
                //            }
                //            else
                //            {
                //                NRow.StdExpectQty = 0;
                //                NRow.Actual = 0;
                //                NRow.Difference = 0;
                //                NRow.VariancePercent = 0;
                //            }

                //            var AllocatedOp = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DBatch.DYEB_Pk).FirstOrDefault();
                //            if (AllocatedOp != null)
                //            {
                //                NRow.DyeOperator = context.TLADM_MachineOperators.Find(AllocatedOp.DYEOP_Operator_FK).MachOp_Description;
                //            }

                //            var AllocatedMachine = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DBatch.DYEB_Pk).FirstOrDefault();
                //            if (AllocatedMachine != null)
                //            {
                //                NRow.DyeMachine = context.TLADM_MachineDefinitions.Find(AllocatedMachine.TLDYEA_MachCode_FK).MD_Description;
                //            }

                //            var Customer = context.TLADM_CustomerFile.Find(DBatch.DYEB_Customer_FK);
                //            if (Customer != null)
                //            {
                //                NRow.Customer = Customer.Cust_Description;
                //            }

                //            dataTable1.Rows.Add(NRow);
                //        }
                //    }
                //}

                //ds.Tables.Add(dataTable1);
                //ds.Tables.Add(dataTable2);
                //DyeHouse.FabricWeightThroughProcess TProcess = new DyeHouse.FabricWeightThroughProcess();
                //TProcess.SetDataSource(ds);

                //if (_parms.DO_OptionSelected == 0)
                //{
                //    //Dye Batch Number
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[1];
                //}
                //else if (_parms.DO_OptionSelected == 1)
                //{
                //    // Quality
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[3];
                //}
                //else if (_parms.DO_OptionSelected == 2)
                //{
                //    //ODye Machine
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[18];
                //}
                //else if (_parms.DO_OptionSelected == 3)
                //{
                //    // Operator
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[19];
                //}
                //else if (_parms.DO_OptionSelected == 4)
                //{
                //    // Process Loss Magnitude
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[7];
                //}
                //else if (_parms.DO_OptionSelected == 5)
                //{
                //    // Width Difference
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[10];
                //}
                //else if (_parms.DO_OptionSelected == 6)
                //{
                //    // Weight Difference
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[16];
                //}
                //else
                //{
                //    TProcess.DataDefinition.Groups[0].ConditionField = TProcess.Database.Tables[0].Fields[20];
                //}
                //crystalReportViewer1.ReportSource = TProcess;

                DataSet ds = new DataSet();
                DataSet53.DataTable1DataTable dataTable1 = new DataSet53.DataTable1DataTable();
                DataSet53.DataTable2DataTable dataTable2 = new DataSet53.DataTable2DataTable();
                core = new Util();
                _repo = new DyeHouse.DyeRepository();

                DataSet53.DataTable2Row NewRow = dataTable2.NewDataTable2Row();
                NewRow.Pk = 1;
                NewRow.FromDate = _parms.FromDate;
                NewRow.ToDate = _parms.ToDate;
                NewRow.Title = "Fabric Weight movement through finishing processes";
                dataTable2.Rows.Add(NewRow);

                using (var context = new TTI2Entities())
                {
                    var DyeBatches = _repo.SelectDyedBasicQuality(_parms);

                    foreach (var DBatch in DyeBatches)
                    {
                        var DyeBatchDetails = context.TLDYE_DyeBatchDetails
                            .Where(x => x.DYEBD_DyeBatch_FK == DBatch.DYEB_Pk && x.DYEBD_BodyTrim)
                            .ToList();

                        foreach (var Detail in DyeBatchDetails)
                        {
                            DataSet53.DataTable1Row NRow = dataTable1.NewDataTable1Row();
                            NRow.Pk = 1;

                            if (!Detail.DYEBD_BodyTrim)
                                continue;

                            var GreigeProd = context.TLKNI_GreigeProduction.Find(Detail.DYEBD_GreigeProduction_FK);
                            if (GreigeProd == null)
                            {
                                continue;
                            }

                            NRow.DyeBatch = DBatch.DYEB_BatchNo;
                            NRow.PieceNo = GreigeProd.GreigeP_PieceNo;
                            var Quality = context.TLADM_Griege.Find(DBatch.DYEB_Greige_FK);
                            NRow.StandardDskWeight = 0.0M;

                            if (Quality != null)
                            {
                                NRow.Quality = Quality.TLGreige_Description;
                                var FWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                if (FWeight != null)
                                {
                                    NRow.StandardDskWeight = FWeight.FWW_Calculation_Value;
                                }
                                var FWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                if (FWidth != null)
                                {
                                    NRow.StandardWidth = FWidth.FW_Calculation_Value;
                                }
                            }

                            NRow.Colour = context.TLADM_Colours.Find(DBatch.DYEB_Colour_FK).Col_Display;
                            NRow.GrossKg = Detail.DYEBD_GreigeProduction_Weight;
                            NRow.NetKg = Detail.DYEBO_Nett;

                            // Calculate Process Loss
                            if (NRow.GrossKg != 0 && NRow.NetKg != 0)
                            {
                                NRow.ProccesLoss = Math.Round(core.CalculateVariance(NRow.GrossKg, NRow.NetKg), 4);
                            }
                            else
                            {
                                NRow.ProccesLoss = 0;
                            }

                            // Filter based on Process Loss
                            if (_parms.ProcessLoss != 0)
                            {
                                if (NRow.ProccesLoss > (_parms.ProcessLoss * -1) && NRow.ProccesLoss < _parms.ProcessLoss)
                                {
                                    continue;
                                }
                            }

                            NRow.DiskWeight = Detail.DYEBO_DiskWeight;

                            // Calculate Weight Variance
                            if (NRow.StandardDskWeight != 0 && NRow.DiskWeight != 0)
                            {
                                NRow.WeightVariance = core.CalculateVariance(NRow.StandardDskWeight, NRow.DiskWeight);
                            }
                            else
                            {
                                NRow.WeightVariance = 0;
                            }

                            NRow.AfterProcess = Math.Round(Detail.DYEBO_Width, 1);

                            // Calculate Width Variance
                            if (NRow.StandardWidth != 0 && Detail.DYEBO_Width != 0)
                            {
                                NRow.WidthVariance = core.CalculateVariance(NRow.StandardWidth, Detail.DYEBO_Width);
                            }
                            else
                            {
                                NRow.WidthVariance = 0;
                            }

                            // Filter based on Width Magnitude
                            if (_parms.WidthMagnitude != 0)
                            {
                                if (NRow.WidthVariance > (_parms.WidthMagnitude * -1) && NRow.WidthVariance < _parms.WidthMagnitude)
                                {
                                    continue;
                                }
                            }

                            // Calculate Yield and Variances
                            var Rating = context.TLADM_ProductRating.Find(Detail.DYEBO_ProductRating_FK)?.Pr_numeric_Rating ?? 0;
                            if (Rating != 0 && Detail.DYEBO_Nett != 0)
                            {
                                if (NRow.StandardDskWeight != 0 && NRow.StandardWidth != 0)
                                {
                                    var Yield = core.FabricYield(NRow.StandardDskWeight, NRow.StandardWidth);
                                    NRow.StdExpectQty = Convert.ToInt32((Yield / Rating) * GreigeProd.GreigeP_weightAvail);

                                    Yield = core.FabricYield(Detail.DYEBO_DiskWeight, Detail.DYEBO_Width);
                                    NRow.Actual = Convert.ToInt32((Yield / Rating) * Detail.DYEBO_Nett);

                                    NRow.Difference = NRow.Actual - NRow.StdExpectQty;
                                    NRow.VariancePercent = core.CalculateVariance(NRow.StdExpectQty, NRow.Actual);
                                }
                                else
                                {
                                    NRow.StdExpectQty = 0;
                                    NRow.Actual = 0;
                                    NRow.Difference = 0;
                                    NRow.VariancePercent = 0;
                                }
                            }
                            else
                            {
                                NRow.StdExpectQty = 0;
                                NRow.Actual = 0;
                                NRow.Difference = 0;
                                NRow.VariancePercent = 0;
                            }

                            // Directly Calculate Length Shrinkage and assign it to NRow.LengthShrinkage
                            var originalLength =  GreigeProd.GreigeP_Meters;   // Original Length before processing
                            var finalLength = Detail.DYEBO_Meters;       // Final Length after processing
                                                                         //if (originalLength != 0 && finalLength != 0)

                            if (finalLength != 0 && originalLength != 0)
                            {
                                // Perform the shrinkage calculation and assign to LengthShrinkage
                                decimal v = Math.Round(((originalLength - finalLength) / originalLength) * 100, 2);
                                NRow.LengthShrinkage = v.ToString("F2");
                            }
                            else
                            {
                                NRow.LengthShrinkage = "0.00";  // Default to "0" as a string if lengths are not available
                            }

                            // Retrieve Operator, Machine, and Customer Information
                            var AllocatedOp = context.TLDYE_AllocatedOperator.FirstOrDefault(x => x.DYEOP_BatchNo_FK == DBatch.DYEB_Pk);
                            if (AllocatedOp != null)
                            {
                                NRow.DyeOperator = context.TLADM_MachineOperators.Find(AllocatedOp.DYEOP_Operator_FK)?.MachOp_Description;
                            }

                            var AllocatedMachine = context.TLDYE_DyeBatchAllocated.FirstOrDefault(x => x.TLDYEA_DyeBatch_FK == DBatch.DYEB_Pk);
                            if (AllocatedMachine != null)
                            {
                                NRow.DyeMachine = context.TLADM_MachineDefinitions.Find(AllocatedMachine.TLDYEA_MachCode_FK)?.MD_Description;
                            }

                            var Customer = context.TLADM_CustomerFile.Find(DBatch.DYEB_Customer_FK);
                            if (Customer != null)
                            {
                                NRow.Customer = Customer.Cust_Description;
                            }

                            // Length Shrinkage, QA Pass/Fail, RFT, and Comments
                            var NonCompliance = context.TLDYE_NonComplianceAnalysis.FirstOrDefault(nc => nc.TLDYEDC_BatchNo == DBatch.DYEB_Pk);
                            NRow.QAPassFail = NonCompliance?.TLDYEDC_Pass == true ? "Pass" : "Fail";
                            NRow.RFT = NonCompliance?.TLDYEDC_Pass == true ? "Yes" : "No";
                            NRow.Comments = GreigeProd?.GreigeP_Remarks ?? "";

                            // Add the row to the DataTable
                            dataTable1.Rows.Add(NRow);
                        }
                    }
                }

                // Add DataTables to the DataSet and set the data source for the report
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                var tProcess = new DyeHouse.FabricWeightThroughProcess();
                tProcess.SetDataSource(ds);

                // Group the report based on the selected option
                switch (_parms.DO_OptionSelected)
                {
                    case 0:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[1];
                        break;
                    case 1:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[3];
                        break;
                    case 2:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[18];
                        break;
                    case 3:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[19];
                        break;
                    case 4:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[7];
                        break;
                    case 5:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[10];
                        break;
                    case 6:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[16];
                        break;
                    default:
                        tProcess.DataDefinition.Groups[0].ConditionField = tProcess.Database.Tables[0].Fields[20];
                        break;
                }

                // Assign the report to the CrystalReportViewer
                crystalReportViewer1.ReportSource = tProcess;
            }
            else if (_RepNo == 50)
            {
                DataSet ds = new DataSet();
                DataSet54.DataTable1DataTable dataTable1 = new DataSet54.DataTable1DataTable();
                DataSet54.DataTable2DataTable dataTable2 = new DataSet54.DataTable2DataTable();
                DataSet54.DataTable3DataTable dataTable3 = new DataSet54.DataTable3DataTable();

                int _Quality_FK = 0;

                DataSet54.DataTable2Row NRow = null;

                IList<double> LListShrinkage = new List<double>();
                IList<double> WListShrinkage = new List<double>();
                IList<double> Spirality = new List<double>();
                IList<double> Courses = new List<double>();
                IList<double> Wales = new List<double>();

                core = new Util();
                _repo = new DyeHouse.DyeRepository();

                DataSet54.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                NewRow.Pk = 1;
                NewRow.FromDate = _parms.FromDate;
                NewRow.ToDate = _parms.ToDate;
                NewRow.Title = "Greige Quality Analysis";
                dataTable1.Rows.Add(NewRow);

                using (var context = new TTI2Entities())
                {
                    var QGrouped = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_Date >= _parms.FromDate && x.TLDYEDC_Date <= _parms.ToDate && x.TLDYEDC_NCStage == 5 && x.TLDYEDC_Code_FK >= 12 && x.TLDYEDC_Code_FK <= 16).GroupBy(x => new { x.TLDYEDC_Quality_FK }).ToList();

                    foreach (var DGrouped in QGrouped)
                    {
                        LListShrinkage.Clear();
                        WListShrinkage.Clear();
                        Spirality.Clear();
                        Courses.Clear();
                        Wales.Clear();

                        foreach (var DGrp in DGrouped)
                        {
                            if (DGrp.TLDYEDC_Code_FK == 12)
                            {
                                NRow = dataTable2.NewDataTable2Row();
                                NRow.Pk = 1;
                                var DyeBatchPk = DGrp.TLDYEDC_BatchNo;
                                var DBatch = context.TLDYE_DyeBatch.Find(DyeBatchPk);
                                if (DBatch != null)
                                {
                                    NRow.BatchNo = DBatch.DYEB_BatchNo;
                                    NRow.Quality = context.TLADM_Griege.Find(DBatch.DYEB_Greige_FK).TLGreige_Description;
                                    _Quality_FK = NRow.Quality_FK = DBatch.DYEB_Greige_FK;
                                    var PiecePk = DGrp.TLDYEDC_PieceNo_FK;
                                    NRow.PieceNo = context.TLKNI_GreigeProduction.Find(PiecePk).GreigeP_PieceNo;
                                    var DOrder = context.TLDYE_DyeOrder.Find(DBatch.DYEB_DyeOrder_FK);
                                    if (DOrder != null)
                                    {
                                        NRow.Style = context.TLADM_Styles.Find(DOrder.TLDYO_Style_FK).Sty_Description;
                                        NRow.Colour = context.TLADM_Colours.Find(DOrder.TLDYO_Colour_FK).Col_Display;
                                        NRow.Size = string.Empty;
                                    }
                                    if (_Quality_FK == 14)
                                    {
                                        int a = 0;
                                    }

                                }
                                dataTable2.Rows.Add(NRow);
                            }

                            if (DGrp.TLDYEDC_Code_FK == 12)
                            {
                                NRow.LShrink = DGrp.TLDYEDC_Value;
                                LListShrinkage.Add((double)DGrp.TLDYEDC_Value);
                            }
                            else if (DGrp.TLDYEDC_Code_FK == 13)
                            {
                                NRow.WShrink = DGrp.TLDYEDC_Value;
                                WListShrinkage.Add((double)DGrp.TLDYEDC_Value);
                            }
                            else if (DGrp.TLDYEDC_Code_FK == 14)
                            {
                                NRow.Spirality = DGrp.TLDYEDC_Value;
                                Spirality.Add((double)DGrp.TLDYEDC_Value);
                            }
                            else if (DGrp.TLDYEDC_Code_FK == 15)
                            {
                                NRow.Courses = DGrp.TLDYEDC_Value;
                                Courses.Add((double)DGrp.TLDYEDC_Value);
                            }
                            else
                            {
                                NRow.Wales = DGrp.TLDYEDC_Value;
                                Wales.Add((double)DGrp.TLDYEDC_Value);
                            }
                        }

                        DataSet54.DataTable3Row ThRow = dataTable3.NewDataTable3Row();
                        ThRow.Pk_ = 1;
                        ThRow.Quality = _Quality_FK;
                        ThRow.Ind = 0;
                        ThRow.Mean_LShrinkage = 0;
                        ThRow.Mean_WShrinkage = 0;
                        ThRow.Mean_Spirality = 0;
                        ThRow.Mean_Courses = 0;
                        ThRow.Mean_Wales = 0;

                        ThRow.Variance_LShrinkage = 0;
                        ThRow.Variance_WShrinkage = 0;
                        ThRow.Variance_Spirality = 0;
                        ThRow.Variance_Courses = 0;
                        ThRow.Variance_Wales = 0;

                        ThRow.StdDev_LShrinkage = 0;
                        ThRow.StdDev_WShrinkage = 0;
                        ThRow.StdDev_Spirality = 0;
                        ThRow.StdDev_Courses = 0;
                        ThRow.StdDev_Wales = 0;

                        ThRow.Largest_LShrinkage = 0;
                        ThRow.Largest_WShrinkage = 0;
                        ThRow.Largest_Spirarility = 0;
                        ThRow.Largest_Courses = 0;
                        ThRow.Largest_Wales = 0;

                        ThRow.Smallest_LShrinkage = 0;
                        ThRow.Smallest_WShrinkage = 0;
                        ThRow.Smallest_Spirality = 0;
                        ThRow.Smallest_Courses = 0;
                        ThRow.Smallest_Wales = 0;

                        ThRow.Range_LShrinkage = 0;
                        ThRow.Range_WShrinkage = 0;
                        ThRow.Range_Spirality = 0;
                        ThRow.Range_Courses = 0;
                        ThRow.Range_Wales = 0;

                        //===============================Mean
                        if (LListShrinkage.Count != 0)
                        {
                            ThRow.Mean_LShrinkage = (LListShrinkage.Sum() / LListShrinkage.Count());
                        }

                        if (WListShrinkage.Count != 0)
                        {
                            ThRow.Mean_WShrinkage = (WListShrinkage.Sum() / WListShrinkage.Count());
                        }


                        if (Spirality.Count != 0)
                        {
                            ThRow.Mean_Spirality = (Spirality.Sum() / Spirality.Count());
                        }

                        if (Courses.Count != 0)
                        {
                            ThRow.Mean_Courses = (Courses.Sum() / Courses.Count());
                        }

                        if (Wales.Count != 0)
                        {
                            ThRow.Mean_Wales = (Wales.Sum() / Wales.Count());
                        }

                        //*****************************
                        // End of Mean 
                        //*********************************
                        ThRow.Variance_LShrinkage = core.standardVariance(ThRow.Mean_LShrinkage, LListShrinkage);
                        ThRow.Variance_WShrinkage = core.standardVariance(ThRow.Mean_WShrinkage, WListShrinkage);
                        ThRow.Variance_Spirality = core.standardVariance(ThRow.Mean_Spirality, Spirality);
                        ThRow.Variance_Courses = core.standardVariance(ThRow.Mean_Courses, Courses);
                        ThRow.Variance_Wales = core.standardVariance(ThRow.Mean_Wales, Wales);

                        //**********************
                        // End of Variance 
                        //**********************************

                        ThRow.Largest_LShrinkage = LListShrinkage.OrderBy(x => x).FirstOrDefault();
                        ThRow.Largest_WShrinkage = WListShrinkage.OrderBy(x => x).FirstOrDefault();
                        ThRow.Largest_Spirarility = Spirality.OrderBy(x => x).FirstOrDefault();
                        ThRow.Largest_Courses = Courses.OrderBy(x => x).FirstOrDefault();
                        ThRow.Largest_Wales = Wales.OrderBy(x => x).FirstOrDefault();

                        ThRow.StdDev_LShrinkage = core.standardDeviation(LListShrinkage);
                        ThRow.StdDev_WShrinkage = core.standardDeviation(WListShrinkage);
                        ThRow.StdDev_Spirality = core.standardDeviation(Spirality);
                        ThRow.StdDev_Courses = core.standardDeviation(Courses);
                        ThRow.StdDev_Wales = core.standardDeviation(Wales);
                        //********************
                        //end of Largest
                        //**********************
                        ThRow.Smallest_LShrinkage = LListShrinkage.OrderBy(x => x).LastOrDefault();
                        ThRow.Smallest_WShrinkage = WListShrinkage.OrderBy(x => x).LastOrDefault();
                        ThRow.Smallest_Spirality = Spirality.OrderBy(x => x).LastOrDefault();
                        ThRow.Smallest_Courses = Courses.OrderBy(x => x).LastOrDefault();
                        ThRow.Smallest_Wales = Wales.OrderBy(x => x).LastOrDefault();

                        //********************
                        //end of Smallest
                        //**********************
                        ThRow.Range_LShrinkage = LListShrinkage.OrderBy(x => x).LastOrDefault() - LListShrinkage.OrderBy(x => x).FirstOrDefault();
                        ThRow.Range_WShrinkage = WListShrinkage.OrderBy(x => x).LastOrDefault() - WListShrinkage.OrderBy(x => x).FirstOrDefault();
                        ThRow.Range_Spirality = Spirality.OrderBy(x => x).LastOrDefault() - Spirality.OrderBy(x => x).FirstOrDefault();
                        ThRow.Range_Courses = Courses.OrderBy(x => x).LastOrDefault() - Courses.OrderBy(x => x).FirstOrDefault();
                        ThRow.Range_Wales = Wales.OrderBy(x => x).LastOrDefault() - Wales.OrderBy(x => x).FirstOrDefault();

                        //********************
                        //end of Range
                        //**********************

                        dataTable3.Rows.Add(ThRow);
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    NRow = dataTable2.NewDataTable2Row();
                    NRow.Pk = 1;
                    NRow.ErrorLog = "No records found for dates selected";
                    dataTable2.Rows.Add(NRow);

                    if (dataTable3.Rows.Count == 0)
                    {
                        DataSet54.DataTable3Row TRow = dataTable3.NewDataTable3Row();
                        dataTable3.Rows.Add(TRow);
                    }
                }
                ds.Tables.Add(dataTable2);
                ds.Tables.Add(dataTable3);
                DyeHouse.DyeGreigeAnalysis TProcess = new DyeHouse.DyeGreigeAnalysis();
                TProcess.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TProcess;
            }
            else if (_RepNo == 51)
            {

                DataSet ds = new DataSet();
                DataSet55.DataTable1DataTable dataTable1 = new DataSet55.DataTable1DataTable();
                DataSet55.DataTable2DataTable dataTable2 = new DataSet55.DataTable2DataTable();

                System.Data.DataTable dt = new System.Data.DataTable();
                DataColumn[] keys = new DataColumn[1];
                DataColumn column;

                core = new Util();
                _repo = new DyeHouse.DyeRepository();

                //------------------------------------------------------
                // Create column 0. // This is the Chemical Key
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Col0";
                dt.Columns.Add(column);
                keys[0] = column;

                //------------------------------------------------------
                // Create column 1. // This is the Code Cost 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Col1";
                dt.Columns.Add(column);

                //------------------------------------------------------
                // Create column 2. // This is the Chemical Description
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Col2";
                dt.Columns.Add(column);

                //------------------------------------------------------
                // Create column 3. // This is the consumption 
                //----------------------------------------------

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Decimal");
                column.ColumnName = "Col3";
                dt.Columns.Add(column);

                //------------------------------------------------------
                // Create column 4. // This is the Standard Cost 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Decimal");
                column.ColumnName = "Col4";
                dt.Columns.Add(column);


                //------------------------------------------------------
                // Create column 5. // This is the Total Cost 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Decimal");
                column.ColumnName = "Col5";
                dt.Columns.Add(column);


                dt.PrimaryKey = keys;

                using (var context = new TTI2Entities())
                {

                    var DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_Stage1 && x.DYEB_DateStage1 >= _parms.FromDate && x.DYEB_DateStage1 <= _parms.ToDate).GroupBy(x => x.DYEB_Colour_FK);
                    foreach (var DyeBatch in DyeBatches)
                    {
                        var ColourPk = DyeBatch.FirstOrDefault().DYEB_Colour_FK;

                        var TotalWeight = DyeBatch.Sum(x => (decimal?)x.DYEB_BatchKG) ?? 0.00M;
                        var RecDefinit = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_ColorChart_FK == ColourPk).ToList();
                        foreach (var Def in RecDefinit)
                        {
                            var DefDetail = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == Def.TLDYE_DefinePk).ToList();

                            foreach (var Detail in DefDetail)
                            {
                                var DCConsumable = context.TLADM_ConsumablesDC.Find(Detail.TLDYED_Cosumables_FK);
                                if (DCConsumable != null)
                                {
                                    Decimal Kgs = 0.00M;

                                    if (!Detail.TLDYED_LiqCalc)
                                    {
                                        Kgs = (decimal)(Detail.TLDYED_MELFC * TotalWeight) / 100;
                                    }
                                    else
                                    {
                                        Kgs = (decimal)(Detail.TLDYED_MELFC * TotalWeight * Detail.TLDYED_LiqRatio) / 1000;
                                    }

                                    var dtDataRow = dt.Rows.Find(DCConsumable.ConsDC_Pk);
                                    if (dtDataRow == null)
                                    {
                                        dtDataRow = dt.NewRow();
                                        dtDataRow[0] = DCConsumable.ConsDC_Pk;
                                        dtDataRow[1] = DCConsumable.ConsDC_Code;
                                        dtDataRow[2] = DCConsumable.ConsDC_Description;
                                        dtDataRow[3] = Kgs;
                                        dtDataRow[4] = DCConsumable.ConsDC_StandardCost;
                                        dt.Rows.Add(dtDataRow);
                                    }
                                    else
                                    {
                                        decimal Current = (decimal)dtDataRow[3];
                                        dtDataRow[3] = Current + Kgs;
                                    }
                                }
                            }
                        }
                    }

                    DataSet55.DataTable1Row NewRow = dataTable1.NewDataTable1Row();
                    NewRow.Pk = 1;
                    NewRow.DateFrom = _parms.FromDate;
                    NewRow.DateTo = _parms.ToDate;
                    NewRow.Title = "Dyes and Chemicals consummed for the period";
                    dataTable1.Rows.Add(NewRow);
                    ds.Tables.Add(dataTable1);

                    DataSet55.DataTable2Row nRow = dataTable2.NewDataTable2Row();
                    if (dt.Rows.Count == 0)
                    {
                        nRow.Pk = 1;
                        nRow.ErrorLog = "No records found for dates selected";
                        dataTable2.Rows.Add(nRow);

                    }
                    else
                    {
                        foreach (DataRow DRow in dt.Rows)
                        {
                            nRow = dataTable2.NewDataTable2Row();
                            nRow.Pk = 1;
                            nRow.Code = DRow.Field<string>(1);
                            nRow.Description = DRow.Field<string>(2);
                            nRow.Consumption = DRow.Field<decimal>(3);
                            nRow.Standard_Cost = DRow.Field<decimal>(4);
                            nRow.Total_Cost = DRow.Field<decimal>(3) * DRow.Field<decimal>(4);
                            dataTable2.Rows.Add(nRow);
                        }

                    }
                    ds.Tables.Add(dataTable2);

                    DyeHouse.DCMonthlyConsumption TProcess = new DyeHouse.DCMonthlyConsumption();
                    TProcess.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = TProcess;
                }

            }
            else if (_RepNo == 52) //Garment Dyeing WIP  
            {
                DataSet ds = new DataSet();
                dsGarmentDyeingWIP.DataTable1DataTable dataTable1 = new dsGarmentDyeingWIP.DataTable1DataTable();
                dsGarmentDyeingWIP.DataTable2DataTable dataTable2 = new dsGarmentDyeingWIP.DataTable2DataTable();
                //_repo = new CuttingRepository();
                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                {   new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text15", string.Empty},
                        new string[] {"Text16", string.Empty},
                        new string[] {"Text17", string.Empty}
                };

                Util core = new Util();

                dsGarmentDyeingWIP.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                dtr.gdPK = "";
                dtr.gdReportTitle = "Garment Dye Batch WIP";
                //dtr.gdToDate = _parms.ToDate;
                dataTable1.AddDataTable1Row(dtr);

                int i = 0;
                var CNames = core.CreateColumnNames();

                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                //using (var context = new TTI2Entities())
                //{
                //    var Existing = _repo.SelectWIPCutSheets(_parms).OrderBy(x => x.TLCutSH_No);
                //    foreach (var row in Existing)
                //    {
                //        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                //        nr.Pk = 1;
                //        nr.Col1 = 0;
                //        nr.Col2 = 0;
                //        nr.Col3 = 0;
                //        nr.Col4 = 0;
                //        nr.Col5 = 0;
                //        nr.Col6 = 0;
                //        nr.Col7 = 0;
                //        nr.Col8 = 0;
                //        nr.Col9 = 0;
                //        nr.Col10 = 0;
                //        nr.Col11 = 0;
                //        nr.Date = row.TLCutSH_Date;
                //        nr.Department = context.TLADM_Departments.Find(row.TLCutSH_Department_FK).Dep_Description;
                //        nr.CutSheetNo = row.TLCutSH_No;
                //        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == row.TLCutSH_Styles_FK).Sty_Description;
                //        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCutSH_Colour_FK).Col_Display;
                //        nr.Priority = row.TLCUTSH_Priority;
                //        nr.OnHold = row.TLCUTSH_OnHold;
                //        nr.DyeBatch = context.TLDYE_DyeBatch.Find(row.TLCutSH_DyeBatch_FK).DYEB_BatchNo;

                //        var xSizes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == row.TLCutSH_Pk).ToList();
                //        foreach (var xSize in xSizes)
                //        {
                //            var Size = _Sizes.FirstOrDefault(s => s.SI_id == xSize.TLCUTE_Size_FK);
                //            if (Size != null)
                //            {
                //                if (Size.SI_ColNumber == 1)
                //                    nr.Col1 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 2)
                //                    nr.Col2 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 3)
                //                    nr.Col3 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 4)
                //                    nr.Col4 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 5)
                //                    nr.Col5 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 6)
                //                    nr.Col6 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 7)
                //                    nr.Col7 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 8)
                //                    nr.Col8 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 9)
                //                    nr.Col9 += xSize.TLCUTE_NoofGarments;
                //                else if (Size.SI_ColNumber == 10)
                //                    nr.Col10 += xSize.TLCUTE_NoofGarments;
                //                else
                //                    nr.Col11 += xSize.TLCUTE_NoofGarments;
                //            }
                //        }
                //        dataTable1.AddDataTable1Row(nr);
                //    }


                //    if (dataTable1.Rows.Count != 0)
                //    {
                //        System.Data.DataTable dt = new System.Data.DataTable();
                //        try
                //        {
                //            if (_parms.RepSortOption == 1)
                //            {
                //                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                //            }
                //            else if (_parms.RepSortOption == 2)
                //            {
                //                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[3].ColumnName, DataViewRowState.Added).CopyToDataTable();
                //            }
                //            else if (_parms.RepSortOption == 3)
                //            {
                //                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                //            }
                //            dataTable1.Rows.Clear();

                //            foreach (DataRow dr in dt.Rows)
                //            {
                //                DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                //                nr.Pk = dr.Field<int>(0);
                //                nr.CutSheetNo = dr.Field<String>(1);
                //                nr.Date = dr.Field<DateTime>(2);
                //                nr.Colour = dr.Field<String>(3);
                //                nr.ErrorLog = dr.Field<String>(4);
                //                nr.Style = dr.Field<String>(5);
                //                nr.Department = dr.Field<String>(6);
                //                nr.OnHold = dr.Field<bool>(7);
                //                nr.Priority = dr.Field<bool>(8);
                //                nr.Col1 = dr.Field<int>(9);
                //                nr.Col2 = dr.Field<int>(10);
                //                nr.Col3 = dr.Field<int>(11);
                //                nr.Col4 = dr.Field<int>(12);
                //                nr.Col5 = dr.Field<int>(13);
                //                nr.Col6 = dr.Field<int>(14);
                //                nr.Col7 = dr.Field<int>(15);
                //                nr.Col8 = dr.Field<int>(16);
                //                nr.Col9 = dr.Field<int>(17);
                //                nr.Col10 = dr.Field<int>(18);
                //                nr.Col11 = dr.Field<int>(19);
                //                nr.DyeBatch = dr.Field<String>(20);

                //                dataTable1.AddDataTable1Row(nr);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message);
                //            return;
                //        }
                //    }


                //}

                //if (dataTable1.Rows.Count == 0)
                //{
                //    DataSet4.DataTable1Row tr = dataTable1.NewDataTable1Row();
                //    tr.Pk = 1;
                //    tr.ErrorLog = "There are no records pertaining to selection made";
                //    dataTable1.AddDataTable1Row(tr);
                //}

                //ds.Tables.Add(dataTable1);
                //ds.Tables.Add(dataTable2);

                //WIPCutting wipCut = null;
                //WIPCuttingByCS wipCut_CS = null;

                //if (_parms.RepSortOption == 1)
                //{
                //    wipCut_CS = new WIPCuttingByCS();
                //    wipCut_CS.SetDataSource(ds);
                //}
                //else if (_parms.RepSortOption == 2)
                //{
                //    wipCut = new WIPCutting();
                //    wipCut.SetDataSource(ds);
                //}
                //else
                //{
                //    wipCut = new WIPCutting();
                //    wipCut.SetDataSource(ds);
                //}

                //System.Collections.IEnumerator ie = null;

                //if (_parms.RepSortOption == 1)
                //    ie = wipCut_CS.Section2.ReportObjects.GetEnumerator();
                //else
                //    ie = wipCut.Section2.ReportObjects.GetEnumerator();

                //while (ie.MoveNext())
                //{
                //    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                //    {
                //        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                //        var result = (from u in ColumnNames
                //                      where u[0] == to.Name
                //                      select u).FirstOrDefault();

                //        if (result != null)
                //            to.Text = result[1];
                //    }
                //}

                //if (_parms.RepSortOption == 1)
                //{
                //    // AS20240315 v5.0.0.124 - Added totals for all locations
                //    crystalReportViewer1.ReportSource = wipCut_CS;

                //}
                //else
                //{
                //    if (_parms.RepSortOption == 2)
                //        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[3];
                //    else
                //        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[5];

                GarmentDyeingWIP garmentDyeingWIP = new GarmentDyeingWIP();
                crystalReportViewer1.ReportSource = garmentDyeingWIP;

                //}

            }
            else if (_RepNo == 65)
            {
                DataSet ds = new DataSet();
                DataSet56.DataTable1DataTable dataTable1 = new DataSet56.DataTable1DataTable();
                DataSet56.DataTable2DataTable dataTable2 = new DataSet56.DataTable2DataTable();
                core = new Util();

                using (var context = new TTI2Entities())
                {
                    IList<TLADM_Styles> _Styles = null;
                    IList<TLADM_Colours> _Colours = null;
                    IList<TLADM_Sizes> _Sizes = null;
                    _Styles = context.TLADM_Styles.ToList();
                    _Sizes = context.TLADM_Sizes.ToList();
                    _Colours = context.TLADM_Colours.ToList();

                    var BatchDetail = context.TLDYE_RFDHistory.Where(x => x.DyeRFD_Transaction_No == _Pk).FirstOrDefault();
                    if (BatchDetail != null)
                    {
                        DataSet56.DataTable1Row trx = dataTable1.NewDataTable1Row();
                        trx.Pk = _Pk.ToString();
                        trx.BatchNo = "GD" + BatchDetail.DyeRFD_Transaction_No.ToString().PadLeft(5, '0');
                        trx.ExpectedDate = BatchDetail.DyeRFD_FinishDyeDate?.ToString("yyyy-MM-dd");
                        dataTable1.AddDataTable1Row(trx);
                    }

                    var Boxes = context.TLDYE_RFDHistory.Where(x => x.DyeRFD_Transaction_No == _Pk).ToList();
                    int totalUnits = 0;
                    double boxWeight = 0;

                    foreach (var selectedBox in Boxes)
                    {
                        DataSet56.DataTable2Row row = dataTable2.NewDataTable2Row();

                        var Box = context.TLCSV_StockOnHand.Find(selectedBox.DyeRFD_StockOnHand_Fk);

                        row.BoxNo = Box.TLSOH_BoxNumber;
                        row.BoxQty = Box.TLSOH_BoxedQty.ToString();

                        row.Style = _Styles.FirstOrDefault(s => s.Sty_Id == selectedBox.DyeRFD_CurrentStyle).Sty_Description;
                        row.Colour = _Colours.FirstOrDefault(s => s.Col_Id == selectedBox.DyeRFD_DyeToColour).Col_Display;
                        row.Size = _Sizes.FirstOrDefault(s => s.SI_id == Box.TLSOH_Size_FK).SI_Description;
                        totalUnits = totalUnits + Box.TLSOH_BoxedQty;
                        row.TotalUnits = totalUnits.ToString();
                        boxWeight = boxWeight + Convert.ToDouble(Box.TLSOH_Weight);
                        row.TotalBoxWeight = boxWeight.ToString();
                        dataTable2.AddDataTable2Row(row);
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                if (dataTable1.Rows.Count > 0)
                {
                    CrystalReport1 garmentDyeingBatchPaper = new CrystalReport1();
                    garmentDyeingBatchPaper.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = garmentDyeingBatchPaper;
                    crystalReportViewer1.RefreshReport();
                }
                else
                {
                    Debug.WriteLine("No data to display in the report.");
                    MessageBox.Show("No data to display in the report.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                crystalReportViewer1.Refresh();
            }
        }
    }
}