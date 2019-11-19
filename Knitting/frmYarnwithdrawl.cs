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
    public partial class frmYarnwithdrawl : Form
    {
        // Note for the file....16th January, 2017 
        // cmboKnitOrders now contains refrerences to the Yarn Orders as opposed to KnitOrders
        //-------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA;    // 0 Pk
        DataGridViewCheckBoxColumn oChkA;   // 1 Select
        DataGridViewTextBoxColumn oTxtB;    // 2 PickSlip Number
        DataGridViewTextBoxColumn oTxtC;    // 3 Customer
        DataGridViewTextBoxColumn oTxtD;    // 4 WareHouse 
        DataGridViewTextBoxColumn oTxtE;    // 5 Created Date

        bool formloaded;
        public frmYarnwithdrawl()
        {
            InitializeComponent();
            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Selected";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.ReadOnly = true;
            oTxtB.HeaderText = "Pallet Number";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.ReadOnly = true;
            oTxtC.HeaderText = "Grade";
            oTxtC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.ReadOnly = true;
            oTxtD.HeaderText = "Nett Weight";
            oTxtD.ValueType = typeof(Decimal);
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.ReadOnly = true;
            oTxtE.HeaderText = "Created Date";
            oTxtE.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtE);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
         
        }

        private void frmYarnwithdrawl_Load(object sender, EventArgs e)
        {
            formloaded = false;
            dataGridView1.Rows.Clear();
            
            using (var context = new TTI2Entities())
            {
                cmboKnitOrder.DataSource = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).ToList();
                cmboKnitOrder.DisplayMember = "YarnO_OrderNumber";
                cmboKnitOrder.ValueMember = "YarnO_Pk";
                cmboKnitOrder.SelectedValue = 0;
            }
            formloaded = true;
        }

        private void cmboKnitOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var YO = (TLSPN_YarnOrder)oCmbo.SelectedItem;
                if (YO != null)
                {
                    dataGridView1.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var LNU = context.TLADM_LastNumberUsed.Find(2);
                        txtDelNo.Text = LNU.col6.ToString();

                        var YarnType = context.TLADM_Yarn.Find(YO.Yarno_YarnType_FK);
                        txtYarnType.Text = YarnType.YA_Description;

                        var Machine = context.TLADM_MachineDefinitions.Find(YO.Yarno_MachNo_FK);
                        txtMachine.Text = Machine.MD_MachineCode;

                        var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == YO.YarnO_Pk && x.YarnOP_YarnAvailable && !x.YarnOP_Issued).ToList();
                        foreach (var Row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Row.YarnOP_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Row.YarnOP_PalletNo;
                            dataGridView1.Rows[index].Cells[3].Value = Row.YarnOP_Grade;
                            dataGridView1.Rows[index].Cells[4].Value = Row.YarnOP_NettWeight;
                            dataGridView1.Rows[index].Cells[5].Value = Row.YarnOP_DatePacked.ToString();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLADM_TranactionType tranType = null;
            
            if (oBtn != null && formloaded)
            {
                var YO = (TLSPN_YarnOrder)cmboKnitOrder.SelectedItem;
                if (YO != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var deptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                        if (deptDetail != null)
                        {
                           tranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == deptDetail.Dep_Id && x.TrxT_Number == 800).FirstOrDefault();
                        }

                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if ((bool)Row.Cells[1].Value == false)
                            {
                                continue;
                            }

                            TLSPN_YarnTransactions YarnT = new TLSPN_YarnTransactions();
                            YarnT.YarnTrx_YarnOrder_FK = YO.YarnO_Pk;
                            YarnT.YarnTrx_SequenceNo = Convert.ToInt32(txtDelNo.Text);
                            YarnT.YarnTrx_Date = dateTimePicker1.Value;

                            if (tranType != null)
                            {
                                YarnT.YarnTrx_TranType_FK = tranType.TrxT_Pk;
                                YarnT.YarnTrx_FromDep_FK = tranType.TrxT_FromWhse_FK;
                                YarnT.YarnTrx_ToDep_FK = tranType.TrxT_ToWhse_FK;
                            }

                            var NettWeight = (Decimal)Row.Cells[4].Value;

                            context.TLSPN_YarnTransactions.Add(YarnT);

                            var index = (int)Row.Cells[0].Value;

                            var PalletStore = context.TLSPN_YarnOrderPallets.Find(index);
                            if (PalletStore != null)
                            {
                                PalletStore.YarnOP_NoOfConesSpun -= PalletStore.YarnOP_NoOfCones;
                                PalletStore.YarnOP_Issued = true;
                                PalletStore.YarnOP_DateDispatched = dateTimePicker1.Value;
                                PalletStore.YarnOP_Store_FK = (int)tranType.TrxT_ToWhse_FK;
                                PalletStore.YarnOP_YarnType_FK = YO.Yarno_YarnType_FK;
                                PalletStore.YarnOP_NettWeight = NettWeight;
                                PalletStore.YarnOP_ReservedBy = int.Parse(txtDelNo.Text);
                                TLKNI_YarnOrderPallets KnitYOP = new TLKNI_YarnOrderPallets();

                                KnitYOP.TLKNIOP_Cones = PalletStore.YarnOP_NoOfCones;
                                KnitYOP.TLKNIOP_PalletNo = PalletStore.YarnOP_PalletNo;
                                KnitYOP.TLKNIOP_NettWeight = NettWeight;
                                KnitYOP.TLKNIOP_YarnOrder_FK = YO.YarnO_Pk;
                                KnitYOP.TLKNIOP_YarnType_FK = YO.Yarno_YarnType_FK;
                                KnitYOP.TLKNIOP_ConesReserved = 0;
                                KnitYOP.TLKNIOP_NettWeightReserved = 0.00M;
                                KnitYOP.TLKNIOP_Grade = PalletStore.YarnOP_Grade;
                                KnitYOP.TLKNIOP_Store_FK = (int)tranType.TrxT_ToWhse_FK;
                                KnitYOP.TLKNIOP_OwnYarn = true;
                                context.TLKNI_YarnOrderPallets.Add(KnitYOP);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved to database successfully");
                            frmYarnwithdrawl_Load(this, null); 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                    /*
                    int TransNum = int.Parse(txtDelNo.Text);
                    frmKnitViewRep vRep = new frmKnitViewRep(8, TransNum);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    */

                }
            }
          
        }
 
    }
}
