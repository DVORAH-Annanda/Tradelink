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


        public frmTLADM_QualityDefinition(int frmNo)
        {
            InitializeComponent();
            SetUp(frmNo);
            frmNumber = frmNo;
        }

        void SetUp(int fn)
        {
            dataGridView1.AutoGenerateColumns = false;

            selecta = new DataGridViewTextBoxColumn();  //0 Primqry Key
            selecta.Visible = false;
            selecta.ValueType = typeof(System.Int32);
            if (fn == 1)
            {
                this.Text = "Quality Defintions Update / Edit";
                selectb = new DataGridViewTextBoxColumn();  //1 Short Code 
                selectb.HeaderText = "QD Short Code";
                selectc = new DataGridViewTextBoxColumn();  //2 Description 
                selectc.HeaderText = "QD Description";
                selectd = new DataGridViewComboBoxColumn(); //3 Reject Reasons 
                selectd.HeaderText = "Reject Reasons";
                selecte = new DataGridViewComboBoxColumn(); //4 Reporting Dept FK
                selecte.HeaderText = "Reporting Dept";
                selectf = new DataGridViewComboBoxColumn(); //5 Originating Dept FK 
                selectf.HeaderText = "Originating Dept";
                selectg = new  DataGridViewTextBoxColumn(); //6 Originating Dept FK 
                selectg.HeaderText = "Power Number";
                selectg.ValueType = typeof(Int32);
                oChk = new DataGridViewCheckBoxColumn(); //7 Originating Dept FK 
                oChk.HeaderText = "Measurable";
                oChk.ValueType = typeof(bool);

            }
            else if (fn == 2)
            {
                this.Text = "Reject Reasons Update / Edit";
                selectb = new DataGridViewTextBoxColumn();  //1 Reject Reason 
                selectb.HeaderText = "Reject Reason";
            }
            else if (fn == 3)
            {
                this.Text = "Consumable Goods Groups Update / Edit";
                selectb = new DataGridViewTextBoxColumn();  //1 Consumable Goods Short Code 
                selectb.HeaderText = "Consumable Goods Short Code";
                selectc = new DataGridViewTextBoxColumn();  //2 Consumable goods Description 
                selectc.HeaderText = "Consumable Goods Description";
            }
            else if (fn == 4)
            {
                this.Text = "Stock Take Frequency codes Update / Edit";
                selectb = new DataGridViewTextBoxColumn();  //1 Stock Take Short Code 
                selectb.HeaderText = "Stock Take Short Code";
                selectc = new DataGridViewTextBoxColumn();  //2 Consumable goods Description 
                selectc.HeaderText = "Stock Take  Description";
                selectg = new DataGridViewTextBoxColumn();  //3 Stock Take Frequency in weeks 
                selectg.HeaderText = "Stock Take Freq Weeks";
                selectg.ValueType = typeof(System.Int32);
            }
            else if (fn == 5)
            {
                this.Text = "Machine operators Update / Edit";
                selectb = new DataGridViewTextBoxColumn();   //1 Machine Operator Code 
                selectb.HeaderText = "Machine Operator Short Code";
                selectc = new DataGridViewTextBoxColumn();   //2 Machine Operator Name / Description 
                selectc.HeaderText = "Machine Operator Name";
                selectd = new DataGridViewComboBoxColumn();  //3 Department index number    
                selectd.HeaderText = "Department Code";
                selectg = new DataGridViewTextBoxColumn();  //4 Payroll Code 
                selectg.HeaderText = "Payroll Code";
                selecth = new DataGridViewCheckBoxColumn(); // 5 Inspector
                selecth.HeaderText = "Quality Inspector";
                selectJ = new DataGridViewCheckBoxColumn(); // 6 Discontinued
                selectJ.HeaderText = "Discontinued";
            }
            else if (fn == 6)
            {
                this.Text = "Reject Reasons Update / Edit";
                selectb = new DataGridViewTextBoxColumn();   //1 Reject Reason 
                selectb.HeaderText = "Reject Reason";
                

            }
            else if (fn == 7)
            {
                this.Text = "Product Types (Good) Update / Edit";
                selectb = new DataGridViewTextBoxColumn();   //1  Product Type Code 
                selectb.HeaderText = "Code";
                selectc = new DataGridViewTextBoxColumn();  //2 Description 
                selectc.HeaderText = "Description";
                selectd = new DataGridViewComboBoxColumn();  //3 UOM    
                selectd.HeaderText = "UOM";
                selecth = new DataGridViewCheckBoxColumn();  //4 Std Cost  
                selecth.HeaderText = "Standard Cost";
                selectJ = new DataGridViewCheckBoxColumn();  //5 Std Cost  
                selectJ.HeaderText = "Hazardous";
                selectg = new DataGridViewTextBoxColumn();   //6  Standard Cost 
                selectg.HeaderText = "Standard Cost Amount";
                selectg.ValueType = typeof(decimal);
                

            }
            else if (fn == 8)
            {
                this.Text = "Finish Goods Update / Edit";
                selectb = new DataGridViewTextBoxColumn();   //1  Description 
                selectb.HeaderText = "Description";
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

                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);
                    dataGridView1.Columns.Add(selectc);
                    dataGridView1.Columns.Add(selectd);
                    dataGridView1.Columns.Add(selecte);
                    dataGridView1.Columns.Add(selectf);
                    dataGridView1.Columns.Add(oChk);

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;
                    
                    var ExistingData = context.TLADM_QualityDefinition.ToList();
                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.QD_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.QD_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.QD_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.QD_RejectReasonFK;
                        dataGridView1.Rows[index].Cells[4].Value = rw.QD_ReportingDept_FK;
                        dataGridView1.Rows[index].Cells[5].Value = rw.QD_OriginatingDept_FK; 
                        dataGridView1.Rows[index].Cells[6].Value = (bool)rw.QD_Measurable;

                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;


                }
                else if (fn == 2)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 50;
              
                    var ExistingData = context.TLADM_RejectReasons.ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.RJR_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.RJR_Description;
                        
                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (fn == 3)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);
                    dataGridView1.Columns.Add(selectc);

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;

                    var ExistingData = context.TLADM_ConsumableGroups.OrderBy(x=>x.ConG_ShortCode).ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.ConG_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.ConG_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.ConG_Description;

                    }

                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].Width = 250;

                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (fn == 4)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);
                    dataGridView1.Columns.Add(selectc);
                    dataGridView1.Columns.Add(selectg);

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;

                    dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

                    var ExistingData = context.TLADM_StockTakeFreq.ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.STF_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.STF_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.STF_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.STF_Period_Weeks;
                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (fn == 5)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectJ);
                    dataGridView1.Columns.Add(selectb);
                    dataGridView1.Columns.Add(selectc);
                    dataGridView1.Columns.Add(selectd);
                    dataGridView1.Columns.Add(selectg);
                    dataGridView1.Columns.Add(selecth);

                    selectd.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    selectd.DisplayMember = "Dep_Description";
                    selectd.ValueMember = "Dep_Id";

                    var ExistingData = context.TLADM_MachineOperators.ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.MachOp_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.MachOp_Discontinued;
                        dataGridView1.Rows[index].Cells[2].Value = rw.MachOp_Code;
                        dataGridView1.Rows[index].Cells[3].Value = rw.MachOp_Description;
                        dataGridView1.Rows[index].Cells[4].Value = rw.MachOp_Department_FK;
                        dataGridView1.Rows[index].Cells[5].Value = rw.MachOp_Payroll_Code;
                        dataGridView1.Rows[index].Cells[6].Value = rw.MachOp_Inspector;
                    }
                    
                }
                else if (fn == 6)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 50;
                    var ExistingData = context.TLADM_RejectReasons.ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.RJR_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.RJR_Description;
                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (fn == 7)
                {
                    dataGridView1.Columns.Add(selecta);  // pk
                    dataGridView1.Columns.Add(selectb);  // code
                    dataGridView1.Columns.Add(selectc);  // description
                    dataGridView1.Columns.Add(selectd);  // UOM
                    dataGridView1.Columns.Add(selecth);  // Std Cost
                    dataGridView1.Columns.Add(selectJ);  // Hazardous 
                    dataGridView1.Columns.Add(selectg);  // Std Cost 

                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;

                    selectd.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    selectd.DisplayMember = "UOM_Description";
                    selectd.ValueMember = "UOM_Pk";

                    var ExistingData = context.TLADM_ProductTypes.ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.PT_pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.PT_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.PT_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.PT_UOMFk;
                        if (rw.PT_StdCost)
                            dataGridView1.Rows[index].Cells[4].Value = true;
                        else
                            dataGridView1.Rows[index].Cells[4].Value = false;
                        if(rw.PT_Hazardous)
                            dataGridView1.Rows[index].Cells[5].Value = true;
                        else
                            dataGridView1.Rows[index].Cells[5].Value = false;

                        dataGridView1.Rows[index].Cells[6].Value = Math.Round(rw.PT_StdCostValue,2).ToString(); ;
                    }
                  
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (fn == 8)
                {
                    dataGridView1.Columns.Add(selecta);
                    dataGridView1.Columns.Add(selectb);

                   
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 50;

                  
                    var ExistingData = context.TLADM_FinishedGoods.OrderBy(x=>x.Fin_Description).ToList();

                    foreach (var rw in ExistingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.Fin_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.Fin_Description;
                    }

                    dataGridView1.Columns[1].Width = 250;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
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
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_QualityDefinition dpt = new TLADM_QualityDefinition();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_QualityDefinition.Find(pk);
                            }

                            dpt.QD_ShortCode = row.Cells[1].Value.ToString();
                            dpt.QD_Description = row.Cells[2].Value.ToString();
                            dpt.QD_RejectReasonFK = Convert.ToInt32(row.Cells[3].Value.ToString());
                            dpt.QD_ReportingDept_FK = Convert.ToInt32(row.Cells[4].Value.ToString());
                            dpt.QD_OriginatingDept_FK = Convert.ToInt32(row.Cells[5].Value.ToString());
                            dpt.QD_Measurable = Convert.ToBoolean(row.Cells[6].Value.ToString());
                            if (lAdd)
                                context.TLADM_QualityDefinition.Add(dpt);

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
                    else if (frmNumber == 2)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_RejectReasons dpt = new TLADM_RejectReasons();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_RejectReasons.Find(pk);
                            }

                            dpt.RJR_Description = row.Cells[1].Value.ToString();
                           

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
                    else if (frmNumber == 3)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_ConsumableGroups dpt = new TLADM_ConsumableGroups();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_ConsumableGroups.Find(pk);
                            }

                            dpt.ConG_ShortCode = row.Cells[1].Value.ToString();
                            dpt.ConG_Description = row.Cells[2].Value.ToString();


                            if (lAdd)
                                context.TLADM_ConsumableGroups.Add(dpt);

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
                    else if (frmNumber == 4)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_StockTakeFreq dpt = new TLADM_StockTakeFreq();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_StockTakeFreq.Find(pk);
                            }

                            dpt.STF_ShortCode = row.Cells[1].Value.ToString();
                            dpt.STF_Description = row.Cells[2].Value.ToString();
                            dpt.STF_Period_Weeks = Convert.ToInt32(row.Cells[3].Value.ToString());
                            if (lAdd)
                                context.TLADM_StockTakeFreq.Add(dpt);

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
                    else if (frmNumber == 5)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            if (row.Cells[1].Value.ToString() == string.Empty)
                                continue;

                            lAdd = false;

                            TLADM_MachineOperators dpt = new TLADM_MachineOperators();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_MachineOperators.Find(pk);
                            }

                            dpt.MachOp_Discontinued = (bool)row.Cells[1].Value;
                            if (dpt.MachOp_Discontinued && dpt.MachOp_Discontinued_Date == null)
                                dpt.MachOp_Discontinued_Date = DateTime.Now.Date;

                            dpt.MachOp_Code = row.Cells[2].Value.ToString();
                            dpt.MachOp_Description = row.Cells[3].Value.ToString();
                            dpt.MachOp_Department_FK = Convert.ToInt32(row.Cells[4].Value.ToString());
                            dpt.MachOp_Payroll_Code = row.Cells[5].Value.ToString();
                            if (row.Cells[6].Value == null)
                            {
                                dpt.MachOp_Inspector = false;
                            }
                            else
                                dpt.MachOp_Inspector =  Convert.ToBoolean(row.Cells[6].Value.ToString());
                           
                            if (lAdd)
                                context.TLADM_MachineOperators.Add(dpt);

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
                    else if (frmNumber == 6)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_RejectReasons dpt = new TLADM_RejectReasons();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_RejectReasons.Find(pk);
                            }

                            dpt.RJR_Description = row.Cells[1].Value.ToString();
                            
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
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_ProductTypes dpt = new TLADM_ProductTypes();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_ProductTypes.Find(pk);
                            }

                            dpt.PT_ShortCode = row.Cells[1].Value.ToString();
                            dpt.PT_Description = row.Cells[2].Value.ToString();
                            dpt.PT_UOMFk = Convert.ToInt32(row.Cells[3].Value.ToString());
                            
                            if (row.Cells[4].Value != null)
                            {
                                if (row.Cells[4].Value.ToString() == bool.TrueString)
                                    dpt.PT_StdCost = true;
                                else
                                    dpt.PT_StdCost = false;
                            }
                            else
                                dpt.PT_StdCost = false;

                            if (row.Cells[5].Value != null)
                            {
                                if (row.Cells[5].Value.ToString() == bool.TrueString)
                                    dpt.PT_Hazardous = true;
                                else
                                    dpt.PT_Hazardous = false;
                            }
                            else
                                dpt.PT_Hazardous = false;

                            dpt.PT_StdCostValue = Convert.ToDecimal(row.Cells[6].Value.ToString());
                            
                            if (lAdd)
                                context.TLADM_ProductTypes.Add(dpt);

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
                    else if (frmNumber == 8)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            lAdd = false;

                            TLADM_FinishedGoods dpt = new TLADM_FinishedGoods();

                            if (row.Cells[0].Value == null)
                            {
                                lAdd = true;
                            }
                            else
                            {
                                int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                                dpt = context.TLADM_FinishedGoods.Find(pk);
                            }

                            dpt.Fin_Description = row.Cells[1].Value.ToString();

                            if (lAdd)
                                context.TLADM_FinishedGoods.Add(dpt);

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
                }
                if (lSuccess)
                {
                    dataGridView1.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
                                        oDgv.Rows.Clear();
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
