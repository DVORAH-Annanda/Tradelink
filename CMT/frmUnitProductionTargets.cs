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
    public partial class frmUnitProductionTargets : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA; // 0 Pk
        DataGridViewComboBoxColumn oCmboA;  // 1 Colour
        DataGridViewComboBoxColumn oCmboB;  // 2 Consumables
        DataGridViewTextBoxColumn oTxtBoxB;    // 3 Amount 
        Util core;

        bool FormLoaded; 

        public frmUnitProductionTargets()
        {
            InitializeComponent();
            core = new Util();

            oTxtBoxA = new DataGridViewTextBoxColumn();  // 0
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.Visible = false;

            oCmboA = new DataGridViewComboBoxColumn();   // 1
            oCmboA.HeaderText = "CMT Line";
            oCmboA.Width = 125;


            oCmboB = new DataGridViewComboBoxColumn();   // 2
            oCmboB.HeaderText = "Style";
            oCmboB.Width = 150;

            oTxtBoxB = new DataGridViewTextBoxColumn();  // 3
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.Visible = true;
            oTxtBoxB.HeaderText = "Units Per Hour";

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns.Add(oCmboB);
            dataGridView1.Columns.Add(oTxtBoxB);

            dataGridView1.AllowUserToOrderColumns = false;
            
        }

        private void frmUnitProductionTargets_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCMT.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboCMT.ValueMember = "Dep_Id";
                cmboCMT.DisplayMember = "Dep_Description";
                cmboCMT.SelectedValue = -1;

                oCmboB.DataSource = context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).ToList();
                oCmboB.ValueMember = "Sty_Id";
                oCmboB.DisplayMember = "Sty_Description";


            }
            FormLoaded = true;
        }

        private void cmboCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var Selected = (TLADM_Departments)oCmbo.SelectedItem;
                if(Selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                       dataGridView1.Rows.Clear();

                        var Entries = context.TLCMT_UnitProductionTargets.Where(x => x.TLUPT_CMT_Pk == Selected.Dep_Id && x.TLUPT_TransDate == dtpTransactionDate.Value.Date).ToList();
                        foreach (var Entry in Entries)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Entry.TLUPT_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Entry.TLUPT_LineNo_Fk;
                            dataGridView1.Rows[index].Cells[2].Value = Entry.TLUPT_Style_Fk;
                            dataGridView1.Rows[index].Cells[3].Value = Entry.TLUPT_Unit_Target;
                        }

                        oCmboA.DataSource = null;
                        oCmboA.Items.Clear();
                        oCmboA.DataSource = context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == Selected.Dep_Id).ToList();
                        oCmboA.ValueMember = "TLCMTCFG_Pk";
                        oCmboA.DisplayMember = "TLCMTCFG_Description";

                    }
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;

            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && FormLoaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        var index = this.dataGridView1.SelectedRows[0].Index;

                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = (int)cr.Cells[0].Value;
                            var locRec =  context.TLCMT_UnitProductionTargets.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLCMT_UnitProductionTargets.Remove(locRec);
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 3)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var Selected = (TLADM_Departments)cmboCMT.SelectedItem;
                    if (Selected == null)
                    {
                        MessageBox.Show("Please select a CMT from the drop down box");
                        return;
                    }

                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[0].Value != null)
                        {
                            var Pk = (int)Row.Cells[0].Value;
                            var ProdTargets = context.TLCMT_UnitProductionTargets.Find(Pk);
                            if (ProdTargets != null)
                            {
                                ProdTargets.TLUPT_LineNo_Fk = (int)Row.Cells[1].Value;
                                ProdTargets.TLUPT_Style_Fk = (int)Row.Cells[2].Value;
                                ProdTargets.TLUPT_Unit_Target = (int)Row.Cells[3].Value;
                            }
                        }
                        else
                        {
                            if (Row.Cells[1].Value == null || Row.Cells[2].Value == null || Row.Cells[3].Value == null)
                                continue;

                            TLCMT_UnitProductionTargets ProdTargets = new TLCMT_UnitProductionTargets();
                            ProdTargets.TLUPT_CMT_Pk = Selected.Dep_Id;
                            ProdTargets.TLUPT_TransDate = dtpTransactionDate.Value;
                            ProdTargets.TLUPT_LineNo_Fk = (int)Row.Cells[1].Value;
                            ProdTargets.TLUPT_Style_Fk = (int)Row.Cells[2].Value;
                            ProdTargets.TLUPT_Unit_Target = (int)Row.Cells[3].Value;

                            context.TLCMT_UnitProductionTargets.Add(ProdTargets);
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
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
