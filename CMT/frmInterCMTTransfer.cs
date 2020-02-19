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
    public partial class frmInterCMTTransfer : Form
    {
        DataTable dt;
        DataColumn column;
        bool FormLoaded;

        public frmInterCMTTransfer()
        {
            InitializeComponent();

            dt = new DataTable();

            //------------------------------------------------------
            // Create column 1. // This is Index Position 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "CMTLI_Pk";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select";
            column.Caption = "Select Record";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "CutSheetNumber";
            column.Caption = "CutSheet Number";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 3. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "CMTLI_Qual";
            column.Caption = "Quality";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 4. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "CMTLI_Style";
            column.Caption = "Style";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 5. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "CMTLI_Colour";
            column.Caption = "Colour";
            dt.Columns.Add(column);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                {
                    dataGridView1.Columns[idx].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    dataGridView1.Columns[col.ColumnName].Width = 120;
                }
            }
        }

        private void frmInterCMTTransfer_Load(object sender, EventArgs e)
        {
            using ( var context = new TTI2Entities())
            {
                dt.Rows.Clear();
                FormLoaded = false;
                cmboCurrentCMT.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                cmboCurrentCMT.ValueMember = "Dep_Id";
                cmboCurrentCMT.DisplayMember = "Dep_Description";
                cmboCurrentCMT.SelectedValue = -1;

                cmboToCMT.Items.Clear();
                
                FormLoaded = true;
            }
        }

        private void cmboCurrentCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                dt.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    var SelectedVal = (TLADM_Departments)oCmbo.SelectedItem;
                    if (SelectedVal != null)
                    {
                        FormLoaded = false;
                        cmboToCMT.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT && x.Dep_Id != SelectedVal.Dep_Id).OrderBy(x => x.Dep_Description).ToList();
                        cmboToCMT.ValueMember = "Dep_Id";
                        cmboToCMT.DisplayMember = "Dep_Description";
                        cmboToCMT.SelectedValue = -1;
                        FormLoaded = true;
                    }

                    var Records = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CmtFacility_FK == SelectedVal.Dep_Id && !x.TLCMTLI_IssuedToLine && !x.TLCMTLI_WorkCompleted).OrderBy(x=>x.TLCMTLI_CutSheet_FK).ToList();
                    foreach(var Record in Records)
                    {
                        DataRow Newr = dt.NewRow();
                        Newr[0] = Record.TLCMTLI_Pk;
                        Newr[1] = false;
                        var CutSheet = context.TLCUT_CutSheet.Find(Record.TLCMTLI_CutSheet_FK);
                        if(CutSheet != null)
                        {
                            Newr[2] = CutSheet.TLCutSH_No;
                            Newr[3] = context.TLADM_Griege.Find(CutSheet.TLCutSH_Quality_FK).TLGreige_Description;
                            Newr[4] = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            Newr[5] = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                        }

                        dt.Rows.Add(Newr);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a current CMT to work with");
                    return;
                }

                var RecCnt = (
                from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                where (bool)Rows.Cells[1].Value == true
                select Rows).Count();

                if(RecCnt == 0)
                {
                    MessageBox.Show("Please select a least one cutsheet to transfer");
                    return;
                }

                var ToCmt = (TLADM_Departments)cmboToCMT.SelectedItem;
                if(ToCmt == null)
                {
                    MessageBox.Show("Please select a CMT to transfer to");
                    return;
                }

                using(var context = new TTI2Entities())
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        if(dr.Field<bool>(1) == false)
                        {
                            continue;
                        }

                        var Cmt = context.TLCMT_LineIssue.Find(dr.Field<int>(0));
                        if (Cmt != null)
                            Cmt.TLCMTLI_CmtFacility_FK = ToCmt.Dep_Id;
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        
                        FormLoaded = false;
                        cmboCurrentCMT.SelectedValue = -1;
                        cmboToCMT.DataSource = null;
                        cmboToCMT.Items.Clear();
                        FormLoaded = true;

                        frmInterCMTTransfer_Load(this, null);
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
