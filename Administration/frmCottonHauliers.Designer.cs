namespace Administration
{
    partial class frmCottonHauliers
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
            this.cmbHauliers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHaulierNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHaulierName = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVehicles = new System.Windows.Forms.Button();
            this.rtbAddress = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHaulierContactPerson = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHaulierTelephone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmbHauliers
            // 
            this.cmbHauliers.FormattingEnabled = true;
            this.cmbHauliers.Location = new System.Drawing.Point(158, 12);
            this.cmbHauliers.Name = "cmbHauliers";
            this.cmbHauliers.Size = new System.Drawing.Size(232, 21);
            this.cmbHauliers.TabIndex = 0;
            this.cmbHauliers.SelectedIndexChanged += new System.EventHandler(this.cmbHauliers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cotton Hauliers";
            // 
            // txtHaulierNo
            // 
            this.txtHaulierNo.Location = new System.Drawing.Point(158, 59);
            this.txtHaulierNo.Name = "txtHaulierNo";
            this.txtHaulierNo.Size = new System.Drawing.Size(80, 20);
            this.txtHaulierNo.TabIndex = 2;
            this.txtHaulierNo.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Haulier No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Haulier Name";
            // 
            // txtHaulierName
            // 
            this.txtHaulierName.Location = new System.Drawing.Point(158, 105);
            this.txtHaulierName.Name = "txtHaulierName";
            this.txtHaulierName.Size = new System.Drawing.Size(232, 20);
            this.txtHaulierName.TabIndex = 5;
            this.txtHaulierName.TextChanged += new System.EventHandler(this.txtBox_ValueChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(544, 283);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(544, 317);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnVehicles
            // 
            this.btnVehicles.Location = new System.Drawing.Point(453, 317);
            this.btnVehicles.Name = "btnVehicles";
            this.btnVehicles.Size = new System.Drawing.Size(75, 23);
            this.btnVehicles.TabIndex = 8;
            this.btnVehicles.Text = "Vehicles";
            this.btnVehicles.UseVisualStyleBackColor = true;
            this.btnVehicles.Click += new System.EventHandler(this.btnVehicles_Click);
            // 
            // rtbAddress
            // 
            this.rtbAddress.Location = new System.Drawing.Point(158, 146);
            this.rtbAddress.Name = "rtbAddress";
            this.rtbAddress.Size = new System.Drawing.Size(182, 96);
            this.rtbAddress.TabIndex = 9;
            this.rtbAddress.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Contact Person";
            // 
            // txtHaulierContactPerson
            // 
            this.txtHaulierContactPerson.Location = new System.Drawing.Point(158, 253);
            this.txtHaulierContactPerson.Name = "txtHaulierContactPerson";
            this.txtHaulierContactPerson.Size = new System.Drawing.Size(232, 20);
            this.txtHaulierContactPerson.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Telephone No";
            // 
            // txtHaulierTelephone
            // 
            this.txtHaulierTelephone.Location = new System.Drawing.Point(158, 282);
            this.txtHaulierTelephone.Name = "txtHaulierTelephone";
            this.txtHaulierTelephone.Size = new System.Drawing.Size(232, 20);
            this.txtHaulierTelephone.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "EMail Address";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(155, 311);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(235, 20);
            this.txtEmailAddress.TabIndex = 16;
            // 
            // frmCottonHauliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 357);
            this.Controls.Add(this.txtEmailAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtHaulierTelephone);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtHaulierContactPerson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtbAddress);
            this.Controls.Add(this.btnVehicles);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtHaulierName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHaulierNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbHauliers);
            this.Name = "frmCottonHauliers";
            this.Text = "frmCottonHauliers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbHauliers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHaulierNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHaulierName;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnVehicles;
        private System.Windows.Forms.RichTextBox rtbAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHaulierContactPerson;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHaulierTelephone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmailAddress;
    }
}