namespace Cutting
{
    partial class frmCMTPanelReissue
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
            this.cmboPanelIssue = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.Reprint = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCutSheetNo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Truck Loads";
            // 
            // cmboPanelIssue
            // 
            this.cmboPanelIssue.FormattingEnabled = true;
            this.cmboPanelIssue.Location = new System.Drawing.Point(223, 32);
            this.cmboPanelIssue.Name = "cmboPanelIssue";
            this.cmboPanelIssue.Size = new System.Drawing.Size(228, 21);
            this.cmboPanelIssue.TabIndex = 1;
            this.cmboPanelIssue.SelectedIndexChanged += new System.EventHandler(this.cmboPanelIssue_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(77, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(374, 150);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(376, 361);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Reprint
            // 
            this.Reprint.AutoSize = true;
            this.Reprint.Location = new System.Drawing.Point(223, 81);
            this.Reprint.Name = "Reprint";
            this.Reprint.Size = new System.Drawing.Size(60, 17);
            this.Reprint.TabIndex = 4;
            this.Reprint.Text = "Reprint";
            this.Reprint.UseVisualStyleBackColor = true;
            this.Reprint.CheckedChanged += new System.EventHandler(this.Reprint_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cut Sheet Number";
            // 
            // txtCutSheetNo
            // 
            this.txtCutSheetNo.Location = new System.Drawing.Point(223, 309);
            this.txtCutSheetNo.Name = "txtCutSheetNo";
            this.txtCutSheetNo.Size = new System.Drawing.Size(154, 20);
            this.txtCutSheetNo.TabIndex = 6;
            // 
            // frmCMTPanelReissue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 430);
            this.Controls.Add(this.txtCutSheetNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Reprint);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboPanelIssue);
            this.Controls.Add(this.label1);
            this.Name = "frmCMTPanelReissue";
            this.Text = "CMT Panel Issue Reprint";
            this.Load += new System.EventHandler(this.frmCMTPanelReissue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboPanelIssue;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox Reprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCutSheetNo;
    }
}