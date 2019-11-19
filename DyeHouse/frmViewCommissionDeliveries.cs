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

namespace DyeHouse
{
    public partial class frmViewCommissionDeliveries : Form
    {
        bool FormLoaded;

        DyeRepository repo;
        DyeQueryParameters DyeParameters;

        public frmViewCommissionDeliveries()
        {
            InitializeComponent();

            repo = new DyeRepository();

            this.comboColour.CheckStateChanged += new System.EventHandler(this.comboColour_CheckStateChanged);
            this.comboCustomers.CheckStateChanged += new EventHandler(this.comboCustomers_CheckStateChanged);
            this.comboQuality.CheckStateChanged += new System.EventHandler(this.comboQuality_CheckStateChanged);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                comboColour.Items.Clear();
                comboCustomers.Items.Clear();
                comboQuality.Items.Clear();

                frmViewCommissionDeliveries_Load(this, null);
            }
        }

        private void frmViewCommissionDeliveries_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            DyeParameters = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    comboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Qualitys = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Quality in Qualitys)
                {
                    comboQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    comboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboColour_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Colours.Add(repo.LoadColour(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Colours.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Customers.Add(repo.LoadCustomer(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        DyeParameters.Customers.Remove(value);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Qualities.Add(repo.LoadQuality(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Qualities.Remove(value);
                }
            }
        }
    }
}
