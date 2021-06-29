
namespace CustomerServices
{
    partial class frmContiWorkWearReceipts
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateOfTrans = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboWarehouses = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboStyles = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQtyOf = new System.Windows.Forms.TextBox();
            this.cmboSizes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTransNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmboSuppliers = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTTIOrderNo = new System.Windows.Forms.TextBox();
            this.txtDelNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(685, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date Of Transaction";
            // 
            // dtpDateOfTrans
            // 
            this.dtpDateOfTrans.Location = new System.Drawing.Point(149, 19);
            this.dtpDateOfTrans.Name = "dtpDateOfTrans";
            this.dtpDateOfTrans.Size = new System.Drawing.Size(131, 20);
            this.dtpDateOfTrans.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Receiving Warehouse";
            // 
            // cmboWarehouses
            // 
            this.cmboWarehouses.FormattingEnabled = true;
            this.cmboWarehouses.Location = new System.Drawing.Point(182, 147);
            this.cmboWarehouses.Name = "cmboWarehouses";
            this.cmboWarehouses.Size = new System.Drawing.Size(173, 21);
            this.cmboWarehouses.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Style";
            // 
            // cmboStyles
            // 
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(378, 228);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(173, 21);
            this.cmboStyles.TabIndex = 7;
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Colours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sizes";
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(378, 283);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(173, 21);
            this.cmboColours.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Qty Of ";
            // 
            // txtQtyOf
            // 
            this.txtQtyOf.Location = new System.Drawing.Point(378, 392);
            this.txtQtyOf.Name = "txtQtyOf";
            this.txtQtyOf.Size = new System.Drawing.Size(66, 20);
            this.txtQtyOf.TabIndex = 13;
            // 
            // cmboSizes
            // 
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(378, 342);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(173, 21);
            this.cmboSizes.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtDelNo);
            this.groupBox1.Controls.Add(this.txtTTIOrderNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmboSuppliers);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTransNumber);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmboWarehouses);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDateOfTrans);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(249, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 191);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Transnumber";
            // 
            // txtTransNumber
            // 
            this.txtTransNumber.Location = new System.Drawing.Point(107, 65);
            this.txtTransNumber.Name = "txtTransNumber";
            this.txtTransNumber.ReadOnly = true;
            this.txtTransNumber.Size = new System.Drawing.Size(71, 20);
            this.txtTransNumber.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(232, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Supplier";
            // 
            // cmboSuppliers
            // 
            this.cmboSuppliers.FormattingEnabled = true;
            this.cmboSuppliers.Location = new System.Drawing.Point(296, 60);
            this.cmboSuppliers.Name = "cmboSuppliers";
            this.cmboSuppliers.Size = new System.Drawing.Size(144, 21);
            this.cmboSuppliers.TabIndex = 9;
            this.cmboSuppliers.SelectedIndexChanged += new System.EventHandler(this.cmboSuppliers_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "TTI Order No";
            // 
            // txtTTIOrderNo
            // 
            this.txtTTIOrderNo.Location = new System.Drawing.Point(95, 102);
            this.txtTTIOrderNo.Name = "txtTTIOrderNo";
            this.txtTTIOrderNo.Size = new System.Drawing.Size(133, 20);
            this.txtTTIOrderNo.TabIndex = 11;
            // 
            // txtDelNo
            // 
            this.txtDelNo.Location = new System.Drawing.Point(340, 106);
            this.txtDelNo.Name = "txtDelNo";
            this.txtDelNo.Size = new System.Drawing.Size(100, 20);
            this.txtDelNo.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(235, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Supplier Del No";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(685, 374);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 16;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // frmContiWorkWearReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.txtQtyOf);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Name = "frmContiWorkWearReceipts";
            this.Text = "Conti WorkWear Stock Receipts";
            this.Load += new System.EventHandler(this.frmContiWorkWearReceipts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateOfTrans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboWarehouses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQtyOf;
        private System.Windows.Forms.ComboBox cmboSizes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDelNo;
        private System.Windows.Forms.TextBox txtTTIOrderNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboSuppliers;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTransNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNext;
    }
}