namespace Spinning
{
    partial class frmCottonDelivery
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
            this.dtpDateReceived = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCottonSuppliers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCottonContracts = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSuppplierGrossWeight = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.microAvailYes = new System.Windows.Forms.RadioButton();
            this.microAvailNo = new System.Windows.Forms.RadioButton();
            this.txtWeighBridgeNett = new System.Windows.Forms.TextBox();
            this.txtWeighBridgeGross = new System.Windows.Forms.TextBox();
            this.txtSupplierNettWeight = new System.Windows.Forms.TextBox();
            this.txtNoOfBales = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbHaulier = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNetAvgBaleWeight = new System.Windows.Forms.TextBox();
            this.txtGrossAvgBaleWeight = new System.Windows.Forms.TextBox();
            this.txtCottonNettWeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtGrnNumber = new System.Windows.Forms.TextBox();
            this.txtVehReg = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date Received";
            // 
            // dtpDateReceived
            // 
            this.dtpDateReceived.Location = new System.Drawing.Point(253, 13);
            this.dtpDateReceived.Name = "dtpDateReceived";
            this.dtpDateReceived.Size = new System.Drawing.Size(150, 20);
            this.dtpDateReceived.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cotton Supplier";
            // 
            // cmbCottonSuppliers
            // 
            this.cmbCottonSuppliers.FormattingEnabled = true;
            this.cmbCottonSuppliers.Location = new System.Drawing.Point(282, 49);
            this.cmbCottonSuppliers.Name = "cmbCottonSuppliers";
            this.cmbCottonSuppliers.Size = new System.Drawing.Size(201, 21);
            this.cmbCottonSuppliers.TabIndex = 1;
            this.cmbCottonSuppliers.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cotton Contracts";
            // 
            // cmbCottonContracts
            // 
            this.cmbCottonContracts.FormattingEnabled = true;
            this.cmbCottonContracts.Location = new System.Drawing.Point(282, 83);
            this.cmbCottonContracts.Name = "cmbCottonContracts";
            this.cmbCottonContracts.Size = new System.Drawing.Size(201, 21);
            this.cmbCottonContracts.TabIndex = 2;
            this.cmbCottonContracts.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(602, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Lot Number";
            // 
            // txtLotNo
            // 
            this.txtLotNo.Location = new System.Drawing.Point(303, 147);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.ReadOnly = true;
            this.txtLotNo.Size = new System.Drawing.Size(100, 20);
            this.txtLotNo.TabIndex = 4;
            this.txtLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSuppplierGrossWeight);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtWeighBridgeNett);
            this.groupBox1.Controls.Add(this.txtWeighBridgeGross);
            this.groupBox1.Controls.Add(this.txtSupplierNettWeight);
            this.groupBox1.Controls.Add(this.txtNoOfBales);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(75, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 256);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // txtSuppplierGrossWeight
            // 
            this.txtSuppplierGrossWeight.Location = new System.Drawing.Point(133, 57);
            this.txtSuppplierGrossWeight.Name = "txtSuppplierGrossWeight";
            this.txtSuppplierGrossWeight.Size = new System.Drawing.Size(100, 20);
            this.txtSuppplierGrossWeight.TabIndex = 8;
            this.txtSuppplierGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSuppplierGrossWeight.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Supplier Gross Weight";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.microAvailYes);
            this.groupBox3.Controls.Add(this.microAvailNo);
            this.groupBox3.Location = new System.Drawing.Point(9, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 59);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "micra info Available";
            // 
            // microAvailYes
            // 
            this.microAvailYes.AutoSize = true;
            this.microAvailYes.Location = new System.Drawing.Point(104, 33);
            this.microAvailYes.Name = "microAvailYes";
            this.microAvailYes.Size = new System.Drawing.Size(43, 17);
            this.microAvailYes.TabIndex = 1;
            this.microAvailYes.TabStop = true;
            this.microAvailYes.Text = "Yes";
            this.microAvailYes.UseVisualStyleBackColor = true;
            this.microAvailYes.CheckedChanged += new System.EventHandler(this.microAvailYes_CheckedChanged);
            // 
            // microAvailNo
            // 
            this.microAvailNo.AutoSize = true;
            this.microAvailNo.Location = new System.Drawing.Point(13, 33);
            this.microAvailNo.Name = "microAvailNo";
            this.microAvailNo.Size = new System.Drawing.Size(39, 17);
            this.microAvailNo.TabIndex = 0;
            this.microAvailNo.TabStop = true;
            this.microAvailNo.Text = "No";
            this.microAvailNo.UseVisualStyleBackColor = true;
            // 
            // txtWeighBridgeNett
            // 
            this.txtWeighBridgeNett.Location = new System.Drawing.Point(133, 153);
            this.txtWeighBridgeNett.Name = "txtWeighBridgeNett";
            this.txtWeighBridgeNett.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeNett.TabIndex = 11;
            this.txtWeighBridgeNett.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeighBridgeNett.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtWeighBridgeGross
            // 
            this.txtWeighBridgeGross.Location = new System.Drawing.Point(133, 121);
            this.txtWeighBridgeGross.Name = "txtWeighBridgeGross";
            this.txtWeighBridgeGross.Size = new System.Drawing.Size(100, 20);
            this.txtWeighBridgeGross.TabIndex = 10;
            this.txtWeighBridgeGross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeighBridgeGross.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtSupplierNettWeight
            // 
            this.txtSupplierNettWeight.Location = new System.Drawing.Point(133, 89);
            this.txtSupplierNettWeight.Name = "txtSupplierNettWeight";
            this.txtSupplierNettWeight.Size = new System.Drawing.Size(100, 20);
            this.txtSupplierNettWeight.TabIndex = 9;
            this.txtSupplierNettWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSupplierNettWeight.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtNoOfBales
            // 
            this.txtNoOfBales.Location = new System.Drawing.Point(133, 25);
            this.txtNoOfBales.Name = "txtNoOfBales";
            this.txtNoOfBales.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfBales.TabIndex = 7;
            this.txtNoOfBales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfBales.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Weigh Bridge Nett";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Weigh Bridge Gross";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Supplier Nett Weight";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "No of Bales";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Haulier";
            // 
            // cmbHaulier
            // 
            this.cmbHaulier.FormattingEnabled = true;
            this.cmbHaulier.Location = new System.Drawing.Point(282, 186);
            this.cmbHaulier.Name = "cmbHaulier";
            this.cmbHaulier.Size = new System.Drawing.Size(201, 21);
            this.cmbHaulier.TabIndex = 5;
            this.cmbHaulier.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Vehicle Registration";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNetAvgBaleWeight);
            this.groupBox2.Controls.Add(this.txtGrossAvgBaleWeight);
            this.groupBox2.Controls.Add(this.txtCottonNettWeight);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(399, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 131);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calculated Values";
            // 
            // txtNetAvgBaleWeight
            // 
            this.txtNetAvgBaleWeight.Location = new System.Drawing.Point(161, 81);
            this.txtNetAvgBaleWeight.Name = "txtNetAvgBaleWeight";
            this.txtNetAvgBaleWeight.ReadOnly = true;
            this.txtNetAvgBaleWeight.Size = new System.Drawing.Size(100, 20);
            this.txtNetAvgBaleWeight.TabIndex = 2;
            this.txtNetAvgBaleWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrossAvgBaleWeight
            // 
            this.txtGrossAvgBaleWeight.Location = new System.Drawing.Point(161, 49);
            this.txtGrossAvgBaleWeight.Name = "txtGrossAvgBaleWeight";
            this.txtGrossAvgBaleWeight.ReadOnly = true;
            this.txtGrossAvgBaleWeight.Size = new System.Drawing.Size(100, 20);
            this.txtGrossAvgBaleWeight.TabIndex = 1;
            this.txtGrossAvgBaleWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCottonNettWeight
            // 
            this.txtCottonNettWeight.Location = new System.Drawing.Point(161, 17);
            this.txtCottonNettWeight.Name = "txtCottonNettWeight";
            this.txtCottonNettWeight.ReadOnly = true;
            this.txtCottonNettWeight.Size = new System.Drawing.Size(100, 20);
            this.txtCottonNettWeight.TabIndex = 0;
            this.txtCottonNettWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Net Average Bale Weight";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(138, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Gross Average Bale Weight";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Cotton Gross Weight";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(521, 472);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Bales";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(165, 117);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "GRN Number";
            // 
            // txtGrnNumber
            // 
            this.txtGrnNumber.Location = new System.Drawing.Point(303, 114);
            this.txtGrnNumber.Name = "txtGrnNumber";
            this.txtGrnNumber.ReadOnly = true;
            this.txtGrnNumber.Size = new System.Drawing.Size(100, 20);
            this.txtGrnNumber.TabIndex = 3;
            this.txtGrnNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVehReg
            // 
            this.txtVehReg.Location = new System.Drawing.Point(282, 222);
            this.txtVehReg.Name = "txtVehReg";
            this.txtVehReg.Size = new System.Drawing.Size(121, 20);
            this.txtVehReg.TabIndex = 6;
            this.txtVehReg.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // frmCottonDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 519);
            this.Controls.Add(this.txtVehReg);
            this.Controls.Add(this.txtGrnNumber);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbHaulier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLotNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbCottonContracts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCottonSuppliers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDateReceived);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonDelivery";
            this.Text = "Cotton Delivery";
            this.Load += new System.EventHandler(this.frmCottonDelivery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateReceived;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCottonSuppliers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCottonContracts;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWeighBridgeNett;
        private System.Windows.Forms.TextBox txtWeighBridgeGross;
        private System.Windows.Forms.TextBox txtSupplierNettWeight;
        private System.Windows.Forms.TextBox txtNoOfBales;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbHaulier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNetAvgBaleWeight;
        private System.Windows.Forms.TextBox txtGrossAvgBaleWeight;
        private System.Windows.Forms.TextBox txtCottonNettWeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton microAvailYes;
        private System.Windows.Forms.RadioButton microAvailNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtGrnNumber;
        private System.Windows.Forms.TextBox txtSuppplierGrossWeight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtVehReg;
    }
}