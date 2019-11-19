namespace Knitting
{
    partial class frmGreigeMeasurementSel
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboQuality = new Knitting.CheckComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboMachines = new Knitting.CheckComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboOperators = new Knitting.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOperator = new System.Windows.Forms.RadioButton();
            this.rbMachine = new System.Windows.Forms.RadioButton();
            this.rbQuality = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(238, 46);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(132, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(238, 93);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(132, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Quality";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(523, 448);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboQuality
            // 
            this.cmboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(238, 158);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(192, 21);
            this.cmboQuality.TabIndex = 5;
            this.cmboQuality.Text = "Select Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Machine";
            // 
            // cmboMachines
            // 
            this.cmboMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboMachines.FormattingEnabled = true;
            this.cmboMachines.Location = new System.Drawing.Point(238, 226);
            this.cmboMachines.Name = "cmboMachines";
            this.cmboMachines.Size = new System.Drawing.Size(192, 21);
            this.cmboMachines.TabIndex = 8;
            this.cmboMachines.Text = "Select Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Operator";
            // 
            // cmboOperators
            // 
            this.cmboOperators.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboOperators.FormattingEnabled = true;
            this.cmboOperators.Location = new System.Drawing.Point(238, 294);
            this.cmboOperators.Name = "cmboOperators";
            this.cmboOperators.Size = new System.Drawing.Size(192, 21);
            this.cmboOperators.TabIndex = 10;
            this.cmboOperators.Text = "Select Options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOperator);
            this.groupBox1.Controls.Add(this.rbMachine);
            this.groupBox1.Controls.Add(this.rbQuality);
            this.groupBox1.Location = new System.Drawing.Point(238, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 137);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grouping Options";
            // 
            // rbOperator
            // 
            this.rbOperator.AutoSize = true;
            this.rbOperator.Location = new System.Drawing.Point(52, 103);
            this.rbOperator.Name = "rbOperator";
            this.rbOperator.Size = new System.Drawing.Size(66, 17);
            this.rbOperator.TabIndex = 2;
            this.rbOperator.Text = "Operator";
            this.rbOperator.UseVisualStyleBackColor = true;
            // 
            // rbMachine
            // 
            this.rbMachine.AutoSize = true;
            this.rbMachine.Location = new System.Drawing.Point(52, 67);
            this.rbMachine.Name = "rbMachine";
            this.rbMachine.Size = new System.Drawing.Size(66, 17);
            this.rbMachine.TabIndex = 1;
            this.rbMachine.Text = "Machine";
            this.rbMachine.UseVisualStyleBackColor = true;
            // 
            // rbQuality
            // 
            this.rbQuality.AutoSize = true;
            this.rbQuality.Checked = true;
            this.rbQuality.Location = new System.Drawing.Point(52, 31);
            this.rbQuality.Name = "rbQuality";
            this.rbQuality.Size = new System.Drawing.Size(57, 17);
            this.rbQuality.TabIndex = 0;
            this.rbQuality.TabStop = true;
            this.rbQuality.Text = "Quality";
            this.rbQuality.UseVisualStyleBackColor = true;
            // 
            // frmGreigeMeasurementSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboOperators);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboMachines);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboQuality);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmGreigeMeasurementSel";
            this.Text = "Greige Key Measurements ";
            this.Load += new System.EventHandler(this.frmGreigeMeasurementSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label4;
        private Knitting.CheckComboBox cmboQuality;
        private Knitting.CheckComboBox cmboMachines;
        private Knitting.CheckComboBox cmboOperators;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOperator;
        private System.Windows.Forms.RadioButton rbMachine;
        private System.Windows.Forms.RadioButton rbQuality;
    
        private System.Windows.Forms.Label label5;
    }
}