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
    public partial class frmDyesChemicalStockAdjustment : Form
    {
        bool FormLoaded;
        DataTable dt;
        BindingSource BindingSrc;
        DataColumn column;
        Util core; 

        DataGridViewTextBoxColumn selecta;     // 0 
        DataGridViewTextBoxColumn selectb;     // 1
        DataGridViewTextBoxColumn selectc;     // 2 
        protected readonly TTI2Entities _context;

        public frmDyesChemicalStockAdjustment()
        {
            InitializeComponent();

            this._context = new TTI2Entities();
            dt = new DataTable();
            BindingSrc = new BindingSource();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            core = new Util();

        }

        private void frmDyesChemicalStockAdjustment_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "DCChemAdjust_Pk";
            column.Caption = "Chemical";
            column.DefaultValue = 0;
            dt.Columns.Add(column);
            
            //----------------------------------------------
            // Col1 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Description_Pk";
            column.Caption = "Description";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //--------------------------------------------------------
            // Col 2
            //----------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Adjustment_Amt";
            column.Caption = "Adjustment Amt";
            column.DefaultValue = 0.00M;
            dt.Columns.Add(column);


            //0 -- index of record 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index";
            selecta.Visible = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;

            //--------------------------------------------
            selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "DC_Name";
            selectb.ValueType = typeof(String);
            selectb.DataPropertyName = dt.Columns[1].ColumnName;
            selectb.HeaderText = "Description";
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[1].DisplayIndex = 1;

            //--------------------------------------------
            selectc = new DataGridViewTextBoxColumn();
            selectc.Name = "DC_Name";
            selectc.ValueType = typeof(decimal);
            selectc.DataPropertyName = dt.Columns[2].ColumnName;
            selectc.HeaderText = "Adjustment";
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[2].DisplayIndex = 2;

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;

            var Records = _context.TLDYE_ConsumableSOH.ToList();
            foreach(var Record in Records)
            {
                DataRow NewRow = dt.NewRow();

                NewRow[0] = Record.DYCSH_Pk;
                NewRow[1] = _context.TLADM_ConsumablesDC.Find(Record.DYCSH_Consumable_FK).ConsDC_Description;
                NewRow[2] = 0.00M;
                dt.Rows.Add(NewRow);
            }

            FormLoaded = true; 
        }

        private void frmDyesChemicalStockAdjustment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null && FormLoaded)
            {
                var Cell = oDgv.CurrentCell;
                if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex > 1)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var oBtn = sender as Button; 
            if(oBtn != null && FormLoaded)
            {
                foreach(DataRow Row in dt.Rows)
                {
                    if(Row.Field<decimal>(2) == 0)
                    {
                        continue;
                    }

                    var Pk = Row.Field<int>(0);

                    var Consum = _context.TLDYE_ConsumableSOH.Find(Pk);
                    if(Consum != null)
                    {
                        Consum.DCSH_K_Adjusted = Row.Field<decimal>(2);
                    }
                    
                }

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data successfully saved to database");
                    dt.Rows.Clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
