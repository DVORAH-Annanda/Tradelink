namespace TTI2_WF
{
    partial class frmQAKnittingSel
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
            this.rbCGrade = new System.Windows.Forms.RadioButton();
            this.rbGreigeEffic = new System.Windows.Forms.RadioButton();
            this.rbGriegeKnittedQa = new System.Windows.Forms.RadioButton();
            this.rbKnitOrdersProcess = new System.Windows.Forms.RadioButton();
            this.rbGreigeProduction = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGreigeProduction);
            this.groupBox1.Controls.Add(this.rbCGrade);
            this.groupBox1.Controls.Add(this.rbGreigeEffic);
            this.groupBox1.Controls.Add(this.rbGriegeKnittedQa);
            this.groupBox1.Controls.Add(this.rbKnitOrdersProcess);
            this.groupBox1.Location = new System.Drawing.Point(145, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Reports Available";
            // 
            // rbCGrade
            // 
            this.rbCGrade.AutoSize = true;
            this.rbCGrade.Location = new System.Drawing.Point(67, 206);
            this.rbCGrade.Name = "rbCGrade";
            this.rbCGrade.Size = new System.Drawing.Size(140, 17);
            this.rbCGrade.TabIndex = 3;
            this.rbCGrade.TabStop = true;
            this.rbCGrade.Text = "C Grade report by period";
            this.rbCGrade.UseVisualStyleBackColor = true;
            this.rbCGrade.CheckedChanged += new System.EventHandler(this.rbCGrade_CheckedChanged);
            // 
            // rbGreigeEffic
            // 
            this.rbGreigeEffic.AutoSize = true;
            this.rbGreigeEffic.Location = new System.Drawing.Point(67, 159);
            this.rbGreigeEffic.Name = "rbGreigeEffic";
            this.rbGreigeEffic.Size = new System.Drawing.Size(160, 17);
            this.rbGreigeEffic.TabIndex = 2;
            this.rbGreigeEffic.TabStop = true;
            this.rbGreigeEffic.Text = "Greige efficiency / Utilisation";
            this.rbGreigeEffic.UseVisualStyleBackColor = true;
            this.rbGreigeEffic.CheckedChanged += new System.EventHandler(this.rbGreigeEffic_CheckedChanged);
            // 
            // rbGriegeKnittedQa
            // 
            this.rbGriegeKnittedQa.AutoSize = true;
            this.rbGriegeKnittedQa.Location = new System.Drawing.Point(67, 112);
            this.rbGriegeKnittedQa.Name = "rbGriegeKnittedQa";
            this.rbGriegeKnittedQa.Size = new System.Drawing.Size(133, 17);
            this.rbGriegeKnittedQa.TabIndex = 1;
            this.rbGriegeKnittedQa.TabStop = true;
            this.rbGriegeKnittedQa.Text = "Knit Orders QA Results";
            this.rbGriegeKnittedQa.UseVisualStyleBackColor = true;
            this.rbGriegeKnittedQa.CheckedChanged += new System.EventHandler(this.rbGriegeKnittedQa_CheckedChanged);
            // 
            // rbKnitOrdersProcess
            // 
            this.rbKnitOrdersProcess.AutoSize = true;
            this.rbKnitOrdersProcess.Location = new System.Drawing.Point(67, 65);
            this.rbKnitOrdersProcess.Name = "rbKnitOrdersProcess";
            this.rbKnitOrdersProcess.Size = new System.Drawing.Size(143, 17);
            this.rbKnitOrdersProcess.TabIndex = 0;
            this.rbKnitOrdersProcess.TabStop = true;
            this.rbKnitOrdersProcess.Text = "Knit Orders Process Loss";
            this.rbKnitOrdersProcess.UseVisualStyleBackColor = true;
            this.rbKnitOrdersProcess.CheckedChanged += new System.EventHandler(this.rbKnitOrdersProcess_CheckedChanged);
            // 
            // rbGreigeProduction
            // 
            this.rbGreigeProduction.AutoSize = true;
            this.rbGreigeProduction.Location = new System.Drawing.Point(67, 253);
            this.rbGreigeProduction.Name = "rbGreigeProduction";
            this.rbGreigeProduction.Size = new System.Drawing.Size(110, 17);
            this.rbGreigeProduction.TabIndex = 4;
            this.rbGreigeProduction.TabStop = true;
            this.rbGreigeProduction.Text = "Greige Production";
            this.rbGreigeProduction.UseVisualStyleBackColor = true;
            this.rbGreigeProduction.CheckedChanged += new System.EventHandler(this.rbGreigeProduction_CheckedChanged);
            // 
            // frmQAKnittingSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 483);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQAKnittingSel";
            this.Text = "QA Knitting Selection";
            this.Load += new System.EventHandler(this.frmQAKnittingSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCGrade;
        private System.Windows.Forms.RadioButton rbGreigeEffic;
        private System.Windows.Forms.RadioButton rbGriegeKnittedQa;
        private System.Windows.Forms.RadioButton rbKnitOrdersProcess;
        private System.Windows.Forms.RadioButton rbGreigeProduction;
    }
}