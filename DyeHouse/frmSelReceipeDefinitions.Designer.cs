namespace DyeHouse
{
    partial class frmSelReceipeDefinitions
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
            this.cmboReceipeDefintions = new DyeHouse.CheckComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboColours = new DyeHouse.CheckComboBox();
            this.cmboQualities = new DyeHouse.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Definitions";
            // 
            // cmboReceipeDefintions
            // 
            this.cmboReceipeDefintions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboReceipeDefintions.FormattingEnabled = true;
            this.cmboReceipeDefintions.Location = new System.Drawing.Point(266, 26);
            this.cmboReceipeDefintions.Name = "cmboReceipeDefintions";
            this.cmboReceipeDefintions.Size = new System.Drawing.Size(184, 21);
            this.cmboReceipeDefintions.TabIndex = 1;
            this.cmboReceipeDefintions.Text = "Select Options";
            this.cmboReceipeDefintions.SelectedIndexChanged += new System.EventHandler(this.cmboReceipeDefintions_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Qualities";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current Colours";
            // 
            // cmboColours
            // 
            this.cmboColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColours.FormattingEnabled = true;
            this.cmboColours.Location = new System.Drawing.Point(266, 99);
            this.cmboColours.Name = "cmboColours";
            this.cmboColours.Size = new System.Drawing.Size(184, 21);
            this.cmboColours.TabIndex = 4;
            this.cmboColours.Text = "Select Options";
            this.cmboColours.SelectedIndexChanged += new System.EventHandler(this.cmboColours_SelectedIndexChanged);
            // 
            // cmboQualities
            // 
            this.cmboQualities.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboQualities.FormattingEnabled = true;
            this.cmboQualities.Location = new System.Drawing.Point(266, 172);
            this.cmboQualities.Name = "cmboQualities";
            this.cmboQualities.Size = new System.Drawing.Size(184, 21);
            this.cmboQualities.TabIndex = 5;
            this.cmboQualities.Text = "Select Options";
            this.cmboQualities.SelectedIndexChanged += new System.EventHandler(this.cmboQualities_SelectedIndexChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(576, 340);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmSelReceipeDefinitions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 408);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboQualities);
            this.Controls.Add(this.cmboColours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboReceipeDefintions);
            this.Controls.Add(this.label1);
            this.Name = "frmSelReceipeDefinitions";
            this.Text = "Select Recipe Definitions";
            this.Load += new System.EventHandler(this.frmSelReceipeDefinitions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
       // private System.Windows.Forms.ComboBox cmboReceipeDefintions;
        private CheckComboBox cmboReceipeDefintions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        // private System.Windows.Forms.ComboBox cmboColours;
        private CheckComboBox cmboColours;
        // private System.Windows.Forms.ComboBox cmboQualities;
        private CheckComboBox cmboQualities;
        private System.Windows.Forms.Button btnSubmit;
    }
}