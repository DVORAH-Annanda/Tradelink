namespace Cutting
{
    partial class FrmBierriebiSel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboReportOptions = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMachines = new Cutting.CheckComboBox();
            this.comboOperator = new Cutting.CheckComboBox();
            this.comboCutSheets = new Cutting.CheckComboBox();
            this.comboGreige = new Cutting.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quality";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cut Sheets";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Operator";
            // 
            // comboReportOptions
            // 
            this.comboReportOptions.FormattingEnabled = true;
            this.comboReportOptions.Location = new System.Drawing.Point(204, 316);
            this.comboReportOptions.Name = "comboReportOptions";
            this.comboReportOptions.Size = new System.Drawing.Size(209, 21);
            this.comboReportOptions.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(124, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sort Options";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Machines";
            // 
            // comboMachines
            // 
            this.comboMachines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboMachines.FormattingEnabled = true;
            this.comboMachines.Location = new System.Drawing.Point(204, 221);
            this.comboMachines.Name = "comboMachines";
            this.comboMachines.Size = new System.Drawing.Size(209, 21);
            this.comboMachines.TabIndex = 12;
            this.comboMachines.Text = "Select Options";
            // 
            // comboOperator
            // 
            this.comboOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboOperator.FormattingEnabled = true;
            this.comboOperator.Location = new System.Drawing.Point(204, 168);
            this.comboOperator.Name = "comboOperator";
            this.comboOperator.Size = new System.Drawing.Size(209, 21);
            this.comboOperator.TabIndex = 7;
            this.comboOperator.Text = "Select Options";
            // 
            // comboCutSheets
            // 
            this.comboCutSheets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCutSheets.FormattingEnabled = true;
            this.comboCutSheets.Location = new System.Drawing.Point(204, 115);
            this.comboCutSheets.Name = "comboCutSheets";
            this.comboCutSheets.Size = new System.Drawing.Size(209, 21);
            this.comboCutSheets.TabIndex = 3;
            this.comboCutSheets.Text = "Select Options";
            // 
            // comboGreige
            // 
            this.comboGreige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboGreige.FormattingEnabled = true;
            this.comboGreige.Location = new System.Drawing.Point(204, 62);
            this.comboGreige.Name = "comboGreige";
            this.comboGreige.Size = new System.Drawing.Size(209, 21);
            this.comboGreige.TabIndex = 1;
            this.comboGreige.Text = "Select Options";
            // 
            // FrmBierriebiSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 460);
            this.Controls.Add(this.comboMachines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboReportOptions);
            this.Controls.Add(this.comboOperator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboCutSheets);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboGreige);
            this.Controls.Add(this.label1);
            this.Name = "FrmBierriebiSel";
            this.Text = "Bierriebi Report Selection";
            this.Load += new System.EventHandler(this.FrmBierriebiSel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;

        /*private System.Windows.Forms.ComboBox comboDyeBatch;
        private System.Windows.Forms.ComboBox cmboOperator;
        private System.Windows.Forms.ComboBox cmboGreige;
        private System.Windows.Forms.ComboBox cmboColour; */
        private Cutting.CheckComboBox comboOperator;
        private Cutting.CheckComboBox comboGreige;
        private Cutting.CheckComboBox comboCutSheets;
        private Cutting.CheckComboBox comboMachines;
        private System.Windows.Forms.ComboBox comboReportOptions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        //private System.Windows.Forms.ComboBox cmboMachines;
    }
}