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

        //-----------------------------------------------------------------------------
        // datagridView1 
        //-------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     //  Bundle No                   1 
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Sizes                        2 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Qty                          3

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

        public frmCutSheetReceipt()
        {
            InitializeComponent();
        }

        private void frmCutSheetReceipt_Load(object sender, EventArgs e)
        {
            int _Width = 100;

            core = new Util();

            txtCurrentTotal.Text = "0";

            chkReset.Checked = false;

            formloaded = false;

            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Bundle";
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = _Width;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oCmboA.HeaderText = "Size";
            oCmboA.ValueType = typeof(int);
            oCmboA.Width = _Width;
            dataGridView1.Columns.Add(oCmboA);

            oTxtC.HeaderText = "Qty";
            oTxtC.ValueType = typeof(int);
            oTxtC.Width = _Width;
            dataGridView1.Columns.Add(oTxtC);

            oTxtZA.Visible = false;
            oTxtZA.ValueType = typeof(int);
            dataGridView2.Columns.Add(oTxtZA);

            oTxtZB.HeaderText = "Size";
            oTxtZB.ValueType = typeof(string);
            oTxtZB.Width = _Width;
            dataGridView2.Columns.Add(oTxtZB);

            oTxtZC.HeaderText = "Expected";
            oTxtZC.ValueType = typeof(int);
            oTxtZC.Width = _Width;
            dataGridView2.Columns.Add(oTxtZC);

            oTxtZD.HeaderText = "Actual";
            oTxtZD.ValueType = typeof(int);
            oTxtZD.Width = _Width;
            dataGridView2.Columns.Add(oTxtZD);

            oTxtZE.HeaderText = "Variance";
            oTxtZE.ValueType = typeof(int);
            oTxtZE.Width = _Width;
            dataGridView2.Columns.Add(oTxtZE);

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            using (var context = new TTI2Entities())
            {
                cmboCutSheet.DataSource = context.TLCUT_CutSheet.Where(x=>x.TLCutSH_Accepted && !x.TLCutSH_WIPComplete && !x.TLCUTSH_OnHold).OrderBy(x=>x.TLCutSH_No).ToList();
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboMachines.ValueMember = "MD_Pk";
                    cmboMachines.DisplayMember = "MD_Description";
                    cmboMachines.SelectedValue = -1;
                }
            }

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

            cmboCutSheet.SelectedValue = -1;
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
                    dataGridView2.Rows.Clear();

                    cutSheet = selected;
                    txtBundles.Enabled = true;

                    txtBundles.Text = "0";

                    rtbNotes.Text = selected.TLCutSH_Notes;

                    using (var context = new TTI2Entities())
                    {
                        var Sizes = core.ExtrapNumber(selected.TLCutSH_Size_PN, context.TLADM_Sizes.Count());
                        Sizes.Sort();

                        foreach (var Size in Sizes)
                        {
                            var sze = context.TLADM_Sizes.Where(x => x.SI_PowerN == Size).FirstOrDefault();
                            if (sze != null)
                            {
                                sz.Add(sze);
                            }
                        }
                    
                        var DB = context.TLDYE_DyeBatch.Find(selected.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            txtDyeBatch.Text = DB.DYEB_BatchNo;

                            var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                NoOfUnits = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault().TLDYOD_Units;
                                txtCustomer.Text = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                txtColour.Text = context.TLADM_Colours.Find(cutSheet.TLCutSH_Colour_FK).Col_Description;
                                txtDateOrdered.Text = DO.TLDYO_OrderDate.ToString("dd/MM/yyyy");

                                DateTime dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                                txtDateRequired.Text = dt.AddDays(5).ToString("dd/MM/yyyy");

                                var DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList();
                                foreach (var row in DBDetails)
                                {
                                    if (row.DYEBD_BodyTrim)
                                    {
                                        txtBody.Text = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;

                                    }
                                    else
                                    {
                                        if (String.IsNullOrEmpty(txtTrim1.Text))
                                            txtTrim1.Text = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                                        else if (String.IsNullOrEmpty(txtTrim2.Text))
                                            txtTrim2.Text = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                                    }
                                }

                               var EUnits = (from EUnitsx in context.TLCUT_ExpectedUnits
                                             join xSizes in context.TLADM_Sizes on EUnitsx.TLCUTE_Size_FK equals xSizes.SI_id
                                             where EUnitsx.TLCUTE_CutSheet_FK == cutSheet.TLCutSH_Pk 
                                             orderby xSizes.SI_DisplayOrder
                                             select EUnitsx).ToList();
                                // var EUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == cutSheet.TLCutSH_Pk).ToList();
                                foreach (var row in EUnits)
                                {
                                    var index = dataGridView2.Rows.Add();
                                    dataGridView2.Rows[index].Cells[0].Value = row.TLCUTE_Size_FK;
                                    dataGridView2.Rows[index].Cells[1].Value = context.TLADM_Sizes.Find(row.TLCUTE_Size_FK).SI_Description;
                                    dataGridView2.Rows[index].Cells[2].Value = row.TLCUTE_NoofGarments;
                                    dataGridView2.Rows[index].Cells[3].Value = 0;
                                    dataGridView2.Rows[index].Cells[4].Value = 0.00M;
                                }
                            }
                        }
                        //------------------------------------------------------------
                        TLCUT_CutSheetReceipt CSR = new TLCUT_CutSheetReceipt();
                        CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == cutSheet.TLCutSH_Pk).FirstOrDefault();

                        if (CSR != null)
                        {
                            cmboMachines.SelectedValue = CSR.TLCUTSHR_Machine_FK;

                            dtpTransDate.Value = CSR.TLCUTSHR_Date;
                            txtBundles.Text = CSR.TLCUTSHR_NoOfBundles.ToString();
                        

                            IList<TLCUT_CutSheetReceiptDetail> CSRD = new List<TLCUT_CutSheetReceiptDetail>();
                            CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                            if (CSRD != null)
                            {
                                foreach (var row in CSRD)
                                {
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
                            CSRB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (CSRB != null)
                            {
                                txtAdultBoxes.Text = CSRB.TLCUTSHB_AdultBoxes.ToString();
                                txtBinding.Text = CSRB.TLCUTSHB_Binding.ToString();
                                txtKidsBoxes.Text = CSRB.TLCUTSHB_KidBoxes.ToString();
                                txtRibbing.Text = CSRB.TLCUTSHB_Ribbing.ToString();
                            }

                        }
                        
                    }

                    oCmboA.ValueMember = "SI_Id";
                    oCmboA.DisplayMember = "SI_Description";
                    oCmboA.DataSource = sz;
                 }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            int val = 0;

            if (oDgv != null && formloaded)
            {
                if (e.ColumnIndex == 3)
                {
                    var Cell = oDgv.CurrentCell;
                    var CurrentRow = oDgv.CurrentRow;

                    if(String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
                    {
                        return;
                    }

                    int Total = Convert.ToInt32(Cell.EditedFormattedValue.ToString());

                    //1st thing clear down the  
                    //-----------------------------------------------------------------
                    foreach (DataGridViewRow rowx in dataGridView2.Rows)
                    {
                        dataGridView2.Rows[rowx.Index].Cells[3].Value = 0;
                        dataGridView2.Rows[rowx.Index].Cells[4].Value = 0;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[2].Value != null)
                        {
                            int CurrentTotal = 0;
                            foreach (DataGridViewRow rowx in dataGridView2.Rows)
                            {
                                if ((int)row.Cells[2].Value == (int)rowx.Cells[0].Value)
                                {
                                    val = (int)dataGridView2.Rows[rowx.Index].Cells[3].Value;
                                    if ((int)dataGridView1.Rows[row.Index].Cells[3].Value == 0)
                                    {
                                        val += Total;
                                        Total = 0;
                                    }
                                    else
                                    {
                                        val += (int)dataGridView1.Rows[row.Index].Cells[3].Value;
                                    }

                                    dataGridView2.Rows[rowx.Index].Cells[3].Value = val;
                                    int Estimated = (int)dataGridView2.Rows[rowx.Index].Cells[2].Value;
                                    if (Estimated > 0)
                                    {
                                        decimal Variance = -1 * (100 - (decimal)val / (decimal)Estimated * 100);
                                        dataGridView2.Rows[rowx.Index].Cells[4].Value = Math.Round(Variance, 2);
                                    }
                                    else
                                        dataGridView2.Rows[rowx.Index].Cells[4].Value = 0;
                                }

                                CurrentTotal += (int)dataGridView2.Rows[rowx.Index].Cells[3].Value;
                            }
                            txtCurrentTotal.Text = CurrentTotal.ToString();
                        }
                    }
                }
            }
        }

        private void txtBundles_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected != null)
                {
                    string cs = selected.TLCutSH_No.Remove(0, 2);

                    int BundleNo = Convert.ToInt32(txtBundles.Text);
                    int i = 1;
                    do
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = 0;
                        dataGridView1.Rows[index].Cells[1].Value = cs + "-" + (i.ToString().PadLeft(2, '0'));
                        dataGridView1.Rows[index].Cells[3].Value = 0;

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
                    using (var context = new TTI2Entities())
                    {
                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();

                        var CS = context.TLCUT_CutSheet.Find(selected.TLCutSH_Pk);
                        if (CS != null)
                        {
                            CS.TLCutSH_WIPComplete = true;
                            CS.TLCUTSH_AddNotes = string.Empty;
                        }
                        CSR = context.TLCUT_CutSheetReceipt.Where(x=>x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();
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
                            var CutStore = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == CS.TLCutSH_Department_FK && x.WhStore_BundleStore).FirstOrDefault();
                            if (CutStore != null)
                            {
                                CSR.TLCUTSHR_WhseBunStore_FK = CutStore.WhStore_Id;
                                CSR.TLCUTSHR_DateIntoBunStore = dtpTransDate.Value;
                            }
                        }
                        if (Add)
                        {
                            context.TLCUT_CutSheetReceipt.Add(CSR);
                        }

                        try
                        {
                            context.SaveChanges();
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
                                CSRD = context.TLCUT_CutSheetReceiptDetail.Find(index);

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
                            CSRD.TLCUTSHRD_InBundleStore = true;

                            if (Dept != null)
                            {
                                var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 200).FirstOrDefault();
                                if (TranType != null)
                                {
                                    CSRD.TLCUTSHRD_TransactionType = TranType.TrxT_Pk;
                                    CSRD.TLCUTSHRD_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                }
                            }

                            if (Add)
                                context.TLCUT_CutSheetReceiptDetail.Add(CSRD);
                        }

                        TLCUT_CutSheetReceiptBoxes CSRB = new TLCUT_CutSheetReceiptBoxes();
                        CSRB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();

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
                            context.TLCUT_CutSheetReceiptBoxes.Add(CSRB);

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                            dataGridView1.Rows.Clear();
                            dataGridView2.Rows.Clear();
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
                    }
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
                        using (var context = new TTI2Entities())
                        {
                            try
                            {
                                foreach (DataGridViewRow Row in dataGridView1.Rows)
                                {
                                    var index = (int)Row.Cells[0].Value;
                                    var detail = context.TLCUT_CutSheetReceiptDetail.Find(index);
                                    if (detail != null)
                                    {
                                        context.TLCUT_CutSheetReceiptDetail.Remove(detail);
                                    }
                                }

                                foreach (DataGridViewRow Row in dataGridView2.Rows)
                                {
                                    var index = (int)Row.Cells[0].Value;
                                    var detail = context.TLCUT_ExpectedUnits.Find(index);
                                    if (detail != null)
                                    {
                                        context.TLCUT_ExpectedUnits.Remove(detail);
                                    }
                                }

                                var existing = context.TLCUT_CutSheetReceipt.Where(x=>x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();
                                if (existing != null)
                                {
                                    context.TLCUT_CutSheetReceipt.Remove(existing);
                                }

                                var CutSheet = context.TLCUT_CutSheet.Find(existing.TLCUTSHR_CutSheet_FK);
                                if (CutSheet != null && CutSheet.TLCutSH_WIPComplete)
                                    CutSheet.TLCutSH_WIPComplete = false;

                                context.SaveChanges();
                                MessageBox.Show("Transaction successfully processed");
                                this.Close();

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
                    dataGridView2.Rows.Clear();
                    
                    using (var context = new TTI2Entities())
                    {
                        cmboCutSheet.DataSource = context.TLCUT_CutSheet.Where(x => x.TLCutSH_Accepted && !x.TLCutSH_WIPComplete).OrderBy(x => x.TLCutSH_No).ToList();
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;

                        formloaded = true;

                    }
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
                    dataGridView2.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var tst = (from CutSheet in context.TLCUT_CutSheet
                                   join CutSheetRec in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutSheetRec.TLCUTSHR_CutSheet_FK
                                   where CutSheet.TLCutSH_WIPComplete && CutSheet.TLCutSH_Accepted && !CutSheetRec.TLCUTSHR_Issued
                                   orderby CutSheet.TLCutSH_No
                                   select CutSheet).ToList();

                        cmboCutSheet.DataSource = tst;
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;

                        formloaded = true;

                    }
                }
            }
        }

    }
}