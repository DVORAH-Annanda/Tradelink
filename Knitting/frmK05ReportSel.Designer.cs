namespace Knitting
{
    partial class frmK05ReportSel
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbKnitOrder = new System.Windows.Forms.RadioButton();
            this.rbKnitMachime = new System.Windows.Forms.RadioButton();
            this.rbGreigeProduct = new System.Windows.Forms.RadioButton();
            this.rbProcesdLoss = new System.Windows.Forms.RadioButton();
            this.rbYarnType = new System.Windows.Forms.RadioButton();
            this.rbYarnTex = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please select from Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select to Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(235, 33);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(134, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(235, 74);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(134, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(504, 295);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "Report";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbYarnTex);
            this.groupBox1.Controls.Add(this.rbYarnType);
            this.groupBox1.Controls.Add(this.rbProcesdLoss);
            this.groupBox1.Controls.Add(this.rbGreigeProduct);
            this.groupBox1.Controls.Add(this.rbKnitMachime);
            this.groupBox1.Controls.Add(this.rbKnitOrder);
            this.groupBox1.Location = new System.Drawing.Point(99, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 135);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Sort Sequence Options";
            // 
            // rbKnitOrder
            // 
            this.rbKnitOrder.AutoSize = true;
            this.rbKnitOrder.Location = new System.Drawing.Point(20, 31);
            this.rbKnitOrder.Name = "rbKnitOrder";
            this.rbKnitOrder.Size = new System.Drawing.Size(72, 17);
            this.rbKnitOrder.TabIndex = 0;
            this.rbKnitOrder.TabStop = true;
            this.rbKnitOrder.Text = "Knit Order";
            this.rbKnitOrder.UseVisualStyleBackColor = true;
            // 
            // rbKnitMachime
            // 
            this.rbKnitMachime.AutoSize = true;
            this.rbKnitMachime.Location = new System.Drawing.Point(20, 64);
            this.rbKnitMachime.Name = "rbKnitMachime";
            this.rbKnitMachime.Size = new System.Drawing.Size(104, 17);
            this.rbKnitMachime.TabIndex = 1;
            this.rbKnitMachime.TabStop = true;
            this.rbKnitMachime.Text = "Knit Machine No";
            this.rbKnitMachime.UseVisualStyleBackColor = true;
            // 
            // rbGreigeProduct
            // 
            this.rbGreigeProduct.AutoSize = true;
            this.rbGreigeProduct.Location = new System.Drawing.Point(20, 99);
            this.rbGreigeProduct.Name = "rbGreigeProduct";
            this.rbGreigeProduct.Size = new System.Drawing.Size(96, 17);
            this.rbGreigeProduct.TabIndex = 2;
            this.rbGreigeProduct.TabStop = true;
            this.rbGreigeProduct.Text = "Greige Product";
            this.rbGreigeProduct.UseVisualStyleBackColor = true;
            // 
            // rbProcesdLoss
            // 
            this.rbProcesdLoss.AutoSize = true;
            this.rbProcesdLoss.Location = new System.Drawing.Point(172, 31);
            this.rbProcesdLoss.Name = "rbProcesdLoss";
            this.rbProcesdLoss.Size = new System.Drawing.Size(159, 17);
            this.rbProcesdLoss.TabIndex = 3;
            this.rbProcesdLoss.TabStop = true;
            this.rbProcesdLoss.Text = "Process Loss ( Big To Small)";
            this.rbProcesdLoss.UseVisualStyleBackColor = true;
            // 
            // rbYarnType
            // 
            this.rbYarnType.AutoSize = true;
            this.rbYarnType.Location = new System.Drawing.Point(172, 64);
            this.rbYarnType.Name = "rbYarnType";
            this.rbYarnType.Size = new System.Drawing.Size(74, 17);
            this.rbYarnType.TabIndex = 4;
            this.rbYarnType.TabStop = true;
            this.rbYarnType.Text = "Yarn Type";
            this.rbYarnType.UseVisualStyleBackColor = true;
            // 
            // rbYarnTex
            // 
            this.rbYarnTex.AutoSize = true;
            this.rbYarnTex.Location = new System.Drawing.Point(172, 99);
            this.rbYarnTex.Name = "rbYarnTex";
            this.rbYarnTex.Size = new System.Drawing.Size(68, 17);
            this.rbYarnTex.TabIndex = 5;
            this.rbYarnTex.TabStop = true;
            this.rbYarnTex.Text = "Yarn Tex";
            this.rbYarnTex.UseVisualStyleBackColor = true;
            // 
            // frmProcessLossDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 330);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmProcessLossDS";
            this.Text = "Knit Orders process loss by date";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbYarnTex;
        private System.Windows.Forms.RadioButton rbYarnType;
        private System.Windows.Forms.RadioButton rbProcesdLoss;
        private System.Windows.Forms.RadioButton rbGreigeProduct;
        private System.Windows.Forms.RadioButton rbKnitMachime;
        private System.Windows.Forms.RadioButton rbKnitOrder;
    }
}