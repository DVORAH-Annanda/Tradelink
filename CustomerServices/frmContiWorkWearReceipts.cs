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
    public partial class frmContiWorkWearReceipts : Form
    {
        bool FormLoaded;
        
        Util core;

        protected readonly TTI2Entities _context;

        DataTable dt;
        DataColumn column;
        BindingSource BindingSrc;

        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Styles
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn();  // Colours
        DataGridViewComboBoxColumn oCmboC = new DataGridViewComboBoxColumn();  // Sizes

        DataGridViewTextBoxColumn oTxtA;   // No of

        public frmContiWorkWearReceipts()
        {
            InitializeComponent();
            core = new Util();

            _context = new TTI2Entities();

            dt = new DataTable();
            BindingSrc = new BindingSource();

            //------------------------------------------------------
            // Create column 0. // This is the style index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the colour index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col1";
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // This is the Size index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col2";
            dt.Columns.Add(column);


            //------------------------------------------------------
            // Create column 3. // This is the Quantity of 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col3";
            column.DefaultValue = 0;
            dt.Columns.Add(column);


            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Styles Description";
            oCmboA.DataSource = _context.TLADM_Styles.Where(x =>x.Sty_WorkWear).ToList();
            oCmboA.ValueMember = "Sty_Id";
            oCmboA.DisplayMember = "Sty_Description";
            oCmboA.DataPropertyName = dt.Columns[0].ColumnName;
            oCmboA.Width = 150;
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns[0].DisplayIndex = 0;

            var Sty = _context.TLADM_Styles.FirstOrDefault(x => x.Sty_WorkWear);
            var tst = (from T1 in _context.TLADM_Colours
                       join T2 in _context.TLADM_StyleColour
                       on T1.Col_Id equals T2.STYCOL_Colour_FK
                       where T2.STYCOL_Style_FK == Sty.Sty_Id
                       select T1).ToList();

            oCmboB = new DataGridViewComboBoxColumn();
            oCmboB.HeaderText = "Colours Description";
            oCmboB.DataSource = tst;
            oCmboB.ValueMember = "Col_Id";
            oCmboB.DisplayMember = "Col_Display";
            oCmboB.DataPropertyName = dt.Columns[1].ColumnName;
            oCmboB.Width = 150;
            dataGridView1.Columns.Add(oCmboB);
            dataGridView1.Columns[1].DisplayIndex = 1;

            oCmboC = new DataGridViewComboBoxColumn();
            oCmboC.HeaderText = "Sizes Description";
            oCmboC.DataSource = _context.TLADM_Sizes.Where(x => x.SI_ContiSize != 0).ToList();
            oCmboC.ValueMember = "SI_Id";
            oCmboC.DisplayMember = "SI_ContiSize";
            oCmboC.DataPropertyName = dt.Columns[2].ColumnName;
            oCmboC.Width = 150;
            dataGridView1.Columns.Add(oCmboC);
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtA = new DataGridViewTextBoxColumn();   // 4 Number of items 
            oTxtA.HeaderText = "Number Of";
            oTxtA.ValueType = typeof(int);
            oTxtA.DataPropertyName = dt.Columns[3].ColumnName;
            oTxtA.Visible = true;
            oTxtA.Width = 100;
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns[3].DisplayIndex = 3;
            
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToAddRows = true;

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;

        }

        private void frmContiWorkWearReceipts_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            txtTransNumber.Text = "0";
            txtDelNo.Text = string.Empty;
            txtTTIOrderNo.Text = string.Empty;
                        
            cmboWarehouses.DataSource = _context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
            cmboWarehouses.ValueMember = "WHStore_Id";
            cmboWarehouses.DisplayMember = "WHStore_Description";
            cmboWarehouses.SelectedValue = -1;
                        
            cmboSuppliers.DataSource = _context.TLADM_Suppliers.Where(x => !(bool)x.Sup_Blocked).OrderBy(x => x.Sup_Description).ToList();
            cmboSuppliers.ValueMember = "SUP_Pk";
            cmboSuppliers.DisplayMember = "SUP_Description";
            cmboSuppliers.SelectedValue = -1;

            
            FormLoaded = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            int QuantityOrdered = 0;
            if(oBtn != null && FormLoaded)
            {
                var SupplierSelected = (TLADM_Suppliers)cmboSuppliers.SelectedItem;
                if(SupplierSelected == null)
                {
                    MessageBox.Show("Please select a supplier from the drop down box");
                    return;
                }

                var WareHouseSelected = (TLADM_WhseStore)cmboWarehouses.SelectedItem;
                if(WareHouseSelected == null)
                {
                    MessageBox.Show("Please enter a warehouse");
                    return;
                }
                if ( txtTTIOrderNo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a TradeLink Order Number");
                    return;
                }

                if (txtDelNo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a supplier delivery");
                    return;
                }

                var TrnsNumber = Convert.ToInt32(txtTransNumber.Text);

                var BoughtInGds = new TLCSV_BoughtInGoods();

                BoughtInGds.TLBIG_SupplierDelNo = txtDelNo.Text;
                BoughtInGds.TLBIG_Supplier_FK = SupplierSelected.Sup_Pk;
                BoughtInGds.TLBIG_TransDate = dtpDateOfTrans.Value;
                BoughtInGds.TLBIG_WareHouse_FK = WareHouseSelected.WhStore_Id;
                BoughtInGds.TLBIG_TransNumber = TrnsNumber;
                BoughtInGds.TLBIG_TTIOrderNo = txtTTIOrderNo.Text;
                _context.TLCSV_BoughtInGoods.Add(BoughtInGds);

                try
                {
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                    this.Close();
                }

                var Ind = 0;
                foreach (DataRow Dr in dt.Rows)
                {
                    QuantityOrdered = Dr.Field<int>(3);

                    Ind = 0;
                    var _Style = _context.TLADM_Styles.Find(Dr.Field<int>(0));
                    var _Colours = _context.TLADM_Colours.Find(Dr.Field<int>(1));
                    var _Sizes = _context.TLADM_Sizes.Find(Dr.Field<int>(2));

                    do
                    {
                        TLCSV_StockOnHand soh = new TLCSV_StockOnHand();

                        soh.TLSOH_BoxedQty = 1;
                        soh.TLSOH_Style_FK = Dr.Field<int>(0);
                        soh.TLSOH_Colour_FK = Dr.Field<int>(1);
                        soh.TLSOH_Size_FK = Dr.Field<int>(2);
                        soh.TLSOH_Is_A = true;
                        soh.TLSOH_Grade = "A";
                        if(_Style != null)
                            soh.TLSOH_PastelNumber = _Style.Sty_PastelNo + _Style.Sty_PastelCode;
                        if (_Colours != null)
                            soh.TLSOH_PastelNumber += _Colours.Col_Pastel;
                        if (_Sizes != null)
                            soh.TLSOH_PastelNumber += _Sizes.SI_PastelNo;
                        soh.TLSOH_SupplierTransNumber = TrnsNumber;
                        soh.TLSOH_Supplier_Fk = SupplierSelected.Sup_Pk;
                        soh.TLSOH_BoughtInGoods = true;
                        soh.TLSOH_BoughtInGoods_Fk = BoughtInGds.TLBIG_Pk;
                        soh.TLSOH_DateIntoStock = dtpDateOfTrans.Value;
                        soh.TLSOH_WareHouse_FK = WareHouseSelected.WhStore_Id;

                        _context.TLCSV_StockOnHand.Add(soh);
                    }
                    while (++Ind < QuantityOrdered);
                }
                try
                {
                    _context.SaveChanges();
                    dt.Rows.Clear();
                    MessageBox.Show("Data successfully saved to the database");
                   
                    frmCSViewRep vRep = new frmCSViewRep(29, BoughtInGds.TLBIG_Pk);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                    this.Close();
                }
            }
        }

        private void cmboSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if(oCmbo != null && FormLoaded)
            {
                var SelectedItem = (TLADM_Suppliers)oCmbo.SelectedItem;
                if(SelectedItem != null)
                {
                    var Cnt = _context.TLCSV_BoughtInGoods.Where(x => x.TLBIG_Supplier_FK == SelectedItem.Sup_Pk).Count();
                    txtTransNumber.Text = (Cnt + 1).ToString();
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex ==3)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
    }
}
