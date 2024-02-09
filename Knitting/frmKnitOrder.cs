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
    public partial class frmKnitOrder : Form
    {
        bool formloaded;
    
        bool EditMode;

        DataTable YarnAvailable;
        DataColumn column;

        BindingSource BindingSrc;


        bool EditKO;
        bool[] SourceSelected;
        string[][] MandatoryFields;
        bool[] MandSelected;
        int LastNumberUsed;
   
        Util core;
        TLKNI_Order _CurrentOrder;

        DataGridViewTextBoxColumn oTxtA;   // 0 Primary Yarn Order Pk
        DataGridViewCheckBoxColumn oChkA;  // 1 Select
        DataGridViewTextBoxColumn oTxtB;   // 2 Pallet Number
        DataGridViewTextBoxColumn oTxtC;   // 3 Pallet Weight 
        DataGridViewTextBoxColumn oTxtD;   // 4 Pallet Cones Available
        DataGridViewTextBoxColumn oTxtE;   // 5 Pallet Weight Reserved
        DataGridViewTextBoxColumn oTxtF;   // 6 Pallet Cones Reserved

        int TrimSize;

        public frmKnitOrder()
        {
            InitializeComponent();
            MandatoryFields = new string[][]
                {   new string[] {"dtpKnitOrderDate", "Please select a knit order date", "0"},
                    new string[] {"dtpDateRequired", "Please select a date required", "1"},
                    new string[] {"txtOrderKG", "Please enter the order weight", "2"}, 
                    new string[] {"cmbMachines", "Please select a machine to process this order", "3"},
                    new string[] {"txtYLTSetting", "Please enter YLT", "4" },
                    new string[] {"cmbFabricType", "Please select the Greige type", "5" }
                };

            YarnAvailable = new DataTable();
            BindingSrc = new BindingSource();
            core = new Util();

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "YarnAvailable_Pk";
            column.Caption = "YarnAvailable Key";
            column.DefaultValue = 0;
            YarnAvailable.Columns.Add(column);


            //=================================================
            // Col 1 
            //=====================================================
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "SelectRecord_Pk";
            column.Caption = "Select Yarn Pallet";
            column.DefaultValue = false;
            YarnAvailable.Columns.Add(column);

            //=====================================================
            // Col 2 
            //======================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Pallet_Number";
            column.Caption = "Pallet Number";
            column.DefaultValue = string.Empty;
            YarnAvailable.Columns.Add(column);

            //======================================================
            // Col 3
            //================================================================
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Pallet_Weight";
            column.Caption = "Pallet Weight";
            column.DefaultValue = 0.0M;
            YarnAvailable.Columns.Add(column);

            //======================================================================
            // Col 4 
            //================================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "ConesAvailable_Pk";
            column.Caption = "Cones Available";
            column.DefaultValue = 0;
            YarnAvailable.Columns.Add(column);

            //===========================================================
            // Col 5 
            //===========================================================
            column = new DataColumn();
            column.DataType = typeof(Decimal);
            column.ColumnName = "PalletWeight_Reserved";
            column.Caption = "Pallet Weight Reserved";
            column.DefaultValue = 0.0M;
            YarnAvailable.Columns.Add(column);

            //============================
            //Col 6 
            //=====================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "PalletCones_Reserved";
            column.Caption = "Pallet Cones Reserved";
            column.DefaultValue = 0;
            YarnAvailable.Columns.Add(column);

            BindingSrc.DataSource = YarnAvailable;
            dataGridView1.DataSource = BindingSrc;

            /*
            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.Name = "GridView0";
            oTxtA.HeaderText = "Key";
            oTxtA.DataPropertyName = YarnAvailable.Columns[0].ColumnName;
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            dataGridView1.Columns.Add(oTxtA);    //0 Pallet No Key 
            dataGridView1.Columns["GridView0"].DisplayIndex = 0;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.Name = "GridView1";
            oChkA.DataPropertyName = YarnAvailable.Columns[1].ColumnName;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);     
            dataGridView1.Columns["GridView1"].DisplayIndex = 1;

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.Name = "GridView2";
            oTxtB.HeaderText = "Pallet Number";
            oTxtB.DataPropertyName = YarnAvailable.Columns[2].ColumnName;
            oTxtB.ValueType = typeof(string);
            oTxtB.ReadOnly = true; 
            oTxtB.Visible = true;
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns["GridView2"].DisplayIndex = 2;

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.Name = "GridView3";
            oTxtC.HeaderText = "Pallet Weight";
            oTxtC.Width = 100;
            oTxtC.DataPropertyName = YarnAvailable.Columns[3].ColumnName;
            oTxtC.ValueType = typeof(decimal);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns["GridView3"].DisplayIndex = 3;

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.Name = "GridView4";
            oTxtD.HeaderText = "Cones Available";
            oTxtD.DataPropertyName = YarnAvailable.Columns[4].ColumnName;
            oTxtD.Width = 100;
            oTxtD.ValueType = typeof(int);
            oTxtD.Visible = true;
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns["GridView4"].DisplayIndex = 4;

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Weight Reserved";
            oTxtE.Name = "GridView5";
            oTxtE.DataPropertyName = YarnAvailable.Columns[5].ColumnName;
            oTxtE.Width = 100;
            oTxtE.ValueType = typeof(decimal);
            oTxtE.Visible = true;
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns["GridView5"].DisplayIndex = 5;

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Cones Reserved";
            oTxtF.Name = "GridView6";
            oTxtF.DataPropertyName = YarnAvailable.Columns[6].ColumnName;
            oTxtF.Width = 100;
            oTxtF.ValueType = typeof(int);
            oTxtF.Visible = true;
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns["GridView6"].DisplayIndex = 6;
            */

            var idx = -1;
            foreach (DataColumn col in YarnAvailable.Columns)
            {
                if (++idx == 0)
                    dataGridView1.Columns[idx].Visible = false;
                else
                {
                    dataGridView1.Columns[idx].HeaderText = col.Caption;
                }
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            LastNumberUsed = 0;
            EditMode = false;

            _CurrentOrder = null;

         

            rbComCustNo.Checked = true;

            EditKO = false;
            cmbEditKO.Visible = false;
            btnEdit.Text = "Edit";
            SourceSelected = core.PopulateArray(2, false);
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            using (var context = new TTI2Entities())
            {
                var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                if (LastNumber != null)
                {
                    LastNumberUsed = LastNumber.col4;
                    txtKnitOrder.Text = "KO" + LastNumber.col4.ToString().PadLeft(6, ' ').Replace(' ', '0');

                }

                cmbKnittedFor.DataSource = context.TLADM_CustomerFile.Where(x=>!x.Cust_CommissionCust).OrderBy(x => x.Cust_Description).ToList();
                cmbKnittedFor.ValueMember = "Cust_Pk";
                cmbKnittedFor.DisplayMember = "Cust_Description";
                cmbKnittedFor.SelectedValue = 0;

                cmbKnitted.DataSource = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust).OrderBy(x => x.Cust_Description).ToList();
                cmbKnitted.ValueMember = "Cust_Pk";
                cmbKnitted.DisplayMember = "Cust_Description";
                cmbKnitted.SelectedValue = 0;
                cmbKnitted.Visible = false;

            
                cmbFabricType.DataSource = context.TLADM_Griege.Where(x=>!(bool)x.TLGriege_Discontinued) .OrderBy(x => x.TLGreige_Description).ToList();
                cmbFabricType.ValueMember = "TLGreige_Id";
                cmbFabricType.DisplayMember = "TLGreige_Description";
                cmbFabricType.SelectedValue = 0;

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode == "KNIT").FirstOrDefault();
                if(Dept != null)
                {
                    cmbMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x=>x.MD_MachineCode).ToList();
                    cmbMachines.DisplayMember = "MD_AlternateDesc";
                    cmbMachines.ValueMember = "MD_Pk";
                    cmbMachines.SelectedIndex = -1;

                }

                EditKO = false;
                cmbEditKO.Visible = false;
                btnEdit.Text = "Edit";

                txtOrderKG.KeyDown  += core.txtWin_KeyDownOEM;
                txtOrderKG.KeyPress += core.txtWin_KeyPress;

       

                richTextBox1.Text = string.Empty;

                txtOrderPieces.KeyDown  += core.txtWin_KeyDown;
                txtOrderPieces.KeyPress += core.txtWin_KeyPress;

                txtYLTSetting.KeyDown += core.txtWin_KeyDownOEM;
                txtYLTSetting.KeyPress += core.txtWin_KeyPress;

                txtYLTSetting.Text= "0.00";
                txtFabricWidth.Text = string.Empty;
                txtGreigeWeight.Text = String.Empty;
                txtOrderKG.Text = "0.00";
                txtOrderPieces.Text = "0";

                dtpDateRequired.Value = DateTime.Now;
                dtpKnitOrderDate.Value = DateTime.Now;

                btnConfirm.Enabled = false;
                cmboColours.Enabled = false;
                cmboSizes.Enabled = false;

                formloaded = true;
            }
        }

        private void cmbFabricType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            Util Core = new Util();
            IList<TLKNI_YarnOrderPallets> PalletStack = new List<TLKNI_YarnOrderPallets>();
            IList<TLADM_Colours> PermissableCols = new List<TLADM_Colours>();

            if (oCmbo != null && formloaded)
            {
                // dataGridView1.Rows.Clear();
                YarnAvailable.Rows.Clear();

                cmboColours.SelectedValue = -1;
                cmboColours.Enabled = false;

                if (!btnSave.Enabled)
                    btnSave.Enabled = true;

                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }


                var Greige = (TLADM_Griege)oCmbo.SelectedItem;
                if (Greige != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        if (Greige.TLGreige_IsBoughtIn)
                        {
                            var Trims = (from T1 in context.TLADM_Griege
                                    join T2 in context.TLADM_Trims on T1.TLGreige_Id equals T2.TR_Greige_FK
                                    where T1.TLGreige_Id == Greige.TLGreige_Id
                                    select T2).ToList();
                        
                            if(Trims.Count() == 0)
                            {
                                MessageBox.Show("This Fabric has been designated as Bought In Fabric with no trims designated as such");
                                return;
                            }

                            foreach (var Trim in Trims)
                            {
                                if (Trim.TR_IsSizes)
                                {
                                    var Sz = context.TLADM_Sizes.Find((int)Trim.TR_Size_FK);
                                    if (Sz != null)
                                        cmboSizes.Items.Add(Sz);
                                }
                            }
                                
                            var Colours = context.TLADM_GreigeColour.Where(x => x.Grcl_Greige_FK == Greige.TLGreige_Id).ToList();
                            foreach (var Colour in Colours)
                            {
                                var Cl = context.TLADM_Colours.Find(Colour.Grlc_Colour_FK);
                                if (Cl != null)
                                {
                                    PermissableCols.Add(Cl);
                                }
                            }
                            if (PermissableCols.Count == 0)
                            {
                                btnSave.Enabled = false;
 
                                MessageBox.Show("This particular item has no associated colours set");
                                return;
                            }
                            cmboColours.DataSource = PermissableCols;
                            cmboColours.ValueMember = "Col_Id";
                            cmboColours.DisplayMember = "Col_Display";
                            cmboColours.SelectedValue = -1;
                            cmboColours.Enabled = true;
                            
                            cmboSizes.ValueMember = "SI_Id";
                            cmboSizes.DisplayMember = "SI_Description";
                            cmboSizes.SelectedValue = -1;
                            cmboSizes.Enabled = true;
                            
                        }
                        var fabweight = context.TLADM_FabricWeight.Where(x => x.FWW_Id == Greige.TLGreige_FabricWeight_FK).FirstOrDefault();
                        if (fabweight != null)
                            txtGreigeWeight.Text = fabweight.FWW_Description;
                        var fabwidth = context.TLADM_FabWidth.Where(x => x.FW_Id == Greige.TLGreige_FabricWidth_FK).FirstOrDefault();
                        if (fabwidth != null)
                            txtFabricWidth.Text = fabwidth.FW_Description;

                        //NB WAIT CURSOR
                        //*AS 20230914 -- if PN filter list, else give whole list: var YarnDet = context.TLADM_Yarn.ToList();
                        //*AS 20240208 -- var GreigePN now obsolete!
                        var GreigePN = Core.ExtrapNumber(Greige.TLGreige_YarnPowerN, context.TLADM_Yarn.Count()).ToList();

                        //if (GreigePN.Count > 0)
                        //{
                        //      var YarnDet = GreigePN.Any() ? GreigePN.SelectMany(Number => context.TLADM_Yarn.Where(x => x.YA_PowerN == Number)).ToList() : context.TLADM_Yarn.ToList();
                        //} 
                        //foreach (var Number in GreigePN)
                        //{
                        //    YarnDet = context.TLADM_Yarn.Where(x => x.YA_PowerN == Number).ToList();
                        //}                    

                        //*AS 20240208 v5.0.0.118 - PalletStack revisited
                        var YarnDet = context.TLADM_Yarn.ToList();
                        int yarnDetTotalCount = YarnDet.Count;
                        //YarnDet = context.TLADM_Greige_Yarn.Where(x => x.TLQual_Greige_Fk == Greige.TLGreige_Id).ToList();
                        var Greige_Yarn = context.TLADM_Greige_Yarn.Where(x => x.TLQual_Greige_Fk == Greige.TLGreige_Id).ToList();
                        YarnDet = Greige_Yarn.SelectMany(item => context.TLADM_Yarn.Where(y => y.YA_Id == item.TLQual_Yarn_Fk)).ToList();
                        
                        if (YarnDet.Count == yarnDetTotalCount)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("No yarn is allocated to " + Greige.TLGreige_Description + "!" + Environment.NewLine + "Please take special care when selecting pallets from the available list.");
                            }
                        }
                        //**AS 20240208

                        if (YarnDet != null)
                           {
                                foreach (var YDet in YarnDet)
                                {
                                    if (!rbComCustYes.Checked)
                                    {
                                        if (!EditKO)
                                        {
                                            if (rbOwnYarn.Checked)
                                                PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YDet.YA_Id && x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                            else
                                                PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YDet.YA_Id && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                        }
                                        else
                                        {
                                            if (rbOwnYarn.Checked)
                                                PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YDet.YA_Id && x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                            else
                                                PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YDet.YA_Id && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                        }
                                    }
                                    else
                                    {
                                        PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YDet.YA_Id && !x.TLKNIOP_OwnYarn && x.TLKNIOP_CommisionCust && !x.TLKNIOP_PalletAllocated).ToList();
                                    }

                                    if (PalletStack.Count == 0)
                                    {
                                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                        {
                                            MessageBox.Show("There are no pallets conforming to the " + YDet.YA_Description + Environment.NewLine + "specifications as selected!");
                                        }
                                        //continue;
                                    }

                                    foreach (var Pallet in PalletStack)
                                    {
                                        DataRow YarnPallet = YarnAvailable.NewRow();

                                        if (rbComCustNo.Checked)
                                        {
                                            var YOrder = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK);
                                            if (YOrder != null)
                                            {
                                                YarnPallet[0] = Pallet.TLKNIOP_Pk;
                                                YarnPallet[1] = false;

                                                if (EditKO)
                                                {
                                                    var YarnAlloc = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == _CurrentOrder.KnitO_Pk && x.TLKYT_YOP_FK == Pallet.TLKNIOP_Pk).FirstOrDefault();
                                                    if (YarnAlloc != null)
                                                    {
                                                        YarnPallet[1] = true;

                                                    }
                                                }

                                                YarnPallet[2] = YOrder.YarnO_OrderNumber + "-" + Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                                YarnPallet[3] = Math.Round(core.CalculatePalletNett(Pallet), 2);
                                                YarnPallet[4] = Pallet.TLKNIOP_Cones + Pallet.TLKNIOP_ConesReturned - Pallet.TLKNIOP_ConesReserved;
                                                YarnPallet[5] = 0.00M;
                                                YarnPallet[6] = 0;

                                            }
                                        }
                                        else
                                        {
                                            YarnPallet[0] = Pallet.TLKNIOP_Pk;
                                            YarnPallet[1] = false;

                                            if (EditMode && Pallet.TLKNIOP_ReservedBy == _CurrentOrder.KnitO_Pk)
                                            {
                                                YarnPallet[1] = true;
                                            }

                                            YarnPallet[2] = Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                            YarnPallet[3] = Math.Round(core.CalculatePalletNett(Pallet), 2);
                                            YarnPallet[4] = Pallet.TLKNIOP_Cones - Pallet.TLKNIOP_ConesReserved;
                                            YarnPallet[5] = 0.00M;
                                            YarnPallet[6] = 0;
                                        }

                                        YarnAvailable.Rows.Add(YarnPallet);
                                    }
                                }
                           }
                        //}

                        var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_GreigeType_FK == Greige.TLGreige_Id).ToList();
                        if (Machines != null)
                        {
                            formloaded = false;
                            cmbMachines.Text = "";
                            cmbMachines.DataSource = null;
                            cmbMachines.DataSource = Machines;
                            cmbMachines.DisplayMember = "MD_AlternateDesc";
                            cmbMachines.ValueMember = "MD_Pk";

                            if (_CurrentOrder != null)
                                cmbMachines.SelectedValue = _CurrentOrder.KnitO_Machine_FK;
                            else
                                cmbMachines.SelectedValue = -1;

                            formloaded = true;
                        }
                                            
                        dataGridView1.Focus();
                    }
                }
            }
        }
        
        private void OrderQty_Changed(object sender, EventArgs e)
        {
            decimal stdWeight = 25.00M;

            TextBox oTxt = sender as TextBox;
            if (oTxt.TextLength > 0)
            {
                var selected =(TLADM_Griege)cmbFabricType.SelectedItem;
                if (selected != null)
                    if (selected.TLGreige_KgPerPiece != 0)
                        stdWeight = selected.TLGreige_KgPerPiece;

                txtOrderPieces.Text = Math.Round(Convert.ToDecimal(oTxt.Text) / stdWeight, 0).ToString();
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if(oTxt.TextLength != 0)
                         MandSelected[nbr] = true;
                    else
                        MandSelected[nbr] = false;                    
                }
            }
           
        }

        private void OrderPieces_Changed(object sender, EventArgs e)
        {
           TextBox oTxt = sender as TextBox;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int LNU = 0;
            bool YarnAssigned = false;

            if (oBtn != null && formloaded)
            {
                var cnt = SourceSelected.Where(x => x == false).Count();
                if (cnt == 0)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select a relevant yarn source");
                    }
                    return;
                }

                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).FirstOrDefault();

                if (SingleRow == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select at least one pallet to process");
                    }
                    return;
                }

                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show(errorM);
                    }
                    return;
                }
              
               
                using (var context = new TTI2Entities())
                {

                    LNU = Convert.ToInt32(txtKnitOrder.Text.Replace("KO", "00"));
                    
                    TLKNI_Order KnitOrder = new TLKNI_Order();
                    if (EditMode)
                    {
                        var KnitO = (TLKNI_Order)cmbEditKO.SelectedItem;
                        if (KnitO != null)
                        {
                            KnitOrder = context.TLKNI_Order.Find(KnitO.KnitO_Pk);
                            LNU = KnitO.KnitO_OrderNumber;
                        }
                    }
                   

                    var ft = (TLADM_Griege)cmbFabricType.SelectedItem;
                    if (ft != null)
                    {
                        KnitOrder.KnitO_Product_FK = ft.TLGreige_Id;
                        if (ft.TLGreige_IsBoughtIn)
                        {
                            var ClrSelected = (TLADM_Colours)cmboColours.SelectedItem;
                            if (ClrSelected == null)
                            {
                                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                {
                                    MessageBox.Show("Please select a colour appropriate to this order");
                                    return;
                                }
                            }

                            var SizeSelected = (TLADM_Sizes)cmboSizes.SelectedItem;
                            if (SizeSelected == null)
                            {
                                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                {
                                    MessageBox.Show("Please select a size appropriate to this order");
                                }
                                return;
                            }
                        }
                    }

                    var MachDet = (TLADM_MachineDefinitions)cmbMachines.SelectedItem;
                    if(MachDet != null)
                        KnitOrder.KnitO_Machine_FK = MachDet.MD_Pk;

                    if (rbComCustNo.Checked)
                    {
                        var CustDet = (TLADM_CustomerFile)cmbKnittedFor.SelectedItem;
                        if (CustDet != null)
                            KnitOrder.KnitO_Customer_FK = CustDet.Cust_Pk;
                    }
                    else
                    {
                        var CustDet = (TLADM_CustomerFile)cmbKnitted.SelectedItem;
                        if (CustDet != null)
                            KnitOrder.KnitO_Customer_FK = CustDet.Cust_Pk;
                    }

                    KnitOrder.KnitO_OrderNumber = LNU;
                    KnitOrder.KnitO_DeliveryDate = dtpDateRequired.Value;
                    KnitOrder.KnitO_OrderDate = dtpKnitOrderDate.Value;
                    KnitOrder.KnitO_OrderConfirmed = false;
                    KnitOrder.KnitO_YLTSetting = Convert.ToDecimal(txtYLTSetting.Text);

                    if (rbComCustYes.Checked)
                        KnitOrder.KnitO_CommisionCust = true;

                    if (txtOrderPieces.TextLength != 0)
                        KnitOrder.KnitO_NoOfPieces = Convert.ToInt32(txtOrderPieces.Text);
                    else
                        KnitOrder.KnitO_NoOfPieces = 0;

                    if (txtOrderKG.TextLength > 0)
                        KnitOrder.KnitO_Weight = Convert.ToDecimal(txtOrderKG.Text);
                    else
                        KnitOrder.KnitO_Weight = 0.00M;

                    KnitOrder.KnitO_Notes = richTextBox1.Text;

                    if (cmboColours.Enabled)
                    {
                        var ClrSelected = (TLADM_Colours)cmboColours.SelectedItem;
                        if (ClrSelected != null)
                            KnitOrder.KnitO_Colour_Fk = ClrSelected.Col_Id;


                        var SizeSelected =  (TLADM_Sizes)cmboSizes.SelectedItem;
                        if(SizeSelected != null)
                            KnitOrder.KnitO_Size_Fk = SizeSelected.SI_id;

                    }
                    //---------------------------------------------------------------
                    if (!EditMode)
                    {
                        var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                        if (LastNumber != null)
                            LastNumber.col4 += 1;

                        context.TLKNI_Order.Add(KnitOrder);

                    }

                    try
                    {
                        context.SaveChanges();
                        foreach (DataRow Row in YarnAvailable.Rows)
                        {
                            if (!Row.Field<bool>(1))
                                continue;

                            var Pk = Row.Field<int>(0);
                            if (Pk > 0)
                            {
                                var Pallet = context.TLKNI_YarnOrderPallets.Find(Pk);
                                if (Pallet != null)
                                {
                                    TLKNI_YarnAllocTransctions AllocTrans = new TLKNI_YarnAllocTransctions();
                                    AllocTrans.TLKYT_TransDate = dtpKnitOrderDate.Value;
                                    AllocTrans.TLKYT_KnitOrder_FK = KnitOrder.KnitO_Pk;
                                    AllocTrans.TLKYT_TranType = 1;
                                    var NettWeight = Row.Field<Decimal>(5);
                                    var Cones = Row.Field<int>(6);

                                    AllocTrans.TLKYT_NettWeight = NettWeight;
                                    AllocTrans.TLKYT_NoOfCones = Cones;

                                    AllocTrans.TLKYT_NoOfCones = Row.Field<int>(6);
                                    AllocTrans.TLKYT_YOP_FK = Pallet.TLKNIOP_Pk;

                                    context.TLKNI_YarnAllocTransctions.Add(AllocTrans);

                                    Pallet.TLKNIOP_ConesReserved += Cones;
                                    Pallet.TLKNIOP_NettWeightReserved += NettWeight;

                                    KnitOrder.KnitO_YarnO_FK = Pallet.TLKNIOP_YarnOrder_FK;

                                    if (core.CalculatePalletNett(Pallet) <= 0.00M)
                                        Pallet.TLKNIOP_PalletAllocated = true;

                                    YarnAssigned = true;
                                }
                            }
                        }

                        if (YarnAssigned)
                            KnitOrder.KnitO_YarnAssigned = true;

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to the database");
                            DialogResult res = MessageBox.Show("Would you like to print this Knit Order", "Inquiry", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                frmKnitViewRep vRep = new frmKnitViewRep(7, KnitOrder.KnitO_Pk);
                                int h = Screen.PrimaryScreen.WorkingArea.Height;
                                int w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            SetUp();
                        }
                   
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                
                }
                
            }
        }

        private void cmbExistingKO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = new ComboBox();
            if (oCmbo != null && formloaded)
            {
                SourceSelected[0] = true;
            }
        }

        private void dtpKnitOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
               
                }
            }
        }

        private void cmbKnittedFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

                var Selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if (Selected != null)
                {
                   
                }

                
            }
        }

        private void YLT_Changed(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {

                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                if (!EditKO)
                {
                    EditKO = !EditKO;
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmbEditKO.DataSource = context.TLKNI_Order.Where(x => !x.KnitO_OrderConfirmed && !x.KnitO_Closed).OrderBy(x => x.KnitO_OrderNumber).ToList();
                        cmbEditKO.ValueMember = "KnitO_Pk";
                        cmbEditKO.DisplayMember = "KnitO_OrderNumber";
                        formloaded = true;
                    }
                    cmbEditKO.Location = new Point(176, 17);
                    cmbEditKO.Visible = true;
                    txtKnitOrder.Visible = false;
                    btnEdit.Text = "Cancel Edit";
                }
                else
                {
                    rbComCustNo.Enabled = true;
                    rbComCustYes.Enabled = true;
                    EditKO = !EditKO;
                    cmbEditKO.Visible = false;
                    txtKnitOrder.Visible = true;
                    btnEdit.Text = "Edit";
                    SetUp();
                }
            }
        }

        private void cmbEditKO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var knitO = (TLKNI_Order)cmbEditKO.SelectedItem;
                if (knitO != null)
                {
                    _CurrentOrder = knitO;

                    dtpKnitOrderDate.Value = knitO.KnitO_OrderDate;
                    dtpDateRequired.Value = knitO.KnitO_DeliveryDate;

                    using (var context = new TTI2Entities())
                    {
                        var Customer = context.TLADM_CustomerFile.Find(knitO.KnitO_Customer_FK);
                        if(Customer != null && Customer.Cust_CommissionCust)
                        {
                            cmbKnitted.SelectedValue = knitO.KnitO_Customer_FK;

                            rbComCustYes.PerformClick();
                            rbComCustYes.Enabled = false;
                            rbComCustNo.Enabled = false;
                        }
                        else
                        {
                            cmbKnittedFor.SelectedValue = knitO.KnitO_Customer_FK;
                        }
                    }

                    txtOrderKG.Text = Math.Round(knitO.KnitO_Weight,2).ToString();
                    txtOrderPieces.Text = knitO.KnitO_NoOfPieces.ToString();
                    
                    cmbMachines.SelectedValue = knitO.KnitO_Machine_FK;
                    //_knittingMachKey = knitO.KnitO_Machine_FK;
                    richTextBox1.Text = knitO.KnitO_Notes;
                    txtYLTSetting.Text = knitO.KnitO_YLTSetting.ToString();
                    cmbFabricType.SelectedValue = knitO.KnitO_Product_FK;
                    

                    EditMode = true;

                    btnConfirm.Enabled = true;
                }
            }
        }

        private void rbComCustYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                cmbKnitted.Visible = true;
                cmbKnitted.Location = new Point(125, 24);
                cmbKnittedFor.Visible = false;

            }
            else
            {
                cmbKnitted.Visible = false;
                cmbKnittedFor.Visible = true;
            }
        }

        private void cmbMachDet_IndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var cmb = (TLKNI_Order)cmbEditKO.SelectedItem;
                if (cmb != null)
                {
                    if (cmb.KnitO_OrderConfirmed)
                    {
                        MessageBox.Show("This order has already been confirmed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult res = MessageBox.Show("You are about to confirm this order!" + Environment.NewLine + "No more amendements to this order will be permitted", " Warning ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLKNI_Order.Find(cmb.KnitO_Pk);
                            if (Existing != null)
                            {
                                Existing.KnitO_OrderConfirmed = true;
                                Existing.KnitO_OrderConfirmedDate = DateTime.Now;

                                try
                                {
                                    context.SaveChanges();
                                    SetUp();
                                    MessageBox.Show("Data updated successfully to database");
                                    try
                                    {
                                        frmGreigeProduction greigeProd = new frmGreigeProduction(Existing);
                                        greigeProd.ShowDialog(this);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
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
        }
        
        private void rbComCustNo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                cmbKnitted.Visible = false;
                cmbKnitted.Location = new Point(125, 24);
                cmbKnittedFor.Visible = true;
            }
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            IList<TLADM_Yarn> yarnTypes = new List<TLADM_Yarn>();
            if (oBtn != null)
            {
                var Greige = (TLADM_Griege)cmbFabricType.SelectedItem;
                if (Greige != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var GreigePN = core.ExtrapNumber(Greige.TLGreige_YarnPowerN, context.TLADM_Yarn.Count()).ToList();
                        foreach (var Number in GreigePN)
                        {
                            var YarnDet = context.TLADM_Yarn.Where(x => x.YA_PowerN == Number).FirstOrDefault();
                            if (YarnDet != null)
                            {
                                yarnTypes.Add(YarnDet);
                            }
                        }
                    }

                    var CustDetails = (TLADM_CustomerFile)cmbKnittedFor.SelectedItem;
                    if (CustDetails == null)
                    {
                        CustDetails = (TLADM_CustomerFile)cmbKnitted.SelectedItem;
                    }

                    if (EditMode)
                    {
                        var EditValue = (TLKNI_Order)cmbEditKO.SelectedItem;
                        LastNumberUsed = EditValue.KnitO_OrderNumber;
                    }

                    frmYarnDetails yarnDet = new frmYarnDetails(yarnTypes, CustDetails, LastNumberUsed);
                    yarnDet.ShowDialog(this);
                    
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
               

                if (EditMode)
                {
                    var EditValue = (TLKNI_Order)cmbEditKO.SelectedItem;
                    var PrimaryKey = EditValue.KnitO_Pk;

                    frmKnitViewRep vRep = new frmKnitViewRep(7, PrimaryKey);
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
                else
                {
                    MessageBox.Show("Must be in EDIT Mode to print an order");   
                }
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (isChecked)
                {
                   var CurrentRow = oDgv.CurrentRow;

                   CurrentRow.Cells[5].Value = (decimal)CurrentRow.Cells[3].Value;
                   CurrentRow.Cells[6].Value = (int)CurrentRow.Cells[4].Value;

                   var CurrentVal = Convert.ToDecimal(txtOrderKG.Text);
                   //txtOrderKG.Text = (CurrentVal + (decimal)CurrentRow.Cells[5].Value).ToString();
                }
                else
                {
                    var CurrentRow = oDgv.CurrentRow;
                    var CurrentVal = Convert.ToDecimal(txtOrderKG.Text);
                                      
                    // txtOrderKG.Text = (CurrentVal - (decimal)CurrentRow.Cells[5].Value).ToString();
                }
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (e.ColumnIndex == 5)
            {
                decimal TotalSummed = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                       select Rows).Where(x => (bool)x.Cells[1].Value == true).Sum(x => (decimal?)x.Cells[5].Value ?? 0.00M);

                txtOrderKG.Text = TotalSummed.ToString();
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 5)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 6)
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

        private void rbOwnYarn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            Util Core = new Util();
            IList<TLKNI_YarnOrderPallets> PalletStack = new List<TLKNI_YarnOrderPallets>();

            if (oRad != null)
            {
                var Greige = (TLADM_Griege)cmbFabricType.SelectedItem;
                if (Greige != null)
                {
                    // dataGridView1.Rows.Clear();
                    YarnAvailable.Rows.Clear();
                    txtOrderKG.Text = "0";
                    txtOrderPieces.Text = "0";
                    using (var context = new TTI2Entities())
                    {
                        var fabweight = context.TLADM_FabricWeight.Where(x => x.FWW_Id == Greige.TLGreige_FabricWeight_FK).FirstOrDefault();
                        if (fabweight != null)
                            txtGreigeWeight.Text = fabweight.FWW_Description;
                        var fabwidth = context.TLADM_FabWidth.Where(x => x.FW_Id == Greige.TLGreige_FabricWidth_FK).FirstOrDefault();
                        if (fabwidth != null)
                            txtFabricWidth.Text = fabwidth.FW_Description;
                        var GreigePN = Core.ExtrapNumber(Greige.TLGreige_YarnPowerN, context.TLADM_Yarn.Count()).ToList();
                        foreach (var Number in GreigePN)
                        {
                             var YarnDet = context.TLADM_Yarn.Where(x => x.YA_PowerN == Number).FirstOrDefault();
                             if (YarnDet != null)
                             {
                                 if (!rbComCustYes.Checked)
                                 {
                                     if (!EditMode)
                                     {
                                         if (rbOwnYarn.Checked)
                                             PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                         else
                                             PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                     }
                                     else
                                     {
                                         if (rbOwnYarn.Checked)
                                             PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated || (x.TLKNIOP_ReservedBy == _CurrentOrder.KnitO_Pk)).ToList();
                                         else
                                             PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated || (x.TLKNIOP_ReservedBy == _CurrentOrder.KnitO_Pk)).ToList();
                                     }
                                 }
                                 else
                                 {
                                     PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && !x.TLKNIOP_OwnYarn && x.TLKNIOP_CommisionCust && !x.TLKNIOP_PalletAllocated).ToList();
                                 }

                                
                                 foreach (var Pallet in PalletStack)
                                 {
                                    DataRow YarnPallet = YarnAvailable.NewRow();

                                     if (rbComCustNo.Checked)
                                     {
                                         var YOrder = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK);
                                         if (YOrder != null)
                                         {
                                             YarnPallet[0] = Pallet.TLKNIOP_Pk;
                                             YarnPallet[1] = false;
                                            
                                             YarnPallet[2] = YOrder.YarnO_OrderNumber + "-" + Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                             YarnPallet[3] = Math.Round(core.CalculatePalletNett(Pallet), 2);
                                             YarnPallet[4] = Pallet.TLKNIOP_Cones + Pallet.TLKNIOP_ConesReturned - Pallet.TLKNIOP_ConesReserved;
                                             YarnPallet[5] = 0.00M;
                                             YarnPallet[6] = 0;

                                         }
                                     }
                                     else
                                     {
                                         YarnPallet[0] = Pallet.TLKNIOP_Pk;
                                         YarnPallet[1] = false;

                                         var AllocTrans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == _CurrentOrder.KnitO_Pk && x.TLKYT_YOP_FK == Pallet.TLKNIOP_Pk).FirstOrDefault();
                                         if (AllocTrans != null)
                                         {
                                             YarnPallet[1] = true;
                                         }

                                         YarnPallet[2] = Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                         Decimal Value = Pallet.TLKNIOP_NettWeight + Pallet.TLKNIOP_AdditionalYarn + Pallet.TLKNIOP_NettWeightReturned;
                                         YarnPallet[3] = Math.Round(Value - Pallet.TLKNIOP_NettWeightReserved, 2);
                                         YarnPallet[4] = Pallet.TLKNIOP_Cones - Pallet.TLKNIOP_ConesReserved;
                                         YarnPallet[5] = 0.00M;
                                         YarnPallet[6] = 0;
                                     }

                                    YarnAvailable.Rows.Add(YarnPallet);
                                 }
                                 
                             }
                        }
                    }
                }
            }
        }

        private void rbThirdParty_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            Util Core = new Util();
            IList<TLKNI_YarnOrderPallets> PalletStack = new List<TLKNI_YarnOrderPallets>();

            if (oRad != null)
            {
                var Greige = (TLADM_Griege)cmbFabricType.SelectedItem;
                if (Greige != null)
                {
                    // dataGridView1.Rows.Clear();

                    YarnAvailable.Rows.Clear();
                    txtOrderKG.Text = "0";
                    txtOrderPieces.Text = "0";

                    using (var context = new TTI2Entities())
                    {
                        var fabweight = context.TLADM_FabricWeight.Where(x => x.FWW_Id == Greige.TLGreige_FabricWeight_FK).FirstOrDefault();
                        if (fabweight != null)
                            txtGreigeWeight.Text = fabweight.FWW_Description;
                        var fabwidth = context.TLADM_FabWidth.Where(x => x.FW_Id == Greige.TLGreige_FabricWidth_FK).FirstOrDefault();
                        if (fabwidth != null)
                            txtFabricWidth.Text = fabwidth.FW_Description;

                        var GreigePN = Core.ExtrapNumber(Greige.TLGreige_YarnPowerN, context.TLADM_Yarn.Count()).ToList();
                        foreach (var Number in GreigePN)
                        {
                            var YarnDet = context.TLADM_Yarn.Where(x => x.YA_PowerN == Number).FirstOrDefault();
                            if (YarnDet != null)
                            {
                               PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && !x.TLKNIOP_OwnYarn && !x.TLKNIOP_PalletAllocated).ToList();
                                
                                foreach (var Pallet in PalletStack)
                                {
                                    DataRow YarnPallet = YarnAvailable.NewRow();

                                    var YOrder = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK);
                               
                                    YarnPallet[0] = Pallet.TLKNIOP_Pk;
                                    YarnPallet[1] = false;
                                    YarnPallet[2] = Pallet.TLKNIOP_PalletNo.ToString().PadLeft(2, '0');
                                    Decimal Value = Pallet.TLKNIOP_NettWeight + Pallet.TLKNIOP_AdditionalYarn + Pallet.TLKNIOP_NettWeightReturned;
                                    YarnPallet[3] = Math.Round(Value - Pallet.TLKNIOP_NettWeightReserved, 2);
                                    YarnPallet[4] = Pallet.TLKNIOP_Cones - Pallet.TLKNIOP_ConesReserved;
                                    YarnPallet[5] = 0.00M;
                                    YarnPallet[6] = 0;

                                    YarnAvailable.Rows.Add(YarnPallet);
                                }
                            }
                        }
                    }
                }
            }
        }

        

    }

    class KnitOrderSel
    {
        public KnitOrderSel()
        {
        }

        public string GProduct { get; set; }
        public decimal Weight { get; set; }
        public int NoOfPieces { get; set; }
        public int KnitOrder { get; set; }
        public string MachineCode { get; set; }
        public decimal YLTSetting { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Customer { get; set; } 

    }
}
