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

namespace CustomerServices
{
    public partial class frmBoxSplitDetail : Form
    {
        DataGridViewRow _CurrentRow;
        TLADM_WhseStore _WhseStore;
        DataGridViewTextBoxColumn oTxtA;  // 0
        DataGridViewTextBoxColumn oTxtB;  // 1
        DataGridViewTextBoxColumn oTxtC;  // 2
        DataGridViewTextBoxColumn oTxtD;  // 3
        DataGridViewTextBoxColumn oTxtE;  // 4
        DataGridViewComboBoxColumn oCmbA; // 5
        bool formloaded;
        string[][] MandatoryFields;

   
     
        Util core;

        public frmBoxSplitDetail(DataGridViewRow CurrentRow, TLADM_WhseStore WhseStore)
        {
            MandatoryFields = new string[][]
                {   new string[] {"0", "A"},
                    new string[] {"1", "B"},
                    new string[] {"2", "C"}, 
                    new string[] {"3", "D"}, 
                    new string[] {"4", "E"},
                    new string[] {"5", "F"},
                    new string[] {"6", "G"},
                    new string[] {"7", "H"}, 
                    new string[] {"8", "I"},
                    new string[] {"9", "J"},
                    new string[] {"10", "K"}
                };

            InitializeComponent();
           
            _CurrentRow = CurrentRow;
            _WhseStore = WhseStore;


         
            txtOriginalBoxNo.Text = CurrentRow.Cells[2].Value.ToString();
            txtOriginalBoxQty.Text = CurrentRow.Cells[4].Value.ToString();

            oTxtA = new DataGridViewTextBoxColumn(); // 0
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB = new DataGridViewTextBoxColumn(); // 1
            oTxtB.ValueType = typeof(string);
            oTxtB.HeaderText = "Box Number"; 
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn(); // 2
            oTxtC.ValueType = typeof(int);
            oTxtC.HeaderText = "Quantity";
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn(); // 3
            oTxtD.ValueType = typeof(int);
            oTxtD.HeaderText = "Weight";
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn(); // 4
            oTxtE.ValueType = typeof(string);
            oTxtE.HeaderText = "Grade";
            dataGridView1.Columns.Add(oTxtE);

            oCmbA = new DataGridViewComboBoxColumn(); // 5
            oCmbA.HeaderText = "Box Type";
            oCmbA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oCmbA);
          
            core = new Util();

            dataGridView1.Focus();

            formloaded = true;

        }

        private void frmBoxSplitDetail_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                if (Dept != null)
                {
                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    if (LNU != null)
                    {
                        txtTransNumber.Text = "BS" + LNU.col1.ToString().PadLeft(5, '0');

                    }

                    oCmbA.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_Description).ToList();
                    oCmbA.ValueMember = "TLADMBT_Pk";
                    oCmbA.DisplayMember = "TLADMBT_Description";

                }

                var Pk = (int)_CurrentRow.Cells[0].Value;
                /*
                var Existing = context.TLCSV_BoxSplit.Where(x => x.TLCMTBS_Completed_FK == Pk).ToList();
                foreach (var row in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = row.TLCMTBS_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = row.TLCMTBS_BoxNo;
                    dataGridView1.Rows[index].Cells[2].Value = row.TLCMTBS_Qty;
                    dataGridView1.Rows[index].Cells[3].Value = row.TLCMTBS_Weight;
                    dataGridView1.Rows[index].Cells[4].Value = row.TLCMTBS_Grade;
                }
                 */
            }
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             var index = e.RowIndex;
             if (oDgv.ColumnCount > 4)
             {
                 using (var context = new TTI2Entities())
                 {
                     oDgv.Rows[e.RowIndex].Cells[1].Value = string.Empty;

                     foreach (var MandatoryField in MandatoryFields)
                     {
                         var result = (from u in MandatoryFields
                                       where u[0] == index.ToString()
                                       select u).FirstOrDefault();

                         var Box = _CurrentRow.Cells[2].Value + result[1];

                         var AllReadyExisting = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == Box).FirstOrDefault();
                         if (AllReadyExisting != null)
                         {
                             index += 1;
                             continue;
                         }

                         DataGridViewRow SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                                          where Rows.Cells[1].Value.ToString() == Box
                                                          select Rows).FirstOrDefault();
                         if (SingleRow != null)
                         {
                                 index += 1;
                                 continue;
                         }
                         
                         oDgv.Rows[e.RowIndex].Cells[1].Value = _CurrentRow.Cells[2].Value + result[1];
                         oDgv.Rows[e.RowIndex].Cells[2].Value = 0;
                         oDgv.Rows[e.RowIndex].Cells[3].Value = 0.00M;
                         oDgv.Rows[e.RowIndex].Cells[4].Value = string.Empty;
                         break;
                     }
                 }
             }
             
         }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

            DataGridView oDgv = sender as DataGridView;
            if (e.Column.Index > 4)
            {
                using (var context = new TTI2Entities())
                {
                    var index = oDgv.NewRowIndex;

                    foreach (var MandatoryField in MandatoryFields)
                    {
                        var result = (from u in MandatoryFields
                                      where u[0] == index.ToString()
                                      select u).FirstOrDefault();

                        var Box = _CurrentRow.Cells[2].Value + result[1];

                        var AllReadyExisting = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == Box).FirstOrDefault();
                        if (AllReadyExisting != null)
                        {
                            MessageBox.Show("This Box already exists in the Warehouse " + Box);
                            index += 1;
                            continue;
                        }

                        if (result != null)
                        {
                            if (oDgv.Rows[oDgv.NewRowIndex].Cells[1].Value == null)
                                oDgv.Rows[oDgv.NewRowIndex].Cells[1].Value = _CurrentRow.Cells[2].Value + result[1];
                            oDgv.Rows[oDgv.NewRowIndex].Cells[2].Value = 0;
                            oDgv.Rows[oDgv.NewRowIndex].Cells[3].Value = 0.00M;
                            oDgv.Rows[oDgv.NewRowIndex].Cells[4].Value = string.Empty;
                            break;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLCSV_StockOnHand SOH = null;

            if (oBtn != null && formloaded)
            {
               
                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (string)Rows.Cells[4].Value == String.Empty && (int)Rows.Cells[2].Value != 0
                                 select Rows).FirstOrDefault();

                if (SingleRow != null)
                {
                    MessageBox.Show("There is a record with no grade assigned. Row No " + SingleRow.Index.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               

                var Total = this.dataGridView1.Rows.Cast<DataGridViewRow>()
                               .Sum(x => Convert.ToInt32(x.Cells[2].Value.ToString()));

                var OriginalQty = Convert.ToInt32(txtOriginalBoxQty.Text);

                if (Total > OriginalQty)
                {
                    MessageBox.Show("New quantity greater than original qty" + Environment.NewLine + "Please correct");
                    return;
                }


                if (OriginalQty - Total != 0 && !_WhseStore.WhStore_RePack)
                {
                    MessageBox.Show("The total split must be equal to original qty");
                    return;
                }

                if (txtAuthorised.Text.Length == 0)
                {
                    MessageBox.Show("Please complete the Authorised by");
                    return;
 
                }

                using (var context = new TTI2Entities())
                {
                    
                    var MainIndex = (int)_CurrentRow.Cells[0].Value;
                    TLADM_LastNumberUsed LNU = null;
                    decimal number;

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                    if (Dept != null)
                    {
                        LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    }

                    SOH = context.TLCSV_StockOnHand.Find(MainIndex);
                   

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        if ((int)row.Cells[2].Value == 0)
                            continue;
                        
                        
                        TLCSV_BoxSplit BoxSplit = new TLCSV_BoxSplit();

                        bool Add = true;
                        var index = 0;

                        if (row.Cells[0].Value != null)
                        {
                            index = (int)row.Cells[0].Value;

                            BoxSplit = context.TLCSV_BoxSplit.Find(index);
                            if (BoxSplit == null)
                            {
                                BoxSplit = new TLCSV_BoxSplit();
                            }
                            else
                            {
                                Add = false;
                            }
                        }

                        BoxSplit.TLCMTBS_BoxNo = row.Cells[1].Value.ToString();
                        BoxSplit.TLCMTBS_AuthorisedBy = this.txtAuthorised.Text;
                        BoxSplit.TLCMTBS_Completed_FK = MainIndex;
                        BoxSplit.TLCMTBS_Grade = row.Cells[4].Value.ToString().ToUpper();
                        BoxSplit.TLCMTBS_Qty = (int)row.Cells[2].Value;
                        number = Decimal.Parse(row.Cells[3].Value.ToString());
                        
                        BoxSplit.TLCMTBS_Weight = number;
                        BoxSplit.TLCMTBS_AdjustmentNo = LNU.col1;
                        
                        if (Add)
                            context.TLCSV_BoxSplit.Add(BoxSplit);

                        //now we need to add the box to the Stock on Hand table
                        //------------------------------------------------------------------
                        if (SOH != null)
                        {
                            TLCSV_StockOnHand SplitB = new TLCSV_StockOnHand();
                            SplitB.TLSOH_BoxSelected_FK = SOH.TLSOH_BoxSelected_FK;
                            SplitB.TLSOH_CMT_FK = SOH.TLSOH_CMT_FK;
                            SplitB.TLSOH_Customer_Fk = SOH.TLSOH_Customer_Fk;
                            SplitB.TLSOH_DateIntoStock = SOH.TLSOH_DateIntoStock;
                            SplitB.TLSOH_DNListDate = SOH.TLSOH_DNListDate;
                            SplitB.TLSOH_DNListNo = SOH.TLSOH_DNListNo;
                            SplitB.TLSOH_MoveAdjust_FK = SOH.TLSOH_MoveAdjust_FK;
                            SplitB.TLSOH_Movement_FK = SOH.TLSOH_Movement_FK;
                            SplitB.TLSOH_Notes = SOH.TLSOH_Notes;
                            SplitB.TLSOH_PastelNumber = SOH.TLSOH_PastelNumber;
                            SplitB.TLSOH_Picked = SOH.TLSOH_Picked;
                            SplitB.TLSOH_PickListDate = SOH.TLSOH_PickListDate;
                            SplitB.TLSOH_PickListNo = SOH.TLSOH_PickListNo;
                            SplitB.TLSOH_POOrder_FK = SOH.TLSOH_POOrder_FK;
                            SplitB.TLSOH_POOrderDetail_FK = SOH.TLSOH_POOrderDetail_FK;
                            SplitB.TLSOH_QtyAdjusted = SOH.TLSOH_QtyAdjusted;
                            SplitB.TLSOH_Returned = SOH.TLSOH_Returned;
                            SplitB.TLSOH_ReturnedBoxQty = SOH.TLSOH_ReturnedBoxQty;
                            SplitB.TLSOH_ReturnedDate = SOH.TLSOH_ReturnedDate;
                            SplitB.TLSOH_ReturnedWeight = SOH.TLSOH_ReturnedWeight;
                            SplitB.TLSOH_ReturnNumber = SOH.TLSOH_ReturnNumber;
                            SplitB.TLSOH_Size_FK = SOH.TLSOH_Size_FK;
                            SplitB.TLSOH_Sold = SOH.TLSOH_Sold;
                            SplitB.TLSOH_SoldDate = SOH.TLSOH_SoldDate;
                            SplitB.TLSOH_Style_FK = SOH.TLSOH_Style_FK;
                            SplitB.TLSOH_Transfered = SOH.TLSOH_Transfered;
                            SplitB.TLSOH_TransferNotes = SOH.TLSOH_TransferNotes;
                            SplitB.TLSOH_WareHouse_FK = SOH.TLSOH_WareHouse_FK;
                            SplitB.TLSOH_WareHouseDeliveryNo = SOH.TLSOH_WareHouseDeliveryNo;
                            SplitB.TLSOH_WareHousePickList = SOH.TLSOH_WareHousePickList;
                            SplitB.TLSOH_Colour_FK = SOH.TLSOH_Colour_FK;
                            SplitB.TLSOH_CutSheet_FK = SOH.TLSOH_CutSheet_FK;
                            SplitB.TLSOH_BoxedQty  = BoxSplit.TLCMTBS_Qty;
                            SplitB.TLSOH_BoxNumber = BoxSplit.TLCMTBS_BoxNo;
                            SplitB.TLSOH_Split = false;
                            SplitB.TLSOH_Weight = number;
                            SplitB.TLSOH_Grade = BoxSplit.TLCMTBS_Grade;
                          
                            if (row.Cells[5].Value != null)
                                SplitB.TLSOH_BoxType = (int)row.Cells[5].Value;
                            else
                                SplitB.TLSOH_BoxType = 1;

                            context.TLCSV_StockOnHand.Add(SplitB);
                        }
                    }

                    try
                    {
                        if(SOH != null)
                        {
                            SOH.TLSOH_BoxedQty = OriginalQty - Total;

                            if (!_WhseStore.WhStore_RePack)
                                SOH.TLSOH_Split = true;
                            else
                                if(SOH.TLSOH_BoxedQty == 0)
                                    SOH.TLSOH_Split = true;
                        }

                        LNU.col1 += 1;

                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
              

                var Cell = oDgv.CurrentCell;

                if (Cell.ColumnIndex == 2)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else if (Cell.ColumnIndex == 3)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else
                {
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
              
            }
        }

       
    }
}
