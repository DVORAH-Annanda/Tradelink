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
    public partial class frmCMTProdAnalysis : Form
    {
        CMTRepository repo;
        CMTQueryParameters QueryParms;

        CMTReportOptions repOpts;

        bool FormLoaded;

        public frmCMTProdAnalysis()
        {
            InitializeComponent();
            repo = new CMTRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            
        }

        private void frmCMTProdAnalysis_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                QueryParms = new CMTQueryParameters();

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CMT.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                //???2023/10/27
                var Colours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CMT.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
                //???2023/10/27

            }

            FormLoaded = true;
        }


        /// //???2023/10/27
        private void LoadColoursBasedOnSelectedStyles()
        {
            using (var context = new TTI2Entities())
            {
                // Get the selected style IDs
                var selectedStyleIds = QueryParms.Styles.Select(style => style.Sty_Id).ToList();

                // Query the bridge table to get associated color IDs for the selected styles
                var colorIds = context.TLADM_StyleColour
                    .Where(sc => selectedStyleIds.Contains(sc.STYCOL_Style_FK))
                    .Select(sc => sc.STYCOL_Colour_FK)
                    .ToList();

                // Get the colors based on the retrieved color IDs
                var colors = context.TLADM_Colours
                    .Where(c => !(bool)c.Col_Discontinued)
                    .Where(c => colorIds.Contains(c.Col_Id))
                    .OrderBy(c => c.Col_Display)
                    .ToList();

                // Clear the ComboBox
                cmboColours.Items.Clear();

                // Populate the ComboBox with the filtered colors
                foreach (var color in colors)
                {
                    cmboColours.Items.Add(new CMT.CheckComboBoxItem(color.Col_Id, color.Col_Display, false));
                }
            }
        }
        /// //???2023/10/27


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the Styles combo box
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

                //???2023/10/27
                // Clear and reload the cmboColours based on the selected styles
                LoadColoursBasedOnSelectedStyles();
                //???2023/10/27
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                repOpts = new CMTReportOptions();
                QueryParms.FromDate = Convert.ToDateTime(FromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(ToDate.Value.ToShortDateString()).AddHours(23) ;

                frmCMTViewRep vRep = new frmCMTViewRep(33, QueryParms, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                cmboColours.Items.Clear();
                cmboStyles.Items.Clear();

                frmCMTProdAnalysis_Load(this, null);
                
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
