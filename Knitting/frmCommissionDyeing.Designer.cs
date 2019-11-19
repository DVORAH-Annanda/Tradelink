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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboCustomer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDateRequired = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDateOrdered = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBatchDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmboGreige = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboTrims = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkWrapFabric = new System.Windows.Forms.CheckBox();
            this.chkLabReport = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKgsSelected = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmboPrevBatches = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Batch Number";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(151, 18);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(100, 20);
            this.txtBatchNo.TabIndex = 1;
            this.txtBatchNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(593, 597);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer";
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(151, 55);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(121, 21);
            this.cmboCustomer.TabIndex = 4;
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Colours";
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(151, 95);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(121, 21);
            this.cmboColours.TabIndex = 6;
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(186, 520);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(254, 83);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDateRequired);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpDateOrdered);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpBatchDate);
            this.groupBox1.Location = new System.Drawing.Point(347, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 133);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dates";
            // 
            // dtpDateRequired
            // 
            this.dtpDateRequired.Location = new System.Drawing.Point(159, 96);
            this.dtpDateRequired.Name = "dtpDateRequired";
            this.dtpDateRequired.Size = new System.Drawing.Size(141, 20);
            this.dtpDateRequired.TabIndex = 5;
            this.dtpDateRequired.ValueChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Date Required";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Date Ordered";
            // 
            // dtpDateOrdered
            // 
            this.dtpDateOrdered.Location = new System.Drawing.Point(159, 56);
            this.dtpDateOrdered.Name = "dtpDateOrdered";
            this.dtpDateOrdered.Size = new System.Drawing.Size(140, 20);
            this.dtpDateOrdered.TabIndex = 2;
            this.dtpDateOrdered.ValueChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Batch Date";
            // 
            // dtpBatchDate
            // 
            this.dtpBatchDate.Location = new System.Drawing.Point(159, 19);
            this.dtpBatchDate.Name = "dtpBatchDate";
            this.dtpBatchDate.Size = new System.Drawing.Size(140, 20);
            this.dtpBatchDate.TabIndex = 0;
            this.dtpBatchDate.ValueChanged += new System.EventHandler(this.dtpBatchDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBatchNo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmboColours);
            this.groupBox2.Controls.Add(this.cmboCustomer);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 133);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Static Data";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Greige Quality";
            // 
            // cmboGreige
            // 
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(447, 174);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(121, 21);
            this.cmboGreige.TabIndex = 11;
            this.cmboGreige.SelectedIndexChanged += new System.EventHandler(this.cmboGreige_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(359, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Trims";
            // 
            // cmboTrims
            // 
            this.cmboTrims.FormattingEnabled = true;
            this.cmboTrims.Location = new System.Drawing.Point(447, 218);
            this.cmboTrims.Name = "cmboTrims";
            this.cmboTrims.Size = new System.Drawing.Size(121, 21);
            this.cmboTrims.TabIndex = 13;
            this.cmboTrims.SelectedIndexChanged += new System.EventHandler(this.cmboTrims_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkWrapFabric);
            this.groupBox3.Controls.Add(this.chkLabReport);
            this.groupBox3.Location = new System.Drawing.Point(31, 520);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(130, 100);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // chkWrapFabric
            // 
            this.chkWrapFabric.AutoSize = true;
            this.chkWrapFabric.Location = new System.Drawing.Point(17, 62);
            this.chkWrapFabric.Name = "chkWrapFabric";
            this.chkWrapFabric.Size = new System.Drawing.Size(84, 17);
            this.chkWrapFabric.TabIndex = 2;
            this.chkWrapFabric.Text = "Wrap Fabric";
            this.chkWrapFabric.UseVisualStyleBackColor = true;
            // 
            // chkLabReport
            // 
            this.chkLabReport.AutoSize = true;
            this.chkLabReport.Location = new System.Drawing.Point(17, 28);
            this.chkLabReport.Name = "chkLabReport";
            this.chkLabReport.Size = new System.Drawing.Size(79, 17);
            this.chkLabReport.TabIndex = 1;
            this.chkLabReport.Text = "Lab Report";
            this.chkLabReport.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(127, 273);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(603, 150);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(161, 454);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Total Kgs Selected";
            // 
            // txtKgsSelected
            // 
            this.txtKgsSelected.Location = new System.Drawing.Point(270, 451);
            this.txtKgsSelected.Name = "txtKgsSelected";
            this.txtKgsSelected.ReadOnly = true;
            this.txtKgsSelected.Size = new System.Drawing.Size(100, 20);
            this.txtKgsSelected.TabIndex = 17;
            this.txtKgsSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmboPrevBatches);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(12, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(321, 49);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            // 
            // cmboPrevBatches
            // 
            this.cmboPrevBatches.FormattingEnabled = true;
            this.cmboPrevBatches.Location = new System.Drawing.Point(151, 13);
            this.cmboPrevBatches.Name = "cmboPrevBatches";
            this.cmboPrevBatches.Size = new System.Drawing.Size(121, 21);
            this.cmboPrevBatches.TabIndex = 1;
            this.cmboPrevBatches.SelectedIndexChanged += new System.EventHandler(this.cmboPrevBatches_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Previous Batches";
            // 
            // frmCommissionDyeing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 638);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtKgsSelected);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmboTrims);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmboGreige);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmCommissionDyeing";
            this.Text = "Commission Dyeing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpDateRequired;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDateOrdered;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBatchDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmboGreige;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmboTrims;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkWrapFabric;
        private System.Windows.Forms.CheckBox chkLabReport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtKgsSelected;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmboPrevBatches;
        private System.Windows.Forms.Label label10;
    }
}