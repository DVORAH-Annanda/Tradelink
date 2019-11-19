namespace DyeHouse
{
    partial class frmConsumablesReceipt
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
            this.cmboReceivingDepartment = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransnumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboIssueDepartment = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Receiving Department";
            // 
            // cmboReceivingDepartment
            // 
            this.cmboReceivingDepartment.FormattingEnabled = true;
            this.cmboReceivingDepartment.Location = new System.Drawing.Point(278, 105);
            this.cmboReceivingDepartment.Name = "cmboReceivingDepartment";
            this.cmboReceivingDepartment.Size = new System.Drawing.Size(206, 21);
            this.cmboReceivingDepartment.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(630, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(143, 162);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(487, 209);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transaction Number";
            // 
            // txtTransnumber
            // 
            this.txtTransnumber.Location = new System.Drawing.Point(278, 12);
            this.txtTransnumber.Name = "txtTransnumber";
            this.txtTransnumber.ReadOnly = true;
            this.txtTransnumber.Size = new System.Drawing.Size(100, 20);
            this.txtTransnumber.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Issueing Department";
            // 
            // cmboIssueDepartment
            // 
            this.cmboIssueDepartment.FormattingEnabled = true;
            this.cmboIssueDepartment.Location = new System.Drawing.Point(278, 58);
            this.cmboIssueDepartment.Name = "cmboIssueDepartment";
            this.cmboIssueDepartment.Size = new System.Drawing.Size(206, 21);
            this.cmboIssueDepartment.TabIndex = 9;
            this.cmboIssueDepartment.SelectedIndexChanged += new System.EventHandler(this.cmboIssueDepartment_SelectedIndexChanged);
            // 
            // frmConsumablesReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 439);
            this.Controls.Add(this.cmboIssueDepartment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTransnumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmboReceivingDepartment);
            this.Controls.Add(this.label1);
            this.Name = "frmConsumablesReceipt";
            this.Text = "Consumables into Dye Kitchen";
            this.Load += new System.EventHandler(this.frmConsumablesReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboReceivingDepartment;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTransnumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboIssueDepartment;
    }
}