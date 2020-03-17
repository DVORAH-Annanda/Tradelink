namespace ProductionPlanning
{
    partial class frmPlanningKnitStock
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkGradeC = new System.Windows.Forms.CheckBox();
            this.chkGradeB = new System.Windows.Forms.CheckBox();
            this.chkGradeA = new System.Windows.Forms.CheckBox();
            this.cbIncludeWithWarnings = new System.Windows.Forms.CheckBox();
            this.cmboStore = new ProductionPlanning.CheckComboBox();
            this.cmboFabWidth = new ProductionPlanning.CheckComboBox();
            this.cmboFabWeight = new ProductionPlanning.CheckComboBox();
            this.cmboGreigeQuality = new ProductionPlanning.CheckComboBox();
            this.cmboGreige = new ProductionPlanning.CheckComboBox();
            this.chkExcludeDiscontinued = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Fabric Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Fabric Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Greige Quality";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Greige Item";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 516);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Store";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkGradeC);
            this.groupBox1.Controls.Add(this.chkGradeB);
            this.groupBox1.Controls.Add(this.chkGradeA);
            this.groupBox1.Location = new System.Drawing.Point(234, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 125);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // chkGradeC
            // 
            this.chkGradeC.AutoSize = true;
            this.chkGradeC.Location = new System.Drawing.Point(45, 83);
            this.chkGradeC.Name = "chkGradeC";
            this.chkGradeC.Size = new System.Drawing.Size(65, 17);
            this.chkGradeC.TabIndex = 2;
            this.chkGradeC.Text = "Grade C";
            this.chkGradeC.UseVisualStyleBackColor = true;
            // 
            // chkGradeB
            // 
            this.chkGradeB.AutoSize = true;
            this.chkGradeB.Location = new System.Drawing.Point(45, 54);
            this.chkGradeB.Name = "chkGradeB";
            this.chkGradeB.Size = new System.Drawing.Size(65, 17);
            this.chkGradeB.TabIndex = 1;
            this.chkGradeB.Text = "Grade B";
            this.chkGradeB.UseVisualStyleBackColor = true;
            // 
            // chkGradeA
            // 
            this.chkGradeA.AutoSize = true;
            this.chkGradeA.Checked = true;
            this.chkGradeA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGradeA.Location = new System.Drawing.Point(45, 20);
            this.chkGradeA.Name = "chkGradeA";
            this.chkGradeA.Size = new System.Drawing.Size(65, 17);
            this.chkGradeA.TabIndex = 0;
            this.chkGradeA.Text = "Grade A";
            this.chkGradeA.UseVisualStyleBackColor = true;
            // 
            // cbIncludeWithWarnings
            // 
            this.cbIncludeWithWarnings.AutoSize = true;
            this.cbIncludeWithWarnings.Location = new System.Drawing.Point(234, 452);
            this.cbIncludeWithWarnings.Name = "cbIncludeWithWarnings";
            this.cbIncludeWithWarnings.Size = new System.Drawing.Size(173, 17);
            this.cbIncludeWithWarnings.TabIndex = 26;
            this.cbIncludeWithWarnings.Text = "Include Grade A with Warnings";
            this.cbIncludeWithWarnings.UseVisualStyleBackColor = true;
            // 
            // cmboStore
            // 
            this.cmboStore.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStore.FormattingEnabled = true;
            this.cmboStore.Location = new System.Drawing.Point(234, 256);
            this.cmboStore.Name = "cmboStore";
            this.cmboStore.Size = new System.Drawing.Size(179, 21);
            this.cmboStore.TabIndex = 23;
            this.cmboStore.Text = "Select Options";
            this.cmboStore.SelectedIndexChanged += new System.EventHandler(this.cmboStore_SelectedIndexChanged);
            // 
            // cmboFabWidth
            // 
            this.cmboFabWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFabWidth.FormattingEnabled = true;
            this.cmboFabWidth.Location = new System.Drawing.Point(234, 207);
            this.cmboFabWidth.Name = "cmboFabWidth";
            this.cmboFabWidth.Size = new System.Drawing.Size(179, 21);
            this.cmboFabWidth.TabIndex = 16;
            this.cmboFabWidth.Text = "Select Options";
            this.cmboFabWidth.SelectedIndexChanged += new System.EventHandler(this.cmboFabWidth_SelectedIndexChanged);
            // 
            // cmboFabWeight
            // 
            this.cmboFabWeight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboFabWeight.FormattingEnabled = true;
            this.cmboFabWeight.Location = new System.Drawing.Point(234, 158);
            this.cmboFabWeight.Name = "cmboFabWeight";
            this.cmboFabWeight.Size = new System.Drawing.Size(179, 21);
            this.cmboFabWeight.TabIndex = 15;
            this.cmboFabWeight.Text = "Select Options";
            this.cmboFabWeight.SelectedIndexChanged += new System.EventHandler(this.cmboFabWeight_SelectedIndexChanged);
            // 
            // cmboGreigeQuality
            // 
            this.cmboGreigeQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreigeQuality.FormattingEnabled = true;
            this.cmboGreigeQuality.Location = new System.Drawing.Point(234, 109);
            this.cmboGreigeQuality.Name = "cmboGreigeQuality";
            this.cmboGreigeQuality.Size = new System.Drawing.Size(179, 21);
            this.cmboGreigeQuality.TabIndex = 14;
            this.cmboGreigeQuality.Text = "Select Options";
            this.cmboGreigeQuality.SelectedIndexChanged += new System.EventHandler(this.cmboGreigeQuality_SelectedIndexChanged);
            // 
            // cmboGreige
            // 
            this.cmboGreige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(234, 60);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(179, 21);
            this.cmboGreige.TabIndex = 13;
            this.cmboGreige.Text = "Select Options";
            this.cmboGreige.SelectedIndexChanged += new System.EventHandler(this.cmboGreige_SelectedIndexChanged);
            // 
            // chkExcludeDiscontinued
            // 
            this.chkExcludeDiscontinued.AutoSize = true;
            this.chkExcludeDiscontinued.Location = new System.Drawing.Point(234, 493);
            this.chkExcludeDiscontinued.Name = "chkExcludeDiscontinued";
            this.chkExcludeDiscontinued.Size = new System.Drawing.Size(157, 17);
            this.chkExcludeDiscontinued.TabIndex = 27;
            this.chkExcludeDiscontinued.Text = "Exclude Discontinued Items";
            this.chkExcludeDiscontinued.UseVisualStyleBackColor = true;
            // 
            // frmPlanningKnitStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 565);
            this.Controls.Add(this.chkExcludeDiscontinued);
            this.Controls.Add(this.cbIncludeWithWarnings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmboStore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboFabWidth);
            this.Controls.Add(this.cmboFabWeight);
            this.Controls.Add(this.cmboGreigeQuality);
            this.Controls.Add(this.cmboGreige);
            this.Name = "frmPlanningKnitStock";
            this.Text = "Planning Knit Stock Levels and Knit Order";
            this.Load += new System.EventHandler(this.PlanningKnitStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /* private System.Windows.Forms.ComboBox cmboFabWidth;
        private System.Windows.Forms.ComboBox cmboFabWeight;
        private System.Windows.Forms.ComboBox cmboGreigeQuality;
        private System.Windows.Forms.ComboBox cmboGreige; */ 

        private ProductionPlanning.CheckComboBox cmboFabWidth;
        private ProductionPlanning.CheckComboBox cmboFabWeight;
        private ProductionPlanning.CheckComboBox cmboGreigeQuality;
        private ProductionPlanning.CheckComboBox cmboGreige;

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        // private System.Windows.Forms.ComboBox cmboStore;
        private ProductionPlanning.CheckComboBox cmboStore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbIncludeWithWarnings;
        private System.Windows.Forms.CheckBox chkGradeC;
        private System.Windows.Forms.CheckBox chkGradeB;
        private System.Windows.Forms.CheckBox chkGradeA;
        private System.Windows.Forms.CheckBox chkExcludeDiscontinued;
    }
}