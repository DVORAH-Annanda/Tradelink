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

namespace Spinning
{
    public partial class frmCloseYarnOrder : Form
    {
        bool _Close;
        bool formloaded;
        public frmCloseYarnOrder(bool Close)
        {
            InitializeComponent();
            _Close = Close;
       
            SetUp();
          
            if (Close)
                this.Text = "Close Yarn Order";
            else
                this.Text = "Reinstate Yarn Order"; 

        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                if(_Close)
                  cmbYarnOrders.DataSource = context.TLSPN_YarnOrder.Where(x=>!x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                else
                    cmbYarnOrders.DataSource = context.TLSPN_YarnOrder.Where(x => x.YarnO_Reinstate).OrderBy(x => x.YarnO_OrderNumber).ToList(); 
                
                cmbYarnOrders.ValueMember = "YarnO_Pk";
                cmbYarnOrders.DisplayMember = "YarnO_OrderNumber";
                cmbYarnOrders.SelectedValue = 0;
             
            }

            if (_Close)
            {
                chkReinstateOrder.Checked = false;
                chkCloseOrder.Checked = false;

            }
            else
            {
               groupBox2.Visible = false;
            }
            txtBalToBeProduced.ReadOnly = true;
            txtProducedToDate.ReadOnly = true;
            txtYarnOrderQty.ReadOnly = true;

            formloaded = true;
        }

        private void cmbYarnOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLSPN_YarnOrder)cmbYarnOrders.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var YarnTrx = context.TLSPN_YarnOrder.Find(selected.YarnO_Pk);
                        if (YarnTrx != null)
                        {

                            var OrderWeight = YarnTrx.Yarno_OrderWeight;
                            txtYarnOrderQty.Text = OrderWeight.ToString();

                            //What is the current status of this order 
                            //------------------------------------------------
                            if (context.TLSPN_YarnOrderPallets.Count() != 0
                                && context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_Complete).Count() != 0)
                            {
                                var SpunWeight = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == selected.YarnO_Pk && x.YarnOP_Complete).Sum(x => (decimal ?)x.YarnOP_NettWeight) ?? 0.00M;

                                txtProducedToDate.Text = Math.Round(SpunWeight, 0).ToString();
                                txtBalToBeProduced.Text = Math.Round(OrderWeight - SpunWeight, 0).ToString();
                            }
                            else
                            {
                                txtBalToBeProduced.Text = OrderWeight.ToString();
                                txtProducedToDate.Text = "0.00";
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if (oBtn != null)
            {
                if(!chkCloseOrder.Checked && _Close)
                {
                    MessageBox.Show("Please check the close order checkbox, if applicable prior to selecting the save button");
                    return;

                }
                var selected = (TLSPN_YarnOrder)cmbYarnOrders.SelectedItem;
                if (selected != null)
                {
                    DialogResult res = MessageBox.Show("Please confirm this action", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        using (var context = new TTI2Entities())
                        {
                            selected = context.TLSPN_YarnOrder.Find(selected.YarnO_Pk);

                            if (selected != null)
                            {
                                if (_Close)
                                {
                                    selected.Yarno_Closed = true;
                                    selected.Yarno_ClosedDate = DateTime.Now;

                                    if (chkReinstateOrder.Checked)
                                        selected.YarnO_Reinstate = true;

                                }
                                else
                                {
                                    selected.Yarno_Closed = false;
                                    selected.Yarno_ClosedDate = DateTime.Now;
                                    selected.YarnO_Reinstate = false;
                                }


                                try
                                {
                                    context.SaveChanges();
                                    SetUp();

                                    formloaded = false;
                                    if (_Close)
                                        cmbYarnOrders.DataSource = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                                    else
                                        cmbYarnOrders.DataSource = context.TLSPN_YarnOrder.Where(x => x.YarnO_Reinstate).OrderBy(x => x.YarnO_OrderNumber).ToList();
                                    formloaded = true;

                                    MessageBox.Show("Results stored successfully");
                                    cmbYarnOrders.SelectedIndex = -1;
                                    txtBalToBeProduced.Text = string.Empty;
                                    txtProducedToDate.Text = string.Empty;
                                    txtYarnOrderQty.Text = string.Empty;

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
}
