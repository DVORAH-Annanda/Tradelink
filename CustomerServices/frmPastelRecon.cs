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

namespace CustomerServices
{
    public partial class frmPastelRecon : Form
    {
        bool FormLoaded;
        CustomerServices.Repository Repo;
        CustomerServices.CustomerServicesParameters QueryParms;
        Util core = null;

        public frmPastelRecon()
        {
            InitializeComponent();
            Repo = new Repository();
            core = new Util();

            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);


        }

        private void frmPastelRecon_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CustomerServices.CustomerServicesParameters();

            using (var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(Repo.LoadStyle(item._Pk));
                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QueryParms.FromDate = dtpFromDate.Value.Date;
                QueryParms.ToDate = dtpToDate.Value.Date;

                frmCSViewRep vRep = new frmCSViewRep(28, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}
