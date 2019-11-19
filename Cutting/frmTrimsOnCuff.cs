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
    public partial class frmTrimsOnCuff : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();    // 0 Pk of Record in file
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();    // 1 Pk of Description
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();    // 2 Description
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn(); // 3 pk Sizes
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();    // 4 Qty int 
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();    // 5 Kg's decimal
        bool formloaded;
        Util core;

        int CutSheetSelected = 0;

        public frmTrimsOnCuff()
        {
            InitializeComponent();
            core = new Util();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void frmTrimsOnCuff_Load(object sender, EventArgs e)
        {
            formloaded = false;
            CutSheetSelected = 0;

            using (var context = new TTI2Entities())
            {
                var Query = from CutSheet in context.TLCUT_CutSheet
                            join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                            select new { Pk = CutSheet.TLCutSH_Pk, Description = CutSheet.TLCutSH_No };

                foreach (var row in Query)
                {
                    cmboCutSheet.Items.Add(row);
                }

                oCmboA.DataSource = context.TLADM_Sizes.ToList();
                oCmboA.ValueMember = "SI_Id";
                oCmboA.DisplayMember = "SI_Description";
                oCmboA.HeaderText = "Sizes";

                cmboCutSheet.ValueMember = "Pk";
                cmboCutSheet.DisplayMember = "Description";
                cmboCutSheet.SelectedValue = -1;


                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oTxtB.Visible = false;
                oTxtB.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC.HeaderText = "Trim";
                oTxtC.ValueType = typeof(string);
                oTxtC.ReadOnly = true;
                dataGridView1.Columns.Add(oTxtC);

                dataGridView1.Columns.Add(oCmboA);

                oTxtD.HeaderText = "QTY";
                oTxtD.ValueType = typeof(Int32);
                dataGridView1.Columns.Add(oTxtD);

                oTxtE.HeaderText = "Kg's";
                oTxtE.ValueType = typeof(Decimal);
                dataGridView1.Columns.Add(oTxtE);

                var tlcut = context.TLADM_CutTrims.ToList();
                foreach (var Record in tlcut)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = 0;
                    dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTTOC_Pk;
                    dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTTOC_Description;
                    dataGridView1.Rows[index].Cells[4].Value = 0;
                    dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                }
            }

            formloaded = true;
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

                if (CutSheetSelected != 0)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLCUT_TrimsOnCut.Where(x => x.TLCUTTOC_CutSheet_FK == CutSheetSelected).ToList();

                        var tlcut = context.TLADM_CutTrims.ToList();
                        foreach (var Record in tlcut)
                        {
                            var exist = Existing.Find(x=>x.TLCUTTOC_Description_FK == Record.TLCUTTOC_Pk);

                            var index = dataGridView1.Rows.Add();
                            if (exist != null)
                            {
                                dataGridView1.Rows[index].Cells[0].Value = exist.TLCUTTOC_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTTOC_Pk;
                                dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTTOC_Description;
                                dataGridView1.Rows[index].Cells[3].Value = exist.TLCUTTOC_Size_FK;
                                dataGridView1.Rows[index].Cells[4].Value = exist.TLCUTTOC_Qty;
                                dataGridView1.Rows[index].Cells[5].Value = exist.TLCUTTOC_Kgs;
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[0].Value = 0;
                                dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTTOC_Pk;
                                dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTTOC_Description;
                                dataGridView1.Rows[index].Cells[4].Value = 0;
                                dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
            else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 5)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                if (CutSheetSelected == 0)
                {
                    MessageBox.Show("Please select a cut sheet from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[3].Value == null)
                        {
                            continue;
                        }

                        bool Add = true;
                        TLCUT_TrimsOnCut tonc = new TLCUT_TrimsOnCut();

                        if ((int)row.Cells[0].Value != 0)
                        {
                            var index = (int)row.Cells[0].Value;
                            tonc = context.TLCUT_TrimsOnCut.Find(index);
                            if (tonc == null)
                                tonc = new TLCUT_TrimsOnCut();
                            else
                                Add = false;
                        }

                        tonc.TLCUTTOC_CutSheet_FK = CutSheetSelected;
                        tonc.TLCUTTOC_Date = dtpTransDate.Value;
                        tonc.TLCUTTOC_Description_FK = (int)row.Cells[1].Value;
                        tonc.TLCUTTOC_Size_FK = (int)row.Cells[3].Value;
                        tonc.TLCUTTOC_Qty = (int)row.Cells[4].Value;
                        tonc.TLCUTTOC_Kgs = (decimal)row.Cells[5].Value;

                        if (Add)
                            context.TLCUT_TrimsOnCut.Add(tonc);

                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        dataGridView1.Rows.Clear();
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
}
