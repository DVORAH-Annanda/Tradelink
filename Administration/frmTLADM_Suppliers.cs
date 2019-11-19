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
using Administration;

namespace TTI2_WF
{
    public partial class frmTLADM_Suppliers : Form
    {
        bool formloaded;
        bool addRecord;
        string[][] FldNames;
        bool[] FldEntered;
        Util core;
        public frmTLADM_Suppliers()
        {
            InitializeComponent();
         
            FldNames = new string[][]
            {   new string[] {"txtSupplierCode", "Please enter a valid code", "0", "10"},
                new string[] {"txtSupplierDescription", "Please enter a description", "1", "50"}, 
                new string[] {"txtStdPaymentTerms", "Please enter std payment terms", "2", "50"},
                new string[] {"txtDiscountStructure", "Please enter the discount structure", "3", "5"},
                new string[] {"txtTelephone", "Please enter telephone details", "4", "50"},
                new string[] {"txtFax", "Please enter a fax number", "5", "50"}, 
                new string[] {"txtEmailAddress", "Please enter a EMail address", "6", "50"}, 
                new string[] {"txtContactPerson", "Please enter a contact person", "7", "50"},
                new string[] {"rtbShippingAddress1", "Please enter a default shipping address", "8"},
                new string[] {"cmbProductTypes", "Please enter a valid product type", "9"},
                new string[] {"cmbProductGroups", "Please select a product group group", "10"}};
            core = new Util();
       

            SetUp(true);
        }

        void SetUp(bool Updt)
        {
            formloaded = false;
            addRecord = false;

            using (var context = new TTI2Entities())
            {
                cmbSelectSupplier.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Description).ToList();
                cmbSelectSupplier.DisplayMember = "Sup_Description";
                cmbSelectSupplier.ValueMember = "Sup_Pk";
                if (Updt)
                {
                    cmbProductGroups.DataSource = context.TLADM_StockTypes.OrderBy(x => x.ST_Description).ToList();
                    cmbProductGroups.DisplayMember = "ST_Description";
                    cmbProductGroups.ValueMember = "ST_Id";

                    cmbProductTypes.DataSource = context.TLADM_ProductTypes.OrderBy(x => x.PT_Description).ToList();
                    cmbProductTypes.DisplayMember = "PT_Description";
                    cmbProductTypes.ValueMember = "PT_Pk"; 

                }
            }

            txtContactPerson.Text = string.Empty;
            txtDiscountStructure.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtStdPaymentTerms.Text = string.Empty;
            txtSupplierCode.Text = string.Empty;
            txtSupplierDescription.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtVatReference.Text = string.Empty;
            rtbPostalAddress.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;

            rtbShippingAddress1.Text = string.Empty;
           

            rbBlockedNo.Checked = true;
            rbAllowsConsignmentNo.Checked = true;
            rbDocsEmailedNo.Checked = true;
            txtEmailAddress.Visible = true;

            cmbProductTypes.SelectedValue = 0;
            cmbProductGroups.SelectedValue = 0;
            cmbSelectSupplier.SelectedValue = 0;
            
            FldEntered = core.PopulateArray(FldNames.Length, false);

            rtbNotes.Text = string.Empty;

            formloaded = true;
        }

        


        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    int length = Convert.ToInt32(result[3].ToString());

                    if (oTxt.TextLength > 0 && oTxt.TextLength < length)
                        FldEntered[nbr] = true;
                    else
                    {
                        FldEntered[nbr] = false;
                        if (oTxt.TextLength > length)
                        {
                            MessageBox.Show("Value entered exceeds maximum length");
                        }
                    }

                    if (nbr == 0 && addRecord)
                    {
                        
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                SetUp(false);
                addRecord = true;
                FldEntered = core.PopulateArray(FldNames.Length, false);
            }
        }

        private void cmbSelectSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var selectedsupplier = (TLADM_Suppliers)cmbSelectSupplier.SelectedItem;
                if (selectedsupplier != null)
                {
                    addRecord = false;

                    txtContactPerson.Text = selectedsupplier.Sup_ContactPerson; ;
                    txtDiscountStructure.Text = selectedsupplier.Sup_DiscountStructure; ;
                    txtEmailAddress.Text = selectedsupplier.Sup_EMailContact;
                    txtFax.Text = selectedsupplier.Sup_Fax;
                    txtStdPaymentTerms.Text = selectedsupplier.Sup_StdPayMentTerms;
                    txtSupplierCode.Text = selectedsupplier.Sup_Code;
                    txtSupplierDescription.Text = selectedsupplier.Sup_Description;
                    txtTelephone.Text = selectedsupplier.Sup_Telephone;
                    txtVatReference.Text = selectedsupplier.Sup_VatReference;
                    rtbPostalAddress.Text = selectedsupplier.Sup_PostalAddress;
                    txtEmailAddress.Text = selectedsupplier.Sup_EMailContact;
                    rtbNotes.Text = selectedsupplier.Sup_Notes;

                    rtbShippingAddress1.Text = selectedsupplier.Suip_ShippingAddress1;
                  

                    if (selectedsupplier.Sup_Blocked)
                        rbAccountBlockedYes.Checked = true;
                    else
                        rbBlockedNo.Checked = true;
                    
                    if (selectedsupplier.Sup_AllowsConsignment)
                        rbAllowsConsignmentYes.Checked = true;
                    else
                        rbAllowsConsignmentNo.Checked = true;

                    if (selectedsupplier.Sup_AllowsEMail == true)
                    {
                        rbDocsEmailedYes.Checked = true;
                       
                    }
                    else
                    {
                        rbDocsEmailedYes.Checked = false;
                       
                    }

                    cmbProductGroups.SelectedValue = selectedsupplier.Sup_ProductGroups_FK;
                    cmbProductTypes.SelectedValue = selectedsupplier.Sup_ProductTypes_FK;

                    FldEntered = core.PopulateArray(FldNames.Length, true);
                }
            }

        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {

                var result = (from u in FldNames
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    FldEntered[nbr] = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var errorM = core.returnMessage(FldEntered, addRecord, FldNames);

               
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                //-------------------------------------------------------------------------------------------------------
                // Now the user may have entered data without selecting the "new" button
                // in that case "force" a new
                //----------------------------------------------------------------------------------------------------------

                TLADM_Suppliers suppRec = new TLADM_Suppliers();
                using (var context = new TTI2Entities())
                {
                     if (!addRecord)
                     {
                         var sRec = (TLADM_Suppliers)cmbSelectSupplier.SelectedItem;
                         if (sRec != null)
                         {
                             suppRec = context.TLADM_Suppliers.Find(sRec.Sup_Pk);
                         }
                         else
                             addRecord = true;
                     }

                     suppRec.Sup_Code = txtSupplierCode.Text;
                     suppRec.Sup_Description = txtSupplierDescription.Text;
                     suppRec.Sup_ContactPerson = txtContactPerson.Text;
                     suppRec.Sup_DiscountStructure = txtDiscountStructure.Text;
                     suppRec.Sup_EMailContact = txtEmailAddress.Text;
                     suppRec.Sup_Fax = txtFax.Text;
                     suppRec.Sup_PostalAddress = rtbPostalAddress.Text;
                     suppRec.Sup_Notes = rtbNotes.Text;
                     suppRec.Sup_ProductGroups_FK = Convert.ToInt32(cmbProductGroups.SelectedValue.ToString());
                     suppRec.Sup_ProductTypes_FK = Convert.ToInt32(cmbProductTypes.SelectedValue.ToString());
                     suppRec.Suip_ShippingAddress1 = rtbShippingAddress1.Text;
                     suppRec.Sup_StdPayMentTerms = txtStdPaymentTerms.Text;
                     suppRec.Sup_Telephone = txtTelephone.Text;
                     suppRec.Sup_VatReference = txtVatReference.Text;
                   
                     if (rbAccountBlockedYes.Checked)
                         suppRec.Sup_Blocked = true;
                     else
                         suppRec.Sup_Blocked = false;

                     if (rbAllowsConsignmentYes.Checked)
                         suppRec.Sup_AllowsConsignment = true;
                     else
                         suppRec.Sup_AllowsConsignment = false;

                     if (rbDocsEmailedYes.Checked)
                         suppRec.Sup_AllowsEMail = true;
                     else
                         suppRec.Sup_AllowsEMail = false;

                     if (addRecord)
                         context.TLADM_Suppliers.Add(suppRec);

                     try
                     {
                         context.SaveChanges();
                         MessageBox.Show("records successfully stored to the database");
                         SetUp(false);
                         FldEntered = core.PopulateArray(FldNames.Length, false);

                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            RichTextBox oRtb = sender as RichTextBox;
            if (oRtb != null && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oRtb.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if (oRtb.Text.Length != 0)
                        FldEntered[nbr] = true;
                    else
                        FldEntered[nbr] = false;
                }
            }
        }

        private void btnAddresses_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                foreach (var Element in FldEntered)
                {
                    if (Element == false)
                    {
                        MessageBox.Show("Please select a supplier from the combo box as appropriate");
                        return;
                    }
                }
                var customer = (TLADM_Suppliers)cmbSelectSupplier.SelectedItem;
                frmTLADM_AdditionalAddress address = new frmTLADM_AdditionalAddress(customer.Sup_Pk, false);
                address.ShowDialog(this);
            }
        }
    }
}
