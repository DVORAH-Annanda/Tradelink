namespace Knitting
{
    partial class frmGreigeRecordOfProd
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
            this.cmboKnitOrders = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpProduction = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGreigeQual = new System.Windows.Forms.TextBox();
            this.cmboDefaultShift = new System.Windows.Forms.ComboBox();
            this.cmboDefaultOperator = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPieces = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNoOfPieces = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPieceNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDskWeight = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please select a Knit Order";
            // 
            // cmboKnitOrders
            // 
            this.cmboKnitOrders.FormattingEnabled = true;
            this.cmboKnitOrders.Location = new System.Drawing.Point(297, 39);
            this.cmboKnitOrders.Name = "cmboKnitOrders";
            this.cmboKnitOrders.Size = new System.Drawing.Size(121, 21);
            this.cmboKnitOrders.TabIndex = 1;
            this.cmboKnitOrders.SelectedIndexChanged += new System.EventHandler(this.cmboKnitOrders_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(101, 294);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(480, 219);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(503, 595);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Please select a production date";
            // 
            // dtpProduction
            // 
            this.dtpProduction.Location = new System.Drawing.Point(297, 78);
            this.dtpProduction.Name = "dtpProduction";
            this.dtpProduction.Size = new System.Drawing.Size(136, 20);
            this.dtpProduction.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Please enter a production start time";
            // 
            // dtpTime
            // 
            this.dtpTime.Location = new System.Drawing.Point(297, 116);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(136, 20);
            this.dtpTime.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 546);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Balance Outstanding";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(300, 539);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(100, 20);
            this.txtBalance.TabIndex = 9;
            this.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 557);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Greige Quality";
            // 
            // txtGreigeQual
            // 
            this.txtGreigeQual.Location = new System.Drawing.Point(297, 148);
            this.txtGreigeQual.Name = "txtGreigeQual";
            this.txtGreigeQual.ReadOnly = true;
            this.txtGreigeQual.Size = new System.Drawing.Size(228, 20);
            this.txtGreigeQual.TabIndex = 12;
            // 
            // cmboDefaultShift
            // 
            this.cmboDefaultShift.FormattingEnabled = true;
            this.cmboDefaultShift.Location = new System.Drawing.Point(181, 184);
            this.cmboDefaultShift.Name = "cmboDefaultShift";
            this.cmboDefaultShift.Size = new System.Drawing.Size(121, 21);
            this.cmboDefaultShift.TabIndex = 4;
            // 
            // cmboDefaultOperator
            // 
            this.cmboDefaultOperator.FormattingEnabled = true;
            this.cmboDefaultOperator.Location = new System.Drawing.Point(431, 184);
            this.cmboDefaultOperator.Name = "cmboDefaultOperator";
            this.cmboDefaultOperator.Size = new System.Drawing.Size(121, 21);
            this.cmboDefaultOperator.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Default Shift";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(332, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Default Operator";
            // 
            // btnPieces
            // 
            this.btnPieces.Location = new System.Drawing.Point(503, 519);
            this.btnPieces.Name = "btnPieces";
            this.btnPieces.Size = new System.Drawing.Size(75, 23);
            this.btnPieces.TabIndex = 10;
            this.btnPieces.Text = "Pieces";
            this.btnPieces.UseVisualStyleBackColor = true;
            this.btnPieces.Click += new System.EventHandler(this.btnPieces_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(361, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "No of Pieces This shift";
            // 
            // txtNoOfPieces
            // 
            this.txtNoOfPieces.Location = new System.Drawing.Point(480, 227);
            this.txtNoOfPieces.Name = "txtNoOfPieces";
            this.txtNoOfPieces.Size = new System.Drawing.Size(72, 20);
            this.txtNoOfPieces.TabIndex = 7;
            this.txtNoOfPieces.Leave += new System.EventHandler(this.txtNoOfPieces_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(95, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Current Starting Piece Number";
            // 
            // txtPieceNo
            // 
            this.txtPieceNo.Location = new System.Drawing.Point(251, 227);
            this.txtPieceNo.Name = "txtPieceNo";
            this.txtPieceNo.Size = new System.Drawing.Size(98, 20);
            this.txtPieceNo.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Shift Dsk Weight";
            // 
            // txtDskWeight
            // 
            this.txtDskWeight.Location = new System.Drawing.Point(269, 268);
            this.txtDskWeight.Name = "txtDskWeight";
            this.txtDskWeight.Size = new System.Drawing.Size(100, 20);
            this.txtDskWeight.TabIndex = 8;
            // 
            // frmGreigeRecordOfProd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 640);
            this.Controls.Add(this.txtDskWeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPieceNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNoOfPieces);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPieces);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmboDefaultOperator);
            this.Controls.Add(this.cmboDefaultShift);
            this.Controls.Add(this.txtGreigeQual);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpProduction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboKnitOrders);
            this.Controls.Add(this.label1);
            this.Name = "frmGreigeRecordOfProd";
            this.Text = "Greige Production Recording";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboKnitOrders;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpProduction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGreigeQual;
        private System.Windows.Forms.ComboBox cmboDefaultShift;
        private System.Windows.Forms.ComboBox cmboDefaultOperator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPieces;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNoOfPieces;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPieceNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDskWeight;
    }
}