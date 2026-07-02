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
            this.cProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cColour = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cSize = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrintProductCodes = new System.Windows.Forms.Button();
            this.btnExportXL = new System.Windows.Forms.Button();
            this.btnDeleteProductCode = new System.Windows.Forms.Button();
            this.grpbxProductCodeFilters = new System.Windows.Forms.GroupBox();
            this.lblFilterCount = new System.Windows.Forms.Label();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblColour = new System.Windows.Forms.Label();
            this.lblStyle = new System.Windows.Forms.Label();
            this.txtFilterProductCode = new System.Windows.Forms.TextBox();
            this.lblProductCode = new System.Windows.Forms.Label();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.cmboFilterSize = new Administration.CheckComboBox();
            this.cmboFilterColour = new Administration.CheckComboBox();
            this.cmboFilterStyle = new Administration.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.grpbxProductCodeFilters.SuspendLayout();
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
            this.dgvMapping.Location = new System.Drawing.Point(29, 254);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.Size = new System.Drawing.Size(699, 468);
            this.dgvMapping.TabIndex = 0;
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(653, 737);
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
            this.label1.Location = new System.Drawing.Point(26, 30);
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
            // btnDeleteProductCode
            // 
            this.btnDeleteProductCode.Enabled = false;
            this.btnDeleteProductCode.Location = new System.Drawing.Point(29, 737);
            this.btnDeleteProductCode.Name = "btnDeleteProductCode";
            this.btnDeleteProductCode.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteProductCode.TabIndex = 5;
            this.btnDeleteProductCode.Text = "Delete selected row";
            this.btnDeleteProductCode.UseVisualStyleBackColor = true;
            this.btnDeleteProductCode.Click += new System.EventHandler(this.btnDeleteProductCode_Click);
            // 
            // grpbxProductCodeFilters
            // 
            this.grpbxProductCodeFilters.AutoSize = true;
            this.grpbxProductCodeFilters.Controls.Add(this.lblProductCount);
            this.grpbxProductCodeFilters.Controls.Add(this.lblFilterCount);
            this.grpbxProductCodeFilters.Controls.Add(this.btnClearFilters);
            this.grpbxProductCodeFilters.Controls.Add(this.cmboFilterSize);
            this.grpbxProductCodeFilters.Controls.Add(this.lblSize);
            this.grpbxProductCodeFilters.Controls.Add(this.cmboFilterColour);
            this.grpbxProductCodeFilters.Controls.Add(this.lblColour);
            this.grpbxProductCodeFilters.Controls.Add(this.cmboFilterStyle);
            this.grpbxProductCodeFilters.Controls.Add(this.lblStyle);
            this.grpbxProductCodeFilters.Controls.Add(this.txtFilterProductCode);
            this.grpbxProductCodeFilters.Controls.Add(this.lblProductCode);
            this.grpbxProductCodeFilters.Location = new System.Drawing.Point(29, 72);
            this.grpbxProductCodeFilters.Name = "grpbxProductCodeFilters";
            this.grpbxProductCodeFilters.Size = new System.Drawing.Size(698, 161);
            this.grpbxProductCodeFilters.TabIndex = 6;
            this.grpbxProductCodeFilters.TabStop = false;
            this.grpbxProductCodeFilters.Text = "Filter Product Codes";
            // 
            // lblFilterCount
            // 
            this.lblFilterCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterCount.Location = new System.Drawing.Point(518, 101);
            this.lblFilterCount.Name = "lblFilterCount";
            this.lblFilterCount.Size = new System.Drawing.Size(155, 33);
            this.lblFilterCount.TabIndex = 9;
            this.lblFilterCount.Text = "Count";
            this.lblFilterCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(518, 31);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(155, 29);
            this.btnClearFilters.TabIndex = 8;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(24, 121);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 6;
            this.lblSize.Text = "Size";
            // 
            // lblColour
            // 
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(24, 92);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(37, 13);
            this.lblColour.TabIndex = 4;
            this.lblColour.Text = "Colour";
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(24, 62);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(30, 13);
            this.lblStyle.TabIndex = 2;
            this.lblStyle.Text = "Style";
            // 
            // txtFilterProductCode
            // 
            this.txtFilterProductCode.Location = new System.Drawing.Point(102, 30);
            this.txtFilterProductCode.Name = "txtFilterProductCode";
            this.txtFilterProductCode.Size = new System.Drawing.Size(236, 20);
            this.txtFilterProductCode.TabIndex = 1;
            // 
            // lblProductCode
            // 
            this.lblProductCode.AutoSize = true;
            this.lblProductCode.Location = new System.Drawing.Point(24, 34);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(72, 13);
            this.lblProductCode.TabIndex = 0;
            this.lblProductCode.Text = "Product Code";
            // 
            // lblProductCount
            // 
            this.lblProductCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductCount.Location = new System.Drawing.Point(516, 82);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(158, 18);
            this.lblProductCount.TabIndex = 10;
            this.lblProductCount.Text = "Product Count";
            this.lblProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmboFilterSize
            // 
            this.cmboFilterSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFilterSize.FormattingEnabled = true;
            this.cmboFilterSize.Location = new System.Drawing.Point(102, 117);
            this.cmboFilterSize.Name = "cmboFilterSize";
            this.cmboFilterSize.Size = new System.Drawing.Size(236, 21);
            this.cmboFilterSize.TabIndex = 7;
            this.cmboFilterSize.Text = "Select Options";
            // 
            // cmboFilterColour
            // 
            this.cmboFilterColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFilterColour.FormattingEnabled = true;
            this.cmboFilterColour.Location = new System.Drawing.Point(102, 88);
            this.cmboFilterColour.Name = "cmboFilterColour";
            this.cmboFilterColour.Size = new System.Drawing.Size(236, 21);
            this.cmboFilterColour.TabIndex = 5;
            this.cmboFilterColour.Text = "Select Options";
            // 
            // cmboFilterStyle
            // 
            this.cmboFilterStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFilterStyle.FormattingEnabled = true;
            this.cmboFilterStyle.Location = new System.Drawing.Point(102, 58);
            this.cmboFilterStyle.Name = "cmboFilterStyle";
            this.cmboFilterStyle.Size = new System.Drawing.Size(236, 21);
            this.cmboFilterStyle.TabIndex = 3;
            this.cmboFilterStyle.Text = "Select Options";
            // 
            // frmProductCodeMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 782);
            this.Controls.Add(this.grpbxProductCodeFilters);
            this.Controls.Add(this.btnDeleteProductCode);
            this.Controls.Add(this.btnExportXL);
            this.Controls.Add(this.btnPrintProductCodes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvMapping);
            this.Name = "frmProductCodeMapping";
            this.Text = "Product Code Mapping";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.grpbxProductCodeFilters.ResumeLayout(false);
            this.grpbxProductCodeFilters.PerformLayout();
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
        private System.Windows.Forms.Button btnDeleteProductCode;
        private System.Windows.Forms.GroupBox grpbxProductCodeFilters;
        private System.Windows.Forms.TextBox txtFilterProductCode;
        private System.Windows.Forms.Label lblProductCode;
        private System.Windows.Forms.Label lblSize;
        private CheckComboBox cmboFilterColour;
        private System.Windows.Forms.Label lblColour;
        private CheckComboBox cmboFilterStyle;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblFilterCount;
        private System.Windows.Forms.Button btnClearFilters;
        private CheckComboBox cmboFilterSize;
        private System.Windows.Forms.Label lblProductCount;
    }
}