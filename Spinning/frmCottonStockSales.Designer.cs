namespace Spinning
{
    partial class frmCottonStockSales
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeliveryNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDateDelivered = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbLotNo = new System.Windows.Forms.ComboBox();
            this.txtCustOrderNo = new System.Windows.Forms.TextBox();
            this.cmbContractNo = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrossWeightDeliv = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNettWeightDel = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.rtbCustomerAddress = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVehReg = new System.Windows.Forms.TextBox();
            this.txtWeighBridgeEmpty = new System.Windows.Forms.TextBox();
            this.txtWeighBridgeFull = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboTransporter = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPickList = new System.Windows.Forms.Button();
            this.btnEditMode = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbPrevious = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivery Note Number";
            // 
            // txtDeliveryNo
            // 
            this.txtDeliveryNo.Location = new System.Drawing.Point(290, 30);
            this.txtDeliveryNo.Name = "txtDeliveryNo";
            this.txtDeliveryNo.ReadOnly = true;
            this.txtDeliveryNo.Size = new System.Drawing.Size(121, 20);
            this.txtDeliveryNo.TabIndex = 1;
            this.txtDeliveryNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Customer Name";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(290, 73);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(200, 21);
            this.cmbCustomerName.TabIndex = 3;
            this.cmbCustomerName.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Customer Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Customer Order No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Contract No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(148, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Date Deilivered";
            // 
            // dtpDateDelivered
            // 
            this.dtpDateDelivered.Location = new System.Drawing.Point(290, 251);
            this.dtpDateDelivered.Name = "dtpDateDelivered";
            this.dtpDateDelivered.Size = new System.Drawing.Size(200, 20);
            this.dtpDateDelivered.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Lot Number";
            // 
            // cmbLotNo
            // 
            this.cmbLotNo.FormattingEnabled = true;
            this.cmbLotNo.Location = new System.Drawing.Point(290, 295);
            this.cmbLotNo.Name = "cmbLotNo";
            this.cmbLotNo.Size = new System.Drawing.Size(121, 21);
            this.cmbLotNo.TabIndex = 10;
            this.cmbLotNo.SelectedIndexChanged += new System.EventHandler(this.cmbLotNo_SelectedIndexChanged);
            // 
            // txtCustOrderNo
            // 
            this.txtCustOrderNo.Location = new System.Drawing.Point(290, 163);
            this.txtCustOrderNo.Name = "txtCustOrderNo";
            this.txtCustOrderNo.Size = new System.Drawing.Size(100, 20);
            this.txtCustOrderNo.TabIndex = 12;
            this.txtCustOrderNo.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // cmbContractNo
            // 
            this.cmbContractNo.FormattingEnabled = true;
            this.cmbContractNo.Location = new System.Drawing.Point(290, 204);
            this.cmbContractNo.Name = "cmbContractNo";
            this.cmbContractNo.Size = new System.Drawing.Size(121, 21);
            this.cmbContractNo.TabIndex = 13;
            this.cmbContractNo.SelectedIndexChanged += new System.EventHandler(this.cmbContractNo_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(291, 491);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(370, 158);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Gross Weight Delivered";
            // 
            // txtGrossWeightDeliv
            // 
            this.txtGrossWeightDeliv.Location = new System.Drawing.Point(170, 457);
            this.txtGrossWeightDeliv.Name = "txtGrossWeightDeliv";
            this.txtGrossWeightDeliv.ReadOnly = true;
            this.txtGrossWeightDeliv.Size = new System.Drawing.Size(100, 20);
            this.txtGrossWeightDeliv.TabIndex = 16;
            this.txtGrossWeightDeliv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 512);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Nett Weight Delivered";
            // 
            // txtNettWeightDel
            // 
            this.txtNettWeightDel.Location = new System.Drawing.Point(170, 509);
            this.txtNettWeightDel.Name = "txtNettWeightDel";
            this.txtNettWeightDel.ReadOnly = true;
            this.txtNettWeightDel.Size = new System.Drawing.Size(100, 20);
            this.txtNettWeightDel.TabIndex = 18;
            this.txtNettWeightDel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(698, 626);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rtbCustomerAddress
            // 
            this.rtbCustomerAddress.Location = new System.Drawing.Point(289, 106);
            this.rtbCustomerAddress.Name = "rtbCustomerAddress";
            this.rtbCustomerAddress.Size = new System.Drawing.Size(201, 47);
            this.rtbCustomerAddress.TabIndex = 20;
            this.rtbCustomerAddress.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVehReg);
            this.groupBox1.Controls.Add(this.txtWeighBridgeEmpty);
            this.groupBox1.Controls.Add(this.txtWeighBridgeFull);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.comboTransporter);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(289, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 149);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transport Details";
            // 
            // txtVehReg
            // 
            this.txtVehReg.Location = new System.Drawing.Point(166, 60);
            this.txtVehReg.Name = "txtVehReg";
            this.txtVehReg.Size = new System.Drawing.Size(154, 20);
            this.txtVehReg.TabIndex = 8;
            this.txtVehReg.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtWeighBridgeEmpty
            // 
            this.txtWeighBridgeEmpty.Location = new System.Drawing.Point(166, 120);
            this.txtWeighBridgeEmpty.Name = "txtWeighBridgeEmpty";
            this.txtWeighBridgeEmpty.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeEmpty.TabIndex = 7;
            this.txtWeighBridgeEmpty.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtWeighBridgeFull
            // 
            this.txtWeighBridgeFull.Location = new System.Drawing.Point(166, 91);
            this.txtWeighBridgeFull.Name = "txtWeighBridgeFull";
            this.txtWeighBridgeFull.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeFull.TabIndex = 6;
            this.txtWeighBridgeFull.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Weigh Bridge Empty";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Weigh Bridge Full";
            // 
            // comboTransporter
            // 
            this.comboTransporter.FormattingEnabled = true;
            this.comboTransporter.Location = new System.Drawing.Point(166, 23);
            this.comboTransporter.Name = "comboTransporter";
            this.comboTransporter.Size = new System.Drawing.Size(179, 21);
            this.comboTransporter.TabIndex = 2;
            this.comboTransporter.SelectedIndexChanged += new System.EventHandler(this.comboTransporter_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Vehicle Registration No";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Transporter Details";
            // 
            // btnPickList
            // 
            this.btnPickList.Location = new System.Drawing.Point(698, 588);
            this.btnPickList.Name = "btnPickList";
            this.btnPickList.Size = new System.Drawing.Size(75, 23);
            this.btnPickList.TabIndex = 22;
            this.btnPickList.Text = "Pick List";
            this.btnPickList.UseVisualStyleBackColor = true;
            this.btnPickList.Click += new System.EventHandler(this.btnPickList_Click);
            // 
            // btnEditMode
            // 
            this.btnEditMode.Location = new System.Drawing.Point(698, 546);
            this.btnEditMode.Name = "btnEditMode";
            this.btnEditMode.Size = new System.Drawing.Size(75, 23);
            this.btnEditMode.TabIndex = 23;
            this.btnEditMode.Text = "Edit Mode";
            this.btnEditMode.UseVisualStyleBackColor = true;
            this.btnEditMode.Click += new System.EventHandler(this.btnEditMode_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cmbPrevious
            // 
            this.cmbPrevious.FormattingEnabled = true;
            this.cmbPrevious.Location = new System.Drawing.Point(528, 73);
            this.cmbPrevious.Name = "cmbPrevious";
            this.cmbPrevious.Size = new System.Drawing.Size(159, 21);
            this.cmbPrevious.TabIndex = 24;
            this.cmbPrevious.SelectedIndexChanged += new System.EventHandler(this.cmbPrevious_SelectedIndexChanged);
            // 
            // frmCottonStockSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 673);
            this.Controls.Add(this.cmbPrevious);
            this.Controls.Add(this.btnEditMode);
            this.Controls.Add(this.btnPickList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbCustomerAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNettWeightDel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtGrossWeightDeliv);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbContractNo);
            this.Controls.Add(this.txtCustOrderNo);
            this.Controls.Add(this.cmbLotNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpDateDelivered);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCustomerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDeliveryNo);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonStockSales";
            this.Text = "Cotton stock Sales";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeliveryNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDateDelivered;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbLotNo;
        private System.Windows.Forms.TextBox txtCustOrderNo;
        private System.Windows.Forms.ComboBox cmbContractNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGrossWeightDeliv;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNettWeightDel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rtbCustomerAddress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboTransporter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWeighBridgeFull;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWeighBridgeEmpty;
        private System.Windows.Forms.TextBox txtVehReg;
        private System.Windows.Forms.Button btnPickList;
        private System.Windows.Forms.Button btnEditMode;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cmbPrevious;
    }
}