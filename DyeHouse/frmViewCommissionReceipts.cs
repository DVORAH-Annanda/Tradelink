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

namespace DyeHouse
{
    public partial class frmViewCommissionReceipts : Form
    {
        bool FormLoaded;

        DyeRepository repo;
        DyeQueryParameters DyeParameters;
        DataGridViewTextBoxColumn  oTxtBoxA = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxE = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxF = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtBoxG = new DataGridViewTextBoxColumn();

        public frmViewCommissionReceipts()
        {
            InitializeComponent();

            oTxtBoxA.HeaderText = "Grn No";
            oTxtBoxA.ValueType = typeof(string);
            oTxtBoxA.ReadOnly = true;

            oTxtBoxB.HeaderText = "Grn Date";
            oTxtBoxB.ValueType = typeof(DateTime);
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC.HeaderText = "Cust Doc";
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD.HeaderText = "Quality";
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE.HeaderText = "Cust  Order";
            oTxtBoxE.ValueType = typeof(string);
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF.HeaderText = "Total Items";
            oTxtBoxF.ValueType = typeof(int);
            oTxtBoxF.ReadOnly = true;

            oTxtBoxG.HeaderText = "Nett Weight";
            oTxtBoxG.ValueType = typeof(decimal);
            oTxtBoxG.ReadOnly = true;

            repo = new DyeRepository();

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oTxtBoxE);
            dataGridView1.Columns.Add(oTxtBoxF);
            dataGridView1.Columns.Add(oTxtBoxG);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            this.comboCustomers.CheckStateChanged += new EventHandler(this.comboCustomers_CheckStateChanged);
            this.comboQuality.CheckStateChanged += new System.EventHandler(this.comboQuality_CheckStateChanged);
        }

        private void frmViewCommissionReceipts_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            DyeParameters = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.Where(x=>x.Cust_CommissionCust).OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    comboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Qualitys = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Quality in Qualitys)
                {
                    comboQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }
            }
            FormLoaded = true;
        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Customers.Add(repo.LoadCustomer(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        DyeParameters.Customers.Remove(value);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Qualities.Add(repo.LoadQuality(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Qualities.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                dataGridView1.Rows.Clear();

                DyeParameters.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DyeParameters.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                DyeParameters.ToDate = DyeParameters.ToDate.AddHours(23);

                var CommTransGrps = repo.CommissionTransactions(DyeParameters).GroupBy(x => x.GreigeCom_GrnNo);
                using ( var context = new TTI2Entities())
                {
                    foreach (var Grp in CommTransGrps)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Grp.FirstOrDefault().GreigeCom_GrnNo.ToString();
                        dataGridView1.Rows[index].Cells[1].Value = Grp.FirstOrDefault().GreigeCom_Transdate;
                        dataGridView1.Rows[index].Cells[2].Value = Grp.FirstOrDefault().GreigeCom_Custdoc;
                        var Pk = Grp.FirstOrDefault().GreigeCom_ProductType_FK;
                        dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(Pk).TLGreige_Description;
                        dataGridView1.Rows[index].Cells[4].Value = Grp.FirstOrDefault().GreigeCom_Custdoc;
                        dataGridView1.Rows[index].Cells[5].Value = Grp.Count();
                        dataGridView1.Rows[index].Cells[6].Value = Grp.Sum(x=> x.GreigeCom_NettWeight);
                        
                    }
                }

                comboCustomers.Items.Clear();
                comboQuality.Items.Clear();

                frmViewCommissionReceipts_Load(this, null);
            }
        }
    }
}
