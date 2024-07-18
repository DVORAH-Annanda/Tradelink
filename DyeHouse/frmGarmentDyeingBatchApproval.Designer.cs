
using System;

namespace DyeHouse
{
    partial class frmGarmentDyeingBatchApproval
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
            this.dgvDyeBatch = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpDateDyed = new System.Windows.Forms.DateTimePicker();
            this.cboBatchNo = new System.Windows.Forms.ComboBox();
            this.cboShift = new System.Windows.Forms.ComboBox();
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.lblDateDyed = new System.Windows.Forms.Label();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblMachine = new System.Windows.Forms.Label();
            this.lblStyle = new System.Windows.Forms.Label();
            this.lblColour = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.txtColour = new System.Windows.Forms.TextBox();
            this.lblCloseDyeBatch = new System.Windows.Forms.Label();
            this.cboCloseDyeBatch = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDyeBatch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDyeBatch
            // 
            this.dgvDyeBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDyeBatch.Location = new System.Drawing.Point(42, 162);
            this.dgvDyeBatch.Name = "dgvDyeBatch";
            this.dgvDyeBatch.Size = new System.Drawing.Size(594, 244);
            this.dgvDyeBatch.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(561, 474);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDateDyed
            // 
            this.dtpDateDyed.Location = new System.Drawing.Point(103, 61);
            this.dtpDateDyed.Name = "dtpDateDyed";
            this.dtpDateDyed.Size = new System.Drawing.Size(200, 20);
            this.dtpDateDyed.TabIndex = 1;
            // 
            // cboBatchNo
            // 
            this.cboBatchNo.FormattingEnabled = true;
            this.cboBatchNo.Location = new System.Drawing.Point(103, 32);
            this.cboBatchNo.Name = "cboBatchNo";
            this.cboBatchNo.Size = new System.Drawing.Size(200, 21);
            this.cboBatchNo.TabIndex = 0;
            // 
            // cboShift
            // 
            this.cboShift.FormattingEnabled = true;
            this.cboShift.Location = new System.Drawing.Point(436, 32);
            this.cboShift.Name = "cboShift";
            this.cboShift.Size = new System.Drawing.Size(200, 21);
            this.cboShift.TabIndex = 2;
            // 
            // cboMachine
            // 
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(436, 61);
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.Size = new System.Drawing.Size(200, 21);
            this.cboMachine.TabIndex = 3;
            // 
            // lblDateDyed
            // 
            this.lblDateDyed.AutoSize = true;
            this.lblDateDyed.Location = new System.Drawing.Point(39, 64);
            this.lblDateDyed.Name = "lblDateDyed";
            this.lblDateDyed.Size = new System.Drawing.Size(58, 13);
            this.lblDateDyed.TabIndex = 8;
            this.lblDateDyed.Text = "Date Dyed";
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(39, 36);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(52, 13);
            this.lblBatchNo.TabIndex = 9;
            this.lblBatchNo.Text = "Batch No";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(372, 36);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(28, 13);
            this.lblShift.TabIndex = 10;
            this.lblShift.Text = "Shift";
            // 
            // lblMachine
            // 
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(372, 65);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(48, 13);
            this.lblMachine.TabIndex = 11;
            this.lblMachine.Text = "Machine";
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(39, 93);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(30, 13);
            this.lblStyle.TabIndex = 13;
            this.lblStyle.Text = "Style";
            // 
            // lblColour
            // 
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(39, 121);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(37, 13);
            this.lblColour.TabIndex = 12;
            this.lblColour.Text = "Colour";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(103, 89);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(200, 20);
            this.txtStyle.TabIndex = 10;
            this.txtStyle.TabStop = false;
            // 
            // txtColour
            // 
            this.txtColour.Location = new System.Drawing.Point(103, 117);
            this.txtColour.Name = "txtColour";
            this.txtColour.ReadOnly = true;
            this.txtColour.Size = new System.Drawing.Size(200, 20);
            this.txtColour.TabIndex = 15;
            this.txtColour.TabStop = false;
            // 
            // lblCloseDyeBatch
            // 
            this.lblCloseDyeBatch.AutoSize = true;
            this.lblCloseDyeBatch.Location = new System.Drawing.Point(463, 433);
            this.lblCloseDyeBatch.Name = "lblCloseDyeBatch";
            this.lblCloseDyeBatch.Size = new System.Drawing.Size(92, 13);
            this.lblCloseDyeBatch.TabIndex = 16;
            this.lblCloseDyeBatch.Text = "Close Dye Batch?";
            // 
            // cboCloseDyeBatch
            // 
            this.cboCloseDyeBatch.FormattingEnabled = true;
            this.cboCloseDyeBatch.Location = new System.Drawing.Point(561, 430);
            this.cboCloseDyeBatch.Name = "cboCloseDyeBatch";
            this.cboCloseDyeBatch.Size = new System.Drawing.Size(75, 21);
            this.cboCloseDyeBatch.TabIndex = 5;
            // 
            // frmGarmentDyeingBatchApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 528);
            this.Controls.Add(this.cboCloseDyeBatch);
            this.Controls.Add(this.lblCloseDyeBatch);
            this.Controls.Add(this.txtColour);
            this.Controls.Add(this.txtStyle);
            this.Controls.Add(this.lblStyle);
            this.Controls.Add(this.lblColour);
            this.Controls.Add(this.lblMachine);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.lblBatchNo);
            this.Controls.Add(this.lblDateDyed);
            this.Controls.Add(this.cboMachine);
            this.Controls.Add(this.cboShift);
            this.Controls.Add(this.cboBatchNo);
            this.Controls.Add(this.dtpDateDyed);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvDyeBatch);
            this.Name = "frmGarmentDyeingBatchApproval";
            this.Text = "Garment Dyeing Batch Approval";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGarmentDyeingBatchApproval_FormClosing);
            this.Load += new System.EventHandler(this.frmGarmentDyeingBatchApproval_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDyeBatch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDyeBatch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDateDyed;
        private System.Windows.Forms.ComboBox cboBatchNo;
        private System.Windows.Forms.ComboBox cboShift;
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.Label lblDateDyed;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblMachine;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblColour;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtColour;
        private System.Windows.Forms.Label lblCloseDyeBatch;
        private System.Windows.Forms.ComboBox cboCloseDyeBatch;
    }
}