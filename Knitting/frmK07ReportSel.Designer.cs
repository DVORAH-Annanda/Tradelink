namespace Knitting
{
    partial class frmK07ReportSel
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
            this.cmbKnittingCustomer = new Knitting.CheckComboBox();
            this.cmbGreigeProduct = new Knitting.CheckComboBox();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.cmbMachines = new Knitting.CheckComboBox();
            this.cmbOperator = new Knitting.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGPOperator = new System.Windows.Forms.RadioButton();
            this.rbGPByTotal = new System.Windows.Forms.RadioButton();
            this.rbGPByMachine = new System.Windows.Forms.RadioButton();
            this.btnReport = new System.Windows.Forms.Button();
            this.chkQASummary = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbYarnTypes = new Knitting.CheckComboBox();
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
            this.label3.Location = new System.Drawing.Point(140, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Knitting Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Greige Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Grade";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Machine";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Operator";
            // 
            // cmbKnittingCustomer
            // 
            this.cmbKnittingCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKnittingCustomer.FormattingEnabled = true;
            this.cmbKnittingCustomer.Location = new System.Drawing.Point(273, 183);
            this.cmbKnittingCustomer.Name = "cmbKnittingCustomer";
            this.cmbKnittingCustomer.Size = new System.Drawing.Size(196, 21);
            this.cmbKnittingCustomer.TabIndex = 9;
            this.cmbKnittingCustomer.Text = "Select Options";
            this.cmbKnittingCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbKnittingCustomer_SelectedIndexChanged);
            // 
            // cmbGreigeProduct
            // 
            this.cmbGreigeProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGreigeProduct.FormattingEnabled = true;
            this.cmbGreigeProduct.Location = new System.Drawing.Point(273, 219);
            this.cmbGreigeProduct.Name = "cmbGreigeProduct";
            this.cmbGreigeProduct.Size = new System.Drawing.Size(196, 21);
            this.cmbGreigeProduct.TabIndex = 10;
            this.cmbGreigeProduct.Text = "Select Options";
            this.cmbGreigeProduct.SelectedIndexChanged += new System.EventHandler(this.cmbGreigeProduct_SelectedIndexChanged);
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(273, 255);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(100, 20);
            this.txtGrade.TabIndex = 11;
            this.txtGrade.TextChanged += new System.EventHandler(this.txtGrade_TextChanged);
            // 
            // cmbMachines
            // 
            this.cmbMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(273, 290);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(196, 21);
            this.cmbMachines.TabIndex = 12;
            this.cmbMachines.Text = "Select Options";
            this.cmbMachines.SelectedIndexChanged += new System.EventHandler(this.cmbMachines_SelectedIndexChanged);
            // 
            // cmbOperator
            // 
            this.cmbOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(273, 326);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(196, 21);
            this.cmbOperator.TabIndex = 13;
            this.cmbOperator.Text = "Select Options";
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.cmbOperator_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGPOperator);
            this.groupBox1.Controls.Add(this.rbGPByTotal);
            this.groupBox1.Controls.Add(this.rbGPByMachine);
            this.groupBox1.Location = new System.Drawing.Point(116, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 103);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Sort Options";
            // 
            // rbGPOperator
            // 
            this.rbGPOperator.AutoSize = true;
            this.rbGPOperator.Location = new System.Drawing.Point(24, 62);
            this.rbGPOperator.Name = "rbGPOperator";
            this.rbGPOperator.Size = new System.Drawing.Size(113, 17);
            this.rbGPOperator.TabIndex = 3;
            this.rbGPOperator.TabStop = true;
            this.rbGPOperator.Text = "Totals By Operator";
            this.rbGPOperator.UseVisualStyleBackColor = true;
            // 
            // rbGPByTotal
            // 
            this.rbGPByTotal.AutoSize = true;
            this.rbGPByTotal.Location = new System.Drawing.Point(228, 19);
            this.rbGPByTotal.Name = "rbGPByTotal";
            this.rbGPByTotal.Size = new System.Drawing.Size(189, 17);
            this.rbGPByTotal.TabIndex = 1;
            this.rbGPByTotal.TabStop = true;
            this.rbGPByTotal.Text = "Totals By Machines and By Quality";
            this.rbGPByTotal.UseVisualStyleBackColor = true;
            // 
            // rbGPByMachine
            // 
            this.rbGPByMachine.AutoSize = true;
            this.rbGPByMachine.Location = new System.Drawing.Point(24, 19);
            this.rbGPByMachine.Name = "rbGPByMachine";
            this.rbGPByMachine.Size = new System.Drawing.Size(163, 17);
            this.rbGPByMachine.TabIndex = 0;
            this.rbGPByMachine.TabStop = true;
            this.rbGPByMachine.Text = "Totals By Quality By Machine";
            this.rbGPByMachine.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(628, 468);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 19;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // chkQASummary
            // 
            this.chkQASummary.AutoSize = true;
            this.chkQASummary.Location = new System.Drawing.Point(273, 118);
            this.chkQASummary.Name = "chkQASummary";
            this.chkQASummary.Size = new System.Drawing.Size(87, 17);
            this.chkQASummary.TabIndex = 20;
            this.chkQASummary.Text = "QA Summary";
            this.chkQASummary.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(140, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Yarn Type ";
            // 
            // cmbYarnTypes
            // 
            this.cmbYarnTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbYarnTypes.FormattingEnabled = true;
            this.cmbYarnTypes.Location = new System.Drawing.Point(273, 147);
            this.cmbYarnTypes.Name = "cmbYarnTypes";
            this.cmbYarnTypes.Size = new System.Drawing.Size(196, 21);
            this.cmbYarnTypes.TabIndex = 22;
            this.cmbYarnTypes.Text = "Select Options";
            // 
            // frmK07ReportSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 523);
            this.Controls.Add(this.cmbYarnTypes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkQASummary);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupBox1);
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
            this.Name = "frmK07ReportSel";
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
        //private System.Windows.Forms.ComboBox cmbKnittingCustomer;
        private Knitting.CheckComboBox cmbKnittingCustomer;
        //private System.Windows.Forms.ComboBox cmbGreigeProduct;
        private Knitting.CheckComboBox cmbGreigeProduct;
        private System.Windows.Forms.TextBox txtGrade;
        //private System.Windows.Forms.ComboBox cmbMachines;
        private Knitting.CheckComboBox cmbMachines;
        //private System.Windows.Forms.ComboBox cmbOperator;
        private Knitting.CheckComboBox cmbOperator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGPOperator;
        private System.Windows.Forms.RadioButton rbGPByMachine;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.RadioButton rbGPByTotal;
        private System.Windows.Forms.CheckBox chkQASummary;
        private System.Windows.Forms.Label label8;
        private Knitting.CheckComboBox cmbYarnTypes;
    }
}