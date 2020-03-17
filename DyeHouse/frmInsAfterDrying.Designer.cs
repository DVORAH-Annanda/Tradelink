namespace DyeHouse
{
    partial class frmInsAfterDrying
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
            this.cmboQEMeasurements = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDyeMachine = new System.Windows.Forms.TextBox();
            this.txtKnittingMachine = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboBatchNumber = new System.Windows.Forms.ComboBox();
            this.dtpStability = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCauses = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboQEMeasurements);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDyeMachine);
            this.groupBox1.Controls.Add(this.txtKnittingMachine);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmboBatchNumber);
            this.groupBox1.Controls.Add(this.dtpStability);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(48, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 281);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cmboQEMeasurements
            // 
            this.cmboQEMeasurements.FormattingEnabled = true;
            this.cmboQEMeasurements.Location = new System.Drawing.Point(219, 88);
            this.cmboQEMeasurements.Name = "cmboQEMeasurements";
            this.cmboQEMeasurements.Size = new System.Drawing.Size(157, 21);
            this.cmboQEMeasurements.TabIndex = 14;
            this.cmboQEMeasurements.SelectedIndexChanged += new System.EventHandler(this.cmboPieceNumbers_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Measurements";
            // 
            // txtDyeMachine
            // 
            this.txtDyeMachine.Location = new System.Drawing.Point(219, 238);
            this.txtDyeMachine.Name = "txtDyeMachine";
            this.txtDyeMachine.ReadOnly = true;
            this.txtDyeMachine.Size = new System.Drawing.Size(263, 20);
            this.txtDyeMachine.TabIndex = 12;
            // 
            // txtKnittingMachine
            // 
            this.txtKnittingMachine.Location = new System.Drawing.Point(219, 203);
            this.txtKnittingMachine.Name = "txtKnittingMachine";
            this.txtKnittingMachine.ReadOnly = true;
            this.txtKnittingMachine.Size = new System.Drawing.Size(263, 20);
            this.txtKnittingMachine.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Dye Machine Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Knitting Machine";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(219, 168);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.ReadOnly = true;
            this.txtQuality.Size = new System.Drawing.Size(263, 20);
            this.txtQuality.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Body Quality";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(219, 133);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(263, 20);
            this.txtColour.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Colour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Batch Number";
            // 
            // cmboBatchNumber
            // 
            this.cmboBatchNumber.FormattingEnabled = true;
            this.cmboBatchNumber.Location = new System.Drawing.Point(219, 49);
            this.cmboBatchNumber.Name = "cmboBatchNumber";
            this.cmboBatchNumber.Size = new System.Drawing.Size(157, 21);
            this.cmboBatchNumber.TabIndex = 2;
            this.cmboBatchNumber.SelectedIndexChanged += new System.EventHandler(this.cmboBatchNumber_SelectedIndexChanged);
            // 
            // dtpStability
            // 
            this.dtpStability.Location = new System.Drawing.Point(219, 14);
            this.dtpStability.Name = "dtpStability";
            this.dtpStability.Size = new System.Drawing.Size(142, 20);
            this.dtpStability.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(539, 551);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(48, 351);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(521, 171);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 557);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Cause";
            // 
            // txtCauses
            // 
            this.txtCauses.Location = new System.Drawing.Point(177, 554);
            this.txtCauses.Name = "txtCauses";
            this.txtCauses.Size = new System.Drawing.Size(336, 20);
            this.txtCauses.TabIndex = 5;
            // 
            // frmInsAfterDrying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 612);
            this.Controls.Add(this.txtCauses);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInsAfterDrying";
            this.Text = "Inspection After Drying";
            this.Load += new System.EventHandler(this.frmInsAfterDrying_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDyeMachine;
        private System.Windows.Forms.TextBox txtKnittingMachine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboBatchNumber;
        private System.Windows.Forms.DateTimePicker dtpStability;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCauses;
        private System.Windows.Forms.ComboBox cmboQEMeasurements;
        private System.Windows.Forms.Label label8;
    }
}