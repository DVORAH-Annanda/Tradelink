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
    public partial class frmBoxesDespatchedNoReceipted : Form
    {
        bool FormLoaded;

        CustomerServices.Repository repo;
        CustomerServices.CustomerServicesParameters parms;

        public frmBoxesDespatchedNoReceipted()
        {
            InitializeComponent();
            repo = new CustomerServices.Repository();
            this.cmboWarehouses.CheckStateChanged += new System.EventHandler(this.cmboWhses_CheckStateChanged);
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWhses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Whses.Add(repo.LoadWhse(item._Pk));

                }
                else
                {
                    var value = parms.Whses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        parms.Whses.Remove(value);

                }
            }
        }

        private void frmBoxesDespatchedNoReceipted_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            parms = new CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {
                var Whses = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                foreach (var Whse in Whses)
                {
                    cmboWarehouses.Items.Add(new CustomerServices.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                }
            }
            FormLoaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                CSVServices csvService = new CSVServices();
                
                frmCSViewRep vRep = new frmCSViewRep(16, parms, csvService);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                cmboWarehouses.Items.Clear();
                frmBoxesDespatchedNoReceipted_Load(this, null);
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
