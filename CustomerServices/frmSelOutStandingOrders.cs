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
    public partial class frmSelOutStandingOrders : Form
    {
        Repository repo;

        Boolean FormLoaded;
        CustomerServicesParameters QueryParms;

        public frmSelOutStandingOrders()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCustomers.CheckStateChanged += new EventHandler(this.cmboCustomers_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            this.cmboMonths.CheckStateChanged += new System.EventHandler(this.cmboMonths_CheckStateChanged);
            repo = new Repository();
        }

        private void frmSelOutStandingOrders_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using ( var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.OrderBy(x=>x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => (bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));

                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }

                var Months = context.TLADM_Months.ToList();
                foreach (var Mnth in Months)
                {
                    cmboMonths.Items.Add(new CustomerServices.CheckComboBoxItem(Mnth.Mth_Pk, Mnth.Mth_Description, false));
                }
                               
            }

            QueryParms = new CustomerServicesParameters();
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {
            if(sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomers(item._Pk));

                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);
                }
            }
        }

        private void cmboMonths_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Months.Add(repo.LoadMonths(item._Pk));

                }
                else
                {
                    var value = QueryParms.Months.Find(it => it.Mth_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Months.Remove(value);
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
            }
        }

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
            frmCSViewRep vRep = null; 
            if (oBtn != null && FormLoaded)
            {
                CSVServices svcs = new CSVServices();
                if (QueryParms.Customers.Count == 0)
                {
                    QueryParms.IncludeAllCustomers = true;
                    vRep = new frmCSViewRep(30, QueryParms, svcs);
                }
                else
                {
                    vRep = new frmCSViewRep(17, QueryParms, svcs);
                }
                            
                
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                cmboColours.Items.Clear();
                cmboCustomers.Items.Clear();
                cmboStyles.Items.Clear();
                cmboSizes.Items.Clear();
                cmboMonths.Items.Clear();
                frmSelOutStandingOrders_Load(this, null);

            }
        }

       

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
