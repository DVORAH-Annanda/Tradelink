namespace Knitting
{
    partial class frmCloseKnitOrder
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
            this.txtTransNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbKnitOrder = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpKnitOrderClosed = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.txtYarnOrder = new System.Windows.Forms.TextBox();
            this.txtCottonOrigin = new System.Windows.Forms.TextBox();
            this.txtIdentification = new System.Windows.Forms.TextBox();
            this.txtTwist = new System.Windows.Forms.TextBox();
            this.txtTexCount = new System.Windows.Forms.TextBox();
            this.txtYarnType = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTransNum);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbKnitOrder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpKnitOrderClosed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(178, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Details";
            // 
            // txtTransNum
            // 
            this.txtTransNum.Location = new System.Drawing.Point(217, 16);
            this.txtTransNum.Name = "txtTransNum";
            this.txtTransNum.ReadOnly = true;
            this.txtTransNum.Size = new System.Drawing.Size(100, 20);
            this.txtTransNum.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Transaction Number";
            // 
            // cmbKnitOrder
            // 
            this.cmbKnitOrder.FormattingEnabled = true;
            this.cmbKnitOrder.Location = new System.Drawing.Point(217, 90);
            this.cmbKnitOrder.Name = "cmbKnitOrder";
            this.cmbKnitOrder.Size = new System.Drawing.Size(121, 21);
            this.cmbKnitOrder.TabIndex = 6;
            this.cmbKnitOrder.SelectedIndexChanged += new System.EventHandler(this.cmbKnitOrder_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Knit Order No ";
            // 
            // dtpKnitOrderClosed
            // 
            this.dtpKnitOrderClosed.Location = new System.Drawing.Point(217, 53);
            this.dtpKnitOrderClosed.Name = "dtpKnitOrderClosed";
            this.dtpKnitOrderClosed.Size = new System.Drawing.Size(132, 20);
            this.dtpKnitOrderClosed.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date Closed";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMachine);
            this.groupBox2.Controls.Add(this.txtYarnOrder);
            this.groupBox2.Controls.Add(this.txtCottonOrigin);
            this.groupBox2.Controls.Add(this.txtIdentification);
            this.groupBox2.Controls.Add(this.txtTwist);
            this.groupBox2.Controls.Add(this.txtTexCount);
            this.groupBox2.Controls.Add(this.txtYarnType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(139, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(541, 158);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Knit Order Details";
            // 
            // txtMachine
            // 
            this.txtMachine.Location = new System.Drawing.Point(345, 96);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.ReadOnly = true;
            this.txtMachine.Size = new System.Drawing.Size(100, 20);
            this.txtMachine.TabIndex = 13;
            // 
            // txtYarnOrder
            // 
            this.txtYarnOrder.Location = new System.Drawing.Point(91, 100);
            this.txtYarnOrder.Name = "txtYarnOrder";
            this.txtYarnOrder.ReadOnly = true;
            this.txtYarnOrder.Size = new System.Drawing.Size(100, 20);
            this.txtYarnOrder.TabIndex = 12;
            // 
            // txtCottonOrigin
            // 
            this.txtCottonOrigin.Location = new System.Drawing.Point(345, 63);
            this.txtCottonOrigin.Name = "txtCottonOrigin";
            this.txtCottonOrigin.ReadOnly = true;
            this.txtCottonOrigin.Size = new System.Drawing.Size(133, 20);
            this.txtCottonOrigin.TabIndex = 11;
            // 
            // txtIdentification
            // 
            this.txtIdentification.Location = new System.Drawing.Point(91, 63);
            this.txtIdentification.Name = "txtIdentification";
            this.txtIdentification.ReadOnly = true;
            this.txtIdentification.Size = new System.Drawing.Size(100, 20);
            this.txtIdentification.TabIndex = 10;
            // 
            // txtTwist
            // 
            this.txtTwist.Location = new System.Drawing.Point(426, 29);
            this.txtTwist.Name = "txtTwist";
            this.txtTwist.ReadOnly = true;
            this.txtTwist.Size = new System.Drawing.Size(100, 20);
            this.txtTwist.TabIndex = 9;
            // 
            // txtTexCount
            // 
            this.txtTexCount.Location = new System.Drawing.Point(242, 29);
            this.txtTexCount.Name = "txtTexCount";
            this.txtTexCount.ReadOnly = true;
            this.txtTexCount.Size = new System.Drawing.Size(100, 20);
            this.txtTexCount.TabIndex = 8;
            // 
            // txtYarnType
            // 
            this.txtYarnType.Location = new System.Drawing.Point(91, 26);
            this.txtYarnType.Name = "txtYarnType";
            this.txtYarnType.ReadOnly = true;
            this.txtYarnType.Size = new System.Drawing.Size(83, 20);
            this.txtYarnType.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Spin Machine No";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Yarn Order No";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(262, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Cotton Origin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Identification";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Twist";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tex Count";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Yarn Type ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(139, 369);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(541, 150);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(631, 540);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(143, 545);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(194, 542);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(401, 20);
            this.txtNotes.TabIndex = 5;
            // 
            // frmCloseKnitOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 586);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCloseKnitOrder";
            this.Text = "Close Knit Order";
            this.Load += new System.EventHandler(this.frmCloseKnitOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbKnitOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpKnitOrderClosed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMachine;
        private System.Windows.Forms.TextBox txtYarnOrder;
        private System.Windows.Forms.TextBox txtCottonOrigin;
        private System.Windows.Forms.TextBox txtIdentification;
        private System.Windows.Forms.TextBox txtTwist;
        private System.Windows.Forms.TextBox txtTexCount;
        private System.Windows.Forms.TextBox txtYarnType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtTransNum;
        private System.Windows.Forms.Label label2;
    }
}