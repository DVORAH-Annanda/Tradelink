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
using System.Reflection;


namespace Cutting
{
    public partial class frmFleeceData : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();    // 0 Pk of Record in file
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();    // 1 Pk of Description 1 = Fleece 2 = Fleece Waistbands
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();    // 2 Pk of size  
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();    // 3 Size Description  
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();    // 4 Kg  
        Util core;

        bool formloaded;
        int CutSheetSelected = 0;
        int fleeceTypeSelected = 0;


        public frmFleeceData()
        {
            formloaded = false;
            CutSheetSelected = 0;

            InitializeComponent();
            core = new Util();

            using (var context = new TTI2Entities())
            {
                var Query = from CutSheet in context.TLCUT_CutSheet
                            join CutReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutReceipt.TLCUTSHR_CutSheet_FK
                            select new { Pk = CutSheet.TLCutSH_Pk, Description = CutSheet.TLCutSH_No };

                foreach (var row in Query)
                {
                    cmboCutSheet.Items.Add(row);
                }

               
                cmboCutSheet.ValueMember = "Pk";
                cmboCutSheet.DisplayMember = "Description";
                cmboCutSheet.SelectedValue = -1;


                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oTxtB.Visible = false;
                oTxtB.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC.Visible = false;
                oTxtC.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtC);

                oTxtD.HeaderText = "Sizes";
                oTxtD.ValueType = typeof(string);
                oTxtD.ReadOnly = true;
                dataGridView1.Columns.Add(oTxtD);

                oTxtE.HeaderText = "Kgs";
                oTxtE.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtE);

                var Sizes = context.TLADM_Sizes.OrderBy(x => x.SI_id).ToList();
                foreach (var Record in Sizes)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = 0;
                    dataGridView1.Rows[index].Cells[1].Value = 0;
                    dataGridView1.Rows[index].Cells[2].Value = Record.SI_id;
                    dataGridView1.Rows[index].Cells[3].Value = Record.SI_Description;
                    dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                }
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            formloaded = true;

        }

        private void frmFleeceData_Load(object sender, EventArgs e)
        {
            formloaded = false;

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Fleece Cuffs"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Fleece Waistbands"));
            cmboCuffsorWaist.DataSource = reportOptions;
            cmboCuffsorWaist.ValueMember = "Key";
            cmboCuffsorWaist.DisplayMember = "Value";
            cmboCuffsorWaist.SelectedIndex = -1;
            formloaded = true;
            
                              

        }

         private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 3)
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

         private void button1_Click(object sender, EventArgs e)
         {
             Button oBtn = sender as Button;
             if (oBtn != null)
             {
                 if (fleeceTypeSelected == 0)
                 {
                     MessageBox.Show("Please select either a Cuff or a Waistband as appropriate");
                     return;

                 }

                 if (CutSheetSelected == 0)
                 {
                     MessageBox.Show("Please select a cut sheet");
                     return;
                 }

                 using (var context = new TTI2Entities())
                 {
                     foreach (DataGridViewRow row in dataGridView1.Rows)
                     {
                         if (Convert.ToDecimal(row.Cells[4].Value.ToString()) == 0.00M)
                             continue;

                         TLCUT_CutFleeceStats fleeceStats = new TLCUT_CutFleeceStats();
                         bool Add = true;

                         if ((int)row.Cells[0].Value != 0)
                         {
                             var index = (int)row.Cells[0].Value;
                             fleeceStats = context.TLCUT_CutFleeceStats.Find(index);
                             if (fleeceStats == null)
                                 fleeceStats = new TLCUT_CutFleeceStats();
                             else
                                 Add = false;

                         }

                         fleeceStats.TLFCFW_Pk = (int)row.Cells[0].Value;
                         fleeceStats.TLFCFW_Size_FK = (int)row.Cells[2].Value;
                         fleeceStats.TLFCFW_FleeceType = fleeceTypeSelected;
                         fleeceStats.TLFCFW_CutSheet_FK = CutSheetSelected;
                         fleeceStats.TLFCFW_Kgs = Convert.ToDecimal(row.Cells[4].Value);

                         if (Add)
                             context.TLCUT_CutFleeceStats.Add(fleeceStats);
                     }

                     try
                     {
                         context.SaveChanges();
                         MessageBox.Show("Data saved successfully to database");
                         dataGridView1.Rows.Clear();
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

         private void cmboCuffsorWaist_SelectedIndexChanged(object sender, EventArgs e)
         {
             ComboBox oCmbo = sender as ComboBox;
             if (oCmbo != null && formloaded)
             {
                 fleeceTypeSelected = Convert.ToInt32(oCmbo.SelectedValue);
             }
         }

         private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
         {
             ComboBox oCmbo = sender as ComboBox;
             if (oCmbo != null && formloaded)
             {
                 dataGridView1.Rows.Clear();
                 Object tst = oCmbo.SelectedItem;
                 foreach (PropertyInfo prop in tst.GetType().GetProperties())
                 {
                     if (prop.Name == "Pk")
                     {
                         CutSheetSelected = Convert.ToInt32(prop.GetValue(tst));
                     }
                 }

                 if (CutSheetSelected != 0 && fleeceTypeSelected != 0)
                 {
                     using (var context = new TTI2Entities())
                     {
                         var Existing = context.TLCUT_CutFleeceStats.Where(x => x.TLFCFW_FleeceType == fleeceTypeSelected &&
                                                                            x.TLFCFW_CutSheet_FK == CutSheetSelected).ToList();

                         var Sizes = context.TLADM_Sizes.OrderBy(x => x.SI_id).ToList();
                         foreach (var record in Sizes)
                         {
                             var index = dataGridView1.Rows.Add();
                             var Exist = Existing.Find(x => x.TLFCFW_Size_FK == record.SI_id);
                             if (Exist != null)
                             {
                                 dataGridView1.Rows[index].Cells[0].Value = Exist.TLFCFW_Pk;
                                 dataGridView1.Rows[index].Cells[1].Value = Exist.TLFCFW_FleeceType;
                                 dataGridView1.Rows[index].Cells[2].Value = Exist.TLFCFW_Size_FK;
                                 dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Sizes.Find(Exist.TLFCFW_Size_FK).SI_Description;
                                 dataGridView1.Rows[index].Cells[4].Value = Exist.TLFCFW_Kgs;
                             }
                             else
                             {
                                 dataGridView1.Rows[index].Cells[0].Value = 0;
                                 dataGridView1.Rows[index].Cells[1].Value = fleeceTypeSelected;
                                 dataGridView1.Rows[index].Cells[2].Value = record.SI_id;
                                 dataGridView1.Rows[index].Cells[3].Value = record.SI_Description;
                                 dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                             }
                         }

                     } 
                 }
             }
         }
    }
}
