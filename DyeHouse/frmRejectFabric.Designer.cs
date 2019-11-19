namespace DyeHouse
{
    partial class frmRejectFabric
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmboBatchNumber = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBatchFabricKg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBatchGreigeKg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCustomerDetails = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.txtDyeOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGrnNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(104, 359);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(431, 180);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(563, 549);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmboBatchNumber
            // 
            this.cmboBatchNumber.FormattingEnabled = true;
            this.cmboBatchNumber.Location = new System.Drawing.Point(280, 79);
            this.cmboBatchNumber.Name = "cmboBatchNumber";
            this.cmboBatchNumber.Size = new System.Drawing.Size(142, 21);
            this.cmboBatchNumber.TabIndex = 18;
            this.cmboBatchNumber.SelectedIndexChanged += new System.EventHandler(this.cmboBatchNumber_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Batch Number";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(280, 32);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(124, 20);
            this.dtpTransDate.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Date of this transaction";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBatchFabricKg);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBatchGreigeKg);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCustomerDetails);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.txtDyeOrder);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(56, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 164);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Static Info";
            // 
            // txtBatchFabricKg
            // 
            this.txtBatchFabricKg.Location = new System.Drawing.Point(390, 107);
            this.txtBatchFabricKg.Name = "txtBatchFabricKg";
            this.txtBatchFabricKg.ReadOnly = true;
            this.txtBatchFabricKg.Size = new System.Drawing.Size(100, 20);
            this.txtBatchFabricKg.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Batch Fabric Kg";
            // 
            // txtBatchGreigeKg
            // 
            this.txtBatchGreigeKg.Location = new System.Drawing.Point(145, 106);
            this.txtBatchGreigeKg.Name = "txtBatchGreigeKg";
            this.txtBatchGreigeKg.ReadOnly = true;
            this.txtBatchGreigeKg.Size = new System.Drawing.Size(100, 20);
            this.txtBatchGreigeKg.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Batch Greige Kg";
            // 
            // txtCustomerDetails
            // 
            this.txtCustomerDetails.Location = new System.Drawing.Point(109, 67);
            this.txtCustomerDetails.Name = "txtCustomerDetails";
            this.txtCustomerDetails.ReadOnly = true;
            this.txtCustomerDetails.Size = new System.Drawing.Size(232, 20);
            this.txtCustomerDetails.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Customer";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(390, 32);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(100, 20);
            this.txtColour.TabIndex = 3;
            // 
            // txtDyeOrder
            // 
            this.txtDyeOrder.Location = new System.Drawing.Point(109, 28);
            this.txtDyeOrder.Name = "txtDyeOrder";
            this.txtDyeOrder.ReadOnly = true;
            this.txtDyeOrder.Size = new System.Drawing.Size(100, 20);
            this.txtDyeOrder.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Colour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dye Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Transaction Number";
            // 
            // txtGrnNumber
            // 
            this.txtGrnNumber.Location = new System.Drawing.Point(280, 123);
            this.txtGrnNumber.Name = "txtGrnNumber";
            this.txtGrnNumber.ReadOnly = true;
            this.txtGrnNumber.Size = new System.Drawing.Size(117, 20);
            this.txtGrnNumber.TabIndex = 22;
            // 
            // frmRejectFabric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 584);
            this.Controls.Add(this.txtGrnNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboBatchNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmRejectFabric";
            this.Text = "Reject Fabric";
            this.Load += new System.EventHandler(this.frmRejectFabric_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboBatchNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBatchFabricKg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBatchGreigeKg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCustomerDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtDyeOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGrnNumber;
    }
}