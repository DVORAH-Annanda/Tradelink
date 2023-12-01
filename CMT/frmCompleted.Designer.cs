namespace CMT
{
    partial class frmCompleted
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
            this.cmboCMTIssue = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoBoxes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboCMTLine = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDateRequired = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboBoxType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtDifference = new System.Windows.Forms.TextBox();
            this.txtTPPanels = new System.Windows.Forms.TextBox();
            this.txtTotBGrade = new System.Windows.Forms.TextBox();
            this.txtTotAGrade = new System.Windows.Forms.TextBox();
            this.txtTPIssued = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CMT Issue";
            // 
            // cmboCMTIssue
            // 
            this.cmboCMTIssue.FormattingEnabled = true;
            this.cmboCMTIssue.Location = new System.Drawing.Point(87, 19);
            this.cmboCMTIssue.Name = "cmboCMTIssue";
            this.cmboCMTIssue.Size = new System.Drawing.Size(121, 21);
            this.cmboCMTIssue.TabIndex = 1;
            this.cmboCMTIssue.SelectedIndexChanged += new System.EventHandler(this.cmboCMTIssue_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNoBoxes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpTransDate);
            this.groupBox1.Controls.Add(this.cmboCMTIssue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 61);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtNoBoxes
            // 
            this.txtNoBoxes.Location = new System.Drawing.Point(552, 12);
            this.txtNoBoxes.Name = "txtNoBoxes";
            this.txtNoBoxes.ReadOnly = true;
            this.txtNoBoxes.Size = new System.Drawing.Size(100, 20);
            this.txtNoBoxes.TabIndex = 5;
            this.txtNoBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "No of Boxes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Transaction Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(334, 16);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(121, 20);
            this.dtpTransDate.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(560, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // cmboCMTLine
            // 
            this.cmboCMTLine.FormattingEnabled = true;
            this.cmboCMTLine.Location = new System.Drawing.Point(103, 24);
            this.cmboCMTLine.Name = "cmboCMTLine";
            this.cmboCMTLine.Size = new System.Drawing.Size(167, 21);
            this.cmboCMTLine.TabIndex = 4;
            this.cmboCMTLine.SelectedIndexChanged += new System.EventHandler(this.cmboCMTLine_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "CMT Line";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDateRequired);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCutSheet);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmboCMTLine);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(16, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 149);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // txtDateRequired
            // 
            this.txtDateRequired.Location = new System.Drawing.Point(103, 102);
            this.txtDateRequired.Name = "txtDateRequired";
            this.txtDateRequired.ReadOnly = true;
            this.txtDateRequired.Size = new System.Drawing.Size(167, 20);
            this.txtDateRequired.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Required";
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(103, 65);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.ReadOnly = true;
            this.txtCutSheet.Size = new System.Drawing.Size(167, 20);
            this.txtCutSheet.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Cut Sheet";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(324, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(464, 199);
            this.dataGridView1.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmboBoxType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(12, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(810, 248);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // cmboBoxType
            // 
            this.cmboBoxType.FormattingEnabled = true;
            this.cmboBoxType.Location = new System.Drawing.Point(119, 189);
            this.cmboBoxType.Name = "cmboBoxType";
            this.cmboBoxType.Size = new System.Drawing.Size(176, 21);
            this.cmboBoxType.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Box Type";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView3);
            this.groupBox4.Controls.Add(this.dataGridView2);
            this.groupBox4.Location = new System.Drawing.Point(12, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(923, 212);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(562, 19);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(342, 176);
            this.dataGridView3.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(22, 19);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(496, 176);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView2_CellBeginEdit);
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEnter);
            this.dataGridView2.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellLeave);
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView2_CellValidating);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            this.dataGridView2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyUp);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtDifference);
            this.groupBox5.Controls.Add(this.txtTPPanels);
            this.groupBox5.Controls.Add(this.txtTotBGrade);
            this.groupBox5.Controls.Add(this.txtTotAGrade);
            this.groupBox5.Controls.Add(this.txtTPIssued);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(148, 592);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(422, 153);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            // 
            // txtDifference
            // 
            this.txtDifference.Location = new System.Drawing.Point(174, 117);
            this.txtDifference.Name = "txtDifference";
            this.txtDifference.ReadOnly = true;
            this.txtDifference.Size = new System.Drawing.Size(100, 20);
            this.txtDifference.TabIndex = 9;
            this.txtDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTPPanels
            // 
            this.txtTPPanels.Location = new System.Drawing.Point(174, 91);
            this.txtTPPanels.Name = "txtTPPanels";
            this.txtTPPanels.ReadOnly = true;
            this.txtTPPanels.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTPPanels.Size = new System.Drawing.Size(100, 20);
            this.txtTPPanels.TabIndex = 8;
            this.txtTPPanels.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTPPanels.TextChanged += new System.EventHandler(this.txtTPPanels_TextChanged);
            // 
            // txtTotBGrade
            // 
            this.txtTotBGrade.Location = new System.Drawing.Point(174, 65);
            this.txtTotBGrade.Name = "txtTotBGrade";
            this.txtTotBGrade.ReadOnly = true;
            this.txtTotBGrade.Size = new System.Drawing.Size(100, 20);
            this.txtTotBGrade.TabIndex = 7;
            this.txtTotBGrade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotAGrade
            // 
            this.txtTotAGrade.Location = new System.Drawing.Point(174, 39);
            this.txtTotAGrade.Name = "txtTotAGrade";
            this.txtTotAGrade.ReadOnly = true;
            this.txtTotAGrade.Size = new System.Drawing.Size(100, 20);
            this.txtTotAGrade.TabIndex = 6;
            this.txtTotAGrade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTPIssued
            // 
            this.txtTPIssued.Location = new System.Drawing.Point(174, 13);
            this.txtTPIssued.Name = "txtTPIssued";
            this.txtTPIssued.ReadOnly = true;
            this.txtTPIssued.Size = new System.Drawing.Size(100, 20);
            this.txtTPIssued.TabIndex = 5;
            this.txtTPIssued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Difference";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Total Panels";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Total B Grade";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Total A Grade";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Total panels issued";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(860, 706);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmCompleted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 757);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCompleted";
            this.Text = "Sewing lines completed Work ";
            this.Load += new System.EventHandler(this.frmCompleted_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCMTIssue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNoBoxes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboCMTLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDateRequired;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDifference;
        private System.Windows.Forms.TextBox txtTPPanels;
        private System.Windows.Forms.TextBox txtTotBGrade;
        private System.Windows.Forms.TextBox txtTotAGrade;
        private System.Windows.Forms.TextBox txtTPIssued;
        private System.Windows.Forms.ComboBox cmboBoxType;
        private System.Windows.Forms.Label label8;
    }
}