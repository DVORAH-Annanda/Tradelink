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

namespace CMT
{
    public partial class frmBodyTMeasureRP : Form
    {
        int _CutSheet;
        int ActiveRow;
        bool RowLeave;

        string[][] MandatoryFields;
        bool[] MandSelected;

        bool formloaded;

        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();  // 0 : Pk
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();  // 1 : Measurement Pk 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();  // 2 : Bundle No 
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();  // 3 : Measurement Description
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();  // 4 : Required Measurement 
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();  // 5 : Top
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();  // 6 : Middele
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();  // 7 : Bottom
        public frmBodyTMeasureRP(int CutSheet)
        {
            InitializeComponent();

            core = new Util();

            _CutSheet = CutSheet;

            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.Visible = false;
            oTxtB.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Bundle No";
            oTxtC.ReadOnly = true;
            oTxtC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Measurement Description";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Required Measurement";
            oTxtE.ValueType = typeof(int);
            oTxtE.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF.HeaderText = "Top";
            oTxtF.ValueType = typeof(decimal);
            dataGridView1.Columns.Add(oTxtF);

            oTxtG.HeaderText = "Middle";
            oTxtG.ValueType = typeof(Decimal);
            dataGridView1.Columns.Add(oTxtG);

            oTxtH.HeaderText = "Bottom";
            oTxtH.ValueType = typeof(Decimal);
            dataGridView1.Columns.Add(oTxtH);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

        }

        private void BodyTMeasureRP_Load(object sender, EventArgs e)
        {
            IList<TLADM_CMTMeasurementPoints> MeasurePoints = new List<TLADM_CMTMeasurementPoints>();
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                var CutSheet = context.TLCUT_CutSheet.Find(_CutSheet);
                if (CutSheet != null)
                {
                    txtCutSheet.Text = CutSheet.TLCutSH_No;

                    var MeasureValues = context.TLCMT_AuditMeasurements.Where(x => x.CMTBFA_Customer_FK == CutSheet.TLCutSH_Customer_FK &&
                                                                          x.CMTBFA_Size_FK == CutSheet.TLCutSH_Size_FK &&
                                                                          x.CMTBFA_Style_FK == CutSheet.TLCutSH_Styles_FK).ToList();

                    MeasurePoints = context.TLADM_CMTMeasurementPoints.Where(x => x.CMTMP_B2MRawPanels).ToList();


                    var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                    if (CSR != null)
                    {
                        var CSD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).OrderBy(x => x.TLCUTSHRD_Description).ToList();
                        foreach (var row in CSD)
                        {
                            foreach (var MP in MeasurePoints)
                            {
                                //-----------------------------------------------------------
                                // 0 : Pk
                                // 1 : Measurement Pk 
                                // 2 : Bundle No 
                                // 3 : Measurement Description
                                // 4 : Required Measurement 
                                // 5 : Top
                                // 6 : Middle
                                // 7 : Bottom
                                //------------------------------------------------------
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[1].Value = MP.CMTMP_Pk;
                                dataGridView1.Rows[index].Cells[2].Value = row.TLCUTSHRD_Description;
                                dataGridView1.Rows[index].Cells[3].Value = MP.CMTMP_Description;
                                
                                var result = (from u in MeasureValues
                                              where u.CMTBFA_MeasureP_FK == MP.CMTMP_Pk
                                              select u).FirstOrDefault();
                                if(result != null)
                                    dataGridView1.Rows[index].Cells[4].Value = result.CMTBFA_Measurement;
                                else
                                    dataGridView1.Rows[index].Cells[4].Value = 0;

                                dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[7].Value = 0.00M;
                            }
                           
                        }
                    }

                    var Existing = context.TLCMT_BodyMeasureRP.Where(x => x.TLBTMRP_CutSheet_PK == _CutSheet).ToList();
                    foreach (var row in Existing)
                    {
                         var SingleRow = (
                         from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                         where (int)Rows.Cells[1].Value == row.TLBMPRP_Measurement_FK
                         select Rows).FirstOrDefault();

                         if (SingleRow != null)
                         {
                              //---------------------------------------------
                              // 0 : Pk
                              // 1 : Measurement Pk 
                              // 2 : Bundle No 
                              // 3 : Measurement Description
                              // 4 : Required Measurement 
                              // 5 : Top
                              // 6 : Middle
                              // 7 : Bottom
                             //-----------------------------------------------------------------
                             SingleRow.Cells[0].Value = row.TLBTMRP_Pk;
                             SingleRow.Cells[4].Value = row.TLBMPRP_RequiredMeasure;
                             SingleRow.Cells[5].Value = row.TLBMPRP_Top;
                             SingleRow.Cells[6].Value = row.TLBMPRP_Middle;
                             SingleRow.Cells[7].Value = row.TLBMPRP_Bottom;
                         }
                    }

                    if (this.dataGridView1.Rows.Count != 0)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[5];
                        this.dataGridView1.BeginEdit(true);
                    }
                }
            }

            MandatoryFields = new string[][]
                {   new string[] {"4", "Please enter a required measurement", "0"},
                    new string[] {"5", "Please enter a Top measurement", "1"},
                    new string[] {"6", "Please enter a Middle measurement", "2"}, 
                    new string[] {"7", "Please enter a bottom measurement", "3"}, 
                };
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            
            formloaded = true;
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
                        if (!RowLeave)
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
                                if (!RowLeave)
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
                                if (!RowLeave)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        bool add = true;
                        TLCMT_BodyMeasureRP Body = new TLCMT_BodyMeasureRP();

                        if (row.Cells[0].Value != null)
                        {
                            var index = (int)row.Cells[0].Value;
                            Body = context.TLCMT_BodyMeasureRP.Find(index);
                            if (Body == null)
                                Body = new TLCMT_BodyMeasureRP();
                            else
                                add = false;
                        }
                        //---------------------------------------------
                        // 0 : Pk
                        // 1 : Measurement Pk 
                        // 2 : Bundle No 
                        // 3 : Measurement Description
                        // 4 : Required Measurement 
                        // 5 : Top
                        // 6 : Middle
                        // 7 : Bottom
                        //-----------------------------------------------------------------
                        Body.TLBMPRP_Measurement_FK = (int)row.Cells[1].Value;
                        Body.TLBMPRP_BundleNo = row.Cells[2].Value.ToString();
                        Body.TLBMPRP_RequiredMeasure = (int)row.Cells[4].Value;
                        Body.TLBMPRP_Top = (decimal)row.Cells[5].Value;
                        Body.TLBMPRP_Middle = (decimal)row.Cells[6].Value;
                        Body.TLBMPRP_Bottom = (decimal)row.Cells[7].Value;
                        Body.TLBMPRP_TransDate = dtpTransDate.Value;
                        Body.TLBTMRP_CutSheet_PK = _CutSheet;

                        if (add)
                            context.TLCMT_BodyMeasureRP.Add(Body);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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

                        if (Cell.ColumnIndex == 4 ||
                            Cell.ColumnIndex == 5 ||
                            Cell.ColumnIndex == 6 ||
                            Cell.ColumnIndex == 7)
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
        }
    }
}
