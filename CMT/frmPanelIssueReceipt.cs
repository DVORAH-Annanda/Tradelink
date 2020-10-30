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
    public partial class frmPanelIssueReceipt : Form
    {
        bool formloaded;
        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();

        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
        
        public frmPanelIssueReceipt()
        {
            InitializeComponent();
            cmboStoreFacilities.Enabled = false;

            core = new Util();
        }

        private void frmPanelIssueReceipt_Load(object sender, EventArgs e)
        {
            formloaded = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;

            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB.HeaderText = "Cut Sheet";
            oTxtB.ValueType = typeof(string);
            oTxtB.ReadOnly = true;

            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);

            oTxtC.HeaderText = "Quality";
            oTxtC.ReadOnly = true;
            oTxtC.ValueType = typeof(string);

            oTxtD.HeaderText = "Colour";
            oTxtD.ValueType = typeof(string);
            oTxtD.ReadOnly = true;

            oTxtE.HeaderText = "Bundle Qty";
            oTxtE.ReadOnly = true;
            oTxtE.ValueType = typeof(int);

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns.Add(oTxtD);
            dataGridView1.Columns.Add(oTxtE);

            using (var context = new TTI2Entities())
            {
                /*
                cmboDelivery.DataSource = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Receipted && x.CMTPI_DeliveryNumber > 0).ToList(); 
                cmboDelivery.ValueMember = "CMTPI_Pk";
                cmboDelivery.DisplayMember = "CMTPI_Number";
                cmboDelivery.SelectedValue = -1;
                */

                cmboCMT.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                cmboCMT.ValueMember = "Dep_Id";
                cmboCMT.DisplayMember = "Dep_Description";
                cmboCMT.SelectedValue = -1;
            }

          
            cmboStoreFacilities.SelectedIndex = -1;
            
            formloaded = true;
        }

        private void cmboDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLCUT_CutSheet> CutSheets = new List<TLCUT_CutSheet>();
            IList<TLCMT_PanelIssueDetail> pid = new List<TLCMT_PanelIssueDetail>();

            if (oCmbo != null && formloaded)
            {
                var selected = (TLCMT_PanelIssue)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();

                    CutSheets = core.GetCSDetails(selected.CMTPI_Pk);

                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        
                        foreach (var CutSheet in CutSheets)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = CutSheet.TLCutSH_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = CutSheet.TLCutSH_No;
                            dataGridView1.Rows[index].Cells[2].Value = true;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(CutSheet.TLCutSH_Quality_FK).TLGreige_Description;
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                            var CSR = context.TLCUT_CutSheetReceipt.FirstOrDefault(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk);
                            if (CSR != null)
                            {
                                dataGridView1.Rows[index].Cells[5].Value = CSR.TLCUTSHR_NoOfBundles.ToString();
                            }
                        }

                        var StoreFacilities = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == selected.CMTPI_Department_FK).ToList();
                        if (StoreFacilities.Count != 0)
                        {
                            cmboStoreFacilities.DataSource = StoreFacilities;
                            cmboStoreFacilities.ValueMember = "WhStore_Id";
                            cmboStoreFacilities.DisplayMember = "WhStore_Description";
                            cmboStoreFacilities.Enabled = true;
                            cmboStoreFacilities.SelectedIndex = -1;

                        }
                        else
                        {
                            MessageBox.Show("There appears to be a problem with the definition of this CMT");
                        }
                        formloaded = true;
                    }
                }
            }
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool UpDateHeader = true;

            if (oBtn != null && formloaded)
            {
                var Depts = (TLADM_Departments)cmboCMT.SelectedItem;
                if(Depts == null)
                {
                    MessageBox.Show("Please select a CMT to work with");
                    return;
                                    }
                              
                TLCMT_PanelIssue PIselected = (TLCMT_PanelIssue)cmboDelivery.SelectedItem;
                if(PIselected == null)
                {
                    MessageBox.Show("Please select a delivery number");
                    return;
                }

                if(cmboStoreFacilities.Items.Count == 0)
                {
                    MessageBox.Show("There appears to be a problem with the definition of this CMT");
                    return;
                }

                var LNSelected = (TLADM_WhseStore)cmboStoreFacilities.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var CutSheetPk = (int)row.Cells[0].Value;

                        if ((bool)row.Cells[2].Value == false)
                        {
                            DialogResult Res = MessageBox.Show("Warning . This cutsheet not selected. Will be sent back to originating Panel Store", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (Res == DialogResult.Yes)
                            {
                                var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheetPk).FirstOrDefault();
                                if(CutSheetReceipt != null)
                                {
                                    CutSheetReceipt.TLCUTSHR_InPanelStore = true;
                                    CutSheetReceipt.TLCUTSHR_Issued = false;
                                    CutSheetReceipt.TLCUTSHR_InReceiptCage = false;
                                }
                                UpDateHeader = false;
                                continue;
                            }
                        }

                        TLCMT_LineIssue li = new TLCMT_LineIssue();
                        li.TLCMTLI_Date = dtpTransDate.Value;
                        li.TLCMTLI_CutSheet_FK = CutSheetPk;
                        li.TLCMTLI_CmtFacility_FK = PIselected.CMTPI_Department_FK;
                        li.TLCMTLI_PanelIssue_FK = PIselected.CMTPI_Pk;
                        li.TLCMTLI_WhStore_FK = LNSelected.WhStore_Id;
                        li.TLCMTLI_Required_Date = dtpTransDate.Value.AddDays(7);
                        li.TLCMTLI_PanelIssue_FK = PIselected.CMTPI_Pk;
                        try
                        {
                            li.TLCMTLI_IssueQty = int.Parse(row.Cells[5].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        //---------------------------------------
                        // this is to ensure that the record is only added once to the table
                        //-------------------------------------------------------------------------------
                        var AlreadyIssued = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CutSheetPk).FirstOrDefault();
                        if(AlreadyIssued == null)
                            context.TLCMT_LineIssue.Add(li);

                        var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheetPk).FirstOrDefault();
                        if (CSR != null)
                        {
                            CSR.TLCUTSHR_InReceiptCage = true;

                            var PanIssueDetail = context.TLCMT_PanelIssueDetail.Where(X => X.CMTPID_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (PanIssueDetail != null)
                            {
                                PanIssueDetail.CMTPID_Receipted = true;
                            }
                        }

                    }

                    if (UpDateHeader)
                    {
                        PIselected = context.TLCMT_PanelIssue.Find(PIselected.CMTPI_Pk);
                        if (PIselected != null)
                        {
                            PIselected.CMTPI_Receipted = true;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        dataGridView1.Rows.Clear();

                        cmboStoreFacilities.SelectedIndex = -1;
                        cmboCMT.SelectedIndex = -1;
                        TxtStoreFacilities.Text = string.Empty;

                        MessageBox.Show("Data saved successfully to database");

                        DialogResult res = MessageBox.Show("Do you wish to print a full report", "All transactions", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {
                            frmCMTViewRep vRep = new frmCMTViewRep(3, PIselected.CMTPI_Pk);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            
                            
                        }

                       
                        
                     }
                     catch (Exception ex)
                     {
                            MessageBox.Show(ex.Message);
                     }
                    
                }
               
            }
        }

        private void cmboStoreFacilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = new ComboBox();
            if (oCmbo != null && formloaded) 
            {
                var selected = (TLADM_WhseStore)cmboStoreFacilities.SelectedItem;
                if (selected != null)
                {
                    TxtStoreFacilities.Text = selected.WhStore_Description;
                }
            }
        }

        private void cmboCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if(oCmbo != null && formloaded)
            {
                var SelectedItem = (TLADM_Departments)oCmbo.SelectedItem;

                if(SelectedItem != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        cmboDelivery.DataSource = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Department_FK == SelectedItem.Dep_Id && !x.CMTPI_Receipted && x.CMTPI_DeliveryNumber > 0).ToList(); 
                        cmboDelivery.ValueMember = "CMTPI_Pk";
                        cmboDelivery.DisplayMember = "CMTPI_Number";
                        cmboDelivery.SelectedValue = -1;
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
