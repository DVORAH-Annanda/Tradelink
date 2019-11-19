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
    public partial class frmSelectReceipeConsumable : Form
    {
        Boolean FormLoaded;
        DyeHouse.DyeQueryParameters parms;
        DyeHouse.DyeRepository repo;
        
        public frmSelectReceipeConsumable()
        {
            InitializeComponent();

            repo = new DyeRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboReceipeConsumables.CheckStateChanged += new System.EventHandler(this.cmboDefinitions_CheckStateChanged);
       }

        private void frmSelectReceipeConsumable_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                rbChemicalAnalysis.Checked = true;
                groupBox4.Visible = false;

                var Entries = context.TLADM_ConsumablesDC.Where(x => !(bool)x.ConsDC_Discontinued).OrderBy(x => x.ConsDC_Description).ToList();
                foreach (var Entry in Entries)
                {
                    cmboReceipeConsumables.Items.Add(new DyeHouse.CheckComboBoxItem(Entry.ConsDC_Pk, Entry.ConsDC_Description, false));
                }
                
                cmboReceipeConsumables.ValueMember = "ConsDC_Pk";
                cmboReceipeConsumables.DisplayMember = "ConsDC_Description";
                cmboReceipeConsumables.SelectedValue = -1;
            }

            parms = new DyeQueryParameters();

            FormLoaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDefinitions_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Consummables.Add(repo.LoadConsummable(item._Pk));
                }
                else
                {
                    var value = parms.Consummables.Find(it => it.ConsDC_Pk == item._Pk);
                    if (value != null)
                        parms.Consummables.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {

                var FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                parms.ToDate = ToDate;
                parms.FromDate = FromDate;
                if (rbChemicalAnalysis.Checked)
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(41, parms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    this.cmboReceipeConsumables.Items.Clear();
                }
                else
                {
                    if (rbDyeHouseWIP.Checked)
                        parms.ProdWIP = true;
                    else
                        parms.ProdWIPCompleted = true;

                    frmDyeViewReport vRep = new frmDyeViewReport(42, parms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }

                this.frmSelectReceipeConsumable_Load(this, null);
            }
        }

        private void rbDyeHouseProduction_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    groupBox3.Visible = false;
                    groupBox4.Visible = true;
                    rbDyeHouseWIP.Checked = true; 
                }
            }
        }

        private void rbChemicalAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;
                }
            }
        }

        private void cmboReceipeConsumables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

    }
}
