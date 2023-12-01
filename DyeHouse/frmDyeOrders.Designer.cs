namespace DyeHouse
{
    partial class frmDyeOrders
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDyeOrder = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboCustomerNo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDyeProductionLoss = new System.Windows.Forms.TextBox();
            this.txtWFDye = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboDyeOrders = new System.Windows.Forms.ComboBox();
            this.txtDyeOrder = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboFabricOrder = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(690, 554);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(569, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order Date";
            // 
            // dtpDyeOrder
            // 
            this.dtpDyeOrder.Location = new System.Drawing.Point(654, 8);
            this.dtpDyeOrder.Name = "dtpDyeOrder";
            this.dtpDyeOrder.Size = new System.Drawing.Size(136, 20);
            this.dtpDyeOrder.TabIndex = 3;
            this.dtpDyeOrder.ValueChanged += new System.EventHandler(this.dtpDyeOrder_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dye Order No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer No";
            // 
            // cmboCustomerNo
            // 
            this.cmboCustomerNo.FormattingEnabled = true;
            this.cmboCustomerNo.Location = new System.Drawing.Point(128, 58);
            this.cmboCustomerNo.Name = "cmboCustomerNo";
            this.cmboCustomerNo.Size = new System.Drawing.Size(179, 21);
            this.cmboCustomerNo.TabIndex = 6;
            this.cmboCustomerNo.SelectedIndexChanged += new System.EventHandler(this.cmboCustomerNo_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDyeProductionLoss);
            this.groupBox1.Controls.Add(this.txtWFDye);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(425, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 70);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Week to be finished by";
            // 
            // txtDyeProductionLoss
            // 
            this.txtDyeProductionLoss.Location = new System.Drawing.Point(210, 21);
            this.txtDyeProductionLoss.Name = "txtDyeProductionLoss";
            this.txtDyeProductionLoss.Size = new System.Drawing.Size(54, 20);
            this.txtDyeProductionLoss.TabIndex = 6;
            // 
            // txtWFDye
            // 
            this.txtWFDye.Location = new System.Drawing.Point(127, 21);
            this.txtWFDye.Name = "txtWFDye";
            this.txtWFDye.Size = new System.Drawing.Size(49, 20);
            this.txtWFDye.TabIndex = 3;
            this.txtWFDye.TextChanged += new System.EventHandler(this.txtWFDye_TextChanged);
            this.txtWFDye.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtWFDye_PreviewKeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Dye";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 26);
            this.label13.TabIndex = 18;
            this.label13.Text = "Customer Fabric Order\r\n(Purchase Order)\r\n";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(86, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(578, 191);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fabric Requirements";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(36, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(523, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // cmboDyeOrders
            // 
            this.cmboDyeOrders.Enabled = false;
            this.cmboDyeOrders.FormattingEnabled = true;
            this.cmboDyeOrders.Location = new System.Drawing.Point(425, 9);
            this.cmboDyeOrders.Name = "cmboDyeOrders";
            this.cmboDyeOrders.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOrders.TabIndex = 21;
            this.cmboDyeOrders.SelectedIndexChanged += new System.EventHandler(this.cmboDyeOrders_SelectedIndexChanged);
            // 
            // txtDyeOrder
            // 
            this.txtDyeOrder.Location = new System.Drawing.Point(128, 12);
            this.txtDyeOrder.Name = "txtDyeOrder";
            this.txtDyeOrder.ReadOnly = true;
            this.txtDyeOrder.Size = new System.Drawing.Size(100, 20);
            this.txtDyeOrder.TabIndex = 23;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(36, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(523, 150);
            this.dataGridView2.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(86, 356);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(578, 191);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trims Requirements";
            // 
            // cmboFabricOrder
            // 
            this.cmboFabricOrder.FormattingEnabled = true;
            this.cmboFabricOrder.Location = new System.Drawing.Point(147, 106);
            this.cmboFabricOrder.Name = "cmboFabricOrder";
            this.cmboFabricOrder.Size = new System.Drawing.Size(160, 21);
            this.cmboFabricOrder.TabIndex = 25;
            this.cmboFabricOrder.SelectedIndexChanged += new System.EventHandler(this.cmboFabricOrder_SelectedIndexChanged);
            // 
            // frmDyeOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 586);
            this.Controls.Add(this.cmboFabricOrder);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtDyeOrder);
            this.Controls.Add(this.cmboDyeOrders);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboCustomerNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDyeOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmDyeOrders";
            this.Text = "Dye Orders (Fabric)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDyeOrders_FormClosing);
            this.Load += new System.EventHandler(this.frmDyeOrders_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDyeOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboCustomerNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWFDye;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDyeProductionLoss;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboDyeOrders;
        private System.Windows.Forms.TextBox txtDyeOrder;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmboFabricOrder;
    }
}