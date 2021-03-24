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
    public partial class frmCMTPanelIssue : Form
    {
        object[] ColumnHeadings;
        bool formloaded;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key in CutSheetReceipt  1
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check Box to select             2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Cut Sheet                       3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Receipt Date                    4
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Colour                          5
       
            
        public frmCMTPanelIssue()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;

            ColumnHeadings = new Object[11];

            txtAdultsBoxes.TextAlign = HorizontalAlignment.Right;
            txtKidsBoxes.TextAlign = HorizontalAlignment.Right;
            txtPanels.TextAlign = HorizontalAlignment.Right;
            txtTotalBoxes.TextAlign = HorizontalAlignment.Right;
 
            ColumnHeadings[0] = oTxtA;   //  Primary Key (Record in File)
            ColumnHeadings[1] = oTxtB;   //  Primary Key in CutSheet Receipt 
            ColumnHeadings[2] = oChkA;   //  True / False  
            ColumnHeadings[3] = oTxtC;   //  Cut Sheet
            ColumnHeadings[4] = oTxtE;   //  Receipt Date
            ColumnHeadings[5] = oTxtG;   //  Colour
        }

        private void SetUp()
        {
            cmboCMTDepartment.SelectedValue = -1;
            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var Dep = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                if (Dep != null)
                {
                    label1.Text = "0";
                   
                    try
                    {
                        formloaded = false;
                        cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x=>!x.CMTPI_Closed).OrderBy(x => x.CMTPI_Number).ToList();
                        cmboCurrentPI.ValueMember = "CMTPI_Pk";
                        cmboCurrentPI.DisplayMember = "CMTPI_Number";
                        cmboCurrentPI.SelectedValue = -1;

                        formloaded = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        private void frmCMTPanelIssue_Load(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                txtAdultsBoxes.Text = "0";
                txtKidsBoxes.Text = "0";
                txtPanels.Text = "0";
                txtTotalBoxes.Text = "0";

                var h2 = (DataGridViewTextBoxColumn)ColumnHeadings[0];
                h2.HeaderText = "Primary Key (Record)";
                h2.Visible = false;
                h2.ValueType = typeof(int);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[1];
                h2.HeaderText = "Primary Key CutSheetReceipt Detail";
                h2.Visible = false;
                h2.ValueType = typeof(int);
                dataGridView1.Columns.Add(h2);

                try
                {
                    var h3 = (DataGridViewCheckBoxColumn)ColumnHeadings[2];
                    h3.HeaderText = "Select";
                    h3.ValueType = typeof(bool);
                    dataGridView1.Columns.Add(h3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
                h2.HeaderText = "Cut Sheet";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[4];
                h2.HeaderText = "Receipt Date";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[5];
                h2.HeaderText = "Colour";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                var Dep = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                if (Dep != null)
                {
                    cmboCMTDepartment.DataSource = Dep;
                    cmboCMTDepartment.ValueMember = "Dep_Id";
                    cmboCMTDepartment.DisplayMember = "Dep_Description";
                    cmboCMTDepartment.SelectedValue = -1;

                    label1.Text = "0";

                    try
                    {
                        cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x=>!x.CMTPI_Closed).OrderBy(x=>x.CMTPI_Number).ToList();
                        cmboCurrentPI.ValueMember = "CMTPI_Number";
                        cmboCurrentPI.DisplayMember = "CMTPI_Number";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                }
                
                var Whse = context.TLADM_WhseStore.Where(x => x.WhStore_PanelStore).ToList();
                if (Whse != null)
                {
                    cmboDepartments.DataSource = Whse;
                    cmboDepartments.ValueMember = "WhStore_Id";
                    cmboDepartments.DisplayMember = "WhStore_Description";
                    cmboDepartments.SelectedValue = -1;
               }
          }
          formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {          
            Button oBtn = sender as Button;
            TLADM_LastNumberUsed LNU = null;
            TLCMT_PanelIssue pi = new TLCMT_PanelIssue();
            TLCMT_PanelIssueDetail pid = new TLCMT_PanelIssueDetail();
            TLCUT_CutSheetReceipt CSR = new TLCUT_CutSheetReceipt();
            bool Add = false;

            if (oBtn != null && formloaded)
            {
                var selected = (TLADM_Departments)cmboCMTDepartment.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a CMT from the drop downlist");
                    return;
                }

                if (((TLADM_WhseStore)cmboDepartments.SelectedItem) == null)
                {
                    MessageBox.Show("Please select Store from which to make an selection");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    int Number = Convert.ToInt32(label1.Text);
                    pi = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Department_FK == selected.Dep_Id &&x.CMTPI_Number == Number).FirstOrDefault();
                    if (pi == null)
                    {
                        pi = new TLCMT_PanelIssue();
                        Add = true;
                    }
                                    
                    pi.CMTPI_Number = Number;
                    pi.CMTPI_Date = dtpTransDate.Value;
                    pi.CMTPI_Department_FK = selected.Dep_Id;
                    pi.CMTPI_Closed = false;
                    pi.CMTPI_DeliveryNumber = 0;
                    pi.CMTPI_CutSheetSummary = false;
                    pi.CMTPI_FromWhse_FK = ((TLADM_WhseStore)cmboDepartments.SelectedItem).WhStore_Id; 
                                      
                    var TransType = context.TLADM_TranactionType.Where(x => x.TrxT_Number == 100 && x.TrxT_Department_FK == selected.Dep_Id).FirstOrDefault();
                    if (TransType != null)
                        pi.CMTPI_TranType_FK = TransType.TrxT_Pk;
                    pi.CMTPI_Display = "DN -" + "0".PadLeft(4, ' ') +  " - TL " + Number.ToString().PadLeft(5, '0');
 
                    if (Add)
                        context.TLCMT_PanelIssue.Add(pi);

                    if (Add && selected != null)
                    {
                        LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == selected.Dep_Id).FirstOrDefault();
                        if (LNU != null)
                        {
                            LNU.col1 += 1;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        //---------------------------------------------------------------
                        if ((bool)row.Cells[2].Value == false)
                           continue;
                        
                        Add = true;
                        pid = new TLCMT_PanelIssueDetail();
                        if ((int)row.Cells[0].Value != 0)
                        {
                            var index = (int)row.Cells[0].Value;
                            pid = context.TLCMT_PanelIssueDetail.Find(index);
                            if (pid == null)
                                pid = new TLCMT_PanelIssueDetail();
                            else
                                Add = false;
                        }
                        //--------------------------------------------------------
                        pid.CMTPID_CutSheet_FK = (int)row.Cells[1].Value;
                        pid.CMTPID_PI_FK = pi.CMTPI_Pk;
                        pid.CMTPID_Receipted = false;
                        pid.CMTPID_BIFabric  = false;

                        CSR = context.TLCUT_CutSheetReceipt.Find(pid.CMTPID_CutSheet_FK);
                        if (CSR != null)
                            CSR.TLCUTSHR_Issued = true;

                        if (Add)
                        {
                            context.TLCMT_PanelIssueDetail.Add(pid);
                        }
                    }

                    try
                    {
                        var SelectedRows = (
                                        from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                        where (bool)Rows.Cells[2].Value == true
                                        select Rows).ToList();

                        foreach (var SelectedRow in SelectedRows)
                        {
                            var index = SelectedRow.Index;
                            dataGridView1.Rows.RemoveAt(index);
                        }

                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to to database");
                        
                        //This report has been housed in the the Cutting Form View Program....
                        Cutting.frmCutViewRep vRep = new Cutting.frmCutViewRep(12, pi.CMTPI_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog();
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();
                        }
                    }

                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
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
                    }
     
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var CurrentRow = oDgv.CurrentRow;
                if (CurrentRow != null)
                {
                    using (var context = new TTI2Entities())
                    {
                            if ((int)CurrentRow.Cells[1].Value != 0)
                            {
                                var index = (int)CurrentRow.Cells[1].Value;
                                var Existing = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == index).FirstOrDefault();
                                if (Existing != null)
                                {
                                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                                    {
                                        int TotalBoxes = 0;

                                        int tot1 = Convert.ToInt32(txtAdultsBoxes.Text);
                                        TotalBoxes += tot1 + Existing.TLCUTSHB_AdultBoxes;
                                        txtAdultsBoxes.Text = (tot1 + Existing.TLCUTSHB_AdultBoxes).ToString();

                                        tot1 = Convert.ToInt32(txtKidsBoxes.Text);
                                        TotalBoxes += tot1 + Existing.TLCUTSHB_KidBoxes;
                                        txtKidsBoxes.Text = (tot1 + Existing.TLCUTSHB_KidBoxes).ToString();

                                        tot1 = Convert.ToInt32(txtPanels.Text);
                                        var totPanels = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == index && x.TLCUTSHRD_BoxUnits != 0 && !x.TLCUTSHRD_PanelRejected && !x.TLCUTSHRD_ToCMT).Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;
                                        txtPanels.Text = (tot1 + totPanels).ToString();

                                        txtTotalBoxes.Text = TotalBoxes.ToString();

                                    }
                                    else
                                    {
                                        DialogResult Res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (Res == DialogResult.Yes)
                                        {
                                            index = (int)CurrentRow.Cells[0].Value;
                                            var pid = context.TLCMT_PanelIssueDetail.Find(index);
                                            if (pid != null)
                                                context.TLCMT_PanelIssueDetail.Remove(pid);

                                            index = (int)CurrentRow.Cells[1].Value;
                                            var CSR = context.TLCUT_CutSheetReceipt.Find(index);
                                            if (CSR != null)
                                            {
                                                CSR.TLCUTSHR_Issued = false;
                                                CSR.TLCUTSHR_DateIntoPanelStore = DateTime.Now;
                                                CSR.TLCUTSHR_InPanelStore = true;
                                            }

                                            int TotalBoxes = 0;

                                            int tot1 = Convert.ToInt32(txtAdultsBoxes.Text);
                                            TotalBoxes -= tot1 - Existing.TLCUTSHB_AdultBoxes;
                                            txtAdultsBoxes.Text = (tot1 - Existing.TLCUTSHB_AdultBoxes).ToString();

                                            tot1 = Convert.ToInt32(txtKidsBoxes.Text);
                                            TotalBoxes -= tot1 - Existing.TLCUTSHB_KidBoxes;
                                            txtKidsBoxes.Text = (tot1 - Existing.TLCUTSHB_KidBoxes).ToString();
                                                                                      

                                            tot1 = Convert.ToInt32(txtPanels.Text);
                                            var totPanels = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == index && x.TLCUTSHRD_BoxUnits != 0 && !x.TLCUTSHRD_PanelRejected && !x.TLCUTSHRD_ToCMT).Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0;
                                            txtPanels.Text = (tot1 - totPanels).ToString();

                                            txtTotalBoxes.Text = TotalBoxes.ToString();

                                            try
                                            {
                                                context.SaveChanges();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                    }
                                }
                            }
                      }
                }
            }
        }

        private void cmboCurrentPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            TLCMT_PanelIssueDetail result = null;
            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    dataGridView1.Rows.Clear();

                    var selected = (TLCMT_PanelIssue)cmboCurrentPI.SelectedItem;
                    if (selected != null)
                    {
                        cmboCMTDepartment.SelectedValue = selected.CMTPI_Department_FK;
                        label1.Text = selected.CMTPI_Number.ToString(); 
                        
                        var Current = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == selected.CMTPI_Pk).ToList();

                        var Existing = context.TLCUT_CutSheetReceipt.ToList();
                        foreach (var CutSheetR in Existing)
                        {
                            result = (from u in Current
                                      where u.CMTPID_CutSheet_FK == CutSheetR.TLCUTSHR_Pk
                                      select u).FirstOrDefault();

                            if (CutSheetR.TLCUTSHR_Issued && result == null)
                            {
                                    continue;
                            }

                            var index = dataGridView1.Rows.Add();

                            if (result == null)
                                dataGridView1.Rows[index].Cells[0].Value = 0;
                            else
                                dataGridView1.Rows[index].Cells[0].Value = result.CMTPID_Pk;

                            dataGridView1.Rows[index].Cells[1].Value = CutSheetR.TLCUTSHR_Pk;
                            if(CutSheetR.TLCUTSHR_Issued)
                                dataGridView1.Rows[index].Cells[2].Value = true;
                            else
                                dataGridView1.Rows[index].Cells[2].Value = false;

                            //-------------------------------------------------------
                            //find the Original CutSheet
                            //--------------------------------------------------
                            var CS = context.TLCUT_CutSheet.Find(CutSheetR.TLCUTSHR_CutSheet_FK);
                            if (CS != null)
                            {
                                dataGridView1.Rows[index].Cells[3].Value = "CS" + CS.TLCutSH_No.Remove(0, 2).ToString();
                            }

                            dataGridView1.Rows[index].Cells[4].Value = CutSheetR.TLCUTSHR_Date.ToShortDateString();
                            var xColour = context.TLADM_Colours.Find(CutSheetR.TLCUTSHR_Colour_FK);
                            if(xColour != null)
                                dataGridView1.Rows[index].Cells[5].Value = xColour.Col_Display;
                      
                            if (CutSheetR.TLCUTSHR_Issued)
                            {
                                var Grp = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetR.TLCUTSHR_Pk);
                                if (Grp.Count() != 0)
                                {
                                   // dataGridView1.Rows[index].Cells[10].Value = Grp.Sum(x => x.TLCUTSHRD_BoxUnits);
                                    txtPanels.Text = (Convert.ToInt32(txtPanels.Text) + Grp.Sum(x => x.TLCUTSHRD_BoxUnits)).ToString();
                                }
                                var Boxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetR.TLCUTSHR_Pk).FirstOrDefault();
                                if (Boxes != null)
                                {
                                    int TotalBoxes = Convert.ToInt32(txtTotalBoxes.Text);
                                    txtAdultsBoxes.Text = (Convert.ToInt32(txtAdultsBoxes.Text) + Boxes.TLCUTSHB_AdultBoxes).ToString();
                                    TotalBoxes += Boxes.TLCUTSHB_AdultBoxes;
                                    txtKidsBoxes.Text = (Convert.ToInt32(txtKidsBoxes.Text) + Boxes.TLCUTSHB_KidBoxes).ToString();
                                    TotalBoxes += Boxes.TLCUTSHB_KidBoxes;
                                    txtTotalBoxes.Text = TotalBoxes.ToString();

                                }
                            }
                        }
                    }
                }
            }
        }

       
        private void cmboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    var selected = (TLADM_Departments)oCmbo.SelectedItem;
                    if (selected != null)
                    {
                        var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == selected.Dep_Id).FirstOrDefault();
                        if (LNU != null)
                        {
                            label1.Text = LNU.col1.ToString();
                        }

                        formloaded = false;
                        cmboCurrentPI.DataSource = null;
                        cmboCurrentPI.DataSource = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Department_FK == selected.Dep_Id && !x.CMTPI_Receipted).ToList();
                        cmboCurrentPI.ValueMember = "CMTPI_Pk";
                        cmboCurrentPI.DisplayMember = "CMTPI_Number";
                        cmboCurrentPI.SelectedValue = 1;
                        formloaded = true;
                    }
                }
            }
        }

   
        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBox oCmbo = sender as ComboBox;

             if (oCmbo != null && formloaded)
             {
                 var selected = (TLADM_WhseStore)oCmbo.SelectedItem;
                 if (selected != null)
                 {
                     using (var context = new TTI2Entities())
                     {
                         var CutSheetReceipts = (from CutSheet in context.TLCUT_CutSheet
                                         join CutSheetReceipt in context.TLCUT_CutSheetReceipt on CutSheet.TLCutSH_Pk equals CutSheetReceipt.TLCUTSHR_CutSheet_FK
                                         where !CutSheetReceipt.TLCUTSHR_Issued && CutSheetReceipt.TLCUTSHR_InPanelStore && CutSheetReceipt.TLCUTSHR_WhsePanStore_FK == selected.WhStore_Id
                                         select new { CutSheet, CutSheetReceipt }).ToList();

                         foreach (var CutSheetR in CutSheetReceipts)
                         {
                             var index = dataGridView1.Rows.Add();

                             dataGridView1.Rows[index].Cells[0].Value = 0;
                             dataGridView1.Rows[index].Cells[1].Value = CutSheetR.CutSheetReceipt.TLCUTSHR_Pk;
                             dataGridView1.Rows[index].Cells[2].Value = false;
                            
                             dataGridView1.Rows[index].Cells[3].Value = "CS" + CutSheetR.CutSheet.TLCutSH_No.Remove(0, 2).ToString();
                             dataGridView1.Rows[index].Cells[4].Value = Convert.ToDateTime(CutSheetR.CutSheet.TLCutSH_Date.ToShortDateString()).ToString("dd/MM/yyyy");
                             dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(CutSheetR.CutSheet.TLCutSH_Colour_FK).Col_Display;
                       
                         }
                     }    
                     
                 }
                 else
                 {
                     MessageBox.Show("Please select a Cutting Department");
                 }
             }

        }
    }
}
