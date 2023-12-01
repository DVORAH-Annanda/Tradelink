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
    public partial class frmSelDyesAndChem : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;

        DyeHouse.DyeQueryParameters QParms = null;
        DyeHouse.DyeRepository repo = null;

        Util core = null;
        public frmSelDyesAndChem()
        {
            InitializeComponent();

            repo = new DyeHouse.DyeRepository();
            core = new Util();

            this.CheckComboBox.CheckStateChanged += new System.EventHandler(this.cmboDyeChemicals_CheckStateChanged);
            _context = new TTI2Entities();
        }

        private void frmSelDyesAndChem_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QParms = new DyeQueryParameters();

            // TLADM_ConsumablesDC
            //==================================================
            CheckComboBox.DisplayMember = "ConsDC_Description";
            CheckComboBox.ValueMember = "ConsDC_Pk";
            CheckComboBox.SelectedValue = -1;

            var Consumables = _context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Description).ToList();
            foreach (var Consumable in Consumables)
            {
                CheckComboBox.Items.Add(new DyeHouse.CheckComboBoxItem(Consumable.ConsDC_Pk, Consumable.ConsDC_Description, false));
            }
            FormLoaded = true;


        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDyeChemicals_CheckStateChanged(object sender, EventArgs e)
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
            Button oBtn = (Button)sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QParms.FromDate = Convert.ToDateTime(dtFromDate.Value);
                QParms.ToDate = Convert.ToDateTime(dtToDate.Value);
                QParms.ToDate = QParms.ToDate.AddHours(23); 

                try
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(51, QParms);
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

        private void frmSelDyesAndChem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();    
                }
            }
        }
    }
}
    
