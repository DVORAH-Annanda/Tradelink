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
    public partial class frmQASelection : Form
    {

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record TLCUT_CutSheetReceiptDetail  0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Description                                          1
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Size                                                 2 
        DataGridViewButtonColumn oBtnA;

        bool formloaded;
        Util core;
        public frmQASelection()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmQASelection_Load(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                oTxtA.HeaderText = "Index";
                oTxtA.ValueType = typeof(Int32);
                oTxtA.Visible = false;

                oTxtB.HeaderText = "Bundle Description";
                oTxtB.ValueType = typeof(string);
                
                oTxtC.HeaderText = "Size";
                oTxtC.ValueType = typeof(string);

                oBtnA = new DataGridViewButtonColumn();
                oBtnA.HeaderText = "QA Results";

                

                dataGridView1.Columns.Add(oTxtA);
                dataGridView1.Columns.Add(oTxtB);
                dataGridView1.Columns.Add(oTxtC);
                dataGridView1.Columns.Add(oBtnA);


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
                    cmboInspector.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Depts.Dep_Id && x.MachOp_Inspector && !x.MachOp_Discontinued ).ToList();
                    cmboInspector.ValueMember = "MachOp_Pk";
                    cmboInspector.DisplayMember = "MachOp_description";
                    cmboInspector.SelectedIndex = -1;

                   
                }
            }

            formloaded = true;
          
        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            var CutSheetSelected = 0;

            if (oCmbo != null && formloaded)
            {
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
                    if (CutSheetSelected != 0)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLCUT_CutSheetReceiptDetail.Where(X => X.TLCUTSHRD_CutSheet_FK == CutSheetSelected).ToList();
                            foreach (var Record in Existing)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Record.TLCUTSHRD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTSHRD_Description;
                                dataGridView1.Rows[index].Cells[2].Value = context.TLADM_Sizes.Find(Record.TLCUTSHRD_Size_FK).SI_Description;
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                var selected = (TLADM_MachineOperators)cmboInspector.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select an inspector from the list above");
                    return;
                }
                
                var CurrentRow = oDgv.CurrentRow;

                try
                {
                    frmQAInput qaInput = new frmQAInput((int)CurrentRow.Cells[0].Value, selected.MachOp_Pk);
                    qaInput.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
