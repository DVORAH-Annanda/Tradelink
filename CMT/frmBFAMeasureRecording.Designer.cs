namespace CMT
{
    partial class frmBFAMeasureRecording
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
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboOperators = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radHistory = new System.Windows.Forms.RadioButton();
            this.radCurrent = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboSizes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(203, 28);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(254, 21);
            this.cmboCutSheet.TabIndex = 0;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current CutSheets";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(54, 163);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(630, 275);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(609, 473);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date of Measurement";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(203, 113);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(135, 20);
            this.dtpTransDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 473);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quality Inspector";
            // 
            // cmboOperators
            // 
            this.cmboOperators.FormattingEnabled = true;
            this.cmboOperators.Location = new System.Drawing.Point(233, 470);
            this.cmboOperators.Name = "cmboOperators";
            this.cmboOperators.Size = new System.Drawing.Size(254, 21);
            this.cmboOperators.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radHistory);
            this.groupBox1.Controls.Add(this.radCurrent);
            this.groupBox1.Location = new System.Drawing.Point(511, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 96);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // radHistory
            // 
            this.radHistory.AutoSize = true;
            this.radHistory.Location = new System.Drawing.Point(51, 65);
            this.radHistory.Name = "radHistory";
            this.radHistory.Size = new System.Drawing.Size(57, 17);
            this.radHistory.TabIndex = 1;
            this.radHistory.TabStop = true;
            this.radHistory.Text = "History";
            this.radHistory.UseVisualStyleBackColor = true;
            this.radHistory.CheckedChanged += new System.EventHandler(this.radHistory_CheckedChanged);
            // 
            // radCurrent
            // 
            this.radCurrent.AutoSize = true;
            this.radCurrent.Location = new System.Drawing.Point(51, 22);
            this.radCurrent.Name = "radCurrent";
            this.radCurrent.Size = new System.Drawing.Size(59, 17);
            this.radCurrent.TabIndex = 0;
            this.radCurrent.TabStop = true;
            this.radCurrent.Text = "Current";
            this.radCurrent.UseVisualStyleBackColor = true;
            this.radCurrent.CheckedChanged += new System.EventHandler(this.radCurrent_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Sizes";
            // 
            // cmboSizes
            // 
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(203, 69);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(213, 21);
            this.cmboSizes.TabIndex = 10;
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // frmBFAMeasureRecording
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 508);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboOperators);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboCutSheet);
            this.Name = "frmBFAMeasureRecording";
            this.Text = "Bulk Final Audit Recording";
            this.Load += new System.EventHandler(this.BFAMeasureRecording_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboOperators;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radHistory;
        private System.Windows.Forms.RadioButton radCurrent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboSizes;
    }
}