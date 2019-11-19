namespace Spinning
{
    partial class frmYarnAdjustment
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
            this.label13 = new System.Windows.Forms.Label();
            this.rtbReasons = new System.Windows.Forms.RichTextBox();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboPalletNumbers = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDeliveryNoteNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYarnOrderNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumberOfCones = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNettWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIdentification = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTwistFactor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTexCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtYarnType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbWriteOff = new System.Windows.Forms.RadioButton();
            this.rbWriteOn = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmboYarnNo = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(184, 455);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "Reasons";
            // 
            // rtbReasons
            // 
            this.rtbReasons.Location = new System.Drawing.Point(268, 455);
            this.rtbReasons.Name = "rtbReasons";
            this.rtbReasons.Size = new System.Drawing.Size(354, 96);
            this.rtbReasons.TabIndex = 47;
            this.rtbReasons.Text = "";
            this.rtbReasons.TextChanged += new System.EventHandler(this.rtb_ValueChanged);
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new System.Drawing.Point(399, 91);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(374, 20);
            this.txtApprovedBy.TabIndex = 46;
            this.txtApprovedBy.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Approved By";
            // 
            // cmboPalletNumbers
            // 
            this.cmboPalletNumbers.FormattingEnabled = true;
            this.cmboPalletNumbers.Location = new System.Drawing.Point(633, 135);
            this.cmboPalletNumbers.Name = "cmboPalletNumbers";
            this.cmboPalletNumbers.Size = new System.Drawing.Size(140, 21);
            this.cmboPalletNumbers.TabIndex = 44;
            this.cmboPalletNumbers.SelectedIndexChanged += new System.EventHandler(this.cmboPalletNumbers_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(542, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Pallet Number";
            // 
            // txtDeliveryNoteNumber
            // 
            this.txtDeliveryNoteNumber.Location = new System.Drawing.Point(399, 53);
            this.txtDeliveryNoteNumber.Name = "txtDeliveryNoteNumber";
            this.txtDeliveryNoteNumber.ReadOnly = true;
            this.txtDeliveryNoteNumber.Size = new System.Drawing.Size(100, 20);
            this.txtDeliveryNoteNumber.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Yarn Adjustment No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Please select a transaction date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYarnOrderNo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtNumberOfCones);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNettWeight);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtIdentification);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTwistFactor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTexCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtYarnType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(187, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 206);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // txtYarnOrderNo
            // 
            this.txtYarnOrderNo.Location = new System.Drawing.Point(335, 139);
            this.txtYarnOrderNo.Name = "txtYarnOrderNo";
            this.txtYarnOrderNo.ReadOnly = true;
            this.txtYarnOrderNo.Size = new System.Drawing.Size(78, 20);
            this.txtYarnOrderNo.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(231, 142);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Yarn Order Number";
            // 
            // txtNumberOfCones
            // 
            this.txtNumberOfCones.Location = new System.Drawing.Point(105, 133);
            this.txtNumberOfCones.Name = "txtNumberOfCones";
            this.txtNumberOfCones.Size = new System.Drawing.Size(60, 20);
            this.txtNumberOfCones.TabIndex = 19;
            this.txtNumberOfCones.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nett Weight";
            // 
            // txtNettWeight
            // 
            this.txtNettWeight.Location = new System.Drawing.Point(105, 177);
            this.txtNettWeight.Name = "txtNettWeight";
            this.txtNettWeight.Size = new System.Drawing.Size(83, 20);
            this.txtNettWeight.TabIndex = 18;
            this.txtNettWeight.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "No oF Cones";
            // 
            // txtIdentification
            // 
            this.txtIdentification.Location = new System.Drawing.Point(335, 92);
            this.txtIdentification.Name = "txtIdentification";
            this.txtIdentification.ReadOnly = true;
            this.txtIdentification.Size = new System.Drawing.Size(147, 20);
            this.txtIdentification.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Identification";
            // 
            // txtTwistFactor
            // 
            this.txtTwistFactor.Location = new System.Drawing.Point(105, 89);
            this.txtTwistFactor.Name = "txtTwistFactor";
            this.txtTwistFactor.ReadOnly = true;
            this.txtTwistFactor.Size = new System.Drawing.Size(100, 20);
            this.txtTwistFactor.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Twist Factor";
            // 
            // txtTexCount
            // 
            this.txtTexCount.Location = new System.Drawing.Point(335, 45);
            this.txtTexCount.Name = "txtTexCount";
            this.txtTexCount.ReadOnly = true;
            this.txtTexCount.Size = new System.Drawing.Size(100, 20);
            this.txtTexCount.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Tex Count";
            // 
            // txtYarnType
            // 
            this.txtYarnType.Location = new System.Drawing.Point(105, 45);
            this.txtYarnType.Name = "txtYarnType";
            this.txtYarnType.ReadOnly = true;
            this.txtYarnType.Size = new System.Drawing.Size(100, 20);
            this.txtYarnType.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Yarn Type";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(399, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(155, 20);
            this.dateTimePicker1.TabIndex = 36;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbWriteOff);
            this.groupBox2.Controls.Add(this.rbWriteOn);
            this.groupBox2.Location = new System.Drawing.Point(400, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 51);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transaction Type";
            // 
            // rbWriteOff
            // 
            this.rbWriteOff.AutoSize = true;
            this.rbWriteOff.Location = new System.Drawing.Point(93, 19);
            this.rbWriteOff.Name = "rbWriteOff";
            this.rbWriteOff.Size = new System.Drawing.Size(67, 17);
            this.rbWriteOff.TabIndex = 1;
            this.rbWriteOff.TabStop = true;
            this.rbWriteOff.Text = "Write Off";
            this.rbWriteOff.UseVisualStyleBackColor = true;
            // 
            // rbWriteOn
            // 
            this.rbWriteOn.AutoSize = true;
            this.rbWriteOn.Location = new System.Drawing.Point(14, 19);
            this.rbWriteOn.Name = "rbWriteOn";
            this.rbWriteOn.Size = new System.Drawing.Size(67, 17);
            this.rbWriteOn.TabIndex = 0;
            this.rbWriteOn.TabStop = true;
            this.rbWriteOn.Text = "Write On";
            this.rbWriteOn.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(804, 561);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmboYarnNo
            // 
            this.cmboYarnNo.FormattingEnabled = true;
            this.cmboYarnNo.Location = new System.Drawing.Point(360, 135);
            this.cmboYarnNo.Name = "cmboYarnNo";
            this.cmboYarnNo.Size = new System.Drawing.Size(121, 21);
            this.cmboYarnNo.TabIndex = 51;
            this.cmboYarnNo.SelectedIndexChanged += new System.EventHandler(this.cmboYarnNo_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(292, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 52;
            this.label14.Text = "Yarn No";
            // 
            // frmYarnAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 614);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmboYarnNo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rtbReasons);
            this.Controls.Add(this.txtApprovedBy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboPalletNumbers);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDeliveryNoteNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmYarnAdjustment";
            this.Text = "Yarn Ajustment Transaction";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Adjustment_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox rtbReasons;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboPalletNumbers;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDeliveryNoteNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtYarnOrderNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumberOfCones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNettWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIdentification;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTwistFactor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTexCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtYarnType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbWriteOff;
        private System.Windows.Forms.RadioButton rbWriteOn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboYarnNo;
        private System.Windows.Forms.Label label14;
    }
}