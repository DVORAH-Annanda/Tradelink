namespace DyeHouse
{
    partial class frmDyeOrdersSel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbFabricOnly = new System.Windows.Forms.RadioButton();
            this.rbGarmentsOnly = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbAdditionalBoth = new System.Windows.Forms.RadioButton();
            this.rbOrderClosed = new System.Windows.Forms.RadioButton();
            this.rbOrderOpen = new System.Windows.Forms.RadioButton();
            this.cmboReportOptions = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBoth);
            this.groupBox1.Controls.Add(this.rbFabricOnly);
            this.groupBox1.Controls.Add(this.rbGarmentsOnly);
            this.groupBox1.Location = new System.Drawing.Point(105, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Criteria";
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Location = new System.Drawing.Point(60, 69);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(47, 17);
            this.rbBoth.TabIndex = 2;
            this.rbBoth.Text = "Both";
            this.rbBoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbBoth.UseVisualStyleBackColor = true;
            // 
            // rbFabricOnly
            // 
            this.rbFabricOnly.AutoSize = true;
            this.rbFabricOnly.Location = new System.Drawing.Point(60, 44);
            this.rbFabricOnly.Name = "rbFabricOnly";
            this.rbFabricOnly.Size = new System.Drawing.Size(78, 17);
            this.rbFabricOnly.TabIndex = 1;
            this.rbFabricOnly.Text = "Fabric Only";
            this.rbFabricOnly.UseVisualStyleBackColor = true;
            // 
            // rbGarmentsOnly
            // 
            this.rbGarmentsOnly.AutoSize = true;
            this.rbGarmentsOnly.Checked = true;
            this.rbGarmentsOnly.Location = new System.Drawing.Point(60, 19);
            this.rbGarmentsOnly.Name = "rbGarmentsOnly";
            this.rbGarmentsOnly.Size = new System.Drawing.Size(127, 17);
            this.rbGarmentsOnly.TabIndex = 0;
            this.rbGarmentsOnly.TabStop = true;
            this.rbGarmentsOnly.Text = "Select Garments Only";
            this.rbGarmentsOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmboReportOptions);
            this.groupBox2.Location = new System.Drawing.Point(105, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort Criteria";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(459, 460);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbAdditionalBoth);
            this.groupBox3.Controls.Add(this.rbOrderClosed);
            this.groupBox3.Controls.Add(this.rbOrderOpen);
            this.groupBox3.Location = new System.Drawing.Point(105, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 126);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "additional Options";
            // 
            // rbAdditionalBoth
            // 
            this.rbAdditionalBoth.AutoSize = true;
            this.rbAdditionalBoth.Location = new System.Drawing.Point(60, 92);
            this.rbAdditionalBoth.Name = "rbAdditionalBoth";
            this.rbAdditionalBoth.Size = new System.Drawing.Size(47, 17);
            this.rbAdditionalBoth.TabIndex = 2;
            this.rbAdditionalBoth.TabStop = true;
            this.rbAdditionalBoth.Text = "Both";
            this.rbAdditionalBoth.UseVisualStyleBackColor = true;
            // 
            // rbOrderClosed
            // 
            this.rbOrderClosed.AutoSize = true;
            this.rbOrderClosed.Location = new System.Drawing.Point(60, 54);
            this.rbOrderClosed.Name = "rbOrderClosed";
            this.rbOrderClosed.Size = new System.Drawing.Size(86, 17);
            this.rbOrderClosed.TabIndex = 1;
            this.rbOrderClosed.TabStop = true;
            this.rbOrderClosed.Text = "Order Closed";
            this.rbOrderClosed.UseVisualStyleBackColor = true;
            // 
            // rbOrderOpen
            // 
            this.rbOrderOpen.AutoSize = true;
            this.rbOrderOpen.Checked = true;
            this.rbOrderOpen.Location = new System.Drawing.Point(60, 20);
            this.rbOrderOpen.Name = "rbOrderOpen";
            this.rbOrderOpen.Size = new System.Drawing.Size(80, 17);
            this.rbOrderOpen.TabIndex = 0;
            this.rbOrderOpen.TabStop = true;
            this.rbOrderOpen.Text = "Order Open";
            this.rbOrderOpen.UseVisualStyleBackColor = true;
            // 
            // cmboReportOptions
            // 
            this.cmboReportOptions.FormattingEnabled = true;
            this.cmboReportOptions.Location = new System.Drawing.Point(27, 28);
            this.cmboReportOptions.Name = "cmboReportOptions";
            this.cmboReportOptions.Size = new System.Drawing.Size(251, 21);
            this.cmboReportOptions.TabIndex = 0;
            // 
            // frmDyeOrdersSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 514);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDyeOrdersSel";
            this.Text = "Dye Order Reports Selection";
            this.Load += new System.EventHandler(this.frmDyeOrdersSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbFabricOnly;
        private System.Windows.Forms.RadioButton rbGarmentsOnly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbAdditionalBoth;
        private System.Windows.Forms.RadioButton rbOrderClosed;
        private System.Windows.Forms.RadioButton rbOrderOpen;
        private System.Windows.Forms.ComboBox cmboReportOptions;
    }
}