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

namespace ProductionPlanning
{
    public partial class frmInterDeptDskAnalysis : Form
    {
        bool FormLoad;
        ProdQueryParameters QueryParms;
        PPSRepository repo;
        Util core;
        protected readonly TTI2Entities _context;
        public frmInterDeptDskAnalysis()
        {
            InitializeComponent();
            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboQuality_CheckStateChanged);
            core = new Util();

            _context = new TTI2Entities();

            repo = new PPSRepository();

        }

        private void frmInterDeptDskAnalysis_Load(object sender, EventArgs e)
        {
            FormLoad = false;
            QueryParms = new ProdQueryParameters();

            var Qualities = _context.TLADM_Griege.OrderBy(x=>x.TLGreige_Description).ToList();
            foreach (var Quality in Qualities)
            {
                cmboQuality.Items.Add(new CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description , false));
            }
            FormLoad = true;
        }

        private void cmboQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is ProductionPlanning.CheckComboBoxItem && FormLoad)
            {
                ProductionPlanning.CheckComboBoxItem item = (ProductionPlanning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Greiges.Add(repo.LoadGreige(item._Pk));
                }
                else
                {
                    var value = QueryParms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Greiges.Remove(value);

                }
            }
        }
       
        private void btnProcess_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoad)
            {
                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                frmPPSViewRep vRep = new frmPPSViewRep(8, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if(vRep != null)
                {
                    vRep.Dispose();
                }

                frmInterDeptDskAnalysis_Load(this, null);
            }

        }

        private void cmboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}
