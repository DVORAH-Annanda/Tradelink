namespace CMT
{
    partial class frmCMTFinishedWAnalysis
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkQaSummary = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkException = new System.Windows.Forms.CheckBox();
            this.cmboSize = new CMT.CheckComboBox();
            this.cmboColour = new CMT.CheckComboBox();
            this.cmboStyle = new CMT.CheckComboBox();
            this.cmboFactory = new CMT.CheckComboBox();
            this.chkValueBySize = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Factory ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To Date";
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(522, 534);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(75, 23);
            this.Submit.TabIndex = 4;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Style";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Colour";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(248, 41);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(141, 20);
            this.dtpFromDate.TabIndex = 8;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(248, 89);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(141, 20);
            this.dtpToDate.TabIndex = 9;
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(248, 461);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(278, 21);
            this.cmboReportOptions.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Report Options";
            // 
            // chkQaSummary
            // 
            this.chkQaSummary.AutoSize = true;
            this.chkQaSummary.Location = new System.Drawing.Point(223, 129);
            this.chkQaSummary.Name = "chkQaSummary";
            this.chkQaSummary.Size = new System.Drawing.Size(87, 17);
            this.chkQaSummary.TabIndex = 17;
            this.chkQaSummary.Text = "QA Summary";
            this.chkQaSummary.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtPercentage);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.chkException);
            this.groupBox4.Location = new System.Drawing.Point(170, 152);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 77);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "% and Greater";
            // 
            // txtPercentage
            // 
            this.txtPercentage.Location = new System.Drawing.Point(110, 47);
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(42, 20);
            this.txtPercentage.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Exceeding";
            // 
            // chkException
            // 
            this.chkException.AutoSize = true;
            this.chkException.Location = new System.Drawing.Point(41, 19);
            this.chkException.Name = "chkException";
            this.chkException.Size = new System.Drawing.Size(132, 17);
            this.chkException.TabIndex = 0;
            this.chkException.Text = "Exception Report Only";
            this.chkException.UseVisualStyleBackColor = true;
            this.chkException.CheckedChanged += new System.EventHandler(this.chkException_CheckedChanged);
            // 
            // cmboSize
            // 
            this.cmboSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSize.FormattingEnabled = true;
            this.cmboSize.Location = new System.Drawing.Point(248, 397);
            this.cmboSize.Name = "cmboSize";
            this.cmboSize.Size = new System.Drawing.Size(278, 21);
            this.cmboSize.TabIndex = 14;
            this.cmboSize.Text = "Select Options";
            this.cmboSize.SelectedIndexChanged += new System.EventHandler(this.cmboSize_SelectedIndexChanged);
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(248, 348);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(278, 21);
            this.cmboColour.TabIndex = 13;
            this.cmboColour.Text = "Select Options";
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // cmboStyle
            // 
            this.cmboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(248, 299);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(278, 21);
            this.cmboStyle.TabIndex = 12;
            this.cmboStyle.Text = "Select Options";
            this.cmboStyle.SelectedIndexChanged += new System.EventHandler(this.cmboStyle_SelectedIndexChanged);
            // 
            // cmboFactory
            // 
            this.cmboFactory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFactory.FormattingEnabled = true;
            this.cmboFactory.Location = new System.Drawing.Point(248, 250);
            this.cmboFactory.Name = "cmboFactory";
            this.cmboFactory.Size = new System.Drawing.Size(278, 21);
            this.cmboFactory.TabIndex = 10;
            this.cmboFactory.Text = "Select Options";
            this.cmboFactory.SelectedIndexChanged += new System.EventHandler(this.cmboFactory_SelectedIndexChanged);
            // 
            // chkValueBySize
            // 
            this.chkValueBySize.AutoSize = true;
            this.chkValueBySize.Location = new System.Drawing.Point(352, 129);
            this.chkValueBySize.Name = "chkValueBySize";
            this.chkValueBySize.Size = new System.Drawing.Size(128, 17);
            this.chkValueBySize.TabIndex = 19;
            this.chkValueBySize.Text = "Display Value By Size";
            this.chkValueBySize.UseVisualStyleBackColor = true;
            // 
            // frmCMTFinishedWAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 583);
            this.Controls.Add(this.chkValueBySize);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chkQaSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.cmboSize);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.cmboFactory);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmCMTFinishedWAnalysis";
            this.Text = "CMT Completed Work Production Analysis";
            this.Load += new System.EventHandler(this.frmCMTFinishedWAnalysis_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        /*private System.Windows.Forms.ComboBox cmboFactory;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.ComboBox cmboStyle;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboSize;*
         */
        private CMT.CheckComboBox cmboFactory;
        private CMT.CheckComboBox cmboStyle;
        private CMT.CheckComboBox cmboColour;
        private CMT.CheckComboBox cmboSize;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkQaSummary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPercentage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkException;
        private System.Windows.Forms.CheckBox chkValueBySize;
    }
}