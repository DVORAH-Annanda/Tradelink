namespace TTI2_WF
{
    partial class frmTLADMCustomerTypes
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
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboMeasurement = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radPanel = new System.Windows.Forms.RadioButton();
            this.radPPS = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCmtLines = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(448, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(48, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(475, 164);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cut Department Measurement Areas";
            // 
            // cmboMeasurement
            // 
            this.cmboMeasurement.FormattingEnabled = true;
            this.cmboMeasurement.Location = new System.Drawing.Point(230, 30);
            this.cmboMeasurement.Name = "cmboMeasurement";
            this.cmboMeasurement.Size = new System.Drawing.Size(179, 21);
            this.cmboMeasurement.TabIndex = 3;
            this.cmboMeasurement.SelectedIndexChanged += new System.EventHandler(this.cmboMeasurement_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radPanel);
            this.groupBox1.Controls.Add(this.radPPS);
            this.groupBox1.Location = new System.Drawing.Point(89, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 62);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measurement Type";
            // 
            // radPanel
            // 
            this.radPanel.AutoSize = true;
            this.radPanel.Location = new System.Drawing.Point(267, 30);
            this.radPanel.Name = "radPanel";
            this.radPanel.Size = new System.Drawing.Size(102, 17);
            this.radPanel.TabIndex = 1;
            this.radPanel.TabStop = true;
            this.radPanel.Text = "Panel Cut Sheet";
            this.radPanel.UseVisualStyleBackColor = true;
            this.radPanel.CheckedChanged += new System.EventHandler(this.radPanel_CheckedChanged);
            // 
            // radPPS
            // 
            this.radPPS.AutoSize = true;
            this.radPPS.Location = new System.Drawing.Point(75, 30);
            this.radPPS.Name = "radPPS";
            this.radPPS.Size = new System.Drawing.Size(121, 17);
            this.radPPS.TabIndex = 0;
            this.radPPS.TabStop = true;
            this.radPPS.Text = "Product Spec Sheet";
            this.radPPS.UseVisualStyleBackColor = true;
            this.radPPS.CheckedChanged += new System.EventHandler(this.radPPS_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboCmtLines);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmboMeasurement);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(89, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CMT ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CMT Line Details";
            // 
            // cmboCmtLines
            // 
            this.cmboCmtLines.FormattingEnabled = true;
            this.cmboCmtLines.Location = new System.Drawing.Point(226, 66);
            this.cmboCmtLines.Name = "cmboCmtLines";
            this.cmboCmtLines.Size = new System.Drawing.Size(183, 21);
            this.cmboCmtLines.TabIndex = 5;
            this.cmboCmtLines.SelectedIndexChanged += new System.EventHandler(this.cmboCmtLines_SelectedIndexChanged);
            // 
            // frmTLADMCustomerTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 422);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmTLADMCustomerTypes";
            this.Text = "Customer Types Update / Edit Facility";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboMeasurement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radPanel;
        private System.Windows.Forms.RadioButton radPPS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmboCmtLines;
        private System.Windows.Forms.Label label2;
    }
}