namespace DyeHouse
{
    partial class frmPieceEnquiry
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
            this.txtPieceNo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRecalc = new System.Windows.Forms.Button();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMeters = new System.Windows.Forms.TextBox();
            this.txtNett = new System.Windows.Forms.TextBox();
            this.txtDisk = new System.Windows.Forms.TextBox();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkClosedCutSheet = new System.Windows.Forms.CheckBox();
            this.chkDBClosed = new System.Windows.Forms.CheckBox();
            this.chkDOClosed = new System.Windows.Forms.CheckBox();
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.txtDyeBatch = new System.Windows.Forms.TextBox();
            this.txtDyeOrder = new System.Windows.Forms.TextBox();
            this.txtKnitOrder = new System.Windows.Forms.TextBox();
            this.txtYarnOrder = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Piece";
            // 
            // txtPieceNo
            // 
            this.txtPieceNo.Location = new System.Drawing.Point(82, 24);
            this.txtPieceNo.Name = "txtPieceNo";
            this.txtPieceNo.Size = new System.Drawing.Size(175, 20);
            this.txtPieceNo.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRecalc);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(48, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 270);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // btnRecalc
            // 
            this.btnRecalc.Location = new System.Drawing.Point(233, 225);
            this.btnRecalc.Name = "btnRecalc";
            this.btnRecalc.Size = new System.Drawing.Size(75, 23);
            this.btnRecalc.TabIndex = 5;
            this.btnRecalc.Text = "button1";
            this.btnRecalc.UseVisualStyleBackColor = true;
            this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(55, 59);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(88, 20);
            this.txtType.TabIndex = 4;
            this.txtType.Text = "Greige";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(55, 27);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.ReadOnly = true;
            this.txtQuality.Size = new System.Drawing.Size(243, 20);
            this.txtQuality.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMeters);
            this.groupBox4.Controls.Add(this.txtNett);
            this.groupBox4.Controls.Add(this.txtDisk);
            this.groupBox4.Controls.Add(this.txtGross);
            this.groupBox4.Controls.Add(this.txtWidth);
            this.groupBox4.Controls.Add(this.txtColour);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(6, 92);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(318, 127);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // txtMeters
            // 
            this.txtMeters.Location = new System.Drawing.Point(214, 84);
            this.txtMeters.Name = "txtMeters";
            this.txtMeters.ReadOnly = true;
            this.txtMeters.Size = new System.Drawing.Size(88, 20);
            this.txtMeters.TabIndex = 8;
            // 
            // txtNett
            // 
            this.txtNett.Location = new System.Drawing.Point(64, 84);
            this.txtNett.Name = "txtNett";
            this.txtNett.ReadOnly = true;
            this.txtNett.Size = new System.Drawing.Size(88, 20);
            this.txtNett.TabIndex = 7;
            // 
            // txtDisk
            // 
            this.txtDisk.Location = new System.Drawing.Point(64, 49);
            this.txtDisk.Name = "txtDisk";
            this.txtDisk.ReadOnly = true;
            this.txtDisk.Size = new System.Drawing.Size(88, 20);
            this.txtDisk.TabIndex = 6;
            // 
            // txtGross
            // 
            this.txtGross.Location = new System.Drawing.Point(214, 49);
            this.txtGross.Name = "txtGross";
            this.txtGross.ReadOnly = true;
            this.txtGross.Size = new System.Drawing.Size(88, 20);
            this.txtGross.TabIndex = 5;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(64, 14);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(88, 20);
            this.txtWidth.TabIndex = 5;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(214, 14);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(88, 20);
            this.txtColour.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Meters";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Disk ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Nett";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Gross";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Colour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quality";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOperator);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.chkClosedCutSheet);
            this.groupBox2.Controls.Add(this.chkDBClosed);
            this.groupBox2.Controls.Add(this.chkDOClosed);
            this.groupBox2.Controls.Add(this.txtCutSheet);
            this.groupBox2.Controls.Add(this.txtDyeBatch);
            this.groupBox2.Controls.Add(this.txtDyeOrder);
            this.groupBox2.Controls.Add(this.txtKnitOrder);
            this.groupBox2.Controls.Add(this.txtYarnOrder);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(403, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 248);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tracking";
            // 
            // chkClosedCutSheet
            // 
            this.chkClosedCutSheet.AutoSize = true;
            this.chkClosedCutSheet.Location = new System.Drawing.Point(216, 215);
            this.chkClosedCutSheet.Name = "chkClosedCutSheet";
            this.chkClosedCutSheet.Size = new System.Drawing.Size(105, 17);
            this.chkClosedCutSheet.TabIndex = 12;
            this.chkClosedCutSheet.Text = "CutSheet Closed";
            this.chkClosedCutSheet.UseVisualStyleBackColor = true;
            // 
            // chkDBClosed
            // 
            this.chkDBClosed.AutoSize = true;
            this.chkDBClosed.Location = new System.Drawing.Point(216, 143);
            this.chkDBClosed.Name = "chkDBClosed";
            this.chkDBClosed.Size = new System.Drawing.Size(89, 17);
            this.chkDBClosed.TabIndex = 11;
            this.chkDBClosed.Text = "Batch Closed";
            this.chkDBClosed.UseVisualStyleBackColor = true;
            this.chkDBClosed.CheckedChanged += new System.EventHandler(this.chkDBClosed_CheckedChanged);
            // 
            // chkDOClosed
            // 
            this.chkDOClosed.AutoSize = true;
            this.chkDOClosed.Location = new System.Drawing.Point(216, 108);
            this.chkDOClosed.Name = "chkDOClosed";
            this.chkDOClosed.Size = new System.Drawing.Size(87, 17);
            this.chkDOClosed.TabIndex = 10;
            this.chkDOClosed.Text = "Order Closed";
            this.chkDOClosed.UseVisualStyleBackColor = true;
            this.chkDOClosed.CheckedChanged += new System.EventHandler(this.chkDOClosed_CheckedChanged);
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(105, 215);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.ReadOnly = true;
            this.txtCutSheet.Size = new System.Drawing.Size(88, 20);
            this.txtCutSheet.TabIndex = 9;
            // 
            // txtDyeBatch
            // 
            this.txtDyeBatch.Location = new System.Drawing.Point(105, 141);
            this.txtDyeBatch.Name = "txtDyeBatch";
            this.txtDyeBatch.ReadOnly = true;
            this.txtDyeBatch.Size = new System.Drawing.Size(88, 20);
            this.txtDyeBatch.TabIndex = 8;
            // 
            // txtDyeOrder
            // 
            this.txtDyeOrder.Location = new System.Drawing.Point(105, 103);
            this.txtDyeOrder.Name = "txtDyeOrder";
            this.txtDyeOrder.ReadOnly = true;
            this.txtDyeOrder.Size = new System.Drawing.Size(88, 20);
            this.txtDyeOrder.TabIndex = 7;
            // 
            // txtKnitOrder
            // 
            this.txtKnitOrder.Location = new System.Drawing.Point(105, 65);
            this.txtKnitOrder.Name = "txtKnitOrder";
            this.txtKnitOrder.ReadOnly = true;
            this.txtKnitOrder.Size = new System.Drawing.Size(88, 20);
            this.txtKnitOrder.TabIndex = 6;
            // 
            // txtYarnOrder
            // 
            this.txtYarnOrder.Location = new System.Drawing.Point(105, 27);
            this.txtYarnOrder.Name = "txtYarnOrder";
            this.txtYarnOrder.ReadOnly = true;
            this.txtYarnOrder.Size = new System.Drawing.Size(88, 20);
            this.txtYarnOrder.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 219);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Cut Sheet";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 141);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Dye Batch";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Dye Order";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Knit Order";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Yarn Order";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(48, 424);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 214);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quality";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(375, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(281, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 179);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Operator";
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(105, 176);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.ReadOnly = true;
            this.txtOperator.Size = new System.Drawing.Size(174, 20);
            this.txtOperator.TabIndex = 14;
            // 
            // frmPieceEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 731);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPieceNo);
            this.Controls.Add(this.label1);
            this.Name = "frmPieceEnquiry";
            this.Text = "Piece Enquiry";
            this.Load += new System.EventHandler(this.frmPieceEnquiry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPieceNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.TextBox txtMeters;
        private System.Windows.Forms.TextBox txtNett;
        private System.Windows.Forms.TextBox txtDisk;
        private System.Windows.Forms.TextBox txtGross;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.TextBox txtDyeBatch;
        private System.Windows.Forms.TextBox txtDyeOrder;
        private System.Windows.Forms.TextBox txtKnitOrder;
        private System.Windows.Forms.TextBox txtYarnOrder;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRecalc;
        private System.Windows.Forms.CheckBox chkDBClosed;
        private System.Windows.Forms.CheckBox chkDOClosed;
        private System.Windows.Forms.CheckBox chkClosedCutSheet;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.Label label15;
    }
}