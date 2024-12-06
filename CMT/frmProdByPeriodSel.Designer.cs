namespace CMT
{
    partial class frmProdByPeriodSel
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
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNoOfBoxes = new System.Windows.Forms.RadioButton();
            this.rbNoOfGarments = new System.Windows.Forms.RadioButton();
            this.cmboColour = new CMT.CheckComboBox();
            this.cmboQuality = new CMT.CheckComboBox();
            this.cmboSize = new CMT.CheckComboBox();
            this.cmboFactory = new CMT.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(147, 32);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(183, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(147, 70);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(183, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Factory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 380);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Report Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(147, 378);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(183, 21);
            this.cmboReportOptions.TabIndex = 9;
            this.cmboReportOptions.SelectedIndexChanged += new System.EventHandler(this.cmboReportOptions_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(107, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Style";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(255, 426);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Colour";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNoOfBoxes);
            this.groupBox1.Controls.Add(this.rbNoOfGarments);
            this.groupBox1.Location = new System.Drawing.Point(59, 270);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 81);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Garment Detail";
            // 
            // rbNoOfBoxes
            // 
            this.rbNoOfBoxes.AutoSize = true;
            this.rbNoOfBoxes.Location = new System.Drawing.Point(156, 37);
            this.rbNoOfBoxes.Name = "rbNoOfBoxes";
            this.rbNoOfBoxes.Size = new System.Drawing.Size(85, 17);
            this.rbNoOfBoxes.TabIndex = 1;
            this.rbNoOfBoxes.Text = "No Of Boxes";
            this.rbNoOfBoxes.UseVisualStyleBackColor = true;
            // 
            // rbNoOfGarments
            // 
            this.rbNoOfGarments.AutoSize = true;
            this.rbNoOfGarments.Checked = true;
            this.rbNoOfGarments.Location = new System.Drawing.Point(25, 37);
            this.rbNoOfGarments.Name = "rbNoOfGarments";
            this.rbNoOfGarments.Size = new System.Drawing.Size(99, 17);
            this.rbNoOfGarments.TabIndex = 0;
            this.rbNoOfGarments.TabStop = true;
            this.rbNoOfGarments.Text = "No of Garments";
            this.rbNoOfGarments.UseVisualStyleBackColor = true;
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(147, 186);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(183, 21);
            this.cmboColour.TabIndex = 14;
            this.cmboColour.Text = "Select Options";
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // cmboQuality
            // 
            this.cmboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(147, 147);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(183, 21);
            this.cmboQuality.TabIndex = 11;
            this.cmboQuality.Text = "Select Options";
            this.cmboQuality.SelectedIndexChanged += new System.EventHandler(this.cmboQuality_SelectedIndexChanged);
            // 
            // cmboSize
            // 
            this.cmboSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSize.FormattingEnabled = true;
            this.cmboSize.Location = new System.Drawing.Point(147, 225);
            this.cmboSize.Name = "cmboSize";
            this.cmboSize.Size = new System.Drawing.Size(183, 21);
            this.cmboSize.TabIndex = 7;
            this.cmboSize.Text = "Select Options";
            this.cmboSize.SelectedIndexChanged += new System.EventHandler(this.cmboSize_SelectedIndexChanged);
            // 
            // cmboFactory
            // 
            this.cmboFactory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFactory.FormattingEnabled = true;
            this.cmboFactory.Location = new System.Drawing.Point(147, 108);
            this.cmboFactory.Name = "cmboFactory";
            this.cmboFactory.Size = new System.Drawing.Size(183, 21);
            this.cmboFactory.TabIndex = 5;
            this.cmboFactory.Text = "Select Options";
            this.cmboFactory.SelectedIndexChanged += new System.EventHandler(this.cmboFactory_SelectedIndexChanged);
            // 
            // frmProdByPeriodSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 475);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboQuality);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboFactory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmProdByPeriodSel";
            this.Text = "CMT Production by period selection";
            this.Load += new System.EventHandler(this.ProdByPeriodSel_Load);
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
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label7;
        // private System.Windows.Forms.ComboBox cmboColour;
        private CMT.CheckComboBox cmboColour;
        // private System.Windows.Forms.ComboBox cmboLine;
        private CMT.CheckComboBox cmboSize;
        // private System.Windows.Forms.ComboBox cmboQuality;
        private CMT.CheckComboBox cmboQuality;
        // private System.Windows.Forms.ComboBox cmboFactory;
        private CMT.CheckComboBox cmboFactory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNoOfBoxes;
        private System.Windows.Forms.RadioButton rbNoOfGarments;
    }
}