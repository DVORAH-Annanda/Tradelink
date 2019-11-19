namespace CustomerServices
{
    partial class frmWareHouseTransfers
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
            this.cmboFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboTo = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.DGVResults = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From CMT";
            // 
            // cmboFrom
            // 
            this.cmboFrom.FormattingEnabled = true;
            this.cmboFrom.Location = new System.Drawing.Point(247, 22);
            this.cmboFrom.Name = "cmboFrom";
            this.cmboFrom.Size = new System.Drawing.Size(292, 21);
            this.cmboFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Warehouse";
            // 
            // cmboTo
            // 
            this.cmboTo.FormattingEnabled = true;
            this.cmboTo.Location = new System.Drawing.Point(247, 66);
            this.cmboTo.Name = "cmboTo";
            this.cmboTo.Size = new System.Drawing.Size(292, 21);
            this.cmboTo.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(247, 114);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(156, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transaction Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboSizes);
            this.groupBox1.Controls.Add(this.cmboColours);
            this.groupBox1.Controls.Add(this.cmboStyles);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.DGVResults);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(105, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 588);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Criteria";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(142, 159);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(356, 21);
            this.cmboSizes.TabIndex = 10;
            this.cmboSizes.Text = "Select Options";
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(142, 95);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(356, 21);
            this.cmboColours.TabIndex = 9;
            this.cmboColours.Text = "Select Options";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(142, 31);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(356, 21);
            this.cmboStyles.TabIndex = 8;
            this.cmboStyles.Text = "Select Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(485, 529);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // DGVResults
            // 
            this.DGVResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVResults.Location = new System.Drawing.Point(32, 204);
            this.DGVResults.Name = "DGVResults";
            this.DGVResults.Size = new System.Drawing.Size(528, 298);
            this.DGVResults.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Colours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Sizes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Styles";
            // 
            // frmWareHouseTransfers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 733);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmboTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboFrom);
            this.Controls.Add(this.label1);
            this.Name = "frmWareHouseTransfers";
            this.Text = "WareHouse Transfers";
            this.Load += new System.EventHandler(this.frmWareHouseTransfers_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboTo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView DGVResults;
       // private System.Windows.Forms.ComboBox cmboSizes;
        private CustomerServices.CheckComboBox cmboSizes;
       // private System.Windows.Forms.ComboBox cmboColours;
        private CustomerServices.CheckComboBox cmboColours;
       // private System.Windows.Forms.ComboBox cmboStyles;
        private CustomerServices.CheckComboBox cmboStyles;
    }
}