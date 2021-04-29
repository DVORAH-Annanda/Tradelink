
namespace ProductionPlanning
{
    partial class frmInterDeptDskAnalysis
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.cmboQuality = new ProductionPlanning.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(359, 98);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(129, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(359, 156);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(129, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(697, 390);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // cmboQuality
            // 
            this.cmboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(359, 233);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(205, 21);
            this.cmboQuality.TabIndex = 5;
            this.cmboQuality.Text = "Select Options";
            this.cmboQuality.SelectedIndexChanged += new System.EventHandler(this.cmboQuality_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select Qualities";
            // 
            // frmInterDeptDskAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboQuality);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmInterDeptDskAnalysis";
            this.Text = "frmInterDeptDskAnalysis";
            this.Load += new System.EventHandler(this.frmInterDeptDskAnalysis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnProcess;
        private ProductionPlanning.CheckComboBox cmboQuality;
        private System.Windows.Forms.Label label3;
    }
}