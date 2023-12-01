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

            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            this.cmboPurchaseOrders.CheckStateChanged += new System.EventHandler(this.cmboPurchaseOrders_CheckStateChanged);
        }

        private void frmCustomerTransHistory_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.Where(x => !x.Cust_Blocked).OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

                this.cmboPurchaseOrders.Items.Clear();

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
            }
            
            QueryParms = new CustomerServicesParameters();

            if(!Transactional)
            {
                cmboColours.Visible = false;
                label3.Visible = false;
                cmboSizes.Visible = false;
                label4.Visible = false;
                cmboStyles.Visible = false;
                label5.Visible = false;
            }

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

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                }

                //???2023/10/27
                // Clear and reload the cmboColours based on the selected styles
                LoadColoursBasedOnSelectedStyles();
                //???2023/10/27
            }
        }


        /// //???2023/11/03
        private void LoadColoursBasedOnSelectedStyles()
        {
            using (var context = new TTI2Entities())
            {
                // Get the selected style IDs
                var selectedStyleIds = QueryParms.Styles.Select(style => style.Sty_Id).ToList();

                // Query the bridge table to get associated color IDs for the selected styles
                var colorIds = context.TLADM_StyleColour
                    .Where(sc => selectedStyleIds.Contains(sc.STYCOL_Style_FK))
                    .Select(sc => sc.STYCOL_Colour_FK)
                    .ToList();

                // Get the colors based on the retrieved color IDs
                var colors = context.TLADM_Colours
                    .Where(c => !(bool)c.Col_Discontinued)
                    .Where(c => colorIds.Contains(c.Col_Id))
                    .OrderBy(c => c.Col_Display)
                    .ToList();

                // Clear the ComboBox
                cmboColours.Items.Clear();

                // Populate the ComboBox with the filtered colors
                foreach (var color in colors)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(color.Col_Id, color.Col_Display, false));
                }
            }
        }
        /// //???2023/11/03 


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = QueryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        QueryParms.Colours.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = QueryParms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        QueryParms.Sizes.Remove(value);

                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            CSVServices services = new CSVServices();

            if (oBtn != null && FormLoaded)
            {
                var SelectedCustomer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                if (SelectedCustomer == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select a customer from the drop down box");
                        return;
                    }
                }

                if (QueryParms.PurchaseOrders.Count() == 0)
                {
                        MessageBox.Show("Please select a customer purchase order from the drop down box");
                        return;
                }
                
                             
                QueryParms.Customers.Add(repo.LoadCustomers(SelectedCustomer.Cust_Pk));
                QueryParms.TransactHistory = Transactional;

                frmCSViewRep vRep = new frmCSViewRep(25, QueryParms, services);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            
                this.frmCustomerTransHistory_Load(this, null);
                
                        
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
