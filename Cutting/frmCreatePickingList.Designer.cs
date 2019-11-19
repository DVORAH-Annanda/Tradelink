namespace Cutting
{
    partial class frmCreatePickingList
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmboTowareHouse = new System.Windows.Forms.ComboBox();
            this.cmboFromwarehouse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboStyle = new Cutting.CheckComboBox();
            this.cmboColour = new Cutting.CheckComboBox();
            this.cmboSizes = new Cutting.CheckComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 280);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(656, 221);
            this.dataGridView1.TabIndex = 11;
            // 
            // cmboTowareHouse
            // 
            this.cmboTowareHouse.FormattingEnabled = true;
            this.cmboTowareHouse.Location = new System.Drawing.Point(219, 521);
            this.cmboTowareHouse.Name = "cmboTowareHouse";
            this.cmboTowareHouse.Size = new System.Drawing.Size(230, 21);
            this.cmboTowareHouse.TabIndex = 10;
            // 
            // cmboFromwarehouse
            // 
            this.cmboFromwarehouse.FormattingEnabled = true;
            this.cmboFromwarehouse.Location = new System.Drawing.Point(224, 60);
            this.cmboFromwarehouse.Name = "cmboFromwarehouse";
            this.cmboFromwarehouse.Size = new System.Drawing.Size(230, 21);
            this.cmboFromwarehouse.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "To Store";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Current Store";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Sizes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Colour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Filter By Style";
            // 
            // cmboStyle
            // 
            this.cmboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboStyle.FormattingEnabled = true;
            this.cmboStyle.Location = new System.Drawing.Point(224, 115);
            this.cmboStyle.Name = "cmboStyle";
            this.cmboStyle.Size = new System.Drawing.Size(230, 21);
            this.cmboStyle.TabIndex = 17;
            this.cmboStyle.Text = "Select Options";
            // 
            // cmboColour
            // 
            this.cmboColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboColour.FormattingEnabled = true;
            this.cmboColour.Location = new System.Drawing.Point(219, 165);
            this.cmboColour.Name = "cmboColour";
            this.cmboColour.Size = new System.Drawing.Size(235, 21);
            this.cmboColour.TabIndex = 18;
            this.cmboColour.Text = "Select Options";
            // 
            // cmboSizes
            // 
            this.cmboSizes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboSizes.FormattingEnabled = true;
            this.cmboSizes.Location = new System.Drawing.Point(219, 215);
            this.cmboSizes.Name = "cmboSizes";
            this.cmboSizes.Size = new System.Drawing.Size(235, 21);
            this.cmboSizes.TabIndex = 19;
            this.cmboSizes.Text = "Select Options";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(601, 549);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 20;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmCreatePickingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 602);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmboSizes);
            this.Controls.Add(this.cmboColour);
            this.Controls.Add(this.cmboStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmboTowareHouse);
            this.Controls.Add(this.cmboFromwarehouse);
            this.Name = "frmCreatePickingList";
            this.Text = "Create a Picking List";
            this.Load += new System.EventHandler(this.frmCreatePickingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmboTowareHouse;
        private System.Windows.Forms.ComboBox cmboFromwarehouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private /*System.Windows.Forms.ComboBox*/ Cutting.CheckComboBox cmboStyle;
        private /*System.Windows.Forms.ComboBox*/ Cutting.CheckComboBox cmboColour;
        private /*System.Windows.Forms.ComboBox*/ Cutting.CheckComboBox cmboSizes;
        private System.Windows.Forms.Button btnSubmit;
    }
}