namespace Knitting
{
    partial class frmKnitOrder
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
            this.txtKnitOrder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpKnitOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateRequired = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboSizes = new System.Windows.Forms.ComboBox();
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.rbThirdParty = new System.Windows.Forms.RadioButton();
            this.rbOwnYarn = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtFabricWidth = new System.Windows.Forms.TextBox();
            this.txtGreigeWeight = new System.Windows.Forms.TextBox();
            this.cmbFabricType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbComCustNo = new System.Windows.Forms.RadioButton();
            this.rbComCustYes = new System.Windows.Forms.RadioButton();
            this.cmbEditKO = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbKnitted = new System.Windows.Forms.ComboBox();
            this.txtYLTSetting = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbMachines = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOrderPieces = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOrderKG = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbKnittedFor = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Knitting Order Number";
            // 
            // txtKnitOrder
            // 
            this.txtKnitOrder.Location = new System.Drawing.Point(176, 17);
            this.txtKnitOrder.Name = "txtKnitOrder";
            this.txtKnitOrder.ReadOnly = true;
            this.txtKnitOrder.Size = new System.Drawing.Size(117, 20);
            this.txtKnitOrder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Knit Order Date";
            // 
            // dtpKnitOrderDate
            // 
            this.dtpKnitOrderDate.Location = new System.Drawing.Point(176, 50);
            this.dtpKnitOrderDate.Name = "dtpKnitOrderDate";
            this.dtpKnitOrderDate.Size = new System.Drawing.Size(127, 20);
            this.dtpKnitOrderDate.TabIndex = 3;
            this.dtpKnitOrderDate.ValueChanged += new System.EventHandler(this.dtpKnitOrderDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Knit Order Required";
            // 
            // dtpDateRequired
            // 
            this.dtpDateRequired.Location = new System.Drawing.Point(176, 79);
            this.dtpDateRequired.Name = "dtpDateRequired";
            this.dtpDateRequired.Size = new System.Drawing.Size(127, 20);
            this.dtpDateRequired.TabIndex = 5;
            this.dtpDateRequired.ValueChanged += new System.EventHandler(this.dtpKnitOrderDate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboSizes);
            this.groupBox1.Controls.Add(this.cmboColours);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.rbThirdParty);
            this.groupBox1.Controls.Add(this.rbOwnYarn);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.txtFabricWidth);
            this.groupBox1.Controls.Add(this.txtGreigeWeight);
            this.groupBox1.Controls.Add(this.cmbFabricType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(472, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 321);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Greige Requirements";
            // 
            // cmboSizes
            // 
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(474, 56);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(137, 21);
            this.cmboSizes.TabIndex = 12;
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(474, 24);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(137, 21);
            this.cmboColours.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(427, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Sizes";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(426, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Colours";
            // 
            // rbThirdParty
            // 
            this.rbThirdParty.AutoSize = true;
            this.rbThirdParty.Location = new System.Drawing.Point(351, 113);
            this.rbThirdParty.Name = "rbThirdParty";
            this.rbThirdParty.Size = new System.Drawing.Size(76, 17);
            this.rbThirdParty.TabIndex = 8;
            this.rbThirdParty.TabStop = true;
            this.rbThirdParty.Text = "Third Party";
            this.rbThirdParty.UseVisualStyleBackColor = true;
            this.rbThirdParty.Visible = false;
            this.rbThirdParty.CheckedChanged += new System.EventHandler(this.rbThirdParty_CheckedChanged);
            // 
            // rbOwnYarn
            // 
            this.rbOwnYarn.AutoSize = true;
            this.rbOwnYarn.Checked = true;
            this.rbOwnYarn.Location = new System.Drawing.Point(239, 113);
            this.rbOwnYarn.Name = "rbOwnYarn";
            this.rbOwnYarn.Size = new System.Drawing.Size(72, 17);
            this.rbOwnYarn.TabIndex = 7;
            this.rbOwnYarn.TabStop = true;
            this.rbOwnYarn.Text = "Own Yarn";
            this.rbOwnYarn.UseVisualStyleBackColor = true;
            this.rbOwnYarn.Visible = false;
            this.rbOwnYarn.CheckedChanged += new System.EventHandler(this.rbOwnYarn_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 149);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(594, 153);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // txtFabricWidth
            // 
            this.txtFabricWidth.Location = new System.Drawing.Point(156, 87);
            this.txtFabricWidth.Name = "txtFabricWidth";
            this.txtFabricWidth.ReadOnly = true;
            this.txtFabricWidth.Size = new System.Drawing.Size(121, 20);
            this.txtFabricWidth.TabIndex = 5;
            // 
            // txtGreigeWeight
            // 
            this.txtGreigeWeight.Location = new System.Drawing.Point(156, 59);
            this.txtGreigeWeight.Name = "txtGreigeWeight";
            this.txtGreigeWeight.ReadOnly = true;
            this.txtGreigeWeight.Size = new System.Drawing.Size(121, 20);
            this.txtGreigeWeight.TabIndex = 4;
            // 
            // cmbFabricType
            // 
            this.cmbFabricType.FormattingEnabled = true;
            this.cmbFabricType.Location = new System.Drawing.Point(156, 27);
            this.cmbFabricType.Name = "cmbFabricType";
            this.cmbFabricType.Size = new System.Drawing.Size(243, 21);
            this.cmbFabricType.TabIndex = 3;
            this.cmbFabricType.SelectedIndexChanged += new System.EventHandler(this.cmbFabricType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Fabric Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Fabric Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Greige Type";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.cmbEditKO);
            this.groupBox2.Controls.Add(this.txtKnitOrder);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpDateRequired);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpKnitOrderDate);
            this.groupBox2.Location = new System.Drawing.Point(237, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 133);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbComCustNo);
            this.groupBox6.Controls.Add(this.rbComCustYes);
            this.groupBox6.Location = new System.Drawing.Point(331, 51);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(131, 48);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Commission Customer";
            // 
            // rbComCustNo
            // 
            this.rbComCustNo.AutoSize = true;
            this.rbComCustNo.Location = new System.Drawing.Point(70, 19);
            this.rbComCustNo.Name = "rbComCustNo";
            this.rbComCustNo.Size = new System.Drawing.Size(39, 17);
            this.rbComCustNo.TabIndex = 1;
            this.rbComCustNo.TabStop = true;
            this.rbComCustNo.Text = "No";
            this.rbComCustNo.UseVisualStyleBackColor = true;
            this.rbComCustNo.CheckedChanged += new System.EventHandler(this.rbComCustNo_CheckedChanged);
            // 
            // rbComCustYes
            // 
            this.rbComCustYes.AutoSize = true;
            this.rbComCustYes.Location = new System.Drawing.Point(9, 19);
            this.rbComCustYes.Name = "rbComCustYes";
            this.rbComCustYes.Size = new System.Drawing.Size(43, 17);
            this.rbComCustYes.TabIndex = 0;
            this.rbComCustYes.TabStop = true;
            this.rbComCustYes.Text = "Yes";
            this.rbComCustYes.UseVisualStyleBackColor = true;
            this.rbComCustYes.CheckedChanged += new System.EventHandler(this.rbComCustYes_CheckedChanged);
            // 
            // cmbEditKO
            // 
            this.cmbEditKO.FormattingEnabled = true;
            this.cmbEditKO.Location = new System.Drawing.Point(304, 16);
            this.cmbEditKO.Name = "cmbEditKO";
            this.cmbEditKO.Size = new System.Drawing.Size(121, 21);
            this.cmbEditKO.TabIndex = 15;
            this.cmbEditKO.SelectedIndexChanged += new System.EventHandler(this.cmbEditKO_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbKnitted);
            this.groupBox3.Controls.Add(this.txtYLTSetting);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cmbMachines);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtOrderPieces);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtOrderKG);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cmbKnittedFor);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(84, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(371, 195);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Order Requirements";
            // 
            // cmbKnitted
            // 
            this.cmbKnitted.FormattingEnabled = true;
            this.cmbKnitted.Location = new System.Drawing.Point(231, 56);
            this.cmbKnitted.Name = "cmbKnitted";
            this.cmbKnitted.Size = new System.Drawing.Size(141, 21);
            this.cmbKnitted.TabIndex = 6;
            this.cmbKnitted.SelectedIndexChanged += new System.EventHandler(this.cmbKnittedFor_SelectedIndexChanged);
            // 
            // txtYLTSetting
            // 
            this.txtYLTSetting.Location = new System.Drawing.Point(125, 156);
            this.txtYLTSetting.Name = "txtYLTSetting";
            this.txtYLTSetting.Size = new System.Drawing.Size(100, 20);
            this.txtYLTSetting.TabIndex = 9;
            this.txtYLTSetting.TextChanged += new System.EventHandler(this.YLT_Changed);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "YLT Setting";
            // 
            // cmbMachines
            // 
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(125, 125);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(186, 21);
            this.cmbMachines.TabIndex = 7;
            this.cmbMachines.SelectedIndexChanged += new System.EventHandler(this.cmbMachDet_IndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Machine Details";
            // 
            // txtOrderPieces
            // 
            this.txtOrderPieces.Location = new System.Drawing.Point(125, 91);
            this.txtOrderPieces.Name = "txtOrderPieces";
            this.txtOrderPieces.Size = new System.Drawing.Size(100, 20);
            this.txtOrderPieces.TabIndex = 5;
            this.txtOrderPieces.TextChanged += new System.EventHandler(this.OrderPieces_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Order Pieces";
            // 
            // txtOrderKG
            // 
            this.txtOrderKG.Location = new System.Drawing.Point(125, 56);
            this.txtOrderKG.Name = "txtOrderKG";
            this.txtOrderKG.Size = new System.Drawing.Size(100, 20);
            this.txtOrderKG.TabIndex = 3;
            this.txtOrderKG.TextChanged += new System.EventHandler(this.OrderQty_Changed);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Order Quantity (KG)\r\n";
            // 
            // cmbKnittedFor
            // 
            this.cmbKnittedFor.FormattingEnabled = true;
            this.cmbKnittedFor.Location = new System.Drawing.Point(125, 24);
            this.cmbKnittedFor.Name = "cmbKnittedFor";
            this.cmbKnittedFor.Size = new System.Drawing.Size(224, 21);
            this.cmbKnittedFor.TabIndex = 1;
            this.cmbKnittedFor.SelectedIndexChanged += new System.EventHandler(this.cmbKnittedFor_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Knitted For";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.richTextBox1);
            this.groupBox5.Location = new System.Drawing.Point(472, 478);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(266, 144);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Notes";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(21, 40);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(217, 100);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1022, 626);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(1022, 586);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(1022, 546);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(1022, 506);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // frmKnitOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 671);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmKnitOrder";
            this.Text = "Knitting Order";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKnitOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpKnitOrderDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDateRequired;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFabricType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFabricWidth;
        private System.Windows.Forms.TextBox txtGreigeWeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOrderKG;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbKnittedFor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtYLTSetting;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbMachines;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOrderPieces;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox cmbEditKO;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbComCustNo;
        private System.Windows.Forms.RadioButton rbComCustYes;
        private System.Windows.Forms.ComboBox cmbKnitted;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rbThirdParty;
        private System.Windows.Forms.RadioButton rbOwnYarn;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmboSizes;
    }
}