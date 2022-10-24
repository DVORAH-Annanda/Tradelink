namespace ProductionPlanning
{
    partial class frmInterDeptFaults
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCutSheet = new System.Windows.Forms.RadioButton();
            this.rbDyeButton = new System.Windows.Forms.RadioButton();
            this.cmboInterDepartmentalOption = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.rbAllDyeBatches = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmboInterDepartmentalOption);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCutSheet);
            this.groupBox1.Location = new System.Drawing.Point(128, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 358);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAllDyeBatches);
            this.groupBox2.Controls.Add(this.rbCutSheet);
            this.groupBox2.Controls.Add(this.rbDyeButton);
            this.groupBox2.Location = new System.Drawing.Point(73, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 143);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Option";
            // 
            // rbCutSheet
            // 
            this.rbCutSheet.AutoSize = true;
            this.rbCutSheet.Checked = true;
            this.rbCutSheet.Location = new System.Drawing.Point(11, 28);
            this.rbCutSheet.Name = "rbCutSheet";
            this.rbCutSheet.Size = new System.Drawing.Size(72, 17);
            this.rbCutSheet.TabIndex = 0;
            this.rbCutSheet.TabStop = true;
            this.rbCutSheet.Text = "Cut Sheet";
            this.rbCutSheet.UseVisualStyleBackColor = true;
            // 
            // rbDyeButton
            // 
            this.rbDyeButton.AutoSize = true;
            this.rbDyeButton.Location = new System.Drawing.Point(11, 74);
            this.rbDyeButton.Name = "rbDyeButton";
            this.rbDyeButton.Size = new System.Drawing.Size(75, 17);
            this.rbDyeButton.TabIndex = 10;
            this.rbDyeButton.Text = "Dye Batch";
            this.rbDyeButton.UseVisualStyleBackColor = true;
            // 
            // cmboInterDepartmentalOption
            // 
            this.cmboInterDepartmentalOption.FormattingEnabled = true;
            this.cmboInterDepartmentalOption.Location = new System.Drawing.Point(294, 90);
            this.cmboInterDepartmentalOption.Name = "cmboInterDepartmentalOption";
            this.cmboInterDepartmentalOption.Size = new System.Drawing.Size(202, 21);
            this.cmboInterDepartmentalOption.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Interdepartmental Comparioson Option";
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(294, 155);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.Size = new System.Drawing.Size(202, 20);
            this.txtCutSheet.TabIndex = 7;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(701, 415);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "From Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "To Date\r\n";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(100, 50);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 20);
            this.dtpFromDate.TabIndex = 13;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(341, 50);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(138, 20);
            this.dtpToDate.TabIndex = 14;
            // 
            // rbAllDyeBatches
            // 
            this.rbAllDyeBatches.AutoSize = true;
            this.rbAllDyeBatches.Location = new System.Drawing.Point(11, 120);
            this.rbAllDyeBatches.Name = "rbAllDyeBatches";
            this.rbAllDyeBatches.Size = new System.Drawing.Size(147, 17);
            this.rbAllDyeBatches.TabIndex = 11;
            this.rbAllDyeBatches.TabStop = true;
            this.rbAllDyeBatches.Text = "All Dye Batches for period";
            this.rbAllDyeBatches.UseVisualStyleBackColor = true;
            // 
            // frmInterDeptFaults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInterDeptFaults";
            this.Text = "Inter Departmental Faults Analysis ";
            this.Load += new System.EventHandler(this.frmInterDeptFaults_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.RadioButton rbCutSheet;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmboInterDepartmentalOption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDyeButton;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbAllDyeBatches;
    }
}