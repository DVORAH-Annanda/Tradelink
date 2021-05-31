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
    public partial class frmTransferConfirm : Form
    {
        // DataGridView1
        //-------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtA;  // 0  FK
        DataGridViewCheckBoxColumn oChkA; // 1  Select
        DataGridViewTextBoxColumn oTxtB;  // 2  Picking List 
        DataGridViewTextBoxColumn oTxtC;  // 3  from Facility
        DataGridViewTextBoxColumn oTxtD;  // 4  to facility
        DataGridViewTextBoxColumn oTxtE;  // 5  Date

        // DataGridView2 
        //-----------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtBA;  //  0 FK
        DataGridViewCheckBoxColumn oChkBA; //  1 Select 
        DataGridViewTextBoxColumn oTxtBB;  //  2 Picking Slip No
        DataGridViewTextBoxColumn oTxtBC;  //  3 Box Number 
        DataGridViewTextBoxColumn oTxtBD;  //  4 Style
        DataGridViewTextBoxColumn oTxtBE;  //  5 Colour
        DataGridViewTextBoxColumn oTxtBF;  //  6 Size
        DataGridViewTextBoxColumn oTxtBG;  //  7 Boxed
        DataGridViewTextBoxColumn oTxtBH;  //  8 Grade
        //-------------------------------------------------------------------------

        bool formloaded;

        public frmTransferConfirm()
        {
            InitializeComponent();
        }

        private void frmTransferConfirm_Load(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                cmboWareHouse.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                cmboWareHouse.ValueMember = "WhStore_Id";
                cmboWareHouse.DisplayMember = "WhStore_Description";
                cmboWareHouse.SelectedValue = -1;

                cmboDepartment.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboDepartment.ValueMember = "Dep_Id";
                cmboDepartment.DisplayMember = "Dep_Description";
                cmboDepartment.SelectedValue = -1;

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.HeaderText = "Select";
                oChkA.Visible = true;
                oChkA.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.HeaderText = "Picking List";
                oTxtB.ReadOnly = true;
                oTxtB.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.HeaderText = "From";
                oTxtC.ReadOnly = true;
                oTxtC.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtC);

                oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.HeaderText = "To";
                oTxtD.ReadOnly = true;
                oTxtD.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtD);


                oTxtE = new DataGridViewTextBoxColumn();
                oTxtE.HeaderText = "Date";
                oTxtE.ReadOnly = true;
                oTxtE.ValueType = typeof(DateTime);
                dataGridView1.Columns.Add(oTxtE);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;

                oTxtBA = new DataGridViewTextBoxColumn();
                oTxtBA.Visible = false;
                oTxtBA.ValueType = typeof(int);
                dataGridView2.Columns.Add(oTxtBA);

                oChkBA = new DataGridViewCheckBoxColumn();
                oChkBA.HeaderText = "Confirm";
                oChkBA.Visible = true;
                oChkBA.ValueType = typeof(bool);
                dataGridView2.Columns.Add(oChkBA);
                
                oTxtBB = new DataGridViewTextBoxColumn();
                oTxtBB.HeaderText = "Picking List";
                oTxtBB.ReadOnly = true;
                oTxtBB.ValueType = typeof(string);
                dataGridView2.Columns.Add(oTxtBB);

                oTxtBC = new DataGridViewTextBoxColumn();
                oTxtBC.HeaderText = "Box Number";
                oTxtBC.ReadOnly = true;
                oTxtBC.ValueType = typeof(string);
                dataGridView2.Columns.Add(oTxtBC);

                oTxtBD = new DataGridViewTextBoxColumn();
                oTxtBD.HeaderText = "Style";
                oTxtBD.ReadOnly = true;
                oTxtBD.ValueType = typeof(string);
                dataGridView2.Columns.Add(oTxtBD);

                oTxtBE = new DataGridViewTextBoxColumn();
                oTxtBE.HeaderText = "Colour";
                oTxtBE.ReadOnly = true;
                oTxtBE.ValueType = typeof(string);
                dataGridView2.Columns.Add(oTxtBE);

                oTxtBF = new DataGridViewTextBoxColumn();
                oTxtBF.HeaderText = "Size";
                oTxtBF.ReadOnly = true;
                oTxtBF.ValueType = typeof(string);
                dataGridView2.Columns.Add(oTxtBF);

                oTxtBG = new DataGridViewTextBoxColumn();
                oTxtBG.HeaderText = "Boxed Qty";
                oTxtBG.ReadOnly = true;
                oTxtBG.ValueType = typeof(int);
                dataGridView2.Columns.Add(oTxtBG);

                oTxtBH = new DataGridViewTextBoxColumn();
                oTxtBH.HeaderText = "Grade";
                oTxtBH.ReadOnly = true;
                oTxtBH.ValueType = typeof(String);
                dataGridView2.Columns.Add(oTxtBH);

                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.AutoGenerateColumns = false;


            }
            formloaded = true;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int TransNumber = 0;
            int MasterPk = 0;

            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var Dept = (TLADM_Departments)cmboDepartment.SelectedItem;
                    if (Dept == null)
                    {
                        MessageBox.Show("Please select a from destination");
                        return;
                    }

                    var RecCount = dataGridView2.Rows.Cast<DataGridViewRow>().Where(x => (bool)x.Cells[1].Value == true).Count();

                    if (RecCount == 0)
                    {
                        MessageBox.Show("Please select a least one record to process");
                        return;
                    }

                    
                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    if (LNU != null)
                    {
                            TransNumber = LNU.col8;
                    }

                    var Whse = (TLADM_WhseStore)cmboWareHouse.SelectedItem;
                    if (Whse != null)
                    {

                    }

                    bool First = true;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                            bool isChecked = (bool)row.Cells[1].Value;
                            if (!isChecked)
                                continue;

                            if (First)
                            {
                                MasterPk = (int)row.Cells[0].Value;
                                First = !First;
                            }

                            if (!chkSelectHistory.Checked)
                            {
                                TLCSV_BoxSelected Box = new TLCSV_BoxSelected();

                                var index = (int)row.Cells[0].Value;
                                Box = context.TLCSV_BoxSelected.Find(index);
                                if (Box != null)
                                {
                                    Box.TLCSV_Despatched = true;
                                    Box.TLCSV_DespatchedDate = DateTime.Now;
                                    Box.TLCSV_DNDeails = "CDN" + TransNumber.ToString().PadLeft(5, '0');
                                    Box.TLCSV_DNTransNumber = TransNumber;
                                }
                            }
                    }

                    foreach (DataGridViewRow rows in dataGridView2.Rows)
                    {
                        bool isChecked = (bool)rows.Cells[1].Value;

                        if (!isChecked)
                            continue;

                        if (!chkSelectHistory.Checked)
                        {
                            var index = (int)rows.Cells[0].Value;
                            TLCMT_CompletedWork comWork = context.TLCMT_CompletedWork.Find(index);
                            if (comWork != null)
                            {
                                if (!isChecked)
                                {
                                    comWork.TLCMTWC_Picked = false;
                                    comWork.TLCMTWC_PickList_FK = null;
                                }
                                else
                                {
                                    comWork.TLCMTWC_Despatched = true;
                                    comWork.TLCMTWC_DepatchedList_FK = MasterPk;
                                }
                            }
                        }
                    }


                    try
                    {
                        if (!chkSelectHistory.Checked)
                        {
                            LNU.col8 += 1;
                            context.SaveChanges();
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                var index = (int)row.Cells[0].Value;
                                var cnt = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_PickList_FK == index && x.TLCMTWC_Picked).Count();
                                if (cnt == 0)
                                {
                                    TLCSV_BoxSelected Box = new TLCSV_BoxSelected();
                                    Box = context.TLCSV_BoxSelected.Find(index);
                                    if (Box != null)
                                    {
                                        context.TLCSV_BoxSelected.Remove(Box);
                                    }
                                }

                            }

                            context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                        }
                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows.Clear();
                   

                        frmCSViewRep vRep = new frmCSViewRep(2, MasterPk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }
                        if (chkSelectHistory.Checked)
                            chkSelectHistory.Checked = false;

                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show(ex.Message);
                    }
               }
           }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var oDgv = sender as DataGridView;
             var oDgv2 = dataGridView2;

             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                  bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                  if (isChecked)
                  {
                      var CurrentRow = dataGridView1.CurrentRow;

                      using (var context = new TTI2Entities())
                      {
                          var index = (int)CurrentRow.Cells[0].Value;
                          var Existing = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_PickList_FK == index).ToList();

                          foreach (var Record in Existing)
                          {
                              var index1 = dataGridView2.Rows.Add();
                              dataGridView2.Rows[index1].Cells[0].Value = Record.TLCMTWC_Pk;
                              dataGridView2.Rows[index1].Cells[1].Value = true;
                              dataGridView2.Rows[index1].Cells[2].Value = CurrentRow.Cells[2].Value;
                              dataGridView2.Rows[index1].Cells[3].Value = Record.TLCMTWC_BoxNumber;
                              dataGridView2.Rows[index1].Cells[4].Value = context.TLADM_Styles.Find(Record.TLCMTWC_Style_FK).Sty_Description;
                              dataGridView2.Rows[index1].Cells[5].Value = context.TLADM_Colours.Find(Record.TLCMTWC_Colour_FK).Col_Description;
                              dataGridView2.Rows[index1].Cells[6].Value = context.TLADM_Sizes.Find(Record.TLCMTWC_Size_FK).SI_Description;
                              dataGridView2.Rows[index1].Cells[7].Value = Record.TLCMTWC_Qty;
                              dataGridView2.Rows[index1].Cells[8].Value = Record.TLCMTWC_Grade;
                          }
                      }
                  }
             }
        }

        private void cmboWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLCSV_BoxSelected> BoxesSelected = null;

            if (oCmbo != null && formloaded)
            {
                var Depts = (TLADM_Departments)cmboDepartment.SelectedItem;
                if (Depts != null)
                {
                    var selected = (TLADM_WhseStore)oCmbo.SelectedItem;
                    if (selected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            if (!chkSelectHistory.Checked)
                            {
                                BoxesSelected = context.TLCSV_BoxSelected.Where(x => x.TLCSV_PickingList && !x.TLCSV_Despatched && x.TLCSV_From_FK == Depts.Dep_Id && x.TLCSV_To_FK == selected.WhStore_Id).ToList();
                            }
                            else
                            {
                                DateTime History = DateTime.Now.AddMonths(-3);
                                BoxesSelected = context.TLCSV_BoxSelected.Where(x => x.TLCSV_PickingList && x.TLCSV_Despatched && x.TLCSV_From_FK == Depts.Dep_Id && x.TLCSV_To_FK == selected.WhStore_Id && x.TLCSV_DespatchedDate >= History).ToList();
                            }
                            if (BoxesSelected.Count == 0)
                            {
                                MessageBox.Show("There were no records pertaining to selection made");
                            }
                            else
                            {
                                foreach (var Box in BoxesSelected)
                                {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = Box.TLCSV_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = true;
                                    dataGridView1.Rows[index].Cells[2].Value = Box.TLCSV_PLDetails;
                                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Departments.Find(Box.TLCSV_From_FK).Dep_Description;
                                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_WhseStore.Find(Box.TLCSV_To_FK).WhStore_Description;
                                    DateTime dt = (DateTime)Box.TLCSV_PLTransDate;
                                    dataGridView1.Rows[index].Cells[5].Value = dt.ToShortDateString();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a to destination");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a from destintation");

                }
  
            }
        }

        private void cmboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var oDgv = sender as DataGridView;
             
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                 if (!isChecked)
                 {

                     using (var context = new TTI2Entities())
                     {
                         var index = (int)oDgv.CurrentRow.Cells[0].Value;
                         TLCMT_CompletedWork comWork = context.TLCMT_CompletedWork.Find(index);
                         if (comWork != null)
                         {
                             comWork.TLCMTWC_Picked = false;
                             comWork.TLCMTWC_PickList_FK = null;

                             try
                             {
                                 context.SaveChanges();
                             }
                             catch (Exception ex)
                             {
                                 MessageBox.Show(ex.Message);
                             }
                         }
                     }

                     var Rindex = oDgv.CurrentRow.Index;
                     dataGridView2.Rows.RemoveAt(Rindex);
                 }
             }
        }
    }
}
