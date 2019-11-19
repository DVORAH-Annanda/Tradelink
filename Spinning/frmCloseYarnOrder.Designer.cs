namespace Spinning
{
    partial class frmCloseYarnOrder
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
            this.cmbYarnOrders = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYarnOrderQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProducedToDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBalToBeProduced = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCloseOrder = new System.Windows.Forms.CheckBox();
            this.chkReinstateOrder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select a Yarn Order";
            // 
            // cmbYarnOrders
            // 
            this.cmbYarnOrders.FormattingEnabled = true;
            this.cmbYarnOrders.Location = new System.Drawing.Point(244, 43);
            this.cmbYarnOrders.Name = "cmbYarnOrders";
            this.cmbYarnOrders.Size = new System.Drawing.Size(205, 21);
            this.cmbYarnOrders.TabIndex = 1;
            this.cmbYarnOrders.SelectedIndexChanged += new System.EventHandler(this.cmbYarnOrders_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(586, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBalToBeProduced);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtProducedToDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtYarnOrderQty);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(97, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 157);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Order Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Order Quantity";
            // 
            // txtYarnOrderQty
            // 
            this.txtYarnOrderQty.Location = new System.Drawing.Point(268, 30);
            this.txtYarnOrderQty.Name = "txtYarnOrderQty";
            this.txtYarnOrderQty.Size = new System.Drawing.Size(100, 20);
            this.txtYarnOrderQty.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Produced To Date";
            // 
            // txtProducedToDate
            // 
            this.txtProducedToDate.Location = new System.Drawing.Point(268, 71);
            this.txtProducedToDate.Name = "txtProducedToDate";
            this.txtProducedToDate.Size = new System.Drawing.Size(100, 20);
            this.txtProducedToDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Balance To be Produced";
            // 
            // txtBalToBeProduced
            // 
            this.txtBalToBeProduced.Location = new System.Drawing.Point(268, 112);
            this.txtBalToBeProduced.Name = "txtBalToBeProduced";
            this.txtBalToBeProduced.Size = new System.Drawing.Size(100, 20);
            this.txtBalToBeProduced.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkReinstateOrder);
            this.groupBox2.Controls.Add(this.chkCloseOrder);
            this.groupBox2.Location = new System.Drawing.Point(97, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 67);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action Steps";
            // 
            // chkCloseOrder
            // 
            this.chkCloseOrder.AutoSize = true;
            this.chkCloseOrder.Location = new System.Drawing.Point(51, 29);
            this.chkCloseOrder.Name = "chkCloseOrder";
            this.chkCloseOrder.Size = new System.Drawing.Size(81, 17);
            this.chkCloseOrder.TabIndex = 0;
            this.chkCloseOrder.Text = "Close Order";
            this.chkCloseOrder.UseVisualStyleBackColor = true;
            // 
            // chkReinstateOrder
            // 
            this.chkReinstateOrder.AutoSize = true;
            this.chkReinstateOrder.Location = new System.Drawing.Point(245, 29);
            this.chkReinstateOrder.Name = "chkReinstateOrder";
            this.chkReinstateOrder.Size = new System.Drawing.Size(100, 17);
            this.chkReinstateOrder.TabIndex = 1;
            this.chkReinstateOrder.Text = "Reinstate Order";
            this.chkReinstateOrder.UseVisualStyleBackColor = true;
            // 
            // frmCloseYarnOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbYarnOrders);
            this.Controls.Add(this.label1);
            this.Name = "frmCloseYarnOrder";
            this.Text = "Yarn Order Status";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYarnOrders;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBalToBeProduced;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProducedToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYarnOrderQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkReinstateOrder;
        private System.Windows.Forms.CheckBox chkCloseOrder;
    }
}