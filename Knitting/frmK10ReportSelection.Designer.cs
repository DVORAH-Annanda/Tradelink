﻿namespace Knitting
{
    partial class frmK10ReportSelection
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
            this.cmboStore = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboProduct = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboProductGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboStockTake = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "By Store";
            // 
            // cmboStore
            // 
            this.cmboStore.FormattingEnabled = true;
            this.cmboStore.Location = new System.Drawing.Point(277, 43);
            this.cmboStore.Name = "cmboStore";
            this.cmboStore.Size = new System.Drawing.Size(226, 21);
            this.cmboStore.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grade";
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(277, 87);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(78, 20);
            this.txtGrade.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Product";
            // 
            // cmboProduct
            // 
            this.cmboProduct.FormattingEnabled = true;
            this.cmboProduct.Location = new System.Drawing.Point(277, 130);
            this.cmboProduct.Name = "cmboProduct";
            this.cmboProduct.Size = new System.Drawing.Size(121, 21);
            this.cmboProduct.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Product Group";
            // 
            // cmboProductGroup
            // 
            this.cmboProductGroup.FormattingEnabled = true;
            this.cmboProductGroup.Location = new System.Drawing.Point(277, 174);
            this.cmboProductGroup.Name = "cmboProductGroup";
            this.cmboProductGroup.Size = new System.Drawing.Size(121, 21);
            this.cmboProductGroup.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stock Take Frequency";
            // 
            // cmboStockTake
            // 
            this.cmboStockTake.FormattingEnabled = true;
            this.cmboStockTake.Location = new System.Drawing.Point(277, 218);
            this.cmboStockTake.Name = "cmboStockTake";
            this.cmboStockTake.Size = new System.Drawing.Size(121, 21);
            this.cmboStockTake.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(555, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Report";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmK10ReportSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 327);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmboStockTake);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboProductGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboProduct);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboStore);
            this.Controls.Add(this.label1);
            this.Name = "frmK10ReportSelection";
            this.Text = "Greige Stock Report";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboStore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboProduct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboProductGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboStockTake;
        private System.Windows.Forms.Button button1;
    }
}