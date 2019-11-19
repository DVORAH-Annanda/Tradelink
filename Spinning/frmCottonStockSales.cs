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

namespace Spinning
{

    public partial class frmCottonStockSales : Form
    {
        bool formloaded;

        DataGridViewCheckBoxColumn oChk;
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        string[][] MandatoryFields;
        bool[] MandSelected;
        bool[] RowsSelected;
        bool _EditMode;
  
        Util core;

        public frmCottonStockSales()
        {
            InitializeComponent();
            rtbCustomerAddress.ReadOnly = true;

            core = new Util();

            txtGrossWeightDeliv.KeyDown += core.txtWin_KeyDownOEM;
            txtGrossWeightDeliv.KeyPress += core.txtWin_KeyPress;
            
            txtWeighBridgeEmpty.KeyPress += core.txtWin_KeyPress;
            txtWeighBridgeEmpty.KeyDown += core.txtWin_KeyDown;

            txtWeighBridgeFull.KeyPress += core.txtWin_KeyPress;
            txtWeighBridgeFull.KeyDown += core.txtWin_KeyDown;

            MandatoryFields = new string[][]
                {   new string[] {"cmbCustomerName", "Please select a customer", "0"},
                    new string[] {"txtCustOrderNo", "Please enter a customer order no", "1"},
                    new string[] {"cmbContractNo", "Please enter a contract number", "2"},
                    new string[] {"cmbLotNo", "Please select a Lot No", "3"}, 
                    new string[] {"comboTransporter", "Please select a transporter", "4"},
                    new string[] {"txtVehReg", "Please enter a vehicle registration", "5"},
                    new string[] {"txtWeighBridgeFull", "Please complete the weigh bridge full field", "6"},
                    new string[] {"txtWeighBridgeEmpty", "Please complete the weigh bridge empty", "7"}
                };

            Setup();
        }

        void Setup()
        {
            formloaded = false;
            using ( var context = new TTI2Entities())
            {
               rtbCustomerAddress.Text = string.Empty;

                var LastNumber  = context.TLADM_LastNumberUsed.Find(1);
                if(LastNumber != null)
                {
                    txtDeliveryNo.Text = LastNumber.col4.ToString();
                }

                cmbCustomerName.DataSource = context.TLADM_CustomerFile.OrderBy(x=>x.Cust_Description).ToList();
                cmbCustomerName.DisplayMember = "Cust_Description";
                cmbCustomerName.ValueMember = "Cust_Pk";
                cmbCustomerName.Focus();

                cmbContractNo.DataSource = context.TLADM_CottonContracts.OrderBy(x => x.CottonCon_Description).ToList();
                cmbContractNo.ValueMember = "CottonCon_Pk";
                cmbContractNo.DisplayMember = "CottonCon_No";

                comboTransporter.DataSource = context.TLADM_CottonHauliers.OrderBy(x => x.Haul_Description).ToList();
                comboTransporter.ValueMember = "Haul_Pk";
                comboTransporter.DisplayMember = "Haul_Description";
                comboTransporter.SelectedValue = 0;

                
            }

            dataGridView1.Rows.Clear();

            oChk = new DataGridViewCheckBoxColumn();
            oChk.HeaderText = "Select";
            oChk.ValueType = typeof(Boolean);
 
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Bale No";
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Mic";
            oTxtBoxC.ValueType = typeof(Decimal);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Kgs (NETT)";
            oTxtBoxD.ValueType = typeof(Decimal);
            oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Staple";
            oTxtBoxE.ValueType = typeof(Decimal);
            oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.HeaderText = "Kgs (GROSS)";
            oTxtBoxF.ValueType = typeof(Decimal);
            oTxtBoxF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxF.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtBoxA);    // 0 
            dataGridView1.Columns.Add(oChk);        // 1 Selected
            dataGridView1.Columns.Add(oTxtBoxB);    // 2 Bales Numeric 
            dataGridView1.Columns.Add(oTxtBoxC);    // 3 MIC Decimal
            dataGridView1.Columns.Add(oTxtBoxD);    // 4 kgs Decimal 
            dataGridView1.Columns.Add(oTxtBoxE);    // 5 Staple Decimal
            dataGridView1.Columns.Add(oTxtBoxF);    // 6 kgs (GROSS) Decimal
            dataGridView1.CellEndEdit += this.dataGridView1_CellEndEdit;

            txtGrossWeightDeliv.Text = "0.00";
            txtNettWeightDel.Text = "0.00";
            txtVehReg.Text = string.Empty;
            txtWeighBridgeEmpty.Text = "0.0";
            txtWeighBridgeFull.Text = "0.0";

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            cmbPrevious.Visible = false;

            btnEditMode.Visible = false;

            btnSave.Enabled = false;
            btnPickList.Enabled = false;
            formloaded = true;
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var Cell = oDgv.CurrentCell;

                if (Cell.FormattedValue.ToString() == bool.TrueString && !RowsSelected[Cell.RowIndex])
                {
                    var Total = Convert.ToDecimal(txtGrossWeightDeliv.Text);
                    txtGrossWeightDeliv.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                    Total = Convert.ToDecimal(txtNettWeightDel.Text);
                    txtNettWeightDel.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                    RowsSelected[Cell.RowIndex] = true;
                }
                else if (Cell.FormattedValue.ToString() == bool.FalseString && RowsSelected[Cell.RowIndex])
                {
                    var Total = Convert.ToDecimal(txtGrossWeightDeliv.Text);
                    txtGrossWeightDeliv.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                    Total = Convert.ToDecimal(txtNettWeightDel.Text);
                    txtNettWeightDel.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                    RowsSelected[Cell.RowIndex] = false;
                }

            }
        }
       
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selectedRecord = (TLADM_CustomerFile)cmbCustomerName.SelectedItem;
                if (selectedRecord != null)
                {
                    rtbCustomerAddress.Text = selectedRecord.Cust_Address1;
                    
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
        }

        private void cmbContractNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                cmbLotNo.SelectedValue = 0;
                using (var context = new TTI2Entities())
                {
                    var selectedRecord = (TLADM_CottonContracts)oCmbo.SelectedItem;
                    if(selectedRecord != null)
                    {
                        IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();
                        var existing = context.SelectCottonRecords1(selectedRecord.CottonCon_Pk, selectedRecord.CottonCon_ConSupplier_FK).ToList();
                        foreach (var row in existing)
                        {
                            TLSPN_CottonTransactions cr = new TLSPN_CottonTransactions();
                            cr.cotrx_ContractNo_Fk = row.cotrx_ContractNo_Fk;
                            cr.cotrx_Customer_FK = row.cotrx_Customer_FK;
                            cr.cotrx_GrossAveBaleWeight = row.cotrx_GrossAveBaleWeight;
                            cr.cotrx_GrossWeight = row.cotrx_GrossWeight;
                            cr.cotrx_LotNo = row.cotrx_LotNo;
                            cr.cotrx_NettPerWB = row.cotrx_NettPerWB;
                            cr.cotrx_NetWeight = row.cotrx_NetWeight;
                            cr.cotrx_NoBales = row.cotrx_NoBales;
                            cr.cotrx_Notes = row.cotrx_Notes;
                            cr.cotrx_pk = row.cotrx_pk;
                            cr.cotrx_Return_No = row.cotrx_Return_No;
                            cr.cotrx_Supplier_FK = row.cotrx_Supplier_FK;
                            cr.cotrx_TransDate = row.cotrx_TransDate;
                            cr.cotrx_TranType = row.cotrx_TranType;
                            cr.cotrx_VehReg = row.cotrx_VehReg;
                            cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                            cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                            cr.cotrx_WriteOff = row.cotrx_WriteOff;
                            cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                            cotreceived.Add(cr);
                        }

                        cmbLotNo.DataSource = cotreceived.ToList();

                        // cmbLotNo.DataSource    = context.TLSPN_CottonReceived.Where(x => x.CotRec_Contract_FK == selectedRecord.CottonCon_Pk).ToList();
                        cmbLotNo.ValueMember   = "Cotrx_LotNo";
                        cmbLotNo.DisplayMember = "Cotrxc_LotNo";

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
            }
        }

        private void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null & formloaded)
            {
                var selectedRecord = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (selectedRecord != null)
                {
                    btnPickList.Enabled = true;
                    dataGridView1.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo && !x.CoBales_CottonSold && !x.CoBales_IssuedToProd && !x.CoBales_CottonReturned).ToList();
                        foreach (var row in ExistingData)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                            dataGridView1.Rows[index].Cells[2].Value = row.CotBales_BaleNo;
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.CotBales_Mic, 2);
                            dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Staple, 2);
                            dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                        }
                        
                        if (ExistingData.Count > 0)
                        {
                            RowsSelected = core.PopulateArray(dataGridView1.Rows.Count, false);
                            btnSave.Enabled = true;

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
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
           
            if (oBtn != null && formloaded)
            {

                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var cnt = RowsSelected.Where(x => x == false).Count();
                if (cnt == RowsSelected.Count())
                {
                    MessageBox.Show("Please select at least one row from the grid as shown");
                    return;
                }

                var CottonRec = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                var ContractDetails = (TLADM_CottonContracts)cmbContractNo.SelectedItem;
                var CustomerDetails = (TLADM_CustomerFile)cmbCustomerName.SelectedItem;

                //Dont forget last number used
                //----------------------------------------------
                using (var context = new TTI2Entities())
                {

                    //Hard Coded at the moment 
                    // See Table TLADM_TranactionType for a complete List of the Transaction Type Per Department
                    //--------------------------------------------------------------------------------------------------
                  
                    var SequenceNo = Convert.ToInt32(txtDeliveryNo.Text);
                    var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumber != null)
                    {
                        LastNumber.col4 += 1;
                    }
                     // 0 
                    // 1 Selected
                    // 2 Bales Numeric 
                    // 3 MIC Decimal
                    // 4 kgs Decimal 
                    // 5 Staple Decimal
                    int NoBales = 0;
                    decimal Mass = 0.00M;
                    decimal Gross = 0.00M;
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null || (bool)row.Cells[1].Value == false)
                            continue;
                        
                        TLSPN_CottonReceivedBales bales = context.TLSPN_CottonReceivedBales.Find((int)row.Cells[0].Value);
                        if (bales != null)
                        {
                            bales.CoBales_CottonSold = true;
                            bales.CoBales_CottonSequence = SequenceNo;
                            NoBales += 1;
                            Mass += (decimal)row.Cells[4].Value;
                            Gross += (decimal)row.Cells[6].Value;
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

                    TLSPN_CottonTransactions cotTrans = new TLSPN_CottonTransactions();
                    var haul = (TLADM_CottonHauliers)comboTransporter.SelectedItem;

                    cotTrans.cotrx_TransDate = dtpDateDelivered.Value;
                    cotTrans.cotrx_ContractNo_Fk = ContractDetails.CottonCon_Pk;
                    cotTrans.cotrx_LotNo = Convert.ToInt32(cmbLotNo.SelectedValue);
                    cotTrans.cotrx_NetWeight = Mass;
                    cotTrans.cotrx_GrossWeight =  Mass;
                    cotTrans.cotrx_NoBales = NoBales;
                    cotTrans.cotrx_Haulier_FK = haul.Haul_Pk;
                    cotTrans.cotrx_VehReg = txtVehReg.Text;
                    cotTrans.cotrx_WeighBridgeFull = Convert.ToDecimal(txtWeighBridgeFull.Text);
                    cotTrans.cotrx_WeighBridgeEmpty = Convert.ToDecimal(txtWeighBridgeEmpty.Text) ;
                    cotTrans.cotrx_NettPerWB = cotTrans.cotrx_WeighBridgeFull - cotTrans.cotrx_WeighBridgeEmpty;
                    cotTrans.cottrx_NettAveBaleWeight = Mass / NoBales;
                    cotTrans.cotrx_GrossAveBaleWeight = Gross / NoBales;
                    cotTrans.cotrx_Return_No = SequenceNo;
                    cotTrans.cotrx_Customer_FK = CustomerDetails.Cust_Pk;
                    cotTrans.cotrx_Supplier_FK = ContractDetails.CottonCon_ConSupplier_FK;
                    cotTrans.cotrx_Notes = txtCustOrderNo.Text;
                    cotTrans.cotrx_WriteOff = true;
                    //-------------------------------------------------------------------------
                    // Consult Table TLADM_TranTypes
                    //----------------------------------------------------------------------------
                    var DeptDetails = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    var Trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 300 && x.TrxT_Department_FK == DeptDetails.Dep_Id).FirstOrDefault();
                    if (Trantype != null)
                        cotTrans.cotrx_TranType = Trantype.TrxT_Pk;

                    try
                    {
                        if(!_EditMode)
                            context.TLSPN_CottonTransactions.Add(cotTrans);

                        context.SaveChanges();
                        Setup();
                        frmViewReport vRep = new frmViewReport(5, SequenceNo);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        MessageBox.Show("Data saved to database successfully");
                        btnPickList.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

               }

                btnSave.Enabled = false;
            }
        }

        private void txt_ValueChanged(object sender, EventArgs e)
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

        private void comboTransporter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            if (oCombo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var Transport = (TLADM_CottonHauliers)comboTransporter.SelectedItem;
                    if (Transport != null)
                    {
                        var result = (from u in MandatoryFields
                                      where u[0] == oCombo.Name
                                      select u).FirstOrDefault();
                        if (result != null)
                        {
                            int nbr = Convert.ToInt32(result[2].ToString());
                            MandSelected[nbr] = true;
                        }


                    }
                }
            }
        }

        private void comboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            if (oCombo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCombo.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var oDgv = sender as DataGridView;
             if (oDgv != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 var Cell = oDgv.CurrentCell;
                 if (Cell.EditedFormattedValue.ToString() == bool.TrueString && !RowsSelected[Cell.RowIndex])
                 {
                     var Total = Convert.ToDecimal(txtGrossWeightDeliv.Text);
                     txtGrossWeightDeliv.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                     Total = Convert.ToDecimal(txtNettWeightDel.Text);
                     txtNettWeightDel.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                     RowsSelected[Cell.RowIndex] = true;
                 }
                 else if (Cell.EditedFormattedValue.ToString() == bool.FalseString && RowsSelected[Cell.RowIndex])
                 {
                     var Total = Convert.ToDecimal(txtGrossWeightDeliv.Text);
                     txtGrossWeightDeliv.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                     Total = Convert.ToDecimal(txtNettWeightDel.Text);
                     txtNettWeightDel.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                     RowsSelected[Cell.RowIndex] = false;
                 }
             }
        }

        private void btnPickList_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var LotNo = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                frmViewReport vRep = new frmViewReport(11, LotNo.cotrx_LotNo, dataGridView1);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void btnEditMode_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                _EditMode = !_EditMode;
                formloaded = false;
                DisplayFields();
                formloaded = true;
            }
        }


        private void DisplayFields()
        {
            if (!_EditMode)
            {
                btnEditMode.Text = "Edit";
                txtDeliveryNo.Visible = true;
                cmbPrevious.Visible = false;
                label1.Text = "Delivery Note";

                Setup();
            }
            else
            {
                btnEditMode.Text = "Update";
                txtDeliveryNo.Visible = false;
                label1.Text = "Previous Transactions";
                cmbPrevious.Location = new Point(290, 30);
                cmbPrevious.Visible = true;

                using (var context = new TTI2Entities())
                {
                    var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (Department != null)
                    {
                        var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 300 && x.TrxT_Department_FK == Department.Dep_Id).FirstOrDefault();
                        if (TranType != null)
                        {
                            label2.Text = TranType.TrxT_Description;
                            var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TranType == TranType.TrxT_Pk).ToList();

                            cmbPrevious.DataSource = Existing;
                            cmbPrevious.DisplayMember = "Cotrx_Return_No";
                            cmbPrevious.ValueMember = "Cotrx_Pk";

                            dataGridView1.Rows.Clear();
                        }
                    }
                }

            }
        }

        private void cmbPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null)
            {
                var CTS = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (CTS != null)
                {
                    txtDeliveryNo.Text = CTS.cotrx_Return_No.ToString();
                    txtCustOrderNo.Text = CTS.cotrx_Notes;
                    cmbCustomerName.SelectedValue = CTS.cotrx_Customer_FK;
                    cmbContractNo.SelectedValue = CTS.cotrx_ContractNo_Fk;
                    dtpDateDelivered.Value = CTS.cotrx_TransDate;
                  
                    using ( var context = new TTI2Entities())
                    {
                        var cust = context.TLADM_CustomerFile.Where(x=>x.Cust_Pk == CTS.cotrx_Customer_FK).FirstOrDefault();
                        if(cust != null)
                            rtbCustomerAddress.Text = cust.Cust_Address1;
                        IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();
                        var existing = context.SelectCottonRecords1(CTS.cotrx_ContractNo_Fk, CTS.cotrx_Supplier_FK).ToList();
                        if (existing != null)
                        {
                            foreach (var row in existing)
                            {
                                TLSPN_CottonTransactions cr = new TLSPN_CottonTransactions();
                                cr.cotrx_ContractNo_Fk = row.cotrx_ContractNo_Fk;
                                cr.cotrx_Customer_FK = row.cotrx_Customer_FK;
                                cr.cotrx_GrossAveBaleWeight = row.cotrx_GrossAveBaleWeight;
                                cr.cotrx_GrossWeight = row.cotrx_GrossWeight;
                                cr.cotrx_LotNo = row.cotrx_LotNo;
                                cr.cotrx_NettPerWB = row.cotrx_NettPerWB;
                                cr.cotrx_NetWeight = row.cotrx_NetWeight;
                                cr.cotrx_NoBales = row.cotrx_NoBales;
                                cr.cotrx_Notes = row.cotrx_Notes;
                                cr.cotrx_pk = row.cotrx_pk;
                                cr.cotrx_Return_No = row.cotrx_Return_No;
                                cr.cotrx_Supplier_FK = row.cotrx_Supplier_FK;
                                cr.cotrx_TransDate = row.cotrx_TransDate;
                                cr.cotrx_TranType = row.cotrx_TranType;
                                cr.cotrx_VehReg = row.cotrx_VehReg;
                                cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                                cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                                cr.cotrx_WriteOff = row.cotrx_WriteOff;
                                cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                                cotreceived.Add(cr);
                            }
                            cmbLotNo.DataSource = cotreceived;
                            cmbLotNo.DisplayMember = "Cotrx_LotNo";
                            cmbLotNo.ValueMember = "Cotrx_LotNo";
                        }
                        cmbLotNo.SelectedValue = CTS.cotrx_LotNo;

                    }
                    comboTransporter.SelectedValue = CTS.cotrx_Haulier_FK;
                    txtVehReg.Text = CTS.cotrx_VehReg;
                    txtWeighBridgeFull.Text = Math.Round(CTS.cotrx_WeighBridgeFull, 1).ToString();
                    txtWeighBridgeEmpty.Text = Math.Round(CTS.cotrx_WeighBridgeEmpty, 1).ToString();

                }
            }
        }
    }
}
