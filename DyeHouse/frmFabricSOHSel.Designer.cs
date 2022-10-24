namespace DyeHouse
{
    partial class frmFabricSOHSel
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.rbBoughtIn = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPenSoldNotDeliv = new System.Windows.Forms.RadioButton();
            this.rbPenNotSoldNotDeliv = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboWhseStore = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cmboReportOptions);
            this.groupBox3.Location = new System.Drawing.Point(92, 394);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 101);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sort Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sort Options";
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(118, 41);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(194, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(410, 543);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // rbBoughtIn
            // 
            this.rbBoughtIn.AutoSize = true;
            this.rbBoughtIn.Location = new System.Drawing.Point(32, 69);
            this.rbBoughtIn.Name = "rbBoughtIn";
            this.rbBoughtIn.Size = new System.Drawing.Size(74, 17);
            this.rbBoughtIn.TabIndex = 0;
            this.rbBoughtIn.TabStop = true;
            this.rbBoughtIn.Text = "Bought In ";
            this.rbBoughtIn.UseVisualStyleBackColor = true;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Location = new System.Drawing.Point(32, 27);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(68, 17);
            this.rbStandard.TabIndex = 1;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBoughtIn);
            this.groupBox1.Controls.Add(this.rbStandard);
            this.groupBox1.Location = new System.Drawing.Point(119, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 107);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fabric Type";
            // 
            // rbPenSoldNotDeliv
            // 
            this.rbPenSoldNotDeliv.AutoSize = true;
            this.rbPenSoldNotDeliv.Location = new System.Drawing.Point(24, 63);
            this.rbPenSoldNotDeliv.Name = "rbPenSoldNotDeliv";
            this.rbPenSoldNotDeliv.Size = new System.Drawing.Size(147, 17);
            this.rbPenSoldNotDeliv.TabIndex = 3;
            this.rbPenSoldNotDeliv.TabStop = true;
            this.rbPenSoldNotDeliv.Text = "Pending Sold Not Delived";
            this.rbPenSoldNotDeliv.UseVisualStyleBackColor = true;
            // 
            // rbPenNotSoldNotDeliv
            // 
            this.rbPenNotSoldNotDeliv.AutoSize = true;
            this.rbPenNotSoldNotDeliv.Location = new System.Drawing.Point(24, 19);
            this.rbPenNotSoldNotDeliv.Name = "rbPenNotSoldNotDeliv";
            this.rbPenNotSoldNotDeliv.Size = new System.Drawing.Size(176, 17);
            this.rbPenNotSoldNotDeliv.TabIndex = 2;
            this.rbPenNotSoldNotDeliv.TabStop = true;
            this.rbPenNotSoldNotDeliv.Text = "Pending Not Sold Not Delivered";
            this.rbPenNotSoldNotDeliv.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Store Selection";
            // 
            // cmboWhseStore
            // 
            this.cmboWhseStore.FormattingEnabled = true;
            this.cmboWhseStore.Location = new System.Drawing.Point(210, 58);
            this.cmboWhseStore.Name = "cmboWhseStore";
            this.cmboWhseStore.Size = new System.Drawing.Size(184, 21);
            this.cmboWhseStore.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPenNotSoldNotDeliv);
            this.groupBox2.Controls.Add(this.rbPenSoldNotDeliv);
            this.groupBox2.Location = new System.Drawing.Point(127, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // frmFabricSOHSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 597);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmboWhseStore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmFabricSOHSel";
            this.Text = "Fabric Stock On Hand Selection";
            this.Load += new System.EventHandler(this.frmFabricSOHSel_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboReportOptions;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RadioButton rbBoughtIn;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboWhseStore;
        private System.Windows.Forms.RadioButton rbPenSoldNotDeliv;
        private System.Windows.Forms.RadioButton rbPenNotSoldNotDeliv;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}