using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Security
{
    public partial class frmUserAccess : Form
    {
        bool formloaded;
        bool EditMode;

        DataGridViewTextBoxColumn oTxtA;   // 0 Primary Key To TLSEC_UserAccess
        DataGridViewTextBoxColumn oTxtB;   // 1 Foreign Key TO TLSEC_Sections

        DataGridViewCheckBoxColumn oChkA;  // 2 Select Yes or No  
        DataGridViewTextBoxColumn oTxtC;   // 3 Description 
      

        public frmUserAccess()
        {
            InitializeComponent();
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);

        }

        private void frmUserAccess_Load(object sender, EventArgs e)
        {
            formloaded = false;
            EditMode = false;

            label2.Visible = false;
            txtDiscontinuedReason.Text = string.Empty;
            txtDiscontinuedReason.Visible = false;
            txtEMail_Address.Text = string.Empty;

            using (var context = new TTI2Entities())
            {
                cmboDepartments.DataSource = context.TLSEC_Departments.ToList();
                cmboDepartments.ValueMember = "TLSECDT_Pk";
                cmboDepartments.DisplayMember = "TLSECDT_Description";
                cmboDepartments.SelectedIndex = -1;

                chkResetPassword.Checked = false;
                chkSuperUser.Checked = false;
                chkExternalUser.Checked = false;
                chkQAFunction.Checked = false;

                txtHostName.Text = string.Empty;
                
                oTxtA = new DataGridViewTextBoxColumn(); // 0 Primary Key To TLSEC_UserAccess 
                oTxtA.ValueType = typeof(int);
                oTxtA.Visible = false;
                dataGridView1.Columns.Add(oTxtA);

                oTxtB = new DataGridViewTextBoxColumn();  // 1 Foreign Key To TLSEC_Sections
                oTxtB.ReadOnly = true;
                oTxtB.ValueType = typeof(int);
                oTxtB.Visible = false;
                dataGridView1.Columns.Add(oTxtB);

                oChkA = new DataGridViewCheckBoxColumn();  // 2
                oChkA.HeaderText = "Select";
                oChkA.ValueType = typeof(Boolean);
                dataGridView1.Columns.Add(oChkA);

                oTxtC = new DataGridViewTextBoxColumn();   // 3
                oTxtC.ReadOnly = true;
                oTxtC.ValueType = typeof(string);
                oTxtC.HeaderText = "Access Description";
                oTxtC.Width = 230; 
                oTxtC.Visible = true;
                dataGridView1.Columns.Add(oTxtC);

            }
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            cmboCurrentUsers.Visible = false;

            this.Text = "Grant user access - Input Mode";

            formloaded = true;
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                var selected = (TLSEC_Departments)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Department_FK == selected.TLSECDT_Pk).OrderBy(x=>x.TLSECSect_Description).ToList();
                        foreach (var row in Sections)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = 0;
                            dataGridView1.Rows[index].Cells[1].Value = row.TLSECSect_Pk;
                            dataGridView1.Rows[index].Cells[2].Value = false;
                            dataGridView1.Rows[index].Cells[3].Value = row.TLSECSect_Description;
                        }

                         var User = (TLSEC_UserAccess)cmboCurrentUsers.SelectedItem;
                         if (User != null)
                         {
                             var Existing = context.TLSEC_UserSections.Where(x => x.TLSECDEP_User_FK == User.TLSECUA_Pk && x.TLSECDEP_Department_FK == selected.TLSECDT_Pk).ToList();

                             foreach (var row in Existing)
                             {
                                 var SingleRow = (
                                            from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                            where (int)Rows.Cells[1].Value == row.TLSECDEP_Section_FK
                                            select Rows).FirstOrDefault();
                                 if (SingleRow != null)
                                 {
                                     SingleRow.Cells[2].Value = row.TLSECDEP_AccessGranted;
                                     SingleRow.Cells[0].Value = row.TLSECDEP_Pk;
                                 }
                             }
                         }
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (!(bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    var Index = (int)CurrentRow.Cells[0].Value;
                    if (Index != 0)
                    {
                       using ( var context = new TTI2Entities())
                       {
                           var Existing = context.TLSEC_UserSections.Find(Index);
                           if (Existing != null)
                           {
                               context.TLSEC_UserSections.Remove(Existing);
                               try
                               {
                                   context.SaveChanges();
                               }
                               catch (Exception ex)
                               {
                                   MessageBox.Show(ex.Message);
                               }
                           }
                       }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLSEC_UserAccess user = null;
            TLSEC_Departments Dept = null;

            if(oBtn != null && formloaded)
            {
                if (String.IsNullOrEmpty(txtHostName.Text)) 
                {
                    MessageBox.Show("Please enter a User Name");
                    return;
                }

                if (!chkResetPassword.Checked && !chkSuperUser.Checked)
                {
                    Dept = (TLSEC_Departments)cmboDepartments.SelectedItem;
                    if (Dept == null)
                    {
                        MessageBox.Show("Please select a valid department");
                        return;
                    }
                }

                using (var context = new TTI2Entities())
                {
                    //----------------------------------------------------------
                    // Add User First..This becomes Master Record
                    //----------------------------------------------------------------
                    if (!EditMode)
                    {
                        user = new TLSEC_UserAccess();

                        user.TLSECUA_UserName = txtHostName.Text;
                        user.TLSECUA_UserPassword = "Not Yet";
                        user.TLSECUA_SuperUser = chkSuperUser.Checked;
                        user.TLSECUA_External = chkExternalUser.Checked;
                        user.TLSUCUA_EmailAddress = txtEMail_Address.Text;
                        user.TLSECUA_QAFunction = chkQAFunction.Checked;

                        context.TLSEC_UserAccess.Add(user);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    else
                    {
                        user = (TLSEC_UserAccess)cmboCurrentUsers.SelectedItem;
                        if (user != null)
                        {
                            user = context.TLSEC_UserAccess.Find(user.TLSECUA_Pk);

                            user.TLSECUA_SuperUser = chkSuperUser.Checked;
                            user.TLSECUA_External = chkExternalUser.Checked;
                            user.TLSUCUA_EmailAddress = txtEMail_Address.Text;
                            user.TLSECUA_QAFunction = chkQAFunction.Checked;

                            if (chkResetPassword.Checked)
                            {
                                user.TLSECUA_ConfirmedPassword = false;
                                user.TLSECUA_UserPassword = string.Empty;

                            }
                            else
                            {
                                if (chkDiscontinue.Checked && !user.TLSECUA_Discontinued)
                                {
                                    user.TLSECUA_Discontinued = true;
                                    user.TLSECUA_DisDate = DateTime.Now;
                                    user.TLSECUA_Reason = txtDiscontinuedReason.Text;
                                }
                            }

                            if (!chkDiscontinue.Checked)
                            {
                                user.TLSECUA_Discontinued = false;
                                user.TLSECUA_DisDate = null;
                                user.TLSECUA_Reason = string.Empty;
                            }
                        }

                        if (!chkResetPassword.Checked && !chkSuperUser.Checked)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                TLSEC_UserSections userSec = new TLSEC_UserSections();
                                bool SecAdd = true;

                                if ((bool)row.Cells[2].Value == false)
                                    continue;


                                if ((int)row.Cells[0].Value != 0)
                                {
                                    var Pk = (int)row.Cells[0].Value;
                                    userSec = context.TLSEC_UserSections.Find(Pk);
                                    if (userSec == null)
                                        userSec = new TLSEC_UserSections();
                                    else
                                        SecAdd = false;
                                }

                                userSec.TLSECDEP_AccessGranted = (bool)row.Cells[2].Value;
                                userSec.TLSECDEP_User_FK = user.TLSECUA_Pk;
                                userSec.TLSECDEP_Section_FK = (int)row.Cells[1].Value;
                                userSec.TLSECDEP_Department_FK = Dept.TLSECDT_Pk;
                              
                                
                                if (SecAdd)
                                    context.TLSEC_UserSections.Add(userSec);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to the database");
                            dataGridView1.Rows.Clear();
                            if (!EditMode)
                                txtHostName.Text = string.Empty;

                            chkDiscontinue.Checked = false;
                            chkResetPassword.Checked = false;
                            chkExternalUser.Checked = false;
                            chkSuperUser.Checked = false;
                            chkQAFunction.Checked = false;
                            txtEMail_Address.Text = string.Empty;
                            cmboCurrentUsers.SelectedValue = -1;
                            cmboDepartments.SelectedValue = -1;

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
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
               
                    formloaded = false;
                    cmboCurrentUsers.DataSource = context.TLSEC_UserAccess.OrderBy(x => x.TLSECUA_UserName).ToList();
                    cmboCurrentUsers.ValueMember = "TLSECUA_Pk";
                    cmboCurrentUsers.DisplayMember = "TLSECUA_UserName";
                    cmboCurrentUsers.SelectedIndex = -1;
                    formloaded = true;
                }

                EditMode = !EditMode;
                if (EditMode)
                {
                    cmboCurrentUsers.Visible = true;
                    txtHostName.Visible = false;

                   this.Text = "Grant user access - Edit Mode";
                }
                else
                {
                    cmboCurrentUsers.Visible = false;
                    txtHostName.Visible = true;
                    cmboDepartments.SelectedIndex = -1;
                    dataGridView1.Rows.Clear();
                    txtHostName.Focus();

                    txtHostName.Text = string.Empty;
                    this.Text = "Grant user access - Input Mode";
                }
            }
        }

        private void cmboCurrentUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var User = (TLSEC_UserAccess)oCmbo.SelectedItem;
                if(User != null)
                {
                    txtHostName.Text = User.TLSECUA_UserName;
                    chkSuperUser.Checked = User.TLSECUA_SuperUser;
                    chkExternalUser.Checked = User.TLSECUA_External;
                    txtEMail_Address.Text = User.TLSUCUA_EmailAddress;  
                    chkDiscontinue.Checked = User.TLSECUA_Discontinued;
                    chkQAFunction.Checked = User.TLSECUA_QAFunction;
                    txtDiscontinuedReason.Text = User.TLSECUA_Reason;
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                using (frmRepSel ReportSel = new frmRepSel())
                {
                    DialogResult dr = ReportSel.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }
                // frmViewSecurity VRep = new frmViewSecurity(1);
                // VRep.ShowDialog();
            }

        }
    }
}
