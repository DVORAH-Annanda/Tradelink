namespace TTI2_WF
{
    partial class frmQADyeSel
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbQualityException = new System.Windows.Forms.RadioButton();
            this.rbNCRResults = new System.Windows.Forms.RadioButton();
            this.rbShadeResultsAfterCompacting = new System.Windows.Forms.RadioButton();
            this.rbShadeResultsAfterDrying = new System.Windows.Forms.RadioButton();
            this.rbShadeResultsAterDyeing = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDyeProcessLoss = new System.Windows.Forms.RadioButton();
            this.rbDyeResourceEfficiency = new System.Windows.Forms.RadioButton();
            this.rbDyeMachinePerformance = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDyedToQuarantine = new System.Windows.Forms.RadioButton();
            this.rbDyedNotFinished = new System.Windows.Forms.RadioButton();
            this.rbReprocessedDyeBatches = new System.Windows.Forms.RadioButton();
            this.rbRejectedDyeBatches = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbFabricToStore = new System.Windows.Forms.RadioButton();
            this.rbFabricToQuar = new System.Windows.Forms.RadioButton();
            this.rbFabricStockOnHAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(96, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 585);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Reports Available";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbQualityException);
            this.groupBox4.Controls.Add(this.rbNCRResults);
            this.groupBox4.Controls.Add(this.rbShadeResultsAfterCompacting);
            this.groupBox4.Controls.Add(this.rbShadeResultsAfterDrying);
            this.groupBox4.Controls.Add(this.rbShadeResultsAterDyeing);
            this.groupBox4.Location = new System.Drawing.Point(16, 365);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(258, 197);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "QA Reports";
            // 
            // rbQualityException
            // 
            this.rbQualityException.AutoSize = true;
            this.rbQualityException.Location = new System.Drawing.Point(28, 171);
            this.rbQualityException.Name = "rbQualityException";
            this.rbQualityException.Size = new System.Drawing.Size(142, 17);
            this.rbQualityException.TabIndex = 4;
            this.rbQualityException.TabStop = true;
            this.rbQualityException.Text = "Quality Exception Report";
            this.rbQualityException.UseVisualStyleBackColor = true;
            this.rbQualityException.CheckedChanged += new System.EventHandler(this.rbQualityException_CheckedChanged);
            // 
            // rbNCRResults
            // 
            this.rbNCRResults.AutoSize = true;
            this.rbNCRResults.Location = new System.Drawing.Point(27, 137);
            this.rbNCRResults.Name = "rbNCRResults";
            this.rbNCRResults.Size = new System.Drawing.Size(86, 17);
            this.rbNCRResults.TabIndex = 3;
            this.rbNCRResults.TabStop = true;
            this.rbNCRResults.Text = "NCR Results";
            this.rbNCRResults.UseVisualStyleBackColor = true;
            this.rbNCRResults.CheckedChanged += new System.EventHandler(this.rbNCRResults_CheckedChanged);
            // 
            // rbShadeResultsAfterCompacting
            // 
            this.rbShadeResultsAfterCompacting.AutoSize = true;
            this.rbShadeResultsAfterCompacting.Location = new System.Drawing.Point(27, 103);
            this.rbShadeResultsAfterCompacting.Name = "rbShadeResultsAfterCompacting";
            this.rbShadeResultsAfterCompacting.Size = new System.Drawing.Size(178, 17);
            this.rbShadeResultsAfterCompacting.TabIndex = 2;
            this.rbShadeResultsAfterCompacting.TabStop = true;
            this.rbShadeResultsAfterCompacting.Text = "Shade Results After Compacting";
            this.rbShadeResultsAfterCompacting.UseVisualStyleBackColor = true;
            this.rbShadeResultsAfterCompacting.CheckedChanged += new System.EventHandler(this.ShadeResultsAfterCompacting_CheckedChanged);
            // 
            // rbShadeResultsAfterDrying
            // 
            this.rbShadeResultsAfterDrying.AutoSize = true;
            this.rbShadeResultsAfterDrying.Location = new System.Drawing.Point(27, 69);
            this.rbShadeResultsAfterDrying.Name = "rbShadeResultsAfterDrying";
            this.rbShadeResultsAfterDrying.Size = new System.Drawing.Size(152, 17);
            this.rbShadeResultsAfterDrying.TabIndex = 1;
            this.rbShadeResultsAfterDrying.TabStop = true;
            this.rbShadeResultsAfterDrying.Text = "Shade Results After Drying";
            this.rbShadeResultsAfterDrying.UseVisualStyleBackColor = true;
            this.rbShadeResultsAfterDrying.CheckedChanged += new System.EventHandler(this.rbShadeResultsAfterDrying_CheckedChanged);
            // 
            // rbShadeResultsAterDyeing
            // 
            this.rbShadeResultsAterDyeing.AutoSize = true;
            this.rbShadeResultsAterDyeing.Location = new System.Drawing.Point(27, 35);
            this.rbShadeResultsAterDyeing.Name = "rbShadeResultsAterDyeing";
            this.rbShadeResultsAterDyeing.Size = new System.Drawing.Size(155, 17);
            this.rbShadeResultsAterDyeing.TabIndex = 0;
            this.rbShadeResultsAterDyeing.TabStop = true;
            this.rbShadeResultsAterDyeing.Text = "Shade Results After Dyeing";
            this.rbShadeResultsAterDyeing.UseVisualStyleBackColor = true;
            this.rbShadeResultsAterDyeing.CheckedChanged += new System.EventHandler(this.rbShadeResultsAterDyeing_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDyeProcessLoss);
            this.groupBox3.Controls.Add(this.rbDyeResourceEfficiency);
            this.groupBox3.Controls.Add(this.rbDyeMachinePerformance);
            this.groupBox3.Location = new System.Drawing.Point(16, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 151);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dyes and Chemicals";
            // 
            // rbDyeProcessLoss
            // 
            this.rbDyeProcessLoss.AutoSize = true;
            this.rbDyeProcessLoss.Location = new System.Drawing.Point(27, 113);
            this.rbDyeProcessLoss.Name = "rbDyeProcessLoss";
            this.rbDyeProcessLoss.Size = new System.Drawing.Size(110, 17);
            this.rbDyeProcessLoss.TabIndex = 2;
            this.rbDyeProcessLoss.TabStop = true;
            this.rbDyeProcessLoss.Text = "Dye Process Loss";
            this.rbDyeProcessLoss.UseVisualStyleBackColor = true;
            this.rbDyeProcessLoss.CheckedChanged += new System.EventHandler(this.rbDyeProcessLoss_CheckedChanged);
            // 
            // rbDyeResourceEfficiency
            // 
            this.rbDyeResourceEfficiency.AutoSize = true;
            this.rbDyeResourceEfficiency.Location = new System.Drawing.Point(27, 73);
            this.rbDyeResourceEfficiency.Name = "rbDyeResourceEfficiency";
            this.rbDyeResourceEfficiency.Size = new System.Drawing.Size(142, 17);
            this.rbDyeResourceEfficiency.TabIndex = 1;
            this.rbDyeResourceEfficiency.TabStop = true;
            this.rbDyeResourceEfficiency.Text = "Dye Resource Efficiency";
            this.rbDyeResourceEfficiency.UseVisualStyleBackColor = true;
            this.rbDyeResourceEfficiency.CheckedChanged += new System.EventHandler(this.rbDyeResourceEfficiency_CheckedChanged);
            // 
            // rbDyeMachinePerformance
            // 
            this.rbDyeMachinePerformance.AutoSize = true;
            this.rbDyeMachinePerformance.Location = new System.Drawing.Point(27, 33);
            this.rbDyeMachinePerformance.Name = "rbDyeMachinePerformance";
            this.rbDyeMachinePerformance.Size = new System.Drawing.Size(151, 17);
            this.rbDyeMachinePerformance.TabIndex = 0;
            this.rbDyeMachinePerformance.TabStop = true;
            this.rbDyeMachinePerformance.Text = "Dye Machine Performance";
            this.rbDyeMachinePerformance.UseVisualStyleBackColor = true;
            this.rbDyeMachinePerformance.CheckedChanged += new System.EventHandler(this.rbDyeMachinePerformance_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDyedToQuarantine);
            this.groupBox2.Controls.Add(this.rbDyedNotFinished);
            this.groupBox2.Controls.Add(this.rbReprocessedDyeBatches);
            this.groupBox2.Controls.Add(this.rbRejectedDyeBatches);
            this.groupBox2.Location = new System.Drawing.Point(16, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 166);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dye Batch Reports";
            // 
            // rbDyedToQuarantine
            // 
            this.rbDyedToQuarantine.AutoSize = true;
            this.rbDyedToQuarantine.Location = new System.Drawing.Point(28, 130);
            this.rbDyedToQuarantine.Name = "rbDyedToQuarantine";
            this.rbDyedToQuarantine.Size = new System.Drawing.Size(153, 17);
            this.rbDyedToQuarantine.TabIndex = 3;
            this.rbDyedToQuarantine.TabStop = true;
            this.rbDyedToQuarantine.Text = "Fabric Dyed To Quarantine";
            this.rbDyedToQuarantine.UseVisualStyleBackColor = true;
            this.rbDyedToQuarantine.CheckedChanged += new System.EventHandler(this.rbDyedToQuarantine_CheckedChanged);
            // 
            // rbDyedNotFinished
            // 
            this.rbDyedNotFinished.AutoSize = true;
            this.rbDyedNotFinished.Location = new System.Drawing.Point(28, 96);
            this.rbDyedNotFinished.Name = "rbDyedNotFinished";
            this.rbDyedNotFinished.Size = new System.Drawing.Size(144, 17);
            this.rbDyedNotFinished.TabIndex = 2;
            this.rbDyedNotFinished.TabStop = true;
            this.rbDyedNotFinished.Text = "Fabric Dyed Not Finished";
            this.rbDyedNotFinished.UseVisualStyleBackColor = true;
            this.rbDyedNotFinished.CheckedChanged += new System.EventHandler(this.rbDyedNotFinished_CheckedChanged);
            // 
            // rbReprocessedDyeBatches
            // 
            this.rbReprocessedDyeBatches.AutoSize = true;
            this.rbReprocessedDyeBatches.Location = new System.Drawing.Point(28, 62);
            this.rbReprocessedDyeBatches.Name = "rbReprocessedDyeBatches";
            this.rbReprocessedDyeBatches.Size = new System.Drawing.Size(152, 17);
            this.rbReprocessedDyeBatches.TabIndex = 1;
            this.rbReprocessedDyeBatches.TabStop = true;
            this.rbReprocessedDyeBatches.Text = "Reprocessed Dye Batches";
            this.rbReprocessedDyeBatches.UseVisualStyleBackColor = true;
            this.rbReprocessedDyeBatches.CheckedChanged += new System.EventHandler(this.rbReprocessedDyeBatches_CheckedChanged);
            // 
            // rbRejectedDyeBatches
            // 
            this.rbRejectedDyeBatches.AutoSize = true;
            this.rbRejectedDyeBatches.Location = new System.Drawing.Point(28, 28);
            this.rbRejectedDyeBatches.Name = "rbRejectedDyeBatches";
            this.rbRejectedDyeBatches.Size = new System.Drawing.Size(132, 17);
            this.rbRejectedDyeBatches.TabIndex = 0;
            this.rbRejectedDyeBatches.TabStop = true;
            this.rbRejectedDyeBatches.Text = "Rejected Dye Batches";
            this.rbRejectedDyeBatches.UseVisualStyleBackColor = true;
            this.rbRejectedDyeBatches.CheckedChanged += new System.EventHandler(this.rbRejectedDyeBatches_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbFabricToStore);
            this.groupBox5.Controls.Add(this.rbFabricToQuar);
            this.groupBox5.Controls.Add(this.rbFabricStockOnHAnd);
            this.groupBox5.Location = new System.Drawing.Point(301, 36);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(258, 147);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dye Fabric Reports";
            // 
            // rbFabricToStore
            // 
            this.rbFabricToStore.AutoSize = true;
            this.rbFabricToStore.Location = new System.Drawing.Point(28, 96);
            this.rbFabricToStore.Name = "rbFabricToStore";
            this.rbFabricToStore.Size = new System.Drawing.Size(98, 17);
            this.rbFabricToStore.TabIndex = 2;
            this.rbFabricToStore.TabStop = true;
            this.rbFabricToStore.Text = "Fabric To Store";
            this.rbFabricToStore.UseVisualStyleBackColor = true;
            this.rbFabricToStore.CheckedChanged += new System.EventHandler(this.rbFabricToStore_CheckedChanged);
            // 
            // rbFabricToQuar
            // 
            this.rbFabricToQuar.AutoSize = true;
            this.rbFabricToQuar.Location = new System.Drawing.Point(28, 62);
            this.rbFabricToQuar.Name = "rbFabricToQuar";
            this.rbFabricToQuar.Size = new System.Drawing.Size(179, 17);
            this.rbFabricToQuar.TabIndex = 1;
            this.rbFabricToQuar.TabStop = true;
            this.rbFabricToQuar.Text = "Fabric Production To Quarantine";
            this.rbFabricToQuar.UseVisualStyleBackColor = true;
            this.rbFabricToQuar.CheckedChanged += new System.EventHandler(this.rbFabricToQuar_CheckedChanged);
            // 
            // rbFabricStockOnHAnd
            // 
            this.rbFabricStockOnHAnd.AutoSize = true;
            this.rbFabricStockOnHAnd.Location = new System.Drawing.Point(28, 28);
            this.rbFabricStockOnHAnd.Name = "rbFabricStockOnHAnd";
            this.rbFabricStockOnHAnd.Size = new System.Drawing.Size(131, 17);
            this.rbFabricStockOnHAnd.TabIndex = 0;
            this.rbFabricStockOnHAnd.TabStop = true;
            this.rbFabricStockOnHAnd.Text = "Fabric Stock On Hand";
            this.rbFabricStockOnHAnd.UseVisualStyleBackColor = true;
            this.rbFabricStockOnHAnd.CheckedChanged += new System.EventHandler(this.rbFabricStockOnHAnd_CheckedChanged);
            // 
            // frmQADyeSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 667);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQADyeSel";
            this.Text = "QA Dye Report Selection ";
            this.Load += new System.EventHandler(this.frmQADyeSel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbNCRResults;
        private System.Windows.Forms.RadioButton rbShadeResultsAfterCompacting;
        private System.Windows.Forms.RadioButton rbShadeResultsAfterDrying;
        private System.Windows.Forms.RadioButton rbShadeResultsAterDyeing;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbDyeProcessLoss;
        private System.Windows.Forms.RadioButton rbDyeResourceEfficiency;
        private System.Windows.Forms.RadioButton rbDyeMachinePerformance;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbReprocessedDyeBatches;
        private System.Windows.Forms.RadioButton rbRejectedDyeBatches;
        private System.Windows.Forms.RadioButton rbDyedToQuarantine;
        private System.Windows.Forms.RadioButton rbDyedNotFinished;
        private System.Windows.Forms.RadioButton rbQualityException;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbFabricToStore;
        private System.Windows.Forms.RadioButton rbFabricToQuar;
        private System.Windows.Forms.RadioButton rbFabricStockOnHAnd;
    }
}