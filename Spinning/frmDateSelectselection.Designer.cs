namespace Spinning
{
    partial class frmDateSelectselection
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LotsByTruck = new System.Windows.Forms.RadioButton();
            this.ContactsBaleStore = new System.Windows.Forms.RadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.ContractStartDate = new System.Windows.Forms.RadioButton();
            this.cmbBoxSupplier = new System.Windows.Forms.ComboBox();
            this.AllSuppliers = new System.Windows.Forms.RadioButton();
            this.PerSupplier = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(309, 42);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(565, 274);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select a date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LotsByTruck);
            this.groupBox1.Controls.Add(this.ContactsBaleStore);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.ContractStartDate);
            this.groupBox1.Controls.Add(this.cmbBoxSupplier);
            this.groupBox1.Controls.Add(this.AllSuppliers);
            this.groupBox1.Controls.Add(this.PerSupplier);
            this.groupBox1.Location = new System.Drawing.Point(111, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 167);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Options";
            // 
            // LotsByTruck
            // 
            this.LotsByTruck.AutoSize = true;
            this.LotsByTruck.Location = new System.Drawing.Point(19, 130);
            this.LotsByTruck.Name = "LotsByTruck";
            this.LotsByTruck.Size = new System.Drawing.Size(249, 17);
            this.LotsByTruck.TabIndex = 6;
            this.LotsByTruck.TabStop = true;
            this.LotsByTruck.Text = "Give details of lots (trucks) received by contract";
            this.LotsByTruck.UseVisualStyleBackColor = true;
            // 
            // ContactsBaleStore
            // 
            this.ContactsBaleStore.AutoSize = true;
            this.ContactsBaleStore.Location = new System.Drawing.Point(19, 107);
            this.ContactsBaleStore.Name = "ContactsBaleStore";
            this.ContactsBaleStore.Size = new System.Drawing.Size(286, 17);
            this.ContactsBaleStore.TabIndex = 5;
            this.ContactsBaleStore.TabStop = true;
            this.ContactsBaleStore.Text = "For all contracts for which stock is held in the bale store";
            this.ContactsBaleStore.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(217, 82);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // ContractStartDate
            // 
            this.ContractStartDate.AutoSize = true;
            this.ContractStartDate.Location = new System.Drawing.Point(19, 83);
            this.ContractStartDate.Name = "ContractStartDate";
            this.ContractStartDate.Size = new System.Drawing.Size(192, 17);
            this.ContractStartDate.TabIndex = 3;
            this.ContractStartDate.TabStop = true;
            this.ContractStartDate.Text = "Contracts Starting on a certain date";
            this.ContractStartDate.UseVisualStyleBackColor = true;
            this.ContractStartDate.CheckedChanged += new System.EventHandler(this.ContractStartDate_CheckedChanged);
            // 
            // cmbBoxSupplier
            // 
            this.cmbBoxSupplier.FormattingEnabled = true;
            this.cmbBoxSupplier.Location = new System.Drawing.Point(139, 32);
            this.cmbBoxSupplier.Name = "cmbBoxSupplier";
            this.cmbBoxSupplier.Size = new System.Drawing.Size(278, 21);
            this.cmbBoxSupplier.TabIndex = 2;
            // 
            // AllSuppliers
            // 
            this.AllSuppliers.AutoSize = true;
            this.AllSuppliers.Location = new System.Drawing.Point(19, 59);
            this.AllSuppliers.Name = "AllSuppliers";
            this.AllSuppliers.Size = new System.Drawing.Size(82, 17);
            this.AllSuppliers.TabIndex = 1;
            this.AllSuppliers.TabStop = true;
            this.AllSuppliers.Text = "All Suppliers";
            this.AllSuppliers.UseVisualStyleBackColor = true;
            // 
            // PerSupplier
            // 
            this.PerSupplier.AutoSize = true;
            this.PerSupplier.Location = new System.Drawing.Point(19, 35);
            this.PerSupplier.Name = "PerSupplier";
            this.PerSupplier.Size = new System.Drawing.Size(82, 17);
            this.PerSupplier.TabIndex = 0;
            this.PerSupplier.TabStop = true;
            this.PerSupplier.Text = "Per Supplier";
            this.PerSupplier.UseVisualStyleBackColor = true;
            this.PerSupplier.CheckedChanged += new System.EventHandler(this.PerSupplier_CheckedChanged);
            // 
            // frmDateSelectselection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 317);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "frmDateSelectselection";
            this.Text = "Row Cotton Movement Date Selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton AllSuppliers;
        private System.Windows.Forms.RadioButton PerSupplier;
        private System.Windows.Forms.RadioButton LotsByTruck;
        private System.Windows.Forms.RadioButton ContactsBaleStore;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.RadioButton ContractStartDate;
        private System.Windows.Forms.ComboBox cmbBoxSupplier;
    }
}