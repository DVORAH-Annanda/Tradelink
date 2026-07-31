namespace ProductionPlanning
{
    partial class frmStockBelowReorderLevel
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
            this.lblWarehouses = new System.Windows.Forms.Label();
            this.lblStyles = new System.Windows.Forms.Label();
            this.lblColours = new System.Windows.Forms.Label();
            this.lblSizes = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.chkcboSizesSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboColoursSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboStylesSelection = new ProductionPlanning.CheckComboBox();
            this.chkcboWarehousesSelection = new ProductionPlanning.CheckComboBox();
            this.lblWareHouseFilterText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWarehouses
            // 
            this.lblWarehouses.AutoSize = true;
            this.lblWarehouses.Location = new System.Drawing.Point(22, 28);
            this.lblWarehouses.Name = "lblWarehouses";
            this.lblWarehouses.Size = new System.Drawing.Size(67, 13);
            this.lblWarehouses.TabIndex = 0;
            this.lblWarehouses.Text = "Warehouses";
            // 
            // lblStyles
            // 
            this.lblStyles.AutoSize = true;
            this.lblStyles.Location = new System.Drawing.Point(54, 55);
            this.lblStyles.Name = "lblStyles";
            this.lblStyles.Size = new System.Drawing.Size(35, 13);
            this.lblStyles.TabIndex = 2;
            this.lblStyles.Text = "Styles";
            // 
            // lblColours
            // 
            this.lblColours.AutoSize = true;
            this.lblColours.Location = new System.Drawing.Point(47, 82);
            this.lblColours.Name = "lblColours";
            this.lblColours.Size = new System.Drawing.Size(42, 13);
            this.lblColours.TabIndex = 4;
            this.lblColours.Text = "Colours";
            // 
            // lblSizes
            // 
            this.lblSizes.AutoSize = true;
            this.lblSizes.Location = new System.Drawing.Point(57, 109);
            this.lblSizes.Name = "lblSizes";
            this.lblSizes.Size = new System.Drawing.Size(32, 13);
            this.lblSizes.TabIndex = 6;
            this.lblSizes.Text = "Sizes";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(209, 166);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(136, 23);
            this.btnGenerateReport.TabIndex = 8;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // chkcboSizesSelection
            // 
            this.chkcboSizesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboSizesSelection.FormattingEnabled = true;
            this.chkcboSizesSelection.Location = new System.Drawing.Point(95, 105);
            this.chkcboSizesSelection.Name = "chkcboSizesSelection";
            this.chkcboSizesSelection.Size = new System.Drawing.Size(250, 21);
            this.chkcboSizesSelection.TabIndex = 7;
            this.chkcboSizesSelection.Text = "Select Options";
            // 
            // chkcboColoursSelection
            // 
            this.chkcboColoursSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboColoursSelection.FormattingEnabled = true;
            this.chkcboColoursSelection.Location = new System.Drawing.Point(95, 78);
            this.chkcboColoursSelection.Name = "chkcboColoursSelection";
            this.chkcboColoursSelection.Size = new System.Drawing.Size(250, 21);
            this.chkcboColoursSelection.TabIndex = 5;
            this.chkcboColoursSelection.Text = "Select Options";
            // 
            // chkcboStylesSelection
            // 
            this.chkcboStylesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboStylesSelection.FormattingEnabled = true;
            this.chkcboStylesSelection.Location = new System.Drawing.Point(95, 51);
            this.chkcboStylesSelection.Name = "chkcboStylesSelection";
            this.chkcboStylesSelection.Size = new System.Drawing.Size(250, 21);
            this.chkcboStylesSelection.TabIndex = 3;
            this.chkcboStylesSelection.Text = "Select Options";
            // 
            // chkcboWarehousesSelection
            // 
            this.chkcboWarehousesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboWarehousesSelection.FormattingEnabled = true;
            this.chkcboWarehousesSelection.Location = new System.Drawing.Point(95, 24);
            this.chkcboWarehousesSelection.Name = "chkcboWarehousesSelection";
            this.chkcboWarehousesSelection.Size = new System.Drawing.Size(250, 21);
            this.chkcboWarehousesSelection.TabIndex = 1;
            this.chkcboWarehousesSelection.Text = "Select Options";
            // 
            // lblWareHouseFilterText
            // 
            this.lblWareHouseFilterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWareHouseFilterText.Location = new System.Drawing.Point(12, 129);
            this.lblWareHouseFilterText.Name = "lblWareHouseFilterText";
            this.lblWareHouseFilterText.Size = new System.Drawing.Size(353, 34);
            this.lblWareHouseFilterText.TabIndex = 9;
            this.lblWareHouseFilterText.Text = "label1";
            this.lblWareHouseFilterText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStockBelowReorderLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 213);
            this.Controls.Add(this.lblWareHouseFilterText);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.chkcboSizesSelection);
            this.Controls.Add(this.lblSizes);
            this.Controls.Add(this.chkcboColoursSelection);
            this.Controls.Add(this.lblColours);
            this.Controls.Add(this.chkcboStylesSelection);
            this.Controls.Add(this.lblStyles);
            this.Controls.Add(this.chkcboWarehousesSelection);
            this.Controls.Add(this.lblWarehouses);
            this.Name = "frmStockBelowReorderLevel";
            this.Text = "Stock below Re-order Level Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarehouses;
        private CheckComboBox chkcboWarehousesSelection;
        private System.Windows.Forms.Label lblStyles;
        private CheckComboBox chkcboStylesSelection;
        private System.Windows.Forms.Label lblColours;
        private CheckComboBox chkcboColoursSelection;
        private System.Windows.Forms.Label lblSizes;
        private CheckComboBox chkcboSizesSelection;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label lblWareHouseFilterText;
    }
}