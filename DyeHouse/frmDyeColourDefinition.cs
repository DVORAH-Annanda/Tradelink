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

namespace DyeHouse
{
    public partial class frmDyeColourDefinition : Form
    {
        int Receipe_Pk;

        DataGridViewTextBoxColumn oTxtBoxA; // 0 Pk
        DataGridViewComboBoxColumn oCmboA;  // 1 Colour
        DataGridViewComboBoxColumn oCmboB;  // 2 Consumables
        DataGridViewTextBoxColumn oTxtBoxB;    // 3 Amount 

        Util core;

        bool FormLoaded;

        public frmDyeColourDefinition(int Pk)
        {
            InitializeComponent();
            core = new Util();

            Receipe_Pk = Pk;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ValueType = typeof(int); 
            oTxtBoxA.Visible = false;

            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Colour";
            oCmboA.Width = 125;
       
            oCmboB = new DataGridViewComboBoxColumn();
            oCmboB.HeaderText = "Dye Consumable";
            oCmboB.Width = 150;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ValueType = typeof(decimal);
            oTxtBoxB.Visible = true;
            oTxtBoxB.HeaderText = "Volume";

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns.Add(oCmboB);
            dataGridView1.Columns.Add(oTxtBoxB);
        }

        private void frmDyeColourDefinition_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                oCmboA.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                oCmboA.ValueMember = "Col_Id";
                oCmboA.DisplayMember = "Col_Display";

                oCmboB.DataSource = context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Description).ToList();
                oCmboB.ValueMember = "ConsDC_Pk";
                oCmboB.DisplayMember = "ConsDC_Description";

                txtReceipeDefinition.Text = context.TLDYE_RecipeDefinition.Find(Receipe_Pk).TLDYE_DefineDescription;

                var Existing = context.TLDYE_RecipeColourDefinition.Where(x => x.TLDYECD_Receipe_FK == Receipe_Pk).ToList();
                foreach (var Row in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Row.TLDYECD_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = Row.TLDYECD_Colour_FK;
                    dataGridView1.Rows[index].Cells[2].Value = Row.TLDYECD_Consumable_FK;
                    dataGridView1.Rows[index].Cells[3].Value = Row.TLDYECD_MELFC;

                }

            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[1].Value == null)
                            continue;

                        if (Row.Cells[2].Value == null ||
                            Row.Cells[3].Value == null)
                        {
                            MessageBox.Show("Line No " + Row.Index.ToString() + " incomplete" + Environment.NewLine + "Process Aborted");
                            return;
                        }

                        bool Add = true;
                        TLDYE_RecipeColourDefinition ColorDef = new TLDYE_RecipeColourDefinition();
                        if (Row.Cells[0].Value != null)
                        {
                            int index = (int)Row.Cells[0].Value;

                            ColorDef = context.TLDYE_RecipeColourDefinition.Find(index);
                            if (ColorDef != null)
                                Add = false;
                            else
                                ColorDef = new TLDYE_RecipeColourDefinition();
                        }

                        
                        ColorDef.TLDYECD_Colour_FK = (int)Row.Cells[1].Value;
                        ColorDef.TLDYECD_Consumable_FK = (int)Row.Cells[2].Value;
                        ColorDef.TLDYECD_MELFC = (decimal)Row.Cells[3].Value;
                        ColorDef.TLDYECD_Receipe_FK = Receipe_Pk;

                        if (Add)
                            context.TLDYE_RecipeColourDefinition.Add(ColorDef);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
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

                    if (Cell.ColumnIndex == 3)
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
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
                            var locRec = context.TLDYE_RecipeColourDefinition.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLDYE_RecipeColourDefinition.Remove(locRec);
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
    }
}
