namespace TTI2_WF
{
    partial class frmCuttingSelProcess
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
            this.rbRejectPanels = new System.Windows.Forms.RadioButton();
            this.rbCutProduction = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCutProduction);
            this.groupBox1.Controls.Add(this.rbRejectPanels);
            this.groupBox1.Location = new System.Drawing.Point(93, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbRejectPanels
            // 
            this.rbRejectPanels.AutoSize = true;
            this.rbRejectPanels.Location = new System.Drawing.Point(105, 76);
            this.rbRejectPanels.Name = "rbRejectPanels";
            this.rbRejectPanels.Size = new System.Drawing.Size(91, 17);
            this.rbRejectPanels.TabIndex = 0;
            this.rbRejectPanels.TabStop = true;
            this.rbRejectPanels.Text = "Reject Panels";
            this.rbRejectPanels.UseVisualStyleBackColor = true;
            this.rbRejectPanels.CheckedChanged += new System.EventHandler(this.rbRejectPanels_CheckedChanged);
            // 
            // rbCutProduction
            // 
            this.rbCutProduction.AutoSize = true;
            this.rbCutProduction.Location = new System.Drawing.Point(105, 126);
            this.rbCutProduction.Name = "rbCutProduction";
            this.rbCutProduction.Size = new System.Drawing.Size(197, 17);
            this.rbCutProduction.TabIndex = 1;
            this.rbCutProduction.TabStop = true;
            this.rbCutProduction.Text = "Cut Production : Actual Vs Expected";
            this.rbCutProduction.UseVisualStyleBackColor = true;
            this.rbCutProduction.CheckedChanged += new System.EventHandler(this.rbCutProduction_CheckedChanged);
            // 
            // frmCuttingSelProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 429);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCuttingSelProcess";
            this.Text = "Cutting Production Process Loss";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCutProduction;
        private System.Windows.Forms.RadioButton rbRejectPanels;
    }
}