namespace Knitting
{
    partial class frmGreigeReceived3P
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
            this.cmbCommissionC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustDeliveryDoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFabricType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGRNNumber = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNoOfPieces = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCustomerOrder = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(663, 549);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Commission Customer";
            // 
            // cmbCommissionC
            // 
            this.cmbCommissionC.FormattingEnabled = true;
            this.cmbCommissionC.Location = new System.Drawing.Point(327, 92);
            this.cmbCommissionC.Name = "cmbCommissionC";
            this.cmbCommissionC.Size = new System.Drawing.Size(121, 21);
            this.cmbCommissionC.TabIndex = 3;
            this.cmbCommissionC.SelectedIndexChanged += new System.EventHandler(this.cmbCommissionC_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer Delivery Document";
            // 
            // txtCustDeliveryDoc
            // 
            this.txtCustDeliveryDoc.Location = new System.Drawing.Point(327, 135);
            this.txtCustDeliveryDoc.Name = "txtCustDeliveryDoc";
            this.txtCustDeliveryDoc.Size = new System.Drawing.Size(257, 20);
            this.txtCustDeliveryDoc.TabIndex = 4;
            this.txtCustDeliveryDoc.TextChanged += new System.EventHandler(this.txt);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "To Store";
            // 
            // cmbStore
            // 
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(327, 199);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(185, 21);
            this.cmbStore.TabIndex = 6;
            this.cmbStore.SelectedIndexChanged += new System.EventHandler(this.cmbStore_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fabric Type";
            // 
            // cmbFabricType
            // 
            this.cmbFabricType.FormattingEnabled = true;
            this.cmbFabricType.Location = new System.Drawing.Point(327, 243);
            this.cmbFabricType.Name = "cmbFabricType";
            this.cmbFabricType.Size = new System.Drawing.Size(185, 21);
            this.cmbFabricType.TabIndex = 7;
            this.cmbFabricType.SelectedIndexChanged += new System.EventHandler(this.cmbFabricType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Comments";
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(327, 284);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(300, 20);
            this.txtComments.TabIndex = 9;
            this.txtComments.TextChanged += new System.EventHandler(this.txt);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "GRN Number";
            // 
            // txtGRNNumber
            // 
            this.txtGRNNumber.Location = new System.Drawing.Point(327, 15);
            this.txtGRNNumber.Name = "txtGRNNumber";
            this.txtGRNNumber.ReadOnly = true;
            this.txtGRNNumber.Size = new System.Drawing.Size(100, 20);
            this.txtGRNNumber.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(152, 365);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(553, 178);
            this.dataGridView1.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(158, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Please enter the number of pieces";
            // 
            // txtNoOfPieces
            // 
            this.txtNoOfPieces.Location = new System.Drawing.Point(412, 324);
            this.txtNoOfPieces.Name = "txtNoOfPieces";
            this.txtNoOfPieces.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfPieces.TabIndex = 10;
            this.txtNoOfPieces.TextChanged += new System.EventHandler(this.txt);
            this.txtNoOfPieces.Leave += new System.EventHandler(this.TxtNoofPieces_OnLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(532, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Grade";
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(605, 241);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(100, 20);
            this.txtGrade.TabIndex = 8;
            this.txtGrade.TextChanged += new System.EventHandler(this.txt);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(149, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Date Received";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(327, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(132, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Customer Order Number";
            // 
            // txtCustomerOrder
            // 
            this.txtCustomerOrder.Location = new System.Drawing.Point(327, 167);
            this.txtCustomerOrder.Name = "txtCustomerOrder";
            this.txtCustomerOrder.Size = new System.Drawing.Size(185, 20);
            this.txtCustomerOrder.TabIndex = 5;
            // 
            // frmGreigeReceived3P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 584);
            this.Controls.Add(this.txtCustomerOrder);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNoOfPieces);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtGRNNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbFabricType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustDeliveryDoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCommissionC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmGreigeReceived3P";
            this.Text = "Greige Received from 3 rd Parties Commission dyeing";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCommissionC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustDeliveryDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFabricType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGRNNumber;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNoOfPieces;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCustomerOrder;
    }
}