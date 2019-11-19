namespace Spinning
{
    partial class frmDQControlReport
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
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboCMMachines = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbYarnOrder = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cmboSPMachines = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RSBdataGridView3 = new System.Windows.Forms.DataGridView();
            this.cmboRSBMachines = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSBdataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.Location = new System.Drawing.Point(227, 12);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(156, 20);
            this.dtpReportDate.TabIndex = 0;
            this.dtpReportDate.ValueChanged += new System.EventHandler(this.dtpReportDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Select a Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.cmboCMMachines);
            this.groupBox1.Location = new System.Drawing.Point(197, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 241);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Card Measurments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please Select a machine";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(39, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(504, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // cmboCMMachines
            // 
            this.cmboCMMachines.FormattingEnabled = true;
            this.cmboCMMachines.Location = new System.Drawing.Point(143, 19);
            this.cmboCMMachines.Name = "cmboCMMachines";
            this.cmboCMMachines.Size = new System.Drawing.Size(235, 21);
            this.cmboCMMachines.TabIndex = 0;
            this.cmboCMMachines.SelectedIndexChanged += new System.EventHandler(this.cmboMachines_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbYarnOrder);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.cmboSPMachines);
            this.groupBox2.Location = new System.Drawing.Point(197, 596);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 288);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spinning Machine Measurements";
            // 
            // cmbYarnOrder
            // 
            this.cmbYarnOrder.FormattingEnabled = true;
            this.cmbYarnOrder.Location = new System.Drawing.Point(198, 56);
            this.cmbYarnOrder.Name = "cmbYarnOrder";
            this.cmbYarnOrder.Size = new System.Drawing.Size(210, 21);
            this.cmbYarnOrder.TabIndex = 5;
            this.cmbYarnOrder.SelectedIndexChanged += new System.EventHandler(this.cmbYarnOrder_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Please select an yarn order no";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Please Select a machine";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(39, 115);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(504, 150);
            this.dataGridView2.TabIndex = 1;
            // 
            // cmboSPMachines
            // 
            this.cmboSPMachines.FormattingEnabled = true;
            this.cmboSPMachines.Location = new System.Drawing.Point(198, 19);
            this.cmboSPMachines.Name = "cmboSPMachines";
            this.cmboSPMachines.Size = new System.Drawing.Size(210, 21);
            this.cmboSPMachines.TabIndex = 0;
            this.cmboSPMachines.SelectedIndexChanged += new System.EventHandler(this.cmbMachines2_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(795, 870);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(795, 838);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(75, 23);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.RSBdataGridView3);
            this.groupBox3.Controls.Add(this.cmboRSBMachines);
            this.groupBox3.Location = new System.Drawing.Point(197, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(573, 237);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RSB Measurements";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Please Select a machine";
            // 
            // RSBdataGridView3
            // 
            this.RSBdataGridView3.AllowUserToAddRows = false;
            this.RSBdataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RSBdataGridView3.Location = new System.Drawing.Point(39, 57);
            this.RSBdataGridView3.Name = "RSBdataGridView3";
            this.RSBdataGridView3.Size = new System.Drawing.Size(504, 150);
            this.RSBdataGridView3.TabIndex = 1;
            // 
            // cmboRSBMachines
            // 
            this.cmboRSBMachines.FormattingEnabled = true;
            this.cmboRSBMachines.Location = new System.Drawing.Point(143, 18);
            this.cmboRSBMachines.Name = "cmboRSBMachines";
            this.cmboRSBMachines.Size = new System.Drawing.Size(235, 21);
            this.cmboRSBMachines.TabIndex = 0;
            this.cmboRSBMachines.SelectedIndexChanged += new System.EventHandler(this.cmboRSBMachines_SelectedIndexChanged);
            // 
            // frmDQControlReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 908);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpReportDate);
            this.Name = "frmDQControlReport";
            this.Text = "QA Daily Quality Control Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RSBdataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboCMMachines;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox cmboSPMachines;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.ComboBox cmbYarnOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView RSBdataGridView3;
        private System.Windows.Forms.ComboBox cmboRSBMachines;
    }
}