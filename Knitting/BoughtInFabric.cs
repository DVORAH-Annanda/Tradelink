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
    public partial class BoughtInFabric : Form
    {
        bool FormLoaded;
        DataGridViewTextBoxColumn oTxtA;   // Our Piece Number
        DataGridViewTextBoxColumn oTxtB;   // Their Piece Number
        DataGridViewTextBoxColumn oTxtC;   // Dsk Weight 
        DataGridViewTextBoxColumn oTxtD;   // Dsk Width
        DataGridViewTextBoxColumn oTxtE;   // Nett Weight

        Util core;
        public BoughtInFabric()
        {
            InitializeComponent();
            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Our Piece No";
            oTxtA.ValueType = typeof(string);
            oTxtA.Visible = true;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Shift Description
            oTxtB.HeaderText = "Their Piece No";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 2 Time Start 
            oTxtC.HeaderText = "Dsk Weight";
            oTxtC.ValueType = typeof(Decimal);
            oTxtC.Visible = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtD.HeaderText = "Dsk Width";
            oTxtD.ValueType = typeof(Decimal);
            oTxtD.Visible = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtE.HeaderText = "Nett Weight";
            oTxtE.ValueType = typeof(Decimal);
            oTxtE.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // Our Piece No 
            dataGridView1.Columns.Add(oTxtB);    // Their Piece No
            dataGridView1.Columns.Add(oTxtC);    // dsk Weight  
            dataGridView1.Columns.Add(oTxtD);    // dsk Width
            dataGridView1.Columns.Add(oTxtE);    // Nett Weight

            core = new Util();

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void BoughtInFabric_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCountry.DataSource = context.TLADM_CottonOrigin.ToList();
                cmboCountry.DisplayMember = "CottonOrigin_Description";
                cmboCountry.ValueMember = "CottonOrigin_Pk";
                cmboCountry.SelectedIndex = -1;

                cmboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboGreige.DisplayMember = "TLGreige_Description";
                cmboGreige.ValueMember = "TLGreige_Id";
                cmboGreige.SelectedIndex = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNI")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboMachine.DisplayMember = "MD_AlternateDesc";
                    cmboMachine.ValueMember = "MD_Pk";
                    cmboMachine.SelectedIndex = -1;



                    cmboStore.DataSource = context.TLADM_WhseStore.Where(x => !x.WhStore_WhseOrStore && x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    cmboStore.DisplayMember = "WhStore_Description";
                    cmboStore.ValueMember = "WhStore_Id";
                    cmboStore.SelectedIndex = -1;
                }
               
            }
            FormLoaded = true;

        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 2 ||
                        Cell.ColumnIndex == 3 || 
                        Cell.ColumnIndex == 4)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            var TransNo = 0;

            if (oBtn != null && FormLoaded)
            {
                var Origin = (TLADM_CottonOrigin)cmboCountry.SelectedItem;
                if (Origin == null)
                {
                    MessageBox.Show("Please select a Country of Origin");
                    return;
                }

                var Greige = (TLADM_Griege)cmboGreige.SelectedItem;
                if (Greige == null)
                {
                    MessageBox.Show("Please select a quality");
                    return;
                }

                var CurrentStore = (TLADM_WhseStore)cmboStore.SelectedItem;
                if (CurrentStore == null)
                {
                    MessageBox.Show("Please select a store");
                    return;
                }

                var Machine = (TLADM_MachineDefinitions)cmboMachine.SelectedItem;
                if (Machine == null)
                {
                    MessageBox.Show("Please select a machine");
                    return;
                }

                TransNo = core.CenturyDayNumber(DateTime.Now) + DateTime.Now.Hour + DateTime.Now.Minute;
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[0].Value == null)
                            continue;

                        TLKNI_BoughtInFabric BoughtIn = new TLKNI_BoughtInFabric();

                        BoughtIn.TLBIN_COfOrigin_FK = Origin.CottonOrigin_Pk;
                        BoughtIn.TLBIN_CurrentStore_FK = CurrentStore.WhStore_Id;
                        BoughtIn.TLBIN_TransDate = TransDate.Value;
                        BoughtIn.TLBIN_Greige_FK = Greige.TLGreige_Id;
                        BoughtIn.TLBIN_Machine_FK = Machine.MD_Pk;

                        BoughtIn.TLBIN_TTS_PN = (string)Row.Cells[0].Value;
                        BoughtIn.TLBIN_Their_PN = (string)Row.Cells[1].Value;
                        BoughtIn.TLBIN_Dsk_Weight = (decimal)Row.Cells[2].Value;
                        BoughtIn.TLBIN_Dsk_Width = (decimal)Row.Cells[3].Value;
                        BoughtIn.TLBIN_Nett_Weight = (decimal)Row.Cells[4].Value;
                        BoughtIn.TLBIN_TransNumber = TransNo;

                        context.TLKNI_BoughtInFabric.Add(BoughtIn);

                        //we need to store further information in a separate file for the processing in the Dye Module
                        //-----------------------------------------------------------------------------------------------------------
                        TLKNI_GreigeProduction gp = new TLKNI_GreigeProduction();
                        gp.GreigeP_Captured = true;
                        gp.GreigeP_BoughtIn = true;
                        gp.GreigeP_KnitO_Fk = TransNo;  // Provides a link to the original transaction 
                        gp.GreigeP_Grade = "A";
                        gp.GreigeP_InspDate = TransDate.Value;
                        gp.GreigeP_Inspected = true;
                        gp.GreigeP_Meas1 = 0;
                        gp.GreigeP_Meas2 = 0;
                        gp.GreigeP_Meas3 = 0;
                        gp.GreigeP_Meas4 = 0;
                        gp.GreigeP_Meas5 = 0;
                        gp.GreigeP_Meas6 = 0;
                        gp.GreigeP_Meas7 = 0;
                        gp.GreigeP_Meas8 = 0;
                        gp.GreigeP_PDate = DateTime.Now;
                        gp.GreigeP_PieceNo = (string)Row.Cells[0].Value;
                        gp.GreigeP_BoughtIn_FabWeight = (decimal)Row.Cells[2].Value;
                        gp.GreigeP_BoughtIn_FabWidth = (decimal)Row.Cells[3].Value;
                        gp.GreigeP_weight = (decimal)Row.Cells[4].Value;
                        gp.GreigeP_weightAvail = (decimal)Row.Cells[4].Value;
                        gp.GreigeP_Store_FK = CurrentStore.WhStore_Id;
                        gp.GreigeP_Greige_Fk = Greige.TLGreige_Id;
                      

                        context.TLKNI_GreigeProduction.Add(gp);
                   }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("data saved successfully to database");

                        frmKnitViewRep vRep = new frmKnitViewRep(31, TransNo);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();

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
