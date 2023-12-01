using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using EntityFramework.Extensions;

namespace CustomerServices
{
    public partial class frmSalesPickListConfirmation : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtBoxA;   // Pk
        DataGridViewCheckBoxColumn oChkA;     // Checkbox 
        DataGridViewTextBoxColumn oTxtBoxB;   // Whse
        DataGridViewTextBoxColumn oTxtBoxC;   // PO Order 
        DataGridViewTextBoxColumn oTxtBoxD;   // Box Number  
        DataGridViewTextBoxColumn oTxtBoxE;   // Boxed Qty
        DataGridViewTextBoxColumn oTxtBoxF;   // Whse Picking List Number;

        protected readonly TTI2Entities _context;
        
       
        Util core;

        public frmSalesPickListConfirmation()
        {
            InitializeComponent();

            _context = new TTI2Entities();
        
            oTxtBoxA = new DataGridViewTextBoxColumn();  //0
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();    //1
            oChkA.HeaderText = "Confirm";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn();  //2
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.HeaderText = "Warehouse";
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn(); //3
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.HeaderText = "Purchase Order";
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxF = new DataGridViewTextBoxColumn(); //4
            oTxtBoxF.ReadOnly = true;
            oTxtBoxF.ValueType = typeof(int);
            oTxtBoxF.HeaderText = "Warehouse Pick Slip";
            dataGridView1.Columns.Add(oTxtBoxF);

            oTxtBoxD = new DataGridViewTextBoxColumn(); //5
            oTxtBoxD.ReadOnly = true;
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.HeaderText = "Box Number";
            dataGridView1.Columns.Add(oTxtBoxD);

            oTxtBoxE = new DataGridViewTextBoxColumn(); //6
            oTxtBoxE.ReadOnly = true;
            oTxtBoxE.ValueType = typeof(int);
            oTxtBoxE.HeaderText = "Boxed Quantity";
            dataGridView1.Columns.Add(oTxtBoxE);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void frmSalesPickListConfirmation_Load(object sender, EventArgs e)
        {
           FormLoaded = false;

           try
           {
              cmboPendingPickLists.DataSource = _context.TLCSV_OrderAllocated.Where(x => x.TLORDA_PickListPrint && !x.TLORDA_Delivered).ToList(); 
              cmboPendingPickLists.ValueMember = "TLORDA_Pk";
              cmboPendingPickLists.DisplayMember = "TLORDA_TransNumber";
              cmboPendingPickLists.SelectedIndex = -1;
           }
           catch (Exception ex)
           {
                MessageBox.Show(ex.Message);
                
           }

          FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lSave = false;
            DialogResult DResult;
            TLCSV_OrderAllocated AllocSelected = null;
            TLCSV_StockOnHand SOH = null;
            if (oBtn != null && FormLoaded)
            {
                AllocSelected = (TLCSV_OrderAllocated)cmboPendingPickLists.SelectedItem;

                if (AllocSelected == null)
                {
                   
                   MessageBox.Show("Please select a delivery Order");
                   return;
                   
                }

                foreach (DataGridViewRow Row in dataGridView1.Rows)
                {
                    if ((bool)Row.Cells[1].Value == true)
                        continue;

                    if (!lSave)
                    {
                        lSave = true;
                    }
                    //========================================
                    // Item NOT confirmed so therefore have to reset all the variables
                    // and return to stock!!!!!!!
                    //======================================================================
                    int Pk = (int)Row.Cells[0].Value;
                    SOH = _context.TLCSV_StockOnHand.Find(Pk);
                    if (SOH != null)
                    {
                        if (SOH.TLSOH_POOrderDetail_FK != null)
                        {
                            var POD = _context.TLCSV_PuchaseOrderDetail.Find(SOH.TLSOH_POOrderDetail_FK);
                            if (POD != null)
                            {
                                POD.TLCUSTO_Picked = false;
                                POD.TLCUSTO_PickedDate = null;
                                POD.TLCUSTO_QtyPicked_ToDate -= SOH.TLSOH_BoxedQty;
                                if(POD.TLCUSTO_QtyPicked_ToDate < 0)
                                {
                                    POD.TLCUSTO_QtyPicked_ToDate = 0;
                                }
                                POD.TLCUSTO_PickedNumber = 0;
                                POD.TLCUSTO_StockOnHand_FK = 0;
                                POD.TLCUSTO_Closed = false;
                            }
                        }

                        SOH.TLSOH_Picked = false;
                        SOH.TLSOH_PickListNo = 0;
                        SOH.TLSOH_PickListDate = null;
                        SOH.TLSOH_POOrder_FK = null;
                        SOH.TLSOH_POOrderDetail_FK = null;
                        SOH.TLSOH_WareHousePickList = 0;
                        SOH.TLSOH_Customer_Fk = null;
                    }
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                DResult = MessageBox.Show("Would you like to confirm this picking List", "Picking List Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (DResult == DialogResult.Yes)
                {
                    if(!lSave)
                    {
                       lSave = true;
                    }

                    AllocSelected.TLORDA_PLConfirmed = true;
                   /* var SOHList = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Picked && x.TLSOH_PickListNo == AllocSelected.TLORDA_TransNumber).GroupBy(x=>x.TLSOH_POOrderDetail_FK).ToList();
                    foreach (var Box in SOHList)
                    {
                        var QtyPicked = Box.Sum(x=>(int ?)x.TLSOH_BoxedQty) ?? 0;
                        if (QtyPicked != 0)
                        {
                            var PoDetail = _context.TLCSV_PuchaseOrderDetail.Find(Box.FirstOrDefault().TLSOH_POOrderDetail_FK);
                            if (PoDetail != null && QtyPicked != PoDetail.TLCUSTO_QtyPicked_ToDate)
                            {
                                if (!PoDetail.TLCUSTO_Picked)
                                {
                                    PoDetail.TLCUSTO_Picked = true;
                                    PoDetail.TLCUSTO_PickedDate = DateTime.Now;
                                }
                                PoDetail.TLCUSTO_QtyPicked_ToDate = QtyPicked;
                            }
                        }
                    }*/

                }
                
                //--------------------------------------------------
                //
                //-----------------------------------------------------------
                try
                {
                    _context.SaveChanges();
             
                    if (lSave)
                    {
                            MessageBox.Show("Data saved successfully to database");
                    }

                    frmSalesPickListConfirmation_Load(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    FormLoaded = false;
                    cmboPendingPickLists.SelectedIndex = -1;
                    FormLoaded = true;
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void cmboPendingPickLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null && FormLoaded)
            {
                var SelectedItem = (TLCSV_OrderAllocated)cmboPendingPickLists.SelectedItem;
                if (SelectedItem != null)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var OrderAllocated = context.TLCSV_OrderAllocated.Find(SelectedItem.TLORDA_Pk);
                        if (OrderAllocated != null)
                        {
                            var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == OrderAllocated.TLORDA_TransNumber).OrderBy(x => x.TLSOH_WareHouse_FK).ToList();
                            foreach (var Record in Existing)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Record.TLSOH_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = true;
                                dataGridView1.Rows[index].Cells[2].Value = context.TLADM_WhseStore.Find(Record.TLSOH_WareHouse_FK).WhStore_Description;
                                // dataGridView1.Rows[index].Cells[3].Value = PoNumber;
                                dataGridView1.Rows[index].Cells[4].Value = Record.TLSOH_WareHousePickList;
                                dataGridView1.Rows[index].Cells[5].Value = Record.TLSOH_BoxNumber;
                                dataGridView1.Rows[index].Cells[6].Value = Record.TLSOH_BoxedQty;
                            }
                        }
                    }
                }
            }
        }

        private void frmSalesPickListConfirmation_FormClosing(object sender, FormClosingEventArgs e)
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
