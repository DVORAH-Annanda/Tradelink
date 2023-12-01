using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CustomerServices
{
    public partial class frmStockAdj : Form
    {
        bool formloaded;
             
        Util core;
        int TransNumber;

        TLCSV_StockOnHand SOH;

        public frmStockAdj()
        {
            InitializeComponent();
        }

        private void frmStockAdj_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            using (var context = new TTI2Entities())
            {
                 var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CSV")).FirstOrDefault();
                if (Dept != null)
                {
                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                    if (LNU != null)
                    {
                        txtTransNumber.Text = "ADJ" + LNU.col5.ToString().PadLeft(5, '0');
                        TransNumber = LNU.col6;
                    }
                }
           }

            txtBoxedQty.Text = string.Empty;
            txtBoxNumber.Text = string.Empty;
            txtColour.Text = string.Empty;
            txtSize.Text = string.Empty;
            txtStyle.Text = string.Empty;

            txtAuthorisedBy.Text = string.Empty;
            txtReasons.Text = string.Empty;

            chkWriteOff.Checked = false;

            core = new Util();
            txtBoxedQty.KeyDown += core.txtWin_KeyDownJI ;
            txtBoxedQty.KeyPress += core.txtWin_KeyPress;
            btnSave.Enabled = false;
            formloaded = true;
        }
              

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
         
            if (oBtn != null && formloaded)
            {
                if (SOH == null)
                {
                    MessageBox.Show("Please enter a box number to write off");
                    return;
                }

                if (txtReasons.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a reason for this transaction");
                    return;
                }

                if (txtAuthorisedBy.Text.Length == 0)
                {
                    MessageBox.Show("Please enter the authorising person");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    TLCSV_Movement Move = new TLCSV_Movement();
                    Move.TLMV_ToWhse_FK = SOH.TLSOH_WareHouse_FK;
                    //=======================================================
                    //Transaction No if  Written on = 1;Written off = 2;Adjustment = 3
                    //================================================================
                    if(chkWriteOff.Checked)
                        Move.TLMV_TransactionNumber = 2;
                    else
                        Move.TLMV_TransactionNumber = 3;
                    //==================================================================
                    Move.TLMV_TransDate = dtpTransDate.Value;
                    Move.TLMV_OriginalNumber = txtTransNumber.Text;
                    Move.TLMV_NoOfBoxes = 1;
                    Move.TLMV_Reasons = txtReasons.Text;
                    Move.TLMV_AuthorisedBy = txtAuthorisedBy.Text;
                    Move.TLMV_BoxNumber = SOH.TLSOH_BoxNumber;
                    Move.TLMV_BoxedQty = SOH.TLSOH_BoxedQty;
                    Move.TLMV_AjustedBoxQty = Convert.ToInt32(txtBoxedQty.Text);    
                    
                    context.TLCSV_Movement.Add(Move);

                    try
                    {
                        var soh = context.TLCSV_StockOnHand.Find(SOH.TLSOH_Pk);
                        if (soh != null)
                        {
                            soh.TLSOH_BoxedQty = Convert.ToInt32(txtBoxedQty.Text);

                            if (chkWriteOff.Checked)
                            {
                                soh.TLSOH_Write_Off = true;
                            }

                            var CompletedWork = context.TLCMT_CompletedWork.Find(soh.TLSOH_CMT_FK);
                            if (CompletedWork != null)
                            {
                                CompletedWork.TLCMTWC_Qty = Convert.ToInt32(txtBoxedQty.Text);
                            }
                        }

                        context.SaveChanges();

                        txtBoxedQty.Text = string.Empty;
                        txtBoxNumber.Text = string.Empty;
                        txtColour.Text = string.Empty;
                        txtBoxNumber.Text = string.Empty;
                        txtSize.Text = string.Empty;
                        chkWriteOff.Checked = false;


                        SOH = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    /*
                    //-------------------------------------------------------
                    //
                    //-----------------------------------------------------------
                    string Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                               .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                               .ToString();

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();

                    TLADM_DailyLog DailyLog = new TLADM_DailyLog();
                    DailyLog.TLDL_IPAddress = Mach_IP;
                    DailyLog.TLDL_Dept_Fk = Dept.Dep_Id;
                    DailyLog.TLDL_Date = DateTime.Now;
                    DailyLog.TLDL_TransDetail = "Customer Sevices Stock Adjustment";
                    DailyLog.TLDL_AuthorisedBy = txtAuthorisedBy.Text; ;
                    DailyLog.TLDL_Comments = txtReasons.Text;
                    context.TLADM_DailyLog.Add(DailyLog);

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("data successfully saved to database");
                        
                        frmCSViewRep vRep = new frmCSViewRep(12, Move.TLMV_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                     */ 

                }
            }
        }

        private void txtBoxNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                if(oTxt.Text.Length > 0)
                {
                    using (var context = new TTI2Entities())
                    {
                        var BoxDetails = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == txtBoxNumber.Text).FirstOrDefault();
                        if (BoxDetails != null)
                        {
                            if (BoxDetails.TLSOH_Picked)
                            {
                                MessageBox.Show("This Box has already been picked. No further modification allowed");
                                btnSave.Enabled = false;
                                return;
                            }

                            btnSave.Enabled = true;
                            txtStyle.Text = context.TLADM_Styles.Find(BoxDetails.TLSOH_Style_FK).Sty_Description;
                            txtColour.Text = context.TLADM_Colours.Find(BoxDetails.TLSOH_Colour_FK).Col_Display;
                            txtSize.Text = context.TLADM_Sizes.Find(BoxDetails.TLSOH_Size_FK).SI_Description;
                            txtBoxedQty.Text = BoxDetails.TLSOH_BoxedQty.ToString();
                            SOH = BoxDetails;
                        }
                    }
                }
            }
        }
    }
}
