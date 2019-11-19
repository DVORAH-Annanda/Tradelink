namespace DyeHouse
{
    partial class frmProductionPlanDC
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboDyeBatches = new DyeHouse.CheckComboBox(); // System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(575, 285);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Pending Dye Batches";
            // 
            // cmboDyeBatches
            // 
            this.cmboDyeBatches.FormattingEnabled = true;
            this.cmboDyeBatches.Location = new System.Drawing.Point(292, 92);
            this.cmboDyeBatches.Name = "cmboDyeBatches";
            this.cmboDyeBatches.Size = new System.Drawing.Size(197, 21);
            this.cmboDyeBatches.TabIndex = 7;
            // 
            // frmProductionPlanDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 352);
            this.Controls.Add(this.cmboDyeBatches);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProcess);
            this.Name = "frmProductionPlanDC";
            this.Text = "Production Planning Dyes and Chemicals";
            this.Load += new System.EventHandler(this.frmProductionPlanDC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // private System.Windows.Forms.ComboBox cmboCustomers;
        // private System.Windows.Forms.ComboBox cmboSizes;
        // private System.Windows.Forms.ComboBox cmboColours;
        //private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label1;
        private /*System.Windows.Forms.ComboBox*/ DyeHouse.CheckComboBox cmboDyeBatches;
    }
}