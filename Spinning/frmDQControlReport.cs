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
namespace Spinning
{
    public partial class frmDQControlReport : Form
    {
        bool formloaded;
        bool[] WhichScreen;   // Pos 0   Card Measurements 
                              // Pos 1   RSB Measurements 
                              // Pos 2   Spinning Machnies Measurements 
        //================================================
        // Card Measureemnts 
        //=============================================================
        DataGridViewTextBoxColumn oIndex;
        DataGridViewTextBoxColumn oTextA;
        DataGridViewTextBoxColumn oTextB;
        DataGridViewTextBoxColumn oTextC;
        DataGridViewTextBoxColumn oTextD;
        DataGridViewTextBoxColumn oTextE;
        DataGridViewTextBoxColumn oTextF;
        //======================================================
        // RSB Measurements 
        //===================================================================
        DataGridViewTextBoxColumn oIndex2;   // Index position 
        DataGridViewComboBoxColumn oCmboA;    // Day / Night Shift
        DataGridViewTextBoxColumn oTextB2;   // Can Number
        DataGridViewTextBoxColumn oTextC2;   // Results  

        //==========================================================
        // Spinning Machine Measurements 
        //====================================================================
        DataGridViewTextBoxColumn oIndex1;
        DataGridViewTextBoxColumn oTextA1;
        DataGridViewTextBoxColumn oTextB1;
        DataGridViewTextBoxColumn oTextC1;
        DataGridViewTextBoxColumn oTextD1;
        DataGridViewTextBoxColumn oTextE1;
        DataGridViewTextBoxColumn oTextF1;
        DataGridViewTextBoxColumn oTextG1;
        Util core;
        IList<TLSPN_YarnOrder> YarnOrder = null;

        public frmDQControlReport()
        {
            InitializeComponent();
            core = new Util();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var deptdetails = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                if (deptdetails != null)
                {
                    cmboCMMachines.DataSource = context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == deptdetails.Dep_Id).OrderBy(x => x.MD_MachineCode).ToList();
                    cmboCMMachines.ValueMember = "MD_Pk";
                    cmboCMMachines.DisplayMember = "MD_MachineCode";
                    cmboCMMachines.SelectedValue = 0;

                    cmboRSBMachines.BindingContext = new System.Windows.Forms.BindingContext();
                    cmboRSBMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == deptdetails.Dep_Id).OrderBy(x => x.MD_MachineCode).ToList();
                    cmboRSBMachines.ValueMember = "MD_Pk";
                    cmboRSBMachines.DisplayMember = "MD_MachineCode";
                    cmboRSBMachines.SelectedValue = 0;

                    cmboSPMachines.BindingContext = new System.Windows.Forms.BindingContext();
                    cmboSPMachines.DataSource = context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == deptdetails.Dep_Id).OrderBy(x => x.MD_MachineCode).ToList();
                    cmboSPMachines.ValueMember = "MD_Pk";
                    cmboSPMachines.DisplayMember = "MD_MachineCode";
                    cmboSPMachines.SelectedValue = 0;
                    /*
                    cmbYarnOrder.DataSource = context.TLSPN_YarnOrder.Where(x => !x.Yarno_Closed).OrderBy(x => x.YarnO_OrderNumber).ToList();
                    cmbYarnOrder.DisplayMember = "YarnO_OrderNumber";
                    cmbYarnOrder.ValueMember = "YarnO_Pk";
                     */
                }
            }

          
            WhichScreen = core.PopulateArray(3, false);

            oIndex = new DataGridViewTextBoxColumn();
            oIndex.Visible = false;
            oIndex.ReadOnly = true;

            // Card Measurements 
            //=====================================================
            oTextA = new DataGridViewTextBoxColumn();
            oTextA.HeaderText = "Tests";
            oTextA.ReadOnly = true;
            oTextB = new DataGridViewTextBoxColumn();
            oTextB.HeaderText = "Daily Avg";
            oTextB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextC = new DataGridViewTextBoxColumn();
            oTextC.HeaderText = "CV %";
            oTextC.ReadOnly = false;
            oTextC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextD = new DataGridViewTextBoxColumn();
            oTextD.HeaderText = "";
            oTextD.ReadOnly = true;
            oTextD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextE = new DataGridViewTextBoxColumn();
            oTextE.HeaderText = "";
            oTextE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextE.ReadOnly = true;
            oTextF = new DataGridViewTextBoxColumn();
            oTextF.HeaderText = "";
            oTextF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextF.ReadOnly = true;

            //RSB Measurements
            //===========================================================
            oIndex2 = new DataGridViewTextBoxColumn();  //0
            oIndex2.Visible = false;
            oIndex2.ReadOnly = true;
            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Shifts";
            oTextB2 = new DataGridViewTextBoxColumn();
            oTextB2.HeaderText = "Can Number";
            oTextB2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTextC2 = new DataGridViewTextBoxColumn();
            oTextC2.HeaderText = "Results";
            oTextC2.ReadOnly = false;
            oTextC2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Spinning Machines Measurements
            //===========================================================
            oIndex1 = new DataGridViewTextBoxColumn();  //0
            oIndex1.Visible = false;
            oIndex1.ReadOnly = true;
            oTextA1 = new DataGridViewTextBoxColumn();  //1
            oTextA1.HeaderText = "SPDLE";
            oTextB1= new DataGridViewTextBoxColumn();   //2
            oTextB1.HeaderText = "COUNT";
            oTextC1 = new DataGridViewTextBoxColumn();  //3
            oTextC1.HeaderText = "CV %";
            oTextD1 = new DataGridViewTextBoxColumn();  //4
            oTextD1.HeaderText = "THIN";
            oTextE1 = new DataGridViewTextBoxColumn();  //5
            oTextE1.HeaderText = "THICK";
            oTextF1 = new DataGridViewTextBoxColumn();  //6
            oTextF1.HeaderText = "NEP";
            oTextG1 = new DataGridViewTextBoxColumn();  //7
            oTextG1.HeaderText = "IPI";

            dataGridView1.Columns.Add(oIndex);
            dataGridView1.Columns.Add(oTextA);
            dataGridView1.Columns.Add(oTextB);
            dataGridView1.Columns.Add(oTextC);
            dataGridView1.Columns.Add(oTextD);
            dataGridView1.Columns.Add(oTextE);
            dataGridView1.Columns.Add(oTextF);

            SetUpTopScreen();


            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            dataGridView2.Columns.Add(oIndex1);  //0
            dataGridView2.Columns.Add(oTextA1);  //1
            dataGridView2.Columns.Add(oTextB1);  //2
            dataGridView2.Columns.Add(oTextC1);  //3
            dataGridView2.Columns.Add(oTextD1);  //4
            dataGridView2.Columns.Add(oTextE1);  //5
            dataGridView2.Columns.Add(oTextF1);  //6
            dataGridView2.Columns.Add(oTextG1);  //7



            RSBdataGridView3.Columns.Add(oIndex2);  // 0
            RSBdataGridView3.Columns.Add(oCmboA);   // 1
            RSBdataGridView3.Columns.Add(oTextB2);  // 2
            RSBdataGridView3.Columns.Add(oTextC2);  // 3

            SetUpBottomScreen();

            RSBdataGridView3.AllowUserToAddRows = true;
            RSBdataGridView3.AllowUserToOrderColumns = false;

           //  dataGridView2.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            dataGridView2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView2_EditingControlShowing);
            RSBdataGridView3.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView3_EditingControlShowing);
            formloaded = true;
        }

        void SetUpTopScreen()
        {
            /*
            var index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[1].Value = "Test 1";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
            index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[1].Value = "Test 2";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
            index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[1].Value = "Test 3";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
            index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[1].Value = "Test 4";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
            index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[1].Value = "Test 5";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
             */ 
            var index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = 0;
            dataGridView1.Rows[index].Cells[1].Value = "Avg ";
            dataGridView1.Rows[index].Cells[2].Value = "0";
            dataGridView1.Rows[index].Cells[3].Value = "0";
            dataGridView1.Rows[index].Cells[4].Value = "0";
            dataGridView1.Rows[index].Cells[5].Value = "0";
            dataGridView1.Rows[index].Cells[6].Value = "0";
         
          
        }

        void SetUpBottomScreen()
        {
            /*
            var index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[1].Value = "55";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
            index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[1].Value = "56";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
            index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[1].Value = "57";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
            index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[1].Value = "58";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
            index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[1].Value = "59";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
             */ 
            var index = dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[0].Value = 0;
            dataGridView2.Rows[index].Cells[1].Value = "Avg ";
            dataGridView2.Rows[index].Cells[2].Value = "0.00";
            dataGridView2.Rows[index].Cells[3].Value = "0.00";
            dataGridView2.Rows[index].Cells[4].Value = "0";
            dataGridView2.Rows[index].Cells[5].Value = "0";
            dataGridView2.Rows[index].Cells[6].Value = "0";
            dataGridView2.Rows[index].Cells[7].Value = "0";

            var index1 = RSBdataGridView3.Rows.Add();
            RSBdataGridView3.Rows[index1].Cells[0].Value = 0;
            RSBdataGridView3.Rows[index1].Cells[1].Value = 1;
            RSBdataGridView3.Rows[index1].Cells[2].Value = "0";
            RSBdataGridView3.Rows[index1].Cells[3].Value = "0";

            var ShiftOptions = new BindingList<KeyValuePair<int, string>>();

            ShiftOptions.Add(new KeyValuePair<int, string>(1, "Day Shift"));
            ShiftOptions.Add(new KeyValuePair<int, string>(2, "Night Shift"));
   
            oCmboA.DataSource = ShiftOptions;
            oCmboA.ValueMember = "Key";
            oCmboA.DisplayMember = "Value";
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            try
            {
                if (oDgv != null)
                {
                    /*
                    try
                    {
                        dataGridView2.Rows[oDgv.NewRowIndex].Cells[2].Value = "0";
                        dataGridView2.Rows[oDgv.NewRowIndex].Cells[3].Value = "0.0";
                        dataGridView2.Rows[oDgv.NewRowIndex].Cells[4].Value = "0.0";
                        dataGridView2.Rows[oDgv.NewRowIndex].Cells[5].Value = "0";
                        dataGridView2.Rows[oDgv.NewRowIndex].Cells[6].Value = "0";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                     */ 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var currentCell = oDgv.CurrentCell;
               

                if (currentCell.ColumnIndex == 2 || 
                    currentCell.ColumnIndex == 3 ||
                    currentCell.ColumnIndex == 4 ||
                    currentCell.ColumnIndex == 5 ||
                    currentCell.ColumnIndex == 6)
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

        void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var currentCell = oDgv.CurrentCell;
                if (currentCell.ColumnIndex == 1 ||
                    currentCell.ColumnIndex == 5 ||
                    currentCell.ColumnIndex == 6 ||
                    currentCell.ColumnIndex == 7)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else if (
                    currentCell.ColumnIndex == 2 || currentCell.ColumnIndex == 3 ||
                         currentCell.ColumnIndex == 4)
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
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                }
            }
        }

        void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                var currentCell = oDgv.CurrentCell;
                
                if (currentCell.ColumnIndex == 2 || currentCell.ColumnIndex == 3 )
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                }
            }
        }

        private void cmboMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            if (oCombo != null && formloaded)
            {
                var selected = (TLADM_MachineDefinitions)cmboCMMachines.SelectedItem;
                using (var context = new TTI2Entities())
                {
                     dataGridView1.Rows.Clear();
                   

                    DateTime dt = Convert.ToDateTime(dtpReportDate.Value.ToShortDateString());

                    var Existing = context.TLSPN_QAMeasurements.Where(x=>x.YarnQA_Date == dt && x.YarnQA_MeasureNo == 1
                                                                       && x.YarnQA_Date == dt
                                                                       && x.YarnQA_MachineNo_FK == selected.MD_Pk).ToList();
                    foreach (var rw in Existing)
                    {
                        var RowNo = dataGridView1.Rows.Add();
                        dataGridView1.Rows[RowNo].Cells[0].Value = rw.YarnQA_Pk;
                        dataGridView1.Rows[RowNo].Cells[1].Value = "Avg " + rw.YarnQA_TestNo.ToString();
                        dataGridView1.Rows[RowNo].Cells[2].Value = rw.YarnQA_08H00;
                        dataGridView1.Rows[RowNo].Cells[3].Value = Math.Round(rw.YarnQA_10H00,2);
                        dataGridView1.Rows[RowNo].Cells[4].Value = Math.Round(rw.YarnQA_12H00,0);
                        dataGridView1.Rows[RowNo].Cells[5].Value = rw.YarnQA_14H00;
                        dataGridView1.Rows[RowNo].Cells[6].Value = rw.YarnQA_16H00;
                       
                    }
                   
                    if (Existing.Count == 0)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = 0;
                        dataGridView1.Rows[index].Cells[1].Value = "Avg ";
                        dataGridView1.Rows[index].Cells[2].Value = "0";
                        dataGridView1.Rows[index].Cells[3].Value = "0";
                        dataGridView1.Rows[index].Cells[4].Value = "0";
                        dataGridView1.Rows[index].Cells[5].Value = "0";
                        dataGridView1.Rows[index].Cells[6].Value = "0";
                       
                    }

                    WhichScreen = core.PopulateArray(3, false);
                    WhichScreen[0] = true;
             

                }
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            
 
            if (oBtn != null && formloaded)
            {
                // Card Measurements 
                //===============================================
                if (WhichScreen[0] == true)
                {
                    var machine = (TLADM_MachineDefinitions)cmboCMMachines.SelectedItem;
                    if (machine == null)
                    {
                        MessageBox.Show("Please select a machine from the top drop down List");
                        return;
                    }
                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            TLSPN_QAMeasurements QA = new TLSPN_QAMeasurements();
                            if ((int)dr.Cells[0].Value != 0)
                            {
                                QA = context.TLSPN_QAMeasurements.Find(Convert.ToInt32(dr.Cells[0].Value.ToString()));
                            }

                            QA.YarnQA_Date = dtpReportDate.Value;
                            QA.YarnQA_MachineNo_FK = machine.MD_Pk;
                            QA.YarnQA_MeasureNo = 1;
                            QA.YarnQA_TestNo = dr.Cells[0].RowIndex + 1;
                            QA.YarnQA_08H00 = Convert.ToDecimal(dr.Cells[2].Value.ToString());
                            QA.YarnQA_10H00 = Convert.ToDecimal(dr.Cells[3].Value.ToString());
                            QA.YarnQA_12H00 = Convert.ToInt32(dr.Cells[4].Value.ToString());
                            QA.YarnQA_14H00 = Convert.ToInt32(dr.Cells[5].Value.ToString());
                            QA.YarnQA_16H00 = Convert.ToInt32(dr.Cells[6].Value.ToString());

                            if ((int)dr.Cells[0].Value == 0)
                                context.TLSPN_QAMeasurements.Add(QA);

                            try
                            {
                                context.SaveChanges();
                                formloaded = false;
                                cmboCMMachines.SelectedIndex = -1;
                                formloaded = true;
                                MessageBox.Show("Data saved to database successfully");
                                WhichScreen = core.PopulateArray(3, false);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else if (WhichScreen[1] == true)
                {
                    //================================
                    // RSB Measurements 
                    //================================================
                    var machine = (TLADM_MachineDefinitions)cmboRSBMachines.SelectedItem;
                    if (machine == null)
                    {
                        MessageBox.Show("Please select a machine from the top drop down List");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow dr in RSBdataGridView3.Rows)
                        {
                            if (dr.Cells[1].Value == null)
                                continue;

                            bool Add = true;

                            TLSPN_QAMeasurements QA = new TLSPN_QAMeasurements();
                            if ( dr.Cells[0].Value != null && Convert.ToInt32(dr.Cells[0].Value.ToString()) != 0)
                            {
                                Add = false;
                                QA = context.TLSPN_QAMeasurements.Find(Convert.ToInt32(dr.Cells[0].Value.ToString()));
                            }

                            QA.YarnQA_Date = dtpReportDate.Value;
                            QA.YarnQA_MachineNo_FK = machine.MD_Pk;
                            QA.YarnQA_MeasureNo = 3;
                            QA.YarnQA_TestNo = Convert.ToInt32(dr.Cells[1].Value.ToString()); ;
                            QA.YarnQA_08H00 = 0.00M;
                            QA.YarnQA_10H00 = Convert.ToDecimal(dr.Cells[2].Value.ToString());
                            QA.YarnQA_12H00 = Convert.ToDecimal(dr.Cells[3].Value.ToString());
                            
                            if (Add)
                                context.TLSPN_QAMeasurements.Add(QA);
                        }

                        try
                        {
                            context.SaveChanges();
                            formloaded = false;
                            cmboRSBMachines.SelectedIndex = -1;
                            formloaded = true;
                            MessageBox.Show("Data saved to database successfully");
                            WhichScreen = core.PopulateArray(3, false);
                            RSBdataGridView3.Rows.Clear();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
                else if (WhichScreen[2] == true)
                {
                    //==========================================
                    // Spinning Machines 
                    //===========================================================
                    var machine2 = (TLADM_MachineDefinitions)cmboSPMachines.SelectedItem;
                    if (machine2 == null)
                    {
                        MessageBox.Show("Please select a machine from the bottom drop down List");
                        return;

                    }

                    var yarno = (TLSPN_YarnOrder)cmbYarnOrder.SelectedItem;
                    if (yarno == null)
                    {
                        MessageBox.Show("Please select a yarn order from the drop down list");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow dr in dataGridView2.Rows)
                        {
                            TLSPN_QAMeasurements QA = new TLSPN_QAMeasurements();
                            if (dr.Cells[2].Value == null)
                                continue;

                            if ((int)dr.Cells[0].Value != 0)
                            {
                                QA = context.TLSPN_QAMeasurements.Find(Convert.ToInt32(dr.Cells[0].Value.ToString()));
                            }

                            QA.YarnQA_Date = dtpReportDate.Value;
                            QA.YarnQA_MachineNo_FK = machine2.MD_Pk;
                            QA.YarnQA_YarnOrder_FK = yarno.YarnO_Pk;
                            QA.YarnQA_MeasureNo = 2;
                            QA.YarnQA_TestNo = 1; //Convert.ToInt32(dr.Cells[1].Value.ToString());
                            QA.YarnQA_08H00 = Convert.ToDecimal(dr.Cells[2].Value.ToString());
                            QA.YarnQA_10H00 = Convert.ToDecimal(dr.Cells[3].Value.ToString());
                            QA.YarnQA_12H00 = Convert.ToDecimal(dr.Cells[4].Value.ToString());
                            QA.YarnQA_14H00 = Convert.ToInt32(dr.Cells[5].Value.ToString());
                            QA.YarnQA_16H00 = Convert.ToInt32(dr.Cells[6].Value.ToString());
                            QA.YarnQA_18H00 = Convert.ToInt32(dr.Cells[7].Value.ToString());

                            if ((int)dr.Cells[0].Value == 0)
                                context.TLSPN_QAMeasurements.Add(QA);

                            try
                            {
                                context.SaveChanges();
                                dataGridView2.Rows.Clear();
                                SetUpBottomScreen();
                                formloaded = false;
                                cmboSPMachines.SelectedIndex = -1;
                                cmbYarnOrder.SelectedIndex = -1;
                                dtpReportDate.Value = DateTime.Now;
                                formloaded = true;
                                MessageBox.Show("Data saved to database successfully");
                                WhichScreen = core.PopulateArray(3, false);
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

        private void cmbMachines2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            
            if (oCombo != null && formloaded)
            {
                var selected = (TLADM_MachineDefinitions)cmboSPMachines.SelectedItem;

                YarnOrder = new List<TLSPN_YarnOrder>();
                using (var context = new TTI2Entities())
                {
                     var Existing = context.TLSPN_YarnOrder.Where(x => x.Yarno_MachNo_FK == selected.MD_Pk).ToList();
                     foreach (var row in Existing)
                     {
                         YarnOrder.Add(row);
                     }

                     formloaded = false;
                     cmbYarnOrder.DataSource = YarnOrder;
                     cmbYarnOrder.DisplayMember = "YarnO_OrderNumber";
                     cmbYarnOrder.ValueMember = "YarnO_Pk";
                     cmbYarnOrder.SelectedValue = 0;
                     formloaded = true;

                     WhichScreen = core.PopulateArray(3, false);
                     WhichScreen[2] = true;
                }
                
                /*
                var selected = (TLADM_MachineDefinitions)cmbMachines2.SelectedItem;
                using (var context = new TTI2Entities())
                {
                    dataGridView2.Rows.Clear();
                 
                    DateTime dt = Convert.ToDateTime(dtpReportDate.Value.ToShortDateString());

                    var Existing = context.TLSPN_QAMeasurements.Where(x => x.YarnQA_Date == dt && x.YarnQA_MeasureNo == 2
                                                                       && x.YarnQA_MachineNo_FK == selected.MD_Pk
                                                                       && x.YarnQA_Date == dt
                                                                       && x.YarnQA_YarnOrder_FK == YO.YarnO_Pk).ToList();
                    foreach (var rw in Existing)
                    {
                        var RowNo = dataGridView2.Rows.Add();
                        dataGridView2.Rows[RowNo].Cells[0].Value = rw.YarnQA_Pk;
                        Pk = (int)rw.YarnQA_YarnOrder_FK;
                        dataGridView2.Rows[RowNo].Cells[1].Value = rw.YarnQA_TestNo.ToString();
                        dataGridView2.Rows[RowNo].Cells[2].Value = rw.YarnQA_08H00;
                        dataGridView2.Rows[RowNo].Cells[3].Value = Math.Round(rw.YarnQA_10H00,2);
                        dataGridView2.Rows[RowNo].Cells[4].Value = Math.Round(rw.YarnQA_12H00,2);
                        dataGridView2.Rows[RowNo].Cells[5].Value = rw.YarnQA_14H00;
                        dataGridView2.Rows[RowNo].Cells[6].Value = rw.YarnQA_16H00;
                        dataGridView2.Rows[RowNo].Cells[7].Value = rw.YarnQA_18H00;
                    }

                    if (Pk > 0)
                        cmbYarnOrder.SelectedValue = Pk;

                    if(Existing.Count == 0)
                        SetUpBottomScreen();
                }
                 */ 

            }
        }

        private void dtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
                dataGridView1.Rows.Clear();
                SetUpTopScreen();
                dataGridView2.Rows.Clear();
                SetUpBottomScreen();
                formloaded = false;
                cmboCMMachines.SelectedIndex = -1;
                cmboSPMachines.SelectedIndex = -1;
                cmbYarnOrder.SelectedIndex = -1;
                formloaded = true;
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                frmQAReportSelection repsel = new frmQAReportSelection();
                repsel.ShowDialog(this);
            }
        }

        private void cmbYarnOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            int Pk = 0;
            if (oCmbo != null && formloaded)
            {
                var YO = (TLSPN_YarnOrder)oCmbo.SelectedItem;

                var selected = (TLADM_MachineDefinitions)cmboSPMachines.SelectedItem;
                using (var context = new TTI2Entities())
                {
                    dataGridView2.Rows.Clear();

                    DateTime dt = Convert.ToDateTime(dtpReportDate.Value.ToShortDateString());

                    var Existing = context.TLSPN_QAMeasurements.Where(x => x.YarnQA_Date == dt && x.YarnQA_MeasureNo == 2
                                                                       && x.YarnQA_MachineNo_FK == selected.MD_Pk
                                                                       && x.YarnQA_YarnOrder_FK == YO.YarnO_Pk).ToList();
                    foreach (var rw in Existing)
                    {
                        var RowNo = dataGridView2.Rows.Add();
                        dataGridView2.Rows[RowNo].Cells[0].Value = rw.YarnQA_Pk;
                        Pk = (int)rw.YarnQA_YarnOrder_FK;
                        dataGridView2.Rows[RowNo].Cells[1].Value = rw.YarnQA_TestNo.ToString();
                        dataGridView2.Rows[RowNo].Cells[2].Value = rw.YarnQA_08H00;
                        dataGridView2.Rows[RowNo].Cells[3].Value = Math.Round(rw.YarnQA_10H00, 2);
                        dataGridView2.Rows[RowNo].Cells[4].Value = Math.Round(rw.YarnQA_12H00, 2);
                        dataGridView2.Rows[RowNo].Cells[5].Value = rw.YarnQA_14H00;
                        dataGridView2.Rows[RowNo].Cells[6].Value = rw.YarnQA_16H00;
                        dataGridView2.Rows[RowNo].Cells[7].Value = rw.YarnQA_18H00;
                    }

                    if (Pk > 0)
                        cmbYarnOrder.SelectedValue = Pk;

                    if (Existing.Count == 0)
                        SetUpBottomScreen();
                }
            }
        }

        private void cmboRSBMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            if (oCombo != null && formloaded)
            {
                var selected = (TLADM_MachineDefinitions)cmboRSBMachines.SelectedItem;
                using (var context = new TTI2Entities())
                {
                    RSBdataGridView3.Rows.Clear();


                    DateTime dt = Convert.ToDateTime(dtpReportDate.Value.ToShortDateString());

                    var Existing = context.TLSPN_QAMeasurements.Where(x => x.YarnQA_Date == dt && x.YarnQA_MeasureNo == 3
                                                                       && x.YarnQA_Date == dt
                                                                       && x.YarnQA_MachineNo_FK == selected.MD_Pk).ToList();
                    foreach (var rw in Existing)
                    {
                        try
                        {
                            var RowNo = RSBdataGridView3.Rows.Add();
                            RSBdataGridView3.Rows[RowNo].Cells[0].Value = rw.YarnQA_Pk;
                            RSBdataGridView3.Rows[RowNo].Cells[1].Value = rw.YarnQA_TestNo;  // 1 -- Day Shift 2 -- Night Shift
                            RSBdataGridView3.Rows[RowNo].Cells[2].Value = Math.Round(rw.YarnQA_10H00, 0); // Can Number --  
                            RSBdataGridView3.Rows[RowNo].Cells[3].Value = Math.Round(rw.YarnQA_12H00, 0); // Tolerance 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }

                    if (Existing.Count == 0)
                    {
                        var index = RSBdataGridView3.Rows.Add();
                        RSBdataGridView3.Rows[index].Cells[0].Value = 0; 
                        RSBdataGridView3.Rows[index].Cells[1].Value = 1;
                        RSBdataGridView3.Rows[index].Cells[2].Value = "0";
                        RSBdataGridView3.Rows[index].Cells[3].Value = "0";
                    }

                    WhichScreen = core.PopulateArray(3, false);
                    WhichScreen[1] = true;


                }

            }
        }
    }
}
