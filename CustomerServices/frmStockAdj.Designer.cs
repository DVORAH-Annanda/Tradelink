namespace CustomerServices
{
    partial class frmStockAdj
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
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReasons = new System.Windows.Forms.TextBox();
            this.txtAuthorisedBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxedQty = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWriteOff = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(603, 537);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date of Transaction";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(232, 87);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(140, 20);
            this.dtpTransDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 461);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Reasons";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 505);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Authorised By";
            // 
            // txtReasons
            // 
            this.txtReasons.Location = new System.Drawing.Point(232, 459);
            this.txtReasons.Name = "txtReasons";
            this.txtReasons.Size = new System.Drawing.Size(270, 20);
            this.txtReasons.TabIndex = 8;
            // 
            // txtAuthorisedBy
            // 
            this.txtAuthorisedBy.Location = new System.Drawing.Point(232, 502);
            this.txtAuthorisedBy.Name = "txtAuthorisedBy";
            this.txtAuthorisedBy.Size = new System.Drawing.Size(270, 20);
            this.txtAuthorisedBy.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Transaction Number";
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Location = new System.Drawing.Point(232, 42);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTransNumber.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWriteOff);
            this.groupBox1.Controls.Add(this.txtBoxedQty);
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.txtStyle);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBoxNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(166, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 291);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Box Details";
            // 
            // txtBoxedQty
            // 
            this.txtBoxedQty.Location = new System.Drawing.Point(92, 212);
            this.txtBoxedQty.Name = "txtBoxedQty";
            this.txtBoxedQty.Size = new System.Drawing.Size(112, 20);
            this.txtBoxedQty.TabIndex = 9;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(92, 167);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(210, 20);
            this.txtSize.TabIndex = 8;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(92, 122);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(210, 20);
            this.txtColour.TabIndex = 7;
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(92, 77);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(210, 20);
            this.txtStyle.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Boxed Qty";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Colour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Style";
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.Location = new System.Drawing.Point(92, 32);
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.Size = new System.Drawing.Size(169, 20);
            this.txtBoxNumber.TabIndex = 1;
            this.txtBoxNumber.TextChanged += new System.EventHandler(this.txtBoxNumber_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Box Number";
            // 
            // chkWriteOff
            // 
            this.chkWriteOff.AutoSize = true;
            this.chkWriteOff.Location = new System.Drawing.Point(24, 257);
            this.chkWriteOff.Name = "chkWriteOff";
            this.chkWriteOff.Size = new System.Drawing.Size(89, 17);
            this.chkWriteOff.TabIndex = 10;
            this.chkWriteOff.Text = "Box Write Off";
            this.chkWriteOff.UseVisualStyleBackColor = true;
            // 
            // frmStockAdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 572);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTransNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAuthorisedBy);
            this.Controls.Add(this.txtReasons);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Name = "frmStockAdj";
            this.Text = "Stock Adjustment Transaction";
            this.Load += new System.EventHandler(this.frmStockAdj_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.ComboBox cmboWarehouse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReasons;
        private System.Windows.Forms.TextBox txtAuthorisedBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTransNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtBoxedQty;
        private System.Windows.Forms.CheckBox chkWriteOff;
    }
}