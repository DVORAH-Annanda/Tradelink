namespace TTI2_WF
{
    partial class frmExecSel
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
            this.rbCommercial = new System.Windows.Forms.RadioButton();
            this.rbStockOnHand = new System.Windows.Forms.RadioButton();
            this.rbProdPerf = new System.Windows.Forms.RadioButton();
            this.rbCapUtil = new System.Windows.Forms.RadioButton();
            this.rbProduction = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCommercial);
            this.groupBox1.Controls.Add(this.rbStockOnHand);
            this.groupBox1.Controls.Add(this.rbProdPerf);
            this.groupBox1.Controls.Add(this.rbCapUtil);
            this.groupBox1.Controls.Add(this.rbProduction);
            this.groupBox1.Location = new System.Drawing.Point(88, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 413);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbCommercial
            // 
            this.rbCommercial.AutoSize = true;
            this.rbCommercial.Location = new System.Drawing.Point(120, 352);
            this.rbCommercial.Name = "rbCommercial";
            this.rbCommercial.Size = new System.Drawing.Size(117, 17);
            this.rbCommercial.TabIndex = 4;
            this.rbCommercial.TabStop = true;
            this.rbCommercial.Text = "Commercial Results";
            this.rbCommercial.UseVisualStyleBackColor = true;
            this.rbCommercial.CheckedChanged += new System.EventHandler(this.rbCommercial_CheckedChanged);
            // 
            // rbStockOnHand
            // 
            this.rbStockOnHand.AutoSize = true;
            this.rbStockOnHand.Location = new System.Drawing.Point(120, 279);
            this.rbStockOnHand.Name = "rbStockOnHand";
            this.rbStockOnHand.Size = new System.Drawing.Size(99, 17);
            this.rbStockOnHand.TabIndex = 3;
            this.rbStockOnHand.TabStop = true;
            this.rbStockOnHand.Text = "Stock On Hand";
            this.rbStockOnHand.UseVisualStyleBackColor = true;
            this.rbStockOnHand.CheckedChanged += new System.EventHandler(this.rbStockOnHand_CheckedChanged);
            // 
            // rbProdPerf
            // 
            this.rbProdPerf.AutoSize = true;
            this.rbProdPerf.Location = new System.Drawing.Point(120, 206);
            this.rbProdPerf.Name = "rbProdPerf";
            this.rbProdPerf.Size = new System.Drawing.Size(99, 17);
            this.rbProdPerf.TabIndex = 2;
            this.rbProdPerf.TabStop = true;
            this.rbProdPerf.Text = "Process Losses";
            this.rbProdPerf.UseVisualStyleBackColor = true;
            this.rbProdPerf.CheckedChanged += new System.EventHandler(this.rbProdPerf_CheckedChanged);
            // 
            // rbCapUtil
            // 
            this.rbCapUtil.AutoSize = true;
            this.rbCapUtil.Location = new System.Drawing.Point(120, 133);
            this.rbCapUtil.Name = "rbCapUtil";
            this.rbCapUtil.Size = new System.Drawing.Size(112, 17);
            this.rbCapUtil.TabIndex = 1;
            this.rbCapUtil.TabStop = true;
            this.rbCapUtil.Text = "Capacity Utilsation";
            this.rbCapUtil.UseVisualStyleBackColor = true;
            this.rbCapUtil.CheckedChanged += new System.EventHandler(this.rbCapUtil_CheckedChanged);
            // 
            // rbProduction
            // 
            this.rbProduction.AutoSize = true;
            this.rbProduction.Location = new System.Drawing.Point(120, 60);
            this.rbProduction.Name = "rbProduction";
            this.rbProduction.Size = new System.Drawing.Size(76, 17);
            this.rbProduction.TabIndex = 0;
            this.rbProduction.TabStop = true;
            this.rbProduction.Text = "Production";
            this.rbProduction.UseVisualStyleBackColor = true;
            this.rbProduction.CheckedChanged += new System.EventHandler(this.rbProduction_CheckedChanged);
            // 
            // frmExecSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 510);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmExecSel";
            this.Text = "Executive Reporting Selection";
            this.Load += new System.EventHandler(this.frmExecSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCommercial;
        private System.Windows.Forms.RadioButton rbStockOnHand;
        private System.Windows.Forms.RadioButton rbProdPerf;
        private System.Windows.Forms.RadioButton rbCapUtil;
        private System.Windows.Forms.RadioButton rbProduction;
    }
}