namespace Spinning
{
    partial class frmYarnProdSel
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
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbSpecificOrder = new System.Windows.Forms.ComboBox();
            this.rbSpecificYes = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbSpecificNo = new System.Windows.Forms.RadioButton();
            this.rbYes = new System.Windows.Forms.RadioButton();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chkQASummary = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(110, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Selection";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(175, 57);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(121, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(175, 23);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(121, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(472, 319);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSpecificOrder);
            this.groupBox2.Controls.Add(this.rbSpecificYes);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rbSpecificNo);
            this.groupBox2.Controls.Add(this.rbYes);
            this.groupBox2.Controls.Add(this.rbNo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(110, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 112);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection criteria";
            // 
            // cmbSpecificOrder
            // 
            this.cmbSpecificOrder.FormattingEnabled = true;
            this.cmbSpecificOrder.Location = new System.Drawing.Point(260, 52);
            this.cmbSpecificOrder.Name = "cmbSpecificOrder";
            this.cmbSpecificOrder.Size = new System.Drawing.Size(121, 21);
            this.cmbSpecificOrder.TabIndex = 6;
            // 
            // rbSpecificYes
            // 
            this.rbSpecificYes.AutoSize = true;
            this.rbSpecificYes.Location = new System.Drawing.Point(211, 51);
            this.rbSpecificYes.Name = "rbSpecificYes";
            this.rbSpecificYes.Size = new System.Drawing.Size(43, 17);
            this.rbSpecificYes.TabIndex = 5;
            this.rbSpecificYes.TabStop = true;
            this.rbSpecificYes.Text = "Yes";
            this.rbSpecificYes.UseVisualStyleBackColor = true;
            this.rbSpecificYes.CheckedChanged += new System.EventHandler(this.rbSpecificYes_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Select a specific order\r\n";
            // 
            // rbSpecificNo
            // 
            this.rbSpecificNo.AutoSize = true;
            this.rbSpecificNo.Location = new System.Drawing.Point(154, 51);
            this.rbSpecificNo.Name = "rbSpecificNo";
            this.rbSpecificNo.Size = new System.Drawing.Size(39, 17);
            this.rbSpecificNo.TabIndex = 3;
            this.rbSpecificNo.TabStop = true;
            this.rbSpecificNo.Text = "No";
            this.rbSpecificNo.UseVisualStyleBackColor = true;
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Location = new System.Drawing.Point(211, 27);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(43, 17);
            this.rbYes.TabIndex = 2;
            this.rbYes.TabStop = true;
            this.rbYes.Text = "Yes";
            this.rbYes.UseVisualStyleBackColor = true;
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(154, 27);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(39, 17);
            this.rbNo.TabIndex = 1;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Include closed yarn orders";
            // 
            // chkQASummary
            // 
            this.chkQASummary.AutoSize = true;
            this.chkQASummary.Location = new System.Drawing.Point(250, 145);
            this.chkQASummary.Name = "chkQASummary";
            this.chkQASummary.Size = new System.Drawing.Size(122, 17);
            this.chkQASummary.TabIndex = 4;
            this.chkQASummary.Text = "QA Summary Format";
            this.chkQASummary.UseVisualStyleBackColor = true;
            // 
            // frmYarnProdSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 354);
            this.Controls.Add(this.chkQASummary);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmYarnProdSel";
            this.Text = "Yarn Production Selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSpecificOrder;
        private System.Windows.Forms.RadioButton rbSpecificYes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbSpecificNo;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkQASummary;
    }
}