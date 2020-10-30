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
                groupBox2.Visible = false;
 
            this.cmboCurrentBatches.CheckStateChanged += new System.EventHandler(this.cmboCurrentBatches_CheckStateChanged);
            this.cmboBatchesReprint.CheckStateChanged += new System.EventHandler(this.cmboCurrentBatchesReprint_CheckStateChanged);
        }
        
               

        private void frmCommissionDeliveries_Load(object sender, EventArgs e)
        {
            formloaded = false;
            IList<TLADM_CustomerFile> Customers = null;

            cmboBatchesReprint.Visible = false;

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
                        //Remember...The Item_Pk is a number aimed at the DyeBatchsDetails DYEBD_DyeBatch_FK
                        QueryParms.DyeBatches.Add(repo.LoadDyeBatch(item._Pk));
                    else
                        QueryParms.DyeTransactions.Add(repo.LoadDyeTrans(item._Pk));
                    
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
                            DateTime dtTime = DateTime.Now.AddDays(-30);


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


                                vRep = new frmDyeViewReport(16, QueryParms, TransNumber);
                                h = Screen.PrimaryScreen.WorkingArea.Height;
                                w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);

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
                      
                        QueryParms.FabricSales = true;

                        frmDyeViewReport vRep = new frmDyeViewReport(17, QueryParms);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                        vRep = new frmDyeViewReport(16, QueryParms);
                        h = Screen.PrimaryScreen.WorkingArea.Height;
                        w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

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
