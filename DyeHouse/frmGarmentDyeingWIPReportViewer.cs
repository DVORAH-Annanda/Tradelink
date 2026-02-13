using System;
using System.IO;
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
using System.Diagnostics;

namespace DyeHouse
{
    public partial class frmGarmentDyeingWIPReportViewer : Form
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

        public frmGarmentDyeingWIPReportViewer(DyeQueryParameters QueryParms)
        {
            InitializeComponent();
            _parms = QueryParms;
        }

        private void populateDataSets()
        {
            

            DataSet ds = new DataSet();
            dsGarmentDyeingWIP.DataTable1DataTable dataTable1 = new dsGarmentDyeingWIP.DataTable1DataTable();
            dsGarmentDyeingWIP.DataTable2DataTable dataTable2 = new dsGarmentDyeingWIP.DataTable2DataTable();

            _repo = new DyeRepository();
            core = new Util();
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


            dsGarmentDyeingWIP.DataTable1Row dtr = dataTable1.NewDataTable1Row();
            dtr.gdPK = "";
            dtr.reportTitle = "Garment Dye Batch WIP";
            dtr.date = _parms.DateWIP.ToString("yyyy-MM-dd"); ;
            dataTable1.AddDataTable1Row(dtr);

            int i = 0;
            var CNames = core.CreateColumnNames();

            foreach (var CName in CNames)
            {
                ColumnNames[i++][1] = CName[1];
            }

            using (var context = new TTI2Entities())
            {
                IList<TLADM_Styles> _Styles = context.TLADM_Styles.ToList();
                IList<TLADM_Colours> _Colours = context.TLADM_Colours.ToList();
                IList<TLADM_Sizes> _Sizes = context.TLADM_Sizes.ToList();

                //select* from TLCSV_StockOnHand where TLSOH_Colour_FK = 337 
                var result = context.TLCUT_CutSheet
                    .Join(context.TLDYE_DyeBatch,
                          cs => cs.TLCutSH_DyeBatch_FK,
                          db => db.DYEB_Pk,
                          (cs, db) => new { CutSheet = cs, DyeBatch = db })
                    .Where(x => x.CutSheet.TLCutSH_Colour_FK == 337
                             && !x.CutSheet.TLCutSH_WIPComplete
                             && x.CutSheet.TLCutSH_Accepted
                             && !x.CutSheet.TLCutSH_Closed)
                    .OrderBy(x => x.CutSheet.TLCutSH_Styles_FK)
                    .ThenBy(x => x.DyeBatch.DYEB_BatchNo)
                    .ToList();

                foreach (var row in result)
                {
                    dsGarmentDyeingWIP.DataTable2Row r = dataTable2.NewDataTable2Row();
                    r.gdPK = "1";
                    r.style = _Styles.FirstOrDefault(s => s.Sty_Id == row.CutSheet.TLCutSH_Styles_FK)?.Sty_Description;

                    r.colour = _Colours.FirstOrDefault(s => s.Col_Id == row.CutSheet.TLCutSH_Colour_FK)?.Col_Display;
                    r.quantity1 = "0";
                    r.quantity2 = "0";
                    r.quantity3 = "0";
                    r.quantity4 = "0";
                    r.quantity5 = "0";
                    r.quantity6 = "0";
                    r.quantity7 = "0";
                    r.quantity8 = "0";
                    r.quantity9 = "0";

                    //r.Date = row.TLCutSH_Date;
                    //r.Department = context.TLADM_Departments.Find(row.TLCutSH_Department_FK).Dep_Description;
                    //r.CutSheetNo = row.TLCutSH_No;
                    //r.Style = _Styles.FirstOrDefault(s => s.Sty_Id == row.TLCutSH_Styles_FK).Sty_Description;
                    //r.Colour = _Colours.FirstOrDefault(s => s.Col_Id == row.TLCutSH_Colour_FK).Col_Display;
                    //r.Priority = row.TLCUTSH_Priority;
                    //r.OnHold = row.TLCUTSH_OnHold;
                    //r.DyeBatch = context.TLDYE_DyeBatch.Find(row.TLCutSH_DyeBatch_FK).DYEB_BatchNo;

                    ////        var xSizes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == row.TLCutSH_Pk).ToList();
                    ////        foreach (var xSize in xSizes)
                    ////        {
                    ////            var Size = _Sizes.FirstOrDefault(s => s.SI_id == xSize.TLCUTE_Size_FK);
                    ////            if (Size != null)
                    ////            {
                    ////                if (Size.SI_ColNumber == 1)
                    ////                    nr.Col1 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 2)
                    ////                    nr.Col2 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 3)
                    ////                    nr.Col3 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 4)
                    ////                    nr.Col4 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 5)
                    ////                    nr.Col5 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 6)
                    ////                    nr.Col6 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 7)
                    ////                    nr.Col7 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 8)
                    ////                    nr.Col8 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 9)
                    ////                    nr.Col9 += xSize.TLCUTE_NoofGarments;
                    ////                else if (Size.SI_ColNumber == 10)
                    ////                    nr.Col10 += xSize.TLCUTE_NoofGarments;
                    ////                else
                    ////                    nr.Col11 += xSize.TLCUTE_NoofGarments;
                    ////            }
                    ////        }
                    dataTable2.AddDataTable2Row(r);
                }


                ////    if (dataTable1.Rows.Count != 0)
                ////    {
                ////        System.Data.DataTable dt = new System.Data.DataTable();
                ////        try
                ////        {
                ////            if (_parms.RepSortOption == 1)
                ////            {
                ////                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[1].ColumnName, DataViewRowState.Added).CopyToDataTable();
                ////            }
                ////            else if (_parms.RepSortOption == 2)
                ////            {
                ////                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[3].ColumnName, DataViewRowState.Added).CopyToDataTable();
                ////            }
                ////            else if (_parms.RepSortOption == 3)
                ////            {
                ////                dt = (DataTable)dataTable1.Select(null, dataTable1.Columns[5].ColumnName, DataViewRowState.Added).CopyToDataTable();
                ////            }
                ////            dataTable1.Rows.Clear();

                ////            foreach (DataRow dr in dt.Rows)
                ////            {
                ////                DataSet4.DataTable1Row nr = dataTable1.NewDataTable1Row();
                ////                nr.Pk = dr.Field<int>(0);
                ////                nr.CutSheetNo = dr.Field<String>(1);
                ////                nr.Date = dr.Field<DateTime>(2);
                ////                nr.Colour = dr.Field<String>(3);
                ////                nr.ErrorLog = dr.Field<String>(4);
                ////                nr.Style = dr.Field<String>(5);
                ////                nr.Department = dr.Field<String>(6);
                ////                nr.OnHold = dr.Field<bool>(7);
                ////                nr.Priority = dr.Field<bool>(8);
                ////                nr.Col1 = dr.Field<int>(9);
                ////                nr.Col2 = dr.Field<int>(10);
                ////                nr.Col3 = dr.Field<int>(11);
                ////                nr.Col4 = dr.Field<int>(12);
                ////                nr.Col5 = dr.Field<int>(13);
                ////                nr.Col6 = dr.Field<int>(14);
                ////                nr.Col7 = dr.Field<int>(15);
                ////                nr.Col8 = dr.Field<int>(16);
                ////                nr.Col9 = dr.Field<int>(17);
                ////                nr.Col10 = dr.Field<int>(18);
                ////                nr.Col11 = dr.Field<int>(19);
                ////                nr.DyeBatch = dr.Field<String>(20);

                ////                dataTable1.AddDataTable1Row(nr);
                ////            }
                ////        }
                ////        catch (Exception ex)
                ////        {
                ////            MessageBox.Show(ex.Message);
                ////            return;
                ////        }
                ////    }


                ////}

                if (dataTable1.Rows.Count == 0)
                {
                    dsGarmentDyeingWIP.DataTable1Row noRecords = dataTable1.NewDataTable1Row();
                    noRecords.gdPK = "There are no records pertaining to selection made";
                    dataTable1.AddDataTable1Row(noRecords);
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);



                ////if (_parms.RepSortOption == 1)
                ////{
                ////    wipCut_CS = new WIPCuttingByCS();
                ////    wipCut_CS.SetDataSource(ds);
                ////}
                ////else if (_parms.RepSortOption == 2)
                ////{
                ////    wipCut = new WIPCutting();
                ////    wipCut.SetDataSource(ds);
                ////}
                ////else
                ////{
                ////    wipCut = new WIPCutting();
                ////    wipCut.SetDataSource(ds);
                ////}

                ////System.Collections.IEnumerator ie = null;

                ////if (_parms.RepSortOption == 1)
                ////    ie = wipCut_CS.Section2.ReportObjects.GetEnumerator();
                ////else
                ////    ie = wipCut.Section2.ReportObjects.GetEnumerator();

                ////while (ie.MoveNext())
                ////{
                ////    if (ie.Current != null && ie.Current.GetType().ToString().Equals("CrystalDecisions.CrystalReports.Engine.TextObject"))
                ////    {
                ////        CrystalDecisions.CrystalReports.Engine.TextObject to = (CrystalDecisions.CrystalReports.Engine.TextObject)ie.Current;

                ////        var result = (from u in ColumnNames
                ////                      where u[0] == to.Name
                ////                      select u).FirstOrDefault();

                ////        if (result != null)
                ////            to.Text = result[1];
                ////    }
                ////}

                ////if (_parms.RepSortOption == 1)
                ////{
                ////    // AS20240315 v5.0.0.124 - Added totals for all locations
                ////    crystalReportViewer1.ReportSource = wipCut_CS;

                ////}
                ////else
                ////{
                ////    if (_parms.RepSortOption == 2)
                ////        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[3];
                ////    else
                ////        wipCut.DataDefinition.Groups[1].ConditionField = wipCut.Database.Tables[0].Fields[5];

                //GarmentDyeingWIP garmentDyeingWIP = new GarmentDyeingWIP();
                //    garmentDyeingWIP.SetDataSource(ds);
                //    crystalReportViewer1.ReportSource = garmentDyeingWIP;

                // … your existing ds.Tables.Add(dataTable1/2) …
                string html = GenerateGarmentDyeingHtmlReport(ds.Tables[1]);
                webBrowserReport.DocumentText = html;
                //File.WriteAllText(@"C:\temp\wip.html", html);
                //Process.Start(new ProcessStartInfo(@"C:\temp\wip.html") { UseShellExecute = true });
            }
        }

        private string GenerateGarmentDyeingHtmlReport(DataTable table)
        {
            var sizeLabels = new[] { "3-4", "5-6", "7-8", "9-10", "11-12", "13-14", "S", "M", "L", "XL", "2XL", "3XL" };
            var html = new StringBuilder();

            html.AppendLine(@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body { font-family: sans-serif; margin: 20px; }
        h1, h2 { margin-bottom: 0; }
        table { border-collapse: collapse; width: 100%; margin-bottom: 40px; }
        th, td { border: 1px solid #ccc; padding: 6px 10px; text-align: right; }
        th { background-color: #f0f0f0; }
        td.left, th.left { text-align: left; }
        tr.subtotal { background-color: #e8f4f8; font-weight: bold; }
        tr.total { background-color: #d1f7d1; font-weight: bold; }
        tr:nth-child(even):not(.subtotal):not(.total) { background-color: #f9f9f9; }
    </style>
</head>
<body>
    <h1>WIP GARMENT DYEING</h1>");

            var styles = table.AsEnumerable()
                              .GroupBy(r => r.Field<string>("style") ?? "UNKNOWN");

            foreach (var styleGroup in styles)
            {
                html.AppendLine($"<h2>STYLE: {styleGroup.Key}</h2>");
                html.AppendLine("<table><thead><tr><th class='left'>Batch No</th><th class='left'>Colour</th>");
                foreach (var label in sizeLabels)
                    html.Append($"<th>{label}</th>");
                html.AppendLine("<th>DUE</th></tr></thead><tbody>");

                var total = new int[sizeLabels.Length];

                var colours = styleGroup.GroupBy(r => r.Field<string>("colour") ?? "UNKNOWN");

                foreach (var colourGroup in colours)
                {
                    var subtotal = new int[sizeLabels.Length];

                    foreach (var row in colourGroup)
                    {
                        string batch = row.Table.Columns.Contains("batch") ? row["batch"].ToString() : "";
                        string due = row.Table.Columns.Contains("dueDate") ? row["dueDate"].ToString() : "";

                        html.Append($"<tr><td class='left'>{batch}</td><td class='left'>{colourGroup.Key}</td>");

                        for (int i = 1; i <= sizeLabels.Length; i++)
                        {
                            string q = row.Table.Columns.Contains($"quantity{i}") ? row[$"quantity{i}"].ToString() : "0";
                            int val = int.TryParse(q, out int result) ? result : 0;
                            subtotal[i - 1] += val;
                            total[i - 1] += val;
                            html.Append($"<td>{val}</td>");
                        }

                        html.AppendLine($"<td>{due}</td></tr>");
                    }

                    // subtotal row
                    html.Append("<tr class='subtotal'><td colspan='2'>SUBTOTAL " + colourGroup.Key.ToUpper() + "</td>");
                    foreach (var val in subtotal)
                        html.Append($"<td>{val}</td>");
                    html.AppendLine("<td></td></tr>");
                }

                // total row
                html.Append("<tr class='total'><td colspan='2'>TOTAL " + styleGroup.Key.ToUpper() + "</td>");
                foreach (var val in total)
                    html.Append($"<td>{val}</td>");
                html.AppendLine("<td></td></tr>");
                html.AppendLine("</tbody></table>");
            }

            html.AppendLine("</body></html>");
            return html.ToString();
        }

        private void frmGarmentDyeingWIPReportViewer_Load(object sender, EventArgs e)
        {


            populateDataSets();
        }
    }
}
