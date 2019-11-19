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

namespace Knitting
{
    public partial class frmStoreToStoreTransfer : Form
    {
        bool FormLoaded;
        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();

        public frmStoreToStoreTransfer()
        {
            InitializeComponent();
            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = true;
            oChkA.ValueType = typeof(Boolean);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.Visible = true;
            oTxtB.HeaderText = "Pallet No";
            oTxtB.ReadOnly = true;
            oTxtB.ValueType = typeof(int);
            oTxtB.Width = 100;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.Visible = true;
            oTxtC.HeaderText = "TradeLink Pallet No";
            oTxtC.ReadOnly = true;
            oTxtC.ValueType = typeof(string);
            oTxtC.Width = 150;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.Visible = true;
            oTxtD.HeaderText = "Yarn Type";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(string);
            oTxtD.Width = 150;
            dataGridView1.Columns.Add(oTxtD);


            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.Visible = true;
            oTxtE.HeaderText = "Yarn Order";
            oTxtE.ReadOnly = true;
            oTxtE.ValueType = typeof(string);
            oTxtE.Width = 100;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.Visible = true;
            oTxtF.HeaderText = "Nett Weight";
            oTxtF.ReadOnly = true;
            oTxtF.ValueType = typeof(decimal);
            oTxtF.Width = 100;
            dataGridView1.Columns.Add(oTxtF);
           
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            core = new Util();

        }

        private void frmStoreToStoreTransfer_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.FirstOrDefault(x=>x.Dep_ShortCode == "KNIT");
                if (Dept != null)
                {
                    comboFromStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    comboFromStore.ValueMember = "WhStore_Id";
                    comboFromStore.DisplayMember = "WhStore_Description";
                    comboFromStore.SelectedValue = -1;

                    comboToStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    comboToStore.ValueMember = "WhStore_Id";
                    comboToStore.DisplayMember = "WhStore_Description";
                    comboToStore.SelectedValue = -1;
                }
            }
            FormLoaded = true;
        }

        private void comboFromStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var SelectedItem = (TLADM_WhseStore)oCmbo.SelectedItem;
                if (SelectedItem != null)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_Store_FK == SelectedItem.WhStore_Id  && !x.TLKNIOP_PalletAllocated).ToList();
                        foreach (var Pallet in Pallets)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;
                            dataGridView1.Rows[index].Cells[3].Value = Pallet.TLKNIOP_TLPalletNo;
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Yarn.Find(Pallet.TLKNIOP_YarnType_FK).YA_Description;
                            if (Pallet.TLKNIOP_YarnOrder_FK != null)
                            {
                                var YarnOrder = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK);
                                if (YarnOrder != null)
                                {
                                    dataGridView1.Rows[index].Cells[5].Value = YarnOrder.YarnO_OrderNumber;
                                }
                            }

                            dataGridView1.Rows[index].Cells[6].Value = core.CalculatePalletNett(Pallet);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            DataGridView oDgv = dataGridView1;

            if (oBtn != null && FormLoaded)
            {
                var ToStore = (TLADM_WhseStore)comboToStore.SelectedItem;
                if (ToStore == null)
                {
                    MessageBox.Show("Please select a store to transfer to");
                    return;
                }

                var FromStore = (TLADM_WhseStore)comboFromStore.SelectedItem;
                if (FromStore.WhStore_Id == ToStore.WhStore_Id)
                {
                    MessageBox.Show("Please select an alternative store to transfer to");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (!(bool)Row.Cells[1].Value)
                            continue;

                        var Pallet_Pk = (int)Row.Cells[0].Value;

                        var Pallet = context.TLKNI_YarnOrderPallets.Find(Pallet_Pk);
                        if (Pallet != null)
                            Pallet.TLKNIOP_Store_FK = ToStore.WhStore_Id;
                    }

                    try
                    {
                        context.SaveChanges();
                        dataGridView1.Rows.Clear();
                        MessageBox.Show("Data successfuly saved to the database");
                        this.dataGridView1.Rows.Clear();
                        frmStoreToStoreTransfer_Load(this, null);
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
