namespace CustomerServices
{
    partial class frmSeleEMailAddress
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
            this.cmboEMailAddress = new CustomerServices.CheckComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmboEMailAddress
            // 
            this.cmboEMailAddress.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboEMailAddress.FormattingEnabled = true;
            this.cmboEMailAddress.Location = new System.Drawing.Point(191, 103);
            this.cmboEMailAddress.Name = "cmboEMailAddress";
            this.cmboEMailAddress.Size = new System.Drawing.Size(323, 21);
            this.cmboEMailAddress.TabIndex = 0;
            this.cmboEMailAddress.Text = "Select Options";
            this.cmboEMailAddress.SelectedIndexChanged += new System.EventHandler(this.cmboEMailAddress_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(581, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSeleEMailAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 387);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmboEMailAddress);
            this.Name = "frmSeleEMailAddress";
            this.Text = "Select EMail Address";
            this.Load += new System.EventHandler(this.frmSeleEMailAddress_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomerServices.CheckComboBox cmboEMailAddress;
        private System.Windows.Forms.Button btnClose;
    }
}