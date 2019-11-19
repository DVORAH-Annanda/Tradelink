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
    public partial class frmCottonReturns : Form
    {
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

        bool formloaded;
        Util core;
        public frmCottonReturns()
        {
            InitializeComponent();
            MandatoryFields = new string[][]
                {   new string[] {"cmbSupplierDetails", "Please select the contract details", "0"},
                    new string[] {"cmbContractNo", "Please select a cotton contract", "1"},
                    new string[] {"dtpDateReturned", "Please select a return date", "2"},
                    new string[] {"cmbLotNo", "Please select a Lot No", "3"},
                    new string[] {"cmbTransporter", "Please select a transporter", "4"},
                    new string[] {"txtVehReg", "Please enter a vehicle registration number", "5"},
                    new string[] {"txtWeighBridgeFull", "Please enter a value for the weigh bridge full", "6"},
                    new string[] {"txtWeighBridgeEmpty", "Please enter a value for the weigh bridge empty", "7"}
                };
            core = new Util();
            SetUp();
        }

        void SetUp()
        {
            dataGridView1.Rows.Clear();
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumber != null)
                {
                    txtReturnNoteNumber.Text = LastNumber.col2.ToString();
                }

                cmbSupplierDetails.DataSource = context.TLADM_Cotton.OrderBy(x =>x.Cotton_Description).ToList();
                cmbSupplierDetails.DisplayMember = "Cotton_Description";
                cmbSupplierDetails.ValueMember = "Cotton_Pk";
                cmbSupplierDetails.SelectedValue = 0;

                cmbContractNo.DataSource = context.TLADM_CottonContracts.OrderBy(x => x.CottonCon_Description).ToList();
                cmbContractNo.ValueMember = "CottonCon_Pk";
                cmbContractNo.DisplayMember = "CottonCon_No";
                cmbContractNo.SelectedValue = 0;
                
                cmbTransporter.DataSource = context.TLADM_CottonHauliers.OrderBy(x => x.Haul_Description).ToList();
                cmbTransporter.ValueMember = "Haul_Pk";
                cmbTransporter.DisplayMember = "Haul_Description";
                cmbTransporter.SelectedValue = 0;

       
            }

            txtGrossWeightReturned.Text = "0.00";
            txtNettWeightReturned.Text = "0.00";
            txtVehReg.Text = String.Empty;

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
            dataGridView1.Columns.Add(oTxtBoxD);    // 4 kgs (NETT) Decimal 
            dataGridView1.Columns.Add(oTxtBoxE);    // 5 Staple Decimal
            dataGridView1.Columns.Add(oTxtBoxF);    // 6 Kgs (GROSS) Decimal
            
            // dataGridView1.CellEndEdit += this.dataGridView1_CellEndEdit;
           
            rtbNotes.Text = string.Empty;

            txtGrossWeightReturned.Text = "0.00";
            txtNettWeightReturned.Text = "0.00";
            txtWeighBridgeEmpty.Text = "0";
            txtWeighBridgeFull.Text = "0";

            txtWeighBridgeEmpty.KeyDown += core.txtWin_KeyDown;
            txtWeighBridgeEmpty.KeyPress += core.txtWin_KeyPress;
            txtWeighBridgeFull.KeyDown += core.txtWin_KeyDown;
            txtWeighBridgeFull.KeyPress += core.txtWin_KeyPress;

            btnSave.Enabled = false;
            btnPickList.Enabled = false;

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            cmbPrevious.Visible = false;
            _EditMode = false;

            formloaded = true;
        }

       
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }
       

        private void cmbContractNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            ;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                cmbLotNo.SelectedValue = 0;
                using (var context = new TTI2Entities())
                {
                    var selectedRecord = (TLADM_CottonContracts)oCmbo.SelectedItem;
                    if (selectedRecord != null)
                    {
                        IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();

                        dataGridView1.Rows.Clear();
                        var existing = context.SelectCottonRecords1(selectedRecord.CottonCon_Pk, selectedRecord.CottonCon_ConSupplier_FK);
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
                            cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                            cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                            cr.cotrx_WriteOff = row.cotrx_WriteOff;
                            cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                            cotreceived.Add(cr);
                        }

                        cmbLotNo.DataSource = cotreceived.ToList();
                        //cmbLotNo.DataSource = context.TLSPN_CottonReceived.Where(x => x.CotRec_Contract_FK == selectedRecord.CottonCon_Pk).ToList();
                        cmbLotNo.ValueMember = "cotrx_LotNo";
                        cmbLotNo.DisplayMember = "cotrx_LotNo";

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
            IList<TLSPN_CottonReceivedBales> ExistingData = new List<TLSPN_CottonReceivedBales>();
            if (oCmbo != null & formloaded)
            {
                var selectedRecord = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (selectedRecord != null)
                {
                    btnPickList.Enabled = true; 
                    dataGridView1.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                        if (!_EditMode)
                            ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo && !x.CoBales_CottonSold && !x.CoBales_IssuedToProd && !x.CoBales_CottonReturned).ToList();
                        else
                        {
                            var prev = (TLSPN_CottonTransactions)cmbPrevious.SelectedItem;
                            ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CoBales_CottonSequence == prev.cotrx_Return_No).ToList();
                        }
                        foreach (var row in ExistingData)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                            if(_EditMode)
                                dataGridView1.Rows[index].Cells[1].Value = true;

                            dataGridView1.Rows[index].Cells[2].Value = row.CotBales_BaleNo;
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.CotBales_Mic, 2);
                            dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Staple, 2);
                            dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                        }
                                                   
                        if (ExistingData.Count() > 0)
                        {
                            if (!_EditMode)
                                RowsSelected = core.PopulateArray(dataGridView1.Rows.Count, false);
                            else
                            {
                                RowsSelected = core.PopulateArray(dataGridView1.Rows.Count, true);
                                MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                            }

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

        private void cmbSupplierDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    cmbContractNo.SelectedValue = 0;
                    dataGridView1.Rows.Clear();

                    var selected = (TLADM_Cotton)cmbSupplierDetails.SelectedItem;
                    if (selected != null)
                    {
                        cmbContractNo.DataSource = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == selected.Cotton_Pk).ToList();
                        
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool success = true;
            Button oBtn = sender as Button;
            TLSPN_CottonReceivedBales bales;
            TLADM_TranactionType trantypes = new TLADM_TranactionType();
            if (oBtn != null)
            {
                             

                var cnt = RowsSelected.Where(x => x == false).Count();
                if (cnt == RowsSelected.Count())
                {
                    MessageBox.Show("Please select at least one row from the grid as shown");
                    return;
                }

                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var CottonContract = (TLADM_CottonContracts)cmbContractNo.SelectedItem;
                var CottonTrans = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                var Supplier = (TLADM_Cotton)cmbSupplierDetails.SelectedItem;
            
                //Dont forget last number used
                //----------------------------------------------
               
                using (var context = new TTI2Entities())
                {
                    //Hard Coded at the moment 
                    // See Table TLADM_TranactionType for a complete List of the Transaction Type Per Department
                    //--------------------------------------------------------------------------------------------------
                    var DeptDetails = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (DeptDetails != null)
                    {
                        trantypes = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 200 && x.TrxT_Department_FK == DeptDetails.Dep_Id).FirstOrDefault();
                    }
                    
                    var Haulier = (TLADM_CottonHauliers)cmbTransporter.SelectedItem;
                    var TransDet = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;

                    int noBales = 0;
                    //-------------------------------------------------------------------------------------------
                    //  Up date the records from DataGridView....If Not in Edit Mode
                    //--------------------------------------------------
                    if (!_EditMode)
                    {
                        var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                        if (LastNumber != null)
                        {
                            LastNumber.col2 += 1;
                        }
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null || (bool)row.Cells[1].Value == false)
                                continue;

                            bales = context.TLSPN_CottonReceivedBales.Find((int)row.Cells[0].Value);
                            if (bales != null)
                            {
                                if ((bool)row.Cells[1].Value == true)
                                {
                                    bales.CoBales_CottonReturned = true;
                                    bales.CoBales_CottonSequence = Convert.ToInt32(txtReturnNoteNumber.Text);
                                    noBales += 1;
                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        success = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if ((bool)row.Cells[1].Value == false)
                            {
                                bales = context.TLSPN_CottonReceivedBales.Find((int)row.Cells[0].Value);
                                if (bales != null)
                                {
                                    bales.CoBales_CottonReturned = false;
                                    bales.CoBales_CottonSequence = 0;

                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        success = false;
                                    }
                                }
                            }
                            else
                                noBales += 1;
                        }
                    }
                    
                    //----------------------------------------------------------------
                    // Now onto the main transaction 
                    // Edit Mode plays a role!!!!!
                    //-------------------------------------------------------------------
                    TLSPN_CottonTransactions CTS = new TLSPN_CottonTransactions();
                    if (_EditMode)
                    {
                        var prev = (TLSPN_CottonTransactions)cmbPrevious.SelectedItem;
                        if (prev != null)
                        {
                            CTS = context.TLSPN_CottonTransactions.Find(prev.cotrx_pk);
                        }
                    }
                    //------------------------------------------------------------------

                    CTS.cotrx_ContractNo_Fk = CottonContract.CottonCon_Pk;
                    CTS.cotrx_Customer_FK = null;
                    CTS.cotrx_GrossAveBaleWeight = (Convert.ToDecimal(txtGrossWeightReturned.Text)/noBales);
                    CTS.cotrx_GrossWeight = Convert.ToDecimal(txtGrossWeightReturned.Text);
                    CTS.cotrx_LotNo = TransDet.cotrx_LotNo;
                    CTS.cotrx_NetWeight = Convert.ToDecimal(txtNettWeightReturned.Text);
                    CTS.cotrx_NoBales = noBales;
                    CTS.cotrx_Notes = string.Empty;
                    CTS.cotrx_Haulier_FK = Haulier.Haul_Pk;
                    CTS.cotrx_Return_No = Convert.ToInt32(txtReturnNoteNumber.Text);
                    CTS.cotrx_Supplier_FK = Supplier.Cotton_Pk;
                    CTS.cotrx_TransDate = dtpDateReturned.Value;
                    CTS.cotrx_TranType = trantypes.TrxT_Pk;
                    CTS.cotrx_VehReg = txtVehReg.Text;
                    CTS.cotrx_WeighBridgeEmpty = Convert.ToDecimal(txtWeighBridgeEmpty.Text);
                    CTS.cotrx_WeighBridgeFull = Convert.ToDecimal(txtWeighBridgeFull.Text);
                    CTS.cotrx_WriteOff = true;
                    CTS.cottrx_NettAveBaleWeight = Convert.ToDecimal(txtNettWeightReturned.Text) / noBales;
                    CTS.cotrx_NettPerWB = CTS.cotrx_WeighBridgeFull - CTS.cotrx_WeighBridgeEmpty;

                    //----------------------------------------------------------------------------------
                    
                    if(!_EditMode)
                        context.TLSPN_CottonTransactions.Add(CTS);
                   
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        success = false;
                    }

                    //----------------------------------------------------------------------
                }

                if (success)
                {
                    MessageBox.Show("Records stored to database successfully");
                    var ReturnNo = Convert.ToInt32(txtReturnNoteNumber.Text);
                    frmViewReport vRep = new frmViewReport(3, ReturnNo);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    SetUp();
                }
            }
        }

        private void cmbTransporter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    var Transport =  (TLADM_CottonHauliers)cmbTransporter.SelectedItem;
                    if(Transport != null) 
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
            }
        }

        private void cmbVehicleReg_SelectedIndexChanged(object sender, EventArgs e)
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

        private void txt_ValuieChanged(object sender, EventArgs e)
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

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var Cell = oDgv.CurrentCell;

                if (Cell.EditedFormattedValue.ToString() == bool.TrueString && !RowsSelected[Cell.RowIndex])
                {
                    var Total = Convert.ToDecimal(txtGrossWeightReturned.Text);
                    txtGrossWeightReturned.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                    Total = Convert.ToDecimal(txtNettWeightReturned.Text);
                    txtNettWeightReturned.Text = (Total + Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                    RowsSelected[Cell.RowIndex] = true;
                }
                else if (Cell.EditedFormattedValue.ToString() == bool.FalseString && RowsSelected[Cell.RowIndex])
                {
                    var Total = Convert.ToDecimal(txtGrossWeightReturned.Text);
                    txtGrossWeightReturned.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[6].Value.ToString()), 2)).ToString();
                    Total = Convert.ToDecimal(txtNettWeightReturned.Text);
                    txtNettWeightReturned.Text = (Total - Math.Round(Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[4].Value.ToString()), 2)).ToString();
                    RowsSelected[Cell.RowIndex] = false;
                }

            }
        }

        private void btnPickList_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a lot number from the drop down list");
                    return;
                }
                
                var cnt = RowsSelected.Where(x => x == false).Count();
                if (cnt == RowsSelected.Count())
                {
                    MessageBox.Show("Please select at least one row from the grid as shown");
                    return;
                }

                var LotNo = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;
                frmViewReport  vRep = new frmViewReport(11, LotNo.cotrx_LotNo, dataGridView1);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                formloaded = false;
                DisplayFields();
                formloaded = true;
            }
        }

        private void DisplayFields()
        {
            /*
            label1.Visible = !label1.Visible;
            label3.Visible = !label3.Visible;
            label4.Visible = !label4.Visible;
            label5.Visible = !label5.Visible;
            */
            cmbSupplierDetails.Visible = !cmbSupplierDetails.Visible;
            /*
            txtReturnNoteNumber.Visible = !txtReturnNoteNumber.Visible;
            cmbContractNo.Visible = !cmbContractNo.Visible;
            dtpDateReturned.Visible = !dtpDateReturned.Visible;
             */ 
            cmbLotNo.Enabled = !cmbLotNo.Enabled;
            
            cmbPrevious.Visible = !cmbPrevious.Visible;
            if (cmbPrevious.Visible)
            {
                _EditMode = true;

                cmbPrevious.Location = new Point(260, 58);
                label2.Text = "Previous Transactions";

                button1.Text = "Update Mode";

                using (var context = new TTI2Entities())
                {
                    var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    if (Department != null)
                    {
                        var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 200 && x.TrxT_Department_FK == Department.Dep_Id).FirstOrDefault();
                        if (TranType != null)
                        {
                            label2.Text = TranType.TrxT_Description;
                            var Existing = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TranType == TranType.TrxT_Pk).ToList();

                            cmbPrevious.DataSource = Existing;
                            cmbPrevious.DisplayMember = "Cotrx_Return_No";
                            cmbPrevious.ValueMember = "Cotrx_Pk";
                        }
                    }
                }
            }
            else
            {
                label2.Text = "Contract Details";
                button1.Text = "Edit Mode";
                dataGridView1.Rows.Clear();
                SetUp();
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnPickList, "Hello");
        }

        private void cmbPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var CTS = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (CTS != null)
                {
                    txtReturnNoteNumber.Text = CTS.cotrx_Return_No.ToString();
                    cmbSupplierDetails.SelectedValue = CTS.cotrx_Supplier_FK;
                    cmbContractNo.SelectedValue = CTS.cotrx_ContractNo_Fk;
                    dtpDateReturned.Value = CTS.cotrx_TransDate;
                    cmbLotNo.SelectedValue = CTS.cotrx_LotNo;
                    txtGrossWeightReturned.Text = Math.Round(CTS.cotrx_GrossWeight, 1).ToString();
                    txtNettWeightReturned.Text = Math.Round(CTS.cotrx_NetWeight, 1).ToString();

                    cmbTransporter.SelectedValue = CTS.cotrx_Haulier_FK;
                    txtVehReg.Text = CTS.cotrx_VehReg;
                    txtWeighBridgeFull.Text = Math.Round(CTS.cotrx_WeighBridgeFull, 1).ToString();
                    txtWeighBridgeEmpty.Text = Math.Round(CTS.cotrx_WeighBridgeEmpty, 1).ToString();

                }
            }
        }

        private void dtpDateReturned_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
                var Contract = (TLADM_CottonContracts)cmbContractNo.SelectedItem;
                if (Contract != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oDtp.Name
                                  select u).FirstOrDefault();
                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;

                        if (oDtp.Value < Contract.CottonCon_StartDate)
                        {
                            MessageBox.Show("Cotton return date cannot be before contract start date", "Contract Start Date " + Contract.CottonCon_StartDate.ToShortDateString());
                            MandSelected[nbr] = false;
                            oDtp.Focus();
                            return;
                        }
                    }
                }
            }
        }
    }
}
