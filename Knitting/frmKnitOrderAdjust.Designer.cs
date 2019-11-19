namespace Knitting
{
    partial class frmKnitOrderAdjust
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboGreige = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboMachine = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbResetYarnAllocated = new System.Windows.Forms.CheckBox();
            this.btnReprint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.rbClosed = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Greige Quality";
            // 
            // comboGreige
            // 
            this.comboGreige.FormattingEnabled = true;
            this.comboGreige.Location = new System.Drawing.Point(251, 90);
            this.comboGreige.Name = "comboGreige";
            this.comboGreige.Size = new System.Drawing.Size(172, 21);
            this.comboGreige.TabIndex = 2;
            this.comboGreige.SelectedIndexChanged += new System.EventHandler(this.comboGreige_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Knit order number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of Pieces";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "label5";
            // 
            // comboMachine
            // 
            this.comboMachine.FormattingEnabled = true;
            this.comboMachine.Location = new System.Drawing.Point(251, 218);
            this.comboMachine.Name = "comboMachine";
            this.comboMachine.Size = new System.Drawing.Size(169, 21);
            this.comboMachine.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Machine Details";
            // 
            // cbResetYarnAllocated
            // 
            this.cbResetYarnAllocated.AutoSize = true;
            this.cbResetYarnAllocated.Location = new System.Drawing.Point(133, 335);
            this.cbResetYarnAllocated.Name = "cbResetYarnAllocated";
            this.cbResetYarnAllocated.Size = new System.Drawing.Size(126, 17);
            this.cbResetYarnAllocated.TabIndex = 9;
            this.cbResetYarnAllocated.Text = "Reset Allocated Yarn";
            this.cbResetYarnAllocated.UseVisualStyleBackColor = true;
            this.cbResetYarnAllocated.CheckedChanged += new System.EventHandler(this.cbResetYarnAllocated_CheckedChanged);
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(359, 331);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(107, 23);
            this.btnReprint.TabIndex = 10;
            this.btnReprint.Text = "Knit Order Reprint";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbClosed);
            this.groupBox1.Controls.Add(this.rbActive);
            this.groupBox1.Location = new System.Drawing.Point(223, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 56);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Location = new System.Drawing.Point(28, 26);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(55, 17);
            this.rbActive.TabIndex = 0;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            // 
            // rbClosed
            // 
            this.rbClosed.AutoSize = true;
            this.rbClosed.Location = new System.Drawing.Point(102, 26);
            this.rbClosed.Name = "rbClosed";
            this.rbClosed.Size = new System.Drawing.Size(57, 17);
            this.rbClosed.TabIndex = 1;
            this.rbClosed.TabStop = true;
            this.rbClosed.Text = "Closed";
            this.rbClosed.UseVisualStyleBackColor = true;
            // 
            // frmKnitOrderAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 437);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.cbResetYarnAllocated);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboMachine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboGreige);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmKnitOrderAdjust";
            this.Text = "Knit Order Adjustment";
            this.Load += new System.EventHandler(this.frmKnitOrderAdjust_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboGreige;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboMachine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbResetYarnAllocated;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbClosed;
        private System.Windows.Forms.RadioButton rbActive;
    }
}