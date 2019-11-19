namespace CustomerServices
{
    partial class frmTransferWarehouseToWarehouse
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
            this.cmboFromwarehouse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboTowareHouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboColour = new CustomerServices.CheckComboBox();
            this.cmboStyle = new CustomerServices.CheckComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Warehouse";
            // 
            // cmboFromwarehouse
            // 
            this.cmboFromwarehouse.FormattingEnabled = true;
            this.cmboFromwarehouse.Location = new System.Drawing.Point(262, 40);
            this.cmboFromwarehouse.Name = "cmboFromwarehouse";
            this.cmboFromwarehouse.Size = new System.Drawing.Size(230, 21);
            this.cmboFromwarehouse.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Warehouse";
            // 
            // cmboTowareHouse
            // 
            this.cmboTowareHouse.FormattingEnabled = true;
            this.cmboTowareHouse.Location = new System.Drawing.Point(257, 501);
            this.cmboTowareHouse.Name = "cmboTowareHouse";
            this.cmboTowareHouse.Size = new System.Drawing.Size(230, 21);
            this.cmboTowareHouse.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filter By Style";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Colour";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(58, 260);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(656, 221);
            this.dataGridView1.TabIndex = 8;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(639, 546);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(262, 168);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(230, 21);
            this.cmboColour.TabIndex = 7;
            this.cmboColour.Text = "Select Options";
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // cmboStyle
            // 
            this.cmboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(262, 116);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(230, 21);
            this.cmboStyle.TabIndex = 4;
            this.cmboStyle.Text = "Select Options";
            this.cmboStyle.SelectedIndexChanged += new System.EventHandler(this.cmboStyle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sizes";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(262, 220);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(230, 21);
            this.cmboSizes.TabIndex = 11;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // frmTransferWarehouseToWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 595);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.cmboTowareHouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboFromwarehouse);
            this.Controls.Add(this.label1);
            this.Name = "frmTransferWarehouseToWarehouse";
            this.Text = "Warehouse To Warehouse";
            this.Load += new System.EventHandler(this.frmTransferWarehouseToWarehouse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboFromwarehouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboTowareHouse;
        //private System.Windows.Forms.ComboBox cmboStyle;
        private CustomerServices.CheckComboBox cmboStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        // private System.Windows.Forms.ComboBox cmboColour;
        private CustomerServices.CheckComboBox cmboColour;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label5;
        //private System.Windows.Forms.ComboBox cmboSizes;
        private CustomerServices.CheckComboBox cmboSizes;
    }
}