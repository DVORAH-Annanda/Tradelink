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
            this.cmboStore = new ProductionPlanning.CheckComboBox();
            this.cmboFabWidth = new ProductionPlanning.CheckComboBox();
            this.cmboFabWeight = new ProductionPlanning.CheckComboBox();
            this.cmboGreigeQuality = new ProductionPlanning.CheckComboBox();
            this.cmboGreige = new ProductionPlanning.CheckComboBox();
            this.cbIncudeWithWarnings = new System.Windows.Forms.CheckBox();
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
            this.button1.Location = new System.Drawing.Point(492, 362);
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
            // cbIncudeWithWarnings
            // 
            this.cbIncudeWithWarnings.AutoSize = true;
            this.cbIncudeWithWarnings.Location = new System.Drawing.Point(234, 318);
            this.cbIncudeWithWarnings.Name = "cbIncudeWithWarnings";
            this.cbIncudeWithWarnings.Size = new System.Drawing.Size(212, 17);
            this.cbIncudeWithWarnings.TabIndex = 24;
            this.cbIncudeWithWarnings.Text = "Include Grade \'A\' Pieces with Warnings";
            this.cbIncudeWithWarnings.UseVisualStyleBackColor = true;
            // 
            // frmPlanningKnitStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 415);
            this.Controls.Add(this.cbIncudeWithWarnings);
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
        private System.Windows.Forms.CheckBox cbIncudeWithWarnings;
    }
}