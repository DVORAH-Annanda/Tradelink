namespace Spinning
{
    partial class frmYarnLabels
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
            this.cmbYarnOrder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMachineNo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYarnDescription = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCottonType = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTwistFactor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTexCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtYarnType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLabels = new System.Windows.Forms.Button();
            this.btnAddPallet = new System.Windows.Forms.Button();
            this.btnDeletePallet = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNoOfCones = new System.Windows.Forms.TextBox();
            this.txtNoOfPallets = new System.Windows.Forms.TextBox();
            this.cmbFromPalletNo = new System.Windows.Forms.ComboBox();
            this.cmbToPalletNo = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a yarn order no";
            // 
            // cmbYarnOrder
            // 
            this.cmbYarnOrder.FormattingEnabled = true;
            this.cmbYarnOrder.Location = new System.Drawing.Point(301, 32);
            this.cmbYarnOrder.Name = "cmbYarnOrder";
            this.cmbYarnOrder.Size = new System.Drawing.Size(121, 21);
            this.cmbYarnOrder.TabIndex = 1;
            this.cmbYarnOrder.SelectedIndexChanged += new System.EventHandler(this.cmbYarnOrder_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pallet Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Machine No";
            // 
            // txtMachineNo
            // 
            this.txtMachineNo.Location = new System.Drawing.Point(301, 96);
            this.txtMachineNo.Name = "txtMachineNo";
            this.txtMachineNo.ReadOnly = true;
            this.txtMachineNo.Size = new System.Drawing.Size(233, 20);
            this.txtMachineNo.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYarnDescription);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtCottonType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtTwistFactor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTexCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtYarnType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(160, 252);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 162);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Order Details";
            // 
            // txtYarnDescription
            // 
            this.txtYarnDescription.Location = new System.Drawing.Point(318, 70);
            this.txtYarnDescription.Name = "txtYarnDescription";
            this.txtYarnDescription.ReadOnly = true;
            this.txtYarnDescription.Size = new System.Drawing.Size(158, 20);
            this.txtYarnDescription.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(221, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Yarn Description";
            // 
            // txtCottonType
            // 
            this.txtCottonType.Location = new System.Drawing.Point(141, 116);
            this.txtCottonType.Name = "txtCottonType";
            this.txtCottonType.ReadOnly = true;
            this.txtCottonType.Size = new System.Drawing.Size(218, 20);
            this.txtCottonType.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Cotton Type";
            // 
            // txtTwistFactor
            // 
            this.txtTwistFactor.Location = new System.Drawing.Point(101, 71);
            this.txtTwistFactor.Name = "txtTwistFactor";
            this.txtTwistFactor.ReadOnly = true;
            this.txtTwistFactor.Size = new System.Drawing.Size(100, 20);
            this.txtTwistFactor.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Twist Factor";
            // 
            // txtTexCount
            // 
            this.txtTexCount.Location = new System.Drawing.Point(318, 23);
            this.txtTexCount.Name = "txtTexCount";
            this.txtTexCount.ReadOnly = true;
            this.txtTexCount.Size = new System.Drawing.Size(100, 20);
            this.txtTexCount.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(221, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Tex Count";
            // 
            // txtYarnType
            // 
            this.txtYarnType.Location = new System.Drawing.Point(101, 23);
            this.txtYarnType.Name = "txtYarnType";
            this.txtYarnType.ReadOnly = true;
            this.txtYarnType.Size = new System.Drawing.Size(100, 20);
            this.txtYarnType.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Yarn Type";
            // 
            // btnLabels
            // 
            this.btnLabels.Location = new System.Drawing.Point(610, 434);
            this.btnLabels.Name = "btnLabels";
            this.btnLabels.Size = new System.Drawing.Size(75, 23);
            this.btnLabels.TabIndex = 11;
            this.btnLabels.Text = "Labels";
            this.btnLabels.UseVisualStyleBackColor = true;
            this.btnLabels.Click += new System.EventHandler(this.btnLabels_Click);
            // 
            // btnAddPallet
            // 
            this.btnAddPallet.Location = new System.Drawing.Point(444, 67);
            this.btnAddPallet.Name = "btnAddPallet";
            this.btnAddPallet.Size = new System.Drawing.Size(75, 23);
            this.btnAddPallet.TabIndex = 14;
            this.btnAddPallet.Text = "Add Pallet";
            this.btnAddPallet.UseVisualStyleBackColor = true;
            this.btnAddPallet.Click += new System.EventHandler(this.btnAddPallet_Click);
            // 
            // btnDeletePallet
            // 
            this.btnDeletePallet.Location = new System.Drawing.Point(545, 67);
            this.btnDeletePallet.Name = "btnDeletePallet";
            this.btnDeletePallet.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePallet.TabIndex = 15;
            this.btnDeletePallet.Text = "Delete Pallet";
            this.btnDeletePallet.UseVisualStyleBackColor = true;
            this.btnDeletePallet.Click += new System.EventHandler(this.btnDeletePallet_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(160, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "No of cones";
            // 
            // txtNoOfCones
            // 
            this.txtNoOfCones.Location = new System.Drawing.Point(301, 125);
            this.txtNoOfCones.Name = "txtNoOfCones";
            this.txtNoOfCones.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfCones.TabIndex = 17;
            this.txtNoOfCones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfCones.Leave += new System.EventHandler(this.txtNoOfCones_CellLeave);
            // 
            // txtNoOfPallets
            // 
            this.txtNoOfPallets.Location = new System.Drawing.Point(301, 69);
            this.txtNoOfPallets.Name = "txtNoOfPallets";
            this.txtNoOfPallets.ReadOnly = true;
            this.txtNoOfPallets.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfPallets.TabIndex = 18;
            this.txtNoOfPallets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbFromPalletNo
            // 
            this.cmbFromPalletNo.FormattingEnabled = true;
            this.cmbFromPalletNo.Location = new System.Drawing.Point(301, 166);
            this.cmbFromPalletNo.Name = "cmbFromPalletNo";
            this.cmbFromPalletNo.Size = new System.Drawing.Size(121, 21);
            this.cmbFromPalletNo.TabIndex = 19;
            this.cmbFromPalletNo.SelectedIndexChanged += new System.EventHandler(this.cmbFromPalletNo_SelectedIndexChanged);
            // 
            // cmbToPalletNo
            // 
            this.cmbToPalletNo.FormattingEnabled = true;
            this.cmbToPalletNo.Location = new System.Drawing.Point(301, 209);
            this.cmbToPalletNo.Name = "cmbToPalletNo";
            this.cmbToPalletNo.Size = new System.Drawing.Size(121, 21);
            this.cmbToPalletNo.TabIndex = 20;
            this.cmbToPalletNo.SelectedIndexChanged += new System.EventHandler(this.cmbToPalletNo_SelectedIndexChanged);
            // 
            // frmYarnLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 479);
            this.Controls.Add(this.cmbToPalletNo);
            this.Controls.Add(this.cmbFromPalletNo);
            this.Controls.Add(this.txtNoOfPallets);
            this.Controls.Add(this.txtNoOfCones);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnDeletePallet);
            this.Controls.Add(this.btnAddPallet);
            this.Controls.Add(this.btnLabels);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMachineNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbYarnOrder);
            this.Controls.Add(this.label1);
            this.Name = "frmYarnLabels";
            this.Text = "Label Printing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYarnOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMachineNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCottonType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTwistFactor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTexCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtYarnType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLabels;
        private System.Windows.Forms.Button btnAddPallet;
        private System.Windows.Forms.Button btnDeletePallet;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNoOfCones;
        private System.Windows.Forms.TextBox txtYarnDescription;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNoOfPallets;
        private System.Windows.Forms.ComboBox cmbFromPalletNo;
        private System.Windows.Forms.ComboBox cmbToPalletNo;
    }
}