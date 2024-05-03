
namespace DyeHouse
{
    partial class frmDyeRFD
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
            this.dgvGarmentsAvailable = new System.Windows.Forms.DataGridView();
            this.cmbColours = new System.Windows.Forms.ComboBox();
            this.dtpDateDyed = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelectColour = new System.Windows.Forms.Label();
            this.cmboStyles = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.dgvSizesQuantities = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGarmentsAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizesQuantities)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGarmentsAvailable
            // 
            this.dgvGarmentsAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGarmentsAvailable.Location = new System.Drawing.Point(48, 408);
            this.dgvGarmentsAvailable.Name = "dgvGarmentsAvailable";
            this.dgvGarmentsAvailable.Size = new System.Drawing.Size(580, 150);
            this.dgvGarmentsAvailable.TabIndex = 0;
            this.dgvGarmentsAvailable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGarmentsAvailable_CellContentClick);
            // 
            // cmbColours
            // 
            this.cmbColours.FormattingEnabled = true;
            this.cmbColours.Location = new System.Drawing.Point(181, 171);
            this.cmbColours.Name = "cmbColours";
            this.cmbColours.Size = new System.Drawing.Size(200, 21);
            this.cmbColours.TabIndex = 4;
            // 
            // dtpDateDyed
            // 
            this.dtpDateDyed.Location = new System.Drawing.Point(181, 86);
            this.dtpDateDyed.Name = "dtpDateDyed";
            this.dtpDateDyed.Size = new System.Drawing.Size(200, 20);
            this.dtpDateDyed.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(553, 579);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Boxes";
            // 
            // lblSelectColour
            // 
            this.lblSelectColour.AutoSize = true;
            this.lblSelectColour.Location = new System.Drawing.Point(44, 174);
            this.lblSelectColour.Name = "lblSelectColour";
            this.lblSelectColour.Size = new System.Drawing.Size(70, 13);
            this.lblSelectColour.TabIndex = 4;
            this.lblSelectColour.Text = "Select Colour";
            // 
            // cmboStyles
            // 
            this.cmboStyles.FormattingEnabled = true;
            this.cmboStyles.Location = new System.Drawing.Point(181, 142);
            this.cmboStyles.Name = "cmboStyles";
            this.cmboStyles.Size = new System.Drawing.Size(200, 21);
            this.cmboStyles.TabIndex = 7;
            this.cmboStyles.SelectedIndexChanged += new System.EventHandler(this.cmboStyles_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Select Style";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(530, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Transaction No";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(640, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Completion Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(181, 114);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Location = new System.Drawing.Point(181, 58);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.ReadOnly = true;
            this.txtBatchNumber.Size = new System.Drawing.Size(200, 20);
            this.txtBatchNumber.TabIndex = 0;
            // 
            // dgvSizesQuantities
            // 
            this.dgvSizesQuantities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizesQuantities.Location = new System.Drawing.Point(47, 234);
            this.dgvSizesQuantities.Name = "dgvSizesQuantities";
            this.dgvSizesQuantities.Size = new System.Drawing.Size(581, 150);
            this.dgvSizesQuantities.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sizes and Quantities";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Batch Number";
            // 
            // frmDyeRFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 625);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvSizesQuantities);
            this.Controls.Add(this.txtBatchNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboStyles);
            this.Controls.Add(this.lblSelectColour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpDateDyed);
            this.Controls.Add(this.cmbColours);
            this.Controls.Add(this.dgvGarmentsAvailable);
            this.Name = "frmDyeRFD";
            this.Text = "Create Garment Dyeing Batch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDyeRFD_FormClosing);
            this.Load += new System.EventHandler(this.frmDyeRFD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGarmentsAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizesQuantities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGarmentsAvailable;
        private System.Windows.Forms.ComboBox cmbColours;
        private System.Windows.Forms.DateTimePicker dtpDateDyed;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelectColour;
        private System.Windows.Forms.ComboBox cmboStyles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.DataGridView dgvSizesQuantities;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
    }
}