namespace Cutting
{
    partial class frmCloseCutSheet
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
            this.dtpCloseDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbReOpenCutSheet = new System.Windows.Forms.RadioButton();
            this.rbCloseCutSheet = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Closure Date";
            // 
            // dtpCloseDate
            // 
            this.dtpCloseDate.Location = new System.Drawing.Point(318, 29);
            this.dtpCloseDate.Name = "dtpCloseDate";
            this.dtpCloseDate.Size = new System.Drawing.Size(133, 20);
            this.dtpCloseDate.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(138, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(481, 268);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(651, 453);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbReOpenCutSheet);
            this.groupBox1.Controls.Add(this.rbCloseCutSheet);
            this.groupBox1.Location = new System.Drawing.Point(214, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 46);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // rbReOpenCutSheet
            // 
            this.rbReOpenCutSheet.AutoSize = true;
            this.rbReOpenCutSheet.Location = new System.Drawing.Point(171, 19);
            this.rbReOpenCutSheet.Name = "rbReOpenCutSheet";
            this.rbReOpenCutSheet.Size = new System.Drawing.Size(115, 17);
            this.rbReOpenCutSheet.TabIndex = 1;
            this.rbReOpenCutSheet.Text = "ReOpen Cut Sheet";
            this.rbReOpenCutSheet.UseVisualStyleBackColor = true;
            this.rbReOpenCutSheet.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbCloseCutSheet
            // 
            this.rbCloseCutSheet.AutoSize = true;
            this.rbCloseCutSheet.Checked = true;
            this.rbCloseCutSheet.Location = new System.Drawing.Point(21, 19);
            this.rbCloseCutSheet.Name = "rbCloseCutSheet";
            this.rbCloseCutSheet.Size = new System.Drawing.Size(101, 17);
            this.rbCloseCutSheet.TabIndex = 0;
            this.rbCloseCutSheet.TabStop = true;
            this.rbCloseCutSheet.Text = "Close Cut Sheet";
            this.rbCloseCutSheet.UseVisualStyleBackColor = true;
            this.rbCloseCutSheet.CheckedChanged += new System.EventHandler(this.rbCloseCutSheet_CheckedChanged);
            // 
            // frmCloseCutSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 488);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dtpCloseDate);
            this.Controls.Add(this.label1);
            this.Name = "frmCloseCutSheet";
            this.Text = "Close / Reopen a Cut Sheet";
            this.Load += new System.EventHandler(this.frmCloseCutSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpCloseDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbReOpenCutSheet;
        private System.Windows.Forms.RadioButton rbCloseCutSheet;
    }
}