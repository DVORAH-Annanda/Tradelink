namespace Knitting
{
    partial class frmPalletSold
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
            this.oCmboA = new System.Windows.Forms.ComboBox();
            this.btnSAve = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Yarn Ord`ers";
            // 
            // oCmboA
            // 
            this.oCmboA.FormattingEnabled = true;
            this.oCmboA.Location = new System.Drawing.Point(441, 44);
            this.oCmboA.Name = "oCmboA";
            this.oCmboA.Size = new System.Drawing.Size(303, 21);
            this.oCmboA.TabIndex = 1;
            this.oCmboA.SelectedIndexChanged += new System.EventHandler(this.oCmboA_SelectedIndexChanged);
            // 
            // btnSAve
            // 
            this.btnSAve.Location = new System.Drawing.Point(888, 403);
            this.btnSAve.Name = "btnSAve";
            this.btnSAve.Size = new System.Drawing.Size(75, 23);
            this.btnSAve.TabIndex = 2;
            this.btnSAve.Text = "Save";
            this.btnSAve.UseVisualStyleBackColor = true;
            this.btnSAve.Click += new System.EventHandler(this.btnSAve_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(441, 102);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(335, 213);
            this.dataGridView1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Existing Pallets";
            // 
            // frmPalletSold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSAve);
            this.Controls.Add(this.oCmboA);
            this.Controls.Add(this.label1);
            this.Name = "frmPalletSold";
            this.Text = "Yarn Pallet Sold to Customer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPalletSold_FormClosing);
            this.Load += new System.EventHandler(this.frmPalletSold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox oCmboA;
        private System.Windows.Forms.Button btnSAve;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
    }
}