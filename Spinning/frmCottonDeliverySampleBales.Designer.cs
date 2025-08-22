namespace Spinning
{
    partial class frmCottonDeliverySampleBales
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
            this.dgvSampleBales = new System.Windows.Forms.DataGridView();
            this.lblMinRows = new System.Windows.Forms.Label();
            this.lblTotals = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtOverrideReason = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblReason = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSampleBales)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSampleBales
            // 
            this.dgvSampleBales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSampleBales.Location = new System.Drawing.Point(15, 43);
            this.dgvSampleBales.Name = "dgvSampleBales";
            this.dgvSampleBales.Size = new System.Drawing.Size(805, 475);
            this.dgvSampleBales.TabIndex = 0;
            // 
            // lblMinRows
            // 
            this.lblMinRows.AutoSize = true;
            this.lblMinRows.Location = new System.Drawing.Point(15, 538);
            this.lblMinRows.Name = "lblMinRows";
            this.lblMinRows.Size = new System.Drawing.Size(35, 13);
            this.lblMinRows.TabIndex = 1;
            this.lblMinRows.Text = "label1";
            // 
            // lblTotals
            // 
            this.lblTotals.AutoSize = true;
            this.lblTotals.Location = new System.Drawing.Point(15, 565);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(35, 13);
            this.lblTotals.TabIndex = 2;
            this.lblTotals.Text = "label1";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 592);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "label1";
            // 
            // txtOverrideReason
            // 
            this.txtOverrideReason.Location = new System.Drawing.Point(15, 636);
            this.txtOverrideReason.Name = "txtOverrideReason";
            this.txtOverrideReason.Size = new System.Drawing.Size(567, 20);
            this.txtOverrideReason.TabIndex = 4;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(588, 636);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(117, 20);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(711, 636);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 20);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(15, 619);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(35, 13);
            this.lblReason.TabIndex = 7;
            this.lblReason.Text = "label1";
            // 
            // frmCottonDeliverySampleBales
            // 
            this.ClientSize = new System.Drawing.Size(838, 673);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtOverrideReason);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTotals);
            this.Controls.Add(this.lblMinRows);
            this.Controls.Add(this.dgvSampleBales);
            this.Name = "frmCottonDeliverySampleBales";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSampleBales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSampleBales;
        private System.Windows.Forms.Label lblMinRows;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtOverrideReason;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblReason;
    }
}