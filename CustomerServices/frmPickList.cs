using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CustomerServices
{
    public partial class frmPickList : Form
    {
        bool formloaded;

        //--------------------------------------------
        // datagridview1 
        //-------------------------------------------------
        DataGridViewTextBoxColumn oTxtA;   // 0 Record Primary Key        
        DataGridViewCheckBoxColumn oChkA;  // 1 Select Yes or No 
        DataGridViewTextBoxColumn oTxtB;   // 2 WareHouse 
        DataGridViewTextBoxColumn oTxtC;   // 3 Garment Size 
        DataGridViewTextBoxColumn oTxtD;   // 4 Box Number 
        DataGridViewTextBoxColumn oTxtE;   // 5 Boxed Quantity
        DataGridViewTextBoxColumn oTxtF;   // 6 Box Grade
        DataGridViewTextBoxColumn oTxtG;   // 6 Style FK (Not Visible )
        DataGridViewTextBoxColumn oTxtH;   // 7 Color  FK (Not Visible);
        DataGridViewTextBoxColumn oTxtJ;   // 8 Size FK (Not Visible);
        //--------------------------------------------
        // datagridview2 
        //-------------------------------------------------

        DataGridViewTextBoxColumn oTxtAA;   // 0 Record Primary Key
        DataGridViewCheckBoxColumn oChkAA;  // 1 Select Yes or No 
        DataGridViewTextBoxColumn oTxtBA;   // 2 Style Description 
        DataGridViewTextBoxColumn oTxtCA;   // 3 Colour Descriptiion
        DataGridViewTextBoxColumn oTxtDA;   // 4 Size Description 
        DataGridViewTextBoxColumn oTxtEA;   // 5 Order Quantity
        DataGridViewTextBoxColumn oTxtFA;   // 6 Assigned Quantity
        DataGridViewTextBoxColumn oTxtGA;   // 7 Style FK
        DataGridViewTextBoxColumn oTxtHA;   // 8 Colour FK
        DataGridViewTextBoxColumn oTxtJA;   // 9 Size FK

        int TransNumber;
        string Mach_IP;

        public frmPickList()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();  //0 Record Primary Key
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn(); // 1 Check Box 
            oChkA.Visible = true;
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();  // 2 WareHouse 
            oTxtB.HeaderText = "Warehouse";
            oTxtB.ValueType = typeof(String);
            oTxtB.Width = 275;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();   // 3 Garment Size
            oTxtC.HeaderText = "Garment Size";
            oTxtC.ValueType = typeof(String);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();  // 4 Box Number
            oTxtD.HeaderText = "Box Number";
            oTxtD.ValueType = typeof(int);
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();  // 5 Boxed Qty
            oTxtE.HeaderText = "Boxed Qty";
            oTxtE.ValueType = typeof(int);
            oTxtE.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF = new DataGridViewTextBoxColumn();  // 6 Box Grade
            oTxtF.ValueType = typeof(string);
            oTxtF.ReadOnly = true;
            oTxtF.HeaderText = "Grade";
            dataGridView1.Columns.Add(oTxtF);

            oTxtG = new DataGridViewTextBoxColumn();  // 7 Style FK
            oTxtG.Visible = false;
            oTxtG.ValueType = typeof(int);
            oTxtG.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtG);

            oTxtH = new DataGridViewTextBoxColumn();   // 8 Colour FK 
            oTxtH.Visible = false;
            oTxtH.ValueType = typeof(int);
            oTxtH.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtH);

            oTxtJ = new DataGridViewTextBoxColumn();   // 9 Size FK 
            oTxtJ.Visible = false;
            oTxtJ.ValueType = typeof(int);
            oTxtJ.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtJ);

            oTxtAA = new DataGridViewTextBoxColumn();   //0
            oTxtAA.ValueType = typeof(int);
            oTxtAA.ReadOnly = true;
            oTxtAA.Visible = false;
            dataGridView2.Columns.Add(oTxtAA);

            oChkAA = new DataGridViewCheckBoxColumn();    //1
            oChkAA.Visible = true;
            oChkAA.HeaderText = "Select";
            dataGridView2.Columns.Add(oChkAA);

            oTxtBA = new DataGridViewTextBoxColumn();   //2
            oTxtBA.HeaderText = "Style";
            oTxtBA.Width = 170;
            oTxtBA.ValueType = typeof(String);
            oTxtBA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtBA);

            oTxtCA = new DataGridViewTextBoxColumn();   //3 
            oTxtCA.HeaderText = "Colour";
            oTxtCA.ValueType = typeof(String);
            oTxtCA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtCA);

            oTxtDA = new DataGridViewTextBoxColumn();    //4
            oTxtDA.HeaderText = "Size";
            oTxtDA.ValueType = typeof(int);
            oTxtDA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtDA);

            oTxtEA = new DataGridViewTextBoxColumn();    //5
            oTxtEA.HeaderText = "Outstanding Qty";
            oTxtEA.ValueType = typeof(int);
            oTxtEA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtEA);

            oTxtFA = new DataGridViewTextBoxColumn();    //6
            oTxtFA.HeaderText = "Selected Qty";
            oTxtFA.ValueType = typeof(int);
            oTxtFA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtFA);

            oTxtGA = new DataGridViewTextBoxColumn();    //7
            oTxtGA.Visible = false;
            oTxtGA.ValueType = typeof(int);
            oTxtGA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtGA);

            oTxtHA = new DataGridViewTextBoxColumn();    //8
            oTxtHA.Visible = false;
            oTxtHA.ValueType = typeof(int);
            oTxtHA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtHA);

            oTxtJA = new DataGridViewTextBoxColumn();    //9
            oTxtJA.Visible = false;
            oTxtJA.ValueType = typeof(int);
            oTxtJA.ReadOnly = true;
            dataGridView2.Columns.Add(oTxtJA);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToOrderColumns = false;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void frmPickList_Load(object sender, EventArgs e)
        {
            formloaded = false;
            TransNumber = 0;
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

                cmboWareHouses.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                cmboWareHouses.DisplayMember = "WhStore_Description";
                cmboWareHouses.ValueMember = "WhStore_Id";
                cmboWareHouses.SelectedValue = -1;

                //-------------------------------------------------------
                //
                //-----------------------------------------------------------
                Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                           .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                           .ToString();
                
                //------------------------------------------------------------------------------------
                // First we must ensure that any previous records that may exist are deleted and that we start with a clean slate
                //---------------------------------------------------------------------------
                context.TLCSV_PickingListMaster.RemoveRange(context.TLCSV_PickingListMaster.Where(x => x.TLPL_IPAddress == Mach_IP));

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }
            formloaded = true;
        }

        private void cmboCurrentOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            Repository repo = new Repository();
            var parms = new CustomerServicesParameters();
            IList<TLCSV_PuchaseOrderDetail> PODetails = null;
  
           
            if (oCmbo != null && formloaded)
            {
                var POSelected = (TLCSV_PurchaseOrder)oCmbo.SelectedItem;
                if (POSelected != null)
                {
                    var CustomerFile = (TLADM_CustomerFile)cmboCustomers.SelectedItem;

                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();

                    if (POSelected.TLCSVPO_RequiredDate > DateTime.Now)
                    {
                        DateTime dt = Convert.ToDateTime(POSelected.TLCSVPO_RequiredDate.ToShortDateString());
                        DialogResult Res = MessageBox.Show("This order is only required on the " + dt.ToString("dd/MM/yyy") + " Continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (Res == DialogResult.No)
                        {
                            return;
                        }
                    }

                    if (POSelected.TLCSVPO_Closeed)
                    {
                        dataGridView2.Enabled = false;
                        dataGridView2.AllowUserToAddRows = false;
                        dataGridView2.AllowUserToOrderColumns = false;
                    }
                    using (var context = new TTI2Entities())
                    {
                        PODetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == POSelected.TLCSVPO_Pk && !x.TLCUSTO_Closed).ToList();
                        if (POSelected.TLCSVPO_RepackTransaction && CustomerFile.Cust_WareHouse_FK != null)
                        {
                            var Whse = CustomerFile.Cust_WareHouse_FK;
                            string WhseDescription = context.TLADM_WhseStore.Find(Whse).WhStore_Description ;

                            foreach (var PODetail in PODetails)
                            {
                                var STOH = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == PODetail.TLCUSTO_Style_FK && x.TLSOH_Colour_FK == PODetail.TLCUSTO_Colour_FK && x.TLSOH_Size_FK == PODetail.TLCUSTO_Size_FK
                                                                     && x.TLSOH_WareHouse_FK == Whse && !x.TLSOH_Picked).Sum(x =>(int ?) x.TLSOH_BoxedQty) ?? 0;

                                if (STOH < PODetail.TLCUSTO_Qty)
                                {
                                    var Style = context.TLADM_Styles.Find(PODetail.TLCUSTO_Style_FK).Sty_Description;
                                    var Colour = context.TLADM_Colours.Find(PODetail.TLCUSTO_Colour_FK).Col_Display;

                                    String Message = "There appears not to be enough stock in " + WhseDescription + Environment.NewLine + "Style " + Style + " Colour " + Colour;
                                    String UpperMessage = "Quantity ordered " + PODetail.TLCUSTO_Qty.ToString() + " Quantity on Hand " + STOH.ToString();
                                    
                                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                    {
                                        MessageBox.Show(Message, UpperMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    
                                    return; 

                                }
                            }
                            
                        }
                        foreach (var Row in PODetails)
                        {
                            //-----------------------------------------------------------
                            // Original Order Amount 
                            //-----------------------------------------------------------
                           var Nett = Row.TLCUSTO_Qty;
                           
                            //-----------------------------------------------------------
                            // Picked To date  
                           //--------------------------------
                           var AllReadyPicked = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == Row.TLCUSTO_Pk).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                           try
                           {
                                Nett = Row.TLCUSTO_Qty - AllReadyPicked;

                               //-------------------
                               // If equal or less than zero
                               //----------------------------------------

                                if (formloaded)
                                {
                                    if (Nett <= 0)
                                    {
                                       // MessageBox.Show("Quantity Issued " + AllReadyPicked.ToString() + " exceeds Qty ordered", "Qty originally ordered " + Row.TLCUSTO_Qty.ToString());
                                        continue;
                                    }
                                }
                          }
                          catch (Exception ex)
                          {
                                MessageBox.Show(ex.Message);
                          }
                           
                          var indexx = dataGridView2.Rows.Add();
                          dataGridView2.Rows[indexx].Cells[0].Value = Row.TLCUSTO_Pk;
                          dataGridView2.Rows[indexx].Cells[1].Value = false;
                          dataGridView2.Rows[indexx].Cells[2].Value = context.TLADM_Styles.Find(Row.TLCUSTO_Style_FK).Sty_Description;
                          dataGridView2.Rows[indexx].Cells[3].Value = context.TLADM_Colours.Find(Row.TLCUSTO_Colour_FK).Col_Display;
                          dataGridView2.Rows[indexx].Cells[4].Value = context.TLADM_Sizes.Find(Row.TLCUSTO_Size_FK).SI_Description;
                          dataGridView2.Rows[indexx].Cells[5].Value = Nett;
                          dataGridView2.Rows[indexx].Cells[6].Value = 0;
                          dataGridView2.Rows[indexx].Cells[7].Value = Row.TLCUSTO_Style_FK;
                          dataGridView2.Rows[indexx].Cells[8].Value = Row.TLCUSTO_Colour_FK;
                          dataGridView2.Rows[indexx].Cells[9].Value = Row.TLCUSTO_Size_FK;
                        }

                     }
                 }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Stock on Hand Data 
            //--------------------------------------------------------------------------
            DataGridView oDgv = sender as DataGridView;
            
            TLADM_CustomerFile Customer = null;
            TLCSV_PurchaseOrder PO = null;
            TLCSV_StockOnHand Box = null;
            TLCSV_RePackConfig RePackConFig = null;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                PO = (TLCSV_PurchaseOrder)cmboCurrentOrders.SelectedItem;

                Customer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;

                var CurrentRow = oDgv.CurrentRow;

                var SingleRow = (from Rows in dataGridView2.Rows.Cast<DataGridViewRow>()
                                     where (int)Rows.Cells[7].Value == (int)CurrentRow.Cells[7].Value
                                     && (int)Rows.Cells[8].Value == (int)CurrentRow.Cells[8].Value
                                     && (int)Rows.Cells[9].Value == (int)CurrentRow.Cells[9].Value
                                     select Rows).FirstOrDefault();
                if (SingleRow != null)
                {
                    var POD = (int)SingleRow.Cells[0].Value;      // PODetail Record
                    var SOH = (int)CurrentRow.Cells[0].Value;     // SOHDetail Record 

                    var Total = (int)SingleRow.Cells[6].Value;
                    if (!Customer.Cust_RePack)
                    {
                        if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                        {
                            Total += (int)CurrentRow.Cells[5].Value;

                            if (Total > (int)SingleRow.Cells[5].Value)
                            {
                                DialogResult oDlgRes = MessageBox.Show("Quantity selected exceeds quantity ordered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }
                        else
                        {
                            if (!Customer.Cust_RePack)
                                Total -= (int)CurrentRow.Cells[5].Value;
                        }
                    }
                    else
                    {
                        if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                        {
                           using (var context = new TTI2Entities())
                           {
                                    Total += context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrderDetail_FK == POD && x.REPACT_StockOnHand_FK == SOH).Sum(x => (int?)x.REPACT_BoxedQty) ?? 0;
                           }
                           if (Total > (int)SingleRow.Cells[5].Value && (bool)SingleRow.Cells[1].Value == true)
                           {
                                    MessageBox.Show("Quantity selected exceeds quantity ordered", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                           }
                        
                        }
                        else
                        {
                            MessageBox.Show("Not supported for Repack Customers", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                    }

                    SingleRow.Cells[6].Value = Total;
                    
                     using (var context = new TTI2Entities())
                     {
                        var Record = context.TLCSV_StockOnHand.Find(SOH);
                        if (Record != null)
                        {
                            if (Customer.Cust_RePack && PO.TLCSVPO_RepackTransaction)
                            {
                                // This bit of code has been developed to handle the concept of Repack Centers 
                                //==============================================================================
                                Box = context.TLCSV_StockOnHand.Find(SOH);
                                if (Box != null)
                                {
                                    var BoxedQty = Box.TLSOH_BoxedQty;
                                    var OrigQty = Box.TLSOH_BoxedQty;
                                    var PurchaseOrder = context.TLCSV_PurchaseOrder;

                                    IList<TLCSV_RePackConfig> RePackConfigs = context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == PO.TLCSVPO_Pk && x.PORConfig_Size_FK == Box.TLSOH_Size_FK).ToList();
                                    if (RePackConfigs.Count > 0)
                                    {
                                        

                                        foreach (var RePackCon in RePackConfigs)
                                        {
                                            int AllReadyPicked = RePackCon.PORConfig_SizeBoxQty_Picked;
                                            int QtyRequired = RePackCon.PORConfig_TotalBoxes * RePackCon.PORConfig_SizeBoxQty;

                                            if (AllReadyPicked < QtyRequired)
                                            {
                                                if (BoxedQty > 0)
                                                {
                                                    int ValueToPack = 0;
                                                    if (BoxedQty < (QtyRequired - AllReadyPicked))
                                                        ValueToPack = BoxedQty;
                                                    else
                                                        ValueToPack = QtyRequired - AllReadyPicked;


                                                    RePackCon.PORConfig_SizeBoxQty_Picked += ValueToPack;
                                                    BoxedQty -= ValueToPack;
                                                    Total += ValueToPack;

                                                    RePackConFig = RePackCon;

                                                    // Add a record to the Transaction File 
                                                    //=======================================
                                                    TLCSV_RePackTransactions Trans = new TLCSV_RePackTransactions();
                                                    Trans.REPACT_BoxedQty = ValueToPack;
                                                    Trans.REPACT_RePackConfig_FK = RePackCon.PORConfig_Pk;
                                                    Trans.REPACT_StockOnHand_FK = Box.TLSOH_Pk;
                                                    Trans.REPACT_PurchaseOrderDetail_FK = POD;
                                                    Trans.REPACT_PurchaseOrder_FK = PO.TLCSVPO_Pk;

                                                    context.TLCSV_RePackTransactions.Add(Trans);
                                                }
                                            }
                                        }
                                    }

                                    SingleRow.Cells[6].Value = Total;

                                    if (BoxedQty > 0)
                                    {
                                        //======================================================
                                        // There is a chance that the user selected a box with a quantity more than the stipulated contract
                                        // =========================================
                                        // 1st Step : Reduce the Stock Record with what is left over;
                                        //=================================================================
                                        Box.TLSOH_BoxedQty = OrigQty - BoxedQty;
                                        if (Box.TLSOH_BoxedQty < 0)
                                            Box.TLSOH_BoxedQty = 0;

                                        // 2nd Step : Create a box split Transaaction 
                                        //===================================================
                                        string[][] MandatoryFields;
                                        MandatoryFields = new string[][]
                                        {    new string[] {"0", "A"},
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

                                        int index = 0;

                                        foreach (var MandatoryField in MandatoryFields)
                                        {
                                            var result = (from u in MandatoryFields
                                                          where u[0] == index.ToString()
                                                          select u).FirstOrDefault();

                                            var NewBoxNumber = Box.TLSOH_BoxNumber + result[1];

                                            var AllReadyExisting = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == NewBoxNumber).FirstOrDefault();
                                            if (AllReadyExisting != null)
                                            {
                                                index += 1;
                                                continue;
                                            }

                                            TLCSV_StockOnHand SplitB = new TLCSV_StockOnHand();
                                            SplitB.TLSOH_BoxSelected_FK = Box.TLSOH_BoxSelected_FK;
                                            SplitB.TLSOH_BoxNumber = NewBoxNumber;
                                            SplitB.TLSOH_BoxedQty = BoxedQty;
                                            SplitB.TLSOH_CMT_FK = Box.TLSOH_CMT_FK;
                                            SplitB.TLSOH_Customer_Fk = Box.TLSOH_Customer_Fk;
                                            SplitB.TLSOH_DateIntoStock = Box.TLSOH_DateIntoStock;
                                            SplitB.TLSOH_DNListDate = Box.TLSOH_DNListDate;
                                            SplitB.TLSOH_DNListNo = Box.TLSOH_DNListNo;
                                            SplitB.TLSOH_MoveAdjust_FK = Box.TLSOH_MoveAdjust_FK;
                                            SplitB.TLSOH_Movement_FK = Box.TLSOH_Movement_FK;
                                            SplitB.TLSOH_Notes = Box.TLSOH_Notes;
                                            SplitB.TLSOH_PastelNumber = Box.TLSOH_PastelNumber;
                                            SplitB.TLSOH_Picked = Box.TLSOH_Picked;
                                            SplitB.TLSOH_PickListDate = Box.TLSOH_PickListDate;
                                            SplitB.TLSOH_PickListNo = Box.TLSOH_PickListNo;
                                            SplitB.TLSOH_POOrder_FK = Box.TLSOH_POOrder_FK;
                                            SplitB.TLSOH_POOrderDetail_FK = Box.TLSOH_POOrderDetail_FK;
                                            SplitB.TLSOH_QtyAdjusted = Box.TLSOH_QtyAdjusted;
                                            SplitB.TLSOH_Returned = Box.TLSOH_Returned;
                                            SplitB.TLSOH_ReturnedBoxQty = Box.TLSOH_ReturnedBoxQty;
                                            SplitB.TLSOH_ReturnedDate = Box.TLSOH_ReturnedDate;
                                            SplitB.TLSOH_ReturnedWeight = Box.TLSOH_ReturnedWeight;
                                            SplitB.TLSOH_ReturnNumber = Box.TLSOH_ReturnNumber;
                                            SplitB.TLSOH_Size_FK = Box.TLSOH_Size_FK;
                                            SplitB.TLSOH_Sold = Box.TLSOH_Sold;
                                            SplitB.TLSOH_SoldDate = Box.TLSOH_SoldDate;
                                            SplitB.TLSOH_Style_FK = Box.TLSOH_Style_FK;
                                            SplitB.TLSOH_Transfered = Box.TLSOH_Transfered;
                                            SplitB.TLSOH_TransferNotes = Box.TLSOH_TransferNotes;
                                            SplitB.TLSOH_WareHouse_FK = Box.TLSOH_WareHouse_FK;
                                            SplitB.TLSOH_WareHouseDeliveryNo = Box.TLSOH_WareHouseDeliveryNo;
                                            SplitB.TLSOH_WareHousePickList = Box.TLSOH_WareHousePickList;
                                            SplitB.TLSOH_Colour_FK = Box.TLSOH_Colour_FK;
                                            SplitB.TLSOH_Split = true;
                                            SplitB.TLSOH_Grade = Box.TLSOH_Grade;
                                            SplitB.TLSOH_CutSheet_FK = Box.TLSOH_CutSheet_FK;
                                            SplitB.TLSOH_Weight = 0.0M;
                                            SplitB.TLSOH_BoxType = 1;

                                            context.TLCSV_StockOnHand.Add(SplitB);

                                            break;
                                        }
                                    }
                                }
                                //=============End of Repack Code
                            }

                            if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                            {
                                TLCSV_PickingListMaster Master = new TLCSV_PickingListMaster();
                                Master.TLPL_IPAddress = Mach_IP;
                                Master.TLPL_PODetail_FK = POD;
                                Master.TLPL_StockOnHand_FK = SOH;
                             
                                if(RePackConFig != null)
                                    Master.TLPL_BoxRePack_FK = RePackConFig.PORConfig_Pk;

                                context.TLCSV_PickingListMaster.Add(Master);
                            }
                            else
                            {
                                if (!Customer.Cust_RePack)
                                {
                                    TLCSV_PickingListMaster Master = context.TLCSV_PickingListMaster.Where(x => x.TLPL_PODetail_FK == POD && x.TLPL_StockOnHand_FK == SOH).FirstOrDefault();
                                    if (Master != null)
                                    {
                                        context.TLCSV_PickingListMaster.Remove(Master);
                                    }
                                }
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
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Purchase Order Details
            //-------------------------------------------------------------------
            DataGridView oDgv = sender as DataGridView;
            Repository repo = new Repository();

            TLADM_CustomerFile Customer = new TLADM_CustomerFile();
            TLCSV_PurchaseOrder CustomerOrder = new TLCSV_PurchaseOrder();
            IList<TLCSV_StockOnHand> Boxes = new List<TLCSV_StockOnHand>();

            var parms = new CustomerServicesParameters();

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    CustomerOrder = (TLCSV_PurchaseOrder)cmboCurrentOrders.SelectedItem;

                    dataGridView1.Rows.Clear();

                    var CurrentRow = oDgv.CurrentRow;

                    var AllReadyTicked = (from Rows in dataGridView2.Rows.Cast<DataGridViewRow>()
                                     where (bool)Rows.Cells[1].Value == true
                                      select Rows).ToList();

                    foreach (DataGridViewRow Row in AllReadyTicked)
                    {
                        if (Row.Index == CurrentRow.Index)
                            continue;
                 
                        dataGridView2.Rows[Row.Index].Cells[1].Value = false;
                    }

                    CurrentRow.Cells[6].Value = 0;

                    var Pk = (int)CurrentRow.Cells[7].Value;
                    parms.Styles.Add(repo.LoadStyle(Pk));
                    Pk = (int)CurrentRow.Cells[8].Value;
                    parms.Colours.Add(repo.LoadColour(Pk));
                    Pk = (int)CurrentRow.Cells[9].Value;
                    parms.Sizes.Add(repo.LoadSize(Pk));
                    if(CustomerOrder != null)
                    {
                        //===============================================================
                        // This is Standard Stuff
                        // The concept of Repack Centers has been introduced for Mr Price 
                        // The following code has been introduced to cover that eventuality
                        // st the user may only select stock from a Repack Centre
                        //-------------------------------------------------------------------
                        using ( var context = new TTI2Entities())
                        {
                            Customer = context.TLADM_CustomerFile.Find(CustomerOrder.TLCSVPO_Customer_FK);

                            if(Customer != null)
                            {
                                IList<TLADM_WhseStore> WareHouses = new List<TLADM_WhseStore>();
                                if (Customer.Cust_RePack)
                                   WareHouses = context.TLADM_WhseStore.Where(x => x.WhStore_RePack).ToList();
                                else
                                   WareHouses = context.TLADM_WhseStore.Where(x => !x.WhStore_RePack).ToList();
                                

                                foreach(var WareHouse in WareHouses)
                                {
                                    parms.Whses.Add(repo.LoadWhse(WareHouse.WhStore_Id));
                                }
                            }
                        }
                        //--------------------------------------------------------------
                        // The end of Repack centers Standard Stuff
                        //-----------------------------------------------------------------
                    }
                    
                    if(Customer.Cust_RePack)
                       Boxes = repo.RePacSOHGradeAQuery(parms).ToList();
                    else
                       Boxes = repo.SOHGradeAQuery(parms).ToList();

                    //---------------------------------------------
                    // We need to filter out to a specific warehouse if neccessary
                    //-----------------------------------
                    var SpecficWhse = (TLADM_WhseStore)cmboWareHouses.SelectedItem;
                    if (SpecficWhse != null && Customer != null)
                    {
                        if(!Customer.Cust_RePack)
                            Boxes = Boxes.Where(x => x.TLSOH_WareHouse_FK == SpecficWhse.WhStore_Id).ToList();
                    }

                    if (Boxes.Count() == 0)
                    {
                        MessageBox.Show("There is no stock(Boxes) on hand matching selection made");
                        return;
                    }

                    Boxes = Boxes.OrderBy(x => x.TLSOH_BoxNumber).ToList();
                    
                    using (var context = new TTI2Entities())
                    {
                        foreach (var Box in Boxes)
                        {
                            var Selected = context.TLCSV_PickingListMaster.Where(x => x.TLPL_StockOnHand_FK == Box.TLSOH_Pk).FirstOrDefault();
                            if (Selected != null)
                            {
                                //=============================================================================
                                // This Box Style, Colour, Size may Have been selected under a previous
                                // order number because the transaction Save button has as yet not been activated;
                                // DJL 20/06/2018
                                //======================================================================
                                var PODetail = context.TLCSV_PuchaseOrderDetail.Find(Selected.TLPL_PODetail_FK);
                                if(PODetail != null)
                                {
                                    if(CustomerOrder.TLCSVPO_Pk != PODetail.TLCUSTO_PurchaseOrder_FK) 
                                        continue;
                                }
                            }
                            
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Box.TLSOH_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            if (Selected != null)
                            {
                                dataGridView1.Rows[index].Cells[1].Value = true;
                                var BoxedQty = (int)CurrentRow.Cells[6].Value;
                                CurrentRow.Cells[6].Value = BoxedQty + (int)Box.TLSOH_BoxedQty;
                            }
                            dataGridView1.Rows[index].Cells[2].Value = context.TLADM_WhseStore.Find(Box.TLSOH_WareHouse_FK).WhStore_Description;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Sizes.Find(Box.TLSOH_Size_FK).SI_Description;
                            dataGridView1.Rows[index].Cells[4].Value = Box.TLSOH_BoxNumber;
                            dataGridView1.Rows[index].Cells[5].Value = Box.TLSOH_BoxedQty;
                            dataGridView1.Rows[index].Cells[6].Value = Box.TLSOH_Grade;
                            dataGridView1.Rows[index].Cells[7].Value = Box.TLSOH_Style_FK;
                            dataGridView1.Rows[index].Cells[8].Value = Box.TLSOH_Colour_FK;
                            dataGridView1.Rows[index].Cells[9].Value = Box.TLSOH_Size_FK;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLCSV_PurchaseOrder PO = null;
            int PriorTransaction = 0;
            TLADM_CustomerFile Customer = new TLADM_CustomerFile();

            if (oBtn != null && formloaded)
            {
                PO = (TLCSV_PurchaseOrder)cmboCurrentOrders.SelectedItem;
                if (PO == null)
                {
                    MessageBox.Show("Please select a purchase order from the drop down box");
                    return;
                }

                Customer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                if (Customer == null)
                {
                    MessageBox.Show("Please select a Customer from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    //--------------------------------------------------------------
                    // 1st Step get all the records selected in this session;
                    //-----------------------------------------------------------------------
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                    if (Dept != null)
                    {
                        PriorTransaction = TransNumber;

                        var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                        if (LNU != null)
                        {
                            TransNumber = LNU.col3;
                            LNU.col3 += 1;
                        }


                        var MasterRecords = context.TLCSV_PickingListMaster.Where(x => x.TLPL_IPAddress == Mach_IP).ToList();
                        if (MasterRecords.Count != 0)
                        {
                            TLCSV_OrderAllocated OrderAllocated = null;

                            OrderAllocated = new TLCSV_OrderAllocated();
                            OrderAllocated.TLORDA_POOrder_FK = PO.TLCSVPO_Pk;
                            OrderAllocated.TLORDA_TransNumber = TransNumber;
                            OrderAllocated.TLORDA_TransDate = DateTime.Now;
                            OrderAllocated.TLORDA_Customer_FK = Customer.Cust_Pk;
                            OrderAllocated.TLORDA_PickListPrint = true;
                            OrderAllocated.TLORDA_PLPrintDate = DateTime.Now;
                            OrderAllocated.TLORDA_BoxSelected = true;
                            if (chkStock.Checked)
                                OrderAllocated.TLORDA_PLStockOrder = true;

                            context.TLCSV_OrderAllocated.Add(OrderAllocated);

                            foreach (var MasterRecord in MasterRecords)
                            {
                                var BoxSelected = context.TLCSV_StockOnHand.Find(MasterRecord.TLPL_StockOnHand_FK);
                                if (BoxSelected != null)
                                {
                                    if (!PO.TLCSVPO_RepackTransaction)
                                    {
                                        var MDetail = context.TLCSV_MergePODetail.Where(x => x.TLMerge_StockOnHand_Fk == MasterRecord.TLPL_StockOnHand_FK && x.TLMerge_PoDetail_FK == MasterRecord.TLPL_PODetail_FK).FirstOrDefault();
                                        if (MDetail == null)
                                        {
                                            TLCSV_MergePODetail MergeDetail = new TLCSV_MergePODetail();
                                            MergeDetail.TLMerge_PoDetail_FK = MasterRecord.TLPL_PODetail_FK;
                                            MergeDetail.TLMerge_StockOnHand_Fk = MasterRecord.TLPL_StockOnHand_FK;
                                            context.TLCSV_MergePODetail.Add(MergeDetail);
                                        }
                                    }

                                    var PODetail = context.TLCSV_PuchaseOrderDetail.Find(MasterRecord.TLPL_PODetail_FK);
                                    if (PODetail != null)
                                    {
                                        PODetail.TLCUSTO_Picked = true;
                                        PODetail.TLCUSTO_PickedNumber = TransNumber;
                                        PODetail.TLCUSTO_PickedDate = DateTime.Now;
                                        PODetail.TLCUSTO_QtyPicked_ToDate += BoxSelected.TLSOH_BoxedQty;
                                    }

                                    BoxSelected.TLSOH_Picked = true;
                                    BoxSelected.TLSOH_PickListDate = DateTime.Now;
                                    BoxSelected.TLSOH_PickListNo = TransNumber;
                                    BoxSelected.TLSOH_POOrder_FK = PO.TLCSVPO_Pk;
                                    BoxSelected.TLSOH_POOrderDetail_FK = MasterRecord.TLPL_PODetail_FK;
                                    BoxSelected.TLSOH_Customer_Fk = PO.TLCSVPO_Customer_FK;
                                    BoxSelected.TLSOH_RePackConfig_Fk = MasterRecord.TLPL_BoxRePack_FK;
                          
                                }
                            }
                            if (PO.TLCSVPO_RepackTransaction)
                            {
                                var TransActions = context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrder_FK == OrderAllocated.TLORDA_POOrder_FK).ToList();
                                foreach (var TransAction in TransActions)
                                {
                                    var RePConfig = context.TLCSV_RePackConfig.Find(TransAction.REPACT_RePackConfig_FK);
                                    if (RePConfig != null)
                                    {
                                        RePConfig.PORConfig_SizeBoxQty_Picked += TransAction.REPACT_BoxedQty;
                                    }
                                }
                            }

                            try
                            {
                                context.SaveChanges();
                                MessageBox.Show("Data successfully saved to the database");

                                if (!PO.TLCSVPO_RepackTransaction)
                                {
                                    var svces = new CSVServices();
                                    svces.PLReprint = false;
                                    svces.POCustomer_PK = PO.TLCSVPO_Customer_FK;

                                    frmCSViewRep vRep = new frmCSViewRep(4, svces, TransNumber);
                                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                                    vRep.ClientSize = new Size(w, h);
                                    vRep.ShowDialog(this);
                                    if (vRep != null)
                                    {
                                        vRep.Close();
                                        vRep.Dispose();
                                    }
                                }
                                else
                                {
                                    var csvService = new CSVServices();
                                    var QueryParms = new CustomerServices.CustomerServicesParameters();
                                    var Repo = new CustomerServices.Repository();

                                    var RePackConfig = context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == OrderAllocated.TLORDA_POOrder_FK).FirstOrDefault();
                                    if (RePackConfig != null)
                                    {
                                        QueryParms.RePackConfigs.Add(Repo.LoadRePackConfig(RePackConfig.PORConfig_BoxNumber_Key));

                                        var vRep = new frmCSViewRep(23, QueryParms, csvService);
                                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                                        vRep.ClientSize = new Size(w, h);
                                        vRep.ShowDialog(this);
                                        if (vRep != null)
                                        {
                                            vRep.Close();
                                            vRep.Dispose();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView2.Rows.Clear();
                                formloaded = false;

                                cmboCurrentOrders.SelectedValue = -1;
                                cmboWareHouses.SelectedValue = -1;

                                this.frmPickList_Load(sender, null);

                                if (!cmboCustomers.Enabled)
                                    cmboCustomers.Enabled = true;

                                formloaded = true;

                            }
                        }
                    }
                }
            }
        }
        
        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            var ComboSource = new BindingList<KeyValuePair<int, string>>();

            if (oCmbo != null && formloaded)
            {
                var Selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if(Selected != null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmboCurrentOrders.DataSource = null;
                        if(Selected.Cust_RePack)
                            cmboCurrentOrders.DataSource = context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed && x.TLCSVPO_Customer_FK == Selected.Cust_Pk && x.TLCSVPO_RepackTransaction && !x.TLCSVPO_Provisional).OrderBy(x=>x.TLCSVPO_PurchaseOrder).ToList();
                        else
                            cmboCurrentOrders.DataSource = context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Provisional && !x.TLCSVPO_Closeed && x.TLCSVPO_Customer_FK == Selected.Cust_Pk).OrderBy(x => x.TLCSVPO_PurchaseOrder).ToList();
                        cmboCurrentOrders.ValueMember = "TLCSVPO_Pk";
                        cmboCurrentOrders.DisplayMember = "TLCSVPO_PurchaseOrder";
                        cmboCurrentOrders.SelectedValue = -1;
               
                        this.cmboCustomers.Enabled = false;
                        formloaded = true;
                    }
                }
                
            }
        }

        private void btnViewPickList_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && formloaded)
            {
                using (frmViewPickList vReportSelection = new frmViewPickList(Mach_IP))
                {
                    DialogResult dr = vReportSelection.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                       
                    }
                }
            }
        }

        private void frmPickList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //-------------------------------------------------------
            //
            //-----------------------------------------------------------
            Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                       .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                       .ToString();

            //------------------------------------------------------------------------------------
            // First we must ensure that any previous records that may exist and warn the user
            //---------------------------------------------------------------------------
            using (var context = new TTI2Entities())
            {
                var PriorRecords = context.TLCSV_PickingListMaster.Where(x => x.TLPL_IPAddress == Mach_IP).Count();
                if (PriorRecords != 0)
                {
                    MessageBox.Show("There appears to be existing records available. Please save current session first");
                    e.Cancel = true;
                }
            }
        }
    }
}
