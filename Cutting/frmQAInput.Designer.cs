namespace Cutting
{
    partial class frmQAInput
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
            this.txtBundleDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboMeasureArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInspector = new System.Windows.Forms.TextBox();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radPPS = new System.Windows.Forms.RadioButton();
            this.RadPanelSize = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bundles Description";
            // 
            // txtBundleDescription
            // 
            this.txtBundleDescription.Location = new System.Drawing.Point(248, 25);
            this.txtBundleDescription.Name = "txtBundleDescription";
            this.txtBundleDescription.ReadOnly = true;
            this.txtBundleDescription.Size = new System.Drawing.Size(133, 20);
            this.txtBundleDescription.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(531, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(108, 238);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(449, 150);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // cmboMeasureArea
            // 
            this.cmboMeasureArea.FormattingEnabled = true;
            this.cmboMeasureArea.Location = new System.Drawing.Point(248, 146);
            this.cmboMeasureArea.Name = "cmboMeasureArea";
            this.cmboMeasureArea.Size = new System.Drawing.Size(121, 21);
            this.cmboMeasureArea.TabIndex = 6;
            this.cmboMeasureArea.SelectedIndexChanged += new System.EventHandler(this.cmboMeasureArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Measurement Areas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Inspector";
            // 
            // txtInspector
            // 
            this.txtInspector.Location = new System.Drawing.Point(248, 101);
            this.txtInspector.Name = "txtInspector";
            this.txtInspector.ReadOnly = true;
            this.txtInspector.Size = new System.Drawing.Size(133, 20);
            this.txtInspector.TabIndex = 9;
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(248, 192);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(121, 20);
            this.dtpTransDate.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Size";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(248, 62);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(133, 20);
            this.txtSize.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadPanelSize);
            this.groupBox1.Controls.Add(this.radPPS);
            this.groupBox1.Location = new System.Drawing.Point(406, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 100);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measurement ";
            // 
            // radPPS
            // 
            this.radPPS.AutoSize = true;
            this.radPPS.Location = new System.Drawing.Point(27, 20);
            this.radPPS.Name = "radPPS";
            this.radPPS.Size = new System.Drawing.Size(121, 17);
            this.radPPS.TabIndex = 0;
            this.radPPS.TabStop = true;
            this.radPPS.Text = "Product Spec Sheet";
            this.radPPS.UseVisualStyleBackColor = true;
            this.radPPS.CheckedChanged += new System.EventHandler(this.radPPS_CheckedChanged);
            // 
            // RadPanelSize
            // 
            this.RadPanelSize.AutoSize = true;
            this.RadPanelSize.Location = new System.Drawing.Point(27, 54);
            this.RadPanelSize.Name = "RadPanelSize";
            this.RadPanelSize.Size = new System.Drawing.Size(75, 17);
            this.RadPanelSize.TabIndex = 1;
            this.RadPanelSize.TabStop = true;
            this.RadPanelSize.Text = "Panel Size";
            this.RadPanelSize.UseVisualStyleBackColor = true;
            this.RadPanelSize.CheckedChanged += new System.EventHandler(this.RadPanelSize_CheckedChanged);
            // 
            // frmQAInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 464);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.txtInspector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboMeasureArea);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtBundleDescription);
            this.Controls.Add(this.label1);
            this.Name = "frmQAInput";
            this.Text = "QA Input form";
            this.Load += new System.EventHandler(this.frmQAInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBundleDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboMeasureArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInspector;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RadPanelSize;
        private System.Windows.Forms.RadioButton radPPS;
    }
}