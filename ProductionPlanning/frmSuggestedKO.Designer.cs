namespace ProductionPlanning
{
    partial class frmSuggestedKO
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmboGreige = new ProductionPlanning.CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboGreigeQuality = new ProductionPlanning.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboMachine = new ProductionPlanning.CheckComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Greige Item";
            // 
            // cmboGreige
            // 
            this.cmboGreige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreige.FormattingEnabled = true;
            this.cmboGreige.Location = new System.Drawing.Point(242, 48);
            this.cmboGreige.Name = "cmboGreige";
            this.cmboGreige.Size = new System.Drawing.Size(179, 21);
            this.cmboGreige.TabIndex = 3;
            this.cmboGreige.Text = "Select Options";
            this.cmboGreige.SelectedIndexChanged += new System.EventHandler(this.cmboGreige_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Greige Quality";
            // 
            // cmboGreigeQuality
            // 
            this.cmboGreigeQuality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboGreigeQuality.FormattingEnabled = true;
            this.cmboGreigeQuality.Location = new System.Drawing.Point(242, 156);
            this.cmboGreigeQuality.Name = "cmboGreigeQuality";
            this.cmboGreigeQuality.Size = new System.Drawing.Size(179, 21);
            this.cmboGreigeQuality.TabIndex = 5;
            this.cmboGreigeQuality.Text = "Select Options";
            this.cmboGreigeQuality.SelectedIndexChanged += new System.EventHandler(this.cmboGreigeQuality_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(613, 263);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Machine Details";
            // 
            // cmboMachine
            // 
            this.cmboMachine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboMachine.FormattingEnabled = true;
            this.cmboMachine.Location = new System.Drawing.Point(242, 102);
            this.cmboMachine.Name = "cmboMachine";
            this.cmboMachine.Size = new System.Drawing.Size(179, 21);
            this.cmboMachine.TabIndex = 16;
            this.cmboMachine.Text = "Select Options";
            this.cmboMachine.SelectedIndexChanged += new System.EventHandler(this.cmboMachine_SelectedIndexChanged);
            // 
            // frmSuggestedKO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 330);
            this.Controls.Add(this.cmboMachine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboGreigeQuality);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboGreige);
            this.Controls.Add(this.label2);
            this.Name = "frmSuggestedKO";
            this.Text = "Suggested Knit Orders and Machine Capacity";
            this.Load += new System.EventHandler(this.frmSuggestedKO_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
      //  private System.Windows.Forms.ComboBox cmboGreige;
        private ProductionPlanning.CheckComboBox cmboGreige;
        private System.Windows.Forms.Label label3;
      //  private System.Windows.Forms.ComboBox cmboGreigeQuality;
        private ProductionPlanning.CheckComboBox cmboGreigeQuality;
        private System.Windows.Forms.Button btnSubmit;
        //  private System.Windows.Forms.ComboBox cmboFabWeight;
        //  private System.Windows.Forms.ComboBox cmboFabWidth; 
        //  private System.Windows.Forms.ComboBox cmboYarn;
        private System.Windows.Forms.Label label4;
      //  private System.Windows.Forms.ComboBox cmboMachine;
        private ProductionPlanning.CheckComboBox cmboMachine; 
    }
}