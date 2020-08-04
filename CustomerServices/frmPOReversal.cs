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
    public partial class frmPOReversal : Form
    {
        bool FormLoaded;

        DataTable ProductRating;
        DataColumn column;
        BindingSource BindingSrc;

        Util core;

        DataGridViewTextBoxColumn selecta;     // 0 
        DataGridViewCheckBoxColumn oChkA;      // 2  
        DataGridViewTextBoxColumn selectc;     // 3
        DataGridViewTextBoxColumn selectd;     // 4 
        DataGridViewTextBoxColumn selecte;     // 5
        DataGridViewTextBoxColumn selectg;     // 7
        
        public frmPOReversal()
        {
            InitializeComponent();

            core = new Util();

            ProductRating = new DataTable();
            BindingSrc = new BindingSource();

           

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "ProdRating_Pk";
            column.Caption = "Product Rating Key";
            column.DefaultValue = 0;
            ProductRating.Columns.Add(column);
            ProductRating.PrimaryKey = new DataColumn[] { ProductRating.Columns[0] };


            //--------------------------------------------------------
            // Col 1
            //----------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Closed_Pk";
            column.Caption = "Open";
            column.DefaultValue = false;
            ProductRating.Columns.Add(column);

            //-----------------------
            // Col 2
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Style_Pk";
            column.Caption = "Style";
            column.DefaultValue = string.Empty;
            ProductRating.Columns.Add(column);

            //-----------------------
            // Col 3
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Colour_Pk";
            column.Caption = "Colour";
            column.DefaultValue = String.Empty;
            ProductRating.Columns.Add(column);

            //------------------------------
            // Col 4
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Size_Pk";
            column.Caption = "Size";
            column.DefaultValue = String.Empty;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 5
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Qty_Pk";
            column.Caption = "Qty REquired";
            column.DefaultValue = 0;
            ProductRating.Columns.Add(column);

            
            //============================================================================
            //0 -- index of record 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = ProductRating.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index";
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns["PrRatingIndex"].DisplayIndex = 0;


            //1 -- Open / Closed 
            //------------------------------------------------
            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Name = "Open";
            oChkA.HeaderText = "Discontinued";
            oChkA.DataPropertyName = ProductRating.Columns[1].ColumnName;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns["Open"].DisplayIndex = 1;

            //2 -- Style
            //----------------------------------------------
            selectg = new DataGridViewTextBoxColumn();
            selectg.Name = "StyleWidth";
            selectg.ValueType = typeof(string);
            selectg.DataPropertyName = ProductRating.Columns[2].ColumnName;
            selectg.HeaderText = "Style Width";
            dataGridView1.Columns.Add(selectg);
            dataGridView1.Columns["StyleWidth"].DisplayIndex = 2;

            //3 -- Colour
            //----------------------------------------------
            selectc = new DataGridViewTextBoxColumn();
            selectc.Name = "ColourWidth";
            selectc.HeaderText = "Colour Width";
            selectc.ValueType = typeof(String);
            selectc.DataPropertyName = ProductRating.Columns[3].ColumnName;
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns["ColourWidth"].DisplayIndex = 3;

            //4 -- Size
            //-------------------------------------------------
            selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "SizeWidth";
            selectd.HeaderText = "Size Width";
            selectd.ValueType = typeof(String);
            selectd.DataPropertyName = ProductRating.Columns[4].ColumnName;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns["SizeWidth"].DisplayIndex = 4;

            //5 -- Qty
            //--------------------------------------------------------
            selecte = new DataGridViewTextBoxColumn();
            selecte.Name = "Ratings";
            selecte.HeaderText = "Rating's";
            selecte.ValueType = typeof(Int32);
            selecte.DataPropertyName = ProductRating.Columns[5].ColumnName;
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns["Ratings"].DisplayIndex = 5;

            BindingSrc.DataSource = ProductRating;
            dataGridView1.DataSource = BindingSrc;

            DataGridView oDgv = dataGridView1;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            // OriginalWidth = dataGridView1.Width;

            int idx = -1;

            foreach (DataColumn col in ProductRating.Columns)
            {
                if (++idx == 0 || idx > 7)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;

            }

        }

        private void frmPOReversal_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using  (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.Where(x => !x.Cust_CommissionCust).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedIndex = -1;
            }

            

            FormLoaded = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex == 5)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                var SelectedItem = (TLADM_CustomerFile)oCmbo.SelectedItem;

                if (SelectedItem != null)
                {
                    cmboClosedPOs.DataSource = null;

                    using (var context = new TTI2Entities())
                    {
                        cmboClosedPOs.DataSource = context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed && x.TLCSVPO_Customer_FK == SelectedItem.Cust_Pk).ToList();
                        cmboClosedPOs.ValueMember = "TLCSVPO_Pk";
                        cmboClosedPOs.DisplayMember = "TLCSVPO_PurchaseOrder";
                        cmboClosedPOs.SelectedIndex = -1;

                    }
                }
            }
        }

        private void cmboClosedPOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                ProductRating.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    var SelectedItem = (TLCSV_PurchaseOrder)cmboClosedPOs.SelectedItem;

                    if(SelectedItem != null)
                    {
                        var Details = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == SelectedItem.TLCSVPO_Pk).ToList();

                        foreach(var Detail in Details)
                        {
                            DataRow nRow = ProductRating.NewRow();
                            nRow[0] = Detail.TLCUSTO_Pk;
                            nRow[1] = false;
                            nRow[2] = context.TLADM_Styles.Find(Detail.TLCUSTO_Style_FK).Sty_Description;
                            nRow[3] = context.TLADM_Colours.Find(Detail.TLCUSTO_Colour_FK).Col_Display;
                            nRow[4] = context.TLADM_Sizes.Find(Detail.TLCUSTO_Size_FK).SI_Description;
                            nRow[5] = Detail.TLCUSTO_Qty; 

                            ProductRating.Rows.Add(nRow);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            var MainPk = 0;

            if(oBtn != null && FormLoaded)
            {
                using ( var context = new TTI2Entities())
                {
                    foreach(DataRow DRow in ProductRating.Rows)
                    {
                        if(!DRow.Field<Boolean>(1))
                        {
                            continue;
                        }

                        var Pk = DRow.Field<Int32>(0);

                        var PurchaseDetail = context.TLCSV_PuchaseOrderDetail.Find(Pk);
                        if(PurchaseDetail != null && PurchaseDetail.TLCUSTO_Closed)
                        {
                            PurchaseDetail.TLCUSTO_Closed = false;
                                                  
                        }
                    }

                    var PoSelected = (TLCSV_PurchaseOrder)cmboClosedPOs.SelectedItem;
                    if (PoSelected != null)
                    {
                        var POrder = context.TLCSV_PurchaseOrder.Find(PoSelected.TLCSVPO_Pk);
                        if (POrder != null)
                        {
                            POrder.TLCSVPO_ClosedDate = null;
                            POrder.TLCSVPO_Closeed = false;
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");

                            frmPOReversal_Load(this, null);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MessageBox.Show(ex.StackTrace);
                            return;

                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
                                 
        }
    }
}
