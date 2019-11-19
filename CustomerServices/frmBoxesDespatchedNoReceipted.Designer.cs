namespace CustomerServices
{
    partial class frmBoxesDespatchedNoReceipted
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboWarehouses = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Destination Warehouse";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(423, 315);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(93, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboWarehouses
            // 
            this.cmboWarehouses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboWarehouses.FormattingEnabled = true;
            this.cmboWarehouses.Location = new System.Drawing.Point(196, 49);
            this.cmboWarehouses.Name = "cmboWarehouses";
            this.cmboWarehouses.Size = new System.Drawing.Size(240, 21);
            this.cmboWarehouses.TabIndex = 1;
            this.cmboWarehouses.Text = "Select Options";
            this.cmboWarehouses.SelectedIndexChanged += new System.EventHandler(this.cmboWarehouses_SelectedIndexChanged);
            // 
            // frmBoxesDespatchedNoReceipted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 367);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboWarehouses);
            this.Controls.Add(this.label1);
            this.Name = "frmBoxesDespatchedNoReceipted";
            this.Text = "Boxes Despatched to Warehouse as yet not receipted";
            this.Load += new System.EventHandler(this.frmBoxesDespatchedNoReceipted_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ComboBox cmboWarehouses;
        private CustomerServices.CheckComboBox cmboWarehouses;
        private System.Windows.Forms.Button btnSubmit;
    }
}