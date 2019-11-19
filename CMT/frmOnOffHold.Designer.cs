namespace CMT
{
    partial class frmOnOffHold
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
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlaceOffHold = new System.Windows.Forms.RadioButton();
            this.rbPlaceOnHold = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReasons = new System.Windows.Forms.TextBox();
            this.txtAuthorisedBy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboDepartments = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(212, 31);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(130, 20);
            this.dtpTransDate.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(107, 250);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(408, 201);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(469, 469);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPlaceOffHold);
            this.groupBox1.Controls.Add(this.rbPlaceOnHold);
            this.groupBox1.Location = new System.Drawing.Point(113, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 55);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // rbPlaceOffHold
            // 
            this.rbPlaceOffHold.AutoSize = true;
            this.rbPlaceOffHold.Location = new System.Drawing.Point(192, 22);
            this.rbPlaceOffHold.Name = "rbPlaceOffHold";
            this.rbPlaceOffHold.Size = new System.Drawing.Size(94, 17);
            this.rbPlaceOffHold.TabIndex = 1;
            this.rbPlaceOffHold.TabStop = true;
            this.rbPlaceOffHold.Text = "Place Off Hold";
            this.rbPlaceOffHold.UseVisualStyleBackColor = true;
            this.rbPlaceOffHold.CheckedChanged += new System.EventHandler(this.rbPlaceOffHold_CheckedChanged);
            // 
            // rbPlaceOnHold
            // 
            this.rbPlaceOnHold.AutoSize = true;
            this.rbPlaceOnHold.Location = new System.Drawing.Point(56, 22);
            this.rbPlaceOnHold.Name = "rbPlaceOnHold";
            this.rbPlaceOnHold.Size = new System.Drawing.Size(94, 17);
            this.rbPlaceOnHold.TabIndex = 0;
            this.rbPlaceOnHold.TabStop = true;
            this.rbPlaceOnHold.Text = "Place On Hold";
            this.rbPlaceOnHold.UseVisualStyleBackColor = true;
            this.rbPlaceOnHold.CheckedChanged += new System.EventHandler(this.rbPlaceOnHold_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reasons ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Authorised By";
            // 
            // txtReasons
            // 
            this.txtReasons.Location = new System.Drawing.Point(201, 169);
            this.txtReasons.Name = "txtReasons";
            this.txtReasons.Size = new System.Drawing.Size(214, 20);
            this.txtReasons.TabIndex = 7;
            // 
            // txtAuthorisedBy
            // 
            this.txtAuthorisedBy.Location = new System.Drawing.Point(201, 202);
            this.txtAuthorisedBy.Name = "txtAuthorisedBy";
            this.txtAuthorisedBy.Size = new System.Drawing.Size(214, 20);
            this.txtAuthorisedBy.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current CMT";
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(212, 65);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(174, 21);
            this.cmboDepartments.TabIndex = 10;
            this.cmboDepartments.SelectedIndexChanged += new System.EventHandler(this.cmboDepartments_SelectedIndexChanged);
            // 
            // frmOnOffHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 504);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAuthorisedBy);
            this.Controls.Add(this.txtReasons);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label1);
            this.Name = "frmOnOffHold";
            this.Text = "Place CutSheet On / Off Hold";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOnOffHold_FormClosing);
            this.Load += new System.EventHandler(this.frmOnOffHold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPlaceOffHold;
        private System.Windows.Forms.RadioButton rbPlaceOnHold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReasons;
        private System.Windows.Forms.TextBox txtAuthorisedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboDepartments;
    }
}