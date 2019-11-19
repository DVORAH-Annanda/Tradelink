namespace Spinning
{
    partial class frmYarnOrderAuditTrail
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cmboYarnOrder = new Spinning.CheckComboBox();
            this.cmboYarnType = new Spinning.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbYarnOrder = new System.Windows.Forms.RadioButton();
            this.rbKnitOrder = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yarn Type";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Yarn Order";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(527, 401);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "From Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(284, 31);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(153, 20);
            this.dtpFromDate.TabIndex = 7;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(284, 93);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(153, 20);
            this.dtpToDate.TabIndex = 8;
            // 
            // cmboYarnOrder
            // 
            this.cmboYarnOrder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboYarnOrder.FormattingEnabled = true;
            this.cmboYarnOrder.Location = new System.Drawing.Point(284, 218);
            this.cmboYarnOrder.Name = "cmboYarnOrder";
            this.cmboYarnOrder.Size = new System.Drawing.Size(153, 21);
            this.cmboYarnOrder.TabIndex = 2;
            this.cmboYarnOrder.Text = "Select Options";
            // 
            // cmboYarnType
            // 
            this.cmboYarnType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboYarnType.FormattingEnabled = true;
            this.cmboYarnType.Location = new System.Drawing.Point(284, 155);
            this.cmboYarnType.Name = "cmboYarnType";
            this.cmboYarnType.Size = new System.Drawing.Size(153, 21);
            this.cmboYarnType.TabIndex = 0;
            this.cmboYarnType.Text = "Select Options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbKnitOrder);
            this.groupBox1.Controls.Add(this.rbYarnOrder);
            this.groupBox1.Location = new System.Drawing.Point(237, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // rbYarnOrder
            // 
            this.rbYarnOrder.AutoSize = true;
            this.rbYarnOrder.Checked = true;
            this.rbYarnOrder.Location = new System.Drawing.Point(47, 20);
            this.rbYarnOrder.Name = "rbYarnOrder";
            this.rbYarnOrder.Size = new System.Drawing.Size(126, 17);
            this.rbYarnOrder.TabIndex = 0;
            this.rbYarnOrder.TabStop = true;
            this.rbYarnOrder.Text = "Yarn Order Audit Trail";
            this.rbYarnOrder.UseVisualStyleBackColor = true;
            // 
            // rbKnitOrder
            // 
            this.rbKnitOrder.AutoSize = true;
            this.rbKnitOrder.Location = new System.Drawing.Point(47, 55);
            this.rbKnitOrder.Name = "rbKnitOrder";
            this.rbKnitOrder.Size = new System.Drawing.Size(160, 17);
            this.rbKnitOrder.TabIndex = 1;
            this.rbKnitOrder.Text = "Greige Production Audit Trail";
            this.rbKnitOrder.UseVisualStyleBackColor = true;
            // 
            // frmYarnOrderAuditTrail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 452);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboYarnOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboYarnType);
            this.Name = "frmYarnOrderAuditTrail";
            this.Text = "Yarn Order Audit Trail";
            this.Load += new System.EventHandler(this.frmYarnOrderAuditTrail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.ComboBox cmboYarnType;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ComboBox cmboYarnOrder;
        private new Spinning.CheckComboBox cmboYarnType;
        private new Spinning.CheckComboBox cmboYarnOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbKnitOrder;
        private System.Windows.Forms.RadioButton rbYarnOrder;
    }
}