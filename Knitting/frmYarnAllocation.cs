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
    public partial class frmYarnAllocation : Form
    {
        bool FormLoaded;
        Util Core;

        DataGridViewTextBoxColumn oTxtA0 = null;
        DataGridViewCheckBoxColumn oChkA = null;
        DataGridViewTextBoxColumn oTxtA1 = null;
        DataGridViewTextBoxColumn oTxtA2 = null;
        DataGridViewTextBoxColumn oTxtA3 = null;
        DataGridViewTextBoxColumn oTxtA4 = null;
        DataGridViewTextBoxColumn oTxtA5 = null;
      
        public frmYarnAllocation()
        {
            InitializeComponent();
        }

        private void frmYarnAllocation_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboKnitOrders.DataSource = context.TLKNI_Order.Where(x => x.KnitO_OrderConfirmed && !x.KnitO_Closed).OrderBy(x => x.KnitO_OrderNumber).ToList();
                cmboKnitOrders.ValueMember = "KnitO_Pk";
                cmboKnitOrders.DisplayMember = "KnitO_OrderNumber";
                cmboKnitOrders.SelectedIndex = -1;
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);

            oTxtA0 = new DataGridViewTextBoxColumn();
            oTxtA0.Visible = false;
            oTxtA0.ReadOnly = true;
            oTxtA0.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA0);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.Visible = true;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

                 
            
            oTxtA1 = new DataGridViewTextBoxColumn();
            oTxtA1.HeaderText = "Pallet Number";
            oTxtA1.ReadOnly = true;
            oTxtA1.Visible = true;
            oTxtA1.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA1);
   
            
            oTxtA2 = new DataGridViewTextBoxColumn();
            oTxtA2.HeaderText = "Cones";
            oTxtA2.Visible = true;
            oTxtA2.ValueType = typeof(int);
            oTxtA2.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA2);

            oTxtA3 = new DataGridViewTextBoxColumn();
            oTxtA3.HeaderText = "Weight";
            oTxtA3.Visible = true;
            oTxtA3.ValueType = typeof(int);
            oTxtA3.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA3);

            oTxtA4 = new DataGridViewTextBoxColumn();
            oTxtA4.HeaderText = "Cones Reserved";
            oTxtA4.Visible = true;
            oTxtA4.ValueType = typeof(int);
            oTxtA4.ReadOnly = false;
            dataGridView1.Columns.Add(oTxtA4);

            oTxtA5 = new DataGridViewTextBoxColumn();
            oTxtA5.HeaderText = "Weight Reserved";
            oTxtA5.Visible = true;
            oTxtA5.ValueType = typeof(Decimal);
            oTxtA5.ReadOnly = false;
            dataGridView1.Columns.Add(oTxtA5);
                      

            Core = new Util();

            rbOwnYarn.Checked = true;
            btnSave.Enabled = false;
            
            FormLoaded = true;
        }

        private void cmboKnitOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLADM_Yarn> yarnTypes = new List<TLADM_Yarn>();
            IList<TLSPN_YarnOrder> yarnOrders = new List<TLSPN_YarnOrder>();
            IList<TLKNI_YarnTransaction> yarnTrans = new List<TLKNI_YarnTransaction>();

            if (oCmbo != null && FormLoaded)
            {
                var Selected = (TLKNI_Order)oCmbo.SelectedItem;
                if (Selected != null)
                {
                    dataGridView1.Rows.Clear();
                    cmboYarnOrders.DataSource = null;
                    using (var context = new TTI2Entities())
                    {
                        var Greige = context.TLADM_Griege.Find(Selected.KnitO_Product_FK);
                        if (Greige != null)
                        {
                            
                           var GreigePN = Core.ExtrapNumber(Greige.TLGreige_YarnPowerN, context.TLADM_Yarn.Count()).ToList();
                           foreach (var Number in GreigePN)
                           {
                                var YarnDet = context.TLADM_Yarn.Where(x => x.YA_PowerN == Number).FirstOrDefault();
                                if (YarnDet != null)
                                {
                                    if (rbOwnYarn.Checked)
                                    {
                                        var PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && x.TLKNIOP_OwnYarn).GroupBy(x => new { x.TLKNIOP_YarnOrder_FK }).ToList();
                                        foreach (var Pallet in PalletStack)
                                        {
                                            var YOrder = context.TLSPN_YarnOrder.Find(Pallet.FirstOrDefault().TLKNIOP_YarnOrder_FK);
                                            var xx = yarnOrders.Where(x => x.YarnO_Pk == YOrder.YarnO_Pk).FirstOrDefault();
                                            if (xx == null)
                                                yarnOrders.Add(YOrder);
                                        }
                                    }
                                    else
                                    {
                                        var PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == YarnDet.YA_Id && !x.TLKNIOP_OwnYarn).GroupBy(x => new { x.TLKNIOP_HeaderRecord_FK }).ToList();
                                        foreach (var Pallet in PalletStack)
                                        {
                                            var Pk = Pallet.FirstOrDefault().TLKNIOP_HeaderRecord_FK;
                                            var YOrder = context.TLKNI_YarnTransaction.Where(x=>x.KnitY_Pk == Pk).FirstOrDefault();
                                            if (YOrder != null)
                                                yarnTrans.Add(YOrder);
                                        }
                                    }
                                }
                           }
                           

                            FormLoaded = false;
                            cmboYarnOrders.DataSource = null;
                            cmboYarnOrders.Items.Clear();
                            if (rbOwnYarn.Checked)
                            {
                                cmboYarnOrders.DataSource = yarnOrders;
                                cmboYarnOrders.DisplayMember = "YarnO_OrderNumber";
                                cmboYarnOrders.ValueMember = "YarnO_Pk";
                            }
                            else
                            {
                                cmboYarnOrders.DataSource = yarnTrans;
                                cmboYarnOrders.DisplayMember = "KnitY_Pk";
                                cmboYarnOrders.ValueMember = "KnitY_TransactionDoc";
                            }
                            cmboYarnOrders.SelectedIndex = -1;
                            FormLoaded = true;
                        }
                    }
                }
            }
        }

        private void cmboYarnOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    btnSave.Enabled = true;
                    if (rbOwnYarn.Checked)
                    {
                        var Selected = (TLSPN_YarnOrder)oCmbo.SelectedItem;
                        if (Selected != null)
                        {
                            dataGridView1.Rows.Clear();
                            oTxtA2.ValueType = typeof(System.Int32);
                            
                            var Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnOrder_FK == Selected.YarnO_Pk && (x.TLKNIOP_NettWeight - x.TLKNIOP_NettWeightReserved > 0) && !x.TLKNIOP_SplitPallet).OrderBy(x => x.TLKNIOP_PalletNo).ToList();
                            foreach (var Pallet in Pallets)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = false;
                                dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;
                                dataGridView1.Rows[index].Cells[3].Value = Pallet.TLKNIOP_Cones - Pallet.TLKNIOP_ConesReserved;
                                dataGridView1.Rows[index].Cells[4].Value = Pallet.TLKNIOP_NettWeight - Pallet.TLKNIOP_NettWeightReserved;
                                dataGridView1.Rows[index].Cells[5].Value = 0;
                                dataGridView1.Rows[index].Cells[6].Value = 0.0M;
                            }
                        }

                    }
                    else
                    {
                        var Selected = (TLKNI_YarnTransaction)oCmbo.SelectedItem;
                        if (Selected != null)
                        {
                            dataGridView1.Rows.Clear();
                            oTxtA2.ValueType = typeof(string);             

                            var Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_HeaderRecord_FK == Selected.KnitY_Pk && (x.TLKNIOP_NettWeight - x.TLKNIOP_NettWeightReserved > 0) && !x.TLKNIOP_SplitPallet).OrderBy(x => x.TLKNIOP_PalletNo).ToList();
                            foreach (var Pallet in Pallets)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = false;
                                dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_TLPalletNo;
                                dataGridView1.Rows[index].Cells[3].Value = Pallet.TLKNIOP_Cones - Pallet.TLKNIOP_ConesReserved;
                                dataGridView1.Rows[index].Cells[4].Value = Pallet.TLKNIOP_NettWeight - Pallet.TLKNIOP_NettWeightReserved;
                                dataGridView1.Rows[index].Cells[5].Value = 0;
                                dataGridView1.Rows[index].Cells[6].Value = 0.0M;
                            }
                          
                        }

                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 5)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(Core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(Core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(Core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(Core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(Core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(Core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(Core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(Core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(Core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(Core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void Check_Changed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[5].Value = dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value;
                    dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[6].Value = dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[4].Value;
                }
                else
                {
                    dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[5].Value = 0;
                    dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[6].Value = 0.0M;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TLSPN_YarnOrder YO = null;
            TLKNI_YarnTransaction YO3rdP = null;

            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                var KO = (TLKNI_Order)cmboKnitOrders.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    if (rbOwnYarn.Checked)
                    {
                        YO = (TLSPN_YarnOrder)cmboYarnOrders.SelectedItem;
                    }
                    else
                    {
                        YO3rdP = (TLKNI_YarnTransaction)cmboYarnOrders.SelectedItem;
                    }

                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if ((bool)Row.Cells[1].Value == false)
                                continue;
                        
                        bool lSplit = false;
    
                        TLKNI_YarnOrderPallets yarnStat = new TLKNI_YarnOrderPallets();
                        var _Key = (int)Row.Cells[0].Value;
                        yarnStat = context.TLKNI_YarnOrderPallets.Find(_Key);

                        if ((decimal)Row.Cells[4].Value > (decimal)Row.Cells[6].Value)
                        {
                            // We now need to start recording as and when a pallet is across orders
                            //---------------------------------------------------------------------
                            DialogResult res = MessageBox.Show("Please confirm split transaction", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (res == DialogResult.Yes)
                            {
                                TLKNI_YarnOrderPallets YarnSplit = new TLKNI_YarnOrderPallets();
                                YarnSplit.TLKNIOP_CommisionCust = yarnStat.TLKNIOP_CommisionCust;
                                YarnSplit.TLKNIOP_Cones         = yarnStat.TLKNIOP_Cones;
                                YarnSplit.TLKNIOP_DatePacked    = yarnStat.TLKNIOP_DatePacked;
                                YarnSplit.TLKNIOP_Grade         = yarnStat.TLKNIOP_Grade;
                                YarnSplit.TLKNIOP_GrossWeight   = yarnStat.TLKNIOP_GrossWeight;
                                YarnSplit.TLKNIOP_HeaderRecord_FK = yarnStat.TLKNIOP_HeaderRecord_FK;
                                YarnSplit.TLKNIOP_NettWeight = yarnStat.TLKNIOP_NettWeight;
                                YarnSplit.TLKNIOP_SplitPallet = true;
                                YarnSplit.TLKNIOP_ReservedBy = KO.KnitO_Pk;
                                YarnSplit.TLKNIOP_ReservedDate = dtpTransactionDate.Value;
                                YarnSplit.TLKNIOP_OrderConfirmed = yarnStat.TLKNIOP_OrderConfirmed;
                                YarnSplit.TLKNIOP_OwnYarn = yarnStat.TLKNIOP_OwnYarn;
                                YarnSplit.TLKNIOP_PalletNo = yarnStat.TLKNIOP_PalletNo;
                                YarnSplit.TLKNIOP_Store_FK = yarnStat.TLKNIOP_Store_FK;
                                YarnSplit.TLKNIOP_TareWeight = yarnStat.TLKNIOP_TareWeight;
                                YarnSplit.TLKNIOP_YarnOrder_FK = yarnStat.TLKNIOP_YarnOrder_FK;
                                YarnSplit.TLKNIOP_YarnType_FK = yarnStat.TLKNIOP_YarnType_FK;

                                YarnSplit.TLKNIOP_NettWeight = (decimal)Row.Cells[6].Value;
                                YarnSplit.TLKNIOP_ConesReserved = (int)Row.Cells[5].Value;
                                YarnSplit.TLKNIOP_NettWeightReserved = (decimal)Row.Cells[6].Value;
                                YarnSplit.TLKNIOP_Cones = (int)Row.Cells[5].Value;

                                context.TLKNI_YarnOrderPallets.Add(YarnSplit);
                                lSplit = true;
                            }
                            else
                                return;
                        }
                       
                         yarnStat.TLKNIOP_ConesReserved += (int)Row.Cells[5].Value;
                         yarnStat.TLKNIOP_NettWeightReserved += (decimal)Row.Cells[6].Value;
                         
                         if(!lSplit)
                         {
                            yarnStat.TLKNIOP_ReservedBy = KO.KnitO_Pk;
                            yarnStat.TLKNIOP_ReservedDate = dtpTransactionDate.Value;
                         }
                    }
                   
                    // Note for the file
                    //-----------------------------------
                    // The Yarn Order Number is either TTL own Yarn or that received 
                    // from a Third Party
                    //----------------------------------------------------------------
                    var KnitOrder = context.TLKNI_Order.Find(KO.KnitO_Pk);
                    if (KnitOrder != null)
                    {
                        KnitOrder.KnitO_YarnAssigned = true;

                        if (rbOwnYarn.Checked)
                        {
                            KnitOrder.KnitO_YarnO_FK = YO.YarnO_Pk;
                        }
                        else
                        {
                            KnitOrder.KnitO_YarnO_FK = YO3rdP.KnitY_Pk;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved to the database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                
                dataGridView1.Rows.Clear();
                
                FormLoaded = false;
                cmboKnitOrders.SelectedItem = -1;
                cmboYarnOrders.SelectedItem = -1;
                FormLoaded = true;

                btnSave.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void RbThirdParty_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
