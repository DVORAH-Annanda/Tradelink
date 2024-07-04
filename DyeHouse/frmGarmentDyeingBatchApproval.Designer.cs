
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
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.lblDateDyed = new System.Windows.Forms.Label();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblMachine = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDyeBatch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDyeBatch
            // 
            this.dgvDyeBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDyeBatch.Location = new System.Drawing.Point(30, 173);
            this.dgvDyeBatch.Name = "dgvDyeBatch";
            this.dgvDyeBatch.Size = new System.Drawing.Size(739, 244);
            this.dgvDyeBatch.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(694, 442);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDateDyed
            // 
            this.dtpDateDyed.Location = new System.Drawing.Point(95, 124);
            this.dtpDateDyed.Name = "dtpDateDyed";
            this.dtpDateDyed.Size = new System.Drawing.Size(200, 20);
            this.dtpDateDyed.TabIndex = 3;
            // 
            // cboBatchNo
            // 
            this.cboBatchNo.FormattingEnabled = true;
            this.cboBatchNo.Location = new System.Drawing.Point(95, 97);
            this.cboBatchNo.Name = "cboBatchNo";
            this.cboBatchNo.Size = new System.Drawing.Size(200, 21);
            this.cboBatchNo.TabIndex = 2;
            // 
            // cboMachine
            // 
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(95, 57);
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.Size = new System.Drawing.Size(200, 21);
            this.cboMachine.TabIndex = 1;
            // 
            // lblDateDyed
            // 
            this.lblDateDyed.AutoSize = true;
            this.lblDateDyed.Location = new System.Drawing.Point(31, 128);
            this.lblDateDyed.Name = "lblDateDyed";
            this.lblDateDyed.Size = new System.Drawing.Size(58, 13);
            this.lblDateDyed.TabIndex = 8;
            this.lblDateDyed.Text = "Date Dyed";
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(31, 101);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(52, 13);
            this.lblBatchNo.TabIndex = 9;
            this.lblBatchNo.Text = "Batch No";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(31, 34);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(28, 13);
            this.lblShift.TabIndex = 10;
            this.lblShift.Text = "Shift";
            // 
            // lblMachine
            // 
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(31, 61);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(48, 13);
            this.lblMachine.TabIndex = 11;
            this.lblMachine.Text = "Machine";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 0;
            // 
            // frmGarmentDyeingBatchApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 487);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblMachine);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.lblBatchNo);
            this.Controls.Add(this.lblDateDyed);
            this.Controls.Add(this.cboMachine);
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
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.Label lblDateDyed;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblMachine;
        private System.Windows.Forms.TextBox textBox1;
    }
}