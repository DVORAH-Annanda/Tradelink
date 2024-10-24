﻿using System;
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
    public partial class frmConsummablesInspectionResults : Form
    {
        DataTable dt;
        DataColumn column;
        BindingSource Bind;
        bool FormLoaded;
        Util core; 

        DataGridViewTextBoxColumn selecta;    // index of consumable Record 
        DataGridViewTextBoxColumn selectb;    // Consumables 
        DataGridViewCheckBoxColumn oChkA;     // Pass Yes or No;
        DataGridViewTextBoxColumn selectc;    // Supplier
        DataGridViewTextBoxColumn selectd;   // Box / Container ID 
        DataGridViewTextBoxColumn selecte;   // Order No 
        DataGridViewTextBoxColumn selectf;    // Amount
        DataGridViewTextBoxColumn selectg;    // UOM

        public frmConsummablesInspectionResults()
        {
            InitializeComponent();
            dt = new DataTable();
            column = new DataColumn();
            Bind = new BindingSource();
            core = new Util();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            
            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "ConsumableReceived_Pk"; // from TLDYE_ConsumableRecived
            column.Caption = "Consumable Received Key";
            column.DefaultValue = 0;
            dt.Columns.Add(column);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
            
            //----------------------------------------------
            // Col1 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Pass_Pk";
            column.Caption = "Pass";
            column.DefaultValue = false;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col2 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Consummable_Pk";
            column.Caption = "Consumable Description";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col3 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Supplier_Pk";
            column.Caption = "Supplier Description";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col4
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Container_Pk";
            column.Caption = "Container Description";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col5
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Order_Pk";
            column.Caption = "Order Description";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col6 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Quantity_Pk";
            column.Caption = "Volume";
            column.DefaultValue = 0.00M;
            dt.Columns.Add(column);

            //----------------------------------------------
            // Col7 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "UOM_Pk";
            column.Caption = "UOM";
            column.DefaultValue = string.Empty;
            dt.Columns.Add(column);

            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ReadOnly = true;
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index";
            selecta.Visible = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.ValueType = typeof(bool);
            oChkA.DataPropertyName = dt.Columns[1].ColumnName;
            oChkA.HeaderText = "Pass";
            oChkA.Visible = true;
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;

            selectb = new DataGridViewTextBoxColumn();
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = dt.Columns[2].ColumnName;
            selectb.HeaderText = "Consummable";
            selectb.Visible = true;
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].DisplayIndex = 2;

            selectc = new DataGridViewTextBoxColumn();
            selectc.ValueType = typeof(string);
            selectc.ReadOnly = true;
            selectc.DataPropertyName = dt.Columns[3].ColumnName;
            selectc.HeaderText = "Supplier";
            selectc.Visible = true;
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[3].DisplayIndex = 3;

            selectd = new DataGridViewTextBoxColumn();
            selectd.ValueType = typeof(string);
            selectd.DataPropertyName = dt.Columns[4].ColumnName;
            selectd.HeaderText = "Container";
            selectd.Visible = true;
            selectd.ReadOnly = true;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns[4].DisplayIndex = 4;

            selecte = new DataGridViewTextBoxColumn();
            selecte.ValueType = typeof(string);
            selecte.DataPropertyName = dt.Columns[5].ColumnName;
            selecte.HeaderText = "Order No";
            selecte.Visible = true;
            selecte.ReadOnly = true;
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns[5].DisplayIndex = 5;

            selectf = new DataGridViewTextBoxColumn();
            selectf.ValueType = typeof(decimal);
            selectf.DataPropertyName = dt.Columns[6].ColumnName;
            selectf.HeaderText = "Quantities";
            selectf.Visible = true;
            dataGridView1.Columns.Add(selectf);
            dataGridView1.Columns[6].DisplayIndex = 6;
            
            selectg = new DataGridViewTextBoxColumn();
            selectg.ValueType = typeof(string);
            selectg.DataPropertyName = dt.Columns[7].ColumnName;
            selectg.HeaderText = "UOM";
            selectg.Visible = true;
            selectg.ReadOnly = true;
            dataGridView1.Columns.Add(selectg);
            dataGridView1.Columns[7].DisplayIndex = 7;


            Bind.DataSource = dt;
            dataGridView1.DataSource = Bind;
        }

        private void frmConsummablesInspectionResults_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using ( var Context = new TTI2Entities())
            {
                cmboWhseStore.DataSource = Context.TLADM_WhseStore.Where(x => x.WhStore_ChemicalStore && !x.WhStore_Default && !x.WhStore_DyeKitchen).ToList();
                cmboWhseStore.ValueMember = "WhStore_Id";
                cmboWhseStore.DisplayMember = "WhStore_Description";
                cmboWhseStore.SelectedValue = -1;

                dt.Rows.Clear();

                var ConsumablesSOH = Context.TLDYE_ConsumableSOH.Where(x => !x.DYCSH_Pass && x.DYCSH_Quarantine).ToList();
                foreach(var ConSOH in ConsumablesSOH)
                {
                    var NewRow = dt.NewRow();
                    NewRow[0] = ConSOH.DYCSH_Pk;
                    NewRow[1] = false;
                    NewRow[2] = Context.TLADM_ConsumablesDC.Find(ConSOH.DYCSH_Consumable_FK).ConsDC_Description;

                    var ConSumerRec = Context.TLDYE_ConSummableReceived.FirstOrDefault(s=>s.DYECON_Consumable_FK == ConSOH.DYCSH_Consumable_FK && s.DYECON_TransNumber == ConSOH.DYCSH_TransNumber);
                    if (ConSumerRec != null)
                    {
                        var Supplier = Context.TLADM_Suppliers.Find(ConSumerRec.DYECON_Supplier_FK);
                        if (Supplier != null)
                        {
                            NewRow[3] = Supplier.Sup_Description;
                        }
                        NewRow[4] = ConSumerRec.DYECON_ContainerId;
                        NewRow[5] = ConSumerRec.DYECON_OrderNo;
                        NewRow[6] = ConSumerRec.DYECON_Amount;
                        NewRow[7] = Context.TLADM_UOM.Find(ConSumerRec.DYECON_UOM_FK).UOM_Description;
                    }
                    dt.Rows.Add(NewRow);
                }
            }
            
            FormLoaded = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                var StoreSelected = (TLADM_WhseStore)cmboWhseStore.SelectedItem;
                if(StoreSelected == null)
                {
                    MessageBox.Show("Please select a Store");
                    return;
                }

                
                using (var Context = new TTI2Entities())
                {
                    var StorePk = StoreSelected.WhStore_Id; 
                    
                    foreach (DataRow Row in dt.Rows)
                    {
                        if (!Row.Field<bool>(1))
                        {
                            continue;
                        }

                        var Pk = Row.Field<int>(0);
                        var Amt = Row.Field<decimal>(6);

                        var soh = Context.TLDYE_ConsumableSOH.Find(Pk);
                        if (soh != null)
                        {
                            soh.DYCSH_WhseStore_FK = StorePk;
                            soh.DYCSH_SOHQuar -= Amt;
                            soh.DYCSH_StockOnHand = Amt;
                            if(soh.DYCSH_SOHQuar <= 0 )
                            {
                                soh.DYCSH_Quarantine = false;
                                soh.DYCSH_Pass = true;
                            }
                        }
                            
                    }

                    try
                    {
                        Context.SaveChanges();
                        MessageBox.Show("Data successfully save to database");
                        dt.Rows.Clear();
                        return;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if ( oDgv != null && FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 6)
                       
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }
    }
}
