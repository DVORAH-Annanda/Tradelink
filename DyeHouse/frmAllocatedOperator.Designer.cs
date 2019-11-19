namespace DyeHouse
{
    partial class frmAllocatedOperator
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
            this.dtpDateAllocated = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboDyeBatch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.txtSubCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMachineDescription = new System.Windows.Forms.TextBox();
            this.cmboOperator = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpDateAllocated
            // 
            this.dtpDateAllocated.Location = new System.Drawing.Point(241, 21);
            this.dtpDateAllocated.Name = "dtpDateAllocated";
            this.dtpDateAllocated.Size = new System.Drawing.Size(139, 20);
            this.dtpDateAllocated.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date Allocated";
            // 
            // cmboDyeBatch
            // 
            this.cmboDyeBatch.FormattingEnabled = true;
            this.cmboDyeBatch.Location = new System.Drawing.Point(241, 60);
            this.cmboDyeBatch.Name = "cmboDyeBatch";
            this.cmboDyeBatch.Size = new System.Drawing.Size(121, 21);
            this.cmboDyeBatch.TabIndex = 2;
            this.cmboDyeBatch.SelectedIndexChanged += new System.EventHandler(this.cmboDyeBatch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Batch No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Machine Code";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(241, 112);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.ReadOnly = true;
            this.txtMachineCode.Size = new System.Drawing.Size(100, 20);
            this.txtMachineCode.TabIndex = 5;
            // 
            // txtSubCode
            // 
            this.txtSubCode.Location = new System.Drawing.Point(241, 162);
            this.txtSubCode.Name = "txtSubCode";
            this.txtSubCode.ReadOnly = true;
            this.txtSubCode.Size = new System.Drawing.Size(100, 20);
            this.txtSubCode.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sub Code";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Machine Description";
            // 
            // txtMachineDescription
            // 
            this.txtMachineDescription.Location = new System.Drawing.Point(241, 206);
            this.txtMachineDescription.Name = "txtMachineDescription";
            this.txtMachineDescription.ReadOnly = true;
            this.txtMachineDescription.Size = new System.Drawing.Size(188, 20);
            this.txtMachineDescription.TabIndex = 9;
            // 
            // cmboOperator
            // 
            this.cmboOperator.FormattingEnabled = true;
            this.cmboOperator.Location = new System.Drawing.Point(241, 261);
            this.cmboOperator.Name = "cmboOperator";
            this.cmboOperator.Size = new System.Drawing.Size(121, 21);
            this.cmboOperator.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Operator";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(519, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAllocatedOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 406);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboOperator);
            this.Controls.Add(this.txtMachineDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSubCode);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboDyeBatch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDateAllocated);
            this.Name = "frmAllocatedOperator";
            this.Text = "Allocated Operator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateAllocated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboDyeBatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMachineCode;
        private System.Windows.Forms.TextBox txtSubCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMachineDescription;
        private System.Windows.Forms.ComboBox cmboOperator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
    }
}