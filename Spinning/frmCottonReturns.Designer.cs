namespace Spinning
{
    partial class frmCottonReturns
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
            this.txtReturnNoteNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSupplierDetails = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbContractNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateReturned = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLotNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGrossWeightReturned = new System.Windows.Forms.TextBox();
            this.txtNettWeightReturned = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVehReg = new System.Windows.Forms.TextBox();
            this.txtWeighBridgeEmpty = new System.Windows.Forms.TextBox();
            this.txtWeighBridgeFull = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbTransporter = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPickList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPrevious = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Return Note Number";
            // 
            // txtReturnNoteNumber
            // 
            this.txtReturnNoteNumber.Location = new System.Drawing.Point(260, 16);
            this.txtReturnNoteNumber.Name = "txtReturnNoteNumber";
            this.txtReturnNoteNumber.ReadOnly = true;
            this.txtReturnNoteNumber.Size = new System.Drawing.Size(118, 20);
            this.txtReturnNoteNumber.TabIndex = 1;
            this.txtReturnNoteNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contract Details";
            // 
            // cmbSupplierDetails
            // 
            this.cmbSupplierDetails.FormattingEnabled = true;
            this.cmbSupplierDetails.Location = new System.Drawing.Point(260, 58);
            this.cmbSupplierDetails.Name = "cmbSupplierDetails";
            this.cmbSupplierDetails.Size = new System.Drawing.Size(194, 21);
            this.cmbSupplierDetails.TabIndex = 3;
            this.cmbSupplierDetails.SelectedIndexChanged += new System.EventHandler(this.cmbSupplierDetails_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Contract Number";
            // 
            // cmbContractNo
            // 
            this.cmbContractNo.FormattingEnabled = true;
            this.cmbContractNo.Location = new System.Drawing.Point(260, 101);
            this.cmbContractNo.Name = "cmbContractNo";
            this.cmbContractNo.Size = new System.Drawing.Size(182, 21);
            this.cmbContractNo.TabIndex = 5;
            this.cmbContractNo.SelectedIndexChanged += new System.EventHandler(this.cmbContractNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date Returned ";
            // 
            // dtpDateReturned
            // 
            this.dtpDateReturned.Location = new System.Drawing.Point(260, 144);
            this.dtpDateReturned.Name = "dtpDateReturned";
            this.dtpDateReturned.Size = new System.Drawing.Size(200, 20);
            this.dtpDateReturned.TabIndex = 7;
            this.dtpDateReturned.ValueChanged += new System.EventHandler(this.dtpDateReturned_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Lot Numbers";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(119, 229);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(511, 150);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 408);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Gross Weight Returned";
            // 
            // cmbLotNo
            // 
            this.cmbLotNo.FormattingEnabled = true;
            this.cmbLotNo.Location = new System.Drawing.Point(260, 186);
            this.cmbLotNo.Name = "cmbLotNo";
            this.cmbLotNo.Size = new System.Drawing.Size(121, 21);
            this.cmbLotNo.TabIndex = 11;
            this.cmbLotNo.SelectedIndexChanged += new System.EventHandler(this.cmbLotNo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 443);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Nett Weight Returned";
            // 
            // txtGrossWeightReturned
            // 
            this.txtGrossWeightReturned.Location = new System.Drawing.Point(260, 401);
            this.txtGrossWeightReturned.Name = "txtGrossWeightReturned";
            this.txtGrossWeightReturned.ReadOnly = true;
            this.txtGrossWeightReturned.Size = new System.Drawing.Size(100, 20);
            this.txtGrossWeightReturned.TabIndex = 13;
            this.txtGrossWeightReturned.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNettWeightReturned
            // 
            this.txtNettWeightReturned.Location = new System.Drawing.Point(260, 436);
            this.txtNettWeightReturned.Name = "txtNettWeightReturned";
            this.txtNettWeightReturned.ReadOnly = true;
            this.txtNettWeightReturned.Size = new System.Drawing.Size(100, 20);
            this.txtNettWeightReturned.TabIndex = 14;
            this.txtNettWeightReturned.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(594, 638);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(260, 565);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(241, 96);
            this.rtbNotes.TabIndex = 16;
            this.rtbNotes.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 568);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Notes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVehReg);
            this.groupBox1.Controls.Add(this.txtWeighBridgeEmpty);
            this.groupBox1.Controls.Add(this.txtWeighBridgeFull);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbTransporter);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(388, 385);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 147);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transporter Details";
            // 
            // txtVehReg
            // 
            this.txtVehReg.Location = new System.Drawing.Point(151, 52);
            this.txtVehReg.Name = "txtVehReg";
            this.txtVehReg.Size = new System.Drawing.Size(121, 20);
            this.txtVehReg.TabIndex = 8;
            this.txtVehReg.TextChanged += new System.EventHandler(this.txt_ValuieChanged);
            // 
            // txtWeighBridgeEmpty
            // 
            this.txtWeighBridgeEmpty.Location = new System.Drawing.Point(151, 109);
            this.txtWeighBridgeEmpty.Name = "txtWeighBridgeEmpty";
            this.txtWeighBridgeEmpty.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeEmpty.TabIndex = 7;
            this.txtWeighBridgeEmpty.TextChanged += new System.EventHandler(this.txt_ValuieChanged);
            // 
            // txtWeighBridgeFull
            // 
            this.txtWeighBridgeFull.Location = new System.Drawing.Point(142, 81);
            this.txtWeighBridgeFull.Name = "txtWeighBridgeFull";
            this.txtWeighBridgeFull.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeFull.TabIndex = 6;
            this.txtWeighBridgeFull.TextChanged += new System.EventHandler(this.txt_ValuieChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Weigh Bridge Empty";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Weigh Bridge Full";
            // 
            // cmbTransporter
            // 
            this.cmbTransporter.FormattingEnabled = true;
            this.cmbTransporter.Location = new System.Drawing.Point(151, 23);
            this.cmbTransporter.Name = "cmbTransporter";
            this.cmbTransporter.Size = new System.Drawing.Size(121, 21);
            this.cmbTransporter.TabIndex = 2;
            this.cmbTransporter.SelectedIndexChanged += new System.EventHandler(this.cmbTransporter_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Vehicle Registration";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Transporter Details";
            // 
            // btnPickList
            // 
            this.btnPickList.Location = new System.Drawing.Point(594, 598);
            this.btnPickList.Name = "btnPickList";
            this.btnPickList.Size = new System.Drawing.Size(75, 23);
            this.btnPickList.TabIndex = 19;
            this.btnPickList.Text = "Pick List";
            this.btnPickList.UseVisualStyleBackColor = true;
            this.btnPickList.Click += new System.EventHandler(this.btnPickList_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(594, 558);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Edit Mode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPrevious
            // 
            this.cmbPrevious.FormattingEnabled = true;
            this.cmbPrevious.Location = new System.Drawing.Point(466, 58);
            this.cmbPrevious.Name = "cmbPrevious";
            this.cmbPrevious.Size = new System.Drawing.Size(194, 21);
            this.cmbPrevious.TabIndex = 21;
            this.cmbPrevious.SelectedIndexChanged += new System.EventHandler(this.cmbPrevious_SelectedIndexChanged);
            // 
            // frmCottonReturns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 673);
            this.Controls.Add(this.cmbPrevious);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPickList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNettWeightReturned);
            this.Controls.Add(this.txtGrossWeightReturned);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbLotNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDateReturned);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbContractNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSupplierDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReturnNoteNumber);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonReturns";
            this.Text = "Cotton Returns to Supplier";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReturnNoteNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSupplierDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbContractNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateReturned;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLotNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGrossWeightReturned;
        private System.Windows.Forms.TextBox txtNettWeightReturned;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTransporter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWeighBridgeEmpty;
        private System.Windows.Forms.TextBox txtWeighBridgeFull;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVehReg;
        private System.Windows.Forms.Button btnPickList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPrevious;
    }
}