namespace Knitting
{
    partial class frmK08ReportSel
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbKnittingCustomer = new Knitting.CheckComboBox();
            this.cmbGreigeProduct = new Knitting.CheckComboBox();
            this.cmbKnittingMachine = new Knitting.CheckComboBox();
            this.cmbOperator = new Knitting.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbQA6 = new System.Windows.Forms.RadioButton();
            this.rbQA5 = new System.Windows.Forms.RadioButton();
            this.rbQA4 = new System.Windows.Forms.RadioButton();
            this.rbQA3 = new System.Windows.Forms.RadioButton();
            this.rbQA2 = new System.Windows.Forms.RadioButton();
            this.rbQA1 = new System.Windows.Forms.RadioButton();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbExcludeCommission = new System.Windows.Forms.RadioButton();
            this.chkQASummary = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Period";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Knitting Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Greige Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Knitting Machine";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Operator";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(267, 28);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(135, 20);
            this.dtpFromDate.TabIndex = 6;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(267, 65);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(135, 20);
            this.dtpToDate.TabIndex = 7;
            // 
            // cmbKnittingCustomer
            // 
            this.cmbKnittingCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKnittingCustomer.FormattingEnabled = true;
            this.cmbKnittingCustomer.Location = new System.Drawing.Point(268, 127);
            this.cmbKnittingCustomer.Name = "cmbKnittingCustomer";
            this.cmbKnittingCustomer.Size = new System.Drawing.Size(172, 21);
            this.cmbKnittingCustomer.TabIndex = 8;
            this.cmbKnittingCustomer.Text = "Select Options";
            this.cmbKnittingCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbKnittingCustomer_SelectedIndexChanged);
            // 
            // cmbGreigeProduct
            // 
            this.cmbGreigeProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGreigeProduct.FormattingEnabled = true;
            this.cmbGreigeProduct.Location = new System.Drawing.Point(268, 165);
            this.cmbGreigeProduct.Name = "cmbGreigeProduct";
            this.cmbGreigeProduct.Size = new System.Drawing.Size(172, 21);
            this.cmbGreigeProduct.TabIndex = 9;
            this.cmbGreigeProduct.Text = "Select Options";
            this.cmbGreigeProduct.SelectedIndexChanged += new System.EventHandler(this.cmbGreigeProduct_SelectedIndexChanged);
            // 
            // cmbKnittingMachine
            // 
            this.cmbKnittingMachine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKnittingMachine.FormattingEnabled = true;
            this.cmbKnittingMachine.Location = new System.Drawing.Point(268, 199);
            this.cmbKnittingMachine.Name = "cmbKnittingMachine";
            this.cmbKnittingMachine.Size = new System.Drawing.Size(172, 21);
            this.cmbKnittingMachine.TabIndex = 10;
            this.cmbKnittingMachine.Text = "Select Options";
            this.cmbKnittingMachine.SelectedIndexChanged += new System.EventHandler(this.cmbKnittingMachine_SelectedIndexChanged);
            // 
            // cmbOperator
            // 
            this.cmbOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(268, 241);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(172, 21);
            this.cmbOperator.TabIndex = 11;
            this.cmbOperator.Text = "Select Options";
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.cmbOperator_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbQA6);
            this.groupBox1.Controls.Add(this.rbQA5);
            this.groupBox1.Controls.Add(this.rbQA4);
            this.groupBox1.Controls.Add(this.rbQA3);
            this.groupBox1.Controls.Add(this.rbQA2);
            this.groupBox1.Controls.Add(this.rbQA1);
            this.groupBox1.Location = new System.Drawing.Point(74, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Sort Options";
            // 
            // rbQA6
            // 
            this.rbQA6.AutoSize = true;
            this.rbQA6.Enabled = false;
            this.rbQA6.Location = new System.Drawing.Point(286, 66);
            this.rbQA6.Name = "rbQA6";
            this.rbQA6.Size = new System.Drawing.Size(313, 17);
            this.rbQA6.TabIndex = 5;
            this.rbQA6.TabStop = true;
            this.rbQA6.Text = "QA Details by Yarn Origin - Spinning Machine and Yarn Order";
            this.rbQA6.UseVisualStyleBackColor = true;
            // 
            // rbQA5
            // 
            this.rbQA5.AutoSize = true;
            this.rbQA5.Enabled = false;
            this.rbQA5.Location = new System.Drawing.Point(286, 43);
            this.rbQA5.Name = "rbQA5";
            this.rbQA5.Size = new System.Drawing.Size(196, 17);
            this.rbQA5.TabIndex = 4;
            this.rbQA5.TabStop = true;
            this.rbQA5.Text = "QA details by Yarn Origin Yarn Order";
            this.rbQA5.UseVisualStyleBackColor = true;
            // 
            // rbQA4
            // 
            this.rbQA4.AutoSize = true;
            this.rbQA4.Location = new System.Drawing.Point(286, 19);
            this.rbQA4.Name = "rbQA4";
            this.rbQA4.Size = new System.Drawing.Size(161, 17);
            this.rbQA4.TabIndex = 3;
            this.rbQA4.TabStop = true;
            this.rbQA4.Text = "QA Details for each Operator";
            this.rbQA4.UseVisualStyleBackColor = true;
            // 
            // rbQA3
            // 
            this.rbQA3.AutoSize = true;
            this.rbQA3.Location = new System.Drawing.Point(9, 66);
            this.rbQA3.Name = "rbQA3";
            this.rbQA3.Size = new System.Drawing.Size(160, 17);
            this.rbQA3.TabIndex = 2;
            this.rbQA3.TabStop = true;
            this.rbQA3.Text = "QA Details for each machine";
            this.rbQA3.UseVisualStyleBackColor = true;
            // 
            // rbQA2
            // 
            this.rbQA2.AutoSize = true;
            this.rbQA2.Location = new System.Drawing.Point(9, 43);
            this.rbQA2.Name = "rbQA2";
            this.rbQA2.Size = new System.Drawing.Size(258, 17);
            this.rbQA2.TabIndex = 1;
            this.rbQA2.TabStop = true;
            this.rbQA2.Text = "QA Details for each Greige Product (No Machine)";
            this.rbQA2.UseVisualStyleBackColor = true;
            // 
            // rbQA1
            // 
            this.rbQA1.AutoSize = true;
            this.rbQA1.Location = new System.Drawing.Point(9, 20);
            this.rbQA1.Name = "rbQA1";
            this.rbQA1.Size = new System.Drawing.Size(253, 17);
            this.rbQA1.TabIndex = 0;
            this.rbQA1.TabStop = true;
            this.rbQA1.Text = "QA Details for each Product by Knitting Machine";
            this.rbQA1.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(612, 471);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 13;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbExcludeCommission);
            this.groupBox2.Location = new System.Drawing.Point(218, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 51);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Filter Options";
            // 
            // rbExcludeCommission
            // 
            this.rbExcludeCommission.AutoSize = true;
            this.rbExcludeCommission.Location = new System.Drawing.Point(18, 21);
            this.rbExcludeCommission.Name = "rbExcludeCommission";
            this.rbExcludeCommission.Size = new System.Drawing.Size(120, 17);
            this.rbExcludeCommission.TabIndex = 0;
            this.rbExcludeCommission.TabStop = true;
            this.rbExcludeCommission.Text = "Exlude Commissions";
            this.rbExcludeCommission.UseVisualStyleBackColor = true;
            // 
            // chkQASummary
            // 
            this.chkQASummary.AutoSize = true;
            this.chkQASummary.Location = new System.Drawing.Point(268, 97);
            this.chkQASummary.Name = "chkQASummary";
            this.chkQASummary.Size = new System.Drawing.Size(87, 17);
            this.chkQASummary.TabIndex = 15;
            this.chkQASummary.Text = "QA Summary";
            this.chkQASummary.UseVisualStyleBackColor = true;
            // 
            // frmK08ReportSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 506);
            this.Controls.Add(this.chkQASummary);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbOperator);
            this.Controls.Add(this.cmbKnittingMachine);
            this.Controls.Add(this.cmbGreigeProduct);
            this.Controls.Add(this.cmbKnittingCustomer);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmK08ReportSel";
            this.Text = "QA Reports for Greige";
            this.LocationChanged += new System.EventHandler(this.FormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
       /* private System.Windows.Forms.ComboBox cmbKnittingCustomer;
        private System.Windows.Forms.ComboBox cmbGreigeProduct;
        private System.Windows.Forms.ComboBox cmbKnittingMachine;
        private System.Windows.Forms.ComboBox cmbOperator;*/
        private Knitting.CheckComboBox cmbKnittingCustomer;
        private Knitting.CheckComboBox cmbGreigeProduct;
        private Knitting.CheckComboBox cmbKnittingMachine;
        private Knitting.CheckComboBox cmbOperator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbQA6;
        private System.Windows.Forms.RadioButton rbQA5;
        private System.Windows.Forms.RadioButton rbQA4;
        private System.Windows.Forms.RadioButton rbQA3;
        private System.Windows.Forms.RadioButton rbQA2;
        private System.Windows.Forms.RadioButton rbQA1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbExcludeCommission;
        private System.Windows.Forms.CheckBox chkQASummary;
    }
}