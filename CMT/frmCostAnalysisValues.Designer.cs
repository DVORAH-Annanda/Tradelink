namespace CMT
{
    partial class frmCostAnalysisValues
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
            this.cmboStyles = new CMT.CheckComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboColours = new CMT.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboSizes = new CMT.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboCMTs = new CMT.CheckComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Styles";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(193, 98);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(253, 21);
            this.cmboStyles.TabIndex = 1;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Colours";
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(193, 169);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(253, 21);
            this.cmboColours.TabIndex = 3;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sizes";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(193, 240);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(253, 21);
            this.cmboSizes.TabIndex = 5;
            this.cmboSizes.Text = "Select Options";
            this.cmboSizes.SelectedIndexChanged += new System.EventHandler(this.cmboSizes_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(544, 379);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboCMTs
            // 
            this.cmboCMTs.FormattingEnabled = true;
            this.cmboCMTs.Location = new System.Drawing.Point(193, 34);
            this.cmboCMTs.Name = "cmboCMTs";
            this.cmboCMTs.Size = new System.Drawing.Size(253, 21);
            this.cmboCMTs.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "CMT";
            // 
            // frmCostAnalysisValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 441);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboCMTs);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label1);
            this.Name = "frmCostAnalysisValues";
            this.Text = "Cost analysis values";
            this.Load += new System.EventHandler(this.frmCostAnalysisValues_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ComboBox cmboStyles;
        private CMT.CheckComboBox cmboStyles;
        private System.Windows.Forms.Label label2;
        //private System.Windows.Forms.ComboBox cmboColours;
        private CMT.CheckComboBox cmboColours;
        private System.Windows.Forms.Label label3;
        //private System.Windows.Forms.ComboBox cmboSizes;
        private CMT.CheckComboBox cmboSizes;
        private System.Windows.Forms.Button btnSubmit;
        CMT.CheckComboBox cmboCMTs;
        // private System.Windows.Forms.ComboBox cmboCMTs;
        private System.Windows.Forms.Label label4;
    }
}