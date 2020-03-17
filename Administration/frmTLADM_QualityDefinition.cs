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

namespace TTI2_WF
{
    public partial class frmTLADM_QualityDefinition : Form
    {
        DataTable dt;
        
        DataGridViewTextBoxColumn selecta;
        DataGridViewTextBoxColumn selectb;
        DataGridViewTextBoxColumn selectc;
        DataGridViewComboBoxColumn selectd;
        DataGridViewComboBoxColumn selecte;
        DataGridViewComboBoxColumn selectf;
        DataGridViewTextBoxColumn selectg;
        DataGridViewCheckBoxColumn selecth;
        DataGridViewCheckBoxColumn selectJ;

        DataGridViewCheckBoxColumn oChk;

        int frmNumber;
        bool nonNumeric;

        BindingSource BindingSrc;

        public frmTLADM_QualityDefinition(int frmNo)
        {
            InitializeComponent();
            SetUp(frmNo);
            frmNumber = frmNo;
        }

        void SetUp(int fn)
        {
            dataGridView1.AutoGenerateColumns = false;
            //============================================================
            //---------Define the datatable 
            //=================================================================
            dt = new DataTable(); 
            DataColumn column;
            BindingSrc = new BindingSource();

            //------------------------------------------------------
            // Create column 0. // This is the record index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            column.DefaultValue = 0;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the whether the record is discontinued or not 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "Col1";
            column.Caption = "Discontinued";
            column.DefaultValue = false;
            dt.Columns.Add(column);

            //=================================================
            //0 -- index of record 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "TableIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Table Index";
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns["TableIndex"].DisplayIndex = 0;

            selectJ = new DataGridViewCheckBoxColumn();
            selectJ.Name = "Discontinued";
            selectJ.ValueType = typeof(bool);
            selectJ.DataPropertyName = dt.Columns[1].ColumnName;
            selectJ.HeaderText = "Discontinued";
            selectJ.Width = 100;
            dataGridView1.Columns.Add(selectJ);
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns["Discontinued"].DisplayIndex = 1;
           
            if (fn == 1)
            {
                this.Text = "Quality Defintions Update / Edit";
               
                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "QD Short Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col3";
                column.DefaultValue = String.Empty;
                column.Caption = "QD  Description";
                dt.Columns.Add(column);
           
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col4";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_RejectReasons.FirstOrDefault().RJR_Pk;
                }
                column.Caption = "Reject Reason";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col5";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_Departments.FirstOrDefault().Dep_Id;
                }
                column.Caption = "Reporting Department";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col6";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_Departments.FirstOrDefault().Dep_Id;
                }
                column.Caption = "Originating Department";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "Col7";
                column.DefaultValue = false;
                column.Caption = "Measurable";
                dt.Columns.Add(column);

                //-------------------------------------------------------------------------------------
                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "Name";
                selectb.HeaderText = "QD Short Code";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Name"].DisplayIndex = 2;

                selectc = new DataGridViewTextBoxColumn();    
                selectc.HeaderText = "QD Description";
                selectc.Name = "QDDescrip";
                selectc.HeaderText = "Reject Reason";
                selectc.ValueType = typeof(System.String);
                selectc.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["QDDescrip"].DisplayIndex = 3;

                selectd = new DataGridViewComboBoxColumn();
                selectd.HeaderText = "Reject Reasons";
                selectd.Name = "RReasons";
                selectd.ValueType = typeof(System.Int32);
                selectd.DataPropertyName = dt.Columns[4].ColumnName;
                dataGridView1.Columns.Add(selectd);
                dataGridView1.Columns["RReasons"].DisplayIndex = 4;

                selecte = new DataGridViewComboBoxColumn();      
                selecte.HeaderText = "Reporting Department";
                selecte.Name = "RDName";
                selecte.ValueType = typeof(System.Int32);
                selecte.DataPropertyName = dt.Columns[5].ColumnName;
                dataGridView1.Columns.Add(selecte);
                dataGridView1.Columns["RDName"].DisplayIndex = 5;

                selectf = new DataGridViewComboBoxColumn();   
                selectf.HeaderText = "Originating Department";
                selectf.Name = "ORName";
                selectf.ValueType = typeof(System.Int32);
                selectf.DataPropertyName = dt.Columns[6].ColumnName;
                dataGridView1.Columns.Add(selectf);
                dataGridView1.Columns["ORName"].DisplayIndex = 6;

                selecth = new DataGridViewCheckBoxColumn(); 
                selecth.HeaderText = "Measurable";
                selecth.Name = "INSName";
                selecth.ValueType = typeof(System.Boolean);
                selecth.DataPropertyName = dt.Columns[7].ColumnName;
                dataGridView1.Columns.Add(selecth);
                dataGridView1.Columns["INSName"].DisplayIndex = 7;

                dataGridView1.Width = 650;

            }
            else if (fn == 2)
            {
               
            }
            else if (fn == 3)
            {
                this.Text = "Consumable Goods Groups Update / Edit";
               
                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Consumables Short Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col3";
                column.DefaultValue = String.Empty;
                column.Caption = "Consumables Description";
                dt.Columns.Add(column);

                //----------------------------------------------
                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "Name";
                selectb.HeaderText = "Consumables Short Code";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Name"].DisplayIndex = 2;


                selectc = new DataGridViewTextBoxColumn();    
                selectc.HeaderText = "Consumables Name";
                selectc.Name = "OpName";
                selectc.ValueType = typeof(System.String);
                selectc.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["OpName"].DisplayIndex = 3;


            }
            else if (fn == 4)
            {
                this.Text = "Stock Take Frequency codes Update / Edit";
               
                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Stock Take Short Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col3";
                column.DefaultValue = String.Empty;
                column.Caption = "Stock Take Description";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col4";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_StockTakeFreq.FirstOrDefault().STF_Pk;
                }
                column.Caption = "Stock Take Frequency";
                dt.Columns.Add(column);

                //----------------------------------------------
                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "Name";
                selectb.HeaderText = "Stock Take Short Code";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Name"].DisplayIndex = 2;
                
                selectc = new DataGridViewTextBoxColumn();
                selectc.HeaderText = "Stock Take Description";
                selectc.Name = "OpName";
                selectc.ValueType = typeof(System.String);
                selectc.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["OpName"].DisplayIndex = 3;

                selectg = new DataGridViewTextBoxColumn();
                selectg.HeaderText = "Stock Take Frequency";
                selectg.Name = "StkFreq";
                selectg.ValueType = typeof(System.String);
                selectg.DataPropertyName = dt.Columns[4].ColumnName;
                dataGridView1.Columns.Add(selectg);
                dataGridView1.Columns["StkFreq"].DisplayIndex = 4;

            }
            else if (fn == 5)
            {
                this.Text = "Machine operators Update / Edit";
                
                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Machine Operator Short Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col3";
                column.DefaultValue = String.Empty;
                column.Caption = "Machine Operator Name / Description";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col4";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_Departments.FirstOrDefault().Dep_Id;
                }
                column.Caption = "Department";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col5";
                column.DefaultValue = String.Empty;
                column.Caption = "Payroll Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "Col6";
                column.DefaultValue = false;
                column.Caption = "Quality Inspector";
                dt.Columns.Add(column);

                //----------------------------------------------
                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "Name";
                selectb.HeaderText = "Operator Short Code";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Name"].DisplayIndex = 2;


                selectc = new DataGridViewTextBoxColumn();   //2 Machine Operator Name / Description 
                selectc.HeaderText = "Machine Operator Name";
                selectc.Name = "OpName";
                selectc.ValueType = typeof(System.String);
                selectc.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["OpName"].DisplayIndex = 3;

                selectd = new DataGridViewComboBoxColumn();  //3 Department index number    
                selectd.HeaderText = "Department Code";
                selectd.Name = "DCName";
                selectd.ValueType = typeof(System.String);
                selectd.DataPropertyName = dt.Columns[4].ColumnName;
                dataGridView1.Columns.Add(selectd);
                dataGridView1.Columns["DCName"].DisplayIndex = 4;

                selectg = new DataGridViewTextBoxColumn();  //4 Payroll Code 
                selectg.HeaderText = "Payroll Code";
                selectg.Name = "PCName";
                selectg.ValueType = typeof(System.String);
                selectg.DataPropertyName = dt.Columns[5].ColumnName;
                dataGridView1.Columns.Add(selectg);
                dataGridView1.Columns["PCName"].DisplayIndex = 5;

                selecth = new DataGridViewCheckBoxColumn(); // 5 Inspector
                selecth.HeaderText = "Quality Inspector";
                selecth.Name = "INSName";
                selecth.ValueType = typeof(System.String);
                selecth.DataPropertyName = dt.Columns[6].ColumnName;
                dataGridView1.Columns.Add(selecth);
                dataGridView1.Columns["INSName"].DisplayIndex = 6;

                dataGridView1.Width = 650;
            

            }
            else if (fn == 6)
            {
                this.Text = "Reject Reasons Update / Edit";
                
                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Reject Reason";
                dt.Columns.Add(column);

                //----------------------------------------------
                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "RName";
                selectb.HeaderText = "Reject Reason";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["RName"].DisplayIndex = 2;

                dataGridView1.Width = 350;

            }
            else if (fn == 7)
            {
                this.Text = "Product Types (Good) Update / Edit";
              /*  selectb = new DataGridViewTextBoxColumn();   //1  Product Type Code 
                selectb.HeaderText = "Code";
                selectc = new DataGridViewTextBoxColumn();  //2 Description 
                selectc.HeaderText = "Description";
                selectd = new DataGridViewComboBoxColumn();  //3 UOM    
                selectd.HeaderText = "UOM";
                selecth = new DataGridViewCheckBoxColumn();  //4 Std Cost  
                selecth.HeaderText = "Standard Cost";
                selectJ = new DataGridViewCheckBoxColumn();  //5 Hazardous  
                selectJ.HeaderText = "Hazardous";
                selectg = new DataGridViewTextBoxColumn();   //6  Standard Cost Amount
                selectg.HeaderText = "Standard Cost Amount";
                selectg.ValueType = typeof(decimal); */

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Code";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col3";
                column.DefaultValue = String.Empty;
                column.Caption = "Description";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col4";
                using (var context = new TTI2Entities())
                {
                    column.DefaultValue = context.TLADM_UOM.FirstOrDefault().UOM_Pk;
                }
                column.Caption = "UOM";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(System.Boolean);
                column.ColumnName = "Col5";
                column.DefaultValue = 0.00M;
                column.Caption = "Standard Cost";
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "Col6";
                column.DefaultValue = false;
                column.Caption = "Hazardous";
                dt.Columns.Add(column);


                column = new DataColumn();
                column.DataType = typeof(System.Decimal);
                column.ColumnName = "Col7";
                column.DefaultValue = 0.00M;
                column.Caption = "Standard Cost Amount";
                dt.Columns.Add(column);

                //----------------------------------------------
                //------------------------------------------------------------------------------

                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "Name";
                selectb.HeaderText = "Product Type Code";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Name"].DisplayIndex = 2;


                selectc = new DataGridViewTextBoxColumn();    
                selectc.HeaderText = "Poduct Type Description";
                selectc.Name = "OpName";
                selectc.ValueType = typeof(System.String);
                selectc.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["OpName"].DisplayIndex = 3;

                selectd = new DataGridViewComboBoxColumn();      
                selectd.HeaderText = "UOM";
                selectd.Name = "DCName";
                selectd.ValueType = typeof(System.Int32);
                selectd.DataPropertyName = dt.Columns[4].ColumnName;
                dataGridView1.Columns.Add(selectd);
                dataGridView1.Columns["DCName"].DisplayIndex = 4;

                selecth = new DataGridViewCheckBoxColumn();   
                selecth.HeaderText = "Standard Cost";
                selecth.Name = "PCName";
                selecth.ValueType = typeof(System.Boolean);
                selecth.DataPropertyName = dt.Columns[5].ColumnName;
                dataGridView1.Columns.Add(selecth);
                dataGridView1.Columns["PCName"].DisplayIndex = 5;

                selectJ = new DataGridViewCheckBoxColumn(); // 5 Inspector
                selectJ.HeaderText = "Hazardous";
                selectJ.Name = "INSName";
                selectJ.ValueType = typeof(System.Boolean);
                selectJ.DataPropertyName = dt.Columns[6].ColumnName;
                dataGridView1.Columns.Add(selectJ);
                dataGridView1.Columns["INSName"].DisplayIndex = 6;

                selectg = new DataGridViewTextBoxColumn();
                selectg.HeaderText = "Standard Cost Amt";
                selectg.Name = "StdCostAmt";
                selectg.ValueType = typeof(System.Decimal);
                selectg.DataPropertyName = dt.Columns[7].ColumnName;
                dataGridView1.Columns.Add(selectg);
                dataGridView1.Columns["StdCostAmt"].DisplayIndex = 7;

                dataGridView1.Width = 650;
            }
            else if (fn == 8)
            {
                this.Text = "Finish Goods Update / Edit";
                selectb = new DataGridViewTextBoxColumn();   //1  Description 
                selectb.HeaderText = "Description";

                column = new DataColumn();
                column.DataType = typeof(System.String);
                column.ColumnName = "Col2";
                column.DefaultValue = String.Empty;
                column.Caption = "Description";
                dt.Columns.Add(column);


                selectb = new DataGridViewTextBoxColumn();
                selectb.Name = "FGName";
                selectb.HeaderText = "Description";
                selectb.ValueType = typeof(System.String);
                selectb.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["FGName"].DisplayIndex = 2;

                dataGridView1.Width = 350;

            }

            using (var context = new TTI2Entities())
            {
                if (fn == 1)
                {
                    selectd.DataSource = context.TLADM_RejectReasons.OrderBy(x => x.RJR_Description).ToList();
                    selectd.DisplayMember = "RJR_Description";
                    selectd.ValueMember = "RJR_Pk";

                    selecte.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    selecte.DisplayMember = "Dep_Description";
                    selecte.ValueMember = "Dep_Id";

                    selectf.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    selectf.DisplayMember = "Dep_Description";
                    selectf.ValueMember = "Dep_Id";

                                   
                    var ExistingData = context.TLADM_QualityDefinition.ToList();
                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = rw.QD_Pk;
                        dr[1] = false;
                        dr[2] = rw.QD_ShortCode;
                        dr[3] = rw.QD_Description;
                        dr[4] = rw.QD_RejectReasonFK;
                        dr[5] = rw.QD_ReportingDept_FK;
                        dr[6] = rw.QD_OriginatingDept_FK; 
                        dr[7] = (bool)rw.QD_Measurable;

                        dt.Rows.Add(dr);
                    }

                }
                else if (fn == 2)
                {
                   
                }
                else if (fn == 3)
                {
                    var ExistingData = context.TLADM_ConsumableGroups.OrderBy(x=>x.ConG_ShortCode).ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] =  rw.ConG_Pk;
                        dr[1] = false;
                        dr[2] = rw.ConG_ShortCode;
                        dr[3] = rw.ConG_Description;

                        dt.Rows.Add(dr);
                    }
                
                }
                else if (fn == 4)
                {
                    var ExistingData = context.TLADM_StockTakeFreq.ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();

                        dr[0] = rw.STF_Pk;
                        dr[1] = false;
                        dr[2] = rw.STF_ShortCode;
                        dr[3] = rw.STF_Description;
                        dr[4] = rw.STF_Period_Weeks;

                        dt.Rows.Add(dr);
                    }
             
                }
                else if (fn == 5)
                {
                    selectd.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    selectd.DisplayMember = "Dep_Description";
                    selectd.ValueMember = "Dep_Id";

                    var ExistingData = context.TLADM_MachineOperators.ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();
                        
                        dr[0] = rw.MachOp_Pk;
                        dr[1] = (bool)rw.MachOp_Discontinued;
                        dr[2] = rw.MachOp_Code;
                        dr[3] = rw.MachOp_Description;
                        dr[4] = (int)rw.MachOp_Department_FK;
                        dr[5] = rw.MachOp_Payroll_Code;
                        dr[6] = (bool)rw.MachOp_Inspector;

                        dt.Rows.Add(dr);
                    
                    }
                    
                   
                }
                else if (fn == 6)
                {
                     var ExistingData = context.TLADM_RejectReasons.ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = rw.RJR_Pk;
                        dr[1] = false;
                        dr[2] = rw.RJR_Description;

                        dt.Rows.Add(dr);
                    }
                
                }
                else if (fn == 7)
                {
                    /*dataGridView1.Columns.Add(selecta);  // pk
                    dataGridView1.Columns.Add(selectb);  // code
                    dataGridView1.Columns.Add(selectc);  // description
                    dataGridView1.Columns.Add(selectd);  // UOM
                    dataGridView1.Columns.Add(selecth);  // Std Cost boolean
                    dataGridView1.Columns.Add(selectJ);  // Hazardous 
                    dataGridView1.Columns.Add(selectg);  // Std Cost */

                    selectd.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    selectd.DisplayMember = "UOM_Description";
                    selectd.ValueMember = "UOM_Pk";

                    var ExistingData = context.TLADM_ProductTypes.ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();

                        var index = dataGridView1.Rows.Add();
                        dr[0] = rw.PT_pk;
                        dr[1] = false;
                        dr[2] = rw.PT_ShortCode;
                        dr[3] = rw.PT_Description;
                        dr[4] = rw.PT_UOMFk;
                        dr[5] = (bool)rw.PT_StdCost;
                        dr[6] = (bool)rw.PT_Hazardous;
                        dr[7] = Math.Round(rw.PT_StdCostValue, 2);
                    }
                }
                else if (fn == 8)
                {
                    // dataGridView1.Columns.Add(selecta);
                    // dataGridView1.Columns.Add(selectb);

                    var ExistingData = context.TLADM_FinishedGoods.OrderBy(x=>x.Fin_Description).ToList();

                    foreach (var rw in ExistingData)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = rw.Fin_Pk;
                        dr[1] = false;
                        dr[2] = rw.Fin_Description;

                        dt.Rows.Add(dr);
                    }
          
                }
            }

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;


            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0 || idx > 7)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;

            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lAdd;
            bool lSuccess = true;

            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    if(frmNumber == 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                        
                            lAdd = false;

                            TLADM_QualityDefinition dpt = new TLADM_QualityDefinition();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_QualityDefinition.Find(row.Field<int>(0));
                            }

                            dpt.QD_ShortCode = row.Field<string>(2);
                            dpt.QD_Description = row.Field<string>(3);
                            dpt.QD_RejectReasonFK = row.Field<int>(4);
                            dpt.QD_ReportingDept_FK = row.Field<int>(5);
                            dpt.QD_OriginatingDept_FK = row.Field<int>(6);
                            dpt.QD_Measurable = row.Field<bool>(7);
                            if (lAdd)
                                context.TLADM_QualityDefinition.Add(dpt);
                      
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }

                    }
                    else if (frmNumber == 2)
                    {
                        
                    }
                    else if (frmNumber == 3)
                    {
  
                        foreach (DataRow row in dt.Rows)
                        {
                            lAdd = false;

                            TLADM_ConsumableGroups dpt = new TLADM_ConsumableGroups();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_ConsumableGroups.Find(row.Field<int>(0));
                            }

                            dpt.ConG_ShortCode = row.Field<string>(2);
                            dpt.ConG_Description = row.Field<string>(3);


                            if (lAdd)
                                context.TLADM_ConsumableGroups.Add(dpt);

                           
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }
                    }
                    else if (frmNumber == 4)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            lAdd = false;

                            TLADM_StockTakeFreq dpt = new TLADM_StockTakeFreq();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_StockTakeFreq.Find(row.Field<int>(0));
                            }

                            dpt.STF_ShortCode = row.Field<string>(2);
                            dpt.STF_Description = row.Field<string>(3);
                            dpt.STF_Period_Weeks = row.Field<int>(4);
                            if (lAdd)
                                context.TLADM_StockTakeFreq.Add(dpt);
                                                      
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }


                    }
                    else if (frmNumber == 5)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            lAdd = false;
                            TLADM_MachineOperators dpt = new TLADM_MachineOperators();

                            if (dr.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_MachineOperators.Find(dr.Field<int>(0));
                            }

                            dpt.MachOp_Discontinued = dr.Field<bool>(1);

                            if(dr.Field<bool>(1) && dpt.MachOp_Discontinued_Date == null)
                            {
                                dpt.MachOp_Discontinued_Date = DateTime.Now;
                            }

                            dpt.MachOp_Code = dr.Field<string>(2);
                            dpt.MachOp_Description = dr.Field<string>(3);
                            dpt.MachOp_Department_FK = dr.Field<int>(4);
                            dpt.MachOp_Payroll_Code = dr.Field<string>(5);
                            dpt.MachOp_Inspector = dr.Field<bool>(6);


                            if (lAdd)
                            {
                                context.TLADM_MachineOperators.Add(dpt);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }
                    }
                    else if (frmNumber == 6)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            lAdd = false;

                            TLADM_RejectReasons dpt = new TLADM_RejectReasons();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_RejectReasons.Find(row.Field<int>(0));
                            }

                            dpt.RJR_Description = row.Field<string>(2);
                            
                            if (lAdd)
                                context.TLADM_RejectReasons.Add(dpt);

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                lSuccess = false;
                            }
                        }
                    }
                    else if (frmNumber == 7)
                    {
                        foreach (DataRow row in dataGridView1.Rows)
                        {
                            lAdd = false;

                            TLADM_ProductTypes dpt = new TLADM_ProductTypes();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_ProductTypes.Find(row.Field<int>(0));
                            }

                            dpt.PT_ShortCode   = row.Field<string>(2);
                            dpt.PT_Description = row.Field<string>(3);
                            dpt.PT_UOMFk = row.Field<int>(4);
                            dpt.PT_StdCost = row.Field<bool>(5);
                            dpt.PT_Hazardous = row.Field<bool>(6);
                            dpt.PT_StdCostValue = row.Field<decimal>(7);
                            
                            if (lAdd)
                                context.TLADM_ProductTypes.Add(dpt);
    
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }
                    }
                    else if (frmNumber == 8)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            lAdd = false;

                            TLADM_FinishedGoods dpt = new TLADM_FinishedGoods();

                            if (row.Field<int>(0) == 0)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                dpt = context.TLADM_FinishedGoods.Find(row.Field<int>(0));
                            }

                            dpt.Fin_Description = row.Field<string>(2);

                            if (lAdd)
                                context.TLADM_FinishedGoods.Add(dpt);
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }
                    }
                }
                if (lSuccess)
                {
                    dt.Rows.Clear();
                    MessageBox.Show("Records have been updated successfully");
                    this.Close();
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (frmNumber < 4)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 2)
                {
                    e.Control.KeyDown -= new KeyEventHandler(txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(txtWin_KeyPress);
                }
            }
            else
            {
                if (frmNumber < 7)
                {
                    if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(txtWin_KeyPress);
                    }
                }
                else
                {
                    if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(txtWin_KeyPress);
                    }
                }
            }
        }

        private void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;
            
            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

                
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        private void txtWin_KeyDownOEM(object sender, KeyEventArgs e)
        {
            nonNumeric = false;
            TextBox oTxt = sender as TextBox;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            
                if (e.KeyCode == Keys.OemPeriod)
                {
                    var PCount = oTxt.Text.Count(x => x == '.');
                    if (PCount > 0)
                        nonNumeric = true;
                }
            
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        private void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumeric)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {

                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    using (var context = new TTI2Entities())
                    {
                        if (res == DialogResult.OK)
                        {
                            int Pk = (int)oDgv.CurrentRow.Cells[0].Value;

                            if (frmNumber == 1)
                            {

                                var dpt = context.TLADM_QualityDefinition.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_QualityDefinition.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();  
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 2)
                            {
                                // context.TLADM_RejectReasons.Add(dpt);
                                var dpt = context.TLADM_RejectReasons.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_RejectReasons.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }


                            }
                            else if (frmNumber == 3)
                            {

                                //dpt = context.TLADM_ConsumableGroups.Find(pk);
                                var dpt = context.TLADM_ConsumableGroups.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_ConsumableGroups.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 4)
                            {

                                //    dpt = context.TLADM_StockTakeFreq.Find(pk);
                                var dpt = context.TLADM_StockTakeFreq.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_StockTakeFreq.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 5)
                            {
                                // dpt = context.TLADM_MachineOperators.Find(pk);
                                var dpt = context.TLADM_MachineOperators.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_MachineOperators.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 6)
                            {

                                //dpt = context.TLADM_RejectReasons.Find(pk);
                                var dpt = context.TLADM_RejectReasons.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_RejectReasons.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 7)
                            {
                                // dpt = context.TLADM_ProductTypes.Find(pk);
                                var dpt = context.TLADM_ProductTypes.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_ProductTypes.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                            }
                            else if (frmNumber == 8)
                            {
                                // context.TLADM_FinishedGoods.Add(dpt);
                                var dpt = context.TLADM_FinishedGoods.Find(Pk);
                                if (dpt != null)
                                {
                                    context.TLADM_FinishedGoods.Remove(dpt);
                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("data successfully saved");
                                        dt.Rows.Clear();


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
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
           
            
            
        }
    }
}
