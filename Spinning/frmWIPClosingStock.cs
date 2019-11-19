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

namespace Spinning
{
    public partial class frmWIPClosingStock : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;

        Util core;
        bool formloaded;

        bool Add;
        public frmWIPClosingStock()
        {
            InitializeComponent();
            core = new Util();
            SetUp(true);
           
            
        }

        void SetUp(bool Flag)
        {
            formloaded = false;
            Add = true;
            if (Flag)
            {
                dataGridView1.AutoGenerateColumns = false;

                oTxtBoxA = new DataGridViewTextBoxColumn();
                oTxtBoxA.ValueType = typeof(int);
                oTxtBoxA.Visible = false;

                oTxtBoxB = new DataGridViewTextBoxColumn();
                oTxtBoxB.ValueType = typeof(int);
                oTxtBoxB.Visible = false;

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.HeaderText = "Area Description";
                oTxtBoxC.ValueType = typeof(string);
                oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                oTxtBoxC.ReadOnly = true;
                oTxtBoxC.Width = 175;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.HeaderText = "Month End Balance";
                oTxtBoxD.ValueType = typeof(decimal);
                oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                dataGridView1.Columns.Add(oTxtBoxA);    // 0 pk
                dataGridView1.Columns.Add(oTxtBoxB);    // 1 Department Area 
                dataGridView1.Columns.Add(oTxtBoxC);    // 2 Measurement
                dataGridView1.Columns.Add(oTxtBoxD);    // 3 Value
                dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
            }

            using (var context = new TTI2Entities())
            {
                var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
              
                var Existing = context.TLADM_DepartmentsArea.Where(x=>x.DeptA_Dept_FK == dept.Dep_Id).ToList();
                foreach (var row in Existing)
                {
                      var index = dataGridView1.Rows.Add();
                      dataGridView1.Rows[index].Cells[0].Value = 0;
                      dataGridView1.Rows[index].Cells[1].Value = row.DeptA_Pk;
                      dataGridView1.Rows[index].Cells[2].Value = row.DeptA_Description;
                      dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                 }
            }

            formloaded = true;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused)
            {
                var Cell = oDgv.CurrentCell;
                if (Cell.ColumnIndex == 2)
                {
                    e.Control.KeyDown -= core.txtWin_KeyDownOEM;
                    e.Control.KeyPress -= core.txtWin_KeyPress;
                    e.Control.KeyDown += core.txtWin_KeyDownOEM;
                    e.Control.KeyPress += core.txtWin_KeyPress;
                }
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
           
            Button oBtn = sender as Button;
            bool success = true;
          
            
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                   
                    DateTime dt = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    var Existing = context.TLADM_DepartmentsAreaTransaction.Where(x => x.TLDEPA_Date == dt).ToList();
                    
                    var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        bool Add = true;
                        TLADM_DepartmentsAreaTransaction tranType = new TLADM_DepartmentsAreaTransaction();
                        var index = (int)row.Cells[0].Value;
                        if(index != 0)
                        {
                           
                            tranType = context.TLADM_DepartmentsAreaTransaction.Find(index);
                            if (tranType == null)
                            {
                                tranType = new TLADM_DepartmentsAreaTransaction();
                            }
                            else
                            {
                                Add = false;
                            }
                        }

                        tranType.TLDEPA_Date = dateTimePicker1.Value;
                        tranType.TRDEPA_Department_FK = dept.Dep_Id;
                        tranType.TRDEPA_DeptA_FK = (int)row.Cells[1].Value;
                        tranType.TRDEPA_Value = (decimal)row.Cells[3].Value;

                        if(Add)
                            context.TLADM_DepartmentsAreaTransaction.Add(tranType);                       
                        
                    }
                    try
                    {
                       context.SaveChanges();
                       formloaded = false;
                       dateTimePicker1.Value = DateTime.Now;
                       formloaded = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        success = false;
                    }
                }
                if (success)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Records Stored to database");
                    SetUp(false);
                }
            
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
    
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    DateTime dt =  Convert.ToDateTime(oDtp.Value.ToShortDateString());
                    var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "SPIN").FirstOrDefault();
                    
                    var Existing = context.TLADM_DepartmentsAreaTransaction.Where(x => x.TLDEPA_Date == dt && x.TRDEPA_Department_FK == dept.Dep_Id).ToList();
                    if (Existing.Count > 0)
                    {
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLDEPA_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = row.TRDEPA_DeptA_FK;
                            dataGridView1.Rows[index].Cells[2].Value = context.TLADM_DepartmentsArea.Find(row.TRDEPA_DeptA_FK).DeptA_Description;
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.TRDEPA_Value, 1);
                        }
                    }
                    else
                    {
                        if (dept != null)
                        {
                            var NewExist = context.TLADM_DepartmentsArea.GroupBy(x=>x.DeptA_Description).Select(x=>x.FirstOrDefault());

                            foreach(var row in NewExist)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = 0;
                                dataGridView1.Rows[index].Cells[1].Value = row.DeptA_Pk;
                                dataGridView1.Rows[index].Cells[2].Value = row.DeptA_Description;
                                dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                            }
                        }
                    }
                   
                }
  
            }
        }
     
    }
}
