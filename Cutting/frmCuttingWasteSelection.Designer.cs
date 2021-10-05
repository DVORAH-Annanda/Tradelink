
namespace Cutting
{
    partial class frmCuttingWasteSelection
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
            this.label5 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.comboCutSheet = new Cutting.CheckComboBox();
            this.comboQuality = new Cutting.CheckComboBox();
            this.comboMachines = new Cutting.CheckComboBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(377, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(132, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(377, 57);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(132, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cut Sheet Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Machine";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(696, 373);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // comboCutSheet
            // 
            this.comboCutSheet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCutSheet.FormattingEnabled = true;
            this.comboCutSheet.Location = new System.Drawing.Point(377, 125);
            this.comboCutSheet.Name = "comboCutSheet";
            this.comboCutSheet.Size = new System.Drawing.Size(132, 21);
            this.comboCutSheet.TabIndex = 8;
            this.comboCutSheet.Text = "Select Options";
            // 
            // comboQuality
            // 
            this.comboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboQuality.FormattingEnabled = true;
            this.comboQuality.Location = new System.Drawing.Point(377, 197);
            this.comboQuality.Name = "comboQuality";
            this.comboQuality.Size = new System.Drawing.Size(132, 21);
            this.comboQuality.TabIndex = 9;
            this.comboQuality.Text = "Select Options";
            // 
            // comboMachines
            // 
            this.comboMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboMachines.FormattingEnabled = true;
            this.comboMachines.Location = new System.Drawing.Point(377, 269);
            this.comboMachines.Name = "comboMachines";
            this.comboMachines.Size = new System.Drawing.Size(132, 21);
            this.comboMachines.TabIndex = 10;
            this.comboMachines.Text = "Select Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(377, 348);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(161, 21);
            this.cmboReportOptions.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Report Options";
            // 
            // frmCuttingWasteSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.comboMachines);
            this.Controls.Add(this.comboQuality);
            this.Controls.Add(this.comboCutSheet);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCuttingWasteSelection";
            this.Text = "Cutting Waste Selection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCuttingWasteSelection_FormClosing);
            this.Load += new System.EventHandler(this.frmCuttingWasteSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSubmit;
        private Cutting.CheckComboBox comboCutSheet;
        private Cutting.CheckComboBox comboQuality;
        private Cutting.CheckComboBox comboMachines;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label6;
    }
}