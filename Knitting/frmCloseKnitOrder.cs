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
    public partial class frmCloseKnitOrder : Form
    {
        bool formloaded;
        DataGridViewComboBoxColumn oCmbA;  // Pallet No
        DataGridViewTextBoxColumn oTxtA;   // Number of Cones
        DataGridViewTextBoxColumn oTxtB;   // Actual Weight returned
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewComboBoxColumn oCmbB;

        Util core;

        public frmCloseKnitOrder()
        {
            InitializeComponent();
            oCmbA = new DataGridViewComboBoxColumn();
            oCmbA.HeaderText = "Pallet No";
            oCmbA.ValueMember = "Pallet_No";
            oCmbA.DisplayMember = "YA_Description";

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "No of Cones";
            oTxtA.ValueType = typeof(int);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Weight Measured";
            oTxtB.ValueType = typeof(decimal);
            oTxtB.Visible = true;

            // *202050919
            //oChkA = new DataGridViewCheckBoxColumn();
            //oChkA.HeaderText = "Select";
            //oChkA.ValueType = typeof(bool);
            //oChkA.Visible = true;

            //oCmbB = new DataGridViewComboBoxColumn();
            //oCmbB.HeaderText = "Alternate KO";
            //oCmbB.Visible = true;

            dataGridView1.Columns.Add(oCmbA); // 0
            dataGridView1.Columns.Add(oTxtA); // 1
            dataGridView1.Columns.Add(oTxtB); // 2
            //dataGridView1.Columns.Add(oChkA); // 3
            //dataGridView1.Columns.Add(oCmbB); // 4

            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToOrderColumns = false;

            core = new Util();

        }

        private void frmCloseKnitOrder_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(2);
                if (LNU != null)
                    txtTransNum.Text = LNU.col7.ToString();

                txtCottonOrigin.Text = string.Empty;
                txtIdentification.Text = string.Empty;
                txtMachine.Text = string.Empty;
                txtNotes.Text = string.Empty;
                txtTexCount.Text = string.Empty;
                txtTwist.Text = string.Empty;
                txtYarnOrder.Text = string.Empty;
                txtYarnType.Text = string.Empty;

                cmbKnitOrder.DataSource = context.TLKNI_Order.Where(x => !x.KnitO_Closed && x.KnitO_OrderConfirmed).ToList();
                cmbKnitOrder.DisplayMember = "KnitO_OrderNumber";
                cmbKnitOrder.ValueMember = "KnitO_Pk";
                cmbKnitOrder.SelectedIndex = -1;

                oCmbA.DataSource = null;
                oCmbA.DisplayMember = "TLKNIOP_PalletNo";
                oCmbA.ValueMember = "TLKNIOP_Pk";

                //oCmbB.DataSource = null;
                //oCmbB.DisplayMember = "KnitO_OrderNumber";
                //oCmbB.ValueMember = "KnitO_Pk";

                dataGridView1.Rows.Add();
            }
            formloaded = true;
        }

       

        private void cmbKnitOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool First = true;
            ComboBox oCmbo = sender as ComboBox;
            IList<TLKNI_YarnOrderPallets> Pallets = new List<TLKNI_YarnOrderPallets>();

            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var selected = (TLKNI_Order)oCmbo.SelectedItem;
                    if (selected != null)
                    {
                        if (selected.KnitO_YarnO_FK != null)
                        {
                            var YO = context.TLSPN_YarnOrder.Find(selected.KnitO_YarnO_FK);
                            if (YO != null)
                            {
                                txtYarnOrder.Text = YO.YarnO_OrderNumber.ToString();

                                var MAC = context.TLADM_MachineDefinitions.Find(YO.Yarno_MachNo_FK);
                                if (MAC != null)
                                {
                                    txtMachine.Text = MAC.MD_MachineCode;
                                }
                            }
                        }

                        var Transactions = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == selected.KnitO_Pk).ToList();
                        foreach(var Transaction in Transactions)
                        {
                            var Pallet = context.TLKNI_YarnOrderPallets.Find(Transaction.TLKYT_YOP_FK);
                            if (Pallet != null)
                            {
                                if (First)
                                {
                                    var YarnDet = context.TLADM_Yarn.Find(Pallet.TLKNIOP_YarnType_FK);
                                    if (YarnDet != null)
                                    {
                                        txtYarnType.Text = YarnDet.YA_Description;
                                        txtTwist.Text = YarnDet.YA_Twist.ToString();
                                        txtTexCount.Text = YarnDet.YA_TexCount.ToString();
                                        txtIdentification.Text = YarnDet.YA_ConeColour;

                                        var Origin = context.TLADM_CottonOrigin.Find(YarnDet.YA_CottonOrigin_FK);
                                        if (Origin != null)
                                        {
                                            txtCottonOrigin.Text = Origin.CottonOrigin_Description;
                                        }
                                    }
                                    First = false;
                                }

                            }
                            Pallets.Add(Pallet);
                        }
                        
                        oCmbA.DataSource = Pallets;
                        oCmbA.DisplayMember = "TLKNIOP_PalletNo";
                        oCmbA.ValueMember = "TLKNIOP_Pk";

                        //oCmbB.DataSource = context.TLKNI_Order.Where(x => !x.KnitO_Closed && x.KnitO_Pk != selected.KnitO_Pk).ToList();
                        //oCmbB.DisplayMember = "KnitO_OrderNumber";
                        //oCmbB.ValueMember = "KnitO_Pk";

                     }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLADM_TranactionType TranType = null;
            if (oBtn != null && formloaded)
            {
              
                var KO = (TLKNI_Order)cmbKnitOrder.SelectedItem;
                if(KO != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        KO = context.TLKNI_Order.Find(KO.KnitO_Pk);
                        KO.KnitO_Closed = true;
                        KO.KnitO_ClosedDate = dtpKnitOrderClosed.Value;

                        var LNU = context.TLADM_LastNumberUsed.Find(2);
                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                   
                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            if (dr.Cells[0].Value == null)
                                continue;

                            //if (dr.Cells[3].Value != null && (bool)dr.Cells[3].Value == true)
                            //{
                            //    if (dr.Cells[4].Value == null) 
                            //    {
                            //        MessageBox.Show("When using the select button please select an alternative Knit Order to allocate against", "Line " + (dr.Index + 1).ToString());
                            //        return;
                            //    }
                            //}

                            TLKNI_YarnTransactionDetails trnsDet = new TLKNI_YarnTransactionDetails();

                            trnsDet.KnitYD_Notes = txtNotes.Text;
                            trnsDet.KnitYD_OriginalOrderNo = KO.KnitO_OrderNumber;
                            trnsDet.KnitYD_PalletNo_FK = (int)dr.Cells[0].Value;
                            trnsDet.KnitYD_RTS = false;
                            trnsDet.KnitYD_TransactionDate = dtpKnitOrderClosed.Value;
                            trnsDet.KnitYD_WriteOff = false;

                            if (LNU != null)
                                trnsDet.KnitYD_TransactionNumber = LNU.col7;

                            trnsDet.KnitYD_ApprovedBy = string.Empty;
                            trnsDet.KnitYD_NoOfCones = (int)dr.Cells[1].Value;
                            trnsDet.KnitYD_NettWeight = (decimal)dr.Cells[2].Value;
                            trnsDet.KnitYD_GrossWeight = 0.00M;
                            trnsDet.KnitYD_KnitY_FK = KO.KnitO_Pk;
                            trnsDet.KnitYD_YarnReturned = true;
                            
                            Decimal WeightRecorded = (decimal)dr.Cells[2].Value;

                            var Pallet = context.TLKNI_YarnOrderPallets.Find(trnsDet.KnitYD_PalletNo_FK);
                            if (Pallet != null)
                            {
                                trnsDet.KnitYD_YarnType_FK = Pallet.TLKNIOP_YarnType_FK;
                                //if (dr.Cells[3].Value != null && (bool)dr.Cells[3].Value == true)
                                //{
                                //    //======================================================================
                                //    // has to be a negative number to facilitate the calculations 
                                //    //=================================================================
                                //    TLKNI_YarnAllocTransctions TransAlloc = new TLKNI_YarnAllocTransctions();
                                //    TransAlloc.TLKYT_KnitOrder_FK = KO.KnitO_Pk;
                                //    TransAlloc.TLKYT_NettWeight = -1 * WeightRecorded;
                                //    TransAlloc.TLKYT_NoOfCones = -1 * (int)dr.Cells[1].Value; ;
                                //    TransAlloc.TLKYT_TransDate = dtpKnitOrderClosed.Value;
                                //    TransAlloc.TLKYT_TranType = 3;
                                //    TransAlloc.TLKYT_YOP_FK = Pallet.TLKNIOP_Pk;

                                //    context.TLKNI_YarnAllocTransctions.Add(TransAlloc);

                                //    //=================================================================
                                //    // Now find the pallet to which the balance is going to be added
                                //    //========================================================
                                //    var ReservedBy = (int)dr.Cells[4].Value;
                                //    var Trans = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == ReservedBy && x.TLKYT_TranType == 1).FirstOrDefault();
                                //    if (Trans != null)
                                //    {
                                //        var PalletTo = context.TLKNI_YarnOrderPallets.Find(Trans.TLKYT_YOP_FK);
                                //        if (PalletTo != null)
                                //        {
                                //            PalletTo.TLKNIOP_AdditionalYarn += WeightRecorded;

                                //            //=============================================
                                //            // we need a record of this transaction
                                //            //========================================
                                //            TransAlloc = new TLKNI_YarnAllocTransctions();
                                //            TransAlloc.TLKYT_KnitOrder_FK = ReservedBy;
                                //            TransAlloc.TLKYT_NoOfCones += (int)dr.Cells[1].Value;
                                //            TransAlloc.TLKYT_NettWeight = WeightRecorded;
                                //            TransAlloc.TLKYT_TransDate = dtpKnitOrderClosed.Value;
                                //            TransAlloc.TLKYT_TranType = 3;
                                //            TransAlloc.TLKYT_YOP_FK = PalletTo.TLKNIOP_Pk;
                                //            context.TLKNI_YarnAllocTransctions.Add(TransAlloc);

                                //            if (core.CalculatePalletNett(PalletTo) <= 0.00m)
                                //            {
                                //                PalletTo.TLKNIOP_PalletAllocated = true;
                                //            }
                                //            else
                                //            {
                                //                PalletTo.TLKNIOP_PalletAllocated = false;
                                //            }

                                //        }
                                //    }

                                //    trnsDet.KnitYD_TransactionType = 2005;
                                //}
                                //else
                                //{
                                    var Cones = (int)dr.Cells[1].Value;
                                    WeightRecorded = (decimal)dr.Cells[2].Value;

                                    trnsDet.KnitYD_YarnReturned = true;

                                    Pallet.TLKNIOP_NettWeightReturned += WeightRecorded;

                                //=============================================
                                //Yarn allocation transaction
                                //========================================
                                var TransAlloc = new TLKNI_YarnAllocTransctions();
                                    TransAlloc.TLKYT_KnitOrder_FK = KO.KnitO_Pk;
                                    TransAlloc.TLKYT_NettWeight = WeightRecorded;
                                    TransAlloc.TLKYT_NoOfCones = -Cones;
                                    TransAlloc.TLKYT_TransDate = dtpKnitOrderClosed.Value;
                                    TransAlloc.TLKYT_TranType = 2; //should be 22 KO Yarn Return Yarn Store
                                TransAlloc.TLKYT_YOP_FK = Pallet.TLKNIOP_Pk;

                                    context.TLKNI_YarnAllocTransctions.Add(TransAlloc);

                                //=========================================
                                // This should make the pallet available   
                                //======================================================
                                // Update pallet allocation status
                                //Pallet.TLKNIOP_PalletAllocated = core.CalculatePalletNett(Pallet) <= 0.00M;
                                if (core.CalculatePalletNett(Pallet) <= 0.00M)
                                {
                                    Pallet.TLKNIOP_PalletAllocated = true;
                                }
                                else
                                {
                                    Pallet.TLKNIOP_PalletAllocated = false;
                                }



                                //==============================
                                // Now do the the swop between stores
                                // dont have to do this for the above has it stays in WIP spinning
                                //===================================================
                                if (Pallet.TLKNIOP_OwnYarn)
                                    {


                                        Pallet.TLKNIOP_Store_FK = 34;
                                        /*
                                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 1400).FirstOrDefault();
                                        if (TranType != null)
                                            Pallet.TLKNIOP_Store_FK = (int)TranType.TrxT_ToWhse_FK;
                                        */

                                        trnsDet.KnitYD_TransactionType = 1400; //??????
                                    }
                                    else
                                    {
                                        if (Pallet.TLKNIOP_CommisionCust)
                                        {
                                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 1300).FirstOrDefault();
                                                if (TranType != null)
                                                    Pallet.TLKNIOP_Store_FK = (int)TranType.TrxT_ToWhse_FK;

                                                trnsDet.KnitYD_TransactionType = 1300;
                                         }
                                         else
                                         {
                                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 1200).FirstOrDefault();
                                                if (TranType != null)
                                                    Pallet.TLKNIOP_Store_FK = (int)TranType.TrxT_ToWhse_FK;

                                                trnsDet.KnitYD_TransactionType = 1200;
                                         }
                                    }
                                //}
            
                            }
                            context.TLKNI_YarnTransactionDetails.Add(trnsDet);
                        }

                        if(LNU != null)
                            LNU.col7 += 1;

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");
                            dataGridView1.Rows.Clear();
                            
                            frmKnitViewRep vRep = new frmKnitViewRep(11, LNU.col7 - 1);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();

                            }
                            frmCloseKnitOrder_Load(this, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (oDgv != null && formloaded)
            {
                oDgv.Rows[oDgv.NewRowIndex].Cells[3].Value = false;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox oCmb = e.Control as ComboBox;

            if (oCmb != null && oDgv.Focused && oDgv.CurrentCell is DataGridViewComboBoxCell && oDgv.CurrentCell.ColumnIndex == 0)
            {
                oCmb.SelectedIndexChanged += new EventHandler(CheckComboBox_SelectedIndexChanged);
            }
            else
            {
                if (oDgv.CurrentCell.ColumnIndex == 1)
                {
                    e.Control.KeyDown += core.txtWin_KeyDownJI;
                    e.Control.KeyPress += core.txtWin_KeyPress;
                }
                else if (oDgv.CurrentCell.ColumnIndex == 2)
                {
                    e.Control.KeyDown -= core.txtWin_KeyDownJI;
                    e.Control.KeyPress -= core.txtWin_KeyPress;

                    e.Control.KeyDown += core.txtWin_KeyDownOEM;
                    e.Control.KeyPress += core.txtWin_KeyPress;
                }
                else
                {
                    e.Control.KeyDown -= core.txtWin_KeyDownJI;
                    e.Control.KeyPress -= core.txtWin_KeyPress;
                }
            }
        }

        private void CheckComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null)
            {
                  var Selected = (TLKNI_YarnOrderPallets)oCmbo.SelectedItem;
                  if (Selected != null)
                  {
                        var oDgv = (DataGridView)dataGridView1;
                        var CurrentRow = oDgv.CurrentRow;
                        dataGridView1.Rows[CurrentRow.Index].Cells[1].Value = 0; //  Selected.TLKNIOP_NettWeightConsummed;
                        dataGridView1.Rows[CurrentRow.Index].Cells[2].Value = 0.0M; //  Selected.TLKNIOP_NettWeightConsummed;

                  }
            }
        }
   }
}
