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
    public partial class frmCMTView : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn(); // 0 Size 
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn(); // 1 Panel Qty 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn(); // 2 Boxed Qty
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn(); // 2 Box Number

        public frmCMTView()
        {
            InitializeComponent();

            oTxtA.Visible = true;
            oTxtA.ValueType = typeof(string);
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.Visible = true;
            oTxtB.ValueType = typeof(int);
            oTxtB.ReadOnly = true; 
            oTxtB.HeaderText = "Panel Qty";
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.ValueType = typeof(int);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;
            oTxtC.HeaderText = "Boxed Qty";
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;
            oTxtD.HeaderText = "Box Number";
            dataGridView1.Columns.Add(oTxtD);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            rbOnHold.Enabled = false;

        }
        private void frmCMTView_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            txtColour.Text = string.Empty;
            txtPanelStore.Text = string.Empty;
            txtStyle.Text = string.Empty;
            txtTransferDate.Text = string.Empty;
            txtTransnumber.Text = string.Empty;
            txtPickingList.Text = string.Empty;
            txtDeliveryNote.Text = string.Empty;
            txtToWareHouse.Text = string.Empty;
            txtPickingListDate.Text = string.Empty;
            txtLineNo.Text = string.Empty;
            txtDeliveryNoteDate.Text = string.Empty;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    TLCUT_CutSheet CS = context.TLCUT_CutSheet.FirstOrDefault(x => x.TLCutSH_No == txtCutSheet.Text);
                    if (CS != null)
                    {
                        frmCMTView_Load(this, null);

                        var CSR = context.TLCUT_CutSheetReceipt.FirstOrDefault(x => x.TLCUTSHR_CutSheet_FK == CS.TLCutSH_Pk);
                        if (CSR != null)
                        {
                            if (!CSR.TLCUTSHR_Issued)
                            {
                                MessageBox.Show("This CutSheet is still in its respective Panel Store and is yet to be issued");
                                return;
                            }
                            txtStyle.Text = context.TLADM_Styles.Find(CSR.TLCUTSHR_Style_FK).Sty_Description;
                            txtColour.Text = context.TLADM_Colours.Find(CSR.TLCUTSHR_Colour_FK).Col_Display;

                            var SizeDetails = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_Pk).ToList();
                            foreach (var SizeDetail in SizeDetails)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = context.TLADM_Sizes.Find(SizeDetail.TLCUTSHRD_Size_FK).SI_Description;
                                dataGridView1.Rows[index].Cells[1].Value = SizeDetail.TLCUTSHRD_BundleQty;
                                dataGridView1.Rows[index].Cells[2].Value = SizeDetail.TLCUTSHRD_BoxUnits;
                                dataGridView1.Rows[index].Cells[3].Value = SizeDetail.TLCUTSHRD_BoxNumber;
                            }


                            var PID = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_CutSheet_FK == CSR.TLCUTSHR_Pk).FirstOrDefault();
                            if (PID != null)
                            {
                                var PI = context.TLCMT_PanelIssue.Find(PID.CMTPID_PI_FK);
                                if (PI != null)
                                {
                                    txtTransferDate.Text = PI.CMTPI_Date.ToShortDateString();
                                    txtTransnumber.Text = PI.CMTPI_DeliveryNumber.ToString();
                                    txtPanelStore.Text = context.TLADM_Departments.Find(PI.CMTPI_Department_FK).Dep_Description;
                                }
                            }

                            var LineIssue = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == CSR.TLCUTSHR_CutSheet_FK).FirstOrDefault();
                            if (LineIssue != null && LineIssue.TLCMTLI_IssuedToLine)
                            {
                                if (LineIssue.TLCMTLI_LineNo_FK != 0)
                                    txtLineNo.Text = context.TLCMT_FactConfig.Find(LineIssue.TLCMTLI_LineNo_FK).TLCMTCFG_Description;

                                rbOnHold.Checked = LineIssue.TLCMTLI_OnHold;

                                if (LineIssue.TLCMTLI_WorkCompleted)
                                {
                                    var CompletedWork = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == LineIssue.TLCMTLI_CutSheet_FK).FirstOrDefault();
                                    if (CompletedWork != null && CompletedWork.TLCMTWC_BoxReceiptedWhse)
                                    {
                                        if (CompletedWork.TLCMTWC_ToWhse_FK != 0)
                                            txtToWareHouse.Text = context.TLADM_WhseStore.Find(CompletedWork.TLCMTWC_ToWhse_FK).WhStore_Description;
                                        else
                                            txtToWareHouse.Text = "Unknown Warehouse";


                                        if (CompletedWork.TLCMTWC_Picked && CompletedWork.TLCMTWC_PickList_FK != null)
                                        {
                                            var BoxSelected = context.TLCSV_BoxSelected.Find(CompletedWork.TLCMTWC_PickList_FK);
                                            if (BoxSelected != null)
                                            {
                                                DateTime dt = new DateTime();
                                                if (BoxSelected.TLCSV_DespatchedDate != null)
                                                {
                                                    dt = (DateTime)BoxSelected.TLCSV_DespatchedDate;
                                                    txtDeliveryNoteDate.Text = dt.ToString("dd/MM/yyyy");
                                                }
                                                if (BoxSelected.TLCSV_PLTransDate != null)
                                                {
                                                    dt = (DateTime)BoxSelected.TLCSV_PLTransDate;
                                                    txtPickingListDate.Text = dt.ToString("dd/MM/yyyy");
                                                }
                                                txtPickingList.Text = BoxSelected.TLCSV_PLDetails;
                                                txtDeliveryNote.Text = BoxSelected.TLCSV_DNDeails;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Cutsheet matching number entered found in the database");
                    }
               }
            }
        }
    }
}
