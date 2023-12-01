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
        DataGridViewTextBoxColumn oTxtBoxC;  // 2 Reserved for Piece No index DyeBatch
        DataGridViewTextBoxColumn oTxtBoxD;  // 3 Reserved for Piece Weight
        DataGridViewTextBoxColumn oTxtBoxE;  // 4 Reserved for Piece Width - decimal
        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;

        protected readonly TTI2Entities _context;

        DataTable DataT;
        BindingSource BindingSrc; 
        

        List<DATA> fieldSelected = new List<DATA>();

        int _LastNumber;

        decimal BalanceOutstanding;
        decimal BatchBalanceCaptured;
        bool formloaded;
        bool MaintenanceTasks; 
        Util core;

        public frmGreigeRecordOfProd()
        {
            InitializeComponent();

            BindingSrc = new BindingSource();
            _context = new TTI2Entities();

            core = new Util();
            dataGridView1.AutoGenerateColumns = false;

            //============================================================
            //---------Define the datatable 
            //=================================================================
            DataT = new System.Data.DataTable();
            DataColumn column;

            //------------------------------------------------------
            // Create column 0. // Reserved for Piece No Key
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Col0";
            column.Caption = "Col0";
            column.DefaultValue = 0;
            DataT.Columns.Add(column);
            
            //------------------------------------------------------
            // Create column 1. // Reserved for Machine Used FK
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Col1";
            column.DefaultValue = 0;
            DataT.Columns.Add(column);
          
            //------------------------------------------------------
            // Create column 2. // Reserved for Piece No
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Col2";
            column.DefaultValue = String.Empty;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 3. // Reserved for Piece Weight
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Col3";
            column.DefaultValue = 0.0M;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // Reserved for a Combo Box
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Col4";
            column.DefaultValue = 0;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 5 // Reserved for a Combo Box
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Col5";
            column.DefaultValue = 0;
            DataT.Columns.Add(column);


            oTxtBoxA = new DataGridViewTextBoxColumn(); //0
            oTxtBoxA.HeaderText = "Piece Number Key";
            oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns[0].DisplayIndex = 0;


            oTxtBoxB = new DataGridViewTextBoxColumn(); //1
            oTxtBoxB.HeaderText = "Machine No Used";
            oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.Visible = false;
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns[1].DisplayIndex = 1;


            oTxtBoxC = new DataGridViewTextBoxColumn(); //2
            oTxtBoxC.HeaderText = "Piece Number";
            oTxtBoxC.DataPropertyName = DataT.Columns[2].ColumnName;
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.Visible = true;
            oTxtBoxC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtBoxD = new DataGridViewTextBoxColumn(); //3
            oTxtBoxD.HeaderText = "Piece Weight";
            oTxtBoxD.DataPropertyName = DataT.Columns[3].ColumnName;
            oTxtBoxD.ValueType = typeof(decimal);
            oTxtBoxD.Visible = true;
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns[3].DisplayIndex = 3;

            oCmbBoxA = new DataGridViewComboBoxColumn();
            oCmbBoxA.HeaderText = "Shifts";
            oCmbBoxA.DataPropertyName = DataT.Columns[4].ColumnName;
            oCmbBoxA.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oCmbBoxA);
            dataGridView1.Columns[4].DisplayIndex = 4;

            oCmbBoxB = new DataGridViewComboBoxColumn();
            oCmbBoxB.HeaderText = "Operators";
            oCmbBoxB.DataPropertyName = DataT.Columns[5].ColumnName;
            oCmbBoxB.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oCmbBoxB);
            dataGridView1.Columns[5].DisplayIndex = 5;

            txtNoOfPieces.KeyPress += core.txtWin_KeyPress;
            txtNoOfPieces.KeyDown += core.txtWin_KeyDownJI;

            txtDskWeight.KeyPress += core.txtWin_KeyPress;
            txtDskWeight.KeyDown += core.txtWin_KeyDownOEM;

            txtDiskWidth.KeyPress += core.txtWin_KeyPress;
            txtDiskWidth.KeyDown += core.txtWin_KeyDownOEM;

            txtPieceNo.KeyPress += core.txtWin_KeyPress;
            txtPieceNo.KeyDown += core.txtWin_KeyDownJI;

            BindingSrc.DataSource = DataT;
            dataGridView1.DataSource = BindingSrc;
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }
        private void frmGreigeRecordOfProd_Load(object sender, EventArgs e)
        {
            formloaded = false;
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;

            BalanceOutstanding = 0.00M;
            txtBalance.Text = BalanceOutstanding.ToString();
            txtNoOfPieces.Text = "0";
            txtDskWeight.Text = "0.0";
            txtDiskWidth.Text = "0.0";

            MaintenanceTasks = false;

            _LastNumber = 0;

            txtPieceNo.Text = _LastNumber.ToString();

           
            cmboKnitOrders.DataSource = _context.TLKNI_Order.Where(x => x.KnitO_OrderConfirmed && !x.KnitO_Closed).OrderBy(x => x.KnitO_OrderNumber).ToList();
            cmboKnitOrders.ValueMember = "KnitO_Pk";
            cmboKnitOrders.DisplayMember = "KnitO_OrderNumber";
            cmboKnitOrders.SelectedValue = -1;

            var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
            if (Dept != null)
            {
                    oCmbBoxA.DataSource = _context.TLADM_Shifts.Where(x => x.Shft_Dept_FK == Dept.Dep_Id).ToList();
                    oCmbBoxA.ValueMember = "shft_Pk";
                    oCmbBoxA.DisplayMember = "Shft_Description";
                    oCmbBoxA.HeaderText = "Shifts";

                    cmboDefaultShift.DataSource = _context.TLADM_Shifts.Where(x => x.Shft_Dept_FK == Dept.Dep_Id).ToList();
                    cmboDefaultShift.ValueMember = "Shft_Pk";
                    cmboDefaultShift.DisplayMember = "Shft_Description";
                    cmboDefaultShift.SelectedValue = -1;

                    oCmbBoxB.DataSource = _context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    oCmbBoxB.ValueMember = "MachOp_Pk";
                    oCmbBoxB.DisplayMember = "MachOp_Description";
                    oCmbBoxB.HeaderText = "Operator";

                    cmboDefaultOperator.DataSource = _context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    cmboDefaultOperator.ValueMember = "MachOp_Pk";
                    cmboDefaultOperator.DisplayMember = "MachOP_Description";
                    cmboDefaultOperator.SelectedValue = -1;

                    var Department = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Department != null)
                    {

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
                    if (Cell.ColumnIndex == 3 || Cell.ColumnIndex == 4)
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
            Decimal DskWght = 0.0M;
            Decimal DskWidth = 0.0M;
            Decimal MaintenanceValue = 0.00M;

            IList<TLKNI_YarnAllocTransctions> PalletTrans = null; 

            if (oBtn != null && formloaded)
            {
                try
                {
                    DskWght = decimal.Parse(txtDskWeight.Text);
                    if(DskWght <= 0 )
                    {
                        MessageBox.Show("Please enter a proper dsk weight");
                        return;
                    }

                    DskWidth = decimal.Parse(txtDiskWidth.Text);
                    if (DskWidth <= 0)
                    {
                        MessageBox.Show("Please enter a proper dsk width");
                        return;
                    }
                }
                catch (Exception ex)
                { 
                    MessageBox.Show( ex.Message + " Please enter a Dsk Weight for this shift");
                    return;
                }
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

                        var TLADMGreige = context.TLADM_Griege.Find(KO.KnitO_Product_FK);

                        foreach (DataRow row in DataT.Rows)
                        {
                            TLKNI_GreigeProduction griegP = new TLKNI_GreigeProduction();
                            var _Key = row.Field<int>(0);
                            if (_Key != 0)
                            {
                                griegP = context.TLKNI_GreigeProduction.Find(_Key);
                            }
                            else
                            {
                                griegP.GreigeP_KnitO_Fk = KO.KnitO_Pk;
                                griegP.GreigeP_Greige_Fk = KO.KnitO_Product_FK;
                                griegP.GreigeP_Machine_FK = row.Field<int>(1);
                                griegP.GreigeP_PieceNo = row.Field<string>(2);
                                if (KO.KnitO_Size_Fk != null)
                                    griegP.GreigeP_Size_Fk = (int)KO.KnitO_Size_Fk;
                                if (KO.KnitO_Colour_Fk != null)
                                    griegP.GreigeP_BIFColour_FK = (int)KO.KnitO_Colour_Fk;
                            }

                            griegP.GreigeP_MergeDetail = MergeDetails;
                            griegP.GreigeP_weight = row.Field<decimal>(3);
                            griegP.GreigeP_weightAvail = row.Field<decimal>(3);
                            
                            if (row.Field<int>(4) != 0)
                            {
                                griegP.GreigeP_Shift_FK = row.Field<int>(4);
                            }
                            else
                            {
                                griegP.GreigeP_Shift_FK = _context.TLADM_Shifts.FirstOrDefault().Shft_Pk; 
                            }
                            if (row.Field<int>(5) != 0)
                            {
                                griegP.GreigeP_Operator_FK = row.Field<int>(5);
                            }
                            else
                            {
                                griegP.GreigeP_Operator_FK = (from T1 in _context.TLADM_Departments
                                          join T2 in _context.TLADM_MachineOperators
                                          on T1.Dep_Id equals T2.MachOp_Department_FK
                                          where !T2.MachOp_Discontinued && T1.Dep_Id == 11
                                          select T2).FirstOrDefault().MachOp_Pk;

                            }
                            if (griegP.GreigeP_PDate == null)
                            {
                                griegP.GreigeP_PDate = dtpProduction.Value;
                            }
                           
                            BatchBalanceCaptured += row.Field<decimal>(3);

                            griegP.GreigeP_Captured = true;
                                
                            griegP.GreigeP_PalletNo = PalletNo;
                            griegP.GreigeP_YarnSupplier = YarnSupplier;
                            griegP.GreigeP_YarnTex = YarnTex;
                            griegP.GreigeP_DskWeight = DskWght;
                            griegP.GreigeP_DiskWidth = DskWidth;

                            if (TLADMGreige.TLGreige_CubicWeight != 0 && DskWght != 0)
                            {
                                griegP.GreigeP_VarianceDiskWeight = core.CalculateDskVariance(TLADMGreige.TLGreige_CubicWeight, DskWght);
                            }

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
                                var CustDetails = context.TLADM_CustomerFile.Find(KO.KnitO_Customer_FK);
                                if (CustDetails != null)
                                {
                                   if (!CustDetails.Cust_FabricCustomer && !CustDetails.Cust_CommissionCust)
                                   {
                                       var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                                       if (TranType != null)
                                       {
                                           griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                       }
                                   }
                                   else if (CustDetails.Cust_CommissionCust)
                                   {
                                       var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1300).FirstOrDefault();
                                       if (TranType != null)
                                       {
                                           griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                       }
                                   }
                                   else
                                   {
                                      // DJL 25/10/2021....Not a bug as per Thys Greef 3rd Party yarn goes into TTS Store pending payment.
                                      var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                                      if (TranType != null)
                                      {
                                         griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                      }
                                   }
                                }
                                else
                                {
                                    var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                                    if (TranType != null)
                                    {
                                            griegP.GreigeP_Store_FK = TranType.TrxT_Pk;
                                    }
                                }
                            }
                            //==============================================================================
                            if (_Key == 0)
                            {
                                _context.TLKNI_GreigeProduction.Add(griegP);
                            }
                        }

                        if (BatchBalanceCaptured >= KO.KnitO_Weight)
                        {
                            KO.KnitO_ProductionCaptured = true;
                        }
                        
                        var MachDet = context.TLADM_MachineDefinitions.Find(KO.KnitO_Pk);
                        if(MachDet != null)
                        {
                            var MC = context.TLKNI_MachineLastNumber.Where(x=>x.TLMDD_MachineCode  == MachDet.MD_MachineCode).FirstOrDefault();
                            if (MC != null)
                            {
                                MC.TLMDD_LastNumber = _LastNumber;
                                txtPieceNo.Text = _LastNumber.ToString(); 
                            }

                            if (MaintenanceTasks)
                            {
                                var MainTasks = context.TLADM_MachineMaintenance.Where(x => x.mman_Machine_Fk == MachDet.MD_Pk && !x.mman_Reset).ToList();
                                foreach (var MainTask in MainTasks)
                                {
                                    if (MainTask.mman_Interval_Between_Tasks != 0)
                                    {
                                        MainTask.mman_Interval_CurrentValue += (int)BatchBalanceCaptured;
                                        if(MainTask.mman_Interval_CurrentValue > MainTask.mman_Interval_Between_Tasks)
                                        {
                                            var Msg = "Mainteance needs to be performed on " + MachDet.MD_Description + Environment.NewLine;

                                            var MainDet = (from T1 in context.TLADM_MachineMaintenance
                                                           join T2 in context.TLADM_MachineMaintenanceTasks
                                                           on T1.mman_MaintenanceTask_FK equals T2.TLMtask_Pk
                                                           where T2.TLMtask_Pk == MainTask.mman_MaintenanceTask_FK
                                                           select T2).FirstOrDefault().TLMtask_Description;

                                            Msg += "Maintenance Detail : " + MainDet;
                                            MessageBox.Show(Msg);
                                        }
                                    }

                                    if (MainTask.mman_Volume_Between_Tasks != 0)
                                    {
                                        MainTask.mman_Volume_CurrentValue += (int)BatchBalanceCaptured;
                                        if (MainTask.mman_Volume_CurrentValue > MainTask.mman_Volume_Between_Tasks)
                                        {
                                            var Msg = "Mainteance needs to be performed on " + MachDet.MD_Description + Environment.NewLine;

                                            var MainDet = (from T1 in context.TLADM_MachineMaintenance
                                                           join T2 in context.TLADM_MachineMaintenanceTasks
                                                           on T1.mman_MaintenanceTask_FK equals T2.TLMtask_Pk
                                                           where T2.TLMtask_Pk == MainTask.mman_MaintenanceTask_FK
                                                           select T2).FirstOrDefault().TLMtask_Description;

                                            Msg += "Maintenance Detail " + MainDet;
                                            MessageBox.Show(Msg);
                                        }
                                    }
                                }
                               
                            }
                        }

                        try
                        {
                            _context.SaveChanges();
                            DataT.Rows.Clear();

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
                DataT.Rows.Clear();
                var KO = (TLKNI_Order)cmboKnitOrders.SelectedItem;
                if (KO != null)
                {
                    txtBalance.Text = Math.Round(KO.KnitO_Weight, 2).ToString();
                    using (var context = new TTI2Entities())
                    {
                        var KMachine = context.TLADM_MachineDefinitions.Find(KO.KnitO_Machine_FK);
                        if (KMachine != null)
                        {
                            if(context.TLADM_MachineMaintenance.Where(x=>x.mman_Machine_Fk== KMachine.MD_Pk).Count() == 0 )
                            {
                                MessageBox.Show("There are no maintenace tasks set for this Machine", KMachine.MD_Description, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MaintenanceTasks = true;
                            }
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
                            DataRow Row = DataT.NewRow();

                            Row[0] = row.GreigeP_Pk;
                            Row[1] = row.GreigeP_Machine_FK;
                            Row[2] = row.GreigeP_PieceNo;
                            Row[3] = Math.Round(row.GreigeP_weight,1);
                           
                            if(row.GreigeP_Shift_FK != 0)
                            {
                                Row[4] = (int)row.GreigeP_Shift_FK;
                            }

                            if (row.GreigeP_Operator_FK != 0)
                            {
                                Row[5] = (int)row.GreigeP_Operator_FK;
                            }
                            DataT.Rows.Add(Row);

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
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                var CurrentRow = oDgv.CurrentRow;
                if(CurrentRow != null && e.ColumnIndex == 3)
                {
                    if((TLADM_Shifts)cmboDefaultShift.SelectedItem != null)
                    {
                        CurrentRow.Cells[e.ColumnIndex + 1].Value = (int)cmboDefaultShift.SelectedValue;
                    }
                    if ((TLADM_MachineOperators)cmboDefaultOperator.SelectedItem != null)
                    {
                        CurrentRow.Cells[e.ColumnIndex + 2].Value = (int)cmboDefaultOperator.SelectedValue;
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
                        {
                            fieldSelected.RemoveAt(RecordIndex);
                        }
                        else
                        {
                            Record._Weight = Amount;
                        }
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
                int DefaultShift = (int)cmboDefaultShift.SelectedValue;
                int DefaultOperator = (int)cmboDefaultOperator.SelectedValue;
                var Result = int.TryParse(oTxt.Text, out NoOfPieces);
                if (Result && NoOfPieces != 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        if(DefaultShift == 0)
                        {
                            DefaultShift = cmboDefaultShift.Items.Count - 1;
                        }

                        if(DefaultOperator == 0)
                        {
                            DefaultOperator = cmboDefaultOperator.Items.Count - 1;
                        }

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
                                        //  var index = dataGridView1.Rows.Add();
                                        DataRow Row = DataT.NewRow();

                                        formloaded = false;
                                        Row[0] = 0;
                                        Row[1] = MachineKey;
                                        Row[2] = PieceNo;
                                        Row[3] = 0.0M;
                                        Row[4] = DefaultShift;
                                        Row[5] = DefaultOperator;

                                        DataT.Rows.Add(Row);
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
                            else
                            {
                                MessageBox.Show("Machine Details no longer exist ?????? ");
                                return;
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

        private void frmGreigeRecordOfProd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

       
    }
}
