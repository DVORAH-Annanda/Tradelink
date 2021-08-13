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
    public partial class frmDyeConsumablesSOH : Form
    {
        bool FormLoaded;
        Util core;
        protected readonly TTI2Entities _context;
        DyeHouse.DyeQueryParameters QParms;
        DyeHouse.DyeRepository repo;
        public frmDyeConsumablesSOH()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            core = new Util();

            repo = new DyeHouse.DyeRepository();
            this.cmboDyeConsumables.CheckStateChanged += new System.EventHandler(this.cmboDyeStores_CheckStateChanged);
            //this.cmboBatchesReprint.CheckStateChanged += new System.EventHandler(this.cmboCurrentBatchesReprint_CheckStateChanged);
        }

        private void frmDyeConsumablesSOH_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            QParms = new DyeQueryParameters();
            // TLADM_ConsumablesDC
            cmboDyeConsumables.DisplayMember = "ConsDC_Description";
            cmboDyeConsumables.ValueMember = "ConsDC_Pk";
            cmboDyeConsumables.SelectedValue = -1;

            var Consumables = _context.TLADM_ConsumablesDC.Where(x=>!(bool)x.ConsDC_Discontinued).OrderBy(x=>x.ConsDC_Description).ToList();
            foreach (var Consumable in Consumables)
            {
                cmboDyeConsumables.Items.Add(new DyeHouse.CheckComboBoxItem(Consumable.ConsDC_Pk ,Consumable.ConsDC_Description , false));
            }
            
            FormLoaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDyeStores_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;

                if (item.CheckState)
                {
                    try
                    {
                        QParms.Consummables.Add(repo.LoadConsummable(item._Pk));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    var value = QParms.Consummables.Find(it => it.ConsDC_Pk == item._Pk);
                    if (value != null)
                        QParms.Consummables.Remove(value);
                }

            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn =  (Button)sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QParms.DConsumablesFullDetail = chkDetail.Checked;
                
                try
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(13, QParms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
