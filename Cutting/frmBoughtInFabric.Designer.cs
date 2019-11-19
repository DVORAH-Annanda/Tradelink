namespace Cutting
{
    partial class frmBoughtInFabric
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
            this.cmboMachine = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboStore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmboGreige = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TransDate = new System.Windows.Forms.DateTimePicker();
            this.cmboCountry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboColour = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNoOfRolls = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDskWeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDskWidth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMetersPerRoll = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNettWeight = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmboMachine
            // 
            this.cmboMachine.FormattingEnabled = true;
            this.cmboMachine.Location = new System.Drawing.Point(209, 240);
            this.cmboMachine.Name = "cmboMachine";
            this.cmboMachine.Size = new System.Drawing.Size(186, 21);
            this.cmboMachine.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Machine ";
            // 
            // cmboStore
            // 
            this.cmboStore.FormattingEnabled = true;
            this.cmboStore.Location = new System.Drawing.Point(209, 187);
            this.cmboStore.Name = "cmboStore";
            this.cmboStore.Size = new System.Drawing.Size(186, 21);
            this.cmboStore.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Current Store";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 402);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(675, 223);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown_1);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp_1);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(629, 640);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmboGreige
            // 
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(209, 134);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(186, 21);
            this.cmboGreige.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Greige";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Transaction Date";
            // 
            // TransDate
            // 
            this.TransDate.Location = new System.Drawing.Point(209, 82);
            this.TransDate.Name = "TransDate";
            this.TransDate.Size = new System.Drawing.Size(149, 20);
            this.TransDate.TabIndex = 2;
            // 
            // cmboCountry
            // 
            this.cmboCountry.FormattingEnabled = true;
            this.cmboCountry.Location = new System.Drawing.Point(209, 37);
            this.cmboCountry.Name = "cmboCountry";
            this.cmboCountry.Size = new System.Drawing.Size(186, 21);
            this.cmboCountry.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Origin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNettWeight);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtMetersPerRoll);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtDskWidth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDskWeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNoOfRolls);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmboColour);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(61, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 120);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common Details ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Colour ";
            // 
            // cmboColour
            // 
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(89, 20);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(154, 21);
            this.cmboColour.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(310, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "No of Rolls";
            // 
            // txtNoOfRolls
            // 
            this.txtNoOfRolls.Location = new System.Drawing.Point(412, 85);
            this.txtNoOfRolls.Name = "txtNoOfRolls";
            this.txtNoOfRolls.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfRolls.TabIndex = 6;
            this.txtNoOfRolls.Leave += new System.EventHandler(this.txtNoOfRolls_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Dsk Weight";
            // 
            // txtDskWeight
            // 
            this.txtDskWeight.Location = new System.Drawing.Point(89, 85);
            this.txtDskWeight.Name = "txtDskWeight";
            this.txtDskWeight.Size = new System.Drawing.Size(100, 20);
            this.txtDskWeight.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(310, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Dsk Width ";
            // 
            // txtDskWidth
            // 
            this.txtDskWidth.Location = new System.Drawing.Point(412, 21);
            this.txtDskWidth.Name = "txtDskWidth";
            this.txtDskWidth.Size = new System.Drawing.Size(100, 20);
            this.txtDskWidth.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(310, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Meters per roll";
            // 
            // txtMetersPerRoll
            // 
            this.txtMetersPerRoll.Location = new System.Drawing.Point(412, 53);
            this.txtMetersPerRoll.Name = "txtMetersPerRoll";
            this.txtMetersPerRoll.Size = new System.Drawing.Size(100, 20);
            this.txtMetersPerRoll.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Nett Weight";
            // 
            // txtNettWeight
            // 
            this.txtNettWeight.Location = new System.Drawing.Point(89, 53);
            this.txtNettWeight.Name = "txtNettWeight";
            this.txtNettWeight.Size = new System.Drawing.Size(100, 20);
            this.txtNettWeight.TabIndex = 2;
            // 
            // frmBoughtInFabric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 666);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboMachine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboStore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboGreige);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TransDate);
            this.Controls.Add(this.cmboCountry);
            this.Controls.Add(this.label1);
            this.Name = "frmBoughtInFabric";
            this.Text = "frmBoughtInFabric";
            this.Load += new System.EventHandler(this.frmBoughtInFabric_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboMachine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboStore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmboGreige;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker TransDate;
        private System.Windows.Forms.ComboBox cmboCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNoOfRolls;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMetersPerRoll;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDskWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDskWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNettWeight;
        private System.Windows.Forms.Label label11;
    }
}