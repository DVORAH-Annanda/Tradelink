namespace CustomerServices
{
    partial class frmDetailCustomerOrders
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboCustomers = new CustomerServices.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboSizes = new CustomerServices.CheckComboBox();
            this.comboColours = new CustomerServices.CheckComboBox();
            this.comboStyles = new CustomerServices.CheckComboBox();
            this.comboWhses = new CustomerServices.CheckComboBox();
            this.chkGroupByWeek = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbStdOrdersOnly = new System.Windows.Forms.RadioButton();
            this.rbProvisionalOrdersOnly = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(249, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(144, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(533, 508);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Customers";
            // 
            // comboCustomers
            // 
            this.comboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCustomers.FormattingEnabled = true;
            this.comboCustomers.Location = new System.Drawing.Point(249, 82);
            this.comboCustomers.Name = "comboCustomers";
            this.comboCustomers.Size = new System.Drawing.Size(210, 21);
            this.comboCustomers.TabIndex = 8;
            this.comboCustomers.Text = "Select Options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboSizes);
            this.groupBox1.Controls.Add(this.comboColours);
            this.groupBox1.Controls.Add(this.comboStyles);
            this.groupBox1.Controls.Add(this.comboWhses);
            this.groupBox1.Location = new System.Drawing.Point(128, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 219);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter By";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Sizes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Colours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Styles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, -37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Warehouses";
            // 
            // comboSizes
            // 
            this.comboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSizes.FormattingEnabled = true;
            this.comboSizes.Location = new System.Drawing.Point(132, 167);
            this.comboSizes.Name = "comboSizes";
            this.comboSizes.Size = new System.Drawing.Size(233, 21);
            this.comboSizes.TabIndex = 15;
            this.comboSizes.Text = "Select Options";
            this.comboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSelectedIndex_Changed);
            // 
            // comboColours
            // 
            this.comboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColours.FormattingEnabled = true;
            this.comboColours.Location = new System.Drawing.Point(132, 99);
            this.comboColours.Name = "comboColours";
            this.comboColours.Size = new System.Drawing.Size(233, 21);
            this.comboColours.TabIndex = 14;
            this.comboColours.Text = "Select Options";
            this.comboColours.SelectedIndexChanged += new System.EventHandler(this.cmboSelectedIndex_Changed);
            // 
            // comboStyles
            // 
            this.comboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStyles.FormattingEnabled = true;
            this.comboStyles.Location = new System.Drawing.Point(132, 31);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.Size = new System.Drawing.Size(233, 21);
            this.comboStyles.TabIndex = 13;
            this.comboStyles.Text = "Select Options";
            this.comboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboSelectedIndex_Changed);
            // 
            // comboWhses
            // 
            this.comboWhses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboWhses.FormattingEnabled = true;
            this.comboWhses.Location = new System.Drawing.Point(132, -37);
            this.comboWhses.Name = "comboWhses";
            this.comboWhses.Size = new System.Drawing.Size(233, 21);
            this.comboWhses.TabIndex = 12;
            this.comboWhses.Text = "Select Options";
            // 
            // chkGroupByWeek
            // 
            this.chkGroupByWeek.AutoSize = true;
            this.chkGroupByWeek.Location = new System.Drawing.Point(240, 371);
            this.chkGroupByWeek.Name = "chkGroupByWeek";
            this.chkGroupByWeek.Size = new System.Drawing.Size(102, 17);
            this.chkGroupByWeek.TabIndex = 11;
            this.chkGroupByWeek.Text = "Group By Week";
            this.chkGroupByWeek.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbStdOrdersOnly);
            this.groupBox2.Controls.Add(this.rbProvisionalOrdersOnly);
            this.groupBox2.Location = new System.Drawing.Point(193, 403);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 88);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Class";
            // 
            // rbStdOrdersOnly
            // 
            this.rbStdOrdersOnly.AutoSize = true;
            this.rbStdOrdersOnly.Location = new System.Drawing.Point(27, 19);
            this.rbStdOrdersOnly.Name = "rbStdOrdersOnly";
            this.rbStdOrdersOnly.Size = new System.Drawing.Size(126, 17);
            this.rbStdOrdersOnly.TabIndex = 1;
            this.rbStdOrdersOnly.TabStop = true;
            this.rbStdOrdersOnly.Text = "Standard Orders Only";
            this.rbStdOrdersOnly.UseVisualStyleBackColor = true;
            // 
            // rbProvisionalOrdersOnly
            // 
            this.rbProvisionalOrdersOnly.AutoSize = true;
            this.rbProvisionalOrdersOnly.Location = new System.Drawing.Point(27, 52);
            this.rbProvisionalOrdersOnly.Name = "rbProvisionalOrdersOnly";
            this.rbProvisionalOrdersOnly.Size = new System.Drawing.Size(129, 17);
            this.rbProvisionalOrdersOnly.TabIndex = 0;
            this.rbProvisionalOrdersOnly.TabStop = true;
            this.rbProvisionalOrdersOnly.Text = "Provisional Order Only\r\n";
            this.rbProvisionalOrdersOnly.UseVisualStyleBackColor = true;
            // 
            // frmDetailCustomerOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 543);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkGroupByWeek);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboCustomers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "frmDetailCustomerOrders";
            this.Text = "Outstanding Customer Orders";
            this.Load += new System.EventHandler(this.frmDetailCustomerOrders_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        // private System.Windows.Forms.ComboBox comboCustomers;
        private CustomerServices.CheckComboBox comboCustomers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private CheckComboBox comboSizes;
        private CheckComboBox comboColours;
        private CheckComboBox comboStyles;
        private CheckComboBox comboWhses;
        private System.Windows.Forms.CheckBox chkGroupByWeek;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbStdOrdersOnly;
        private System.Windows.Forms.RadioButton rbProvisionalOrdersOnly;
        // private System.Windows.Forms.ComboBox comboStyles;
    }
}