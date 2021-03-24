namespace TTI2_WF
{
    partial class frmTLADMWhseStores
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
            this.cmbWhse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAddress5 = new System.Windows.Forms.TextBox();
            this.txtAddress4 = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rctNotes = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbStore = new System.Windows.Forms.RadioButton();
            this.rbWarehouse = new System.Windows.Forms.RadioButton();
            this.btnNew = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCountryOfOrigin = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbGradeOther = new System.Windows.Forms.RadioButton();
            this.rbGradeA = new System.Windows.Forms.RadioButton();
            this.rbDyeKitchYes = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbDyeKitchenNo = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbChemicalStoreNo = new System.Windows.Forms.RadioButton();
            this.rbChemicalStoreYes = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbBundleStoreNo = new System.Windows.Forms.RadioButton();
            this.rbBundleStoreYes = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbPanelStoreNo = new System.Windows.Forms.RadioButton();
            this.rbPanelStoreYes = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rbRepackNo = new System.Windows.Forms.RadioButton();
            this.rbRepacYes = new System.Windows.Forms.RadioButton();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWhse
            // 
            this.cmbWhse.FormattingEnabled = true;
            this.cmbWhse.Location = new System.Drawing.Point(221, 12);
            this.cmbWhse.Name = "cmbWhse";
            this.cmbWhse.Size = new System.Drawing.Size(286, 21);
            this.cmbWhse.TabIndex = 0;
            this.cmbWhse.SelectedIndexChanged += new System.EventHandler(this.cmbWhse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select a warehouse / Store";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(600, 706);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(73, 49);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            this.txtCode.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Location = new System.Drawing.Point(418, 49);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 20);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(73, 92);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(121, 21);
            this.cmbDepartment.TabIndex = 3;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(418, 92);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 21);
            this.cmbType.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAddress5);
            this.groupBox1.Controls.Add(this.txtAddress4);
            this.groupBox1.Controls.Add(this.txtAddress3);
            this.groupBox1.Controls.Add(this.txtAddress2);
            this.groupBox1.Controls.Add(this.txtAddress1);
            this.groupBox1.Location = new System.Drawing.Point(73, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 174);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Address Info";
            // 
            // txtAddress5
            // 
            this.txtAddress5.Location = new System.Drawing.Point(75, 136);
            this.txtAddress5.Name = "txtAddress5";
            this.txtAddress5.Size = new System.Drawing.Size(394, 20);
            this.txtAddress5.TabIndex = 4;
            // 
            // txtAddress4
            // 
            this.txtAddress4.Location = new System.Drawing.Point(75, 108);
            this.txtAddress4.Name = "txtAddress4";
            this.txtAddress4.Size = new System.Drawing.Size(394, 20);
            this.txtAddress4.TabIndex = 3;
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(75, 81);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(394, 20);
            this.txtAddress3.TabIndex = 2;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(75, 53);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(394, 20);
            this.txtAddress2.TabIndex = 1;
            this.txtAddress2.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(75, 25);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(394, 20);
            this.txtAddress1.TabIndex = 0;
            this.txtAddress1.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description";
            // 
            // rctNotes
            // 
            this.rctNotes.Location = new System.Drawing.Point(73, 643);
            this.rctNotes.Name = "rctNotes";
            this.rctNotes.Size = new System.Drawing.Size(424, 85);
            this.rctNotes.TabIndex = 8;
            this.rctNotes.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtEMail);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtTelephone);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtContact);
            this.groupBox2.Location = new System.Drawing.Point(73, 504);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 113);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Contact Info";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "EMail";
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(123, 71);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(321, 20);
            this.txtEMail.TabIndex = 4;
            this.txtEMail.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Telephone";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(123, 45);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(247, 20);
            this.txtTelephone.TabIndex = 2;
            this.txtTelephone.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Contact";
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(123, 19);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(311, 20);
            this.txtContact.TabIndex = 0;
            this.txtContact.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Department";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(352, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbStore);
            this.groupBox3.Controls.Add(this.rbWarehouse);
            this.groupBox3.Location = new System.Drawing.Point(210, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(121, 67);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Warehouse or Store";
            // 
            // rbStore
            // 
            this.rbStore.AutoSize = true;
            this.rbStore.Location = new System.Drawing.Point(11, 42);
            this.rbStore.Name = "rbStore";
            this.rbStore.Size = new System.Drawing.Size(50, 17);
            this.rbStore.TabIndex = 1;
            this.rbStore.TabStop = true;
            this.rbStore.Text = "Store";
            this.rbStore.UseVisualStyleBackColor = true;
            // 
            // rbWarehouse
            // 
            this.rbWarehouse.AutoSize = true;
            this.rbWarehouse.Location = new System.Drawing.Point(11, 19);
            this.rbWarehouse.Name = "rbWarehouse";
            this.rbWarehouse.Size = new System.Drawing.Size(80, 17);
            this.rbWarehouse.TabIndex = 0;
            this.rbWarehouse.TabStop = true;
            this.rbWarehouse.Text = "Warehouse";
            this.rbWarehouse.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(600, 677);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Country of Origin";
            // 
            // txtCountryOfOrigin
            // 
            this.txtCountryOfOrigin.Location = new System.Drawing.Point(104, 134);
            this.txtCountryOfOrigin.Name = "txtCountryOfOrigin";
            this.txtCountryOfOrigin.Size = new System.Drawing.Size(279, 20);
            this.txtCountryOfOrigin.TabIndex = 15;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbGradeOther);
            this.groupBox4.Controls.Add(this.rbGradeA);
            this.groupBox4.Location = new System.Drawing.Point(131, 164);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 46);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Grade A Status";
            // 
            // rbGradeOther
            // 
            this.rbGradeOther.AutoSize = true;
            this.rbGradeOther.Location = new System.Drawing.Point(22, 19);
            this.rbGradeOther.Name = "rbGradeOther";
            this.rbGradeOther.Size = new System.Drawing.Size(39, 17);
            this.rbGradeOther.TabIndex = 1;
            this.rbGradeOther.TabStop = true;
            this.rbGradeOther.Text = "No";
            this.rbGradeOther.UseVisualStyleBackColor = true;
            // 
            // rbGradeA
            // 
            this.rbGradeA.AutoSize = true;
            this.rbGradeA.Location = new System.Drawing.Point(105, 19);
            this.rbGradeA.Name = "rbGradeA";
            this.rbGradeA.Size = new System.Drawing.Size(43, 17);
            this.rbGradeA.TabIndex = 0;
            this.rbGradeA.TabStop = true;
            this.rbGradeA.Text = "Yes";
            this.rbGradeA.UseVisualStyleBackColor = true;
            // 
            // rbDyeKitchYes
            // 
            this.rbDyeKitchYes.AutoSize = true;
            this.rbDyeKitchYes.Location = new System.Drawing.Point(127, 19);
            this.rbDyeKitchYes.Name = "rbDyeKitchYes";
            this.rbDyeKitchYes.Size = new System.Drawing.Size(43, 17);
            this.rbDyeKitchYes.TabIndex = 17;
            this.rbDyeKitchYes.TabStop = true;
            this.rbDyeKitchYes.Text = "Yes";
            this.rbDyeKitchYes.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbDyeKitchenNo);
            this.groupBox5.Controls.Add(this.rbDyeKitchYes);
            this.groupBox5.Location = new System.Drawing.Point(355, 272);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 46);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dye Kitchen";
            // 
            // rbDyeKitchenNo
            // 
            this.rbDyeKitchenNo.AutoSize = true;
            this.rbDyeKitchenNo.Location = new System.Drawing.Point(34, 19);
            this.rbDyeKitchenNo.Name = "rbDyeKitchenNo";
            this.rbDyeKitchenNo.Size = new System.Drawing.Size(39, 17);
            this.rbDyeKitchenNo.TabIndex = 18;
            this.rbDyeKitchenNo.TabStop = true;
            this.rbDyeKitchenNo.Text = "No";
            this.rbDyeKitchenNo.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkDefault);
            this.groupBox6.Controls.Add(this.rbChemicalStoreNo);
            this.groupBox6.Controls.Add(this.rbChemicalStoreYes);
            this.groupBox6.Location = new System.Drawing.Point(101, 272);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(230, 46);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Chemical Store";
            // 
            // rbChemicalStoreNo
            // 
            this.rbChemicalStoreNo.AutoSize = true;
            this.rbChemicalStoreNo.Location = new System.Drawing.Point(22, 19);
            this.rbChemicalStoreNo.Name = "rbChemicalStoreNo";
            this.rbChemicalStoreNo.Size = new System.Drawing.Size(39, 17);
            this.rbChemicalStoreNo.TabIndex = 18;
            this.rbChemicalStoreNo.TabStop = true;
            this.rbChemicalStoreNo.Text = "No";
            this.rbChemicalStoreNo.UseVisualStyleBackColor = true;
            // 
            // rbChemicalStoreYes
            // 
            this.rbChemicalStoreYes.AutoSize = true;
            this.rbChemicalStoreYes.Location = new System.Drawing.Point(105, 19);
            this.rbChemicalStoreYes.Name = "rbChemicalStoreYes";
            this.rbChemicalStoreYes.Size = new System.Drawing.Size(43, 17);
            this.rbChemicalStoreYes.TabIndex = 17;
            this.rbChemicalStoreYes.TabStop = true;
            this.rbChemicalStoreYes.Text = "Yes";
            this.rbChemicalStoreYes.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbBundleStoreNo);
            this.groupBox7.Controls.Add(this.rbBundleStoreYes);
            this.groupBox7.Location = new System.Drawing.Point(131, 220);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 46);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Bundle Store";
            // 
            // rbBundleStoreNo
            // 
            this.rbBundleStoreNo.AutoSize = true;
            this.rbBundleStoreNo.Location = new System.Drawing.Point(22, 19);
            this.rbBundleStoreNo.Name = "rbBundleStoreNo";
            this.rbBundleStoreNo.Size = new System.Drawing.Size(39, 17);
            this.rbBundleStoreNo.TabIndex = 18;
            this.rbBundleStoreNo.TabStop = true;
            this.rbBundleStoreNo.Text = "No";
            this.rbBundleStoreNo.UseVisualStyleBackColor = true;
            // 
            // rbBundleStoreYes
            // 
            this.rbBundleStoreYes.AutoSize = true;
            this.rbBundleStoreYes.Location = new System.Drawing.Point(105, 19);
            this.rbBundleStoreYes.Name = "rbBundleStoreYes";
            this.rbBundleStoreYes.Size = new System.Drawing.Size(43, 17);
            this.rbBundleStoreYes.TabIndex = 17;
            this.rbBundleStoreYes.TabStop = true;
            this.rbBundleStoreYes.Text = "Yes";
            this.rbBundleStoreYes.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbPanelStoreNo);
            this.groupBox8.Controls.Add(this.rbPanelStoreYes);
            this.groupBox8.Location = new System.Drawing.Point(355, 220);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(200, 46);
            this.groupBox8.TabIndex = 21;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Panel Store";
            // 
            // rbPanelStoreNo
            // 
            this.rbPanelStoreNo.AutoSize = true;
            this.rbPanelStoreNo.Location = new System.Drawing.Point(34, 19);
            this.rbPanelStoreNo.Name = "rbPanelStoreNo";
            this.rbPanelStoreNo.Size = new System.Drawing.Size(39, 17);
            this.rbPanelStoreNo.TabIndex = 18;
            this.rbPanelStoreNo.TabStop = true;
            this.rbPanelStoreNo.Text = "No";
            this.rbPanelStoreNo.UseVisualStyleBackColor = true;
            // 
            // rbPanelStoreYes
            // 
            this.rbPanelStoreYes.AutoSize = true;
            this.rbPanelStoreYes.Location = new System.Drawing.Point(127, 19);
            this.rbPanelStoreYes.Name = "rbPanelStoreYes";
            this.rbPanelStoreYes.Size = new System.Drawing.Size(43, 17);
            this.rbPanelStoreYes.TabIndex = 17;
            this.rbPanelStoreYes.TabStop = true;
            this.rbPanelStoreYes.Text = "Yes";
            this.rbPanelStoreYes.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.rbRepackNo);
            this.groupBox9.Controls.Add(this.rbRepacYes);
            this.groupBox9.Location = new System.Drawing.Point(355, 164);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(200, 46);
            this.groupBox9.TabIndex = 22;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Re Pack Center";
            // 
            // rbRepackNo
            // 
            this.rbRepackNo.AutoSize = true;
            this.rbRepackNo.Location = new System.Drawing.Point(35, 19);
            this.rbRepackNo.Name = "rbRepackNo";
            this.rbRepackNo.Size = new System.Drawing.Size(39, 17);
            this.rbRepackNo.TabIndex = 1;
            this.rbRepackNo.TabStop = true;
            this.rbRepackNo.Text = "No";
            this.rbRepackNo.UseVisualStyleBackColor = true;
            // 
            // rbRepacYes
            // 
            this.rbRepacYes.AutoSize = true;
            this.rbRepacYes.Location = new System.Drawing.Point(127, 19);
            this.rbRepacYes.Name = "rbRepacYes";
            this.rbRepacYes.Size = new System.Drawing.Size(43, 17);
            this.rbRepacYes.TabIndex = 0;
            this.rbRepacYes.TabStop = true;
            this.rbRepacYes.Text = "Yes";
            this.rbRepacYes.UseVisualStyleBackColor = true;
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(165, 19);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(60, 17);
            this.chkDefault.TabIndex = 19;
            this.chkDefault.Text = "Default";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // frmTLADMWhseStores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 748);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtCountryOfOrigin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.rctNotes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbWhse);
            this.Name = "frmTLADMWhseStores";
            this.Text = "Warehouse and Store Update / Edit";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWhse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAddress5;
        private System.Windows.Forms.TextBox txtAddress4;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rctNotes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbStore;
        private System.Windows.Forms.RadioButton rbWarehouse;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCountryOfOrigin;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbGradeOther;
        private System.Windows.Forms.RadioButton rbGradeA;
        private System.Windows.Forms.RadioButton rbDyeKitchYes;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbDyeKitchenNo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbChemicalStoreNo;
        private System.Windows.Forms.RadioButton rbChemicalStoreYes;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbBundleStoreNo;
        private System.Windows.Forms.RadioButton rbBundleStoreYes;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbPanelStoreNo;
        private System.Windows.Forms.RadioButton rbPanelStoreYes;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton rbRepackNo;
        private System.Windows.Forms.RadioButton rbRepacYes;
        private System.Windows.Forms.CheckBox chkDefault;
    }
}