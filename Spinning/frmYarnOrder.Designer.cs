namespace Spinning
{
    partial class frmYarnOrder
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
            this.txtYarnOrder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpYarnOrderDate = new System.Windows.Forms.DateTimePicker();
            this.dtpYarnDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMachineNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOrderWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rtbPacking = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbYarnType = new System.Windows.Forms.ComboBox();
            this.txtTexCount = new System.Windows.Forms.TextBox();
            this.txtTexFactor = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnRecall = new System.Windows.Forms.Button();
            this.txtCottonOrigin = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtConeColour = new System.Windows.Forms.TextBox();
            this.cmbPrevious = new System.Windows.Forms.ComboBox();
            this.cmbMerge = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yarn Order Number";
            // 
            // txtYarnOrder
            // 
            this.txtYarnOrder.Location = new System.Drawing.Point(315, 35);
            this.txtYarnOrder.Name = "txtYarnOrder";
            this.txtYarnOrder.ReadOnly = true;
            this.txtYarnOrder.Size = new System.Drawing.Size(100, 20);
            this.txtYarnOrder.TabIndex = 1;
            this.txtYarnOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Yarn Order Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Yarn Delivery Date";
            // 
            // dtpYarnOrderDate
            // 
            this.dtpYarnOrderDate.Location = new System.Drawing.Point(315, 73);
            this.dtpYarnOrderDate.Name = "dtpYarnOrderDate";
            this.dtpYarnOrderDate.Size = new System.Drawing.Size(200, 20);
            this.dtpYarnOrderDate.TabIndex = 2;
            this.dtpYarnOrderDate.ValueChanged += new System.EventHandler(this.dtpYarnOrderDate_ValueChanged);
            // 
            // dtpYarnDeliveryDate
            // 
            this.dtpYarnDeliveryDate.Location = new System.Drawing.Point(315, 111);
            this.dtpYarnDeliveryDate.Name = "dtpYarnDeliveryDate";
            this.dtpYarnDeliveryDate.Size = new System.Drawing.Size(200, 20);
            this.dtpYarnDeliveryDate.TabIndex = 3;
            this.dtpYarnDeliveryDate.ValueChanged += new System.EventHandler(this.dtpYarnDeliveryDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cotton Merge ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Yarn Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tex Count";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(146, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Twist Factor";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Machine Number";
            // 
            // cmbMachineNo
            // 
            this.cmbMachineNo.FormattingEnabled = true;
            this.cmbMachineNo.Location = new System.Drawing.Point(315, 334);
            this.cmbMachineNo.Name = "cmbMachineNo";
            this.cmbMachineNo.Size = new System.Drawing.Size(249, 21);
            this.cmbMachineNo.TabIndex = 8;
            this.cmbMachineNo.SelectedIndexChanged += new System.EventHandler(this.cmbMachineNo_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 375);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Order Weight (NETT)";
            // 
            // txtOrderWeight
            // 
            this.txtOrderWeight.Location = new System.Drawing.Point(315, 377);
            this.txtOrderWeight.Name = "txtOrderWeight";
            this.txtOrderWeight.Size = new System.Drawing.Size(174, 20);
            this.txtOrderWeight.TabIndex = 9;
            this.txtOrderWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOrderWeight.TextChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(514, 380);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Kg";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(146, 426);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Packing";
            // 
            // rtbPacking
            // 
            this.rtbPacking.Location = new System.Drawing.Point(315, 426);
            this.rtbPacking.Name = "rtbPacking";
            this.rtbPacking.Size = new System.Drawing.Size(174, 54);
            this.rtbPacking.TabIndex = 10;
            this.rtbPacking.Text = "";
            this.rtbPacking.TextChanged += new System.EventHandler(this.rtbPacking_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(681, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbYarnType
            // 
            this.cmbYarnType.FormattingEnabled = true;
            this.cmbYarnType.Location = new System.Drawing.Point(315, 221);
            this.cmbYarnType.Name = "cmbYarnType";
            this.cmbYarnType.Size = new System.Drawing.Size(175, 21);
            this.cmbYarnType.TabIndex = 5;
            this.cmbYarnType.SelectedIndexChanged += new System.EventHandler(this.cmbYarnType_SelectedIndexChanged);
            // 
            // txtTexCount
            // 
            this.txtTexCount.Location = new System.Drawing.Point(315, 258);
            this.txtTexCount.Name = "txtTexCount";
            this.txtTexCount.ReadOnly = true;
            this.txtTexCount.Size = new System.Drawing.Size(100, 20);
            this.txtTexCount.TabIndex = 20;
            this.txtTexCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTexFactor
            // 
            this.txtTexFactor.Location = new System.Drawing.Point(315, 296);
            this.txtTexFactor.Name = "txtTexFactor";
            this.txtTexFactor.ReadOnly = true;
            this.txtTexFactor.Size = new System.Drawing.Size(100, 20);
            this.txtTexFactor.TabIndex = 21;
            this.txtTexFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTexFactor.TextChanged += new System.EventHandler(this.txtTexFactor_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(146, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Cotton Origin";
            // 
            // btnRecall
            // 
            this.btnRecall.Location = new System.Drawing.Point(681, 439);
            this.btnRecall.Name = "btnRecall";
            this.btnRecall.Size = new System.Drawing.Size(75, 23);
            this.btnRecall.TabIndex = 25;
            this.btnRecall.Text = "Edit";
            this.btnRecall.UseVisualStyleBackColor = true;
            this.btnRecall.Click += new System.EventHandler(this.btnRecall_Click);
            // 
            // txtCottonOrigin
            // 
            this.txtCottonOrigin.Location = new System.Drawing.Point(315, 181);
            this.txtCottonOrigin.Name = "txtCottonOrigin";
            this.txtCottonOrigin.ReadOnly = true;
            this.txtCottonOrigin.Size = new System.Drawing.Size(200, 20);
            this.txtCottonOrigin.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(434, 258);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Cone Colour";
            // 
            // txtConeColour
            // 
            this.txtConeColour.Location = new System.Drawing.Point(533, 258);
            this.txtConeColour.Name = "txtConeColour";
            this.txtConeColour.ReadOnly = true;
            this.txtConeColour.Size = new System.Drawing.Size(168, 20);
            this.txtConeColour.TabIndex = 28;
            // 
            // cmbPrevious
            // 
            this.cmbPrevious.FormattingEnabled = true;
            this.cmbPrevious.Location = new System.Drawing.Point(456, 34);
            this.cmbPrevious.Name = "cmbPrevious";
            this.cmbPrevious.Size = new System.Drawing.Size(166, 21);
            this.cmbPrevious.TabIndex = 29;
            this.cmbPrevious.SelectedIndexChanged += new System.EventHandler(this.cmbPrevious_SelectedIndexChanged);
            // 
            // cmbMerge
            // 
            this.cmbMerge.FormattingEnabled = true;
            this.cmbMerge.Location = new System.Drawing.Point(315, 145);
            this.cmbMerge.Name = "cmbMerge";
            this.cmbMerge.Size = new System.Drawing.Size(121, 21);
            this.cmbMerge.TabIndex = 30;
            this.cmbMerge.SelectedIndexChanged += new System.EventHandler(this.cmbMerge_SelectedIndexChanged);
            // 
            // frmYarnOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 510);
            this.Controls.Add(this.cmbMerge);
            this.Controls.Add(this.cmbPrevious);
            this.Controls.Add(this.txtConeColour);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtCottonOrigin);
            this.Controls.Add(this.btnRecall);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtTexFactor);
            this.Controls.Add(this.txtTexCount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rtbPacking);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtOrderWeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbMachineNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbYarnType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpYarnDeliveryDate);
            this.Controls.Add(this.dtpYarnOrderDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYarnOrder);
            this.Controls.Add(this.label1);
            this.Name = "frmYarnOrder";
            this.Text = "Yarn Orders";
            this.Load += new System.EventHandler(this.frmYarnOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYarnOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpYarnOrderDate;
        private System.Windows.Forms.DateTimePicker dtpYarnDeliveryDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMachineNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOrderWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox rtbPacking;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbYarnType;
        private System.Windows.Forms.TextBox txtTexCount;
        private System.Windows.Forms.TextBox txtTexFactor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnRecall;
        private System.Windows.Forms.TextBox txtCottonOrigin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtConeColour;
        private System.Windows.Forms.ComboBox cmbPrevious;
        private System.Windows.Forms.ComboBox cmbMerge;
    }
}