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
    public partial class frmNonComplianceSelection : Form
    {
        bool FormLoaded;

        CMTQueryParameters QueryParms;
        CMTRepository repo;

        public frmNonComplianceSelection()
        {
            InitializeComponent();

            repo = new CMTRepository();

            this.cmboCutSheets.CheckStateChanged += new System.EventHandler(this.cmboCutSheets_CheckStateChanged);
            this.cmbONonCompliance.CheckStateChanged += new System.EventHandler(this.cmboNonCompliance_CheckStateChanged);
        }

        private void frmNonComplianceSelection_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var CSheets = (from NCR in context.TLCMT_NonCompliance
                               join CS in context.TLCUT_CutSheet on NCR.CMTNCD_CutSheet_Fk equals CS.TLCutSH_Pk 
                               select CS).Distinct().ToList();

                foreach(var CSheet in CSheets)
                {
                    cmboCutSheets.Items.Add(new CMT.CheckComboBoxItem(CSheet.TLCutSH_Pk, CSheet.TLCutSH_No, false));
                }

                var NonCDetails = context.TLADM_CMTNonCompliance.OrderBy(x => x.CMTNC_ShortCode).ToList(); 
                foreach(var Detail in NonCDetails)
                {
                    cmbONonCompliance.Items.Add(new CMT.CheckComboBoxItem(Detail.CMTNC_Pk, Detail.CMTNC_Description, false));
                }
            }

            QueryParms = new CMTQueryParameters();

            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboCutSheets_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.CutSheets.Add(repo.LoadCutSheet(item._Pk));
                }
                else
                {
                    var value = QueryParms.CutSheets.Find(it => it.TLCutSH_Pk  == item._Pk);
                    if (value != null)
                        QueryParms.CutSheets.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboNonCompliance_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.NonADMCompliances.Add(repo.LoadNoneCompliance(item._Pk));

                }
                else
                {
                    var value = QueryParms.NonADMCompliances.Find(it => it.CMTNC_Pk == item._Pk);
                    if (value != null)
                        QueryParms.NonADMCompliances.Remove(value);

                }
            }
        }
        private void cmboCutSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmbONonCompliance_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker odtp = (DateTimePicker)sender;
            if (odtp != null && FormLoaded)
                QueryParms.UseDatePicker = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                if (QueryParms.UseDatePicker)
                { 
                    QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    QueryParms.ToDate = QueryParms.ToDate.AddHours(23);
                }


                frmCMTViewRep vRep = new frmCMTViewRep(35, QueryParms, false);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
