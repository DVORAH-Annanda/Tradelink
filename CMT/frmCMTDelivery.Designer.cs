namespace CMT
{
    partial class frmCMTDelivery
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCMT = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.chkPrevious = new System.Windows.Forms.CheckBox();
            this.cmboPrevious = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboCurrentPIMultiple = new CMT.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CMT Delivery Note Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(192, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // cmboCMT
            // 
            this.cmboCMT.FormattingEnabled = true;
            this.cmboCMT.Location = new System.Drawing.Point(263, 15);
            this.cmboCMT.Name = "cmboCMT";
            this.cmboCMT.Size = new System.Drawing.Size(220, 21);
            this.cmboCMT.TabIndex = 13;
            this.cmboCMT.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentPI_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Current Panel Issues";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(535, 439);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(111, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 175);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "label6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Transfer Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(263, 119);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(132, 20);
            this.dtpTransDate.TabIndex = 17;
            // 
            // chkPrevious
            // 
            this.chkPrevious.AutoSize = true;
            this.chkPrevious.Location = new System.Drawing.Point(263, 170);
            this.chkPrevious.Name = "chkPrevious";
            this.chkPrevious.Size = new System.Drawing.Size(136, 17);
            this.chkPrevious.TabIndex = 20;
            this.chkPrevious.Text = "Previous Delivey Notes";
            this.chkPrevious.UseVisualStyleBackColor = true;
            this.chkPrevious.CheckedChanged += new System.EventHandler(this.chkPrevious_CheckedChanged);
            // 
            // cmboPrevious
            // 
            this.cmboPrevious.FormattingEnabled = true;
            this.cmboPrevious.Location = new System.Drawing.Point(263, 202);
            this.cmboPrevious.Name = "cmboPrevious";
            this.cmboPrevious.Size = new System.Drawing.Size(220, 21);
            this.cmboPrevious.TabIndex = 21;
            this.cmboPrevious.SelectedIndexChanged += new System.EventHandler(this.cmboPrevious_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Current CMT\'s";
            // 
            // cmboCurrentPIMultiple
            // 
            this.cmboCurrentPIMultiple.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCurrentPIMultiple.FormattingEnabled = true;
            this.cmboCurrentPIMultiple.Location = new System.Drawing.Point(263, 67);
            this.cmboCurrentPIMultiple.Name = "cmboCurrentPIMultiple";
            this.cmboCurrentPIMultiple.Size = new System.Drawing.Size(220, 21);
            this.cmboCurrentPIMultiple.TabIndex = 19;
            this.cmboCurrentPIMultiple.Text = "Select Options";
            this.cmboCurrentPIMultiple.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentPIMultiple_SelectedIndexChanged);
            // 
            // frmCMTDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 503);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmboPrevious);
            this.Controls.Add(this.chkPrevious);
            this.Controls.Add(this.cmboCurrentPIMultiple);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboCMT);
            this.Controls.Add(this.label9);
            this.Name = "frmCMTDelivery";
            this.Text = "CMT Delivery Note";
            this.Load += new System.EventHandler(this.frmCMTDelivery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCMT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private CMT.CheckComboBox cmboCurrentPIMultiple;
        private System.Windows.Forms.CheckBox chkPrevious;
        private System.Windows.Forms.ComboBox cmboPrevious;  // System.Windows.Forms.ComboBox cmboCurrentPIMultiple;
        private System.Windows.Forms.Label label8;
    }
}