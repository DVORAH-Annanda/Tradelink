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

namespace Cutting
{
    public partial class FrmSelectBIF : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtBoxA;  // Pk Greige Production 
        DataGridViewTextBoxColumn oTxtBoxB;  // Piece No 
        DataGridViewTextBoxColumn oTxtBoxC;  // Weight 
        DataGridViewTextBoxColumn oTxtBoxD;  // Quality  
        DataGridViewTextBoxColumn oTxtBoxE;  // Colour 
        // DataGridViewTextBoxColumn oTxtBoxF;  // Fabic Weight

        Cutting.CuttingRepository  repo;
        Cutting.CuttingQueryParameters QueryParms;

        DataGridViewCheckBoxColumn oChkA;     // Select

        
        public FrmSelectBIF()
        {
            InitializeComponent();
            
            oTxtBoxA = new DataGridViewTextBoxColumn();  // 0
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();  // 1
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn(); // 2
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.HeaderText = "Piece No";
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn(); // 3
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.ValueType = typeof(decimal);
            oTxtBoxC.HeaderText = "Weight";
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();  // 4
            oTxtBoxD.ReadOnly = true;
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.HeaderText = "Quality";
            dataGridView1.Columns.Add(oTxtBoxD);

            oTxtBoxE = new DataGridViewTextBoxColumn();  // 5
            oTxtBoxE.ReadOnly = true;
            oTxtBoxE.ValueType = typeof(string);
            oTxtBoxE.HeaderText = "Colour";
            dataGridView1.Columns.Add(oTxtBoxE);

            /*oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.ReadOnly = true;
            oTxtBoxF.ValueType = typeof(decimal);
            oTxtBoxF.HeaderText = "Fab Weight";
            dataGridView1.Columns.Add(oTxtBoxF); */

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            repo = new CuttingRepository();
        }

        private void FrmSelectBIF_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboWareHouses.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                    cmboWareHouses.ValueMember = "WhStore_Id";
                    cmboWareHouses.DisplayMember = "WhStore_Description";
                    cmboWareHouses.SelectedValue = -1;

                    Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("BLWCT")).FirstOrDefault();
                    if (Dept != null)
                    {
                        cmboToWareHouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id ).ToList();
                        cmboToWareHouse.ValueMember = "WhStore_Id";
                        cmboToWareHouse.DisplayMember = "WhStore_Description";
                        cmboToWareHouse.SelectedValue = -1;
                    }
                }
                
            }
            
            QueryParms = new CuttingQueryParameters();
            FormLoaded = true;
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var Selected = (TLADM_WhseStore)oCmbo.SelectedItem;
                    if (Selected != null)
                    {
                        FormLoaded = false;
                        var Entries = (from GP in context.TLKNI_GreigeProduction
                                       join DBD in context.TLDYE_DyeBatchDetails on GP.GreigeP_Pk equals DBD.DYEBD_GreigeProduction_FK
                                       where GP.GreigeP_BoughtIn && !DBD.DYEBO_CutSheet && !DBD.DYEBO_BIFInTransit && DBD.DYEBO_CurrentStore_FK == Selected.WhStore_Id
                                       select GP).ToList();

                        foreach (var Entry in Entries)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Entry.GreigeP_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Entry.GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(Entry.GreigeP_weight, 1);
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Griege.Find(Entry.GreigeP_Greige_Fk).TLGreige_Description;
                            if (Entry.GreigeP_BIFColour_FK != 0)
                                dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(Entry.GreigeP_BIFColour_FK).Col_Display;   
                        }

                        FormLoaded = true;
                    }
                  
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            TLDYE_BIFInTransit BIFTransit = new TLDYE_BIFInTransit();

            if (oBtn != null && FormLoaded)
            {
               

                var CurrWhse = (TLADM_WhseStore)cmboWareHouses.SelectedItem;
                if (CurrWhse == null)
                {
                    MessageBox.Show("Please select a from Warehouse");
                    return;
                }

                var ToWhse =  (TLADM_WhseStore)cmboToWareHouse.SelectedItem;
                if (ToWhse == null)
                {
                    MessageBox.Show("Please select a to Warehouse");
                    return;
                }

                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).FirstOrDefault();

                if (SingleRow == null)
                {
                    MessageBox.Show("Please select at least one record");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "CUT").FirstOrDefault();
                    if (Dept != null)
                    {
                        var LstNumberUsed = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();

                        if (LstNumberUsed != null)
                        {
                            BIFTransit = new TLDYE_BIFInTransit();
                            BIFTransit.BIFT_PickingList = true;
                            BIFTransit.BIFT_PickingList_Date = DateTime.Now;
                            BIFTransit.BIFT_PickingList_Number = LstNumberUsed.col3;
                            BIFTransit.BIFT_FromFabric_FK = CurrWhse.WhStore_Id;
                            BIFTransit.BIFT_ToFabric_FK = ToWhse.WhStore_Id;

                            context.TLDYE_BIFInTransit.Add(BIFTransit);
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
                                var IsChecked = (bool)Row.Cells[1].Value;
                                if (!IsChecked)
                                    continue;

                                TLDYE_BIFInTransitDetails TransDetails = new TLDYE_BIFInTransitDetails();
                                TransDetails.BIFD_Intransit_FK = BIFTransit.BIFT_Pk;
                                var Greige_Pk = (int)Row.Cells[0].Value;
                                TransDetails.BIFD_Greige_FK = Greige_Pk;
                                if (Row.Cells[5].Value != null)
                                {
                                    var Col = (string)Row.Cells[5].Value;
                                    var Colour_Pk = context.TLADM_Colours.Where(x=>x.Col_Display == Col).FirstOrDefault().Col_Id;
                                    TransDetails.BIFD_Colour_FK = Colour_Pk;
                                }
                              
                                var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == Greige_Pk).FirstOrDefault();
                                if (DyeBatchDetail != null)
                                {
                                    DyeBatchDetail.DYEBO_BIFInTransit = true;
                                    TransDetails.BIFD_DyeBatchDetail_FK = DyeBatchDetail.DYEBD_Pk;
                                }

                                context.TLDYE_BIFInTransitDetails.Add(TransDetails);
                            }

                            try
                            {
                                LstNumberUsed.col3 += 1;

                                context.SaveChanges();
                                MessageBox.Show("Data successfully saved to database");

                                dataGridView1.Rows.Clear();

                                FrmSelectBIF_Load(this, null);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            
                        }
                    }
                }

                Cutting.frmCutViewRep vRep = new Cutting.frmCutViewRep(16, BIFTransit.BIFT_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                try
                {
                    frmReprintBIFTransfer Reprint = new frmReprintBIFTransfer(true);
                    Reprint.ShowDialog(this);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                frmReprintBIFTransfer Reprint = new frmReprintBIFTransfer(false);
                Reprint.ShowDialog(this);
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
