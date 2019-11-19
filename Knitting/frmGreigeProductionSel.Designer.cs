namespace Knitting
{
    partial class frmGreigeProductionSel
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
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbKnittingCustomer = new System.Windows.Forms.ComboBox();
            this.cmbGreigeProduct = new System.Windows.Forms.ComboBox();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.cmbMachines = new System.Windows.Forms.ComboBox();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.rbGreigeProductAll = new System.Windows.Forms.RadioButton();
            this.rbGradeAll = new System.Windows.Forms.RadioButton();
            this.rbMachineAll = new System.Windows.Forms.RadioButton();
            this.rbOperatorAll = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGPByMachine = new System.Windows.Forms.RadioButton();
            this.rbGPByTotal = new System.Windows.Forms.RadioButton();
            this.rbGPByGrade = new System.Windows.Forms.RadioButton();
            this.rbGPOperator = new System.Windows.Forms.RadioButton();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date From ";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(273, 40);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(273, 76);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(131, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Knitting Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Greige Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Grade";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Machine";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Operator";
            // 
            // cmbKnittingCustomer
            // 
            this.cmbKnittingCustomer.FormattingEnabled = true;
            this.cmbKnittingCustomer.Location = new System.Drawing.Point(273, 127);
            this.cmbKnittingCustomer.Name = "cmbKnittingCustomer";
            this.cmbKnittingCustomer.Size = new System.Drawing.Size(121, 21);
            this.cmbKnittingCustomer.TabIndex = 9;
            // 
            // cmbGreigeProduct
            // 
            this.cmbGreigeProduct.FormattingEnabled = true;
            this.cmbGreigeProduct.Location = new System.Drawing.Point(273, 166);
            this.cmbGreigeProduct.Name = "cmbGreigeProduct";
            this.cmbGreigeProduct.Size = new System.Drawing.Size(121, 21);
            this.cmbGreigeProduct.TabIndex = 10;
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(273, 200);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(100, 20);
            this.txtGrade.TabIndex = 11;
            // 
            // cmbMachines
            // 
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(273, 234);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(121, 21);
            this.cmbMachines.TabIndex = 12;
            // 
            // cmbOperator
            // 
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(273, 268);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(121, 21);
            this.cmbOperator.TabIndex = 13;
            // 
            // rbGreigeProductAll
            // 
            this.rbGreigeProductAll.AutoSize = true;
            this.rbGreigeProductAll.Location = new System.Drawing.Point(427, 168);
            this.rbGreigeProductAll.Name = "rbGreigeProductAll";
            this.rbGreigeProductAll.Size = new System.Drawing.Size(36, 17);
            this.rbGreigeProductAll.TabIndex = 14;
            this.rbGreigeProductAll.TabStop = true;
            this.rbGreigeProductAll.Text = "All";
            this.rbGreigeProductAll.UseVisualStyleBackColor = true;
            // 
            // rbGradeAll
            // 
            this.rbGradeAll.AutoSize = true;
            this.rbGradeAll.Location = new System.Drawing.Point(427, 201);
            this.rbGradeAll.Name = "rbGradeAll";
            this.rbGradeAll.Size = new System.Drawing.Size(36, 17);
            this.rbGradeAll.TabIndex = 15;
            this.rbGradeAll.TabStop = true;
            this.rbGradeAll.Text = "All";
            this.rbGradeAll.UseVisualStyleBackColor = true;
            // 
            // rbMachineAll
            // 
            this.rbMachineAll.AutoSize = true;
            this.rbMachineAll.Location = new System.Drawing.Point(427, 234);
            this.rbMachineAll.Name = "rbMachineAll";
            this.rbMachineAll.Size = new System.Drawing.Size(36, 17);
            this.rbMachineAll.TabIndex = 16;
            this.rbMachineAll.TabStop = true;
            this.rbMachineAll.Text = "All";
            this.rbMachineAll.UseVisualStyleBackColor = true;
            // 
            // rbOperatorAll
            // 
            this.rbOperatorAll.AutoSize = true;
            this.rbOperatorAll.Location = new System.Drawing.Point(427, 269);
            this.rbOperatorAll.Name = "rbOperatorAll";
            this.rbOperatorAll.Size = new System.Drawing.Size(36, 17);
            this.rbOperatorAll.TabIndex = 17;
            this.rbOperatorAll.TabStop = true;
            this.rbOperatorAll.Text = "All";
            this.rbOperatorAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGPOperator);
            this.groupBox1.Controls.Add(this.rbGPByGrade);
            this.groupBox1.Controls.Add(this.rbGPByTotal);
            this.groupBox1.Controls.Add(this.rbGPByMachine);
            this.groupBox1.Location = new System.Drawing.Point(121, 308);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 103);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Sort Options";
            // 
            // rbGPByMachine
            // 
            this.rbGPByMachine.AutoSize = true;
            this.rbGPByMachine.Location = new System.Drawing.Point(24, 19);
            this.rbGPByMachine.Name = "rbGPByMachine";
            this.rbGPByMachine.Size = new System.Drawing.Size(155, 17);
            this.rbGPByMachine.TabIndex = 0;
            this.rbGPByMachine.TabStop = true;
            this.rbGPByMachine.Text = "Greige Product By Machine";
            this.rbGPByMachine.UseVisualStyleBackColor = true;
            // 
            // rbGPByTotal
            // 
            this.rbGPByTotal.AutoSize = true;
            this.rbGPByTotal.Location = new System.Drawing.Point(210, 19);
            this.rbGPByTotal.Name = "rbGPByTotal";
            this.rbGPByTotal.Size = new System.Drawing.Size(244, 17);
            this.rbGPByTotal.TabIndex = 1;
            this.rbGPByTotal.TabStop = true;
            this.rbGPByTotal.Text = "By Total for each Greige Product No Machnes";
            this.rbGPByTotal.UseVisualStyleBackColor = true;
            // 
            // rbGPByGrade
            // 
            this.rbGPByGrade.AutoSize = true;
            this.rbGPByGrade.Location = new System.Drawing.Point(24, 60);
            this.rbGPByGrade.Name = "rbGPByGrade";
            this.rbGPByGrade.Size = new System.Drawing.Size(69, 17);
            this.rbGPByGrade.TabIndex = 2;
            this.rbGPByGrade.TabStop = true;
            this.rbGPByGrade.Text = "By Grade";
            this.rbGPByGrade.UseVisualStyleBackColor = true;
            // 
            // rbGPOperator
            // 
            this.rbGPOperator.AutoSize = true;
            this.rbGPOperator.Location = new System.Drawing.Point(210, 60);
            this.rbGPOperator.Name = "rbGPOperator";
            this.rbGPOperator.Size = new System.Drawing.Size(128, 17);
            this.rbGPOperator.TabIndex = 3;
            this.rbGPOperator.TabStop = true;
            this.rbGPOperator.Text = "By Grade By Operator";
            this.rbGPOperator.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(588, 415);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 19;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // frmGreigeProductionSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 450);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbOperatorAll);
            this.Controls.Add(this.rbMachineAll);
            this.Controls.Add(this.rbGradeAll);
            this.Controls.Add(this.rbGreigeProductAll);
            this.Controls.Add(this.cmbOperator);
            this.Controls.Add(this.cmbMachines);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.cmbGreigeProduct);
            this.Controls.Add(this.cmbKnittingCustomer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmGreigeProductionSel";
            this.Text = "Greige Production Report Selection";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbKnittingCustomer;
        private System.Windows.Forms.ComboBox cmbGreigeProduct;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.ComboBox cmbMachines;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.RadioButton rbGreigeProductAll;
        private System.Windows.Forms.RadioButton rbGradeAll;
        private System.Windows.Forms.RadioButton rbMachineAll;
        private System.Windows.Forms.RadioButton rbOperatorAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGPOperator;
        private System.Windows.Forms.RadioButton rbGPByGrade;
        private System.Windows.Forms.RadioButton rbGPByTotal;
        private System.Windows.Forms.RadioButton rbGPByMachine;
        private System.Windows.Forms.Button btnReport;
    }
}