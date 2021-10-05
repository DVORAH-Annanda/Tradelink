namespace TTI2_WF
{
    partial class frmKPIQuality
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
            this.grpbxDatePicker = new System.Windows.Forms.GroupBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.grpbxSpinning = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpinning = new System.Windows.Forms.Label();
            this.grKPIHeader = new System.Windows.Forms.GroupBox();
            this.lblDataLoading = new System.Windows.Forms.Label();
            this.prgBarKPI = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateFromKPI = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateToKPI = new System.Windows.Forms.DateTimePicker();
            this.grpCardMachineQualityCheck = new System.Windows.Forms.GroupBox();
            this.button13 = new System.Windows.Forms.Button();
            this.grpKPIType = new System.Windows.Forms.GroupBox();
            this.rbtnCommercial = new System.Windows.Forms.RadioButton();
            this.rbtnQuality = new System.Windows.Forms.RadioButton();
            this.rbtnProduction = new System.Windows.Forms.RadioButton();
            this.backgroundWorkerKPI = new System.ComponentModel.BackgroundWorker();
            this.grpKPIQualityContainer = new System.Windows.Forms.GroupBox();
            this.dgvByMachineByGradeQA = new System.Windows.Forms.DataGridView();
            this.lblKPIDescription = new System.Windows.Forms.Label();
            this.lblKPI = new System.Windows.Forms.Label();
            this.lblQualityKPI = new System.Windows.Forms.Label();
            this.tvwKPIQuality = new System.Windows.Forms.TreeView();
            this.grpbxDatePicker.SuspendLayout();
            this.grpbxSpinning.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grKPIHeader.SuspendLayout();
            this.grpCardMachineQualityCheck.SuspendLayout();
            this.grpKPIType.SuspendLayout();
            this.grpKPIQualityContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvByMachineByGradeQA)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbxDatePicker
            // 
            this.grpbxDatePicker.Controls.Add(this.lblDateTo);
            this.grpbxDatePicker.Controls.Add(this.lblDateFrom);
            this.grpbxDatePicker.Controls.Add(this.dateTimePicker2);
            this.grpbxDatePicker.Controls.Add(this.dateTimePicker1);
            this.grpbxDatePicker.Location = new System.Drawing.Point(-214, -46);
            this.grpbxDatePicker.Name = "grpbxDatePicker";
            this.grpbxDatePicker.Size = new System.Drawing.Size(1228, 48);
            this.grpbxDatePicker.TabIndex = 8;
            this.grpbxDatePicker.TabStop = false;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(348, 16);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(46, 13);
            this.lblDateTo.TabIndex = 4;
            this.lblDateTo.Text = "Date To";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(8, 16);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(56, 13);
            this.lblDateFrom.TabIndex = 3;
            this.lblDateFrom.Text = "Date From";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(400, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(206, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(70, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(206, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // grpbxSpinning
            // 
            this.grpbxSpinning.Controls.Add(this.groupBox3);
            this.grpbxSpinning.Controls.Add(this.button1);
            this.grpbxSpinning.Controls.Add(this.label2);
            this.grpbxSpinning.Controls.Add(this.lblSpinning);
            this.grpbxSpinning.Location = new System.Drawing.Point(-214, 8);
            this.grpbxSpinning.Name = "grpbxSpinning";
            this.grpbxSpinning.Size = new System.Drawing.Size(200, 489);
            this.grpbxSpinning.TabIndex = 9;
            this.grpbxSpinning.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Controls.Add(this.button10);
            this.groupBox3.Controls.Add(this.button11);
            this.groupBox3.Location = new System.Drawing.Point(6, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 181);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quality Measurement Area ";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(11, 19);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(171, 45);
            this.button9.TabIndex = 11;
            this.button9.Text = "Card Total";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(11, 121);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(171, 45);
            this.button10.TabIndex = 10;
            this.button10.Text = "Spinning Machine";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(11, 70);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(171, 45);
            this.button11.TabIndex = 9;
            this.button11.Text = "RSB Total";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 52);
            this.button1.TabIndex = 4;
            this.button1.Text = "Production Total";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // lblSpinning
            // 
            this.lblSpinning.BackColor = System.Drawing.Color.Silver;
            this.lblSpinning.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpinning.Location = new System.Drawing.Point(6, 19);
            this.lblSpinning.Name = "lblSpinning";
            this.lblSpinning.Size = new System.Drawing.Size(188, 44);
            this.lblSpinning.TabIndex = 0;
            this.lblSpinning.Text = "Spinning";
            this.lblSpinning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grKPIHeader
            // 
            this.grKPIHeader.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grKPIHeader.Controls.Add(this.lblDataLoading);
            this.grKPIHeader.Controls.Add(this.prgBarKPI);
            this.grKPIHeader.Controls.Add(this.label4);
            this.grKPIHeader.Controls.Add(this.dtpDateFromKPI);
            this.grKPIHeader.Controls.Add(this.label3);
            this.grKPIHeader.Controls.Add(this.dtpDateToKPI);
            this.grKPIHeader.Location = new System.Drawing.Point(3, 4);
            this.grKPIHeader.Name = "grKPIHeader";
            this.grKPIHeader.Size = new System.Drawing.Size(1025, 95);
            this.grKPIHeader.TabIndex = 10;
            this.grKPIHeader.TabStop = false;
            // 
            // lblDataLoading
            // 
            this.lblDataLoading.AutoSize = true;
            this.lblDataLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataLoading.Location = new System.Drawing.Point(772, 24);
            this.lblDataLoading.Name = "lblDataLoading";
            this.lblDataLoading.Size = new System.Drawing.Size(105, 17);
            this.lblDataLoading.TabIndex = 11;
            this.lblDataLoading.Text = "Loading Data...";
            // 
            // prgBarKPI
            // 
            this.prgBarKPI.Location = new System.Drawing.Point(648, 41);
            this.prgBarKPI.Name = "prgBarKPI";
            this.prgBarKPI.Size = new System.Drawing.Size(353, 26);
            this.prgBarKPI.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(200, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date From";
            // 
            // dtpDateFromKPI
            // 
            this.dtpDateFromKPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateFromKPI.Location = new System.Drawing.Point(302, 19);
            this.dtpDateFromKPI.Name = "dtpDateFromKPI";
            this.dtpDateFromKPI.Size = new System.Drawing.Size(271, 26);
            this.dtpDateFromKPI.TabIndex = 5;
            this.dtpDateFromKPI.ValueChanged += new System.EventHandler(this.DtpDateFromKPI_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date To";
            // 
            // dtpDateToKPI
            // 
            this.dtpDateToKPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateToKPI.Location = new System.Drawing.Point(303, 51);
            this.dtpDateToKPI.Name = "dtpDateToKPI";
            this.dtpDateToKPI.Size = new System.Drawing.Size(271, 26);
            this.dtpDateToKPI.TabIndex = 1;
            this.dtpDateToKPI.ValueChanged += new System.EventHandler(this.DtpDateToKPI_ValueChanged);
            // 
            // grpCardMachineQualityCheck
            // 
            this.grpCardMachineQualityCheck.Controls.Add(this.button13);
            this.grpCardMachineQualityCheck.Location = new System.Drawing.Point(260, 93);
            this.grpCardMachineQualityCheck.Margin = new System.Windows.Forms.Padding(2);
            this.grpCardMachineQualityCheck.Name = "grpCardMachineQualityCheck";
            this.grpCardMachineQualityCheck.Padding = new System.Windows.Forms.Padding(2);
            this.grpCardMachineQualityCheck.Size = new System.Drawing.Size(200, 592);
            this.grpCardMachineQualityCheck.TabIndex = 20;
            this.grpCardMachineQualityCheck.TabStop = false;
            this.grpCardMachineQualityCheck.Text = "Quality Check Results by Card";
            this.grpCardMachineQualityCheck.Visible = false;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(5, 20);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(190, 50);
            this.button13.TabIndex = 14;
            this.button13.Text = "Card 1 Avg";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Visible = false;
            // 
            // grpKPIType
            // 
            this.grpKPIType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grpKPIType.Controls.Add(this.rbtnCommercial);
            this.grpKPIType.Controls.Add(this.rbtnQuality);
            this.grpKPIType.Controls.Add(this.rbtnProduction);
            this.grpKPIType.Location = new System.Drawing.Point(3, 4);
            this.grpKPIType.Name = "grpKPIType";
            this.grpKPIType.Size = new System.Drawing.Size(151, 95);
            this.grpKPIType.TabIndex = 7;
            this.grpKPIType.TabStop = false;
            // 
            // rbtnCommercial
            // 
            this.rbtnCommercial.AutoSize = true;
            this.rbtnCommercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCommercial.Location = new System.Drawing.Point(24, 68);
            this.rbtnCommercial.Name = "rbtnCommercial";
            this.rbtnCommercial.Size = new System.Drawing.Size(99, 21);
            this.rbtnCommercial.TabIndex = 2;
            this.rbtnCommercial.Text = "Commercial";
            this.rbtnCommercial.UseVisualStyleBackColor = true;
            // 
            // rbtnQuality
            // 
            this.rbtnQuality.AutoSize = true;
            this.rbtnQuality.Checked = true;
            this.rbtnQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnQuality.Location = new System.Drawing.Point(24, 39);
            this.rbtnQuality.Name = "rbtnQuality";
            this.rbtnQuality.Size = new System.Drawing.Size(70, 21);
            this.rbtnQuality.TabIndex = 1;
            this.rbtnQuality.TabStop = true;
            this.rbtnQuality.Text = "Quality";
            this.rbtnQuality.UseVisualStyleBackColor = true;
            this.rbtnQuality.CheckedChanged += new System.EventHandler(this.rbtnQuality_CheckedChanged);
            // 
            // rbtnProduction
            // 
            this.rbtnProduction.AutoSize = true;
            this.rbtnProduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnProduction.Location = new System.Drawing.Point(23, 10);
            this.rbtnProduction.Name = "rbtnProduction";
            this.rbtnProduction.Size = new System.Drawing.Size(94, 21);
            this.rbtnProduction.TabIndex = 0;
            this.rbtnProduction.Text = "Production";
            this.rbtnProduction.UseVisualStyleBackColor = true;
            // 
            // backgroundWorkerKPI
            // 
            this.backgroundWorkerKPI.WorkerReportsProgress = true;
            // 
            // grpKPIQualityContainer
            // 
            this.grpKPIQualityContainer.Controls.Add(this.grpCardMachineQualityCheck);
            this.grpKPIQualityContainer.Controls.Add(this.dgvByMachineByGradeQA);
            this.grpKPIQualityContainer.Controls.Add(this.lblKPIDescription);
            this.grpKPIQualityContainer.Controls.Add(this.lblKPI);
            this.grpKPIQualityContainer.Controls.Add(this.lblQualityKPI);
            this.grpKPIQualityContainer.Controls.Add(this.tvwKPIQuality);
            this.grpKPIQualityContainer.Location = new System.Drawing.Point(5, 104);
            this.grpKPIQualityContainer.Name = "grpKPIQualityContainer";
            this.grpKPIQualityContainer.Size = new System.Drawing.Size(1025, 695);
            this.grpKPIQualityContainer.TabIndex = 12;
            this.grpKPIQualityContainer.TabStop = false;
            // 
            // dgvByMachineByGradeQA
            // 
            this.dgvByMachineByGradeQA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvByMachineByGradeQA.Location = new System.Drawing.Point(255, 108);
            this.dgvByMachineByGradeQA.Name = "dgvByMachineByGradeQA";
            this.dgvByMachineByGradeQA.Size = new System.Drawing.Size(760, 582);
            this.dgvByMachineByGradeQA.TabIndex = 4;
            this.dgvByMachineByGradeQA.Visible = false;
            // 
            // lblKPIDescription
            // 
            this.lblKPIDescription.AutoSize = true;
            this.lblKPIDescription.Location = new System.Drawing.Point(265, 83);
            this.lblKPIDescription.Name = "lblKPIDescription";
            this.lblKPIDescription.Size = new System.Drawing.Size(87, 13);
            this.lblKPIDescription.TabIndex = 3;
            this.lblKPIDescription.Text = "lblKPIDescription";
            this.lblKPIDescription.Visible = false;
            // 
            // lblKPI
            // 
            this.lblKPI.AutoSize = true;
            this.lblKPI.Location = new System.Drawing.Point(265, 73);
            this.lblKPI.Name = "lblKPI";
            this.lblKPI.Size = new System.Drawing.Size(34, 13);
            this.lblKPI.TabIndex = 2;
            this.lblKPI.Text = "lblKPI";
            this.lblKPI.Visible = false;
            // 
            // lblQualityKPI
            // 
            this.lblQualityKPI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblQualityKPI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblQualityKPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQualityKPI.Location = new System.Drawing.Point(520, 13);
            this.lblQualityKPI.Name = "lblQualityKPI";
            this.lblQualityKPI.Size = new System.Drawing.Size(188, 57);
            this.lblQualityKPI.TabIndex = 1;
            this.lblQualityKPI.Text = "Spinning";
            this.lblQualityKPI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tvwKPIQuality
            // 
            this.tvwKPIQuality.Location = new System.Drawing.Point(5, 13);
            this.tvwKPIQuality.Name = "tvwKPIQuality";
            this.tvwKPIQuality.Size = new System.Drawing.Size(245, 677);
            this.tvwKPIQuality.TabIndex = 0;
            this.tvwKPIQuality.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwKPIQuality_AfterSelect);
            // 
            // frmKPIQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 801);
            this.Controls.Add(this.grpKPIQualityContainer);
            this.Controls.Add(this.grpKPIType);
            this.Controls.Add(this.grKPIHeader);
            this.Controls.Add(this.grpbxDatePicker);
            this.Controls.Add(this.grpbxSpinning);
            this.Name = "frmKPIQuality";
            this.Text = "Key Perfomance Indexes - Quality";
            this.grpbxDatePicker.ResumeLayout(false);
            this.grpbxDatePicker.PerformLayout();
            this.grpbxSpinning.ResumeLayout(false);
            this.grpbxSpinning.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.grKPIHeader.ResumeLayout(false);
            this.grKPIHeader.PerformLayout();
            this.grpCardMachineQualityCheck.ResumeLayout(false);
            this.grpKPIType.ResumeLayout(false);
            this.grpKPIType.PerformLayout();
            this.grpKPIQualityContainer.ResumeLayout(false);
            this.grpKPIQualityContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvByMachineByGradeQA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxDatePicker;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox grpbxSpinning;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSpinning;
        private System.Windows.Forms.GroupBox grKPIHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDateToKPI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateFromKPI;
        private System.Windows.Forms.GroupBox grpCardMachineQualityCheck;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.GroupBox grpKPIType;
        private System.Windows.Forms.RadioButton rbtnCommercial;
        private System.Windows.Forms.RadioButton rbtnQuality;
        private System.Windows.Forms.RadioButton rbtnProduction;
        private System.Windows.Forms.ProgressBar prgBarKPI;
        private System.ComponentModel.BackgroundWorker backgroundWorkerKPI;
        private System.Windows.Forms.Label lblDataLoading;
        private System.Windows.Forms.GroupBox grpKPIQualityContainer;
        private System.Windows.Forms.TreeView tvwKPIQuality;
        private System.Windows.Forms.Label lblKPI;
        private System.Windows.Forms.Label lblQualityKPI;
        private System.Windows.Forms.Label lblKPIDescription;
        private System.Windows.Forms.DataGridView dgvByMachineByGradeQA;
    }
}