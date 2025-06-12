namespace DyeHouse
{
    partial class frmGarmentDyeProduction
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkcboCMTSelection = new DyeHouse.CheckComboBox();
            this.chkcboSizesSelection = new DyeHouse.CheckComboBox();
            this.chkcboColoursSelection = new DyeHouse.CheckComboBox();
            this.chkcboStylesSelection = new DyeHouse.CheckComboBox();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "CMT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Colour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(274, 331);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(130, 33);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(151, 20);
            this.dtpFrom.TabIndex = 13;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(130, 66);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(151, 20);
            this.dtpTo.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Period From";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Period To";
            // 
            // chkcboCMTSelection
            // 
            this.chkcboCMTSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboCMTSelection.FormattingEnabled = true;
            this.chkcboCMTSelection.Location = new System.Drawing.Point(130, 120);
            this.chkcboCMTSelection.Name = "chkcboCMTSelection";
            this.chkcboCMTSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboCMTSelection.TabIndex = 0;
            this.chkcboCMTSelection.Text = "Select CMT\'s";
            // 
            // chkcboSizesSelection
            // 
            this.chkcboSizesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboSizesSelection.FormattingEnabled = true;
            this.chkcboSizesSelection.Location = new System.Drawing.Point(130, 228);
            this.chkcboSizesSelection.Name = "chkcboSizesSelection";
            this.chkcboSizesSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboSizesSelection.TabIndex = 3;
            this.chkcboSizesSelection.Text = "Select Sizes";
            // 
            // chkcboColoursSelection
            // 
            this.chkcboColoursSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboColoursSelection.FormattingEnabled = true;
            this.chkcboColoursSelection.Location = new System.Drawing.Point(130, 192);
            this.chkcboColoursSelection.Name = "chkcboColoursSelection";
            this.chkcboColoursSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboColoursSelection.TabIndex = 2;
            this.chkcboColoursSelection.Text = "Select Colours";
            // 
            // chkcboStylesSelection
            // 
            this.chkcboStylesSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.chkcboStylesSelection.FormattingEnabled = true;
            this.chkcboStylesSelection.Location = new System.Drawing.Point(130, 156);
            this.chkcboStylesSelection.Name = "chkcboStylesSelection";
            this.chkcboStylesSelection.Size = new System.Drawing.Size(219, 21);
            this.chkcboStylesSelection.TabIndex = 1;
            this.chkcboStylesSelection.Text = "Select Styles";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.AutoCompleteCustomSource.AddRange(new string[] {
            "Batch No",
            "Style ",
            "Colour ",
            "Sizes"});
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Items.AddRange(new object[] {
            "Batch No",
            "Style",
            "Colour",
            "Size"});
            this.cmboReportOptions.Location = new System.Drawing.Point(130, 283);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(219, 21);
            this.cmboReportOptions.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 287);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Order by";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Style";
            // 
            // frmGarmentDyeProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 378);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmboReportOptions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.chkcboCMTSelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.chkcboSizesSelection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkcboColoursSelection);
            this.Controls.Add(this.chkcboStylesSelection);
            this.Controls.Add(this.label3);
            this.Name = "frmGarmentDyeProduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Garment Dye Production";
            this.Load += new System.EventHandler(this.frmGarmentDyeingProd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Label label1, label3, label4;
        private DyeHouse.CheckComboBox chkcboCMTSelection, chkcboStylesSelection, chkcboColoursSelection, chkcboSizesSelection;
        private System.Windows.Forms.Button btnSubmit;
        //  private System.Windows.Forms.ProgressBar pbarExpedite;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
    }
}