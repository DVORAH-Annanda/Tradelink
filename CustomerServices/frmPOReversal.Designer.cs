namespace CustomerServices
{
    partial class frmPOReversal
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.cmboClosedPOs = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Purchase Orders";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(363, 49);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(182, 21);
            this.cmboCustomers.TabIndex = 2;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // cmboClosedPOs
            // 
            this.cmboClosedPOs.FormattingEnabled = true;
            this.cmboClosedPOs.Location = new System.Drawing.Point(363, 147);
            this.cmboClosedPOs.Name = "cmboClosedPOs";
            this.cmboClosedPOs.Size = new System.Drawing.Size(182, 21);
            this.cmboClosedPOs.TabIndex = 3;
            this.cmboClosedPOs.SelectedIndexChanged += new System.EventHandler(this.cmboClosedPOs_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(689, 394);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(128, 206);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(607, 150);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // frmPOReversal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboClosedPOs);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmPOReversal";
            this.Text = "Reversal of closed Purchase Orders";
            this.Load += new System.EventHandler(this.frmPOReversal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.ComboBox cmboClosedPOs;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}