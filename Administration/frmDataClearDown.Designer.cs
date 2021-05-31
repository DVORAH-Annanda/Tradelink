namespace Administration
{
    partial class frmDataClearDown
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
            this.dtpPriorDate = new System.Windows.Forms.DateTimePicker();
            this.btnCommence = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "All records prior to this date";
            // 
            // dtpPriorDate
            // 
            this.dtpPriorDate.Location = new System.Drawing.Point(440, 58);
            this.dtpPriorDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpPriorDate.Name = "dtpPriorDate";
            this.dtpPriorDate.Size = new System.Drawing.Size(202, 26);
            this.dtpPriorDate.TabIndex = 1;
            // 
            // btnCommence
            // 
            this.btnCommence.Location = new System.Drawing.Point(802, 663);
            this.btnCommence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCommence.Name = "btnCommence";
            this.btnCommence.Size = new System.Drawing.Size(112, 35);
            this.btnCommence.TabIndex = 2;
            this.btnCommence.Text = "Commence";
            this.btnCommence.UseVisualStyleBackColor = true;
            this.btnCommence.Click += new System.EventHandler(this.btnCommence_Click);
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(213, 174);
            this.pBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(608, 35);
            this.pBar1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(213, 300);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(606, 26);
            this.textBox1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(664, 663);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDataClearDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 734);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btnCommence);
            this.Controls.Add(this.dtpPriorDate);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDataClearDown";
            this.Text = "Data Clear Down";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDataClearDown_FormClosing);
            this.Load += new System.EventHandler(this.frmDataClearDown_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpPriorDate;
        private System.Windows.Forms.Button btnCommence;
        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCancel;
    }
}