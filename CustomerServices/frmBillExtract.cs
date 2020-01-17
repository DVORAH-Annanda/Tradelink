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

namespace CustomerServices
{
    public partial class frmBillExtract : Form
    {
        bool FormLoaded;
        private DataTable dt;
        private Util core;
        public frmBillExtract()
        {
            InitializeComponent();
            dt = new DataTable();
            //------------------------------------------------------
            // Create column 1. // This is Index Position of the measurement in the TLCMT_AuditMeasurent Recorded Table
            //----------------------------------------------
            DataColumn column = new DataColumn();

            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "OrderAllocated_Pk";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "OrderSelected";
            column.Caption = "Select";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "PurchaseOrder_Details";
            column.Caption = "Purchase Order";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "DeliveryNo";
            column.Caption = "Delivery No";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "DeliveryDate";
            column.Caption = "Date";
            dt.Columns.Add(column);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                {
                    dataGridView1.Columns[idx].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    dataGridView1.Columns[col.ColumnName].Width = 120;
                }
            }

            core = new Util();
           
        }

        private void frmBillExtract_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.Where(x => !x.Cust_Blocked).OrderBy(x => x.Cust_Code).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

            }
            
            FormLoaded = true;

        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if(oCmbo != null && FormLoaded)
            {
                var SelectedItem = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if (SelectedItem != null)
                {
                    dt.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var OrdersAllocated = context.TLCSV_OrderAllocated.Where(x => !x.TLORDA_Returned && x.TLORDA_Delivered && !x.TLORDA_Invoiced).ToList();
                        foreach(var OrderAllocated in OrdersAllocated)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = OrderAllocated.TLORDA_Pk;
                            dr[1] = false;
                            dr[2] = context.TLCSV_PurchaseOrder.Find(OrderAllocated.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                            dr[3] = OrderAllocated.TLORDA_DelTransNumber;
                            dr[4] = OrderAllocated.TLORDA_DeliveredDate;
                            dr[5] = OrderAllocated.TLORDA_Transporter;

                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender; 
            if(oBtn != null && FormLoaded)
            {
                var RecCount = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).Count();
                if(RecCount == 0)
                {
                    MessageBox.Show("Please select at least one delivery to bill");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.Field<bool>(1) == false)
                        {
                            continue;
                        }

                        var Boxes = context.TLCSV_StockOnHand.Where(x => x.TLSOH_DNListNo == row.Field<int>(3) && !x.TLSOH_Invoiced).ToList();
                        foreach(var Box in Boxes)
                        {
                            // Need to develop the code to write to ODO
                            //====================================================
                            Box.TLSOH_InvDate = DateTime.Now;
                            Box.TLSOH_Invoiced = true;
                        }

                        var OrderAlloc = context.TLCSV_OrderAllocated.Find(row.Field<int>(0));

                        if(OrderAlloc != null)
                        {
                            OrderAlloc.TLORDA_Invoiced = true;
                            OrderAlloc.TLORDA_DateInvoiced = DateTime.Now;
                        }
    
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data Successfully Saved to Database");
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
