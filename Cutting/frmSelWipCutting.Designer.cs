﻿namespace Cutting
{
    partial class frmSelWipCutting
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
            this.label3 = new System.Windows.Forms.Label();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboQuality = new Cutting.CheckComboBox();
            this.cmboColour = new Cutting.CheckComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboDepartments = new Cutting.CheckComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmboStyles = new Cutting.CheckComboBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(426, 487);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Report Sort Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(184, 403);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(121, 21);
            this.cmboReportOptions.TabIndex = 6;
            this.cmboReportOptions.SelectedIndexChanged += new System.EventHandler(this.cmboReportOptions_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Colour";
            // 
            // cmboQuality
            // 
            this.cmboQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQuality.FormattingEnabled = true;
            this.cmboQuality.Location = new System.Drawing.Point(184, 110);
            this.cmboQuality.Name = "cmboQuality";
            this.cmboQuality.Size = new System.Drawing.Size(233, 21);
            this.cmboQuality.TabIndex = 14;
            this.cmboQuality.Text = "Select Options";
            this.cmboQuality.SelectedIndexChanged += new System.EventHandler(this.cmboQuality_SelectedIndexChanged);
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(184, 212);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(233, 21);
            this.cmboColour.TabIndex = 15;
            this.cmboColour.Text = "Select Options";
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(120, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Location";
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(184, 60);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(161, 21);
            this.cmboDepartments.TabIndex = 17;
            this.cmboDepartments.Text = "Select Options";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(120, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Styles";
            // 
            // cmboStyles
            // 
            this.cmboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(184, 158);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(233, 21);
            this.cmboStyles.TabIndex = 19;
            this.cmboStyles.Text = "Select Options";
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // frmSelWipCutting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 533);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.cmboQuality);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmSelWipCutting";
            this.Text = "WIP Cutting";
            this.Load += new System.EventHandler(this.frmSelWipCutting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Cutting.CheckComboBox comboBox1;
        private Cutting.CheckComboBox cmboColour;
        private Cutting.CheckComboBox cmboQuality;
        private System.Windows.Forms.Label label6;
        //private System.Windows.Forms.ComboBox cmboDepartments;
        private Cutting.CheckComboBox cmboDepartments;
        private System.Windows.Forms.Label label7;
        private CheckComboBox cmboStyles;
    }
}