namespace Cutting
{
    partial class frmQaReportSelection
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
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmboSizes = new Cutting.CheckComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboCutSheets = new Cutting.CheckComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboStyles = new Cutting.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbFullDetail = new System.Windows.Forms.RadioButton();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(136, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Selection";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(124, 66);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(126, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(124, 30);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(126, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboSizes);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmboCutSheets);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmboStyles);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(136, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 211);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Selection";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(124, 148);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(143, 21);
            this.cmboSizes.TabIndex = 5;
            this.cmboSizes.Text = "Select Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sizes";
            // 
            // cmboCutSheets
            // 
            this.cmboCutSheets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCutSheets.FormattingEnabled = true;
            this.cmboCutSheets.Location = new System.Drawing.Point(124, 93);
            this.cmboCutSheets.Name = "cmboCutSheets";
            this.cmboCutSheets.Size = new System.Drawing.Size(143, 21);
            this.cmboCutSheets.TabIndex = 3;
            this.cmboCutSheets.Text = "Select Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "CutSheets";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(124, 38);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(143, 21);
            this.cmboStyles.TabIndex = 1;
            this.cmboStyles.Text = "Select Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Styles";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(518, 482);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSummary);
            this.groupBox3.Controls.Add(this.rbFullDetail);
            this.groupBox3.Location = new System.Drawing.Point(136, 387);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Display Requirements";
            // 
            // rbFullDetail
            // 
            this.rbFullDetail.AutoSize = true;
            this.rbFullDetail.Checked = true;
            this.rbFullDetail.Location = new System.Drawing.Point(53, 33);
            this.rbFullDetail.Name = "rbFullDetail";
            this.rbFullDetail.Size = new System.Drawing.Size(71, 17);
            this.rbFullDetail.TabIndex = 0;
            this.rbFullDetail.TabStop = true;
            this.rbFullDetail.Text = "Full Detail";
            this.rbFullDetail.UseVisualStyleBackColor = true;
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Location = new System.Drawing.Point(55, 66);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(68, 17);
            this.rbSummary.TabIndex = 1;
            this.rbSummary.Text = "Summary";
            this.rbSummary.UseVisualStyleBackColor = true;
            // 
            // frmQaReportSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 535);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQaReportSelection";
            this.Text = "QA Report Selection";
            this.Load += new System.EventHandler(this.frmQaReportSelection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        //private System.Windows.Forms.ComboBox cmboCutSheets;
        //private System.Windows.Forms.ComboBox cmboStyles;
        private Cutting.CheckComboBox cmboCutSheets;
        private Cutting.CheckComboBox cmboStyles;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button btnSubmit;
        // private System.Windows.Forms.ComboBox cmboSizes;
        private Cutting.CheckComboBox cmboSizes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.RadioButton rbFullDetail;
    }
}