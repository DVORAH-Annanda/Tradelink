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
    public partial class frmDyeOrdersSelection : Form
    {
        bool FormLoaded;

        DyeHouse.DyeQueryParameters QueryParms;
        DyeHouse.DyeRepository repo;

        public frmDyeOrdersSelection()
        {
            InitializeComponent();
            repo = new DyeRepository();

            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);
        }

        private void frmDyeOrdersSelection_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                FormLoaded = false;
                QueryParms = new DyeQueryParameters();
                FormLoaded = true;

                var Grieges = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Greige in Grieges)
                {
                    cmboGreige.Items.Add(new DyeHouse.CheckComboBoxItem(Greige.TLGreige_Id, Greige.TLGreige_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new DyeHouse.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Customers = context.TLADM_CustomerFile.Where(x=>!x.Cust_Blocked).OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }
            }
        }

        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Qualities.Add(repo.LoadQuality(item._Pk));

                }
                else
                {
                    var value = QueryParms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Qualities.Remove(value);

                }
            }
        }

        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
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

        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
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
            
            try
            {
                frmDyeViewReport vRep = new frmDyeViewReport(10, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmboColour.Items.Clear();
                cmboCustomers.Items.Clear();
                cmboGreige.Items.Clear();
                cmboStyle.Items.Clear();

                this.frmDyeOrdersSelection_Load(this, null);
            }

        }
    }
}
