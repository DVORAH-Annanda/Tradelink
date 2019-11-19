namespace TTI2_WF
{
    partial class frmExecProdPerf
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
            this.rbPPCMT = new System.Windows.Forms.RadioButton();
            this.rbPPCutting = new System.Windows.Forms.RadioButton();
            this.rbPPDyeing = new System.Windows.Forms.RadioButton();
            this.rbPPKnitting = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPPCMT);
            this.groupBox1.Controls.Add(this.rbPPCutting);
            this.groupBox1.Controls.Add(this.rbPPDyeing);
            this.groupBox1.Controls.Add(this.rbPPKnitting);
            this.groupBox1.Location = new System.Drawing.Point(107, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbPPCMT
            // 
            this.rbPPCMT.AutoSize = true;
            this.rbPPCMT.Location = new System.Drawing.Point(108, 221);
            this.rbPPCMT.Name = "rbPPCMT";
            this.rbPPCMT.Size = new System.Drawing.Size(214, 17);
            this.rbPPCMT.TabIndex = 3;
            this.rbPPCMT.TabStop = true;
            this.rbPPCMT.Text = "CMT Production Report B Grade Report";
            this.rbPPCMT.UseVisualStyleBackColor = true;
            this.rbPPCMT.CheckedChanged += new System.EventHandler(this.rbPPCMT_CheckedChanged);
            // 
            // rbPPCutting
            // 
            this.rbPPCutting.AutoSize = true;
            this.rbPPCutting.Location = new System.Drawing.Point(108, 159);
            this.rbPPCutting.Name = "rbPPCutting";
            this.rbPPCutting.Size = new System.Drawing.Size(58, 17);
            this.rbPPCutting.TabIndex = 2;
            this.rbPPCutting.TabStop = true;
            this.rbPPCutting.Text = "Cutting";
            this.rbPPCutting.UseVisualStyleBackColor = true;
            this.rbPPCutting.CheckedChanged += new System.EventHandler(this.rbPPCutting_CheckedChanged);
            // 
            // rbPPDyeing
            // 
            this.rbPPDyeing.AutoSize = true;
            this.rbPPDyeing.Location = new System.Drawing.Point(108, 97);
            this.rbPPDyeing.Name = "rbPPDyeing";
            this.rbPPDyeing.Size = new System.Drawing.Size(58, 17);
            this.rbPPDyeing.TabIndex = 1;
            this.rbPPDyeing.TabStop = true;
            this.rbPPDyeing.Text = "Dyeing";
            this.rbPPDyeing.UseVisualStyleBackColor = true;
            this.rbPPDyeing.CheckedChanged += new System.EventHandler(this.rbPPDyeing_CheckedChanged);
            // 
            // rbPPKnitting
            // 
            this.rbPPKnitting.AutoSize = true;
            this.rbPPKnitting.Location = new System.Drawing.Point(108, 35);
            this.rbPPKnitting.Name = "rbPPKnitting";
            this.rbPPKnitting.Size = new System.Drawing.Size(60, 17);
            this.rbPPKnitting.TabIndex = 0;
            this.rbPPKnitting.TabStop = true;
            this.rbPPKnitting.Text = "Knitting";
            this.rbPPKnitting.UseVisualStyleBackColor = true;
            this.rbPPKnitting.CheckedChanged += new System.EventHandler(this.rbPPKnitting_CheckedChanged);
            // 
            // frmExecProdPerf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 422);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmExecProdPerf";
            this.Text = "Executive Production Performance";
            this.Load += new System.EventHandler(this.frmProdPerformance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPPCMT;
        private System.Windows.Forms.RadioButton rbPPCutting;
        private System.Windows.Forms.RadioButton rbPPDyeing;
        private System.Windows.Forms.RadioButton rbPPKnitting;
    }
}