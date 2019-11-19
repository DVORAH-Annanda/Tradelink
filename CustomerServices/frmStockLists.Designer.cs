namespace CustomerServices
{
    partial class frmStockLists
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.comboSizes = new CustomerServices.CheckComboBox();
            this.comboColour = new CustomerServices.CheckComboBox();
            this.comboStyles = new CustomerServices.CheckComboBox();
            this.comboWareHouse = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Warehouse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Styles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Colour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sizes";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(510, 431);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // comboSizes
            // 
            this.comboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSizes.FormattingEnabled = true;
            this.comboSizes.Location = new System.Drawing.Point(161, 290);
            this.comboSizes.Name = "comboSizes";
            this.comboSizes.Size = new System.Drawing.Size(246, 21);
            this.comboSizes.TabIndex = 7;
            this.comboSizes.Text = "Select Options";
            this.comboSizes.SelectedIndexChanged += new System.EventHandler(this.comboSizes_SelectedIndexChanged);
            // 
            // comboColour
            // 
            this.comboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColour.FormattingEnabled = true;
            this.comboColour.Location = new System.Drawing.Point(161, 193);
            this.comboColour.Name = "comboColour";
            this.comboColour.Size = new System.Drawing.Size(246, 21);
            this.comboColour.TabIndex = 4;
            this.comboColour.Text = "Select Options";
            this.comboColour.SelectedIndexChanged += new System.EventHandler(this.comboColour_SelectedIndexChanged);
            // 
            // comboStyles
            // 
            this.comboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStyles.FormattingEnabled = true;
            this.comboStyles.Location = new System.Drawing.Point(161, 111);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.Size = new System.Drawing.Size(246, 21);
            this.comboStyles.TabIndex = 2;
            this.comboStyles.Text = "Select Options";
            this.comboStyles.SelectedIndexChanged += new System.EventHandler(this.comboStyles_SelectedIndexChanged);
            // 
            // comboWareHouse
            // 
            this.comboWareHouse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboWareHouse.FormattingEnabled = true;
            this.comboWareHouse.Location = new System.Drawing.Point(161, 45);
            this.comboWareHouse.Name = "comboWareHouse";
            this.comboWareHouse.Size = new System.Drawing.Size(246, 21);
            this.comboWareHouse.TabIndex = 0;
            this.comboWareHouse.Text = "Select Options";
            this.comboWareHouse.SelectedIndexChanged += new System.EventHandler(this.comboWareHouse_SelectedIndexChanged);
            // 
            // frmStockLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 511);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.comboSizes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboColour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboStyles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboWareHouse);
            this.Name = "frmStockLists";
            this.Text = "Warehouse Stock Take List Preparation";
            this.Load += new System.EventHandler(this.frmStockLists_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CustomerServices.CheckComboBox comboSizes;
        private System.Windows.Forms.Button btnSubmit;
        private CustomerServices.CheckComboBox comboWareHouse;
        private CustomerServices.CheckComboBox comboStyles;
        private CustomerServices.CheckComboBox comboColour;
    }
}