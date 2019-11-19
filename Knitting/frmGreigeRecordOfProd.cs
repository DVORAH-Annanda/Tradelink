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
    public partial class frmGreigeRecordOfProd : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;  // 0 Reserved for Piece No index 
        DataGridViewTextBoxColumn oTxtBoxB;  // 1 Reserved for Machine Used Fk
        DataGridViewTextBoxColumn oTxtBoxC;  // 2 Reserved for Piece No
        DataGridViewTextBoxColumn oTxtBoxD;  // 3 Reserved for Piece Weight
        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;

        List<DATA> fieldSelected = new List<DATA>();

        int _LastNumber;

        decimal BalanceOutstanding;
        decimal BatchBalanceCaptured;
        bool formloaded;

        Util core;

        public frmGreigeRecordOfProd()
        {
            InitializeComponent();

            core = new Util();
            dataGridView1.AutoGenerateColumns = false;
            
            oTxtBoxA = new DataGridViewTextBoxColumn(); //0
            oTxtBoxA.HeaderText = "Piece Number Key";
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn(); //1
            oTxtBoxB.HeaderText = "Piece Number";
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.Visible = false;

            oTxtBoxC = new DataGridViewTextBoxColumn(); //2
            oTxtBoxC.HeaderText = "Piece Number";
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.Visible = true;
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD = new DataGridViewTextBoxColumn(); //3
            oTxtBoxD.HeaderText = "Piece Weight";
            oTxtBoxD.ValueType = typeof(decimal);
            oTxtBoxD.Visible = true;
        

            txtNoOfPieces.KeyPress += core.txtWin_KeyPress;
            txtNoOfPieces.KeyDown += core.txtWin_KeyDownJI;

            txtPieceNo.KeyPress += core.txtWin_KeyPress;
            txtPieceNo.KeyDown += core.txtWin_KeyDownJI;
 
            SetUp();

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oCmbBoxA);
            dataGridView1.Columns.Add(oCmbBoxB);

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        void SetUp()
        {
            formloaded = false;
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;

            BalanceOutstanding = 0.00M;
            txtBalance.Text = BalanceOutstanding.ToString();
            txtNoOfPieces.Text = "0";
            _LastNumber = 0;

            txtPieceNo.Text = _LastNumber.ToString();

            using (var context = new TTI2Entities())
            {
                cmboKnitOrders.DataSource = context.TLKNI_Order.Where(x=>x.KnitO_OrderConfirmed && !x.KnitO_Closed).OrderBy(x=>x.KnitO_OrderNumber).ToList();
                cmboKnitOrders.ValueMember = "KnitO_Pk";
                cmboKnitOrders.DisplayMember = "KnitO_OrderNumber";
                cmboKnitOrders.SelectedValue = -1;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    oCmbBoxA = new DataGridViewComboBoxColumn();
                    oCmbBoxA.DataSource = context.TLADM_Shifts.Where(x => x.Shft_Dept_FK == Dept.Dep_Id).ToList();
                    oCmbBoxA.ValueMember = "shft_Pk";
                    oCmbBoxA.DisplayMember = "Shft_Description";
                    oCmbBoxA.HeaderText = "Shifts";

                    cmboDefaultShift.DataSource = context.TLADM_Shifts.Where(x => x.Shft_Dept_FK == Dept.Dep_Id).ToList();
                    cmboDefaultShift.ValueMember = "Shft_Pk";
                    cmboDefaultShift.DisplayMember = "Shft_Description";
                    cmboDefaultShift.SelectedValue = -1;

                    oCmbBoxB = new DataGridViewComboBoxColumn();
                    oCmbBoxB.DataSource = context.TLADM_MachineOperators.Where(x=>x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    oCmbBoxB.ValueMember = "MachOp_Pk";
                    oCmbBoxB.DisplayMember = "MachOp_Description";
                    oCmbBoxB.HeaderText = "Operator";

                    cmboDefaultOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    cmboDefaultOperator.ValueMember = "MachOp_Pk";
                    cmboDefaultOperator.DisplayMember = "MachOP_Description";
                    cmboDefaultOperator.SelectedValue = -1;

                    var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Department != null)
                    {
                      
                    }
                }
            }
            formloaded = true;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (formloaded)
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

                        /*
                        var val = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());

                        decimal bal = Convert.ToDecimal(txtBalance.Text);

                        txtBalance.Text = Math.Round(bal - val,2).ToString();
                         */
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            DataGridView oDgv = dataGridView1;
            String PalletNo = string.Empty;
            String YarnSupplier = String.Empty;
            Decimal YarnTex = 0.00M;
            TLADM_Yarn YarnType = null;
            string MergeDetails = string.Empty;

            IList<TLKNI_YarnAllocTransctions> PalletTrans = null; 

            if (oBtn != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    var KO = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                    if (KO != null)
                    {
                        var _Begin = dtpProduction.Value.ToShortDateString();
                        DateTime psd = Convert.ToDateTime(_Begin);
                        DateTime time = dtpTime.Value;
                        var YarnAllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KO.KnitO_Pk && x.TLKYT_TranType == 1).FirstOrDefault();
                        if (YarnAllocTrans != null)
                        {
                            var YarnOrderPallet = context.TLKNI_YarnOrderPallets.Find(YarnAllocTrans.TLKYT_YOP_FK);
                            if (YarnOrderPallet != null)
                            {
                                var YarnOrder = context.TLSPN_YarnOrder.Find(YarnOrderPallet.TLKNIOP_YarnOrder_FK);
                                if (YarnOrder != null)
                                {
                                    PalletNo = YarnOrder.YarnO_OrderNumber.ToString() + " - " + YarnOrderPallet.TLKNIOP_PalletNo;
 
                                    YarnType = context.TLADM_Yarn.Find(YarnOrder.Yarno_YarnType_FK);
                                    if (YarnType != null)
                                    {
                                        YarnTex = YarnType.YA_TexCount;
                                        YarnSupplier = context.TLADM_Suppliers.Find(YarnType.YA_Supplier_FK).Sup_Description;
                                    }
                                }
                            }
                        }
                        
                        KO = context.TLKNI_Order.Find(KO.KnitO_Pk);
                        if (KO != null)
                        {
                            var YarnO = context.TLSPN_YarnOrder.Find(KO.KnitO_YarnO_FK);
                            if(YarnO != null)
                            {
                                if(YarnO.YarnO_MergeContract_FK != null)
                                {
                                    var CottonM = context.TLSPN_CottonMerge.Find(YarnO.YarnO_MergeContract_FK);
                                    if(CottonM != null)
                                    {
                                        MergeDetails = CottonM.TLCTM_Description; 
                                    }
                                }
                            }

                            if(KO.KnitO_ProductionStartDate == null)
                            {
                                KO.KnitO_ProductionStartDate = psd.AddHours(time.Hour);
                                KO.KnitO_ProductionStartDate = psd.AddMinutes(time.Minute);
                            }
                        }

                        PalletTrans = context.TLKNI_YarnAllocTransctions.Where( x=>x.TLKYT_KnitOrder_FK == KO.KnitO_Pk && x.TLKYT_TranType == 1).ToList();

                        foreach (DataGridViewRow row in oDgv.Rows)
                        {
                            if ((decimal)row.Cells[3].Value > 0)
                            {
                                if (row.Cells[4].Value == null || row.Cells[5].Value == null)
                                {
                                    MessageBox.Show("Row Number " + (row.Index + 1).ToString() + " incomplete");
                                    return;
                                }

                                TLKNI_GreigeProduction griegP = new TLKNI_GreigeProduction();
                                var _Key = (int)row.Cells[0].Value;
                               
                                if (_Key != 0)
                                    griegP = context.TLKNI_GreigeProduction.Find(_Key);
                                else
                                { 
                                    griegP.GreigeP_KnitO_Fk = KO.KnitO_Pk;
                                    griegP.GreigeP_Greige_Fk = KO.KnitO_Product_FK;
                                    griegP.GreigeP_Machine_FK = (int)row.Cells[1].Value;
                                    griegP.GreigeP_PieceNo = row.Cells[2].Value.ToString();
                                    if(KO.KnitO_Size_Fk != null)
                                        griegP.GreigeP_Size_Fk = (int)KO.KnitO_Size_Fk;
                                    if(KO.KnitO_Colour_Fk != null)
                                        griegP.GreigeP_BIFColour_FK = (int)KO.KnitO_Colour_Fk;


                                }

                                griegP.GreigeP_MergeDetail = MergeDetails;

                                griegP.GreigeP_weight = (decimal)row.Cells[3].Value;
                                griegP.GreigeP_weightAvail = (decimal)row.Cells[3].Value;
                                if (row.Cells[4].Value != null)
                                    griegP.GreigeP_Shift_FK = (int)row.Cells[4].Value;
                                if (row.Cells[5].Value != null)
                                    griegP.GreigeP_Operator_FK = (int)row.Cells[5].Value;
                                if (griegP.GreigeP_PDate == null)
                                    griegP.GreigeP_PDate = dtpProduction.Value;

                           
                                BatchBalanceCaptured += (decimal)row.Cells[3].Value;

                                griegP.GreigeP_Captured = true;
                                
                                griegP.GreigeP_PalletNo = PalletNo;
                                griegP.GreigeP_YarnSupplier = YarnSupplier;
                                griegP.GreigeP_YarnTex = YarnTex;

                                var TLADMGreige = context.TLADM_Griege.Find(griegP.GreigeP_Greige_Fk);
                                if (TLADMGreige != null)
                                {
                                    // We need to start storing the meters knitted for statistical purposes
                                    //===========================================================================
                                    var FabWeight = context.TLADM_FabricWeight.Find(TLADMGreige.TLGreige_FabricWeight_FK);
                                    var FabWidth = context.TLADM_FabWidth.Find(TLADMGreige.TLGreige_FabricWidth_FK);
                                    var FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);
                                    griegP.GreigeP_Meters = FabricYield * griegP.GreigeP_weightAvail;

                                }
                                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                                if (Dept != null)
                                {
                                    if (KO.KnitO_YarnO_FK != null)
                                    {
                                        var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                                        if (TranType != null)
                                        {
                                            griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                        }
                                    }
                                    else
                                    {
                                        if (KO.KnitO_CommisionCust)
                                        {
                                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1300).FirstOrDefault();
                                            if (TranType != null)
                                            {
                                                griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                            }
                                        }
                                        else
                                        {
                                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1200).FirstOrDefault();
                                            if (TranType != null)
                                            {
                                                griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                            }
                                        }
                                    }
                                }
                                //==============================================================================
                                if (_Key == 0)
                                    context.TLKNI_GreigeProduction.Add(griegP);
                            }
                            
                        }

                        if (BatchBalanceCaptured >= KO.KnitO_Weight)
                            KO.KnitO_ProductionCaptured = true;

                        var MachDet = context.TLADM_MachineDefinitions.Find(KO.KnitO_Pk);
                        if(MachDet != null)
                        {
                            var MC = context.TLKNI_MachineLastNumber.Where(x=>x.TLMDD_MachineCode  == MachDet.MD_MachineCode).FirstOrDefault();
                            if (MC != null)
                            {
                                MC.TLMDD_LastNumber = _LastNumber;
                                txtPieceNo.Text = _LastNumber.ToString(); 
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            oDgv.Rows.Clear();
                            txtNoOfPieces.Text = "0";
                            _LastNumber = 0;
                            MessageBox.Show("Data saved to database");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void cmboKnitOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            DataGridView oDgv = dataGridView1;
            BatchBalanceCaptured = 0.00M;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                var KO = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                if (KO != null)
                {
                    txtBalance.Text = Math.Round(KO.KnitO_Weight, 2).ToString();
                    using (var context = new TTI2Entities())
                    {
                        var KMachine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                        if (KMachine != null)
                        {
                            var LstN = context.TLKNI_MachineLastNumber.Where(x => x.TLMDD_MachineCode == KMachine.MD_MachineCode).FirstOrDefault();
                            if (LstN != null)
                                txtPieceNo.Text = LstN.TLMDD_LastNumber.ToString();
                        }

                        if (context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk && x.GreigeP_Captured).Count() > 0)
                            BatchBalanceCaptured = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk && x.GreigeP_Captured).Sum(x => (decimal ?)x.GreigeP_weight) ?? 0.00M;
                                                
                        var Existing = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KO.KnitO_Pk /* && !x.GreigeP_Captured*/).OrderBy(x=>x.GreigeP_PieceNo).ToList();
                        if (Existing.Count != 0)
                            txtBalance.Text = Math.Round(KO.KnitO_Weight - BatchBalanceCaptured, 2).ToString();
                          
                        txtGreigeQual.Text = context.TLADM_Griege.Find(KO.KnitO_Product_FK).TLGreige_Description;
                        
                        formloaded = false;
                        foreach (var row in Existing)
                        {
                            var index = oDgv.Rows.Add();
                            oDgv.Rows[index].Cells[0].Value = row.GreigeP_Pk;
                            oDgv.Rows[index].Cells[1].Value = row.GreigeP_Machine_FK;
                            oDgv.Rows[index].Cells[2].Value = row.GreigeP_PieceNo;
                            oDgv.Rows[index].Cells[3].Value = Math.Round(row.GreigeP_weight,1);
                            if (row.GreigeP_Shift_FK != null)
                                oDgv.Rows[index].Cells[4].Value = row.GreigeP_Shift_FK;
                            if (row.GreigeP_Operator_FK != null)
                                oDgv.Rows[index].Cells[5].Value = row.GreigeP_Operator_FK;
                            
                        }
                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[2];
                            this.dataGridView1.BeginEdit(true);
                        }
                        formloaded = true;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Knit Order from the drop down box");
                    return;
                }

                frmKnitViewRep vRep = new frmKnitViewRep(26, selected.KnitO_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                var CurrentRow = oDgv.CurrentRow;

                if (e.ColumnIndex == 3 && Convert.ToDecimal(oDgv.CurrentRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString()) > 0)
                {
                    if (CurrentRow != null)
                    {
                        var oShift = (TLADM_Shifts)cmboDefaultShift.SelectedItem;
                        if (oShift != null)
                        {
                            CurrentRow.Cells[e.ColumnIndex + 1].Value = oShift.Shft_Pk;
                        }

                        var oOperator = (TLADM_MachineOperators)cmboDefaultOperator.SelectedItem;
                        if (oOperator != null)
                        {
                            CurrentRow.Cells[e.ColumnIndex + 2].Value = oOperator.MachOp_Pk;
                        }
                    
                    }
                }
            }
        }

        private struct DATA
        {
            public int _RowIndex;
            public decimal _Weight;

            public DATA(int Key, decimal Weight)
            {
                this._RowIndex = Key;
                this._Weight = Weight;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (e.ColumnIndex == 3 && formloaded)
            {
                var Bal = Convert.ToDecimal(txtBalance.Text);
                var Cell = oDgv.CurrentCell;

                if (Cell != null)
                {
                    var Amount = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());
                    var Record = fieldSelected.Find(x => x._RowIndex == e.RowIndex);
                    var RecordIndex = fieldSelected.IndexOf(Record);
                    if (RecordIndex != -1)
                    {
                        Bal += Record._Weight;
                        if (Amount == 0)
                            fieldSelected.RemoveAt(RecordIndex);
                        else
                            Record._Weight = Amount;  
                    }
                    else
                    {
                        fieldSelected.Add(new DATA(e.RowIndex, Amount));
                    }

                    txtBalance.Text = Math.Round(Bal - Amount, 2).ToString();
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
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

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
        }

        private void btnPieces_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            /*
            if (oBtn != null && formloaded)
            {
                var KnitOrderSelected = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                if (KnitOrderSelected == null)
                {
                    MessageBox.Show("Please select an Knit order from the drop down list");
                    return;
                }
                // In the event of a production over run this facility will allow for that addition of any extra pieces
                //-----------------------------------------------------------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    var Machine = context.TLADM_MachineDefinitions.Find(KnitOrderSelected.KnitO_Machine_FK);
                    if (Machine != null)
                    {
                        int LastNumber = Machine.MD_LastNumberUsed;
                        var index = dataGridView1.Rows.Add();
                        //-----------------------------------------------------------------
                        //0  Piece No Key 
                        //1  Actual Key Number
                        //2  Production Value 
                        //   Have to drop the 1st Character of Machine Code
                        //---------------------------------------------------------------
                        var Code = Machine.MD_MachineCode;
                        Code = Code.Substring(1, -1 + Code.Length);
                        formloaded = false;
                        dataGridView1.Rows[index].Cells[0].Value = 0;  // this is the default for the moment
                        dataGridView1.Rows[index].Cells[1].Value = Code + LastNumber.ToString();
                        dataGridView1.Rows[index].Cells[2].Value = 0.00M;
                        formloaded = true;
                        LastNumber += 1;
                        context.UpdateLastNumber(LastNumber, Machine.MD_MachineCode);
                    }
                }
            }
             */ 
        }

        private void txtNoOfPieces_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                int NoOfPieces = 0;
                var Result = int.TryParse(oTxt.Text, out NoOfPieces);
                if (Result && NoOfPieces != 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        string Code = string.Empty;
                        int LastNumber = 0;
                        int MachineKey = 0;
                      
                        var KO = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                        if (KO != null)
                        {
                            var Mach = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                            if (Mach != null)
                            {
                                Code = Mach.MD_MachineCode.Remove(0, 1).PadLeft(2, '0');
                                int.TryParse(txtPieceNo.Text, out LastNumber);
                                MachineKey = Mach.MD_Pk;
                                
                                int i = 0;
                                do
                                {
                                    var PieceNo = Code + LastNumber.ToString().PadLeft(5, '0');

                                    var GriegProd = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PieceNo == PieceNo).FirstOrDefault();
                                    if (GriegProd == null)
                                    {
                                        var index = dataGridView1.Rows.Add();
                                        formloaded = false;
                                        dataGridView1.Rows[index].Cells[0].Value = 0;  // this is the default for the moment
                                        dataGridView1.Rows[index].Cells[1].Value = MachineKey;
                                        dataGridView1.Rows[index].Cells[2].Value = PieceNo;
                                        dataGridView1.Rows[index].Cells[3].Value = 0.0M;
                                        formloaded = true;
                                        LastNumber += 1;
                                    }
                                    else
                                    {
                                        MessageBox.Show(" Piece Number " + PieceNo + " has already been capture ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                } while (++i < NoOfPieces);

                                _LastNumber = LastNumber;
                            }
                        }
                    }
                }
            }
        }

        private void cmboMachineOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null)
            {
                var selected = (TLADM_MachineDefinitions)oCmbo.SelectedItem;
                if (selected != null)
                    txtPieceNo.Text = selected.MD_LastNumberUsed.ToString();
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
             DataGridView oDgv = (DataGridView)sender;
             if (oDgv != null && formloaded)
             {
                 if (e.ColumnIndex == 3)
                 {
                     decimal wt = 0.0M;
                     decimal.TryParse(e.FormattedValue.ToString(), out wt);
                     if (wt > 0)
                     {
                         var KnitOrder = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                         if (KnitOrder != null)
                         {
                             using (var context = new TTI2Entities())
                             {
                                 var Qual = context.TLADM_Griege.Find(KnitOrder.KnitO_Product_FK);
                                 if (Qual != null)
                                 {
                                    var MinWeightAllowed = Qual.TLGreige_KgPerPiece / Convert.ToDecimal(1.25);
                                    var MaxWeightAllowed = Qual.TLGreige_KgPerPiece * Convert.ToDecimal(1.25);
                                     if (wt > MaxWeightAllowed)
                                     {
                                         String s = String.Format("The maximum weight permitted is {0} ", Math.Round(MaxWeightAllowed,2));
                                         using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                         {
                                             MessageBox.Show("The value entered exceeds the maximum weight allowed", s);
                                         }
                                         e.Cancel = true;
                                     }
                                     else if(wt < MinWeightAllowed)
                                    {
                                        String s = String.Format("The minimum weight permitted is {0} ", Math.Round(MinWeightAllowed, 2));
                                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                        {
                                            DialogResult res =MessageBox.Show("The value entered is less than the minimum weight expected. Proceed ?", s, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                            if(res == DialogResult.No)
                                            {
                                                e.Cancel = true;
                                            }
                                        }
                                       
                                        
                                    }
                                 }
                            }

                         }
                     }
                     else
                     {
                         using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                         {
                             MessageBox.Show("Please enter a value");
                         }
                         e.Cancel = true;
                     }
                 }
             }
        }
    }
}
