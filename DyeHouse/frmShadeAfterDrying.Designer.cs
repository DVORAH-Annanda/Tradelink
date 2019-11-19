namespace DyeHouse
{
    partial class frmShadeAfterDrying
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboRemedy = new System.Windows.Forms.ComboBox();
            this.cmboCause = new System.Windows.Forms.ComboBox();
            this.cmboDyeOperator = new System.Windows.Forms.ComboBox();
            this.cmboDyeMachine = new System.Windows.Forms.ComboBox();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.cmboQuality = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.rbShades = new System.Windows.Forms.RadioButton();
            this.rbStability = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(431, 468);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmboReportOptions);
            this.groupBox3.Location = new System.Drawing.Point(76, 413);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 100);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sort Criteria";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(60, 35);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(166, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmboRemedy);
            this.groupBox2.Controls.Add(this.cmboCause);
            this.groupBox2.Controls.Add(this.cmboDyeOperator);
            this.groupBox2.Controls.Add(this.cmboDyeMachine);
            this.groupBox2.Controls.Add(this.cmboColour);
            this.groupBox2.Controls.Add(this.cmboQuality);
            this.groupBox2.Location = new System.Drawing.Point(76, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 231);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection criteria";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Remedy when not compliant";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Cause when not complying";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Dye Operator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Dye Machine";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Colour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Product";
            // 
            // cmboRemedy
            // 
            this.cmboRemedy.FormattingEnabled = true;
            this.cmboRemedy.Location = new System.Drawing.Point(171, 189);
            this.cmboRemedy.Name = "cmboRemedy";
            this.cmboRemedy.Size = new System.Drawing.Size(121, 21);
            this.cmboRemedy.TabIndex = 5;
            // 
            // cmboCause
            // 
            this.cmboCause.FormattingEnabled = true;
            this.cmboCause.Location = new System.Drawing.Point(171, 155);
            this.cmboCause.Name = "cmboCause";
            this.cmboCause.Size = new System.Drawing.Size(121, 21);
            this.cmboCause.TabIndex = 4;
            // 
            // cmboDyeOperator
            // 
            this.cmboDyeOperator.FormattingEnabled = true;
            this.cmboDyeOperator.Location = new System.Drawing.Point(171, 121);
            this.cmboDyeOperator.Name = "cmboDyeOperator";
            this.cmboDyeOperator.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOperator.TabIndex = 3;
            // 
            // cmboDyeMachine
            // 
            this.cmboDyeMachine.FormattingEnabled = true;
            this.cmboDyeMachine.Location = new System.Drawing.Point(171, 87);
            this.cmboDyeMachine.Name = "cmboDyeMachine";
            this.cmboDyeMachine.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeMachine.TabIndex = 2;
            // 
            // cmboColour
            // 
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(171, 53);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(121, 21);
            this.cmboColour.TabIndex = 1;
            // 
            // cmboQuality
            // 
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(171, 19);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(121, 21);
            this.cmboQuality.TabIndex = 0;
            this.cmboQuality.SelectedIndexChanged += new System.EventHandler(this.cmboQuality_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStability);
            this.groupBox1.Controls.Add(this.rbShades);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Location = new System.Drawing.Point(76, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 132);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dates selection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(166, 58);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(126, 20);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(166, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // rbShades
            // 
            this.rbShades.AutoSize = true;
            this.rbShades.Location = new System.Drawing.Point(60, 98);
            this.rbShades.Name = "rbShades";
            this.rbShades.Size = new System.Drawing.Size(61, 17);
            this.rbShades.TabIndex = 4;
            this.rbShades.TabStop = true;
            this.rbShades.Text = "Shades";
            this.rbShades.UseVisualStyleBackColor = true;
            // 
            // rbStability
            // 
            this.rbStability.AutoSize = true;
            this.rbStability.Location = new System.Drawing.Point(181, 98);
            this.rbStability.Name = "rbStability";
            this.rbStability.Size = new System.Drawing.Size(61, 17);
            this.rbStability.TabIndex = 5;
            this.rbStability.TabStop = true;
            this.rbStability.Text = "Stability";
            this.rbStability.UseVisualStyleBackColor = true;
            // 
            // frmShadeAfterDrying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 537);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmShadeAfterDrying";
            this.Text = "Stabilities PK after Drying";
            this.Load += new System.EventHandler(this.frmShadeAfterDrying_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboRemedy;
        private System.Windows.Forms.ComboBox cmboCause;
        private System.Windows.Forms.ComboBox cmboDyeOperator;
        private System.Windows.Forms.ComboBox cmboDyeMachine;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboQuality;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbStability;
        private System.Windows.Forms.RadioButton rbShades;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}