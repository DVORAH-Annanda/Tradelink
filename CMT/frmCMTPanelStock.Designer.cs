namespace CMT
{
    partial class frmCMTPanelStock
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboSizes = new CMT.CheckComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboColours = new CMT.CheckComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboStyles = new CMT.CheckComboBox();
            this.cmboDepartment = new CMT.CheckComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.chkExcludeOnHold = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "CMT";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(592, 377);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboSizes);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboColours);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboStyles);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmboDepartment);
            this.groupBox1.Location = new System.Drawing.Point(140, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 238);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // comboSizes
            // 
            this.comboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSizes.FormattingEnabled = true;
            this.comboSizes.Location = new System.Drawing.Point(150, 186);
            this.comboSizes.Name = "comboSizes";
            this.comboSizes.Size = new System.Drawing.Size(168, 21);
            this.comboSizes.TabIndex = 11;
            this.comboSizes.Text = "Select Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Sizes";
            // 
            // comboColours
            // 
            this.comboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColours.FormattingEnabled = true;
            this.comboColours.Location = new System.Drawing.Point(150, 135);
            this.comboColours.Name = "comboColours";
            this.comboColours.Size = new System.Drawing.Size(168, 21);
            this.comboColours.TabIndex = 9;
            this.comboColours.Text = "Select Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Colours";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Styles";
            // 
            // comboStyles
            // 
            this.comboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStyles.FormattingEnabled = true;
            this.comboStyles.Location = new System.Drawing.Point(150, 84);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.Size = new System.Drawing.Size(168, 21);
            this.comboStyles.TabIndex = 6;
            this.comboStyles.Text = "Select Options";
            // 
            // cmboDepartment
            // 
            this.cmboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDepartment.FormattingEnabled = true;
            this.cmboDepartment.Location = new System.Drawing.Point(150, 33);
            this.cmboDepartment.Name = "cmboDepartment";
            this.cmboDepartment.Size = new System.Drawing.Size(168, 21);
            this.cmboDepartment.TabIndex = 5;
            this.cmboDepartment.Text = "Select Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboReportOptions);
            this.groupBox2.Location = new System.Drawing.Point(181, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(109, 39);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(168, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // chkExcludeOnHold
            // 
            this.chkExcludeOnHold.AutoSize = true;
            this.chkExcludeOnHold.Location = new System.Drawing.Point(290, 279);
            this.chkExcludeOnHold.Name = "chkExcludeOnHold";
            this.chkExcludeOnHold.Size = new System.Drawing.Size(140, 17);
            this.chkExcludeOnHold.TabIndex = 13;
            this.chkExcludeOnHold.Text = "Exclude On Hold Items?";
            this.chkExcludeOnHold.UseVisualStyleBackColor = true;
            // 
            // frmCMTPanelStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 459);
            this.Controls.Add(this.chkExcludeOnHold);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmCMTPanelStock";
            this.Text = "Panel Stock Selection at CMT";
            this.Load += new System.EventHandler(this.frmCMTPanelStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
       //  private System.Windows.Forms.ComboBox cmboDepartment;
        private CMT.CheckComboBox cmboDepartment;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        //private System.Windows.Forms.ComboBox comboSizes;
        private CMT.CheckComboBox comboSizes;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.ComboBox comboColours;
        private CMT.CheckComboBox comboColours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        // private System.Windows.Forms.ComboBox comboStyles;
        private CMT.CheckComboBox comboStyles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.CheckBox chkExcludeOnHold;
    }
}