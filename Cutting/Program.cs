using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using System.Data;

namespace Cutting
{
    public class CreateDataStuctures
    {
        /*
        DataGridViewTextBoxColumn oTxtZA;  // Size index 
        DataGridViewTextBoxColumn oTxtZB;  // Size 
        DataGridViewTextBoxColumn oTxtZC;  // Ratio 
        DataGridViewTextBoxColumn oTxtZD;  // Garments 
        DataGridViewTextBoxColumn oTxtZE;  // Trims 
        DataGridViewTextBoxColumn oTxtZF;  // Binding 
        DataGridViewTextBoxColumn oTxtZG;  // The size power Number 
        DataGridViewTextBoxColumn oTxtZH;  // The Estimated Weight after binding taken off

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key (DYE Batch)          0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key (Greige Production)  1 
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check box                        2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Piece                            3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Nett                             4
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Quality                          5
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Tex                              6
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Yarn Supplier                    7 
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // K/Order                          9
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // Colour;                         10
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // Grade                           11
        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn();   // Piece Remarks     3             12
        DataGridViewTextBoxColumn oTxtR = new DataGridViewTextBoxColumn();   // No 1              4             13
        DataGridViewTextBoxColumn oTxt1 = new DataGridViewTextBoxColumn();   // No 2              5             14
        DataGridViewTextBoxColumn oTxt2 = new DataGridViewTextBoxColumn();   // No 3              6             15
        DataGridViewTextBoxColumn oTxt3 = new DataGridViewTextBoxColumn();   // No 4              7             16
        DataGridViewTextBoxColumn oTxt4 = new DataGridViewTextBoxColumn();   // No 5              8             17
        DataGridViewTextBoxColumn oTxt5 = new DataGridViewTextBoxColumn();   // No 6              9             18
        DataGridViewTextBoxColumn oTxt6 = new DataGridViewTextBoxColumn();   // No 7             10             19
        DataGridViewTextBoxColumn oTxt7 = new DataGridViewTextBoxColumn();   // No 8             11             20  
        DataGridViewTextBoxColumn oTxt8 = new DataGridViewTextBoxColumn();   // No 9   
        */

        public DataTable CreateDataTAB1 ()
        {   DataTable DT = new DataTable();

            return DT;
        }

        public DataTable CreateDataTAB2()
        {
            DataTable DT = new DataTable();
            DataColumn column;
            
            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Size_Pk";
            column.Caption = "Size Rating Key";
            column.DefaultValue = 0;
            DT.Columns.Add(column);
            DT.PrimaryKey = new DataColumn[] { DT.Columns[0] };

            //===Col 1
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Description";
            column.Caption = "Size Description";
            column.DefaultValue = 0;
            DT.Columns.Add(column);

            //====Col 2
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.ColumnName = "Ratio";
            column.Caption = "Ratio";
            column.DefaultValue = 0.00M;
            DT.Columns.Add(column);

            //====Col 3 
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Garments";
            column.Caption = "Number of Garments";
            column.DefaultValue = 0;
            DT.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Binding";
            column.Caption = "Binding(KG)";
            column.DefaultValue = 0.0M;
            DT.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Trims";
            column.Caption = "Trims (KG)";
            column.DefaultValue = 0.0M;
            DT.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Size_PN";
            column.Caption = "Size Power Number";
            column.DefaultValue = 0;
            DT.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Estimated_Weight";
            column.Caption = "Nett Weight";
            column.DefaultValue = 0.0;
            DT.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Boolean);
            column.ColumnName = "Active";
            column.Caption = "Active";
            column.DefaultValue = false;
            DT.Columns.Add(column);

            using (var context = new TTI2Entities())
            {
                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    DataRow dr = DT.NewRow();

                    dr[0] = Size.SI_id;
                    dr[1] = Size.SI_Description;
                    dr[2] = 0.00M;
                    dr[3] = 0; // row.Value.ToString();
                    dr[4] = 0.00M; // row.Value.ToString();
                    dr[5] = 0.00M; // row.Value.ToString();
                    dr[6] = Size.SI_PowerN; // row.Value.ToString();
                    dr[7] = 0.00M; // row.Value.ToString();
                    dr[8] = false;

                    DT.Rows.Add(dr);
                }
            }
           
            return DT;
        }

        public DataGridView CreateUpperGridView(DataTable dt)
        {
            DataGridView gv = new DataGridView();
            return gv;
        }

        public DataGridView CreateLowerGridView(DataTable dt)
        {
            DataGridView gv = new DataGridView();

            DataGridViewTextBoxColumn gc = new DataGridViewTextBoxColumn();     // 0  
            gc.ValueType = typeof(int);
            gc.DataPropertyName = dt.Columns[0].ColumnName;
            gc.Visible = false;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     // 1
            gc.HeaderText = "Size";
            gc.ValueType = typeof(string);
            gc.DataPropertyName = dt.Columns[1].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     // 2
            gc.HeaderText = "Ratio";
            gc.ValueType = typeof(string);
            gc.DataPropertyName = dt.Columns[2].ColumnName;
            gv.Columns.Add(gc);
            // oTxtZC.ReadOnly = true;

            gc = new DataGridViewTextBoxColumn();     // 3
            gc.HeaderText = "Garments";
            gc.ValueType = typeof(int);
            gc.DataPropertyName = dt.Columns[3].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();      // 4
            gc.HeaderText = "Binding(Kg)";
            gc.ValueType = typeof(Decimal);
            gc.DataPropertyName = dt.Columns[4].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();      // 5
            gc.HeaderText = "Trims (Kg)";
            gc.ValueType = typeof(Decimal);
            gc.DataPropertyName = dt.Columns[5].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     //  6
            gc.HeaderText = "Power";
            gc.Visible = false;
            gc.ValueType = typeof(int);
            gc.DataPropertyName = dt.Columns[6].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     //  7
            gc.HeaderText = "Estimated Nett Kg";
            gc.Visible = false;
            gc.ValueType = typeof(decimal);
            gc.DataPropertyName = dt.Columns[7].ColumnName;
            gc.ReadOnly = true;
            gv.Columns.Add(gc);

            return gv;
        }
        static void Calculate(TLDYE_DyeBatchDetails dbd)
        {
            /*
            Util core = new Util();

            var NettWeight = dbd.DYEBO_Nett;
            var BindWeight = 0.00M;
            var TrimWeight = 0.00M;

            var NoOfGarments = 0;


            var Yield = core.FabricYield(dbd.DYEBO_DiskWeight, dbd.DYEBO_Width);

            if (Yield == 0)
            {
                MessageBox.Show("Yield Factor is incorrect", "Error Message Fabric Weight " + dbd.DYEBO_DiskWeight.ToString() + " Fabric Width " + dbd.DYEBO_Width.ToString());
                return;
            }

            var StyleFK = (from DO in context.TLDYE_DyeOrder
                           join DB in context.TLDYE_DyeBatch on DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                           join DBD in context.TLDYE_DyeBatchDetails on DB.DYEB_Pk equals DBD.DYEBD_DyeBatch_FK
                           where DBD.DYEBD_Pk == dbd.DYEBD_Pk
                           select DO).FirstOrDefault().TLDYO_Style_FK;

            if (StyleFK == 0)
            {
                MessageBox.Show("Unable to establish a style factor", "Error Message Fabric Weight " + dbd.DYEBO_DiskWeight.ToString() + " Fabric Width " + dbd.DYEBO_Width.ToString());
                return;
            }

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
            */
        }
            
    }
}
