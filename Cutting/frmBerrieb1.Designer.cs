namespace Cutting
{
    partial class frmBerrieb1
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
            this.txtQuality = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDyeBatchNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboOperators = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQuality
            // 
            this.txtQuality.Controls.Add(this.textBox1);
            this.txtQuality.Controls.Add(this.label4);
            this.txtQuality.Controls.Add(this.txtColour);
            this.txtQuality.Controls.Add(this.label3);
            this.txtQuality.Controls.Add(this.txtDyeBatchNo);
            this.txtQuality.Controls.Add(this.label2);
            this.txtQuality.Controls.Add(this.cmboCutSheet);
            this.txtQuality.Controls.Add(this.label1);
            this.txtQuality.Location = new System.Drawing.Point(147, 12);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(383, 144);
            this.txtQuality.TabIndex = 0;
            this.txtQuality.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(196, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Body Quality";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(196, 74);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(100, 20);
            this.txtColour.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Colour";
            // 
            // txtDyeBatchNo
            // 
            this.txtDyeBatchNo.Location = new System.Drawing.Point(196, 46);
            this.txtDyeBatchNo.Name = "txtDyeBatchNo";
            this.txtDyeBatchNo.ReadOnly = true;
            this.txtDyeBatchNo.Size = new System.Drawing.Size(100, 20);
            this.txtDyeBatchNo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dye Batch No";
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(132, 17);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(164, 21);
            this.cmboCutSheet.TabIndex = 1;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheet";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(95, 216);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(516, 229);
            this.dataGridView1.TabIndex = 2;
            // 
            // cmboOperators
            // 
            this.cmboOperators.FormattingEnabled = true;
            this.cmboOperators.Location = new System.Drawing.Point(287, 178);
            this.cmboOperators.Name = "cmboOperators";
            this.cmboOperators.Size = new System.Drawing.Size(156, 21);
            this.cmboOperators.TabIndex = 3;
            this.cmboOperators.SelectedIndexChanged += new System.EventHandler(this.cmboOperators_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Operators";
            // 
            // frmBerrieb1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 536);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboOperators);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtQuality);
            this.Name = "frmBerrieb1";
            this.Text = "Berribi Check Results";
            this.Load += new System.EventHandler(this.frmBerrieb1_Load);
            this.txtQuality.ResumeLayout(false);
            this.txtQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox txtQuality;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDyeBatchNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboOperators;
        private System.Windows.Forms.Label label5;
    }
}