namespace Spinning
{
    partial class frmYarnSales
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
            this.txtDeliveryNoteNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboCustomerNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rtbAddress = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPalletNo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPalletWeightSold = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPalletWeightAvailable = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtConesSold = new System.Windows.Forms.TextBox();
            this.txtConesAvailable = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmboYarnOrder = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDeliveryNoteNumber
            // 
            this.txtDeliveryNoteNumber.Location = new System.Drawing.Point(375, 51);
            this.txtDeliveryNoteNumber.Name = "txtDeliveryNoteNumber";
            this.txtDeliveryNoteNumber.ReadOnly = true;
            this.txtDeliveryNoteNumber.Size = new System.Drawing.Size(100, 20);
            this.txtDeliveryNoteNumber.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Sales Note Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Please select a transaction date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(375, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(155, 20);
            this.dateTimePicker1.TabIndex = 16;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Please select a customer No";
            // 
            // cmboCustomerNo
            // 
            this.cmboCustomerNo.FormattingEnabled = true;
            this.cmboCustomerNo.Location = new System.Drawing.Point(375, 91);
            this.cmboCustomerNo.Name = "cmboCustomerNo";
            this.cmboCustomerNo.Size = new System.Drawing.Size(155, 21);
            this.cmboCustomerNo.TabIndex = 21;
            this.cmboCustomerNo.SelectedIndexChanged += new System.EventHandler(this.cmboCustomerNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Customer Order No";
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(375, 131);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerOrder.TabIndex = 23;
            this.txtCustomerOrder.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Customer Address";
            // 
            // rtbAddress
            // 
            this.rtbAddress.Location = new System.Drawing.Point(375, 180);
            this.rtbAddress.Name = "rtbAddress";
            this.rtbAddress.Size = new System.Drawing.Size(155, 53);
            this.rtbAddress.TabIndex = 25;
            this.rtbAddress.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Pallet Number";
            // 
            // cmbPalletNo
            // 
            this.cmbPalletNo.FormattingEnabled = true;
            this.cmbPalletNo.Location = new System.Drawing.Point(375, 319);
            this.cmbPalletNo.Name = "cmbPalletNo";
            this.cmbPalletNo.Size = new System.Drawing.Size(155, 21);
            this.cmbPalletNo.TabIndex = 27;
            this.cmbPalletNo.SelectedIndexChanged += new System.EventHandler(this.cmbPalletNo_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPalletWeightSold);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtPalletWeightAvailable);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtConesSold);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtConesAvailable);
            this.groupBox1.Location = new System.Drawing.Point(182, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 151);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pallet Details";
            // 
            // txtPalletWeightSold
            // 
            this.txtPalletWeightSold.Location = new System.Drawing.Point(171, 112);
            this.txtPalletWeightSold.Name = "txtPalletWeightSold";
            this.txtPalletWeightSold.Size = new System.Drawing.Size(100, 20);
            this.txtPalletWeightSold.TabIndex = 7;
            this.txtPalletWeightSold.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Pallet Weight Sold";
            // 
            // txtPalletWeightAvailable
            // 
            this.txtPalletWeightAvailable.Location = new System.Drawing.Point(171, 79);
            this.txtPalletWeightAvailable.Name = "txtPalletWeightAvailable";
            this.txtPalletWeightAvailable.ReadOnly = true;
            this.txtPalletWeightAvailable.Size = new System.Drawing.Size(100, 20);
            this.txtPalletWeightAvailable.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Pallet Weight Available to sell";
            // 
            // txtConesSold
            // 
            this.txtConesSold.Location = new System.Drawing.Point(171, 46);
            this.txtConesSold.Name = "txtConesSold";
            this.txtConesSold.Size = new System.Drawing.Size(100, 20);
            this.txtConesSold.TabIndex = 3;
            this.txtConesSold.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // txtConesAvailable
            // 
            this.txtConesAvailable.Location = new System.Drawing.Point(171, 13);
            this.txtConesAvailable.Name = "txtConesAvailable";
            this.txtConesAvailable.ReadOnly = true;
            this.txtConesAvailable.Size = new System.Drawing.Size(100, 20);
            this.txtConesAvailable.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cones Sold";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Cones Available To Sell";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(621, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(182, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Yarn Order";
            // 
            // cmboYarnOrder
            // 
            this.cmboYarnOrder.FormattingEnabled = true;
            this.cmboYarnOrder.Location = new System.Drawing.Point(375, 265);
            this.cmboYarnOrder.Name = "cmboYarnOrder";
            this.cmboYarnOrder.Size = new System.Drawing.Size(155, 21);
            this.cmboYarnOrder.TabIndex = 31;
            this.cmboYarnOrder.SelectedIndexChanged += new System.EventHandler(this.cmboYarnOrder_SelectedIndexChanged);
            // 
            // frmYarnSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 522);
            this.Controls.Add(this.cmboYarnOrder);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbPalletNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtbAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCustomerOrder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboCustomerNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeliveryNoteNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmYarnSales";
            this.Text = "Form Yarn Sales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sales_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDeliveryNoteNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboCustomerNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustomerOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPalletNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConesSold;
        private System.Windows.Forms.TextBox txtConesAvailable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboYarnOrder;
        private System.Windows.Forms.TextBox txtPalletWeightSold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPalletWeightAvailable;
        private System.Windows.Forms.Label label10;
    }
}