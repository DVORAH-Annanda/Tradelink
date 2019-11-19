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
    public partial class frmTLADMCompanyDetails : Form
    {
        bool AddRecord;
        bool formLoaded;
        public frmTLADMCompanyDetails()
        {
            InitializeComponent();
            Setup();
        }

        void Setup()
        {
            AddRecord = true;
            formLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmbCompany.DataSource = context.TLADM_CompanyDetails.OrderBy(x => x.Comp_Name).ToList();
                cmbCompany.DisplayMember = "Comp_Name";
                cmbCompany.ValueMember = "Comp_Pk";

            }

            cmbCompany.SelectedValue = 0;
            txtCompanyName.Text      = string.Empty;
            txtCompanyName.Focus();
            txtContactPerson.Text    = string.Empty;
            txtFaxNumber.Text        = string.Empty;
            txtTelephoneNo.Text      = string.Empty;
            rtbAddress.Text          = string.Empty;
            txtContactPersonEMail.Text = string.Empty;
            btnLabels.Text = "0";

            formLoaded = true;

        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            AddRecord = false;
            if (oCmb != null && formLoaded)
            {
               

                var selected = (TLADM_CompanyDetails)cmbCompany.SelectedItem;
                if (selected != null)
                {
                    txtCompanyName.Text   = selected.Comp_Name;
                    txtContactPerson.Text = selected.Comp_ContactPerson;
                    txtFaxNumber.Text     = selected.Comp_FaxNo;
                    txtTelephoneNo.Text   = selected.Comp_TelephoneNo;
                    rtbAddress.Text       = selected.Comp_Address;
                    txtContactPersonEMail.Text = selected.Comp_ContactPersonEmail;
                    btnLabels.Text = selected.Comp_TotalPowerN.ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Util core = new Util();

            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if (String.IsNullOrEmpty(txtCompanyName.Text))
                {
                    MessageBox.Show("Please complete the company name as indicated");
                    return;
                }
                TLADM_CompanyDetails comdet = new TLADM_CompanyDetails();

              
             

                using ( var context = new TTI2Entities())
                {
                    if (AddRecord)
                        context.TLADM_CompanyDetails.Add(comdet);
                    else
                    {
                        var SI = (TLADM_CompanyDetails)cmbCompany.SelectedItem;
                        if (SI != null)
                            comdet = context.TLADM_CompanyDetails.Find(SI.Comp_Pk);
                    }

                    comdet.Comp_Name = txtCompanyName.Text;
                    comdet.Comp_Address = rtbAddress.Text;
                    comdet.Comp_ContactPerson = txtContactPerson.Text;
                    comdet.Comp_ContactPersonEmail = txtContactPersonEMail.Text;
                    comdet.Comp_FaxNo = txtFaxNumber.Text;
                    comdet.Comp_TelephoneNo = txtTelephoneNo.Text;

                    if (core.IsValueDidgit(btnLabels.Text))
                        comdet.Comp_TotalPowerN = Convert.ToInt32(btnLabels.Text);

                    try
                    {
                        context.SaveChanges();
                        cmbCompany.DataSource = context.TLADM_CompanyDetails.OrderBy(x => x.Comp_Name).ToList();
                        MessageBox.Show("Records stored to database successfully");
                        Setup();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnLabels_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if (oBtn != null && formLoaded)
            {

                frmTLADMGardProp depts = new frmTLADMGardProp(7000, Convert.ToInt32(oBtn.Text));
                depts.ShowDialog(this);
                oBtn.Text = depts.TotalPN.ToString();
            }

        }
    }
}
