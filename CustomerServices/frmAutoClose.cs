using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
namespace CustomerServices
{
    public partial class frmAutoClose : Form
    {
        bool formloaded = false;
        TTI2Entities _context;
        Repository repo;
        CustomerServicesParameters CustParameters;
        public frmAutoClose()
        {
            InitializeComponent();
        ;
            CustParameters = new CustomerServicesParameters();

            this.cmboPurchaseOrders.CheckStateChanged += new System.EventHandler(this.cmboPurchaseOrders_CheckStateChanged);
        }

        private void frrmAutoClose_Load(object sender, EventArgs e)
        {
            formloaded = false;

            _context = new TTI2Entities();

            repo = new Repository();
            cmboPurchaseOrders.Items.Clear();
                
            cmboCustomeres.DataSource = _context.TLADM_CustomerFile.Where(x=>!x.Cust_Blocked && !x.Cust_FabricCustomer).OrderBy(x=>x.Cust_Description).ToList();
            cmboCustomeres.ValueMember = "Cust_Pk";
            cmboCustomeres.DisplayMember = "Cust_Description";
            cmboCustomeres.SelectedIndex = -1;

            formloaded = true;


        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboPurchaseOrders_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    CustParameters.PurchaseOrders.Add(repo.LoadPurchaseOrder(item._Pk));

                }
                else
                {
                    var value = CustParameters.PurchaseOrders.Find(it => it.TLCSVPO_Pk == item._Pk);
                    if (value != null)
                        CustParameters.PurchaseOrders.Remove(value);

                }
            }
        }
       
        private void cmboCustomeres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(formloaded)
            {
                var oDlg = sender as ComboBox;
                if (oDlg != null)
                {
                    var ItemSelected = (TLADM_CustomerFile)oDlg.SelectedItem;
                    var CPO = _context.TLCSV_PurchaseOrder.Where(x=>x.TLCSVPO_Customer_FK == ItemSelected.Cust_Pk && !x.TLCSVPO_Closeed).ToList();
                    foreach (var Ord in CPO)
                    {
                        cmboPurchaseOrders.Items.Add(new CustomerServices.CheckComboBoxItem (Ord.TLCSVPO_Pk, Ord.TLCSVPO_PurchaseOrder, false));
                    }
                }
            }
        }

        private void frrmAutoClose_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_context != null)
            {
                _context.Dispose();
            }    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (formloaded)
            {
                DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (CustParameters.PurchaseOrders.Count == 0)
                    {
                        MessageBox.Show("Plesae select a Purchase Order to process");
                        return;
                    }

                    foreach (var item in CustParameters.PurchaseOrders)
                    {
                        var OrderDet = _context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == x.TLCUSTO_Pk && !x.TLCUSTO_Closed).ToList();
                        foreach (var OrderDetail in OrderDet)
                        {
                            OrderDetail.TLCUSTO_Closed = true;
                        }

                        var Po = _context.TLCSV_PurchaseOrder.Find(item.TLCSVPO_Pk);
                        if(Po != null)
                        {
                            Po.TLCSVPO_Closeed = true;
                            Po.TLCSVPO_ClosedDate = DateTime.Now;
                            Po.TLCSVPO_ClosedBy = "Transaction";
                        }
                    }
                        

                     try
                     {
                        _context.SaveChanges();
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("Transaction successfully completed");
                        }
                        frrmAutoClose_Load(this, null);


                     }
                     catch (Exception ex)
                     {
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show(ex.Message);
                        }
                     }
                    
                }
                
            }
        }
    }
}
