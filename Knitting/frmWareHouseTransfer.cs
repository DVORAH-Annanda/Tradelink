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
    public partial class frmWareHouseTransfer : Form
    {
        protected readonly TTI2Entities _context;

        DataTable DataT;
        BindingSource Binding;


        bool FormLoaded;
        public frmWareHouseTransfer()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            DataT = new DataTable();
            Binding = new BindingSource();

            DataColumn column = new DataColumn();

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Production_Pk";
            column.Caption = "Product Primary Key";
            column.DefaultValue = 0;
            DataT.Columns.Add(column);
      
            //----------------------------------------------
            // Col1 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Selected_Pk";
            column.Caption = "Selected";
            column.DefaultValue = false;
            DataT.Columns.Add(column); 

            //--------------------------------------------------------
            // Col 2
            //----------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Piece_No";
            column.Caption = "Piece No";
            column.DefaultValue = string.Empty;
            DataT.Columns.Add(column);

            //-----------------------
            // Col 3
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Quality";
            column.Caption = "Quality";
            column.DefaultValue = string.Empty;
            DataT.Columns.Add(column);

            //-----------------------
            // Col 4
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Weight_Avail";
            column.Caption = "Weight";
            column.DefaultValue = 0.0M;
            DataT.Columns.Add(column);


            //0 -- index of record 
            //--------------------------------------------
            DataGridViewTextBoxColumn selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = DataT.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index";
            selecta.Visible = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;


            //1 -- Selected  
            //------------------------------------------------
            DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Name = "Selected";
            oChkA.HeaderText = "Select";
            oChkA.DataPropertyName = DataT.Columns[1].ColumnName;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;


            //2 -- index of record 
            //--------------------------------------------
            DataGridViewTextBoxColumn selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "PieceNo";
            selectb.ValueType = typeof(String);
            selectb.DataPropertyName = DataT.Columns[2].ColumnName;
            selectb.HeaderText = "Piece No";
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].DisplayIndex = 2;

            //3 -- index of record 
            //--------------------------------------------
            DataGridViewTextBoxColumn selectc = new DataGridViewTextBoxColumn();
            selectc.Name = "Quality";
            selectc.ValueType = typeof(String);
            selectc.DataPropertyName = DataT.Columns[3].ColumnName;
            selectc.HeaderText = "Quality";
            selectc.ReadOnly = true;
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[3].DisplayIndex = 3;

            //3 -- index of record 
            //--------------------------------------------
            DataGridViewTextBoxColumn selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "WeightAvail";
            selectd.ValueType = typeof(decimal);
            selectd.DataPropertyName = DataT.Columns[4].ColumnName;
            selectd.HeaderText = "Weight";
            selectd.ReadOnly = true;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns[4].DisplayIndex = 4;

            Binding.DataSource = DataT;
            dataGridView1.DataSource = Binding;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            int idx = -1;

            foreach (DataColumn col in DataT.Columns)
            {
                dataGridView1.Columns[++idx].HeaderText = col.Caption;
            }
        }

        private void frmWareHouseTransfer_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            var WHouses = _context.TLADM_WhseStore.Where(x => !x.WhStore_WhseOrStore && x.WhStore_DepartmentFK == 11).ToList();

            cmboFromWareHouse.DataSource = WHouses; 
            cmboFromWareHouse.ValueMember = "WhStore_Id";
            cmboFromWareHouse.DisplayMember = "WhStore_Description";
            cmboFromWareHouse.SelectedValue = -1;

            cmboToWareHouse.DataSource = WHouses;
            cmboToWareHouse.ValueMember = "WhStore_Id";
            cmboToWareHouse.DisplayMember = "WhStore_Discription";
            cmboToWareHouse.SelectedValue = -1;


            FormLoaded = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button Btn = sender as Button;
            if(Btn != null && FormLoaded)
            {
                foreach(DataRow Row in DataT.Rows)
                {
                    if(!Row.Field<bool>(1))
                    {
                        continue;
                    }

                    int Pk = Row.Field<int>(0);
                    TLKNI_GreigeProduction gp = _context.TLKNI_GreigeProduction.Find(Pk);
                    if(gp != null)
                    {
                        var SelectedWareH = (TLADM_WhseStore)cmboToWareHouse.SelectedItem;
                        if(SelectedWareH != null)
                        {
                            gp.GreigeP_Store_FK = SelectedWareH.WhStore_Id;
                        }
                    }
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void cmboFromWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                var SelectedWhse = (TLADM_WhseStore)oCmbo.SelectedItem;
                if(SelectedWhse == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select a warehouse to process");
                        return;
                    }
                }

                var Pk = SelectedWhse.WhStore_Id;
                var Entities = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Store_FK == Pk && !x.GreigeP_Dye).ToList();
                if(Entities.Count == 0)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("There are no records to be processed in store selected");
                        return;
                    }
                }

                foreach(var Entity in Entities)
                {
                    DataRow Row = DataT.NewRow();

                    Row[0] = Entity.GreigeP_Pk;
                    Row[1] = Entity.GreigeP_PieceNo;
                    Row[2] = _context.TLADM_Griege.Find(Entity.GreigeP_Greige_Fk).TLGreige_Description;
                    Row[3] = Entity.GreigeP_weightAvail;

                    DataT.Rows.Add(Row);
                }
            }
        }

        private void frmWareHouseTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
