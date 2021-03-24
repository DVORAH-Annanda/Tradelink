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
    public partial class frmKnitOrderAdjust : Form
    {
        Boolean FormLoaded;
        int _KnitOrderPk;
        TLKNI_Order KnitO;
        Util core;

        DataTable dt;
        DataTable dt1;

        BindingSource BindingSrc;
        BindingSource BindingSrc1;

        DataGridViewTextBoxColumn selecta;
        DataGridViewTextBoxColumn selectb;
        DataGridViewTextBoxColumn selectc;

        DataGridViewTextBoxColumn selectd;
        DataGridViewTextBoxColumn selecte;
        DataGridViewTextBoxColumn selectf;

        DataColumn column;

        DataGridViewCheckBoxColumn oChk;

        public frmKnitOrderAdjust(int KnitOrderPk)
        {
            InitializeComponent();
            
            _KnitOrderPk = KnitOrderPk;

            core = new Util();

            dt = new DataTable();
            dt1 = new DataTable();

            BindingSrc = new BindingSource();
            BindingSrc1 = new BindingSource();
            //------------------------------------------------------
            // Create column 0. // This is the record index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            column.DefaultValue = 0;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the whether the record is discontinued or not 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "Col1";
            column.Caption = "Delete";
            column.DefaultValue = false;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // Shows Piece Number  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Col2";
            column.Caption = "Piece Number";
            column.DefaultValue = String.Empty;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 3. // This is the whether the record is discontinued or not 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Col3";
            column.Caption = "Piece Weight";
            column.DefaultValue = 0.00M;
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is the whether the record is discontinued or not 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col4";
            column.Caption = "GreigeKey";
            column.DefaultValue = 0;
            dt.Columns.Add(column);


            //=================================================
            //3 -- Record Index 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "IndexPos";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Index Pos";
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns["IndexPos"].DisplayIndex = 0;


            oChk = new DataGridViewCheckBoxColumn();
            oChk.Name = "Delete";
            oChk.ValueType = typeof(Boolean);
            oChk.DataPropertyName = dt.Columns[1].ColumnName;
            oChk.HeaderText = "Delete";
            dataGridView1.Columns.Add(oChk);
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns["Delete"].DisplayIndex = 1;

            //=================================================
            //3 -- Record Index 
            //--------------------------------------------
            selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "PNumber";
            selectb.ValueType = typeof(String);
            selectb.DataPropertyName = dt.Columns[2].ColumnName;
            selectb.HeaderText = "Piece Number";
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns["PNumber"].DisplayIndex = 2;

            //=================================================
            //3 -- Record Index 
            //--------------------------------------------
            selectc = new DataGridViewTextBoxColumn();
            selectc.Name = "PWeight";
            selectc.ValueType = typeof(Decimal);
            selectc.DataPropertyName = dt.Columns[3].ColumnName;
            selectc.HeaderText = "Piece Weight";
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns["PWeight"].DisplayIndex = 3;


            //------------------------------------------------------
            // Create column 0. // This is the record index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            column.DefaultValue = 0;
            dt1.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the Pallet Number 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Col1";
            column.Caption = "Pallet Number";
            column.DefaultValue = String.Empty;
            dt1.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // This is Pallet Weight 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Col2";
            column.DefaultValue = 0M;
            dt1.Columns.Add(column);

            //=================================================
            //=================================================
            selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "IndexPos";
            selectd.ValueType = typeof(Int32);
            selectd.DataPropertyName = dt1.Columns[0].ColumnName;
            selectd.HeaderText = "Index Pos";
            dataGridView2.Columns.Add(selectd);
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns["IndexPos"].DisplayIndex = 0;


            selecte = new DataGridViewTextBoxColumn();
            selecte.Name = "PalletNo";
            selecte.ValueType = typeof(Int32);
            selecte.DataPropertyName = dt1.Columns[1].ColumnName;
            selecte.HeaderText = "Pallet No";
            dataGridView2.Columns.Add(selecte);
            dataGridView2.Columns[1].Visible = true;
            dataGridView2.Columns["PalletNo"].DisplayIndex = 1;

            selectf = new DataGridViewTextBoxColumn();
            selectf.Name = "PalletWeight";
            selectf.ValueType = typeof(Decimal);
            selectf.DataPropertyName = dt1.Columns[2].ColumnName;
            selectf.HeaderText = "Pallet Weight";
            dataGridView2.Columns.Add(selectf);
            dataGridView2.Columns[2].Visible = true;
            dataGridView2.Columns["PalletWeight"].DisplayIndex = 2;

                   

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToOrderColumns = false;

        }

        private void frmKnitOrderAdjust_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                comboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                comboGreige.ValueMember = "TLGreige_Id";
                comboGreige.DisplayMember = "TLGreige_Description";
                comboGreige.SelectedValue = -1;
                
                var Department = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Department != null)
                {
                    comboMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Department.Dep_Id).OrderBy(x => x.MD_AlternateDesc).ToList();
                    comboMachine.DisplayMember = "MD_AlternateDesc";
                    comboMachine.ValueMember = "MD_Pk";
                    comboMachine.SelectedValue = -1;

                    KnitO = context.TLKNI_Order.Find(_KnitOrderPk);

                    if (KnitO != null)
                    {
                        cbResetYarnAllocated.Checked = false;

                        if (KnitO.KnitO_Closed)
                           rbClosed.Checked = true;
                        else
                           rbActive.Checked = true;

                        label5.Text = KnitO.KnitO_OrderNumber.ToString();
                        comboGreige.SelectedValue = KnitO.KnitO_Product_FK;
                        label4.Text = KnitO.KnitO_NoOfPieces.ToString();

                        comboMachine.SelectedValue = KnitO.KnitO_Machine_FK;

                        var GreigeProduction = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KnitO.KnitO_Pk).ToList();
                        foreach (var Item in GreigeProduction)
                        {
                            DataRow Row = dt.NewRow();
                            
                            Row[0] = Item.GreigeP_Pk;
                            Row[1] = false;
                            Row[2] = Item.GreigeP_PieceNo;
                            Row[3] = Item.GreigeP_weight;
                            Row[4] = Item.GreigeP_Greige_Fk;
                            
                            dt.Rows.Add(Row);
                          
                        }

                        BindingSrc.DataSource = dt;
                        dataGridView1.DataSource = BindingSrc;

                        int idx = -1;

                        foreach (DataColumn col in dt.Columns)
                        {
                            if (++idx == 0)
                                dataGridView1.Columns[idx].Visible = false;
                            else
                                dataGridView1.Columns[idx].HeaderText = col.Caption;

                        }

                        var YarnAllocated = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_TranType == 1 && x.TLKYT_KnitOrder_FK == KnitO.KnitO_Pk).ToList();
                        foreach (var Item in YarnAllocated)
                        {
                            DataRow Row1 = dt1.NewRow();

                            Row1[0] = Item.TLKYT_Pk;
                            Row1[1] = context.TLKNI_YarnOrderPallets.Find(Item.TLKYT_YOP_FK).TLKNIOP_PalletNo ;
                            Row1[2] = Item.TLKYT_NettWeight;
                            dt1.Rows.Add(Row1);

                        }

                        BindingSrc1.DataSource = dt1;
                        dataGridView2.DataSource = BindingSrc1;
                        idx = -1;

                        foreach (DataColumn col in dt1.Columns)
                        {
                            if (++idx > 0)
                            {
                                dataGridView2.Columns[idx].HeaderText = col.Caption;
                            }

                        }
                    }
                }

            }

            FormLoaded = true;
        }

        private void comboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                var Selected = (TLADM_Griege)oCmbo.SelectedItem;
                if (Selected != null)
                {
                    using ( var context = new TTI2Entities())
                    {
                        comboMachine.DataSource = null;
                        comboMachine.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_GreigeType_FK == Selected.TLGreige_Id).ToList();
                        comboMachine.DisplayMember = "MD_AlternateDesc";
                        comboMachine.ValueMember = "MD_Pk";
                        comboMachine.SelectedValue = -1;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                var GreigeSelected = (TLADM_Griege)comboGreige.SelectedItem;
                if (GreigeSelected == null)
                {
                    MessageBox.Show("Please select a Quality item from the drop down box");
                    return;
                }

                var MachineSelected = (TLADM_MachineDefinitions)comboMachine.SelectedItem;
                if (MachineSelected == null)
                {
                    MessageBox.Show("Please select a Machine from the drop down box provided");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var KO = context.TLKNI_Order.Find(KnitO.KnitO_Pk);
                    if (KO != null)
                    {
                        KO.KnitO_Machine_FK = MachineSelected.MD_Pk;
                        KO.KnitO_Product_FK = GreigeSelected.TLGreige_Id;
                      
                        if (rbActive.Checked)
                        {
                            KO.KnitO_Closed = false;
                            KO.KnitO_ClosedDate = null;
                        }
                        else
                        {
                            KO.KnitO_Closed = true;
                            if (KnitO.KnitO_ClosedDate == null)
                                KnitO.KnitO_ClosedDate = DateTime.Now;
                        }


                        foreach(DataRow dr in dt.Rows)
                        {
                            if(dr.Field<Int32>(4) != GreigeSelected.TLGreige_Id || dr.Field<bool>(1))
                            {
                                var RecKey = dr.Field<Int32>(0);

                                var GP = context.TLKNI_GreigeProduction.Find(RecKey);

                                if(GP != null )
                                {
                                    if (dr.Field<bool>(1))
                                    {
                                        context.TLKNI_GreigeProduction.Remove(GP);
                                    }
                                    else
                                    {
                                        GP.GreigeP_Greige_Fk = GreigeSelected.TLGreige_Id;
                                    }
                                }
                            }
                        }
                        var TotalPalletWeight = 0.00M;

                        foreach (DataRow dr in dt1.Rows)
                        {
                            TotalPalletWeight += dr.Field<Decimal>(2); 
                            var RecKey = dr.Field<Int32>(0);

                            var AllocTrans = context.TLKNI_YarnAllocTransctions.Find(RecKey);

                            if(AllocTrans != null && AllocTrans.TLKYT_NettWeight != dr.Field<Decimal>(2) )
                            {
                                AllocTrans.TLKYT_NettWeight = dr.Field<Decimal>(2);
                            }
                        }

                        if (TotalPalletWeight != KnitO.KnitO_Weight)
                        {
                            KnitO.KnitO_Weight = TotalPalletWeight;
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully stored to the database");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }
        }

        private void cbResetYarnAllocated_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if (oChk != null && FormLoaded)
            {
                if (oChk.Checked)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confimation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        using (var context = new TTI2Entities())
                        {
                            if (KnitO == null)
                                KnitO = context.TLKNI_Order.Find(_KnitOrderPk);

                            if (KnitO != null && KnitO.KnitO_YarnAssigned)
                            {
                                KnitO.KnitO_YarnAssigned = false;

                                var YarnAllocs = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KnitO.KnitO_Pk).ToList();
                                foreach (var YarnAlloc in YarnAllocs)
                                {
                                    var YarnPallet = context.TLKNI_YarnOrderPallets.Find(YarnAlloc.TLKYT_YOP_FK);
                                    if(YarnPallet != null)
                                    {
                                        YarnPallet.TLKNIOP_ReservedBy = 0;
                                        YarnPallet.TLKNIOP_NettWeightReserved = 0;
                                        YarnPallet.TLKNIOP_ConesReserved = 0;
                                        YarnPallet.TLKNIOP_ReservedDate = null;
                                        YarnPallet.TLKNIOP_AdditionalYarn = 0;
                                        YarnPallet.TLKNIOP_NettWeightReturned = 0;
                                    }
                                }

                                context.TLKNI_YarnAllocTransctions.RemoveRange(context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KnitO.KnitO_Pk));

                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Data successfully saved to database");
                                    return;
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
                            else
                            {
                                MessageBox.Show("Unable to action request. No yarn currently assigned");
                            }
                        }
                    }
                }
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                frmKnitViewRep vRep = new frmKnitViewRep(7, KnitO.KnitO_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            // if (formloaded)
            //{
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 2 )
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                   
                }
            // }

        }
    }
}
