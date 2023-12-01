
namespace Knitting
{
    partial class frmWareHouseTransfer
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboFromWareHouse = new System.Windows.Forms.ComboBox();
            this.cmboToWareHouse = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Warehouse";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(662, 397);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Warehouse";
            // 
            // cmboFromWareHouse
            // 
            this.cmboFromWareHouse.FormattingEnabled = true;
            this.cmboFromWareHouse.Location = new System.Drawing.Point(338, 65);
            this.cmboFromWareHouse.Name = "cmboFromWareHouse";
            this.cmboFromWareHouse.Size = new System.Drawing.Size(204, 21);
            this.cmboFromWareHouse.TabIndex = 3;
            this.cmboFromWareHouse.SelectedIndexChanged += new System.EventHandler(this.cmboFromWareHouse_SelectedIndexChanged);
            // 
            // cmboToWareHouse
            // 
            this.cmboToWareHouse.FormattingEnabled = true;
            this.cmboToWareHouse.Location = new System.Drawing.Point(338, 349);
            this.cmboToWareHouse.Name = "cmboToWareHouse";
            this.cmboToWareHouse.Size = new System.Drawing.Size(204, 21);
            this.cmboToWareHouse.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(214, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(426, 214);
            this.dataGridView1.TabIndex = 5;
            // 
            // frmWareHouseTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboToWareHouse);
            this.Controls.Add(this.cmboFromWareHouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Name = "frmWareHouseTransfer";
            this.Text = "WareHouse To WareHouse Transferr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWareHouseTransfer_FormClosing);
            this.Load += new System.EventHandler(this.frmWareHouseTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboFromWareHouse;
        private System.Windows.Forms.ComboBox cmboToWareHouse;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}