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

        DataGridViewTextBoxColumn selecta;  // Description 
        DataGridViewTextBoxColumn selectb;  // requirements
        DataGridViewTextBoxColumn selectc;  // Projected Loss
        DataGridViewTextBoxColumn selectd;  // Volume Need
        DataGridViewTextBoxColumn selecte;  // Alternate description

        public frmDyeOrders()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
            {   new string[] {"cmboCustomerNo", "Please select a customer number", "0"},
                new string[] {"dtpDyeOrder", "Please enter the date of order", "1"},
                new string[] {"txtWFDye", "Please enter the week no required", "2"},
                new string[] {"txtCustomerOrder", "Please enter the customer order number", "3"},
                new string[] {"cmboFabric", "Please select a fabric", "4"},
                new string[] {"cmboColors", "Please select a colour", "5"}
            };

            selecta = new DataGridViewTextBoxColumn();
            selecta.HeaderText = "Description";
            selecta.ReadOnly = true;
            selecta.ValueType = typeof(string);

            selectb = new DataGridViewTextBoxColumn();
            selectb.HeaderText = "Requirements";
            selectb.ValueType = typeof(decimal);

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Projected Loss (Kgs)";
            selectc.ReadOnly = true;
            selectb.ValueType = typeof(decimal);

            selectd = new DataGridViewTextBoxColumn();
            selectd.HeaderText = "Demand";
            selectd.ReadOnly = true;
            selectb.ValueType = typeof(decimal);

            selecte = new DataGridViewTextBoxColumn();
            // selecte.HeaderText = "Alternate";
            selecte.ReadOnly = true;

            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns.Add(selecte);

            dataGridView1.AllowUserToAddRows = false;

            SetUp();
        }

        void SetUp()
        {
            formloaded = false;

            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var LastNum = context.TLADM_LastNumberUsed.Find(3);
                if (LastNum != null)
                {
                    txtDyeOrder.Text  = "DOF" + LastNum.col2.ToString().PadLeft(5, '0');
                   
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

                cmboFabric.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboFabric.ValueMember = "TLGreige_Id";
                cmboFabric.DisplayMember = "TLGreige_Description";
                cmboFabric.SelectedValue = 0;

                cmboColors.ValueMember = "Col_Id";
                cmboColors.DisplayMember = "Col_Description";
                cmboColors.SelectedValue = 0;

                cmboDyeOrders.DataSource = context.TLDYE_DyeOrderFabric.OrderBy(x => x.TLDYEF_DyeOrderNo).ToList();
                cmboDyeOrders.DisplayMember = "TLDYEF_DyeOrderNo";
                cmboDyeOrders.ValueMember = "TLDYEF_Pk";
                cmboDyeOrders.SelectedValue = 0;

                txtCustomerOrder.Text = string.Empty;
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }

            var i = 0;
            do
            {
                var index = dataGridView1.Rows.Add();
                if (i == 0)
                {
                    dataGridView1.Rows[index].Cells[0].Value = "Meters";
                    dataGridView1.Rows[index].Cells[1].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[2].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[4].Value = "Kilograms";
                }
                else
                {
                    dataGridView1.Rows[index].Cells[0].Value = "Kilograms";
                    dataGridView1.Rows[index].Cells[1].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[2].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                    dataGridView1.Rows[index].Cells[4].Value = "Meters";
                }

            } while (i++ < 1);
            formloaded = true;

        }
        
        private void cmboCustomerNo_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cmboFabric_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
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

                                cmboColors.DataSource = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                                cmboColors.ValueMember = "Col_Id";
                                cmboColors.DisplayMember = "Col_Display";
                            }
                        }
                    }
                }
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
                /*
                var ErrM = core.returnMessage(MandSelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrM))
                {
                    MessageBox.Show(ErrM);
                    return;
                }
                 */ 
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

                if (selected != null)
                {
                    dof = context.TLDYE_DyeOrderFabric.Find(selected.TLDYEF_Pk);
                    Add = false;
                }

                dof.TLDYEF_Colours_FK = (int)cmboColors.SelectedValue;
                dof.TLDYEF_Customer_FK = (int)cmboCustomerNo.SelectedValue;
                dof.TLDYEF_CustomerOrder = txtCustomerOrder.Text;
                dof.TLDYEF_DyeOrderNo = txtDyeOrder.Text;
                dof.TLDYEF_DyeWeek = Convert.ToInt32(txtWFDye.Text);
                dof.TLDYEF_Greige_FK = (int)cmboFabric.SelectedValue;
                dof.TLDYEF_OrderDate = dtpDyeOrder.Value;
                dof.TLDYEF_PLoss = Convert.ToDecimal(txtDyeProductionLoss.Text);
                dof.TLDYEF_Yield = Convert.ToDecimal(txtYieldFactor.Text);
              
                DataGridViewRow dr = new DataGridViewRow();
                dr = dataGridView1.Rows[0];

                dof.TLDYEF_Quantity = Convert.ToDecimal(dr.Cells[1].Value.ToString());
                dof.TLDYEF_ProjectedLoss = Convert.ToDecimal(dr.Cells[2].Value.ToString());
                dof.TLDYEF_Demand = Convert.ToDecimal(dr.Cells[3].Value.ToString());

                if(Add)
                    context.TLDYE_DyeOrderFabric.Add(dof);
                try
                {
                    context.SaveChanges();
                    SetUp();
                    MessageBox.Show("Data saved successfully to database");
                    frmDyeViewReport vRep = new frmDyeViewReport(2, dof.TLDYEF_Pk);
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
                var selected = (TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem;
                if (selected != null)
                {
                    dtpDyeOrder.Value = selected.TLDYEF_OrderDate;
                    txtCustomerOrder.Text = selected.TLDYEF_CustomerOrder;
                    cmboCustomerNo.SelectedValue = selected.TLDYEF_Customer_FK;
                    
                    txtCustomerOrder.Text = selected.TLDYEF_CustomerOrder;
                    txtWFDye.Text = selected.TLDYEF_DyeWeek.ToString();
                    txtDyeOrder.Text = selected.TLDYEF_DyeOrderNo;
                    txtDyeProductionLoss.Text = selected.TLDYEF_PLoss.ToString();
                    txtYieldFactor.Text = selected.TLDYEF_Yield.ToString();
                    txtYieldFactor.Text = selected.TLDYEF_Yield.ToString();
                    cmboFabric.SelectedValue = selected.TLDYEF_Greige_FK;
                    cmboColors.SelectedValue = selected.TLDYEF_Colours_FK;

                    formloaded = false;
                    dataGridView1.Rows[0].Cells[1].Value = Convert.ToDecimal(selected.TLDYEF_Quantity.ToString());
                    dataGridView1.Rows[0].Cells[2].Value = Convert.ToDecimal(selected.TLDYEF_ProjectedLoss.ToString());
                    dataGridView1.Rows[0].Cells[3].Value = Convert.ToDecimal(selected.TLDYEF_Demand.ToString());
                                    
                    dataGridView1.Rows[1].Cells[1].Value = 0.00M;
                    dataGridView1.Rows[1].Cells[2].Value = 0.00M;
                    dataGridView1.Rows[1].Cells[3].Value = 0.00M;
                    formloaded = true;
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

    
    }
}
