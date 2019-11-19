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

namespace Cutting
{
    public partial class frmSplitCutSheet : Form
    {
        int _CsPk;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;

        Util core;
        
        

        public frmSplitCutSheet(int CsPk)
        {
            InitializeComponent();
            core = new Util();

            _CsPk = CsPk;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.Visible = false;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Piece Number";
            oTxtBoxC.ValueType = typeof(string);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "DyeBatch Number";
            oTxtBoxD.ValueType = typeof(string);

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Nett Weight";
            oTxtBoxE.ValueType = typeof(decimal);

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oTxtBoxE);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }

        private void frmSplitCutSheet_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                TLCUT_CutSheet CS = context.TLCUT_CutSheet.Find(_CsPk);
                if (CS != null)
                {
                    txtCutSheet.Text = CS.TLCutSH_No;
                    
                    var Detail = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                    foreach (var Row in Detail)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Row.TLCutSHD_Pk;
                        dataGridView1.Rows[index].Cells[2].Value = false;
                        
                        var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Find(Row.TLCutSHD_DyeBatchDet_FK);
                        if(DyeBatchDetail != null)
                        {
                            dataGridView1.Rows[index].Cells[1].Value = DyeBatchDetail.DYEBD_Pk;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLKNI_GreigeProduction.Find(DyeBatchDetail.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                        }

                        var DyeBatch = DyeBatchDetail.DYEBD_DyeBatch_FK;

                        dataGridView1.Rows[index].Cells[4].Value = context.TLDYE_DyeBatch.Find(DyeBatch).DYEB_BatchNo; 
                        dataGridView1.Rows[index].Cells[5].Value = Row.TLCUTSHD_NettWeight;
                    }

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindingList<KeyValuePair<int, decimal>> CurrentRatios;
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    //------------------------------------------------------------------------------------
                    // First we must ensure that all records are removed from 
                    // the expectedUnits table for this particular Cut Sheet
                    //---------------------------------------------------------------------------
                    context.TLCUT_ExpectedUnits.RemoveRange(context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == _CsPk));
                    context.SaveChanges();
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        var DbPk = (int)Row.Cells[1].Value;

                        if ((bool)Row.Cells[2].Value == false)
                        {
                            //------------------------------------------------------------
                            // The pieces that are left are used to recalculate
                            // the number of expected units 
                            //--------------------------------------------------------------
                             decimal Nett = (decimal)Row.Cells[5].Value;
                             TLDYE_DyeBatchDetails dbd = context.TLDYE_DyeBatchDetails.Find(DbPk);
                             if (dbd != null)
                             {
                                 // Based on code developed for frmCutSheet 
                                 // Is a body or is it a Trim Record
                                 //=================================================
                                 if (dbd.DYEBD_BodyTrim)
                                 {
                                     //--------------------------------------------------------------
                                     // It is a body
                                     // Now calculate the yield 
                                     //------------------------------------------------------------------
                                     var Yield = core.FabricYield(dbd.DYEBO_DiskWeight, dbd.DYEBO_Width);
                                     var Rating = context.TLADM_ProductRating.Find((int)dbd.DYEBO_ProductRating_FK).Pr_numeric_Rating;
                                   
                                     var Factor = Math.Round(Yield / Rating * Nett , 0);
                                     var tst = core.CalculateRatios((int)dbd.DYEBO_ProductRating_FK, (int)Factor);
                                     foreach (var row in tst)
                                     {
                                         bool Add = true;
                                         var Kg = Math.Round(0.01M * row.Value / Yield, 2);
                                         
                                         TLCUT_ExpectedUnits eUnits = new TLCUT_ExpectedUnits();
                                         eUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == _CsPk && x.TLCUTE_Size_FK == row.Key).FirstOrDefault();
                                         if (eUnits == null)
                                         {
                                             eUnits = new TLCUT_ExpectedUnits();

                                             eUnits.TLCUTE_CutSheet_FK = _CsPk;
                                             eUnits.TLCUTE_Size_FK = row.Key;
                                             eUnits.TLCUTE_NoofGarments = row.Value;
                                             eUnits.TLCUTE_NoOfTrims = Kg;
                                             eUnits.TLCUTE_NoOfBinding = 0;
                                         }
                                         else
                                         {
                                             eUnits.TLCUTE_NoofGarments += row.Value;
                                             eUnits.TLCUTE_NoOfTrims += Kg;
                                             Add = !Add;
                                         }

                                         if (Add)
                                         {
                                             context.TLCUT_ExpectedUnits.Add(eUnits);
                                             context.SaveChanges();
                                         }
                                     }
                                     
                                 }
                                 else
                                 {
                                     //--------------------------------------------------------------
                                     // It is a Trim
                                     // Now calculate the yield 
                                     //------------------------------------------------------------------
                                     var Yield = core.FabricYield(dbd.DYEBO_DiskWeight, dbd.DYEBO_Width);
                                     var Rating = context.TLADM_ProductRating.Find((int)dbd.DYEBO_ProductRating_FK).Pr_numeric_Rating;
                                     CurrentRatios = core.ReturnRatios((int)dbd.DYEBO_ProductRating_FK);

                                     if (dbd.DYEBO_GVRowNumber == 1)
                                     {
                                         var NumberOf = Math.Round(Yield / Rating * Nett, 0);
                                         var TotalOf = CurrentRatios.Sum(x => (decimal ?) x.Value) ?? 0.00M;
                                         foreach (var row in CurrentRatios)
                                         {
                                             int Calc = (int)Math.Round(row.Value / TotalOf * NumberOf, 0);

                                             TLCUT_ExpectedUnits eUnits = new TLCUT_ExpectedUnits();
                                             eUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == _CsPk && x.TLCUTE_Size_FK == row.Key).FirstOrDefault();
                                             if(eUnits != null)
                                                 // The imfamous switch
                                                 //----------------------------
                                                 eUnits.TLCUTE_NoOfTrims += Calc;
                                             
                                         }
                                     }
                                     else if (dbd.DYEBO_GVRowNumber == 2)
                                     {
                                         var NumberOf = Math.Round(Yield / Rating * Nett, 0);
                                         var TotalOf = CurrentRatios.Sum(x => (decimal?)x.Value) ?? 0.00M;
                                         foreach (var row in CurrentRatios)
                                         {
                                             int Calc = (int)Math.Round(row.Value / TotalOf * NumberOf, 0);

                                             TLCUT_ExpectedUnits eUnits = new TLCUT_ExpectedUnits();
                                             eUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == _CsPk && x.TLCUTE_Size_FK == row.Key).FirstOrDefault();
                                             if (eUnits != null)
                                                 // The imfamous switch
                                                 //----------------------------
                                                 eUnits.TLCUTE_NoOfBinding += Calc;
                                         }
                                     }
                                 }
                             }

                             continue;
                        }

                       
                        var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Find(DbPk);
                        if (DyeBatchDetails != null)
                        {
                            DyeBatchDetails.DYEBO_CutSheet = false;
                        }

                        var CutSheetPk = (int)Row.Cells[0].Value;
                        var CutSheetDetails = context.TLCUT_CutSheetDetail.Find(CutSheetPk);
                        if (CutSheetDetails != null)
                        {
                            context.TLCUT_CutSheetDetail.Remove(CutSheetDetails);
                        }
                    }

                    try
                    {
                        
                        context.SaveChanges();
                        MessageBox.Show("Data saved to the database successfully");
                        frmCutViewRep vRep = new frmCutViewRep(1, _CsPk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        this.Close();
                        return;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
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
