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
    public partial class frmYarnPurchased3rdParty : Form
    {
        bool formloaded;
        string[][] MandatoryFields;
        bool[] MandFieldsSelected;

        string[][] MandatoryRows;
        bool[] MandRows;

        int ActiveRow;
        bool TabPressed = false;

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

        public frmYarnPurchased3rdParty()
        {
            using (var context = new TTI2Entities())
            {
               
            }
            MandatoryFields = new string[][]
                {   new string[] {"cmbCommissionCustomers", "Please select a commision customer", "0", "10"},
                    new string[] {"txtCustomerDoc", "Please enter document no details", "1", ""}
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

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Pallet No";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = true;

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Text Count";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Twist";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Identification";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Gross Weight";
            oTxtE.ValueType = typeof(decimal);
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Nett Weight";
            oTxtF.ValueType = typeof(decimal);
            oTxtF.Visible = true;

            oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "No Of Cones";
            oTxtG.ValueType = typeof(int);
            oTxtG.Visible = true;

            oCmbA = new DataGridViewComboBoxColumn();
            oCmbA.HeaderText = "Yarn Type";
            oCmbA.ValueMember = "YA_Id";
            oCmbA.DisplayMember = "YA_Description";

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oCmbA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns.Add(oTxtG);

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            this.dataGridView1.CellLeave += new DataGridViewCellEventHandler(dataGridView1_CellLeave);
            SetUp();
        }

        void SetUp()
        {      

            formloaded = false;
            dataGridView1.Rows.Clear();

            MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);

            using (var context = new TTI2Entities())
            {
                cmbCommissionCustomers.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Description).ToList();
                cmbCommissionCustomers.ValueMember = "Sup_Pk";
                cmbCommissionCustomers.DisplayMember = "Sup_Description";
                cmbCommissionCustomers.SelectedValue = 0;

                oCmbA.DataSource = context.TLADM_Yarn.ToList();

                var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                if (LastNumber != null)
                {
                    txtGrnNumber.Text = "P" + LastNumber.col2.ToString().PadLeft(6, ' ').Replace(' ', '0');
                }
            }

            txtCustomerDoc.Text = string.Empty;
            txtTotalGrossWeight.Text = "0.00";
            txtTotalNettWeight.Text = "0.00";

            formloaded = true;
        }

        private struct DATA
        {
            public int rownumber;
            public bool[] fieldComplete;

            public DATA(int rownumber, bool[] fieldComplete)
            {
                this.rownumber = rownumber;
                this.fieldComplete = fieldComplete;
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal RunningTotal = 0.00M;

                if (e.ColumnIndex == 5 ||
                    e.ColumnIndex == 6)
                {
                    var Cell = dataGridView1.CurrentCell;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
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
                        if (e.ColumnIndex == 6)
                        {
                            if (!String.IsNullOrEmpty(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
                            {
                                var cell = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                                if (cell < Convert.ToDecimal(e.FormattedValue.ToString()))
                                {
                                    MessageBox.Show("Nett Weight cannot be greater than Gross Weight");
                                    oDgv.CancelEdit();
                                    e.Cancel = true;
                                    oDgv.BeginEdit(true);
                                }
                            }
                        }

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
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
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
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                var ActiveRow = -1 + oDgv.RowCount;
                if (ActiveRow >= 0)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
      
            int RecCount = 0;
            int LastNumberUsed = 0;

            TLADM_Suppliers CommissionCust;

            TLKNI_YarnTransaction yarnT; 
            TLKNI_YarnTransactionDetails yarnTD;
            TLADM_TranactionType TranType = null;
            TLKNI_YarnOrderPallets PalletStore;
          
            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandFieldsSelected, false, MandatoryFields);

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

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    var tst = fieldEntered.Find(x => x.rownumber == dr.Index);
                    if (tst.fieldComplete == null)
                    {
                        continue;
                    }

                    var cnt = tst.fieldComplete.Where(x => x == false).Count();
                    if (cnt == MandatoryRows.Length)
                        continue;

                    cnt = tst.fieldComplete.Where(x => x == true).Count();
                    if (cnt != MandatoryRows.Length)
                    {
                        MessageBox.Show("Line " + (1 + dr.Index).ToString() + " Has not been completed correctly");
                        return;
                    }
                }

                CommissionCust = (TLADM_Suppliers)cmbCommissionCustomers.SelectedItem;
                using (var context = new TTI2Entities())
                {
                    yarnT = new TLKNI_YarnTransaction();
                    yarnT.KnitY_TransactionDate = dtpDateReceived.Value;
                    yarnT.KnitY_Customer_FK = CommissionCust.Sup_Pk;
                    yarnT.KnitY_Notes = rtbComments.Text;
                    yarnT.KnitY_TransactionDoc = txtCustomerDoc.Text;
                    yarnT.KnitY_ThirdParty = true;
                                    
                    var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                    if (LastNumber != null)
                    {
                        LastNumberUsed = LastNumber.col2;
                        yarnT.KnitY_GRNNumber = LastNumberUsed;
                        LastNumber.col2 += 1;
                    }

                    var YarnLastNumber = context.TLADM_LastNumberUsed.Find(1);

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Dept != null)
                    {
                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 200).FirstOrDefault();
                    }
                    

                    try
                    {
                        context.TLKNI_YarnTransaction.Add(yarnT);
                        context.SaveChanges();

                        StringBuilder sb = null;
                        int Counter = 0;
                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            var tst = fieldEntered.Find(x => x.rownumber == dr.Index);
                            if (tst.fieldComplete == null)
                                continue;
                            var cnt = tst.fieldComplete.Where(x => x == false).Count();
                            if (cnt == MandatoryRows.Count())
                                continue;

                            var PalletNo = (int)dr.Cells[0].Value;

                            PalletStore = new TLKNI_YarnOrderPallets();

                            PalletStore.TLKNIOP_CommisionCust = false;
                            sb = new StringBuilder();
                            sb.Append(YarnLastNumber.col6.ToString());
                            sb.Append(" - ");
                            sb.Append((++Counter).ToString());
                            PalletStore.TLKNIOP_TLPalletNo = sb.ToString(); 
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
                            yarnTD.KnitYD_KnitY_FK = yarnT.KnitY_Pk;
                            yarnTD.KnitYD_YarnType_FK = (int)dr.Cells[1].Value;
                            yarnTD.KnitYD_PalletNo_FK = PalletStore.TLKNIOP_Pk;
                            yarnTD.KnitYD_GrossWeight = (decimal)dr.Cells[5].Value;
                            yarnTD.KnitYD_NettWeight = (decimal)dr.Cells[6].Value;
                            yarnTD.KnitYD_NoOfCones = (int)dr.Cells[7].Value;
                            yarnTD.KnitYD_TransactionDate = dtpDateReceived.Value;
                            yarnTD.KnitYD_RTS = false;
                            yarnTD.KnitYD_TransactionNumber = LastNumberUsed;
                            
                            if (TranType != null)
                                yarnTD.KnitYD_TransactionType = TranType.TrxT_Pk;
                            yarnTD.KnitYD_RTS = false;

                            context.TLKNI_YarnTransactionDetails.Add(yarnTD);
                           
                            RecCount += 1;
                        }

                        YarnLastNumber = context.TLADM_LastNumberUsed.Find(1);
                        if(YarnLastNumber != null)
                            YarnLastNumber.col6 += 1;
                        
                        context.SaveChanges();

                        formloaded = false;
                        dataGridView1.Rows.Clear();
                        formloaded = true;

                        MessageBox.Show("Records stored successfully to database");
                        
                        frmKnitViewRep vRep = new frmKnitViewRep(3, yarnT.KnitY_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                        SetUp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                      
                    }
                }
               
            }
        }

        private void txt_Changed(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null & formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    var nbr = Convert.ToInt32(result[2]);
                    MandFieldsSelected[nbr] = true;
                }
            }
        }

        private void cmbCommissionCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    var nbr = Convert.ToInt32(result[2]);
                    MandFieldsSelected[nbr] = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                YarnReportOptions opts = new YarnReportOptions();
                opts.reportChoice = 2;

                frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
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
