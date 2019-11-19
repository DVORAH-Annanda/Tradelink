namespace Knitting
{
    partial class frmProductionSplit
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
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReasons = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeightAvailable = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKnitOrder = new System.Windows.Forms.Label();
            this.txtAdjustedWeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radKNumber = new System.Windows.Forms.RadioButton();
            this.radRNumber = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(559, 366);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReasons);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtWeightAvailable);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblKnitOrder);
            this.groupBox1.Controls.Add(this.txtAdjustedWeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radKNumber);
            this.groupBox1.Controls.Add(this.radRNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(114, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 344);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtReasons
            // 
            this.txtReasons.Location = new System.Drawing.Point(96, 251);
            this.txtReasons.Name = "txtReasons";
            this.txtReasons.Size = new System.Drawing.Size(257, 20);
            this.txtReasons.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Notes";
            // 
            // txtWeightAvailable
            // 
            this.txtWeightAvailable.Location = new System.Drawing.Point(211, 156);
            this.txtWeightAvailable.Name = "txtWeightAvailable";
            this.txtWeightAvailable.ReadOnly = true;
            this.txtWeightAvailable.Size = new System.Drawing.Size(142, 20);
            this.txtWeightAvailable.TabIndex = 9;
            this.txtWeightAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Current Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // lblKnitOrder
            // 
            this.lblKnitOrder.AutoSize = true;
            this.lblKnitOrder.Location = new System.Drawing.Point(188, 39);
            this.lblKnitOrder.Name = "lblKnitOrder";
            this.lblKnitOrder.Size = new System.Drawing.Size(54, 13);
            this.lblKnitOrder.TabIndex = 6;
            this.lblKnitOrder.Text = "Knit Order";
            // 
            // txtAdjustedWeight
            // 
            this.txtAdjustedWeight.Location = new System.Drawing.Point(211, 201);
            this.txtAdjustedWeight.Name = "txtAdjustedWeight";
            this.txtAdjustedWeight.Size = new System.Drawing.Size(142, 20);
            this.txtAdjustedWeight.TabIndex = 5;
            this.txtAdjustedWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "New adjusted weight\r\nMust be less than current weight\r\n";
            // 
            // radKNumber
            // 
            this.radKNumber.AutoSize = true;
            this.radKNumber.Location = new System.Drawing.Point(211, 120);
            this.radKNumber.Name = "radKNumber";
            this.radKNumber.Size = new System.Drawing.Size(72, 17);
            this.radKNumber.TabIndex = 3;
            this.radKNumber.TabStop = true;
            this.radKNumber.Text = "K Number";
            this.radKNumber.UseVisualStyleBackColor = true;
            // 
            // radRNumber
            // 
            this.radRNumber.AutoSize = true;
            this.radRNumber.Location = new System.Drawing.Point(38, 119);
            this.radRNumber.Name = "radRNumber";
            this.radRNumber.Size = new System.Drawing.Size(73, 17);
            this.radRNumber.TabIndex = 2;
            this.radRNumber.TabStop = true;
            this.radRNumber.Text = "R Number";
            this.radRNumber.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Quality";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Knit Order";
            // 
            // frmProductionSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 401);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Name = "frmProductionSplit";
            this.Text = "Greige Production Split";
            this.Load += new System.EventHandler(this.frmProductionSplit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKnitOrder;
        private System.Windows.Forms.TextBox txtAdjustedWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radKNumber;
        private System.Windows.Forms.RadioButton radRNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWeightAvailable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReasons;
        private System.Windows.Forms.Label label6;
    }
}