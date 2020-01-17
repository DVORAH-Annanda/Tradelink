using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Windows.Forms;
using Utilities;

namespace Cutting
{
    public partial class ReCalculateExpectedUnits : Form
    {
        bool FormLoaded;

        DataTable dt;

        Util core;

        public ReCalculateExpectedUnits()
        {
            InitializeComponent();

            core = new Util();
            dt = new DataTable();

            DataColumn Column;

            Column = new DataColumn();
            Column.DataType = typeof(Int32);
            Column.ColumnName = "CS_Pk";
            Column.Caption = "Cut Sheet Primary";
            Column.ReadOnly = true;
            Column.DefaultValue = 0;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "CS_Select";
            Column.Caption = "Select";
            Column.DefaultValue = false;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_No";
            Column.Caption = "Cut Sheet Number";
            Column.ReadOnly = true;
            Column.DefaultValue = string.Empty;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_Style";
            Column.Caption = "Style";
            Column.DefaultValue = string.Empty;
            Column.ReadOnly = true;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_Colour";
            Column.Caption = "Colour";
            Column.DefaultValue = string.Empty;
            Column.ReadOnly = true;

            dt.Columns.Add(Column);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView1.DataSource = dt;

            var idx = -1;
            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                    dataGridView1.Columns[idx].Visible = false;
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    if (idx == 1)
                        dataGridView1.Columns[col.ColumnName].Width = 75;
                    else
                        dataGridView1.Columns[col.ColumnName].Width = 125;
                }
            }
        }

        private void ReCalculateExpectedUnits_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                FormLoaded = false;
                ExtractData(true);
                FormLoaded = true;
            }
        }

        private void ExtractData(bool Flag)
        {
            IList<TLCUT_CutSheet> CutSheets = null;

            using (var context = new TTI2Entities())
            {
                if (Flag)
                {
                    CutSheets = (from CS in context.TLCUT_CutSheet
                                 where !CS.TLCutSH_Closed && !CS.TLCUTSH_OnHold && !CS.TLCutSH_WIPComplete
                                 orderby CS.TLCutSH_No
                                 select CS).ToList();
                }
                else
                {
                    CutSheets = (from CS in context.TLCUT_CutSheet
                                 where CS.TLCutSH_Closed && !CS.TLCutSH_WIPComplete
                                 orderby CS.TLCutSH_No
                                 select CS).ToList();
                }

                dt.Rows.Clear();

                foreach (var CutSheet in CutSheets)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = CutSheet.TLCutSH_Pk;
                    dr[1] = false;
                    dr[2] = CutSheet.TLCutSH_No;
                    dr[3] = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                    dr[4] = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;

                    dt.Rows.Add(dr);
                }
            }
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {

            Button oBtn = (Button)sender;
            var oDgv = (DataGridView)dataGridView1;

            if (oBtn != null && FormLoaded)
            {
                var RowCount = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                where (bool)Rows.Cells[1].Value == true
                                select Rows).Count();

                if (RowCount == 0)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select at least one row to process");
                    }
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!dr.Field<bool>(1))
                            continue;

                        var CurrentRow = oDgv.CurrentRow;
                        var CurrentCell = oDgv.CurrentCell;
                        if (CurrentRow != null)
                        {
                            var CutSheetIndex = (int)CurrentRow.Cells[0].Value;
                            var CutSheet = context.TLCUT_CutSheet.Find(CutSheetIndex);
                            if (CutSheet != null)
                            {
                                context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk)
                                                           .Update(x => new TLCUT_ExpectedUnits() { TLCUTE_EstNettWeight = 0, TLCUTE_NoOfBinding = 0, TLCUTE_NoofGarments = 0, TLCUTE_NoOfTrims = 0 , TLCUTE_MarkerRatio = 0});
                                
                                var DBDetails = (from DBatch in context.TLDYE_DyeBatch
                                                 join DBatchDetails in context.TLDYE_DyeBatchDetails on DBatch.DYEB_Pk equals DBatchDetails.DYEBD_DyeBatch_FK
                                                 where DBatch.DYEB_Pk == CutSheet.TLCutSH_DyeBatch_FK
                                                 select DBatchDetails).ToList();

                                foreach (var dbd in DBDetails)
                                {
                                    if (dbd != null)
                                    {
                                        //Is a body or is it a Trim Record
                                        //===================================================
                                        if (dbd.DYEBD_BodyTrim)
                                        {
                                            //--------------------------------------------------------------
                                            // The aim of this event is to calculate the number of expected units
                                            // based on the nett weight of the piece of fabric selected
                                            // less the weight of the calculated binding needed (if applicable)
                                            //------------------------------------------------------------------
                                            var Yield = core.FabricYield(dbd.DYEBO_DiskWeight, dbd.DYEBO_Width);

                                            var NettWeight = dbd.DYEBO_Nett;
                                            var BindWeight = 0.00M;
                                            var TrimWeight = 0.00M;

                                            //---------------------------------------------------------
                                            // We now have to establish whether the style relating to the dye batch
                                            // has a Binding and if so what is the rating
                                            //----------------------------------------------------------------

                                            var StyleFK = (from DO in context.TLDYE_DyeOrder
                                                           join DB in context.TLDYE_DyeBatch on DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                                           join DBD in context.TLDYE_DyeBatchDetails on DB.DYEB_Pk equals DBD.DYEBD_DyeBatch_FK
                                                           where DBD.DYEBD_Pk == dbd.DYEBD_Pk
                                                           select DO).FirstOrDefault().TLDYO_Style_FK;

                                            var ISBinding = (from stytrim in context.TLADM_StyleTrim
                                                             join trim in context.TLADM_Trims on stytrim.StyTrim_Trim_Fk equals trim.TR_Id
                                                             join prodrating in context.TLADM_ProductRating on stytrim.StyTrim_ProdRating_FK equals prodrating.Pr_Id
                                                             where stytrim.StyTrim_Styles_Fk == StyleFK && trim.TR_IsBinding
                                                             select prodrating).FirstOrDefault();

                                            if (ISBinding != null)
                                            {
                                                BindWeight = NettWeight * ISBinding.Pr_numeric_Rating;
                                                NettWeight -= BindWeight;
                                            }

                                            var ISTrim = (from stytrim in context.TLADM_StyleTrim
                                                          join trim in context.TLADM_Trims on stytrim.StyTrim_Trim_Fk equals trim.TR_Id
                                                          join prodrating in context.TLADM_ProductRating on stytrim.StyTrim_ProdRating_FK equals prodrating.Pr_Id
                                                          where stytrim.StyTrim_Styles_Fk == StyleFK && !trim.TR_IsBinding
                                                          select prodrating).FirstOrDefault();

                                            if (ISTrim != null)
                                            {
                                                TrimWeight = dbd.DYEBO_Nett * ISTrim.Pr_numeric_Rating;
                                            }

                                            // select trims.TR_Description , pr.Pr_numeric_Rating 
                                            // from TLADM_StyleTrim strim
                                            // inner join. TLADM_Trims trims
                                            // on trims.TR_Id = strim.StyTrim_Trim_Fk 
                                            // inner join TLADM_ProductRating pr
                                            // on pr.Pr_Id = strim.StyTrim_ProdRating_FK 
                                            // where trims.TR_IsBinding = 1 and strim.StyTrim_Styles_Fk = 34
                                            //-------------------------------------------------
                                            var ProdFK = dbd.DYEBO_ProductRating_FK;

                                            var Rating = context.TLADM_ProductRating.Find(ProdFK).Pr_numeric_Rating;
                                            var NoOfGarments = Math.Round(Yield / Rating * NettWeight);

                                            var Factor = Math.Round(Yield / Rating * NettWeight, 0);
                                            var tst = core.CalculateRatios(ProdFK, (int)Factor);

                                            bool Add = false;

                                            foreach (var row in tst)
                                            {
                                                Add = false;

                                                decimal EstKg = Math.Round(NettWeight * (row.Value / Factor), 2);
                                                decimal BindKg = Math.Round(BindWeight * (row.Value / Factor), 2);
                                                decimal TrimKg = Math.Round(TrimWeight * (row.Value / Factor), 2);

                                                var ExpectUnits = context.TLCUT_ExpectedUnits.FirstOrDefault(x => x.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk && x.TLCUTE_Size_FK == row.Key);
                                                if (ExpectUnits == null)
                                                {
                                                    ExpectUnits = new TLCUT_ExpectedUnits();
                                                    ExpectUnits.TLCUTE_CutSheet_FK = CutSheetIndex;
                                                    ExpectUnits.TLCUTE_Size_FK = row.Key;
                                                    Add = true;
                                                }

                                                ExpectUnits.TLCUTE_EstNettWeight += EstKg;
                                                ExpectUnits.TLCUTE_NoOfBinding += (int)(Yield / Rating) * BindKg;
                                                ExpectUnits.TLCUTE_NoOfTrims += (int)(Yield / Rating) * TrimKg;
                                                ExpectUnits.TLCUTE_NoofGarments += (int)((Yield / Rating) * EstKg);
                                                if (ExpectUnits.TLCUTE_MarkerRatio == 0.00M)
                                                        ExpectUnits.TLCUTE_MarkerRatio = row.Value;

                                                if(Add)
                                                {
                                                    context.TLCUT_ExpectedUnits.Add(ExpectUnits);

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
                                        }
                                    }
                                }
                            }
                            dr[1] = false;
                        }
                      
                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDlg = (DataGridView)sender;

            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index == oDlg.CurrentRow.Index)
                    continue;

                row.Cells[1].Value = false;
            }
        }
    }
}
