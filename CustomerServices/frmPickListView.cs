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
    public partial class frmPickListView : Form
    {
        DataGridViewTextBoxColumn oTxtA;   // Whse
        DataGridViewTextBoxColumn oTxtB;   // Box Number
        DataGridViewTextBoxColumn oTxtC;   // Purchase Order
        DataGridViewTextBoxColumn oTxtD;   // Date
        DataGridViewTextBoxColumn oTxtE;   // Style 
        DataGridViewTextBoxColumn oTxtF;   // Colour
        DataGridViewTextBoxColumn oTxtG;   // Size
        DataGridViewTextBoxColumn oTxtH;   // Primary Palet Index no

        TLCSV_PurchaseOrder PO;

        bool RePackTran = false;
        DataTable DataT = new DataTable();

        bool FormLoaded;
        Util core;
        bool _PickList;
        
        public frmPickListView(bool PickList)
        {
            // Note for the file if PickList = True then call the Picking list info
            //                   else call the Delivery Note routine 
            //=======================================================================

            InitializeComponent();

            if (PickList)
            {
                this.Text = "Picking List View And Reprint";
                this.label1.Text = "Whse PickList Number";
                this.btnReprint.Text = "PL Reprint";
            }
            else
            {
                this.Text = "Delivery Note View And Reprint";
                this.label1.Text = "Whse Delivery Note Number";
                this.btnReprint.Text = "DN Reprint";
            }

            _PickList = PickList;

            //============================================================
            //---------Define the datatable 
            //=================================================================
            DataT = new System.Data.DataTable();
            
            DataT.Columns.Add("WareHouse", typeof(String));
            DataT.Columns["WareHouse"].DefaultValue = String.Empty;

            DataT.Columns.Add("Box Number", typeof(String));
            DataT.Columns["Box Number"].DefaultValue = String.Empty;

            DataT.Columns.Add("Purchase", typeof(String));
            DataT.Columns["Purchase"].DefaultValue = String.Empty;

            DataT.Columns.Add("Purchase Date", typeof(DateTime));
            DataT.Columns["Purchase Date"].DefaultValue =  null;
         
            DataT.Columns.Add("Style", typeof(String));
            DataT.Columns["Style"].DefaultValue = String.Empty;

            DataT.Columns.Add("Colour", typeof(String));
            DataT.Columns["Colour"].DefaultValue = String.Empty;

            DataT.Columns.Add("Size", typeof(String));
            DataT.Columns["Size"].DefaultValue = String.Empty;

            DataT.Columns.Add("Boxed Qty", typeof(Int32));
            DataT.Columns["Purchase"].DefaultValue = 0;

            dataGridView1.DataSource = DataT;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            core = new Util();

            txtNo.KeyDown += core.txtWin_KeyDownJI;
            txtNo.KeyPress += core.txtWin_KeyPress;

        }

        private void frmPickListView_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            txtCustomer.Text = string.Empty;
            txtNo.Text = string.Empty;
            txtTransDate.Text = string.Empty;
            FormLoaded = true;
            PO = null;
            RePackTran = false;

        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            frmCSViewRep vRep = null;

            if (oBtn != null && FormLoaded)
            {
                if (_PickList)
                {
                    CSVServices svces = new CSVServices();
                    svces.PLReprint = true;
                    svces.DNReprint = false;
                    svces.POCustomer_PK = PO.TLCSVPO_Customer_FK;

                    var Pk = Convert.ToInt32(txtNo.Text);
                    if (!RePackTran)
                    {
                        vRep = new frmCSViewRep(4, svces, Pk);
                    }
                    else
                    {
                        vRep = new frmCSViewRep(23, svces, Pk);
                    }
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
                    CSVServices svces = new CSVServices();
                    svces.PLReprint = false;
                    svces.DNReprint = true;

                    var Pk = Convert.ToInt32(txtNo.Text);

                    vRep = new frmCSViewRep(10, svces, Pk);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            IList<TLCSV_StockOnHand> soh = null;
            if (oBtn != null && FormLoaded)
            {
                DataT.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    int Txt = Convert.ToInt32(txtNo.Text);
                    if (_PickList)
                        soh = context.TLCSV_StockOnHand.Where(x => x.TLSOH_PickListNo == Txt).OrderBy(x => x.TLSOH_BoxNumber).ToList();
                    else
                        soh = context.TLCSV_StockOnHand.Where(x => x.TLSOH_DNListNo == Txt).OrderBy(x => x.TLSOH_BoxNumber).ToList();


                    if (soh.Count > 0)
                    {
                        //----------------------------------------------
                        // WareHouse 
                        // Box Number
                        // Purchase Order  
                        // Purchase Order Date
                        // Style
                        // Colour
                        // Size
                        // Boxed Qty
                        //----------------------------------------------------------
                        var PO_Key = soh.FirstOrDefault().TLSOH_POOrder_FK;

                        PO = context.TLCSV_PurchaseOrder.Find(PO_Key);
                        if (PO != null)
                        {
                            RePackTran = PO.TLCSVPO_RepackTransaction;

                            txtCustomer.Text = context.TLADM_CustomerFile.Find(PO.TLCSVPO_Customer_FK).Cust_Description;
                            txtTransDate.Text = PO.TLCSVPO_TransDate.ToShortDateString();
                        }

                        foreach (var Item in soh)
                        {
                            DataRow dr = DataT.NewRow();
                            dr[0] = context.TLADM_WhseStore.Find(Item.TLSOH_WareHouse_FK).WhStore_Description;
                            dr[1] = Item.TLSOH_BoxNumber;
                            var PODetail = context.TLCSV_PuchaseOrderDetail.Find(Item.TLSOH_POOrderDetail_FK);
                            if (PODetail != null)
                            {
                                dr[2] = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK).TLCSVPO_PurchaseOrder;
                                dr[3] = context.TLCSV_PurchaseOrder.Find(PODetail.TLCUSTO_PurchaseOrder_FK).TLCSVPO_TransDate.ToShortDateString();
                            }
                            dr[4] = context.TLADM_Styles.Find(Item.TLSOH_Style_FK).Sty_Description;
                            dr[5] = context.TLADM_Colours.Find(Item.TLSOH_Colour_FK).Col_Display;
                            dr[6] = context.TLADM_Sizes.Find(Item.TLSOH_Size_FK).SI_Description;
                            dr[7] = Item.TLSOH_BoxedQty;

                            DataT.Rows.Add(dr);

                        }
                    }
                    else
                    {
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                             MessageBox.Show("No records found for selection made");
                        }
                        
                      
                        txtCustomer.Text = string.Empty;
                        txtTransDate.Text = string.Empty;
                    }
                }
            }
        }
    }
}
