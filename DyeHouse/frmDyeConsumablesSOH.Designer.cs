
namespace DyeHouse
{
    partial class frmDyeConsumablesSOH
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
            this.cmboDyeConsumables = new DyeHouse.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkDetail = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmboDyeConsumables
            // 
            this.cmboDyeConsumables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboDyeConsumables.FormattingEnabled = true;
            this.cmboDyeConsumables.Location = new System.Drawing.Point(336, 88);
            this.cmboDyeConsumables.Name = "cmboDyeConsumables";
            this.cmboDyeConsumables.Size = new System.Drawing.Size(207, 21);
            this.cmboDyeConsumables.TabIndex = 1;
            this.cmboDyeConsumables.Text = "Select Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(676, 402);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit ";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkDetail
            // 
            this.chkDetail.AutoSize = true;
            this.chkDetail.Location = new System.Drawing.Point(336, 176);
            this.chkDetail.Name = "chkDetail";
            this.chkDetail.Size = new System.Drawing.Size(118, 17);
            this.chkDetail.TabIndex = 3;
            this.chkDetail.Text = "Full Detail Required";
            this.chkDetail.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Please select a consumable";
            // 
            // frmDyeConsumablesSOH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDetail);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboDyeConsumables);
            this.Name = "frmDyeConsumablesSOH";
            this.Text = "Dye Consumables Stock On Hand";
            this.Load += new System.EventHandler(this.frmDyeConsumablesSOH_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DyeHouse.CheckComboBox cmboDyeConsumables;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkDetail;
        private System.Windows.Forms.Label label1;
    }
}