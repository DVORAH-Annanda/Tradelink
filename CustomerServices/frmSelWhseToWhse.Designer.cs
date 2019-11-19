namespace CustomerServices
{
    partial class frmSelWhseToWhse
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCreateADeliveryNote = new System.Windows.Forms.RadioButton();
            this.rbCreateAPickingList = new System.Windows.Forms.RadioButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCreateADeliveryNote);
            this.groupBox1.Controls.Add(this.rbCreateAPickingList);
            this.groupBox1.Location = new System.Drawing.Point(131, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbCreateADeliveryNote
            // 
            this.rbCreateADeliveryNote.AutoSize = true;
            this.rbCreateADeliveryNote.Location = new System.Drawing.Point(129, 101);
            this.rbCreateADeliveryNote.Name = "rbCreateADeliveryNote";
            this.rbCreateADeliveryNote.Size = new System.Drawing.Size(207, 17);
            this.rbCreateADeliveryNote.TabIndex = 1;
            this.rbCreateADeliveryNote.Text = "Create A Delivery Note / Receipt Note";
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
            // frmSelWhseToWhse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 358);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSelWhseToWhse";
            this.Text = "Warehouse to warehouse transactional selection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCreateADeliveryNote;
        private System.Windows.Forms.RadioButton rbCreateAPickingList;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}