namespace CMT
{
    partial class frmCMTPanelIssue
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
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCMTDepartment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPanels = new System.Windows.Forms.TextBox();
            this.txtKidsBoxes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdultsBoxes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmboCurrentPI = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboDepartments = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTotalBoxes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(288, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(288, 111);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(135, 20);
            this.dtpTransDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transaction Date";
            // 
            // cmboCMTDepartment
            // 
            this.cmboCMTDepartment.FormattingEnabled = true;
            this.cmboCMTDepartment.Location = new System.Drawing.Point(288, 75);
            this.cmboCMTDepartment.Name = "cmboCMTDepartment";
            this.cmboCMTDepartment.Size = new System.Drawing.Size(183, 21);
            this.cmboCMTDepartment.TabIndex = 3;
            this.cmboCMTDepartment.SelectedIndexChanged += new System.EventHandler(this.cmboDepartment_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Issue To";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 188);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(532, 224);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(540, 597);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Panel Issue Number";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTotalBoxes);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtKidsBoxes);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPanels);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAdultsBoxes);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(57, 433);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 135);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Totals";
            // 
            // txtPanels
            // 
            this.txtPanels.Location = new System.Drawing.Point(360, 51);
            this.txtPanels.Name = "txtPanels";
            this.txtPanels.ReadOnly = true;
            this.txtPanels.Size = new System.Drawing.Size(100, 20);
            this.txtPanels.TabIndex = 5;
            // 
            // txtKidsBoxes
            // 
            this.txtKidsBoxes.Location = new System.Drawing.Point(142, 51);
            this.txtKidsBoxes.Name = "txtKidsBoxes";
            this.txtKidsBoxes.ReadOnly = true;
            this.txtKidsBoxes.Size = new System.Drawing.Size(100, 20);
            this.txtKidsBoxes.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Panels";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Kids Boxes";
            // 
            // txtAdultsBoxes
            // 
            this.txtAdultsBoxes.Location = new System.Drawing.Point(142, 25);
            this.txtAdultsBoxes.Name = "txtAdultsBoxes";
            this.txtAdultsBoxes.ReadOnly = true;
            this.txtAdultsBoxes.Size = new System.Drawing.Size(100, 20);
            this.txtAdultsBoxes.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Adults Boxes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(173, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Current Issues";
            // 
            // cmboCurrentPI
            // 
            this.cmboCurrentPI.FormattingEnabled = true;
            this.cmboCurrentPI.Location = new System.Drawing.Point(288, 153);
            this.cmboCurrentPI.Name = "cmboCurrentPI";
            this.cmboCurrentPI.Size = new System.Drawing.Size(121, 21);
            this.cmboCurrentPI.TabIndex = 11;
            this.cmboCurrentPI.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentPI_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(176, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Panel Stores";
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(288, 35);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(183, 21);
            this.cmboDepartments.TabIndex = 13;
            this.cmboDepartments.SelectedIndexChanged += new System.EventHandler(this.cmboDepartments_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Total Boxes";
            // 
            // txtTotalBoxes
            // 
            this.txtTotalBoxes.Location = new System.Drawing.Point(142, 88);
            this.txtTotalBoxes.Name = "txtTotalBoxes";
            this.txtTotalBoxes.ReadOnly = true;
            this.txtTotalBoxes.Size = new System.Drawing.Size(100, 20);
            this.txtTotalBoxes.TabIndex = 18;
            // 
            // frmCMTPanelIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 632);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmboCurrentPI);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboCMTDepartment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label1);
            this.Name = "frmCMTPanelIssue";
            this.Text = "Panel Issue Selection";
            this.Load += new System.EventHandler(this.frmCMTPanelIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCMTDepartment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPanels;
        private System.Windows.Forms.TextBox txtKidsBoxes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdultsBoxes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboCurrentPI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmboDepartments;
        private System.Windows.Forms.TextBox txtTotalBoxes;
        private System.Windows.Forms.Label label12;
    }
}