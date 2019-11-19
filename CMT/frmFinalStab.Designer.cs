namespace CMT
{
    partial class frmFinalStab
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCMTLineNumber = new System.Windows.Forms.TextBox();
            this.txtPanelIssue = new System.Windows.Forms.TextBox();
            this.txtRecordedBy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheet Number";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(296, 39);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CMT Line Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Panel Issue Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Record By";
            // 
            // txtCMTLineNumber
            // 
            this.txtCMTLineNumber.Location = new System.Drawing.Point(296, 88);
            this.txtCMTLineNumber.Name = "txtCMTLineNumber";
            this.txtCMTLineNumber.ReadOnly = true;
            this.txtCMTLineNumber.Size = new System.Drawing.Size(157, 20);
            this.txtCMTLineNumber.TabIndex = 5;
            // 
            // txtPanelIssue
            // 
            this.txtPanelIssue.Location = new System.Drawing.Point(296, 122);
            this.txtPanelIssue.Name = "txtPanelIssue";
            this.txtPanelIssue.ReadOnly = true;
            this.txtPanelIssue.Size = new System.Drawing.Size(157, 20);
            this.txtPanelIssue.TabIndex = 6;
            // 
            // txtRecordedBy
            // 
            this.txtRecordedBy.Location = new System.Drawing.Point(296, 169);
            this.txtRecordedBy.Name = "txtRecordedBy";
            this.txtRecordedBy.Size = new System.Drawing.Size(238, 20);
            this.txtRecordedBy.TabIndex = 7;
            // 
            // frmFinalStab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 408);
            this.Controls.Add(this.txtRecordedBy);
            this.Controls.Add(this.txtPanelIssue);
            this.Controls.Add(this.txtCMTLineNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmFinalStab";
            this.Text = "Final Stability Check";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCMTLineNumber;
        private System.Windows.Forms.TextBox txtPanelIssue;
        private System.Windows.Forms.TextBox txtRecordedBy;
    }
}