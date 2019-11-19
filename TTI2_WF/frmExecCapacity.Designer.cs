namespace TTI2_WF
{
    partial class frmExecCapacity
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
            this.rbCapKnitting = new System.Windows.Forms.RadioButton();
            this.rbCapSpinning = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCapKnitting);
            this.groupBox1.Controls.Add(this.rbCapSpinning);
            this.groupBox1.Location = new System.Drawing.Point(121, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbCapKnitting
            // 
            this.rbCapKnitting.AutoSize = true;
            this.rbCapKnitting.Location = new System.Drawing.Point(123, 143);
            this.rbCapKnitting.Name = "rbCapKnitting";
            this.rbCapKnitting.Size = new System.Drawing.Size(60, 17);
            this.rbCapKnitting.TabIndex = 1;
            this.rbCapKnitting.TabStop = true;
            this.rbCapKnitting.Text = "Knitting";
            this.rbCapKnitting.UseVisualStyleBackColor = true;
            this.rbCapKnitting.CheckedChanged += new System.EventHandler(this.rbCapKnitting_CheckedChanged);
            // 
            // rbCapSpinning
            // 
            this.rbCapSpinning.AutoSize = true;
            this.rbCapSpinning.Location = new System.Drawing.Point(123, 70);
            this.rbCapSpinning.Name = "rbCapSpinning";
            this.rbCapSpinning.Size = new System.Drawing.Size(124, 17);
            this.rbCapSpinning.TabIndex = 0;
            this.rbCapSpinning.TabStop = true;
            this.rbCapSpinning.Text = "Spinning Department";
            this.rbCapSpinning.UseVisualStyleBackColor = true;
            this.rbCapSpinning.CheckedChanged += new System.EventHandler(this.rbCapSpinning_CheckedChanged);
            // 
            // frmExecCapacity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 475);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmExecCapacity";
            this.Text = "Executive Capacity Utilsation";
            this.Load += new System.EventHandler(this.frmExecCapacity_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCapKnitting;
        private System.Windows.Forms.RadioButton rbCapSpinning;
    }
}