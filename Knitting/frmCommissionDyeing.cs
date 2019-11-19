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
    public partial class frmCommissionDyeing : Form
    {
        bool formloaded;
        Util core;
        bool Add;
        List<DATA> fieldSelected = null;
        bool[] MandSelected;
        string[][] MandatoryFields;

        public frmCommissionDyeing()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
            {   new string[] {"dtpBatchDate", "Please select a batch date", "0"},
                new string[] {"dtpDateOrdered", "Please select an order date", "1"},
                new string[] {"dtpDateRequired", "Please select the day required", "2"},
                new string[] {"cmboCustomer", "Please select a customer", "3"},
                new string[] {"cmboColours", "Please select a colour", "4"},
                new string[] {"cmboGreige", "Please select a fabric quality", "5"},
                new string[] {"cmboTrims", "Please select a trim", "6"}
            };

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            
            SetUp();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            dataGridView1.Columns.Add(oTxtA);

            DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.ValueType = typeof(string);
            oTxtB.HeaderText = "Customer Piece No";
            dataGridView1.Columns.Add(oTxtB);

            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.ValueType = typeof(string);
            oTxtC.HeaderText = "TTS Piece No";
            dataGridView1.Columns.Add(oTxtC);

            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.ValueType = typeof(string);
            oTxtD.HeaderText = "Quality";
            dataGridView1.Columns.Add(oTxtD);

            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.ValueType = typeof(decimal);
            oTxtE.HeaderText = "Nett Weight";
            oTxtE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns.Add(oTxtE);

            DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.ValueType = typeof(string);
            oTxtF.HeaderText = "Customer Order No";
            dataGridView1.Columns.Add(oTxtF);

            DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.ValueType = typeof(int);
            oTxtG.HeaderText = "Customer Order No";
            oTxtG.ReadOnly = true;
            oTxtG.Visible = false;
            dataGridView1.Columns.Add(oTxtG);
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                fieldSelected = new List<DATA>();

                dataGridView1.Rows.Clear();

                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                    txtBatchNo.Text = "CB" + LNU.col4.ToString().PadLeft(6,'0');

                cmboColours.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColours.DisplayMember = "Col_Display";
                cmboColours.ValueMember = "Col_Id";
                cmboColours.SelectedValue = 0;

                cmboCustomer.DataSource = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust).OrderBy(x => x.Cust_Description).ToList();
                cmboCustomer.DisplayMember = "Cust_Description";
                cmboCustomer.ValueMember = "Cust_Pk";
                cmboCustomer.SelectedValue = 0;

                cmboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboGreige.DisplayMember = "TLGreige_Description";
                cmboGreige.ValueMember = "TLGreige_Id";
                cmboGreige.SelectedValue = 0;

                cmboTrims.DataSource = context.TLADM_Trims.OrderBy(x => x.TR_Description).ToList();
                cmboTrims.DisplayMember = "TR_Description";
                cmboTrims.ValueMember = "TR_id";
                cmboTrims.SelectedValue = 0;

                cmboPrevBatches.DataSource = context.TLDYE_ComDyeBatch.OrderBy(x => x.DYEBC_BatchNo).ToList();
                cmboPrevBatches.DisplayMember = "DYEBC_BatchNo";
                cmboPrevBatches.ValueMember = "DYEBC_Pk";

            }
            txtKgsSelected.Text = "0.00";
            formloaded = true;
            Add = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var ErrM = core.returnMessage(MandSelected, false, MandatoryFields);
                if(!String.IsNullOrEmpty(ErrM))
                {
                     MessageBox.Show(ErrM);
                    return;
                }
                using (var context = new TTI2Entities())
                {
                    if (Add)
                    {
                        var LNU = context.TLADM_LastNumberUsed.Find(3);
                        if (LNU != null)
                        {
                            LNU.col4 += 1;
                        }
                    }

                    TLDYE_ComDyeBatch cb = new TLDYE_ComDyeBatch();
                    if (!Add)
                    {
                        var Prev = (TLDYE_ComDyeBatch)cmboPrevBatches.SelectedItem;
                        if (Prev != null)
                        {
                            cb = context.TLDYE_ComDyeBatch.Find(Prev.DYEBC_Pk);
                        }
                    }

                    cb.DYEBC_BatchDate = dtpBatchDate.Value;
                    cb.DYEBC_BatchNo = txtBatchNo.Text;
                    cb.DYEBC_DateOrder = dtpDateOrdered.Value;
                    cb.DYEBC_DateRequired = dtpDateOrdered.Value;
                    cb.DYEBC_Greige_FK = (int)cmboGreige.SelectedValue;
                    cb.DYEBC_Trim_Fk = (int)cmboTrims.SelectedValue;
                    cb.DYEBC_Customer_FK = (int)cmboCustomer.SelectedValue;
                    cb.DYEBC_Colour_FK = (int)cmboColours.SelectedValue;

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                    if (Dept != null)
                    {
                        var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 300).FirstOrDefault();
                        if (TranType != null)
                        {
                            cb.DYEBC_TransactionType = TranType.TrxT_Pk;
                        }
                    }

                    if (Add)
                    {
                        context.TLDYE_ComDyeBatch.Add(cb);
                        try
                        {
                            context.SaveChanges();
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
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(ex.Message);
                                ex = ex.InnerException;
                            }
                            while (ex != null);

                            MessageBox.Show(exceptionMessages.ToString());

                        }
                    }
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString() == bool.FalseString)
                            continue;
                        
                        var index = (int)row.Cells[0].Value;

                        TLKNI_GreigeCommissionTransctions rec = context.TLKNI_GreigeCommissionTransctions.Find(index);
                        if (rec != null)
                        {
                            rec.GreigeCom_Batched = true;
                            rec.GreigeCom_DyeBatch_FK = cb.DYEBC_Pk;
                        }

                        TLDYE_ComDyeBatchDetails cdb = new TLDYE_ComDyeBatchDetails();
                        cdb.TLCDD_PieceNo_FK = rec.GreigeCom_PK;
                        cdb.TLCDD_ComDyeBatch_FK = cb.DYEBC_Pk;

                        context.TLDYE_ComDyeBatchDetails.Add(cdb);
                     
                
                    }

                    try
                    {
                        context.SaveChanges();
                       
                        MessageBox.Show("Data saved to database successfully");
                        frmDyeViewReport vrep = new frmDyeViewReport(9, cb.DYEBC_Pk);
                        vrep.ShowDialog(this);
                        SetUp();
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
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);

                        MessageBox.Show(exceptionMessages.ToString());
                       
                    }
                }
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var Customer = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (Customer == null)
                {
                    MessageBox.Show("Please select a customer from the relevant drop down box");
                    return;
                }

                var Greige = (TLADM_Griege)cmboGreige.SelectedItem;
                if (Greige != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_CommisionCust && x.GreigeP_CommissionCust_FK == Customer.Cust_Pk &&
                              !x.GreigeP_Dye).ToList();

                        if (Existing == null)
                        {
                            MessageBox.Show("No records found for selection made");
                            return;
                        }
                        else
                        {
                            var result = (from u in MandatoryFields
                                              where u[0] == oCmbo.Name
                                              select u).FirstOrDefault();

                             if (result != null)
                             {
                                    int nbr = Convert.ToInt32(result[2].ToString());
                                    MandSelected[nbr] = true;
                             }
                        }

                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.GreigeP_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = row.GreigeP_PieceNo;
                            var HR = context.TLKNI_GreigeCommissionTransctions.Find(row.GreigeP_KnitO_Fk);
                            if (HR != null)
                            {
                                dataGridView1.Rows[index].Cells[3].Value = HR.GreigeCom_TTSNo;
                                dataGridView1.Rows[index].Cells[6].Value = HR.GreigeCom_CustOrderNo;
                            }

                            var Qual = context.TLADM_Griege.Find(row.GreigeP_Greige_Fk);
                            if (Qual != null)
                            {
                                dataGridView1.Rows[index].Cells[4].Value = Qual.TLGreige_Description;
                            }
                                                     
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.GreigeP_weightAvail, 2);
                            dataGridView1.Rows[index].Cells[7].Value = null;
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
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        fieldSelected.Add(new DATA(CurrentRow.Index, (decimal)CurrentRow.Cells[5].Value));

                        decimal Amount = Convert.ToDecimal(txtKgsSelected.Text);

                        txtKgsSelected.Text = (Amount + (decimal)CurrentRow.Cells[5].Value).ToString();
                    }
                }
                else
                {
                    var CurrentRow = oDgv.CurrentRow;
                    if (CurrentRow != null)
                    {
                        decimal curBal = Convert.ToDecimal(txtKgsSelected.Text);
                        txtKgsSelected.Text = (curBal - (decimal)CurrentRow.Cells[5].Value).ToString();

                        var Record = fieldSelected.Find(x => x._RowIndex == CurrentRow.Index);
                         var RecordIndex = fieldSelected.IndexOf(Record);
                         if (RecordIndex != -1)
                         {
                             fieldSelected.RemoveAt(RecordIndex);
                             using (var context = new TTI2Entities())
                             {
                                 var rec = context.TLKNI_GreigeCommissionTransctions.Find((int)CurrentRow.Cells[0].Value);
                                 if (rec != null)
                                 {
                                     rec.GreigeCom_DyeBatch_FK = null;
                                     rec.GreigeCom_Batched = false;

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

        private void cmboPrevBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded)
            {
                Add = false;

                var selected = (TLDYE_ComDyeBatch)cmboPrevBatches.SelectedItem;
                if (selected != null)
                {
                    cmboColours.SelectedValue = selected.DYEBC_Colour_FK;
                    cmboCustomer.SelectedValue = selected.DYEBC_Customer_FK;
                    formloaded = false;
                    cmboGreige.SelectedValue = selected.DYEBC_Greige_FK;
                    formloaded = true;
                    cmboTrims.SelectedValue = selected.DYEBC_Trim_Fk;



                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_Customer_FK == selected.DYEBC_Customer_FK &&
                                           x.GreigeCom_ProductType_FK == selected.DYEBC_Greige_FK).ToList();

                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.GreigeCom_PK;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = row.GreigeCom_PieceNo;
                            dataGridView1.Rows[index].Cells[3].Value = row.GreigeCom_TTSNo;

                            var Qual = context.TLADM_Griege.Find(row.GreigeCom_ProductType_FK);
                            if (Qual != null)
                            {
                                dataGridView1.Rows[index].Cells[4].Value = Qual.TLGreige_Description;
                            }
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.GreigeCom_NettWeight, 2);
                            dataGridView1.Rows[index].Cells[6].Value = row.GreigeCom_CustOrderNo;
                            dataGridView1.Rows[index].Cells[7].Value = null;
                            if (row.GreigeCom_DyeBatch_FK != null)
                            {
                                if (row.GreigeCom_DyeBatch_FK == selected.DYEBC_Pk)
                                {
                                    fieldSelected.Add(new DATA(index, (decimal)dataGridView1.Rows[index].Cells[5].Value));
                                    dataGridView1.Rows[index].Cells[1].Value = true;
                                    var val = Convert.ToDecimal(txtKgsSelected.Text);
                                    txtKgsSelected.Text = Math.Round((val + row.GreigeCom_NettWeight), 2).ToString();
                                }
                            }
                        }
                    }
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                }
            }
        }

        private void dtpBatchDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }


        }

        private void cmboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmboTrims_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

   }
}