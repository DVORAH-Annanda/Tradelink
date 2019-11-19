namespace CMT
{
    partial class frmCMTWip
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
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmboLine = new CMT.CheckComboBox();
            this.cmboCMT = new CMT.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(247, 87);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(115, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(247, 133);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(115, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "CMT Facility";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Line No";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 467);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmboLine
            // 
            this.cmboLine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboLine.FormattingEnabled = true;
            this.cmboLine.Location = new System.Drawing.Point(247, 271);
            this.cmboLine.Name = "cmboLine";
            this.cmboLine.Size = new System.Drawing.Size(227, 21);
            this.cmboLine.TabIndex = 7;
            this.cmboLine.Text = "Select Options";
            this.cmboLine.SelectedIndexChanged += new System.EventHandler(this.cmboLine_SelectedIndexChanged);
            // 
            // cmboCMT
            // 
            this.cmboCMT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCMT.FormattingEnabled = true;
            this.cmboCMT.Location = new System.Drawing.Point(247, 215);
            this.cmboCMT.Name = "cmboCMT";
            this.cmboCMT.Size = new System.Drawing.Size(227, 21);
            this.cmboCMT.TabIndex = 5;
            this.cmboCMT.Text = "Select Options";
            this.cmboCMT.SelectedIndexChanged += new System.EventHandler(this.cmboCMT_SelectedIndexChanged);
            // 
            // frmCMTWip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmboLine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboCMT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCMTWip";
            this.Text = "WIP Selection";
            this.Load += new System.EventHandler(this.frmCMTWip_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
       // private System.Windows.Forms.ComboBox cmboStyle;
        private CMT.CheckComboBox cmboCMT;
        private System.Windows.Forms.Label label4;
       // private System.Windows.Forms.ComboBox cmboColour;
        private CMT.CheckComboBox cmboLine;
        private System.Windows.Forms.Button button1;
        // private System.Windows.Forms.ComboBox cmboSizes;
        
    }
}