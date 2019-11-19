namespace Knitting
{
    partial class frmGreigeProduction
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
            this.txtKOPieces = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKnitOrderWeight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGreigeProduct = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMachDetails = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtYarnOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKOPieces);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtKnitOrderWeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtGreigeProduct);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMachDetails);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtYarnOrder);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(130, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 267);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Knit Order Details";
            // 
            // txtKOPieces
            // 
            this.txtKOPieces.Location = new System.Drawing.Point(156, 153);
            this.txtKOPieces.Name = "txtKOPieces";
            this.txtKOPieces.Size = new System.Drawing.Size(100, 20);
            this.txtKOPieces.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Knit Order Pieces";
            // 
            // txtKnitOrderWeight
            // 
            this.txtKnitOrderWeight.Location = new System.Drawing.Point(156, 123);
            this.txtKnitOrderWeight.Name = "txtKnitOrderWeight";
            this.txtKnitOrderWeight.Size = new System.Drawing.Size(100, 20);
            this.txtKnitOrderWeight.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Knit Order Weight";
            // 
            // txtGreigeProduct
            // 
            this.txtGreigeProduct.Location = new System.Drawing.Point(156, 93);
            this.txtGreigeProduct.Name = "txtGreigeProduct";
            this.txtGreigeProduct.ReadOnly = true;
            this.txtGreigeProduct.Size = new System.Drawing.Size(100, 20);
            this.txtGreigeProduct.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Greige Product";
            // 
            // txtMachDetails
            // 
            this.txtMachDetails.Location = new System.Drawing.Point(156, 63);
            this.txtMachDetails.Name = "txtMachDetails";
            this.txtMachDetails.ReadOnly = true;
            this.txtMachDetails.Size = new System.Drawing.Size(100, 20);
            this.txtMachDetails.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Knit Machine Details";
            // 
            // txtYarnOrder
            // 
            this.txtYarnOrder.Location = new System.Drawing.Point(156, 33);
            this.txtYarnOrder.Name = "txtYarnOrder";
            this.txtYarnOrder.ReadOnly = true;
            this.txtYarnOrder.Size = new System.Drawing.Size(100, 20);
            this.txtYarnOrder.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Yarn Order Number";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(558, 423);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmGreigeProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 472);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmGreigeProduction";
            this.Text = "Greige Production";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGreigeProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMachDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtYarnOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtKOPieces;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtKnitOrderWeight;
        private System.Windows.Forms.Label label8;
    }
}