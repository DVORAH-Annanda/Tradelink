namespace TTI2_WF
{
    partial class frmTLADM_MachineDefinition
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
            this.cmbCodeSelection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDepartmentSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMeasure4Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure3Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure2Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure1Qty = new System.Windows.Forms.TextBox();
            this.cmbFourthM = new System.Windows.Forms.ComboBox();
            this.cmbThirdM = new System.Windows.Forms.ComboBox();
            this.cmbSecondM = new System.Windows.Forms.ComboBox();
            this.cmbFirstM = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMeasure8Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure7Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure6Qty = new System.Windows.Forms.TextBox();
            this.txtMeasure5Qty = new System.Windows.Forms.TextBox();
            this.cmbEighthM = new System.Windows.Forms.ComboBox();
            this.cmbSeventhM = new System.Windows.Forms.ComboBox();
            this.cmbSixM = new System.Windows.Forms.ComboBox();
            this.cmbFifthM = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbFabricProdType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLastNumberUsed = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbFinishedGoods = new System.Windows.Forms.ComboBox();
            this.txtAssetRegNo = new System.Windows.Forms.TextBox();
            this.txtRealistic = new System.Windows.Forms.TextBox();
            this.txtMaxCapacity = new System.Windows.Forms.TextBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtGLCostCentre = new System.Windows.Forms.TextBox();
            this.txtMacineDescription = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbGreigeProductType = new Administration.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCodeSelection
            // 
            this.cmbCodeSelection.FormattingEnabled = true;
            this.cmbCodeSelection.Location = new System.Drawing.Point(207, 12);
            this.cmbCodeSelection.Name = "cmbCodeSelection";
            this.cmbCodeSelection.Size = new System.Drawing.Size(164, 21);
            this.cmbCodeSelection.TabIndex = 0;
            this.cmbCodeSelection.SelectedIndexChanged += new System.EventHandler(this.cmbCodeSelection_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select a machine code";
            // 
            // cmbDepartmentSelect
            // 
            this.cmbDepartmentSelect.FormattingEnabled = true;
            this.cmbDepartmentSelect.Location = new System.Drawing.Point(595, 54);
            this.cmbDepartmentSelect.Name = "cmbDepartmentSelect";
            this.cmbDepartmentSelect.Size = new System.Drawing.Size(164, 21);
            this.cmbDepartmentSelect.TabIndex = 2;
            this.cmbDepartmentSelect.SelectedIndexChanged += new System.EventHandler(this.cmb_MandatorySelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select a department";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(726, 497);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 19;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(726, 526);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Items";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMeasure4Qty);
            this.groupBox1.Controls.Add(this.txtMeasure3Qty);
            this.groupBox1.Controls.Add(this.txtMeasure2Qty);
            this.groupBox1.Controls.Add(this.txtMeasure1Qty);
            this.groupBox1.Controls.Add(this.cmbFourthM);
            this.groupBox1.Controls.Add(this.cmbThirdM);
            this.groupBox1.Controls.Add(this.cmbSecondM);
            this.groupBox1.Controls.Add(this.cmbFirstM);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(37, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 194);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consumption Measurements";
            // 
            // txtMeasure4Qty
            // 
            this.txtMeasure4Qty.Location = new System.Drawing.Point(256, 160);
            this.txtMeasure4Qty.Name = "txtMeasure4Qty";
            this.txtMeasure4Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure4Qty.TabIndex = 17;
            this.txtMeasure4Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure3Qty
            // 
            this.txtMeasure3Qty.Location = new System.Drawing.Point(256, 127);
            this.txtMeasure3Qty.Name = "txtMeasure3Qty";
            this.txtMeasure3Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure3Qty.TabIndex = 16;
            this.txtMeasure3Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure2Qty
            // 
            this.txtMeasure2Qty.Location = new System.Drawing.Point(256, 93);
            this.txtMeasure2Qty.Name = "txtMeasure2Qty";
            this.txtMeasure2Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure2Qty.TabIndex = 15;
            this.txtMeasure2Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure1Qty
            // 
            this.txtMeasure1Qty.Location = new System.Drawing.Point(256, 58);
            this.txtMeasure1Qty.Name = "txtMeasure1Qty";
            this.txtMeasure1Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure1Qty.TabIndex = 14;
            this.txtMeasure1Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbFourthM
            // 
            this.cmbFourthM.FormattingEnabled = true;
            this.cmbFourthM.Location = new System.Drawing.Point(58, 159);
            this.cmbFourthM.Name = "cmbFourthM";
            this.cmbFourthM.Size = new System.Drawing.Size(185, 21);
            this.cmbFourthM.TabIndex = 3;
            this.cmbFourthM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbFourthM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbThirdM
            // 
            this.cmbThirdM.FormattingEnabled = true;
            this.cmbThirdM.Location = new System.Drawing.Point(58, 126);
            this.cmbThirdM.Name = "cmbThirdM";
            this.cmbThirdM.Size = new System.Drawing.Size(185, 21);
            this.cmbThirdM.TabIndex = 2;
            this.cmbThirdM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbThirdM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbSecondM
            // 
            this.cmbSecondM.FormattingEnabled = true;
            this.cmbSecondM.Location = new System.Drawing.Point(58, 92);
            this.cmbSecondM.Name = "cmbSecondM";
            this.cmbSecondM.Size = new System.Drawing.Size(185, 21);
            this.cmbSecondM.TabIndex = 1;
            this.cmbSecondM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbSecondM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbFirstM
            // 
            this.cmbFirstM.FormattingEnabled = true;
            this.cmbFirstM.Location = new System.Drawing.Point(58, 57);
            this.cmbFirstM.Name = "cmbFirstM";
            this.cmbFirstM.Size = new System.Drawing.Size(185, 21);
            this.cmbFirstM.TabIndex = 0;
            this.cmbFirstM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbFirstM.DisplayMemberChanged += new System.EventHandler(this.cmb_DisplayMemberChanged);
            this.cmbFirstM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "4th M";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "3rd M";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "2nd M";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "1st M";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMeasure8Qty);
            this.groupBox2.Controls.Add(this.txtMeasure7Qty);
            this.groupBox2.Controls.Add(this.txtMeasure6Qty);
            this.groupBox2.Controls.Add(this.txtMeasure5Qty);
            this.groupBox2.Controls.Add(this.cmbEighthM);
            this.groupBox2.Controls.Add(this.cmbSeventhM);
            this.groupBox2.Controls.Add(this.cmbSixM);
            this.groupBox2.Controls.Add(this.cmbFifthM);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(434, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 194);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Consumption Measurements";
            // 
            // txtMeasure8Qty
            // 
            this.txtMeasure8Qty.AcceptsReturn = true;
            this.txtMeasure8Qty.Location = new System.Drawing.Point(261, 160);
            this.txtMeasure8Qty.Name = "txtMeasure8Qty";
            this.txtMeasure8Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure8Qty.TabIndex = 21;
            this.txtMeasure8Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure7Qty
            // 
            this.txtMeasure7Qty.Location = new System.Drawing.Point(261, 127);
            this.txtMeasure7Qty.Name = "txtMeasure7Qty";
            this.txtMeasure7Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure7Qty.TabIndex = 20;
            this.txtMeasure7Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure6Qty
            // 
            this.txtMeasure6Qty.Location = new System.Drawing.Point(261, 93);
            this.txtMeasure6Qty.Name = "txtMeasure6Qty";
            this.txtMeasure6Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure6Qty.TabIndex = 19;
            this.txtMeasure6Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMeasure5Qty
            // 
            this.txtMeasure5Qty.Location = new System.Drawing.Point(261, 58);
            this.txtMeasure5Qty.Name = "txtMeasure5Qty";
            this.txtMeasure5Qty.Size = new System.Drawing.Size(100, 20);
            this.txtMeasure5Qty.TabIndex = 18;
            this.txtMeasure5Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbEighthM
            // 
            this.cmbEighthM.FormattingEnabled = true;
            this.cmbEighthM.Location = new System.Drawing.Point(58, 159);
            this.cmbEighthM.Name = "cmbEighthM";
            this.cmbEighthM.Size = new System.Drawing.Size(185, 21);
            this.cmbEighthM.TabIndex = 3;
            this.cmbEighthM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbEighthM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbSeventhM
            // 
            this.cmbSeventhM.FormattingEnabled = true;
            this.cmbSeventhM.Location = new System.Drawing.Point(60, 126);
            this.cmbSeventhM.Name = "cmbSeventhM";
            this.cmbSeventhM.Size = new System.Drawing.Size(185, 21);
            this.cmbSeventhM.TabIndex = 2;
            this.cmbSeventhM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbSeventhM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbSixM
            // 
            this.cmbSixM.FormattingEnabled = true;
            this.cmbSixM.Location = new System.Drawing.Point(60, 92);
            this.cmbSixM.Name = "cmbSixM";
            this.cmbSixM.Size = new System.Drawing.Size(185, 21);
            this.cmbSixM.TabIndex = 1;
            this.cmbSixM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbSixM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // cmbFifthM
            // 
            this.cmbFifthM.FormattingEnabled = true;
            this.cmbFifthM.Location = new System.Drawing.Point(58, 57);
            this.cmbFifthM.Name = "cmbFifthM";
            this.cmbFifthM.Size = new System.Drawing.Size(185, 21);
            this.cmbFifthM.TabIndex = 0;
            this.cmbFifthM.SelectedIndexChanged += new System.EventHandler(this.cmb_MeasurementSelectedIndexchanged);
            this.cmbFifthM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWin_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "8th M";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "7th M";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "6th M";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "5th M";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(107, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Items";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(258, 55);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(121, 20);
            this.txtMachineCode.TabIndex = 1;
            this.txtMachineCode.TextChanged += new System.EventHandler(this.txt);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(81, 62);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Machine Code";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(81, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(170, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Please select a fabric product type";
            // 
            // cmbFabricProdType
            // 
            this.cmbFabricProdType.FormattingEnabled = true;
            this.cmbFabricProdType.Location = new System.Drawing.Point(258, 89);
            this.cmbFabricProdType.Name = "cmbFabricProdType";
            this.cmbFabricProdType.Size = new System.Drawing.Size(121, 21);
            this.cmbFabricProdType.TabIndex = 3;
            this.cmbFabricProdType.SelectedIndexChanged += new System.EventHandler(this.cmb_MandatorySelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cmbFinishedGoods);
            this.groupBox3.Controls.Add(this.txtAssetRegNo);
            this.groupBox3.Controls.Add(this.txtRealistic);
            this.groupBox3.Controls.Add(this.txtMaxCapacity);
            this.groupBox3.Controls.Add(this.txtSerialNo);
            this.groupBox3.Controls.Add(this.txtGLCostCentre);
            this.groupBox3.Controls.Add(this.txtMacineDescription);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Location = new System.Drawing.Point(37, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(738, 131);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Static Info";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtLastNumberUsed);
            this.groupBox4.Location = new System.Drawing.Point(256, 51);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(133, 71);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Last Number Used";
            // 
            // txtLastNumberUsed
            // 
            this.txtLastNumberUsed.Location = new System.Drawing.Point(11, 29);
            this.txtLastNumberUsed.Name = "txtLastNumberUsed";
            this.txtLastNumberUsed.Size = new System.Drawing.Size(100, 20);
            this.txtLastNumberUsed.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(711, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "%";
            // 
            // cmbFinishedGoods
            // 
            this.cmbFinishedGoods.FormattingEnabled = true;
            this.cmbFinishedGoods.Location = new System.Drawing.Point(527, 76);
            this.cmbFinishedGoods.Name = "cmbFinishedGoods";
            this.cmbFinishedGoods.Size = new System.Drawing.Size(121, 21);
            this.cmbFinishedGoods.TabIndex = 5;
            this.cmbFinishedGoods.SelectedIndexChanged += new System.EventHandler(this.cmb_MandatorySelectedIndexChanged);
            this.cmbFinishedGoods.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtAssetRegNo
            // 
            this.txtAssetRegNo.Location = new System.Drawing.Point(527, 105);
            this.txtAssetRegNo.Name = "txtAssetRegNo";
            this.txtAssetRegNo.Size = new System.Drawing.Size(172, 20);
            this.txtAssetRegNo.TabIndex = 6;
            this.txtAssetRegNo.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtRealistic
            // 
            this.txtRealistic.Location = new System.Drawing.Point(527, 50);
            this.txtRealistic.Name = "txtRealistic";
            this.txtRealistic.Size = new System.Drawing.Size(172, 20);
            this.txtRealistic.TabIndex = 4;
            this.txtRealistic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRealistic.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtMaxCapacity
            // 
            this.txtMaxCapacity.Location = new System.Drawing.Point(527, 25);
            this.txtMaxCapacity.Name = "txtMaxCapacity";
            this.txtMaxCapacity.Size = new System.Drawing.Size(172, 20);
            this.txtMaxCapacity.TabIndex = 3;
            this.txtMaxCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxCapacity.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(110, 77);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(100, 20);
            this.txtSerialNo.TabIndex = 2;
            this.txtSerialNo.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtGLCostCentre
            // 
            this.txtGLCostCentre.Location = new System.Drawing.Point(110, 50);
            this.txtGLCostCentre.Name = "txtGLCostCentre";
            this.txtGLCostCentre.Size = new System.Drawing.Size(100, 20);
            this.txtGLCostCentre.TabIndex = 1;
            this.txtGLCostCentre.TextChanged += new System.EventHandler(this.txt);
            // 
            // txtMacineDescription
            // 
            this.txtMacineDescription.Location = new System.Drawing.Point(110, 25);
            this.txtMacineDescription.Name = "txtMacineDescription";
            this.txtMacineDescription.Size = new System.Drawing.Size(279, 20);
            this.txtMacineDescription.TabIndex = 0;
            this.txtMacineDescription.TextChanged += new System.EventHandler(this.txt);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 80);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 13);
            this.label25.TabIndex = 6;
            this.label25.Text = "Serial No";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(20, 54);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "GL Cost Centre";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(419, 108);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(73, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Asset Reg No";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(419, 78);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Product";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(419, 53);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Realistic";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(419, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Capacity 100%";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(20, 28);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Description";
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.Location = new System.Drawing.Point(276, 497);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(82, 23);
            this.btnMaintenance.TabIndex = 21;
            this.btnMaintenance.Text = "Maintentance";
            this.btnMaintenance.UseVisualStyleBackColor = true;
            this.btnMaintenance.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 502);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Please select the appropriate maintenance";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(403, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Please select a greige poduct type";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(630, 524);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbGreigeProductType
            // 
            this.cmbGreigeProductType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGreigeProductType.FormattingEnabled = true;
            this.cmbGreigeProductType.Location = new System.Drawing.Point(595, 89);
            this.cmbGreigeProductType.Name = "cmbGreigeProductType";
            this.cmbGreigeProductType.Size = new System.Drawing.Size(164, 21);
            this.cmbGreigeProductType.TabIndex = 24;
            this.cmbGreigeProductType.Text = "Select Options";
            this.cmbGreigeProductType.SelectedIndexChanged += new System.EventHandler(this.cmbGreigeProductType_SelectedIndexChanged);
            // 
            // frmTLADM_MachineDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 558);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cmbGreigeProductType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMaintenance);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmbFabricProdType);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDepartmentSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCodeSelection);
            this.Name = "frmTLADM_MachineDefinition";
            this.Text = "Machine Update / Edit Facility";
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

        private System.Windows.Forms.ComboBox cmbCodeSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDepartmentSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMachineCode;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbFabricProdType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAssetRegNo;
        private System.Windows.Forms.TextBox txtRealistic;
        private System.Windows.Forms.TextBox txtMaxCapacity;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtGLCostCentre;
        private System.Windows.Forms.TextBox txtMacineDescription;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbFirstM;
        private System.Windows.Forms.ComboBox cmbFourthM;
        private System.Windows.Forms.ComboBox cmbThirdM;
        private System.Windows.Forms.ComboBox cmbSecondM;
        private System.Windows.Forms.ComboBox cmbEighthM;
        private System.Windows.Forms.ComboBox cmbSeventhM;
        private System.Windows.Forms.ComboBox cmbSixM;
        private System.Windows.Forms.ComboBox cmbFifthM;
        private System.Windows.Forms.ComboBox cmbFinishedGoods;
        private System.Windows.Forms.Button btnMaintenance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMeasure3Qty;
        private System.Windows.Forms.TextBox txtMeasure2Qty;
        private System.Windows.Forms.TextBox txtMeasure1Qty;
        private System.Windows.Forms.TextBox txtMeasure4Qty;
        private System.Windows.Forms.TextBox txtMeasure8Qty;
        private System.Windows.Forms.TextBox txtMeasure7Qty;
        private System.Windows.Forms.TextBox txtMeasure6Qty;
        private System.Windows.Forms.TextBox txtMeasure5Qty;
        private System.Windows.Forms.Label label5;
       // private System.Windows.Forms.ComboBox cmbGreigeProductType;
       //  private Administration.ColourCombo cmbGreigeProductType;
        private Administration.CheckComboBox cmbGreigeProductType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtLastNumberUsed;
        private System.Windows.Forms.Button btnDelete;
    }
}