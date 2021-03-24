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

        UserDetails _Uid;

        DataGridViewTextBoxColumn oTxtA;   // 0 Primary Key To TLSEC_UserAccess
        DataGridViewTextBoxColumn oTxtB;   // 1 Foreign Key TO TLSEC_Sections

        DataGridViewCheckBoxColumn oChkA;  // 2 Select Yes or No  
        DataGridViewTextBoxColumn oTxtC;   // 3 Description 

        DataTable dt;
        DataColumn Column;
        BindingSource BindingSrc;


        public frmUserAccess(UserDetails UserId)
        {
            InitializeComponent();
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            _Uid = UserId;

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
                chkDownSizeAuthority.Checked = false;
                txtHostName.Text = string.Empty;

                dt = new DataTable();
                Column = new DataColumn();


                dataGridView1.AutoGenerateColumns = false;
                //============================================================
                //---------Define the datatable 
                //=================================================================
                dt = new DataTable();
                DataColumn[] keys = new DataColumn[1];
              
                DataColumn column;
                BindingSrc = new BindingSource();

                //------------------------------------------------------
                // Create column 0. // This is the record index 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col0";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //------------------------------------------------------
                // Create column 1. // This is the record index 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Col1";
                column.DefaultValue = 0;
                dt.Columns.Add(column);
                keys[0] = column;
                dt.PrimaryKey = keys;

                //------------------------------------------------------
                // Create column 2. // This is the record index 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Boolean);
                column.ColumnName = "col2";
                column.Caption = "Select";
                column.DefaultValue = false;
                dt.Columns.Add(column);

                //------------------------------------------------------
                // Create column 3. // This is the whether the record is discontinued or not 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Col3";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                dt.Columns.Add(column);

                oTxtA = new DataGridViewTextBoxColumn();  
                oTxtA.Name = "PrimKey";
                oTxtA.ValueType = typeof(int);
                oTxtA.Visible = false;
                oTxtA.DataPropertyName = dt.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtA);
                dataGridView1.Columns["PrimKey"].DisplayIndex = 0;

                oTxtB = new DataGridViewTextBoxColumn(); 
                oTxtB.Name = "ForeignKey";
                oTxtB.ReadOnly = true;
                oTxtB.DataPropertyName = dt.Columns[1].ColumnName;
                oTxtB.ValueType = typeof(int);
                oTxtB.Visible = false;
                dataGridView1.Columns.Add(oTxtB);
                dataGridView1.Columns["ForeignKey"].DisplayIndex = 1;

                oChkA = new DataGridViewCheckBoxColumn();  
                oChkA.Name = "Select";
                oChkA.HeaderText = "Select";
                oChkA.ValueType = typeof(Boolean);
                oChkA.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkA);
                dataGridView1.Columns["Select"].DisplayIndex = 2;

                oTxtC = new DataGridViewTextBoxColumn();   
                oTxtC.Name = "Descrip";
                oTxtC.ReadOnly = true;
                oTxtC.ValueType = typeof(string);
                oTxtC.DataPropertyName = dt.Columns[3].ColumnName;
                oTxtC.HeaderText = "Access Description";
                oTxtC.Width = 285; 
                oTxtC.Visible = true;
                dataGridView1.Columns.Add(oTxtC); 
                dataGridView1.Columns["Descrip"].DisplayIndex = 3;

            }
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0 || idx == 1)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;

            }

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;

            cmboCurrentUsers.Visible = false;

            this.Text = "Grant user access - Input Mode";

            formloaded = true;
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dt.Rows.Clear();
                var selected = (TLSEC_Departments)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Department_FK == selected.TLSECDT_Pk).OrderBy(x=>x.TLSECSect_Description).ToList();
                        foreach (var row in Sections)
                        {
                            DataRow dr = dt.NewRow();

                            dr[0] = 0;
                            dr[1] = row.TLSECSect_Pk;
                            dr[2] = false;
                            dr[3] = row.TLSECSect_Description;

                            dt.Rows.Add(dr);
                        }

                         var User = (TLSEC_UserAccess)cmboCurrentUsers.SelectedItem;
                         if (User != null)
                         {
                             var Existing = context.TLSEC_UserSections.Where(x => x.TLSECDEP_User_FK == User.TLSECUA_Pk && x.TLSECDEP_Department_FK == selected.TLSECDT_Pk).ToList();

                             foreach (var row in Existing)
                             {
                                var ExistRec = dt.Rows.Find(row.TLSECDEP_Section_FK);
                                if(ExistRec != null)
                                {
                                    ExistRec[0] = row.TLSECDEP_Pk;
                                    ExistRec[2] = (bool)row.TLSECDEP_AccessGranted;  
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

        private void ResetTextFields()
        {
            txtDiscontinuedReason.Text = string.Empty;
            txtEMail_Address.Text = string.Empty;
            txtHostName.Text = string.Empty;
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLSEC_UserAccess user = null;
            TLSEC_Departments Dept = null;

            if (oBtn != null && formloaded)
            {
                if (String.IsNullOrEmpty(txtHostName.Text))
                {
                    MessageBox.Show("Please enter a User Name");
                    return;
                }

                Dept = (TLSEC_Departments)cmboDepartments.SelectedItem;

                if (EditMode && !chkDiscontinue.Checked && !chkExternalUser.Checked && !chkQAFunction.Checked && !chkResetPassword.Checked && !chkSuperUser.Checked)
                {
                    if (Object.Equals(Dept, null))
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
                        user.TLSECUA_DownSizeAuthority = chkDownSizeAuthority.Checked;

                        context.TLSEC_UserAccess.Add(user);
                        try
                        {
                            context.SaveChanges();
                            btnSearch_Click(btnSearch, null);
                            cmboCurrentUsers.SelectedValue = user.TLSECUA_Pk;
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

                        user = context.TLSEC_UserAccess.Find(user.TLSECUA_Pk);

                        user.TLSECUA_SuperUser = chkSuperUser.Checked;
                        user.TLSECUA_External = chkExternalUser.Checked;
                        user.TLSUCUA_EmailAddress = txtEMail_Address.Text;
                        user.TLSECUA_QAFunction = chkQAFunction.Checked;
                        user.TLSECUA_DownSizeAuthority = chkDownSizeAuthority.Checked;

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

                        try
                        {
                            context.SaveChanges();
                            cmboCurrentUsers.SelectedValue = user.TLSECUA_Pk;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        TLSEC_UserSections userSec = new TLSEC_UserSections();
                        bool SecAdd = true;

                        if (row.Field<bool>(2) == false)
                        {
                            continue;
                        }

                        if (row.Field<int>(0) != 0)
                        {
                            userSec = context.TLSEC_UserSections.Find(row.Field<int>(0));
                            SecAdd = false;
                        }

                        userSec.TLSECDEP_AccessGranted = row.Field<bool>(2);
                        userSec.TLSECDEP_User_FK = user.TLSECUA_Pk;
                        userSec.TLSECDEP_Section_FK = row.Field<int>(1);
                        userSec.TLSECDEP_Department_FK = Dept.TLSECDT_Pk;


                        if (SecAdd)
                            context.TLSEC_UserSections.Add(userSec);

                    }
                    

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        dt.Rows.Clear();
                        if (!EditMode)
                            txtHostName.Text = string.Empty;

                        chkDiscontinue.Checked = false;
                        chkResetPassword.Checked = false;
                        chkExternalUser.Checked = false;
                        chkSuperUser.Checked = false;
                        chkQAFunction.Checked = false;
                        ResetTextFields();
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
                    txtHostName.Focus();
                    dt.Rows.Clear();
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
                    using (var context = new TTI2Entities())
                    {
                        var UserDet = context.TLSEC_UserAccess.Find(User.TLSECUA_Pk);
                        if (UserDet != null)
                        {
                            txtHostName.Text = UserDet.TLSECUA_UserName;
                            chkSuperUser.Checked = UserDet.TLSECUA_SuperUser;
                            chkExternalUser.Checked = UserDet.TLSECUA_External;
                            txtEMail_Address.Text = UserDet.TLSUCUA_EmailAddress;
                            chkDiscontinue.Checked = UserDet.TLSECUA_Discontinued;
                            chkQAFunction.Checked = UserDet.TLSECUA_QAFunction;
                            txtDiscontinuedReason.Text = UserDet.TLSECUA_Reason;
                        }
                    }
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
