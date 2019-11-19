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

namespace Knitting
{
    public partial class frmViewOrders : Form
    {
        bool FormLoaded;
        Util core;

        KnitQueryParameters QueryParms;
        KnitRepository repo;

        DataGridViewCheckBoxColumn oChkA;      // CheckBox Selector 
        DataGridViewTextBoxColumn oTxtA;       // Primary Key Knit Orders
        DataGridViewTextBoxColumn oTxtB;       // Knit Order Number 
        DataGridViewTextBoxColumn oTxtC;       // Knit Order Quality  
        DataGridViewTextBoxColumn oTxtD;       // Knit Order Order Date
        DataGridViewTextBoxColumn oTxtE;       // Knit Order Due Date 
        DataGridViewTextBoxColumn oTxtF;       // Knit Order Machine
        DataGridViewTextBoxColumn oTxtG;       // Knit Order Weight

        public frmViewOrders()
        {
            InitializeComponent();
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;

        }

        private void frmViewOrders_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            QueryParms = new KnitQueryParameters();
            repo = new Knitting.KnitRepository();
            core = new Util();

            txtOrderNumber.Text = string.Empty;

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_Griege.OrderBy(x=>x.TLGreige_Description).ToList();
                foreach (var Record in Existing)
                {
                    cmboGreige.Items.Add(new Knitting.CheckComboBoxItem(Record.TLGreige_Id, Record.TLGreige_Description, false));
                }

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "KNIT").FirstOrDefault();
                if (Dept != null)
                {
                    var Machines = context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == Dept.Dep_Id).OrderBy(x=>x.MD_MachineCode).ToList();
                    foreach (var Machine in Machines)
                    {
                        cmboMachines.Items.Add(new Knitting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_AlternateDesc, false));
                    }
                }
            }

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(Boolean);
            oChkA.Visible = true;

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Knit Order Number
            oTxtB.HeaderText = "Knit Order";
            oTxtB.ValueType = typeof(Int32);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 2 Knit Order Quality
            oTxtC.HeaderText = "Quality";
            oTxtC.ValueType = typeof(String);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Knit Order Date 
            oTxtD.HeaderText = "Date Ordered";
            oTxtD.ValueType = typeof(DateTime);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 4 Knit Order Requested Date 
            oTxtE.HeaderText = "Date Required";
            oTxtE.ValueType = typeof(DateTime);
            oTxtE.Visible = true;
            oTxtE.ReadOnly = true;

            oTxtF = new DataGridViewTextBoxColumn();  // 5 Knitting Machine 
            oTxtF.HeaderText = "Knitting Machine";
            oTxtF.ValueType = typeof(String);
            oTxtF.Visible = true;
            oTxtF.ReadOnly = true;

            oTxtG = new DataGridViewTextBoxColumn();  // 6 Weight Required 
            oTxtG.HeaderText = "Weight Required";
            oTxtG.ValueType = typeof(Decimal);
            oTxtG.Visible = true;
            oTxtG.ReadOnly = true;

        
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.Columns.Add(oTxtG);
            

            this.txtOrderNumber.KeyPress += core.txtWin_KeyPress;
            this.txtOrderNumber.KeyDown  += core.txtWin_KeyDownJI;

            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboMachines.CheckStateChanged += new EventHandler(this.cmboMachines_CheckStateChanged);
        
            FormLoaded = true;


        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem && FormLoaded)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Greiges.Add(repo.LoadGriege(item._Pk));

                }
                else
                {
                    var value = QueryParms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Greiges.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboMachines_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Knitting.CheckComboBoxItem && FormLoaded)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Machines.Add(repo.LoadMachine(item._Pk));
                }
                else
                {
                    var value = QueryParms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Machines.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            IQueryable<TLKNI_Order> Orders = null;
            
            if (oBtn != null && FormLoaded)
            {
                dataGridView1.Rows.Clear();
                DateTime fromDate = Convert.ToDateTime(FromDate.Value.ToShortDateString());
                DateTime toDate = Convert.ToDateTime(ToDate.Value.ToShortDateString());
                toDate.AddHours(23);
                
                using ( var context = new TTI2Entities())
                {
                    if (txtOrderNumber.Text.Length == 0)
                    {
                        Orders = repo.KnitOrders(QueryParms);
                        Orders = Orders.Where(x => x.KnitO_OrderDate >= fromDate && x.KnitO_OrderDate <= toDate);

                        if (!rbIncudeClosedOrders.Checked)
                            Orders = Orders.Where(x => !x.KnitO_Closed);
                    }
                    else
                    {
                        int OrdN = Convert.ToInt32(txtOrderNumber.Text);
                        Orders = context.TLKNI_Order.Where(x => x.KnitO_OrderNumber == OrdN).AsQueryable();
                    }

             
                    if (Orders.Count() > 0)
                    {
                    
                            foreach (var Order in Orders)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Order.KnitO_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = false;
                                dataGridView1.Rows[index].Cells[2].Value = Order.KnitO_OrderNumber;
                                dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(Order.KnitO_Product_FK).TLGreige_Description;
                                dataGridView1.Rows[index].Cells[4].Value = Convert.ToDateTime(Order.KnitO_OrderDate.ToShortDateString()).ToString("dd/MM/yyyy");
                                dataGridView1.Rows[index].Cells[5].Value = Convert.ToDateTime(Order.KnitO_DeliveryDate.ToShortDateString()).ToString("dd/MM/yyy");
                                dataGridView1.Rows[index].Cells[6].Value = context.TLADM_MachineDefinitions.Find(Order.KnitO_Machine_FK).MD_AlternateDesc;
                                dataGridView1.Rows[index].Cells[7].Value = Order.KnitO_Weight;

                            }
                    }
                    else
                    {
                        MessageBox.Show("There are no records pertaining to selection made");
                    }
                }

                rbIncudeClosedOrders.Checked = false;
                txtOrderNumber.Text = string.Empty;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;

             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                 {
                     var CurrentRow = oDgv.CurrentRow;
                     var Pk = (int)CurrentRow.Cells[0].Value;
                     using (frmKnitOrderAdjust ViewOrders = new frmKnitOrderAdjust(Pk))
                     {
                         DialogResult dr = ViewOrders.ShowDialog(this);
                         if (dr == DialogResult.OK)
                         {
                             dataGridView1.Rows.Clear();
                         }
                     }
                 }
             }
        }
    }
}
