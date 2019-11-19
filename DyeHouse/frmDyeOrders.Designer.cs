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
            this.cmboFabric = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtYieldFactor = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmboColors = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboDyeOrders = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDyeOrder = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(715, 480);
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
            this.groupBox1.Location = new System.Drawing.Point(394, 99);
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
            // cmboFabric
            // 
            this.cmboFabric.FormattingEnabled = true;
            this.cmboFabric.Location = new System.Drawing.Point(128, 151);
            this.cmboFabric.Name = "cmboFabric";
            this.cmboFabric.Size = new System.Drawing.Size(158, 21);
            this.cmboFabric.TabIndex = 10;
            this.cmboFabric.SelectedIndexChanged += new System.EventHandler(this.cmboFabric_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Fabric";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Yield Factor";
            // 
            // txtYieldFactor
            // 
            this.txtYieldFactor.Location = new System.Drawing.Point(128, 198);
            this.txtYieldFactor.Name = "txtYieldFactor";
            this.txtYieldFactor.ReadOnly = true;
            this.txtYieldFactor.Size = new System.Drawing.Size(100, 20);
            this.txtYieldFactor.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(266, 205);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Colour Required";
            // 
            // cmboColors
            // 
            this.cmboColors.FormattingEnabled = true;
            this.cmboColors.Location = new System.Drawing.Point(394, 200);
            this.cmboColors.Name = "cmboColors";
            this.cmboColors.Size = new System.Drawing.Size(121, 21);
            this.cmboColors.TabIndex = 17;
            this.cmboColors.SelectedIndexChanged += new System.EventHandler(this.cmboColors_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Customer Order No";
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(128, 105);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerOrder.TabIndex = 19;
            this.txtCustomerOrder.TextChanged += new System.EventHandler(this.txtWFDye_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(82, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(578, 191);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Requirements";
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
            this.cmboDyeOrders.FormattingEnabled = true;
            this.cmboDyeOrders.Location = new System.Drawing.Point(425, 9);
            this.cmboDyeOrders.Name = "cmboDyeOrders";
            this.cmboDyeOrders.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOrders.TabIndex = 21;
            this.cmboDyeOrders.SelectedIndexChanged += new System.EventHandler(this.cmboDyeOrders_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(349, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Dye Orders";
            // 
            // txtDyeOrder
            // 
            this.txtDyeOrder.Location = new System.Drawing.Point(128, 12);
            this.txtDyeOrder.Name = "txtDyeOrder";
            this.txtDyeOrder.ReadOnly = true;
            this.txtDyeOrder.Size = new System.Drawing.Size(100, 20);
            this.txtDyeOrder.TabIndex = 23;
            // 
            // frmDyeOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 515);
            this.Controls.Add(this.txtDyeOrder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboDyeOrders);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtCustomerOrder);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmboColors);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtYieldFactor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmboFabric);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboCustomerNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDyeOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmDyeOrders";
            this.Text = "Dye Orders (Fabric)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.ComboBox cmboFabric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDyeProductionLoss;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtYieldFactor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmboColors;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCustomerOrder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboDyeOrders;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDyeOrder;
    }
}