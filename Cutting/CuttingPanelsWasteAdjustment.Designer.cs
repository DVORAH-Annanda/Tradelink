
namespace Cutting
{
    partial class CuttingPanelsWasteAdjustment
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
            this.txtCutSheetNumber = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxCurrentPanels = new System.Windows.Forms.TextBox();
            this.txtCurrentCuttingWaste = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheet Number";
            // 
            // txtCutSheetNumber
            // 
            this.txtCutSheetNumber.Location = new System.Drawing.Point(275, 39);
            this.txtCutSheetNumber.Name = "txtCutSheetNumber";
            this.txtCutSheetNumber.Size = new System.Drawing.Size(228, 20);
            this.txtCutSheetNumber.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(519, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(657, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCurrentCuttingWaste);
            this.groupBox1.Controls.Add(this.txtBoxCurrentPanels);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(275, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 186);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Values";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Current Panels";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Current Cut Waste";
            // 
            // txtBoxCurrentPanels
            // 
            this.txtBoxCurrentPanels.Location = new System.Drawing.Point(165, 56);
            this.txtBoxCurrentPanels.Name = "txtBoxCurrentPanels";
            this.txtBoxCurrentPanels.Size = new System.Drawing.Size(131, 20);
            this.txtBoxCurrentPanels.TabIndex = 2;
            // 
            // txtCurrentCuttingWaste
            // 
            this.txtCurrentCuttingWaste.Location = new System.Drawing.Point(165, 129);
            this.txtCurrentCuttingWaste.Name = "txtCurrentCuttingWaste";
            this.txtCurrentCuttingWaste.Size = new System.Drawing.Size(131, 20);
            this.txtCurrentCuttingWaste.TabIndex = 3;
            // 
            // CuttingPanelsWasteAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCutSheetNumber);
            this.Controls.Add(this.label1);
            this.Name = "CuttingPanelsWasteAdjustment";
            this.Text = "Cutting Panels Waste Adjustment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CuttingPanelsWasteAdjustment_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutSheetNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCurrentCuttingWaste;
        private System.Windows.Forms.TextBox txtBoxCurrentPanels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}