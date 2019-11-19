namespace Spinning
{
    partial class frmQAReportSelection
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbOutsideTNo = new System.Windows.Forms.RadioButton();
            this.rbOutsideTYes = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTopTolerance = new System.Windows.Forms.TextBox();
            this.txtBottomTolerance = new System.Windows.Forms.TextBox();
            this.chkRSBQaSummary = new System.Windows.Forms.CheckBox();
            this.dtpRSBToDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpRSBFromDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmboRSBMachines = new Spinning.CheckComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbSpinning = new System.Windows.Forms.RadioButton();
            this.rbRSBMeasurements = new System.Windows.Forms.RadioButton();
            this.rbCardMeasurements = new System.Windows.Forms.RadioButton();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(527, 319);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtTopTolerance);
            this.groupBox4.Controls.Add(this.txtBottomTolerance);
            this.groupBox4.Controls.Add(this.chkRSBQaSummary);
            this.groupBox4.Controls.Add(this.dtpRSBToDate);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.dtpRSBFromDate);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Location = new System.Drawing.Point(36, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(560, 222);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selection Criteria";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbOutsideTNo);
            this.groupBox7.Controls.Add(this.rbOutsideTYes);
            this.groupBox7.Location = new System.Drawing.Point(24, 157);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(129, 44);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Outside Tolerance";
            // 
            // rbOutsideTNo
            // 
            this.rbOutsideTNo.AutoSize = true;
            this.rbOutsideTNo.Location = new System.Drawing.Point(77, 21);
            this.rbOutsideTNo.Name = "rbOutsideTNo";
            this.rbOutsideTNo.Size = new System.Drawing.Size(39, 17);
            this.rbOutsideTNo.TabIndex = 1;
            this.rbOutsideTNo.TabStop = true;
            this.rbOutsideTNo.Text = "No";
            this.rbOutsideTNo.UseVisualStyleBackColor = true;
            // 
            // rbOutsideTYes
            // 
            this.rbOutsideTYes.AutoSize = true;
            this.rbOutsideTYes.Location = new System.Drawing.Point(25, 21);
            this.rbOutsideTYes.Name = "rbOutsideTYes";
            this.rbOutsideTYes.Size = new System.Drawing.Size(43, 17);
            this.rbOutsideTYes.TabIndex = 0;
            this.rbOutsideTYes.TabStop = true;
            this.rbOutsideTYes.Text = "Yes";
            this.rbOutsideTYes.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(387, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Upper Tolerance";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Lower Tolerance";
            // 
            // txtTopTolerance
            // 
            this.txtTopTolerance.Location = new System.Drawing.Point(485, 177);
            this.txtTopTolerance.Name = "txtTopTolerance";
            this.txtTopTolerance.Size = new System.Drawing.Size(59, 20);
            this.txtTopTolerance.TabIndex = 18;
            this.txtTopTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBottomTolerance
            // 
            this.txtBottomTolerance.Location = new System.Drawing.Point(313, 177);
            this.txtBottomTolerance.Name = "txtBottomTolerance";
            this.txtBottomTolerance.Size = new System.Drawing.Size(59, 20);
            this.txtBottomTolerance.TabIndex = 17;
            this.txtBottomTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkRSBQaSummary
            // 
            this.chkRSBQaSummary.AutoSize = true;
            this.chkRSBQaSummary.Location = new System.Drawing.Point(220, 132);
            this.chkRSBQaSummary.Name = "chkRSBQaSummary";
            this.chkRSBQaSummary.Size = new System.Drawing.Size(87, 17);
            this.chkRSBQaSummary.TabIndex = 16;
            this.chkRSBQaSummary.Text = "QA Summary";
            this.chkRSBQaSummary.UseVisualStyleBackColor = true;
            // 
            // dtpRSBToDate
            // 
            this.dtpRSBToDate.Location = new System.Drawing.Point(358, 87);
            this.dtpRSBToDate.Name = "dtpRSBToDate";
            this.dtpRSBToDate.Size = new System.Drawing.Size(136, 20);
            this.dtpRSBToDate.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "To Date";
            // 
            // dtpRSBFromDate
            // 
            this.dtpRSBFromDate.Location = new System.Drawing.Point(82, 87);
            this.dtpRSBFromDate.Name = "dtpRSBFromDate";
            this.dtpRSBFromDate.Size = new System.Drawing.Size(136, 20);
            this.dtpRSBFromDate.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "From Date";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.cmboRSBMachines);
            this.groupBox6.Location = new System.Drawing.Point(20, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(516, 51);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Machine selection";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Select a Machine ";
            // 
            // cmboRSBMachines
            // 
            this.cmboRSBMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboRSBMachines.FormattingEnabled = true;
            this.cmboRSBMachines.Location = new System.Drawing.Point(159, 19);
            this.cmboRSBMachines.Name = "cmboRSBMachines";
            this.cmboRSBMachines.Size = new System.Drawing.Size(243, 21);
            this.cmboRSBMachines.TabIndex = 2;
            this.cmboRSBMachines.Text = "Select Options";
            this.cmboRSBMachines.SelectedIndexChanged += new System.EventHandler(this.cmboRSBMachines_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbSpinning);
            this.groupBox8.Controls.Add(this.rbRSBMeasurements);
            this.groupBox8.Controls.Add(this.rbCardMeasurements);
            this.groupBox8.Location = new System.Drawing.Point(42, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(554, 46);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Report Selection ";
            // 
            // rbSpinning
            // 
            this.rbSpinning.AutoSize = true;
            this.rbSpinning.Location = new System.Drawing.Point(421, 19);
            this.rbSpinning.Name = "rbSpinning";
            this.rbSpinning.Size = new System.Drawing.Size(115, 17);
            this.rbSpinning.TabIndex = 2;
            this.rbSpinning.Text = "Spinning Machines";
            this.rbSpinning.UseVisualStyleBackColor = true;
            // 
            // rbRSBMeasurements
            // 
            this.rbRSBMeasurements.AutoSize = true;
            this.rbRSBMeasurements.Location = new System.Drawing.Point(217, 19);
            this.rbRSBMeasurements.Name = "rbRSBMeasurements";
            this.rbRSBMeasurements.Size = new System.Drawing.Size(119, 17);
            this.rbRSBMeasurements.TabIndex = 1;
            this.rbRSBMeasurements.Text = "RSB Measurements";
            this.rbRSBMeasurements.UseVisualStyleBackColor = true;
            // 
            // rbCardMeasurements
            // 
            this.rbCardMeasurements.AutoSize = true;
            this.rbCardMeasurements.Checked = true;
            this.rbCardMeasurements.Location = new System.Drawing.Point(34, 19);
            this.rbCardMeasurements.Name = "rbCardMeasurements";
            this.rbCardMeasurements.Size = new System.Drawing.Size(119, 17);
            this.rbCardMeasurements.TabIndex = 0;
            this.rbCardMeasurements.TabStop = true;
            this.rbCardMeasurements.Text = "Card Measurements";
            this.rbCardMeasurements.UseVisualStyleBackColor = true;
            // 
            // frmQAReportSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 359);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmQAReportSelection";
            this.Text = "Quality Assurance Report Selection";
            this.Load += new System.EventHandler(this.frmQAReportSelection_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        // private System.Windows.Forms.ComboBox cmbMachines;
        // private System.Windows.Forms.ComboBox cmbMachines2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbOutsideTNo;
        private System.Windows.Forms.RadioButton rbOutsideTYes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTopTolerance;
        private System.Windows.Forms.TextBox txtBottomTolerance;
        private System.Windows.Forms.CheckBox chkRSBQaSummary;
        private System.Windows.Forms.DateTimePicker dtpRSBToDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpRSBFromDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label9;
        private CheckComboBox cmboRSBMachines;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbSpinning;
        private System.Windows.Forms.RadioButton rbRSBMeasurements;
        private System.Windows.Forms.RadioButton rbCardMeasurements;
    }
}