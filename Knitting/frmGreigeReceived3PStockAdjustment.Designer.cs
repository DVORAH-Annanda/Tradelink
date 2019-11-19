namespace Knitting
{
    partial class frmGreigeReceived3PStockAdjustment
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
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdjustmentNumber = new System.Windows.Forms.TextBox();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTrans = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Date";
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Location = new System.Drawing.Point(285, 104);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(135, 20);
            this.dtpTransactionDate.TabIndex = 1;
            this.dtpTransactionDate.ValueChanged += new System.EventHandler(this.dtpTransactionDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Adjustment Number";
            // 
            // txtAdjustmentNumber
            // 
            this.txtAdjustmentNumber.Location = new System.Drawing.Point(285, 23);
            this.txtAdjustmentNumber.Name = "txtAdjustmentNumber";
            this.txtAdjustmentNumber.ReadOnly = true;
            this.txtAdjustmentNumber.Size = new System.Drawing.Size(135, 20);
            this.txtAdjustmentNumber.TabIndex = 3;
            // 
            // cmbStore
            // 
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(285, 144);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(263, 21);
            this.cmbStore.TabIndex = 4;
            this.cmbStore.SelectedIndexChanged += new System.EventHandler(this.cmbStore_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Store";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new System.Drawing.Point(285, 185);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(305, 20);
            this.txtApprovedBy.TabIndex = 6;
            this.txtApprovedBy.Leave += new System.EventHandler(this.OnLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Approved By";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(621, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(152, 274);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(438, 170);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Previous Transactions";
            // 
            // cmbTrans
            // 
            this.cmbTrans.FormattingEnabled = true;
            this.cmbTrans.Location = new System.Drawing.Point(285, 63);
            this.cmbTrans.Name = "cmbTrans";
            this.cmbTrans.Size = new System.Drawing.Size(135, 21);
            this.cmbTrans.TabIndex = 12;
            this.cmbTrans.SelectedIndexChanged += new System.EventHandler(this.cmbTrans_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Reason for Ajustment";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(286, 223);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(305, 20);
            this.txtReason.TabIndex = 14;
            this.txtReason.Leave += new System.EventHandler(this.OnLeave);
            // 
            // frmGreigeReceived3PStockAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 492);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbTrans);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtApprovedBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.txtAdjustmentNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTransactionDate);
            this.Controls.Add(this.label1);
            this.Name = "frmGreigeReceived3PStockAdjustment";
            this.Text = "Greige Received from 3rd Parties Commission Dyeing  Stock Ajustment";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdjustmentNumber;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTrans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReason;
    }
}