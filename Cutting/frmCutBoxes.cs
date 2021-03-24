using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Cutting
{

    public partial class frmCutBoxes : Form
    {
        bool formloaded;

        int CutSheetSelected;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // 0 Primary Key (CutSheetDetail) 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     // 1 Bundle No                    1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // 2 Bundle Qty                   2
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();     // 3 Box Number                   3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();     // 4 Boxed  Qty                   4
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // 5 Box Types                    5
        
        Util core;

        
        public frmCutBoxes()
        {
            InitializeComponent();
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
          
        }

        private void frmCutBoxes_Load(object sender, EventArgs e)
        {
            
            formloaded = false;
            CutSheetSelected = 0;
            int _Width = 100;

            core = new Util();

            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Bundle";
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = _Width;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Qty";
            oTxtC.ValueType = typeof(int);
            oTxtC.Width = _Width;
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Box Number";
            oTxtD.ValueType = typeof(string);
            oTxtD.Width = _Width;
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Boxed Qty";
            oTxtE.ValueType = typeof(int);
            oTxtE.Width = _Width;
            dataGridView1.Columns.Add(oTxtE);

            /*
            oCmboA.HeaderText = "Box Type";
            oCmboA.ValueType = typeof(int);
            oCmboA.Width = _Width;
            dataGridView1.Columns.Add(oCmboA);
            */

            txtAdultBoxes.Text = "0";
            txtBinding.Text = "0";
            txtKidsBoxes.Text = "0";
            txtRibbing.Text = "0";

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            using (var context = new TTI2Entities())
            {
                var Query = from CutSheet in context.TLCUT_CutSheet
                            join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                            where CutSheet.TLCutSH_Accepted && CutReceipt.TLCUTSHR_InBundleStore && !CutReceipt.TLCUTSHR_InPanelStore && !CutSheet.TLCUTSH_OnHold
                            select new { Pk = CutSheet.TLCutSH_Pk, Description = CutSheet.TLCutSH_No };

                foreach (var row in Query)
                {
                    cmboCutSheet.Items.Add(row);
                }

                oCmboA.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_ShortCode).ToList();
                oCmboA.ValueMember = "TLADMBT_Pk";
                oCmboA.DisplayMember = "TLADMBT_Description";

                cmboCutSheet.ValueMember = "Pk";
                cmboCutSheet.DisplayMember = "Description";
                cmboCutSheet.SelectedValue = -1;
            }

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
                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
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
                Object tst = oCmbo.SelectedItem;
                foreach (PropertyInfo prop in tst.GetType().GetProperties())
                {
                    if (prop.Name == "Pk")
                    {
                        CutSheetSelected = Convert.ToInt32(prop.GetValue(tst));
                    }
                }
            }

            if (CutSheetSelected != 0)
            {
                dataGridView1.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    TLCUT_CutSheetReceipt CSR = new TLCUT_CutSheetReceipt();
                    CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheetSelected).FirstOrDefault();
                    if (CSR != null)
                    {
                        IList<TLCUT_CutSheetReceiptDetail> CSRD = new List<TLCUT_CutSheetReceiptDetail>();
                        CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                        if (CSRD != null)
                        {
                            foreach (var row in CSRD)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = row.TLCUTSHRD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = row.TLCUTSHRD_Description;
                                dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_BundleQty;
                                
                                if (row.TLCUTSHRD_BoxNumber != null)
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUTSHRD_BoxNumber;
                                else
                                    dataGridView1.Rows[index].Cells[3].Value = row.TLCUTSHRD_Description;

                                dataGridView1.Rows[index].Cells[4].Value = row.TLCUTSHRD_BoxUnits;

                                /*
                                if(row.TLCUTSHRD_BoxType_FK != 0)
                                     dataGridView1.Rows[index].Cells[5].Value = row.TLCUTSHRD_BoxType_FK;
                                else
                                    dataGridView1.Rows[index].Cells[5].Value = 1;
                                 * */

                            }

                            if (this.dataGridView1.Rows.Count != 0)
                            {
                                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[4];
                                this.dataGridView1.BeginEdit(true);
                            }
                        }
                    }
                    var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                    if (CSB != null)
                    {
                        txtAdultBoxes.Text = CSB.TLCUTSHB_AdultBoxes.ToString();
                        txtBinding.Text = CSB.TLCUTSHB_Binding.ToString();
                        txtKidsBoxes.Text = CSB.TLCUTSHB_KidBoxes.ToString();
                        txtRibbing.Text = CSB.TLCUTSHB_Ribbing.ToString();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLCUT_CutSheetReceipt CSR = null;

            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();

                    if (CutSheetSelected != 0)
                    {
                        CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheetSelected).FirstOrDefault();
                        if (CSR != null)
                        {
                            CSR.TLCUTSHR_InBundleStore = false;
                            CSR.TLCUTSHR_InPanelStore = true;
                            CSR.TLCUTSHR_DateIntoPanelStore = dtpTransActionDate.Value;
 
                            var CS = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK);
                            if (CS != null)
                            {
                                if (rtbAdditional.Text.Length != 0)
                                    CS.TLCUTSH_AddNotes = rtbAdditional.Text;

                                CS.TLCUTSH_Completed_Date = DateTime.Now;
                                CSR.TLCUTSHR_Colour_FK = CS.TLCutSH_Colour_FK;
                                var CutStore = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == CS.TLCutSH_Department_FK && x.WhStore_PanelStore).FirstOrDefault();
                                if (CutStore != null)
                                {
                                    CSR.TLCUTSHR_WhsePanStore_FK = CutStore.WhStore_Id;
                                    CSR.TLCUTSHR_DateIntoPanelStore = dtpTransActionDate.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a cutsheet from the drop down box");
                        return;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        TLCUT_CutSheetReceiptDetail CSRD = new TLCUT_CutSheetReceiptDetail();

                        var index = (int)row.Cells[0].Value;
                        CSRD = context.TLCUT_CutSheetReceiptDetail.Find(index);

                        if (CSRD != null)
                        {
                            CSRD.TLCUTSHRD_BoxNumber = dataGridView1.Rows[row.Index].Cells[3].Value.ToString();
                            if ((int)dataGridView1.Rows[row.Index].Cells[4].Value == 0)
                                continue;

                            CSRD.TLCUTSHRD_BoxUnits = (int)dataGridView1.Rows[row.Index].Cells[4].Value;
                            CSRD.TLCUTSHRD_BoxType_FK = 1; // removed to speed up data capture (int)dataGridView1.Rows[row.Index].Cells[5].Value;
                            CSRD.TLCUTSHRD_InBundleStore = false;
                            CSRD.TLCUTSHRD_PanelDate = dtpTransActionDate.Value;

                            if (Dept != null)
                            {
                                var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 600).FirstOrDefault();
                                if (TranType != null)
                                {
                                    CSRD.TLCUTSHRD_TransactionType = TranType.TrxT_Pk;
                                    CSRD.TLCUTSHRD_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                }
                            }
                         }
                   }

                    var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_Pk == CSR.TLCUTSHR_Pk).FirstOrDefault();
                    if (CSB != null)
                    {
                        int value = 0;
                        if (int.TryParse(txtAdultBoxes.Text, out value))
                        {
                                CSB.TLCUTSHB_AdultBoxes = value;
                        }
                        

                        value = 0;
                        if (int.TryParse(txtBinding.Text, out value))
                        {
                            CSB.TLCUTSHB_Binding = value;
                        }
               
                        value = 0;
                        if (int.TryParse(txtKidsBoxes.Text, out value))
                        {
                            CSB.TLCUTSHB_KidBoxes = value;
                        }
                        value = 0;
                        if (int.TryParse(txtRibbing.Text, out value))
                        {
                            CSB.TLCUTSHB_Ribbing = value;
                        }

                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        dataGridView1.Rows.Clear();
                        
                        //This form is the summary form for Mary in the accessories store
                        //===================================================
                        frmCutViewRep vRep = new frmCutViewRep(11, CutSheetSelected);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog();
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }
                        //This form is the CMT Issued to line document 
                        //==============================================================
                        vRep = new frmCutViewRep(14, CutSheetSelected);
                        h = Screen.PrimaryScreen.WorkingArea.Height;
                        w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog();
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                if (dataGridView1.CurrentCell.ReadOnly)
                    dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
            else if (oDgv != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (dataGridView1.CurrentCell.ReadOnly)
                        dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                    e.Handled = true;
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
                    oCmboA.DataSource = null;
                    oCmboA.Items.Clear();
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var Query = from CutSheet in context.TLCUT_CutSheet
                                    join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                                    where CutSheet.TLCutSH_Accepted && CutReceipt.TLCUTSHR_InBundleStore && !CutReceipt.TLCUTSHR_InPanelStore
                                    select new { Pk = CutSheet.TLCutSH_Pk, Description = CutSheet.TLCutSH_No };

                        foreach (var row in Query)
                        {
                            cmboCutSheet.Items.Add(row);
                        }

                        oCmboA.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_ShortCode).ToList();
                        oCmboA.ValueMember = "TLADMBT_Pk";
                        oCmboA.DisplayMember = "TLADMBT_Description";

                        cmboCutSheet.ValueMember = "Pk";
                        cmboCutSheet.DisplayMember = "Description";
                        cmboCutSheet.SelectedValue = -1;
                    }
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
                    oCmboA.DataSource = null;
                    oCmboA.Items.Clear();
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var Query = from CutSheet in context.TLCUT_CutSheet
                                    join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                                    where CutSheet.TLCutSH_Accepted && !CutReceipt.TLCUTSHR_InBundleStore && CutReceipt.TLCUTSHR_InPanelStore && !CutReceipt.TLCUTSHR_Issued
                                    select new { Pk = CutSheet.TLCutSH_Pk, Description = CutSheet.TLCutSH_No };

                        foreach (var row in Query)
                        {
                            cmboCutSheet.Items.Add(row);
                        }

                        oCmboA.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_ShortCode).ToList();
                        oCmboA.ValueMember = "TLADMBT_Pk";
                        oCmboA.DisplayMember = "TLADMBT_Description";

                        cmboCutSheet.ValueMember = "Pk";
                        cmboCutSheet.DisplayMember = "Description";
                        cmboCutSheet.SelectedValue = -1;
                    }

                    formloaded = true;
                }
            }
        }
    }
}
