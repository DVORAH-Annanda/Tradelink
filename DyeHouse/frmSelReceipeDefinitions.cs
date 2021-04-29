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
    public partial class frmSelReceipeDefinitions : Form
    {
        bool formloaded;
        DyeQueryParameters parms = null;
        DyeRepository repo = null;
        public frmSelReceipeDefinitions()
        {
            InitializeComponent();
         
            repo = new DyeRepository();

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboQualities.CheckStateChanged += new System.EventHandler(this.cmboQualities_CheckStateChanged);
            this.cmboReceipeDefintions.CheckStateChanged += new System.EventHandler(this.cmboReceipeDefintions_CheckStateChanged);

        }

        private void frmSelReceipeDefinitions_Load(object sender, EventArgs e)
        {
            formloaded = false;
            parms = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var ExistingGreige = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var record in ExistingGreige)
                {
                    cmboQualities.Items.Add(new DyeHouse.CheckComboBoxItem(record.TLGreige_Id, record.TLGreige_Description, false));
                }

                var ExistDefinitions = context.TLDYE_RecipeDefinition.ToList();
                foreach (var record in ExistDefinitions)
                {
                    cmboReceipeDefintions.Items.Add(new DyeHouse.CheckComboBoxItem(record.TLDYE_DefinePk, record.TLDYE_DefineDescription, false));
                }

                var ExistWeight = context.TLADM_Colours.OrderBy(x=>x.Col_Display).ToList();
                foreach (var Record in ExistWeight)
                {
                    cmboColours.Items.Add(new DyeHouse.CheckComboBoxItem(Record.Col_Id, Record.Col_Display, false));
                }

               
                 
            }
            formloaded = true;
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = parms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        parms.Colours.Remove(value);

                }
            }
        }
        private void cmboQualities_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Qualities.Add(repo.LoadQuality(item._Pk));

                }
                else
                {
                    var value = parms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        parms.Qualities.Remove(value);

                }
            }
        }

        private void cmboReceipeDefintions_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.RecDefinitions.Add(repo.LoadDefinition(item._Pk));
                }
                else
                {
                    var value = parms.RecDefinitions.Find(it => it.TLDYE_DefinePk == item._Pk);
                    if (value != null)
                        parms.RecDefinitions.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                frmDyeViewReport vRep = new frmDyeViewReport(1, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                this.cmboColours.Items.Clear();
                this.cmboQualities.Items.Clear();
                this.cmboReceipeDefintions.Items.Clear();
                frmSelReceipeDefinitions_Load(this, null);
                
            }
        }

        private void cmboReceipeDefintions_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cmboQualities_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
