﻿namespace TTI2_WF
{
    partial class frmTLADMProductRating
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
            this.cmbLabels = new System.Windows.Forms.ComboBox();
            this.cmbStyles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTrims = new System.Windows.Forms.RadioButton();
            this.rbBody = new System.Windows.Forms.RadioButton();
            this.btnSaveTrims = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLabels
            // 
            this.cmbLabels.FormattingEnabled = true;
            this.cmbLabels.Location = new System.Drawing.Point(169, 8);
            this.cmbLabels.Name = "cmbLabels";
            this.cmbLabels.Size = new System.Drawing.Size(250, 21);
            this.cmbLabels.TabIndex = 0;
            this.cmbLabels.SelectedIndexChanged += new System.EventHandler(this.cmbLabels_SelectedIndexChanged);
            // 
            // cmbStyles
            // 
            this.cmbStyles.FormattingEnabled = true;
            this.cmbStyles.Location = new System.Drawing.Point(169, 64);
            this.cmbStyles.Name = "cmbStyles";
            this.cmbStyles.Size = new System.Drawing.Size(250, 21);
            this.cmbStyles.TabIndex = 1;
            this.cmbStyles.SelectedIndexChanged += new System.EventHandler(this.cmbStyles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select a customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select a style";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(654, 276);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTrims);
            this.groupBox1.Controls.Add(this.rbBody);
            this.groupBox1.Location = new System.Drawing.Point(468, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 98);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // rbTrims
            // 
            this.rbTrims.AutoSize = true;
            this.rbTrims.Location = new System.Drawing.Point(15, 60);
            this.rbTrims.Name = "rbTrims";
            this.rbTrims.Size = new System.Drawing.Size(50, 17);
            this.rbTrims.TabIndex = 2;
            this.rbTrims.TabStop = true;
            this.rbTrims.Text = "Trims";
            this.rbTrims.UseVisualStyleBackColor = true;
            this.rbTrims.CheckedChanged += new System.EventHandler(this.rbTrims_CheckedChanged);
            this.rbTrims.Click += new System.EventHandler(this.rbTrims_Click);
            // 
            // rbBody
            // 
            this.rbBody.AutoSize = true;
            this.rbBody.Checked = true;
            this.rbBody.Location = new System.Drawing.Point(15, 19);
            this.rbBody.Name = "rbBody";
            this.rbBody.Size = new System.Drawing.Size(49, 17);
            this.rbBody.TabIndex = 0;
            this.rbBody.TabStop = true;
            this.rbBody.Text = "Body";
            this.rbBody.UseVisualStyleBackColor = true;
            this.rbBody.CheckedChanged += new System.EventHandler(this.rbBody_CheckedChanged);
            // 
            // btnSaveTrims
            // 
            this.btnSaveTrims.Location = new System.Drawing.Point(612, 460);
            this.btnSaveTrims.Name = "btnSaveTrims";
            this.btnSaveTrims.Size = new System.Drawing.Size(75, 23);
            this.btnSaveTrims.TabIndex = 8;
            this.btnSaveTrims.Text = "Save";
            this.btnSaveTrims.UseVisualStyleBackColor = true;
            this.btnSaveTrims.Click += new System.EventHandler(this.btnSaveTrims_Click);
            // 
            // frmTLADMProductRating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 526);
            this.Controls.Add(this.btnSaveTrims);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStyles);
            this.Controls.Add(this.cmbLabels);
            this.Name = "frmTLADMProductRating";
            this.Text = "Product Rating Definition";
            this.Load += new System.EventHandler(this.frmTLADMProductRating_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLabels;
        private System.Windows.Forms.ComboBox cmbStyles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBody;
        private System.Windows.Forms.RadioButton rbTrims;
        private System.Windows.Forms.Button btnSaveTrims;
    }
}