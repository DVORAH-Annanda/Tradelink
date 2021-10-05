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

namespace Cutting
{
    public partial class frmBoughtInFabric : Form
    {
        bool FormLoaded;
        DataGridViewTextBoxColumn oTxtA;   // Our Piece Number
        DataGridViewTextBoxColumn oTxtB;   // Their Piece Number
        DataGridViewTextBoxColumn oTxtC;   // Dsk Weight 
        DataGridViewTextBoxColumn oTxtD;   // Dsk Width
        DataGridViewTextBoxColumn oTxtE;   // Nett Weight
        DataGridViewTextBoxColumn oTxtF;   // Meters Per Roll
        DataGridViewComboBoxColumn oCmbB;  // Colours

        Util core;

        public frmBoughtInFabric()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Our Piece No 
            oTxtA.HeaderText = "TTS Piece No";
            oTxtA.ReadOnly = true;
            oTxtA.ValueType = typeof(string);
            oTxtA.Visible = true;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Their Piece No 
            oTxtB.HeaderText = "Foreign Piece No";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;

            oCmbB = new DataGridViewComboBoxColumn();  // 2 Piece Colour
            oCmbB.HeaderText = "Colours";
            oCmbB.ValueMember = "Col_Id";
            oCmbB.DisplayMember = "Col_Display";
        
            oTxtC = new DataGridViewTextBoxColumn();  // 3 Dsk Weight 
            oTxtC.HeaderText = "Dsk Weight";
            oTxtC.ValueType = typeof(Decimal);
            oTxtC.Visible = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 4 Dsk Width 
            oTxtD.HeaderText = "Dsk Width";
            oTxtD.ValueType = typeof(Decimal);
            oTxtD.Visible = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 5 Nett Weight
            oTxtE.HeaderText = "Nett Weight";
            oTxtE.ValueType = typeof(Decimal);
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();  // 6 Meters Per Roll
            oTxtF.HeaderText = "Meters Per Roll";
            oTxtF.ValueType = typeof(Decimal);
            oTxtF.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // Our Piece No    0
            dataGridView1.Columns.Add(oTxtB);    // Their Piece No  1
            dataGridView1.Columns.Add(oTxtC);    // dsk Weight      2 
            dataGridView1.Columns.Add(oTxtD);    // dsk Width       3
            dataGridView1.Columns.Add(oTxtE);    // Nett Weight     4
            dataGridView1.Columns.Add(oTxtF);    // Meters Per Roll 5

            core = new Util();
            
            txtDskWeight.KeyPress += core.txtWin_KeyPress;
            txtDskWeight.KeyDown += core.txtWin_KeyDownOEM;

            txtNoOfRolls.KeyPress += core.txtWin_KeyPress;
            txtNoOfRolls.KeyDown += core.txtWin_KeyDownJI;

            txtDskWidth.KeyPress += core.txtWin_KeyPress;
            txtDskWidth.KeyDown += core.txtWin_KeyDownOEM;

            txtMetersPerRoll.KeyPress += core.txtWin_KeyPress;
            txtMetersPerRoll.KeyDown += core.txtWin_KeyDown;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

            this.Text = "Brought In Fabric Receipts";

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void frmBoughtInFabric_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                dataGridView1.Rows.Clear();
 
                txtMetersPerRoll.Text = "0.00";
                txtDskWeight.Text = "0.00";
                txtDskWidth.Text = "0.00";
                txtNoOfRolls.Text = "0";
                txtNettWeight.Text = "0.00";
                
                cmboCountry.DataSource = context.TLADM_CottonOrigin.ToList();
                cmboCountry.DisplayMember = "CottonOrigin_Description";
                cmboCountry.ValueMember = "CottonOrigin_Pk";
                cmboCountry.SelectedIndex = -1;

                oCmbB.DataSource = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                oCmbB.HeaderText = "Colours";
                oCmbB.ValueMember = "Col_Id";
                oCmbB.DisplayMember = "Col_Display";

                cmboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboGreige.DisplayMember = "TLGreige_Description";
                cmboGreige.ValueMember = "TLGreige_Id";
                cmboGreige.SelectedIndex = -1;

                cmboColour.DataSource = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).ToList();
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.ValueMember = "Col_Id";
                cmboColour.SelectedIndex = -1;

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

                    if (Cell.ColumnIndex == 3 ||
                        Cell.ColumnIndex == 4 ||
                        Cell.ColumnIndex == 5)
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

                var Colour = (TLADM_Colours)cmboColour.SelectedItem;
                if (Colour == null)
                {
                    MessageBox.Show("Please select a colour");
                    return;
                }

                TransNo = core.CenturyDayNumber(DateTime.Now);
                
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
                        BoughtIn.TLBIN_Colour_FK = (int)cmboColour.SelectedValue;
                        BoughtIn.TLBIN_Dsk_Weight = (decimal)Row.Cells[2].Value;
                        BoughtIn.TLBIN_Dsk_Width = (decimal)Row.Cells[3].Value;
                        BoughtIn.TLBIN_Nett_Weight = (decimal)Row.Cells[4].Value;
                        BoughtIn.TLBIN_Meters_Roll = (decimal)Row.Cells[5].Value;
                        BoughtIn.TLBIN_TransNumber = TransNo;

                        context.TLKNI_BoughtInFabric.Add(BoughtIn);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                        //we need to store further information in a separate file for the processing in the Dye Module
                        //-----------------------------------------------------------------------------------------------------------
                        TLKNI_GreigeProduction gp = new TLKNI_GreigeProduction();
                        gp.GreigeP_Captured = true;
                        gp.GreigeP_BoughtIn = true;
                        gp.GreigeP_KnitO_Fk = BoughtIn.TLBIN_Pk;  // Provides a link to the original transaction 
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
                        gp.GreigeP_weightAvail = (decimal)Row.Cells[5].Value;
                        gp.GreigeP_Store_FK = CurrentStore.WhStore_Id;
                        gp.GreigeP_Greige_Fk = Greige.TLGreige_Id;
                        gp.GreigeP_BIFColour_FK = BoughtIn.TLBIN_Colour_FK;
                        gp.GreigeP_BoughtIn_Fk = BoughtIn.TLBIN_Pk;
                        gp.GreigeP_Meters = BoughtIn.TLBIN_Meters_Roll;

                        try
                        {
                            context.TLKNI_GreigeProduction.Add(gp);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.Message);
                            return;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");

                        Cutting.CuttingQueryParameters QParms = new CuttingQueryParameters();
                        QParms.BIFTransNumber = TransNo;

                        frmCutViewRep vRep = new frmCutViewRep(19, QParms);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }

                        vRep = new frmCutViewRep(20, QParms);
                        h = Screen.PrimaryScreen.WorkingArea.Height;
                        w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }

                        frmBoughtInFabric_Load(this, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txtNoOfRolls_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = (TextBox)sender;
            var BIFLstNumberUsed = 0;

            if (oTxt != null && FormLoaded)
            {
                
                var Origin = (TLADM_CottonOrigin)cmboCountry.SelectedItem;
                if (Origin == null)
                {
                    MessageBox.Show("Please select a Country of Origin");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var OriginDet = context.TLADM_CottonOrigin.Find(Origin.CottonOrigin_Pk);
                    if (OriginDet != null)
                    {
                        BIFLstNumberUsed = (int)OriginDet.CottonOrigin_LastNumber;
                        if (BIFLstNumberUsed == 0)
                            BIFLstNumberUsed = 1;
                    }

                    if (oTxt.Text.Length == 0 || Convert.ToInt32(oTxt.Text) == 0)
                    {
                        MessageBox.Show("Please Enter a value in this field");
                        oTxt.Focus();
                        return;
                    }


                    var NoofRolls = Convert.ToInt32(oTxt.Text);
                    var Mth = Origin.CottonOrigin_Pk;
                    var Year = DateTime.Now.Year;
                    var DOY = DateTime.Now.DayOfYear;
                    var Index = 0;

                    do
                    {
                        var RowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[RowIndex].Cells[0].Value = Mth.ToString().PadLeft(2, '0') + Year.ToString() + DOY.ToString().PadLeft(3, '0') +  " " + (++BIFLstNumberUsed).ToString().PadLeft(6, '0');
                        dataGridView1.Rows[RowIndex].Cells[1].Value = "0";
                        dataGridView1.Rows[RowIndex].Cells[2].Value = Convert.ToDecimal(txtDskWeight.Text);
                        dataGridView1.Rows[RowIndex].Cells[3].Value = Convert.ToDecimal(txtDskWidth.Text);
                        dataGridView1.Rows[RowIndex].Cells[4].Value = Convert.ToDecimal(txtNettWeight.Text);
                        dataGridView1.Rows[RowIndex].Cells[5].Value = Convert.ToDecimal(txtMetersPerRoll.Text);


                    } while (++Index < NoofRolls);

                    OriginDet.CottonOrigin_LastNumber = BIFLstNumberUsed;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
        
        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_KeyUp_1(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                if (dataGridView1.CurrentCell.ReadOnly)
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
       
    }
}
