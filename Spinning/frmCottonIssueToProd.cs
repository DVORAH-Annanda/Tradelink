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
    public partial class frmCottonIssueToProd : Form
    {
        DataGridViewCheckBoxColumn oChk;
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        DataGridViewTextBoxColumn oTxtBoxG;
        DataGridViewTextBoxColumn oTxtBoxH; 
        bool formloaded;
        int IndexPos;

        List<DATA> fieldSelected = new List<DATA>();

        public frmCottonIssueToProd()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            IndexPos = 0;

            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();

                var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                if (LastNumber != null)
                    txtIssueToProdNo.Text = LastNumber.col5.ToString();
               
                var existing = context.AltSelectCottonRecords().ToList();
                foreach (var row in existing)
                {
                    TLSPN_CottonTransactions cr = new TLSPN_CottonTransactions();
                    cr.cotrx_ContractNo_Fk = row.cotrx_ContractNo_Fk;
                    cr.cotrx_Customer_FK = row.cotrx_Customer_FK;
                    cr.cotrx_GrossAveBaleWeight = row.cotrx_GrossAveBaleWeight;
                    cr.cotrx_GrossWeight = row.cotrx_GrossWeight;
                    cr.cotrx_LotNo = row.cotrx_LotNo;
                    cr.cotrx_NettPerWB = row.cotrx_NettPerWB;
                    cr.cotrx_NetWeight = row.cotrx_NetWeight;
                    cr.cotrx_NoBales = row.cotrx_NoBales;
                    cr.cotrx_Notes = row.cotrx_Notes;
                    cr.cotrx_pk = row.cotrx_pk;
                    cr.cotrx_Return_No = row.cotrx_Return_No;
                    cr.cotrx_Supplier_FK = row.cotrx_Supplier_FK;
                    cr.cotrx_TransDate = row.cotrx_TransDate;
                    cr.cotrx_TranType = row.cotrx_TranType;
                    //cr.cotrx_VehReg_FK = row.cotrx_VehReg_FK;
                    cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                    cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                    cr.cotrx_WriteOff = row.cotrx_WriteOff;
                    cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                    cotreceived.Add(cr);
                }
              
                cmbLotNo.DataSource = cotreceived.OrderBy(x=>x.cotrx_LotNo).ToList();
                cmbLotNo.ValueMember = "cotrx_LotNo";
                cmbLotNo.DisplayMember = "cotrx_LotNo";
                cmbLotNo.SelectedValue = -1;
            }

            oChk = new DataGridViewCheckBoxColumn();              //1
            oChk.HeaderText = "Select";
            oChk.ValueType = typeof(Boolean);

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();          //2
            oTxtBoxB.HeaderText = "Lay down position";
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxB.Width = 50;

            oTxtBoxC = new DataGridViewTextBoxColumn();          //3
            oTxtBoxC.HeaderText = "Bale No";
            oTxtBoxC.ValueType = typeof(int);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD = new DataGridViewTextBoxColumn();          //4
            oTxtBoxD.HeaderText = "Mic";
            oTxtBoxD.ValueType = typeof(Decimal);
            oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE = new DataGridViewTextBoxColumn();          //5
            oTxtBoxE.HeaderText = "Kgs (NETT)";
            oTxtBoxE.ValueType = typeof(Decimal);
            oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF = new DataGridViewTextBoxColumn();         //6
            oTxtBoxF.HeaderText = "Staple";
            oTxtBoxF.ValueType = typeof(Decimal);
            oTxtBoxF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxF.ReadOnly = true;

            oTxtBoxG = new DataGridViewTextBoxColumn();         //7
            oTxtBoxG.HeaderText = "Kgs (GROSS)";
            oTxtBoxG.ValueType = typeof(Decimal);
            oTxtBoxG.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtBoxG.ReadOnly = true;

            oTxtBoxH = new DataGridViewTextBoxColumn();         //7
            oTxtBoxH.HeaderText = "Lot No";
            oTxtBoxH.ValueType = typeof(string);
            oTxtBoxH.ReadOnly = true;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            dataGridView1.Columns.Add(oTxtBoxA);    // 0 Index Pk value
            dataGridView1.Columns.Add(oChk);        // 1 Selected
            dataGridView1.Columns.Add(oTxtBoxB);    // 2 Lay Down Position  
            dataGridView1.Columns.Add(oTxtBoxC);    // 3 Bales Numeric 
            dataGridView1.Columns.Add(oTxtBoxD);    // 4 MIC Decimal
            dataGridView1.Columns.Add(oTxtBoxE);    // 5 kgs (NETT) Decimal 
            dataGridView1.Columns.Add(oTxtBoxF);    // 6 Staple Decimal
            dataGridView1.Columns.Add(oTxtBoxG);    // 7 Kgs S(GROSS) Decimal
            dataGridView1.Columns.Add(oTxtBoxH);    // 8 Lot No 
           
            formloaded = true;

        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        }

        private void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null & formloaded)
            {
                var selectedRecord = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (selectedRecord != null)
                {
                   
                    using (var context = new TTI2Entities())
                    {
                        var ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo && !x.CoBales_CottonSold && !x.CoBales_IssuedToProd && !x.CoBales_CottonReturned && x.CotBales_ConfirmedByQA).ToList();
                        if (ExistingData.Count() == 0)
                        {
                            MessageBox.Show("There are no records available under this lot number", "Insure that QA has confirmed release", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                        foreach (var row in ExistingData)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                            if (row.CoBales_BlowRoomPosition != 0)
                            {
                                dataGridView1.Rows[index].Cells[1].Value = true;
                                fieldSelected.Add(new DATA(index, row.CoBales_BlowRoomPosition, true, row.CotBales_Pk));
                                                                
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[1].Value = false;
                            }
                            dataGridView1.Rows[index].Cells[2].Value = row.CoBales_BlowRoomPosition;
                            dataGridView1.Rows[index].Cells[3].Value = row.CotBales_BaleNo;
                            dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Mic, 2);
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                            dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Staple, 2);
                            dataGridView1.Rows[index].Cells[7].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                            dataGridView1.Rows[index].Cells[8].Value = row.CotBales_LotNo;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[oDgv.CurrentCell.ColumnIndex + 2].Value != null)
                {
                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        var CurrentRow = oDgv.CurrentRow;
                        if (CurrentRow != null)
                        {
                            oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[oDgv.CurrentCell.ColumnIndex + 1].Value = ++IndexPos;
                            fieldSelected.Add(new DATA(CurrentRow.Index, IndexPos, true, (int)CurrentRow.Cells[0].Value));
                        }
                    }
                    else
                    {
                        
                        var record    = fieldSelected.Find(x => x.rownumber == oDgv.CurrentCell.RowIndex);
                        var ListIndex = fieldSelected.IndexOf(record);

                        record.rowChecked  = false;
                        if(ListIndex != -1)
                            fieldSelected[ListIndex] = record;
 
                        oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[oDgv.CurrentCell.ColumnIndex + 1].Value = 0;

                        fieldSelected = ReNumber(fieldSelected).ToList();
                        foreach (var row in fieldSelected)
                        {
                            dataGridView1.Rows[row.rownumber].Selected = true;
                            dataGridView1.Rows[row.rownumber].Cells[2].Value = row.seqNo;
                            dataGridView1.Rows[row.rownumber].Selected = false;
                        }

                        IndexPos = fieldSelected.Count();
                     }
                }
            }
        }

        private struct DATA
        {
            public int reckey;
            public int rownumber;
            public int seqNo;
            public bool rowChecked;

            public DATA(int rowNumber, int seqNumber, bool RowChecked, int RKey)
            {
                this.rownumber = rowNumber;
                this.seqNo = seqNumber;
                this.rowChecked = RowChecked;
                this.reckey = RKey;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (!(bool)rw.Cells[1].Value)
                           continue;
                        
                        TLSPN_CottonReceivedBales bales = context.TLSPN_CottonReceivedBales.Find((int)rw.Cells[0].Value);
                        if (bales != null)
                        {
                            if ((bool)rw.Cells[1].EditedFormattedValue)
                            {
                                bales.CoBales_CottonSequence = Convert.ToInt32(txtIssueToProdNo.Text);
                                bales.CoBales_BlowRoomPosition = (int)rw.Cells[2].Value;
                                
                            }
                            else
                            {
                                bales.CoBales_CottonSequence = 0;
                                bales.CoBales_BlowRoomPosition = 0;
                            }

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
                    //-----------------------------------------------------------------------
                    //Now we need to print the report
                    //-------------------------------------------------------------------
                    frmViewReport vRep = new frmViewReport(1);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this); 
                }
            }
        }

        private IList<DATA> ReNumber(IList<DATA> a)
        {
            TLSPN_CottonReceivedBales bales;

            IList<DATA> newListing = new List<DATA>();
            int Seqno = 0;
            using ( var context = new TTI2Entities())
            {
                foreach (var dr in a)
                {
                    if (!dr.rowChecked)
                    {
                       
                       bales = context.TLSPN_CottonReceivedBales.Find(dr.reckey);
                       if (bales != null)
                       {
                           bales.CoBales_CottonSequence = 0;
                           bales.CoBales_BlowRoomPosition = 0;
                       }
                       
                       continue;
                    }

                    DATA x = new DATA();

                    x.rowChecked   = true;
                    x.rownumber    = dr.rownumber;
                    x.seqNo        = ++Seqno;
                    x.reckey       = dr.reckey;

                    /*

                    bales = context.TLSPN_CottonReceivedBales.Find(dr.reckey);
                    if (bales != null)
                    {
                        bales.CoBales_BlowRoomPosition = x.seqNo;
                    }
                    */

                    newListing.Add(x);
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            return newListing;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool success = true; 
            if (oBtn != null)
            {
               
                var CottonRec = (TLSPN_CottonTransactions)cmbLotNo.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    //Hard Coded at the moment 
                    // See Table TLADM_TranactionType for a complete List of the Transaction Type Per Department
                    //--------------------------------------------------------------------------------------------------
                    var DeptDetail = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 500 && x.TrxT_Department_FK == DeptDetail.Dep_Id).FirstOrDefault();

                    var LastNumber = context.TLADM_LastNumberUsed.Find(1);
                    if (LastNumber != null)
                        LastNumber.col5 += 1;

                    var NoBales = 0;
                    var GrossWeight = 0M;
                    var NettWeight = 0M;

                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (rw.Cells[1].Value == null || (bool)rw.Cells[1].Value == false)
                            continue;

                        TLSPN_CottonReceivedBales bales = context.TLSPN_CottonReceivedBales.Find((int)rw.Cells[0].Value);
                        if (bales != null)
                        {
                            bales.CoBales_IssuedToProd = true;
                            bales.CoBales_BlowRoomPosition = 0;
                            bales.CoBales_CottonSequence = Convert.ToInt32(txtIssueToProdNo.Text);

                            NoBales += 1;
                            NettWeight += (decimal)rw.Cells[5].Value;
                            GrossWeight += (decimal)rw.Cells[7].Value;
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                success = false;
                            }
                        }
                    }

                    //------------------------------------------------------------------
                    TLSPN_CottonTransactions cotTrans = new TLSPN_CottonTransactions();
                    cotTrans.cotrx_TransDate = dtpTransDate.Value;
                    cotTrans.cotrx_Supplier_FK = CottonRec.cotrx_Supplier_FK;
                    cotTrans.cotrx_Return_No = Convert.ToInt32(txtIssueToProdNo.Text);
                    cotTrans.cotrx_ContractNo_Fk = CottonRec.cotrx_ContractNo_Fk;
                    cotTrans.cotrx_LotNo = CottonRec.cotrx_LotNo;
                    cotTrans.cotrx_NoBales = NoBales;
                    cotTrans.cotrx_GrossWeight = GrossWeight;
                    cotTrans.cotrx_NetWeight = NettWeight;
                    cotTrans.cotrx_WeighBridgeFull = 0M;
                    cotTrans.cotrx_WeighBridgeEmpty = 0M;
                    cotTrans.cotrx_NettPerWB = 0M;
                    cotTrans.cotrx_GrossAveBaleWeight = (GrossWeight / NoBales);
                    cotTrans.cottrx_NettAveBaleWeight = (NettWeight / NoBales);
                    cotTrans.cotrx_TranType = TranType.TrxT_Pk;
                    cotTrans.cotrx_WriteOff = true;
                
                    //---------------------------------------------
                    // Need to store this to the laydown file
                    //-----------------------------------------------------------
                    TLSPN_YarnOrderLayDown ld = new TLSPN_YarnOrderLayDown();
                    ld.YarnLD_BaleAvgWeight = cotTrans.cottrx_NettAveBaleWeight;
                    ld.YarnLD_Date = dtpTransDate.Value;
                    ld.YarnLD_LayDownNo = Convert.ToInt32(txtIssueToProdNo.Text);
                    ld.YarnLD_LotNo = CottonRec.cotrx_LotNo;
                    ld.YarnLD_NoOfBales = NoBales;
                    ld.YarnLD_WeightKg = NettWeight;

                    try
                    {
                        context.TLSPN_CottonTransactions.Add(cotTrans);
                        context.TLSPN_YarnOrderLayDown.Add(ld);

                        context.SaveChanges();
                        SetUp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        success = false;

                    }
                }

                if (success)
                {
                    MessageBox.Show("Records stored successfully to database");
                }
            }
        }

        private void frmCottonIssueToProd_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                var BlowRowPostion = context.TLSPN_CottonReceivedBales.Where(x => x.CoBales_BlowRoomPosition != 0).Count();
                if (BlowRowPostion != 0)
                {
                    var IssuedtoProduction = context.TLSPN_CottonReceivedBales.Where(x => x.CoBales_IssuedToProd).Count();

                    if (BlowRowPostion != IssuedtoProduction)
                    {
                        DialogResult res = MessageBox.Show("There are some laydown's that have not been saved" + Environment.NewLine + "Do you wish to save", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                            e.Cancel = true;
                    }
                }
            }

        }

        private void dtpTransDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker Pick = sender as DateTimePicker;
            if(Pick != null && formloaded)
            {
                if(Pick.Value > DateTime.Now)
                {
                    MessageBox.Show("Date Time Selected Greater Than Today","Warning");

                }
            }
        }
    }
}
