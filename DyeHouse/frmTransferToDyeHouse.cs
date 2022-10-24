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
    public partial class frmTransferToDyeHouse : Form
    {
        bool formloaded;
        Util Core;

        DyeQueryParameters QueryParms;
        DyeRepository repo;

        public frmTransferToDyeHouse()
        {
            InitializeComponent();

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key (DYE Batch)          0
            DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check box select which record    1                    2
            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Batch                            2
            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Colour                           3
            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Week Due                         4
            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Style                            5
            DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Greige Weight                    6 
            DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Greige Weight                    7 

            repo = new DyeRepository();

            oTxtA.HeaderText = "Primary Key";
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtA);

            oChkA.HeaderText = "Please Select a Record";
            oChkA.Visible = true;
            oChkA.ValueType = typeof(Boolean);
            dataGridView1.Columns.Add(oChkA);

            oTxtB.HeaderText = "Batch No";
            oTxtB.ValueType = typeof(string);
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Date";
            oTxtC.ValueType = typeof(DateTime);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Colour";
            oTxtD.ValueType = typeof(string);
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Week Due";
            oTxtE.ValueType = typeof(Int32);
            oTxtE.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF.HeaderText = "Style";
            oTxtF.ValueType = typeof(string);
            oTxtF.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtF);

            oTxtG.HeaderText = "Weight";
            oTxtG.ValueType = typeof(decimal);
            oTxtG.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtG);


            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            Core = new Util();

            this.cmboBatches.CheckStateChanged += new System.EventHandler(this.cmboBatches_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new EventHandler(this.cmboColour_CheckStateChanged);
            this.cmboFabricQuality.CheckStateChanged += new System.EventHandler(this.cmboFabricQuality_CheckStateChanged);
            this.cmboFabricWeight.CheckStateChanged += new EventHandler(this.cmboFabricWeight_CheckStateChanged);
            this.cmboFabricWidth.CheckStateChanged += new EventHandler(this.comboFabricWidth_CheckStateChanged);
            
        }

        private void frmTransferToDyeHouse_Load(object sender, EventArgs e)
        {
            formloaded = false;
            QueryParms = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Batches = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Transfered).ToList();
                foreach (var Batch in Batches)
                {
                    cmboBatches.Items.Add(new DyeHouse.CheckComboBoxItem(Batch.DYEB_Pk, Batch.DYEB_BatchNo, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    cmboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var FabricWeights = context.TLADM_FabricWeight.OrderBy(x => x.FWW_Id).ToList();
                foreach (var FabricWeight in FabricWeights)
                {
                    cmboFabricWeight.Items.Add(new DyeHouse.CheckComboBoxItem(FabricWeight.FWW_Id, FabricWeight.FWW_Description, false));
                }

                var FabricWidths = context.TLADM_FabWidth.OrderBy(x => x.FW_Id).ToList();
                foreach (var FabricWidth in FabricWidths)
                {
                    cmboFabricWidth.Items.Add(new DyeHouse.CheckComboBoxItem(FabricWidth.FW_Id, FabricWidth.FW_Description, false));
                }

                var FabricQualities = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                foreach (var FabricQuality in FabricQualities)
                {
                    cmboFabricQuality.Items.Add(new DyeHouse.CheckComboBoxItem(FabricQuality.GQ_Pk, FabricQuality.GQ_Description, false));
                }
            }

            formloaded = true;

        }

         //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboBatches_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.DyeBatches.Add(repo.LoadDyeBatch(item._Pk));
                }
                else
                {
                    var value = QueryParms.DyeBatches.Find(it => it.DYEB_Pk == item._Pk);
                    if (value != null)
                        QueryParms.DyeBatches.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColour_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
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
        private void cmboFabricQuality_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.FabricQualities.Add(repo.LoadFabricQuality(item._Pk));
                }
                else
                {
                    var value = QueryParms.FabricQualities.Find(it => it.GQ_Pk == item._Pk);
                    if (value != null)
                        QueryParms.FabricQualities.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboFabricWeight_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.FabricWeights.Add(repo.LoadFabricWeight(item._Pk));
                }
                else
                {
                    var value = QueryParms.FabricWeights.Find(it => it.FWW_Id == item._Pk);
                    if (value != null)
                        QueryParms.FabricWeights.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboFabricWidth_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.FabricWidths.Add(repo.LoadFabricWidth(item._Pk));
                }
                else
                {
                    var value = QueryParms.FabricWidths.Find(it => it.FW_Id == item._Pk);
                    if (value != null)
                        QueryParms.FabricWidths.Remove(value);
                }
            }
        }

        private void cmboFabricType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {

            }
        }

        private void cmboFabricWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                
            }
        }
        private void cmboFabricWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
               
            }
        }

        private void cmboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                
            }
        }

        private void cmboBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                            
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            
            if (oBtn != null && formloaded)
            {
                dataGridView1.Rows.Clear();

                var DyeBatches = repo.SelectDyeBatches(QueryParms);
                if (DyeBatches.Count() == 0)
                {
                    MessageBox.Show("There are no records pertaining to selection made");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (var DyeBatch in DyeBatches)
                    {
                        var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                        //--------------------------------------------
                        // Fabric Weight, Fabric Width and Quality Group
                        //-----------------------------------------------------
                        if (DyeOrder != null)
                        {
                            var Quality = context.TLADM_Griege.Find(DyeOrder.TLDYO_Greige_FK);

                            if (QueryParms.FabricWeights.Count > 0 && Quality != null)
                            {
                                var FW = QueryParms.FabricWeights.Find(s => s.FWW_Id == Quality.TLGreige_FabricWeight_FK);
                                if (FW == null)
                                    continue;
                            }

                            if (QueryParms.FabricWidths.Count > 0 && Quality != null)
                            {
                                var FWW = QueryParms.FabricWidths.Find(s => s.FW_Id == Quality.TLGreige_FabricWidth_FK);
                                if (FWW == null)
                                    continue;
                            }

                            if (QueryParms.FabricQualities.Count > 0 && Quality != null)
                            {
                                var FabricGroup = context.TLADM_GreigeQuality.Find(Quality.TLGreige_Quality_FK);
                                if (FabricGroup != null)
                                {
                                    var FG = QueryParms.FabricQualities.Find(s => s.GQ_Pk == FabricGroup.GQ_Pk);
                                    if (FG == null)
                                        continue;
                                }
                            }


                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = DyeBatch.DYEB_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = DyeBatch.DYEB_BatchNo;
                            if (DyeOrder != null)
                            {
                                dataGridView1.Rows[index].Cells[3].Value = DyeOrder.TLDYO_OrderDate.ToString("dd/MM/yyyy");
                                var colour = context.TLADM_Colours.Find(DyeOrder.TLDYO_Colour_FK);
                                if (colour != null)
                                {
                                    dataGridView1.Rows[index].Cells[4].Value = colour.Col_Display;
                                }

                                dataGridView1.Rows[index].Cells[5].Value = DyeOrder.TLDYO_DyeReqWeek;


                                var Style = context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK);
                                if (Style != null)
                                {
                                    dataGridView1.Rows[index].Cells[6].Value = Style.Sty_Description;
                                }
                                dataGridView1.Rows[index].Cells[7].Value = Math.Round(DyeBatch.DYEB_BatchKG, 2);
                            }
                        }
                        else
                        {
                            var dof = context.TLDYE_DyeOrderFabric.Find(DyeBatch.DYEB_DyeOrder_FK);
                            if(dof != null)
                            {
                                var Quality = context.TLADM_Griege.Find(dof.TLDYEF_Greige_FK);
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = DyeBatch.DYEB_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = false;
                                dataGridView1.Rows[index].Cells[2].Value = DyeBatch.DYEB_BatchNo;
                                if (dof != null)
                                {
                                    dataGridView1.Rows[index].Cells[3].Value = dof.TLDYEF_OrderDate.ToString("dd/MM/yyyy");
                                    var colour = context.TLADM_Colours.Find(dof.TLDYEF_Colours_FK);
                                    if (colour != null)
                                    {
                                        dataGridView1.Rows[index].Cells[4].Value = colour.Col_Display;
                                    }

                                    dataGridView1.Rows[index].Cells[5].Value = dof.TLDYEF_DyeWeek;
                                    dataGridView1.Rows[index].Cells[6].Value = "Fabric Only";
                                    dataGridView1.Rows[index].Cells[7].Value = Math.Round(DyeBatch.DYEB_BatchKG, 2);
                                }
                            }
                        }

                       
                    }


                }
                
                cmboBatches.Items.Clear();
                cmboColour.Items.Clear();
                cmboFabricQuality.Items.Clear();
                cmboFabricWeight.Items.Clear();
                cmboFabricWidth.Items.Clear();

                frmTransferToDyeHouse_Load(this, null);
               
        
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                TLDYE_DyeBatch db = new TLDYE_DyeBatch();

                using (var context = new TTI2Entities())
                {
                    int DyeBatchKey = (int)oDgv.CurrentRow.Cells[0].Value;
                    db = context.TLDYE_DyeBatch.Find(DyeBatchKey);
                    if (db != null)
                    {
                         db.DYEB_Transfered = true;
                         db.DYEB_TransferDate = dtpTransferDate.Value;

                         var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                         if (Dept != null)
                         {
                             var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 300).FirstOrDefault();
                             if (TranType != null)
                             {
                                 db.DYEB_TransactionType_FK = TranType.TrxT_Pk;
                             }
                         }

                         TLDYE_DyeTransactions dt = new TLDYE_DyeTransactions();
                         dt.TLDYET_BatchNo = db.DYEB_BatchNo;
                         dt.TLDYET_BatchWeight = db.DYEB_BatchKG;
                         dt.TLDYET_Date = dtpTransferDate.Value;
                         dt.TLDYET_SequenceNo = db.DYEB_SequenceNo;
                         dt.TLDYET_TransactionType = db.DYEB_TransactionType_FK;
                         dt.TLDYET_Batch_FK = db.DYEB_Pk;
                         dt.TLDYET_Stage = 2;
                         dt.TLDYET_CurrentStore_FK = (int)context.TLADM_TranactionType.Find(db.DYEB_TransactionType_FK).TrxT_ToWhse_FK;
                      
                         context.TLDYE_DyeTransactions.Add(dt);

                          var DBD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == db.DYEB_Pk).ToList();
                          foreach (var rowx in DBD)
                          {
                              rowx.DYEBO_CurrentStore_FK = dt.TLDYET_CurrentStore_FK;
                          }

                          try
                          {
                             context.SaveChanges();
                             frmDyeViewReport vRep = new frmDyeViewReport(8, DyeBatchKey);
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
               oDgv.Rows.RemoveAt(oDgv.CurrentRow.Index);
            }
        }
    }
}
