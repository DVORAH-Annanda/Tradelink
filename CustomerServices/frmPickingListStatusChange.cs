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
    public partial class frmPickingListStatusChange : Form
    {

        DataGridViewTextBoxColumn  oTxtBoxA;    // Record Index of datatable file 
        DataGridViewCheckBoxColumn oChkBoxA;    // CheckBox  
      
        DataGridViewTextBoxColumn oTxtBoxC;     // Picking List Number
        DataGridViewTextBoxColumn oTxtBoxD;     // Date 
        DataGridViewTextBoxColumn oTxtBoxE;     // Customer No
        DataGridViewTextBoxColumn oTxtBoxF;     // Customer Name 

        bool FormLoaded;

        public frmPickingListStatusChange()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
           
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkBoxA = new DataGridViewCheckBoxColumn();    // CheckBox
            oChkBoxA.HeaderText = "Select";
            oChkBoxA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkBoxA);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.HeaderText = "Picking List Number";
            oTxtBoxC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.ReadOnly = true;
            oTxtBoxD.HeaderText = "Date Created";
            oTxtBoxD.ValueType = typeof(String);
            dataGridView1.Columns.Add(oTxtBoxD);

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.ReadOnly = true;
            oTxtBoxE.HeaderText = "Order Number";
            oTxtBoxE.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtBoxE);

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.ReadOnly = true;
            oTxtBoxF.HeaderText = "Customer Name";
            oTxtBoxF.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtBoxF);

        }

        private void frmPickingListStatusChange_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            rbPlaceOnStatus.Checked = true;
            using (var context = new TTI2Entities())
            {
                var Orders = context.TLCSV_OrderAllocated.Where(x => !x.TLORDA_Delivered && !x.TLORDA_PLStockOrder).ToList();
                foreach (var Order in Orders)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Order.TLORDA_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = Order.TLORDA_TransNumber;
                    var dt = Order.TLORDA_TransDate.ToShortDateString();
                    dataGridView1.Rows[index].Cells[3].Value = Convert.ToDateTime(dt).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[index].Cells[4].Value = context.TLCSV_PurchaseOrder.Find(Order.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                    dataGridView1.Rows[index].Cells[5].Value = context.TLADM_CustomerFile.Find(Order.TLORDA_Customer_FK).Cust_Description;

                }
            }
            FormLoaded = true;

        }

        private void rbPlaceOnStatus_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                dataGridView1.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    var Orders = context.TLCSV_OrderAllocated.Where(x => !x.TLORDA_Delivered && !x.TLORDA_PLStockOrder).ToList();
                    foreach (var Order in Orders)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Order.TLORDA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Order.TLORDA_TransNumber;
                        var dt = Order.TLORDA_TransDate.ToShortDateString();
                        dataGridView1.Rows[index].Cells[3].Value = Convert.ToDateTime(dt).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[index].Cells[4].Value = context.TLCSV_PurchaseOrder.Find(Order.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                        dataGridView1.Rows[index].Cells[5].Value = context.TLADM_CustomerFile.Find(Order.TLORDA_Customer_FK).Cust_Description;

                    }
                }
 
            }
        }

        private void rbPlaceOffStatus_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                dataGridView1.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    var Orders = context.TLCSV_OrderAllocated.Where(x => !x.TLORDA_Delivered && x.TLORDA_PLStockOrder).ToList();
                    foreach (var Order in Orders)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Order.TLORDA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Order.TLORDA_TransNumber;
                        var dt = Order.TLORDA_TransDate.ToShortDateString();
                        dataGridView1.Rows[index].Cells[3].Value = Convert.ToDateTime(dt).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[index].Cells[4].Value = context.TLCSV_PurchaseOrder.Find(Order.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                        dataGridView1.Rows[index].Cells[5].Value = context.TLADM_CustomerFile.Find(Order.TLORDA_Customer_FK).Cust_Description;

                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null && FormLoaded)
            {
                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[0].Value == null || !(bool)Row.Cells[1].Value)
                            continue;

                        var Pk = (int)Row.Cells[0].Value;

                        var OrderAllocated = context.TLCSV_OrderAllocated.Find(Pk);
                        if (OrderAllocated != null)
                        {
                            if (rbPlaceOnStatus.Checked)
                                OrderAllocated.TLORDA_PLStockOrder = true;
                            else
                                OrderAllocated.TLORDA_PLStockOrder = false;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data Saved to database successfully");
                        dataGridView1.Rows.Clear();
                        frmPickingListStatusChange_Load(this, null);

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
