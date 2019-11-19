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
    public partial class fmtCuttingBoxesAdjustment : Form
    {
        bool FormLoaded;
        Util core;
        int CutSheetReceiptBoxes_PK;

        public fmtCuttingBoxesAdjustment()
        {
            InitializeComponent();
            
            core = new Util();

            txtAdultBoxes.TextAlign = HorizontalAlignment.Right;
            txtAdultBoxes.KeyPress += core.txtWin_KeyPress;
            txtAdultBoxes.KeyDown += core.txtWin_KeyDownJI;
 
            txtKidsBoxes.TextAlign = HorizontalAlignment.Right;
            txtKidsBoxes.KeyPress += core.txtWin_KeyPress;
            txtKidsBoxes.KeyDown += core.txtWin_KeyDownJI;

            txtRibbing.TextAlign = HorizontalAlignment.Right;
            txtRibbing.KeyPress += core.txtWin_KeyPress;
            txtRibbing.KeyDown += core.txtWin_KeyDownJI;
            
            txtBinding.TextAlign = HorizontalAlignment.Right;
            txtBinding.KeyPress += core.txtWin_KeyPress;
            txtBinding.KeyDown += core.txtWin_KeyDownJI;


        }

        private void fmtCuttingBoxesAdjustment_Load(object sender, EventArgs e)
        {
            FormLoaded = false;


            CutSheetReceiptBoxes_PK = 0;

            txtCutSheetNo.Text = string.Empty;
            txtColour.Text = string.Empty;
            txtGreige.Text = string.Empty;
            txtStyles.Text = string.Empty;
 
            txtAdultBoxes.Text = "0";
            txtBinding.Text = "0";
            txtKidsBoxes.Text = "0";
            txtRibbing.Text = "0";
 
            FormLoaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    //1st check to see whether this is a valid Cut Sheet 
                    //======================================================
                    var CutSheet = context.TLCUT_CutSheet.Where(x => x.TLCutSH_No == txtCutSheetNo.Text).FirstOrDefault();
                    if (CutSheet == null)
                    {
                        MessageBox.Show("This Cut Sheet does not exist", "Cut Sheet Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                    if (CutSheetReceipt == null)
                    {
                        MessageBox.Show("This cut sheet has not yet been receipted", "Cut Sheet Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    txtColour.Text = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                    txtGreige.Text = context.TLADM_Griege.Find(CutSheet.TLCutSH_Quality_FK).TLGreige_Description;
                    txtStyles.Text = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;

                    var CutBoxes = context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault();
                    if (CutBoxes != null)
                    {
                        txtAdultBoxes.Text = CutBoxes.TLCUTSHB_AdultBoxes.ToString();
                        txtBinding.Text = CutBoxes.TLCUTSHB_Binding.ToString();
                        txtKidsBoxes.Text = CutBoxes.TLCUTSHB_KidBoxes.ToString();
                        txtRibbing.Text = CutBoxes.TLCUTSHB_Ribbing.ToString();

                        CutSheetReceiptBoxes_PK = CutBoxes.TLCUTSHB_Pk; 
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                if (CutSheetReceiptBoxes_PK == 0)
                {
                    MessageBox.Show("Please select a Cut Sheet", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var context = new TTI2Entities())
                {
                   
                        var CutBoxes = context.TLCUT_CutSheetReceiptBoxes.Find(CutSheetReceiptBoxes_PK);
                        if (CutBoxes == null)
                        {
                            MessageBox.Show("Master Key Failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var Adult   = Convert.ToInt32(txtAdultBoxes.Text);
                        var Kids    = Convert.ToInt32(txtKidsBoxes.Text);
                        var Binding = Convert.ToInt32(txtBinding.Text);
                        var Ribbing = Convert.ToInt32(txtRibbing.Text);

                        CutBoxes.TLCUTSHB_AdultBoxes = Adult;
                        CutBoxes.TLCUTSHB_Binding = Binding;
                        CutBoxes.TLCUTSHB_Ribbing = Ribbing;
                        CutBoxes.TLCUTSHB_KidBoxes = Kids;

                        try
                        {
                                context.SaveChanges();
                                MessageBox.Show("Data successfully saved to database", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch(Exception ex) 
                        {
                                MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                                fmtCuttingBoxesAdjustment_Load(this, null);
                        }

                }
            }
        }


    }
}
