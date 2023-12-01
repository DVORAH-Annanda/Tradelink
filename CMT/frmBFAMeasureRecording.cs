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

namespace CMT
{
    public partial class frmBFAMeasureRecording : Form
    {
        bool formloaded;
        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn(); // 0 TLCMT_AuditMeasurementsRecorded 
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn(); // 1 TLCMT_AuditMeasurements 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn(); // 2 Measurement Description
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn(); // 3 Measure 1
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn(); // 4 Measure 2 
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn(); // 5 Measure 3 
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn(); // 6 Measure 4
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn(); // 7 Measure 5
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn(); // 8 Measure 6
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn(); // 9 Measure 7
        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn(); // 10 Measure 8
        DataGridViewTextBoxColumn oTxtM = new DataGridViewTextBoxColumn(); // 11 Measure 9
        DataGridViewTextBoxColumn oTxtN = new DataGridViewTextBoxColumn(); // 12 Measure 10
 
         public frmBFAMeasureRecording()
        {
            InitializeComponent();
            core = new Util();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
   
        }

        private void BFAMeasureRecording_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            IList<TLADM_MachineOperators> Operators = new List<TLADM_MachineOperators>();
            using (var context = new TTI2Entities())

            {
                radCurrent.Checked = true;

                var CutSht = (from LineIssue in context.TLCMT_LineIssue
                           join CutSheet in context.TLCUT_CutSheet on LineIssue.TLCMTLI_CutSheet_FK equals CutSheet.TLCutSH_Pk
                           where LineIssue.TLCMTLI_IssuedToLine && !LineIssue.TLCMTLI_WorkCompleted
                           select CutSheet).OrderBy(x=>x.TLCutSH_No).ToList();

                cmboCutSheet.DataSource = CutSht;
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_IsCMT);
                if(Dept != null)
                {
                    foreach(var Record in Dept)
                    {
                        var Ops = context.TLADM_MachineOperators.Where(x=>x.MachOp_Department_FK == Record.Dep_Id && !x.MachOp_Discontinued).ToList();
                        foreach( var Op in Ops)
                        {
                            Operators.Add(Op);
                        }
                    }
                }
                
                cmboOperators.DataSource = Operators;
                cmboOperators.ValueMember = "MachOp_Pk";
                cmboOperators.DisplayMember = "MachOp_Description";
                cmboOperators.SelectedValue = -1;


                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtA);

                oTxtB.Visible = false;
                oTxtB.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC.ValueType = typeof(String);
                oTxtC.ReadOnly = true;
                oTxtC.HeaderText = "Measurement Point";
                dataGridView1.Columns.Add(oTxtC);

                oTxtD.ValueType = typeof(decimal);
                oTxtD.HeaderText = "Prod 1";
                dataGridView1.Columns.Add(oTxtD);

                oTxtE.ValueType = typeof(decimal);
                oTxtE.HeaderText = "Prod 2";
                dataGridView1.Columns.Add(oTxtE);

                oTxtF.ValueType = typeof(decimal);
                oTxtF.HeaderText = "Prod 3";
                dataGridView1.Columns.Add(oTxtF);

                oTxtG.ValueType = typeof(decimal);
                oTxtG.HeaderText = "Prod 4";
                dataGridView1.Columns.Add(oTxtG);

                oTxtH.ValueType = typeof(decimal);
                oTxtH.HeaderText = "Prod 5";
                dataGridView1.Columns.Add(oTxtH);

                oTxtJ.ValueType = typeof(decimal);
                oTxtJ.HeaderText = "Prod 6";
                dataGridView1.Columns.Add(oTxtJ);

                oTxtK.ValueType = typeof(decimal);
                oTxtK.HeaderText = "Prod 7";
                dataGridView1.Columns.Add(oTxtK);

                oTxtL.ValueType = typeof(decimal);
                oTxtL.HeaderText = "Prod 8";
                dataGridView1.Columns.Add(oTxtL);

                oTxtM.ValueType = typeof(decimal);
                oTxtM.HeaderText = "Prod 9";
                dataGridView1.Columns.Add(oTxtM);

                oTxtN.ValueType = typeof(decimal);
                oTxtN.HeaderText = "Prod 10";
                dataGridView1.Columns.Add(oTxtN);
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

                    if (Cell.ColumnIndex == 3 ||
                        Cell.ColumnIndex == 4 || 
                        Cell.ColumnIndex == 5 || 
                        Cell.ColumnIndex == 6 || 
                        Cell.ColumnIndex == 7 || 
                        Cell.ColumnIndex == 8 || 
                        Cell.ColumnIndex == 9 || 
                        Cell.ColumnIndex == 10)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add = false;
            if (oBtn != null)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if(selected == null)
                {
                    MessageBox.Show("Please select a Cut Sheet from the drop down box");
                    return;
                }

                var SizeSelected = (TLADM_Sizes)cmboSizes.SelectedItem;
                if (SizeSelected == null)
                {
                    MessageBox.Show("Please select the appropriate size from the drop down box");
                    return;
                }
            
                var Inspector = (TLADM_MachineOperators)cmboOperators.SelectedItem;
                if (Inspector == null)
                {
                    MessageBox.Show("Please select an Inspector from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var LI = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == selected.TLCutSH_Pk).FirstOrDefault();
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        Add = true;

                        TLCMT_AuditMeasureRecorded amr = new TLCMT_AuditMeasureRecorded();
                        if (row.Cells[0].Value != null)
                        {
                            var index = (int)row.Cells[0].Value;
                            amr = context.TLCMT_AuditMeasureRecorded.Find(index);
                            
                            if (amr == null)
                                amr = new TLCMT_AuditMeasureRecorded();
                            else
                                Add = false;
                        }

                        amr.TLBFAR_CutSheet_FK = selected.TLCutSH_Pk;
                        amr.TLBFAR_AuditMeasure_FK = (int)row.Cells[1].Value;
                        amr.TLDFAR_Date = dtpTransDate.Value;
                        amr.TLDFAR_Prod1 = (decimal)row.Cells[3].Value;
                        amr.TLDFAR_Prod2 = (decimal)row.Cells[4].Value;
                        amr.TLDFAR_Prod3 = (decimal)row.Cells[5].Value;
                        amr.TLDFAR_Prod4 = (decimal)row.Cells[6].Value;
                        amr.TLDFAR_Prod5 = (decimal)row.Cells[7].Value;
                        amr.TLDFAR_Prod6 = (decimal)row.Cells[8].Value;
                        amr.TLDFAR_Prod7 = (decimal)row.Cells[9].Value;
                        amr.TLDFAR_Prod8 = (decimal)row.Cells[10].Value;
                        amr.TLDFAR_Prod9 = (decimal)row.Cells[11].Value;
                        amr.TLDFAR_Prod10 = (decimal)row.Cells[12].Value;
                        amr.TLDFAR_Inspector_FK = Inspector.MachOp_Pk;
                        amr.TLDFAR_Size_FK = SizeSelected.SI_id;

                        if (LI != null)
                            amr.TLBFAR_Department_FK = LI.TLCMTLI_CmtFacility_FK;

                        if (Add)
                            context.TLCMT_AuditMeasureRecorded.Add(amr);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Record successfully updated to database");
                        dataGridView1.Rows.Clear();
                        cmboCutSheet.SelectedValue = -1;
                        cmboOperators.SelectedValue = -1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            //CS Size 
            if (oCmbo != null && formloaded)
            {
                var CS = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (CS != null)
                {
                    cmboSizes.DataSource = null;
                    cmboSizes.Items.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var Sizes = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                        foreach (var Size in Sizes)
                        {
                            var Display = context.TLADM_Sizes.Find(Size.TLCUTE_Size_FK);
                            if (Display != null)
                            {
                                cmboSizes.Items.Add(Display);
                            }
                        }

                        cmboSizes.DisplayMember = "SI_Description";
                        cmboSizes.ValueMember = "SI_Id";
                        cmboSizes.SelectedIndex = -1;
                    }
                    
                          
                }
                          
            }
                          
        }

        private void radHistory_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    using (var context = new TTI2Entities())
                    {
                        cmboCutSheet.DataSource = null;
                        dataGridView1.Rows.Clear();

                        formloaded = false;

                        var CutSht = (from LineIssue in context.TLCMT_LineIssue
                                      join CutSheet in context.TLCUT_CutSheet on LineIssue.TLCMTLI_CutSheet_FK equals CutSheet.TLCutSH_Pk
                                      where LineIssue.TLCMTLI_IssuedToLine && LineIssue.TLCMTLI_WorkCompleted
                                      select CutSheet).ToList();

                        cmboCutSheet.DataSource = CutSht;
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;

                        formloaded = true;
                    }
                }
            }
        }

        private void radCurrent_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();

                        cmboCutSheet.DataSource = null;
                        formloaded = false;
                        var CutSht = (from LineIssue in context.TLCMT_LineIssue
                                      join CutSheet in context.TLCUT_CutSheet on LineIssue.TLCMTLI_CutSheet_FK equals CutSheet.TLCutSH_Pk
                                      where LineIssue.TLCMTLI_IssuedToLine && !LineIssue.TLCMTLI_WorkCompleted
                                      select CutSheet).ToList();

                        cmboCutSheet.DataSource = CutSht;
                        cmboCutSheet.ValueMember = "TLCutSH_Pk";
                        cmboCutSheet.DisplayMember = "TLCutSH_No";
                        cmboCutSheet.SelectedValue = -1;
                        formloaded = true;
                    }
                }
            }
        }

        private void cmboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                var CS = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                if (CS != null)
                {
                    var SizeSelected = (TLADM_Sizes)oCmbo.SelectedItem;
                    if (SizeSelected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLCMT_AuditMeasurements.Where(x => x.CMTBFA_Customer_FK == CS.TLCutSH_Customer_FK && x.CMTBFA_Size_FK == SizeSelected.SI_id && x.CMTBFA_Style_FK == CS.TLCutSH_Styles_FK).ToList();
                            if (Existing.Count == 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append("There is no CMT measurement data set up for ");
                                sb.Append(" Customer : " + context.TLADM_CustomerFile.Find(CS.TLCutSH_Customer_FK).Cust_Description);
                                sb.Append(" Style : " + context.TLADM_Styles.Find(CS.TLCutSH_Styles_FK).Sty_Description);
                                sb.Append(" Size : " + context.TLADM_Sizes.Find(SizeSelected.SI_id).SI_Description);
                                MessageBox.Show(sb.ToString());
                                return;
                            }

                            dataGridView1.Rows.Clear();
                            //=============================================================================
                            // We need to sort this into a particular sequence based on the short code 
                            //============================================================================
                            Existing = (from ADM_AuditM in context.TLADM_CMTMeasurementPoints
                                        join CMT_AuditM in context.TLCMT_AuditMeasurements on ADM_AuditM.CMTMP_Pk equals CMT_AuditM.CMTBFA_MeasureP_FK
                                        where CMT_AuditM.CMTBFA_Customer_FK == CS.TLCutSH_Customer_FK && CMT_AuditM.CMTBFA_Style_FK == CS.TLCutSH_Styles_FK && CMT_AuditM.CMTBFA_Size_FK == SizeSelected.SI_id
                                        orderby ADM_AuditM.CMTMP_DisplayOrder
                                            select CMT_AuditM).ToList();

                            foreach (var Record in Existing)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[1].Value = Record.CMTBFA_Pk;
                                dataGridView1.Rows[index].Cells[2].Value = context.TLADM_CMTMeasurementPoints.Find(Record.CMTBFA_MeasureP_FK).CMTMP_Description;
                                dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[6].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[7].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[8].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[9].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[10].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[11].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[12].Value = 0.00M;
                            }

                            var ExistRecord = context.TLCMT_AuditMeasureRecorded.Where(x => x.TLBFAR_CutSheet_FK == CS.TLCutSH_Pk && x.TLDFAR_Size_FK == SizeSelected.SI_id).ToList();
                            foreach (var Record in ExistRecord)
                            {
                                var SingleRow = (
                                        from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                        where (int)Rows.Cells[1].Value == Record.TLBFAR_AuditMeasure_FK
                                        select Rows).FirstOrDefault();
                                if (SingleRow != null)
                                {
                                    SingleRow.Cells[3].Value = Record.TLDFAR_Prod1;
                                    SingleRow.Cells[4].Value = Record.TLDFAR_Prod2;
                                    SingleRow.Cells[5].Value = Record.TLDFAR_Prod3;
                                    SingleRow.Cells[6].Value = Record.TLDFAR_Prod4;
                                    SingleRow.Cells[7].Value = Record.TLDFAR_Prod5;
                                    SingleRow.Cells[8].Value = Record.TLDFAR_Prod6;
                                    SingleRow.Cells[9].Value = Record.TLDFAR_Prod7;
                                    SingleRow.Cells[10].Value = Record.TLDFAR_Prod8;
                                    SingleRow.Cells[11].Value = Record.TLDFAR_Prod9;
                                    SingleRow.Cells[12].Value = Record.TLDFAR_Prod10;
                                }

                            }
                        }

                    }
                }
            }
        }
    }
}
