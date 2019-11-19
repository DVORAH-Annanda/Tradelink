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
    public partial class frmProductionPlanDC : Form
    {
        bool formloaded;
        DyeRepository repo;
        DyeQueryParameters QueryParms;

        Util core;
        DataGridViewComboBoxColumn oCmbA;

        public frmProductionPlanDC()
        {
            InitializeComponent();

            core = new Util();

            this.cmboDyeBatches.CheckStateChanged += new System.EventHandler(this.cmboDyeBatches_CheckStateChanged);
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDyeBatches_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.DyeBatches.Add(repo.LoadDyeBatch(item._Pk));
                }
                else
                {
                    var value = QueryParms.DyeBatches.Find(it => it.DYEB_Pk == item._Pk);
                    if (value != null)
                        QueryParms.DyeBatches.Remove(value);

                }
            }
        }

        private void frmProductionPlanDC_Load(object sender, EventArgs e)
        {
            formloaded = false;

            repo = new DyeRepository();
            QueryParms = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Entries = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Allocated && !x.DYEB_Closed).OrderBy(x => x.DYEB_BatchNo).ToList();
                foreach (var Entry in Entries)
                {
                    cmboDyeBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Entry.DYEB_Pk, Entry.DYEB_BatchNo, false));
                }

                cmboDyeBatches.ValueMember = "DYEB_PK";
                cmboDyeBatches.DisplayMember = "DYEB_BatchNo";
             }

            formloaded = true;
        }

      

        private void btnProcess_Click(object sender, EventArgs e)
        {
            IList<DyeProductionDetails> prodDetail = new List<DyeProductionDetails>();
            Button oBtn = sender as Button;
            
            if (oBtn != null && formloaded)
            {

                foreach (var Batch in QueryParms.DyeBatches)
                {

                    DyeProductionDetails prodDet = new DyeProductionDetails();
                    prodDet.GreigePk = Batch.DYEB_Greige_FK;
                    prodDet.ColorPk =  Batch.DYEB_Colour_FK;
                    prodDet.PlannedProd = Batch.DYEB_BatchKG;
                    prodDet.DyeBatchPk = Batch.DYEB_Pk;

                    prodDetail.Add(prodDet);
                }


                frmDyeViewReport vRep = new frmDyeViewReport(37, prodDetail);
                vRep.ShowDialog(this);
            }
        }

       
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = 0;
        }
    }
}
