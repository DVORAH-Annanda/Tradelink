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

        public frmInsAfterDrying(UserDetails UserAc)
        {
            InitializeComponent();

            core = new Util();

            _UserAccess = UserAc;
            
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.Visible = false;
            oTxtBoxA.HeaderText = "Pk";
            dataGridView1.Columns.Add(oTxtBoxA);


            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ValueType = typeof(Int32);
            oTxtBoxB.HeaderText = "Number Of";
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.HeaderText = "Concerns";
            oTxtBoxC.Width = 200;
            dataGridView1.Columns.Add(oTxtBoxC);


            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.ValueType = typeof(Int32);
            oTxtBoxD.Visible = false;
            oTxtBoxD.HeaderText = "Pk";
            dataGridView1.Columns.Add(oTxtBoxD);

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
                

                var QAProcessItems = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 4).ToList();
                foreach (var Record in QAProcessItems)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Record.TLQADPF_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = 0;
                    dataGridView1.Rows[index].Cells[3].Value = Record.TLQADPF_Description;
                    dataGridView1.Rows[index].Cells[4].Value = null;
                }

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

                using ( var context = new TTI2Entities())
                {
                    if (!_UserAccess._QAFunction)
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if ((bool)Row.Cells[1].Value == false)
                                continue;

                            TLDye_QualityException QualExp = new TLDye_QualityException();
                            if (Row.Cells[4].Value != null)
                            {
                                QualExp = context.TLDye_QualityException.Find((int)Row.Cells[4].Value);
                                if (QualExp != null)
                                    QualExp.TLDyeIns_Quantity = (int)Row.Cells[2].Value;
                            }
                            else
                            {
                                QualExp.TLDyeIns_QADyeProcessField_Fk = (int)Row.Cells[0].Value;
                                QualExp.TLDyeIns_DyeBatch_Fk = DBatch.DYEB_Pk;
                                QualExp.TLDyeIns_Quantity = (int)Row.Cells[2].Value;
                                QualExp.TLDyeIns_TransactionDate = dtpStability.Value;

                                context.TLDye_QualityException.Add(QualExp);
                            }
                        }
                    }
                    else
                    {
                        var DB = context.TLDYE_DyeBatch.Find(DBatch.DYEB_Pk);
                        if (DB != null)
                        {
                            DB.DYEB_QExceptionCause = txtCauses.Text;
                        }
                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        dataGridView1.Rows.Clear();
                        this.frmInsAfterDrying_Load(this, null);

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

                        var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x=>x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).FirstOrDefault();
                        if (DyeBatchDetail != null)
                        {
                            var GriegeProd = context.TLKNI_GreigeProduction.Find(DyeBatchDetail.DYEBD_GreigeProduction_FK);
                            if (GriegeProd != null && GriegeProd.GreigeP_Machine_FK != null)
                            {
                                txtKnittingMachine.Text = context.TLADM_MachineDefinitions.Find(GriegeProd.GreigeP_Machine_FK).MD_Description;
                            }
                            else
                                txtKnittingMachine.Text = string.Empty;
                        }
                    }

                    // We need to see whether an existing record exists in TLDYE
                    //===========================================================

                    var Existing = context.TLDye_QualityException.Where(x => x.TLDyeIns_DyeBatch_Fk == DyeBatch.DYEB_Pk).ToList();
                    foreach (var Item in Existing)
                    {
                        var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                         where (int)Rows.Cells[0].Value == Item.TLDyeIns_QADyeProcessField_Fk
                                         select Rows).FirstOrDefault();

                        if (SingleRow != null)
                        {
                            SingleRow.Cells[1].Value = true;
                            SingleRow.Cells[2].Value = Item.TLDyeIns_Quantity;
                            SingleRow.Cells[4].Value = Item.TLDyeIns_Pk;
                         
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

                    if (Cell.ColumnIndex == 2 )
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
