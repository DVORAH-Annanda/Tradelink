namespace CMT
{
    partial class frmCostingProfitability
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboStyle = new CMT.CheckComboBox();
            this.cmboColour = new CMT.CheckComboBox();
            this.cmboSizes = new CMT.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboCMTs = new CMT.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(196, 26);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(118, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(196, 71);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(118, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Style";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Colour";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Size";
            // 
            // cmboStyle
            // 
            this.cmboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(196, 172);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(206, 21);
            this.cmboStyle.TabIndex = 7;
            this.cmboStyle.Text = "Select Options";
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(199, 222);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(203, 21);
            this.cmboColour.TabIndex = 8;
            this.cmboColour.Text = "Select Options";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(199, 272);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(203, 21);
            this.cmboSizes.TabIndex = 9;
            this.cmboSizes.Text = "Select Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(465, 367);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(135, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "CMT";
            // 
            // cmboCMTs
            // 
            this.cmboCMTs.FormattingEnabled = true;
            this.cmboCMTs.Location = new System.Drawing.Point(199, 122);
            this.cmboCMTs.Name = "cmboCMTs";
            this.cmboCMTs.Size = new System.Drawing.Size(203, 21);
            this.cmboCMTs.TabIndex = 12;
            // 
            // frmCostingProfitability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 425);
            this.Controls.Add(this.cmboCMTs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmCostingProfitability";
            this.Text = "Costing Profitability";
            this.Load += new System.EventHandler(this.frmCostingProfitability_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private /*System.Windows.Forms.ComboBox*/  CMT.CheckComboBox cmboStyle;
        private /*System.Windows.Forms.ComboBox*/ CMT.CheckComboBox cmboColour;
        private /*System.Windows.Forms.ComboBox*/ CMT.CheckComboBox cmboSizes;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label6;
        //private System.Windows.Forms.ComboBox cmboCMTs;
        private CMT.CheckComboBox cmboCMTs;
    }
}