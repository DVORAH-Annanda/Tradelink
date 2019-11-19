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

namespace Spinning
{
    public partial class frmCottonWaste : Form
    {
        Util core;
        bool formloaded;

        public frmCottonWaste()
        {
            InitializeComponent();
            
            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();

            oTxtA.HeaderText = "Pk";
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtB.HeaderText = "Bale Number";
            oTxtB.ValueType = typeof(String);
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Bale Gross Weight";
            oTxtC.ValueType = typeof(decimal);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Bale Nett Weight";
            oTxtD.ValueType = typeof(decimal);
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            
            core = new Util();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;

        
            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(1);
                if(LNU != null)
                    txtTransactionNumber.Text = LNU.col13.ToString();


                cmboCustomerFile.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomerFile.DisplayMember = "Cust_Description";
                cmboCustomerFile.ValueMember = "Cust_Pk";


                var Existing = context.TLSPN_YarnWaste.Where(x => !x.TLYW_Disposed).ToList();
                foreach (var row in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = row.TLYW_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = row.TLYW_BaleNo;
                    dataGridView1.Rows[index].Cells[3].Value = row.TLYW_BaleGrossWeight;
                    dataGridView1.Rows[index].Cells[4].Value = row.TLYW_BaleNettWeight;

                }

            }
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var cust = (TLADM_CustomerFile)cmboCustomerFile.SelectedItem;
                if (cust == null)
                {
                    MessageBox.Show("Please select a customer number");
                    return;
                }
                using (var context = new TTI2Entities())
                {
                    var LNU = context.TLADM_LastNumberUsed.Find(1);
                    

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == bool.FalseString)
                            continue;

                        TLSPN_YarnWaste yw = new TLSPN_YarnWaste();
                        var index = (int)row.Cells[0].Value;
                        yw = context.TLSPN_YarnWaste.Find(index);
                        if (yw != null)
                        {
                            yw.TLYW_Customer_FK = cust.Cust_Pk;
                            yw.TLYW_DateDisposed = dtpSales.Value;
                            yw.TLYW_SalesTransactionNO = LNU.col13;
                            yw.TLYW_Disposed = true;
                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("SPIN")).FirstOrDefault();
                            if (Dept != null)
                            {
                                var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1300).FirstOrDefault();
                                if (TranType != null)
                                {
                                    yw.TLYW_TransactionType_Out = TranType.TrxT_Pk;
                                }
                            }
                        }
                    }

                    try
                    {
                        LNU.col13 += 1;

                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        frmViewReport vRep = new frmViewReport(21, LNU.col13 - 1);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

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
