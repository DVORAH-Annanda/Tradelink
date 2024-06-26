﻿using System;
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
    public partial class frmSelSalesByPeriod : Form
    {
        bool FormLoaded;
        CustomerServices.Repository repo;
        CustomerServices.CustomerServicesParameters QueryParms;

        bool SalesByPeriod;
        public frmSelSalesByPeriod(bool SBP)
        {
            InitializeComponent();
            repo = new Repository();

            SalesByPeriod = SBP;

            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            
            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);
            
            if (SalesByPeriod)
            {
                this.Text = "Sales By Style By Period";
            }
            else
            {
                this.label4.Visible = false;
                this.cmboCustomers.Visible = false;
                this.Text = "Sales By Style By Customer";
            }

        }

        private void frmSelSalesByPeriod_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CustomerServicesParameters();
            
            using (var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach(var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_Description).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id,Size.SI_Display, false));
                }

                var Customers = context.TLADM_CustomerFile.OrderBy(x=>x.Cust_Description).ToList();
                 foreach (var Customer in Customers)
                 {
                     this.cmboCustomers.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                 }

                // cmboCustomers.DisplayMember = "Cust_Description";
	            // cmboCustomers.ValueMember = "Cust_PK";
	            
            }
            
            FormLoaded = true;

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
                    var value = QueryParms.Styles.Find(it => it.Sty_Id  == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);
                }

                // AS-20231103 ***** 
                LoadColoursBasedOnSelectedStyles();
                // AS-20231103 ***** 
            }
        }

        // AS-20231103 ***** 
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
        // AS-20231103 *****

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
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            frmCSViewRep vRep = null;

            if (oBtn != null && FormLoaded)
            {
                var csvService = new CSVServices();
                var FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;
                if(SalesByPeriod)
                    vRep = new frmCSViewRep(21, QueryParms, csvService);
                else
                    vRep = new frmCSViewRep(22, QueryParms, csvService);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
