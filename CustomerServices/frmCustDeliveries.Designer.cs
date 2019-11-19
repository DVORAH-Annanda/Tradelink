namespace CustomerServices
{
    partial class frmCustDeliveries
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeliveryNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.chkPLStockOrders = new System.Windows.Forms.CheckBox();
            this.cmboTransporters = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customers";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(41, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(604, 236);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(593, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Delivery Note Number";
            // 
            // txtDeliveryNo
            // 
            this.txtDeliveryNo.Location = new System.Drawing.Point(254, 107);
            this.txtDeliveryNo.Name = "txtDeliveryNo";
            this.txtDeliveryNo.ReadOnly = true;
            this.txtDeliveryNo.Size = new System.Drawing.Size(169, 20);
            this.txtDeliveryNo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transporter";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(254, 62);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(221, 21);
            this.cmboCustomers.TabIndex = 8;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // chkPLStockOrders
            // 
            this.chkPLStockOrders.AutoSize = true;
            this.chkPLStockOrders.Location = new System.Drawing.Point(254, 22);
            this.chkPLStockOrders.Name = "chkPLStockOrders";
            this.chkPLStockOrders.Size = new System.Drawing.Size(183, 17);
            this.chkPLStockOrders.TabIndex = 9;
            this.chkPLStockOrders.Text = "Include Picking List Stock Orders";
            this.chkPLStockOrders.UseVisualStyleBackColor = true;
            // 
            // cmboTransporters
            // 
            this.cmboTransporters.FormattingEnabled = true;
            this.cmboTransporters.Location = new System.Drawing.Point(254, 144);
            this.cmboTransporters.Name = "cmboTransporters";
            this.cmboTransporters.Size = new System.Drawing.Size(221, 21);
            this.cmboTransporters.TabIndex = 10;
            // 
            // frmCustDeliveries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 500);
            this.Controls.Add(this.cmboTransporters);
            this.Controls.Add(this.chkPLStockOrders);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeliveryNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "frmCustDeliveries";
            this.Text = "Deliveries To Customers";
            this.Load += new System.EventHandler(this.frmCustDeliveries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.CheckBox chkPLStockOrders;
        private System.Windows.Forms.ComboBox cmboTransporters;
    }
}