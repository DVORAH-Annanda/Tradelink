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


namespace CustomerServices
{
    public partial class frmSeleEMailAddress : Form
    {
        Repository repo;
        CustomerServicesParameters QueryParms;
        public StringBuilder EMailSelected;

        public frmSeleEMailAddress()
        {
            InitializeComponent();
            repo = new Repository();

            this.cmboEMailAddress.CheckStateChanged += new System.EventHandler(this.cmboEMailAddress_CheckStateChanged);
        }

        private void frmSeleEMailAddress_Load(object sender, EventArgs e)
        {
            QueryParms = new CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {
                var Ua = context.TLSEC_UserAccess.Where(x => !(bool)x.TLSECUA_Discontinued && x.TLSUCUA_EmailAddress.Length != 0 ).OrderBy(x=>x.TLSECUA_UserName).ToList();
                foreach (var U in Ua)
                {
                    cmboEMailAddress.Items.Add(new CheckComboBoxItem(U.TLSECUA_Pk, U.TLSECUA_UserName, false));
                }
            }

        }

        private void cmboEMailAddress_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CheckComboBoxItem)
            {
                CheckComboBoxItem item = (CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.UserAccesses.Add(repo.LoadUserAccess(item._Pk));

                }
                else
                {
                    var value = QueryParms.UserAccesses.Find(it => it.TLSECUA_Pk == item._Pk);
                    if (value != null)
                        QueryParms.UserAccesses.Remove(value);

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                EMailSelected = new StringBuilder();
                int Cnt = -1;
                using (var context = new TTI2Entities())
                {
                    foreach (var Ua in QueryParms.UserAccesses)
                    {
                        var UA = context.TLSEC_UserAccess.Find(Ua.TLSECUA_Pk);
                        if(UA != null)
                        {
                            if (UA.TLSUCUA_EmailAddress == null)
                            {
                                MessageBox.Show("This user " + UA.TLSECUA_UserName + " does not have a valid email id set ", " Please contact Project Managerment",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                            if(++Cnt < QueryParms.UserAccesses.Count && QueryParms.UserAccesses.Count > 1) 
                                EMailSelected.Append(Ua.TLSUCUA_EmailAddress.TrimEnd() + ";");
                            else
                                EMailSelected.Append(Ua.TLSUCUA_EmailAddress);
                        }
                    }
                }

                this.Close();
            }
        }

        private void cmboEMailAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }
    }
}
