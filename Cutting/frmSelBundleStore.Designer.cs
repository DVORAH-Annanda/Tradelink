namespace Cutting
{
    partial class frmSelBundleStore
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
            this.label1 = new System.Windows.Forms.Label();
            // this.cmboCutSheets = new System.Windows.Forms.ComboBox();
            this.cmboCutSheets = new Cutting.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Cutsheets";
            // 
            // cmboCutSheets
            // 
            this.cmboCutSheets.FormattingEnabled = true;
            this.cmboCutSheets.Location = new System.Drawing.Point(260, 68);
            this.cmboCutSheets.Name = "cmboCutSheets";
            this.cmboCutSheets.Size = new System.Drawing.Size(121, 21);
            this.cmboCutSheets.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(487, 331);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmSelBundleStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 388);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboCutSheets);
            this.Controls.Add(this.label1);
            this.Name = "frmSelBundleStore";
            this.Text = "Bundles in Bundle Store";
            this.Load += new System.EventHandler(this.frmSelBundleStore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Cutting.CheckComboBox cmboCutSheets;
       // private System.Windows.Forms.ComboBox cmboCutSheets;
        private System.Windows.Forms.Button btnSubmit;
    }
}