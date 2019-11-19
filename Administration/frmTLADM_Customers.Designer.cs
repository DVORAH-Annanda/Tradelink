namespace TTI2_WF
{
    partial class frmTLADM_Customers
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbSelectCustomers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbPostalAddress = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTelephoneNo = new System.Windows.Forms.TextBox();
            this.txtFaxNo = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEMailAddress = new System.Windows.Forms.TextBox();
            this.rbDocsEmailedNo = new System.Windows.Forms.RadioButton();
            this.rbDocsEmailedYes = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAccountBlockedYes = new System.Windows.Forms.RadioButton();
            this.rbAccountBlockedNo = new System.Windows.Forms.RadioButton();
            this.txtVatReference = new System.Windows.Forms.TextBox();
            this.cmbSelectCustomerCategory = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContactPersonEmail = new System.Windows.Forms.TextBox();
            this.rtbAddress1 = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddress = new System.Windows.Forms.Button();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLastNumberUsed = new System.Windows.Forms.TextBox();
            this.txtGreigePrefix = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rbCCNo = new System.Windows.Forms.RadioButton();
            this.rbCCYes = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbWareHouse = new System.Windows.Forms.ComboBox();
            this.rbRepackNo = new System.Windows.Forms.RadioButton();
            this.rbRepackYes = new System.Windows.Forms.RadioButton();
            this.btnUsers = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(622, 527);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(622, 559);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbSelectCustomers
            // 
            this.cmbSelectCustomers.FormattingEnabled = true;
            this.cmbSelectCustomers.Location = new System.Drawing.Point(244, 12);
            this.cmbSelectCustomers.Name = "cmbSelectCustomers";
            this.cmbSelectCustomers.Size = new System.Drawing.Size(172, 21);
            this.cmbSelectCustomers.TabIndex = 0;
            this.cmbSelectCustomers.SelectedIndexChanged += new System.EventHandler(this.cmbSelectCustomers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please select a customer";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(107, 55);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerCode.TabIndex = 1;
            this.txtCustomerCode.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Customer Code";
            // 
            // txtCustomerDescription
            // 
            this.txtCustomerDescription.Location = new System.Drawing.Point(379, 55);
            this.txtCustomerDescription.Name = "txtCustomerDescription";
            this.txtCustomerDescription.Size = new System.Drawing.Size(318, 20);
            this.txtCustomerDescription.TabIndex = 2;
            this.txtCustomerDescription.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Customer Description";
            // 
            // rtbPostalAddress
            // 
            this.rtbPostalAddress.Location = new System.Drawing.Point(107, 99);
            this.rtbPostalAddress.Name = "rtbPostalAddress";
            this.rtbPostalAddress.Size = new System.Drawing.Size(201, 96);
            this.rtbPostalAddress.TabIndex = 3;
            this.rtbPostalAddress.Text = "";
            this.rtbPostalAddress.TextChanged += new System.EventHandler(this.rtb_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Postal Address";
            // 
            // txtTelephoneNo
            // 
            this.txtTelephoneNo.Location = new System.Drawing.Point(473, 101);
            this.txtTelephoneNo.Name = "txtTelephoneNo";
            this.txtTelephoneNo.Size = new System.Drawing.Size(224, 20);
            this.txtTelephoneNo.TabIndex = 4;
            this.txtTelephoneNo.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtFaxNo
            // 
            this.txtFaxNo.Location = new System.Drawing.Point(473, 140);
            this.txtFaxNo.Name = "txtFaxNo";
            this.txtFaxNo.Size = new System.Drawing.Size(224, 20);
            this.txtFaxNo.TabIndex = 5;
            this.txtFaxNo.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(473, 189);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(224, 20);
            this.txtContactPerson.TabIndex = 6;
            this.txtContactPerson.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Telephone";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(382, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Fax No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(382, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Contact Person";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEMailAddress);
            this.groupBox1.Controls.Add(this.rbDocsEmailedNo);
            this.groupBox1.Controls.Add(this.rbDocsEmailedYes);
            this.groupBox1.Location = new System.Drawing.Point(385, 306);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 88);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Documents EMailed";
            // 
            // txtEMailAddress
            // 
            this.txtEMailAddress.Location = new System.Drawing.Point(6, 45);
            this.txtEMailAddress.Name = "txtEMailAddress";
            this.txtEMailAddress.Size = new System.Drawing.Size(229, 20);
            this.txtEMailAddress.TabIndex = 2;
            this.txtEMailAddress.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // rbDocsEmailedNo
            // 
            this.rbDocsEmailedNo.AutoSize = true;
            this.rbDocsEmailedNo.Location = new System.Drawing.Point(55, 19);
            this.rbDocsEmailedNo.Name = "rbDocsEmailedNo";
            this.rbDocsEmailedNo.Size = new System.Drawing.Size(39, 17);
            this.rbDocsEmailedNo.TabIndex = 1;
            this.rbDocsEmailedNo.TabStop = true;
            this.rbDocsEmailedNo.Text = "No";
            this.rbDocsEmailedNo.UseVisualStyleBackColor = true;
            // 
            // rbDocsEmailedYes
            // 
            this.rbDocsEmailedYes.AutoSize = true;
            this.rbDocsEmailedYes.Location = new System.Drawing.Point(6, 19);
            this.rbDocsEmailedYes.Name = "rbDocsEmailedYes";
            this.rbDocsEmailedYes.Size = new System.Drawing.Size(43, 17);
            this.rbDocsEmailedYes.TabIndex = 0;
            this.rbDocsEmailedYes.TabStop = true;
            this.rbDocsEmailedYes.Text = "Yes";
            this.rbDocsEmailedYes.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(382, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Vat Reference";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAccountBlockedYes);
            this.groupBox2.Controls.Add(this.rbAccountBlockedNo);
            this.groupBox2.Location = new System.Drawing.Point(17, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 63);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account Blocked";
            // 
            // rbAccountBlockedYes
            // 
            this.rbAccountBlockedYes.AutoSize = true;
            this.rbAccountBlockedYes.Location = new System.Drawing.Point(6, 33);
            this.rbAccountBlockedYes.Name = "rbAccountBlockedYes";
            this.rbAccountBlockedYes.Size = new System.Drawing.Size(43, 17);
            this.rbAccountBlockedYes.TabIndex = 20;
            this.rbAccountBlockedYes.TabStop = true;
            this.rbAccountBlockedYes.Text = "Yes";
            this.rbAccountBlockedYes.UseVisualStyleBackColor = true;
            // 
            // rbAccountBlockedNo
            // 
            this.rbAccountBlockedNo.AutoSize = true;
            this.rbAccountBlockedNo.Location = new System.Drawing.Point(61, 32);
            this.rbAccountBlockedNo.Name = "rbAccountBlockedNo";
            this.rbAccountBlockedNo.Size = new System.Drawing.Size(39, 17);
            this.rbAccountBlockedNo.TabIndex = 0;
            this.rbAccountBlockedNo.TabStop = true;
            this.rbAccountBlockedNo.Text = "No";
            this.rbAccountBlockedNo.UseVisualStyleBackColor = true;
            // 
            // txtVatReference
            // 
            this.txtVatReference.Location = new System.Drawing.Point(473, 266);
            this.txtVatReference.Name = "txtVatReference";
            this.txtVatReference.Size = new System.Drawing.Size(224, 20);
            this.txtVatReference.TabIndex = 7;
            this.txtVatReference.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // cmbSelectCustomerCategory
            // 
            this.cmbSelectCustomerCategory.FormattingEnabled = true;
            this.cmbSelectCustomerCategory.Location = new System.Drawing.Point(111, 337);
            this.cmbSelectCustomerCategory.Name = "cmbSelectCustomerCategory";
            this.cmbSelectCustomerCategory.Size = new System.Drawing.Size(195, 21);
            this.cmbSelectCustomerCategory.TabIndex = 8;
            this.cmbSelectCustomerCategory.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 342);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Customer Category";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Contact Person Email Address";
            // 
            // txtContactPersonEmail
            // 
            this.txtContactPersonEmail.Location = new System.Drawing.Point(473, 228);
            this.txtContactPersonEmail.Name = "txtContactPersonEmail";
            this.txtContactPersonEmail.Size = new System.Drawing.Size(224, 20);
            this.txtContactPersonEmail.TabIndex = 24;
            // 
            // rtbAddress1
            // 
            this.rtbAddress1.Location = new System.Drawing.Point(109, 379);
            this.rtbAddress1.Name = "rtbAddress1";
            this.rtbAddress1.Size = new System.Drawing.Size(201, 96);
            this.rtbAddress1.TabIndex = 25;
            this.rtbAddress1.Text = "";
            this.rtbAddress1.TextChanged += new System.EventHandler(this.rtbAddress1_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 382);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Delivery Address";
            // 
            // btnAddress
            // 
            this.btnAddress.Location = new System.Drawing.Point(622, 493);
            this.btnAddress.Name = "btnAddress";
            this.btnAddress.Size = new System.Drawing.Size(75, 23);
            this.btnAddress.TabIndex = 27;
            this.btnAddress.Text = "Address";
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.btnAddress_Click);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(108, 494);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(201, 96);
            this.rtbNotes.TabIndex = 28;
            this.rtbNotes.Text = "";
            this.rtbNotes.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 497);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Customer Notes";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLastNumberUsed);
            this.groupBox3.Controls.Add(this.txtGreigePrefix);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.rbCCNo);
            this.groupBox3.Controls.Add(this.rbCCYes);
            this.groupBox3.Location = new System.Drawing.Point(354, 404);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 146);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commission Customer";
            // 
            // txtLastNumberUsed
            // 
            this.txtLastNumberUsed.Location = new System.Drawing.Point(30, 116);
            this.txtLastNumberUsed.Name = "txtLastNumberUsed";
            this.txtLastNumberUsed.Size = new System.Drawing.Size(100, 20);
            this.txtLastNumberUsed.TabIndex = 5;
            // 
            // txtGreigePrefix
            // 
            this.txtGreigePrefix.Location = new System.Drawing.Point(30, 67);
            this.txtGreigePrefix.Name = "txtGreigePrefix";
            this.txtGreigePrefix.Size = new System.Drawing.Size(100, 20);
            this.txtGreigePrefix.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Last Number Used";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Greige No Prefix";
            // 
            // rbCCNo
            // 
            this.rbCCNo.AutoSize = true;
            this.rbCCNo.Location = new System.Drawing.Point(92, 20);
            this.rbCCNo.Name = "rbCCNo";
            this.rbCCNo.Size = new System.Drawing.Size(39, 17);
            this.rbCCNo.TabIndex = 1;
            this.rbCCNo.TabStop = true;
            this.rbCCNo.Text = "No";
            this.rbCCNo.UseVisualStyleBackColor = true;
            // 
            // rbCCYes
            // 
            this.rbCCYes.AutoSize = true;
            this.rbCCYes.Location = new System.Drawing.Point(16, 19);
            this.rbCCYes.Name = "rbCCYes";
            this.rbCCYes.Size = new System.Drawing.Size(43, 17);
            this.rbCCYes.TabIndex = 0;
            this.rbCCYes.TabStop = true;
            this.rbCCYes.Text = "Yes";
            this.rbCCYes.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbWareHouse);
            this.groupBox4.Controls.Add(this.rbRepackNo);
            this.groupBox4.Controls.Add(this.rbRepackYes);
            this.groupBox4.Location = new System.Drawing.Point(128, 212);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(174, 119);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Repack Customer";
            // 
            // cmbWareHouse
            // 
            this.cmbWareHouse.FormattingEnabled = true;
            this.cmbWareHouse.Location = new System.Drawing.Point(12, 71);
            this.cmbWareHouse.Name = "cmbWareHouse";
            this.cmbWareHouse.Size = new System.Drawing.Size(150, 21);
            this.cmbWareHouse.TabIndex = 2;
            this.cmbWareHouse.SelectedIndexChanged += new System.EventHandler(this.cmbWareHouse_SelectedIndexChanged);
            // 
            // rbRepackNo
            // 
            this.rbRepackNo.AutoSize = true;
            this.rbRepackNo.Location = new System.Drawing.Point(101, 32);
            this.rbRepackNo.Name = "rbRepackNo";
            this.rbRepackNo.Size = new System.Drawing.Size(39, 17);
            this.rbRepackNo.TabIndex = 1;
            this.rbRepackNo.TabStop = true;
            this.rbRepackNo.Text = "No";
            this.rbRepackNo.UseVisualStyleBackColor = true;
            this.rbRepackNo.CheckedChanged += new System.EventHandler(this.rbRepackNo_CheckedChanged);
            // 
            // rbRepackYes
            // 
            this.rbRepackYes.AutoSize = true;
            this.rbRepackYes.Location = new System.Drawing.Point(39, 32);
            this.rbRepackYes.Name = "rbRepackYes";
            this.rbRepackYes.Size = new System.Drawing.Size(43, 17);
            this.rbRepackYes.TabIndex = 0;
            this.rbRepackYes.TabStop = true;
            this.rbRepackYes.Text = "Yes";
            this.rbRepackYes.UseVisualStyleBackColor = true;
            this.rbRepackYes.CheckedChanged += new System.EventHandler(this.rbRepackYes_CheckedChanged);
            // 
            // btnUsers
            // 
            this.btnUsers.Location = new System.Drawing.Point(622, 449);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(75, 36);
            this.btnUsers.TabIndex = 32;
            this.btnUsers.Text = "External User";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // frmTLADM_Customers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 619);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.btnAddress);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rtbAddress1);
            this.Controls.Add(this.txtContactPersonEmail);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbSelectCustomerCategory);
            this.Controls.Add(this.txtVatReference);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtFaxNo);
            this.Controls.Add(this.txtTelephoneNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtbPostalAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustomerDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSelectCustomers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Name = "frmTLADM_Customers";
            this.Text = "Customers Update / Edit Facility";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbSelectCustomers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbPostalAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTelephoneNo;
        private System.Windows.Forms.TextBox txtFaxNo;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEMailAddress;
        private System.Windows.Forms.RadioButton rbDocsEmailedNo;
        private System.Windows.Forms.RadioButton rbDocsEmailedYes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbAccountBlockedYes;
        private System.Windows.Forms.RadioButton rbAccountBlockedNo;
        private System.Windows.Forms.TextBox txtVatReference;
        private System.Windows.Forms.ComboBox cmbSelectCustomerCategory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContactPersonEmail;
        private System.Windows.Forms.RichTextBox rtbAddress1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAddress;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbCCNo;
        private System.Windows.Forms.RadioButton rbCCYes;
        private System.Windows.Forms.TextBox txtLastNumberUsed;
        private System.Windows.Forms.TextBox txtGreigePrefix;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbRepackNo;
        private System.Windows.Forms.RadioButton rbRepackYes;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.ComboBox cmbWareHouse;
    }
}