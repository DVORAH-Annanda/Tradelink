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

namespace TTI2_WF
{
    public partial class frmTLADMWhseStores : Form
    {
     
        string[][] FldNames;
        bool[] FldEntered;
        bool addRecord;
        bool formloaded;
        Util core;
        //---------------------------------------------------------------------------------
        // if WhStore_WhseorStore bit set to true then the record is a warehouse otherwise the record is a store!!!!!! 
        //----------------------------------------------------------------------------------------------
        public frmTLADMWhseStores()
        {
            InitializeComponent();
            core = new Util();

            FldNames = new string[][]
            {   new string[] {"txtCode", "Please enter a valid code", "0", "7"},
                new string[] {"txtDescription", "Please enter a description", "1", "50"}, 
                new string[] {"txtAddress1", "Please enter an 1st Line address for this warehouse or store", "2", "50"},
                new string[] {"txtAddress2", "Please enter an 2nd Line address for this warehouse or store", "3", "50"},
                new string[] {"txtContact", "Please enter a contact name", "4", "50"},
                new string[] {"txtTelephone", "Please enter telephone number for this store", "5", "50"}, 
                new string[] {"txtEMail", "Please enter a email address for this store", "6", "50"}
            };
   
            Setup(true);
        }

        void Setup(bool updt)
        {
            formloaded = false;
            FldEntered = core.PopulateArray(FldNames.Length, false);

            using (var context = new TTI2Entities())
            {
                cmbWhse.DataSource = context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).ToList();
                cmbWhse.DisplayMember = "WhStore_Description";
                cmbWhse.ValueMember = "WhStore_Id";

                if (updt)
                {
                    cmbDepartment.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    cmbDepartment.DisplayMember = "Dep_Description";
                    cmbDepartment.ValueMember = "Dep_Id";
                    
                    cmbType.DataSource = context.TLADM_StoreTypes.OrderBy(x => x.StoreT_Description).ToList();
                    cmbType.ValueMember = "StoreT_Id";
                    cmbType.DisplayMember = "StoreT_Description";
                }
            }

            txtCode.Text        = string.Empty;
            txtDescription.Text = string.Empty;
            txtAddress1.Text    = string.Empty;
            txtAddress2.Text    = string.Empty;
            txtAddress3.Text    = string.Empty;
            txtAddress4.Text    = string.Empty;
            txtAddress5.Text    = string.Empty;
            txtEMail.Text       = string.Empty;
            txtTelephone.Text   = string.Empty;
            txtContact.Text     = string.Empty;
            txtCountryOfOrigin.Text = string.Empty;

            rbChemicalStoreNo.Checked = true;
            rbBundleStoreNo.Checked = true;
            rbPanelStoreNo.Checked = true;
            rbDyeKitchenNo.Checked = true;
            rbStore.Checked = true;
            rbGradeA.Checked = true;
            rbRepackNo.Checked = true;

            rctNotes.Text = string.Empty;

           

            if (cmbWhse.Items.Count == 0)
                addRecord = true;
            else
                addRecord = false;
                       
            cmbDepartment.SelectedValue = 0;
            cmbType.SelectedValue = 0;

            formloaded = true;
            
        }

        private void text_Changed(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null)
            {
                var result = (from u in FldNames
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    int length = Convert.ToInt32(result[3].ToString());
                    
                    if (oTxt.TextLength > 0 && oTxt.TextLength <= length)
                        FldEntered[nbr] = true;
                    else
                    {
                        FldEntered[nbr] = false;
                        if (oTxt.TextLength > length)
                        {
                            MessageBox.Show("Data entered exceeded maximum length");
                        }
                    }

                    if (nbr == 0 && addRecord)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLADM_WhseStore.Where(x => x.WhStore_Code == txtCode.Text).FirstOrDefault();
                            /*
                            if (Existing != null)
                            {
                                FldEntered[nbr] = false;
                                txtCode.Text = string.Empty;
                                MessageBox.Show("A record already exists for the code as entered");
                            }
                             */ 
                        }
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                Setup(false);
                addRecord = true;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool success = true;

            if (oBtn != null)
            {
                var errorM = returnMessage(FldEntered);
                if (!String.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                TLADM_WhseStore whse = new TLADM_WhseStore();
                using (var context = new TTI2Entities())
                {
                   if (!addRecord)
                   {
                       var whseSelected = (TLADM_WhseStore)cmbWhse.SelectedItem;
                       if (whseSelected != null)
                       {
                           whse = context.TLADM_WhseStore.Find(whseSelected.WhStore_Id);
                       }
                   }

                   whse.WhStore_Code = txtCode.Text;
                   whse.WhStore_Description = txtDescription.Text;
                   //--------------------------------------------------------------------
                   if (rbWarehouse.Checked)
                       whse.WhStore_WhseOrStore = true;
                   else
                       whse.WhStore_WhseOrStore = false;
                   //------------------------------------------------------------------------------------------
                   if (rbGradeA.Checked)
                       whse.WhStore_GradeA = true;
                   else
                       whse.WhStore_GradeA = false;

                   if (rbChemicalStoreYes.Checked)
                       whse.WhStore_ChemicalStore = true;
                   else
                       whse.WhStore_ChemicalStore = false;

                   if (rbBundleStoreYes.Checked)
                       whse.WhStore_BundleStore = true;
                   else
                       whse.WhStore_BundleStore = false;

                   if (rbPanelStoreYes.Checked)
                       whse.WhStore_PanelStore = true;
                   else
                       whse.WhStore_PanelStore = false;

                   if (rbDyeKitchYes.Checked)
                       whse.WhStore_DyeKitchen = true;
                   else
                       whse.WhStore_DyeKitchen = false;
                   if (rbRepacYes.Checked)
                       whse.WhStore_RePack = true;
                   else
                       whse.WhStore_RePack = false;


                   whse.WhStore_Address1 = txtAddress1.Text;
                   whse.WhStore_Address2 = txtAddress2.Text;
                   whse.WhStore_Address3 = txtAddress3.Text;
                   whse.WhStore_Address4 = txtAddress4.Text;
                   whse.WhStore_Address5 = txtAddress5.Text;
                   whse.WhStore_Contact = txtContact.Text;
                   whse.WhStore_Notes = rctNotes.Text;
                   whse.WhStore_Description = txtDescription.Text;
                   whse.WhStore_Contact = txtContact.Text;
                   whse.WhStore_Email = txtEMail.Text;
                   whse.WhStore_Telephone = txtTelephone.Text;
                   whse.WhStore_CountryOfOrigin = txtCountryOfOrigin.Text;

                   
                   var dept = (TLADM_Departments)cmbDepartment.SelectedItem;
                   if (dept != null)
                       whse.WhStore_DepartmentFK = dept.Dep_Id;
                   else
                       whse.WhStore_DepartmentFK = 1;

                   var storetypes = (TLADM_StoreTypes)cmbType.SelectedItem;
                   if (storetypes != null)
                       whse.WhStore_TypeFK = storetypes.StoreT_Id;
                   else
                       whse.WhStore_TypeFK = 1;

                   if(addRecord)
                   {
                       context.TLADM_WhseStore.Add(whse);
                   }
                   try
                   {
                       context.SaveChanges();
                       formloaded = false;
                       cmbWhse.DataSource = context.TLADM_WhseStore.OrderBy(x => x.WhStore_Code).ToList();
                       cmbWhse.SelectedValue = 0;

                       formloaded = true;
                       Setup(false);
                   }
                   catch (ArgumentException ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
                   catch (Exception ex)
                   {
                       var exceptionMessages = new StringBuilder();
                       do
                       {
                           exceptionMessages.Append(ex.Message);
                           ex = ex.InnerException;
                       }
                       while (ex != null);

                       MessageBox.Show(exceptionMessages.ToString());
                       success = false;
                   }
               }
                if (success)
                {
                    MessageBox.Show("Records stored to database sucessfully"); 
                }
           }

        }

        private void cmbWhse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var whseSelected = (TLADM_WhseStore)oCmb.SelectedItem;
                if(whseSelected  != null)
                {
                    addRecord = false;

                    using (var context = new TTI2Entities())
                    {
                        var whse = context.TLADM_WhseStore.Find(whseSelected.WhStore_Id);
                        txtCode.Text = whse.WhStore_Code;
                        txtDescription.Text = whse.WhStore_Description;
                        //--------------------------------------------------------------------
                        if(whse.WhStore_WhseOrStore)
                            rbWarehouse.Checked = true;
                        else
                            rbStore.Checked = true;
                        //------------------------------------------------------------------------------------------
                        if (whse.WhStore_GradeA)
                            rbGradeA.Checked = true;
                        else
                            rbGradeOther.Checked = true;

                        if (whse.WhStore_BundleStore)
                            rbBundleStoreYes.Checked = true;
                        
                        if (whse.WhStore_PanelStore)
                            rbPanelStoreYes.Checked = true;

                        if (whse.WhStore_DyeKitchen)
                            rbDyeKitchYes.Checked = true;

                        if (whse.WhStore_ChemicalStore)
                            rbChemicalStoreYes.Checked = true;
                        
                        if (whse.WhStore_RePack)
                             rbRepacYes.Checked = true;
                        else
                            rbRepackNo.Checked = false;
 

                        txtAddress1.Text = whse.WhStore_Address1;
                        txtAddress2.Text = whse.WhStore_Address2;
                        txtAddress3.Text = whse.WhStore_Address3;
                        txtAddress4.Text = whse.WhStore_Address4;
                        txtAddress5.Text = whse.WhStore_Address5;
                        txtContact.Text = whse.WhStore_Contact;
                        rctNotes.Text = whse.WhStore_Notes;
                        txtEMail.Text = whse.WhStore_Email;
                        txtCountryOfOrigin.Text = whse.WhStore_CountryOfOrigin;

                        txtTelephone.Text = whse.WhStore_Telephone;
                        cmbDepartment.SelectedValue = whse.WhStore_DepartmentFK;
                        cmbType.SelectedValue = whse.WhStore_TypeFK;
                        FldEntered = core.PopulateArray(FldNames.Length, true);
                    }
                }
            }
        }
    }
}
