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
    public partial class frmFabricReversal : Form
    {
        bool FormLoaded = false;

        List<DATA> fieldSelected = null;

        Util core;

        public frmFabricReversal()
        {
            InitializeComponent();

            core = new Util();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn();
            oTxtIndex.HeaderText = "Index";
            oTxtIndex.ValueType = typeof(int);
            oTxtIndex.ReadOnly = true;
            oTxtIndex.Visible = false;

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Piece No";
            oTxtA.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Quality";
            oTxtB.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Tex";
            oTxtC.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Gross";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Nett";
            oTxtE.ValueType = typeof(decimal);

            DataGridViewCheckBoxColumn oChk = new DataGridViewCheckBoxColumn();
            oChk.HeaderText = "Reverse";
            oChk.ValueType = typeof(bool);

            dataGridView1.Columns.Add(oTxtIndex);  //0
            dataGridView1.Columns.Add(oTxtA);      //1
            dataGridView1.Columns.Add(oTxtB);      //2
            dataGridView1.Columns.Add(oTxtC);      //3
            dataGridView1.Columns.Add(oTxtD);      //4
            dataGridView1.Columns.Add(oTxtE);      //5
            dataGridView1.Columns.Add(oChk);       //6
        }

        private void frmFabricReversal_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtGrnNumber.Text = "RFREV" + LNU.col7.ToString().PadLeft(6, '0');
                }

                var Query = from T1 in context.TLDYE_DyeBatch
                            join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                            where T2.DYEBO_Rejected
                            select new { T1.DYEB_Pk, T1.DYEB_BatchNo, T1.DYEB_DyeOrder_FK, T1.DYEB_Colour_FK };

                var QueryGroup = Query.OrderBy(x => x.DYEB_BatchNo).GroupBy(x => x.DYEB_BatchNo);

                
                foreach (var BatchGroup in QueryGroup)
                {
                    TLDYE_DyeBatch DB = new TLDYE_DyeBatch();
                    DB.DYEB_Pk = BatchGroup.FirstOrDefault().DYEB_Pk;
                    DB.DYEB_DyeOrder_FK = BatchGroup.FirstOrDefault().DYEB_DyeOrder_FK;
                    DB.DYEB_BatchNo = BatchGroup.FirstOrDefault().DYEB_BatchNo;
                    DB.DYEB_Colour_FK = BatchGroup.FirstOrDefault().DYEB_Colour_FK;

                    cmboBatchNumber.Items.Add(DB);
                }

                cmboBatchNumber.ValueMember = "DYEB_Pk";
                cmboBatchNumber.DisplayMember = "DYEB_BatchNo";
                cmboBatchNumber.SelectedValue = -1;

                txtBatchFabricKg.Text = "0.0";
                txtBatchGreigeKg.Text = "0.0";

                txtColour.Text = string.Empty;
                txtCustomerDetails.Text = string.Empty;
                txtDyeOrder.Text = string.Empty;

                            
            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Decimal weight = 0.00M;
            bool AddRec = false;
            TLADM_TranactionType TranType = null;


            if (oBtn != null && FormLoaded)
            {
                var Select = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if(Select == null)
                {
                    MessageBox.Show("Please select a batch number from the drop down box ");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                    if (Dept != null)
                    {
                        TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1400).FirstOrDefault();
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        TLDYE_DyeBatchDetails bd = new TLDYE_DyeBatchDetails();

                        int index = (int)row.Cells[0].Value;

                        bd = context.TLDYE_DyeBatchDetails.Find(index);
                        if (bd != null)
                        {
                            if ((bool)row.Cells[6].Value == true)
                            {
                                bd.DYEBO_Rejected = false;
                                bd.DYEBO_RejectedDate = null;
                                bd.DYEBO_QAApproved  = true;
                                bd.DYEBO_ApprovalDate = dtpTransDate.Value;
                                bd.DYEBO_WasRejected = true;

                                weight += (decimal)row.Cells[5].Value;
                                bd.DYEBO_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                AddRec = true;
                            }

                        }
                    }

                    if (AddRec)
                    {
                        var LNU = context.TLADM_LastNumberUsed.Find(3);

                        TLDYE_DyeTransactions tt = new TLDYE_DyeTransactions();
                        tt.TLDYET_BatchNo = Select.DYEB_BatchNo;
                        tt.TLDYET_BatchWeight = Select.DYEB_BatchKG;
                        tt.TLDYET_SequenceNo = Select.DYEB_SequenceNo;
                        tt.TLDYET_Batch_FK = Select.DYEB_Pk;
                        tt.TLDYET_TransactionNumber = "RFREV" + LNU.col7.ToString().PadLeft(6,'0');
                        tt.TLDYET_Date = dtpTransDate.Value;
                        tt.TLDYET_TransactionWeight = weight;
                        tt.TLDYET_TransactionType = (int)TranType.TrxT_Pk;
                        tt.TLDYET_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;                  

                        context.TLDYE_DyeTransactions.Add(tt);

                        try
                        {
                            LNU.col7 += 1;
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to the database");
                            frmDyeViewReport vRep = new frmDyeViewReport(11, tt.TLDYET_Pk, true);
                            vRep.ShowDialog(this);

                            frmFabricReversal_Load(this, null);


                            
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException en)
                        {
                            foreach (var eve in en.EntityValidationErrors)
                            {
                                MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
       }

        private struct DATA
        {
            public int _RowIndex;
            public decimal _WeightGross;
            public decimal _WeightNett;

            public DATA(int Key, decimal WGross, decimal WNett)
            {
                this._RowIndex = Key;
                this._WeightGross = WGross;
                this._WeightNett = WNett;
            }
        }

        private void cmboDyeBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && FormLoaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (selected != null)
                {
                    fieldSelected = new List<DATA>();
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var DO = context.TLDYE_DyeOrder.Find(selected.DYEB_DyeOrder_FK);
                        if (DO != null)
                        {
                            txtDyeOrder.Text = DO.TLDYO_DyeOrderNum;

                            var Cust = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK);
                            if (Cust != null)
                                txtCustomerDetails.Text = Cust.Cust_Description;
                        }

                        var Colour = context.TLADM_Colours.Find(selected.DYEB_Colour_FK);
                        if (Colour != null)
                        {
                            txtColour.Text = Colour.Col_Display;
                        }

                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk && x.DYEBO_Rejected).ToList();
                        foreach (var row in Existing)
                        {
                            if (row.DYEBO_QAApproved)
                                continue;

                            var index = dataGridView1.Rows.Add();

                            var GP = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if (GP != null)
                            {
                                dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = GP.GreigeP_PieceNo;

                                var Qual = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk);
                                if (Qual != null)
                                {
                                    dataGridView1.Rows[index].Cells[2].Value = Qual.TLGreige_Description;
                                }

                                dataGridView1.Rows[index].Cells[4].Value = row.DYEBD_GreigeProduction_Weight;
                                dataGridView1.Rows[index].Cells[5].Value = row.DYEBO_Nett;
                                dataGridView1.Rows[index].Cells[6].Value = row.DYEBO_QAApproved;
                            }
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
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    decimal nett = 0.0M;
                    decimal weight = 0.0M;

                    if ((decimal)oDgv.Rows[e.RowIndex].Cells[4].Value != 0)
                    {
                        weight = (decimal)oDgv.Rows[e.RowIndex].Cells[4].Value;
                    }

                    if ((decimal)oDgv.Rows[e.RowIndex].Cells[5].Value != 0)
                    {
                        nett = (decimal)oDgv.Rows[e.RowIndex].Cells[5].Value;
                    }


                    decimal GrossWeight = Convert.ToDecimal(txtBatchGreigeKg.Text);
                    GrossWeight += weight;
                    txtBatchGreigeKg.Text = Math.Round(GrossWeight, 1).ToString();

                    decimal NettWeight = Convert.ToDecimal(txtBatchFabricKg.Text);
                    NettWeight += nett;
                    txtBatchFabricKg.Text = Math.Round(NettWeight, 1).ToString();

                    var CurrentRow = oDgv.CurrentRow;

                    fieldSelected.Add(new DATA(CurrentRow.Index, weight, nett));

                }
                else
                {
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        var Record = fieldSelected.Find(x => x._RowIndex == CurrentRow.Index);
                        var RecordIndex = fieldSelected.IndexOf(Record);
                        if (RecordIndex != -1)
                        {
                            decimal NettWeight = Convert.ToDecimal(txtBatchFabricKg.Text);
                            NettWeight -= Record._WeightNett;
                            txtBatchFabricKg.Text = Math.Round(NettWeight, 1).ToString();

                            decimal GrossWeight = Convert.ToDecimal(txtBatchGreigeKg.Text);
                            GrossWeight -= Record._WeightGross;
                            txtBatchGreigeKg.Text = Math.Round(GrossWeight, 1).ToString();


                            fieldSelected.RemoveAt(RecordIndex);

                        }
                    }
                }
            }
        }

        private void cmboBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && FormLoaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (selected != null)
                {
                    fieldSelected = new List<DATA>();
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var DO = context.TLDYE_DyeOrder.Find(selected.DYEB_DyeOrder_FK);
                        if (DO != null)
                        {
                            txtDyeOrder.Text = DO.TLDYO_DyeOrderNum;

                            var Cust = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK);
                            if (Cust != null)
                                txtCustomerDetails.Text = Cust.Cust_Description;
                        }

                        var Colour = context.TLADM_Colours.Find(selected.DYEB_Colour_FK);
                        if (Colour != null)
                        {
                            txtColour.Text = Colour.Col_Display;
                        }

                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk && x.DYEBO_Rejected).ToList();
                        foreach (var row in Existing)
                        {
                            if (row.DYEBO_QAApproved)
                                continue;

                            var index = dataGridView1.Rows.Add();

                            var GP = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if (GP != null)
                            {
                                dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = GP.GreigeP_PieceNo;

                                var Qual = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk);
                                if (Qual != null)
                                {
                                    dataGridView1.Rows[index].Cells[2].Value = Qual.TLGreige_Description;
                                }

                                dataGridView1.Rows[index].Cells[4].Value = row.DYEBD_GreigeProduction_Weight;
                                dataGridView1.Rows[index].Cells[5].Value = row.DYEBO_Nett;
                                dataGridView1.Rows[index].Cells[6].Value = row.DYEBO_QAApproved;
                            }
                        }
                    }
                }

            }
        }
    }
}
