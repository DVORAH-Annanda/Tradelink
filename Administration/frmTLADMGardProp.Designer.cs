namespace TTI2_WF
{
    partial class frmTLADMGardProp
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
            this.dataGridViewxx = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.comboColours = new Administration.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewxx)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewxx
            // 
            this.dataGridViewxx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewxx.Location = new System.Drawing.Point(84, 60);
            this.dataGridViewxx.Name = "dataGridViewxx";
            this.dataGridViewxx.Size = new System.Drawing.Size(324, 170);
            this.dataGridViewxx.TabIndex = 0;
            this.dataGridViewxx.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(398, 261);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // comboColours
            // 
            this.comboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColours.FormattingEnabled = true;
            this.comboColours.Location = new System.Drawing.Point(148, 23);
            this.comboColours.Name = "comboColours";
            this.comboColours.Size = new System.Drawing.Size(206, 21);
            this.comboColours.TabIndex = 2;
            this.comboColours.Text = "Select Options";
            this.comboColours.SelectedIndexChanged += new System.EventHandler(this.comboColours_SelectedIndexChanged);
            // 
            // frmTLADMGardProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 296);
            this.Controls.Add(this.comboColours);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridViewxx);
            this.Name = "frmTLADMGardProp";
            this.Text = "frmTLADMGardProp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTLADMGardProp_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewxx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewxx;
        private System.Windows.Forms.Button btnSave;
        // private System.Windows.Forms.ComboBox comboColours;

        private Administration.CheckComboBox comboColours;
    }
}