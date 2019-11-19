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
    public partial class frmYarnDetails : Form
    {
        
        DataGridViewTextBoxColumn oTxtA;   // Palet Index no
        DataGridViewTextBoxColumn oTxtB;   // Yarn Order Number
        DataGridViewTextBoxColumn oTxtC;   // Pallet Number
        DataGridViewTextBoxColumn oTxtD;   // No Of Cones Spun 
        DataGridViewTextBoxColumn oTxtE;   // Nett Weight Spun
        DataGridViewTextBoxColumn oTxtF;   // No Of Cones Reserved
        DataGridViewTextBoxColumn oTxtG;   // Nett Weight Reserved
        DataGridViewTextBoxColumn oTxtH;   // No of Cones Available
        DataGridViewTextBoxColumn oTxtJ;   // Nett Weight Available
        DataGridViewCheckBoxColumn oChkA;  // Reset 
        bool formloaded; 
        Util core;
        IList<TLADM_Yarn> _YarnDet;
        TLADM_CustomerFile _CustDet;
        int _KO;

        List<DATA> fieldSelected = null;

      

        public frmYarnDetails(IList<TLADM_Yarn> YarnDet, TLADM_CustomerFile CustDet, int KO)
        {
            // 1st parameter is a list of applicable yarn type 
            // 2nd parameter 
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _YarnDet = YarnDet;
            _CustDet = CustDet;
            _KO = KO;
            core = new Util(); 
       

        }

        private void Form_Load(object sender, EventArgs e)
        {
            formloaded = false;
            bool first = false;

            //Initialise the dataGrid
            //------------------------------------

            fieldSelected = new List<DATA>();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Palett Number key 
            oTxtA.HeaderText = "Pallet No";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Pallet Number Yarn Order Number
            oTxtB.HeaderText = "Yarn Order Number";
            oTxtB.ValueType = typeof(int);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;
                
            oTxtC = new DataGridViewTextBoxColumn();  // 2 No of Cones Spun 
            oTxtC.HeaderText = "Pallet Number";
            oTxtC.ValueType = typeof(int);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Nett Weight spun
            oTxtD.HeaderText = "No of Cones spun";
            oTxtD.ValueType = typeof(int);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 4 No of Cones reserved
            oTxtE.HeaderText = "Nett Weight Spun";
            oTxtE.ValueType = typeof(decimal);
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();  // 5 Nett Weight reserved
            oTxtF.HeaderText = "No of Cones Reserved";
            oTxtF.ValueType = typeof(int);
            oTxtF.Visible = true;
            oTxtF.ReadOnly = false;

            oTxtG = new DataGridViewTextBoxColumn();  // 6 No of Cones Available
            oTxtG.HeaderText = "Nett Weight Reserved";
            oTxtG.ValueType = typeof(decimal);
            oTxtG.Visible = true;
            oTxtG.ReadOnly = true;

            oTxtH = new DataGridViewTextBoxColumn();  // 7 Nett vWeight Available
            oTxtH.HeaderText = "No of Cones Available";
            oTxtH.ValueType = typeof(int);
            oTxtH.Visible = true;
            oTxtH.ReadOnly = true;

            oTxtJ = new DataGridViewTextBoxColumn();  // 8 Nett vWeight Available
            oTxtJ.HeaderText = "Nett Weight Available";
            oTxtJ.ValueType = typeof(decimal);
            oTxtJ.Visible = true;
            oTxtJ.ReadOnly = true;

            oChkA = new DataGridViewCheckBoxColumn(); 
            oChkA.HeaderText = "Reset Weight";
            oChkA.ValueType = typeof(bool);
            oChkA.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // 0  Pallet No Key 
            dataGridView1.Columns.Add(oTxtB);    // 1  Pallet No
            dataGridView1.Columns.Add(oTxtC);    // 2  No of Cones Spun  
            dataGridView1.Columns.Add(oTxtD);    // 3  Nett Weight Spun
            dataGridView1.Columns.Add(oTxtE);    // 4  No of Cones Reserved
            dataGridView1.Columns.Add(oTxtF);    // 5  Nett Weight Reserved
            dataGridView1.Columns.Add(oTxtG);    // 6  No of Cones available
            dataGridView1.Columns.Add(oTxtH);    // 7  Nett Weight Available
            dataGridView1.Columns.Add(oTxtJ);    // 8  Nett Weight Available
            dataGridView1.Columns.Add(oChkA);    // 9  reset
 
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);

            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);
                
            // 1st Decision.... Is this customer a Commission Customer
            //----------------------------------------------------------
            using (var context = new TTI2Entities())
            {
                    if (_CustDet.Cust_CommissionCust)
                    {
                        // Commission Customers have their own yarn which has been sent prior 
                        //-------------------------------------------------------------------------

                        txtSupplier.Text = _CustDet.Cust_Description;
                        txtOrderNo.Text = "N/A";
                        txtYarnType.Text = _YarnDet.FirstOrDefault().YA_Description;
                        txtTexCount.Text = _YarnDet.FirstOrDefault().YA_TexCount.ToString();
                        txtTwistCount.Text = _YarnDet.FirstOrDefault().YA_Twist.ToString();
                        txtCottonOrigin.Text = "N/A";
                        txtMachineCode.Text = "N/A";

                      
                        var Receipts = context.TLKNI_YarnTransaction.Where(x => x.KnitY_Customer_FK == _CustDet.Cust_Pk).ToList();

                        foreach (var Record in Receipts)
                        {
                            var PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_HeaderRecord_FK == Record.KnitY_Pk).ToList();
                            foreach (var Pallet in PalletStack)
                            {
                               
                                foreach (var row in _YarnDet)
                                {
                                    if (Pallet.TLKNIOP_YarnType_FK == row.YA_Id)
                                    {
                                        var NetWeightReserved = 0.00M;
                                        var NettConesReserved = 0;
                                        NetWeightReserved = (decimal)Pallet.TLKNIOP_NettWeightReserved;
                                        NettConesReserved = (int)Pallet.TLKNIOP_ConesReserved;
                                        var index = dataGridView1.Rows.Add();
                                        dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                        dataGridView1.Rows[index].Cells[1].Value = 0;
                                        dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;
                                        dataGridView1.Rows[index].Cells[3].Value = Pallet.TLKNIOP_Cones;
                                        dataGridView1.Rows[index].Cells[4].Value = Math.Round(Pallet.TLKNIOP_NettWeight, 2);
                                        dataGridView1.Rows[index].Cells[5].Value = NettConesReserved;
                                        dataGridView1.Rows[index].Cells[6].Value = Math.Round(NetWeightReserved, 2);
                                        dataGridView1.Rows[index].Cells[7].Value = Pallet.TLKNIOP_Cones - NettConesReserved;
                                        dataGridView1.Rows[index].Cells[8].Value = Math.Round(Pallet.TLKNIOP_NettWeight - NetWeightReserved, 2);
                                        dataGridView1.Rows[index].Cells[9].Value = false;

                                        var PalletCones = Pallet.TLKNIOP_Cones;
                                        var PalletWeight = Pallet.TLKNIOP_NettWeight;

                                        try
                                        {

                                            fieldSelected.Add(new DATA(index, PalletWeight / PalletCones));
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
                    else
                    {
                        txtSupplier.Text = "N/A";
                        txtYarnType.Text = _YarnDet.FirstOrDefault().YA_Description;
                        txtTexCount.Text = _YarnDet.FirstOrDefault().YA_TexCount.ToString();
                        txtTwistCount.Text = _YarnDet.FirstOrDefault().YA_Twist.ToString();
                      
                        foreach (var row in _YarnDet)
                        {
                            var PalletStack = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnType_FK == row.YA_Id && x.TLKNIOP_OwnYarn &&!x.TLKNIOP_OrderConfirmed).OrderBy(x => new { x.TLKNIOP_YarnOrder_FK, x.TLKNIOP_PalletNo }).ToList();

                            bool _First = true;

                            foreach (var Pallet in PalletStack)
                            {
                                if (Pallet.TLKNIOP_ReservedBy != 0)
                                {
                                    if (Pallet.TLKNIOP_ReservedBy != _KO)
                                        continue;
                                }

                                if (_First)
                                {
                                    if (Pallet.TLKNIOP_YarnOrder_FK != null)
                                    {
                                        var _YO = context.TLSPN_YarnOrder.Find((int)Pallet.TLKNIOP_YarnOrder_FK);
                                        if (_YO != null)
                                        {
                                            txtOrderNo.Text = _YO.YarnO_OrderNumber.ToString();

                                            var CO = context.TLADM_CottonOrigin.Find(_YO.Yarno_CottonOrigin_FK);
                                            if (CO != null)
                                               txtCottonOrigin.Text = CO.CottonOrigin_Description;

                                            var MAC = context.TLADM_MachineDefinitions.Find(_YO.Yarno_MachNo_FK);
                                            if(MAC != null)
                                               txtMachineCode.Text = MAC.MD_Description;

                                            var SUP = context.TLADM_Suppliers.Find(_YO.YarnO_Supplier_FK);
                                            if (SUP != null)
                                                txtSupplier.Text = SUP.Sup_Description;

                                        }

                                      
                                    }
                                    _First = false;
                                }

                                var NetWeightReserved = 0.00M;
                                var NettConesReserved = 0;
                                NetWeightReserved = (decimal)Pallet.TLKNIOP_NettWeightReserved;
                                NettConesReserved = (int)Pallet.TLKNIOP_ConesReserved;
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                if(Pallet.TLKNIOP_OwnYarn)
                                  dataGridView1.Rows[index].Cells[1].Value = context.TLSPN_YarnOrder.Find(Pallet.TLKNIOP_YarnOrder_FK).YarnO_OrderNumber;
                                else
                                    dataGridView1.Rows[index].Cells[1].Value = 0;

                                dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;
                                dataGridView1.Rows[index].Cells[3].Value = Pallet.TLKNIOP_Cones;
                                dataGridView1.Rows[index].Cells[4].Value = Math.Round(Pallet.TLKNIOP_NettWeight, 2);
                                dataGridView1.Rows[index].Cells[5].Value = NettConesReserved;
                                dataGridView1.Rows[index].Cells[6].Value = Math.Round(NetWeightReserved, 2);
                                dataGridView1.Rows[index].Cells[7].Value = Pallet.TLKNIOP_Cones - NettConesReserved;
                                dataGridView1.Rows[index].Cells[8].Value = Math.Round(Pallet.TLKNIOP_NettWeight - NetWeightReserved, 2);
                                dataGridView1.Rows[index].Cells[9].Value = false;

                                var PalletCones = Pallet.TLKNIOP_Cones;
                                var PalletWeight = Pallet.TLKNIOP_NettWeight;

                                try
                                {

                                    fieldSelected.Add(new DATA(index, PalletWeight / PalletCones));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                   
                            }
                        }
                   }
                    if (this.dataGridView1.Rows.Count != 0)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[5];
                        this.dataGridView1.BeginEdit(true);
                    }
            }
            formloaded = true;
        }

        private void Check_Changed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (e.ColumnIndex == 8 && (bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    DialogResult res = MessageBox.Show("Please confirm transaction", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                    
                        using (var context = new TTI2Entities())
                        {
                            
                            TLKNI_YarnOrderPallets yarnStat = new TLKNI_YarnOrderPallets();
                            var _Key = (int)dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[0].Value;

                            yarnStat = context.TLKNI_YarnOrderPallets.Find(_Key);
                            yarnStat.TLKNIOP_ConesReserved = 0;
                            yarnStat.TLKNIOP_NettWeightReserved = 0.00M;
                            yarnStat.TLKNIOP_ReservedBy = 0;

                            dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[5].Value = 0;
                            dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[6].Value = 0.00M;
                            dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[7].Value = dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[2].Value;
                            dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[8].Value = dataGridView1.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value;
                           
                            try
                            {
                                context.SaveChanges();
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            /*
            if (e.ColumnIndex == 4)
            {
                    var Cell = oDgv.CurrentCell;
                    var CurrentWeight = (decimal)oDgv.Rows[Cell.RowIndex].Cells[3].Value;
                    var ReservedWeight = (decimal)oDgv.Rows[Cell.RowIndex].Cells[4].Value;
                    var RequestedWeight = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());

                    if ((RequestedWeight + ReservedWeight) > CurrentWeight)
                    {
                        MessageBox.Show("The weight amount entered exceeded that of the pallet ");
                        e.Cancel = true;
                    }
            }
          */
            
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 5)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else 
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                      
                    }
                }
           }
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lsuccess = true;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if ((int)dr.Cells[5].Value > 0)
                        {
                            TLKNI_YarnOrderPallets yarnStat = new TLKNI_YarnOrderPallets();
                            var _Key = (int)dr.Cells[0].Value;
                            yarnStat = context.TLKNI_YarnOrderPallets.Find(_Key);
                            if (yarnStat.TLKNIOP_ReservedBy != 0 && yarnStat.TLKNIOP_ReservedBy != _KO)
                                continue;

                            yarnStat.TLKNIOP_ConesReserved = (int)dr.Cells[5].Value;
                            yarnStat.TLKNIOP_NettWeightReserved = (decimal)dr.Cells[6].Value;
                            yarnStat.TLKNIOP_ReservedBy = _KO;
                           
                       }
                    }
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        lsuccess = false;
                    }
                }

                if (lsuccess)
                {
                    MessageBox.Show("data saved to database successfully");
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            decimal ConeAvgWeight = 0.00M; ;
            if (oDgv != null && formloaded)
            {
                if (e.ColumnIndex == 5)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        var Val = (int)CurrentRow.Cells[e.ColumnIndex].Value;
                        if (Val != 0)
                        {
                            var Record = fieldSelected.Find(x => x._RowIndex == CurrentRow.Index);
                            var RecordIndex = fieldSelected.IndexOf(Record);
                            if(RecordIndex != -1 )
                                ConeAvgWeight = Record._Weight;

                            CurrentRow.Cells[6].Value = Math.Round(Val * ConeAvgWeight, 2);
                            CurrentRow.Cells[7].Value = (int)CurrentRow.Cells[7].Value - Val;
                            CurrentRow.Cells[8].Value = (Decimal)CurrentRow.Cells[8].Value - (decimal)CurrentRow.Cells[6].Value;
                        }
                        else
                        {
                            CurrentRow.Cells[6].Value = 0;
                            CurrentRow.Cells[7].Value = (int)CurrentRow.Cells[3].Value;
                            CurrentRow.Cells[8].Value = (Decimal)CurrentRow.Cells[8].Value;
                        }

                    }

                }
            }
        }

        private struct DATA
        {
            public int _RowIndex;
            public decimal _Weight;

            public DATA(int Key, decimal Weight)
            {
                this._RowIndex = Key;
                this._Weight = Weight;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;

            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                var CurrentCell = oDgv.CurrentCell;
                if (CurrentCell.ReadOnly && dataGridView1.Rows[CurrentCell.RowIndex].Cells[CurrentCell.ColumnIndex + 1].Visible)
                    dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
            else if (oDgv != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (dataGridView1.CurrentCell.ReadOnly)
                        dataGridView1.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                    e.Handled = true;
                }
            }
        }
    }
}
