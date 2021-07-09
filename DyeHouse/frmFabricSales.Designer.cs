namespace DyeHouse
{
    partial class frmFabricSales
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
            this.dtTransDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbRejectStore = new System.Windows.Forms.RadioButton();
            this.rbFabricStore = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboContracts = new System.Windows.Forms.ComboBox();
            this.cmboGreige = new DyeHouse.CheckComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Date";
            // 
            // dtTransDate
            // 
            this.dtTransDate.Location = new System.Drawing.Point(247, 17);
            this.dtTransDate.Name = "dtTransDate";
            this.dtTransDate.Size = new System.Drawing.Size(152, 20);
            this.dtTransDate.TabIndex = 1;
            this.dtTransDate.ValueChanged += new System.EventHandler(this.dtTransDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transaction Number";
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Location = new System.Drawing.Point(247, 59);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTransNumber.TabIndex = 3;
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(247, 104);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(214, 21);
            this.cmboCustomers.TabIndex = 4;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Customer Order Number";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(247, 148);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.txtOrderNumber.TabIndex = 7;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.Validated += new System.EventHandler(this.txtOrderNumber_Validated);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboContracts);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmboGreige);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rbRejectStore);
            this.groupBox1.Controls.Add(this.rbFabricStore);
            this.groupBox1.Location = new System.Drawing.Point(114, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 183);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fabric Location";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Fabric Type";
            // 
            // rbRejectStore
            // 
            this.rbRejectStore.AutoSize = true;
            this.rbRejectStore.Location = new System.Drawing.Point(231, 139);
            this.rbRejectStore.Name = "rbRejectStore";
            this.rbRejectStore.Size = new System.Drawing.Size(116, 17);
            this.rbRejectStore.TabIndex = 1;
            this.rbRejectStore.TabStop = true;
            this.rbRejectStore.Text = "Fabric Reject Store";
            this.rbRejectStore.UseVisualStyleBackColor = true;
            this.rbRejectStore.CheckedChanged += new System.EventHandler(this.rbRejectStore_CheckedChanged);
            // 
            // rbFabricStore
            // 
            this.rbFabricStore.AutoSize = true;
            this.rbFabricStore.Location = new System.Drawing.Point(59, 139);
            this.rbFabricStore.Name = "rbFabricStore";
            this.rbFabricStore.Size = new System.Drawing.Size(82, 17);
            this.rbFabricStore.TabIndex = 0;
            this.rbFabricStore.TabStop = true;
            this.rbFabricStore.Text = "Fabric Store";
            this.rbFabricStore.UseVisualStyleBackColor = true;
            this.rbFabricStore.CheckedChanged += new System.EventHandler(this.rbFabricStore_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(590, 592);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(71, 401);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(557, 174);
            this.dataGridView1.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Fabric Contract No";
            // 
            // cmboContracts
            // 
            this.cmboContracts.FormattingEnabled = true;
            this.cmboContracts.Location = new System.Drawing.Point(173, 83);
            this.cmboContracts.Name = "cmboContracts";
            this.cmboContracts.Size = new System.Drawing.Size(183, 21);
            this.cmboContracts.TabIndex = 5;
            // 
            // cmboGreige
            // 
            this.cmboGreige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(173, 37);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(282, 21);
            this.cmboGreige.TabIndex = 3;
            this.cmboGreige.Text = "Select Options";
            this.cmboGreige.SelectedIndexChanged += new System.EventHandler(this.cmboGreige_SelectedIndexChanged);
            // 
            // frmFabricSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 627);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.txtTransNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtTransDate);
            this.Controls.Add(this.label1);
            this.Name = "frmFabricSales";
            this.Text = "Fabric Sales to Customer";
            this.Load += new System.EventHandler(this.frmFabricSales_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTransDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTransNumber;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRejectStore;
        private System.Windows.Forms.RadioButton rbFabricStore;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        // private System.Windows.Forms.ComboBox cmboGreige;
        private DyeHouse.CheckComboBox cmboGreige;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboContracts;
        private System.Windows.Forms.Label label6;
    }
}