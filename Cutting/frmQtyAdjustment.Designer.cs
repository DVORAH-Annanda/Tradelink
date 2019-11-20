namespace Cutting
{
    partial class frmQtyAdjustment
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
            this.txtCutSheet = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtRibbing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBinding = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKidsBoxes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdultBoxes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cut Sheet Number";
            // 
            // txtCutSheet
            // 
            this.txtCutSheet.Location = new System.Drawing.Point(201, 43);
            this.txtCutSheet.Name = "txtCutSheet";
            this.txtCutSheet.Size = new System.Drawing.Size(167, 20);
            this.txtCutSheet.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(415, 40);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(428, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(88, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(415, 150);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // txtRibbing
            // 
            this.txtRibbing.Location = new System.Drawing.Point(377, 355);
            this.txtRibbing.Name = "txtRibbing";
            this.txtRibbing.Size = new System.Drawing.Size(83, 20);
            this.txtRibbing.TabIndex = 15;
            this.txtRibbing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(315, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ribbing";
            // 
            // txtBinding
            // 
            this.txtBinding.Location = new System.Drawing.Point(377, 319);
            this.txtBinding.Name = "txtBinding";
            this.txtBinding.Size = new System.Drawing.Size(83, 20);
            this.txtBinding.TabIndex = 13;
            this.txtBinding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(315, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Binding";
            // 
            // txtKidsBoxes
            // 
            this.txtKidsBoxes.Location = new System.Drawing.Point(164, 355);
            this.txtKidsBoxes.Name = "txtKidsBoxes";
            this.txtKidsBoxes.Size = new System.Drawing.Size(71, 20);
            this.txtKidsBoxes.TabIndex = 11;
            this.txtKidsBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 362);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Kid Boxes";
            // 
            // txtAdultBoxes
            // 
            this.txtAdultBoxes.Location = new System.Drawing.Point(164, 319);
            this.txtAdultBoxes.Name = "txtAdultBoxes";
            this.txtAdultBoxes.Size = new System.Drawing.Size(71, 20);
            this.txtAdultBoxes.TabIndex = 9;
            this.txtAdultBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Adult Boxes";
            // 
            // frmQtyAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 507);
            this.Controls.Add(this.txtRibbing);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBinding);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtKidsBoxes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAdultBoxes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtCutSheet);
            this.Controls.Add(this.label1);
            this.Name = "frmQtyAdjustment";
            this.Text = "Bundle Store / Panel Store Qtys Adjustment";
            this.Load += new System.EventHandler(this.frmQtyAdjustment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutSheet;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtRibbing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBinding;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKidsBoxes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdultBoxes;
        private System.Windows.Forms.Label label5;
    }
}