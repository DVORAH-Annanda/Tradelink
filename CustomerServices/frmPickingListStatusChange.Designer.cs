namespace CustomerServices
{
    partial class frmPickingListStatusChange
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlaceOffStatus = new System.Windows.Forms.RadioButton();
            this.rbPlaceOnStatus = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPlaceOffStatus);
            this.groupBox1.Controls.Add(this.rbPlaceOnStatus);
            this.groupBox1.Location = new System.Drawing.Point(110, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbPlaceOffStatus
            // 
            this.rbPlaceOffStatus.AutoSize = true;
            this.rbPlaceOffStatus.Location = new System.Drawing.Point(63, 61);
            this.rbPlaceOffStatus.Name = "rbPlaceOffStatus";
            this.rbPlaceOffStatus.Size = new System.Drawing.Size(219, 17);
            this.rbPlaceOffStatus.TabIndex = 1;
            this.rbPlaceOffStatus.TabStop = true;
            this.rbPlaceOffStatus.Text = "Place Picking List Off Stock Order Status";
            this.rbPlaceOffStatus.UseVisualStyleBackColor = true;
            this.rbPlaceOffStatus.CheckedChanged += new System.EventHandler(this.rbPlaceOffStatus_CheckedChanged);
            // 
            // rbPlaceOnStatus
            // 
            this.rbPlaceOnStatus.AutoSize = true;
            this.rbPlaceOnStatus.Location = new System.Drawing.Point(63, 20);
            this.rbPlaceOnStatus.Name = "rbPlaceOnStatus";
            this.rbPlaceOnStatus.Size = new System.Drawing.Size(207, 17);
            this.rbPlaceOnStatus.TabIndex = 0;
            this.rbPlaceOnStatus.TabStop = true;
            this.rbPlaceOnStatus.Text = "Change to Stock Order status  (To On)";
            this.rbPlaceOnStatus.UseVisualStyleBackColor = true;
            this.rbPlaceOnStatus.CheckedChanged += new System.EventHandler(this.rbPlaceOnStatus_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 207);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(515, 189);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(478, 436);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPickingListStatusChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 471);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPickingListStatusChange";
            this.Text = "Place Picking List On / Off Status";
            this.Load += new System.EventHandler(this.frmPickingListStatusChange_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPlaceOffStatus;
        private System.Windows.Forms.RadioButton rbPlaceOnStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
    }
}