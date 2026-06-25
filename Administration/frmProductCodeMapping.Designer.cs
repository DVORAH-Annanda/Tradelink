namespace Administration
{
    partial class frmProductCodeMapping
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
            this.dgvMapping = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrintProductCodes = new System.Windows.Forms.Button();
            this.btnExportXL = new System.Windows.Forms.Button();
            this.cProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cColour = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cSize = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMapping
            // 
            this.dgvMapping.AllowUserToOrderColumns = true;
            this.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cProductCode,
            this.cStyle,
            this.cColour,
            this.cSize});
            this.dgvMapping.Location = new System.Drawing.Point(29, 63);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.Size = new System.Drawing.Size(699, 556);
            this.dgvMapping.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(653, 634);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Submit";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Vic Bay Branch Product Codes to Style, Colour and Size";
            // 
            // btnPrintProductCodes
            // 
            this.btnPrintProductCodes.Location = new System.Drawing.Point(623, 24);
            this.btnPrintProductCodes.Name = "btnPrintProductCodes";
            this.btnPrintProductCodes.Size = new System.Drawing.Size(105, 25);
            this.btnPrintProductCodes.TabIndex = 3;
            this.btnPrintProductCodes.Text = "Print as PDF";
            this.btnPrintProductCodes.UseVisualStyleBackColor = true;
            this.btnPrintProductCodes.Click += new System.EventHandler(this.btnPrintProductCodes_Click);
            // 
            // btnExportXL
            // 
            this.btnExportXL.Location = new System.Drawing.Point(512, 24);
            this.btnExportXL.Name = "btnExportXL";
            this.btnExportXL.Size = new System.Drawing.Size(105, 25);
            this.btnExportXL.TabIndex = 4;
            this.btnExportXL.Text = "Export to Excel";
            this.btnExportXL.UseVisualStyleBackColor = true;
            this.btnExportXL.Click += new System.EventHandler(this.btnExportXL_Click);
            // 
            // cProductCode
            // 
            this.cProductCode.HeaderText = "Product Code";
            this.cProductCode.Name = "cProductCode";
            this.cProductCode.Width = 125;
            // 
            // cStyle
            // 
            this.cStyle.HeaderText = "Style";
            this.cStyle.Name = "cStyle";
            this.cStyle.Width = 245;
            // 
            // cColour
            // 
            this.cColour.HeaderText = "Colour";
            this.cColour.Name = "cColour";
            this.cColour.Width = 185;
            // 
            // cSize
            // 
            this.cSize.HeaderText = "Size";
            this.cSize.Name = "cSize";
            this.cSize.Width = 85;
            // 
            // frmProductCodeMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 681);
            this.Controls.Add(this.btnExportXL);
            this.Controls.Add(this.btnPrintProductCodes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvMapping);
            this.Name = "frmProductCodeMapping";
            this.Text = "Product Code Mapping";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMapping;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintProductCodes;
        private System.Windows.Forms.Button btnExportXL;
        private System.Windows.Forms.DataGridViewTextBoxColumn cProductCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn cStyle;
        private System.Windows.Forms.DataGridViewComboBoxColumn cColour;
        private System.Windows.Forms.DataGridViewComboBoxColumn cSize;
    }
}