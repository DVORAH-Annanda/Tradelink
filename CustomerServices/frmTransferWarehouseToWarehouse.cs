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
    public partial class frmTransferWarehouseToWarehouse : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtBoxA;  // Pk CSV_StockOnHand 
        DataGridViewTextBoxColumn oTxtBoxB;  // CSV Box Number
        DataGridViewTextBoxColumn oTxtBoxC;  // CSV Style 
        DataGridViewTextBoxColumn oTxtBoxD;  // CSV Colour  
        DataGridViewTextBoxColumn oTxtBoxE;  // CSV Size 
        DataGridViewTextBoxColumn oTxtBoxF;  // Boxed Quantity

        Repository repo;
        CustomerServicesParameters QueryParms;

        DataGridViewCheckBoxColumn oChkA;

        bool TransferMode;

        public frmTransferWarehouseToWarehouse()
        {
            InitializeComponent();

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);
           
            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.HeaderText = "Box Number";
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.HeaderText = "Style";
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.ReadOnly = true;
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.HeaderText = "Colour";
            dataGridView1.Columns.Add(oTxtBoxD);
            
            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.ReadOnly = true;
            oTxtBoxE.ValueType = typeof(string);
            oTxtBoxE.HeaderText = "Size";
            dataGridView1.Columns.Add(oTxtBoxE);

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.ReadOnly = true;
            oTxtBoxF.ValueType = typeof(Int32);
            oTxtBoxF.HeaderText = "Box Qty";
            dataGridView1.Columns.Add(oTxtBoxF);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            repo = new Repository();

            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);           
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = QueryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        QueryParms.Colours.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = QueryParms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        QueryParms.Sizes.Remove(value);

                }
            }
        }

        private void frmTransferWarehouseToWarehouse_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                cmboFromwarehouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                cmboFromwarehouse.DisplayMember = "WhStore_Description";
                cmboFromwarehouse.ValueMember = "WhStore_Id";
                cmboFromwarehouse.SelectedItem = -1;

                cmboTowareHouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                cmboTowareHouse.DisplayMember = "WhStore_Description";
                cmboTowareHouse.ValueMember = "WhStore_Id";
                cmboTowareHouse.SelectedItem = -1;

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyle.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }
                
                var Sizes = context.TLADM_Sizes.Where(x=>!(bool)x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).ToList();
                foreach(var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
            }

            QueryParms = new CustomerServicesParameters();
            TransferMode = false;
            btnSubmit.Text = "Submit";
            FormLoaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                var FromWhse = (TLADM_WhseStore)cmboFromwarehouse.SelectedItem;
                if (FromWhse == null)
                {
                    MessageBox.Show("Please select a warehouse from which to transfer");
                    return;
                }

                var ToWhse = (TLADM_WhseStore)cmboTowareHouse.SelectedItem;
                if (ToWhse == null)
                {
                    MessageBox.Show("Please select a warehousewhich to transfer too");
                    return;
                }

                if (FromWhse.WhStore_Id == ToWhse.WhStore_Id)
                {
                    MessageBox.Show("Please select a different warehouse to transfer too");
                    return;
                }

                if (!TransferMode)
                {
                    TransferMode = !TransferMode;
                    btnSubmit.Text = "Transfer";
                    QueryParms.FromWhse = FromWhse.WhStore_Id;

                    var Existing = repo.FromWareHouse(QueryParms);
                    if (Existing.Count() == 0)
                    {
                        MessageBox.Show("No Records found for selection made", FromWhse.WhStore_Description);
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        foreach (var Record in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Record.TLSOH_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Record.TLSOH_BoxNumber;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(Record.TLSOH_Style_FK).Sty_Description;
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(Record.TLSOH_Colour_FK).Col_Display;
                            dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Sizes.Find(Record.TLSOH_Size_FK).SI_Description;
                            dataGridView1.Rows[index].Cells[6].Value = Record.TLSOH_BoxedQty;
                        }
                    }
                }
                else
                {
                    var CountSelected = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                       where (bool)Rows.Cells[1].Value == true
                                       select Rows).Count();

                    if (CountSelected == 0)
                    {
                        MessageBox.Show("Please tick at least one box to be transferred");
                        return;
                    }


              
                    TransferMode = !TransferMode;
                    btnSubmit.Text = "Submit"; 
                    using (var context = new TTI2Entities())
                    {
                        //Need to create a Header Record in the file
                        //=======================================================
                        TLCSV_WhseTransfer WhseTransfer = new TLCSV_WhseTransfer();

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "CSV").FirstOrDefault();
                        if (Dept != null)
                        {
                            var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                            if (LNU != null)
                            {
                                WhseTransfer.TLCSVWHT_Date = DateTime.Now;
                                WhseTransfer.TLCSVWHT_PickList = true;
                                WhseTransfer.TLCSVWHT_FromWhse_Fk = FromWhse.WhStore_Id;
                                WhseTransfer.TLCSVWHT_ToWhse_Fk = ToWhse.WhStore_Id;
                                WhseTransfer.TLCSVWHT_PickListDate = DateTime.Now;
                                WhseTransfer.TLCSVWHT_PickListNo = LNU.col6;

                                LNU.col6 += 1;

                                context.TLCSV_WhseTransfer.Add(WhseTransfer);

                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    return;
                                }
                               

                                foreach (DataGridViewRow Row in dataGridView1.Rows)
                                {
                                    if ((bool)Row.Cells[1].Value == false)
                                        continue;

                                    TLCSV_WhseTransferDetail WhDetail = new TLCSV_WhseTransferDetail();
                                    
                                    WhDetail.TLCSVWHTD_WhseTranfer_FK = WhseTransfer.TLCSVWHT_Pk;
                                    WhDetail.TLCSVWHTD_TLSOH_Fk  = (int)Row.Cells[0].Value;
                                    WhDetail.TLCSVWHTD_PickList = true;

                                    context.TLCSV_WhseTransferDetail.Add(WhDetail);
                                }

                                try
                                {
                                  

                                    context.SaveChanges();
                                    MessageBox.Show("Records successfully updated to database");

                                    FormLoaded = false;

                                    frmCSViewRep vRep = new frmCSViewRep(18, WhseTransfer.TLCSVWHT_Pk);
                                    
                                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                                    vRep.ClientSize = new Size(w, h);
                                    vRep.ShowDialog(this);

                                    this.Close();


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

        private void cmboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
