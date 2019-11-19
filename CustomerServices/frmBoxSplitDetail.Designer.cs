namespace CustomerServices
{
    partial class frmBoxSplitDetail
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
            this.txtOriginalBoxNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOriginalBoxQty = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAuthorised = new System.Windows.Forms.TextBox();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original Box Number";
            // 
            // txtOriginalBoxNo
            // 
            this.txtOriginalBoxNo.Location = new System.Drawing.Point(247, 89);
            this.txtOriginalBoxNo.Name = "txtOriginalBoxNo";
            this.txtOriginalBoxNo.ReadOnly = true;
            this.txtOriginalBoxNo.Size = new System.Drawing.Size(188, 20);
            this.txtOriginalBoxNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Original Box Qty";
            // 
            // txtOriginalBoxQty
            // 
            this.txtOriginalBoxQty.Location = new System.Drawing.Point(247, 127);
            this.txtOriginalBoxQty.Name = "txtOriginalBoxQty";
            this.txtOriginalBoxQty.ReadOnly = true;
            this.txtOriginalBoxQty.Size = new System.Drawing.Size(162, 20);
            this.txtOriginalBoxQty.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(97, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(401, 173);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(516, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transaction Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Authorised By";
            // 
            // txtAuthorised
            // 
            this.txtAuthorised.Location = new System.Drawing.Point(247, 51);
            this.txtAuthorised.Name = "txtAuthorised";
            this.txtAuthorised.Size = new System.Drawing.Size(282, 20);
            this.txtAuthorised.TabIndex = 8;
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Location = new System.Drawing.Point(247, 13);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(139, 20);
            this.txtTransNumber.TabIndex = 9;
            // 
            // frmBoxSplitDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 415);
            this.Controls.Add(this.txtTransNumber);
            this.Controls.Add(this.txtAuthorised);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtOriginalBoxQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOriginalBoxNo);
            this.Controls.Add(this.label1);
            this.Name = "frmBoxSplitDetail";
            this.Text = "Box Split Detail";
            this.Load += new System.EventHandler(this.frmBoxSplitDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOriginalBoxNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOriginalBoxQty;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAuthorised;
        private System.Windows.Forms.TextBox txtTransNumber;
    }
}