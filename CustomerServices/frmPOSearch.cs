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
    public partial class frmPOSearch : Form
    {
        bool FormLoaded;

        DataTable DataT;
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Box Number         0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Picking List       1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Picking List Date  2                         2
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Delivery Note      3                             3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Delivery Note Date 4                         4
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Style              5                              5
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Colour             6 
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // Size               7                          7
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // Boxed Qty          8                           8
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // Grade              9              9


        public frmPOSearch()
        {
            InitializeComponent();

            DataT = new DataTable();
            DataT.Columns.Add("Box Number", typeof(String));
            DataT.Columns["Box Number"].DefaultValue = String.Empty;

            DataT.Columns.Add("Picking List", typeof(String));
            DataT.Columns["Picking List"].DefaultValue = String.Empty;

            DataT.Columns.Add("Picking List Date", typeof(DateTime));
            DataT.Columns["Picking List Date"].DefaultValue = null;

            DataT.Columns.Add("Delivery Note", typeof(String));
            DataT.Columns["Delivery Note"].DefaultValue = String.Empty;

            DataT.Columns.Add("Delivery Note Date", typeof(DateTime));
            DataT.Columns["Delivery Note Date"].DefaultValue = null;

            DataT.Columns.Add("Style", typeof(String));
            DataT.Columns["Style"].DefaultValue = String.Empty;

            DataT.Columns.Add("Colour", typeof(String));
            DataT.Columns["Colour"].DefaultValue = String.Empty;

            DataT.Columns.Add("Size", typeof(String));
            DataT.Columns["Size"].DefaultValue = String.Empty;

            DataT.Columns.Add("Boxed Qty", typeof(Int32));
            DataT.Columns["Boxed Qty"].DefaultValue = 0;

            DataT.Columns.Add("Grade", typeof(String));
            DataT.Columns["Grade"].DefaultValue = String.Empty;

            dataGridView1.DataSource = DataT;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            /*
            oTxtA.HeaderText = "Box Number";
            oTxtA.ValueType = typeof(String);

            oTxtB.HeaderText = "Picking List";
            oTxtB.ValueType = typeof(String);
            
            oTxtC.HeaderText = "Picking List Date";
            oTxtC.ValueType = typeof(DateTime);

            oTxtD.HeaderText = "Delivery Note";
            oTxtD.ValueType = typeof(String);

            oTxtE.HeaderText = "Delivery Note Date";
            oTxtE.ValueType = typeof(DateTime);

            oTxtF.HeaderText = "Style";
            oTxtF.ValueType = typeof(String);

            oTxtG.HeaderText = "Colour";
            oTxtG.ValueType = typeof(String);

            oTxtH.HeaderText = "Size";
            oTxtH.ValueType = typeof(String);

            oTxtJ.HeaderText = "Boxed Qty";
            oTxtJ.ValueType = typeof(int);

            oTxtK.HeaderText = "Grade";
            oTxtK.ValueType = typeof(String);

            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns.Add(oTxtG);
            dataGridView1.Columns.Add(oTxtH);
            dataGridView1.Columns.Add(oTxtJ);
            dataGridView1.Columns.Add(oTxtK);
            */

        }

        private void frmPOSearch_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.SelectedValue = -1;
            }
            FormLoaded = true;
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                DataT.Rows.Clear();

                var selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        FormLoaded = false;
                        cmboPurchaseOrder.DataSource = null;
                        
                        if (rbOpenOrders.Checked)
                        {
                            cmboPurchaseOrder.DataSource = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == selected.Cust_Pk && !x.TLCSVPO_Closeed).ToList();
                        }
                        else
                        {
                            cmboPurchaseOrder.DataSource = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == selected.Cust_Pk && x.TLCSVPO_Closeed).ToList();
                        }
                                             
                        cmboPurchaseOrder.ValueMember = "TLCSVPO_Pk";
                        cmboPurchaseOrder.DisplayMember = "TLCSVPO_PurchaseOrder";
                        cmboPurchaseOrder.SelectedValue = -1;
                        FormLoaded = true;
                    }
                }
           }
        }

        private void cmboPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBox oCmbo = (ComboBox)sender;
             if (oCmbo != null && FormLoaded)
             {
                 var selected = (TLCSV_PurchaseOrder)oCmbo.SelectedItem;
                 if (selected != null)
                 {
                     DataT.Rows.Clear();

                     using (var context = new TTI2Entities())
                     {
                         var Records = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == selected.TLCSVPO_Pk).ToList();
                         foreach (var Record in Records)
                         {
                             var Boxes = context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == Record.TLCUSTO_Pk).ToList();
                             foreach (var Box in Boxes)
                             {
                                 DataRow dr = DataT.NewRow();

                                 dr[0] = Box.TLSOH_BoxNumber;
                                 dr[1] = Box.TLSOH_PickListNo.ToString();
                                 if (Box.TLSOH_PickListDate != null)
                                 {
                                     dr[2] = Box.TLSOH_PickListDate;
                                 }
                                 dr[3] = Box.TLSOH_DNListNo.ToString();
                                 if (Box.TLSOH_DNListDate != null)
                                 {
                                     dr[4] = Box.TLSOH_DNListDate;
                                 }
                                 dr[5] = context.TLADM_Styles.Find(Box.TLSOH_Style_FK).Sty_Description ;
                                 dr[6] = context.TLADM_Colours.Find(Box.TLSOH_Colour_FK).Col_Display;
                                 dr[7] = context.TLADM_Sizes.Find(Box.TLSOH_Size_FK).SI_Description;
                                 dr[8] = Box.TLSOH_BoxedQty;
                                 dr[9] = Box.TLSOH_Grade;

                                 DataT.Rows.Add(dr);
                             }
                         }
                     }
                 }
             }
        }
    }
}
