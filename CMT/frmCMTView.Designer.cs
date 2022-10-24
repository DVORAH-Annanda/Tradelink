namespace CMT
{
    partial class frmCMTView
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
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDeliveryNoteDate = new System.Windows.Forms.TextBox();
            this.txtDeliveryNote = new System.Windows.Forms.TextBox();
            this.txtPickingListDate = new System.Windows.Forms.TextBox();
            this.txtPickingList = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtToWareHouse = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rbOnHold = new System.Windows.Forms.RadioButton();
            this.txtTransnumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtLineNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTransferDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPanelStore = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWareHouse = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter a Cut Sheet Number";
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(210, 22);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.Size = new System.Drawing.Size(193, 20);
            this.txtCutSheet.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.rbOnHold);
            this.groupBox1.Controls.Add(this.txtTransnumber);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.txtLineNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.txtStyle);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTransferDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPanelStore);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(46, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 637);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDeliveryNoteDate);
            this.groupBox2.Controls.Add(this.txtDeliveryNote);
            this.groupBox2.Controls.Add(this.txtPickingListDate);
            this.groupBox2.Controls.Add(this.txtPickingList);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtToWareHouse);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(35, 469);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 152);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WareHouse Details";
            // 
            // txtDeliveryNoteDate
            // 
            this.txtDeliveryNoteDate.Location = new System.Drawing.Point(358, 113);
            this.txtDeliveryNoteDate.Name = "txtDeliveryNoteDate";
            this.txtDeliveryNoteDate.ReadOnly = true;
            this.txtDeliveryNoteDate.Size = new System.Drawing.Size(100, 20);
            this.txtDeliveryNoteDate.TabIndex = 9;
            // 
            // txtDeliveryNote
            // 
            this.txtDeliveryNote.Location = new System.Drawing.Point(116, 113);
            this.txtDeliveryNote.Name = "txtDeliveryNote";
            this.txtDeliveryNote.ReadOnly = true;
            this.txtDeliveryNote.Size = new System.Drawing.Size(100, 20);
            this.txtDeliveryNote.TabIndex = 8;
            // 
            // txtPickingListDate
            // 
            this.txtPickingListDate.Location = new System.Drawing.Point(361, 69);
            this.txtPickingListDate.Name = "txtPickingListDate";
            this.txtPickingListDate.ReadOnly = true;
            this.txtPickingListDate.Size = new System.Drawing.Size(100, 20);
            this.txtPickingListDate.TabIndex = 7;
            // 
            // txtPickingList
            // 
            this.txtPickingList.Location = new System.Drawing.Point(111, 69);
            this.txtPickingList.Name = "txtPickingList";
            this.txtPickingList.ReadOnly = true;
            this.txtPickingList.Size = new System.Drawing.Size(100, 20);
            this.txtPickingList.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(255, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Delivery Note Date";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Delivery Note No";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(255, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Picking List Date";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Picking List No";
            // 
            // txtToWareHouse
            // 
            this.txtToWareHouse.Location = new System.Drawing.Point(111, 26);
            this.txtToWareHouse.Name = "txtToWareHouse";
            this.txtToWareHouse.ReadOnly = true;
            this.txtToWareHouse.Size = new System.Drawing.Size(309, 20);
            this.txtToWareHouse.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "WareHouse";
            // 
            // rbOnHold
            // 
            this.rbOnHold.AutoSize = true;
            this.rbOnHold.Location = new System.Drawing.Point(417, 420);
            this.rbOnHold.Name = "rbOnHold";
            this.rbOnHold.Size = new System.Drawing.Size(64, 17);
            this.rbOnHold.TabIndex = 17;
            this.rbOnHold.TabStop = true;
            this.rbOnHold.Text = "On Hold";
            this.rbOnHold.UseVisualStyleBackColor = true;
            // 
            // txtTransnumber
            // 
            this.txtTransnumber.Location = new System.Drawing.Point(146, 132);
            this.txtTransnumber.Name = "txtTransnumber";
            this.txtTransnumber.ReadOnly = true;
            this.txtTransnumber.Size = new System.Drawing.Size(292, 20);
            this.txtTransnumber.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "PStore Del Number";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(78, 268);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(424, 121);
            this.dataGridView1.TabIndex = 12;
            // 
            // txtLineNo
            // 
            this.txtLineNo.Location = new System.Drawing.Point(146, 417);
            this.txtLineNo.Name = "txtLineNo";
            this.txtLineNo.ReadOnly = true;
            this.txtLineNo.Size = new System.Drawing.Size(244, 20);
            this.txtLineNo.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 424);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Issue To Line";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Size";
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(146, 224);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(292, 20);
            this.txtColour.TabIndex = 7;
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(146, 178);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(292, 20);
            this.txtStyle.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Colour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Style";
            // 
            // txtTransferDate
            // 
            this.txtTransferDate.Location = new System.Drawing.Point(146, 86);
            this.txtTransferDate.Name = "txtTransferDate";
            this.txtTransferDate.ReadOnly = true;
            this.txtTransferDate.Size = new System.Drawing.Size(158, 20);
            this.txtTransferDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date";
            // 
            // txtPanelStore
            // 
            this.txtPanelStore.Location = new System.Drawing.Point(146, 40);
            this.txtPanelStore.Name = "txtPanelStore";
            this.txtPanelStore.ReadOnly = true;
            this.txtPanelStore.Size = new System.Drawing.Size(292, 20);
            this.txtPanelStore.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "CMT ";
            // 
            // txtWareHouse
            // 
            this.txtWareHouse.Location = new System.Drawing.Point(235, 629);
            this.txtWareHouse.Name = "txtWareHouse";
            this.txtWareHouse.ReadOnly = true;
            this.txtWareHouse.Size = new System.Drawing.Size(292, 20);
            this.txtWareHouse.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(103, 632);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "To Warehouse";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(439, 20);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 17;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmCMTView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 731);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCutSheet);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtWareHouse);
            this.Controls.Add(this.label1);
            this.Name = "frmCMTView";
            this.Text = "CMT Cut Sheet View";
            this.Load += new System.EventHandler(this.frmCMTView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWareHouse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTransnumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtLineNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTransferDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPanelStore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbOnHold;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDeliveryNoteDate;
        private System.Windows.Forms.TextBox txtDeliveryNote;
        private System.Windows.Forms.TextBox txtPickingListDate;
        private System.Windows.Forms.TextBox txtPickingList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtToWareHouse;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSubmit;
    }
}