namespace Security
{
    partial class frmUserAccess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboDepartments = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmboCurrentUsers = new System.Windows.Forms.ComboBox();
            this.chkSuperUser = new System.Windows.Forms.CheckBox();
            this.chkResetPassword = new System.Windows.Forms.CheckBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDiscontinue = new System.Windows.Forms.CheckBox();
            this.txtDiscontinuedReason = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkQAFunction = new System.Windows.Forms.CheckBox();
            this.txtEMail_Address = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkExternalUser = new System.Windows.Forms.CheckBox();
            this.chkDownSizeAuthority = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // txtHostName
            // 
            this.txtHostName.Location = new System.Drawing.Point(211, 27);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(189, 20);
            this.txtHostName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Departments";
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(211, 73);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(189, 21);
            this.cmboDepartments.TabIndex = 3;
            this.cmboDepartments.SelectedIndexChanged += new System.EventHandler(this.cmboDepartments_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(81, 300);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(449, 347);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(545, 629);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(416, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Users";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmboCurrentUsers
            // 
            this.cmboCurrentUsers.FormattingEnabled = true;
            this.cmboCurrentUsers.Location = new System.Drawing.Point(211, 25);
            this.cmboCurrentUsers.Name = "cmboCurrentUsers";
            this.cmboCurrentUsers.Size = new System.Drawing.Size(189, 21);
            this.cmboCurrentUsers.TabIndex = 7;
            this.cmboCurrentUsers.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentUsers_SelectedIndexChanged);
            // 
            // chkSuperUser
            // 
            this.chkSuperUser.AutoSize = true;
            this.chkSuperUser.Location = new System.Drawing.Point(6, 23);
            this.chkSuperUser.Name = "chkSuperUser";
            this.chkSuperUser.Size = new System.Drawing.Size(79, 17);
            this.chkSuperUser.TabIndex = 4;
            this.chkSuperUser.Text = "Super User";
            this.chkSuperUser.UseVisualStyleBackColor = true;
            // 
            // chkResetPassword
            // 
            this.chkResetPassword.AutoSize = true;
            this.chkResetPassword.Location = new System.Drawing.Point(242, 23);
            this.chkResetPassword.Name = "chkResetPassword";
            this.chkResetPassword.Size = new System.Drawing.Size(103, 17);
            this.chkResetPassword.TabIndex = 5;
            this.chkResetPassword.Text = "Reset Password";
            this.chkResetPassword.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(545, 590);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 8;
            this.btnReport.Text = "Reports";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Discontinued Reason";
            // 
            // chkDiscontinue
            // 
            this.chkDiscontinue.AutoSize = true;
            this.chkDiscontinue.Location = new System.Drawing.Point(110, 23);
            this.chkDiscontinue.Name = "chkDiscontinue";
            this.chkDiscontinue.Size = new System.Drawing.Size(107, 17);
            this.chkDiscontinue.TabIndex = 10;
            this.chkDiscontinue.Text = "Discontinue User";
            this.chkDiscontinue.UseVisualStyleBackColor = true;
            // 
            // txtDiscontinuedReason
            // 
            this.txtDiscontinuedReason.Location = new System.Drawing.Point(269, 227);
            this.txtDiscontinuedReason.Name = "txtDiscontinuedReason";
            this.txtDiscontinuedReason.Size = new System.Drawing.Size(228, 20);
            this.txtDiscontinuedReason.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDownSizeAuthority);
            this.groupBox1.Controls.Add(this.chkQAFunction);
            this.groupBox1.Controls.Add(this.txtEMail_Address);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkExternalUser);
            this.groupBox1.Controls.Add(this.chkDiscontinue);
            this.groupBox1.Controls.Add(this.chkResetPassword);
            this.groupBox1.Controls.Add(this.chkSuperUser);
            this.groupBox1.Location = new System.Drawing.Point(39, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 111);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // chkQAFunction
            // 
            this.chkQAFunction.AutoSize = true;
            this.chkQAFunction.Location = new System.Drawing.Point(484, 23);
            this.chkQAFunction.Name = "chkQAFunction";
            this.chkQAFunction.Size = new System.Drawing.Size(85, 17);
            this.chkQAFunction.TabIndex = 14;
            this.chkQAFunction.Text = "QA Function";
            this.chkQAFunction.UseVisualStyleBackColor = true;
            // 
            // txtEMail_Address
            // 
            this.txtEMail_Address.Location = new System.Drawing.Point(119, 74);
            this.txtEMail_Address.Name = "txtEMail_Address";
            this.txtEMail_Address.Size = new System.Drawing.Size(405, 20);
            this.txtEMail_Address.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "EMail Address";
            // 
            // chkExternalUser
            // 
            this.chkExternalUser.AutoSize = true;
            this.chkExternalUser.Location = new System.Drawing.Point(370, 23);
            this.chkExternalUser.Name = "chkExternalUser";
            this.chkExternalUser.Size = new System.Drawing.Size(89, 17);
            this.chkExternalUser.TabIndex = 11;
            this.chkExternalUser.Text = "External User";
            this.chkExternalUser.UseVisualStyleBackColor = true;
            // 
            // chkDownSizeAuthority
            // 
            this.chkDownSizeAuthority.AutoSize = true;
            this.chkDownSizeAuthority.Location = new System.Drawing.Point(6, 46);
            this.chkDownSizeAuthority.Name = "chkDownSizeAuthority";
            this.chkDownSizeAuthority.Size = new System.Drawing.Size(129, 17);
            this.chkDownSizeAuthority.TabIndex = 15;
            this.chkDownSizeAuthority.Text = "Down Sizing Authority";
            this.chkDownSizeAuthority.UseVisualStyleBackColor = true;
            // 
            // frmUserAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 659);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDiscontinuedReason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.cmboCurrentUsers);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHostName);
            this.Controls.Add(this.label1);
            this.Name = "frmUserAccess";
            this.Text = "Grant User Access";
            this.Load += new System.EventHandler(this.frmUserAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboDepartments;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmboCurrentUsers;
        private System.Windows.Forms.CheckBox chkSuperUser;
        private System.Windows.Forms.CheckBox chkResetPassword;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDiscontinue;
        private System.Windows.Forms.TextBox txtDiscontinuedReason;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkExternalUser;
        private System.Windows.Forms.TextBox txtEMail_Address;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkQAFunction;
        private System.Windows.Forms.CheckBox chkDownSizeAuthority;
    }
}