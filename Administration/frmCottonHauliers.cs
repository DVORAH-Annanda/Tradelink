using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTI2_WF;
using Utilities;

namespace Administration
{
    public partial class frmCottonHauliers : Form
    {
        string[][] MandatoryFields;
        bool[] MandSelected;

        bool formloaded;
        bool addRec;
        Util core;

        public frmCottonHauliers()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
                {   new string[] {"txtHaulierNo", "Please enter a haulier no", "0"},
                    new string[] {"txtHaulierName", "Please enter a haulier name", "1"}
                };

            txtHaulierTelephone.KeyPress += core.txtWin_KeyPress;
            txtHaulierTelephone.KeyDown  += core.txtWin_KeyDown;

            Setup();
        }

        void Setup()
        {
            formloaded = false;
            addRec = true;

            using (var context = new TTI2Entities())
            {
                cmbHauliers.DataSource = context.TLADM_CottonHauliers.OrderBy(x => x.Haul_No).ToList();
                cmbHauliers.ValueMember = "Haul_Pk";
                cmbHauliers.DisplayMember = "Haul_Description";
            }

            txtHaulierNo.Text            = string.Empty;
            txtHaulierContactPerson.Text = string.Empty;
            txtHaulierName.Text          = string.Empty;
            txtHaulierTelephone.Text     = string.Empty;
            rtbAddress.Text              = string.Empty;

            cmbHauliers.SelectedValue = 0;

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
        }

        private void txtBox_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTBox = sender as TextBox;
            if (oTBox != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTBox.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    if (oTBox.TextLength > 0)
                        MandSelected[nbr] = true;
                    else
                        MandSelected[nbr] = false;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                Setup();
            }
        }

        private void cmbHauliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                addRec = false;
                var Existing = (TLADM_CottonHauliers)cmbHauliers.SelectedItem;

                if (Existing != null)
                {
                    txtEmailAddress.Text         = Existing.Haul_EMailAddress;
                    txtHaulierContactPerson.Text = Existing.Haul_ContactPerson;
                    txtHaulierName.Text          = Existing.Haul_Description;
                    txtHaulierNo.Text            = Existing.Haul_No;
                    txtHaulierTelephone.Text     = Existing.Haul_TelephoneNo.ToString();
                    rtbAddress.Text              = Existing.Haul_Address;

                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                TLADM_CottonHauliers ch = new TLADM_CottonHauliers();
                using ( var context = new TTI2Entities())
                {

                    var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                    if (!string.IsNullOrEmpty(errorM))
                    {
                        MessageBox.Show(errorM);
                        return;
                    }

                    if (!addRec)
                    {
                        var Selected = (TLADM_CottonHauliers)cmbHauliers.SelectedItem;
                        if (Selected != null)
                            ch = context.TLADM_CottonHauliers.Find(Selected.Haul_Pk);
                    }


                    ch.Haul_Address = rtbAddress.Text;
                    ch.Haul_ContactPerson = txtHaulierContactPerson.Text;
                    ch.Haul_Description = txtHaulierName.Text;
                    ch.Haul_EMailAddress = txtEmailAddress.Text;
                    ch.Haul_No = txtHaulierNo.Text;
                    if(!string.IsNullOrEmpty(txtHaulierTelephone.Text))
                      ch.Haul_TelephoneNo = Convert.ToInt32(txtHaulierTelephone.Text);

                    if (addRec)
                        context.TLADM_CottonHauliers.Add(ch);

                    try
                    {
                        context.SaveChanges();
                        Setup();
                        formloaded = false;
                        cmbHauliers.DataSource = context.TLADM_CottonHauliers.OrderBy(x => x.Haul_No).ToList();
                        formloaded = true;
                        MessageBox.Show("Records stored to database successfully");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLADM_CottonHauliers)cmbHauliers.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a haulier from the drop down box provided");
                    return;
                }

                frmCottonHauliersVehicles hv = new frmCottonHauliersVehicles(selected.Haul_Pk, selected.Haul_Description);
                hv.ShowDialog(this);
            }
        }
    }
}
