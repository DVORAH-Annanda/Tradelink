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
using TTI2_WF;

namespace Administration
{
    public partial class frmCottonContracts : Form
    {
        Util core;
        int SupFK;
        bool formloaded;
        bool addRec;
        bool CottonSelected;

        string[][] MandatoryFields;
        bool[] MandSelected;

        public frmCottonContracts(int SupplierFK)
        {
            MandatoryFields = new string[][]
                {   new string[] {"txtContractNo", "Please enter a contract number", "0", "10", "D"},
                    new string[] {"txtSupplierRef", "Please enter a supplier reference No", "1", "10", ""},
                    new string[] {"txtKiloPM", "Please enter the expected kilograms pr month", "2", "", ""}, 
                    new string[] {"txtUSDPerLb", "Please enter a USD per Lb amount", "3", "", ""}, 
                    new string[] {"txtUSDPerKg", "Please enter a USD per Kg amount", "4", "", ""},
                    new string[] {"txtRandPerKg", "Please enter a Rand per Kg amount", "5", "", "" },
                    new string[] {"dtpContractDate", "Please enter a contract date", "6", "", ""},
                    new string[] {"dtpContractStart", "Please enter a contract start date", "7", "", ""}, 
                    new string[] {"dtpContractComplete", "Please enter a contract completion date", "8", "", ""},
                    new string[] {"cmb_unitsOfMeasure", "Please select a unit of measure", "9", "", ""},
                    new string[] {"txtMass", "Please enter a contract mass", "10", "", ""},
                    new string[] {"txtNoOfBales", "Please enter the number of bales", "11", "", ""},
                    new string[] {"txtMicraFrom", "Please enter Micra From", "12", "", ""},
                    new string[] {"txtMicraTo", "Please enter Micra To", "13", "", ""},
                    new string[] {"txtStapleFrom", "Please enter Staple from", "14", "", ""},
                    new string[] {"txtStapleTo", "Please enter Staple To", "15", "", ""}
                };


            core = new Util();
          

            InitializeComponent();
            
            txtKiloPM.KeyPress    += core.txtWin_KeyPress;
            txtKiloPM.KeyDown     += core.txtWin_KeyDownOEM;

            txtMicraFrom.KeyPress += core.txtWin_KeyPress;
            txtMicraFrom.KeyDown += core.txtWin_KeyDownOEM;

            txtMicraTo.KeyPress += core.txtWin_KeyPress;
            txtMicraTo.KeyDown += core.txtWin_KeyDownOEM;

            txtStapleFrom.KeyPress += core.txtWin_KeyPress;
            txtStapleFrom.KeyDown += core.txtWin_KeyDownOEM;

            txtStapleTo.KeyPress += core.txtWin_KeyPress;
            txtStapleTo.KeyDown += core.txtWin_KeyDownOEM;

            txtMass.KeyDown       += core.txtWin_KeyDown;
            txtMass.KeyPress      += core.txtWin_KeyPress;

            txtNoOfBales.KeyDown += core.txtWin_KeyDown;
            txtNoOfBales.KeyPress += core.txtWin_KeyPress;

            txtRandPerKg.KeyPress += core.txtWin_KeyPress;
            txtRandPerKg.KeyDown  += core.txtWin_KeyDownOEM;

            txtUSDPerKg.KeyPress  += core.txtWin_KeyPress;
            txtUSDPerKg.KeyDown   += core.txtWin_KeyDownOEM;

            txtUSDPerLb.KeyPress  += core.txtWin_KeyPress;
            txtUSDPerLb.KeyDown   += core.txtWin_KeyDownOEM;

            SupFK = SupplierFK;

          //   setup();

        }

        private void frmCottonContracts_Load(object sender, EventArgs e)
        {
            formloaded = false;
            CottonSelected = false;
            addRec = true;

            txtContractNo.Text = string.Empty;
            txtContractNo.Focus();

            txtKiloPM.Text = "0.00";
            txtRandPerKg.Text = "0.00";
            txtSupplierRef.Text = string.Empty;
            txtUSDPerKg.Text = "0.00";
            txtUSDPerLb.Text = "0.00";
            txtMass.Text = "0";
            txtNoOfBales.Text = "0";
            txtMicraFrom.Text = "0.00";
            txtMicraTo.Text = "0.00";
            txtStapleFrom.Text = "0.00";
            txtStapleTo.Text = "0.00";


            txtCottonDescription.Text = string.Empty;
            rtbRemarks.Text = string.Empty;
            rbShowKgNo.Checked = true;
            rbShowKgReceivedNo.Checked = true;

            dtpContractComplete.Value = DateTime.Now;
            dtpContractDate.Value = DateTime.Now;
            dtpContractStart.Value = DateTime.Now;

            using (var context = new TTI2Entities())
            {
                cmbCottonContracts.DataSource = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == SupFK).ToList();
                cmbCottonContracts.ValueMember = "CottonCon_Pk";
                cmbCottonContracts.DisplayMember = "CottonCon_No";
                cmbCottonContracts.SelectedValue = -1;

                cmb_unitsOfMeasure.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_ShortCode).ToList();
                cmb_unitsOfMeasure.ValueMember = "UOM_Pk";
                cmb_unitsOfMeasure.DisplayMember = "UOM_Description";
                cmb_unitsOfMeasure.SelectedValue = -1;
            }

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            CottonSelected = true;
            formloaded = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null & formloaded)
            {
                frmCottonContracts_Load(this, null);
            }
        }

        private void cmbCottonContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                addRec = false;

                var Record = (TLADM_CottonContracts)cmbCottonContracts.SelectedItem;
                if (Record != null)
                {
                    CottonSelected = false;
                    txtContractNo.Text  = Record.CottonCon_No;
                    txtKiloPM.Text      = Record.CottonCon_PerMonth.ToString();
                    txtRandPerKg.Text   = Math.Round(Record.CottonCon_ZAPricePerKg, 2).ToString();
                    txtSupplierRef.Text = Record.CottonCon_Reference;
                    rtbRemarks.Text     = Record.CottonCon_Remarks;
                    txtUSDPerKg.Text    = Math.Round(Record.CottonCon_USPricePerKg, 2).ToString();
                    txtUSDPerLb.Text    = Math.Round(Record.CottonCon_USPriceperLb, 2).ToString();
                    txtCottonDescription.Text = Record.CottonCon_Description;
                    txtMass.Text = Record.CottonCon_Mass.ToString();
                    txtNoOfBales.Text = Record.CottonCon_NoOfBales.ToString();
                    txtMicraFrom.Text = Record.CottonCon_MicraFrom.ToString();
                    txtMicraTo.Text = Record.CottonCon_MicraTo.ToString();

                    txtStapleFrom.Text = Record.CottonCon_StapleFrom.ToString();
                    txtStapleTo.Text = Record.CottonCon_StapleTo.ToString();

                    if (Record.CottonCon_ShowOutStandingKg)
                        rbShowKgYes.Checked = true;
                    else
                        rbShowKgNo.Checked = true;

                    if (Record.CottonCon_ShowTotalKgReceived)
                        rbShowKgReceivedYes.Checked = true;
                    else
                        rbShowKgReceivedNo.Checked = true;

                    dtpContractDate.Value = Record.CottonCon_ContractDate;
                    dtpContractStart.Value = Record.CottonCon_StartDate;
                    dtpContractComplete.Value = Record.CottonCon_EndDate;
                    if (Record.CottonCon_Closed)
                    {
                        ChkContractClosed.Checked = Record.CottonCon_Closed;
                        dtpClosedDate.Value = (DateTime)Record.CottonCon_DateClosed;
                    }
                    cmb_unitsOfMeasure.SelectedValue = Record.CottonCon_UOM_Fk;

                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                    CottonSelected = true;
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

                TLADM_CottonContracts cotton = new TLADM_CottonContracts();

                using (var context = new TTI2Entities())
                {
                    if (!addRec)
                    {
                        var ExistRecords = (TLADM_CottonContracts)cmbCottonContracts.SelectedItem;
                        if (ExistRecords != null)
                        {
                            cotton = context.TLADM_CottonContracts.Find(ExistRecords.CottonCon_Pk);
                        }
                    }

                    cotton.CottonCon_ConSupplier_FK   = SupFK;
                    cotton.CottonCon_ContractDate     = dtpContractDate.Value;
                    cotton.CottonCon_Description      = string.Empty;
                    cotton.CottonCon_StartDate        = dtpContractStart.Value;
                    cotton.CottonCon_EndDate          = dtpContractComplete.Value;
                    cotton.CottonCon_Mass             = 0;
                    cotton.CottonCon_No               = txtContractNo.Text;
                    cotton.CottonCon_PerMonth         = Convert.ToDecimal(txtKiloPM.Text);
                    cotton.CottonCon_Reference        = txtSupplierRef.Text;
                    cotton.CottonCon_Remarks          = rtbRemarks.Text;
                    cotton.CottonCon_UOM_Fk           = Convert.ToInt32(cmb_unitsOfMeasure.SelectedValue.ToString());
                    cotton.CottonCon_USPricePerKg     = Convert.ToDecimal(txtUSDPerKg.Text);
                    cotton.CottonCon_USPriceperLb     = Convert.ToDecimal(txtUSDPerLb.Text);
                    cotton.CottonCon_ZAPricePerKg     = Convert.ToDecimal(txtRandPerKg.Text);
                    cotton.CottonCon_Description      = txtCottonDescription.Text;
                    cotton.CottonCon_Mass             = Convert.ToInt32(txtMass.Text);
                    cotton.CottonCon_NoOfBales        = Convert.ToInt32(txtNoOfBales.Text);
                    cotton.CottonCon_StapleFrom       = Convert.ToDecimal(txtStapleFrom.Text);
                    cotton.CottonCon_StapleTo         = Convert.ToDecimal(txtStapleTo.Text);
                    cotton.CottonCon_MicraFrom = Convert.ToDecimal(txtMicraFrom.Text);
                    cotton.CottonCon_MicraTo = Convert.ToDecimal(txtMicraTo.Text);
                    if (ChkContractClosed.Checked)
                    {
                        cotton.CottonCon_Closed = true;
                        cotton.CottonCon_DateClosed = dtpClosedDate.Value;
                    }

                    if(rbShowKgReceivedYes.Checked)
                        cotton.CottonCon_ShowTotalKgReceived = true;
                    else
                        cotton.CottonCon_ShowTotalKgReceived = false;
                    
                    if (rbShowKgYes.Checked)
                        cotton.CottonCon_ShowOutStandingKg = true;
                    else
                        cotton.CottonCon_ShowOutStandingKg = false;

                    if (addRec)
                        context.TLADM_CottonContracts.Add(cotton);

                    try
                    {
                        context.SaveChanges();
                        formloaded = false;
                        cmbCottonContracts.DataSource = context.TLADM_CottonContracts.Where(x => x.CottonCon_ConSupplier_FK == SupFK).OrderBy(x => x.CottonCon_No).ToList();
                        formloaded = true;
                        //  setup();
                        frmCottonContracts_Load(this, null);
                        MessageBox.Show("Records save to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txtBox_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTBox = sender as TextBox;
            DateTimePicker oDtp = sender as DateTimePicker;
            ComboBox oComB = sender as ComboBox;

            if (oTBox != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTBox.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    
                    if(!String.IsNullOrEmpty(result[3]))
                    {
                        if(oTBox.TextLength > Convert.ToInt32(result[3].ToString()))
                        {
                            MessageBox.Show("Value entered exceeds permissable length", "Maximum characters allowed " + result[3].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                      
                       
                    }

                    if (!String.IsNullOrEmpty(result[4]) && CottonSelected)
                    {
                        int index = cmbCottonContracts.FindStringExact(oTBox.Text);
                        if (index != -1)
                        {
                            MessageBox.Show("Value entered already exists in the database", "Already exists " + result[3].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            int xnbr = Convert.ToInt32(result[2].ToString());
                            MandSelected[xnbr] = false;
                            oTBox.Focus();
                            return;
                        }
                    }

                    int nbr = Convert.ToInt32(result[2].ToString());
                    if(oTBox.TextLength > 0)
                      MandSelected[nbr] = true;
                    else
                      MandSelected[nbr] = false;
                }
            }
            else if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();
                if (oDtp.Name == "dtpContractComplete")
                {
                    if (oDtp.Value < dtpContractStart.Value)
                    {
                        MessageBox.Show("Contract end date cannot be before contract start date");
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = false;
                        oDtp.Focus();
                        return;
                    }
                }
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
            else if (oComB != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oComB.Name
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
