namespace TTI2_WF
{
    partial class frmQACuttingSel
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
            this.rbCutProduction = new System.Windows.Forms.RadioButton();
            this.rbCutWIP = new System.Windows.Forms.RadioButton();
            this.rbQAResults = new System.Windows.Forms.RadioButton();
            this.rbBerrieBe = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBerrieBe);
            this.groupBox1.Controls.Add(this.rbQAResults);
            this.groupBox1.Controls.Add(this.rbCutWIP);
            this.groupBox1.Controls.Add(this.rbCutProduction);
            this.groupBox1.Location = new System.Drawing.Point(115, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 253);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Reports";
            // 
            // rbCutProduction
            // 
            this.rbCutProduction.AutoSize = true;
            this.rbCutProduction.Location = new System.Drawing.Point(69, 44);
            this.rbCutProduction.Name = "rbCutProduction";
            this.rbCutProduction.Size = new System.Drawing.Size(95, 17);
            this.rbCutProduction.TabIndex = 0;
            this.rbCutProduction.TabStop = true;
            this.rbCutProduction.Text = "Cut Production";
            this.rbCutProduction.UseVisualStyleBackColor = true;
            this.rbCutProduction.CheckedChanged += new System.EventHandler(this.rbCutProduction_CheckedChanged);
            // 
            // rbCutWIP
            // 
            this.rbCutWIP.AutoSize = true;
            this.rbCutWIP.Location = new System.Drawing.Point(69, 90);
            this.rbCutWIP.Name = "rbCutWIP";
            this.rbCutWIP.Size = new System.Drawing.Size(82, 17);
            this.rbCutWIP.TabIndex = 1;
            this.rbCutWIP.TabStop = true;
            this.rbCutWIP.Text = "WIP Cutting";
            this.rbCutWIP.UseVisualStyleBackColor = true;
            this.rbCutWIP.CheckedChanged += new System.EventHandler(this.rbCutWIP_CheckedChanged);
            // 
            // rbQAResults
            // 
            this.rbQAResults.AutoSize = true;
            this.rbQAResults.Location = new System.Drawing.Point(69, 136);
            this.rbQAResults.Name = "rbQAResults";
            this.rbQAResults.Size = new System.Drawing.Size(78, 17);
            this.rbQAResults.TabIndex = 2;
            this.rbQAResults.TabStop = true;
            this.rbQAResults.Text = "QA Results";
            this.rbQAResults.UseVisualStyleBackColor = true;
            this.rbQAResults.CheckedChanged += new System.EventHandler(this.rbQAResults_CheckedChanged);
            // 
            // rbBerrieBe
            // 
            this.rbBerrieBe.AutoSize = true;
            this.rbBerrieBe.Location = new System.Drawing.Point(69, 182);
            this.rbBerrieBe.Name = "rbBerrieBe";
            this.rbBerrieBe.Size = new System.Drawing.Size(102, 17);
            this.rbBerrieBe.TabIndex = 3;
            this.rbBerrieBe.TabStop = true;
            this.rbBerrieBe.Text = "Berriebe Results";
            this.rbBerrieBe.UseVisualStyleBackColor = true;
            this.rbBerrieBe.CheckedChanged += new System.EventHandler(this.rbBerrieBe_CheckedChanged);
            // 
            // frmQACuttingSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 383);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQACuttingSel";
            this.Text = "QA Cutting Reports";
            this.Load += new System.EventHandler(this.frmQACuttingSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBerrieBe;
        private System.Windows.Forms.RadioButton rbQAResults;
        private System.Windows.Forms.RadioButton rbCutWIP;
        private System.Windows.Forms.RadioButton rbCutProduction;
    }
}