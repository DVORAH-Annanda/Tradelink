
namespace CustomerServices
{
    partial class frmContiWorkWearReceipts
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
            this.dtpDateOfTrans = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboWarehouses = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDelNo = new System.Windows.Forms.TextBox();
            this.txtTTIOrderNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmboSuppliers = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 521);
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
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date Of Transaction";
            // 
            // dtpDateOfTrans
            // 
            this.dtpDateOfTrans.Location = new System.Drawing.Point(149, 19);
            this.dtpDateOfTrans.Name = "dtpDateOfTrans";
            this.dtpDateOfTrans.Size = new System.Drawing.Size(131, 20);
            this.dtpDateOfTrans.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Receiving Warehouse";
            // 
            // cmboWarehouses
            // 
            this.cmboWarehouses.FormattingEnabled = true;
            this.cmboWarehouses.Location = new System.Drawing.Point(149, 147);
            this.cmboWarehouses.Name = "cmboWarehouses";
            this.cmboWarehouses.Size = new System.Drawing.Size(191, 21);
            this.cmboWarehouses.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtDelNo);
            this.groupBox1.Controls.Add(this.txtTTIOrderNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmboSuppliers);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTransNumber);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmboWarehouses);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDateOfTrans);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(157, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 191);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(235, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Supplier Del No";
            // 
            // txtDelNo
            // 
            this.txtDelNo.Location = new System.Drawing.Point(340, 106);
            this.txtDelNo.Name = "txtDelNo";
            this.txtDelNo.Size = new System.Drawing.Size(100, 20);
            this.txtDelNo.TabIndex = 12;
            // 
            // txtTTIOrderNo
            // 
            this.txtTTIOrderNo.Location = new System.Drawing.Point(95, 102);
            this.txtTTIOrderNo.Name = "txtTTIOrderNo";
            this.txtTTIOrderNo.Size = new System.Drawing.Size(133, 20);
            this.txtTTIOrderNo.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "TTI Order No";
            // 
            // cmboSuppliers
            // 
            this.cmboSuppliers.FormattingEnabled = true;
            this.cmboSuppliers.Location = new System.Drawing.Point(296, 60);
            this.cmboSuppliers.Name = "cmboSuppliers";
            this.cmboSuppliers.Size = new System.Drawing.Size(144, 21);
            this.cmboSuppliers.TabIndex = 9;
            this.cmboSuppliers.SelectedIndexChanged += new System.EventHandler(this.cmboSuppliers_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(232, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Supplier";
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Location = new System.Drawing.Point(107, 65);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(71, 20);
            this.txtTransNumber.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Transnumber";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(157, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(537, 324);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // frmContiWorkWearReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 572);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmContiWorkWearReceipts";
            this.Text = "Conti WorkWear Stock Receipts";
            this.Load += new System.EventHandler(this.frmContiWorkWearReceipts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateOfTrans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboWarehouses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDelNo;
        private System.Windows.Forms.TextBox txtTTIOrderNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboSuppliers;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTransNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}