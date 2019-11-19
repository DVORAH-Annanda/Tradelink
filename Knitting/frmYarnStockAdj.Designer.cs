namespace Knitting
{
    partial class frmYarnStockAdj
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdjustmentNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb3rdParty = new System.Windows.Forms.RadioButton();
            this.rbCommission = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.oCmboTransactions = new System.Windows.Forms.ComboBox();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbOwnYarn = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please select a date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(320, 47);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(135, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Yarn Adjustment Number";
            // 
            // txtAdjustmentNo
            // 
            this.txtAdjustmentNo.Location = new System.Drawing.Point(320, 6);
            this.txtAdjustmentNo.Name = "txtAdjustmentNo";
            this.txtAdjustmentNo.ReadOnly = true;
            this.txtAdjustmentNo.Size = new System.Drawing.Size(100, 20);
            this.txtAdjustmentNo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Approved by";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new System.Drawing.Point(320, 233);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(258, 20);
            this.txtApprovedBy.TabIndex = 7;
            this.txtApprovedBy.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(147, 286);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(578, 195);
            this.dataGridView1.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(696, 579);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOwnYarn);
            this.groupBox1.Controls.Add(this.rb3rdParty);
            this.groupBox1.Controls.Add(this.rbCommission);
            this.groupBox1.Location = new System.Drawing.Point(163, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 81);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Receipts Detail";
            // 
            // rb3rdParty
            // 
            this.rb3rdParty.AutoSize = true;
            this.rb3rdParty.Location = new System.Drawing.Point(112, 46);
            this.rb3rdParty.Name = "rb3rdParty";
            this.rb3rdParty.Size = new System.Drawing.Size(120, 17);
            this.rb3rdParty.TabIndex = 1;
            this.rb3rdParty.TabStop = true;
            this.rb3rdParty.Text = "3rd Party Purchases";
            this.rb3rdParty.UseVisualStyleBackColor = true;
            this.rb3rdParty.CheckedChanged += new System.EventHandler(this.rb3rdParty_CheckedChanged);
            // 
            // rbCommission
            // 
            this.rbCommission.AutoSize = true;
            this.rbCommission.Location = new System.Drawing.Point(112, 19);
            this.rbCommission.Name = "rbCommission";
            this.rbCommission.Size = new System.Drawing.Size(132, 17);
            this.rbCommission.TabIndex = 0;
            this.rbCommission.TabStop = true;
            this.rbCommission.Text = "Commission Customers";
            this.rbCommission.UseVisualStyleBackColor = true;
            this.rbCommission.CheckedChanged += new System.EventHandler(this.rbCommission_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Yarn Transaction Details";
            // 
            // oCmboTransactions
            // 
            this.oCmboTransactions.FormattingEnabled = true;
            this.oCmboTransactions.Location = new System.Drawing.Point(320, 184);
            this.oCmboTransactions.Name = "oCmboTransactions";
            this.oCmboTransactions.Size = new System.Drawing.Size(170, 21);
            this.oCmboTransactions.TabIndex = 12;
            this.oCmboTransactions.SelectedIndexChanged += new System.EventHandler(this.oCmboTransactions_SelectedIndexChanged);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(253, 506);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(346, 96);
            this.rtbNotes.TabIndex = 13;
            this.rtbNotes.Text = "";
            this.rtbNotes.TextChanged += new System.EventHandler(this.text_Changed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 509);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Notes";
            // 
            // rbOwnYarn
            // 
            this.rbOwnYarn.AutoSize = true;
            this.rbOwnYarn.Location = new System.Drawing.Point(274, 19);
            this.rbOwnYarn.Name = "rbOwnYarn";
            this.rbOwnYarn.Size = new System.Drawing.Size(72, 17);
            this.rbOwnYarn.TabIndex = 2;
            this.rbOwnYarn.TabStop = true;
            this.rbOwnYarn.Text = "Own Yarn";
            this.rbOwnYarn.UseVisualStyleBackColor = true;
            this.rbOwnYarn.CheckedChanged += new System.EventHandler(this.rbOwnYarn_CheckedChanged);
            // 
            // frmYarnStockAdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 621);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.oCmboTransactions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtApprovedBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAdjustmentNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "frmYarnStockAdj";
            this.Text = "Yarn Stock Adjustment";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdjustmentNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb3rdParty;
        private System.Windows.Forms.RadioButton rbCommission;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox oCmboTransactions;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbOwnYarn;
    }
}