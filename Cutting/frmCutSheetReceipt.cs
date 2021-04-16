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

namespace Cutting
{
    public partial class frmCutSheetReceipt : Form
    {
        bool formloaded;

        protected readonly TTI2Entities _context;
        //-----------------------------------------------------------------------------
        // datagridView1 
        //-------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     //  Bundle No                   1 
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Sizes                        2 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Qty                          3
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();

        TLCUT_CutSheet cutSheet;
        //------------------------------------------------------------------------------
        // datagridview2 
        //-------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtZA = new DataGridViewTextBoxColumn();     // Primary Key Sizes 0
        DataGridViewTextBoxColumn oTxtZB = new DataGridViewTextBoxColumn();     // Size Description  1 
        DataGridViewTextBoxColumn oTxtZC = new DataGridViewTextBoxColumn();     // Expected Qty      2
        DataGridViewTextBoxColumn oTxtZD = new DataGridViewTextBoxColumn();     // Actual            3
        DataGridViewTextBoxColumn oTxtZE = new DataGridViewTextBoxColumn();     // Variance          4
        Util core;

        DataTable dataTable; 
        DataTable dataTable2;
        DataColumn column;
        BindingSource BindingSrc;
        BindingSource BindingSrc2;

        public frmCutSheetReceipt()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            core = new Util();

        }

        private void frmCutSheetReceipt_Load(object sender, EventArgs e)
        {
            int _Width = 100;

            txtPrevCutSheet.Text = String.Empty;
            chkSearch.Checked = false;

       

            txtCurrentTotal.Text = "0";

            chkReset.Checked = false;

            formloaded = false;
            //----------------------------------------------------
            //0
            //----------------------------------------------------

            txtPrevCutSheet.Text = string.Empty;
            chkSearch.Checked = false;
                    

            //-----------------------------------------------------
            //
            //----------------------------------------------------------
            dataTable = new DataTable();
            BindingSrc = new BindingSource();
            dataTable2 = new DataTable();
            BindingSrc2 = new BindingSource();

            dataGridView1.AutoGenerateColumns = false;
            // dataGridView2.AutoGenerateColumns = false;

            //==========================================================================================
            // 1st task is to create the data table dataTable 
            // Col 0
            //=====================================================================
            /*
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Primary_Pk";
            column.Caption = "Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Bundle_Description";
            column.Caption = "Bundle Description";
            column.DefaultValue = String.Empty;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Bundle_Size";
            column.Caption = "Bundle Size";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Bundle_Qty";
            column.Caption = "Bundle Qty";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Updated";
            column.Caption = "Updated";
            column.DefaultValue = false;
            dataTable.Columns.Add(column); */

            oTxtA.Visible = false;
            oTxtA.Name = "Primary_Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.HeaderText = "Primary Key";
            // oTxtA.DataPropertyName = dataTable.Columns[0].ColumnName;
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns["Primary_Key"].DisplayIndex = 0;

            //--------------------------------------------------------
            //1
            //--------------------------------------------------------------
            oTxtB.HeaderText = "Bundle";
            oTxtB.ValueType = typeof(string);
            oTxtB.Name = "Bundle";
            oTxtB.Width = _Width;
            // oTxtB.DataPropertyName = dataTable.Columns[1].ColumnName;
            oTxtB.HeaderText = "Bundle Description";
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns["Bundle"].DisplayIndex = 1;
            //-----------------------------------------------------
            //2
            //------------------------------------------------------------------
            oCmboA.HeaderText = "Size";
            oCmboA.ValueType = typeof(int);
            oCmboA.HeaderText = "Size";
            //oCmboA.DataPropertyName = dataTable.Columns[2].ColumnName;
            oCmboA.Width = _Width;
            oCmboA.Name = "Size";
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns["Size"].DisplayIndex = 2;
            //-----------------------------------------------------------
            //3
            //-------------------------------------
            oTxtC.HeaderText = "Qty";
            oTxtC.Name = "Qty";
            oTxtC.ValueType = typeof(int);
            // oTxtC.DataPropertyName = dataTable.Columns[3].ColumnName;
            oTxtC.Width = _Width;
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns["Qty"].DisplayIndex = 3;

            //-----------------------------------------------------------
            //4 
            //----------------------------------------------------------------
            oChkA.HeaderText = "Updated";
            oChkA.Name = "UpDated";
            oChkA.ValueType = typeof(bool);
           // oChkA.DataPropertyName = dataTable.Columns[4].ColumnName;
            oChkA.Width = _Width;
            oChkA.Visible = false;
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns["Updated"].DisplayIndex = 4;


            // BindingSrc.DataSource = dataTable;
            // dataGridView1.DataSource = BindingSrc;


            //==========================================================================================
            // 1st task is to create the data table dataTable2 
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Size_Pk";
            column.Caption = "Size Primary Key";
            column.DefaultValue = 0;
            dataTable2.Columns.Add(column);
            dataTable2.PrimaryKey = new DataColumn[] { dataTable2.Columns[0] };

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Size_Description";
            column.Caption = "Size Description";
            column.DefaultValue = String.Empty;
            dataTable2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Expected_Qty";
            column.Caption = "Expected Qty";
            column.DefaultValue = 0;
            dataTable2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Actual_Qty";
            column.Caption = "Actual Qty";
            column.DefaultValue = 0;
            dataTable2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Variance_Perc";
            column.Caption = "Variance";
            column.DefaultValue = 0;
            dataTable2.Columns.Add(column);

            BindingSrc2.DataSource = dataTable2;
            dataGridView2.DataSource = BindingSrc2;

            /*
            oTxtZA.Visible = false;
            oTxtZA.ValueType = typeof(int);
            oTxtZA.DataPropertyName = dataTable2.Columns[0].ColumnName;
            dataGridView2.Columns.Add(oTxtZA);

            oTxtZB.Visible = false;
            oTxtZB.HeaderText = "Size";
            oTxtZB.ValueType = typeof(string);
            oTxtZB.DataPropertyName = dataTable2.Columns[1].ColumnName;
            oTxtZB.Width = _Width;
            dataGridView2.Columns.Add(oTxtZB);

            oTxtZC.Visible = false;
            oTxtZC.HeaderText = "Expected";
            oTxtZC.ValueType = typeof(int);
            oTxtZC.DataPropertyName = dataTable2.Columns[2].ColumnName;
            oTxtZC.Width = _Width;
            dataGridView2.Columns.Add(oTxtZC);

            oTxtZD.Visible = false;
            oTxtZD.HeaderText = "Actual";
            oTxtZD.ValueType = typeof(int);
            oTxtZD.Width = _Width;
            oTxtZD.DataPropertyName = dataTable2.Columns[3].ColumnName;
            dataGridView2.Columns.Add(oTxtZD);

            oTxtZE.HeaderText = "Variance";
            oTxtZE.ValueType = typeof(decimal);
            oTxtZE.Width = _Width;
            oTxtZE.DataPropertyName = dataTable2.Columns[4].ColumnName;
            dataGridView2.Columns.Add(oTxtZE);
            */

            int idx = -1;

            foreach (DataColumn col in dataTable2.Columns)
            {
                if (++idx == 0 )
                    dataGridView2.Columns[idx].Visible = false;
                else
                    dataGridView2.Columns[idx].HeaderText = col.Caption;
                
            }

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            //using (var context = new TTI2Entities())
            //{
                cmboCutSheet.DataSource = _context.TLCUT_CutSheet.Where(x=>x.TLCutSH_Accepted && !x.TLCutSH_WIPComplete && !x.TLCUTSH_OnHold).OrderBy(x=>x.TLCutSH_No).ToList();
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;

                var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboMachines.DataSource = _context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboMachines.ValueMember = "MD_Pk";
                    cmboMachines.DisplayMember = "MD_Description";
                    cmboMachines.SelectedValue = -1;
                }
           // }

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;

            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoGenerateColumns = false;

            this.txtBundles.Enabled = false;
            this.txtBundles.KeyDown += core.txtWin_KeyDownOEM;
            this.txtBundles.KeyPress += core.txtWin_KeyPress;
        
            this.txtAdultBoxes.KeyPress += core.txtWin_KeyPress;
            this.txtAdultBoxes.KeyDown += core.txtWin_KeyDownOEM;
            this.txtAdultBoxes.Text = "0";

            this.txtKidsBoxes.KeyPress += core.txtWin_KeyPress;
            this.txtKidsBoxes.KeyDown += core.txtWin_KeyDownOEM;
            this.txtKidsBoxes.Text = "0";

            this.txtBinding.KeyPress += core.txtWin_KeyPress;
            this.txtBinding.KeyDown += core.txtWin_KeyDownOEM;
            this.txtBinding.Text = "0";

            this.txtRibbing.KeyPress += core.txtWin_KeyPress;
            this.txtRibbing.KeyDown += core.txtWin_KeyDownOEM;
            this.txtRibbing.Text = "0";
               
            formloaded = true;
        }

        void SetUp()
        {
            this.txtAdultBoxes.Text = "0";
            this.txtKidsBoxes.Text = "0";
            this.txtBinding.Text = "0";
            this.txtRibbing.Text = "0";
            this.txtBundles.Text = "0";
            this.txtCurrentTotal.Text = "0";

            this.formloaded = false;;
            this.chkSearch.Checked = false;
            this.txtPrevCutSheet.Text = string.Empty;
            
            txtPrevCutSheet.Text = string.Empty;
            chkSearch.Checked = false;
            cmboCutSheet.SelectedValue = -1;
            formloaded = true;

        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;
            var Cell = oDgv.CurrentCell;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    if (Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLADM_Sizes> sz = new List<TLADM_Sizes>();
            int NoOfUnits = 0;
        

            if (oCmbo != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();
                    dataTable2.Rows.Clear();

                    cutSheet = selected;
                    txtBundles.Enabled = true;

                    rtbNotes.Text = selected.TLCutSH_Notes;

                    // using (var context = new TTI2Entities())
                    //{
                    var PowerN = (from t1 in _context.TLADM_Sizes
                                  join t2 in _context.TLCUT_ExpectedUnits
                                  on t1.SI_id equals t2.TLCUTE_Size_FK
                                  where cutSheet.TLCutSH_Pk == t2.TLCUTE_CutSheet_FK
                                  select t1).Sum(x => x.SI_PowerN);

                    if(PowerN != cutSheet.TLCutSH_Size_PN)
                    {
                        cutSheet.TLCutSH_Size_PN = PowerN;

                        var CS = _context.TLCUT_CutSheet.Find(cutSheet.TLCutSH_Pk);
                        if(CS != null)
                        {
                            CS.TLCutSH_Size_PN = PowerN;
                            try
                            {
                                _context.SaveChanges();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }


                    var Sizes = core.ExtrapNumber(cutSheet.TLCutSH_Size_PN, _context.TLADM_Sizes.Count());
                    Sizes.Sort();

                    foreach (var Size in Sizes)
                    {
                       var sze = _context.TLADM_Sizes.Where(x => x.SI_PowerN == Size).FirstOrDefault();
                       if (sze != null)
                       {
                          sz.Add(sze);
                       }
                     }
                    
                     var DB = _context.TLDYE_DyeBatch.Find(cutSheet.TLCutSH_DyeBatch_FK);
                     if (DB != null)
                     {
                            txtDyeBatch.Text = DB.DYEB_BatchNo;

                            var DO = _context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                NoOfUnits = _context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault().TLDYOD_Units;
                                txtCustomer.Text = _context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                txtColour.Text = _context.TLADM_Colours.Find(cutSheet.TLCutSH_Colour_FK).Col_Description;
                                txtDateOrdered.Text = DO.TLDYO_OrderDate.ToString("dd/MM/yyyy");

                                DateTime dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                                txtDateRequired.Text = dt.AddDays(5).ToString("dd/MM/yyyy");

                                var DBDetails = _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList();
                                foreach (var row in DBDetails)
                                {
                                    if (row.DYEBD_BodyTrim)
                                    {
                                        txtBody.Text = _context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;

                                    }
                                    else
                                    {
                                        if (String.IsNullOrEmpty(txtTrim1.Text))
                                            txtTrim1.Text = _context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                                        else if (String.IsNullOrEmpty(txtTrim2.Text))
                                            txtTrim2.Text = _context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                                    }
                                }

                               var EUnits = (from EUnitsx in _context.TLCUT_ExpectedUnits
                                             join xSizes in _context.TLADM_Sizes on EUnitsx.TLCUTE_Size_FK equals xSizes.SI_id
                                             where EUnitsx.TLCUTE_CutSheet_FK == cutSheet.TLCutSH_Pk 
                                             orderby xSizes.SI_DisplayOrder
                                             select EUnitsx).ToList();
                                // var EUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == cutSheet.TLCutSH_Pk).ToList();
                                foreach (var row in EUnits)
                                {
                                    DataRow NewRow = dataTable2.NewRow();
                                    NewRow[0] = row.TLCUTE_Size_FK;
                                    NewRow[1] = _context.TLADM_Sizes.Find(row.TLCUTE_Size_FK).SI_Description;
                                    NewRow[2] = row.TLCUTE_NoofGarments;
                                    NewRow[3] = 0;
                                    NewRow[4] = 0.00M;
                                    dataTable2.Rows.Add(NewRow);
                                 }
                            }
                        }
                        //------------------------------------------------------------
                        TLCUT_CutSheetReceipt CSR = new TLCUT_CutSheetReceipt();
                        CSR = _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == cutSheet.TLCutSH_Pk).FirstOrDefault();

                        if (CSR != null)
                        {
                            cmboMachines.SelectedValue = CSR.TLCUTSHR_Machine_FK;

                            dtpTransDate.Value = CSR.TLCUTSHR_Date;
                            txtBundles.Text = CSR.TLCUTSHR_NoOfBundles.ToString();
                        

                            IList<TLCUT_CutSheetReceiptDetail> CSRD = new List<TLCUT_CutSheetReceiptDetail>();
                            CSRD = _context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                            if (CSRD != null)
                            {
                                foreach (var row in CSRD)
                                {
                                    /*DataRow NewRow = dataTable.NewRow();
                                    NewRow[0] = row.TLCUTSHRD_Pk;
                                    NewRow[1] = row.TLCUTSHRD_Description;
                                    NewRow[2] = row.TLCUTSHRD_Size_FK;
                                    NewRow[3] = row.TLCUTSHRD_BundleQty;
                                    NewRow[4] = false;
                                    dataTable.Rows.Add(NewRow);*/

                                    
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = row.TLCUTSHRD_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = row.TLCUTSHRD_Description;
                                    dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_Size_FK;
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUTSHRD_BundleQty;
                                    

                                }

                                DataGridViewCellEventArgs exx = new DataGridViewCellEventArgs(3, 1);
                                try
                                {
                                    // dataGridView1_CellLeave(dataGridView1, exx);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }

                            TLCUT_CutSheetReceiptBoxes CSRB = new TLCUT_CutSheetReceiptBoxes();
                            CSRB = _context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (CSRB != null)
                            {
                                txtAdultBoxes.Text = CSRB.TLCUTSHB_AdultBoxes.ToString();
                                txtBinding.Text = CSRB.TLCUTSHB_Binding.ToString();
                                txtKidsBoxes.Text = CSRB.TLCUTSHB_KidBoxes.ToString();
                                txtRibbing.Text = CSRB.TLCUTSHB_Ribbing.ToString();
                            }

                    }
                        
                    // }

                    oCmboA.ValueMember = "SI_Id";
                    oCmboA.DisplayMember = "SI_Description";
                    oCmboA.DataSource = sz;
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var CurrentRow = oDgv.Rows;
            var Cell = oDgv.CurrentCell;
            int val = 0;
            if (e.ColumnIndex == 3)
            {
                if (String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
                {
                    return;
                }
                val = int.Parse(Cell.EditedFormattedValue.ToString());
                if (val > 0)
                {
                    if (oDgv.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        var SizePk = (int)oDgv.Rows[e.RowIndex].Cells[2].Value;
                        if (SizePk != 0)
                        {
                            int total = oDgv.Rows.Cast<DataGridViewRow>()
                                        .Where(r => Convert.ToInt32(r.Cells[2].Value) == SizePk 
                                                && r.Cells[0].RowIndex != e.RowIndex)
                                        .Sum(t => Convert.ToInt32(t.Cells[3].Value));

                            DataRow foundRow = dataTable2.Rows.Find(SizePk);
                            if (foundRow != null)
                            {
                                var Estimated = (int)foundRow[2];
                                foundRow[3] = total + val;
                                foundRow[4] = Math.Round(-1 * (100 - (decimal)(total + val) / (decimal)Estimated * 100), 2);
                            }
                            var sum = dataTable2.AsEnumerable().Sum(x => x.Field<int>(3));
                            txtCurrentTotal.Text = sum.ToString();
                        }

                    }
                }
                
            }
        }

        private void txtBundles_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            int Size = 0;
            if (oTxt != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected != null)
                {
                    string cs = selected.TLCutSH_No.Remove(0, 2);

                    int SizesCnt = dataTable2.Rows.Count;

                    if(SizesCnt == 1)
                    {
                        Size = dataTable2.Rows[0].Field<int>(0); 
                    }

                    int BundleNo = Convert.ToInt32(txtBundles.Text);
                    int i = 1;
                    do
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = 0;
                        dataGridView1.Rows[index].Cells[1].Value = cs + "-" + (i.ToString().PadLeft(2, '0'));
                        if(Size != 0)
                        {
                            dataGridView1.Rows[index].Cells[2].Value = Size;
                        }
                        dataGridView1.Rows[index].Cells[3].Value = 0;
                        dataGridView1.Rows[index].Cells[4].Value = false; 

                    } while (++i <= BundleNo);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add = false;
            TLCUT_CutSheetReceipt CSR = null;
            if (oBtn != null)
            {
                if(dataGridView1.Rows.Count < Convert.ToInt32(txtBundles.Text))
                {
                    MessageBox.Show("Please complete the form correctly");
                    return;
                }

                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected != null)
                {
                    var Machine = (TLADM_MachineDefinitions)cmboMachines.SelectedItem;
                    if (Machine == null)
                    {
                        MessageBox.Show("Please select a machine on which this cutsheet was cut");
                        return;
                    }
                    //using (var context = new TTI2Entities())
                    //{
                        var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();

                        var CS = _context.TLCUT_CutSheet.Find(selected.TLCutSH_Pk);
                        if (CS != null)
                        {
                            CS.TLCutSH_WIPComplete = true;
                            CS.TLCUTSH_AddNotes = string.Empty;
                        }
                        CSR = _context.TLCUT_CutSheetReceipt.Where(x=>x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();
                        if (CSR == null)
                        {
                            Add = true;
                            CSR = new TLCUT_CutSheetReceipt();
                        }

                        CSR.TLCUTSHR_Style_FK = selected.TLCutSH_Styles_FK;
                        CSR.TLCUTSHR_CutSheet_FK = selected.TLCutSH_Pk;
                        CSR.TLCUTSHR_Date = dtpTransDate.Value;
                        CSR.TLCUTSHR_NoOfBundles = Convert.ToInt32(txtBundles.Text);
                        CSR.TLCUTSHR_Machine_FK = Machine.MD_Pk;
                        CSR.TLCUTSHR_InBundleStore = true;
                        if (CSR.TLCUTSHR_InBundleStore)
                        {
                            var CutStore = _context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == CS.TLCutSH_Department_FK && x.WhStore_BundleStore).FirstOrDefault();
                            if (CutStore != null)
                            {
                                CSR.TLCUTSHR_WhseBunStore_FK = CutStore.WhStore_Id;
                                CSR.TLCUTSHR_DateIntoBunStore = dtpTransDate.Value;
                            }
                        }
                        if (Add)
                        {
                            _context.TLCUT_CutSheetReceipt.Add(CSR);
                        }

                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException en)
                        {
                            foreach (var eve in en.EntityValidationErrors)
                            {
                                MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                     
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[2].Value == null)
                                continue;

                            TLCUT_CutSheetReceiptDetail CSRD = new TLCUT_CutSheetReceiptDetail();
                            
                            Add = true;
                            if ((int)row.Cells[0].Value != 0)
                            {
                                var index = (int)row.Cells[0].Value;
                                CSRD = _context.TLCUT_CutSheetReceiptDetail.Find(index);

                                if (CSRD != null)
                                {
                                    Add = false;
                                }
                                else
                                {
                                    CSRD = new TLCUT_CutSheetReceiptDetail();
                                }
                            }

                            CSRD.TLCUTSHRD_CutSheet_FK = CSR.TLCUTSHR_Pk;
                            CSRD.TLCUTSHRD_Description = (String)row.Cells[1].Value;
                            CSRD.TLCUTSHRD_Size_FK     = (int)row.Cells[2].Value;
                            CSRD.TLCUTSHRD_BundleQty   = (int)row.Cells[3].Value;
                            CSRD.TLCUTSHRD_BoxUnits = (int)row.Cells[3].Value;
                            CSRD.TLCUTSHRD_InBundleStore = true;

                            if (chkSearch.Checked &&
                                CSRD.TLCUTSHRD_BundleQty != CSRD.TLCUTSHRD_BoxUnits)
                            {
                                if (CSRD.TLCUTSHRD_BoxUnits != 0)
                                {
                                    CSRD.TLCUTSHRD_BoxUnits = CSRD.TLCUTSHRD_BundleQty;
                                }
                            }

                            if (Dept != null)
                            {
                                var TranType = _context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 200).FirstOrDefault();
                                if (TranType != null)
                                {
                                    CSRD.TLCUTSHRD_TransactionType = TranType.TrxT_Pk;
                                    CSRD.TLCUTSHRD_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                }
                            }

                            if (Add)
                                _context.TLCUT_CutSheetReceiptDetail.Add(CSRD);
                        }

                        TLCUT_CutSheetReceiptBoxes CSRB = new TLCUT_CutSheetReceiptBoxes();
                        CSRB = _context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();

                        Add = false;
                        if (CSRB == null)
                        {
                            CSRB = new TLCUT_CutSheetReceiptBoxes();
                            Add = true;
                        }

                        CSRB.TLCUTSHB_CutSheet_FK = CSR.TLCUTSHR_Pk;
                        
                        if (!String.IsNullOrEmpty(txtAdultBoxes.Text))
                            CSRB.TLCUTSHB_AdultBoxes = Convert.ToInt32(txtAdultBoxes.Text);
                        else
                            CSRB.TLCUTSHB_AdultBoxes = 0;

                        if (!String.IsNullOrEmpty(txtKidsBoxes.Text))
                            CSRB.TLCUTSHB_KidBoxes = Convert.ToInt32(txtKidsBoxes.Text);
                        else
                            CSRB.TLCUTSHB_KidBoxes = 0;

                        if (!String.IsNullOrEmpty(txtBinding.Text))
                            CSRB.TLCUTSHB_Binding = Convert.ToInt32(txtBinding.Text);
                        else
                            CSRB.TLCUTSHB_Binding = 0;

                        if (!String.IsNullOrEmpty(txtRibbing.Text))
                            CSRB.TLCUTSHB_Ribbing = Convert.ToInt32(txtRibbing.Text);
                        else
                            CSRB.TLCUTSHB_Ribbing = 0;


                        if (Add)
                            _context.TLCUT_CutSheetReceiptBoxes.Add(CSRB);

                        try
                        {
                            _context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                            dataGridView1.Rows.Clear();
                            dataTable2.Rows.Clear();
                            
                            formloaded = false;
                            cmboCutSheet.DataSource = _context.TLCUT_CutSheet.Where(x=>x.TLCutSH_Accepted && !x.TLCutSH_WIPComplete && !x.TLCUTSH_OnHold).OrderBy(x=>x.TLCutSH_No).ToList();
                            cmboCutSheet.ValueMember = "TLCutSH_Pk";
                            cmboCutSheet.DisplayMember = "TLCutSH_No";
                            cmboCutSheet.SelectedValue = -1;
                            formloaded = true;
                            
                            SetUp();

                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException en)
                        {
                            foreach (var eve in en.EntityValidationErrors)
                            {
                                MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    //}
                }
            }
        }

        private void chkReset_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChkBx = sender as CheckBox;
            if (oChkBx != null && formloaded)
            {
                var selected = (TLCUT_CutSheet) cmboCutSheet.SelectedItem;
                if (selected != null)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                       // using (var context = new TTI2Entities())
                       //{
                            try
                            {
                                foreach (DataGridViewRow Row in dataGridView1.Rows)
                                {
                                    var index = (int)Row.Cells[0].Value;
                                    var detail = _context.TLCUT_CutSheetReceiptDetail.Find(index);
                                    if (detail != null)
                                    {
                                        _context.TLCUT_CutSheetReceiptDetail.Remove(detail);
                                    }
                                }

                                foreach (DataGridViewRow Row in dataGridView2.Rows)
                                {
                                    var index = (int)Row.Cells[0].Value;
                                    var detail = _context.TLCUT_ExpectedUnits.Find(index);
                                    if (detail != null)
                                    {
                                        _context.TLCUT_ExpectedUnits.Remove(detail);
                                    }
                                }

                                var existing = _context.TLCUT_CutSheetReceipt.Where(x=>x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();
                                if (existing != null)
                                {
                                    _context.TLCUT_CutSheetReceipt.Remove(existing);
                                }

                                var CutSheet = _context.TLCUT_CutSheet.Find(existing.TLCUTSHR_CutSheet_FK);
                                if (CutSheet != null && CutSheet.TLCutSH_WIPComplete)
                                    CutSheet.TLCutSH_WIPComplete = false;

                                _context.SaveChanges();
                                MessageBox.Show("Transaction successfully processed");
                                this.Close();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        //}
                    }
                }
            }
        }

        private void rbCurrentCutSheets_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    formloaded = false;
                    cmboCutSheet.DataSource = null;
                    cmboCutSheet.Items.Clear();
                   // cmboMachines.DataSource = null;
                   // cmboMachines.Items.Clear();
                    dataGridView1.Rows.Clear();
                    //     dataGridView2.Rows.Clear();
                    dataTable2.Rows.Clear();
                    
                    //using (var context = new TTI2Entities())
                    //{
                        cmboCutSheet.DataSource = _context.TLCUT_CutSheet.Where(x => x.TLCutSH_Accepted && !x.TLCutSH_WIPComplete).OrderBy(x => x.TLCutSH_No).ToList();
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;
                    //}

                    formloaded = true;
                }
            }
        }

        private void rbPreviousCutSheets_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    formloaded = false;
                    cmboCutSheet.DataSource = null;
                    cmboCutSheet.Items.Clear();
                    // cmboMachines.DataSource = null;
                    // cmboMachines.Items.Clear();
                    dataGridView1.Rows.Clear();
                    //  dataGridView2.Rows.Clear();
                    dataTable2.Rows.Clear(); 

                    //using (var context = new TTI2Entities())
                    //{
                        var tst = (from CutSheet in _context.TLCUT_CutSheet
                                   join CutSheetRec in _context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutSheetRec.TLCUTSHR_CutSheet_FK
                                   where CutSheet.TLCutSH_WIPComplete && CutSheet.TLCutSH_Accepted && !CutSheetRec.TLCUTSHR_Issued
                                   orderby CutSheet.TLCutSH_No
                                   select CutSheet).ToList();

                        cmboCutSheet.DataSource = tst;
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;

                        formloaded = true;

                    //}
                }
            }
        }

        private void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if(oChk != null && oChk.Checked && formloaded)
            {
                //using (var context = new TTI2Entities())
                //{
                    var CS = _context.TLCUT_CutSheet.Where(x => x.TLCutSH_No == txtPrevCutSheet.Text).FirstOrDefault();
                    if(CS == null)
                    {
                        MessageBox.Show("No Master Record on file");
                        return;
                    }

                    var CSR = _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                    if(CSR == null)
                    {
                        MessageBox.Show("It would appear that this CutSheet has not yet been receipted");
                        return;
                    }
                                      
                    var Details = _context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                    if(Details.Count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        foreach(var Detail in Details)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Detail.TLCUTSHRD_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Detail.TLCUTSHRD_Description;
                            dataGridView1.Rows[index].Cells[2].Value = Detail.TLCUTSHRD_Size_FK;
                            dataGridView1.Rows[index].Cells[3].Value = Detail.TLCUTSHRD_BundleQty;
                        }
                    }
                //}
            }
        }

        private void frmCutSheetReceipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}