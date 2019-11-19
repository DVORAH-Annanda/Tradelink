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

namespace Spinning
{
    public partial class frmCottonLayDownSelection : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChk = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();

        bool _mode;
        int _pk;

        public IList<Int32> rowsSelected = null;

        public frmCottonLayDownSelection(bool mode)
        {
            InitializeComponent();
            _mode = mode;
        }

        public frmCottonLayDownSelection(bool mode, int pk)
        {
            InitializeComponent();
            _mode = mode;
            _pk = pk;
        }

        private void frmCottonLayDownSelection_Load(object sender, EventArgs e)
        {
            oTxtA.HeaderText = "Primary Key";
            oTxtA.ValueType = typeof(Int32);
            oTxtA.Visible = false;

            oTxtB.HeaderText = "Lay down number";
            oTxtB.ValueType = typeof(Int32);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC.HeaderText = "Lay down Weight";
            oTxtC.ValueType = typeof(decimal);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD.HeaderText = "Lot No";
            oTxtD.ValueType = typeof(decimal);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oChk.HeaderText = "select";
            oChk.ValueType = typeof(Boolean);

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oChk);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);

            using (var context = new TTI2Entities())
            {
                if (!_mode)
                {
                    var cotreceived = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TranType == 5 && !x.cotrx_Selected4Yarn).ToList();
                    foreach (var record in cotreceived)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = record.cotrx_pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = record.cotrx_Return_No;
                        dataGridView1.Rows[index].Cells[3].Value = Math.Round(record.cotrx_NetWeight, 2);
                        dataGridView1.Rows[index].Cells[4].Value = record.cotrx_LotNo;
                    }
                }
                else 
                {
                    var cotreceived = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TranType == 5).ToList();
                    foreach (var record in cotreceived)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = record.cotrx_pk;
                        if (record.cotrx_YarnOrder_FK == _pk)
                            dataGridView1.Rows[index].Cells[1].Value = true;
                        else 
                            dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = record.cotrx_Return_No;
                        dataGridView1.Rows[index].Cells[3].Value = Math.Round(record.cotrx_NetWeight, 2);
                        dataGridView1.Rows[index].Cells[4].Value = record.cotrx_LotNo;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            rowsSelected = new List<Int32>();

            if(oBtn != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString() == bool.FalseString)
                        continue;

                    rowsSelected.Add((int)row.Cells[0].Value);
                }

                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (!(bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    using (var context = new TTI2Entities())
                    {
                        int pk = (int)oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[0].Value;

                        var trans = context.TLSPN_CottonTransactions.Find(pk);
                        if (trans != null)
                        {
                            trans.cotrx_YarnOrder_FK = null;
                            trans.cotrx_Selected4Yarn = false;

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                       }
                   }
                }
            }
        }
    }
}
