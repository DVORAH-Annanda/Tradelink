
namespace ProductionPlanning
{
    partial class frmWIPQuickLook
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
            this.chkcboCMTSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboStylesSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboColoursSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboSizesSelection = new ProductionPlanning.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pbarExpedite = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "CMT Selection ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Style Selection";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Colour Selection ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sizes Selection";
            // 
            // chkcboCMTSelection
            // 
            this.chkcboCMTSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboCMTSelection.FormattingEnabled = true;
            this.chkcboCMTSelection.Location = new System.Drawing.Point(143, 36);
            this.chkcboCMTSelection.Name = "chkcboCMTSelection";
            this.chkcboCMTSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboCMTSelection.TabIndex = 0;
            this.chkcboCMTSelection.Text = "Select CMT\'s";
            // 
            // chkcboStylesSelection
            // 
            this.chkcboStylesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboStylesSelection.FormattingEnabled = true;
            this.chkcboStylesSelection.Location = new System.Drawing.Point(143, 63);
            this.chkcboStylesSelection.Name = "chkcboStylesSelection";
            this.chkcboStylesSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboStylesSelection.TabIndex = 1;
            this.chkcboStylesSelection.Text = "Select Styles";
            // 
            // chkcboColoursSelection
            // 
            this.chkcboColoursSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboColoursSelection.FormattingEnabled = true;
            this.chkcboColoursSelection.Location = new System.Drawing.Point(143, 90);
            this.chkcboColoursSelection.Name = "chkcboColoursSelection";
            this.chkcboColoursSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboColoursSelection.TabIndex = 2;
            this.chkcboColoursSelection.Text = "Select Colours";
            // 
            // chkcboSizesSelection
            // 
            this.chkcboSizesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboSizesSelection.FormattingEnabled = true;
            this.chkcboSizesSelection.Location = new System.Drawing.Point(143, 117);
            this.chkcboSizesSelection.Name = "chkcboSizesSelection";
            this.chkcboSizesSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboSizesSelection.TabIndex = 3;
            this.chkcboSizesSelection.Text = "Select Sizes";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(287, 163);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pbarExpedite
            // 
            this.pbarExpedite.Location = new System.Drawing.Point(39, 163);
            this.pbarExpedite.Name = "pbarExpedite";
            this.pbarExpedite.Size = new System.Drawing.Size(242, 23);
            this.pbarExpedite.TabIndex = 12;
            // 
            // frmWIPQuickLook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 216);
            this.Controls.Add(this.pbarExpedite);
            this.Controls.Add(this.chkcboCMTSelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.chkcboSizesSelection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkcboColoursSelection);
            this.Controls.Add(this.chkcboStylesSelection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmWIPQuickLook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WIP Quick Look for Expediting";
            this.Load += new System.EventHandler(this.frmWIPQuickLook_Load);
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
        private System.Windows.Forms.ProgressBar pbarExpedite;
    }
}