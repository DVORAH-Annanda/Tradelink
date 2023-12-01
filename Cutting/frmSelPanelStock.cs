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

        protected readonly TTI2Entities _context;

        public frmSelPanelStock()
        {
           InitializeComponent();
            
            _context = new TTI2Entities();

           repo = new CuttingRepository();
           this.cmboWareHouseStore.CheckStateChanged += new System.EventHandler(this.cmboWareHouse_CheckStateChanged);

        }

        private void frmSelPanelStock_Load(object sender, EventArgs e)
        {
            formloaded = false;
            parms = new CuttingQueryParameters();

            var Existing = _context.TLADM_WhseStore.Where(x => x.WhStore_PanelStore).ToList();
            foreach (var Entry in Existing)
            {
               cmboWareHouseStore.Items.Add(new Cutting.CheckComboBoxItem(Entry.WhStore_Id, Entry.WhStore_Description, false));
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
                                
                cutOpts.PanelStockByAttrib = chkPanelAttributes.Checked;
                              
                if (!chkPanelAttributes.Checked)
                {
                    if(rbCostingColoursOnly.Checked)
                    {
                        parms.Colours.Clear();
                        cutOpts.CostingColour = true;
                        var Clrs = _context.TLADM_Colours.Where(x => x.Col_ColCosting).ToList();
                        foreach(var Clr in Clrs)
                        {
                              parms.Colours.Add(repo.LoadColour(Clr.Col_Id));
                        }
                    }
                    else if(rbCostingWhiteOnly.Checked)
                    {
                        parms.Colours.Clear();
                        cutOpts.CostingColourWhite = true;
                        var Clrs = _context.TLADM_Colours.Where(x => !x.Col_ColCosting).ToList();
                        foreach (var Clr in Clrs)
                        {
                              parms.Colours.Add(repo.LoadColour(Clr.Col_Id));
                        }
                    }

                    frmCutViewRep vRep = new frmCutViewRep(6, cutOpts, parms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog();
                }
                else
                {
                    parms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    parms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    parms.ToDate = parms.ToDate.AddHours(23);
                   
                    frmCutViewRep vRep = new frmCutViewRep(24, cutOpts, parms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog();
                }
                sortOptions = 1;
                cmboWareHouseStore.Items.Clear();
                frmSelPanelStock_Load(this, null);
            }
        }

        private void frmSelPanelStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
