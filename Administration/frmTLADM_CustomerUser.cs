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
namespace Administration
{
    public partial class frmTLADM_CustomerUser : Form
    {
        bool FormLoaded;

        int Customer_Fk;
        DataGridViewTextBoxColumn oTxtBoxA;  // Pk User Access File 
        DataGridViewTextBoxColumn oTxtBoxB;  // Pk User Namer
        DataGridViewCheckBoxColumn oChkA;

        public frmTLADM_CustomerUser(int Cust_Fk)
        {
            InitializeComponent();

            Customer_Fk = Cust_Fk;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.HeaderText = "User Details";
            oTxtBoxB.Width = 250;
            dataGridView1.Columns.Add(oTxtBoxB);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void frmTLADM_CustomerUser_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Users = context.TLSEC_UserAccess.Where(x => x.TLSECUA_External).OrderBy(x => x.TLSECUA_UserName).ToList();
                foreach (var User in Users)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = User.TLSECUA_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = User.TLSECUA_UserName;
                    var Exists = context.TLADM_CustomerAccess.Where(x => x.CustAcc_Customer_Fk == Customer_Fk && x.CustAcc_User_Fk == User.TLSECUA_Pk).FirstOrDefault();
                    if (Exists != null)
                        dataGridView1.Rows[index].Cells[1].Value = true;
                }

            }
            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        bool IsChecked = (bool)Row.Cells[1].Value;
                        int UserKey = (int)Row.Cells[0].Value;
                        var Existing = context.TLADM_CustomerAccess.Where(x => x.CustAcc_Customer_Fk == Customer_Fk && x.CustAcc_User_Fk == UserKey).FirstOrDefault();

                        if (Existing == null && IsChecked)
                        {
                            TLADM_CustomerAccess Access = new TLADM_CustomerAccess();
                            Access.CustAcc_Customer_Fk = Customer_Fk;
                            Access.CustAcc_User_Fk = UserKey;

                            context.TLADM_CustomerAccess.Add(Access);

                        }
                        else if(Existing != null && !IsChecked)
                        {
                            context.TLADM_CustomerAccess.Remove(Existing);
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
