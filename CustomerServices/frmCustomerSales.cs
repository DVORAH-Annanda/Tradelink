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
    public partial class frmCustomerSales : Form
    {

        Repository repo;
        CustomerServicesParameters CustParameters;
        bool formloaded;

        public frmCustomerSales()
        {
            InitializeComponent();
            repo = new Repository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            this.CmboWareHouses.CheckStateChanged += new System.EventHandler(this.CmboWareHouses_CheckStateChanged);
        }

        private void frmCustomerSales_Load(object sender, EventArgs e)
        {
            formloaded = false;

            CustParameters = new CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x=>!(bool)x.Col_Discontinued).OrderBy(x=>x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Display, false));
                }

                var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                foreach (var Whse in Whses)
                {
                    CmboWareHouses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                }
            }


            rbSummarisedByCustomer.Checked = false;
            rbSummarisedForCompany.Checked = false;
            rbRankedByStyle.Checked = false;
            rbRankedByStyleColor.Checked = false;
            rbRankedByStyleSize.Checked = false;

            formloaded = true;

        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                 CustParameters.Customers.Add(repo.LoadCustomers(item._Pk));

                }
                else
                {
                    var value = CustParameters.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        CustParameters.Customers.Remove(value);

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
                    CustParameters.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = CustParameters.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        CustParameters.Styles.Remove(value);

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
                    CustParameters.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value =CustParameters.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        CustParameters.Colours.Remove(value);

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
                    CustParameters.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = CustParameters.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        CustParameters.Sizes.Remove(value);

                }
            }
        }

        private void CmboWareHouses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    CustParameters.Whses.Add(repo.LoadWhse(item._Pk));

                }
                else
                {
                    var value = CustParameters.Whses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        CustParameters.Whses.Remove(value);

                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            CSVServices svces = new CSVServices();


            if (oBtn != null && formloaded)
            {
                DateTime fDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime tDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                tDate = tDate.AddHours(23);

                svces.fromDate = fDate;
                svces.toDate = tDate;
                svces.Title = "Customer sales analysis (full detail) for the period ";
                if (rbSummarisedByCustomer.Checked)
                {
                    svces.Title = "Customer sales analysis (summarised) for the period";
                    CustParameters.SummarisedSalesByCustomer = true;
                }
                else if (rbSummarisedForCompany.Checked)
                {
                    CustParameters.SummarisedSalesByCompany = true;
                    svces.Title = "Sales analysis (summarised for company) for the period";
                }
                else if (rbRankedByStyle.Checked)
                {
                    CustParameters.RankedByStyleColour = true;
                    svces.Title = "Sales analysis ranked by Style, Colour for the period";
                }
                else if (rbRankedByStyleColor.Checked)
                {
                    CustParameters.RankedByStyleColourSize = true;
                    svces.Title = "Sales analysis ranked by Style, Colour, Size for the period";
                }
                else if ( rbRankedByStyleSize.Checked)
                {
                    CustParameters.RankedByStyleSize = true;
                    svces.Title = "Sales analysis ranked by Style, Size for the period";
                }

                frmCSViewRep vRep = new frmCSViewRep(13, CustParameters, svces);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboColours.Items.Clear();
                cmboCustomers.Items.Clear();
                cmboSizes.Items.Clear();
                cmboStyles.Items.Clear();

                frmCustomerSales_Load(this, null);
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
    }
}
