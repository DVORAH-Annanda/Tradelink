namespace Cutting
{
    partial class frmQASelection
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
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboInspector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheets";
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(198, 31);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(142, 21);
            this.cmboCutSheet.TabIndex = 1;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(96, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(379, 222);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cmboInspector
            // 
            this.cmboInspector.FormattingEnabled = true;
            this.cmboInspector.Location = new System.Drawing.Point(198, 81);
            this.cmboInspector.Name = "cmboInspector";
            this.cmboInspector.Size = new System.Drawing.Size(142, 21);
            this.cmboInspector.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Inspector";
            // 
            // frmQASelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 423);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboInspector);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboCutSheet);
            this.Controls.Add(this.label1);
            this.Name = "frmQASelection";
            this.Text = "QA Results Update / Edit";
            this.Load += new System.EventHandler(this.frmQASelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboInspector;
        private System.Windows.Forms.Label label2;
    }
}