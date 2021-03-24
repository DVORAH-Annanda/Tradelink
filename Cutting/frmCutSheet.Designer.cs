namespace Cutting
{
    partial class frmCutSheet
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
            this.txtLastNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoBinding = new System.Windows.Forms.TextBox();
            this.txtNoTrims = new System.Windows.Forms.TextBox();
            this.txtNoGarments = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRequiredDate = new System.Windows.Forms.DateTimePicker();
            this.btnDyeBatch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.cmboLabels = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmboStyles = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtQualTrim2 = new System.Windows.Forms.TextBox();
            this.txtQualTrim1 = new System.Windows.Forms.TextBox();
            this.txtQualBody = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTrimKg = new System.Windows.Forms.TextBox();
            this.txtQtyKg = new System.Windows.Forms.TextBox();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTrimRating = new System.Windows.Forms.TextBox();
            this.txtBodyRating = new System.Windows.Forms.TextBox();
            this.cmboRatingTrims = new System.Windows.Forms.ComboBox();
            this.cmboRating = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.ChkAccepted = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDyeBatchNumber = new System.Windows.Forms.TextBox();
            this.cmboDepartment = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.chkSample = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmboDownSize = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.chkDownSize = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLastNumber
            // 
            this.txtLastNumber.Location = new System.Drawing.Point(962, 7);
            this.txtLastNumber.Name = "txtLastNumber";
            this.txtLastNumber.ReadOnly = true;
            this.txtLastNumber.Size = new System.Drawing.Size(151, 20);
            this.txtLastNumber.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select a Dye batch";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNoBinding);
            this.groupBox1.Controls.Add(this.txtNoTrims);
            this.groupBox1.Controls.Add(this.txtNoGarments);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(883, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtNoBinding
            // 
            this.txtNoBinding.Location = new System.Drawing.Point(114, 48);
            this.txtNoBinding.Name = "txtNoBinding";
            this.txtNoBinding.ReadOnly = true;
            this.txtNoBinding.Size = new System.Drawing.Size(100, 20);
            this.txtNoBinding.TabIndex = 5;
            this.txtNoBinding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNoTrims
            // 
            this.txtNoTrims.Location = new System.Drawing.Point(114, 76);
            this.txtNoTrims.Name = "txtNoTrims";
            this.txtNoTrims.ReadOnly = true;
            this.txtNoTrims.Size = new System.Drawing.Size(100, 20);
            this.txtNoTrims.TabIndex = 4;
            this.txtNoTrims.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNoGarments
            // 
            this.txtNoGarments.Location = new System.Drawing.Point(114, 20);
            this.txtNoGarments.Name = "txtNoGarments";
            this.txtNoGarments.ReadOnly = true;
            this.txtNoGarments.Size = new System.Drawing.Size(100, 20);
            this.txtNoGarments.TabIndex = 3;
            this.txtNoGarments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Trims";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Binding";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Garments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ordered";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Required";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Location = new System.Drawing.Point(102, 85);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(118, 20);
            this.dtpOrderDate.TabIndex = 5;
            // 
            // dtpRequiredDate
            // 
            this.dtpRequiredDate.Location = new System.Drawing.Point(102, 119);
            this.dtpRequiredDate.Name = "dtpRequiredDate";
            this.dtpRequiredDate.Size = new System.Drawing.Size(118, 20);
            this.dtpRequiredDate.TabIndex = 6;
            // 
            // btnDyeBatch
            // 
            this.btnDyeBatch.Location = new System.Drawing.Point(171, 7);
            this.btnDyeBatch.Name = "btnDyeBatch";
            this.btnDyeBatch.Size = new System.Drawing.Size(49, 23);
            this.btnDyeBatch.TabIndex = 7;
            this.btnDyeBatch.Text = "Dye Batch";
            this.btnDyeBatch.UseVisualStyleBackColor = true;
            this.btnDyeBatch.Click += new System.EventHandler(this.btnDyeBatch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 196);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(846, 180);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Body";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Binding";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Trims 2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboColour);
            this.groupBox2.Controls.Add(this.cmboLabels);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cmboStyles);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(17, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 137);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // cmboColour
            // 
            this.cmboColour.Enabled = false;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(51, 19);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(127, 21);
            this.cmboColour.TabIndex = 6;
            this.cmboColour.Visible = false;
            // 
            // cmboLabels
            // 
            this.cmboLabels.Enabled = false;
            this.cmboLabels.FormattingEnabled = true;
            this.cmboLabels.Location = new System.Drawing.Point(51, 93);
            this.cmboLabels.Name = "cmboLabels";
            this.cmboLabels.Size = new System.Drawing.Size(127, 21);
            this.cmboLabels.TabIndex = 5;
            this.cmboLabels.Visible = false;
            this.cmboLabels.SelectedIndexChanged += new System.EventHandler(this.cmboLabels_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Label";
            this.label12.Visible = false;
            // 
            // cmboStyles
            // 
            this.cmboStyles.Enabled = false;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(51, 56);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(127, 21);
            this.cmboStyles.TabIndex = 3;
            this.cmboStyles.Visible = false;
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Styles";
            this.label11.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Colour";
            this.label7.Visible = false;
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(956, 215);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(157, 129);
            this.rtbNotes.TabIndex = 13;
            this.rtbNotes.Text = "";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(229, 396);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(524, 189);
            this.dataGridView2.TabIndex = 14;
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtQualTrim2);
            this.groupBox3.Controls.Add(this.txtQualTrim1);
            this.groupBox3.Controls.Add(this.txtQualBody);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(252, 44);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 130);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quality";
            // 
            // txtQualTrim2
            // 
            this.txtQualTrim2.Location = new System.Drawing.Point(95, 90);
            this.txtQualTrim2.Name = "txtQualTrim2";
            this.txtQualTrim2.ReadOnly = true;
            this.txtQualTrim2.Size = new System.Drawing.Size(141, 20);
            this.txtQualTrim2.TabIndex = 14;
            // 
            // txtQualTrim1
            // 
            this.txtQualTrim1.Location = new System.Drawing.Point(95, 56);
            this.txtQualTrim1.Name = "txtQualTrim1";
            this.txtQualTrim1.ReadOnly = true;
            this.txtQualTrim1.Size = new System.Drawing.Size(141, 20);
            this.txtQualTrim1.TabIndex = 13;
            // 
            // txtQualBody
            // 
            this.txtQualBody.Location = new System.Drawing.Point(95, 19);
            this.txtQualBody.Name = "txtQualBody";
            this.txtQualBody.ReadOnly = true;
            this.txtQualBody.Size = new System.Drawing.Size(141, 20);
            this.txtQualBody.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Customer ";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(91, 149);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(155, 20);
            this.txtCustomer.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTrimKg);
            this.groupBox4.Controls.Add(this.txtQtyKg);
            this.groupBox4.Controls.Add(this.txtCustomerOrder);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtTrimRating);
            this.groupBox4.Controls.Add(this.txtBodyRating);
            this.groupBox4.Controls.Add(this.cmboRatingTrims);
            this.groupBox4.Controls.Add(this.cmboRating);
            this.groupBox4.Location = new System.Drawing.Point(511, 44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(352, 130);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            // 
            // txtTrimKg
            // 
            this.txtTrimKg.Location = new System.Drawing.Point(20, 67);
            this.txtTrimKg.Name = "txtTrimKg";
            this.txtTrimKg.ReadOnly = true;
            this.txtTrimKg.Size = new System.Drawing.Size(56, 20);
            this.txtTrimKg.TabIndex = 9;
            this.txtTrimKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQtyKg
            // 
            this.txtQtyKg.Location = new System.Drawing.Point(20, 30);
            this.txtQtyKg.Name = "txtQtyKg";
            this.txtQtyKg.ReadOnly = true;
            this.txtQtyKg.Size = new System.Drawing.Size(56, 20);
            this.txtQtyKg.TabIndex = 8;
            this.txtQtyKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(103, 102);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.Size = new System.Drawing.Size(153, 20);
            this.txtCustomerOrder.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Qty Kg";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(100, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Rating";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Customer Order";
            // 
            // txtTrimRating
            // 
            this.txtTrimRating.Location = new System.Drawing.Point(97, 68);
            this.txtTrimRating.Name = "txtTrimRating";
            this.txtTrimRating.ReadOnly = true;
            this.txtTrimRating.Size = new System.Drawing.Size(55, 20);
            this.txtTrimRating.TabIndex = 3;
            // 
            // txtBodyRating
            // 
            this.txtBodyRating.Location = new System.Drawing.Point(97, 30);
            this.txtBodyRating.Name = "txtBodyRating";
            this.txtBodyRating.ReadOnly = true;
            this.txtBodyRating.Size = new System.Drawing.Size(55, 20);
            this.txtBodyRating.TabIndex = 2;
            // 
            // cmboRatingTrims
            // 
            this.cmboRatingTrims.Enabled = false;
            this.cmboRatingTrims.FormattingEnabled = true;
            this.cmboRatingTrims.Location = new System.Drawing.Point(166, 69);
            this.cmboRatingTrims.Name = "cmboRatingTrims";
            this.cmboRatingTrims.Size = new System.Drawing.Size(170, 21);
            this.cmboRatingTrims.TabIndex = 1;
            this.cmboRatingTrims.Visible = false;
            this.cmboRatingTrims.SelectedIndexChanged += new System.EventHandler(this.cmboRatingTrims_SelectedIndexChanged);
            // 
            // cmboRating
            // 
            this.cmboRating.Enabled = false;
            this.cmboRating.FormattingEnabled = true;
            this.cmboRating.Location = new System.Drawing.Point(166, 29);
            this.cmboRating.Name = "cmboRating";
            this.cmboRating.Size = new System.Drawing.Size(170, 21);
            this.cmboRating.TabIndex = 0;
            this.cmboRating.Visible = false;
            this.cmboRating.SelectedIndexChanged += new System.EventHandler(this.cmboRating_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(852, 543);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(252, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Current CutSheets";
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(351, 6);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(137, 21);
            this.cmboCutSheet.TabIndex = 20;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // ChkAccepted
            // 
            this.ChkAccepted.AutoSize = true;
            this.ChkAccepted.Location = new System.Drawing.Point(847, 510);
            this.ChkAccepted.Name = "ChkAccepted";
            this.ChkAccepted.Size = new System.Drawing.Size(122, 17);
            this.ChkAccepted.TabIndex = 21;
            this.ChkAccepted.Text = "Cut Sheet Accepted";
            this.ChkAccepted.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(545, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 13);
            this.label18.TabIndex = 24;
            this.label18.Text = "Dye Batch Number";
            // 
            // txtDyeBatchNumber
            // 
            this.txtDyeBatchNumber.Location = new System.Drawing.Point(667, 10);
            this.txtDyeBatchNumber.Name = "txtDyeBatchNumber";
            this.txtDyeBatchNumber.ReadOnly = true;
            this.txtDyeBatchNumber.Size = new System.Drawing.Size(128, 20);
            this.txtDyeBatchNumber.TabIndex = 25;
            // 
            // cmboDepartment
            // 
            this.cmboDepartment.FormattingEnabled = true;
            this.cmboDepartment.Location = new System.Drawing.Point(992, 44);
            this.cmboDepartment.Name = "cmboDepartment";
            this.cmboDepartment.Size = new System.Drawing.Size(121, 21);
            this.cmboDepartment.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(917, 47);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 13);
            this.label19.TabIndex = 27;
            this.label19.Text = "Department";
            // 
            // chkSample
            // 
            this.chkSample.AutoSize = true;
            this.chkSample.Location = new System.Drawing.Point(36, 44);
            this.chkSample.Name = "chkSample";
            this.chkSample.Size = new System.Drawing.Size(64, 17);
            this.chkSample.TabIndex = 28;
            this.chkSample.Text = "Sample ";
            this.chkSample.UseVisualStyleBackColor = true;
            this.chkSample.CheckedChanged += new System.EventHandler(this.chkBIF_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmboDownSize);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Location = new System.Drawing.Point(17, 559);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(190, 67);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            // 
            // cmboDownSize
            // 
            this.cmboDownSize.Enabled = false;
            this.cmboDownSize.FormattingEnabled = true;
            this.cmboDownSize.Location = new System.Drawing.Point(51, 30);
            this.cmboDownSize.Name = "cmboDownSize";
            this.cmboDownSize.Size = new System.Drawing.Size(127, 21);
            this.cmboDownSize.TabIndex = 6;
            this.cmboDownSize.SelectedIndexChanged += new System.EventHandler(this.cmboDownSize_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 34);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(27, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Size";
            // 
            // chkDownSize
            // 
            this.chkDownSize.AutoSize = true;
            this.chkDownSize.Location = new System.Drawing.Point(17, 542);
            this.chkDownSize.Name = "chkDownSize";
            this.chkDownSize.Size = new System.Drawing.Size(113, 17);
            this.chkDownSize.TabIndex = 30;
            this.chkDownSize.Text = "Re Size Cut Sheet";
            this.chkDownSize.UseVisualStyleBackColor = true;
            this.chkDownSize.CheckedChanged += new System.EventHandler(this.chkDownSize_CheckedChanged);
            // 
            // frmCutSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 638);
            this.Controls.Add(this.chkDownSize);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.chkSample);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cmboDepartment);
            this.Controls.Add(this.txtDyeBatchNumber);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.ChkAccepted);
            this.Controls.Add(this.cmboCutSheet);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDyeBatch);
            this.Controls.Add(this.dtpRequiredDate);
            this.Controls.Add(this.dtpOrderDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLastNumber);
            this.Name = "frmCutSheet";
            this.Text = "Cut Sheet";
            this.Load += new System.EventHandler(this.frmCutSheet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLastNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.DateTimePicker dtpRequiredDate;
        private System.Windows.Forms.Button btnDyeBatch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtNoBinding;
        private System.Windows.Forms.TextBox txtNoTrims;
        private System.Windows.Forms.TextBox txtNoGarments;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmboLabels;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtQualTrim2;
        private System.Windows.Forms.TextBox txtQualTrim1;
        private System.Windows.Forms.TextBox txtQualBody;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTrimKg;
        private System.Windows.Forms.TextBox txtQtyKg;
        private System.Windows.Forms.TextBox txtCustomerOrder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTrimRating;
        private System.Windows.Forms.TextBox txtBodyRating;
        private System.Windows.Forms.ComboBox cmboRatingTrims;
        private System.Windows.Forms.ComboBox cmboRating;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.CheckBox ChkAccepted;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDyeBatchNumber;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboDepartment;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chkSample;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmboDownSize;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chkDownSize;
    }
}

