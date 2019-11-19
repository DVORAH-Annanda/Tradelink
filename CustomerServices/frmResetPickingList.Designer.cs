namespace CustomerServices
{
    partial class frmResetPickingList
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
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboWarehouses = new CustomerServices.CheckComboBox();
            this.cmboCustomer = new CustomerServices.CheckComboBox();
            this.cmboOrderAssigned = new CustomerServices.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Picking Lists";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(573, 428);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.cmboWarehouses);
            this.groupBox1.Controls.Add(this.cmboCustomer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(123, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 141);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Criteria";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(294, 100);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "By Warehouse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "By Customer";
            // 
            // cmboWarehouses
            // 
            this.cmboWarehouses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboWarehouses.FormattingEnabled = true;
            this.cmboWarehouses.Location = new System.Drawing.Point(155, 67);
            this.cmboWarehouses.Name = "cmboWarehouses";
            this.cmboWarehouses.Size = new System.Drawing.Size(214, 21);
            this.cmboWarehouses.TabIndex = 3;
            this.cmboWarehouses.Text = "Select Options";
            this.cmboWarehouses.SelectedIndexChanged += new System.EventHandler(this.cmboWarehouses_SelectedIndexChanged);
            // 
            // cmboCustomer
            // 
            this.cmboCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomer.FormattingEnabled = true;
            this.cmboCustomer.Location = new System.Drawing.Point(155, 26);
            this.cmboCustomer.Name = "cmboCustomer";
            this.cmboCustomer.Size = new System.Drawing.Size(214, 21);
            this.cmboCustomer.TabIndex = 2;
            this.cmboCustomer.Text = "Select Options";
            this.cmboCustomer.SelectedIndexChanged += new System.EventHandler(this.cmboCustomer_SelectedIndexChanged);
            // 
            // cmboOrderAssigned
            // 
            this.cmboOrderAssigned.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboOrderAssigned.FormattingEnabled = true;
            this.cmboOrderAssigned.Location = new System.Drawing.Point(253, 269);
            this.cmboOrderAssigned.Name = "cmboOrderAssigned";
            this.cmboOrderAssigned.Size = new System.Drawing.Size(255, 21);
            this.cmboOrderAssigned.TabIndex = 1;
            this.cmboOrderAssigned.Text = "Select Options";
            this.cmboOrderAssigned.SelectedIndexChanged += new System.EventHandler(this.cmboOrderAssigned_SelectedIndexChanged);
            // 
            // frmResetPickingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 483);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cmboOrderAssigned);
            this.Controls.Add(this.label1);
            this.Name = "frmResetPickingList";
            this.Text = "Reset Picking Lists";
            this.Load += new System.EventHandler(this.frmResetPickingList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomerServices.CheckComboBox cmboOrderAssigned;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
        private CustomerServices.CheckComboBox cmboWarehouses;
        private CustomerServices.CheckComboBox cmboCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}