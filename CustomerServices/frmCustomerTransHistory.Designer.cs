namespace CustomerServices
{
    partial class frmCustomerTransHistory
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboPurchaseOrders = new CustomerServices.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPurchaseOrder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Customer Purchase Orders";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(250, 67);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(173, 21);
            this.cmboCustomers.TabIndex = 2;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(482, 384);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboPurchaseOrders
            // 
            this.cmboPurchaseOrders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboPurchaseOrders.FormattingEnabled = true;
            this.cmboPurchaseOrders.Location = new System.Drawing.Point(250, 132);
            this.cmboPurchaseOrders.Name = "cmboPurchaseOrders";
            this.cmboPurchaseOrders.Size = new System.Drawing.Size(173, 21);
            this.cmboPurchaseOrders.TabIndex = 4;
            this.cmboPurchaseOrders.Text = "Select Options";
            this.cmboPurchaseOrders.SelectedIndexChanged += new System.EventHandler(this.cmboPurchaseOrders_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Please enter a Purchase Order No";
            // 
            // txtPurchaseOrder
            // 
            this.txtPurchaseOrder.Location = new System.Drawing.Point(262, 338);
            this.txtPurchaseOrder.Name = "txtPurchaseOrder";
            this.txtPurchaseOrder.Size = new System.Drawing.Size(176, 20);
            this.txtPurchaseOrder.TabIndex = 7;
            // 
            // frmCustomerTransHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 436);
            this.Controls.Add(this.txtPurchaseOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboPurchaseOrders);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCustomerTransHistory";
            this.Text = "Customer Transaction History";
            this.Load += new System.EventHandler(this.frmCustomerTransHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.Button btnSubmit;
        private CustomerServices.CheckComboBox cmboPurchaseOrders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPurchaseOrder;
    }
}