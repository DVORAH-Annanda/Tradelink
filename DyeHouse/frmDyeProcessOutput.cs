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
    public partial class frmDyeProcessOutput : Form
    {
        bool formloaded;
        Util core;

        public frmDyeProcessOutput()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            
            core = new Util();
            Setup();

        }

        void Setup()
        {
            formloaded = false;
            
            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                cmboBatchNumber.DataSource = context.TLDYE_DyeBatch.Where(x=>x.DYEB_Transfered && x.DYEB_Stage3 && !x.DYEB_OutProcess).OrderBy(x=>x.DYEB_BatchNo).ToList();
                cmboBatchNumber.ValueMember = "DYEB_Pk";
                cmboBatchNumber.DisplayMember = "DYEB_BatchNo";
                cmboBatchNumber.SelectedValue = -1;
            }

            txtBatchFabricKg.Text = "0.0";
            txtBatchGreigeKg.Text = "0.0";

            txtColour.Text = string.Empty;
            txtCustomerDetails.Text = string.Empty;
            txtDyeOrder.Text = string.Empty;

            formloaded = true;

        }

        private void cmboBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            bool first = true;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (selected != null)
                {
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

                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk).OrderBy(x=>x.DYEBD_GreigeProduction_FK).ToList();
                        foreach (var row in Existing)
                        {
                            if (first)
                            {
                                if(row.DYEBO_DyeDate != null)
                                     dtpDyeDate.Value =  (DateTime)row.DYEBO_DyeDate;
                                if(row.DYEBO_TransDate != null)
                                    dtpTransDate.Value = (DateTime)row.DYEBO_TransDate;
                            }

                            var GP = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                            if (GP != null)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = GP.GreigeP_PieceNo;

                                var Qual = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk);
                                if (Qual != null)
                                {
                                    dataGridView1.Rows[index].Cells[2].Value = Qual.TLGreige_Description;
                                }

                                dataGridView1.Rows[index].Cells[4].Value = row.DYEBD_GreigeProduction_Weight;
                                dataGridView1.Rows[index].Cells[5].Value = row.DYEBO_Nett;
                                dataGridView1.Rows[index].Cells[6].Value = row.DYEBO_DiskWeight;
                                dataGridView1.Rows[index].Cells[7].Value = row.DYEBO_Width;
                                dataGridView1.Rows[index].Cells[8].Value = row.DYEBO_Meters;

                                if (first)
                                    first = !first;
                            }
                        }

                        if (this.dataGridView1.Rows.Count != 0)
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[5];
                            this.dataGridView1.BeginEdit(true);
                        }
                    }
                }
            }
        }

        private void frmDyeProcessOutput_Load(object sender, EventArgs e)
        {
            formloaded = false;
            DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn(); // 0
            oTxtIndex.HeaderText = "Index";
            oTxtIndex.ValueType = typeof(int);
            oTxtIndex.ReadOnly = true;
            oTxtIndex.Visible = false;

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();      // 1
            oTxtA.HeaderText = "Piece No";
            oTxtA.ReadOnly = true;
            
            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();      //  2
            oTxtB.HeaderText = "Quality";
            oTxtB.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // 3
            oTxtC.HeaderText = "Tex";
            oTxtC.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();     //  4
            oTxtD.HeaderText = "Gross";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();      // 5
            oTxtE.HeaderText = "Nett";
            oTxtE.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Disk Weight";
            oTxtF.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "Width";
            oTxtG.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.HeaderText = "Meters";
            oTxtH.ReadOnly = true;
            oTxtH.ValueType = typeof(decimal);

            dataGridView1.Columns.Add(oTxtIndex);
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns.Add(oTxtG);
            dataGridView1.Columns.Add(oTxtH);
            formloaded = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var currentCell = oDgv.CurrentCell;

                if (currentCell.ColumnIndex == 5 || 
                    currentCell.ColumnIndex == 6 || 
                    currentCell.ColumnIndex == 7)
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
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
          
            if (oDgv != null && formloaded)
            {
                if (e.ColumnIndex == 5 || 
                    e.ColumnIndex == 6 || 
                    e.ColumnIndex == 7)
                {
                    String CellValue = oDgv.CurrentRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString();
                    if (String.IsNullOrEmpty(CellValue) || Convert.ToDecimal(CellValue) == 0.00M)
                    {
                        MessageBox.Show("No value entered","Error");
                        e.Cancel = true;
                        return;
                    }
                }

                if (e.ColumnIndex == 5 )
                {
                    Decimal NettValue = Convert.ToDecimal(oDgv.CurrentRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString());
                    Decimal GrossValue = Convert.ToDecimal(oDgv.CurrentRow.Cells[-1 + e.ColumnIndex].Value.ToString());

                    decimal ans = core.CalculateVariance(GrossValue, NettValue);
                    if( ans > 5.00M || ans < -5.00M )
                    {
                        DialogResult res = MessageBox.Show("Possible incorrect amount entered -- Continue?", "Possible error variance " + Math.Round(ans,1) + " % ",  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (formloaded)
            {
                if (e.ColumnIndex == 7)
                {
                    decimal nett = 0.0M;
                    decimal weight = 0.0M;
                    decimal width = 0.0M;

                    if ((decimal)oDgv.Rows[e.RowIndex].Cells[5].Value != 0)
                    {
                        nett = (decimal)oDgv.Rows[e.RowIndex].Cells[5].Value;
                    }

                    if ((decimal)oDgv.Rows[e.RowIndex].Cells[6].Value != 0)
                    {
                        weight = (decimal)oDgv.Rows[e.RowIndex].Cells[6].Value;
                    }

                    if (Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].EditedFormattedValue.ToString()) != 0)
                    {
                        width = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].EditedFormattedValue.ToString());
                    }

                    if (weight > 0 && width > 0 && nett > 0)
                    {
                        oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(core.FabricYield(weight, width) * nett, 2);

                        decimal GrossWeight = Convert.ToDecimal(txtBatchGreigeKg.Text);
                        GrossWeight += (decimal)oDgv.Rows[e.RowIndex].Cells[4].Value;
                        txtBatchGreigeKg.Text = Math.Round(GrossWeight, 1).ToString();

                        decimal NettWeight = Convert.ToDecimal(txtBatchFabricKg.Text);
                        NettWeight += nett;
                        txtBatchFabricKg.Text = Math.Round(NettWeight, 1).ToString();
                    }

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Decimal Weight = 0.00M;

            TLADM_TranactionType trantype = null;
 
            if (oBtn != null && formloaded)
            {
                var Select = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (Select == null)
                {
                    MessageBox.Show("Please select a Batch number from the drop down box");
                    return;
                }


                int SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                             where ((Decimal)Rows.Cells[5].Value == 0.00M
                                             || (Decimal)Rows.Cells[6].Value == 0.00M
                                             || (Decimal)Rows.Cells[7].Value == 0.00M)
                                             select Rows).Count();
                if (SingleRow > 0)
                {
                    MessageBox.Show("There are incorrect values in the form. Please correct before saving");
                    return;
                }
               

                using (var context = new TTI2Entities())
                {
                    var DB = context.TLDYE_DyeBatch.Find(Select.DYEB_Pk);
                    if (DB != null)
                    {
                        DB.DYEB_OutProcess = true;
                        DB.DYEB_OutProcessDate = DateTime.Now;
                    }

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                    if (Dept != null)
                    {
                        trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 600).FirstOrDefault();
                        
                    }

                    int Cnt = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        TLDYE_DyeBatchDetails bd = new TLDYE_DyeBatchDetails();

                        if (row.Cells[0].Value == null)
                            continue;

                        int index = (int)row.Cells[0].Value;

                        bd = context.TLDYE_DyeBatchDetails.Find(index);
                        if (bd != null)
                        {
                            Weight += (decimal)row.Cells[5].Value;
                            
                            bd.DYEBO_Nett       = (decimal)row.Cells[5].Value;
                            bd.DYEBO_DiskWeight = (decimal)row.Cells[6].Value;
                            bd.DYEBO_Width      = (decimal)row.Cells[7].Value;
                            bd.DYEBO_Meters     = (decimal)row.Cells[8].Value;
                            bd.DYEBO_TransDate  = dtpTransDate.Value;
                            bd.DYEBO_DyeDate    = dtpDyeDate.Value;
                            if (trantype != null)
                                bd.DYEBO_CurrentStore_FK = (int)trantype.TrxT_ToWhse_FK;
                            
                            if(Convert.ToDecimal(row.Cells[5].Value.ToString()) != 0) 
                                Cnt += 1; 
                        }
                    }

                    int NoOfRecs = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList().Count;
                    if (Cnt == NoOfRecs)
                    {
                        DB.DYEB_Closed = true;
                    }

                    TLDYE_DyeTransactions tt = new TLDYE_DyeTransactions();
                    tt.TLDYET_BatchNo = Select.DYEB_BatchNo;
                    tt.TLDYET_BatchWeight = Select.DYEB_BatchKG;
                    tt.TLDYET_TransactionType = (int)trantype.TrxT_Pk;
                    tt.TLDYET_SequenceNo = Select.DYEB_SequenceNo;
                    tt.TLDYET_Batch_FK = Select.DYEB_Pk;
                    tt.TLDYET_Date = dtpTransDate.Value;
                    tt.TLDYET_TransactionWeight = Weight;
                    tt.TLDYET_CurrentStore_FK = (int)trantype.TrxT_ToWhse_FK;

                    context.TLDYE_DyeTransactions.Add(tt);

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        Setup();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {

                decimal nett = 0.0M;
                decimal weight = 0.0M;
                decimal width = 0.0M;

                if ((decimal)oDgv.Rows[e.RowIndex].Cells[5].Value != 0)
                {
                    nett = (decimal)oDgv.Rows[e.RowIndex].Cells[5].Value;
                }

                if ((decimal)oDgv.Rows[e.RowIndex].Cells[6].Value != 0)
                {
                    weight = (decimal)oDgv.Rows[e.RowIndex].Cells[6].Value;
                }

                if (Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].EditedFormattedValue.ToString()) != 0)
                {
                    width = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].EditedFormattedValue.ToString());
                }

                oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(core.FabricYield(weight, width) * nett, 2);

                decimal GrossWeight = Convert.ToDecimal(txtBatchGreigeKg.Text);
                GrossWeight += (decimal)oDgv.Rows[e.RowIndex].Cells[4].Value;
                txtBatchGreigeKg.Text = Math.Round(GrossWeight, 1).ToString();

                decimal NettWeight = Convert.ToDecimal(txtBatchFabricKg.Text);
                NettWeight += nett;
                txtBatchFabricKg.Text = Math.Round(NettWeight, 1).ToString();
               
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

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                if (dataGridView1.CurrentCell.ReadOnly)
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
