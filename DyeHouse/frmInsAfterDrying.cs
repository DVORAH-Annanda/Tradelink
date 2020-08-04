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
        DataGridViewTextBoxColumn oTxtBoxB;  // Qty Number of
        DataGridViewTextBoxColumn oTxtBoxC;  // Concern Description
        DataGridViewTextBoxColumn oTxtBoxD;  // Pk of the record in TLDye_Quality Exception
        Util core;
       UserDetails _UserAccess;

        DataTable dt;
        DataColumn column;
        BindingSource BindingSrc;

        public frmInsAfterDrying(UserDetails UserAc)
        {
            InitializeComponent();

            core = new Util();

            dt = new DataTable();
            BindingSrc = new BindingSource();

            _UserAccess = UserAc;

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
            column.DefaultValue = string.Empty ;
            dt.Columns.Add(column);

            //-----------------------
            // Col 3
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "FabWidth_Pk";
            column.Caption = "Quantity";
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
            oTxtBoxC.DataPropertyName = dt.Columns[2].ColumnName;
            oTxtBoxC.HeaderText = "Piece Number";
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns["PieceNumber"].DisplayIndex = 2;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.ValueType = typeof(Int32);
            oTxtBoxD.Visible = true;
            oTxtBoxD.Name = "Quantity";
            oTxtBoxD.DataPropertyName = dt.Columns[3].ColumnName;
            oTxtBoxD.HeaderText = "Quantity";
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns["Quantity"].DisplayIndex = 3;

            
            if (_UserAccess._QAFunction)
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
                if (++idx == 0 || idx == 1)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;

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

            using (var context = new TTI2Entities())
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
                
                cmboBatchNumber.DataSource = DBatch;
                cmboBatchNumber.ValueMember = "DYEB_Pk";
                cmboBatchNumber.DisplayMember = "DYEB_BatchNo";
                cmboBatchNumber.SelectedValue = 0;

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
                var DBatch = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if(DBatch == null)
                {
                    MessageBox.Show("Please select a batch number from the drop down box provided");
                    return;
                }

                var Fields = (TLADM_QADyeProcessFields)cmboQEMeasurements.SelectedItem;
                if (Fields == null || Fields.TLQADPF_Pk != 28)
                {
                    MessageBox.Show("Please select a measurement from the drop down box provided");
                    return;
                }        
                using ( var context = new TTI2Entities())
                {
                    
                    foreach (DataRow Row in dt.Rows)
                    {
                            TLDye_QualityException QualExp = new TLDye_QualityException();
                            if(Row.Field<int>(0) > 0)
                            {
                                QualExp = context.TLDye_QualityException.Find(Row.Field<int>(0));
                                if (QualExp != null)
                                    QualExp.TLDyeIns_Quantity = Row.Field<int>(3);
                            }
                            else
                            {
                                QualExp.TLDyeIns_QADyeProcessField_Fk = Fields.TLQADPF_Pk ;
                                QualExp.TLDyeIns_DyeBatch_Fk = DBatch.DYEB_Pk;
                                QualExp.TLDyeIns_Quantity = Row.Field<int>(3);
                                QualExp.TLDyeIns_TransactionDate = dtpStability.Value;
                                QualExp.TLDyeIns_GriegeProduct_Fk = Row.Field<int>(1);

                                context.TLDye_QualityException.Add(QualExp);
                            }
                    }
                    
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        dt.Rows.Clear();
                       
                       
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void cmboBatchNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    dt.Rows.Clear();

                    var DyeBatch = (TLDYE_DyeBatch)oCmbo.SelectedItem;
                    if (DyeBatch != null)
                    {
                        txtQuality.Text = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                        txtColour.Text = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;
                        var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                        if (Allocated != null)
                        {
                            txtDyeMachine.Text = context.TLADM_MachineDefinitions.Find(Allocated.TLDYEA_MachCode_FK).MD_Description;
                        }
                        else
                            txtDyeMachine.Text = string.Empty;

                        txtQuality.Text = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                    }
                }
            }
        }

        private void cmboPieceNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && formloaded)
            {
                var DyeBatch = (TLDYE_DyeBatch)cmboBatchNumber.SelectedItem;
                if (DyeBatch != null)
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
                                     where DyeBatch.DYEB_Pk == GProd.GreigeP_DyeBatch_FK
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

                    if (Cell.ColumnIndex == 3 )
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
            }
        }

        
    }
}
