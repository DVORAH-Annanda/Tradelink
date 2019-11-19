namespace Administration
{
    partial class frmTLADMCottonAgents
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
            this.cmbAgents = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAgentName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbAddress = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAgentContactP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTelephoneDetails = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cotton Agents";
            // 
            // cmbAgents
            // 
            this.cmbAgents.FormattingEnabled = true;
            this.cmbAgents.Location = new System.Drawing.Point(230, 28);
            this.cmbAgents.Name = "cmbAgents";
            this.cmbAgents.Size = new System.Drawing.Size(207, 21);
            this.cmbAgents.TabIndex = 1;
            this.cmbAgents.SelectedIndexChanged += new System.EventHandler(this.cmbAgents_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(507, 320);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(507, 355);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Agent Name";
            // 
            // txtAgentName
            // 
            this.txtAgentName.Location = new System.Drawing.Point(230, 82);
            this.txtAgentName.Name = "txtAgentName";
            this.txtAgentName.Size = new System.Drawing.Size(207, 20);
            this.txtAgentName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Agent Address";
            // 
            // rtbAddress
            // 
            this.rtbAddress.Location = new System.Drawing.Point(230, 139);
            this.rtbAddress.Name = "rtbAddress";
            this.rtbAddress.Size = new System.Drawing.Size(207, 78);
            this.rtbAddress.TabIndex = 7;
            this.rtbAddress.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Agent Contact Person";
            // 
            // txtAgentContactP
            // 
            this.txtAgentContactP.Location = new System.Drawing.Point(230, 248);
            this.txtAgentContactP.Name = "txtAgentContactP";
            this.txtAgentContactP.Size = new System.Drawing.Size(207, 20);
            this.txtAgentContactP.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Agent Telephone No";
            // 
            // txtTelephoneDetails
            // 
            this.txtTelephoneDetails.Location = new System.Drawing.Point(230, 294);
            this.txtTelephoneDetails.Name = "txtTelephoneDetails";
            this.txtTelephoneDetails.Size = new System.Drawing.Size(149, 20);
            this.txtTelephoneDetails.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(102, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Agent Email Details";
            // 
            // txtEmailDetails
            // 
            this.txtEmailDetails.Location = new System.Drawing.Point(230, 340);
            this.txtEmailDetails.Name = "txtEmailDetails";
            this.txtEmailDetails.Size = new System.Drawing.Size(207, 20);
            this.txtEmailDetails.TabIndex = 13;
            // 
            // frmTLADMCottonAgents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 395);
            this.Controls.Add(this.txtEmailDetails);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTelephoneDetails);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAgentContactP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtbAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAgentName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cmbAgents);
            this.Controls.Add(this.label1);
            this.Name = "frmTLADMCottonAgents";
            this.Text = "Cotton Agents Update / Edit ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAgents;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAgentName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAgentContactP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTelephoneDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmailDetails;
    }
}