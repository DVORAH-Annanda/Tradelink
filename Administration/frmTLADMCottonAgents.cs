using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using TTI2_WF;

namespace Administration
{
    public partial class frmTLADMCottonAgents : Form
    {
        bool formloaded;
        bool addrec; 
        public frmTLADMCottonAgents()
        {
            Util core = new Util();

            InitializeComponent();
            
            txtTelephoneDetails.KeyPress += core.txtWin_KeyPress;
            txtTelephoneDetails.KeyDown += core.txtWin_KeyDown;

            SetUp();
        }

        void SetUp()
        {
            
            formloaded = false;
            addrec = true;

            txtAgentContactP.Text    = string.Empty;
            txtAgentName.Text        = string.Empty;
            txtEmailDetails.Text     = string.Empty;
            txtTelephoneDetails.Text = string.Empty;
          
        
            rtbAddress.Text = string.Empty;

            using (var context = new TTI2Entities())
            {
                cmbAgents.DataSource = context.TLADM_CottonAgent.OrderBy(x => x.CottonAgent_Description).ToList();
                cmbAgents.ValueMember = "CottonAgent_Pk";
                cmbAgents.DisplayMember = "CottonAgent_Description";
            }

            formloaded = true;
        }

        private void cmbAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var CottonAgent = (TLADM_CottonAgent)cmbAgents.SelectedItem;
                if (CottonAgent != null)
                {
                    txtAgentContactP.Text = CottonAgent.CottonAgent_Contact;
                    txtAgentName.Text = CottonAgent.CottonAgent_Description;
                    txtEmailDetails.Text = CottonAgent.CottonAgent_Email;
                    txtTelephoneDetails.Text = CottonAgent.CottonAgent_PhoneNo.ToString();

                    rtbAddress.Text = CottonAgent.CottonAgent_Address;

                    addrec = false;

                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                SetUp();
                addrec = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Util core = new Util();

            if (oBtn != null && formloaded)
            {
                if (String.IsNullOrEmpty(txtAgentName.Text))
                {
                    MessageBox.Show("Please enter the agents name in the text box provided");
                    return;
                }

                if (String.IsNullOrEmpty(rtbAddress.Text))
                {
                    MessageBox.Show("Please enter the agents address in the text box provided");
                }

                TLADM_CottonAgent coagents = new TLADM_CottonAgent();

                using (var context = new TTI2Entities())
                {
                    if (!addrec)
                    {
                        var AgentSelected = (TLADM_CottonAgent)cmbAgents.SelectedItem;
                        
                        if (AgentSelected == null)
                        {
                            MessageBox.Show("Please select an agent from the drop down list provided");
                            return;
                        }
                        coagents = context.TLADM_CottonAgent.Find(AgentSelected.CottonAgent_Pk);
                    }

                    coagents.CottonAgent_Contact = txtAgentContactP.Text;
                    coagents.CottonAgent_Address = rtbAddress.Text;
                    coagents.CottonAgent_Description = txtAgentName.Text;
                    coagents.CottonAgent_Email = txtEmailDetails.Text;
                    if(!String.IsNullOrEmpty(txtTelephoneDetails.Text) && core.IsValueDidgit(txtTelephoneDetails.Text))
                        coagents.CottonAgent_PhoneNo = Convert.ToInt32(txtTelephoneDetails.Text);

                    if (addrec)
                        context.TLADM_CottonAgent.Add(coagents);

                    try
                    {
                        context.SaveChanges();
                        formloaded = false;
                        cmbAgents.DataSource = context.TLADM_CottonAgent.OrderBy(x => x.CottonAgent_Description).ToList();
                        formloaded = true;
                        SetUp();
                        MessageBox.Show("Record stored to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
