namespace DyeHouse
{
    partial class frmDyeOrderReprint
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGarments = new System.Windows.Forms.RadioButton();
            this.rbFabric = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Enter A Dye  Order Number";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(262, 144);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(213, 20);
            this.txtInput.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(520, 358);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFabric);
            this.groupBox1.Controls.Add(this.rbGarments);
            this.groupBox1.Location = new System.Drawing.Point(262, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // rbGarments
            // 
            this.rbGarments.AutoSize = true;
            this.rbGarments.Location = new System.Drawing.Point(32, 19);
            this.rbGarments.Name = "rbGarments";
            this.rbGarments.Size = new System.Drawing.Size(70, 17);
            this.rbGarments.TabIndex = 0;
            this.rbGarments.TabStop = true;
            this.rbGarments.Text = "Garments";
            this.rbGarments.UseVisualStyleBackColor = true;
            // 
            // rbFabric
            // 
            this.rbFabric.AutoSize = true;
            this.rbFabric.Location = new System.Drawing.Point(32, 66);
            this.rbFabric.Name = "rbFabric";
            this.rbFabric.Size = new System.Drawing.Size(54, 17);
            this.rbFabric.TabIndex = 1;
            this.rbFabric.TabStop = true;
            this.rbFabric.Text = "Fabric";
            this.rbFabric.UseVisualStyleBackColor = true;
            // 
            // frmDyeOrderReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 414);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Name = "frmDyeOrderReprint";
            this.Text = "Dye Order Reprint";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFabric;
        private System.Windows.Forms.RadioButton rbGarments;
    }
}