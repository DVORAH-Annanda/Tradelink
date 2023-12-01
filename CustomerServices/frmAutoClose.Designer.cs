namespace CustomerServices
{
    partial class frmAutoClose
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
            this.cmboCustomeres = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboPurchaseOrders = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer ";
            // 
            // cmboCustomeres
            // 
            this.cmboCustomeres.FormattingEnabled = true;
            this.cmboCustomeres.Location = new System.Drawing.Point(423, 59);
            this.cmboCustomeres.Name = "cmboCustomeres";
            this.cmboCustomeres.Size = new System.Drawing.Size(229, 21);
            this.cmboCustomeres.TabIndex = 1;
            this.cmboCustomeres.SelectedIndexChanged += new System.EventHandler(this.cmboCustomeres_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(811, 396);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer Purchase Orders";
            // 
            // cmboPurchaseOrders
            // 
            this.cmboPurchaseOrders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboPurchaseOrders.FormattingEnabled = true;
            this.cmboPurchaseOrders.Location = new System.Drawing.Point(423, 114);
            this.cmboPurchaseOrders.Name = "cmboPurchaseOrders";
            this.cmboPurchaseOrders.Size = new System.Drawing.Size(229, 21);
            this.cmboPurchaseOrders.TabIndex = 4;
            this.cmboPurchaseOrders.Text = "Select Options";
            // 
            // frrmAutoClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 450);
            this.Controls.Add(this.cmboPurchaseOrders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboCustomeres);
            this.Controls.Add(this.label1);
            this.Name = "frrmAutoClose";
            this.Text = "AutoClose Customer Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frrmAutoClose_FormClosing);
            this.Load += new System.EventHandler(this.frrmAutoClose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCustomeres;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        //private System.Windows.Forms.ComboBox cmboPurchaseOrders;
        private CustomerServices.CheckComboBox cmboPurchaseOrders;
    }
}