namespace Spinning
{
    partial class frmCottonQA
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
            this.cmbLotNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGlobal = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFromMicr = new System.Windows.Forms.TextBox();
            this.txtFromStaple = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtToMicra = new System.Windows.Forms.TextBox();
            this.txtToStaple = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(588, 438);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(107, 127);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(458, 150);
            this.dataGridView1.TabIndex = 6;
            // 
            // cmbLotNo
            // 
            this.cmbLotNo.FormattingEnabled = true;
            this.cmbLotNo.Location = new System.Drawing.Point(221, 36);
            this.cmbLotNo.Name = "cmbLotNo";
            this.cmbLotNo.Size = new System.Drawing.Size(206, 21);
            this.cmbLotNo.TabIndex = 5;
            this.cmbLotNo.SelectedIndexChanged += new System.EventHandler(this.cmbLotNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Please select a lot No";
            // 
            // cbGlobal
            // 
            this.cbGlobal.AutoSize = true;
            this.cbGlobal.Location = new System.Drawing.Point(221, 76);
            this.cbGlobal.Name = "cbGlobal";
            this.cbGlobal.Size = new System.Drawing.Size(135, 17);
            this.cbGlobal.TabIndex = 8;
            this.cbGlobal.Text = "Global  Confirm Update";
            this.cbGlobal.UseVisualStyleBackColor = true;
            this.cbGlobal.CheckedChanged += new System.EventHandler(this.cbGlobal_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtToStaple);
            this.groupBox1.Controls.Add(this.txtToMicra);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txtFromStaple);
            this.groupBox1.Controls.Add(this.txtFromMicr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(111, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 136);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Micra From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Staple From";
            // 
            // txtFromMicr
            // 
            this.txtFromMicr.Location = new System.Drawing.Point(112, 20);
            this.txtFromMicr.Name = "txtFromMicr";
            this.txtFromMicr.Size = new System.Drawing.Size(100, 20);
            this.txtFromMicr.TabIndex = 1;
            this.txtFromMicr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFromStaple
            // 
            this.txtFromStaple.Location = new System.Drawing.Point(112, 60);
            this.txtFromStaple.Name = "txtFromStaple";
            this.txtFromStaple.Size = new System.Drawing.Size(100, 20);
            this.txtFromStaple.TabIndex = 3;
            this.txtFromStaple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 103);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Apply the above";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Micra To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Staple To";
            // 
            // txtToMicra
            // 
            this.txtToMicra.Location = new System.Drawing.Point(328, 20);
            this.txtToMicra.Name = "txtToMicra";
            this.txtToMicra.Size = new System.Drawing.Size(100, 20);
            this.txtToMicra.TabIndex = 2;
            this.txtToMicra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtToStaple
            // 
            this.txtToStaple.Location = new System.Drawing.Point(328, 60);
            this.txtToStaple.Name = "txtToStaple";
            this.txtToStaple.Size = new System.Drawing.Size(100, 20);
            this.txtToStaple.TabIndex = 4;
            this.txtToStaple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmCottonQA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 473);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbGlobal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbLotNo);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonQA";
            this.Text = "QA Cotton Confirmation";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbLotNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbGlobal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFromStaple;
        private System.Windows.Forms.TextBox txtFromMicr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtToStaple;
        private System.Windows.Forms.TextBox txtToMicra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}