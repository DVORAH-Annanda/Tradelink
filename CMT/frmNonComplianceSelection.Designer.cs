namespace CMT
{
    partial class frmNonComplianceSelection
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmbONonCompliance = new CMT.CheckComboBox();
            this.cmboCutSheets = new CMT.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select CutSheets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select By Non Compliance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(357, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(131, 20);
            this.dtpFromDate.TabIndex = 4;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(357, 52);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(131, 20);
            this.dtpToDate.TabIndex = 5;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(679, 383);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbONonCompliance
            // 
            this.cmbONonCompliance.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbONonCompliance.FormattingEnabled = true;
            this.cmbONonCompliance.Location = new System.Drawing.Point(357, 145);
            this.cmbONonCompliance.Name = "cmbONonCompliance";
            this.cmbONonCompliance.Size = new System.Drawing.Size(185, 21);
            this.cmbONonCompliance.TabIndex = 9;
            this.cmbONonCompliance.Text = "Select Options";
            this.cmbONonCompliance.SelectedIndexChanged += new System.EventHandler(this.cmbONonCompliance_SelectedIndexChanged);
            // 
            // cmboCutSheets
            // 
            this.cmboCutSheets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCutSheets.FormattingEnabled = true;
            this.cmboCutSheets.Location = new System.Drawing.Point(357, 98);
            this.cmboCutSheets.Name = "cmboCutSheets";
            this.cmboCutSheets.Size = new System.Drawing.Size(185, 21);
            this.cmboCutSheets.TabIndex = 6;
            this.cmboCutSheets.Text = "Select Options";
            this.cmboCutSheets.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheets_SelectedIndexChanged);
            // 
            // frmNonComplianceSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbONonCompliance);
            this.Controls.Add(this.cmboCutSheets);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmNonComplianceSelection";
            this.Text = "CMT Non Compliance Selection ";
            this.Load += new System.EventHandler(this.frmNonComplianceSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private CMT.CheckComboBox cmboCutSheets;
        private CMT.CheckComboBox cmbONonCompliance;
        private System.Windows.Forms.Button btnSubmit;
    }
}