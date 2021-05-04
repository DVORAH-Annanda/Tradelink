namespace Cutting
{
    partial class frmCutBoxes
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
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRibbing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBinding = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKidsBoxes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdultBoxes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransActionDate = new System.Windows.Forms.DateTimePicker();
            this.rtbAdditional = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(162, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(436, 367);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(255, 26);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(121, 21);
            this.cmboCutSheet.TabIndex = 8;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Cut Sheet";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(672, 646);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRibbing);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBinding);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtKidsBoxes);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAdultBoxes);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(162, 483);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 102);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // txtRibbing
            // 
            this.txtRibbing.Location = new System.Drawing.Point(320, 62);
            this.txtRibbing.Name = "txtRibbing";
            this.txtRibbing.Size = new System.Drawing.Size(83, 20);
            this.txtRibbing.TabIndex = 7;
            this.txtRibbing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Ribbing";
            // 
            // txtBinding
            // 
            this.txtBinding.Location = new System.Drawing.Point(320, 26);
            this.txtBinding.Name = "txtBinding";
            this.txtBinding.Size = new System.Drawing.Size(83, 20);
            this.txtBinding.TabIndex = 5;
            this.txtBinding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(258, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Binding";
            // 
            // txtKidsBoxes
            // 
            this.txtKidsBoxes.Location = new System.Drawing.Point(107, 62);
            this.txtKidsBoxes.Name = "txtKidsBoxes";
            this.txtKidsBoxes.Size = new System.Drawing.Size(71, 20);
            this.txtKidsBoxes.TabIndex = 3;
            this.txtKidsBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Kid Boxes";
            // 
            // txtAdultBoxes
            // 
            this.txtAdultBoxes.Location = new System.Drawing.Point(107, 26);
            this.txtAdultBoxes.Name = "txtAdultBoxes";
            this.txtAdultBoxes.Size = new System.Drawing.Size(71, 20);
            this.txtAdultBoxes.TabIndex = 1;
            this.txtAdultBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Adult Boxes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Date";
            // 
            // dtpTransActionDate
            // 
            this.dtpTransActionDate.Location = new System.Drawing.Point(255, 64);
            this.dtpTransActionDate.Name = "dtpTransActionDate";
            this.dtpTransActionDate.Size = new System.Drawing.Size(121, 20);
            this.dtpTransActionDate.TabIndex = 16;
            // 
            // rtbAdditional
            // 
            this.rtbAdditional.Location = new System.Drawing.Point(193, 591);
            this.rtbAdditional.Name = "rtbAdditional";
            this.rtbAdditional.Size = new System.Drawing.Size(372, 78);
            this.rtbAdditional.TabIndex = 24;
            this.rtbAdditional.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 594);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Additional Notes";
            // 
            // frmCutBoxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 685);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbAdditional);
            this.Controls.Add(this.dtpTransActionDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboCutSheet);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmCutBoxes";
            this.Text = "Boxes Update";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCutBoxes_FormClosing);
            this.Load += new System.EventHandler(this.frmCutBoxes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRibbing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBinding;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKidsBoxes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdultBoxes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransActionDate;
        private System.Windows.Forms.RichTextBox rtbAdditional;
        private System.Windows.Forms.Label label3;
    }
}