namespace ProductionPlanning
{
    partial class frmInterDeptFaults
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.rbCutSheet = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboInterDepartmentalOption = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboInterDepartmentalOption);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCutSheet);
            this.groupBox1.Controls.Add(this.rbCutSheet);
            this.groupBox1.Location = new System.Drawing.Point(128, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(290, 103);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.Size = new System.Drawing.Size(202, 20);
            this.txtCutSheet.TabIndex = 7;
            // 
            // rbCutSheet
            // 
            this.rbCutSheet.AutoSize = true;
            this.rbCutSheet.Checked = true;
            this.rbCutSheet.Location = new System.Drawing.Point(66, 104);
            this.rbCutSheet.Name = "rbCutSheet";
            this.rbCutSheet.Size = new System.Drawing.Size(72, 17);
            this.rbCutSheet.TabIndex = 0;
            this.rbCutSheet.TabStop = true;
            this.rbCutSheet.Text = "Cut Sheet";
            this.rbCutSheet.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(701, 415);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Interdepartmental Comparioson Option";
            // 
            // cmboInterDepartmentalOption
            // 
            this.cmboInterDepartmentalOption.FormattingEnabled = true;
            this.cmboInterDepartmentalOption.Location = new System.Drawing.Point(290, 38);
            this.cmboInterDepartmentalOption.Name = "cmboInterDepartmentalOption";
            this.cmboInterDepartmentalOption.Size = new System.Drawing.Size(202, 21);
            this.cmboInterDepartmentalOption.TabIndex = 9;
            // 
            // frmInterDeptFaults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInterDeptFaults";
            this.Text = "Inter Departmental Faults Analysis ";
            this.Load += new System.EventHandler(this.frmInterDeptFaults_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.RadioButton rbCutSheet;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmboInterDepartmentalOption;
        private System.Windows.Forms.Label label1;
    }
}