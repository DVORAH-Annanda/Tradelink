namespace CMT
{
    partial class frmMPSel
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
            this.cmboDepartment = new CMT.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboMeasurement = new CMT.CheckComboBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboStyle = new CMT.CheckComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboSize = new CMT.CheckComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CMT Factory";
            // 
            // cmboDepartment
            // 
            this.cmboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDepartment.FormattingEnabled = true;
            this.cmboDepartment.Location = new System.Drawing.Point(244, 114);
            this.cmboDepartment.Name = "cmboDepartment";
            this.cmboDepartment.Size = new System.Drawing.Size(189, 21);
            this.cmboDepartment.TabIndex = 1;
            this.cmboDepartment.Text = "Select Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(520, 317);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Measurement Point";
            // 
            // cmboMeasurement
            // 
            this.cmboMeasurement.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboMeasurement.FormattingEnabled = true;
            this.cmboMeasurement.Location = new System.Drawing.Point(244, 162);
            this.cmboMeasurement.Name = "cmboMeasurement";
            this.cmboMeasurement.Size = new System.Drawing.Size(278, 21);
            this.cmboMeasurement.TabIndex = 4;
            this.cmboMeasurement.Text = "Select Options";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(244, 67);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(141, 20);
            this.dtpToDate.TabIndex = 11;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(244, 20);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(141, 20);
            this.dtpFromDate.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "To Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "From Date";
            // 
            // cmboStyle
            // 
            this.cmboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(244, 210);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(278, 21);
            this.cmboStyle.TabIndex = 15;
            this.cmboStyle.Text = "Select Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Style";
            // 
            // cmboSize
            // 
            this.cmboSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSize.FormattingEnabled = true;
            this.cmboSize.Location = new System.Drawing.Point(244, 258);
            this.cmboSize.Name = "cmboSize";
            this.cmboSize.Size = new System.Drawing.Size(278, 21);
            this.cmboSize.TabIndex = 17;
            this.cmboSize.Text = "Select Options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(123, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Size";
            // 
            // frmMPSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 362);
            this.Controls.Add(this.cmboSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.cmboMeasurement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboDepartment);
            this.Controls.Add(this.label1);
            this.Name = "frmMPSel";
            this.Text = "Measurement Data Reporting";
            this.Load += new System.EventHandler(this.frmMPSel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ComboBox cmboDepartment;
        private CMT.CheckComboBox cmboDepartment;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label2;
        private CMT.CheckComboBox cmboMeasurement;
        //private System.Windows.Forms.ComboBox cmboMeasurement;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private CheckComboBox cmboStyle;
        private System.Windows.Forms.Label label5;
        private CheckComboBox cmboSize;
        private System.Windows.Forms.Label label6;
    }
}