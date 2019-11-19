namespace DyeHouse
{
    partial class frmDyeOrder1
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
            this.txtDyeOrderNo = new System.Windows.Forms.TextBox();
            this.dtpDyeOrderDate = new System.Windows.Forms.DateTimePicker();
            this.cmboCustomer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCMTPLoss = new System.Windows.Forms.TextBox();
            this.txtCutPLoss = new System.Windows.Forms.TextBox();
            this.txtDyePLoss = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCmtReq = new System.Windows.Forms.TextBox();
            this.txtCutReq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDyeReq = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmboStyles = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmboFabric = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radKg2Meters = new System.Windows.Forms.RadioButton();
            this.radMeters2Kg = new System.Windows.Forms.RadioButton();
            this.radKgs2Units = new System.Windows.Forms.RadioButton();
            this.radUnits2Kg = new System.Windows.Forms.RadioButton();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.txtTotalKgs = new System.Windows.Forms.TextBox();
            this.cmboDyeOrders = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataSet11 = new Cutting.DataSet1();
            this.cmboLabel = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dye Order Number";
            // 
            // txtDyeOrderNo
            // 
            this.txtDyeOrderNo.Location = new System.Drawing.Point(134, 18);
            this.txtDyeOrderNo.Name = "txtDyeOrderNo";
            this.txtDyeOrderNo.ReadOnly = true;
            this.txtDyeOrderNo.Size = new System.Drawing.Size(100, 20);
            this.txtDyeOrderNo.TabIndex = 1;
            this.txtDyeOrderNo.TabStop = false;
            // 
            // dtpDyeOrderDate
            // 
            this.dtpDyeOrderDate.Location = new System.Drawing.Point(772, 18);
            this.dtpDyeOrderDate.Name = "dtpDyeOrderDate";
            this.dtpDyeOrderDate.Size = new System.Drawing.Size(151, 20);
            this.dtpDyeOrderDate.TabIndex = 2;
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(134, 67);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(177, 21);
            this.cmboCustomer.TabIndex = 2;
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Customer Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer Order No";
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(134, 113);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerOrder.TabIndex = 3;
            this.txtCustomerOrder.TextChanged += new System.EventHandler(this.txtDyeReq_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCMTPLoss);
            this.groupBox1.Controls.Add(this.txtCutPLoss);
            this.groupBox1.Controls.Add(this.txtDyePLoss);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCmtReq);
            this.groupBox1.Controls.Add(this.txtCutReq);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDyeReq);
            this.groupBox1.Location = new System.Drawing.Point(489, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 176);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Week";
            // 
            // txtCMTPLoss
            // 
            this.txtCMTPLoss.Location = new System.Drawing.Point(214, 111);
            this.txtCMTPLoss.Name = "txtCMTPLoss";
            this.txtCMTPLoss.Size = new System.Drawing.Size(51, 20);
            this.txtCMTPLoss.TabIndex = 5;
            // 
            // txtCutPLoss
            // 
            this.txtCutPLoss.Location = new System.Drawing.Point(214, 69);
            this.txtCutPLoss.Name = "txtCutPLoss";
            this.txtCutPLoss.Size = new System.Drawing.Size(51, 20);
            this.txtCutPLoss.TabIndex = 4;
            // 
            // txtDyePLoss
            // 
            this.txtDyePLoss.Location = new System.Drawing.Point(214, 27);
            this.txtDyePLoss.Name = "txtDyePLoss";
            this.txtDyePLoss.Size = new System.Drawing.Size(51, 20);
            this.txtDyePLoss.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(125, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Process Loss";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(125, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Process Loss";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Process Loss";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "CMT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cutting";
            // 
            // txtCmtReq
            // 
            this.txtCmtReq.Location = new System.Drawing.Point(56, 111);
            this.txtCmtReq.Name = "txtCmtReq";
            this.txtCmtReq.Size = new System.Drawing.Size(51, 20);
            this.txtCmtReq.TabIndex = 2;
            this.txtCmtReq.TextChanged += new System.EventHandler(this.txtDyeReq_TextChanged);
            this.txtCmtReq.Leave += new System.EventHandler(this.txtDyeReq_Leave);
            this.txtCmtReq.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtCmtReq_PreviewKeyDown);
            // 
            // txtCutReq
            // 
            this.txtCutReq.Location = new System.Drawing.Point(56, 69);
            this.txtCutReq.Name = "txtCutReq";
            this.txtCutReq.Size = new System.Drawing.Size(51, 20);
            this.txtCutReq.TabIndex = 1;
            this.txtCutReq.TextChanged += new System.EventHandler(this.txtDyeReq_TextChanged);
            this.txtCutReq.Leave += new System.EventHandler(this.txtDyeReq_Leave);
            this.txtCutReq.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtCutReq_PreviewKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Dye";
            // 
            // txtDyeReq
            // 
            this.txtDyeReq.Location = new System.Drawing.Point(56, 27);
            this.txtDyeReq.Name = "txtDyeReq";
            this.txtDyeReq.Size = new System.Drawing.Size(51, 20);
            this.txtDyeReq.TabIndex = 0;
            this.txtDyeReq.TextChanged += new System.EventHandler(this.txtDyeReq_TextChanged);
            this.txtDyeReq.Leave += new System.EventHandler(this.txtDyeReq_Leave);
            this.txtDyeReq.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtDyeReq_PreviewKeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Style Required";
            // 
            // cmboStyles
            // 
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(134, 158);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(177, 21);
            this.cmboStyles.TabIndex = 4;
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Fabric Required";
            // 
            // cmboFabric
            // 
            this.cmboFabric.FormattingEnabled = true;
            this.cmboFabric.Location = new System.Drawing.Point(134, 205);
            this.cmboFabric.Name = "cmboFabric";
            this.cmboFabric.Size = new System.Drawing.Size(177, 21);
            this.cmboFabric.TabIndex = 5;
            this.cmboFabric.SelectedIndexChanged += new System.EventHandler(this.cmboFabric_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(30, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(944, 145);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Body";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(849, 98);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(33, 463);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(944, 140);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bindings and Trims";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(25, 23);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(852, 100);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellLeave);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(872, 685);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Colour Required";
            // 
            // cmboColour
            // 
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(134, 242);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(177, 21);
            this.cmboColour.TabIndex = 6;
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radKg2Meters);
            this.groupBox4.Controls.Add(this.radMeters2Kg);
            this.groupBox4.Controls.Add(this.radKgs2Units);
            this.groupBox4.Controls.Add(this.radUnits2Kg);
            this.groupBox4.Location = new System.Drawing.Point(344, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(110, 146);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Calculations";
            // 
            // radKg2Meters
            // 
            this.radKg2Meters.AutoSize = true;
            this.radKg2Meters.Location = new System.Drawing.Point(7, 122);
            this.radKg2Meters.Name = "radKg2Meters";
            this.radKg2Meters.Size = new System.Drawing.Size(90, 17);
            this.radKg2Meters.TabIndex = 3;
            this.radKg2Meters.TabStop = true;
            this.radKg2Meters.Text = "Kgs to Meters";
            this.radKg2Meters.UseVisualStyleBackColor = true;
            // 
            // radMeters2Kg
            // 
            this.radMeters2Kg.AutoSize = true;
            this.radMeters2Kg.Location = new System.Drawing.Point(7, 91);
            this.radMeters2Kg.Name = "radMeters2Kg";
            this.radMeters2Kg.Size = new System.Drawing.Size(85, 17);
            this.radMeters2Kg.TabIndex = 2;
            this.radMeters2Kg.TabStop = true;
            this.radMeters2Kg.Text = "Meters to Kg";
            this.radMeters2Kg.UseVisualStyleBackColor = true;
            this.radMeters2Kg.CheckedChanged += new System.EventHandler(this.radMeters2Kg_CheckedChanged);
            // 
            // radKgs2Units
            // 
            this.radKgs2Units.AutoSize = true;
            this.radKgs2Units.Location = new System.Drawing.Point(7, 60);
            this.radKgs2Units.Name = "radKgs2Units";
            this.radKgs2Units.Size = new System.Drawing.Size(82, 17);
            this.radKgs2Units.TabIndex = 1;
            this.radKgs2Units.TabStop = true;
            this.radKgs2Units.Text = "Kgs to Units";
            this.radKgs2Units.UseVisualStyleBackColor = true;
            // 
            // radUnits2Kg
            // 
            this.radUnits2Kg.AutoSize = true;
            this.radUnits2Kg.Location = new System.Drawing.Point(7, 29);
            this.radUnits2Kg.Name = "radUnits2Kg";
            this.radUnits2Kg.Size = new System.Drawing.Size(86, 17);
            this.radUnits2Kg.TabIndex = 0;
            this.radUnits2Kg.TabStop = true;
            this.radUnits2Kg.Text = "Units To Kgs";
            this.radUnits2Kg.UseVisualStyleBackColor = true;
            this.radUnits2Kg.CheckedChanged += new System.EventHandler(this.radUnits2Kg_CheckedChanged);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(134, 633);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(190, 96);
            this.txtNotes.TabIndex = 18;
            this.txtNotes.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 622);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Notes";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(366, 622);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(318, 107);
            this.dataGridView3.TabIndex = 20;
            // 
            // txtTotalKgs
            // 
            this.txtTotalKgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalKgs.Location = new System.Drawing.Point(748, 609);
            this.txtTotalKgs.Name = "txtTotalKgs";
            this.txtTotalKgs.ReadOnly = true;
            this.txtTotalKgs.ShortcutsEnabled = false;
            this.txtTotalKgs.Size = new System.Drawing.Size(162, 26);
            this.txtTotalKgs.TabIndex = 21;
            this.txtTotalKgs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmboDyeOrders
            // 
            this.cmboDyeOrders.FormattingEnabled = true;
            this.cmboDyeOrders.Location = new System.Drawing.Point(620, 18);
            this.cmboDyeOrders.Name = "cmboDyeOrders";
            this.cmboDyeOrders.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeOrders.TabIndex = 22;
            this.cmboDyeOrders.SelectedIndexChanged += new System.EventHandler(this.cmboDyeOrders_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(496, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Current Dye Orders";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(872, 656);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(791, 656);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cmboLabel
            // 
            this.cmboLabel.FormattingEnabled = true;
            this.cmboLabel.Location = new System.Drawing.Point(134, 280);
            this.cmboLabel.Name = "cmboLabel";
            this.cmboLabel.Size = new System.Drawing.Size(177, 21);
            this.cmboLabel.TabIndex = 26;
            this.cmboLabel.SelectedIndexChanged += new System.EventHandler(this.cmboLabel_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 280);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "Label Required";
            // 
            // frmDyeOrder1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 741);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmboLabel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmboDyeOrders);
            this.Controls.Add(this.txtTotalKgs);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmboFabric);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCustomerOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboCustomer);
            this.Controls.Add(this.dtpDyeOrderDate);
            this.Controls.Add(this.txtDyeOrderNo);
            this.Controls.Add(this.label1);
            this.Name = "frmDyeOrder1";
            this.Text = "Dye Order (Garments)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDyeOrderNo;
        private System.Windows.Forms.DateTimePicker dtpDyeOrderDate;
        private System.Windows.Forms.ComboBox cmboCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustomerOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCMTPLoss;
        private System.Windows.Forms.TextBox txtCutPLoss;
        private System.Windows.Forms.TextBox txtDyePLoss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCmtReq;
        private System.Windows.Forms.TextBox txtCutReq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDyeReq;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmboFabric;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radKgs2Units;
        private System.Windows.Forms.RadioButton radUnits2Kg;
        private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox txtTotalKgs;
        private System.Windows.Forms.ComboBox cmboDyeOrders;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton radKg2Meters;
        private System.Windows.Forms.RadioButton radMeters2Kg;
        private Cutting.DataSet1 dataSet11;
        private System.Windows.Forms.ComboBox cmboLabel;
        private System.Windows.Forms.Label label15;
    }
}