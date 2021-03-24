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
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace DyeHouse
{
    public partial class frmDyeBatch : Form
    {
        Util core;

        object[] ColumnHeadings;

        List<DATA> PieceSelected = new List<DATA>();
        //===========================================================
        //---------Define the datatable 
        //=================================================================
        
        System.Data.DataTable DODataTable = null;
        System.Data.DataTable ProdDataTable = null;
        DataColumn column = null;

                   


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
        DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn(); // index           0   
        DataGridViewCheckBoxColumn oChkB = new DataGridViewCheckBoxColumn();   // Check box       1      
        DataGridViewTextBoxColumn oTxtAA = new DataGridViewTextBoxColumn();    // description     2                     
        DataGridViewTextBoxColumn oTxtAB = new DataGridViewTextBoxColumn();    // quality         3                     
        DataGridViewTextBoxColumn oTxtAC = new DataGridViewTextBoxColumn();    // ordered         4                     
        DataGridViewTextBoxColumn oTxtAD = new DataGridViewTextBoxColumn();    // batched         5                      
        DataGridViewTextBoxColumn oTxtAE = new DataGridViewTextBoxColumn();    // selected        6
        DataGridViewTextBoxColumn oTxtAF = new DataGridViewTextBoxColumn();    // Outstanding     7
        DataGridViewTextBoxColumn oTxtAG = new DataGridViewTextBoxColumn();    // Greige Quality FK  8
        DataGridViewCheckBoxColumn oTxtAH = new DataGridViewCheckBoxColumn();  // Used as a boolean 1 = Body and 0 Trim 9
        DataGridViewCheckBoxColumn oTxtAJ = new DataGridViewCheckBoxColumn();  // Trim Position FK  10
        DataGridViewCheckBoxColumn oTxtAK = new DataGridViewCheckBoxColumn();  // Trim Position FK  11
        
     

        bool formLoaded;
        bool PrevDyeBatch;
        int PrevDyeBatch_Fk;

        public frmDyeBatch()
        {
            
            InitializeComponent();

            DODataTable = new DataTable();
            ProdDataTable = new DataTable();
           
            column = new DataColumn();
            DataColumn[] keys = new DataColumn[1];
            
            //-----------------------------------------------------
            // This following code is for the top dataGridView
            // Create column 0 this is the key column
            //------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "RowNo";
            column.Caption = "RowNo";
            column.DefaultValue = 0;
            DODataTable.Columns.Add(column);
           
            keys[0] = DODataTable.Columns[0];
            DODataTable.PrimaryKey = keys;
           

            // Create column 1. This is Primary Key hidden from user sight 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "TLDYOD_PK";
            column.Caption = "Primary Key";
            column.DefaultValue = 0;
            DODataTable.Columns.Add(column);
            
            //-----------------------------------------------------------
            // Create column 2. User selectable
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select";
            column.Caption = "Select";
            column.DefaultValue = false;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 3. Description
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Desc";
            column.Caption = "Description";
            column.DefaultValue = string.Empty;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 4. Quality Description
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Qual_Desc";
            column.Caption = "Quality Description";
            column.DefaultValue = string.Empty;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 5. Dye Order Total Kgs
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "OrderKgs";
            column.Caption = "Dye Order Kgs";
            column.DefaultValue = 0.00M;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 6. Previously Dye Batched in Kgs
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "BatchKgs";
            column.Caption = "Already Batched";
            column.DefaultValue = 0.00M;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 7. Batched this session in Kgs
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "SessionKgs";
            column.Caption = "Batched This Session";
            column.DefaultValue = 0.00M;
            DODataTable.Columns.Add(column);

            //----------------------------------------------------------
            // Create Column 8 : Balance outstanding
            //-----------------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "OutStandingKgs";
            column.Caption = "Outstanding Kgs";
            column.DefaultValue = 0.00M;
            DODataTable.Columns.Add(column);

            //--------------------------------------------------------------------
            // Create column 9. This is Greige Key hidden from user sight 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "TLDYOD_Greige_Fk";
            column.Caption = "Greige Key";
            column.DefaultValue = 0;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 10 Body Yes Trim No 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Body";
            column.Caption = "Body";
            column.DefaultValue = false;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 11. If a trim then the trim Key value  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "TrimKey";
            column.Caption = "TrimKey";
            column.DefaultValue = 0;
            DODataTable.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 12. The Marker Rating Key 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "MarkerRating_Fk";
            column.Caption = "Marker Rating";
            column.DefaultValue = 0;
            DODataTable.Columns.Add(column);

           
            dataGridView2.DataSource = DODataTable;

            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[12].Visible = false;
        

            dataGridView2.AllowUserToAddRows = false;

            var idx = 0;
            foreach (DataColumn col in DODataTable.Columns)
            {
                if (++idx > 1)
                    dataGridView2.Columns[col.ColumnName].HeaderText = col.Caption;
            }


            //-----------------------------------------------------
            // This following code is for the bottom dataGridView
            //------------------------------------------------------
            // Create column 0. This is Primary Key hidden from user sight 
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "DYEB_PK";
            column.Caption = "Dye Batch Primary Key";
            column.DefaultValue = 0;
            ProdDataTable.Columns.Add(column);

            // Create column 1.
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Greige_PK";
            column.Caption = "Greige Primary Key";
            column.DefaultValue = 0;
            ProdDataTable.Columns.Add(column);

            // Create column 2.
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select";
            column.Caption = "Select";
            column.DefaultValue = false;
            ProdDataTable.Columns.Add(column);
            
            // Create column 3.
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "PieceNo";
            column.Caption = "Piece No";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create column 4.
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Gross";
            column.Caption = "Gross";
            column.DefaultValue = 0.00M;
            ProdDataTable.Columns.Add(column);

            // Create column 5.
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Quality";
            column.Caption = "Quality";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create column 6.
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Tex";
            column.Caption = "Tex";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create column 7.
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Pallet_No";
            column.Caption = "Pallet No";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create column 8.
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Knit_Order";
            column.Caption = "Knit Order";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create Column 9
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Colour";
            column.Caption = "Colour";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            // Create Column 10
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Grade";
            column.Caption = "Grade";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);

            //Create Col 11
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Remarks";
            column.Caption = "Remarks";
            column.DefaultValue = String.Empty;
            ProdDataTable.Columns.Add(column);


            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Depts != null)
                {
                    var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id && x.QD_Measurable).OrderBy(x=>x.QD_ShortCode) .ToList();
                    // Col 12 , 13, 14 
                    foreach (var Reason in Reasons)
                    {
                        column = new DataColumn();
                        column.DataType = typeof(Int32);
                        column.ColumnName = Reason.QD_ShortCode;
                        column.Caption = Reason.QD_Description;
                        column.DefaultValue = 0;
                        ProdDataTable.Columns.Add(column);
                    }
                }
            }

            //Create col 15;
            //=======================================
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "GreigeQual_Pk";
            column.Caption = "Description";
            column.DefaultValue = 0;
            ProdDataTable.Columns.Add(column);

          
            dataGridView1.DataSource = ProdDataTable;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[-1 + dataGridView1.Columns.Count].Visible = false;
            dataGridView1.AllowUserToAddRows = false;

            idx = 0;
            foreach (DataColumn col in ProdDataTable.Columns)
            {
                dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
            }

            core = new Util();


            SetUp(true);
        }

        void SetUp(bool SetUp)
        {
            formLoaded = false;
            PrevDyeBatch = false;
            radioButton1.Checked = true;

            


            txtNoOfPieces.Text = "0";
            txtNoOfPieces.ReadOnly = true;
            txtNoOfPieces.TextAlign = HorizontalAlignment.Center;

            txtColour.Visible = true;

            using (var context = new TTI2Entities())
            {
               
                cmboDyeOrders.DataSource = context.TLDYE_DyeOrder.Where(x => !x.TLDYO_Closed).OrderBy(x => x.TLDYO_DyeOrderNum).ToList();
                cmboDyeOrders.ValueMember = "TLDYO_Pk";
                cmboDyeOrders.DisplayMember = "TLDYO_DyeOrderNum";
                cmboDyeOrders.SelectedValue = 0;

                if (radioButton1.Checked)
                {
                    cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed && !x.DYEB_FabricMode && !x.DYEB_CommissinCust).OrderBy(X => X.DYEB_BatchNo).ToList();
                    cmboBatches.ValueMember = "DYEB_Pk";
                    cmboBatches.DisplayMember = "DYEB_BatchNo";
                    cmboBatches.SelectedValue = 0;
                }
                
                cmboColours.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColours.DisplayMember = "Col_Display";
                cmboColours.ValueMember = "Col_Id";
                cmboColours.SelectedValue = 0;
                cmboColours.Enabled = false;
                cmboColours.Visible = false;

                DODataTable.Rows.Clear();
                ProdDataTable.Rows.Clear();

                txtBatchKg.Text = "0.00";
                txtColour.Text = string.Empty;
                txtCustomer.Text = string.Empty;
                txtCustomerOrder.Text = string.Empty;
                txtOrderedDate.Text = string.Empty;
                //txtRequiredDate.Text = string.Empty;
                
                rbStandardMode.Checked  = true;
                cmboDyeOrders.Enabled = true;
                groupBox2.Enabled = true;

                chkLabReport.Checked = false;
                chkWrap.Checked = false;

                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtBatchNo.Text = "DB" + LNU.col3.ToString().PadLeft(6, '0');
                }

                 
            }

            formLoaded = true;

        }

        private void cmboBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                if (selected != null)
                {
                    txtBatchNo.Text = selected.DYEB_BatchNo;
                    PrevDyeBatch = true;
                    PrevDyeBatch_Fk = selected.DYEB_Pk;
 
                    using (var context = new TTI2Entities())
                    {
                        var DO = context.TLDYE_DyeOrder.Find(selected.DYEB_DyeOrder_FK);

                        if (DO != null && DO.TLDYO_Closed)
                        {
                            MessageBox.Show("Dye Order " + DO.TLDYO_DyeOrderNum + " has been closed. No further activity permitted");
                            return;
                        }
                        else if(DO == null)
                        {
                            MessageBox.Show("Dye Order not found");
                            return;

                        }

                        cmboDyeOrders.SelectedValue = DO.TLDYO_Pk;

                        txtOrderedDate.Text = DO.TLDYO_OrderDate.ToString("dd/MM/yyyy");
                        DateTime dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CMTReqWeek);
                        dt = dt.AddDays(5);
                        dtpRequiredDate.Value = dt; // txtRequiredDate.Text = dt.ToString("dd/MM/yyyy");
                        txtCustomerOrder.Text = DO.TLDYO_OrderNum;
                        rcbNotes.Text = DO.TLDYO_Notes;

                        var customer = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK);
                            if (customer != null)
                                txtCustomer.Text = customer.Cust_Description;

                            if (rbStandardMode.Checked)
                            {
                                var colour = context.TLADM_Colours.Find(DO.TLDYO_Colour_FK);
                                if (colour != null)
                                    txtColour.Text = colour.Col_Display;
                            }
                            else if (rbReprocessMode.Checked)
                            {
                                var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                                if (DB != null)
                                {
                                    var Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK);
                                    if (Colour != null)
                                        txtColour.Text = Colour.Col_Display;
                                }
                            }
                    }
                    
                 
                    DODataTable.Rows.Clear();
                    ProdDataTable.Rows.Clear();

                    UpdateDyeOrderDetails(selected.DYEB_DyeOrder_FK);
                  
                }
            }
        }

        private void cmboDyeOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
              
            if (oCmbo != null && formLoaded)
            {
                DODataTable.Rows.Clear();
                ProdDataTable.Rows.Clear();
                if (radioButton1.Checked)
                {
                    var selected = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;
                    if (selected != null)
                    {
                        txtOrderedDate.Text = selected.TLDYO_OrderDate.ToString("dd/MM/yyyy");
                        DateTime dt = core.FirstDateOfWeek(selected.TLDYO_OrderDate.Year, selected.TLDYO_CMTReqWeek);
                        dt = dt.AddDays(5);
                        dtpRequiredDate.Value = dt; // txtRequiredDate.Text = dt.ToString("dd/MM/yyyy");
                        txtCustomerOrder.Text = selected.TLDYO_OrderNum;
                        rcbNotes.Text = selected.TLDYO_Notes;
                        //----------------------------------------------------------------
                        //
                        //--------------------------------------------------------------------
                        using (var context = new TTI2Entities())
                        {
                            var customer = context.TLADM_CustomerFile.Find(selected.TLDYO_Customer_FK);
                            if (customer != null)
                                txtCustomer.Text = customer.Cust_Description;

                            if (rbStandardMode.Checked)
                            {
                                var colour = context.TLADM_Colours.Find(selected.TLDYO_Colour_FK);
                                if (colour != null)
                                    txtColour.Text = colour.Col_Display;
                            }
                            else if (rbReprocessMode.Checked)
                            {
                                var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                                if (DB != null)
                                {
                                    var Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK);
                                    if (Colour != null)
                                        txtColour.Text = Colour.Col_Display;
                                }
                            }
                            
                            UpdateDyeOrderDetails(selected.TLDYO_Pk);

                            this.dataGridView2.Refresh();
 
                            
                 
                        }
                    }
                }
            }
        }



        private void UpdateDyeOrderDetails(object o)
        {
            int DyeOrder_Pk = (int)o;

             decimal Tot;
            // Row No             0   
            // Dye Order Pk       1
            // Check box          2      
            // description        3                     
            // quality            4                     
            // ordered (kgs)      5                     
            // batched (Kgs)      6                      
            // selected (Kgs)     7
            // Outstanding (Kgs)  8
            // Greige Quality FK  9
            // Used as a boolean 1 = Body and 0 Trim 10
            // Trim Position FK  11
            // MarkerRating  FK  12
       
            using (var context = new TTI2Entities())
            {
                var ExistingDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder_Pk).AsParallel().ToList();
                var cnt = -1;
                foreach (var row in ExistingDetails)
                {
                    Tot = 0.00M;

                    DataRow Row = DODataTable.NewRow();
                    Row[0] = ++cnt;
                    Row[1] = row.TLDYOD_Pk;
                    Row[2] = false;

                    if (row.TLDYOD_BodyOrTrim)
                    {
                        Row[3] = "Body";
                    }
                    else
                    {
                        Row[3] = "Trims";
                    }
                    if(row.TLDYOD_BodyOrTrim)
                    {
                        var qual = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK);
                        if (qual != null)
                            Row[4] = qual.TLGreige_Description;
                    }
                    else
                    {
                         var qual = context.TLADM_Trims.Find(row.TLDYOD_Trims_FK);
                        if (qual != null)
                            Row[4] = qual.TR_Description;
                    }

                     Row[5] = Math.Round((decimal)row.TLDYOD_Kgs,2);

                    if(row.TLDYOD_BodyOrTrim)
                    {
                        var details = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeOrderDet_FK == DyeOrder_Pk && x.DYEBD_BodyTrim).ToList();
                         if (details != null)
                            Tot = details.Sum(x => x.DYEBD_GreigeProduction_Weight);

                        Row[6] = Math.Round(Tot,2);
                        Row[7] = 0.00M;
                        Row[8]  = Math.Round((decimal)row.TLDYOD_Kgs - ((decimal)Row[7] + (decimal)Row[6]), 2);
                        Row[9]  = row.TLDYOD_Greige_FK; 
                        Row[10]  = true;                 //  Body
                        Row[11] = 0;                     //  Therefore no Trim Key 
                        Row[12] = row.TLDYOD_MarkerRating_FK;
                    }
                    else
                    {
                         var details = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeOrderDet_FK == DyeOrder_Pk && x.DYEBO_TrimKey == row.TLDYOD_Trims_FK).ToList();
                        if (details != null)
                            Tot = details.Sum(x => x.DYEBD_GreigeProduction_Weight);
                        
                        Row[6] = Math.Round(Tot,2);
                        Row[7] = 0.00M;
                        Row[8] = Math.Round((decimal)row.TLDYOD_Kgs - ((decimal)Row[7] + (decimal)Row[6]), 2);
                        Row[9] = row.TLDYOD_Greige_FK;
                        Row[10] = false; //  Body
                        Row[11] = row.TLDYOD_Trims_FK;//  Trims
                        Row[12] = row.TLDYOD_MarkerRating_FK;  //  The fabric rating from the Dye Order
                    }
                    DODataTable.Rows.Add(Row);
                }
              
            }
        }

        private void UpdateKnitProduction(int RowN, bool PDyeBatch, int Greige_P, int Colour_P, int Size_P)
        {

            IList<TLKNI_GreigeProduction> Production = null;
          
            // Greige Type Primary Key
            int Greige_Pk = Greige_P;
            // The Colour Primary Key in TLADM_Colours
            int Colour_Pk = Colour_P;
            // The Size Primary key
            int Size_Pk = Size_P;  


            //-----------------------------------------------------
            // This following code is for the bottom dataGridView
            //------------------------------------------------------
            // Create column 0. This is Primary Key hidden from user sight 

           //  0 column.ColumnName = "DYEB_PK";
           //  1 column.ColumnName = "Greige_PK";
            // 2 column.ColumnName = "Select";
            // 3 column.ColumnName = "PieceNo";
            // 4 column.Caption =    "Production Weight";
            // 5 column.ColumnName = "Gross";
            // 6 column.ColumnName = "Quality";
            // 7 column.ColumnName = "Tex";
            // 8 column.ColumnName = "Pallet_No";
            // 9 column.ColumnName = "Knit_Order";
            // 10 column.ColumnName = "Colour";
            // 11 column.ColumnName = "Grade";
            // 12 column.ColumnName = "Remarks";
            // 13 Measure One
            // 14 Measure Two 
            // 15 Measure Three
            // 16 column.ColumnName = "GreigeQual_Pk";

            IList<TLADM_QualityDefinition> QualityDefinitions = null;

           
            using ( var context = new TTI2Entities())
            {
                 var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                 if (Depts != null)
                 {
                     QualityDefinitions = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x=>x.QD_ShortCode).ToList();
                 }
                if (rbStandardMode.Checked)
                {
                   Production = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Greige_Fk == Greige_Pk 
                                                                      && !x.GreigeP_Dye && x.GreigeP_Captured
                                                                      && !x.GreigeP_CommisionCust && x.GreigeP_Inspected).ToList();
                   if (Colour_Pk > 0 && Size_Pk > 0)
                   {
                        Production = Production.Where(x=>x.GreigeP_Size_Fk == Size_Pk && x.GreigeP_BIFColour_FK == Colour_Pk).ToList();
                   }
                   else if(Colour_Pk > 0)
                   {
                       Production = Production.Where(x => x.GreigeP_BIFColour_FK == Colour_Pk).ToList();
                   }

                   if (PrevDyeBatch)
                   {
                       var Prev = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_DyeBatch_FK == PrevDyeBatch_Fk).ToList();
                       Production = Production.Concat(Prev).OrderBy(x => x.GreigeP_PieceNo).ToList();
                   }

                   if (radbGradeA.Checked)
                   {
                            Production = Production.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).ToList();
                   }
                   else if (radbGradeB.Checked)
                   {
                            Production = Production.Where(x => x.GreigeP_Grade == "B").ToList();
                   }
                   else if (radbBoth.Checked)
                   {
                            Production = Production.Where(x => x.GreigeP_Grade == "A" || x.GreigeP_Grade == "B").ToList();
                   }
                   else if (radWithWarning.Checked)
                   {
                       Production = Production.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).ToList();
                   }
                }

                if (Production.Count == 0)
                {
                    MessageBox.Show("there are no records available pertaining to selection made");
                    return;
                }


                 var SingleRow = dataGridView2.CurrentRow;

                foreach (var prow in Production)
                {
                    DataRow Row = ProdDataTable.NewRow();

                    Row[0] = 0;
                    Row[1] = prow.GreigeP_Pk;
                    if (prow.GreigeP_Dye)
                    {
                        PieceSelected.Add(new DATA((int)SingleRow.Index, prow.GreigeP_Pk, prow.GreigeP_Dye));
                    }
                    var Record = PieceSelected.Find(x => x.DGV1_Greige_Pk  == prow.GreigeP_Pk);
                    var RecordIndex = PieceSelected.IndexOf(Record);
                    if (RecordIndex != -1)
                    {
                        if (Record.DGV1_Row != RowN)
                            continue;

                        Row[2] = true;
                    }
                    else
                    {
                        Row[2] = false;
                    }
                    Row[3] = prow.GreigeP_PieceNo;
                    Row[4] = Math.Round(prow.GreigeP_weight, 2);

                    var Greige = context.TLADM_Griege.Find(prow.GreigeP_Greige_Fk);

                    if (Greige != null)
                        Row[5] = Greige.TLGreige_Description;

                    Row[6] = Math.Round(prow.GreigeP_YarnTex, 1).ToString();
                    Row[7] = prow.GreigeP_PalletNo;

                    var KnitOrder = context.TLKNI_Order.Find(prow.GreigeP_KnitO_Fk);
                    if (KnitOrder != null)
                    {
                        Row[8] = "KO" + KnitOrder.KnitO_OrderNumber.ToString().PadLeft(6, '0');
                    }

                    if (!prow.GreigeP_BoughtIn)
                        Row[9] = prow.GreigeP_MergeDetail;
                    else
                    {
                        if (prow.GreigeP_BIFColour_FK != 0)
                            Row[9] = context.TLADM_Colours.Find(prow.GreigeP_BIFColour_FK).Col_Display;
                        else
                            Row[9] = "Greige";
                    }
                    Row[10] = prow.GreigeP_Grade;
                    if (prow.GreigeP_Remarks != null)
                        Row[11] = prow.GreigeP_Remarks;
                    else
                        Row[11] = string.Empty;

                   
                    var Reasons = QualityDefinitions.Where(x => x.QD_Measurable).OrderBy(x => x.QD_ShortCode).ToList();
                    //===============================================
                    int[] ColPos = new int[] { 12, 13, 14};
                    //=========================================================
                    // record.GreigeP_Meas1 = (int)row.Cells[4].Value;
                    // record.GreigeP_Meas2 = (int)row.Cells[5].Value;
                    // record.GreigeP_Meas3 = (int)row.Cells[6].Value;
                    // record.GreigeP_Meas4 = (int)row.Cells[7].Value;
                    // record.GreigeP_Meas5 = (int)row.Cells[8].Value;
                    // record.GreigeP_Meas6 = (int)row.Cells[9].Value;
                    // record.GreigeP_Meas7 = (int)row.Cells[10].Value;
                    // record.GreigeP_Meas8 = (int)row.Cells[11].Value;
                    //=========================================================
                    // column.ColumnName = Reason.QD_ShortCode;
                    // column.Caption = Reason.QD_Description;
                    // System.Data.DataTable ProdDataTable = null;\
                         var cnt = 0;
                         do
                         {

                             foreach (var Reason in Reasons)
                             {
                                 var Index = ProdDataTable.Columns.IndexOf(Reason.QD_ShortCode);
                                 if (Index >= 0)
                                 {
                                     var MeasureName = QualityDefinitions.Where(x => x.QD_ShortCode == Reason.QD_ShortCode).FirstOrDefault();

                                     if (MeasureName != null)
                                     {
                                         int ColumnIndex = MeasureName.QD_ColumnIndex;

                                         if (ColumnIndex == 1)
                                         {
                                             Row[Index] = prow.GreigeP_Meas1;
                                         }
                                         else if (ColumnIndex == 2)
                                         {
                                             Row[Index] = prow.GreigeP_Meas2;
                                         }
                                         else if (ColumnIndex == 3)
                                         {
                                             Row[Index] = prow.GreigeP_Meas3;
                                         }
                                         else if (ColumnIndex == 4)
                                         {
                                             Row[Index] = prow.GreigeP_Meas4;
                                         }
                                         else if (ColumnIndex == 5)
                                         {
                                             Row[Index] = prow.GreigeP_Meas5;
                                         }
                                         else if (ColumnIndex == 6)
                                         {
                                             Row[Index] = prow.GreigeP_Meas6;
                                         }
                                         else if (ColumnIndex == 7)
                                         {
                                             Row[Index] = prow.GreigeP_Meas7;
                                         }
                                         else
                                         {
                                             Row[Index] = prow.GreigeP_Meas8;
                                         }

                                     }
                                 }
                             }
                        } while (cnt++ < Reasons.Count);
                         Row[15] = prow.GreigeP_Greige_Fk; 


                        ProdDataTable.Rows.Add(Row);
               }
            }
        }
        private void radbGradeB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                   cmboDyeOrders_SelectedIndexChanged(cmboDyeOrders, null);
                }
            }
        }

        private void radbGradeA_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                    cmboDyeOrders_SelectedIndexChanged(cmboDyeOrders, null);
                }
            }
        }

        private void radbBoth_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked && formLoaded)
                {
                   cmboDyeOrders_SelectedIndexChanged(cmboDyeOrders, null);
                }
            }
        }

        private void radWhiteOnly_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                    cmboDyeOrders_SelectedIndexChanged(cmboDyeOrders, null);
                }
            }
        }

        private void radBAll_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                    cmboDyeOrders_SelectedIndexChanged(cmboDyeOrders, null);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView oDgv = sender as DataGridView;
            Thread.Sleep(10);
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var CurrentRow = oDgv.CurrentRow;

                foreach (DataGridViewRow Row in oDgv.Rows)
                {
                    if (Row.Index == CurrentRow.Index)
                        continue;

                    oDgv.Rows[Row.Index].Cells[2].Value = false;
                }

                ProdDataTable.Rows.Clear();

                var DYOD_Pk = (int)CurrentRow.Cells[1].Value;

                using (var context = new TTI2Entities())
                {
                    var tst = (from T1 in context.TLDYE_DyeOrder
                              join T2 in context.TLDYE_DyeOrderDetails on T1.TLDYO_Pk equals T2.TLDYOD_DyeOrder_Fk
                              where T2.TLDYOD_Pk == DYOD_Pk
                              select T1).FirstOrDefault().TLDYO_Pk;

                    var Dye_Order = context.TLDYE_DyeOrder.Find(tst);
                    if (Dye_Order != null)
                    {
                        var Style = context.TLADM_Styles.Find(Dye_Order.TLDYO_Style_FK);
                        if (Style != null)
                        {
                            if (Style.Sty_BoughtIn)
                            {
                                var Size = 0;

                                var TrimKey = (int)CurrentRow.Cells[11].Value;
                                if (TrimKey > 0)
                                {
                                    var Trim = context.TLADM_Trims.Find(TrimKey);
                                    if (Trim != null && Trim.TR_IsSizes)
                                        Size = (int)Trim.TR_Size_FK;
                                }

                                UpdateKnitProduction((int)CurrentRow.Cells[0].Value, PrevDyeBatch, (int)CurrentRow.Cells[9].Value, Dye_Order.TLDYO_Colour_FK, Size);
                             }
                             else
                             {
                                UpdateKnitProduction((int)CurrentRow.Cells[0].Value, PrevDyeBatch, (int)CurrentRow.Cells[9].Value, 0, 0);
                             }
                              
                        }
                    }
                }
                
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             int rowindex = 0;
             // index           0   
             // Check box       1      
             // description     2                     
             // quality         3                     
             // ordered         4                     
             // batched         5                      
             // selected        6
             // Outstanding     7
             // Greige Quality FK  8
             // Used as a boolean 1 = Body and 0 Trim 9
             // Trim Position FK  10
             // MarkerRating  FK  11
             //  Size Foreign Key  12
             //-----------------------------------------------------
             // This following code is for the bottom dataGridView
             //------------------------------------------------------
             // Create column 0. This is Primary Key hidden from user sight 
             //  0 column.ColumnName = "DYEB_PK";
             //  1 column.ColumnName = "Greige_PK";
            //--------------------------------------------------------------------------
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 var CurrentRow = oDgv.CurrentRow;
           
                 if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                 {
                     if (CurrentRow != null)
                     {
                         rowindex = dataGridView2.CurrentRow.Index;
                         
                         //-----------------------------------
                         // Put code here
                         //----------------------------------------------------
                         var SingleRow = (
                                       from Rows in dataGridView2.Rows.Cast<DataGridViewRow>()
                                       where (bool)Rows.Cells[2].Value == true
                                       select Rows).FirstOrDefault();

                         if (SingleRow != null)
                         {
                             PieceSelected.Add(new DATA(SingleRow.Index, (int)CurrentRow.Cells[1].Value,false));
                             DataRow Row = DODataTable.Rows.Find(SingleRow.Index);
                             if (Row != null)
                             {
                                 var Ordered = (decimal)Row[5];

                                 //Batched Previous session
                                 var BatchedPrev = Math.Round((decimal)Row[6], 2);
                               
                                 var Selected = (decimal)CurrentRow.Cells[4].Value;
                                 var OutStanding = (decimal)SingleRow.Cells[8].Value;
                                
                                 var BatchedCurrentSession = Math.Round((decimal)Row[7], 2);
                                 var TotalThisSession = BatchedCurrentSession + Selected;
                                 Row[7] =  Math.Round(TotalThisSession, 2);
                                 Row[8] = Math.Round(Ordered - BatchedPrev - TotalThisSession, 2);

                                 decimal curBal = Convert.ToDecimal(txtBatchKg.Text);
                                 curBal += Selected;
                                 txtBatchKg.Text = Math.Round(curBal, 2).ToString();

                                 var NoOfPieces = Int16.Parse(txtNoOfPieces.Text.ToString());
                                 txtNoOfPieces.Text = (1 + NoOfPieces).ToString(); 
                             }

                         }
                        
                    }
                 }
                 else
                 {
                     CurrentRow = oDgv.CurrentRow;
                     if (CurrentRow != null)
                     {
                         var SingleRow = (
                                     from Rows in dataGridView2.Rows.Cast<DataGridViewRow>()
                                     where (bool)Rows.Cells[2].Value == true
                                     select Rows).FirstOrDefault();

                         var Record = PieceSelected.Find(x => x.DGV1_Greige_Pk == (int)CurrentRow.Cells[1].Value);
                         var RecordIndex = PieceSelected.IndexOf(Record);
                         if (RecordIndex != -1)
                         {
                             if (Record.PrevDyeBatch)
                             {
                                 using (var context = new TTI2Entities())
                                 {
                                     var DB = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == Record.DGV1_Greige_Pk).FirstOrDefault();
                                     if (DB != null)
                                     {
                                         context.TLDYE_DyeBatchDetails.Remove(DB);

                                         var GP = context.TLKNI_GreigeProduction.Find(Record.DGV1_Greige_Pk);
                                         if (GP != null)
                                         {
                                             GP.GreigeP_Dye = false;
                                             GP.GreigeP_DyeBatch_FK = 0;


                                             var Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                                                          .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                                                          .ToString();

                                             TLADM_DailyLog Log = new TLADM_DailyLog();
                                             Log.TLDL_IPAddress = Mach_IP;
                                             Log.TLDL_Date = DateTime.Now;
                                             Log.TLDL_Comments = "Piece Removed from DB " + DB.DYEBD_DyeBatch_FK.ToString();
                                             Log.TLDL_Dept_Fk = 12;
                                             Log.TLDL_TransDetail = "Piece Number " + GP.GreigeP_PieceNo;

                                             context.TLADM_DailyLog.Add(Log);

                                             context.SaveChanges();


                                         }
                                     }
                                 }
                             }


                             PieceSelected.RemoveAt(RecordIndex);

                             var NoOfPieces = Int16.Parse(txtNoOfPieces.Text.ToString());
                             if (NoOfPieces > 0)
                                txtNoOfPieces.Text = (-1 + NoOfPieces).ToString();

                             DataRow Row = DODataTable.Rows.Find(SingleRow.Index);

                             var Selected = (decimal)CurrentRow.Cells[4].Value;
                             Row[7] = Math.Round((decimal)Row[7] - Selected, 2);
                             Row[8] = Math.Round((decimal)Row[8] + Selected, 2);

                             decimal curBal = Convert.ToDecimal(txtBatchKg.Text);
                             curBal -= Selected;
                             txtBatchKg.Text = Math.Round(curBal, 2).ToString();
                        }
                      
                     }
                      
                 }
             }
        }

        private struct DATA
        {
            public int DGV1_Row;        // The data grid row no 
            public bool PrevDyeBatch;   // The physical piece no
            public int DGV1_Greige_Pk; // the index no of the piece in TLKNI_GreigeProduction
 
            public DATA(int Row, int GreigePk, bool PDyeBatch)
            {
                DGV1_Row = Row;
                DGV1_Greige_Pk = GreigePk;
                PrevDyeBatch = PDyeBatch;
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

         
        private struct BackGroundColours
        {
            public int DbKey;                          // Merge Key
            public System.Drawing.Color BackGround;            // The Primary key from the TLSPN_CottonMerge Table
       
            public BackGroundColours(int _DBKey , System.Drawing.Color _clrs)
            {
                this.DbKey = _DBKey;
                this.BackGround = _clrs;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            int DyeBatchKey = 0;
            Button oBtn = sender as Button;
            bool BoughtIn = false;

            if (oBtn != null && formLoaded)
            {
                // First thing we have to create a header record
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
                                LNU.col3 += 1;
                            }
                        }
                                                
                        TLDYE_DyeBatch db = new TLDYE_DyeBatch();

                        db.DYEB_BatchNo = txtBatchNo.Text;
                        db.DYEB_BatchKG = Convert.ToDecimal(txtBatchKg.Text);
                        db.DYEB_BatchDate = DateTime.Now;
                        if (rbReprocessMode.Checked)
                        {
                            var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                            if (DB != null)
                            {
                                db.DYEB_SequenceNo = DB.DYEB_SequenceNo + 1;
                                db.DYEB_BatchNo = DB.DYEB_BatchNo + "R" + db.DYEB_SequenceNo.ToString().PadLeft(3, '0');
                                db.DYEB_OriginalBatch_FK = db.DYEB_Pk;
                            }
                        }
                        else
                             db.DYEB_SequenceNo = 0;

                        if (radioButton1.Checked)
                        {
                            if (rbStandardMode.Checked)
                                db.DYEB_Colour_FK = ((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Colour_FK;
                            else
                                db.DYEB_Colour_FK = ((TLADM_Colours)cmboColours.SelectedItem).Col_Id;
                        }
                        else
                        {
                            if (rbStandardMode.Checked)
                                db.DYEB_Colour_FK = ((TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem).TLDYEF_Colours_FK;
                            else
                                db.DYEB_Colour_FK = ((TLADM_Colours)cmboColours.SelectedItem).Col_Id;
                        }

                        try
                        {
                            if (radioButton1.Checked)
                            {
                                var selected = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;
                                if (selected != null)
                                {
                                    DateTime dt = core.FirstDateOfWeek(selected.TLDYO_OrderDate.Year, selected.TLDYO_DyeReqWeek);
                                    db.DYEB_RequiredDate = dt.AddDays(5);
                                    db.DYEB_Greige_FK = selected.TLDYO_Greige_FK;
                                }
                            }
                            else
                            {
                                var selected = (TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem;
                                if (selected != null)
                                {
                                    // DateTime dt = core.FirstDateOfWeek(selected.TLDYEF_OrderDate.Year, selected.TLDYEF_DyeWeek);
                                    db.DYEB_RequiredDate = dtpRequiredDate.Value; // dt.AddDays(5);
                                    db.DYEB_Greige_FK = selected.TLDYEF_Greige_FK;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        if (chkLabReport.Checked)
                            db.DYEB_Lab = true;
                        else
                            db.DYEB_Lab = false;

                        if (chkWrap.Checked)
                            db.DYEB_Wrap = true;
                        else
                            db.DYEB_Wrap = false;

                        if (radioButton2.Checked)
                            db.DYEB_FabricMode = true;

                        db.DYEB_DyeOrder_FK = (int)cmboDyeOrders.SelectedValue;
                        db.DYEB_Notes       = rcbNotes.Text;
                        if (radioButton1.Checked)
                        {
                            db.DYEB_Customer_FK = ((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Customer_FK;
                            db.DYEB_Greige_FK = ((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Greige_FK;
                            BoughtIn = context.TLADM_Styles.Find(((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Style_FK).Sty_BoughtIn;

                        }
                        else
                        {
                            db.DYEB_Customer_FK = ((TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem).TLDYEF_Customer_FK;
                            db.DYEB_Greige_FK = ((TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem).TLDYEF_Greige_FK;
                            if(!db.DYEB_FabricMode)
                                BoughtIn = context.TLADM_Styles.Find(((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Style_FK).Sty_BoughtIn;
                        }

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var Trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 100).FirstOrDefault();
                            if (Trantype != null)
                                db.DYEB_TransactionType_FK = Trantype.TrxT_Pk;
                        }

                        //Bought In Fabric needs to have special rules applied 
                        //does not need to go thru the normal processes
                        //================================================================================
                        if (BoughtIn)
                        {
                            db.DYEB_Allocated = true;
                            db.DYEB_OutProcess = true;
                            db.DYEB_OutProcessDate = DateTime.Now;
                            db.DYEB_Stage1 = true;
                            db.DYEB_Stage2 = true;
                            db.DYEB_Stage3 = true;
                            db.DYEB_TransferDate = DateTime.Now;
                            db.DYEB_Transfered = true;
                           
                        }
                        try
                        {
                            context.TLDYE_DyeBatch.Add(db);
                            context.SaveChanges();

                            DyeBatchKey = db.DYEB_Pk;
                            if (!PrevDyeBatch)
                            {
                                TLDYE_DyeTransactions dt = new TLDYE_DyeTransactions();
                                dt.TLDYET_BatchNo = txtBatchNo.Text;
                                dt.TLDYET_Date = DateTime.Now;
                                dt.TLDYET_SequenceNo = 0;
                                dt.TLDYET_TransactionType = db.DYEB_TransactionType_FK;
                                dt.TLDYET_BatchWeight = db.DYEB_BatchKG;
                                dt.TLDYET_Batch_FK = DyeBatchKey;
                                dt.TLDYET_TransactionNumber = txtBatchNo.Text;
                                if(radioButton1.Checked)
                                    dt.TLDYET_Customer_FK = ((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Customer_FK;
                                else
                                    dt.TLDYET_Customer_FK = ((TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem).TLDYEF_Customer_FK;
                                dt.TLDYET_CurrentStore_FK = (int)context.TLADM_TranactionType.Find(db.DYEB_TransactionType_FK).TrxT_ToWhse_FK;
                                dt.TLDYET_Stage = 1;
                                
                                context.TLDYE_DyeTransactions.Add(dt);

                            }
                            else if (rbReprocessMode.Checked)
                            {
                                TLDYE_DyeTransactions dt = new TLDYE_DyeTransactions();
                                dt.TLDYET_BatchNo = db.DYEB_BatchNo;
                                dt.TLDYET_Date = DateTime.Now;
                                dt.TLDYET_SequenceNo = db.DYEB_SequenceNo;
                                dt.TLDYET_TransactionType = db.DYEB_TransactionType_FK;
                                dt.TLDYET_BatchWeight = db.DYEB_BatchKG;
                                dt.TLDYET_Batch_FK = DyeBatchKey;
                                dt.TLDYET_TransactionNumber = txtBatchNo.Text;
                                if(radioButton1.Checked)
                                    dt.TLDYET_Customer_FK = ((TLDYE_DyeOrder)cmboDyeOrders.SelectedItem).TLDYO_Customer_FK;
                                else
                                    dt.TLDYET_Customer_FK = ((TLDYE_DyeOrderFabric)cmboDyeOrders.SelectedItem).TLDYEF_Customer_FK;
                                dt.TLDYET_CurrentStore_FK = (int)context.TLADM_TranactionType.Find(db.DYEB_TransactionType_FK).TrxT_ToWhse_FK;
                                dt.TLDYET_Stage = 1;
                                context.TLDYE_DyeTransactions.Add(dt);
                            }

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
                        if (db != null)
                        {
                            var ColorSelected = (TLADM_Colours)cmboColours.SelectedItem;
                            if (ColorSelected != null)
                            {
                                if (ColorSelected.Col_Id != db.DYEB_Colour_FK)
                                    db.DYEB_Colour_FK = ColorSelected.Col_Id;
                            }
                            db.DYEB_BatchKG = Convert.ToDecimal(txtBatchKg.Text);

                            if (chkLabReport.Checked)
                                db.DYEB_Lab = true;
                            else
                                db.DYEB_Lab = false;

                            if (chkWrap.Checked)
                                db.DYEB_Wrap = true;
                            else
                                db.DYEB_Wrap = false;
                        }
                    }
                    // now for each record in the PieceSelected struct
                    foreach (var Piece in PieceSelected)
                    {
                        if (Piece.PrevDyeBatch)
                            continue;

                        var data = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == Piece.DGV1_Greige_Pk).FirstOrDefault();
                        if (data == null)
                        {
                            TLDYE_DyeBatchDetails tbd = new TLDYE_DyeBatchDetails();
                            var DOOrderRow = DODataTable.Rows.Find(Piece.DGV1_Row);
                            if (DOOrderRow != null)
                            {
                                tbd.DYEBD_DyeBatch_FK = DyeBatchKey;
                                tbd.DYEBD_BodyTrim = (bool)DOOrderRow[10];
                                tbd.DYEBD_DyeOrderDet_FK = (int)cmboDyeOrders.SelectedValue;
                                tbd.DYEBD_GreigeProduction_FK = Piece.DGV1_Greige_Pk;
                                
                                var GP = context.TLKNI_GreigeProduction.Find(Piece.DGV1_Greige_Pk);
                                if (GP != null)
                                {
                                    GP.GreigeP_Dye = true;
                                    GP.GreigeP_DyeBatch_FK = DyeBatchKey;
                                    tbd.DYEBD_GreigeProduction_Weight = GP.GreigeP_weight;
                                }

                                tbd.DYEBD_QualityKey = (int)DOOrderRow[9];
                                tbd.DYEBO_TrimKey = (int)DOOrderRow[11];
                                tbd.DYEBO_GVRowNumber = (int)DOOrderRow[0];
                                tbd.DYEBO_ProductRating_FK = (int)DOOrderRow[12];

                                if (BoughtIn)
                                {
                                    //=====================================================
                                    //Bought In Fabric requires special rules
                                    //==================================================
                                    tbd.DYEBO_Meters = Math.Round(core.FabricYield(GP.GreigeP_BoughtIn_FabWeight, GP.GreigeP_BoughtIn_FabWidth) * GP.GreigeP_weight , 2);
                                    tbd.DYEBO_DiskWeight = GP.GreigeP_BoughtIn_FabWeight;
                                    tbd.DYEBO_Nett = GP.GreigeP_weight;
                                    tbd.DYEBO_Width = GP.GreigeP_BoughtIn_FabWidth;
                                    tbd.DYEBO_QAApproved = true;
                                    tbd.DYEBO_ApprovalDate = DateTime.Now;

                                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                                    if (Dept != null)
                                    {
                                           TLADM_TranactionType trantype = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 700).FirstOrDefault();
                                           if (trantype != null)
                                              tbd.DYEBO_CurrentStore_FK = (int)trantype.TrxT_ToWhse_FK;
                                    }
                                
                            

                                }
                                
                            }

                            context.TLDYE_DyeBatchDetails.Add(tbd);
                        }
                        else
                        {
                           
                            var GP = context.TLKNI_GreigeProduction.Find(Piece.DGV1_Greige_Pk);
                            if (GP != null)
                            {
                                GP.GreigeP_Dye = true;
                                GP.GreigeP_DyeBatch_FK = DyeBatchKey;
                            }

                            data.DYEBD_DyeBatch_FK = DyeBatchKey;
                           
                           
  
                        }
                    }
                    

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Records Saved to Database Successfully");
               
                        //------------------
                        // Do the printing
                        //-------------------------------
                        if (PrevDyeBatch)
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

                        DialogResult Result = MessageBox.Show("Would you like to print the transfer tickets", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (Result == DialogResult.Yes)
                        {
                            vRep = new frmDyeViewReport(5, DyeBatchKey);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                        }

                        var DyeBatch = context.TLDYE_DyeBatch.Find(DyeBatchKey);
                        if(DyeBatch != null)
                        {
                            var DyeStandards = context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == DyeBatch.DYEB_Greige_FK).FirstOrDefault();
                            if(DyeStandards == null)
                            {
                                MessageBox.Show("Unable to print Standards control sheet");

                            }
                            else
                            {
                                vRep = new frmDyeViewReport(46, DyeBatchKey);
                                h = Screen.PrimaryScreen.WorkingArea.Height;
                                w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                            }
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
            }
        }

      

        private void frmDyeBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //This Transaction might be a 
            using (var context = new TTI2Entities())
            {
                var DB = (TLDYE_DyeBatch)cmboBatches.SelectedItem;
                if (DB != null && rbStandardMode.Checked)
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

        private void rbStandardMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                    SetUp(false);
                }
            }
        }

        private void rbReprocessMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                if (oRad.Checked)
                {
                    cmboDyeOrders.Enabled = false;
                    groupBox2.Enabled = false;

                    using (var context = new TTI2Entities())
                    {
                        formLoaded = false;
                        cmboBatches.DataSource = null;
                        cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed && (bool)x.DYEB_Reprocess).OrderBy(X => X.DYEB_BatchNo).ToList();
                        cmboBatches.ValueMember = "DYEB_Pk";
                        cmboBatches.DisplayMember = "DYEB_BatchNo";
                        cmboBatches.SelectedValue = 0;

                        formLoaded = true;
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if(oRad != null && formLoaded)
            {
                using ( var context = new TTI2Entities())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();

                    txtBatchKg.Text = "0.00";

                    formLoaded = false;
                    cmboDyeOrders.DataSource = null;
                    cmboDyeOrders.DataSource = context.TLDYE_DyeOrderFabric.ToList();
                    cmboDyeOrders.ValueMember = "TLDYEF_Pk";
                    cmboDyeOrders.DisplayMember = "TLDYEF_DyeOrderNo";
                    cmboDyeOrders.SelectedValue = 0;

                    cmboBatches.DataSource = null;
                    cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed && x.DYEB_FabricMode).OrderBy(X => X.DYEB_BatchNo).ToList();
                    cmboBatches.ValueMember = "DYEB_Pk";
                    cmboBatches.DisplayMember = "DYEB_BatchNo";
                    cmboBatches.SelectedValue = 0;
                    formLoaded = true;
                }
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    DODataTable.Rows.Clear();
                    ProdDataTable.Rows.Clear();
                    
                    txtBatchKg.Text = "0.00";

                    formLoaded = false;
                    cmboDyeOrders.DataSource = null;
                    cmboDyeOrders.DataSource = context.TLDYE_DyeOrder.OrderBy(x=>x.TLDYO_OrderNum).ToList();
                    cmboDyeOrders.ValueMember = "TLDYO_Pk";
                    cmboDyeOrders.DisplayMember = "TLDYO_DyeOrderNum";
                    cmboDyeOrders.SelectedValue = 0;

                    cmboBatches.DataSource = null;
                    cmboBatches.DataSource = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed && !x.DYEB_FabricMode).OrderBy(X => X.DYEB_BatchNo).ToList();
                    cmboBatches.ValueMember = "DYEB_Pk";
                    cmboBatches.DisplayMember = "DYEB_BatchNo";
                    cmboBatches.SelectedValue = 0;
                    formLoaded = true;
                }
            }
        }

       

       
   }
}
