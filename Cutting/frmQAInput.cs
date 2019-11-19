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
    public partial class frmQAInput : Form
    {
        int _Bundle;
        int _Inspector;
        Util core;

        bool formloaded;

        IList<TLADM_CutMeasureArea> MeasureArea;
        IList<TLADM_CutMeasureStandards> MeasureStandards;
        TLCUT_CutMeasureActuals CutMeasureActual = null;
        BindingList<KeyValuePair<int, string>> reportOptions = new BindingList<KeyValuePair<int, string>>();

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // 0 - pk of storage file
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // 1 - Bundle Pk                                          1
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // 2 - inspector pk     
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // 3 - measure stds pk TOP / MIDDLE / BOTTOM
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // 4 - Col1 Data
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // 5 - Col2 Data
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // 6 - Col3 Data
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // 7 - Col4 Data
        
        public frmQAInput(int Bundle, int Inspector)
        {
            InitializeComponent();
            core = new Util();

            _Bundle = Bundle;
            _Inspector = Inspector;

            radPPS.Enabled = false;
        }

        private void frmQAInput_Load(object sender, EventArgs e)
        {
            formloaded = false;

            oTxtA.ValueType = typeof(int);       // 0 pk of the storage file
            oTxtA.Visible = false;
            oTxtB.ValueType = typeof(int);       // 1 Column 1  
            oTxtB.Visible = false;
            oTxtC.ValueType = typeof(int);       // 2 Column 2
            oTxtC.Visible = false;
            oTxtD.ValueType = typeof(int);       // 3 Column 3
            oTxtD.Visible = false;
            oTxtE.ValueType = typeof(decimal);   // 4 Column 4
            oTxtF.ValueType = typeof(decimal);   // 5 Column 5
            oTxtG.ValueType = typeof(decimal);   // 6 Column 6
            oTxtH.ValueType = typeof(decimal);   // 7 Column 7
           

            reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Top"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Middle"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Bottom"));
            cmboMeasureArea.DataSource = reportOptions;
            cmboMeasureArea.ValueMember = "Key";
            cmboMeasureArea.DisplayMember = "Value";
            cmboMeasureArea.SelectedIndex = -1;

         

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_CutAreaLocations.OrderBy(x => x.TLCUTAL_Pk).ToList();
               
                dataGridView1.Columns.Add(oTxtA);
                dataGridView1.Columns.Add(oTxtB);
                dataGridView1.Columns.Add(oTxtC);
                dataGridView1.Columns.Add(oTxtD);
                dataGridView1.Columns.Add(oTxtE);
                dataGridView1.Columns.Add(oTxtF);
                dataGridView1.Columns.Add(oTxtG);
                dataGridView1.Columns.Add(oTxtH);

                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = null;
                dataGridView1.Rows[index].Cells[1].Value = _Bundle;
                dataGridView1.Rows[index].Cells[2].Value = _Inspector;
                dataGridView1.Rows[index].Cells[3].Value = null;
                dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                dataGridView1.Rows[index].Cells[7].Value = 0.00M;

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;

                var CSRecDet = context.TLCUT_CutSheetReceiptDetail.Find(_Bundle);
                if (CSRecDet != null)
                {
                    var CSRec = context.TLCUT_CutSheetReceipt.Find(CSRecDet.TLCUTSHRD_CutSheet_FK);
                    if (CSRec != null)
                    {
                        txtSize.Text = context.TLADM_Sizes.Find(CSRecDet.TLCUTSHRD_Size_FK).SI_Description;
                        txtBundleDescription.Text = context.TLCUT_CutSheetReceiptDetail.Find(_Bundle).TLCUTSHRD_Description;
                        txtInspector.Text = context.TLADM_MachineOperators.Find(_Inspector).MachOp_Description;

                        MeasureArea = context.TLADM_CutMeasureArea.OrderBy(x => x.TLCUTA_Pk).ToList();

                        MeasureStandards = context.TLADM_CutMeasureStandards.Where(x=>x.TLCUTAS_Style_FK == CSRec.TLCUTSHR_Style_FK && x.TLCUTAS_Size_FK == CSRecDet.TLCUTSHRD_Size_FK).ToList();
                        if (MeasureStandards.Count == 0)
                        {
                            string Style = context.TLADM_Styles.Find(CSRec.TLCUTSHR_Style_FK).Sty_Description;
                            string Size = context.TLADM_Sizes.Find(CSRecDet.TLCUTSHRD_Size_FK).SI_Description;

                            MessageBox.Show("No measurement standards have been defined for this style " + Style + " and size " + Size + " combination");
                            this.Close();
                        }
                    }
                }

                CutMeasureActual = context.TLCUT_CutMeasureActuals.Where(x => x.TLCUTM_Bundle_FK == _Bundle).FirstOrDefault();

              
                
                formloaded = true;
                RadPanelSize.Checked = true;
            }
        }

        private void cmboMeasureArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = Convert.ToInt32(cmboMeasureArea.SelectedValue);
                if (selected != 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();

                        var Existing = context.TLADM_CutAreaLocations.Where(x => !x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                        int Cnt = 0;
                        foreach (var Row in Existing)
                        {
                            Cnt += 1;
                            if (Cnt == 1)
                            {
                                oTxtE.HeaderText = Row.TLCUTAL_Description;
                            }
                            else if (Cnt == 2)
                            {
                                oTxtF.HeaderText = Row.TLCUTAL_Description;
                            }
                            else if (Cnt == 3)
                            {
                                oTxtG.HeaderText = Row.TLCUTAL_Description;
                            }
                            else
                            {
                                oTxtH.HeaderText = Row.TLCUTAL_Description;
                            }
                        }

                        var QActual = context.TLCUT_QAResults.Where(x => x.TLCUTQA_Bundle_FK == _Bundle && !x.TLCUTQA_PPS && x.TLCUTQA_MeasureArea_FK == selected).FirstOrDefault();
                        if (QActual != null)
                        {
                            dataGridView1.Rows.Clear();
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = QActual.TLCUTQA_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = QActual.TLCUTQA_Bundle_FK;
                            dataGridView1.Rows[index].Cells[2].Value = QActual.TLCUTQA_Inspectore_FK;
                            dataGridView1.Rows[index].Cells[3].Value = QActual.TLCUTQA_MeasureArea_FK;
                            dataGridView1.Rows[index].Cells[4].Value = QActual.TLCUTQA_Col1;
                            dataGridView1.Rows[index].Cells[5].Value = QActual.TLCUTQA_Col2;
                            dataGridView1.Rows[index].Cells[6].Value = QActual.TLCUTQA_Col3;
                            dataGridView1.Rows[index].Cells[7].Value = QActual.TLCUTQA_Col4;
                            dtpTransDate.Value = QActual.TLCUTQA_Date;

                            

                        }
                        else
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = null;
                            dataGridView1.Rows[index].Cells[1].Value = _Bundle;
                            dataGridView1.Rows[index].Cells[2].Value = _Inspector;
                            dataGridView1.Rows[index].Cells[3].Value = null;
                            dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                            dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                            dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                            dataGridView1.Rows[index].Cells[7].Value = 0.00M;
                          
                        } 
                        
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int MeasureA = 0;
            if (oBtn != null && formloaded)
            {
                if (RadPanelSize.Checked)
                {
                    if(cmboMeasureArea.SelectedValue != null)
                        MeasureA = Convert.ToInt32(cmboMeasureArea.SelectedValue.ToString());
                    else
                    {
                        MessageBox.Show("Please select an measurement area");
                        return;
                    }

                }
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        TLCUT_QAResults qaRes = new TLCUT_QAResults();
                        bool Add = true;
                        if (row.Cells[0].Value != null)
                        {
                            var index = (int)row.Cells[0].Value;
                            qaRes = context.TLCUT_QAResults.Find(index);
                            if (qaRes == null)
                                qaRes = new TLCUT_QAResults();
                            else
                                Add = false;
                        }

                        qaRes.TLCUTQA_Bundle_FK = (int)row.Cells[1].Value;
                        qaRes.TLCUTQA_Inspectore_FK = (int)row.Cells[2].Value;
                        qaRes.TLCUTQA_Date = dtpTransDate.Value;
                        qaRes.TLCUTQA_Col1 = (decimal)row.Cells[4].Value;
                        qaRes.TLCUTQA_Col2 = (decimal)row.Cells[5].Value;
                        qaRes.TLCUTQA_Col3 = (decimal)row.Cells[6].Value;
                        qaRes.TLCUTQA_Col4 = (decimal)row.Cells[7].Value;
                        
                        if(RadPanelSize.Checked && MeasureA > 0)
                           qaRes.TLCUTQA_MeasureArea_FK = MeasureA;
                        if (radPPS.Checked)
                            qaRes.TLCUTQA_PPS = true;

                        if(Add)
                            context.TLCUT_QAResults.Add(qaRes);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        dataGridView1.Rows.Clear();
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

        private void radPPS_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    dataGridView1.Rows.Clear();

                    var Existing = context.TLADM_CutAreaLocations.Where(x => x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                    int Cnt = 0;
                    foreach (var Row in Existing)
                    {
                        Cnt += 1;
                        if (Cnt == 1)
                        {
                            oTxtE.HeaderText = Row.TLCUTAL_Description;
                        }
                        else if (Cnt == 2)
                        {
                            oTxtF.HeaderText = Row.TLCUTAL_Description;
                        }
                        else if (Cnt == 3)
                        {
                            oTxtG.HeaderText = Row.TLCUTAL_Description;
                        }
                        else
                        {
                            oTxtH.HeaderText = Row.TLCUTAL_Description;
                        }
                    }

                    var QActual = context.TLCUT_QAResults.Where(x => x.TLCUTQA_Bundle_FK == _Bundle && x.TLCUTQA_PPS).FirstOrDefault();
                    if (QActual != null)
                    {
                        dataGridView1.Rows.Clear();
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = QActual.TLCUTQA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = QActual.TLCUTQA_Bundle_FK;
                        dataGridView1.Rows[index].Cells[2].Value = QActual.TLCUTQA_Inspectore_FK;
                        dataGridView1.Rows[index].Cells[3].Value = QActual.TLCUTQA_MeasureArea_FK;
                        dataGridView1.Rows[index].Cells[4].Value = QActual.TLCUTQA_Col1;
                        dataGridView1.Rows[index].Cells[5].Value = QActual.TLCUTQA_Col2;
                        dataGridView1.Rows[index].Cells[6].Value = QActual.TLCUTQA_Col3;
                        dataGridView1.Rows[index].Cells[7].Value = QActual.TLCUTQA_Col4;
                        dtpTransDate.Value = QActual.TLCUTQA_Date;

                        if (QActual.TLCUTQA_MeasureArea_FK > 0)
                            cmboMeasureArea.SelectedValue = QActual.TLCUTQA_MeasureArea_FK;
                        else
                            cmboMeasureArea.SelectedValue = -1;
                    }
                    else
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = null;
                        dataGridView1.Rows[index].Cells[1].Value = _Bundle;
                        dataGridView1.Rows[index].Cells[2].Value = _Inspector;
                        dataGridView1.Rows[index].Cells[3].Value = null;
                        dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[7].Value = 0.00M;
                        cmboMeasureArea.SelectedValue = -1;
                    }
                
                }
            }
        }

        private void RadPanelSize_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    dataGridView1.Rows.Clear();

                    var Existing = context.TLADM_CutAreaLocations.Where(x=>!x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                    int Cnt = 0;
                    foreach (var Row in Existing)
                    {
                        Cnt += 1; 
                        if (Cnt == 1)
                        {
                            oTxtE.HeaderText = Row.TLCUTAL_Description;
                        }
                        else if (Cnt == 2)
                        {
                            oTxtF.HeaderText = Row.TLCUTAL_Description;
                        }
                        else if (Cnt == 3)
                        {
                            oTxtG.HeaderText = Row.TLCUTAL_Description;
                        }
                        else
                        {
                            oTxtH.HeaderText = Row.TLCUTAL_Description;
                        }
                    }

                    var QActual = context.TLCUT_QAResults.Where(x => x.TLCUTQA_Bundle_FK == _Bundle && !x.TLCUTQA_PPS && x.TLCUTQA_MeasureArea_FK == 1).FirstOrDefault();
                    if (QActual != null)
                    {
                        dataGridView1.Rows.Clear();
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = QActual.TLCUTQA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = QActual.TLCUTQA_Bundle_FK;
                        dataGridView1.Rows[index].Cells[2].Value = QActual.TLCUTQA_Inspectore_FK;
                        dataGridView1.Rows[index].Cells[3].Value = QActual.TLCUTQA_MeasureArea_FK;
                        dataGridView1.Rows[index].Cells[4].Value = QActual.TLCUTQA_Col1;
                        dataGridView1.Rows[index].Cells[5].Value = QActual.TLCUTQA_Col2;
                        dataGridView1.Rows[index].Cells[6].Value = QActual.TLCUTQA_Col3;
                        dataGridView1.Rows[index].Cells[7].Value = QActual.TLCUTQA_Col4;
                        dtpTransDate.Value = QActual.TLCUTQA_Date;

                        if (QActual.TLCUTQA_MeasureArea_FK > 0)
                            cmboMeasureArea.SelectedValue = QActual.TLCUTQA_MeasureArea_FK;
                        else
                            cmboMeasureArea.SelectedValue = -1;

                    }
                    else
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = null;
                        dataGridView1.Rows[index].Cells[1].Value = _Bundle;
                        dataGridView1.Rows[index].Cells[2].Value = _Inspector;
                        dataGridView1.Rows[index].Cells[3].Value = null;
                        dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                        dataGridView1.Rows[index].Cells[7].Value = 0.00M;
                        cmboMeasureArea.SelectedValue = -1;
                    }

                 
                }
            }
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

                    if (radPPS.Checked)
                    {
                        if (Cell.ColumnIndex == 4 ||
                            Cell.ColumnIndex == 5 ||
                            Cell.ColumnIndex == 6 ||
                            Cell.ColumnIndex == 7)
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
                    else
                    {
                        if (Cell.ColumnIndex == 4 ||
                            Cell.ColumnIndex == 5 ||
                            Cell.ColumnIndex == 6 ||
                            Cell.ColumnIndex == 7)
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
        }
    }
}
