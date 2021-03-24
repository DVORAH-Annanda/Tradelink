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
    public partial class frmProdByPeriodSel : Form
    {
        bool formLoaded;
        int ReportSort;
        bool _Prod;
        bool formloaded;

        CMTQueryParameters QueryParms;
        CMTRepository repo;

        public frmProdByPeriodSel(bool Production)
        {
            InitializeComponent();
            _Prod = Production;
            
            if (_Prod)
                this.Text = "Production Report By Period";
            else
                this.Text = "Stock on hand in despatch cage (CMT Finished Goods Store";

        }

        private void ProdByPeriodSel_Load(object sender, EventArgs e)
        {
            formloaded = false;

            ReportSort = 0;

            QueryParms = new CMTQueryParameters();
            repo = new CMTRepository();

            using (var context = new TTI2Entities())
            {
                var Factories = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Factory in Factories)
                {
                    cmboFactory.Items.Add(new CheckComboBoxItem(Factory.Dep_Id, Factory.Dep_Description, false));
                }
                
                var Qualities = context.TLADM_Styles.ToList();
                foreach(var Quality in Qualities)
                {
                    cmboQuality.Items.Add(new CheckComboBoxItem(Quality.Sty_Id, Quality.Sty_Description, false));
                }

                               
                if (_Prod)
                {
                    var Sizes = context.TLADM_Sizes.ToList();
                    foreach(var Size in Sizes)
                    {
                        cmboSize.Items.Add(new CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                    }

                    groupBox1.Visible = true;
                    
                }
                else
                {
                    cmboSize.Visible = false;
                    label4.Visible = false;
                    groupBox1.Visible = false;
                }

                var Colours = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach(var Colour in Colours)
                {
                    cmboColour.Items.Add(new CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
            }

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            if (_Prod)
            {
                reportOptions.Add(new KeyValuePair<int, string>(1, "Line"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Style"));
            }
            else
            {
                reportOptions.Add(new KeyValuePair<int, string>(4, "CMT"));
             }

            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboFactory.CheckStateChanged += new System.EventHandler(this.cmboFactorys_CheckStateChanged);
            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboQualities_CheckStateChanged);
            this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
      

            formloaded = true;

        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboFactorys_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
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
        private void cmboQualities_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
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
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
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

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
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
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            frmCMTViewRep vRep;
            if (oBtn != null && formloaded)
            {
                CMTReportOptions repOpts = new CMTReportOptions();
                repOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOpts.toDate = repOpts.toDate.AddHours(23);

                if (rbNoOfGarments.Checked)
                    repOpts.NoOfGarments = true;
                else
                    repOpts.NoOfBoxes = true;

                repOpts.SortSequence = ReportSort; // ReportSort;
                if (_Prod)
                {
                    vRep = new frmCMTViewRep(12,QueryParms, repOpts);
                }
                else
                {
                    vRep = new frmCMTViewRep(13, QueryParms, repOpts);
                }

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                ReportSort = 0;
                
                formloaded = false;
                
                cmboSize.SelectedValue = -1;
                cmboFactory.SelectedValue = -1;
                cmboQuality.SelectedValue = -1;
                cmboReportOptions.SelectedValue = -1;

                formloaded = true;
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                ReportSort = (int)oCmbo.SelectedValue;
            }
        }

        private void cmboFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
