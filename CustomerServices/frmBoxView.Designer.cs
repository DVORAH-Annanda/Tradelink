namespace CustomerServices
{
    partial class frmBoxView
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
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkReturned = new System.Windows.Forms.CheckBox();
            this.chkInStock = new System.Windows.Forms.CheckBox();
            this.chkDelivered = new System.Windows.Forms.CheckBox();
            this.chkPicked = new System.Windows.Forms.CheckBox();
            this.txtBoxedQty = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPickingList = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDeliveryNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPoOrder = new System.Windows.Forms.TextBox();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDeliveryDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.txtWareHouse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkWriteOff = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Box Number";
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.Location = new System.Drawing.Point(216, 19);
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.Size = new System.Drawing.Size(195, 20);
            this.txtBoxNumber.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtBoxedQty);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtPickingList);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtCustomer);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDeliveryNote);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPoOrder);
            this.groupBox1.Controls.Add(this.txtGrade);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDeliveryDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtStyle);
            this.groupBox1.Controls.Add(this.txtWareHouse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(53, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 539);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Box View Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkWriteOff);
            this.groupBox2.Controls.Add(this.chkReturned);
            this.groupBox2.Controls.Add(this.chkInStock);
            this.groupBox2.Controls.Add(this.chkDelivered);
            this.groupBox2.Controls.Add(this.chkPicked);
            this.groupBox2.Location = new System.Drawing.Point(345, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 150);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // chkReturned
            // 
            this.chkReturned.AutoSize = true;
            this.chkReturned.Location = new System.Drawing.Point(14, 103);
            this.chkReturned.Name = "chkReturned";
            this.chkReturned.Size = new System.Drawing.Size(70, 17);
            this.chkReturned.TabIndex = 3;
            this.chkReturned.Text = "Returned";
            this.chkReturned.UseVisualStyleBackColor = true;
            // 
            // chkInStock
            // 
            this.chkInStock.AutoSize = true;
            this.chkInStock.Location = new System.Drawing.Point(14, 25);
            this.chkInStock.Name = "chkInStock";
            this.chkInStock.Size = new System.Drawing.Size(66, 17);
            this.chkInStock.TabIndex = 2;
            this.chkInStock.Text = "In Stock";
            this.chkInStock.UseVisualStyleBackColor = true;
            // 
            // chkDelivered
            // 
            this.chkDelivered.AutoSize = true;
            this.chkDelivered.Location = new System.Drawing.Point(14, 77);
            this.chkDelivered.Name = "chkDelivered";
            this.chkDelivered.Size = new System.Drawing.Size(71, 17);
            this.chkDelivered.TabIndex = 1;
            this.chkDelivered.Text = "Delivered";
            this.chkDelivered.UseVisualStyleBackColor = true;
            // 
            // chkPicked
            // 
            this.chkPicked.AutoSize = true;
            this.chkPicked.Location = new System.Drawing.Point(14, 51);
            this.chkPicked.Name = "chkPicked";
            this.chkPicked.Size = new System.Drawing.Size(59, 17);
            this.chkPicked.TabIndex = 0;
            this.chkPicked.Text = "Picked";
            this.chkPicked.UseVisualStyleBackColor = true;
            // 
            // txtBoxedQty
            // 
            this.txtBoxedQty.Location = new System.Drawing.Point(404, 233);
            this.txtBoxedQty.Name = "txtBoxedQty";
            this.txtBoxedQty.ReadOnly = true;
            this.txtBoxedQty.Size = new System.Drawing.Size(64, 20);
            this.txtBoxedQty.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(330, 236);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Boxed Qty\'s";
            // 
            // txtPickingList
            // 
            this.txtPickingList.Location = new System.Drawing.Point(176, 280);
            this.txtPickingList.Name = "txtPickingList";
            this.txtPickingList.ReadOnly = true;
            this.txtPickingList.Size = new System.Drawing.Size(141, 20);
            this.txtPickingList.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Whse Picking List";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(174, 487);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(299, 20);
            this.txtCustomer.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 490);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Customer";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 441);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "PO Order";
            // 
            // txtDeliveryNote
            // 
            this.txtDeliveryNote.Location = new System.Drawing.Point(174, 340);
            this.txtDeliveryNote.Name = "txtDeliveryNote";
            this.txtDeliveryNote.ReadOnly = true;
            this.txtDeliveryNote.Size = new System.Drawing.Size(152, 20);
            this.txtDeliveryNote.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Whse DeliveryNote No";
            // 
            // txtPoOrder
            // 
            this.txtPoOrder.Location = new System.Drawing.Point(174, 438);
            this.txtPoOrder.Name = "txtPoOrder";
            this.txtPoOrder.ReadOnly = true;
            this.txtPoOrder.Size = new System.Drawing.Size(197, 20);
            this.txtPoOrder.TabIndex = 14;
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(174, 233);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.ReadOnly = true;
            this.txtGrade.Size = new System.Drawing.Size(143, 20);
            this.txtGrade.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Delivery Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Grade";
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.Location = new System.Drawing.Point(174, 389);
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.ReadOnly = true;
            this.txtDeliveryDate.Size = new System.Drawing.Size(114, 20);
            this.txtDeliveryDate.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Size";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(174, 184);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(197, 20);
            this.txtSize.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Colour";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(174, 135);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(197, 20);
            this.txtColour.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Style";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(174, 86);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(197, 20);
            this.txtStyle.TabIndex = 6;
            // 
            // txtWareHouse
            // 
            this.txtWareHouse.Location = new System.Drawing.Point(174, 37);
            this.txtWareHouse.Name = "txtWareHouse";
            this.txtWareHouse.ReadOnly = true;
            this.txtWareHouse.Size = new System.Drawing.Size(265, 20);
            this.txtWareHouse.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "WareHouse";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(446, 22);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkWriteOff
            // 
            this.chkWriteOff.AutoSize = true;
            this.chkWriteOff.Location = new System.Drawing.Point(15, 129);
            this.chkWriteOff.Name = "chkWriteOff";
            this.chkWriteOff.Size = new System.Drawing.Size(74, 17);
            this.chkWriteOff.TabIndex = 4;
            this.chkWriteOff.Text = "Writen Off";
            this.chkWriteOff.UseVisualStyleBackColor = true;
            // 
            // frmBoxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 630);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBoxNumber);
            this.Controls.Add(this.label1);
            this.Name = "frmBoxView";
            this.Text = "Box Number View";
            this.Load += new System.EventHandler(this.frmBoxView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDeliveryNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPoOrder;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDeliveryDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtWareHouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPickingList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBoxedQty;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkReturned;
        private System.Windows.Forms.CheckBox chkInStock;
        private System.Windows.Forms.CheckBox chkDelivered;
        private System.Windows.Forms.CheckBox chkPicked;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkWriteOff;
    }
}