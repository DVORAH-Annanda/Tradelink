namespace Knitting
{
    partial class frmGreigeAdjust
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
            this.txtGreigeAdjNo = new System.Windows.Forms.TextBox();
            this.dtpAdjustDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbKnitOrder = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmboOperator = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Greige Ajustment Number";
            // 
            // txtGreigeAdjNo
            // 
            this.txtGreigeAdjNo.Location = new System.Drawing.Point(301, 25);
            this.txtGreigeAdjNo.Name = "txtGreigeAdjNo";
            this.txtGreigeAdjNo.Size = new System.Drawing.Size(100, 20);
            this.txtGreigeAdjNo.TabIndex = 1;
            // 
            // dtpAdjustDate
            // 
            this.dtpAdjustDate.Location = new System.Drawing.Point(301, 65);
            this.dtpAdjustDate.Name = "dtpAdjustDate";
            this.dtpAdjustDate.Size = new System.Drawing.Size(143, 20);
            this.dtpAdjustDate.TabIndex = 2;
            this.dtpAdjustDate.ValueChanged += new System.EventHandler(this.dtpAdjustDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Adjustment Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbKnitOrder
            // 
            this.cmbKnitOrder.FormattingEnabled = true;
            this.cmbKnitOrder.Location = new System.Drawing.Point(301, 105);
            this.cmbKnitOrder.Name = "cmbKnitOrder";
            this.cmbKnitOrder.Size = new System.Drawing.Size(121, 21);
            this.cmbKnitOrder.TabIndex = 4;
            this.cmbKnitOrder.SelectedIndexChanged += new System.EventHandler(this.cmbKnitOrder_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Knit Order";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Approved By";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.Location = new System.Drawing.Point(301, 144);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.Size = new System.Drawing.Size(277, 20);
            this.txtApprovedBy.TabIndex = 7;
            this.txtApprovedBy.TextChanged += new System.EventHandler(this.txt);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(618, 476);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Please select an Operator";
            // 
            // cmboOperator
            // 
            this.cmboOperator.FormattingEnabled = true;
            this.cmboOperator.Location = new System.Drawing.Point(301, 185);
            this.cmboOperator.Name = "cmboOperator";
            this.cmboOperator.Size = new System.Drawing.Size(168, 21);
            this.cmboOperator.TabIndex = 11;
            this.cmboOperator.SelectedIndexChanged += new System.EventHandler(this.cmboOperator_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(180, 228);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(427, 222);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // frmGreigeAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 511);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboOperator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtApprovedBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbKnitOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpAdjustDate);
            this.Controls.Add(this.txtGreigeAdjNo);
            this.Controls.Add(this.label1);
            this.Name = "frmGreigeAdjust";
            this.Text = "Greige Stock Adjustments";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGreigeAdjNo;
        private System.Windows.Forms.DateTimePicker dtpAdjustDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbKnitOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboOperator;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}