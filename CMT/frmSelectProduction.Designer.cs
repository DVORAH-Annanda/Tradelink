namespace CMT
{
    partial class frmSelectProduction
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGradeA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpToDate = new System.Windows.Forms.DateTimePicker();
            this.rbGradeB = new System.Windows.Forms.RadioButton();
            this.rbGradeBoth = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbUnits = new System.Windows.Forms.RadioButton();
            this.rbBoxes = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Production Start Date ";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(285, 51);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(126, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.rbGradeBoth);
            this.groupBox1.Controls.Add(this.rbGradeB);
            this.groupBox1.Controls.Add(this.rbGradeA);
            this.groupBox1.Location = new System.Drawing.Point(196, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 254);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Filter";
            // 
            // rbGradeA
            // 
            this.rbGradeA.AutoSize = true;
            this.rbGradeA.Location = new System.Drawing.Point(47, 35);
            this.rbGradeA.Name = "rbGradeA";
            this.rbGradeA.Size = new System.Drawing.Size(88, 17);
            this.rbGradeA.TabIndex = 0;
            this.rbGradeA.TabStop = true;
            this.rbGradeA.Text = "Grade A Only";
            this.rbGradeA.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Production End Date";
            // 
            // DtpToDate
            // 
            this.DtpToDate.Location = new System.Drawing.Point(285, 103);
            this.DtpToDate.Name = "DtpToDate";
            this.DtpToDate.Size = new System.Drawing.Size(126, 20);
            this.DtpToDate.TabIndex = 4;
            // 
            // rbGradeB
            // 
            this.rbGradeB.AutoSize = true;
            this.rbGradeB.Location = new System.Drawing.Point(47, 79);
            this.rbGradeB.Name = "rbGradeB";
            this.rbGradeB.Size = new System.Drawing.Size(88, 17);
            this.rbGradeB.TabIndex = 1;
            this.rbGradeB.TabStop = true;
            this.rbGradeB.Text = "Grade B Only";
            this.rbGradeB.UseVisualStyleBackColor = true;
            // 
            // rbGradeBoth
            // 
            this.rbGradeBoth.AutoSize = true;
            this.rbGradeBoth.Location = new System.Drawing.Point(47, 123);
            this.rbGradeBoth.Name = "rbGradeBoth";
            this.rbGradeBoth.Size = new System.Drawing.Size(84, 17);
            this.rbGradeBoth.TabIndex = 2;
            this.rbGradeBoth.TabStop = true;
            this.rbGradeBoth.Text = "Both Grades";
            this.rbGradeBoth.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBoxes);
            this.groupBox2.Controls.Add(this.rbUnits);
            this.groupBox2.Location = new System.Drawing.Point(33, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 62);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // rbUnits
            // 
            this.rbUnits.AutoSize = true;
            this.rbUnits.Location = new System.Drawing.Point(16, 20);
            this.rbUnits.Name = "rbUnits";
            this.rbUnits.Size = new System.Drawing.Size(49, 17);
            this.rbUnits.TabIndex = 0;
            this.rbUnits.TabStop = true;
            this.rbUnits.Text = "Units";
            this.rbUnits.UseVisualStyleBackColor = true;
            // 
            // rbBoxes
            // 
            this.rbBoxes.AutoSize = true;
            this.rbBoxes.Location = new System.Drawing.Point(114, 20);
            this.rbBoxes.Name = "rbBoxes";
            this.rbBoxes.Size = new System.Drawing.Size(54, 17);
            this.rbBoxes.TabIndex = 1;
            this.rbBoxes.TabStop = true;
            this.rbBoxes.Text = "Boxes";
            this.rbBoxes.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(580, 443);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmSelectProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 504);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.DtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmSelectProduction";
            this.Text = "Production Analysis Selection Screen";
            this.Load += new System.EventHandler(this.frmSelectProduction_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbBoxes;
        private System.Windows.Forms.RadioButton rbUnits;
        private System.Windows.Forms.RadioButton rbGradeBoth;
        private System.Windows.Forms.RadioButton rbGradeB;
        private System.Windows.Forms.RadioButton rbGradeA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpToDate;
        private System.Windows.Forms.Button btnSubmit;
    }
}