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
    public partial class frmDyeConsReceived : Form
    {
        DataGridViewComboBoxColumn oCmboA;  // ConsumablesDC 
        DataGridViewComboBoxColumn oCmboB;  // ConsumablesDC UOM
        DataGridViewComboBoxColumn oCmboC;  // Suppliers
        DataGridViewTextBoxColumn selectb;  // Volume
        DataGridViewTextBoxColumn selecta;  // Box ID
        DataGridViewTextBoxColumn selectc;  // TLOrder No

        DyeHouse.DyeQueryParameters QueryParms;
        DyeHouse.DyeRepository repo;

        Util core;

        public frmDyeConsReceived()
        {
            InitializeComponent();
            core = new Util();

            repo = new DyeRepository();

            oCmboA = new DataGridViewComboBoxColumn();   // Consumables foreign Key
            oCmboA.HeaderText = "Consumables";
            oCmboA.Width = 175;

            oCmboB = new DataGridViewComboBoxColumn();   // Consumables UOM foreign Key
            oCmboB.HeaderText = "UOM";
            oCmboB.Width = 120;

            oCmboC = new DataGridViewComboBoxColumn();   // Consumables UOM foreign Key
            oCmboC.HeaderText = "Suppliers";
            oCmboC.Width = 120;

            selectb = new DataGridViewTextBoxColumn();  //  Mel / FC value
            selectb.HeaderText = "Quantity Received";
            selectb.ValueType = typeof(decimal);

            selecta = new DataGridViewTextBoxColumn();  //  Mel / FC value
            selecta.HeaderText = "Box Id / Container";
            selecta.ValueType = typeof(string);

            selectc = new DataGridViewTextBoxColumn();  //  Mel / FC value
            selectc.HeaderText = "Order No";
            selectc.ValueType = typeof(string);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(oCmboA);   // 0
            dataGridView1.Columns.Add(oCmboC);   // 1
            dataGridView1.Columns.Add(selecta);  // 2
            dataGridView1.Columns.Add(selectc);  // 3
            dataGridView1.Columns.Add(selectb);  // 4
            dataGridView1.Columns.Add(oCmboB);   // 5

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            SetUp(); 
        }

        void SetUp()
        {
            dataGridView1.Rows.Clear();

            QueryParms = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                txtGrnNumber.Text = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == 12).FirstOrDefault().col13.ToString();

                oCmboA.DataSource = context.TLADM_ConsumablesDC.Where(x=>!x.ConsDC_Discontinued).OrderBy(x => x.ConsDC_Description).ToList();
                oCmboA.DisplayMember = "ConsDC_Description";
                oCmboA.ValueMember = "ConsDC_Pk";

                oCmboC.DataSource = context.TLADM_Suppliers.Where(x => !x.Sup_Blocked).OrderBy(x => x.Sup_Description).ToList();
                oCmboC.DisplayMember = "Sup_Description";
                oCmboC.ValueMember = "Sup_Pk";

                cmboChemicalStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_ChemicalStore && x.WhStore_Default).ToList();
                cmboChemicalStore.ValueMember = "WhStore_Id";
                cmboChemicalStore.DisplayMember = "WhStore_Description";
                cmboChemicalStore.SelectedValue = -1;

                oCmboB.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                oCmboB.DisplayMember = "UOM_Description";
                oCmboB.ValueMember = "UOM_Pk";
            }

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
            else if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                
            }
       }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var Cell = dataGridView1.CurrentCell;
            if (cb != null && Cell.ColumnIndex == 0)
            {
                try
                {
                    var selected = (TLADM_ConsumablesDC)cb.SelectedItem;
                    if (selected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var UOM = context.TLADM_UOM.Find(selected.ConsDC_UOM_Fk);
                            if (UOM != null)
                            {
                                dataGridView1.Rows[Cell.RowIndex].Cells[Cell.ColumnIndex + 5].Value = UOM.UOM_Pk;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Incorrect UOM Selected");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    var StoreSelected = (TLADM_WhseStore)cmboChemicalStore.SelectedItem;
                    if (StoreSelected == null)
                    {
                        MessageBox.Show("Please select a Chemical Store from the list provided");
                        return;

                    }

                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == 12).FirstOrDefault();
                    if(LNU != null)
                    {
                        LNU.col13 += 1;
                    }

                    var TransNumber = Int32.Parse(txtGrnNumber.Text);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
                            continue;
                        if(row.Cells[3].Value == null)
                        {
                            row.Cells[3].Value = string.Empty; 
                        }
                        if (row.Cells[4].Value == null)
                        {
                            row.Cells[4].Value = string.Empty;
                        }
                        TLDYE_ConSummableReceived consReceived = new TLDYE_ConSummableReceived();

                        consReceived.DYECON_Consumable_FK = (int)row.Cells[0].Value;
                        consReceived.DYECON_Supplier_FK = (int)row.Cells[1].Value;
                        consReceived.DYECON_ContainerId  = (String)row.Cells[2].Value;
                        consReceived.DYECON_OrderNo = (String)row.Cells[3].Value;
                        consReceived.DYECON_Amount = (decimal)row.Cells[4].Value;
                        consReceived.DYECON_UOM_FK = (int)row.Cells[5].Value;
                        consReceived.DYECON_Pass = false;
                        consReceived.DYECON_WhseStore_FK = StoreSelected.WhStore_Id;
                        consReceived.DYECON_TransactionDate = dtpDateReceived.Value;
                        consReceived.DYECON_TransNumber = TransNumber;
                        context.TLDYE_ConSummableReceived.Add(consReceived);
                      
                        TLDYE_ConsumableSOH soh = new TLDYE_ConsumableSOH();
                        soh.DYCSH_Consumable_FK = consReceived.DYECON_Consumable_FK;
                        soh.DYCSH_SOHQuar = consReceived.DYECON_Amount;
                        soh.DYCSH_QuarantineStore_FK = (int)consReceived.DYECON_WhseStore_FK;
                        soh.DYCSH_DyeKitchen = false;
                        soh.DYCSH_Pass = false;
                        soh.DYCSH_Quarantine = true;
                        soh.DYCSH_TransNumber = TransNumber;
                        context.TLDYE_ConsumableSOH.Add(soh);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data updated to database successfully");
                        QueryParms.Consumable_Whse_FK = StoreSelected.WhStore_Id;

                        DyeHouse.frmDyeViewReport vRep = new DyeHouse.frmDyeViewReport(47, QueryParms, txtGrnNumber.Text);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        
                        SetUp();


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
