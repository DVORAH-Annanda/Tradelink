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
    public partial class frmDBDatesReq : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check Box to select             1
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // DB Batch Number                2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();  //  Current Required Date                 3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();  //  Kgs                

        bool FormLoaded;

        public frmDBDatesReq()
        {
            InitializeComponent();

            //Initialise the dataGrid
            //------------------------------------

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();  // 1 Check Box
            oChkA.ValueType = typeof(Boolean);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();   // 2 DBBatch No 
            oTxtB.HeaderText = "Batch No";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();   // 3 Date Currently required 
            oTxtC.HeaderText = "Required Date";
            oTxtC.ValueType = typeof(string);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);


            oTxtD = new DataGridViewTextBoxColumn();   // 4 Date 
            oTxtD.HeaderText = "Kgs";
            oTxtD.ValueType = typeof(int);
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

        }

        private void frmDBDatesReq_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                var Entries = context.TLDYE_DyeBatch.Where(x =>!x.DYEB_Closed && !x.DYEB_OutProcess);
                foreach (var Entry in Entries)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Entry.DYEB_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = Entry.DYEB_BatchNo;
                    dataGridView1.Rows[index].Cells[3].Value = Entry.DYEB_RequiredDate.Value.ToString("dd/MM/yyyy");
                    dataGridView1.Rows[index].Cells[4].Value = Math.Round(Entry.DYEB_BatchKG,2).ToString();
                }
            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if ((bool)Row.Cells[1].Value)
                            continue;

                        var Pk = (int)Row.Cells[0].Value;
                        var DyeBatch = context.TLDYE_DyeBatch.Find(Pk);
                        if (DyeBatch != null)
                            DyeBatch.DYEB_RequiredDate = dtpDateRequired.Value;
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to databases");

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
