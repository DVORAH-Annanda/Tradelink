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
    public partial class frmCutSheetReturn : Form
    {
        bool FormLoaded;
        DataTable dt;
        BindingSource BindingSrc;

        public frmCutSheetReturn()
        {
            InitializeComponent();
        }

        private void frmCutSheetReturn_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboWhses.Visible = false;

                BindingSrc = new BindingSource();

                cmboCMTs.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                cmboCMTs.ValueMember = "Dep_Id";
                cmboCMTs.DisplayMember = "Dep_Description";
                cmboCMTs.SelectedIndex = -1;

                cmboDepartments.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCut).OrderBy(x => x.Dep_Description).ToList();
                cmboDepartments.ValueMember = "Dep_Id";
                cmboDepartments.DisplayMember = "Dep_Description";
                cmboDepartments.SelectedIndex = -1;
                cmboDepartments.Visible = false;

                cmboWhses.DataSource = context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).ToList();
                cmboWhses.ValueMember = "WhStore_Id";
                cmboWhses.DisplayMember = "Whstore_Description";
                cmboWhses.SelectedIndex = -1;
                cmboWhses.Visible = false;

                dt = new DataTable();
                DataColumn column;

                dataGridView1.AutoGenerateColumns = false;

                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "LineIssue_Pk";
                column.Caption = "LineIssue Primary Key";
                column.DefaultValue = 0;
                dt.Columns.Add(column);
                dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };


                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Selected_Pk";
                column.Caption = "Selected";
                column.DefaultValue = false;
                dt.Columns.Add(column);

                //--------------------------------------------------------
                // Col 2
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(String);
                column.ColumnName = "CutSheet_No";
                column.Caption = "CutSheet No";
                column.DefaultValue = string.Empty;
                dt.Columns.Add(column);

                //-----------------------
                // Col 3
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Style";
                column.Caption = "Style";
                column.DefaultValue = string.Empty;
                dt.Columns.Add(column);

                //-----------------------
                // Col 4
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "NoOfBoxes";
                column.Caption = "No Of Boxes";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //-----------------------
                // Col 5
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CutSheet_Pk";
                column.Caption = "Cut Sheet Primary Key";
                column.DefaultValue = 0;
                dt.Columns.Add(column);

                //0 -- index of record 
                //--------------------------------------------
                var selecta = new DataGridViewTextBoxColumn();
                selecta.Name = "CutSheet_Pk";
                selecta.ValueType = typeof(Int32);
                selecta.DataPropertyName = dt.Columns[0].ColumnName;
                selecta.HeaderText = "CutSheet Primary Key";
                dataGridView1.Columns.Add(selecta);
                dataGridView1.Columns["CutSheet_Pk"].DisplayIndex = 0;

                //1 -- index of record 
                //--------------------------------------------
                var selectb = new DataGridViewCheckBoxColumn();
                selectb.Name = "Selected_Item";
                selectb.ValueType = typeof(bool);
                selectb.DataPropertyName = dt.Columns[1].ColumnName;
                selectb.HeaderText = "Selected Item";
                dataGridView1.Columns.Add(selectb);
                dataGridView1.Columns["Selected_Item"].DisplayIndex = 1;

                //2 -- index of record 
                //--------------------------------------------
                var selectc = new DataGridViewTextBoxColumn();
                selectc.Name = "CutSheetNo";
                selectc.ValueType = typeof(string);
                selectc.DataPropertyName = dt.Columns[2].ColumnName;
                selectc.HeaderText = "CutSheet No";
                dataGridView1.Columns.Add(selectc);
                dataGridView1.Columns["CutSheetNo"].DisplayIndex = 2;

                //3 -- index of record 
                //--------------------------------------------
                var selectd = new DataGridViewTextBoxColumn();
                selectd.Name = "Style";
                selectd.ValueType = typeof(String);
                selectd.DataPropertyName = dt.Columns[3].ColumnName;
                selectd.HeaderText = "Style";
                dataGridView1.Columns.Add(selectd);
                dataGridView1.Columns["Style"].DisplayIndex = 3;

                // 3-- index of record
                //--------------------------------------------
                var selecte = new DataGridViewTextBoxColumn();
                selecte.Name = "NoOfBoxes";
                selecte.ValueType = typeof(String);
                selecte.DataPropertyName = dt.Columns[4].ColumnName;
                selecte.HeaderText = "Cut Sheet No";
                dataGridView1.Columns.Add(selecte);
                dataGridView1.Columns["NoOfBoxes"].DisplayIndex = 4;

                // 3-- index of record
                //--------------------------------------------
                var selectf = new DataGridViewTextBoxColumn();
                selectf.Name = "CS_Primarykey";
                selectf.ValueType = typeof(String);
                selectf.DataPropertyName = dt.Columns[5].ColumnName;
                selectf.HeaderText = "Cut Sheet No";
                dataGridView1.Columns.Add(selectf);
                dataGridView1.Columns["CS_PrimaryKey"].DisplayIndex = 5;
                dataGridView1.Visible = false;

                BindingSrc.DataSource = dt;
                dataGridView1.DataSource = BindingSrc;

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;

                int idx = -1;

                foreach (DataColumn col in dt.Columns)
                {
                    if (++idx == 0)
                        dataGridView1.Columns[idx].Visible = false;
                    else
                    {
                        if (idx < 5)
                        {
                            dataGridView1.Columns[idx].HeaderText = col.Caption;
                            dataGridView1.Columns[idx].Visible = true;
                        }
                        else
                        {
                            dataGridView1.Columns[idx].Visible = false;
                        }
                    }

                }
            }
            FormLoaded = true;
        }

        private void cmboCMTs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;


            if (oCmbo != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    dt.Rows.Clear();

                    var LineIssues = context.TLCMT_LineIssue.Where(x => !x.TLCMTLI_WorkCompleted).ToList();

                    if(LineIssues.Count == 0)
                    {
                        MessageBox.Show("No records found for selection made");
                        return;
                    }
                    foreach (var LineIssue in LineIssues)
                    {
                        DataRow NewRow = dt.NewRow();
                        NewRow[0] = LineIssue.TLCMTLI_Pk;
                        NewRow[1] = false;
                        NewRow[2] = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK).TLCutSH_No;
                        NewRow[3] = (from CS in context.TLCUT_CutSheet
                                     join Style in context.TLADM_Styles
                                     on CS.TLCutSH_Styles_FK equals Style.Sty_Id
                                     select Style).FirstOrDefault().Sty_Description; // context.TLADM_Styles.Find(LineIssueut.TLCutSH_Styles_FK).Sty_Description;
                        NewRow[4] = (from CDetails in context.TLCUT_CutSheetReceiptDetail
                                     join CReceipt in context.TLCUT_CutSheetReceipt
                                     on CDetails.TLCUTSHRD_CutSheet_FK equals CReceipt.TLCUTSHR_Pk
                                     where CReceipt.TLCUTSHR_CutSheet_FK == LineIssue.TLCMTLI_CutSheet_FK
                                     select CDetails).Count();
                        NewRow[5] = LineIssue.TLCMTLI_CutSheet_FK; 

                        dt.Rows.Add(NewRow);


                    }
                    if (!dataGridView1.Visible)
                        dataGridView1.Visible = true;
                }
            }
        }

        private void rbReturnToPanelStore_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oBtn = sender as RadioButton;
            if (oBtn != null && FormLoaded)
            {
                if (oBtn.Checked)
                {
                    using (var context = new TTI2Entities())
                    {
                        cmboDepartments.Visible = false;

                        if (!cmboWhses.Visible)
                        {
                            cmboWhses.Visible = true;
                        }
                    }
                }
            }
        }

        private void rbReturnToWIPCutting_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oBtn = sender as RadioButton;
            if (oBtn != null && FormLoaded)
            {
                if (oBtn.Checked)
                {
                    using (var context = new TTI2Entities())
                    {
                        cmboWhses.Visible = false;

                        if (!cmboDepartments.Visible)
                        {
                            cmboDepartments.Visible = true;
                        }
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Object DestSelected = null;

            if (oBtn != null && FormLoaded)
            {
                var Cmt = (TLADM_Departments)cmboCMTs.SelectedItem;
                if (Cmt == null)
                {
                    MessageBox.Show("Please select a CMT from the dropdown above");
                    return;
                }

                if (rbReturnToPanelStore.Checked)
                {
                    DestSelected = (TLADM_WhseStore)cmboWhses.SelectedItem;
                    if (DestSelected == null)
                    {
                        MessageBox.Show("Please select a destination from the dropdown above");
                        return;
                    }
                }
                else
                {
                    DestSelected = (TLADM_Departments)cmboDepartments.SelectedItem;
                    if (DestSelected == null)
                    {
                        MessageBox.Show("Please select a destination from the dropdown above");
                        return;
                    }
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        if (!Row.Field<bool>(1))
                        {
                            continue;
                        }

                        //1st no matter what Button selected have to re

                        var CutSheet_Pk = Row.Field<Int32>(5);

                        var ResPk = (from CS in context.TLCUT_CutSheet
                                     join CSR in context.TLCUT_CutSheetReceipt
                                     on CS.TLCutSH_Pk equals CSR.TLCUTSHR_CutSheet_FK
                                     where CS.TLCutSH_Pk == CutSheet_Pk
                                     select CSR).FirstOrDefault().TLCUTSHR_Pk;


                        var PIDetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_CutSheet_FK == ResPk).FirstOrDefault();
                        if (PIDetail != null)
                        {
                            context.TLCMT_PanelIssueDetail.Remove(PIDetail);
                        }

                        var CsReceipt = context.TLCUT_CutSheetReceipt.Find(ResPk);

                        if (CsReceipt != null && rbReturnToPanelStore.Checked)
                        {
                            CsReceipt.TLCUTSHR_DateIntoPanelStore = DateTime.Now;
                            CsReceipt.TLCUTSHR_InReceiptCage = false;
                            CsReceipt.TLCUTSHR_Issued = false;
                            CsReceipt.TLCUTSHR_InPanelStore = true;
                            if (rbReturnToPanelStore.Checked)
                            {
                                var SelectedItem = (TLADM_WhseStore)cmboWhses.SelectedItem;

                                CsReceipt.TLCUTSHR_WhsePanStore_FK = SelectedItem.WhStore_Id;
                            }
                        }
                        else if (CsReceipt != null && rbReturnToWIPCutting.Checked)
                        {
                            context.TLCUT_CutSheetReceiptDetail.RemoveRange(context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CsReceipt.TLCUTSHR_Pk));
                            context.TLCUT_CutSheetReceipt.Remove(CsReceipt);


                            var CutSh = context.TLCUT_CutSheet.Find(CsReceipt.TLCUTSHR_CutSheet_FK);
                            if (CutSh != null)
                            {
                                var EU = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSh.TLCutSH_Pk).FirstOrDefault();
                                if (EU != null)
                                {
                                    context.TLCUT_ExpectedUnits.Remove(EU);
                                }

                                var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSh.TLCutSH_Pk).FirstOrDefault();
                                if (Boxes != null)
                                {
                                    context.TLCUT_CutSheetReceiptBoxes.Remove(Boxes);
                                }
                                CutSh.TLCutSH_Closed = false;
                                CutSh.TLCutSH_ClosedDate = null;
                                CutSh.TLCUTSH_Completed_Date = null;
                                CutSh.TLCutSH_WIPComplete = false;

                            }

                           
                        }

                        var LiPk = Row.Field<Int32>(0);
                        var li = context.TLCMT_LineIssue.Find(LiPk);
                        if (li != null)
                        {
                            context.TLCMT_LineIssue.Remove(li);
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved to database successfully");
                        frmCutSheetReturn_Load(this, null);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }

                }
            }
        }
    }
}
