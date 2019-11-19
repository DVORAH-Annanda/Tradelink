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
    public partial class frmYarnReturnToSupplier : Form
    {
        string[][] MandatoryFields;
        bool[] MandatoryFieldsSelected;

        bool[] rowTicked;

        
    
        bool formloaded;
        Util core;
        int _reportNo;

        DataGridViewTextBoxColumn oTxtA;   // Table Pk  0
        DataGridViewTextBoxColumn oTxtB;   // Pallet No  1
        DataGridViewTextBoxColumn oTxtC;   // Yarn Type   2
        DataGridViewTextBoxColumn oTxtD;   // Tex Count   3
        DataGridViewTextBoxColumn oTxtE;   // Twist       4
        DataGridViewTextBoxColumn oTxtF;   // Identification  5
        DataGridViewTextBoxColumn oTxtG;   // Gross Weight    6 
        DataGridViewTextBoxColumn oTxtH;   // Nett Weight     7
        DataGridViewTextBoxColumn oTxtJ;   // no Of Cones     8
        
        DataGridViewCheckBoxColumn oChkA;  // select
        
     

        public frmYarnReturnToSupplier(int ReportNo)
        {
            InitializeComponent();

            _reportNo = ReportNo;

            if (_reportNo == 1)
                this.Text = "Yarn Returned to Suppliers (3rd Party purchase)";
            else
                this.Text = "Yarn Returned to Customers (Commission Knitting)"; 

            MandatoryFields = new string[][]
                {   new string[] {"cmboYarnOrder", "Please enter a contract number", "0", "10"},
                    new string[] {"txtApprovedBy", "Please enter the approved by details", "1", ""}
                };

           

            core = new Util();

            dataGridView1.AutoGenerateColumns = false;

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Primary Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            oChkA.Visible = true;

           
            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Pallet No";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Yarn Type";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Tex Count";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Twist";
            oTxtE.ValueType = typeof(string);
            oTxtE.Visible = true;
            oTxtE.ReadOnly = true;

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Identification";
            oTxtF.ValueType = typeof(string);
            oTxtF.Visible = true;
            oTxtF.ReadOnly = true;

            oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "Gross Weight";
            oTxtG.ValueType = typeof(decimal);
            oTxtG.Visible = true;

            oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.HeaderText = "Nett Weight";
            oTxtH.ValueType = typeof(decimal);
            oTxtH.Visible = true;

            oTxtJ = new DataGridViewTextBoxColumn();
            oTxtJ.HeaderText = "No Of Cones";
            oTxtJ.ValueType = typeof(int);
            oTxtJ.Visible = true;
            
        
            dataGridView1.Columns.Add(oTxtA);  // Primary Key 0
            dataGridView1.Columns.Add(oChkA);  // Check Box  1
           
            dataGridView1.Columns.Add(oTxtB);  // Pallet No 2
            dataGridView1.Columns.Add(oTxtC);  // Yarn Type 3
            dataGridView1.Columns.Add(oTxtD);  // Text Count 4 
            dataGridView1.Columns.Add(oTxtE);  // Twist  5
            dataGridView1.Columns.Add(oTxtF);  // Identification 6
            dataGridView1.Columns.Add(oTxtG);  // Gross Weight 7
            dataGridView1.Columns.Add(oTxtH);  // Nett Weight 8
            dataGridView1.Columns.Add(oTxtJ);  // No Of Cones 9
         

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);
            // dataGridView1.CellLeave += new DataGridViewCellEventHandler(DataGridView1_ColumnsSummed);

            SetUp();
          
        }
        void SetUp()
        {

            formloaded = false;
            MandatoryFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);
            IList<TLKNI_YarnTransaction> Transactions = null;
            using (var context = new TTI2Entities())
            {
                
                if (_reportNo == 1)
                {
                    Transactions = (from Trans in context.TLKNI_YarnTransaction
                                        join Pallets in context.TLKNI_YarnOrderPallets on Trans.KnitY_Pk equals Pallets.TLKNIOP_HeaderRecord_FK
                                        where !Trans.KnitY_RTS && Trans.KnitY_ThirdParty && !Pallets.TLKNIOP_PalletAllocated
                                        select Trans).ToList(); 
                }
                else
                {
                    Transactions = (from Trans in context.TLKNI_YarnTransaction
                                        join Pallets in context.TLKNI_YarnOrderPallets on Trans.KnitY_Pk equals Pallets.TLKNIOP_HeaderRecord_FK
                                        where !Trans.KnitY_RTS && !Trans.KnitY_ThirdParty && !Pallets.TLKNIOP_PalletAllocated
                                        select Trans).ToList();
                }

                cmboYarnOrder.DataSource = Transactions.GroupBy(x=>x.KnitY_GRNNumber).Select(y=>y.First()).ToList();
                cmboYarnOrder.DisplayMember = "KnitY_GRNNumber";
                cmboYarnOrder.ValueMember = "KnitY_Pk";
                cmboYarnOrder.SelectedValue = -1;
                
                var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                if (LastNumber != null)
                {
                    txtGrnNumber.Text = "D" + LastNumber.col3.ToString().PadLeft(6, ' ').Replace(' ', '0');
                }
            }

            txtTotalGrossWeight.Text = "0.00";
            txtTotalNettWeight.Text  = "0.00";

            rtbNotes.Text = string.Empty;
            txtApprovedBy.Text = string.Empty;

            formloaded = true;
        }
        /*
        private void DataGridView1_ColumnsSummed(object sender, DataGridViewCellEventHandler e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                decimal Total = 0.00M;

                foreach (DataGridViewRow dr in oDgv.Rows)
                {
                    if ((bool)dr.Cells[1].Value == false)
                        continue;


                }
            }
        }
        */

        private void Check_Changed(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var Cell = oDgv.CurrentCell;

                if ((bool)Cell.EditedFormattedValue)
                {
                    var ActiveRow = oDgv.CurrentRow;
                    var TotalGW = Convert.ToDecimal(txtTotalGrossWeight.Text);
                    TotalGW += (decimal)dataGridView1.Rows[Cell.RowIndex].Cells[7].Value;
                    txtTotalGrossWeight.Text = TotalGW.ToString();

                    var TotalNW = Convert.ToDecimal(txtTotalNettWeight.Text);
                    TotalNW += (decimal)dataGridView1.Rows[Cell.RowIndex].Cells[8].Value;
                    txtTotalNettWeight.Text = TotalNW.ToString();

                    rowTicked[ActiveRow.Index] = true;

                }
                else
                {
                    var ActiveRow = oDgv.CurrentRow;
                    if(rowTicked[ActiveRow.Index])
                    {
                        var TotalGW = Convert.ToDecimal(txtTotalGrossWeight.Text);
                        TotalGW -= (decimal)dataGridView1.Rows[Cell.RowIndex].Cells[7].Value;
                        txtTotalGrossWeight.Text = TotalGW.ToString();

                        var TotalNW = Convert.ToDecimal(txtTotalNettWeight.Text);
                        TotalNW -= (decimal)dataGridView1.Rows[Cell.RowIndex].Cells[8].Value;
                        txtTotalNettWeight.Text = TotalNW.ToString();

                        rowTicked[ActiveRow.Index] = false;
                    }
                }

            }
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
                    if (Cell.ColumnIndex == 7 || Cell.ColumnIndex == 8)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 1 || Cell.ColumnIndex == 9)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                   

                }
                
            }
        }

       
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                var ActiveRow = oDgv.CurrentRow;
                if (ActiveRow != null)
                {
                }
               
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 7 ||
                e.ColumnIndex == 8 ||
                e.ColumnIndex == 9)
            {
                if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                {
                    if (Convert.ToDecimal(e.FormattedValue.ToString()) == 0)
                    {
                        var result = (from u in MandatoryFields
                                      where u[0] == e.ColumnIndex.ToString()
                                      select u).FirstOrDefault();

                        if (result != null)
                        {
                            MessageBox.Show(result[1]);
                        }
                        e.Cancel = true;
                    }
               }
            }
        }

        private void txt(object sender, EventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;
            if (oTxtBx != null && formloaded)
            {

                var result = (from u in MandatoryFields
                              where u[0] == oTxtBx.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                if (oTxtBx.TextLength > 0)
                    MandatoryFieldsSelected[nbr] = true;
                else
                {
                    MandatoryFieldsSelected[nbr] = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLKNI_YarnTransactionDetails yarnTD;
            TLADM_TranactionType TranType = null;
            int LastNumberUsed = 0;
            bool success = true;

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatoryFieldsSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                var YO = (TLKNI_YarnTransaction)cmboYarnOrder.SelectedItem;
                if(YO != null)
                {
                    using (var context = new TTI2Entities())
                    {

                        var LNU = context.TLADM_LastNumberUsed.Find(2);
                        if (LNU != null)
                        {
                            LastNumberUsed = LNU.col3;
                            LNU.col3 += 1;
                        }

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                        if (Dept != null)
                        {
                            if (_reportNo == 1)
                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 400).FirstOrDefault();
                            else
                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 300).FirstOrDefault();
                        }

                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            if (!(bool)dr.Cells[1].Value)
                                continue;

                            yarnTD = new TLKNI_YarnTransactionDetails();
                            yarnTD.KnitYD_KnitY_FK = YO.KnitY_Pk;

                            var TypeOfYarn = dr.Cells[3].Value;
                            var YarnDetails = context.TLADM_Yarn.Where(x => x.YA_Description.Contains(TypeOfYarn.ToString())).FirstOrDefault();
                            if (YarnDetails != null)
                                yarnTD.KnitYD_YarnType_FK = YarnDetails.YA_Id;

                            yarnTD.KnitYD_PalletNo_FK = (int)dr.Cells[0].Value;
                            yarnTD.KnitYD_GrossWeight = (decimal)dr.Cells[7].Value;
                            yarnTD.KnitYD_NettWeight = (decimal)dr.Cells[8].Value;
                            yarnTD.KnitYD_NoOfCones = (int)dr.Cells[9].Value;
                            yarnTD.KnitYD_WriteOff = true;
                            yarnTD.KnitYD_RTS = true;
                            yarnTD.KnitYD_TransactionDate = dtpDateReceived.Value;
                            yarnTD.KnitYD_TransactionNumber = LastNumberUsed;
                            if (TranType != null)
                                yarnTD.KnitYD_TransactionType = TranType.TrxT_Pk;

                            context.TLKNI_YarnTransactionDetails.Add(yarnTD);

                            //--------------------------------------------------------------------
                            // Now we have to keep the Status file uptodate
                            //--------------------------------------------------------------------------
                            TLKNI_YarnOrderPallets palletStore = context.TLKNI_YarnOrderPallets.Find(yarnTD.KnitYD_PalletNo_FK);
                            if (palletStore != null)
                            {
                                 palletStore.TLKNIOP_Cones -= yarnTD.KnitYD_NoOfCones;
                                 palletStore.TLKNIOP_NettWeight -= yarnTD.KnitYD_NettWeight;
                                 palletStore.TLKNIOP_GrossWeight -= yarnTD.KnitYD_GrossWeight;
                                 palletStore.TLKNIOP_PalletAllocated = true;
                            }
                       }

                        try
                        {
                            context.SaveChanges();
                            frmKnitViewRep vRep = new frmKnitViewRep(4, LastNumberUsed);
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
                    if (success)
                    {
                        dataGridView1.Rows.Clear();
                        MessageBox.Show("Data saved to database successfully");
                        SetUp();

                    }
                    
                }
            }
        }

        private void cmboYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var OrderSelected = (TLKNI_YarnTransaction)cmboYarnOrder.SelectedItem;
                if (OrderSelected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        if (_reportNo != 1)
                        {
                            var Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnReceived_FK == OrderSelected.KnitY_Pk && !x.YarnOP_PalletReserved).ToList();
                            if (Pallets != null)
                            {
                                dataGridView1.Rows.Clear();

                                foreach (var Pallet in Pallets)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = Pallet.YarnOP_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = false;
                                    dataGridView1.Rows[index].Cells[2].Value = Pallet.YarnOP_PalletNo;

                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Pallet.YarnOP_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        dataGridView1.Rows[index].Cells[3].Value = YarnType.YA_Description;
                                        dataGridView1.Rows[index].Cells[4].Value = YarnType.YA_TexCount.ToString();
                                        dataGridView1.Rows[index].Cells[5].Value = YarnType.YA_Twist.ToString();
                                        dataGridView1.Rows[index].Cells[6].Value = YarnType.YA_ConeColour;
                                    }

                                    dataGridView1.Rows[index].Cells[7].Value = Math.Round(Pallet.YarnOP_GrossWeight, 2);
                                    dataGridView1.Rows[index].Cells[8].Value = Math.Round(Pallet.YarnOP_NettWeight, 2);
                                    dataGridView1.Rows[index].Cells[9].Value = Pallet.YarnOP_NoOfConesSpun;
                                }

                                rowTicked = core.PopulateArray(dataGridView1.Rows.Count, false);

                                var result = (from u in MandatoryFields
                                              where u[0] == oCmbo.Name
                                              select u).FirstOrDefault();

                                if (result != null)
                                {
                                    int mnbr = Convert.ToInt32(result[2].ToString());
                                    MandatoryFieldsSelected[mnbr] = true;
                                }

                            }

                        }
                        else
                        {
                            var Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_HeaderRecord_FK == OrderSelected.KnitY_Pk && !x.TLKNIOP_PalletAllocated).ToList();
                            if (Pallets != null)
                            {
                                dataGridView1.Rows.Clear();

                                foreach (var Pallet in Pallets)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = false;
                                    dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;

                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Pallet.TLKNIOP_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        dataGridView1.Rows[index].Cells[3].Value = YarnType.YA_Description;
                                        dataGridView1.Rows[index].Cells[4].Value = YarnType.YA_TexCount.ToString();
                                        dataGridView1.Rows[index].Cells[5].Value = YarnType.YA_Twist.ToString();
                                        dataGridView1.Rows[index].Cells[6].Value = YarnType.YA_ConeColour;
                                    }

                                    dataGridView1.Rows[index].Cells[7].Value = Math.Round(Pallet.TLKNIOP_GrossWeight, 2);
                                    dataGridView1.Rows[index].Cells[8].Value = Math.Round(Pallet.TLKNIOP_NettWeight, 2);
                                    dataGridView1.Rows[index].Cells[9].Value = Pallet.TLKNIOP_Cones;
                                }

                                rowTicked = core.PopulateArray(dataGridView1.Rows.Count, false);

                                var result = (from u in MandatoryFields
                                              where u[0] == oCmbo.Name
                                              select u).FirstOrDefault();

                                if (result != null)
                                {
                                    int mnbr = Convert.ToInt32(result[2].ToString());
                                    MandatoryFieldsSelected[mnbr] = true;
                                }

                            }
                        }
                    }
                         
                }

            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    decimal Total = 0.00M;

                    foreach (DataGridViewRow dr in oDgv.Rows)
                    {

                        if ((bool)dr.Cells[1].Value == false)
                            continue;

                        if (e.ColumnIndex == 6)
                        {
                            Total += Convert.ToDecimal(dr.Cells[6].EditedFormattedValue.ToString());
                        }
                        else if (e.ColumnIndex == 7)
                        {
                            Total += Convert.ToDecimal(dr.Cells[7].EditedFormattedValue.ToString());
                        }

                    }

                    if (e.ColumnIndex == 6)
                        txtTotalGrossWeight.Text = Total.ToString();
                    else
                        txtTotalNettWeight.Text = Total.ToString();
                }
            }
        }
        
    }
}
