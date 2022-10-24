
namespace DyeHouse
{
    partial class FrmFabricSaleInstr
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
            this.rbCancelPending = new System.Windows.Forms.RadioButton();
            this.rbPaymentConfirm = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbFabricDespatched = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFabricDespatched);
            this.groupBox1.Controls.Add(this.rbCancelPending);
            this.groupBox1.Controls.Add(this.rbPaymentConfirm);
            this.groupBox1.Location = new System.Drawing.Point(170, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rbCancelPending
            // 
            this.rbCancelPending.AutoSize = true;
            this.rbCancelPending.Location = new System.Drawing.Point(72, 110);
            this.rbCancelPending.Name = "rbCancelPending";
            this.rbCancelPending.Size = new System.Drawing.Size(152, 17);
            this.rbCancelPending.TabIndex = 1;
            this.rbCancelPending.Text = "Cancel Pending Instruction";
            this.rbCancelPending.UseVisualStyleBackColor = true;
            // 
            // rbPaymentConfirm
            // 
            this.rbPaymentConfirm.AutoSize = true;
            this.rbPaymentConfirm.Checked = true;
            this.rbPaymentConfirm.Location = new System.Drawing.Point(72, 51);
            this.rbPaymentConfirm.Name = "rbPaymentConfirm";
            this.rbPaymentConfirm.Size = new System.Drawing.Size(130, 17);
            this.rbPaymentConfirm.TabIndex = 0;
            this.rbPaymentConfirm.TabStop = true;
            this.rbPaymentConfirm.Text = "Payment Confirmation ";
            this.rbPaymentConfirm.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(608, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbFabricDespatched
            // 
            this.rbFabricDespatched.AutoSize = true;
            this.rbFabricDespatched.Location = new System.Drawing.Point(72, 165);
            this.rbFabricDespatched.Name = "rbFabricDespatched";
            this.rbFabricDespatched.Size = new System.Drawing.Size(153, 17);
            this.rbFabricDespatched.TabIndex = 2;
            this.rbFabricDespatched.Text = "Fabric Despatch Confirmed";
            this.rbFabricDespatched.UseVisualStyleBackColor = true;
            // 
            // FrmFabricSaleInstr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 379);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmFabricSaleInstr";
            this.Text = "Fabric Sales Instruction";
            this.Load += new System.EventHandler(this.FrmFabricSaleInstr_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCancelPending;
        private System.Windows.Forms.RadioButton rbPaymentConfirm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbFabricDespatched;
    }
}