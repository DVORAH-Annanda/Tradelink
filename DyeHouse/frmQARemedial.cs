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
    public partial class frmQARemedial : Form
    {
        bool formloaded;
        Util core;

        DataGridViewTextBoxColumn oTxtA0 = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtA1 = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtA2 = new DataGridViewTextBoxColumn();

        DataGridViewTextBoxColumn oTxtB0 = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkB = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB1 = new DataGridViewTextBoxColumn();
        
        DataGridViewTextBoxColumn oTxtC0 = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();
        DataGridViewCheckBoxColumn oChkC = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtC1 = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC2 = new DataGridViewTextBoxColumn();

        DataGridViewTextBoxColumn oTxtD0 = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn();
        DataGridViewTextBoxColumn oTxtD1 = new DataGridViewTextBoxColumn();

        List<DATA> fieldSelected;

        public frmQARemedial()
        {
            InitializeComponent();
                   
            core = new Util();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = false;

            dataGridView3.AutoGenerateColumns = false;
            dataGridView4.AutoGenerateColumns = false;
            //--------------------------------------------------------
            // Receipe Master Records 
            //-----------------------------------------------------------------
            oTxtA0.ReadOnly = true;
            oTxtA0.Visible = false;
            oTxtA0.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA0);

            oChkA.HeaderText = "Selected";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtA1.HeaderText = "Fault Code";
            oTxtA1.ReadOnly = true;
            oTxtA1.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtA1);

            oTxtA2.HeaderText = "Fault Description";
            oTxtA2.ValueType = typeof(string);
            oTxtA2.ReadOnly = true;
            oTxtA2.Width = 200;
            dataGridView1.Columns.Add(oTxtA2);
            //-----------------------------------------------------------------------------
            // Receipe Details 
            //--------------------------------------------------------------------------------
            oTxtB0.ReadOnly = true;
            oTxtB0.Visible = false;
            oTxtB0.ValueType = typeof(int);
            dataGridView2.Columns.Add(oTxtB0);

            oChkB.HeaderText = "Select";
            oChkB.ValueType = typeof(bool);
            dataGridView2.Columns.Add(oChkB);

            oTxtB1.HeaderText = "Recipe Description";
            oTxtB1.ReadOnly = true;
            oTxtB1.ValueType = typeof(string);
            oTxtB1.Width = 200; 
            dataGridView2.Columns.Add(oTxtB1);

            //----------------------------------------------------------------
            // Consumables 
            //-------------------------------------------------------------------
            oTxtC0.ReadOnly = true;                                  // 0
            oTxtC0.Visible = false;
            oTxtC0.ValueType = typeof(int);
            dataGridView3.Columns.Add(oTxtC0);

            oCmboA.HeaderText = "Consumables";                       // 1
            oCmboA.ValueType = typeof(int);
            oCmboA.Width = 150;
            dataGridView3.Columns.Add(oCmboA);

            oChkC.HeaderText = "g/l";                                // 2
            oChkC.ValueType = typeof(bool);
            dataGridView3.Columns.Add(oChkC);

            oTxtC1.HeaderText = "Chemical Ratios";                  // 3
            oTxtC1.ValueType = typeof(decimal);
            oTxtC1.Width = 100;
            dataGridView3.Columns.Add(oTxtC1);

            oTxtC2.HeaderText = "Weight";                          // 4
            oTxtC2.ValueType = typeof(decimal);
            oTxtC2.Width = 100;
            dataGridView3.Columns.Add(oTxtC2);

            //----------------------------------------------------------------
            // Non Consumables 
            //-------------------------------------------------------------------

            oTxtD0.ReadOnly = true;
            oTxtD0.Visible = false;
            oTxtD0.ValueType = typeof(int);
            dataGridView4.Columns.Add(oTxtD0);

            oCmboB.HeaderText = "Non Consumables";
            oCmboB.ValueType = typeof(int);
            oCmboB.Width = 200;
            dataGridView4.Columns.Add(oCmboB);

            oTxtD1.HeaderText = "Quantities";
            oTxtD1.ValueType = typeof(decimal);
            oTxtD1.Width = 200;
            dataGridView4.Columns.Add(oTxtD1);

            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);

            SetUp();

        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboProcess.DataSource = context.TLADM_QADyeProcess.OrderBy(x => x.QADYEP_Pk).ToList();
                cmboProcess.DisplayMember = "QADYEP_Description";
                cmboProcess.ValueMember = "QADYEP_Pk";

                oCmboA.DataSource = context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Code).ToList();
                oCmboA.DisplayMember = "ConsDC_Description";
                oCmboA.ValueMember = "ConsDC_Pk";

                oCmboB.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Code).ToList();
                oCmboB.DisplayMember = "NSI_Description";
                oCmboB.ValueMember = "NSI_Pk";

                var Existing = context.TLDYE_RecipeDefinition.Where(x => !x.TLDYE_StandardReceipe).ToList();
                foreach (var Row in Existing)
                {
                    var Index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[Index].Cells[0].Value = Row.TLDYE_DefinePk;
                    dataGridView2.Rows[Index].Cells[1].Value = false;
                    dataGridView2.Rows[Index].Cells[2].Value = Row.TLDYE_DefineDescription;
                }
            }

            dataGridView3.Enabled = false;
            dataGridView4.Enabled = false;

            formloaded = true;
        }

        private void cmboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_QADyeProcess)cmboProcess.SelectedItem;
                if (selected != null && formloaded)
                {
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmboBatches.DataSource = context.TLDYE_NonCompliance.Where(x => x.TLDYE_NCStage == selected.QADYEP_Pk  && !x.TLDYE_RemedyApplied).ToList();
                        cmboBatches.DisplayMember = "TLDYE_NcrNumber";
                        cmboBatches.ValueMember = "TLDYE_NcrPk";
                        formloaded = true;
                    }
                }
            }
        }

        private void cmboBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();

                fieldSelected = new List<DATA>();

                using (var context = new TTI2Entities())
                {
                    var selected = (TLDYE_NonCompliance)cmboBatches.SelectedItem;
                    if (selected != null)
                    {
                        var QDC = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Code).ToList();
                        foreach (var record in QDC)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = record.QDF_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = record.QDF_Code;
                            dataGridView1.Rows[index].Cells[3].Value = record.QDF_Description;
                        }

                        var Existing = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == selected.TLDYE_NcrNumber && x.DYENCRD_FR).ToList();
                        foreach (var row in Existing)
                        {
                            foreach (DataGridViewRow rows in dataGridView1.Rows)
                            {
                                if ((int)rows.Cells[0].Value == row.DYENCRD_Code_FK)
                                {
                                     dataGridView1.Rows[rows.Index].Cells[1].Value = true;
                                     fieldSelected.Add(new DATA(rows.Index, row.DYENCRD_PK, true)); 
                                     break;
                                 }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;

             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 if (!(bool)oDgv.CurrentCell.EditedFormattedValue)
                 {
                     var CurrentRow = oDgv.CurrentRow;
                     if (CurrentRow != null)
                     {
                         var Record = fieldSelected.Find(x => x.recordindex == CurrentRow.Index && x.Fault);
                         var RecordIndex = fieldSelected.IndexOf(Record);
                         if (RecordIndex != -1)
                         {
                             DialogResult res = MessageBox.Show("Please confirm this action", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                             if (res == DialogResult.Yes)
                             {
                                 using (var context = new TTI2Entities())
                                 {
                                     var index = (int)CurrentRow.Cells[0].Value;
                                     var record = context.TLDYE_NonComplianceDetail.Find(Record.QDFIndex);
                                     if (record != null)
                                     {
                                         context.TLDYE_NonComplianceDetail.Remove(record);
                                         try
                                         {
                                             context.SaveChanges();
                                             MessageBox.Show("Record Changed successfully");
                                             fieldSelected.Remove(Record);
                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show(ex.Message);
                                         }
                                     }
                                 }
                             }
                             else
                             {
                                 oDgv.CurrentCell.Value = true;
                                 dataGridView1.Rows[CurrentRow.Index].Cells[1].Value = true;
                                 dataGridView1.Refresh();
                             }

                         }
                     }
                 }
             }
        }

        private struct DATA
        {
            public int recordindex;        // datagridview1
            public int QDFIndex;
            public bool Fault;             // if True then a fault else False = remedy

            public DATA(int recindex, int QDF, bool Flt)
            {
                recordindex = recindex;
                QDFIndex = QDF;
                Fault = Flt;
            }
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add = false;
            TLADM_QADyeProcess Stages  = null;
            if (oBtn != null && formloaded)
            {
                var selected = (TLDYE_NonCompliance)cmboBatches.SelectedItem;
                if (selected != null)
                {
                    if (dataGridView4.Rows.Count == 0)
                    {
                        MessageBox.Show("Please supply a non consumable description and quantity");
                        return;
                    }
                    using (var context = new TTI2Entities())
                    {

                        if (rbPass.Checked || rbReprocess.Checked)
                        {
                            var DB = context.TLDYE_DyeBatch.Find(selected.TLDYE_NcrBatchNo_FK);
                            if (DB != null)
                            {
                                Stages = (TLADM_QADyeProcess)cmboProcess.SelectedItem;
                                if (rbPass.Checked)
                                {
                                    if (Stages.QADYEP_Pk == 1)
                                        DB.DYEB_Stage1 = true;
                                    else if (Stages.QADYEP_Pk == 2)
                                        DB.DYEB_Stage2 = true;
                                    else
                                        DB.DYEB_Stage3 = true;
                                }

                                if (rbReprocess.Checked)
                                {
                                    DB.DYEB_Reprocess = true;

                                    if (rbReprocess.Checked)
                                    {
                                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                                        if (Dept != null)
                                        {
                                            TLDYE_DyeTransactions trns = new TLDYE_DyeTransactions();
                                            trns.TLDYET_BatchNo = DB.DYEB_BatchNo;
                                            trns.TLDYET_BatchWeight = DB.DYEB_BatchKG;
                                            trns.TLDYET_Date = DateTime.Now;
                                            trns.TLDYET_SequenceNo = DB.DYEB_SequenceNo;
                                            trns.TLDYET_Batch_FK = DB.DYEB_Pk;
                                            trns.TLDYET_RejectDate = DateTime.Now;

                                            var TT = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 500).FirstOrDefault();
                                            if (TT != null)
                                            {
                                                trns.TLDYET_TransactionType = TT.TrxT_Pk;
                                                trns.TLDYET_CurrentStore_FK = (int)TT.TrxT_ToWhse_FK;
                                            }

                                            context.TLDYE_DyeTransactions.Add(trns);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == bool.FalseString)
                                continue;

                            //--------------------------------------------------------------
                            // We dont want to update the same record twice
                            //----------------------------------------------------------
                            var Value = (int)row.Cells[0].Value;
                            var record = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == selected.TLDYE_NcrNumber &&
                                                                                  x.DYENCRD_Code_FK == Value && x.DYENCRD_FR).FirstOrDefault();
                            if (record != null)
                                continue;

                            TLDYE_NonComplianceDetail nonD = new TLDYE_NonComplianceDetail();
                            nonD.DYENCRD_BatchNo_Fk = selected.TLDYE_NcrBatchNo_FK;
                            nonD.DYENCRD_FR = true;
                            nonD.DYENCRD_Code_FK = (int)row.Cells[0].Value;
                            nonD.DYENCRD_ComNumber = selected.TLDYE_NcrNumber;

                            context.TLDYE_NonComplianceDetail.Add(nonD);
                        }

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == bool.FalseString)
                                continue;

                            //--------------------------------------------------------------
                            // We dont want to update the same record twice
                            //----------------------------------------------------------
                            var Value = (int)row.Cells[0].Value;
                            var record = context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_ComNumber == selected.TLDYE_NcrNumber &&
                                                                                  x.DYENCRD_Code_FK == Value && !x.DYENCRD_FR).FirstOrDefault();
                            if (record != null)
                                continue;
                            
                            TLDYE_NonComplianceDetail nonD = new TLDYE_NonComplianceDetail();
                            nonD.DYENCRD_BatchNo_Fk = selected.TLDYE_NcrBatchNo_FK;
                            nonD.DYENCRD_FR = false;
                            nonD.DYENCRD_Code_FK = (int)row.Cells[0].Value;
                            nonD.DYENCRD_ComNumber = selected.TLDYE_NcrNumber;
                            
                            context.TLDYE_NonComplianceDetail.Add(nonD);
                        }

                        foreach (DataGridViewRow row in dataGridView3.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            var Value = 0.00M;
                            if(row.Cells[4].Value != null)
                                decimal.TryParse(row.Cells[4].Value.ToString(), out Value);

                            if (Value == 0.00M)
                                continue;

                            Add = false;
                            
                            TLDYE_NonComplianceConsDetail nonD = new TLDYE_NonComplianceConsDetail();
                            // Cant add the same record twice
                            if (row.Cells[0].Value != null)
                            {
                                var index = (int)row.Cells[0].Value;
                                nonD = context.TLDYE_NonComplianceConsDetail.Find(index);

                            }
                            else
                                Add = true;

                            nonD.DYENCCON_BatchNo_FK = selected.TLDYE_NcrBatchNo_FK;
                            nonD.DYENCCON_ConOrNon = true;
                            nonD.DYENCCON_Code_FK = (int)row.Cells[1].Value;
                            nonD.DYENCCON_ConNumber = selected.TLDYE_NcrNumber;
                            nonD.DYENCCON_Qunatities = (decimal)row.Cells[4].Value;
                            
                            if(Add)
                                context.TLDYE_NonComplianceConsDetail.Add(nonD);
                        }

                        foreach (DataGridViewRow row in dataGridView4.Rows)
                        {
                            if (row.Cells[1].Value == null)
                            {
                               continue;
                            }

                            Add = false;

                            TLDYE_NonComplianceConsDetail nonD = new TLDYE_NonComplianceConsDetail();
                            // Cant add the same record twice
                            if (row.Cells[0].Value != null)
                            {
                                var index = (int)row.Cells[0].Value;
                                nonD = context.TLDYE_NonComplianceConsDetail.Find(index);
                                
                            }
                            else
                                Add = true;

                            nonD.DYENCCON_BatchNo_FK = selected.TLDYE_NcrBatchNo_FK;
                            nonD.DYENCCON_ConOrNon = false;
                            nonD.DYENCCON_Code_FK = (int)row.Cells[1].Value;
                            nonD.DYENCCON_ConNumber = selected.TLDYE_NcrNumber;
                            if (row.Cells[2].Value != null)
                                nonD.DYENCCON_Qunatities = (decimal)row.Cells[2].Value;
                            else
                                nonD.DYENCCON_Qunatities = 0.00M;

                            if(Add)
                                context.TLDYE_NonComplianceConsDetail.Add(nonD);
                        }

                       

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved successfully to database");
                            frmDyeViewReport vRep = new frmDyeViewReport(7, selected.TLDYE_NcrNumber);
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
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView3.Enabled = true;
                dataGridView4.Enabled = true;

                foreach (DataGridViewRow Row in oDgv.Rows)
                {
                    if ((bool)Row.Cells[1].EditedFormattedValue)
                    {
                        var CurrentRow = oDgv.CurrentRow;

                        if (CurrentRow.Index != Row.Index)
                        {
                            oDgv.Rows[Row.Index].Cells[1].Value = false;
                            continue; 
                        }

                        dataGridView3.Rows.Clear();

                        int Pk = (int)Row.Cells[0].Value;

                        using(var context = new TTI2Entities())
                        {
                            var selected = (TLDYE_NonCompliance)cmboBatches.SelectedItem;

                            var NCExisting = context.TLDYE_NonComplianceConsDetail.Where(x => x.DYENCCON_ConNumber == selected.TLDYE_NcrNumber).ToList();
                            if (NCExisting.Count != 0)
                            {
                                foreach (var row in NCExisting)
                                {
                                    if (row.DYENCCON_ConOrNon)
                                    {
                                        var index = dataGridView3.Rows.Add();
                                        dataGridView3.Rows[index].Cells[0].Value = row.DYENCCON_Pk;
                                        dataGridView3.Rows[index].Cells[1].Value = row.DYENCCON_Code_FK;
                                        dataGridView3.Rows[index].Cells[2].Value = row.DYENCCON_LiqCalc ;
                                        dataGridView3.Rows[index].Cells[3].Value = row.DYENCCON_LiqRatio;
                                        dataGridView3.Rows[index].Cells[4].Value = row.DYENCCON_Qunatities;

                                    }
                                    else
                                    {
                                        var index = dataGridView4.Rows.Add();
                                        dataGridView4.Rows[index].Cells[0].Value = row.DYENCCON_Pk;
                                        dataGridView4.Rows[index].Cells[1].Value = row.DYENCCON_Code_FK;
                                        dataGridView4.Rows[index].Cells[2].Value = row.DYENCCON_Qunatities;
                                    }

                                }
                            }
                            else
                            {

                                var Records = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == Pk).ToList();
                                foreach (var Record in Records)
                                {
                                    var index = dataGridView3.Rows.Add();
                                    dataGridView3.Rows[index].Cells[1].Value = Record.TLDYED_Cosumables_FK;
                                    dataGridView3.Rows[index].Cells[2].Value = Record.TLDYED_LiqCalc;
                                    dataGridView3.Rows[index].Cells[3].Value = Record.TLDYED_LiqRatio;
                                    dataGridView3.Rows[index].Cells[4].Value = 0;
                                }
                            }
                        }

                    }
                }
            }
        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                
            }
        }

        private void dataGridView4_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView4.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());
                            //------------------------------------------------------------------------------------
                            // 
                            //---------------------------------------------------------------------------
                            var locRec = context.TLDYE_NonComplianceConsDetail.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLDYE_NonComplianceConsDetail.Remove(locRec);
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        oDgv.Rows.RemoveAt(this.dataGridView4.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
