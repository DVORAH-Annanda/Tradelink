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
            this.cmboWareHouseStore = new Cutting.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPanelAttributes = new System.Windows.Forms.CheckBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.rbCostingColoursOnly = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCostingWhiteOnly = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.btnSubmit.Location = new System.Drawing.Point(509, 361);
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
            this.cmboWareHouseStore.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboWareHouseStore.FormattingEnabled = true;
            this.cmboWareHouseStore.Location = new System.Drawing.Point(240, 27);
            this.cmboWareHouseStore.Name = "cmboWareHouseStore";
            this.cmboWareHouseStore.Size = new System.Drawing.Size(162, 21);
            this.cmboWareHouseStore.TabIndex = 4;
            this.cmboWareHouseStore.Text = "Select Options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPanelAttributes);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Location = new System.Drawing.Point(144, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 141);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Panel Stock By Attribute";
            // 
            // chkPanelAttributes
            // 
            this.chkPanelAttributes.AutoSize = true;
            this.chkPanelAttributes.Location = new System.Drawing.Point(143, 22);
            this.chkPanelAttributes.Name = "chkPanelAttributes";
            this.chkPanelAttributes.Size = new System.Drawing.Size(80, 17);
            this.chkPanelAttributes.TabIndex = 4;
            this.chkPanelAttributes.Text = "By Attribute";
            this.chkPanelAttributes.UseVisualStyleBackColor = true;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(143, 92);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(125, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "To Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "From Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(143, 55);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(125, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // rbCostingColoursOnly
            // 
            this.rbCostingColoursOnly.AutoSize = true;
            this.rbCostingColoursOnly.Location = new System.Drawing.Point(24, 23);
            this.rbCostingColoursOnly.Name = "rbCostingColoursOnly";
            this.rbCostingColoursOnly.Size = new System.Drawing.Size(119, 17);
            this.rbCostingColoursOnly.TabIndex = 6;
            this.rbCostingColoursOnly.TabStop = true;
            this.rbCostingColoursOnly.Text = "Costing Colous Only";
            this.rbCostingColoursOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCostingWhiteOnly);
            this.groupBox2.Controls.Add(this.rbCostingColoursOnly);
            this.groupBox2.Location = new System.Drawing.Point(240, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 98);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // rbCostingWhiteOnly
            // 
            this.rbCostingWhiteOnly.AutoSize = true;
            this.rbCostingWhiteOnly.Location = new System.Drawing.Point(24, 63);
            this.rbCostingWhiteOnly.Name = "rbCostingWhiteOnly";
            this.rbCostingWhiteOnly.Size = new System.Drawing.Size(155, 17);
            this.rbCostingWhiteOnly.TabIndex = 7;
            this.rbCostingWhiteOnly.TabStop = true;
            this.rbCostingWhiteOnly.Text = "Costing Colous Whites Only";
            this.rbCostingWhiteOnly.UseVisualStyleBackColor = true;
            // 
            // frmSelPanelStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 416);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboWareHouseStore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboReportSelection);
            this.Name = "frmSelPanelStock";
            this.Text = "Panel Store Stock";
            this.Load += new System.EventHandler(this.frmSelPanelStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPanelAttributes;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.RadioButton rbCostingColoursOnly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCostingWhiteOnly;
    }
}