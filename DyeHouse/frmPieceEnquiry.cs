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

namespace DyeHouse
{
    public partial class frmPieceEnquiry : Form
    {
        DataGridViewTextBoxColumn oTxtA;   // Primary Palet Index no
        DataGridViewTextBoxColumn oTxtB;   // Pallet Number
        DataGridViewTextBoxColumn oTxtC;   // Pallet Number

        TLDYE_DyeBatchDetails DyeBatchDetail;

        bool FormLoaded;
        bool Recalculate;
        Util core;

        public frmPieceEnquiry()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Short Code";
            oTxtA.ValueType = typeof(string);

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Record Key 
            oTxtB.HeaderText = "Description";
            oTxtB.ValueType = typeof(string);

            oTxtC = new DataGridViewTextBoxColumn();   // 2 Shift Description
            oTxtC.HeaderText = "Value";
            oTxtC.ValueType = typeof(int);
            oTxtC.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // Pallet No Key 
            dataGridView1.Columns.Add(oTxtB);    // Pallet No
            dataGridView1.Columns.Add(oTxtC);    // Pallet No

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            Util core = new Util();

            txtWidth.Enabled = false;
            txtWidth.KeyDown += core.txtWin_KeyDownOEM;
            txtWidth.KeyPress += core.txtWin_KeyPress;

            txtDisk.Enabled = false;
            txtDisk.KeyDown += core.txtWin_KeyDownOEM;
            txtDisk.KeyPress += core.txtWin_KeyPress;

            txtNett.Enabled = false;
            txtNett.KeyDown += core.txtWin_KeyDownOEM;
            txtNett.KeyPress += core.txtWin_KeyPress;
        }

        private void frmPieceEnquiry_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            Recalculate = false;

          

            using (var context = new TTI2Entities())
            {
                dataGridView1.Rows.Clear();

                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();
                foreach (var Reason in Reasons)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Reason.QD_ShortCode;
                    dataGridView1.Rows[index].Cells[1].Value = Reason.QD_Description;
                    dataGridView1.Rows[index].Cells[2].Value = 0;
                }
                
                txtColour.Text = string.Empty;
                txtDisk.Text = string.Empty;
                txtDyeBatch.Text = string.Empty;
                txtDyeOrder.Text = string.Empty;
                txtGross.Text = string.Empty;
                txtKnitOrder.Text = string.Empty;
                txtMeters.Text = string.Empty;
                txtNett.Text = string.Empty;
                txtQuality.Text = string.Empty;
                txtWidth.Text = string.Empty;
                txtYarnOrder.Text = string.Empty;

                chkDBClosed.Enabled = false;
                chkDOClosed.Enabled = false;

                btnRecalc.Text = "Recalculate";
                btnRecalc.Enabled = false;

            }

            FormLoaded = true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && txtPieceNo.Text.Length > 0)
            {
               using( var context = new TTI2Entities())
               {
                    dataGridView1.Rows.Clear();
                    frmPieceEnquiry_Load(this, null);

                    FormLoaded = false;
                   
                    var Production = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PieceNo == txtPieceNo.Text).FirstOrDefault();
                    if (Production == null)
                    {
                        MessageBox.Show("No record found for value entered");
                        txtPieceNo.Text = string.Empty;
                        return;
                    }

                    if (Production.GreigeP_Operator_FK != null)
                    {
                        var Operator = context.TLADM_MachineOperators.Find(Production.GreigeP_Operator_FK);
                        if (Operator != null)
                        {
                            txtOperator.Text = Operator.MachOp_Description;
                        }
                    }

                    if (Production.GreigeP_KnitO_Fk != null)
                    {
                        if (Production.GreigeP_Greige_Fk != null)
                        {
                            var Quality = context.TLADM_Griege.Find(Production.GreigeP_Greige_Fk).TLGreige_Description;
                            if (Quality != null)
                            {
                                txtQuality.Text = Quality;
                            }
                        }

                        var KO = context.TLKNI_Order.Find(Production.GreigeP_KnitO_Fk);
                        if(KO != null)
                        {
                            txtKnitOrder.Text = KO.KnitO_OrderNumber.ToString();
                          
                            var YarnOrderPallet = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_ReservedBy == KO.KnitO_Pk).FirstOrDefault();
                            if (YarnOrderPallet != null)
                            {
                                if(YarnOrderPallet.TLKNIOP_OwnYarn)
                                {
                                    var YarnOrder = context.TLSPN_YarnOrder.Find(KO.KnitO_YarnO_FK);
                                    if (YarnOrder != null)
                                    {
                                           txtYarnOrder.Text = YarnOrder.YarnO_OrderNumber.ToString();
                                    }
                                }
                                else
                                    txtYarnOrder.Text = YarnOrderPallet.TLKNIOP_TLPalletNo; 
                            }
                            else if(KO.KnitO_YarnO_FK != null)
                            {
                                var YarnOrder = context.TLSPN_YarnOrder.Find(KO.KnitO_YarnO_FK);
                                if (YarnOrder != null)
                                {
                                    txtYarnOrder.Text = YarnOrder.YarnO_OrderNumber.ToString();
                                }
                                else
                                {
                                   var  YarnTrans = context.TLKNI_YarnTransaction.Find(KO.KnitO_YarnO_FK);
                                   if (YarnTrans != null)
                                       txtYarnOrder.Text = YarnTrans.KnitY_TransactionDoc;
                                }
                            }
                        }

                    }

                    if (Production.GreigeP_Dye)
                    {
                        DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_GreigeProduction_FK == Production.GreigeP_Pk).FirstOrDefault();
                        if (DyeBatchDetail != null)
                        {
                            txtMeters.Text = Math.Round(DyeBatchDetail.DYEBO_Meters, 2).ToString();
                            txtGross.Text = Math.Round(Production.GreigeP_weight, 2).ToString();

                            txtWidth.Text = Math.Round(DyeBatchDetail.DYEBO_Width, 2).ToString();
                            txtDisk.Text = Math.Round(DyeBatchDetail.DYEBO_DiskWeight, 2).ToString();
                            txtNett.Text = Math.Round(DyeBatchDetail.DYEBO_Nett, 2).ToString();

                            btnRecalc.Enabled = true;

                            var DyeBatch = context.TLDYE_DyeBatch.Find(DyeBatchDetail.DYEBD_DyeBatch_FK);
                            if (DyeBatch != null)
                            {
                                txtDyeBatch.Text = DyeBatch.DYEB_BatchNo;

                                var DO = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                if (DO != null)
                                {
                                    txtColour.Text = context.TLADM_Colours.Find(DO.TLDYO_Colour_FK).Col_Description;
                                    txtDyeOrder.Text = DO.TLDYO_DyeOrderNum;
                                    chkDOClosed.Checked = DO.TLDYO_Closed;
                                }

                                chkDBClosed.Checked = DyeBatch.DYEB_Closed;
                            }

                            if (DyeBatchDetail.DYEBO_CutSheet)
                            {
                                var CutSheetDet = context.TLCUT_CutSheetDetail.FirstOrDefault(x => x.TLCutSHD_DyeBatchDet_FK == DyeBatchDetail.DYEBD_Pk);
                                if (CutSheetDet != null)
                                {
                                    var CutSheet = context.TLCUT_CutSheet.Find(CutSheetDet.TLCutSHD_CutSheet_FK);
                                    if (CutSheet != null)
                                    {
                                        chkClosedCutSheet.Checked = CutSheet.TLCutSH_Closed;
                                        txtCutSheet.Text = CutSheet.TLCutSH_No;
                                    }
                                }
                            }
                        }
                    }

                    dataGridView1.Rows[0].Cells[2].Value = Production.GreigeP_Meas1;
                    dataGridView1.Rows[1].Cells[2].Value = Production.GreigeP_Meas2;
                    dataGridView1.Rows[2].Cells[2].Value = Production.GreigeP_Meas3;
                    dataGridView1.Rows[3].Cells[2].Value = Production.GreigeP_Meas4;
                    dataGridView1.Rows[4].Cells[2].Value = Production.GreigeP_Meas5;
                    dataGridView1.Rows[5].Cells[2].Value = Production.GreigeP_Meas6;
                    dataGridView1.Rows[6].Cells[2].Value = Production.GreigeP_Meas7;
                    dataGridView1.Rows[7].Cells[2].Value = Production.GreigeP_Meas8;
                    
                   FormLoaded = true;
                }
            }
        }

        private void btnRecalc_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded && DyeBatchDetail != null)
            {
                Recalculate = !Recalculate;
                if (Recalculate)
                {
                    btnRecalc.Text = "Submit";

                    txtWidth.Enabled  = true;
                    txtWidth.ReadOnly = false; 
                    txtDisk.Enabled   = true;
                    txtDisk.ReadOnly  = false;
                    txtNett.Enabled   = true;
                    txtNett.ReadOnly  = false;
                    
                    if(txtDyeOrder.Text.Length != 0)
                        chkDOClosed.Enabled = true;
                    if(txtDyeBatch.Text.Length != 0)
                        chkDBClosed.Enabled = true;
                                      
                    txtWidth.Focus();

                }
                else
                {
                    try
                    {
                        var Width = Decimal.Parse(txtWidth.Text.ToString());
                        var Nett = Decimal.Parse(txtNett.Text.ToString());
                        var DiskWeight = Decimal.Parse(txtDisk.Text.ToString());
                        
                        var core = new Util();
                        var Meters = Math.Round(core.FabricYield(Width, DiskWeight) * Nett, 2);

                        txtMeters.Text = Meters.ToString();
                        
                        using (var context = new TTI2Entities())
                        {
                            var DBD = context.TLDYE_DyeBatchDetails.Find(DyeBatchDetail.DYEBD_Pk);
                            if (DBD != null)
                            {
                                DBD.DYEBO_Meters = Meters;

                                if (DBD.DYEBO_Width != Width)
                                    DBD.DYEBO_Width = Width;

                                if (DBD.DYEBO_Nett != Nett)
                                    DBD.DYEBO_Nett = Nett;

                                if (DBD.DYEBO_DiskWeight != DiskWeight)
                                    DBD.DYEBO_DiskWeight = DiskWeight;

                                var CSD = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_DyeBatchDet_FK == DBD.DYEBD_Pk).FirstOrDefault();
                                if (CSD != null && CSD.TLCUTSHD_NettWeight != Nett)
                                {
                                    CSD.TLCUTSHD_NettWeight = Nett;
                                }

                                context.SaveChanges();
                                MessageBox.Show("Data successfully saved to the database");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                  
                    txtWidth.Enabled = false;
                    txtWidth.ReadOnly = true;
                    txtDisk.Enabled = false;
                    txtDisk.ReadOnly = true;
                    txtNett.Enabled = false;
                    txtDisk.ReadOnly = true;
                    
                    chkDBClosed.Enabled = false;
                    chkDOClosed.Enabled = false;
 
                    btnRecalc.Text = "Recalculate";
                }
            }
        }

        private void chkDOClosed_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb != null && DyeBatchDetail != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var DyeBatch = context.TLDYE_DyeBatch.Find(DyeBatchDetail.DYEBD_DyeBatch_FK);
                    if (DyeBatch != null)
                    {
                        var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                        if (DyeOrder != null)
                        {
                            DialogResult DResult = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (DResult == DialogResult.Yes)
                            {
                                DyeOrder.TLDYO_Closed = cb.Checked;
                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Data updated successfully to database");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to perform transaction");
                        }
                    }
                }
            }
        }

        private void chkDBClosed_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb != null && DyeBatchDetail != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var DyeBatch = context.TLDYE_DyeBatch.Find(DyeBatchDetail.DYEBD_DyeBatch_FK);
                    if (DyeBatch != null)
                    {
                        DialogResult DResult = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DResult == DialogResult.Yes)
                        {
                            DyeBatch.DYEB_Closed = cb.Checked;
                            try
                            {
                                context.SaveChanges();
                                MessageBox.Show("Data updated successfully to database");
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
