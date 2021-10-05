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
    public partial class CuttingPanelsWasteAdjustment : Form
    {
        protected readonly TTI2Entities _context;
        TLCUT_CutSheet CSheet;
        TLCUT_CutSheetReceipt CutSReceipt;
        Util core;
        public CuttingPanelsWasteAdjustment()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            CSheet = new TLCUT_CutSheet();
            core = new Util();

            txtBoxCurrentPanels.KeyDown += core.txtWin_KeyDownOEM;
            txtBoxCurrentPanels.KeyPress += core.txtWin_KeyPress;

            txtCurrentCuttingWaste.KeyDown += core.txtWin_KeyDownOEM;
            txtCurrentCuttingWaste.KeyPress += core.txtWin_KeyPress;

            txtCutSheetNumber.Text = string.Empty;
            txtCurrentCuttingWaste.Text = string.Empty;
            txtBoxCurrentPanels.Text = string.Empty;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if(oBtn != null)
            {
                CSheet = _context.TLCUT_CutSheet.FirstOrDefault(x => x.TLCutSH_No == txtCutSheetNumber.Text);
                if(CSheet == null)
                {
                    MessageBox.Show("Cut Sheet Number Not Found");
                    CSheet = new TLCUT_CutSheet();
                    return;
                    
                }

                CutSReceipt = _context.TLCUT_CutSheetReceipt.FirstOrDefault(x => x.TLCUTSHR_CutSheet_FK == CSheet.TLCutSH_Pk);

                if(CutSReceipt != null)
                {
                    txtBoxCurrentPanels.Text = CutSReceipt.TLCUTSHR_WastePanels.ToString();
                    txtCurrentCuttingWaste.Text = CutSReceipt.TLCUTSHR_WasteCutSheet.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && CutSReceipt != null)
            {
                CutSReceipt.TLCUTSHR_WastePanels = Convert.ToDecimal(txtBoxCurrentPanels.Text);
                CutSReceipt.TLCUTSHR_WasteCutSheet = Convert.ToDecimal(txtCurrentCuttingWaste.Text);
                try
                {
                    _context.SaveChanges();
                    txtCutSheetNumber.Text = string.Empty;
                    txtCurrentCuttingWaste.Text = string.Empty;
                    txtBoxCurrentPanels.Text = string.Empty;
                    MessageBox.Show("Data successfully stored to the database");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }

        private void CuttingPanelsWasteAdjustment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
