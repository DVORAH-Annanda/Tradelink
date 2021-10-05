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

namespace DyeHouse
{
    public partial class frmDyeOrders : Form
    {
        decimal FabricYield;

        Util core;
        bool formloaded;
     
        bool[] MandSelected;
        string[][] MandatoryFields;

        DataColumn column;

        BindingSource BindingSrc;
        DataTable DataT;

        BindingSource BindingSrc2;
        DataTable DataT2;
        
        
        TLADM_CustomerFile CustomerSelected = null; 

        DataGridViewTextBoxColumn selecta;  // 0 Pk 
        DataGridViewTextBoxColumn selectb;  // 1 Desc
        DataGridViewTextBoxColumn selectc;  // 2 Requested meters
        DataGridViewTextBoxColumn selectd;  // 3 Requested meters to Kg
        DataGridViewTextBoxColumn selecte;  // 4 Projected Loss
        DataGridViewTextBoxColumn selectf;  // 5 Final Demand

        DataGridViewTextBoxColumn selecta1;  // Index pos of current record 
        DataGridViewTextBoxColumn selectb1;  // requirements
        DataGridViewTextBoxColumn selectc1;  // Projected Loss
        DataGridViewTextBoxColumn selectd1;  // Volume Need
        DataGridViewTextBoxColumn selecte1;  // Volume Need
        DataGridViewTextBoxColumn selectf1;  // Volume Need
        public frmDyeOrders()
        {
            InitializeComponent();
            core = new Util();
            
            column = new DataColumn();

            BindingSrc = new BindingSource();
            DataT = new DataTable();
            

            MandatoryFields = new string[][]
            {   new string[] {"cmboCustomerNo", "Please select a customer number", "0"},
                new string[] {"dtpDyeOrder", "Please enter the date of order", "1"},
                new string[] {"txtWFDye", "Please enter the week no required", "2"},
                new string[] {"cmboFabricOrder", "Please enter the customer order number", "3"}};

            //============================================================
            //---------Define the datatable 
            //------------------------------------------------------
            // Create column 0. // This is Index Position 
            //----------------------------------------------
            column.DataType = typeof(Int32);
            column.DefaultValue = 0;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.Caption = "Description";
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.Caption = "Requirements (Meters)";
            DataT.Columns.Add(column);
            
            //------------------------------------------------------
            // Create column 3. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.Caption = "Requirements (Kgs)";
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.Caption = "Projected Loss";
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 5. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.Caption = "Final Demand";
            DataT.Columns.Add(column);
          

            selecta = new DataGridViewTextBoxColumn();
            selecta.HeaderText = "";
            selecta.Visible = false;
            selecta.ReadOnly = true;
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = DataT.Columns[0].ColumnName;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;

            selectb = new DataGridViewTextBoxColumn();
            selectb.HeaderText = "Quality Description";
            selectb.Width = 200;
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = DataT.Columns[1].ColumnName;
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[1].DisplayIndex = 1;

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Requirements (Meters)";
            selectc.ReadOnly = true;
            selectc.Visible = true;
            selectc.DataPropertyName = DataT.Columns[2].ColumnName;
            selectc.ValueType = typeof(decimal);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[2].DisplayIndex = 2;

            selectd = new DataGridViewTextBoxColumn();
            selectd.HeaderText = "Requirements (Kgs)";
            selectd.ReadOnly = true;
            selectd.ValueType = typeof(decimal);
            selectd.DataPropertyName = DataT.Columns[3].ColumnName;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns[3].DisplayIndex = 3;

            selecte = new DataGridViewTextBoxColumn();
            selecte.ReadOnly = true;
            selecte.Visible = true;
            selecte.ValueType = typeof(decimal);
            selecte.DataPropertyName = DataT.Columns[4].ColumnName;
            selecte.HeaderText = "Proj loss";
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns[4].DisplayIndex = 4;

            selectf = new DataGridViewTextBoxColumn();
            selectf.ReadOnly = true;
            selectf.Visible = true;
            selectf.ValueType = typeof(decimal);
            selectf.DataPropertyName = DataT.Columns[5].ColumnName;
            selectf.HeaderText = "Demand";
            dataGridView1.Columns.Add(selectf);
            dataGridView1.Columns[5].DisplayIndex = 5;
            

            BindingSrc.DataSource = DataT;
            dataGridView1.DataSource = BindingSrc;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
           
            
            BindingSrc2 = new BindingSource();
            DataT2 = new DataTable();


            //============================================================
            //---------Define the datatable2 
            //------------------------------------------------------
            // Create column 0. // This is Index Position of the measurement in the TLADM_QADyeProcessFields Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Trims_Pk2";
            column.DefaultValue = 0;
            DataT2.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is Index Position of the dof Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Trim_Description2";
            column.Caption = "Description";
            DataT2.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // This is 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.ColumnName = "Trim_RequiremntMeters";
            column.Caption = "Requirements Meters";
            DataT2.Columns.Add(column);

            //------------------------------------------------------
            // Create column 3. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.ColumnName = "Trim_RequirementKg";
            column.Caption = "Requirement Kgs";
            DataT2.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.ColumnName = "Trim_PLoss";
            column.Caption = "Projected Loss";
            DataT2.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is Index Position of the quality in the TLADM_Greige Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.DefaultValue = 0.00M;
            column.ColumnName = "Trim_Demand2";
            column.Caption = "Demand";
            DataT2.Columns.Add(column);

            selecta1 = new DataGridViewTextBoxColumn();
            selecta1.Name = "FabricPk2";
            selecta1.Visible = false;
            selecta1.HeaderText = " ";
            selecta1.ValueType = typeof(Int32);
            selecta1.DataPropertyName = DataT2.Columns[0].ColumnName;
            dataGridView2.Columns.Add(selecta1);
            dataGridView2.Columns[0].DisplayIndex = 0;
                        
            selectb1 = new DataGridViewTextBoxColumn();
            selectb1.Name = "TrimsDescription2";
            selectb1.ValueType = typeof(string); 
            selectb1.Visible = true;
            selectb1.Width = 200;
            selectb1.HeaderText = "Trim Quality Description";
            selectb1.DataPropertyName = DataT2.Columns[1].ColumnName;
            dataGridView2.Columns.Add(selectb1);
            dataGridView2.Columns[1].DisplayIndex = 1;

            selectc1 = new DataGridViewTextBoxColumn();
            selectc1.Name = "RequirementMeters";
            selectc1.HeaderText = "Requirement(Meters)";
            selectc1.Visible = true;
            selectc1.ValueType = typeof(decimal);
            selectc1.DataPropertyName = DataT2.Columns[2].ColumnName;
            dataGridView2.Columns.Add(selectc1);
            dataGridView2.Columns[2].DisplayIndex = 2;

            selectd1 = new DataGridViewTextBoxColumn();
            selectd1.Name = "RequirementsKgs";
            selectd1.HeaderText = "Requirement (Kgs)";
            selectd1.Visible = true;
            selectd1.ValueType = typeof(decimal);
            selectd1.DataPropertyName = DataT2.Columns[3].ColumnName;
            dataGridView2.Columns.Add(selectd1);
            dataGridView2.Columns[3].DisplayIndex = 3;

            selecte1 = new DataGridViewTextBoxColumn();
            selecte1.Name = "TrimsPLoss";
            selecte1.HeaderText = "Proj Loss";
            selecte1.Visible = true;
            selecte1.ValueType = typeof(decimal);
            selecte1.DataPropertyName = DataT2.Columns[4].ColumnName;
            dataGridView2.Columns.Add(selecte1);
            dataGridView2.Columns[4].DisplayIndex = 4;

            selectf1 = new DataGridViewTextBoxColumn();
            selectf1.ReadOnly = true;
            selectf1.Visible = true;
            selecte1.Name = "TrimsDemand";
            selectf1.ValueType = typeof(decimal);
            selectf1.DataPropertyName = DataT2.Columns[5].ColumnName;
            selectf1.HeaderText = "Demand";
            dataGridView2.Columns.Add(selectf1);
            dataGridView2.Columns[5].DisplayIndex = 5;

            BindingSrc2.DataSource = DataT2;
            dataGridView2.DataSource = BindingSrc2;
        }

        private void frmDyeOrders_Load(object sender, EventArgs e)
        {
            formloaded = false;

            DataT.Rows.Clear();
            DataT2.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var LastNum = context.TLADM_LastNumberUsed.Find(3);
                if (LastNum != null)
                {
                    txtDyeOrder.Text = "DOF" + LastNum.col2.ToString().PadLeft(5, '0');

                }

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    var Existing = context.TLADM_ProductionLoss.ToList();
                    if (Existing != null)
                    {
                        foreach (var row in Existing)
                        {
                            if (row.TLProdLoss_Dept_Fk == 12)
                                txtDyeProductionLoss.Text = row.TLProdLoss_Percent.ToString();

                        }

                    }
                }

                               
                cmboCustomerNo.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomerNo.ValueMember = "Cust_Pk";
                cmboCustomerNo.DisplayMember = "Cust_Description";
                cmboCustomerNo.SelectedValue = 0;

                var result = context.TLDYE_DyeOrderFabric.GroupBy(x => x.TLDYEF_DyeOrderNumeric).Select(x => x.FirstOrDefault()).ToList();
                cmboDyeOrders.DataSource = result;
                cmboDyeOrders.DisplayMember = "TLDYEF_DyeOrderNo";
                cmboDyeOrders.ValueMember = "TLDYEF_Pk";
                cmboDyeOrders.SelectedValue = 0;

                //  txtCustomerOrder.Text = string.Empty;
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }

            formloaded = true;
        }
        
        private void cmboCustomerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var SelectedCustomer = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if(!SelectedCustomer.Cust_FabricCustomer)
                {
                    MessageBox.Show("Customer selected is not a fabric sales customer");
                    frmDyeOrders_Load(this, null);
                    return;
                }

                cmboFabricOrder.DataSource = null;
                using (var context = new TTI2Entities())
                {
                    var ExistingOrders = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == SelectedCustomer.Cust_Pk && x.TLCSVPO_FabricCustomer && !x.TLCSVPO_Closeed).ToList();
                    if(ExistingOrders.Count()== 0)
                    {
                        MessageBox.Show("There are no orders for the customer selected");
                        frmDyeOrders_Load(this, null);
                        return;
                    }
                    
                    formloaded = false;
                    cmboFabricOrder.DataSource = ExistingOrders;
                    cmboFabricOrder.DisplayMember = "TLCSVPO_PurchaseOrder";
                    cmboFabricOrder.ValueMember = "TLCSVPO_Pk";
                    cmboFabricOrder.SelectedValue = -1;
                    formloaded = true;

                }

                CustomerSelected = (TLADM_CustomerFile)oCmbo.SelectedItem;

                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmboFabric_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                /*
                var selected = (TLADM_Griege)cmboFabric.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells[1].Value = 0.00M;
                        row.Cells[2].Value = 0.00M;
                        row.Cells[3].Value = 0.00M;
                    }

                    using (var context = new TTI2Entities())
                    {
                        
                        var FabWeight = context.TLADM_FabricWeight.Find(selected.TLGreige_FabricWeight_FK);
                        var FabWidth = context.TLADM_FabWidth.Find(selected.TLGreige_FabricWidth_FK);
                        
                        FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                        if (FabricYield != 0)
                            txtYieldFactor.Text = Math.Round(FabricYield, 4).ToString();

                        var FabQual = context.TLADM_GreigeQuality.Where(x => x.GQ_Pk == selected.TLGreige_Quality_FK).FirstOrDefault();
                        if (FabQual != null)
                        {
                            var ExistingData = context.TLDYE_RecipeDefinition.ToList();
                            if (ExistingData != null)
                            {
                                IList<TLADM_Colours> Cols = new List<TLADM_Colours>();

                                foreach (var row in ExistingData)
                                {
                                   TLADM_Colours cl = context.TLADM_Colours.Find(row.TLDYE_ColorChart_FK);

                                    Cols.Add(cl);
                                }

                               
                            }
                        }
                    }
                }
                */

            }
        }

        private void txtWFDye_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxtB = sender as TextBox;
            if (oTxtB != null && formloaded)
            {
                if (!String.IsNullOrEmpty(oTxtB.Text) && oTxtB.Name == "txtWFDye")
                {
                    var weekno = Convert.ToInt32(oTxtB.Text);
                    var thisWeekNo = core.GetIso8601WeekOfYear(dtpDyeOrder.Value);

                    if (oTxtB.Text.Length > 0)
                    {
                        var TxtVal = Convert.ToInt32(oTxtB.Text);

                        if (TxtVal < 1 || TxtVal > 52)
                        {
                            MessageBox.Show("Please enter a value between 1 and 52");
                            oTxtB.Text = "0";
                            oTxtB.Focus();
                            return;
                        }

                        int Yr = dtpDyeOrder.Value.Year;
                        string ThisYear = "11/01/" + Yr.ToString().Trim();
                        DateTime dt1 = DateTime.Parse(ThisYear);
                        DateTime dt2 = DateTime.Today;
                        if (dt2 < dt1)
                        {
                           
                        }
                        else
                        {
                            
                        }
                    }

                    var result = (from u in MandatoryFields
                                  where u[0] == oTxtB.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());

                        if (oTxtB.TextLength > 0)
                            MandSelected[nbr] = true;
                        else
                        {
                            MandSelected[nbr] = false;
                        }
                    }
                }
                else
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oTxtB.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());

                        if (oTxtB.TextLength > 0)
                            MandSelected[nbr] = true;
                        else
                        {
                            MandSelected[nbr] = false;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add = true;
            if (oBtn != null && formloaded)
            {
                var ErrM = core.returnMessage(MandSelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrM))
                {
                    MessageBox.Show(ErrM);
                    return;
                }
                


                using (var context = new TTI2Entities())
                {
                    var LastNum = context.TLADM_LastNumberUsed.Find(3);
                    if (LastNum != null)
                    {
                        LastNum.col2 += 1;
                    }

                    TLDYE_DyeOrderFabric dof = new TLDYE_DyeOrderFabric();

                    var selected = (TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem;

                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        var FabricPk = Row.Field<int>(0);

                        dof = context.TLDYE_DyeOrderFabric
                             .Where(x => x.TLDYEF_FabricOrder_FK == FabricPk).FirstOrDefault();
                        if (dof == null)
                        {
                            Add = true;
                            dof = new TLDYE_DyeOrderFabric();
                            dof.TLDYEF_FabricOrder_FK = Row.Field<int>(0);
                        }

                        var FabricOrder = context.TLCSV_PuchaseOrderDetail.Find(Row.Field<int>(0));
                        if (FabricOrder != null)
                        {
                            dof.TLDYEF_Colours_FK = FabricOrder.TLCUSTO_Colour_FK;
                            dof.TLDYEF_Customer_FK = FabricOrder.TLCUSTO_Customer_FK;
                            dof.TLDYEF_CustomerOrder = (from T1 in context.TLCSV_PurchaseOrder
                                                        join T2 in context.TLCSV_PuchaseOrderDetail
                                                        on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                                        where T2.TLCUSTO_Pk == FabricPk
                                                        select T1).FirstOrDefault().TLCSVPO_PurchaseOrder;
                            dof.TLDYEF_DyeOrderNo = txtDyeOrder.Text;
                            dof.TLDYEF_DyeWeek = Convert.ToInt32(txtWFDye.Text);
                            dof.TLDYEF_Greige_FK = (int)FabricOrder.TLCUSTO_Quality_FK;
                            dof.TLDYEF_OrderDate = dtpDyeOrder.Value;
                            dof.TLDYEF_PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);
                            dof.TLDYEF_Yield = 0.00M; //Convert.ToDecimal(txtYieldFactor.Text);

                            dof.TLDYEF_Quantity = Row.Field<decimal>(3);
                            dof.TLDYEF_ProjectedLoss = Row.Field<decimal>(4);
                            dof.TLDYEF_Demand = Row.Field<decimal>(5);
                            dof.TLDYEF_DyeOrderNumeric = -1 + LastNum.col2;
                            dof.TLDYEF_Body = true;

                        }
                        if (Add)
                            context.TLDYE_DyeOrderFabric.Add(dof);
                    }

                    foreach (DataRow Row in DataT2.Rows)
                    {
                        Add = false;
                        var FabricPk = Row.Field<int>(0);
                        dof = context.TLDYE_DyeOrderFabric
                             .Where(x => x.TLDYEF_FabricOrder_FK == FabricPk).FirstOrDefault();

                        if (dof == null)
                        {
                            Add = true;
                            dof = new TLDYE_DyeOrderFabric();
                            dof.TLDYEF_FabricOrder_FK = Row.Field<int>(0);
                        }

                        var FabricOrder = context.TLCSV_PuchaseOrderDetail.Find(Row.Field<int>(0));
                        if (FabricOrder != null)
                        {
                            dof.TLDYEF_Colours_FK = FabricOrder.TLCUSTO_Colour_FK;
                            dof.TLDYEF_Customer_FK = FabricOrder.TLCUSTO_Customer_FK;
                            dof.TLDYEF_CustomerOrder = (from T1 in context.TLCSV_PurchaseOrder
                                                        join T2 in context.TLCSV_PuchaseOrderDetail
                                                        on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                                                        where T2.TLCUSTO_Pk == FabricPk
                                                        select T1).FirstOrDefault().TLCSVPO_PurchaseOrder;
                            dof.TLDYEF_DyeOrderNo = txtDyeOrder.Text;
                            dof.TLDYEF_DyeWeek = Convert.ToInt32(txtWFDye.Text);
                            dof.TLDYEF_Greige_FK = (int)FabricOrder.TLCUSTO_Quality_FK;
                            dof.TLDYEF_OrderDate = dtpDyeOrder.Value;
                            dof.TLDYEF_PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);
                            dof.TLDYEF_Yield = 0.00M; //Convert.ToDecimal(txtYieldFactor.Text);

                            dof.TLDYEF_Quantity = Row.Field<decimal>(3);
                            dof.TLDYEF_ProjectedLoss = Row.Field<decimal>(4);
                            dof.TLDYEF_Demand = Row.Field<decimal>(5);
                            dof.TLDYEF_DyeOrderNumeric = -1 + LastNum.col2;
                            dof.TLDYEF_Body = false;
                        }
                        if (Add)
                            context.TLDYE_DyeOrderFabric.Add(dof);
                    }

                    try
                    {
                        context.SaveChanges();
                        frmDyeOrders_Load(this, null);

                        MessageBox.Show("Data saved successfully to database");
                        frmDyeViewReport vRep = new frmDyeViewReport(2, dof.TLDYEF_DyeOrderNumeric);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                        MessageBox.Show(exceptionMessages.ToString());
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell &&
                 oDgv.CurrentCell.ColumnIndex == 1)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }

        private void dtpDyeOrder_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
               
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused && e.ColumnIndex == 1)
            {
                if (FabricYield == 0)
                {
                    oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    MessageBox.Show("Please select a fabric type");
                }
                else
                    oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                if (e.RowIndex == 0)
                {
                  
                    var Cell = oDgv.CurrentCell;
                    var Qty = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());
                    var PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);

                    var Vol = Qty / FabricYield;
                    var Qty2 = core.ProdLoss(Vol, PLoss);

                    oDgv.Rows[e.RowIndex].Cells[2].Value = Math.Round(Qty2 - Vol, 4);
                    oDgv.Rows[e.RowIndex].Cells[3].Value = Math.Round(Qty2, 4);
                }
                else
                {
                    var Cell = oDgv.CurrentCell;
                    var Qty = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());
                    var PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);

                    var Qty2 = core.ProdLoss(Qty, PLoss);
                    oDgv.Rows[e.RowIndex].Cells[2].Value = Math.Round(Qty2 - Qty, 4);
                    oDgv.Rows[e.RowIndex].Cells[3].Value = Math.Round(Qty2 * FabricYield, 4);
                }
            }
            
        }

        private void txtWFDye_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;

            var result = (from u in MandatoryFields
                          where u[0] == oTxtBx.Name
                          select u).FirstOrDefault();

            int nbr = Convert.ToInt32(result[2].ToString());
            if (!MandSelected[nbr])
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = true;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = false;
                }
            }
        }

        private void cmboDyeOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                DataT.Rows.Clear();
                DataT2.Rows.Clear();

                var Result = (TLDYE_DyeOrderFabric)oCmbo.SelectedItem;
                if(Result != null)
                {
                    formloaded = false;
                    using (var context = new TTI2Entities())
                    {
                        cmboCustomerNo.SelectedValue = Result.TLDYEF_Customer_FK;
                        cmboFabricOrder.DataSource = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == Result.TLDYEF_Customer_FK).ToList();
                        cmboFabricOrder.DisplayMember = "TLCSVPO_PurchaseOrder";
                        cmboFabricOrder.ValueMember = "TLCSVPO_Pk";
                        cmboFabricOrder.SelectedValue = Result.TLDYEF_FabricOrder_FK;
                        formloaded = true;

                    
                        var DofDetails = context.TLDYE_DyeOrderFabric.Where(x => x.TLDYEF_DyeOrderNumeric == Result.TLDYEF_DyeOrderNumeric).ToList();
                        foreach (var DofDetail in DofDetails)
                        {
                            var OrderDetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_Pk == DofDetail.TLDYEF_FabricOrder_FK).ToList();
                            foreach (var OrderDetail in OrderDetails)
                            {
                                var Quality = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK);
                                if (Quality != null && Quality.TLGreige_Body)
                                {
                                    var FabWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                    var FabWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                    FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                                    DataRow Row = DataT.NewRow();
                                    Row[0] = OrderDetail.TLCUSTO_Pk;
                                    Row[1] = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK).TLGreige_Description;
                                    Row[2] = Math.Round(OrderDetail.TLCUSTO_QtyMeters, 2);
                                    Row[3] = Math.Round(OrderDetail.TLCUSTO_QtyMeters / FabricYield, 2);
                                    Row[5] = Math.Round((100 - Result.TLDYEF_PLoss) / 100 * Row.Field<decimal>(3), 2);
                                    Row[4] = Math.Round(Row.Field<decimal>(3) - Row.Field<decimal>(5), 2);
                                    DataT.Rows.Add(Row);

                                }
                                else
                                {
                                    var FabWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                    var FabWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                    FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                                    DataRow Row = DataT2.NewRow();
                                    Row[0] = OrderDetail.TLCUSTO_Pk;
                                    Row[1] = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK).TLGreige_Description;
                                    Row[2] = Math.Round(OrderDetail.TLCUSTO_QtyMeters, 2);
                                    Row[3] = Math.Round(OrderDetail.TLCUSTO_QtyMeters / FabricYield, 2);
                                    Row[5] = Math.Round((100 - Result.TLDYEF_PLoss) / 100 * Row.Field<decimal>(3), 2);
                                    Row[4] = Math.Round(Row.Field<decimal>(3) - Row.Field<decimal>(5), 2);
                                    DataT2.Rows.Add(Row);

                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmboColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmboFabricOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded)
            {
                DataT.Rows.Clear();
                DataT2.Rows.Clear();

                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

                var PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);

                using (var context = new TTI2Entities())
                {
                    var OrderSelected = (TLCSV_PurchaseOrder)oCmbo.SelectedItem;

                    if (OrderSelected != null)
                    {
                        var OrderDetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == OrderSelected.TLCSVPO_Pk && !OrderSelected.TLCSVPO_Closeed).ToList();
                        foreach (var OrderDetail in OrderDetails)
                        {
                            var Quality = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK);
                            if (Quality != null && Quality.TLGreige_Body)
                            {
                                var FabWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                var FabWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                                DataRow Row = DataT.NewRow();
                                Row[0] = OrderDetail.TLCUSTO_Pk;
                                Row[1] = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK).TLGreige_Description;
                                Row[2] = Math.Round(OrderDetail.TLCUSTO_QtyMeters, 2);
                                Row[3] = Math.Round(OrderDetail.TLCUSTO_QtyMeters / FabricYield, 2);
                                Row[5] = Math.Round((100 - PLoss) / 100 * Row.Field<decimal>(3), 2);
                                Row[4] = Math.Round(Row.Field<decimal>(3) - Row.Field<decimal>(5), 2);
                                DataT.Rows.Add(Row);

                            }
                            else
                            {
                                var FabWeight = context.TLADM_FabricWeight.Find(Quality.TLGreige_FabricWeight_FK);
                                var FabWidth = context.TLADM_FabWidth.Find(Quality.TLGreige_FabricWidth_FK);
                                FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);

                                DataRow Row = DataT2.NewRow();
                                Row[0] = OrderDetail.TLCUSTO_Pk;
                                Row[1] = context.TLADM_Griege.Find(OrderDetail.TLCUSTO_Quality_FK).TLGreige_Description;
                                Row[2] = Math.Round(OrderDetail.TLCUSTO_QtyMeters, 2);
                                Row[3] = Math.Round(OrderDetail.TLCUSTO_QtyMeters / FabricYield, 2);
                                Row[5] = Math.Round((100 - PLoss) / 100 * Row.Field<decimal>(3), 2);
                                Row[4] = Math.Round(Row.Field<decimal>(3) - Row.Field<decimal>(5), 2);
                                DataT2.Rows.Add(Row);

                            }
                        }
                    }
                
                }
            }
        }
    }
}
