namespace Knitting
{
    partial class frmConversion
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
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.comboGreige = new System.Windows.Forms.ComboBox();
            this.comboMachines = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Location = new System.Drawing.Point(168, 27);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(144, 20);
            this.dtpTransactionDate.TabIndex = 0;
            // 
            // comboGreige
            // 
            this.comboGreige.FormattingEnabled = true;
            this.comboGreige.Location = new System.Drawing.Point(168, 79);
            this.comboGreige.Name = "comboGreige";
            this.comboGreige.Size = new System.Drawing.Size(218, 21);
            this.comboGreige.TabIndex = 1;
            // 
            // comboMachines
            // 
            this.comboMachines.FormattingEnabled = true;
            this.comboMachines.Location = new System.Drawing.Point(168, 132);
            this.comboMachines.Name = "comboMachines";
            this.comboMachines.Size = new System.Drawing.Size(218, 21);
            this.comboMachines.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(77, 183);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(486, 229);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(527, 446);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 499);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboMachines);
            this.Controls.Add(this.comboGreige);
            this.Controls.Add(this.dtpTransactionDate);
            this.Name = "frmConversion";
            this.Text = "Conversion from Previous System";
            this.Load += new System.EventHandler(this.frmConversion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.ComboBox comboGreige;
        private System.Windows.Forms.ComboBox comboMachines;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
    }
}