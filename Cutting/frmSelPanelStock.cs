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
    public partial class frmSelPanelStock : Form
    {
        bool formloaded;
        int sortOptions;
        bool _Store;

        CuttingQueryParameters parms;
        CuttingRepository repo;

        public frmSelPanelStock()
        {
           InitializeComponent();
           repo = new CuttingRepository();
           this.cmboWareHouseStore.CheckStateChanged += new System.EventHandler(this.cmboWareHouse_CheckStateChanged);

        }

        private void frmSelPanelStock_Load(object sender, EventArgs e)
        {
            formloaded = false;
            parms = new CuttingQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_WhseStore.Where(x => x.WhStore_PanelStore).ToList();
                foreach (var Entry in Existing)
                {
                    cmboWareHouseStore.Items.Add(new Cutting.CheckComboBoxItem(Entry.WhStore_Id, Entry.WhStore_Description, false));
                }
            }

            sortOptions = 1;

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(2, "Quality"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(5, "Style"));
            cmboReportSelection.DataSource = reportOptions;
            cmboReportSelection.ValueMember = "Key";
            cmboReportSelection.DisplayMember = "Value";
            cmboReportSelection.SelectedIndex = -1;
            formloaded = true;
        
            formloaded = true;
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWareHouse_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.WareHouses.Add(repo.LoadWareHouse(item._Pk));
                }
                else
                {
                    var value = parms.WareHouses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        parms.WareHouses.Remove(value);
                }
            }
        }
        private void cmboReportSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                sortOptions = Convert.ToInt32(oCmbo.SelectedValue);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                CutReportOptions cutOpts = new CutReportOptions();
                cutOpts.C2SortOption = sortOptions;

                frmCutViewRep vRep = new frmCutViewRep(6, cutOpts, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();

                sortOptions = 1;
                cmboWareHouseStore.Items.Clear();
                frmSelPanelStock_Load(this, null);
            }
        }
    }
}
