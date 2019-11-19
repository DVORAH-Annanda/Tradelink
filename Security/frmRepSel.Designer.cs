namespace Security
{
    partial class frmRepSel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAllRecords = new System.Windows.Forms.RadioButton();
            this.rbActiveRecords = new System.Windows.Forms.RadioButton();
            this.rbDiscontinued = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(454, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDiscontinued);
            this.groupBox1.Controls.Add(this.rbActiveRecords);
            this.groupBox1.Controls.Add(this.rbAllRecords);
            this.groupBox1.Location = new System.Drawing.Point(132, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbAllRecords
            // 
            this.rbAllRecords.AutoSize = true;
            this.rbAllRecords.Checked = true;
            this.rbAllRecords.Location = new System.Drawing.Point(84, 34);
            this.rbAllRecords.Name = "rbAllRecords";
            this.rbAllRecords.Size = new System.Drawing.Size(79, 17);
            this.rbAllRecords.TabIndex = 0;
            this.rbAllRecords.TabStop = true;
            this.rbAllRecords.Text = "All Records";
            this.rbAllRecords.UseVisualStyleBackColor = true;
            // 
            // rbActiveRecords
            // 
            this.rbActiveRecords.AutoSize = true;
            this.rbActiveRecords.Location = new System.Drawing.Point(84, 73);
            this.rbActiveRecords.Name = "rbActiveRecords";
            this.rbActiveRecords.Size = new System.Drawing.Size(98, 17);
            this.rbActiveRecords.TabIndex = 1;
            this.rbActiveRecords.Text = "Active Records";
            this.rbActiveRecords.UseVisualStyleBackColor = true;
            // 
            // rbDiscontinued
            // 
            this.rbDiscontinued.AutoSize = true;
            this.rbDiscontinued.Location = new System.Drawing.Point(84, 112);
            this.rbDiscontinued.Name = "rbDiscontinued";
            this.rbDiscontinued.Size = new System.Drawing.Size(117, 17);
            this.rbDiscontinued.TabIndex = 2;
            this.rbDiscontinued.Text = "Discontinued Users";
            this.rbDiscontinued.UseVisualStyleBackColor = true;
            // 
            // frmRepSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 398);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "frmRepSel";
            this.Text = "Report Selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDiscontinued;
        private System.Windows.Forms.RadioButton rbActiveRecords;
        private System.Windows.Forms.RadioButton rbAllRecords;
    }
}