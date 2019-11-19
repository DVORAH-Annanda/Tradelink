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

namespace ProductionPlanning
{
    public partial class frmReplenishment : Form
    {
        bool formloaded;

        Util core;

        DataGridViewTextBoxColumn  oTxtA;   // 0 File Primary index 
        DataGridViewCheckBoxColumn oChkA;   // 1 Check Button Discontinued Yes / No
        DataGridViewComboBoxColumn oCmbA;   // 2 Styles
        DataGridViewComboBoxColumn oCmbB;   // 3 Colours 
        DataGridViewComboBoxColumn oCmbC;   // 4 Sizes  
        DataGridViewTextBoxColumn  oTxtB;   // 5 Expected Sales 
        DataGridViewTextBoxColumn  oTxtC;   // 6 ReOrder Level 
        DataGridViewTextBoxColumn  oTxtD;   // 7 ReOrder Quantity
        DataGridViewTextBoxColumn  oTxtE;   // 8 ReOrder Level weeks
        DataGridViewTextBoxColumn  oTxtF;   // 9 ReOrder Quantity weeks

        IList<TLPPS_Replenishment> Replenishment;

        System.Data.DataTable dt = null;

        string[][] MandatoryFields;
        bool[] MandSelected;

        public frmReplenishment()
        {
            InitializeComponent();
        }

        private void frmReplenishment_Load(object sender, EventArgs e)
        {
            formloaded = false;

            core = new Util();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Primary Key of Record otherwise null
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            dataGridView1.Columns.Add(oTxtA);


            oChkA = new DataGridViewCheckBoxColumn();   // 1 Discontinued Y / N
            oChkA.Visible = true;
            oChkA.HeaderText = "Discontinued";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oCmbA = new DataGridViewComboBoxColumn();   // 2 Styles
            oCmbA.HeaderText = "Styles";
            oCmbA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oCmbA);

            oCmbB = new DataGridViewComboBoxColumn();   // 3 Colours 
            oCmbB.HeaderText = "Colours";
            oCmbB.ValueType = typeof(int);
            dataGridView1.Columns.Add(oCmbB);

            oCmbC = new DataGridViewComboBoxColumn();   // 4 Sizes 
            oCmbC.HeaderText = "Sizes";
            oCmbC.ValueType = typeof(int);
            dataGridView1.Columns.Add(oCmbC);


            oTxtB = new DataGridViewTextBoxColumn();    // 5 Expected Sales
            oTxtB.ValueType = typeof(int);
            oTxtB.HeaderText = "Expected Sales";
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();    // 6 Reorder Level ( weeks)
            oTxtC.ValueType = typeof(int);
            oTxtC.HeaderText = "ReOrder Level (weeks)";
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();    // 7 Reorder Qty ( weeks)
            oTxtD.ValueType = typeof(int);
            oTxtD.HeaderText = "ReOrder Quantity (weeks)";
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();    // 8 Reorder Level
            oTxtE.ValueType = typeof(int);
            oTxtE.HeaderText = "ReOrder Level";
            oTxtE.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF = new DataGridViewTextBoxColumn();    // 9 Reorder Qty 
            oTxtF.ValueType = typeof(int);
            oTxtF.HeaderText = "ReOrder Quantity";
            oTxtF.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtF);
            

            dataGridView1.AutoGenerateColumns = false;
 

            using (var context = new TTI2Entities())
            {

                oCmbA.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                oCmbA.ValueMember = "Sty_Id";
                oCmbA.DisplayMember = "Sty_Description";

                oCmbB.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                oCmbB.ValueMember = "Col_Id";
                oCmbB.DisplayMember = "Col_Display";
                
                oCmbC.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                oCmbC.ValueMember = "SI_Id";
                oCmbC.DisplayMember = "SI_Description";

                try
                {
                    Replenishment = (from T1 in context.TLPPS_Replenishment
                                       join T2 in context.TLADM_Styles on T1.TLREP_Style_FK equals T2.Sty_Id
                                       join T3 in context.TLADM_Colours on T1.TLREP_Colour_FK equals T3.Col_Id
                                       join T4 in context.TLADM_Sizes on T1.TLREP_Size_FK equals T4.SI_id
                                       orderby T2.Sty_Description, T3.Col_Display, T4.SI_DisplayOrder
                                       select T1).ToList();
                    
                    foreach (var Record in Replenishment)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Record.TLREP_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = (bool)Record.TLREP_Discontinued;
                        dataGridView1.Rows[index].Cells[2].Value = Record.TLREP_Style_FK;
                        dataGridView1.Rows[index].Cells[3].Value = Record.TLREP_Colour_FK;
                        dataGridView1.Rows[index].Cells[4].Value = Record.TLREP_Size_FK;
                        dataGridView1.Rows[index].Cells[5].Value = Record.TLREP_ExpectedSales;
                        dataGridView1.Rows[index].Cells[6].Value = Record.TLREP_ReOrderLevelWeeks; ;
                        dataGridView1.Rows[index].Cells[7].Value = Record.TLREP_ReorderQtyWeeks;
                        dataGridView1.Rows[index].Cells[8].Value = Record.TLREP_ReOrderLevel ;
                        dataGridView1.Rows[index].Cells[9].Value = Record.TLREP_ReOrderQty;
                    }
                }
                catch (Exception ex)
                {
                    var exceptionMessages = new StringBuilder();
                    do
                    {
                        exceptionMessages.Append(ex.Message);
                        ex = ex.InnerException;
                    }
                    while (ex != null);

                    MessageBox.Show(exceptionMessages.ToString());
                }
            }

            MandatoryFields = new string[][]
                {   new string[] {"2", "Please select a style", "0"},
                    new string[] {"3", "Please select a colour", "1"},
                    new string[] {"4", "Please select a size", "2"}, 
                    new string[] {"5", "Please enter an expected sales value", "3"}, 
                    new string[] {"6", "Please enter a reorder level (weeks) value", "4"},
                    new string[] {"7", "Please enter a reorder (weeks) quantity", "5"} 
                };
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;
        }

        
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var Cell = oDgv.CurrentCell;
                if (Cell.ColumnIndex == 5 || Cell.ColumnIndex == 6 || Cell.ColumnIndex == 7)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
               
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        bool Add = true;
                        
                        var CurrentRow = row;
                        if (CurrentRow != null)
                        {
                            MandSelected = core.RowComplete(CurrentRow, MandatoryFields);
                            var ErrC = MandSelected.Where(x => x == false).Count();

                            if (ErrC == MandatoryFields.Count())
                                continue;

                            if (ErrC != MandatoryFields.Count())
                            {
                                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                                if (!string.IsNullOrEmpty(errorM))
                                {
                                    MessageBox.Show(errorM, "Error Message");
                                    return;
                                }
                            }
                        }

                        TLPPS_Replenishment replen = new TLPPS_Replenishment();

                        if (row.Cells[0].Value != null)
                        {
                            Add = false;

                            var Pk = (int)row.Cells[0].Value;
                            if (!(bool)row.Cells[1].Value)
                            {
                                replen = Replenishment.FirstOrDefault(x => x.TLREP_Pk == Pk);
                                if (replen != null)
                                {
                                    if (core.PPSCompareValues(replen, CurrentRow))
                                        continue;
                                }
                            }

                            replen = context.TLPPS_Replenishment.Find(Pk);
                        }

                        try
                        {
                           
                           /*  Original code before changed
                             **************************************************** 
                            replen.TLREP_Style_FK = (int)row.Cells[1].Value;
                            replen.TLREP_Colour_FK = (int)row.Cells[2].Value;
                            replen.TLREP_Size_FK = (int)row.Cells[3].Value;
                            replen.TLREP_ExpectedSales = (int)row.Cells[4].Value;
                            replen.TLREP_ReOrderLevelWeeks = (int)row.Cells[5].Value;
                            replen.TLREP_ReorderQtyWeeks = (int)row.Cells[6].Value;
                            replen.TLREP_ReOrderLevel = (int)row.Cells[4].Value * (int)row.Cells[5].Value;
                            replen.TLREP_ReOrderQty = (int)row.Cells[4].Value * (int)row.Cells[6].Value;
                            ****************************************************************/
                            replen.TLREP_Discontinued = (bool)row.Cells[1].Value;
                            replen.TLREP_Style_FK = (int)row.Cells[2].Value;
                            replen.TLREP_Colour_FK = (int)row.Cells[3].Value;
                            replen.TLREP_Size_FK = (int)row.Cells[4].Value;
                            replen.TLREP_ExpectedSales = (int)row.Cells[5].Value;
                            replen.TLREP_ReOrderLevelWeeks = (int)row.Cells[6].Value;
                            replen.TLREP_ReorderQtyWeeks = (int)row.Cells[7].Value;
                            replen.TLREP_ReOrderLevel = (int)row.Cells[5].Value * (int)row.Cells[6].Value;
                            replen.TLREP_ReOrderQty = (int)row.Cells[5].Value * (int)row.Cells[7].Value;

                            if (Add)
                                context.TLPPS_Replenishment.Add(replen);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
                   

            if (oDgv != null && formloaded)
            {
                if (oDgv.CurrentCell != null)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        MandSelected = core.RowComplete(CurrentRow, MandatoryFields);
                        var ErrC = MandSelected.Where(x => x == false).Count();
                        if (ErrC != MandatoryFields.Count())
                        {
                            var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                            if (!string.IsNullOrEmpty(errorM))
                            {
                                MessageBox.Show(errorM, "Error Message");
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction" + Environment.NewLine + " This action could possible cause a system abend", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());
                            var locRec = context.TLPPS_Replenishment.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLPPS_Replenishment.Remove(locRec);
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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            var CurrentRow = oDgv.CurrentRow;
            if(CurrentRow != null)
               oDgv.Rows[CurrentRow.Index].Cells[1].Value = false;
        }
    }
}
