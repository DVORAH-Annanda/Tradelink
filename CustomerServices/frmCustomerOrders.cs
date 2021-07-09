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
using Microsoft.Office.Interop.Outlook;
using EntityFramework.Extensions;

namespace CustomerServices
{
    public partial class frmCustomerOrders : Form
    {
        bool formloaded;
        DataGridViewTextBoxColumn oTxtA;   //  0 datagridView1 Current Pk
        DataGridViewTextBoxColumn oTxtB;   //  1 Line Numbers
        DataGridViewComboBoxColumn oCmbA;  //  2 datagridView1 Styles
        DataGridViewComboBoxColumn oCmbB;  //  3 datagridView1 Colours
        DataGridViewComboBoxColumn oCmbC;  //  4 datagridView1 Sizes
        DataGridViewTextBoxColumn oTxtC;   //  5 datagridView1 Order Qty
        DataGridViewTextBoxColumn oTxtD;   //  6 Grade
        DataGridViewCheckBoxColumn oChkA;  //  7 datagridView1 Check Qty on Hand at Whses / CMT 
        DataGridViewCheckBoxColumn oChkB;  //  8 datagridView1 Check status of previous orders
        DataGridViewTextBoxColumn oTxtE;   //  9 The Key Pertaining to the repack center 
        DataGridViewTextBoxColumn oTxtF;   //  10 The Date Required for a particlar line 

        DataGridViewTextBoxColumn oTxtAA;   // 0 datagridview2 warehouse description 
        DataGridViewTextBoxColumn oTxtAB;   // 1 datagridview2 Total SOH for selection 
        DataGridViewTextBoxColumn oTxtAC;   // 2 datagridview2 Total Picked for selection
        DataGridViewTextBoxColumn oTxtAD;   // 3 datagridview2 Total Balance for selection

        DateTimePicker dtp;

        bool Mode;
        string[][] MandatoryFields;
        bool[] MandSelected;
        Util core;

        bool EditMode;
        bool AddnAdd;
        bool FabricMode; 

        int ActiveRow;
        bool RowLeave;

        StringBuilder sb = null;
        DataTable dt;

        protected readonly TTI2Entities _context;
        //==========================================
        // Note for the file 
        //============================================================
        // We have two scenerios 
        //   1 - Normal Customers - ie where the boxes are delivered as is
        //   2 - Repack Customers - Who have selected boxes repacked into a configuration
        //                         provided by themselves 
        //      However the situation has arose whereby Repack Customers might also need to be Normal Customers
        //
        //==============================================================
        Boolean RepackTransaction;

        public frmCustomerOrders()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            FabricMode = false;

        }

        private void frmCustomerOrders_Load(object sender, EventArgs e)
        {
            formloaded = false;
            RepackTransaction = false;

            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                if (Dept != null)
                {
                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    if (LNU != null)
                        txtTransNumber.Text = "CO" + LNU.col2.ToString().PadLeft(5, '0');

                }

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.ReadOnly = true;
                oTxtA.ValueType = typeof(int);
                oTxtA.Visible = false;
                oTxtA.HeaderText = "Pk";
                dataGridView1.Columns.Add(oTxtA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.Visible = true;
                oTxtB.ReadOnly = true;
                oTxtB.HeaderText = "Line Number";
                oTxtB.Width = 100;
                oTxtB.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtB);

                oCmbA = new DataGridViewComboBoxColumn();
                oCmbA.HeaderText = "Styles";
                oCmbA.ValueMember = "Sty_Id";
                oCmbA.DisplayMember = "Sty_Description";
                dataGridView1.Columns.Add(oCmbA);

                oCmbB = new DataGridViewComboBoxColumn();
                oCmbB.HeaderText = "Colours";
                oCmbB.ValueMember = "Col_Id";
                oCmbB.DisplayMember = "Col_Display";
                dataGridView1.Columns.Add(oCmbB);

                oCmbC = new DataGridViewComboBoxColumn();
                oCmbC.HeaderText = "Sizes";
                oCmbC.ValueMember = "SI_id";
                oCmbC.DisplayMember = "SI_Description";
                dataGridView1.Columns.Add(oCmbC);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.Visible = true;
                oTxtC.HeaderText = "Qty";
                oTxtC.Width = 100;
                oTxtC.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtC);

                oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.Visible = true;
                oTxtD.HeaderText = "Grade";
                oTxtD.Width = 100;
                oTxtD.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtD);

                oTxtF = new DataGridViewTextBoxColumn();
                oTxtF.Visible = true;
                oTxtF.HeaderText = "Date Required";
                oTxtF.Width = 100;
                oTxtF.ValueType = typeof(DateTime);
                oTxtF.DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns.Add(oTxtF);
                                
                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.ValueType = typeof(bool);
                oChkA.HeaderText = "Stock Available";
                dataGridView1.Columns.Add(oChkA);

                oChkB = new DataGridViewCheckBoxColumn();
                oChkB.ValueType = typeof(bool);
                oChkB.HeaderText = "Order Status";
                dataGridView1.Columns.Add(oChkB);

                oTxtE = new DataGridViewTextBoxColumn();
                oTxtE.Visible = false;
                oTxtE.HeaderText = "RePack Center Key";
                oTxtE.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtE);
                
                dataGridView1.AutoGenerateColumns = false;

                oTxtAA = new DataGridViewTextBoxColumn();
                oTxtAA.ReadOnly = true;
                oTxtAA.ValueType = typeof(String);
                oTxtAA.Width = 200;
                oTxtAA.HeaderText = "Warehouse Description";
                dataGridView2.Columns.Add(oTxtAA);

                oTxtAB = new DataGridViewTextBoxColumn();
                oTxtAB.ReadOnly = true;
                oTxtAB.ValueType = typeof(int);
                oTxtAB.HeaderText = "Current Available";
                dataGridView2.Columns.Add(oTxtAB);

                oTxtAC = new DataGridViewTextBoxColumn();
                oTxtAC.ReadOnly = true;
                oTxtAC.ValueType = typeof(int);
                oTxtAC.HeaderText = "Current Picked";
                dataGridView2.Columns.Add(oTxtAC);

                oTxtAD = new DataGridViewTextBoxColumn();
                oTxtAD.ReadOnly = true;
                oTxtAD.ValueType = typeof(int);
                oTxtAD.HeaderText = "Balance";
                dataGridView2.Columns.Add(oTxtAD);

                dataGridView2.Visible = false;
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.AllowUserToAddRows = false;

                core = new Util();
                MandatoryFields = new string[][]
                {   new string[] {"2", "Please select a style", "0"},
                    new string[] {"3", "Please select a Colour", "1"},
                    new string[] {"4", "Please select a Size", "2"}, 
                    new string[] {"5", "Please enter the required qty", "3"} 
                    
                };

                AddnAdd = false;

                dtpCustOrderDate.Value = DateTime.Now;
                dtpRequiredDate.Value = DateTime.Now.AddDays(30);
                
                if (dtpRequiredDate.Value.DayOfWeek == DayOfWeek.Saturday)
                    dtpRequiredDate.Value.AddDays(2);
                else if(dtpRequiredDate.Value.DayOfWeek == DayOfWeek.Sunday)
                    dtpRequiredDate.Value.AddDays(1);

                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }
            
            Mode = true;
           
            RowLeave = false;

            cmboCustomers.Focus();
            EditMode = false;
            AddnAdd = false;
            rbOrderActive.Checked = true;
            rbSpecialNo.Checked = true;
            formloaded = true;
           
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
                   
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                formloaded = false;
                cmboCurrentOrders.SelectedIndex = -1;
                formloaded = true;

                if(!cmboCurrentOrders.Enabled)
                {
                    cmboCurrentOrders.Enabled = true;
                }

                var selected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                if (selected != null)
                {
                    if (!selected.Cust_FabricCustomer)
                    {
                        oCmbA.DataSource = null;
                        oCmbA.DataSource = _context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                        oCmbA.HeaderText = "Styles";
                        oCmbA.ValueMember = "Sty_Id";
                        oCmbA.DisplayMember = "Sty_Description";

                      
                        oCmbB.DataSource = null;
                        oCmbB.DataSource = _context.TLADM_Colours.Where(x => (bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                       
                        DataGridViewColumn col = dataGridView1.Columns[4];
                        col.Visible = true;

                        oTxtC.HeaderText = "Qty";
                    }
                    else
                    {
                        oCmbA.DataSource = null;
                        oCmbA.DataSource = _context.TLADM_Griege.Where(x => (bool)!x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                        oCmbA.HeaderText = "Qualities";
                        oCmbA.ValueMember = "TLGreige_Id";
                        oCmbA.DisplayMember = "TLGreige_Description";
                      
                        oCmbB.DataSource = null;
                        oCmbB.DataSource = _context.TLADM_Colours.Where(x => (bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                        FabricMode = true;

                        DataGridViewColumn col = dataGridView1.Columns[4];
                        col.Visible = false;
                        oTxtC.HeaderText = "Meters";
                        
                    }
                    formloaded = false;
                          
                    cmboCurrentOrders.DataSource = _context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == selected.Cust_Pk && !x.TLCSVPO_Closeed).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                    cmboCurrentOrders.ValueMember = "TLCSVPO_Pk";
                    cmboCurrentOrders.DisplayMember = "TLCSVPO_PurchaseOrder";
                    cmboCurrentOrders.SelectedValue = -1;
                        
                       
                    formloaded = true;
                }
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            Repository repo = new Repository();

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var CurrentRow = oDgv.CurrentRow;
                var CurrentCell = oDgv.CurrentCell;

                RowLeave = false;

                var parms = new CustomerServicesParameters();
                dataGridView2.Rows.Clear();

                if (CurrentCell.ColumnIndex == 8)
                {
                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        if (CurrentRow.Cells[2].Value != null)
                        {
                            parms.Styles.Add(repo.LoadStyle((int)CurrentRow.Cells[2].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a style");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        if (CurrentRow.Cells[3].Value != null)
                        {
                            parms.Colours.Add(repo.LoadColour((int)CurrentRow.Cells[3].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a colour");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        if (CurrentRow.Cells[4].Value != null)
                        {
                            parms.Sizes.Add(repo.LoadSize((int)CurrentRow.Cells[4].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a size");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        var soh = repo.SOHQuery(parms);
                        if (soh.Count() == 0)
                        {
                            MessageBox.Show("There are no records pertaining to selection made");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        dataGridView1.Visible = false;
                        Mode = !Mode;
                        btnSave.Text = "Close";
                        cmboCustomers.Enabled = false;
                        btnAdd.Visible = false;
                        dataGridView2.Visible = true;

                        using (var context = new TTI2Entities())
                        {
                            var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                            foreach (var Whse in Whses)
                            {
                                var index = dataGridView2.Rows.Add();
                                dataGridView2.Rows[index].Cells[0].Value = Whse.WhStore_Description;
                                int TotalSOH = soh.Where(x => x.TLSOH_WareHouse_FK == Whse.WhStore_Id).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                int TotalPicked = soh.Where(x => x.TLSOH_WareHouse_FK == Whse.WhStore_Id && x.TLSOH_Picked).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                dataGridView2.Rows[index].Cells[1].Value = TotalSOH;
                                dataGridView2.Rows[index].Cells[2].Value = TotalPicked;
                                dataGridView2.Rows[index].Cells[3].Value = TotalSOH - TotalPicked;
                            }
                        }
                    }
                }
                else if (CurrentCell.ColumnIndex == 9)
                {
                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        var CustSelected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                        if (CustSelected == null)
                        {
                            MessageBox.Show("Please select a customer");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        if (CurrentRow.Cells[2].Value != null)
                        {
                            parms.Styles.Add(repo.LoadStyle((int)CurrentRow.Cells[2].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a style");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value = false;
                            return;
                        }

                        if (CurrentRow.Cells[3].Value != null)
                        {
                            parms.Colours.Add(repo.LoadColour((int)CurrentRow.Cells[3].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a colour");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        if (CurrentRow.Cells[4].Value != null)
                        {
                            parms.Sizes.Add(repo.LoadSize((int)CurrentRow.Cells[4].Value));
                        }
                        else
                        {
                            MessageBox.Show("Please select a size");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        var pod = repo.POQuery(parms);
                        if (pod.Count() == 0)
                        {
                            MessageBox.Show("There are no records pertaining to selection made");
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                            return;
                        }

                        oTxtAA.HeaderText = "PONumber";
                        oTxtAB.HeaderText = "Qty On Order";
                        oTxtAC.HeaderText = "Picked";
                        oTxtAD.HeaderText = "Balance to be picked";


                        dataGridView1.Visible = false;
                        Mode = !Mode;
                        btnSave.Text = "Close";
                        cmboCustomers.Enabled = false;
                        using ( var context = new TTI2Entities())
                        {
                            var Groups = pod.GroupBy(x => x.TLCUSTO_PurchaseOrder_FK).ToList();
                            foreach (var Group in Groups)
                            {
                                var index = dataGridView2.Rows.Add();
                                var pk = Group.FirstOrDefault().TLCUSTO_PurchaseOrder_FK;

                                dataGridView2.Rows[index].Cells[0].Value = context.TLCSV_PurchaseOrder.Find(pk).TLCSVPO_PurchaseOrder;
                                int TotalSOH = Group.Sum(x=>x.TLCUSTO_Qty);
                                int TotalPicked = Group.Where(x => x.TLCUSTO_Picked).Sum(x =>x.TLCUSTO_Qty);
                                dataGridView2.Rows[index].Cells[1].Value = TotalSOH;
                                dataGridView2.Rows[index].Cells[2].Value = TotalPicked;
                                dataGridView2.Rows[index].Cells[3].Value = TotalSOH - TotalPicked;
                            }
                        }
                        dataGridView2.Visible = true;
                     }
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (oDgv.Focused && e.ColumnIndex == 7)
            {
                 oDgv.Columns[e.ColumnIndex].DefaultCellStyle.Format = "dd/MM/yyyy";
                 dtp = new DateTimePicker();
                 dataGridView1.Controls.Add(dtp);
                 dtp.Format = DateTimePickerFormat.Short;
                 Rectangle Rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                 dtp.Size = new Size(Rectangle.Width, Rectangle.Height);
                 dtp.Location = new Point(Rectangle.X, Rectangle.Y);

                 dtp.CloseUp += new EventHandler(dtp_CloseUp);
                 dtp.TextChanged += new EventHandler(dtp_OnTextChange);
                    
                 dtp.Visible = true;
             }
        }

        private void dtp_OnTextChange(object sender, EventArgs e)
        {
            var CurrentRow = dataGridView1.CurrentRow;
            CurrentRow.Cells[7].Value = dtp.Value;
        } 

        void dtp_CloseUp(object sender, EventArgs e)
        { 
             dtp.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int TransNumber = 0;
            sb = new StringBuilder();

            if (oBtn != null && formloaded)
            {
                if (!Mode)
                {
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    btnSave.Text = "Save";
                    btnAdd.Visible = true;
                    cmboCustomers.Enabled = true;
                    Mode = !Mode;
                    RowLeave = true;
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = false;
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value = false;
                    oTxtAA.HeaderText = "Warehouse Description";
                    oTxtAB.HeaderText = "Current SOH";
                    oTxtAC.HeaderText = "Current Picked";
                    oTxtAD.HeaderText = "Balance";
                }
                else
                {
                    var CustSelected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;

                    if (CustSelected == null)
                    {
                        MessageBox.Show("Please select a customer from the drop down box");
                        return;
                    }

                    if (!rbOrderClosed.Checked)
                    {
                        // "Normal" Mode
                        //--------------------------------------------------------------------------
                       
                        if (txtCustomerPO.Text.Length == 0)
                        {
                            MessageBox.Show("Please enter an unique Purchase Order Number for this customer");
                            return;
                        }

                        if (dtpRequiredDate.Value < dtpCustOrderDate.Value)
                        {
                            MessageBox.Show("Delivery date must be at least the same or greater then order date");
                            return;
                        }
                    }
                    using (var context = new TTI2Entities())
                    {
                        if (!EditMode)
                        {
                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                            if (Dept != null)
                            {
                                var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                                if (LNU != null)
                                {
                                    TransNumber = LNU.col2;
                                    LNU.col2 += 1;

                                }
                            }
                        }
                        //----------------------------------------------------
                        // Build Up a header Record 
                        // Must take care of an "edit" scenario as well 
                        //------------------------------------------------------

                        TLCSV_PurchaseOrder PO = new TLCSV_PurchaseOrder();
                        if (EditMode || AddnAdd)
                        {
                            var PoSelected = (TLCSV_PurchaseOrder)cmboCurrentOrders.SelectedItem;
                            if (PoSelected != null)
                            {
                                PO = context.TLCSV_PurchaseOrder.Find(PoSelected.TLCSVPO_Pk);
                                if (PO == null)
                                {
                                    MessageBox.Show("Technical error encountered...Please advise Programmer");
                                    return;
                                }
                            }
                            else
                            {
                                if (PO.TLCSVPO_Pk == 0)
                                {
                                    MessageBox.Show("Technical error encountered...Please advise Programmer..Parent Key value set to zero");
                                    return;
                                }
                            }
                          
                        }
                                    
            
                        if (rbOrderActive.Checked)
                            PO.TLCSVPO_Closeed = false;
                        else
                        {
                            if (rbOrderClosed.Checked)
                            {
                                PO.TLCSVPO_Closeed = true;
                                PO.TLCSVPO_ClosedDate = DateTime.Now;
                            }
                        }

                        if (rbSpecialYes.Checked)
                            PO.TLCSVPO_SpecialOrder = true;
                        else
                            PO.TLCSVPO_SpecialOrder = false;

                        if (!rbCopyOrder.Checked)
                        {
                            if (rbProvisional.Checked)
                            {
                                //ie Might have been a standard order rechecked to provisonal ????? 
                                //========================================================
                                PO.TLCSVPO_Provisional = true;
                                if (String.Compare(PO.TLCSVPO_PurchaseOrder, txtCustomerPO.Text) != 0)
                                {
                                    PO.TLCSVPO_PurchaseOrder = txtCustomerPO.Text;
                                }
                            }
                            else if (PO.TLCSVPO_Provisional)
                            {
                                //ie was a provisionsl order 
                                //========================================================
                                if (String.Compare(PO.TLCSVPO_PurchaseOrder, txtCustomerPO.Text) != 0)
                                {
                                    PO.TLCSVPO_PurchaseOrder = txtCustomerPO.Text;
                                }

                                PO.TLCSVPO_Provisional = false;

                            }
                        }
                        else
                            PO.TLCSVPO_Provisional = false;

                        if (!EditMode && !AddnAdd)
                        {
                            PO.TLCSVPO_RepackTransaction = RepackTransaction;
                            PO.TLCSVPO_RequiredDate = dtpRequiredDate.Value;
                            PO.TLCSVPO_TransDate = dtpCustOrderDate.Value;
                            PO.TLCSVPO_Customer_FK = CustSelected.Cust_Pk;

                            PO.TLCSVPO_PurchaseOrder = txtCustomerPO.Text;
                            PO.TLCVSPO_SequenceNo = TransNumber;

                            context.TLCSV_PurchaseOrder.Add(PO);
                        }                   
                        

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }

                        //------------------------------------------------------
                        // Table for email structure 
                        //----------------------------------------------------------
                        if (!FabricMode)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("Style", typeof(string));
                            dt.Columns.Add("Colour", typeof(string));
                            dt.Columns.Add("Size", typeof(string));
                            dt.Columns.Add("Qty", typeof(decimal));
                        }
                        //----------------------------------------------------------------------------

                        if (rbOrderClosed.Checked)
                        {
                            context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK  == PO.TLCSVPO_Pk).Update(x => new TLCSV_PuchaseOrderDetail() { TLCUSTO_Closed = true });
                        }
                        else
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[1].Value == null)
                                    continue;

                                if (!FabricMode)
                                {
                                    var RWComplete = core.RowComplete(row, MandatoryFields);

                                    var cnt = RWComplete.Where(x => x == false).Count();

                                    if (cnt == MandatoryFields.Length)
                                    {
                                        continue;
                                    }


                                    if (cnt != 0)
                                    {
                                        var errorM = core.returnMessage(RWComplete, true, MandatoryFields);
                                        if (!string.IsNullOrEmpty(errorM))
                                        {
                                            MessageBox.Show(errorM, "Error Message for Line No" + (row.Index + 1).ToString());
                                            return;
                                        }
                                    }
                                }
                                TLCSV_PuchaseOrderDetail PODet = new TLCSV_PuchaseOrderDetail();
                                bool NewRecord = true;
                                if (EditMode)
                                {
                                    if (row.Cells[0].Value != null)
                                    {
                                        var index = (int)row.Cells[0].Value;
                                        PODet = context.TLCSV_PuchaseOrderDetail.Find(index);
                                        if (PODet != null)
                                        {
                                            NewRecord = false;
                                        }
                                        else
                                        {
                                            PODet = new TLCSV_PuchaseOrderDetail();
                                        }

                                    }
                                }
                                else
                                {
                                    if (!rbCopyOrder.Checked)
                                    {
                                        if (row.Cells[0].Value != null)
                                        {
                                            var index = (int)row.Cells[0].Value;
                                            PODet = context.TLCSV_PuchaseOrderDetail.Find(index);
                                            if (PODet != null)
                                            {
                                                NewRecord = false;
                                            }
                                            else
                                            {
                                                PODet = new TLCSV_PuchaseOrderDetail();
                                            }

                                        }
                                    }
                                    else
                                    {
                                        PODet = new TLCSV_PuchaseOrderDetail();
                                    }
                                }

                                PODet.TLCUSTO_LineNumber = row.Index + 1;
                                PODet.TLCUSTO_PurchaseOrder_FK = PO.TLCSVPO_Pk;
                                if (!FabricMode)
                                {
                                    PODet.TLCUSTO_Style_FK = (int)row.Cells[2].Value;
                                }
                                else
                                {
                                    PODet.TLCUSTO_Quality_FK = Convert.ToInt32(row.Cells[2].Value.ToString());
                                }

                                PODet.TLCUSTO_Colour_FK = (int)row.Cells[3].Value;

                                if (!FabricMode)
                                {
                                    PODet.TLCUSTO_Size_FK = (int)row.Cells[4].Value;
                                }

                                PODet.TLCUSTO_Customer_FK = CustSelected.Cust_Pk;
                                if (!FabricMode)
                                {
                                    if (row.Cells[4].Value != null)
                                        PODet.TLCUSTO_Qty = (int)row.Cells[5].Value;
                                    else
                                        PODet.TLCUSTO_Qty = (int)row.Cells[5].EditedFormattedValue;
                                }
                                else
                                {
                                    if (row.Cells[4].Value != null)
                                        PODet.TLCUSTO_QtyMeters  = Convert.ToInt32(row.Cells[5].Value.ToString());
                                    else
                                        PODet.TLCUSTO_QtyMeters = Convert.ToInt32(row.Cells[5].EditedFormattedValue.ToString());
                                }

                                if (!FabricMode)
                                {
                                    PODet.TLCUSTO_Grade = row.Cells[6].Value.ToString();
                                }
                                else
                                {
                                    PODet.TLCUSTO_Grade = "A";
                                }
                                
                                if (row.Cells[7].Value != null)
                                {
                                    PODet.TLCUSTO_DateRequired = Convert.ToDateTime(row.Cells[7].Value);
                                }
                                else
                                {
                                    PODet.TLCUSTO_DateRequired = dtpRequiredDate.Value;
                                }

                                if ((!EditMode || AddnAdd) && NewRecord)
                                {
                                    context.TLCSV_PuchaseOrderDetail.Add(PODet);
                                }
                                //-----------------------------------------------------------------

                                if (!FabricMode)
                                {
                                    DataRow dr = dt.NewRow();
                                    string temp = context.TLADM_Styles.Find((int)row.Cells[2].Value).Sty_Description.Trim();
                                    dr[0] = temp;
                                    temp = context.TLADM_Colours.Find(row.Cells[3].Value).Col_Description;
                                    dr[1] = temp;
                                    temp = context.TLADM_Sizes.Find(row.Cells[4].Value).SI_Description;
                                    dr[2] = temp;
                                    int temp1 = 0;
                                    if (row.Cells[5].Value != null)
                                    {
                                        temp1 = (int)row.Cells[5].Value;
                                    }
                                    else
                                    {
                                        temp1 = (int)row.Cells[5].EditedFormattedValue;
                                    }
                                    dr[3] = temp1;

                                    dt.Rows.Add(dr);
                                }
                            }
                       }

                        try
                        {
                            context.SaveChanges();
                            
                            MessageBox.Show("Data successfully saved to database");
                            
                            EditMode = false;
                            AddnAdd  = false;

                            if (!rbOrderClosed.Checked && CustSelected.Cust_SendEmail && !FabricMode)
                            {
                                DialogResult res = MessageBox.Show("Would you like to send a confirmation email to this client", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (res == DialogResult.Yes)
                                {
                                    if (!String.IsNullOrEmpty(CustSelected.Cust_ContactPersonEmail))
                                    {
                                        core = new Util();
                                        core.SendEmailtoContacts(CustSelected.Cust_ContactPersonEmail, dt , 1, DateTime.Now, txtCustomerPO.Text,  txtTransNumber.Text);
                                    }
                                    else
                                    {
                                        MessageBox.Show("This customer needs to have a valid email address", "Valid EMail address required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }

                            formloaded = false;

                            dataGridView1.Rows.Clear();
                            if (txtCustomerPO.ReadOnly)
                                txtCustomerPO.ReadOnly = false;

                            txtCustomerPO.Text = string.Empty;
                            txtTransNumber.Text = "CO" + TransNumber.ToString().PadLeft(5, '0');
                            
                            cmboCustomers.SelectedValue = -1;
                            rbOrderActive.Checked = true;

                            cmboCurrentOrders.DataSource = null;
                            cmboCurrentOrders.DataSource = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == CustSelected.Cust_Pk && !x.TLCSVPO_Closeed).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                            cmboCurrentOrders.ValueMember = "TLCSVPO_Pk";
                            cmboCurrentOrders.DisplayMember = "TLCSVPO_PurchaseOrder";
                            cmboCurrentOrders.SelectedValue = -1;

                           
                            
                            rbSpecialNo.Checked = true;
                            if (!cmboCurrentOrders.Enabled)
                                cmboCurrentOrders.Enabled = true;

                            formloaded = true;
                       
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
               }
            }
        }

      
        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;
          
            if (oDgv != null && formloaded)
            {
                if (oDgv.CurrentCell != null)
                {
                    var row = oDgv.CurrentRow;

                    var RWComplete = core.RowComplete(row, MandatoryFields);
                    var cnt = RWComplete.Where(x => x == false).Count();

                    if(cnt != MandatoryFields.Count() && RowLeave)
                    {
                        ActiveRow = oDgv.CurrentCell.RowIndex;
                        var CurrentRow = oDgv.CurrentRow;
                        if (CurrentRow != null)
                        {
                            complete = core.RowComplete(CurrentRow, MandatoryFields);
                        }

                        MandSelected = complete;
                        var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                        if (!FabricMode && !string.IsNullOrEmpty(errorM))
                        {
                            MessageBox.Show(errorM, "Error Message");
                            RowLeave = true;
                        }
                    }
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
                            
            if (!EditMode && !AddnAdd)
            {
                oDgv.Rows[oDgv.NewRowIndex].Cells[1].Value = "L" + (oDgv.NewRowIndex + 1).ToString().PadLeft(5, '0');
                oDgv.Rows[oDgv.NewRowIndex].Cells[6].Value = "A";
            }
            else
            {
                if (AddnAdd)
                {
                    oDgv.Rows[oDgv.NewRowIndex].Cells[1].Value = "L" + (oDgv.NewRowIndex + 1).ToString().PadLeft(5, '0');
                    oDgv.Rows[oDgv.NewRowIndex].Cells[6].Value = "A";
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox oCombo = e.Control as ComboBox;       
            if (formloaded)
            {
                RowLeave = true;
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else
                {
                    if (oDgv.CurrentCell is DataGridViewComboBoxCell)
                    {
                        oCombo.SelectedIndexChanged += new EventHandler (ComboBox_SelectedIndexChanged);
                    }
                }
            }
        }

        private void OCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var Cell = dataGridView1.CurrentCell;
            if (cb != null)
            {
                if (Cell.ColumnIndex == 2)
                {
                    var Style = _context.TLADM_Styles.Where(x => x.Sty_Description == Cell.EditedFormattedValue.ToString()).FirstOrDefault();
                    if (Style != null && Style.Sty_WorkWear)
                    {
                        oCmbB.DataSource = null;
                        oCmbB.Items.Clear();
                        var tst = (from T1 in _context.TLADM_StyleColour
                                   join T2 in _context.TLADM_Colours
                                   on T1.STYCOL_Colour_FK  equals T2.Col_Id
                                   where T1.STYCOL_Style_FK == Style.Sty_Id
                                   select T2).ToList();

                        oCmbB.DataSource = tst;
                        oCmbB.ValueMember = "Col_Id";
                        oCmbB.DisplayMember = "Col_Display";
                        
                        oCmbC.DataSource = null;
                        oCmbC.Items.Clear();
                        oCmbC.DataSource = _context.TLADM_Sizes.Where(x =>x.SI_ContiSize != 0).OrderBy(x => x.SI_DisplayOrder).ToList();
                        oCmbC.ValueMember = "SI_id";
                        oCmbC.DisplayMember = "SI_ContiSize";
                    }
                }
            }
        }
        private void txtCustomerPO_Validating(object sender, CancelEventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null)
            {
                using (var context = new TTI2Entities())
                {
                    var selected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                    if (selected != null)
                    {
                        if (oTxt.Name == "txtCustomerPO" && oTxt.Text.Length > 0)
                        {

                            var Exists = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_PurchaseOrder == txtCustomerPO.Text.Trim() && x.TLCSVPO_Customer_FK == selected.Cust_Pk).FirstOrDefault();
                            if (Exists != null)
                            {
                                MessageBox.Show("The Purchase order entered " + oTxt.Text.Trim() + " already exists on file. Please reenter");
                                e.Cancel = true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a customer number from the combobox provided");
                        return;
                    }
                }
            }
        }

        private void dtpRequiredDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
                if (dtpRequiredDate.Value < dtpCustOrderDate.Value)
                {
                    MessageBox.Show("Required date must be at least one day more than order date");
                }
            }
        }

        private void cmboCurrentOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            DialogResult Result = new DialogResult();

            if (oCmbo != null && formloaded)
            {
                var SelectedOrder = (TLCSV_PurchaseOrder)oCmbo.SelectedItem;
                if (SelectedOrder != null)
                {
                    if (SelectedOrder.TLCSVPO_SpecialOrder)
                        rbSpecialYes.Checked = true;

                    if (SelectedOrder.TLCSVPO_Provisional)
                        rbProvisional.Checked = true;

                    RepackTransaction = SelectedOrder.TLCSVPO_RepackTransaction;

                    txtCustomerPO.Text = SelectedOrder.TLCSVPO_PurchaseOrder;

                   if (SelectedOrder.TLCSVPO_Closeed)
                    {
                        rbOrderClosed.Checked = true;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.Enabled = true;
                        btnAdd.Enabled = false;
                    }
                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == SelectedOrder.TLCSVPO_Pk && !x.TLCUSTO_Closed).OrderBy(x=>x.TLCUSTO_LineNumber) .ToList();

                        if (!SelectedOrder.TLCSVPO_Provisional)
                            txtCustomerPO.Enabled = false;
                        else
                            txtCustomerPO.Enabled = true;

                        
                        dataGridView1.Rows.Clear();
                        if (Existing.Count != 0)
                        {
                            EditMode = true;
                            dtpCustOrderDate.Value = SelectedOrder.TLCSVPO_TransDate;
                            dtpRequiredDate.Value = SelectedOrder.TLCSVPO_RequiredDate;
                            txtCustomerPO.Text = SelectedOrder.TLCSVPO_PurchaseOrder;
                        }

                        var CustomerFile = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                        if (CustomerFile != null)
                        {
                            if (CustomerFile.Cust_RePack && !SelectedOrder.TLCSVPO_RepackTransaction)
                            {
                                Result = MessageBox.Show("Do you wish to create an Repack Configuration", "Re Pack Confiquration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (Result == DialogResult.Yes)
                                {
                                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                    {
                                        try
                                        {
                                            //-----------------------------------------------------------------------
                                            // Repack center order configuration 
                                            //-----------------------------------------------------------------------------------------
                                            using (frmPurchaseOrderConfig ConFig = new frmPurchaseOrderConfig(SelectedOrder.TLCSVPO_Pk))
                                            {
                                                DialogResult dr = ConFig.ShowDialog(this);
                                                if (dr == DialogResult.OK)
                                                {

                                                }

                                                ConFig.Close();

                                                formloaded = false;
                                                cmboCurrentOrders.DataSource = null;
                                                cmboCurrentOrders.DataSource = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == CustomerFile.Cust_Pk && !x.TLCSVPO_Closeed).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                                                cmboCurrentOrders.ValueMember = "TLCSVPO_Pk";
                                                cmboCurrentOrders.DisplayMember = "TLCSVPO_PurchaseOrder";
                                                cmboCurrentOrders.SelectedValue = -1;
                                                formloaded = true;

                                                return;
                                            }
                                        }
                                        catch (System.Exception ex)
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    this.Close();
                                }
                            }
                            else if (CustomerFile.Cust_RePack && SelectedOrder.TLCSVPO_RepackTransaction)
                            {
                                MessageBox.Show("This order has already been reconfigured for Repacking", "No modifications allowed");
                                return;
                            }
                            

                            formloaded = false;
                           
                           foreach (var row in Existing)
                           {
                               var index = dataGridView1.Rows.Add();
                                if (FabricMode)
                                {
                                    dataGridView1.Rows[index].Cells[0].Value = row.TLCUSTO_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = "L" + row.TLCUSTO_LineNumber.ToString().PadLeft(5, '0');
                                    dataGridView1.Rows[index].Cells[2].Value = row.TLCUSTO_Style_FK;
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUSTO_Colour_FK;
                                    dataGridView1.Rows[index].Cells[4].Value = row.TLCUSTO_Size_FK;
                                    dataGridView1.Rows[index].Cells[5].Value = row.TLCUSTO_Qty;
                                    dataGridView1.Rows[index].Cells[6].Value = row.TLCUSTO_Grade;
                                    if (row.TLCUSTO_DateRequired != null)
                                        dataGridView1.Rows[index].Cells[7].Value = row.TLCUSTO_DateRequired;
                                    else
                                        dataGridView1.Rows[index].Cells[7].Value = SelectedOrder.TLCSVPO_RequiredDate;
                                    dataGridView1.Rows[index].Cells[8].Value = false;
                                    dataGridView1.Rows[index].Cells[9].Value = false;
                                    dataGridView1.Rows[index].Cells[10].Value = null;
                                }
                                else
                                {
                                    dataGridView1.Rows[index].Cells[0].Value = row.TLCUSTO_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = "L" + row.TLCUSTO_LineNumber.ToString().PadLeft(5, '0');
                                    dataGridView1.Rows[index].Cells[2].Value = row.TLCUSTO_Quality_FK;
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUSTO_Colour_FK;
                                    dataGridView1.Rows[index].Cells[4].Value = null;
                                    dataGridView1.Rows[index].Cells[5].Value = row.TLCUSTO_QtyMeters;
                                    dataGridView1.Rows[index].Cells[6].Value = row.TLCUSTO_Grade;
                                    if (row.TLCUSTO_DateRequired != null)
                                        dataGridView1.Rows[index].Cells[7].Value = row.TLCUSTO_DateRequired;
                                    else
                                        dataGridView1.Rows[index].Cells[7].Value = SelectedOrder.TLCSVPO_RequiredDate;
                                    dataGridView1.Rows[index].Cells[8].Value = false;
                                    dataGridView1.Rows[index].Cells[9].Value = false;
                                    dataGridView1.Rows[index].Cells[10].Value = null;
                                }

                           }
                           
                            formloaded = true;
                            if (cmboCurrentOrders.Enabled)
                                cmboCurrentOrders.Enabled = false;
                        }
                        if (EditMode)
                            dataGridView1.AllowUserToAddRows = false;
                    }
                }
            }
        }

        private void txtCustomerPO_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                if(!dataGridView1.AllowUserToAddRows)
                {
                    dataGridView1.AllowUserToAddRows = true;
                    AddnAdd = true;
                    EditMode = false;
                    dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[1].Value = "L" + (dataGridView1.NewRowIndex + 1).ToString().PadLeft(5, '0');
                    dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[6].Value = "A";
                }
                    
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("This line is to be closed", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    using (var context = new TTI2Entities())
                    {
                        if (res == DialogResult.OK)
                        {
                            var Pk = (int)oDgv.CurrentRow.Cells[0].Value;
                            TLCSV_PuchaseOrderDetail PoDetail = context.TLCSV_PuchaseOrderDetail.Find(Pk);
                            if (PoDetail != null)
                            {
                                var Count = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == PoDetail.TLCUSTO_Pk && x.TLSOH_Picked && !x.TLSOH_Sold).Count();
                                if (Count != 0)
                                {
                                    MessageBox.Show("There are outstanding items for this line order picked and not delivered. Closing prohibited", "Warning");
                                    return;
                                }

                                DataGridViewRow CurrentRow = oDgv.CurrentRow;

                                PoDetail.TLCUSTO_Closed = true;
                                oDgv.Rows.Remove(CurrentRow);

                                try
                                {
                                        context.SaveChanges();
                                }
                                catch (System.Exception ex)
                                {
                                        MessageBox.Show(ex.Message);
                                        return;
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

        private void rbOrderActive_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;
            if (oRb != null && formloaded)
            {
                if (oRb.Checked)
                {
                    dataGridView1.Enabled = true;
                    dataGridView1.AllowUserToAddRows = true;
                    if (!btnAdd.Enabled)
                        btnAdd.Enabled = true;
                }
            }
        }

        private void rbOrderClosed_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;
            if (oRb != null && formloaded)
            {
                if (oRb.Checked)
                {
                    dataGridView1.Enabled = false;
                    dataGridView1.AllowUserToAddRows = false;
                    EditMode = true;
                    btnAdd.Enabled = false;
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (formloaded)
            {
                if (e.ColumnIndex == 4)
                {
                    int Style = (int)oDgv.CurrentRow.Cells[2].Value;
                    int Colour = (int)oDgv.CurrentRow.Cells[3].Value;
                    int Size = (int)oDgv.CurrentRow.Cells[4].Value;

                    DataGridViewRow SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                                 where (int)Rows.Cells[2].Value == Style
                                                 && (int)Rows.Cells[3].Value == Colour
                                                 && (int)Rows.Cells[4].Value == Size
                                                 select Rows).FirstOrDefault();

                    if (SingleRow != null && SingleRow.Index != e.RowIndex)
                    {
                        MessageBox.Show("This Style, Colour and Size combination already exists on this order");
                    }
                }
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void frmCustomerOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form oFrm = (Form)sender;
            if (!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }

                var Customer = (TLADM_CustomerFile)this.cmboCustomers.SelectedItem;
                if (Customer != null)
                {
                    if (Customer.Cust_RePack)
                    {
                        //------------------------------------------------------------------------------------
                        // If a repack customer, the user may have created a packing config and then 
                        // at the last moment decided not to save the results 
                        // This creates an imbalance and 'loose' data with no reference point
                        // This is to do the neccessary house keeping....
                        //---------------------------------------------------------------------------
                        using (var context = new TTI2Entities())
                        {
                            context.TLCSV_RePackConfig.RemoveRange(context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == 0));
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (System.Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void rbProvisional_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked)
            {
                if (!txtCustomerPO.Enabled)
                    txtCustomerPO.Enabled = true;
            }
        }

        private void rbCopyOrder_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && EditMode && oRad.Checked)
            {
                EditMode = false;
                if (!txtCustomerPO.Enabled)
                {
                    txtCustomerPO.Enabled = true;
                    txtCustomerPO.Text = string.Empty;
                    AddnAdd = false;
                    Mode = true;
                }
            }
        }

        private void rbSpecialNo_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && EditMode && oRad.Checked)
             {
                 if (txtCustomerPO.Enabled)
                 {
                     txtCustomerPO.Enabled = false;
                 }
             }
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if(oDgv != null && formloaded)
            {
               
            }
        }
    }
}
