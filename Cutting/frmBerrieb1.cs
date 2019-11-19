using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;


namespace Cutting
{
    public partial class frmBerrieb1 : Form
    {
        Util core;
        bool formloaded;
        object[] ColumnHeadings;

        string[][] MandatoryFields;
        bool[] MandSelected;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key Operator            2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Primary Key Bundle no           3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Bundle No Description           4
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Measure1                        5
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Measure2                        6 
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Measure3                        7
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // Measure4                        8
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // Measure5                        9
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // Measure6                        10
        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn();   // Measure7                        11
        DataGridViewTextBoxColumn oTxtM = new DataGridViewTextBoxColumn();   // Measure8                        12
        DataGridViewTextBoxColumn oTxtN = new DataGridViewTextBoxColumn();   // Measure9                        13
        DataGridViewTextBoxColumn oTxtP = new DataGridViewTextBoxColumn();   // Measure10                       14
        DataGridViewTextBoxColumn oTxtQ = new DataGridViewTextBoxColumn();   // Measure11    

        int CutSheetSelected;

        public frmBerrieb1()
        {
            InitializeComponent();

            CutSheetSelected = 0;
            core = new Util();

            ColumnHeadings = new Object[15];

            ColumnHeadings[0] = oTxtA;  //  Primary Key (Record in File)
            ColumnHeadings[1] = oTxtB;  //  Primary Key Operator
            ColumnHeadings[2] = oTxtC;  //  Primary Key Bundle Number from TLCUT_CutSheetReceipt
            ColumnHeadings[3] = oTxtD;  //  Bundle Number Description
            ColumnHeadings[4] = oTxtE;  //  measure1
            ColumnHeadings[5] = oTxtF;  //  measure2
            ColumnHeadings[6] = oTxtG;  //  measure3
            ColumnHeadings[7] = oTxtH;  //  measure4
            ColumnHeadings[8] = oTxtJ;  //  measure5
            ColumnHeadings[9] = oTxtK;  //  measure6
            ColumnHeadings[10] = oTxtL; //  measure7 
            ColumnHeadings[11] = oTxtM; //  measure8
            ColumnHeadings[12] = oTxtN; //  measure9
            ColumnHeadings[13] = oTxtP; //  measure10
            ColumnHeadings[14] = oTxtQ; //  measure11

            MandatoryFields = new string[][]
                {   new string[] {"cmboCutSheet", "Please select a Cut Sheet number", "0"},
                    new string[] {"cmboOperators", "Please select a operator", "1"}
                };

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex > 3)
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

        private void frmBerrieb1_Load(object sender, EventArgs e)
        {
            formloaded = false;

            var h2 = (DataGridViewTextBoxColumn)ColumnHeadings[0];
            h2.HeaderText = "Primary Key (Record)";
            h2.Visible = false;
            h2.ValueType = typeof(int);
            dataGridView1.Columns.Add(h2);

            h2 = (DataGridViewTextBoxColumn)ColumnHeadings[1];
            h2.HeaderText = "Primary Key (Operator)";
            h2.Visible = false;
            h2.ValueType = typeof(int);
            dataGridView1.Columns.Add(h2);

            h2 = (DataGridViewTextBoxColumn)ColumnHeadings[2];
            h2.HeaderText = "Primary Key (Bundle No)";
            h2.Visible = false;
            h2.ValueType = typeof(int);
            dataGridView1.Columns.Add(h2);

            h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
            h2.HeaderText = "Bundle Description";
            h2.ReadOnly = true;
            h2.ValueType = typeof(string);
            dataGridView1.Columns.Add(h2);


            using (var context = new TTI2Entities())
            {
                var Query = from CutSheet in context.TLCUT_CutSheet
                            join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                            select new { Pk = CutReceipt.TLCUTSHR_Pk, Description = CutSheet.TLCutSH_No };

                foreach (var row in Query)
                {
                    cmboCutSheet.Items.Add(row);
                }

                cmboCutSheet.ValueMember = "Pk";
                cmboCutSheet.DisplayMember = "Description";
                cmboCutSheet.SelectedValue = -1;

                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                if (Depts != null)
                {
                    cmboOperators.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Depts.Dep_Id && !x.MachOp_Discontinued).ToList();
                    cmboOperators.ValueMember = "MachOp_Pk";
                    cmboOperators.DisplayMember = "MachOp_description";
                    cmboOperators.SelectedIndex = -1;

                    var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                    foreach (var Reason in Reasons)
                    {
                        foreach (var elementH in ColumnHeadings)
                        {
                            Type t = elementH.GetType();
                            if (t.Equals(typeof(DataGridViewCheckBoxColumn)))
                                continue;

                            var ch = (DataGridViewTextBoxColumn)elementH;
                            if (String.IsNullOrEmpty(ch.HeaderText))
                            {
                                StringBuilder sb = new StringBuilder();
                                //sb.Append(Reason.QD_ShortCode);
                                //sb.Append(Environment.NewLine);
                                sb.Append(Reason.QD_Description);
                                ch.HeaderText = sb.ToString();
                                ch.ValueType = typeof(int);
                                ch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1.Columns.Add(ch);
                                break;
                            }
                        }
                    }
                }

            }

            formloaded = true;
        }

        private void cmboOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    var xnbr = Convert.ToInt32(result[2]);
                    MandSelected[xnbr] = true;
                }
            }
        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            bool DataExists = false;
            IList<TLCUT_QCBerrie> Existing = new List<TLCUT_QCBerrie>();

            if (oCmbo != null && formloaded)
            {
               Object tst = oCmbo.SelectedItem;
               foreach (PropertyInfo prop in tst.GetType().GetProperties())
               {
                   if (prop.Name == "Pk")
                   {
                       CutSheetSelected = Convert.ToInt32(prop.GetValue(tst));
                   }
               }
               //----------------------------------------------------------
               //
               //---------------------------------------------------------------------------
               using (var context = new TTI2Entities())
               {
                   dataGridView1.Rows.Clear();

                   string CSDescription = string.Empty;
                   if (CutSheetSelected != 0)
                   {
                       var CutSheetR = context.TLCUT_CutSheetReceipt.Find(CutSheetSelected);
                       if (CutSheetR != null)
                       {
                           var CutSheet = context.TLCUT_CutSheet.Find(CutSheetR.TLCUTSHR_CutSheet_FK);
                           if (CutSheet != null)
                           {
                               CSDescription = CutSheet.TLCutSH_No;
                               var DB = context.TLDYE_DyeBatch.Find(CutSheet.TLCutSH_DyeBatch_FK);
                               if (DB != null)
                               {
                                   txtDyeBatchNo.Text = DB.DYEB_BatchNo;
                                   txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;
                                   txtQuality.Text = context.TLADM_Griege.Find(DB.DYEB_Greige_FK).TLGreige_Description;
                               }

                           }
                       }
                       
                       Existing = context.TLCUT_QCBerrie.Where(x => x.TLQCFB_CutSheetReceipt_FK == CutSheetSelected).ToList();
                       if (Existing.Count != 0)
                       {
                           cmboOperators.SelectedValue = Existing.First().TLQCFB_Operator_FK;

                           int Cnt = 0;
                           foreach (var Record in Existing)
                           {
                               var index = dataGridView1.Rows.Add();
                               dataGridView1.Rows[index].Cells[0].Value = Record.TLQCFB_Pk;
                               dataGridView1.Rows[index].Cells[1].Value = Record.TLQCFB_Operator_FK;
                               dataGridView1.Rows[index].Cells[2].Value = Record.TLQCFB_CutSheetReceipt_FK;
                               dataGridView1.Rows[index].Cells[3].Value = CSDescription + " -" + (++Cnt).ToString().PadLeft(2, '0');
                               dataGridView1.Rows[index].Cells[4].Value = Record.TLQCFB_Measure1;
                               dataGridView1.Rows[index].Cells[5].Value = Record.TLQCFB_Measure2;
                               dataGridView1.Rows[index].Cells[6].Value = Record.TLQCFB_Measure3;
                               dataGridView1.Rows[index].Cells[7].Value = Record.TLQCFB_Measure4;
                               dataGridView1.Rows[index].Cells[8].Value = Record.TLQCFB_Measure5;
                               dataGridView1.Rows[index].Cells[9].Value = Record.TLQCFB_Measure6;
                               dataGridView1.Rows[index].Cells[10].Value = Record.TLQCFB_Measure7;
                               dataGridView1.Rows[index].Cells[11].Value = Record.TLQCFB_Measure8;
                               dataGridView1.Rows[index].Cells[12].Value = Record.TLQCFB_Measure9;
                               dataGridView1.Rows[index].Cells[13].Value = Record.TLQCFB_Measure10;
                               dataGridView1.Rows[index].Cells[14].Value = Record.TLQCFB_Measure11;
                           }

                           DataExists = true;
                       }
                   }
                   //-----------------------------------------------
                   // Previous entries do not exist then get from the Cut Receipts Detail Table 
                   //-----------------------------------------------------------------------
                   if (CutSheetSelected != 0 && !DataExists)
                   {
                       
                           var Records = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetSelected).ToList();
                           foreach (var Record in Records)
                           {
                               var index = dataGridView1.Rows.Add();
                               dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTSHRD_CutSheet_FK;
                               dataGridView1.Rows[index].Cells[3].Value = Record.TLCUTSHRD_Description;
                               dataGridView1.Rows[index].Cells[4].Value = 0;
                               dataGridView1.Rows[index].Cells[5].Value = 0;
                               dataGridView1.Rows[index].Cells[6].Value = 0;
                               dataGridView1.Rows[index].Cells[7].Value = 0;
                               dataGridView1.Rows[index].Cells[8].Value = 0;
                               dataGridView1.Rows[index].Cells[9].Value = 0;
                               dataGridView1.Rows[index].Cells[10].Value = 0;
                               dataGridView1.Rows[index].Cells[11].Value = 0;
                               dataGridView1.Rows[index].Cells[12].Value = 0;
                               dataGridView1.Rows[index].Cells[13].Value = 0;
                               dataGridView1.Rows[index].Cells[14].Value = 0;
                          }
                   
                   }
               }
               var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

               if (result != null)
               {
                   var xnbr = Convert.ToInt32(result[2]);
                   MandSelected[xnbr] = true;
               }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var selected = (TLADM_MachineOperators)cmboOperators.SelectedItem;

                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                       
                        bool Add = true;
                       TLCUT_QCBerrie qcb = new TLCUT_QCBerrie();
                       if (row.Cells[0].Value != null)
                       {
                           var index = (int)row.Cells[0].Value;
                           qcb = context.TLCUT_QCBerrie.Find(index);
                           if (qcb != null)
                               Add = false;
                       }
                       qcb.TLQCFB_Operator_FK = selected.MachOp_Pk; 
                       qcb.TLQCFB_CutSheetReceipt_FK = CutSheetSelected;
                       
                       qcb.TLQCFB_Measure1 = (int)row.Cells[4].Value;
                       qcb.TLQCFB_Measure2 = (int)row.Cells[5].Value;
                       qcb.TLQCFB_Measure3 = (int)row.Cells[6].Value;
                       qcb.TLQCFB_Measure4 = (int)row.Cells[7].Value;
                       qcb.TLQCFB_Measure5 = (int)row.Cells[8].Value;
                       qcb.TLQCFB_Measure6 = (int)row.Cells[9].Value;
                       qcb.TLQCFB_Measure7 = (int)row.Cells[10].Value;
                       qcb.TLQCFB_Measure8 = (int)row.Cells[11].Value;
                       qcb.TLQCFB_Measure9 = (int)row.Cells[12].Value;
                       qcb.TLQCFB_Measure10 = (int)row.Cells[13].Value;
                       qcb.TLQCFB_Measure11 = (int)row.Cells[14].Value;

                       if (Add)
                           context.TLCUT_QCBerrie.Add(qcb);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data Saved to database successfully");
                        dataGridView1.Rows.Clear();
                        
                        cmboCutSheet.SelectedValue = -1;
                        cmboOperators.SelectedValue = -1;
                        txtColour.Text = string.Empty;
                        txtDyeBatchNo.Text = string.Empty;
                        txtQuality.Text = string.Empty;


                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
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
