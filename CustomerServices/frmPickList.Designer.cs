namespace CustomerServices
{
    partial class frmPickList
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
            this.cmboCurrentOrders = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStock = new System.Windows.Forms.CheckBox();
            this.cmboWareHouses = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnViewPickList = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmboCurrentOrders
            // 
            this.cmboCurrentOrders.FormattingEnabled = true;
            this.cmboCurrentOrders.Location = new System.Drawing.Point(159, 53);
            this.cmboCurrentOrders.Name = "cmboCurrentOrders";
            this.cmboCurrentOrders.Size = new System.Drawing.Size(199, 21);
            this.cmboCurrentOrders.TabIndex = 0;
            this.cmboCurrentOrders.Text = "`";
            this.cmboCurrentOrders.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentOrders_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Orders";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStock);
            this.groupBox1.Controls.Add(this.cmboWareHouses);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmboCustomers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmboCurrentOrders);
            this.groupBox1.Location = new System.Drawing.Point(51, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 151);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "`";
            // 
            // chkStock
            // 
            this.chkStock.AutoSize = true;
            this.chkStock.Location = new System.Drawing.Point(159, 124);
            this.chkStock.Name = "chkStock";
            this.chkStock.Size = new System.Drawing.Size(140, 17);
            this.chkStock.TabIndex = 12;
            this.chkStock.Text = "Stock Order Picking List";
            this.chkStock.UseVisualStyleBackColor = true;
            // 
            // cmboWareHouses
            // 
            this.cmboWareHouses.FormattingEnabled = true;
            this.cmboWareHouses.Location = new System.Drawing.Point(159, 88);
            this.cmboWareHouses.Name = "cmboWareHouses";
            this.cmboWareHouses.Size = new System.Drawing.Size(199, 21);
            this.cmboWareHouses.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Warehouse";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(159, 18);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(199, 21);
            this.cmboCustomers.TabIndex = 9;
            this.cmboCustomers.Text = "`";
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Customers";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(42, 381);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(729, 258);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(679, 706);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(42, 187);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(729, 168);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // btnViewPickList
            // 
            this.btnViewPickList.Location = new System.Drawing.Point(679, 654);
            this.btnViewPickList.Name = "btnViewPickList";
            this.btnViewPickList.Size = new System.Drawing.Size(92, 35);
            this.btnViewPickList.TabIndex = 6;
            this.btnViewPickList.Text = "View PickList";
            this.btnViewPickList.UseVisualStyleBackColor = true;
            this.btnViewPickList.Click += new System.EventHandler(this.btnViewPickList_Click);
            // 
            // frmPickList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 749);
            this.Controls.Add(this.btnViewPickList);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPickList";
            this.Text = "Allocation of Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPickList_FormClosing);
            this.Load += new System.EventHandler(this.frmPickList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboCurrentOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewPickList;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ComboBox cmboWareHouses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkStock;
    }
}