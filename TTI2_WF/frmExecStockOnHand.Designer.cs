namespace TTI2_WF
{
    partial class frmExecStockOnHand
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
            this.rbSOHSpinningCotton = new System.Windows.Forms.RadioButton();
            this.rbSOHSpinningYarnStock = new System.Windows.Forms.RadioButton();
            this.rbSOHKnittingGreigeStock = new System.Windows.Forms.RadioButton();
            this.rbSOHDyeBatchPending = new System.Windows.Forms.RadioButton();
            this.rbSOHDyeBatches = new System.Windows.Forms.RadioButton();
            this.rbSOHRejectStock = new System.Windows.Forms.RadioButton();
            this.rbSOHCuttingWIP = new System.Windows.Forms.RadioButton();
            this.rbSOHPanelStore = new System.Windows.Forms.RadioButton();
            this.rbSOHCMT = new System.Windows.Forms.RadioButton();
            this.rbSOHWareHouse = new System.Windows.Forms.RadioButton();
            this.rbNegativeStock = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbSOHSpinningCotton
            // 
            this.rbSOHSpinningCotton.AutoSize = true;
            this.rbSOHSpinningCotton.Location = new System.Drawing.Point(90, 33);
            this.rbSOHSpinningCotton.Name = "rbSOHSpinningCotton";
            this.rbSOHSpinningCotton.Size = new System.Drawing.Size(131, 17);
            this.rbSOHSpinningCotton.TabIndex = 0;
            this.rbSOHSpinningCotton.TabStop = true;
            this.rbSOHSpinningCotton.Text = "Spinning Cotton Stock";
            this.rbSOHSpinningCotton.UseVisualStyleBackColor = true;
            this.rbSOHSpinningCotton.CheckedChanged += new System.EventHandler(this.rbSOHSpinningCotton_CheckedChanged);
            // 
            // rbSOHSpinningYarnStock
            // 
            this.rbSOHSpinningYarnStock.AutoSize = true;
            this.rbSOHSpinningYarnStock.Location = new System.Drawing.Point(90, 86);
            this.rbSOHSpinningYarnStock.Name = "rbSOHSpinningYarnStock";
            this.rbSOHSpinningYarnStock.Size = new System.Drawing.Size(122, 17);
            this.rbSOHSpinningYarnStock.TabIndex = 1;
            this.rbSOHSpinningYarnStock.TabStop = true;
            this.rbSOHSpinningYarnStock.Text = "Spinning Yarn Stock";
            this.rbSOHSpinningYarnStock.UseVisualStyleBackColor = true;
            this.rbSOHSpinningYarnStock.CheckedChanged += new System.EventHandler(this.rbSOHSpinningYarnStock_CheckedChanged);
            // 
            // rbSOHKnittingGreigeStock
            // 
            this.rbSOHKnittingGreigeStock.AutoSize = true;
            this.rbSOHKnittingGreigeStock.Location = new System.Drawing.Point(90, 139);
            this.rbSOHKnittingGreigeStock.Name = "rbSOHKnittingGreigeStock";
            this.rbSOHKnittingGreigeStock.Size = new System.Drawing.Size(125, 17);
            this.rbSOHKnittingGreigeStock.TabIndex = 2;
            this.rbSOHKnittingGreigeStock.TabStop = true;
            this.rbSOHKnittingGreigeStock.Text = "Knitting Greige Stock";
            this.rbSOHKnittingGreigeStock.UseVisualStyleBackColor = true;
            this.rbSOHKnittingGreigeStock.CheckedChanged += new System.EventHandler(this.rbSOHKnittingGreigeStock_CheckedChanged);
            // 
            // rbSOHDyeBatchPending
            // 
            this.rbSOHDyeBatchPending.AutoSize = true;
            this.rbSOHDyeBatchPending.Location = new System.Drawing.Point(90, 192);
            this.rbSOHDyeBatchPending.Name = "rbSOHDyeBatchPending";
            this.rbSOHDyeBatchPending.Size = new System.Drawing.Size(166, 17);
            this.rbSOHDyeBatchPending.TabIndex = 3;
            this.rbSOHDyeBatchPending.TabStop = true;
            this.rbSOHDyeBatchPending.Text = "Dyeing WIP Batches Pending";
            this.rbSOHDyeBatchPending.UseVisualStyleBackColor = true;
            this.rbSOHDyeBatchPending.CheckedChanged += new System.EventHandler(this.rbSOHDyeBatchPending_CheckedChanged);
            // 
            // rbSOHDyeBatches
            // 
            this.rbSOHDyeBatches.AutoSize = true;
            this.rbSOHDyeBatches.Location = new System.Drawing.Point(90, 245);
            this.rbSOHDyeBatches.Name = "rbSOHDyeBatches";
            this.rbSOHDyeBatches.Size = new System.Drawing.Size(137, 17);
            this.rbSOHDyeBatches.TabIndex = 4;
            this.rbSOHDyeBatches.TabStop = true;
            this.rbSOHDyeBatches.Text = "Dye Batches in process";
            this.rbSOHDyeBatches.UseVisualStyleBackColor = true;
            this.rbSOHDyeBatches.CheckedChanged += new System.EventHandler(this.rbSOHDyeBatches_CheckedChanged);
            // 
            // rbSOHRejectStock
            // 
            this.rbSOHRejectStock.AutoSize = true;
            this.rbSOHRejectStock.Location = new System.Drawing.Point(400, 33);
            this.rbSOHRejectStock.Name = "rbSOHRejectStock";
            this.rbSOHRejectStock.Size = new System.Drawing.Size(109, 17);
            this.rbSOHRejectStock.TabIndex = 5;
            this.rbSOHRejectStock.TabStop = true;
            this.rbSOHRejectStock.Text = "Dye Reject Stock";
            this.rbSOHRejectStock.UseVisualStyleBackColor = true;
            this.rbSOHRejectStock.CheckedChanged += new System.EventHandler(this.rbSOHRejectStock_CheckedChanged);
            // 
            // rbSOHCuttingWIP
            // 
            this.rbSOHCuttingWIP.AutoSize = true;
            this.rbSOHCuttingWIP.Location = new System.Drawing.Point(400, 86);
            this.rbSOHCuttingWIP.Name = "rbSOHCuttingWIP";
            this.rbSOHCuttingWIP.Size = new System.Drawing.Size(82, 17);
            this.rbSOHCuttingWIP.TabIndex = 6;
            this.rbSOHCuttingWIP.TabStop = true;
            this.rbSOHCuttingWIP.Text = "Cutting WIP";
            this.rbSOHCuttingWIP.UseVisualStyleBackColor = true;
            this.rbSOHCuttingWIP.CheckedChanged += new System.EventHandler(this.rbSOHCuttingWIP_CheckedChanged);
            // 
            // rbSOHPanelStore
            // 
            this.rbSOHPanelStore.AutoSize = true;
            this.rbSOHPanelStore.Location = new System.Drawing.Point(400, 139);
            this.rbSOHPanelStore.Name = "rbSOHPanelStore";
            this.rbSOHPanelStore.Size = new System.Drawing.Size(122, 17);
            this.rbSOHPanelStore.TabIndex = 7;
            this.rbSOHPanelStore.TabStop = true;
            this.rbSOHPanelStore.Text = "Stock in Panel Store";
            this.rbSOHPanelStore.UseVisualStyleBackColor = true;
            this.rbSOHPanelStore.CheckedChanged += new System.EventHandler(this.rbSOHPanelStore_CheckedChanged);
            // 
            // rbSOHCMT
            // 
            this.rbSOHCMT.AutoSize = true;
            this.rbSOHCMT.Location = new System.Drawing.Point(400, 192);
            this.rbSOHCMT.Name = "rbSOHCMT";
            this.rbSOHCMT.Size = new System.Drawing.Size(123, 17);
            this.rbSOHCMT.TabIndex = 8;
            this.rbSOHCMT.TabStop = true;
            this.rbSOHCMT.Text = "CMT Stock on Hand";
            this.rbSOHCMT.UseVisualStyleBackColor = true;
            this.rbSOHCMT.CheckedChanged += new System.EventHandler(this.rbSOHCMT_CheckedChanged);
            // 
            // rbSOHWareHouse
            // 
            this.rbSOHWareHouse.AutoSize = true;
            this.rbSOHWareHouse.Location = new System.Drawing.Point(400, 245);
            this.rbSOHWareHouse.Name = "rbSOHWareHouse";
            this.rbSOHWareHouse.Size = new System.Drawing.Size(152, 17);
            this.rbSOHWareHouse.TabIndex = 9;
            this.rbSOHWareHouse.TabStop = true;
            this.rbSOHWareHouse.Text = "Garments available for sale";
            this.rbSOHWareHouse.UseVisualStyleBackColor = true;
            this.rbSOHWareHouse.CheckedChanged += new System.EventHandler(this.rbSOHWareHouse_CheckedChanged);
            // 
            // rbNegativeStock
            // 
            this.rbNegativeStock.AutoSize = true;
            this.rbNegativeStock.Location = new System.Drawing.Point(221, 301);
            this.rbNegativeStock.Name = "rbNegativeStock";
            this.rbNegativeStock.Size = new System.Drawing.Size(155, 17);
            this.rbNegativeStock.TabIndex = 10;
            this.rbNegativeStock.TabStop = true;
            this.rbNegativeStock.Text = "Wholesaler Negative Stock";
            this.rbNegativeStock.UseVisualStyleBackColor = true;
            this.rbNegativeStock.CheckedChanged += new System.EventHandler(this.rbNegativeStock_CheckedChanged);
            // 
            // frmExecStockOnHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 382);
            this.Controls.Add(this.rbNegativeStock);
            this.Controls.Add(this.rbSOHWareHouse);
            this.Controls.Add(this.rbSOHCMT);
            this.Controls.Add(this.rbSOHPanelStore);
            this.Controls.Add(this.rbSOHCuttingWIP);
            this.Controls.Add(this.rbSOHRejectStock);
            this.Controls.Add(this.rbSOHDyeBatches);
            this.Controls.Add(this.rbSOHDyeBatchPending);
            this.Controls.Add(this.rbSOHKnittingGreigeStock);
            this.Controls.Add(this.rbSOHSpinningYarnStock);
            this.Controls.Add(this.rbSOHSpinningCotton);
            this.Name = "frmExecStockOnHand";
            this.Text = "Executive Stock On Hand";
            this.Load += new System.EventHandler(this.frmExecStockOnHand_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbSOHSpinningCotton;
        private System.Windows.Forms.RadioButton rbSOHSpinningYarnStock;
        private System.Windows.Forms.RadioButton rbSOHKnittingGreigeStock;
        private System.Windows.Forms.RadioButton rbSOHDyeBatchPending;
        private System.Windows.Forms.RadioButton rbSOHDyeBatches;
        private System.Windows.Forms.RadioButton rbSOHRejectStock;
        private System.Windows.Forms.RadioButton rbSOHCuttingWIP;
        private System.Windows.Forms.RadioButton rbSOHPanelStore;
        private System.Windows.Forms.RadioButton rbSOHCMT;
        private System.Windows.Forms.RadioButton rbSOHWareHouse;
        private System.Windows.Forms.RadioButton rbNegativeStock;
    }
}