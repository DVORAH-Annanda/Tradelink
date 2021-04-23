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
    public partial class frmHydroResults : Form
    {
        bool FormLoaded;

        protected readonly TTI2Entities _context;

        DataTable dataTable;
        DataColumn column;
        BindingSource BindingSrc;
        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();     // Primary Key (CutSheetDetail) 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();     //  Bundle No                   1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();     // Qty                          3
        DataGridViewCheckBoxColumn oTxtD = new DataGridViewCheckBoxColumn();
        public frmHydroResults()
        {
            InitializeComponent();
            _context = new TTI2Entities();

            core = new Util();
            dataTable = new DataTable();
            column = new DataColumn();
            BindingSrc = new BindingSource();

            BindingSrc.DataSource = dataTable;
            dataGridView1.DataSource = BindingSrc;

            //==========================================================================================
            // 1st task is to create the data table dataTable 
            // Col 0
            //=====================================================================
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Primary_Pk";
            column.Caption = "Primary Key";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Measurement_Description";
            column.Caption = "Measurement Description";
            column.DefaultValue = String.Empty;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Measuremnt_Standard";
            column.Caption = "Measurement Standard";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Measurement_Result";
            column.Caption = "Measurement";
            column.DefaultValue = 0;
            dataTable.Columns.Add(column);
}

private void frmHydroResults_Load(object sender, EventArgs e)
{

}
}
}
