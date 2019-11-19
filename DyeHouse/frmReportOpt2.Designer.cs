namespace DyeHouse
{
    partial class frmReportOpt2
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
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbReprocess = new System.Windows.Forms.RadioButton();
            this.rbWIPDyeing = new System.Windows.Forms.RadioButton();
            this.rbWIPDBPending = new System.Windows.Forms.RadioButton();
            this.rbAllCustomers = new System.Windows.Forms.RadioButton();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Controls.Add(this.rbReprocess);
            this.groupBox1.Controls.Add(this.rbWIPDyeing);
            this.groupBox1.Controls.Add(this.rbWIPDBPending);
            this.groupBox1.Location = new System.Drawing.Point(138, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Options";
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(202, 68);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 10;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbReprocess
            // 
            this.rbReprocess.AutoSize = true;
            this.rbReprocess.Location = new System.Drawing.Point(31, 68);
            this.rbReprocess.Name = "rbReprocess";
            this.rbReprocess.Size = new System.Drawing.Size(76, 17);
            this.rbReprocess.TabIndex = 9;
            this.rbReprocess.TabStop = true;
            this.rbReprocess.Text = "Reprocess";
            this.rbReprocess.UseVisualStyleBackColor = true;
            // 
            // rbWIPDyeing
            // 
            this.rbWIPDyeing.AutoSize = true;
            this.rbWIPDyeing.Location = new System.Drawing.Point(202, 31);
            this.rbWIPDyeing.Name = "rbWIPDyeing";
            this.rbWIPDyeing.Size = new System.Drawing.Size(82, 17);
            this.rbWIPDyeing.TabIndex = 8;
            this.rbWIPDyeing.TabStop = true;
            this.rbWIPDyeing.Text = "WIP Dyeing";
            this.rbWIPDyeing.UseVisualStyleBackColor = true;
            // 
            // rbWIPDBPending
            // 
            this.rbWIPDBPending.AutoSize = true;
            this.rbWIPDBPending.Location = new System.Drawing.Point(31, 31);
            this.rbWIPDBPending.Name = "rbWIPDBPending";
            this.rbWIPDBPending.Size = new System.Drawing.Size(152, 17);
            this.rbWIPDBPending.TabIndex = 7;
            this.rbWIPDBPending.TabStop = true;
            this.rbWIPDBPending.Text = "WIP Dye Batches Pending";
            this.rbWIPDBPending.UseVisualStyleBackColor = true;
            // 
            // rbAllCustomers
            // 
            this.rbAllCustomers.AutoSize = true;
            this.rbAllCustomers.Location = new System.Drawing.Point(122, 60);
            this.rbAllCustomers.Name = "rbAllCustomers";
            this.rbAllCustomers.Size = new System.Drawing.Size(88, 17);
            this.rbAllCustomers.TabIndex = 6;
            this.rbAllCustomers.TabStop = true;
            this.rbAllCustomers.Text = "All Customers";
            this.rbAllCustomers.UseVisualStyleBackColor = true;
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(118, 19);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(145, 21);
            this.cmboCustomers.TabIndex = 4;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(559, 390);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmboReportOptions);
            this.groupBox2.Location = new System.Drawing.Point(138, 288);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 125);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sort Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(118, 45);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(145, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbAllCustomers);
            this.groupBox3.Controls.Add(this.cmboCustomers);
            this.groupBox3.Location = new System.Drawing.Point(138, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 98);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Customer";
            // 
            // frmReportOpt2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 425);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReportOpt2";
            this.Text = "List of Outstanding pending dye batches";
            this.Load += new System.EventHandler(this.frmReportOpt2_Load_1);
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
        private System.Windows.Forms.RadioButton rbAllCustomers;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbReprocess;
        private System.Windows.Forms.RadioButton rbWIPDyeing;
        private System.Windows.Forms.RadioButton rbWIPDBPending;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}