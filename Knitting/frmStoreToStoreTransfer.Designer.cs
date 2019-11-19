namespace Knitting
{
    partial class frmStoreToStoreTransfer
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
            this.comboFromStore = new System.Windows.Forms.ComboBox();
            this.comboToStore = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboFromStore
            // 
            this.comboFromStore.FormattingEnabled = true;
            this.comboFromStore.Location = new System.Drawing.Point(172, 31);
            this.comboFromStore.Name = "comboFromStore";
            this.comboFromStore.Size = new System.Drawing.Size(258, 21);
            this.comboFromStore.TabIndex = 0;
            this.comboFromStore.SelectedIndexChanged += new System.EventHandler(this.comboFromStore_SelectedIndexChanged);
            // 
            // comboToStore
            // 
            this.comboToStore.FormattingEnabled = true;
            this.comboToStore.Location = new System.Drawing.Point(172, 284);
            this.comboToStore.Name = "comboToStore";
            this.comboToStore.Size = new System.Drawing.Size(270, 21);
            this.comboToStore.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(87, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(439, 172);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(497, 315);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmStoreToStoreTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 362);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboToStore);
            this.Controls.Add(this.comboFromStore);
            this.Name = "frmStoreToStoreTransfer";
            this.Text = "Store To Store Transfer";
            this.Load += new System.EventHandler(this.frmStoreToStoreTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboFromStore;
        private System.Windows.Forms.ComboBox comboToStore;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
    }
}