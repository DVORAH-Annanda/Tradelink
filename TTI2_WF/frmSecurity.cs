using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace TTI2_WF
{
    public partial class frmSecurity : Form
    {
        bool formloaded;
        bool ConfirmPassword;
        public bool IsAuthorised;
        public string UserName;
        public bool IsSuperUser;
        public bool IsExternal;
        public bool QAFunction;
        public int UserPk;
        public bool DownSizeAllowed;

        public frmSecurity()
        {
            InitializeComponent();
            // Set the maximum length of text in the control to eight.
            txtUserPassword.MaxLength = 10;
            // Assign the asterisk to be the password character.
            txtUserPassword.PasswordChar = '*';
            // Align the text in the center of the TextBox control.
            txtUserPassword.TextAlign = HorizontalAlignment.Left;

            // Set the maximum length of text in the control to eight.
            txtConfirmPassword.MaxLength = 10;
            // Assign the asterisk to be the password character.
            txtConfirmPassword.PasswordChar = '*';
            // Align the text in the center of the TextBox control.
            txtUserPassword.TextAlign = HorizontalAlignment.Left;

            label3.Visible = false;
            txtConfirmPassword.Visible = false;

        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null & formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    if (ConfirmPassword)
                    {
                        String Password = txtUserPassword.Text.Replace('*', ' ');
                        if (Password.Length == 0)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("Please enter a valid password");
                            }
                            return;
                        }
                        String Confirm = txtConfirmPassword.Text.Replace('*', ' ');
                        if (Confirm.Length == 0)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("Please enter a validation password");
                            }
                            return;
                        }

                        IsAuthorised = String.Equals(Password.Trim(), Confirm.Trim(), StringComparison.CurrentCulture);
                        if (IsAuthorised)
                        {
                            var SelectedUser = (TLSEC_UserAccess)cmboUserDetails.SelectedItem;
                            if (SelectedUser != null)
                            {
                                var User = context.TLSEC_UserAccess.Where(x => x.TLSECUA_UserName == SelectedUser.TLSECUA_UserName).FirstOrDefault();
                                if (User != null)
                                {
                                    User.TLSECUA_ConfirmedPassword = true;
                                    User.TLSECUA_UserPassword = Password;

                                    UserName = User.TLSECUA_UserName;
                                    IsSuperUser = User.TLSECUA_SuperUser;
                                    UserPk = User.TLSECUA_Pk;
                                    IsExternal = User.TLSECUA_External;
                                    QAFunction = User.TLSECUA_QAFunction;
                                    DownSizeAllowed = User.TLSECUA_DownSizeAuthority;
                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                                
                            }
                            this.Close();
                        }
                        else
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("Password not confirmed. Please reconfirm");
                            }
                            return;
                        }
                    }
                    else
                    {
                        var SelectedUser = (TLSEC_UserAccess)cmboUserDetails.SelectedItem;
                        if (SelectedUser != null)
                        {
                            var User = context.TLSEC_UserAccess.Where(x => x.TLSECUA_UserName == SelectedUser.TLSECUA_UserName).FirstOrDefault();
                            if (User != null)
                            {
                                 String Password = txtUserPassword.Text.Replace('*', ' ');
                                 String Confirm = User.TLSECUA_UserPassword;

                                 IsAuthorised = String.Equals(Password.Trim(), Confirm.Trim(), StringComparison.CurrentCulture);
                                 if (IsAuthorised)
                                 {
                                    UserName = User.TLSECUA_UserName;
                                    IsSuperUser = User.TLSECUA_SuperUser;
                                    UserPk = User.TLSECUA_Pk;
                                    IsExternal = User.TLSECUA_External;
                                    DownSizeAllowed = User.TLSECUA_DownSizeAuthority; 
                                     this.Close();
                                 }
                                 else
                                 {
                                     using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                     {
                                         MessageBox.Show("Please re-enter password", "Invalid Password for " + User.TLSECUA_UserName);
                                     }
                                     return;
                                 }
                            }
                            
                        }
                    }
                }
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {
            formloaded = true;
            ConfirmPassword = false;
            
            using (var context = new TTI2Entities())
            {
                cmboUserDetails.DataSource = context.TLSEC_UserAccess.Where(x => !x.TLSECUA_Discontinued).OrderBy(x => x.TLSECUA_UserName).ToList();
                cmboUserDetails.ValueMember = "TLSECUA_Pk";
                cmboUserDetails.DisplayMember = "TLSECUA_UserName";
                cmboUserDetails.SelectedValue = -1;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    /*
                    if (String.IsNullOrEmpty(txtUserName.Text))
                    {
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("Please enter a valid User name");
                        }
                        return;
                    }

                    var User = context.TLSEC_UserAccess.Where(x => x.TLSECUA_UserName == oTxt.Text).FirstOrDefault();
                    if (User == null)
                    {
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("User not registered");
                        }

                        this.Close();
                    }
                    else
                    {
                        if (!User.TLSECUA_ConfirmedPassword)
                        {
                            label3.Visible = true;
                            txtConfirmPassword.Visible = true;
                            txtConfirmPassword.Text = String.Empty;
                            ConfirmPassword = true;
                        }
                    }
                     * */
                }
            }
        }

        private void txtUserPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               btnAccess.PerformClick();
            }
        }

        private void cmboUserDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null)
            {
                var SelectedUser = (TLSEC_UserAccess)oCmbo.SelectedItem;
                if (SelectedUser != null)
                {
                    if (!SelectedUser.TLSECUA_ConfirmedPassword)
                    {
                        label3.Visible = true;
                        txtConfirmPassword.Visible = true;
                        txtConfirmPassword.Text = String.Empty;
                        ConfirmPassword = true;
                    }
                    else
                    {
                        label3.Visible = false;
                        txtConfirmPassword.Visible = false;
                        ConfirmPassword = false;
                       
                    }
                }
            }
        }
    }
}
