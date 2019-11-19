namespace Spinning
{
    partial class frmYarnIssueToKnitting
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeliveryNoteNumber = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmboPalletNumbers = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.comboYarnNo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(434, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(155, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(224, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 206);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Please select a transaction date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Delivery Note Number";
            // 
            // txtDeliveryNoteNumber
            // 
            this.txtDeliveryNoteNumber.Location = new System.Drawing.Point(434, 47);
            this.txtDeliveryNoteNumber.Name = "txtDeliveryNoteNumber";
            this.txtDeliveryNoteNumber.ReadOnly = true;
            this.txtDeliveryNoteNumber.Size = new System.Drawing.Size(100, 20);
            this.txtDeliveryNoteNumber.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(538, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Pallet Number";
            // 
            // cmboPalletNumbers
            // 
            this.cmboPalletNumbers.FormattingEnabled = true;
            this.cmboPalletNumbers.Location = new System.Drawing.Point(625, 124);
            this.cmboPalletNumbers.Name = "cmboPalletNumbers";
            this.cmboPalletNumbers.Size = new System.Drawing.Size(98, 21);
            this.cmboPalletNumbers.TabIndex = 21;
            this.cmboPalletNumbers.SelectedIndexChanged += new System.EventHandler(this.cmboPalletNumbers_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(747, 411);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // comboYarnNo
            // 
            this.comboYarnNo.FormattingEnabled = true;
            this.comboYarnNo.Location = new System.Drawing.Point(383, 124);
            this.comboYarnNo.Name = "comboYarnNo";
            this.comboYarnNo.Size = new System.Drawing.Size(121, 21);
            this.comboYarnNo.TabIndex = 23;
            this.comboYarnNo.SelectedIndexChanged += new System.EventHandler(this.comboYarnNo_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(275, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Yarn Order No";
            // 
            // frmYarnIssueToKnitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 457);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboYarnNo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboPalletNumbers);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDeliveryNoteNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmYarnIssueToKnitting";
            this.Text = "Yarn Availability";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Issue_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryNoteNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmboPalletNumbers;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboYarnNo;
        private System.Windows.Forms.Label label13;
    }
}