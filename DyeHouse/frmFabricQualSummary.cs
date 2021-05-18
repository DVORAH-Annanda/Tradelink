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

namespace DyeHouse
{
    public partial class frmFabricQualSummary : Form
    {
        bool FormLoaded;
        public frmFabricQualSummary()
        {
            InitializeComponent();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            // this.cmboFactory.CheckStateChanged += new System.EventHandler(this.cmboFactorys_CheckStateChanged);
            // this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboQualities_CheckStateChanged);
            // this.cmboSize.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
            // this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
        }

        private void frmFabricQualSummary_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using ( var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColours.Items.Add(new CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.OrderBy(x=>x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }

                var Qualities = context.TLADM_Griege.OrderBy(x=>x.TLGreige_Description).ToList();
                foreach (var Quality in Qualities)
                {
                    cmboQualities.Items.Add(new CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }
            }
            FormLoaded = true;

        }
    }
}
