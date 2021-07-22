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
    public partial class frmTLADM_Customers : Form
    {
        bool formloaded;
        string[][] FldNames;
        bool[] FldEntered;
        bool addRecord;
        Util core;

        public frmTLADM_Customers()
        {
            InitializeComponent();

            core = new Util();
            FldNames = new string[][]
            {   new string[] {"txtCustomerCode", "Please enter a valid code", "0", "10"},
                new string[] {"txtCustomerDescription", "Please enter a description", "1", "50"}, 
                new string[] {"rtbPostalAddress", "Please enter a postal address", "2"},
                new string[] {"txtTelephoneNo", "Please enter a telephone No", "3", "50"},
                new string[] {"txtFaxNo", "Please enter a fax no", "4", "50"},
                new string[] {"txtContactPerson", "Please enter contact details", "5", "50"}, 
                new string[] {"txtVatReference", "Please enter a VAT reference", "6", "50"},
                new string[] {"rtbAddress1", "Please enter a default address details", "7"},
                new string[] {"cmbSelectCustomerCategory", "Please selet a customer category", "8"}
            };

            txtLastNumberUsed.KeyPress += core.txtWin_KeyPress;
            txtLastNumberUsed.KeyDown += core.txtWin_KeyDown;

            Setup(true);
  
        }

        void Setup(bool Updt)
        {
            formloaded = false;
            addRecord = false;

            FldEntered = core.PopulateArray(FldNames.Length, false);

            using (var context = new TTI2Entities())
            {
                cmbSelectCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmbSelectCustomers.DisplayMember = "Cust_Description";
                cmbSelectCustomers.ValueMember = "Cust_Pk";
                cmbSelectCustomers.SelectedValue = -1;

                if (Updt)
                {
                    cmbSelectCustomerCategory.DataSource = context.TLADM_CustomerTypes.OrderBy(x => x.CT_ShortCode).ToList();
                    cmbSelectCustomerCategory.DisplayMember = "CT_Description";
                    cmbSelectCustomerCategory.ValueMember = "CT_Id";
                   
                    cmbWareHouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_RePack).ToList();
                    cmbWareHouse.DisplayMember = "WhStore_Description";
                    cmbWareHouse.ValueMember = "WhStore_Id";
                }
            }

            cmbWareHouse.SelectedValue = -1;
            cmbWareHouse.Enabled = false;
            cmbSelectCustomerCategory.SelectedValue = -1;

            rtbPostalAddress.Text = string.Empty;
            rtbAddress1.Text = string.Empty;

           // rbFabricCustomer.Checked = false;

            txtContactPerson.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtCustomerDescription.Text = string.Empty;
            txtEMailAddress.Text = string.Empty;
            txtFaxNo.Text = string.Empty;
            txtTelephoneNo.Text = string.Empty;
            txtVatReference.Text = string.Empty;
            txtContactPersonEmail.Text = String.Empty;
            txtLastNumberUsed.Text = "0";
            txtGreigePrefix.Text = "0";
            rbAccountBlockedNo.Checked = true;
            rbDocsEmailedNo.Checked = true;
            rbRepackNo.Checked = true;
 
            rtbNotes.Text = string.Empty;

            rbFabricNo.Checked = true;

            rbCCNo.Checked = true;
            formloaded = true;
        }

       
        

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                Setup(false);
                addRecord = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var errorM = core.returnMessage(FldEntered, true, FldNames);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                TLADM_CustomerFile customers = new TLADM_CustomerFile();

                using (var context = new TTI2Entities())
                {
                    if (!addRecord)
                    {
                        var selected = (TLADM_CustomerFile)cmbSelectCustomers.SelectedItem;
                        if (selected != null)
                            customers = context.TLADM_CustomerFile.Find(selected.Cust_Pk);
                    }

                    customers.Cust_Code          = txtCustomerCode.Text;
                    customers.Cust_Description   = txtCustomerDescription.Text;
                    customers.Cust_ContactPerson = txtContactPerson.Text;
                    customers.Cust_EmailContact  = txtEMailAddress.Text;
                    customers.Cust_PostalAddress = rtbPostalAddress.Text;
                    customers.Cust_Telephone     = txtTelephoneNo.Text;
                    customers.Cust_Fax           = txtFaxNo.Text;
                    customers.Cust_VatReferenced = txtVatReference.Text;
                    customers.Cust_Address1 = rtbAddress1.Text;
                    customers.Cust_ContactPersonEmail = txtContactPersonEmail.Text;
                    customers.Cust_Notes = rtbNotes.Text;
                    if (rbCCYes.Checked)
                    {
                        customers.Cust_GreigePrefix = txtGreigePrefix.Text;
                        customers.Cust_LastNumberUsed = Convert.ToInt32(txtLastNumberUsed.Text);
                    }
                    if (rbAccountBlockedYes.Checked)
                        customers.Cust_Blocked = true;
                    else
                        customers.Cust_Blocked = false;

                    if (rbDocsEmailedYes.Checked)
                        customers.Cust_SendEmail = true;
                    else
                        customers.Cust_SendEmail = false;

                    if (rbCCYes.Checked)
                        customers.Cust_CommissionCust = true;
                    else
                        customers.Cust_CommissionCust = false;

                    customers.Cust_FabricCustomer = false;

                    if(rbFabricNo.Checked)
                    {
                        customers.Cust_FabricCustomer = false;
                    }
                    else
                    {
                        customers.Cust_FabricCustomer = true;
                    }

                    if (rbRepackYes.Checked)
                    {
                        var Whse = (TLADM_WhseStore)cmbWareHouse.SelectedItem;
                        if (Whse == null)
                        {
                            MessageBox.Show("Please select a Repack Warehouse");
                            return;
                        }
                        customers.Cust_RePack = true;
                        customers.Cust_WareHouse_FK = Whse.WhStore_Id;

                    }
                    else
                        customers.Cust_RePack = false;

                    customers.Cust_CustomerCat_FK = Convert.ToInt32(cmbSelectCustomerCategory.SelectedValue.ToString());

                    if (addRecord)
                        context.TLADM_CustomerFile.Add(customers);

                    try
                    {
                        context.SaveChanges();
                        Setup(false);
                       
                        MessageBox.Show("records saved to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
               }
            }
        }

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            
            if (oTxt != null  && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    int length = Convert.ToInt32(result[3].ToString());

                    if (oTxt.TextLength != 0 && oTxt.TextLength < length)
                        FldEntered[nbr] = true;
                    else
                    {
                        FldEntered[nbr] = false;
                        if (oTxt.TextLength > length)
                            MessageBox.Show("Value entered exceeds maximum length allowed");
                    }

                    if (nbr == 0 && addRecord)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var existing = context.TLADM_CustomerFile
                                            .Where(x=>x.Cust_Code == txtCustomerCode.Text).FirstOrDefault();
                            if (existing != null)
                            {
                                MessageBox.Show("A record is already on file for this code");
                                FldEntered[nbr] = false;
                                txtCustomerCode.Text = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        private string returnMessage(bool[] selectedarray)
        {
            int Cnt = 0;
            StringBuilder Mess = new StringBuilder();

            foreach (var ArrayElement in selectedarray)
            {
                if (bool.FalseString == ArrayElement.ToString())
                {
                    var result = (from u in FldNames
                                  where u[2] == Cnt.ToString()
                                  select u).FirstOrDefault();

                    Mess.Append(result[1] + Environment.NewLine);
                }

                Cnt += 1;
            }
            return Mess.ToString();
        }

        private void cmb_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in FldNames
                                  where u[0] == oCmb.Name
                                  select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                FldEntered[nbr] = true;
             }
        }

        private void cmbSelectCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var customers = (TLADM_CustomerFile)cmbSelectCustomers.SelectedItem;
                if (customers != null)
                {
                    addRecord = false;

                    txtCustomerCode.Text = customers.Cust_Code;
                    txtCustomerDescription.Text = customers.Cust_Description;
                    txtContactPerson.Text = customers.Cust_ContactPerson;
                    txtEMailAddress.Text = customers.Cust_EmailContact;
                    rtbPostalAddress.Text = customers.Cust_PostalAddress;
                    txtTelephoneNo.Text = customers.Cust_Telephone;
                    txtFaxNo.Text = customers.Cust_Fax;
                    txtVatReference.Text = customers.Cust_VatReferenced;
                    rtbAddress1.Text = customers.Cust_Address1;
                    txtContactPersonEmail.Text = customers.Cust_ContactPersonEmail;

                    if (customers.Cust_CommissionCust)
                    {
                        txtGreigePrefix.Text = customers.Cust_GreigePrefix;
                        txtLastNumberUsed.Text = customers.Cust_LastNumberUsed.ToString();
                    }

                   // rbFabricCustomer.Checked = customers.Cust_FabricCustomer;

                    if (customers.Cust_Blocked)
                        rbAccountBlockedYes.Checked = true;
                    else
                        rbAccountBlockedNo.Checked = true;
                    
                    if (customers.Cust_SendEmail)
                        rbDocsEmailedYes.Checked = true;
                    else
                        rbDocsEmailedNo.Checked = true;

                    if (customers.Cust_CommissionCust)
                        rbCCYes.Checked = true;
                    else
                        rbCCNo.Checked = true;

                    if (customers.Cust_RePack)
                    {
                        rbRepackYes.Checked = true;
                        cmbWareHouse.Enabled = true;
                        if (customers.Cust_WareHouse_FK != null)
                            cmbWareHouse.SelectedValue = customers.Cust_WareHouse_FK;

                    }
                    else
                    {
                        rbRepackNo.Checked = true;
                        cmbWareHouse.Enabled = false;
                    }
                    cmbSelectCustomerCategory.SelectedValue = customers.Cust_CustomerCat_FK;
                    rtbNotes.Text = customers.Cust_Notes;
                    if(customers.Cust_FabricCustomer)
                    {
                        rbFabricYes.Checked = true;
                    }
                    else
                    {
                        rbFabricNo.Checked = true;
                    }
                    FldEntered = core.PopulateArray(FldNames.Length, true);
                }
            }
        }

        private void rtb_ValueChanged(object sender, EventArgs e)
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

        private void btnAddress_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                foreach (var Element in FldEntered)
                {
                    if (Element == false)
                    {
                        MessageBox.Show("Please select a customer from the combo box as appropriate");
                        return;
                    }
                }

                var customer = (TLADM_CustomerFile)cmbSelectCustomers.SelectedItem;
                frmTLADM_AdditionalAddress address = new frmTLADM_AdditionalAddress(customer.Cust_Pk, true);
                address.ShowDialog(this);
            }
        }

        private void rtbAddress1_TextChanged(object sender, EventArgs e)
        {
            RichTextBox oRich = sender as RichTextBox;
            if (oRich != null && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oRich.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if (oRich.Text.Length != 0)
                        FldEntered[nbr] = true;
                    else
                        FldEntered[nbr] = false;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            RichTextBox oRich = sender as RichTextBox;
            if (oRich != null && formloaded)
            {

            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && formloaded)
            {
                var Customer = (TLADM_CustomerFile)cmbSelectCustomers.SelectedItem;
                if (Customer != null)
                {
                    frmTLADM_CustomerUser customers = new frmTLADM_CustomerUser(Customer.Cust_Pk);
                    customers.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Please select a Customer from the drop down box"); 
                }
            }
        }

        private void rbRepackNo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded && oRad.Checked)
            {
                if (!cmbWareHouse.Enabled)
                {
                    cmbWareHouse.Enabled = false;

                }
            }
        }

        private void cmbWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbRepackYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded && oRad.Checked)
            {
                if (!cmbWareHouse.Enabled)
                {
                    cmbWareHouse.Enabled = true;
                    cmbWareHouse.Visible = true;
                    
                }
            }
        }
    }
}
