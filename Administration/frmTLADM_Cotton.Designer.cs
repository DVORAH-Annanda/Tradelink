namespace TTI2_WF
{
    partial class frmTLADM_Cotton
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
            this.cmbCotton = new System.Windows.Forms.ComboBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbUOM = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStockType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbOrigin = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbShowQtyNo = new System.Windows.Forms.RadioButton();
            this.rbShowQtyYes = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbShowCostNo = new System.Windows.Forms.RadioButton();
            this.rbShowCostYes = new System.Windows.Forms.RadioButton();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRoq = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtStdCost = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.cmbCottonAgent = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnContracts = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCotton
            // 
            this.cmbCotton.FormattingEnabled = true;
            this.cmbCotton.Location = new System.Drawing.Point(261, 12);
            this.cmbCotton.Name = "cmbCotton";
            this.cmbCotton.Size = new System.Drawing.Size(249, 21);
            this.cmbCotton.TabIndex = 0;
            this.cmbCotton.SelectedIndexChanged += new System.EventHandler(this.cmbCotton_SelectedIndexChanged);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(87, 55);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            this.txtCode.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(366, 55);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(320, 20);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // cmbUOM
            // 
            this.cmbUOM.FormattingEnabled = true;
            this.cmbUOM.Location = new System.Drawing.Point(87, 99);
            this.cmbUOM.Name = "cmbUOM";
            this.cmbUOM.Size = new System.Drawing.Size(139, 21);
            this.cmbUOM.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "UOM";
            // 
            // cmbStockType
            // 
            this.cmbStockType.FormattingEnabled = true;
            this.cmbStockType.Location = new System.Drawing.Point(329, 98);
            this.cmbStockType.Name = "cmbStockType";
            this.cmbStockType.Size = new System.Drawing.Size(140, 21);
            this.cmbStockType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stock Type";
            // 
            // cmbStore
            // 
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(565, 98);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(121, 21);
            this.cmbStore.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(515, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Store";
            // 
            // cmbOrigin
            // 
            this.cmbOrigin.FormattingEnabled = true;
            this.cmbOrigin.Location = new System.Drawing.Point(87, 141);
            this.cmbOrigin.Name = "cmbOrigin";
            this.cmbOrigin.Size = new System.Drawing.Size(139, 21);
            this.cmbOrigin.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Origin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbShowQtyNo);
            this.groupBox1.Controls.Add(this.rbShowQtyYes);
            this.groupBox1.Location = new System.Drawing.Point(87, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 67);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Qtys";
            // 
            // rbShowQtyNo
            // 
            this.rbShowQtyNo.AutoSize = true;
            this.rbShowQtyNo.Location = new System.Drawing.Point(6, 42);
            this.rbShowQtyNo.Name = "rbShowQtyNo";
            this.rbShowQtyNo.Size = new System.Drawing.Size(39, 17);
            this.rbShowQtyNo.TabIndex = 1;
            this.rbShowQtyNo.TabStop = true;
            this.rbShowQtyNo.Text = "No";
            this.rbShowQtyNo.UseVisualStyleBackColor = true;
            // 
            // rbShowQtyYes
            // 
            this.rbShowQtyYes.AutoSize = true;
            this.rbShowQtyYes.Location = new System.Drawing.Point(6, 19);
            this.rbShowQtyYes.Name = "rbShowQtyYes";
            this.rbShowQtyYes.Size = new System.Drawing.Size(43, 17);
            this.rbShowQtyYes.TabIndex = 0;
            this.rbShowQtyYes.TabStop = true;
            this.rbShowQtyYes.Text = "Yes";
            this.rbShowQtyYes.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbShowCostNo);
            this.groupBox2.Controls.Add(this.rbShowCostYes);
            this.groupBox2.Location = new System.Drawing.Point(210, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 67);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Std Cost";
            // 
            // rbShowCostNo
            // 
            this.rbShowCostNo.AutoSize = true;
            this.rbShowCostNo.Location = new System.Drawing.Point(6, 42);
            this.rbShowCostNo.Name = "rbShowCostNo";
            this.rbShowCostNo.Size = new System.Drawing.Size(39, 17);
            this.rbShowCostNo.TabIndex = 15;
            this.rbShowCostNo.TabStop = true;
            this.rbShowCostNo.Text = "No";
            this.rbShowCostNo.UseVisualStyleBackColor = true;
            // 
            // rbShowCostYes
            // 
            this.rbShowCostYes.AutoSize = true;
            this.rbShowCostYes.Location = new System.Drawing.Point(6, 19);
            this.rbShowCostYes.Name = "rbShowCostYes";
            this.rbShowCostYes.Size = new System.Drawing.Size(43, 17);
            this.rbShowCostYes.TabIndex = 0;
            this.rbShowCostYes.TabStop = true;
            this.rbShowCostYes.Text = "Yes";
            this.rbShowCostYes.UseVisualStyleBackColor = true;
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(412, 303);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(100, 20);
            this.txtRol.TabIndex = 17;
            this.txtRol.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(349, 310);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "ROL";
            // 
            // txtRoq
            // 
            this.txtRoq.Location = new System.Drawing.Point(598, 303);
            this.txtRoq.Name = "txtRoq";
            this.txtRoq.Size = new System.Drawing.Size(100, 20);
            this.txtRoq.TabIndex = 19;
            this.txtRoq.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(548, 310);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "ROQ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(86, 310);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Contact";
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(139, 307);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(138, 20);
            this.txtContact.TabIndex = 22;
            this.txtContact.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // txtStdCost
            // 
            this.txtStdCost.Location = new System.Drawing.Point(216, 276);
            this.txtStdCost.Name = "txtStdCost";
            this.txtStdCost.Size = new System.Drawing.Size(100, 20);
            this.txtStdCost.TabIndex = 23;
            this.txtStdCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStdCost.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(216, 253);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Std Cost";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(93, 253);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 25;
            this.label15.Text = "Units";
            // 
            // txtUnits
            // 
            this.txtUnits.Location = new System.Drawing.Point(89, 276);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(100, 20);
            this.txtUnits.TabIndex = 26;
            this.txtUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnits.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // rtbNotes
            // 
            this.rtbNotes.Location = new System.Drawing.Point(89, 333);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(271, 109);
            this.rtbNotes.TabIndex = 27;
            this.rtbNotes.Text = "";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(623, 382);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 28;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(623, 419);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(117, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "Please make a selection";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(34, 336);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 31;
            this.label17.Text = "Notes";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(376, 355);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Grade";
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(429, 352);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(118, 20);
            this.txtGrade.TabIndex = 33;
            this.txtGrade.TextChanged += new System.EventHandler(this.Cotton_TextChanged);
            // 
            // cmbCottonAgent
            // 
            this.cmbCottonAgent.FormattingEnabled = true;
            this.cmbCottonAgent.Location = new System.Drawing.Point(412, 141);
            this.cmbCottonAgent.Name = "cmbCottonAgent";
            this.cmbCottonAgent.Size = new System.Drawing.Size(150, 21);
            this.cmbCottonAgent.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(326, 148);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Cotton Agent";
            // 
            // btnContracts
            // 
            this.btnContracts.Location = new System.Drawing.Point(522, 382);
            this.btnContracts.Name = "btnContracts";
            this.btnContracts.Size = new System.Drawing.Size(75, 23);
            this.btnContracts.TabIndex = 36;
            this.btnContracts.Text = "Contracts";
            this.btnContracts.UseVisualStyleBackColor = true;
            this.btnContracts.Click += new System.EventHandler(this.btnContracts_Click);
            // 
            // frmTLADM_Cotton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 473);
            this.Controls.Add(this.btnContracts);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cmbCottonAgent);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.rtbNotes);
            this.Controls.Add(this.txtUnits);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtStdCost);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRoq);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbOrigin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStockType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUOM);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.cmbCotton);
            this.Name = "frmTLADM_Cotton";
            this.Text = "Cotton Contracts Update / Edit";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCotton;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cmbUOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStockType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOrigin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbShowQtyNo;
        private System.Windows.Forms.RadioButton rbShowQtyYes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbShowCostNo;
        private System.Windows.Forms.RadioButton rbShowCostYes;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRoq;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtStdCost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.ComboBox cmbCottonAgent;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnContracts;
    }
}