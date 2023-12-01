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
using EntityFramework.Extensions;
namespace CustomerServices
{
    public partial class frmCustDeliveries : Form
    {
        bool formloaded;

        protected readonly TTI2Entities _context;

        DataGridViewTextBoxColumn oTxtA;    // 0 Pk
        DataGridViewCheckBoxColumn oChkA;   // 1 Select
        DataGridViewTextBoxColumn oTxtB;    // 2 PickSlip Number
        DataGridViewTextBoxColumn oTxtC;    // 3 Customer
        DataGridViewTextBoxColumn oTxtD;    // 4 WareHouse 
        DataGridViewTextBoxColumn oTxtE;    // 5 Created Date
        DataGridViewTextBoxColumn oTxtF;    // 6 Pick Slip Number Number ( numeric Number)
        DataGridViewTextBoxColumn oTxtG;    // 7 PO Primary Key
        DataGridViewTextBoxColumn oTxtH;    // 8 PickingList Primary Key
        DataGridViewButtonColumn oBtnA;     // View Picking Lists  

        CustomerServicesParameters ShirtQueryParameters;

        public frmCustDeliveries()
        {
            InitializeComponent();
            _context = new TTI2Entities();
        }

        private void frmCustDeliveries_Load(object sender, EventArgs e)
        {
            formloaded = false;
            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            
            var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
            if (Dept != null)
            {
                var LNU = _context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                if (LNU != null)
                {
                        txtDeliveryNo.Text = "F" + LNU.col4.ToString().PadLeft(5, '0');
                }
            }

            cmboCustomers.DataSource = _context.TLADM_CustomerFile.OrderBy(x=>x.Cust_Description).ToList();
            cmboCustomers.DisplayMember = "Cust_Description";
            cmboCustomers.ValueMember = "Cust_PK";
            cmboCustomers.SelectedValue = -1;

            cmboTransporters.DataSource = _context.TLADM_Transporters.OrderBy(x => x.TLTRNS_Description).ToList();
            cmboTransporters.DisplayMember = "TLTRNS_Description";
            cmboTransporters.ValueMember = "TLTRNS_Pk";
            cmboTransporters.SelectedValue = -1;
    
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
            oTxtB.HeaderText = "Pick Slip";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.ReadOnly = true;
            oTxtC.HeaderText = "Customer";
            oTxtC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.ReadOnly = true;
            oTxtD.HeaderText = "PO Number";
            oTxtD.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.ReadOnly = true;
            oTxtE.HeaderText = "Created Date";
            oTxtE.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtE);


            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.ReadOnly = true;
            oTxtF.Visible = false;
            oTxtF.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtF);

            oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.ReadOnly = true;
            oTxtG.Visible = false;
            oTxtG.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtG);

            oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.ReadOnly = true;
            oTxtH.Visible = false;
            oTxtH.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtH);

            oBtnA = new DataGridViewButtonColumn();
            oBtnA.HeaderText = "View PL";
            dataGridView1.Columns.Add(oBtnA);
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;

            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Repository repo = new Repository();
            CustomerServicesParameters parms = new CustomerServicesParameters();
            CSVServices svces = new CSVServices();
            int TransNumber = 0;

            if (oBtn != null && formloaded)
            {
                var SelectedTransporter = (TLADM_Transporters)cmboTransporters.SelectedItem;
                if (SelectedTransporter == null)
                {
                    MessageBox.Show("Please select a transporter");
                    return;
                    
                }

                //using (var context = new TTI2Entities())
                //{
                    var selected = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                    if (selected == null)
                    {
                        using (DialogCenteringService svs = new DialogCenteringService(this))
                        {
                            MessageBox.Show("Please select a customer from the drop down box");
                            return;
                        }
                    }

                    parms.Customers.Add(repo.LoadCustomers(selected.Cust_Pk));

                    var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                    if (Dept != null)
                    {
                        var LNU = _context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                        if (LNU != null)
                        {
                            TransNumber = LNU.col4;
                            LNU.col4 += 1;
                            txtDeliveryNo.Text = "F" + LNU.col4.ToString().PadLeft(5, '0');
                        }
                    }

                    int Fk = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;

                        //  Find the record the allocated file
                        //----------------------------------------------------
                        var pk = (int)row.Cells[0].Value;
                        parms.OrdersAllocated.Add(repo.LoadOrderAllocated(pk));
                        
                        var Allocated = _context.TLCSV_OrderAllocated.Find(pk);
                        if (Allocated != null)
                        { 
                            Allocated.TLORDA_Delivered =      true;
                            Allocated.TLORDA_DeliveredDate =  DateTime.Now;
                            Allocated.TLORDA_DelTransNumber = TransNumber;
                            Allocated.TLORDA_Transporter    = string.Empty;
                            Allocated.TLORDA_Transporter_FK = SelectedTransporter.TLTRNS_Pk;
                            Allocated.TLORDA_PLStockOrder   = false;

                            Fk = (int)row.Cells[6].Value;
                            var soh = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == Fk && !x.TLSOH_Sold).ToList();
                            foreach (var rec in soh)
                            {
                                rec.TLSOH_Sold = true;
                                rec.TLSOH_SoldDate = DateTime.Now;
                                rec.TLSOH_Customer_Fk = selected.Cust_Pk;
                                rec.TLSOH_DNListNo = TransNumber;
                                rec.TLSOH_DNListDate = DateTime.Now;

                                var POD = _context.TLCSV_PuchaseOrderDetail.Find(rec.TLSOH_POOrderDetail_FK);
                                if (POD != null)
                                {
                                    POD.TLCUSTO_Delivered = true;
                                    POD.TLCUSTO_DeliveredDate = DateTime.Now;
                                    POD.TLCUSTO_DeliveryNumber = TransNumber;
                                    POD.TLCUSTO_QtyDelivered_ToDate += rec.TLSOH_BoxedQty;

                                    if (POD.TLCUSTO_Qty - POD.TLCUSTO_QtyDelivered_ToDate <= 0)
                                    {
                                        POD.TLCUSTO_Closed = true;
                                    }
                                }
                            }

                            //Make this a global transaction 
                            //===========================================================
                            var RePackTransactions = _context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrder_FK == Allocated.TLORDA_POOrder_FK).ToList();
                            foreach (var RePackTransaction in RePackTransactions)
                            {
                                var RePackConfig = _context.TLCSV_RePackConfig.Find(RePackTransaction.REPACT_RePackConfig_FK);
                                if (RePackConfig != null)
                                {
                                    RePackConfig.PORConfig_SizeBoxQty_Delivered += RePackTransaction.REPACT_BoxedQty; 
                                }
                            }
                        }
                    }

                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show("data successfully saved to database");
                    
                        dataGridView1.Rows.Clear();
                        cmboCustomers.SelectedValue = -1;
                    

                        svces.TransNumber = TransNumber;
                        svces.DateIntoStock = DateTime.Now;
                        svces.DNReprint = false;

                        frmCSViewRep vRep = new frmCSViewRep(10, parms, svces);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                //}
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLCSV_OrderAllocated> Existing = null;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if(selected != null)
                {
                    dataGridView1.Rows.Clear();

                    //using (var context = new TTI2Entities())
                    //{
                        if (!chkPLStockOrders.Checked)
                        {
                            Existing = _context.TLCSV_OrderAllocated.Where(x => x.TLORDA_Customer_FK == selected.Cust_Pk && x.TLORDA_PLConfirmed && x.TLORDA_PickListPrint && !x.TLORDA_Delivered).ToList();
                        }
                        else
                        {
                            Existing = _context.TLCSV_OrderAllocated.Where(x => x.TLORDA_Customer_FK == selected.Cust_Pk && x.TLORDA_PLConfirmed &&x.TLORDA_PickListPrint && !x.TLORDA_Delivered && x.TLORDA_PLStockOrder).ToList();
                        }

                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLORDA_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = "PL" + row.TLORDA_TransNumber.ToString().PadLeft(5, '0');
                            dataGridView1.Rows[index].Cells[3].Value = _context.TLADM_CustomerFile.Find(row.TLORDA_Customer_FK).Cust_Description;
                            dataGridView1.Rows[index].Cells[4].Value = _context.TLCSV_PurchaseOrder.Find(row.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                            dataGridView1.Rows[index].Cells[5].Value = row.TLORDA_PLPrintDate.ToString();
                            dataGridView1.Rows[index].Cells[6].Value = row.TLORDA_TransNumber;
                            dataGridView1.Rows[index].Cells[7].Value = row.TLORDA_Customer_FK;
                        }
                    //}
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                int Pk = Convert.ToInt32(oDgv.Rows[e.RowIndex].Cells[2].Value.ToString().Remove(0,2));
                if (Pk != 0)
                {
                    CSVServices svces = new CSVServices();
                    svces.PLReprint = true;
                    svces.DNReprint = false;
                    svces.POCustomer_PK = (int)oDgv.Rows[e.RowIndex].Cells[7].Value;
                                        

                    frmCSViewRep vRep = new frmCSViewRep(4, svces, Pk);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }

            }
        }

        private void frmCustDeliveries_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
