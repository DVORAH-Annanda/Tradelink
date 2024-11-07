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
    public partial class frmPanelStock : Form
    {
        bool formloaded;
        CMTRepository repo;
        CMTQueryParameters parms; 
        
        public frmPanelStock()
        {
            InitializeComponent();
            repo = new CMTRepository();
            this.cmboWarehouses.CheckStateChanged += new System.EventHandler(this.cmboWareHouse_CheckStateChanged);
        }

        private void frmPanelStock_Load(object sender, EventArgs e)
        {
            formloaded = false;
            parms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_WhseStore.Where(x => x.WhStore_PanelStore).ToList();
                foreach (var Record in Existing)
                {
                    cmboWarehouses.Items.Add(new CMT.CheckComboBoxItem(Record.WhStore_Id, Record.WhStore_Description, false));
                }
 
            }

            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWareHouse_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CMT.CheckComboBoxItem && formloaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.WhseStores.Add(repo.LoadWareHouse(item._Pk));
                }
                else
                {
                    var value = parms.WhseStores.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        parms.WhseStores.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && formloaded)
            {
                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(6 ,parms, false);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    cmboWarehouses.Items.Clear();
                    
                    frmPanelStock_Load(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmboWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
