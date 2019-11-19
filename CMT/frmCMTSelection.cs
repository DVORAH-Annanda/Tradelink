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

namespace CMT
{
    public partial class frmCMTSelection : Form
    {
        CMTRepository repo;
        CMTQueryParameters QueryParms;
        public frmCMTSelection()
        {
            InitializeComponent();
            repo = new CMTRepository();
            
            this.cmboCMT.CheckStateChanged += new System.EventHandler(this.cmboDepartment_CheckStateChanged);
        }

        private void frmCMTSelection_Load(object sender, EventArgs e)
        {
            QueryParms = new CMTQueryParameters();
            using (var context = new TTI2Entities()) 
            {
                var CMTs = context.TLADM_Departments.Where(x=>x.Dep_IsCMT).OrderBy(x=>x.Dep_Description).ToList();
                foreach (var CMT in CMTs)
                {
                    cmboCMT.Items.Add(new CMT.CheckComboBoxItem(CMT.Dep_Id, CMT.Dep_Description, false));
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDepartment_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CMT.CheckComboBoxItem)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Depts.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Depts.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Depts.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var oBtn = sender as Button;
            if (oBtn != null)
            {
                DataSet ds = new DataSet();
                DataSet19.DataTable1DataTable dataTable1 = new DataSet19.DataTable1DataTable();
                int Style_FK = 0;
                int Colour_FK = 0;
                int Size_FK = 0;
                int LineIssue_FK = 0;
                Util core = new Util();
                
                QueryParms.BillingRecords = true;
                
                DateTime dt = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                dt = dt.AddHours(23);
                QueryParms.ToDate = dt;
                
                String FileName = string.Empty;

                using (var context = new TTI2Entities())
                {
                    var ExistingGroups = repo.CMTCompletedWorkExport(QueryParms).GroupBy(x => new { x.TLCMTWC_LineIssue_FK /*, x.TLCMTWC_Size_FK*/}).ToList();
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

                            var SizeDesc = context.TLADM_Sizes.Find(Size_FK).SI_Description;
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
                            NewRow.Style = context.TLADM_Styles.Find(Style_FK).Sty_Description;
                            NewRow.Colour = context.TLADM_Colours.Find(Colour_FK).Col_Display;
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
                           
                            //===========================================================
                            //Find the faults
                            //===================================================
                            var PF = from PFaults in context.TLCMT_ProductionFaults
                                     join DFlaw in context.TLCMT_DeflectFlaw on PFaults.TLCMTPF_Fault_FK equals DFlaw.TLCMTDF_Pk
                                     where PFaults.TLCMTPF_PanelIssue_FK == LineIssue_FK && DFlaw.TLCMTDF_Manufacturing
                                     select PFaults;

                            NewRow.ManuFaultQty = PF.Sum(x => (int?)x.TLCMTPF_Qty) ?? 0;

                            NewRow.ManuTotal      = NewRow.ManuQty * NewRow.ManuCost;
                            NewRow.ManuFaultTotal = NewRow.ManuFaultQty * NewRow.ManuFaultCost;
                            NewRow.ManuShortTotal = NewRow.ManuShortQty * NewRow.ManuShortCost;

                            dataTable1.AddDataTable1Row(NewRow);
                        }
                    }


                    DataView DataV = dataTable1.DefaultView;
                    DataV.Sort = "LineIssuedNo";
                    ds.Tables.Add(DataV.ToTable());

                    Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                    Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();

                    osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                    int colIndex = 1;
                    int rowIndex = 1;

                    foreach (DataColumn dc in dataTable1.Columns)
                    {
                             if (colIndex == 1)
                             {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "CMT";
                            }
                            else if (colIndex == 2)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "CutSheet";
                            }
                            else if (colIndex == 3)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "Line Detail";
                            }
                            else if (colIndex == 4)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "Style";
                            }
                            else if (colIndex == 5)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "Colour";
                            }
                            else if (colIndex == 6)
                            {
                                osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                                osheet.Cells[1, colIndex] = "Size";
                            }
                            else if (colIndex == 7)
                            {

                                osheet.Cells[1, colIndex] = "Manu Qty";
                            }
                            else if (colIndex == 8)
                            {
                                osheet.Cells[1, colIndex] = "Manu Cost";
                            }
                            else if (colIndex == 9)
                            {
                                osheet.Cells[1, colIndex] = "Manu Total";
                            }
                            else if (colIndex == 10)
                            {
                                osheet.Cells[1, colIndex].NumberFormat = "0";
                                osheet.Cells[1, colIndex] = "Fault Qty";
                            }
                            else if (colIndex == 11)
                            {
                                osheet.Cells[1, colIndex] = "Fault Cost";
                            }
                            else if (colIndex == 12)
                            {
                                osheet.Cells[1, colIndex] = "Fault Total";
                            }
                            else if (colIndex == 13)
                            {
                                osheet.Cells[1, colIndex] = "Short Qty";
                            }
                            else if (colIndex == 14)
                            {
                                osheet.Cells[1, colIndex] = "Short Cost";
                            }
                            else if (colIndex == 15)
                            {
                                osheet.Cells[1, colIndex] = "Short Total";
                            }
                        
                        colIndex++;
                     }


                     foreach (DataRow Row in dataTable1.Rows)
                     {
                            rowIndex++;
                            colIndex = 1;

                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {

                                if (colIndex == 1)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 2)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 3)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 4)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 5)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 6)
                                {
                                    string Val = Row.Field<string>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 7)
                                {
                                    int Val = Row.Field<int>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 8)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 9)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 10)
                                {
                                    int Val = Row.Field<int>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 11)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 12)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 13)
                                {
                                    int Val = Row.Field<int>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 14)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                else if (colIndex == 15)
                                {
                                    decimal Val = Row.Field<decimal>(colIndex - 1);
                                    osheet.Cells[rowIndex, colIndex] = Val;
                                }
                                colIndex++;
                            }
                        }

                        osheet.Range["H2", "H" + rowIndex].NumberFormat = "R #,###.00";
                        osheet.Range["I2", "I" + rowIndex].NumberFormat = "R #,###.00";
                        osheet.Range["K2", "K" + rowIndex].NumberFormat = "R #,###.00";
                        osheet.Range["L2", "L" + rowIndex].NumberFormat = "R #,###.00";
                        osheet.Range["N2", "N" + rowIndex].NumberFormat = "R #,###.00";
                        osheet.Range["O2", "O" + rowIndex].NumberFormat = "R #,###.00";

                        /*osheet.Range["G2", "G" + rowIndex].NumberFormat = "R #,###.00";
                          osheet.Range["H2", "H" + rowIndex].NumberFormat = "R #,###.00";
                          osheet.Range["J2", "J" + rowIndex].NumberFormat = "R #,###.00";
                          osheet.Range["K2", "K" + rowIndex].NumberFormat = "R #,###.00";
                          osheet.Range["M2", "M" + rowIndex].NumberFormat = "R #,###.00";
                          osheet.Range["N2", "N" + rowIndex].NumberFormat = "R #,###.00";
                        `*/

                        osheet.Columns.AutoFit();

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = "C:\\Temp";
                        sfd.Filter = "Excel |*.xlsx";

                        DialogResult xdr = sfd.ShowDialog();
                        if (xdr == DialogResult.OK)
                        {
                            try
                            {
                                obook.SaveAs(sfd.FileName);
                                obook.Saved = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }

                        //=====================================================
                        //Release and terminate excel
                        //====================================================================
                        obook.Close();
                        oexcel.Quit();

                        releaseObject(osheet);
                        releaseObject(obook);
                        releaseObject(oexcel);
                        GC.Collect();

                        DialogResult Result = MessageBox.Show("Do you wish to close this months transactions", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Result == DialogResult.Yes)
                        {
                            try
                            {
                                foreach (var Dept in QueryParms.Depts)
                                {
                                   DateTime dtx = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                                   dtx = dtx.AddHours(23);
                                   context.UpdateCompletedWork1(dtx, Dept.Dep_Id);
                                }

                                MessageBox.Show("Data successfully updated");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                 }

            }
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

        private void cmboCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        } 
    }

}
