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

namespace Cutting
{
    public partial class frmSelCutProduction : Form
    {
        Util core;
        int RepSortOption;
        bool formloaded;

        CutReportOptions repOptions;

        Cutting.CuttingRepository repo;
        Cutting.CuttingQueryParameters QueryParms;

        public frmSelCutProduction()
        {
            InitializeComponent();
            repo = new CuttingRepository();

            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);

          
        }

        private void frmSelCutProduction_Load(object sender, EventArgs e)
        {
            formloaded = false;
            core = new Util();
            RepSortOption = 1;

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Machine Code"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Style"));
            reportOptions.Add(new KeyValuePair<int, string>(4, "Quality"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            repOptions = new CutReportOptions();

            QueryParms = new CuttingQueryParameters();

            formloaded = true;

            using (var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new Cutting.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new Cutting.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
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
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
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
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                   
                    if (chkQAReport.Checked)
                        repOptions.QAReport = true;

                    repOptions.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    repOptions.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    repOptions.toDate = repOptions.toDate.AddHours(23);
                    repOptions.C4SortOption = RepSortOption;

                    QueryParms.FromDate = repOptions.fromDate;
                    QueryParms.ToDate = repOptions.toDate;

                    frmCutViewRep vRep = new frmCutViewRep(5, repOptions, QueryParms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                   

                    cmboStyles.Items.Clear();
                    cmboColours.Items.Clear();

                    frmSelCutProduction_Load(this, null);

                }
            }
        }

        private void cmboReportOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                RepSortOption = Convert.ToInt32(oCmbo.SelectedValue);
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
    }
}
