namespace Knitting
{
    partial class frmCommKnitting
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
            this.txtTotalGrossWeight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalNettWeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGrnNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rtbComments = new System.Windows.Forms.RichTextBox();
            this.dtpDateReceived = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerDoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCommissionCustomers = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTotalGrossWeight
            // 
            this.txtTotalGrossWeight.Location = new System.Drawing.Point(461, 382);
            this.txtTotalGrossWeight.Name = "txtTotalGrossWeight";
            this.txtTotalGrossWeight.ReadOnly = true;
            this.txtTotalGrossWeight.Size = new System.Drawing.Size(100, 20);
            this.txtTotalGrossWeight.TabIndex = 51;
            this.txtTotalGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(348, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Total Gross Weight";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(121, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Total Nett Weight";
            // 
            // txtTotalNettWeight
            // 
            this.txtTotalNettWeight.Location = new System.Drawing.Point(233, 382);
            this.txtTotalNettWeight.Name = "txtTotalNettWeight";
            this.txtTotalNettWeight.ReadOnly = true;
            this.txtTotalNettWeight.Size = new System.Drawing.Size(100, 20);
            this.txtTotalNettWeight.TabIndex = 48;
            this.txtTotalNettWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Comments";
            // 
            // txtGrnNumber
            // 
            this.txtGrnNumber.Location = new System.Drawing.Point(211, 26);
            this.txtGrnNumber.Name = "txtGrnNumber";
            this.txtGrnNumber.ReadOnly = true;
            this.txtGrnNumber.Size = new System.Drawing.Size(100, 20);
            this.txtGrnNumber.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Grn Number";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(43, 173);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(571, 190);
            this.dataGridView1.TabIndex = 44;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // rtbComments
            // 
            this.rtbComments.Location = new System.Drawing.Point(218, 432);
            this.rtbComments.Name = "rtbComments";
            this.rtbComments.Size = new System.Drawing.Size(242, 106);
            this.rtbComments.TabIndex = 43;
            this.rtbComments.Text = "";
            // 
            // dtpDateReceived
            // 
            this.dtpDateReceived.Location = new System.Drawing.Point(211, 131);
            this.dtpDateReceived.Name = "dtpDateReceived";
            this.dtpDateReceived.Size = new System.Drawing.Size(122, 20);
            this.dtpDateReceived.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Receipt Date";
            // 
            // txtCustomerDoc
            // 
            this.txtCustomerDoc.Location = new System.Drawing.Point(211, 97);
            this.txtCustomerDoc.Name = "txtCustomerDoc";
            this.txtCustomerDoc.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerDoc.TabIndex = 40;
            this.txtCustomerDoc.TextChanged += new System.EventHandler(this.txtCustomerDoc_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Supplier Document";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Supplier";
            // 
            // cmbCommissionCustomers
            // 
            this.cmbCommissionCustomers.FormattingEnabled = true;
            this.cmbCommissionCustomers.Location = new System.Drawing.Point(211, 62);
            this.cmbCommissionCustomers.Name = "cmbCommissionCustomers";
            this.cmbCommissionCustomers.Size = new System.Drawing.Size(169, 21);
            this.cmbCommissionCustomers.TabIndex = 37;
            this.cmbCommissionCustomers.SelectedIndexChanged += new System.EventHandler(this.cmbCommissionCustomers_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(531, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 53;
            this.button1.Text = "Stock Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(531, 496);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 23);
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmCommKnitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 569);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTotalGrossWeight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTotalNettWeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtGrnNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.rtbComments);
            this.Controls.Add(this.dtpDateReceived);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustomerDoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCommissionCustomers);
            this.Name = "frmCommKnitting";
            this.Text = "frmCommKnitting";
            this.Load += new System.EventHandler(this.frmCommKnitting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTotalGrossWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalNettWeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGrnNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox rtbComments;
        private System.Windows.Forms.DateTimePicker dtpDateReceived;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustomerDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCommissionCustomers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSave;
    }
}