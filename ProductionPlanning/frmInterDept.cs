using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using Utilities;

namespace ProductionPlanning
{
    public partial class frmInterDept : Form
    {
        DataTable dt;
        DataColumn column;

        DataGridViewComboBoxColumn oCmboA;
        DataGridViewComboBoxColumn oCmboB;
        DataGridViewComboBoxColumn oCmboC;

        DataGridViewTextBoxColumn selecta;
        DataGridViewTextBoxColumn selectb;

        BindingSource BindingSrc;

        bool FormLoaded;
        public frmInterDept()
        {
            InitializeComponent();
            dt = new DataTable();

            BindingSrc = new BindingSource();

            //------------------------------------------------------
            // Create column 0. // This is the primary key
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.Caption = "Primary Key";
            column.ColumnName = "Col0";
            column.DefaultValue = 0;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 1. // This is the description
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.Caption = "Generic Name";
            column.ColumnName = "Col1";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. // This is the Knitting Department Key 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.Caption = "Knitting selection";
            column.ColumnName = "Col2";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 3. // This is the Dying Department Key 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.Caption = "Dyeing Selection";
            column.ColumnName = "Col3";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 4. // This is the CMT Department Key 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Col4";
            column.Caption = "CMT Selection";
            dt.Columns.Add(column);

            //0 -- Primary Key of DataTable 
            //----------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrimKey";
            selecta.ValueType = typeof(int);
            selecta.DataPropertyName = dt.Columns[0].ColumnName;
            selecta.HeaderText = "Primary Key";
            selecta.Visible = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns["PrimKey"].DisplayIndex = 0;

            //1 -- Generic Nme given to measurement
            //----------------------------------------------
            selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "Descrip";
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = dt.Columns[1].ColumnName;
            selectb.HeaderText = "Description";
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns["Descrip"].DisplayIndex = 1;

            using (var context = new TTI2Entities())
            {
                oCmboA = new DataGridViewComboBoxColumn();  // Foreign Key to Knitting measurement 
                oCmboA.Name = "SelKnitting";
                oCmboA.HeaderText = "Select Knitting Measurement";  // 2
                oCmboA.Width = 100; 
                oCmboA.DataSource = context.TLADM_QualityDefinition.Where(x => x.QD_OriginatingDept_FK == 11).OrderBy(x => x.QD_ColumnIndex).ToList();
                oCmboA.ValueMember = "QD_Pk";
                oCmboA.DisplayMember = "QD_Description";
                oCmboA.DataPropertyName = dt.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oCmboA);
                dataGridView1.Columns["SelKnitting"].DisplayIndex = 2;

                oCmboB = new DataGridViewComboBoxColumn();  // Foreign Key to Dying Measurement 
                oCmboB.HeaderText = "Select Dying Measurement";  // 3
                oCmboB.Name = "SelDying";
                oCmboB.DataPropertyName = dt.Columns[3].ColumnName;
                oCmboB.Width = 100;
                oCmboB.DataSource = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK == 4).ToList();
                oCmboB.ValueMember = "TLQADPF_Pk";
                oCmboB.DisplayMember = "TLQADPF_Description";
                dataGridView1.Columns.Add(oCmboB);
                dataGridView1.Columns["SelDying"].DisplayIndex = 3;

                oCmboC = new DataGridViewComboBoxColumn();  // Foreign Key to Dying Measurement 
                oCmboC.HeaderText = "Select CMT Measurement";  // 4
                oCmboC.Name = "SelCMT";
                oCmboC.DataPropertyName = dt.Columns[4].ColumnName;
                oCmboC.Width = 100;
                oCmboC.DataSource = context.TLCMT_DeflectFlaw.OrderBy(x => x.TLCMTDF_ShortCode).ToList();
                oCmboC.ValueMember = "TLCMTDF_Pk";
                oCmboC.DisplayMember = "TLCMTDF_Description";
                dataGridView1.Columns.Add(oCmboC);
                dataGridView1.Columns["SelCMT"].DisplayIndex = 4;
            }

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;

            }
        }

        private void frmInterDept_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
           
            using (var context = new TTI2Entities())
            {
             
                var Entities = context.TLPPS_InterDept.OrderBy(x => x.TLInter_Description).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow row = dt.NewRow();
                    row[0] = Entity.TLInter_Pk;
                    row[1] = Entity.TLInter_Description;
                    row[2] = Entity.TLInter_Knitting_Fk;
                    row[3] = Entity.TLInter_Dying_Fk;
                    row[4] = Entity.TLInter_CMT_Fk;

                    dt.Rows.Add(row);
                }
             }
            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oButton = (Button)sender;
            TLPPS_InterDept InterD;
            bool NewRecord;

            if (oButton != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        NewRecord = true;
                        if (row.Field<int>(2) != 0 && row.Field<int>(3) != 0 && row.Field<int>(4) != 0)
                        {
                            if (row.Field<int>(0) == 0)
                            {
                                InterD = new TLPPS_InterDept();
                            }
                            else
                            {
                                InterD = context.TLPPS_InterDept.Find((int)row[0]);
                                if(InterD == null)
                                {
                                    continue;
                                }
                                NewRecord = false;
                            }

                            InterD.TLInter_Description = row.Field<String>(1);
                            InterD.TLInter_Knitting_Fk = row.Field<int>(2);
                            InterD.TLInter_Dying_Fk = row.Field<int>(3);
                            InterD.TLInter_CMT_Fk = row.Field<int>(4);
                            
                            if (NewRecord)
                            {
                                context.TLPPS_InterDept.Add(InterD);
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
