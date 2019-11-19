namespace DyeHouse
{
    partial class frmStockTake
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
            this.cmboStore = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboStockType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboStockTake = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmboStore
            // 
            this.cmboStore.FormattingEnabled = true;
            this.cmboStore.Location = new System.Drawing.Point(241, 30);
            this.cmboStore.Name = "cmboStore";
            this.cmboStore.Size = new System.Drawing.Size(121, 21);
            this.cmboStore.TabIndex = 0;
            this.cmboStore.SelectedIndexChanged += new System.EventHandler(this.cmboStore_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Store";
            // 
            // cmboStockType
            // 
            this.cmboStockType.FormattingEnabled = true;
            this.cmboStockType.Location = new System.Drawing.Point(241, 81);
            this.cmboStockType.Name = "cmboStockType";
            this.cmboStockType.Size = new System.Drawing.Size(121, 21);
            this.cmboStockType.TabIndex = 2;
            this.cmboStockType.SelectedIndexChanged += new System.EventHandler(this.cmboStore_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stock Type";
            // 
            // cmboStockTake
            // 
            this.cmboStockTake.FormattingEnabled = true;
            this.cmboStockTake.Location = new System.Drawing.Point(241, 134);
            this.cmboStockTake.Name = "cmboStockTake";
            this.cmboStockTake.Size = new System.Drawing.Size(121, 21);
            this.cmboStockTake.TabIndex = 4;
            this.cmboStockTake.SelectedIndexChanged += new System.EventHandler(this.cmboStore_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stock Take Category";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(451, 202);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmStockTake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 262);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboStockTake);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboStockType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboStore);
            this.Name = "frmStockTake";
            this.Text = "Stock Take Seletion Screen";
            this.Load += new System.EventHandler(this.frmStockTake_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboStockType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboStockTake;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSubmit;
    }
}