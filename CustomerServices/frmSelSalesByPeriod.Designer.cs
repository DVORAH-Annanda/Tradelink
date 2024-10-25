﻿namespace CustomerServices
{
    partial class frmSelSalesByPeriod
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboCustomers = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(219, 94);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(130, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(219, 136);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(130, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Styles";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(219, 270);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(203, 21);
            this.cmboStyles.TabIndex = 5;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(466, 432);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Customers";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(219, 210);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(203, 21);
            this.cmboCustomers.TabIndex = 8;
            this.cmboCustomers.Text = "Select Options";
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(219, 330);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(203, 21);
            this.cmboColours.TabIndex = 9;
            this.cmboColours.Text = "Select Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 339);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Current Colours";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 399);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Current Sizes";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(219, 390);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(203, 21);
            this.cmboSizes.TabIndex = 12;
            this.cmboSizes.Text = "Select Options";
            // 
            // frmSelSalesByPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 480);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "frmSelSalesByPeriod";
            this.Text = "Sales by Style By Period Selection Screen";
            this.Load += new System.EventHandler(this.frmSelSalesByPeriod_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private /*System.Windows.Forms.ComboBox*/ CustomerServices.CheckComboBox cmboStyles;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label4;
        private CustomerServices.CheckComboBox cmboCustomers;
        private CheckComboBox cmboColours;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CheckComboBox cmboSizes;
    }
}