namespace Knitting
{
    partial class frmReconKnitOrder
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkReOpenKnitOrder = new System.Windows.Forms.CheckBox();
            this.chkKnitOrderClosed = new System.Windows.Forms.CheckBox();
            this.chkYarnReturned = new System.Windows.Forms.CheckBox();
            this.chkProduction = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbKnitOrders = new System.Windows.Forms.ComboBox();
            this.btnReports = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(622, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date ";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(336, 33);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(137, 20);
            this.dtpDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time";
            // 
            // dtpTime
            // 
            this.dtpTime.Location = new System.Drawing.Point(336, 76);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(137, 20);
            this.dtpTime.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkReOpenKnitOrder);
            this.groupBox1.Controls.Add(this.chkKnitOrderClosed);
            this.groupBox1.Controls.Add(this.chkYarnReturned);
            this.groupBox1.Controls.Add(this.chkProduction);
            this.groupBox1.Location = new System.Drawing.Point(252, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 181);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confirmation";
            // 
            // chkReOpenKnitOrder
            // 
            this.chkReOpenKnitOrder.AutoSize = true;
            this.chkReOpenKnitOrder.Location = new System.Drawing.Point(8, 148);
            this.chkReOpenKnitOrder.Name = "chkReOpenKnitOrder";
            this.chkReOpenKnitOrder.Size = new System.Drawing.Size(114, 17);
            this.chkReOpenKnitOrder.TabIndex = 3;
            this.chkReOpenKnitOrder.Text = "Reopen Knit Order";
            this.chkReOpenKnitOrder.UseVisualStyleBackColor = true;
            this.chkReOpenKnitOrder.CheckedChanged += new System.EventHandler(this.chkReOpenKnitOrder_CheckedChanged);
            // 
            // chkKnitOrderClosed
            // 
            this.chkKnitOrderClosed.AutoSize = true;
            this.chkKnitOrderClosed.Location = new System.Drawing.Point(8, 108);
            this.chkKnitOrderClosed.Name = "chkKnitOrderClosed";
            this.chkKnitOrderClosed.Size = new System.Drawing.Size(108, 17);
            this.chkKnitOrderClosed.TabIndex = 2;
            this.chkKnitOrderClosed.Text = "Knit Order Closed";
            this.chkKnitOrderClosed.UseVisualStyleBackColor = true;
            // 
            // chkYarnReturned
            // 
            this.chkYarnReturned.AutoSize = true;
            this.chkYarnReturned.Location = new System.Drawing.Point(8, 68);
            this.chkYarnReturned.Name = "chkYarnReturned";
            this.chkYarnReturned.Size = new System.Drawing.Size(95, 17);
            this.chkYarnReturned.TabIndex = 1;
            this.chkYarnReturned.Text = "Yarn Returned";
            this.chkYarnReturned.UseVisualStyleBackColor = true;
            // 
            // chkProduction
            // 
            this.chkProduction.AutoSize = true;
            this.chkProduction.Location = new System.Drawing.Point(8, 28);
            this.chkProduction.Name = "chkProduction";
            this.chkProduction.Size = new System.Drawing.Size(137, 17);
            this.chkProduction.TabIndex = 0;
            this.chkProduction.Text = "All Production Captured";
            this.chkProduction.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Knit Orders";
            // 
            // cmbKnitOrders
            // 
            this.cmbKnitOrders.FormattingEnabled = true;
            this.cmbKnitOrders.Location = new System.Drawing.Point(336, 115);
            this.cmbKnitOrders.Name = "cmbKnitOrders";
            this.cmbKnitOrders.Size = new System.Drawing.Size(121, 21);
            this.cmbKnitOrders.TabIndex = 7;
            this.cmbKnitOrders.SelectedIndexChanged += new System.EventHandler(this.cmbKnitOrders_SelectedIndexChanged);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(622, 316);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(75, 23);
            this.btnReports.TabIndex = 8;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // frmReconKnitOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 389);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.cmbKnitOrders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmReconKnitOrder";
            this.Text = "Reconcile Knit Order";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkReOpenKnitOrder;
        private System.Windows.Forms.CheckBox chkKnitOrderClosed;
        private System.Windows.Forms.CheckBox chkYarnReturned;
        private System.Windows.Forms.CheckBox chkProduction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbKnitOrders;
        private System.Windows.Forms.Button btnReports;
    }
}