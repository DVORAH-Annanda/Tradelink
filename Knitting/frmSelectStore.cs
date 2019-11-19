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

namespace Knitting
{
    public partial class frmSelectStore : Form
    {
        public int WhseId;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
        bool Submit;

        public frmSelectStore()
        {
            InitializeComponent();
            
            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = true;
            oChkA.ValueType = typeof(Boolean);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.Visible = true;
            oTxtB.HeaderText = "Whse Code";
            oTxtB.ReadOnly = true;
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = 200;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.Visible = true;
            oTxtC.HeaderText = "Whse Description";
            oTxtC.ReadOnly = true;
            oTxtC.ValueType = typeof(string);
            oTxtC.Width = 250;
            dataGridView1.Columns.Add(oTxtC);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void frmSelectStore_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x=>x.Dep_ShortCode == "KNIT").FirstOrDefault();
                if(Depts != null)
                {
                    var Entries = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Depts.Dep_Id).ToList();
                    foreach (var Entry in Entries)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Entry.WhStore_Id;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Entry.WhStore_Code;
                        dataGridView1.Rows[index].Cells[3].Value = Entry.WhStore_Description;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).Count();
                if (SingleRow == 0)
                {
                    MessageBox.Show("Please select a store");
                    return;
                }

                foreach (DataGridViewRow Row in dataGridView1.Rows)
                {
                    if ((bool)Row.Cells[1].Value == true)
                    {
                        WhseId = (int)Row.Cells[0].Value;
                        break;
                    }
                }

                this.Close(); 
            }
        }
    }
}
