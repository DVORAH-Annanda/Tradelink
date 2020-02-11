namespace Administration
{
    partial class frmSelectProductRating
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.rbActiveRatingsOnly = new System.Windows.Forms.RadioButton();
            this.rbClosedRatingsOnly = new System.Windows.Forms.RadioButton();
            this.rbAllProductRatings = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAllProductRatings);
            this.groupBox1.Controls.Add(this.rbClosedRatingsOnly);
            this.groupBox1.Controls.Add(this.rbActiveRatingsOnly);
            this.groupBox1.Location = new System.Drawing.Point(220, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(568, 388);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // rbActiveRatingsOnly
            // 
            this.rbActiveRatingsOnly.AutoSize = true;
            this.rbActiveRatingsOnly.Checked = true;
            this.rbActiveRatingsOnly.Location = new System.Drawing.Point(48, 34);
            this.rbActiveRatingsOnly.Name = "rbActiveRatingsOnly";
            this.rbActiveRatingsOnly.Size = new System.Drawing.Size(153, 17);
            this.rbActiveRatingsOnly.TabIndex = 0;
            this.rbActiveRatingsOnly.TabStop = true;
            this.rbActiveRatingsOnly.Text = "Active Product Rating Only";
            this.rbActiveRatingsOnly.UseVisualStyleBackColor = true;
            // 
            // rbClosedRatingsOnly
            // 
            this.rbClosedRatingsOnly.AutoSize = true;
            this.rbClosedRatingsOnly.Location = new System.Drawing.Point(48, 75);
            this.rbClosedRatingsOnly.Name = "rbClosedRatingsOnly";
            this.rbClosedRatingsOnly.Size = new System.Drawing.Size(136, 17);
            this.rbClosedRatingsOnly.TabIndex = 1;
            this.rbClosedRatingsOnly.TabStop = true;
            this.rbClosedRatingsOnly.Text = "Non Active Rating Only";
            this.rbClosedRatingsOnly.UseVisualStyleBackColor = true;
            // 
            // rbAllProductRatings
            // 
            this.rbAllProductRatings.AutoSize = true;
            this.rbAllProductRatings.Location = new System.Drawing.Point(48, 116);
            this.rbAllProductRatings.Name = "rbAllProductRatings";
            this.rbAllProductRatings.Size = new System.Drawing.Size(47, 17);
            this.rbAllProductRatings.TabIndex = 2;
            this.rbAllProductRatings.TabStop = true;
            this.rbAllProductRatings.Text = "Both";
            this.rbAllProductRatings.UseVisualStyleBackColor = true;
            // 
            // frmSelectProductRating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 450);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSelectProductRating";
            this.Text = "Select as appropriate";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAllProductRatings;
        private System.Windows.Forms.RadioButton rbClosedRatingsOnly;
        private System.Windows.Forms.RadioButton rbActiveRatingsOnly;
        private System.Windows.Forms.Button btnSubmit;
    }
}