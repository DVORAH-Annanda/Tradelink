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
    public partial class frmDyeRFD : Form
    {
        bool formLoaded;

        //DateTimePicker dtp = null;
        //DataGridViewComboBoxColumn oCmbSizes;  //Sizes
        //DataGridViewTextBoxColumn oTxtQty;   //Qty
        //DataGridViewTextBoxColumn oTxtQtyOutstanding; //oTxtQtyOutstanding

        protected readonly TTI2Entities _context;

        DataTable dtQuantitiesSizes;
        DataTable dtGarmentsAvailable;
        BindingSource bSrc;
        TLADM_LastNumberUsed lastBatchNo; 

        public frmDyeRFD()
        {
            InitializeComponent();
            
            _context = new TTI2Entities();

            bSrc = new BindingSource();
            DataColumn Column;

            dtQuantitiesSizes = new DataTable();

            ////==========================================================================================
            //// 1st task is to create the data table
            //// Col 0 - RFD Key
            ////=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "RFD_Pk";
            Column.Caption = "RFD Key";
            Column.DefaultValue = 0;
            dtQuantitiesSizes.Columns.Add(Column);

            //==========================================================================================
            // Col 1 - Select
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "RFD_Select";
            Column.Caption = "Select";
            Column.DefaultValue = false;
            dtQuantitiesSizes.Columns.Add(Column);

            //==========================================================================================
            // Col 2
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "Quantity";
            Column.Caption = "Quantity";
            Column.DefaultValue = string.Empty;
            dtQuantitiesSizes.Columns.Add(Column);

            //==========================================================================================
            // Col 3
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "Outstanding";
            Column.Caption = "Outstanding";
            Column.DefaultValue = 0;
            dtQuantitiesSizes.Columns.Add(Column);

            ///////////
            
            dgvGarmentsAvailable.AutoGenerateColumns = false;
            dgvGarmentsAvailable.AllowUserToAddRows = false;

            dtGarmentsAvailable = new DataTable();
            //==========================================================================================
            // 1st task is to create the data table
            // Col 0 - RFD Key
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "GD_pk";
            Column.Caption = "GD Key";
            Column.DefaultValue = 0;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 1 - Select
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "GD_Select";
            Column.Caption = "GD Select";
            Column.DefaultValue = false;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 2
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "GD_BoxNo";
            Column.Caption = "Box Number";
            Column.DefaultValue = string.Empty;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 3
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "GD_Size";
            Column.Caption = "Size";
            Column.DefaultValue = string.Empty;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 4
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "GD_Quantity";
            Column.Caption = "Quantity";
            Column.DefaultValue = 0;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 5
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "SplitBox";
            Column.Caption = "Split Box";
            Column.DefaultValue = false;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 6
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "QtyToBatch";
            Column.Caption = "Qty to Batch";
            Column.DefaultValue = 0;
            dtGarmentsAvailable.Columns.Add(Column);

            //==========================================================================================
            // Col 7
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "QtyToStock";
            Column.Caption = "Qty to Stock";
            Column.DefaultValue = 0;
            dtGarmentsAvailable.Columns.Add(Column);





            //======================================================
            // DataGridView 
            //==========================================================
            //Row[0] = SAvail.TLSOH_Pk;
            DataGridViewTextBoxColumn selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "Key";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dtGarmentsAvailable.Columns[0].ColumnName;
            selecta.HeaderText = "RFDKey";
            selecta.Visible = false;
            selecta.ReadOnly = true;
            dgvGarmentsAvailable.Columns.Add(selecta);
            dgvGarmentsAvailable.Columns[0].DisplayIndex = 0;


            //Row[1] = false;
            DataGridViewCheckBoxColumn selectAvailableGarments = new DataGridViewCheckBoxColumn();
            selectAvailableGarments.Name = "Select";
            selectAvailableGarments.ValueType = typeof(bool);
            selectAvailableGarments.HeaderText = "Select";
            selectAvailableGarments.DataPropertyName = dtGarmentsAvailable.Columns[1].ColumnName;
            selectAvailableGarments.Visible = true;
            dgvGarmentsAvailable.Columns.Add(selectAvailableGarments);
            dgvGarmentsAvailable.Columns[1].DisplayIndex = 1;


            //Row[2] = SAvail.TLSOH_BoxNumber;
            DataGridViewTextBoxColumn selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "BoxNumber";
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = dtGarmentsAvailable.Columns[2].ColumnName;
            selectb.HeaderText = "Box Number";
            selectb.ReadOnly = true;
            selectb.Visible = true;
            dgvGarmentsAvailable.Columns.Add(selectb);
            dgvGarmentsAvailable.Columns[2].DisplayIndex = 2;


            //Row[3] = SAvail.TLSOH_Size_FK;
            DataGridViewTextBoxColumn txtSizes = new DataGridViewTextBoxColumn();
            txtSizes.Name = "Size";
            txtSizes.ValueType = typeof(string);
            txtSizes.DataPropertyName = dtGarmentsAvailable.Columns[3].ColumnName;
            txtSizes.HeaderText = "Size";
            txtSizes.ReadOnly = true;
            txtSizes.Visible = true;
            dgvGarmentsAvailable.Columns.Add(txtSizes);
            dgvGarmentsAvailable.Columns[3].DisplayIndex = 3;


            //Row[4] = SAvail.TLSOH_BoxedQty;
            DataGridViewTextBoxColumn selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "BoxedQty";
            selectd.ValueType = typeof(Int32);
            selectd.DataPropertyName = dtGarmentsAvailable.Columns[4].ColumnName;
            selectd.HeaderText = "Boxed Qty";
            selectd.ReadOnly = true;
            selectd.Visible = true;
            dgvGarmentsAvailable.Columns.Add(selectd);
            dgvGarmentsAvailable.Columns[4].DisplayIndex = 4;

            //Row[5] = false;
            DataGridViewCheckBoxColumn selectSplitBox = new DataGridViewCheckBoxColumn();
            selectSplitBox.Name = "SelectSplitBox";
            selectSplitBox.ValueType = typeof(bool);
            selectSplitBox.HeaderText = "Split Box";
            selectSplitBox.DataPropertyName = dtGarmentsAvailable.Columns[5].ColumnName;
            selectSplitBox.Visible = true;
            dgvGarmentsAvailable.Columns.Add(selectSplitBox);
            dgvGarmentsAvailable.Columns[5].DisplayIndex = 5;

           
            //Row[6] = 0;
            DataGridViewTextBoxColumn qtyToBatch = new DataGridViewTextBoxColumn();
            qtyToBatch.Name = "ToBatchQty";
            qtyToBatch.ValueType = typeof(Int32);
            qtyToBatch.DataPropertyName = dtGarmentsAvailable.Columns[6].ColumnName;
            qtyToBatch.HeaderText = "Qty to Batch";
            qtyToBatch.ReadOnly = true;
            qtyToBatch.Visible = true;
            dgvGarmentsAvailable.Columns.Add(qtyToBatch);
            dgvGarmentsAvailable.Columns[6].DisplayIndex = 6;


            //Row[7] = 0;
            DataGridViewTextBoxColumn qtyToStock = new DataGridViewTextBoxColumn();
            qtyToBatch.Name = "StockQty";
            qtyToBatch.ValueType = typeof(Int32);
            qtyToBatch.DataPropertyName = dtGarmentsAvailable.Columns[7].ColumnName;
            qtyToBatch.HeaderText = "Qty to Stock";
            qtyToBatch.ReadOnly = true;
            qtyToBatch.Visible = true;
            dgvGarmentsAvailable.Columns.Add(qtyToStock);
            dgvGarmentsAvailable.Columns[7].DisplayIndex = 7;

            bSrc.DataSource = dtGarmentsAvailable;
            dgvGarmentsAvailable.DataSource = bSrc;

        }

        private void frmDyeRFD_Load(object sender, EventArgs e)
        {
            formLoaded = false;

            bool RepackTransaction = false;

            dtQuantitiesSizes.Rows.Clear();

            lastBatchNo = _context.TLADM_LastNumberUsed.Find(3);
            if(lastBatchNo != null)
            {
                if(lastBatchNo.col14 == 0)
                {
                    lastBatchNo.col14 = 1;
                }

                txtBatchNumber.Text = "GD" + lastBatchNo.col14.ToString().PadLeft(5, '0');
            }

            cmbColours.DataSource = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
            cmbColours.DisplayMember = "Col_Display";
            cmbColours.ValueMember = "Col_Id";
            cmbColours.SelectedValue = -1;

            cmboStyles.DataSource = _context.TLADM_Styles.Where(x => x.Sty_PFD).OrderBy(x => x.Sty_Description).ToList();
            cmboStyles.DisplayMember = "Sty_Description";
            cmboStyles.ValueMember = "Sty_Id";
            cmboStyles.SelectedValue = -1;


            /////////


            using (var context = new TTI2Entities())
            {


                //var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                //if (Dept != null)
                //{
                //    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                //    if (LNU != null)
                //        txtTransNumber.Text = "CO" + LNU.col2.ToString().PadLeft(5, '0');

                //}

                //0
                //***************************
                DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.ReadOnly = true;
                oTxtA.ValueType = typeof(int);
                oTxtA.Visible = false;
                oTxtA.HeaderText = "Pk";
                dgvSizesQuantities.Columns.Add(oTxtA);

                //1
                //*************************
                DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.Visible = false;
                oTxtB.ReadOnly = true;
                oTxtB.HeaderText = "Line Number";
                oTxtB.Width = 100;
                oTxtB.ValueType = typeof(string);
                dgvSizesQuantities.Columns.Add(oTxtB);

                //2
                //****************************
                DataGridViewComboBoxColumn oCmbC = new DataGridViewComboBoxColumn();
                oCmbC.DataSource = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                oCmbC.HeaderText = "Size";
                oCmbC.ValueMember = "SI_id";
                oCmbC.DisplayMember = "SI_Display";
                dgvSizesQuantities.Columns.Add(oCmbC);

                //6
                //**************************************
                DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.Visible = true;
                oTxtC.HeaderText = "Quantity";
                oTxtC.Width = 100;
                oTxtC.ValueType = typeof(int);
                dgvSizesQuantities.Columns.Add(oTxtC);

                //7
                //************************************************
                DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.Visible = true;
                oTxtD.HeaderText = "Outstanding";
                oTxtD.Width = 100;
                oTxtD.ValueType = typeof(string);
                dgvSizesQuantities.Columns.Add(oTxtD);



                //    //10
                //    //*************************************
                //    oChkB = new DataGridViewCheckBoxColumn();
                //    oChkB.ValueType = typeof(bool);
                //    oChkB.HeaderText = "Order Status";
                //    dgvSizesQuantities.Columns.Add(oChkB);

                //    //11
                //    //**********************************
                //    oTxtE = new DataGridViewTextBoxColumn();
                //    oTxtE.Visible = false;
                //    oTxtE.HeaderText = "RePack Center Key";
                //    oTxtE.ValueType = typeof(int);
                //    dgvSizesQuantities.Columns.Add(oTxtE);

                //    dgvSizesQuantities.AutoGenerateColumns = false;





                //    core = new Util();
                //    MandatoryFields = new string[][]
                //    {   new string[] {"2", "Please select a style", "0"},
                //        new string[] {"4", "Please select a Colour", "1"},
                //        new string[] {"5", "Please select a Size", "2"},
                //        new string[] {"6", "Please enter the required qty", "3"}

                //    };

                //    AddnAdd = false;

                //    dtpCustOrderDate.Value = DateTime.Now;
                //    dtpRequiredDate.Value = DateTime.Now.AddDays(30);

                //    if (dtpRequiredDate.Value.DayOfWeek == DayOfWeek.Saturday)
                //        dtpRequiredDate.Value.AddDays(2);
                //    else if (dtpRequiredDate.Value.DayOfWeek == DayOfWeek.Sunday)
                //        dtpRequiredDate.Value.AddDays(1);

                //    MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }

            //Mode = true;

            //RowLeave = false;

            //cmboCustomers.Focus();
            //EditMode = false;
            //AddnAdd = false;
            //rbOrderActive.Checked = true;
            //rbSpecialNo.Checked = true;
            formLoaded = true;

 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && formLoaded)
            {
                TLADM_Colours ColourSelected = (TLADM_Colours)cmbColours.SelectedItem;
                if (ColourSelected == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a colour");
                    }

                    return;
                }

                foreach(DataRow Row in dtQuantitiesSizes.Rows)
                {
                    if(!Row.Field<bool>(1))
                    {
                        continue;
                    }

                    var Pk = Row.Field<int>(0);
                    var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);

                    try
                    {
                        if (StockOnHand != null)
                        {
                            StockOnHand.TLSOH_RFD_NotYetDyed = true;

                            var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
                            if (History == null)
                            {
                                History = new TLDYE_RFDHistory();
                                History.DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK;
                                History.DyeRFD_BeginDyeDate = dtpDateDyed.Value.Date; ;
                                History.DyeRFD_DyeToColour = ColourSelected.Col_Id;
                                History.DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk;
                                History.DyeRFD_Transaction_No = lastBatchNo.col14;

                                _context.TLDYE_RFDHistory.Add(History);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);

                    }
                }
            }

            lastBatchNo.col14 += 1;

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Data successfully saved to database");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void frmDyeRFD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formLoaded)
            {
                var StySelected = (TLADM_Styles)oCmbo.SelectedItem;

                if(StySelected != null)
                {
                    var StockAvail = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == StySelected.Sty_Id && !x.TLSOH_RFD_NotYetDyed).OrderBy(x => x.TLSOH_BoxNumber).ToList();

                    dtGarmentsAvailable.Rows.Clear();

                    foreach (var SAvail in StockAvail)
                    {
                        DataRow Row = dtGarmentsAvailable.NewRow();
                        Row[0] = SAvail.TLSOH_Pk;
                        Row[1] = false;
                        Row[2] = SAvail.TLSOH_BoxNumber;
                        Row[3] = SAvail.TLSOH_Size_FK;
                        Row[4] = SAvail.TLSOH_BoxedQty;
                        Row[5] = false;
                        Row[6] = 0;
                        Row[7] = 0;

                        dtGarmentsAvailable.Rows.Add(Row);
                    }
                }

            }
        }

        private void dgvGarmentsAvailable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
