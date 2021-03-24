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

namespace Knitting
{
    public partial class frmYarnOrderAssigned : Form
    {
        KnitQueryParameters QueryParms;
        KnitRepository repo;
        bool FormLoaded;
        Util core;
        public frmYarnOrderAssigned()
        {
            InitializeComponent();
            
            repo = new KnitRepository();
            core = new Util();

            this.comboYarnOrders.CheckStateChanged += new System.EventHandler(this.cmboYarnOrders_CheckStateChanged);
        }

        private void frmYarnOrderAssigned_Load(object sender, EventArgs e)
        {
            FormLoaded = false; 
            QueryParms = new KnitQueryParameters();
            using (var context = new TTI2Entities())
            {
                var YarnOrders = context.TLSPN_YarnOrder.OrderBy(x => x.YarnO_OrderNumber).ToList();
                foreach (var YarnOrder in YarnOrders)
                {
                    comboYarnOrders.Items.Add(new Knitting.CheckComboBoxItem(YarnOrder.YarnO_Pk, YarnOrder.YarnO_OrderNumber.ToString(), false));
                }
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboYarnOrders_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.YarnOrders.Add(repo.LoadYarnOrder(item._Pk));
                }
                else
                {
                    var value = QueryParms.YarnOrders.Find(it => it.YarnO_Pk == item._Pk);
                    if (value != null)
                        QueryParms.YarnOrders.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QueryParms.FromDate  =  Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate    = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate    = QueryParms.ToDate.AddHours(23.30);

                frmKnitViewRep vRep = new frmKnitViewRep(29, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }


        }

        private void comboYarnOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
