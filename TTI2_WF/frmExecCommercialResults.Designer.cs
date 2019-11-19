namespace TTI2_WF
{
    partial class frmExecCommercialResults
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
            this.rbSalesByStyle = new System.Windows.Forms.RadioButton();
            this.rbSalesByCustomer = new System.Windows.Forms.RadioButton();
            this.rbPickedNotDelivered = new System.Windows.Forms.RadioButton();
            this.rbOrderAgeing = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbSalesByStyle
            // 
            this.rbSalesByStyle.AutoSize = true;
            this.rbSalesByStyle.Location = new System.Drawing.Point(148, 73);
            this.rbSalesByStyle.Name = "rbSalesByStyle";
            this.rbSalesByStyle.Size = new System.Drawing.Size(92, 17);
            this.rbSalesByStyle.TabIndex = 0;
            this.rbSalesByStyle.TabStop = true;
            this.rbSalesByStyle.Text = "Sales By Style";
            this.rbSalesByStyle.UseVisualStyleBackColor = true;
            this.rbSalesByStyle.CheckedChanged += new System.EventHandler(this.rbSalesByStyle_CheckedChanged);
            // 
            // rbSalesByCustomer
            // 
            this.rbSalesByCustomer.AutoSize = true;
            this.rbSalesByCustomer.Location = new System.Drawing.Point(148, 141);
            this.rbSalesByCustomer.Name = "rbSalesByCustomer";
            this.rbSalesByCustomer.Size = new System.Drawing.Size(113, 17);
            this.rbSalesByCustomer.TabIndex = 1;
            this.rbSalesByCustomer.TabStop = true;
            this.rbSalesByCustomer.Text = "Sales By Customer";
            this.rbSalesByCustomer.UseVisualStyleBackColor = true;
            this.rbSalesByCustomer.CheckedChanged += new System.EventHandler(this.rbSalesByCustomer_CheckedChanged);
            // 
            // rbPickedNotDelivered
            // 
            this.rbPickedNotDelivered.AutoSize = true;
            this.rbPickedNotDelivered.Location = new System.Drawing.Point(148, 209);
            this.rbPickedNotDelivered.Name = "rbPickedNotDelivered";
            this.rbPickedNotDelivered.Size = new System.Drawing.Size(160, 17);
            this.rbPickedNotDelivered.TabIndex = 2;
            this.rbPickedNotDelivered.TabStop = true;
            this.rbPickedNotDelivered.Text = "Orders Picked Not Delivered";
            this.rbPickedNotDelivered.UseVisualStyleBackColor = true;
            this.rbPickedNotDelivered.CheckedChanged += new System.EventHandler(this.rbPickedNotDelivered_CheckedChanged);
            // 
            // rbOrderAgeing
            // 
            this.rbOrderAgeing.AutoSize = true;
            this.rbOrderAgeing.Location = new System.Drawing.Point(148, 277);
            this.rbOrderAgeing.Name = "rbOrderAgeing";
            this.rbOrderAgeing.Size = new System.Drawing.Size(87, 17);
            this.rbOrderAgeing.TabIndex = 3;
            this.rbOrderAgeing.TabStop = true;
            this.rbOrderAgeing.Text = "Order Ageing";
            this.rbOrderAgeing.UseVisualStyleBackColor = true;
            this.rbOrderAgeing.CheckedChanged += new System.EventHandler(this.rbOrderAgeing_CheckedChanged);
            // 
            // frmExecCommercialResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 427);
            this.Controls.Add(this.rbOrderAgeing);
            this.Controls.Add(this.rbPickedNotDelivered);
            this.Controls.Add(this.rbSalesByCustomer);
            this.Controls.Add(this.rbSalesByStyle);
            this.Name = "frmExecCommercialResults";
            this.Text = "Executive Commercial Results";
            this.Load += new System.EventHandler(this.frmExecCommercialResults_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSalesByStyle;
        private System.Windows.Forms.RadioButton rbSalesByCustomer;
        private System.Windows.Forms.RadioButton rbPickedNotDelivered;
        private System.Windows.Forms.RadioButton rbOrderAgeing;
    }
}