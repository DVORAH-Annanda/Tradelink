namespace DyeHouse
{
    partial class FrmStabilityDrying
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
            this.oCmboPieceNumber = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboOperator = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboBatchNumber = new System.Windows.Forms.ComboBox();
            this.dtpStability = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbPassYes = new System.Windows.Forms.RadioButton();
            this.rbPassNo = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oCmboPieceNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmboOperator);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmboBatchNumber);
            this.groupBox1.Controls.Add(this.dtpStability);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(148, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // oCmboPieceNumber
            // 
            this.oCmboPieceNumber.FormattingEnabled = true;
            this.oCmboPieceNumber.Location = new System.Drawing.Point(157, 194);
            this.oCmboPieceNumber.Name = "oCmboPieceNumber";
            this.oCmboPieceNumber.Size = new System.Drawing.Size(121, 21);
            this.oCmboPieceNumber.TabIndex = 11;
            this.oCmboPieceNumber.SelectedIndexChanged += new System.EventHandler(this.oCmboPieceNumber_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Piece Number";
            // 
            // cmboOperator
            // 
            this.cmboOperator.FormattingEnabled = true;
            this.cmboOperator.Location = new System.Drawing.Point(157, 162);
            this.cmboOperator.Name = "cmboOperator";
            this.cmboOperator.Size = new System.Drawing.Size(121, 21);
            this.cmboOperator.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Operator";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(157, 130);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.ReadOnly = true;
            this.txtQuality.Size = new System.Drawing.Size(100, 20);
            this.txtQuality.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Body Quality";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(157, 97);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(100, 20);
            this.txtColour.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Colour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Batch Number";
            // 
            // cmboBatchNumber
            // 
            this.cmboBatchNumber.FormattingEnabled = true;
            this.cmboBatchNumber.Location = new System.Drawing.Point(157, 54);
            this.cmboBatchNumber.Name = "cmboBatchNumber";
            this.cmboBatchNumber.Size = new System.Drawing.Size(121, 21);
            this.cmboBatchNumber.TabIndex = 2;
            this.cmboBatchNumber.SelectedIndexChanged += new System.EventHandler(this.cmboBatchNumber_SelectedIndexChanged);
            // 
            // dtpStability
            // 
            this.dtpStability.Location = new System.Drawing.Point(157, 14);
            this.dtpStability.Name = "dtpStability";
            this.dtpStability.Size = new System.Drawing.Size(142, 20);
            this.dtpStability.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(148, 276);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(455, 206);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Results";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(44, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(384, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPassNo);
            this.groupBox2.Controls.Add(this.rbPassYes);
            this.groupBox2.Location = new System.Drawing.Point(261, 503);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 87);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(633, 594);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbPassYes
            // 
            this.rbPassYes.AutoSize = true;
            this.rbPassYes.Location = new System.Drawing.Point(32, 35);
            this.rbPassYes.Name = "rbPassYes";
            this.rbPassYes.Size = new System.Drawing.Size(69, 17);
            this.rbPassYes.TabIndex = 0;
            this.rbPassYes.TabStop = true;
            this.rbPassYes.Text = "Pass Yes";
            this.rbPassYes.UseVisualStyleBackColor = true;
            // 
            // rbPassNo
            // 
            this.rbPassNo.AutoSize = true;
            this.rbPassNo.Location = new System.Drawing.Point(145, 35);
            this.rbPassNo.Name = "rbPassNo";
            this.rbPassNo.Size = new System.Drawing.Size(65, 17);
            this.rbPassNo.TabIndex = 1;
            this.rbPassNo.TabStop = true;
            this.rbPassNo.Text = "Pass No";
            this.rbPassNo.UseVisualStyleBackColor = true;
            this.rbPassNo.CheckedChanged += new System.EventHandler(this.rbPassNo_CheckedChanged);
            // 
            // FrmStabilityDrying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 629);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmStabilityDrying";
            this.Text = "Stability after Drying";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboBatchNumber;
        private System.Windows.Forms.DateTimePicker dtpStability;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboOperator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox oCmboPieceNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbPassNo;
        private System.Windows.Forms.RadioButton rbPassYes;
    }
}