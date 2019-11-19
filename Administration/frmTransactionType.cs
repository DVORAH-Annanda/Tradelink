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

namespace Administration
{
    public partial class frmTransactionType : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewComboBoxColumn oCmbA;
        DataGridViewComboBoxColumn oCmbB;
        DataGridViewComboBoxColumn oCmbC;
        bool formLoaded;
        Util core;

        public frmTransactionType()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            core = new Util();
            formLoaded = false;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Transaction No";
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Description";
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            using (var context = new TTI2Entities())
            {
                cmbDepartments.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                cmbDepartments.DisplayMember = "Dep_description";
                cmbDepartments.ValueMember = "Dep_Id";

                oCmbA = new DataGridViewComboBoxColumn();
                oCmbA.DataSource = context.TLADM_WhseStore.Where(x=>!x.WhStore_WhseOrStore).OrderBy(x=>x.WhStore_Description).ToList();
                oCmbA.DisplayMember = "WhStore_Description";
                oCmbA.ValueMember = "WhStore_Id";
                oCmbA.HeaderText = "From Store";

                oCmbB = new DataGridViewComboBoxColumn();
                oCmbB.DataSource = context.TLADM_WhseStore.Where(x => !x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                oCmbB.DisplayMember = "WhStore_Description";
                oCmbB.ValueMember = "WhStore_Id";
                oCmbB.HeaderText = "To Store";

                oCmbC = new DataGridViewComboBoxColumn();
                oCmbC.DataSource = context.TLADM_FinishedGoods.OrderBy(x => x.Fin_Description).ToList();
                oCmbC.DisplayMember = "Fin_Description";
                oCmbC.ValueMember = "Fin_Pk";
                oCmbC.HeaderText = "Finshed Goods";
            }

           
            dataGridView1.Columns.Add(oTxtBoxA);    // 0 pk
            dataGridView1.Columns.Add(oTxtBoxB);    // 1 Transaction No 
            dataGridView1.Columns.Add(oTxtBoxC);    // 2 Description
            dataGridView1.Columns.Add(oCmbA);       // 3 From Whse
            dataGridView1.Columns.Add(oCmbB);       // 4 To Whse
            dataGridView1.Columns.Add(oCmbC);       // 5 Finished Goods

            formLoaded = true;

        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formLoaded)
            {
                dataGridView1.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    var selected = (TLADM_Departments)cmbDepartments.SelectedItem;
                    if (selected != null)
                    {
                        var Existing = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == selected.Dep_Id).OrderBy(x=>x.TrxT_Number).ToList();
                        foreach (var Row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Row.TrxT_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Row.TrxT_Number;
                            dataGridView1.Rows[index].Cells[2].Value = Row.TrxT_Description;
                            dataGridView1.Rows[index].Cells[3].Value = Row.TrxT_FromWhse_FK;
                            dataGridView1.Rows[index].Cells[4].Value = Row.TrxT_ToWhse_FK;
                            dataGridView1.Rows[index].Cells[5].Value = Row.TrxT_FinishedGoods_FK;
                        }
                    }
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool AddRec;
            bool success = true;

            if (oBtn != null && formLoaded)
            {
                var SelectedDept = (TLADM_Departments)cmbDepartments.SelectedItem;
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        TLADM_TranactionType tranType = new TLADM_TranactionType();
                        AddRec = true;
                        if (row.Cells[0].Value != null)
                        {
                            tranType = context.TLADM_TranactionType.Find((int)row.Cells[0].Value);
                            if (tranType != null)
                            {
                                AddRec = false;
                            }
                        }

                        tranType.TrxT_Number = (int)row.Cells[1].Value;
                        tranType.TrxT_Description = (string)row.Cells[2].Value;
                        tranType.TrxT_FromWhse_FK = null;
                        tranType.TrxT_ToWhse_FK = null;

                        if (SelectedDept != null)
                            tranType.TrxT_Department_FK = SelectedDept.Dep_Id;

                        if (row.Cells[3].Value != null)
                        {
                            tranType.TrxT_FromWhse_FK = (int)row.Cells[3].Value;
                        }

                        if (row.Cells[4].Value != null)
                        {
                            tranType.TrxT_ToWhse_FK = (int)row.Cells[4].Value;
                        }

                        if (row.Cells[5].Value != null)
                        {
                            tranType.TrxT_FinishedGoods_FK = (int)row.Cells[5].Value;
                        }
                        else
                            tranType.TrxT_FinishedGoods_FK = 0;

                        if (AddRec)
                            context.TLADM_TranactionType.Add(tranType);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                }
                if (success)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Records Stored to database");
                  
                }
            }
        }
    }
}
