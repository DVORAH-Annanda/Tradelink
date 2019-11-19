namespace Cutting
{
    partial class frmBIFStoreToStore
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCreateADeliveryNote = new System.Windows.Forms.RadioButton();
            this.rbCreateAPickingList = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCreateADeliveryNote);
            this.groupBox1.Controls.Add(this.rbCreateAPickingList);
            this.groupBox1.Location = new System.Drawing.Point(84, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbCreateADeliveryNote
            // 
            this.rbCreateADeliveryNote.AutoSize = true;
            this.rbCreateADeliveryNote.Location = new System.Drawing.Point(129, 94);
            this.rbCreateADeliveryNote.Name = "rbCreateADeliveryNote";
            this.rbCreateADeliveryNote.Size = new System.Drawing.Size(133, 17);
            this.rbCreateADeliveryNote.TabIndex = 1;
            this.rbCreateADeliveryNote.Text = "Create A Delivery Note";
            this.rbCreateADeliveryNote.UseVisualStyleBackColor = true;
            this.rbCreateADeliveryNote.CheckedChanged += new System.EventHandler(this.rbCreateADeliveryNote_CheckedChanged);
            // 
            // rbCreateAPickingList
            // 
            this.rbCreateAPickingList.AutoSize = true;
            this.rbCreateAPickingList.Location = new System.Drawing.Point(129, 45);
            this.rbCreateAPickingList.Name = "rbCreateAPickingList";
            this.rbCreateAPickingList.Size = new System.Drawing.Size(122, 17);
            this.rbCreateAPickingList.TabIndex = 0;
            this.rbCreateAPickingList.Text = "Create a Picking List";
            this.rbCreateAPickingList.UseVisualStyleBackColor = true;
            this.rbCreateAPickingList.CheckedChanged += new System.EventHandler(this.rbCreateAPickingList_CheckedChanged);
            // 
            // frmBIFStoreToStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 408);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBIFStoreToStore";
            this.Text = "Bought in Fabric - Store to Store Transfer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCreateADeliveryNote;
        private System.Windows.Forms.RadioButton rbCreateAPickingList;
    }
}