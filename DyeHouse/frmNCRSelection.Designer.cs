namespace DyeHouse
{
    partial class frmNCRSelection
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboNCRNumber = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboDyeMachine = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmboDyeOperator = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboCause = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmboRemedy = new System.Windows.Forms.ComboBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(174, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Selection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(104, 22);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(129, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(104, 54);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(129, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboRemedy);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmboCause);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmboDyeOperator);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmboDyeMachine);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmboColour);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmboProduct);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmboNCRNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(174, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 294);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection criteria";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(485, 510);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "NCR Number";
            // 
            // cmboNCRNumber
            // 
            this.cmboNCRNumber.FormattingEnabled = true;
            this.cmboNCRNumber.Location = new System.Drawing.Point(104, 37);
            this.cmboNCRNumber.Name = "cmboNCRNumber";
            this.cmboNCRNumber.Size = new System.Drawing.Size(121, 21);
            this.cmboNCRNumber.TabIndex = 1;
            this.cmboNCRNumber.SelectedIndexChanged += new System.EventHandler(this.cmboNCRNumber_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Product";
            // 
            // cmboProduct
            // 
            this.cmboProduct.FormattingEnabled = true;
            this.cmboProduct.Location = new System.Drawing.Point(104, 74);
            this.cmboProduct.Name = "cmboProduct";
            this.cmboProduct.Size = new System.Drawing.Size(121, 21);
            this.cmboProduct.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Colour";
            // 
            // cmboColour
            // 
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(104, 111);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(121, 21);
            this.cmboColour.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Dye Machine";
            // 
            // cmboDyeMachine
            // 
            this.cmboDyeMachine.FormattingEnabled = true;
            this.cmboDyeMachine.Location = new System.Drawing.Point(104, 148);
            this.cmboDyeMachine.Name = "cmboDyeMachine";
            this.cmboDyeMachine.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeMachine.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Operator";
            // 
            // cmboDyeOperator
            // 
            this.cmboDyeOperator.FormattingEnabled = true;
            this.cmboDyeOperator.Location = new System.Drawing.Point(104, 185);
            this.cmboDyeOperator.Name = "cmboDyeOperator";
            this.cmboDyeOperator.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOperator.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 225);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Cause";
            // 
            // cmboCause
            // 
            this.cmboCause.FormattingEnabled = true;
            this.cmboCause.Location = new System.Drawing.Point(104, 222);
            this.cmboCause.Name = "cmboCause";
            this.cmboCause.Size = new System.Drawing.Size(121, 21);
            this.cmboCause.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 262);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Remedy";
            // 
            // cmboRemedy
            // 
            this.cmboRemedy.FormattingEnabled = true;
            this.cmboRemedy.Location = new System.Drawing.Point(104, 259);
            this.cmboRemedy.Name = "cmboRemedy";
            this.cmboRemedy.Size = new System.Drawing.Size(121, 21);
            this.cmboRemedy.TabIndex = 13;
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(278, 478);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(121, 21);
            this.cmboReportOptions.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 486);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Sort options";
            // 
            // frmNCRSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 560);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmNCRSelection";
            this.Text = "NCR Selection";
            this.Load += new System.EventHandler(this.frmNCRSelection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmboRemedy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboCause;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmboDyeOperator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmboDyeMachine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboProduct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboNCRNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label10;
    }
}