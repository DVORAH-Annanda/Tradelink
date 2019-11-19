namespace DyeHouse
{
    partial class frmTransferToDyeHouse
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
            this.dtpTransferDate = new System.Windows.Forms.DateTimePicker();
            this.cmboFabricQuality = new DyeHouse.CheckComboBox();
            this.cmboBatches = new DyeHouse.CheckComboBox();
            this.cmboColour = new DyeHouse.CheckComboBox();
            this.cmboFabricWeight = new DyeHouse.CheckComboBox();
            this.cmboFabricWidth = new DyeHouse.CheckComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date Of Transfer";
            // 
            // dtpTransferDate
            // 
            this.dtpTransferDate.Location = new System.Drawing.Point(309, 29);
            this.dtpTransferDate.Name = "dtpTransferDate";
            this.dtpTransferDate.Size = new System.Drawing.Size(159, 20);
            this.dtpTransferDate.TabIndex = 1;
            // 
            // cmboFabricQuality
            // 
            this.cmboFabricQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFabricQuality.FormattingEnabled = true;
            this.cmboFabricQuality.Location = new System.Drawing.Point(309, 122);
            this.cmboFabricQuality.Name = "cmboFabricQuality";
            this.cmboFabricQuality.Size = new System.Drawing.Size(121, 21);
            this.cmboFabricQuality.TabIndex = 3;
            this.cmboFabricQuality.Text = "Select Options";
            this.cmboFabricQuality.SelectedIndexChanged += new System.EventHandler(this.cmboFabricType_SelectedIndexChanged);
            // 
            // cmboBatches
            // 
            this.cmboBatches.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboBatches.FormattingEnabled = true;
            this.cmboBatches.Location = new System.Drawing.Point(309, 75);
            this.cmboBatches.Name = "cmboBatches";
            this.cmboBatches.Size = new System.Drawing.Size(121, 21);
            this.cmboBatches.TabIndex = 14;
            this.cmboBatches.Text = "Select Options";
            this.cmboBatches.SelectedIndexChanged += new System.EventHandler(this.cmboBatches_SelectedIndexChanged);
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(309, 263);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(121, 21);
            this.cmboColour.TabIndex = 10;
            this.cmboColour.Text = "Select Options";
            this.cmboColour.SelectedIndexChanged += new System.EventHandler(this.cmboColour_SelectedIndexChanged);
            // 
            // cmboFabricWeight
            // 
            this.cmboFabricWeight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFabricWeight.FormattingEnabled = true;
            this.cmboFabricWeight.Location = new System.Drawing.Point(309, 169);
            this.cmboFabricWeight.Name = "cmboFabricWeight";
            this.cmboFabricWeight.Size = new System.Drawing.Size(121, 21);
            this.cmboFabricWeight.TabIndex = 6;
            this.cmboFabricWeight.Text = "Select Options";
            this.cmboFabricWeight.SelectedIndexChanged += new System.EventHandler(this.cmboFabricWeight_SelectedIndexChanged);
            // 
            // cmboFabricWidth
            // 
            this.cmboFabricWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFabricWidth.FormattingEnabled = true;
            this.cmboFabricWidth.Location = new System.Drawing.Point(309, 216);
            this.cmboFabricWidth.Name = "cmboFabricWidth";
            this.cmboFabricWidth.Size = new System.Drawing.Size(121, 21);
            this.cmboFabricWidth.TabIndex = 8;
            this.cmboFabricWidth.Text = "Select Options";
            this.cmboFabricWidth.SelectedIndexChanged += new System.EventHandler(this.cmboFabricWidth_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fabric Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fabric Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fabric Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fabric Colour";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 332);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(639, 150);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(598, 541);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Available Batches";
            // 
            // frmTransferToDyeHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 576);
            this.Controls.Add(this.cmboBatches);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmboFabricWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboFabricWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboFabricQuality);
            this.Controls.Add(this.dtpTransferDate);
            this.Controls.Add(this.label1);
            this.Name = "frmTransferToDyeHouse";
            this.Text = "Transfer of Dye Batches To Dyehouse";
            this.Load += new System.EventHandler(this.frmTransferToDyeHouse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTransferDate;
        
        /*
        private System.Windows.Forms.ComboBox cmboFabricType;
        private System.Windows.Forms.ComboBox cmboFabricWeight;
        private System.Windows.Forms.ComboBox cmboFabricWidth;
        private System.Windows.Forms.ComboBox cmboColour;
        private System.Windows.Forms.ComboBox cmboBatches; */

        private DyeHouse.CheckComboBox cmboFabricQuality;
        private DyeHouse.CheckComboBox cmboFabricWeight;
        private DyeHouse.CheckComboBox cmboFabricWidth;
        private DyeHouse.CheckComboBox cmboColour;
        private DyeHouse.CheckComboBox cmboBatches;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label6;
     
    }
}