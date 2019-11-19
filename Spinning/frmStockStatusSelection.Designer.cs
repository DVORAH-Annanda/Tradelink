namespace Spinning
{
    partial class frmStockStatusSelection
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
            this.rbStockStatMachine = new System.Windows.Forms.RadioButton();
            this.rbStockStatTex = new System.Windows.Forms.RadioButton();
            this.rbStockStatYO = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOrderStatWIP = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStockStatMachine);
            this.groupBox1.Controls.Add(this.rbStockStatTex);
            this.groupBox1.Controls.Add(this.rbStockStatYO);
            this.groupBox1.Location = new System.Drawing.Point(211, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Stock Status By";
            // 
            // rbStockStatMachine
            // 
            this.rbStockStatMachine.AutoSize = true;
            this.rbStockStatMachine.Location = new System.Drawing.Point(50, 97);
            this.rbStockStatMachine.Name = "rbStockStatMachine";
            this.rbStockStatMachine.Size = new System.Drawing.Size(81, 17);
            this.rbStockStatMachine.TabIndex = 2;
            this.rbStockStatMachine.TabStop = true;
            this.rbStockStatMachine.Text = "By Machine";
            this.rbStockStatMachine.UseVisualStyleBackColor = true;
            // 
            // rbStockStatTex
            // 
            this.rbStockStatTex.AutoSize = true;
            this.rbStockStatTex.Location = new System.Drawing.Point(50, 63);
            this.rbStockStatTex.Name = "rbStockStatTex";
            this.rbStockStatTex.Size = new System.Drawing.Size(58, 17);
            this.rbStockStatTex.TabIndex = 1;
            this.rbStockStatTex.TabStop = true;
            this.rbStockStatTex.Text = "By Tex";
            this.rbStockStatTex.UseVisualStyleBackColor = true;
            // 
            // rbStockStatYO
            // 
            this.rbStockStatYO.AutoSize = true;
            this.rbStockStatYO.Location = new System.Drawing.Point(50, 29);
            this.rbStockStatYO.Name = "rbStockStatYO";
            this.rbStockStatYO.Size = new System.Drawing.Size(76, 17);
            this.rbStockStatYO.TabIndex = 0;
            this.rbStockStatYO.TabStop = true;
            this.rbStockStatYO.Text = "Yarn Order";
            this.rbStockStatYO.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOrderStatWIP);
            this.groupBox2.Location = new System.Drawing.Point(211, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Status";
            // 
            // rbOrderStatWIP
            // 
            this.rbOrderStatWIP.AutoSize = true;
            this.rbOrderStatWIP.Location = new System.Drawing.Point(50, 20);
            this.rbOrderStatWIP.Name = "rbOrderStatWIP";
            this.rbOrderStatWIP.Size = new System.Drawing.Size(46, 17);
            this.rbOrderStatWIP.TabIndex = 0;
            this.rbOrderStatWIP.TabStop = true;
            this.rbOrderStatWIP.Text = "WIP";
            this.rbOrderStatWIP.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(604, 460);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpToDate);
            this.groupBox3.Controls.Add(this.dtpFromDate);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(211, 286);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 96);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Date Selection";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(133, 57);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(146, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(133, 28);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(146, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // frmStockStatusSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 495);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmStockStatusSelection";
            this.Text = "Stock Status Reporting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbStockStatMachine;
        private System.Windows.Forms.RadioButton rbStockStatTex;
        private System.Windows.Forms.RadioButton rbStockStatYO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOrderStatWIP;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}