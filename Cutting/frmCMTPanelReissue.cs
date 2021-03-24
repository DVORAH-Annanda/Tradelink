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

namespace Cutting
{
    public partial class frmCMTPanelReissue : Form
    {
        bool FormLoaded;
       
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;

        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;
        DataGridViewComboBoxColumn oCmbBoxC;

        DataGridViewCheckBoxColumn oChkA;

        Util core;

        public frmCMTPanelReissue()
        {
            InitializeComponent();
            core = new Util();

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.ReadOnly = true;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = true;
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "Select";
 
            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.HeaderText = "CutSheet";
            oTxtBoxB.Width = 220;

            txtCutSheetNo.Text = string.Empty;

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtBoxB);  

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            
        }

        private void frmCMTPanelReissue_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            txtCutSheetNo.Text = string.Empty;

            using (var context = new TTI2Entities())
            {
                cmboPanelIssue.DataSource = context.TLCMT_PanelIssue.Where(x => !x.CMTPI_Closed).ToList();
                cmboPanelIssue.ValueMember = "CMTPI_Pk";
                cmboPanelIssue.DisplayMember = "CMTPI_Number";
                cmboPanelIssue.SelectedValue = -1;
            }
            
            FormLoaded = true;
        }

        private void cmboPanelIssue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                if(dataGridView1.Rows.Count != 0) 
                    dataGridView1.Rows.Clear();
 
                using (var context = new TTI2Entities())
                {
                    var SelectedItem = (TLCMT_PanelIssue)oCmbo.SelectedItem;
                    if(SelectedItem != null)
                    {
                      TLCMT_PanelIssue pi = context.TLCMT_PanelIssue.Find(SelectedItem.CMTPI_Pk);
                      if (pi != null)
                      {
                          var pidetail = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == pi.CMTPI_Pk).ToList();
                          foreach (var item in pidetail)
                          {
                              var index = dataGridView1.Rows.Add();
                              dataGridView1.Rows[index].Cells[1].Value = false;
                              var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Find(item.CMTPID_CutSheet_FK);
                              dataGridView1.Rows[index].Cells[2].Value = context.TLCUT_CutSheet.Find(CutSheetReceipt.TLCUTSHR_CutSheet_FK).TLCutSH_No;
                              dataGridView1.Rows[index].Cells[0].Value = context.TLCUT_CutSheet.Find(CutSheetReceipt.TLCUTSHR_CutSheet_FK).TLCutSH_Pk;
                          }
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
                foreach (DataGridViewRow Row in oDgv.Rows)
                {
                    if ((bool)Row.Cells[1].Value)
                    {
                        var CurrentRow = oDgv.CurrentRow;

                        if (CurrentRow.Index != Row.Index)
                        {
                            oDgv.Rows[Row.Index].Cells[1].Value = false;
                            continue;
                        }
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                if (txtCutSheetNo.Text.Length != 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        var CutSheet = context.TLCUT_CutSheet.Where(x => x.TLCutSH_No == txtCutSheetNo.Text).FirstOrDefault();
                        if (CutSheet == null)
                        {
                            MessageBox.Show("Cut Sheet does not exist");
                            return;
                        }

                        //This form is the CMT Issued to line document 
                        //==============================================================
                        var vRep = new frmCutViewRep(14, CutSheet.TLCutSH_Pk);
                        var h = Screen.PrimaryScreen.WorkingArea.Height;
                        var w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog();
                   }
                }
                else
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if ((bool)Row.Cells[1].Value == true)
                        {
                            int Pk = (int)Row.Cells[0].Value;

                            using (var context = new TTI2Entities())
                            {
                                Cutting.frmCutViewRep vRepx = new Cutting.frmCutViewRep(13, Pk);
                                int h = Screen.PrimaryScreen.WorkingArea.Height;
                                int w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRepx.ClientSize = new Size(w, h);
                                vRepx.ShowDialog();
                                if (vRepx != null)
                                {
                                    vRepx.Close();
                                    vRepx.Dispose();
                                }

                            }
                        }
                    }
                }
                frmCMTPanelReissue_Load(this, null);
            }
        }

        private void Reprint_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oCheckB = (CheckBox)sender as CheckBox;
            if (oCheckB != null && FormLoaded && oCheckB.Checked)
            {
                dataGridView1.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    FormLoaded = false;
                    cmboPanelIssue.DataSource = null;
                    cmboPanelIssue.DataSource = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Closed).ToList();
                    cmboPanelIssue.ValueMember = "CMTPI_Pk";
                    cmboPanelIssue.DisplayMember = "CMTPI_Number";
                    cmboPanelIssue.SelectedValue = -1;
                    FormLoaded = true;
                }
            }
            else if (FormLoaded)
            {
                dataGridView1.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    FormLoaded = false;
                    cmboPanelIssue.DataSource = null;
                    cmboPanelIssue.DataSource = context.TLCMT_PanelIssue.Where(x =>! x.CMTPI_Closed).ToList();
                    cmboPanelIssue.ValueMember = "CMTPI_Pk";
                    cmboPanelIssue.DisplayMember = "CMTPI_Number";
                    cmboPanelIssue.SelectedValue = -1;
                    FormLoaded = true;
                }
            }
        }
    }
}
