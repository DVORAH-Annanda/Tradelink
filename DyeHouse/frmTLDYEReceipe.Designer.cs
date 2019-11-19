namespace DyeHouse
{
    partial class frmTLDYEReceipe
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
            this.cmboProductCodes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProgramLoad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProgramVolume = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboColours = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.rbRemedy = new System.Windows.Forms.RadioButton();
            this.cmboGreigeQuality = new DyeHouse.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Program Codes";
            // 
            // cmboProductCodes
            // 
            this.cmboProductCodes.FormattingEnabled = true;
            this.cmboProductCodes.Location = new System.Drawing.Point(310, 105);
            this.cmboProductCodes.Name = "cmboProductCodes";
            this.cmboProductCodes.Size = new System.Drawing.Size(207, 21);
            this.cmboProductCodes.TabIndex = 1;
            this.cmboProductCodes.SelectedIndexChanged += new System.EventHandler(this.cmboProductCodes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Program Code";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(310, 150);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(207, 20);
            this.txtProductCode.TabIndex = 3;
            this.txtProductCode.TextChanged += new System.EventHandler(this.txt);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(140, 328);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(480, 207);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing_1);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Greige Quality";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(646, 569);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(646, 537);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 10;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(124, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Program Load";
            // 
            // txtProgramLoad
            // 
            this.txtProgramLoad.Location = new System.Drawing.Point(213, 243);
            this.txtProgramLoad.Name = "txtProgramLoad";
            this.txtProgramLoad.Size = new System.Drawing.Size(100, 20);
            this.txtProgramLoad.TabIndex = 12;
            this.txtProgramLoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Program Volume";
            // 
            // txtProgramVolume
            // 
            this.txtProgramVolume.Location = new System.Drawing.Point(444, 243);
            this.txtProgramVolume.Name = "txtProgramVolume";
            this.txtProgramVolume.Size = new System.Drawing.Size(100, 20);
            this.txtProgramVolume.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Colours";
            // 
            // cmboColours
            // 
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(310, 291);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(161, 21);
            this.cmboColours.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRemedy);
            this.groupBox1.Controls.Add(this.rbStandard);
            this.groupBox1.Location = new System.Drawing.Point(213, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 73);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receipe Types";
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Location = new System.Drawing.Point(52, 36);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(68, 17);
            this.rbStandard.TabIndex = 0;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);
            // 
            // rbRemedy
            // 
            this.rbRemedy.AutoSize = true;
            this.rbRemedy.Location = new System.Drawing.Point(210, 36);
            this.rbRemedy.Name = "rbRemedy";
            this.rbRemedy.Size = new System.Drawing.Size(64, 17);
            this.rbRemedy.TabIndex = 1;
            this.rbRemedy.TabStop = true;
            this.rbRemedy.Text = "Remedy\r\n";
            this.rbRemedy.UseVisualStyleBackColor = true;
            this.rbRemedy.CheckedChanged += new System.EventHandler(this.rbRemedy_CheckedChanged);
            // 
            // cmboGreigeQuality
            // 
            this.cmboGreigeQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreigeQuality.FormattingEnabled = true;
            this.cmboGreigeQuality.Location = new System.Drawing.Point(310, 194);
            this.cmboGreigeQuality.Name = "cmboGreigeQuality";
            this.cmboGreigeQuality.Size = new System.Drawing.Size(207, 21);
            this.cmboGreigeQuality.TabIndex = 8;
            this.cmboGreigeQuality.Text = "Select Options";
            this.cmboGreigeQuality.SelectedValueChanged += new System.EventHandler(this.cmboColorList_SelectedValueChanged);
            // 
            // frmTLDYEReceipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 610);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProgramVolume);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtProgramLoad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmboGreigeQuality);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboProductCodes);
            this.Controls.Add(this.label1);
            this.Name = "frmTLDYEReceipe";
            this.Text = "Receipe Definition";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboProductCodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private DyeHouse.CheckComboBox cmboGreigeQuality;
        // private System.Windows.Forms.ComboBox cmboGreigeQuality;
        private System.Windows.Forms.TextBox txtProgramLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProgramVolume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmboColours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRemedy;
        private System.Windows.Forms.RadioButton rbStandard;
    }
}