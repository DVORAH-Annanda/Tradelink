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
    public partial class frmCompleted : Form
    {
        bool formloaded;
        
        DataGridViewTextBoxColumn oTxtA;     // Bundle datagridView1
        DataGridViewTextBoxColumn oTxtB;     // Style datagridView1
        DataGridViewTextBoxColumn oTxtC;     // Colour datagridView1
        //DataGridViewTextBoxColumn oTxtD;   // Size datagridView1
        DataGridViewTextBoxColumn oTxtE;     // Qty datagridView1

        DataGridViewTextBoxColumn oTxtAA;    // 0 Box No datagridView2
        DataGridViewTextBoxColumn oTxtAB;    // 1 Code datagridView2
        DataGridViewTextBoxColumn oTxtAC;    // 2 Size datagridView2
        DataGridViewTextBoxColumn oTxtAD;    // 3 Grade datagridView2
        DataGridViewTextBoxColumn oTxtAE;    // 4 Qty datagridView2
        DataGridViewTextBoxColumn oTxtAF;    // 5 Weight dataGridView2
        DataGridViewTextBoxColumn oTxtAG;    // 6 FK CutSheetSheet Receipt Detail datagridView2

        DataGridViewTextBoxColumn oTxtBA;    // Fault Description Pk datagridView3
        DataGridViewTextBoxColumn oTxtBB;    // Fault Description datagridView3
        DataGridViewTextBoxColumn oTxtBC;    // Fault Qty dataGridView3

        DataGridViewComboBoxColumn oCmboA;   // Sizes  = new DataGridViewComboBoxColumn(); // 0 CutSheet
        //--------------------------------------------------
        TLADM_Styles Styles;
        TLADM_Colours Colours;
        TLADM_Sizes Sizes;
        TLADM_StylesGrades StyleGrades;
        TLCUT_CutSheet CS;
        TLCUT_CutSheetReceipt CSR;
        //---------------------------------------------------------------------------------

        bool lCancel;
 
        Util core;
        public frmCompleted()
        {
            InitializeComponent();
            core = new Util();

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Bundle";
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Style";
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Colour";
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            /*
            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Size";
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);
            */

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Qty";
            oTxtE.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtE);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            //----------------------- dataGridView2
            oTxtAA = new DataGridViewTextBoxColumn();
            oTxtAA.HeaderText = "Box No";
            oTxtAA.ValueType = typeof(string);
            oTxtAA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtAA);

            oTxtAB = new DataGridViewTextBoxColumn();
            oTxtAB.HeaderText = "Grade";
            dataGridView2.Columns.Add(oTxtAB);

            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Size";
            oCmboA.ValueMember = "SI_Id";
            oCmboA.DisplayMember = "SI_Description";
            dataGridView2.Columns.Add(oCmboA);

            oTxtAD = new DataGridViewTextBoxColumn();
            oTxtAD.HeaderText = "Pastel Number";
            oTxtAD.ValueType = typeof(string);
            oTxtAD.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtAD);

            oTxtAE = new DataGridViewTextBoxColumn();
            oTxtAE.HeaderText = "Qty";
            oTxtAE.ValueType = typeof(int);
            dataGridView2.Columns.Add(oTxtAE);

            oTxtAF = new DataGridViewTextBoxColumn();
            oTxtAF.HeaderText = "Weight";
            oTxtAF.ValueType = typeof(decimal);
            dataGridView2.Columns.Add(oTxtAF);

            oTxtAG = new DataGridViewTextBoxColumn();
            oTxtAG.HeaderText = "CutSheetReceiptFK";
            oTxtAG.ValueType = typeof(int);
            oTxtAG.Visible = false;
            oTxtAG.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtAG);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView2.AllowUserToAddRows = false;

            //----------------------- dataGridView3
            oTxtBA = new DataGridViewTextBoxColumn();
            oTxtBA.ValueType = typeof(int);
            oTxtBA.ReadOnly = true;
            oTxtBA.Visible = false;
            dataGridView3.Columns.Add(oTxtBA);

            oTxtBB = new DataGridViewTextBoxColumn();
            oTxtBB.HeaderText = "Description";
            oTxtBB.ReadOnly = true;
            dataGridView3.Columns.Add(oTxtBB);

            oTxtBC = new DataGridViewTextBoxColumn();
            oTxtBC.HeaderText = "Qty";
            oTxtBC.ValueType = typeof(int);
            dataGridView3.Columns.Add(oTxtBC);

            txtTPPanels.ReadOnly = false;
            txtTPPanels.KeyDown  += core.txtWin_KeyDownTS;
            txtTPPanels.KeyPress += core.txtWin_KeyPress;

            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.AllowUserToAddRows = false;

        }

        private void frmCompleted_Load(object sender, EventArgs e)
        {
            formloaded = false;
            IList<TLCMT_PanelIssue> PI = new List<TLCMT_PanelIssue>();
            IList<TLCMT_LineIssue> LI = new List<TLCMT_LineIssue>();
            lCancel = false;


            using (var context = new TTI2Entities())
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();

                var Existing = context.TLCMT_LineIssue.Where(x => !x.TLCMTLI_WorkCompleted && x.TLCMTLI_IssuedToLine && !x.TLCMTLI_OnHold).OrderBy(x=>x.TLCMTLI_CutSheetDetails).ToList();
                LI = Existing.Where(x => x.TLCMTLI_CutSheetDetails != null).ToList();
                cmboCMTIssue.DataSource = LI;
                cmboCMTIssue.ValueMember = "TLCMTLI_PK";
                cmboCMTIssue.DisplayMember = "TLCMTLI_CutSheetDetails";

                var ExistingFlaws = context.TLCMT_DeflectFlaw.ToList();
                foreach (var row in ExistingFlaws)
                {
                    var index = dataGridView3.Rows.Add();
                    this.dataGridView3.Rows[index].Cells[0].Value = row.TLCMTDF_Pk;
                    this.dataGridView3.Rows[index].Cells[1].Value = row.TLCMTDF_Description;
                    this.dataGridView3.Rows[index].Cells[2].Value = 0;
                }

                txtDifference.Text = "0";
                txtTotAGrade.Text  = "0";
                txtTotBGrade.Text  = "0";
                txtTPIssued.Text   = "0";
                txtTPPanels.Text   = "0";
                txtNoBoxes.Text    = "0";
                txtCutSheet.Text = String.Empty;
                txtDateRequired.Text = string.Empty;

                cmboBoxType.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_ShortCode).ToList();
                cmboBoxType.ValueMember = "TLADMBT_Pk";
                cmboBoxType.DisplayMember = "TLADMBT_Description";
                cmboBoxType.SelectedValue = -1;

                cmboCMTLine.DataSource = null;
                cmboCMTLine.Items.Clear();
                cmboCMTLine.SelectedIndex = -1;
            }

            formloaded = true;

        }

        private void cmboCMTIssue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
           
            var ComboOptions = new BindingList<KeyValuePair<int, string>>();
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCMT_LineIssue)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        //=========================================================================================
                        // As a result of the ill discipline at the CMT I now have to build in a check 
                        // to ensure that the user does not capture this twice 
                        //===========================================================
                        int MayBeCaptured = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_LineIssue_FK == selected.TLCMTLI_Pk).Count();
                        if (MayBeCaptured != 0)
                        {
                            MessageBox.Show("This Cut Sheet has already been captured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         
                            var LineIss = context.TLCMT_LineIssue.Find(selected.TLCMTLI_Pk);
                            if (LineIss != null)
                            {
                                LineIss.TLCMTLI_WorkCompleted = true;
                                LineIss.TLCMTLI_WorkCompletedDate = DateTime.Now;

                                context.SaveChanges();
                            }
                           
                            frmCompleted_Load(this, null);

                            return;

                        }
                        var Existing = context.TLCMT_LineIssue.Where(x=>x.TLCMTLI_Pk == selected.TLCMTLI_Pk).ToList();
                        foreach (var row in Existing)
                        {
                            var Desc = context.TLCMT_FactConfig.Find(row.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                            ComboOptions.Add(new KeyValuePair<int, string>(row.TLCMTLI_Pk, Desc));
                        }

                        formloaded = false;
                        cmboCMTLine.DataSource = ComboOptions;
                        cmboCMTLine.ValueMember = "Key";
                        cmboCMTLine.DisplayMember = "Value";
                        cmboCMTLine.SelectedIndex = -1;

                        formloaded = true;
                    }
                }
            }
        }

        private void cmboCMTLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (int)oCmbo.SelectedValue;
                if (selected != 0)
                {
                    formloaded = false;
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    formloaded = true;

                    txtDifference.Text = "0";
                    txtTotAGrade.Text  = "0";
                    txtTotBGrade.Text  = "0";
                    txtTPIssued.Text   = "0";
                    txtTPPanels.Text   = "0";
                    txtNoBoxes.Text    = "0";

                    using ( var context = new TTI2Entities())
                    {
                        var LI = context.TLCMT_LineIssue.Find(selected);
                        if (LI != null)
                        {
                            CS = context.TLCUT_CutSheet.Find(LI.TLCMTLI_CutSheet_FK);
                            if (CS != null)
                            {
                                CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                                if (CSR == null)
                                {
                                    MessageBox.Show("Technical error encounted. Qoute number 1400");
                                    frmCompleted_Load(this, null);
                                    return;
                                }
                                
                                //-------------------------------------------------------
                                //We have to check that all the BFA Data has been recorded 
                                //--------------------------------------------------------------------
                                int BFACount = context.TLCMT_AuditMeasureRecorded.Where(x => x.TLBFAR_CutSheet_FK == CS.TLCutSH_Pk).Count();
                                if (BFACount == 0)
                                {
                                    MessageBox.Show("Please enter the BFA Audit information" + Environment.NewLine + "using the facility provided");
                                    frmCompleted_Load(this, null);
                                    return;
                                }
                             
                                //------------------------------------------------------
                                // We have to now 
                                //=========================================
                                var ExpectUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                                foreach (var Unit in ExpectUnits)
                                {
                                    TLADM_Sizes Size = new TLADM_Sizes();
                                    Size.SI_Description = context.TLADM_Sizes.Find(Unit.TLCUTE_Size_FK).SI_Description;
                                    Size.SI_id = Unit.TLCUTE_Size_FK;

                                    oCmboA.Items.Add(Size);
                                }
                                //---------------------------------------------------------------
                                //If expected units data not available, for what ever reason, go back to the original order
                                //==========================================================================
                                if (oCmboA.Items.Count == 0)
                                {
                                    var CutSheetDetail = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).GroupBy(x => x.TLCUTSHRD_Size_FK);
                                    foreach (var Grouped in CutSheetDetail)
                                    {
                                            var Size_Pk = Grouped.FirstOrDefault().TLCUTSHRD_Size_FK;
                                            TLADM_Sizes Size = new TLADM_Sizes();
                                            Size.SI_Description = context.TLADM_Sizes.Find(Size_Pk).SI_Description;
                                            Size.SI_id = Size_Pk;

                                            oCmboA.Items.Add(Size);
                                    }
                                    
                                }

                                oCmboA.DisplayMember = "SI_Description";
                                oCmboA.ValueMember = "SI_Id";

                                txtCutSheet.Text = CS.TLCutSH_No;

                                var DB = context.TLDYE_DyeBatch.Find(CS.TLCutSH_DyeBatch_FK);
                                if (DB != null)
                                {
                                    var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                                    if (DO != null)
                                    {
                                        var dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CMTReqWeek);
                                        txtDateRequired.Text = dt.AddDays(5).ToString("dd/MM/yyyy");
                                    }
                                }

                                Styles      = context.TLADM_Styles.Find(CS.TLCutSH_Styles_FK);
                                Colours     = context.TLADM_Colours.Find(CS.TLCutSH_Colour_FK);
                                StyleGrades = context.TLADM_StylesGrades.Where(x=>x.TLSG_Style_Fk == CS.TLCutSH_Styles_FK).FirstOrDefault();

                                var Existing = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                               /* if (Existing != null)
                                {
                                    Sizes = context.TLADM_Sizes.Find(Existing.FirstOrDefault().TLCUTSHRD_Size_FK);
                                }*/

                                foreach (var row in Existing)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    this.dataGridView1.Rows[index].Cells[0].Value = row.TLCUTSHRD_BoxNumber;
                                    this.dataGridView1.Rows[index].Cells[1].Value = Styles.Sty_Description;
                                    this.dataGridView1.Rows[index].Cells[2].Value = Colours.Col_Display;
                                    this.dataGridView1.Rows[index].Cells[3].Value = row.TLCUTSHRD_BoxUnits;
                                }

                                txtTPIssued.Text = Existing.Sum(x => x.TLCUTSHRD_BoxUnits).ToString();

                                // 0 Box No                          datagridView2
                                // 1 Code                            datagridView2
                                // 2 Size                            datagridView2
                                // 3 Grade                           datagridView2
                                // 4 Qty                             datagridView2
                                // 5 Weight                          dataGridView2
                                // 6 FK CutSheetSheet Receipt Detail datagridView2
                                formloaded = false;
                                foreach (var row in Existing)
                                {
                                    var index = dataGridView2.Rows.Add();
                                    this.dataGridView2.Rows[index].Cells[0].Value = row.TLCUTSHRD_BoxNumber;
                                    this.dataGridView2.Rows[index].Cells[1].Value = string.Empty;
                                    this.dataGridView2.Rows[index].Cells[2].Value = null; // string.Empty;
                                    this.dataGridView2.Rows[index].Cells[3].Value = string.Empty;
                                    this.dataGridView2.Rows[index].Cells[4].Value = 0;
                                    this.dataGridView2.Rows[index].Cells[5].Value = 0.00M;
                                    this.dataGridView2.Rows[index].Cells[6].Value = row.TLCUTSHRD_Pk;
                                }
                                formloaded = true;

                                if (this.dataGridView2.Rows.Count != 0)
                                {
                                    this.dataGridView2.CurrentCell = this.dataGridView2.Rows[0].Cells[1];
                                    this.dataGridView2.BeginEdit(true);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                string headerText = dataGridView2.Columns[e.ColumnIndex].HeaderText;
                if (headerText.Equals("Size"))
                {
                    // Confirm that the cell is not empty.
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        dataGridView2.Rows[e.RowIndex].ErrorText =
                            "Please select a size";

                        lCancel = true;
                        e.Cancel = true;

                    }
                }
                else if (headerText.Equals("Qty"))
                {
                    // Confirm that the cell is not empty.
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        dataGridView2.Rows[e.RowIndex].ErrorText =
                            "Please enter a boxed quantity";

                        lCancel = true;
                        e.Cancel = true;

                    }
                    else
                    {
                        if (int.Parse(e.FormattedValue.ToString()) == 0)
                        {
                            dataGridView2.Rows[e.RowIndex].ErrorText =
                           "Please enter a boxed quantity";

                            lCancel = true;
                            e.Cancel = true;

                        }
                    }
                }
                else if (headerText.Equals("Weight"))
                {
                    // Confirm that the cell is not empty.
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        dataGridView2.Rows[e.RowIndex].ErrorText =
                            "Please enter the boxed weight";

                        lCancel = true;
                        e.Cancel = true;

                    }
                    else
                    {
                        try
                        {
                            if (decimal.Parse(e.FormattedValue.ToString()) <= 0.00M)
                            {
                                dataGridView2.Rows[e.RowIndex].ErrorText =
                               "Please enter a boxed weight";

                                lCancel = true;
                                e.Cancel = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

                    }
                }
            }
        }

       private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
               var CurrentCell = oDgv.CurrentCell;
               if (CurrentCell.ColumnIndex == 1)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.CharacterCasing = CharacterCasing.Upper;
                   
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownTS);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else if (CurrentCell.ColumnIndex == 4)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else if (CurrentCell.ColumnIndex == 5)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }
        }

        private void dataGridView2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null)
            {
                if (e.ColumnIndex == 1)
                {
                   
                }
                else if (e.ColumnIndex == 4)
                {
                    txtDifference.Text = "0";
                    txtTotAGrade.Text = "0";
                    txtTotBGrade.Text = "0";

                    int PanelsV = 0;

                    int.TryParse(txtTPPanels.Text, out PanelsV);

                    int TotalPIssued = Convert.ToInt32(txtTPIssued.Text);
                    int TotalAGrade  = (int)CellSum(true);
                    int TotalBgrade  = (int)CellSum(false);

                    txtTotBGrade.Text = CellSum(false).ToString(); 
                    txtTotAGrade.Text = CellSum(true).ToString(); 
                    txtTotBGrade.Text = CellSum(false).ToString(); 

                    txtDifference.Text = (TotalPIssued - (TotalAGrade + TotalBgrade + PanelsV)).ToString();
                }
            }
        }
        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var CurrentCell = oDgv.CurrentCell;
            
            string headerText = dataGridView2.Columns[e.ColumnIndex].HeaderText;

            if (!headerText.Equals("Pastel Number")) return;

            var CurrentRow = oDgv.CurrentRow;

            var PastelCode = Styles.Sty_PastelNo.ToString();
            PastelCode += Colours.Col_FinishedCode;

            if (CurrentRow.Cells[1].Value != null)
            {
                var Grade = CurrentRow.Cells[1].Value.ToString().ToUpper();
                if (Grade.Contains("A"))
                {
                    if (StyleGrades != null)
                        PastelCode += StyleGrades.TLSG_Grade_A;
                    else
                        PastelCode += Styles.Sty_PastelCode;

                    txtNoBoxes.Text = (1 + Convert.ToInt32(txtNoBoxes.Text)).ToString();

                }
                else
                {
                    if (StyleGrades != null)
                        PastelCode += StyleGrades.TLSG_Grade_B;
                    else
                        PastelCode += Styles.Sty_PastelCode;

                    txtNoBoxes.Text = (1 + Convert.ToInt32(txtNoBoxes.Text)).ToString();
                }
            }
            else
            {
                var Grade = CurrentRow.Cells[1].EditedFormattedValue.ToString().ToUpper();
                if (Grade.Contains("A"))
                {
                    if (StyleGrades != null)
                        PastelCode += StyleGrades.TLSG_Grade_A;
                    else
                        PastelCode += Styles.Sty_PastelCode;

                    txtNoBoxes.Text = (1 + Convert.ToInt32(txtNoBoxes.Text)).ToString();
                }
                else
                {
                    if (StyleGrades != null)
                        PastelCode += StyleGrades.TLSG_Grade_B;
                    else
                        PastelCode += Styles.Sty_PastelCode;

                    txtNoBoxes.Text = (1 + Convert.ToInt32(txtNoBoxes.Text)).ToString();
                }
            }

            using (var context = new TTI2Entities())
            {
                if (CurrentRow.Cells[6].Value != null)
                {
                    // int index = (int)CurrentRow.Cells[6].Value;
                    try
                    {
                        int index = (int)CurrentRow.Cells[2].Value;
                        if (index != 0)
                        {
                            PastelCode += context.TLADM_Sizes.Find(index).SI_PastelNo.ToString();
                            dataGridView2.Rows[CurrentRow.Index].Cells[3].Value = PastelCode;
                            // dataGridView2.Rows[CurrentRow.Index].Cells[2].Value = context.TLADM_Sizes.Find(CSRD.TLCUTSHRD_Size_FK).SI_Description;
                        }
                    }
                    catch (Exception Ex)
                    {
                    }
                }

            }
             
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            dataGridView2.Rows[e.RowIndex].ErrorText = String.Empty;
            DataGridView oDgv = sender as DataGridView;
            var CurrentCell = oDgv.CurrentCell;
                        
            if (CurrentCell.ColumnIndex == 4 && !lCancel)
            {
             
                txtDifference.Text = "0";
                txtTotAGrade.Text = "0";
                txtTotBGrade.Text = "0";
                
                var PPanels = 0;
                var PIssued = Convert.ToInt32(txtTPIssued.Text);
                int TotAGrade = (int)CellSum(true);
                int TotBGrade = (int)CellSum(false);

                if (this.txtTPPanels.Text.Contains("-"))
                {
                    var Index = this.txtTPPanels.Text.IndexOf("-");
                    var Value = this.txtTPPanels.Text.Remove(Index);
                    if (Value.Length > 0)
                        PPanels = Convert.ToInt32(Value) * -1;
                }
                else
                    if (this.txtTPPanels.TextLength > 0)
                        PPanels = Convert.ToInt32(this.txtTPPanels.Text);

                txtTotAGrade.Text = CellSum(true).ToString(); 
                txtTotBGrade.Text = CellSum(false).ToString();
                
                txtDifference.Text = ( PIssued - (TotAGrade + TotBGrade + PPanels)).ToString();
                
            }
        }

        private double CellSum(bool AorB)
        {
            double sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                int d = 0;
                
                if (dataGridView2.Rows[i].Cells[3].Value == null)
                    continue;
                if (dataGridView2.Rows[i].Cells[4].Value == null) 
                    continue;

                var Grade = dataGridView2.Rows[i].Cells[1].Value.ToString();
                if (AorB)
                {
                    if (Grade == "A")
                        Int32.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out d);
                }
                else
                {
                    if (Grade == "B")
                        Int32.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out d);
                }
                    
                sum += d;
            }
            return sum;
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            lCancel = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
                       
            if (oBtn != null && formloaded)
            {
                int TotBGradeFaults = dataGridView3.Rows.Cast<DataGridViewRow>()
                                     .Sum(x => Convert.ToInt32(x.Cells[2].Value.ToString()));

                if(TotBGradeFaults != Convert.ToInt32(txtTotBGrade.Text))
                {
                    MessageBox.Show("Please enter the total number of B Grade faults");
                    return;
                }
                
                var BoxType = (TLADM_BoxTypes)cmboBoxType.SelectedItem;
                if (BoxType == null)
                {
                    MessageBox.Show("Please select a box type from the facilities");
                    return;
                }
                var PanelIssue_FK = (int)cmboCMTIssue.SelectedValue;
                if (PanelIssue_FK == 0)
                {
                    MessageBox.Show("Please select a panel issue document");
                    return;
                }

                int LineIssue = (int)cmboCMTLine.SelectedValue;
                if (LineIssue == 0)
                {
                    MessageBox.Show("Please select a production line");
                    return;
                }

               

                using (var context = new TTI2Entities())
                {
                    var LI = context.TLCMT_LineIssue.Find(LineIssue);
                    if(LI != null)
                         LI.TLCMTLI_WorkCompletedDate = dtpTransDate.Value;

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        TLCMT_CompletedWork comWork = new TLCMT_CompletedWork();

                        if (row.ErrorText.Length != 0)
                        {
                            continue;
                        }

                        if (row.Cells[0].Value == null)
                            continue;
                        
                        if((int)row.Cells[4].Value == 0)
                            continue;

                        comWork.TLCMTWC_LineIssue_FK = LineIssue;
                        if (LI != null)
                        {
                            comWork.TLCMTWC_CMTFacility_FK = LI.TLCMTLI_CmtFacility_FK;
                            LI.TLCMTLI_WorkCompleted = true;
                        }
                        
                        comWork.TLCMTWC_PanelIssue_FK = PanelIssue_FK;
                        comWork.TLCMTWC_TransactionDate = dtpTransDate.Value;
                        comWork.TLCMTWC_BoxNumber = row.Cells[0].Value.ToString();
                        comWork.TLCMTWC_PastelNumber = row.Cells[3].Value.ToString();

                        var Pk = (int)row.Cells[6].Value;
                        if (Pk != 0)
                        {
                            var CSRD = context.TLCUT_CutSheetReceiptDetail.Find(Pk);
                            if (CSRD != null)
                            {
                                if (CSRD.TLCUTSHRD_BoxType_FK != null)
                                    comWork.TLCMTWC_BoxType_FK = (int)CSRD.TLCUTSHRD_BoxType_FK;
                            }
                        }
                        else
                        {
                            if(CS != null)
                            {
                                var CSRD = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CS.TLCutSH_Pk).FirstOrDefault();
                                if (CSRD != null)
                                {
                                    if (CSRD.TLCUTSHRD_BoxType_FK != null)
                                        comWork.TLCMTWC_BoxType_FK = (int)CSRD.TLCUTSHRD_BoxType_FK;
                                }
                             }
                        }

                        comWork.TLCMTWC_Grade = row.Cells[1].Value.ToString();
                        comWork.TLCMTWC_Qty = (int)row.Cells[4].Value;

                        if (row.Cells[2].Value == null)
                        {
                            MessageBox.Show("Please select a size from the combo box in the grid");
                            return;
                        }
                        
                        comWork.TLCMTWC_Size_FK = (int)row.Cells[2].Value;
                       
                       
                        if (row.Cells[5].Value != null)
                            comWork.TLCMTWC_Weight = (decimal)row.Cells[5].Value;
                        else
                            comWork.TLCMTWC_Weight = 0;
                        
                        if (CS != null)
                        {
                            comWork.TLCMTWC_CutSheet_FK = CS.TLCutSH_Pk;
                            comWork.TLCMTWC_Colour_FK = CS.TLCutSH_Colour_FK;
                            comWork.TLCMTWC_Style_FK = CS.TLCutSH_Styles_FK;
                        }

                        //BoxType Stored 
                        comWork.TLCMTWC_BoxType_FK = BoxType.TLADMBT_Pk;

                        context.TLCMT_CompletedWork.Add(comWork);
                    }

                    foreach (DataGridViewRow rw in dataGridView3.Rows)
                    {
                        if ((int)rw.Cells[2].Value == 0)
                            continue;

                        TLCMT_ProductionFaults pf = new TLCMT_ProductionFaults();

                        pf.TLCMTPF_Fault_FK = (int)rw.Cells[0].Value;
                        pf.TLCMTPF_PanelIssue_FK = PanelIssue_FK;
                        if(LI != null)
                            pf.TLCMTPF_LineIssue_FK = LI.TLCMTLI_LineNo_FK;
                        pf.TLCMTPF_Qty = (int)rw.Cells[2].Value;

                        context.TLCMT_ProductionFaults.Add(pf);
                    }

                    //===============================================================
                    // Need to save statistics 
                    //==================================================================
                    TLCMT_Statistics stats = new TLCMT_Statistics();
                    bool Add = true;

                    stats = context.TLCMT_Statistics.Where(x => x.CMTS_PanelIssue_FK == PanelIssue_FK).FirstOrDefault();
                    if (stats == null)
                    {
                        stats = new TLCMT_Statistics();
                    }
                    else
                        Add = !Add;

                    stats.CMTS_PanelIssue_FK = PanelIssue_FK;
                    stats.CMTS_Total_A_Grades = Convert.ToInt32(txtTotAGrade.Text);
                    stats.CMTS_Total_B_Grades = Convert.ToInt32(txtTotBGrade.Text);
                    stats.CMTS_TotalPanelIssued = Convert.ToInt32(txtTPIssued.Text);
                    stats.CMTS_Panels = Convert.ToInt32(txtTPPanels.Text);

                    stats.CMTS_Transdate = dtpTransDate.Value;
                    stats.CMTS_Total_Difference = Convert.ToInt32(txtDifference.Text);
                    
                    if(Add)
                        context.TLCMT_Statistics.Add(stats);

                    //=====================================================================

                    try
                    {
                        context.SaveChanges();
                        
                       
                        MessageBox.Show("Records successfully saved to database");
                   

                        formloaded = false;
                        cmboCMTIssue.SelectedValue = -1;
                        cmboCMTLine.SelectedValue = -1;
                        formloaded = true;

                        frmCompleted_Load(this, null);

                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView2.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
        }

        private void dataGridView2_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                if (dataGridView2.CurrentCell.ReadOnly)
                    dataGridView2.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
            else if (oDgv != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (dataGridView2.CurrentCell.ReadOnly)
                        dataGridView2.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                    e.Handled = true;
                }
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0 && e.RowIndex == -1 + oDgv.Rows.Count)
                {
                    DialogResult res = MessageBox.Show("Do you wish to add a new line", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        var PanelIssue = txtCutSheet.Text.Remove(0, 2);
                        var index = oDgv.Rows.Add();
                        oDgv.Rows[index].Cells[0].Value = PanelIssue + "-" + oDgv.Rows.Count.ToString().PadLeft(2, '0');
                        oDgv.Rows[index].Cells[4].Value = 0;
                        oDgv.Rows[index].Cells[5].Value = 0.00M;
                        oDgv.Rows[index].Cells[6].Value = CS.TLCutSH_Pk; 
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Please select a line");
                }

            }
        }

        private void txtTPPanels_TextChanged(object sender, EventArgs e)
        {
            if (formloaded)
            {
                int PPanels = 0;
                var PIssued = Convert.ToInt32(txtTPIssued.Text);
                int TotAGrade = (int)CellSum(true);
                int TotBGrade = (int)CellSum(false);
                
                if (this.txtTPPanels.Text.Contains("-"))
                {
                    var Index = this.txtTPPanels.Text.IndexOf("-");
                    var Value = this.txtTPPanels.Text.Substring(Index + 1, -1 + txtTPPanels.TextLength);
 
                    if(Value.Length > 0)
                      PPanels = Convert.ToInt32(Value) * -1;
                }
                else
                    if(this.txtTPPanels.TextLength > 0)
                      PPanels = Convert.ToInt32(this.txtTPPanels.Text);

                txtTotAGrade.Text = CellSum(true).ToString();
                txtTotBGrade.Text = CellSum(false).ToString();
                txtDifference.Text = (PIssued - (TotAGrade + TotBGrade + PPanels)).ToString();
            }
        }

        

       
    }
}
