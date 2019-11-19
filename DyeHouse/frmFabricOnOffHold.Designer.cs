namespace DyeHouse
{
    partial class frmFabricOnOffHold
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOffHold = new System.Windows.Forms.RadioButton();
            this.rbOnHold = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReasons = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(96, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(509, 219);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOffHold);
            this.groupBox1.Controls.Add(this.rbOnHold);
            this.groupBox1.Location = new System.Drawing.Point(167, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 53);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbOffHold
            // 
            this.rbOffHold.AutoSize = true;
            this.rbOffHold.Location = new System.Drawing.Point(192, 18);
            this.rbOffHold.Name = "rbOffHold";
            this.rbOffHold.Size = new System.Drawing.Size(64, 17);
            this.rbOffHold.TabIndex = 1;
            this.rbOffHold.TabStop = true;
            this.rbOffHold.Text = "Off Hold";
            this.rbOffHold.UseVisualStyleBackColor = true;
            this.rbOffHold.CheckedChanged += new System.EventHandler(this.rbOffHold_CheckedChanged);
            // 
            // rbOnHold
            // 
            this.rbOnHold.AutoSize = true;
            this.rbOnHold.Checked = true;
            this.rbOnHold.Location = new System.Drawing.Point(32, 18);
            this.rbOnHold.Name = "rbOnHold";
            this.rbOnHold.Size = new System.Drawing.Size(64, 17);
            this.rbOnHold.TabIndex = 0;
            this.rbOnHold.TabStop = true;
            this.rbOnHold.Text = "On Hold";
            this.rbOnHold.UseVisualStyleBackColor = true;
            this.rbOnHold.CheckedChanged += new System.EventHandler(this.rbOnHold_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(609, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reasons ";
            // 
            // txtReasons
            // 
            this.txtReasons.Location = new System.Drawing.Point(184, 342);
            this.txtReasons.Name = "txtReasons";
            this.txtReasons.Size = new System.Drawing.Size(407, 20);
            this.txtReasons.TabIndex = 4;
            // 
            // frmFabricOnOffHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 425);
            this.Controls.Add(this.txtReasons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmFabricOnOffHold";
            this.Text = "Fabric On Off Hold";
            this.Load += new System.EventHandler(this.frmFabricOnOffHold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOffHold;
        private System.Windows.Forms.RadioButton rbOnHold;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReasons;
    }
}