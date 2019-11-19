namespace Knitting
{
    partial class frmYarnAllocation
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
            this.cmboKnitOrders = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboYarnOrders = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOwnYarn = new System.Windows.Forms.RadioButton();
            this.RbThirdParty = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(494, 529);
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
            this.label1.Location = new System.Drawing.Point(149, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Knit Orders";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmboKnitOrders
            // 
            this.cmboKnitOrders.FormattingEnabled = true;
            this.cmboKnitOrders.Location = new System.Drawing.Point(260, 15);
            this.cmboKnitOrders.Name = "cmboKnitOrders";
            this.cmboKnitOrders.Size = new System.Drawing.Size(176, 21);
            this.cmboKnitOrders.TabIndex = 2;
            this.cmboKnitOrders.SelectedIndexChanged += new System.EventHandler(this.cmboKnitOrders_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Transaction Date";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Location = new System.Drawing.Point(260, 138);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(176, 20);
            this.dtpTransactionDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current Yarn Orders";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmboYarnOrders
            // 
            this.cmboYarnOrders.FormattingEnabled = true;
            this.cmboYarnOrders.Location = new System.Drawing.Point(260, 180);
            this.cmboYarnOrders.Name = "cmboYarnOrders";
            this.cmboYarnOrders.Size = new System.Drawing.Size(176, 21);
            this.cmboYarnOrders.TabIndex = 6;
            this.cmboYarnOrders.SelectedIndexChanged += new System.EventHandler(this.cmboYarnOrders_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 232);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(517, 265);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbThirdParty);
            this.groupBox1.Controls.Add(this.rbOwnYarn);
            this.groupBox1.Location = new System.Drawing.Point(197, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 53);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Source";
            // 
            // rbOwnYarn
            // 
            this.rbOwnYarn.AutoSize = true;
            this.rbOwnYarn.Location = new System.Drawing.Point(45, 19);
            this.rbOwnYarn.Name = "rbOwnYarn";
            this.rbOwnYarn.Size = new System.Drawing.Size(72, 17);
            this.rbOwnYarn.TabIndex = 0;
            this.rbOwnYarn.TabStop = true;
            this.rbOwnYarn.Text = "Own Yarn";
            this.rbOwnYarn.UseVisualStyleBackColor = true;
            // 
            // RbThirdParty
            // 
            this.RbThirdParty.AutoSize = true;
            this.RbThirdParty.Location = new System.Drawing.Point(154, 19);
            this.RbThirdParty.Name = "RbThirdParty";
            this.RbThirdParty.Size = new System.Drawing.Size(76, 17);
            this.RbThirdParty.TabIndex = 1;
            this.RbThirdParty.TabStop = true;
            this.RbThirdParty.Text = "Third Party";
            this.RbThirdParty.UseVisualStyleBackColor = true;
            this.RbThirdParty.CheckedChanged += new System.EventHandler(this.RbThirdParty_CheckedChanged);
            // 
            // frmYarnAllocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 576);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboYarnOrders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTransactionDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboKnitOrders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmYarnAllocation";
            this.Text = "Yarn Allocation to Knit Orders";
            this.Load += new System.EventHandler(this.frmYarnAllocation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboKnitOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboYarnOrders;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RbThirdParty;
        private System.Windows.Forms.RadioButton rbOwnYarn;
    }
}