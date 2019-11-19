namespace CustomerServices
{
    partial class frmPOSearch
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
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOpenOrders = new System.Windows.Forms.RadioButton();
            this.rbClosedOrders = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboPurchaseOrder = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Number";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(289, 157);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(222, 21);
            this.cmboCustomers.TabIndex = 1;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbClosedOrders);
            this.groupBox1.Controls.Add(this.rbOpenOrders);
            this.groupBox1.Location = new System.Drawing.Point(289, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbOpenOrders
            // 
            this.rbOpenOrders.AutoSize = true;
            this.rbOpenOrders.Checked = true;
            this.rbOpenOrders.Location = new System.Drawing.Point(39, 29);
            this.rbOpenOrders.Name = "rbOpenOrders";
            this.rbOpenOrders.Size = new System.Drawing.Size(85, 17);
            this.rbOpenOrders.TabIndex = 0;
            this.rbOpenOrders.TabStop = true;
            this.rbOpenOrders.Text = "Open Orders";
            this.rbOpenOrders.UseVisualStyleBackColor = true;
            // 
            // rbClosedOrders
            // 
            this.rbClosedOrders.AutoSize = true;
            this.rbClosedOrders.Location = new System.Drawing.Point(39, 65);
            this.rbClosedOrders.Name = "rbClosedOrders";
            this.rbClosedOrders.Size = new System.Drawing.Size(91, 17);
            this.rbClosedOrders.TabIndex = 1;
            this.rbClosedOrders.Text = "Closed Orders";
            this.rbClosedOrders.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Purchase Order";
            // 
            // cmboPurchaseOrder
            // 
            this.cmboPurchaseOrder.FormattingEnabled = true;
            this.cmboPurchaseOrder.Location = new System.Drawing.Point(289, 236);
            this.cmboPurchaseOrder.Name = "cmboPurchaseOrder";
            this.cmboPurchaseOrder.Size = new System.Drawing.Size(222, 21);
            this.cmboPurchaseOrder.TabIndex = 4;
            this.cmboPurchaseOrder.SelectedIndexChanged += new System.EventHandler(this.cmboPurchaseOrder_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 279);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(717, 267);
            this.dataGridView1.TabIndex = 5;
            // 
            // frmPOSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 558);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboPurchaseOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label1);
            this.Name = "frmPOSearch";
            this.Text = "Search By Purchase Order";
            this.Load += new System.EventHandler(this.frmPOSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbClosedOrders;
        private System.Windows.Forms.RadioButton rbOpenOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboPurchaseOrder;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}