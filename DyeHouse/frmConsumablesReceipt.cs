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
    public partial class frmConsumablesReceipt : Form
    {
        DataGridViewTextBoxColumn selecta;  // index of the main record 
        DataGridViewCheckBoxColumn chkBoxa; //  
        DataGridViewTextBoxColumn selectb;  // Chemical Description
        DataGridViewTextBoxColumn selectc;  // Chemical current soh
        DataGridViewTextBoxColumn selectd;  // Value required
        DataGridViewTextBoxColumn selecte;  // Value required

        bool formloaded;
        Util core;
        public frmConsumablesReceipt()
        {
            InitializeComponent();

            core = new Util();

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtTransnumber.Text = "DK" + LNU.col12.ToString().PadLeft(5, '0');
                }
            }
            selecta = new DataGridViewTextBoxColumn();   // record index
            selecta.Visible = false;
            selecta.ValueType = typeof(System.Int32);

            selectb = new DataGridViewTextBoxColumn();  //  Mel / FC value
            selectb.HeaderText = "Description";
            selectb.ValueType = typeof(string);
            selectb.ReadOnly = true;

            selectc = new DataGridViewTextBoxColumn();  //  Liquid Ratio's
            selectc.HeaderText = "Current SOH in Chemical Store";
            selectc.ValueType = typeof(int);
            selectc.ReadOnly = true;

            selectd = new DataGridViewTextBoxColumn();  //  Liquid Ratio's
            selectd.HeaderText = "Required";
            selectd.ValueType = typeof(decimal);

            selecte = new DataGridViewTextBoxColumn();  //  Liquid Ratio's
            selecte.HeaderText = "Required";
            selecte.ValueType = typeof(int);
            selecte.ReadOnly = true;
            selecte.Visible = false;

            chkBoxa = new DataGridViewCheckBoxColumn();
            chkBoxa.HeaderText = "Select";
            chkBoxa.ValueType = typeof(Boolean);

            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(chkBoxa);
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns.Add(selecte);
        }

        private void frmConsumablesReceipt_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboIssueDepartment.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_ChemicalStore).ToList();
                cmboIssueDepartment.ValueMember = "WhStore_Id";
                cmboIssueDepartment.DisplayMember = "WhStore_Description";
                cmboIssueDepartment.SelectedValue = -1;

                cmboReceivingDepartment.DataSource = context.TLADM_WhseStore.Where(x=>x.WhStore_DyeKitchen).ToList();
                cmboReceivingDepartment.ValueMember = "WhStore_Id";
                cmboReceivingDepartment.DisplayMember = "WhStore_Description";
                cmboReceivingDepartment.SelectedValue = -1;
            }
            formloaded = true;
        }

       

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            
            if (oBtn != null && formloaded)
            {
                var WhseIssue = (TLADM_WhseStore)cmboIssueDepartment.SelectedItem;
                if (WhseIssue == null)
                {
                    MessageBox.Show("Please select a Chemical Store from the list provided");
                    return;
                }

                var WhseReceiving = (TLADM_WhseStore)cmboReceivingDepartment.SelectedItem;
                if (WhseReceiving == null)
                {
                    MessageBox.Show("Please select a Dye Kitchen from the list provided");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var LNU = context.TLADM_LastNumberUsed.Find(3);
                    LNU.col12 += 1;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(bool)row.Cells[1].Value)
                            continue;

                        var index = (int)row.Cells[0].Value;

                        //---------------------------------------
                        // 1st decision
                        //-------------------------------------------------------
                        TLDYE_ConsumableSOH newSOH = new TLDYE_ConsumableSOH();
                    
                        newSOH = context.TLDYE_ConsumableSOH.Where(x=>x.DYCSH_WhseStore_FK == WhseReceiving.WhStore_Id && x.DYCSH_Consumable_FK == index).FirstOrDefault();
                        if (newSOH == null)
                        {
                            newSOH = new TLDYE_ConsumableSOH();
                            newSOH.DYCSH_StockOnHand   = (decimal)row.Cells[4].Value;
                            newSOH.DYCSH_TransNumber   = -1 + LNU.col12;
                            newSOH.DYCSH_Consumable_FK = (int)row.Cells[5].Value;
                            newSOH.DYCSH_WhseStore_FK  = WhseReceiving.WhStore_Id;
                            newSOH.DYCSH_DyeKitchen    = WhseReceiving.WhStore_DyeKitchen;

                            context.TLDYE_ConsumableSOH.Add(newSOH);
                        }
                        else
                        {
                            newSOH.DYCSH_StockOnHand += (decimal)row.Cells[4].Value;
                        }

                        TLDYE_ConsumableSOH oldSOH = new TLDYE_ConsumableSOH();
                        oldSOH = context.TLDYE_ConsumableSOH.Find(index);
                        if (oldSOH != null)
                            oldSOH.DYCSH_StockOnHand -= (Decimal)row.Cells[4].Value;
 

                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");

                        DyeReportOptions opts = new DyeReportOptions();
                        opts.fromDate = DateTime.Now;
                        opts.LNU = -1 + LNU.col12;
                        opts.toStore = WhseReceiving.WhStore_Id;

                        TLADM_WhseStore FromWhse = context.TLADM_WhseStore.Where(x => x.WhStore_Code.Contains("CS")).FirstOrDefault();
                        opts.fromStore = FromWhse.WhStore_Id;

                        frmDyeViewReport vRep = new frmDyeViewReport(30, opts);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            
            }
        }

        private void cmboIssueDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    var Selected = (TLADM_WhseStore)oCmbo.SelectedItem;

                    var SOH = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_WhseStore_FK == Selected.WhStore_Id).ToList();
                    foreach (var row in SOH)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = row.DYCSH_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = context.TLADM_ConsumablesDC.Find(row.DYCSH_Consumable_FK).ConsDC_Description;
                        dataGridView1.Rows[index].Cells[3].Value = row.DYCSH_StockOnHand;
                        dataGridView1.Rows[index].Cells[4].Value = 0;
                        dataGridView1.Rows[index].Cells[5].Value = (int)row.DYCSH_Consumable_FK;
                    }
                
                }
            }
        }
    }
}
