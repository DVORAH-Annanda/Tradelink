namespace TTI2_WF
{
    partial class frmQASDaysDelay
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
            this.chkCMT = new System.Windows.Forms.CheckBox();
            this.chkCutting = new System.Windows.Forms.CheckBox();
            this.chkDyePrep = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkWhse = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWhse);
            this.groupBox1.Controls.Add(this.chkCMT);
            this.groupBox1.Controls.Add(this.chkCutting);
            this.groupBox1.Controls.Add(this.chkDyePrep);
            this.groupBox1.Location = new System.Drawing.Point(120, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Departments";
            // 
            // chkCMT
            // 
            this.chkCMT.AutoSize = true;
            this.chkCMT.Location = new System.Drawing.Point(83, 161);
            this.chkCMT.Name = "chkCMT";
            this.chkCMT.Size = new System.Drawing.Size(52, 17);
            this.chkCMT.TabIndex = 4;
            this.chkCMT.Text = "CMT ";
            this.chkCMT.UseVisualStyleBackColor = true;
            // 
            // chkCutting
            // 
            this.chkCutting.AutoSize = true;
            this.chkCutting.Location = new System.Drawing.Point(83, 101);
            this.chkCutting.Name = "chkCutting";
            this.chkCutting.Size = new System.Drawing.Size(59, 17);
            this.chkCutting.TabIndex = 3;
            this.chkCutting.Text = "Cutting";
            this.chkCutting.UseVisualStyleBackColor = true;
            // 
            // chkDyePrep
            // 
            this.chkDyePrep.AutoSize = true;
            this.chkDyePrep.Location = new System.Drawing.Point(83, 41);
            this.chkDyePrep.Name = "chkDyePrep";
            this.chkDyePrep.Size = new System.Drawing.Size(79, 17);
            this.chkDyePrep.TabIndex = 0;
            this.chkDyePrep.Text = "Dye House";
            this.chkDyePrep.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(217, 20);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 20);
            this.dtpFromDate.TabIndex = 3;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(217, 54);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 20);
            this.dtpToDate.TabIndex = 4;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(474, 425);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkWhse
            // 
            this.chkWhse.AutoSize = true;
            this.chkWhse.Location = new System.Drawing.Point(83, 221);
            this.chkWhse.Name = "chkWhse";
            this.chkWhse.Size = new System.Drawing.Size(112, 17);
            this.chkWhse.TabIndex = 5;
            this.chkWhse.Text = "WareHouse Sales";
            this.chkWhse.UseVisualStyleBackColor = true;
            // 
            // frmQASDaysDelay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 473);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQASDaysDelay";
            this.Text = "Production Days By Department";
            this.Load += new System.EventHandler(this.frmQASDaysDelay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCMT;
        private System.Windows.Forms.CheckBox chkCutting;
        private System.Windows.Forms.CheckBox chkDyePrep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkWhse;
    }
}