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
    public partial class frmCommissionDeliveries : Form
    {
        bool formloaded;
        DataTable dt;
        DataColumn Col;
        BindingSource BindSrc;

        bool CancelPending;
        bool SaleConfirmed;
        bool FabDespatched; 

        bool ManagementMode;

        DataGridViewTextBoxColumn Col0;  //Index in File 
        DataGridViewCheckBoxColumn Col1;  // Piece Selected
        DataGridViewTextBoxColumn Col2;   // Piece Number
        DataGridViewTextBoxColumn Col3;   // Colour 
        DataGridViewTextBoxColumn Col4;   // Meters 
        DataGridViewTextBoxColumn Col5;   // Gross 
        DataGridViewTextBoxColumn Col6;   // Nett;
                                          // 

        DyeHouse.DyeQueryParameters QueryParms;
        DyeHouse.DyeRepository repo;

        bool _Commission = false;

        public frmCommissionDeliveries( bool Com)
        {
            InitializeComponent();
            // Note for the file
            // If Com = True then this is a commission delivery 
            // else          then this is a fabric sales delivery   
            repo = new DyeRepository();
            if (Com)
                this.Text = "Commission Deliveries";
            else
                this.Text = "Fabric Sales Deliveries";

            _Commission = Com;

            if (!Com)
            {
                groupBox2.Visible = false;
                dataGridView1.Visible = true;

                dt = new DataTable();
                BindSrc = new BindingSource();

                Col = new DataColumn();

                //------------------------------------------------------
                // Create column 0. // This is Index Position of the measurement in the TLADM_QADyeProcessFields Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(Int32);
                Col.ColumnName = "DyeBatchDetail_Pk";
                Col.DefaultValue = 0;
                dt.Columns.Add(Col);
                //  DataT.PrimaryKey = new DataColumn[] { DataT.Columns[0] };

                //------------------------------------------------------
                // Create column 1. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(bool);
                Col.ColumnName = "RecordSelected";
                Col.Caption = "Select";
                Col.DefaultValue = false;
                dt.Columns.Add(Col);

                //------------------------------------------------------
                // Create column 2. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(string);
                Col.ColumnName = "PieceNo";
                Col.Caption = "Piece No";
                Col.DefaultValue = String.Empty;
                dt.Columns.Add(Col);

                //------------------------------------------------------
                // Create column 3. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(string);
                Col.ColumnName = "PieceColour";
                Col.Caption = "Colour";
                Col.DefaultValue = String.Empty;
                dt.Columns.Add(Col);

                //------------------------------------------------------
                // Create column 4. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(Decimal);
                Col.ColumnName = "PieceMeters";
                Col.Caption = "Meters";
                Col.DefaultValue = 0.00M;
                dt.Columns.Add(Col);

                //------------------------------------------------------
                // Create column 5. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(Decimal);
                Col.ColumnName = "PieceMeterGross";
                Col.Caption = "Gross";
                Col.DefaultValue = 0.00M;
                dt.Columns.Add(Col);

                //------------------------------------------------------
                // Create column 6. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                Col = new DataColumn();
                Col.DataType = typeof(Decimal);
                Col.ColumnName = "PieceNett";
                Col.Caption = "Nett";
                Col.DefaultValue = 0.00M;
                dt.Columns.Add(Col);

                //0 -- index of record 
                //--------------------------------------------
                Col0 = new DataGridViewTextBoxColumn();
                Col0.Name = "DyeBatchIndex";
                Col0.ValueType = typeof(Int32);
                Col0.DataPropertyName = dt.Columns[0].ColumnName;
                Col0.HeaderText = "DyeBatch Index";
                dataGridView1.Columns.Add(Col0);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].DisplayIndex = 0;

                //--------------------------------------------
                Col1 = new DataGridViewCheckBoxColumn();
                Col1.Name = "Select";
                Col1.ValueType = typeof(bool);
                Col1.HeaderText = "Select";
                Col1.DataPropertyName = dt.Columns[1].ColumnName;
                dataGridView1.Columns.Add(Col1);
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[1].DisplayIndex = 1;

                Col2 = new DataGridViewTextBoxColumn();
                Col2.Name = "PieceNo";
                Col2.ValueType = typeof(String);
                Col2.HeaderText = "Piece No";
                Col2.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(Col2);
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[2].DisplayIndex = 2;

                Col3 = new DataGridViewTextBoxColumn();
                Col3.Name = "ColourDesc";
                Col3.ValueType = typeof(String);
                Col3.HeaderText = "Colour";
                Col3.DataPropertyName = dt.Columns[3].ColumnName;
                dataGridView1.Columns.Add(Col3);
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[3].DisplayIndex = 3;

                Col4 = new DataGridViewTextBoxColumn();
                Col4.Name = "Meters";
                Col4.ValueType = typeof(decimal);
                Col4.HeaderText = "Meters";
                Col4.DataPropertyName = dt.Columns[4].ColumnName;
                dataGridView1.Columns.Add(Col4);
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[4].DisplayIndex = 4;

                Col5 = new DataGridViewTextBoxColumn();
                Col5.Name = "Gross";
                Col5.ValueType = typeof(decimal);
                Col5.HeaderText = "Gross";
                Col5.DataPropertyName = dt.Columns[5].ColumnName;
                dataGridView1.Columns.Add(Col5);
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[5].DisplayIndex = 5;

                Col6 = new DataGridViewTextBoxColumn();
                Col6.Name = "Nett";
                Col6.ValueType = typeof(decimal);
                Col6.HeaderText = "Nett";
                Col6.DataPropertyName = dt.Columns[6].ColumnName;
                dataGridView1.Columns.Add(Col6);
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[6].DisplayIndex = 6;


                dataGridView1.DataSource = dt;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;

                BindSrc.DataSource = dt;
                dataGridView1.DataSource = BindSrc;

            }
            else
            {
                dataGridView1.Visible = false;
            }

            this.cmboCurrentBatches.CheckStateChanged += new System.EventHandler(this.cmboCurrentBatches_CheckStateChanged);
            this.cmboBatchesReprint.CheckStateChanged += new System.EventHandler(this.cmboCurrentBatchesReprint_CheckStateChanged);
        }
        
               

        private void frmCommissionDeliveries_Load(object sender, EventArgs e)
        {
            formloaded = false;
            IList<TLADM_CustomerFile> Customers = null;

            cmboBatchesReprint.Visible = false;

            ManagementMode = false;

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    if (_Commission)
                    {
                        TxtTransNumber.Text = "CD" + LNU.col9.ToString().PadLeft(6, '0');
                        Customers = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust).ToList();
                    }
                    else
                    {
                        TxtTransNumber.Text = "FS" + LNU.col10.ToString().PadLeft(6, '0');
                        Customers = context.TLADM_CustomerFile.Where(x => !x.Cust_CommissionCust).ToList();
                    }
                }


                cmboCustomer.DataSource = Customers;
                cmboCustomer.ValueMember = "Cust_Pk";
                cmboCustomer.DisplayMember = "Cust_Description";
                cmboCustomer.SelectedValue = -1;

                QueryParms = new DyeQueryParameters();

                cmboCurrentBatches.Items.Clear();
                cmboBatchesReprint.Items.Clear();


            }

          
            formloaded = true;

            
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCurrentBatches_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                
                if (item.CheckState)
                {
                    if (_Commission)
                    {
                        //Remember...The Item_Pk is a number aimed at the DyeBatchsDetails DYEBD_DyeBatch_FK
                        QueryParms.DyeBatches.Add(repo.LoadDyeBatch(item._Pk));
                    }
                    else
                    {
                        QueryParms.DyeTransactions.Add(repo.LoadDyeTrans(item._Pk));
                        DialogResult Res = MessageBox.Show("Do you wish to modify this order?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Res == DialogResult.Yes)
                        {
                            DyeHouse.FrmFabricSaleInstr Instr = new DyeHouse.FrmFabricSaleInstr();
                            Instr.ShowDialog(this);
                            if (!Instr.SelectionMade)
                            {
                                this.Close();
                            }

                            CancelPending = Instr.rbPCanceled;
                            SaleConfirmed = Instr.rbPConfirmed;
                            FabDespatched = Instr.rbFDespatched;
                            
                            ManagementMode = true;

                        }

                           
                        using (var context = new TTI2Entities())
                        {
                           var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_TransactionNo == item.Text ).ToList();
                           if (Existing.Count > 0)
                           {
                                    dt.Rows.Clear();
                                    foreach (var Item in Existing)
                                    {
                                        DataRow NewRow = dt.NewRow();
                                        NewRow[0] = Item.DYEBD_Pk;
                                        if (CancelPending || SaleConfirmed || FabDespatched)
                                        {
                                          NewRow[1] = true;
                                        }
                                        else
                                        {
                                          NewRow[1] = false;
                                        }
                                    
                                        NewRow[2] = context.TLKNI_GreigeProduction.Find(Item.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                                        var DB = context.TLDYE_DyeBatch.Find(Item.DYEBD_DyeBatch_FK);
                                        if (DB != null)
                                        {
                                            NewRow[3] = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                        }
                                        NewRow[4] = Item.DYEBO_Meters;
                                        NewRow[5] = Item.DYEBD_GreigeProduction_Weight;
                                        NewRow[6] = Item.DYEBO_Nett;

                                        dt.Rows.Add(NewRow);
                                    }
                                }
                                else
                                {
                                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                    {
                                        MessageBox.Show("There are no records for Delivery Note selected ");
                                    }
                                }
                           }
                    }
                    
                }
                else
                {
                    if (_Commission)
                    {
                        var value = QueryParms.DyeBatches.Find(it => it.DYEB_Pk == item._Pk);
                        if (value != null)
                            QueryParms.DyeBatches.Remove(value);
                    }
                    else
                    {
                        var value = QueryParms.DyeTransactions.Find(it => it.TLDYET_Pk == item._Pk);
                        if (value != null)
                            QueryParms.DyeTransactions.Remove(value);
                    }
                }
                
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCurrentBatchesReprint_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem && formloaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;

                if (item.CheckState)
                {
                    try
                    {
                        //Remember...The Item_Pk is a number aimed at the DyeBatchsDetails DYEBD_DyeBatch_FK
                        QueryParms.DyeBatchDetails.Add(repo.LoadDyeBatchDetails(item._Pk));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    var value = QueryParms.DyeBatchDetails.Find(it => it.DYEBD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.DyeBatchDetails.Remove(value);
                }

            }
        }
        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                cmboCurrentBatches.Items.Clear();

                using (var context = new TTI2Entities())
                {
                    var Selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                    if (Selected != null)
                    {
                        if (_Commission)
                        {
                            if (rbCommissionDyeing.Checked)
                            {
                                var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                                  join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                                  where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && !DyeBatch.DYEB_FabricMode
                                                  && DyeBatchDetails.DYEBO_QAApproved && !DyeBatchDetails.DYEBO_Sold && DyeBatch.DYEB_CommissinCust
                                                  select DyeBatch).GroupBy(x => x.DYEB_BatchNo);

                                foreach (var Group in DyeBatches)
                                {
                                    var Key = Group.FirstOrDefault().DYEB_Pk;
                                    var Desc = Group.FirstOrDefault().DYEB_BatchNo;
                                    cmboCurrentBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                                }
                            }
                            else
                            {
                                if (rbCommissionKnitandDye.Checked)
                                {
                                    var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                                      join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                                      where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && DyeBatch.DYEB_FabricMode
                                                      && DyeBatchDetails.DYEBO_QAApproved && !DyeBatchDetails.DYEBO_Sold
                                                      select DyeBatch).GroupBy(x => x.DYEB_BatchNo);

                                    foreach (var Group in DyeBatches)
                                    {
                                        var Key = Group.FirstOrDefault().DYEB_Pk;
                                        var Desc = Group.FirstOrDefault().DYEB_BatchNo;
                                        cmboCurrentBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                                    }
                                }
                                else
                                {
                                    var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                                      join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                                      where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && DyeBatchDetails.DYEBO_Sold
                                                      select DyeBatchDetails).GroupBy(x => x.DYEBO_TransactionNo);

                                    foreach (var Group in DyeBatches)
                                    {
                                        var Key = Group.FirstOrDefault().DYEBD_DyeBatch_FK;
                                        var Desc = Group.FirstOrDefault().DYEBO_TransactionNo;
                                        cmboBatchesReprint.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                                    }
                                }
                            }
                        }
                        else
                        {
                            DateTime dtTime = DateTime.Now.AddDays(-60);


                            var DyeTrans = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Customer_FK == Selected.Cust_Pk && x.TLDYET_FabricSales && x.TLDYET_Date >= dtTime).GroupBy(x=>x.TLDYET_TransactionNumber) .ToList();
                            foreach (var DyeTran in DyeTrans)
                            {
                                var Key = DyeTran.FirstOrDefault().TLDYET_Pk;
                                var Desc = DyeTran.FirstOrDefault().TLDYET_TransactionNumber;
                                cmboCurrentBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                            }
 
                        }
                    }
                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IList<TLDYE_DyeBatch> DB = new List<TLDYE_DyeBatch>();
            Button oBtn = sender as Button;
            TLADM_TranactionType TranType = null;
            TLADM_LastNumberUsed LNU = null;

            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    if (_Commission)
                    {
                        // This is for Commission Deliveries 
                        //============================================
                        if (!rbCommissionDeliveriesReprint.Checked)
                        {
                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                            if (Dept != null)
                            {
                                TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1300).FirstOrDefault();
                            }

                            LNU = context.TLADM_LastNumberUsed.Find(3);
                            if (LNU != null)
                            {
                                LNU.col9 += 1;
                            }

                            foreach (var Batch in QueryParms.DyeBatches)
                            {
                                var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == Batch.DYEB_Pk).ToList();
                                foreach (var Detail in DyeBatchDetails)
                                {
                                    Detail.DYEBO_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                    Detail.DYEBO_TransactionNo = TxtTransNumber.Text;
                                    Detail.DYEBO_DateSold = dtpTransDate.Value;
                                    Detail.DYEBO_Sold = true;
                                }
                            }

                            try
                            {
                                context.SaveChanges();
                                MessageBox.Show("Data succesfully saved to database");

                                String TransNumber = TxtTransNumber.Text.Trim();

                                QueryParms.CommDyeing = false;
                                StringBuilder sb = new StringBuilder();
                                sb.Append(richTextBox1.Text);
                                QueryParms.Notes = sb;

                                if (rbCommissionDyeing.Checked)
                                {
                                    QueryParms.CommDyeing = true;
                                }
                                else
                                {
                                    QueryParms.CommDyeing = false;
                                }

                                frmDyeViewReport vRep = new frmDyeViewReport(17, QueryParms, TransNumber);
                                int h = Screen.PrimaryScreen.WorkingArea.Height;
                                int w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                                if(vRep != null)
                                {
                                    vRep.Dispose();
                                }


                                vRep = new frmDyeViewReport(16, QueryParms, TransNumber);
                                h = Screen.PrimaryScreen.WorkingArea.Height;
                                w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                                if(vRep != null)
                                {
                                    vRep.Dispose();
                                }
                                frmCommissionDeliveries_Load(this, null);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            QueryParms.CommDyeingReprint = true;
                            String TransNumber = string.Empty;

                            foreach (var DBDet in QueryParms.DyeBatchDetails)
                            {
                                TransNumber = DBDet.DYEBO_TransactionNo;
                            }

                            var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                              join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                              where DyeBatchDetails.DYEBO_TransactionNo == TransNumber
                                              select DyeBatch).GroupBy(x => x.DYEB_BatchNo);

                            foreach (var DBatch in DyeBatches)
                            {
                                var Pk = DBatch.FirstOrDefault().DYEB_Pk;
                                QueryParms.DyeBatches.Add(repo.LoadDyeBatch(Pk));
                            }

                            frmDyeViewReport vRep = new frmDyeViewReport(17, QueryParms, TransNumber);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);

                            vRep = new frmDyeViewReport(16, QueryParms, TransNumber);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);

                            frmCommissionDeliveries_Load(this, null);

                        }
                    }
                    else
                    {
                        // This is for Fabric Sales 
                        //============================================================
                      
                        if(ManagementMode)
                        {
                            foreach(DataRow Row in dt.Rows)
                            {
                                var DBD = context.TLDYE_DyeBatchDetails.Find(Row.Field<int>(0));
                                if (DBD != null)
                                { 
                                    if (!Row.Field<bool>(1) && DBD.DYEBO_TransactionNo.Length != 0)
                                    {
                                        DBD.DYEBO_TransactionNo = string.Empty;
                                        DBD.DYEBO_DateSold = null;
                                        DBD.DYEBO_Sold = false;
                                        DBD.DYEBO_PendingDelivery = false;
                                        DBD.DYEBO_SaleConfirmed = false;
                                        DBD.DYEBO_FabricDespatched = false;

                                        continue;
                                    }

                                    if(CancelPending)
                                    {
                                        DBD.DYEBO_PendingDelivery = false;
                                    }
                                    if (SaleConfirmed)
                                    {
                                        DBD.DYEBO_Sold = true;
                                        DBD.DYEBO_DateSold = dtpTransDate.Value;
                                        DBD.DYEBO_SaleConfirmed = true;
                                        DBD.DYEBO_PendingDelivery = false;
                                    }
                                    else if(FabDespatched)
                                    {
                                        DBD.DYEBO_FabricDespatched = FabDespatched;
                                    }
                                }
                            }
                            try
                            {
                                context.SaveChanges();
                                DialogResult Saved = MessageBox.Show("Data saved to database", "Close this interface option", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if(Saved == DialogResult.Yes)
                                {
                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                            }

                        }
                       // DialogResult Res = MessageBox.Show("Would you like to print the docmentation", "Customer Docunebtation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       // if (Res == DialogResult.Yes)
                       // {
                            QueryParms.FabricSales = true;
                            StringBuilder sb = new StringBuilder();
                            sb.Append(richTextBox1.Text);
                            QueryParms.Notes = sb;

                            frmDyeViewReport vRep = new frmDyeViewReport(17, QueryParms);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Dispose();
                            }

                            vRep = new frmDyeViewReport(16, QueryParms);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Dispose();
                            }
                        // }
                        
                        frmCommissionDeliveries_Load(this, null);
                    }
                }
            }
            else
            {

            }
        }

        private void rbCommissionKnitandDye_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                 var Selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                 if (Selected != null)
                 {
                     cmboCurrentBatches.Items.Clear();
                     
                     if (!cmboCurrentBatches.Visible)
                         cmboCurrentBatches.Visible = true;

                     if (cmboBatchesReprint.Visible)
                         cmboBatchesReprint.Visible = false;


                     using (var context = new TTI2Entities())
                     {
                         var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                           join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                           where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && DyeBatch.DYEB_FabricMode
                                           && DyeBatchDetails.DYEBO_QAApproved && !DyeBatchDetails.DYEBO_Sold
                                           select DyeBatch).GroupBy(x => x.DYEB_BatchNo);

                         foreach (var Group in DyeBatches)
                         {
                             var Key = Group.FirstOrDefault().DYEB_Pk;
                             var Desc = Group.FirstOrDefault().DYEB_BatchNo;
                             cmboCurrentBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                         }
                     }
                 }
            }
        }

        private void rbCommissionDyeing_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                var Selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (Selected != null)
                {
                    cmboCurrentBatches.Items.Clear();

                    if (!cmboCurrentBatches.Visible)
                        cmboCurrentBatches.Visible = true;

                    if (cmboBatchesReprint.Visible)
                        cmboBatchesReprint.Visible = false;

                    using (var context = new TTI2Entities())
                    {
                        var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                          join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                          where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && !DyeBatch.DYEB_FabricMode
                                          && DyeBatchDetails.DYEBO_QAApproved && !DyeBatchDetails.DYEBO_Sold && DyeBatch.DYEB_CommissinCust
                                          select DyeBatch).GroupBy(x => x.DYEB_BatchNo);

                        foreach (var Group in DyeBatches)
                        {
                            var Key = Group.FirstOrDefault().DYEB_Pk;
                            var Desc = Group.FirstOrDefault().DYEB_BatchNo;
                            cmboCurrentBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                        }
                    }
                }
               
            }
        }

        private void rbCommissionDeliveriesReprint_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                var Selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (Selected != null)
                {
                    cmboBatchesReprint.Items.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var DyeBatches = (from DyeBatch in context.TLDYE_DyeBatch
                                          join DyeBatchDetails in context.TLDYE_DyeBatchDetails on DyeBatch.DYEB_Pk equals DyeBatchDetails.DYEBD_DyeBatch_FK
                                          where DyeBatch.DYEB_Customer_FK == Selected.Cust_Pk && DyeBatchDetails.DYEBO_Sold && DyeBatch.DYEB_CommissinCust 
                                          select DyeBatchDetails).GroupBy(x => x.DYEBO_TransactionNo);

                        foreach (var Group in DyeBatches)
                        {
                            var Key = Group.FirstOrDefault().DYEBD_DyeBatch_FK;
                            var Desc = Group.FirstOrDefault().DYEBO_TransactionNo;
                            cmboBatchesReprint.Items.Add(new DyeHouse.CheckComboBoxItem(Key, Desc, false));
                        }
                    }
                    
                    if (!cmboBatchesReprint.Visible)
                        cmboBatchesReprint.Visible = true;
                    if (cmboCurrentBatches.Visible)
                        cmboCurrentBatches.Visible = false;

                }
            }
        }

        private void cmboBatchesReprint_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
