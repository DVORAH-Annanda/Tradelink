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

namespace CMT
{
    public partial class frmCMTFinishedWAnalysis : Form
    {
        bool formloaded;
        CMTQueryParameters QueryParms;
        CMTRepository repo;
        Util core;
        public frmCMTFinishedWAnalysis()
        {
            InitializeComponent();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboFactory.CheckStateChanged += new System.EventHandler(this.cmboDepartment_CheckStateChanged);
            this.cmboStyle.CheckStateChanged += new EventHandler(this.cmboStyle_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColour_CheckStateChanged);
            this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSize_CheckStateChanged);
            repo = new CMTRepository();
            core = new Util();

            txtPercentage.KeyDown += core.txtWin_KeyDownJI;
            txtPercentage.KeyPress += core.txtWin_KeyPress;

        }

        private void frmCMTFinishedWAnalysis_Load(object sender, EventArgs e)
        {
            var reportOptions = new BindingList<KeyValuePair<int, string>>();

            formloaded = false;

            QueryParms = new CMTQueryParameters();

            reportOptions.Add(new KeyValuePair<int, string>(1, "Cut Sheet"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Style"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "CMT Line"));
            reportOptions.Add(new KeyValuePair<int, string>(4, "Style By Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(5, "Style By Size"));

            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember =  "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            chkValueBySize.Checked = false;
            
            using (var context = new TTI2Entities())
            {
                var Factories = context.TLADM_Departments.Where(x => x.Dep_IsCMT).OrderBy(x=>x.Dep_Description).ToList();
                foreach (var Factory in Factories)
                {
                    cmboFactory.Items.Add(new CMT.CheckComboBoxItem(Factory.Dep_Id, Factory.Dep_Description, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new CMT.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x => (bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new CMT.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSize.Items.Add(new CMT.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }

            }
            formloaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDepartment_CheckStateChanged(object sender, EventArgs e)
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
        private void cmboStyle_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                using (var context = new TTI2Entities())
                {
                    if (item.CheckState)
                    {
                        if (QueryParms.Styles.Count == 0)
                        {
                            // Clear the color combo if this is the first style selected
                            cmboColour.Items.Clear();
                        }

                        // Add selected style to QueryParms
                        QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                        // Get colors associated with the selected style
                        var coloursForSelectedStyle = context.TLPPS_Replenishment
                            .Where(x => x.TLREP_Style_FK == item._Pk && !x.TLREP_Discontinued)
                            .GroupBy(x => x.TLREP_Colour_FK)
                            .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                            .ToList();

                        // Loop through the colours for the selected style
                        foreach (var colourPk in coloursForSelectedStyle)
                        {
                            var clr = context.TLADM_Colours.Find(colourPk);
                            if (clr != null && !cmboColour.Items.Cast<CMT.CheckComboBoxItem>()
                                .Any(c => c._Pk == clr.Col_Id)) // Avoid duplicates
                            {
                                cmboColour.Items.Add(new CMT.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                            }
                        }
                    }
                    else
                    {
                        // Remove the deselected style from QueryParms
                        var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                        if (value != null)
                            QueryParms.Styles.Remove(value);

                        // If no styles are selected, reset the combo box to show all available colors
                        if (QueryParms.Styles.Count == 0)
                        {
                            cmboColour.Items.Clear();
                            var allColours = context.TLADM_Colours
                                .Where(x => !x.Col_Discontinued ?? false)
                                .OrderBy(x => x.Col_Display)
                                .ToList();

                            foreach (var colour in allColours)
                            {
                                cmboColour.Items.Add(new CMT.CheckComboBoxItem(colour.Col_Id, colour.Col_Display, false));
                            }
                        }
                        else
                        {
                            // If there are still styles selected, re-filter the colours to reflect the remaining selected styles
                            cmboColour.Items.Clear();

                            var selectedStyleIds = QueryParms.Styles.Select(s => s.Sty_Id).ToList();
                            var allSelectedColours = context.TLPPS_Replenishment
                                .Where(x => selectedStyleIds.Contains(x.TLREP_Style_FK) && !x.TLREP_Discontinued)
                                .GroupBy(x => x.TLREP_Colour_FK)
                                .Select(g => g.FirstOrDefault().TLREP_Colour_FK)
                                .Distinct()
                                .ToList();

                            foreach (var colourPk in allSelectedColours)
                            {
                                var clr = context.TLADM_Colours.Find(colourPk);
                                if (clr != null)
                                {
                                    cmboColour.Items.Add(new CMT.CheckComboBoxItem(clr.Col_Id, clr.Col_Display, false));
                                }
                            }
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColour_CheckStateChanged(object sender, EventArgs e)
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
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSize_CheckStateChanged(object sender, EventArgs e)
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

        private void Submit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                CMTReportOptions CMTRepOpts = new CMTReportOptions();

                Object tst = cmboReportOptions.SelectedItem;
                if (tst == null)
                {
                    MessageBox.Show("Please select a report option");
                    return;
                }

                foreach (PropertyInfo prop in tst.GetType().GetProperties())
                {
                    if (prop.Name == "Key")
                    {
                            CMTRepOpts.WorkAnalysis = Convert.ToInt32(prop.GetValue(tst));
                    }
                }
                
              
                try
                {
                    if (chkQaSummary.Checked)
                        CMTRepOpts.QAReport = true;

                    if (chkException.Checked)
                    {
                        CMTRepOpts.Exception = true;
                        CMTRepOpts.Percentage_Exception = Convert.ToInt32(txtPercentage.Text);
                    }

                    CMTRepOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    CMTRepOpts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    CMTRepOpts.toDate = CMTRepOpts.toDate.AddHours(23);
                    frmCMTViewRep vRep = null;
                    if (!chkValueBySize.Checked)
                    {
                        vRep = new frmCMTViewRep(20, QueryParms, CMTRepOpts);
                    }
                    else
                    {
                        vRep = new frmCMTViewRep(36, QueryParms, CMTRepOpts);
                    }
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    if (chkException.Checked)
                    {
                        chkException.Checked = false;
                        txtPercentage.Text = "0";
                    }

                }
                finally
                {
                    cmboFactory.Items.Clear();
                    cmboSize.Items.Clear();
                    cmboStyle.Items.Clear();
                    cmboColour.Items.Clear();

                    frmCMTFinishedWAnalysis_Load(this, null);
                }
            }
        }

        private void cmboFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboStyle_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cmboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void chkException_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;

            if (oChk != null && oChk.Checked)
            {
                txtPercentage.Text = "5";
            }
            else
                txtPercentage.Text = "0";
        }
    }
}
