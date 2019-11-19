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
    public partial class frmShiftDefinition : Form
    {
        DataGridViewTextBoxColumn oTxtA;   // Primary Palet Index no
        DataGridViewTextBoxColumn oTxtB;   // Pallet Number
        DataGridViewTextBoxColumn oTxtC;   // Pallet Nett Weight 
        DataGridViewTextBoxColumn oTxtD;   // Pallet Cones Available
        DataGridViewTextBoxColumn oTxtE;   // Pallet Cones Reserved
        bool formloaded;
        Util core;
        public frmShiftDefinition()
        {
            InitializeComponent();
            core = new Util();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmbDepartments.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                cmbDepartments.DisplayMember = "Dep_Description";
                cmbDepartments.ValueMember = "Dep_Id";
            }

            //Initialise the dataGrid
            //------------------------------------

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Shift Description
            oTxtB.HeaderText = "Shift Description";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;
         
            oTxtC = new DataGridViewTextBoxColumn();  // 2 Time Start 
            oTxtC.HeaderText = "Time Start";
            oTxtC.ValueType = typeof(TimeSpan);
            oTxtC.Visible = true;
            
            oTxtD = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtD.HeaderText = "Time End";
            oTxtD.ValueType = typeof(TimeSpan);
            oTxtD.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // Pallet No Key 
            dataGridView1.Columns.Add(oTxtB);    // Pallet No
            dataGridView1.Columns.Add(oTxtC);    // Nett Weight  
            dataGridView1.Columns.Add(oTxtD);    // Cones Available
            
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            formloaded = true;
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
                    
                    if (Cell.ColumnIndex == 2 ||
                        Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownTS);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownTS);
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
        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
        
            if (oCmbo != null && formloaded)
            {
                var Department = (TLADM_Departments)cmbDepartments.SelectedItem;
                if (Department != null)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        var shifts = context.TLADM_Shifts.Where(x => x.Shft_Dept_FK == Department.Dep_Id).ToList();
                        foreach(var shift in shifts)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = shift.Shft_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = shift.Shft_Description;
                            dataGridView1.Rows[index].Cells[2].Value = shift.Shft_Start;
                            dataGridView1.Rows[index].Cells[3].Value = shift.Shft_End;

                        }

                        dataGridView1.Columns[2].Width = 150;
                        int width = 0;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (!col.Visible)
                                continue;
                            width += col.Width;

                        }

                        dataGridView1.Width = width + 40;

                        dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                        dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             Button oBtn = sender as Button;
             bool success = true; 
             bool lAdd;
             if (oBtn != null && formloaded)
             {
                  var Department = (TLADM_Departments)cmbDepartments.SelectedItem;
                  if (Department != null)
                  {
                      using ( var context = new TTI2Entities())
                      {

                          foreach(DataGridViewRow dr in dataGridView1.Rows)
                          {
                              TLADM_Shifts shifts = new TLADM_Shifts();
                          
                              if(dr.Cells[1].Value == null)
                                  continue;
                          
                              lAdd = true;

                              if(dr.Cells[0].Value != null)
                              {
                                  if((int)dr.Cells[0].Value != 0)
                                  {
                                      lAdd = false;
                                      shifts = context.TLADM_Shifts.Find((int)dr.Cells[0].Value);
                                  }
                              }

                              shifts.Shft_Dept_FK = Department.Dep_Id;
                              shifts.Shft_Description = dr.Cells[1].Value.ToString();
                              shifts.Shft_Start = (TimeSpan)dr.Cells[2].Value;
                              shifts.Shft_End = (TimeSpan)dr.Cells[3].Value; 

                              if(lAdd)
                                  context.TLADM_Shifts.Add(shifts);

                              try
                              {
                                  context.SaveChanges();
                              }
                              catch (Exception ex)
                              {
                                  var exceptionMessages = new StringBuilder();
                                  do
                                  {
                                      exceptionMessages.Append(ex.Message);
                                      ex = ex.InnerException;
                                  }
                                  while (ex != null);

                                  MessageBox.Show(exceptionMessages.ToString());
                                  success = false;
                              }
                            

                           }
                      }
                      if (success)
                      {
                          dataGridView1.Rows.Clear();
                          MessageBox.Show("data save to database");
                      }
                  }
             }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    using (var context = new TTI2Entities())
                    {
                        if (res == DialogResult.OK)
                        {
                            var Pk = (int)oDgv.CurrentRow.Cells[0].Value;
                            TLADM_Shifts shifts = context.TLADM_Shifts.Find(Pk);
                            if(shifts != null)
                            {
                                context.TLADM_Shifts.Remove(shifts);

                                try
                                {
                                    context.SaveChanges();
                                    oDgv.Rows.Clear();
                                    MessageBox.Show("Data successfully saved to database");

                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }
    }
}
