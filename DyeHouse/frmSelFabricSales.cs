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

    public partial class frmSelFabricSales : Form
    {
        protected readonly TTI2Entities _context;

        bool FormLoaded;
        Util core;

        DyeHouse.DyeRepository repo;
        DyeHouse.DyeQueryParameters QueryParms;

        public frmSelFabricSales()
        {
            InitializeComponent();
            repo = new DyeRepository();
            _context = new TTI2Entities();

            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);

        }

        
        private void frmSelFabricSales_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            QueryParms = new DyeQueryParameters();

            var Customers = _context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();

            foreach (var Customer in Customers)
            {
                cmboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
            }

            FormLoaded = true;
        }

        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomer(item._Pk));
                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23);
                
                frmDyeViewReport vRep = new frmDyeViewReport(36, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Dispose();
                }

                cmboCustomers.Items.Clear();
                frmSelFabricSales_Load(this, null);

            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                if (!oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }
    }
}
