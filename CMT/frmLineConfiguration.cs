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
    public partial class frmLineConfiguration : Form
    {
        bool formloaded;

        string[][] MandatoryFields;
        bool[] MandSelected;
        int ActiveRow;

        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();    // 0 File PK
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();    // 1 Department FK
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();    // 2 Line Number
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();    // 3 Line Description
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn(); // 4 Style // Std Product
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();    // 5 No Of Operators
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();    // 6 Std Output
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn(); // 7 UOM
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();    // 8 Supervisor  
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();    // 9 Display Order  
        public frmLineConfiguration()
        {
            InitializeComponent();
            core = new Util();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmLineConfiguration_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboDepartments.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboDepartments.ValueMember = "DEP_Id";
                cmboDepartments.DisplayMember = "DEP_Description";
                cmboDepartments.SelectedValue = -1;
               
                // Note for the file need to change the source to that of styles 
                /*
                oCmboA.DataSource = context.TLADM_StandardProduct.OrderBy(x => x.TLADMSP_ShortCode).ToList();
                oCmboA.ValueMember = "TLADMSP_Pk";
                oCmboA.DisplayMember = "TLADMSP_Description"; */

                oCmboA.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                oCmboA.ValueMember = "Sty_Id";
                oCmboA.DisplayMember = "Sty_Description"; 
                
                oCmboB.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                oCmboB.ValueMember = "UOM_Pk";
                oCmboB.DisplayMember = "UOM_Description";

            }

            oTxtA.HeaderText = "PK";
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Department PK";
            oTxtB.Visible = false;
            oTxtB.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Line Number";
            oTxtC.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Line Description";
            oTxtD.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtD);

            oCmboA.HeaderText = "Std Product";
            dataGridView1.Columns.Add(oCmboA);

            oTxtE.HeaderText = "No Of Operators";
            oTxtE.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtE);

            oTxtF.HeaderText = "Standard Rate";
            oTxtF.ValueType = typeof(decimal);
            dataGridView1.Columns.Add(oTxtF);

            oCmboB.HeaderText = "UOM";
            dataGridView1.Columns.Add(oCmboB);

            oTxtG.HeaderText = "Supervisors";
            oTxtG.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtG);

            oTxtH.HeaderText = "Display Order";
            oTxtH.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtH);

            MandatoryFields = new string[][]
                {   new string[] {"2", "Please enter a line number", "0"},
                    new string[] {"3", "Please enter a line description", "1"},
                    new string[] {"4", "Please select a standard product", "2"},
                    new string[] {"5", "Please enter the number of operators", "3"},
                    new string[] {"6", "Please enter the std Rate", "4"},
                    new string[] {"7", "Please select a unit of measure", "5"},
                    new string[] {"8", "Please enter the supervisors name", "6"},
                    new string[] {"9", "Please enter the Display Order", "7"}
                };
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();

                var selected = (TLADM_Departments)cmboDepartments.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == selected.Dep_Id).ToList();
                        foreach (var Record in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Record.TLCMTCFG_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Record.TLCMTCFG_Department_FK;
                            dataGridView1.Rows[index].Cells[2].Value = Record.TLCMTCFG_LineNo;
                            dataGridView1.Rows[index].Cells[3].Value = Record.TLCMTCFG_Description;
                            dataGridView1.Rows[index].Cells[4].Value = Record.TLCMTCFG_Quality_FK;
                            dataGridView1.Rows[index].Cells[5].Value = Record.TLCMTCFG_NoOfOperators;
                            dataGridView1.Rows[index].Cells[6].Value = Record.TLCMTCFG_StdOutput;
                            dataGridView1.Rows[index].Cells[7].Value = Record.TLCMTCFG_UOM_FK;
                            dataGridView1.Rows[index].Cells[8].Value = Record.TLCMTCFG_Operator;
                            dataGridView1.Rows[index].Cells[9].Value = Record.TLCMTCFG_DisplayOrder;
                        }
                        MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using (var context = ( new TTI2Entities()))
                {
                    var selected = (TLADM_Departments)cmboDepartments.SelectedItem;
                    if (selected != null)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[2].Value == null)
                            {
                                continue;
                            }

                            bool Add = true;

                            TLCMT_FactConfig factcon = new TLCMT_FactConfig();
                            if (row.Cells[0].Value != null)
                            {
                                var index = (int)row.Cells[0].Value;
                                factcon = context.TLCMT_FactConfig.Find(index);
                                if (factcon == null)
                                {
                                    factcon = new TLCMT_FactConfig();
                                }
                                else
                                {
                                    Add = false;
                                }
                            }

                            factcon.TLCMTCFG_Department_FK = selected.Dep_Id;
                            factcon.TLCMTCFG_LineNo = (int)row.Cells[2].Value;
                            factcon.TLCMTCFG_Description = (string)row.Cells[3].Value;
                            factcon.TLCMTCFG_Quality_FK = (int)row.Cells[4].Value;
                            factcon.TLCMTCFG_NoOfOperators = (int)row.Cells[5].Value;
                            factcon.TLCMTCFG_StdOutput = (decimal)row.Cells[6].Value;
                            factcon.TLCMTCFG_UOM_FK = (int)row.Cells[7].Value;
                            factcon.TLCMTCFG_Operator = (string)row.Cells[8].Value;
                            factcon.TLCMTCFG_DisplayOrder = Convert.ToInt32(row.Cells[9].Value);
                            if(Add)
                               context.TLCMT_FactConfig.Add(factcon);
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                        formloaded = false;
                        cmboDepartments.SelectedValue = -1;
                        dataGridView1.Rows.Clear();
                        formloaded = true;
                    }
                    else
                    {
                        MessageBox.Show("Please select a department from the drop down box provided");
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 2 || Cell.ColumnIndex == 5 )
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 6)
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;

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

                    MandSelected = complete; // core.PopulateArray(MandatoryFields.Length, complete);
                }
               
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && ActiveRow == oDgv.CurrentCell.RowIndex)
            {
                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                var currentCell = oDgv.CurrentCell;
                var result = (from u in MandatoryFields
                                  where u[0] == currentCell.ColumnIndex.ToString()
                                  select u).FirstOrDefault();

                if (result != null && currentCell.EditedFormattedValue != null && !String.IsNullOrEmpty(currentCell.EditedFormattedValue.ToString()))
                {
                     int nbr = Convert.ToInt32(result[2].ToString());
                     MandSelected[nbr] = true;
                }
                else
                {
                     e.Cancel = true;
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
                        if(cr.Cells[0].Value != null)
                        {
                            using (var context = new TTI2Entities())
                            {
                                int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());
                                int IssuedLines = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_LineNo_FK == RecNo).Count();
                                if (IssuedLines != 0)
                                {
                                    MessageBox.Show("This Line is still operational", "Transaction Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var locRec = context.TLCMT_FactConfig.Find(RecNo);
                                if (locRec != null)
                                {
                                    try
                                    {
                                        context.TLCMT_FactConfig.Remove(locRec);
                                        context.SaveChanges();
                                        MessageBox.Show("Record successfully deleted");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
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
    }
}
