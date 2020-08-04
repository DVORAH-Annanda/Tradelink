namespace CMT
{
    partial class frmTruckLoading
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
            this.cmboCurrentPI = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtpLoadDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbReprint = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboDepartments = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Panel Issue ";
            // 
            // cmboCurrentPI
            // 
            this.cmboCurrentPI.FormattingEnabled = true;
            this.cmboCurrentPI.Location = new System.Drawing.Point(236, 79);
            this.cmboCurrentPI.Name = "cmboCurrentPI";
            this.cmboCurrentPI.Size = new System.Drawing.Size(170, 21);
            this.cmboCurrentPI.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(531, 320);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtpLoadDate
            // 
            this.dtpLoadDate.Location = new System.Drawing.Point(236, 128);
            this.dtpLoadDate.Name = "dtpLoadDate";
            this.dtpLoadDate.Size = new System.Drawing.Size(170, 20);
            this.dtpLoadDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Load Date";
            // 
            // cbReprint
            // 
            this.cbReprint.AutoSize = true;
            this.cbReprint.Location = new System.Drawing.Point(236, 207);
            this.cbReprint.Name = "cbReprint";
            this.cbReprint.Size = new System.Drawing.Size(60, 17);
            this.cbReprint.TabIndex = 5;
            this.cbReprint.Text = "Reprint";
            this.cbReprint.UseVisualStyleBackColor = true;
            this.cbReprint.CheckedChanged += new System.EventHandler(this.cbReprint_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Current Departments";
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(236, 30);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(170, 21);
            this.cmboDepartments.TabIndex = 7;
            this.cmboDepartments.SelectedIndexChanged += new System.EventHandler(this.cmboDepartments_SelectedIndexChanged);
            // 
            // frmTruckLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 355);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbReprint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpLoadDate);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cmboCurrentPI);
            this.Controls.Add(this.label1);
            this.Name = "frmTruckLoading";
            this.Text = "Truck Loading Instruction";
            this.Load += new System.EventHandler(this.frmTruckLoading_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCurrentPI;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DateTimePicker dtpLoadDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbReprint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboDepartments;
    }
}