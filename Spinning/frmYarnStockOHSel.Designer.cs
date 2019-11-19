namespace Spinning
{
    partial class frmYarnStockOHSel
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
            this.rbByStore = new System.Windows.Forms.RadioButton();
            this.rbYarnOrder = new System.Windows.Forms.RadioButton();
            this.rbTwistFactor = new System.Windows.Forms.RadioButton();
            this.rbTextCount = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbByStore);
            this.groupBox1.Controls.Add(this.rbYarnOrder);
            this.groupBox1.Controls.Add(this.rbTwistFactor);
            this.groupBox1.Controls.Add(this.rbTextCount);
            this.groupBox1.Location = new System.Drawing.Point(139, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection criteria";
            // 
            // rbByStore
            // 
            this.rbByStore.AutoSize = true;
            this.rbByStore.Location = new System.Drawing.Point(57, 161);
            this.rbByStore.Name = "rbByStore";
            this.rbByStore.Size = new System.Drawing.Size(65, 17);
            this.rbByStore.TabIndex = 5;
            this.rbByStore.TabStop = true;
            this.rbByStore.Text = "By Store";
            this.rbByStore.UseVisualStyleBackColor = true;
            this.rbByStore.CheckedChanged += new System.EventHandler(this.rbByStore_CheckedChanged);
            // 
            // rbYarnOrder
            // 
            this.rbYarnOrder.AutoSize = true;
            this.rbYarnOrder.Location = new System.Drawing.Point(53, 121);
            this.rbYarnOrder.Name = "rbYarnOrder";
            this.rbYarnOrder.Size = new System.Drawing.Size(91, 17);
            this.rbYarnOrder.TabIndex = 4;
            this.rbYarnOrder.TabStop = true;
            this.rbYarnOrder.Text = "By Yarn Order\r\n";
            this.rbYarnOrder.UseVisualStyleBackColor = true;
            // 
            // rbTwistFactor
            // 
            this.rbTwistFactor.AutoSize = true;
            this.rbTwistFactor.Location = new System.Drawing.Point(53, 81);
            this.rbTwistFactor.Name = "rbTwistFactor";
            this.rbTwistFactor.Size = new System.Drawing.Size(98, 17);
            this.rbTwistFactor.TabIndex = 2;
            this.rbTwistFactor.TabStop = true;
            this.rbTwistFactor.Text = "By Twist Factor";
            this.rbTwistFactor.UseVisualStyleBackColor = true;
            // 
            // rbTextCount
            // 
            this.rbTextCount.AutoSize = true;
            this.rbTextCount.Location = new System.Drawing.Point(53, 41);
            this.rbTextCount.Name = "rbTextCount";
            this.rbTextCount.Size = new System.Drawing.Size(89, 17);
            this.rbTextCount.TabIndex = 0;
            this.rbTextCount.TabStop = true;
            this.rbTextCount.Text = "By Tex Count";
            this.rbTextCount.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
         
            // 
            // frmYarnStockOHSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 355);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmYarnStockOHSel";
            this.Text = "Yarn Stock On Hand By Store";
         
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbYarnOrder;
        private System.Windows.Forms.RadioButton rbTwistFactor;
        private System.Windows.Forms.RadioButton rbTextCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbByStore;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}