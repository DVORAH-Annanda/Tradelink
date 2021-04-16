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
    public partial class frmTLADM_MachineMaintenance : Form
    {
        TLADM_Departments _Depts;
        TLADM_MachineDefinitions _Machs;
        protected readonly TTI2Entities _context;

        bool FormLoaded;
        DataTable dataTable;
        DataColumn column;
        BindingSource BindingSrc;

        TLADM_MachineMaintenance TaskDetail = null;


        /*[mman_Pk] [int] IDENTITY(1,1) NOT NULL,
          [mman_MaintenanceTask_FK] [int] NOT NULL,
	      [mman_Machine_Fk] [int] NOT NULL,
	      [mman_Interval_Between_Tasks] [int] NULL,
          [mman_Interval_UOM] [int] NULL,
	      [mman_Reset] [bit] NOT NULL,
	      [mman_Date_Last_Reset] [date] NULL,
	      [mman_Volume_CurrentValue] [decimal](18, 4) NOT NULL,
	      [mman_Volume_Between_Tasks] [int] NULL,
          [mman_Volumne_UOM] [int] NULL,
	      [mman_Last_Identifier] [varchar](50) NULL, */

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();  // 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();  // 1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();  // 2 
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();  // 3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();  // 4
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // 5 
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // 6
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();  // 7
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();  // 8
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();  // 9
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn();  // 10
        DataGridViewTextBoxColumn oTxtM = new DataGridViewTextBoxColumn();  // 11
     
        public frmTLADM_MachineMaintenance(TLADM_Departments Depts, TLADM_MachineDefinitions Mcs)
        {
            InitializeComponent();
            
            _Depts = Depts;
            _Machs = Mcs;

            var _Width = 100;

            label2.Text = _Machs.MD_Description;

            _context = new TTI2Entities();

          

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;

            column     = new DataColumn();
            dataTable  = new DataTable();
            BindingSrc = new BindingSource();

            //==========================================================================================
            // 1st task is to create the data table dataTable2 
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Pk";
            column.Caption = "Record Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[0] };

            //1
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_MaintenanceTask_FK";
            column.Caption = "Record Task Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            //2
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Machine_Fk";
            column.Caption = "Machine Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            //3
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Interval_Between_Tasks";
            column.Caption = "Size Primary Key";
            column.DefaultValue = 0.00M;
            dataTable.Columns.Add(column);

            //4
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Interval_CurrentValue";
            column.Caption = "Interval Current Value";
            column.DefaultValue = 0.00M;
            dataTable.Columns.Add(column);


            //5
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Interval_UOM";
            column.Caption = "Size Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            //6
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "mman_Reset";
            column.Caption = "Reset";
            column.DefaultValue = false;
            dataTable.Columns.Add(column);

            //7
            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "mman_Date_Last_Reset";
            column.Caption = "Date Last Reset";
            column.DefaultValue = DateTime.Now;
            dataTable.Columns.Add(column);

            //8
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Volume_Between_Tasks";
            column.Caption = "Volume Between Tasks";
            column.DefaultValue = 0.00M;
            dataTable.Columns.Add(column);

            //9
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Volume_Current_Value";
            column.Caption = "Volume Current Value";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);
            
            //10
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "mman_Volume_UOM";
            column.Caption = "Unit Of Measure";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            //11
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "mman_Last_Identifier";
            column.Caption = "Last Identifier";
            column.DefaultValue = String.Empty;
            dataTable.Columns.Add(column);

            //[mman_Pk] [int] IDENTITY(1, 1) NOT NULL,
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.DataPropertyName = dataTable.Columns[0].ColumnName;
            dataGridView1.Columns.Add(oTxtA);

            //[mman_MaintenanceTask_FK] [int] NOT NULL,
            oTxtB.Visible = false;
            oTxtB.ValueType = typeof(int);
            oTxtB.DataPropertyName = dataTable.Columns[1].ColumnName;
            oTxtB.Width = _Width;
            dataGridView1.Columns.Add(oTxtB);

            //[mman_Machine_Fk] [int] NOT NULL,
            oTxtC.Visible = false;
            oTxtC.ValueType = typeof(int);
            oTxtC.DataPropertyName = dataTable.Columns[2].ColumnName;
            oTxtC.Width = _Width;
            dataGridView1.Columns.Add(oTxtC);

            //[mman_Interval_Between_Tasks] [int] NULL,
            oTxtD.HeaderText = "Interval between tasks";
            oTxtD.ValueType = typeof(int);
            oTxtD.DataPropertyName = dataTable.Columns[3].ColumnName;
            oTxtD.Width = _Width;
            dataGridView1.Columns.Add(oTxtD);

            //[mman_Interval_Between_Tasks][int] NULL,
            oTxtE.HeaderText = "Interval - Current Value";
            oTxtE.ValueType = typeof(int);
            oTxtE.DataPropertyName = dataTable.Columns[4].ColumnName;
            oTxtE.Width = _Width;
            dataGridView1.Columns.Add(oTxtE);

            //[mman_Interval_UOM] [int] NULL,
            oCmboA.HeaderText = "UOM";
            oCmboA.ValueType = typeof(int);
            oCmboA.DataPropertyName = dataTable.Columns[5].ColumnName;
            oCmboA.Width = _Width;
            oCmboA.DataSource = _context.TLADM_UOM.ToList();
            oCmboA.ValueMember = "UOM_Pk";
            oCmboA.DisplayMember = "UOM_Description";
            dataGridView1.Columns.Add(oCmboA);

            //[mman_Reset] [bit] NOT NULL,
            oChkA.HeaderText = "Reset";
            oChkA.ValueType = typeof(bool);
            oChkA.DataPropertyName = dataTable.Columns[6].ColumnName;
            oChkA.Width = _Width;
            dataGridView1.Columns.Add(oChkA);

            // [mman_Date_Last_Reset] [date] NULL,
            oTxtG.Visible = false;
            oTxtG.HeaderText = "Date Last Reset";
            oTxtG.ValueType = typeof(DateTime);
            oTxtG.DataPropertyName = dataTable.Columns[7].ColumnName;
            oTxtG.Width = _Width;
            dataGridView1.Columns.Add(oTxtG);

            //[mman_Volume_CurrentValue] [decimal](18, 4) NOT NULL,
            oTxtH.HeaderText = "Volume (Interval) Between Tasks";
            oTxtH.ValueType = typeof(int);
            oTxtH.DataPropertyName = dataTable.Columns[8].ColumnName;
            oTxtH.Width = _Width;
            dataGridView1.Columns.Add(oTxtH);

            // [mman_Volume_Between_Tasks] [int] NULL,
            oTxtJ.HeaderText = "Volume Current Value";
            oTxtJ.ValueType = typeof(int);
            oTxtJ.DataPropertyName = dataTable.Columns[9].ColumnName;
            oTxtJ.Width = _Width;
            dataGridView1.Columns.Add(oTxtJ);

            // [mman_Volumne_UOM] [int] NULL,
            oCmboB.HeaderText = "UOM";
            oCmboB.ValueType = typeof(int);
            oCmboB.DataPropertyName = dataTable.Columns[10].ColumnName;
            oCmboB.Width = _Width;
            oCmboB.DataSource = _context.TLADM_UOM.ToList();
            oCmboB.ValueMember = "UOM_Pk";
            oCmboB.DisplayMember = "UOM_Description";
            dataGridView1.Columns.Add(oCmboB);

            oTxtM.HeaderText = "Last Intentity";
            oTxtM.ValueType = typeof(String);
            oTxtM.DataPropertyName = dataTable.Columns[11].ColumnName;
            oTxtM.Width = _Width;
            dataGridView1.Columns.Add(oTxtM);

            BindingSrc.DataSource = dataTable;
            dataGridView1.DataSource = BindingSrc;

        }

        private void frmTLADM_MachineMaintenance_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            cmboTasks.DataSource = _context.TLADM_MachineMaintenanceTasks.Where(x=>x.TLMtask_Dept_FK == _Depts.Dep_Id).ToList();
            cmboTasks.ValueMember = "TLMtask_Pk";
            cmboTasks.DisplayMember = "TLMtask_Description";
            cmboTasks.SelectedIndex = -1;
            
            if(cmboTasks.Items.Count == 0)
            {
                MessageBox.Show("There are no tasks defined for this Department");
                this.Close();
            }
            FormLoaded = true;

        }

        private void frmTLADM_MachineMaintenance_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel && FormLoaded)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void cmboTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                dataTable.Rows.Clear();

                var SelectedTask = (TLADM_MachineMaintenanceTasks)cmboTasks.SelectedItem;
                if (SelectedTask != null)
                {
                    TaskDetail = _context.TLADM_MachineMaintenance.Where(x => x.mman_MaintenanceTask_FK == SelectedTask.TLMtask_Pk && x.mman_Machine_Fk == _Machs.MD_Pk && !x.mman_Reset).FirstOrDefault();
                    if(TaskDetail == null)
                    {
                        TaskDetail = new TLADM_MachineMaintenance();
                        TaskDetail.mman_MaintenanceTask_FK = SelectedTask.TLMtask_Pk;
                        TaskDetail.mman_Machine_Fk = _Machs.MD_Pk;
                        TaskDetail.mman_Last_Identifier = String.Empty;
                        TaskDetail.mman_Reset = false;
                        TaskDetail.mman_Volume_Between_Tasks = 0;
                        TaskDetail.mman_Volume_CurrentValue = 0;
                        TaskDetail.mman_Volumne_UOM = _context.TLADM_UOM.FirstOrDefault().UOM_Pk ;
                        TaskDetail.mman_Interval_Between_Tasks = 0;
                        TaskDetail.mman_Interval_CurrentValue = 0;
                        TaskDetail.mman_Interval_UOM = _context.TLADM_UOM.FirstOrDefault().UOM_Pk;

                    }

                    DataRow NewRow = dataTable.NewRow();
                    
                    NewRow[0] = TaskDetail.mman_Pk;
                    NewRow[1] = TaskDetail.mman_MaintenanceTask_FK;
                    NewRow[2] = TaskDetail.mman_Machine_Fk;
                    NewRow[3] = TaskDetail.mman_Interval_Between_Tasks;
                    NewRow[4] = TaskDetail.mman_Interval_CurrentValue;
                    NewRow[5] = TaskDetail.mman_Interval_UOM;
                    NewRow[6] = TaskDetail.mman_Reset;
                    NewRow[7] = DateTime.Now;
                    NewRow[8] = TaskDetail.mman_Volume_Between_Tasks;
                    NewRow[9] = TaskDetail.mman_Volume_CurrentValue;
                    NewRow[10] = TaskDetail.mman_Volumne_UOM;
                    NewRow[11] = TaskDetail.mman_Last_Identifier;

                    try
                    {
                        dataTable.Rows.Add(NewRow);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lAdd;
            if(oBtn != null && FormLoaded)
            {
                foreach(DataRow NewRow in dataTable.Rows)
                {
                    lAdd = true;
                    if(NewRow.Field<int>(0)!= 0)
                    {
                       lAdd = false;
                    }
                     
                    TaskDetail.mman_MaintenanceTask_FK = NewRow.Field<int>(1);
                    TaskDetail.mman_Machine_Fk = NewRow.Field<int>(2);
                    TaskDetail.mman_Interval_Between_Tasks = NewRow.Field<int>(3);
                    TaskDetail.mman_Interval_CurrentValue = NewRow.Field<int>(4);
                    TaskDetail.mman_Interval_UOM = NewRow.Field<int>(5);
                    TaskDetail.mman_Reset  = NewRow.Field<bool>(6);
                    TaskDetail.mman_Date_Last_Reset = NewRow.Field<DateTime>(7);
                    TaskDetail.mman_Volume_Between_Tasks = NewRow.Field<int>(8);
                    TaskDetail.mman_Volume_CurrentValue  = NewRow.Field<int>(9);
                    TaskDetail.mman_Volumne_UOM = NewRow.Field<int>(10);
                    TaskDetail.mman_Last_Identifier = NewRow.Field<String>(11);

                    if (lAdd)
                    {
                        _context.TLADM_MachineMaintenance.Add(TaskDetail);
                    }

                    if(NewRow.Field<bool>(6))
                    {
                        TLADM_MachineMaintenance MachMaint = new TLADM_MachineMaintenance();
                        
                        MachMaint.mman_MaintenanceTask_FK = NewRow.Field<int>(1);
                        MachMaint.mman_Machine_Fk = NewRow.Field<int>(2);
                        MachMaint.mman_Interval_Between_Tasks = NewRow.Field<int>(3);
                        MachMaint.mman_Interval_UOM = NewRow.Field<int>(5);
                        MachMaint.mman_Reset = false;
                        MachMaint.mman_Date_Last_Reset = null;
                        MachMaint.mman_Interval_CurrentValue = 0;
                        MachMaint.mman_Volume_CurrentValue = 0;
                        MachMaint.mman_Volume_Between_Tasks = NewRow.Field<int>(9);
                        MachMaint.mman_Volumne_UOM = NewRow.Field<int>(10);
                        MachMaint.mman_Last_Identifier = string.Empty;

                        _context.TLADM_MachineMaintenance.Add(MachMaint);
                    }
                }
                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data successfully to the database");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
