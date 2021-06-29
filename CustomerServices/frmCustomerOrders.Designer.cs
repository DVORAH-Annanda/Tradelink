namespace CustomerServices
{
    partial class frmCustomerOrders
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
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbCopyOrder = new System.Windows.Forms.RadioButton();
            this.rbProvisional = new System.Windows.Forms.RadioButton();
            this.rbSpecialNo = new System.Windows.Forms.RadioButton();
            this.rbSpecialYes = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOrderClosed = new System.Windows.Forms.RadioButton();
            this.rbOrderActive = new System.Windows.Forms.RadioButton();
            this.cmboCurrentOrders = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpCustOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustomerPO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpRequiredDate = new System.Windows.Forms.DateTimePicker();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(721, 677);
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
            this.label1.Location = new System.Drawing.Point(89, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customers";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(233, 8);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(248, 21);
            this.cmboCustomers.TabIndex = 1;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmboCurrentOrders);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpCustOrderDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCustomerPO);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpRequiredDate);
            this.groupBox1.Controls.Add(this.cmboCustomers);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(99, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 312);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbCopyOrder);
            this.groupBox3.Controls.Add(this.rbProvisional);
            this.groupBox3.Controls.Add(this.rbSpecialNo);
            this.groupBox3.Controls.Add(this.rbSpecialYes);
            this.groupBox3.Location = new System.Drawing.Point(346, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 93);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Order Classification";
            // 
            // rbCopyOrder
            // 
            this.rbCopyOrder.AutoSize = true;
            this.rbCopyOrder.Location = new System.Drawing.Point(133, 57);
            this.rbCopyOrder.Name = "rbCopyOrder";
            this.rbCopyOrder.Size = new System.Drawing.Size(117, 17);
            this.rbCopyOrder.TabIndex = 3;
            this.rbCopyOrder.Text = "Copy Existing Order";
            this.rbCopyOrder.UseVisualStyleBackColor = true;
            this.rbCopyOrder.CheckedChanged += new System.EventHandler(this.rbCopyOrder_CheckedChanged);
            // 
            // rbProvisional
            // 
            this.rbProvisional.AutoSize = true;
            this.rbProvisional.Location = new System.Drawing.Point(38, 57);
            this.rbProvisional.Name = "rbProvisional";
            this.rbProvisional.Size = new System.Drawing.Size(76, 17);
            this.rbProvisional.TabIndex = 2;
            this.rbProvisional.Text = "Provisional";
            this.rbProvisional.UseVisualStyleBackColor = true;
            this.rbProvisional.CheckedChanged += new System.EventHandler(this.rbProvisional_CheckedChanged);
            // 
            // rbSpecialNo
            // 
            this.rbSpecialNo.AutoSize = true;
            this.rbSpecialNo.Checked = true;
            this.rbSpecialNo.Location = new System.Drawing.Point(133, 20);
            this.rbSpecialNo.Name = "rbSpecialNo";
            this.rbSpecialNo.Size = new System.Drawing.Size(97, 17);
            this.rbSpecialNo.TabIndex = 1;
            this.rbSpecialNo.TabStop = true;
            this.rbSpecialNo.Text = "Standard Order";
            this.rbSpecialNo.UseVisualStyleBackColor = true;
            this.rbSpecialNo.CheckedChanged += new System.EventHandler(this.rbSpecialNo_CheckedChanged);
            // 
            // rbSpecialYes
            // 
            this.rbSpecialYes.AutoSize = true;
            this.rbSpecialYes.Location = new System.Drawing.Point(38, 20);
            this.rbSpecialYes.Name = "rbSpecialYes";
            this.rbSpecialYes.Size = new System.Drawing.Size(81, 17);
            this.rbSpecialYes.TabIndex = 0;
            this.rbSpecialYes.Text = "Special Yes";
            this.rbSpecialYes.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOrderClosed);
            this.groupBox2.Controls.Add(this.rbOrderActive);
            this.groupBox2.Location = new System.Drawing.Point(88, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 93);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Status";
            // 
            // rbOrderClosed
            // 
            this.rbOrderClosed.AutoSize = true;
            this.rbOrderClosed.Location = new System.Drawing.Point(144, 20);
            this.rbOrderClosed.Name = "rbOrderClosed";
            this.rbOrderClosed.Size = new System.Drawing.Size(57, 17);
            this.rbOrderClosed.TabIndex = 1;
            this.rbOrderClosed.Text = "Closed";
            this.rbOrderClosed.UseVisualStyleBackColor = true;
            this.rbOrderClosed.CheckedChanged += new System.EventHandler(this.rbOrderClosed_CheckedChanged);
            // 
            // rbOrderActive
            // 
            this.rbOrderActive.AutoSize = true;
            this.rbOrderActive.Checked = true;
            this.rbOrderActive.Location = new System.Drawing.Point(68, 20);
            this.rbOrderActive.Name = "rbOrderActive";
            this.rbOrderActive.Size = new System.Drawing.Size(55, 17);
            this.rbOrderActive.TabIndex = 0;
            this.rbOrderActive.TabStop = true;
            this.rbOrderActive.Text = "Active";
            this.rbOrderActive.UseVisualStyleBackColor = true;
            this.rbOrderActive.CheckedChanged += new System.EventHandler(this.rbOrderActive_CheckedChanged);
            // 
            // cmboCurrentOrders
            // 
            this.cmboCurrentOrders.FormattingEnabled = true;
            this.cmboCurrentOrders.Location = new System.Drawing.Point(233, 172);
            this.cmboCurrentOrders.Name = "cmboCurrentOrders";
            this.cmboCurrentOrders.Size = new System.Drawing.Size(195, 21);
            this.cmboCurrentOrders.TabIndex = 6;
            this.cmboCurrentOrders.SelectedIndexChanged += new System.EventHandler(this.cmboCurrentOrders_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(89, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Customer Current Orders";
            // 
            // dtpCustOrderDate
            // 
            this.dtpCustOrderDate.Location = new System.Drawing.Point(233, 94);
            this.dtpCustOrderDate.Name = "dtpCustOrderDate";
            this.dtpCustOrderDate.Size = new System.Drawing.Size(154, 20);
            this.dtpCustOrderDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Customer Purchase Order";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Customer Order Date";
            // 
            // txtCustomerPO
            // 
            this.txtCustomerPO.Location = new System.Drawing.Point(233, 51);
            this.txtCustomerPO.Name = "txtCustomerPO";
            this.txtCustomerPO.Size = new System.Drawing.Size(186, 20);
            this.txtCustomerPO.TabIndex = 4;
            this.txtCustomerPO.TextChanged += new System.EventHandler(this.txtCustomerPO_TextChanged);
            this.txtCustomerPO.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerPO_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Customer Required Date";
            // 
            // dtpRequiredDate
            // 
            this.dtpRequiredDate.Location = new System.Drawing.Point(233, 136);
            this.dtpRequiredDate.Name = "dtpRequiredDate";
            this.dtpRequiredDate.Size = new System.Drawing.Size(154, 20);
            this.dtpRequiredDate.TabIndex = 3;
            this.dtpRequiredDate.ValueChanged += new System.EventHandler(this.dtpRequiredDate_ValueChanged);
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransNumber.Location = new System.Drawing.Point(332, 338);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(154, 22);
            this.txtTransNumber.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Transaction Number";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(98, 379);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(745, 263);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(173, 399);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(579, 224);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView2_RowsAdded);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(721, 648);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmCustomerOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 711);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTransNumber);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmCustomerOrders";
            this.Text = "Customer Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCustomerOrders_FormClosing);
            this.Load += new System.EventHandler(this.frmCustomerOrders_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCustomerPO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpRequiredDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtTransNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpCustOrderDate;
        private System.Windows.Forms.ComboBox cmboCurrentOrders;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOrderClosed;
        private System.Windows.Forms.RadioButton rbOrderActive;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbSpecialNo;
        private System.Windows.Forms.RadioButton rbSpecialYes;
        private System.Windows.Forms.RadioButton rbProvisional;
        private System.Windows.Forms.RadioButton rbCopyOrder;
    }
}