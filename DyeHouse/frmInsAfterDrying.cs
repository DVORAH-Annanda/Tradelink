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
    public partial class frmInsAfterDrying : Form
    {
        bool formloaded;

        DataGridViewTextBoxColumn oTxtBoxA;  // Pk Concern Record 
        DataGridViewCheckBoxColumn oChkA;    // true / False
        int TextBWidth = 250;
        DataGridViewTextBoxColumn oTxtBoxB;  // Qty Number of
        DataGridViewTextBoxColumn oTxtBoxC;  // Concern Description
        DataGridViewTextBoxColumn oTxtBoxD;  // Pk of the record in TLDye_Quality Exception
        Util core;
        UserDetails _UserAccess;
        TLDYE_DyeBatch DyeBatchSelected;
        bool _InspectType;

        DataTable dt;
        DataColumn column;
        BindingSource BindingSrc;

        protected readonly TTI2Entities _context;

        public frmInsAfterDrying(UserDetails UserAc, bool InspectType)
        {
            //**************************************************
            // If Inspect type is true then the function must 
            //    be as before 
            // else
            //    must be set up to record the dyeingInspection weight asw before cutting
            //***********************************************************
            InitializeComponent();
            
            _context = new TTI2Entities();
            core = new Util();

            dt = new DataTable();
            BindingSrc = new BindingSource();

            _UserAccess = UserAc;
            _InspectType = InspectType;

            if (_InspectType)
            {
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Measurement_Pk";
                column.Caption = "Measuement key";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "GreigeProduction_Pk";
                column.Caption = "GreigeProduction Pk";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //--------------------------------------------------------
                // Col 2
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "PieceNo";
                column.Caption = "Piece Number";
                column.DefaultValue = string.Empty;
                dt.Columns.Add(column);

                //-----------------------
                // Col 3
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "FabWidth_Pk";
                column.Caption = "Measurement";
                column.DefaultValue = 0;
                dt.Columns.Add(column);


                oTxtBoxA = new DataGridViewTextBoxColumn();
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.Visible = false;
                oTxtBoxA.Name = "MeasurementIndex";
                oTxtBoxA.DataPropertyName = dt.Columns[0].ColumnName;
                oTxtBoxA.HeaderText = "Pk";
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns["MeasurementIndex"].DisplayIndex = 0;

                oTxtBoxB = new DataGridViewTextBoxColumn();
                oTxtBoxB.ValueType = typeof(Int32);
                oTxtBoxB.Visible = false;
                oTxtBoxB.Name = "GreigeIndex";
                oTxtBoxB.DataPropertyName = dt.Columns[1].ColumnName;
                oTxtBoxB.HeaderText = "Pk";
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns["GreigeIndex"].DisplayIndex = 1;

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.ValueType = typeof(Int32);
                oTxtBoxC.Visible = true;
                oTxtBoxC.ReadOnly = true;
                oTxtBoxC.Name = "PieceNumber";
                oTxtBoxC.Width = TextBWidth;
                oTxtBoxC.DataPropertyName = dt.Columns[2].ColumnName;
                oTxtBoxC.HeaderText = "Piece Number";
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns["PieceNumber"].DisplayIndex = 2;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.ValueType = typeof(Int32);
                oTxtBoxD.Visible = true;
                oTxtBoxD.Name = "Quantity";
                oTxtBoxD.Width = TextBWidth;
                oTxtBoxD.DataPropertyName = dt.Columns[3].ColumnName;
                oTxtBoxD.HeaderText = "Quantity";
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns["Quantity"].DisplayIndex = 3;

            }
            else
            {
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "DyeBatchDetail_Pk";
                column.Caption = "DyeBatchDetail key";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //--------------------------------------------------------
                // Col 1
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "PieceNo";
                column.Caption = "Piece Number";
                column.DefaultValue = string.Empty;
                dt.Columns.Add(column);
                
                //-----------------------
                // Col 2
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "Quantity_Pk";
                column.Caption = "Width Measurement";
                column.DefaultValue = 0.00M;
                dt.Columns.Add(column);

                oTxtBoxA = new DataGridViewTextBoxColumn();
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.Visible = false;
                oTxtBoxA.Name = "MeasurementIndex";
                oTxtBoxA.DataPropertyName = dt.Columns[0].ColumnName;
                oTxtBoxA.HeaderText = "Pk";
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;

                oTxtBoxB = new DataGridViewTextBoxColumn();
                oTxtBoxB.ValueType = typeof(string);
                oTxtBoxB.Visible = true;
                oTxtBoxB.ReadOnly = true;
                oTxtBoxB.Name = "PieceNumber";
                oTxtBoxB.Width = TextBWidth;
                oTxtBoxB.DataPropertyName = dt.Columns[1].ColumnName;
                oTxtBoxB.HeaderText = "Piece Number";
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.ValueType = typeof(decimal);
                oTxtBoxC.Visible = true;
                oTxtBoxC.Name = "Width_Measurement";
                oTxtBoxC.Width = TextBWidth;
                oTxtBoxC.DataPropertyName = dt.Columns[2].ColumnName;
                oTxtBoxC.HeaderText = "Width Measurement";
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[2].DisplayIndex = 2;


            }
            if (_UserAccess._QAFunction && _InspectType)
            {
                dataGridView1.Enabled = false;
                txtCauses.Visible = true;
                label7.Visible = true; 
            }
            else
            {
                txtCauses.Visible = false;
                label7.Visible = false;
 
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (_InspectType)
                {
                    if (++idx == 0 || idx == 1)
                        dataGridView1.Columns[idx].Visible = false;
                    else
                        dataGridView1.Columns[idx].HeaderText = col.Caption;
                }
                else
                {
                    if (++idx == 0 )
                        dataGridView1.Columns[idx].Visible = false;
                    else
                        dataGridView1.Columns[idx].HeaderText = col.Caption;
                }
            }
        }

        private void frmInsAfterDrying_Load(object sender, EventArgs e)
        {
            formloaded = false;
            IList<TLDYE_DyeBatch> DBatch = null;
 
            txtCauses.Text = string.Empty;
            txtColour.Text = string.Empty;
            txtDyeMachine.Text = string.Empty;
            txtKnittingMachine.Text = string.Empty;
            txtQuality.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
            DyeBatchSelected = null;

            using (var context = new TTI2Entities())
            {
                if(_InspectType)
                {
                    this.Text = "Inspection After Drying";
                }
                else
                {
                    this.label8.Visible = false;
                    cmboQEMeasurements.Visible = false;
                    this.Text = "Fabric Width after Dyeing before Cutting ";
                }

                if (_InspectType)
                {
                    if (!_UserAccess._QAFunction)
                    {
                        DateTime FromDate = DateTime.Now.AddDays(-1 * DateTime.Now.Day);
                        FromDate = FromDate.AddDays(1).AddMonths(-3);
                        DBatch = context.TLDYE_DyeBatch.Where(x => x.DYEB_Transfered && x.DYEB_Allocated && x.DYEB_Stage1 && x.DYEB_BatchDate >= FromDate).OrderBy(x => x.DYEB_BatchNo).ToList();
                    }
                    else
                    {
                        DBatch = (from DB in context.TLDYE_DyeBatch
                                 join QE in context.TLDye_QualityException on DB.DYEB_Pk equals QE.TLDyeIns_DyeBatch_Fk
                                 where DB.DYEB_QExceptionCause.Length == 0
                                 select DB).ToList();
                       
                    }
                }
                else
                {
                    DateTime FromDate = DateTime.Now.AddDays(-1 * DateTime.Now.Day);
                    FromDate = FromDate.AddDays(1).AddMonths(-6);
                    if(DBatch == null)
                    {
                        DBatch = new List<TLDYE_DyeBatch>();
                    }
                    var Details = (from T1 in context.TLDYE_DyeBatch
                               join T2 in context.TLDYE_DyeBatchDetails
                               on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                               where T1.DYEB_BatchDate >= FromDate && T2.DYEBO_FWAtCutting == 0 && T2.DYEBD_BodyTrim
                               select T1).GroupBy(x => x.DYEB_Pk).ToList();
                    
                    foreach(var Detail in Details)
                    {
                        var dt = Detail.FirstOrDefault();
                        DBatch.Add(dt);
                    }
                }
                /*
                cmboBatchNumber.DataSource = DBatch;
                cmboBatchNumber.ValueMember = "DYEB_Pk";
                cmboBatchNumber.DisplayMember = "DYEB_BatchNo";
                cmboBatchNumber.SelectedValue = 0;
                */

                cmboQEMeasurements.DataSource = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 4).ToList();
                cmboQEMeasurements.ValueMember = "TLQADPF_Process_FK";
                cmboQEMeasurements.DisplayMember = "TLQADPF_Description";
                cmboQEMeasurements.SelectedValue = -1;
            }
          
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && formloaded)
            {
                if(DyeBatchSelected == null)
                {
                    MessageBox.Show("Please enter a Dye Batch Number");
                    return;
                }
               
                    if (_InspectType)
                    {
                        var Fields = (TLADM_QADyeProcessFields)cmboQEMeasurements.SelectedItem;
                        if (Fields == null || Fields.TLQADPF_Pk != 28)
                        {
                            MessageBox.Show("Please select a measurement from the drop down box provided");
                            return;
                        }


                        foreach (DataRow Row in dt.Rows)
                        {
                            TLDye_QualityException QualExp = new TLDye_QualityException();
                            if (Row.Field<int>(0) > 0)
                            {
                                QualExp = _context.TLDye_QualityException.Find(Row.Field<int>(0));
                                if (QualExp != null)
                                    QualExp.TLDyeIns_Quantity = Row.Field<int>(3);
                            }
                            else
                            {
                                QualExp.TLDyeIns_QADyeProcessField_Fk = Fields.TLQADPF_Pk;
                                QualExp.TLDyeIns_DyeBatch_Fk = DyeBatchSelected.DYEB_Pk;
                                QualExp.TLDyeIns_Quantity = Row.Field<int>(3);
                                QualExp.TLDyeIns_TransactionDate = dtpStability.Value;
                                QualExp.TLDyeIns_GriegeProduct_Fk = Row.Field<int>(1);

                                _context.TLDye_QualityException.Add(QualExp);
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            if(Row.Field<decimal>(2) == 0.00M)
                            {
                                continue;
                            }
                            var DBDetails = _context.TLDYE_DyeBatchDetails.Find(Row.Field<int>(0));
                            if(DBDetails != null)
                            {
                                DBDetails.DYEBO_FWAtCutting = Row.Field<decimal>(2);
                            }
                        }
                    }
                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        dt.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }
                
            }
        }

        private void cmboBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
             

            }
        }

        private void cmboPieceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                if(DyeBatchSelected == null)
                {
                    MessageBox.Show("Please select a Dye Batch Number");
                    return;
                }

                if (DyeBatchSelected != null)
                {
                    var SelectedItem = (TLADM_QADyeProcessFields)oCmbo.SelectedItem;
                    if (SelectedItem != null)
                    {
                        dt.Rows.Clear();
                        using (var context = new TTI2Entities())
                        {
                            var Records = (from GProd in context.TLKNI_GreigeProduction
                                     join DBatch in context.TLDYE_DyeBatchDetails
                                     on GProd.GreigeP_Pk equals DBatch.DYEBD_GreigeProduction_FK
                                     where DyeBatchSelected.DYEB_Pk == GProd.GreigeP_DyeBatch_FK
                                     select GProd).OrderBy(x => x.GreigeP_PieceNo).ToList();

                            foreach(var Record in Records)
                            {
                                DataRow dr = dt.NewRow();

                                dr[0] = 0;
                                dr[1] = Record.GreigeP_Pk;
                                dr[2] = Record.GreigeP_PieceNo;
                                dr[3] = 0;

                                var QualExp = context.TLDye_QualityException.FirstOrDefault(x=>x.TLDyeIns_GriegeProduct_Fk == Record.GreigeP_Pk);
                                if(QualExp != null)
                                {
                                    dr[0] = QualExp.TLDyeIns_Pk;
                                    dr[3] = QualExp.TLDyeIns_Quantity; 
                                }
                                dt.Rows.Add(dr);
                            }
                        
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (isChecked)
                {
                    oDgv.CurrentRow.Cells[2].Value = 1;
                    oDgv.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                    oDgv.CurrentRow.Cells[e.ColumnIndex + 1].Selected = true;
                }
                else
                {
                    oDgv.CurrentRow.Cells[2].Value = 0;

                    if (oDgv.CurrentRow.Cells[4].Value != null)
                    {
                        oDgv.CurrentRow.Cells[2].Value = 0;
                        using (var context = new TTI2Entities())
                        {
                            var Pk = (int)oDgv.CurrentRow.Cells[4].Value;
                            var QualExp = context.TLDye_QualityException.Find(Pk);
                            if (QualExp != null)
                            {
                                context.TLDye_QualityException.Remove(QualExp);
                                context.SaveChanges();
                            }
                        }
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

                    if (_InspectType)
                    {
                        if (Cell.ColumnIndex == 3)
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
                        }
                    }
                    else
                    {
                        if (Cell.ColumnIndex == 2)
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

        private void frmInsAfterDrying_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
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
            if (oBtn != null && formloaded)
            {
                if (txtBatchNumber.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Dye Batch Number");
                    return;
                }

                DyeBatchSelected = _context.TLDYE_DyeBatch.FirstOrDefault(x => x.DYEB_BatchNo == txtBatchNumber.Text);

                if (DyeBatchSelected == null)
                {
                    MessageBox.Show("Please enter a valid Dye Batch number");
                    return;
                }

                dt.Rows.Clear();


                txtQuality.Text = _context.TLADM_Griege.Find(DyeBatchSelected.DYEB_Greige_FK).TLGreige_Description;
                txtColour.Text = _context.TLADM_Colours.Find(DyeBatchSelected.DYEB_Colour_FK).Col_Display;
                var Allocated = _context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatchSelected.DYEB_Pk).FirstOrDefault();
                if (Allocated != null)
                {
                    txtDyeMachine.Text = _context.TLADM_MachineDefinitions.Find(Allocated.TLDYEA_MachCode_FK).MD_Description;
                }
                else
                {
                    txtDyeMachine.Text = string.Empty;
                }
                
                txtQuality.Text = _context.TLADM_Griege.Find(DyeBatchSelected.DYEB_Greige_FK).TLGreige_Description;


                if (!_InspectType)
                {
                    var BatchDetails = _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatchSelected.DYEB_Pk && x.DYEBO_FWAtCutting == 0 && x.DYEBD_BodyTrim).ToList();
                    if (BatchDetails.Count != 0)
                    {
                        foreach (var Det in BatchDetails)
                        {
                                DataRow dr = dt.NewRow();
                                dr[0] = Det.DYEBD_Pk;
                                dr[1] = _context.TLKNI_GreigeProduction.Find(Det.DYEBD_GreigeProduction_FK).GreigeP_PieceNo.ToString();
                                dr[2] = 0.00M;
                                dt.Rows.Add(dr);
                        }
                    }
                }
               
            }
        }
    }
}
