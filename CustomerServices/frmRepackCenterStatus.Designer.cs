namespace CustomerServices
{
    partial class frmRepackCenterStatus
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboRepackConfig = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(427, 298);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Configuration Plans";
            // 
            // cmboRepackConfig
            // 
            this.cmboRepackConfig.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboRepackConfig.FormattingEnabled = true;
            this.cmboRepackConfig.Location = new System.Drawing.Point(198, 54);
            this.cmboRepackConfig.Name = "cmboRepackConfig";
            this.cmboRepackConfig.Size = new System.Drawing.Size(184, 21);
            this.cmboRepackConfig.TabIndex = 2;
            this.cmboRepackConfig.Text = "Select Options";
            this.cmboRepackConfig.SelectedIndexChanged += new System.EventHandler(this.cmboRepackConfig_SelectedIndexChanged);
            // 
            // frmRepackCenterStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 333);
            this.Controls.Add(this.cmboRepackConfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmRepackCenterStatus";
            this.Text = "RePack Center Status Report";
            this.Load += new System.EventHandler(this.frmRepackCenterStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private CustomerServices.CheckComboBox cmboRepackConfig;
    }
}