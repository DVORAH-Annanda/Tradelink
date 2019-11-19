namespace Cutting
{
    partial class FrmSelectBIF
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboToWareHouse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboWareHouses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnRePrint = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(656, 292);
            this.dataGridView1.TabIndex = 17;
            // 
            // cmboToWareHouse
            // 
            this.cmboToWareHouse.FormattingEnabled = true;
            this.cmboToWareHouse.Location = new System.Drawing.Point(229, 406);
            this.cmboToWareHouse.Name = "cmboToWareHouse";
            this.cmboToWareHouse.Size = new System.Drawing.Size(230, 21);
            this.cmboToWareHouse.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "To WareHouse / Stores";
            // 
            // cmboWareHouses
            // 
            this.cmboWareHouses.FormattingEnabled = true;
            this.cmboWareHouses.Location = new System.Drawing.Point(229, 29);
            this.cmboWareHouses.Name = "cmboWareHouses";
            this.cmboWareHouses.Size = new System.Drawing.Size(230, 21);
            this.cmboWareHouses.TabIndex = 12;
            this.cmboWareHouses.SelectedIndexChanged += new System.EventHandler(this.cmboDepartments_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "WareHouse / Stores";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(593, 433);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnRePrint
            // 
            this.btnRePrint.Location = new System.Drawing.Point(593, 399);
            this.btnRePrint.Name = "btnRePrint";
            this.btnRePrint.Size = new System.Drawing.Size(75, 23);
            this.btnRePrint.TabIndex = 19;
            this.btnRePrint.Text = "Reprint";
            this.btnRePrint.UseVisualStyleBackColor = true;
            this.btnRePrint.Click += new System.EventHandler(this.btnRePrint_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(502, 399);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 20;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FrmSelectBIF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 481);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnRePrint);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboToWareHouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboWareHouses);
            this.Controls.Add(this.label1);
            this.Name = "FrmSelectBIF";
            this.Text = "Bought in Fabric - From Store to Store";
            this.Load += new System.EventHandler(this.FrmSelectBIF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboToWareHouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboWareHouses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnRePrint;
        private System.Windows.Forms.Button btnRemove;
    }
}