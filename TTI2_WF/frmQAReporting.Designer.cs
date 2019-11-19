namespace TTI2_WF
{
    partial class frmQAReporting
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
            this.chkSpinning = new System.Windows.Forms.CheckBox();
            this.chkKnitting = new System.Windows.Forms.CheckBox();
            this.chkCutting = new System.Windows.Forms.CheckBox();
            this.chkDyeing = new System.Windows.Forms.CheckBox();
            this.chkCMT = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCMT);
            this.groupBox1.Controls.Add(this.chkDyeing);
            this.groupBox1.Controls.Add(this.chkCutting);
            this.groupBox1.Controls.Add(this.chkKnitting);
            this.groupBox1.Controls.Add(this.chkSpinning);
            this.groupBox1.Location = new System.Drawing.Point(158, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reporting Departments";
            // 
            // chkSpinning
            // 
            this.chkSpinning.AutoSize = true;
            this.chkSpinning.Location = new System.Drawing.Point(138, 40);
            this.chkSpinning.Name = "chkSpinning";
            this.chkSpinning.Size = new System.Drawing.Size(67, 17);
            this.chkSpinning.TabIndex = 5;
            this.chkSpinning.Text = "Spinning";
            this.chkSpinning.UseVisualStyleBackColor = true;
            this.chkSpinning.CheckedChanged += new System.EventHandler(this.chkSpinning_CheckedChanged);
            // 
            // chkKnitting
            // 
            this.chkKnitting.AutoSize = true;
            this.chkKnitting.Location = new System.Drawing.Point(138, 81);
            this.chkKnitting.Name = "chkKnitting";
            this.chkKnitting.Size = new System.Drawing.Size(61, 17);
            this.chkKnitting.TabIndex = 6;
            this.chkKnitting.Text = "Knitting";
            this.chkKnitting.UseVisualStyleBackColor = true;
            this.chkKnitting.CheckedChanged += new System.EventHandler(this.chkKnitting_CheckedChanged);
            // 
            // chkCutting
            // 
            this.chkCutting.AutoSize = true;
            this.chkCutting.Location = new System.Drawing.Point(138, 163);
            this.chkCutting.Name = "chkCutting";
            this.chkCutting.Size = new System.Drawing.Size(59, 17);
            this.chkCutting.TabIndex = 7;
            this.chkCutting.Text = "Cutting";
            this.chkCutting.UseVisualStyleBackColor = true;
            this.chkCutting.CheckedChanged += new System.EventHandler(this.chkCutting_CheckedChanged);
            // 
            // chkDyeing
            // 
            this.chkDyeing.AutoSize = true;
            this.chkDyeing.Location = new System.Drawing.Point(138, 122);
            this.chkDyeing.Name = "chkDyeing";
            this.chkDyeing.Size = new System.Drawing.Size(59, 17);
            this.chkDyeing.TabIndex = 8;
            this.chkDyeing.Text = "Dyeing";
            this.chkDyeing.UseVisualStyleBackColor = true;
            this.chkDyeing.CheckedChanged += new System.EventHandler(this.chkDyeing_CheckedChanged);
            // 
            // chkCMT
            // 
            this.chkCMT.AutoSize = true;
            this.chkCMT.Location = new System.Drawing.Point(138, 204);
            this.chkCMT.Name = "chkCMT";
            this.chkCMT.Size = new System.Drawing.Size(49, 17);
            this.chkCMT.TabIndex = 9;
            this.chkCMT.Text = "CMT";
            this.chkCMT.UseVisualStyleBackColor = true;
            this.chkCMT.CheckedChanged += new System.EventHandler(this.chkCMT_CheckedChanged);
            // 
            // frmQAReporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 471);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQAReporting";
            this.Text = "QA Reporting Manager";
            this.Load += new System.EventHandler(this.frmQAReporting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCMT;
        private System.Windows.Forms.CheckBox chkDyeing;
        private System.Windows.Forms.CheckBox chkCutting;
        private System.Windows.Forms.CheckBox chkKnitting;
        private System.Windows.Forms.CheckBox chkSpinning;
    }
}