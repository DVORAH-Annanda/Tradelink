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

namespace ExecutiveReporting
{
    public partial class frmExecViewRep : Form
    {
        int _RepNo;
        DataTable dt; 
        ExecQueryParameters _QueryParms;

        Util core;

        public frmExecViewRep(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmExecViewRep(int RepNo, ExecQueryParameters QParms)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _QueryParms = QParms;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1)
            {
                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable dataTable1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable dataTable2 = new DataSet1.DataTable2DataTable();

                ExecRepository repo = new ExecRepository();
                ExecQueryParameters QueryParms = new ExecQueryParameters();

                core = new Util();
                //================================================================
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Description", typeof(string));     // 0
                //===============================================================================
                dt.Columns.Add("01", typeof(decimal));       // 1  
                dt.Columns[1].DefaultValue = 0.0;
                dt.Columns.Add("02", typeof(decimal));       // 2    
                dt.Columns[2].DefaultValue = 0.0;
                dt.Columns.Add("03", typeof(decimal));       // 3
                dt.Columns[3].DefaultValue = 0.0;
                dt.Columns.Add("04", typeof(decimal));       // 4
                dt.Columns[4].DefaultValue = 0.0;
                dt.Columns.Add("05", typeof(decimal));       // 5
                dt.Columns[5].DefaultValue = 0.0;
                dt.Columns.Add("06", typeof(decimal));       // 6
                dt.Columns[6].DefaultValue = 0.0;
                dt.Columns.Add("07", typeof(decimal));       // 7
                dt.Columns[7].DefaultValue = 0.0;
                dt.Columns.Add("08", typeof(decimal));       // 8
                dt.Columns[8].DefaultValue = 0.0;
                dt.Columns.Add("09", typeof(decimal));       // 9
                dt.Columns[9].DefaultValue = 0.0;
                dt.Columns.Add("10", typeof(decimal));       // 10
                dt.Columns[10].DefaultValue = 0.0;
                dt.Columns.Add("11", typeof(decimal));       // 11
                dt.Columns[11].DefaultValue = 0.0;
                dt.Columns.Add("12", typeof(decimal));       // 12   
                dt.Columns[12].DefaultValue = 0.0;
                dt.Columns.Add("13", typeof(int));           // 13   
                dt.Columns[13].DefaultValue = 0;

                using (var context = new TTI2Entities())
                {
                    QueryParms.ToDate = DateTime.Now;
                    QueryParms.FromDate = QueryParms.ToDate.AddDays(-1 * QueryParms.ToDate.DayOfYear + 1);

                    var YarnProd = repo.ExecYarnProduction(QueryParms).GroupBy(x=>x.YarnOP_DatePacked.Value.Month);
                    if (YarnProd.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "Spinning - Yarn Production";

                        foreach (var Mth in YarnProd)
                        {
                            var MthKey = Mth.FirstOrDefault().YarnOP_DatePacked.Value.Month.ToString().PadLeft(2, '0');
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<decimal>(ColIndex) + Mth.Sum(x => (decimal?)x.YarnOP_NettWeight) ?? 0.0M;
                            }
                        }
                        Row[13] = 1;
                        dt.Rows.Add(Row);
                    }

                    var GreigeProd = repo.ExecGreigeProduction(QueryParms).GroupBy(x => x.GreigeP_PDate.Value.Month);
                    if (GreigeProd.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "Knitting - Greige Production";

                        foreach (var Mth in GreigeProd)
                        {
                            var MthKey = Mth.FirstOrDefault().GreigeP_PDate.Value.Month.ToString().PadLeft(2, '0');
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<decimal>(ColIndex) + Mth.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                            }
                        }
                        Row[13] = 2;
                        dt.Rows.Add(Row);
                    }

                    var DyedNotComplete = repo.ExecDyeNotFinshed(QueryParms).GroupBy(x => x.TLDYET_Date.Month);
                    if (DyedNotComplete.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "Dyeing - Fabric Dyed";

                        foreach (var Mth in DyedNotComplete)
                        {
                            var MthKey = Mth.FirstOrDefault().TLDYET_Date.Month.ToString().PadLeft(2, '0');
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<decimal>(ColIndex) + Mth.Sum(x => (decimal?)x.TLDYET_BatchWeight) ?? 0.0M;
                            }
                        }
                        Row[13] = 3;
                        dt.Rows.Add(Row);

                    }

                    var DyedIntoQuarantine = repo.ExecDyeIntoQuarantine(QueryParms).GroupBy(x => x.DYEBO_ApprovalDate.Value.Month);
                    if (DyedIntoQuarantine.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "Dyeing - Fabric Into Quarantine";

                        foreach (var Mth in DyedIntoQuarantine)
                        {
                            var MthKey = Mth.FirstOrDefault().DYEBO_ApprovalDate.Value.Month.ToString().PadLeft(2, '0');
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<decimal>(ColIndex) + Mth.Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.0M;
                            }
                        }
                        Row[13] = 4;
                        dt.Rows.Add(Row);
                    }
                    var CutProd = repo.ExecIntoPanelStore(QueryParms).GroupBy(x => x.TLCUTSHRD_PanelDate.Value.Month);
                    if (CutProd.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "Cutting - Panels into Store";

                        foreach (var Mth in CutProd)
                        {
                            var MthKey = Mth.FirstOrDefault().TLCUTSHRD_PanelDate.Value.Month.ToString().PadLeft(2, '0');
                            var ColIndex = dt.Columns.IndexOf(MthKey);
                            if (ColIndex != 0)
                            {
                                Row[ColIndex] = Row.Field<decimal>(ColIndex) + Mth.Sum(x => (decimal?)x.TLCUTSHRD_BoxUnits) ?? 0.0M;
                            }
                        }
                        Row[13] = 5;
                        dt.Rows.Add(Row);

                    }
                    
                    var WorkComplete = repo.ExecWorkCompleted(QueryParms);
                    if (WorkComplete.Count() != 0)
                    {
                        DataRow Row = dt.NewRow();
                        Row[0] = "CMT - Work Completed";

                        var Records = (from LI in context.TLCMT_LineIssue
                                      join WC in context.TLCMT_CompletedWork on LI.TLCMTLI_Pk equals WC.TLCMTWC_LineIssue_FK
                                      where LI.TLCMTLI_WorkCompleted && LI.TLCMTLI_WorkCompletedDate >= QueryParms.FromDate && LI.TLCMTLI_WorkCompletedDate <= QueryParms.ToDate 
                                      select new { LI.TLCMTLI_WorkCompletedDate, WC.TLCMTWC_Qty}).GroupBy(x=>x.TLCMTLI_WorkCompletedDate.Value.Month);
 
                        foreach (var Group in Records)
                        {
                             var MthKey = Group.FirstOrDefault().TLCMTLI_WorkCompletedDate.Value.Month.ToString().PadLeft(2, '0');
                             var ColIndex = dt.Columns.IndexOf(MthKey);
                             if (ColIndex != 0)
                             {
                                    Row[ColIndex] = Row.Field<decimal>(ColIndex) + Group.Sum(x=>(decimal ?)x.TLCMTWC_Qty) ?? 0.0M;
                             }
                            
                        }
                        Row[13] = 6;
                        dt.Rows.Add(Row);
                        
                        foreach (DataRow DRow in dt.Rows)
                        {
                            DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                            nr.Description = DRow.Field<String>(0);
                            nr.Jan = DRow.Field<decimal>(1);
                            nr.Feb = DRow.Field<decimal>(2);
                            nr.Mar = DRow.Field<decimal>(3);
                            nr.Apr = DRow.Field<decimal>(4);
                            nr.May = DRow.Field<decimal>(5);
                            nr.Jun = DRow.Field<decimal>(6);
                            nr.Jul = DRow.Field<decimal>(7);
                            nr.Aug = DRow.Field<decimal>(8);
                            nr.Sep = DRow.Field<decimal>(9);
                            nr.Oct = DRow.Field<decimal>(10);
                            nr.Nov = DRow.Field<decimal>(11);
                            nr.Dec = DRow.Field<decimal>(12);
                            nr.SortOrder = DRow.Field<int>(13);
                            nr.Pk = 1;
                            dataTable1.AddDataTable1Row(nr);
                        }
                        dt.Rows.Clear();
                    }

                     
                }

                DataSet1.DataTable2Row hnr = dataTable2.NewDataTable2Row();
                hnr.Pk = 1;
                hnr.FromDate = QueryParms.FromDate;
                hnr.ToDate = QueryParms.ToDate;
                hnr.Title = "Company Production By Department";
                dataTable2.AddDataTable2Row(hnr);

                if (dataTable1.Rows.Count == 0)
                {
                    DataSet1.DataTable1Row nr = dataTable1.NewDataTable1Row();
                    nr.ErrorLog = "No record found peratining to selection made";
                    dataTable1.AddDataTable1Row(nr);

                }
                
                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                Production Prod = new Production();
                Prod.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Prod;
            
            }

            crystalReportViewer1.Refresh();
        }
    }
}
