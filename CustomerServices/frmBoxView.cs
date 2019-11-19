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
    public partial class frmBoxView : Form
    {
        bool Formloaded;
        public frmBoxView()
        {
            InitializeComponent();
        }

        private void frmBoxView_Load(object sender, EventArgs e)
        {
            Formloaded = false;
            
            txtColour.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtDeliveryDate.Text = string.Empty;
            txtDeliveryNote.Text = string.Empty;
            txtGrade.Text = string.Empty;
            txtPickingList.Text = string.Empty;
            txtPoOrder.Text = string.Empty;
            txtSize.Text = string.Empty;
            txtStyle.Text = string.Empty;
            txtWareHouse.Text = string.Empty;
            txtBoxedQty.Text = string.Empty;

            chkDelivered.Checked = false;
            chkInStock.Checked = false;
            chkPicked.Checked = false;
            chkReturned.Checked = false;
            chkWriteOff.Checked = false;

            Formloaded = true;

        }

       

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && Formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    string BoxNumber = txtBoxNumber.Text.ToString();

                    TLCSV_StockOnHand SOH = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == BoxNumber).FirstOrDefault();
                    if (SOH != null)
                    {
                        frmBoxView_Load(this, null);

                        txtWareHouse.Text = context.TLADM_WhseStore.Find(SOH.TLSOH_WareHouse_FK).WhStore_Description;
                        txtColour.Text = context.TLADM_Colours.Find(SOH.TLSOH_Colour_FK).Col_Display;
                        txtGrade.Text = SOH.TLSOH_Grade;
                        txtSize.Text = context.TLADM_Sizes.Find(SOH.TLSOH_Size_FK).SI_Description;
                        txtStyle.Text = context.TLADM_Styles.Find(SOH.TLSOH_Style_FK).Sty_Description;
                        txtPickingList.Text = SOH.TLSOH_WareHousePickList + " / " + SOH.TLSOH_PickListNo;
                        txtDeliveryNote.Text = SOH.TLSOH_WareHouseDeliveryNo + " / " + SOH.TLSOH_DNListNo;
                        txtGrade.Text = SOH.TLSOH_Grade;
                        txtBoxedQty.Text = SOH.TLSOH_BoxedQty.ToString();
                        
                        TLCSV_PurchaseOrder POOrder = context.TLCSV_PurchaseOrder.Find(SOH.TLSOH_POOrder_FK);
                        if (POOrder != null)
                        {
                            txtPoOrder.Text = POOrder.TLCSVPO_PurchaseOrder;
                            txtCustomer.Text = context.TLADM_CustomerFile.Find(POOrder.TLCSVPO_Customer_FK).Cust_Description;
                        }

                        if (!SOH.TLSOH_Write_Off)
                        {
                            chkInStock.Checked = true;

                            if (SOH.TLSOH_Picked && SOH.TLSOH_DNListNo == 0)
                            {
                                chkPicked.Checked = SOH.TLSOH_Picked;
                            }

                            if (SOH.TLSOH_Picked && SOH.TLSOH_DNListNo != 0)
                            {
                                chkInStock.Checked = false;
                                chkDelivered.Checked = true;
                            }

                            if (SOH.TLSOH_Returned)
                            {
                                chkInStock.Checked = true;
                                chkPicked.Checked = SOH.TLSOH_Picked;
                                chkReturned.Checked = SOH.TLSOH_Returned;
                            }
                        }
                        else
                        {
                            chkWriteOff.Checked = true;
                        }

                    }
                    else
                    {
                        MessageBox.Show("No data matching the text as entered found");
                        frmBoxView_Load(this, null);
                    }
                }
            }
        }
    }
}
