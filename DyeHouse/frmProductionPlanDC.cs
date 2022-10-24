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
        
        
        DataTable dt;
        DataColumn column;

        BindingSource BindSrc; 


        public frmProductionPlanDC()
        {
            InitializeComponent();

            core = new Util();

            dt = new DataTable();
            column = new DataColumn();
            BindSrc = new BindingSource();

                        //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Colour_Pk";
            column.Caption = "Colour Index Key";
            column.DefaultValue = 0;
            dt.Columns.Add(column);
            //==========================================================================================
            // 1st task is to create the data table
            // Col 1
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select_Pk";
            column.Caption = "Select";
            column.DefaultValue = false;
            dt.Columns.Add(column);
            //==========================================================================================
            // 1st task is to create the data table
            // Col 2
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Colour_Desc";
            column.Caption = "Colour";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //==========================================================================================
            // 1st task is to create the data table
            // Col 3
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Greige_Desc";
            column.Caption = "Quality";
            column.DefaultValue = 1;
            dt.Columns.Add(column);
            
            //==========================================================================================
            // 1st task is to create the data table
            // Col 4
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Load_Amt";
            column.Caption = "Load Amount";
            column.DefaultValue = 0.00M;
            dt.Columns.Add(column);

                 

           
                        
        

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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex == 3)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
        private void frmProductionPlanDC_Load(object sender, EventArgs e)
        {
            formloaded = false;

            repo = new DyeRepository();
            QueryParms = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                dt.Rows.Clear();
                var Greige_Pk = context.TLADM_Griege.FirstOrDefault().TLGreige_Id;
                var ColoursAvail = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x=>x.Col_Display).ToList();
                foreach (var Colour in ColoursAvail)
                {
                    DataRow Dr = dt.NewRow();
                    Dr[0] = Colour.Col_Id;
                    Dr[1] = false;
                    Dr[2] = Colour.Col_Display;
                    Dr[3] = Greige_Pk;
                    Dr[4] = 0.00M;
                    dt.Rows.Add(Dr);
                }
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
                if (QueryParms.DyeBatches.Count != 0)
                {
                    foreach (var Batch in QueryParms.DyeBatches)
                    {
                        DyeProductionDetails prodDet = new DyeProductionDetails();
                        prodDet.GreigePk = Batch.DYEB_Greige_FK;
                        prodDet.ColorPk = Batch.DYEB_Colour_FK;
                        prodDet.PlannedProd = Batch.DYEB_BatchKG;
                        prodDet.DyeBatchPk = Batch.DYEB_Pk;

                        prodDetail.Add(prodDet);
                    }
                }
               /* else
                {
                    foreach(DataRow Row in dt.Rows)
                    {
                        if (!Row.Field<bool>(1))
                            continue;

                        DyeProductionDetails prodDet = new DyeProductionDetails();
                        prodDet.PlannedProd = Row.Field<decimal>(4);
                        prodDet.DyeBatchPk = 0;
                        prodDet.GreigePk = Row.Field<int>(3);
                        prodDet.ColorPk = Row.Field<int>(0);

                        prodDetail.Add(prodDet);

                    }
                } */

                if (prodDetail.Count != 0)
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(37, prodDetail);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h); vRep.ShowDialog(this);

                    frmProductionPlanDC_Load(this, null);
                }
                else
                {
                    MessageBox.Show("No Details entered");
                }
            }
        }

       
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = 0;
        }

        private void cmboDyeBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
