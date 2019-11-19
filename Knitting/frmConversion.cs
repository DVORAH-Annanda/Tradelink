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


namespace Knitting
{
    public partial class frmConversion : Form
    {
        bool FormLoaded;

        Util core;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;

        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn(); // Stores
        public frmConversion()
        {
            InitializeComponent();
        }

        private void frmConversion_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            core = new Util();

            oCmboA.HeaderText = "Select a Store";  // 0
            oCmboA.Width = 150;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.HeaderText = "Piece Number"; // 1
            oTxtBoxA.Width = 150;
            oTxtBoxA.Visible = true;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Grade";  //2
            oTxtBoxB.Width = 150;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Weight";  //3
            oTxtBoxC.ValueType = typeof(decimal);
            oTxtBoxC.Width = 175;

            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
           
            using (var context = new TTI2Entities())
            {
                comboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                comboGreige.ValueMember = "TLGreige_Id";
                comboGreige.DisplayMember = "TLGreige_Description";
                comboGreige.SelectedIndex = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    comboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    comboMachines.ValueMember = "MD_Pk";
                    comboMachines.DisplayMember = "MD_AlternateDesc";
                    comboMachines.SelectedIndex = -1;

                    oCmboA.DataSource = context.TLADM_WhseStore.Where(x=>x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    oCmboA.ValueMember = "WhStore_Id";
                    oCmboA.DisplayMember = "WhStore_Description";
                }
            }

            FormLoaded = true;

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

                    if (Cell.ColumnIndex == 0)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 1)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 2)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        // e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        // e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Random rnd = new Random();

            if (oBtn != null)
            {
          

                var Greige = (TLADM_Griege)comboGreige.SelectedItem;
                if (Greige == null)
                {
                    MessageBox.Show("Please select a Greige quality from the drop down box");
                    return;
                }
                var Machine = (TLADM_MachineDefinitions)comboMachines.SelectedItem;
                if (Machine == null)
                {
                    MessageBox.Show("Please select a machine record from the drop down list");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();

                    var Operators  = context.TLADM_MachineOperators.Where(y => y.MachOp_Department_FK == Dept.Dep_Id).ToList();

                    var Inspectors = context.TLADM_MachineOperators.Where(y => y.MachOp_Department_FK == Dept.Dep_Id && y.MachOp_Inspector).ToList();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        TLKNI_GreigeProduction greigP = new TLKNI_GreigeProduction();
                        greigP.GreigeP_Greige_Fk = Greige.TLGreige_Id;
                        greigP.GreigeP_PieceNo = row.Cells[1].Value.ToString();
                        var PieceNo = greigP.GreigeP_PieceNo;
                        var GP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PieceNo == PieceNo).FirstOrDefault();
                        if (GP != null)
                            continue;

                        greigP.GreigeP_weight = (decimal)row.Cells[3].Value;
                        greigP.GreigeP_PDate = dtpTransactionDate.Value;
                        greigP.GreigeP_weightAvail = greigP.GreigeP_weight;
                        greigP.GreigeP_Captured = true;
                        greigP.GreigeP_Shift_FK = 1;
                        

                        greigP.GreigeP_InspDate = dtpTransactionDate.Value;
                        greigP.GreigeP_Inspected = true;

                        int OperNo = rnd.Next(1, Inspectors.Count);
                        var nthItem = Inspectors.Skip(OperNo - 1).FirstOrDefault();
                        greigP.GreigeP_Inspector_FK = nthItem.MachOp_Pk;

                        OperNo = rnd.Next(1, Operators.Count);
                        nthItem = Operators.Skip(OperNo - 1).FirstOrDefault();
                        greigP.GreigeP_Operator_FK = nthItem.MachOp_Pk;
                        greigP.GreigeP_Grade = row.Cells[2].Value.ToString().ToUpper();
                        greigP.GreigeP_Dye = false;
                        greigP.GreigeP_Store_FK = (int)row.Cells[0].Value;
                        
                        
                        
                        context.TLKNI_GreigeProduction.Add(greigP);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to the database");
                        comboGreige.SelectedIndex = -1;
                        comboMachines.SelectedIndex = -1;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }
    }
}
