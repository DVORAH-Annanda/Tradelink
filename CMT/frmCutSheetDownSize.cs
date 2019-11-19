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
    public partial class frmCutSheetDownSize : Form
    {
        bool FormLoaded;

        IList<TLCMT_LineIssue> LI = new List<TLCMT_LineIssue>();
        IList<TLCUT_CutSheet > CS = new List<TLCUT_CutSheet>();

        DataGridViewTextBoxColumn  oTxtA;   // 0 Record Primary Key
        DataGridViewTextBoxColumn  oTxtB;   // 1 Box Number 
        DataGridViewCheckBoxColumn oChkA;   // 2 Select Yes or No 
        DataGridViewComboBoxColumn oCmboA;  // 3 Cmbo Boxes containing current sizes  
        DataGridViewTextBoxColumn  oTxtC;   // 4 Current Box Qty  
        Util core; 

        public frmCutSheetDownSize()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();    // 0
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();   // 1
            oChkA.HeaderText = "Selected";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();    // 2
            oTxtB.ReadOnly = true;
            oTxtB.HeaderText = "Box Number";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oCmboA = new DataGridViewComboBoxColumn();   // 3
            oCmboA.HeaderText = "Sizes";
            oCmboA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oCmboA);

            oTxtC = new DataGridViewTextBoxColumn();    // 4
            oTxtC.HeaderText = "Boxed Qty";
            oTxtC.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtC);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;

            core = new Util();

        }

        private void frmCutSheetDownSize_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                LI = (from LIssue in context.TLCMT_LineIssue
                      join CWork in context.TLCMT_CompletedWork on LIssue.TLCMTLI_Pk equals CWork.TLCMTWC_LineIssue_FK
                      where LIssue.TLCMTLI_WorkCompleted && !CWork.TLCMTWC_Picked
                      select LIssue).Distinct().ToList();

                cmboCutSheets.DataSource = LI;
                cmboCutSheets.ValueMember = "TLCMTLI_PK";
                cmboCutSheets.DisplayMember = "TLCMTLI_CutSheetDetails";
                cmboCutSheets.SelectedValue = -1;

                oCmboA.DataSource = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).ToList();
                oCmboA.ValueMember = "SI_Id";
                oCmboA.DisplayMember = "SI_Description";

                radWorkComplete.Checked = true;
 
                
            }

            FormLoaded = true;

        }

        private void cmboCutSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;

            if (oCmbo != null && FormLoaded)
            {
                dataGridView1.Rows.Clear();
                               
                using (var context = new TTI2Entities())
                {
                    if (radWorkComplete.Checked)
                    {
                        var SelectedItem = (TLCMT_LineIssue)oCmbo.SelectedItem;
                        var Boxes = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_LineIssue_FK == SelectedItem.TLCMTLI_Pk).ToList();
                        foreach (var Box in Boxes)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Box.TLCMTWC_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Box.TLCMTWC_BoxNumber;
                            dataGridView1.Rows[index].Cells[3].Value = Box.TLCMTWC_Size_FK;
                            dataGridView1.Rows[index].Cells[4].Value = Box.TLCMTWC_Qty;
                        }
                    }
                    else
                    {
                        var SelectedItem = (TLCUT_CutSheet)oCmbo.SelectedItem;
                        var Boxes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == SelectedItem.TLCutSH_Pk).ToList();
                        foreach (var Box in Boxes)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Box.TLCUTE_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = "Dummy";
                            dataGridView1.Rows[index].Cells[3].Value = Box.TLCUTE_Size_FK;
                            dataGridView1.Rows[index].Cells[4].Value = 0;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach(DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (radWorkComplete.Checked)
                        {
                            if (!(bool)Row.Cells[1].Value)
                                continue;

                            int Pk = (int)Row.Cells[0].Value;
                            var CompletedWork = context.TLCMT_CompletedWork.Find(Pk);
                            if (CompletedWork != null)
                            {
                                var Size = (int)Row.Cells[3].Value;
                                if (Size != CompletedWork.TLCMTWC_Size_FK)
                                    CompletedWork.TLCMTWC_Size_FK = (int)Row.Cells[3].Value;

                                var BoxedQty = (int)Row.Cells[4].Value;
                                if (BoxedQty != CompletedWork.TLCMTWC_Qty)
                                    CompletedWork.TLCMTWC_Qty = (int)Row.Cells[4].Value;
                            }
                        }
                        else
                        {
                            if (!(bool)Row.Cells[1].Value)
                                continue;
                            
                            int Pk = (int)Row.Cells[0].Value;
                            var ExpectedUnits = context.TLCUT_ExpectedUnits.Find(Pk);
                            if (ExpectedUnits != null)
                            {
                                ExpectedUnits.TLCUTE_Size_FK = (int)Row.Cells[3].Value;
                                
                            }
                        }
                    }
                   
                        
                    try
                    {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");

                            frmCutSheetDownSize_Load(this, null);
                            
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
     
            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownTS);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownTS);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    
                }
            }
        }

        private void radWorkComplete_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    dataGridView1.Rows.Clear();

                    DataGridViewColumn dc = dataGridView1.Columns[2];
                    dc.Visible = true;
                    dc = dataGridView1.Columns[4];
                    dc.Visible = true;

                    using (var context = new TTI2Entities())
                    {
                        FormLoaded = false;

                        LI = (from LIssue in context.TLCMT_LineIssue
                              join CWork in context.TLCMT_CompletedWork on LIssue.TLCMTLI_Pk equals CWork.TLCMTWC_LineIssue_FK
                              where LIssue.TLCMTLI_WorkCompleted && !CWork.TLCMTWC_Picked
                              select LIssue).Distinct().ToList();

                        cmboCutSheets.DataSource = null;
                        cmboCutSheets.Items.Clear();
                        cmboCutSheets.DataSource = LI;
                        cmboCutSheets.ValueMember = "TLCMTLI_PK";
                        cmboCutSheets.DisplayMember = "TLCMTLI_CutSheetDetails";
                        cmboCutSheets.SelectedValue = -1;

                        FormLoaded = true;
                    }
                }
            }
        }

        private void radWorkInProgress_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded)
            {
                if (oRad.Checked)
                {
                    dataGridView1.Rows.Clear();

                    DataGridViewColumn dc = dataGridView1.Columns[2];
                    dc.Visible = false;
                    dc = dataGridView1.Columns[4];
                    dc.Visible = false;

                    using (var context = new TTI2Entities())
                    {
                        FormLoaded = false;

                        CS = (from LIssue in context.TLCMT_LineIssue
                              join CutSheet in context.TLCUT_CutSheet on LIssue.TLCMTLI_CutSheet_FK equals CutSheet.TLCutSH_Pk
                              where !LIssue.TLCMTLI_WorkCompleted
                              select CutSheet).Distinct().ToList();

                        cmboCutSheets.DataSource = null;
                        cmboCutSheets.Items.Clear();
                        cmboCutSheets.DataSource = CS;
                        cmboCutSheets.ValueMember = "TLCutSH_Pk";
                        cmboCutSheets.DisplayMember = "TLCutSH_No";
                        cmboCutSheets.SelectedValue = -1;
                        FormLoaded = true;

                    }
                }
            }
        }
    }
}
