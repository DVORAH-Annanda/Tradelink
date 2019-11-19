namespace DyeHouse
{
    partial class frmReportOpts
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rrbWIPDyeing = new System.Windows.Forms.RadioButton();
            this.rbDyeBatchPending = new System.Windows.Forms.RadioButton();
            this.cmboCustomer = new System.Windows.Forms.ComboBox();
            this.rbAllCustomers = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbByQuality = new System.Windows.Forms.RadioButton();
            this.rbBatchNumber = new System.Windows.Forms.RadioButton();
            this.rbSortByDate = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(168, 19);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(147, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(168, 61);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(147, 20);
            this.dtpToDate.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.cmboCustomer);
            this.groupBox1.Controls.Add(this.rbAllCustomers);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Location = new System.Drawing.Point(59, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 282);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rrbWIPDyeing);
            this.groupBox3.Controls.Add(this.rbDyeBatchPending);
            this.groupBox3.Location = new System.Drawing.Point(36, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 89);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transaction Selection";
            // 
            // rrbWIPDyeing
            // 
            this.rrbWIPDyeing.AutoSize = true;
            this.rrbWIPDyeing.Location = new System.Drawing.Point(175, 33);
            this.rrbWIPDyeing.Name = "rrbWIPDyeing";
            this.rrbWIPDyeing.Size = new System.Drawing.Size(82, 17);
            this.rrbWIPDyeing.TabIndex = 1;
            this.rrbWIPDyeing.TabStop = true;
            this.rrbWIPDyeing.Text = "WIP Dyeing";
            this.rrbWIPDyeing.UseVisualStyleBackColor = true;
            // 
            // rbDyeBatchPending
            // 
            this.rbDyeBatchPending.AutoSize = true;
            this.rbDyeBatchPending.Location = new System.Drawing.Point(15, 33);
            this.rbDyeBatchPending.Name = "rbDyeBatchPending";
            this.rbDyeBatchPending.Size = new System.Drawing.Size(117, 17);
            this.rbDyeBatchPending.TabIndex = 0;
            this.rbDyeBatchPending.TabStop = true;
            this.rbDyeBatchPending.Text = "Dye Batch Pending";
            this.rbDyeBatchPending.UseVisualStyleBackColor = true;
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(168, 101);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(193, 21);
            this.cmboCustomer.TabIndex = 8;
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // rbAllCustomers
            // 
            this.rbAllCustomers.AutoSize = true;
            this.rbAllCustomers.Location = new System.Drawing.Point(169, 137);
            this.rbAllCustomers.Name = "rbAllCustomers";
            this.rbAllCustomers.Size = new System.Drawing.Size(119, 17);
            this.rbAllCustomers.TabIndex = 7;
            this.rbAllCustomers.TabStop = true;
            this.rbAllCustomers.Text = "Select all customers";
            this.rbAllCustomers.UseVisualStyleBackColor = true;
            this.rbAllCustomers.CheckedChanged += new System.EventHandler(this.rbAllCustomers_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Customer ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbByQuality);
            this.groupBox2.Controls.Add(this.rbBatchNumber);
            this.groupBox2.Controls.Add(this.rbSortByDate);
            this.groupBox2.Location = new System.Drawing.Point(59, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 151);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort Options";
            // 
            // rbByQuality
            // 
            this.rbByQuality.AutoSize = true;
            this.rbByQuality.Location = new System.Drawing.Point(127, 107);
            this.rbByQuality.Name = "rbByQuality";
            this.rbByQuality.Size = new System.Drawing.Size(72, 17);
            this.rbByQuality.TabIndex = 3;
            this.rbByQuality.TabStop = true;
            this.rbByQuality.Text = "By Quality";
            this.rbByQuality.UseVisualStyleBackColor = true;
            // 
            // rbBatchNumber
            // 
            this.rbBatchNumber.AutoSize = true;
            this.rbBatchNumber.Location = new System.Drawing.Point(127, 74);
            this.rbBatchNumber.Name = "rbBatchNumber";
            this.rbBatchNumber.Size = new System.Drawing.Size(108, 17);
            this.rbBatchNumber.TabIndex = 2;
            this.rbBatchNumber.TabStop = true;
            this.rbBatchNumber.Text = "By Batch Number\r\n";
            this.rbBatchNumber.UseVisualStyleBackColor = true;
            // 
            // rbSortByDate
            // 
            this.rbSortByDate.AutoSize = true;
            this.rbSortByDate.Location = new System.Drawing.Point(127, 41);
            this.rbSortByDate.Name = "rbSortByDate";
            this.rbSortByDate.Size = new System.Drawing.Size(63, 17);
            this.rbSortByDate.TabIndex = 1;
            this.rbSortByDate.TabStop = true;
            this.rbSortByDate.Text = "By Date";
            this.rbSortByDate.UseVisualStyleBackColor = true;
            // 
            // frmReportOpts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 528);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "frmReportOpts";
            this.Text = "Dye Batch Transactions";
            this.Load += new System.EventHandler(this.frmReportOpts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboCustomer;
        private System.Windows.Forms.RadioButton rbAllCustomers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbByQuality;
        private System.Windows.Forms.RadioButton rbBatchNumber;
        private System.Windows.Forms.RadioButton rbSortByDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rrbWIPDyeing;
        private System.Windows.Forms.RadioButton rbDyeBatchPending;
    }
}