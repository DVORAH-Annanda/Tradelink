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
    public partial class frmDepartmentAreas : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        bool formloaded;
        

        public frmDepartmentAreas()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
           
            using (var context = new TTI2Entities())
            {
                cmbDepartments.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                cmbDepartments.DisplayMember = "Dep_description";
                cmbDepartments.ValueMember = "Dep_Id";
            }

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Area Description";
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oTxtBoxB.Width = 450;
            
            dataGridView1.Columns.Add(oTxtBoxA);    // 0 pk
            dataGridView1.Columns.Add(oTxtBoxB);    // 1 Department Area
            dataGridView1.Width = 450;

            formloaded = true;

        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                var selectedDept = (TLADM_Departments)cmbDepartments.SelectedItem;
                if (selectedDept != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        
                        var Existing = context.TLADM_DepartmentsArea.Where(x => x.DeptA_Dep_Fk == selectedDept.Dep_Id).OrderBy(x => x.DeptA_Description).ToList();
                        foreach (var Row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Row.DeptA_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = Row.DeptA_Description;
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
            if (oBtn != null && formloaded)
            {
                var SelectedDept = (TLADM_Departments)cmbDepartments.SelectedItem;
                if (SelectedDept == null)
                {
                    MessageBox.Show("Please select a department from the drop down list");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        TLADM_DepartmentsArea tranType = new TLADM_DepartmentsArea();
                        AddRec = true;

                        if (row.Cells[0].Value != null)
                        {
                            tranType = context.TLADM_DepartmentsArea.Find((int)row.Cells[0].Value);
                            if (tranType != null)
                            {
                                AddRec = false;
                            }
                        }

                        tranType.DeptA_Description = (string)row.Cells[1].Value;
                        tranType.DeptA_Dep_Fk = SelectedDept.Dep_Id;

                        if (AddRec)
                            context.TLADM_DepartmentsArea.Add(tranType);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
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
