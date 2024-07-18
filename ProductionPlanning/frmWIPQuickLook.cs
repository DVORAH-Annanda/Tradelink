using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;


namespace ProductionPlanning
{
    public partial class frmWIPQuickLook : Form
    {
        bool formloaded;
        PPSRepository repo;        
        ProdQueryParameters QueryParms;
        Util core;
        public frmWIPQuickLook()
        {
            InitializeComponent();
            repo = new PPSRepository();
            core = new Util();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.chkcboCMTSelection.CheckStateChanged += new System.EventHandler(this.chkcboCMTSelection_CheckStateChanged);
            this.chkcboStylesSelection.CheckStateChanged += new System.EventHandler(this.chkcboStylesSelection_CheckStateChanged);
            this.chkcboColoursSelection.CheckStateChanged += new System.EventHandler(this.chkcboColoursSelection_CheckStateChanged);
            this.chkcboSizesSelection.CheckStateChanged += new System.EventHandler(this.chkcboSizesSelection_CheckStateChanged);
        }

        private void frmWIPQuickLook_Load(object sender, EventArgs e)
        {
            formloaded = false;

            QueryParms = new ProdQueryParameters();
            using (var context = new TTI2Entities())
            {
                var Departments = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Record in Departments)
                {
                    chkcboCMTSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Dep_Id, Record.Dep_Description, false));
                }

                var Styles = context.TLADM_Styles.ToList();
                foreach (var Record in Styles)
                {
                    chkcboStylesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(Record.Sty_Id, Record.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var record in Colours)
                {
                    chkcboColoursSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.Col_Id, record.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var record in Sizes)
                {
                    chkcboSizesSelection.Items.Add(new ProductionPlanning.CheckComboBoxItem(record.SI_id, record.SI_Description, false));
                }
                

            }
            formloaded = true;

        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void chkcboCMTSelection_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Departments.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Departments.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Departments.Remove(value);

                }
            }
        }
        private void chkcboStylesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
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
        private void chkcboColoursSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
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
        private void chkcboSizesSelection_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is ProductionPlanning.CheckComboBoxItem && formloaded)
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
            if (oBtn != null)
            {

                //frmCMTViewRep vRep = new frmCMTViewRep(7, QueryParms);

                //int h = Screen.PrimaryScreen.WorkingArea.Height;
                //int w = Screen.PrimaryScreen.WorkingArea.Width;
                //vRep.ClientSize = new Size(w, h);
                //vRep.ShowDialog(this);

                chkcboCMTSelection.Items.Clear();
                chkcboStylesSelection.Items.Clear();
                chkcboColoursSelection.Items.Clear();
                chkcboSizesSelection.Items.Clear();

                frmWIPQuickLook_Load(this, null);
            }
        }
    }
}
