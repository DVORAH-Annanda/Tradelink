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
    public partial class frmDetailCustomerOrders : Form
    {
        Repository repo;
        CustomerServicesParameters parms;
        bool formloaded;
        UserDetails UserDet;

        public frmDetailCustomerOrders(UserDetails UDetails)
        {
            InitializeComponent();
            UserDet = UDetails;

        }

        private void frmDetailCustomerOrders_Load(object sender, EventArgs e)
        {
            IList<TLADM_CustomerFile> CustFile = null;
            formloaded = false;
            
            repo = new Repository();
            parms = new  CustomerServicesParameters();
       

            var reportOptions = new BindingList<KeyValuePair<int, string>>();

            using (var context = new TTI2Entities())
            {
                if (UserDet._External == false)
                {
                    CustFile = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();

                    foreach (var Customer in CustFile)
                    {
                        comboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                    }
                }
                else
                {
                    var AccessPermitted = context.TLADM_CustomerAccess.Where(x => x.CustAcc_User_Fk == UserDet._UserPk).ToList();
                    foreach (var Access in AccessPermitted)
                    {
                        var CustDesc = context.TLADM_CustomerFile.Find(Access.CustAcc_Customer_Fk).Cust_Description; 
                        comboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Access.CustAcc_Customer_Fk, CustDesc, false));
                        
                    }
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    comboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    comboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Display, false));
                }


                rbProvisionalOrdersOnly.Checked = false;
                rbStdOrdersOnly.Checked = true;

            }

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------

            this.comboCustomers.CheckStateChanged += new System.EventHandler(this.comboCust_CheckStateChanged);
            this.comboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.comboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.comboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged); 

            formloaded = true;
  
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboCust_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Customers.Add(repo.LoadCustomers(item._Pk));

                }
                else
                {
                    var value = parms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        parms.Customers.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = parms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        parms.Styles.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = parms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        parms.Colours.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = parms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        parms.Sizes.Remove(value);

                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            CSVServices svcs = new CSVServices();
           
            Button oBtn = sender as Button;
            if(oBtn != null)
            {
                if (UserDet._External)
                {
                    if (parms.Customers.Count() == 0)
                    {
                        MessageBox.Show("Please select a Customer from the drop down list");
                        return;
                    }
                }

                if (rbProvisionalOrdersOnly.Checked)
                    parms.IncludeProvisional = true;


                parms.ClosedOrders = false;
                parms.AllPurchaseOrders = false;

                if (chkGroupByWeek.Checked)
                    parms.GroupByWeek = true;

                if (rbBoth.Checked)
                    parms.Both = true;

                frmCSViewRep VRep = new frmCSViewRep(8, parms, svcs);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                VRep.ClientSize = new Size(w, h);
                VRep.ShowDialog(this);
                if (VRep != null)
                {
                    VRep.Close();
                    VRep.Dispose();
                }
                comboCustomers.Items.Clear();
                comboColours.Items.Clear();
                comboSizes.Items.Clear();
                comboStyles.Items.Clear();

              
                frmDetailCustomerOrders_Load(this, null);
            }
        }

        private void cmboSelectedIndex_Changed(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;  
        }
    }
}
