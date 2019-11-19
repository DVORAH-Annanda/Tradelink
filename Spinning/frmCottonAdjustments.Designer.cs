namespace Spinning
{
    partial class frmCottonAdjustments
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
            this.txtAdjustmentNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbContractNo = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbLotNo = new System.Windows.Forms.ComboBox();
            this.txtPre_DeterminedNettWA = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtActualNettWA = new System.Windows.Forms.TextBox();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWriteOff = new System.Windows.Forms.RadioButton();
            this.rbWriteOn = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtActualGrossWA = new System.Windows.Forms.TextBox();
            this.txtPre_DeterminedGrossWA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adjustment Number";
            // 
            // txtAdjustmentNumber
            // 
            this.txtAdjustmentNumber.Location = new System.Drawing.Point(258, 23);
            this.txtAdjustmentNumber.Name = "txtAdjustmentNumber";
            this.txtAdjustmentNumber.Size = new System.Drawing.Size(192, 20);
            this.txtAdjustmentNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contract Number";
            // 
            // cmbContractNo
            // 
            this.cmbContractNo.FormattingEnabled = true;
            this.cmbContractNo.Location = new System.Drawing.Point(258, 67);
            this.cmbContractNo.Name = "cmbContractNo";
            this.cmbContractNo.Size = new System.Drawing.Size(192, 21);
            this.cmbContractNo.TabIndex = 3;
            this.cmbContractNo.SelectedIndexChanged += new System.EventHandler(this.cmbContractNo_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(258, 114);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date Adjusted";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Lot Number";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(145, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(499, 150);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.datagridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // cmbLotNo
            // 
            this.cmbLotNo.FormattingEnabled = true;
            this.cmbLotNo.Location = new System.Drawing.Point(258, 156);
            this.cmbLotNo.Name = "cmbLotNo";
            this.cmbLotNo.Size = new System.Drawing.Size(200, 21);
            this.cmbLotNo.TabIndex = 8;
            this.cmbLotNo.SelectedIndexChanged += new System.EventHandler(this.cmbLotNo_SelectedIndexChanged);
            // 
            // txtPre_DeterminedNettWA
            // 
            this.txtPre_DeterminedNettWA.Location = new System.Drawing.Point(450, 46);
            this.txtPre_DeterminedNettWA.Name = "txtPre_DeterminedNettWA";
            this.txtPre_DeterminedNettWA.Size = new System.Drawing.Size(100, 20);
            this.txtPre_DeterminedNettWA.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(269, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Actual Weight Adjusted";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(679, 611);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtActualNettWA
            // 
            this.txtActualNettWA.Location = new System.Drawing.Point(450, 88);
            this.txtActualNettWA.Name = "txtActualNettWA";
            this.txtActualNettWA.ReadOnly = true;
            this.txtActualNettWA.Size = new System.Drawing.Size(100, 20);
            this.txtActualNettWA.TabIndex = 13;
            this.txtActualNettWA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(313, 550);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(145, 96);
            this.rtbNotes.TabIndex = 14;
            this.rtbNotes.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(193, 599);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Notes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWriteOff);
            this.groupBox1.Controls.Add(this.rbWriteOn);
            this.groupBox1.Location = new System.Drawing.Point(290, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 55);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Detail";
            // 
            // rbWriteOff
            // 
            this.rbWriteOff.AutoSize = true;
            this.rbWriteOff.Location = new System.Drawing.Point(83, 19);
            this.rbWriteOff.Name = "rbWriteOff";
            this.rbWriteOff.Size = new System.Drawing.Size(67, 17);
            this.rbWriteOff.TabIndex = 18;
            this.rbWriteOff.TabStop = true;
            this.rbWriteOff.Text = "Write Off";
            this.rbWriteOff.UseVisualStyleBackColor = true;
            this.rbWriteOff.CheckedChanged += new System.EventHandler(this.rbWriteOff_CheckedChanged);
            // 
            // rbWriteOn
            // 
            this.rbWriteOn.AutoSize = true;
            this.rbWriteOn.Location = new System.Drawing.Point(6, 19);
            this.rbWriteOn.Name = "rbWriteOn";
            this.rbWriteOn.Size = new System.Drawing.Size(67, 17);
            this.rbWriteOn.TabIndex = 17;
            this.rbWriteOn.TabStop = true;
            this.rbWriteOn.Text = "Write On";
            this.rbWriteOn.UseVisualStyleBackColor = true;
            this.rbWriteOn.CheckedChanged += new System.EventHandler(this.rbWriteOn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtActualGrossWA);
            this.groupBox2.Controls.Add(this.txtPre_DeterminedGrossWA);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtPre_DeterminedNettWA);
            this.groupBox2.Controls.Add(this.txtActualNettWA);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(103, 420);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 124);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adjustments Stats";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(139, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Gross Weight";
            // 
            // txtActualGrossWA
            // 
            this.txtActualGrossWA.Location = new System.Drawing.Point(114, 88);
            this.txtActualGrossWA.Name = "txtActualGrossWA";
            this.txtActualGrossWA.ReadOnly = true;
            this.txtActualGrossWA.Size = new System.Drawing.Size(100, 20);
            this.txtActualGrossWA.TabIndex = 17;
            this.txtActualGrossWA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPre_DeterminedGrossWA
            // 
            this.txtPre_DeterminedGrossWA.Location = new System.Drawing.Point(114, 46);
            this.txtPre_DeterminedGrossWA.Name = "txtPre_DeterminedGrossWA";
            this.txtPre_DeterminedGrossWA.Size = new System.Drawing.Size(100, 20);
            this.txtPre_DeterminedGrossWA.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Predetermined Value";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(466, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Nett Weight";
            // 
            // frmCottonAdjustments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 658);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbLotNo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmbContractNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAdjustmentNumber);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonAdjustments";
            this.Text = "Cotton Adjustments";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdjustmentNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbContractNo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbLotNo;
        private System.Windows.Forms.TextBox txtPre_DeterminedNettWA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtActualNettWA;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWriteOff;
        private System.Windows.Forms.RadioButton rbWriteOn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtActualGrossWA;
        private System.Windows.Forms.TextBox txtPre_DeterminedGrossWA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}