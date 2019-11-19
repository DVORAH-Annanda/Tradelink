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

namespace ProductionPlanning
{
    public partial class frmSelReorderDetails : Form
    {
        bool formloaded;
        PPSRepository repo = null;
        ProdQueryParameters parms = null;

        public frmSelReorderDetails()
        {
            InitializeComponent();
        }

        private void frmSelReorderDetails_Load(object sender, EventArgs e)
        {
            formloaded = false;
            repo = new PPSRepository();
            parms = new ProdQueryParameters();
            
            using (var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmboCustomers.Items.Add(new ProductionPlanning.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Styles = context.TLADM_Styles.ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new ProductionPlanning.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new ProductionPlanning.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
            }

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomer_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
             

            formloaded = true;
           
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomer_CheckStateChanged(object sender, EventArgs e)
        {

            
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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
            Button oBtn = sender as Button;
           
            if (oBtn != null && formloaded)

            {
                frmPPSViewRep vRep = new frmPPSViewRep(1, parms);
                vRep.ShowDialog(this);

            }
        }
    }
}
