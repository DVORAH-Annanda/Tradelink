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
using System.Reflection;

namespace CMT
{
    public partial class frmLineFeederBC : Form
    {
        bool formloaded;
        Util core;

        bool RowLeave;

        int ActiveRow;

        DataGridViewTextBoxColumn  oTxtA = new DataGridViewTextBoxColumn();  // 0 : Pk
        DataGridViewTextBoxColumn  oTxtB = new DataGridViewTextBoxColumn();  // 1 : Bundle No 
        DataGridViewTextBoxColumn  oTxtC = new DataGridViewTextBoxColumn();  // 2 : Bundle Qty 
        DataGridViewTextBoxColumn  oTxtF = new DataGridViewTextBoxColumn();  // 5 : Difference Qty
        DataGridViewTextBoxColumn  oTxtD = new DataGridViewTextBoxColumn();  // 3 : Sleeve Qty 
        DataGridViewTextBoxColumn  oTxtE = new DataGridViewTextBoxColumn();  // 4 : Labels Qty
        DataGridViewTextBoxColumn  oTxtG = new DataGridViewTextBoxColumn();  // 6 : Sizes
        DataGridViewComboBoxColumn oTxtH = new DataGridViewComboBoxColumn(); // 7 : Sizes (FK); 

        string[][] MandatoryFields;
        bool[] MandSelected;


        public frmLineFeederBC()
        {
            InitializeComponent();
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            core = new Util();
        }

        private void frmLineFeederBC_Load(object sender, EventArgs e)
        {
            IList<TLADM_MachineOperators> Supervisors = new List<TLADM_MachineOperators>();

            formloaded = false;
            
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Bundle Number";
            oTxtB.ReadOnly = true;
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Body Qty";
            oTxtC.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtC);

            oTxtF.HeaderText = "Actual";
            oTxtF.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtF);
                       
            oTxtD.HeaderText = "Sleeve Qty";
            oTxtD.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Labels Qty";
            oTxtE.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtE);

            oTxtG.HeaderText = "Sizes";
            oTxtG.ValueType = typeof(String);
            oTxtG.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtG);

            oTxtH.HeaderText = "Sizes FK";
            oTxtH.ValueType = typeof(int);
            oTxtH.ReadOnly = true;
            oTxtH.Visible = false;
            dataGridView1.Columns.Add(oTxtH);
            
            // oCmbA.HeaderText = "Size";
            // oCmbA.ValueMember = "SI_Id";
            // oCmbA.DisplayMember = "SI_Description";

            cmboSupervisor.DisplayMember = "MachOp_Description";
            cmboSupervisor.ValueMember = "MachOp_Pk";

            // dataGridView1.Columns.Add(oCmbA);
 
            using (var context = new TTI2Entities())
            {
                var Query = (from LineIssue in context.TLCMT_LineIssue
                            join FactConfig in context.TLCMT_FactConfig on LineIssue.TLCMTLI_LineNo_FK equals FactConfig.TLCMTCFG_Pk
                            where LineIssue.TLCMTLI_IssuedToLine && !LineIssue.TLCMTLI_WorkCompleted 
                            select new { Pk = LineIssue.TLCMTLI_LineNo_FK , Description = FactConfig.TLCMTCFG_Description}).GroupBy(x=>x.Pk);

                foreach (var row in Query)
                {
                    cmboLineIssue.Items.Add(row.FirstOrDefault());
                }

                cmboLineIssue.ValueMember = "Pk";
                cmboLineIssue.DisplayMember = "Description";
                cmboLineIssue.SelectedValue = -1;

                var Depts = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                foreach (var Dept in Depts)
                {
                    var Inspectors = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && x.MachOp_Inspector && !x.MachOp_Discontinued).ToList();
                    foreach (var Inspector in Inspectors)
                    {
                        var Insp = context.TLADM_MachineOperators.Find(Inspector.MachOp_Pk);
                        if (Insp != null)
                        {
                            Supervisors.Add(Insp);
                        }
                    }
                }

                cmboSupervisor.DataSource = Supervisors;
                cmboSupervisor.SelectedValue = -1;

                MandatoryFields = new string[][]
                {   new string[] {"2", "Please enter a body quantity", "0"},
                    new string[] {"4", "Please enter a sleeve quantity", "1"},
                    new string[] {"5", "Please enter a label quantity", "2"}, 
                    new string[] {"6", "Please select a size", "3"}, 
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                formloaded = true;

            }
        }

        private void cmboLineIssue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            int CutSheetPK = 0;
            IList<TLADM_Sizes> Sizes = new List<TLADM_Sizes>();

            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                      dataGridView1.Rows.Clear();
                    
                      var tst = (Object)oCmbo.SelectedItem;
                      foreach (PropertyInfo prop in tst.GetType().GetProperties())
                      {
                          if (prop.Name == "Pk")
                          {
                              CutSheetPK = Convert.ToInt32(prop.GetValue(tst));
                          }
                     }

                      if (CutSheetPK != 0)
                      {
                           var Query = from LineIssue in context.TLCMT_LineIssue
                                       join CutSH in context.TLCUT_CutSheet on LineIssue.TLCMTLI_CutSheet_FK equals CutSH.TLCutSH_Pk 
                                       where LineIssue.TLCMTLI_LineNo_FK == CutSheetPK 
                                       select CutSH;
                          
                          formloaded = false;

                          cmboCutSheet.DataSource = null;
                          cmboCutSheet.DataSource = Query.ToList();
                          cmboCutSheet.ValueMember = "TLCutSH_Pk";
                          cmboCutSheet.DisplayMember = "TLCutSH_No";
                          cmboCutSheet.SelectedValue = -1;

                          formloaded = true;
                          /*
                          var CutSheet = context.TLCUT_CutSheet.Find(CutSheetPK);
                          if (CutSheet != null)
                          {
                              // txtCutSheet.Text = CutSheet.TLCutSH_No;
                              txtDescription.Text = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                              
                              var DB = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                              if (DB != null)
                              {
                                  txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                              }

                              //------------------------------------------
                              // 0 : Pk
                              // 1 : Bundle No 
                              // 2 : Bundle Qty 
                              // 3 : Difference Qty
                              // 4 : Sleeve Qty 
                              // 5 : Labels Qty
                              // 6 : Sizes
                              // 7 : Sizes FK (Not Visible);
                              //-------------------------------------------------------
  
                              var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CustSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                              if (CSR != null)
                              {
                                  var CSD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).OrderBy(x=>x.TLCUTSHRD_Description).ToList();
                                  foreach (var row in CSD)
                                  {
                                      var index = dataGridView1.Rows.Add();
                                      dataGridView1.Rows[index].Cells[1].Value = row.TLCUTSHRD_Description;
                                      dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_BundleQty;
                                      dataGridView1.Rows[index].Cells[3].Value = 0;
                                      dataGridView1.Rows[index].Cells[4].Value = 0;
                                      dataGridView1.Rows[index].Cells[5].Value = 0;
                                      dataGridView1.Rows[index].Cells[6].Value = context.TLADM_Sizes.Find(row.TLCUTSHRD_Size_FK).SI_Description;
                                      dataGridView1.Rows[index].Cells[7].Value = row.TLCUTSHRD_Size_FK;

                                  }
                              }

                              formloaded = false;
                              var Existing = context.TLCMT_LineFeederBundleCheck.Where(x => x.TLCMTLF_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                              if (Existing.Count != 0)
                              {
                                  cmboSupervisor.SelectedValue = Existing.FirstOrDefault().TLCMTLF_Operator_FK;
                              }
                              foreach (var row in Existing)
                              {
                                  var SingleRow = (
                                       from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                       where (string)Rows.Cells[1].Value == row.TLCMTLF_Bundle_No
                                       select Rows).FirstOrDefault();

                                  if (SingleRow != null)
                                  {
                                      SingleRow.Cells[0].Value = row.TLCMTLF_Pk;
                                      SingleRow.Cells[2].Value = row.TLCMTLF_Body_Qty;
                                      SingleRow.Cells[3].Value = row.TLCMTLF_Difference;
                                      SingleRow.Cells[4].Value = row.TLCMTLF_Sleeve_Qty;
                                      SingleRow.Cells[5].Value = row.TLCMTLF_Labels_Qty;
                                      try
                                      {
                                          SingleRow.Cells[6].Value =  context.TLADM_Sizes.Find( row.TLCMTLF_Size_FK).SI_Description;
                                          SingleRow.Cells[7].Value = row.TLCMTLF_Size_FK;
                                      }
                                      catch (Exception ex)
                                      {
                                          MessageBox.Show(ex.Message);
                                      }
                                   }
                              }

                              if (this.dataGridView1.Rows.Count != 0)
                              {
                                  this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[2];
                                  this.dataGridView1.BeginEdit(true);
                              }
                              formloaded = true;
                          }
                           */
 
                      }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null)
            {
                if (formloaded)
                {
                    if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                    {
                        var Cell = oDgv.CurrentCell;

                        if (Cell.ColumnIndex == 2 || 
                            Cell.ColumnIndex == 3 || 
                            Cell.ColumnIndex == 4 ||
                            Cell.ColumnIndex == 5)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownTS);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownTS);
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
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add = true;
            
            bool[] complete = null;

            if (oBtn != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Production Line and / or CutSheet  from the drop down box");
                    return;
                }

                var Operator = cmboSupervisor.SelectedItem;
                if (Operator == null)
                {
                    MessageBox.Show("Please select an Supervisor from the drop down box");
                    return;
                }

                using ( var context = new TTI2Entities())
                {
                    TLCMT_LineIssue LineIssue = context.TLCMT_LineIssue.Where(x=>x.TLCMTLI_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        Add = true;
                    
                        TLCMT_LineFeederBundleCheck lfBundle = new TLCMT_LineFeederBundleCheck();

                        if (row.Cells[0].Value != null)
                        {
                            int index = (int)row.Cells[0].Value;
                            lfBundle = context.TLCMT_LineFeederBundleCheck.Find(index);
                            if (lfBundle == null)
                            {
                                lfBundle = new TLCMT_LineFeederBundleCheck();
                            }
                            else
                            {
                                Add = false;
                            }
                        }

                     
                        //---------------------------------------------
                        // 0 : Pk
                        // 1 : Bundle No 
                        // 2 : Bundle Qty 
                        // 3 : Difference Qty
                        // 4 : Sleeve Qty 
                        // 5 : Labels Qty
                        // 6 : Sizes
                        // 7 : Sizes (Not Visible...FK); 
                        //---------------------------------------------------------------

                        complete = core.RowComplete(row, MandatoryFields);
                        var errorM = core.returnMessage(complete, true, MandatoryFields);
                        if (!string.IsNullOrEmpty(errorM))
                        {
                            errorM = errorM + Environment.NewLine + "Process will now be aborted";
                            MessageBox.Show(errorM, "Error Message on Line " + (1 + row.Index).ToString());
                            return;
                        }

                        lfBundle.TLCMTLF_Operator_FK = (int)cmboSupervisor.SelectedValue;
                        lfBundle.TLCMTLF_Bundle_No = row.Cells[1].Value.ToString();
                        lfBundle.TLCMTLF_Body_Qty = (int)row.Cells[2].Value;
                        lfBundle.TLCMTLF_Difference = (int)row.Cells[3].Value;
                        lfBundle.TLCMTFL_TransDate = dtpTransDate.Value;

                        if (LineIssue != null)
                        {
                            lfBundle.TLCMTLF_LineNo_FK = LineIssue.TLCMTLI_LineNo_FK;
                            lfBundle.TLCMTLF_Facility_FK = LineIssue.TLCMTLI_CmtFacility_FK;
                            lfBundle.TLCMTLF_CutSheet_FK = LineIssue.TLCMTLI_CutSheet_FK;
                        }

                        lfBundle.TLCMTLF_Sleeve_Qty = (int)row.Cells[4].Value;
                        lfBundle.TLCMTLF_Labels_Qty = (int)row.Cells[5].Value;
                        lfBundle.TLCMTLF_Size_FK = (int)row.Cells[7].Value;

                        var CS = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK);
                        if (CS != null)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                lfBundle.TLCMTLF_Colour_FK = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Id; 
                            }
                        }
                            
                        if(Add)
                            context.TLCMT_LineFeederBundleCheck.Add(lfBundle);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        dataGridView1.Rows.Clear();
                        formloaded = false;
                        
                        cmboLineIssue.SelectedValue = -1;
                        cmboSupervisor.SelectedValue = -1;
                        
                        txtColour.Text = string.Empty;
                        cmboCutSheet.SelectedValue = -1; //  txtCutSheet.Text = string.Empty;
                        txtDescription.Text = string.Empty;

                        formloaded = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnBodyToMeasure_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
           
            if (oBtn != null && formloaded)
            {
                if (cmboLineIssue.SelectedItem == null)
                {
                    MessageBox.Show("Please select a production Line");
                    return;
                }

                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Cut Sheet from the drop down provided");
                    return;
                }

                try
                {
                    frmBodyTMeasureRP BodyMeasure = new frmBodyTMeasureRP(selected.TLCutSH_Pk);
                    BodyMeasure.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());
                            var locRec = context.TLCMT_LineFeederBundleCheck.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLCMT_LineFeederBundleCheck.Remove(locRec);
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                        }
                        oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;
            RowLeave = false;

            if (oDgv != null && formloaded)
            {
                if (oDgv.CurrentCell != null)
                {
                    ActiveRow = oDgv.CurrentCell.RowIndex;
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        complete = core.RowComplete(CurrentRow, MandatoryFields);
                    }

                    MandSelected = complete;
                    var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                    if (!string.IsNullOrEmpty(errorM))
                    {
                            MessageBox.Show(errorM, "Error Message");
                            RowLeave = true;
                    }
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                btnSave.Enabled = true;

                var cell = oDgv.CurrentCell;

                var result = (from u in MandatoryFields
                              where u[0] == cell.ColumnIndex.ToString()
                              select u).FirstOrDefault();
                if (result != null)
                {
                    //----------------------------------------------------------------
                    // This column needs to be verified 
                    //-------------------------------------------------------------
                    if (String.IsNullOrEmpty(cell.EditedFormattedValue.ToString()))
                    {
                        if(!RowLeave)
                            MessageBox.Show(result[1].ToString());
                        
                        e.Cancel = true;
                        RowLeave = false;
                        return;
                    }
                    else if (cell.ValueType == typeof(int))
                    {
                        //--------------------------------------------------------------
                        // this is a numeric 
                        //------------------------------------------------------------
                        if (core.IsValueDidgit(cell.EditedFormattedValue.ToString()))
                        {
                            if (int.Parse(cell.EditedFormattedValue.ToString()) == 0)
                            {
                                if(!RowLeave)
                                    MessageBox.Show(result[1].ToString());
                                
                                e.Cancel = true;
                                RowLeave = false;
                                btnSave.Enabled = false;
                                return;
                            }
                        }
                    }
                    else if (cell.ValueType == typeof(decimal))
                    {
                        //--------------------------------------------------------------
                        // this is a decimal 
                        //------------------------------------------------------------
                        if (core.IsValueDidgit(cell.EditedFormattedValue.ToString()))
                        {
                            if (decimal.Parse(cell.EditedFormattedValue.ToString()) == 0)
                            {
                                if(!RowLeave)
                                    MessageBox.Show(result[1].ToString());
                                
                                btnSave.Enabled = false;
                                RowLeave = false;
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
        }
        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            

            if (oDgv != null && formloaded)
            {
                

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
                var CurrentCell = oDgv.CurrentCell;
                if (CurrentCell.ReadOnly && dataGridView1.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1].Visible)
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

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var CutSheet = context.TLCUT_CutSheet.Find(selected.TLCutSH_Pk);
                        if (CutSheet != null)
                        {
                            txtDescription.Text = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;

                            var DB = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                            if (DB != null)
                            {
                                txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                            }

                            //------------------------------------------
                            // 0 : Pk
                            // 1 : Bundle No 
                            // 2 : Bundle Qty 
                            // 3 : Difference Qty
                            // 4 : Sleeve Qty 
                            // 5 : Labels Qty
                            // 6 : Sizes
                            // 7 : Sizes FK (Not Visible);
                            //-------------------------------------------------------

                            var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                            if (CSR != null)
                            {
                                var CSD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).OrderBy(x => x.TLCUTSHRD_Description).ToList();
                                foreach (var row in CSD)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[1].Value = row.TLCUTSHRD_Description;
                                    dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_BundleQty;
                                    dataGridView1.Rows[index].Cells[3].Value = 0;
                                    dataGridView1.Rows[index].Cells[4].Value = 0;
                                    dataGridView1.Rows[index].Cells[5].Value = 0;
                                    dataGridView1.Rows[index].Cells[6].Value = context.TLADM_Sizes.Find(row.TLCUTSHRD_Size_FK).SI_Description;
                                    dataGridView1.Rows[index].Cells[7].Value = row.TLCUTSHRD_Size_FK;

                                }
                            }

                            formloaded = false;
                            var Existing = context.TLCMT_LineFeederBundleCheck.Where(x => x.TLCMTLF_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                            if (Existing.Count != 0)
                            {
                                cmboSupervisor.SelectedValue = Existing.FirstOrDefault().TLCMTLF_Operator_FK;
                            }
                            foreach (var row in Existing)
                            {
                                var SingleRow = (
                                     from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                     where (string)Rows.Cells[1].Value == row.TLCMTLF_Bundle_No
                                     select Rows).FirstOrDefault();

                                if (SingleRow != null)
                                {
                                    SingleRow.Cells[0].Value = row.TLCMTLF_Pk;
                                    SingleRow.Cells[2].Value = row.TLCMTLF_Body_Qty;
                                    SingleRow.Cells[3].Value = row.TLCMTLF_Difference;
                                    SingleRow.Cells[4].Value = row.TLCMTLF_Sleeve_Qty;
                                    SingleRow.Cells[5].Value = row.TLCMTLF_Labels_Qty;
                                    try
                                    {
                                        SingleRow.Cells[6].Value = context.TLADM_Sizes.Find(row.TLCMTLF_Size_FK).SI_Description;
                                        SingleRow.Cells[7].Value = row.TLCMTLF_Size_FK;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }

                            if (this.dataGridView1.Rows.Count != 0)
                            {
                                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[2];
                                this.dataGridView1.BeginEdit(true);
                            }
                            formloaded = true;
                        }

                    }
                }
                
            }
        }
    }
}
