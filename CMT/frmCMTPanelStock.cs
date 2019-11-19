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
    public partial class frmCMTPanelStock : Form
    {
        bool formloaded;
        CMTQueryParameters QueryParms;
        CMTRepository repo;


        public frmCMTPanelStock()
        {
            InitializeComponent();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboDepartment.CheckStateChanged += new System.EventHandler(this.cmboDepartment_CheckStateChanged);
            this.comboStyles.CheckStateChanged += new EventHandler(this.cmboStyle_CheckStateChanged);
            this.comboColours.CheckStateChanged += new System.EventHandler(this.cmboColour_CheckStateChanged);
            this.comboSizes.CheckStateChanged += new System.EventHandler(this.cmboSize_CheckStateChanged);
        }

        private void frmCMTPanelStock_Load(object sender, EventArgs e)
        {
            formloaded = false;

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "CutSheet"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Style"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(4, "Style By Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(5, "CutSheet By Style By Colour"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;


            QueryParms = new CMTQueryParameters();
            repo = new CMTRepository();

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Record in Existing)
                {
                    cmboDepartment.Items.Add(new CMT.CheckComboBoxItem(Record.Dep_Id, Record.Dep_Description, false));
                 }

                var Styles = context.TLADM_Styles.ToList();
                foreach (var Record in Styles)
                {
                    comboStyles.Items.Add(new CMT.CheckComboBoxItem(Record.Sty_Id, Record.Sty_Description, false));
                }
                
                var Colours = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach(var Colour in Colours)
                {
                    comboColours.Items.Add(new CMT.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.ToList();
                foreach( var Size in Sizes)
                {
                    comboSizes.Items.Add(new CMT.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
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
        private void btnSubmit_Click(object sender, EventArgs e)
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
                        CMTRepOpts.PanelStockSortOrder = Convert.ToInt32(prop.GetValue(tst));
                    }
                }

                frmCMTViewRep vRep = new frmCMTViewRep(7, QueryParms, CMTRepOpts);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboDepartment.Items.Clear();
                comboColours.Items.Clear();
                comboSizes.Items.Clear();
                comboStyles.Items.Clear();

                frmCMTPanelStock_Load(this, null);
            }
        }

   
    }
}
