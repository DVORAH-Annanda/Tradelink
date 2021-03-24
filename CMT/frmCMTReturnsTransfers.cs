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
using System.Reflection;

namespace CMT
{
    public partial class frmCMTReturnsTransfers : Form
    {
        bool formloaded;
        Util core;

        bool InternalTransfer;
        int NewLineAssigned;
 
        DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn(); // 0 CutSheet
        DataGridViewComboBoxColumn oCmboB = new DataGridViewComboBoxColumn(); // 1 To Line
        DataGridViewComboBoxColumn oCmboC = new DataGridViewComboBoxColumn(); // 2 To Alternate Facility
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();  // 3 On Hold

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Current Production Lines
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Reasons for moving 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Authorised by

        public frmCMTReturnsTransfers()
        {
            InitializeComponent();
            core = new Util();
        }

        private void frmCMTReturnsTransfers_Load(object sender, EventArgs e)
        {
            formloaded = false;
            InternalTransfer = false;
            NewLineAssigned = 0;

            using (var context = new TTI2Entities())
            {
                cmboDepartment.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboDepartment.DisplayMember = "Dep_Description";
                cmboDepartment.ValueMember = "Dep_Id";
                cmboDepartment.SelectedValue = -1;
            }

            oCmboA.HeaderText = "Select Cut Sheet";  // 0
            oCmboA.Width = 100;
            dataGridView1.Columns.Add(oCmboA);

            oTxtA.HeaderText = "Current Line";       // 1
            oTxtA.Width = 100;
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Reasons";            // 2
            oTxtB.Width = 100;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Authorised By";      // 3
            oTxtC.Width = 100;
            dataGridView1.Columns.Add(oTxtC);

            oCmboB.HeaderText = "Select Line No";    // 4 
            oCmboB.Width = 100;
            dataGridView1.Columns.Add(oCmboB);

            oChkA.HeaderText = "On Hold";            // 5 
            dataGridView1.Columns.Add(oChkA);
 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Rows.Add();

            formloaded = true;
            
        }

       

        private void cmboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            IList<TLCMT_LineIssue> LineIssue = new List<TLCMT_LineIssue>();
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_Departments)cmboDepartment.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add();
                    using (var context = new TTI2Entities())
                    {
                        var Existing =  (from li in context.TLCMT_LineIssue
                                        join cs in context.TLCUT_CutSheet
                                        on li.TLCMTLI_CutSheet_FK equals cs.TLCutSH_Pk
                                        where li.TLCMTLI_CmtFacility_FK == selected.Dep_Id && !li.TLCMTLI_IssuedToLine && !li.TLCMTLI_WorkCompleted
                                        orderby cs.TLCutSH_No
                                        select new { li, cs.TLCutSH_No}).ToList();
                       
                        foreach (var Record in Existing)
                        {
                            reportOptions.Add(new KeyValuePair<int, string>(Record.li.TLCMTLI_Pk ,
                               Record.TLCutSH_No));
                        }

                        
                        formloaded = false;
                        
                        cmboCutSheet.DataSource = reportOptions;
                        cmboCutSheet.ValueMember =  "Key";
                        cmboCutSheet.DisplayMember = "Value";
                        cmboCutSheet.SelectedIndex = -1;
                        
                        cmboLineDetails.DataSource = context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == selected.Dep_Id).OrderBy(x=>x.TLCMTCFG_DisplayOrder).ToList();
                        cmboLineDetails.ValueMember = "TLCMTCFG_Pk";
                        cmboLineDetails.DisplayMember = "TLCMTCFG_Description";
                        cmboLineDetails.SelectedIndex = -1;

                        reportOptions = new BindingList<KeyValuePair<int, string>>();
                        Existing = (from li in context.TLCMT_LineIssue
                                        join cs in context.TLCUT_CutSheet
                                        on li.TLCMTLI_CutSheet_FK equals cs.TLCutSH_Pk
                                        where li.TLCMTLI_CmtFacility_FK == selected.Dep_Id && li.TLCMTLI_IssuedToLine && !li.TLCMTLI_WorkCompleted
                                        orderby cs.TLCutSH_No
                                        select new { li, cs.TLCutSH_No }).ToList();

                        foreach (var Record in Existing)
                        {
                            reportOptions.Add(new KeyValuePair<int, string>(Record.li.TLCMTLI_Pk,
                               Record.TLCutSH_No));
                        }

                        oCmboA.DataSource = reportOptions;
                        oCmboA.ValueMember = "Key";
                        oCmboA.DisplayMember = "Value";

                        oCmboB.DataSource = null;
                        oCmboB.Items.Clear();
                        oCmboB.DataSource = context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == selected.Dep_Id).ToList();
                        oCmboB.ValueMember = "TLCMTCFG_Pk";
                        oCmboB.DisplayMember = "TLCMTCFG_Description";

                        formloaded = true;
           
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int Col3 = 0;
            TLCMT_LineIssue LineIssue = null;
            int Pk = 0;
            string Description = string.Empty;
            var RepOpts = new CMTReportOptions();

            if (oBtn != null && formloaded)
            {
                if (!InternalTransfer)
                {
                    var DeptSelected = (TLADM_Departments)cmboDepartment.SelectedItem;
                    if (DeptSelected == null)
                    {
                        MessageBox.Show("Please select an location from the drop down box");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        Object tst = cmboCutSheet.SelectedItem;
                        if (tst != null)
                        {
                            foreach (PropertyInfo prop in tst.GetType().GetProperties())
                            {
                                if (prop.Name == "Key")
                                {
                                    Pk = Convert.ToInt32(prop.GetValue(tst));
                                }
                                else if (prop.Name == "Value")
                                {
                                    Description = Convert.ToString(prop.GetValue(tst));
                                }

                            }
                        }

                        if (Pk == 0)
                        {
                            MessageBox.Show("Please select a cut sheet from the drop down box");
                            return;
                        }

                        var LineDetails = (TLCMT_FactConfig)cmboLineDetails.SelectedItem;

                        if (LineDetails == null)
                        {
                            MessageBox.Show("Please select a line to assign this cut sheet too");
                            return;
                        }

                        LineIssue = new TLCMT_LineIssue();
                        LineIssue = context.TLCMT_LineIssue.Find(Pk);
                        if (LineIssue != null)
                        {
                            LineIssue.TLCMTLI_TransactionNo = Col3;
                            LineIssue.TLCMTLI_TransferDate = DateTime.Now;
                            LineIssue.TLCMTLI_CutSheetDetails = "SI" + Description.Remove(0,2);
                            LineIssue.TLCMTLI_OnHold = false;
                            var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == LineIssue.TLCMTLI_CmtFacility_FK).FirstOrDefault();
                            if (LNU != null)
                                LineIssue.TLCMTLI_TransactionNo = LNU.col3;

                            LineIssue.TLCMTLI_Required_Date = dtpRequiredDate.Value;
                            LineIssue.TLCMTLI_LineNo_FK = LineDetails.TLCMTCFG_Pk;
                            LineIssue.TLCMTLI_IssuedToLine = true;
                        }

                        Col3 += 1;

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            MessageBox.Show("Data successfully saved to database");
                            //--------------Cut Sheet document
                            frmCMTViewRep vRep = new frmCMTViewRep(8, LineIssue.TLCMTLI_Pk);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            
                            RepOpts.ReportTitle = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK).TLCutSH_No;
                            RepOpts.SLFCutSheetFK = LineIssue.TLCMTLI_CutSheet_FK;
                            
                            /*//------------------Line Feeder Document 
                             * No longer required as they copy this onto the back of cutsheet to conserve 
                            vRep = new frmCMTViewRep(23, RepOpts);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);*////

                            vRep = new frmCMTViewRep(5, LineIssue.TLCMTLI_CutSheet_FK );
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();
                            }
                            formloaded = false;
                            cmboCutSheet.SelectedValue = -1;
                            cmboDepartment.SelectedValue = -1;
                            cmboLineDetails.SelectedValue = -1;
                            formloaded = true;

                            txtColours.Text = string.Empty;
                            txtPanels.Text = string.Empty;
                            txtSizes.Text = string.Empty;
                            txtStyle.Text = string.Empty;

                        }

                    }
                }
                else
                {

                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value == null)
                                continue;

                            var OnHold = (bool)row.Cells[5].Value;
                            if (OnHold)
                            {
                                var Reason = row.Cells[2].Value.ToString();
                                if (Reason.Length == 0)
                                {
                                    MessageBox.Show("Please provide a reason as to why this Cutsheet is placed on hold");
                                    return;
                                }
                            }


                            Pk = (int)row.Cells[0].Value;

                            LineIssue = context.TLCMT_LineIssue.Find(Pk);
                            if (LineIssue != null)
                            {
                               LineIssue.TLCMTLI_Reason = row.Cells[2].Value.ToString();
                               LineIssue.TLCMTLI_TransferApproved = row.Cells[3].Value.ToString();
                               LineIssue.TLCMTLI_TransferDate = DateTime.Now;
                               LineIssue.TLCMTLI_OnHold = OnHold;

                               if (NewLineAssigned != 0)
                                   LineIssue.TLCMTLI_LineNo_FK = NewLineAssigned;

                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");
                                                      
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add();

                            cmboCutSheet.SelectedValue = -1;
                            cmboDepartment.SelectedValue = -1;
                            cmboLineDetails.SelectedValue = -1;

                            txtColours.Text = string.Empty;
                            txtPanels.Text = string.Empty;
                            txtSizes.Text = string.Empty;
                            txtStyle.Text = string.Empty;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void cmboToDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var ToDepart = (TLADM_Departments)oCmbo.SelectedItem;
                if(ToDepart != null)
                {
                    var selected = (TLADM_Departments)cmboDepartment.SelectedItem;
                    if (selected != null)
                    {
                        if (ToDepart.Dep_Id == selected.Dep_Id)
                        {
                            MessageBox.Show("Please select an alternative CMT");
                            return;
                        }
                        using (var context = new TTI2Entities())
                        {
                            
                        }
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (combo != null)
            {
                var CurrentCell = oDgv.CurrentCell;
                if (CurrentCell.ColumnIndex == 0 || CurrentCell.ColumnIndex == 4)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int Pk = 0;

            var IndexPos = this.dataGridView1.CurrentCell.ColumnIndex;

            Object tst = cb.SelectedItem;
            if (tst != null)
            {
                if (IndexPos == 0)
                {
                    foreach (PropertyInfo prop in tst.GetType().GetProperties())
                    {
                        if (prop.Name == "Key")
                        {
                            Pk = Convert.ToInt32(prop.GetValue(tst));
                        }
                    }
                    if (Pk != 0)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var LineIssue = context.TLCMT_LineIssue.Find(Pk);
                            if (LineIssue != null)
                            {
                                var CurrentRow = dataGridView1.CurrentRow;

                                var LineDetails = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;
                                dataGridView1.Rows[CurrentRow.Index].Cells[1].Value = LineDetails;
                                dataGridView1.Rows[CurrentRow.Index].Cells[2].Value = String.Empty;
                                dataGridView1.Rows[CurrentRow.Index].Cells[3].Value = String.Empty;
                                dataGridView1.Rows[CurrentRow.Index].Cells[5].Value = LineIssue.TLCMTLI_OnHold;

                                cmboLineDetails.SelectedValue = (int)LineIssue.TLCMTLI_LineNo_FK;
                            }
                        }

                        InternalTransfer = true;
                    }
                }
                else
                {
                    foreach (PropertyInfo prop in tst.GetType().GetProperties())
                    {
                        if (prop.Name == "TLCMTCFG_Pk")
                        {
                            NewLineAssigned = Convert.ToInt32(prop.GetValue(tst));
                        }
                    }

                }
            }

        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            int Pk = 0;
          
            if (oCmbo != null && formloaded)
            {
                Object tst = cmboCutSheet.SelectedItem;
                if (tst != null)
                {
                    foreach (PropertyInfo prop in tst.GetType().GetProperties())
                    {
                        if (prop.Name == "Key")
                        {
                            Pk = Convert.ToInt32(prop.GetValue(tst));
                        }
                    }
                }

                if (Pk == 0)
                {
                    MessageBox.Show("Please select a cut sheet from the drop down box");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var LineIssue = context.TLCMT_LineIssue.Find(Pk);
                    if (LineIssue != null)
                    {
                        var CutSheet = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK);
                        if (CutSheet != null)
                        {
                            txtColours.Text = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                            txtSizes.Text = context.TLADM_Sizes.Find(CutSheet.TLCutSH_Size_FK).SI_Description;
                            txtStyle.Text = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;

                            var PanelReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                            if (PanelReceipt != null)
                            {
                                txtPanels.Text = PanelReceipt.TLCUTSHR_NoOfBundles.ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
