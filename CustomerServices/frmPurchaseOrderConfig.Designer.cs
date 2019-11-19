namespace CustomerServices
{
    partial class frmPurchaseOrderConfig
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
            this.dataGridViewx = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewx)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Puchase Order Number";
            // 
            // dataGridViewx
            // 
            this.dataGridViewx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewx.Location = new System.Drawing.Point(66, 153);
            this.dataGridViewx.Name = "dataGridViewx";
            this.dataGridViewx.Size = new System.Drawing.Size(501, 312);
            this.dataGridViewx.TabIndex = 2;
            this.dataGridViewx.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewx_CellValidating);
            this.dataGridViewx.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridViewx.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewx_RowEnter);
            this.dataGridViewx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewx_KeyDown);
            this.dataGridViewx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewx_KeyPress);
            this.dataGridViewx.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewx_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(492, 499);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(254, 33);
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.ReadOnly = true;
            this.txtPONumber.Size = new System.Drawing.Size(212, 20);
            this.txtPONumber.TabIndex = 4;
            this.txtPONumber.Leave += new System.EventHandler(this.txtPONumber_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Style";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(254, 64);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(212, 20);
            this.txtStyle.TabIndex = 7;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(254, 95);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(212, 20);
            this.txtColour.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Colour";
            // 
            // frmPurchaseOrderConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 544);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColour);
            this.Controls.Add(this.txtStyle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPONumber);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridViewx);
            this.Controls.Add(this.label1);
            this.Name = "frmPurchaseOrderConfig";
            this.Text = "Purchase Order Repack Center Configuration";
            this.Load += new System.EventHandler(this.frmPurchaseOrderConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewx;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label label3;
    }
}