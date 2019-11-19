namespace CustomerServices
{
    partial class frmCustomerSales
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
            this.rbRankedByStyleSize = new System.Windows.Forms.RadioButton();
            this.rbRankedByStyleColor = new System.Windows.Forms.RadioButton();
            this.rbRankedByStyle = new System.Windows.Forms.RadioButton();
            this.rbSummarisedForCompany = new System.Windows.Forms.RadioButton();
            this.rbSummarisedByCustomer = new System.Windows.Forms.RadioButton();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.cmboCustomers = new CustomerServices.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRankedByStyleSize);
            this.groupBox1.Controls.Add(this.rbRankedByStyleColor);
            this.groupBox1.Controls.Add(this.rbRankedByStyle);
            this.groupBox1.Controls.Add(this.rbSummarisedForCompany);
            this.groupBox1.Controls.Add(this.rbSummarisedByCustomer);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(212, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbRankedByStyleSize
            // 
            this.rbRankedByStyleSize.AutoSize = true;
            this.rbRankedByStyleSize.Location = new System.Drawing.Point(100, 180);
            this.rbRankedByStyleSize.Name = "rbRankedByStyleSize";
            this.rbRankedByStyleSize.Size = new System.Drawing.Size(130, 17);
            this.rbRankedByStyleSize.TabIndex = 8;
            this.rbRankedByStyleSize.TabStop = true;
            this.rbRankedByStyleSize.Text = "Ranked By Style, Size";
            this.rbRankedByStyleSize.UseVisualStyleBackColor = true;
            // 
            // rbRankedByStyleColor
            // 
            this.rbRankedByStyleColor.AutoSize = true;
            this.rbRankedByStyleColor.Location = new System.Drawing.Point(173, 145);
            this.rbRankedByStyleColor.Name = "rbRankedByStyleColor";
            this.rbRankedByStyleColor.Size = new System.Drawing.Size(184, 17);
            this.rbRankedByStyleColor.TabIndex = 7;
            this.rbRankedByStyleColor.TabStop = true;
            this.rbRankedByStyleColor.Text = "Ranked By Style, Colour and Size";
            this.rbRankedByStyleColor.UseVisualStyleBackColor = true;
            // 
            // rbRankedByStyle
            // 
            this.rbRankedByStyle.AutoSize = true;
            this.rbRankedByStyle.Location = new System.Drawing.Point(23, 145);
            this.rbRankedByStyle.Name = "rbRankedByStyle";
            this.rbRankedByStyle.Size = new System.Drawing.Size(139, 17);
            this.rbRankedByStyle.TabIndex = 6;
            this.rbRankedByStyle.TabStop = true;
            this.rbRankedByStyle.Text = "Ranked by Style, Colour";
            this.rbRankedByStyle.UseVisualStyleBackColor = true;
            // 
            // rbSummarisedForCompany
            // 
            this.rbSummarisedForCompany.AutoSize = true;
            this.rbSummarisedForCompany.Location = new System.Drawing.Point(173, 102);
            this.rbSummarisedForCompany.Name = "rbSummarisedForCompany";
            this.rbSummarisedForCompany.Size = new System.Drawing.Size(147, 17);
            this.rbSummarisedForCompany.TabIndex = 5;
            this.rbSummarisedForCompany.TabStop = true;
            this.rbSummarisedForCompany.Text = "Summarised For Company";
            this.rbSummarisedForCompany.UseVisualStyleBackColor = true;
            // 
            // rbSummarisedByCustomer
            // 
            this.rbSummarisedByCustomer.AutoSize = true;
            this.rbSummarisedByCustomer.Location = new System.Drawing.Point(23, 102);
            this.rbSummarisedByCustomer.Name = "rbSummarisedByCustomer";
            this.rbSummarisedByCustomer.Size = new System.Drawing.Size(144, 17);
            this.rbSummarisedByCustomer.TabIndex = 4;
            this.rbSummarisedByCustomer.TabStop = true;
            this.rbSummarisedByCustomer.Text = "Summarised By Customer";
            this.rbSummarisedByCustomer.UseVisualStyleBackColor = true;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(121, 62);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(134, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(121, 25);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(134, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(667, 536);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Style";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Colour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Size";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmboSizes);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmboColours);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmboStyles);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmboCustomers);
            this.groupBox2.Location = new System.Drawing.Point(114, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 282);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection Criteria";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(223, 217);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(251, 21);
            this.cmboSizes.TabIndex = 13;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(223, 154);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(251, 21);
            this.cmboColours.TabIndex = 12;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(223, 91);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(251, 21);
            this.cmboStyles.TabIndex = 11;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(223, 28);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(251, 21);
            this.cmboCustomers.TabIndex = 10;
            this.cmboCustomers.Text = "Select Options";
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // frmCustomerSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 582);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCustomerSales";
            this.Text = "Report On Customer Sales";
            this.Load += new System.EventHandler(this.frmCustomerSales_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
      //  private System.Windows.Forms.ComboBox cmboCustomers;
      //  private System.Windows.Forms.ComboBox cmboStyles;
      //  private System.Windows.Forms.ComboBox cmboColours;
      //  private System.Windows.Forms.ComboBox cmboSizes;
        private CustomerServices.CheckComboBox cmboCustomers;
        private CustomerServices.CheckComboBox cmboStyles;
        private CustomerServices.CheckComboBox cmboColours;
        private CustomerServices.CheckComboBox cmboSizes;
        private System.Windows.Forms.RadioButton rbSummarisedByCustomer;
        private System.Windows.Forms.RadioButton rbSummarisedForCompany;
        private System.Windows.Forms.RadioButton rbRankedByStyle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbRankedByStyleColor;
        private System.Windows.Forms.RadioButton rbRankedByStyleSize;
    }
}