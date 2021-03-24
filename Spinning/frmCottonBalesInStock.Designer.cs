namespace Spinning
{
    partial class frmCottonBalesInStock
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
            this.button1 = new System.Windows.Forms.Button();
            this.chkSummarised = new System.Windows.Forms.CheckBox();
            this.comboLotNo = new Spinning.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Lot Number";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(423, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkSummarised
            // 
            this.chkSummarised.AutoSize = true;
            this.chkSummarised.Location = new System.Drawing.Point(221, 162);
            this.chkSummarised.Name = "chkSummarised";
            this.chkSummarised.Size = new System.Drawing.Size(118, 17);
            this.chkSummarised.TabIndex = 3;
            this.chkSummarised.Text = "Summarised Format";
            this.chkSummarised.UseVisualStyleBackColor = true;
            // 
            // comboLotNo
            // 
            this.comboLotNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboLotNo.FormattingEnabled = true;
            this.comboLotNo.Location = new System.Drawing.Point(221, 99);
            this.comboLotNo.Name = "comboLotNo";
            this.comboLotNo.Size = new System.Drawing.Size(180, 21);
            this.comboLotNo.TabIndex = 1;
            this.comboLotNo.Text = "Select Options";
            this.comboLotNo.SelectedIndexChanged += new System.EventHandler(this.comboLotNo_SelectedIndexChanged);
            // 
            // frmCottonBalesInStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 349);
            this.Controls.Add(this.chkSummarised);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboLotNo);
            this.Controls.Add(this.label1);
            this.Name = "frmCottonBalesInStock";
            this.Text = "Cotton Bales in Stock by Bale Number";
            this.Load += new System.EventHandler(this.frmCottonBalesInStock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Spinning.CheckComboBox comboLotNo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkSummarised;
    }
}