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

namespace Administration
{
    public partial class frmTLADM_AdditionalAddress : Form
    {
        bool formloaded;
        bool WhoIsIt;
        int ParentKey;

        //---------------------------------------------------------------------
        // If IsCustomer is True then obviously it is a customer else it will be a supplier 
        /// <summary>
     
        /// </summary>
        /// <param name="FK"> Will either be a customer or Supplier FK </param>
        /// <param name="IsCustomer"></param>
        public frmTLADM_AdditionalAddress(int FK, bool IsCustomer)
        {
            InitializeComponent();
            SetUp(FK, IsCustomer);
        }

        void SetUp(int key, bool whoisit)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var ExistingData = context.TLADM_AdditionalAddress
                                   .Where(x => x.Addit_FK == key && x.Addit_IsCustomer == whoisit).ToList();

                cmbAddAddress.DataSource = ExistingData;
                cmbAddAddress.ValueMember = "Addit_Pk";
                cmbAddAddress.DisplayMember = "Addit_Description";
                
            }

            txtDescription.Text = string.Empty;
            rtbAddress.Text = string.Empty;
            rtbNotes.Text = string.Empty;

            WhoIsIt = whoisit;
            ParentKey = key;

            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool New_Record = true;

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description");
                return;
            }

            if(string.IsNullOrEmpty(rtbAddress.Text))
            {
                MessageBox.Show("Please enter a address");
                return;
            }

            TLADM_AdditionalAddress addAddress = new TLADM_AdditionalAddress();
 
            var SelectedAddress = (TLADM_AdditionalAddress)cmbAddAddress.SelectedItem;
            if(SelectedAddress != null)
            {
                if (String.Compare(SelectedAddress.Addit_Description, txtDescription.Text) == 0)
                {
                    New_Record = false;
                }
            }

            using (var context = new TTI2Entities())
            {
                if (!New_Record)
                    addAddress = context.TLADM_AdditionalAddress.Find(SelectedAddress.Addit_Pk);

                addAddress.Addit_Description = txtDescription.Text;
                addAddress.Addit_IsCustomer = WhoIsIt;
                addAddress.Addit_FK = ParentKey;
                addAddress.Addit_Notes = rtbNotes.Text;
                addAddress.Addit_Address = rtbAddress.Text;
                
                if (New_Record)
                    context.TLADM_AdditionalAddress.Add(addAddress);

                try
                {
                    context.SaveChanges();
                    cmbAddAddress.DataSource = context.TLADM_AdditionalAddress
                                  .Where(x => x.Addit_FK == ParentKey && x.Addit_IsCustomer == WhoIsIt).ToList();

                    MessageBox.Show("Records updated to database successfully");
                    SetUp(ParentKey, WhoIsIt);

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException eX)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var eve in eX.EntityValidationErrors)
                    {
                       // Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                       //     eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.Append("- Property: " + ve.PropertyName + "Error: " + ve.ErrorMessage); 
                        }
                    }
                    
                    MessageBox.Show(sb.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


        }

        private void cmbAddAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbBx = sender as ComboBox;
            if (oCmbBx != null)
            {
                var SelectedAddress = (TLADM_AdditionalAddress)cmbAddAddress.SelectedItem;
                if (SelectedAddress != null)
                {
                    txtDescription.Text = SelectedAddress.Addit_Description;
                    rtbAddress.Text = SelectedAddress.Addit_Address;
                    rtbNotes.Text = SelectedAddress.Addit_Notes;

                }
            }
        }

    }
}
