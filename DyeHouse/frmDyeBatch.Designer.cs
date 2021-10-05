namespace DyeHouse
{
    partial class frmDyeBatch
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
            this.dtpRequiredDate = new System.Windows.Forms.DateTimePicker();
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbReprocessMode = new System.Windows.Forms.RadioButton();
            this.rbStandardMode = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            this.cmboBatches = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrderedDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboDyeOrders = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radWithWarning = new System.Windows.Forms.RadioButton();
            this.radBAll = new System.Windows.Forms.RadioButton();
            this.radbBoth = new System.Windows.Forms.RadioButton();
            this.radbGradeB = new System.Windows.Forms.RadioButton();
            this.radbGradeA = new System.Windows.Forms.RadioButton();
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
            this.label11 = new System.Windows.Forms.Label();
            this.txtNoOfPieces = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpRequiredDate);
            this.groupBox1.Controls.Add(this.cmboColours);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtCustomerOrder);
            this.groupBox1.Controls.Add(this.cmboBatches);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtOrderedDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCustomer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmboDyeOrders);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(43, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 234);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpRequiredDate
            // 
            this.dtpRequiredDate.Location = new System.Drawing.Point(361, 199);
            this.dtpRequiredDate.Name = "dtpRequiredDate";
            this.dtpRequiredDate.Size = new System.Drawing.Size(121, 20);
            this.dtpRequiredDate.TabIndex = 17;
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(132, 88);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(121, 21);
            this.cmboColours.TabIndex = 12;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbReprocessMode);
            this.groupBox4.Controls.Add(this.rbStandardMode);
            this.groupBox4.Location = new System.Drawing.Point(274, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 42);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mode";
            // 
            // rbReprocessMode
            // 
            this.rbReprocessMode.AutoSize = true;
            this.rbReprocessMode.Location = new System.Drawing.Point(99, 15);
            this.rbReprocessMode.Name = "rbReprocessMode";
            this.rbReprocessMode.Size = new System.Drawing.Size(76, 17);
            this.rbReprocessMode.TabIndex = 1;
            this.rbReprocessMode.TabStop = true;
            this.rbReprocessMode.Text = "Reprocess";
            this.rbReprocessMode.UseVisualStyleBackColor = true;
            this.rbReprocessMode.CheckedChanged += new System.EventHandler(this.rbReprocessMode_CheckedChanged);
            // 
            // rbStandardMode
            // 
            this.rbStandardMode.AutoSize = true;
            this.rbStandardMode.Location = new System.Drawing.Point(25, 15);
            this.rbStandardMode.Name = "rbStandardMode";
            this.rbStandardMode.Size = new System.Drawing.Size(68, 17);
            this.rbStandardMode.TabIndex = 0;
            this.rbStandardMode.TabStop = true;
            this.rbStandardMode.Text = "Standard";
            this.rbStandardMode.UseVisualStyleBackColor = true;
            this.rbStandardMode.CheckedChanged += new System.EventHandler(this.rbStandardMode_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Dye Batches";
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(132, 146);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.ReadOnly = true;
            this.txtCustomerOrder.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerOrder.TabIndex = 13;
            // 
            // cmboBatches
            // 
            this.cmboBatches.FormattingEnabled = true;
            this.cmboBatches.Location = new System.Drawing.Point(132, 54);
            this.cmboBatches.Name = "cmboBatches";
            this.cmboBatches.Size = new System.Drawing.Size(121, 21);
            this.cmboBatches.TabIndex = 15;
            this.cmboBatches.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Customer Order";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Required Date";
            // 
            // txtOrderedDate
            // 
            this.txtOrderedDate.Location = new System.Drawing.Point(132, 176);
            this.txtOrderedDate.Name = "txtOrderedDate";
            this.txtOrderedDate.ReadOnly = true;
            this.txtOrderedDate.Size = new System.Drawing.Size(100, 20);
            this.txtOrderedDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ordered Date";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(132, 116);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(121, 20);
            this.txtCustomer.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(132, 88);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(121, 20);
            this.txtColour.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Colour";
            // 
            // cmboDyeOrders
            // 
            this.cmboDyeOrders.FormattingEnabled = true;
            this.cmboDyeOrders.Location = new System.Drawing.Point(132, 23);
            this.cmboDyeOrders.Name = "cmboDyeOrders";
            this.cmboDyeOrders.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOrders.TabIndex = 2;
            this.cmboDyeOrders.SelectedIndexChanged += new System.EventHandler(this.cmboDyeOrders_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dye Orders";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radWithWarning);
            this.groupBox2.Controls.Add(this.radBAll);
            this.groupBox2.Controls.Add(this.radbBoth);
            this.groupBox2.Controls.Add(this.radbGradeB);
            this.groupBox2.Controls.Add(this.radbGradeA);
            this.groupBox2.Location = new System.Drawing.Point(274, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 101);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show Grade";
            // 
            // radWithWarning
            // 
            this.radWithWarning.AutoSize = true;
            this.radWithWarning.Location = new System.Drawing.Point(16, 75);
            this.radWithWarning.Name = "radWithWarning";
            this.radWithWarning.Size = new System.Drawing.Size(129, 17);
            this.radWithWarning.TabIndex = 4;
            this.radWithWarning.TabStop = true;
            this.radWithWarning.Text = "Grade A with Warning\r\n";
            this.radWithWarning.UseVisualStyleBackColor = true;
            this.radWithWarning.CheckedChanged += new System.EventHandler(this.radWhiteOnly_CheckedChanged);
            // 
            // radBAll
            // 
            this.radBAll.AutoSize = true;
            this.radBAll.Location = new System.Drawing.Point(109, 44);
            this.radBAll.Name = "radBAll";
            this.radBAll.Size = new System.Drawing.Size(36, 17);
            this.radBAll.TabIndex = 3;
            this.radBAll.TabStop = true;
            this.radBAll.Text = "All";
            this.radBAll.UseVisualStyleBackColor = true;
            this.radBAll.CheckedChanged += new System.EventHandler(this.radBAll_CheckedChanged);
            // 
            // radbBoth
            // 
            this.radbBoth.AutoSize = true;
            this.radbBoth.Location = new System.Drawing.Point(109, 19);
            this.radbBoth.Name = "radbBoth";
            this.radbBoth.Size = new System.Drawing.Size(83, 17);
            this.radbBoth.TabIndex = 2;
            this.radbBoth.TabStop = true;
            this.radbBoth.Text = "Grade A + B";
            this.radbBoth.UseVisualStyleBackColor = true;
            this.radbBoth.CheckedChanged += new System.EventHandler(this.radbBoth_CheckedChanged);
            // 
            // radbGradeB
            // 
            this.radbGradeB.AutoSize = true;
            this.radbGradeB.Location = new System.Drawing.Point(16, 47);
            this.radbGradeB.Name = "radbGradeB";
            this.radbGradeB.Size = new System.Drawing.Size(64, 17);
            this.radbGradeB.TabIndex = 1;
            this.radbGradeB.TabStop = true;
            this.radbGradeB.Text = "Grade B";
            this.radbGradeB.UseVisualStyleBackColor = true;
            this.radbGradeB.CheckedChanged += new System.EventHandler(this.radbGradeB_CheckedChanged);
            // 
            // radbGradeA
            // 
            this.radbGradeA.AutoSize = true;
            this.radbGradeA.Location = new System.Drawing.Point(16, 19);
            this.radbGradeA.Name = "radbGradeA";
            this.radbGradeA.Size = new System.Drawing.Size(64, 17);
            this.radbGradeA.TabIndex = 0;
            this.radbGradeA.TabStop = true;
            this.radbGradeA.Text = "Grade A";
            this.radbGradeA.UseVisualStyleBackColor = true;
            this.radbGradeA.CheckedChanged += new System.EventHandler(this.radbGradeA_CheckedChanged);
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
            this.dataGridView2.Location = new System.Drawing.Point(71, 252);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(717, 138);
            this.dataGridView2.TabIndex = 9;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(836, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 24);
            this.label11.TabIndex = 12;
            this.label11.Text = "No of Pieces";
            // 
            // txtNoOfPieces
            // 
            this.txtNoOfPieces.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfPieces.Location = new System.Drawing.Point(857, 217);
            this.txtNoOfPieces.Name = "txtNoOfPieces";
            this.txtNoOfPieces.ReadOnly = true;
            this.txtNoOfPieces.Size = new System.Drawing.Size(86, 29);
            this.txtNoOfPieces.TabIndex = 13;
            // 
            // frmDyeBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 750);
            this.Controls.Add(this.txtNoOfPieces);
            this.Controls.Add(this.label11);
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
            this.Name = "frmDyeBatch";
            this.Text = "Dye Batching";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDyeBatch_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCustomerOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOrderedDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboDyeOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radBAll;
        private System.Windows.Forms.RadioButton radbBoth;
        private System.Windows.Forms.RadioButton radbGradeB;
        private System.Windows.Forms.RadioButton radbGradeA;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbReprocessMode;
        private System.Windows.Forms.RadioButton rbStandardMode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNoOfPieces;
        private System.Windows.Forms.DateTimePicker dtpRequiredDate;
        private System.Windows.Forms.RadioButton radWithWarning;
    }
}