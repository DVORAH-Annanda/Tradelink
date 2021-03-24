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
    public partial class frmStockOnHand : Form
    {
        bool formloaded;
        int _OptionNo;

        Repository repo;
        CustomerServicesParameters QueryParms;
        UserDetails _Ud; 

        public frmStockOnHand( int OptionNo, UserDetails Ud)
        {
            InitializeComponent();
            _OptionNo = OptionNo;
            _Ud = Ud;
        }

        public frmStockOnHand(int OptionNo)
        {
            InitializeComponent();
            _OptionNo = OptionNo;
            _Ud = null;
        }

        private void frmStockOnHand_Load(object sender, EventArgs e)
        {
            IList<TLADM_WhseStore> Whses = null;

            formloaded = false;

            repo = new Repository();
            QueryParms = new CustomerServicesParameters();

            if (_OptionNo == 1)
                this.Text = "Stock quantities on hand";
            else
                this.Text = "Boxes in stock";

            rbGradeA.Checked = true;

            using (var context = new TTI2Entities())
            {
                if (_Ud == null || !_Ud._External)
                {
                    Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA).ToList();
                }
                else
                {
                    Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA && !x.WhStore_RePack).ToList();
                }

                foreach (var Whse in Whses)
                {
                    comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    comboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    comboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
                //--------------------------------------------------------
                // wire up the check state changed event
                //--------------------------------------------------------------------------------------------------------
                this.comboWhses.CheckStateChanged += new System.EventHandler(this.cmboWhses_CheckStateChanged);
                this.comboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
                this.comboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
                this.comboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            }
            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWhses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Whses.Add(repo.LoadWhse(item._Pk));

                }
                else
                {
                    var value = QueryParms.Whses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        QueryParms.Whses.Remove(value);

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
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));
                    using (var context = new TTI2Entities())
                    {
                        comboColours.Items.Clear();
                        var Colours = context.TLPPS_Replenishment.Where(x=>x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued).GroupBy(x=>x.TLREP_Colour_FK).ToList();
                        foreach (var Col in Colours)
                        {
                            var Clr_Pk = Col.FirstOrDefault().TLREP_Colour_FK;
                            var Clr = context.TLADM_Colours.Find(Clr_Pk);
                            if (Clr != null)
                            {
                                comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Clr.Col_Id, Clr.Col_Display, false));
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
                        comboColours.Items.Clear();
                        using (var context = new TTI2Entities())
                        {
                            var Colours = context.TLADM_Colours.Where(x=>!(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                            foreach (var Colour in Colours)
                            {
                                comboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                            }
                        }
                    }

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

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
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
        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
     
            int repNo = 0;
            if (_OptionNo == 1)
                repNo = 6;
            else
                repNo = 7;

            if (oBtn != null)
            {
               

                CSVServices csvService = new CSVServices();
                if (RBStockOH.Checked)
                    csvService.SOHClassification = true;
                else if (RBStockAvail.Checked)
                    csvService.SOHClassification = false;
                else if (rbBoxesReturned.Checked)
                    csvService.SOHBoxReturned = true;
                else
                    csvService.SplitBoxOnly = true;


                csvService.DateIntoStock = dtpDateinStock.Value;
                
                if (rbGradeA.Checked)
                    QueryParms.GradeA = true;
                else
                    QueryParms.GradeA = false;

                if (rbDiscontinued.Checked)
                    QueryParms.Discontinued = true;
 
                frmCSViewRep vRep = new frmCSViewRep(repNo, QueryParms, csvService);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if(vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                comboColours.Items.Clear();
                comboSizes.Items.Clear();
                comboStyles.Items.Clear();
                comboWhses.Items.Clear();

                frmStockOnHand_Load(this, null);
                
            }
        }

        private void rbOtherGrades_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && !x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }

        private void rbGradeA_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }

        private void comboWhses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown) 
                oCmbo.DroppedDown = true;
        }

        private void comboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void rbDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    comboWhses.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && !x.WhStore_GradeA).ToList();
                        foreach (var Whse in Whses)
                        {
                            comboWhses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                        }
                    }
                }
            }
        }
    }
}
