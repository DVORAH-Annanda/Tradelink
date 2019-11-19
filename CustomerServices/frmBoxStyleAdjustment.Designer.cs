namespace CustomerServices
{
    partial class frmBoxStyleAdjustment
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
            this.oBoxNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmboStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radStyle = new System.Windows.Forms.RadioButton();
            this.radSizes = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Box Number to change";
            // 
            // oBoxNumber
            // 
            this.oBoxNumber.Location = new System.Drawing.Point(283, 37);
            this.oBoxNumber.Name = "oBoxNumber";
            this.oBoxNumber.Size = new System.Drawing.Size(172, 20);
            this.oBoxNumber.TabIndex = 1;
            this.oBoxNumber.TextChanged += new System.EventHandler(this.oBoxNumber_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Controls.Add(this.txtColour);
            this.groupBox1.Controls.Add(this.txtStyle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(151, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 213);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Styles";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(110, 142);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(215, 20);
            this.txtSize.TabIndex = 5;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(110, 91);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(215, 20);
            this.txtColour.TabIndex = 4;
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(110, 40);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(215, 20);
            this.txtStyle.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Colour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Style";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(504, 419);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // cmboStyle
            // 
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(261, 382);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(259, 21);
            this.cmboStyle.TabIndex = 4;
            this.cmboStyle.SelectedIndexChanged += new System.EventHandler(this.cmboStyle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "New Style";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radSizes);
            this.groupBox2.Controls.Add(this.radStyle);
            this.groupBox2.Location = new System.Drawing.Point(221, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 50);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // radStyle
            // 
            this.radStyle.AutoSize = true;
            this.radStyle.Location = new System.Drawing.Point(28, 20);
            this.radStyle.Name = "radStyle";
            this.radStyle.Size = new System.Drawing.Size(103, 17);
            this.radStyle.TabIndex = 0;
            this.radStyle.TabStop = true;
            this.radStyle.Text = "Style Adjustment";
            this.radStyle.UseVisualStyleBackColor = true;
            this.radStyle.CheckedChanged += new System.EventHandler(this.radStyle_CheckedChanged);
            // 
            // radSizes
            // 
            this.radSizes.AutoSize = true;
            this.radSizes.Location = new System.Drawing.Point(154, 20);
            this.radSizes.Name = "radSizes";
            this.radSizes.Size = new System.Drawing.Size(100, 17);
            this.radSizes.TabIndex = 1;
            this.radSizes.TabStop = true;
            this.radSizes.Text = "Size Adjustment";
            this.radSizes.UseVisualStyleBackColor = true;
            this.radSizes.CheckedChanged += new System.EventHandler(this.radSizes_CheckedChanged);
            // 
            // frmBoxStyleAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 464);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.oBoxNumber);
            this.Controls.Add(this.label1);
            this.Name = "frmBoxStyleAdjustment";
            this.Text = "Box Style / Size  Adjustment";
            this.Load += new System.EventHandler(this.frmBoxStyleAdjustment_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oBoxNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radSizes;
        private System.Windows.Forms.RadioButton radStyle;
    }
}