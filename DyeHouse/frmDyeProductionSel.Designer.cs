namespace DyeHouse
{
    partial class frmDyeProductionSel
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
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radReprocessed = new System.Windows.Forms.RadioButton();
            this.radFirstTime = new System.Windows.Forms.RadioButton();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkQASummary = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboCustomers);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtFromDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(178, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Required";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(108, 111);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(121, 21);
            this.cmboCustomers.TabIndex = 5;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Customer";
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(113, 71);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(113, 20);
            this.dtToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtFromDate
            // 
            this.dtFromDate.Location = new System.Drawing.Point(113, 32);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(113, 20);
            this.dtFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(550, 364);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radReprocessed);
            this.groupBox2.Controls.Add(this.radFirstTime);
            this.groupBox2.Location = new System.Drawing.Point(178, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Production Selection";
            // 
            // radReprocessed
            // 
            this.radReprocessed.AutoSize = true;
            this.radReprocessed.Location = new System.Drawing.Point(141, 30);
            this.radReprocessed.Name = "radReprocessed";
            this.radReprocessed.Size = new System.Drawing.Size(88, 17);
            this.radReprocessed.TabIndex = 1;
            this.radReprocessed.TabStop = true;
            this.radReprocessed.Text = "Reprocessed";
            this.radReprocessed.UseVisualStyleBackColor = true;
            // 
            // radFirstTime
            // 
            this.radFirstTime.AutoSize = true;
            this.radFirstTime.Location = new System.Drawing.Point(20, 30);
            this.radFirstTime.Name = "radFirstTime";
            this.radFirstTime.Size = new System.Drawing.Size(70, 17);
            this.radFirstTime.TabIndex = 0;
            this.radFirstTime.TabStop = true;
            this.radFirstTime.Text = "First Time";
            this.radFirstTime.UseVisualStyleBackColor = true;
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(286, 364);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(121, 21);
            this.cmboReportOptions.TabIndex = 3;
            this.cmboReportOptions.SelectedIndexChanged += new System.EventHandler(this.cmboReportOptions_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 364);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sort Options";
            // 
            // chkQASummary
            // 
            this.chkQASummary.AutoSize = true;
            this.chkQASummary.Location = new System.Drawing.Point(286, 204);
            this.chkQASummary.Name = "chkQASummary";
            this.chkQASummary.Size = new System.Drawing.Size(87, 17);
            this.chkQASummary.TabIndex = 5;
            this.chkQASummary.Text = "QA Summary";
            this.chkQASummary.UseVisualStyleBackColor = true;
            // 
            // frmDyeProductionSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 457);
            this.Controls.Add(this.chkQASummary);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDyeProductionSel";
            this.Text = "Dye Production Reports";
            this.Load += new System.EventHandler(this.frmDyeProductionSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radReprocessed;
        private System.Windows.Forms.RadioButton radFirstTime;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.CheckBox chkQASummary;
    }
}