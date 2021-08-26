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
    public partial class frmCancelTLInstruction : Form
    {
        protected readonly TTI2Entities _context;
        DataTable dt;
        BindingSource BindSrc;
        DataColumn column;
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;
        DataGridViewCheckBoxColumn oChkA;
        public frmCancelTLInstruction()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            _context = new TTI2Entities();
            BindSrc = new BindingSource();
            dt = new DataTable();

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

            //------------------------------------------------------
            // Create column 3. // This is the whether the record has been selected 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Col3";
            dt.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is the whether the record has been selected 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Col4";
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
            oTxtB.Name = "To_Department";
            oTxtB.ValueType = typeof(String);
            oTxtB.DataPropertyName = dt.Columns[2].ColumnName;
            oTxtB.HeaderText = "To CMT";
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.Name = "From_Department";
            oTxtC.ValueType = typeof(String);
            oTxtC.DataPropertyName = dt.Columns[3].ColumnName;
            oTxtC.HeaderText = "From Cutting";
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns[3].DisplayIndex = 3;

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.Name = "TruckLoading_No";
            oTxtD.ValueType = typeof(Int32);
            oTxtD.DataPropertyName = dt.Columns[4].ColumnName;
            oTxtD.HeaderText = "Truck Loading No";
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns[4].DisplayIndex = 4;

            BindSrc.DataSource = dt;
            dataGridView1.DataSource = BindSrc;

        }

        private void frmCancelTLInstruction_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            var TruckLoadings = _context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Closed && !x.CMTPI_Cancelled && !x.CMTPI_Receipted).ToList();

            foreach (var TruckLoading in TruckLoadings)
            {
                DataRow NewR = dt.NewRow();
                NewR[0] = TruckLoading.CMTPI_Pk;
                NewR[1] = false;
                NewR[2] = _context.TLADM_Departments.Find(TruckLoading.CMTPI_Department_FK).Dep_Description;
                NewR[3] = _context.TLADM_WhseStore.Find(TruckLoading.CMTPI_FromWhse_FK).WhStore_Description;
                NewR[4] = TruckLoading.CMTPI_Number;
                dt.Rows.Add(NewR);
            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                foreach (DataRow Item in dt.Rows)
                {
                    if (!Item.Field<bool>(1))
                    {
                        continue;
                    }

                    var Mess = " Truck Load No " + Item.Field<int>(4).ToString();

                    DialogResult Result = MessageBox.Show("Would you like to cancel the complete loading instruction", Mess, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    var PanKey = Item.Field<int>(0);

                    if (Result == DialogResult.Yes)
                    {
                        CancelComplete(PanKey);

                        var PanIssue = _context.TLCMT_PanelIssue.Find(PanKey);

                        if (PanIssue != null)
                        {
                            PanIssue.CMTPI_Cancelled = true;
                            PanIssue.CMTPI_Closed = true;
                            PanIssue.CMTPI_Date = dtpTransDate.Value;
                        }
                    }
                    else
                    {
                        CMT.frmCancelIndividual CancelInv = new frmCancelIndividual(PanKey, Mess);
                        CancelInv.ShowDialog(this);
                        return;

                    }
                }

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Data successfully saved to database");
                    dt.Rows.Clear();
                    frmCancelTLInstruction_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

            }

        }

        private void CancelComplete(int KeyNo)
        {
            var PanIssueDetails = _context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == KeyNo).ToList();
            foreach (var PanIssue in PanIssueDetails)
            {
                var CutSheetReceipt = _context.TLCUT_CutSheetReceipt.Find(PanIssue.CMTPID_CutSheet_FK);
                if (CutSheetReceipt != null)
                {
                    CutSheetReceipt.TLCUTSHR_Issued = false;
                }

                _context.TLCMT_PanelIssueDetail.Remove(PanIssue);
            }
        }

        private void frmCancelTLInstruction_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
    }
