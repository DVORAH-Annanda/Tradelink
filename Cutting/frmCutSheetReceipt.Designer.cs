namespace Cutting
{
    partial class frmCutSheetReceipt
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
            this.cmboCutSheet = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTransDate = new System.Windows.Forms.DateTimePicker();
            this.txtBundles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRibbing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBinding = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKidsBoxes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdultBoxes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTrim2 = new System.Windows.Forms.TextBox();
            this.txtTrim1 = new System.Windows.Forms.TextBox();
            this.txtDyeBatch = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtDateRequired = new System.Windows.Forms.TextBox();
            this.txtDateOrdered = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCurrentTotal = new System.Windows.Forms.TextBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.cmboMachines = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSearch = new System.Windows.Forms.CheckBox();
            this.txtPrevCutSheet = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheet";
            // 
            // cmboCutSheet
            // 
            this.cmboCutSheet.FormattingEnabled = true;
            this.cmboCutSheet.Location = new System.Drawing.Point(72, 12);
            this.cmboCutSheet.Name = "cmboCutSheet";
            this.cmboCutSheet.Size = new System.Drawing.Size(121, 21);
            this.cmboCutSheet.TabIndex = 1;
            this.cmboCutSheet.SelectedIndexChanged += new System.EventHandler(this.cmboCutSheet_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date";
            // 
            // dtpTransDate
            // 
            this.dtpTransDate.Location = new System.Drawing.Point(240, 13);
            this.dtpTransDate.Name = "dtpTransDate";
            this.dtpTransDate.Size = new System.Drawing.Size(132, 20);
            this.dtpTransDate.TabIndex = 3;
            // 
            // txtBundles
            // 
            this.txtBundles.Location = new System.Drawing.Point(442, 11);
            this.txtBundles.Name = "txtBundles";
            this.txtBundles.Size = new System.Drawing.Size(71, 20);
            this.txtBundles.TabIndex = 4;
            this.txtBundles.Leave += new System.EventHandler(this.txtBundles_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bundles";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(313, 293);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
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
            this.groupBox1.Location = new System.Drawing.Point(254, 502);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 102);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // txtRibbing
            // 
            this.txtRibbing.Location = new System.Drawing.Point(270, 62);
            this.txtRibbing.Name = "txtRibbing";
            this.txtRibbing.Size = new System.Drawing.Size(83, 20);
            this.txtRibbing.TabIndex = 7;
            this.txtRibbing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Ribbing";
            // 
            // txtBinding
            // 
            this.txtBinding.Location = new System.Drawing.Point(270, 26);
            this.txtBinding.Name = "txtBinding";
            this.txtBinding.Size = new System.Drawing.Size(83, 20);
            this.txtBinding.TabIndex = 5;
            this.txtBinding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 33);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(789, 581);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTrim2);
            this.groupBox2.Controls.Add(this.txtTrim1);
            this.groupBox2.Controls.Add(this.txtDyeBatch);
            this.groupBox2.Controls.Add(this.txtBody);
            this.groupBox2.Controls.Add(this.txtDateRequired);
            this.groupBox2.Controls.Add(this.txtDateOrdered);
            this.groupBox2.Controls.Add(this.txtColour);
            this.groupBox2.Controls.Add(this.txtCustomer);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(361, 358);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 138);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // txtTrim2
            // 
            this.txtTrim2.Location = new System.Drawing.Point(321, 108);
            this.txtTrim2.Name = "txtTrim2";
            this.txtTrim2.Size = new System.Drawing.Size(146, 20);
            this.txtTrim2.TabIndex = 15;
            // 
            // txtTrim1
            // 
            this.txtTrim1.Location = new System.Drawing.Point(321, 79);
            this.txtTrim1.Name = "txtTrim1";
            this.txtTrim1.Size = new System.Drawing.Size(146, 20);
            this.txtTrim1.TabIndex = 14;
            // 
            // txtDyeBatch
            // 
            this.txtDyeBatch.Location = new System.Drawing.Point(77, 108);
            this.txtDyeBatch.Name = "txtDyeBatch";
            this.txtDyeBatch.Size = new System.Drawing.Size(100, 20);
            this.txtDyeBatch.TabIndex = 13;
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(321, 52);
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(146, 20);
            this.txtBody.TabIndex = 12;
            // 
            // txtDateRequired
            // 
            this.txtDateRequired.Location = new System.Drawing.Point(77, 79);
            this.txtDateRequired.Name = "txtDateRequired";
            this.txtDateRequired.Size = new System.Drawing.Size(100, 20);
            this.txtDateRequired.TabIndex = 11;
            // 
            // txtDateOrdered
            // 
            this.txtDateOrdered.Location = new System.Drawing.Point(77, 52);
            this.txtDateOrdered.Name = "txtDateOrdered";
            this.txtDateOrdered.Size = new System.Drawing.Size(100, 20);
            this.txtDateOrdered.TabIndex = 10;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(321, 26);
            this.txtColour.Name = "txtColour";
            this.txtColour.Size = new System.Drawing.Size(146, 20);
            this.txtColour.TabIndex = 9;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(77, 26);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(148, 20);
            this.txtCustomer.TabIndex = 8;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Dye Batch";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Required";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(279, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Trim 2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(279, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Trim 1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(279, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Body";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Ordered";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(277, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Colour";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Customer";
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(20, 431);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(212, 120);
            this.rtbNotes.TabIndex = 11;
            this.rtbNotes.Text = "";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(394, 75);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(467, 179);
            this.dataGridView2.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(503, 322);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "Current Total";
            // 
            // txtCurrentTotal
            // 
            this.txtCurrentTotal.Location = new System.Drawing.Point(591, 322);
            this.txtCurrentTotal.Name = "txtCurrentTotal";
            this.txtCurrentTotal.ReadOnly = true;
            this.txtCurrentTotal.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentTotal.TabIndex = 18;
            this.txtCurrentTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Location = new System.Drawing.Point(706, 503);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(113, 17);
            this.chkReset.TabIndex = 19;
            this.chkReset.Text = "Reset Transaction";
            this.chkReset.UseVisualStyleBackColor = true;
            this.chkReset.CheckedChanged += new System.EventHandler(this.chkReset_CheckedChanged);
            // 
            // cmboMachines
            // 
            this.cmboMachines.FormattingEnabled = true;
            this.cmboMachines.Location = new System.Drawing.Point(631, 12);
            this.cmboMachines.Name = "cmboMachines";
            this.cmboMachines.Size = new System.Drawing.Size(121, 21);
            this.cmboMachines.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(549, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Machines";
            // 
            // chkSearch
            // 
            this.chkSearch.AutoSize = true;
            this.chkSearch.Location = new System.Drawing.Point(240, 48);
            this.chkSearch.Name = "chkSearch";
            this.chkSearch.Size = new System.Drawing.Size(102, 17);
            this.chkSearch.TabIndex = 22;
            this.chkSearch.Text = "Execute Search";
            this.chkSearch.UseVisualStyleBackColor = true;
            this.chkSearch.CheckedChanged += new System.EventHandler(this.chkSearch_CheckedChanged);
            // 
            // txtPrevCutSheet
            // 
            this.txtPrevCutSheet.Location = new System.Drawing.Point(72, 46);
            this.txtPrevCutSheet.Name = "txtPrevCutSheet";
            this.txtPrevCutSheet.Size = new System.Drawing.Size(121, 20);
            this.txtPrevCutSheet.TabIndex = 23;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 13);
            this.label18.TabIndex = 24;
            this.label18.Text = "Previous";
            // 
            // frmCutSheetReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 631);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtPrevCutSheet);
            this.Controls.Add(this.chkSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboMachines);
            this.Controls.Add(this.chkReset);
            this.Controls.Add(this.txtCurrentTotal);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBundles);
            this.Controls.Add(this.dtpTransDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboCutSheet);
            this.Controls.Add(this.label1);
            this.Name = "frmCutSheetReceipt";
            this.Text = "Receipt into bundle store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCutSheetReceipt_FormClosing);
            this.Load += new System.EventHandler(this.frmCutSheetReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboCutSheet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTransDate;
        private System.Windows.Forms.TextBox txtBundles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRibbing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBinding;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKidsBoxes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdultBoxes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTrim2;
        private System.Windows.Forms.TextBox txtTrim1;
        private System.Windows.Forms.TextBox txtDyeBatch;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.TextBox txtDateRequired;
        private System.Windows.Forms.TextBox txtDateOrdered;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtCurrentTotal;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.ComboBox cmboMachines;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSearch;
        private System.Windows.Forms.TextBox txtPrevCutSheet;
        private System.Windows.Forms.Label label18;
    }
}