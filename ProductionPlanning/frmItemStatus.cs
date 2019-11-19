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
    public partial class frmItemStatus : Form
    {
        bool FormLoaded;

        ProductionPlanning.ProdQueryParameters QueryParms;
        ProductionPlanning.PPSRepository repo;
        bool _ItemStatus = false;
        Util core;

        public frmItemStatus(bool ItemStatus )
        {
            InitializeComponent();

            if (ItemStatus)
            {
                this.Text = "Item Status Report";

                this.dtpFromDate.Visible = false;
                this.dtpToDate.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.label6.Visible = false;
                this.txtNoOf.Visible = false;

            }
            else
            {
                this.Text = "Sales Ranked in Style Colour Order";
                this.cmboColours.Enabled = false;
                this.cmboStyles.Enabled = false;
                this.cmboSizes.Enabled = false;

            }
            repo = new PPSRepository();

            _ItemStatus = ItemStatus;

            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);

        }

        private void frmItemStatus_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            core = new Util();

            using (var context = new TTI2Entities())
            {
                QueryParms = new ProductionPlanning.ProdQueryParameters();

                this.txtNoOf.Text = "0";

                this.txtNoOf.KeyDown += core.txtWin_KeyDownJI;
                this.txtNoOf.KeyPress += core.txtWin_KeyPress;
 
                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new ProductionPlanning.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new ProductionPlanning.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }

            }
            FormLoaded = true;
        }

        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));
                    using (var context = new TTI2Entities())
                    {
                        cmboColours.Items.Clear();

                        var Colours = context.TLPPS_Replenishment.Where(x => x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued).GroupBy(x=>x.TLREP_Colour_FK).ToList();
                        foreach (var Col in Colours)
                        {
                            var Clr_Pk = Col.FirstOrDefault().TLREP_Colour_FK;
                            var Clr = context.TLADM_Colours.Find(Clr_Pk);
                            if (Clr != null)
                            {
                                cmboColours.Items.Add(new  ProductionPlanning.CheckComboBoxItem(Clr.Col_Id, Clr.Col_Display, false));
                            }
                        }
                    }
                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                    if (QueryParms.Styles.Count == 0)
                    {
                        cmboColours.Items.Clear();
                        using (var context = new TTI2Entities())
                        {
                            var Colours = context.TLADM_Colours.Where(x=>!(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                            foreach (var Colour in Colours)
                            {
                                cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                            }
                        }
                    }
                }
            }
        }

        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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

            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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
            frmPPSViewRep vRep = null;

            if (oBtn != null && FormLoaded)
            {
                if (_ItemStatus)
                {
                    if (QueryParms.Styles.Count == 0)
                    {
                        MessageBox.Show("Please select a style / styles from the drop down box");
                        return;
                    }
                    if (QueryParms.Colours.Count == 0)
                    {
                        MessageBox.Show("Please select a colour / colours from the drop down box");
                        return;
                    }
                    if (QueryParms.Sizes.Count == 0)
                    {
                        MessageBox.Show("Please select a size / sizes from the drop down box");
                        return;
                    }
                }
               
                if (_ItemStatus)
                    vRep = new frmPPSViewRep(4, QueryParms);
                else
                {
                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    ToDate = ToDate.AddHours(23);

                    QueryParms.TopSellers = Convert.ToInt32(txtNoOf.Text);
                    if (QueryParms.TopSellers == 0)
                        QueryParms.TopSellers = 5;

                    QueryParms.FromDate = FromDate;
                    QueryParms.ToDate = ToDate;

                    vRep = new frmPPSViewRep(5, QueryParms);
                }

               
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                
                cmboColours.Items.Clear();
                cmboSizes.Items.Clear();
                cmboStyles.Items.Clear();


                frmItemStatus_Load(this, null);
            }
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
