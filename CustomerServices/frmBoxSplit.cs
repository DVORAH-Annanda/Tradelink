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

namespace CustomerServices
{
    public partial class frmBoxSplit : Form
    {
        bool formloaded;

        DataGridViewTextBoxColumn oTxtA;
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;
        DataGridViewTextBoxColumn oTxtE;

        public frmBoxSplit()
        {
            InitializeComponent();
        }

        private void frmBoxSplit_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                try
                {
                    var Query = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                    cmboWareHouse.DataSource = Query;
                    cmboWareHouse.ValueMember = "WhStore_Id";
                    cmboWareHouse.DisplayMember = "WhStore_Description";
                    cmboWareHouse.SelectedValue = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.ReadOnly = true;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.Visible = true;
                oChkA.HeaderText = "Select";
                oChkA.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.ReadOnly = true;
                oTxtB.Visible = true;
                oTxtB.ValueType = typeof(string);
                oTxtB.HeaderText = "Box Number";
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.ReadOnly = true;
                oTxtC.Visible = true;
                oTxtC.ValueType = typeof(string);
                oTxtC.HeaderText = "Grade";
                dataGridView1.Columns.Add(oTxtC);


                oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.ReadOnly = true;
                oTxtD.Visible = true;
                oTxtD.ValueType = typeof(int);
                oTxtD.HeaderText = "Boxed Qty";
                dataGridView1.Columns.Add(oTxtD);

                oTxtE = new DataGridViewTextBoxColumn();
                oTxtE.ReadOnly = true;
                oTxtE.Visible = false;
                oTxtE.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtE);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;
            }
            formloaded = true;

        }

        private void cmboWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_WhseStore)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();

                    //============================================================
                    //---------Define the datatable 
                    //=================================================================
                    System.Data.DataTable dt = new System.Data.DataTable();
                    DataColumn column;

                   //------------------------------------------------------
                   // Create column 0. // This is the Primary Key
                   //----------------------------------------------
                   column = new DataColumn();
                   column.DataType = System.Type.GetType("System.Int32");
                   column.ColumnName = "Col0";
                   dt.Columns.Add(column);
                

                  //-----------------------------------------------------------
                  // Create column 1. // This is the flag
                  //----------------------------------------------
                  column = new DataColumn();
                  column.DataType = System.Type.GetType("System.Boolean");
                  column.ColumnName = "Col1";
                  dt.Columns.Add(column);
               

                  //-----------------------------------------------------------
                  // Create column 2. // This is the BoxNumber 
                  //----------------------------------------------
                  column = new DataColumn();
                  column.DataType = System.Type.GetType("System.String");
                  column.ColumnName = "Col2";
                  dt.Columns.Add(column);
                
                  //-----------------------------------------------------------
                  // Create column 3. // This is the Grade 
                  //----------------------------------------------
                  column = new DataColumn();
                  column.DataType = System.Type.GetType("System.String");
                  column.ColumnName = "Col3";
                  dt.Columns.Add(column);

                  //-----------------------------------------------------------
                  // Create column 4. // This is the Boxed Qty 
                  //----------------------------------------------
                  column = new DataColumn();
                  column.DataType = System.Type.GetType("System.Int32");
                  column.ColumnName = "Col4";
                  dt.Columns.Add(column);

                  //-----------------------------------------------------------
                  // Create column 5. // This is the Key to the warehouse
                  //----------------------------------------------
                  column = new DataColumn();
                  column.DataType = System.Type.GetType("System.Int32");
                  column.ColumnName = "Col5";
                  dt.Columns.Add(column);


                  using (var context = new TTI2Entities())
                  {
                      var Existing = context.TLCSV_StockOnHand.Where(x => x.TLSOH_WareHouse_FK == selected.WhStore_Id
                               && !x.TLSOH_Sold && !x.TLSOH_Split && !x.TLSOH_Transfered && !x.TLSOH_Picked && x.TLSOH_BoxedQty != 0).ToList();

                      foreach (var row in Existing)
                      {
                          var index = dataGridView1.Rows.Add();
                          dataGridView1.Rows[index].Cells[0].Value = row.TLSOH_Pk;
                          dataGridView1.Rows[index].Cells[1].Value = false;
                          dataGridView1.Rows[index].Cells[2].Value = row.TLSOH_BoxNumber;
                          dataGridView1.Rows[index].Cells[3].Value = row.TLSOH_Grade;
                          dataGridView1.Rows[index].Cells[4].Value = row.TLSOH_BoxedQty;
                          dataGridView1.Rows[index].Cells[5].Value = row.TLSOH_CMT_FK;
                      }
                  }
                        
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            int rowindex = 0;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var selected = (TLADM_WhseStore)cmboWareHouse.SelectedItem;
                if (selected != null)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        rowindex = (int)CurrentRow.Cells[0].Value;

                        frmBoxSplitDetail SplitDetail = new frmBoxSplitDetail(CurrentRow, selected);
                        SplitDetail.ShowDialog(this);
                    }
                }
            }
        }
    }
}
