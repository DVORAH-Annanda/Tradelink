namespace Cutting
{
    partial class frmSelPanelStock
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
            this.cmboReportSelection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboWareHouseStore = new Cutting.CheckComboBox(); // System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmboReportSelection
            // 
            this.cmboReportSelection.FormattingEnabled = true;
            this.cmboReportSelection.Location = new System.Drawing.Point(240, 86);
            this.cmboReportSelection.Name = "cmboReportSelection";
            this.cmboReportSelection.Size = new System.Drawing.Size(162, 21);
            this.cmboReportSelection.TabIndex = 0;
            this.cmboReportSelection.SelectedIndexChanged += new System.EventHandler(this.cmboReportSelection_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report Selection";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(499, 206);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Location";
            // 
            // cmboWareHouseStore
            // 
            this.cmboWareHouseStore.FormattingEnabled = true;
            this.cmboWareHouseStore.Location = new System.Drawing.Point(240, 27);
            this.cmboWareHouseStore.Name = "cmboWareHouseStore";
            this.cmboWareHouseStore.Size = new System.Drawing.Size(162, 21);
            this.cmboWareHouseStore.TabIndex = 4;
            // 
            // frmSelPanelStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 264);
            this.Controls.Add(this.cmboWareHouseStore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboReportSelection);
            this.Name = "frmSelPanelStock";
            this.Text = "Panel Store Stock";
            this.Load += new System.EventHandler(this.frmSelPanelStock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboReportSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label2;
        // private System.Windows.Forms.ComboBox cmboWareHouseStore;
        private Cutting.CheckComboBox cmboWareHouseStore;
    }
}