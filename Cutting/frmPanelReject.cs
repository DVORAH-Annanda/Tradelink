using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Cutting
{
    public partial class frmPanelReject : Form
    {
        bool formloaded;
        //-------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewCheckBoxColumn oChkBoxA = new DataGridViewCheckBoxColumn(); //  Selected Y/N               1
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     //  Bundle No                   2 
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Sizes                        3 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Qty                          4
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn();  // Reason                       5
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();     // Qty Rejected                 6
     
        //-------------------------------------------------------------------------------------
        Util core;

        public frmPanelReject()
        {
            InitializeComponent();
            core = new Util();
        }

        private void frmPanelReject_Load(object sender, EventArgs e)
        {
            IList<TLADM_WhseStore> WhStore = new List<TLADM_WhseStore>();

            formloaded = false;
            int _Width = 100;

            oTxtA.Visible = false;                        // 0
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkBoxA.HeaderText = "Select";              // 1
            oChkBoxA.ValueType = typeof(bool);
            oChkBoxA.Width = 50;
            dataGridView1.Columns.Add(oChkBoxA);

            oTxtB.HeaderText = "Bundle";                 // 2
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = _Width;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oCmboA.HeaderText = "Size";                 // 3
            oCmboA.ValueType = typeof(int);
            oCmboA.Width = _Width;
            oCmboA.ReadOnly = true;
            dataGridView1.Columns.Add(oCmboA);

            oTxtC.HeaderText = "Qty";                  // 4
            oTxtC.ValueType = typeof(int);
            oTxtC.Width = _Width;
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oCmboB.HeaderText = "Reason Code";           //5
            oCmboB.ValueType = typeof(int);
            oCmboB.Width = _Width;
            dataGridView1.Columns.Add(oCmboB);

            oTxtE.HeaderText = "Qty Returned";          //6
            oTxtE.ValueType = typeof(int);
            oTxtE.Width = _Width;
            dataGridView1.Columns.Add(oTxtE);
       

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            using (var context = new TTI2Entities())
            {
                cmboCutSheet.DataSource = context.TLCUT_CutSheet.ToList();
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;

                oCmboA.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_Description).ToList();
                oCmboA.ValueMember = "SI_Id";
                oCmboA.DisplayMember = "SI_Description";

                oCmboB.DataSource = context.TLCUT_RejectReasons.ToList();
                oCmboB.ValueMember = "TLCUTRJR_Pk";
                oCmboB.DisplayMember = "TLCUTRJR_Description";

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                if(Dept != null)
                {
                    var Existing = context.TLADM_WhseStore.Where(x=>x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    foreach(var row in Existing)
                    {
                        if(row.WhStore_Code.Contains("CWIP") || row.WhStore_Code.Contains("CPANREJ"))
                        {
                            WhStore.Add(row);
                        }
                    }

                    cmboRejectStore.DataSource = WhStore;
                    cmboRejectStore.ValueMember ="WhStore_Id";
                    cmboRejectStore.DisplayMember = "WhStore_Description";
                    cmboRejectStore.SelectedValue = -1;
                }

            }

            this.txtBundles.Enabled = false;

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;

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
                    if (Cell.ColumnIndex == 6)
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
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();
                        var DB = context.TLDYE_DyeBatch.Find(selected.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            txtDyeBatch.Text = DB.DYEB_BatchNo;

                            var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                txtCustomer.Text = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                                txtColour.Text = context.TLADM_Colours.Find(DO.TLDYO_Colour_FK).Col_Description;
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
                            }
                        }
                        
                        TLCUT_CutSheetReceipt CSR = new TLCUT_CutSheetReceipt();
                        CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();

                        if (CSR != null)
                        {
                            IList<TLCUT_CutSheetReceiptDetail> CSRD = new List<TLCUT_CutSheetReceiptDetail>();
                            CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk && !x.TLCUTSHRD_PanelRejected && x.TLCUTSHRD_BundleQty != 0).ToList();
                            if (CSRD != null)
                            {
                                foreach (var row in CSRD)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = row.TLCUTSHRD_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = false;
                                    dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_Description;
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUTSHRD_Size_FK;
                                    dataGridView1.Rows[index].Cells[4].Value = row.TLCUTSHRD_BundleQty - row.TLCUTSHRD_RejectQty;
                                    if(row.TLCUTSHRD_RejectReason == 0)
                                        dataGridView1.Rows[index].Cells[5].Value = context.TLCUT_RejectReasons.FirstOrDefault().TLCUTRJR_Pk;
                                    else
                                        dataGridView1.Rows[index].Cells[5].Value = row.TLCUTSHRD_RejectReason;
                                    dataGridView1.Rows[index].Cells[6].Value = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool Update = false;

            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a cut sheet from the drop down list ");
                    return;
                }

                var WhseSelected = (TLADM_WhseStore)cmboRejectStore.SelectedItem;
                if (WhseSelected == null)
                {
                    MessageBox.Show("Please select a reject store from the drop down list");
                    return;
                }

                if (String.IsNullOrEmpty(txtAuthorisedBy.Text))
                {
                    MessageBox.Show("Please complete the approved by details");
                    return;
                }
             
                using (var context = new TTI2Entities())
                {
                    var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();

                    CutReportOptions cropts = new CutReportOptions();
                    cropts.remarks = rtbNotes.Text;
                    cropts.TransDate = dtpTransDate.Value;
                    cropts.ApprovedBy = txtAuthorisedBy.Text;
                    cropts.Pk = CSR.TLCUTSHR_Pk;
                    cropts.CutSheetPk = selected.TLCutSH_Pk;
                    cropts.ReturnedTo = context.TLADM_WhseStore.Find(WhseSelected.WhStore_Id).WhStore_Description;

                    var cs = context.TLCUT_CutSheet.Find(selected.TLCutSH_Pk);
                    if (cs != null)
                    {
                        cs.TLCutSH_Notes = rtbNotes.Text;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;
                        
                        //------------------------------------------------------
                        // 1st Step reduce the SOH with what was written off
                        //-----------------------------------------------------------
                        var index = (int)row.Cells[0].Value;
                        var CSRD = context.TLCUT_CutSheetReceiptDetail.Find(index);
                        if (CSRD != null)
                        {
                            CSRD.TLCUTSHRD_RejectQty += (int)row.Cells[6].Value;
                            CSRD.TLCUTSHRD_RejectDate = dtpTransDate.Value;
                            CSRD.TLCUTSHRD_RejectReason = (int)row.Cells[5].Value;
                        }

                        //-------------------------------------------------
                        // Now create a new Record of the written off value
                        //---------------------------------------------------
                        TLCUT_CutSheetReceiptDetail NewCSR = new TLCUT_CutSheetReceiptDetail();
                        NewCSR.TLCUTSHRD_CutSheet_FK = CSR.TLCUTSHR_Pk;
                        NewCSR.TLCUTSHRD_Description = row.Cells[2].Value.ToString() + "R";
                        NewCSR.TLCUTSHRD_Size_FK = (int)row.Cells[3].Value;
                        NewCSR.TLCUTSHRD_BundleQty = (int)row.Cells[4].Value;
                        NewCSR.TLCUTSHRD_BoxUnits = 0;
                        NewCSR.TLCUTSHRD_TransactionType = 55;
                        NewCSR.TLCUTSHRD_CurrentStore_FK = WhseSelected.WhStore_Id;
                        NewCSR.TLCUTSHRD_PanelRejected = true;
                        NewCSR.TLCUTSHRD_RejectDate = dtpTransDate.Value;
                        NewCSR.TLCUTSHRD_RejectQty = (int)row.Cells[6].Value;
                        NewCSR.TLCUTSHRD_RejectReason = (int)row.Cells[5].Value;

                        context.TLCUT_CutSheetReceiptDetail.Add(NewCSR);

                        Update = true;

                    }

                    //-------------------------------------------------------
                    //
                    //-----------------------------------------------------------
                    string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                               .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                               .ToString();

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                    
                    TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                    DailyLog.TLDL_IPAddress = Mach_IP;
                    DailyLog.TLDL_Dept_Fk = Dept.Dep_Id;
                    DailyLog.TLDL_Date = DateTime.Now;
                    DailyLog.TLDL_TransDetail = "Cutting Dept Panel Rejected";
                    DailyLog.TLDL_AuthorisedBy = txtAuthorisedBy.Text; ;
                    DailyLog.TLDL_Comments = rtbNotes.Text;
                    context.TLADM_DailyLog.Add(DailyLog);

                    //----------------------------------------------------------
                    if (Update)
                    {
                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                            frmCutViewRep vRep = new frmCutViewRep(3, cropts);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                var CurrentRows = oDgv.CurrentRow;
                 if ((bool)CurrentRows.Cells[1].Value == true)
                 {
                     if (e.ColumnIndex == 5)
                     {
                          string val = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue as string;
                          if (string.IsNullOrEmpty(val))
                          {
                                MessageBox.Show("Please enter a value");
                                e.Cancel = true;
                          }
                     }
                     else if (e.ColumnIndex == 6)
                     {
                          var CellValue = Convert.ToInt32(e.FormattedValue.ToString());
                          if (CellValue == 0)
                          {
                                MessageBox.Show("Please enter a value");
                                e.Cancel = true;
                          }
                          else
                          {
                                int val = (int)dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                                if (CellValue > val)
                                {
                                    MessageBox.Show("Cannot write off more than the total value");
                                    e.Cancel = true;
                                }
                          }
                     }
                 }
            }
        }
    }
}
