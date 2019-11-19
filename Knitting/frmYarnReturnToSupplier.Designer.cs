namespace Knitting
{
    partial class frmYarnReturnToSupplier
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
            this.btnSave = new System.Windows.Forms.Button();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.dtpDateReceived = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.cmboYarnOrder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTotalGrossWeight
            // 
            this.txtTotalGrossWeight.Location = new System.Drawing.Point(506, 356);
            this.txtTotalGrossWeight.Name = "txtTotalGrossWeight";
            this.txtTotalGrossWeight.ReadOnly = true;
            this.txtTotalGrossWeight.Size = new System.Drawing.Size(100, 20);
            this.txtTotalGrossWeight.TabIndex = 54;
            this.txtTotalGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(391, 363);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Total Gross Weight";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(152, 363);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Total Nett Weight";
            // 
            // txtTotalNettWeight
            // 
            this.txtTotalNettWeight.Location = new System.Drawing.Point(265, 356);
            this.txtTotalNettWeight.Name = "txtTotalNettWeight";
            this.txtTotalNettWeight.ReadOnly = true;
            this.txtTotalNettWeight.Size = new System.Drawing.Size(100, 20);
            this.txtTotalNettWeight.TabIndex = 51;
            this.txtTotalNettWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 424);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Comments";
            // 
            // txtGrnNumber
            // 
            this.txtGrnNumber.Location = new System.Drawing.Point(201, 2);
            this.txtGrnNumber.Name = "txtGrnNumber";
            this.txtGrnNumber.ReadOnly = true;
            this.txtGrnNumber.Size = new System.Drawing.Size(100, 20);
            this.txtGrnNumber.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Delivery Note Number";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(489, 531);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 26);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(213, 421);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(242, 101);
            this.rtbNotes.TabIndex = 45;
            this.rtbNotes.Text = "";
            // 
            // dtpDateReceived
            // 
            this.dtpDateReceived.Location = new System.Drawing.Point(201, 66);
            this.dtpDateReceived.Name = "dtpDateReceived";
            this.dtpDateReceived.Size = new System.Drawing.Size(122, 20);
            this.dtpDateReceived.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Return  Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Approved by";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new System.Drawing.Point(201, 34);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(317, 20);
            this.txtApprovedBy.TabIndex = 56;
            this.txtApprovedBy.TextChanged += new System.EventHandler(this.txt);
            // 
            // cmboYarnOrder
            // 
            this.cmboYarnOrder.FormattingEnabled = true;
            this.cmboYarnOrder.Location = new System.Drawing.Point(201, 96);
            this.cmboYarnOrder.Name = "cmboYarnOrder";
            this.cmboYarnOrder.Size = new System.Drawing.Size(136, 21);
            this.cmboYarnOrder.TabIndex = 57;
            this.cmboYarnOrder.SelectedIndexChanged += new System.EventHandler(this.cmboYarnOrder_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Yarn Transaction No";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(54, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(657, 161);
            this.dataGridView1.TabIndex = 59;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            // 
            // frmYarnReturnToSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 577);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboYarnOrder);
            this.Controls.Add(this.txtApprovedBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalGrossWeight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTotalNettWeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtGrnNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.dtpDateReceived);
            this.Controls.Add(this.label3);
            this.Name = "frmYarnReturnToSupplier";
            this.Text = "Yarn returned to suppliers";
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.DateTimePicker dtpDateReceived;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.ComboBox cmboYarnOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}