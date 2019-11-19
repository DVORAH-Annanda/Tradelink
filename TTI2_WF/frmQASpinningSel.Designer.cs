namespace TTI2_WF
{
    partial class frmQASpinningSel
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
            this.rbConsumption = new System.Windows.Forms.RadioButton();
            this.rbCapacityUtil = new System.Windows.Forms.RadioButton();
            this.rbInspection = new System.Windows.Forms.RadioButton();
            this.rbYarnProduction = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbYarnProduction);
            this.groupBox1.Controls.Add(this.rbConsumption);
            this.groupBox1.Controls.Add(this.rbCapacityUtil);
            this.groupBox1.Controls.Add(this.rbInspection);
            this.groupBox1.Location = new System.Drawing.Point(137, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Reports";
            // 
            // rbConsumption
            // 
            this.rbConsumption.AutoSize = true;
            this.rbConsumption.Location = new System.Drawing.Point(43, 144);
            this.rbConsumption.Name = "rbConsumption";
            this.rbConsumption.Size = new System.Drawing.Size(166, 17);
            this.rbConsumption.TabIndex = 2;
            this.rbConsumption.TabStop = true;
            this.rbConsumption.Text = "Non Stock items consumption";
            this.rbConsumption.UseVisualStyleBackColor = true;
            this.rbConsumption.CheckedChanged += new System.EventHandler(this.rbConsumption_CheckedChanged);
            // 
            // rbCapacityUtil
            // 
            this.rbCapacityUtil.AutoSize = true;
            this.rbCapacityUtil.Location = new System.Drawing.Point(43, 96);
            this.rbCapacityUtil.Name = "rbCapacityUtil";
            this.rbCapacityUtil.Size = new System.Drawing.Size(116, 17);
            this.rbCapacityUtil.TabIndex = 1;
            this.rbCapacityUtil.TabStop = true;
            this.rbCapacityUtil.Text = "Capacility Utilsation";
            this.rbCapacityUtil.UseVisualStyleBackColor = true;
            this.rbCapacityUtil.CheckedChanged += new System.EventHandler(this.rbCapacityUtil_CheckedChanged);
            // 
            // rbInspection
            // 
            this.rbInspection.AutoSize = true;
            this.rbInspection.Location = new System.Drawing.Point(43, 48);
            this.rbInspection.Name = "rbInspection";
            this.rbInspection.Size = new System.Drawing.Size(95, 17);
            this.rbInspection.TabIndex = 0;
            this.rbInspection.TabStop = true;
            this.rbInspection.Text = "QA Inspection ";
            this.rbInspection.UseVisualStyleBackColor = true;
            this.rbInspection.CheckedChanged += new System.EventHandler(this.rbInspection_CheckedChanged);
            // 
            // rbYarnProduction
            // 
            this.rbYarnProduction.AutoSize = true;
            this.rbYarnProduction.Location = new System.Drawing.Point(43, 192);
            this.rbYarnProduction.Name = "rbYarnProduction";
            this.rbYarnProduction.Size = new System.Drawing.Size(101, 17);
            this.rbYarnProduction.TabIndex = 3;
            this.rbYarnProduction.TabStop = true;
            this.rbYarnProduction.Text = "Yarn Production";
            this.rbYarnProduction.UseVisualStyleBackColor = true;
            this.rbYarnProduction.CheckedChanged += new System.EventHandler(this.rbYarnProduction_CheckedChanged);
            // 
            // frmQASpinningSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 415);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQASpinningSel";
            this.Text = "QA Spinning Report Selection";
            this.Load += new System.EventHandler(this.frmQASpinningSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbConsumption;
        private System.Windows.Forms.RadioButton rbCapacityUtil;
        private System.Windows.Forms.RadioButton rbInspection;
        private System.Windows.Forms.RadioButton rbYarnProduction;
    }
}