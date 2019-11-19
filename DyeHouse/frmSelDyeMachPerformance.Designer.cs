namespace DyeHouse
{
    partial class frmSelDyeMachPerformance
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbFirstTime = new System.Windows.Forms.RadioButton();
            this.rbOwnReprocessing = new System.Windows.Forms.RadioButton();
            this.rbCommission = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbModeSummary = new System.Windows.Forms.RadioButton();
            this.rbModeDetail = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboMachines = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(563, 444);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboMachines);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Location = new System.Drawing.Point(90, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 141);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Parameters";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(253, 19);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(253, 56);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(115, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCommission);
            this.groupBox2.Controls.Add(this.rbOwnReprocessing);
            this.groupBox2.Controls.Add(this.rbFirstTime);
            this.groupBox2.Location = new System.Drawing.Point(90, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Production Type";
            // 
            // rbFirstTime
            // 
            this.rbFirstTime.AutoSize = true;
            this.rbFirstTime.Checked = true;
            this.rbFirstTime.Location = new System.Drawing.Point(29, 32);
            this.rbFirstTime.Name = "rbFirstTime";
            this.rbFirstTime.Size = new System.Drawing.Size(125, 17);
            this.rbFirstTime.TabIndex = 0;
            this.rbFirstTime.TabStop = true;
            this.rbFirstTime.Text = "First Time Processing";
            this.rbFirstTime.UseVisualStyleBackColor = true;
            // 
            // rbOwnReprocessing
            // 
            this.rbOwnReprocessing.AutoSize = true;
            this.rbOwnReprocessing.Location = new System.Drawing.Point(178, 32);
            this.rbOwnReprocessing.Name = "rbOwnReprocessing";
            this.rbOwnReprocessing.Size = new System.Drawing.Size(115, 17);
            this.rbOwnReprocessing.TabIndex = 1;
            this.rbOwnReprocessing.Text = "Own Reprocessing";
            this.rbOwnReprocessing.UseVisualStyleBackColor = true;
            // 
            // rbCommission
            // 
            this.rbCommission.AutoSize = true;
            this.rbCommission.Location = new System.Drawing.Point(324, 32);
            this.rbCommission.Name = "rbCommission";
            this.rbCommission.Size = new System.Drawing.Size(80, 17);
            this.rbCommission.TabIndex = 2;
            this.rbCommission.TabStop = true;
            this.rbCommission.Text = "Commission";
            this.rbCommission.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbModeDetail);
            this.groupBox3.Controls.Add(this.rbModeSummary);
            this.groupBox3.Location = new System.Drawing.Point(90, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 65);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Report Mode";
            // 
            // rbModeSummary
            // 
            this.rbModeSummary.AutoSize = true;
            this.rbModeSummary.Enabled = false;
            this.rbModeSummary.Location = new System.Drawing.Point(152, 29);
            this.rbModeSummary.Name = "rbModeSummary";
            this.rbModeSummary.Size = new System.Drawing.Size(103, 17);
            this.rbModeSummary.TabIndex = 0;
            this.rbModeSummary.Text = "Summary Report";
            this.rbModeSummary.UseVisualStyleBackColor = true;
            // 
            // rbModeDetail
            // 
            this.rbModeDetail.AutoSize = true;
            this.rbModeDetail.Checked = true;
            this.rbModeDetail.Location = new System.Drawing.Point(18, 29);
            this.rbModeDetail.Name = "rbModeDetail";
            this.rbModeDetail.Size = new System.Drawing.Size(52, 17);
            this.rbModeDetail.TabIndex = 1;
            this.rbModeDetail.TabStop = true;
            this.rbModeDetail.Text = "Detail";
            this.rbModeDetail.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Production Machines";
            // 
            // cmboMachines
            // 
            this.cmboMachines.FormattingEnabled = true;
            this.cmboMachines.Location = new System.Drawing.Point(208, 97);
            this.cmboMachines.Name = "cmboMachines";
            this.cmboMachines.Size = new System.Drawing.Size(160, 21);
            this.cmboMachines.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmboReportOptions);
            this.groupBox4.Location = new System.Drawing.Point(90, 361);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(386, 100);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Report Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(82, 39);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(223, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // frmSelDyeMachPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 494);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmSelDyeMachPerformance";
            this.Text = "Dye Machine Performance";
            this.Load += new System.EventHandler(this.frmSelDyeMachPerformance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboMachines;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCommission;
        private System.Windows.Forms.RadioButton rbOwnReprocessing;
        private System.Windows.Forms.RadioButton rbFirstTime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbModeDetail;
        private System.Windows.Forms.RadioButton rbModeSummary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmboReportOptions;
    }
}