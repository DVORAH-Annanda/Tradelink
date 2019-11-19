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
    public partial class frmCustomerTransHistory : Form
    {
        bool FormLoaded;
        CustomerServices.Repository repo;
        CustomerServices.CustomerServicesParameters QueryParms;

        bool Transactional = false;
        public frmCustomerTransHistory(bool Transact)
        {
            InitializeComponent();
            repo = new Repository();

            Transactional = Transact;

            if (Transactional)
                this.Text = "Tranactional Customer History";
            else
                this.Text = "Customer Audit History";
 

            this.cmboPurchaseOrders.CheckStateChanged += new System.EventHandler(this.cmboPurchaseOrders_CheckStateChanged);
        }

        private void frmCustomerTransHistory_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.Where(x=>!x.Cust_Blocked).OrderBy(x=>x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;
            }

            this.cmboPurchaseOrders.Items.Clear();

            QueryParms = new CustomerServicesParameters();

            FormLoaded = true;
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            IList<TLCSV_PurchaseOrder> PurchaseOrders = null; // new List<TLCSV_PurchaseOrder>();
            if (oCmbo != null && FormLoaded)
            {
                cmboPurchaseOrders.Items.Clear();
 
                var SelectedCustomer = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if (SelectedCustomer != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        PurchaseOrders = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == SelectedCustomer.Cust_Pk).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                                               
                        foreach (var PurchaseOrder in PurchaseOrders)
                        {
                            cmboPurchaseOrders.Items.Add( new CustomerServices.CheckComboBoxItem( PurchaseOrder.TLCSVPO_Pk, PurchaseOrder.TLCSVPO_PurchaseOrder, false));

                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboPurchaseOrders_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.PurchaseOrders.Add(repo.LoadPurchaseOrder(item._Pk));
                }
                else
                {
                    var value = QueryParms.PurchaseOrders.Find(it => it.TLCSVPO_Pk == item._Pk);
                    if (value != null)
                        QueryParms.PurchaseOrders.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            CSVServices services = new CSVServices();

            if (oBtn != null && FormLoaded)
            {
                if (txtPurchaseOrder.Text.Length > 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        var PurchaseOrder = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_PurchaseOrder == txtPurchaseOrder.Text).FirstOrDefault();
                        if (PurchaseOrder == null)
                        {
                            MessageBox.Show("Purchase Order Number not found");
                            return;
                        }

                        QueryParms.PurchaseOrders.Add(repo.LoadPurchaseOrder(PurchaseOrder.TLCSVPO_Pk));
                    }
                }
                else
                {
                    var SelectedCustomer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                    if (SelectedCustomer == null)
                    {
                        MessageBox.Show("Please select a customer from the drop down box");
                        return;
                    }

                    QueryParms.Customers.Add(repo.LoadCustomers(SelectedCustomer.Cust_Pk));
                }

                QueryParms.TransactHistory = Transactional;
 
                frmCSViewRep vRep = new frmCSViewRep(25, QueryParms, services);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            
                this.frmCustomerTransHistory_Load(this, null);
                this.txtPurchaseOrder.Text = string.Empty;
                        
            }
        }

        private void cmboPurchaseOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void radClosed_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            IList<TLCSV_PurchaseOrder> PurchaseOrders = null;
            if (oRad != null && FormLoaded && oRad.Checked)
            {
                TLADM_CustomerFile Customer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                if (Customer != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        FormLoaded = false;

                        cmboPurchaseOrders.DataSource = null;
                        cmboPurchaseOrders.Items.Clear();

                        QueryParms.PurchaseOrders.Clear();
                        PurchaseOrders = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Closeed && x.TLCSVPO_Customer_FK == Customer.Cust_Pk).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();

                        foreach (var PurchaseOrder in PurchaseOrders)
                        {
                            cmboPurchaseOrders.Items.Add(new CustomerServices.CheckComboBoxItem(PurchaseOrder.TLCSVPO_Pk, PurchaseOrder.TLCSVPO_PurchaseOrder, false));
                        }
                        FormLoaded = true;
                    }
                }
            }

        }

        private void radOpen_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            IList<TLCSV_PurchaseOrder> PurchaseOrders = null;
            if (oRad != null && FormLoaded && oRad.Checked)
            {
                TLADM_CustomerFile Customer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                if (Customer != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        FormLoaded = false;
                        
                        cmboPurchaseOrders.DataSource = null; 
                        cmboPurchaseOrders.Items.Clear();

                        QueryParms.PurchaseOrders.Clear();
                        PurchaseOrders = context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed && x.TLCSVPO_Customer_FK == Customer.Cust_Pk).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                        
                        foreach (var PurchaseOrder in PurchaseOrders)
                        {
                            cmboPurchaseOrders.Items.Add(new CustomerServices.CheckComboBoxItem(PurchaseOrder.TLCSVPO_Pk, PurchaseOrder.TLCSVPO_PurchaseOrder, false));
                        }
                        FormLoaded = true;
                    }
                }
            }
        }
    }
}
