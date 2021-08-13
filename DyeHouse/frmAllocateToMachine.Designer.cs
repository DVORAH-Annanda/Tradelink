namespace DyeHouse
{
    partial class frmAllocateToMachine
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
            this.dtpAllocationDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboDyeBatch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboDyeMachines = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBatchColour = new System.Windows.Forms.TextBox();
            this.txtBatchQuality = new System.Windows.Forms.TextBox();
            this.txtBatchWeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFabricType = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date of allocation";
            // 
            // dtpAllocationDate
            // 
            this.dtpAllocationDate.Location = new System.Drawing.Point(233, 20);
            this.dtpAllocationDate.Name = "dtpAllocationDate";
            this.dtpAllocationDate.Size = new System.Drawing.Size(140, 20);
            this.dtpAllocationDate.TabIndex = 1;
            this.dtpAllocationDate.ValueChanged += new System.EventHandler(this.dtpAllocationDate_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(514, 352);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dye Batch Available";
            // 
            // cmboDyeBatch
            // 
            this.cmboDyeBatch.FormattingEnabled = true;
            this.cmboDyeBatch.Location = new System.Drawing.Point(233, 53);
            this.cmboDyeBatch.Name = "cmboDyeBatch";
            this.cmboDyeBatch.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeBatch.TabIndex = 5;
            this.cmboDyeBatch.SelectedIndexChanged += new System.EventHandler(this.cmboDyeBatch_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dye Machines";
            // 
            // cmboDyeMachines
            // 
            this.cmboDyeMachines.FormattingEnabled = true;
            this.cmboDyeMachines.Location = new System.Drawing.Point(233, 278);
            this.cmboDyeMachines.Name = "cmboDyeMachines";
            this.cmboDyeMachines.Size = new System.Drawing.Size(140, 21);
            this.cmboDyeMachines.TabIndex = 7;
            this.cmboDyeMachines.SelectedIndexChanged += new System.EventHandler(this.cmboDyeMachines_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fabric Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBatchColour);
            this.groupBox1.Controls.Add(this.txtBatchQuality);
            this.groupBox1.Controls.Add(this.txtBatchWeight);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(166, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 117);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Batch Info";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Batch Weight";
            // 
            // txtBatchColour
            // 
            this.txtBatchColour.Location = new System.Drawing.Point(94, 84);
            this.txtBatchColour.Name = "txtBatchColour";
            this.txtBatchColour.ReadOnly = true;
            this.txtBatchColour.Size = new System.Drawing.Size(123, 20);
            this.txtBatchColour.TabIndex = 5;
            // 
            // txtBatchQuality
            // 
            this.txtBatchQuality.Location = new System.Drawing.Point(94, 53);
            this.txtBatchQuality.Name = "txtBatchQuality";
            this.txtBatchQuality.ReadOnly = true;
            this.txtBatchQuality.Size = new System.Drawing.Size(123, 20);
            this.txtBatchQuality.TabIndex = 4;
            // 
            // txtBatchWeight
            // 
            this.txtBatchWeight.Location = new System.Drawing.Point(94, 22);
            this.txtBatchWeight.Name = "txtBatchWeight";
            this.txtBatchWeight.ReadOnly = true;
            this.txtBatchWeight.Size = new System.Drawing.Size(123, 20);
            this.txtBatchWeight.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Batch Colour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Batch Quality";
            // 
            // txtFabricType
            // 
            this.txtFabricType.Location = new System.Drawing.Point(233, 325);
            this.txtFabricType.Name = "txtFabricType";
            this.txtFabricType.Size = new System.Drawing.Size(140, 20);
            this.txtFabricType.TabIndex = 12;
            // 
            // frmAllocateToMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 397);
            this.Controls.Add(this.txtFabricType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboDyeMachines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboDyeBatch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpAllocationDate);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "frmAllocateToMachine";
            this.Text = "Allocate batch to dye machine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAllocateToMachine_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpAllocationDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboDyeBatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboDyeMachines;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBatchColour;
        private System.Windows.Forms.TextBox txtBatchQuality;
        private System.Windows.Forms.TextBox txtBatchWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFabricType;
    }
}