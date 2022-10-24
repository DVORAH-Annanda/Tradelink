namespace DyeHouse
{
    partial class frmCommissionDeliveries
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
            this.TxtTransNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.cmboCustomer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboBatchesReprint = new DyeHouse.CheckComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCommissionDeliveriesReprint = new System.Windows.Forms.RadioButton();
            this.rbCommissionKnitandDye = new System.Windows.Forms.RadioButton();
            this.rbCommissionDyeing = new System.Windows.Forms.RadioButton();
            this.cmboCurrentBatches = new DyeHouse.CheckComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Number";
            // 
            // TxtTransNumber
            // 
            this.TxtTransNumber.Location = new System.Drawing.Point(176, 13);
            this.TxtTransNumber.Name = "TxtTransNumber";
            this.TxtTransNumber.ReadOnly = true;
            this.TxtTransNumber.Size = new System.Drawing.Size(100, 20);
            this.TxtTransNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transaction Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(176, 55);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(139, 20);
            this.dtpTransDate.TabIndex = 3;
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(176, 97);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(169, 21);
            this.cmboCustomer.TabIndex = 4;
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(667, 600);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current Batches";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.cmboBatchesReprint);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmboCurrentBatches);
            this.groupBox1.Controls.Add(this.TxtTransNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpTransDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmboCustomer);
            this.groupBox1.Location = new System.Drawing.Point(105, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 502);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // cmboBatchesReprint
            // 
            this.cmboBatchesReprint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboBatchesReprint.FormattingEnabled = true;
            this.cmboBatchesReprint.Location = new System.Drawing.Point(176, 154);
            this.cmboBatchesReprint.Name = "cmboBatchesReprint";
            this.cmboBatchesReprint.Size = new System.Drawing.Size(211, 21);
            this.cmboBatchesReprint.TabIndex = 11;
            this.cmboBatchesReprint.Text = "Select Options";
            this.cmboBatchesReprint.SelectedIndexChanged += new System.EventHandler(this.cmboBatchesReprint_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCommissionDeliveriesReprint);
            this.groupBox2.Controls.Add(this.rbCommissionKnitandDye);
            this.groupBox2.Controls.Add(this.rbCommissionDyeing);
            this.groupBox2.Location = new System.Drawing.Point(47, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 93);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transaction Type";
            // 
            // rbCommissionDeliveriesReprint
            // 
            this.rbCommissionDeliveriesReprint.AutoSize = true;
            this.rbCommissionDeliveriesReprint.Location = new System.Drawing.Point(21, 59);
            this.rbCommissionDeliveriesReprint.Name = "rbCommissionDeliveriesReprint";
            this.rbCommissionDeliveriesReprint.Size = new System.Drawing.Size(159, 17);
            this.rbCommissionDeliveriesReprint.TabIndex = 2;
            this.rbCommissionDeliveriesReprint.TabStop = true;
            this.rbCommissionDeliveriesReprint.Text = "Commission Dyeing (Reprint)";
            this.rbCommissionDeliveriesReprint.UseVisualStyleBackColor = true;
            this.rbCommissionDeliveriesReprint.CheckedChanged += new System.EventHandler(this.rbCommissionDeliveriesReprint_CheckedChanged);
            // 
            // rbCommissionKnitandDye
            // 
            this.rbCommissionKnitandDye.AutoSize = true;
            this.rbCommissionKnitandDye.Location = new System.Drawing.Point(161, 26);
            this.rbCommissionKnitandDye.Name = "rbCommissionKnitandDye";
            this.rbCommissionKnitandDye.Size = new System.Drawing.Size(175, 17);
            this.rbCommissionKnitandDye.TabIndex = 1;
            this.rbCommissionKnitandDye.Text = "Commission Knitting and Dyeing";
            this.rbCommissionKnitandDye.UseVisualStyleBackColor = true;
            this.rbCommissionKnitandDye.CheckedChanged += new System.EventHandler(this.rbCommissionKnitandDye_CheckedChanged);
            // 
            // rbCommissionDyeing
            // 
            this.rbCommissionDyeing.AutoSize = true;
            this.rbCommissionDyeing.Checked = true;
            this.rbCommissionDyeing.Location = new System.Drawing.Point(21, 26);
            this.rbCommissionDyeing.Name = "rbCommissionDyeing";
            this.rbCommissionDyeing.Size = new System.Drawing.Size(116, 17);
            this.rbCommissionDyeing.TabIndex = 0;
            this.rbCommissionDyeing.TabStop = true;
            this.rbCommissionDyeing.Text = "Commission Dyeing";
            this.rbCommissionDyeing.UseVisualStyleBackColor = true;
            this.rbCommissionDyeing.CheckedChanged += new System.EventHandler(this.rbCommissionDyeing_CheckedChanged);
            // 
            // cmboCurrentBatches
            // 
            this.cmboCurrentBatches.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCurrentBatches.FormattingEnabled = true;
            this.cmboCurrentBatches.Location = new System.Drawing.Point(176, 154);
            this.cmboCurrentBatches.Name = "cmboCurrentBatches";
            this.cmboCurrentBatches.Size = new System.Drawing.Size(211, 21);
            this.cmboCurrentBatches.TabIndex = 9;
            this.cmboCurrentBatches.Text = "Select Options";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(196, 538);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(307, 96);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 541);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Notes";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 294);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(527, 190);
            this.dataGridView1.TabIndex = 12;
            // 
            // frmCommissionDeliveries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 646);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmCommissionDeliveries";
            this.Text = "Commission Deliveries";
            this.Load += new System.EventHandler(this.frmCommissionDeliveries_Load);
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
        private System.Windows.Forms.TextBox TxtTransNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.ComboBox cmboCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.ComboBox cmboCurrentBatches;
        private DyeHouse.CheckComboBox cmboCurrentBatches;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCommissionKnitandDye;
        private System.Windows.Forms.RadioButton rbCommissionDyeing;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbCommissionDeliveriesReprint;
        // private System.Windows.Forms.ComboBox cmboBatchesReprint;
        private DyeHouse.CheckComboBox cmboBatchesReprint;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}