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
    public partial class frmQaReportSelection : Form
    {
        bool formloaded;

        CuttingRepository repo;
        CuttingQueryParameters parms;

        public frmQaReportSelection()
        {
            InitializeComponent();

            parms = new CuttingQueryParameters();
            repo = new CuttingRepository();

            
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCutSheets.CheckStateChanged += new System.EventHandler(this.cmboQAResults_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
        }

        private void frmQaReportSelection_Load(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new Cutting.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }
               

                var Query = from T1 in context.TLCUT_CutSheetReceiptDetail
                            join T2 in context.TLCUT_QAResults on T1.TLCUTSHRD_Pk equals T2.TLCUTQA_Bundle_FK
                            group T2 by new { T2.TLCUTQA_Bundle_FK, T1.TLCUTSHRD_BoxNumber } into g
                            select new
                            {
                                Pk = g.Key.TLCUTQA_Bundle_FK,
                                Description = g.Key.TLCUTSHRD_BoxNumber
                            };

                Query = Query.OrderBy(x => x.Description);

             
                foreach (var Receipt in Query)
                {
                    cmboCutSheets.Items.Add(new Cutting.CheckComboBoxItem(Receipt.Pk, Receipt.Description, false));
                }

                var Sizes = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Size in Sizes)
                {
                    cmboSizes.Items.Add(new Cutting.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
            }

            formloaded = true;

        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboQAResults_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
               Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
               if (item.CheckState)
               {
                   parms.QAResults.Add(repo.LoadQAResults(item._Pk));
               }
               else
               {
                   var value = parms.QAResults.Find(it => it.TLCUTQA_Bundle_FK == item._Pk);
                   if (value != null)
                       parms.QAResults.Remove(value);
               }
           }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Sizes.Add(repo.LoadSize(item._Pk));
                }
                else
                {
                    var value = parms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        parms.Sizes.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && formloaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = parms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        parms.Styles.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            frmCutViewRep vRep = null;

            if (oBtn != null && formloaded)
            {
                if (rbFullDetail.Checked)
                    parms.QAFullDetail = true;
                else
                    parms.QAFullDetail = false;

                parms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                parms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                parms.ToDate = parms.ToDate.AddHours(23);
                
                
                vRep = new frmCutViewRep(8, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                cmboCutSheets.Items.Clear();
                cmboSizes.Items.Clear();
                cmboStyles.Items.Clear();

                frmQaReportSelection_Load(this, null);
                parms = new CuttingQueryParameters();
            }

        }
    }
}
