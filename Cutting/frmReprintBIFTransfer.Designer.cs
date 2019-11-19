namespace Cutting
{
    partial class frmReprintBIFTransfer
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
            this.cmboPickingLists = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPending = new System.Windows.Forms.RadioButton();
            this.rbPast = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmboPickingLists
            // 
            this.cmboPickingLists.FormattingEnabled = true;
            this.cmboPickingLists.Location = new System.Drawing.Point(251, 80);
            this.cmboPickingLists.Name = "cmboPickingLists";
            this.cmboPickingLists.Size = new System.Drawing.Size(233, 21);
            this.cmboPickingLists.TabIndex = 0;
            this.cmboPickingLists.SelectedIndexChanged += new System.EventHandler(this.cmboPickingLists_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Picking List";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(100, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(384, 196);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(489, 413);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPast);
            this.groupBox1.Controls.Add(this.rbPending);
            this.groupBox1.Location = new System.Drawing.Point(123, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Categories";
            // 
            // rbPending
            // 
            this.rbPending.AutoSize = true;
            this.rbPending.Checked = true;
            this.rbPending.Location = new System.Drawing.Point(56, 20);
            this.rbPending.Name = "rbPending";
            this.rbPending.Size = new System.Drawing.Size(64, 17);
            this.rbPending.TabIndex = 0;
            this.rbPending.TabStop = true;
            this.rbPending.Text = "Pending";
            this.rbPending.UseVisualStyleBackColor = true;
            this.rbPending.CheckedChanged += new System.EventHandler(this.rbPending_CheckedChanged);
            // 
            // rbPast
            // 
            this.rbPast.AutoSize = true;
            this.rbPast.Location = new System.Drawing.Point(174, 20);
            this.rbPast.Name = "rbPast";
            this.rbPast.Size = new System.Drawing.Size(46, 17);
            this.rbPast.TabIndex = 1;
            this.rbPast.Text = "Past";
            this.rbPast.UseVisualStyleBackColor = true;
            this.rbPast.CheckedChanged += new System.EventHandler(this.rbPast_CheckedChanged);
            // 
            // frmReprintBIFTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 466);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboPickingLists);
            this.Name = "frmReprintBIFTransfer";
            this.Text = "Reprint Picking List Bought in Fabric Transfers";
            this.Load += new System.EventHandler(this.frmReprintBIFTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboPickingLists;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPast;
        private System.Windows.Forms.RadioButton rbPending;
    }
}