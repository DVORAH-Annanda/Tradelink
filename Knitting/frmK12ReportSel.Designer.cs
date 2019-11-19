namespace Knitting
{
    partial class frmK12ReportSel
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbKnitMachine = new System.Windows.Forms.RadioButton();
            this.rbYarnOrder = new System.Windows.Forms.RadioButton();
            this.rbOperator = new System.Windows.Forms.RadioButton();
            this.rbSpinning = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(257, 50);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(149, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(257, 101);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(149, 20);
            this.dtpToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(556, 304);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSpinning);
            this.groupBox1.Controls.Add(this.rbOperator);
            this.groupBox1.Controls.Add(this.rbYarnOrder);
            this.groupBox1.Controls.Add(this.rbKnitMachine);
            this.groupBox1.Location = new System.Drawing.Point(155, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 129);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sort Order";
            // 
            // rbKnitMachine
            // 
            this.rbKnitMachine.AutoSize = true;
            this.rbKnitMachine.Location = new System.Drawing.Point(16, 25);
            this.rbKnitMachine.Name = "rbKnitMachine";
            this.rbKnitMachine.Size = new System.Drawing.Size(87, 17);
            this.rbKnitMachine.TabIndex = 0;
            this.rbKnitMachine.TabStop = true;
            this.rbKnitMachine.Text = "Knit Machine";
            this.rbKnitMachine.UseVisualStyleBackColor = true;
            // 
            // rbYarnOrder
            // 
            this.rbYarnOrder.AutoSize = true;
            this.rbYarnOrder.Location = new System.Drawing.Point(16, 61);
            this.rbYarnOrder.Name = "rbYarnOrder";
            this.rbYarnOrder.Size = new System.Drawing.Size(74, 17);
            this.rbYarnOrder.TabIndex = 1;
            this.rbYarnOrder.TabStop = true;
            this.rbYarnOrder.Text = "Yarn order";
            this.rbYarnOrder.UseVisualStyleBackColor = true;
            // 
            // rbOperator
            // 
            this.rbOperator.AutoSize = true;
            this.rbOperator.Location = new System.Drawing.Point(16, 97);
            this.rbOperator.Name = "rbOperator";
            this.rbOperator.Size = new System.Drawing.Size(66, 17);
            this.rbOperator.TabIndex = 2;
            this.rbOperator.TabStop = true;
            this.rbOperator.Text = "Operator";
            this.rbOperator.UseVisualStyleBackColor = true;
            // 
            // rbSpinning
            // 
            this.rbSpinning.AutoSize = true;
            this.rbSpinning.Location = new System.Drawing.Point(166, 25);
            this.rbSpinning.Name = "rbSpinning";
            this.rbSpinning.Size = new System.Drawing.Size(110, 17);
            this.rbSpinning.TabIndex = 3;
            this.rbSpinning.TabStop = true;
            this.rbSpinning.Text = "Spinning Machine";
            this.rbSpinning.UseVisualStyleBackColor = true;
            // 
            // frmK12ReportSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmK12ReportSel";
            this.Text = "C Grade report for Period ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSpinning;
        private System.Windows.Forms.RadioButton rbOperator;
        private System.Windows.Forms.RadioButton rbYarnOrder;
        private System.Windows.Forms.RadioButton rbKnitMachine;
    }
}