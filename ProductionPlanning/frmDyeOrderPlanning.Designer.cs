
namespace ProductionPlanning
{
    partial class frmDyeOrderPlanning
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chkcboCMTSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboSizesSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboColoursSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboStylesSelection = new ProductionPlanning.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(56, 311);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Style Selection";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Colour Selection ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 360);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sizes Selection";
            this.label4.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(380, 172);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 28);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(189, 30);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(291, 22);
            this.dtpFromDate.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Date";
            // 
            // chkcboCMTSelection
            // 
            this.chkcboCMTSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboCMTSelection.Location = new System.Drawing.Point(49, 416);
            this.chkcboCMTSelection.Name = "chkcboCMTSelection";
            this.chkcboCMTSelection.Size = new System.Drawing.Size(121, 23);
            this.chkcboCMTSelection.TabIndex = 13;
            this.chkcboCMTSelection.Text = "Select Options";
            // 
            // chkcboSizesSelection
            // 
            this.chkcboSizesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboSizesSelection.FormattingEnabled = true;
            this.chkcboSizesSelection.Location = new System.Drawing.Point(206, 433);
            this.chkcboSizesSelection.Margin = new System.Windows.Forms.Padding(4);
            this.chkcboSizesSelection.Name = "chkcboSizesSelection";
            this.chkcboSizesSelection.Size = new System.Drawing.Size(291, 23);
            this.chkcboSizesSelection.TabIndex = 3;
            this.chkcboSizesSelection.Text = "Select Sizes";
            this.chkcboSizesSelection.Visible = false;
            // 
            // chkcboColoursSelection
            // 
            this.chkcboColoursSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboColoursSelection.FormattingEnabled = true;
            this.chkcboColoursSelection.Location = new System.Drawing.Point(189, 126);
            this.chkcboColoursSelection.Margin = new System.Windows.Forms.Padding(4);
            this.chkcboColoursSelection.Name = "chkcboColoursSelection";
            this.chkcboColoursSelection.Size = new System.Drawing.Size(291, 23);
            this.chkcboColoursSelection.TabIndex = 2;
            this.chkcboColoursSelection.Text = "Select Colours";
            // 
            // chkcboStylesSelection
            // 
            this.chkcboStylesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboStylesSelection.FormattingEnabled = true;
            this.chkcboStylesSelection.Location = new System.Drawing.Point(189, 78);
            this.chkcboStylesSelection.Margin = new System.Windows.Forms.Padding(4);
            this.chkcboStylesSelection.Name = "chkcboStylesSelection";
            this.chkcboStylesSelection.Size = new System.Drawing.Size(291, 23);
            this.chkcboStylesSelection.TabIndex = 1;
            this.chkcboStylesSelection.Text = "Select Styles";
            // 
            // FrmDyeOrderPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 230);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.chkcboCMTSelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.chkcboSizesSelection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkcboColoursSelection);
            this.Controls.Add(this.chkcboStylesSelection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDyeOrderPlanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dye Order Planning";
            this.Load += new System.EventHandler(this.frmDyeOrderPlanning_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ProductionPlanning.CheckComboBox chkcboCMTSelection;
        private ProductionPlanning.CheckComboBox chkcboStylesSelection;
        private ProductionPlanning.CheckComboBox chkcboColoursSelection;
        private ProductionPlanning.CheckComboBox chkcboSizesSelection;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label5;
    }
}