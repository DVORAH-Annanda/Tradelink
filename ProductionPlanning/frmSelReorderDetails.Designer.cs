namespace ProductionPlanning
{
    partial class frmSelReorderDetails
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboCustomers = new ProductionPlanning.CheckComboBox();
            this.cmboStyles = new ProductionPlanning.CheckComboBox();
            this.cmboColours = new ProductionPlanning.CheckComboBox();
            this.cmboSizes = new ProductionPlanning.CheckComboBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(548, 436);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Customers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Styles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Colours";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sizes";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(245, 44);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(210, 21);
            this.cmboCustomers.TabIndex = 9;
            this.cmboCustomers.Text = "Select Options";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(245, 123);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(210, 21);
            this.cmboStyles.TabIndex = 10;
            this.cmboStyles.Text = "Select Options";
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(245, 202);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(210, 21);
            this.cmboColours.TabIndex = 11;
            this.cmboColours.Text = "Select Options";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(245, 281);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(210, 21);
            this.cmboSizes.TabIndex = 12;
            this.cmboSizes.Text = "Select Options";
            // 
            // frmSelReorderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 488);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmSelReorderDetails";
            this.Text = "Re Order Selection Details";
            this.Load += new System.EventHandler(this.frmSelReorderDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
      /*  private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.ComboBox cmboSizes; */

        private ProductionPlanning.CheckComboBox cmboCustomers;
        private ProductionPlanning.CheckComboBox cmboStyles;
        private ProductionPlanning.CheckComboBox cmboColours;
        private ProductionPlanning.CheckComboBox cmboSizes; 

    }
}