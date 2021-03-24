
namespace CMT
{
    partial class frmCutSheetReturn
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboCMTs = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbReturnToWIPCutting = new System.Windows.Forms.RadioButton();
            this.rbReturnToPanelStore = new System.Windows.Forms.RadioButton();
            this.cmboWhses = new System.Windows.Forms.ComboBox();
            this.cmboDepartments = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(688, 465);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CMT";
            // 
            // cmboCMTs
            // 
            this.cmboCMTs.FormattingEnabled = true;
            this.cmboCMTs.Location = new System.Drawing.Point(295, 66);
            this.cmboCMTs.Name = "cmboCMTs";
            this.cmboCMTs.Size = new System.Drawing.Size(234, 21);
            this.cmboCMTs.TabIndex = 2;
            this.cmboCMTs.SelectedIndexChanged += new System.EventHandler(this.cmboCMTs_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(176, 124);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(500, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbReturnToWIPCutting);
            this.groupBox1.Controls.Add(this.rbReturnToPanelStore);
            this.groupBox1.Location = new System.Drawing.Point(312, 303);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 96);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // rbReturnToWIPCutting
            // 
            this.rbReturnToWIPCutting.AutoSize = true;
            this.rbReturnToWIPCutting.Location = new System.Drawing.Point(31, 63);
            this.rbReturnToWIPCutting.Name = "rbReturnToWIPCutting";
            this.rbReturnToWIPCutting.Size = new System.Drawing.Size(133, 17);
            this.rbReturnToWIPCutting.TabIndex = 1;
            this.rbReturnToWIPCutting.Text = "Return To WIP Cutting";
            this.rbReturnToWIPCutting.UseVisualStyleBackColor = true;
            this.rbReturnToWIPCutting.CheckedChanged += new System.EventHandler(this.rbReturnToWIPCutting_CheckedChanged);
            // 
            // rbReturnToPanelStore
            // 
            this.rbReturnToPanelStore.AutoSize = true;
            this.rbReturnToPanelStore.Location = new System.Drawing.Point(31, 19);
            this.rbReturnToPanelStore.Name = "rbReturnToPanelStore";
            this.rbReturnToPanelStore.Size = new System.Drawing.Size(131, 17);
            this.rbReturnToPanelStore.TabIndex = 0;
            this.rbReturnToPanelStore.Text = "Return To Panel Store";
            this.rbReturnToPanelStore.UseVisualStyleBackColor = true;
            this.rbReturnToPanelStore.CheckedChanged += new System.EventHandler(this.rbReturnToPanelStore_CheckedChanged);
            // 
            // cmboWhses
            // 
            this.cmboWhses.FormattingEnabled = true;
            this.cmboWhses.Location = new System.Drawing.Point(295, 452);
            this.cmboWhses.Name = "cmboWhses";
            this.cmboWhses.Size = new System.Drawing.Size(234, 21);
            this.cmboWhses.TabIndex = 5;
            // 
            // cmboDepartments
            // 
            this.cmboDepartments.FormattingEnabled = true;
            this.cmboDepartments.Location = new System.Drawing.Point(295, 452);
            this.cmboDepartments.Name = "cmboDepartments";
            this.cmboDepartments.Size = new System.Drawing.Size(234, 21);
            this.cmboDepartments.TabIndex = 6;
            // 
            // frmCutSheetReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.cmboDepartments);
            this.Controls.Add(this.cmboWhses);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboCMTs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSave);
            this.Name = "frmCutSheetReturn";
            this.Text = "Cut Sheet Return Transaction";
            this.Load += new System.EventHandler(this.frmCutSheetReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCMTs;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbReturnToWIPCutting;
        private System.Windows.Forms.RadioButton rbReturnToPanelStore;
        private System.Windows.Forms.ComboBox cmboWhses;
        private System.Windows.Forms.ComboBox cmboDepartments;
    }
}