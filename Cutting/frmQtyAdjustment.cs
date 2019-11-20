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
    public partial class frmQtyAdjustment : Form
    {
        bool FormLoaded;
        Util core;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewComboBoxColumn oCmboA;

        public frmQtyAdjustment()
        {
            InitializeComponent();
            core = new Util();

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Box Number";
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.Width = 100;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Bundle Store Qty";
            oTxtBoxC.ValueType = typeof(Int32);
            oTxtBoxC.Width = 75;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Panel Store";
            oTxtBoxD.ValueType = typeof(Int32);
            oTxtBoxD.Width = 75;

            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Sizes";
                      
            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

           

            txtAdultBoxes.KeyDown += core.txtWin_KeyDownJI;
            txtAdultBoxes.KeyPress += core.txtWin_KeyPress;
            txtBinding.KeyDown += core.txtWin_KeyDownJI;
            txtBinding.KeyPress += core.txtWin_KeyPress;
            txtKidsBoxes.KeyDown += core.txtWin_KeyDownJI;
            txtKidsBoxes.KeyPress += core.txtWin_KeyPress;
            txtRibbing.KeyDown += core.txtWin_KeyDownJI;
            txtRibbing.KeyPress += core.txtWin_KeyPress;
        }

        private void frmQtyAdjustment_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            dataGridView1.Visible = false;
            txtCutSheet.Text = string.Empty;

            
            txtAdultBoxes.Text = "0";
            txtBinding.Text = "0";
            txtKidsBoxes.Text = "0";
            txtRibbing.Text = "0";

            FormLoaded = true;

        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var CutSheet = context.TLCUT_CutSheet.Where(x => x.TLCutSH_No == txtCutSheet.Text.Trim()).FirstOrDefault();
                    if (CutSheet == null)
                    {
                        MessageBox.Show("Cut Sheet selected does not exist");
                        return;
                    }


                    var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                    if (CutSheetReceipt == null)
                    {
                        MessageBox.Show("Cut Sheet might not have been receipted yet");
                        return;
                    }

                    var xSizes = (from EUnits in context.TLCUT_ExpectedUnits
                                  join Sizes in context.TLADM_Sizes on EUnits.TLCUTE_Size_FK equals Sizes.SI_id
                                  where EUnits.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk
                                  select Sizes).ToList();

                    oCmboA.DataSource = xSizes;
                    oCmboA.ValueMember = "SI_Id";
                    oCmboA.DisplayMember = "SI_Description";

                    dataGridView1.Rows.Clear();

                    if (!dataGridView1.Visible)
                    {
                        dataGridView1.Visible = true;
                    }

                    var Boxes = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).ToList();
                    foreach (var Box in Boxes)
                    {
                        var Index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[Index].Cells[0].Value = Box.TLCUTSHRD_Pk;
                        dataGridView1.Rows[Index].Cells[1].Value = Box.TLCUTSHRD_BoxNumber;
                        dataGridView1.Rows[Index].Cells[2].Value = Box.TLCUTSHRD_Size_FK;
                        dataGridView1.Rows[Index].Cells[3].Value = Box.TLCUTSHRD_BundleQty;
                        dataGridView1.Rows[Index].Cells[4].Value = Box.TLCUTSHRD_BoxUnits;

                    }

                    var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault();
                    if (CSB != null)
                    {
                        txtAdultBoxes.Text = CSB.TLCUTSHB_AdultBoxes.ToString();
                        txtBinding.Text = CSB.TLCUTSHB_Binding.ToString();
                        txtKidsBoxes.Text = CSB.TLCUTSHB_KidBoxes.ToString();
                        txtRibbing.Text = CSB.TLCUTSHB_Ribbing.ToString();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            int CutSheetRec_Pk = 0;

            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        var BatchPk = (Int32)Row.Cells[0].Value;

                        var CutSheetRecDet = context.TLCUT_CutSheetReceiptDetail.Find(BatchPk);
                        if (CutSheetRecDet != null)
                        {
                            CutSheetRec_Pk = CutSheetRecDet.TLCUTSHRD_CutSheet_FK;

                            var Size = int.Parse(Row.Cells[2].Value.ToString());
                            var BundleQty = int.Parse(Row.Cells[3].Value.ToString());
                            var BoxedQty = int.Parse(Row.Cells[4].Value.ToString());

                            if (Size != CutSheetRecDet.TLCUTSHRD_Size_FK)
                                CutSheetRecDet.TLCUTSHRD_Size_FK = Size;
                            if (BundleQty != CutSheetRecDet.TLCUTSHRD_BundleQty)
                                CutSheetRecDet.TLCUTSHRD_BundleQty = BundleQty;
                            if (BoxedQty != CutSheetRecDet.TLCUTSHRD_BoxUnits)
                                CutSheetRecDet.TLCUTSHRD_BoxUnits = BoxedQty;
                        }
                    }

                    if (CutSheetRec_Pk != 0)
                    {
                        var CSB = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetRec_Pk).FirstOrDefault();
                        if (CSB != null)
                        {
                            if (int.Parse(txtAdultBoxes.Text) != CSB.TLCUTSHB_AdultBoxes)
                                CSB.TLCUTSHB_AdultBoxes = int.Parse(txtAdultBoxes.Text);

                            if (int.Parse(txtBinding.Text) != CSB.TLCUTSHB_Binding)
                                CSB.TLCUTSHB_Binding  = int.Parse(txtBinding.Text);

                            if (int.Parse(txtKidsBoxes.Text) != CSB.TLCUTSHB_KidBoxes)
                                CSB.TLCUTSHB_KidBoxes = int.Parse(txtKidsBoxes.Text);

                            if (int.Parse(txtRibbing.Text) != CSB.TLCUTSHB_Ribbing)
                                CSB.TLCUTSHB_Ribbing = int.Parse(txtAdultBoxes.Text);
                        }
                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        this.frmQtyAdjustment_Load(this, null);
                                            }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 2 ||
                        Cell.ColumnIndex == 3)
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
