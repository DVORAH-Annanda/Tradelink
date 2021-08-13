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

namespace Cutting
{
    public partial class frmCutViewRep : Form
    {

        int _RepNo;
        int _KeyValue;
        CutReportOptions _repopts;
        CuttingQueryParameters _parms;
        CuttingRepository _repo;

        IList<TLADM_Styles> _Styles;
        IList<TLADM_Colours> _Colours;
        IList<TLADM_Sizes> _Sizes;
        IList<TLADM_CustomerFile> _Customers;
        IList<TLADM_Griege> _Qualities;
        IList<TLADM_MachineDefinitions> _Machines;


        public bool _NoRecords;


        public frmCutViewRep()
        {
            InitializeComponent();
        }

        public frmCutViewRep(int RepNo)
        {
            InitializeComponent();

            _NoRecords = false;
            _RepNo = RepNo;
        }

        public frmCutViewRep(int RepNo, int BatchKey)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _KeyValue = BatchKey;
            _NoRecords = false;

        }

        public frmCutViewRep(int RepNo, CuttingQueryParameters parms)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _NoRecords = false;
            _parms = parms;

        }

        public frmCutViewRep(int RepNo, CutReportOptions repopts)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _repopts = repopts;
            _NoRecords = false;
        }

        public frmCutViewRep(int RepNo, CutReportOptions repopts, CuttingQueryParameters parms)
        {
            InitializeComponent();

            _RepNo = RepNo;
            _repopts = repopts;
            _NoRecords = false;
            _parms = parms;
        }

        private void frmCutViewRep_Load(object sender, EventArgs e)
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


            if (_RepNo == 1) // CutSheet -------------------------------------------------------------
            {
                string cs = string.Empty;

                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable dataTable2 = new DataSet1.DataTable2DataTable();

                string[][] ColumnNames = null;

                IList<TLADM_QualityDefinition> Reasons = null;
                IList<TLCUT_ExpectedUnits> Expectedunits = new List<TLCUT_ExpectedUnits>();
                IList<TLDYE_DyeOrderDetails> DyeOrderDetails = new List<TLDYE_DyeOrderDetails>();
                BindingList<KeyValuePair<int, decimal>> Ratios = null;
                Int32 DyeOrderUnits = 0;

                Util core = new Util();
                using (var context = new TTI2Entities())
                {
                    var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Depts != null)
                    {
                        Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();
                    }

                    var CutSheet = context.TLCUT_CutSheet.Find(_KeyValue);
                    if (CutSheet != null)
                    {
                        cs = CutSheet.TLCutSH_No + "-";
                        cs = cs.Remove(0, 2);

                        Expectedunits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == _KeyValue).ToList();

                        var DB = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            DyeOrderDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DB.DYEB_DyeOrder_FK).ToList();
                            if (DyeOrderDetails != null)
                            {
                                DyeOrderUnits = DyeOrderDetails.First().TLDYOD_Units;
                                Ratios = core.ReturnRatios(DyeOrderDetails.First().TLDYOD_MarkerRating_FK);
                            }

                            //----------------------------------------------------------
                            // Got to do the Header Record 1st
                            //---------------------------------------------
                            DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Pk = _KeyValue;
                            nr.CutSheet = CutSheet.TLCutSH_No;
                            nr.DyeBatchNo = DB.DYEB_BatchNo;

                            StringBuilder sb = new StringBuilder();
                            if (!String.IsNullOrEmpty(DB.DYEB_Notes))
                                sb.Append(DB.DYEB_Notes + Environment.NewLine);
                            if (!String.IsNullOrEmpty(CutSheet.TLCutSH_Notes))
                                sb.Append(CutSheet.TLCutSH_Notes);
                            nr.Notes = sb.ToString();

                            var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                nr.DateOrdered = DO.TLDYO_OrderDate;
                                nr.OrderedWeekNo = core.GetIso8601WeekOfYear(DO.TLDYO_OrderDate);
                                nr.RequiredWeekNo = DO.TLDYO_CutReqWeek;
                                nr.DateRequired = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                                nr.DateRequired = nr.DateRequired.AddDays(5);
                                nr.Customer = _Customers.FirstOrDefault(s => s.Cust_Pk == CutSheet.TLCutSH_Customer_FK).Cust_Description;
                                nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;
                                nr.Binding = Expectedunits.Sum(x => (decimal?)x.TLCUTE_NoOfBinding) ?? 0.00M;
                                nr.TrimKg_Estimated = Expectedunits.Sum(x => (decimal?)x.TLCUTE_NoOfTrims) ?? 0.00M;
                                nr.Trim1 = string.Empty;
                                nr.TotalG = Expectedunits.Sum(x => x.TLCUTE_NoofGarments);
                                nr.TrimKg = 0;
                                nr.BodyKg = Expectedunits.Sum(x => (decimal?)x.TLCUTE_EstNettWeight) ?? 0.00M;
                                foreach (var DyeOrderD in DyeOrderDetails)
                                {
                                    if (DyeOrderD.TLDYOD_BodyOrTrim)
                                    {
                                        var ProductRating = context.TLADM_ProductRating.Find(DyeOrderD.TLDYOD_MarkerRating_FK);
                                        if (ProductRating != null)
                                        {
                                            nr.BodyRating = ProductRating.Pr_numeric_Rating;
                                        }

                                        nr.Body = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrderD.TLDYOD_Greige_FK).TLGreige_Description;
                                        if (CutSheet.TLCutSH_Size_FK > 0)
                                            nr.Sizes = _Sizes.FirstOrDefault(s => s.SI_id == CutSheet.TLCutSH_Size_FK).SI_Description;  // core.Det
                                    }
                                    else
                                    {
                                        if (DyeOrderD.TLDYOD_Trims_FK != 0)
                                        {
                                            if (String.IsNullOrEmpty(nr.Trim1))
                                            {
                                                nr.Trim1 = context.TLADM_Trims.Find(DyeOrderD.TLDYOD_Trims_FK).TR_Description;
                                                var ProductRating = context.TLADM_ProductRating.Find(DyeOrderD.TLDYOD_MarkerRating_FK);
                                                if (ProductRating != null)
                                                {
                                                    nr.TrimRating = DyeOrderD.TLDYOD_Rating;
                                                }
                                            }
                                            else
                                            {
                                                nr.Trim2 = context.TLADM_Trims.Find(DyeOrderD.TLDYOD_Trims_FK).TR_Description;
                                            }
                                        }
                                    }
                                }

                                dataTable1.AddDataTable1Row(nr);

                                //----------------------------------------------------------
                                // Got to do the Detail next
                                //---------------------------------------------
                                var CustSheetDetails = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                                foreach (var row in CustSheetDetails)
                                {
                                    DataSet1.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                                    xnr.Pk = _KeyValue;

                                    var DyeBatchD = context.TLDYE_DyeBatchDetails.Find(row.TLCutSHD_DyeBatchDet_FK);
                                    if (DyeBatchD != null)
                                    {
                                        /*----------------------------------
                                        if (DyeBatchD.DYEBD_BodyTrim)
                                           nr.BodyKg += DyeBatchD.DYEBO_Nett;
                                        else
                                            nr.TrimKg += DyeBatchD.DYEBO_Nett;
                                        ----------------------------------*/

                                        var GP = context.TLKNI_GreigeProduction.Find(DyeBatchD.DYEBD_GreigeProduction_FK);
                                        if (GP != null)
                                        {
                                            xnr.PieceNo = GP.GreigeP_PieceNo;
                                            xnr.Gross = GP.GreigeP_weight;
                                            xnr.Nett = DyeBatchD.DYEBO_Nett;
                                            xnr.Quality = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk).TLGreige_Description;

                                            if (GP.GreigeP_KnitO_Fk != null && !GP.GreigeP_BoughtIn)
                                                xnr.KnitOrder = "KO" + context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk).KnitO_OrderNumber.ToString();
                                            else
                                                xnr.KnitOrder = string.Empty;

                                            xnr.col0 = GP.GreigeP_Meas1;
                                            xnr.col1 = GP.GreigeP_Meas2;
                                            xnr.col2 = GP.GreigeP_Meas3;
                                            xnr.col3 = GP.GreigeP_Meas4;
                                            xnr.col4 = GP.GreigeP_Meas5;
                                            xnr.col5 = GP.GreigeP_Meas6;
                                            xnr.col6 = GP.GreigeP_Meas7;
                                            xnr.col7 = GP.GreigeP_Meas8;
                                            xnr.Grade = GP.GreigeP_Grade;
                                            xnr.Remarks = GP.GreigeP_Remarks;
                                            xnr.Gm3 = DyeBatchD.DYEBO_DiskWeight;
                                            xnr.Width = DyeBatchD.DYEBO_Width;
                                        }
                                    }
                                    dataTable2.AddDataTable2Row(xnr);
                                }
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                CrystalReport1 rep1 = new CrystalReport1();
                try
                {
                    rep1.SetDataSource(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                crystalReportViewer1.ReportSource = rep1;
                ColumnNames = new string[][]
                    {   new string[] {"Text15", "Piece"},
                        new string[] {"Text16", "Gross"},
                        new string[] {"Text17", "Nett"},
                        new string[] {"Text18", "Quality"},
                        new string[] {"Text19", "Yarn Palett"},
                        new string[] {"Text20", "K/Order"},
                        new string[] {"Text29", "NI"},
                        new string[] {"Text30", "Grd"},
                        new string[] {"Text31", "BIN"},
                        new string[] {"Text32", "Remarks"},
                        new string[] {"Text33", "g/m3"},
                        new string[] {"Text34", "Wdth"}
                    };

                System.Collections.IEnumerator ie = rep1.Section2.ReportObjects.GetEnumerator();

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
                //---------------------------------------------------------------------------
                ColumnNames = new string[][]
                    {   new string[] {"Text38", ""},
                        new string[] {"Text39", ""},
                        new string[] {"Text40", ""},
                        new string[] {"Text41", ""},
                        new string[] {"Text42", ""},
                        new string[] {"Text43", ""},
                        new string[] {"Text44", ""},
                        new string[] {"Text45", ""},
                        new string[] {"Text46", ""},
                        new string[] {"Text47", ""},
                        new string[] {"Text48", ""},
                        new string[] {"Text49", ""},
                        new string[] {"Text50", ""},
                        new string[] {"Text51", ""},
                        new string[] {"Text52", ""},
                        new string[] {"Text53", ""},
                        new string[] {"Text54", ""},
                        new string[] {"Text55", ""},
                        new string[] {"Text56", ""},
                        new string[] {"Text57", ""},
                        new string[] {"Text58", ""},
                        new string[] {"Text59", ""},
                        new string[] {"Text60", ""},
                        new string[] {"Text61", ""},
                    };
                //-------------------------------------------------------------------------
                // This section does the grid at the bottom containing the Bundle numbers
                // Default 24
                ie = rep1.Section5.ReportObjects.GetEnumerator();

                int cnt = 0;
                while (ie.MoveNext())
                {
                    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;


                        var result = (from u in ColumnNames
                                      where u[0] == to.Name
                                      select u).FirstOrDefault();

                        if (result != null)
                            to.Text = cs + (++cnt).ToString().PadLeft(2, '0');
                    }
                }
                //--------------------------------------------------------------------------
                // This section of the code handles the grid where sizes etc etc are show
                //-----------------------------------------------------------------------------
                ColumnNames = new string[][]
                {   new string[] {"1", "Text69","", ""},
                    new string[] {"1",  "Text70", "", ""},
                    new string[] {"1", "Text71", "", ""},
                    new string[] {"1", "Text72", "", ""},
                    new string[] {"1", "Text73", "", ""},
                    new string[] {"1", "Text74", "", ""},
                    new string[] {"1", "Text75", "", ""},
                    new string[] {"1", "Text76", "", ""},
                    new string[] {"2","Text77", "", ""},
                    new string[] {"2","Text104", "", ""},
                    new string[] {"2","Text105", "", ""},
                    new string[] {"2","Text106", "", ""},
                    new string[] {"2","Text107", "", ""},
                    new string[] {"2","Text108", "", ""},
                    new string[] {"2","Text109", "", ""},
                    new string[] {"2","Text110", "", ""},
                    new string[] {"3","Text78", "",""},
                    new string[] {"3","Text79", "", ""},
                    new string[] {"3","Text80", "", ""},
                    new string[] {"3","Text81", "", ""},
                    new string[] {"3","Text82", "", ""},
                    new string[] {"3","Text92", "", ""},
                    new string[] {"3","Text93", "", ""},
                    new string[] {"3","Text94", "", ""},
                    new string[] {"4","Text96", "", ""},
                    new string[] {"4","Text97", "", ""},
                    new string[] {"4","Text98", "", ""},
                    new string[] {"4","Text99", "", ""},
                    new string[] {"4","Text100", "", ""},
                    new string[] {"4","Text101", "", ""},
                    new string[] {"4","Text102", "", ""},
                    new string[] {"4","Text103", "", ""},
                };
                //-----------------------------------------------------------------
                // Insert a Key in column 4 
                // 1st and 2nd Row relate to the sizes 
                //------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    foreach (var EUnit in Expectedunits)
                    {
                        foreach (var row in ColumnNames)
                        {
                            if (row[0] == "1")
                            {
                                if (String.IsNullOrEmpty(row[3]))
                                {
                                    row[3] = EUnit.TLCUTE_Size_FK.ToString();
                                    break;
                                }
                            }
                        }

                        foreach (var row in ColumnNames)
                        {
                            if (row[0] == "2")
                            {
                                if (String.IsNullOrEmpty(row[3]))
                                {
                                    row[3] = EUnit.TLCUTE_Size_FK.ToString();
                                    break;
                                }
                            }
                        }

                        foreach (var row in ColumnNames)
                        {
                            if (row[0] == "3")
                            {
                                if (String.IsNullOrEmpty(row[3]))
                                {
                                    row[3] = EUnit.TLCUTE_Size_FK.ToString();
                                    break;
                                }
                            }
                        }

                        foreach (var row in ColumnNames)
                        {
                            if (row[0] == "4")
                            {
                                if (String.IsNullOrEmpty(row[3]))
                                {
                                    row[3] = EUnit.TLCUTE_Size_FK.ToString();
                                    break;
                                }
                            }
                        }
                    }
                    //-----------------------------------------------------------
                    // insert the appropriate values in the data matrix 
                    // Row 1 sizes ie S,M,L
                    // Row 2 The appropriate expected units from the Dye Order
                    // Row 3 The number of units garments spread accross the sizes as per the ratio
                    // row 4 the of units for binding , ribbing etc spread across the sizes  as per the ratio
                    //--------------------------------------------------------------------
                    foreach (var EUnit in Expectedunits)
                    {
                        foreach (var row in ColumnNames)
                        {
                            if (!String.IsNullOrEmpty(row[3]))
                            {
                                if (EUnit.TLCUTE_Size_FK == Convert.ToInt32(row[3]))
                                {
                                    if (row[0] == "1")
                                    {
                                        row[2] = context.TLADM_Sizes.Find(EUnit.TLCUTE_Size_FK).SI_Description;
                                    }
                                    else if (row[0] == "2")
                                    {
                                        /*
                                        var total = Ratios.Sum(x => x.Value);

                                        foreach (var ratio in Ratios)
                                        {
                                            if (ratio.Key == EUnit.TLCUTE_Size_FK)
                                            {
                                                row[2] = Math.Round(ratio.Value, 2).ToString();
                                            }
                                        } */

                                        row[2] = EUnit.TLCUTE_MarkerRatio.ToString();


                                    }
                                    else if (row[0] == "3")
                                    {
                                        if (EUnit.TLCUTE_Size_FK == Convert.ToInt32(row[3]))
                                        {
                                            if (!String.IsNullOrEmpty(row[2]))
                                            {
                                                var Total = Convert.ToInt32(row[2]);
                                                row[2] = (Total + EUnit.TLCUTE_NoofGarments).ToString();
                                            }
                                            else
                                                row[2] = (EUnit.TLCUTE_NoofGarments).ToString();
                                        }
                                    }
                                    else if (row[0] == "4")
                                    {
                                        if (EUnit.TLCUTE_Size_FK == Convert.ToInt32(row[3]))
                                        {
                                            if (!String.IsNullOrEmpty(row[2]))
                                            {
                                                var Total = Convert.ToInt32(row[2]);
                                                row[2] = (Total + EUnit.TLCUTE_NoOfBinding).ToString();
                                            }
                                            else
                                            {
                                                row[2] = (EUnit.TLCUTE_NoOfBinding).ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ie.Reset();
                while (ie.MoveNext())
                {
                    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;


                        var result = (from u in ColumnNames
                                      where u[1] == to.Name
                                      select u).FirstOrDefault();

                        if (result != null)
                        {
                            if (!String.IsNullOrEmpty(result[2]))
                                to.Text = result[2];
                        }
                    }
                }
            }
            else if (_RepNo == 2)   // Cut Sheet tickets 
            {
                DataSet ds = new DataSet();
                DataSet2.DataTable1DataTable dataTable1 = new DataSet2.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    TLCUT_CutSheet cutSheet = context.TLCUT_CutSheet.Find(_KeyValue);
                    if (cutSheet != null)
                    {
                        var DB = context.TLDYE_DyeBatch.Find(cutSheet.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            string Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DB.DYEB_Greige_FK).TLGreige_Description;
                            string Colour = _Colours.FirstOrDefault(s => s.Col_Id == cutSheet.TLCutSH_Colour_FK).Col_Display;
                            string Sizes = _Sizes.FirstOrDefault(s => s.SI_id == cutSheet.TLCutSH_Size_FK).SI_Description;
                            int i = 1;
                            do
                            {
                                DataSet2.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.CutSheetNo = cutSheet.TLCutSH_No;
                                nr.Quality = Quality;
                                nr.Size = Sizes;
                                nr.Colour = Colour;
                                nr.BagNo = cutSheet.TLCutSH_No + " - " + i.ToString();
                                nr.BundleNo = i.ToString();

                                dataTable1.AddDataTable1Row(nr);

                            } while (++i < 25);   // Default 
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                CutSheetTickets cutsheetT = new CutSheetTickets();
                cutsheetT.SetDataSource(ds);
                crystalReportViewer1.ReportSource = cutsheetT;
            }
            if (_RepNo == 3) // Cut Sheet Panel Return
            {
                DataSet ds = new DataSet();
                DataSet3.DataTable1DataTable dataTable1 = new DataSet3.DataTable1DataTable();
                DataSet3.DataTable2DataTable dataTable2 = new DataSet3.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var CutSheet = context.TLCUT_CutSheet.Find(_repopts.CutSheetPk);
                    if (CutSheet != null)
                    {
                        DataSet3.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = _repopts.Pk;
                        nr.CutSheetNo = CutSheet.TLCutSH_No;
                        nr.Notes = _repopts.remarks;
                        nr.ApprovedBy = _repopts.ApprovedBy;
                        nr.ReturnedTo = _repopts.ReturnedTo;

                        dataTable1.AddDataTable1Row(nr);

                        var Existing = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == _repopts.Pk && x.TLCUTSHRD_PanelRejected).ToList();

                        foreach (var row in Existing)
                        {
                            DataSet3.DataTable2Row dtr = dataTable2.NewDataTable2Row();

                            dtr.Pk = _repopts.Pk;
                            dtr.BundleNo = row.TLCUTSHRD_Description;
                            dtr.QtyReturned = row.TLCUTSHRD_RejectQty;
                            dataTable2.AddDataTable2Row(dtr);
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                PanelReject reject = new PanelReject();
                reject.SetDataSource(ds);
                crystalReportViewer1.ReportSource = reject;
            }
            else if (_RepNo == 4) // WIP Cuttinc (C1)
            {
                DataSet ds = new DataSet();
                DataSet4.DataTable1DataTable dataTable1 = new DataSet4.DataTable1DataTable();
                DataSet4.DataTable2DataTable dataTable2 = new DataSet4.DataTable2DataTable();
                _repo = new CuttingRepository();
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

                DataSet4.DataTable2Row dtr = dataTable2.NewDataTable2Row();
                dtr.Pk = 1;
                if (_parms.AllWIP)
                {
                    dtr.ReportTitale = "WIP Cutting currently in progress";
                    dtr.FromDate = _parms.FromDate;
                    dtr.ToDate = _parms.ToDate;
                }
                else
                {
                    dtr.ReportTitale = "WIP Cutting currently in progress for the period";
                    dtr.FromDate = _parms.FromDate;
                    dtr.ToDate = _parms.ToDate;
                }
                dataTable2.AddDataTable2Row(dtr);

                int i = 0;
                var CNames = core.CreateColumnNames();

                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                using (var context = new TTI2Entities())
                {
                    var Existing = _repo.SelectWIPCutSheets(_parms).OrderBy(x => x.TLCutSH_No);
                    foreach (var row in Existing)
                    {
                        DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
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
                        nr.Date = row.TLCutSH_Date;
                        nr.Department = context.TLADM_Departments.Find(row.TLCutSH_Department_FK).Dep_Description;
                        nr.CutSheetNo = row.TLCutSH_No;
                        nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == row.TLCutSH_Styles_FK).Sty_Description;
                        nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCutSH_Colour_FK).Col_Display;
                        nr.Priority = row.TLCUTSH_Priority;
                        nr.OnHold = row.TLCUTSH_OnHold;
                        nr.DyeBatch = context.TLDYE_DyeBatch.Find(row.TLCutSH_DyeBatch_FK).DYEB_BatchNo;

                        var xSizes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == row.TLCutSH_Pk).ToList();
                        foreach (var xSize in xSizes)
                        {
                            var Size = _Sizes.FirstOrDefault(s => s.SI_id == xSize.TLCUTE_Size_FK);
                            if (Size != null)
                            {
                                if (Size.SI_ColNumber == 1)
                                    nr.Col1 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 2)
                                    nr.Col2 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 3)
                                    nr.Col3 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 4)
                                    nr.Col4 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 5)
                                    nr.Col5 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 6)
                                    nr.Col6 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 7)
                                    nr.Col7 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 8)
                                    nr.Col8 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 9)
                                    nr.Col9 += xSize.TLCUTE_NoofGarments;
                                else if (Size.SI_ColNumber == 10)
                                    nr.Col10 += xSize.TLCUTE_NoofGarments;
                                else
                                    nr.Col11 += xSize.TLCUTE_NoofGarments;
                            }
                        }
                        dataTable1.AddDataTable1Row(nr);
                    }


                    if (dataTable1.Rows.Count != 0)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        try
                        {
                            if (_parms.RepSortOption == 1)
                            {
                                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            }
                            else if (_parms.RepSortOption == 2)
                            {
                                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[3].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            }
                            else if (_parms.RepSortOption == 3)
                            {
                                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            }
                            dataTable1.Rows.Clear();

                            foreach (DataRow dr in dt.Rows)
                            {
                                DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Pk = dr.Field<int>(0);
                                nr.CutSheetNo = dr.Field<String>(1);
                                nr.Date = dr.Field<DateTime>(2);
                                nr.Colour = dr.Field<String>(3);
                                nr.ErrorLog = dr.Field<String>(4);
                                nr.Style = dr.Field<String>(5);
                                nr.Department = dr.Field<String>(6);
                                nr.OnHold = dr.Field<bool>(7);
                                nr.Priority = dr.Field<bool>(8);
                                nr.Col1 = dr.Field<int>(9);
                                nr.Col2 = dr.Field<int>(10);
                                nr.Col3 = dr.Field<int>(11);
                                nr.Col4 = dr.Field<int>(12);
                                nr.Col5 = dr.Field<int>(13);
                                nr.Col6 = dr.Field<int>(14);
                                nr.Col7 = dr.Field<int>(15);
                                nr.Col8 = dr.Field<int>(16);
                                nr.Col9 = dr.Field<int>(17);
                                nr.Col10 = dr.Field<int>(18);
                                nr.Col11 = dr.Field<int>(19);
                                nr.DyeBatch = dr.Field<String>(20);

                                dataTable1.AddDataTable1Row(nr);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }


                }

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet4.DataTable1Row tr = dataTable1.NewDataTable1Row();
                    tr.Pk = 1;
                    tr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(tr);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                WIPCutting wipCut = null;
                WIPCuttingByCS wipCut_CS = null;

                if (_parms.RepSortOption == 1)
                {
                    wipCut_CS = new WIPCuttingByCS();
                    wipCut_CS.SetDataSource(ds);
                }
                else if (_parms.RepSortOption == 2)
                {
                    wipCut = new WIPCutting();
                    wipCut.SetDataSource(ds);
                }
                else
                {
                    wipCut = new WIPCutting();
                    wipCut.SetDataSource(ds);
                }

                System.Collections.IEnumerator ie = null;

                if (_parms.RepSortOption == 1)
                    ie = wipCut_CS.Section2.ReportObjects.GetEnumerator();
                else
                    ie = wipCut.Section2.ReportObjects.GetEnumerator();

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

                if (_parms.RepSortOption == 1)
                {
                    crystalReportViewer1.ReportSource = wipCut_CS;

                }
                else
                {
                    if (_parms.RepSortOption == 2)
                        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[3];
                    else
                        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[5];

                    crystalReportViewer1.ReportSource = wipCut;

                }

            }
            else if (_RepNo == 5) // WIP Cut Production (C4)
            {
                DataSet ds = new DataSet();
                DataSet5.DataTable1DataTable dataTable1 = new DataSet5.DataTable1DataTable();
                DataSet5.DataTable2DataTable dataTable2 = new DataSet5.DataTable2DataTable();

                _repo = new CuttingRepository();

                Util core = new Util();

                DataTable dt = new DataTable();
                dt.Columns.Add("PK", typeof(int));               // 0
                dt.Columns.Add("CutSheetNo", typeof(String));    // 1
                dt.Columns.Add("Date", typeof(DateTime));        // 2
                dt.Columns.Add("DyeBatch", typeof(String));      // 3
                dt.Columns.Add("Quality", typeof(String));       // 4
                dt.Columns.Add("Colour", typeof(String));        // 5 
                dt.Columns.Add("OrderKg", typeof(decimal));      // 6
                dt.Columns.Add("Actual", typeof(int));           // 7
                dt.Columns.Add("Expected", typeof(int));         // 8
                dt.Columns.Add("Difference", typeof(decimal));   // 9
                dt.Columns.Add("Machine", typeof(string));       // 10
                dt.Columns.Add("Customer", typeof(string));      // 11
                dt.Columns.Add("CustomerOrder", typeof(string)); // 12
                dt.Columns.Add("DueDate", typeof(string));       // 13
                dt.Columns.Add("Style", typeof(string));       // 14
                DataSet5.DataTable1Row dtr = dataTable1.NewDataTable1Row();
                dtr.Pk = 1;
                dtr.FromDate = _repopts.fromDate;
                dtr.ToDate = _repopts.toDate;
                dtr.ReportTitle = "Cut Production";
                dataTable1.AddDataTable1Row(dtr);

                var Existing = _repo.SelectCutProduction(_parms).AsQueryable();
                using (var context = new TTI2Entities())
                {
                    foreach (var row in Existing)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = 1;
                        var CS = context.TLCUT_CutSheet.Find(row.TLCUTSHR_CutSheet_FK);
                        if (CS != null)
                        {
                            if (CS.TLCutSH_Closed)
                                continue;

                            dr[1] = CS.TLCutSH_No;
                            dr[2] = row.TLCUTSHR_DateIntoPanelStore;
                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                dr[11] = _Customers.FirstOrDefault(s => s.Cust_Pk == DB.DYEB_Customer_FK).Cust_Description;
                                dr[3] = DB.DYEB_BatchNo;
                                dr[4] = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DB.DYEB_Greige_FK).TLGreige_Description;
                                dr[5] = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;
                                var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    dr[12] = DO.TLDYO_OrderNum;
                                    DateTime DT = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                                    dr[13] = DT.AddDays(5);
                                }
                            }
                            var CSDetail = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                            if (CSDetail != null)
                                dr[6] = CSDetail.Sum(x => (double?)x.TLCUTSHD_NettWeight) ?? 0.00;
                            else
                                dr[6] = 0.00;
                            var BoxedUnits = (decimal)context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == row.TLCUTSHR_Pk).Sum(x => x.TLCUTSHRD_BoxUnits);
                            var ExpectedUnits = (decimal)context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CS.TLCutSH_Pk).Sum(x => x.TLCUTE_NoofGarments);
                            Decimal Difference = 0.00M;
                            if (ExpectedUnits != 0)
                                Difference = ((BoxedUnits - ExpectedUnits) / ExpectedUnits) * 100;
                            dr[7] = (int)BoxedUnits;
                            dr[8] = (int)ExpectedUnits;
                            dr[9] = Difference;
                            dr[10] = _Machines.FirstOrDefault(s => s.MD_Pk == row.TLCUTSHR_Machine_FK).MD_Description;
                            dr[11] = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            dt.Rows.Add(dr);
                        }
                    }
                }

                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        if (_repopts.C4SortOption == 1)
                            dt = dt.Select(null, dt.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repopts.C4SortOption == 2)
                            dt = dt.Select(null, dt.Columns[10].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repopts.C4SortOption == 3)
                            dt = dt.Select(null, dt.Columns[11].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        else if (_repopts.C4SortOption == 4)
                            dt = dt.Select(null, dt.Columns[4].ColumnName, DataViewRowState.Added).CopyToDataTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    foreach (DataRow rw in dt.Rows)
                    {
                        DataSet5.DataTable2Row nr = dataTable2.NewDataTable2Row();
                        nr.Pk = Convert.ToInt32(rw[0].ToString());
                        nr.CutSheet = rw[1].ToString();
                        nr.Date = Convert.ToDateTime(rw[2].ToString());
                        nr.DyeBatch = rw[3].ToString();
                        nr.Quality = rw[4].ToString();
                        nr.Colour = rw[5].ToString();
                        nr.Kg = Convert.ToDecimal(rw[6].ToString());
                        nr.ActualQty = Convert.ToInt32(rw[7].ToString());
                        nr.ExpectedQty = Convert.ToInt32(rw[8].ToString());
                        nr.Difference = Convert.ToDecimal(rw[9].ToString());
                        nr.Machine = rw[10].ToString();
                        nr.Customer = rw[11].ToString();
                        nr.OrderNo = rw[12].ToString();
                        nr.DueDate = Convert.ToDateTime(rw[13].ToString());
                        nr.Styles = rw[11].ToString();

                        dataTable2.AddDataTable2Row(nr);
                    }
                }
                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet5.DataTable2Row nr = dataTable2.NewDataTable2Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable2.AddDataTable2Row(nr);

                }
                ds.Tables.Add(dataTable2);

                CutProduction wipCut = new CutProduction();
                if (_repopts.C4SortOption == 1)
                {
                    wipCut.DataDefinition.Groups[0].ConditionField = wipCut.Database.Tables[1].Fields[5];
                }
                else if (_repopts.C4SortOption == 2)
                {
                    wipCut.DataDefinition.Groups[0].ConditionField = wipCut.Database.Tables[1].Fields[10];
                }
                else if (_repopts.C4SortOption == 3)
                {
                    wipCut.DataDefinition.Groups[0].ConditionField = wipCut.Database.Tables[1].Fields[11];
                }
                else if (_repopts.C4SortOption == 4)
                {
                    wipCut.DataDefinition.Groups[0].ConditionField = wipCut.Database.Tables[1].Fields[4];
                }

                if (_repopts.QAReport)
                    wipCut.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                wipCut.SetDataSource(ds);
                crystalReportViewer1.ReportSource = wipCut;
            }
            else if (_RepNo == 6) // Panel Store 
            {
                DataSet ds = new DataSet();
                DataSet6.DataTable1DataTable dataTable1 = new DataSet6.DataTable1DataTable();
                Util core = new Util();
                _repo = new CuttingRepository();

                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                    {   new string[] {"Text5", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text16", string.Empty},
                        new string[] {"Text17", string.Empty},
                        new string[] {"Text18", string.Empty},
                        new string[] {"Text19", string.Empty}

                    };

                var CNames = core.CreateColumnNames();
                int i = 0;
                foreach (var CName in CNames)
                {
                    ColumnNames[i++][1] = CName[1];
                }

                using (var context = new TTI2Entities())
                {
                    var Existing = _repo.SelCutReceiptByLoc(_parms);
                    foreach (var Record in Existing)
                    {
                        DataSet6.DataTable1Row nr = dataTable1.NewDataTable1Row();
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
                        nr.Total = 0;
                        nr.BoxesAdult = 0;
                        nr.BoxesKids = 0;

                        var PStore = context.TLADM_WhseStore.Find(Record.TLCUTSHR_WhsePanStore_FK);
                        if (PStore != null)
                            nr.PanelStore = PStore.WhStore_Description;

                        var CS = context.TLCUT_CutSheet.Find(Record.TLCUTSHR_CutSheet_FK);
                        if (CS != null)
                        {
                            if (CS.TLCutSH_Closed)
                                continue;

                            nr.CutSheetNo = CS.TLCutSH_No;
                            nr.Styles = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Description;
                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                nr.Quality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DB.DYEB_Greige_FK).TLGreige_Description;
                                nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;

                                var CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == Record.TLCUTSHR_Pk && !x.TLCUTSHRD_PanelRejected && !x.TLCUTSHRD_InBundleStore && !x.TLCUTSHRD_ToCMT);
                                if (CSRD != null && CSRD.Count() != 0)
                                {
                                    var SizeGrps = CSRD.GroupBy(x => x.TLCUTSHRD_Size_FK);

                                    foreach (var grp in SizeGrps)
                                    {
                                        var SizePk = grp.FirstOrDefault().TLCUTSHRD_Size_FK;
                                        var ColNo = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_ColNumber;
                                        var GrpTotal = 0;

                                        try
                                        {
                                            GrpTotal = grp.Sum(x => x.TLCUTSHRD_BoxUnits - x.TLCUTSHRD_RejectQty);
                                        }
                                        catch (Exception ex)
                                        {
                                            GrpTotal = 0;
                                        }

                                        if (ColNo == 1)
                                            nr.Col1 = GrpTotal;
                                        else if (ColNo == 2)
                                            nr.Col2 = GrpTotal;
                                        else if (ColNo == 3)
                                            nr.Col3 = GrpTotal;
                                        else if (ColNo == 4)
                                            nr.Col4 = GrpTotal;
                                        else if (ColNo == 5)
                                            nr.Col5 = GrpTotal;
                                        else if (ColNo == 6)
                                            nr.Col6 = GrpTotal;
                                        else if (ColNo == 7)
                                            nr.Col7 = GrpTotal;
                                        else if (ColNo == 8)
                                            nr.Col8 = GrpTotal;
                                        else if (ColNo == 9)
                                            nr.Col9 = GrpTotal;
                                        else if (ColNo == 10)
                                            nr.Col10 = GrpTotal;
                                        else
                                            nr.Col11 = GrpTotal;
                                    }

                                    nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                                }

                                try
                                {
                                    var CSR_PK = CSRD.FirstOrDefault().TLCUTSHRD_CutSheet_FK;

                                    var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR_PK).FirstOrDefault();
                                    if (CSB != null)
                                    {
                                        nr.BoxesAdult = CSB.TLCUTSHB_AdultBoxes;
                                        nr.BoxesKids = CSB.TLCUTSHB_KidBoxes;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("There appears to be a problem with data for CutSheet " + CS.TLCutSH_No + Environment.NewLine);
                                    MessageBox.Show("Please investigate " + ex.Message);

                                    continue;
                                }
                            }
                        }
                        dataTable1.AddDataTable1Row(nr);
                    }
                }
                if (dataTable1.Rows.Count == 0)
                {
                    DataSet6.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    nr.ErrorLog = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(nr);
                }


                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CutSheetNo";
                ds.Tables.Add(DataV.ToTable());


                PanelStock wipCut = new PanelStock();
                wipCut.SetDataSource(ds);
                crystalReportViewer1.ReportSource = wipCut;

                System.Collections.IEnumerator ie = wipCut.Section2.ReportObjects.GetEnumerator();
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
                if (_repopts.C2SortOption == 2)
                {
                    // Quality
                    wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[3];
                }
                else if (_repopts.C2SortOption == 3)
                {
                    // Colour
                    wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[4];
                }
                else if (_repopts.C2SortOption == 5)
                {
                    //Styles
                    wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[6];
                }

            }
            else if (_RepNo == 7) // Rejected panel store (C3) 
            {
                DataSet ds = new DataSet();
                DataSet7.DataTable1DataTable dataTable1 = new DataSet7.DataTable1DataTable();

                Util core = new Util();

                DataTable dt = new DataTable();
                dt.Columns.Add("PK", typeof(int));               // 0
                dt.Columns.Add("CutSheetNo", typeof(String));    // 1
                dt.Columns.Add("Date", typeof(DateTime));        // 2
                dt.Columns.Add("DyeBatch", typeof(String));      // 3
                dt.Columns.Add("Quality", typeof(String));       // 4
                dt.Columns.Add("Colour", typeof(String));        // 5 
                dt.Columns.Add("PanelQty", typeof(int));         // 6
                dt.Columns.Add("Customer", typeof(string));      // 7
                dt.Columns.Add("CustomerOrder", typeof(string)); // 8
                dt.Columns.Add("DueDate", typeof(string));       // 9

                using (var context = new TTI2Entities())
                {
                    var ExistingGrp = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_PanelRejected).GroupBy(x => x.TLCUTSHRD_CutSheet_FK);
                    foreach (var Group in ExistingGrp)
                    {
                        int CSPk = Group.FirstOrDefault().TLCUTSHRD_CutSheet_FK;
                        var CSR = context.TLCUT_CutSheetReceipt.Find(CSPk);
                        if (CSR != null)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = 1;

                            var CS = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK);
                            if (CS != null)
                            {
                                if (CS.TLCutSH_Closed)
                                    continue;

                                dr[1] = CS.TLCutSH_No;
                                dr[2] = Group.FirstOrDefault().TLCUTSHRD_RejectDate;

                                var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                                if (DB != null)
                                {
                                    dr[3] = DB.DYEB_BatchNo;
                                    dr[4] = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DB.DYEB_Greige_FK).TLGreige_Description;
                                    dr[5] = _Colours.FirstOrDefault(s => s.Col_Id == DB.DYEB_Colour_FK).Col_Display;
                                    dr[6] = Group.Sum(x => x.TLCUTSHRD_RejectQty);

                                    var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                    if (DO != null)
                                    {
                                        dr[7] = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                        dr[8] = DO.TLDYO_OrderNum;
                                        DateTime dt1 = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                                        dr[9] = dt1.AddDays(5);
                                    }
                                }
                            }

                            dt.Rows.Add(dr);
                        }
                    }
                    /*
                    var reportOptions = new BindingList<KeyValuePair<int, string>>();
                    reportOptions.Add(new KeyValuePair<int, string>(1, "CutSheet Number"));
                    reportOptions.Add(new KeyValuePair<int, string>(2, "Quality"));
                    reportOptions.Add(new KeyValuePair<int, string>(3, "Colour"));
                    reportOptions.Add(new KeyValuePair<int, string>(4, "Customer"));
                    */
                    if (dt.Rows.Count != 0)
                    {
                        try
                        {
                            if (_repopts.C3SortOption == 1)
                                dt = dt.Select(null, dt.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_repopts.C3SortOption == 2)
                                dt = dt.Select(null, dt.Columns[4].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_repopts.C3SortOption == 3)
                                dt = dt.Select(null, dt.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                            else if (_repopts.C3SortOption == 4)
                                dt = dt.Select(null, dt.Columns[7].ColumnName, DataViewRowState.Added).CopyToDataTable();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                }

                foreach (DataRow rw in dt.Rows)
                {
                    DataSet7.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = Convert.ToInt32(rw[0].ToString());
                    nr.CutSheet = rw[1].ToString();
                    nr.Date = Convert.ToDateTime(rw[2].ToString());
                    nr.DyeBatch = rw[3].ToString();
                    nr.Quality = rw[4].ToString();
                    nr.Colour = rw[5].ToString();
                    nr.PanelQty = rw[6].ToString();
                    nr.Customer = rw[7].ToString();
                    nr.CustomerOrder = rw[8].ToString();
                    nr.DueDate = Convert.ToDateTime(rw[9].ToString());

                    dataTable1.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable1);

                RejectPanelStore rejPanelStore = new RejectPanelStore();
                rejPanelStore.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rejPanelStore;


            }
            else if (_RepNo == 8) // QA check results  
            {
                DataSet ds = new DataSet();
                DataSet9.DataTable1DataTable dataTable1 = new DataSet9.DataTable1DataTable();
                DataSet9.DataTable2DataTable dataTable2 = new DataSet9.DataTable2DataTable();
                int Size = 0;
                TLADM_CutMeasureStandards Std = null;
                int Style = 0;

                List<DATA> QAPanel = new List<DATA>();

                _repo = new CuttingRepository();
                Util core = new Util();

                var QABundleGroup = _repo.SelectQaResults(_parms).GroupBy(x => x.TLCUTQA_Bundle_FK);
                using (var context = new TTI2Entities())
                {
                    if (_parms.QAFullDetail)
                    {
                        // This section of the code prepares the full detail 
                        //=====================================================
                        foreach (var QAResult in QABundleGroup)
                        {
                            var Index = QAResult.FirstOrDefault().TLCUTQA_Bundle_FK;
                            var CSR = context.TLCUT_CutSheetReceiptDetail.Find(Index);
                            if (CSR != null)
                            {
                                Size = CSR.TLCUTSHRD_Size_FK;
                                if (_parms.Sizes.Count != 0)
                                {
                                    var value = _Sizes.FirstOrDefault(s => s.SI_id == Size);
                                    if (value == null)
                                        continue;
                                }
                                Style = context.TLCUT_CutSheetReceipt.Find(CSR.TLCUTSHRD_CutSheet_FK).TLCUTSHR_Style_FK;
                                if (_parms.Styles.Count != 0)
                                {
                                    var value = _parms.Styles.Find(it => it.Sty_Id == Style);
                                    if (value == null)
                                        continue;
                                }

                                Std = context.TLADM_CutMeasureStandards.Where(x => x.TLCUTAS_Size_FK == Size && x.TLCUTAS_Style_FK == Style && !x.TLCUTAS_PPS).FirstOrDefault();
                            }
                            foreach (var Result in QAResult)
                            {
                                int Count = 0;
                                do
                                {
                                    DataSet9.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                    nr.Pk = 1;
                                    if (CSR != null)
                                    {
                                        nr.Bundle_No = CSR.TLCUTSHRD_Description;
                                    }
                                    nr.Measure_Area = context.TLADM_CutMeasureArea.Find(Result.TLCUTQA_MeasureArea_FK).TLCUTA_ShortCode;
                                    if (Count == 0)
                                    {
                                        nr.Description = "Actual";
                                        nr.Column1 = Result.TLCUTQA_Col1;
                                        nr.Column2 = Result.TLCUTQA_Col2;
                                        nr.Column3 = Result.TLCUTQA_Col3;
                                        nr.Column4 = Result.TLCUTQA_Col4;
                                    }
                                    else if (Count == 1)
                                    {
                                        nr.Description = "Standard";
                                        if (Std != null)
                                        {
                                            nr.Column1 = Std.TLCUTAS_Col1;
                                            nr.Column2 = Std.TLCUTAS_Col2;
                                            nr.Column3 = Std.TLCUTAS_Col3;
                                            nr.Column4 = Std.TLCUTAS_Col4;
                                        }
                                    }
                                    else
                                    {
                                        nr.Description = "Deviation %";
                                        if (Std != null)
                                        {
                                            nr.Column1 = core.CalCulateVariance(Result.TLCUTQA_Col1, Std.TLCUTAS_Col1);
                                            nr.Column2 = core.CalCulateVariance(Result.TLCUTQA_Col2, Std.TLCUTAS_Col2);
                                            nr.Column3 = core.CalCulateVariance(Result.TLCUTQA_Col3, Std.TLCUTAS_Col3);
                                            nr.Column4 = core.CalCulateVariance(Result.TLCUTQA_Col4, Std.TLCUTAS_Col4);
                                        }
                                        else
                                        {
                                            nr.Column1 = 9999;
                                            nr.Column2 = 9999;
                                            nr.Column3 = 9999;
                                            nr.Column4 = 9999;
                                        }
                                    }

                                    nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Size).SI_Description;
                                    nr.Style = _Styles.FirstOrDefault(s => s.Sty_Id == Style).Sty_Description;

                                    dataTable1.AddDataTable1Row(nr);
                                } while (++Count < 3);
                            }
                        }
                    }
                    else     // This section summaries all of the selected records 
                             //==================================================================
                    {
                        foreach (var QAResult in QABundleGroup)
                        {
                            var Index = QAResult.FirstOrDefault().TLCUTQA_Bundle_FK;
                            var CSR = context.TLCUT_CutSheetReceiptDetail.Find(Index);
                            if (CSR != null)
                            {
                                Size = CSR.TLCUTSHRD_Size_FK;
                                Style = context.TLCUT_CutSheetReceipt.Find(CSR.TLCUTSHRD_CutSheet_FK).TLCUTSHR_Style_FK;

                                if (_parms.Sizes.Count != 0)
                                {
                                    var value = _parms.Sizes.Find(it => it.SI_id == Size);
                                    if (value == null)
                                        continue;
                                }
                                Style = context.TLCUT_CutSheetReceipt.Find(CSR.TLCUTSHRD_CutSheet_FK).TLCUTSHR_Style_FK;
                                if (_parms.Styles.Count != 0)
                                {
                                    var value = _parms.Styles.Find(it => it.Sty_Id == Style);
                                    if (value == null)
                                        continue;
                                }

                                foreach (var Result in QAResult)
                                {
                                    var Record = QAPanel.FindAll(x => x.Style == Style && x.Size == Size && x.MeasureArea == Result.TLCUTQA_MeasureArea_FK).FirstOrDefault();
                                    var index = QAPanel.IndexOf(Record);
                                    if (index < 0)
                                    {
                                        DATA data = new DATA();
                                        data.Style = Style;
                                        data.Size = Size;
                                        data.NumberOf = 1;
                                        data.MeasureArea = Result.TLCUTQA_MeasureArea_FK;
                                        data.Col1 = Result.TLCUTQA_Col1;
                                        data.Col2 = Result.TLCUTQA_Col2;
                                        data.Col3 = Result.TLCUTQA_Col3;
                                        data.Col4 = Result.TLCUTQA_Col4;
                                        data.BundleFk = Result.TLCUTQA_Bundle_FK;

                                        QAPanel.Add(data);
                                    }
                                    else
                                    {
                                        Record.NumberOf += 1;
                                        Record.Col1 += Result.TLCUTQA_Col1;
                                        Record.Col2 += Result.TLCUTQA_Col2;
                                        Record.Col3 += Result.TLCUTQA_Col3;
                                        Record.Col4 += Result.TLCUTQA_Col4;
                                    }
                                }
                            }
                        }

                        var StyleGroups = QAPanel;

                        foreach (var Styles in StyleGroups)
                        {
                            Size = Styles.Size;
                            Style = Styles.Style;

                            Std = context.TLADM_CutMeasureStandards.Where(x => x.TLCUTAS_Size_FK == Size
                                           && x.TLCUTAS_Style_FK == Style && !x.TLCUTAS_PPS).FirstOrDefault();

                            int Count = 0;
                            do
                            {
                                DataSet9.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                nr.Pk = 1;
                                nr.Bundle_No = _Styles.FirstOrDefault(s => s.Sty_Id == Style).Sty_Description;
                                nr.Style = string.Empty;
                                nr.Size = _Sizes.FirstOrDefault(s => s.SI_id == Size).SI_Description;
                                nr.Measure_Area = context.TLADM_CutMeasureArea.Find(Styles.MeasureArea).TLCUTA_ShortCode;
                                nr.Measure_Area += " : " + context.TLCUT_CutSheetReceiptDetail.Find(Styles.BundleFk).TLCUTSHRD_BoxNumber;
                                if (Count == 0)
                                {
                                    nr.Description = "Actual ";
                                    nr.Column1 = Styles.Col1;
                                    nr.Column2 = Styles.Col2;
                                    nr.Column3 = Styles.Col3;
                                    nr.Column4 = Styles.Col4;
                                }
                                else if (Count == 1)
                                {
                                    nr.Description = "Standard";
                                    if (Std != null)
                                    {
                                        nr.Column1 = Std.TLCUTAS_Col1;
                                        nr.Column2 = Std.TLCUTAS_Col2;
                                        nr.Column3 = Std.TLCUTAS_Col3;
                                        nr.Column4 = Std.TLCUTAS_Col4;
                                    }
                                }
                                else
                                {
                                    nr.Description = "Deviation %";
                                    if (Std != null)
                                    {
                                        nr.Column1 = core.CalCulateVariance(Styles.Col1, Std.TLCUTAS_Col1);
                                        nr.Column2 = core.CalCulateVariance(Styles.Col2, Std.TLCUTAS_Col2);
                                        nr.Column3 = core.CalCulateVariance(Styles.Col3, Std.TLCUTAS_Col3);
                                        nr.Column4 = core.CalCulateVariance(Styles.Col4, Std.TLCUTAS_Col4);
                                    }
                                    else
                                    {
                                        nr.Column1 = 9999;
                                        nr.Column2 = 9999;
                                        nr.Column3 = 9999;
                                        nr.Column4 = 9999;
                                    }
                                }
                                dataTable1.AddDataTable1Row(nr);
                            } while (++Count < 3);
                        }
                    }
                    DataSet9.DataTable2Row xhn = dataTable2.NewDataTable2Row();
                    xhn.Pk = 1;
                    xhn.FromDate = _parms.FromDate;
                    xhn.ToDate = _parms.ToDate;

                    if (_parms.QAFullDetail)
                        xhn.Title = "QA Panel Report for the period (Full Details)";
                    else
                        xhn.Title = "QA Panel Report for the period (Summarised)";

                    dataTable2.AddDataTable2Row(xhn);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                QACutting PanelData = new QACutting();
                PanelData.SetDataSource(ds);
                crystalReportViewer1.ReportSource = PanelData;

                IEnumerator ie = PanelData.Section2.ReportObjects.GetEnumerator();

                var ColumnNames = new string[][]
                {   new string[] {"Text3", "Neck Line"},
                        new string[] {"Text4", "Neck Drop"},
                        new string[] {"Text5", "Chest"},
                        new string[] {"Text6", "Length"}
                };
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
            else if (_RepNo == 9) // Bundle Store Reporting 
            {
                DataSet ds = new DataSet();
                DataSet8.DataTable1DataTable dataTable1 = new DataSet8.DataTable1DataTable();
                DataSet8.DataTable2DataTable dataTable2 = new DataSet8.DataTable2DataTable();
                _repo = new CuttingRepository();

                var Existing = _repo.SelectCSReceipts(_parms).ToList();

                using (var context = new TTI2Entities())
                {
                    int Count = 0;
                    foreach (var Record in Existing)
                    {
                        var CS = context.TLCUT_CutSheet.Find(Record.TLCUTSHR_CutSheet_FK);

                        DataSet8.DataTable1Row tr = dataTable1.NewDataTable1Row();
                        tr.Pk = ++Count;
                        if (CS != null)
                        {
                            tr.CutSheet = CS.TLCutSH_No;
                        }

                        tr.Date = CS.TLCutSH_Date;
                        var CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == Record.TLCUTSHR_Pk);

                        tr.Bundles = CSRD.Count();

                        dataTable1.AddDataTable1Row(tr);

                        foreach (var Row in CSRD)
                        {
                            DataSet8.DataTable2Row t2r = dataTable2.NewDataTable2Row();
                            t2r.Pk = Count;
                            t2r.Bundle = Row.TLCUTSHRD_Description;
                            t2r.Size = _Sizes.FirstOrDefault(s => s.SI_id == Row.TLCUTSHRD_Size_FK).SI_Description;
                            t2r.Quantity = Row.TLCUTSHRD_BundleQty;
                            dataTable2.AddDataTable2Row(t2r);
                        }

                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);
                StockBundleStore BS = new StockBundleStore();
                BS.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BS;
            }
            else if (_RepNo == 10) // Berribi Check Results 
            {
                DataSet ds = new DataSet();
                DataSet10.DataTable1DataTable dataTable1 = new DataSet10.DataTable1DataTable();
                DataSet10.DataTable2DataTable dataTable2 = new DataSet10.DataTable2DataTable();

                _repo = new CuttingRepository();
                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                    {   new string[] {"Text3", "."},
                        new string[] {"Text4", "Bundle"},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text15", string.Empty}
                    };

                using (var context = new TTI2Entities())
                {
                    var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                    if (Depts != null)
                    {
                        var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                        foreach (var Reason in Reasons)
                        {
                            foreach (var Column in ColumnNames)
                            {
                                if (String.IsNullOrEmpty(Column[1]))
                                {
                                    Column[1] = Reason.QD_Description;
                                    break;
                                }
                            }

                        }

                        DataSet10.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.FromDate = _parms.FromDate;
                        nr.ToDate = _parms.ToDate;
                        nr.ReportTitle = "Berribi Check Results";
                        dataTable1.AddDataTable1Row(nr);

                        var ResultsGroup = _repo.SelectQCBerrie(_parms).GroupBy(x => x.TLQCFB_CutSheetReceipt_FK);
                        foreach (var ResGrp in ResultsGroup)
                        {
                            var CSR = context.TLCUT_CutSheetReceipt.Find(ResGrp.FirstOrDefault().TLQCFB_CutSheetReceipt_FK);
                            if (CSR != null)
                            {
                                if (_parms.Machines.Count != 0)
                                {
                                    var value = _parms.Machines.FirstOrDefault(x => x.MD_Pk == CSR.TLCUTSHR_Machine_FK);
                                    if (value == null)
                                        continue;
                                }
                                var CS = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK);
                                if (CS != null)
                                {
                                    if (_parms.Qualities.Count != 0)
                                    {
                                        var value = _parms.Qualities.FirstOrDefault(x => x.TLGreige_Id == CS.TLCutSH_Quality_FK);
                                        if (value == null)
                                            continue;
                                    }

                                    var Cnt = 0;
                                    foreach (var Result in ResGrp)
                                    {
                                        DataSet10.DataTable2Row hxr = dataTable2.NewDataTable2Row();
                                        hxr.Inspector = context.TLADM_MachineOperators.Find(Result.TLQCFB_Operator_FK).MachOp_Description;
                                        Cnt += 1;
                                        hxr.BundleId = CS.TLCutSH_No + "-" + (Cnt.ToString().PadLeft(2, '0'));
                                        hxr.DataColumn1 = Result.TLQCFB_Measure1;
                                        hxr.DataColumn2 = Result.TLQCFB_Measure2;
                                        hxr.DataColumn3 = Result.TLQCFB_Measure3;
                                        hxr.DataColumn4 = Result.TLQCFB_Measure4;
                                        hxr.DataColumn5 = Result.TLQCFB_Measure5;
                                        hxr.DataColumn6 = Result.TLQCFB_Measure6;
                                        hxr.DataColumn7 = Result.TLQCFB_Measure7;
                                        hxr.DataColumn8 = Result.TLQCFB_Measure8;
                                        hxr.DataColumn9 = Result.TLQCFB_Measure9;
                                        hxr.DataColumn10 = Result.TLQCFB_Measure10;
                                        hxr.DataColumn11 = Result.TLQCFB_Measure11;
                                        hxr.Machine = _Machines.FirstOrDefault(s => s.MD_Pk == CSR.TLCUTSHR_Machine_FK).MD_Description;
                                        hxr.CutSheet = CS.TLCutSH_No;
                                        dataTable2.AddDataTable2Row(hxr);
                                    }
                                }
                            }
                        }
                    }
                }

                ds.Tables.Add(dataTable1);
                if (dataTable2.Rows.Count == 0)
                {
                    DataSet10.DataTable2Row hxr = dataTable2.NewDataTable2Row();
                    hxr.ErrorLog = "No records matched the selection made";
                    dataTable2.AddDataTable2Row(hxr);
                }
                ds.Tables.Add(dataTable2);

                QCBerrie xtst = new QCBerrie();
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

                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;
            }
            else if (_RepNo == 11) // Cut Sheet Summary Report and Consumables report Single. Vversion 12 does Multiple  
            {
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable datatable = new DataSet11.DataTable1DataTable();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {
                    //Set up the column names for the report relative to the sizes 
                    //----------------------------------------------------------------
                    string[][] ColumnNames = null;

                    ColumnNames = new string[][]
                    {   new string[] {"Text2", string.Empty},
                        new string[] {"Text3", string.Empty},
                        new string[] {"Text4", string.Empty},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text19", string.Empty}

                    };


                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }

                    /*var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).GroupBy(x => x.SI_ColNumber).ToList();
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

                    //-----------End of sizes set up
                    //-----------------------------------------------------------------
                    var CS = context.TLCUT_CutSheet.Find(_KeyValue);
                    if (CS != null)
                    {
                        var DO = new TLDYE_DyeOrder();

                        var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                        }
                        var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == _KeyValue).FirstOrDefault();
                        if (CSR != null)
                        {
                            DataSet11.DataTable1Row nr = datatable.NewDataTable1Row();
                            nr.CutSheet = CS.TLCutSH_No;
                            nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK).Col_Display;
                            var Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK);
                            nr.Cotton = 0;
                            nr.Bags = 0;
                            nr.Binding = 0;
                            nr.Boxes = 0;
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

                            if (DO != null)
                            {
                                nr.Label = context.TLADM_Labels.Find(DO.TLDYO_Label_FK).Lbl_Description;
                            }

                            var CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                            var TotalUnits = CSRD.Sum(x => (int?)x.TLCUTSHRD_BoxUnits - x.TLCUTSHRD_RejectQty) ?? 0;
                            if (TotalUnits != 0)
                            {
                                if (Style != null)
                                {
                                    nr.Style = Style.Sty_Description;
                                    if (Style.Sty_CottonFactor != 0)
                                    {
                                        nr.Cotton = (int)(TotalUnits * 120) / Style.Sty_CottonFactor;
                                    }
                                    if (Style.Sty_Bags != 0)
                                    {
                                        nr.Bags = (int)TotalUnits / Style.Sty_Bags;
                                    }
                                }
                            }
                            var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_Pk == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (CSB != null)
                            {
                                nr.Boxes = CSB.TLCUTSHB_AdultBoxes + CSB.TLCUTSHB_KidBoxes;
                                nr.Binding = CSB.TLCUTSHB_Binding;
                            }
                            var SizeGrps = CSRD.GroupBy(x => x.TLCUTSHRD_Size_FK);

                            foreach (var grp in SizeGrps)
                            {
                                var SizePk = grp.FirstOrDefault().TLCUTSHRD_Size_FK;

                                var ColNo = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_ColNumber;
                                var GrpTotal = 0;
                                try
                                {
                                    GrpTotal = grp.Sum(x => x.TLCUTSHRD_BoxUnits - x.TLCUTSHRD_RejectQty);
                                }
                                catch (Exception ex)
                                {
                                    GrpTotal = 0;
                                }

                                if (ColNo == 1)
                                    nr.Col1 = GrpTotal;
                                else if (ColNo == 2)
                                    nr.Col2 = GrpTotal;
                                else if (ColNo == 3)
                                    nr.Col3 = GrpTotal;
                                else if (ColNo == 4)
                                    nr.Col4 = GrpTotal;
                                else if (ColNo == 5)
                                    nr.Col5 = GrpTotal;
                                else if (ColNo == 6)
                                    nr.Col6 = GrpTotal;
                                else if (ColNo == 7)
                                    nr.Col7 = GrpTotal;
                                else if (ColNo == 8)
                                    nr.Col8 = GrpTotal;
                                else if (ColNo == 9)
                                    nr.Col9 = GrpTotal;
                                else if (ColNo == 10)
                                    nr.Col10 = GrpTotal;
                                else
                                    nr.Col11 = GrpTotal;
                            }

                            nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                            datatable.AddDataTable1Row(nr);
                        }
                    }
                    //------------write up the report 
                    ds.Tables.Add(datatable);
                    CRSummary xtst = new CRSummary();
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
                    //---------------------------------------------------

                    crystalReportViewer1.ReportSource = xtst;
                }


            }
            else if (_RepNo == 12)        // Cut Sheet Summary Report and Consumables report  Multiple version
            {
                DataSet ds1 = new DataSet();
                DataSet11.DataTable1DataTable datatable1 = new DataSet11.DataTable1DataTable();
                DataSet11.DataTable2DataTable datatable2 = new DataSet11.DataTable2DataTable();
                DataSet11.DataTable3DataTable datatable3 = new DataSet11.DataTable3DataTable();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {
                    //Set up the column names for the report relative to the sizes 
                    //----------------------------------------------------------------
                    string[][] ColumnNames = null;

                    ColumnNames = new string[][]
                    {   new string[] {"Text2", string.Empty},
                        new string[] {"Text3", string.Empty},
                        new string[] {"Text4", string.Empty},
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text19", string.Empty}

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
                    //-----------End of sizes set up
                     */

                    TLCMT_PanelIssue pi = context.TLCMT_PanelIssue.Find(_KeyValue);
                    if (pi != null)
                    {
                        var pidetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == pi.CMTPI_Pk).ToList();
                        foreach (var item in pidetail)
                        {
                            var CSR = context.TLCUT_CutSheetReceipt.Find(item.CMTPID_CutSheet_FK);
                            if (CSR != null)
                            {
                                var CS = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK);
                                if (CS != null)
                                {
                                    var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                                    if (DB != null)
                                    {
                                        var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);

                                        DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();
                                        nr.CutSheet = CS.TLCutSH_No;
                                        var Clr = _Colours.FirstOrDefault(s => s.Col_Id == CS.TLCutSH_Colour_FK);
                                        if (Clr != null)
                                            nr.Colour = Clr.Col_Display;
                                        else
                                            nr.Colour = string.Empty;

                                        var Style = _Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK);
                                        nr.TransNumber = pi.CMTPI_Number;
                                        nr.Cotton = 0;
                                        nr.Bags = 0;
                                        nr.Binding = 0;
                                        nr.Boxes = 0;
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


                                        if (DO != null)
                                        {
                                            nr.Label = context.TLADM_Labels.Find(DO.TLDYO_Label_FK).Lbl_Description;
                                        }

                                        var CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();

                                        var TotalUnits = CSRD.Sum(x => (int?)x.TLCUTSHRD_BoxUnits - x.TLCUTSHRD_RejectQty) ?? 0;
                                        if (TotalUnits != 0)
                                        {
                                            if (Style != null)
                                            {
                                                nr.Style = Style.Sty_Description;
                                                if (Style.Sty_CottonFactor != 0)
                                                {
                                                    nr.Cotton = (int)(TotalUnits * 120) / Style.Sty_CottonFactor;
                                                }
                                                if (Style.Sty_Bags != 0)
                                                {
                                                    nr.Bags = (int)TotalUnits / Style.Sty_Bags;
                                                }
                                            }
                                        }
                                        var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_Pk == CSR.TLCUTSHR_Pk).FirstOrDefault();
                                        if (CSB != null)
                                        {
                                            nr.Boxes = CSB.TLCUTSHB_AdultBoxes + CSB.TLCUTSHB_KidBoxes;
                                            nr.Binding = CSB.TLCUTSHB_Binding;
                                        }
                                        var SizeGrps = CSRD.GroupBy(x => x.TLCUTSHRD_Size_FK);

                                        foreach (var grp in SizeGrps)
                                        {
                                            var SizePk = grp.FirstOrDefault().TLCUTSHRD_Size_FK;
                                            var ColNo = _Sizes.FirstOrDefault(s => s.SI_id == SizePk).SI_ColNumber;
                                            int GrpTotal = 0;

                                            try
                                            {
                                                GrpTotal = grp.Sum(x => x.TLCUTSHRD_BoxUnits - x.TLCUTSHRD_RejectQty);
                                            }
                                            catch (Exception ex)
                                            {
                                                GrpTotal = 0;
                                            }
                                            if (ColNo == 1)
                                                nr.Col1 = GrpTotal;
                                            else if (ColNo == 2)
                                                nr.Col2 = GrpTotal;
                                            else if (ColNo == 3)
                                                nr.Col3 = GrpTotal;
                                            else if (ColNo == 4)
                                                nr.Col4 = GrpTotal;
                                            else if (ColNo == 5)
                                                nr.Col5 = GrpTotal;
                                            else if (ColNo == 6)
                                                nr.Col6 = GrpTotal;
                                            else if (ColNo == 7)
                                                nr.Col7 = GrpTotal;
                                            else if (ColNo == 8)
                                                nr.Col8 = GrpTotal;
                                            else if (ColNo == 9)
                                                nr.Col9 = GrpTotal;
                                            else if (ColNo == 10)
                                                nr.Col10 = GrpTotal;
                                            else
                                                nr.Col11 = GrpTotal;

                                            nr.PanelTotal = GrpTotal;
                                        }

                                        nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;

                                        if (_Styles.FirstOrDefault(s => s.Sty_Id == CS.TLCutSH_Styles_FK).Sty_Buttons == true)
                                        {
                                            nr.Buttons = (int)(nr.Total * 2 * 1.05);
                                            nr.ButtonWeight = (decimal)(nr.Buttons * 0.23) / 100;
                                        }
                                        else
                                        {
                                            nr.Buttons = 0;
                                            nr.ButtonWeight = 0.00M;
                                        }

                                        datatable1.AddDataTable1Row(nr);

                                        // This table keeps a record of the Bags statistical data
                                        DataSet11.DataTable2Row t2 = datatable2.NewDataTable2Row();
                                        t2.Labels = nr.Label;
                                        t2.BagCount = nr.Bags;
                                        datatable2.AddDataTable2Row(t2);

                                        // This table keeps a record of the Cottons statistical data
                                        DataSet11.DataTable3Row t3 = datatable3.NewDataTable3Row();
                                        t3.Colour = nr.Colour;
                                        t3.ConesCount = nr.Cotton;
                                        t3.Boxes = (int)(nr.Cotton / 120);
                                        datatable3.AddDataTable3Row(t3);

                                    }
                                }
                            }
                        }
                        //---------------------------------------------------

                    }

                    //------------write up the report 
                    DataView DataV = datatable1.DefaultView;
                    DataV.Sort = "style";
                    ds1.Tables.Add(DataV.ToTable());
                    ds1.Tables.Add(datatable2);
                    ds1.Tables.Add(datatable3);

                    CRListSummary xtst = new CRListSummary();
                    xtst.SetDataSource(ds1);
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
                }
            }
            else if (_RepNo == 13)  // this printsout the CMT Panel Issue Document that must accompany the truck
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable dataTable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable dataTable2 = new DataSet13.DataTable2DataTable();
                Util core = new Util();
                int _Lable = 0;

                using (var context = new TTI2Entities())
                {
                    DataSet13.DataTable1Row nr = dataTable1.NewDataTable1Row();
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

                    nr.Line = string.Empty;            //  FactConfig.TLCMTCFG_LineNo.ToString();
                    nr.LineDescription = string.Empty; //  FactConfig.TLCMTCFG_Description; 

                    TLCMT_PanelIssue pi = context.TLCMT_PanelIssue.Find(_KeyValue);
                    //if (pi != null)
                    //{
                    // var pidetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == pi.CMTPI_Pk).ToList();
                    // foreach (var item in pidetail)
                    // {
                    var CutSheet = context.TLCUT_CutSheet.Find(_KeyValue);
                    if (CutSheet != null)
                    {
                        nr.Line = string.Empty;            //  FactConfig.TLCMTCFG_LineNo.ToString();
                        nr.LineDescription = string.Empty; //  FactConfig.TLCMTCFG_Description; */

                        nr.Pk = CutSheet.TLCutSH_Pk;

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

                            nr.Date = DateTime.Now;

                            dataTable1.AddDataTable1Row(nr);

                            //var CutSheetDetails = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).ToList();

                            var CutSheetDetails = from T1 in context.TLCUT_CutSheetReceiptDetail
                                                  join T2 in context.TLADM_Sizes on T1.TLCUTSHRD_Size_FK equals T2.SI_id
                                                  where T1.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk
                                                  orderby T2.SI_DisplayOrder
                                                  select T1;


                            foreach (var CutSheetDetail in CutSheetDetails)
                            {
                                DataSet13.DataTable2Row dt2 = dataTable2.NewDataTable2Row();
                                dt2.Pk = CutSheetDetail.TLCUTSHRD_CutSheet_FK;
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
                        // }

                        // }
                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                CutPanelIssue fcon = new CutPanelIssue();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

            }
            else if (_RepNo == 14)  // this prints out a singleC CMT Panel Issue Document that must be given to Mary to begin processing
            {
                DataSet ds = new DataSet();
                DataSet13.DataTable1DataTable dataTable1 = new DataSet13.DataTable1DataTable();
                DataSet13.DataTable2DataTable dataTable2 = new DataSet13.DataTable2DataTable();
                Util core = new Util();
                int _Lable = 0;
                int _Style = 0;
                string _Notes = string.Empty;

                string[][] ColumnNames = null;

                using (var context = new TTI2Entities())
                {

                    DataSet13.DataTable1Row nr = dataTable1.NewDataTable1Row();
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

                    nr.Line = string.Empty;            //  FactConfig.TLCMTCFG_LineNo.ToString();
                    nr.LineDescription = string.Empty; //  FactConfig.TLCMTCFG_Description;

                    var CutSheet = context.TLCUT_CutSheet.Find(_KeyValue);
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
                            _Notes = DyeBatch.DYEB_Notes + Environment.NewLine;

                            if (CutSheet.TLCUTSH_AddNotes != null && CutSheet.TLCUTSH_AddNotes.Length != 0)
                            {
                                _Notes += CutSheet.TLCUTSH_AddNotes + Environment.NewLine;
                            }

                            nr.Notes = _Notes;

                            var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                            if (DyeOrder != null)
                            {
                                nr.BodyQuality = _Qualities.FirstOrDefault(s => s.TLGreige_Id == DyeOrder.TLDYO_Greige_FK).TLGreige_Description;
                                nr.Order = DyeOrder.TLDYO_DyeOrderNum;
                                var DtReq = core.FirstDateOfWeek(DyeOrder.TLDYO_OrderDate.Year, DyeOrder.TLDYO_CMTReqWeek);
                                nr.DateRequired = DtReq.AddDays(5);
                                _Lable = DyeOrder.TLDYO_Label_FK;
                                _Style = CutSheet.TLCutSH_Styles_FK;

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
                            var AdultBoxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_AdultBoxes;
                            var KidsBoxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_KidBoxes;

                            nr.Boxes = AdultBoxes + KidsBoxes;
                            nr.Ribbing = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Ribbing;
                            nr.Binding = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault().TLCUTSHB_Binding;

                            var CutSheetDetails = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).ToList();

                            foreach (var CutSheetDetail in CutSheetDetails)
                            {
                                DataSet13.DataTable2Row dt2 = dataTable2.NewDataTable2Row();

                                dt2.Bundle = CutSheetDetail.TLCUTSHRD_Description;
                                dt2.Size = _Sizes.FirstOrDefault(s => s.SI_id == CutSheetDetail.TLCUTSHRD_Size_FK).SI_Description;
                                dt2.Style = _Styles.FirstOrDefault(s => s.Sty_Id == _Style).Sty_Description;
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

                CutPanelIssue fcon = new CutPanelIssue();
                fcon.SetDataSource(ds);
                crystalReportViewer1.ReportSource = fcon;

                ColumnNames = new string[][]
                {
                    new string[] {"Text18", _Notes}
                };

                IEnumerator ie = fcon.DetailSection1.ReportObjects.GetEnumerator();
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
            else if (_RepNo == 15)  // this prints out the CutSheet Register for Bossie
            {
                DataSet ds = new DataSet();
                DataSet14.DataTable1DataTable dataTable1 = new DataSet14.DataTable1DataTable();
                DataSet14.DataTable2DataTable dataTable2 = new DataSet14.DataTable2DataTable();
                Util core = new Util();
                _repo = new CuttingRepository();


                var T2 = dataTable2.NewDataTable2Row();
                T2.Pk = 1;
                T2.FromDate = _parms.FromDate;
                T2.ToDate = _parms.ToDate;
                T2.Title = "CutSheet Register By Location";
                dataTable2.AddDataTable2Row(T2);


                using (var context = new TTI2Entities())
                {
                    var Shipments = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Date >= _parms.FromDate && x.CMTPI_Date <= _parms.ToDate && x.CMTPI_Closed).ToList();

                    foreach (var Shipment in Shipments)
                    {

                        var Details = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == Shipment.CMTPI_Pk).ToList();
                        foreach (var Detail in Details)
                        {
                            var tst = (from CutSheetRec in context.TLCUT_CutSheetReceipt
                                       join CutSheet in context.TLCUT_CutSheet on CutSheetRec.TLCUTSHR_CutSheet_FK equals CutSheet.TLCutSH_Pk
                                       where CutSheetRec.TLCUTSHR_Pk == Detail.CMTPID_CutSheet_FK
                                       select CutSheet).FirstOrDefault();
                            if (tst != null)
                            {
                                var T1 = dataTable1.NewDataTable1Row();
                                T1.Pk = 1;
                                T1.CutSheet = tst.TLCutSH_No;
                                T1.Date = Shipment.CMTPI_Date;
                                T1.Location = context.TLADM_Departments.Find(tst.TLCutSH_Department_FK).Dep_Description;
                                T1.Style = _Styles.FirstOrDefault(s => s.Sty_Id == tst.TLCutSH_Styles_FK).Sty_Description;
                                T1.Colour = _Colours.FirstOrDefault(s => s.Col_Id == tst.TLCutSH_Colour_FK).Col_Display;
                                T1.Delivery_Number = Shipment.CMTPI_DeliveryNumber;
                                T1.Picking_Number = Shipment.CMTPI_Number;
                                dataTable1.AddDataTable1Row(T1);
                            }

                        }
                    }
                }

                if (dataTable1.Rows.Count == 0)
                {
                    var T1 = dataTable1.NewDataTable1Row();
                    T1.Pk = 1;
                    T1.ErrorLog = "No records found matching selection criteria";
                    dataTable1.AddDataTable1Row(T1);
                }


                DataView DataV = dataTable1.DefaultView;
                DataV.Sort = "CutSheet";
                ds.Tables.Add(dataTable2);
                ds.Tables.Add(DataV.ToTable());

                CutSheetRegister Register = new CutSheetRegister();
                Register.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Register;

            }
            else if (_RepNo == 16)      // Fabric Store Picking List 
            {
                DataSet ds = new DataSet();
                DataSet15.DataTable1DataTable dataTable1 = new DataSet15.DataTable1DataTable();
                DataSet15.DataTable2DataTable dataTable2 = new DataSet15.DataTable2DataTable();

                using (var context = new TTI2Entities())
                {
                    var BIFTransit = context.TLDYE_BIFInTransit.Find(_KeyValue);
                    if (BIFTransit != null)
                    {
                        DataSet15.DataTable1Row hrw = dataTable1.NewDataTable1Row();
                        hrw.Date = DateTime.Now;
                        hrw.Pk = 1;
                        hrw.TransNumber = BIFTransit.BIFT_PickingList_Number;
                        hrw.WareHouse = context.TLADM_WhseStore.Find(BIFTransit.BIFT_FromFabric_FK).WhStore_Description;
                        hrw.ToWareHouse = context.TLADM_WhseStore.Find(BIFTransit.BIFT_ToFabric_FK).WhStore_Description;

                        dataTable1.AddDataTable1Row(hrw);

                        var Entries = context.TLDYE_BIFInTransitDetails.Where(x => x.BIFD_Intransit_FK == BIFTransit.BIFT_Pk).ToList();

                        foreach (var Entry in Entries)
                        {
                            DataSet15.DataTable2Row nr = dataTable2.NewDataTable2Row();
                            nr.Pk = 1;
                            var GreigeProd = context.TLKNI_GreigeProduction.Find(Entry.BIFD_Greige_FK);
                            if (GreigeProd != null)
                            {
                                nr.PieceNumber = GreigeProd.GreigeP_PieceNo;
                                nr.Weight = GreigeProd.GreigeP_weight;
                                nr.Description = _Qualities.FirstOrDefault(s => s.TLGreige_Id == GreigeProd.GreigeP_Greige_Fk).TLGreige_Description;
                                if (Entry.BIFD_Colour_FK != 0)
                                    nr.Colour = _Colours.FirstOrDefault(s => s.Col_Id == Entry.BIFD_Colour_FK).Col_Display;

                            }
                            dataTable2.AddDataTable2Row(nr);
                        }

                    }
                }
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                BIFInterTransfer InterTrans = new BIFInterTransfer();
                InterTrans.SetDataSource(ds);
                crystalReportViewer1.ReportSource = InterTrans;

            }
            else if (_RepNo == 17)      // Fabric Store Delivery Notes 
            {
                DataSet ds = new DataSet();
                DataSet16.TLDYE_BIFInTransitDataTable Table1 = new DataSet16.TLDYE_BIFInTransitDataTable();
                DataSet16.TLDYE_BIFInTransitDetailsDataTable Table2 = new DataSet16.TLDYE_BIFInTransitDetailsDataTable();
                DataSet16.TLADM_WhseStoreDataTable FromWhse = new DataSet16.TLADM_WhseStoreDataTable();
                DataSet16.TLADM_WhseStore1DataTable ToWhse = new DataSet16.TLADM_WhseStore1DataTable();

                using (var context = new TTI2Entities())
                {
                    foreach (var ItemPk in _repopts.BIFTransit)
                    {
                        var BIFTrans = context.TLDYE_BIFInTransit.Find(ItemPk);
                        if (BIFTrans != null)
                        {
                            DataSet16.TLDYE_BIFInTransitRow HeaderRow = Table1.NewTLDYE_BIFInTransitRow();
                            HeaderRow.BIFT_Despatch_FK = BIFTrans.BIFT_Despatch_FK;
                            HeaderRow.BIFT_Despatched = BIFTrans.BIFT_Despatched;
                            HeaderRow.BIFT_Despatched_Date = (DateTime)BIFTrans.BIFT_Despatched_Date;
                            HeaderRow.BIFT_FromFabric_FK = BIFTrans.BIFT_FromFabric_FK;
                            HeaderRow.BIFT_PickingList = BIFTrans.BIFT_PickingList;
                            HeaderRow.BIFT_PickingList_Date = BIFTrans.BIFT_PickingList_Date;
                            HeaderRow.BIFT_PickingList_Number = BIFTrans.BIFT_PickingList_Number;
                            HeaderRow.BIFT_Pk = BIFTrans.BIFT_Pk;
                            if (BIFTrans.BIFT_Receipt_Date != null)
                                HeaderRow.BIFT_Receipt_Date = (DateTime)BIFTrans.BIFT_Receipt_Date;
                            HeaderRow.BIFT_Receipted = BIFTrans.BIFT_Receipted;
                            HeaderRow.BIFT_ToFabric_FK = BIFTrans.BIFT_ToFabric_FK;
                            HeaderRow.BIFT_Delivery_Number = BIFTrans.BIFT_Delivery_Number;

                            Table1.AddTLDYE_BIFInTransitRow(HeaderRow);

                            var FWhse = FromWhse.FindByWhStore_Id(HeaderRow.BIFT_FromFabric_FK);
                            if (FWhse == null)
                            {
                                var Whse = context.TLADM_WhseStore.Find(HeaderRow.BIFT_FromFabric_FK);
                                if (Whse != null)
                                {
                                    DataSet16.TLADM_WhseStoreRow FromWhseRow = FromWhse.NewTLADM_WhseStoreRow();
                                    FromWhseRow.WhStore_Description = Whse.WhStore_Description;
                                    FromWhseRow.WhStore_Id = Whse.WhStore_Id;
                                    FromWhse.AddTLADM_WhseStoreRow(FromWhseRow);

                                }
                            }

                            var ToWh = ToWhse.FindByWhStore_Id(HeaderRow.BIFT_ToFabric_FK);
                            if (ToWh == null)
                            {
                                var Whse = context.TLADM_WhseStore.Find(HeaderRow.BIFT_ToFabric_FK);
                                if (Whse != null)
                                {
                                    DataSet16.TLADM_WhseStore1Row FromWhseRow = ToWhse.NewTLADM_WhseStore1Row();
                                    FromWhseRow.WhStore_Description = Whse.WhStore_Description;
                                    FromWhseRow.WhStore_Id = Whse.WhStore_Id;
                                    ToWhse.AddTLADM_WhseStore1Row(FromWhseRow);
                                }
                            }

                            var BIFTransDetails = context.TLDYE_BIFInTransitDetails.Where(x => x.BIFD_Intransit_FK == BIFTrans.BIFT_Pk).ToList();
                            foreach (var Detail in BIFTransDetails)
                            {
                                DataSet16.TLDYE_BIFInTransitDetailsRow DetailRow = Table2.NewTLDYE_BIFInTransitDetailsRow();
                                DetailRow.BIFD_Colour_FK = Detail.BIFD_Colour_FK;
                                DetailRow.BIFD_DyeBatchDetail_FK = Detail.BIFD_DyeBatchDetail_FK;
                                DetailRow.BIFD_Intransit_FK = BIFTrans.BIFT_Pk;
                                if (DetailRow.BIFD_Colour_FK > 0)
                                    DetailRow.BIFD_Colour_Description = _Colours.FirstOrDefault(s => s.Col_Id == DetailRow.BIFD_Colour_FK).Col_Display;
                                else
                                    DetailRow.BIFD_Colour_Description = "Unknown";

                                var GreigeProd = context.TLKNI_GreigeProduction.Find(Detail.BIFD_Greige_FK);
                                if (GreigeProd != null)
                                {
                                    DetailRow.BIFD_Greige_Description = _Qualities.FirstOrDefault(s => s.TLGreige_Id == GreigeProd.GreigeP_Greige_Fk).TLGreige_Description;
                                    DetailRow.BIFD_Greige_FK = (int)GreigeProd.GreigeP_Greige_Fk;
                                }
                                var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Find(Detail.BIFD_DyeBatchDetail_FK);
                                if (DyeBatchDetail != null)
                                {
                                    DetailRow.BIFD_Piece_No = context.TLKNI_GreigeProduction.Find(DyeBatchDetail.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                                    DetailRow.BIFD_Weight_Available = DyeBatchDetail.DYEBD_GreigeProduction_Weight;

                                }

                                Table2.AddTLDYE_BIFInTransitDetailsRow(DetailRow);
                            }
                        }
                    }
                }

                ds.Tables.Add(Table1);
                ds.Tables.Add(Table2);
                ds.Tables.Add(FromWhse);
                ds.Tables.Add(ToWhse);

                BIFDeliveryNote InterTrans = new BIFDeliveryNote();
                InterTrans.SetDataSource(ds);
                crystalReportViewer1.ReportSource = InterTrans;


            }
            else if (_RepNo == 18)      // CutSheet Required Days Delay 
            {
                DataSet ds = new DataSet();
                IList<TLCUT_CutSheet> CutSheets = null;
                Util core = new Util();

                DataSet17.DataTable1DataTable dataTable1 = new DataSet17.DataTable1DataTable();
                DataSet17.DataTable2DataTable dataTable2 = new DataSet17.DataTable2DataTable();
                using (var context = new TTI2Entities())
                {
                    if (_parms.ProductionResults)
                        CutSheets = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_Closed && x.TLCutSH_WIPComplete && x.TLCUTSH_RequiredDate >= _parms.FromDate && x.TLCUTSH_RequiredDate <= _parms.ToDate).ToList();
                    else
                        CutSheets = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_Closed && !x.TLCutSH_WIPComplete && x.TLCUTSH_RequiredDate >= _parms.FromDate && x.TLCUTSH_RequiredDate <= _parms.ToDate).ToList();

                    foreach (var CutSheet in CutSheets)
                    {
                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = 1;
                        nr.CutSheet_Number = CutSheet.TLCutSH_No;
                        nr.CutSheet_Required_Date = (DateTime)CutSheet.TLCUTSH_RequiredDate;
                        nr.CutSheet_Days = 0;
                        nr.CutSheet_Style = _Styles.FirstOrDefault(s => s.Sty_Id == CutSheet.TLCutSH_Styles_FK).Sty_Description;
                        nr.CutSheet_Colour = _Colours.FirstOrDefault(s => s.Col_Id == CutSheet.TLCutSH_Colour_FK).Col_Display;
                        nr.CutSheet_Units = 0;
                        var ExpectedUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                        if (ExpectedUnits.Count != 0)
                        {
                            nr.CutSheet_Units = ExpectedUnits.Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0;
                        }
                        if (_parms.ProductionResults)
                        {
                            nr.CutSheet_Completed_Date = (DateTime)CutSheet.TLCUTSH_Completed_Date;
                            if ((DateTime)CutSheet.TLCUTSH_Completed_Date != null && (DateTime)CutSheet.TLCUTSH_RequiredDate != null)
                            {
                                nr.CutSheet_Days = core.GetWorkingDays((DateTime)CutSheet.TLCUTSH_RequiredDate.Value, (DateTime)CutSheet.TLCUTSH_Completed_Date.Value);
                            }
                        }

                        dataTable1.AddDataTable1Row(nr);
                    }
                    if (!_parms.ProductionResults)
                    {
                        // We have to include Dye Batches if not production results
                        //===================================
                        var DyeBatches = context.TLDYE_DyeBatch.Where(x => !x.DYEB_CommissinCust && !x.DYEB_Closed && x.DYEB_OutProcess && x.DYEB_RequiredDate >= _parms.FromDate && x.DYEB_RequiredDate <= _parms.ToDate).ToList();
                        foreach (var DyeBatch in DyeBatches)
                        {
                            // Get The Data 
                            //======================================================
                            IList<TLDYE_DyeBatchDetails> DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).ToList();

                            //1st Step count sum the kgs sent to Cutting if Already done so
                            //=============================================================================
                            var CutSheetKg = DBDetails.Where(x => x.DYEBO_CutSheet).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                            //2nd Step sum the kgs Approved but not sent to cutting
                            //=============================================================================
                            var ApprovedKg = DBDetails.Where(x => !x.DYEBO_CutSheet && x.DYEBO_QAApproved).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                            //3rd Step sum that maybe in Quarantine Store
                            //=============================================================================
                            var QuarantineKg = DBDetails.Where(x => !x.DYEBO_CutSheet && !x.DYEBO_QAApproved && !x.DYEBO_Rejected).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                            //4th Step sum the Kgs that may have been sold 
                            //=================================================
                            var SoldKg = DBDetails.Where(x => x.DYEBO_Sold).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;

                            //5th Step sum the Rejected Kgs 
                            //=================================================
                            var RejectedKg = DBDetails.Where(x => x.DYEBO_Rejected).Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.0M;

                            Decimal Production = (ApprovedKg + QuarantineKg) - (CutSheetKg + SoldKg + RejectedKg);

                            if (Production > 0.0M)
                            {
                                var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                if (DyeOrder != null)
                                {
                                    var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                                    if (DyeOrderDetail != null)
                                    {
                                        var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                                        var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                                        var ExpectedUnits = 0;

                                        try
                                        {
                                            ExpectedUnits = Convert.ToInt32(FabricYield / FabricRating * Production);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExpectedUnits = 0;
                                        }

                                        DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                                        nr.Pk = 1;
                                        nr.CutSheet_Number = DyeBatch.DYEB_BatchNo;
                                        nr.CutSheet_Required_Date = (DateTime)DyeBatch.DYEB_RequiredDate;
                                        nr.CutSheet_Days = 0;
                                        nr.CutSheet_Style = _Styles.FirstOrDefault(s => s.Sty_Id == DyeOrder.TLDYO_Style_FK).Sty_Description;
                                        nr.CutSheet_Colour = _Colours.FirstOrDefault(s => s.Col_Id == DyeOrder.TLDYO_Colour_FK).Col_Display;
                                        nr.CutSheet_Units = ExpectedUnits;


                                        dataTable1.AddDataTable1Row(nr);

                                    }
                                }
                            }

                        }
                    }
                }

                DataSet17.DataTable2Row hnr = dataTable2.NewDataTable2Row();

                hnr.Pk = 1;
                hnr.FromDate = _parms.FromDate;
                hnr.ToDate = _parms.ToDate;
                if (_parms.ProductionResults)
                    hnr.Title = "Cut Sheet Production analysis on required dates";
                else
                    hnr.Title = "Cut Sheet Required Dates";

                if (dataTable1.Rows.Count == 0)
                {
                    hnr.ErrorLog = "No entries found for dates selected";

                    DataSet17.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.Pk = 1;
                    dataTable1.AddDataTable1Row(nr);
                }

                dataTable2.AddDataTable2Row(hnr);

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ProductionDays ProdDays = new ProductionDays();
                ProdDays.SetDataSource(ds);
                crystalReportViewer1.ReportSource = ProdDays;
            }
            else if (_RepNo == 19)   // BIF Receipts 
            {
                DataSet ds = new DataSet();
                DataSet20.DataTable1DataTable dataTable = new DataSet20.DataTable1DataTable();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {
                    var Entries = context.TLKNI_BoughtInFabric.Where(x => x.TLBIN_TransNumber == _parms.BIFTransNumber).ToList();
                    foreach (var Entry in Entries)
                    {
                        DataSet20.DataTable1Row nr = dataTable.NewDataTable1Row();
                        nr.TTS_PieceNo = Entry.TLBIN_TTS_PN;
                        nr.Their_Piece_No = Entry.TLBIN_Their_PN;
                        nr.NettWeight = Entry.TLBIN_Nett_Weight;
                        nr.Store = context.TLADM_WhseStore.Find(Entry.TLBIN_CurrentStore_FK).WhStore_Description;
                        nr.Quality = context.TLADM_Griege.Find(Entry.TLBIN_Greige_FK).TLGreige_Description;
                        nr.Colour = context.TLADM_Colours.Find(Entry.TLBIN_Colour_FK).Col_Display;
                        nr.TransDate = Entry.TLBIN_TransDate;
                        nr.TransNo = _parms.BIFTransNumber;
                        nr.MetersPerRoll = Entry.TLBIN_Meters_Roll;

                        dataTable.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable);

                BIFReceipts BIFReceipts = new BIFReceipts();
                BIFReceipts.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BIFReceipts;

            }
            else if (_RepNo == 20)      // BIF Lables to go on each roll  
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable = new DataSet19.DataTable1DataTable();
                Util core = new Util();

                using (var context = new TTI2Entities())
                {
                    var Entries = context.TLKNI_BoughtInFabric.Where(x => x.TLBIN_TransNumber == _parms.BIFTransNumber).ToList();
                    foreach (var Entry in Entries)
                    {
                        DataSet19.DataTable1Row nr = dataTable.NewDataTable1Row();

                        nr.PieceNo = Entry.TLBIN_TTS_PN;
                        nr.Their_Piece_No = Entry.TLBIN_Their_PN;
                        nr.Weight = Entry.TLBIN_Dsk_Weight;
                        nr.Width = Entry.TLBIN_Dsk_Width;
                        nr.MetersPerRoll = Entry.TLBIN_Meters_Roll;
                        nr.NettWeight = Entry.TLBIN_Nett_Weight;
                        nr.Colour = context.TLADM_Colours.Find(Entry.TLBIN_Colour_FK).Col_Display;

                        dataTable.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable);

                BIFTickets Tickets = new BIFTickets();
                Tickets.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Tickets;

            }
            else if (_RepNo == 21)      // Cutting On Hold Report  
            {
                DataSet ds = new DataSet();
                DataSet21.DataTable1DataTable dataTable = new DataSet21.DataTable1DataTable();
                Util core = new Util();
                string[][] ColumnNames = null;

                ColumnNames = new string[][]
                    {   new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty},
                        new string[] {"Text15", string.Empty},
                        new string[] {"Text16", string.Empty},
                        new string[] {"Text17", string.Empty},
                        new string[] {"Text18", string.Empty},
                        new string[] {"Text19", string.Empty},
                        new string[] {"Text20", string.Empty}

                    };


                using (var context = new TTI2Entities())
                {
                    var CNames = core.CreateColumnNames();
                    int i = 0;
                    foreach (var CName in CNames)
                    {
                        ColumnNames[i++][1] = CName[1];
                    }


                    var Entries = context.TLCUT_CutSheet.Where(x => x.TLCUTSH_OnHold && !x.TLCutSH_WIPComplete).OrderBy(x => x.TLCutSH_No).ToList();
                    foreach (var Entry in Entries)
                    {
                        DataSet21.DataTable1Row nr = dataTable.NewDataTable1Row();
                        nr.CutSheet = Entry.TLCutSH_No;
                        nr.Colour = context.TLADM_Colours.Find(Entry.TLCutSH_Colour_FK).Col_Display;
                        nr.Reason = Entry.TLCUTSH_OnHold_Reasons;
                        nr.Style = context.TLADM_Styles.Find(Entry.TLCutSH_Styles_FK).Sty_Description;
                        nr.Date_On_Hold = (DateTime)Entry.TLCUTSH_OnHoldDate;
                        nr.Days_On_Hold = core.GetWorkingDays((DateTime)Entry.TLCUTSH_OnHoldDate, DateTime.Now);
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
                        nr.Priority = (bool)Entry.TLCUTSH_Priority;

                        var ExpectedUnitsGrouped = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == Entry.TLCutSH_Pk).GroupBy(x => x.TLCUTE_Size_FK);
                        foreach (var Group in ExpectedUnitsGrouped)
                        {
                            var SizePk = Group.FirstOrDefault().TLCUTE_Size_FK;
                            var Size = context.TLADM_Sizes.Find(SizePk);
                            if (Size != null)
                            {
                                var EUnits = Group.Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0;
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
                            nr.Total = nr.Col1 + nr.Col2 + nr.Col3 + nr.Col4 + nr.Col5 + nr.Col6 + nr.Col7 + nr.Col8 + nr.Col9 + nr.Col10 + nr.Col11;
                        }
                        dataTable.AddDataTable1Row(nr);
                    }

                }

                if (dataTable.Rows.Count == 0)
                {
                    DataSet21.DataTable1Row nr = dataTable.NewDataTable1Row();
                    nr.Error_log = "No records found";
                    dataTable.AddDataTable1Row(nr);
                }

                ds.Tables.Add(dataTable);
                CutsheetsOnHold OnHold = new CutsheetsOnHold();
                OnHold.SetDataSource(ds);
                IEnumerator ie = OnHold.Section2.ReportObjects.GetEnumerator();
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
                crystalReportViewer1.ReportSource = OnHold;

            }
            else if (_RepNo == 22)      // Cutting On Hold Report  
            {
                DataSet ds = new DataSet();
                DataSet22.DataTable1DataTable dataTable = new DataSet22.DataTable1DataTable();
                DataSet22.DataTable2DataTable dataTable2 = new DataSet22.DataTable2DataTable();
                Util core = new Util();

                DataSet22.DataTable1Row nr = dataTable.NewDataTable1Row();
                nr.Pk = 1;
                nr.FromDate = _parms.FromDate;
                nr.ToDate = _parms.ToDate;
                nr.Title = "Cutting Waste Anaysis";
                dataTable.AddDataTable1Row(nr);

                using (var context = new TTI2Entities())
                {
                    var CutSheets = context.TLCUT_CutSheet.Where(x => x.TLCutSH_Date >= _parms.FromDate && x.TLCutSH_Date <= _parms.ToDate).ToList();
                    foreach(var CutSheet in CutSheets)
                    {
                        DataSet22.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                        xnr.Pk = 1;
                        xnr.CutSheetNo = CutSheet.TLCutSH_No;
                        var DB = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            xnr.DyeBatchNo = DB.DYEB_BatchNo;
                            xnr.FabNetWeight = (from T1 in context.TLDYE_DyeBatch
                                       join T2 in context.TLDYE_DyeBatchDetails
                                       on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                       where T1.DYEB_Pk == CutSheet.TLCutSH_DyeBatch_FK && T2.DYEBD_BodyTrim
                                       select T2).Sum(x =>(decimal?)x.DYEBO_Nett) ?? 0.00M;
                        }
                        var CutReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                        if (CutReceipt != null)
                        {
                            xnr.Machine = context.TLADM_MachineDefinitions.Find(CutReceipt.TLCUTSHR_Machine_FK).MD_Description;
                            xnr.GoodPanels = (from T1 in context.TLCUT_CutSheetReceipt
                                                join T2 in context.TLCUT_CutSheetReceiptDetail
                                                on T1.TLCUTSHR_Pk equals T2.TLCUTSHRD_CutSheet_FK
                                                where T1.TLCUTSHR_Pk == CutReceipt.TLCUTSHR_CutSheet_FK
                                                select T2).Sum(x => (int ?)x.TLCUTSHRD_BundleQty ) ?? 0;

                            xnr.RecordedCuttingWeight = CutReceipt.TLCUTSHR_WasteCutSheet;
                            xnr.RecordedPanelWaste = CutReceipt.TLCUTSHR_WastePanels;

                            if (xnr.RecordedCuttingWeight != 0 && xnr.FabNetWeight != 0)
                            {
                                xnr.CuttingWastePerc = Math.Round(xnr.RecordedCuttingWeight / xnr.FabNetWeight * 100, 1);
                            }

                            if (xnr.RecordedPanelWaste != 0 && xnr.FabNetWeight != 0)
                            {
                                xnr.PanelWastePerc = Math.Round(xnr.RecordedPanelWaste / xnr.FabNetWeight * 100, 1);
                            }

                            xnr.TotalWaste = xnr.RecordedCuttingWeight + xnr.RecordedPanelWaste;

                            if(xnr.TotalWaste != 0 && xnr.FabNetWeight != 0)
                            {
                                xnr.TotalWastePerc = Math.Round(xnr.TotalWaste / xnr.FabNetWeight * 100, 1);
                            }

                        }
                        
                        xnr.Quality = context.TLADM_Griege.Find(CutSheet.TLCutSH_Quality_FK).TLGreige_Description;
                        xnr.Size = context.TLADM_Sizes.Find(CutSheet.TLCutSH_Size_FK).SI_Description;
                        xnr.Colour = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;



                        dataTable2.AddDataTable2Row(xnr);
                    }

                }
                ds.Tables.Add(dataTable);
                ds.Tables.Add(dataTable2);
                // CuttingWasteMangement OnHold = new CuttingWasteManagement();
                // OnHold.SetDataSource(ds);
                // crystalReportViewer1.ReportSource = OnHold;
            }
            crystalReportViewer1.Refresh();
        }

        private struct DATA
        {
            public int Style;
            public int Size;
            public int MeasureArea;
            public decimal Col1;
            public decimal Col2;
            public decimal Col3;
            public decimal Col4;
            public int NumberOf;
            public int BundleFk;

            public DATA(int _Style, int _Size, int _MeasureA, decimal _Col1, decimal _Col2, decimal _Col3, decimal _Col4, int _NumberOf, int _BundleFk)
            {
                this.Style = _Style;
                this.Size = _Size;
                this.MeasureArea = _MeasureA;
                this.Col1 = _Col1;
                this.Col2 = _Col2;
                this.Col3 = _Col3;
                this.Col4 = _Col4;
                this.NumberOf = _NumberOf;
                this.BundleFk = _BundleFk;
            }
        }
    }
}
