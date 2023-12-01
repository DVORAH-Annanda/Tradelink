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

namespace CustomerServices
{
    public partial class frmStockTakeOn : Form
    {
        bool FormLoaded;
        
        //--------------------------------------------------------------------------------------------------------------
        string[][] MandatoryFields;
        bool[] MandSelected;
        Util core;
        protected readonly TTI2Entities _context;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;

        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;
        DataGridViewComboBoxColumn oCmbBoxC;
        DataGridViewComboBoxColumn oCmbBoxD;

        public frmStockTakeOn()
        {
            InitializeComponent();

            _context = new TTI2Entities();

            //0
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.HeaderText = "Box Number";
            oTxtBoxA.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtBoxA);
            //1
            oCmbBoxA = new DataGridViewComboBoxColumn();
            oCmbBoxA.HeaderText = "Style";
            dataGridView1.Columns.Add(oCmbBoxA);
            //2
            oCmbBoxB = new DataGridViewComboBoxColumn();
            oCmbBoxB.HeaderText = "Colour";
            dataGridView1.Columns.Add(oCmbBoxB);
            //3
            oCmbBoxC = new DataGridViewComboBoxColumn();
            oCmbBoxC.HeaderText = "Size";
            dataGridView1.Columns.Add(oCmbBoxC);
            //4
            oCmbBoxD = new DataGridViewComboBoxColumn();
            oCmbBoxD.HeaderText = "CMT";
            dataGridView1.Columns.Add(oCmbBoxD);
            //5
            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Weight" ;
            oTxtBoxB.ValueType = typeof(decimal);
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Boxed Quantity";
            oTxtBoxC.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Grade";
            oTxtBoxC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtBoxD);

            core = new Util();
          
            MandatoryFields = new string[][]
            {   new string[] {"0", "Please enter a Box Number", "0"},
                    new string[] {"1", "Please select the appropriate style", "1"},
                    new string[] {"2", "Please select the appropraite colour", "2"},
                    new string[] {"3", "Please select the appropraite size", "3"},
                    new string[] {"4", "Please select the appropraite CMT", "4"},
                    new string[] {"5", "Please enter the box weight", "5"},
                    new string[] {"6", "Please enter the appropiate boxed qty", "6"},
                    new string[] {"7", "Please enter a Grade", "7"},
            };
          
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void frmStockTakeOn_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboWarehouses.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                cmboWarehouses.ValueMember = "WhStore_Id";
                cmboWarehouses.DisplayMember = "WhStore_Description";
                cmboWarehouses.SelectedIndex = -1;

                oCmbBoxA.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                oCmbBoxA.ValueMember = "Sty_Id";
                oCmbBoxA.DisplayMember = "Sty_Description";

                oCmbBoxB.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                oCmbBoxB.ValueMember = "Col_Id";
                oCmbBoxB.DisplayMember = "Col_Display";

                oCmbBoxC.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                oCmbBoxC.ValueMember = "SI_Id";
                oCmbBoxC.DisplayMember = "SI_Description";

                oCmbBoxD.DataSource = context.TLADM_Departments.Where(x=>x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                oCmbBoxD.ValueMember = "Dep_Id";
                oCmbBoxD.DisplayMember = "Dep_Description";
            }

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                var Selected = (TLADM_WhseStore)cmboWarehouses.SelectedItem;
                if (Selected == null)
                {
                    MessageBox.Show("Please select a warehouse from the combobox provided");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[0].Value == null)
                            continue;

                        var RWComplete = core.RowComplete(Row, MandatoryFields);
                        var cnt = RWComplete.Where(x => x == false).Count();

                        if (cnt == MandatoryFields.Length)
                        {
                            continue;
                        }

                        if (cnt != 0)
                        {
                            var errorM = core.returnMessage(RWComplete, true, MandatoryFields);
                            if (!string.IsNullOrEmpty(errorM))
                            {
                                MessageBox.Show(errorM, "Error Message for Line No" + (Row.Index + 1).ToString());
                                return;
                            }
                        }

                        TLCSV_StockOnHand SOH = new TLCSV_StockOnHand();
                        var Style_FK = (int)Row.Cells[1].Value;
                        var Colour_FK = (int)Row.Cells[2].Value;
                        var Size_FK = (int)Row.Cells[3].Value;

                     

                        TLADM_Styles Styles = context.TLADM_Styles.Find(Style_FK);
                        TLADM_Colours Colours = context.TLADM_Colours.Find(Colour_FK);
                        TLADM_Sizes Sizes = context.TLADM_Sizes.Find(Size_FK);

                        SOH.TLSOH_PastelNumber = Styles.Sty_PastelNo + Colours.Col_FinishedCode + "NG" + Sizes.SI_PastelNo;
                        SOH.TLSOH_BoxNumber = Row.Cells[0].Value.ToString();
                        SOH.TLSOH_Style_FK = (int)Row.Cells[1].Value;
                        SOH.TLSOH_Colour_FK = (int)Row.Cells[2].Value;
                        SOH.TLSOH_Size_FK = (int)Row.Cells[3].Value;
                        SOH.TLSOH_CMT_FK = (int)Row.Cells[4].Value;
                        SOH.TLSOH_Weight = (decimal)Row.Cells[5].Value;
                        SOH.TLSOH_BoxedQty = Convert.ToInt32(Row.Cells[6].Value.ToString());
                        SOH.TLSOH_Grade = Row.Cells[7].Value.ToString().ToUpper().Trim();
                        SOH.TLSOH_BoxType = 1;
                        SOH.TLSOH_DateIntoStock = dtpTransdate.Value;
                        SOH.TLSOH_WareHouse_FK = Selected.WhStore_Id;
                        if (SOH.TLSOH_Grade.Contains("A") )
                            SOH.TLSOH_Is_A = true;

                        var Cwork = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_BoxNumber == SOH.TLSOH_BoxNumber).FirstOrDefault();
                        if (Cwork != null)
                            SOH.TLSOH_CutSheet_FK = Cwork.TLCMTWC_CutSheet_FK;

                        var Already = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == SOH.TLSOH_BoxNumber).FirstOrDefault();
                        if (Already != null)
                        {
                            if(!Already.TLSOH_Write_Off) 
                                continue;
                        }

                        context.TLCSV_StockOnHand.Add(SOH);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully save to the database");
                        cmboWarehouses.SelectedItem = -1;
                        dataGridView1.Rows.Clear();
                       
                        frmStockTakeOn_Load(this, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;
            if (oDgv != null && FormLoaded)
            {
                var CurrentRow = oDgv.CurrentRow;
                if (CurrentRow != null)
                {
                    complete = core.RowComplete(CurrentRow, MandatoryFields);
                }

                int InCorrect = complete.Where(x => x == false).Count();

                if (InCorrect != MandatoryFields.Count())
                {
                    MandSelected = complete;

                    var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                    if (!string.IsNullOrEmpty(errorM))
                    {
                        MessageBox.Show(errorM);
                        return;
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox oCombo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
                if (oDgv != null && oDgv.Focused)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 5)
                    {
                        e.Control.KeyDown -= core.txtWin_KeyDownOEM;
                        e.Control.KeyPress -= core.txtWin_KeyPress;
                        e.Control.KeyDown += core.txtWin_KeyDownOEM;
                        e.Control.KeyPress += core.txtWin_KeyPress;
                    }
                    else if (Cell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= core.txtWin_KeyDownJI;
                        e.Control.KeyPress -= core.txtWin_KeyPress;
                        e.Control.KeyDown += core.txtWin_KeyDownJI;
                        e.Control.KeyPress += core.txtWin_KeyPress;
                    }
                    else
                    {
                        e.Control.KeyDown -= core.txtWin_KeyDownOEM;
                        e.Control.KeyPress -= core.txtWin_KeyPress;
                        e.Control.KeyDown -= core.txtWin_KeyDownJI;
                        // e.Control.KeyPress += core.txtWin_KeyPress;
                    }
                }
            }
            else
            {
                if (oDgv.CurrentCell is DataGridViewComboBoxCell)
                {
                    oCombo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var Cell = dataGridView1.CurrentCell;
            var CurrentRow = dataGridView1.CurrentRow;
            if (cb != null)
            {
                if (Cell.ColumnIndex == 1)
                {
                    var SelItem = (TLADM_Styles)cb.SelectedItem;

                    if (SelItem != null)
                    {
                         var tst = (from T1 in _context.TLADM_StyleColour
                                       join T2 in _context.TLADM_Colours
                                       on T1.STYCOL_Colour_FK equals T2.Col_Id
                                       where T1.STYCOL_Style_FK == SelItem.Sty_Id
                                       orderby T2.Col_Display
                                       select T2).ToList();

                        oCmbBoxB.DataSource = tst;
                        oCmbBoxB.HeaderText = "Colours";
                        oCmbBoxB.ValueMember = "Col_Id";
                        oCmbBoxB.DisplayMember = "Col_Display";
                      

                    }
                }
            }
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (e.ColumnIndex == 0)
            {
                String Message = "Please enter a box number in the format xxxxx-xx";

                var Cell = oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                if (Cell.ToString().Length < 8)
                {
                    MessageBox.Show(Message, "Error Message");
                    e.Cancel = true;
                }
                else if (!Cell.ToString().Contains('-'))
                {
                    MessageBox.Show(Message, "Error Message");
                    e.Cancel = true;
                }
                else
                {
                    var Index = Cell.ToString().IndexOf('-');
                    if (Cell.ToString().Length - Index < 2)
                    {
                        MessageBox.Show(Message, "Error Message");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void frmStockTakeOn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
