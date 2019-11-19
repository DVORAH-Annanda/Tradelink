namespace DyeHouse
{
    partial class frmReportOpts4
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.cmboQuality = new System.Windows.Forms.ComboBox();
            this.cmboRemedy = new System.Windows.Forms.ComboBox();
            this.cmboFaultCode = new System.Windows.Forms.ComboBox();
            this.cmboDyeMachine = new System.Windows.Forms.ComboBox();
            this.cmboBatches = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmboReportOptions);
            this.groupBox2.Location = new System.Drawing.Point(138, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 105);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sort Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(118, 41);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(194, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(587, 441);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboColour);
            this.groupBox1.Controls.Add(this.cmboQuality);
            this.groupBox1.Controls.Add(this.cmboRemedy);
            this.groupBox1.Controls.Add(this.cmboFaultCode);
            this.groupBox1.Controls.Add(this.cmboDyeMachine);
            this.groupBox1.Controls.Add(this.cmboBatches);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(138, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 275);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Selection";
            // 
            // cmboColour
            // 
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(133, 221);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(121, 21);
            this.cmboColour.TabIndex = 16;
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboQuality
            // 
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(133, 185);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(121, 21);
            this.cmboQuality.TabIndex = 15;
            this.cmboQuality.SelectedValueChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboRemedy
            // 
            this.cmboRemedy.FormattingEnabled = true;
            this.cmboRemedy.Location = new System.Drawing.Point(133, 149);
            this.cmboRemedy.Name = "cmboRemedy";
            this.cmboRemedy.Size = new System.Drawing.Size(121, 21);
            this.cmboRemedy.TabIndex = 14;
            this.cmboRemedy.SelectedValueChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboFaultCode
            // 
            this.cmboFaultCode.FormattingEnabled = true;
            this.cmboFaultCode.Location = new System.Drawing.Point(133, 113);
            this.cmboFaultCode.Name = "cmboFaultCode";
            this.cmboFaultCode.Size = new System.Drawing.Size(121, 21);
            this.cmboFaultCode.TabIndex = 13;
            this.cmboFaultCode.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboDyeMachine
            // 
            this.cmboDyeMachine.FormattingEnabled = true;
            this.cmboDyeMachine.Location = new System.Drawing.Point(133, 74);
            this.cmboDyeMachine.Name = "cmboDyeMachine";
            this.cmboDyeMachine.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeMachine.TabIndex = 11;
            this.cmboDyeMachine.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboBatches
            // 
            this.cmboBatches.FormattingEnabled = true;
            this.cmboBatches.Location = new System.Drawing.Point(133, 38);
            this.cmboBatches.Name = "cmboBatches";
            this.cmboBatches.Size = new System.Drawing.Size(121, 21);
            this.cmboBatches.TabIndex = 10;
            this.cmboBatches.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "By colour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "By quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "By remedy code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "By Fault Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Batches";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Dye Machine";
            // 
            // frmReportOpts4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmReportOpts4";
            this.Text = "List of reprocessed batches";
            this.Load += new System.EventHandler(this.frmReportOpts4_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboQuality;
        private System.Windows.Forms.ComboBox cmboRemedy;
        private System.Windows.Forms.ComboBox cmboFaultCode;
        private System.Windows.Forms.ComboBox cmboDyeMachine;
        private System.Windows.Forms.ComboBox cmboBatches;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
    }
}