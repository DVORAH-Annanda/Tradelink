namespace TTI2_WF
{
    partial class frmQACMTSel
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
            this.rbNCRDetails = new System.Windows.Forms.RadioButton();
            this.rbNonCompliance = new System.Windows.Forms.RadioButton();
            this.rbCutSheetsOnHold = new System.Windows.Forms.RadioButton();
            this.rbCompltedeWork = new System.Windows.Forms.RadioButton();
            this.rbProdByPeriod = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNCRDetails);
            this.groupBox1.Controls.Add(this.rbNonCompliance);
            this.groupBox1.Controls.Add(this.rbCutSheetsOnHold);
            this.groupBox1.Controls.Add(this.rbCompltedeWork);
            this.groupBox1.Controls.Add(this.rbProdByPeriod);
            this.groupBox1.Location = new System.Drawing.Point(120, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 294);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Availability";
            // 
            // rbNCRDetails
            // 
            this.rbNCRDetails.AutoSize = true;
            this.rbNCRDetails.Location = new System.Drawing.Point(57, 224);
            this.rbNCRDetails.Name = "rbNCRDetails";
            this.rbNCRDetails.Size = new System.Drawing.Size(155, 17);
            this.rbNCRDetails.TabIndex = 4;
            this.rbNCRDetails.TabStop = true;
            this.rbNCRDetails.Text = "CMT NCR details By Month";
            this.rbNCRDetails.UseVisualStyleBackColor = true;
            this.rbNCRDetails.CheckedChanged += new System.EventHandler(this.rbNCRDetails_CheckedChanged);
            // 
            // rbNonCompliance
            // 
            this.rbNonCompliance.AutoSize = true;
            this.rbNonCompliance.Location = new System.Drawing.Point(57, 182);
            this.rbNonCompliance.Name = "rbNonCompliance";
            this.rbNonCompliance.Size = new System.Drawing.Size(176, 17);
            this.rbNonCompliance.TabIndex = 3;
            this.rbNonCompliance.TabStop = true;
            this.rbNonCompliance.Text = "CMT CutSheet Non Compliance";
            this.rbNonCompliance.UseVisualStyleBackColor = true;
            this.rbNonCompliance.CheckedChanged += new System.EventHandler(this.rbNonCompliance_CheckedChanged);
            // 
            // rbCutSheetsOnHold
            // 
            this.rbCutSheetsOnHold.AutoSize = true;
            this.rbCutSheetsOnHold.Location = new System.Drawing.Point(57, 140);
            this.rbCutSheetsOnHold.Name = "rbCutSheetsOnHold";
            this.rbCutSheetsOnHold.Size = new System.Drawing.Size(119, 17);
            this.rbCutSheetsOnHold.TabIndex = 2;
            this.rbCutSheetsOnHold.TabStop = true;
            this.rbCutSheetsOnHold.Text = "Cut Sheets On Hold";
            this.rbCutSheetsOnHold.UseVisualStyleBackColor = true;
            this.rbCutSheetsOnHold.CheckedChanged += new System.EventHandler(this.rbCutSheetsOnHold_CheckedChanged);
            // 
            // rbCompltedeWork
            // 
            this.rbCompltedeWork.AutoSize = true;
            this.rbCompltedeWork.Location = new System.Drawing.Point(57, 98);
            this.rbCompltedeWork.Name = "rbCompltedeWork";
            this.rbCompltedeWork.Size = new System.Drawing.Size(145, 17);
            this.rbCompltedeWork.TabIndex = 1;
            this.rbCompltedeWork.TabStop = true;
            this.rbCompltedeWork.Text = "Completed Work Analysis";
            this.rbCompltedeWork.UseVisualStyleBackColor = true;
            this.rbCompltedeWork.CheckedChanged += new System.EventHandler(this.rbCompltedeWork_CheckedChanged);
            // 
            // rbProdByPeriod
            // 
            this.rbProdByPeriod.AutoSize = true;
            this.rbProdByPeriod.Location = new System.Drawing.Point(57, 56);
            this.rbProdByPeriod.Name = "rbProdByPeriod";
            this.rbProdByPeriod.Size = new System.Drawing.Size(159, 17);
            this.rbProdByPeriod.TabIndex = 0;
            this.rbProdByPeriod.TabStop = true;
            this.rbProdByPeriod.Text = "Production Report By Period";
            this.rbProdByPeriod.UseVisualStyleBackColor = true;
            this.rbProdByPeriod.CheckedChanged += new System.EventHandler(this.rbProdByPeriod_CheckedChanged);
            // 
            // frmQACMTSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 486);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQACMTSel";
            this.Text = "CMT Report Selection";
            this.Load += new System.EventHandler(this.frmQACMTSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNCRDetails;
        private System.Windows.Forms.RadioButton rbNonCompliance;
        private System.Windows.Forms.RadioButton rbCutSheetsOnHold;
        private System.Windows.Forms.RadioButton rbCompltedeWork;
        private System.Windows.Forms.RadioButton rbProdByPeriod;
    }
}