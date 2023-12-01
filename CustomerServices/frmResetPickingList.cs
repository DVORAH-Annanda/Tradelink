using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CustomerServices
{
    public partial class frmResetPickingList : Form
    {
        bool FormLoaded;
        Util core;

        Repository repo;
        CustomerServicesParameters QueryParms;

        public frmResetPickingList()
        {
            InitializeComponent();
            core = new Util();

            repo = new Repository();

            this.cmboWarehouses.CheckStateChanged += new EventHandler(this.cmboWareHouses_CheckStateChanged);
            this.cmboCustomer.CheckStateChanged += new System.EventHandler(this.cmboCustomer_CheckStateChanged);
            this.cmboOrderAssigned.CheckStateChanged += new System.EventHandler(this.cmboAssigned_CheckStateChanged);
        }

        private void frmResetPickingList_Load(object sender, EventArgs e)
        {
            var ComboSource = new BindingList<KeyValuePair<int, string>>();
            FormLoaded = false;

            cmboOrderAssigned.DataSource = null;
            cmboOrderAssigned.Items.Clear();

            QueryParms = new CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {
                var WareHouses = context.TLADM_WhseStore.Where(x => x.WhStore_GradeA).OrderBy(x => x.WhStore_Description).ToList();
                foreach (var WareHouse in WareHouses)
                {
                   cmboWarehouses.Items.Add(new CustomerServices.CheckComboBoxItem(WareHouse.WhStore_Id, WareHouse.WhStore_Description, false));
                }

                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmboCustomer.Items.Add(new CustomerServices.CheckComboBoxItem(Customer.Cust_Pk , Customer.Cust_Description, false));
                }
            }

            FormLoaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboWareHouses_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Whses.Add(repo.LoadWhse(item._Pk));

                }
                else
                {
                    var value = QueryParms.Whses.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        QueryParms.Whses.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCustomer_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomers(item._Pk));
                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboAssigned_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.PendingPickingSlips.Add(repo.LoadPickingList(item._Pk));
                }
                else
                {
                    var value = QueryParms.PendingPickingSlips.Find(it => it.TLSOH_PickListNo == item._Pk);
                    if (value != null)
                    {
                        QueryParms.PendingPickingSlips.Remove(value);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
              Button oBtn = (Button)sender;
              if (oBtn != null && FormLoaded)
              {
                  using (var context = new TTI2Entities())
                  {
                      foreach (var Item in QueryParms.PendingPickingSlips)
                      {
                          var SOH = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == Item.TLSOH_PickListNo).ToList();
                          foreach (var SI in SOH)
                          {
                              SI.TLSOH_Picked = false;
                              SI.TLSOH_PickListDate = null;

                              var PODetail = context.TLCSV_PuchaseOrderDetail.Find(SI.TLSOH_POOrderDetail_FK);
                              if (PODetail != null)
                              {
                                  PODetail.TLCUSTO_QtyPicked_ToDate -= SI.TLSOH_BoxedQty;
                                  if(PODetail.TLCUSTO_QtyPicked_ToDate < 0)
                                  {
                                      PODetail.TLCUSTO_QtyPicked_ToDate = 0; 
                                  }
                              }

                              var MergeDetail = context.TLCSV_MergePODetail.Where(x => x.TLMerge_StockOnHand_Fk == SI.TLSOH_Pk).FirstOrDefault();
                              if (MergeDetail != null)
                              {
                                  context.TLCSV_MergePODetail.Remove(MergeDetail);
                              }

                             
                          }
                      }

                    /*
                    var SelectedO = (TLCSV_OrderAllocated)cmboOrderAssigned.SelectedItem; 
                    if(SelectedO != null)
                    {
                        var OrderAssigned = context.TLCSV_OrderAllocated.Find(SelectedO.TLORDA_Pk);
                        if(OrderAssigned != null)
                        {
                            context.TLCSV_OrderAllocated.Remove(OrderAssigned);
                        }
                    }
                    */

                    try
                    {
                        context.SaveChanges();
                        using (DialogCenteringService srves = new DialogCenteringService(this))
                        { 
                            MessageBox.Show("Transactions sucessfully updated");
                        }
                        frmResetPickingList_Load(this, null);

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                          return;
                      }

                  } 
              }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                var SOHDetails = repo.PendingPS(QueryParms);
                
                if (SOHDetails.Count() == 0)
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("There were No records found matching selection criteria made");
                    }
                    return;
                }
                
                cmboOrderAssigned.DataSource = null;
                cmboOrderAssigned.Items.Clear();

                var GroupedSOHDetails = SOHDetails.GroupBy(x=>x.TLSOH_PickListNo);
                foreach (var SOHDetail in GroupedSOHDetails)
                {
                    var PLNumber = (int)SOHDetail.FirstOrDefault().TLSOH_PickListNo;

                    cmboOrderAssigned.Items.Add(new CustomerServices.CheckComboBoxItem(PLNumber, "PL" + PLNumber.ToString(), false));
                }
            }
        }

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboOrderAssigned_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
