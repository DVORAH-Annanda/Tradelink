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

namespace CMT
{
    public partial class frmCancelIndividual : Form
    {
        protected readonly TTI2Entities _Context;
        DataTable dt;
        BindingSource BindSrc;
        DataColumn column;
      
        int _PrimaryKey;
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;
        DataGridViewCheckBoxColumn oChkA;

        public frmCancelIndividual(int Pk, string Mess)
        {
            InitializeComponent();

            _PrimaryKey = Pk;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            _Context = new TTI2Entities();
            BindSrc = new BindingSource();
            dt = new DataTable();

            this.Text += Mess;

            //------------------------------------------------------
            // Create column 0. // This is the primary key index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the whether the record has been selected 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Boolean);
            column.DefaultValue = false;
            column.ColumnName = "Col1";
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 2. // This is the number of the truck loading  string format 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Col2";
            dt.Columns.Add(column);

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Name = "TruckLoadIndex";
            oTxtA.ValueType = typeof(Int32);
            oTxtA.DataPropertyName = dt.Columns[0].ColumnName;
            oTxtA.HeaderText = "TruckLoad Index";
            oTxtA.Visible = false;
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns[0].DisplayIndex = 0;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Name = "Select";
            oChkA.HeaderText = "Select";
            oChkA.DataPropertyName = dt.Columns[1].ColumnName;
            oChkA.ValueType = typeof(Boolean);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.Name = "CutSheet";
            oTxtB.ValueType = typeof(String);
            oTxtB.DataPropertyName = dt.Columns[2].ColumnName;
            oTxtB.HeaderText = "CutSheet ";
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns[2].DisplayIndex = 2;

            BindSrc.DataSource = dt;
            dataGridView1.DataSource = BindSrc;
        }

        private void frmCancelIndividual_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            var TruckLoadingDetails = _Context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == _PrimaryKey).ToList();
            foreach(var Detail in TruckLoadingDetails)
            {
                DataRow Row = dt.NewRow();
                Row[0] = Detail.CMTPID_Pk;
                Row[1] = false;
                var Csr = _Context.TLCUT_CutSheetReceipt.Find(Detail.CMTPID_CutSheet_FK);
                if(Csr != null)
                {
                    var CS = _Context.TLCUT_CutSheet.Where(x => x.TLCutSH_Pk == Csr.TLCUTSHR_CutSheet_FK).FirstOrDefault();
                    if(CS != null)
                    {
                        Row[2] = CS.TLCutSH_No;
                    }
                }

                dt.Rows.Add(Row);
            }
            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow Row in dt.Rows)
            {
                if (!Row.Field<bool>(1))
                {
                    continue;
                }

                var RecNo = Row.Field<int>(0);

                var TruckLoadingDet = _Context.TLCMT_PanelIssueDetail.Find(RecNo);
                if (TruckLoadingDet != null)
                {
                    var CutSheetRec = _Context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == TruckLoadingDet.CMTPID_CutSheet_FK).FirstOrDefault();
                    if (CutSheetRec != null)
                    {
                        CutSheetRec.TLCUTSHR_Issued = false;
                    }

                    _Context.TLCMT_PanelIssueDetail.Remove(TruckLoadingDet);
                }
            }

            try
            {
                _Context.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    

        private void frmCancelIndividual_FormClosing(object sender, FormClosingEventArgs e)
        {          
            if(!e.Cancel)
            {
                if(_Context != null)
                {
                    _Context.Dispose();
                }
            }
        }
    }
}
