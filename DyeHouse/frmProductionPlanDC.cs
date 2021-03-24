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
        DataGridViewComboBoxColumn oCmboA;

        DataGridViewTextBoxColumn selecta;    // index of the main record 
        DataGridViewCheckBoxColumn oChkA;     // Selected Yes or No 
        DataGridViewTextBoxColumn selectb;    // Colour Description 
        DataGridViewTextBoxColumn selectc;    // Amount
        DataGridViewTextBoxColumn selectd;    // Amount
        
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

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;


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

            //0 -- index of record 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Colour Index";
            selecta.Visible = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.DataPropertyName = dt.Columns[1].ColumnName;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;

            selectb = new DataGridViewTextBoxColumn();
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = dt.Columns[2].ColumnName;
            selectb.HeaderText = "Colour Descrip";
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].DisplayIndex = 2;

            using (var context = new TTI2Entities())
            {
                oCmboA = new DataGridViewComboBoxColumn();
                oCmboA.HeaderText = "Quality";
                oCmboA.DataSource = context.TLADM_Griege.Where(x=>!(bool)x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                oCmboA.ValueMember = "TLGreige_Id";
                oCmboA.DisplayMember = "TLGreige_Description";
                oCmboA.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oCmboA);
                dataGridView1.Columns[3].DisplayIndex = 3;
            }

            selectc = new DataGridViewTextBoxColumn();
            selectc.ValueType = typeof(string);
            selectc.DataPropertyName = dt.Columns[4].ColumnName;
            selectc.HeaderText = "Load Value";
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[4].DisplayIndex = 4;
                        
            BindSrc.DataSource = dt;
            dataGridView1.DataSource = BindSrc;

            this.cmboDyeBatches.CheckStateChanged += new System.EventHandler(this.cmboDyeBatches_CheckStateChanged);
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
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
                else
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
                }

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
    }
}
