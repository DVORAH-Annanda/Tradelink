
namespace Administration
{
    partial class frmNewForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboCustomers = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboCMT = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkImport = new System.Windows.Forms.CheckBox();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.chkIncludeDiscontinued = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(742, 432);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(694, 554);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select a Customer";
            // 
            // cmboCustomers
            // 
            this.cmboCustomers.FormattingEnabled = true;
            this.cmboCustomers.Location = new System.Drawing.Point(145, 22);
            this.cmboCustomers.Name = "cmboCustomers";
            this.cmboCustomers.Size = new System.Drawing.Size(261, 21);
            this.cmboCustomers.TabIndex = 3;
            this.cmboCustomers.SelectedIndexChanged += new System.EventHandler(this.cmboCustomers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboCMT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkImport);
            this.groupBox1.Controls.Add(this.chkExport);
            this.groupBox1.Location = new System.Drawing.Point(27, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // cmboCMT
            // 
            this.cmboCMT.FormattingEnabled = true;
            this.cmboCMT.Location = new System.Drawing.Point(118, 17);
            this.cmboCMT.Name = "cmboCMT";
            this.cmboCMT.Size = new System.Drawing.Size(260, 21);
            this.cmboCMT.TabIndex = 4;
            this.cmboCMT.SelectedIndexChanged += new System.EventHandler(this.cmboCMT_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select a CMT Faciity";
            // 
            // chkImport
            // 
            this.chkImport.AutoSize = true;
            this.chkImport.Enabled = false;
            this.chkImport.Location = new System.Drawing.Point(586, 19);
            this.chkImport.Name = "chkImport";
            this.chkImport.Size = new System.Drawing.Size(136, 17);
            this.chkImport.TabIndex = 1;
            this.chkImport.Text = "Import Data From Excel";
            this.chkImport.UseVisualStyleBackColor = true;
            this.chkImport.CheckedChanged += new System.EventHandler(this.chkImport_CheckedChanged);
            // 
            // chkExport
            // 
            this.chkExport.AutoSize = true;
            this.chkExport.Enabled = false;
            this.chkExport.Location = new System.Drawing.Point(426, 19);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(123, 17);
            this.chkExport.TabIndex = 0;
            this.chkExport.Text = "Export Data to Excel";
            this.chkExport.UseVisualStyleBackColor = true;
            this.chkExport.CheckedChanged += new System.EventHandler(this.chkExport_CheckedChanged);
            // 
            // chkIncludeDiscontinued
            // 
            this.chkIncludeDiscontinued.AutoSize = true;
            this.chkIncludeDiscontinued.Location = new System.Drawing.Point(454, 24);
            this.chkIncludeDiscontinued.Name = "chkIncludeDiscontinued";
            this.chkIncludeDiscontinued.Size = new System.Drawing.Size(157, 17);
            this.chkIncludeDiscontinued.TabIndex = 5;
            this.chkIncludeDiscontinued.Text = "Include Discontinued Styles";
            this.chkIncludeDiscontinued.UseVisualStyleBackColor = true;
            this.chkIncludeDiscontinued.Visible = false;
            this.chkIncludeDiscontinued.CheckedChanged += new System.EventHandler(this.chkIncludeDiscontinued_CheckedChanged);
            // 
            // frmNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 589);
            this.Controls.Add(this.chkIncludeDiscontinued);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboCustomers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmNewForm";
            this.Text = "frmNewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewForm_FormClosing);
            this.Load += new System.EventHandler(this.frmNewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCustomers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmboCMT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkImport;
        private System.Windows.Forms.CheckBox chkExport;
        private System.Windows.Forms.CheckBox chkIncludeDiscontinued;
    }
}