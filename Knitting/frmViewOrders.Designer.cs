namespace Knitting
{
    partial class frmViewOrders
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
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.rbIncudeClosedOrders = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmboMachines = new Knitting.CheckComboBox();
            this.cmboGreige = new Knitting.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // FromDate
            // 
            this.FromDate.Location = new System.Drawing.Point(234, 28);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(129, 20);
            this.FromDate.TabIndex = 0;
            // 
            // ToDate
            // 
            this.ToDate.Location = new System.Drawing.Point(234, 79);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(129, 20);
            this.ToDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(234, 130);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.txtOrderNumber.TabIndex = 6;
            // 
            // rbIncudeClosedOrders
            // 
            this.rbIncudeClosedOrders.AutoSize = true;
            this.rbIncudeClosedOrders.Location = new System.Drawing.Point(234, 285);
            this.rbIncudeClosedOrders.Name = "rbIncudeClosedOrders";
            this.rbIncudeClosedOrders.Size = new System.Drawing.Size(129, 17);
            this.rbIncudeClosedOrders.TabIndex = 7;
            this.rbIncudeClosedOrders.TabStop = true;
            this.rbIncudeClosedOrders.Text = "Include Closed Orders";
            this.rbIncudeClosedOrders.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Order Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Quality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Machine";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(81, 329);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(498, 239);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(526, 611);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmboMachines
            // 
            this.cmboMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboMachines.FormattingEnabled = true;
            this.cmboMachines.Location = new System.Drawing.Point(234, 233);
            this.cmboMachines.Name = "cmboMachines";
            this.cmboMachines.Size = new System.Drawing.Size(235, 21);
            this.cmboMachines.TabIndex = 5;
            this.cmboMachines.Text = "Select Options";
            // 
            // cmboGreige
            // 
            this.cmboGreige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(234, 181);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(235, 21);
            this.cmboGreige.TabIndex = 4;
            this.cmboGreige.Text = "Select Options";
            // 
            // frmViewOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 650);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbIncudeClosedOrders);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.cmboMachines);
            this.Controls.Add(this.cmboGreige);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToDate);
            this.Controls.Add(this.FromDate);
            this.Name = "frmViewOrders";
            this.Text = "View Orders";
            this.Load += new System.EventHandler(this.frmViewOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
       // private System.Windows.Forms.ComboBox cmboGreige;
        private CheckComboBox cmboGreige;
       // private System.Windows.Forms.ComboBox cmboMachines;
        private CheckComboBox cmboMachines;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.RadioButton rbIncudeClosedOrders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSubmit;
    }
}