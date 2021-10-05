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
    public partial class frmHydroResults : Form
    {
        bool FormLoaded;

        protected readonly TTI2Entities _context;

        DataTable dataTable;
        DataColumn column;
        BindingSource BindingSrc;
        Util core;

        TLDYE_DyeBatch SelectedDyeBatch;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     //  Bundle No                   1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Qty                          3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
        public frmHydroResults()
        {
            InitializeComponent();
            _context = new TTI2Entities();

            SelectedDyeBatch = null;
            core = new Util();
            dataTable = new DataTable();
            column = new DataColumn();
            BindingSrc = new BindingSource();

            BindingSrc.DataSource = dataTable;
            dataGridView1.DataSource = BindingSrc;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            //==========================================================================================
            // 1st task is to create the data table dataTable 
            // Col 0
            //=====================================================================
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Primary_Pk";
            column.Caption = "Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Measurement_Description";
            column.Caption = "Measurement Description";
            column.DefaultValue = String.Empty;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Measuremnt_Standard";
            column.Caption = "Measurement Standard";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Measurement_Result";
            column.Caption = "Measurement";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            oTxtA.Visible = false;
            oTxtA.Name = "Primary_Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.HeaderText = "Primary Key";
            oTxtA.DataPropertyName = dataTable.Columns[0].ColumnName;
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns[0].DisplayIndex = 0;

            //--------------------------------------------------------
            //1
            //--------------------------------------------------------------
            oTxtB.HeaderText = "Measurement";
            oTxtB.ValueType = typeof(string);
            oTxtB.Name = "Bundle";
            oTxtB.DataPropertyName = dataTable.Columns[1].ColumnName;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns[1].DisplayIndex = 1;

            oTxtC.HeaderText = "Standard";
            oTxtC.ValueType = typeof(int);
            oTxtC.Name = "Standard";
            oTxtC.DataPropertyName = dataTable.Columns[2].ColumnName;
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtD.HeaderText = "Result";
            oTxtD.ValueType = typeof(int);
            oTxtD.Name = "Result";
            oTxtD.DataPropertyName = dataTable.Columns[3].ColumnName;
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns[3].DisplayIndex = 3;
        }

        private void frmHydroResults_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
           

            FormLoaded = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex > 1)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
        private void frmHydroResults_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel && FormLoaded)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                if (txtDyeBatch.Text.Length == 0)
                {
                    MessageBox.Show("Please complete the text before pressing this button");
                    return;

                }

                SelectedDyeBatch = _context.TLDYE_DyeBatch.FirstOrDefault(x => x.DYEB_BatchNo == txtDyeBatch.Text);
                if (SelectedDyeBatch == null)
                {
                    MessageBox.Show("Dye Batch Not found");
                    return;
                }

                var nca = _context.TLDYE_NonComplianceAnalysis.FirstOrDefault(x=>x.TLDYEDC_NCStage == 6 && x.TLDYEDC_BatchNo == SelectedDyeBatch.DYEB_Pk);
                if(nca != null)
                {
                    MessageBox.Show("This Dye Batch has already been captured");
                    SelectedDyeBatch = null;
                    return;
                }
               
                dataTable.Rows.Clear();
                
                var MeasurementFields = _context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Hydro && !x.TLQAPF_Operator_Ins && x.TLQADPF_Process_FK == 5).ToList();
                foreach (var MeasurementField in MeasurementFields)
                {
                    DataRow NewRow = dataTable.NewRow();
                    NewRow[0] = MeasurementField.TLQADPF_Pk;
                    NewRow[1] = MeasurementField.TLQADPF_Description;
                    var DStand = _context.TLDYE_DyeingStandards.Where(x => x.DyeStan_QAProccessField_FK == MeasurementField.TLQADPF_Pk && x.DyeStan_Quality_FK == SelectedDyeBatch.DYEB_Greige_FK).FirstOrDefault();
                    if (DStand != null)
                    {
                        NewRow[2] = DStand.DyeStan_Value;
                    }
                    else
                    {
                        NewRow[2] = 0;
                    }
                    NewRow[3] = 0;

                    dataTable.Rows.Add(NewRow);
                }


            }
        
        }

        private void cmboDyeBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                /*
                SelectedDyeBatch= (TLDYE_DyeBatch)oCmbo.SelectedItem;
                if(SelectedDyeBatch != null)
                {
                    dataTable.Rows.Clear();

                    var MeasurementFields = _context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Hydro && !x.TLQAPF_Operator_Ins && x.TLQADPF_Process_FK == 5).ToList();
                    foreach(var MeasurementField in MeasurementFields)
                    {
                        DataRow NewRow = dataTable.NewRow();
                        NewRow[0] = MeasurementField.TLQADPF_Pk;
                        NewRow[1] = MeasurementField.TLQADPF_Description;
                        var DStand = _context.TLDYE_DyeingStandards.Where(x => x.DyeStan_QAProccessField_FK == MeasurementField.TLQADPF_Pk && x.DyeStan_Quality_FK == SelectedDyeBatch.DYEB_Greige_FK).FirstOrDefault();
                        if(DStand != null)
                        {
                            NewRow[2] = DStand.DyeStan_Value;
                        }
                        else
                        {
                            NewRow[2] = 0;
                        }
                        NewRow[3] = 0;

                        dataTable.Rows.Add(NewRow);
                    }

                }
                */

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var oBtn = (Button)sender;
            TLDYE_DyeTransactions trns = null;
            if(oBtn != null && FormLoaded)
            {
                if (SelectedDyeBatch != null)
                {
                    trns = new TLDYE_DyeTransactions();
                    trns.TLDYET_BatchNo = SelectedDyeBatch.DYEB_BatchNo;
                    trns.TLDYET_BatchWeight = SelectedDyeBatch.DYEB_BatchKG;
                    trns.TLDYET_Date = DateTime.Now;
                    trns.TLDYET_SequenceNo = SelectedDyeBatch.DYEB_SequenceNo;
                    trns.TLDYET_Batch_FK = SelectedDyeBatch.DYEB_Pk;
                    trns.TLDYET_Stage = 5;
                    trns.TLDYET_MeasurementField_FK = 0;
                    trns.TLDYET_TransactionWeight = 0;

                    _context.TLDYE_DyeTransactions.Add(trns);
                    foreach (DataRow DRow in dataTable.Rows)
                    {
                        TLDYE_NonComplianceAnalysis nca = new TLDYE_NonComplianceAnalysis();

                        var Add = true;

                        nca.TLDYEDC_Code_FK = DRow.Field<int>(0);
                        nca.TLDYEDC_BatchNo = SelectedDyeBatch.DYEB_Pk;
                        // nca.TLDYEDC_Operator_FK = (int)cmboOperator.SelectedValue;
                        // nca.TLDYEDC_PieceNo_FK = GP.GreigeP_Pk;
                        nca.TLDYEDC_Pass = true;
                        nca.TLDYEDC_NCStage = 6;
                        nca.TLDYEDC_Value = DRow.Field<int>(2);
                        nca.TLDYEDC_Date = DateTime.Now;

                        if (Add)
                            _context.TLDYE_NonComplianceAnalysis.Add(nca);


                    }
                    //*************************************************
                    SelectedDyeBatch.DYEB_Stage4 = true;
                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show("Data saved to database");
                        dataTable.Rows.Clear();
                        frmHydroResults_Load(this, null);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid Dye Batch");

                }
            }
        }

       
    }
}
