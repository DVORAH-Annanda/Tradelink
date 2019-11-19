namespace CustomerServices
{
    partial class frmOutStandingPL
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
            this.cmboWareHouses = new CustomerServices.CheckComboBox();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cbSummarised = new System.Windows.Forms.CheckBox();
            this.cbPLStockOrders = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmboWareHouses
            // 
            this.cmboWareHouses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboWareHouses.FormattingEnabled = true;
            this.cmboWareHouses.Location = new System.Drawing.Point(277, 62);
            this.cmboWareHouses.Name = "cmboWareHouses";
            this.cmboWareHouses.Size = new System.Drawing.Size(225, 21);
            this.cmboWareHouses.TabIndex = 0;
            this.cmboWareHouses.Text = "Select Options";
            this.cmboWareHouses.SelectedIndexChanged += new System.EventHandler(this.cmboWareHouses_SelectedIndexChanged);
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(277, 150);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(225, 21);
            this.cmboStyles.TabIndex = 1;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(277, 238);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(225, 21);
            this.cmboColours.TabIndex = 2;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(277, 326);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(225, 21);
            this.cmboSizes.TabIndex = 3;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "WareHouse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Styles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Colours";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sizes";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(600, 476);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cbSummarised
            // 
            this.cbSummarised.AutoSize = true;
            this.cbSummarised.Location = new System.Drawing.Point(277, 397);
            this.cbSummarised.Name = "cbSummarised";
            this.cbSummarised.Size = new System.Drawing.Size(118, 17);
            this.cbSummarised.TabIndex = 9;
            this.cbSummarised.Text = "Summarised Format";
            this.cbSummarised.UseVisualStyleBackColor = true;
            // 
            // cbPLStockOrders
            // 
            this.cbPLStockOrders.AutoSize = true;
            this.cbPLStockOrders.Location = new System.Drawing.Point(277, 443);
            this.cbPLStockOrders.Name = "cbPLStockOrders";
            this.cbPLStockOrders.Size = new System.Drawing.Size(142, 17);
            this.cbPLStockOrders.TabIndex = 10;
            this.cbPLStockOrders.Text = "Exclude PLStock Orders\r\n";
            this.cbPLStockOrders.UseVisualStyleBackColor = true;
            // 
            // frmOutStandingPL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 540);
            this.Controls.Add(this.cbPLStockOrders);
            this.Controls.Add(this.cbSummarised);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.cmboWareHouses);
            this.Name = "frmOutStandingPL";
            this.Text = "OutStanding Picking Slips";
            this.Load += new System.EventHandler(this.frmOutStandingPL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /* private System.Windows.Forms.ComboBox cmboWareHouses;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.ComboBox cmboSizes;*/

        private CustomerServices.CheckComboBox cmboWareHouses;
        private CustomerServices.CheckComboBox cmboStyles;
        private CustomerServices.CheckComboBox cmboColours;
        private CustomerServices.CheckComboBox cmboSizes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox cbSummarised;
        private System.Windows.Forms.CheckBox cbPLStockOrders;
    }
}