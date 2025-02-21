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
            this.dgvMapping.Location = new System.Drawing.Point(34, 58);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.Size = new System.Drawing.Size(569, 424);
            this.dgvMapping.TabIndex = 0;
            // 
            // cProductCode
            // 
            this.cProductCode.HeaderText = "Product Code";
            this.cProductCode.Name = "cProductCode";
            // 
            // cStyle
            // 
            this.cStyle.HeaderText = "Style";
            this.cStyle.Name = "cStyle";
            this.cStyle.Width = 200;
            // 
            // cColour
            // 
            this.cColour.HeaderText = "Colour";
            this.cColour.Name = "cColour";
            this.cColour.Width = 125;
            // 
            // cSize
            // 
            this.cSize.HeaderText = "Size";
            this.cSize.Name = "cSize";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(528, 496);
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
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Vic Bay Branch Product Codes to Style, Colour and Size :";
            // 
            // frmProductCodeMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 543);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn cProductCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn cStyle;
        private System.Windows.Forms.DataGridViewComboBoxColumn cColour;
        private System.Windows.Forms.DataGridViewComboBoxColumn cSize;
        private System.Windows.Forms.Label label1;
    }
}