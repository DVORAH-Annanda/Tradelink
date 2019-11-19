namespace CustomerServices
{
    partial class frmSelOutStandingOrders
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
            this.cmboSizes = new CustomerServices.CheckComboBox();
            this.cmboColours = new CustomerServices.CheckComboBox();
            this.cmboStyles = new CustomerServices.CheckComboBox();
            this.cmboCustomers = new CustomerServices.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Styles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Colours";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sizes";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(489, 373);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(224, 275);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(233, 21);
            this.cmboSizes.TabIndex = 7;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(224, 213);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(233, 21);
            this.cmboColours.TabIndex = 5;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(224, 138);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(233, 21);
            this.cmboStyles.TabIndex = 2;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(224, 64);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(233, 21);
            this.cmboCustomers.TabIndex = 0;
            this.cmboCustomers.Text = "Select Options";
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // frmSelOutStandingOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 429);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboCustomers);
            this.Name = "frmSelOutStandingOrders";
            this.Text = "Outstanding Customer Orders";
            this.Load += new System.EventHandler(this.frmSelOutStandingOrders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private /*System.Windows.Forms.ComboBox*/ CustomerServices.CheckComboBox cmboCustomers;
        private System.Windows.Forms.Label label1;
        private /*System.Windows.Forms.ComboBox*/ CustomerServices.CheckComboBox cmboStyles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private /*System.Windows.Forms.ComboBox*/ CustomerServices.CheckComboBox cmboColours;
        private System.Windows.Forms.Label label4;
        private /*System.Windows.Forms.ComboBox*/ CustomerServices.CheckComboBox cmboSizes;
        private System.Windows.Forms.Button btnSubmit;
    }
}