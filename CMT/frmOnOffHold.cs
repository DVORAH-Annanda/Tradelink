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
    public partial class frmOnOffHold : Form
    {
        bool FormLoaded;
        bool RememberSave;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key (Line Issue)         0
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check box                        1
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // CutSheet No                      2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Style                            3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Colour                           4                    
        

        public frmOnOffHold()
        {
            InitializeComponent();

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
            oTxtB = new DataGridViewTextBoxColumn();   // 2 Cut Sheet No 
            oTxtB.HeaderText = "CutSheet No";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();   // 3 Style  
            oTxtC.HeaderText = "Style";
            oTxtC.ValueType = typeof(string);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);


            oTxtD = new DataGridViewTextBoxColumn();   // 4 Colour 
            oTxtD.HeaderText = "Colour";
            oTxtD.ValueType = typeof(String);
            oTxtD.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtD);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void frmOnOffHold_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            rbPlaceOnHold.Checked = true;
            RememberSave = false;

            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                cmboDepartments.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboDepartments.DisplayMember = "Dep_Description";
                cmboDepartments.ValueMember = "Dep_Id";
                cmboDepartments.SelectedValue = -1;


                var tst = (from LIss in context.TLCMT_LineIssue
                           join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CutSheet_FK equals CSheet.TLCutSH_Pk
                           join CWork in context.TLCMT_CompletedWork on LIss.TLCMTLI_Pk  equals CWork.TLCMTWC_LineIssue_FK
                           where !LIss.TLCMTLI_OnHold && !LIss.TLCMTLI_WorkCompleted && LIss.TLCMTLI_IssuedToLine
                           orderby CSheet.TLCutSH_No
                           select new { LIss, CSheet }).Distinct().ToList();

                var  tst1 = (from LIss in context.TLCMT_LineIssue
                           join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CutSheet_FK equals CSheet.TLCutSH_Pk
                           join CWork in context.TLCMT_CompletedWork on LIss.TLCMTLI_Pk equals CWork.TLCMTWC_LineIssue_FK
                           where !LIss.TLCMTLI_OnHold && LIss.TLCMTLI_WorkCompleted && !CWork.TLCMTWC_Picked && LIss.TLCMTLI_IssuedToLine
                           orderby CSheet.TLCutSH_No
                           select new { LIss, CSheet }).Distinct().ToList();

                
                tst.Concat(tst1);


                foreach (var Record in tst)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Record.LIss.TLCMTLI_Pk ;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = Record.CSheet.TLCutSH_No;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(Record.CSheet.TLCutSH_Styles_FK).Sty_Description;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(Record.CSheet.TLCutSH_Colour_FK).Col_Display;
                }
            }
                     
            FormLoaded = true;
        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var SelectedItem = (TLADM_Departments)oCmbo.SelectedItem;
                if (SelectedItem == null)
                {
                    MessageBox.Show("Please select a CMT from the dropdown box");
                    return;


                }
                using (var context = new TTI2Entities())
                {
                   var tst = (from LIss in context.TLCMT_LineIssue
                               join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CutSheet_FK equals CSheet.TLCutSH_Pk
                               where !LIss.TLCMTLI_OnHold && LIss.TLCMTLI_CmtFacility_FK == SelectedItem.Dep_Id && !LIss.TLCMTLI_WorkCompleted && LIss.TLCMTLI_IssuedToLine
                               orderby CSheet.TLCutSH_No
                               select new { LIss, CSheet }).Distinct().ToList();

                   /* var tst1 = (from LIss in context.TLCMT_LineIssue
                                join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CustSheet_FK equals CSheet.TLCutSH_Pk
                                join CWork in context.TLCMT_CompletedWork on LIss.TLCMTLI_Pk equals CWork.TLCMTWC_LineIssue_FK
                                where !LIss.TLCMTLI_OnHold && LIss.TLCMTLI_WorkCompleted && !CWork.TLCMTWC_Picked
                                orderby CSheet.TLCutSH_No
                                select new { LIss, CSheet }).Distinct().ToList();


                    tst.Concat(tst1); */


                    foreach (var Record in tst)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Record.LIss.TLCMTLI_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Record.CSheet.TLCutSH_No;
                        dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(Record.CSheet.TLCutSH_Styles_FK).Sty_Description;
                        dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(Record.CSheet.TLCutSH_Colour_FK).Col_Display;
                    }
                }
            }

        }

        private void rbPlaceOnHold_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                var SelectedItem = (TLADM_Departments)cmboDepartments.SelectedItem;
                if (SelectedItem == null)
                    frmOnOffHold_Load(this, null);
                else
                {
                    using (var context = new TTI2Entities())
                    {
                        var tst = (from LIss in context.TLCMT_LineIssue
                                   join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CutSheet_FK equals CSheet.TLCutSH_Pk
                                   where !LIss.TLCMTLI_OnHold && LIss.TLCMTLI_CmtFacility_FK == SelectedItem.Dep_Id && !LIss.TLCMTLI_WorkCompleted
                                   orderby CSheet.TLCutSH_No
                                   select new { LIss, CSheet }).Distinct().ToList();

                        foreach (var Record in tst)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Record.LIss.TLCMTLI_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Record.CSheet.TLCutSH_No;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(Record.CSheet.TLCutSH_Styles_FK).Sty_Description;
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(Record.CSheet.TLCutSH_Colour_FK).Col_Display;
                        }
                    }
                }
            }
        }

        private void rbPlaceOffHold_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                FormLoaded = false;
                
                dataGridView1.Rows.Clear();

                var SelectedItem = (TLADM_Departments)cmboDepartments.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    var tst = (from LIss in context.TLCMT_LineIssue
                               join CSheet in context.TLCUT_CutSheet on LIss.TLCMTLI_CutSheet_FK equals CSheet.TLCutSH_Pk
                               where LIss.TLCMTLI_OnHold && SelectedItem.Dep_Id == LIss.TLCMTLI_CmtFacility_FK
                               orderby CSheet.TLCutSH_No
                               select new { LIss, CSheet }).Distinct().ToList();

                    foreach (var Record in tst)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Record.LIss.TLCMTLI_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Record.CSheet.TLCutSH_No;
                        dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(Record.CSheet.TLCutSH_Styles_FK).Sty_Description;
                        dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(Record.CSheet.TLCutSH_Colour_FK).Col_Display;
                    }

                }

                FormLoaded = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).FirstOrDefault();

                if (SingleRow == null)
                {
                    MessageBox.Show("Please select at least one CutSheet");
                    return;
                }

                if (rbPlaceOnHold.Checked)
                {
                    if (txtAuthorisedBy.Text.Length == 0)
                    {
                        MessageBox.Show("Please complete the Authorised By field");
                        return;
                    }

                    if ( txtReasons.Text.Length == 0)
                    {
                        MessageBox.Show("Please complete the Reasons");
                        return;
                    }
                }

                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (!(bool)Row.Cells[1].Value)
                            continue;

                        var PK = (int)Row.Cells[0].Value;

                        var LineIssue = context.TLCMT_LineIssue.Find(PK);
                        if (LineIssue != null)
                        {
                            if (rbPlaceOffHold.Checked)
                            {
                                LineIssue.TLCMTLI_OnHold = false;
                                LineIssue.TLCMTLI_Reason = String.Empty;
                                LineIssue.TLCMTLI_TransferApproved = String.Empty;
                            }
                            else
                            {
                                LineIssue.TLCMTLI_OnHold = true;
                                LineIssue.TLCMTLI_Reason = txtReasons.Text;;
                                LineIssue.TLCMTLI_TransferApproved = txtAuthorisedBy.Text;
                                LineIssue.TLCMTLI_TransferDate = dtpTransDate.Value;

                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");

                        if (RememberSave)
                            RememberSave = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        dataGridView1.Rows.Clear();
                        cmboDepartments.SelectedValue = -1;

                        if (rbPlaceOnHold.Checked)
                        {

                            txtAuthorisedBy.Text = string.Empty;
                            txtReasons.Text = string.Empty;
                        }
                    }

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
            
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 if ((bool)oDgv.CurrentCell.EditedFormattedValue && rbPlaceOnHold.Checked)
                 {
                     var PK = (int)oDgv.CurrentRow.Cells[0].Value;
                     try
                     {
                         using (frmCMTNonCompliance NonC = new frmCMTNonCompliance(PK))
                         {
                             DialogResult dr = NonC.ShowDialog(this);
                             if (dr == DialogResult.OK)
                             {

                             }
                         }

                         RememberSave = true;

                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }
                 }
                 else
                 {
                     RememberSave = false;
                 }
             }
        }

        private void frmOnOffHold_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form oFrm = (Form)sender;
            if (oFrm != null && this.RememberSave)
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show("There is a transaction that must be saved prior to existing this transaction");
                    e.Cancel = true;
                }
            }
        }

        
    }
}
