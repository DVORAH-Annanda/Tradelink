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
using Barcodes;


namespace Knitting
{
    public partial class frmComKnitting : Form
    {
        string[][] MandatoryFields;
        string[][] MandatoryRows;

        bool[] MandSelected;
        bool[] MandRows;

        bool formloaded;
        bool error;
        Util core;
                

        List<DATA> fieldEntered = new List<DATA>();

        DataGridViewTextBoxColumn oTxtA;   // Palet No
        DataGridViewTextBoxColumn oTxtB;   // Text Count
        DataGridViewTextBoxColumn oTxtC;   // Twist Factor 
        DataGridViewTextBoxColumn oTxtD;   // identification
        DataGridViewTextBoxColumn oTxtE;   // Gross
        DataGridViewTextBoxColumn oTxtF;   // Nett
        DataGridViewTextBoxColumn oTxtG;   // No Of Cones
  
        DataGridViewComboBoxColumn oCmbA;  // Yarn Type

        DataTable DT = new DataTable();

        public frmComKnitting()
        {
            MandatoryFields = new string[][]
                {   new string[] {"cmbCommissionCustomers", "Please select a customer number from the drop downlist", "0"},
                    new string[] {"txtCustomerDoc", "Please enter a commission customer reference number", "1"}
                };

            MandatoryRows = new string[][]
                {   new string[] {"0", "Please enter a pallet number", "0"},
                    new string[] {"5", "Please enter the gross weight", "1"},
                    new string[] {"6", "Please enter the nett weight", "2"},
                    new string[] {"7", "Please enter the number of cones", "3"}
                };
            
            core = new Util();
      
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
           

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Palett Number 
            oTxtA.HeaderText = "Pallet No";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = true;

            oTxtB = new DataGridViewTextBoxColumn();   // 2 Text Count
            oTxtB.HeaderText = "Text Count";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 3 Twist 
            oTxtC.HeaderText = "Twist";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 4 Identification
            oTxtD.HeaderText = "Identification";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 5 Gross Weight 
            oTxtE.HeaderText = "Gross Weight";
            oTxtE.ValueType = typeof(decimal);
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();   // 6 Nett Weight 
            oTxtF.HeaderText = "Nett Weight";
            oTxtF.ValueType = typeof(decimal);
            oTxtF.Visible = true;

            oTxtG = new DataGridViewTextBoxColumn();   // 7 No of Cones 
            oTxtG.HeaderText = "No Of Cones";
            oTxtG.ValueType = typeof(int);
            oTxtG.Visible = true;

            oCmbA = new DataGridViewComboBoxColumn(); // 1 Yarn Selection 
            oCmbA.HeaderText = "Yarn Type";
            oCmbA.ValueMember = "YA_Id";
            oCmbA.DisplayMember = "YA_Description";

            dataGridView1.Columns.Add(oTxtA);    // Pallet No 
            dataGridView1.Columns.Add(oCmbA);    // Yarn Selection  
            dataGridView1.Columns.Add(oTxtB);    // Tex Count
            dataGridView1.Columns.Add(oTxtC);    // Twist  
            dataGridView1.Columns.Add(oTxtD);    // Identification
            dataGridView1.Columns.Add(oTxtE);    // Gross Weight 
            dataGridView1.Columns.Add(oTxtF);    // Nett Weight
            dataGridView1.Columns.Add(oTxtG);    // No of cones
           
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.CellLeave += new DataGridViewCellEventHandler(dataGridView1_CellLeave);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
      
            SetUp();
        }
        
        void SetUp()
        {
       
            formloaded = false;
            
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            
            using (var context = new TTI2Entities())
            {
                cmbCommissionCustomers.DataSource = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust == true).ToList();
                cmbCommissionCustomers.ValueMember = "Cust_Pk";
                cmbCommissionCustomers.DisplayMember = "Cust_Description";
                cmbCommissionCustomers.SelectedValue = 0;
            
                oCmbA.DataSource = context.TLADM_Yarn.ToList();

                var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                if (LastNumber != null)
                {
                    txtGrnNumber.Text = "CY" + LastNumber.col1.ToString().PadLeft(6, ' ').Replace(' ', '0');
                }
            }

            txtCustomerDoc.Text = string.Empty;
            txtTotalNettWeight.Text = "0.00";
            txtTotalGrossWeight.Text = "0.00";
            formloaded = true;
        }

       

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (e.ColumnIndex == 0 || 
                e.ColumnIndex == 5 ||
                e.ColumnIndex == 6 || 
                e.ColumnIndex == 7)
            {
                var record = fieldEntered.Find(x => x.rownumber == e.RowIndex);
                if (record.fieldComplete != null)
                {

                    if (e.ColumnIndex == 0)
                    {

                        if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {
                            var result = (from u in MandatoryRows
                                          where u[0] == e.ColumnIndex.ToString()
                                          select u).FirstOrDefault();

                            if (result != null)
                            {
                                MessageBox.Show(result[1]);
                            }
                            e.Cancel = true;
                        }
                        else
                        {

                            var index = fieldEntered.IndexOf(record);

                            var result = (from u in MandatoryRows
                                              where u[0] == e.ColumnIndex.ToString()
                                              select u).FirstOrDefault();

                            if (result != null)
                            {
                                    int a = Convert.ToInt32(result[2]);
                                    record.fieldComplete[a] = true;
                            }

                            fieldEntered[index] = record;
                            

                        }
                    }
                    else if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        if (!String.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {
                            if (Convert.ToDecimal(e.FormattedValue.ToString()) == 0)
                            {
                                var result = (from u in MandatoryRows
                                              where u[0] == e.ColumnIndex.ToString()
                                              select u).FirstOrDefault();

                                if (result != null)
                                {
                                    MessageBox.Show(result[1]);
                                }
                                e.Cancel = true;
                            }
                            else
                            {
                                if (e.ColumnIndex == 6)
                                {
                                    if (!String.IsNullOrEmpty(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
                                    {
                                        var cell = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                                        if (cell < Convert.ToDecimal(e.FormattedValue.ToString()))
                                        {
                                            MessageBox.Show("Nett Weight cannot be greater than Gross Weight");
                                            dataGridView1.CancelEdit();
                                            e.Cancel = true;
                                            dataGridView1.BeginEdit(true);
                                        }


                                    }
                                }

                                var index = fieldEntered.IndexOf(record);

                                var result = (from u in MandatoryRows
                                                  where u[0] == e.ColumnIndex.ToString()
                                                  select u).FirstOrDefault();

                                 if (result != null)
                                 {
                                        int a = Convert.ToInt32(result[2]);
                                        record.fieldComplete[a] = true;
                                       
                                 }

                                 fieldEntered[index] = record;
                            }
                        }
                    }
                }
           }

        }

       
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            try
            {
                decimal RunningTotal = 0.00M;
                if (e.ColumnIndex == 5 ||
                    e.ColumnIndex == 6)
                {
                    var Cell = dataGridView1.CurrentCell;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if(!String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
                             RunningTotal += Convert.ToDecimal(row.Cells[e.ColumnIndex].EditedFormattedValue);
                    }

                    if (e.ColumnIndex == 5)
                        txtTotalGrossWeight.Text = RunningTotal.ToString();
                    else
                        txtTotalNettWeight.Text = RunningTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cmb_MeasurementSelectedIndexchanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int mnbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[mnbr] = true;
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
                    MandSelected[nbr] = true;
                else
                {
                    MandSelected[nbr] = false;
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
                    if (Cell.ColumnIndex == 5 || Cell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 0 || Cell.ColumnIndex == 7)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else if (combo != null)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var cell = dataGridView1.CurrentCell;
            if (cb != null)
            {
                try
                {
                    using (var context = new TTI2Entities())
                    {
                        var selectedYarn = (TLADM_Yarn)cb.SelectedItem;
                        if (selectedYarn != null)
                        {
                            var YarnDetails = context.TLADM_Yarn.Where(x => x.YA_Id == selectedYarn.YA_Id).FirstOrDefault();
                            if (YarnDetails != null)
                            {
                                dataGridView1.Rows[cell.RowIndex].Cells[2].Value = YarnDetails.YA_TexCount;
                                dataGridView1.Rows[cell.RowIndex].Cells[3].Value = YarnDetails.YA_Twist;
                                dataGridView1.Rows[cell.RowIndex].Cells[4].Value = YarnDetails.YA_ConeColour;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       

        private struct DATA
        {
            public int rownumber;
            public bool[] fieldComplete;

            public DATA(int rownumber, bool[] fieldComplete)
            {
                this.rownumber   = rownumber;
                this.fieldComplete = fieldComplete;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

                     
            TLADM_CustomerFile CommissionCust;
            TLKNI_YarnTransaction yarnT;
            TLKNI_YarnTransactionDetails yarnTD;
            //TLKNI_YarnStatus yarnStat;
            TLADM_TranactionType TranType = null;
            TLKNI_YarnOrderPallets PalletStore;

            int LastNumberUsed = 0;

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, false, MandatoryFields);

                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("Please enter a least one row in the datagrid");
                    return;
                }
                
                CommissionCust = (TLADM_CustomerFile)cmbCommissionCustomers.SelectedItem;
                using ( var context = new TTI2Entities())
                {
                   
                    //-------------------------
                    //  Yarn Header Record
                    //----------------------------------------
                    yarnT = new TLKNI_YarnTransaction();
                    yarnT.KnitY_TransactionDate    = dtpDateReceived.Value;
                    yarnT.KnitY_Customer_FK        = CommissionCust.Cust_Pk;
                    yarnT.KnitY_Notes              = rtbComments.Text;
                    yarnT.KnitY_TransactionDoc     = txtCustomerDoc.Text;
                    yarnT.KnitY_ThirdParty = false;
                    yarnT.KnitY_RTS = false;

                    var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                    if (LastNumber != null)
                    {
                        LastNumberUsed        = LastNumber.col1;
                        yarnT.KnitY_GRNNumber = LastNumberUsed;
                        LastNumber.col1 += 1;
                    }

                    var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if(Dept != null)
                    {
                        TranType = context.TLADM_TranactionType.Where(x=>x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 100).FirstOrDefault();
                    }
                    

                    try
                    {
                        context.TLKNI_YarnTransaction.Add(yarnT);
                        context.SaveChanges();


                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            if (dr.Cells[1].Value == null)
                                continue;
                            
                            var PalletNo = (int)dr.Cells[0].Value;

                            PalletStore = new TLKNI_YarnOrderPallets();

                            PalletStore.TLKNIOP_CommisionCust = true;
                            PalletStore.TLKNIOP_Cones = (int)dr.Cells[7].Value;
                            PalletStore.TLKNIOP_DatePacked = dtpDateReceived.Value;
                            PalletStore.TLKNIOP_Grade = string.Empty;
                            PalletStore.TLKNIOP_GrossWeight = (decimal)dr.Cells[5].Value;
                            PalletStore.TLKNIOP_TareWeight = 0.00M;
                            PalletStore.TLKNIOP_NettWeight = (decimal)dr.Cells[6].Value;
                            PalletStore.TLKNIOP_YarnType_FK = (int)dr.Cells[1].Value;
                            PalletStore.TLKNIOP_PalletNo = PalletNo;
                            PalletStore.TLKNIOP_Store_FK = (int)TranType.TrxT_ToWhse_FK;
                            PalletStore.TLKNIOP_NettWeightReserved = 0.00M;
                            PalletStore.TLKNIOP_HeaderRecord_FK = yarnT.KnitY_Pk;
                            PalletStore.TLKNIOP_ConesReserved = 0;

                            context.TLKNI_YarnOrderPallets.Add(PalletStore);
                            context.SaveChanges();

                            yarnTD = new TLKNI_YarnTransactionDetails();
                            yarnTD.KnitYD_KnitY_FK     = yarnT.KnitY_Pk;
                            yarnTD.KnitYD_YarnType_FK  = (int)dr.Cells[1].Value;
                            yarnTD.KnitYD_PalletNo_FK     = PalletStore.TLKNIOP_Pk;
                            yarnTD.KnitYD_GrossWeight  = (decimal)dr.Cells[5].Value;
                            yarnTD.KnitYD_NettWeight   = (decimal)dr.Cells[6].Value;
                            yarnTD.KnitYD_NoOfCones    = (int)dr.Cells[7].Value;
                            yarnTD.KnitYD_TransactionDate = dtpDateReceived.Value;
                            yarnTD.KnitYD_RTS = false;
                            yarnTD.KnitYD_TransactionNumber = LastNumberUsed;
                            if (TranType != null)
                                yarnTD.KnitYD_TransactionType = TranType.TrxT_Pk;
                            yarnTD.KnitYD_RTS = false;
                          
                            context.TLKNI_YarnTransactionDetails.Add(yarnTD);
                          
                        }

                        context.SaveChanges();

                        dataGridView1.Rows.Clear();
                        MessageBox.Show("Records stored successfully to database");
                        
                        frmKnitViewRep vRep = new frmKnitViewRep(1, yarnT.KnitY_Pk);
                        vRep.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                       
                    }
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                YarnReportOptions opts = new YarnReportOptions();
                opts.reportChoice = 1;

                frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                vRep.ShowDialog(this);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                if (e.ColumnIndex == 7 )
                {
                    

                }
            }
        }

        private void Cell_BeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
               

            }
        }

        private void dtpDateReceived_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
              
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                
            }

        }

        private void dataGridView1_CellLeave_1(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                var ActiveRow = -1 + oDgv.RowCount;
                if (ActiveRow >= 0 && oDgv.Columns.Count > 7)
                {
                    MandRows = core.PopulateArray(MandatoryRows.Length, false);
                    oDgv.Rows[ActiveRow].Cells[5].Value = 0.00;
                    oDgv.Rows[ActiveRow].Cells[6].Value = 0.00;
                    oDgv.Rows[ActiveRow].Cells[7].Value = 0;

                    // fieldEntered.Add(new DATA(ActiveRow, MandRows));

                    // dataGridView1.Rows[ActiveRow].Cells[0].Selected = true;
                    // dataGridView1.BeginEdit(true);
                    // dataGridView1.Focus();
                }


            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {

                // Edit mode
                var Cell = oDgv.CurrentCell;

                if (Cell.ColumnIndex == 0)
                {
                    var record = fieldEntered.Find(x => x.rownumber == Cell.RowIndex);
                    var index = fieldEntered.IndexOf(record);

                    if (index == -1)
                    {
                        var CurrentRow = oDgv.CurrentRow;
                        MandRows = core.PopulateArray(MandatoryRows.Length, false);

                        fieldEntered.Add(new DATA(CurrentRow.Index, MandRows));
                    }
                }

            }
        }

    }
    
}
