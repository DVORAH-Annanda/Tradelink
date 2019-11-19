namespace CMT
{
    partial class frmCutSheetDownSize
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
            this.cmboCutSheets = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.radWorkComplete = new System.Windows.Forms.RadioButton();
            this.radWorkInProgress = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmboCutSheets
            // 
            this.cmboCutSheets.FormattingEnabled = true;
            this.cmboCutSheets.Location = new System.Drawing.Point(227, 64);
            this.cmboCutSheets.Name = "cmboCutSheets";
            this.cmboCutSheets.Size = new System.Drawing.Size(244, 21);
            this.cmboCutSheets.TabIndex = 0;
            this.cmboCutSheets.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheets_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Cut Sheets";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radWorkInProgress);
            this.groupBox1.Controls.Add(this.radWorkComplete);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(54, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 307);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(482, 215);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(517, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radWorkComplete
            // 
            this.radWorkComplete.AutoSize = true;
            this.radWorkComplete.Location = new System.Drawing.Point(140, 20);
            this.radWorkComplete.Name = "radWorkComplete";
            this.radWorkComplete.Size = new System.Drawing.Size(98, 17);
            this.radWorkComplete.TabIndex = 1;
            this.radWorkComplete.TabStop = true;
            this.radWorkComplete.Text = "Work Complete";
            this.radWorkComplete.UseVisualStyleBackColor = true;
            this.radWorkComplete.CheckedChanged += new System.EventHandler(this.radWorkComplete_CheckedChanged);
            // 
            // radWorkInProgress
            // 
            this.radWorkInProgress.AutoSize = true;
            this.radWorkInProgress.Location = new System.Drawing.Point(306, 20);
            this.radWorkInProgress.Name = "radWorkInProgress";
            this.radWorkInProgress.Size = new System.Drawing.Size(107, 17);
            this.radWorkInProgress.TabIndex = 2;
            this.radWorkInProgress.TabStop = true;
            this.radWorkInProgress.Text = "Work In Progress";
            this.radWorkInProgress.UseVisualStyleBackColor = true;
            this.radWorkInProgress.CheckedChanged += new System.EventHandler(this.radWorkInProgress_CheckedChanged);
            // 
            // frmCutSheetDownSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 475);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboCutSheets);
            this.Name = "frmCutSheetDownSize";
            this.Text = "CMT Department Cut Sheet Downsizing";
            this.Load += new System.EventHandler(this.frmCutSheetDownSize_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboCutSheets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radWorkInProgress;
        private System.Windows.Forms.RadioButton radWorkComplete;
    }
}