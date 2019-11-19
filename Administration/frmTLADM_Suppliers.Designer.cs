namespace TTI2_WF
{
    partial class frmTLADM_Suppliers
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
            this.cmbSelectSupplier = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSupplierDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStdPaymentTerms = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiscountStructure = new System.Windows.Forms.TextBox();
            this.rtbPostalAddress = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rtbShippingAddress1 = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbBlockedNo = new System.Windows.Forms.RadioButton();
            this.rbAccountBlockedYes = new System.Windows.Forms.RadioButton();
            this.cmbProductGroups = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAllowsConsignmentNo = new System.Windows.Forms.RadioButton();
            this.rbAllowsConsignmentYes = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.txtVatReference = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.rbDocsEmailedNo = new System.Windows.Forms.RadioButton();
            this.rbDocsEmailedYes = new System.Windows.Forms.RadioButton();
            this.cmbProductTypes = new System.Windows.Forms.ComboBox();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAddresses = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSelectSupplier
            // 
            this.cmbSelectSupplier.FormattingEnabled = true;
            this.cmbSelectSupplier.Location = new System.Drawing.Point(219, 12);
            this.cmbSelectSupplier.Name = "cmbSelectSupplier";
            this.cmbSelectSupplier.Size = new System.Drawing.Size(211, 21);
            this.cmbSelectSupplier.TabIndex = 0;
            this.cmbSelectSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSelectSupplier_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select a supplier";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(756, 581);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(756, 548);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(144, 60);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(100, 20);
            this.txtSupplierCode.TabIndex = 4;
            this.txtSupplierCode.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Supplier Code";
            // 
            // txtSupplierDescription
            // 
            this.txtSupplierDescription.Location = new System.Drawing.Point(437, 60);
            this.txtSupplierDescription.Name = "txtSupplierDescription";
            this.txtSupplierDescription.Size = new System.Drawing.Size(252, 20);
            this.txtSupplierDescription.TabIndex = 6;
            this.txtSupplierDescription.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description";
            // 
            // txtStdPaymentTerms
            // 
            this.txtStdPaymentTerms.Location = new System.Drawing.Point(144, 98);
            this.txtStdPaymentTerms.Name = "txtStdPaymentTerms";
            this.txtStdPaymentTerms.Size = new System.Drawing.Size(100, 20);
            this.txtStdPaymentTerms.TabIndex = 8;
            this.txtStdPaymentTerms.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Std Payment Terms";
            // 
            // txtDiscountStructure
            // 
            this.txtDiscountStructure.Location = new System.Drawing.Point(437, 98);
            this.txtDiscountStructure.Name = "txtDiscountStructure";
            this.txtDiscountStructure.Size = new System.Drawing.Size(100, 20);
            this.txtDiscountStructure.TabIndex = 10;
            this.txtDiscountStructure.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // rtbPostalAddress
            // 
            this.rtbPostalAddress.Location = new System.Drawing.Point(144, 138);
            this.rtbPostalAddress.Name = "rtbPostalAddress";
            this.rtbPostalAddress.Size = new System.Drawing.Size(185, 96);
            this.rtbPostalAddress.TabIndex = 11;
            this.rtbPostalAddress.Text = "";
            this.rtbPostalAddress.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Postal Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Discount Structure";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(454, 138);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(204, 20);
            this.txtTelephone.TabIndex = 14;
            this.txtTelephone.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(373, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Telephone";
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(454, 178);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(204, 20);
            this.txtFax.TabIndex = 16;
            this.txtFax.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(376, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Fax Details";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(454, 214);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(204, 20);
            this.txtContactPerson.TabIndex = 18;
            this.txtContactPerson.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(354, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Contact Person";
            // 
            // rtbShippingAddress1
            // 
            this.rtbShippingAddress1.Location = new System.Drawing.Point(144, 271);
            this.rtbShippingAddress1.Name = "rtbShippingAddress1";
            this.rtbShippingAddress1.Size = new System.Drawing.Size(185, 96);
            this.rtbShippingAddress1.TabIndex = 20;
            this.rtbShippingAddress1.Text = "";
            this.rtbShippingAddress1.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Shipping Address 1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBlockedNo);
            this.groupBox1.Controls.Add(this.rbAccountBlockedYes);
            this.groupBox1.Location = new System.Drawing.Point(144, 412);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 49);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Blocked";
            // 
            // rbBlockedNo
            // 
            this.rbBlockedNo.AutoSize = true;
            this.rbBlockedNo.Location = new System.Drawing.Point(98, 19);
            this.rbBlockedNo.Name = "rbBlockedNo";
            this.rbBlockedNo.Size = new System.Drawing.Size(39, 17);
            this.rbBlockedNo.TabIndex = 1;
            this.rbBlockedNo.TabStop = true;
            this.rbBlockedNo.Text = "No";
            this.rbBlockedNo.UseVisualStyleBackColor = true;
            // 
            // rbAccountBlockedYes
            // 
            this.rbAccountBlockedYes.AutoSize = true;
            this.rbAccountBlockedYes.Location = new System.Drawing.Point(15, 19);
            this.rbAccountBlockedYes.Name = "rbAccountBlockedYes";
            this.rbAccountBlockedYes.Size = new System.Drawing.Size(43, 17);
            this.rbAccountBlockedYes.TabIndex = 0;
            this.rbAccountBlockedYes.TabStop = true;
            this.rbAccountBlockedYes.Text = "Yes";
            this.rbAccountBlockedYes.UseVisualStyleBackColor = true;
            // 
            // cmbProductGroups
            // 
            this.cmbProductGroups.FormattingEnabled = true;
            this.cmbProductGroups.Location = new System.Drawing.Point(454, 409);
            this.cmbProductGroups.Name = "cmbProductGroups";
            this.cmbProductGroups.Size = new System.Drawing.Size(133, 21);
            this.cmbProductGroups.TabIndex = 29;
            this.cmbProductGroups.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(345, 412);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Product Groups";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAllowsConsignmentNo);
            this.groupBox2.Controls.Add(this.rbAllowsConsignmentYes);
            this.groupBox2.Location = new System.Drawing.Point(634, 412);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 49);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Allows Consignments";
            // 
            // rbAllowsConsignmentNo
            // 
            this.rbAllowsConsignmentNo.AutoSize = true;
            this.rbAllowsConsignmentNo.Location = new System.Drawing.Point(75, 20);
            this.rbAllowsConsignmentNo.Name = "rbAllowsConsignmentNo";
            this.rbAllowsConsignmentNo.Size = new System.Drawing.Size(39, 17);
            this.rbAllowsConsignmentNo.TabIndex = 1;
            this.rbAllowsConsignmentNo.TabStop = true;
            this.rbAllowsConsignmentNo.Text = "No";
            this.rbAllowsConsignmentNo.UseVisualStyleBackColor = true;
            // 
            // rbAllowsConsignmentYes
            // 
            this.rbAllowsConsignmentYes.AutoSize = true;
            this.rbAllowsConsignmentYes.Location = new System.Drawing.Point(17, 20);
            this.rbAllowsConsignmentYes.Name = "rbAllowsConsignmentYes";
            this.rbAllowsConsignmentYes.Size = new System.Drawing.Size(43, 17);
            this.rbAllowsConsignmentYes.TabIndex = 0;
            this.rbAllowsConsignmentYes.TabStop = true;
            this.rbAllowsConsignmentYes.Text = "Yes";
            this.rbAllowsConsignmentYes.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(345, 469);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Product Types";
            // 
            // txtVatReference
            // 
            this.txtVatReference.Location = new System.Drawing.Point(661, 97);
            this.txtVatReference.Name = "txtVatReference";
            this.txtVatReference.Size = new System.Drawing.Size(121, 20);
            this.txtVatReference.TabIndex = 34;
            this.txtVatReference.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(566, 100);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "Vat Reference";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtEmailAddress);
            this.groupBox3.Controls.Add(this.rbDocsEmailedNo);
            this.groupBox3.Controls.Add(this.rbDocsEmailedYes);
            this.groupBox3.Location = new System.Drawing.Point(144, 498);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(275, 95);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Documents emailed";
            this.groupBox3.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(6, 62);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(254, 20);
            this.txtEmailAddress.TabIndex = 2;
            this.txtEmailAddress.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // rbDocsEmailedNo
            // 
            this.rbDocsEmailedNo.AutoSize = true;
            this.rbDocsEmailedNo.Location = new System.Drawing.Point(98, 29);
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
            this.rbDocsEmailedYes.Location = new System.Drawing.Point(15, 29);
            this.rbDocsEmailedYes.Name = "rbDocsEmailedYes";
            this.rbDocsEmailedYes.Size = new System.Drawing.Size(43, 17);
            this.rbDocsEmailedYes.TabIndex = 0;
            this.rbDocsEmailedYes.TabStop = true;
            this.rbDocsEmailedYes.Text = "Yes";
            this.rbDocsEmailedYes.UseVisualStyleBackColor = true;
           
            // 
            // cmbProductTypes
            // 
            this.cmbProductTypes.FormattingEnabled = true;
            this.cmbProductTypes.Location = new System.Drawing.Point(454, 466);
            this.cmbProductTypes.Name = "cmbProductTypes";
            this.cmbProductTypes.Size = new System.Drawing.Size(133, 21);
            this.cmbProductTypes.TabIndex = 37;
            this.cmbProductTypes.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(511, 498);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(202, 96);
            this.rtbNotes.TabIndex = 38;
            this.rtbNotes.Text = "";
            this.rtbNotes.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(451, 501);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Notes";
            // 
            // btnAddresses
            // 
            this.btnAddresses.Location = new System.Drawing.Point(756, 515);
            this.btnAddresses.Name = "btnAddresses";
            this.btnAddresses.Size = new System.Drawing.Size(75, 23);
            this.btnAddresses.TabIndex = 40;
            this.btnAddresses.Text = "Addresses";
            this.btnAddresses.UseVisualStyleBackColor = true;
            this.btnAddresses.Click += new System.EventHandler(this.btnAddresses_Click);
            // 
            // frmTLADM_Suppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 627);
            this.Controls.Add(this.btnAddresses);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.cmbProductTypes);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtVatReference);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbProductGroups);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rtbShippingAddress1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtbPostalAddress);
            this.Controls.Add(this.txtDiscountStructure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStdPaymentTerms);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSupplierDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSupplierCode);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSelectSupplier);
            this.Name = "frmTLADM_Suppliers";
            this.Text = "Suppliers update / edit faci;lity";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectSupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSupplierDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStdPaymentTerms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDiscountStructure;
        private System.Windows.Forms.RichTextBox rtbPostalAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbShippingAddress1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBlockedNo;
        private System.Windows.Forms.RadioButton rbAccountBlockedYes;
        private System.Windows.Forms.ComboBox cmbProductGroups;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbAllowsConsignmentNo;
        private System.Windows.Forms.RadioButton rbAllowsConsignmentYes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtVatReference;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.RadioButton rbDocsEmailedNo;
        private System.Windows.Forms.RadioButton rbDocsEmailedYes;
        private System.Windows.Forms.ComboBox cmbProductTypes;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnAddresses;
    }
}