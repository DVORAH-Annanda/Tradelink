namespace Spinning
{
    partial class frmCottonWaste
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
            this.dtpSales = new System.Windows.Forms.DateTimePicker();
            this.cmboCustomerFile = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransactionNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date of Transaction";
            // 
            // dtpSales
            // 
            this.dtpSales.Location = new System.Drawing.Point(291, 48);
            this.dtpSales.Name = "dtpSales";
            this.dtpSales.Size = new System.Drawing.Size(134, 20);
            this.dtpSales.TabIndex = 1;
            // 
            // cmboCustomerFile
            // 
            this.cmboCustomerFile.FormattingEnabled = true;
            this.cmboCustomerFile.Location = new System.Drawing.Point(291, 83);
            this.cmboCustomerFile.Name = "cmboCustomerFile";
            this.cmboCustomerFile.Size = new System.Drawing.Size(121, 21);
            this.cmboCustomerFile.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(97, 166);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(463, 150);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(485, 349);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transaction Number";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new System.Drawing.Point(291, 119);
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.ReadOnly = true;
            this.txtTransactionNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTransactionNumber.TabIndex = 7;
            this.txtTransactionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmCottonWaste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 412);
            this.Controls.Add(this.txtTransactionNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboCustomerFile);
            this.Controls.Add(this.dtpSales);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonWaste";
            this.Text = "Cotton Waste Sales";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpSales;
        private System.Windows.Forms.ComboBox cmboCustomerFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTransactionNumber;
    }
}