namespace DyeHouse
{
    partial class frmCloseDyeOrders
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGarments = new System.Windows.Forms.RadioButton();
            this.rbFabricSales = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(39, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(563, 311);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(555, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save\r\n";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFabricSales);
            this.groupBox1.Controls.Add(this.rbGarments);
            this.groupBox1.Location = new System.Drawing.Point(243, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 34);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\\";
            // 
            // rbGarments
            // 
            this.rbGarments.AutoSize = true;
            this.rbGarments.Location = new System.Drawing.Point(20, 11);
            this.rbGarments.Name = "rbGarments";
            this.rbGarments.Size = new System.Drawing.Size(70, 17);
            this.rbGarments.TabIndex = 0;
            this.rbGarments.TabStop = true;
            this.rbGarments.Text = "Garments";
            this.rbGarments.UseVisualStyleBackColor = true;
            this.rbGarments.CheckedChanged += new System.EventHandler(this.rbGarments_CheckedChanged);
            // 
            // rbFabricSales
            // 
            this.rbFabricSales.AutoSize = true;
            this.rbFabricSales.Location = new System.Drawing.Point(133, 11);
            this.rbFabricSales.Name = "rbFabricSales";
            this.rbFabricSales.Size = new System.Drawing.Size(83, 17);
            this.rbFabricSales.TabIndex = 1;
            this.rbFabricSales.TabStop = true;
            this.rbFabricSales.Text = "Fabric Sales";
            this.rbFabricSales.UseVisualStyleBackColor = true;
            this.rbFabricSales.CheckedChanged += new System.EventHandler(this.rbFabricSales_CheckedChanged);
            // 
            // frmCloseDyeOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 434);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmCloseDyeOrders";
            this.Text = "Close Dye Orders";
            this.Load += new System.EventHandler(this.frmCloseDyeOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFabricSales;
        private System.Windows.Forms.RadioButton rbGarments;
    }
}