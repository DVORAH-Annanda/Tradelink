namespace CustomerServices
{
    partial class frmTransferConfirm
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
            this.cmboWareHouse = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboDepartment = new System.Windows.Forms.ComboBox();
            this.chkSelectHistory = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Existing Picking Slips for";
            // 
            // cmboWareHouse
            // 
            this.cmboWareHouse.FormattingEnabled = true;
            this.cmboWareHouse.Location = new System.Drawing.Point(271, 118);
            this.cmboWareHouse.Name = "cmboWareHouse";
            this.cmboWareHouse.Size = new System.Drawing.Size(251, 21);
            this.cmboWareHouse.TabIndex = 1;
            this.cmboWareHouse.SelectedIndexChanged += new System.EventHandler(this.cmboWareHouse_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(610, 573);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(79, 179);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(506, 150);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(79, 363);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(506, 208);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "From CMT Facility";
            // 
            // cmboDepartment
            // 
            this.cmboDepartment.FormattingEnabled = true;
            this.cmboDepartment.Location = new System.Drawing.Point(271, 56);
            this.cmboDepartment.Name = "cmboDepartment";
            this.cmboDepartment.Size = new System.Drawing.Size(251, 21);
            this.cmboDepartment.TabIndex = 6;
            this.cmboDepartment.SelectedIndexChanged += new System.EventHandler(this.cmboDepartment_SelectedIndexChanged);
            // 
            // chkSelectHistory
            // 
            this.chkSelectHistory.AutoSize = true;
            this.chkSelectHistory.Location = new System.Drawing.Point(271, 13);
            this.chkSelectHistory.Name = "chkSelectHistory";
            this.chkSelectHistory.Size = new System.Drawing.Size(158, 17);
            this.chkSelectHistory.TabIndex = 7;
            this.chkSelectHistory.Text = "Select 3 months prior history";
            this.chkSelectHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSelectHistory.UseVisualStyleBackColor = true;
            // 
            // frmTransferConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 619);
            this.Controls.Add(this.chkSelectHistory);
            this.Controls.Add(this.cmboDepartment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboWareHouse);
            this.Controls.Add(this.label1);
            this.Name = "frmTransferConfirm";
            this.Text = "Transfer Confirmation (Delivery Note)";
            this.Load += new System.EventHandler(this.frmTransferConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboWareHouse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboDepartment;
        private System.Windows.Forms.CheckBox chkSelectHistory;
    }
}