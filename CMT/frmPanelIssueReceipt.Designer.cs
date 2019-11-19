namespace CMT
{
    partial class frmPanelIssueReceipt
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
            this.cmboDelivery = new System.Windows.Forms.ComboBox();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtStoreFacilities = new System.Windows.Forms.TextBox();
            this.cmboStoreFacilities = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboCMT = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(593, 534);
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
            this.label1.Location = new System.Drawing.Point(150, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CMT Delivery Number";
            // 
            // cmboDelivery
            // 
            this.cmboDelivery.FormattingEnabled = true;
            this.cmboDelivery.Location = new System.Drawing.Point(302, 63);
            this.cmboDelivery.Name = "cmboDelivery";
            this.cmboDelivery.Size = new System.Drawing.Size(158, 21);
            this.cmboDelivery.TabIndex = 2;
            this.cmboDelivery.SelectedIndexChanged += new System.EventHandler(this.cmboDelivery_SelectedIndexChanged);
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(302, 116);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(121, 20);
            this.dtpTransDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Transaction Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(22, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 219);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(562, 178);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtStoreFacilities);
            this.groupBox2.Controls.Add(this.cmboStoreFacilities);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(153, 403);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(361, 130);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Assign To";
            // 
            // TxtStoreFacilities
            // 
            this.TxtStoreFacilities.Location = new System.Drawing.Point(63, 82);
            this.TxtStoreFacilities.Name = "TxtStoreFacilities";
            this.TxtStoreFacilities.ReadOnly = true;
            this.TxtStoreFacilities.Size = new System.Drawing.Size(256, 20);
            this.TxtStoreFacilities.TabIndex = 2;
            // 
            // cmboStoreFacilities
            // 
            this.cmboStoreFacilities.FormattingEnabled = true;
            this.cmboStoreFacilities.Location = new System.Drawing.Point(149, 37);
            this.cmboStoreFacilities.Name = "cmboStoreFacilities";
            this.cmboStoreFacilities.Size = new System.Drawing.Size(179, 21);
            this.cmboStoreFacilities.TabIndex = 1;
            this.cmboStoreFacilities.SelectedIndexChanged += new System.EventHandler(this.cmboStoreFacilities_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Store Facilities";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current CMTs ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmboCMT
            // 
            this.cmboCMT.FormattingEnabled = true;
            this.cmboCMT.Location = new System.Drawing.Point(302, 12);
            this.cmboCMT.Name = "cmboCMT";
            this.cmboCMT.Size = new System.Drawing.Size(158, 21);
            this.cmboCMT.TabIndex = 8;
            this.cmboCMT.SelectedIndexChanged += new System.EventHandler(this.cmboCMT_SelectedIndexChanged);
            // 
            // frmPanelIssueReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 569);
            this.Controls.Add(this.cmboCMT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.cmboDelivery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmPanelIssueReceipt";
            this.Text = "Panel Issue Receipt";
            this.Load += new System.EventHandler(this.frmPanelIssueReceipt_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboDelivery;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmboStoreFacilities;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtStoreFacilities;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboCMT;
        // private System.Windows.Forms.ComboBox cmboMultiChoice; 
    }
}