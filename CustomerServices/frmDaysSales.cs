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
    public partial class frmDaysSales : Form
    {
        bool FormLoaded;
        CustomerServices.Repository repo = null;
        CustomerServices.CustomerServicesParameters QueryParms = null;

        public frmDaysSales()
        {
            InitializeComponent();

            repo = new Repository(); 
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            
        }

        private void frmDaysSales_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CustomerServicesParameters();

            using ( var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.Where(x=>(bool)!x.Sty_Discontinued).OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x=>(bool)!x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                if (QueryParms.Styles.Count == 0)
                {
                    using (DialogCenteringService svcs = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a style from the dropdown box");
                        return;
                    }
                }

                if (QueryParms.Colours.Count == 0)
                {
                    using (DialogCenteringService svcs = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a colour from the dropdown box");
                        return;
                    }
                }

                CSVServices csvService = new CSVServices();

                frmCSViewRep vRep = new frmCSViewRep(26, QueryParms, csvService);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboColours.Items.Clear();
                cmboStyles.Items.Clear();
                
                frmDaysSales_Load(this, null);
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
