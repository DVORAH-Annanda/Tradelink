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

namespace CMT
{
    public partial class frmCostingProfitability : Form
    {
        bool FormLoaded;
        CMT.CMTQueryParameters QueryParms;
        CMT.CMTRepository repo;

        public frmCostingProfitability()
        {
            InitializeComponent();
            repo = new CMT.CMTRepository();
            this.cmboCMTs.CheckStateChanged += new System.EventHandler(this.cmboCMTs_CheckStateChanged);
            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);           
        }

        private void frmCostingProfitability_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x=>x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                foreach (var Dept in Depts)
                {
                    cmboCMTs.Items.Add(new CMT.CheckComboBoxItem(Dept.Dep_Id, Dept.Dep_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new CMT.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new CMT.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CMT.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
            }
            QueryParms = new CMT.CMTQueryParameters();
            FormLoaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCMTs_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Depts.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Depts.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Depts.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
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

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
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

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
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
  
            if (oBtn != null && FormLoaded)
            {
                if (QueryParms.Depts.Count == 0)
                {
                    MessageBox.Show("Please select at least one CMT to process");
                    return;
                }

                var FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                frmCMTViewRep vRep = new frmCMTViewRep(25, QueryParms, false);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboCMTs.Items.Clear();
                cmboColour.Items.Clear();
                cmboSizes.Items.Clear();
                cmboStyle.Items.Clear();

                frmCostingProfitability_Load(this, null);

            }
        }
    }
}
