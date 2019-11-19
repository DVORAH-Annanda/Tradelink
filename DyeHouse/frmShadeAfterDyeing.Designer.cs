namespace DyeHouse
{
    partial class frmShadeAfterDyeing
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
            this.rbRejected = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboRemedy = new DyeHouse.CheckComboBox();
            this.cmboCause = new DyeHouse.CheckComboBox();
            this.cmboDyeOperator = new DyeHouse.CheckComboBox();
            this.cmboDyeMachine = new DyeHouse.CheckComboBox();
            this.cmboColour = new DyeHouse.CheckComboBox();
            this.cmboQuality = new DyeHouse.CheckComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRejected);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Location = new System.Drawing.Point(172, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dates selection";
            // 
            // rbRejected
            // 
            this.rbRejected.AutoSize = true;
            this.rbRejected.Location = new System.Drawing.Point(166, 104);
            this.rbRejected.Name = "rbRejected";
            this.rbRejected.Size = new System.Drawing.Size(92, 17);
            this.rbRejected.TabIndex = 5;
            this.rbRejected.Text = "Rejected Only";
            this.rbRejected.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(73, 104);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(78, 17);
            this.rbAll.TabIndex = 4;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All Batches";
            this.rbAll.UseVisualStyleBackColor = true;
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
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(166, 58);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(126, 20);
            this.dtpToDate.TabIndex = 2;
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
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(166, 19);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(126, 20);
            this.dtpFromDate.TabIndex = 0;
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
            this.groupBox2.Location = new System.Drawing.Point(149, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 231);
            this.groupBox2.TabIndex = 1;
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
            this.cmboRemedy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboRemedy.FormattingEnabled = true;
            this.cmboRemedy.Location = new System.Drawing.Point(171, 189);
            this.cmboRemedy.Name = "cmboRemedy";
            this.cmboRemedy.Size = new System.Drawing.Size(172, 21);
            this.cmboRemedy.TabIndex = 5;
            this.cmboRemedy.Text = "Select Options";
            // 
            // cmboCause
            // 
            this.cmboCause.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCause.FormattingEnabled = true;
            this.cmboCause.Location = new System.Drawing.Point(171, 155);
            this.cmboCause.Name = "cmboCause";
            this.cmboCause.Size = new System.Drawing.Size(172, 21);
            this.cmboCause.TabIndex = 4;
            this.cmboCause.Text = "Select Options";
            // 
            // cmboDyeOperator
            // 
            this.cmboDyeOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDyeOperator.FormattingEnabled = true;
            this.cmboDyeOperator.Location = new System.Drawing.Point(171, 121);
            this.cmboDyeOperator.Name = "cmboDyeOperator";
            this.cmboDyeOperator.Size = new System.Drawing.Size(172, 21);
            this.cmboDyeOperator.TabIndex = 3;
            this.cmboDyeOperator.Text = "Select Options";
            // 
            // cmboDyeMachine
            // 
            this.cmboDyeMachine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDyeMachine.FormattingEnabled = true;
            this.cmboDyeMachine.Location = new System.Drawing.Point(171, 87);
            this.cmboDyeMachine.Name = "cmboDyeMachine";
            this.cmboDyeMachine.Size = new System.Drawing.Size(172, 21);
            this.cmboDyeMachine.TabIndex = 2;
            this.cmboDyeMachine.Text = "Select Options";
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(171, 53);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(172, 21);
            this.cmboColour.TabIndex = 1;
            this.cmboColour.Text = "Select Options";
            // 
            // cmboQuality
            // 
            this.cmboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(171, 19);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(172, 21);
            this.cmboQuality.TabIndex = 0;
            this.cmboQuality.Text = "Select Options";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmboReportOptions);
            this.groupBox3.Location = new System.Drawing.Point(172, 420);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 86);
            this.groupBox3.TabIndex = 2;
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
            this.cmboReportOptions.SelectedIndexChanged += new System.EventHandler(this.cmboReportOptions_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(541, 533);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmShadeAfterDyeing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 568);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmShadeAfterDyeing";
            this.Text = "Shade results straiight after dyeing";
            this.Load += new System.EventHandler(this.frmShadeAfterDyeing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        /*private System.Windows.Forms.ComboBox cmboRemedy;
        private System.Windows.Forms.ComboBox cmboCause;
        private System.Windows.Forms.ComboBox cmboDyeOperator;
        private System.Windows.Forms.ComboBox cmboDyeMachine;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboQuality;*/
        private DyeHouse.CheckComboBox cmboRemedy;
        private DyeHouse.CheckComboBox cmboCause;
        private DyeHouse.CheckComboBox cmboDyeOperator;
        private DyeHouse.CheckComboBox cmboDyeMachine;
        private DyeHouse.CheckComboBox cmboColour;
        private DyeHouse.CheckComboBox cmboQuality;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RadioButton rbRejected;
        private System.Windows.Forms.RadioButton rbAll;
    }
}