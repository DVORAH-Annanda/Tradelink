namespace DyeHouse
{
    partial class frmCommissionDyeing
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
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmboTrims = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboGreige = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRequiredDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dtpBatchDate = new System.Windows.Forms.DateTimePicker();
            this.cmboCustomer = new System.Windows.Forms.ComboBox();
            this.cmboBatches = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkWrap = new System.Windows.Forms.CheckBox();
            this.chkLabReport = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBatchKg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.rcbNotes = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbStandardMode = new System.Windows.Forms.RadioButton();
            this.rbReprocessMode = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmboColours);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmboTrims);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmboGreige);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpRequiredDate);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dtpBatchDate);
            this.groupBox1.Controls.Add(this.cmboCustomer);
            this.groupBox1.Controls.Add(this.cmboBatches);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(43, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(132, 82);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(121, 21);
            this.cmboColours.TabIndex = 27;
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(316, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Trims";
            // 
            // cmboTrims
            // 
            this.cmboTrims.FormattingEnabled = true;
            this.cmboTrims.Location = new System.Drawing.Point(406, 156);
            this.cmboTrims.Name = "cmboTrims";
            this.cmboTrims.Size = new System.Drawing.Size(121, 21);
            this.cmboTrims.TabIndex = 25;
            this.cmboTrims.SelectedIndexChanged += new System.EventHandler(this.cmboTrims_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(316, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Body Fabric";
            // 
            // cmboGreige
            // 
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(406, 118);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(121, 21);
            this.cmboGreige.TabIndex = 23;
            this.cmboGreige.SelectedIndexChanged += new System.EventHandler(this.cmboGreige_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Date Required";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Date Ordered";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(310, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Batch Date";
            // 
            // dtpRequiredDate
            // 
            this.dtpRequiredDate.Location = new System.Drawing.Point(422, 79);
            this.dtpRequiredDate.Name = "dtpRequiredDate";
            this.dtpRequiredDate.Size = new System.Drawing.Size(144, 20);
            this.dtpRequiredDate.TabIndex = 19;
            this.dtpRequiredDate.VisibleChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(422, 45);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(144, 20);
            this.dateTimePicker2.TabIndex = 18;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // dtpBatchDate
            // 
            this.dtpBatchDate.Location = new System.Drawing.Point(422, 13);
            this.dtpBatchDate.Name = "dtpBatchDate";
            this.dtpBatchDate.Size = new System.Drawing.Size(144, 20);
            this.dtpBatchDate.TabIndex = 17;
            this.dtpBatchDate.ValueChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(132, 44);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(121, 21);
            this.cmboCustomer.TabIndex = 16;
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // cmboBatches
            // 
            this.cmboBatches.FormattingEnabled = true;
            this.cmboBatches.Location = new System.Drawing.Point(132, 12);
            this.cmboBatches.Name = "cmboBatches";
            this.cmboBatches.Size = new System.Drawing.Size(121, 21);
            this.cmboBatches.TabIndex = 15;
            this.cmboBatches.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Dye Batches";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Colour";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkWrap);
            this.groupBox3.Controls.Add(this.chkLabReport);
            this.groupBox3.Location = new System.Drawing.Point(32, 630);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(114, 89);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // chkWrap
            // 
            this.chkWrap.AutoSize = true;
            this.chkWrap.Location = new System.Drawing.Point(22, 55);
            this.chkWrap.Name = "chkWrap";
            this.chkWrap.Size = new System.Drawing.Size(52, 17);
            this.chkWrap.TabIndex = 1;
            this.chkWrap.Text = "Wrap";
            this.chkWrap.UseVisualStyleBackColor = true;
            // 
            // chkLabReport
            // 
            this.chkLabReport.AutoSize = true;
            this.chkLabReport.Location = new System.Drawing.Point(22, 20);
            this.chkLabReport.Name = "chkLabReport";
            this.chkLabReport.Size = new System.Drawing.Size(79, 17);
            this.chkLabReport.TabIndex = 0;
            this.chkLabReport.Text = "Lab Report";
            this.chkLabReport.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(678, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "Batch Kg:";
            // 
            // txtBatchKg
            // 
            this.txtBatchKg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchKg.Location = new System.Drawing.Point(798, 95);
            this.txtBatchKg.Name = "txtBatchKg";
            this.txtBatchKg.ReadOnly = true;
            this.txtBatchKg.Size = new System.Drawing.Size(190, 29);
            this.txtBatchKg.TabIndex = 3;
            this.txtBatchKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(678, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 24);
            this.label8.TabIndex = 4;
            this.label8.Text = "Batch No";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchNo.Location = new System.Drawing.Point(798, 42);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(190, 29);
            this.txtBatchNo.TabIndex = 5;
            // 
            // rcbNotes
            // 
            this.rcbNotes.Location = new System.Drawing.Point(240, 630);
            this.rcbNotes.Name = "rcbNotes";
            this.rcbNotes.Size = new System.Drawing.Size(263, 96);
            this.rcbNotes.TabIndex = 6;
            this.rcbNotes.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(183, 633);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Notes";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 404);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1074, 202);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(71, 231);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(631, 116);
            this.dataGridView2.TabIndex = 9;
            this.dataGridView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1019, 684);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1019, 649);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbReprocessMode);
            this.groupBox2.Controls.Add(this.rbStandardMode);
            this.groupBox2.Location = new System.Drawing.Point(28, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 56);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // rbStandardMode
            // 
            this.rbStandardMode.AutoSize = true;
            this.rbStandardMode.Location = new System.Drawing.Point(6, 22);
            this.rbStandardMode.Name = "rbStandardMode";
            this.rbStandardMode.Size = new System.Drawing.Size(68, 17);
            this.rbStandardMode.TabIndex = 0;
            this.rbStandardMode.TabStop = true;
            this.rbStandardMode.Text = "Standard";
            this.rbStandardMode.UseVisualStyleBackColor = true;
            this.rbStandardMode.CheckedChanged += new System.EventHandler(this.rbStandardMode_CheckedChanged);
            // 
            // rbReprocessMode
            // 
            this.rbReprocessMode.AutoSize = true;
            this.rbReprocessMode.Location = new System.Drawing.Point(115, 22);
            this.rbReprocessMode.Name = "rbReprocessMode";
            this.rbReprocessMode.Size = new System.Drawing.Size(76, 17);
            this.rbReprocessMode.TabIndex = 1;
            this.rbReprocessMode.TabStop = true;
            this.rbReprocessMode.Text = "Reprocess";
            this.rbReprocessMode.UseVisualStyleBackColor = true;
            this.rbReprocessMode.CheckedChanged += new System.EventHandler(this.rbReprocessMode_CheckedChanged);
            // 
            // frmCommissionDyeing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 750);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rcbNotes);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBatchKg);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCommissionDyeing";
            this.Text = "Dye Batching (Commission Customers)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDyeBatch_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkWrap;
        private System.Windows.Forms.CheckBox chkLabReport;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBatchKg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.RichTextBox rcbNotes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboBatches;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmboTrims;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmboGreige;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRequiredDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dtpBatchDate;
        private System.Windows.Forms.ComboBox cmboCustomer;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbReprocessMode;
        private System.Windows.Forms.RadioButton rbStandardMode;
    }
}