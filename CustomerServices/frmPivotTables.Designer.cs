namespace CustomerServices
{
    partial class frmPivotTables
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DGVOutput = new System.Windows.Forms.DataGridView();
            this.rbOutStanding = new System.Windows.Forms.RadioButton();
            this.rbAvailable = new System.Windows.Forms.RadioButton();
            this.rbGrossStock = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.cmboWareHouses = new CustomerServices.CheckComboBox();
            this.cmboCustomers = new CustomerServices.CheckComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboSizes);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmboColours);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmboStyles);
            this.groupBox1.Controls.Add(this.DGVOutput);
            this.groupBox1.Controls.Add(this.cmboWareHouses);
            this.groupBox1.Controls.Add(this.rbOutStanding);
            this.groupBox1.Controls.Add(this.rbAvailable);
            this.groupBox1.Controls.Add(this.rbGrossStock);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmboCustomers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(89, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 656);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Sizes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Colours";
            // 
            // DGVOutput
            // 
            this.DGVOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVOutput.Location = new System.Drawing.Point(21, 337);
            this.DGVOutput.Name = "DGVOutput";
            this.DGVOutput.Size = new System.Drawing.Size(536, 282);
            this.DGVOutput.TabIndex = 8;
            this.DGVOutput.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGVOutput_CellMouseClick);
            // 
            // rbOutStanding
            // 
            this.rbOutStanding.AutoSize = true;
            this.rbOutStanding.Location = new System.Drawing.Point(347, 19);
            this.rbOutStanding.Name = "rbOutStanding";
            this.rbOutStanding.Size = new System.Drawing.Size(116, 17);
            this.rbOutStanding.TabIndex = 9;
            this.rbOutStanding.TabStop = true;
            this.rbOutStanding.Text = "Outstanding Orders";
            this.rbOutStanding.UseVisualStyleBackColor = true;
            this.rbOutStanding.CheckedChanged += new System.EventHandler(this.rbOutStanding_CheckedChanged);
            // 
            // rbAvailable
            // 
            this.rbAvailable.AutoSize = true;
            this.rbAvailable.Location = new System.Drawing.Point(202, 19);
            this.rbAvailable.Name = "rbAvailable";
            this.rbAvailable.Size = new System.Drawing.Size(99, 17);
            this.rbAvailable.TabIndex = 7;
            this.rbAvailable.Text = "Available Stock";
            this.rbAvailable.UseVisualStyleBackColor = true;
            this.rbAvailable.CheckedChanged += new System.EventHandler(this.rbAvailable_CheckedChanged);
            // 
            // rbGrossStock
            // 
            this.rbGrossStock.AutoSize = true;
            this.rbGrossStock.Checked = true;
            this.rbGrossStock.Location = new System.Drawing.Point(66, 19);
            this.rbGrossStock.Name = "rbGrossStock";
            this.rbGrossStock.Size = new System.Drawing.Size(99, 17);
            this.rbGrossStock.TabIndex = 6;
            this.rbGrossStock.TabStop = true;
            this.rbGrossStock.Text = "Stock On Hand";
            this.rbGrossStock.UseVisualStyleBackColor = true;
            this.rbGrossStock.CheckedChanged += new System.EventHandler(this.rbGrossStock_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Styles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Customer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WareHouses";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(627, 742);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(203, 294);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(271, 21);
            this.cmboSizes.TabIndex = 15;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(202, 235);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(272, 21);
            this.cmboColours.TabIndex = 13;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(203, 176);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(271, 21);
            this.cmboStyles.TabIndex = 11;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // cmboWareHouses
            // 
            this.cmboWareHouses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboWareHouses.FormattingEnabled = true;
            this.cmboWareHouses.Location = new System.Drawing.Point(203, 58);
            this.cmboWareHouses.Name = "cmboWareHouses";
            this.cmboWareHouses.Size = new System.Drawing.Size(271, 21);
            this.cmboWareHouses.TabIndex = 10;
            this.cmboWareHouses.Text = "Select Options";
            this.cmboWareHouses.SelectedIndexChanged += new System.EventHandler(this.cmboWareHouses_SelectedIndexChanged);
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(203, 117);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(271, 21);
            this.cmboCustomers.TabIndex = 3;
            this.cmboCustomers.Text = "Select Options";
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // frmPivotTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 777);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPivotTables";
            this.Text = "Pivot Summary Tables";
            this.Load += new System.EventHandler(this.frmPivotTables_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
       // private System.Windows.Forms.ComboBox cmboCustomers;
        private CustomerServices.CheckComboBox cmboCustomers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbAvailable;
        private System.Windows.Forms.RadioButton rbGrossStock;
        private System.Windows.Forms.DataGridView DGVOutput;
        private System.Windows.Forms.RadioButton rbOutStanding;
       // private System.Windows.Forms.ComboBox cmboStyles;
        private CustomerServices.CheckComboBox cmboStyles;
       // private System.Windows.Forms.ComboBox cmboWareHouses;
        private CustomerServices.CheckComboBox cmboWareHouses;
       // private System.Windows.Forms.ComboBox cmboSizes;
        private CustomerServices.CheckComboBox cmboSizes;
        private System.Windows.Forms.Label label5;
        // private System.Windows.Forms.ComboBox cmboColours;
        private CustomerServices.CheckComboBox cmboColours;
        private System.Windows.Forms.Label label4;

    }
}