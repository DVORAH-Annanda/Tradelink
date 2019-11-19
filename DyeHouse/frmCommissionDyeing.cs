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
        Util core;

        object[] ColumnHeadings;


        string[][] MandatoryFields;
        bool[] MandSelected;

          

       
        
      

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key (DYE Batch)          0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key (Greige Production)  1 
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check box                        2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Piece                            3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Gross                            4
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Quality                          5
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Tex                              6
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Yarn Supplier                    7 
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // Pallet No                        8
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // K/Order                          9
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // Colour;                         10
        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn();   // Grade                           11

        DataGridViewTextBoxColumn oTxtR = new DataGridViewTextBoxColumn();   // Piece Remarks     3             12
        DataGridViewTextBoxColumn oTxt1 = new DataGridViewTextBoxColumn();   // No 1              4             13
        DataGridViewTextBoxColumn oTxt2 = new DataGridViewTextBoxColumn();   // No 2              5             14
        DataGridViewTextBoxColumn oTxt3 = new DataGridViewTextBoxColumn();   // No 3              6             15
        DataGridViewTextBoxColumn oTxt4 = new DataGridViewTextBoxColumn();   // No 4              7             16
        DataGridViewTextBoxColumn oTxt5 = new DataGridViewTextBoxColumn();   // No 5              8             17
        DataGridViewTextBoxColumn oTxt6 = new DataGridViewTextBoxColumn();   // No 6              9             18
        DataGridViewTextBoxColumn oTxt7 = new DataGridViewTextBoxColumn();   // No 7             10             19
        DataGridViewTextBoxColumn oTxt8 = new DataGridViewTextBoxColumn();   // No 8             11             20  
        DataGridViewTextBoxColumn oTxt9 = new DataGridViewTextBoxColumn();   // No 9             12             21    
        //--------------------------------------------------------------------
        //
        //---------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn(); // index           0                      0
        DataGridViewTextBoxColumn oTxtAA = new DataGridViewTextBoxColumn();    // description     1                      1
        DataGridViewTextBoxColumn oTxtAB = new DataGridViewTextBoxColumn();    // quality         2                     2
        DataGridViewTextBoxColumn oTxtAC = new DataGridViewTextBoxColumn();    // ordered         3                     3
        DataGridViewTextBoxColumn oTxtAD = new DataGridViewTextBoxColumn();    // batched         4                      4 
        DataGridViewTextBoxColumn oTxtAE = new DataGridViewTextBoxColumn();    // selected                              5
        DataGridViewTextBoxColumn oTxtAF = new DataGridViewTextBoxColumn();    // Outstanding                           6
        DataGridViewTextBoxColumn oTxtAG = new DataGridViewTextBoxColumn();    // Greige Quality FK                     7
        DataGridViewCheckBoxColumn oTxtAH = new DataGridViewCheckBoxColumn();  // Used as a boolean 1 = Body and 0 Trim 8
        DataGridViewCheckBoxColumn oTxtAJ = new DataGridViewCheckBoxColumn();  // Trim Position FK
        
        List<DATA> fieldSelected = new List<DATA>();
        List<DATA1> GreigeSelected = new List<DATA1>();

        bool formLoaded;
        bool PrevDyeBatch;
        

        public frmCommissionDyeing()
        {
            
            InitializeComponent();

            core = new Util();

            MandatoryFields = new string[][]
            {   new string[] {"cmboCustomer", "Please select a customer number", "0"},
                new string[] {"cmboColours", "Please a colour", "1"},
                new string[] {"dtpBatchDate", "Please select a batch date", "2"},
                new string[] {"dateTimePicker2", "Please select the date ordered", "3"},
                new string[] {"dtpRequiredDate", "Please date required", "4"},
                new string[] {"cmboGreige", "Please select a fabric", "5"}
            };
          

            ColumnHeadings = new Object[22];
            oTxtA.Visible = false;

            ColumnHeadings[0] = oTxtA;  //  Primary Key (DYE Batch)
            ColumnHeadings[1] = oTxtB;  //  Primary Key (Greige Production)
            ColumnHeadings[2] = oChkA;  //  Select Option
            ColumnHeadings[3] = oTxtC;  //  Piece No
            ColumnHeadings[4] = oTxtD;  //  Gross
            ColumnHeadings[5] = oTxtE;  //  Quality
            ColumnHeadings[6] = oTxtF;  //  Text
            ColumnHeadings[7] = oTxtG;  //  Yarn Supplier
            ColumnHeadings[8] = oTxtH;  //  Pallet No
            ColumnHeadings[9] = oTxtJ;  //  K/Order
            ColumnHeadings[10] = oTxtK; //  Colour
            ColumnHeadings[11] = oTxtL; //  Grade
            ColumnHeadings[12] = oTxtR; //  Remarks
            ColumnHeadings[13] = oTxt1; //  measur1 
            ColumnHeadings[14] = oTxt2; //  measur2
            ColumnHeadings[15] = oTxt3; //  measur3
            ColumnHeadings[16] = oTxt4; //  measur4
            ColumnHeadings[17] = oTxt5; //  measur5
            ColumnHeadings[18] = oTxt6; //  measur6
            ColumnHeadings[19] = oTxt7; //  measur7
            ColumnHeadings[20] = oTxt8; //  measur8
            ColumnHeadings[21] = oTxt9; //  used to store the Greige_FK value
            SetUp(true);
        }

        void SetUp(bool SetUp)
        {
            formLoaded = false;
            PrevDyeBatch = false;
          

            using (var context = new TTI2Entities())
            {

                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                rbStandardMode.Checked = true;

                cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_CommissinCust && !x.DYEB_Transfered).OrderBy(x => x.DYEB_BatchNo).ToList();
                cmboBatches.ValueMember = "DYEB_Pk";
                cmboBatches.DisplayMember = "DYEB_BatchNo";
                cmboBatches.SelectedValue = -1;

                cmboColours.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColours.DisplayMember = "Col_Display";
                cmboColours.ValueMember = "Col_Id";
                cmboColours.SelectedValue = 0;

                cmboCustomer.DataSource = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust).ToList();
                cmboCustomer.ValueMember = "Cust_Pk";
                cmboCustomer.DisplayMember = "Cust_Description";
                cmboCustomer.SelectedValue = -1;

                cmboGreige.DataSource = context.TLADM_Griege.OrderBy(x=>x.TLGreige_Description).ToList();
                cmboGreige.DisplayMember =  "TLGreige_Description";
                cmboGreige.ValueMember = "TLGreige_Id";
                cmboGreige.SelectedValue = -1;

                cmboTrims.DataSource = context.TLADM_Trims.OrderBy(x=>x.TR_Description).ToList();
                cmboTrims.DisplayMember = "TR_Description";
                cmboTrims.ValueMember = "TR_Id";
                cmboTrims.SelectedValue = -1;

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();

                txtBatchKg.Text = "0.00";
                          

                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtBatchNo.Text = "CB" + LNU.col4.ToString().PadLeft(6, '0');
                }

                if (SetUp)
                {
                    var h2 = (DataGridViewTextBoxColumn)ColumnHeadings[0];
                    h2.HeaderText = "Primary Key (DYE Batch)";
                    h2.Visible = false;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[1];
                    h2.HeaderText = "Primary Key (Greige Production)";
                    h2.Name = "xxx";
                    h2.Visible = false;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                    var chkh2 = (DataGridViewCheckBoxColumn)ColumnHeadings[2];
                    chkh2.HeaderText = "Select";
                    dataGridView1.Columns.Add(chkh2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
                    h2.HeaderText = "Piece";
                    h2.ValueType = typeof(int);
                    h2.ReadOnly = true;
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[4];
                    h2.HeaderText = "Gross";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(decimal);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[5];
                    h2.HeaderText = "Quality";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[6];
                    h2.HeaderText = "Tex";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[7];
                    h2.HeaderText = "Yarn Supplier";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[8];
                    h2.HeaderText = "Pallet No";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[9];
                    h2.HeaderText = "K/Order";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[10];
                    h2.HeaderText = "Colour";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[11];
                    h2.HeaderText = "Grade";
                    h2.ReadOnly = true;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[12];
                    h2.HeaderText = "Remarks";
                    h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    h2.ValueType = typeof(string);
                    dataGridView1.Columns.Add(h2);

                  

                    var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                    if (Depts != null)
                    {
                        var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                        foreach (var Reason in Reasons)
                        {
                            foreach (var elementH in ColumnHeadings)
                            {
                                Type t = elementH.GetType();
                                if (t.Equals(typeof(DataGridViewCheckBoxColumn)))
                                    continue;

                                var ch = (DataGridViewTextBoxColumn)elementH;
                                if (String.IsNullOrEmpty(ch.HeaderText))
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(Reason.QD_ShortCode);
                                    // sb.Append(Environment.NewLine);
                                    // sb.Append(Reason.QD_Description);
                                    ch.HeaderText = sb.ToString();
                                    ch.ValueType = typeof(int);
                                    ch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dataGridView1.Columns.Add(ch);
                                    break;
                                }
                            }
                        }
                    }

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[21];
                    h2.HeaderText = "Greige Quality PK";
                    h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    h2.Visible = false;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                    oTxtIndex.HeaderText = "index";            // 0
                    oTxtIndex.ValueType = typeof(int);
                    oTxtIndex.ReadOnly = true;
                    oTxtIndex.Visible = false;

                    oTxtAA.HeaderText = "Description";         //  1
                    oTxtAA.ValueType = typeof(string);
                    oTxtAA.ReadOnly = true;

                    oTxtAB.HeaderText = "Quality";             //  2
                    oTxtAB.ValueType = typeof(string);
                    oTxtAB.ReadOnly = true;

                    oTxtAC.HeaderText = "Ordered";             //  3
                    oTxtAC.ValueType = typeof(decimal);
                    oTxtAC.ReadOnly = true;

                    oTxtAD.HeaderText = "Batched";             //  4  
                    oTxtAD.ValueType = typeof(decimal);
                    oTxtAD.ReadOnly = true;

                    oTxtAE.HeaderText = "Selected";            //  5
                    oTxtAE.ValueType = typeof(decimal);
                    oTxtAE.ReadOnly = true;

                    oTxtAF.HeaderText = "Outstanding";         //  6 
                    oTxtAF.ValueType = typeof(decimal);
                    oTxtAF.ReadOnly = true;


                    oTxtAG.HeaderText = "Trims Pk Holder";     // 7
                    oTxtAG.ValueType = typeof(int);
                    oTxtAG.Visible = false;
                    oTxtAG.ReadOnly = true;

                    oTxtAH.HeaderText = "Body or Trim";        // 8
                    oTxtAH.ValueType = typeof(bool);
                    oTxtAH.Visible = false;
                    oTxtAH.ReadOnly = true;

                    oTxtAJ.HeaderText = "Trim FK";
                    oTxtAJ.ValueType = typeof(int);
                    oTxtAJ.Visible = false;
                    oTxtAJ.ReadOnly = true;

                    dataGridView2.Columns.Add(oTxtIndex);  // record index 0;
                    dataGridView2.Columns.Add(oTxtAA);     // description 
                    dataGridView2.Columns.Add(oTxtAB);     // quality 
                    dataGridView2.Columns.Add(oTxtAC);     // kgs Ordered 
                    dataGridView2.Columns.Add(oTxtAD);     // batched 
                    dataGridView2.Columns.Add(oTxtAE);     // selected 
                    dataGridView2.Columns.Add(oTxtAF);     // Outstanding 
                    dataGridView2.Columns.Add(oTxtAG);     // PK for Greige Qual
                    dataGridView2.Columns.Add(oTxtAH);     // Body or Trim Body = true Trim = false;
                    dataGridView2.Columns.Add(oTxtAJ);     // Trim FK;

                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView2.AllowUserToAddRows = false;

                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView2.AutoGenerateColumns = false;

                    this.dataGridView1.Columns[1].Frozen = true;
                    this.dataGridView1.Columns[2].Frozen = true;
                    this.dataGridView1.Columns[3].Frozen = true;

                    txtBatchKg.Text = "0.00";

                    formLoaded = true;
                }
            }
        }

        private void cmboDyeOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             int rowindex = 0;

             
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {

                 if (!MandSelected[-1 + MandSelected.Length] && !PrevDyeBatch)
                 {
                     MessageBox.Show("Please select a body fabric");
                     return;
                 }
          
                 if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                 {
                     try
                     {
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }
                    
                     var CurrentRow = oDgv.CurrentRow;
                     if (CurrentRow != null)
                     {
                         if (e.ColumnIndex != 9999)
                         {
                             rowindex = dataGridView2.CurrentRow.Index;
                         }
                         else
                         {
                             rowindex = e.RowIndex;
                         }
                         foreach (DataGridViewRow row in dataGridView2.Rows)
                         {
                             if (rowindex == row.Index)
                             {
                           
                                 fieldSelected.Add(new DATA(CurrentRow.Index, (decimal)CurrentRow.Cells[4].Value, rowindex, (int)row.Cells[9].Value, (bool)row.Cells[8].Value,(int)CurrentRow.Cells[1].Value, (int)row.Cells[7].Value));

                                 var val = Math.Round((decimal)row.Cells[5].Value, 2);
                                 row.Cells[5].Value = val + Math.Round((decimal)CurrentRow.Cells[4].Value,2);
                                 row.Cells[3].Value = (decimal)row.Cells[5].Value;
                                 if(e.ColumnIndex == 9999)
                                 {
                                     // var prevVal = (decimal)CurrentRow.Cells[4].Value;
                                     // row.Cells[4].Value = ((decimal)row.Cells[4].Value) - prevVal;

                                 }
                                 var ord = (decimal)row.Cells[3].Value;
                                 row.Cells[6].Value = ord - Math.Round((decimal)row.Cells[4].Value + (decimal)row.Cells[5].Value,2);

                                 decimal curBal = Convert.ToDecimal(txtBatchKg.Text);
                                 curBal += (decimal)CurrentRow.Cells[4].Value;
                                 txtBatchKg.Text = Math.Round(curBal,2).ToString();
                             }
                        }
                    }
                 }
                 else
                 {
                     var CurrentRow = oDgv.CurrentRow;
                     if (CurrentRow != null)
                     {
                         decimal curBal = Convert.ToDecimal(txtBatchKg.Text);
                         txtBatchKg.Text = (curBal - (decimal)CurrentRow.Cells[4].Value).ToString();

                         var Record = fieldSelected.Find(x => x.GV1reckey == CurrentRow.Index);
                         var RecordIndex = fieldSelected.IndexOf(Record);
                         if (RecordIndex != -1)
                         {
                             foreach (DataGridViewRow row in dataGridView2.Rows)
                             {
                                 if (Record.GV2reckey == row.Index)
                                 {
                                     var val = (decimal)row.Cells[5].Value;
                                     row.Cells[5].Value = val - (decimal)CurrentRow.Cells[4].Value;
                                     row.Cells[3].Value = (decimal)row.Cells[5].Value;
                                     row.Cells[6].Value = (decimal)row.Cells[3].Value - ((decimal)row.Cells[4].Value + (decimal)row.Cells[5].Value);
                                   
                                     fieldSelected.RemoveAt(RecordIndex);

                                     
                                 }
                             }
                         }
                     }
                 }
             }
        }

        private struct DATA
        {
            public int GV1reckey;            // datagridview1
            public decimal Amount;           // datagridview1 Weight
            public int GV2reckey;            // datagridview2 Row index     
            public int GV2TrimRecKey;        // dataGridView Trim Record Key
            public bool GV2BoddyOrTrim;      // bool True = body    false = trim
            public int GV1GreigeProduction;  // dataGridView1 The record key of the Greige Produced
            public int Quality;               // what is the Greige FK
 
            public DATA(int Key, decimal Amt, int rowIndex, int TrimKey, bool body, int GP, int Qual)
            {
                this.GV1reckey = Key;
                this.Amount    = Amt;
                this.GV2reckey = rowIndex;
                this.GV2TrimRecKey = TrimKey;
                this.GV2BoddyOrTrim = body;
                this.GV1GreigeProduction = GP;
                Quality = Qual;
            }
        }

        private struct DATA1
        {
            public int GV1reckey;   // Greige _FK
            public DATA1(int Key)
            {
                this.GV1reckey = Key;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int DyeBatchKey = 0;
            Button oBtn = sender as Button;
            if (oBtn != null && formLoaded)
            {
                var ErrM = core.returnMessage(MandSelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrM))
                {
                    MessageBox.Show(ErrM);
                    return;
                }

                // First thing we have to creare a header record
                //------------------------------------------------
                using (var context = new TTI2Entities())
                {
                    if (!PrevDyeBatch || rbReprocessMode.Checked)
                    {
                        if (rbStandardMode.Checked)
                        {
                            var LNU = context.TLADM_LastNumberUsed.Find(3);
                            if (LNU != null)
                            {
                                LNU.col4 += 1;
                            }
                        }
                        
                                  
                        TLDYE_DyeBatch db = new TLDYE_DyeBatch();

                        db.DYEB_BatchNo = txtBatchNo.Text;
                        db.DYEB_BatchKG = Convert.ToDecimal(txtBatchKg.Text);
                        db.DYEB_BatchDate = dtpBatchDate.Value;
                        db.DYEB_RequiredDate = dtpRequiredDate.Value;
                        db.DYEB_Customer_FK = (int)cmboCustomer.SelectedValue;
                        db.DYEB_CommissinCust = true;
                        db.DYEB_Colour_FK = (int)cmboColours.SelectedValue;
                        db.DYEB_Greige_FK = (int)cmboGreige.SelectedValue;

                        if (rbReprocessMode.Checked)
                        {
                            var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                            if (DB != null)
                            {
                                db.DYEB_SequenceNo = DB.DYEB_SequenceNo + 1;
                                db.DYEB_BatchNo = DB.DYEB_BatchNo + "R" + db.DYEB_SequenceNo.ToString().PadLeft(3, '0');
                            }
                        }
                        if (chkLabReport.Checked)
                            db.DYEB_Lab = true;
                        else
                            db.DYEB_Lab = false;

                        if (chkWrap.Checked)
                            db.DYEB_Wrap = true;
                        else
                            db.DYEB_Wrap = false;

                        // db.DYEB_DyeOrder_FK = (int)cmboDyeOrders.SelectedValue;
                        db.DYEB_Notes       = rcbNotes.Text;

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var Trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 200).FirstOrDefault();
                            if (Trantype != null)
                                db.DYEB_TransactionType_FK = Trantype.TrxT_Pk;
                        }

                        
                        
                        try
                        {
                            context.TLDYE_DyeBatch.Add(db);
                            context.SaveChanges();

                            DyeBatchKey = db.DYEB_Pk;

                            if (!PrevDyeBatch || rbReprocessMode.Checked)
                            {
                                TLDYE_DyeTransactions dt = new TLDYE_DyeTransactions();
                                dt.TLDYET_BatchNo = txtBatchNo.Text;
                                dt.TLDYET_Date = DateTime.Now;
                                dt.TLDYET_SequenceNo = db.DYEB_SequenceNo;
                                dt.TLDYET_TransactionType = db.DYEB_TransactionType_FK;
                                dt.TLDYET_BatchWeight = db.DYEB_BatchKG;
                                dt.TLDYET_Batch_FK = DyeBatchKey;
                                dt.TLDYET_TransactionNumber = txtBatchNo.Text;
                                dt.TLDYET_Customer_FK = (int)cmboCustomer.SelectedValue;
                                dt.TLDYET_CurrentStore_FK = (int)context.TLADM_TranactionType.Find(db.DYEB_TransactionType_FK).TrxT_ToWhse_FK;
                                
                                context.TLDYE_DyeTransactions.Add(dt);

                                context.SaveChanges();
                                

                            }
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
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    else
                    {
                       
                        DyeBatchKey = (int)cmboBatches.SelectedValue;
                        TLDYE_DyeBatch db = new TLDYE_DyeBatch();
                        db = context.TLDYE_DyeBatch.Find(DyeBatchKey);
                        if(db != null)
                           db.DYEB_BatchKG = Convert.ToDecimal(txtBatchKg.Text);
                    }
                    // now for each record in the fieldSelected struct

                    foreach (var rec in fieldSelected)
                    {
                        var data = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == rec.GV1GreigeProduction).FirstOrDefault();
                        if (data == null)
                        {
                            TLDYE_DyeBatchDetails tbd = new TLDYE_DyeBatchDetails();

                            tbd.DYEBD_DyeBatch_FK = DyeBatchKey;
                            tbd.DYEBD_BodyTrim = rec.GV2BoddyOrTrim;
                            tbd.DYEBD_GreigeProduction_FK = rec.GV1GreigeProduction;
                            tbd.DYEBD_GreigeProduction_Weight = rec.Amount;
                            tbd.DYEBD_QualityKey = rec.Quality;
                            tbd.DYEBO_TrimKey = rec.GV2TrimRecKey;
                            tbd.DYEBO_GVRowNumber = rec.GV2reckey;

                            context.TLDYE_DyeBatchDetails.Add(tbd);
                        }
                        else
                        {
                            data.DYEBD_DyeBatch_FK = DyeBatchKey;
                        }
                        
                        var GP = context.TLKNI_GreigeProduction.Find(rec.GV1GreigeProduction);
                        if (GP != null)
                        {
                            GP.GreigeP_Dye = true;
                            GP.GreigeP_DyeBatch_FK = DyeBatchKey;

                            var ComTr = context.TLKNI_GreigeCommissionTransctions.Find(GP.GreigeP_KnitO_Fk);
                            if (ComTr != null)
                            {
                                ComTr.GreigeCom_DyeBatch_FK = DyeBatchKey;
                                ComTr.GreigeCom_Batched = true;
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Records Saved to Database Successfully");
               
                        //------------------
                        // Do the printing
                        //-------------------------------
                        if (PrevDyeBatch || rbReprocessMode.Checked)
                        {
                            DialogResult res = MessageBox.Show("Would you like to reprint the DyeBatch", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.No)
                            {
                                return;
                            }
                           
                        }
                        
                        SetUp(false);

                        frmDyeViewReport vRep = new frmDyeViewReport(4, DyeBatchKey);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                        vRep = new frmDyeViewReport(5, DyeBatchKey);
                        h = Screen.PrimaryScreen.WorkingArea.Height;
                        w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        

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
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        private void cmboBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            IList<TLKNI_GreigeProduction> ExistingPieces = new List<TLKNI_GreigeProduction>();
            if(oCmbo != null && formLoaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                if (selected != null)
                {
                    txtBatchNo.Text = selected.DYEB_BatchNo;

                    PrevDyeBatch = true;

                    cmboCustomer.SelectedValue = selected.DYEB_Customer_FK;
                    cmboColours.SelectedValue = selected.DYEB_Colour_FK;
                    dtpBatchDate.Value = (DateTime)selected.DYEB_BatchDate;
                    dtpRequiredDate.Value = (DateTime)selected.DYEB_RequiredDate;

                    if (selected.DYEB_Lab)
                        chkLabReport.Checked = true;
                    if (selected.DYEB_Wrap)
                        chkWrap.Checked = true;

                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);

                    using (var context = new TTI2Entities())
                    {
                        //-------------------------------------------------
                        // This does the header
                        //------------------------------------------
                        var existingGrp = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk).ToList().GroupBy(x => new { x.DYEBD_QualityKey, x.DYEBD_BodyTrim });
                        foreach (var row in existingGrp)
                        {
                            if (row.FirstOrDefault().DYEBD_BodyTrim)
                            {
                                cmboGreige.SelectedValue = row.FirstOrDefault().DYEBD_QualityKey;
                                var index = dataGridView2.Rows.Add();
                                dataGridView2.Rows[index].Cells[1].Value = "Fabric :";
                                dataGridView2.Rows[index].Cells[2].Value = ((TLADM_Griege)cmboGreige.SelectedItem).TLGreige_Description;
                                dataGridView2.Rows[index].Cells[3].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[4].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[5].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[6].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[7].Value = row.FirstOrDefault().DYEBD_QualityKey;//  Body
                                dataGridView2.Rows[index].Cells[8].Value = true;                 //  Body
                                dataGridView2.Rows[index].Cells[9].Value = row.FirstOrDefault().DYEBO_GVRowNumber;
                            }
                            else
                            {
                                cmboTrims.SelectedValue = row.FirstOrDefault().DYEBD_QualityKey;
                                var index = dataGridView2.Rows.Add();
                                dataGridView2.Rows[index].Cells[1].Value = "Trims :";
                                dataGridView2.Rows[index].Cells[2].Value = ((TLADM_Griege)cmboGreige.SelectedItem).TLGreige_Description;
                                dataGridView2.Rows[index].Cells[3].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[4].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[5].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[6].Value = 0.00M;
                                dataGridView2.Rows[index].Cells[7].Value = row.FirstOrDefault().DYEBD_QualityKey;//  Body
                                dataGridView2.Rows[index].Cells[8].Value = false;                 //  Body
                                dataGridView2.Rows[index].Cells[9].Value = row.FirstOrDefault().DYEBO_GVRowNumber;
                            }
                        }
                        //------------------------------------------
                        // The body 
                        //---------------------------------------------
                        if (rbStandardMode.Checked)
                        {
                            ExistingPieces = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_DyeBatch_FK == selected.DYEB_Pk || (x.GreigeP_CommissionCust_FK == selected.DYEB_Customer_FK && !x.GreigeP_Dye)).ToList();

                            foreach (var prow in ExistingPieces)
                            {
                                var pindex = dataGridView1.Rows.Add();
                                dataGridView1.Rows[pindex].Cells[0].Value = 0;
                                dataGridView1.Rows[pindex].Cells[1].Value = prow.GreigeP_Pk;

                                dataGridView1.Rows[pindex].Cells[3].Value = prow.GreigeP_PieceNo;
                                dataGridView1.Rows[pindex].Cells[4].Value = Math.Round(prow.GreigeP_weight, 2);
                                var Greige = context.TLADM_Griege.Find(prow.GreigeP_Greige_Fk);
                                if (Greige != null)
                                    dataGridView1.Rows[pindex].Cells[5].Value = Greige.TLGreige_Description;

                                var KnitOrder = context.TLKNI_Order.Find(prow.GreigeP_KnitO_Fk);
                                if (KnitOrder != null)
                                {

                                    dataGridView1.Rows[pindex].Cells[9].Value = "KO" + KnitOrder.KnitO_OrderNumber.ToString().PadLeft(6, '0');
                                    var YarnOrder = context.TLSPN_YarnOrder.Find(KnitOrder.KnitO_YarnO_FK);
                                    if (YarnOrder != null)
                                    {
                                        var YarnType = context.TLADM_Yarn.Find(YarnOrder.Yarno_YarnType_FK);
                                        if (YarnType != null)
                                        {
                                            dataGridView1.Rows[pindex].Cells[6].Value = Math.Round(YarnType.YA_TexCount, 1);
                                            var Supplier = context.TLADM_Suppliers.Find(YarnType.YA_Supplier_FK);
                                            if (Supplier != null)
                                            {
                                                dataGridView1.Rows[pindex].Cells[7].Value = Supplier.Sup_Description;
                                            }
                                        }
                                    }

                                }
                                //------------------------------------------------------------------------------
                                // Column 8 still have to do pallet number's ??????????????????
                                //--------------------------------------------------------------------

                                dataGridView1.Rows[pindex].Cells[10].Value = "Greige";
                                dataGridView1.Rows[pindex].Cells[11].Value = prow.GreigeP_Grade;
                                dataGridView1.Rows[pindex].Cells[12].Value = prow.GreigeP_Remarks;
                                dataGridView1.Rows[pindex].Cells[13].Value = prow.GreigeP_Meas1;
                                dataGridView1.Rows[pindex].Cells[14].Value = prow.GreigeP_Meas2;
                                dataGridView1.Rows[pindex].Cells[15].Value = prow.GreigeP_Meas3;
                                dataGridView1.Rows[pindex].Cells[16].Value = prow.GreigeP_Meas4;
                                dataGridView1.Rows[pindex].Cells[17].Value = prow.GreigeP_Meas5;
                                dataGridView1.Rows[pindex].Cells[18].Value = prow.GreigeP_Meas6;
                                dataGridView1.Rows[pindex].Cells[19].Value = prow.GreigeP_Meas7;
                                dataGridView1.Rows[pindex].Cells[20].Value = prow.GreigeP_Meas8;
                                dataGridView1.Rows[pindex].Cells[21].Value = prow.GreigeP_Greige_Fk;
                            
                            }
                        }
                        else
                        {
                            ExistingPieces = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_DyeBatch_FK == selected.DYEB_Pk).ToList();
                            foreach (var prow in ExistingPieces)
                            {
                                var pindex = dataGridView1.Rows.Add();
                                dataGridView1.Rows[pindex].Cells[0].Value = 0;
                                dataGridView1.Rows[pindex].Cells[1].Value = prow.GreigeP_Pk;

                                dataGridView1.Rows[pindex].Cells[3].Value = prow.GreigeP_PieceNo;
                                dataGridView1.Rows[pindex].Cells[4].Value = Math.Round(prow.GreigeP_weight, 2);
                                var Greige = context.TLADM_Griege.Find(prow.GreigeP_Greige_Fk);
                                if (Greige != null)
                                    dataGridView1.Rows[pindex].Cells[5].Value = Greige.TLGreige_Description;

                                var KnitOrder = context.TLKNI_Order.Find(prow.GreigeP_KnitO_Fk);
                                if (KnitOrder != null)
                                {

                                    dataGridView1.Rows[pindex].Cells[9].Value = "KO" + KnitOrder.KnitO_OrderNumber.ToString().PadLeft(6, '0');
                                    var YarnOrder = context.TLSPN_YarnOrder.Find(KnitOrder.KnitO_YarnO_FK);
                                    if (YarnOrder != null)
                                    {
                                        var YarnType = context.TLADM_Yarn.Find(YarnOrder.Yarno_YarnType_FK);
                                        if (YarnType != null)
                                        {
                                            dataGridView1.Rows[pindex].Cells[6].Value = Math.Round(YarnType.YA_TexCount, 1);
                                            var Supplier = context.TLADM_Suppliers.Find(YarnType.YA_Supplier_FK);
                                            if (Supplier != null)
                                            {
                                                dataGridView1.Rows[pindex].Cells[7].Value = Supplier.Sup_Description;
                                            }
                                        }
                                    }

                                }
                                //------------------------------------------------------------------------------
                                // Column 8 still have to do pallet number's ??????????????????
                                //--------------------------------------------------------------------

                                dataGridView1.Rows[pindex].Cells[10].Value = "Greige";
                                dataGridView1.Rows[pindex].Cells[11].Value = prow.GreigeP_Grade;
                                dataGridView1.Rows[pindex].Cells[12].Value = prow.GreigeP_Remarks;
                                dataGridView1.Rows[pindex].Cells[13].Value = prow.GreigeP_Meas1;
                                dataGridView1.Rows[pindex].Cells[14].Value = prow.GreigeP_Meas2;
                                dataGridView1.Rows[pindex].Cells[15].Value = prow.GreigeP_Meas3;
                                dataGridView1.Rows[pindex].Cells[16].Value = prow.GreigeP_Meas4;
                                dataGridView1.Rows[pindex].Cells[17].Value = prow.GreigeP_Meas5;
                                dataGridView1.Rows[pindex].Cells[18].Value = prow.GreigeP_Meas6;
                                dataGridView1.Rows[pindex].Cells[19].Value = prow.GreigeP_Meas7;
                                dataGridView1.Rows[pindex].Cells[20].Value = prow.GreigeP_Meas8;
                                dataGridView1.Rows[pindex].Cells[21].Value = prow.GreigeP_Greige_Fk;

                            }
                        }
                        // Now to work out the total to put in the Header
                        //------------------------------------------------------------------
                        var existingGrpx = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk).ToList();
                        foreach (var row in existingGrpx)
                        {
                            var Count = -1;
                            foreach (var rowx in ExistingPieces)
                            {
                                ++Count;
                                if (rowx.GreigeP_DyeBatch_FK == selected.DYEB_Pk && row.DYEBD_GreigeProduction_FK == rowx.GreigeP_Pk)
                                {
                                    dataGridView1.Rows[Count].Cells[2].Value = true;
                                    dataGridView1.Focus();
                                    dataGridView1.CurrentCell = dataGridView1[2, Count];
                                    DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(9999, row.DYEBO_GVRowNumber);
                                    dataGridView1_CellContentClick(dataGridView1, ee);
                                }
                            }
                        }
                        
                    }
               }
            }
        }

        private void frmDyeBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //This Transaction might be a 
            using (var context = new TTI2Entities())
            {
                var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                if (DB != null)
                {
                    // this code is for the situation when greige mighyt have been previously
                    // selected in a batch and now the batch is being recalled
                    // the only way to get the original records back
                    //------------------------------------------
                    context.TLKNI_GreigeProduction
                          .Where(x => x.GreigeP_DyeBatch_FK == DB.DYEB_Pk)
                          .ToList()
                          .ForEach(a => a.GreigeP_Dye = true);

                    context.SaveChanges();

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formLoaded)
            {
                var Batches = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                if (Batches != null)
                {
                    DialogResult res = MessageBox.Show("Please confirm this action", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Record = context.TLDYE_DyeBatch.Find(Batches.DYEB_Pk);
                            if (Record != null)
                            {
                                Record.DYEB_Closed = true;
                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Record successfully updated");
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
                }
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded && !PrevDyeBatch)
            {
                var selected = (TLADM_Griege)cmboGreige.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;

                    }
                    var index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells[1].Value = "Fabric :";
                    dataGridView2.Rows[index].Cells[2].Value = selected.TLGreige_Description;
                    dataGridView2.Rows[index].Cells[3].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[4].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[5].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[6].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[7].Value = selected.TLGreige_Id;//  Body
                    dataGridView2.Rows[index].Cells[8].Value = true;                 //  Body
                    dataGridView2.Rows[index].Cells[9].Value = 0;
                }
            }
        }

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded && !PrevDyeBatch)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;

                }

                var selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Production = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_CommisionCust && x.GreigeP_CommissionCust_FK == selected.Cust_Pk && !x.GreigeP_Dye).ToList();
                        foreach (var prow in Production)
                        {
                            var pindex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[pindex].Cells[0].Value = 0;
                            dataGridView1.Rows[pindex].Cells[1].Value = prow.GreigeP_Pk;
                            dataGridView1.Rows[pindex].Cells[3].Value = prow.GreigeP_PieceNo;
                            dataGridView1.Rows[pindex].Cells[4].Value = Math.Round(prow.GreigeP_weight, 2);

                            var Greige = context.TLADM_Griege.Find(prow.GreigeP_Greige_Fk);
                            if (Greige != null)
                                dataGridView1.Rows[pindex].Cells[5].Value = Greige.TLGreige_Description;

                            var KnitOrder = context.TLKNI_Order.Find(prow.GreigeP_KnitO_Fk);
                            if (KnitOrder != null)
                            {

                                dataGridView1.Rows[pindex].Cells[9].Value = "KO" + KnitOrder.KnitO_OrderNumber.ToString().PadLeft(6, '0');
                                var YarnOrder = context.TLSPN_YarnOrder.Find(KnitOrder.KnitO_YarnO_FK);
                                if (YarnOrder != null)
                                {
                                    var YarnType = context.TLADM_Yarn.Find(YarnOrder.Yarno_YarnType_FK);
                                    if (YarnType != null)
                                    {
                                        dataGridView1.Rows[pindex].Cells[6].Value = Math.Round(YarnType.YA_TexCount, 1);
                                        var Supplier = context.TLADM_Suppliers.Find(YarnType.YA_Supplier_FK);
                                        if (Supplier != null)
                                        {
                                            dataGridView1.Rows[pindex].Cells[7].Value = Supplier.Sup_Description;
                                        }
                                    }
                                }

                            }
                            //------------------------------------------------------------------------------
                            // Column 8 still have to do pallet number's ??????????????????
                            //--------------------------------------------------------------------

                            dataGridView1.Rows[pindex].Cells[10].Value = "Greige";
                            dataGridView1.Rows[pindex].Cells[11].Value = prow.GreigeP_Grade;
                            dataGridView1.Rows[pindex].Cells[12].Value = prow.GreigeP_Remarks;
                            dataGridView1.Rows[pindex].Cells[13].Value = prow.GreigeP_Meas1;
                            dataGridView1.Rows[pindex].Cells[14].Value = prow.GreigeP_Meas2;
                            dataGridView1.Rows[pindex].Cells[15].Value = prow.GreigeP_Meas3;
                            dataGridView1.Rows[pindex].Cells[16].Value = prow.GreigeP_Meas4;
                            dataGridView1.Rows[pindex].Cells[17].Value = prow.GreigeP_Meas5;
                            dataGridView1.Rows[pindex].Cells[18].Value = prow.GreigeP_Meas6;
                            dataGridView1.Rows[pindex].Cells[19].Value = prow.GreigeP_Meas7;
                            dataGridView1.Rows[pindex].Cells[20].Value = prow.GreigeP_Meas8;
                            dataGridView1.Rows[pindex].Cells[21].Value = prow.GreigeP_Greige_Fk;
                            

                        }
                    }
                }
            }

        }

        private void cmboTrims_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded && !PrevDyeBatch)
            {
                var selected = (TLADM_Griege)cmboGreige.SelectedItem;
                if (selected != null)
                {
                    var index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells[1].Value = "Trims :";
                    dataGridView2.Rows[index].Cells[2].Value = selected.TLGreige_Description;
                    dataGridView2.Rows[index].Cells[3].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[4].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[5].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[6].Value = 0.00M;
                    dataGridView2.Rows[index].Cells[7].Value = selected.TLGreige_Id;//  Body
                    dataGridView2.Rows[index].Cells[8].Value = false; //  Body
                    dataGridView2.Rows[index].Cells[9].Value = 0;
                }

            }
        }

        private void dtpBatchDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formLoaded)
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

        private void cmboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded)
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

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formLoaded && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                       oDgv.Rows.RemoveAt(oDgv.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void rbReprocessMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded && oRad.Checked)
            {
                using (var context = new TTI2Entities())
                {
                    formLoaded = false;
                    cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_CommissinCust == true  && x.DYEB_Reprocess == true).OrderBy(x => x.DYEB_BatchNo).ToList();
                    cmboBatches.ValueMember = "DYEB_Pk";
                    cmboBatches.DisplayMember = "DYEB_BatchNo";
                    cmboBatches.SelectedValue = -1;
                    formLoaded = true;
                }
            }
        }

        private void rbStandardMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded && oRad.Checked)
                SetUp(false);
        }
   }
}
