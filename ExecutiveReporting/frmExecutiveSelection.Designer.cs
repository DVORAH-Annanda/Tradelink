namespace ExecutiveReporting
{
    partial class frmExecutiveSelection
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
            this.chkProduction = new System.Windows.Forms.CheckBox();
            this.chkCapacityUtil = new System.Windows.Forms.CheckBox();
            this.chkProductionPerformance = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkProduction
            // 
            this.chkProduction.AutoSize = true;
            this.chkProduction.Location = new System.Drawing.Point(160, 81);
            this.chkProduction.Name = "chkProduction";
            this.chkProduction.Size = new System.Drawing.Size(107, 17);
            this.chkProduction.TabIndex = 0;
            this.chkProduction.Text = "Production Portal";
            this.chkProduction.UseVisualStyleBackColor = true;
            this.chkProduction.CheckedChanged += new System.EventHandler(this.chkProduction_CheckedChanged);
            // 
            // chkCapacityUtil
            // 
            this.chkCapacityUtil.AutoSize = true;
            this.chkCapacityUtil.Location = new System.Drawing.Point(160, 144);
            this.chkCapacityUtil.Name = "chkCapacityUtil";
            this.chkCapacityUtil.Size = new System.Drawing.Size(145, 17);
            this.chkCapacityUtil.TabIndex = 1;
            this.chkCapacityUtil.Text = "Capacity Utilisation Portal";
            this.chkCapacityUtil.UseVisualStyleBackColor = true;
            // 
            // chkProductionPerformance
            // 
            this.chkProductionPerformance.AutoSize = true;
            this.chkProductionPerformance.Location = new System.Drawing.Point(160, 201);
            this.chkProductionPerformance.Name = "chkProductionPerformance";
            this.chkProductionPerformance.Size = new System.Drawing.Size(170, 17);
            this.chkProductionPerformance.TabIndex = 2;
            this.chkProductionPerformance.Text = "Production Performance Portal";
            this.chkProductionPerformance.UseVisualStyleBackColor = true;
            // 
            // frmExecutiveSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 504);
            this.Controls.Add(this.chkProductionPerformance);
            this.Controls.Add(this.chkCapacityUtil);
            this.Controls.Add(this.chkProduction);
            this.Name = "frmExecutiveSelection";
            this.Text = "Executive Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkProduction;
        private System.Windows.Forms.CheckBox chkCapacityUtil;
        private System.Windows.Forms.CheckBox chkProductionPerformance;
    }
}