namespace Knitting
{
    partial class frmWhiteOnly
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
            this.rbWhiteYes = new System.Windows.Forms.RadioButton();
            this.rbWhiteOnly_No = new System.Windows.Forms.RadioButton();
            this.btnClosre = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWhiteOnly_No);
            this.groupBox1.Controls.Add(this.rbWhiteYes);
            this.groupBox1.Location = new System.Drawing.Point(117, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbWhiteYes
            // 
            this.rbWhiteYes.AutoSize = true;
            this.rbWhiteYes.Location = new System.Drawing.Point(87, 32);
            this.rbWhiteYes.Name = "rbWhiteYes";
            this.rbWhiteYes.Size = new System.Drawing.Size(104, 17);
            this.rbWhiteYes.TabIndex = 0;
            this.rbWhiteYes.Text = "White Only - Yes";
            this.rbWhiteYes.UseVisualStyleBackColor = true;
            this.rbWhiteYes.CheckedChanged += new System.EventHandler(this.rbWhiteYes_CheckedChanged);
            // 
            // rbWhiteOnly_No
            // 
            this.rbWhiteOnly_No.AutoSize = true;
            this.rbWhiteOnly_No.Checked = true;
            this.rbWhiteOnly_No.Location = new System.Drawing.Point(87, 67);
            this.rbWhiteOnly_No.Name = "rbWhiteOnly_No";
            this.rbWhiteOnly_No.Size = new System.Drawing.Size(100, 17);
            this.rbWhiteOnly_No.TabIndex = 1;
            this.rbWhiteOnly_No.TabStop = true;
            this.rbWhiteOnly_No.Text = "White Only - No";
            this.rbWhiteOnly_No.UseVisualStyleBackColor = true;
            // 
            // btnClosre
            // 
            this.btnClosre.Location = new System.Drawing.Point(438, 221);
            this.btnClosre.Name = "btnClosre";
            this.btnClosre.Size = new System.Drawing.Size(75, 23);
            this.btnClosre.TabIndex = 1;
            this.btnClosre.Text = "Close";
            this.btnClosre.UseVisualStyleBackColor = true;
            this.btnClosre.Click += new System.EventHandler(this.btnClosre_Click);
            // 
            // frmWhiteOnly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 256);
            this.Controls.Add(this.btnClosre);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWhiteOnly";
            this.Text = "Is this Piece White Only ?";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWhiteYes;
        private System.Windows.Forms.RadioButton rbWhiteOnly_No;
        private System.Windows.Forms.Button btnClosre;
    }
}