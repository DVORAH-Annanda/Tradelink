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
    public partial class frmCSSelectDyeBatch : Form
    {
        DataGridViewTextBoxColumn oTxtA;
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;
        DataGridViewTextBoxColumn oTxtE;
        DataGridViewTextBoxColumn oTxtF; 
        DataGridViewTextBoxColumn oTxtG;
        DataGridViewTextBoxColumn oTxtH;
        DataGridViewTextBoxColumn oTxtJ;
        DataGridViewTextBoxColumn oTxtK;

        DataTable dt; 

        Util core;

        public int BatchSelected;

        public frmCSSelectDyeBatch()
        {
            InitializeComponent();
            core = new Util();

            dt = new DataTable();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Primary Key (DYE Batch)          0
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);

            oChkA = new DataGridViewCheckBoxColumn(); //  1 Select 
            oChkA.HeaderText = "Select";

            oTxtB = new DataGridViewTextBoxColumn();   // 2 Batch Number
            oTxtB.HeaderText = "Batch No";
            oTxtB.ValueType = typeof(string);


            oTxtC = new DataGridViewTextBoxColumn();   // 3 Colour       
            oTxtC.HeaderText = "Colour";
            oTxtC.ValueType = typeof(string);

            oTxtD = new DataGridViewTextBoxColumn();   // 4 Dye Order
            oTxtD.HeaderText = "Dye Order";
            oTxtD.ValueType = typeof(string);

            oTxtE = new DataGridViewTextBoxColumn();   // 5 date Ordered   
            oTxtE.HeaderText = "Date Ordered";
            oTxtE.ValueType = typeof(DateTime);


            oTxtF = new DataGridViewTextBoxColumn();   // 6 date required 
            oTxtF.HeaderText = "Date Required";
            oTxtF.ValueType = typeof(DateTime);

            oTxtG = new DataGridViewTextBoxColumn();   // 7 Body
            oTxtG.HeaderText = "Body";
            oTxtG.ValueType = typeof(string);


            oTxtH = new DataGridViewTextBoxColumn();   // 8 Trim 1
            oTxtH.HeaderText = "Binding";
            oTxtH.ValueType = typeof(string);

            oTxtJ = new DataGridViewTextBoxColumn();   // 9 Trim 2
            oTxtJ.HeaderText = "Trim";
            oTxtJ.ValueType = typeof(string);


            oTxtK = new DataGridViewTextBoxColumn();   // 10 Size
            oTxtK.HeaderText = "Size";
            oTxtK.ValueType = typeof(string);

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns.Add(oTxtG);
            dataGridView1.Columns.Add(oTxtH);
            dataGridView1.Columns.Add(oTxtJ);
            dataGridView1.Columns.Add(oTxtK);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

        }

        private void frmCSSelectDyeBatch_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                //var DBatches = (from T1 in context.TLDYE_DyeBatch
                //                join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                //                where !T1.DYEB_FabicSales && T2.DYEBO_QAApproved && !T2.DYEBO_Sold && !T2.DYEBO_WriteOff && !T2.DYEBO_CutSheet && !T1.DYEB_CommissinCust
                //                select T1).GroupBy(x => x.DYEB_Pk);

                //AS20240126 - Optimize query
                var DBatches = (
                    from T1 in context.TLDYE_DyeBatch
                    join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                    where !T1.DYEB_FabicSales &&
                          T2.DYEBO_QAApproved &&
                          !T2.DYEBO_Sold &&
                          !T2.DYEBO_WriteOff &&
                          !T2.DYEBO_CutSheet &&
                          !T1.DYEB_CommissinCust
                    select T1
                ).Distinct();

                foreach (var DBatch in DBatches)
                {
                    var index = dataGridView1.Rows.Add();

                    //AS20240126
                    dataGridView1.Rows[index].Cells[0].Value = DBatch.DYEB_Pk;
                    dataGridView1.Rows[index].Cells[2].Value = DBatch.DYEB_BatchNo;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(DBatch.DYEB_Colour_FK).Col_Display;
                    //dataGridView1.Rows[index].Cells[0].Value = DBatch.FirstOrDefault().DYEB_Pk;
                    //dataGridView1.Rows[index].Cells[2].Value = DBatch.FirstOrDefault().DYEB_BatchNo;
                    //dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(DBatch.FirstOrDefault().DYEB_Colour_FK).Col_Display;

                    //AS20240126
                    //var DO = context.TLDYE_DyeOrder.Find(DBatch.FirstOrDefault().DYEB_DyeOrder_FK);
                    var DO = context.TLDYE_DyeOrder.Find(DBatch.DYEB_DyeOrder_FK);
                        if (DO != null)
                        {
                            dataGridView1.Rows[index].Cells[4].Value = DO.TLDYO_OrderNum;
                            dataGridView1.Rows[index].Cells[5].Value = DO.TLDYO_OrderDate;
                            DateTime dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                            dt = dt.AddDays(5);
                            dataGridView1.Rows[index].Cells[6].Value = dt;

                            int cnt = 0;
                            var details = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk).ToList();
                            foreach (var row in details)
                            {
                                if (cnt == 0)
                                {
                                    dataGridView1.Rows[index].Cells[7].Value = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                }
                                else if (cnt == 1)
                                {
                                    dataGridView1.Rows[index].Cells[8].Value = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                }
                                else if (cnt == 2)
                                {
                                    dataGridView1.Rows[index].Cells[9].Value = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                }

                                // dataGridView1.Rows[index].Cells[10].Value = context.TLADM_Sizes.Find(row.TLDYOD_Sizes_FK).SI_Description;
                                ++cnt;
                            }

                        }
                    }
                }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             

             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 BatchSelected = (int)oDgv.CurrentRow.Cells[0].Value;
                 this.Close();
             }
        }
    }
}
