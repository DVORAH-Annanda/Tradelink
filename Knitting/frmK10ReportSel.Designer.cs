namespace Knitting
{
    partial class frmK10ReportSel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkBoughtInFabric = new System.Windows.Forms.CheckBox();
            this.cmboStockTake = new Knitting.CheckComboBox();
            this.cmboProductGroup = new Knitting.CheckComboBox();
            this.cmboProduct = new Knitting.CheckComboBox();
            this.cmboStore = new Knitting.CheckComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDetail = new System.Windows.Forms.RadioButton();
            this.rbBIFSummarised = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGradeA = new System.Windows.Forms.CheckBox();
            this.cbGradeB = new System.Windows.Forms.CheckBox();
            this.cbGradeC = new System.Windows.Forms.CheckBox();
            this.cbIncludeGradweAWithWarnings = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grade";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stock Take Frequency";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(555, 491);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkBoughtInFabric
            // 
            this.chkBoughtInFabric.AutoSize = true;
            this.chkBoughtInFabric.Location = new System.Drawing.Point(277, 341);
            this.chkBoughtInFabric.Name = "chkBoughtInFabric";
            this.chkBoughtInFabric.Size = new System.Drawing.Size(104, 17);
            this.chkBoughtInFabric.TabIndex = 11;
            this.chkBoughtInFabric.Text = "Bought In Fabric";
            this.chkBoughtInFabric.UseVisualStyleBackColor = true;
            // 
            // cmboStockTake
            // 
            this.cmboStockTake.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStockTake.FormattingEnabled = true;
            this.cmboStockTake.Location = new System.Drawing.Point(277, 290);
            this.cmboStockTake.Name = "cmboStockTake";
            this.cmboStockTake.Size = new System.Drawing.Size(121, 21);
            this.cmboStockTake.TabIndex = 9;
            this.cmboStockTake.Text = "Select Options";
            this.cmboStockTake.SelectedIndexChanged += new System.EventHandler(this.cmboStockTake_SelectedIndexChanged);
            // 
            // cmboProductGroup
            // 
            this.cmboProductGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboProductGroup.FormattingEnabled = true;
            this.cmboProductGroup.Location = new System.Drawing.Point(277, 246);
            this.cmboProductGroup.Name = "cmboProductGroup";
            this.cmboProductGroup.Size = new System.Drawing.Size(226, 21);
            this.cmboProductGroup.TabIndex = 7;
            this.cmboProductGroup.Text = "Select Options";
            this.cmboProductGroup.SelectedIndexChanged += new System.EventHandler(this.cmboProductGroup_SelectedIndexChanged);
            // 
            // cmboProduct
            // 
            this.cmboProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboProduct.FormattingEnabled = true;
            this.cmboProduct.Location = new System.Drawing.Point(277, 202);
            this.cmboProduct.Name = "cmboProduct";
            this.cmboProduct.Size = new System.Drawing.Size(226, 21);
            this.cmboProduct.TabIndex = 5;
            this.cmboProduct.Text = "Select Options";
            this.cmboProduct.SelectedIndexChanged += new System.EventHandler(this.cmboProduct_SelectedIndexChanged);
            // 
            // cmboStore
            // 
            this.cmboStore.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStore.FormattingEnabled = true;
            this.cmboStore.Location = new System.Drawing.Point(277, 43);
            this.cmboStore.Name = "cmboStore";
            this.cmboStore.Size = new System.Drawing.Size(226, 21);
            this.cmboStore.TabIndex = 1;
            this.cmboStore.Text = "Select Options";
            this.cmboStore.SelectedIndexChanged += new System.EventHandler(this.cmboStore_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDetail);
            this.groupBox1.Controls.Add(this.rbBIFSummarised);
            this.groupBox1.Location = new System.Drawing.Point(249, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // rbDetail
            // 
            this.rbDetail.AutoSize = true;
            this.rbDetail.Location = new System.Drawing.Point(138, 25);
            this.rbDetail.Name = "rbDetail";
            this.rbDetail.Size = new System.Drawing.Size(52, 17);
            this.rbDetail.TabIndex = 13;
            this.rbDetail.TabStop = true;
            this.rbDetail.Text = "Detail";
            this.rbDetail.UseVisualStyleBackColor = true;
            // 
            // rbBIFSummarised
            // 
            this.rbBIFSummarised.AutoSize = true;
            this.rbBIFSummarised.Location = new System.Drawing.Point(21, 25);
            this.rbBIFSummarised.Name = "rbBIFSummarised";
            this.rbBIFSummarised.Size = new System.Drawing.Size(82, 17);
            this.rbBIFSummarised.TabIndex = 12;
            this.rbBIFSummarised.TabStop = true;
            this.rbBIFSummarised.Text = "Summarised";
            this.rbBIFSummarised.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbIncludeGradweAWithWarnings);
            this.groupBox2.Controls.Add(this.cbGradeC);
            this.groupBox2.Controls.Add(this.cbGradeB);
            this.groupBox2.Controls.Add(this.cbGradeA);
            this.groupBox2.Location = new System.Drawing.Point(277, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 115);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Quality";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(152, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Quality Group\r\n";
            // 
            // cbGradeA
            // 
            this.cbGradeA.AutoSize = true;
            this.cbGradeA.Checked = true;
            this.cbGradeA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGradeA.Location = new System.Drawing.Point(59, 13);
            this.cbGradeA.Name = "cbGradeA";
            this.cbGradeA.Size = new System.Drawing.Size(65, 17);
            this.cbGradeA.TabIndex = 0;
            this.cbGradeA.Text = "Grade A";
            this.cbGradeA.UseVisualStyleBackColor = true;
            // 
            // cbGradeB
            // 
            this.cbGradeB.AutoSize = true;
            this.cbGradeB.Location = new System.Drawing.Point(59, 36);
            this.cbGradeB.Name = "cbGradeB";
            this.cbGradeB.Size = new System.Drawing.Size(65, 17);
            this.cbGradeB.TabIndex = 1;
            this.cbGradeB.Text = "Grade B";
            this.cbGradeB.UseVisualStyleBackColor = true;
            // 
            // cbGradeC
            // 
            this.cbGradeC.AutoSize = true;
            this.cbGradeC.Location = new System.Drawing.Point(59, 59);
            this.cbGradeC.Name = "cbGradeC";
            this.cbGradeC.Size = new System.Drawing.Size(65, 17);
            this.cbGradeC.TabIndex = 2;
            this.cbGradeC.Text = "Grade C";
            this.cbGradeC.UseVisualStyleBackColor = true;
            // 
            // cbIncludeGradweAWithWarnings
            // 
            this.cbIncludeGradweAWithWarnings.AutoSize = true;
            this.cbIncludeGradweAWithWarnings.Location = new System.Drawing.Point(59, 82);
            this.cbIncludeGradweAWithWarnings.Name = "cbIncludeGradweAWithWarnings";
            this.cbIncludeGradweAWithWarnings.Size = new System.Drawing.Size(138, 17);
            this.cbIncludeGradweAWithWarnings.TabIndex = 3;
            this.cbIncludeGradweAWithWarnings.Text = "Grade A With Warnings\r\n";
            this.cbIncludeGradweAWithWarnings.UseVisualStyleBackColor = true;
            // 
            // frmK10ReportSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 535);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkBoughtInFabric);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmboStockTake);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboProductGroup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboProduct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboStore);
            this.Controls.Add(this.label1);
            this.Name = "frmK10ReportSel";
            this.Text = "Greige Stock Report";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        /* private System.Windows.Forms.ComboBox cmboStore;
        private System.Windows.Forms.ComboBox cmboProductGroup;
        private System.Windows.Forms.ComboBox cmboProduct;
        private System.Windows.Forms.ComboBox cmboStockTake; */

        private Knitting.CheckComboBox cmboStore;
        private Knitting.CheckComboBox cmboProductGroup;
        private Knitting.CheckComboBox cmboProduct;
        private Knitting.CheckComboBox cmboStockTake;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkBoughtInFabric;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDetail;
        private System.Windows.Forms.RadioButton rbBIFSummarised;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbIncludeGradweAWithWarnings;
        private System.Windows.Forms.CheckBox cbGradeC;
        private System.Windows.Forms.CheckBox cbGradeB;
        private System.Windows.Forms.CheckBox cbGradeA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}