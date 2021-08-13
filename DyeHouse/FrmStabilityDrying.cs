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
    public partial class FrmStabilityDrying : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();

        bool formloaded;
        bool _StabAfterDrying;

        Util core;

        bool Add;

        public FrmStabilityDrying(bool StabAfterDrying)
        {
            InitializeComponent();

            core = new Util();

            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Fields";
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = 200;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Results";
            oTxtC.ValueType = typeof(decimal);
            oTxtC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTxtC.Width = 125;
            dataGridView1.Columns.Add(oTxtC);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            if (StabAfterDrying)
                this.Text = "Stability Check After Drying";
            else
                this.Text = "Fabric Stability check after compacting";

           _StabAfterDrying = StabAfterDrying;
            SetUp(true);

        }

        void SetUp(bool IsSetUp)
        {
            formloaded = false;
            dataGridView1.Enabled = false;
            dataGridView1.Rows.Clear();
            List<TLADM_QADyeProcessFields> QAProcessItems = null;
            Add = true;

            label5.Visible = false;
            cmboOperator.Visible = false;

            var TwelveMonths = DateTime.Now.AddMonths(-12);

            rbPassYes.Checked = true;

            using (var context = new TTI2Entities())
            {
                if (IsSetUp)
                {
                    if (_StabAfterDrying)
                    {
                        cmboBatchNumber.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= TwelveMonths).OrderBy(x => x.DYEB_BatchNo).ToList();
                        //cmboBatchNumber.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_Transfered && x.DYEB_Allocated && x.DYEB_Stage1 && !x.DYEB_Stage2 && !x.DYEB_Stage3).OrderBy(x => x.DYEB_BatchNo).ToList();
                    }
                    else
                    {
                        cmboBatchNumber.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= TwelveMonths).OrderBy(x => x.DYEB_BatchNo).ToList();
                        // cmboBatchNumber.DataSource = context.TLDYE_DyeBatch.Where(x => x.DYEB_Transfered && x.DYEB_Allocated && x.DYEB_Stage1 && x.DYEB_Stage2 && !x.DYEB_Stage3).OrderBy(x => x.DYEB_BatchNo).ToList();
                    }
                    
                    cmboBatchNumber.ValueMember = "DYEB_Pk";
                    cmboBatchNumber.DisplayMember = "DYEB_BatchNo";
                    cmboBatchNumber.SelectedValue = 0;
                    
                   /* cmboOperator.DataSource = context.TLADM_MachineOperators.Where(x => !x.MachOp_Inspector && !x.MachOp_Discontinued).OrderBy(x => x.MachOp_Description).ToList();
                    cmboOperator.DisplayMember = "MachOp_Description";
                    cmboOperator.ValueMember = "MachOp_Pk";
                    cmboOperator.SelectedValue = 0;
                    oCmboPieceNumber.DataSource = null;*/

                }

                //var QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 2).ToList();
                if (_StabAfterDrying)
                {
                    QAProcessItems = context.TLADM_QADyeProcessFields.Where(x=>x.TLQADPF_Drier).ToList();
                }
                else
                {
                    QAProcessItems = context.TLADM_QADyeProcessFields.Where(x=>x.TLQAPF_Compactor || x.TLQADPF_Process_FK == 3).ToList();
                }

                foreach (var Record in QAProcessItems)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Record.TLQADPF_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = Record.TLQADPF_Description;
                    dataGridView1.Rows[index].Cells[2].Value = 0.00M;
                }
                dataGridView1.Enabled = true;
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

                    if (Cell.ColumnIndex == 2)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void cmboBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            TLDYE_NonComplianceAnalysis Existing;
            bool first = true;
            IList<TLADM_QADyeProcessFields> QAProcessItems = null;  // context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 2).ToList();
            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (selected != null)
                {
                    oCmboPieceNumber.DataSource = null;
                    oCmboPieceNumber.Items.Clear();
                    dataGridView1.Rows.Clear();
                    
                    using (var context = new TTI2Entities())
                    {
                        if (!selected.DYEB_CommissinCust)
                        {
                            var DO = context.TLDYE_DyeOrder.Find(selected.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                var color = context.TLADM_Colours.Find(DO.TLDYO_Colour_FK);
                                if (color != null)
                                {
                                    txtColour.Text = color.Col_Display;
                                }

                                var Qual = context.TLADM_Griege.Find(DO.TLDYO_Greige_FK);
                                if (Qual != null)
                                {
                                    txtQuality.Text = Qual.TLGreige_Description;
                                }
                            }
                        }
                        else
                        {
                            txtColour.Text = context.TLADM_Colours.Find(selected.DYEB_Colour_FK).Col_Display;
                        }

                        var Query = (from GProd in context.TLKNI_GreigeProduction
                                     join DyeBatch in context.TLDYE_DyeBatchDetails
                                     on GProd.GreigeP_Pk equals DyeBatch.DYEBD_GreigeProduction_FK
                                     where DyeBatch.DYEBD_DyeBatch_FK == selected.DYEB_Pk
                                     select GProd).OrderBy(x => x.GreigeP_PieceNo).ToList();

                        formloaded = false;
                        oCmboPieceNumber.DataSource = null;
                        oCmboPieceNumber.DataSource = Query;
                        oCmboPieceNumber.ValueMember = "GreigeP_Pk";
                        oCmboPieceNumber.DisplayMember = "GreigeP_PieceNo";
                        oCmboPieceNumber.SelectedValue = -1;
                        formloaded = true;
                        

                        dataGridView1.Rows.Clear();
                        // var QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 2 || x.TL.ToList();
                        if (_StabAfterDrying)
                        {
                            QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Drier).ToList();
                        }
                        else
                        {
                            QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQAPF_Compactor || x.TLQADPF_Process_FK == 3).ToList();
                        }
                        if (QAProcessItems.Count == 0)
                        {
                            MessageBox.Show("No set up has been applied to QA Dye Process Fields table");
                            return;
                        }
                        foreach (var Record in QAProcessItems)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Record.TLQADPF_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Record.TLQADPF_Description;

                            if (_StabAfterDrying)
                                Existing = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == selected.DYEB_Pk && x.TLDYEDC_Code_FK == Record.TLQADPF_Pk && x.TLDYEDC_NCStage == 4).FirstOrDefault();
                            else
                                Existing = context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == selected.DYEB_Pk && x.TLDYEDC_Code_FK == Record.TLQADPF_Pk && x.TLDYEDC_NCStage == 5).FirstOrDefault();

                            if (Existing != null)
                            {
                                if (first)
                                {
                                    Add = false;
                                    first = false;
                                    cmboOperator.SelectedValue = Existing.TLDYEDC_Operator_FK;
                                }
                                dataGridView1.Rows[index].Cells[2].Value = Existing.TLDYEDC_Value;
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[2].Value = 0.00M;
                            }
                        }

                    }
                }
            }
        }

        private void chkNCRRequired_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if (oChk != null && formloaded)
            {
                if (oChk.Checked)
                {
                    var DB = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                    if (DB == null)
                    {
                        MessageBox.Show("Please select a dye batch from the drop down list");
                        return;
                    }
                    var Selected = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                    if (Selected == null)
                    {
                        MessageBox.Show("Please select an operator from the drop down box");
                        return;
                    }

                   

                    if (_StabAfterDrying)
                    {
                        frmDye_NonCompliance nonCom = new frmDye_NonCompliance(DB.DYEB_Pk, Selected.MachOp_Pk, 2, 0);
                        nonCom.ShowDialog(this);
                    }
                    else
                    {
                        frmDye_NonCompliance nonCom = new frmDye_NonCompliance(DB.DYEB_Pk, Selected.MachOp_Pk, 3, 0);
                        nonCom.ShowDialog(this);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLDYE_DyeTransactions trns = new TLDYE_DyeTransactions();
            IList<TLDYE_DyeBatchDetails> BatchDetails = new List<TLDYE_DyeBatchDetails>();

            if (oBtn != null && formloaded)
            {
                var DB = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (DB == null)
                {
                    MessageBox.Show("Please select a dye batch from the drop down list");
                    return;
                }

                /*
                var OP = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                if (OP == null)
                {
                    MessageBox.Show("Please select an operator from the drop down list");
                    return;
                }
                */


                var GP = (TLKNI_GreigeProduction)oCmboPieceNumber.SelectedItem;
                if (GP == null)
                {
                    MessageBox.Show("Please select a piece number from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    if (_StabAfterDrying)
                    {
                        trns = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DB.DYEB_Pk && x.TLDYET_Stage == 2).FirstOrDefault(); 
                    }
                    else
                    {
                        trns = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DB.DYEB_Pk && x.TLDYET_Stage == 3).FirstOrDefault(); 
                    }

                    if (trns == null)
                    {
                        trns = new TLDYE_DyeTransactions();
                        trns.TLDYET_BatchNo = DB.DYEB_BatchNo;
                        trns.TLDYET_BatchWeight = DB.DYEB_BatchKG;
                        trns.TLDYET_Date = DateTime.Now;
                        trns.TLDYET_SequenceNo = DB.DYEB_SequenceNo;
                        trns.TLDYET_Batch_FK = DB.DYEB_Pk;
                        if (_StabAfterDrying)
                               trns.TLDYET_Stage = 2;
                        else
                               trns.TLDYET_Stage = 3; 
                    
                         if (rbPassYes.Checked)
                         { 
                             trns.TLDYET_Rejected = false;
                         } 
                        else
                         {
                            trns.TLDYET_Rejected = true;
                            trns.TLDYET_RejectDate = DateTime.Now;
                         }

                         var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                         if (Dept != null)
                         {
                             var TT = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 400).FirstOrDefault();
                             if (TT != null)
                             {
                                 trns.TLDYET_TransactionType = TT.TrxT_Pk;
                                 trns.TLDYET_CurrentStore_FK = (int)TT.TrxT_ToWhse_FK;
                             }
                         }

                        context.TLDYE_DyeTransactions.Add(trns);
                    }

                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                       TLDYE_NonComplianceAnalysis nca = new TLDYE_NonComplianceAnalysis();

                       Add = true;
                           
                       nca.TLDYEDC_Code_FK = (int)row.Cells[0].Value;
                       nca.TLDYEDC_BatchNo = DB.DYEB_Pk;
                       nca.TLDYEDC_Operator_FK = 0; // (int)cmboOperator.SelectedValue;
                       nca.TLDYEDC_PieceNo_FK = GP.GreigeP_Pk;

                        if (rbPassYes.Checked)
                        {
                            nca.TLDYEDC_Pass = true;
                        }
                        else
                        {
                            nca.TLDYEDC_Pass = false;
                        }

                       nca.TLDYEDC_Date = DateTime.Now;

                        if (_StabAfterDrying)
                        {
                            nca.TLDYEDC_NCStage = 4;
                        }
                        else
                        {
                            nca.TLDYEDC_NCStage = 5;
                        }
                            

                       nca.TLDYEDC_Value = (decimal)row.Cells[2].Value;

                        if (Add)
                        {
                            context.TLDYE_NonComplianceAnalysis.Add(nca);
                        }
                    }


                    if (rbPassYes.Checked)
                    {
                        var DBatch = context.TLDYE_DyeBatch.Find(DB.DYEB_Pk);
                        if (DBatch != null)
                        {
                            if (_StabAfterDrying)
                            {
                                DBatch.DYEB_Stage2 = true;
                            }
                            else
                            {
                                DBatch.DYEB_Stage3 = true;
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved to database successfully");
                            SetUp(false);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
       
        private void oCmboPieceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void rbPassNo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    var DB = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                    if (DB == null)
                    {
                        MessageBox.Show("Please select a dye batch from the drop down list");
                        return;
                    }

                    /*
                    var Selected = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                    if (Selected == null)
                    {
                        MessageBox.Show("Please select an operator from the drop down box");
                        return;
                    }*/


                    if (_StabAfterDrying)
                    {
                        frmDye_NonCompliance nonCom = new frmDye_NonCompliance(DB.DYEB_Pk, 0, 2, 0);
                        nonCom.ShowDialog(this);
                    }
                    else
                    {
                        frmDye_NonCompliance nonCom = new frmDye_NonCompliance(DB.DYEB_Pk, 0, 3, 0);
                        nonCom.ShowDialog(this);
                    }
                }
            }
        }
    }
}
