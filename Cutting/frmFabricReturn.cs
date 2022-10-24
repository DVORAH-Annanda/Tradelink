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
    public partial class frmFabricReturn : Form
    {
        bool formloaded;

        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();   // Select                       1
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     // Piece                        2 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Quality                      3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();     // Kg Nett Production           4
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();     // Kg Nett Weight Returned      5
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();     // Reason Code                  6
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Fabric Stores                7
       

        public frmFabricReturn()
        {
            InitializeComponent();
            core = new Util();
        }

        private void frmFabricReturn_Load(object sender, EventArgs e)
        {
            formloaded = false;
            int _Width = 100;

            using (var context = new TTI2Entities())
            {
                cmboCutSheets.DataSource = context.TLCUT_CutSheet.Where(x=>!x.TLCutSH_WIPComplete).OrderBy(x => x.TLCutSH_Pk).ToList();
                cmboCutSheets.ValueMember = "TLCutSH_Pk";
                cmboCutSheets.DisplayMember = "TLCutSH_No";
                cmboCutSheets.SelectedValue = -1;

                var LNU = context.TLADM_LastNumberUsed.Find(4);
                if (LNU != null)
                {
                    label5.Text = "FR" + LNU.col2.ToString().PadLeft(5, '0');
                }
            }

            
            oTxtA.Visible = false;                      // 0
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();   // 1
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB.HeaderText = "Piece No";              // 2 
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = _Width;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Quality";               // 3
            oTxtC.ValueType = typeof(string);
            oTxtC.Width = _Width;
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Weight";                // 4 
            oTxtD.ValueType = typeof(Decimal);
            oTxtD.Width = _Width;
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Weight Returned";       // 5 
            oTxtE.ValueType = typeof(Decimal);
            oTxtE.Width = _Width;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF.HeaderText = "Reason Code";          // 6
            oTxtF.ValueType = typeof(string);
            oTxtF.Width = _Width;
            dataGridView1.Columns.Add(oTxtF);

            oCmboA.HeaderText = "To Store";            // 7
            oCmboA.ValueType = typeof(int);
            oCmboA.Width = _Width;
            oCmboA.ValueMember = "WhStore_Id";
            oCmboA.DisplayMember = "WhStore_Description";
            dataGridView1.Columns.Add(oCmboA);

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            formloaded = true;
        }

        private void cmboCutSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheets.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                                            
                        var DB = context.TLDYE_DyeBatch.Find(selected.TLCutSH_DyeBatch_FK);
                        if (DB != null)
                        {
                            txtDyeBatchNo.Text = DB.DYEB_BatchNo;
                            txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;

                            var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk);
                            foreach (var Record in Existing)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Record.DYEBD_Pk;
                             
                                var GP = context.TLKNI_GreigeProduction.Find(Record.DYEBD_GreigeProduction_FK);
                                if (GP != null)
                                {
                                    dataGridView1.Rows[index].Cells[1].Value = false;
                                    dataGridView1.Rows[index].Cells[2].Value = GP.GreigeP_PieceNo;
                                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(GP.GreigeP_Greige_Fk).TLGreige_Description;
                                    dataGridView1.Rows[index].Cells[4].Value = Math.Round(GP.GreigeP_weight, 2);
                                    dataGridView1.Rows[index].Cells[5].Value = Math.Round(GP.GreigeP_weight, 2);
                                }
                                                            }
                        }
                        var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                        if(Dept != null)
                        {
                            var Whses = context.TLADM_WhseStore.Where(x=>x.WhStore_DepartmentFK == Dept.Dep_Id).ToList();
                            foreach(var Whse in Whses)
                            {
                                if(Whse.WhStore_Code.Equals("FQS") || Whse.WhStore_Code.Equals("FS"))
                                {
                                    oCmboA.Items.Add(Whse);
                                }

                            }
                        }
                  }

                }
            }

        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;
            var Cell = oDgv.CurrentCell;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    if (Cell.ColumnIndex == 5)
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
            int CS_PK = 0; 
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(bool)row.Cells[1].Value)
                            continue;

                        var DyeB_Pk = (int)row.Cells[0].Value;

                        var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Find(DyeB_Pk);
                        if (DyeBatchDetail != null)
                        {
                            DyeBatchDetail.DYEBO_CutSheet = false;
                            DyeBatchDetail.DYEBO_QAApproved = false;

                            if (row.Cells[7].Value != null)
                            {
                                DyeBatchDetail.DYEBO_CurrentStore_FK = (int)row.Cells[7].Value;
                            }
                            else
                            {
                                var Dept = context.TLADM_Departments.FirstOrDefault(x => x.Dep_ShortCode == "CUT");
                                if (Dept != null)
                                {
                                    var TranType = context.TLADM_TranactionType.FirstOrDefault(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 100);
                                    if (TranType != null)
                                    {
                                        DyeBatchDetail.DYEBO_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                    }
                                }
                            }
                        
                            var DyeBatch = context.TLDYE_DyeBatch.Find(DyeBatchDetail.DYEBD_DyeBatch_FK);
                            if (DyeBatch != null && DyeBatch.DYEB_Closed)
                            {
                                    DyeBatch.DYEB_Closed = false;
                            }

                            var CutSheetRDetail = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == DyeB_Pk).FirstOrDefault();
                            if(CutSheetRDetail != null)
                            {
                                context.TLCUT_CutSheetReceiptDetail.Remove(CutSheetRDetail);
                            }
                        }
                    }
                    //---------------------------------------
                    //----------------------------------------------------
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        return;
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
