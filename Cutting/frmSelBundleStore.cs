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
    public partial class frmSelBundleStore : Form
    {
        bool formloaded;
        CuttingQueryParameters QueryParms;
        CuttingRepository repo;

        public frmSelBundleStore()
        {
            InitializeComponent();

            repo = new CuttingRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCutSheets.CheckStateChanged += new System.EventHandler(this.cmboCutSheets_CheckStateChanged);
        }

        private void frmSelBundleStore_Load(object sender, EventArgs e)
        {
            QueryParms = new CuttingQueryParameters();

            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_InBundleStore && !x.TLCUTSHR_Issued);
                foreach (var Record in CSR)
                {
                    var CutSheet = context.TLCUT_CutSheet.Find(Record.TLCUTSHR_CutSheet_FK);
                    if (CutSheet != null && !CutSheet.TLCutSH_Closed)
                    {
                        cmboCutSheets.Items.Add(new Cutting.CheckComboBoxItem(Record.TLCUTSHR_Pk, CutSheet.TLCutSH_No , false));
                    }
                }

            }
            formloaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCutSheets_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.CutSheetReceipts.Add(repo.LoadCutSheetReceipt(item._Pk));

                }
                else
                {
                    var value = QueryParms.CutSheetReceipts.Find(it => it.TLCUTSHR_Pk == item._Pk);
                    if (value != null)
                        QueryParms.CutSheetReceipts.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if (oBtn != null && formloaded)
            {
               frmCutViewRep vRep = new frmCutViewRep(9, QueryParms);
               int h = Screen.PrimaryScreen.WorkingArea.Height;
               int w = Screen.PrimaryScreen.WorkingArea.Width;
               vRep.ClientSize = new Size(w, h);
               vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }
    }
}
