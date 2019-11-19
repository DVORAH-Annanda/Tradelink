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
    public partial class frmYarnDetails3rdParty : Form
    {
        int _CustomerKey;
        int _YarnKey; 

        DataGridViewTextBoxColumn oTxtA;   // Palet Index no
        DataGridViewTextBoxColumn oTxtB;   // Pallet Number
        DataGridViewTextBoxColumn oTxtC;   // Pallet Nett Weight 
        DataGridViewTextBoxColumn oTxtD;   // Pallet Cones Spun
        DataGridViewTextBoxColumn oTxtE;   // Pallet Cones Reserved
        DataGridViewCheckBoxColumn oChkA;  // Details 
        DataGridViewCheckBoxColumn oChkB;  // Reset 
        bool formloaded;
        Util core;

        public frmYarnDetails3rdParty(int CustomerKey, int YarnKey)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            //Initialise the dataGrid
            //------------------------------------

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Palett Number key 
            oTxtA.HeaderText = "Pallet No";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Pallet Number
            oTxtB.HeaderText = "Pallet Number";
            oTxtB.ValueType = typeof(int);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 2 Nett Weight 
            oTxtC.HeaderText = "Nett Weight";
            oTxtC.ValueType = typeof(decimal);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Nett Weight Reserved
            oTxtD.HeaderText = "Nett Weight Reserved";
            oTxtD.ValueType = typeof(int);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 4 NettWeight Available
            oTxtE.HeaderText = "Nett Weight Available";
            oTxtE.ValueType = typeof(decimal);
            oTxtE.Visible = true;


            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Reserve";
            oChkA.ValueType = typeof(bool);
            oChkA.Visible = true;

            oChkB = new DataGridViewCheckBoxColumn();
            oChkB.HeaderText = "Reset Weight Reserved";
            oChkB.ValueType = typeof(bool);
            oChkB.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // 0  Pallet No Key 
            dataGridView1.Columns.Add(oChkA);    // 1  Reserve Yes / No
            dataGridView1.Columns.Add(oTxtB);    // 2  Pallet No
            dataGridView1.Columns.Add(oTxtC);    // 3  NettWeight  
            dataGridView1.Columns.Add(oTxtD);    // 4  NettWeight Reserved
            dataGridView1.Columns.Add(oTxtE);    // 5  NettWeight Available
            dataGridView1.Columns.Add(oChkB);    // 6  Reset Weight Reserved

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);

            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);

            _CustomerKey = CustomerKey;
            _YarnKey = YarnKey;


            core = new Util(); 

            SetUp();
        }

        void SetUp()
        {
             formloaded = false;
             using (var context = new TTI2Entities())
             {
                 
             }
        }

        private void Check_Changed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (e.ColumnIndex == 6 && (bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    DialogResult res = MessageBox.Show("Please confirm transaction", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[4].Value = 0.00;
                        dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[5].Value = dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value;
                        using (var context = new TTI2Entities())
                        {
                            var _Key = (int)dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[0].Value;
                           
                        }
                    }

                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        dataGridView1.Rows[CurrentRow.Index].Selected = true;
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[6];
                        CurrentRow.Cells[6].Value = false;
                        dataGridView1.Rows[CurrentRow.Index].Selected = false;
                    }

                    dataGridView1.Refresh();
                }


            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (e.ColumnIndex == 4)
            {
                var Cell = oDgv.CurrentCell;
                var CurrentWeight = (decimal)oDgv.Rows[Cell.RowIndex].Cells[2].Value;
                var ReservedWeight = (decimal)oDgv.Rows[Cell.RowIndex].Cells[3].Value;
                var RequestedWeight = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());

                if ((RequestedWeight + ReservedWeight) > CurrentWeight)
                {
                    MessageBox.Show("The weight amount entered exceeded that of the pallet ");
                    e.Cancel = true;
                }
            }


        }
    }
}
