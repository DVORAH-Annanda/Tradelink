namespace DyeHouse
{
    partial class frmViewDyeOrder
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCustomers = new DyeHouse.CheckComboBox();
            this.comboColour = new DyeHouse.CheckComboBox();
            this.comboQuality = new DyeHouse.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboStyles = new DyeHouse.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(244, 26);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(141, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(244, 70);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(141, 20);
            this.dtpToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // comboCustomers
            // 
            this.comboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCustomers.FormattingEnabled = true;
            this.comboCustomers.Location = new System.Drawing.Point(244, 114);
            this.comboCustomers.Name = "comboCustomers";
            this.comboCustomers.Size = new System.Drawing.Size(191, 21);
            this.comboCustomers.TabIndex = 4;
            this.comboCustomers.Text = "Select Options";
            // 
            // comboColour
            // 
            this.comboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColour.FormattingEnabled = true;
            this.comboColour.Location = new System.Drawing.Point(244, 204);
            this.comboColour.Name = "comboColour";
            this.comboColour.Size = new System.Drawing.Size(191, 21);
            this.comboColour.TabIndex = 8;
            this.comboColour.Text = "Select Options";
            // 
            // comboQuality
            // 
            this.comboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboQuality.FormattingEnabled = true;
            this.comboQuality.Location = new System.Drawing.Point(244, 159);
            this.comboQuality.Name = "comboQuality";
            this.comboQuality.Size = new System.Drawing.Size(191, 21);
            this.comboQuality.TabIndex = 5;
            this.comboQuality.Text = "Select Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Customers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Colour";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(46, 316);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(578, 206);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(575, 552);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Styles";
            // 
            // comboStyles
            // 
            this.comboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStyles.FormattingEnabled = true;
            this.comboStyles.Location = new System.Drawing.Point(244, 252);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.Size = new System.Drawing.Size(191, 21);
            this.comboStyles.TabIndex = 13;
            this.comboStyles.Text = "Select Options";
            // 
            // frmViewDyeOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 603);
            this.Controls.Add(this.comboStyles);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboColour);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboQuality);
            this.Controls.Add(this.comboCustomers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFromDate);
            this.Name = "frmViewDyeOrder";
            this.Text = "View Active Dye Orders";
            this.Load += new System.EventHandler(this.frmViewDyeOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        /*private System.Windows.Forms.ComboBox comboCustomers;
        private System.Windows.Forms.ComboBox comboQuality;
        private System.Windows.Forms.ComboBox comboStyles;
        private System.Windows.Forms.ComboBox comboColour; */

        private DyeHouse.CheckComboBox comboCustomers;
        private DyeHouse.CheckComboBox comboQuality;
        private DyeHouse.CheckComboBox comboColour;
        private DyeHouse.CheckComboBox comboStyles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
       
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label6;
        
    }
}