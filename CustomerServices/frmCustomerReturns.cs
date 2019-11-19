using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;


namespace CustomerServices
{
  
    public partial class frmCustomerReturns : Form
    {
        bool formloaded;
        Util core;

        DataTable DataT = null;

        //datgridview1  
        DataGridViewTextBoxColumn oTxtA;    // 0 Pk
        DataGridViewCheckBoxColumn oChkA;   // 1 Select
        DataGridViewTextBoxColumn oTxtB;    // 2 PO Number
        DataGridViewTextBoxColumn oTxtC;    // 3 Delivery Note
        DataGridViewTextBoxColumn oTxtD;    // 4 Picking Slip Number 
        DataGridViewTextBoxColumn oTxtE;    // 5 WareHouse
        DataGridViewTextBoxColumn oTxtF;    // 5 Created Date
        DataGridViewTextBoxColumn oTxtG;    // 7 PO Index Number

        DataGridViewTextBoxColumn oTxtAA;   // 0 Record Primary Key
        DataGridViewCheckBoxColumn oChkAA;  // 1 Select Yes or No 
        DataGridViewCheckBoxColumn oChkAB;  // 2 Select Yes or No for resold  
        DataGridViewTextBoxColumn oTxtBA;   // 3 Box Number 
        DataGridViewTextBoxColumn oTxtCA;   // 4 Style Description 
        DataGridViewTextBoxColumn oTxtDA;   // 5 Colour Descriptiion
        DataGridViewTextBoxColumn oTxtEA;   // 6 Size Description 
        DataGridViewTextBoxColumn oTxtFA;   // 7 Delivered Quantity 

       CustomerServicesParameters shirtParameters;

        int TransNumber = 0;

        public frmCustomerReturns()
        {
            InitializeComponent();
        }

        private void frmCustomerReturns_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                if (Dept != null)
                {
                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    if (LNU != null)
                    {
                        txtTransNumber.Text = "R" + LNU.col5.ToString().PadLeft(5, '0');
                        TransNumber = LNU.col5;
                    }
                }

                txtReasons.Text = string.Empty;
                txtCustomerRef.Text = string.Empty;
                txtApprovedBy.Text = string.Empty;
 

                cmboCustomers.DataSource = context.TLADM_CustomerFile.ToList();
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.ValueMember = "Cust_PK";
                cmboCustomers.SelectedValue = -1;

                cmboWareHouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                cmboWareHouse.ValueMember = "WhStore_Id";
                cmboWareHouse.DisplayMember = "WhStore_Description";
                cmboWareHouse.SelectedValue = -1;

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.HeaderText = "Selected";
                oChkA.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.ReadOnly = true;
                oTxtB.HeaderText = "PONumber";
                oTxtB.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.ReadOnly = true;
                oTxtC.HeaderText = "Delivery Note";
                oTxtC.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtC);

                oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.ReadOnly = true;
                oTxtD.HeaderText = "Picking Slip";
                oTxtD.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtD);

                oTxtE = new DataGridViewTextBoxColumn();
                oTxtE.ReadOnly = true;
                oTxtE.HeaderText = "WareHouse";
                oTxtE.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtE);


                oTxtF = new DataGridViewTextBoxColumn();
                oTxtF.ReadOnly = true;
                oTxtF.HeaderText = "Craeted Date";
                oTxtF.Visible = false;
                oTxtF.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtF);


                oTxtG = new DataGridViewTextBoxColumn();
                oTxtG.ReadOnly = true;
                oTxtG.Visible = false;
                oTxtG.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtG);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.AllowUserToOrderColumns = false;

                oTxtAA = new DataGridViewTextBoxColumn();   //0
                oTxtAA.ValueType = typeof(int);
                oTxtAA.ReadOnly = true;
                oTxtAA.Visible = false;
                dataGridView2.Columns.Add(oTxtAA);

                oChkAA = new DataGridViewCheckBoxColumn();    //1
                oChkAA.Visible = true;
                oChkAA.HeaderText = "Select";
                dataGridView2.Columns.Add(oChkAA);

                oChkAB = new DataGridViewCheckBoxColumn();    //2
                oChkAB.Visible = true;
                oChkAB.HeaderText = "Resold";
                dataGridView2.Columns.Add(oChkAB);

                oTxtBA = new DataGridViewTextBoxColumn();   //3
                oTxtBA.HeaderText = "Box Number";
                oTxtBA.Width = 170;
                oTxtBA.ValueType = typeof(String);
                oTxtBA.ReadOnly = true;
                dataGridView2.Columns.Add(oTxtBA);

                oTxtCA = new DataGridViewTextBoxColumn();   //4 
                oTxtCA.HeaderText = "Style";
                oTxtCA.ValueType = typeof(String);
                oTxtCA.ReadOnly = true;
                dataGridView2.Columns.Add(oTxtCA);

                oTxtDA = new DataGridViewTextBoxColumn();    //5
                oTxtDA.HeaderText = "Colour";
                oTxtDA.ValueType = typeof(string);
                oTxtDA.ReadOnly = true;
                dataGridView2.Columns.Add(oTxtDA);

                oTxtEA = new DataGridViewTextBoxColumn();    //6
                oTxtEA.HeaderText = "Size";
                oTxtEA.ValueType = typeof(string);
                oTxtEA.ReadOnly = true;
                dataGridView2.Columns.Add(oTxtEA);

                oTxtFA = new DataGridViewTextBoxColumn();    //7
                oTxtFA.HeaderText = "Qty Returned";
                oTxtFA.ValueType = typeof(int);
                dataGridView2.Columns.Add(oTxtFA);

                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.AllowUserToOrderColumns = false;

                core = new Util();
            }

            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLCSV_OrderAllocated OrderAllocated = null;
            Repository repo = new Repository();
            CSVServices services = new CSVServices();
            shirtParameters = new  CustomerServicesParameters();

            if (oBtn != null && formloaded)
            {
                var WareHouse = (TLADM_WhseStore)cmboWareHouse.SelectedItem;
                if (WareHouse == null)
                {
                    MessageBox.Show("Please select a warehouse from the drop down box");
                    return;
                }

                if (txtCustomerRef.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a customer reference");
                    return;
                }

                if (txtReasons.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the reason these goods are being returned");
                    return;
                }

                if (txtApprovedBy.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the name of the approver");
                    return;
                }
                //----------------------------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;

                        OrderAllocated = new TLCSV_OrderAllocated();
                        var Pk = (int)row.Cells[0].Value;
                        OrderAllocated = context.TLCSV_OrderAllocated.Find(Pk);
                        if (OrderAllocated != null)
                        {
                            OrderAllocated.TLORDA_Returned = true;
                            OrderAllocated.TLORDA_ReturnCustRef = txtCustomerRef.Text;
                            OrderAllocated.TLORDA_ApprovedBy = txtApprovedBy.Text;
                            OrderAllocated.TLORDA_ReturnNumber = TransNumber;
                            OrderAllocated.TLORDA_ReturnedDate = dtpTransactionDate.Value;
                          
                            shirtParameters.OrdersAllocated.Add(repo.LoadOrderAllocated(Pk));
                        }

                    }

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;

                        var ReSold = (bool)row.Cells[2].Value; 

                        int Pk = (int)row.Cells[0].Value;
                        
                        var soh = context.TLCSV_StockOnHand.Find(Pk);
                        if (soh != null)
                        {
                            int BoxedQty = (int)row.Cells[7].Value;
                            soh.TLSOH_ReturnedBoxQty = BoxedQty;
                            soh.TLSOH_BoxedQty = BoxedQty;
                            soh.TLSOH_WareHouse_FK = WareHouse.WhStore_Id;
                            soh.TLSOH_ReturnNumber = TransNumber;
                            soh.TLSOH_ReturnedDate = dtpTransactionDate.Value;

                            if (!ReSold)
                            {
                                soh.TLSOH_Returned = true;
                                soh.TLSOH_Grade = "B";
                            }
                            else
                            {
                                soh.TLSOH_Picked = false;
                                soh.TLSOH_PickListDate = null;
                                soh.TLSOH_PickListNo = 0;
                                soh.TLSOH_Sold = false;
                                soh.TLSOH_DNListDate = null;
                                soh.TLSOH_DNListNo = 0;
                                soh.TLSOH_Grade = "A";
                                soh.TLSOH_SoldDate = null;
                                soh.TLSOH_WareHousePickList = 0;
                                soh.TLSOH_WareHouseDeliveryNo = 0;
                            }
                        }
                    }

                    try
                    {
                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                            if (LNU != null)
                            {
                                TransNumber = LNU.col5;
                                LNU.col5 += 1;
                            }
                        }

                        //-------------------------------------------------------
                        //
                        //-----------------------------------------------------------
                        string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                                   .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                                   .ToString();

                        TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                        DailyLog.TLDL_IPAddress = Mach_IP;
                        DailyLog.TLDL_Dept_Fk = Dept.Dep_Id;
                        DailyLog.TLDL_Date = DateTime.Now;
                        DailyLog.TLDL_TransDetail = "Customer Services - Stock Returned";
                        DailyLog.TLDL_AuthorisedBy = txtApprovedBy.Text; ;
                        DailyLog.TLDL_Comments = txtCustomerRef.Text; 

                        context.TLADM_DailyLog.Add(DailyLog);

                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");

                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows.Clear();
 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    services.ApprovedBy = txtApprovedBy.Text;
                    services.Reasons = txtReasons.Text;
                    services.ReturnDate = dtpTransactionDate.Value;
                    services.CustomerRef = txtCustomerRef.Text;
                    services.WareHouse = WareHouse.WhStore_Id;

                    frmCSViewRep vRep = new frmCSViewRep(11, shirtParameters, services);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    frmCustomerReturns_Load(this, null);

                 }
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_CustomerFile)oCmbo.SelectedItem;

                if (selected == null)
                {
                    MessageBox.Show("Please selected a customer from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var DteTime = DateTime.Now.AddMonths(-3);
                    var Allocated = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_Customer_FK == selected.Cust_Pk && x.TLORDA_TransDate >= DteTime).ToList();
                                 

                    foreach (var row in Allocated)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = row.TLORDA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        var PurchaseOrder = context.TLCSV_PurchaseOrder.Find(row.TLORDA_POOrder_FK);
                        if (PurchaseOrder != null)
                        {
                             dataGridView1.Rows[index].Cells[2].Value = PurchaseOrder.TLCSVPO_PurchaseOrder;
                        }
                        dataGridView1.Rows[index].Cells[3].Value = "F" + row.TLORDA_DelTransNumber.ToString().PadLeft(5, '0'); ;
                        dataGridView1.Rows[index].Cells[4].Value = "PL" + row.TLORDA_TransNumber.ToString().PadLeft(5, '0');
                        dataGridView1.Rows[index].Cells[5].Value = " "; //  context.TLADM_WhseStore.Find(row.TLORDA_WareHouse_FK).WhStore_Description;
                        dataGridView1.Rows[index].Cells[6].Value = row.TLORDA_PLPrintDate.ToString();
                        dataGridView1.Rows[index].Cells[7].Value = row.TLORDA_POOrder_FK;
                        
                    }
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    var CurrentRow = oDgv.CurrentRow;

                    var AllReadyTicked = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                          where (bool)Rows.Cells[1].Value == true
                                          select Rows).ToList();

                    foreach (DataGridViewRow Row in AllReadyTicked)
                    {
                        if (Row.Index == CurrentRow.Index)
                            continue;

                        dataGridView1.Rows[Row.Index].Cells[1].Value = false;
                    }

                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();

                    if(DataT != null && DataT.Rows.Count != 0)
                        DataT.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var FK = (int)CurrentRow.Cells[7].Value;
                        var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrder_FK == FK).ToList();

                        foreach (var Row in Existing)
                        {
                            var indexx = dataGridView2.Rows.Add();
                            dataGridView2.Rows[indexx].Cells[0].Value = Row.TLSOH_Pk;
                            dataGridView2.Rows[indexx].Cells[1].Value = false;
                            dataGridView2.Rows[indexx].Cells[2].Value = false;
                            dataGridView2.Rows[indexx].Cells[3].Value = Row.TLSOH_BoxNumber;
                            dataGridView2.Rows[indexx].Cells[4].Value = context.TLADM_Styles.Find(Row.TLSOH_Style_FK).Sty_Description;
                            dataGridView2.Rows[indexx].Cells[5].Value = context.TLADM_Colours.Find(Row.TLSOH_Colour_FK).Col_Description;
                            dataGridView2.Rows[indexx].Cells[6].Value = context.TLADM_Sizes.Find(Row.TLSOH_Size_FK).SI_Description;
                            dataGridView2.Rows[indexx].Cells[7].Value = Row.TLSOH_BoxedQty;
                        }

                       // dataGridView2.DataSource = DataT;
                    }
                }
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var Cell = oDgv.CurrentCell;

                if (Cell.ColumnIndex == 6)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }
        }
    }
}
