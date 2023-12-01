namespace Administration
{
    partial class frmCottonContracts
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
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbCottonContracts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoOfBales = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMass = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpContractDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSupplierRef = new System.Windows.Forms.TextBox();
            this.txtContractNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpContractComplete = new System.Windows.Forms.DateTimePicker();
            this.dtpContractStart = new System.Windows.Forms.DateTimePicker();
            this.txtKiloPM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRandPerKg = new System.Windows.Forms.TextBox();
            this.txtUSDPerKg = new System.Windows.Forms.TextBox();
            this.txtUSDPerLb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbShowKgReceivedNo = new System.Windows.Forms.RadioButton();
            this.rbShowKgReceivedYes = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbShowKgNo = new System.Windows.Forms.RadioButton();
            this.rbShowKgYes = new System.Windows.Forms.RadioButton();
            this.rtbRemarks = new System.Windows.Forms.RichTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmb_unitsOfMeasure = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCottonDescription = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtStapleTo = new System.Windows.Forms.TextBox();
            this.txtStapleFrom = new System.Windows.Forms.TextBox();
            this.txtMicraTo = new System.Windows.Forms.TextBox();
            this.txtMicraFrom = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.dtpClosedDate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.ChkContractClosed = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(599, 565);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 532);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbCottonContracts
            // 
            this.cmbCottonContracts.FormattingEnabled = true;
            this.cmbCottonContracts.Location = new System.Drawing.Point(243, 12);
            this.cmbCottonContracts.Name = "cmbCottonContracts";
            this.cmbCottonContracts.Size = new System.Drawing.Size(220, 21);
            this.cmbCottonContracts.TabIndex = 2;
            this.cmbCottonContracts.SelectedIndexChanged += new System.EventHandler(this.cmbCottonContracts_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Contracts in force";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNoOfBales);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtMass);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtpContractDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSupplierRef);
            this.groupBox1.Controls.Add(this.txtContractNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(327, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 149);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Details";
            // 
            // txtNoOfBales
            // 
            this.txtNoOfBales.Location = new System.Drawing.Point(187, 121);
            this.txtNoOfBales.Name = "txtNoOfBales";
            this.txtNoOfBales.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfBales.TabIndex = 8;
            this.txtNoOfBales.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "No of Bales";
            // 
            // txtMass
            // 
            this.txtMass.Location = new System.Drawing.Point(188, 94);
            this.txtMass.Name = "txtMass";
            this.txtMass.Size = new System.Drawing.Size(100, 20);
            this.txtMass.TabIndex = 6;
            this.txtMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMass.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Contract Mass (Kg)";
            // 
            // dtpContractDate
            // 
            this.dtpContractDate.Location = new System.Drawing.Point(188, 72);
            this.dtpContractDate.Name = "dtpContractDate";
            this.dtpContractDate.Size = new System.Drawing.Size(119, 20);
            this.dtpContractDate.TabIndex = 4;
            this.dtpContractDate.ValueChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Contract Date";
            // 
            // txtSupplierRef
            // 
            this.txtSupplierRef.Location = new System.Drawing.Point(188, 47);
            this.txtSupplierRef.Name = "txtSupplierRef";
            this.txtSupplierRef.Size = new System.Drawing.Size(100, 20);
            this.txtSupplierRef.TabIndex = 3;
            this.txtSupplierRef.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtContractNo
            // 
            this.txtContractNo.Location = new System.Drawing.Point(188, 22);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(100, 20);
            this.txtContractNo.TabIndex = 2;
            this.txtContractNo.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Supplier Ref";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpContractComplete);
            this.groupBox2.Controls.Add(this.dtpContractStart);
            this.groupBox2.Controls.Add(this.txtKiloPM);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(328, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Terms";
            // 
            // dtpContractComplete
            // 
            this.dtpContractComplete.Location = new System.Drawing.Point(188, 77);
            this.dtpContractComplete.Name = "dtpContractComplete";
            this.dtpContractComplete.Size = new System.Drawing.Size(119, 20);
            this.dtpContractComplete.TabIndex = 7;
            this.dtpContractComplete.ValueChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // dtpContractStart
            // 
            this.dtpContractStart.Location = new System.Drawing.Point(188, 19);
            this.dtpContractStart.Name = "dtpContractStart";
            this.dtpContractStart.Size = new System.Drawing.Size(119, 20);
            this.dtpContractStart.TabIndex = 5;
            this.dtpContractStart.ValueChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtKiloPM
            // 
            this.txtKiloPM.Location = new System.Drawing.Point(188, 49);
            this.txtKiloPM.Name = "txtKiloPM";
            this.txtKiloPM.Size = new System.Drawing.Size(100, 20);
            this.txtKiloPM.TabIndex = 6;
            this.txtKiloPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKiloPM.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Contract Complete Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Kg per Month";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Contract Start Date";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRandPerKg);
            this.groupBox3.Controls.Add(this.txtUSDPerKg);
            this.groupBox3.Controls.Add(this.txtUSDPerLb);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(326, 334);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 111);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pricing";
            // 
            // txtRandPerKg
            // 
            this.txtRandPerKg.Location = new System.Drawing.Point(188, 79);
            this.txtRandPerKg.Name = "txtRandPerKg";
            this.txtRandPerKg.Size = new System.Drawing.Size(100, 20);
            this.txtRandPerKg.TabIndex = 10;
            this.txtRandPerKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRandPerKg.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtUSDPerKg
            // 
            this.txtUSDPerKg.Location = new System.Drawing.Point(188, 51);
            this.txtUSDPerKg.Name = "txtUSDPerKg";
            this.txtUSDPerKg.Size = new System.Drawing.Size(100, 20);
            this.txtUSDPerKg.TabIndex = 9;
            this.txtUSDPerKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUSDPerKg.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtUSDPerLb
            // 
            this.txtUSDPerLb.Location = new System.Drawing.Point(188, 23);
            this.txtUSDPerLb.Name = "txtUSDPerLb";
            this.txtUSDPerLb.Size = new System.Drawing.Size(100, 20);
            this.txtUSDPerLb.TabIndex = 8;
            this.txtUSDPerLb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUSDPerLb.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Price R / Kg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Price USD / Kg";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Price USD / lb";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.rtbRemarks);
            this.groupBox4.Location = new System.Drawing.Point(18, 129);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 251);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Remarks";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbShowKgReceivedNo);
            this.groupBox6.Controls.Add(this.rbShowKgReceivedYes);
            this.groupBox6.Location = new System.Drawing.Point(15, 186);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(135, 56);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Show Total Kg received";
            // 
            // rbShowKgReceivedNo
            // 
            this.rbShowKgReceivedNo.AutoSize = true;
            this.rbShowKgReceivedNo.Location = new System.Drawing.Point(68, 19);
            this.rbShowKgReceivedNo.Name = "rbShowKgReceivedNo";
            this.rbShowKgReceivedNo.Size = new System.Drawing.Size(39, 17);
            this.rbShowKgReceivedNo.TabIndex = 1;
            this.rbShowKgReceivedNo.TabStop = true;
            this.rbShowKgReceivedNo.Text = "No";
            this.rbShowKgReceivedNo.UseVisualStyleBackColor = true;
            // 
            // rbShowKgReceivedYes
            // 
            this.rbShowKgReceivedYes.AutoSize = true;
            this.rbShowKgReceivedYes.Location = new System.Drawing.Point(6, 19);
            this.rbShowKgReceivedYes.Name = "rbShowKgReceivedYes";
            this.rbShowKgReceivedYes.Size = new System.Drawing.Size(43, 17);
            this.rbShowKgReceivedYes.TabIndex = 0;
            this.rbShowKgReceivedYes.TabStop = true;
            this.rbShowKgReceivedYes.Text = "Yes";
            this.rbShowKgReceivedYes.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbShowKgNo);
            this.groupBox5.Controls.Add(this.rbShowKgYes);
            this.groupBox5.Location = new System.Drawing.Point(15, 121);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(135, 56);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Show Outstanding Kg";
            // 
            // rbShowKgNo
            // 
            this.rbShowKgNo.AutoSize = true;
            this.rbShowKgNo.Location = new System.Drawing.Point(68, 19);
            this.rbShowKgNo.Name = "rbShowKgNo";
            this.rbShowKgNo.Size = new System.Drawing.Size(39, 17);
            this.rbShowKgNo.TabIndex = 1;
            this.rbShowKgNo.TabStop = true;
            this.rbShowKgNo.Text = "No";
            this.rbShowKgNo.UseVisualStyleBackColor = true;
            // 
            // rbShowKgYes
            // 
            this.rbShowKgYes.AutoSize = true;
            this.rbShowKgYes.Location = new System.Drawing.Point(6, 19);
            this.rbShowKgYes.Name = "rbShowKgYes";
            this.rbShowKgYes.Size = new System.Drawing.Size(43, 17);
            this.rbShowKgYes.TabIndex = 0;
            this.rbShowKgYes.TabStop = true;
            this.rbShowKgYes.Text = "Yes";
            this.rbShowKgYes.UseVisualStyleBackColor = true;
            // 
            // rtbRemarks
            // 
            this.rtbRemarks.Location = new System.Drawing.Point(15, 19);
            this.rtbRemarks.Name = "rtbRemarks";
            this.rtbRemarks.Size = new System.Drawing.Size(168, 96);
            this.rtbRemarks.TabIndex = 12;
            this.rtbRemarks.Text = "";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmb_unitsOfMeasure);
            this.groupBox7.Location = new System.Drawing.Point(18, 42);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 72);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Unit of Measure";
            // 
            // cmb_unitsOfMeasure
            // 
            this.cmb_unitsOfMeasure.FormattingEnabled = true;
            this.cmb_unitsOfMeasure.Location = new System.Drawing.Point(21, 19);
            this.cmb_unitsOfMeasure.Name = "cmb_unitsOfMeasure";
            this.cmb_unitsOfMeasure.Size = new System.Drawing.Size(162, 21);
            this.cmb_unitsOfMeasure.TabIndex = 11;
            this.cmb_unitsOfMeasure.SelectedIndexChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 568);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Cotton Description";
            // 
            // txtCottonDescription
            // 
            this.txtCottonDescription.Location = new System.Drawing.Point(131, 565);
            this.txtCottonDescription.Name = "txtCottonDescription";
            this.txtCottonDescription.Size = new System.Drawing.Size(352, 20);
            this.txtCottonDescription.TabIndex = 16;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtStapleTo);
            this.groupBox8.Controls.Add(this.txtStapleFrom);
            this.groupBox8.Controls.Add(this.txtMicraTo);
            this.groupBox8.Controls.Add(this.txtMicraFrom);
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.label16);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Controls.Add(this.label14);
            this.groupBox8.Location = new System.Drawing.Point(22, 388);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(298, 100);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Micra and Staple";
            // 
            // txtStapleTo
            // 
            this.txtStapleTo.Location = new System.Drawing.Point(221, 55);
            this.txtStapleTo.Name = "txtStapleTo";
            this.txtStapleTo.Size = new System.Drawing.Size(47, 20);
            this.txtStapleTo.TabIndex = 7;
            this.txtStapleTo.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtStapleFrom
            // 
            this.txtStapleFrom.Location = new System.Drawing.Point(221, 28);
            this.txtStapleFrom.Name = "txtStapleFrom";
            this.txtStapleFrom.Size = new System.Drawing.Size(47, 20);
            this.txtStapleFrom.TabIndex = 6;
            this.txtStapleFrom.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtMicraTo
            // 
            this.txtMicraTo.Location = new System.Drawing.Point(79, 55);
            this.txtMicraTo.Name = "txtMicraTo";
            this.txtMicraTo.Size = new System.Drawing.Size(47, 20);
            this.txtMicraTo.TabIndex = 5;
            this.txtMicraTo.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // txtMicraFrom
            // 
            this.txtMicraFrom.Location = new System.Drawing.Point(78, 30);
            this.txtMicraFrom.Name = "txtMicraFrom";
            this.txtMicraFrom.Size = new System.Drawing.Size(47, 20);
            this.txtMicraFrom.TabIndex = 4;
            this.txtMicraFrom.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(147, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Staple To";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(143, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Staple From";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Micra To";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Micra From";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.dtpClosedDate);
            this.groupBox9.Controls.Add(this.label18);
            this.groupBox9.Controls.Add(this.ChkContractClosed);
            this.groupBox9.Location = new System.Drawing.Point(326, 451);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(257, 100);
            this.groupBox9.TabIndex = 18;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Contract Status";
            // 
            // dtpClosedDate
            // 
            this.dtpClosedDate.Location = new System.Drawing.Point(121, 62);
            this.dtpClosedDate.Name = "dtpClosedDate";
            this.dtpClosedDate.Size = new System.Drawing.Size(117, 20);
            this.dtpClosedDate.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(36, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Closed Date";
            // 
            // ChkContractClosed
            // 
            this.ChkContractClosed.AutoSize = true;
            this.ChkContractClosed.Location = new System.Drawing.Point(36, 33);
            this.ChkContractClosed.Name = "ChkContractClosed";
            this.ChkContractClosed.Size = new System.Drawing.Size(101, 17);
            this.ChkContractClosed.TabIndex = 1;
            this.ChkContractClosed.Text = "Contract Closed";
            this.ChkContractClosed.UseVisualStyleBackColor = true;
            // 
            // frmCottonContracts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 605);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.txtCottonDescription);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCottonContracts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmCottonContracts";
            this.Text = "Current Contracts in force";
            this.Load += new System.EventHandler(this.frmCottonContracts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbCottonContracts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSupplierRef;
        private System.Windows.Forms.TextBox txtContractNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpContractComplete;
        private System.Windows.Forms.DateTimePicker dtpContractStart;
        private System.Windows.Forms.TextBox txtKiloPM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRandPerKg;
        private System.Windows.Forms.TextBox txtUSDPerKg;
        private System.Windows.Forms.TextBox txtUSDPerLb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbShowKgReceivedNo;
        private System.Windows.Forms.RadioButton rbShowKgReceivedYes;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbShowKgNo;
        private System.Windows.Forms.RadioButton rbShowKgYes;
        private System.Windows.Forms.RichTextBox rtbRemarks;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmb_unitsOfMeasure;
        private System.Windows.Forms.DateTimePicker dtpContractDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCottonDescription;
        private System.Windows.Forms.TextBox txtMass;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNoOfBales;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtStapleTo;
        private System.Windows.Forms.TextBox txtStapleFrom;
        private System.Windows.Forms.TextBox txtMicraTo;
        private System.Windows.Forms.TextBox txtMicraFrom;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.DateTimePicker dtpClosedDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox ChkContractClosed;
    }
}