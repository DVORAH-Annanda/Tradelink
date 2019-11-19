namespace CMT
{
    partial class frmSelFeederFChk
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
            this.cmboStyles = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboSupervisor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboLines = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboFacility = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtptoDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpfromDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboStyles);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmboCutSheet);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmboSupervisor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmboLines);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmboFacility);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtptoDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpfromDate);
            this.groupBox1.Location = new System.Drawing.Point(167, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 368);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Selection";
            // 
            // cmboStyles
            // 
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(88, 326);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(121, 21);
            this.cmboStyles.TabIndex = 13;
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.Selected_IndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Style";
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(88, 277);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(121, 21);
            this.cmboCutSheet.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "CutSheet";
            // 
            // cmboSupervisor
            // 
            this.cmboSupervisor.FormattingEnabled = true;
            this.cmboSupervisor.Location = new System.Drawing.Point(88, 227);
            this.cmboSupervisor.Name = "cmboSupervisor";
            this.cmboSupervisor.Size = new System.Drawing.Size(121, 21);
            this.cmboSupervisor.TabIndex = 9;
            this.cmboSupervisor.SelectedValueChanged += new System.EventHandler(this.Selected_IndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Supervisor";
            // 
            // cmboLines
            // 
            this.cmboLines.FormattingEnabled = true;
            this.cmboLines.Location = new System.Drawing.Point(88, 177);
            this.cmboLines.Name = "cmboLines";
            this.cmboLines.Size = new System.Drawing.Size(121, 21);
            this.cmboLines.TabIndex = 7;
            this.cmboLines.SelectedIndexChanged += new System.EventHandler(this.Selected_IndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Line";
            // 
            // cmboFacility
            // 
            this.cmboFacility.FormattingEnabled = true;
            this.cmboFacility.Location = new System.Drawing.Point(88, 127);
            this.cmboFacility.Name = "cmboFacility";
            this.cmboFacility.Size = new System.Drawing.Size(121, 21);
            this.cmboFacility.TabIndex = 5;
            this.cmboFacility.SelectedIndexChanged += new System.EventHandler(this.Selected_IndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "CMT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // dtptoDate
            // 
            this.dtptoDate.Location = new System.Drawing.Point(88, 78);
            this.dtptoDate.Name = "dtptoDate";
            this.dtptoDate.Size = new System.Drawing.Size(141, 20);
            this.dtptoDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // dtpfromDate
            // 
            this.dtpfromDate.Location = new System.Drawing.Point(88, 29);
            this.dtpfromDate.Name = "dtpfromDate";
            this.dtpfromDate.Size = new System.Drawing.Size(141, 20);
            this.dtpfromDate.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboReportOptions);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(167, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Sort Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(77, 41);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(164, 21);
            this.cmboReportOptions.TabIndex = 1;
            this.cmboReportOptions.SelectedIndexChanged += new System.EventHandler(this.cmboReportOptions_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sort";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(509, 557);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmSelFeederFChk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 607);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSelFeederFChk";
            this.Text = "Line Feeder Quality Check List";
            this.Load += new System.EventHandler(this.frmSelFeederFChk_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmboSupervisor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboLines;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboFacility;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtptoDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpfromDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSubmit;
    }
}