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
    public partial class frmProcessLossAcrossProd : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;
        PPSRepository repo;
        
        ProdQueryParameters QueryParms;
        Util core;

        public frmProcessLossAcrossProd()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            repo = new PPSRepository();


            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyle_CheckStateChanged);
            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboQuality_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);


        }

        private void fromProssesLossAcrossProd_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new ProdQueryParameters();

            var ExistingStyles = _context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
            foreach (var record in ExistingStyles)
            {
               this.cmboStyle.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Sty_Id, record.Sty_Description, false));
            }

            var ExistingColours = _context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
            foreach (var record in ExistingColours)
            {
                this.cmboColours.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
            }

            var ExistingQualities = _context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
            foreach (var record in ExistingQualities)
            {
               this.cmboQuality.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.TLGreige_Id, record.TLGreige_Description, false));
            }

            var ExistingSizes = _context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
            foreach (var record in ExistingSizes)
            {
                cmboSize.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.SI_id, record.SI_Description, false));
            }

            FormLoaded = true;
        }
        private void cmboStyle_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
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

        //-------------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //--------------------------------------------------------------------------------------
        private void cmboQuality_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Greiges.Add(repo.LoadGreige(item._Pk));

                }
                else
                {
                    var value = QueryParms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Greiges.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //--------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //---------------------------------------------------------------------------------------
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
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                frmPPSViewRep vRep = new frmPPSViewRep(10, QueryParms);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                this.cmboSize.Items.Clear();
                this.cmboStyle.Items.Clear();
                this.cmboQuality.Items.Clear();

                fromProssesLossAcrossProd_Load(this, null);

            }

        }
    }
  
}
