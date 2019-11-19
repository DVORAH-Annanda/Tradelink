namespace DyeHouse
{
    partial class frmSelectReceipeConsumable
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDyeHouseProduction = new System.Windows.Forms.RadioButton();
            this.rbChemicalAnalysis = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmboReceipeConsumables = new DyeHouse.CheckComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbDyeHouseCompleted = new System.Windows.Forms.RadioButton();
            this.rbDyeHouseWIP = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Consumables";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(457, 437);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Location = new System.Drawing.Point(99, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 99);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "To Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "From Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(147, 59);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(117, 20);
            this.dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(147, 24);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(114, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDyeHouseProduction);
            this.groupBox2.Controls.Add(this.rbChemicalAnalysis);
            this.groupBox2.Location = new System.Drawing.Point(145, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Selection";
            // 
            // rbDyeHouseProduction
            // 
            this.rbDyeHouseProduction.AutoSize = true;
            this.rbDyeHouseProduction.Location = new System.Drawing.Point(36, 68);
            this.rbDyeHouseProduction.Name = "rbDyeHouseProduction";
            this.rbDyeHouseProduction.Size = new System.Drawing.Size(129, 17);
            this.rbDyeHouseProduction.TabIndex = 1;
            this.rbDyeHouseProduction.TabStop = true;
            this.rbDyeHouseProduction.Text = "DyeHouse Production";
            this.rbDyeHouseProduction.UseVisualStyleBackColor = true;
            this.rbDyeHouseProduction.CheckedChanged += new System.EventHandler(this.rbDyeHouseProduction_CheckedChanged);
            // 
            // rbChemicalAnalysis
            // 
            this.rbChemicalAnalysis.AutoSize = true;
            this.rbChemicalAnalysis.Location = new System.Drawing.Point(36, 32);
            this.rbChemicalAnalysis.Name = "rbChemicalAnalysis";
            this.rbChemicalAnalysis.Size = new System.Drawing.Size(106, 17);
            this.rbChemicalAnalysis.TabIndex = 0;
            this.rbChemicalAnalysis.TabStop = true;
            this.rbChemicalAnalysis.Text = "Receipe Analysis";
            this.rbChemicalAnalysis.UseVisualStyleBackColor = true;
            this.rbChemicalAnalysis.CheckedChanged += new System.EventHandler(this.rbChemicalAnalysis_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmboReceipeConsumables);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(99, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 99);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // cmboReceipeConsumables
            // 
            this.cmboReceipeConsumables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboReceipeConsumables.FormattingEnabled = true;
            this.cmboReceipeConsumables.Location = new System.Drawing.Point(59, 48);
            this.cmboReceipeConsumables.Name = "cmboReceipeConsumables";
            this.cmboReceipeConsumables.Size = new System.Drawing.Size(225, 21);
            this.cmboReceipeConsumables.TabIndex = 0;
            this.cmboReceipeConsumables.Text = "Select Options";
            this.cmboReceipeConsumables.SelectedIndexChanged += new System.EventHandler(this.cmboReceipeConsumables_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbDyeHouseCompleted);
            this.groupBox4.Controls.Add(this.rbDyeHouseWIP);
            this.groupBox4.Location = new System.Drawing.Point(99, 361);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 89);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // rbDyeHouseCompleted
            // 
            this.rbDyeHouseCompleted.AutoSize = true;
            this.rbDyeHouseCompleted.Location = new System.Drawing.Point(176, 40);
            this.rbDyeHouseCompleted.Name = "rbDyeHouseCompleted";
            this.rbDyeHouseCompleted.Size = new System.Drawing.Size(75, 17);
            this.rbDyeHouseCompleted.TabIndex = 1;
            this.rbDyeHouseCompleted.TabStop = true;
            this.rbDyeHouseCompleted.Text = "Completed";
            this.rbDyeHouseCompleted.UseVisualStyleBackColor = true;
            // 
            // rbDyeHouseWIP
            // 
            this.rbDyeHouseWIP.AutoSize = true;
            this.rbDyeHouseWIP.Location = new System.Drawing.Point(31, 40);
            this.rbDyeHouseWIP.Name = "rbDyeHouseWIP";
            this.rbDyeHouseWIP.Size = new System.Drawing.Size(106, 17);
            this.rbDyeHouseWIP.TabIndex = 0;
            this.rbDyeHouseWIP.TabStop = true;
            this.rbDyeHouseWIP.Text = "Work in Progress";
            this.rbDyeHouseWIP.UseVisualStyleBackColor = true;
            // 
            // frmSelectReceipeConsumable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 483);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "frmSelectReceipeConsumable";
            this.Text = "Select Receipe Consumable ";
            this.Load += new System.EventHandler(this.frmSelectReceipeConsumable_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private /*System.Windows.Forms.ComboBox*/ DyeHouse.CheckComboBox cmboReceipeConsumables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDyeHouseProduction;
        private System.Windows.Forms.RadioButton rbChemicalAnalysis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbDyeHouseCompleted;
        private System.Windows.Forms.RadioButton rbDyeHouseWIP;
    }
}