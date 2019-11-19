namespace Knitting
{
    partial class frmYarnAllocatedDirectly
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYarnType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTexCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTwist = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdentification = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCottonOrigin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtYarnOrderNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtYarnReturnNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReceivingKnitOrder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAllocating = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMachine);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtYarnOrderNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCottonOrigin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIdentification);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTwist);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTexCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtYarnType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(112, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yarn Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yarn Type";
            // 
            // txtYarnType
            // 
            this.txtYarnType.Location = new System.Drawing.Point(87, 34);
            this.txtYarnType.Name = "txtYarnType";
            this.txtYarnType.ReadOnly = true;
            this.txtYarnType.Size = new System.Drawing.Size(69, 20);
            this.txtYarnType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tex Count";
            // 
            // txtTexCount
            // 
            this.txtTexCount.Location = new System.Drawing.Point(252, 34);
            this.txtTexCount.Name = "txtTexCount";
            this.txtTexCount.ReadOnly = true;
            this.txtTexCount.Size = new System.Drawing.Size(69, 20);
            this.txtTexCount.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Twist";
            // 
            // txtTwist
            // 
            this.txtTwist.Location = new System.Drawing.Point(415, 34);
            this.txtTwist.Name = "txtTwist";
            this.txtTwist.ReadOnly = true;
            this.txtTwist.Size = new System.Drawing.Size(69, 20);
            this.txtTwist.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Identification";
            // 
            // txtIdentification
            // 
            this.txtIdentification.Location = new System.Drawing.Point(87, 99);
            this.txtIdentification.Name = "txtIdentification";
            this.txtIdentification.ReadOnly = true;
            this.txtIdentification.Size = new System.Drawing.Size(100, 20);
            this.txtIdentification.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cotton Origin";
            // 
            // txtCottonOrigin
            // 
            this.txtCottonOrigin.Location = new System.Drawing.Point(311, 99);
            this.txtCottonOrigin.Name = "txtCottonOrigin";
            this.txtCottonOrigin.ReadOnly = true;
            this.txtCottonOrigin.Size = new System.Drawing.Size(148, 20);
            this.txtCottonOrigin.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Yarn Order No";
            // 
            // txtYarnOrderNo
            // 
            this.txtYarnOrderNo.Location = new System.Drawing.Point(87, 151);
            this.txtYarnOrderNo.Name = "txtYarnOrderNo";
            this.txtYarnOrderNo.ReadOnly = true;
            this.txtYarnOrderNo.Size = new System.Drawing.Size(100, 20);
            this.txtYarnOrderNo.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAllocating);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtReceivingKnitOrder);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtYarnReturnNo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(200, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 134);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General Details";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Yarn Return No";
            // 
            // txtYarnReturnNo
            // 
            this.txtYarnReturnNo.Location = new System.Drawing.Point(164, 22);
            this.txtYarnReturnNo.Name = "txtYarnReturnNo";
            this.txtYarnReturnNo.ReadOnly = true;
            this.txtYarnReturnNo.Size = new System.Drawing.Size(100, 20);
            this.txtYarnReturnNo.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Receiving Knit Order";
            // 
            // txtReceivingKnitOrder
            // 
            this.txtReceivingKnitOrder.Location = new System.Drawing.Point(164, 56);
            this.txtReceivingKnitOrder.Name = "txtReceivingKnitOrder";
            this.txtReceivingKnitOrder.ReadOnly = true;
            this.txtReceivingKnitOrder.Size = new System.Drawing.Size(100, 20);
            this.txtReceivingKnitOrder.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Allocating Order No";
            // 
            // txtAllocating
            // 
            this.txtAllocating.Location = new System.Drawing.Point(164, 90);
            this.txtAllocating.Name = "txtAllocating";
            this.txtAllocating.ReadOnly = true;
            this.txtAllocating.Size = new System.Drawing.Size(100, 20);
            this.txtAllocating.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(175, 377);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(376, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(614, 534);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(204, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Spinning Machine";
            // 
            // txtMachine
            // 
            this.txtMachine.Location = new System.Drawing.Point(311, 151);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.ReadOnly = true;
            this.txtMachine.Size = new System.Drawing.Size(148, 20);
            this.txtMachine.TabIndex = 13;
            // 
            // frmYarnAllocatedDirectly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 569);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmYarnAllocatedDirectly";
            this.Text = "Yarn allocated directly to another knit order";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMachine;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtYarnOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCottonOrigin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIdentification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTwist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTexCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYarnType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAllocating;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReceivingKnitOrder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtYarnReturnNo;
        private System.Windows.Forms.Label label7;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
    }
}