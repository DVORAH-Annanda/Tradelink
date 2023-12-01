namespace CustomerServices
{
    partial class frmStockOnHand
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
            this.dtpDateinStock = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadSplitBoxes = new System.Windows.Forms.RadioButton();
            this.rbBoxesReturned = new System.Windows.Forms.RadioButton();
            this.RBStockAvail = new System.Windows.Forms.RadioButton();
            this.RBStockOH = new System.Windows.Forms.RadioButton();
            this.rbCostingPastel = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDiscontinued = new System.Windows.Forms.RadioButton();
            this.rbOtherGrades = new System.Windows.Forms.RadioButton();
            this.rbGradeA = new System.Windows.Forms.RadioButton();
            this.comboSizes = new CustomerServices.CheckComboBox();
            this.comboColours = new CustomerServices.CheckComboBox();
            this.comboStyles = new CustomerServices.CheckComboBox();
            this.comboWhses = new CustomerServices.CheckComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "As at Date";
            // 
            // dtpDateinStock
            // 
            this.dtpDateinStock.Location = new System.Drawing.Point(209, 22);
            this.dtpDateinStock.Name = "dtpDateinStock";
            this.dtpDateinStock.Size = new System.Drawing.Size(144, 20);
            this.dtpDateinStock.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadSplitBoxes);
            this.groupBox1.Controls.Add(this.rbBoxesReturned);
            this.groupBox1.Controls.Add(this.RBStockAvail);
            this.groupBox1.Controls.Add(this.RBStockOH);
            this.groupBox1.Location = new System.Drawing.Point(209, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 195);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Classification";
            // 
            // RadSplitBoxes
            // 
            this.RadSplitBoxes.AutoSize = true;
            this.RadSplitBoxes.Location = new System.Drawing.Point(31, 130);
            this.RadSplitBoxes.Name = "RadSplitBoxes";
            this.RadSplitBoxes.Size = new System.Drawing.Size(101, 17);
            this.RadSplitBoxes.TabIndex = 3;
            this.RadSplitBoxes.TabStop = true;
            this.RadSplitBoxes.Text = "Split Boxes Only";
            this.RadSplitBoxes.UseVisualStyleBackColor = true;
            // 
            // rbBoxesReturned
            // 
            this.rbBoxesReturned.AutoSize = true;
            this.rbBoxesReturned.Location = new System.Drawing.Point(31, 96);
            this.rbBoxesReturned.Name = "rbBoxesReturned";
            this.rbBoxesReturned.Size = new System.Drawing.Size(101, 17);
            this.rbBoxesReturned.TabIndex = 2;
            this.rbBoxesReturned.TabStop = true;
            this.rbBoxesReturned.Text = "Boxes Returned";
            this.rbBoxesReturned.UseVisualStyleBackColor = true;
            // 
            // RBStockAvail
            // 
            this.RBStockAvail.AutoSize = true;
            this.RBStockAvail.Location = new System.Drawing.Point(31, 62);
            this.RBStockAvail.Name = "RBStockAvail";
            this.RBStockAvail.Size = new System.Drawing.Size(99, 17);
            this.RBStockAvail.TabIndex = 1;
            this.RBStockAvail.Text = "Stock Available";
            this.RBStockAvail.UseVisualStyleBackColor = true;
            // 
            // RBStockOH
            // 
            this.RBStockOH.AutoSize = true;
            this.RBStockOH.Checked = true;
            this.RBStockOH.Location = new System.Drawing.Point(31, 28);
            this.RBStockOH.Name = "RBStockOH";
            this.RBStockOH.Size = new System.Drawing.Size(99, 17);
            this.RBStockOH.TabIndex = 0;
            this.RBStockOH.TabStop = true;
            this.RBStockOH.Text = "Stock On Hand";
            this.RBStockOH.UseVisualStyleBackColor = true;
            // 
            // rbCostingPastel
            // 
            this.rbCostingPastel.AutoSize = true;
            this.rbCostingPastel.Location = new System.Drawing.Point(244, 403);
            this.rbCostingPastel.Name = "rbCostingPastel";
            this.rbCostingPastel.Size = new System.Drawing.Size(98, 17);
            this.rbCostingPastel.TabIndex = 3;
            this.rbCostingPastel.Text = "Costing Colours";
            this.rbCostingPastel.UseVisualStyleBackColor = true;
            this.rbCostingPastel.CheckedChanged += new System.EventHandler(this.rbCostingPastel_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 636);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 433);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Warehouses";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 498);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Styles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 563);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Colours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 628);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sizes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDiscontinued);
            this.groupBox2.Controls.Add(this.rbOtherGrades);
            this.groupBox2.Controls.Add(this.rbGradeA);
            this.groupBox2.Location = new System.Drawing.Point(209, 269);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 128);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grade Status";
            // 
            // rbDiscontinued
            // 
            this.rbDiscontinued.AutoSize = true;
            this.rbDiscontinued.Location = new System.Drawing.Point(35, 96);
            this.rbDiscontinued.Name = "rbDiscontinued";
            this.rbDiscontinued.Size = new System.Drawing.Size(125, 17);
            this.rbDiscontinued.TabIndex = 2;
            this.rbDiscontinued.Text = "Discontinued Colours";
            this.rbDiscontinued.UseVisualStyleBackColor = true;
            this.rbDiscontinued.CheckedChanged += new System.EventHandler(this.rbDiscontinued_CheckedChanged);
            // 
            // rbOtherGrades
            // 
            this.rbOtherGrades.AutoSize = true;
            this.rbOtherGrades.Location = new System.Drawing.Point(35, 63);
            this.rbOtherGrades.Name = "rbOtherGrades";
            this.rbOtherGrades.Size = new System.Drawing.Size(88, 17);
            this.rbOtherGrades.TabIndex = 1;
            this.rbOtherGrades.Text = "Other Grades";
            this.rbOtherGrades.UseVisualStyleBackColor = true;
            this.rbOtherGrades.CheckedChanged += new System.EventHandler(this.rbOtherGrades_CheckedChanged);
            // 
            // rbGradeA
            // 
            this.rbGradeA.AutoSize = true;
            this.rbGradeA.Checked = true;
            this.rbGradeA.Location = new System.Drawing.Point(35, 30);
            this.rbGradeA.Name = "rbGradeA";
            this.rbGradeA.Size = new System.Drawing.Size(64, 17);
            this.rbGradeA.TabIndex = 0;
            this.rbGradeA.TabStop = true;
            this.rbGradeA.Text = "Grade A";
            this.rbGradeA.UseVisualStyleBackColor = true;
            this.rbGradeA.CheckedChanged += new System.EventHandler(this.rbGradeA_CheckedChanged);
            // 
            // comboSizes
            // 
            this.comboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSizes.FormattingEnabled = true;
            this.comboSizes.Location = new System.Drawing.Point(244, 620);
            this.comboSizes.Name = "comboSizes";
            this.comboSizes.Size = new System.Drawing.Size(233, 21);
            this.comboSizes.TabIndex = 7;
            this.comboSizes.Text = "Select Options";
            this.comboSizes.SelectedIndexChanged += new System.EventHandler(this.comboSizes_SelectedIndexChanged);
            // 
            // comboColours
            // 
            this.comboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboColours.FormattingEnabled = true;
            this.comboColours.Location = new System.Drawing.Point(244, 555);
            this.comboColours.Name = "comboColours";
            this.comboColours.Size = new System.Drawing.Size(233, 21);
            this.comboColours.TabIndex = 6;
            this.comboColours.Text = "Select Options";
            this.comboColours.SelectedIndexChanged += new System.EventHandler(this.comboColours_SelectedIndexChanged);
            // 
            // comboStyles
            // 
            this.comboStyles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboStyles.FormattingEnabled = true;
            this.comboStyles.Location = new System.Drawing.Point(244, 490);
            this.comboStyles.Name = "comboStyles";
            this.comboStyles.Size = new System.Drawing.Size(233, 21);
            this.comboStyles.TabIndex = 5;
            this.comboStyles.Text = "Select Options";
            this.comboStyles.SelectedIndexChanged += new System.EventHandler(this.comboStyles_SelectedIndexChanged);
            // 
            // comboWhses
            // 
            this.comboWhses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboWhses.FormattingEnabled = true;
            this.comboWhses.Location = new System.Drawing.Point(244, 425);
            this.comboWhses.Name = "comboWhses";
            this.comboWhses.Size = new System.Drawing.Size(233, 21);
            this.comboWhses.TabIndex = 4;
            this.comboWhses.Text = "Select Options";
            this.comboWhses.SelectedIndexChanged += new System.EventHandler(this.comboWhses_SelectedIndexChanged);
            // 
            // frmStockOnHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 671);
            this.Controls.Add(this.rbCostingPastel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboSizes);
            this.Controls.Add(this.comboColours);
            this.Controls.Add(this.comboStyles);
            this.Controls.Add(this.comboWhses);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpDateinStock);
            this.Controls.Add(this.label1);
            this.Name = "frmStockOnHand";
            this.Text = "Stock Quantities on Hand";
            this.Load += new System.EventHandler(this.frmStockOnHand_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateinStock;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RBStockAvail;
        private System.Windows.Forms.RadioButton RBStockOH;
        private System.Windows.Forms.Button button1;
        private CustomerServices.CheckComboBox comboWhses;
        private CustomerServices.CheckComboBox comboStyles;
        private CustomerServices.CheckComboBox comboColours;
        private CustomerServices.CheckComboBox comboSizes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOtherGrades;
        private System.Windows.Forms.RadioButton rbGradeA;
        private System.Windows.Forms.RadioButton rbBoxesReturned;
        private System.Windows.Forms.RadioButton RadSplitBoxes;
        private System.Windows.Forms.RadioButton rbDiscontinued;
        private System.Windows.Forms.RadioButton rbCostingPastel;
    }
}