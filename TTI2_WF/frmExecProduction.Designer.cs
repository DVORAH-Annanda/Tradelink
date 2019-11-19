namespace TTI2_WF
{
    partial class frmExecProduction
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
            this.rbCmtProduction = new System.Windows.Forms.RadioButton();
            this.rbCutProduction = new System.Windows.Forms.RadioButton();
            this.rbFabricDyed = new System.Windows.Forms.RadioButton();
            this.rbKnitingGreige = new System.Windows.Forms.RadioButton();
            this.rbProdSpinning = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCmtProduction);
            this.groupBox1.Controls.Add(this.rbCutProduction);
            this.groupBox1.Controls.Add(this.rbFabricDyed);
            this.groupBox1.Controls.Add(this.rbKnitingGreige);
            this.groupBox1.Controls.Add(this.rbProdSpinning);
            this.groupBox1.Location = new System.Drawing.Point(86, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 359);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbCmtProduction
            // 
            this.rbCmtProduction.AutoSize = true;
            this.rbCmtProduction.Location = new System.Drawing.Point(107, 298);
            this.rbCmtProduction.Name = "rbCmtProduction";
            this.rbCmtProduction.Size = new System.Drawing.Size(102, 17);
            this.rbCmtProduction.TabIndex = 4;
            this.rbCmtProduction.TabStop = true;
            this.rbCmtProduction.Text = "CMT Production";
            this.rbCmtProduction.UseVisualStyleBackColor = true;
            this.rbCmtProduction.CheckedChanged += new System.EventHandler(this.rbCmtProduction_CheckedChanged);
            // 
            // rbCutProduction
            // 
            this.rbCutProduction.AutoSize = true;
            this.rbCutProduction.Location = new System.Drawing.Point(107, 236);
            this.rbCutProduction.Name = "rbCutProduction";
            this.rbCutProduction.Size = new System.Drawing.Size(112, 17);
            this.rbCutProduction.TabIndex = 3;
            this.rbCutProduction.TabStop = true;
            this.rbCutProduction.Text = "Cutting Production";
            this.rbCutProduction.UseVisualStyleBackColor = true;
            this.rbCutProduction.CheckedChanged += new System.EventHandler(this.rbCutProduction_CheckedChanged);
            // 
            // rbFabricDyed
            // 
            this.rbFabricDyed.AutoSize = true;
            this.rbFabricDyed.Location = new System.Drawing.Point(107, 174);
            this.rbFabricDyed.Name = "rbFabricDyed";
            this.rbFabricDyed.Size = new System.Drawing.Size(82, 17);
            this.rbFabricDyed.TabIndex = 2;
            this.rbFabricDyed.TabStop = true;
            this.rbFabricDyed.Text = "Fabric Dyed";
            this.rbFabricDyed.UseVisualStyleBackColor = true;
            this.rbFabricDyed.CheckedChanged += new System.EventHandler(this.rbFabricDyed_CheckedChanged);
            // 
            // rbKnitingGreige
            // 
            this.rbKnitingGreige.AutoSize = true;
            this.rbKnitingGreige.Location = new System.Drawing.Point(107, 112);
            this.rbKnitingGreige.Name = "rbKnitingGreige";
            this.rbKnitingGreige.Size = new System.Drawing.Size(143, 17);
            this.rbKnitingGreige.TabIndex = 1;
            this.rbKnitingGreige.TabStop = true;
            this.rbKnitingGreige.Text = "Knitting Greige Produced";
            this.rbKnitingGreige.UseVisualStyleBackColor = true;
            this.rbKnitingGreige.CheckedChanged += new System.EventHandler(this.rbKnitingGreige_CheckedChanged);
            // 
            // rbProdSpinning
            // 
            this.rbProdSpinning.AutoSize = true;
            this.rbProdSpinning.Location = new System.Drawing.Point(107, 50);
            this.rbProdSpinning.Name = "rbProdSpinning";
            this.rbProdSpinning.Size = new System.Drawing.Size(140, 17);
            this.rbProdSpinning.TabIndex = 0;
            this.rbProdSpinning.TabStop = true;
            this.rbProdSpinning.Text = "Spinning Yarn Produced";
            this.rbProdSpinning.UseVisualStyleBackColor = true;
            this.rbProdSpinning.CheckedChanged += new System.EventHandler(this.rbProdSpinning_CheckedChanged);
            // 
            // frmExecProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 441);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmExecProduction";
            this.Text = "Executive Production Reporting";
            this.Load += new System.EventHandler(this.frmExecProduction_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCmtProduction;
        private System.Windows.Forms.RadioButton rbCutProduction;
        private System.Windows.Forms.RadioButton rbFabricDyed;
        private System.Windows.Forms.RadioButton rbKnitingGreige;
        private System.Windows.Forms.RadioButton rbProdSpinning;
    }
}