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
        bool UpdateTable;

        Util core;

        protected readonly TTI2Entities _context;

        DataTable dt;
        DataColumn column;
        public frmContiWorkWearReceipts()
        {
            InitializeComponent();
            core = new Util();

            _context = new TTI2Entities();

            dt = new DataTable();

            //------------------------------------------------------
            // Create column 0. // This is the style index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            column.DefaultValue = 0;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the colour index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col1";
            column.DefaultValue = 0;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // This is the Size index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col2";
            column.DefaultValue = 0;
            dt.Columns.Add(column);


            //------------------------------------------------------
            // Create column 3. // This is the Quantity of 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col3";
            column.DefaultValue = 0;
            dt.Columns.Add(column);


            //------------------------------------------------------
            // Create column 3. // This is the Quantity of 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Col4";
            column.DefaultValue = String.Empty;
            dt.Columns.Add(column);


            txtQtyOf.KeyDown += core.txtWin_KeyDownJI;
            txtQtyOf.KeyPress += core.txtWin_KeyPress;

           


        }

        private void frmContiWorkWearReceipts_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            txtQtyOf.Text = "0";
            txtTransNumber.Text = "0";
            txtDelNo.Text = string.Empty;
            txtTTIOrderNo.Text = string.Empty;
                        
            cmboWarehouses.DataSource = _context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
            cmboWarehouses.ValueMember = "WHStore_Id";
            cmboWarehouses.DisplayMember = "WHStore_Description";
            cmboWarehouses.SelectedValue = -1;

            cmboStyles.DataSource = _context.TLADM_Styles.Where(x => x.Sty_WorkWear).ToList();
            cmboStyles.ValueMember = "Sty_Id";
            cmboStyles.DisplayMember = "Sty_Description";
            cmboStyles.SelectedValue = -1;

            var Sty = _context.TLADM_Styles.FirstOrDefault(x => x.Sty_WorkWear);
            var tst = (from T1 in _context.TLADM_Colours
                      join T2 in _context.TLADM_StyleColour 
                      on T1.Col_Id equals T2.STYCOL_Colour_FK
                      where T2.STYCOL_Style_FK == Sty.Sty_Id 
                      select T1).ToList();

            cmboColours.DataSource = tst;
            cmboColours.ValueMember = "Col_Id";
            cmboColours.DisplayMember = "Col_Display";
            cmboColours.SelectedValue = -1;

            cmboSizes.DataSource = _context.TLADM_Sizes.Where(x=>x.SI_ContiSize != 0).OrderBy(x=>x.SI_DisplayOrder).ToList();
            cmboSizes.ValueMember = "SI_Id";
            cmboSizes.DisplayMember = "SI_ContiSize";
            cmboSizes.SelectedValue = -1;

            cmboSuppliers.DataSource = _context.TLADM_Suppliers.Where(x => !(bool)x.Sup_Blocked).OrderBy(x => x.Sup_Description).ToList();
            cmboSuppliers.ValueMember = "SUP_Pk";
            cmboSuppliers.DisplayMember = "SUP_Description";
            cmboSuppliers.SelectedValue = -1;

            
            FormLoaded = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
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

                var StyleSel = (TLADM_Styles)cmboStyles.SelectedItem;
                if(StyleSel == null)
                {
                    MessageBox.Show("Please select a style");
                    return;
                }

                var ColourSel = (TLADM_Colours)cmboColours.SelectedItem;
                if (ColourSel == null)
                {
                    MessageBox.Show("Please select a colour");
                    return;
                }

                var SizeSel = (TLADM_Sizes)cmboSizes.SelectedItem;
                if (SizeSel == null)
                {
                    MessageBox.Show("Please select a size");
                    return;
                }

                var QuantyOrder = Convert.ToInt32(txtQtyOf.Text);

                if(QuantyOrder == 0)
                {
                    MessageBox.Show("Please enter a quantity ");
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
                if(UpdateTable)
                {
                    if (!AddToDataTable())
                    {
                        return; 
                    }
                }
                foreach (DataRow Dr in dt.Rows)
                {
                    QuantyOrder = Dr.Field<int>(3);

                    Ind = 0;    
                    do
                    {
                        TLCSV_StockOnHand soh = new TLCSV_StockOnHand();

                        soh.TLSOH_BoxedQty = 1;
                        soh.TLSOH_Style_FK = Dr.Field<int>(0);
                        soh.TLSOH_Colour_FK = Dr.Field<int>(1);
                        soh.TLSOH_Size_FK = Dr.Field<int>(2);
                        soh.TLSOH_Is_A = true;
                        soh.TLSOH_Grade = "A";
                        soh.TLSOH_PastelNumber = Dr.Field<String>(4);
                        soh.TLSOH_SupplierTransNumber = TrnsNumber;
                        soh.TLSOH_Supplier_Fk = SupplierSelected.Sup_Pk;
                        soh.TLSOH_BoughtInGoods = true;
                        soh.TLSOH_BoughtInGoods_Fk = BoughtInGds.TLBIG_Pk;
                        soh.TLSOH_DateIntoStock = dtpDateOfTrans.Value;
                        soh.TLSOH_WareHouse_FK = WareHouseSelected.WhStore_Id;

                        _context.TLCSV_StockOnHand.Add(soh);
                    }
                    while (++Ind < QuantyOrder);
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

        private bool AddToDataTable()
        {
            bool res = false;

            DataRow Dr = dt.NewRow();

            var Styles = (TLADM_Styles)cmboStyles.SelectedItem;
            if (Styles == null)
            {
                MessageBox.Show("Please select a style");
                return false;
            }
            var Colours = (TLADM_Colours)cmboColours.SelectedItem;
            if (Colours == null)
            {
                MessageBox.Show("Please select a colour");
                return false ;
            }
            var Sizes = (TLADM_Sizes)cmboSizes.SelectedItem;
            if (Sizes == null)
            {
                MessageBox.Show("Please select a size");
                return false;
            }
            Dr[0] = Styles.Sty_Id;
            Dr[1] = Colours.Col_Id;
            Dr[2] = Sizes.SI_id;
            Dr[3] = Convert.ToInt32(txtQtyOf.Text);
            Dr[4] = Styles.Sty_PastelNo + Styles.Sty_PastelCode + Colours.Col_Pastel + Sizes.SI_PastelNo;

            try
            {
                dt.Rows.Add(Dr);
                UpdateTable = false;
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res; 
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null && FormLoaded)
            {

                if (AddToDataTable())
                {
                    cmboColours.SelectedValue = -1;
                    cmboStyles.SelectedValue = -1;
                    cmboSizes.SelectedValue = -1;

                    UpdateTable = false;

                    txtQtyOf.Text = "0";
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FormLoaded)
            {
                UpdateTable = true;
            }
        }
    }
}
