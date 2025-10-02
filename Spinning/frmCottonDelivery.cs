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

    // *20250926
    //new database fields to be added
    //--CotBales_CotReceived_FK = 0-- >> CotBales_Sample_BaleNo
    //--CotBales_Weight_Gross_Sample
    //--SampleOverride
    //--OverrideReason

    public partial class frmCottonDelivery : Form
    {
        Util core;

        private List<SampleBaleRow> _sampleRows;
        private string _overrideReason;

        string[][] MandatoryFields;
        string[][] MandatoryFieldsWeighBridgeAvailable;
        bool[] MandSelected;
        bool[] MandWeighBridgeAvailableSelected;
        bool formloaded;

        public frmCottonDelivery()
        {
            InitializeComponent();
            core = new Util();

            txtCottonNettWeight.KeyPress += core.txtWin_KeyPress;
            txtCottonNettWeight.KeyDown += core.txtWin_KeyDownOEM;
            txtGrossAvgBaleWeight.KeyPress += core.txtWin_KeyPress;
            txtGrossAvgBaleWeight.KeyDown += core.txtWin_KeyDownOEM;
            txtNetAvgBaleWeight.KeyPress += core.txtWin_KeyPress;
            txtNetAvgBaleWeight.KeyDown += core.txtWin_KeyDownOEM;
            txtNoOfBales.KeyPress += core.txtWin_KeyPress;
            txtNoOfBales.KeyDown += core.txtWin_KeyDown;
            txtSupplierNettWeight.KeyPress += core.txtWin_KeyPress;
            txtSupplierNettWeight.KeyDown += core.txtWin_KeyDownOEM;
            txtWeighBridgeGross.KeyPress += core.txtWin_KeyPress;
            txtWeighBridgeGross.KeyDown += core.txtWin_KeyDownOEM;
            txtWeighBridgeNett.KeyPress += core.txtWin_KeyPress;
            txtWeighBridgeNett.KeyDown += core.txtWin_KeyDownOEM;

            MandatoryFields = new string[][]
                {   new string[] {"cmbCottonSuppliers", "Please select a cotton supplier", "0"},
                    new string[] {"cmbCottonContracts", "Please select a cotton contract", "1"},
                    new string[] {"cmbHaulier", "Please select a Haulier", "2"},
                    new string[] {"txtVehReg", "Please enter a vehicle registration number", "3"},
                    new string[] {"txtNoOfBales", "Please enter the number of bales received", "4"},
                                        new string[] {"txtSuppplierGrossWeight", "Please enter the supplier GROSS weight", "5" },
                    new string[] {"txtSupplierNettWeight", "Please enter the supplier NETT weight", "6" },

                };

            MandatoryFieldsWeighBridgeAvailable = new string[][]
    {

                    new string[] {"txtWeighBridgeGross", "Please enter the weigh bridge GROSS weight", "0"},
                    new string[] {"txtWeighBridgeNett", "Please enter the weigh bridge NET weight", "1"}
    };

            Setup();
        }


        void Setup()
        {

            formloaded = false;
            //button1.Visible = false;

            //txtCottonNettWeight.Text = "0.0";
            //txtGrossAvgBaleWeight.Text = "0.0";
            //txtLotNo.Text = "0";
            //txtNetAvgBaleWeight.Text = "0.0";
            //txtNoOfBales.Text = "0";
            //txtSupplierNettWeight.Text = "0.0";
            //txtWeighBridgeGross.Text = "0.0";
            //txtWeighBridgeNett.Text = "0.0";
            //txtSuppplierGrossWeight.Text = "0.0";
            //txtVehReg.Text = string.Empty;

            cmbCottonContracts.SelectedValue = 0;
            cmbCottonContracts.Enabled = true;
            cmbHaulier.SelectedValue = 0;


            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            MandWeighBridgeAvailableSelected = core.PopulateArray(MandatoryFieldsWeighBridgeAvailable.Length, false);

            using (var context = new TTI2Entities())
            {
                //last used number for GRN and lot number
                var LastNo = context.TLADM_LastNumberUsed.Find(1);
                if (LastNo != null)
                {
                    //populate lot and grn fields
                    txtLotNo.Text = LastNo.col1.ToString();
                    txtGrnNumber.Text = LastNo.col12.ToString();
                }

                cmbCottonSuppliers.DataSource = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                cmbCottonSuppliers.ValueMember = "Cotton_Pk";
                cmbCottonSuppliers.DisplayMember = "Cotton_Description";
                cmbCottonSuppliers.SelectedValue = 0;

                cmbHaulier.DataSource = context.TLADM_CottonHauliers.OrderBy(x => x.Haul_Description).ToList();
                cmbHaulier.ValueMember = "Haul_Pk";
                cmbHaulier.DisplayMember = "Haul_Description";
                cmbHaulier.SelectedIndex = 0;
                cmbHaulier.SelectedValue = 0;

            }

            microAvailNo.Checked = true;
            microAvailYes.Checked = false;

            formloaded = true;
        }

        private void cmb_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;

                    if (nbr == 0)
                    {
                        cmbCottonContracts.SelectedValue = 0;

                        var selected = (TLADM_Cotton)cmbCottonSuppliers.SelectedItem;
                        if (selected != null)
                        {
                            using (var context = new TTI2Entities())
                            {
                                formloaded = false;
                                cmbCottonContracts.DataSource = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == selected.Cotton_Pk).ToList();
                                cmbCottonContracts.ValueMember = "CottonCon_Pk";
                                cmbCottonContracts.DisplayMember = "CottonCon_No";
                                cmbCottonContracts.Enabled = true;
                                cmbCottonContracts.SelectedValue = 0;
                                formloaded = true;
                            }
                        }
                    }
                    else if (nbr == 1)
                    {

                    }
                    else if (nbr == 2)
                    {
                        var selected = (TLADM_CottonHauliers)cmbHaulier.SelectedItem;
                        if (selected != null)
                        {

                        }
                    }
                }
            }
        }

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxtB = sender as TextBox;

            if (oTxtB != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxtB.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if (oTxtB.TextLength > 0)
                        MandSelected[nbr] = true;
                    else
                        MandSelected[nbr] = false;
                }

                if (result == null)
                {
                    result = (from u in MandatoryFieldsWeighBridgeAvailable
                              where u[0] == oTxtB.Name
                              select u).FirstOrDefault();


                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        if (oTxtB.TextLength > 0)
                            MandWeighBridgeAvailableSelected[nbr] = true;
                        else
                            MandWeighBridgeAvailableSelected[nbr] = false;
                    }
                }
                //if (MandSelected[4] && MandSelected[5] && MandSelected[6] && MandSelected[7] && MandSelected[8])
                if (chkWeighBridgeAvailable.Enabled && MandSelected[4] && MandSelected[5] && MandSelected[6] && MandWeighBridgeAvailableSelected[0] && MandWeighBridgeAvailableSelected[1])
                {
                    var NoBales = Convert.ToInt32(txtNoOfBales.Text);
                    var WBGrossWeight = Convert.ToDecimal(txtWeighBridgeGross.Text);
                    var WBNettWeight = Convert.ToDecimal(txtWeighBridgeNett.Text);
                    var SupplierGross = Convert.ToDecimal(txtSuppplierGrossWeight.Text);
                    var SupplierNett = Convert.ToDecimal(txtSupplierNettWeight.Text);

                    txtCottonNettWeight.Text = Math.Round(WBGrossWeight - WBNettWeight, 2).ToString();
                    txtGrossAvgBaleWeight.Text = Math.Round(SupplierGross / NoBales, 2).ToString();
                    txtNetAvgBaleWeight.Text = Math.Round(SupplierNett / NoBales, 2).ToString();

                    UpdateCalculatedValues();

                }
                else
                {
                    txtCottonNettWeight.Text = "0.0";
                    txtGrossAvgBaleWeight.Text = "0.0";
                    txtNetAvgBaleWeight.Text = "0.0";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool success = true;
            Button oBtn = sender as Button;


            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);

                if (!string.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                if (chkWeighBridgeAvailable.Checked)
                {
                    var ErrorMWeighBridgeAvailable = core.returnMessage(MandSelected, true, MandatoryFieldsWeighBridgeAvailable);

                    if (!string.IsNullOrEmpty(ErrorMWeighBridgeAvailable))
                    {
                        MessageBox.Show(ErrorMWeighBridgeAvailable);
                        return;
                    }
                }

                using (var context = new TTI2Entities())
                {
                    //TLSPN_CottonReceived cotrec = new TLSPN_CottonReceived();

                    var Contract = (TLADM_CottonContracts)cmbCottonContracts.SelectedItem;
                    var Supplier = (TLADM_Cotton)cmbCottonSuppliers.SelectedItem;
                    var Haulier = (TLADM_CottonHauliers)cmbHaulier.SelectedItem;
                    var NoOfBales = Convert.ToInt32(txtNoOfBales.Text);


                    //---------------------------------------------------------------------------------------------------
                    //Unfortunately hard coded at the moment
                    //--------------------------------------------------------------------------------------------------
                    var DeptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 100 && x.TrxT_Department_FK == DeptDetail.Dep_Id).FirstOrDefault();
                    if (TranType != null)
                    {

                        //cotrec.CotReC_TranType_FK = TranType.TrxT_Pk;

                        var CountTrx = context.TLSPN_CottonTransactions.Count();
                        if (CountTrx == 0)
                        {
                            //--------------------------------------------
                            //this means that this transaction is being run for the first time ie add a record of zeros to the Opening Balance table
                            // This table will cater for future yearend 
                            //--------------------------------------------------------------------------------
                            TLSPN_OpenBalance openBal = new TLSPN_OpenBalance();
                            openBal.OpenBal_Store_FK = (int)TranType.TrxT_FromWhse_FK;
                            openBal.OpenBal_GrossBaleWeight = 0M;
                            openBal.OpenBal_NettBaleWeight = 0M;
                            openBal.OpenBal_NoOfBales = 0;

                            context.TLSPN_OpenBalance.Add(openBal);
                        }
                    }

                    //----------------------------------------------------------------
                    // Now onto the main transaction 
                    //-------------------------------------------------------------------
                    TLSPN_CottonTransactions CTS = new TLSPN_CottonTransactions();
                    CTS.cotrx_ContractNo_Fk = Contract.CottonCon_Pk;
                    CTS.cotrx_Customer_FK = null;
                    CTS.cotrx_GrossWeight = Convert.ToDecimal(txtSuppplierGrossWeight.Text);
                    CTS.cotrx_LotNo = Convert.ToInt32(txtLotNo.Text);
                    CTS.cotrx_NettPerWB = Convert.ToDecimal(txtCottonNettWeight.Text);
                    CTS.cotrx_NetWeight = Convert.ToDecimal(txtSupplierNettWeight.Text);
                    CTS.cotrx_NoBales = NoOfBales;
                    CTS.cotrx_Notes = string.Empty;
                    CTS.cotrx_Return_No = Convert.ToInt32(txtGrnNumber.Text);
                    CTS.cotrx_Supplier_FK = Supplier.Cotton_Pk;
                    CTS.cotrx_TransDate = dtpDateReceived.Value;
                    CTS.cotrx_TranType = TranType.TrxT_Pk;
                    CTS.cotrx_VehReg = txtVehReg.Text;
                    CTS.cotrx_Haulier_FK = Haulier.Haul_Pk;
                    if (decimal.TryParse(txtWeighBridgeNett.Text, out var nett))
                        CTS.cotrx_WeighBridgeEmpty = nett;
                    else
                        CTS.cotrx_WeighBridgeEmpty = 0;

                    if (decimal.TryParse(txtWeighBridgeGross.Text, out var gross))
                        CTS.cotrx_WeighBridgeFull = gross;
                    else
                        CTS.cotrx_WeighBridgeFull = 0;
                    CTS.cotrx_WriteOff = false;
                    CTS.cottrx_NettAveBaleWeight = Convert.ToDecimal(txtNetAvgBaleWeight.Text);
                    CTS.cotrx_GrossAveBaleWeight = Convert.ToDecimal(txtGrossAvgBaleWeight.Text);

                    context.TLSPN_CottonTransactions.Add(CTS);
                    //--------------------------------------------------------------------------------

                    var lastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (lastNumber != null)
                    {
                        lastNumber.col1 += 1;
                        lastNumber.col12 += 1;
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (microAvailNo.Checked)
                    {
                        decimal micraFrom = 0.00M;
                        decimal micraTo = 0.00M;
                        decimal micraAvg = 0.00M;
                        decimal staplefrom = 0.00M;
                        decimal stapleto = 0.00M;
                        decimal stapleAvg = 0.00M;

                        var CottonContract = (TLADM_CottonContracts)cmbCottonContracts.SelectedItem;

                        if (CottonContract != null)
                        {
                            micraFrom = CottonContract.CottonCon_MicraFrom;
                            micraTo = CottonContract.CottonCon_MicraTo;

                            staplefrom = CottonContract.CottonCon_StapleFrom;
                            stapleto = CottonContract.CottonCon_StapleTo;

                            micraAvg = (micraFrom > 0 && micraTo > 0) ? (micraFrom + micraTo) / 2 : 1;
                            stapleAvg = (staplefrom != 0 && stapleto != 0) ? (staplefrom + stapleto) / 2 : 1;
                        }

                        int sampleCount = _sampleRows?.Count ?? 0;
                        int sampleIdx = 0;
                        for (int n = 1; n <= CTS.cotrx_NoBales; n++)
                        {

                            //TLSPN_CottonReceivedBales recBales = new TLSPN_CottonReceivedBales();
                            //recBales.CotBales_BaleNo = n;
                            ////recBales.CotBales_CotReceived_FK = cotrec.CotRec_Pk;
                            //recBales.CotBales_LotNo = Convert.ToInt32(txtLotNo.Text);
                            //recBales.CotBales_Mic = micraAvg;
                            //recBales.CotBales_Staple = stapleAvg;
                            //recBales.CotBales_Weight_Gross = Convert.ToDecimal(txtGrossAvgBaleWeight.Text);
                            //recBales.CotBales_Weight_Nett = Convert.ToDecimal(txtNetAvgBaleWeight.Text);
                            //recBales.CoBales_CottonSequence = Convert.ToInt32(txtGrnNumber.Text);

                            var recBales = new TLSPN_CottonReceivedBales
                            {
                                CotBales_BaleNo = n,
                                CotBales_LotNo = CTS.cotrx_LotNo,
                                CotBales_Mic = micraAvg,
                                CotBales_Staple = stapleAvg,
                                CotBales_Weight_Gross = Convert.ToDecimal(txtGrossAvgBaleWeight.Text), // default gross avg
                                CotBales_Weight_Nett = Convert.ToDecimal(txtNetAvgBaleWeight.Text),
                                CoBales_CottonSequence = CTS.cotrx_Return_No,

                                // initialize new fields empty
                                CotBales_Sample_BaleNo = null,
                                CotBales_Sample_Weight_Gross = null,
                                SampleWeightOverride = null,
                                OverrideReason = null
                            };

                            if (sampleIdx < sampleCount)
                            {
                                var s = _sampleRows[sampleIdx++];

                                recBales.CotBales_Sample_BaleNo = s.SampleBaleNo;
                                recBales.CotBales_Weight_Gross = s.SupplierWeight;   // overwrite gross with supplier sample gross
                                recBales.CotBales_Sample_Weight_Gross = s.TTSWeight;   // store TTS sample gross
                                recBales.SampleWeightOverride = s.Overridden;
                                recBales.OverrideReason = _overrideReason;
                            }

                            context.TLSPN_CottonReceivedBales.Add(recBales);

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                            {
                                foreach (var eve in ex.EntityValidationErrors)
                                {
                                    Console.WriteLine(
                                        $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has validation errors:");
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        Console.WriteLine($"- Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                                    }
                                }

                                throw; // or MessageBox.Show for WinForms
                            }

                        }
                    }
                    else if (microAvailYes.Checked && _sampleRows != null && _sampleRows.Any())
                    {
                        // We expect the correct, actual bales for this LotNo to have been
                        // captured & saved already (e.g., via frmCottonBales).
                        // For each sample row, try to match an existing bale by LotNo + BaleNo (when numeric),
                        // otherwise attach the sample to the next bale without a sample yet (or create a new row as a fallback).

                        // Preload all bales for this LotNo
                        var lotBales = context.TLSPN_CottonReceivedBales
                                              .Where(b => b.CotBales_LotNo == CTS.cotrx_LotNo)
                                              .OrderBy(b => b.CotBales_BaleNo)
                                              .ToList();

                        foreach (var s in _sampleRows)
                        {
                            TLSPN_CottonReceivedBales target = null;

                            // Try numeric match first
                            if (int.TryParse(s.SampleBaleNo, out var numericBaleNo))
                            {
                                target = lotBales.FirstOrDefault(b => b.CotBales_BaleNo == numericBaleNo);
                            }

                            // If not numeric or not found, attach to first bale without a sample yet
                            if (target == null)
                            {
                                target = lotBales.FirstOrDefault(b => b.CotBales_Sample_BaleNo == null);
                            }

                            // As a last resort, create a new bale row to carry the sample info
                            if (target == null)
                            {
                                target = new TLSPN_CottonReceivedBales
                                {
                                    CotBales_LotNo = CTS.cotrx_LotNo,
                                    CotBales_BaleNo = numericBaleNo > 0 ? numericBaleNo : 0,
                                    CoBales_CottonSequence = CTS.cotrx_Return_No
                                };
                                context.TLSPN_CottonReceivedBales.Add(target);
                                lotBales.Add(target);
                            }

                            // Apply sample fields (do NOT alter Mic/Staple/Weight_Nett that micra import set)
                            target.CotBales_Sample_BaleNo = s.SampleBaleNo;
                            target.CotBales_Weight_Gross = s.SupplierWeight;   // supplier gross for that sample bale
                            target.CotBales_Sample_Weight_Gross = s.TTSWeight;        // TTS gross for that sample bale
                            target.SampleWeightOverride = s.Overridden;
                            target.OverrideReason = _overrideReason;
                        }

                        try { context.SaveChanges(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }


                }
                if (success)
                {
                    MessageBox.Show("Data stored to the database successfully");
                    int returnno = Convert.ToInt32(txtGrnNumber.Text);
                    frmViewReport vRep = new frmViewReport(2, returnno);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                    Setup();
                }

            }
        }

        private void btnCaptureSampleBales_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);

                if (!string.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                using (var cb = new frmCottonDeliverySampleBales(Convert.ToInt32(txtNoOfBales.Text),
                                                                dtpDateReceived.Value,
                                                                txtLotNo.Text))
                {
                    if (cb.ShowDialog(this) == DialogResult.OK)
                    {
                        // Save sample rows in a field so Save button can use them later
                        _sampleRows = cb.SampleRows;
                        _overrideReason = cb.OverrideReason;

                        UpdateCalculatedValues();
                        btnSave.Enabled = true;
                    }
                }

            }
        }

        private void frmCottonDelivery_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    using (var ctx = new TTI2Entities())
            //    {
            //        // Populate Suppliers
            //        var suppliers = ctx.TLADM_CottonSuppliers
            //                           .OrderBy(s => s.CS_Description)
            //                           .ToList();
            //        cmbCottonSuppliers.DataSource = suppliers;
            //        cmbCottonSuppliers.DisplayMember = "CS_Description";
            //        cmbCottonSuppliers.ValueMember = "CS_Id";

            //        // Populate Contracts
            //        var contracts = ctx.TLADM_CottonContracts
            //                           .OrderBy(c => c.CottonCon_Reference)
            //                           .ToList();
            //        cmbCottonContracts.DataSource = contracts;
            //        cmbCottonContracts.DisplayMember = "ContractNo";
            //        cmbCottonContracts.ValueMember = "ContractId";

            //        // Populate Hauliers
            //        var hauliers = ctx.TLADM_CottonHauliers
            //                          .OrderBy(h => h.Haul_Description)
            //                          .ToList();
            //        cmbHaulier.DataSource = hauliers;
            //        cmbHaulier.DisplayMember = "H_Description";
            //        cmbHaulier.ValueMember = "H_Id";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading lookup data: " + ex.Message,
            //        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            // Trigger initial validation
            UpdateCalculatedValues();
        }


        private void microAvailYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;
            if (oRb != null && formloaded)
            {
                if (oRb.Checked)
                {
                    var ErrorM = core.returnMessage(MandSelected, true, MandatoryFields);

                    if (!string.IsNullOrEmpty(ErrorM))
                    {
                        MessageBox.Show(ErrorM);
                    }
                    else
                    {

                        TLSPN_CottonTransactions cotTrans = new TLSPN_CottonTransactions();
                        cotTrans.cotrx_LotNo = Convert.ToInt32(txtLotNo.Text);
                        cotTrans.cotrx_Return_No = Convert.ToInt32(txtGrnNumber.Text);
                        cotTrans.cotrx_GrossAveBaleWeight = Convert.ToDecimal(txtGrossAvgBaleWeight.Text);
                        cotTrans.cottrx_NettAveBaleWeight = Convert.ToDecimal(txtNetAvgBaleWeight.Text);
                        cotTrans.cotrx_NoBales = Convert.ToInt32(txtNoOfBales.Text);
                        frmCottonBales cb = new frmCottonBales(cotTrans);
                        cb.ShowDialog(this);
                    }

                }
            }
        }

        private void chkWeighBridgeAvailable_CheckedChanged(object sender, EventArgs e)
        {
            bool available = chkWeighBridgeAvailable.Checked;
            txtWeighBridgeGross.Enabled = available;
            txtWeighBridgeNett.Enabled = available;

            btnCaptureSampleBales.Enabled = !available;
            btnSave.Enabled = available;

            // Update calculations when toggled
            UpdateCalculatedValues();
        }

        private void UpdateCalculatedValues()
        {
            // Parse inputs
            int.TryParse(txtNoOfBales.Text, out int baleCount);

            decimal suppGross = ParseDecimal(txtSuppplierGrossWeight.Text); //without truck, with packaging
            decimal suppNett = ParseDecimal(txtSupplierNettWeight.Text); //without truck, without packaging 
            decimal wbGross = ParseDecimal(txtWeighBridgeGross.Text); //with truck
            decimal wbNett = ParseDecimal(txtWeighBridgeNett.Text); //without truck, with packaging

            // Pick which values to use depending on weigh bridge availability
            decimal gross = chkWeighBridgeAvailable.Checked ? wbGross - wbNett : suppGross;
            //decimal nett = chkWeighBridgeAvailable.Checked ? wbNett : suppNett; --not corect!

            // Set outputs
            txtCottonNettWeight.Text = gross > 0 ? gross.ToString("N1") : ""; //without truck, with packaging
            txtGrossAvgBaleWeight.Text = (baleCount > 0 && gross > 0)
                ? (gross / baleCount).ToString("N1")
                : "";
            txtNetAvgBaleWeight.Text = (baleCount > 0 && suppNett > 0)
                ? (suppNett / baleCount).ToString("N1")
                : "";
        }

        private decimal ParseDecimal(string input)
        {
            return decimal.TryParse(input, out var d) ? d : 0m;
        }

        private void cmbCottonSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;

                    if (nbr == 0)
                    {
                        cmbCottonContracts.SelectedValue = 0;

                        var selected = (TLADM_Cotton)cmbCottonSuppliers.SelectedItem;
                        if (selected != null)
                        {
                            using (var context = new TTI2Entities())
                            {
                                formloaded = false;
                                cmbCottonContracts.DataSource = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == selected.Cotton_Pk).ToList();
                                cmbCottonContracts.ValueMember = "CottonCon_Pk";
                                cmbCottonContracts.DisplayMember = "CottonCon_No";
                                cmbCottonContracts.Enabled = true;
                                cmbCottonContracts.SelectedValue = 0;
                                formloaded = true;
                            }
                        }
                    }
                    else if (nbr == 1)
                    {

                    }
                    else if (nbr == 2)
                    {
                        var selected = (TLADM_CottonHauliers)cmbHaulier.SelectedItem;
                        if (selected != null)
                        {

                        }
                    }
                }
            }
        }

    }
}
